using Microsoft.AspNetCore.Mvc;
using Pasteleria.Models;
using System.Diagnostics;

namespace Pasteleria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult NotAuthenticated()
        {
            return View();
        }

        [HttpGet]
        public IActionResult OperationFailed()
        {
            return View();
        }

        [HttpGet]
        public IActionResult invalidOperation()
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
