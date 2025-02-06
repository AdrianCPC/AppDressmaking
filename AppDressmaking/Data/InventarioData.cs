using AppDressmaking.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace AppDressmaking.Data
{
    public class InventarioData
    {
        private ConexionBD conexion;

        public InventarioData()
        {
            conexion = new ConexionBD();
        }

        public Inventario ObtenerInventarioPorId(int idInventario)
        {
            Inventario inventario = null;
            string sentenciaSQL = "usp_ConsultarInventarioPorId";
            conexion.AgregarParametro(ParameterDirection.Input, "@idInventario", SqlDbType.Int, 0, idInventario);
            if (conexion.Consultar(sentenciaSQL, true))
            {
                SqlDataReader reader = conexion.Reader;
                if (reader.Read())
                {
                    inventario = new Inventario
                    {
                        idInventario = reader.GetInt32(0),
                        idMaterial = reader.GetInt32(1),
                        cantidad = reader.GetInt32(2)
                    };
                }
                conexion.CerrarConexion();
            }
            else
            {
                throw new Exception("Error al obtener inventario: " + conexion.Error);
            }
            return inventario;
        }

        public List<Inventario> ListarInventarios()
        {
            List<Inventario> inventarios = new List<Inventario>();
            string sentenciaSQL = "usp_ListarInventarios";
            if (conexion.Consultar(sentenciaSQL, true))
            {
                SqlDataReader reader = conexion.Reader;
                while (reader.Read())
                {
                    inventarios.Add(new Inventario
                    {
                        idInventario = reader.GetInt32(0),
                        idMaterial = reader.GetInt32(1),
                        cantidad = reader.GetInt32(2)
                    });
                }
                conexion.CerrarConexion();
            }
            else
            {
                throw new Exception("Error al listar inventarios: " + conexion.Error);
            }
            return inventarios;
        }

        public int RegistrarInventario(Inventario inventario)
        {
            int idInventarioInsertado = 0;
            string sentenciaSQL = "usp_RegistrarInventario";

            conexion.AgregarParametro(ParameterDirection.Output, "@idInventarioOut", SqlDbType.Int, 0, idInventarioInsertado);
            conexion.AgregarParametro(ParameterDirection.Input, "@idMaterial", SqlDbType.Int, 0, inventario.idMaterial);
            conexion.AgregarParametro(ParameterDirection.Input, "@cantidad", SqlDbType.Int, 0, inventario.cantidad);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                idInventarioInsertado = Convert.ToInt32(conexion.ValorUnico);
            }
            else
            {
                throw new Exception("Error al registrar inventario: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return idInventarioInsertado;
        }

        public bool ActualizarInventario(Inventario inventario)
        {
            bool actualizado = false;
            string sentenciaSQL = "usp_ActualizarInventario";

            conexion.AgregarParametro(ParameterDirection.Input, "@idInventario", SqlDbType.Int, 0, inventario.idInventario);
            //conexion.AgregarParametro(ParameterDirection.Input, "@idMaterial", SqlDbType.Int, 0, inventario.idMaterial);
            conexion.AgregarParametro(ParameterDirection.Input, "@cantidad", SqlDbType.Int, 0, inventario.cantidad);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                actualizado = true;
            }
            else
            {
                throw new Exception("Error al actualizar inventario: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return actualizado;
        }

        public bool EliminarInventario(int idInventario)
        {
            bool eliminado = false;
            string sentenciaSQL = "usp_EliminarInventario";
            conexion.AgregarParametro(ParameterDirection.Input, "@idInventario", SqlDbType.Int, 0, idInventario);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                eliminado = true;
            }
            else
            {
                throw new Exception("Error al eliminar inventario: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return eliminado;
        }
    }
}