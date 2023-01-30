using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using log4net.Repository;
using Microsoft.Extensions.Logging;
using System.Xml;
using System.IO;
using System.Reflection;

namespace aravindMiddleware.Data
{
    public class Log4Net
    {
        private static log4net.ILog log = null;
        private static XmlDocument log4netConfig = null;
        private static ILoggerRepository iLogger = null;

        private static void LogToFile(String message, LogLevel logType, Exception ex)
        {
            if (logType == LogLevel.Debug) { log.Debug(message, ex); }
            if (logType == LogLevel.Information) { log.Info(message, ex); }
            if (logType == LogLevel.Warning) { log.Warn(message, ex); }
            if (logType == LogLevel.Error) { log.Error(message, ex); }
        }

        public static void LogEvent(LogLevel logType, string className, string functionName, string message, Exception ex = null)
        {

            if (log4netConfig == null || iLogger == null)
            {
                log4netConfig = new XmlDocument();
                log4netConfig.Load(File.OpenRead(Path.Combine(System.AppContext.BaseDirectory, "log4net.config")));

                iLogger = log4net.LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
                log4net.Config.XmlConfigurator.Configure(iLogger, log4netConfig["log4net"]);
            }
            log = LogManager.GetLogger(typeof(Log4Net));
            LogToFile(String.Format("{0}|{1} - {2}", className, functionName, message), logType, ex);
        }
    }
}
