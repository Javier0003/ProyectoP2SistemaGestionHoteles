using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SGH.Web.Models;
using SGHT.Application.Dtos.Piso;
using SGHT.Application.Interfaces;
using SGHT.Domain.Entities;
using System.Diagnostics;

namespace SGH.Web.Controllers
{
    public class PisoController : Controller
    {
        private readonly ILogger<PisoController> _logger;
        private readonly IPisoService _pisoService;
        private readonly IMapper _mapper;

        public PisoController(IPisoService pisoService, ILogger<PisoController> logger, IMapper mapper)
        {
            _logger = logger;
            _pisoService = pisoService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _pisoService.GetAll();
            var pisos = _mapper.Map<List<UpdatePisoDto>>(result.Data);
            if (!result.Success) return View();

            return View(pisos);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _pisoService.GetById(id);
            if (!result.Success) return View();

            var mappedResult = _mapper.Map<UpdatePisoDto>(result.Data);

            return View(mappedResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdatePisoDto piso)
        {
            piso.IdPiso = id;

            var result = await _pisoService.UpdateById(piso);
            if (!result.Success) return View();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SavePisoDto piso)
        {
            var result = await _pisoService.Save(piso);

            if (!result.Success) return View();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var model = new DeletePisoDto
            {
                IdPiso = id
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeletePisoDto dto)
        {
            if (dto.IdPiso == 0)
            {
                ModelState.AddModelError("", "Invalid piso ID.");
                return View(dto);
            }

            var result = await _pisoService.DeleteById(dto);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error deleting piso.");
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
