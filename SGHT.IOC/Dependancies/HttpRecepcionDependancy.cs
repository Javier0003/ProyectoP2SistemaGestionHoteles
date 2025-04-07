using Microsoft.Extensions.DependencyInjection;
using SGHT.Http.Repositories.Interfaces;
using SGHT.Http.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGHT.IOC.Dependancies
{
    public static class HttpRecepcionDependancy
    {
        public static void AddHttpRecepcionDependancy(this IServiceCollection services)
        {
            services.AddTransient<IRecepcionHttpRepository, RecepcionHttpRepository>();
        }
    }
}
