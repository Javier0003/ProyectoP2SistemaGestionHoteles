using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Domain.Repository;

namespace SGHT.Persistance.Interfaces
{
    public interface IEstadoHabitacionRepository : IBaseRepository<EstadoHabitacion>
    {
           Task<OperationResult> GetEstadoByIdAsync(int id);
            Task<OperationResult> SaveEstadoAsync(EstadoHabitacion estadoHabitacion);
        
    }
}

