using SGHT.Domain.Entities;
using SGHT.Domain.Entities.Configuration;
using SGHT.Domain.Repository;
using SGHT.Domain.Base;

namespace SGHT.Persistance.Interfaces
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        Task<OperationResult> GetClienteByID(int idCliente);
    }
}
