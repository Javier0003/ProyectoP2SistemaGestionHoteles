using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Domain.Repository;
using SGHT.Persistance.Entities.Users;

namespace SGHT.Persistance.Interfaces
{
    public interface IUsuariosRepository : IBaseRepository<Usuarios>
    {
        Task<OperationResult> LogIn(UserLogIn usuario);
    }
}
