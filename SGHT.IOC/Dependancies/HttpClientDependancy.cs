using Microsoft.Extensions.DependencyInjection;
using SGHT.Web.Api.Controllers.Base;

namespace SGHT.IOC.Dependancies
{
    public static class HttpClientDependancy
    {
        public static void AddHttpClientDependancy(this IServiceCollection service, string baseUrl)
        {
            service.AddHttpClient<IHttpClientRepository, HttpClientRepository>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });
        }
    }
}
