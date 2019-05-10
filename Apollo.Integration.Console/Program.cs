using System;
using System.IO;
using System.Linq;
using Apollo.Core.Configuration;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Configuration;
using Apollo.Infrastructure.Logger;
using Microsoft.Extensions.Configuration;
using SimpleInjector;

namespace Apollo.Integration.Console
{
    static class Program
    {
        private static readonly Container Container;

        static Program()
        {
            Container = new Container();
            var configBuilder = new ConfigurationBuilder();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            configBuilder
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", true);

            var configuration = configBuilder.Build();

            InitializeContainer(Container, configuration);
        }

        static void Main(string[] args)
        {
            var service = Container.GetInstance<IClientIntegrationService>();
            var result = service.ExecuteAsync().Result;

            System.Console.WriteLine(result.IsSuccessful);
        }

        private static void InitializeContainer(Container container, IConfiguration configuration)
        {
            // Get configuration settings
            container.RegisterInstance<IAuditConfiguration>(
                configuration.GetSection("AuditConfiguration")
                    .Get<AuditConfiguration>());

            #region Core Assembly
            // Register services
            var coreAssembly = typeof(IAuditConfiguration).Assembly;
            var registrations = coreAssembly.GetExportedTypes()
                .Where(t => t.Namespace.EndsWith("Services") && t.GetInterfaces().Any())
                .Select(t => new { Service = t.GetInterfaces().Single(), Implemtation = t });

            foreach (var reg in registrations)
                container.Register(reg.Service, reg.Implemtation, Lifestyle.Transient);

            registrations = coreAssembly.GetExportedTypes()
                .Where(t => t.Namespace.Contains("DomainServices") && t.GetInterfaces().Any())
                .Where(t => !t.IsAbstract)
                .Select(t => new { Service = t.GetInterfaces().Single(i => i.Namespace.Contains("Contract")), Implemtation = t });

            foreach (var reg in registrations)
                container.Register(reg.Service, reg.Implemtation, Lifestyle.Transient);
            #endregion	Core Assembly

            #region Infrastructure Assembly

            var infrastructureAssembly = typeof(LogManager).Assembly;
            registrations = infrastructureAssembly.GetExportedTypes()
                .Where(t => t.Namespace.EndsWith("Logger") && t.GetInterfaces().Any())
                .Select(t => new { Service = t.GetInterfaces().Single(), Implemtation = t });

            foreach (var reg in registrations)
                Container.Register(reg.Service, reg.Implemtation, Lifestyle.Singleton);

            registrations = infrastructureAssembly.GetExportedTypes()
                .Where(t => t.Namespace.EndsWith("Repositories.v1") && t.GetInterfaces().Any())
                .Select(t => new { Service = t.GetInterfaces().Single(), Implemtation = t });

            foreach (var reg in registrations)
                Container.Register(reg.Service, reg.Implemtation, Lifestyle.Transient);

            registrations = infrastructureAssembly.GetExportedTypes()
                .Where(t => t.Namespace.EndsWith("Factories") && t.GetInterfaces().Any())
                .Select(t => new { Service = t.GetInterfaces().Single(), Implemtation = t });

            foreach (var reg in registrations)
                Container.Register(reg.Service, reg.Implemtation, Lifestyle.Transient);

            #endregion Infrastructure Assembly

            container.Verify();
        }
    }
}
