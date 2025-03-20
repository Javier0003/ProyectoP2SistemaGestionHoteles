using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SGH.Web.Models;
using SGHT.Application.Dtos.Servicio;
using SGHT.Application.Interfaces;
using System.Diagnostics;

namespace SGHT.Web.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly ILogger<ServiciosController> _logger;
        private readonly IServiciosService _serviciosService;
        private readonly IMapper _mapper;

        public ServiciosController(IServiciosService serviciosService, ILogger<ServiciosController> logger, IMapper mapper)
        {
            _logger = logger;
            _serviciosService = serviciosService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _serviciosService.GetAll();

            var servicio = _mapper.Map<List<UpdateServiciosDto>>(result.Data);
            if (!result.Success) return View();

            return View(servicio);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _serviciosService.GetById(id);
            if (!result.Success) return View();

            var mappedResult = _mapper.Map<UpdateServiciosDto>(result.Data);

            return View(mappedResult);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateServiciosDto servicio)
        {
            servicio.IdServicio = id;

            var result = await _serviciosService.UpdateById(servicio);
            if (!result.Success) return View();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveServiciosDto servicio)
        {
            var result = await _serviciosService.Save(servicio);

            if (!result.Success) return View();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var model = new DeleteServiciosDto
            {
                IdServicio = id
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteServiciosDto dto)
        {
            if (dto.IdServicio == 0)
            {
                ModelState.AddModelError("", "Servicio invalidado ID.");
                return View(dto);
            }

            var result = await _serviciosService.DeleteById(dto);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error eliminando servicio.");
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
