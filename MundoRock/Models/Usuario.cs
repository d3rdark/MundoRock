using System;
using System.Collections.Generic;

#nullable disable

namespace MundoRock.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string NombreReal { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
    }
}
