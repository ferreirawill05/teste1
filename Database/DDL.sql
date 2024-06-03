CREATE DATABASE WilliamDB

USE WilliamDB
GO

CREATE TABLE CF_ColaboradorTipoUsuario(
	Id_TipoUsuario INT IDENTITY(1,1) NOT NULL,
	Nm_TipoUsuario VARCHAR(100) NOT NULL,
	Fl_Ativo BIT NOT NULL,
	Dt_Criacao DATE,
	Dt_UltAlteracao DATE,
	Ds_UltAlteracao VARCHAR(10),
	CONSTRAINT PK_CF_ColaboradorTipoUsuario PRIMARY KEY(Id_TipoUsuario)
)

SELECT * FROM CF_ColaboradorTipoUsuario

CREATE TABLE CF_Colaborador(
	Id_Colaborador INT IDENTITY(1,1) NOT NULL,
	Id_TipoUsuario INT NOT NULL,
	Nm_Nome VARCHAR (100) NOT NULL,
	Ds_Cpf VARCHAR (11) NOT NULL,
	Nm_Usuario VARCHAR (100) NOT NULL,
	Ds_Senha VARCHAR(20) NOT NULL,
	Fl_Ativo BIT,
	Dt_Criacao DATE,
	Dt_UltAlteracao DATE,
	Ds_UltAlteracao VARCHAR(50),
	CONSTRAINT PK_CF_Colaborador PRIMARY KEY(Id_Colaborador),
	CONSTRAINT FK_CF_Colaborador FOREIGN KEY (Id_TipoUsuario) REFERENCES CF_ColaboradorTipoUsuario(Id_TipoUsuario)
)

CREATE TABLE CF_ColaboradorEmail(
	Id_Email INT IDENTITY(1,1) NOT NULL,
	Ds_Email VARCHAR(50) NOT NULL,
	Fl_PrincipaL BIT NOT NULL,
	Id_Colaborador INT NOT NULL,
	Fl_Ativo BIT,
	Dt_Criacao DATE,
	Dt_UltAlteracao DATE,
	Ds_UltAlteracao VARCHAR(50),
	CONSTRAINT PK_CF_ColaboradorEmail PRIMARY KEY(Id_Email),
	CONSTRAINT FK_CF_ColaboradorEmail FOREIGN KEY (Id_Colaborador) REFERENCES CF_Colaborador(Id_Colaborador)
)

CREATE TABLE CF_ColaboradorTelefone(
	Id_Telefone INT IDENTITY(1,1) NOT NULL,
	Nm_Apelido VARCHAR(100) NOT NULL,
	Ds_Numero VARCHAR NOT NULL,
	Fl_PrincipaL BIT NOT NULL,
	Fl_Ativo BIT,
	Id_Colaborador INT,
	Dt_Criacao DATE,
	Dt_UltAlteracao DATE,
	Ds_UltAlteracao VARCHAR(50),
	CONSTRAINT PK_CF_ColaboradorTelefone PRIMARY KEY(Id_Telefone),
	CONSTRAINT FK_CF_ColaboradorTelefone FOREIGN KEY (Id_Colaborador) REFERENCES CF_Colaborador(Id_Colaborador)
)

CREATE TABLE DF_Permissao(
	Id_Permissao INTEGER IDENTITY(1,1) NOT NULL,
	Nm_Permissao VARCHAR(100) NOT NULL,
	Fl_Ativo BIT,
	Dt_Criacao DATE,
	CONSTRAINT PK_DF_Permissao PRIMARY KEY (Id_Permissao)
)

---------------------------------------------------------------------------
CREATE TABLE CF_Permissao(
	Id_Permissao INT NOT NULL,
	Nm_Permissao VARCHAR(100) NOT NULL,
	Fl_Ativo BIT,
	Dt_Criacao DATE,
	CONSTRAINT PK_DF_NewPermissao PRIMARY KEY (Id_Permissao)
)

INSERT INTO CF_Permissao(Id_Permissao, Nm_Permissao, Fl_Ativo, Dt_Criacao)
SELECT Id_Permissao, Nm_Permissao, Fl_Ativo, Dt_Criacao
FROM DF_Permissao

EXEC sp_rename 'DF_Permissao', 'DF_PermissaoTemp';

EXEC sp_rename 'CF_Permissao', 'DF_PermissaoTemp'

select * from DF_Permissao
select * from CF_Permissao
---------------------------------------------------------------------------

CREATE TABLE DF_ColaboradorPermissao(
	Id_Colaborador_Permissao INT IDENTITY(1,1) NOT NULL,
	Fl_Ativo BIT,
	Dt_Criacao DATE,
	Id_Colaborador INT NOT NULL,
	Id_Permissao INT NOT NULL,
	CONSTRAINT PK_DF_ColaboradorPermissao PRIMARY KEY (Id_Colaborador_Permissao),
	CONSTRAINT FK_Id_Colaborador FOREIGN KEY (Id_Colaborador) REFERENCES CF_Colaborador(Id_Colaborador),
	CONSTRAINT FK_Id_Permissao FOREIGN KEY (Id_Permissao) REFERENCES DF_Permissao(Id_Permissao)
)

select * from CF_Colaborador
