using Microsoft.AspNetCore.Mvc;
using SGHT.Domain.Entities;
using SGHT.Http.Repositories.Interfaces;
using SGHT.Model.Model.usuario;
using SGHT.Web.Api.Base;

namespace SGHT.Web.Api.Controllers
{
    public class UsuariosController : BaseController
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IUsuariosHttpRepository _usuariosHttpRepository;
        public UsuariosController(ILogger<UsuariosController> logger, IUsuariosHttpRepository usuariosHttpRepository)
        {
            _logger = logger;
            _usuariosHttpRepository = usuariosHttpRepository;
        }

        // GET: UsuariosController
        public async Task<IActionResult> Index()
        {
            try
            {
                var res = await _usuariosHttpRepository.SendGetRequestAsync("Usuario");
                return View(await res.Content.ReadFromJsonAsync<List<GetUsuarioModel>>());
            }
            catch (Exception ex) 
            {
                _logger.LogError($"UsuariosController.Index: {ex}");
                return ViewBagError("Error obteniendo los usuarios.");
            }
        }

        // GET: RolUsuarioController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var res = await _usuariosHttpRepository.SendGetRequestAsync($"Usuario/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetUsuarioModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"UsuariosController.Details: {ex}");
                return ViewBagError("Erorr obteniendo usuario");
            }
        }

        // GET: UsuariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GetUsuarioModel usuario)
        {
            try
            {
                if (usuario == null) throw new ArgumentNullException();

                var res = await _usuariosHttpRepository.SendPostRequestAsync("Usuario/crear", usuario);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError($"UsuariosController.Create: {ex}");
                return ViewBagError("Error creando el usuario");
            }
        }

        // GET: UsuariosController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var res = await _usuariosHttpRepository.SendGetRequestAsync($"Usuario/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetUsuarioModel>());
            }
            catch(Exception ex)
            {
                _logger.LogError($"UsuariosController.Edit: {ex}");
                return ViewBagError("Error Obteniendo el usuario");
            }
        }

        // POST: UsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GetUsuarioModel usuario)
        {
            try
            {
                if (usuario == null) throw new ArgumentNullException();

                var res = await _usuariosHttpRepository.SendPatchRequestAsync("Usuario/actualizar", usuario);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError($"UsuariosController.Edit: {ex}");
                return RedirectToAction(nameof(Edit));
            }
        }

        // GET: UsuariosController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await _usuariosHttpRepository.SendGetRequestAsync($"Usuario/{id}");
                return View(await res.Content.ReadFromJsonAsync<GetUsuarioModel>());
            }
            catch(Exception ex)
            {
                _logger.LogError($"UsuariosController.Delete: {ex}");
                return ViewBagError("Error obteniendo el usuario");
            }
        }

        // POST: UsuariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, EliminarUsuarioModel usuario)
        {
            try
            {
                if (usuario == null) throw new ArgumentNullException();

                var res = await _usuariosHttpRepository.SendDeleteRequestAsync("Usuario/eliminar", usuario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"UsuariosController.Delete: {ex}");
                return ViewBagError("Error eliminando usuario");
            }
        }
    }
}