USE master;
GO

ALTER DATABASE EduData SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

DROP DATABASE IF EXISTS EduData;
GO

CREATE DATABASE EduData;
GO

USE EduData;
GO

/*=========================================================
=                         TABLAS
=========================================================*/

CREATE TABLE Profesor (
    IdProfesor INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Especialidad NVARCHAR(100) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    Fecha_Creacion DATETIME NOT NULL DEFAULT GETDATE(),
    Creado_Por NVARCHAR(50) NOT NULL,
    Fecha_Modificacion DATETIME NULL,
    Modificado_Por NVARCHAR(50) NULL
);
GO

CREATE TABLE Estudiante (
    IdEstudiante INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Telefono NVARCHAR(20) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    Fecha_Creacion DATETIME NOT NULL DEFAULT GETDATE(),
    Creado_Por NVARCHAR(50) NOT NULL,
    Fecha_Modificacion DATETIME NULL,
    Modificado_Por NVARCHAR(50) NULL
);
GO

CREATE TABLE Curso (
    IdCurso INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255) NOT NULL,
    IdProfesor INT NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    Fecha_Creacion DATETIME NOT NULL DEFAULT GETDATE(),
    Creado_Por NVARCHAR(50) NOT NULL,
    Fecha_Modificacion DATETIME NULL,
    Modificado_Por NVARCHAR(50) NULL,
    CONSTRAINT FK_Curso_Profesor FOREIGN KEY (IdProfesor)
    REFERENCES Profesor(IdProfesor)
);
GO

CREATE TABLE EstudianteCurso (
    IdEstudianteCurso INT IDENTITY(1,1) PRIMARY KEY,
    IdEstudiante INT NOT NULL,
    IdCurso INT NOT NULL,
    Fecha_Inscripcion DATETIME NOT NULL DEFAULT GETDATE(),
    Activo BIT NOT NULL DEFAULT 1,
    Fecha_Creacion DATETIME NOT NULL DEFAULT GETDATE(),
    Creado_Por NVARCHAR(50) NOT NULL,
    Fecha_Modificacion DATETIME NULL,
    Modificado_Por NVARCHAR(50) NULL,
    CONSTRAINT FK_EstudianteCurso_Estudiante FOREIGN KEY (IdEstudiante)
    REFERENCES Estudiante(IdEstudiante),
    CONSTRAINT FK_EstudianteCurso_Curso FOREIGN KEY (IdCurso)
    REFERENCES Curso(IdCurso)
);
GO

/*=========================================================
=                         ÍNDICES
=========================================================*/

CREATE INDEX IX_Curso_IdProfesor ON Curso(IdProfesor);
GO

CREATE INDEX IX_EstudianteCurso_IdEstudiante ON EstudianteCurso(IdEstudiante);
GO

CREATE INDEX IX_EstudianteCurso_IdCurso ON EstudianteCurso(IdCurso);
GO

/*=========================================================
=                  PROCEDIMIENTOS PROFESOR
=========================================================*/

CREATE OR ALTER PROCEDURE sp_CrearProfesor
    @Nombre NVARCHAR(100),
    @Email NVARCHAR(100),
    @Especialidad NVARCHAR(100),
    @Creado_Por NVARCHAR(50)
AS
BEGIN
    INSERT INTO Profesor (Nombre, Email, Especialidad, Activo, Fecha_Creacion, Creado_Por)
    VALUES (@Nombre, @Email, @Especialidad, 1, GETDATE(), @Creado_Por);
END
GO

CREATE OR ALTER PROCEDURE sp_LeerProfesores
AS
BEGIN
    SELECT IdProfesor, Nombre, Email, Especialidad, Activo,
           Fecha_Creacion, Creado_Por, Fecha_Modificacion, Modificado_Por
    FROM Profesor
    WHERE Activo = 1;
END
GO

CREATE OR ALTER PROCEDURE sp_BuscarProfesorPorId
    @IdProfesor INT
