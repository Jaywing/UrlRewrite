﻿using System;
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository;
using log4net.spi;
using log4net.Repository.Hierarchy;

namespace Hi.UrlRewrite
{
    public static class LogHelper
    {

        private static bool _initialized;

        public static void Inititalize()
        {
            if (!_initialized)
            {
                if (Configuration.LogFileEnabled)
                {
                    CreateLogger();
                }
                _initialized = true;
            }
        }

        private static IAppender CreateAppender()
        {
            var layout = new PatternLayout()
            {
                ConversionPattern = @"%4t %d{ABSOLUTE} %-5p %m%n"
            };

            var appender = new SitecoreLogFileAppender()
            {
                Name = "UrlRewriteAppender",
                AppendToFile = true,
                File = Configuration.LogFileName,
                Layout = layout
            };

            appender.ActivateOptions();

            return appender;
        }

        public static void CreateLogger()
        {
            var logger = (Logger)LogManager.GetLogger("Hi.UrlRewrite").Logger;
            IAppender appender = CreateAppender();

            logger.AddAppender(appender);
            logger.Additivity = false;

            string configLogLevel = Configuration.LogFileLevel;
            var repository = (ILoggerRepository)logger.Hierarchy;
            Level logLevel;

            try
            {
                logLevel = repository.LevelMap[configLogLevel];
            }
            catch (Exception)
            {
                logLevel = Level.INFO;
            }

            logger.Level = logLevel;
        }
    }
}