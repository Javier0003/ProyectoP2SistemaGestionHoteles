using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces.Reservation;

namespace SGHT.Persistance.Repositories.Reservation
{
    public class TarifasRepository : BaseRepository<Tarifas>, ITarifasRepository
    {
        public TarifasRepository(SGHTContext context) : base(context)
        {
        }
    }
}
