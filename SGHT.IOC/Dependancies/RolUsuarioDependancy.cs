using Microsoft.Extensions.DependencyInjection;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.IOC.Dependancies
{
    public static class RolUsuarioDependancy
    {
        public static void AddRolUsuarioDependancy(this IServiceCollection service)
        {
            service.AddScoped<IRolUsuarioRepository, RolUsuarioRepository>();
            service.AddTransient<IRolUsuarioRepository, RolUsuarioRepository>();
        }
    }
}
