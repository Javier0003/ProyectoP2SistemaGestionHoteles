﻿using Microsoft.AspNetCore.Mvc;
using SGHT.Domain.Base;
using SGHT.Model.Model.usuario;

namespace SGHT.Web.Api.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UsuariosController> _logger;
        public UsuariosController(IConfiguration configuration, ILogger<UsuariosController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }


        // GET: UsuariosController
        public async Task<IActionResult> Index()
        {
            List<GetUsuarioModel> usuarios = new List<GetUsuarioModel>();
            using (var client = new HttpClient()) 
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.GetAsync("Usuario");

                if (response.IsSuccessStatusCode)
                    usuarios = await response.Content.ReadFromJsonAsync<List<GetUsuarioModel>>();
                else
                {
                    ViewBag.Message = "Error obteniendo los usuarios.";
                    return View();
                }
            }

            return View(usuarios);
        }

        // GET: RolUsuarioController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.GetAsync($"Usuario/{id}");

                if (response.IsSuccessStatusCode)
                    return View(await response.Content.ReadFromJsonAsync<GetUsuarioModel>());
                else
                {
                    ViewBag.Message = "Error obteniendo los usuarios.";
                    return View();
                }
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
                OperationResult result;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5118/api/");

                    var response = await client.PostAsJsonAsync<GetUsuarioModel>("Usuario/crear", usuario);

                    if (response.IsSuccessStatusCode)
                        result = await response.Content.ReadFromJsonAsync<OperationResult>();
                    else
                    {
                        ViewBag.Message = "Error creando el usuario.";
                        return View();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: UsuariosController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            GetUsuarioModel usuario;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.GetAsync($"Usuario/{id}");

                if (response.IsSuccessStatusCode)
                    usuario = await response.Content.ReadFromJsonAsync<GetUsuarioModel>();
                else
                {
                    ViewBag.Message = "Error obteniendo el usuario.";
                    return View();
                }
            }

            return View(usuario);
        }

        // POST: UsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GetUsuarioModel usuario)
        {
            OperationResult result;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5118/api/");

                    var response = await client.PatchAsJsonAsync<GetUsuarioModel>("Usuario/actualizar", usuario);

                    if (response.IsSuccessStatusCode)
                         result = await response.Content.ReadFromJsonAsync<OperationResult>();
                    else
                    {
                        ViewBag.Message = "Error actualizando el usuario.";
                        return View();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: UsuariosController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            GetUsuarioModel usuario;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.GetAsync($"Usuario/{id}");

                if (response.IsSuccessStatusCode)
                    usuario = await response.Content.ReadFromJsonAsync<GetUsuarioModel>();
                else
                {
                    ViewBag.Message = "Error obteniendo el usuario.";
                    return View();
                }
            }

            return View(usuario);
        }

        // POST: UsuariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, EliminarUsuarioModel collection)
        {
            try
            {
                using (var client = new HttpClient()) 
                { 
                    client.BaseAddress = new Uri("http://localhost:5118/api/");

                    var request = new HttpRequestMessage(HttpMethod.Delete, "Usuario/eliminar")
                    {
                        Content = JsonContent.Create(collection)
                    };

                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    ViewBag.Message = "Error eliminando el usuario.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}