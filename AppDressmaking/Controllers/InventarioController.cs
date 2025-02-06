using AppDressmaking.Models;
using AppDressmaking.Data;
using System.Collections.Generic;
using System.Web.Http;

namespace AppDressmaking.Controllers
{
    public class InventarioController : ApiController
    {
        private InventarioData inventarioData = new InventarioData();

        // GET api/inventario
        public List<Inventario> Get()
        {
            return inventarioData.ListarInventarios();
        }

        // GET api/inventario/5
        public Inventario Get(int id)
        {
            return inventarioData.ObtenerInventarioPorId(id);
        }

        // POST api/inventario
        public int Post([FromBody] Inventario inventario)
        {
            return inventarioData.RegistrarInventario(inventario);
        }

        // PUT api/inventario
        public bool Put([FromBody] Inventario inventario)
        {
            return inventarioData.ActualizarInventario(inventario);
        }

        // DELETE api/inventario/5
        public bool Delete(int id)
        {
            return inventarioData.EliminarInventario(id);
        }
    }
}