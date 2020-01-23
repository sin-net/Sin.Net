using System;

namespace Sin.Net.Domain.Persistence.Logging
{
    /// <summary>
    /// This interface defines the implementation of concrete logging classes that will be injected and used in the static Log class.
    /// </summary>
    public interface ILoggable
    {
        /// <summary>
        /// The startup routine for the logging implementation. 
        /// Call this method in the constructor for auto-start readiness of the logging.
        /// </summary>
        ILoggable Start();

        /// <summary>
        /// The stop routine.
        /// </summary>
        void Stop();

        /// <summary>
        /// Log the trace message.
        /// </summary>
        /// <param name="msg">The message string</param>
        void Trace(string msg);

        /// <summary>
        /// Log the debug message.
        /// </summary>
        /// <param name="msg">The message string</param>
        void Debug(string msg);

        /// <summary>
        /// Log the info message.
        /// </summary>
        /// <param name="msg">The message string</param>
        void Info(string msg);

        /// <summary>
        /// Log the warning message.
        /// </summary>
        /// <param name="msg">The message string</param>
        void Warn(string msg);

        /// <summary>
        /// Log the error message.
        /// </summary>
        /// <param name="msg">The message string</param>
        void Error(string msg);

        /// <summary>
        /// Log the exception.
        /// </summary>
        /// <param name="msg">The message string</param>
        void Fatal(Exception ex);
    }
}
