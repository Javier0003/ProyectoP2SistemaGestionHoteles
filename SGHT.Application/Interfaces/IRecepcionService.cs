using SGHT.Application.Base;
using SGHT.Application.Dtos.RecepcionDto;

namespace SGHT.Application.Interfaces
{
    public interface IRecepcionService : IBaseService<SaveRecepcionDto, UpdateRecepcionDto, DeleteRecepcionDto>
    {
    }
}
