using Microsoft.AspNetCore.Mvc;
using SGH.Web.Models;
using SGHT.Application.Interfaces;
using System.Diagnostics;

namespace SGHT.Web.Controllers
{
    public class TarifasController : Controller
    {
        private readonly ILogger<TarifasController> _logger;
        private readonly ITarifaService _tarifaService;

        public TarifasController(ITarifaService tarifaService,ILogger<TarifasController> logger)
        {
            _logger = logger;
            _tarifaService = tarifaService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _tarifaService.GetAll();
            if (!result.Success) return View();

            return View(result.Data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
