using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace MundoRock.Helpers
{
    public static class Cifrado
    {
        /*Cifrado asimetrico*/
        public static string GetHash(string cadena)
        {
            var algoritmo = SHA512.Create();
            //Convertir la cadena en un arreglo de byte[]
            var arreglo = Encoding.UTF8.GetBytes(cadena+"proyectoMundoRock");

            var hash = algoritmo.ComputeHash(arreglo).Select(x => x.ToString("x2"));
            return string.Join("", hash);
        }

    }
}
