using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MundoRock.Models;
using Microsoft.EntityFrameworkCore;


namespace MundoRock.Controllers
{
    public class HomeController : Controller
    {
        public mundorockContext Context { get; set; }
        public HomeController(mundorockContext context)
        {
            Context = context;
        }
        public IActionResult Index()
        {
            var banda = Context.Banda.FirstOrDefault();
            return View(banda);
        }
        public IActionResult Albumes()
        {
            var albumes = Context.Albums.Where(x => x.Eliminado == false).OrderBy(x => x.Nombre);
            return View(albumes);
        }
        
        [Route("{id?}/Datos")]
        public IActionResult DAlbumes(string id)
        {
            id = id.Replace("-", " ");
            var datos = Context.Albums.FirstOrDefault(x => x.Nombre == id);
            if (datos == null)
            {
                return RedirectToAction("Albumes");
            }
            return View(datos);
        }

        public IActionResult Integrantes()
        {
            var integrantes = Context.Integrantes.OrderBy(x => x.Nombre);
            return View(integrantes);
        }

        [Route("{id?}/Informacion")]
        public IActionResult InformacionInt(string id)
        {
            return View();
        }

        [Route("{id?}/Reseña")]
        public IActionResult Reseña(string id)
        {
            id = id.Replace("-", " ");
            var reseña = Context.Reseñas.Include(x => x.IdalbumNavigation)
                .FirstOrDefault(x => x.IdalbumNavigation.Nombre == id);
            if (reseña == null)
            {
                return RedirectToAction("Albumes");
            }
            return View(reseña);
        }
    }
}
