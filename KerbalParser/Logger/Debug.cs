using System;

namespace KerbalParser
{
    public class Debug
    {
        private static Logger _logger;

        public static ILogger logger
        {
            get
            {
                return Debug._logger;
            }
        }

        public static void SetLogHandler(ILogHandler logHandler)
        {
            if (Debug._logger == null)
            {
                Debug._logger = new Logger(logHandler);
            }
            else
            {
                Debug._logger.LogHandler = logHandler;
            }
        }

        public static void Log(object message)
        {
            Debug._logger.Log(Logger.LogType.Log, message);
        }

        public static void Log(object message, Object context)
        {
            Debug._logger.Log(Logger.LogType.Log, message, context);
        }

        public static void LogFormat(string format, params object[] args)
        {
            Debug._logger.LogFormat(Logger.LogType.Log, format, args);
        }

        public static void LogFormat(Object context, string format, params object[] args)
        {
            Debug._logger.LogFormat(Logger.LogType.Log, context, format, args);
        }

        public static void LogError(object message)
        {
            Debug._logger.Log(Logger.LogType.Error, message);
        }

        public static void LogError(object message, Object context)
        {
            Debug._logger.Log(Logger.LogType.Error, message, context);
        }

        public static void LogErrorFormat(string format, params object[] args)
        {
            Debug._logger.LogFormat(Logger.LogType.Error, format, args);
        }

        public static void LogErrorFormat(Object context, string format, params object[] args)
        {
            Debug._logger.LogFormat(Logger.LogType.Error, context, format, args);
        }

        public static void LogException(Exception exception)
        {
            Debug._logger.LogException(exception, null);
        }

        public static void LogException(Exception exception, Object context)
        {
            Debug._logger.LogException(exception, context);
        }

        public static void LogWarning(object message)
        {
            Debug._logger.Log(Logger.LogType.Warning, message);
        }

        public static void LogWarning(object message, Object context)
        {
            Debug._logger.Log(Logger.LogType.Warning, message, context);
        }

        public static void LogWarningFormat(string format, params object[] args)
        {
            Debug._logger.LogFormat(Logger.LogType.Warning, format, args);
        }

        public static void LogWarningFormat(Object context, string format, params object[] args)
        {
            Debug._logger.LogFormat(Logger.LogType.Warning, context, format, args);
        }
    }
}
