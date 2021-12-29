using System;
using System.Collections.Generic;

#nullable disable

namespace MundoRock.Models
{
    public partial class Reseña
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Informacion { get; set; }
        public int Idalbum { get; set; }

        public virtual Album IdalbumNavigation { get; set; }
    }
}
