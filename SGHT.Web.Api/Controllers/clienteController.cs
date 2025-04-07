using Microsoft.AspNetCore.Mvc;
using SGHT.Http.Repositories.Interfaces;
using SGHT.Model.Model.Cliente;
using SGHT.Web.Api.Base;

namespace SGHT.Web.Api.Controllers
{
    public class clienteController : BaseController
    {
        private readonly ILogger<clienteController> _logger;
        private readonly IClienteHttpRepository _clienteHttpRepository;
        public clienteController(ILogger<clienteController> logger, IClienteHttpRepository clienteHttpRepository)
        {
            _clienteHttpRepository = clienteHttpRepository;
            _logger = logger;
        }

        // GET: clienteController
        public async Task<IActionResult> Index()
        {
            try
            {
                var res = await _clienteHttpRepository.SendGetRequestAsync("Cliente");
                return View(await res.Content.ReadFromJsonAsync<List<GetClienteModel>>());
            }
            catch(Exception ex)
            {
                _logger.LogError($"clienteController.Index: {ex}");
                return ViewBagError("Error obteniendo los clientes.");
            }
        }

        // GET: clienteController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var res = await _clienteHttpRepository.SendGetRequestAsync($"Cliente/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetClienteModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"clienteController.Details: {ex}");
                return ViewBagError("Error obteniendo los clientes.");
            }
        }

        // GET: clienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: clienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AgregarClienteModel cliente)
        {
            try
            {
                if (cliente == null) throw new ArgumentNullException();

                var res = await _clienteHttpRepository.SendPostRequestAsync("Cliente/crear", cliente);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"clienteController.Create: {ex}");
                return ViewBagError("Error guardando al cliente");
            }
        }

        // GET: clienteController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var res = await _clienteHttpRepository.SendGetRequestAsync($"Cliente/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetClienteModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"clienteController.Edit: {ex}");
                return ViewBagError("Error Obteniendo el cliente");
            }
        }

        // PUT: clienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ActualizarCliente cliente)
        {
            try
            {
                if (cliente == null) throw new ArgumentNullException();

                var res = await _clienteHttpRepository.SendPatchRequestAsync("Cliente/actualizar", cliente);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                _logger.LogError($"clienteController.Edit: {ex}");
                return RedirectToAction(nameof(Edit));
            }
        }

        // GET: RecepcionController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await _clienteHttpRepository.SendGetRequestAsync($"Cliente/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetClienteModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"clienteController.Delete: {ex}");
                return ViewBagError("Error obteniendo el cliente");
            }
        }

        // POST: RecepcionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, ActualizarCliente cliente)
        {
            try
            {
                if (cliente == null) throw new ArgumentNullException();

                var res = await _clienteHttpRepository.SendDeleteRequestAsync("Cliente/eliminar", cliente);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"clienteController.Delete: {ex}");
                return ViewBagError("Error eliminando al cliente");
            }
        }

    }
}
