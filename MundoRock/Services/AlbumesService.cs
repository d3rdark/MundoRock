using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MundoRock.Models;

namespace MundoRock.Services
{
    public class AlbumesService
    {
        public List<Album> Albums { get; set; }

        public AlbumesService(mundorockContext context)
        {
            Albums = context.Albums.OrderBy(x => x.Nombre).Where(x => x.Eliminado == false).ToList();
        }
    }
}
