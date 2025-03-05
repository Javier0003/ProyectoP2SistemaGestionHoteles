using Microsoft.Extensions.DependencyInjection;
using SGHT.Persistance.Entities.Users;

namespace SGHT.IOC.Dependancies
{
    public static class JwtAuthenticationDependancy
    {
        public static void AddJwtAuthenticationDependancy(this IServiceCollection service)
        {
            service.AddSingleton<TokenProvider>();
        }
    }
}

