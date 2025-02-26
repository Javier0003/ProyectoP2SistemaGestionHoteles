using SGHT.Application.Base;
using SGHT.Application.Dtos.Tarifa;

namespace SGHT.Application.Interfaces
{
    public interface ITarifaService : IBaseService<SaveTarifaDto, UpdateTarifaDto, DeleteTarifaDto>
    {
    }
}
