using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces.Reservation;

namespace SGHT.Persistance.Repositories.Reservation
{
    public class RecepcionRepository : BaseRepository<Recepcion>, IRecepcionRepository
    {
        public RecepcionRepository(SGHTContext context) : base(context)
        {
        }
    }
}
