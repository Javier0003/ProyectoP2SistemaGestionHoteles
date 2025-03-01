using Microsoft.Extensions.DependencyInjection;
using SGHT.Application.Interfaces;
using SGHT.Application.Services;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.IOC.Dependancies
{
    public static class UsuariosDependancy
    {
        public static void AddUsuariosDependancy(this IServiceCollection service)
        {
            service.AddScoped<IUsuariosRepository, UsuariosRepository>();
            service.AddTransient<IUsuarioService, UsuarioService>();
        }
    }
}
