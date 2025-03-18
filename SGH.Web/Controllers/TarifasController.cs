using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SGH.Web.Models;
using SGHT.Application.Dtos.Tarifa;
using SGHT.Application.Interfaces;

using System.Diagnostics;

namespace SGHT.Web.Controllers
{
    public class TarifasController : Controller
    {
        private readonly ILogger<TarifasController> _logger;
        private readonly ITarifaService _tarifaService;
        private readonly IMapper _mapper;

        public TarifasController(ITarifaService tarifaService,ILogger<TarifasController> logger, IMapper mapper)
        {
            _logger = logger;
            _tarifaService = tarifaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _tarifaService.GetAll();
            var tarifas = _mapper.Map<List<UpdateTarifaDto>>(result.Data);
            if (!result.Success) return View();

            return View(tarifas);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _tarifaService.GetById(id);
            if (!result.Success) return View();

            var mappedResult = _mapper.Map<UpdateTarifaDto>(result.Data);

            return View(mappedResult);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateTarifaDto tarifa)
        {
            tarifa.IdTarifa = id;

            var result = await _tarifaService.UpdateById(tarifa);
            if (!result.Success) return View();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveTarifaDto tarifa)
        {
            var result = await _tarifaService.Save(tarifa);

            if (!result.Success) return View();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var model = new DeleteTarifaDto
            {
                IdTarifa = id
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteTarifaDto dto)
        {
            if (dto.IdTarifa == 0)
            {
                ModelState.AddModelError("", "Invalid tarifa ID.");
                return View(dto);
            }

            var result = await _tarifaService.DeleteById(dto);
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
