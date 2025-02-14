using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces.Security;

namespace SGHT.Persistance.Repositories.Security
{
    public class RolUsuarioRepository : BaseRepository<RolUsuario>, IRolUsuarioRepository
    {
        public RolUsuarioRepository(SGHTContext context) : base(context)
        {
        }
    }
}
