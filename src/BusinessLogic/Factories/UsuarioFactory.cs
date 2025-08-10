using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.Models;
using BusinessLogic.Security;
using DataAccess.Entities;
using DataAccess.Repositories;
using UserManagementSystem.BusinessLogic.Exceptions;

namespace BusinessLogic.Factories
{
    public class UsuarioFactory
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UsuarioFactory(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public (Usuario Usuario, string PlainPassword) Create(UserRequest request)
        {
            var persona = _userRepository.GetPersonaById(int.Parse(request.PersonaId))
                ?? throw new ValidationException("Persona no encontrada");

            if (string.IsNullOrWhiteSpace(persona.Correo))
            {
                throw new ValidationException("La persona seleccionada no tiene un correo electrónico para enviar la contraseña.");
            }

            string passwordToUse = GenerateRandomPassword(request.Username, persona);

            var usuario = new Usuario
            {
                IdPersona = int.Parse(request.PersonaId),
                UsuarioNombre = request.Username,
                ContrasenaScript = _passwordHasher.Hash(request.Username, passwordToUse),
                IdRol = _userRepository.GetRolByNombre(request.Rol)?.IdRol ?? throw new ValidationException("Rol no encontrado"),
                FechaUltimoCambio = DateTime.Now,
                CambioContrasenaObligatorio = true
            };

            usuario.Habilitar(); // Use the entity's own method to set the state

            return (usuario, passwordToUse);
        }

        private string GenerateRandomPassword(string? username = null, Persona? persona = null)
        {
            var politica = _userRepository.GetPoliticaSeguridad() ?? new PoliticaSeguridad();
            var random = new Random();

            while (true)
            {
                var minLength = politica.MinCaracteres > 0 ? politica.MinCaracteres : 12;
                var passwordChars = new List<char>();
                const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                const string lower = "abcdefghijklmnopqrstuvwxyz";
                const string digits = "0123456789";
                const string specials = "!@#$%^&*()";
                var allChars = new StringBuilder(upper).Append(lower).Append(digits).Append(specials).ToString();

                if (politica.MayusYMinus)
                {
                    passwordChars.Add(upper[random.Next(upper.Length)]);
                    passwordChars.Add(lower[random.Next(lower.Length)]);
                }
                if (politica.LetrasYNumeros)
                {
                    passwordChars.Add(digits[random.Next(digits.Length)]);
                }
                if (politica.CaracterEspecial)
                {
                    passwordChars.Add(specials[random.Next(specials.Length)]);
                }

                while (passwordChars.Count < minLength)
                {
                    passwordChars.Add(allChars[random.Next(allChars.Length)]);
                }

                var password = new string(passwordChars.OrderBy(c => random.Next()).ToArray());

                if (politica.SinDatosPersonales && username != null && persona != null)
                {
                    try
                    {
                        // This validation should ideally be in a separate validator object,
                        // but for now we keep it here to match the original logic.
                        new PasswordPolicyValidator().Validate(password, username, persona, politica);
                    }
                    catch (ValidationException)
                    {
                        continue; // Try generating a new password
                    }
                }
                return password;
            }
        }
    }
}
