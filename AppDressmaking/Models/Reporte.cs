using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppDressmaking.Models
{
    public class Reporte
    {
        public int idReporte { get; set; }
        public string tipo { get; set; }
        public DateTime fechaGeneracion { get; set; }
        public string contenido { get; set; }
        public int idUsuario { get; set; }

        // Propiedad de navegación
        public Usuario Usuario { get; set; }
    }
}