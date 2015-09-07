IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getObjetosDigitaisPublicados]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getObjetosDigitaisPublicados]
GO

/****** Object:  StoredProcedure [dbo].[sp_getObjetosDigitaisPublicados]    Script Date: 07/01/2009 17:25:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_getObjetosDigitaisPublicados]
AS
BEGIN

	SELECT od.pid
	FROM ObjetoDigital od
	WHERE od.Publicado=1 AND od.isDeleted=0 
END
GO
