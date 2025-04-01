using SGHT.Http.Repositories.Base;
using SGHT.Http.Repositories.Interfaces;
using SGHT.Web.Api.Controllers.Base;

namespace SGHT.Http.Repositories.Repositories
{
    public class TarifaHttpRepository : BaseHttpRepository, ITarifaHttpRepository
    {
        public TarifaHttpRepository(IHttpClientRepository httpClientRepository, IErrorHandler errorHandler) : base(httpClientRepository, errorHandler) { }
    }
}
