using EduDataAPI.Data;
using EduDataAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EduDataAPI.Repositories
{
    public class ProfesorRepository
    {
        private readonly Database _database;

        public ProfesorRepository(Database database)
        {
            _database = database;
        }

        public List<Profesor> LeerProfesores()
        {
            var lista = new List<Profesor>();

            using var connection = _database.GetConnection();
            using var command = new SqlCommand("sp_LeerProfesores", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Profesor
                {
                    IdProfesor = Convert.ToInt32(reader["IdProfesor"]),
                    Nombre = reader["Nombre"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    Especialidad = reader["Especialidad"].ToString()!,
                    Activo = Convert.ToBoolean(reader["Activo"]),
                    Fecha_Creacion = Convert.ToDateTime(reader["Fecha_Creacion"]),
                    Creado_Por = reader["Creado_Por"].ToString()!,
                    Fecha_Modificacion = reader["Fecha_Modificacion"] == DBNull.Value ? null : Convert.ToDateTime(reader["Fecha_Modificacion"]),
                    Modificado_Por = reader["Modificado_Por"] == DBNull.Value ? null : reader["Modificado_Por"].ToString()
                });
            }

            return lista;
        }

        public Profesor? BuscarProfesorPorId(int id)
        {
            Profesor? profesor = null;

            using var connection = _database.GetConnection();
            using var command = new SqlCommand("sp_BuscarProfesorPorId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdProfesor", id);

            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                profesor = new Profesor
                {
                    IdProfesor = Convert.ToInt32(reader["IdProfesor"]),
                    Nombre = reader["Nombre"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    Especialidad = reader["Especialidad"].ToString()!,
                    Activo = Convert.ToBoolean(reader["Activo"]),
                    Fecha_Creacion = Convert.ToDateTime(reader["Fecha_Creacion"]),
                    Creado_Por = reader["Creado_Por"].ToString()!,
                    Fecha_Modificacion = reader["Fecha_Modificacion"] == DBNull.Value ? null : Convert.ToDateTime(reader["Fecha_Modificacion"]),
                    Modificado_Por = reader["Modificado_Por"] == DBNull.Value ? null : reader["Modificado_Por"].ToString()
                };
            }

            return profesor;
        }

        public void CrearProfesor(Profesor profesor)
        {
            using var connection = _database.GetConnection();
            using var command = new SqlCommand("sp_CrearProfesor", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Nombre", profesor.Nombre);
            command.Parameters.AddWithValue("@Email", profesor.Email);
            command.Parameters.AddWithValue("@Especialidad", profesor.Especialidad);
            command.Parameters.AddWithValue("@Creado_Por", profesor.Creado_Por);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void ModificarProfesor(int id, Profesor profesor)
        {
            using var connection = _database.GetConnection();
            using var command = new SqlCommand("sp_ModificarProfesor", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@IdProfesor", id);
            command.Parameters.AddWithValue("@Nombre", profesor.Nombre);
            command.Parameters.AddWithValue("@Email", profesor.Email);
            command.Parameters.AddWithValue("@Especialidad", profesor.Especialidad);
            command.Parameters.AddWithValue("@Modificado_Por", profesor.Modificado_Por ?? "Sistema");

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void EliminarProfesor(int id)
        {
            using var connection = _database.GetConnection();
            using var command = new SqlCommand("sp_EliminarProfesor", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@IdProfesor", id);
            command.Parameters.AddWithValue("@Modificado_Por", "Sistema");

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}