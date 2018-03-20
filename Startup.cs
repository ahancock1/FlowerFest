// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   Startup.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest
{
    using System.IO;
    using Areas.Dashboard.Mappings;
    using AutoMapper;
    using Mappings;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
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

        private void ConfigureMappings(IServiceCollection services)
        {
            // Mappings
            services
                .AddSingleton<IMapperConfiguration, DashboardMappings>();

            services
                .AddSingleton<IMapperConfiguration, BlogMappings>()
                .AddSingleton<IMapperConfiguration, CommentMappings>()
                .AddSingleton<IMapperConfiguration, PartnerMappings>()
                .AddSingleton<IMapperConfiguration, TestimonalMappings>()
                .AddSingleton<IMapperConfiguration, BlogMappings>()
                .AddSingleton<IMapperConfiguration, SectionMappings>();

            services.AddSingleton(provider =>
            {
                var mapper = new MapperConfiguration(config =>
                {
                    foreach (var mapping in provider.GetServices<IMapperConfiguration>())
                    {
                        mapping.Configure(config);
                    }
                });
                return mapper.CreateMapper();
            });
        }

        private void ConfigureRepositories(IServiceCollection services, string webroot)
        {
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
                        Path.Combine(webroot, "Support")))
                .AddSingleton<ISectionRepository>(s =>
                    new SectionRepository(
                        Path.Combine(webroot, "Sections")));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IMailService, MailService>();
            
            var webroot = _environment.WebRootPath;

            ConfigureMappings(services);
            ConfigureRepositories(services, webroot);
            
            // Uploads
            services
                .AddSingleton<IFileService>(s =>
                    new FileService(
                        Path.Combine(webroot, "Uploads")));
            
            // Services
            services
                .AddSingleton<IBlogService, BlogService>()
                .AddSingleton<ITestimonalService, TestimonalService>()
                .AddSingleton<IPartnerService, PartnerService>()
                .AddSingleton<ISectionService, SectionService>();
            

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
                    "areas",
                    "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}