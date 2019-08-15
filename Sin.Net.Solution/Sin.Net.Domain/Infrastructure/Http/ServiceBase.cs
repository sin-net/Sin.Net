using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sin.Net.Domain.Infrastructure.Http
{
    public abstract class ServiceBase
    {
        protected HttpClient _client;
        protected HttpEndpoint _endpoint;
        protected StringContent _content;

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

        /// <summary>
        /// Sets the content for the request provided as json string with UTF8 and media type as `application/json`.
        /// </summary>
        /// <param name="jsonContent">The json string.</param>
        /// <returns>The calling instance.</returns>
        public virtual ServiceBase SetContent(string jsonContent)
        {
            return SetContent(jsonContent, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Sets the content for the request with individual encoding and media type.
        /// </summary>
        /// <param name="content">The user-specific string content.</param>
        /// <param name="encoding">The user-specific encoding.</param>
        /// <param name="mediatype">The user-specific media type.</param>
        /// <returns>The calling instance.</returns>
        public virtual ServiceBase SetContent(string content, Encoding encoding, string mediatype)
        {
            _content = new StringContent(content, Encoding.UTF8, "application/json");
            return this;
        }

        /// <summary>
        /// Adds a specific header information for the request. 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ServiceBase AddHeader(string name, string value)
        {
            _client.DefaultRequestHeaders.Add(name, value);
            return this;
        }

        public abstract Task<string> CallAsync();

        public abstract Task<T> CallAsync<T>(Func<string, T> cast) where T : new();

        // -- properties

        /// <summary>
        /// Gets the string content that was lastly set by the SetContent method.
        /// </summary>
        public StringContent Content => _content;

    }
}
