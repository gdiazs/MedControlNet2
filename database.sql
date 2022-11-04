
USE master
GO

IF NOT EXISTS (SELECT * FROM sys.sysdatabases WHERE name = 'MedControlNetDB')
	CREATE DATABASE "MedControlNetDB"
	GO
GO

USE MedControlNetDB
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'EspecialidadMedica')
	CREATE TABLE dbo.EspecialidadMedica
	(
		EspecialidadMedicaID INT IDENTITY(1,1),
		NombreEspecialidad VARCHAR(100),
		CONSTRAINT PK_EspecialidadMedica PRIMARY KEY  CLUSTERED 
		(
			EspecialidadMedicaID
		)
	)
	GO
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Consultorio')
	CREATE TABLE dbo.Consultorio
	(
		ConsultorioID INT IDENTITY(1,1),
		NombreDeConsultorio VARCHAR(80),
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
		EspecialidadMedicaID INT,
		CONSTRAINT PK_MatriculaDeCurso PRIMARY KEY  CLUSTERED 
		(
			ConsultorioID,
			EspecialidadMedicaID
		),
		CONSTRAINT FK_ConsultorioEspecialidad_ConsultorioID FOREIGN KEY 
		(
			ConsultorioID

		) REFERENCES dbo.Consultorio (
			ConsultorioID
		),
		CONSTRAINT FK_ConsultorioEspecialidad_EspecialidadMedicaID FOREIGN KEY 
		(
			EspecialidadMedicaID

		) REFERENCES dbo.EspecialidadMedica (
			EspecialidadMedicaID
		)
	)
	GO
GO



IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'MedicoEspecialista')
	CREATE TABLE dbo.MedicoEspecialista
	(
		MedicoEspecialistaID BIGINT IDENTITY(1,1),
		Nombre VARCHAR(255),
		EsTemporal BIT,
		EspecialidadMedicaID INT, 
		
		CONSTRAINT PK_MedicoEspecialista PRIMARY KEY  CLUSTERED 
		(
			MedicoEspecialistaID
		),
		CONSTRAINT FK_MedicoEspecialista_EspecialidadMedicaID FOREIGN KEY 
		(
			EspecialidadMedicaID

		) REFERENCES dbo.EspecialidadMedica (
			EspecialidadMedicaID
		)
	)
	GO
GO

INSERT INTO Consultorio
		(NombreDeConsultorio)
		VALUES 
			('Arenal'),
			('Barva'),
			('Miravalles'),
			('Tenorio'),
			('Poas'),
			('Irazu'),
			('Turrialba'),
			('Chato'),
			('Orosi'),
			('Platanar')
GO


INSERT INTO EspecialidadMedica
           (NombreEspecialidad)
     VALUES
		('Medicina General'),
		('Geriatría'),
		('Oftalmología'),
		('Urología'),
		('Oncología'),
		('Ginecología')
GO


