namespace EduDataAPI.Models
{
    public class Estudiante
    {
        public int IdEstudiante { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public string Creado_Por { get; set; } = string.Empty;
        public DateTime? Fecha_Modificacion { get; set; }
        public string? Modificado_Por { get; set; }
    }
}