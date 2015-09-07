SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_reportAutoEliminacaoPortaria]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sp_reportAutoEliminacaoPortaria]
GO

CREATE PROCEDURE sp_reportAutoEliminacaoPortaria @IDTrustee BIGINT, @IDAutoEliminacao BIGINT
AS
BEGIN
	DECLARE @IDTipoFRDBaseRecolha BIGINT
	DECLARE @IDTipoFRDBaseUnidadeFisica BIGINT
	DECLARE @IDTipoNivelDocumental BIGINT
	DECLARE @IDTipoNivelRelacionadoDocumento BIGINT
	DECLARE @IDTipoNivelRelacionadoSerie BIGINT
	DECLARE @IDTipoNivelOutro BIGINT
	DECLARE @IDTipoNivelRelacionadoSubSerie BIGINT
	SET @IDTipoFRDBaseRecolha = 1
	SET @IDTipoFRDBaseUnidadeFisica = 2
	SET @IDTipoNivelDocumental = 3
	SET @IDTipoNivelOutro = 4
	SET @IDTipoNivelRelacionadoDocumento = 9
	SET @IDTipoNivelRelacionadoSerie = 7
	SET @IDTipoNivelRelacionadoSubSerie = 8

	-- tabela temporaria, ir? conter todos os documentos que devam constar do auto de eliminacao
	CREATE TABLE #Documentos (ID BIGINT, IDSerie BIGINT);

	-- * todas as unidades fisicas que tenham sido explicitamente seleccionadas para o auto de elimina??o
	-- * todas as unidades fisicas que tenham documentos que devam constar do auto de elimina??o
	CREATE TABLE #UnidadesFisicas (ID BIGINT, Seleccionada BIT, IDDocumental BIGINT, DataInicio NCHAR(8), DataFim NCHAR(8)); -- IDs n?o ?nicos se uma UF estiver associada a mais que um n?vel documental eliminado

	-- * todas as s?ries que tenham documentos que devam constar do auto de elimina??o
	-- * todas as s?ries que tenham UFs que tenham sido explicitamente seleccionadas para o auto de elimina??o
	-- * todas as s?ries que tenham UFs com documentos que devam constar do auto de elimina??o
	CREATE TABLE #Series (ID BIGINT, RefTab int, Designacao nvarchar(768), Suporte nvarchar(50));

	-- Obter todos os documentos incluidos no auto de eliminacao especificado
	-- Determinar o nivel acima para todos os documentos que constituam serie
	INSERT INTO #Documentos (ID, IDSerie) -- distinct ? necess?rio por causa do join com a tabela RelacaoHierarquica, ? conta do qual podem aparecer v?rios resultados para o mesmo documento, se este n?o constituir s?rie e tiver v?rios produtores
	SELECT DISTINCT rh.ID, rh.IDUpper -- os documentos soltos s?o excluidos
	FROM RelacaoHierarquica rh
		INNER JOIN FRDBase frd ON frd.IDNivel = rh.ID AND frd.isDeleted = 0
		INNER JOIN SFRDAvaliacao frda ON frda.IDFRDBase = frd.ID AND frda.isDeleted = 0
		INNER JOIN Nivel nUpper ON nUpper.ID = rh.IDUpper AND nUpper.IDTipoNivel = @IDTipoNivelDocumental AND nUpper.isDeleted = 0
	WHERE rh.IDTipoNivelRelacionado = @IDTipoNivelRelacionadoDocumento
		AND frda.IDAutoEliminacao = @IDAutoEliminacao 
		AND rh.isDeleted = 0

	-- unidades f?sicas que tenham documentos com o auto (? passada juntamente a (sub)s?rie)
	INSERT INTO #UnidadesFisicas
	SELECT sfrduf.IDNivel, 0, #Documentos.IDSerie, 
		dbo.fn_AddPaddingToDateMember_new(dp.InicioAno, 4) + dbo.fn_AddPaddingToDateMember_new(dp.InicioMes, 2) + dbo.fn_AddPaddingToDateMember_new(dp.InicioDia, 2),
		dbo.fn_AddPaddingToDateMember_new(dp.FimAno, 4) + dbo.fn_AddPaddingToDateMember_new(dp.FimMes, 2) + dbo.fn_AddPaddingToDateMember_new(dp.FimDia, 2)
	FROM SFRDUnidadeFisica sfrduf
		INNER JOIN FRDBase frdDoc ON frdDoc.ID = sfrduf.IDFRDBase AND frdDoc.IDTipoFRDBase = 1 AND frdDoc.isDeleted = 0
		INNER JOIN FRDBase frdUF ON frdUF.IDNivel = sfrduf.IDNivel AND frdUF.isDeleted = 0
		LEFT JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frdUF.ID AND dp.isDeleted = 0
		INNER JOIN #Documentos ON #Documentos.ID = frdDoc.IDNivel
	WHERE sfrduf.isDeleted = 0
		
	-- unidades f?sicas directamente associadas ao auto de elimina??o
	-- NOTA: ? garantido que os n?veis documentais s?o (sub)s?ries
	INSERT INTO #UnidadesFisicas
	SELECT nUF.ID, 1, frdDoc.IDNivel,
		dbo.fn_AddPaddingToDateMember_new(dp.InicioAno, 4) + dbo.fn_AddPaddingToDateMember_new(dp.InicioMes, 2) + dbo.fn_AddPaddingToDateMember_new(dp.InicioDia, 2),
		dbo.fn_AddPaddingToDateMember_new(dp.FimAno, 4) + dbo.fn_AddPaddingToDateMember_new(dp.FimMes, 2) + dbo.fn_AddPaddingToDateMember_new(dp.FimDia, 2)
	FROM Nivel nUF
		INNER JOIN FRDBase frdUF ON frdUF.IDNivel = nUF.ID AND frdUF.isDeleted = 0
		LEFT JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frdUF.ID AND dp.isDeleted = 0
		INNER JOIN SFRDUFAutoEliminacao sfrdae ON sfrdae.IDFRDBase = frdUF.ID AND sfrdae.isDeleted = 0
		INNER JOIN SFRDUnidadeFisica sfrduf ON sfrduf.IDNivel = nUF.ID AND sfrduf.isDeleted = 0
		INNER JOIN FRDBase frdDoc ON frdDoc.ID = sfrduf.IDFRDBase AND frdDoc.isDeleted = 0
	WHERE sfrdae.IDAutoEliminacao = @IDAutoEliminacao
		AND nUF.IDTipoNivel = @IDTipoNivelOutro
		AND	nUF.isDeleted = 0
		
	-- obter as s?ries que tenham documentos com o auto
	-- e obter as que tenham unidades f?sicas seleccionadas para o auto
	INSERT INTO #Series (ID, RefTab, Designacao)
	SELECT DISTINCT nSerie.ID, a.RefTabelaAvaliacao, nd.Designacao--, tfsa.Designacao supDesign -- distinct ? necess?rio por causa do Join com a tabela RelacaoHierarquica
	FROM #Documentos
		INNER JOIN Nivel nSerie ON nSerie.ID = #Documentos.IDSerie AND nSerie.IDTipoNivel = @IDTipoNivelDocumental AND nSerie.isDeleted = 0 -- este IDSerie cont?m s?ries e subs?ries
		INNER JOIN NivelDesignado nd ON nd.ID = nSerie.ID AND nd.isDeleted = 0
		INNER JOIN FRDBase frdSerie ON frdSerie.IDNivel = nSerie.ID AND frdSerie.IDTipoFRDBase = 1 AND frdSerie.isDeleted = 0
		LEFT JOIN SFRDAvaliacao a ON a.IDFRDBase = frdSerie.ID AND a.isDeleted = 0
	UNION
	SELECT DISTINCT nSerie.ID, a.RefTabelaAvaliacao, nd.Designacao--, tfsa.Designacao supDesign -- distinct ? necess?rio por causa do Join com a tabela RelacaoHierarquica
	FROM #UnidadesFisicas
		INNER JOIN Nivel nSerie ON nSerie.ID = #UnidadesFisicas.IDDocumental AND nSerie.IDTipoNivel = @IDTipoNivelDocumental AND nSerie.isDeleted = 0
		INNER JOIN NivelDesignado nd ON nd.ID = nSerie.ID AND nd.isDeleted = 0
		INNER JOIN FRDBase frdSerie ON frdSerie.IDNivel = nSerie.ID AND frdSerie.IDTipoFRDBase = 1 AND frdSerie.isDeleted = 0
		LEFT JOIN SFRDAvaliacao a ON a.IDFRDBase = frdSerie.ID AND a.isDeleted = 0
	WHERE #UnidadesFisicas.Seleccionada = 1

	-- obter conjuntos de s?ries por tipo de acondicionamento
	CREATE TABLE #Info (IDSerie bigint, IDTipoAcondicionamento bigint, NroUFs bigint, Metragem numeric(18,3), DataInicio NCHAR(8), DataFim NCHAR(8))
	INSERT INTO #Info
	SELECT #Series.ID, df.IDTipoAcondicionamento, COUNT(#UnidadesFisicas.ID), SUM(df.MedidaLargura), MIN(#UnidadesFisicas.DataInicio), MAX(#UnidadesFisicas.DataFim)
	FROM #Series
		INNER JOIN #UnidadesFisicas ON #UnidadesFisicas.IDDocumental = #Series.ID
		INNER JOIN FRDBase frdUF ON frdUF.IDNivel = #UnidadesFisicas.ID
		INNER JOIN SFRDUFDescricaoFisica df ON df.IDFRDBase = frdUF.ID
	GROUP BY #Series.ID, df.IDTipoAcondicionamento
	
	-- para cada s?rie encontrar as refer?ncias na tabela
	CREATE TABLE #RefTab (IDSerie bigint, IDSerieUpper bigint, RefTab int)
	INSERT INTO #RefTab
	SELECT #Series.ID, null, a.RefTabelaAvaliacao
	FROM #Series
		INNER JOIN FRDBase frdNivel ON frdNivel.IDNivel = #Series.ID
		INNER JOIN SFRDAvaliacao a ON a.IDFRDBase = frdNivel.ID
		
	WHILE (@@ROWCOUNT>0)
	BEGIN
		UPDATE #RefTab
		SET RefTab = a.RefTabelaAvaliacao, IDSerieUpper = nUpper.ID
		FROM #RefTab
			INNER JOIN RelacaoHierarquica rh ON rh.ID = #RefTab.IDSerie
			INNER JOIN Nivel nUpper ON nUpper.ID = rh.IDUpper AND nUpper.IDTipoNivel = @IDTipoNivelDocumental
			INNER JOIN FRDBase frdUpper ON frdUpper.IDNivel = nUpper.ID
			LEFT JOIN SFRDAvaliacao a ON a.IDFRDBase = frdUpper.ID
		WHERE #RefTab.RefTab IS NULL AND a.RefTabelaAvaliacao IS NOT NULL
	END

	-- tabela que vai contar cada entrada na tabela do relat?rio
	CREATE TABLE #Entries (
		seq_id INT IDENTITY(1,1) NOT NULL, 
		IDSerie bigint, 
		IDTipoAcondicionamento bigint, 
		Designacao nvarchar(768),
		NroUfsPorTipo nvarchar (50),
		DataInicio nchar(8), DataFim nchar(8),
		Metragem numeric(18,3)
	)

	INSERT INTO #Entries (IDSerie, IDTipoAcondicionamento, Designacao, NroUfsPorTipo, DataInicio, DataFim, Metragem)
	SELECT #Series.ID, #Info.IDTipoAcondicionamento, 
		#Series.Designacao, convert(nvarchar, #Info.NroUFs) + ' ' + ta.Designacao + '(s)', #Info.DataInicio, #Info.DataFim, #Info.Metragem 
	FROM #Info
		INNER JOIN #RefTab ON #RefTab.IDSerie = #Info.IDSerie
		INNER JOIN #Series ON #Series.ID = #Info.IDSerie
		INNER JOIN TipoAcondicionamento ta ON ta.ID = #Info.IDTipoAcondicionamento
	
	-- Retornar suporte das s?ries
	SELECT #Series.ID, tms.Designacao
	FROM #Series 
		INNER JOIN FRDBase frd ON frd.IDNivel = #Series.ID
		INNER JOIN SFRDConteudoEEstrutura ce ON ce.IDFRDBase = frd.ID
		INNER JOIN SFRDMaterialDeSuporte ms ON ms.IDFRDBase = ce.IDFRDBase
		INNER JOIN TipoMaterialDeSuporte tms ON tms.ID = ms.IDTipoMaterialDeSuporte	
	
	-- Retornar as refer?ncias na tabela de cada s?rie
	SELECT * FROM #RefTab
	SELECT * FROM #Entries

	-- Retornar Cotas, Guias das UFs
	SELECT #Series.ID, df.IDTipoAcondicionamento, c.Cota, nuf.GuiaIncorporacao
	FROM #Series
		INNER JOIN #UnidadesFisicas ON #UnidadesFisicas.IDDocumental = #Series.ID
		INNER JOIN FRDBase frdUF ON frdUF.IDNivel = #UnidadesFisicas.ID
		INNER JOIN SFRDUFDescricaoFisica df ON df.IDFRDBase = frdUF.ID
		INNER JOIN SFRDUFCota c ON c.IDFRDBase = frdUF.ID 
		INNER JOIN NivelUnidadeFisica nuf ON nuf.ID = #UnidadesFisicas.ID
	
	DROP TABLE #Documentos
	DROP TABLE #UnidadesFisicas
	DROP TABLE #Series
	DROP TABLE #Entries
	DROP TABLE #RefTab
	DROP TABLE #Info
END
GO