using Microsoft.Extensions.DependencyInjection;
using SGHT.Http.Repositories.Interfaces;
using SGHT.Http.Repositories.Repositories;

namespace SGHT.IOC.Dependancies
{
    public static class HttpClienteDependancy
    {
        public static void AddHttpClienteDependancy(this IServiceCollection services)
        {
            services.AddTransient<IClienteHttpRepository, ClienteHttpRepository>();
        }
    }
}
