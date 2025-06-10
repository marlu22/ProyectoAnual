using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DataAccess.Repositories;
using DataAccess.Entities;
using BusinessLogic.Models;
using BusinessLogic.Exceptions;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var usuarios = _userRepository.GetAll();
            return usuarios.Select(u => new UserDto
            {
                Id = u.IdUsuario,
                Username = u.UsuarioNombre,
                // Agrega aquí otros campos que tengas en UserDto
            });
        }

        public UserDto CreateUser(UserRequest request)
        {
            if (string.IsNullOrEmpty(request.Password))
                throw new ArgumentException("Password is required.", nameof(request.Password));

            var usuario = new Usuario
            {
                UsuarioNombre = request.Username,
                ContrasenaScript = System.Text.Encoding.UTF8.GetBytes(request.Password),
                // Asigna aquí otros campos requeridos, por ejemplo:
                // IdPersona = ...,
                // IdRol = ...,
            };

            _userRepository.Add(usuario);

            return new UserDto
            {
                Id = usuario.IdUsuario,
                Username = usuario.UsuarioNombre,
                // Otros campos
            };
        }

        public UserDto UpdateUser(int id, UserRequest request)
        {
            var usuario = _userRepository.GetById(id);
            if (usuario == null) throw new KeyNotFoundException("Usuario no encontrado.");

            usuario.UsuarioNombre = request.Username;
            // Actualiza otros campos si es necesario

            _userRepository.Update(usuario);

            return new UserDto
            {
                Id = usuario.IdUsuario,
                Username = usuario.UsuarioNombre,
                // Otros campos
            };
        }

        public void DeleteUser(int id)
        {
            var usuario = _userRepository.GetById(id);
            if (usuario == null) throw new KeyNotFoundException("Usuario no encontrado.");

            _userRepository.Delete(usuario);
        }

        public void CrearPersona(PersonaRequest persona)
        {
            // Busca los objetos relacionados en la base de datos
            var tipoDoc = _userRepository.GetTipoDocByNombre(persona.TipoDoc);
            var localidad = _userRepository.GetLocalidadByNombre(persona.Localidad);
            var genero = _userRepository.GetGeneroByNombre(persona.Genero);

            var nuevaPersona = new DataAccess.Entities.Persona
            {
                Legajo = int.Parse(persona.Legajo),
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                IdTipoDoc = tipoDoc.IdTipoDoc,
                TipoDoc = tipoDoc,
                NumDoc = persona.NumDoc,
                Cuil = persona.Cuil,
                Calle = persona.Calle,
                Altura = persona.Altura,
                IdLocalidad = localidad.IdLocalidad,
                Localidad = localidad,
                IdGenero = genero.IdGenero,
                Genero = genero,
                Correo = persona.Correo
            };
            _userRepository.AddPersona(nuevaPersona);
        }

        public void CrearUsuario(UserRequest usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Username))
                throw new ValidationException("El nombre de usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Password) || usuario.Password.Length < 8)
                throw new ValidationException("La contraseña debe tener al menos 8 caracteres.");

            var persona = _userRepository.GetPersonaById(int.Parse(usuario.PersonaId));
            var rol = _userRepository.GetRolByNombre(usuario.Rol);

            var nuevoUsuario = new Usuario
            {
                IdPersona = persona.IdPersona,
                UsuarioNombre = usuario.Username,
                ContrasenaScript = System.Text.Encoding.UTF8.GetBytes(usuario.Password),
                IdRol = rol.IdRol,
                FechaUltimoCambio = DateTime.Now
            };
            _userRepository.AddUsuario(nuevoUsuario);
        }

        public List<PersonaDto> GetPersonas()
        {
            var personas = _userRepository.GetAllPersonas();
            return personas.Select(p => new PersonaDto
            {
                Id = p.IdPersona, // CORREGIDO: usar IdPersona
                NombreCompleto = $"{p.Nombre} {p.Apellido}"
            }).ToList();
        }

        public List<TipoDoc> GetTiposDoc() => _userRepository.GetAllTipoDocs().ToList();
        public List<Localidad> GetLocalidades() => _userRepository.GetAllLocalidades().ToList();
        public List<Genero> GetGeneros() => _userRepository.GetAllGeneros().ToList();
        public List<Rol> GetRoles() => _userRepository.GetAllRoles().ToList();

        public void CambiarContrasena(string usuario, string nuevaContrasena)
        {
            // Validar políticas de seguridad aquí (longitud, complejidad, etc.)
            if (string.IsNullOrWhiteSpace(nuevaContrasena) || nuevaContrasena.Length < 8)
                throw new ValidationException("La contraseña debe tener al menos 8 caracteres.");

            var user = _userRepository.GetByUsername(usuario);
            if (user == null)
                throw new ValidationException("Usuario no encontrado.");

            // Hashear usuario+contraseña
            var hash = HashUsuarioContrasena(usuario, nuevaContrasena);
            user.ContrasenaScript = Encoding.UTF8.GetBytes(hash);
            user.FechaUltimoCambio = DateTime.Now;

            _userRepository.Update(user);
            // Opcional: guardar en historial_contrasena
        }

        public void RecuperarContrasena(string usuario, string[] respuestas)
        {
            var user = _userRepository.GetByUsername(usuario);
            if (user == null)
                throw new ValidationException("Usuario no encontrado.");

            // Validar respuestas de seguridad (deberías tener un método para esto)
            if (!_userRepository.ValidarRespuestasSeguridad(user.IdUsuario, respuestas))
                throw new ValidationException("Respuestas de seguridad incorrectas.");

            // Generar nueva contraseña aleatoria
            var nueva = GenerarContrasenaAleatoria();
            var hash = HashUsuarioContrasena(usuario, nueva);
            user.ContrasenaScript = Encoding.UTF8.GetBytes(hash);
            user.FechaUltimoCambio = DateTime.Now;
            _userRepository.Update(user);

            // Enviar por correo (implementa el envío real en un helper o servicio)
            _userRepository.EnviarCorreoRecuperacion(user, nueva);
        }

        private string HashUsuarioContrasena(string usuario, string contrasena)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(usuario + contrasena);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private string GenerarContrasenaAleatoria(int longitud = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, longitud)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
