using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sin.Net.Domain.Infrastructure.Http
{
    public abstract class ServiceBase
    {
        protected HttpClient _client;
        protected HttpEndpoint _endpoint;

        /// <summary>
        /// This event shall be fired when the request call returns with a response.
        /// </summary>
        public abstract event HttpResponseEventHandler CallResponded;

        // --fields

        /// <summary>
        /// Default (empty) constructor
        /// </summary>
        public ServiceBase()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="endpoint"></param>
        public ServiceBase(HttpClient client, HttpEndpoint endpoint) : this()
        {
            Setup(client, endpoint);
        }

        // -- methods


        public ServiceBase Setup(HttpClient client, HttpEndpoint endpoint)
        {
            try
            {
                _client = client;
                _endpoint = endpoint;
                _client.BaseAddress = new Uri(_endpoint.BaseAddress);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"ServiceBase constructor failed: {ex.Message}", ex);
            }

            return this;
        }

        public ServiceBase AddHeader(string name, string value)
        {
            _client.DefaultRequestHeaders.Add(name, value);
            return this;
        }

        public abstract Task<string> CallAsync();

        public abstract Task<T> CallAsync<T>(Func<string, T> cast) where T : new();

        // -- properties


    }
}
