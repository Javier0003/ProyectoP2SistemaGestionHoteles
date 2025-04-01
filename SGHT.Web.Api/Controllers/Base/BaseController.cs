using Microsoft.AspNetCore.Mvc;

namespace SGHT.Web.Api.Controllers.Base
{
    public class BaseController : Controller
    {
        protected readonly IConfiguration _configuration;
        private readonly IHttpClientService _httpClientService;
        private readonly IErrorHandler _errorHandler;
        private readonly ILogger<BaseController> _logger;

        public BaseController(
            IConfiguration configuration,
            ILogger<BaseController> logger,
            IHttpClientService httpClientService,
            IErrorHandler errorHandler)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClientService = httpClientService;
            _errorHandler = errorHandler;
        }

        protected async Task<HttpResponseMessage> SendGetRequestAsync(string url)
        {
            var result = await _httpClientService.GetAsync(url);
            if (result.IsSuccessStatusCode) return result;

            throw await _errorHandler.HandleErrorAsync(result, "Get", url);
        }

        protected async Task<HttpResponseMessage> SendPostRequestAsync<T>(string url, T body) where T : class
        {
            var result = await _httpClientService.PostAsJsonAsync(url, body);
            if (result.IsSuccessStatusCode) return result;

            throw await _errorHandler.HandleErrorAsync(result, "Post", url);
        }

        protected async Task<HttpResponseMessage> SendPatchRequestAsync<T>(string url, T body) where T : class
        {
            var result = await _httpClientService.PatchAsJsonAsync(url, body);
            if (result.IsSuccessStatusCode) return result;

            throw await _errorHandler.HandleErrorAsync(result, "Patch", url);
        }

        /**
         * <summary>
         * HttpClient no permite enviar body con el metodo Delete
         * </summary> 
        */
        protected async Task<HttpResponseMessage> SendDeleteRequestAsync<T>(string url, T id) where T : class
        {
            var result = await _httpClientService.DeleteAsync(url, id);
            if (result.IsSuccessStatusCode) return result;

            throw await _errorHandler.HandleErrorAsync(result, "Delete", url);
        }

        protected IActionResult ViewBagError(string error)
        {
            ViewBag.Message = error;
            return View();
        }
    }
}