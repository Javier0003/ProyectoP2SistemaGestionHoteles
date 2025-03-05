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
            if (string.IsNullOrWhiteSpace(usuario.Email)) return OperationResult.GetErrorResult("Email is null", code: 400);
            if (string.IsNullOrWhiteSpace(usuario.Password)) return OperationResult.GetErrorResult("Password is null", code: 400);

            try
            {
                var query = await (from Usuarios in _context.Usuarios
                                   where Usuarios.Correo == usuario.Email
                                   select new Usuarios()
                                   {
                                       IdUsuario = Usuarios.IdUsuario,
                                       IdRolUsuario = Usuarios.IdRolUsuario,
                                       Estado = Usuarios.Estado,
                                       NombreCompleto = Usuarios.NombreCompleto,
                                       Correo = Usuarios.Correo,
                                       Clave = Usuarios.Clave,
                                   }).ToListAsync();
                return OperationResult.GetSuccesResult(query[0], code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in LogIn: {ex}");
                return OperationResult.GetErrorResult("No se pudo iniciar sesion", code: 500);
            }
        }

        public override Task<OperationResult> SaveEntityAsync(Usuarios usuarios)
        {
            if (usuarios == null) return OperationResult.GetErrorResultAsync("Body is null", code: 400);
            if (usuarios.Estado == null) return OperationResult.GetErrorResultAsync("Estado can't be null", code: 400);
            if (string.IsNullOrWhiteSpace(usuarios.Correo)) return OperationResult.GetErrorResultAsync("Correo can't be null or whitespace", code: 400);
            if (string.IsNullOrWhiteSpace(usuarios.Clave)) return OperationResult.GetErrorResultAsync("Clave can't be null or whitespace", code: 400);
            if (string.IsNullOrWhiteSpace(usuarios.NombreCompleto)) return OperationResult.GetErrorResultAsync("Nombre can't be null or whitespace", code: 400);

            if (_context.Usuarios.Any(cd => cd.Correo == usuarios.Correo)) return OperationResult.GetErrorResultAsync("Este correo ya esta registrado en la base de datos", code: 400);
            return base.SaveEntityAsync(usuarios);
        }
    }
}