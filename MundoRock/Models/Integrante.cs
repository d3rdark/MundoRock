using System;
using System.Collections.Generic;

#nullable disable

namespace MundoRock.Models
{
    public partial class Integrante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public sbyte Edad { get; set; }
        public string Nacionalidad { get; set; }
        public string NombrePila { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
