using AppDressmaking.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace AppDressmaking.Data
{
    public class ClienteData
    {
        private ConexionBD conexion;

        public ClienteData()
        {
            conexion = new ConexionBD();
        }

        public Cliente ObtenerClientePorId(int idCliente)
        {
            Cliente cliente = null;
            string sentenciaSQL = "usp_ConsultarClientePorId";
            conexion.AgregarParametro(ParameterDirection.Input, "@idCliente", SqlDbType.Int, 0, idCliente);
            if (conexion.Consultar(sentenciaSQL, true))
            {
                SqlDataReader reader = conexion.Reader;
                if (reader.Read())
                {
                    cliente = new Cliente
                    {
                        idCliente = reader.GetInt32(0),
                        nombre = reader.GetString(1),
                        direccion = reader.GetString(2),
                        telefono = reader.GetString(3),
                        email = reader.GetString(4)
                    };
                }
                conexion.CerrarConexion();
            }
            else
            {
                throw new Exception("Error al obtener cliente: " + conexion.Error);
            }
            return cliente;
        }

        public List<Cliente> ListarClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            string sentenciaSQL = "usp_ListarClientes";
            if (conexion.Consultar(sentenciaSQL, true))
            {
                SqlDataReader reader = conexion.Reader;
                while (reader.Read())
                {
                    clientes.Add(new Cliente
                    {
                        idCliente = reader.GetInt32(0),
                        nombre = reader.GetString(1),
                        direccion = reader.GetString(2),
                        telefono = reader.GetString(3),
                        email = reader.GetString(4)
                    });
                }
                conexion.CerrarConexion();
            }
            else
            {
                throw new Exception("Error al listar clientes: " + conexion.Error);
            }
            return clientes;
        }

        public int RegistrarCliente(Cliente cliente)
        {
            int idClienteInsertado = 0;
            string sentenciaSQL = "usp_RegistrarCliente";

            conexion.AgregarParametro(ParameterDirection.Output, "@idClienteOut", SqlDbType.Int, 0, idClienteInsertado);
            conexion.AgregarParametro(ParameterDirection.Input, "@nombre", SqlDbType.VarChar, 100, cliente.nombre);
            conexion.AgregarParametro(ParameterDirection.Input, "@direccion", SqlDbType.VarChar, 200, cliente.direccion);
            conexion.AgregarParametro(ParameterDirection.Input, "@telefono", SqlDbType.VarChar, 20, cliente.telefono);
            conexion.AgregarParametro(ParameterDirection.Input, "@email", SqlDbType.VarChar, 50, cliente.email);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                idClienteInsertado = Convert.ToInt32(conexion.ValorUnico);
            }
            else
            {
                throw new Exception("Error al registrar cliente: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return idClienteInsertado;
        }

        public bool ActualizarCliente(Cliente cliente)
        {
            bool actualizado = false;
            string sentenciaSQL = "usp_ActualizarCliente";

            conexion.AgregarParametro(ParameterDirection.Input, "@idCliente", SqlDbType.Int, 0, cliente.idCliente);
            conexion.AgregarParametro(ParameterDirection.Input, "@nombre", SqlDbType.VarChar, 100, cliente.nombre);
            conexion.AgregarParametro(ParameterDirection.Input, "@direccion", SqlDbType.VarChar, 200, cliente.direccion);
            conexion.AgregarParametro(ParameterDirection.Input, "@telefono", SqlDbType.VarChar, 20, cliente.telefono);
            conexion.AgregarParametro(ParameterDirection.Input, "@email", SqlDbType.VarChar, 50, cliente.email);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                actualizado = true;
            }
            else
            {
                throw new Exception("Error al actualizar cliente: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return actualizado;
        }

        public bool EliminarCliente(int idCliente)
        {
            bool eliminado = false;
            string sentenciaSQL = "usp_EliminarCliente";
            conexion.AgregarParametro(ParameterDirection.Input, "@idCliente", SqlDbType.Int, 0, idCliente);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                eliminado = true;
            }
            else
            {
                throw new Exception("Error al eliminar cliente: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return eliminado;
        }
    }
}