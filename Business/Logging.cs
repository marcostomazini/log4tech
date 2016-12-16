using log4net;
using log4net.Appender;
using log4net.Config;
using System;
using System.EnterpriseServices;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: ApplicationName("log4tech")]
[assembly: Description("MTTech log layer com+")]
[assembly: ApplicationActivation(ActivationOption.Server)]
[assembly: ApplicationAccessControl(false, AccessChecksLevel = AccessChecksLevelOption.ApplicationComponent)]
namespace log4tech
{
    [Guid("ADF0D549-D84B-422c-A15E-5B22C1E35FB5")]
    public class Logging : ServicedComponent, ILogging
    {
        private static readonly ILog log = LogManager.GetLogger("PortoLogger");
        private static readonly log4net.Core.Level AuditoriaLevel = new log4net.Core.Level(50000, "AUDIT");

        public Logging()
        {
            LogManager.GetRepository().LevelMap.Add(AuditoriaLevel);
            XmlConfigurator.ConfigureAndWatch(GetNameFileConfig());
        }

        public void Audit(string message)
        {
            log.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, AuditoriaLevel, message, null);
        }

        public void Info(string message)
        {
            log.Info(message);
        }

        public void Error(string message)
        {
            log.Error(message);
        }

        public string AuditWithString(string message)
        {
            Audit(message);
            return message;
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
            Audit("Audit is OK");

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
