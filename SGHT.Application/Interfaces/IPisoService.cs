using SGHT.Application.Base;
using SGHT.Application.Dtos.Piso;

namespace SGHT.Application.Interfaces
{
    public interface IPisoService : IBaseService<SavePisoDto, UpdatePisoDto, DeletePisoDto>
    {
    }
}
