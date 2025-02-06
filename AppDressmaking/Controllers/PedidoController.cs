using AppDressmaking.Models;
using AppDressmaking.Data;
using System.Collections.Generic;
using System.Web.Http;

namespace AppDressmaking.Controllers
{
    public class PedidoController : ApiController
    {
        private PedidoData pedidoData = new PedidoData();

        // GET api/pedido
        public List<Pedido> Get()
        {
            return pedidoData.ListarPedidos();
        }

        // GET api/pedido/5
        public Pedido Get(int id)
        {
            return pedidoData.ObtenerPedidoPorId(id);
        }

        // POST api/pedido
        public int Post([FromBody] Pedido pedido)
        {
            return pedidoData.RegistrarPedido(pedido);
        }

        // PUT api/pedido
        public bool Put([FromBody] Pedido pedido)
        {
            return pedidoData.ActualizarPedido(pedido);
        }

        // DELETE api/pedido/5
        public bool Delete(int id)
        {
            return pedidoData.EliminarPedido(id);
        }
    }
}