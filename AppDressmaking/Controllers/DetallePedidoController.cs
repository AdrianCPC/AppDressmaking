using AppDressmaking.Models;
using AppDressmaking.Data;
using System.Collections.Generic;
using System.Web.Http;

namespace AppDressmaking.Controllers
{
    public class DetallePedidoController : ApiController
    {
        private DetallePedidoData detallePedidoData = new DetallePedidoData();

        // GET api/detallepedido
        public List<DetallePedido> Get()
        {
            return detallePedidoData.ListarDetallePedidos();
        }

        // GET api/detallepedido/5
        public DetallePedido Get(int id)
        {
            return detallePedidoData.ObtenerDetallePedidoPorId(id);
        }

        // POST api/detallepedido
        public int Post([FromBody] DetallePedido detallePedido)
        {
            return detallePedidoData.RegistrarDetallePedido(detallePedido);
        }

        // PUT api/detallepedido
        public bool Put([FromBody] DetallePedido detallePedido)
        {
            return detallePedidoData.ActualizarDetallePedido(detallePedido);
        }

        // DELETE api/detallepedido/5
        public bool Delete(int id)
        {
            return detallePedidoData.EliminarDetallePedido(id);
        }
    }
}