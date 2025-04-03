using Microsoft.AspNetCore.Mvc;
using SGHT.Http.Repositories.Interfaces;
using SGHT.Model.Model.estadoHabitacion;
using SGHT.Web.Api.Base;

namespace SGHT.Web.Api.Controllers
{
    public class EstadoHabitacionController : BaseController
    {
        private readonly ILogger<EstadoHabitacionController> _logger;
        private readonly IEstadoHabitacionHttpRepository _estadoHabitacionHttpRepository;

        public EstadoHabitacionController(ILogger<EstadoHabitacionController> logger,
                                          IEstadoHabitacionHttpRepository estadoHabitacionHttpRepository)
        {
            _logger = logger;
            _estadoHabitacionHttpRepository = estadoHabitacionHttpRepository;
        }

        // GET: EstadoHabitacion
        public async Task<IActionResult> Index()
        {
            try
            {
                var res = await _estadoHabitacionHttpRepository.SendGetRequestAsync("EstadoHabitacion");
                return View(await res.Content.ReadFromJsonAsync<List<GetEstadoHabitacionModel>>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionController.Index: {ex}");
                return ViewBagError("Error obteniendo los estados de habitación.");
            }
        }

        // GET: EstadoHabitacion/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var res = await _estadoHabitacionHttpRepository.SendGetRequestAsync($"EstadoHabitacion/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetEstadoHabitacionModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionController.Details: {ex}");
                return ViewBagError("Error obteniendo el estado de habitación.");
            }
        }

        // GET: EstadoHabitacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoHabitacion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GetEstadoHabitacionModel estadoHabitacion)
        {
            try
            {
                if (estadoHabitacion == null) throw new ArgumentNullException();

                var res = await _estadoHabitacionHttpRepository.SendPostRequestAsync("EstadoHabitacion/crear", estadoHabitacion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionController.Create: {ex}");
                return ViewBagError("Error creando el estado de habitación.");
            }
        }

        // GET: EstadoHabitacion/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var res = await _estadoHabitacionHttpRepository.SendGetRequestAsync($"EstadoHabitacion/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetEstadoHabitacionModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionController.Edit: {ex}");
                return ViewBagError("Error obteniendo el estado de habitación.");
            }
        }

        // POST: EstadoHabitacion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GetEstadoHabitacionModel estadoHabitacion)
        {
            try
            {
                if (estadoHabitacion == null) throw new ArgumentNullException();

                var res = await _estadoHabitacionHttpRepository.SendPatchRequestAsync("EstadoHabitacion/actualizar", estadoHabitacion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionController.Edit: {ex}");
                return RedirectToAction(nameof(Edit));
            }
        }

        // GET: EstadoHabitacion/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await _estadoHabitacionHttpRepository.SendGetRequestAsync($"EstadoHabitacion/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetEstadoHabitacionModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionController.Delete: {ex}");
                return ViewBagError("Error obteniendo el estado de habitación.");
            }
        }

        // POST: EstadoHabitacion/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteEstadoHabitacionModel estadoHabitacion)
        {
            try
            {
                if (estadoHabitacion == null) throw new ArgumentNullException();

                var res = await _estadoHabitacionHttpRepository.SendDeleteRequestAsync("EstadoHabitacion/eliminar", estadoHabitacion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionController.Delete: {ex}");
                return ViewBagError("Error eliminando el estado de habitación.");
            }
        }
    }
}