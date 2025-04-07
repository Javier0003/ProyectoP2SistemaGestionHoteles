using Microsoft.Extensions.DependencyInjection;
using SGHT.Http.Repositories.Interfaces;
using SGHT.Http.Repositories.Repositories;

namespace SGHT.IOC.Dependancies
{
    public static class HttpPisoDependancy
    {
        public static void AddHttpPisoDependancy(this IServiceCollection service)
        {
            service.AddTransient<IPisoHttpRepository, PisoHttpRepository>();
        }
    }
}
