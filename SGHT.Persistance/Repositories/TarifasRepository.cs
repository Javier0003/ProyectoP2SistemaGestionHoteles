using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;

namespace SGHT.Persistance.Repositories
{
    public class TarifasRepository : BaseRepository<Tarifas>, ITarifasRepository
    {
        public TarifasRepository(SGHTContext context) : base(context)
        {
        }
    }
}
