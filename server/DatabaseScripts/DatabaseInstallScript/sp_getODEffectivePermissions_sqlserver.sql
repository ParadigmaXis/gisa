/****** Object:  StoredProcedure [dbo].[sp_getODEffectivePermissions]    Script Date: 08/09/2013 17:44:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getODEffectivePermissions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getODEffectivePermissions]
GO

/****** Object:  StoredProcedure [dbo].[sp_getODEffectivePermissions]    Script Date: 08/09/2013 17:44:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_getODEffectivePermissions] (@IDTrustee BIGINT) AS
BEGIN

	SET NOCOUNT ON

	-- NOTA: este procedure precisa que a tabela a seguir esteja criada
	-- CREATE TABLE #effective (BIGINT ID, BIGINT IDTipoOperation, TINYINT IsGrant, PRIMARY KEY (ID, IDTipoOperation))

	DECLARE @groups TABLE (IDTrustee BIGINT);
	INSERT INTO @groups SELECT IDGroup FROM UserGroups WHERE IDUser = @IDTrustee AND isDeleted = 0;

	UPDATE E
	SET IsGrant = CONVERT(TINYINT, todp.IsGrant)
	FROM #effective E 
		INNER JOIN TrusteeObjetoDigitalPrivilege todp ON todp.IDObjetoDigital = E.ID AND todp.IDTipoOperation = E.IDTipoOperation AND todp.isDeleted = 0
	WHERE todp.IDTrustee = @IDTrustee

	UPDATE E
	SET IsGrant = todp.IsGrant
	FROM #effective E 
		INNER JOIN (
			SELECT todp.IDObjetoDigital, todp.IDTipoOperation, MIN(CONVERT(TINYINT, todp.IsGrant)) IsGrant
			FROM ObjetoDigital od
				INNER JOIN TrusteeObjetoDigitalPrivilege todp ON todp.IDObjetoDigital = od.ID    
			WHERE todp.IDTrustee IN (SELECT IDTrustee FROM @groups) AND todp.isDeleted = 0
			GROUP BY todp.IDObjetoDigital, todp.IDTipoOperation
		) todp ON todp.IDObjetoDigital = E.ID AND todp.IDTipoOperation = E.IDTipoOperation
	WHERE E.IsGrant IS NULL
END

GO


