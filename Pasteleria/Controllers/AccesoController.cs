using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Pasteleria.Models;
using Pasteleria.Service;
using System.Security.Claims;

namespace Pasteleria.Controllers
{
    public class AccesoController : Controller
    {
        private readonly AccesoService _access;

        public AccesoController(AccesoService acc)
        {
            _access = acc;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(UsuarioDTO model)
        {
            if (ModelState.IsValid)
            {
                var success = await _access.RegistrarseAsync(model);
                if (success)
                {
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("", "Registration failed.");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _access.LoginAsync(model);
                if (response.IsSuccess)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, response.UserId),
                        new Claim(ClaimTypes.Name, response.UserName)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    HttpContext.Session.SetString("JWToken", response.Token);
                    HttpContext.Session.SetString("UserId", response.UserId);
                    HttpContext.Session.SetString("UserName", response.UserName);

                    return RedirectToAction("Lista", "Pastel");
                }
                ModelState.AddModelError("", "Login failed.");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }



    }
}
