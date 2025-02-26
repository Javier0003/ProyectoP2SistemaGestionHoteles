using SGHT.Application.Base;
using SGHT.Application.Dtos.Usuarios;

namespace SGHT.Application.Interfaces
{
    internal interface IUsuarioService : IBaseService<SaveUsuarioDto, UpdateUsuarioDto, DeleteUsuarioDto>
    {
    }
}
