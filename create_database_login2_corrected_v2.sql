-- Step 1: Terminate all connections to the login2 database
IF DB_ID('login2') IS NOT NULL
BEGIN
    -- Set database to single-user mode to terminate connections
    ALTER DATABASE login2 SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    -- Drop the database
    DROP DATABASE login2;
END
GO

-- Step 2: Create the new login2 database
CREATE DATABASE login2;
GO

-- Step 3: Use the login2 database
USE login2;
GO

-- Step 4: Create Tables
-- 1. Provincias
CREATE TABLE provincias (
    id_provincia INT PRIMARY KEY IDENTITY(1,1),
    provincia VARCHAR(50) NOT NULL
);

-- 2. Partidos
CREATE TABLE partidos (
    id_partido INT PRIMARY KEY IDENTITY(1,1),
    partido VARCHAR(50) NOT NULL,
    id_provincia INT NOT NULL,
    FOREIGN KEY (id_provincia) REFERENCES provincias(id_provincia)
);

-- 3. Localidades
CREATE TABLE localidades (
    id_localidad INT PRIMARY KEY IDENTITY(1,1),
    localidad VARCHAR(50) NOT NULL,
    id_partido INT NOT NULL,
    FOREIGN KEY (id_partido) REFERENCES partidos(id_partido)
);

-- 4. Tipos de documento
CREATE TABLE tipo_doc (
    id_tipo_doc INT PRIMARY KEY IDENTITY(1,1),
    tipo_doc VARCHAR(30) NOT NULL
);

-- 5. Géneros
CREATE TABLE generos (
    id_genero INT PRIMARY KEY IDENTITY(1,1),
    genero VARCHAR(25) NOT NULL
);

-- 6. Personas
CREATE TABLE personas (
    id_persona INT PRIMARY KEY IDENTITY(1,1),
    legajo INT NOT NULL,
    nombre VARCHAR(30) NOT NULL,
    apellido VARCHAR(30) NOT NULL,
    id_tipo_doc INT NOT NULL,
    num_doc VARCHAR(20) NOT NULL,
    cuil VARCHAR(10),
    calle VARCHAR(50),
    altura VARCHAR(30),   
    id_localidad INT NOT NULL,
    id_genero INT NOT NULL,
    correo VARCHAR(100),
    fecha_ingreso DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (id_tipo_doc) REFERENCES tipo_doc(id_tipo_doc),
    FOREIGN KEY (id_localidad) REFERENCES localidades(id_localidad),
    FOREIGN KEY (id_genero) REFERENCES generos(id_genero)
);

-- 7. Contactos
CREATE TABLE contactos (
    id_contacto INT PRIMARY KEY IDENTITY(1,1),
    email VARCHAR(100) NOT NULL,
    celular VARCHAR(30) NOT NULL,
    id_persona INT NOT NULL,
    FOREIGN KEY (id_persona) REFERENCES personas(id_persona)
);

-- 8. Roles
CREATE TABLE roles (
    id_rol INT PRIMARY KEY IDENTITY(1,1),
    rol VARCHAR(50) NOT NULL
);

-- 9. Políticas de Seguridad
CREATE TABLE politicas_seguridad (
    id_politica INT PRIMARY KEY IDENTITY(1,1),
    min_caracteres INT,
    cant_preguntas INT,
    mayus_y_minus BIT,
    letras_y_numeros BIT,
    caracter_especial BIT,
    autenticacion_2fa BIT,
    no_repetir_anteriores BIT,
    sin_datos_personales BIT
);

-- 10. Usuarios
CREATE TABLE usuarios (
    id_usuario INT PRIMARY KEY IDENTITY(1,1),
    usuario VARCHAR(30) NOT NULL,
    contrasena_script VARBINARY(512) NOT NULL,
    id_persona INT NOT NULL,
    fecha_bloqueo DATETIME NOT NULL DEFAULT GETDATE(),
    nombre_usuario_bloqueo VARCHAR(30),
    fecha_ultimo_cambio DATETIME NOT NULL DEFAULT GETDATE(),
    id_rol INT NOT NULL,
    id_politica INT,
    CambioContrasenaObligatorio BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (id_persona) REFERENCES personas(id_persona),
    FOREIGN KEY (id_rol) REFERENCES roles(id_rol),
    FOREIGN KEY (id_politica) REFERENCES politicas_seguridad(id_politica)
);

