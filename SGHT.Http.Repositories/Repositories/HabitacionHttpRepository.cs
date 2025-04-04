using SGHT.Http.Repositories.Base;
using SGHT.Http.Repositories.Interfaces;
using SGHT.Web.Api.Controllers.Base;

namespace SGHT.Http.Repositories.Repositories
{
    public class HabitacionHttpRepository : BaseHttpRepository, IHabitacionHttpRepository
    {
        public HabitacionHttpRepository(IHttpClientRepository httpClientService, IErrorHandler errorHandler) : base(httpClientService, errorHandler)
        {
        }
    }
}
