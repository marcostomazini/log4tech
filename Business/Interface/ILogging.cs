using System.EnterpriseServices;
using System.IO;
using System.Runtime.InteropServices;

[assembly: ApplicationName("log4tech")]
[assembly: Description("MTTech log layer com+")]
[assembly: ApplicationActivation(ActivationOption.Server)]
[assembly: ApplicationAccessControl(false, AccessChecksLevel = AccessChecksLevelOption.ApplicationComponent)]
namespace log4tech.Business.Interface
{
    [Guid("ADF0D549-D84B-422c-A15E-5B22C1E35FB5")]
    interface ILogging
    {
        void Info(string message);

        void Error(string message);

        string InfoWithString(string message);

        string ErrorWithString(string message);

        bool InfoWithBool(string message);

        bool ErrorWithBool(string message);

        string Teste();

        FileInfo GetNameFileConfig();

        string GetLogFileName();
    }
}
