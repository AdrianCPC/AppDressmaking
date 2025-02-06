using AppDressmaking.Models;
using AppDressmaking.Data;
using System.Collections.Generic;
using System.Web.Http;

namespace AppDressmaking.Controllers
{
    public class UsuarioController : ApiController
    {
        private UsuarioData usuarioData = new UsuarioData(); // Instancia  capa de datos

        // GET api/usuario
        public List<Usuario> Get()
        {
            return usuarioData.ListarUsuarios();
        }

        // GET api/usuario/5  (o se puede usar query parameters: api/usuario?id=5)
        public Usuario Get(int id) // Mejor  int para el ID
        {
            return usuarioData.ObtenerUsuarioPorId(id);
        }

        // POST api/usuario
        public int Post([FromBody] Usuario usuario) // Devuelve el ID del usuario creado
        {
            return usuarioData.RegistrarUsuario(usuario);
        }

        // PUT api/usuario/5 (o usar el objeto completo en el body)
        public bool Put([FromBody] Usuario usuario) // Recibe el usuario completo en el body
        {
            return usuarioData.ActualizarUsuario(usuario);
        }

        // DELETE api/usuario/5
        public bool Delete(int id) // Mejor usar int para el ID
        {
            return usuarioData.EliminarUsuario(id);
        }


        // Métodos para Administrador
        // GET api/usuario/administrador/5 (Obtener administrador por idUsuario)
        [HttpGet] // Especifica el verbo HTTP para mayor claridad
        [Route("api/usuario/administrador/{idUsuario}")] // Define la ruta específica
        public Administrador GetAdministrador(int idUsuario)
        {
            return usuarioData.ObtenerAdministradorPorIdUsuario(idUsuario);
        }

        // POST api/usuario/administrador
        [HttpPost] // Especifica el verbo HTTP para mayor claridad
        [Route("api/usuario/administrador")] // Define la ruta específica
        public int PostAdministrador([FromBody] Administrador administrador)
        {
            return usuarioData.RegistrarAdministrador(administrador);
        }

        // PUT api/usuario/administrador
        [HttpPut] // Especifica el verbo HTTP para mayor claridad
        [Route("api/usuario/administrador")] // Define la ruta específica
        public bool PutAdministrador([FromBody] Administrador administrador)
        {
            return usuarioData.ActualizarAdministrador(administrador);
        }

        // DELETE api/usuario/administrador/5
        [HttpDelete] // Especifica el verbo HTTP para mayor claridad
        [Route("api/usuario/administrador/{idAdministrador}")] // Define la ruta específica
        public bool DeleteAdministrador(int idAdministrador)
        {
            return usuarioData.EliminarAdministrador(idAdministrador);
        }

        // Métodos para Empleado (similar a Administrador)
        [HttpGet]
        [Route("api/usuario/empleado/{idUsuario}")]
        public Empleado GetEmpleado(int idUsuario)
        {
            return usuarioData.ObtenerEmpleadoPorIdUsuario(idUsuario);
        }

        [HttpPost]
        [Route("api/usuario/empleado")]
        public int PostEmpleado([FromBody] Empleado empleado)
        {
            return usuarioData.RegistrarEmpleado(empleado);
        }

        [HttpPut]
        [Route("api/usuario/empleado")]
        public bool PutEmpleado([FromBody] Empleado empleado)
        {
            return usuarioData.ActualizarEmpleado(empleado);
        }

        [HttpDelete]
        [Route("api/usuario/empleado/{idEmpleado}")]
        public bool DeleteEmpleado(int idEmpleado)
        {
            return usuarioData.EliminarEmpleado(idEmpleado);
        }
    }
}