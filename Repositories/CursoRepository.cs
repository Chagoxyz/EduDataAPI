using EduDataAPI.Data;
using EduDataAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EduDataAPI.Repositories
{
    public class CursoRepository
    {
        private readonly Database _database;

        public CursoRepository(Database database)
        {
            _database = database;
        }

        public List<Curso> LeerCursos()
        {
            var lista = new List<Curso>();

            using var connection = _database.GetConnection();
            using var command = new SqlCommand("sp_LeerCursos", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Curso
                {
                    IdCurso = Convert.ToInt32(reader["IdCurso"]),
                    Nombre = reader["Nombre"].ToString()!,
                    Descripcion = reader["Descripcion"].ToString()!,
                    IdProfesor = Convert.ToInt32(reader["IdProfesor"]),
                    NombreProfesor = reader["NombreProfesor"].ToString(),
                    Activo = Convert.ToBoolean(reader["Activo"]),
                    Fecha_Creacion = Convert.ToDateTime(reader["Fecha_Creacion"]),
                    Creado_Por = reader["Creado_Por"].ToString()!,
                    Fecha_Modificacion = reader["Fecha_Modificacion"] == DBNull.Value ? null : Convert.ToDateTime(reader["Fecha_Modificacion"]),
                    Modificado_Por = reader["Modificado_Por"] == DBNull.Value ? null : reader["Modificado_Por"].ToString()
                });
            }

            return lista;
        }

        public Curso? BuscarCursoPorId(int id)
        {
            Curso? curso = null;

            using var connection = _database.GetConnection();
            using var command = new SqlCommand("sp_BuscarCursoPorId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdCurso", id);

            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                curso = new Curso
                {
                    IdCurso = Convert.ToInt32(reader["IdCurso"]),
                    Nombre = reader["Nombre"].ToString()!,
                    Descripcion = reader["Descripcion"].ToString()!,
                    IdProfesor = Convert.ToInt32(reader["IdProfesor"]),
                    NombreProfesor = reader["NombreProfesor"].ToString(),
                    Activo = Convert.ToBoolean(reader["Activo"]),
                    Fecha_Creacion = Convert.ToDateTime(reader["Fecha_Creacion"]),
                    Creado_Por = reader["Creado_Por"].ToString()!,
                    Fecha_Modificacion = reader["Fecha_Modificacion"] == DBNull.Value ? null : Convert.ToDateTime(reader["Fecha_Modificacion"]),
                    Modificado_Por = reader["Modificado_Por"] == DBNull.Value ? null : reader["Modificado_Por"].ToString()
                };
            }

            return curso;
        }

        public void CrearCurso(Curso curso)
        {
            using var connection = _database.GetConnection();
            using var command = new SqlCommand("sp_CrearCurso", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Nombre", curso.Nombre);
            command.Parameters.AddWithValue("@Descripcion", curso.Descripcion);
            command.Parameters.AddWithValue("@IdProfesor", curso.IdProfesor);
            command.Parameters.AddWithValue("@Creado_Por", curso.Creado_Por);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void ModificarCurso(int id, Curso curso)
        {
            using var connection = _database.GetConnection();
            using var command = new SqlCommand("sp_ModificarCurso", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@IdCurso", id);
            command.Parameters.AddWithValue("@Nombre", curso.Nombre);
            command.Parameters.AddWithValue("@Descripcion", curso.Descripcion);
            command.Parameters.AddWithValue("@IdProfesor", curso.IdProfesor);
            command.Parameters.AddWithValue("@Modificado_Por", curso.Modificado_Por ?? "Sistema");

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void EliminarCurso(int id)
        {
            using var connection = _database.GetConnection();
            using var command = new SqlCommand("sp_EliminarCurso", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@IdCurso", id);
            command.Parameters.AddWithValue("@Modificado_Por", "Sistema");

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}