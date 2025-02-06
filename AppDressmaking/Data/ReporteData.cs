using AppDressmaking.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace AppDressmaking.Data
{
    public class ReporteData
    {
        private ConexionBD conexion;

        public ReporteData()
        {
            conexion = new ConexionBD();
        }

        public Reporte ObtenerReportePorId(int idReporte)
        {
            Reporte reporte = null;
            string sentenciaSQL = "usp_ConsultarReportePorId";
            conexion.AgregarParametro(ParameterDirection.Input, "@idReporte", SqlDbType.Int, 0, idReporte);

            if (conexion.Consultar(sentenciaSQL, true))
            {
                SqlDataReader reader = conexion.Reader;
                if (reader.Read())
                {
                    reporte = new Reporte
                    {
                        idReporte = reader.GetInt32(0),
                        tipo = reader.GetString(1),
                        fechaGeneracion = reader.GetDateTime(2),
                        contenido = reader.GetString(3),
                        //idUsuario = reader.GetInt32(4)
                    };
                }
                conexion.CerrarConexion();
            }
            else
            {
                throw new Exception("Error al obtener reporte: " + conexion.Error);
            }
            return reporte;
        }

        public List<Reporte> ListarReportes()
        {
            List<Reporte> reportes = new List<Reporte>();
            string sentenciaSQL = "usp_ListarReportes";

            if (conexion.Consultar(sentenciaSQL, true))
            {
                SqlDataReader reader = conexion.Reader;
                while (reader.Read())
                {
                    reportes.Add(new Reporte
                    {
                        idReporte = reader.GetInt32(0),
                        tipo = reader.GetString(1),
                        fechaGeneracion = reader.GetDateTime(2),
                        contenido = reader.GetString(3),
                        //idUsuario = reader.GetInt32(4)
                    });
                }
                conexion.CerrarConexion();
            }
            else
            {
                throw new Exception("Error al listar reportes: " + conexion.Error);
            }
            return reportes;
        }

        public int RegistrarReporte(Reporte reporte)
        {
            int idReporteInsertado = 0;
            string sentenciaSQL = "usp_RegistrarReporte";

            conexion.AgregarParametro(ParameterDirection.Output, "@idReporteOut", SqlDbType.Int, 0, idReporteInsertado);
            conexion.AgregarParametro(ParameterDirection.Input, "@tipo", SqlDbType.VarChar, 50, reporte.tipo);
            conexion.AgregarParametro(ParameterDirection.Input, "@fechaGeneracion", SqlDbType.DateTime, 0, reporte.fechaGeneracion);
            conexion.AgregarParametro(ParameterDirection.Input, "@contenido", SqlDbType.VarChar, -1, reporte.contenido); // -1 para MAX
            //conexion.AgregarParametro(ParameterDirection.Input, "@idUsuario", SqlDbType.Int, 0, reporte.idUsuario);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                idReporteInsertado = Convert.ToInt32(conexion.ValorUnico);
            }
            else
            {
                throw new Exception("Error al registrar reporte: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return idReporteInsertado;
        }

        public bool ActualizarReporte(Reporte reporte)
        {
            bool actualizado = false;
            string sentenciaSQL = "usp_ActualizarReporte";

            conexion.AgregarParametro(ParameterDirection.Input, "@idReporte", SqlDbType.Int, 0, reporte.idReporte);
            conexion.AgregarParametro(ParameterDirection.Input, "@tipo", SqlDbType.VarChar, 50, reporte.tipo);
            conexion.AgregarParametro(ParameterDirection.Input, "@fechaGeneracion", SqlDbType.DateTime, 0, reporte.fechaGeneracion);
            conexion.AgregarParametro(ParameterDirection.Input, "@contenido", SqlDbType.VarChar, -1, reporte.contenido); // -1 para MAX
            //conexion.AgregarParametro(ParameterDirection.Input, "@idUsuario", SqlDbType.Int, 0, reporte.idUsuario);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                actualizado = true;
            }
            else
            {
                throw new Exception("Error al actualizar reporte: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return actualizado;
        }

        public bool EliminarReporte(int idReporte)
        {
            bool eliminado = false;
            string sentenciaSQL = "usp_EliminarReporte";
            conexion.AgregarParametro(ParameterDirection.Input, "@idReporte", SqlDbType.Int, 0, idReporte);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                eliminado = true;
            }
            else
            {
                throw new Exception("Error al eliminar reporte: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return eliminado;
        }
    }
}