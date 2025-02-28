using SGHT.Domain.Entities;
using SGHT.Domain.Entities.Configuration;
using SGHT.Domain.Repository;

namespace SGHT.Persistance.Interfaces
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        Task<dynamic> SaveEntityAsync(Cliente cliente);
    }
}
