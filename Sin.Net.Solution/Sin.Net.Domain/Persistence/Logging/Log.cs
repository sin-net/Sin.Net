using Sin.Net.Domain.Exeptions;
using System;

namespace Sin.Net.Domain.Persistence.Logging
{
    public static class Log
    {

        // -- fields

        private static ILoggable _logger;
        private static readonly string NO_LOGGER_EXCEPTION = "no logger instance is present";
        private static readonly string SEP = "-";

        // -- methods

        private static string ToMessage(string message, string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return message;
            }
            else
            {
                return $"{source} {SEP} {message}";
            }
        }

        #region Trace
        public static void Trace(string msg)
        {
            L?.Trace(ToMessage(msg, null));
        }
        public static void Trace(string msg, string src)
        {
            L?.Trace(ToMessage(msg, src));
        }
        public static void Trace(string msg, object sender)
        {
            L?.Trace(ToMessage(msg, sender?.GetType().Name));
        }
        public static void Trace(string msg, string src, object attachment)
        {
            L?.Trace(ToMessage(msg, src));
            L?.Trace(attachment);
        }
        public static void Trace(string msg, object sender, object attachment)
        {
            L?.Trace(ToMessage(msg, sender?.GetType().Name));
            L?.Trace(attachment);
        }
        #endregion

        #region Debug
        public static void Debug(string msg)
        {
            L?.Debug(ToMessage(msg, null));
        }
        public static void Debug(string msg, string src)
        {
            L?.Debug(ToMessage(msg, src));
        }
        public static void Debug(string msg, object sender)
        {
            Debug(msg, sender.GetType().Name);
        }
        public static void Debug(string msg, string src, object attachment)
        {
            L?.Debug(ToMessage(msg, src));
            L?.Debug(attachment);
        }
        public static void Debug(string msg, object sender, object attachment)
        {
            L?.Debug(ToMessage(msg, sender?.GetType().Name));
            L?.Debug(attachment);
        }
        #endregion

        #region Info
        public static void Info(string msg)
        {
            L?.Info(ToMessage(msg, null));
        }
        public static void Info(string msg, string src)
        {
            L?.Info(ToMessage(msg, src));
        }
        public static void Info(string msg, object sender)
        {
            Info(msg, sender.GetType().Name);
        }
        public static void Info(string msg, string src, object attachment)
        {
            L?.Info(ToMessage(msg, src));
            L?.Info(attachment);
        }
        public static void Info(string msg, object sender, object attachment)
        {
            L?.Info(ToMessage(msg, sender?.GetType().Name));
            L?.Info(attachment);
        }
        #endregion

        #region Warn
        public static void Warn(string msg)
        {
            L?.Warn(ToMessage(msg, null));
        }
        public static void Warn(string msg, string src)
        {
            L?.Warn(ToMessage(msg, src));
        }
        public static void Warn(string msg, object sender)
        {
            Warn(msg, sender.GetType().Name);
        }
        public static void Warn(string msg, string src, object attachment)
        {
            L?.Warn(ToMessage(msg, src));
            L?.Warn(attachment);
        }
        public static void Warn(string msg, object sender, object attachment)
        {
            L?.Warn(ToMessage(msg, sender?.GetType().Name));
            L?.Warn(attachment);
        }
        #endregion

        #region Error
        public static void Error(string msg)
        {
            L?.Error(ToMessage(msg, null));
        }
        public static void Error(string msg, string src)
        {
            L?.Error(ToMessage(msg, src));
        }

        public static void Error(string msg, object sender)
        {
            Error(msg, sender.GetType().Name);
        }
        public static void Error(string msg, string src, object attachment)
        {
            L?.Error(ToMessage(msg, src));
            L?.Error(attachment);
        }
        public static void Error(string msg, object sender, object attachment)
        {
            L?.Error(ToMessage(msg, sender?.GetType().Name));
            L?.Error(attachment);
        }
        #endregion

        public static void Fatal(Exception ex)
        {
            L?.Fatal(ex);
        }

        public static void Stop()
        {
            L?.Stop();
        }

        /// <summary>
        /// Injects concrete ILoggable implementation. Calling this method multiple times is allowed.
        /// </summary>
        /// <param name="logger"></param>
        public static void Inject(ILoggable logger)
        {
            L = logger;
        }

        // -- properties

        /// <summary>
        /// Gets the information, if the logger instance is present or not.
        /// </summary>
        public static bool IsNotNull => _logger != null;

        /// <summary>
        /// Gets or sets the logger implementation
        /// </summary>
        private static ILoggable L
        {
            get
            {
                return _logger;
            }
            set
            {
                if (value != null)
                {
                    _logger = value;
                }
                else
                {
                    throw new LogException(NO_LOGGER_EXCEPTION);
                }
            }
        }
    }
}
