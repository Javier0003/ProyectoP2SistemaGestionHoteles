using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Domain.Repository;

namespace SGHT.Persistance.Interfaces
{
    public interface IRecepcionRepository : IBaseRepository<Recepcion>
    {
        Task<OperationResult> GetRecepcionByClienteID(int IDCliente);
    }
}
