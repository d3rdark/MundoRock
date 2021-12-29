using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MundoRock.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using MundoRock.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MundoRock.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Administrador")]
    public class AlbumesController : Controller
    {
        public mundorockContext Context { get; }
        public IWebHostEnvironment Host { get; set; }
        public AlbumesController(mundorockContext context, IWebHostEnvironment host)
        {
            Context = context;
            Host = host;
        }

        [Route("admin/Albumes")]
        [Route("admin/Albumes/Index")]
        public IActionResult Index()
        {
            /**/
            var albumes = Context.Albums.Where(x => x.Eliminado == false).OrderBy(x => x.Nombre);
            return View(albumes);
        }

        #region Agregar
        [HttpGet]
        public IActionResult Agregar()
        {

            return View(new Album());
        }

        [HttpPost]
        public IActionResult Agregar(Album album, IFormFile archivo1)
        {
            if (string.IsNullOrWhiteSpace(album.Nombre))
            {
                ModelState.AddModelError("", "El nombre no puede estar en vacio");
            }
            else if (Context.Albums.Any(x => x.Nombre == album.Nombre && x.Eliminado == true && x.Id != album.Id))
            {
                ModelState.AddModelError("","Ya existe un album con el mismo nombre.");
            }
            else if (string.IsNullOrWhiteSpace(album.Descripcion))
            {
                ModelState.AddModelError("", "La descripcion del album no puede estar vacia");
            }
            else if (string.IsNullOrWhiteSpace(album.Genero))
            {
                ModelState.AddModelError("", "El genero del album no puede estar vacio");
            }
            else
            {
                if (archivo1 != null)
                {
                    if (archivo1.ContentType != "image/jpeg")
                    {
                        ModelState.AddModelError("", "Solo se permite la carga de imagen en formato jpg");
                        return View(album);
                    }
                }
                album.Eliminado = false;
                Context.Add(album);
                Context.SaveChanges();
                if (archivo1 != null)
                {
                    var path = Host.WebRootPath + "/Imagenes/Albumes/" + album.Id + "_a.jpg";
                    FileStream fs = new FileStream(path, FileMode.Create);
                    archivo1.CopyTo(fs);
                    fs.Close();
                }
                return RedirectToAction("Index");
            }
            return View(album);
        }
        #endregion

        #region Editar
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var album = Context.Albums.FirstOrDefault(x => x.Id == id);
            if (album == null || album.Eliminado)
            {
                return RedirectToAction("Index");
            }
            return View(album);
        }


        [HttpPost]
        public IActionResult Editar(Album alb, IFormFile archivo1)
        {

            if (string.IsNullOrWhiteSpace(alb.Nombre))
            {
                ModelState.AddModelError("", "El nombre no puede estar en vacio");
            }
            else if (Context.Albums.Any(x => x.Nombre == alb.Nombre && x.Eliminado == true && x.Id != alb.Id))
            {
                ModelState.AddModelError("", "Ya existe un album con el mismo nombre");
            }
            else if (string.IsNullOrWhiteSpace(alb.Descripcion))
            {
                ModelState.AddModelError("", "La descripcion del album no puede estar vacia");
            }
            else if (string.IsNullOrWhiteSpace(alb.Genero))
            {
                ModelState.AddModelError("", "El genero del album no puede estar vacio");
            }
            else
            {
                if (archivo1 != null)
                {
                    if (archivo1.ContentType != "image/jpeg")
                    {
                        ModelState.AddModelError("", "Solo se permite la carga de imagen en formato jpg");
                        return View(alb);
                    }
                }
                var Editaralbum = Context.Albums.FirstOrDefault(x => x.Id == alb.Id);
                if (Editaralbum == null || Editaralbum.Eliminado)
                {
                    RedirectToAction("Index");
                }
                Editaralbum.Nombre = alb.Nombre;
                Editaralbum.Descripcion = alb.Descripcion;
                Editaralbum.Lanzamiento = alb.Lanzamiento;
                Editaralbum.Genero = alb.Genero;
                Context.Update(Editaralbum);
                Context.SaveChanges();
                if (archivo1 != null)
                {
                    var path = Host.WebRootPath + "/Imagenes/Albumes/" + alb.Id + "_a.jpg";
                    FileStream fs = new FileStream(path, FileMode.Create);
                    archivo1.CopyTo(fs);
                    fs.Close();
                }
                return RedirectToAction("Index");

            }

            return View(alb);
        }
        #endregion

        #region Eliminar
        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var alb = Context.Albums.FirstOrDefault(x => x.Id == id);
            if (alb == null)
            {
                return RedirectToAction("Index");
            }
            return View(alb);
        }


        [HttpPost]
        public IActionResult Eliminar(Album album)
        {
            var alb = Context.Albums.Include(x => x.Reseñas).FirstOrDefault(x => x.Id == album.Id);
            if (alb == null)
            {
                ModelState.AddModelError("", "Este album ya ha sido eliminado");
            }
            else
            {
                if (alb.Reseñas.Count() == 0)
                {
                    Context.Remove(alb);
                }
                else
                {
                    alb.Eliminado = true;
                }
                Context.SaveChanges();
                var foto = Host.WebRootPath + "/Imagenes/Albumes/" + alb.Id + "_a.jpg";
                if (System.IO.File.Exists(foto))
                {
                    System.IO.File.Delete(foto);
                }
                return RedirectToAction("Index");
            }
            return View(alb);
        }
        #endregion
    }
}
