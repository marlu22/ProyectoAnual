using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "generos",
                columns: table => new
                {
                    id_genero = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    genero = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_generos", x => x.id_genero);
                });

            migrationBuilder.CreateTable(
                name: "historial_contrasena",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_usuario = table.Column<int>(type: "INTEGER", nullable: false),
                    contrasena_script = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historial_contrasena", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permisos",
                columns: table => new
                {
                    id_permiso = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    permiso = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permisos", x => x.id_permiso);
                });

            migrationBuilder.CreateTable(
                name: "permisos_usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "INTEGER", nullable: false),
                    id_permiso = table.Column<int>(type: "INTEGER", nullable: false),
                    fecha_vencimiento = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permisos_usuarios", x => new { x.id_usuario, x.id_permiso });
                });

            migrationBuilder.CreateTable(
                name: "politicas_seguridad",
                columns: table => new
                {
                    id_politica = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    min_caracteres = table.Column<int>(type: "INTEGER", nullable: false),
                    cant_preguntas = table.Column<int>(type: "INTEGER", nullable: false),
                    mayus_y_minus = table.Column<bool>(type: "INTEGER", nullable: false),
                    letras_y_numeros = table.Column<bool>(type: "INTEGER", nullable: false),
                    caracter_especial = table.Column<bool>(type: "INTEGER", nullable: false),
                    autenticacion_2fa = table.Column<bool>(type: "INTEGER", nullable: false),
                    no_repetir_anteriores = table.Column<bool>(type: "INTEGER", nullable: false),
                    sin_datos_personales = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_politicas_seguridad", x => x.id_politica);
                });

            migrationBuilder.CreateTable(
                name: "preguntas_seguridad",
                columns: table => new
                {
                    id_pregunta = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    pregunta = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_preguntas_seguridad", x => x.id_pregunta);
                });

            migrationBuilder.CreateTable(
                name: "provincias",
                columns: table => new
                {
                    id_provincia = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    provincia = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provincias", x => x.id_provincia);
                });

            migrationBuilder.CreateTable(
                name: "respuestas_seguridad",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "INTEGER", nullable: false),
                    id_pregunta = table.Column<int>(type: "INTEGER", nullable: false),
                    respuesta = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_respuestas_seguridad", x => new { x.id_usuario, x.id_pregunta });
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    rol = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id_rol);
                });

            migrationBuilder.CreateTable(
                name: "tipo_doc",
                columns: table => new
                {
                    id_tipo_doc = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    tipo_doc = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipo_doc", x => x.id_tipo_doc);
                });

            migrationBuilder.CreateTable(
                name: "partidos",
                columns: table => new
                {
                    id_partido = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    partido = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    id_provincia = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partidos", x => x.id_partido);
                    table.ForeignKey(
                        name: "FK_partidos_provincias_id_provincia",
                        column: x => x.id_provincia,
                        principalTable: "provincias",
                        principalColumn: "id_provincia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rol_permiso",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "INTEGER", nullable: false),
                    id_permiso = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rol_permiso", x => new { x.id_rol, x.id_permiso });
                    table.ForeignKey(
                        name: "FK_rol_permiso_permisos_id_permiso",
                        column: x => x.id_permiso,
                        principalTable: "permisos",
                        principalColumn: "id_permiso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rol_permiso_roles_id_rol",
                        column: x => x.id_rol,
                        principalTable: "roles",
                        principalColumn: "id_rol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    usuario = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    contrasena_script = table.Column<byte[]>(type: "BLOB", nullable: false),
                    id_persona = table.Column<int>(type: "INTEGER", nullable: false),
                    fecha_bloqueo = table.Column<DateTime>(type: "TEXT", nullable: true),
                    nombre_usuario_bloqueo = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    fecha_ultimo_cambio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    id_rol = table.Column<int>(type: "INTEGER", nullable: false),
                    CambioContrasenaObligatorio = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id_usuario);
                    table.ForeignKey(
                        name: "FK_usuarios_roles_id_rol",
                        column: x => x.id_rol,
                        principalTable: "roles",
                        principalColumn: "id_rol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "localidades",
                columns: table => new
                {
                    id_localidad = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    localidad = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    id_partido = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_localidades", x => x.id_localidad);
                    table.ForeignKey(
                        name: "FK_localidades_partidos_id_partido",
                        column: x => x.id_partido,
                        principalTable: "partidos",
                        principalColumn: "id_partido",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personas",
                columns: table => new
                {
                    id_persona = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    legajo = table.Column<int>(type: "INTEGER", nullable: false),
                    nombre = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    apellido = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    id_tipo_doc = table.Column<int>(type: "INTEGER", nullable: false),
                    num_doc = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    cuil = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    calle = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    altura = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    id_localidad = table.Column<int>(type: "INTEGER", nullable: false),
                    id_genero = table.Column<int>(type: "INTEGER", nullable: false),
                    correo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personas", x => x.id_persona);
                    table.ForeignKey(
                        name: "FK_personas_generos_id_genero",
                        column: x => x.id_genero,
                        principalTable: "generos",
                        principalColumn: "id_genero",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personas_localidades_id_localidad",
                        column: x => x.id_localidad,
                        principalTable: "localidades",
                        principalColumn: "id_localidad",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personas_tipo_doc_id_tipo_doc",
                        column: x => x.id_tipo_doc,
                        principalTable: "tipo_doc",
                        principalColumn: "id_tipo_doc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contactos",
                columns: table => new
                {
                    id_contacto = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    celular = table.Column<string>(type: "TEXT", nullable: false),
                    id_persona = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contactos", x => x.id_contacto);
                    table.ForeignKey(
                        name: "FK_contactos_personas_id_persona",
                        column: x => x.id_persona,
                        principalTable: "personas",
                        principalColumn: "id_persona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contactos_id_persona",
                table: "contactos",
                column: "id_persona");

            migrationBuilder.CreateIndex(
                name: "IX_localidades_id_partido",
                table: "localidades",
                column: "id_partido");

            migrationBuilder.CreateIndex(
                name: "IX_partidos_id_provincia",
                table: "partidos",
                column: "id_provincia");

            migrationBuilder.CreateIndex(
                name: "IX_personas_id_genero",
                table: "personas",
                column: "id_genero");

            migrationBuilder.CreateIndex(
                name: "IX_personas_id_localidad",
                table: "personas",
                column: "id_localidad");

            migrationBuilder.CreateIndex(
                name: "IX_personas_id_tipo_doc",
                table: "personas",
                column: "id_tipo_doc");

            migrationBuilder.CreateIndex(
                name: "IX_rol_permiso_id_permiso",
                table: "rol_permiso",
                column: "id_permiso");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_id_rol",
                table: "usuarios",
                column: "id_rol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contactos");

            migrationBuilder.DropTable(
                name: "historial_contrasena");

            migrationBuilder.DropTable(
                name: "permisos_usuarios");

            migrationBuilder.DropTable(
                name: "politicas_seguridad");

            migrationBuilder.DropTable(
                name: "preguntas_seguridad");

            migrationBuilder.DropTable(
                name: "respuestas_seguridad");

            migrationBuilder.DropTable(
                name: "rol_permiso");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "personas");

            migrationBuilder.DropTable(
                name: "permisos");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "generos");

            migrationBuilder.DropTable(
                name: "localidades");

            migrationBuilder.DropTable(
                name: "tipo_doc");

            migrationBuilder.DropTable(
                name: "partidos");

            migrationBuilder.DropTable(
                name: "provincias");
        }
    }
}
