using System;

namespace KerbalParser
{
    public interface ILogHandler
    {
        void LogFormat(Logger.LogType logType, Object context, string format, params object[] args);

        void LogException(Exception exception, Object context);
    }
}
