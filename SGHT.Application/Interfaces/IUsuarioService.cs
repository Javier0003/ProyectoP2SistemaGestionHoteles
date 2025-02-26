using SGHT.Application.Base;
using SGHT.Application.Dtos.Usuarios;

namespace SGHT.Application.Interfaces
{
    public interface IUsuarioService : IBaseService<SaveUsuarioDto, UpdateUsuarioDto, DeleteUsuarioDto>
    {
    }
}
