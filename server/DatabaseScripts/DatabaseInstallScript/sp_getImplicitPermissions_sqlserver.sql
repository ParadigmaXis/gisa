/****** Object:  StoredProcedure [dbo].[sp_getImplicitPermissions]    Script Date: 08/07/2013 14:07:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getImplicitPermissions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getImplicitPermissions]
GO

/****** Object:  StoredProcedure [dbo].[sp_getImplicitPermissions]    Script Date: 08/07/2013 14:07:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_getImplicitPermissions] (@IDTrustee BIGINT) as
BEGIN

	SET NOCOUNT ON

	DECLARE @groups TABLE (IDTrustee BIGINT)
	INSERT INTO @groups SELECT IDGroup FROM UserGroups WHERE IDUser = @IDTrustee

	DECLARE @updated BIGINT = 1
	
	DECLARE @justImplicit TINYINT
	SET @justImplicit = 0

	WHILE (@updated > 0)
	BEGIN
		SELECT @updated = 0;
		
		IF @justImplicit = 1
		BEGIN
			UPDATE E
			SET 
				Criar = ISNULL(E.Criar, TrusteeNivelPrivilege.Criar),
				Ler = ISNULL(E.Ler, TrusteeNivelPrivilege.Ler),
				Escrever = ISNULL(E.Escrever, TrusteeNivelPrivilege.Escrever),
				Apagar = ISNULL(E.Apagar, TrusteeNivelPrivilege.Apagar),
				Expandir = ISNULL(E.Expandir, TrusteeNivelPrivilege.Expandir)
			FROM #effective E
				INNER JOIN TrusteeNivelPrivilege ON E.IDUpper = TrusteeNivelPrivilege.IDNivel
			WHERE TrusteeNivelPrivilege.IDTrustee = @IDTrustee
				AND (E.Criar IS NULL OR E.Ler IS NULL OR E.Escrever IS NULL OR E.Apagar IS NULL OR E.Expandir IS NULL)
		END
		SET @justImplicit = 1
		
		UPDATE E
		SET 
			Criar = ISNULL(E.Criar, TrusteeNivelPrivilege.Criar),
			Ler = ISNULL(E.Ler, TrusteeNivelPrivilege.Ler),
			Escrever = ISNULL(E.Escrever, TrusteeNivelPrivilege.Escrever),
			Apagar = ISNULL(E.Apagar, TrusteeNivelPrivilege.Apagar),
			Expandir = ISNULL(E.Expandir, TrusteeNivelPrivilege.Expandir)
		FROM #effective E
			INNER JOIN (
				SELECT IDNivel, MIN(Criar) Criar, MIN(Ler) Ler, MIN(Escrever) Escrever, MIN(Apagar) Apagar, MIN(Expandir) Expandir 
				FROM TrusteeNivelPrivilege 
				WHERE IDTrustee IN (SELECT IDTrustee FROM @groups) 
				GROUP BY IDNivel
			) TrusteeNivelPrivilege ON E.IDUpper = TrusteeNivelPrivilege.IDNivel
		WHERE (E.Criar IS NULL OR E.Ler IS NULL OR E.Escrever IS NULL OR E.Apagar IS NULL OR E.Expandir IS NULL)
		
		UPDATE E
		SET IDUpper = RH.IDUpper
		FROM #effective E
			INNER JOIN RelacaoHierarquica RH ON E.IDUpper = RH.ID
		WHERE RH.IDTipoNivelRelacionado > 7

		SELECT @updated = @updated + @@ROWCOUNT;
	END
END

GO


