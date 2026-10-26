-- ==============================================================================
-- PROYECTO: Sistema de Gestión de Inventario y Ventas
-- CURSO: Programación III
-- MOTOR: Microsoft SQL Server 2019 / 2022 / Express
-- DESCRIPCIÓN: Script de creación de base de datos, tablas, relaciones (PK/FK),
--              restricciones (CHECK, DEFAULT) y datos iniciales de prueba (Seed).
-- ==============================================================================

-- 1. CREACIÓN DE LA BASE DE DATOS
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'SistemaGestionDB')
BEGIN
    CREATE DATABASE SistemaGestionDB;
    PRINT 'Base de datos [SistemaGestionDB] creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La base de datos [SistemaGestionDB] ya existe.';
END
GO

USE SistemaGestionDB;
GO

-- ==============================================================================
-- 2. ELIMINACIÓN DE TABLAS SI YA EXISTEN (EN ORDEN INVERSO DE DEPENDENCIAS)
-- ==============================================================================
IF OBJECT_ID('dbo.HistoricoMovimientosInventario', 'U') IS NOT NULL DROP TABLE dbo.HistoricoMovimientosInventario;
IF OBJECT_ID('dbo.DetalleVenta', 'U') IS NOT NULL DROP TABLE dbo.DetalleVenta;
IF OBJECT_ID('dbo.Venta', 'U') IS NOT NULL DROP TABLE dbo.Venta;
IF OBJECT_ID('dbo.Usuarios', 'U') IS NOT NULL DROP TABLE dbo.Usuarios;
IF OBJECT_ID('dbo.Roles', 'U') IS NOT NULL DROP TABLE dbo.Roles;
IF OBJECT_ID('dbo.Cliente', 'U') IS NOT NULL DROP TABLE dbo.Cliente;
IF OBJECT_ID('dbo.Empleado', 'U') IS NOT NULL DROP TABLE dbo.Empleado;
IF OBJECT_ID('dbo.Persona', 'U') IS NOT NULL DROP TABLE dbo.Persona;
IF OBJECT_ID('dbo.Productos', 'U') IS NOT NULL DROP TABLE dbo.Productos;
IF OBJECT_ID('dbo.Categorias', 'U') IS NOT NULL DROP TABLE dbo.Categorias;
GO

-- ==============================================================================
-- 3. CREACIÓN DE TABLAS
-- ==============================================================================

-- Tabla: Categorias
CREATE TABLE dbo.Categorias (
    CategoriaId INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(255) NULL,
    FechaCreacion DATETIME NOT NULL CONSTRAINT DF_Categorias_FechaCreacion DEFAULT (GETDATE()),
    Activo BIT NOT NULL CONSTRAINT DF_Categorias_Activo DEFAULT (1),
    CONSTRAINT PK_Categorias PRIMARY KEY CLUSTERED (CategoriaId)
);
GO

-- Tabla: Productos
CREATE TABLE dbo.Productos (
    ProductoId INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(150) NOT NULL,
    Descripcion VARCHAR(500) NULL,
    PrecioCompra DECIMAL(18,2) NOT NULL,
    PrecioVenta DECIMAL(18,2) NOT NULL,
    StockActual INT NOT NULL,
    StockMinimo INT NOT NULL,
    CategoriaId INT NOT NULL,
    FechaRegistro DATETIME NOT NULL CONSTRAINT DF_Productos_FechaRegistro DEFAULT (GETDATE()),
    Activo BIT NOT NULL CONSTRAINT DF_Productos_Activo DEFAULT (1),
    CONSTRAINT PK_Productos PRIMARY KEY CLUSTERED (ProductoId),
    CONSTRAINT FK_Productos_Categorias FOREIGN KEY (CategoriaId) REFERENCES dbo.Categorias(CategoriaId),
    -- Reglas de negocio obligatorias del PDF:
    CONSTRAINT CK_Productos_PrecioCompra CHECK (PrecioCompra >= 0),
    CONSTRAINT CK_Productos_PrecioVenta CHECK (PrecioVenta > 0),
    CONSTRAINT CK_Productos_StockActual CHECK (StockActual >= 0),
    CONSTRAINT CK_Productos_StockMinimo CHECK (StockMinimo >= 0)
);
GO

