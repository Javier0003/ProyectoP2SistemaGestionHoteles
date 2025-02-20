using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Domain.Repository;

namespace SGHT.Persistance.Interfaces.Reservation
{
    public interface IRecepcionRepository : IBaseRepository<Recepcion>
    {
        Task<OperationResult> GetRecepcionByClienteID(int idCliente);
    }
}
