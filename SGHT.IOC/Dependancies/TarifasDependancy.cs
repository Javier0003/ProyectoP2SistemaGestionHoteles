using Microsoft.Extensions.DependencyInjection;
using SGHT.Application.Interfaces;
using SGHT.Application.Services;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.IOC.Dependancies
{
    public static class TarifasDependancy
    {
        public static void AddTarifasDependancy(this IServiceCollection service)
        {
            service.AddScoped<ITarifasRepository, TarifasRepository>();
            service.AddTransient<ITarifaService, TarifaService>();
        }
    }
}