-- Tabla: Persona (Base para Cliente y Empleado)
CREATE TABLE dbo.Persona (
    PersonaId INT IDENTITY(1,1) NOT NULL,
    NombreCompleto VARCHAR(200) NOT NULL,
    Identificacion VARCHAR(50) NOT NULL,
    Email VARCHAR(150) NULL,
    Telefono VARCHAR(50) NULL,
    Direccion VARCHAR(255) NULL,
    FechaRegistro DATETIME NOT NULL CONSTRAINT DF_Persona_FechaRegistro DEFAULT (GETDATE()),
    Activo BIT NOT NULL CONSTRAINT DF_Persona_Activo DEFAULT (1),
    CONSTRAINT PK_Persona PRIMARY KEY CLUSTERED (PersonaId),
    CONSTRAINT UQ_Persona_Identificacion UNIQUE (Identificacion)
);
GO

-- Tabla: Cliente (Relación 1:1 con Persona)
CREATE TABLE dbo.Cliente (
    ClienteId INT IDENTITY(1,1) NOT NULL,
    PersonaId INT NOT NULL,
    LimiteCredito DECIMAL(18,2) NOT NULL CONSTRAINT DF_Cliente_LimiteCredito DEFAULT (0.00),
    FechaAfiliacion DATETIME NOT NULL CONSTRAINT DF_Cliente_FechaAfiliacion DEFAULT (GETDATE()),
    Activo BIT NOT NULL CONSTRAINT DF_Cliente_Activo DEFAULT (1),
    CONSTRAINT PK_Cliente PRIMARY KEY CLUSTERED (ClienteId),
    CONSTRAINT FK_Cliente_Persona FOREIGN KEY (PersonaId) REFERENCES dbo.Persona(PersonaId),
    CONSTRAINT UQ_Cliente_PersonaId UNIQUE (PersonaId),
    CONSTRAINT CK_Cliente_LimiteCredito CHECK (LimiteCredito >= 0)
);
GO

-- Tabla: Empleado (Relación 1:1 con Persona)
CREATE TABLE dbo.Empleado (
    EmpleadoId INT IDENTITY(1,1) NOT NULL,
    PersonaId INT NOT NULL,
    Puesto VARCHAR(100) NOT NULL,
    Salario DECIMAL(18,2) NOT NULL,
    FechaIngreso DATETIME NOT NULL CONSTRAINT DF_Empleado_FechaIngreso DEFAULT (GETDATE()),
    Activo BIT NOT NULL CONSTRAINT DF_Empleado_Activo DEFAULT (1),
    CONSTRAINT PK_Empleado PRIMARY KEY CLUSTERED (EmpleadoId),
    CONSTRAINT FK_Empleado_Persona FOREIGN KEY (PersonaId) REFERENCES dbo.Persona(PersonaId),
    CONSTRAINT UQ_Empleado_PersonaId UNIQUE (PersonaId),
    CONSTRAINT CK_Empleado_Salario CHECK (Salario >= 0)
);
GO

-- Tabla: Roles
CREATE TABLE dbo.Roles (
    RolId INT IDENTITY(1,1) NOT NULL,
    NombreRol VARCHAR(50) NOT NULL,
    Descripcion VARCHAR(200) NULL,
    Activo BIT NOT NULL CONSTRAINT DF_Roles_Activo DEFAULT (1),
    CONSTRAINT PK_Roles PRIMARY KEY CLUSTERED (RolId),
    CONSTRAINT UQ_Roles_NombreRol UNIQUE (NombreRol)
);
GO

-- Tabla: Usuarios (Acceso al sistema)
CREATE TABLE dbo.Usuarios (
    UsuarioId INT IDENTITY(1,1) NOT NULL,
    EmpleadoId INT NOT NULL,
    NombreUsuario VARCHAR(50) NOT NULL,
    ContrasenaHash VARCHAR(256) NOT NULL,
    RolId INT NOT NULL,
    Activo BIT NOT NULL CONSTRAINT DF_Usuarios_Activo DEFAULT (1),
    CONSTRAINT PK_Usuarios PRIMARY KEY CLUSTERED (UsuarioId),
    CONSTRAINT FK_Usuarios_Empleado FOREIGN KEY (EmpleadoId) REFERENCES dbo.Empleado(EmpleadoId),
    CONSTRAINT FK_Usuarios_Roles FOREIGN KEY (RolId) REFERENCES dbo.Roles(RolId),
    CONSTRAINT UQ_Usuarios_NombreUsuario UNIQUE (NombreUsuario)
);
GO

