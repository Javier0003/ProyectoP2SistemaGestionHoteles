using Microsoft.Extensions.DependencyInjection;
using SGHT.Http.Repositories.Interfaces;
using SGHT.Http.Repositories.Repositories;

namespace SGHT.IOC.Dependancies
{
    public static class HttpRolUsuarioDependancy
    {
        public static void AddHttpRolUsuarioDependancy(this IServiceCollection service)
        {
            service.AddTransient<IRolUsuarioHttpRepository, RolUsuarioHttpRepository>();
        }
    }
}
