using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces.Security;

namespace SGHT.Persistance.Repositories.Security
{
    public class UsuariosRepository : BaseRepository<Usuarios>, IUsuariosRepository
    {
        public UsuariosRepository(SGHTContext context) : base(context)
        {
        }
    }
}
