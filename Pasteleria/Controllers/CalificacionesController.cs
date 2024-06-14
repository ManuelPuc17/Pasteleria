using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pasteleria.Models;
using Pasteleria.Service;

namespace Pasteleria.Controllers
{
    [Authorize]
    public class CalificacionesController : Controller
    {

        private CalificacionService _serv;
        private PastelService _servPast;

        public CalificacionesController(CalificacionService serv, PastelService pastelService)
        {
            _serv = serv;
            _servPast = pastelService;
        }


        [HttpGet]
        public async Task<IActionResult> Calificaciones()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var cali = await _serv.ObtenerCalificacionesAsync(token!);
            return View(cali);
        }


        [HttpGet]
        public async Task<IActionResult> Calificar()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var pasteles = await _servPast.ObtenerListaAsync(token!);

            var model = new CalificacionDTO
            {
                ListaPasteles = pasteles
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Calificar(CalificacionDTO calificar)
        {
            if (ModelState.IsValid)
            {
                var token = HttpContext.Session.GetString("JWToken");
                var iduser = HttpContext.Session.GetString("UserId");
                if (int.TryParse(iduser, out int idUsuario))
                {
                    calificar.IdUsuario = idUsuario;
                    calificar.Usuario = iduser;
                }
                else
                {
                    return RedirectToAction("OperationFailed", "Home");
                }
                if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(iduser))
                {
                    var success = await _serv.CalificarAsync(calificar, token);
                    if (success)
                    {
                        return RedirectToAction("Calificaciones");
                    }
                    return RedirectToAction("OperationFailed", "Home");
                }
                else
                {
                    return RedirectToAction("NotAuthenticated", "Home");
                }
            }

            var pasteles = await _servPast.ObtenerListaAsync(HttpContext.Session.GetString("JWToken")!);
            calificar.ListaPasteles = pasteles;

            return View(calificar);

        }

        [HttpGet]
        public async Task<IActionResult> MiLista()
        {
            var iduser = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(iduser) || !int.TryParse(iduser, out int userId))
            {
                return RedirectToAction("NotAuthenticated", "Home");
            }

            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("NotAuthenticated", "Home");
            }

            var lista = await _serv.MiCalificacion(userId, token);

            if (lista == null)
            {
                return RedirectToAction("OperationFailed", "Home");
            }

            return View(lista);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var califi = await _serv.UnaCalificacion(id, token);
            if (califi == null)
            {
                return RedirectToAction("OperationFailed", "Home");
            }

            return View(califi);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(CalificacionDTO calificacion)
        {
            var iduser = HttpContext.Session.GetString("UserId");
            if (!int.TryParse(iduser, out int idUsuario))
            {
                return RedirectToAction("NotAuthenticated", "Home");
            }

            if (calificacion.IdUsuario != idUsuario)
            {
                return RedirectToAction("invalidOperation", "Home");
            }

            if (ModelState.IsValid)
            {
                var token = HttpContext.Session.GetString("JWToken");
                var result = await _serv.ActualizarCalificacion(calificacion, token);
                if (result)
                {
                    return RedirectToAction("MiLista");
                }
                return RedirectToAction("OperationFailed", "Home");
            }

            return View(calificacion);
        }


    }
}
