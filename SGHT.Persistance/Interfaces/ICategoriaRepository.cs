
using SGHT.Domain.Base;
using SGHT.Domain.Entities.Reservation;
using SGHT.Domain.Repository;


namespace SGHT.Persistance.Interfaces
{
    public interface ICategoriaRepository : IBaseRepository<Categoria>
    {
        Task<OperationResult> GetById(int categoriaId);
    }
}
