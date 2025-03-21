using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Entities.Users;
using SGHT.Persistance.Interfaces;

namespace SGHT.Persistance.Repositories
{
    public class UsuariosRepository : BaseRepository<Usuarios>, IUsuariosRepository
    {
        private readonly SGHTContext _context;
        private readonly ILogger<UsuariosRepository> _logger;
        private readonly IConfiguration _configuration;
        public UsuariosRepository(SGHTContext context, ILogger<UsuariosRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> LogIn(UserLogIn usuario)
        {
            if (usuario is null) return OperationResult.GetErrorResult("Body can't be null", code: 400);
            if (string.IsNullOrWhiteSpace(usuario.Email)) return OperationResult.GetErrorResult("Email is null", code: 400);
            if (string.IsNullOrWhiteSpace(usuario.Password)) return OperationResult.GetErrorResult("Password is null", code: 400);

            try
            {
                var query = await (from Usuarios in _context.Usuarios
                                   where Usuarios.Correo == usuario.Email && Usuarios.Estado != false
                                   select new Usuarios()
                                   {
                                       IdUsuario = Usuarios.IdUsuario,
                                       IdRolUsuario = Usuarios.IdRolUsuario,
                                       Estado = Usuarios.Estado,
                                       NombreCompleto = Usuarios.NombreCompleto,
                                       Correo = Usuarios.Correo,
                                       Clave = Usuarios.Clave,
                                   }).ToListAsync();

                if (query.Count == 0) return OperationResult.GetErrorResult("Usuario no encontrado", code: 404);
                return OperationResult.GetSuccesResult(query[0], code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in LogIn: {ex}");
                return OperationResult.GetErrorResult("No se pudo iniciar sesion", code: 500);
            }
        }

        public override async Task<OperationResult> SaveEntityAsync(Usuarios usuarios)
        {
            if (usuarios == null) return OperationResult.GetErrorResult("Body is null", code: 400);
            if (usuarios.Estado == null) return OperationResult.GetErrorResult("Estado can't be null", code: 400);
            if (string.IsNullOrWhiteSpace(usuarios.Correo)) return OperationResult.GetErrorResult("Correo can't be null or whitespace", code: 400);
            if (string.IsNullOrWhiteSpace(usuarios.Clave)) return OperationResult.GetErrorResult("Clave can't be null or whitespace", code: 400);
            if (string.IsNullOrWhiteSpace(usuarios.NombreCompleto)) return OperationResult.GetErrorResult("Nombre can't be null or whitespace", code: 400);

            if (_context.Usuarios.Any(cd => cd.Correo == usuarios.Correo)) return OperationResult.GetErrorResult("Este correo ya esta registrado en la base de datos", code: 400);
            return await base.SaveEntityAsync(usuarios);
        }

        public override async Task<OperationResult> DeleteEntityAsync(Usuarios entity)
        {
            if (entity is null) return OperationResult.GetErrorResult("Input can't be null");
            try
            {
                var result = await GetEntityByIdAsync(entity.IdUsuario);
                if (result is null) return OperationResult.GetErrorResult("usuario no encontrado", code: 404);

                result.Estado = false;

                var resultDelete = await UpdateEntityAsync(result);
                if (!resultDelete.Success) return OperationResult.GetErrorResult("No se elimino correctamente", code: 500);

                return OperationResult.GetSuccesResult(resultDelete, "usuairo eliminado correctamente", code: 200);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"UsuariosRepository.DeleteEntityAsync: {ex}");
                return OperationResult.GetErrorResult("Error eliminando usuario", code: 500);
            }
        }

        public override async Task<Usuarios> GetEntityByIdAsync(int id)
        {
            try
            {
                var result = await base.GetEntityByIdAsync(id);
                if (result.Estado == false) return null;

                return result;
            }
            catch (Exception ex) 
            {
                _logger.LogError($"UsuariosRepository.GetEntityByIdAsync: {ex}");
                return null;
            }
        }

        public override async Task<OperationResult> UpdateEntityAsync(Usuarios entity)
        {
            try
            {
                if(entity is null) return OperationResult.GetErrorResult("body can't be null", code: 400);
                if (string.IsNullOrWhiteSpace(entity.NombreCompleto)) return OperationResult.GetErrorResult("Name can't be null", code: 400);
                if (string.IsNullOrWhiteSpace(entity.Clave)) return OperationResult.GetErrorResult("Password can't be null", code: 400);
                if (string.IsNullOrWhiteSpace(entity.Correo)) return OperationResult.GetErrorResult("Email can't be null", code: 400);
                if (!int.IsPositive(entity.IdRolUsuario)) return OperationResult.GetErrorResult("IdRolUsuario can't be null", code: 400);

                var result = await base.UpdateEntityAsync(entity);
                return result.Success
                    ? OperationResult.GetSuccesResult("Actualización exitosa", code: 200)
                    : OperationResult.GetErrorResult("No se actualizo", code: 400);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UsuariosRepository.UpdateEntityAsync: {ex}");
                return OperationResult.GetErrorResult($"Error al actualizar: {ex.Message}");
            }
        }
    }
}