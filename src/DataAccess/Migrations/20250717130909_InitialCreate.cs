using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    IdGenero = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.IdGenero);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    IdPermiso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.IdPermiso);
                });

            migrationBuilder.CreateTable(
                name: "PoliticasSeguridad",
                columns: table => new
                {
                    IdPolitica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MayusYMinus = table.Column<bool>(type: "bit", nullable: false),
                    LetrasYNumeros = table.Column<bool>(type: "bit", nullable: false),
                    CaracterEspecial = table.Column<bool>(type: "bit", nullable: false),
                    Autenticacion2FA = table.Column<bool>(type: "bit", nullable: false),
                    NoRepetirAnteriores = table.Column<bool>(type: "bit", nullable: false),
                    SinDatosPersonales = table.Column<bool>(type: "bit", nullable: false),
                    MinCaracteres = table.Column<int>(type: "int", nullable: false),
                    CantPreguntas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticasSeguridad", x => x.IdPolitica);
                });

            migrationBuilder.CreateTable(
                name: "PreguntasSeguridad",
                columns: table => new
                {
                    IdPregunta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pregunta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreguntasSeguridad", x => x.IdPregunta);
                });

            migrationBuilder.CreateTable(
                name: "Provincias",
                columns: table => new
                {
                    IdProvincia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.IdProvincia);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "TiposDoc",
                columns: table => new
                {
                    IdTipoDoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDoc", x => x.IdTipoDoc);
                });

            migrationBuilder.CreateTable(
                name: "Partidos",
                columns: table => new
                {
                    IdPartido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdProvincia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidos", x => x.IdPartido);
                    table.ForeignKey(
                        name: "FK_Partidos_Provincias_IdProvincia",
                        column: x => x.IdProvincia,
                        principalTable: "Provincias",
                        principalColumn: "IdProvincia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolPermisos",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    IdPermiso = table.Column<int>(type: "int", nullable: false),
                    RolIdRol = table.Column<int>(type: "int", nullable: true),
                    PermisoIdPermiso = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPermisos", x => new { x.IdRol, x.IdPermiso });
                    table.ForeignKey(
                        name: "FK_RolPermisos_Permisos_PermisoIdPermiso",
                        column: x => x.PermisoIdPermiso,
                        principalTable: "Permisos",
                        principalColumn: "IdPermiso");
                    table.ForeignKey(
                        name: "FK_RolPermisos_Roles_RolIdRol",
                        column: x => x.RolIdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol");
                });

            migrationBuilder.CreateTable(
                name: "Localidades",
                columns: table => new
                {
                    IdLocalidad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPartido = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localidades", x => x.IdLocalidad);
                    table.ForeignKey(
                        name: "FK_Localidades_Partidos_IdPartido",
                        column: x => x.IdPartido,
                        principalTable: "Partidos",
                        principalColumn: "IdPartido",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    IdPersona = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Legajo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoDoc = table.Column<int>(type: "int", nullable: false),
                    NumDoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cuil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Calle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Altura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdLocalidad = table.Column<int>(type: "int", nullable: false),
                    IdGenero = table.Column<int>(type: "int", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.IdPersona);
                    table.ForeignKey(
                        name: "FK_Personas_Generos_IdGenero",
                        column: x => x.IdGenero,
                        principalTable: "Generos",
                        principalColumn: "IdGenero",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personas_Localidades_IdLocalidad",
                        column: x => x.IdLocalidad,
                        principalTable: "Localidades",
                        principalColumn: "IdLocalidad",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personas_TiposDoc_IdTipoDoc",
                        column: x => x.IdTipoDoc,
                        principalTable: "TiposDoc",
                        principalColumn: "IdTipoDoc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    TipoContacto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonaIdPersona = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Contactos_Personas_PersonaIdPersona",
                        column: x => x.PersonaIdPersona,
                        principalTable: "Personas",
                        principalColumn: "IdPersona");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContrasenaScript = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    FechaBloqueo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombreUsuarioBloqueo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaUltimoCambio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    IdPolitica = table.Column<int>(type: "int", nullable: true),
                    CambioContrasenaObligatorio = table.Column<bool>(type: "bit", nullable: false),
                    PoliticaSeguridadIdPolitica = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Personas_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Personas",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_PoliticasSeguridad_PoliticaSeguridadIdPolitica",
                        column: x => x.PoliticaSeguridadIdPolitica,
                        principalTable: "PoliticasSeguridad",
                        principalColumn: "IdPolitica");
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistorialContrasenas",
                columns: table => new
                {
                    IdHistorial = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    ContrasenaScript = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FechaCambio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioIdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialContrasenas", x => x.IdHistorial);
                    table.ForeignKey(
                        name: "FK_HistorialContrasenas_Usuarios_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "PermisosUsuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdPermiso = table.Column<int>(type: "int", nullable: false),
                    UsuarioIdUsuario = table.Column<int>(type: "int", nullable: true),
                    PermisoIdPermiso = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermisosUsuarios", x => new { x.IdUsuario, x.IdPermiso });
                    table.ForeignKey(
                        name: "FK_PermisosUsuarios_Permisos_PermisoIdPermiso",
                        column: x => x.PermisoIdPermiso,
                        principalTable: "Permisos",
                        principalColumn: "IdPermiso");
                    table.ForeignKey(
                        name: "FK_PermisosUsuarios_Usuarios_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "RespuestasSeguridad",
                columns: table => new
                {
                    IdRespuesta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdPregunta = table.Column<int>(type: "int", nullable: false),
                    Respuesta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioIdUsuario = table.Column<int>(type: "int", nullable: true),
                    PreguntaIdPregunta = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestasSeguridad", x => x.IdRespuesta);
                    table.ForeignKey(
                        name: "FK_RespuestasSeguridad_PreguntasSeguridad_PreguntaIdPregunta",
                        column: x => x.PreguntaIdPregunta,
                        principalTable: "PreguntasSeguridad",
                        principalColumn: "IdPregunta");
                    table.ForeignKey(
                        name: "FK_RespuestasSeguridad_Usuarios_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "IdGenero", "Nombre" },
                values: new object[,]
                {
                    { 1, "Masculino" },
                    { 2, "Femenino" }
                });

            migrationBuilder.InsertData(
                table: "PoliticasSeguridad",
                columns: new[] { "IdPolitica", "Autenticacion2FA", "CantPreguntas", "CaracterEspecial", "LetrasYNumeros", "MayusYMinus", "MinCaracteres", "NoRepetirAnteriores", "SinDatosPersonales" },
                values: new object[] { 1, false, 2, true, true, true, 8, true, true });

            migrationBuilder.InsertData(
                table: "Provincias",
                columns: new[] { "IdProvincia", "Nombre" },
                values: new object[,]
                {
                    { 1, "Buenos Aires" },
                    { 2, "Catamarca" },
                    { 3, "Chaco" },
                    { 4, "Chubut" },
                    { 5, "Córdoba" },
                    { 6, "Corrientes" },
                    { 7, "Entre Ríos" },
                    { 8, "Formosa" },
                    { 9, "Jujuy" },
                    { 10, "La Pampa" },
                    { 11, "La Rioja" },
                    { 12, "Mendoza" },
                    { 13, "Misiones" },
                    { 14, "Neuquén" },
                    { 15, "Río Negro" },
                    { 16, "Salta" },
                    { 17, "San Juan" },
                    { 18, "San Luis" },
                    { 19, "Santa Cruz" },
                    { 20, "Santa Fe" },
                    { 21, "Santiago del Estero" },
                    { 22, "Tierra del Fuego" },
                    { 23, "Tucumán" },
                    { 24, "CABA" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "IdRol", "Nombre" },
                values: new object[] { 1, "Administrador" });

            migrationBuilder.InsertData(
                table: "TiposDoc",
                columns: new[] { "IdTipoDoc", "Nombre" },
                values: new object[,]
                {
                    { 1, "DNI" },
                    { 2, "Pasaporte" }
                });

            migrationBuilder.InsertData(
                table: "Partidos",
                columns: new[] { "IdPartido", "IdProvincia", "Nombre" },
                values: new object[,]
                {
                    { 1, 1, "La Plata" },
                    { 2, 1, "Quilmes" },
                    { 3, 1, "Lomas de Zamora" },
                    { 4, 5, "Córdoba Capital" },
                    { 5, 5, "Río Cuarto" },
                    { 6, 12, "Mendoza Capital" },
                    { 7, 12, "Godoy Cruz" },
                    { 8, 20, "Rosario" },
                    { 9, 20, "Santa Fe Capital" },
                    { 10, 24, "Comuna 1" }
                });

            migrationBuilder.InsertData(
                table: "Localidades",
                columns: new[] { "IdLocalidad", "IdPartido", "Nombre" },
                values: new object[,]
                {
                    { 1, 1, "La Plata" },
                    { 2, 1, "City Bell" },
                    { 3, 1, "Gonnet" },
                    { 4, 2, "Quilmes" },
                    { 5, 2, "Bernal" },
                    { 6, 3, "Lomas de Zamora" },
                    { 7, 3, "Banfield" },
                    { 8, 4, "Córdoba" },
                    { 9, 4, "Alta Córdoba" },
                    { 10, 5, "Río Cuarto" },
                    { 11, 5, "Las Higueras" },
                    { 12, 6, "Mendoza" },
                    { 13, 6, "Guaymallén" },
                    { 14, 7, "Godoy Cruz" },
                    { 15, 7, "Las Heras" },
                    { 16, 8, "Rosario" },
                    { 17, 8, "Funes" },
                    { 18, 9, "Santa Fe" },
                    { 19, 9, "Santo Tomé" },
                    { 20, 10, "Retiro" },
                    { 21, 10, "San Nicolás" },
                    { 22, 1, "Tolosa" },
                    { 23, 2, "Ezpeleta" },
                    { 24, 3, "Temperley" },
                    { 25, 4, "Nueva Córdoba" },
                    { 26, 5, "Holmberg" },
                    { 27, 6, "Luján de Cuyo" },
                    { 28, 7, "Maipú" },
                    { 29, 8, "Roldán" },
                    { 30, 9, "Recreo" },
                    { 31, 10, "Recoleta" },
                    { 32, 10, "Palermo" },
                    { 33, 10, "Belgrano" }
                });

            migrationBuilder.InsertData(
                table: "Personas",
                columns: new[] { "IdPersona", "Altura", "Apellido", "Calle", "Correo", "Cuil", "FechaIngreso", "IdGenero", "IdLocalidad", "IdTipoDoc", "Legajo", "Nombre", "NumDoc" },
                values: new object[] { 1, "123", "Pérez", "Calle Falsa", "juan.perez@example.com", "20-12345678-9", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 1, "1001", "Juan", "12345678" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "IdUsuario", "CambioContrasenaObligatorio", "ContrasenaScript", "FechaBloqueo", "FechaUltimoCambio", "IdPersona", "IdPolitica", "IdRol", "NombreUsuarioBloqueo", "PoliticaSeguridadIdPolitica", "UsuarioNombre" },
                values: new object[] { 1, true, new byte[] { 169, 51, 211, 112, 211, 78, 1, 75, 47, 222, 42, 90, 127, 165, 8, 235, 201, 135, 125, 114, 131, 201, 74, 69, 193, 214, 198, 141, 75, 147, 49, 62 }, new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 1, null, null, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_PersonaIdPersona",
                table: "Contactos",
                column: "PersonaIdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialContrasenas_UsuarioIdUsuario",
                table: "HistorialContrasenas",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Localidades_IdPartido",
                table: "Localidades",
                column: "IdPartido");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_IdProvincia",
                table: "Partidos",
                column: "IdProvincia");

            migrationBuilder.CreateIndex(
                name: "IX_PermisosUsuarios_PermisoIdPermiso",
                table: "PermisosUsuarios",
                column: "PermisoIdPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_PermisosUsuarios_UsuarioIdUsuario",
                table: "PermisosUsuarios",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_IdGenero",
                table: "Personas",
                column: "IdGenero");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_IdLocalidad",
                table: "Personas",
                column: "IdLocalidad");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_IdTipoDoc",
                table: "Personas",
                column: "IdTipoDoc");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasSeguridad_PreguntaIdPregunta",
                table: "RespuestasSeguridad",
                column: "PreguntaIdPregunta");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasSeguridad_UsuarioIdUsuario",
                table: "RespuestasSeguridad",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_RolPermisos_PermisoIdPermiso",
                table: "RolPermisos",
                column: "PermisoIdPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_RolPermisos_RolIdRol",
                table: "RolPermisos",
                column: "RolIdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdPersona",
                table: "Usuarios",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdRol",
                table: "Usuarios",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PoliticaSeguridadIdPolitica",
                table: "Usuarios",
                column: "PoliticaSeguridadIdPolitica");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.DropTable(
                name: "HistorialContrasenas");

            migrationBuilder.DropTable(
                name: "PermisosUsuarios");

            migrationBuilder.DropTable(
                name: "RespuestasSeguridad");

            migrationBuilder.DropTable(
                name: "RolPermisos");

            migrationBuilder.DropTable(
                name: "PreguntasSeguridad");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "PoliticasSeguridad");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Localidades");

            migrationBuilder.DropTable(
                name: "TiposDoc");

            migrationBuilder.DropTable(
                name: "Partidos");

            migrationBuilder.DropTable(
                name: "Provincias");
        }
    }
}
