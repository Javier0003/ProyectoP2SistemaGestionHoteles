using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SGH.Web.Models;
using SGHT.Application.Dtos.EstadoHabitacion;
using SGHT.Application.Interfaces;
using SGHT.Domain.Entities;
using System.Diagnostics;

namespace SGH.Web.Controllers
{
    public class EstadoHabitacionController : Controller
    {
        private readonly ILogger<EstadoHabitacionController> _logger;
        private readonly IEstadoHabitacionService _estadoHabitacionService;
        private readonly IMapper _mapper;

        public EstadoHabitacionController(IEstadoHabitacionService estadoHabitacionService, ILogger<EstadoHabitacionController> logger, IMapper mapper)
        {
            _logger = logger;
            _estadoHabitacionService = estadoHabitacionService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _estadoHabitacionService.GetAll();
            if (!result.Success) return View();

            return View(result.Data);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _estadoHabitacionService.GetById(id);
            if (!result.Success) return View();

            var mappedResult = _mapper.Map<UpdateEstadoHabitacionDto>(result.Data);

            return View(mappedResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateEstadoHabitacionDto estado)
        {
            estado.IdEstadoHabitacion = id;

            var result = await _estadoHabitacionService.UpdateById(estado);
            if (!result.Success) return View();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveEstadoHabitacionDto estado1)
        {
            var result = await _estadoHabitacionService.Save(estado1);

            if (!result.Success) return View();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var model = new DeleteEstadoHabitacionDto
            {
                IdEstadoHabitacion = id
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteEstadoHabitacionDto dto)
        {
            if (dto.IdEstadoHabitacion == 0)
            {
                ModelState.AddModelError("", "Invalid estado ID.");
                return View(dto);
            }

            var result = await _estadoHabitacionService.DeleteById(dto);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error deleting estado.");
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

