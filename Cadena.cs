using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
//Clase variable
namespace Ensamblador
{
    public class Cadena
    {
        private string _nombre;
        private string _cadena;
        public Cadena(string nombre,string cadena)
        {
            _nombre = nombre;
            _cadena = cadena;
        }
        public string Nombre
        {
            get => _nombre;
            set => _nombre = value;
        }
        public string Contenido
        {
            get => _cadena;
            set => _cadena = value;
        }
        
    }
}