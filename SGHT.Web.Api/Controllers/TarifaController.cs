using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGHT.Domain.Base;
using SGHT.Model.Model.rolUsuario;
using SGHT.Model.Model.tarifa;

namespace SGHT.Web.Api.Controllers
{
    public class TarifaController : Controller
    {
        // GET: TarifaController
        public async Task<IActionResult> Index()
        {
            List<GetTarifaModel> tarifas = new List<GetTarifaModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.GetAsync("Tarifas");

                if (response.IsSuccessStatusCode)
                    tarifas = await response.Content.ReadFromJsonAsync<List<GetTarifaModel>>();
                else
                {
                    ViewBag.Message = "Error obteniendo las tarifas.";
                    return View();
                }
            }

            return View(tarifas);
        }

        // GET: TarifaController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.GetAsync($"Tarifas/{id}");

                if (response.IsSuccessStatusCode)
                    return View(await response.Content.ReadFromJsonAsync<GetTarifaModel>());
                else
                {
                    ViewBag.Message = "Error obteniendo la tarifa.";
                    return View();
                }
            }
        }

        // GET: TarifaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TarifaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTarifaModel tarifa)
        {
            try
            {
                OperationResult result;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5118/api/");

                    var response = await client.PostAsJsonAsync<CreateTarifaModel>("Tarifas/crear", tarifa);

                    if (response.IsSuccessStatusCode)
                        result = await response.Content.ReadFromJsonAsync<OperationResult>();
                    else
                    {
                        ViewBag.Message = "Error creando el rol.";
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

        // GET: TarifaController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            GetTarifaModel usuario;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.GetAsync($"Tarifas/{id}");

                if (response.IsSuccessStatusCode)
                    usuario = await response.Content.ReadFromJsonAsync<GetTarifaModel>();
                else
                {
                    ViewBag.Message = "Error obteniendo la tarifa.";
                    return View();
                }
            }

            return View(usuario);
        }

        // POST: TarifaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GetTarifaModel tarifa)
        {
            OperationResult result;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5118/api/");

                    var response = await client.PatchAsJsonAsync<GetTarifaModel>("Tarifas/actualizar", tarifa);

                    if (response.IsSuccessStatusCode)
                        result = await response.Content.ReadFromJsonAsync<OperationResult>();
                    else
                    {
                        ViewBag.Message = "Error actualizando la tarifa.";
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

        // GET: TarifaController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            GetTarifaModel tarifa;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.GetAsync($"Tarifas/{id}");

                if (response.IsSuccessStatusCode)
                    tarifa = await response.Content.ReadFromJsonAsync<GetTarifaModel>();
                else
                {
                    ViewBag.Message = "Error obteniendo la tarifa.";
                    return View();
                }
            }

            return View(tarifa);
        }

        // POST: TarifaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteTarifaModel collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5118/api/");

                    var request = new HttpRequestMessage(HttpMethod.Delete, "Tarifas/eliminar")
                    {
                        Content = JsonContent.Create(collection)
                    };

                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    ViewBag.Message = "Error eliminando la tarifa.";
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
