using System.IO;

namespace log4tech
{
    interface ILogging
    {
        void Audit(string message);

        void Info(string message);

        void Error(string message);

        string AuditWithString(string message);

        string InfoWithString(string message);

        string ErrorWithString(string message);

        bool InfoWithBool(string message);

        bool ErrorWithBool(string message);

        string Teste();

        FileInfo GetNameFileConfig();

        string GetLogFileName();
    }
}
