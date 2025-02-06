using AppDressmaking.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace AppDressmaking.Data
{
    public class DetallePedidoData
    {
        private ConexionBD conexion;

        public DetallePedidoData()
        {
            conexion = new ConexionBD();
        }

        public DetallePedido ObtenerDetallePedidoPorId(int idDetallePedido)
        {
            DetallePedido detallePedido = null;
            string sentenciaSQL = "usp_ConsultarDetallePedidoPorId";
            conexion.AgregarParametro(ParameterDirection.Input, "@idDetallePedido", SqlDbType.Int, 0, idDetallePedido);
            if (conexion.Consultar(sentenciaSQL, true))
            {
                SqlDataReader reader = conexion.Reader;
                if (reader.Read())
                {
                    detallePedido = new DetallePedido
                    {
                        idDetallePedido = reader.GetInt32(0),
                        idPedido = reader.GetInt32(1),
                        idMaterial = reader.GetInt32(2),
                        cantidad = reader.GetInt32(3)
                    };
                }
                conexion.CerrarConexion();
            }
            else
            {
                throw new Exception("Error al obtener detalle de pedido: " + conexion.Error);
            }
            return detallePedido;
        }

        public List<DetallePedido> ListarDetallePedidos()
        {
            List<DetallePedido> detallePedidos = new List<DetallePedido>();
            string sentenciaSQL = "usp_ListarDetallesPedido";
            if (conexion.Consultar(sentenciaSQL, true))
            {
                SqlDataReader reader = conexion.Reader;
                while (reader.Read())
                {
                    detallePedidos.Add(new DetallePedido
                    {
                        idDetallePedido = reader.GetInt32(0),
                        idPedido = reader.GetInt32(1),
                        idMaterial = reader.GetInt32(2),
                        cantidad = reader.GetInt32(3)
                    });
                }
                conexion.CerrarConexion();
            }
            else
            {
                throw new Exception("Error al listar detalles de pedido: " + conexion.Error);
            }
            return detallePedidos;
        }

        public int RegistrarDetallePedido(DetallePedido detallePedido)
        {
            int idDetallePedidoInsertado = 0;
            string sentenciaSQL = "usp_RegistrarDetallePedido";

            conexion.AgregarParametro(ParameterDirection.Output, "@idDetallePedidoOut", SqlDbType.Int, 0, idDetallePedidoInsertado);
            conexion.AgregarParametro(ParameterDirection.Input, "@idPedido", SqlDbType.Int, 0, detallePedido.idPedido);
            conexion.AgregarParametro(ParameterDirection.Input, "@idMaterial", SqlDbType.Int, 0, detallePedido.idMaterial);
            conexion.AgregarParametro(ParameterDirection.Input, "@cantidad", SqlDbType.Int, 0, detallePedido.cantidad);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                idDetallePedidoInsertado = Convert.ToInt32(conexion.ValorUnico);
            }
            else
            {
                throw new Exception("Error al registrar detalle de pedido: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return idDetallePedidoInsertado;
        }

        public bool ActualizarDetallePedido(DetallePedido detallePedido)
        {
            bool actualizado = false;
            string sentenciaSQL = "usp_ActualizarDetallePedido";

            conexion.AgregarParametro(ParameterDirection.Input, "@idDetallePedido", SqlDbType.Int, 0, detallePedido.idDetallePedido);
            conexion.AgregarParametro(ParameterDirection.Input, "@idPedido", SqlDbType.Int, 0, detallePedido.idPedido);
            conexion.AgregarParametro(ParameterDirection.Input, "@idMaterial", SqlDbType.Int, 0, detallePedido.idMaterial);
            conexion.AgregarParametro(ParameterDirection.Input, "@cantidad", SqlDbType.Int, 0, detallePedido.cantidad);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                actualizado = true;
            }
            else
            {
                throw new Exception("Error al actualizar detalle de pedido: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return actualizado;
        }

        public bool EliminarDetallePedido(int idDetallePedido)
        {
            bool eliminado = false;
            string sentenciaSQL = "usp_EliminarDetallePedido";
            conexion.AgregarParametro(ParameterDirection.Input, "@idDetallePedido", SqlDbType.Int, 0, idDetallePedido);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                eliminado = true;
            }
            else
            {
                throw new Exception("Error al eliminar detalle de pedido: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return eliminado;
        }
    }
}