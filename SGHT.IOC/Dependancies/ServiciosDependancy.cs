using Microsoft.Extensions.DependencyInjection;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.IOC.Dependancies
{
    public static class ServiciosDependancy
    {
        public static void AddServiciosDependancy(this IServiceCollection service)
        {
            service.AddScoped<IServiciosRepository, ServiciosRepository>();
            service.AddTransient<IServiciosRepository, ServiciosRepository>();
        }
    }
}
