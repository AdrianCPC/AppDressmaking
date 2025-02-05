using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppDressmaking.Models
{
    public class Inventario
    {
        public int idInventario { get; set; }
        public int idMaterial { get; set; }
        public int cantidad { get; set; }

        // Propiedad de navegación
        public Material Material { get; set; }
    }
}