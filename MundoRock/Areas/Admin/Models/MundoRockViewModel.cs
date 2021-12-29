using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MundoRock.Models;

namespace MundoRock.Areas.Admin.Models
{
    
    public class MundoRockViewModel
    {
        public IEnumerable<Album> Albumes { get; set; }

        public Reseña Reseñas { get; set; }

        /*public IEnumerable<Album> lisAlbumes { get; set; }*/
    }
}
