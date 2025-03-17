using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SGH.Web.Models;
using SGHT.Application.Dtos.Habitacion;
using SGHT.Application.Interfaces;
using System.Diagnostics;

namespace SGHT.Web.Controllers
{
    public class HabitacionController : Controller
    {
        private readonly ILogger<HabitacionController> _logger;
        private readonly IHabitacionService _habitacionService;
        private readonly IMapper _mapper;

        public HabitacionController(IHabitacionService habitacionService, ILogger< HabitacionController> logger, IMapper mapper)
        {
            _logger = logger;
            _habitacionService = habitacionService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _habitacionService.GetAll();
            if (!result.Success) return View();

            return View(result.Data);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _habitacionService.GetById(id);
            if (!result.Success) return View();

            var mappedResult = _mapper.Map<UpdateHabitacionDto>(result.Data);

            return View(mappedResult);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateHabitacionDto habitacion)
        {
            habitacion.IdHabitacion = id;

            var result = await _habitacionService.UpdateById(habitacion);
            if (!result.Success) return View();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveHabitacionDto habitacion)
        {
            var result = await _habitacionService.Save(habitacion);

            if (!result.Success) return View();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var model = new DeleteHabitacionDto
            {
                IdHabitacion = id
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteHabitacionDto dto)
        {
            if (dto.IdHabitacion == 0)
            {
                ModelState.AddModelError("", "Habitacion invalidada ID.");
                return View(dto);
            }

            var result = await _habitacionService.DeleteById(dto);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error eliminando habitacion.");
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
