using Microsoft.AspNetCore.Mvc;
using Pasteleria.Service;
using System.Reflection;
using Pasteleria.Models;
using Microsoft.AspNetCore.Authorization;

namespace Pasteleria.Controllers
{
    [Authorize]
    public class PastelController : Controller
    {
        private readonly PastelService _pastelService;

        public PastelController(PastelService pastelService)
        {
            _pastelService = pastelService;
        }


        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var name = HttpContext.Session.GetString("UserName");
            var pasteles = await _pastelService.ObtenerListaAsync(token);

            ViewBag.UserName = name;

            return View(pasteles); 
        }       
        


        [HttpGet]
        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(PastelDTO pastel)
        {
            if (ModelState.IsValid)
            {
                var token = HttpContext.Session.GetString("JWToken");
                if (!string.IsNullOrEmpty(token))
                {
                    var success = await _pastelService.AgregarAsync(pastel, token);
                    if (success)
                    {
                        return RedirectToAction("Lista");
                    }
                    return RedirectToAction("OperationFailed", "Home");
                }
                else
                {
                    return RedirectToAction("NotAuthenticated", "Home");
                }
            }
            return View(pastel);

        }


        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var pastel = await _pastelService.ObtenerListaidAsync(id, token);
            if (pastel == null)
            {
                return NotFound();
            }

            return View(pastel);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(PastelDTO pastel)
        {
            if (ModelState.IsValid)
            {
                var token = HttpContext.Session.GetString("JWToken");
                if (!string.IsNullOrEmpty(token))
                {
                    var success = await _pastelService.EditarAsync(pastel, token);
                    if (success)
                    {
                        return RedirectToAction("Lista");
                    }
                    return RedirectToAction("OperationFailed", "Home");
                }
                else
                {
                    return RedirectToAction("NotAuthenticated", "Home");
                }
            }
            return View(pastel);

        }

        [HttpGet]
        public IActionResult Eliminar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("NotAuthenticated", "Home");
            }
            var success = await _pastelService.EliminarAsync(id, token);
            if (!success)
            {
                return RedirectToAction("OperationFailed", "Home");
            }
            return RedirectToAction("Lista");
        }

    }
}