AS
BEGIN
    SELECT IdProfesor, Nombre, Email, Especialidad, Activo,
           Fecha_Creacion, Creado_Por, Fecha_Modificacion, Modificado_Por
    FROM Profesor
    WHERE IdProfesor = @IdProfesor AND Activo = 1;
END
GO

CREATE OR ALTER PROCEDURE sp_ModificarProfesor
    @IdProfesor INT,
    @Nombre NVARCHAR(100),
    @Email NVARCHAR(100),
    @Especialidad NVARCHAR(100),
    @Modificado_Por NVARCHAR(50)
AS
BEGIN
    UPDATE Profesor
    SET Nombre = @Nombre,
        Email = @Email,
        Especialidad = @Especialidad,
        Fecha_Modificacion = GETDATE(),
        Modificado_Por = @Modificado_Por
    WHERE IdProfesor = @IdProfesor AND Activo = 1;
END
GO

CREATE OR ALTER PROCEDURE sp_EliminarProfesor
    @IdProfesor INT,
    @Modificado_Por NVARCHAR(50)
AS
BEGIN
    UPDATE Profesor
    SET Activo = 0,
        Fecha_Modificacion = GETDATE(),
        Modificado_Por = @Modificado_Por
    WHERE IdProfesor = @IdProfesor;
END
GO

/*=========================================================
=                 PROCEDIMIENTOS ESTUDIANTE
=========================================================*/

CREATE OR ALTER PROCEDURE sp_CrearEstudiante
    @Nombre NVARCHAR(100),
    @Email NVARCHAR(100),
    @Telefono NVARCHAR(20),
    @Creado_Por NVARCHAR(50)
AS
BEGIN
    INSERT INTO Estudiante (Nombre, Email, Telefono, Activo, Fecha_Creacion, Creado_Por)
    VALUES (@Nombre, @Email, @Telefono, 1, GETDATE(), @Creado_Por);
END
GO

CREATE OR ALTER PROCEDURE sp_LeerEstudiantes
AS
BEGIN
    SELECT IdEstudiante, Nombre, Email, Telefono, Activo,
           Fecha_Creacion, Creado_Por, Fecha_Modificacion, Modificado_Por
    FROM Estudiante
    WHERE Activo = 1;
END
GO

CREATE OR ALTER PROCEDURE sp_BuscarEstudiantePorId
    @IdEstudiante INT
AS
BEGIN
    SELECT IdEstudiante, Nombre, Email, Telefono, Activo,
           Fecha_Creacion, Creado_Por, Fecha_Modificacion, Modificado_Por
    FROM Estudiante
    WHERE IdEstudiante = @IdEstudiante AND Activo = 1;
END
GO

CREATE OR ALTER PROCEDURE sp_ModificarEstudiante
    @IdEstudiante INT,
    @Nombre NVARCHAR(100),
    @Email NVARCHAR(100),
    @Telefono NVARCHAR(20),
    @Modificado_Por NVARCHAR(50)
AS
BEGIN
    UPDATE Estudiante
    SET Nombre = @Nombre,
        Email = @Email,
        Telefono = @Telefono,
        Fecha_Modificacion = GETDATE(),
        Modificado_Por = @Modificado_Por
    WHERE IdEstudiante = @IdEstudiante AND Activo = 1;
END
GO

CREATE OR ALTER PROCEDURE sp_EliminarEstudiante
    @IdEstudiante INT,
    @Modificado_Por NVARCHAR(50)
AS
BEGIN
    UPDATE Estudiante
    SET Activo = 0,
        Fecha_Modificacion = GETDATE(),
        Modificado_Por = @Modificado_Por
    WHERE IdEstudiante = @IdEstudiante;
END
GO

/*=========================================================
=                    PROCEDIMIENTOS CURSO
=========================================================*/

CREATE OR ALTER PROCEDURE sp_CrearCurso
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(255),
    @IdProfesor INT,
    @Creado_Por NVARCHAR(50)
AS
BEGIN
    INSERT INTO Curso (Nombre, Descripcion, IdProfesor, Activo, Fecha_Creacion, Creado_Por)
    VALUES (@Nombre, @Descripcion, @IdProfesor, 1, GETDATE(), @Creado_Por);