-- 11. Permisos
CREATE TABLE permisos (
    id_permiso INT PRIMARY KEY IDENTITY(1,1),
    permiso VARCHAR(50) NOT NULL,
    descripcion VARCHAR(200)
);

-- 12. Rol - Permiso
CREATE TABLE rol_permiso (
    id_rol INT NOT NULL,
    id_permiso INT NOT NULL,
    PRIMARY KEY (id_rol, id_permiso),
    FOREIGN KEY (id_rol) REFERENCES roles(id_rol),
    FOREIGN KEY (id_permiso) REFERENCES permisos(id_permiso)
);

-- 13. Usuario - Permiso
CREATE TABLE permisos_usuarios (
    id_usuario INT NOT NULL,
    id_permiso INT NOT NULL,
    fecha_vencimiento DATE,
    PRIMARY KEY (id_usuario, id_permiso),
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id_usuario),
    FOREIGN KEY (id_permiso) REFERENCES permisos(id_permiso)
);

-- 14. Historial de Contraseñas
CREATE TABLE historial_contrasena (
    id INT PRIMARY KEY IDENTITY(1,1),
    id_usuario INT NOT NULL,
    fecha_cambio DATETIME NOT NULL DEFAULT GETDATE(),
    contrasena_script VARBINARY(512) NOT NULL,
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id_usuario)
);

-- 15. Preguntas de Seguridad
CREATE TABLE preguntas_seguridad (
    id_pregunta INT PRIMARY KEY IDENTITY(1,1),
    pregunta VARCHAR(255) NOT NULL
);

-- 16. Respuestas de Seguridad
CREATE TABLE respuestas_seguridad (
    id_usuario INT NOT NULL,
    id_pregunta INT NOT NULL,
    respuesta VARCHAR(60) NOT NULL,
    PRIMARY KEY (id_usuario, id_pregunta),
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id_usuario),
    FOREIGN KEY (id_pregunta) REFERENCES preguntas_seguridad(id_pregunta)
);

-- Step 5: Create Stored Procedures

-- 1. Provincias
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

-- 2. Partidos
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

-- 3. Localidades
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

-- 4. Tipo_doc
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

-- 5. Generos
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

-- 6. Personas
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

-- 7. Contactos
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

-- 8. Roles
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

-- 9. Usuarios
DROP PROCEDURE IF EXISTS sp_insert_usuario;
GO
CREATE PROCEDURE sp_insert_usuario
    @usuario VARCHAR(30),
    @contrasena_script VARBINARY(512),
    @id_persona INT,
    @fecha_bloqueo DATETIME,
    @nombre_usuario_bloqueo VARCHAR(30),
    @fecha_ultimo_cambio DATETIME,
    @id_rol INT,
    @CambioContrasenaObligatorio BIT = 0
