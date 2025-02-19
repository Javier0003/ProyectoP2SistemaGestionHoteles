using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Domain.Repository;

namespace SGHT.Persistance.Interfaces.Configuration
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        // Inicializacion de entidades
        // Task<Cliente> Get(int id);
    }
}
