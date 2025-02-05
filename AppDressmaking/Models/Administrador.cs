using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppDressmaking.Models
{
    public class Administrador
    {
        public int idAdministrador { get; set; }
        public int idUsuario { get; set; }
        public int nivelAcceso { get; set; }
    }
}