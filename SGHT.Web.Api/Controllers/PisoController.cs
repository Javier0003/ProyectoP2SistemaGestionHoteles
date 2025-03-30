using Microsoft.AspNetCore.Mvc;
using SGHT.Domain.Base;
using SGHT.Model.Model.Piso;

namespace SGHT.Web.Api.Controllers
{
    public class PisoController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<GetPisoModel> pisos = new List<GetPisoModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5223/api/");
                var response = await client.GetAsync("Piso");
                if (response.IsSuccessStatusCode)
                    pisos = await response.Content.ReadFromJsonAsync<List<GetPisoModel>>();
                else
                {
                    ViewBag.Message = "Error obteniendo los Pisos.";
                    return View();
                }
            }
            return View(pisos);
        }

        public async Task<IActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5223/api/");
                var response = await client.GetAsync($"Piso/{id}");
                if (response.IsSuccessStatusCode)
                    return View(await response.Content.ReadFromJsonAsync<GetPisoModel>());
                else
                {
                    ViewBag.Message = "Error obteniendo el Piso.";
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
        public async Task<IActionResult> Create(CreatePisoModel piso)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5223/api/");
                    var response = await client.PostAsJsonAsync("Piso/crear", piso);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    ViewBag.Message = "Error creando el Piso.";
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            GetPisoModel piso;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5223/api/");
                var response = await client.GetAsync($"Piso/{id}");
                if (response.IsSuccessStatusCode)
                    piso = await response.Content.ReadFromJsonAsync<GetPisoModel>();
                else
                {
                    ViewBag.Message = "Error obteniendo el Piso.";
                    return View();
                }
            }
            return View(piso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GetPisoModel piso)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5223/api/");
                    var response = await client.PatchAsJsonAsync("Piso/actualizar", piso);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    ViewBag.Message = "Error actualizando el Piso.";
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            GetPisoModel piso;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5223/api/");
                var response = await client.GetAsync($"Piso/{id}");
                if (response.IsSuccessStatusCode)
                    piso = await response.Content.ReadFromJsonAsync<GetPisoModel>();
                else
                {
                    ViewBag.Message = "Error obteniendo el Piso.";
                    return View();
                }
            }
            return View(piso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeletePisoModel collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5223/api/");
                    var request = new HttpRequestMessage(HttpMethod.Delete, "Piso/eliminar")
                    {
                        Content = JsonContent.Create(collection)
                    };
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    ViewBag.Message = "Error eliminando el Piso.";
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}