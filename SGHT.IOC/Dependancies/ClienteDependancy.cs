using Microsoft.Extensions.DependencyInjection;
using SGHT.Application.Interfaces;
using SGHT.Application.Services;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.IOC.Dependancies
{
    public static class ClienteDependancy
    {
        public static void AddCLienteDependancy(this IServiceCollection service)
        {
            service.AddScoped<IClienteRepository, ClienteRepository>();
            service.AddTransient<IClienteService, ClienteService>();
        }
    }
}
