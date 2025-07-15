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

-- 9. Usuarios
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
    FOREIGN KEY (id_persona) REFERENCES personas(id_persona),
    FOREIGN KEY (id_rol) REFERENCES roles(id_rol),
    FOREIGN KEY (id_politica) REFERENCES politicas_seguridad(id_politica)
);

-- 10. Permisos
CREATE TABLE permisos (
    id_permiso INT PRIMARY KEY IDENTITY(1,1),
    permiso VARCHAR(50) NOT NULL,
    descripcion VARCHAR(200)
);

-- 11. Rol - Permiso
CREATE TABLE rol_permiso (
    id_rol INT NOT NULL,
    id_permiso INT NOT NULL,
    PRIMARY KEY (id_rol, id_permiso),
    FOREIGN KEY (id_rol) REFERENCES roles(id_rol),
    FOREIGN KEY (id_permiso) REFERENCES permisos(id_permiso)
);

-- 12. Usuario - Permiso
CREATE TABLE permisos_usuarios (
    id_usuario INT NOT NULL,
    id_permiso INT NOT NULL,
    fecha_vencimiento DATE,
    PRIMARY KEY (id_usuario, id_permiso),
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id_usuario),
    FOREIGN KEY (id_permiso) REFERENCES permisos(id_permiso)
);

-- 13. Historial de Contraseñas
CREATE TABLE historial_contrasena (
    id INT PRIMARY KEY IDENTITY(1,1),
    id_usuario INT NOT NULL,
    fecha_cambio DATETIME NOT NULL DEFAULT GETDATE(),
    contrasena_script VARBINARY(512) NOT NULL,
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id_usuario)
);

-- 14. Preguntas de Seguridad
CREATE TABLE preguntas_seguridad (
    id_pregunta INT PRIMARY KEY IDENTITY(1,1),
    pregunta VARCHAR(255) NOT NULL
);

-- 15. Respuestas de Seguridad
CREATE TABLE respuestas_seguridad (
    id_usuario INT NOT NULL,
    id_pregunta INT NOT NULL,
    respuesta VARCHAR(60) NOT NULL,
    PRIMARY KEY (id_usuario, id_pregunta),
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id_usuario),
    FOREIGN KEY (id_pregunta) REFERENCES preguntas_seguridad(id_pregunta)
);

-- 16. Políticas de Seguridad
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