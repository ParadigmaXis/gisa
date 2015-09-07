IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deleteDeletedRows]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_deleteDeletedRows]
GO

/****** Object:  StoredProcedure [dbo].[sp_deleteDeletedRows]    Script Date: 04/17/2009 15:29:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
 Elimina em definitivo as linhas que tiverem sido marcadas para eliminação pela anterior atribuição do valor isDeleted = 1
*/
CREATE PROCEDURE [dbo].[sp_deleteDeletedRows] @return BIT OUTPUT AS
BEGIN
	--DECLARE @return BIT
	/*
	DECLARE @deleteQuery NVARCHAR(4000)
	DECLARE EliminaApagadasCursor CURSOR FOR
	--SELECT 'SELECT * FROM ' + CONVERT(NVARCHAR(4000), dbo.sysobjects.name) + ' WHERE isDeleted=1'
	SELECT 'DELETE FROM ' + CONVERT(NVARCHAR(4000), dbo.sysobjects.name) + ' WHERE isDeleted=1'
	FROM dbo.sysobjects INNER JOIN
		dbo.syscolumns ON dbo.syscolumns.id = dbo.sysobjects.id 
	WHERE dbo.sysobjects.xtype='U' AND dbo.syscolumns.name = 'isDeleted'
	ORDER BY dbo.sysobjects.name
	
	
	OPEN EliminaApagadasCursor
	FETCH NEXT FROM EliminaApagadasCursor
	INTO @deleteQuery 
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
		EXEC sp_executesql @deleteQuery
		FETCH NEXT FROM EliminaApagadasCursor
		INTO @deleteQuery 
	END 
	
	CLOSE EliminaApagadasCursor 
	DEALLOCATE EliminaApagadasCursor 
	*/

	--ToDo: Desenvolver uma solução que automatize este processo tendo em atenção à ordem das tabelas

	DELETE FROM SFRDUFMateriaisComponente WHERE isDeleted = 1
	DELETE FROM SFRDUFTecnicasRegComponente WHERE isDeleted = 1
	DELETE FROM SFRDEstadoDeConservacao WHERE isDeleted = 1
	DELETE FROM SFRDTecnicasDeRegisto WHERE isDeleted = 1
	DELETE FROM SFRDFormaSuporteAcond WHERE isDeleted = 1
	DELETE FROM SFRDUFComponente WHERE isDeleted = 1
	DELETE FROM SFRDAvaliacaoRel WHERE isDeleted = 1
	DELETE FROM SFRDMaterialDeSuporte WHERE isDeleted = 1
	DELETE FROM SFRDOrdenacao WHERE isDeleted = 1
	DELETE FROM SFRDAvaliacao WHERE isDeleted = 1
	DELETE FROM FRDBaseDataDeDescricao WHERE isDeleted = 1
	DELETE FROM SFRDUFAutoEliminacao WHERE isDeleted = 1
	DELETE FROM SFRDTradicaoDocumental WHERE isDeleted = 1
	DELETE FROM IndexFRDCA WHERE isDeleted = 1
	DELETE FROM SFRDAutor WHERE isDeleted = 1
	DELETE FROM TrusteePrivilege WHERE isDeleted = 1
	DELETE FROM SFRDUFDescricaoFisica WHERE isDeleted = 1
	DELETE FROM SFRDUnidadeFisica WHERE isDeleted = 1
	DELETE FROM SFRDDatasProducao WHERE isDeleted = 1
	DELETE FROM SFRDNotaGeral WHERE isDeleted = 1
	DELETE FROM SFRDDimensaoSuporte WHERE isDeleted = 1
	DELETE FROM SFRDImagemObjetoDigital WHERE isDeleted = 1	
	DELETE FROM SFRDImagem WHERE isDeleted = 1	
	DELETE FROM NivelUnidadeFisicaDeposito WHERE isDeleted = 1
	--DELETE FROM NivelUnidadeFisica WHERE isDeleted = 1
	-- *
	DELETE FROM NivelUnidadeFisica FROM NivelUnidadeFisica nuf INNER JOIN NivelDesignado nd ON nd.ID=nuf.ID INNER JOIN Nivel n ON nd.ID=n.ID WHERE n.IDTipoNivel = 4 AND nd.isDeleted = 0 AND n.isDeleted = 1
	DELETE FROM NivelUnidadeFisica WHERE isDeleted = 1
	DELETE FROM SFRDUFCota WHERE isDeleted = 1
	DELETE FROM SFRDConteudoEEstrutura WHERE isDeleted = 1
	DELETE FROM SFRDLingua WHERE isDeleted = 1;
	DELETE FROM SFRDAlfabeto WHERE isDeleted = 1;
	DELETE FROM SFRDCondicaoDeAcesso WHERE isDeleted = 1
	DELETE FROM SFRDDocumentacaoAssociada WHERE isDeleted = 1
	DELETE FROM SFRDContexto WHERE isDeleted = 1
	--DELETE FROM RelacaoHierarquica WHERE isDeleted = 1
	-- *
	DELETE FROM RelacaoHierarquica FROM RelacaoHierarquica rh INNER JOIN Nivel n ON n.ID = rh.ID WHERE rh.IDTipoNivelRelacionado = 11 AND rh.isDeleted = 0 AND n.isDeleted = 1
	DELETE FROM RelacaoHierarquica WHERE isDeleted = 1
	DELETE FROM ObjetoDigitalRelacaoHierarquica WHERE isDeleted = 1
	DELETE FROM ProductFunction WHERE isDeleted = 1
	DELETE FROM ControloAutDataDeDescricao WHERE isDeleted = 1
	DELETE FROM ControloAutDicionario WHERE isDeleted = 1
	DELETE FROM ControloAutRel WHERE isDeleted = 1
	DELETE FROM UserGroups WHERE isDeleted = 1
	DELETE FROM ControloAutDatasExistencia WHERE isDeleted = 1
	DELETE FROM FunctionOperation WHERE isDeleted = 1
	DELETE FROM NivelControloAut WHERE isDeleted = 1
	DELETE FROM ControloAutEntidadeProdutora WHERE isDeleted = 1
	
	DELETE FROM NivelUnidadeFisicaCodigo WHERE isDeleted = 1
	DELETE FROM TrusteeNivelPrivilege WHERE isDeleted = 1
	DELETE FROM TrusteeDepositoPrivilege WHERE isDeleted = 1
	DELETE FROM TrusteeObjetoDigitalPrivilege WHERE isDeleted = 1
	DELETE FROM ObjetoDigital WHERE isDeleted = 1
	DELETE FROM RelacaoTipoNivelRelacionado WHERE isDeleted = 1
	DELETE FROM NivelDocumentoSimples WHERE isDeleted = 1
	--DELETE FROM NivelDesignado WHERE isDeleted = 1
	-- *
	DELETE FROM NivelDesignado FROM NivelDesignado nd INNER JOIN Nivel n ON nd.ID=n.ID WHERE n.IDTipoNivel = 4 AND nd.isDeleted = 0 AND n.isDeleted = 1
	DELETE FROM NivelDesignado WHERE isDeleted = 1
	DELETE FROM NivelImagemIlustracao WHERE isDeleted = 1
	DELETE FROM TipoNivelRelacionadoCodigo WHERE isDeleted = 1
	DELETE FROM ClientActivity WHERE isDeleted = 1
	DELETE FROM TipoNivelRelacionado WHERE isDeleted = 1
	DELETE FROM ClientLicense WHERE isDeleted = 1
	DELETE FROM TipoNoticiaATipoControloAForma WHERE isDeleted = 1
	DELETE FROM NivelTipoOperation WHERE isDeleted = 1
	DELETE FROM ObjetoDigitalTipoOperation WHERE isDeleted = 1
	UPDATE TipoFunction SET IDTipoFunctionGroupContext = NULL, IdxTipoFunctionGroupContext = NULL WHERE isDeleted = 1
	UPDATE TrusteeUser SET IDTrusteeUserDefaultAuthority = NULL WHERE isDeleted = 1
	DELETE FROM TrusteeUser WHERE isDeleted = 1
	DELETE FROM TipoSubDensidade WHERE isDeleted = 1
	DELETE FROM DocumentosMovimentados WHERE isDeleted = 1	
	DELETE FROM Movimento WHERE isDeleted = 1
	DELETE FROM Integ_RelacaoExternaControloAut WHERE isDeleted = 1
	DELETE FROM Integ_RelacaoExternaNivel WHERE isDeleted = 1
	DELETE FROM ControloAut WHERE isDeleted = 1	
	DELETE FROM LicencaObraDataLicencaConstrucao WHERE isDeleted = 1
	DELETE FROM LicencaObraLocalizacaoObraActual WHERE isDeleted = 1
	DELETE FROM LicencaObraLocalizacaoObraAntiga WHERE isDeleted = 1
	DELETE FROM LicencaObraRequerentes WHERE isDeleted = 1
	DELETE FROM LicencaObraTecnicoObra WHERE isDeleted = 1
	DELETE FROM LicencaObraAtestadoHabitabilidade WHERE isDeleted = 1
	DELETE FROM LicencaObra WHERE isDeleted = 1
	DELETE FROM SFRDAgrupador WHERE isDeleted = 1
	DELETE FROM Codigo WHERE isDeleted = 1
	DELETE FROM FRDBase WHERE isDeleted = 1
	DELETE FROM Nivel WHERE isDeleted = 1
	DELETE FROM TrusteeGroup WHERE isDeleted = 1
	DELETE FROM Iso639 WHERE isDeleted = 1
	DELETE FROM TipoTecnicasDeRegisto WHERE isDeleted = 1
	DELETE FROM Iso15924 WHERE isDeleted = 1
	DELETE FROM AutoEliminacao WHERE isDeleted = 1
	DELETE FROM TipoEstadoDeConservacao WHERE isDeleted = 1
	DELETE FROM TipoOrdenacao WHERE isDeleted = 1
	DELETE FROM TipoNoticiaAut WHERE isDeleted = 1
	DELETE FROM TipoFormaSuporteAcond WHERE isDeleted = 1
	DELETE FROM TipoFunctionGroup WHERE isDeleted = 1
	DELETE FROM Dicionario WHERE isDeleted = 1
	DELETE FROM TipoFRDBase WHERE isDeleted = 1
	DELETE FROM TipoOperation WHERE isDeleted = 1
	DELETE FROM TipoControloAutRel WHERE isDeleted = 1
	DELETE FROM TipoControloAutForma WHERE isDeleted = 1
	DELETE FROM TipoEntidadeProdutora WHERE isDeleted = 1
	DELETE FROM GlobalConfig WHERE isDeleted = 1
	DELETE FROM TipoTradicaoDocumental WHERE isDeleted = 1
	DELETE FROM TipoMaterial WHERE isDeleted = 1
	DELETE FROM TipoNivel WHERE isDeleted = 1
	DELETE FROM SFRDImagemVolume WHERE isDeleted = 1
	DELETE FROM TipoTecnicaRegisto WHERE isDeleted = 1
	DELETE FROM TipoSuporte WHERE isDeleted = 1
	DELETE FROM TipoPertinencia WHERE isDeleted = 1
	DELETE FROM TipoServer WHERE isDeleted = 1
	--DELETE FROM LicenseModules WHERE isDeleted = 1
	DELETE FROM Modules WHERE isDeleted = 1
	DELETE FROM Iso3166 WHERE isDeleted = 1
	DELETE FROM TipoMaterialDeSuporte WHERE isDeleted = 1
	DELETE FROM Trustee WHERE isDeleted = 1
	DELETE FROM TipoAcondicionamento WHERE isDeleted = 1
	DELETE FROM TipoEstadoConservacao WHERE isDeleted = 1
	DELETE FROM TipoDensidade WHERE isDeleted = 1
	DELETE FROM TipoMedida WHERE isDeleted = 1
	DELETE FROM Deposito WHERE isDeleted = 1
	DELETE FROM ObjetoDigitalTitulo WHERE isDeleted = 1
	DELETE FROM LocalConsulta WHERE isDeleted = 1
	-- * as 3 linhas marcadas com ' * ' resolvem temporariamente um bug relacionado com UFs em que uma UF pode ser eliminada sem que a sua rela??o ? respectiva ED o seja

	-- indicar que a "limpeza" foi conclu?da com ?xito
	SET @return = 1
	--RETURN @return

END
GO
