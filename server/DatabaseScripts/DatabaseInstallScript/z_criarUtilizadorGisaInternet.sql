-- Script para acriação no SqlServer do utilizador do 
-- Gisa sincronizando-o com o mesmo utilizador existente 
-- na base de dados
--USE GISA
IF NOT EXISTS (SELECT * FROM master.dbo.syslogins WHERE loginname = N'gisainternet')
BEGIN
	DECLARE @logindb NVARCHAR(132), @loginlang NVARCHAR(132) SELECT @logindb = N'master', @loginlang = N'Português'
	IF @logindb IS NULL OR NOT EXISTS (SELECT * FROM master.dbo.sysdatabases WHERE name = @logindb)
		SELECT @logindb = N'master'
	IF @loginlang IS NULL OR (NOT EXISTS (SELECT * FROM master.dbo.syslanguages WHERE name = @loginlang) AND @loginlang <> N'us_english')
		SELECT @loginlang = @@language
	EXEC sp_addlogin N'gisainternet', 'teste', @logindb, @loginlang
END
GO

if not exists (select * from dbo.sysusers where name = N'gisainternet')
	EXEC sp_grantdbaccess N'gisainternet'
GO

EXEC sp_change_users_login 'update_one', 'gisainternet', 'gisainternet'

--Set Object Specific Permissions
GRANT SELECT ON OBJECT::AutoEliminacao TO gisainternet
GRANT SELECT ON OBJECT::ClientActivity TO gisainternet
GRANT SELECT ON OBJECT::ClientLicense TO gisainternet
GRANT SELECT ON OBJECT::Codigo TO gisainternet
GRANT SELECT ON OBJECT::ControloAut TO gisainternet
GRANT SELECT ON OBJECT::ControloAutDataDeDescricao TO gisainternet
GRANT SELECT ON OBJECT::ControloAutDatasExistencia TO gisainternet
GRANT SELECT ON OBJECT::ControloAutDicionario TO gisainternet
GRANT SELECT ON OBJECT::ControloAutEntidadeProdutora TO gisainternet
GRANT SELECT ON OBJECT::ControloAutRel TO gisainternet
GRANT SELECT ON OBJECT::Dicionario TO gisainternet
GRANT SELECT ON OBJECT::DocumentosMovimentados TO gisainternet
GRANT SELECT ON OBJECT::FRDBase TO gisainternet
GRANT SELECT ON OBJECT::FRDBaseDataDeDescricao TO gisainternet
GRANT SELECT ON OBJECT::FunctionOperation TO gisainternet
GRANT SELECT ON OBJECT::GlobalConfig TO gisainternet
GRANT SELECT ON OBJECT::IndexFRDCA TO gisainternet
GRANT SELECT ON OBJECT::Iso15924 TO gisainternet
GRANT SELECT ON OBJECT::Iso3166 TO gisainternet
GRANT SELECT ON OBJECT::Iso639 TO gisainternet
GRANT SELECT ON OBJECT::LicenseModules TO gisainternet
GRANT SELECT ON OBJECT::ListaModelosAvaliacao TO gisainternet
GRANT SELECT ON OBJECT::ModelosAvaliacao TO gisainternet
GRANT SELECT ON OBJECT::Modules TO gisainternet
GRANT SELECT ON OBJECT::Movimento TO gisainternet
GRANT SELECT ON OBJECT::MovimentoEntidade TO gisainternet
GRANT SELECT ON OBJECT::Nivel TO gisainternet
GRANT SELECT ON OBJECT::NivelControloAut TO gisainternet
GRANT SELECT ON OBJECT::NivelDesignado TO gisainternet
GRANT SELECT ON OBJECT::NivelTipoOperation TO gisainternet
GRANT SELECT ON OBJECT::NivelUnidadeFisica TO gisainternet
GRANT SELECT ON OBJECT::NivelUnidadeFisicaCodigo TO gisainternet
GRANT SELECT ON OBJECT::NivelUnidadeFisicaDeposito TO gisainternet 
GRANT SELECT ON OBJECT::ProductFunction TO gisainternet
GRANT SELECT ON OBJECT::RelacaoHierarquica TO gisainternet
GRANT SELECT ON OBJECT::RelacaoTipoNivelRelacionado TO gisainternet
GRANT SELECT, DELETE, INSERT, UPDATE ON OBJECT::SearchCacheWeb TO gisainternet
GRANT SELECT ON OBJECT::SFRDAlfabeto TO gisainternet
GRANT SELECT ON OBJECT::SFRDAutor TO gisainternet
GRANT SELECT ON OBJECT::SFRDAvaliacao TO gisainternet
GRANT SELECT ON OBJECT::SFRDAvaliacaoRel TO gisainternet
GRANT SELECT ON OBJECT::SFRDCondicaoDeAcesso TO gisainternet
GRANT SELECT ON OBJECT::SFRDConteudoEEstrutura TO gisainternet
GRANT SELECT ON OBJECT::SFRDContexto TO gisainternet
GRANT SELECT ON OBJECT::SFRDDatasProducao TO gisainternet
GRANT SELECT ON OBJECT::SFRDDimensaoSuporte TO gisainternet
GRANT SELECT ON OBJECT::SFRDDocumentacaoAssociada TO gisainternet
GRANT SELECT ON OBJECT::SFRDEstadoDeConservacao TO gisainternet
GRANT SELECT ON OBJECT::SFRDFormaSuporteAcond TO gisainternet
GRANT SELECT ON OBJECT::SFRDImagem TO gisainternet
GRANT SELECT ON OBJECT::SFRDImagemVolume TO gisainternet
GRANT SELECT ON OBJECT::SFRDLingua TO gisainternet
GRANT SELECT ON OBJECT::SFRDMaterialDeSuporte TO gisainternet
GRANT SELECT ON OBJECT::SFRDNotaGeral TO gisainternet
GRANT SELECT ON OBJECT::SFRDOrdenacao TO gisainternet
GRANT SELECT ON OBJECT::SFRDTecnicasDeRegisto TO gisainternet
GRANT SELECT ON OBJECT::SFRDTradicaoDocumental TO gisainternet
GRANT SELECT ON OBJECT::SFRDUFAutoEliminacao TO gisainternet
GRANT SELECT ON OBJECT::SFRDUFComponente TO gisainternet
GRANT SELECT ON OBJECT::SFRDUFCota TO gisainternet
GRANT SELECT ON OBJECT::SFRDUFDescricaoFisica TO gisainternet
GRANT SELECT ON OBJECT::SFRDUFMateriaisComponente TO gisainternet
GRANT SELECT ON OBJECT::SFRDUFTecnicasRegComponente TO gisainternet
GRANT SELECT ON OBJECT::SFRDUnidadeFisica TO gisainternet
GRANT SELECT ON OBJECT::TipoAcondicionamento TO gisainternet
GRANT SELECT ON OBJECT::TipoControloAutForma TO gisainternet
GRANT SELECT ON OBJECT::TipoControloAutRel TO gisainternet
GRANT SELECT ON OBJECT::TipoDensidade TO gisainternet
GRANT SELECT ON OBJECT::TipoEntidadeProdutora TO gisainternet
GRANT SELECT ON OBJECT::TipoEntrega TO gisainternet
GRANT SELECT ON OBJECT::TipoEstadoConservacao TO gisainternet
GRANT SELECT ON OBJECT::TipoEstadoDeConservacao TO gisainternet
GRANT SELECT ON OBJECT::TipoFormaSuporteAcond TO gisainternet
GRANT SELECT ON OBJECT::TipoFRDBase TO gisainternet
GRANT SELECT ON OBJECT::TipoFunction TO gisainternet
GRANT SELECT ON OBJECT::TipoFunctionGroup TO gisainternet
GRANT SELECT ON OBJECT::TipoMaterial TO gisainternet
GRANT SELECT ON OBJECT::TipoMaterialDeSuporte TO gisainternet
GRANT SELECT ON OBJECT::TipoMedida TO gisainternet
GRANT SELECT ON OBJECT::TipoNivel TO gisainternet
GRANT SELECT ON OBJECT::TipoNivelRelacionado TO gisainternet
GRANT SELECT ON OBJECT::TipoNivelRelacionadoCodigo TO gisainternet
GRANT SELECT ON OBJECT::TipoNoticiaATipoControloAForma TO gisainternet
GRANT SELECT ON OBJECT::TipoNoticiaAut TO gisainternet
GRANT SELECT ON OBJECT::TipoOperation TO gisainternet
GRANT SELECT ON OBJECT::TipoOrdenacao TO gisainternet
GRANT SELECT ON OBJECT::TipoPertinencia TO gisainternet
GRANT SELECT ON OBJECT::TipoServer TO gisainternet
GRANT SELECT ON OBJECT::TipoSubDensidade TO gisainternet
GRANT SELECT ON OBJECT::TipoSuporte TO gisainternet
GRANT SELECT ON OBJECT::TipoTecnicaRegisto TO gisainternet
GRANT SELECT ON OBJECT::TipoTecnicasDeRegisto TO gisainternet
GRANT SELECT ON OBJECT::TipoTradicaoDocumental TO gisainternet
GRANT SELECT ON OBJECT::Trustee TO gisainternet
GRANT SELECT ON OBJECT::TrusteeGroup TO gisainternet
GRANT SELECT ON OBJECT::TrusteeNivelPrivilege TO gisainternet
GRANT SELECT ON OBJECT::TrusteePrivilege TO gisainternet
GRANT SELECT ON OBJECT::TrusteeUser TO gisainternet
GRANT SELECT ON OBJECT::UserGroups TO gisainternet
GRANT SELECT, DELETE, INSERT, UPDATE ON OBJECT::WebClientActivity TO gisainternet
GRANT SELECT ON OBJECT::Integ_Sistema TO gisainternet 
GRANT SELECT ON OBJECT::Integ_TipoEntidade TO gisainternet 
GRANT SELECT ON OBJECT::Integ_EntidadeExterna TO gisainternet 
GRANT SELECT ON OBJECT::Integ_RelacaoExternaNivel TO gisainternet
GRANT SELECT ON OBJECT::Integ_RelacaoExternaControloAut TO gisainternet 
GRANT SELECT ON OBJECT::LicencaObra TO gisainternet 
GRANT SELECT ON OBJECT::LicencaObraAtestadoHabitabilidade TO gisainternet 
GRANT SELECT ON OBJECT::LicencaObraDataLicencaConstrucao TO gisainternet 
GRANT SELECT ON OBJECT::LicencaObraLocalizacaoObraActual TO gisainternet 
GRANT SELECT ON OBJECT::LicencaObraLocalizacaoObraAntiga TO gisainternet 
GRANT SELECT ON OBJECT::LicencaObraRequerentes TO gisainternet 
GRANT SELECT ON OBJECT::LicencaObraTecnicoObra TO gisainternet 
GRANT SELECT ON OBJECT::TipoTipologias TO gisainternet
GRANT SELECT ON OBJECT::Deposito TO gisainternet
GRANT SELECT ON OBJECT::TrusteeDepositoPrivilege TO gisainternet
GRANT SELECT ON OBJECT::DepositoTipoOperation TO gisainternet
GRANT SELECT ON OBJECT::SFRDAgrupador TO gisainternet
GRANT SELECT ON OBJECT::ObjetoDigitalTipoOperation TO gisainternet
GRANT SELECT ON OBJECT::TrusteeObjetoDigitalPrivilege TO gisainternet
GRANT SELECT ON OBJECT::ObjetoDigitalTitulo TO gisainternet
GRANT SELECT ON OBJECT::ObjetoDigital TO gisainternet
GRANT SELECT ON OBJECT::ObjetoDigitalRelacaoHierarquica TO gisainternet
GRANT SELECT ON OBJECT::SFRDImagemObjetoDigital TO gisainternet
GRANT SELECT ON OBJECT::NivelDocumentoSimples TO gisainternet
GRANT SELECT ON OBJECT::NivelImagemIlustracao TO gisainternet
GRANT SELECT ON OBJECT::NivelDocumentoSimples TO gisainternet
GRANT SELECT ON OBJECT::NivelImagemIlustracao TO gisainternet
GRANT SELECT ON OBJECT::LocalConsulta TO gisainternet
GRANT SELECT ON OBJECT::ConfigAlfabeto TO gisainternet
GRANT SELECT ON OBJECT::ConfigLingua TO gisainternet
GRANT SELECT ON OBJECT::ObjetoDigitalStatus TO gisainternet

