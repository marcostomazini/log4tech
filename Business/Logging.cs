using log4net;
using log4net.Appender;
using log4net.Config;
using log4tech.Business.Interface;
using System;
using System.EnterpriseServices;
using System.IO;
using System.Reflection;

namespace log4tech
{
    public class Logging : ServicedComponent, ILogging
    {
        private static readonly ILog log = LogManager.GetLogger("ErrorLogger");

        public Logging()
        {
            XmlConfigurator.ConfigureAndWatch(GetNameFileConfig());
        }        

        public void Info(string message)
        {
            log.Info(message);
        }

        public void Error(string message)
        {
            log.Error(message);
        }

        public string InfoWithString(string message)
        {
            log.Info(message);
            return message;
        }

        public string ErrorWithString(string message)
        {
            log.Error(message);
            return message;
        }

        public bool InfoWithBool(string message)
        {
            log.Info(message);
            return log.IsInfoEnabled;
        }

        public bool ErrorWithBool(string message)
        {
            log.Error(message);
            return log.IsErrorEnabled;
        }

        public string Teste()
        {
            log.Debug("Debug is OK");
            log.Info("Info is OK");
            log.Warn("Warn is OK");
            log.Error("Error is OK");
            return "pow " + GetNameFileConfig().FullName;
        }

        public FileInfo GetNameFileConfig()
        {
            string absolutePath = (new Uri(Assembly.GetExecutingAssembly().CodeBase))
                .AbsolutePath;

            return new FileInfo($"{absolutePath}.config");
        }

        public string GetLogFileName()
        {
            var filename = string.Empty;

            IAppender[] appenders = log.Logger.Repository.GetAppenders();
            foreach (IAppender appender in appenders)
            {
                Type type = appender.GetType();
                if (type.Equals(typeof(FileAppender)) || type.Equals(typeof(RollingFileAppender)))
                {
                    filename = ((FileAppender)appender).File;
                    break;
                }
            }
            return filename;
        }
    }
}
