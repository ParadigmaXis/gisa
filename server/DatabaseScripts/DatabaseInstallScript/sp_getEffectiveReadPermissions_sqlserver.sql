/****** Object:  StoredProcedure [dbo].[sp_getEffectiveReadPermissions]    Script Date: 08/08/2013 01:55:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getEffectiveReadPermissions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getEffectiveReadPermissions]
GO

/****** Object:  StoredProcedure [dbo].[sp_getEffectiveReadPermissions]    Script Date: 08/08/2013 01:55:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_getEffectiveReadPermissions] (@IDTrustee BIGINT) AS
BEGIN

	SET NOCOUNT ON

	-- NOTA: este procedure precisa que a tabela a seguir esteja criada
	-- CREATE TABLE #effective (IDNivel BIGINT PRIMARY KEY, IDUpper BIGINT, Ler TINYINT)
	
	-- NOTA: as permissoes atribuidas pelo grupo publicados nao sao inicialmente consideradas porque nao podem ser herdadas por niveis hierarquicamente inferiores. 
	--		 Essas permiss�es s� ser�o atribuidas no fim do calculo a niveis que nao tenham a permissao de leitura definida	e o IDTrustee seja um USER e n�o um GROUP
	DECLARE @grp_pub BIGINT
	SELECT @grp_pub = ID FROM Trustee where Name = 'ACESSO_PUBLICADOS'

	DECLARE @groups TABLE (IDTrustee BIGINT)
	INSERT INTO @groups SELECT IDGroup FROM UserGroups WHERE IDUser = @IDTrustee AND IDGroup <> @grp_pub AND isDeleted = 0
	
	-- No caso do @IDTrustee ser o do grupo publicados so serao consideradas as permissoes explicitas por nivel isto porque trata-se de um grupo especial onde
	-- as permissoes atribuidas nao sao herdaveis a niveis hierarquicamente inferiores
	IF (@IDTrustee = @grp_pub)
	BEGIN
		UPDATE E
		SET 
			Ler = ISNULL(E.Ler, TrusteeNivelPrivilege.Ler)
		FROM #effective E
			INNER JOIN TrusteeNivelPrivilege ON E.IDUpper = TrusteeNivelPrivilege.IDNivel
		WHERE TrusteeNivelPrivilege.IDTrustee = @IDTrustee AND TrusteeNivelPrivilege.isDeleted = 0
			AND E.Ler IS NULL
			
		RETURN
	END
	
	DECLARE @updated BIGINT = 1

	WHILE (@updated > 0)
	BEGIN
		SELECT @updated = 0;
		UPDATE E
		SET 
			Ler = ISNULL(E.Ler, TrusteeNivelPrivilege.Ler)
		FROM #effective E
			INNER JOIN TrusteeNivelPrivilege ON E.IDUpper = TrusteeNivelPrivilege.IDNivel
		WHERE TrusteeNivelPrivilege.IDTrustee = @IDTrustee AND TrusteeNivelPrivilege.isDeleted = 0
			AND E.Ler IS NULL
		
		UPDATE E
		SET 
			Ler = ISNULL(E.Ler, TrusteeNivelPrivilege.Ler)
		FROM #effective E
			INNER JOIN (
				SELECT IDNivel, MIN(Ler) Ler
				FROM TrusteeNivelPrivilege 
				WHERE IDTrustee IN (SELECT IDTrustee FROM @groups) AND TrusteeNivelPrivilege.isDeleted = 0
				GROUP BY IDNivel
			) TrusteeNivelPrivilege ON E.IDUpper = TrusteeNivelPrivilege.IDNivel
		WHERE E.Ler IS NULL
		
		UPDATE E
		SET IDUpper = RH.IDUpper
		FROM #effective E
			INNER JOIN RelacaoHierarquica RH ON E.IDUpper = RH.ID
			INNER JOIN Nivel n ON n.ID = RH.IDUpper and n.IDTipoNivel = 3 and n.isDeleted = 0 -- como os documentos tambem podem ser de niveis topo, na ausencia desta condicao, o idupper seria actualizado para um nivel organico
		WHERE RH.IDTipoNivelRelacionado > 7 AND RH.isDeleted = 0

		SELECT @updated = @updated + @@ROWCOUNT;
	END
	
	-- Aplicar as permissoes do grupo publicados ao IDTrustee caso seja um USER
	IF (EXISTS(SELECT ID FROM TrusteeUser WHERE ID = @IDTrustee))
	BEGIN
		UPDATE E
		SET Ler = TrusteeNivelPrivilege.Ler
		FROM #effective E
			INNER JOIN TrusteeNivelPrivilege ON E.IDNivel = TrusteeNivelPrivilege.IDNivel
		WHERE TrusteeNivelPrivilege.IDTrustee = @grp_pub AND TrusteeNivelPrivilege.isDeleted = 0
			AND E.Ler IS NULL
	END
END
GO


