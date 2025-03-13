using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SGHT.Application.Mappings;

namespace SGHT.IOC.Dependancies
{
    public static class AutomapperDependancy
    {
        public static void AddAutomapperDependancy(this IServiceCollection service)
        {
            service.AddAutoMapper((Assembly)Assembly.GetAssembly(typeof(AutoMapperProfile)));
        }
    }
}
