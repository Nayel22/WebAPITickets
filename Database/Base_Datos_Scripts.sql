-- Crear Base de Datos
CREATE DATABASE VisorTickets;
GO

-- Usar la base de datos creada
USE VisorTickets;

-- Crear Tabla de Roles
CREATE TABLE Roles (
    ro_identificador INT PRIMARY KEY IDENTITY(1,1),
    ro_descripcion NVARCHAR(125) NOT NULL,
    ro_fecha_auditoria DATETIME DEFAULT GETDATE(),
    ro_agregado_por NVARCHAR(10) NOT NULL,
    ro_fecha_modificacion DATETIME NULL,
    ro_modificado_por NVARCHAR(10) NULL
);

-- Insertar Roles
INSERT INTO Roles (ro_descripcion, ro_agregado_por) 
VALUES 
    ('Soporte', 'admin'),
    ('Analista', 'admin');

-- Crear Tabla de Usuarios
CREATE TABLE Usuarios (
    us_identificador INT PRIMARY KEY IDENTITY(1,1),
    us_nombre_completo NVARCHAR(150) NOT NULL,
    us_correo NVARCHAR(150) NOT NULL,
    us_clave NVARCHAR(255) NOT NULL,
    us_ro_identificador INT FOREIGN KEY REFERENCES Roles(ro_identificador),
    us_estado NVARCHAR(1) NOT NULL,
    us_fecha_agregado DATETIME DEFAULT GETDATE() NOT NULL,
    us_agregado_por NVARCHAR(10) NOT NULL,
    us_fecha_modificacion DATETIME NULL,
    us_modificado_por NVARCHAR(10) NULL
);

-- Insertar Usuario
INSERT INTO Usuarios (
    us_nombre_completo, 
    us_correo, 
    us_clave, 
    us_ro_identificador, 
    us_estado, 
    us_agregado_por
) VALUES (
    'Nayel Solorzano',
    'nayelsolorzanom213@gmail.com', 
    '123', 
    1, 
    'A', 
    'admin'
);

-- Crear Tabla de Tiquetes
CREATE TABLE Tiquetes (
    ti_identificador INT PRIMARY KEY IDENTITY(1,1),
    ti_asunto NVARCHAR(150) NOT NULL,
    ti_categoria NVARCHAR(150) NOT NULL,
    ti_us_id_asignado INT FOREIGN KEY REFERENCES Usuarios(us_identificador),
    ti_urgencia NVARCHAR(150) NOT NULL,
    ti_importancia NVARCHAR(150) NOT NULL,
    ti_estado NVARCHAR(1) NOT NULL,
    ti_fecha_agregado DATETIME DEFAULT GETDATE() NOT NULL,
    ti_agregado_por NVARCHAR(10) NOT NULL,
    ti_fecha_modificacion DATETIME NULL,
    ti_modificado_por NVARCHAR(10) NULL
);

-- Insertar Tiquetes
INSERT INTO Tiquetes (
    ti_asunto, 
    ti_categoria, 
    ti_us_id_asignado, 
    ti_urgencia, 
    ti_importancia, 
    ti_estado, 
    ti_agregado_por
) VALUES 
    ('Problema de red', 'Redes', 1, 'Alta', 'Alta', 'A', 'admin'),
    ('Problema de computadora', 'Hardware', 1, 'Alta', 'Alta', 'A', 'admin');

-- Consultas de verificación
SELECT * FROM Roles;
SELECT * FROM Usuarios;
SELECT * FROM Tiquetes;

-- Consulta para verificar la relación entre Usuarios y Roles
SELECT ro_identificador, us_ro_identificador, us_correo
FROM Usuarios
JOIN Roles ON ro_identificador = us_ro_identificador;