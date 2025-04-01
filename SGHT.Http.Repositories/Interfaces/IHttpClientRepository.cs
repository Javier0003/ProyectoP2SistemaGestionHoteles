using System.Net.Http.Json;

namespace SGHT.Web.Api.Controllers.Base
{
    public interface IHttpClientRepository
    {
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> PostAsJsonAsync<T>(string url, T body) where T : class;
        Task<HttpResponseMessage> PatchAsJsonAsync<T>(string url, T body) where T : class;
        Task<HttpResponseMessage> DeleteAsync<T>(string url, T id) where T : class;
    }
} 