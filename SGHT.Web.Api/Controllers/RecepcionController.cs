using Microsoft.AspNetCore.Mvc;
using SGHT.Domain.Base;
using SGHT.Model.Model.Recepcion;

namespace SGHT.Web.Api.Controllers
{
    public class RecepcionController : Controller
    {
        // GET: RecepcionController
        public async  Task<IActionResult> Index()
        {
            List<GetRecepcionModel> recepciones = new List<GetRecepcionModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.GetAsync("Recepcion");

                if (response.IsSuccessStatusCode)
                    recepciones = await response.Content.ReadFromJsonAsync<List<GetRecepcionModel>>();
                else
                {
                    ViewBag.Message = "Error obteniendo las recepciones.";
                    return View();
                }
            }

            return View(recepciones);
        } 
        
        // GET: RecepcionController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.GetAsync($"Recepcion/{id}");

                if (response.IsSuccessStatusCode)
                    return View(await response.Content.ReadFromJsonAsync<GetRecepcionModel>());
                else
                {
                    ViewBag.Message = "Error obteniendo la recepcion.";
                    return View();
                }
            }
        }

        // GET: RecepcionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecepcionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrearRecepcionModel recepcion)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.PostAsJsonAsync("Recepcion", recepcion);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                else
                {
                    ViewBag.Message = "Error creando la recepcion.";
                    return View();
                }
            }
        }

        // GET: RecepcionController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.GetAsync($"Recepcion/{id}");

                if (response.IsSuccessStatusCode)
                    return View(await response.Content.ReadFromJsonAsync<GetRecepcionModel>());
                else
                {
                    ViewBag.Message = "Error obteniendo la recepcion.";
                    return View();
                }
            }
        }

        // PUT: RecepcionController/Edit/5
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ActualizarRecepcionModel recepcion)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.PutAsJsonAsync($"Recepcion/{id}", recepcion);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                else
                {
                    ViewBag.Message = "Error actualizando la recepcion.";
                    return View();
                }
            }
        }

        // GET: RecepcionController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5118/api/");

                var response = await client.GetAsync($"Recepcion/{id}");

                if (response.IsSuccessStatusCode)
                    return View(await response.Content.ReadFromJsonAsync<GetRecepcionModel>());
                else
                {
                    ViewBag.Message = "Error obteniendo la recepcion.";
                    return View();
                }
            }
        }

        // POST: RecepcionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteRecepcionModel recepcionModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5118/api/");

                    var request = new HttpRequestMessage(HttpMethod.Delete, "Recepcion/eliminar")
                    {
                        Content = JsonContent.Create(recepcionModel)
                    };

                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    ViewBag.Message = "Error eliminando la recepcion.";
                    return View();
                }

            }
            catch (Exception e)
            {
                return View();
            }
        }

    }
}
