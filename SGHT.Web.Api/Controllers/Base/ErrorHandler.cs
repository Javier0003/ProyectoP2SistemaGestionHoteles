using System.Net;

namespace SGHT.Web.Api.Controllers.Base
{
    public class ErrorHandler : IErrorHandler
    {
        private readonly ILogger<ErrorHandler> _logger;

        public ErrorHandler(ILogger<ErrorHandler> logger)
        {
            _logger = logger;
        }

        public async Task<Exception> HandleErrorAsync(HttpResponseMessage result, string method, string url)
        {
            _logger.LogError($"Error in {method}: executing route {url}, Status code: {result.StatusCode}");

            switch (result.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new KeyNotFoundException($"Resource not found at {url}");
                case HttpStatusCode.BadRequest:
                    return new BadHttpRequestException($"Incorrect form of body at {url}");
                default:
                    return new HttpRequestException($"Error in {method}: {await result.Content.ReadAsStringAsync()}");
            }
        }
    }
} 