using Microsoft.Extensions.DependencyInjection;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.IOC.Dependancies
{
    public static class UsuariosDependancy
    {
        public static void AddUsuariosDependancy(this IServiceCollection service)
        {
            service.AddScoped<IUsuariosRepository, UsuariosRepository>();
            service.AddTransient<IUsuariosRepository, UsuariosRepository>();
        }
    }
}
