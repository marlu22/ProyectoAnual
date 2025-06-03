namespace BusinessLogic.Models
{
    public class UserRequest
    {
        public string PersonaId { get; set; } // O el campo clave de persona
        public string Username { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
    }
}
