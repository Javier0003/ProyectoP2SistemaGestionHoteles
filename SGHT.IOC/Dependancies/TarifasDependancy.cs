using Microsoft.Extensions.DependencyInjection;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.IOC.Dependancies
{
    public static class TarifasDependancy
    {
        public static void AddTarifasDependancy(this IServiceCollection service)
        {
            service.AddScoped<ITarifasRepository, TarifasRepository>();
            service.AddTransient<ITarifasRepository, TarifasRepository>();
        }
    }
}
