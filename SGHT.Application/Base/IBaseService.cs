using SGHT.Domain.Base;

namespace SGHT.Application.Base
{
    internal interface IBaseService<TDtoSave, TDtoUpdate, TDtoDelete>
    {
        Task<OperationResult> GetAll();
        Task<OperationResult> GetById(int id);
        Task<OperationResult> UpdateById(TDtoUpdate dto);
        Task<OperationResult> DeleteById(TDtoDelete dto);
        Task<OperationResult> Save(TDtoSave dto);
    }
}
