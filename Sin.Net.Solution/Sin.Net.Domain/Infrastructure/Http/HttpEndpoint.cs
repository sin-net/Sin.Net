using System;
using System.Net.Http;

namespace Sin.Net.Domain.Infrastructure.Http
{
    /// <summary>
    /// Simple Data model class to represent a http service.
    /// </summary>
    public class HttpEndpoint
    {
        private const string GET = "get";
        private const string POST = "post";
        private const string PUT = "put";
        private const string DELETE = "delete";

        /// <summary>
        /// The default constructor sets the method type to a GET-Method
        /// </summary>
        public HttpEndpoint()
        {
            MethodName = GET;
        }

        /// <summary>
        /// Returns the HttpMethod class based on the property MethodName
        /// </summary>
        /// <returns>It`s wheater Get, Post, Put or Delete</returns>
        public HttpMethod Method()
        {
            switch (MethodName.ToLower())
            {
                case GET:
                    return HttpMethod.Get;
                case POST:
                    return HttpMethod.Post;
                case PUT:
                    return HttpMethod.Put;
                case DELETE:
                    return HttpMethod.Delete;
                default:
                    return HttpMethod.Get;
            }
        }

        // -- properties

        /// <summary>
        /// Gets or sets the base address of the REST service
        /// </summary>
        public string BaseAddress { get; set; }

        /// <summary>
        /// Gets or sets the request query
        /// </summary>
        public string Request { get; set; }

        /// <summary>
        /// Gets or sets the name of the method. Allowed names are Get, Post, Put or Delete.
        /// Case sensitivity is ignored, because the property it will be used in lower case.
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Gets the complete Url out of base address and request
        /// </summary>
        public string Url
        {
            get
			{
                Uri uri;
                if (string.IsNullOrEmpty(Request))
                {
                    uri = new Uri((!string.IsNullOrEmpty(BaseAddress) ? BaseAddress : "no-base-address"));
                }
                else
                {
                   uri = new Uri(
                    new Uri((!string.IsNullOrEmpty(BaseAddress) ? BaseAddress : "no-base-address")), Request);
                }
                return uri?.OriginalString ?? "no-uri";
            }
        }

        /// <summary>
        /// Gets or sets an optional authentication string or API key for requests.
        /// </summary>
        public string Authentication { get; set; }

    }
}
