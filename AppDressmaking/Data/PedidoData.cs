using AppDressmaking.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace AppDressmaking.Data
{
    public class PedidoData
    {
        private ConexionBD conexion;

        public PedidoData()
        {
            conexion = new ConexionBD();
        }

        public Pedido ObtenerPedidoPorId(int idPedido)
        {
            Pedido pedido = null;
            string sentenciaSQL = "usp_ConsultarPedidoPorId";
            conexion.AgregarParametro(ParameterDirection.Input, "@idPedido", SqlDbType.Int, 0, idPedido);
            if (conexion.Consultar(sentenciaSQL, true))
            {
                SqlDataReader reader = conexion.Reader;
                if (reader.Read())
                {
                    pedido = new Pedido
                    {
                        idPedido = reader.GetInt32(0),
                        fecha = reader.GetDateTime(1),
                        estado = reader.GetString(2),
                        cantidad = reader.GetInt32(3),
                        total = reader.GetDecimal(4),
                        idCliente = reader.GetInt32(5)
                    };
                }
                conexion.CerrarConexion();
            }
            else
            {
                throw new Exception("Error al obtener pedido: " + conexion.Error);
            }
            return pedido;
        }

        public List<Pedido> ListarPedidos()
        {
            List<Pedido> pedidos = new List<Pedido>();
            string sentenciaSQL = "usp_ListarPedidos";
            if (conexion.Consultar(sentenciaSQL, true))
            {
                SqlDataReader reader = conexion.Reader;
                while (reader.Read())
                {
                    pedidos.Add(new Pedido
                    {
                        idPedido = reader.GetInt32(0),
                        fecha = reader.GetDateTime(1),
                        estado = reader.GetString(2),
                        cantidad = reader.GetInt32(3),
                        total = reader.GetDecimal(4),
                        idCliente = reader.GetInt32(5)
                    });
                }
                conexion.CerrarConexion();
            }
            else
            {
                throw new Exception("Error al listar pedidos: " + conexion.Error);
            }
            return pedidos;
        }

        public int RegistrarPedido(Pedido pedido)
        {
            int idPedidoInsertado = 0;
            string sentenciaSQL = "usp_RegistrarPedido";

            conexion.AgregarParametro(ParameterDirection.Output, "@idPedidoOut", SqlDbType.Int, 0, idPedidoInsertado);
            conexion.AgregarParametro(ParameterDirection.Input, "@fecha", SqlDbType.Date, 0, pedido.fecha);
            conexion.AgregarParametro(ParameterDirection.Input, "@estado", SqlDbType.VarChar, 20, pedido.estado);
            conexion.AgregarParametro(ParameterDirection.Input, "@cantidad", SqlDbType.Int, 0, pedido.cantidad);
            conexion.AgregarParametro(ParameterDirection.Input, "@total", SqlDbType.Decimal, 0, pedido.total);
            conexion.AgregarParametro(ParameterDirection.Input, "@idCliente", SqlDbType.Int, 0, pedido.idCliente);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                idPedidoInsertado = Convert.ToInt32(conexion.ValorUnico);
            }
            else
            {
                throw new Exception("Error al registrar pedido: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return idPedidoInsertado;
        }

        public bool ActualizarPedido(Pedido pedido)
        {
            bool actualizado = false;
            string sentenciaSQL = "usp_ActualizarPedido";

            conexion.AgregarParametro(ParameterDirection.Input, "@idPedido", SqlDbType.Int, 0, pedido.idPedido);
            conexion.AgregarParametro(ParameterDirection.Input, "@fecha", SqlDbType.Date, 0, pedido.fecha);
            conexion.AgregarParametro(ParameterDirection.Input, "@estado", SqlDbType.VarChar, 20, pedido.estado);
            conexion.AgregarParametro(ParameterDirection.Input, "@cantidad", SqlDbType.Int, 0, pedido.cantidad);
            conexion.AgregarParametro(ParameterDirection.Input, "@total", SqlDbType.Decimal, 0, pedido.total);
            conexion.AgregarParametro(ParameterDirection.Input, "@idCliente", SqlDbType.Int, 0, pedido.idCliente);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                actualizado = true;
            }
            else
            {
                throw new Exception("Error al actualizar pedido: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return actualizado;
        }

        public bool EliminarPedido(int idPedido)
        {
            bool eliminado = false;
            string sentenciaSQL = "usp_EliminarPedido";
            conexion.AgregarParametro(ParameterDirection.Input, "@idPedido", SqlDbType.Int, 0, idPedido);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                eliminado = true;
            }
            else
            {
                throw new Exception("Error al eliminar pedido: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return eliminado;
        }
    }
}