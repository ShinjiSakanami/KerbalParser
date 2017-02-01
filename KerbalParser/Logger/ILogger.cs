using System;

namespace KerbalParser
{
    public interface ILogger
    {
        ILogHandler LogHandler { get; set; }

        bool LogEnabled { get; set; }

        Logger.LogType FilterLogType { get; set; }

        bool IsLogTypeAllowed(Logger.LogType logType);

        void Log(Logger.LogType logType, object message);

        void Log(Logger.LogType logType, object message, Object context);

        void Log(Logger.LogType logType, string tag, object message);

        void Log(Logger.LogType logType, string tag, object message, Object context);

        void Log(object message);

        void Log(string tag, object message);

        void Log(string tag, object message, Object context);

        void LogWarning(string tag, object message);

        void LogWarning(string tag, object message, Object context);

        void LogError(string tag, object message);

        void LogError(string tag, object message, Object context);

        void LogFormat(Logger.LogType logType, string format, params object[] args);

        void LogException(Exception exception);
    }
}
