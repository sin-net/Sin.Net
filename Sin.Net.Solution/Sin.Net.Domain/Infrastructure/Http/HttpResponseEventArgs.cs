using System;
using System.Net;

namespace Sin.Net.Domain.Infrastructure.Http
{
    /// <summary>
    /// This is the event handler for http responses.
    /// </summary>
    /// <param name="sender">The sending object.</param>
    /// <param name="e">The event arguments.</param>
    public delegate void HttpResponseEventHandler(object sender, HttpResponseEventArgs e);

    /// <summary>
    /// This class is used to pass properties through events, based on the HttpResponseEventHandler type.
    /// </summary>
    public class HttpResponseEventArgs : EventArgs
    {
        /// <summary>
        /// The default constructor creates the timestamp and needs the status code of the http response as argument.
        /// </summary>
        /// <param name="statusCode">The http status code.</param>
        public HttpResponseEventArgs(HttpStatusCode statusCode)
        {
            Timestamp = DateTime.Now;
            StatusCode = statusCode;
        }

        /// <summary>
        /// The constructor calls the default constructor and takes a response object as parameter.
        /// </summary>
        /// <param name="statusCode">The http status code.</param>
        /// <param name="response">The response object of the http call.</param>
        public HttpResponseEventArgs(HttpStatusCode statusCode, object response) : this(statusCode)
        { 
            Response = response;
        }

        /// <summary>
        /// Gets or sets the optional property to keep the information about the http endpoint.
        /// </summary>
        public HttpEndpoint Endpoint { get; set; }

        /// <summary>
        /// Gets the response object of the http call.
        /// </summary>
        public object Response { get; private set; }

        /// <summary>
        /// Gets the timestamp of the instanciation of the arguments. 
        /// </summary>
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// Gets the http status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }
    }
}
