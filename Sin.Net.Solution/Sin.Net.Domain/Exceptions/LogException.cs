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

        public LogException(string message) : base(message)
        {
        }

        public LogException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LogException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
