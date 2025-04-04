using Microsoft.AspNetCore.Mvc;
using SGHT.Http.Repositories.Interfaces;
using SGHT.Model.Model.habitacion;
using SGHT.Web.Api.Base;

namespace SGHT.Web.Api.Controllers
{
    public class HabitacionController : BaseController
    {
        private readonly ILogger<HabitacionController> _logger;
        private readonly IHabitacionHttpRepository _habitacionHttpRepository;
        public HabitacionController(ILogger<HabitacionController> logger, IHabitacionHttpRepository habitacionHttpRepository)
        {
            _habitacionHttpRepository = habitacionHttpRepository;
            _logger = logger;
        }

        // GET: HabitacionController
        public async Task<IActionResult> Index()
        {
            try
            {
                var res = await _habitacionHttpRepository.SendGetRequestAsync("Habitaciones");
                return View(await res.Content.ReadFromJsonAsync<List<GetHabitacionModel>>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"HabitacionesController.Index: {ex}");
                return ViewBagError("Error obteniendo las habitaciones.");
            }
        }

        // GET: HabitacionController/Details/2
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var res = await _habitacionHttpRepository.SendGetRequestAsync($"Habitacion/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetHabitacionModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"HabitacionController.Index: {ex}");
                return ViewBagError("Error obteniendo las habitaciones.");
            }
        }

        // GET: HabitacionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HabitacionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateHabitacionModel habitacion)
        {
            try
            {
                if (habitacion == null)
                    throw new ArgumentNullException();

                var res = await _habitacionHttpRepository.SendPostRequestAsync("Habitacion/crear", habitacion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"HabitacionController.Index: {ex}");
                return ViewBagError("Error creando la habitacion.");
            }
        }

        // GET: HabitacionController/Edit/2
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var res = await _habitacionHttpRepository.SendGetRequestAsync($"Habitacion/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetHabitacionModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"HabitacionController.Index: {ex}");
                return ViewBagError("Error obteniendo la habitacion.");
            }
        }

        // POST: HabitacionController/Edit/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GetHabitacionModel habitacion)
        {
            try
            {
                if (habitacion == null)
                    throw new ArgumentNullException();

                var res = await _habitacionHttpRepository.SendPatchRequestAsync("Habitacion/actualizar", habitacion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"HabitacionController.Edit: {ex}");
                return RedirectToAction(nameof(Edit));
            }
        }

        // GET: HabitacionController/Delete/2
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await _habitacionHttpRepository.SendGetRequestAsync($"Habitacion/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetHabitacionModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"HabitacionController.Index: {ex}");
                return ViewBagError("Error obteniendo la habitacion.");
            }
        }

        // POST: HabitacionController/Delete/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteHabitacionModel habitacion)
        {
            try
            {
                if (habitacion == null) throw new ArgumentNullException();

                var res = await _habitacionHttpRepository.SendDeleteRequestAsync("Habitacion/eliminar", habitacion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"HabitacionController.Delete: {ex}");
                return ViewBagError("Error eliminando la habitacion");
            }
        }
    }
}