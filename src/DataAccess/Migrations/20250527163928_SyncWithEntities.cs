using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SyncWithEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contactos_personas_IdPersona",
                table: "contactos");

            migrationBuilder.DropForeignKey(
                name: "FK_permiso_usuario_permisos_id_permiso",
                table: "permiso_usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_permiso_usuario_usuarios_id_usuario",
                table: "permiso_usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_personas_generos_IdGenero",
                table: "personas");

            migrationBuilder.DropForeignKey(
                name: "FK_personas_localidades_IdLocalidad",
                table: "personas");

            migrationBuilder.DropForeignKey(
                name: "FK_personas_tipo_doc_IdTipoDoc",
                table: "personas");

            migrationBuilder.DropForeignKey(
                name: "FK_respuestas_seguridad_preguntas_seguridad_id_pregunta",
                table: "respuestas_seguridad");

            migrationBuilder.DropForeignKey(
                name: "FK_respuestas_seguridad_usuarios_id_usuario",
                table: "respuestas_seguridad");

            migrationBuilder.DropForeignKey(
                name: "FK_usuarios_roles_IdRol",
                table: "usuarios");

            migrationBuilder.DropIndex(
                name: "IX_respuestas_seguridad_id_pregunta",
                table: "respuestas_seguridad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_permiso_usuario",
                table: "permiso_usuario");

            migrationBuilder.DropIndex(
                name: "IX_permiso_usuario_id_permiso",
                table: "permiso_usuario");

            migrationBuilder.DropColumn(
                name: "fecha_cambio",
                table: "historial_contrasena");

            migrationBuilder.RenameTable(
                name: "permiso_usuario",
                newName: "permisos_usuarios");

            migrationBuilder.RenameColumn(
                name: "UsuarioNombre",
                table: "usuarios",
                newName: "usuario");

            migrationBuilder.RenameColumn(
                name: "NombreUsuarioBloqueo",
                table: "usuarios",
                newName: "nombre_usuario_bloqueo");

            migrationBuilder.RenameColumn(
                name: "IdRol",
                table: "usuarios",
                newName: "id_rol");

            migrationBuilder.RenameColumn(
                name: "IdPersona",
                table: "usuarios",
                newName: "id_persona");

            migrationBuilder.RenameColumn(
                name: "FechaUltimoCambio",
                table: "usuarios",
                newName: "fecha_ultimo_cambio");

            migrationBuilder.RenameColumn(
                name: "FechaBloqueo",
                table: "usuarios",
                newName: "fecha_bloqueo");

            migrationBuilder.RenameColumn(
                name: "ContrasenaScript",
                table: "usuarios",
                newName: "contrasena_script");

            migrationBuilder.RenameIndex(
                name: "IX_usuarios_IdRol",
                table: "usuarios",
                newName: "IX_usuarios_id_rol");

            migrationBuilder.RenameColumn(
                name: "SinDatosPersonales",
                table: "politicas_seguridad",
                newName: "sin_datos_personales");

            migrationBuilder.RenameColumn(
                name: "MinCaracteres",
                table: "politicas_seguridad",
                newName: "min_caracteres");

            migrationBuilder.RenameColumn(
                name: "CantPreguntas",
                table: "politicas_seguridad",
                newName: "cant_preguntas");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "personas",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "Legajo",
                table: "personas",
                newName: "legajo");

            migrationBuilder.RenameColumn(
                name: "Cuil",
                table: "personas",
                newName: "cuil");

            migrationBuilder.RenameColumn(
                name: "Correo",
                table: "personas",
                newName: "correo");

            migrationBuilder.RenameColumn(
                name: "Calle",
                table: "personas",
                newName: "calle");

            migrationBuilder.RenameColumn(
                name: "Apellido",
                table: "personas",
                newName: "apellido");

            migrationBuilder.RenameColumn(
                name: "Altura",
                table: "personas",
                newName: "altura");

            migrationBuilder.RenameColumn(
                name: "NumDoc",
                table: "personas",
                newName: "num_doc");

            migrationBuilder.RenameColumn(
                name: "IdTipoDoc",
                table: "personas",
                newName: "id_tipo_doc");

            migrationBuilder.RenameColumn(
                name: "IdLocalidad",
                table: "personas",
                newName: "id_localidad");

            migrationBuilder.RenameColumn(
                name: "IdGenero",
                table: "personas",
                newName: "id_genero");

            migrationBuilder.RenameIndex(
                name: "IX_personas_IdTipoDoc",
                table: "personas",
                newName: "IX_personas_id_tipo_doc");

            migrationBuilder.RenameIndex(
                name: "IX_personas_IdLocalidad",
                table: "personas",
                newName: "IX_personas_id_localidad");

            migrationBuilder.RenameIndex(
                name: "IX_personas_IdGenero",
                table: "personas",
                newName: "IX_personas_id_genero");

            migrationBuilder.RenameColumn(
                name: "IdPersona",
                table: "contactos",
                newName: "id_persona");

            migrationBuilder.RenameIndex(
                name: "IX_contactos_IdPersona",
                table: "contactos",
                newName: "IX_contactos_id_persona");

            migrationBuilder.AddColumn<string>(
                name: "respuesta",
                table: "respuestas_seguridad",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "autenticacion_2fa",
                table: "politicas_seguridad",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "caracter_especial",
                table: "politicas_seguridad",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "letras_y_numeros",
                table: "politicas_seguridad",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "mayus_y_minus",
                table: "politicas_seguridad",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "no_repetir_anteriores",
                table: "politicas_seguridad",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "celular",
                table: "contactos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "contactos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_vencimiento",
                table: "permisos_usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_permisos_usuarios",
                table: "permisos_usuarios",
                columns: new[] { "id_usuario", "id_permiso" });

            migrationBuilder.AddForeignKey(
                name: "FK_contactos_personas_id_persona",
                table: "contactos",
                column: "id_persona",
                principalTable: "personas",
                principalColumn: "id_persona",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_personas_generos_id_genero",
                table: "personas",
                column: "id_genero",
                principalTable: "generos",
                principalColumn: "id_genero",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_personas_localidades_id_localidad",
                table: "personas",
                column: "id_localidad",
                principalTable: "localidades",
                principalColumn: "id_localidad",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_personas_tipo_doc_id_tipo_doc",
                table: "personas",
                column: "id_tipo_doc",
                principalTable: "tipo_doc",
                principalColumn: "id_tipo_doc",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_usuarios_roles_id_rol",
                table: "usuarios",
                column: "id_rol",
                principalTable: "roles",
                principalColumn: "id_rol",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contactos_personas_id_persona",
                table: "contactos");

            migrationBuilder.DropForeignKey(
                name: "FK_personas_generos_id_genero",
                table: "personas");

            migrationBuilder.DropForeignKey(
                name: "FK_personas_localidades_id_localidad",
                table: "personas");

            migrationBuilder.DropForeignKey(
                name: "FK_personas_tipo_doc_id_tipo_doc",
                table: "personas");

            migrationBuilder.DropForeignKey(
                name: "FK_usuarios_roles_id_rol",
                table: "usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_permisos_usuarios",
                table: "permisos_usuarios");

            migrationBuilder.DropColumn(
                name: "respuesta",
                table: "respuestas_seguridad");

            migrationBuilder.DropColumn(
                name: "autenticacion_2fa",
                table: "politicas_seguridad");

            migrationBuilder.DropColumn(
                name: "caracter_especial",
                table: "politicas_seguridad");

            migrationBuilder.DropColumn(
                name: "letras_y_numeros",
                table: "politicas_seguridad");

            migrationBuilder.DropColumn(
                name: "mayus_y_minus",
                table: "politicas_seguridad");

            migrationBuilder.DropColumn(
                name: "no_repetir_anteriores",
                table: "politicas_seguridad");

            migrationBuilder.DropColumn(
                name: "celular",
                table: "contactos");

            migrationBuilder.DropColumn(
                name: "email",
                table: "contactos");

            migrationBuilder.DropColumn(
                name: "fecha_vencimiento",
                table: "permisos_usuarios");

            migrationBuilder.RenameTable(
                name: "permisos_usuarios",
                newName: "permiso_usuario");

            migrationBuilder.RenameColumn(
                name: "usuario",
                table: "usuarios",
                newName: "UsuarioNombre");

            migrationBuilder.RenameColumn(
                name: "nombre_usuario_bloqueo",
                table: "usuarios",
                newName: "NombreUsuarioBloqueo");

            migrationBuilder.RenameColumn(
                name: "id_rol",
                table: "usuarios",
                newName: "IdRol");

            migrationBuilder.RenameColumn(
                name: "id_persona",
                table: "usuarios",
                newName: "IdPersona");

            migrationBuilder.RenameColumn(
                name: "fecha_ultimo_cambio",
                table: "usuarios",
                newName: "FechaUltimoCambio");

            migrationBuilder.RenameColumn(
                name: "fecha_bloqueo",
                table: "usuarios",
                newName: "FechaBloqueo");

            migrationBuilder.RenameColumn(
                name: "contrasena_script",
                table: "usuarios",
                newName: "ContrasenaScript");

            migrationBuilder.RenameIndex(
                name: "IX_usuarios_id_rol",
                table: "usuarios",
                newName: "IX_usuarios_IdRol");

            migrationBuilder.RenameColumn(
                name: "sin_datos_personales",
                table: "politicas_seguridad",
                newName: "SinDatosPersonales");

            migrationBuilder.RenameColumn(
                name: "min_caracteres",
                table: "politicas_seguridad",
                newName: "MinCaracteres");

            migrationBuilder.RenameColumn(
                name: "cant_preguntas",
                table: "politicas_seguridad",
                newName: "CantPreguntas");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "personas",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "legajo",
                table: "personas",
                newName: "Legajo");

            migrationBuilder.RenameColumn(
                name: "cuil",
                table: "personas",
                newName: "Cuil");

            migrationBuilder.RenameColumn(
                name: "correo",
                table: "personas",
                newName: "Correo");

            migrationBuilder.RenameColumn(
                name: "calle",
                table: "personas",
                newName: "Calle");

            migrationBuilder.RenameColumn(
                name: "apellido",
                table: "personas",
                newName: "Apellido");

            migrationBuilder.RenameColumn(
                name: "altura",
                table: "personas",
                newName: "Altura");

            migrationBuilder.RenameColumn(
                name: "num_doc",
                table: "personas",
                newName: "NumDoc");

            migrationBuilder.RenameColumn(
                name: "id_tipo_doc",
                table: "personas",
                newName: "IdTipoDoc");

            migrationBuilder.RenameColumn(
                name: "id_localidad",
                table: "personas",
                newName: "IdLocalidad");

            migrationBuilder.RenameColumn(
                name: "id_genero",
                table: "personas",
                newName: "IdGenero");

            migrationBuilder.RenameIndex(
                name: "IX_personas_id_tipo_doc",
                table: "personas",
                newName: "IX_personas_IdTipoDoc");

            migrationBuilder.RenameIndex(
                name: "IX_personas_id_localidad",
                table: "personas",
                newName: "IX_personas_IdLocalidad");

            migrationBuilder.RenameIndex(
                name: "IX_personas_id_genero",
                table: "personas",
                newName: "IX_personas_IdGenero");

            migrationBuilder.RenameColumn(
                name: "id_persona",
                table: "contactos",
                newName: "IdPersona");

            migrationBuilder.RenameIndex(
                name: "IX_contactos_id_persona",
                table: "contactos",
                newName: "IX_contactos_IdPersona");

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_cambio",
                table: "historial_contrasena",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_permiso_usuario",
                table: "permiso_usuario",
                columns: new[] { "id_usuario", "id_permiso" });

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_seguridad_id_pregunta",
                table: "respuestas_seguridad",
                column: "id_pregunta");

            migrationBuilder.CreateIndex(
                name: "IX_permiso_usuario_id_permiso",
                table: "permiso_usuario",
                column: "id_permiso");

            migrationBuilder.AddForeignKey(
                name: "FK_contactos_personas_IdPersona",
                table: "contactos",
                column: "IdPersona",
                principalTable: "personas",
                principalColumn: "id_persona",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_permiso_usuario_permisos_id_permiso",
                table: "permiso_usuario",
                column: "id_permiso",
                principalTable: "permisos",
                principalColumn: "id_permiso",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_permiso_usuario_usuarios_id_usuario",
                table: "permiso_usuario",
                column: "id_usuario",
                principalTable: "usuarios",
                principalColumn: "id_usuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_personas_generos_IdGenero",
                table: "personas",
                column: "IdGenero",
                principalTable: "generos",
                principalColumn: "id_genero",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_personas_localidades_IdLocalidad",
                table: "personas",
                column: "IdLocalidad",
                principalTable: "localidades",
                principalColumn: "id_localidad",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_personas_tipo_doc_IdTipoDoc",
                table: "personas",
                column: "IdTipoDoc",
                principalTable: "tipo_doc",
                principalColumn: "id_tipo_doc",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_respuestas_seguridad_preguntas_seguridad_id_pregunta",
                table: "respuestas_seguridad",
                column: "id_pregunta",
                principalTable: "preguntas_seguridad",
                principalColumn: "id_pregunta",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_respuestas_seguridad_usuarios_id_usuario",
                table: "respuestas_seguridad",
                column: "id_usuario",
                principalTable: "usuarios",
                principalColumn: "id_usuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_usuarios_roles_IdRol",
                table: "usuarios",
                column: "IdRol",
                principalTable: "roles",
                principalColumn: "id_rol",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
