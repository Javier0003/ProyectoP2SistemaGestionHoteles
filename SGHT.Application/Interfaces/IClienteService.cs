using SGHT.Application.Base;
using SGHT.Application.Dtos.ClienteDto;


namespace SGHT.Application.Interfaces
{
    public interface IClienteService : IBaseService<SaveClienteDto, UpdateClienteDto, DeleteClienteDto>
    {

    }
}