GRANT EXECUTE ON OBJECT::Search_Estrutura TO gisainternet
GRANT EXECUTE ON OBJECT::sp_actionLogin TO gisainternet
GRANT EXECUTE ON OBJECT::sp_actionLogout TO gisainternet
GRANT EXECUTE ON OBJECT::sp_actionPingDB TO gisainternet
GRANT EXECUTE ON OBJECT::sp_avaliaDocumetosTabela TO gisainternet
GRANT EXECUTE ON OBJECT::sp_canCreateRH TO gisainternet
GRANT EXECUTE ON OBJECT::sp_clearAvaliacaoTabela TO gisainternet
GRANT EXECUTE ON OBJECT::sp_deleteDeletedRows TO gisainternet
GRANT EXECUTE ON OBJECT::sp_deleteFRD TO gisainternet
GRANT EXECUTE ON OBJECT::sp_deleteNivel TO gisainternet
GRANT EXECUTE ON OBJECT::sp_ExistsControloAutDicionario TO gisainternet
GRANT EXECUTE ON OBJECT::sp_genTree TO gisainternet
GRANT EXECUTE ON OBJECT::sp_genTreeLevel TO gisainternet
GRANT EXECUTE ON OBJECT::sp_getAutosEliminacao TO gisainternet
GRANT EXECUTE ON OBJECT::sp_getCANiveisAssociados TO gisainternet
GRANT EXECUTE ON OBJECT::sp_getCANiveisDocAssociados TO gisainternet
GRANT EXECUTE ON OBJECT::sp_getCodigoCompletoNivel TO gisainternet
GRANT EXECUTE ON OBJECT::sp_getCodigosCompletosNiveis TO gisainternet
GRANT EXECUTE ON OBJECT::sp_getDocumentosComProdutores TO gisainternet
GRANT EXECUTE ON OBJECT::sp_getEffectivePermissions TO gisainternet
GRANT EXECUTE ON OBJECT::sp_getEffectiveReadPermissions TO gisainternet
GRANT EXECUTE ON OBJECT::sp_getEffectiveReadWritePermissions TO gisainternet
GRANT EXECUTE ON OBJECT::sp_getEntidadeDetentoraForNivel TO gisainternet
GRANT EXECUTE ON OBJECT::sp_getEntidadesProdutorasHerdadas TO gisainternet
GRANT EXECUTE ON OBJECT::sp_getImplicitPermissions TO gisainternet
GRANT EXECUTE ON OBJECT::sp_getLocalConsulta TO gisainternet
GRANT EXECUTE ON OBJECT::sp_getNivelEstruturalCount TO gisainternet
GRANT EXECUTE ON OBJECT::sp_getObjetosDigitaisPublicados TO gisainternet
GRANT EXECUTE ON OBJECT::sp_getODEffectivePermissions TO gisainternet
GRANT EXECUTE ON OBJECT::sp_getPossibleSubTypesOf TO gisainternet
GRANT EXECUTE ON OBJECT::sp_getSortedSubNiveisOf TO gisainternet
GRANT EXECUTE ON OBJECT::sp_getUFsEDocsAssociados TO gisainternet
GRANT EXECUTE ON OBJECT::sp_loadUFUnidadesDescricao TO gisainternet
GRANT EXECUTE ON OBJECT::sp_manageListaModelosAvaliacao TO gisainternet
GRANT EXECUTE ON OBJECT::sp_manageModelosAvaliacao TO gisainternet
GRANT EXECUTE ON OBJECT::sp_publishSubDocumentos TO gisainternet
GRANT EXECUTE ON OBJECT::sp_registerLicense TO gisainternet
GRANT EXECUTE ON OBJECT::sp_removeOldODsFromQueue TO gisainternet
GRANT EXECUTE ON OBJECT::sp_reportAutoEliminacao TO gisainternet
GRANT EXECUTE ON OBJECT::sp_reportAutoEliminacaoPortaria TO gisainternet
GRANT EXECUTE ON OBJECT::sp_reportInventario TO gisainternet
GRANT EXECUTE ON OBJECT::sp_reportParameterAddNivel TO gisainternet
GRANT EXECUTE ON OBJECT::sp_reportSearchResults TO gisainternet
GRANT EXECUTE ON OBJECT::sp_updateODStatusInQueue TO gisainternet
GRANT EXECUTE ON OBJECT::sp_validateUser TO gisainternet

GRANT EXECUTE ON fn_AddPaddingToDateMember TO gisainternet
GRANT EXECUTE ON fn_AddPaddingToDateMember_new TO gisainternet
GRANT EXECUTE ON fn_AddPaddingToDateMember_new2 TO gisainternet
GRANT EXECUTE ON fn_ComparePartialDate TO gisainternet
GRANT EXECUTE ON fn_ComparePartialDate2 TO gisainternet
GRANT EXECUTE ON fn_ComparePartialNumber TO gisainternet
GRANT EXECUTE ON fn_ComparePartialNumber2 TO gisainternet
GRANT EXECUTE ON fn_isExpiredSession TO gisainternet
GRANT EXECUTE ON fn_IsPrazoElimExp TO gisainternet

GRANT CONTROL ON TYPE::[dbo].[SearchIDs] TO gisainternet

GO

