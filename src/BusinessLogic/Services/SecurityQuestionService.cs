using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Exceptions;
using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class SecurityQuestionService : ISecurityQuestionService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISecurityRepository _securityRepository;
        private readonly ILogger<SecurityQuestionService> _logger;
        private readonly ISecurityPolicyService _securityPolicyService;

        public SecurityQuestionService(
            IUserRepository userRepository,
            ISecurityRepository securityRepository,
            ILogger<SecurityQuestionService> logger,
            ISecurityPolicyService securityPolicyService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _securityRepository = securityRepository ?? throw new ArgumentNullException(nameof(securityRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _securityPolicyService = securityPolicyService ?? throw new ArgumentNullException(nameof(securityPolicyService));
        }

        public PoliticaSeguridadDto? GetPoliticaSeguridad()
        {
            return _securityPolicyService.GetPoliticaSeguridad();
        }

        public void GuardarRespuestasSeguridad(string username, Dictionary<int, string> respuestas) => ExecuteServiceOperation(() =>
        {
            var usuario = _userRepository.GetUsuarioByNombreUsuario(username)
                ?? throw new ValidationException($"Usuario '{username}' not found");

            var politica = _securityRepository.GetPoliticaSeguridad() ?? throw new BusinessLogicException("Security policy not configured.");
            if (respuestas.Count != politica.CantPreguntas)
                throw new ValidationException($"Se requieren exactamente {politica.CantPreguntas} respuestas de seguridad.");

            _securityRepository.DeleteRespuestasSeguridadByUsuarioId(usuario.IdUsuario);

            foreach (var par in respuestas)
            {
                var respuesta = new RespuestaSeguridad
                {
                    IdUsuario = usuario.IdUsuario,
                    IdPregunta = par.Key,
                    Respuesta = par.Value
                };
                _securityRepository.AddRespuestaSeguridad(respuesta);
            }
        }, "saving security answers");

        public List<PreguntaSeguridadDto> GetPreguntasSeguridad() => ExecuteServiceOperation(() =>
            _securityRepository.GetPreguntasSeguridad().Select(p => new PreguntaSeguridadDto { IdPregunta = p.IdPregunta, Pregunta = p.Pregunta }).ToList(),
            "getting security questions");

        public List<PreguntaSeguridadDto> GetPreguntasDeUsuario(string username) => ExecuteServiceOperation(() =>
        {
            var usuario = _userRepository.GetUsuarioByNombreUsuario(username)
                ?? throw new ValidationException($"Usuario '{username}' not found");

            var respuestas = _securityRepository.GetRespuestasSeguridadByUsuarioId(usuario.IdUsuario)
                ?? throw new ValidationException("No se han configurado las preguntas de seguridad.");

            var idPreguntas = respuestas.Select(r => r.IdPregunta).ToList();
            var preguntas = _securityRepository.GetPreguntasSeguridadByIds(idPreguntas);

            return preguntas.Select(p => new PreguntaSeguridadDto { IdPregunta = p.IdPregunta, Pregunta = p.Pregunta }).ToList();
        }, "getting user security questions");

        private T ExecuteServiceOperation<T>(Func<T> operation, string operationName)
        {
            try
            {
                return operation();
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during {OperationName}", operationName);
                throw new BusinessLogicException($"An unexpected error occurred during {operationName}.", ex);
            }
        }

        private void ExecuteServiceOperation(Action operation, string operationName)
        {
            try
            {
                operation();
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during {OperationName}", operationName);
                throw new BusinessLogicException($"An unexpected error occurred during {operationName}.", ex);
            }
        }
    }
}
