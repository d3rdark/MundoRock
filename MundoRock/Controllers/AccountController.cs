using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MundoRock.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;



namespace MundoRock.Controllers
{
    public class AccountController : Controller
    {
        public mundorockContext Context { get; }

        public AccountController(mundorockContext context)
        {
            Context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var hash = Helpers.Cifrado.GetHash(password);
            var user = Context.Usuarios.SingleOrDefault
                (x => x.NombreUsuario == username && x.Password == hash);
                 
            if (user != null)
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.NombreReal));
                claims.Add(new Claim(ClaimTypes.Role, user.Rol));
                claims.Add(new Claim("Id", user.Id.ToString()));

                var identidad = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(new ClaimsPrincipal(identidad));
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else
            {
                ModelState.AddModelError("", "Usuario o contraseña son incorrectos");
                return View();

            }

        }

        public async Task< IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
    }
}
