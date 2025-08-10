using System;
using BusinessLogic.Exceptions;
using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class SecurityPolicyService : ISecurityPolicyService
    {
        private readonly ISecurityRepository _securityRepository;
        private readonly ILogger<SecurityPolicyService> _logger;

        public SecurityPolicyService(ISecurityRepository securityRepository, ILogger<SecurityPolicyService> logger)
        {
            _securityRepository = securityRepository ?? throw new ArgumentNullException(nameof(securityRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public PoliticaSeguridadDto? GetPoliticaSeguridad() => ExecuteServiceOperation(() =>
        {
            var politica = _securityRepository.GetPoliticaSeguridad();
            return MapToPoliticaSeguridadDto(politica);
        }, "getting security policy");

        public void UpdatePoliticaSeguridad(PoliticaSeguridadDto politicaDto) => ExecuteServiceOperation(() =>
        {
            if (politicaDto == null) throw new ArgumentNullException(nameof(politicaDto));
            var politica = MapToPoliticaSeguridadEntity(politicaDto);
            _securityRepository.UpdatePoliticaSeguridad(politica);
        }, "updating security policy");

        private PoliticaSeguridadDto? MapToPoliticaSeguridadDto(PoliticaSeguridad? politica)
        {
            if (politica == null) return null;
            return new PoliticaSeguridadDto
            {
                IdPolitica = politica.IdPolitica,
                MayusYMinus = politica.MayusYMinus,
                LetrasYNumeros = politica.LetrasYNumeros,
                CaracterEspecial = politica.CaracterEspecial,
                Autenticacion2FA = politica.Autenticacion2FA,
                NoRepetirAnteriores = politica.NoRepetirAnteriores,
                SinDatosPersonales = politica.SinDatosPersonales,
                MinCaracteres = politica.MinCaracteres,
                CantPreguntas = politica.CantPreguntas
            };
        }

        private PoliticaSeguridad MapToPoliticaSeguridadEntity(PoliticaSeguridadDto politicaDto)
        {
            return new PoliticaSeguridad(
                politicaDto.IdPolitica,
                politicaDto.MayusYMinus,
                politicaDto.LetrasYNumeros,
                politicaDto.CaracterEspecial,
                politicaDto.Autenticacion2FA,
                politicaDto.NoRepetirAnteriores,
                politicaDto.SinDatosPersonales,
                politicaDto.MinCaracteres,
                politicaDto.CantPreguntas
            );
        }

        private void ExecuteServiceOperation(Action operation, string operationName)
        {
            try
            {
                operation();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during {OperationName}", operationName);
                throw new BusinessLogicException($"An unexpected error occurred during {operationName}.", ex);
            }
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
    }
}
