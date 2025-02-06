using AppDressmaking.Models;
using AppDressmaking.Data;
using System.Collections.Generic;
using System.Web.Http;

namespace AppDressmaking.Controllers
{
    public class ClienteController : ApiController
    {
        private ClienteData clienteData = new ClienteData();

        // GET api/cliente
        public List<Cliente> Get()
        {
            return clienteData.ListarClientes();
        }

        // GET api/cliente/5
        public Cliente Get(int id)
        {
            return clienteData.ObtenerClientePorId(id);
        }

        // POST api/cliente
        public int Post([FromBody] Cliente cliente)
        {
            return clienteData.RegistrarCliente(cliente);
        }

        // PUT api/cliente
        public bool Put([FromBody] Cliente cliente)
        {
            return clienteData.ActualizarCliente(cliente);
        }

        // DELETE api/cliente/5
        public bool Delete(int id)
        {
            return clienteData.EliminarCliente(id);
        }
    }
}