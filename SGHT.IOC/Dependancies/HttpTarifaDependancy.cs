using Microsoft.Extensions.DependencyInjection;
using SGHT.Http.Repositories.Interfaces;
using SGHT.Http.Repositories.Repositories;

namespace SGHT.IOC.Dependancies
{
    public static class HttpTarifaDependancy
    {
        public static void AddHttpTarifaDependancy(this IServiceCollection service)
        {
            service.AddTransient<ITarifaHttpRepository, TarifaHttpRepository>();
        }
    }
}
