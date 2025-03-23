using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Application.Dtos.Usuarios;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Entities.Users;
using SGHT.Persistance.Interfaces;
using SGHT.Application.Utils;

namespace SGHT.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly ILogger<UsuarioService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly TokenProvider _tokenProvider;

        public UsuarioService(
            IUsuariosRepository usuariosRepository, 
            ILogger<UsuarioService> logger, 
            IConfiguration configuration,
            IMapper mapper,
            TokenProvider tokenProvider)
        {
            _usuariosRepository = usuariosRepository;
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
            _tokenProvider = tokenProvider;
        }

        public async Task<OperationResult> GetAll()
        {
            try
            {
                var usuarios = await _usuariosRepository.GetAllAsync();
                var usuariosDto = _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
                return OperationResult.GetSuccesResult(usuariosDto, code: 200);
            }
            catch (Exception ex) {
                _logger.LogError($"UsuarioService.GetAll: {ex}");
                return OperationResult.GetErrorResult("Error getting users", code: 500);
            }
        }

        public async Task<OperationResult> GetById(int id)
        {
            if (id <= 0) return OperationResult.GetErrorResult("Invalid user ID", code: 400);
            try
            {
                var usuario = await _usuariosRepository.GetEntityByIdAsync(id);
                if (usuario == null) return OperationResult.GetErrorResult("User not found", code: 404);
                
                var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
                return OperationResult.GetSuccesResult(usuarioDto, code: 200);
            }
            catch (Exception ex) {
                _logger.LogError($"UsuarioService.GetById: {ex}");
                return OperationResult.GetErrorResult("Error finding user", 500);
            }
        }

        public async Task<OperationResult> Save(SaveUsuarioDto dto)
        {
            if (dto is null) return OperationResult.GetErrorResult("Body is null", code: 400);
            if (string.IsNullOrWhiteSpace(dto.Correo)) return OperationResult.GetErrorResult("Email is required", code: 400);
            if (string.IsNullOrWhiteSpace(dto.Clave)) return OperationResult.GetErrorResult("Password is required", code: 400);
            if (dto.Clave.Length < 6) return OperationResult.GetErrorResult("Password must be at least 6 characters long", code: 400);

            try
            {
                var existingUser = await _usuariosRepository.GetUserByEmail(dto.Correo);
                if (existingUser.Success) return OperationResult.GetErrorResult("Email already in use", code: 400);

                var usuario = _mapper.Map<Usuarios>(dto);
                usuario.Clave = Passwords.HashPassword(dto.Clave);
                usuario.FechaCreacion = DateTime.Now;

                var result = await _usuariosRepository.SaveEntityAsync(usuario);
                return OperationResult.GetSuccesResult(result, "User created successfully", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UsuarioService.Save: {ex}");
                return OperationResult.GetErrorResult("Couldn't save user", code: 500);
            }
        }

        public async Task<OperationResult> UpdateById(UpdateUsuarioDto dto)
        {
            if (dto is null) return OperationResult.GetErrorResult("Body is null", code: 400);
            if (dto.IdUsuario <= 0) return OperationResult.GetErrorResult("Invalid user ID", code: 400);
            if (string.IsNullOrWhiteSpace(dto.Correo)) return OperationResult.GetErrorResult("Email is required", code: 400);
            if (string.IsNullOrWhiteSpace(dto.Clave)) return OperationResult.GetErrorResult("Password is required", code: 400);
            if (dto.Clave.Length < 6) return OperationResult.GetErrorResult("Password must be at least 6 characters long", code: 400);

            try
            {
                var existingUser = await _usuariosRepository.GetEntityByIdAsync(dto.IdUsuario);
                if (existingUser == null) return OperationResult.GetErrorResult("User not found", code: 404);

                if (!dto.Clave.Contains('/')) dto.Clave = Passwords.HashPassword(dto.Clave);

                dto.FechaCreacion = (DateTime)existingUser.FechaCreacion!;

                var usuario = _mapper.Map(dto, existingUser);
                var queryResult = await _usuariosRepository.UpdateEntityAsync(usuario);

                return OperationResult.GetSuccesResult(queryResult, "Usuario Actualizado Correctamente", 200);
            }
            catch (Exception ex)
            {
            _logger.LogError($"UsuarioService.UpdateById: {ex}");
            return OperationResult.GetErrorResult("Error updating user", code: 500);
            }
        }

        public async Task<OperationResult> DeleteById(DeleteUsuarioDto dto)
        {
            if (dto is null) return OperationResult.GetErrorResult("Body is null", code: 400);
            if (dto.IdUsuario <= 0) return OperationResult.GetErrorResult("Invalid user ID", code: 400);

            try
            {
                var entityToRemove = await _usuariosRepository.GetEntityByIdAsync(dto.IdUsuario);
                if (entityToRemove == null) return OperationResult.GetErrorResult("User not found", code: 404);

                var result = await _usuariosRepository.DeleteEntityAsync(entityToRemove);
                return OperationResult.GetSuccesResult(result, "Usuario eliminado", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UsuarioService.DeleteById: {ex}");
                return OperationResult.GetErrorResult("Error removing user", code: 500);
            }
        }

        public async Task<OperationResult> LogIn(UserLogIn usuario)
        {
            if (usuario is null) return OperationResult.GetErrorResult("Body is null", code: 400);

            if (usuario.Email is null) return OperationResult.GetErrorResult("Email is null", code: 400);
            if (usuario.Password is null) return OperationResult.GetErrorResult("Password is null", code: 400);
            try
            {
                var result = await _usuariosRepository.LogIn(usuario);
                if (!Passwords.VerifyPassword(usuario.Password, result.Data.Clave))
                {
                    return OperationResult.GetErrorResult("Usuario o contraseña incorrectos", code: 403);
                }

                var token = _tokenProvider.Create(result.Data);
                return OperationResult.GetSuccesResult(token, code: 200);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"UsuarioService.LogIn: {ex}");
                return OperationResult.GetErrorResult($"{ex}", code: 500);
            }
        }
    }
}

//{
//    "email": "javier@gmail.com",
//  "password": "javier"
//}