-- Tabla: Venta (Encabezado)
CREATE TABLE dbo.Venta (
    VentaId INT IDENTITY(1,1) NOT NULL,
    ClienteId INT NOT NULL,
    EmpleadoId INT NOT NULL,
    FechaVenta DATETIME NOT NULL CONSTRAINT DF_Venta_FechaVenta DEFAULT (GETDATE()),
    SubTotal DECIMAL(18,2) NOT NULL,
    Impuesto DECIMAL(18,2) NOT NULL,
    Descuento DECIMAL(18,2) NOT NULL CONSTRAINT DF_Venta_Descuento DEFAULT (0.00),
    Total DECIMAL(18,2) NOT NULL,
    Estado VARCHAR(30) NOT NULL CONSTRAINT DF_Venta_Estado DEFAULT ('Procesada'),
    Observaciones VARCHAR(500) NULL,
    CONSTRAINT PK_Venta PRIMARY KEY CLUSTERED (VentaId),
    CONSTRAINT FK_Venta_Cliente FOREIGN KEY (ClienteId) REFERENCES dbo.Cliente(ClienteId),
    CONSTRAINT FK_Venta_Empleado FOREIGN KEY (EmpleadoId) REFERENCES dbo.Empleado(EmpleadoId),
    CONSTRAINT CK_Venta_Estado CHECK (Estado IN ('Pendiente', 'Procesada', 'Anulada')),
    CONSTRAINT CK_Venta_SubTotal CHECK (SubTotal >= 0),
    CONSTRAINT CK_Venta_Impuesto CHECK (Impuesto >= 0),
    CONSTRAINT CK_Venta_Descuento CHECK (Descuento >= 0),
    CONSTRAINT CK_Venta_Total CHECK (Total >= 0)
);
GO

-- Tabla: DetalleVenta
CREATE TABLE dbo.DetalleVenta (
    DetalleVentaId INT IDENTITY(1,1) NOT NULL,
    VentaId INT NOT NULL,
    ProductoId INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    Descuento DECIMAL(18,2) NOT NULL CONSTRAINT DF_DetalleVenta_Descuento DEFAULT (0.00),
    SubTotal DECIMAL(18,2) NOT NULL,
    CONSTRAINT PK_DetalleVenta PRIMARY KEY CLUSTERED (DetalleVentaId),
    CONSTRAINT FK_DetalleVenta_Venta FOREIGN KEY (VentaId) REFERENCES dbo.Venta(VentaId) ON DELETE CASCADE,
    CONSTRAINT FK_DetalleVenta_Productos FOREIGN KEY (ProductoId) REFERENCES dbo.Productos(ProductoId),
    CONSTRAINT CK_DetalleVenta_Cantidad CHECK (Cantidad > 0),
    CONSTRAINT CK_DetalleVenta_PrecioUnitario CHECK (PrecioUnitario > 0),
    CONSTRAINT CK_DetalleVenta_Descuento CHECK (Descuento >= 0),
    CONSTRAINT CK_DetalleVenta_SubTotal CHECK (SubTotal >= 0)
);
GO

-- Tabla: Histórico de Movimientos de Inventario (Auditoría / Kardex)
CREATE TABLE dbo.HistoricoMovimientosInventario (
    MovimientoId INT IDENTITY(1,1) NOT NULL,
    ProductoId INT NOT NULL,
    EmpleadoId INT NOT NULL,
    FechaMovimiento DATETIME NOT NULL CONSTRAINT DF_HistoricoMovimientos_Fecha DEFAULT (GETDATE()),
    TipoMovimiento VARCHAR(30) NOT NULL,
    Cantidad INT NOT NULL,
    StockAnterior INT NOT NULL,
    StockNuevo INT NOT NULL,
    Observacion VARCHAR(255) NULL,
    CONSTRAINT PK_HistoricoMovimientos PRIMARY KEY CLUSTERED (MovimientoId),
    CONSTRAINT FK_HistoricoMovimientos_Productos FOREIGN KEY (ProductoId) REFERENCES dbo.Productos(ProductoId),
    CONSTRAINT FK_HistoricoMovimientos_Empleado FOREIGN KEY (EmpleadoId) REFERENCES dbo.Empleado(EmpleadoId),
    CONSTRAINT CK_HistoricoMovimientos_Tipo CHECK (TipoMovimiento IN ('Entrada', 'Salida', 'Ajuste', 'Devolución', 'Venta')),
    CONSTRAINT CK_HistoricoMovimientos_Cantidad CHECK (Cantidad > 0),
    CONSTRAINT CK_HistoricoMovimientos_StockAnterior CHECK (StockAnterior >= 0),
    CONSTRAINT CK_HistoricoMovimientos_StockNuevo CHECK (StockNuevo >= 0)
);
GO

