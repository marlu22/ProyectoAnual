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

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
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
                IdGenero = _userRepository.GetGeneroByNombre(request.Genero)?.IdGenero ?? throw new ValidationException("Género no encontrado"),
                Correo = request.Correo,
                FechaIngreso = DateTime.Now
            };
            _userRepository.AddPersona(persona);
        }

        // src/BusinessLogic/Services/UserService.cs (partial)
        public void CrearUsuario(UserRequest request)
        {
            var usuario = new Usuario
            {
                IdPersona = int.Parse(request.PersonaId), // Convert string to int
                UsuarioNombre = request.Username,
                ContrasenaScript = HashUsuarioContrasena(request.Username, request.Password),
                IdRol = _userRepository.GetRolByNombre(request.Rol)?.IdRol ?? throw new ValidationException("Rol no encontrado"),
                FechaUltimoCambio = DateTime.Now,
                FechaBloqueo = new DateTime(9999, 12, 31),
                CambioContrasenaObligatorio = false
            };
            _userRepository.AddUsuario(usuario);
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

        public void RecuperarContrasena(string username, string[] respuestas)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ValidationException("Username is required");

            if (respuestas == null || respuestas.Length != 2 || respuestas.Any(r => string.IsNullOrWhiteSpace(r)))
                throw new ValidationException("Two security answers are required");

            var usuario = _userRepository.GetUsuarioByNombreUsuario(username)
                ?? throw new ValidationException($"Usuario '{username}' not found");

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
        }

        public void CambiarContrasena(string username, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(newPassword))
                throw new ValidationException("Username and new password are required");

            var usuario = _userRepository.GetUsuarioByNombreUsuario(username)
                ?? throw new ValidationException($"Usuario '{username}' not found");

            usuario.ContrasenaScript = HashUsuarioContrasena(username, newPassword);
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
            return Guid.NewGuid().ToString("N").Substring(0, 12);
        }
    }
}