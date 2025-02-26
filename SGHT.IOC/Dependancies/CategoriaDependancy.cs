using Microsoft.Extensions.DependencyInjection;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;
using SGHT.Application.Services;
using SGHT.Application.Interfaces;

namespace SGHT.IOC.Dependancies
{
    public static class CategoriaDependancy
    {
        public static void AddCategoriaDependancy(this IServiceCollection service)
        {
            service.AddScoped<ICategoriaRepository, CategoriaRepository>();
            service.AddTransient<ICategoriaRepository, CategoriaRepository>();
        }
    }
}
