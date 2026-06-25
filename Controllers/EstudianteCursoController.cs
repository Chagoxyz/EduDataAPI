using EduDataAPI.Models;
using EduDataAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EduDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteCursoController : ControllerBase
    {
        private readonly EstudianteCursoRepository _repository;

        public EstudianteCursoController(EstudianteCursoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult LeerEstudianteCursos()
        {
            try
            {
                return Ok(_repository.LeerEstudianteCursos());
            }
            catch (SqlException)
            {
                return StatusCode(500, "Error al consultar las inscripciones en la base de datos.");
            }
        }

        [HttpPost]
        public IActionResult InscribirEstudianteCurso([FromBody] EstudianteCurso estudianteCurso)
        {
            try
            {
                _repository.InscribirEstudianteCurso(estudianteCurso);
                return Ok("Estudiante inscrito correctamente en el curso.");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Error al inscribir el estudiante. Verifique que el estudiante y el curso existan.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarEstudianteCurso(int id)
        {
            try
            {
                _repository.EliminarEstudianteCurso(id);
                return Ok("Inscripción eliminada lógicamente.");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Error al eliminar la inscripción.");
            }
        }
    }
}