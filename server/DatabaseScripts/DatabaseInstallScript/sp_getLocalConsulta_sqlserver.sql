/****** Object:  StoredProcedure [dbo].[sp_getLocalConsulta]    Script Date: 03/07/2014 13:18:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getLocalConsulta]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getLocalConsulta]
GO

/****** Object:  StoredProcedure [dbo].[sp_getLocalConsulta]    Script Date: 03/07/2014 13:18:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_getLocalConsulta] @NivelID BIGINT AS
BEGIN
	SET NOCOUNT ON

	SELECT DISTINCT lc.Designacao
	FROM FRDBase frd
		INNER JOIN SFRDUnidadeFisica sfrduf ON sfrduf.IDFRDBase = frd.ID AND sfrduf.isDeleted = 0
		INNER JOIN NivelUnidadeFisica nuf ON nuf.ID = sfrduf.IDNivel AND nuf.isDeleted = 0
		INNER JOIN LocalConsulta lc ON lc.ID = nuf.IDLocalConsulta AND lc.isDeleted = 0
	WHERE frd.IDNivel = @NivelID AND frd.isDeleted = 0
END
GO