// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   Startup.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest
{
    using System.IO;
    using AutoMapper;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Middleware;
    using Repository;
    using Repository.Interfaces;
    using Services;
    using Services.Interfaces;

    public class Startup
    {
        private readonly IHostingEnvironment _environment;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IMailService, MailService>();
            services.AddSingleton<IOldBlogService, OldBlogService>();
            services.AddSingleton<ITestimonalService, TestimonalService>();

            // Webroot
            var webroot = _environment.WebRootPath;

            // Mappings
            services.AddAutoMapper();

            // Uploads
            services
                .AddSingleton<IFileService>(s =>
                    new FileService(
                        Path.Combine(webroot, "Uploads")));

            // Repositories
            services
                .AddSingleton<IBlogRepository>(s =>
                    new BlogRepository(
                        Path.Combine(webroot, "Posts")))
                .AddSingleton<ITestimonalRepository>(s =>
                    new TestimonalRepository(
                        Path.Combine(webroot, "Testimonals")))
                .AddSingleton<IPartnerRepository>(s =>
                    new PartnerRepository(
                        Path.Combine(webroot, "Support")));

            // Services
            services.AddSingleton<IBlogService, BlogService>();

            
            services.Configure<BlogSettings>(Configuration.GetSection("blog"));
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Cookie authentication.
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/login/";
                    options.LogoutPath = "/logout/";
                });

            services.AddWebOptimizer();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePages("text/plain", "Status code page, status code: {0}");

            app.UseStaticFiles(); // For the wwwroot folder
            
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}