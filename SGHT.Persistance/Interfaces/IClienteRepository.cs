using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Domain.Repository;

namespace SGHT.Persistance.Interfaces
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
       // Task<OperationResult> GetClienteByRecepcionID(int recepcionID);
    }
}
