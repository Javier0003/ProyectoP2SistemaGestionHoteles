using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SGH.Web.Models;
using SGHT.Application.Dtos.Usuarios;
using SGHT.Application.Interfaces;

namespace SGHT.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(
            IUsuarioService usuarioService, 
            ILogger<UsuarioController> logger)
        {
            _logger = logger;
            _usuarioService = usuarioService;
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
            if (!result.Success) return View();

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateUsuarioDto usuario)
        {
            usuario.IdUsuario = id;

            if (usuario.Clave == null) return View();

            var result = await _usuarioService.UpdateById(usuario);
            if (!result.Success) return View();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveUsuarioDto usuario)
        {
            var result = await _usuarioService.Save(usuario);

            if (!result.Success) return View();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var model = new DeleteUsuarioDto
            {
                IdUsuario = id
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteUsuarioDto dto)
        {
            if (dto.IdUsuario == 0)
            {
                ModelState.AddModelError("", "Invalid user ID.");
                return View(dto);
            }

            var result = await _usuarioService.DeleteById(dto);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error deleting user.");
            return View(dto);
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
