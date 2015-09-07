using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
	public class SqlClientRelatorioRule: RelatorioRule
	{
        protected override List<ReportParameter> BuildParamList(TipoRel tRel, bool incCamposEstr)
        {
            List<ReportParameter> parameters = new List<ReportParameter>();

            // NOTA: os parâmetros listados seguem a ordenação das normas
            switch (tRel)
            {
                case TipoRel.InventariosCatalogosPesqDetalhada:
                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.IDNivel,
                        new string[] { "n.ID" },
                        new string[] { },
                        new string[] { "n.isDeleted = 0" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Agrupador,
                        new string[] { "agr.Agrupador" },
                        new string[] { "LEFT JOIN SFRDAgrupador agr ON agr.IDFRDBase = frd.ID" },
                        new string[] { "(agr.isDeleted IS NULL OR agr.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Autores,
                        new string[] { },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.List));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Dimensao,
                        new string[] { "sfrdds.Nota" },
                        new string[] { "LEFT JOIN SFRDDimensaoSuporte sfrdds ON sfrdds.IDFRDBase = frd.ID" },
                        new string[] { "(sfrdds.isDeleted IS NULL OR sfrdds.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.CotaDocumento,
                        new string[] { },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.List));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.UFsAssociadas,
                        new string[] { },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.List));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.HistAdministrativaBiografica,
                        new string[] {  },
                        new string[] {  },
                        new string[] {  },
                        ReportParameter.ReturnType.List));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.FonteImediataAquisicaoTransferencia,
                        new string[] { "sfrdc.FonteImediataDeAquisicao" },
                        new string[] { "LEFT JOIN SFRDContexto sfrdc ON sfrdc.IDFRDBase = frd.ID" },
                        new string[] { "(sfrdc.isDeleted IS NULL OR sfrdc.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.HistoriaArquivistica,
                        new string[] { "sfrdc.HistoriaCustodial" },
                        new string[] { "LEFT JOIN SFRDContexto sfrdc ON sfrdc.IDFRDBase = frd.ID" },
                        new string[] { "(sfrdc.isDeleted IS NULL OR sfrdc.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.TipologiaInformacional,
                        new string[] { },
                        new string[] { },
                        new string[] { "ca.IDTipoNoticiaAut = 5" },
                        ReportParameter.ReturnType.List));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Diplomas,
                        new string[] { },
                        new string[] { },
                        new string[] { "ca.IDTipoNoticiaAut = 7 AND idx.Selector IS NULL" },
                        ReportParameter.ReturnType.List));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Modelos,
                        new string[] { },
                        new string[] { },
                        new string[] { "ca.IDTipoNoticiaAut = 8 AND idx.Selector IS NULL" },
                        ReportParameter.ReturnType.List));

                    if (incCamposEstr)
                    {
                        parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_RequerentesIniciais,
                            new string[] { },
                            new string[] { },
                            new string[] { },
                            ReportParameter.ReturnType.List));

                        parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_RequerentesAverbamentos,
                            new string[] { },
                            new string[] { },
                            new string[] { },
                            ReportParameter.ReturnType.List));

                        parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_DesignacaoNumPoliciaAct,
                            new string[] { },
                            new string[] { },
                            new string[] { },
                            ReportParameter.ReturnType.List));

                        parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_DesignacaoNumPoliciaAntigo,
                            new string[] { },
                            new string[] { },
                            new string[] { },
                            ReportParameter.ReturnType.List));

                        parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_TipoObra,
                            new string[] { "lo.TipoObra" },
                            new string[] { "LEFT JOIN LicencaObra lo ON lo.IDFRDBase = frd.ID AND lo.isDeleted = 0" },
                            new string[] { },
                            ReportParameter.ReturnType.TextOnly));

                        parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_PropHorizontal,
                            new string[] { "lo.PropriedadeHorizontal" },
                            new string[] { "LEFT JOIN LicencaObra lo ON lo.IDFRDBase = frd.ID AND lo.isDeleted = 0" },
                            new string[] { },
                            ReportParameter.ReturnType.TextOnly));

                        parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_TecnicoObra,
                            new string[] { },
                            new string[] { },
                            new string[] { },
                            ReportParameter.ReturnType.List));

                        parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_AtestHabit,
                            new string[] { },
                            new string[] { },
                            new string[] { },
                            ReportParameter.ReturnType.List));

                        parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_DataLicConst,
                            new string[] { },
                            new string[] { },
                            new string[] { },
                            ReportParameter.ReturnType.List));
                    }

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.ConteudoInformacional,
                        new string[] { "ce.ConteudoInformacional" },
                        new string[] { "LEFT JOIN SFRDConteudoEEstrutura ce ON ce.IDFRDBase = frd.ID" },
                        new string[] { "(ce.isDeleted IS NULL OR ce.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.DiplomaLegal,
                        new string[] { },
                        new string[] { },
                        new string[] { "ca.IDTipoNoticiaAut = 7 AND idx.Selector = 1" },
                        ReportParameter.ReturnType.List));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.RefTab,
                        new string[] { "sfrda.RefTabelaAvaliacao" },
                        new string[] { "LEFT JOIN SFRDAvaliacao sfrda ON sfrda.IDFRDBase = frd.ID" },
                        new string[] { "(sfrda.isDeleted IS NULL OR sfrda.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.DestinoFinal,
                        new string[] { "CASE sfrda.Preservar WHEN 0 THEN 'Eliminação' WHEN 1 THEN 'Conservação' ELSE '' END 'Preservar'" },
                        new string[] { "LEFT JOIN SFRDAvaliacao sfrda ON sfrda.IDFRDBase = frd.ID" },
                        new string[] { "(sfrda.isDeleted IS NULL OR sfrda.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Prazo,
                        new string[] { "sfrda.PrazoConservacao" },
                        new string[] { "LEFT JOIN SFRDAvaliacao sfrda ON sfrda.IDFRDBase = frd.ID" },
                        new string[] { "(sfrda.isDeleted IS NULL OR sfrda.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Publicado,
                        new string[] { "CASE sfrda.Publicar WHEN 1 THEN 'Sim' ELSE 'Não' END 'Publicado'" },
                        new string[] { "LEFT JOIN SFRDAvaliacao sfrda ON sfrda.IDFRDBase = frd.ID" },
                        new string[] { "(sfrda.isDeleted IS NULL OR sfrda.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.ObservacoesEnquadramentoLegal,
                        new string[] { "sfrda.Observacoes" },
                        new string[] { "LEFT JOIN SFRDAvaliacao sfrda ON sfrda.IDFRDBase = frd.ID" },
                        new string[] { "(sfrda.isDeleted IS NULL OR sfrda.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Incorporacoes,
                        new string[] { "ce.Incorporacao" },
                        new string[] { "LEFT JOIN SFRDConteudoEEstrutura ce ON ce.IDFRDBase = frd.ID" },
                        new string[] { "(ce.isDeleted IS NULL OR ce.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.TradicaoDocumental,
                        new string[] { },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.List));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Ordenacao,
                        new string[] { },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.List));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.ObjectosDigitais,
                        new string[] { },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.List));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.CondicoesAcesso,
                        new string[] { "sfrdcda.CondicaoDeAcesso" },
                        new string[] { "LEFT JOIN SFRDCondicaoDeAcesso sfrdcda ON sfrdcda.IDFRDBase = frd.ID" },
                        new string[] { "(sfrdcda.isDeleted IS NULL OR sfrdcda.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.CondicoesReproducao,
                        new string[] { "sfrdcda.CondicaoDeReproducao" },
                        new string[] { "LEFT JOIN SFRDCondicaoDeAcesso sfrdcda ON sfrdcda.IDFRDBase = frd.ID" },
                        new string[] { "(sfrdcda.isDeleted IS NULL OR sfrdcda.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Lingua,
                        new string[] { },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.List));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Alfabeto,
                        new string[] { },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.List));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.FormaSuporteAcondicionamento,
                        new string[] { },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.List));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.MaterialSuporte,
                        new string[] { },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.List));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.TecnicaRegisto,
                        new string[] { },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.List));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.EstadoConservacao,
                        new string[] { "tedc.Designacao" },
                        new string[] { "LEFT JOIN SFRDCondicaoDeAcesso sfrdcda ON sfrdcda.IDFRDBase = frd.ID", 
	                            "LEFT JOIN SFRDEstadoDeConservacao sfrdedc ON sfrdedc.IDFRDBase = sfrdcda.IDFRDBase", 
	                            "LEFT JOIN TipoEstadoDeConservacao tedc ON tedc.ID = sfrdedc.IDTipoEstadoDeConservacao" },
                        new string[] { "(sfrdcda.isDeleted IS NULL OR sfrdcda.isDeleted = 0)", 
	                            "(sfrdedc.isDeleted IS NULL OR sfrdedc.isDeleted = 0)", 
	                            "(tedc.isDeleted IS NULL OR tedc.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.InstrumentosPesquisa,
                        new string[] { "sfrdcda.AuxiliarDePesquisa" },
                        new string[] { "LEFT JOIN SFRDCondicaoDeAcesso sfrdcda ON sfrdcda.IDFRDBase = frd.ID" },
                        new string[] { "(sfrdcda.isDeleted IS NULL OR sfrdcda.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.ExistenciaLocalizacaoOriginais,
                        new string[] { "sfrdda.ExistenciaDeOriginais" },
                        new string[] { "LEFT JOIN SFRDDocumentacaoAssociada sfrdda ON sfrdda.IDFRDBase = frd.ID" },
                        new string[] { "(sfrdda.isDeleted IS NULL OR sfrdda.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.ExistenciaLocalizacaoCopias,
                        new string[] { "sfrdda.ExistenciaDeCopias" },
                        new string[] { "LEFT JOIN SFRDDocumentacaoAssociada sfrdda ON sfrdda.IDFRDBase = frd.ID" },
                        new string[] { "(sfrdda.isDeleted IS NULL OR sfrdda.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.UnidadesDescricaoAssociadas,
                        new string[] { "sfrdda.UnidadesRelacionadas" },
                        new string[] { "LEFT JOIN SFRDDocumentacaoAssociada sfrdda ON sfrdda.IDFRDBase = frd.ID" },
                        new string[] { "(sfrdda.isDeleted IS NULL OR sfrdda.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.NotaPublicacao,
                        new string[] { "sfrdda.NotaDePublicacao" },
                        new string[] { "LEFT JOIN SFRDDocumentacaoAssociada sfrdda ON sfrdda.IDFRDBase = frd.ID" },
                        new string[] { "(sfrdda.isDeleted IS NULL OR sfrdda.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Notas,
                        new string[] { "sfrdng.NotaGeral" },
                        new string[] { "LEFT JOIN SFRDNotaGeral sfrdng ON sfrdng.IDFRDBase = frd.ID" },
                        new string[] { "(sfrdng.isDeleted IS NULL OR sfrdng.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.NotaArquivista,
                        new string[] { "frd.NotaDoArquivista" },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.RegrasConvenções,
                        new string[] { "RegrasOuConvencoes" },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelInvCatPesqDet(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Indexacao,
                        new string[] { },
                        new string[] { },
                        new string[] { "(ca.IDTipoNoticiaAut = 1 OR ca.IDTipoNoticiaAut = 2 OR ca.IDTipoNoticiaAut = 3)" },
                        ReportParameter.ReturnType.List));
                    break;
                case TipoRel.UnidadesFisicas:
                    parameters.Add(new ReportParameterRelPesqUF(ReportParameterRelPesqUF.CamposRelPesqUF.GuiaIncorporacao,
                        new string[] { "nuf.GuiaIncorporacao" },
                        new string[] { "LEFT JOIN NivelUnidadeFisica nuf ON nuf.ID = nd.ID" },
                        new string[] { "(nuf.isDeleted IS NULL OR nuf.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelPesqUF(ReportParameterRelPesqUF.CamposRelPesqUF.CotaCodigoBarras,
                        new string[] { "c.Cota", "nuf.CodigoBarras" },
                        new string[] { 
                            "LEFT JOIN NivelUnidadeFisica nuf ON nuf.ID = nd.ID", 
                            "LEFT JOIN SFRDUFCota c ON c.IDFRDBase = frd.ID" },
                        new string[] { 
                            "(nuf.isDeleted IS NULL OR nuf.isDeleted = 0)", 
                            "(c.isDeleted IS NULL OR c.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelPesqUF(ReportParameterRelPesqUF.CamposRelPesqUF.DatasProducao,
                        new string[] { "dp.InicioAno", "dp.InicioMes", "dp.InicioDia", "dp.InicioAtribuida", "dp.FimAno", "dp.FimMes", "dp.FimDia", "dp.FimAtribuida" },
                        new string[] { "LEFT JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frd.ID" },
                        new string[] { "(dp.isDeleted IS NULL OR dp.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelPesqUF(ReportParameterRelPesqUF.CamposRelPesqUF.TipoDimensoes,
                        new string[] { "ta.Designacao", "df.MedidaAltura", "df.MedidaLargura", "df.MedidaProfundidade" },
                        new string[] { 
                            "LEFT JOIN SFRDUFDescricaoFisica df ON df.IDFRDBase = frd.ID", 
                            "LEFT JOIN TipoAcondicionamento ta ON ta.ID = df.IDTipoAcondicionamento" },
                        new string[] { 
                            "(df.isDeleted IS NULL OR df.isDeleted = 0)", 
                            "(ta.isDeleted IS NULL OR ta.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelPesqUF(ReportParameterRelPesqUF.CamposRelPesqUF.UltimaAlteracao,
                        new string[] { "DataEdicao.de" },
                        new string[] { 
                            "LEFT JOIN (" +
		                        "SELECT frd.ID, MAX(ddd.DataEdicao) de " +
		                        "FROM #ReportParametersUnidadesFisicas report " +
                                    "INNER JOIN FRDBase frd ON frd.IDNivel = report.ID " +
			                        "INNER JOIN FRDBaseDataDeDescricao ddd ON ddd.IDFRDBase = frd.ID " +
		                        "WHERE frd.isDeleted = 0 " +
			                        "AND ddd.isDeleted = 0 " +
		                        "GROUP BY frd.ID " +
	                        ") DataEdicao ON DataEdicao.ID = frd.ID" },
                        new string[] { },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelPesqUF(ReportParameterRelPesqUF.CamposRelPesqUF.ConteudoInformacional,
                        new string[] { "ce.ConteudoInformacional" },
                        new string[] { "LEFT JOIN SFRDConteudoEEstrutura ce ON ce.IDFRDBase = frd.ID" },
                        new string[] { "(ce.isDeleted IS NULL OR ce.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelPesqUF(ReportParameterRelPesqUF.CamposRelPesqUF.UnidadesInformacionaisAssociadas,
                        new string[] { },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.List));
                    
                    parameters.Add(new ReportParameterRelPesqUF(ReportParameterRelPesqUF.CamposRelPesqUF.Eliminada,
                        new string[] { "nuf.Eliminado", "AutosEliminacao = LEFT(o1.list, LEN(o1.list)-1)" },
                        new string[] { "LEFT JOIN NivelUnidadeFisica nuf ON nuf.ID = nd.ID",  
                                      @"CROSS APPLY ( 
                                            SELECT CONVERT(VARCHAR(200), autos.Designacao) + '; ' AS [text()]
                                            FROM (
                                                SELECT ae.Designacao
                                                FROM SFRDUFAutoEliminacao ufae
                                                    INNER JOIN AutoEliminacao ae ON ae.ID = ufae.IDAutoEliminacao AND ae.isDeleted = 0
                                                WHERE ufae.isDeleted = 0 AND ufae.IDFRDBase = frd.ID
                                                UNION
                                                SELECT ae.Designacao
                                                FROM SFRDUnidadeFisica sfrduf
                                                    INNER JOIN FRDBase frdNvlDoc ON frdNvlDoc.ID = sfrduf.IDFRDBase AND frdNvlDoc.isDeleted = 0
	                                                INNER JOIN SFRDAvaliacao av ON av.IDFRDBase = frdNvlDoc.ID AND av.isDeleted = 0
	                                                INNER JOIN AutoEliminacao ae ON ae.ID = av.IDAutoEliminacao AND ae.isDeleted = 0
                                                WHERE sfrduf.isDeleted = 0 AND sfrduf.IDNivel = frd.IDNivel
                                            ) autos
                                            GROUP BY autos.Designacao
                                            FOR XML PATH('') 
                                        ) o1 (list)" },
                        new string[] { "(nuf.isDeleted IS NULL OR nuf.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    break;
                case TipoRel.EntidadesProdutoras:
                    // quando uma EP é criada não é atribuido o tipo de entidade produtora só quando é gravada pela 
                    // 2ª vez (a 1ª é no masterpanel e a 2ª vez é quando se des-selecciona a EP); o LEFT JOIN (em 
                    // vez do INNER JOIN) é para prever o caso de algo de errado acontecer durante o 2º save e o 
                    // tipo não ser atribuído
                    parameters.Add(new ReportParameterRelEPs(ReportParameterRelEPs.CamposRelEPs.TipoEntidadeProdutora,
                        new string[] { "tep.Designacao" },
                        new string[] { 
                            "LEFT JOIN ControloAutEntidadeProdutora caep ON caep.IDControloAut = ca.ID ",
                            "LEFT JOIN TipoEntidadeProdutora tep ON tep.ID = caep.IDTipoEntidadeProdutora " },
                        new string[] { },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelEPs(ReportParameterRelEPs.CamposRelEPs.FormaParalela,
                        new string[] { },
                        new string[] { },
                        new string[] { "cad.IDTipoControloAutForma = 2" },
                        ReportParameter.ReturnType.List));

                    parameters.Add(new ReportParameterRelEPs(ReportParameterRelEPs.CamposRelEPs.FormaNormalizada,
                        new string[] { },
                        new string[] { },
                        new string[] { "cad.IDTipoControloAutForma = 3" },
                        ReportParameter.ReturnType.List));

                    parameters.Add(new ReportParameterRelEPs(ReportParameterRelEPs.CamposRelEPs.OutrasFormas,
                        new string[] { },
                        new string[] { },
                        new string[] { "cad.IDTipoControloAutForma = 4" },
                        ReportParameter.ReturnType.List));

                    parameters.Add(new ReportParameterRelEPs(ReportParameterRelEPs.CamposRelEPs.IdentificadorUnico,
                        new string[] { "ca.ChaveColectividade" },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelEPs(ReportParameterRelEPs.CamposRelEPs.DatasExistencia,
                        new string[] { "cade.InicioAno", "cade.InicioMes", "cade.InicioDia", "cade.InicioAtribuida", "cade.FimAno", "cade.FimMes", "cade.FimDia", "cade.FimAtribuida", "cade.DescDatasExistencia" },
                        new string[] { "LEFT JOIN ControloAutDatasExistencia cade ON cade.IDControloAut = ca.ID" },
                        new string[] { "(cade.isDeleted IS NULL OR cade.isDeleted = 0)" },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelEPs(ReportParameterRelEPs.CamposRelEPs.Historia,
                        new string[] { "ca.DescHistoria" },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelEPs(ReportParameterRelEPs.CamposRelEPs.ZonaGeografica,
                        new string[] { "ca.DescZonaGeografica" },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelEPs(ReportParameterRelEPs.CamposRelEPs.EstatutoLegal,
                        new string[] { "ca.DescEstatutoLegal" },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelEPs(ReportParameterRelEPs.CamposRelEPs.FuncoesOcupacoesActividades,
                        new string[] { "ca.DescOcupacoesActividades" },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelEPs(ReportParameterRelEPs.CamposRelEPs.EnquadramentoLegal,
                        new string[] { "ca.DescEnquadramentoLegal" },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelEPs(ReportParameterRelEPs.CamposRelEPs.EstruturaInterna,
                        new string[] { "ca.DescEstruturaInterna" },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelEPs(ReportParameterRelEPs.CamposRelEPs.ContextoGeral,
                        new string[] { "ca.DescContextoGeral" },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelEPs(ReportParameterRelEPs.CamposRelEPs.OutrasInformacoesRelevantes,
                        new string[] { "ca.DescOutraInformacaoRelevante" },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelEPs(ReportParameterRelEPs.CamposRelEPs.Relacoes,
                        new string[] { },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.List));

                    parameters.Add(new ReportParameterRelEPs(ReportParameterRelEPs.CamposRelEPs.RegrasConvencoes,
                        new string[] { "ca.RegrasConvencoes" },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelEPs(ReportParameterRelEPs.CamposRelEPs.Validado,
                        new string[] { "CASE ca.Autorizado WHEN 1 THEN 'Sim' ELSE 'Não' END 'Autorizado'" },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelEPs(ReportParameterRelEPs.CamposRelEPs.Completo,
                        new string[] { "CASE ca.Completo WHEN 1 THEN 'Sim' ELSE 'Não' END 'Completo'" },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelEPs(ReportParameterRelEPs.CamposRelEPs.LinguaAlfabeto,
                        new string[] {
                            "Iso639.LanguageNameEnglish",
                            "Iso15924.ScriptNameEnglish" },
                        new string[] { 
                            "LEFT JOIN Iso639 ON Iso639.ID = IDIso639p2", 
                            "LEFT JOIN Iso15924 ON Iso15924.ID = ca.IDIso15924" },
                        new string[] { 
                            "(Iso639.isDeleted IS NULL OR Iso639.isDeleted = 0)",
                            "(Iso15924.isDeleted IS NULL OR Iso15924.isDeleted = 0)"},
                        ReportParameter.ReturnType.TextOnly));

                    parameters.Add(new ReportParameterRelEPs(ReportParameterRelEPs.CamposRelEPs.FontesObservacoes,
                        new string[] { "ca.Observacoes" },
                        new string[] { },
                        new string[] { },
                        ReportParameter.ReturnType.TextOnly));
                    break;
            }

            return parameters;
        }

		#region AutoEliminacao
		public override IDataReader ReportAutoEliminacao(long IDTrustee, long IDAutoEliminacao, IDbConnection conn)	{
			Trace.WriteLine("<reportAutoEliminacao>");
			SqlCommand command = new SqlCommand("sp_reportAutoEliminacao", (SqlConnection) conn);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@IDTrustee", SqlDbType.BigInt);
			command.Parameters.Add("@IDAutoEliminacao", SqlDbType.BigInt);
			command.Parameters[0].Value = IDTrustee;
			command.Parameters[1].Value = IDAutoEliminacao;
			SqlDataReader reader = command.ExecuteReader();
			Trace.WriteLine("</reportAutoEliminacao>");
			return reader;
		}

        public override IDataReader ReportAutoEliminacaoPortaria(long IDTrustee, long IDAutoEliminacao, IDbConnection conn)
        {
            Trace.WriteLine("<reportAutoEliminacaoPortaria>");
            SqlCommand command = new SqlCommand("sp_reportAutoEliminacaoPortaria", (SqlConnection)conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@IDTrustee", SqlDbType.BigInt);
            command.Parameters.Add("@IDAutoEliminacao", SqlDbType.BigInt);
            command.Parameters[0].Value = IDTrustee;
            command.Parameters[1].Value = IDAutoEliminacao;
            SqlDataReader reader = command.ExecuteReader();
            Trace.WriteLine("</reportAutoEliminacaoPortaria>");
            return reader;
        }
		#endregion

		#region ControlAutoEliminacao
		public override void LoadAutosEliminacao(DataSet currentDataSet, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["AutoEliminacao"]);
				da.Fill(currentDataSet, "AutoEliminacao");
			}
		}

        public override List<string> GetAutoEliminacaoAssociations(long IDAutoEliminacao, IDbConnection conn)
        {
            var res = new List<string>();
            SqlCommand command = new SqlCommand("", (SqlConnection)conn);
            command.CommandText = "SELECT COUNT(*) FROM SFRDUFAutoEliminacao WHERE IDAutoEliminacao = " + IDAutoEliminacao .ToString();
            var cnt = System.Convert.ToInt64(command.ExecuteScalar());

            command.CommandText = "SELECT COUNT(*) FROM SFRDAvaliacao WHERE IDAutoEliminacao = " + IDAutoEliminacao.ToString();
            cnt += System.Convert.ToInt64(command.ExecuteScalar());

            if (cnt > 0)
            {
                command.CommandText = string.Format(@"
DECLARE @tnrUF NVARCHAR(20)
SELECT @tnrUF = Designacao FROM TipoNivelRelacionado WHERE ID = 11

SELECT @tnrUF + ': ' + nd.Designacao
FROM SFRDUFAutoEliminacao ufae
	INNER JOIN FRDBase frd on frd.ID = ufae.IDFRDBase AND frd.IDTipoFRDBase = 2 AND frd.isDeleted = 0
	INNER JOIN Nivel n ON n.ID = frd.IDNivel AND n.IDTipoNivel = 4 AND n.isDeleted = 0
	INNER JOIN NivelDesignado nd on nd.ID = frd.IDNivel AND nd.isDeleted = 0
WHERE ufae.isDeleted = 0 AND ufae.IDAutoEliminacao = {0}
UNION ALL
SELECT MIN(nvl.tnrDesignacao) + ': ' + MIN(nvl.nvlDesignacao )
FROM (
	SELECT n.ID, tnr.Designacao tnrDesignacao, nd.Designacao nvlDesignacao
	FROM SFRDAvaliacao a
		INNER JOIN FRDBase frd on frd.ID = a.IDFRDBase AND frd.IDTipoFRDBase = 1 AND frd.isDeleted = 0
		INNER JOIN Nivel n ON n.ID = frd.IDNivel AND n.isDeleted = 0
		INNER JOIN RelacaoHierarquica rh ON rh.ID = n.ID AND rh.isDeleted = 0
		INNER JOIN TipoNivelRelacionado tnr ON tnr.ID = rh.IDTipoNivelRelacionado AND tnr.isDeleted = 0
		INNER JOIN NivelDesignado nd on nd.ID = frd.IDNivel AND nd.isDeleted = 0
	WHERE a.isDeleted = 0 AND a.IDAutoEliminacao = {0}
) nvl
GROUP BY nvl.ID", IDAutoEliminacao);
                var reader = command.ExecuteReader();
                while (reader.Read())
                    res.Add(reader.GetString(0));
                reader.Close();
            }

            return res;
        }
		#endregion

        #region ControlLocalConsulta
        public override void LoadLocaisConsulta(DataSet currentDataSet, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["LocalConsulta"]);
                da.Fill(currentDataSet, "LocalConsulta");
            }
        }

        public override List<string> GetLocalConsultaAssociations(long IDLocalConsulta, IDbConnection conn)
        {
            var res = new List<string>();
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            {
                command.Parameters.AddWithValue("@IDLocalConsulta", IDLocalConsulta);

                command.CommandText = "SELECT COUNT(*) FROM NivelUnidadeFisica WHERE IDLocalConsulta = @IDLocalConsulta and isDeleted = @isDeleted";
                var cnt = System.Convert.ToInt64(command.ExecuteScalar());

                if (cnt > 0)
                {
                    command.CommandText = @"
SELECT nd.Designacao
FROM NivelUnidadeFisica nuf
    INNER JOIN NivelDesignado nd ON nd.ID = nuf.ID AND nd.isDeleted = @isDeleted
WHERE nuf.IDLocalConsulta = @IDLocalConsulta AND nuf.isDeleted = @isDeleted";
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                        res.Add(reader.GetString(0));
                    reader.Close();
                }
            }

            return res;
        }
        #endregion

		#region ControloAut
		public override void InitializeControloAut(ArrayList parameters, System.Data.IDbConnection conn) 
		{
            SqlCommand command = new SqlCommand("CREATE TABLE #ReportParametersCAs (IDTipoCA BIGINT)", (SqlConnection)conn);
			command.ExecuteNonQuery();

            string com = "INSERT INTO #ReportParametersCAs VALUES({0})";
			foreach (long IDTipoCA in parameters)
			{
                command.CommandText = string.Format(com, IDTipoCA.ToString());
				command.ExecuteNonQuery();
			}
		}

		public override void FinalizeControloAut(System.Data.IDbConnection conn)
		{
			SqlCommand command;
            command = new SqlCommand("DROP TABLE #ReportParametersCAs", (SqlConnection)conn);
			command.ExecuteNonQuery();
		}

        private static string relDetFormas_ep_columns = "SELECT ca.ID, d.Termo ";
        private static string relDetFormas_ep_joins =
                    "INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = ca.ID " +
                    "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario " +
                    "INNER JOIN #ReportParametersCAs ON #ReportParametersCAs.IDTipoCA = ca.IDTipoNoticiaAut ";
        private static string relDetFormas_ep_wheres =
                    "WHERE ca.isDeleted = 0 " +
                        "AND cad.isDeleted = 0 " +
                        "AND d.isDeleted = 0 ";

        private static string relBase_ep_columns = relDetFormas_ep_columns + ", n.Codigo ";
        private static string relBase_ep_from = "FROM ControloAut ca ";
        private static string relBase_ep_joins =
                    relDetFormas_ep_joins +
                    "INNER JOIN NivelControloAut nca ON nca.IDControloAut = ca.ID " +
			        "INNER JOIN Nivel n ON nca.ID = n.ID ";
        private static string relBase_ep_wheres =
                    relDetFormas_ep_wheres +
                    "AND cad.IDTipoControloAutForma = 1 " +	                
                    "AND nca.isDeleted = 0 " +
                    "AND n.isDeleted = 0 ";
        private static string relBase_ep_order = "ORDER BY d.Termo ";
		public override IDataReader ReportControloAut(List<ReportParameter> fields, IDbConnection conn)
		{
            StringBuilder com = new StringBuilder();

            // relatório para entidades produtoras
            if (fields != null)
            {
                List<ReportParameter> uniqueResultFields = new List<ReportParameter>();
                foreach (ReportParameter rp in fields)
                {
                    if (rp.RetType == ReportParameter.ReturnType.TextOnly)
                        uniqueResultFields.Add(rp);
                }

                if (uniqueResultFields.Count > 0)
                {
                    DBAbstractDataLayer.DataAccessRules.RelatorioRule.BuildReportQuery(uniqueResultFields, new List<string> { ", ", " ", " AND " });

                    if (DBAbstractDataLayer.DataAccessRules.RelatorioRule.mWhereClause != string.Empty) 
                        com.Append(
                            relBase_ep_columns + ", " + DBAbstractDataLayer.DataAccessRules.RelatorioRule.mSelectClause + " " +
                            relBase_ep_from +
                            relBase_ep_joins + " " + DBAbstractDataLayer.DataAccessRules.RelatorioRule.mJoinClause + " " +
                            relBase_ep_wheres + (DBAbstractDataLayer.DataAccessRules.RelatorioRule.mWhereClause.Length > 0 ? " AND " + DBAbstractDataLayer.DataAccessRules.RelatorioRule.mWhereClause : "") + " " +
                            relBase_ep_order + "; ");
                    else
                        com.Append(
                            relBase_ep_columns + ", " + DBAbstractDataLayer.DataAccessRules.RelatorioRule.mSelectClause + " " +
                            relBase_ep_from +
                            relBase_ep_joins + " " + DBAbstractDataLayer.DataAccessRules.RelatorioRule.mJoinClause + " " +
                            relBase_ep_wheres + 
                            relBase_ep_order + "; ");

                }
                else
                    com.Append(relBase_ep_columns + " " + relBase_ep_from + relBase_ep_joins + relBase_ep_wheres + relBase_ep_order + "; ");

                List<ReportParameter> formasFields = new List<ReportParameter>();
                foreach (ReportParameterRelEPs rp in fields)
                {
                    if (rp.Campo == ReportParameterRelEPs.CamposRelEPs.FormaParalela || rp.Campo == ReportParameterRelEPs.CamposRelEPs.FormaNormalizada || rp.Campo == ReportParameterRelEPs.CamposRelEPs.OutrasFormas)
                        formasFields.Add(rp);
                }

                if (formasFields.Count > 0)
                {
                    DBAbstractDataLayer.DataAccessRules.RelatorioRule.BuildReportQuery(formasFields, new List<string> { "", "", " OR " });
                    com.Append(
                        relDetFormas_ep_columns + ", cad.IDTipoControloAutForma " +
                        relBase_ep_from +
                        relDetFormas_ep_joins +
                        relDetFormas_ep_wheres + " AND " + DBAbstractDataLayer.DataAccessRules.RelatorioRule.mWhereClause + "; ");
                }

                List<ReportParameter> relacoesFields = new List<ReportParameter>();
                foreach (ReportParameterRelEPs rp in fields)
                {
                    if (rp.Campo == ReportParameterRelEPs.CamposRelEPs.Relacoes)
                        relacoesFields.Add(rp);
                }

                if (relacoesFields.Count > 0)
                {
                    string query1 =
                        "SELECT ca.ID, d.Termo, caRelacionado.ChaveColectividade, {0}, car.InicioAno, car.InicioMes, car.InicioDia, car.FimAno, car.FimMes, car.FimDia, car.Descricao " +
                        "FROM ControloAut ca " +
                            "INNER JOIN ControloAutRel car ON {1} = ca.ID " +
                            "INNER JOIN TipoControloAutRel tcar ON tcar.ID = car.IDTipoRel " +
                            "INNER JOIN ControloAut caRelacionado ON caRelacionado.ID = {1} " +
                            "INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = caRelacionado.ID " +
                            "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario " +
                        "WHERE tcar.ID IN (4,5,6) " +
                            "AND cad.IDTipoControloAutForma = 1 " +
                            "AND ca.isDeleted = 0 " +
                            "AND car.isDeleted = 0 " +
                            "AND caRelacionado.isDeleted = 0 " +
                            "AND cad.isDeleted = 0 " +
                            "AND d.isDeleted = 0 ";

                    string query2 =
                        "SELECT ca.ID, dRelacionado.Termo, caRelacionado.ChaveColectividade, {0}, rh.InicioAno, rh.InicioMes, rh.InicioDia, rh.FimAno, rh.FimMes, rh.FimDia, rh.Descricao " +
                        "FROM ControloAut ca " +
                            "INNER JOIN NivelControloAut nca ON nca.IDControloAut = ca.ID " +
                            "INNER JOIN RelacaoHierarquica rh ON {1} = nca.ID " +
                            "INNER JOIN Nivel nRelacionado ON nRelacionado.ID = {2} AND nRelacionado.IDTipoNivel > 1 " +
                            "INNER JOIN NivelControloAut ncaRelacionado ON ncaRelacionado.ID = nRelacionado.ID " +
                            "INNER JOIN ControloAut caRelacionado ON caRelacionado.ID = ncaRelacionado.IDControloAut " +
                            "INNER JOIN ControloAutDicionario cadRelacionado ON cadRelacionado.IDControloAut = caRelacionado.ID " +
                            "INNER JOIN Dicionario dRelacionado ON dRelacionado.ID = cadRelacionado.IDDicionario " +
                        "WHERE cadRelacionado.IDTipoControloAutForma = 1 " +
                            "AND ca.isDeleted = 0 " +
                            "AND nca.isDeleted = 0 " +
                            "AND rh.isDeleted = 0 " +
                            "AND nRelacionado.isDeleted = 0 " +
                            "AND ncaRelacionado.isDeleted = 0 " +
                            "AND caRelacionado.isDeleted = 0 " +
                            "AND cadRelacionado.isDeleted = 0 " +
                            "AND dRelacionado.isDeleted = 0 ";

                    com.Append(
                        string.Format(query1, "tcar.Designacao", "car.IDControloAut") +
                        "UNION " +
                        string.Format(query1, "tcar.DesignacaoInversa Designacao", "car.IDControloAutAlias") +
                        "UNION " +
                        string.Format(query2, "'Hierárquica superior' Designacao", "rh.ID", "rh.IDUpper") +
                        "UNION " +
                        string.Format(query2, "'Hierárquica subordinada' Designacao", "rh.IDUpper", "rh.ID"));
                }
            }
            else
            {
                com.Append(
                    "SELECT ca.ID, tna.ID, tna.Designacao, d.Termo " +
                    "FROM ControloAutDicionario cad " +
                        "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario " +                        
                        "INNER JOIN ControloAut ca ON ca.ID = cad.IDControloAut " +
                        "INNER JOIN TipoNoticiaAut tna ON tna.ID = ca.IDTipoNoticiaAut " +
                        "INNER JOIN #ReportParametersCAs prca on prca.IDTipoCA = ca.IDTipoNoticiaAut " +
                    "WHERE cad.IDTipoControloAutForma = 1 AND cad.isDeleted=0 AND tna.isDeleted=0 AND d.isDeleted=0 AND ca.isDeleted = 0 " +
                    "ORDER BY tna.ID, d.Termo");
            }            
            

			SqlCommand command = new SqlCommand(string.Empty, (SqlConnection) conn);
            command.CommandText = com.ToString();
			SqlDataReader reader = command.ExecuteReader();

            return reader;
		}
		#endregion

		#region " FormAutoEliminacaoPicker "
		public override ArrayList LoadDataFormAutoEliminacaoPicker(bool excludeEmptyAutos, IDbConnection conn)
		{
			ArrayList result = new ArrayList();
			SqlCommand command = new SqlCommand("sp_getAutosEliminacao", (SqlConnection) conn);			
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@excludeEmptyAutos", SqlDbType.Bit);
			command.Parameters[0].Value = excludeEmptyAutos;
			SqlDataReader da = command.ExecuteReader();
			while (da.Read()) 
			{
				result.Add(new object[] {da.GetValue(0), da.GetValue(1), da.GetValue(2), da.GetValue(3)});
			}
			da.Close();
			return result;
		}

		public override void LoadAutoEliminacao(DataSet currentDataSet, long aeID, IDbConnection conn) {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@aeID", aeID);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["AutoEliminacao"],
                    "WHERE ID=@aeID");
				da.Fill(currentDataSet, "AutoEliminacao");
			}
		}

		public override void LoadAutosEliminacao(DataSet currentDataSet, ArrayList aeIDs, IDbConnection conn) {
			if (aeIDs.Count == 0){
				return;
			}

			System.Text.StringBuilder queryFilter = new System.Text.StringBuilder();;
			foreach (long aeID in aeIDs){
				if (queryFilter.Length != 0){
					queryFilter.Append(" OR ");
				}
				queryFilter.AppendFormat("ID={0}", aeID);
			}
			queryFilter.Insert(0, "WHERE ");

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
				da.SelectCommand.CommandText =
					SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["AutoEliminacao"], queryFilter.ToString());
				da.Fill(currentDataSet, "AutoEliminacao");
			}
		}
		#endregion

		#region " Inventario "
        public override void InitializeInventario(long ? IDTopoNivel, IDbConnection conn)
        {
			StringBuilder paramsQuery = new StringBuilder();
			SqlCommand command;
			command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = "CREATE TABLE #Niveis(ID BIGINT, IDUpper BIGINT, IsContext BIT DEFAULT 0) " +
                        "CREATE INDEX id_ix ON #Niveis (ID) " +
                        "CREATE TABLE #ReportParametersNiveis (ID BIGINT, seq_id BIGINT, PRIMARY KEY(ID))";
			command.ExecuteNonQuery();

            if (IDTopoNivel == null)
			{
                GisaDataSetHelperRule.ImportIDs(PesquisaRule.Current.CacheSearchResult.ToArray(), conn);
                command.CommandText =
                   "INSERT INTO #ReportParametersNiveis " +
                   "SELECT ID, NULL " +
                   "FROM #temp ";
				command.CommandType = CommandType.Text;
				command.ExecuteNonQuery();
			}
			else
			{
			    command.CommandText = "sp_reportParameterAddNivel";
			    command.CommandType = CommandType.StoredProcedure;
			    SqlParameter param = command.Parameters.Add("@IDNivel", SqlDbType.BigInt);
			    param.Value = IDTopoNivel;
				command.ExecuteNonQuery();
			}			
		}

		public override void FinalizeInventario(System.Data.IDbConnection conn){
			string query = "DROP TABLE #Niveis ";
			SqlCommand command = new SqlCommand(query, (SqlConnection) conn);
			command.CommandType = CommandType.Text;
			command.ExecuteNonQuery();

            PermissoesRule.Current.DropEffectivePermissionsTempTable(conn);
        }

        #region Query base para os relatórios Inventário, Catálogo
        private static string relBase_columns = "SELECT n.ID, Niveis.IDUpper, n.Codigo, n.IDTipoNivel, Niveis.Designacao, nd.Designacao, dp.InicioTexto, dp.InicioAno, dp.InicioMes, dp.InicioDia, dp.InicioAtribuida, dp.FimAno, dp.FimMes, dp.FimDia, dp.FimAtribuida";
        private static string relBase_from = "FROM Nivel n ";
        private static string relBase_joins =
                    "INNER JOIN ( " +
                        "SELECT DISTINCT rh.ID, rh.IDUpper, tnr.Designacao, rh.IDTipoNivelRelacionado, rpn.IsContext " +
                        "FROM #Niveis rpn " +
                            "INNER JOIN RelacaoHierarquica rh ON rh.ID = rpn.ID " +
                            "INNER JOIN TipoNivelRelacionado tnr ON tnr.ID = rh.IDTipoNivelRelacionado " +
                        "WHERE rh.isDeleted = 0 AND rh.IDTipoNivelRelacionado >= 7 " +
                    ") Niveis ON Niveis.ID = n.ID " +
                    "INNER JOIN NivelDesignado nd on nd.ID = n.ID " +
                    "LEFT JOIN FRDBase frd ON frd.IDNivel = n.ID " +
                    "LEFT JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frd.ID ";
        private static string relBase_wheres =
                "WHERE Niveis.IsContext = {0} " +
                    "AND n.isDeleted = 0 " +
                    "AND nd.isDeleted = 0 " +
                    "AND (frd.isDeleted IS NULL OR frd.isDeleted = 0) " +
                    "AND (dp.isDeleted IS NULL OR dp.isDeleted = 0) ";
        private static string relBase_orders = "ORDER BY Niveis.IDTipoNivelRelacionado, nd.Designacao";
        #endregion

        #region Query base para detalhes dos relatórios Inventário, Catálogo
        private static string relDetalhe_columns = "SELECT Niveis.ID, ";
        private static string relDetalhe_from = "FROM FRDBase frd ";
        private static string relDetalhe_joins =
                                "INNER JOIN ( " +
                                    "SELECT DISTINCT rh.ID " +
                                    "FROM #Niveis rpn " +
                                        "INNER JOIN RelacaoHierarquica rh ON rh.ID = rpn.ID " +
                                    "WHERE rpn.IsContext = 0 AND rh.isDeleted = 0 AND rh.IDTipoNivelRelacionado >= 7 " +
                                ") Niveis ON Niveis.ID = frd.IDNivel ";
        private static string relDetalhe_where = "WHERE frd.isDeleted = 0 ";
        #endregion

        #region Query base para nível inicial dos relatórios Inventário, Catálogo
        private static string relTopNivel_columns = "SELECT n.ID, rh.IDUpper, n.Codigo, n.IDTipoNivel, '', d.Termo, dp.InicioTexto, dp.InicioAno, dp.InicioMes, dp.InicioDia, dp.InicioAtribuida, dp.FimAno, dp.FimMes, dp.FimDia, dp.FimAtribuida ";
        private static string relTopNivel_from = "FROM Nivel n ";
        private static string relTopNivel_joins =
                    "INNER JOIN RelacaoHierarquica rh ON rh.ID = n.ID " +
                    "INNER JOIN NivelControloAut nca ON nca.ID = n.ID " +
	                "INNER JOIN ControloAut ca ON ca.ID = nca.IDControloAut " +
                    "INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = ca.ID AND cad.IDTipoControloAutForma = 1 " +
	                "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario " +
                    "LEFT JOIN FRDBase frd ON frd.IDNivel = n.ID " +
                    "LEFT JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frd.ID ";
        private static string relTopNivel_wheres =
                "WHERE n.ID = {0} AND n.isDeleted = 0 AND rh.isDeleted = 0 AND nca.isDeleted = 0 AND ca.isDeleted = 0 AND cad.isDeleted = 0 AND d.isDeleted = 0 " +
                    "AND (frd.isDeleted IS NULL OR frd.isDeleted = 0) " +
                    "AND (dp.isDeleted IS NULL OR dp.isDeleted = 0) ";
        #endregion

        private ArrayList produtores;
        private Hashtable codCompletos;
        public override IDataReader ReportInventario(long IDTopoNivel, long IDTrustee, bool isCatalogo, bool isDetalhado, bool isTopDownExpansion, List<ReportParameter> fields, IDbConnection conn)
		{
            produtores = new ArrayList();
            codCompletos = new Hashtable();
            
            SqlCommand command = new SqlCommand("sp_reportInventario", (SqlConnection) conn);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@IDTrustee", SqlDbType.BigInt);
			command.Parameters.Add("@IsCatalogo", SqlDbType.Bit);
			command.Parameters.Add("@IsDetalhado", SqlDbType.Bit);
			command.Parameters.Add("@TopDownExpansion", SqlDbType.Bit);
			command.Parameters[0].Value = System.Convert.ToInt64(IDTrustee);
			command.Parameters[1].Value = System.Convert.ToSByte(isCatalogo);
			command.Parameters[2].Value = System.Convert.ToSByte(isDetalhado);
			command.Parameters[3].Value = System.Convert.ToSByte(isTopDownExpansion);
			SqlDataReader reader = null;
			try 
			{
				reader = command.ExecuteReader();
                GetRelInfo(reader);
                reader.Close();

                PermissoesRule.Current.GetEffectivePermissions(" FROM #Niveis ", IDTrustee, conn);

                StringBuilder com = new StringBuilder();
                command = new SqlCommand(string.Empty, (SqlConnection)conn);
                command.CommandType = CommandType.Text;
                if (isDetalhado)
                {
                    StringBuilder nullColumns = new StringBuilder();
                    List<ReportParameter> uniqueResultFields = new List<ReportParameter>();
                    foreach (ReportParameter rp in fields)
                    {
                        if (rp.RetType == ReportParameter.ReturnType.TextOnly)
                        {
                            if (nullColumns.Length > 0)
                                nullColumns.Append(", null");
                            else
                                nullColumns.Append("null");
                            uniqueResultFields.Add(rp);
                        }
                    }

                    // As duas queries adicionadas no if ou no else são quase iguais excepto num parâmetro. Esse parâmetro serve para
                    // indicar se um dado nível impresso no relatório serve como contexto (o valor do parâmetro é igual a 1) para 
                    // outros ou se é para mostrar toda a informação que lhe diz respeito (o valor do parâmetro é igual a 0). Este 
                    // parâmetro é relevante quando o nível usado como ponto de partida é uma subsérie e dessa forma expande-se a 
                    // árvore para cima de forma os seus níveis superiores serem usados somente como contexto
                    if (uniqueResultFields.Count > 0)
                    {
                        DBAbstractDataLayer.DataAccessRules.RelatorioRule.BuildReportQuery(uniqueResultFields, new List<string> { ", ", " ", " AND " });

                        // top nivel
                        com.Append(
                            relTopNivel_columns + ", " + DBAbstractDataLayer.DataAccessRules.RelatorioRule.mSelectClause + " " +
                            relTopNivel_from +
                            string.Format(relTopNivel_joins, IDTrustee.ToString()) + " " + DBAbstractDataLayer.DataAccessRules.RelatorioRule.mJoinClause + " " +
                            string.Format(relTopNivel_wheres, IDTopoNivel) + " AND " + DBAbstractDataLayer.DataAccessRules.RelatorioRule.mWhereClause + " " +
                            "; ");

                        // niveis de contexto                        
                        com.Append(
                            relBase_columns + ", " + nullColumns.ToString() + " " +
                            relBase_from +
                            string.Format(relBase_joins, IDTrustee.ToString()) + " " +
                            string.Format(relBase_wheres, "1") + " " +
                            relBase_orders + "; ");

                        // niveis com detalhe
                        com.Append(
                            relBase_columns + ", " + DBAbstractDataLayer.DataAccessRules.RelatorioRule.mSelectClause + " " +
                            relBase_from +
                            string.Format(relBase_joins, IDTrustee.ToString()) + " " + DBAbstractDataLayer.DataAccessRules.RelatorioRule.mJoinClause + " " +
                            string.Format(relBase_wheres, "0") + " AND " + DBAbstractDataLayer.DataAccessRules.RelatorioRule.mWhereClause + " " +
                            relBase_orders + "; ");
                    }
                    else
                    {
                        // top nivel
                        com.Append(relTopNivel_columns + " " + relTopNivel_from + string.Format(relTopNivel_joins, IDTrustee.ToString()) + " " + string.Format(relTopNivel_wheres, IDTopoNivel) + "; ");
                        // apesar de o nível de detalhe ser idêntico para os dois tipos de nível, é preciso executar as 2 queries na mesma
                        // niveis de contexto
                        com.Append(relBase_columns + " " + relBase_from + string.Format(relBase_joins, IDTrustee.ToString()) + " " + string.Format(relBase_wheres, "1") + relBase_orders + "; ");
                        // niveis com detalhe
                        com.Append(relBase_columns + " " + relBase_from + string.Format(relBase_joins, IDTrustee.ToString()) + " " + string.Format(relBase_wheres, "0") + relBase_orders + "; ");
                    }

                    com.Append(BuildDetailsQuery(fields, IDTrustee));

                    command.CommandText = com.ToString();
                    reader = command.ExecuteReader();
                }
                else
                {
                    // top nivel
                    com.Append(relTopNivel_columns + " " + relTopNivel_from + string.Format(relTopNivel_joins, IDTrustee.ToString()) + " " + string.Format(relTopNivel_wheres, IDTopoNivel) + "; ");
                    // apesar de o nível de detalhe ser idêntico para os dois tipos de nível, é preciso executar as 2 queries na mesma
                    // niveis de contexto
                    com.Append(relBase_columns + " " + relBase_from + string.Format(relBase_joins, IDTrustee.ToString()) + " " + string.Format(relBase_wheres, "1") + relBase_orders + "; ");
                    // niveis com detalhe
                    com.Append(relBase_columns + " " + relBase_from + string.Format(relBase_joins, IDTrustee.ToString()) + " " + string.Format(relBase_wheres, "0") + relBase_orders + "; ");
                    command.CommandText = com.ToString();
                    reader = command.ExecuteReader();
                }
			}
			catch (Exception e)
			{
                Trace.WriteLine("ReportInventario: " + e);
                throw;
			}

            return reader;
		}

        public override ArrayList GetProdutores()
        {
            return produtores;
        }

        public override Hashtable GetCodCompletos()
        {
            return codCompletos;
        }
		#endregion

		#region UnidadeFisica

        #region query base para os relatórios de unidades físicas
        private static string relBase_uf_columns = "SELECT n.ID, rh.IDUpper, nUpper.Codigo  + '/' + n.Codigo, nd.Designacao";
        private static string relBase_uf_from = "FROM Nivel n ";
        private static string relBase_uf_joins =
                    "INNER JOIN #ReportParametersUnidadesFisicas ON #ReportParametersUnidadesFisicas.ID = n.ID " +
                    "INNER JOIN RelacaoHierarquica rh ON rh.ID = n.ID " +
	                "INNER JOIN Nivel nUpper ON nUpper.ID = rh.IDUpper " +
	                "INNER JOIN NivelDesignado nd ON nd.ID = n.ID " +
                    "INNER JOIN FRDBase frd ON frd.IDNivel = n.ID ";
        private static string relBase_uf_wheres =
                "WHERE n.isDeleted = 0 " +
	                "AND nUpper.isDeleted = 0 " +
	                "AND nd.isDeleted = 0 " +
                    "AND frd.isDeleted = 0 ";
        private static string relBase_uf_order =
            "ORDER BY #ReportParametersUnidadesFisicas.seq_id ";
        #endregion

		public override void InitializeListaUnidadesFisicas(ArrayList parameters, System.Data.IDbConnection conn){			
			SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
			command.CommandText = "CREATE TABLE #ReportParametersUnidadesFisicas (ID BIGINT, seq_id BIGINT, PRIMARY KEY(ID));";
			command.ExecuteNonQuery();

			if (parameters == null)
			{
                GisaDataSetHelperRule.ImportIDs(PesquisaRule.Current.CacheSearchResult.ToArray(), conn);
                command.CommandText = 
                    "INSERT INTO #ReportParametersUnidadesFisicas " +
                    "SELECT ID, seq_nr " +
                    "FROM #temp";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
			}
			else
			{
                string com = "INSERT INTO #ReportParametersUnidadesFisicas VALUES ({0}, {1})";
				command.CommandType = CommandType.Text;				
				foreach (long IDNivel in parameters)
				{
                    command.CommandText = string.Format(com, IDNivel.ToString(), parameters.IndexOf(IDNivel).ToString());
					command.ExecuteNonQuery();
				}
			}
		}

		public override void FinalizeListaUnidadesFisicas(IDbConnection conn){
			string query = 
				"DROP TABLE #ReportParametersUnidadesFisicas;";
			SqlCommand command = new SqlCommand(query, (SqlConnection) conn);
			command.CommandType = CommandType.Text;
			command.ExecuteNonQuery();
		}

        public override IDataReader ReportUnidadesFisicas(long IDTrustee, List<ReportParameter> fields, IDbConnection conn)
        {
			Trace.WriteLine("<reportUnidadesFisicas>");
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection) conn);
            command.CommandType = CommandType.Text;

            // garatir que os parâmetros são válidos
            command.CommandText = 
                "DELETE FROM #ReportParametersUnidadesFisicas " +
			    "FROM #ReportParametersUnidadesFisicas " +
				    "INNER JOIN Nivel n ON n.ID =  #ReportParametersUnidadesFisicas.ID " +
			    "WHERE n.IDTipoNivel != 4";
            command.ExecuteNonQuery();

            StringBuilder com = new StringBuilder();
            List<ReportParameter> uniqueResultFields = new List<ReportParameter>();
            foreach (ReportParameter rp in fields)
            {
                if (rp.RetType == ReportParameter.ReturnType.TextOnly)
                    uniqueResultFields.Add(rp);
            }

            if (uniqueResultFields.Count > 0)
            {
                DBAbstractDataLayer.DataAccessRules.RelatorioRule.BuildReportQuery(uniqueResultFields, new List<string> { ", ", " ", " AND " });
                com.Append(
                    relBase_uf_columns + ", " + DBAbstractDataLayer.DataAccessRules.RelatorioRule.mSelectClause + " " +
                    relBase_uf_from +
                    relBase_uf_joins + " " + DBAbstractDataLayer.DataAccessRules.RelatorioRule.mJoinClause + " " +
                    relBase_uf_wheres + (DBAbstractDataLayer.DataAccessRules.RelatorioRule.mWhereClause.Length > 0 ? " AND " + DBAbstractDataLayer.DataAccessRules.RelatorioRule.mWhereClause : "")+ " " +
                    relBase_uf_order + "; ");
            }
            else
                com.Append(relBase_uf_columns + " " + relBase_uf_from + relBase_uf_joins + relBase_uf_wheres + relBase_uf_order + "; ");

            List<ReportParameter> UAFields = new List<ReportParameter>();
            foreach (ReportParameterRelPesqUF rp in fields)
            {
                if (rp.Campo == ReportParameterRelPesqUF.CamposRelPesqUF.UnidadesInformacionaisAssociadas)
                    UAFields.Add(rp);
            }

            if (UAFields.Count > 0)
            {
                com.AppendFormat(
                    // TODO: permissoes
                    "SELECT ufs.ID, nd.Designacao, tnr.Designacao, min(rh.IDTipoNivelRelacionado) IDTipoNivelRelacionado, nUA.ID " +
                    "FROM #ReportParametersUnidadesFisicas ufs " +
                        "INNER JOIN SFRDUnidadeFisica sfrduf ON sfrduf.IDNivel = ufs.ID " +
                        "INNER JOIN FRDBase frd ON frd.ID = sfrduf.IDFRDBase " +
                        "INNER JOIN Nivel nUA ON nUA.ID = frd.IDNivel " +
                        "INNER JOIN RelacaoHierarquica rh ON rh.ID = nUA.ID " +
                        "INNER JOIN TipoNivelRelacionado tnr ON tnr.ID = rh.IDTipoNivelRelacionado " +
                        "INNER JOIN NivelDesignado nd ON nd.ID = nUA.ID " +
                    "WHERE sfrduf.isDeleted = 0 " +
                        "AND frd.isDeleted = 0 " +
                        "AND nUA.isDeleted = 0 " +
                        "AND rh.isDeleted = 0 " +
                        "AND nd.isDeleted = 0 " +
                    "GROUP BY ufs.ID, nUA.ID, nd.Designacao, tnr.Designacao " +
                    "UNION " +

                    /// TODO: permissoes
                    "SELECT ufs.ID, d.Termo Designacao, tnr.Designacao, min(rh.IDTipoNivelRelacionado) IDTipoNivelRelacionado, nUA.ID " +
                    "FROM #ReportParametersUnidadesFisicas ufs " +
                        "INNER JOIN SFRDUnidadeFisica sfrduf ON sfrduf.IDNivel = ufs.ID " +
                        "INNER JOIN FRDBase frd ON frd.ID = sfrduf.IDFRDBase " +
                        "INNER JOIN Nivel nUA ON nUA.ID = frd.IDNivel " +
                        "INNER JOIN RelacaoHierarquica rh ON rh.ID = nUA.ID " +
                        "INNER JOIN TipoNivelRelacionado tnr ON tnr.ID = rh.IDTipoNivelRelacionado " +
                        "INNER JOIN NivelControloAut nca ON nca.ID = nUA.ID " +
                        "INNER JOIN ControloAut ca ON ca.ID = nca.IDControloAut " +
                        "INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = ca.ID " +
                        "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario " +
                    "WHERE sfrduf.isDeleted = 0 " +
                        "AND frd.isDeleted = 0 " +
                        "AND nUA.isDeleted = 0 " +
                        "AND rh.isDeleted = 0 " +
                        "AND nca.isDeleted = 0 " +
                        "AND ca.isDeleted = 0 " +
                        "AND cad.isDeleted = 0 " +
                        "AND d.isDeleted = 0 " +
                    "GROUP BY ufs.ID, nUA.ID, d.Termo, tnr.Designacao ", IDTrustee);
            }

            command.CommandText = com.ToString();
            SqlDataReader reader = command.ExecuteReader();

            Trace.WriteLine("</reportUnidadesFisicas>");
			return reader;
		}

		public override void LoadGeneratePdfUFAgData(DataSet currentDataSet, long NivelRowID, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@NivelRowID", NivelRowID);
                string Query = "WHERE Nivel.ID=@NivelRowID";
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"], 
					Query);
				da.Fill(currentDataSet, "Nivel");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"], 
					string.Format("INNER JOIN Nivel ON Nivel.ID=FRDBase.IDNivel {0}", Query));
				da.Fill(currentDataSet, "FRDBase");

				string Query2 = string.Format("INNER JOIN FRDBase ON SFRDUnidadeFisica.IDFRDBase=FRDBase.ID INNER JOIN Nivel ON FRDBase.IDNivel=Nivel.ID {0}", Query);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDUnidadeFisica"], 
					Query2);
				da.Fill(currentDataSet, "SFRDUnidadeFisica");

				string Query3 = string.Format("(SELECT SFRDUnidadeFisica.IDNivel FROM SFRDUnidadeFisica {0})", Query2);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"], 
					string.Format("WHERE ID IN {0}", Query3));
				da.Fill(currentDataSet, "Nivel");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"], 
					string.Format("WHERE ID IN {0}", Query3));
				da.Fill(currentDataSet, "NivelDesignado");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelUnidadeFisica"], 
					string.Format("WHERE ID IN {0}", Query3));
				da.Fill(currentDataSet, "NivelUnidadeFisica");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"], 
					string.Format("WHERE IDNivel IN {0}", Query3));
				da.Fill(currentDataSet, "FRDBase");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDDatasProducao"], 
					string.Format("INNER JOIN FRDBase ON SFRDDatasProducao.IDFRDBase=FRDBase.ID WHERE IDNivel IN {0}", Query3));
				da.Fill(currentDataSet, "SFRDDatasProducao");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDCota"], 
					string.Format("INNER JOIN FRDBase ON SFRDCota.IDFRDBase=FRDBase.ID WHERE IDNivel IN {0}", Query3));
				da.Fill(currentDataSet, "SFRDCota");

				// Entidades Detentoras das UFs
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"], 
					string.Format("WHERE ID IN (SELECT IDUpper FROM RelacaoHierarquica WHERE ID IN {0})", Query3));
				da.Fill(currentDataSet, "Nivel");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"], 
					string.Format("WHERE ID IN {0}", Query3));
				da.Fill(currentDataSet, "RelacaoHierarquica");
			}
		}

		public override void LoadGeneratePdfUFnAgData(DataSet currentDataSet, long TipoNivelRelacionadoUF, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@TipoNivelRelacionadoUF", TipoNivelRelacionadoUF);
                string Query = "INNER JOIN RelacaoHierarquica rh ON rh.ID = Nivel.ID LEFT JOIN SFRDUnidadeFisica ON Nivel.ID=SFRDUnidadeFisica.IDNivel WHERE SFRDUnidadeFisica.IDNivel IS NULL AND rh.IDTipoNivelRelacionado=@TipoNivelRelacionadoUF";
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"], 
					Query);
				da.Fill(currentDataSet, "Nivel");

				string Query2 = string.Format("INNER JOIN Nivel ON NivelDesignado.ID=Nivel.ID {0}", Query);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"], 
					Query2);
				da.Fill(currentDataSet, "NivelDesignado");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"], 
					Query2.Replace("NivelDesignado.ID", "FRDBase.IDNivel"));
				da.Fill(currentDataSet, "FRDBase");

				string Query3 = string.Format("INNER JOIN FRDBase ON SFRDCota.IDFRDBase=FRDBase.ID {0}", Query2.Replace("NivelDesignado.ID", "FRDBase.IDNivel"));
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDCota"], 
					Query3);
				da.Fill(currentDataSet, "SFRDCota");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDDatasProducao"], 
					Query3.Replace("SFRDCota", "SFRDDatasProducao"));
				da.Fill(currentDataSet, "SFRDDatasProducao");

				// Entidades Detentoras das UFs
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"], 
					String.Format("WHERE ID IN (SELECT IDUpper FROM RelacaoHierarquica INNER JOIN Nivel ON RelacaoHierarquica.ID=Nivel.ID {0})", Query));
				da.Fill(currentDataSet, "Nivel");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"], 
					String.Format("WHERE ID IN (SELECT ID FROM Nivel {0})", Query));
				da.Fill(currentDataSet, "RelacaoHierarquica");
			}
		}

        public override IDataReader ReportResPesquisaResumidoUnidadesFisicas(IDbConnection conn)
        {
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = @"
SELECT rpn.ID, nEP.Codigo + '/' + n.Codigo as Codigo, nd.Designacao, dp.InicioAno, dp.InicioMes, dp.InicioDia, dp.InicioAtribuida, dp.FimAno, dp.FimMes, dp.FimDia, dp.FimAtribuida, c.Cota, nuf.GuiaIncorporacao, nuf.Eliminado, nuf.CodigoBarras
FROM #ReportParametersUnidadesFisicas rpn
    inner join Nivel n on n.ID = rpn.ID and n.isDeleted = 0
	INNER JOIN NivelDesignado nd on nd.ID = n.ID and nd.isDeleted = 0
	inner join RelacaoHierarquica rh on rh.ID = n.ID and rh.isDeleted = 0
	inner join Nivel nEP on nEP.ID = rh.IDUpper and nEP.isDeleted = 0
	inner join FRDBase frd on frd.IDNivel = n.ID and frd.isDeleted = 0
	left join SFRDDatasProducao dp on dp.IDFRDBase = frd.ID and dp.isDeleted = 0
	left join SFRDUFCota c on c.IDFRDBase = frd.ID and c.isDeleted = 0
	inner join NivelUnidadeFisica nuf on nuf.ID = n.ID and nuf.isDeleted = 0
order by rpn.seq_id";
            SqlDataReader reader = command.ExecuteReader();

            return reader;
        }
		#endregion

		#region Resultados Pesquisa

        #region Resultados Pesquisa Simples
        public override void InitializeReportResPesquisa(IDbConnection conn)
        {
            GisaDataSetHelperRule.ImportIDs(PesquisaRule.Current.CacheSearchResult.ToArray(), conn);

            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText =
                "CREATE TABLE #ReportParametersNiveis (ID BIGINT, seq_id BIGINT, PRIMARY KEY(ID))";
            command.ExecuteNonQuery();

            command.CommandText =
                "INSERT INTO #ReportParametersNiveis " +
                "SELECT ID, seq_nr " +
                "FROM #temp";
            command.ExecuteNonQuery();
        }

        public override void FinalizeReportResPesquisa(IDbConnection conn)
        {
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = "DROP TABLE #ReportParametersNiveis";
            command.ExecuteNonQuery();
        }

        public override IDataReader ReportResPesquisa(System.Data.IDbConnection conn)
        {
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = "sp_reportSearchResults";
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = command.ExecuteReader();

            return reader;
        }
        #endregion

        #region Resultados Pesquisa Detalhados
        #region Query base para obter os niveis de contexto do relatório de pesquisa detalhado
        private static string contextRelBase_columns = "SELECT n.ID, Niveis.IDUpper, n.Codigo, n.IDTipoNivel, Niveis.Designacao, nd.Designacao, dp.InicioTexto, dp.InicioAno, dp.InicioMes, dp.InicioDia, dp.InicioAtribuida, dp.FimAno, dp.FimMes, dp.FimDia, dp.FimAtribuida";
        private static string contextRelBase_from = "FROM Nivel n ";
        private static string contextRelBase_joins =
                    "INNER JOIN ( " +
                        "SELECT DISTINCT rh.ID, rh.IDUpper, tnr.Designacao, rh.IDTipoNivelRelacionado, rpn.IsContext " +
                        "FROM #Niveis rpn " +
                            "INNER JOIN RelacaoHierarquica rh ON rh.ID = rpn.ID " +
                            "INNER JOIN TipoNivelRelacionado tnr ON tnr.ID = rh.IDTipoNivelRelacionado " +
                        "WHERE rh.isDeleted = 0 AND rh.IDTipoNivelRelacionado >= 7 " +
                    ") Niveis ON Niveis.ID = n.ID " +
                    "INNER JOIN NivelDesignado nd on nd.ID = n.ID " +
                    "LEFT JOIN FRDBase frd ON frd.IDNivel = n.ID " +
                    "LEFT JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frd.ID ";
        private static string contextRelBase_wheres =
                "WHERE Niveis.IsContext = 1 " +
                    "AND n.isDeleted = 0 " +
                    "AND nd.isDeleted = 0 " +
                    "AND (frd.isDeleted IS NULL OR frd.isDeleted = 0) " +
                    "AND (dp.isDeleted IS NULL OR dp.isDeleted = 0) ";
        #endregion

        #region Query base para obter os niveis de contexto do relatório de pesquisa detalhado
        private static string relPesqBase_columns = "SELECT n.ID, Niveis.IDUpper, n.Codigo, n.IDTipoNivel, Niveis.Designacao, nd.Designacao, dp.InicioTexto, dp.InicioAno, dp.InicioMes, dp.InicioDia, dp.InicioAtribuida, dp.FimAno, dp.FimMes, dp.FimDia, dp.FimAtribuida";
        private static string relPesqBase_from = "FROM Nivel n ";
        private static string relPesqBase_joins =
                    "INNER JOIN #ReportParametersNiveis rpn ON rpn.ID = n.ID " +
                    "INNER JOIN ( " +
                        "SELECT DISTINCT rh.ID, rh.IDUpper, tnr.Designacao, rh.IDTipoNivelRelacionado, rpn.IsContext " +
                        "FROM #Niveis rpn " +
                            "INNER JOIN RelacaoHierarquica rh ON rh.ID = rpn.ID " +
                            "INNER JOIN TipoNivelRelacionado tnr ON tnr.ID = rh.IDTipoNivelRelacionado " +
                        "WHERE rh.isDeleted = 0 AND rh.IDTipoNivelRelacionado >= 7 " +
                    ") Niveis ON Niveis.ID = rpn.ID " +
                    "INNER JOIN NivelDesignado nd on nd.ID = rpn.ID " +
                    "LEFT JOIN FRDBase frd ON frd.IDNivel = rpn.ID " +
                    "LEFT JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frd.ID ";
        private static string relPesqBase_wheres =
                "WHERE n.isDeleted = 0 " +
                    "AND nd.isDeleted = 0 " +
                    "AND (frd.isDeleted IS NULL OR frd.isDeleted = 0) " +
                    "AND (dp.isDeleted IS NULL OR dp.isDeleted = 0) ";
        private static string relPesqBase_orders = "ORDER BY rpn.seq_id ";
        #endregion

        public override void InitializeReportResPesquisaDet(IDbConnection conn)
        {
            GisaDataSetHelperRule.ImportIDs(PesquisaRule.Current.CacheSearchResult.ToArray(), conn);

            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = 
                "CREATE TABLE #Niveis(ID BIGINT, IDUpper BIGINT, IsContext BIT DEFAULT 0) " +
                "CREATE INDEX id_ix ON #Niveis (ID) " +
                "CREATE TABLE #ReportParametersNiveis (ID BIGINT, seq_id BIGINT, PRIMARY KEY(ID)) " +
                "INSERT INTO #ReportParametersNiveis " +
                "SELECT ID, seq_nr " +
                "FROM #temp";
            command.ExecuteNonQuery();
        }

        public override void FinalizeReportResPesquisaDet(IDbConnection conn)
        {
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText =
                "DROP TABLE #Niveis " +
                "DROP TABLE #ReportParametersNiveis " +
                "DROP TABLE #SPParametersNiveis " +
                "DROP TABLE #SPResultsCodigos ";
            command.ExecuteNonQuery();
        }

        public override IDataReader ReportResPesquisaDetalhado(List<ReportParameter> fields, long IDTrustee, IDbConnection conn)
        {
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText =
                "INSERT INTO #Niveis " +
                "SELECT n.ID, rh.IDUpper, 0 " +
                "FROM #ReportParametersNiveis rpn " +
                    "INNER JOIN Nivel n ON n.ID = rpn.ID " +
                    "INNER JOIN RelacaoHierarquica rh ON rh.ID = n.ID " +
                "WHERE n.isDeleted = 0 " +
                    "AND rh.isDeleted = 0 " +

                "WHILE (@@ROWCOUNT > 0) " +
                "BEGIN " +
                    "INSERT INTO #Niveis " +
                    "SELECT DISTINCT n.ID, rh.IDUpper, 1 " +
                    "FROM Nivel n " +
                        "INNER JOIN RelacaoHierarquica rh ON rh.ID = n.ID " +
                        "INNER JOIN Nivel nUpper ON nUpper.ID = rh.IDUpper " +
                        "INNER JOIN #Niveis niveisAExpandir ON niveisAExpandir.IDUpper = n.ID " +
                        "LEFT JOIN #Niveis niveisJaExpandidos ON niveisJaExpandidos.ID = n.ID AND niveisJaExpandidos.IDUpper = nUpper.ID " +
                    "WHERE niveisJaExpandidos.ID IS NULL AND " +
                        "((nUpper.IDTipoNivel = 2 AND n.IDTipoNivel = 3) OR (nUpper.IDTipoNivel = 3 AND n.IDTipoNivel = 3)) " + // niveis subsequentes podem ser estruturais ou documentais
                        "AND n.isDeleted = 0 " +
                    "END " +
                // retornar lista dos produtores de todas as séries e documentos soltos
                "SELECT DISTINCT nProdutores.IDUpper, n.Codigo, tnr.Designacao, d.Termo " +
                "FROM Nivel n " +
                    "INNER JOIN ( " +
                        "SELECT DISTINCT rh.IDUpper " +
                        "FROM #Niveis rpn " +
                            "INNER JOIN RelacaoHierarquica rh ON rh.ID = rpn.ID " +
                        "WHERE rh.isDeleted = 0 AND rh.IDTipoNivelRelacionado IN (7,9) " +
                    ") nProdutores ON nProdutores.IDUpper = n.ID " +
                // os LEFT JOIN são para prever os casos onde os níveis produtores não são controlados (por isso. logicamente não são produtores)
                    "LEFT JOIN NivelControloAut nca ON nca.ID = n.ID " +
                    "LEFT JOIN ControloAut ca ON ca.ID = nca.IDControloAut " +
                    "LEFT JOIN ControloAutDicionario cad ON cad.IDControloAut = ca.ID AND cad.IDTipoControloAutForma = 1 " +
                    "LEFT JOIN Dicionario d ON d.ID = cad.IDDicionario " +
                    "LEFT JOIN RelacaoHierarquica rh ON rh.ID = n.ID " + // podem haver produtores que não estejam ligados na estrutura
                    "LEFT JOIN TipoNivelRelacionado tnr ON tnr.ID = rh.IDTipoNivelRelacionado " +
                "WHERE n.IDTipoNivel = 2 " +
                    "AND n.isDeleted = 0 " +
                    "AND (rh.isDeleted IS NULL OR rh.isDeleted = 0) " +
                    "AND (nca.isDeleted IS NULL OR nca.isDeleted = 0) " +
                    "AND (ca.isDeleted IS NULL OR ca.isDeleted = 0) " +
                    "AND (cad.isDeleted IS NULL OR cad.isDeleted = 0) " +
                    "AND (d.isDeleted IS NULL OR d.isDeleted = 0) " +

                // Obter e retornar os códigos completos de todas as séries e documentos soltos
                "CREATE TABLE #SPParametersNiveis (IDNivel BIGINT) " +
                "INSERT INTO #SPParametersNiveis " +
                "SELECT distinct rh.ID " +
                "FROM #Niveis rpn " +
                    "INNER JOIN RelacaoHierarquica rh on rh.ID = rpn.ID " +
                    "INNER JOIN Nivel n ON n.ID = rh.ID " +
                    "INNER JOIN Nivel nUpper ON nUpper.ID = rh.IDUpper " +
                "WHERE n.IDTipoNivel = 3 " +
                    "AND nUpper.IDTipoNivel = 2 " +
                    "AND n.isDeleted = 0 " +
                    "AND nUpper.isDeleted = 0 " +
                    "AND rh.isDeleted = 0 " +

                "CREATE TABLE #SPResultsCodigos(IDNivel BIGINT, CodigoCompleto NVARCHAR(255)) " +
                "CREATE CLUSTERED INDEX SPResultsCodigos_id_ix ON #SPResultsCodigos (IDNivel, CodigoCompleto) " +
                "EXEC sp_getCodigosCompletosNiveis " +

                // devolver a lista de codigos de referencia completos para os niveis documentais
                "SELECT IDNivel, CASE WHEN num > 1 THEN CodigoCompleto + ' (x' + CONVERT(NVARCHAR, num) + ')' ELSE CodigoCompleto END " +
                "FROM " +
                    "(SELECT c.IDNivel, c.CodigoCompleto, COUNT(*) num " +
                    "FROM #SPResultsCodigos c " +
                    "GROUP BY c.IDNivel, c.CodigoCompleto) codigosRepetidos " +
                "ORDER BY IDNivel";
           
            try {
                SqlDataReader reader = command.ExecuteReader();
                GetRelInfo(reader);

                StringBuilder com = new StringBuilder();
                StringBuilder nullColumns = new StringBuilder();
                List<ReportParameter> uniqueResultFields = new List<ReportParameter>();
                foreach (ReportParameter rp in fields)
                {
                    if (rp.RetType == ReportParameter.ReturnType.TextOnly)
                    {
                        if (nullColumns.Length > 0)
                            nullColumns.Append(", null");
                        else
                            nullColumns.Append("null");
                        uniqueResultFields.Add(rp);
                    }
                }

                // As duas queries adicionadas no if ou no else são quase iguais excepto num parâmetro. Esse parâmetro serve para
                // indicar se um dado nível impresso no relatório serve como contexto (o valor do parâmetro é igual a 1) para 
                // outros ou se é para mostrar toda a informação que lhe diz respeito (o valor do parâmetro é igual a 0). Este 
                // parâmetro é relevante quando o nível usado como ponto de partida é uma subsérie e dessa forma expande-se a 
                // árvore para cima de forma os seus níveis superiores serem usados somente como contexto
                if (uniqueResultFields.Count > 0)
                {
                    // niveis de contexto
                    DBAbstractDataLayer.DataAccessRules.RelatorioRule.BuildReportQuery(uniqueResultFields, new List<string> { ", ", " ", " AND " });
                    com.Append(
                        contextRelBase_columns + ", " + nullColumns.ToString() + " " +
                        contextRelBase_from +
                        contextRelBase_joins + " " +
                        contextRelBase_wheres + "; ");

                    // niveis com detalhe
                    com.Append(
                        relPesqBase_columns + ", " + DBAbstractDataLayer.DataAccessRules.RelatorioRule.mSelectClause + " " +
                        relPesqBase_from +
                        relPesqBase_joins + " " + DBAbstractDataLayer.DataAccessRules.RelatorioRule.mJoinClause + " " +
                        relPesqBase_wheres);

                    if (DBAbstractDataLayer.DataAccessRules.RelatorioRule.mWhereClause.Length > 0)
                        com.Append(" AND " + DBAbstractDataLayer.DataAccessRules.RelatorioRule.mWhereClause + " ");

                    com.Append(relPesqBase_orders + "; ");
                }
                else
                {
                    // apesar de o nível de detalhe ser idêntico para os dois tipos de nível, é preciso executar as 2 queries na mesma
                    // niveis de contexto
                    com.Append(contextRelBase_columns + " " + contextRelBase_from + contextRelBase_joins + contextRelBase_wheres + "; ");
                    // niveis com detalhe
                    com.Append(relPesqBase_columns + " " + relPesqBase_from + relPesqBase_joins + relPesqBase_wheres + relPesqBase_orders + "; ");
                }

                com.Append(BuildDetailsQuery(fields, IDTrustee));

                command.CommandText = com.ToString();
                command.CommandTimeout = 300; // TODO: optimizar query...
                reader = command.ExecuteReader();

                return reader;
            }
            catch (Exception e) {
                Debug.WriteLine(e.ToString());
                throw;
            }
        }


        #endregion

        #endregion

        private void GetRelInfo(IDataReader reader)
        {
            // obter a lista dos produtores de todas as séries e documentos soltos
            List<string> infoProdutores;
            produtores = new ArrayList();
            long IDNivel = 0;
            while (reader.Read())
            {
                // não deverão aparecer resultados repetidos excepto no caso de haver um nivel estrutural ser uma secção e subsecção ao mesmo tempo, por exemplo
                IDNivel = System.Convert.ToInt64(reader.GetValue(0));
                infoProdutores = new List<string>();
                infoProdutores.Add(IDNivel.ToString());
                infoProdutores.Add(reader.GetValue(1).ToString());
                infoProdutores.Add(reader.GetValue(2).ToString());
                infoProdutores.Add(reader.GetValue(3).ToString());
                produtores.Add(infoProdutores);
            }
            reader.NextResult();

            // obter a lista de codigos de referência completos para os niveis documentais (somente séries e documentos soltos)
            codCompletos = new Hashtable();
            IDNivel = 0;
            while (reader.Read())
            {
                IDNivel = System.Convert.ToInt64(reader.GetValue(0));
                if (!codCompletos.Contains(IDNivel))
                    codCompletos.Add(IDNivel, new List<string>());
                ((List<string>)codCompletos[IDNivel]).Add(reader.GetString(1).ToString());
            }
            reader.Close();
        }

        private string BuildDetailsQuery(List<ReportParameter> fields, long IDTrustee)
        {
            StringBuilder com = new StringBuilder();

            // autores
            List<ReportParameter> autoresFields = new List<ReportParameter>();
            foreach (ReportParameterRelInvCatPesqDet rp in fields)
            {
                if (rp.Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Autores)
                    autoresFields.Add(rp);
            }

            if (autoresFields.Count > 0)
            {
                com.Append(
                    relDetalhe_columns + "d.Termo " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN SFRDAutor autor ON autor.IDFRDBase = frd.ID AND autor.isDeleted = 0 " +
                        "INNER JOIN ControloAut ca ON ca.ID = autor.IDControloAut AND ca.isDeleted = 0 " +
                        "INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = ca.ID AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0 " +
                        "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted = 0 " +
                    relDetalhe_where);
            }

            // 
            List<ReportParameter> topNivelDesc = new List<ReportParameter>();
            foreach (ReportParameterRelInvCatPesqDet rp in fields)
            {
                if (rp.Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.HistAdministrativaBiografica)
                    topNivelDesc.Add(rp);
            }

            if (topNivelDesc.Count > 0)
            {
                com.Append(
                    "SELECT niveis.ID, sfrdc.FonteImediataDeAquisicao " +
                    "FROM (SELECT DISTINCT ID FROM #Niveis) niveis " +
	                    "INNER JOIN Nivel n ON n.ID = niveis.ID " +
	                    "INNER JOIN FRDBase frd ON n.ID = frd.IDNivel " +
	                    "LEFT JOIN SFRDContexto sfrdc ON sfrdc.IDFRDBase = frd.ID " +
                    "WHERE n.IDTipoNivel = 3 " +
	                    "AND n.isDeleted = 0 " +
                        "AND frd.isDeleted = 0 " +
	                    "AND (sfrdc.isDeleted IS NULL OR sfrdc.isDeleted = 0)");

                com.Append(
                    "SELECT niveis.ID, cade.DescDatasExistencia, cade.InicioAno, cade.InicioMes, cade.InicioDia, cade.InicioAtribuida, cade.FimAno, cade.FimMes, cade.FimDia, cade.FimAtribuida, ca.DescHistoria, ca.DescZonaGeografica, ca.DescEstatutoLegal, ca.DescOcupacoesActividades, ca.DescEnquadramentoLegal, ca.DescEstruturaInterna, ca.DescContextoGeral, ca.DescOutraInformacaoRelevante " +
                    "FROM (SELECT DISTINCT ID FROM #Niveis) niveis " +
                        "INNER JOIN Nivel n ON n.ID = niveis.ID " +
                        "INNER JOIN NivelControloAut nca ON nca.ID = n.ID " +
                        "INNER JOIN ControloAut ca ON ca.ID = nca.IDControloAut " +
                        "LEFT JOIN ControloAutDatasExistencia cade ON cade.IDControloAut = nca.ID " +
                    "WHERE n.IDTipoNivel = 2 " +
                        "AND n.isDeleted = 0 " +
                        "AND nca.isDeleted = 0 " +
                        "AND ca.isDeleted = 0 " +
                        "AND (cade.isDeleted IS NULL OR cade.isDeleted = 0)");
            }

            // tipologias, conteúdos, diplomas e modelos
            List<ReportParameter> caFields = new List<ReportParameter>();
            foreach (ReportParameterRelInvCatPesqDet rp in fields)
            {
                if (rp.Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Indexacao || rp.Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.TipologiaInformacional || rp.Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Diplomas || rp.Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Modelos)
                    caFields.Add(rp);
            }

            if (caFields.Count > 0)
            {
                DBAbstractDataLayer.DataAccessRules.RelatorioRule.BuildReportQuery(caFields, new List<string> { "", "", " OR " });
                com.Append(
                    relDetalhe_columns + "ca.IDTipoNoticiaAut, d.Termo " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN IndexFRDCA idx ON idx.IDFRDBase = frd.ID " +
                        "INNER JOIN ControloAut ca ON ca.ID = idx.IDControloAut " +
                        "INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = ca.ID " +
                        "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario " +
                    relDetalhe_where + "AND (" + DBAbstractDataLayer.DataAccessRules.RelatorioRule.mWhereClause + ") " +
                        "AND cad.IDTipoControloAutForma = 1 " +
                        "AND idx.isDeleted = 0 " +
                        "AND ca.isDeleted = 0 " +
                        "AND cad.isDeleted = 0 " +
                        "AND d.isDeleted = 0; ");
            }

            // diplomas legais (avaliação)
            List<ReportParameter> dplFields = new List<ReportParameter>();
            foreach (ReportParameterRelInvCatPesqDet rp in fields)
            {
                if (rp.Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.DiplomaLegal)
                    dplFields.Add(rp);
            }

            if (dplFields.Count > 0)
            {
                DBAbstractDataLayer.DataAccessRules.RelatorioRule.BuildReportQuery(caFields, new List<string> { "", "", " OR " });
                com.Append(
                    relDetalhe_columns + "ca.IDTipoNoticiaAut, d.Termo " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN IndexFRDCA idx ON idx.IDFRDBase = frd.ID " +
                        "INNER JOIN ControloAut ca ON ca.ID = idx.IDControloAut " +
                        "INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = ca.ID " +
                        "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario " +
                    relDetalhe_where + "AND (" + DBAbstractDataLayer.DataAccessRules.RelatorioRule.mWhereClause + ") " +
                        "AND cad.IDTipoControloAutForma = 1 " +
                        "AND idx.isDeleted = 0 " +
                        "AND ca.isDeleted = 0 " +
                        "AND cad.isDeleted = 0 " +
                        "AND d.isDeleted = 0; ");
            }

            // cota do documento
            List<ReportParameter> cotaDocFields = new List<ReportParameter>();
            foreach (ReportParameterRelInvCatPesqDet rp in fields)
            {
                if (rp.Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.CotaDocumento)
                    cotaDocFields.Add(rp);
            }

            if (cotaDocFields.Count > 0)
            {
                com.Append(
                    relDetalhe_columns + "COALESCE(c.Cota, ''), COALESCE(sfrduf.Cota, '') " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN SFRDUnidadeFisica sfrduf ON sfrduf.IDFRDBase = frd.ID " +
                        "INNER JOIN Nivel nuf ON nuf.ID = sfrduf.IDNivel AND nuf.isDeleted = 0 " +
                        "INNER JOIN FRDBase frduf ON frduf.IDNivel = nuf.ID AND frduf.isDeleted = 0 " +
                        "LEFT JOIN SFRDUFCota c ON c.IDFRDBase = frduf.ID AND c.isDeleted = 0 " +
                    relDetalhe_where +
                        "AND sfrduf.isDeleted = 0 ");
            }

            // unidades físicas
            List<ReportParameter> ufFields = new List<ReportParameter>();
            foreach (ReportParameterRelInvCatPesqDet rp in fields)
            {
                if (rp.Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.UFsAssociadas)
                    ufFields.Add(rp);
            }

            if (ufFields.Count > 0)
            {
                com.Append(
                    relDetalhe_columns + "nd.Designacao, n.Codigo, sfrdufc.Cota, sfrdufdf.MedidaAltura, sfrdufdf.MedidaLargura, sfrdufdf.MedidaProfundidade, tAcond.Designacao, tMed.Designacao " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN SFRDUnidadeFisica sfrduf ON sfrduf.IDFRDBase = frd.ID " +
                        "INNER JOIN Nivel n ON n.ID = sfrduf.IDNivel " +
                        "INNER JOIN NivelDesignado nd ON nd.ID = n.ID " +
                        "LEFT JOIN NivelUnidadeFisica nuf ON nuf.ID = n.ID " +
                        "INNER JOIN FRDBase frduf on n.ID = frduf.IDNivel " +
                        "LEFT JOIN SFRDUFCota sfrdufc ON sfrdufc.IDFRDBase = frduf.ID " +
                        "LEFT JOIN SFRDUFDescricaoFisica sfrdufdf ON sfrdufdf.IDFRDBase = frduf.ID " +
                        "LEFT JOIN TipoAcondicionamento tAcond ON tAcond.ID = sfrdufdf.IDTipoAcondicionamento " +
                        "LEFT JOIN TipoMedida tMed ON tMed.ID = sfrdufdf.IDTipoMedida " +
                    relDetalhe_where +
                        "AND sfrduf.isDeleted = 0 " +
                        "AND n.isDeleted = 0 " +
                        "AND nd.isDeleted = 0 " +
                        "AND (nuf.isDeleted IS NULL OR ((nuf.Eliminado IS NULL OR nuf.Eliminado = 0) AND nuf.isDeleted = 0)) " +
                        "AND frduf.isDeleted = 0 " +
                        "AND (tMed.isDeleted IS NULL OR tMed.isDeleted = 0) " +
                        "AND (tMed.isDeleted IS NULL OR tMed.isDeleted = 0) " +
                        "AND (sfrdufc.isDeleted IS NULL OR sfrdufc.isDeleted = 0) " +
                        "AND (sfrdufdf.isDeleted IS NULL OR sfrdufdf.isDeleted = 0); ");
            }

            List<ReportParameter> tradDocFields = new List<ReportParameter>();
            foreach (ReportParameterRelInvCatPesqDet rp in fields)
            {
                if (rp.Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.TradicaoDocumental)
                    tradDocFields.Add(rp);
            }

            if (tradDocFields.Count > 0)
            {
                com.Append(
                    relDetalhe_columns + "ttd.Designacao " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN SFRDTradicaoDocumental sfrdtd ON sfrdtd.IDFRDBase = frd.ID " +
                        "INNER JOIN TipoTradicaoDocumental ttd ON ttd.ID = sfrdtd.IDTipoTradicaoDocumental " +
                    relDetalhe_where +
                        "AND sfrdtd.isDeleted = 0 " +
                        "AND ttd.isDeleted = 0; ");
            }

            List<ReportParameter> ordFields = new List<ReportParameter>();
            foreach (ReportParameterRelInvCatPesqDet rp in fields)
            {
                if (rp.Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Ordenacao)
                    ordFields.Add(rp);
            }

            if (ordFields.Count > 0)
            {
                com.Append(
                    relDetalhe_columns + "tOrd.Designacao " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN SFRDOrdenacao sfrdo ON sfrdo.IDFRDBase = frd.ID " +
                        "INNER JOIN TipoOrdenacao tOrd ON tOrd.ID = sfrdo.IDTipoOrdenacao " +
                    relDetalhe_where +
                        "AND sfrdo.isDeleted = 0 " +
                        "AND tOrd.isDeleted = 0; ");
            }

            List<ReportParameter> objDigFields = new List<ReportParameter>();
            foreach (ReportParameterRelInvCatPesqDet rp in fields)
            {
                if (rp.Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.ObjectosDigitais)
                    objDigFields.Add(rp);
            }

            if (objDigFields.Count > 0)
            {
                com.Append(
                    relDetalhe_columns + "sfrdi.Identificador, sfrdi.Descricao, sfrdiv.Mount " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN SFRDImagem sfrdi ON sfrdi.IDFRDBase = frd.ID AND sfrdi.Tipo <> 'Fedora' " +
                        "INNER JOIN SFRDImagemVolume sfrdiv ON sfrdiv.ID = sfrdi.IDSFDImagemVolume " +
                    relDetalhe_where +
                        "AND sfrdi.isDeleted = 0 " +
                        "AND sfrdiv.isDeleted = 0; ");

                com.Append(
                    relDetalhe_columns + "od.Titulo, od.pid " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN SFRDImagem sfrdi ON sfrdi.IDFRDBase = frd.ID AND sfrdi.Tipo = 'Fedora' " +
                        "INNER JOIN SFRDImagemObjetoDigital sfrdod ON sfrdod.IDFRDBase = sfrdi.IDFRDBase AND sfrdod.idx = sfrdi.idx " +
                        "INNER JOIN ObjetoDigital od ON od.ID = sfrdod.IDObjetoDigital " +
                    relDetalhe_where +
                        "AND sfrdi.isDeleted = 0 " +
                        "AND sfrdod.isDeleted = 0 " +
                        "AND od.isDeleted = 0; ");
            }

            List<ReportParameter> lingFields = new List<ReportParameter>();
            foreach (ReportParameterRelInvCatPesqDet rp in fields)
            {
                if (rp.Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Lingua)
                    lingFields.Add(rp);
            }

            if (lingFields.Count > 0)
            {
                com.Append(
                    relDetalhe_columns + "iso.LanguageNameEnglish " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN SFRDCondicaoDeAcesso sfrdcda ON sfrdcda.IDFRDBase = frd.ID " +
                        "INNER JOIN SFRDLingua sfrdl ON sfrdl.IDFRDBase = sfrdcda.IDFRDBase " +
                        "INNER JOIN Iso639 iso ON iso.ID = sfrdl.IDIso639 " +
                    relDetalhe_where +
                        "AND sfrdcda.isDeleted = 0 " +
                        "AND sfrdl.isDeleted = 0 " +
                        "AND iso.isDeleted = 0; ");
            }

            List<ReportParameter> alfaFields = new List<ReportParameter>();
            foreach (ReportParameterRelInvCatPesqDet rp in fields)
            {
                if (rp.Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Alfabeto)
                    alfaFields.Add(rp);
            }

            if (alfaFields.Count > 0)
            {
                com.Append(
                    relDetalhe_columns + "iso.ScriptNameEnglish " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN SFRDCondicaoDeAcesso sfrdcda ON sfrdcda.IDFRDBase = frd.ID " +
                        "INNER JOIN SFRDAlfabeto sfrda ON sfrda.IDFRDBase = sfrdcda.IDFRDBase " +
                        "INNER JOIN Iso15924 iso ON iso.ID = sfrda.IDIso15924 " +
                    relDetalhe_where +
                        "AND sfrdcda.isDeleted = 0 " +
                        "AND sfrda.isDeleted = 0 " +
                        "AND iso.isDeleted = 0; ");
            }

            List<ReportParameter> TipoFormaSupAcondFields = new List<ReportParameter>();
            foreach (ReportParameterRelInvCatPesqDet rp in fields)
            {
                if (rp.Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.FormaSuporteAcondicionamento)
                    TipoFormaSupAcondFields.Add(rp);
            }

            if (TipoFormaSupAcondFields.Count > 0)
            {
                com.Append(
                    relDetalhe_columns + "tfsa.Designacao " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN SFRDCondicaoDeAcesso sfrdcda ON sfrdcda.IDFRDBase = frd.ID " +
                        "INNER JOIN SFRDFormaSuporteAcond sfrdfsa ON sfrdfsa.IDFRDBase = sfrdcda.IDFRDBase " +
                        "INNER JOIN TipoFormaSuporteAcond tfsa ON tfsa.ID = sfrdfsa.IDTipoFormaSuporteAcond " +
                    relDetalhe_where +
                        "AND sfrdcda.isDeleted = 0 " +
                        "AND sfrdfsa.isDeleted = 0 " +
                        "AND tfsa.isDeleted = 0; ");
            }

            List<ReportParameter> TipoMatSupFields = new List<ReportParameter>();
            foreach (ReportParameterRelInvCatPesqDet rp in fields)
            {
                if (rp.Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.MaterialSuporte)
                    TipoMatSupFields.Add(rp);
            }

            if (TipoMatSupFields.Count > 0)
            {
                com.Append(
                    relDetalhe_columns + "tmds.Designacao " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN SFRDCondicaoDeAcesso sfrdcda ON sfrdcda.IDFRDBase = frd.ID " +
                        "INNER JOIN SFRDMaterialDeSuporte sfrdmds ON sfrdmds.IDFRDBase = sfrdcda.IDFRDBase " +
                        "INNER JOIN TipoMaterialDeSuporte tmds ON tmds.ID = sfrdmds.IDTipoMaterialDeSuporte " +
                    relDetalhe_where +
                        "AND sfrdcda.isDeleted = 0 " +
                        "AND sfrdmds.isDeleted = 0 " +
                        "AND tmds.isDeleted = 0; ");
            }

            List<ReportParameter> TipoTecRegFields = new List<ReportParameter>();
            foreach (ReportParameterRelInvCatPesqDet rp in fields)
            {
                if (rp.Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.TecnicaRegisto)
                    TipoTecRegFields.Add(rp);
            }

            if (TipoTecRegFields.Count > 0)
            {
                com.Append(
                    relDetalhe_columns + "ttdr.Designacao " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN SFRDCondicaoDeAcesso sfrdcda ON sfrdcda.IDFRDBase = frd.ID " +
                        "INNER JOIN SFRDTecnicasDeRegisto sfrdtdr ON sfrdtdr.IDFRDBase = sfrdcda.IDFRDBase " +
                        "INNER JOIN TipoTecnicasDeRegisto ttdr ON ttdr.ID = sfrdtdr.IDTipoTecnicasDeRegisto " +
                    relDetalhe_where +
                        "AND sfrdcda.isDeleted = 0 " +
                        "AND sfrdtdr.isDeleted = 0 " +
                        "AND ttdr.isDeleted = 0; ");
            }

            #region Licença de obra
            if (fields.FirstOrDefault(f => ((ReportParameterRelInvCatPesqDet)f).Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_RequerentesIniciais) != null)
            {
                com.Append(
                    relDetalhe_columns + "lor.Nome " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN LicencaObra lo ON lo.IDFRDBase = frd.ID AND lo.isDeleted = 0 " +
                        "INNER JOIN LicencaObraRequerentes lor ON lor.IDFRDBase = lo.IDFRDBase AND lor.Tipo = 'INICIAL' AND lor.isDeleted = 0 " +
                    relDetalhe_where + "; ");
            }

            if (fields.FirstOrDefault(f => ((ReportParameterRelInvCatPesqDet)f).Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_RequerentesAverbamentos) != null)
            {
                com.Append(
                    relDetalhe_columns + "lor.Nome " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN LicencaObra lo ON lo.IDFRDBase = frd.ID AND lo.isDeleted = 0 " +
                        "INNER JOIN LicencaObraRequerentes lor ON lor.IDFRDBase = lo.IDFRDBase AND lor.Tipo = 'AVRB' AND lor.isDeleted = 0 " +
                    relDetalhe_where + "; ");
            }

            if (fields.FirstOrDefault(f => ((ReportParameterRelInvCatPesqDet)f).Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_DesignacaoNumPoliciaAct) != null)
            {
                com.Append(
                    relDetalhe_columns + "d.Termo, loa.NumPolicia " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN LicencaObra lo ON lo.IDFRDBase = frd.ID AND lo.isDeleted = 0 " +
                        "INNER JOIN LicencaObraLocalizacaoObraActual loa ON loa.IDFRDBase = lo.IDFRDBase AND loa.isDeleted = 0 " +
                        "INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = loa.IDControloAut AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0 " +
                        "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted = 0 " +
                    relDetalhe_where + "; ");
            }

            if (fields.FirstOrDefault(f => ((ReportParameterRelInvCatPesqDet)f).Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_DesignacaoNumPoliciaAntigo) != null)
            {
                com.Append(
                    relDetalhe_columns + "loa.NomeLocal, loa.NumPolicia " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN LicencaObra lo ON lo.IDFRDBase = frd.ID AND lo.isDeleted = 0 " +
                        "INNER JOIN LicencaObraLocalizacaoObraAntiga loa ON loa.IDFRDBase = lo.IDFRDBase AND loa.isDeleted = 0 " +
                    relDetalhe_where + "; ");
            }

            if (fields.FirstOrDefault(f => ((ReportParameterRelInvCatPesqDet)f).Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_TecnicoObra) != null)
            {
                com.Append(
                    relDetalhe_columns + "d.Termo " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN LicencaObra lo ON lo.IDFRDBase = frd.ID AND lo.isDeleted = 0 " +
                        "INNER JOIN LicencaObraTecnicoObra tecObr ON tecObr.IDFRDBase = lo.IDFRDBase AND tecObr.isDeleted = 0 " +
                        "INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = tecObr.IDControloAut AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0 " +
                        "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted = 0 " +
                    relDetalhe_where + "; ");
            }

            if (fields.FirstOrDefault(f => ((ReportParameterRelInvCatPesqDet)f).Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_AtestHabit) != null)
            {
                com.Append(
                    relDetalhe_columns + "ah.Codigo " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN LicencaObra lo ON lo.IDFRDBase = frd.ID AND lo.isDeleted = 0 " +
                        "INNER JOIN LicencaObraAtestadoHabitabilidade ah ON ah.IDFRDBase = lo.IDFRDBase AND ah.isDeleted = 0 " +
                    relDetalhe_where + "; ");
            }

            if (fields.FirstOrDefault(f => ((ReportParameterRelInvCatPesqDet)f).Campo == ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_DataLicConst) != null)
            {
                com.Append(
                    relDetalhe_columns + "dlc.Ano, dlc.Mes, dlc.Dia " +
                    relDetalhe_from +
                    string.Format(relDetalhe_joins, IDTrustee.ToString()) + " " +
                        "INNER JOIN LicencaObra lo ON lo.IDFRDBase = frd.ID AND lo.isDeleted = 0 " +
                        "INNER JOIN LicencaObraDataLicencaConstrucao dlc ON dlc.IDFRDBase = lo.IDFRDBase AND dlc.isDeleted = 0 " +
                    relDetalhe_where + "; ");
            }
            #endregion

            return com.ToString();
        }
    }
}
