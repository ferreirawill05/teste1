USE WilliamDB
GO

IF NOT EXISTS (SELECT 1 FROM CF_ColaboradorTipoUsuario WHERE Nm_TipoUsuario = 'adm' OR Nm_TipoUsuario = 'colab')
BEGIN
	INSERT INTO CF_ColaboradorTipoUsuario (Nm_TipoUsuario, Fl_Ativo, Dt_Criacao)
	VALUES ('adm', 1, '2023-08-22'),
			('colab', 1, '2023-08-22');
END

--select * from CF_ColaboradorTipoUsuario

IF NOT EXISTS (SELECT 1 FROM DF_Permissao where Nm_Permissao = 'cadastrar' OR Nm_Permissao = 'alterar' OR Nm_Permissao = 'excluir' )
BEGIN
	INSERT INTO DF_Permissao (Nm_Permissao, Fl_Ativo, Dt_Criacao)
	VALUES ('cadastrar', 1, '2022-09-30'),
			('alterar', 1, '2023-09-30'),
			('excluir', 1, '2024-01-20');
END

--SELECT * FROM DF_Permissao

IF NOT EXISTS (SELECT 1 FROM DF_ColaboradorPermissao WHERE Id_Colaborador_Permissao = 1)
BEGIN
	INSERT INTO DF_ColaboradorPermissao (Id_Colaborador, Id_Permissao, Fl_Ativo, Dt_Criacao)
	VALUES (1,1,1,'2023-08-25'),
			(1,2,1,'2023-08-25'),
			(1,3,1,'2023-08-25');
END

--SELECT * FROM DF_ColaboradorPermissao

IF NOT EXISTS (SELECT 1 FROM CF_Colaborador WHERE Nm_Usuario = 'master')
BEGIN
	INSERT INTO CF_Colaborador (Id_TipoUsuario, Nm_Nome, Ds_Cpf, Nm_Usuario, Ds_Senha, Fl_Ativo, Dt_Criacao)
	VALUES (1, 'master', '29586476709', 'Carlos', 'carlos@123', 1, '2023-08-22'),
			(2, 'colab', '14586735310', 'Hugo', 'hugo@123', 1, '2023-08-22');
END

--SELECT * FROM CF_Colaborador
