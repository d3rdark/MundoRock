using System;
using System.Collections.Generic;

#nullable disable

namespace MundoRock.Models
{
    public partial class Album
    {
        public Album()
        {
            Reseñas = new HashSet<Reseña>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime Lanzamiento { get; set; }
        public string Genero { get; set; }
        public bool Eliminado { get; set; }

        public virtual ICollection<Reseña> Reseñas { get; set; }
    }
}
