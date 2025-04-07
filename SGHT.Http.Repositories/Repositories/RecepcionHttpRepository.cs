using SGHT.Http.Repositories.Base;
using SGHT.Http.Repositories.Interfaces;
using SGHT.Web.Api.Controllers.Base;

namespace SGHT.Http.Repositories.Repositories
{
    public class RecepcionHttpRepository : BaseHttpRepository, IRecepcionHttpRepository
    {
        public RecepcionHttpRepository(IHttpClientRepository httpClientRepository, IErrorHandler errorHandler) : base(httpClientRepository, errorHandler) { }
    }
}