END
GO

CREATE OR ALTER PROCEDURE sp_LeerCursos
AS
BEGIN
    SELECT C.IdCurso, C.Nombre, C.Descripcion, C.IdProfesor,
           P.Nombre AS NombreProfesor,
           C.Activo, C.Fecha_Creacion, C.Creado_Por,
           C.Fecha_Modificacion, C.Modificado_Por
    FROM Curso C
    INNER JOIN Profesor P ON C.IdProfesor = P.IdProfesor
    WHERE C.Activo = 1;
END
GO

CREATE OR ALTER PROCEDURE sp_BuscarCursoPorId
    @IdCurso INT
AS
BEGIN
    SELECT C.IdCurso, C.Nombre, C.Descripcion, C.IdProfesor,
           P.Nombre AS NombreProfesor,
           C.Activo, C.Fecha_Creacion, C.Creado_Por,
           C.Fecha_Modificacion, C.Modificado_Por
    FROM Curso C
    INNER JOIN Profesor P ON C.IdProfesor = P.IdProfesor
    WHERE C.IdCurso = @IdCurso AND C.Activo = 1;
END
GO

CREATE OR ALTER PROCEDURE sp_ModificarCurso
    @IdCurso INT,
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(255),
    @IdProfesor INT,
    @Modificado_Por NVARCHAR(50)
AS
BEGIN
    UPDATE Curso
    SET Nombre = @Nombre,
        Descripcion = @Descripcion,
        IdProfesor = @IdProfesor,
        Fecha_Modificacion = GETDATE(),
        Modificado_Por = @Modificado_Por
    WHERE IdCurso = @IdCurso AND Activo = 1;
END
GO

CREATE OR ALTER PROCEDURE sp_EliminarCurso
    @IdCurso INT,
    @Modificado_Por NVARCHAR(50)
AS
BEGIN
    UPDATE Curso
    SET Activo = 0,
        Fecha_Modificacion = GETDATE(),
        Modificado_Por = @Modificado_Por
    WHERE IdCurso = @IdCurso;
END
GO

/*=========================================================
=              PROCEDIMIENTOS ESTUDIANTE CURSO
=========================================================*/

CREATE OR ALTER PROCEDURE sp_InscribirEstudianteCurso
    @IdEstudiante INT,
    @IdCurso INT,
    @Creado_Por NVARCHAR(50)
AS
BEGIN
    INSERT INTO EstudianteCurso
    (IdEstudiante, IdCurso, Fecha_Inscripcion, Activo, Fecha_Creacion, Creado_Por)
    VALUES
    (@IdEstudiante, @IdCurso, GETDATE(), 1, GETDATE(), @Creado_Por);
END
GO

CREATE OR ALTER PROCEDURE sp_LeerEstudianteCursos
AS
BEGIN
    SELECT EC.IdEstudianteCurso,
           EC.IdEstudiante,
           E.Nombre AS NombreEstudiante,
           EC.IdCurso,
           C.Nombre AS NombreCurso,
           EC.Fecha_Inscripcion,
           EC.Activo,
           EC.Fecha_Creacion,
           EC.Creado_Por,
           EC.Fecha_Modificacion,
           EC.Modificado_Por
    FROM EstudianteCurso EC
    INNER JOIN Estudiante E ON EC.IdEstudiante = E.IdEstudiante
    INNER JOIN Curso C ON EC.IdCurso = C.IdCurso
    WHERE EC.Activo = 1;
END
GO

CREATE OR ALTER PROCEDURE sp_EliminarEstudianteCurso
    @IdEstudianteCurso INT,
    @Modificado_Por NVARCHAR(50)
AS
BEGIN
    UPDATE EstudianteCurso
    SET Activo = 0,
        Fecha_Modificacion = GETDATE(),
        Modificado_Por = @Modificado_Por
    WHERE IdEstudianteCurso = @IdEstudianteCurso;
END
GO