-- ==============================================================================
-- 4. INSERCIÓN DE DATOS INICIALES (SEED DATA)
-- ==============================================================================

PRINT 'Insertando datos iniciales...';

-- A. ROLES
INSERT INTO dbo.Roles (NombreRol, Descripcion, Activo) VALUES
('Administrador', 'Control total de la aplicación y configuraciones', 1),
('Vendedor', 'Gestión de clientes y emisión de ventas', 1),
('Bodeguero', 'Control de existencias y movimientos de inventario', 1),
('Consulta', 'Solo visualización de reportes y catálogo', 1);
GO

-- B. CATEGORÍAS
INSERT INTO dbo.Categorias (Nombre, Descripcion, Activo) VALUES
('Electrónica y Cómputo', 'Laptops, accesorios, teclados y monitores', 1),
('Bebidas y Alimentos', 'Snacks, café, refrescos y confitería', 1),
('Papelería y Oficina', 'Cuadernos, bolígrafos, resmas y escritorios', 1),
('Limpieza y Hogar', 'Artículos de higiene, desinfectantes y papel', 1);
GO

-- C. PRODUCTOS (Los productos se gestionan desde la aplicación)

-- D. PERSONAS (Para Empleados y Clientes)
INSERT INTO dbo.Persona (NombreCompleto, Identificacion, Email, Telefono, Direccion, Activo) VALUES
-- 1: Empleado Administrador
('Carlos Solís Delgado', '1-1520-0345', 'carlos.solis@gmail.com', '8888-1111', 'San José, Costa Rica', 1),
-- 2: Empleado Vendedor
('Ana María Morales Mora', '2-0789-0123', 'ana.morales@gmail.com', '8888-2222', 'Alajuela, Costa Rica', 1),
-- 3: Empleado Bodeguero
('Roberto Jiménez Castro', '3-0456-0789', 'roberto.jimenez@gmail.com', '8888-3333', 'Heredia, Costa Rica', 1),
-- 4: Cliente Regular
('Juan Pérez Gómez', '1-0987-0654', 'juan.perez@gmail.com', '7777-1111', 'Cartago, Costa Rica', 1),
-- 5: Cliente Corporativo
('Empresa Alfa S.A.', '3-101-998877', 'compras.alfa@gmail.com', '7777-2222', 'Escazú, San José', 1);
GO

-- E. EMPLEADOS (Asociados a Persona 1, 2, 3)
INSERT INTO dbo.Empleado (PersonaId, Puesto, Salario, Activo) VALUES
(1, 'Gerente General / Administrador', 1250000.00, 1),
(2, 'Ejecutiva de Ventas', 550000.00, 1),
(3, 'Supervisor de Inventario', 480000.00, 1);
GO

-- F. CLIENTES (Asociados a Persona 4, 5)
INSERT INTO dbo.Cliente (PersonaId, LimiteCredito, Activo) VALUES
(4, 350000.00, 1),
(5, 2500000.00, 1);
GO

-- G. USUARIOS (Credenciales de acceso al sistema)
-- NOTA DE SEGURIDAD:
-- La contraseña se almacena con SHA-256 en formato Hexadecimal (64 caracteres)
-- 'admin123'    -> '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9'
-- 'vendedor123' -> '285d03bb6973e8e195724a8ec1ae071f0545931758c0cf7d8b5a86a603c4f74a'
-- 'bodega123'   -> '60e73e970e7f7f9037e2ab774c48972551a37cfa9bf7413d0ee6e3a62888cf3e'
INSERT INTO dbo.Usuarios (EmpleadoId, NombreUsuario, ContrasenaHash, RolId, Activo) VALUES
(1, 'admin',    LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'admin123'), 2)), 1, 1),
(2, 'vendedor', LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'vendedor123'), 2)), 2, 1),
(3, 'bodega',   LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'bodega123'), 2)), 3, 1);
GO

-- H. REGISTRO INICIAL DE KARDEX (Los movimientos se registran desde la aplicación)

PRINT '=======================================================';
PRINT '  BASE DE DATOS [SistemaGestionDB] CREADA CON ÉXITO   ';
PRINT '=======================================================';
PRINT 'Credenciales de prueba generadas:';
PRINT '  - Usuario: admin    | Clave: admin123    (Rol: Administrador)';
PRINT '  - Usuario: vendedor | Clave: vendedor123 (Rol: Vendedor)';
PRINT '  - Usuario: bodega   | Clave: bodega123   (Rol: Bodeguero)';
PRINT '=======================================================';
GO
