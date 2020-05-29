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

        void Trace(object data);

        /// <summary>
        /// Log the debug message.
        /// </summary>
        /// <param name="msg">The message string</param>
        void Debug(string msg);

        void Debug(object data);

        /// <summary>
        /// Log the info message.
        /// </summary>
        /// <param name="msg">The message string</param>
        void Info(string msg);

        void Info(object data);

        /// <summary>
        /// Log the warning message.
        /// </summary>
        /// <param name="msg">The message string</param>
        void Warn(string msg);

        void Warn(object data);

        /// <summary>
        /// Log the error message.
        /// </summary>
        /// <param name="msg">The message string</param>
        void Error(string msg);

        void Error(object data);

        /// <summary>
        /// Log the exception.
        /// </summary>
        /// <param name="msg">The message string</param>
        void Fatal(Exception ex);

        /// <summary>
        /// Converts objects into string messages that can be logged by the static Log class.
        /// </summary>
        /// <param name="attachment">Any kind of object that is handed as attachment parameter.</param>
        /// <returns></returns>
        string Convert(object attachment);
    }
}
