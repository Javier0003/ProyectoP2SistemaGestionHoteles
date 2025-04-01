using System.Net;

namespace SGHT.Web.Api.Controllers.Base
{
    public interface IErrorHandler
    {
        Task<Exception> HandleErrorAsync(HttpResponseMessage result, string method, string url);
    }
} 