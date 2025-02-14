using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces.Configuration;

namespace SGHT.Persistance.Repositories.Configuration
{
    public class PisoRepository : BaseRepository<Piso>, IPisoRepository
    {
        public PisoRepository(SGHTContext context) : base(context)
        {
        }
    }
}
