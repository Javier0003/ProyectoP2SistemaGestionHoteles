using Microsoft.Extensions.DependencyInjection;
using SGHT.Http.Repositories.Base;
using SGHT.Web.Api.Controllers.Base;

namespace SGHT.IOC.Dependancies
{
    public static class HttpErrorHandler
    {
        public static void AddHttpErrorHandlerDependancy(this IServiceCollection service)
        {
            service.AddScoped<IErrorHandler, ErrorHandler>();
        }
    }
}
