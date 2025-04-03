using SGHT.Http.Repositories.Base;
using SGHT.Http.Repositories.Interfaces;
using SGHT.Web.Api.Controllers.Base;

namespace SGHT.Http.Repositories.Repositories
{
    public class EstadoHabitacionHttpRepository : BaseHttpRepository, IEstadoHabitacionHttpRepository
    {
        public EstadoHabitacionHttpRepository(IHttpClientRepository httpClientRepository, IErrorHandler errorHandler) : base(httpClientRepository, errorHandler) { }
    }
}
