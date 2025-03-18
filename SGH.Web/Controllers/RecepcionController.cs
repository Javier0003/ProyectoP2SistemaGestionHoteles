using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using SGH.Web.Models;
using SGHT.Application.Dtos.RecepcionDto;
using SGHT.Application.Interfaces;
using SGHT.Application.Services;
using SGHT.Domain.Entities.Reservation;
using System.Diagnostics;


namespace SGHT.Web.Controllers
{
    public class RecepcionController : Controller
    {
        private readonly ILogger<RecepcionController> _logger;
        private readonly IRecepcionService _recepcionService;
        private readonly IMapper _mapper;
        
        public RecepcionController(IRecepcionService recepcionService, ILogger<RecepcionController> logger, IMapper mapper)
        {
            _logger = logger;
            _recepcionService = recepcionService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _recepcionService.GetAll();
      
            if (!result.Success) 
                return View();

            return View(result.Data);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _recepcionService.GetById(id);
            if (!result.Success) return View();

            var mappedResult = _mapper.Map<UpdateRecepcionDto>(result.Data);

            return View(mappedResult);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateRecepcionDto recepcion)
        {
            recepcion.IdRecepcion = id;

            var result = await _recepcionService.UpdateById(recepcion);
            if (!result.Success) return View();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveRecepcionDto recepcionDto)
        {
            var result = await _recepcionService.Save(recepcionDto);

            if(!result.Success) return View();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var model = new DeleteRecepcionDto
            {
                IdRecepcion = id
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete( DeleteRecepcionDto recepcion)
        {
            if(recepcion.IdRecepcion == 0)
            {
                ModelState.AddModelError("IdRecepcion", "El id de la recepcion no puede ser 0");
                return View(recepcion);
            }
            
            var result = await _recepcionService.DeleteById(recepcion);

            if (!result.Success)
            {
                ModelState.AddModelError("", "Error deleting user.");
                return View(recepcion);
            }

            return RedirectToAction(nameof(Index));
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
