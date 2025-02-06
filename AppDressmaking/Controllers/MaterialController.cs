using AppDressmaking.Models;
using AppDressmaking.Data;
using System.Collections.Generic;
using System.Web.Http;

namespace AppDressmaking.Controllers
{
    public class MaterialController : ApiController
    {
        private MaterialData materialData = new MaterialData();

        // GET api/material
        public List<Material> Get()
        {
            return materialData.ListarMateriales();
        }

        // GET api/material/5
        public Material Get(int id)
        {
            return materialData.ObtenerMaterialPorId(id);
        }

        // POST api/material
        public int Post([FromBody] Material material)
        {
            return materialData.RegistrarMaterial(material);
        }

        // PUT api/material
        public bool Put([FromBody] Material material)
        {
            return materialData.ActualizarMaterial(material);
        }

        // DELETE api/material/5
        public bool Delete(int id)
        {
            return materialData.EliminarMaterial(id);
        }
    }
}