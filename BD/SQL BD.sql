go
USE EduData;
go

create table Usuarios (
Id INT PRIMARY KEY IDENTITY (1,1),
Apodo NVARCHAR(50) UNIQUE NOT NULL,
Correo NVARCHAR(100) UNIQUE NOT NULL,
Clave NVARCHAR(255) NOT NULL, --CONTRA
Estado BIT NOT NULL DEFAULT 1,
Rol NVARCHAR(20) NOT NULL DEFAULT 'Usuario',
Fecha_Creacion DATETIME NOT NULL DEFAULT GETDATE(),
Creado_Por NVARCHAR(50) NOT NULL,
Fecha_Modificacion DATETIME NULL,
Modificado_Por NVARCHAR(50) NULL
);
go

----PROCEDIMIENTOS PARA GESTION DE USUARIOS---
--1,CREAR USUARIO
Create Procedure sp_CrearUsuario
@Apodo NVARCHAR(50),
@Correo NVARCHAR(100),
@Clave NVARCHAR(255),
@Estado BIT =1,
@Rol NVARCHAR(20) = 'Usuario',
@Creado_Por NVARCHAR(50)
AS
BEGIN
INSERT INTO Usuarios (Apodo, Correo, Clave, Estado,Rol, Fecha_Creacion, Creado_Por)

Values (@Apodo, @Correo, @Clave, @Estado,@Rol,GETDATE(), @Creado_Por);

END
go

---modificar usuario--
CREATE PROCEDURE sp_ModificarUsuario
@Id INT,
@Apodo NVARCHAR(50),
@Correo NVARCHAR(100),
@Estado BIT,
@Rol NVARCHAR(20),
@Modificado_Por NVARCHAR(50)
AS
BEGIN
UPDATE Usuarios
SET Apodo = @Apodo,
    Correo =@Correo,
    Estado = @Estado,
    Rol = @Rol,
    Fecha_Modificacion = GETDATE(),
    Modificado_Por = @Modificado_Por
    WHERE Id = @Id;
    END
    go

---- cambiar contra--
CREATE PROCEDURE sp_CambiarClave
@Id INT,
@Nueva_Clave NVARCHAR(255),
@Modificado_Por NVARCHAR(50)
AS
BEGIN
UPDATE Usuarios
SET Clave = @Nueva_Clave,
Fecha_Modificacion = GETDATE(),
Modificado_Por = @Modificado_Por
WHere Id = @Id;
END
GO

--leer todos los usuarios---
CREATE PROCEDURE sp_LeerUsuarios
AS
BEGIN 
SELECT Id, Apodo,Correo, Estado, Rol, Fecha_Creacion, Creado_Por
FROM Usuarios;

End
go

-----6. buscar usuario--
CREATE PROCEDURE sp_BuscarUsuarioPorId
@Id INT
as
Begin
Select Id, Apodo, Correo, Clave, Estado, Rol, Fecha_Creacion, Creado_Por, 
Fecha_Modificacion, Modificado_Por
FROM Usuarios
Where Id = @Id;
End
go

----7. buscar-----
CREATE PROCEDURE sp_BuscarUsuarioPorApodo
@Apodo NVARCHAR(50)
AS
BEGIN
SELECT Id, Apodo,Correo, Clave, Estado,Rol,
Fecha_Creacion, Creado_Por,
Fecha_modificacion, Modificado_Por
FROM Usuarios
WHere Apodo LIKE '%' + @Apodo + ' %';
END
go