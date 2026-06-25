using EduDataAPI.Data;
using EduDataAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EduDataAPI.Repositories
{
    public class EstudianteCursoRepository
    {
        private readonly Database _database;

        public EstudianteCursoRepository(Database database)
        {
            _database = database;
        }

        public List<EstudianteCurso> LeerEstudianteCursos()
        {
            var lista = new List<EstudianteCurso>();

            using var connection = _database.GetConnection();
            using var command = new SqlCommand("sp_LeerEstudianteCursos", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new EstudianteCurso
                {
                    IdEstudianteCurso = Convert.ToInt32(reader["IdEstudianteCurso"]),
                    IdEstudiante = Convert.ToInt32(reader["IdEstudiante"]),
                    NombreEstudiante = reader["NombreEstudiante"].ToString(),
                    IdCurso = Convert.ToInt32(reader["IdCurso"]),
                    NombreCurso = reader["NombreCurso"].ToString(),
                    Fecha_Inscripcion = Convert.ToDateTime(reader["Fecha_Inscripcion"]),
                    Activo = Convert.ToBoolean(reader["Activo"]),
                    Fecha_Creacion = Convert.ToDateTime(reader["Fecha_Creacion"]),
                    Creado_Por = reader["Creado_Por"].ToString()!,
                    Fecha_Modificacion = reader["Fecha_Modificacion"] == DBNull.Value ? null : Convert.ToDateTime(reader["Fecha_Modificacion"]),
                    Modificado_Por = reader["Modificado_Por"] == DBNull.Value ? null : reader["Modificado_Por"].ToString()
                });
            }

            return lista;
        }

        public void InscribirEstudianteCurso(EstudianteCurso estudianteCurso)
        {
            using var connection = _database.GetConnection();
            using var command = new SqlCommand("sp_InscribirEstudianteCurso", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@IdEstudiante", estudianteCurso.IdEstudiante);
            command.Parameters.AddWithValue("@IdCurso", estudianteCurso.IdCurso);
            command.Parameters.AddWithValue("@Creado_Por", estudianteCurso.Creado_Por);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void EliminarEstudianteCurso(int id)
        {
            using var connection = _database.GetConnection();
            using var command = new SqlCommand("sp_EliminarEstudianteCurso", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@IdEstudianteCurso", id);
            command.Parameters.AddWithValue("@Modificado_Por", "Sistema");

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}