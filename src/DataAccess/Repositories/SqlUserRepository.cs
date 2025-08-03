// src/DataAccess/Repositories/SqlUserRepository.cs
using System;
using System.Collections.Generic;
using System.Data;
using DataAccess.Entities;
using Microsoft.Data.SqlClient;
using UserManagementSystem.DataAccess.Exceptions;

namespace DataAccess.Repositories
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly DatabaseConnectionFactory _connectionFactory;

        public SqlUserRepository(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        // Generic helper for executing read operations
        private T ExecuteReader<T>(string sql, Func<SqlDataReader, T> map, Action<SqlParameterCollection>? addParameters = null, CommandType commandType = CommandType.Text)
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

        // Generic helper for executing write operations
        private void ExecuteNonQuery(string sql, Action<SqlParameterCollection> addParameters, CommandType commandType = CommandType.StoredProcedure)
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

        public List<TipoDoc> GetAllTiposDoc() => ExecuteReader("SELECT id_tipo_doc, tipo_doc FROM tipo_doc;", reader =>
        {
            var list = new List<TipoDoc>();
            while (reader.Read())
            {
                list.Add(new TipoDoc { IdTipoDoc = (int)reader["id_tipo_doc"], Nombre = (string)reader["tipo_doc"] });
            }
            return list;
        });

        public List<Genero> GetAllGeneros() => ExecuteReader("SELECT id_genero, genero FROM generos;", reader =>
        {
            var list = new List<Genero>();
            while (reader.Read())
            {
                list.Add(new Genero { IdGenero = (int)reader["id_genero"], Nombre = (string)reader["genero"] });
            }
            return list;
        });

        public List<Rol> GetAllRoles() => ExecuteReader("SELECT id_rol, rol FROM roles;", reader =>
        {
            var list = new List<Rol>();
            while (reader.Read())
            {
                list.Add(new Rol { IdRol = (int)reader["id_rol"], Nombre = (string)reader["rol"] });
            }
            return list;
        });

        public List<Localidad> GetAllLocalidades() => ExecuteReader("SELECT id_localidad, localidad, id_partido FROM localidades;", reader =>
        {
            var list = new List<Localidad>();
            while (reader.Read())
            {
                list.Add(new Localidad { IdLocalidad = (int)reader["id_localidad"], Nombre = (string)reader["localidad"], IdPartido = (int)reader["id_partido"] });
            }
            return list;
        });

        public PoliticaSeguridad? GetPoliticaSeguridad() => ExecuteReader("SELECT TOP 1 * FROM politicas_seguridad;", reader =>
        {
            if (!reader.Read()) return null;
            return new PoliticaSeguridad
            {
                IdPolitica = (int)reader["id_politica"],
                MinCaracteres = reader["min_caracteres"] as int? ?? 8,
                CantPreguntas = reader["cant_preguntas"] as int? ?? 3,
                MayusYMinus = reader["mayus_y_minus"] as bool? ?? false,
                LetrasYNumeros = reader["letras_y_numeros"] as bool? ?? false,
                CaracterEspecial = reader["caracter_especial"] as bool? ?? false,
                Autenticacion2FA = reader["autenticacion_2fa"] as bool? ?? false,
                NoRepetirAnteriores = reader["no_repetir_anteriores"] as bool? ?? false,
                SinDatosPersonales = reader["sin_datos_personales"] as bool? ?? false
            };
        });

        public List<PreguntaSeguridad> GetPreguntasSeguridad() => ExecuteReader("SELECT id_pregunta, pregunta FROM preguntas_seguridad;", reader =>
        {
            var list = new List<PreguntaSeguridad>();
            while (reader.Read())
            {
                list.Add(new PreguntaSeguridad { IdPregunta = (int)reader["id_pregunta"], Pregunta = (string)reader["pregunta"] });
            }
            return list;
        });

        public TipoDoc? GetTipoDocByNombre(string nombre) => ExecuteReader("SELECT id_tipo_doc, tipo_doc FROM tipo_doc WHERE tipo_doc = @nombre;", reader =>
        {
            if (!reader.Read()) return null;
            return new TipoDoc { IdTipoDoc = (int)reader["id_tipo_doc"], Nombre = (string)reader["tipo_doc"] };
        }, p => p.AddWithValue("@nombre", nombre));

        public Localidad? GetLocalidadByNombre(string nombre) => ExecuteReader("SELECT id_localidad, localidad, id_partido FROM localidades WHERE localidad = @nombre;", reader =>
        {
            if (!reader.Read()) return null;
            return new Localidad { IdLocalidad = (int)reader["id_localidad"], Nombre = (string)reader["localidad"], IdPartido = (int)reader["id_partido"] };
        }, p => p.AddWithValue("@nombre", nombre));

        public Genero? GetGeneroByNombre(string nombre) => ExecuteReader("SELECT id_genero, genero FROM generos WHERE genero = @nombre;", reader =>
        {
            if (!reader.Read()) return null;
            return new Genero { IdGenero = (int)reader["id_genero"], Nombre = (string)reader["genero"] };
        }, p => p.AddWithValue("@nombre", nombre));

        public Rol? GetRolByNombre(string nombre) => ExecuteReader("SELECT id_rol, rol FROM roles WHERE rol = @nombre;", reader =>
        {
            if (!reader.Read()) return null;
            return new Rol { IdRol = (int)reader["id_rol"], Nombre = (string)reader["rol"] };
        }, p => p.AddWithValue("@nombre", nombre));

        public Persona? GetPersonaById(int id) => ExecuteReader("SELECT * FROM personas WHERE id_persona = @id;", reader =>
        {
            if (!reader.Read()) return null;
            return new Persona
            {
                IdPersona = (int)reader["id_persona"],
                Legajo = (int)reader["legajo"],
                Nombre = (string)reader["nombre"],
                Apellido = (string)reader["apellido"],
                IdTipoDoc = (int)reader["id_tipo_doc"],
                NumDoc = (string)reader["num_doc"],
                Cuil = reader["cuil"] as string,
                Calle = reader["calle"] as string,
                Altura = reader["altura"] as string,
                IdLocalidad = (int)reader["id_localidad"],
                IdGenero = (int)reader["id_genero"],
                Correo = reader["correo"] as string,
                FechaIngreso = (DateTime)reader["fecha_ingreso"]
            };
        }, p => p.AddWithValue("@id", id));

        public List<RespuestaSeguridad>? GetRespuestasSeguridadByUsuarioId(int idUsuario) => ExecuteReader("SELECT id_usuario, id_pregunta, respuesta FROM respuestas_seguridad WHERE id_usuario = @id_usuario;", reader =>
        {
            var list = new List<RespuestaSeguridad>();
            while (reader.Read())
            {
                list.Add(new RespuestaSeguridad { IdUsuario = (int)reader["id_usuario"], IdPregunta = (int)reader["id_pregunta"], Respuesta = (string)reader["respuesta"] });
            }
            return list;
        }, p => p.AddWithValue("@id_usuario", idUsuario));

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
            return new Usuario
            {
                IdUsuario = (int)reader["id_usuario"],
                UsuarioNombre = (string)reader["usuario"],
                ContrasenaScript = (byte[])reader["contrasena_script"],
                IdPersona = (int)reader["id_persona"],
                FechaBloqueo = (DateTime)reader["fecha_bloqueo"],
                NombreUsuarioBloqueo = reader["nombre_usuario_bloqueo"] as string,
                FechaUltimoCambio = (DateTime)reader["fecha_ultimo_cambio"],
                IdRol = (int)reader["id_rol"],
                IdPolitica = reader["id_politica"] as int?,
                CambioContrasenaObligatorio = (bool)reader["CambioContrasenaObligatorio"],
                Rol = new Rol { IdRol = (int)reader["rol_id_rol"], Nombre = (string)reader["rol"] }
            };
        }, p => p.AddWithValue("@usuario_nombre", nombre), CommandType.StoredProcedure);

        public List<Usuario> GetAllUsers() => ExecuteReader("sp_get_all_users", reader =>
        {
            var list = new List<Usuario>();
            while (reader.Read())
            {
                list.Add(new Usuario
                {
                    IdUsuario = (int)reader["id_usuario"],
                    UsuarioNombre = (string)reader["usuario"],
                    ContrasenaScript = (byte[])reader["contrasena_script"],
                    IdPersona = (int)reader["id_persona"],
                    FechaBloqueo = (DateTime)reader["fecha_bloqueo"],
                    NombreUsuarioBloqueo = reader["nombre_usuario_bloqueo"] as string,
                    FechaUltimoCambio = (DateTime)reader["fecha_ultimo_cambio"],
                    IdRol = (int)reader["id_rol"],
                    IdPolitica = reader["id_politica"] as int?,
                    CambioContrasenaObligatorio = (bool)reader["CambioContrasenaObligatorio"],
                    Rol = new Rol { IdRol = (int)reader["rol_id_rol"], Nombre = (string)reader["rol"] },
                    Persona = new Persona
                    {
                        IdPersona = (int)reader["persona_id_persona"],
                        Legajo = (int)reader["legajo"],
                        Nombre = (string)reader["nombre"],
                        Apellido = (string)reader["apellido"],
                        IdTipoDoc = (int)reader["id_tipo_doc"],
                        NumDoc = (string)reader["num_doc"],
                        Cuil = reader["cuil"] as string,
                        Calle = reader["calle"] as string,
                        Altura = reader["altura"] as string,
                        IdLocalidad = (int)reader["id_localidad"],
                        IdGenero = (int)reader["id_genero"],
                        Correo = reader["correo"] as string,
                        FechaIngreso = (DateTime)reader["fecha_ingreso"]
                    }
                });
            }
            return list;
        }, commandType: CommandType.StoredProcedure);

        public List<Persona> GetAllPersonas() => ExecuteReader("SELECT * FROM personas;", reader =>
        {
            var list = new List<Persona>();
            while (reader.Read())
            {
                list.Add(new Persona
                {
                    IdPersona = (int)reader["id_persona"],
                    Legajo = (int)reader["legajo"],
                    Nombre = (string)reader["nombre"],
                    Apellido = (string)reader["apellido"],
                    IdTipoDoc = (int)reader["id_tipo_doc"],
                    NumDoc = (string)reader["num_doc"],
                    Cuil = reader["cuil"] as string,
                    Calle = reader["calle"] as string,
                    Altura = reader["altura"] as string,
                    IdLocalidad = (int)reader["id_localidad"],
                    IdGenero = (int)reader["id_genero"],
                    Correo = reader["correo"] as string,
                    FechaIngreso = (DateTime)reader["fecha_ingreso"]
                });
            }
            return list;
        });

        public void AddHistorialContrasena(HistorialContrasena historial) => ExecuteNonQuery("sp_historial_contrasena", p =>
        {
            p.AddWithValue("@id_usuario", historial.IdUsuario);
            p.AddWithValue("@contrasena_script", historial.ContrasenaScript);
        });

        public void AddPersona(Persona persona) => ExecuteNonQuery("sp_insert_persona", p =>
        {
            p.AddWithValue("@legajo", persona.Legajo);
            p.AddWithValue("@nombre", persona.Nombre);
            p.AddWithValue("@apellido", persona.Apellido);
            p.AddWithValue("@id_tipo_doc", persona.IdTipoDoc);
            p.AddWithValue("@num_doc", persona.NumDoc);
            p.AddWithValue("@cuil", (object)persona.Cuil ?? DBNull.Value);
            p.AddWithValue("@calle", (object)persona.Calle ?? DBNull.Value);
            p.AddWithValue("@altura", (object)persona.Altura ?? DBNull.Value);
            p.AddWithValue("@id_localidad", persona.IdLocalidad);
            p.AddWithValue("@id_genero", persona.IdGenero);
            p.AddWithValue("@correo", (object)persona.Correo ?? DBNull.Value);
        });

        public void AddRespuestaSeguridad(RespuestaSeguridad respuesta) => ExecuteNonQuery("sp_insert_respuesta_seguridad", p =>
        {
            p.AddWithValue("@id_usuario", respuesta.IdUsuario);
            p.AddWithValue("@id_pregunta", respuesta.IdPregunta);
            p.AddWithValue("@respuesta", respuesta.Respuesta);
        });

        public void AddUsuario(Usuario usuario) => ExecuteNonQuery("sp_insert_usuario", p =>
        {
            p.AddWithValue("@usuario", usuario.UsuarioNombre);
            p.AddWithValue("@contrasena_script", usuario.ContrasenaScript);
            p.AddWithValue("@id_persona", usuario.IdPersona);
            p.AddWithValue("@fecha_bloqueo", usuario.FechaBloqueo);
            p.AddWithValue("@nombre_usuario_bloqueo", (object)usuario.NombreUsuarioBloqueo ?? DBNull.Value);
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
            p.AddWithValue("@nombre_usuario_bloqueo", (object)usuario.NombreUsuarioBloqueo ?? DBNull.Value);
            p.AddWithValue("@fecha_ultimo_cambio", usuario.FechaUltimoCambio);
            p.AddWithValue("@id_rol", usuario.IdRol);
            p.AddWithValue("@CambioContrasenaObligatorio", usuario.CambioContrasenaObligatorio);
        });

        public void UpdatePoliticaSeguridad(PoliticaSeguridad politica) => ExecuteNonQuery("sp_update_politica_seguridad", p =>
        {
            p.AddWithValue("@id_politica", politica.IdPolitica);
            p.AddWithValue("@min_caracteres", (object)politica.MinCaracteres ?? DBNull.Value);
            p.AddWithValue("@cant_preguntas", (object)politica.CantPreguntas ?? DBNull.Value);
            p.AddWithValue("@mayus_y_minus", (object)politica.MayusYMinus ?? DBNull.Value);
            p.AddWithValue("@letras_y_numeros", (object)politica.LetrasYNumeros ?? DBNull.Value);
            p.AddWithValue("@caracter_especial", (object)politica.CaracterEspecial ?? DBNull.Value);
            p.AddWithValue("@autenticacion_2fa", (object)politica.Autenticacion2FA ?? DBNull.Value);
            p.AddWithValue("@no_repetir_anteriores", (object)politica.NoRepetirAnteriores ?? DBNull.Value);
            p.AddWithValue("@sin_datos_personales", (object)politica.SinDatosPersonales ?? DBNull.Value);
        });

        public void DeleteUsuario(int usuarioId) => ExecuteNonQuery("sp_delete_usuario", p =>
        {
            p.AddWithValue("@id_usuario", usuarioId);
        });
    }
}
