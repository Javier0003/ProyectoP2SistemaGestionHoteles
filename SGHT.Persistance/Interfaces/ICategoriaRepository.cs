
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Domain.Repositorio;

namespace SGHT.Persistance.Interfaces
{
    public interface ICategoriaRepository : IBaseRepositorio<Catetgoria>
    {
        Task<OperationResult> GetById(int categoriaId);
    }
}
