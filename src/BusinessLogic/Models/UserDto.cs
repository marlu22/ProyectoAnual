namespace BusinessLogic.Models
{
    public class UserDto
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; } = null!;
        public string? NombreCompleto { get; set; }
        public string? Rol { get; set; }
        public int IdRol { get; set; }
        public bool CambioContrasenaObligatorio { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public bool Habilitado { get; set; }
    }
}