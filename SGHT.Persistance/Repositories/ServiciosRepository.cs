using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;

namespace SGHT.Persistance.Repositories
{
    public class ServiciosRepository : BaseRepository<Servicios>, IServiciosRepository
    {
        public ServiciosRepository(SGHTContext context) : base(context)
        {
        }
    }
}
