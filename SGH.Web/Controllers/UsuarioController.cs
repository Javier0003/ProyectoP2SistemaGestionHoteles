using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SGH.Web.Models;
using SGHT.Application.Dtos.Usuarios;
using SGHT.Application.Interfaces;
using SGHT.Application.Utils;
using SGHT.Domain.Entities;

namespace SGHT.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioService _usuarioService;
        private readonly IRolUsuarioService _rolUsuarioService;

        public UsuarioController(
            IUsuarioService usuarioService, 
            IRolUsuarioService rolUsuarioService,
            ILogger<UsuarioController> logger)
        {
            _logger = logger;
            _usuarioService = usuarioService;
            _rolUsuarioService = rolUsuarioService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _usuarioService.GetAll();
            if (!result.Success) return View();

            return View(result.Data);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _usuarioService.GetById(id);
            if (!result.Success) return NotFound();

            // Get roles for dropdown
            var rolesResult = await _rolUsuarioService.GetAll();
            if (rolesResult.Success)
            {
                var roles = (List<RolUsuario>)rolesResult.Data;
                ViewBag.Roles = new SelectList(roles, "IdRolUsuario", "Descripcion");
            }

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuarios usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }
            if(usuario.Clave == null)
            {
                ModelState.AddModelError("Clave", "La contraseña es requerida");
                return View(usuario);
            }

            if (ModelState.IsValid)
            {
                // Ensure Estado is not null
                usuario.Estado = usuario.Estado ?? false;

                var updateDto = new UpdateUsuarioDto
                {
                    IdUsuario = usuario.IdUsuario,
                    NombreCompleto = usuario.NombreCompleto,
                    Correo = usuario.Correo,
                    Clave = Passwords.HashPassword(usuario.Clave),
                    IdRolUsuario = usuario.IdRolUsuario,
                    Estado = usuario.Estado.Value,
                    FechaCreacion = usuario.FechaCreacion ?? DateTime.Now
                };
                var result = await _usuarioService.UpdateById(updateDto);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                
                ModelState.AddModelError("", "Error updating user. Please try again.");
            }

            // If we got this far, something failed, redisplay form
            var rolesResult = await _rolUsuarioService.GetAll();
            if (rolesResult.Success)
            {
                var roles = (List<RolUsuario>)rolesResult.Data;
                ViewBag.Roles = new SelectList(roles, "IdRolUsuario", "Descripcion");
            }

            return View(usuario);
        }

        public IActionResult Create()
        {
            return View();
        } 

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
