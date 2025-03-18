using AutoMapper;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SGH.Web.Models;
using SGHT.Application.Interfaces;
using SGHT.Application.Dtos.Categoria;

namespace SGHT.Web.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ILogger<CategoriaController> _logger;
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriaService categoriaService, ILogger<CategoriaController> logger, IMapper mapper)
        {
            _logger = logger;
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoriaService.GetAll();
            
            var categoria = _mapper.Map<List<UpdateCategoriaDto>>(result.Data);
            if (!result.Success) return View();

            return View(categoria);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _categoriaService.GetById(id);
            if (!result.Success) return View();

            var mappedResult = _mapper.Map<UpdateCategoriaDto>(result.Data);

            return View(mappedResult);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateCategoriaDto categoria)
        {
            categoria.IdCategoria = id;

            var result = await _categoriaService.UpdateById(categoria);
            if (!result.Success) return View();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveCategoriaDto categoria)
        {
            var result = await _categoriaService.Save(categoria);

            if (!result.Success) return View();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var model = new DeleteCategoriaDto
            {
                IdCategoria = id
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteCategoriaDto dto)
        {
            if (dto.IdCategoria == 0)
            {
                ModelState.AddModelError("", "Categoria invalidada ID.");
                return View(dto);
            }

            var result = await _categoriaService.DeleteById(dto);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error eliminando categoria.");
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
