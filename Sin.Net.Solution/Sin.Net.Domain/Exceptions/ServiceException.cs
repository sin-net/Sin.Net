using System;
using System.Runtime.Serialization;

namespace Sin.Net.Domain.Infrastructure.Http
{
    /// <summary>
    /// The exception type that is thrown when the access to an external service fails,
    /// e.g. Http or Mqtt calls from the infrastructure layer. 
    /// </summary>
    public class ServiceException : Exception
    {
        /// <summary>
        /// Default empty constructor
        /// </summary>
        public ServiceException()
        {
        }

        /// <summary>
        /// Constructor that calls the base class and adds a standard message before the message parameter.
        /// </summary>
        /// <param name="message">The additional message of the client code.</param>
        public ServiceException(string message) : base($"Service call failed: {message}")
        {
        }

        /// <summary>
        /// Constructor that calls the base class and adds a standard message and the inner exception.
        /// </summary>
        /// <param name="message">The additional message of the client code.</param>
        /// <param name="innerException">The inner exception.</param>
        public ServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor that calls the base class and fowards the SerializationInfo and the StreamingContext.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}