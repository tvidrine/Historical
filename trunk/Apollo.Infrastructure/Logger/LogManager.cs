using System;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Configuration;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace Apollo.Infrastructure.Logger
{
    public class LogManager : ILogManager
    {
        private static readonly ILogger Logger = NLog.LogManager.GetCurrentClassLogger();
        
        public LogManager(IAuditConfiguration configuration)
        {
            ConfigureLogger(configuration);
        }
        public void LogInfo(string message)
        {
            Logger.Debug(message);
            Console.WriteLine(message);
        }

        public void LogWarn(string message)
        {
            Logger.Warn(message);
            Console.WriteLine(message);
        }

        public void LogDebug(string message)
        {
            Logger.Debug(message);
        }

        public void LogError(Exception ex, string message)
        {
            Logger.Error(ex, message);
            Console.WriteLine($@"{message}->{ex.Message}");

        }

        private static void ConfigureLogger(IAuditConfiguration configuration)
        {
            var config = new LoggingConfiguration();

            var databaseTarget = new NLog.Targets.DatabaseTarget
            {
                ConnectionString = configuration.AuditDataConnection,
                CommandText = @"
                    INSERT INTO [v2].[ApplicationLog]([Level], [Type], [Message]) 
                    VALUES(@level, @type, @message);"
            };
            databaseTarget.Parameters.Add(new DatabaseParameterInfo{ Name = "@level", Layout = "${level}"});
            databaseTarget.Parameters.Add(new DatabaseParameterInfo { Name = "@type", Layout = "${exception:format=type}" });
            databaseTarget.Parameters.Add(new DatabaseParameterInfo { Name = "@message", Layout = "${message:exceptionSeparator=->:withException=true}" });
            databaseTarget.Parameters.Add(new DatabaseParameterInfo { Name = "@stackTrace", Layout = "${exception:format=stackTrace}" });
           
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Error, databaseTarget));
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Warn, databaseTarget));
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, databaseTarget));

            var fileTarget = new FileTarget
            {
                FileName = $@"logs\errors_{DateTime.Now:yyyyMMdd}.log",
                Layout = Layout.FromString("${message:exceptionSeparator=->:withException=true}")
            };

            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Error, fileTarget));

            NLog.LogManager.Configuration = config;
        }
    }
}