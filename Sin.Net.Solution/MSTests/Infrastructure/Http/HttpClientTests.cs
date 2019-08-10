using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sin.Net.Domain.Infrastructure.Http;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Infrastructure.Http;
using Sin.Net.Persistence.IO.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MSTests.Infrastructure.Http
{
    /// <summary>
    /// This class runs all tests for HTTP calls to external services like magna service or test services.
    /// </summary>
    [TestClass]
    public class HttpClientTests : TestsBase
    {
        // --fields

        private HttpEndpoint _endpoint;
        private ServiceBase _service;

        // -- constructor

        /// <summary>
        /// Default empty constructor
        /// </summary>
        public HttpClientTests()
        {
        }

        // -- initializer and cleanup



        /// <summary>
        /// Initialize method that runs before every test.
        /// </summary>
        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();
        }

        /// <summary>
        /// Cleanup method that runs after every test.
        /// </summary>
        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        // -- test methods

        /// <summary>
        /// Test method that runs a generic test against a public REST-API.
        /// </summary>
        /// <returns>The method awaits for async calls.</returns>
        [TestMethod]
        public async Task ServiceCallAsync()
        {
            // arrange
            _endpoint = new HttpEndpoint
            {
                BaseAddress = "https://api.abalin.net",
                Request = "/get/today?country=de",
                MethodName = "get"
            };
            _service = new GenericService(new HttpClient(), _endpoint);
            _service.CallResponded += (o, e) =>
            {
                Log.Info($"Http responded with code: {e.StatusCode}");
            };

            // act
            var result = await _service.CallAsync<object>((s) => JsonIO.FromJsonString<object>(s));

            // assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test method that shall throw an exception to prof that custom exceptions are working.
        /// </summary>
        /// <returns>The method awaits for async calls.</returns>
        [TestMethod]
        public async Task ThrowServiceException()
        {
            // arrange
            var exception = default(Exception);
            _endpoint = new HttpEndpoint
            {
                BaseAddress = "http://not-found.de/123",
                Request = "",
                MethodName = ""
            };

            // act
            try
            {
                _service = new GenericService(new HttpClient(), _endpoint);
                _service.CallResponded += (o, e) =>
                {
                    Log.Info($"Http responded with code: {e.StatusCode}");
                };
                var result = await _service.CallAsync<object>((s) => JsonIO.FromJsonString<object>(s));
            }
            catch (Exception ex)
            {
                Log.Fatal(ex);
                exception = ex;
            }

            // assert
            Assert.IsNotNull(exception);
        }
    }
}