AS
BEGIN
    INSERT INTO usuarios (
        usuario, contrasena_script, id_persona, fecha_bloqueo,
        nombre_usuario_bloqueo, fecha_ultimo_cambio, id_rol, CambioContrasenaObligatorio
    )
    VALUES (
        @usuario, @contrasena_script, @id_persona, @fecha_bloqueo,
        @nombre_usuario_bloqueo, @fecha_ultimo_cambio, @id_rol, @CambioContrasenaObligatorio
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
    @id_rol INT,
    @CambioContrasenaObligatorio BIT
AS
BEGIN
    UPDATE usuarios
    SET usuario = @usuario,
        contrasena_script = @contrasena_script,
        id_persona = @id_persona,
        fecha_bloqueo = @fecha_bloqueo,
        nombre_usuario_bloqueo = @nombre_usuario_bloqueo,
        fecha_ultimo_cambio = @fecha_ultimo_cambio,
        id_rol = @id_rol,
        CambioContrasenaObligatorio = @CambioContrasenaObligatorio
    WHERE id_usuario = @id_usuario
END
GO

-- 10. Permisos
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

-- 11. Rol_permiso
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

-- 12. Permisos_usuarios
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

-- 13. Historial_contrasena
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

-- 14. Preguntas_seguridad
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

-- 15. Respuestas_seguridad
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

-- 16. Politicas_seguridad
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

-- Step 6: Populate Initial Data with Robust Handling

-- tipo_doc
EXEC sp_insert_tipo_doc @tipo_doc = 'DNI';
EXEC sp_insert_tipo_doc @tipo_doc = 'Pasaporte';
GO

-- géneros
EXEC sp_insert_genero @genero = 'Masculino';
EXEC sp_insert_genero @genero = 'Femenino';
GO

-- Provincias
EXEC sp_insert_prov @provincia = 'CABA';
EXEC sp_insert_prov @provincia = 'Buenos Aires';
EXEC sp_insert_prov @provincia = 'Catamarca';
EXEC sp_insert_prov @provincia = 'Chaco';
EXEC sp_insert_prov @provincia = 'Chubut';
EXEC sp_insert_prov @provincia = 'Córdoba';
EXEC sp_insert_prov @provincia = 'Corrientes';
EXEC sp_insert_prov @provincia = 'Entre Ríos';
EXEC sp_insert_prov @provincia = 'Formosa';
EXEC sp_insert_prov @provincia = 'Jujuy';
EXEC sp_insert_prov @provincia = 'La Pampa';
EXEC sp_insert_prov @provincia = 'La Rioja';
EXEC sp_insert_prov @provincia = 'Mendoza';
EXEC sp_insert_prov @provincia = 'Misiones';
EXEC sp_insert_prov @provincia = 'Neuquén';
EXEC sp_insert_prov @provincia = 'Río Negro';
EXEC sp_insert_prov @provincia = 'Salta';
EXEC sp_insert_prov @provincia = 'San Juan';
EXEC sp_insert_prov @provincia = 'San Luis';
EXEC sp_insert_prov @provincia = 'Santa Cruz';
EXEC sp_insert_prov @provincia = 'Santa Fe';
EXEC sp_insert_prov @provincia = 'Santiago del Estero';
EXEC sp_insert_prov @provincia = 'Tierra del Fuego';
EXEC sp_insert_prov @provincia = 'Tucumán';
GO

-- Partidos
DECLARE @id_provincia INT;
SELECT @id_provincia = id_provincia FROM provincias WHERE provincia = 'Buenos Aires';
IF @id_provincia IS NOT NULL
BEGIN
    EXEC sp_insert_partido @partido = 'Almirante Brown', @id_provincia = @id_provincia;
    EXEC sp_insert_partido @partido = 'Avellaneda', @id_provincia = @id_provincia;
    EXEC sp_insert_partido @partido = 'Berazategui', @id_provincia = @id_provincia;
    EXEC sp_insert_partido @partido = 'Esteban Echeverría', @id_provincia = @id_provincia;
    EXEC sp_insert_partido @partido = 'Ezeiza', @id_provincia = @id_provincia;
    EXEC sp_insert_partido @partido = 'Florencio Varela', @id_provincia = @id_provincia;
    EXEC sp_insert_partido @partido = 'Lanús', @id_provincia = @id_provincia;
    EXEC sp_insert_partido @partido = 'Lomas de Zamora', @id_provincia = @id_provincia;
    EXEC sp_insert_partido @partido = 'Presidente Perón', @id_provincia = @id_provincia;
    EXEC sp_insert_partido @partido = 'Quilmes', @id_provincia = @id_provincia;
END
ELSE
BEGIN
    RAISERROR ('Provincia "Buenos Aires" not found.', 16, 1);
END
GO

-- Localidades
-- Lanús
DECLARE @id_partido INT;
SELECT @id_partido = id_partido FROM partidos WHERE partido = 'Lanús';
IF @id_partido IS NOT NULL
BEGIN
    EXEC sp_insert_localidad @localidad = 'Lanús Este', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Lanús Oeste', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Villa Caraza', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Remedios de Escalada', @id_partido = @id_partido;
END
ELSE
BEGIN
    RAISERROR ('Partido "Lanús" not found.', 16, 1);
END
GO

-- Avellaneda
DECLARE @id_partido INT;
SELECT @id_partido = id_partido FROM partidos WHERE partido = 'Avellaneda';
IF @id_partido IS NOT NULL
BEGIN
    EXEC sp_insert_localidad @localidad = 'Avellaneda', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Dock Sud', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Sarandí', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Villa Domínico', @id_partido = @id_partido;
END
ELSE
BEGIN
    RAISERROR ('Partido "Avellaneda" not found.', 16, 1);
END
GO

-- Quilmes
DECLARE @id_partido INT;
SELECT @id_partido = id_partido FROM partidos WHERE partido = 'Quilmes';
IF @id_partido IS NOT NULL
BEGIN
    EXEC sp_insert_localidad @localidad = 'Quilmes', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Bernal', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Ezpeleta', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'San Francisco Solano', @id_partido = @id_partido;
END
ELSE
BEGIN
    RAISERROR ('Partido "Quilmes" not found.', 16, 1);
END
GO

-- Lomas de Zamora
DECLARE @id_partido INT;
SELECT @id_partido = id_partido FROM partidos WHERE partido = 'Lomas de Zamora';
IF @id_partido IS NOT NULL
BEGIN
    EXEC sp_insert_localidad @localidad = 'Lomas de Zamora', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Banfield', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Temperley', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Villa Centenario', @id_partido = @id_partido;
END
ELSE
BEGIN
    RAISERROR ('Partido "Lomas de Zamora" not found.', 16, 1);
END
GO

-- Almirante Brown
DECLARE @id_partido INT;
SELECT @id_partido = id_partido FROM partidos WHERE partido = 'Almirante Brown';
IF @id_partido IS NOT NULL
BEGIN
    EXEC sp_insert_localidad @localidad = 'Adrogué', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Burzaco', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Claypole', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Glew', @id_partido = @id_partido;
END
ELSE
BEGIN
    RAISERROR ('Partido "Almirante Brown" not found.', 16, 1);
END
GO

-- Esteban Echeverría
DECLARE @id_partido INT;
SELECT @id_partido = id_partido FROM partidos WHERE partido = 'Esteban Echeverría';
IF @id_partido IS NOT NULL
BEGIN
    EXEC sp_insert_localidad @localidad = 'Monte Grande', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'El Jagüel', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Canning', @id_partido = @id_partido;
END
ELSE
BEGIN
    RAISERROR ('Partido "Esteban Echeverría" not found.', 16, 1);
END
GO

-- Ezeiza
DECLARE @id_partido INT;
SELECT @id_partido = id_partido FROM partidos WHERE partido = 'Ezeiza';
IF @id_partido IS NOT NULL
BEGIN
    EXEC sp_insert_localidad @localidad = 'Ezeiza', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Tristán Suárez', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Carlos Spegazzini', @id_partido = @id_partido;
END
ELSE
BEGIN
    RAISERROR ('Partido "Ezeiza" not found.', 16, 1);
END
GO

-- Florencio Varela
DECLARE @id_partido INT;
SELECT @id_partido = id_partido FROM partidos WHERE partido = 'Florencio Varela';
IF @id_partido IS NOT NULL
BEGIN
    EXEC sp_insert_localidad @localidad = 'Florencio Varela', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Bosques', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Ingeniero Allan', @id_partido = @id_partido;
END
ELSE
BEGIN
    RAISERROR ('Partido "Florencio Varela" not found.', 16, 1);
END
GO

-- Presidente Perón
DECLARE @id_partido INT;
SELECT @id_partido = id_partido FROM partidos WHERE partido = 'Presidente Perón';
IF @id_partido IS NOT NULL
BEGIN
    EXEC sp_insert_localidad @localidad = 'Guernica', @id_partido = @id_partido;
END
ELSE
BEGIN
    RAISERROR ('Partido "Presidente Perón" not found.', 16, 1);
END
GO

-- Berazategui
DECLARE @id_partido INT;
SELECT @id_partido = id_partido FROM partidos WHERE partido = 'Berazategui';
IF @id_partido IS NOT NULL
BEGIN
    EXEC sp_insert_localidad @localidad = 'Berazategui', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Hudson', @id_partido = @id_partido;
    EXEC sp_insert_localidad @localidad = 'Ranelagh', @id_partido = @id_partido;
END
ELSE
BEGIN
    RAISERROR ('Partido "Berazategui" not found.', 16, 1);
END
GO

-- Step 7: Insert Admin User
-- Insert Rol "Administrador"
EXEC sp_insert_rol @rol = 'Administrador';
GO

-- Insert Preguntas de Seguridad
EXEC sp_insert_pregunta_seguridad @pregunta = '¿Cuál era el nombre de tu primera mascota?';
EXEC sp_insert_pregunta_seguridad @pregunta = '¿Cuál es el apellido de soltera de tu madre?';
EXEC sp_insert_pregunta_seguridad @pregunta = '¿Cómo se llamaba tu escuela primaria?';
EXEC sp_insert_pregunta_seguridad @pregunta = '¿En qué ciudad naciste?';
EXEC sp_insert_pregunta_seguridad @pregunta = '¿Cuál es tu libro favorito?';
GO

-- Insert Persona for Admin
DECLARE @id_tipo_doc INT, @id_localidad INT, @id_genero INT;
SELECT @id_tipo_doc = id_tipo_doc FROM tipo_doc WHERE tipo_doc = 'DNI';
SELECT @id_localidad = id_localidad FROM localidades WHERE localidad = 'Lanús Este';
SELECT @id_genero = id_genero FROM generos WHERE genero = 'Masculino';

IF @id_tipo_doc IS NOT NULL AND @id_localidad IS NOT NULL AND @id_genero IS NOT NULL
BEGIN
    EXEC sp_insert_persona 
        @legajo = 1,
        @nombre = 'Admin',
        @apellido = 'User',
        @id_tipo_doc = @id_tipo_doc,
        @num_doc = '12345678',
        @cuil = '20123456781',
        @calle = 'Admin St',
        @altura = '123',
        @id_localidad = @id_localidad,
        @id_genero = @id_genero,
        @correo = 'admin@example.com';
END
ELSE
BEGIN
    RAISERROR ('Initial data for persona not found.', 16, 1);
END
GO

-- Insert Usuario Admin
DECLARE @id_persona INT, @id_rol INT, @fecha_bloqueo DATETIME, @fecha_ultimo_cambio DATETIME;
SELECT @id_persona = id_persona FROM personas WHERE nombre = 'Admin';
SELECT @id_rol = id_rol FROM roles WHERE rol = 'Administrador';
SET @fecha_bloqueo = CAST('9999-12-31' AS DATETIME);
SET @fecha_ultimo_cambio = GETDATE();

IF @id_persona IS NOT NULL AND @id_rol IS NOT NULL
BEGIN
    -- Contraseña "admin123" encriptada con SHA256
    DECLARE @password VARBINARY(512) = HASHBYTES('SHA2_256', 'admin123');
    EXEC sp_insert_usuario
        @usuario = 'admin',
        @contrasena_script = @password,
        @id_persona = @id_persona,
        @fecha_bloqueo = @fecha_bloqueo,
        @nombre_usuario_bloqueo = NULL,
        @fecha_ultimo_cambio = @fecha_ultimo_cambio,
        @id_rol = @id_rol,
        @CambioContrasenaObligatorio = 0;
END
ELSE
BEGIN
    RAISERROR ('Persona or Rol for admin not found.', 16, 1);
END
GO