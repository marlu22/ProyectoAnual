namespace BusinessLogic.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public bool CambioContrasenaObligatorio { get; set; }
        public string? Rol { get; set; }
    }
}