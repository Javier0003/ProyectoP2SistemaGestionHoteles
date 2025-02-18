using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Domain.Repositorio;

namespace SGHT.Persistance.Interfaces.Configuration
{
    public interface IClienteRepository : IBaseRepositorio<Cliente>
    {
        // Inicializacion de entidades
        Task<Cliente> Get(int id);
    }
}
