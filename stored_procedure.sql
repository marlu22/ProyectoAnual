-- 1. provincias

DROP PROCEDURE IF EXISTS sp_insert_prov;
GO
CREATE PROCEDURE sp_insert_prov
    @provincia VARCHAR(50)
AS
BEGIN
    INSERT INTO provincias (provincia)
    VALUES (@provincia)
END
GO

DROP PROCEDURE IF EXISTS sp_update_prov;
GO
CREATE PROCEDURE sp_update_prov
    @id_provincia INT,
    @provincia VARCHAR(50)
AS
BEGIN
    UPDATE provincias
    SET provincia = @provincia
    WHERE id_provincia = @id_provincia
END
GO

-- 2. partidos

DROP PROCEDURE IF EXISTS sp_insert_partido;
GO
CREATE PROCEDURE sp_insert_partido
    @partido VARCHAR(50),
    @id_provincia INT
AS
BEGIN
    INSERT INTO partidos (partido, id_provincia)
    VALUES (@partido, @id_provincia)
END
GO

DROP PROCEDURE IF EXISTS sp_update_partido;
GO
CREATE PROCEDURE sp_update_partido
    @id_partido INT,
    @partido VARCHAR(50),
    @id_provincia INT
AS
BEGIN
    UPDATE partidos
    SET partido = @partido,
        id_provincia = @id_provincia
    WHERE id_partido = @id_partido
END
GO

-- 3. localidades

DROP PROCEDURE IF EXISTS sp_insert_localidad;
GO
CREATE PROCEDURE sp_insert_localidad
    @localidad VARCHAR(50),
    @id_partido INT
AS
BEGIN
    INSERT INTO localidades (localidad, id_partido)
    VALUES (@localidad, @id_partido)
END
GO

DROP PROCEDURE IF EXISTS sp_update_localidad;
GO
CREATE PROCEDURE sp_update_localidad
    @id_localidad INT,
    @localidad VARCHAR(50),
    @id_partido INT
AS
BEGIN
    UPDATE localidades
    SET localidad = @localidad,
        id_partido = @id_partido
    WHERE id_localidad = @id_localidad
END
GO

-- 4. tipo_doc

DROP PROCEDURE IF EXISTS sp_insert_tipo_doc;
GO
CREATE PROCEDURE sp_insert_tipo_doc
    @tipo_doc VARCHAR(30)
AS
BEGIN
    INSERT INTO tipo_doc (tipo_doc)
    VALUES (@tipo_doc)
END
GO

DROP PROCEDURE IF EXISTS sp_update_tipo_doc;
GO
CREATE PROCEDURE sp_update_tipo_doc
    @id_tipo_doc INT,
    @tipo_doc VARCHAR(30)
AS
BEGIN
    UPDATE tipo_doc
    SET tipo_doc = @tipo_doc
    WHERE id_tipo_doc = @id_tipo_doc
END
GO

-- 5. generos

DROP PROCEDURE IF EXISTS sp_insert_genero;
GO
CREATE PROCEDURE sp_insert_genero
    @genero VARCHAR(25)
AS
BEGIN
    INSERT INTO generos (genero)
    VALUES (@genero)
END
GO

DROP PROCEDURE IF EXISTS sp_update_genero;
GO
CREATE PROCEDURE sp_update_genero
    @id_genero INT,
    @genero VARCHAR(25)
AS
BEGIN
    UPDATE generos
    SET genero = @genero
    WHERE id_genero = @id_genero
END
GO

-- 6. personas

DROP PROCEDURE IF EXISTS sp_insert_persona;
GO
CREATE PROCEDURE sp_insert_persona
    @legajo INT,
    @nombre VARCHAR(30),
    @apellido VARCHAR(30),
    @id_tipo_doc INT,
    @num_doc VARCHAR(20),
    @cuil VARCHAR(10),
    @calle VARCHAR(50),
    @altura VARCHAR(30),
    @id_localidad INT,
    @id_genero INT,
    @correo VARCHAR(100)
AS
BEGIN
    INSERT INTO personas (
        legajo, nombre, apellido, id_tipo_doc, num_doc,
        cuil, calle, altura, id_localidad, id_genero, correo
    )
    VALUES (
        @legajo, @nombre, @apellido, @id_tipo_doc, @num_doc,
        @cuil, @calle, @altura, @id_localidad, @id_genero, @correo
    )
END
GO

DROP PROCEDURE IF EXISTS sp_update_persona;
GO
CREATE PROCEDURE sp_update_persona
    @id_persona INT,
    @legajo INT,
    @nombre VARCHAR(30),
    @apellido VARCHAR(30),
    @id_tipo_doc INT,
    @num_doc VARCHAR(20),
    @cuil VARCHAR(10),
    @calle VARCHAR(50),
    @altura VARCHAR(30),
    @id_localidad INT,
    @id_genero INT,
    @correo VARCHAR(100)
