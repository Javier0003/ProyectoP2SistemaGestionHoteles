using Microsoft.AspNetCore.Mvc;

namespace SGHT.Web.Api.Controllers.Base
{
    public class BaseController : Controller
    {
        protected readonly HttpClient _httpClient;

        public BaseController(IConfiguration configuration) 
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(configuration["baseUrl"]);
        }


        protected async Task<T?> Get<T>(string url) where T : class
        {
            try
            {
                var res = await _httpClient.GetFromJsonAsync<T>(url);
                return res;
            }
            catch (Exception ex) 
            {
                return null;
            }
        }
    }
}


