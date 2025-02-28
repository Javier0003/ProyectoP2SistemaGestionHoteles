using Microsoft.Extensions.DependencyInjection;
using SGHT.Application.Interfaces;
using SGHT.Application.Services;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.IOC.Dependancies
{
    public static class RecepcionDependancy
    {
        public static void AddRecepcionDependancy(this IServiceCollection service)
        {
            service.AddScoped<IRecepcionRepository, RecepcionRepository>();
            service.AddTransient<IRecepcionService, RecepcionService>();
        }
    }
}
