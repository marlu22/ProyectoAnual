using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Models;
using DataAccess.Repositories;
using Microsoft.Extensions.Logging;
using System;
using UserManagementSystem.BusinessLogic.Exceptions;

namespace BusinessLogic.Services
{
    public class ReferenceDataService : IReferenceDataService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<ReferenceDataService> _logger;

        public ReferenceDataService(IUserRepository userRepository, ILogger<ReferenceDataService> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private T ExecuteServiceOperation<T>(Func<T> operation, string operationName)
        {
            try
            {
                return operation();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during {OperationName}", operationName);
                throw new BusinessLogicException($"An unexpected error occurred during {operationName}.", ex);
            }
        }

        public List<TipoDocDto> GetTiposDoc() => ExecuteServiceOperation(() =>
            _userRepository.GetAllTiposDoc().Select(t => new TipoDocDto { IdTipoDoc = t.IdTipoDoc, Nombre = t.Nombre }).ToList(),
            "getting all document types");

        public List<ProvinciaDto> GetProvincias() => ExecuteServiceOperation(() =>
            _userRepository.GetAllProvincias().Select(p => new ProvinciaDto { IdProvincia = p.IdProvincia, Nombre = p.Nombre }).ToList(),
            "getting all provinces");

        public List<PartidoDto> GetPartidosByProvinciaId(int provinciaId) => ExecuteServiceOperation(() =>
            _userRepository.GetPartidosByProvinciaId(provinciaId).Select(p => new PartidoDto { IdPartido = p.IdPartido, Nombre = p.Nombre, IdProvincia = p.IdProvincia }).ToList(),
            "getting partidos by provincia");

        public List<LocalidadDto> GetLocalidadesByPartidoId(int partidoId) => ExecuteServiceOperation(() =>
            _userRepository.GetLocalidadesByPartidoId(partidoId).Select(l => new LocalidadDto { IdLocalidad = l.IdLocalidad, Nombre = l.Nombre, IdPartido = l.IdPartido }).ToList(),
            "getting localidades by partido");

        public List<GeneroDto> GetGeneros() => ExecuteServiceOperation(() =>
            _userRepository.GetAllGeneros().Select(g => new GeneroDto { IdGenero = g.IdGenero, Nombre = g.Nombre }).ToList(),
            "getting all genders");

        public List<RolDto> GetRoles() => ExecuteServiceOperation(() =>
            _userRepository.GetAllRoles().Select(r => new RolDto { IdRol = r.IdRol, Nombre = r.Nombre }).ToList(),
            "getting all roles");
    }
}
