using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppDressmaking.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string contrasena { get; set; }
        public char tipoUsuario { get; set; }
        public DateTime FechaIngreso { get; set; }

        // Propiedades de navegación
        public Empleado Empleado { get; set; }
        public Administrador Administrador { get; set; }
    }
}