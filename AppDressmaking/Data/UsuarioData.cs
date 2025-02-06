using AppDressmaking.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace AppDressmaking.Data
{
    public class UsuarioData
    {
        // Declaración conexion
        private ConexionBD conexion;

        public UsuarioData()
        {
            // Inicio variable conexion
            conexion = new ConexionBD();
        }

        public Usuario ObtenerUsuarioPorId(int idUsuario)
        {
            Usuario usuario = null;
            string sentenciaSQL = "usp_ConsultarUsuarioPorId";
            conexion.AgregarParametro(ParameterDirection.Input, "@idUsuario", SqlDbType.Int, 0, idUsuario);
            if (conexion.Consultar(sentenciaSQL, true))
            {
                SqlDataReader reader = conexion.Reader;
                if (reader.Read())
                {
                    usuario = new Usuario
                    {
                        idUsuario = reader.GetInt32(0),
                        nombreUsuario = reader.GetString(1),
                        tipoUsuario = reader.GetString(2)[0],
                        FechaIngreso = reader.GetDateTime(3)
                    };
                }
                conexion.CerrarConexion();
            }
            else
            {
                throw new Exception("Error al obtener usuario: " + conexion.Error);
            }
            return usuario;
        }

        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            string sentenciaSQL = "usp_ListarUsuarios";
            if (conexion.Consultar(sentenciaSQL, true))
            {
                SqlDataReader reader = conexion.Reader;
                while (reader.Read())
                {
                    usuarios.Add(new Usuario
                    {
                        idUsuario = reader.GetInt32(0),
                        nombreUsuario = reader.GetString(1),
                        contrasena = reader.GetString(1), //Agregado ultimo
                        tipoUsuario = reader.GetString(2)[0],
                        FechaIngreso = reader.GetDateTime(3)
                    });
                }
                conexion.CerrarConexion();
            }
            else
            {
                throw new Exception("Error al listar usuarios: " + conexion.Error);
            }
            return usuarios;
        }

        public int RegistrarUsuario(Usuario usuario)
        {
            int idUsuarioInsertado = 0;
            string sentenciaSQL = "usp_RegistrarUsuario";

            conexion.AgregarParametro(ParameterDirection.Output, "@idUsuarioOut", SqlDbType.Int, 0, idUsuarioInsertado);
            conexion.AgregarParametro(ParameterDirection.Input, "@nombreUsuario", SqlDbType.VarChar, 50, usuario.nombreUsuario);
            conexion.AgregarParametro(ParameterDirection.Input, "@contrasena", SqlDbType.VarChar, 50, usuario.contrasena);
            conexion.AgregarParametro(ParameterDirection.Input, "@tipoUsuario", SqlDbType.Char, 1, usuario.tipoUsuario);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                idUsuarioInsertado = Convert.ToInt32(conexion.ValorUnico);
            }
            else
            {
                throw new Exception("Error al registrar usuario: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return idUsuarioInsertado;
        }

        public bool ActualizarUsuario(Usuario usuario)
        {
            bool actualizado = false;
            string sentenciaSQL = "usp_ActualizarUsuario";

            conexion.AgregarParametro(ParameterDirection.Input, "@idUsuario", SqlDbType.Int, 0, usuario.idUsuario);
            conexion.AgregarParametro(ParameterDirection.Input, "@nombreUsuario", SqlDbType.VarChar, 50, usuario.nombreUsuario);
            conexion.AgregarParametro(ParameterDirection.Input, "@contrasena", SqlDbType.VarChar, 50, usuario.contrasena);
            conexion.AgregarParametro(ParameterDirection.Input, "@tipoUsuario", SqlDbType.Char, 1, usuario.tipoUsuario);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                actualizado = true;
            }
            else
            {
                throw new Exception("Error al actualizar usuario: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return actualizado;
        }

        public bool EliminarUsuario(int idUsuario)
        {
            bool eliminado = false;
            string sentenciaSQL = "usp_EliminarUsuario";
            conexion.AgregarParametro(ParameterDirection.Input, "@idUsuario", SqlDbType.Int, 0, idUsuario);

            if (conexion.EjecutarSentencia(sentenciaSQL, true))
            {
                eliminado = true;
            }
            else
            {
                throw new Exception("Error al eliminar usuario: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return eliminado;
        }
        public Administrador ObtenerAdministradorPorIdUsuario(int idUsuario)
        {
            Administrador administrador = null;
            string sentenciaSQL = "usp_ObtenerAdministradorPorIdUsuario"; // Usando el SP
            conexion.AgregarParametro(ParameterDirection.Input, "@idUsuario", SqlDbType.Int, 0, idUsuario);
            if (conexion.Consultar(sentenciaSQL, true)) // true porque usa un SP
            {
                SqlDataReader reader = conexion.Reader;
                if (reader.Read())
                {
                    administrador = new Administrador
                    {
                        idAdministrador = reader.GetInt32(0),
                        idUsuario = reader.GetInt32(1),
                        nivelAcceso = reader.GetInt32(2)
                    };
                }
                conexion.CerrarConexion();
            }
            else
            {
                throw new Exception("Error al obtener administrador: " + conexion.Error);
            }
            return administrador;
        }
        public List<Administrador> ListarAdministradores()
        {
            List<Administrador> administradores = new List<Administrador>();
            string sentenciaSQL = "usp_ListarAdministradores"; // Usando el SP
            if (conexion.Consultar(sentenciaSQL, true)) // true porque usa un SP
            {
                SqlDataReader reader = conexion.Reader;
                while (reader.Read())
                {
                    administradores.Add(new Administrador
                    {
                        idAdministrador = reader.GetInt32(0),
                        idUsuario = reader.GetInt32(1),
                        nivelAcceso = reader.GetInt32(2)
                    });
                }
                conexion.CerrarConexion();
            }
            else
            {
                throw new Exception("Error al listar administradores: " + conexion.Error);
            }
            return administradores;
        }
        public int RegistrarAdministrador(Administrador administrador)
        {
            int idAdministradorInsertado = 0;
            string sentenciaSQL = "usp_RegistrarAdministrador"; // Usando el SP

            conexion.AgregarParametro(ParameterDirection.Output, "@idAdministradorOut", SqlDbType.Int, 0, idAdministradorInsertado);
            conexion.AgregarParametro(ParameterDirection.Input, "@idUsuario", SqlDbType.Int, 0, administrador.idUsuario);
            conexion.AgregarParametro(ParameterDirection.Input, "@nivelAcceso", SqlDbType.Int, 0, administrador.nivelAcceso);

            if (conexion.EjecutarSentencia(sentenciaSQL, true)) // true porque usa un SP
            {
                idAdministradorInsertado = Convert.ToInt32(conexion.ValorUnico);
            }
            else
            {
                throw new Exception("Error al registrar administrador: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return idAdministradorInsertado;
        }
        public bool ActualizarAdministrador(Administrador administrador)
        {
            bool actualizado = false;
            string sentenciaSQL = "usp_ActualizarAdministrador"; // Usando el SP

            conexion.AgregarParametro(ParameterDirection.Input, "@idAdministrador", SqlDbType.Int, 0, administrador.idAdministrador);
            conexion.AgregarParametro(ParameterDirection.Input, "@idUsuario", SqlDbType.Int, 0, administrador.idUsuario);
            conexion.AgregarParametro(ParameterDirection.Input, "@nivelAcceso", SqlDbType.Int, 0, administrador.nivelAcceso);

            if (conexion.EjecutarSentencia(sentenciaSQL, true)) // true porque usa un SP
            {
                actualizado = true;
            }
            else
            {
                throw new Exception("Error al actualizar administrador: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return actualizado;
        }
        public bool EliminarAdministrador(int idAdministrador)
        {
            bool eliminado = false;
            string sentenciaSQL = "usp_EliminarAdministrador"; // Usando el SP
            conexion.AgregarParametro(ParameterDirection.Input, "@idAdministrador", SqlDbType.Int, 0, idAdministrador);

            if (conexion.EjecutarSentencia(sentenciaSQL, true)) // true porque usa un SP
            {
                eliminado = true;
            }
            else
            {
                throw new Exception("Error al eliminar administrador: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return eliminado;
        }
        public Empleado ObtenerEmpleadoPorIdUsuario(int idUsuario)
        {
            Empleado empleado = null;
            string sentenciaSQL = "usp_ObtenerEmpleadoPorIdUsuario"; // Usando el SP
            conexion.AgregarParametro(ParameterDirection.Input, "@idUsuario", SqlDbType.Int, 0, idUsuario);
            if (conexion.Consultar(sentenciaSQL, true)) // true porque usa un SP
            {
                SqlDataReader reader = conexion.Reader;
                if (reader.Read())
                {
                    empleado = new Empleado
                    {
                        idEmpleado = reader.GetInt32(0),
                        idUsuario = reader.GetInt32(1),
                        departamento = reader.GetString(2)
                    };
                }
                conexion.CerrarConexion();
            }
            else
            {
                throw new Exception("Error al obtener empleado: " + conexion.Error);
            }
            return empleado;
        }
        public List<Empleado> ListarEmpleados()
        {
            List<Empleado> empleados = new List<Empleado>();
            string sentenciaSQL = "usp_ListarEmpleados"; // Usando el SP
            if (conexion.Consultar(sentenciaSQL, true)) // true porque usa un SP
            {
                SqlDataReader reader = conexion.Reader;
                while (reader.Read())
                {
                    empleados.Add(new Empleado
                    {
                        idEmpleado = reader.GetInt32(0),
                        idUsuario = reader.GetInt32(1),
                        departamento = reader.GetString(2)
                    });
                }
                conexion.CerrarConexion();
            }
            else
            {
                throw new Exception("Error al listar empleados: " + conexion.Error);
            }
            return empleados;
        }
        public int RegistrarEmpleado(Empleado empleado)
        {
            int idEmpleadoInsertado = 0;
            string sentenciaSQL = "usp_RegistrarEmpleado"; // Usando el SP

            conexion.AgregarParametro(ParameterDirection.Output, "@idEmpleadoOut", SqlDbType.Int, 0, idEmpleadoInsertado);
            conexion.AgregarParametro(ParameterDirection.Input, "@idUsuario", SqlDbType.Int, 0, empleado.idUsuario);
            conexion.AgregarParametro(ParameterDirection.Input, "@departamento", SqlDbType.VarChar, 50, empleado.departamento);

            if (conexion.EjecutarSentencia(sentenciaSQL, true)) // true porque usa un SP
            {
                idEmpleadoInsertado = Convert.ToInt32(conexion.ValorUnico);
            }
            else
            {
                throw new Exception("Error al registrar empleado: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return idEmpleadoInsertado;
        }
        public bool ActualizarEmpleado(Empleado empleado)
        {
            bool actualizado = false;
            string sentenciaSQL = "usp_ActualizarEmpleado"; // Usando el SP

            conexion.AgregarParametro(ParameterDirection.Input, "@idEmpleado", SqlDbType.Int, 0, empleado.idEmpleado);
            conexion.AgregarParametro(ParameterDirection.Input, "@idUsuario", SqlDbType.Int, 0, empleado.idUsuario);
            conexion.AgregarParametro(ParameterDirection.Input, "@departamento", SqlDbType.VarChar, 50, empleado.departamento);

            if (conexion.EjecutarSentencia(sentenciaSQL, true)) // true porque usa un SP
            {
                actualizado = true;
            }
            else
            {
                throw new Exception("Error al actualizar empleado: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return actualizado;
        }
        public bool EliminarEmpleado(int idEmpleado)
        {
            bool eliminado = false;
            string sentenciaSQL = "usp_EliminarEmpleado"; // Usando el SP
            conexion.AgregarParametro(ParameterDirection.Input, "@idEmpleado", SqlDbType.Int, 0, idEmpleado);

            if (conexion.EjecutarSentencia(sentenciaSQL, true)) // true porque usa un SP
            {
                eliminado = true;
            }
            else
            {
                throw new Exception("Error al eliminar empleado: " + conexion.Error);
            }
            conexion.CerrarConexion();
            return eliminado;
        }

    }
}