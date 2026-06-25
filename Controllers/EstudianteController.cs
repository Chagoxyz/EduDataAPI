using EduDataAPI.Models;
using EduDataAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EduDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly EstudianteRepository _repository;

        public EstudianteController(EstudianteRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult LeerEstudiantes()
        {
            try
            {
                return Ok(_repository.LeerEstudiantes());
            }
            catch (SqlException)
            {
                return StatusCode(500, "Error al consultar los estudiantes en la base de datos.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult BuscarEstudiantePorId(int id)
        {
            try
            {
                var estudiante = _repository.BuscarEstudiantePorId(id);

                if (estudiante == null)
                {
                    return NotFound("No se encontró el estudiante solicitado.");
                }

                return Ok(estudiante);
            }
            catch (SqlException)
            {
                return StatusCode(500, "Error al buscar el estudiante en la base de datos.");
            }
        }

        [HttpPost]
        public IActionResult CrearEstudiante([FromBody] Estudiante estudiante)
        {
            try
            {
                _repository.CrearEstudiante(estudiante);
                return Ok("Estudiante creado correctamente.");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Error al crear el estudiante. Verifique que el email no esté repetido.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult ModificarEstudiante(int id, [FromBody] Estudiante estudiante)
        {
            try
            {
                _repository.ModificarEstudiante(id, estudiante);
                return Ok("Estudiante modificado correctamente.");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Error al modificar el estudiante.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarEstudiante(int id)
        {
            try
            {
                _repository.EliminarEstudiante(id);
                return Ok("Estudiante eliminado lógicamente.");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Error al eliminar el estudiante.");
            }
        }
    }
}