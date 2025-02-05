using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppDressmaking.Models
{
    public class Material
    {
        public int idMaterial { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; }
        public int cantidadDisponible { get; set; }
        public decimal precioUnitario { get; set; }
    }
}