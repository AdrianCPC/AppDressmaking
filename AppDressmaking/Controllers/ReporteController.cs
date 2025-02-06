using AppDressmaking.Models;
using AppDressmaking.Data;
using System.Collections.Generic;
using System.Web.Http;

namespace AppDressmaking.Controllers
{
    public class ReporteController : ApiController
    {
        private ReporteData reporteData = new ReporteData();

        // GET api/reporte
        public List<Reporte> Get()
        {
            return reporteData.ListarReportes();
        }

        // GET api/reporte/5
        public Reporte Get(int id)
        {
            return reporteData.ObtenerReportePorId(id);
        }

        // POST api/reporte
        public int Post([FromBody] Reporte reporte)
        {
            return reporteData.RegistrarReporte(reporte);
        }

        // PUT api/reporte
        public bool Put([FromBody] Reporte reporte)
        {
            return reporteData.ActualizarReporte(reporte);
        }

        // DELETE api/reporte/5
        public bool Delete(int id)
        {
            return reporteData.EliminarReporte(id);
        }
    }
}