AS
BEGIN
    UPDATE personas
    SET legajo = @legajo,
        nombre = @nombre,
        apellido = @apellido,
        id_tipo_doc = @id_tipo_doc,
        num_doc = @num_doc,
        cuil = @cuil,
        calle = @calle,
        altura = @altura,
        id_localidad = @id_localidad,
        id_genero = @id_genero,
        correo = @correo
    WHERE id_persona = @id_persona
END
GO

-- 7. contactos

DROP PROCEDURE IF EXISTS sp_insert_contacto;
GO
CREATE PROCEDURE sp_insert_contacto
    @email VARCHAR(100),
    @celular VARCHAR(30),
    @id_persona INT
AS
BEGIN
    INSERT INTO contactos (email, celular, id_persona)
    VALUES (@email, @celular, @id_persona)
END
GO

DROP PROCEDURE IF EXISTS sp_update_contacto;
GO
CREATE PROCEDURE sp_update_contacto
    @id_contacto INT,
    @email VARCHAR(100),
    @celular VARCHAR(30),
    @id_persona INT
AS
BEGIN
    UPDATE contactos
    SET email = @email,
        celular = @celular,
        id_persona = @id_persona
    WHERE id_contacto = @id_contacto
END
GO

-- 8. roles

DROP PROCEDURE IF EXISTS sp_insert_rol;
GO
CREATE PROCEDURE sp_insert_rol
    @rol VARCHAR(50)
AS
BEGIN
    INSERT INTO roles (rol)
    VALUES (@rol)
END
GO

DROP PROCEDURE IF EXISTS sp_update_rol;
GO
CREATE PROCEDURE sp_update_rol
    @id_rol INT,
    @rol VARCHAR(50)
AS
BEGIN
    UPDATE roles
    SET rol = @rol
    WHERE id_rol = @id_rol
END
GO

-- 9. usuarios

DROP PROCEDURE IF EXISTS sp_insert_usuario;
GO
CREATE PROCEDURE sp_insert_usuario
    @usuario VARCHAR(30),
    @contrasena_script VARBINARY(512),
    @id_persona INT,
    @fecha_bloqueo DATETIME,
    @nombre_usuario_bloqueo VARCHAR(30),
    @fecha_ultimo_cambio DATETIME,
    @id_rol INT
AS
BEGIN
    INSERT INTO usuarios (
        usuario, contrasena_script, id_persona, fecha_bloqueo,
        nombre_usuario_bloqueo, fecha_ultimo_cambio, id_rol
    )
    VALUES (
        @usuario, @contrasena_script, @id_persona, @fecha_bloqueo,
        @nombre_usuario_bloqueo, @fecha_ultimo_cambio, @id_rol
    )
END
GO

DROP PROCEDURE IF EXISTS sp_actualizar_usuario;
GO
CREATE PROCEDURE sp_actualizar_usuario
    @id_usuario INT,
    @usuario VARCHAR(30),
    @contrasena_script VARBINARY(512),
    @id_persona INT,
    @fecha_bloqueo DATETIME,
    @nombre_usuario_bloqueo VARCHAR(30),
    @fecha_ultimo_cambio DATETIME,
    @id_rol INT
AS
BEGIN
    UPDATE usuarios
    SET usuario = @usuario,
        contrasena_script = @contrasena_script,
        id_persona = @id_persona,
        fecha_bloqueo = @fecha_bloqueo,
        nombre_usuario_bloqueo = @nombre_usuario_bloqueo,
        fecha_ultimo_cambio = @fecha_ultimo_cambio,
        id_rol = @id_rol
    WHERE id_usuario = @id_usuario
END
GO

-- 10. permisos

DROP PROCEDURE IF EXISTS sp_insert_permiso;
GO
CREATE PROCEDURE sp_insert_permiso
    @permiso VARCHAR(50),
    @descripcion VARCHAR(200)
AS
BEGIN
    INSERT INTO permisos (permiso, descripcion)
    VALUES (@permiso, @descripcion)
END
GO

DROP PROCEDURE IF EXISTS sp_update_permiso;
GO
CREATE PROCEDURE sp_update_permiso
    @id_permiso INT,
    @permiso VARCHAR(50),
    @descripcion VARCHAR(200)
AS
BEGIN
    UPDATE permisos
    SET permiso = @permiso,
        descripcion = @descripcion
    WHERE id_permiso = @id_permiso
END
GO

-- 11. rol_permiso

DROP PROCEDURE IF EXISTS sp_insert_rol_permiso;
GO
CREATE PROCEDURE sp_insert_rol_permiso
    @id_rol INT,
    @id_permiso INT
AS
BEGIN
    INSERT INTO rol_permiso (id_rol, id_permiso)
    VALUES (@id_rol, @id_permiso)
END
GO

-- 12. permisos_usuarios

