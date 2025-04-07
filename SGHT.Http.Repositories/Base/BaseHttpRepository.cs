using SGHT.Http.Repositories.Interfaces;
using SGHT.Web.Api.Controllers.Base;

namespace SGHT.Http.Repositories.Base
{
    public class BaseHttpRepository : IBaseHttpRepository
    {
        private readonly IHttpClientRepository _httpClientService;
        private readonly IErrorHandler _errorHandler;
        public BaseHttpRepository(
            IHttpClientRepository httpClientService,
            IErrorHandler errorHandler)
        {
            _httpClientService = httpClientService;
            _errorHandler = errorHandler;
        }

        public virtual async Task<HttpResponseMessage> SendGetRequestAsync(string url)
        {
            var result = await _httpClientService.GetAsync(url);
            if (result.IsSuccessStatusCode) return result;

            throw await _errorHandler.HandleErrorAsync(result, "Get", url);
        }

        public virtual async Task<HttpResponseMessage> SendPostRequestAsync<T>(string url, T body) where T : class
        {
            var result = await _httpClientService.PostAsJsonAsync(url, body);
            if (result.IsSuccessStatusCode) return result;

            throw await _errorHandler.HandleErrorAsync(result, "Post", url);
        }

        public virtual async Task<HttpResponseMessage> SendPatchRequestAsync<T>(string url, T body) where T : class
        {
            var result = await _httpClientService.PatchAsJsonAsync(url, body);
            if (result.IsSuccessStatusCode) return result;

            throw await _errorHandler.HandleErrorAsync(result, "Patch", url);
        }

        public virtual async Task<HttpResponseMessage> SendDeleteRequestAsync<T>(string url, T id) where T : class
        {
            var result = await _httpClientService.DeleteAsync(url, id);
            if (result.IsSuccessStatusCode) return result;

            throw await _errorHandler.HandleErrorAsync(result, "Delete", url);
        }

        
    }
}
