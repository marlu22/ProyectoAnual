// src/BusinessLogic/Services/UserService.cs
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using BusinessLogic.Exceptions;
using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public UserService(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public void CrearPersona(PersonaRequest request)
        {
            var persona = new Persona
            {
                Legajo = request.Legajo,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                IdTipoDoc = _userRepository.GetTipoDocByNombre(request.TipoDoc)?.IdTipoDoc ?? throw new ValidationException("Tipo de documento no encontrado"),
                NumDoc = request.NumDoc,
                Cuil = request.Cuil,
                Calle = request.Calle,
                Altura = request.Altura,
                IdLocalidad = _userRepository.GetLocalidadByNombre(request.Localidad)?.IdLocalidad ?? throw new ValidationException("Localidad no encontrada"),
                IdGenero = _userRepository.GetGeneroByNombre(request.Genero)?.IdGenero ?? throw new ValidationException("G�nero no encontrado"),
                Correo = request.Correo,
                FechaIngreso = DateTime.Now
            };
            _userRepository.AddPersona(persona);
        }

        // src/BusinessLogic/Services/UserService.cs (partial)
        public void CrearUsuario(UserRequest request)
        {
            var persona = _userRepository.GetPersonaById(int.Parse(request.PersonaId))
                ?? throw new ValidationException("Persona no encontrada");

            var generatedPassword = GenerateRandomPassword();
            ValidatePasswordPolicy(generatedPassword, request.Username, persona.Nombre, persona.Apellido);

            var usuario = new Usuario
            {
                IdPersona = int.Parse(request.PersonaId),
                UsuarioNombre = request.Username,
                ContrasenaScript = HashUsuarioContrasena(request.Username, generatedPassword),
                IdRol = _userRepository.GetRolByNombre(request.Rol)?.IdRol ?? throw new ValidationException("Rol no encontrado"),
                FechaUltimoCambio = DateTime.Now,
                FechaBloqueo = new DateTime(9999, 12, 31),
                CambioContrasenaObligatorio = true // Forzar cambio de contraseña en el primer login
            };
            _userRepository.AddUsuario(usuario);

            // Enviar la contraseña generada por correo
            _emailService.SendEmailAsync(persona.Correo, "Bienvenido al Sistema", $"Su contraseña temporal es: {generatedPassword}");
        }

        public UserResponse? Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            var usuario = _userRepository.GetUsuarioByNombreUsuario(username);
            if (usuario == null)
                return null;

            var hash = HashUsuarioContrasena(username, password);
            if (!hash.SequenceEqual(usuario.ContrasenaScript))
                return null;

            return new UserResponse
            {
                Username = usuario.UsuarioNombre,
                Rol = usuario.Rol?.Nombre,
                CambioContrasenaObligatorio = usuario.CambioContrasenaObligatorio
            };
        }

        public async void RecuperarContrasena(string username, string[] respuestas)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ValidationException("Username is required");

            if (respuestas == null || respuestas.Length != 2 || respuestas.Any(r => string.IsNullOrWhiteSpace(r)))
                throw new ValidationException("Two security answers are required");

            var usuario = _userRepository.GetUsuarioByNombreUsuario(username)
                ?? throw new ValidationException($"Usuario '{username}' not found");

            var persona = _userRepository.GetPersonaById(usuario.IdPersona)
                ?? throw new ValidationException("Persona no encontrada");

            var respuestasSeguridad = _userRepository.GetRespuestasSeguridadByUsuarioId(usuario.IdUsuario)
                ?? throw new ValidationException("Security answers not configured");

            if (respuestasSeguridad.Count != 2)
                throw new ValidationException("Exactly two security answers are required");

            if (respuestasSeguridad[0].Respuesta != respuestas[0] || respuestasSeguridad[1].Respuesta != respuestas[1])
                throw new ValidationException("Incorrect security answers");

            var newPassword = GenerateRandomPassword();
            usuario.ContrasenaScript = HashUsuarioContrasena(username, newPassword);
            usuario.FechaUltimoCambio = DateTime.Now;
            usuario.CambioContrasenaObligatorio = true;
            _userRepository.UpdateUsuario(usuario);

            await _emailService.SendEmailAsync(persona.Correo, "Recuperación de Contraseña", $"Su nueva contraseña es: {newPassword}");
        }

        public void CambiarContrasena(string username, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(newPassword))
                throw new ValidationException("Username and new password are required");

            var usuario = _userRepository.GetUsuarioByNombreUsuario(username)
                ?? throw new ValidationException($"Usuario '{username}' not found");

            var persona = _userRepository.GetPersonaById(usuario.IdPersona)
                ?? throw new ValidationException("Persona no encontrada");

            ValidatePasswordPolicy(newPassword, username, persona.Nombre, persona.Apellido);

            var newPasswordHash = HashUsuarioContrasena(username, newPassword);

            var politica = _userRepository.GetPoliticaSeguridad();
            if (politica?.NoRepetirAnteriores ?? false)
            {
                var historial = _userRepository.GetHistorialContrasenasByUsuarioId(usuario.IdUsuario);
                if (historial.Any(h => h.ContrasenaScript.SequenceEqual(newPasswordHash)))
                {
                    throw new ValidationException("La nueva contraseña no puede ser igual a ninguna de las contraseñas anteriores.");
                }
            }

            _userRepository.AddHistorialContrasena(new HistorialContrasena
            {
                IdUsuario = usuario.IdUsuario,
                ContrasenaScript = usuario.ContrasenaScript,
                FechaCambio = DateTime.Now
            });

            usuario.ContrasenaScript = newPasswordHash;
            usuario.FechaUltimoCambio = DateTime.Now;
            usuario.CambioContrasenaObligatorio = false;
            _userRepository.UpdateUsuario(usuario);
        }

        public List<TipoDoc> GetTiposDoc()
        {
            return _userRepository.GetAllTiposDoc();
        }

        public List<Localidad> GetLocalidades()
        {
            return _userRepository.GetAllLocalidades();
        }

        public List<Genero> GetGeneros()
        {
            return _userRepository.GetAllGeneros();
        }

        public List<Persona> GetPersonas()
        {
            return _userRepository.GetAllPersonas();
        }

        public List<Rol> GetRoles()
        {
            return _userRepository.GetAllRoles();
        }

        public PoliticaSeguridad? GetPoliticaSeguridad()
        {
            return _userRepository.GetPoliticaSeguridad();
        }

        public void UpdatePoliticaSeguridad(PoliticaSeguridad politica)
        {
            _userRepository.UpdatePoliticaSeguridad(politica);
        }

        public List<Usuario> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        private byte[] HashUsuarioContrasena(string username, string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var salted = $"{username}:{password}";
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(salted));
            }
        }

        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 12)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void ValidatePasswordPolicy(string password, string username, string nombre, string apellido)
        {
            var politica = _userRepository.GetPoliticaSeguridad();
            if (politica == null) return;

            if (password.Length < politica.MinCaracteres)
                throw new ValidationException($"La contraseña debe tener al menos {politica.MinCaracteres} caracteres.");

            if (politica.MayusYMinus && (!password.Any(char.IsUpper) || !password.Any(char.IsLower)))
                throw new ValidationException("La contraseña debe contener mayúsculas y minúsculas.");

            if (politica.LetrasYNumeros && !password.Any(char.IsDigit))
                throw new ValidationException("La contraseña debe contener números.");

            if (politica.CaracterEspecial && !password.Any(c => !char.IsLetterOrDigit(c)))
                throw new ValidationException("La contraseña debe contener caracteres especiales.");

            if (politica.SinDatosPersonales)
            {
                if (password.Contains(username, StringComparison.OrdinalIgnoreCase) ||
                    password.Contains(nombre, StringComparison.OrdinalIgnoreCase) ||
                    password.Contains(apellido, StringComparison.OrdinalIgnoreCase))
                {
                    throw new ValidationException("La contraseña no debe contener datos personales (nombre de usuario, nombre o apellido).");
                }
            }
        }

        public void GuardarRespuestasSeguridad(string username, string[] respuestas)
        {
            var usuario = _userRepository.GetUsuarioByNombreUsuario(username)
                ?? throw new ValidationException($"Usuario '{username}' not found");

            if (respuestas.Length != 2)
                throw new ValidationException("Se requieren exactamente dos respuestas de seguridad.");

            // Asumiendo que las preguntas de seguridad tienen IDs 1 y 2
            var respuesta1 = new RespuestaSeguridad
            {
                IdUsuario = usuario.IdUsuario,
                IdPregunta = 1,
                Respuesta = respuestas[0]
            };
            _userRepository.AddRespuestaSeguridad(respuesta1);

            var respuesta2 = new RespuestaSeguridad
            {
                IdUsuario = usuario.IdUsuario,
                IdPregunta = 2,
                Respuesta = respuestas[1]
            };
            _userRepository.AddRespuestaSeguridad(respuesta2);
        }
    }
}