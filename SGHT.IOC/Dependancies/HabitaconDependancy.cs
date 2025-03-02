using Microsoft.Extensions.DependencyInjection;
using SGHT.Application.Interfaces;
using SGHT.Application.Services;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.IOC.Dependancies
{
    public static class HabitacionDependancy
    {
        public static void AddHabitacionDependancy(this IServiceCollection service)
        {
            service.AddScoped<IHabitacionRepository, HabitacionRepository>();
            service.AddTransient<IHabitacionService, HabitacionService>();
        }
    }
}
