using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Apollo.Infrastructure.Factories
{
    public class RestApiClientFactory
    {
        private static HttpClient _client;

        public static HttpClient GetClient(string baseUrl)
        {
            if (_client == null)
            {
                _client = new HttpClient
                {
                    BaseAddress = new Uri(baseUrl)
                };
                _client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
               
            return _client;
        }
    }
}
