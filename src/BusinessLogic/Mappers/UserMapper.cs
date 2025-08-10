using BusinessLogic.Models;
using DataAccess.Entities;

namespace BusinessLogic.Mappers
{
    public static class UserMapper
    {
        public static UserDto? MapToUserDto(Usuario? u)
        {
            if (u == null) return null;
            return new UserDto
            {
                IdUsuario = u.IdUsuario,
                Username = u.UsuarioNombre,
                NombreCompleto = u.Persona != null ? $"{u.Persona.Nombre} {u.Persona.Apellido}" : "N/A",
                Rol = u.Rol?.Nombre,
                IdRol = u.IdRol,
                IdPersona = u.IdPersona,
                CambioContrasenaObligatorio = u.CambioContrasenaObligatorio,
                FechaExpiracion = u.FechaExpiracion,
                Habilitado = u.FechaBloqueo > System.DateTime.Now
            };
        }
    }
}
