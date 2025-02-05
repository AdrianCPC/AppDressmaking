using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppDressmaking.Models
{
    public class Pedido
    {
        public int idPedido { get; set; }
        public DateTime fecha { get; set; }
        public string estado { get; set; }
        public int cantidad { get; set; }
        public decimal total { get; set; }
        public int idCliente { get; set; }

        // Propiedad de navegación
        public Cliente Cliente { get; set; }
    }
}