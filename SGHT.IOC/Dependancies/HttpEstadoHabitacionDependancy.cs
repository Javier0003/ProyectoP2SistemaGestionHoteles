using Microsoft.Extensions.DependencyInjection;
using SGHT.Http.Repositories.Interfaces;
using SGHT.Http.Repositories.Repositories;

namespace SGHT.IOC.Dependancies
{
    public static class HttpEstadoHabitacionDependancy
    {
        public static void AddHttpEstadoHabitacionDependancy(this IServiceCollection service)
        {
            service.AddTransient<IEstadoHabitacionHttpRepository, EstadoHabitacionHttpRepository>();
        }
    }
}
