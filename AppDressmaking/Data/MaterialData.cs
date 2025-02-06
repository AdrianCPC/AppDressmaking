using AppDressmaking.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace AppDressmaking.Data
{
    public class MaterialData
    {
        private ConexionBD conexion;

        public MaterialData()
        {
            conexion = new ConexionBD();
        }

        public Material ObtenerMaterialPorId(int idMaterial)
        {
            Material material = null;
            string sentenciaSQL = "usp_ConsultarMaterialPorId";
            conexion.AgregarParametro(ParameterDirection.Input, "@idMaterial", SqlDbType.Int, 0, idMaterial);
            if (conexion.Consultar(sentenciaSQL, true))
            {
                SqlDataReader reader = conexion.Reader;
                if (reader.Read())
                {
                    material = new Material
                    {
                        idMaterial = reader.GetInt32(0),
                        nombre = reader.GetString(1),
                        tipo = reader.GetString(2),
                        cantidadDisponible = reader.GetInt32(3),
                        precioUnitario = reader.GetDecimal(4)
                    };
                }
                conexion.CerrarConexion();
            }
            else
            {
                throw new Exception("Error al obtener material: " + conexion.Error);
            }
            return material;
        }

        public List<Material> ListarMateriales()
        {
            List<Material> materiales = new List<Material>();
            string sentenciaSQL = "usp_ListarMateriales";
            if (conexion.Consultar(sentenciaSQL, true))
            {
                SqlDataReader reader = conexion.Reader;
                while (reader.Read())
                {
                    materiales.Add(new Material
                    {
                        idMaterial = reader.GetInt32(0),
                        nombre = reader.GetString(1),
                        tipo = reader.GetString(2),
                        cantidadDisponible = reader.GetInt32(3),
                        precioUnitario = reader.GetDecimal(4)
                    });
                }
                conexion.CerrarConexion();
            }
            else
            {
                throw new Exception("Error al listar materiales: " + conexion.Error);
            }
            return materiales;
        }

        public int RegistrarMaterial(Material material)
        {
            int idMaterialInsertado = 0;
            string sentenciaSQL = "usp_RegistrarMaterial";

            conexion.AgregarParametro(ParameterDirection.Output, "@idMaterialOut", SqlDbType.Int, 0, idMaterialInsertado);
            conexion.AgregarParametro(ParameterDirection.Input, "@nombre", SqlDbType.VarChar, 100, material.nombre);
            conexion.AgregarParametro(ParameterDirection.Input, "@tipo", SqlDbType.VarChar, 50, material.tipo);
            conexion.AgregarParametro(ParameterDirection.Input, "@cantidadDisponible", SqlDbType.Int, 0, material.cantidadDisponible);
            conexion.AgregarParametro(ParameterDirection.Input, "@precioUnitario", SqlDbType.Decimal, 0, material.precioUnitario);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                idMaterialInsertado = Convert.ToInt32(conexion.ValorUnico);
            }
            else
            {
                throw new Exception("Error al registrar material: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return idMaterialInsertado;
        }

        public bool ActualizarMaterial(Material material)
        {
            bool actualizado = false;
            string sentenciaSQL = "usp_ActualizarMaterial";

            conexion.AgregarParametro(ParameterDirection.Input, "@idMaterial", SqlDbType.Int, 0, material.idMaterial);
            conexion.AgregarParametro(ParameterDirection.Input, "@nombre", SqlDbType.VarChar, 100, material.nombre);
            conexion.AgregarParametro(ParameterDirection.Input, "@tipo", SqlDbType.VarChar, 50, material.tipo);
            conexion.AgregarParametro(ParameterDirection.Input, "@cantidadDisponible", SqlDbType.Int, 0, material.cantidadDisponible);
            conexion.AgregarParametro(ParameterDirection.Input, "@precioUnitario", SqlDbType.Decimal, 0, material.precioUnitario);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                actualizado = true;
            }
            else
            {
                throw new Exception("Error al actualizar material: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return actualizado;
        }

        public bool EliminarMaterial(int idMaterial)
        {
            bool eliminado = false;
            string sentenciaSQL = "usp_EliminarMaterial";
            conexion.AgregarParametro(ParameterDirection.Input, "@idMaterial", SqlDbType.Int, 0, idMaterial);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                eliminado = true;
            }
            else
            {
                throw new Exception("Error al eliminar material: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return eliminado;
        }
    }
}