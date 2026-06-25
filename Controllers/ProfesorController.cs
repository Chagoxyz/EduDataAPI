using EduDataAPI.Models;
using EduDataAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EduDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly ProfesorRepository _repository;

        public ProfesorController(ProfesorRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult LeerProfesores()
        {
            try
            {
                return Ok(_repository.LeerProfesores());
            }
            catch (SqlException)
            {
                return StatusCode(500, "Error al consultar los profesores en la base de datos.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult BuscarProfesorPorId(int id)
        {
            try
            {
                var profesor = _repository.BuscarProfesorPorId(id);

                if (profesor == null)
                {
                    return NotFound("No se encontró el profesor solicitado.");
                }

                return Ok(profesor);
            }
            catch (SqlException)
            {
                return StatusCode(500, "Error al buscar el profesor en la base de datos.");
            }
        }

        [HttpPost]
        public IActionResult CrearProfesor([FromBody] Profesor profesor)
        {
            try
            {
                _repository.CrearProfesor(profesor);
                return Ok("Profesor creado correctamente.");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Error al crear el profesor. Verifique que el email no esté repetido.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult ModificarProfesor(int id, [FromBody] Profesor profesor)
        {
            try
            {
                _repository.ModificarProfesor(id, profesor);
                return Ok("Profesor modificado correctamente.");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Error al modificar el profesor.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarProfesor(int id)
        {
            try
            {
                _repository.EliminarProfesor(id);
                return Ok("Profesor eliminado lógicamente.");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Error al eliminar el profesor.");
            }
        }
    }
}