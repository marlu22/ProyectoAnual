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
                    id_genero = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    genero = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_generos", x => x.id_genero);
                });

            migrationBuilder.CreateTable(
                name: "historial_contrasena",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    fecha_cambio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    contrasena_script = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historial_contrasena", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permisos",
                columns: table => new
                {
                    id_permiso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    permiso = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permisos", x => x.id_permiso);
                });

            migrationBuilder.CreateTable(
                name: "politicas_seguridad",
                columns: table => new
                {
                    id_politica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SinDatosPersonales = table.Column<bool>(type: "bit", nullable: false),
                    MinCaracteres = table.Column<int>(type: "int", nullable: false),
                    CantPreguntas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_politicas_seguridad", x => x.id_politica);
                });

            migrationBuilder.CreateTable(
                name: "preguntas_seguridad",
                columns: table => new
                {
                    id_pregunta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pregunta = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_preguntas_seguridad", x => x.id_pregunta);
                });

            migrationBuilder.CreateTable(
                name: "provincias",
                columns: table => new
                {
                    id_provincia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    provincia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provincias", x => x.id_provincia);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id_rol);
                });

            migrationBuilder.CreateTable(
                name: "tipo_doc",
                columns: table => new
                {
                    id_tipo_doc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo_doc = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipo_doc", x => x.id_tipo_doc);
                });

            migrationBuilder.CreateTable(
                name: "partidos",
                columns: table => new
                {
                    id_partido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    partido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    id_provincia = table.Column<int>(type: "int", nullable: false)
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
                    id_rol = table.Column<int>(type: "int", nullable: false),
                    id_permiso = table.Column<int>(type: "int", nullable: false)
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
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioNombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ContrasenaScript = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    FechaBloqueo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombreUsuarioBloqueo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FechaUltimoCambio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id_usuario);
                    table.ForeignKey(
                        name: "FK_usuarios_roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "roles",
                        principalColumn: "id_rol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "localidades",
                columns: table => new
                {
                    id_localidad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    localidad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    id_partido = table.Column<int>(type: "int", nullable: false)
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
                name: "permiso_usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    id_permiso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permiso_usuario", x => new { x.id_usuario, x.id_permiso });
                    table.ForeignKey(
                        name: "FK_permiso_usuario_permisos_id_permiso",
                        column: x => x.id_permiso,
                        principalTable: "permisos",
                        principalColumn: "id_permiso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_permiso_usuario_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "respuestas_seguridad",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    id_pregunta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_respuestas_seguridad", x => new { x.id_usuario, x.id_pregunta });
                    table.ForeignKey(
                        name: "FK_respuestas_seguridad_preguntas_seguridad_id_pregunta",
                        column: x => x.id_pregunta,
                        principalTable: "preguntas_seguridad",
                        principalColumn: "id_pregunta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_respuestas_seguridad_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personas",
                columns: table => new
                {
                    id_persona = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Legajo = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IdTipoDoc = table.Column<int>(type: "int", nullable: false),
                    NumDoc = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cuil = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Calle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Altura = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IdLocalidad = table.Column<int>(type: "int", nullable: false),
                    IdGenero = table.Column<int>(type: "int", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personas", x => x.id_persona);
                    table.ForeignKey(
                        name: "FK_personas_generos_IdGenero",
                        column: x => x.IdGenero,
                        principalTable: "generos",
                        principalColumn: "id_genero",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personas_localidades_IdLocalidad",
                        column: x => x.IdLocalidad,
                        principalTable: "localidades",
                        principalColumn: "id_localidad",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personas_tipo_doc_IdTipoDoc",
                        column: x => x.IdTipoDoc,
                        principalTable: "tipo_doc",
                        principalColumn: "id_tipo_doc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contactos",
                columns: table => new
                {
                    id_contacto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersona = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contactos", x => x.id_contacto);
                    table.ForeignKey(
                        name: "FK_contactos_personas_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "personas",
                        principalColumn: "id_persona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contactos_IdPersona",
                table: "contactos",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_localidades_id_partido",
                table: "localidades",
                column: "id_partido");

            migrationBuilder.CreateIndex(
                name: "IX_partidos_id_provincia",
                table: "partidos",
                column: "id_provincia");

            migrationBuilder.CreateIndex(
                name: "IX_permiso_usuario_id_permiso",
                table: "permiso_usuario",
                column: "id_permiso");

            migrationBuilder.CreateIndex(
                name: "IX_personas_IdGenero",
                table: "personas",
                column: "IdGenero");

            migrationBuilder.CreateIndex(
                name: "IX_personas_IdLocalidad",
                table: "personas",
                column: "IdLocalidad");

            migrationBuilder.CreateIndex(
                name: "IX_personas_IdTipoDoc",
                table: "personas",
                column: "IdTipoDoc");

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_seguridad_id_pregunta",
                table: "respuestas_seguridad",
                column: "id_pregunta");

            migrationBuilder.CreateIndex(
                name: "IX_rol_permiso_id_permiso",
                table: "rol_permiso",
                column: "id_permiso");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_IdRol",
                table: "usuarios",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contactos");

            migrationBuilder.DropTable(
                name: "historial_contrasena");

            migrationBuilder.DropTable(
                name: "permiso_usuario");

            migrationBuilder.DropTable(
                name: "politicas_seguridad");

            migrationBuilder.DropTable(
                name: "respuestas_seguridad");

            migrationBuilder.DropTable(
                name: "rol_permiso");

            migrationBuilder.DropTable(
                name: "personas");

            migrationBuilder.DropTable(
                name: "preguntas_seguridad");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "permisos");

            migrationBuilder.DropTable(
                name: "generos");

            migrationBuilder.DropTable(
                name: "localidades");

            migrationBuilder.DropTable(
                name: "tipo_doc");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "partidos");

            migrationBuilder.DropTable(
                name: "provincias");
        }
    }
}
