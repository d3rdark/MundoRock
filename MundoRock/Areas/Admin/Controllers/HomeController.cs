using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MundoRock.Models;
using Microsoft.AspNetCore.Authorization;

namespace MundoRock.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {

        public HomeController(mundorockContext context)
        {
            Context = context;
        }

        public mundorockContext Context { get; }

        [Route("admin/Home")]
        [Route("admin/Home/Index")]
        [Route("admin/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
