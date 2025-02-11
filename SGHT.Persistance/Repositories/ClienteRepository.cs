using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;

namespace SGHT.Persistance.Repositories
{
    internal class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(SGHTContext context) : base(context)
        {
        }
    }
}