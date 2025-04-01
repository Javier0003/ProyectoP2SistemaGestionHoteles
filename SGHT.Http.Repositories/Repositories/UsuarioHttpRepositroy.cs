using SGHT.Http.Repositories.Base;
using SGHT.Http.Repositories.Interfaces;
using SGHT.Web.Api.Controllers.Base;

namespace SGHT.Http.Repositories.Repositories
{
    public class UsuarioHttpRepositroy : BaseHttpRepository, IUsuariosHttpRepository
    {
        public UsuarioHttpRepositroy(IHttpClientRepository httpClientRepository, IErrorHandler errorHandler) : base(httpClientRepository, errorHandler) { }
    }
}
