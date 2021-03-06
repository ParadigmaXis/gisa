SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[fn_IsPrazoElimExp]') and xtype in (N'FN', N'IF', N'TF'))
drop function [dbo].[fn_IsPrazoElimExp]
GO
CREATE FUNCTION dbo.fn_IsPrazoElimExp 
( @IDFRDBase BIGINT, @dt DATETIME  )
RETURNS TINYINT AS  
BEGIN 
	DECLARE @ano NVARCHAR (4)
  	DECLARE @mes NVARCHAR (2)
  	DECLARE @dia NVARCHAR (2)
  	DECLARE @prazo NUMERIC
	DECLARE @IDUpper BIGINT

	-- Obter data final de produção e prazo de conservação para documentos que constituam séries
	SELECT @ano = sfrddp.FimAno, @mes = sfrddp.FimMes, @dia = sfrddp.FimDia, @prazo = sfrdaUpper.PrazoConservacao, @IDUpper = nUpper.ID
	FROM FRDBase frd
		INNER JOIN Nivel n ON n.ID = frd.IDNivel
		INNER JOIN RelacaoHierarquica rh ON rh.ID = n.ID AND IDTipoNivelRelacionado = 9
		INNER JOIN Nivel nUpper ON nUpper.ID = rh.IDUpper AND nUpper.IDTipoNivel = 3
		INNER JOIN FRDBase frdUpper ON frdUpper.IDNivel = nUpper.ID
		INNER JOIN SFRDAvaliacao sfrdaUpper ON sfrdaUpper.IDFRDBase = frdUpper.ID AND sfrdaUpper.Preservar = 0
		INNER JOIN SFRDDatasProducao sfrddp ON sfrddp.IDFRDBase = frd.ID
		LEFT JOIN SFRDAvaliacao sfrda ON sfrda.IDFRDBase = frd.ID
	WHERE (sfrda.isDeleted IS NULL OR (sfrda.isDeleted = 0 AND sfrda.Preservar IS NULL))
		AND frd.ID = @IDFRDBase
		AND frd.isDeleted = 0
		AND n.isDeleted = 0
		AND rh.isDeleted = 0
		AND nUpper.isDeleted = 0
		AND frdUpper.isDeleted = 0
		AND sfrdaUpper.isDeleted = 0
		AND sfrddp.isDeleted = 0

	-- Caso o ID passado como argumento não corresponda a um FRD de um documento que constitua série então presume-se que se trata de um documento solto (se ainda assim não for, o resultado final será sempre 0)
	IF (@IDUpper IS NULL)
		SELECT @ano = sfrddp.FimAno, @mes = sfrddp.FimMes, @dia = sfrddp.FimDia, @prazo = sfrda.PrazoConservacao
		FROM FRDBase frd
			INNER JOIN Nivel n ON n.ID = frd.IDNivel
			INNER JOIN RelacaoHierarquica rh ON rh.ID = n.ID AND IDTipoNivelRelacionado = 9
			INNER JOIN Nivel nUpper ON nUpper.ID = rh.IDUpper AND nUpper.IDTipoNivel = 2
			INNER JOIN SFRDAvaliacao sfrda ON sfrda.IDFRDBase = frd.ID AND sfrda.Preservar = 0
			INNER JOIN SFRDDatasProducao sfrddp ON sfrddp.IDFRDBase = frd.ID
		WHERE frd.isDeleted = 0
			AND frd.ID = @IDFRDBase
			AND n.isDeleted = 0
			AND rh.isDeleted = 0
			AND nUpper.isDeleted = 0
			AND sfrda.isDeleted = 0
			AND sfrddp.isDeleted = 0
	
	IF (ISNUMERIC(@ano)  = 1 AND NOT (@prazo IS NULL))
	BEGIN		
		SET @ano = CAST( (CAST(@ano AS INT) + @prazo  ) AS NVARCHAR(4) )

		IF (dbo.fn_ComparePartialDate2(@ano, @mes, @dia, YEAR(@dt), MONTH(@dt) , DAY(@dt))= -1)
			RETURN 1
	END

	RETURN 0
END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

