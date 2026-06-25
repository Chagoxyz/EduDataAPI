using EduDataAPI.Models;
using EduDataAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EduDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly CursoRepository _repository;

        public CursoController(CursoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult LeerCursos()
        {
            try
            {
                return Ok(_repository.LeerCursos());
            }
            catch (SqlException)
            {
                return StatusCode(500, "Error al consultar los cursos en la base de datos.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult BuscarCursoPorId(int id)
        {
            try
            {
                var curso = _repository.BuscarCursoPorId(id);

                if (curso == null)
                {
                    return NotFound("No se encontró el curso solicitado.");
                }

                return Ok(curso);
            }
            catch (SqlException)
            {
                return StatusCode(500, "Error al buscar el curso en la base de datos.");
            }
        }

        [HttpPost]
        public IActionResult CrearCurso([FromBody] Curso curso)
        {
            try
            {
                _repository.CrearCurso(curso);
                return Ok("Curso creado correctamente.");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Error al crear el curso. Verifique que el profesor exista.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult ModificarCurso(int id, [FromBody] Curso curso)
        {
            try
            {
                _repository.ModificarCurso(id, curso);
                return Ok("Curso modificado correctamente.");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Error al modificar el curso.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarCurso(int id)
        {
            try
            {
                _repository.EliminarCurso(id);
                return Ok("Curso eliminado lógicamente.");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Error al eliminar el curso.");
            }
        }
    }
}