using SGHT.Application.Base;
using SGHT.Application.Dtos.RecepcionDto;
using SGHT.Domain.Base;

namespace SGHT.Application.Interfaces
{
    public interface IRecepcionService : IBaseService<SaveRecepcionDto, UpdateRecepcionDto, DeleteRecepcionDto>
    {
    }
}
