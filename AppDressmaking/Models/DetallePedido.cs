using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppDressmaking.Models
{
    public class DetallePedido
    {
        public int idDetallePedido { get; set; }
        public int idPedido { get; set; }
        public int idMaterial { get; set; }
        public int cantidad { get; set; }

        // Propiedades de navegación
        public Pedido Pedido { get; set; }
        public Material Material { get; set; }
    }
}