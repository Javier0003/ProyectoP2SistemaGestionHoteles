using System.Net.Http.Json;

namespace SGHT.Web.Api.Controllers.Base
{
    public class HttpClientRepository : IHttpClientRepository
    {
        private readonly HttpClient _httpClient;

        public HttpClientRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _httpClient.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string url, T body) where T : class
        {
            return await _httpClient.PostAsJsonAsync(url, body);
        }

        public async Task<HttpResponseMessage> PatchAsJsonAsync<T>(string url, T body) where T : class
        {
            return await _httpClient.PatchAsJsonAsync(url, body);
        }

        /**
        * <summary>
        * HttpClient no permite enviar body con el metodo Delete
        * </summary> 
        */
        public async Task<HttpResponseMessage> DeleteAsync<T>(string url, T id) where T : class
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = JsonContent.Create(id)
            };
            return await _httpClient.SendAsync(request);
        }
    }
} 