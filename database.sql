
USE master
GO

IF NOT EXISTS (SELECT * FROM sys.sysdatabases WHERE name = 'MedControlNetDB')
	CREATE DATABASE "MedControlNetDB"
	GO
GO

USE MedControlNetDB
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Especialidad')
	CREATE TABLE dbo.Especialidad
	(
		EspecialidadID INT IDENTITY(1,1),
		NombreEspecialidad VARCHAR(100),
		CONSTRAINT PK_EspecialidadMedica PRIMARY KEY  CLUSTERED 
		(
			EspecialidadID
		)
	)
	GO
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Consultorio')
	CREATE TABLE dbo.Consultorio
	(
		ConsultorioID INT IDENTITY(1,1),
		NumeroConsultorio INT,
		CONSTRAINT PK_Consultorio PRIMARY KEY  CLUSTERED 
		(
			ConsultorioID
		)
	)
	GO
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'ConsultorioEspecialidad' and xtype = 'U')
	CREATE TABLE dbo.ConsultorioEspecialidad
	(
		ConsultorioID INT, 
		EspecialidadID INT,
		CONSTRAINT PK_MatriculaDeCurso PRIMARY KEY  CLUSTERED 
		(
			ConsultorioID,
			EspecialidadID
		),
		CONSTRAINT FK_ConsultorioEspecialidad_ConsultorioID FOREIGN KEY 
		(
			ConsultorioID

		) REFERENCES dbo.Consultorio (
			ConsultorioID
		),
		CONSTRAINT FK_ConsultorioEspecialidad_EspecialidadID FOREIGN KEY 
		(
			EspecialidadID

		) REFERENCES dbo.Especialidad (
			EspecialidadID
		)
	)
	GO
GO



IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Medico')
	CREATE TABLE dbo.Medico
	(
		MedicoID INT IDENTITY(1,1),
		Identificacion VARCHAR(30) UNIQUE,
		Nombre VARCHAR(255),
		EsTemporal BIT,
		EspecialidadID INT, 
		
		CONSTRAINT PK_Medico PRIMARY KEY  CLUSTERED 
		(
			MedicoID
		),
		CONSTRAINT FK_Medico_EspecialidadID FOREIGN KEY 
		(
			EspecialidadID

		) REFERENCES dbo.Especialidad (
			EspecialidadID
		)
	)
	GO
GO


IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Paciente')
	CREATE TABLE dbo.Paciente
	(
		PacienteID INT IDENTITY(1,1),
		Identificacion VARCHAR(30),
		Nombre VARCHAR(255),
		CONSTRAINT PK_Paciente PRIMARY KEY  CLUSTERED 
		(
			PacienteID
		)
	)
	GO
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Cita')
	CREATE TABLE dbo.Cita
	(
		CitaID INT IDENTITY(1,1),
		PacienteID INT, 
		MedicoID INT,
		HoraCita DATETIME,
		Costo DECIMAL(19,4),
	    ConsultorioID INT,
		CONSTRAINT PK_Cita PRIMARY KEY  CLUSTERED 
		(
			CitaID,
			PacienteID,
			MedicoID,
			ConsultorioID
		),
		CONSTRAINT FK_Cita_PacienteID FOREIGN KEY 
		(
			PacienteID

		) REFERENCES dbo.Paciente (
			PacienteID
		),
		CONSTRAINT FK_Cita_MedicoID FOREIGN KEY 
		(
			MedicoID

		) REFERENCES dbo.Medico (
			MedicoID
		),
		CONSTRAINT FK_Cita_ConsultorioID FOREIGN KEY
		(
			ConsultorioID

		) REFERENCES dbo.Consultorio (
			ConsultorioID
		)
	)
	GO
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Inventario')
	CREATE TABLE dbo.Inventario
	(
		InventarioID INT IDENTITY(1,1),
		NumeroActivo INT, 
		Nombre VARCHAR(30),
		Serie VARCHAR(30),
		Descripcion VARCHAR(200),
		FechaDeCompra DATETIME,
		EspecialidadID INT,
		ConsultorioID INT,

		CONSTRAINT PK_Inventario PRIMARY KEY CLUSTERED 
		(
			InventarioID
		),
		CONSTRAINT FK_Inventario_EspecialidadID FOREIGN KEY 
		(
			EspecialidadID

		) REFERENCES dbo.Especialidad (
			EspecialidadID
		),
		CONSTRAINT FK_Inventario_ConsultorioID FOREIGN KEY 
		(
			ConsultorioID

		) REFERENCES dbo.Consultorio (
			ConsultorioID
		)
	)
	GO
GO


INSERT INTO Consultorio
		(NumeroConsultorio)
		VALUES 
			(1),
			(2),
			(3),
			(4),
			(5),
			(6),
			(7),
			(8),
			(9),
			(10)
GO


INSERT INTO Especialidad
           (NombreEspecialidad)
     VALUES
		('Medicina General'),
		('Geriatría'),
		('Oftalmología'),
		('Urología'),
		('Oncología'),
		('Ginecología')
GO


