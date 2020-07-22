using System;
using System.Runtime.Serialization;

namespace Sin.Net.Domain.Exeptions
{
    /// <summary>
    /// This is an exception type for the persistence layer.
    /// Use this exception for procedures related to persistence functionality.
    /// </summary>
    public class PersistenceException : Exception
    {
        /// <summary>
        /// Default empty constructor
        /// </summary>
        public PersistenceException()
        {
        }

        /// <summary>
        /// This constructor calls the base class constructor and sets the message property.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        public PersistenceException(string message) : base(message)
        {
        }

        /// <summary>
        /// This constructor calls the base class constructor and sets the message and inner exception property.
        /// </summary>
        /// <param name="message">The message of this instance.</param>
        /// <param name="innerException">The inner exception of this instance.</param>
        public PersistenceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// This constructor calls the base class constructor and sets the serialization info and the streaming context.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected PersistenceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
