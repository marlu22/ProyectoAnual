// src/DataAccess/Repositories/SqlUserRepository.cs
using System;
using System.Collections.Generic;
using System.Data;
using DataAccess.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using UserManagementSystem.DataAccess.Exceptions;

namespace DataAccess.Repositories
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly ILogger<SqlUserRepository> _logger;

        public SqlUserRepository(DatabaseConnectionFactory connectionFactory, ILogger<SqlUserRepository> logger)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Generic helper for executing read operations
        private T ExecuteReader<T>(string sql, Func<SqlDataReader, T> map, Action<SqlParameterCollection>? addParameters = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        command.CommandType = commandType;
                        addParameters?.Invoke(command.Parameters);

                        using (var reader = command.ExecuteReader())
                        {
                            return map(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Ocurrió un error de SQL al ejecutar ExecuteReader para el comando: {sql}", sql);
                throw new DataAccessLayerException($"Ocurrió un error de SQL al ejecutar {sql}", ex);
            }
        }

        // Generic helper for executing write operations
        private void ExecuteNonQuery(string sql, Action<SqlParameterCollection> addParameters, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        command.CommandType = commandType;
                        addParameters(command.Parameters);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Ocurrió un error de SQL al ejecutar ExecuteNonQuery para el comando: {sql}", sql);
                throw new DataAccessLayerException($"Ocurrió un error de SQL al ejecutar {sql}", ex);
            }
        }

        public List<HistorialContrasena> GetHistorialContrasenasByUsuarioId(int idUsuario) => ExecuteReader("SELECT id, id_usuario, fecha_cambio, contrasena_script FROM historial_contrasena WHERE id_usuario = @id_usuario;", reader =>
        {
            var list = new List<HistorialContrasena>();
            while (reader.Read())
            {
                list.Add(new HistorialContrasena
                {
                    IdHistorial = (int)reader["id"],
                    IdUsuario = (int)reader["id_usuario"],
                    FechaCambio = (DateTime)reader["fecha_cambio"],
                    ContrasenaScript = (byte[])reader["contrasena_script"]
                });
            }
            return list;
        }, p => p.AddWithValue("@id_usuario", idUsuario));

        public Usuario? GetUsuarioByNombreUsuario(string nombre) => ExecuteReader("sp_get_usuario_by_nombre", reader =>
        {
            if (!reader.Read()) return null;
            var usuario = new Usuario(
                reader["usuario"] as string ?? string.Empty,
                (byte[])reader["contrasena_script"],
                (int)reader["id_persona"],
                (int)reader["id_rol"],
                reader["id_politica"] as int?
            );
            // As the constructor sets default values, we might need to update some properties
            // based on the data returned from the database if they differ from the defaults.
            // For now, we assume the constructor logic is sufficient.
            return usuario;
        }, p => p.AddWithValue("@usuario_nombre", nombre), CommandType.StoredProcedure);

        public void Set2faCode(string username, string? code, DateTime? expiry) => ExecuteNonQuery(
            "UPDATE usuarios SET Codigo2FA = @code, Codigo2FAExpiracion = @expiry WHERE usuario = @username",
            p =>
            {
                p.AddWithValue("@code", (object?)code ?? DBNull.Value);
                p.AddWithValue("@expiry", (object?)expiry ?? DBNull.Value);
                p.AddWithValue("@username", username);
            },
            CommandType.Text
        );

        public List<Usuario> GetAllUsers() => ExecuteReader("sp_get_all_users", reader =>
        {
            var list = new List<Usuario>();
            while (reader.Read())
            {
                var usuario = new Usuario(
                    reader["usuario"] as string ?? string.Empty,
                    (byte[])reader["contrasena_script"],
                    (int)reader["id_persona"],
                    (int)reader["id_rol"],
                    reader["id_politica"] as int?
                );
                list.Add(usuario);
            }
            return list;
        }, commandType: CommandType.StoredProcedure);

        public void AddHistorialContrasena(HistorialContrasena historial) => ExecuteNonQuery("sp_historial_contrasena", p =>
        {
            p.AddWithValue("@id_usuario", historial.IdUsuario);
            p.AddWithValue("@contrasena_script", historial.ContrasenaScript);
        });

        public void AddUsuario(Usuario usuario) => ExecuteNonQuery("sp_insert_usuario", p =>
        {
            p.AddWithValue("@usuario", usuario.UsuarioNombre);
            p.AddWithValue("@contrasena_script", usuario.ContrasenaScript);
            p.AddWithValue("@id_persona", usuario.IdPersona);
            p.AddWithValue("@fecha_bloqueo", usuario.FechaBloqueo);
            p.AddWithValue("@nombre_usuario_bloqueo", (object?)usuario.NombreUsuarioBloqueo ?? DBNull.Value);
            p.AddWithValue("@fecha_ultimo_cambio", usuario.FechaUltimoCambio);
            p.AddWithValue("@id_rol", usuario.IdRol);
            p.AddWithValue("@CambioContrasenaObligatorio", usuario.CambioContrasenaObligatorio);
        });

        public void UpdateUsuario(Usuario usuario) => ExecuteNonQuery("sp_actualizar_usuario", p =>
        {
            p.AddWithValue("@id_usuario", usuario.IdUsuario);
            p.AddWithValue("@usuario", usuario.UsuarioNombre);
            p.AddWithValue("@contrasena_script", usuario.ContrasenaScript);
            p.AddWithValue("@id_persona", usuario.IdPersona);
            p.AddWithValue("@fecha_bloqueo", usuario.FechaBloqueo);
            p.AddWithValue("@nombre_usuario_bloqueo", (object?)usuario.NombreUsuarioBloqueo ?? DBNull.Value);
            p.AddWithValue("@fecha_ultimo_cambio", usuario.FechaUltimoCambio);
            p.AddWithValue("@id_rol", usuario.IdRol);
            p.AddWithValue("@CambioContrasenaObligatorio", usuario.CambioContrasenaObligatorio);
        });

        public void DeleteUsuario(int usuarioId) => ExecuteNonQuery("sp_delete_usuario", p =>
        {
            p.AddWithValue("@id_usuario", usuarioId);
        });
    }
}
