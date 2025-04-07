using SGHT.Domain.Base;
using SGHT.Http.Repositories.Base;
using SGHT.Http.Repositories.Interfaces;
using SGHT.Model.Model.Cliente;
using SGHT.Web.Api.Controllers.Base;

namespace SGHT.Http.Repositories.Repositories
{
    public class ClienteHttpRepository : BaseHttpRepository, IClienteHttpRepository
    { 
        public ClienteHttpRepository(IHttpClientRepository httpClientRepository, IErrorHandler errorHandler) : base(httpClientRepository, errorHandler) 
        {
                
        }

        
    }
}
