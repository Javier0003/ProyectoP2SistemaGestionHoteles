using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces.Configuration;

namespace SGHT.Persistance.Repositories.Configuration
{
    class EstadoHabitacionRepository : BaseRepository<EstadoHabitacion>, IEstadoHabitacionRepository
    {
        public EstadoHabitacionRepository(SGHTContext context) : base(context)
        {
        }
    }
}
