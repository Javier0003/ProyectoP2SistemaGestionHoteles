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
    }
}
