using SGHT.Application.Base;
using SGHT.Application.Dtos.RolUsuario;

namespace SGHT.Application.Interfaces
{
    public interface IRolUsuarioService : IBaseService<SaveRolUsuarioDto, UpdateRolUsuarioDto, DeleteRolUsuarioDto>
    {
    }
}
