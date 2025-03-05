using SGHT.Application.Base;
using SGHT.Application.Dtos.Usuarios;
using SGHT.Domain.Base;
using SGHT.Persistance.Entities.Users;

namespace SGHT.Application.Interfaces
{
    public interface IUsuarioService : IBaseService<SaveUsuarioDto, UpdateUsuarioDto, DeleteUsuarioDto>
    {
        Task<OperationResult> LogIn(UserLogIn usuario); 
    }
}
