using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGHT.Application.Base;
using SGHT.Application.Dtos.Habitacion;

namespace SGHT.Application.Interfaces
{
    public interface IHabitacionService : IBaseService<SaveHabitacionDto, UpdateHabitacionDto, DeleteHabitacionDto>
    {

    }

}
