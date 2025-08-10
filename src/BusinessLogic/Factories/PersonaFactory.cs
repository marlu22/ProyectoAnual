using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using UserManagementSystem.BusinessLogic.Exceptions;
using System.Text.RegularExpressions;

namespace BusinessLogic.Factories
{
    public class PersonaFactory
    {
        private readonly IUserRepository _userRepository;

        public PersonaFactory(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Persona Create(PersonaRequest request)
        {
            if (!int.TryParse(request.Legajo, out int legajo))
            {
                throw new ValidationException("El legajo debe ser un número válido.");
            }

            if (string.IsNullOrWhiteSpace(request.Nombre))
                throw new ValidationException("El nombre no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(request.Apellido))
                throw new ValidationException("El apellido no puede estar vacío.");
            if (!long.TryParse(request.NumDoc, out _))
                throw new ValidationException("El número de documento debe ser numérico.");
            if (!string.IsNullOrWhiteSpace(request.Cuil) && !long.TryParse(request.Cuil, out _))
                throw new ValidationException("El CUIL debe ser numérico.");
            if (string.IsNullOrWhiteSpace(request.Calle))
                throw new ValidationException("La calle no puede estar vacía.");
            if (!int.TryParse(request.Altura, out _))
                throw new ValidationException("La altura de la dirección debe ser un número.");
            if (string.IsNullOrWhiteSpace(request.Correo) || !IsValidEmail(request.Correo))
                throw new ValidationException("El formato del correo electrónico no es válido.");
            if (!int.TryParse(request.Localidad, out int localidadId))
            {
                throw new ValidationException("El ID de localidad no es válido.");
            }

            var persona = new Persona
            {
                Legajo = legajo,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                IdTipoDoc = _userRepository.GetTipoDocByNombre(request.TipoDoc)?.IdTipoDoc ?? throw new ValidationException("Tipo de documento no encontrado"),
                NumDoc = request.NumDoc,
                FechaNacimiento = request.FechaNacimiento,
                Cuil = request.Cuil,
                Calle = request.Calle,
                Altura = request.Altura,
                IdLocalidad = localidadId,
                IdGenero = _userRepository.GetGeneroByNombre(request.Genero)?.IdGenero ?? throw new ValidationException("Género no encontrado"),
                Correo = request.Correo,
                Celular = request.Celular,
                FechaIngreso = request.FechaIngreso
            };

            return persona;
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
