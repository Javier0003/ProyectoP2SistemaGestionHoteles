using Microsoft.Extensions.DependencyInjection;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.IOC.Dependancies
{
    public static class PisoDependancy
    {
        public static void AddPisoDependancy(this IServiceCollection service)
        {
            service.AddScoped<IPisoRepository, PisoRepository>();
            service.AddTransient<IPisoRepository, PisoRepository>();
        }
    }
}
