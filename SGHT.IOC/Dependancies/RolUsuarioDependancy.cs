using Microsoft.Extensions.DependencyInjection;
using SGHT.Application.Interfaces;
using SGHT.Application.Services;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.IOC.Dependancies
{
    public static class RolUsuarioDependancy
    {
        public static void AddRolUsuarioDependancy(this IServiceCollection service)
        {
            service.AddScoped<IRolUsuarioRepository, RolUsuarioRepository>();
            service.AddTransient<IRolUsuarioService, RolUsuarioService>();
        }
    }
}