DROP PROCEDURE IF EXISTS sp_insertar_permiso;
GO
CREATE PROCEDURE sp_insertar_permiso
    @id_usuario INT,
    @id_permiso INT,
    @fecha_vencimiento DATE
AS
BEGIN
    INSERT INTO permisos_usuarios (id_usuario, id_permiso, fecha_vencimiento)
    VALUES (@id_usuario, @id_permiso, @fecha_vencimiento)
END
GO

DROP PROCEDURE IF EXISTS sp_update_permiso_usuario;
GO
CREATE PROCEDURE sp_update_permiso_usuario
    @id_usuario INT,
    @id_permiso INT,
    @fecha_vencimiento DATE
AS
BEGIN
    UPDATE permisos_usuarios
    SET fecha_vencimiento = @fecha_vencimiento
    WHERE id_usuario = @id_usuario AND id_permiso = @id_permiso
END
GO

-- 13. historial_contrasena

DROP PROCEDURE IF EXISTS sp_historial_contrasena;
GO
CREATE PROCEDURE sp_historial_contrasena
    @id_usuario INT,
    @contrasena_script VARBINARY(512)
AS
BEGIN
    INSERT INTO historial_contrasena (id_usuario, contrasena_script)
    VALUES (@id_usuario, @contrasena_script)
END
GO

-- 14. preguntas_seguridad

DROP PROCEDURE IF EXISTS sp_insert_pregunta_seguridad;
GO
CREATE PROCEDURE sp_insert_pregunta_seguridad
    @pregunta VARCHAR(255)
AS
BEGIN
    INSERT INTO preguntas_seguridad (pregunta)
    VALUES (@pregunta)
END
GO

DROP PROCEDURE IF EXISTS sp_update_pregunta_seguridad;
GO
CREATE PROCEDURE sp_update_pregunta_seguridad
    @id_pregunta INT,
    @pregunta VARCHAR(255)
AS
BEGIN
    UPDATE preguntas_seguridad
    SET pregunta = @pregunta
    WHERE id_pregunta = @id_pregunta
END
GO

-- 15. respuestas_seguridad

DROP PROCEDURE IF EXISTS sp_insert_respuesta_seguridad;
GO
CREATE PROCEDURE sp_insert_respuesta_seguridad
    @id_usuario INT,
    @id_pregunta INT,
    @respuesta VARCHAR(60)
AS
BEGIN
    INSERT INTO respuestas_seguridad (id_usuario, id_pregunta, respuesta)
    VALUES (@id_usuario, @id_pregunta, @respuesta)
END
GO

DROP PROCEDURE IF EXISTS sp_update_respuesta_seguridad;
GO
CREATE PROCEDURE sp_update_respuesta_seguridad
    @id_usuario INT,
    @id_pregunta INT,
    @respuesta VARCHAR(60)
AS
BEGIN
    UPDATE respuestas_seguridad
    SET respuesta = @respuesta
    WHERE id_usuario = @id_usuario AND id_pregunta = @id_pregunta
END
GO

-- 16. politicas_seguridad

DROP PROCEDURE IF EXISTS sp_insert_politica_seguridad;
GO
CREATE PROCEDURE sp_insert_politica_seguridad
    @min_caracteres INT,
    @cant_preguntas INT,
    @mayus_y_minus BIT,
    @letras_y_numeros BIT,
    @caracter_especial BIT,
    @autenticacion_2fa BIT,
    @no_repetir_anteriores BIT,
    @sin_datos_personales BIT
AS
BEGIN
    INSERT INTO politicas_seguridad (
        min_caracteres, cant_preguntas, mayus_y_minus, letras_y_numeros,
        caracter_especial, autenticacion_2fa, no_repetir_anteriores, sin_datos_personales
    )
    VALUES (
        @min_caracteres, @cant_preguntas, @mayus_y_minus, @letras_y_numeros,
        @caracter_especial, @autenticacion_2fa, @no_repetir_anteriores, @sin_datos_personales
    )
END
GO

DROP PROCEDURE IF EXISTS sp_update_politica_seguridad;
GO
CREATE PROCEDURE sp_update_politica_seguridad
    @id_politica INT,
    @min_caracteres INT,
    @cant_preguntas INT,
    @mayus_y_minus BIT,
    @letras_y_numeros BIT,
    @caracter_especial BIT,
    @autenticacion_2fa BIT,
    @no_repetir_anteriores BIT,
    @sin_datos_personales BIT
AS
BEGIN
    UPDATE politicas_seguridad
    SET min_caracteres = @min_caracteres,
        cant_preguntas = @cant_preguntas,
        mayus_y_minus = @mayus_y_minus,
        letras_y_numeros = @letras_y_numeros,
        caracter_especial = @caracter_especial,
        autenticacion_2fa = @autenticacion_2fa,
        no_repetir_anteriores = @no_repetir_anteriores,
        sin_datos_personales = @sin_datos_personales
    WHERE id_politica = @id_politica
END
GO



