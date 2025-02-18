using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces.Configuration;

namespace SGHT.Persistance.Repositories
{
    public class CategoriaRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public CategoriaRepository(SGHTContext context) : base(context)
        { }
    }
}
