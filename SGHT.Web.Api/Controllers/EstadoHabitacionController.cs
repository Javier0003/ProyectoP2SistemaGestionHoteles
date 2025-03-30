using Microsoft.AspNetCore.Mvc;

using SGHT.Model.Model.estadoHabitacion;
using SGHT.Domain.Base;

namespace SGHT.Web.Api.Controllers
{
    public class EstadoHabitacionController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<GetEstadoHabitacionModel> estados = new List<GetEstadoHabitacionModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5223/api/");
                var response = await client.GetAsync("EstadoHabitacion");
                if (response.IsSuccessStatusCode)
                    estados = await response.Content.ReadFromJsonAsync<List<GetEstadoHabitacionModel>>();
                else
                {
                    ViewBag.Message = "Error obteniendo los estados de habitación.";
                    return View();
                }
            }
            return View(estados);
        }

        public async Task<IActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5223/api/");
                var response = await client.GetAsync($"EstadoHabitacion/{id}");
                if (response.IsSuccessStatusCode)
                    return View(await response.Content.ReadFromJsonAsync<GetEstadoHabitacionModel>());
                else
                {
                    ViewBag.Message = "Error obteniendo el estado de habitación.";
                    return View();
                }
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEstadoHabitacionModel estadoO)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5223/api/");
                    var response = await client.PostAsJsonAsync("EstadoHabitacion/crear", estadoO);
                    if (!response.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Error creando el estado de habitación.";
                        return View();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            GetEstadoHabitacionModel estado;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5223/api/");
                var response = await client.GetAsync($"EstadoHabitacion/{id}");
                if (response.IsSuccessStatusCode)
                    estado = await response.Content.ReadFromJsonAsync<GetEstadoHabitacionModel>();
                else
                {
                    ViewBag.Message = "Error obteniendo el estado de habitación.";
                    return View();
                }
            }
            return View(estado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GetEstadoHabitacionModel estado)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5223/api/");
                    var response = await client.PatchAsJsonAsync("EstadoHabitacion/actualizar", estado);
                    if (!response.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Error actualizando el estado de habitación.";
                        return View();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            GetEstadoHabitacionModel estado;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5223/api/");
                var response = await client.GetAsync($"EstadoHabitacion/{id}");
                if (response.IsSuccessStatusCode)
                    estado = await response.Content.ReadFromJsonAsync<GetEstadoHabitacionModel>();
                else
                {
                    ViewBag.Message = "Error obteniendo el estado de habitación.";
                    return View();
                }
            }
            return View(estado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteEstadoHabitacionModel estado)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5223/api/");
                    var request = new HttpRequestMessage(HttpMethod.Delete, "EstadoHabitacion/eliminar")
                    {
                        Content = JsonContent.Create(estado)    
                    };
                    var response = await client.SendAsync(request);
                    if (!response.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Error eliminando el estado de habitación.";
                        return View();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
