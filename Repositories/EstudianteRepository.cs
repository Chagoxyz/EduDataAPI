using EduDataAPI.Data;
using EduDataAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EduDataAPI.Repositories
{
    public class EstudianteRepository
    {
        private readonly Database _database;

        public EstudianteRepository(Database database)
        {
            _database = database;
        }

        public List<Estudiante> LeerEstudiantes()
        {
            var lista = new List<Estudiante>();

            using var connection = _database.GetConnection();
            using var command = new SqlCommand("sp_LeerEstudiantes", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Estudiante
                {
                    IdEstudiante = Convert.ToInt32(reader["IdEstudiante"]),
                    Nombre = reader["Nombre"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    Telefono = reader["Telefono"].ToString()!,
                    Activo = Convert.ToBoolean(reader["Activo"]),
                    Fecha_Creacion = Convert.ToDateTime(reader["Fecha_Creacion"]),
                    Creado_Por = reader["Creado_Por"].ToString()!,
                    Fecha_Modificacion = reader["Fecha_Modificacion"] == DBNull.Value ? null : Convert.ToDateTime(reader["Fecha_Modificacion"]),
                    Modificado_Por = reader["Modificado_Por"] == DBNull.Value ? null : reader["Modificado_Por"].ToString()
                });
            }

            return lista;
        }

        public Estudiante? BuscarEstudiantePorId(int id)
        {
            Estudiante? estudiante = null;

            using var connection = _database.GetConnection();
            using var command = new SqlCommand("sp_BuscarEstudiantePorId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdEstudiante", id);

            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                estudiante = new Estudiante
                {
                    IdEstudiante = Convert.ToInt32(reader["IdEstudiante"]),
                    Nombre = reader["Nombre"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    Telefono = reader["Telefono"].ToString()!,
                    Activo = Convert.ToBoolean(reader["Activo"]),
                    Fecha_Creacion = Convert.ToDateTime(reader["Fecha_Creacion"]),
                    Creado_Por = reader["Creado_Por"].ToString()!,
                    Fecha_Modificacion = reader["Fecha_Modificacion"] == DBNull.Value ? null : Convert.ToDateTime(reader["Fecha_Modificacion"]),
                    Modificado_Por = reader["Modificado_Por"] == DBNull.Value ? null : reader["Modificado_Por"].ToString()
                };
            }

            return estudiante;
        }

        public void CrearEstudiante(Estudiante estudiante)
        {
            using var connection = _database.GetConnection();
            using var command = new SqlCommand("sp_CrearEstudiante", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Nombre", estudiante.Nombre);
            command.Parameters.AddWithValue("@Email", estudiante.Email);
            command.Parameters.AddWithValue("@Telefono", estudiante.Telefono);
            command.Parameters.AddWithValue("@Creado_Por", estudiante.Creado_Por);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void ModificarEstudiante(int id, Estudiante estudiante)
        {
            using var connection = _database.GetConnection();
            using var command = new SqlCommand("sp_ModificarEstudiante", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@IdEstudiante", id);
            command.Parameters.AddWithValue("@Nombre", estudiante.Nombre);
            command.Parameters.AddWithValue("@Email", estudiante.Email);
            command.Parameters.AddWithValue("@Telefono", estudiante.Telefono);
            command.Parameters.AddWithValue("@Modificado_Por", estudiante.Modificado_Por ?? "Sistema");

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void EliminarEstudiante(int id)
        {
            using var connection = _database.GetConnection();
            using var command = new SqlCommand("sp_EliminarEstudiante", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@IdEstudiante", id);
            command.Parameters.AddWithValue("@Modificado_Por", "Sistema");

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}