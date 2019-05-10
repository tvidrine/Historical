using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Apollo.Core.Configuration;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Configuration;
using Apollo.Core.Domain.Document;
using Apollo.Infrastructure.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using Swashbuckle.AspNetCore.Swagger;

namespace Apollo.WebApi
{
    /// <summary>
    /// Web API Startup
    /// </summary>
    public class Startup
    {
        private readonly Container _container = new Container();
        private readonly IConfigurationRoot _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            _configuration = configBuilder.Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                //.AddAuthorization()
                .AddMvcOptions(options =>
                {
                    options.RespectBrowserAcceptHeader = true;
                    options.Filters.Add(new RequireHttpsAttribute());
                })
                .AddFormatterMappings()
                .AddJsonFormatters()
                .AddApiExplorer();

            // Authentication
            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        //options.Authority = "https://localhost:44301";
            //        //options.RequireHttpsMetadata = true;
            //        //options.ApiName = "Apollo.WebApi";

            //        options.Authority = "http://localhost:5000";
            //        options.RequireHttpsMetadata = false;

            //        options.ApiName = "Apollo.WebApi";
            //    });

            // Handle CORS
            services.AddCors(options =>
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()));

            // Dependency Injection
            IntegrateSimpleInjector(services);

            // Api Documentation
            services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments(XmlCommentsFilePath);

                options.SwaggerDoc("v2",
                    new Info
                    {
                        Title = "ZoomAudits - eAudit API",
                        Version = "v2",
                        Description = "A Web API for eAudits",
                        Contact = new Contact() { Name = "Zoom Audits", Email = "info@zoomaudits.com" }
                    });
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            InitializeContainer(app);

            app.UseRewriter(new RewriteOptions()
                                .AddRedirectToHttpsPermanent());
            app.UseAuthentication();
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(o => o.SwaggerEndpoint("/swagger/v2/swagger.json", "ZoomAudits - eAudit API"));

        }

        #region Private Helper Methods
        #region Simple Injector
        private void InitializeContainer(IApplicationBuilder app)
        {
            _container.RegisterMvcControllers(app);
            
            // Get configuration settings
            _container.RegisterInstance<IAuditConfiguration>(
                _configuration.GetSection("AuditConfiguration")
                    .Get<AuditConfiguration>());


            #region Core Assembly
            // Register services
            var coreAssembly = typeof(IAuditConfiguration).Assembly;
            var registrations = coreAssembly.GetExportedTypes()
                .Where(t => t.Namespace.EndsWith("Services") && t.GetInterfaces().Any())
                .Select(t => new { Service = t.GetInterfaces().Single(), Implemtation = t });

            foreach (var reg in registrations)
                _container.Register(reg.Service, reg.Implemtation, Lifestyle.Transient);

            registrations = coreAssembly.GetExportedTypes()
                .Where(t => t.Namespace.Contains("DomainServices.Validators") && t.GetInterfaces().Any())
                .Where(t => !t.IsAbstract)
                .Select(t => new { Service = t.GetInterfaces().Single(i => i.Namespace.Contains("Contract")), Implemtation = t });

            foreach (var reg in registrations)
                _container.Register(reg.Service, reg.Implemtation, Lifestyle.Transient);

            registrations = coreAssembly.GetExportedTypes()
                .Where(t => t.Namespace.Contains("DomainServices.Integration") && t.GetInterfaces().Any())
                .Where(t => !t.IsAbstract)
                .Select(t => new { Service = t.GetInterfaces().Single(i => i.Namespace.Contains("Contract")), Implemtation = t });

            foreach (var reg in registrations)
                _container.Register(reg.Service, reg.Implemtation, Lifestyle.Transient);
            #endregion	Core Assembly

            #region Infrastructure Assembly

            var infrastructureAssembly = typeof(LogManager).Assembly;
            registrations = infrastructureAssembly.GetExportedTypes()
                .Where(t => t.Namespace.EndsWith("Logger") && t.GetInterfaces().Any())
                .Select(t => new { Service = t.GetInterfaces().Single(), Implemtation = t });

            foreach (var reg in registrations)
                _container.Register(reg.Service, reg.Implemtation, Lifestyle.Transient);

            registrations = infrastructureAssembly.GetExportedTypes()
                .Where(t => t.Namespace.EndsWith("Providers") && t.GetInterfaces().Any())
                .Select(t => new { Service = t.GetInterfaces().Single(), Implemtation = t });

            foreach (var reg in registrations.Where(reg => !reg.Service.Name.Contains("ICommunicationProvider")))
                _container.Register(reg.Service, reg.Implemtation, Lifestyle.Transient);

            registrations = infrastructureAssembly.GetExportedTypes()
                .Where(t => t.Namespace.EndsWith("Repositories") && t.GetInterfaces().Any())
                .Select(t => new { Service = t.GetInterfaces().Single(), Implemtation = t });

            foreach (var reg in registrations)
                _container.Register(reg.Service, reg.Implemtation, Lifestyle.Transient);

            registrations = infrastructureAssembly.GetExportedTypes()
                .Where(t => t.Namespace.EndsWith("Factories") && t.GetInterfaces().Any())
                .Select(t => new { Service = t.GetInterfaces().Single(), Implemtation = t });

            foreach (var reg in registrations)
                _container.Register(reg.Service, reg.Implemtation, Lifestyle.Transient);

            #endregion Infrastructure Assembly
            
            _container.Verify();

        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_container));

            services.EnableSimpleInjectorCrossWiring(_container);
            services.UseSimpleInjectorAspNetRequestScoping(_container);

        }
        #endregion Simple Injector
        #region Swagger Helpers
        private static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }

        }
        #endregion Swagger Helpers
        #endregion Private Helper Methods
    }
}
