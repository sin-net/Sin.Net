using System;
using System.Runtime.Serialization;

namespace Sin.Net.Domain.Infrastructure.Http
{

    public class ServiceException : Exception
    {
        public ServiceException()
        {
        }

        public ServiceException(string message) : base($"Service call failed: {message}")
        {
        }

        public ServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}