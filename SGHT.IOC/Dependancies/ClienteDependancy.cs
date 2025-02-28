using Microsoft.Extensions.DependencyInjection;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.IOC.Dependancies
{
    public static class ClienteDependancy
    {
        public static void AddCLienteDependancy(this IServiceCollection service)
        {
            service.AddScoped<IClienteRepository, ClienteRepository>();
            service.AddTransient<IClienteRepository, ClienteRepository>();
        }
    }
}
