using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SGH.Web.Models;
using SGHT.Application.Dtos.RolUsuario;
using SGHT.Application.Interfaces;
using System.Diagnostics;

namespace SGHT.Web.Controllers
{
    public class RolUsuarioController : Controller
    {
        private readonly ILogger<RolUsuarioController> _logger;
        private readonly IRolUsuarioService _rolUsuarioService;
        private readonly IMapper _mapper;

        public RolUsuarioController(IRolUsuarioService rolUsuarioService ,ILogger<RolUsuarioController> logger, IMapper mapper)
        {
            _logger = logger;
            _rolUsuarioService = rolUsuarioService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _rolUsuarioService.GetAll();
            if (!result.Success) return View();

            return View(result.Data);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _rolUsuarioService.GetById(id);
            if (!result.Success) return View();

            var mappedResult = _mapper.Map<UpdateRolUsuarioDto>(result.Data);

            return View(mappedResult);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateRolUsuarioDto rol)
        {
            rol.IdRolUsuario = id;

            var result = await _rolUsuarioService.UpdateById(rol);
            if (!result.Success) return View();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveRolUsuarioDto rol)
        {
            var result = await _rolUsuarioService.Save(rol);

            if (!result.Success) return View();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var model = new DeleteRolUsuarioDto
            {
                IdRolUsuario = id
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteRolUsuarioDto dto)
        {
            if (dto.IdRolUsuario == 0)
            {
                ModelState.AddModelError("", "Invalid user ID.");
                return View(dto);
            }

            var result = await _rolUsuarioService.DeleteById(dto);
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
