// src/BusinessLogic/Services/UserService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using UserManagementSystem.DataAccess.Exceptions;
using UserManagementSystem.BusinessLogic.Exceptions;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        private T ExecuteServiceOperation<T>(Func<T> operation, string operationName)
        {
            try
            {
                return operation();
            }
            catch (InfrastructureException ex)
            {
                throw new DataAccessLayerException($"A data access error occurred during {operationName}.", ex);
            }
        }

        private void ExecuteServiceOperation(Action operation, string operationName)
        {
            try
            {
                operation();
            }
            catch (InfrastructureException ex)
            {
                throw new DataAccessLayerException($"A data access error occurred during {operationName}.", ex);
            }
        }

        public void CrearPersona(PersonaRequest request) => ExecuteServiceOperation(() =>
        {
            var persona = new Persona
            {
                Legajo = int.Parse(request.Legajo),
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                IdTipoDoc = _userRepository.GetTipoDocByNombre(request.TipoDoc)?.IdTipoDoc ?? throw new ValidationException("Tipo de documento no encontrado"),
                NumDoc = request.NumDoc,
                Cuil = request.Cuil,
                Calle = request.Calle,
                Altura = request.Altura,
                IdLocalidad = _userRepository.GetLocalidadByNombre(request.Localidad)?.IdLocalidad ?? throw new ValidationException("Localidad no encontrada"),
                IdGenero = _userRepository.GetGeneroByNombre(request.Genero)?.IdGenero ?? throw new ValidationException("Género no encontrado"),
                Correo = request.Correo,
                FechaIngreso = DateTime.Now
            };
            _userRepository.AddPersona(persona);
        }, "creating a person");

        public void CrearUsuario(UserRequest request) => ExecuteServiceOperation(() =>
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
            // _emailService.SendEmailAsync(persona.Correo, "Bienvenido al Sistema", $"Su contraseña temporal es: {generatedPassword}");
        }, "creating a user");

        public UserResponse? Authenticate(string username, string password) => ExecuteServiceOperation(() =>
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
        }, "authenticating user");

        public void RecuperarContrasena(string username, Dictionary<int, string> respuestas) => ExecuteServiceOperation(() =>
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ValidationException("Username is required");

            var politica = _userRepository.GetPoliticaSeguridad() ?? new PoliticaSeguridad { CantPreguntas = 3 }; // Default to 3
            if (respuestas == null || respuestas.Count != politica.CantPreguntas || respuestas.Any(r => string.IsNullOrWhiteSpace(r.Value)))
                throw new ValidationException($"Se requieren {politica.CantPreguntas} respuestas de seguridad.");

            var usuario = _userRepository.GetUsuarioByNombreUsuario(username)
                ?? throw new ValidationException($"Usuario '{username}' not found");

            var persona = _userRepository.GetPersonaById(usuario.IdPersona)
                ?? throw new ValidationException("Persona no encontrada");

            var respuestasGuardadas = _userRepository.GetRespuestasSeguridadByUsuarioId(usuario.IdUsuario)
                ?? throw new ValidationException("No se han configurado las preguntas de seguridad.");

            if (respuestasGuardadas.Count != politica.CantPreguntas)
                throw new ValidationException("La cantidad de respuestas guardadas no coincide con la política de seguridad.");

            // Convertir a diccionario para búsqueda fácil
            var respuestasGuardadasDict = respuestasGuardadas.ToDictionary(r => r.IdPregunta, r => r.Respuesta);

            foreach (var respuesta in respuestas)
            {
                if (!respuestasGuardadasDict.TryGetValue(respuesta.Key, out var respuestaGuardada) || respuestaGuardada != respuesta.Value)
                {
                    throw new ValidationException("Una o más respuestas de seguridad son incorrectas.");
                }
            }

            var newPassword = GenerateRandomPassword();
            usuario.ContrasenaScript = HashUsuarioContrasena(username, newPassword);
            usuario.FechaUltimoCambio = DateTime.Now;
            usuario.CambioContrasenaObligatorio = true;
            _userRepository.UpdateUsuario(usuario);

            ArmarMail.DireccionCorreo = persona.Correo ?? string.Empty;
            ArmarMail.Asunto = "Recuperación de Contraseña";
            ArmarMail.NuevaContraseña = newPassword;
            ArmarMail.Preparar();
        }, "recovering password");

        public void CambiarContrasena(string username, string newPassword) => ExecuteServiceOperation(() =>
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

            var oldPasswordHash = usuario.ContrasenaScript;
            if (oldPasswordHash == null)
            {
                // This case should ideally not happen if data is consistent
                throw new InvalidOperationException("User password hash cannot be null.");
            }
            _userRepository.AddHistorialContrasena(new HistorialContrasena
            {
                IdUsuario = usuario.IdUsuario,
                ContrasenaScript = oldPasswordHash,
                FechaCambio = DateTime.Now
            });

            usuario.ContrasenaScript = newPasswordHash;
            usuario.FechaUltimoCambio = DateTime.Now;
            usuario.CambioContrasenaObligatorio = false;
            _userRepository.UpdateUsuario(usuario);
        }, "changing password");

        public List<TipoDoc> GetTiposDoc() => ExecuteServiceOperation(() => _userRepository.GetAllTiposDoc(), "getting all document types");

        public List<Localidad> GetLocalidades() => ExecuteServiceOperation(() => _userRepository.GetAllLocalidades(), "getting all locations");

        public List<Genero> GetGeneros() => ExecuteServiceOperation(() => _userRepository.GetAllGeneros(), "getting all genders");

        public List<Persona> GetPersonas() => ExecuteServiceOperation(() => _userRepository.GetAllPersonas(), "getting all people");

        public List<Rol> GetRoles() => ExecuteServiceOperation(() => _userRepository.GetAllRoles(), "getting all roles");

        public PoliticaSeguridad? GetPoliticaSeguridad() => ExecuteServiceOperation(() => _userRepository.GetPoliticaSeguridad(), "getting security policy");

        public void UpdatePoliticaSeguridad(PoliticaSeguridad politica) => ExecuteServiceOperation(() => _userRepository.UpdatePoliticaSeguridad(politica), "updating security policy");

        public List<Usuario> GetAllUsers() => ExecuteServiceOperation(() => _userRepository.GetAllUsers(), "getting all users");

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

        public void GuardarRespuestasSeguridad(string username, Dictionary<int, string> respuestas) => ExecuteServiceOperation(() =>
        {
            var usuario = _userRepository.GetUsuarioByNombreUsuario(username)
                ?? throw new ValidationException($"Usuario '{username}' not found");

            var politica = _userRepository.GetPoliticaSeguridad() ?? new PoliticaSeguridad { CantPreguntas = 3 };
            if (respuestas.Count != politica.CantPreguntas)
                throw new ValidationException($"Se requieren exactamente {politica.CantPreguntas} respuestas de seguridad.");

            // Opcional: borrar respuestas anteriores
            var respuestasAnteriores = _userRepository.GetRespuestasSeguridadByUsuarioId(usuario.IdUsuario);
            if(respuestasAnteriores != null)
            {
                foreach(var r in respuestasAnteriores)
                {
                    // Asumiendo que hay un método para borrar, si no, habría que agregarlo
                    // _userRepository.DeleteRespuestaSeguridad(r);
                }
            }


            foreach (var par in respuestas)
            {
                var respuesta = new RespuestaSeguridad
                {
                    IdUsuario = usuario.IdUsuario,
                    IdPregunta = par.Key,
                    Respuesta = par.Value // Idealmente, las respuestas también deberían ser hasheadas
                };
                _userRepository.AddRespuestaSeguridad(respuesta);
            }
        }, "saving security answers");

        public List<PreguntaSeguridad> GetPreguntasSeguridad() => ExecuteServiceOperation(
            () => _userRepository.GetPreguntasSeguridad(), "getting security questions");

        public List<PreguntaSeguridad> GetPreguntasDeUsuario(string username) => ExecuteServiceOperation(() =>
        {
            var usuario = _userRepository.GetUsuarioByNombreUsuario(username)
                ?? throw new ValidationException($"Usuario '{username}' not found");

            var respuestas = _userRepository.GetRespuestasSeguridadByUsuarioId(usuario.IdUsuario)
                ?? throw new ValidationException("No se han configurado las preguntas de seguridad.");

            var idPreguntas = respuestas.Select(r => r.IdPregunta).ToList();

            var preguntas = _userRepository.GetPreguntasSeguridad();

            return preguntas.Where(p => idPreguntas.Contains(p.IdPregunta)).ToList();

        }, "getting user security questions");
    }
}