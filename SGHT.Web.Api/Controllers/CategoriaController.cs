using Microsoft.AspNetCore.Mvc;
using SGHT.Http.Repositories.Interfaces;
using SGHT.Model.Model.categoria;
using SGHT.Web.Api.Base;

namespace SGHT.Web.Api.Controllers
{
    public class CategoriaController : BaseController
    {
        private readonly ILogger<CategoriaController> _logger;
        private readonly ICategoriaHttpRepository _categoriaHttpRepository;
        public CategoriaController(ILogger<CategoriaController> logger, ICategoriaHttpRepository categoriaHttpRepository)
        {
            _categoriaHttpRepository = categoriaHttpRepository;
            _logger = logger;
        }

        // GET: CategoriaController
        public async Task<IActionResult> Index()
        {
            try
            {
                var res = await _categoriaHttpRepository.SendGetRequestAsync("Categorias");
                return View(await res.Content.ReadFromJsonAsync<List<GetCategoriaModel>>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoriaController.Index: {ex}");
                return ViewBagError("Error obteniendo las categorias.");
            }
        }

        // GET: CategoriaController/Details/2
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var res = await _categoriaHttpRepository.SendGetRequestAsync($"Categorias/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetCategoriaModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoriaController.Index: {ex}");
                return ViewBagError("Error obteniendo las categorias.");
            }
        }

        // GET: CategoriaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoriaModel categoria)
        {
            try
            {
                if (categoria == null)
                    throw new ArgumentNullException();

                var res = await _categoriaHttpRepository.SendPostRequestAsync("Categoria/crear", categoria);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoriaController.Index: {ex}");
                return ViewBagError("Error creando las categorias.");
            }
        }

        // GET: CategoriaController/Edit/2
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var res = await _categoriaHttpRepository.SendGetRequestAsync($"Categoria/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetCategoriaModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoriaController.Index: {ex}");
                return ViewBagError("Error obteniendo las categorias.");
            }
        }

        // POST: CategoriaController/Edit/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GetCategoriaModel categoria)
        {
            try
            {
                if (categoria == null)
                    throw new ArgumentNullException();

                var res = await _categoriaHttpRepository.SendPatchRequestAsync("Categoria/actualizar", categoria);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoriaController.Edit: {ex}");
                return RedirectToAction(nameof(Edit));
            }
        }

        // GET: CategoriaController/Delete/2
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await _categoriaHttpRepository.SendGetRequestAsync($"Categoria/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetCategoriaModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoriaController.Index: {ex}");
                return ViewBagError("Error obteniendo las categorias.");
            }
        }

        // POST: CategoriaController/Delete/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteCategoriaModel categoria)
        {
            try
            {
                if (categoria == null) throw new ArgumentNullException();

                var res = await _categoriaHttpRepository.SendDeleteRequestAsync("Categorias/eliminar", categoria);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoriaController.Delete: {ex}");
                return ViewBagError("Error eliminando las categorias");
            }
        }
    }
}