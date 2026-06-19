namespace EduDataAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Apodo { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public bool Estado { get; set; }
        public string Rol { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public string Creado_Por { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public string? Modificado_Por { get; set; }
    }
}