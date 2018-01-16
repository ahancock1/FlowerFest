﻿// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   Startup.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest
{
    using System;
    using FlowerFest.Services.Interfaces;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Rewrite;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Net.Http.Headers;
    using Services;
    using WebEssentials.AspNetCore.OutputCaching;
    using WebMarkupMin.AspNetCore2;
    using WebMarkupMin.Core;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IMailService, MailService>();
            services.AddSingleton<IBlogService, BlogService>();
            services.AddSingleton<ITestimonalService, TestimonalService>();

            services.Configure<BlogSettings>(Configuration.GetSection("blog"));
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddMetaWeblog<Services.MetaWeblogService>();

            // Output caching (https://github.com/madskristensen/WebEssentials.AspNetCore.OutputCaching)
            //services.AddOutputCaching(options =>
            //{
            //    options.Profiles["default"] = new OutputCacheProfile
            //    {
            //        Duration = 3600
            //    };
            //});

            // Cookie authentication.
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/login/";
                    options.LogoutPath = "/logout/";
                });

            // HTML minification (https://github.com/Taritsyn/WebMarkupMin)
            //services
            //    .AddWebMarkupMin(options =>
            //    {
            //        options.AllowMinificationInDevelopmentEnvironment = true;
            //        options.DisablePoweredByHttpHeaders = true;
            //    })
            //    .AddHtmlMinification(options =>
            //    {
            //        options.MinificationSettings.RemoveOptionalEndTags = false;
            //        options.MinificationSettings.WhitespaceMinificationMode = WhitespaceMinificationMode.Safe;
            //    });
            //services.AddSingleton<IWmmLogger, WmmNullLogger>(); // Used by HTML minifier

            // Bundling, minification and Sass transpilation (https://github.com/ligershark/WebOptimizer)
            services.AddWebOptimizer(
                //pipeline =>
            //{
            //    pipeline.MinifyJsFiles();
            //    pipeline.CompileScssFiles()
            //        .InlineImages(1);
            //}
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePages("text/plain", "Status code page, status code: {0}");
            //app.UseWebOptimizer();

            app.UseStaticFiles(); // For the wwwroot folder

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    OnPrepareResponse = context =>
            //    {
            //        var time = TimeSpan.FromDays(365);
            //        context.Context.Response.Headers[HeaderNames.CacheControl] =
            //            $"max-age={time.TotalSeconds}";
            //        context.Context.Response.Headers[HeaderNames.Expires] = DateTime.UtcNow.Add(time).ToString("R");
            //    }
            //});

            //if (Configuration.GetValue<bool>("forcessl"))
            //{
            //    app.UseRewriter(new RewriteOptions().AddRedirectToHttps());
            //}

            //app.UseMetaWeblog("/metaweblog");
            app.UseAuthentication();

            //app.UseOutputCaching();
            //app.UseWebMarkupMin();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}