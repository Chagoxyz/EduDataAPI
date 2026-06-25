namespace EduDataAPI.Models
{
    public class EstudianteCurso
    {
        public int IdEstudianteCurso { get; set; }
        public int IdEstudiante { get; set; }
        public string? NombreEstudiante { get; set; }
        public int IdCurso { get; set; }
        public string? NombreCurso { get; set; }
        public DateTime Fecha_Inscripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public string Creado_Por { get; set; } = string.Empty;
        public DateTime? Fecha_Modificacion { get; set; }
        public string? Modificado_Por { get; set; }
    }
}