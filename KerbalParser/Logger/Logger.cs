using System;

namespace KerbalParser
{
    public class Logger : ILogger, ILogHandler
    {
        public enum LogType
        {
            Error,
            Warning,
            Log,
            Exception
        }

        private ILogHandler _logHandler;

        private bool _logEnabled;

        private Logger.LogType _filterLogType;

        public ILogHandler LogHandler
        {
            get
            {
                return this._logHandler;
            }
            set
            {
                this._logHandler = value;
            }
        }

        public bool LogEnabled
        {
            get
            {
                return this._logEnabled;
            }
            set
            {
                this._logEnabled = value;
            }
        }

        public Logger.LogType FilterLogType
        {
            get
            {
                return this._filterLogType;
            }
            set
            {
                this._filterLogType = value;
            }
        }

        private Logger()
        {
        }

        public Logger(ILogHandler logHandler)
        {
            this._logHandler = logHandler;
            this._logEnabled = true;
            this._filterLogType = Logger.LogType.Log;
        }

        public bool IsLogTypeAllowed(Logger.LogType logType)
        {
            return this._logEnabled && (logType <= this._filterLogType || logType == Logger.LogType.Exception);
        }

        private static string GetString(object message)
        {
            return (message == null) ? "Null" : message.ToString();
        }

        public void Log(Logger.LogType logType, object message)
        {
            if (this.IsLogTypeAllowed(logType))
            {
                this._logHandler.LogFormat(logType, null, "{0}", new object[]
                {
                    Logger.GetString(message)
                });
            }
        }

        public void Log(Logger.LogType logType, object message, Object context)
        {
            if (this.IsLogTypeAllowed(logType))
            {
                this._logHandler.LogFormat(logType, context, "{0}", new object[]
                {
                    Logger.GetString(message)
                });
            }
        }

        public void Log(Logger.LogType logType, string tag, object message)
        {
            if (this.IsLogTypeAllowed(logType))
            {
                this._logHandler.LogFormat(logType, null, "{0}: {1}", new object[]
                {
                    tag,
                    Logger.GetString(message)
                });
            }
        }

        public void Log(Logger.LogType logType, string tag, object message, Object context)
        {
            if (this.IsLogTypeAllowed(logType))
            {
                this._logHandler.LogFormat(logType, context, "{0}: {1}", new object[]
                {
                    tag,
                    Logger.GetString(message)
                });
            }
        }

        public void Log(object message)
        {
            if (this.IsLogTypeAllowed(Logger.LogType.Log))
            {
                this._logHandler.LogFormat(Logger.LogType.Log, null, "{0}", new object[]
                {
                    Logger.GetString(message)
                });
            }
        }

        public void Log(string tag, object message)
        {
            if (this.IsLogTypeAllowed(Logger.LogType.Log))
            {
                this._logHandler.LogFormat(Logger.LogType.Log, null, "{0}: {1}", new object[]
                {
                    tag,
                    Logger.GetString(message)
                });
            }
        }

        public void Log(string tag, object message, Object context)
        {
            if (this.IsLogTypeAllowed(Logger.LogType.Log))
            {
                this._logHandler.LogFormat(Logger.LogType.Log, context, "{0}: {1}", new object[]
                {
                    tag,
                    Logger.GetString(message)
                });
            }
        }

        public void LogWarning(string tag, object message)
        {
            if (this.IsLogTypeAllowed(Logger.LogType.Warning))
            {
                this._logHandler.LogFormat(Logger.LogType.Warning, null, "{0}: {1}", new object[]
                {
                    tag,
                    Logger.GetString(message)
                });
            }
        }

        public void LogWarning(string tag, object message, Object context)
        {
            if (this.IsLogTypeAllowed(Logger.LogType.Warning))
            {
                this._logHandler.LogFormat(Logger.LogType.Warning, context, "{0}: {1}", new object[]
                {
                    tag,
                    Logger.GetString(message)
                });
            }
        }

        public void LogError(string tag, object message)
        {
            if (this.IsLogTypeAllowed(Logger.LogType.Error))
            {
                this._logHandler.LogFormat(Logger.LogType.Error, null, "{0}: {1}", new object[]
                {
                    tag,
                    Logger.GetString(message)
                });
            }
        }

        public void LogError(string tag, object message, Object context)
        {
            if (this.IsLogTypeAllowed(Logger.LogType.Error))
            {
                this._logHandler.LogFormat(Logger.LogType.Error, context, "{0}: {1}", new object[]
                {
                    tag,
                    Logger.GetString(message)
                });
            }
        }

        public void LogFormat(Logger.LogType logType, string format, params object[] args)
        {
            if (this.IsLogTypeAllowed(logType))
            {
                this._logHandler.LogFormat(logType, null, format, args);
            }
        }

        public void LogException(Exception exception)
        {
            if (this._logEnabled)
            {
                this._logHandler.LogException(exception, null);
            }
        }

        public void LogFormat(Logger.LogType logType, Object context, string format, params object[] args)
        {
            if (this.IsLogTypeAllowed(logType))
            {
                this._logHandler.LogFormat(logType, context, format, args);
            }
        }

        public void LogException(Exception exception, Object context)
        {
            if (this._logEnabled)
            {
                this._logHandler.LogException(exception, context);
            }
        }
    }
}
