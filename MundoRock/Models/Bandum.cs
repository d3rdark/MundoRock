using System;
using System.Collections.Generic;

#nullable disable

namespace MundoRock.Models
{
    public partial class Bandum
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Genero { get; set; }
        public string Origen { get; set; }
    }
}
