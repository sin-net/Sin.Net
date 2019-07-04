using System;
using System.Net;

namespace Sin.Net.Domain.Infrastructure.Http
{
    public delegate void HttpResponseEventHandler(object sender, HttpResponseEventArgs e);

    public class HttpResponseEventArgs : EventArgs
    {

        public HttpResponseEventArgs(HttpStatusCode statusCode)
        {
            Timestamp = DateTime.Now;
            StatusCode = statusCode;
        }

        public DateTime Timestamp { get; private set; }

        public HttpStatusCode StatusCode { get; private set; }
    }
}
