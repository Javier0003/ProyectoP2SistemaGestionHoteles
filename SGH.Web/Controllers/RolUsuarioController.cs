using Microsoft.AspNetCore.Mvc;
using SGH.Web.Models;
using SGHT.Application.Interfaces;
using System.Diagnostics;

namespace SGHT.Web.Controllers
{
    public class RolUsuarioController : Controller
    {
        private readonly ILogger<RolUsuarioController> _logger;
        private readonly IRolUsuarioService _rolUsuarioService;

        public RolUsuarioController(IRolUsuarioService rolUsuarioService ,ILogger<RolUsuarioController> logger)
        {
            _logger = logger;
            _rolUsuarioService = rolUsuarioService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _rolUsuarioService.GetAll();
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
