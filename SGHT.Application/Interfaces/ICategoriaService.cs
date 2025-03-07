﻿using SGHT.Application.Base;
using SGHT.Application.Dtos.Categoria;
using SGHT.Domain.Entities;

namespace SGHT.Application.Interfaces
{
    public interface ICategoriaService : IBaseService<SaveCategoriaDto, UpdateCategoriaDto, DeleteCategoriaDto>
    {
    }
}
