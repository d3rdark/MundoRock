using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MundoRock.Models;
using MundoRock.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MundoRock.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ReseñasController : Controller
    {
        public mundorockContext Context { get; }
        public ReseñasController(mundorockContext context)
        {
            Context = context;
        }

        

        [Route("admin/Reseñas")]
        [Route("admin/Reseñas/Index")]
        public IActionResult Index()
        {
            var reseñas = Context.Reseñas.Where(x => x.IdalbumNavigation.Eliminado == false).OrderBy(x => x.Nombre);
            return View(reseñas);
        }

        #region Agregar
        [HttpGet]
        public IActionResult Agregar()
        {
            MundoRockViewModel vm = new MundoRockViewModel();
            vm.Albumes = Context.Albums.Where(x => x.Eliminado == false).OrderBy(x => x.Nombre);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Agregar(MundoRockViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Reseñas.Nombre))
            {
                ModelState.AddModelError("","El nombre de la reseña no puede estar vacio");
            }
            else if (string.IsNullOrWhiteSpace(vm.Reseñas.Informacion))
            {
                ModelState.AddModelError("", "La reseña de un album no puede estar vacia");
               
            }
            else 
            {
                Context.Add(vm.Reseñas);
                Context.SaveChanges();
                return RedirectToAction("Index");
            }
            vm.Albumes = Context.Albums.OrderBy(x => x.Lanzamiento);
            return View(vm);
        }
        #endregion


        #region Editar
        [HttpGet]
        public IActionResult Editar(int id)
        {
            MundoRockViewModel vm = new MundoRockViewModel();
            var reseñas = Context.Reseñas.FirstOrDefault(x => x.Id == id);
            if (reseñas == null)
            {
                return RedirectToAction("Index");
            }

            vm.Reseñas = reseñas;
            vm.Albumes = Context.Albums.Where(x => x.Eliminado == false).OrderBy(x => x.Nombre);

            return View(vm);
        }

        [HttpPost]
        public IActionResult Editar(MundoRockViewModel vm)
        {
            vm.Albumes = Context.Albums.Where(x => x.Eliminado == false).OrderBy(x => x.Nombre);
            
            if (string.IsNullOrWhiteSpace(vm.Reseñas.Nombre))
            {
                ModelState.AddModelError("", "El nombre de la reseña no puede estar vacio");
                
            }
            else if (string.IsNullOrWhiteSpace(vm.Reseñas.Informacion))
            {
                ModelState.AddModelError("", "La reseña de un album no puede estar vacia");
               
            }

            var reseñas = Context.Reseñas.FirstOrDefault(x => x.Id == vm.Reseñas.Id);

            if (reseñas == null)
            {
                return RedirectToAction("Index");
            }

            reseñas.Idalbum = vm.Reseñas.Idalbum;
            reseñas.Nombre = vm.Reseñas.Nombre;
            reseñas.Informacion = vm.Reseñas.Informacion;

            Context.Update(reseñas);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        #endregion

        #region Eliminar
        [Authorize(Roles ="Administrador")]
        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var reseñas = Context.Reseñas.FirstOrDefault(x => x.Id == id);
            if (reseñas == null)
            {
                return RedirectToAction("Index");
            }
            return View(reseñas);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult Eliminar(Reseña R)
        {
            var reseña = Context.Reseñas.FirstOrDefault(x => x.Id == R.Id);

            if (reseña == null)
            {
                ModelState.AddModelError("", "La reseña ya ha sido eliminada");
            }
            else
            {
                Context.Remove(reseña);
                Context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(R);
        }
        #endregion
    }
}
