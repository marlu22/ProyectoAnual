using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using BusinessLogic.Exceptions;

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
            if (!int.TryParse(request.Localidad, out int localidadId))
            {
                throw new ValidationException("El ID de localidad no es válido.");
            }

            var idTipoDoc = _userRepository.GetTipoDocByNombre(request.TipoDoc)?.IdTipoDoc
                ?? throw new ValidationException("Tipo de documento no encontrado");
            var idGenero = _userRepository.GetGeneroByNombre(request.Genero)?.IdGenero
                ?? throw new ValidationException("Género no encontrado");

            return new Persona(
                legajo,
                request.Nombre,
                request.Apellido,
                idTipoDoc,
                request.NumDoc,
                request.FechaNacimiento,
                request.Cuil,
                request.Calle,
                request.Altura,
                localidadId,
                idGenero,
                request.Correo,
                request.Celular,
                request.FechaIngreso
            );
        }
    }
}
