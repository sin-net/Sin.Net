using Sin.Net.Domain.Infrastructure.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sin.Net.Infrastructure.Http
{
    public class GenericService : ServiceBase
    {
        public sealed override event HttpResponseEventHandler CallResponded;

        // -- constructors

        public GenericService() : base()
        {

        }

        public GenericService(HttpClient client, HttpEndpoint endpoint) : base(client, endpoint)
        {
        }

        // -- methods

        public sealed override async Task<string> CallAsync()
        {
            var request = new HttpRequestMessage(_endpoint.Method(), _endpoint.Request);
            var response = await _client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            CallResponded?.Invoke(this, new HttpResponseEventArgs(response.StatusCode));
            return result;
        }

        public sealed override async Task<T> CallAsync<T>(Func<string, T> cast)
        {
            var result = default(T);
            try
            {
                string response = await CallAsync();
                result = cast(response);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
            return result;
        }
    }
}
