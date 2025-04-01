using Microsoft.Extensions.DependencyInjection;
using SGHT.Http.Repositories.Interfaces;
using SGHT.Http.Repositories.Repositories;
using SGHT.Web.Api.Controllers.Base;

namespace SGHT.IOC.Dependancies
{
    public static class UsuarioHttpDependancy
    {
        public static void AddUsuarioHttpDependancy(this IServiceCollection service)
        {
            service.AddTransient<IUsuariosHttpRepository, UsuarioHttpRepositroy>();
        }
    }
}
