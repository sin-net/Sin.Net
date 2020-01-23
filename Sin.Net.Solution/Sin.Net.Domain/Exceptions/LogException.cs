using System;
using System.Runtime.Serialization;

namespace Sin.Net.Domain.Exeptions
{
    /// <summary>
    /// This is an exception type for the static Log class. Use this exception only when the log system fails.
    /// </summary>
    internal class LogException : Exception
    {
        /// <summary>
        /// Default empty constructor
        /// </summary>
        public LogException()
        {
        }

        /// <summary>
        /// This constructor calls the base class constructor and sets the message property.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        public LogException(string message) : base(message)
        {
        }

        /// <summary>
        /// This constructor calls the base class constructor and sets the message and inner exception property.
        /// </summary>
        /// <param name="message">The message of this instance.</param>
        /// <param name="innerException">The inner exception of this instance.</param>
        public LogException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// This constructor calls the base class constructor and sets the serialization info and the streaming context.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected LogException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
