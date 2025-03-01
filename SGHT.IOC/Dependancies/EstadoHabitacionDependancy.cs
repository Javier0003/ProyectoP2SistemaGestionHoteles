using Microsoft.Extensions.DependencyInjection;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;
using SGHT.Application.Services;
using SGHT.Application.Interfaces;

namespace SGHT.IOC.Dependancies
{
    public static class EstadoHabitacionDependancy
    {
        public static void AddEstadoHabitacionDependancy(this IServiceCollection service)
        {
            service.AddScoped<IEstadoHabitacionRepository, EstadoHabitacionRepository>();
            service.AddTransient<IEstadoHabitacionService, EstadoHabitacionService>();
        }
    }
}
