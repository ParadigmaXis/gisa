using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.IntGestDoc.Model;
using GISA.IntGestDoc.Model.EntidadesInternas;
using GISA.IntGestDoc.Model.EntidadesExternas;
using GISA.Search;
using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

using log4net;

namespace GISA.IntGestDoc.Database
{
    internal class Database
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Database));

        internal Database() { }

        #region Documento

        internal static void MapDocumentoGisaToDataRows(CorrespondenciaDocs correspondenciaDoc, Dictionary<Entidade, DataRow> rows)
        {
            var dg = (DocumentoGisa)correspondenciaDoc.EntidadeInterna;
            GISADataset.NivelRow nRow = null;
            GISADataset.NivelDesignadoRow ndRow = null;
            GISADataset.FRDBaseRow frdRow = null;

            if (dg.Estado == TipoEstado.Novo)
            {
                nRow = GisaDataSetHelper.GetInstance().Nivel.NewNivelRow();
                nRow.CatCode = "NVL";
                nRow.Codigo = dg.Codigo;
                nRow.IDTipoNivel = TipoNivel.DOCUMENTAL;
                nRow.Versao = new byte[] { };
                nRow.isDeleted = 0;
                GisaDataSetHelper.GetInstance().Nivel.AddNivelRow(nRow);

                ndRow = GisaDataSetHelper.GetInstance().NivelDesignado.NewNivelDesignadoRow();
                ndRow.NivelRow = nRow;
                ndRow.Designacao = dg.TituloDoc.Valor;
                ndRow.Versao = new byte[] { };
                ndRow.isDeleted = 0;
                GisaDataSetHelper.GetInstance().NivelDesignado.AddNivelDesignadoRow(ndRow);

                frdRow = GisaDataSetHelper.GetInstance().FRDBase.NewFRDBaseRow();
                frdRow.NivelRow = nRow;
                frdRow.IDTipoFRDBase = (long)TipoFRDBase.FRDOIRecolha;
                frdRow.Versao = new byte[] { };
                frdRow.isDeleted = 0;
                GisaDataSetHelper.GetInstance().FRDBase.AddFRDBaseRow(frdRow);
            }
            else {
                nRow = GisaDataSetHelper.GetInstance().Nivel.Rows.Cast<GISADataset.NivelRow>().Where(r => r.ID == dg.Id).Single();
                frdRow = nRow.GetFRDBaseRows()[0];
            }

            rows[correspondenciaDoc.EntidadeInterna] = nRow;

            var sfrdConteudoEEstruturaRow = GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.Cast<GISADataset.SFRDConteudoEEstruturaRow>().Where(r => r.IDFRDBase == frdRow.ID).SingleOrDefault();
            if (sfrdConteudoEEstruturaRow == null)
            {
                sfrdConteudoEEstruturaRow = GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.NewSFRDConteudoEEstruturaRow();
                sfrdConteudoEEstruturaRow.FRDBaseRow = frdRow;
                sfrdConteudoEEstruturaRow.Incorporacao = "";
                sfrdConteudoEEstruturaRow.ConteudoInformacional = "";   
                sfrdConteudoEEstruturaRow.Versao = new byte[] { };
                sfrdConteudoEEstruturaRow.isDeleted = 0;
                GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.AddSFRDConteudoEEstruturaRow(sfrdConteudoEEstruturaRow);
            }

            if (correspondenciaDoc.EntidadeExterna.Tipo == TipoEntidadeExterna.Documento)
            {
                var morada = correspondenciaDoc.CorrespondenciasRAs.Select(cRa => cRa.EntidadeInterna).Cast<RegistoAutoridadeInterno>().SingleOrDefault(rai => rai.TipoNoticiaAut == TipoNoticiaAut.ToponimicoGeografico);
                if (morada != null)
                    sfrdConteudoEEstruturaRow.ConteudoInformacional = AppendValor(sfrdConteudoEEstruturaRow.ConteudoInformacional, "Morada: " + morada.Titulo);

                sfrdConteudoEEstruturaRow.ConteudoInformacional = AppendValor(sfrdConteudoEEstruturaRow.ConteudoInformacional, dg.NumLocalRefPred);
                sfrdConteudoEEstruturaRow.ConteudoInformacional = AppendValor(sfrdConteudoEEstruturaRow.ConteudoInformacional, dg.CodPostalLoc);
            }

            if (dg.Notas.TipoOpcao != TipoOpcao.Ignorar && dg.Notas.Valor != null && dg.Notas.Valor.Length > 0)
                sfrdConteudoEEstruturaRow.ConteudoInformacional = AppendValor(sfrdConteudoEEstruturaRow.ConteudoInformacional, dg.Notas);

            var sfrdDatasProducaoRow = GisaDataSetHelper.GetInstance().SFRDDatasProducao.Cast<GISADataset.SFRDDatasProducaoRow>().Where(r => r.IDFRDBase == frdRow.ID).SingleOrDefault();
            if (sfrdDatasProducaoRow == null)
            {
                sfrdDatasProducaoRow = GisaDataSetHelper.GetInstance().SFRDDatasProducao.NewSFRDDatasProducaoRow();
                sfrdDatasProducaoRow.FRDBaseRow = frdRow;
                sfrdDatasProducaoRow.FimAno = "";
                sfrdDatasProducaoRow.FimMes = "";
                sfrdDatasProducaoRow.FimDia = "";
                sfrdDatasProducaoRow.InicioAno = "";
                sfrdDatasProducaoRow.InicioMes = "";
                sfrdDatasProducaoRow.InicioDia = "";
                sfrdDatasProducaoRow.InicioAtribuida = false;
                sfrdDatasProducaoRow.FimAtribuida = false;
                sfrdDatasProducaoRow.Versao = new byte[] { };
                sfrdDatasProducaoRow.isDeleted = 0;
                GisaDataSetHelper.GetInstance().SFRDDatasProducao.AddSFRDDatasProducaoRow(sfrdDatasProducaoRow);
            }

            if (dg.DataCriacao.TipoOpcao != TipoOpcao.Ignorar && dg.DataCriacao.Valor != null)
            {
                sfrdDatasProducaoRow.FimAno = dg.DataCriacao.Valor.AnoFim;
                sfrdDatasProducaoRow.FimMes = dg.DataCriacao.Valor.MesFim;
                sfrdDatasProducaoRow.FimDia = dg.DataCriacao.Valor.DiaFim;
                sfrdDatasProducaoRow.InicioAno = dg.DataCriacao.Valor.AnoInicio;
                sfrdDatasProducaoRow.InicioMes = dg.DataCriacao.Valor.MesInicio;
                sfrdDatasProducaoRow.InicioDia = dg.DataCriacao.Valor.DiaInicio;
            }

            var sfrdAgrupadorRow = GisaDataSetHelper.GetInstance().SFRDAgrupador.Cast<GISADataset.SFRDAgrupadorRow>().Where(r => r.IDFRDBase == frdRow.ID).SingleOrDefault();
            if (sfrdAgrupadorRow == null)
            {
                sfrdAgrupadorRow = GisaDataSetHelper.GetInstance().SFRDAgrupador.NewSFRDAgrupadorRow();
                sfrdAgrupadorRow.FRDBaseRow = frdRow;
                sfrdAgrupadorRow.Agrupador = "";
                sfrdAgrupadorRow.Versao = new byte[] { };
                sfrdAgrupadorRow.isDeleted = 0;
                GisaDataSetHelper.GetInstance().SFRDAgrupador.AddSFRDAgrupadorRow(sfrdAgrupadorRow);
            }

            if (dg.Agrupador.TipoOpcao != TipoOpcao.Ignorar && dg.Agrupador.Valor != null)
                sfrdAgrupadorRow.Agrupador = dg.Agrupador.Valor;

            if (dg.Estado != TipoEstado.Novo && !nRow.Codigo.Equals(dg.Codigo))
            {                
                var sfrdCodigoRow = GisaDataSetHelper.GetInstance().Codigo.Cast<GISADataset.CodigoRow>().Where(r => r.IDFRDBase == frdRow.ID && r.Codigo.Equals(dg.Codigo)).SingleOrDefault();
                if (sfrdCodigoRow == null)
                {
                    sfrdCodigoRow = GisaDataSetHelper.GetInstance().Codigo.NewCodigoRow();
                    sfrdCodigoRow.FRDBaseRow = frdRow;
                    sfrdCodigoRow.Codigo = dg.Codigo;
                    sfrdCodigoRow.Versao = new byte[] { };
                    sfrdCodigoRow.isDeleted = 0;
                    GisaDataSetHelper.GetInstance().Codigo.AddCodigoRow(sfrdCodigoRow);
                }
            }

            if (dg.NumeroEspecifico.TipoOpcao != TipoOpcao.Ignorar && dg.NumeroEspecifico.Valor != null && dg.NumeroEspecifico.Valor.Length > 0)
            {
                var sfrdCodigoRow = GisaDataSetHelper.GetInstance().Codigo.Cast<GISADataset.CodigoRow>().Where(r => r.IDFRDBase == frdRow.ID && r.Codigo.Equals(dg.NumeroEspecifico.Valor)).SingleOrDefault();
                if (sfrdCodigoRow == null)
                {
                    sfrdCodigoRow = GisaDataSetHelper.GetInstance().Codigo.NewCodigoRow();
                    sfrdCodigoRow.FRDBaseRow = frdRow;
                    sfrdCodigoRow.Codigo = dg.NumeroEspecifico.Valor;
                    sfrdCodigoRow.Versao = new byte[] { };
                    sfrdCodigoRow.isDeleted = 0;
                    GisaDataSetHelper.GetInstance().Codigo.AddCodigoRow(sfrdCodigoRow);
                }
            }

            var sfrdCondicaoDeAcessoRow = GisaDataSetHelper.GetInstance().SFRDCondicaoDeAcesso.Cast<GISADataset.SFRDCondicaoDeAcessoRow>().Where(r => r.IDFRDBase == frdRow.ID).SingleOrDefault();
            if (sfrdCondicaoDeAcessoRow == null)
            {
                sfrdCondicaoDeAcessoRow = GisaDataSetHelper.GetInstance().SFRDCondicaoDeAcesso.NewSFRDCondicaoDeAcessoRow();
                sfrdCondicaoDeAcessoRow.FRDBaseRow = frdRow;
                sfrdCondicaoDeAcessoRow.CondicaoDeAcesso = "";
                sfrdCondicaoDeAcessoRow.Versao = new byte[] { };
                sfrdCondicaoDeAcessoRow.isDeleted = 0;
                GisaDataSetHelper.GetInstance().SFRDCondicaoDeAcesso.AddSFRDCondicaoDeAcessoRow(sfrdCondicaoDeAcessoRow);
            }

            if (dg.Confidencialidade.TipoOpcao != TipoOpcao.Ignorar && dg.Confidencialidade.Valor != null && dg.Confidencialidade.Valor.Length > 0)
                sfrdCondicaoDeAcessoRow.CondicaoDeAcesso = AppendValor(sfrdCondicaoDeAcessoRow.CondicaoDeAcesso, dg.Confidencialidade);

            long imgOrder = 1;
            var imgRows = GisaDataSetHelper.GetInstance().SFRDImagem.Cast<GISADataset.SFRDImagemRow>().Where(r => r.IDFRDBase == frdRow.ID);
            if (imgRows.Count() > 0)
                imgOrder = imgRows.Max(r => r.GUIOrder) + 1;

            foreach(var o in dg.ObjDigitais)
            {
                GISADataset.SFRDImagemVolumeRow imgVolRow = null;
                string mount = "";
                string identifier = "";
                if (o.Tipo == (int)ResourceAccessType.DICAnexo)
                {
                    // TODO: em vez de se guardar o NUD do Anexo deve-se guardar o NUD do documento
                    mount = o.NUD;
                    // TODO: o nome do ficheiro que deve ser guardado deve ter o padrão "Conteudo_numero_numero_numero.extensao"
                    //       tem que se fazer split do ficheiro por '_' e substituir o primeiro token por "Conteudo" e voltar a juntar tudo com o '_'
                    identifier = o.NomeFicheiro;
                }
                else
                {
                    mount = "";
                    identifier = o.NUD;
                }

                if (identifier == null || identifier == "")
                {
                    //MessageBox.Show(String.Format("O conteúdo associado com a descrição \"{0}\" do anexo {1} não vai ser integrado porque não tem o nome do ficheiro definido.", o.TipoDescricao, o.NUD), "Integração", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Trace.WriteLine(String.Format("INTEGRACAO: O conteúdo associado com a descrição \"{0}\" do anexo {1} não vai ser integrado porque não tem o nome do ficheiro definido.", o.TipoDescricao, o.NUD));
                    continue;
                }

                imgVolRow = GisaDataSetHelper.GetInstance().SFRDImagemVolume.Cast<GISADataset.SFRDImagemVolumeRow>().
                    FirstOrDefault(r => r.Mount == o.NUD);

                if (imgVolRow == null)
                    imgVolRow = GisaDataSetHelper.GetInstance().SFRDImagemVolume.AddSFRDImagemVolumeRow(mount, new byte[] { }, 0);

                var imgRow = GisaDataSetHelper.GetInstance().SFRDImagem.NewSFRDImagemRow();
                imgRow.FRDBaseRow = frdRow;
                imgRow.GUIOrder = imgOrder++;
                imgRow.Tipo = GUIHelper.TranslationHelper.FormatTipoAcessoEnumToTipoAcessoText((ResourceAccessType)o.Tipo);
                imgRow.Descricao = o.TipoDescricao;
                imgRow.SFRDImagemVolumeRow = imgVolRow;
                imgRow.Identificador = identifier;
                GisaDataSetHelper.GetInstance().SFRDImagem.AddSFRDImagemRow(imgRow);
            }
            
            foreach (var correspRA in correspondenciaDoc.CorrespondenciasRAs)
            {
                // onomásticos e geográficos são mapeados no conteudo estruturado
                if (!(correspondenciaDoc.EntidadeExterna.Tipo == TipoEntidadeExterna.DocumentoComposto && correspRA.EntidadeExterna.Tipo == TipoEntidadeExterna.Onomastico && correspRA.EntidadeExterna.Tipo == TipoEntidadeExterna.Geografico))
                    MapControloAutToDataRows(correspRA, frdRow, rows);
            }

            // NOTA: o mapeamento do conteudo estruturado deve ser sempre feito depois do mapeamento dos controlos de autoridade
            if (correspondenciaDoc.EntidadeExterna.Tipo == TipoEntidadeExterna.DocumentoComposto)
                MapConteudoEstruturado(correspondenciaDoc, rows);
            else if (dg.Processo != null)
            {
                var proc = dg.Processo.EntidadeInterna;
                var nProcRow = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().Single(r => r.ID == proc.Id);
                rows[proc] = nProcRow;
            }

            MapDocumentoRHToDataRows(correspondenciaDoc, rows);
        }

        private static string AppendValor(string strBase, string strAppend)
        {
            if (strBase.Length == 0)
                strBase = strAppend;
            else
                strBase += System.Environment.NewLine + strAppend;

            return strBase;
        }

        private static string AppendValor(string strBase, PropriedadeDocumentoGisaTemplate<string> propValor)
        {
            if (propValor.TipoOpcao == TipoOpcao.Ignorar || propValor.EstadoRelacaoPorOpcao[propValor.TipoOpcao] == TipoEstado.SemAlteracoes || propValor.Valor == null || propValor.Valor.Length == 0) return strBase;

            var strAppend = propValor.Valor;
            if (strBase.Length == 0)
                strBase = strAppend;
            else
                strBase += System.Environment.NewLine + strAppend;

            propValor.EstadoRelacaoPorOpcao[propValor.TipoOpcao] = TipoEstado.SemAlteracoes;

            return strBase;
        }

        private static void MapConteudoEstruturado(CorrespondenciaDocs correspondenciaDoc, Dictionary<Entidade, DataRow> rows)
        {
            var docExt = correspondenciaDoc.EntidadeExterna as DocumentoComposto;
            var dg = correspondenciaDoc.EntidadeInterna as DocumentoGisa;
            if (dg.Processo != null) return;

            var nRow = rows[dg] as GISADataset.NivelRow;
            var frdRow = nRow.GetFRDBaseRows().Single();
            var caTipologia = frdRow.GetIndexFRDCARows().Where(i => i["Selector"] != DBNull.Value && i.Selector == -1)
                .Select(r => r.ControloAutRow)
                .SingleOrDefault(ca => ca.IDTipoNoticiaAut == (long)TipoNoticiaAut.TipologiaInformacional);
            
            if (caTipologia != null && caTipologia.TipoTipologiasRow != null && caTipologia.TipoTipologiasRow.BuiltInName.Equals("PROCESSO_DE_OBRAS"))
            {
                var licenca = frdRow.GetLicencaObraRows().SingleOrDefault();
                if (licenca == null)
                {
                    licenca = GisaDataSetHelper.GetInstance().LicencaObra.NewLicencaObraRow();
                    licenca.FRDBaseRow = frdRow;
                    licenca.TipoObra = string.Empty;
                    licenca.PropriedadeHorizontal = false;
                    licenca.PHTexto = string.Empty;
                    licenca.Versao = new byte[] { };
                    licenca.isDeleted = 0;
                    GisaDataSetHelper.GetInstance().LicencaObra.AddLicencaObraRow(licenca);
                }

                correspondenciaDoc.CorrespondenciasRAs.Where(cRA => ((RegistoAutoridadeInterno)cRA.EntidadeInterna).TipoNoticiaAut == TipoNoticiaAut.ToponimicoGeografico).ToList().ForEach(cRA =>
                {
                    var caRow = CreateControloAut(cRA, rows);
                    var locObr = licenca.GetLicencaObraLocalizacaoObraActualRows().SingleOrDefault(r => r.IDControloAut == caRow.ID);
                    if (locObr == null)
                    {
                        locObr = GisaDataSetHelper.GetInstance().LicencaObraLocalizacaoObraActual.NewLicencaObraLocalizacaoObraActualRow();
                        locObr.LicencaObraRow = licenca;
                        locObr.ControloAutRow = caRow;
                        locObr.IDFRDBase = frdRow.ID;
                        locObr.NumPolicia = docExt.LocalizacoesObraDesignacaoActual.SingleOrDefault(l => l.LocalizacaoObraDesignacaoActual.Equals((Model.EntidadesExternas.Geografico)cRA.EntidadeExterna)).NroPolicia;
                        locObr.Versao = new byte[] { };
                        locObr.isDeleted = 0;
                        GisaDataSetHelper.GetInstance().LicencaObraLocalizacaoObraActual.AddLicencaObraLocalizacaoObraActualRow(locObr);
                    }
                });

                correspondenciaDoc.CorrespondenciasRAs.Where(cRA => ((RegistoAutoridadeInterno)cRA.EntidadeInterna).TipoNoticiaAut == TipoNoticiaAut.Onomastico).ToList().ForEach(cRA =>
                {
                    var caRow = CreateControloAut(cRA, rows);
                    var tecObr = licenca.GetLicencaObraTecnicoObraRows().SingleOrDefault(r => r.IDControloAut == caRow.ID);
                    if (tecObr == null)
                    {
                        tecObr = GisaDataSetHelper.GetInstance().LicencaObraTecnicoObra.NewLicencaObraTecnicoObraRow();
                        tecObr.LicencaObraRow = licenca;
                        tecObr.ControloAutRow = caRow;
                        tecObr.IDFRDBase = frdRow.ID;
                        tecObr.Versao = new byte[] { };
                        tecObr.isDeleted = 0;
                        GisaDataSetHelper.GetInstance().LicencaObraTecnicoObra.AddLicencaObraTecnicoObraRow(tecObr);
                    }
                });
                
                dg.Requerentes.Where(r => r.TipoOpcao != TipoOpcao.Ignorar).ToList().ForEach(requerente =>
                {
                    var req = licenca.GetLicencaObraRequerentesRows().SingleOrDefault(r => r.Nome == requerente.Valor && r.Tipo.ToUpper().Trim().Equals("INICIAL"));
                    if (req == null)
                    {
                        req = GisaDataSetHelper.GetInstance().LicencaObraRequerentes.NewLicencaObraRequerentesRow();
                        req.IDFRDBase = licenca.IDFRDBase;
                        req.Nome = requerente.Valor;
                        req.Tipo = "INICIAL";
                        req.isDeleted = 0;
                        req.Versao = new byte[] { };
                        GisaDataSetHelper.GetInstance().LicencaObraRequerentes.AddLicencaObraRequerentesRow(req);
                    }
                });

                dg.Averbamentos.Where(r => r.TipoOpcao != TipoOpcao.Ignorar).ToList().ForEach(averbamento =>
                {
                    var av = licenca.GetLicencaObraRequerentesRows().SingleOrDefault(r => r.Nome == averbamento.Valor && r.Tipo.ToUpper().Trim().Equals("AVRB"));
                    if (av == null)
                    {
                        av = GisaDataSetHelper.GetInstance().LicencaObraRequerentes.NewLicencaObraRequerentesRow();
                        av.IDFRDBase = licenca.IDFRDBase;
                        av.Nome = averbamento.Valor;
                        av.Tipo = "AVRB";
                        av.isDeleted = 0;
                        av.Versao = new byte[] { };
                        GisaDataSetHelper.GetInstance().LicencaObraRequerentes.AddLicencaObraRequerentesRow(av);
                    }
                });
            }
            else
            {
                var conteudo = frdRow.GetSFRDConteudoEEstruturaRows().SingleOrDefault();
                if (conteudo == null)
                {
                    conteudo = GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.NewSFRDConteudoEEstruturaRow();
                    conteudo.FRDBaseRow = frdRow;
                    conteudo.ConteudoInformacional = "";
                    conteudo.Incorporacao = "";
                    conteudo.isDeleted = 0;
                    conteudo.Versao = new byte[] { };
                    GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.AddSFRDConteudoEEstruturaRow(conteudo);
                }

                var s = new StringBuilder();
                var requerentes = dg.Requerentes.Where(r => r.TipoOpcao != TipoOpcao.Ignorar).ToList();
                if (requerentes.Count > 0)
                {
                    s.AppendLine();

                    s.Append("Requerentes (inicial): ");
                    requerentes.ForEach(requerente => s.AppendFormat("{0}, ", requerente.Valor));
                    s.Remove(s.Length - 2, 2);

                    s.AppendLine();
                }

                var averbamentos = dg.Averbamentos.Where(r => r.TipoOpcao != TipoOpcao.Ignorar).ToList();
                if (averbamentos.Count > 0)
                {
                    s.AppendLine();

                    s.Append("Requerentes (averbamento): ");
                    averbamentos.ForEach(averbamento => s.AppendFormat("{0}, ", averbamento.Valor));
                    s.Remove(s.Length - 2, 2);

                    s.AppendLine();
                }

                var locais = correspondenciaDoc.CorrespondenciasRAs.Where(cRA => ((RegistoAutoridadeInterno)cRA.EntidadeInterna).TipoNoticiaAut == TipoNoticiaAut.ToponimicoGeografico).ToList();
                if (locais.Count > 0)
                {
                    s.AppendLine();

                    s.Append("Locais: ");
                    locais.ForEach(cRA => { 
                        var caRow = CreateControloAut(cRA, rows); 
                        var index = frdRow.GetIndexFRDCARows().SingleOrDefault(r => r.IDControloAut == caRow.ID);
                        if (index == null)
                        {
                            index = GisaDataSetHelper.GetInstance().IndexFRDCA.NewIndexFRDCARow();
                            index.ControloAutRow = caRow;
                            index.FRDBaseRow = frdRow;
                            index.Versao = new byte[] { };
                            index.isDeleted = 0;
                            GisaDataSetHelper.GetInstance().IndexFRDCA.AddIndexFRDCARow(index);
                        }
                        s.AppendFormat("{0}, ", cRA.EntidadeInterna.Titulo); });
                    s.Remove(s.Length - 2, 2);

                    s.AppendLine();
                }

                var tecnicos = correspondenciaDoc.CorrespondenciasRAs.Where(cRA => ((RegistoAutoridadeInterno)cRA.EntidadeInterna).TipoNoticiaAut == TipoNoticiaAut.Onomastico).ToList();
                if (tecnicos.Count > 0)
                {
                    s.AppendLine();

                    s.Append("Técnicos de obra: ");
                    tecnicos.ForEach(cRA => {
                        var caRow = CreateControloAut(cRA, rows);
                        var index = frdRow.GetIndexFRDCARows().SingleOrDefault(r => r.IDControloAut == caRow.ID);
                        if (index == null)
                        {
                            index = GisaDataSetHelper.GetInstance().IndexFRDCA.NewIndexFRDCARow();
                            index.ControloAutRow = caRow;
                            index.FRDBaseRow = frdRow;
                            index.Versao = new byte[] { };
                            index.isDeleted = 0;
                            GisaDataSetHelper.GetInstance().IndexFRDCA.AddIndexFRDCARow(index);
                        }
                        s.AppendFormat("{0}, ", cRA.EntidadeInterna.Titulo); });
                    s.Remove(s.Length - 2, 2);
                }

                if (s.Length > 0)
                    conteudo.ConteudoInformacional += s.ToString();
            }
        }

        internal static void MapDocumentoRHToDataRows(CorrespondenciaDocs correspondenciaDoc, Dictionary<Entidade, DataRow> rows)
        {
            var dg = correspondenciaDoc.EntidadeInterna as DocumentoGisa;
            var nRowUpper = default(GISADataset.NivelRow);

            if (dg.Processo != null)
            {
                nRowUpper = rows[dg.Processo.EntidadeInterna] as GISADataset.NivelRow;

                var nRow = rows[dg] as GISADataset.NivelRow;
                var r = nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().SingleOrDefault();
                if (r != null)
                    r.Delete();

                var rhRow = GisaDataSetHelper.GetInstance().RelacaoHierarquica.NewRelacaoHierarquicaRow();
                rhRow.ID = nRow.ID;
                rhRow.IDUpper = nRowUpper.ID;
                rhRow.IDTipoNivelRelacionado = TipoNivelRelacionado.SD;
                rhRow.Versao = new byte[] { };
                rhRow.isDeleted = 0;
                GisaDataSetHelper.GetInstance().RelacaoHierarquica.AddRelacaoHierarquicaRow(rhRow);
            }

            if (dg.Serie.Valor != null)
            {
                var serie = dg.Serie.Valor;
                var nRowSerie = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().Single(r => r.ID == serie.Id);

                if (dg.Serie.EstadoRelacaoPorOpcao[dg.Serie.TipoOpcao] != TipoEstado.SemAlteracoes)
                {
                    // mapear relacão entre o processo e a série
                    var nRow = rows[correspondenciaDoc.EntidadeInterna] as GISADataset.NivelRow;

                    var rhRow = GisaDataSetHelper.GetInstance().RelacaoHierarquica.NewRelacaoHierarquicaRow();
                    rhRow.ID = nRowUpper == null ? nRow.ID : nRowUpper.ID;
                    rhRow.IDUpper = serie.Id;
                    rhRow.IDTipoNivelRelacionado = TipoNivelRelacionado.D;
                    rhRow.Versao = new byte[] { };
                    rhRow.isDeleted = 0;
                    GisaDataSetHelper.GetInstance().RelacaoHierarquica.AddRelacaoHierarquicaRow(rhRow);
                }

                nRowUpper = nRowSerie;
            }
            
            // mapear produtor do documento simples 
            var correspCA = correspondenciaDoc.CorrespondenciasRAs.Where(c => ((RegistoAutoridadeInterno)c.EntidadeInterna).TipoNoticiaAut == TipoNoticiaAut.EntidadeProdutora).SingleOrDefault();

            if (correspCA != null && correspCA.EstadoRelacaoPorOpcao[correspCA.TipoOpcao] != TipoEstado.SemAlteracoes)
            {
                // determinar se o ID da RH é referente ao documento simples, composto, ou alguma (sub)série
                long nDocID = -1;
                long idSerie = 0;
                if (nRowUpper != null)
                {
                    idSerie = GetSerieID(nRowUpper.ID);
                    nDocID = idSerie < 0 ? nRowUpper.ID : idSerie;
                }
                else
                {
                    var nRow = rows[correspondenciaDoc.EntidadeInterna] as GISADataset.NivelRow;
                    idSerie = GetSerieID(nRow.ID);
                    nDocID = idSerie < 0 ? nRow.ID : idSerie;
                }

                var caRow = GisaDataSetHelper.GetInstance().ControloAut.Cast<GISADataset.ControloAutRow>().Single(ca => ca.ID == correspCA.EntidadeInterna.Id);
                var nRowCA = caRow.GetNivelControloAutRows().Single().NivelRow;

                if (GisaDataSetHelper.GetInstance().RelacaoHierarquica.Cast<GISADataset.RelacaoHierarquicaRow>().SingleOrDefault(r => r.RowState != DataRowState.Deleted && r.ID == nDocID && r.IDUpper == nRowCA.ID) != null)
                    return;

                var rhRow = GisaDataSetHelper.GetInstance().RelacaoHierarquica.NewRelacaoHierarquicaRow();
                rhRow.ID = nDocID;
                rhRow.IDUpper = nRowCA.ID;
                rhRow.IDTipoNivelRelacionado = idSerie < 0 ? TipoNivelRelacionado.D : TipoNivelRelacionado.SR;
                rhRow.Versao = new byte[] { };
                rhRow.isDeleted = 0;
                GisaDataSetHelper.GetInstance().RelacaoHierarquica.AddRelacaoHierarquicaRow(rhRow);
            }
        }

        private static long GetSerieID(long p)
        {
            long dg = -1;
            var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                dg = DBAbstractDataLayer.DataAccessRules.IntGestDocRule.Current.GetSerie(GisaDataSetHelper.GetInstance(), p, ho.Connection);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
            finally
            {
                ho.Dispose();
            }
            return dg;
        }
        #endregion

        #region ControloAut

        internal static void MapControloAutToDataRows(CorrespondenciaRAs correspondenciaRA, GISADataset.FRDBaseRow dgFRDRow, Dictionary<Entidade, DataRow> rows)
        {
            var ca = (RegistoAutoridadeInterno)correspondenciaRA.EntidadeInterna;
            var caRow = CreateControloAut(correspondenciaRA, rows);

            if (ca.TipoNoticiaAut != TipoNoticiaAut.EntidadeProdutora)
                MapDocCARelationToDataRows(correspondenciaRA, dgFRDRow, caRow);
        }

        private static GISADataset.ControloAutRow CreateControloAut(CorrespondenciaRAs correspondenciaRA, Dictionary<Entidade, DataRow> rows)
        {
            var ca = (RegistoAutoridadeInterno)correspondenciaRA.EntidadeInterna;
            GISADataset.ControloAutRow caRow = null;
            GISADataset.ControloAutDicionarioRow cadRow = null;
            GISADataset.DicionarioRow dRow = null;

            // podem existir registos de autoridade internos diferentes mas com informação idêntica entre si por via 
            // da edição de sugestões onde podem ser criados novos sem que haja qualquer tipo de validação quanto 
            // à repetição de dados
            if (ca.Estado == TipoEstado.Novo)
            {
                dRow = GisaDataSetHelper.GetInstance().Dicionario.Cast<GISADataset.DicionarioRow>().Where(d => d.Termo.Equals(ca.Titulo)).SingleOrDefault();

                if (dRow != null)
                {
                    cadRow = GisaDataSetHelper.GetInstance().ControloAutDicionario.Cast<GISADataset.ControloAutDicionarioRow>().Where(cad =>
                        cad.IDDicionario == dRow.ID &&
                        cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada &&
                        cad.ControloAutRow.IDTipoNoticiaAut == (long)ca.TipoNoticiaAut).SingleOrDefault();

                    if (cadRow != null)
                        caRow = cadRow.ControloAutRow;
                    else
                    {
                        caRow = GisaDataSetHelper.GetInstance().ControloAut.NewControloAutRow();
                        caRow.IDTipoNoticiaAut = (long)ca.TipoNoticiaAut;
                        caRow.Autorizado = false;
                        caRow.Completo = false;
                        if (ca.TipoNoticiaAut == TipoNoticiaAut.Onomastico)
                            caRow.ChaveColectividade = ((Model.EntidadesInternas.Onomastico)ca).Codigo;
                        caRow.Versao = new byte[] { };
                        caRow.isDeleted = 0;
                        GisaDataSetHelper.GetInstance().ControloAut.AddControloAutRow(caRow);

                        cadRow = GisaDataSetHelper.GetInstance().ControloAutDicionario.NewControloAutDicionarioRow();
                        cadRow.ControloAutRow = caRow;
                        cadRow.DicionarioRow = dRow;
                        cadRow.IDTipoControloAutForma = (long)TipoControloAutForma.FormaAutorizada;
                        cadRow.Versao = new byte[] { };
                        cadRow.isDeleted = 0;
                        GisaDataSetHelper.GetInstance().ControloAutDicionario.AddControloAutDicionarioRow(cadRow);
                    }
                }
                else
                {
                    caRow = GisaDataSetHelper.GetInstance().ControloAut.NewControloAutRow();
                    caRow.IDTipoNoticiaAut = (long)ca.TipoNoticiaAut;
                    caRow.Autorizado = false;
                    caRow.Completo = false;
                    if (ca.TipoNoticiaAut == TipoNoticiaAut.Onomastico)
                        caRow.ChaveColectividade = ((Model.EntidadesInternas.Onomastico)ca).Codigo;
                    caRow.Versao = new byte[] { };
                    caRow.isDeleted = 0;
                    GisaDataSetHelper.GetInstance().ControloAut.AddControloAutRow(caRow);

                    dRow = GisaDataSetHelper.GetInstance().Dicionario.NewDicionarioRow();
                    dRow.CatCode = "CA";
                    dRow.Termo = ca.Titulo;
                    dRow.Versao = new byte[] { };
                    dRow.isDeleted = 0;
                    GisaDataSetHelper.GetInstance().Dicionario.AddDicionarioRow(dRow);

                    cadRow = GisaDataSetHelper.GetInstance().ControloAutDicionario.NewControloAutDicionarioRow();
                    cadRow.ControloAutRow = caRow;
                    cadRow.DicionarioRow = dRow;
                    cadRow.IDTipoControloAutForma = (long)TipoControloAutForma.FormaAutorizada;
                    cadRow.Versao = new byte[] { };
                    cadRow.isDeleted = 0;
                    GisaDataSetHelper.GetInstance().ControloAutDicionario.AddControloAutDicionarioRow(cadRow);
                }

                if (ca.TipoNoticiaAut == TipoNoticiaAut.EntidadeProdutora && caRow.RowState == DataRowState.Added)
                {
                    var nRow = GisaDataSetHelper.GetInstance().Nivel.NewNivelRow();
                    nRow.IDTipoNivel = TipoNivel.ESTRUTURAL;
                    nRow.Codigo = ((Model.EntidadesInternas.Produtor)ca).Codigo;
                    //if (nRow.Codigo.Length > 50)
                    //    nRow.Codigo = nRow.Codigo.Substring(0, 49);//
                    nRow.CatCode = "CA";
                    nRow.Versao = new byte[] { };
                    nRow.isDeleted = 0;
                    GisaDataSetHelper.GetInstance().Nivel.AddNivelRow(nRow);

                    var frdRow = GisaDataSetHelper.GetInstance().FRDBase.NewFRDBaseRow();
                    frdRow.NivelRow = nRow;
                    frdRow.IDTipoFRDBase = (long)TipoFRDBase.FRDOIRecolha;
                    frdRow.Versao = new byte[] { };
                    frdRow.isDeleted = 0;
                    GisaDataSetHelper.GetInstance().FRDBase.AddFRDBaseRow(frdRow);

                    var ncaRow = GisaDataSetHelper.GetInstance().NivelControloAut.NewNivelControloAutRow();
                    ncaRow.ControloAutRow = caRow;
                    ncaRow.NivelRow = nRow;
                    ncaRow.Versao = new byte[] { };
                    ncaRow.isDeleted = 0;
                    GisaDataSetHelper.GetInstance().NivelControloAut.AddNivelControloAutRow(ncaRow);
                }

                ca.Id = caRow.ID;
            }
            else
                caRow = GisaDataSetHelper.GetInstance().ControloAut.Rows.Cast<GISADataset.ControloAutRow>().Where(r => r.ID == ca.Id).Single();

            rows[correspondenciaRA.EntidadeInterna] = caRow;
            return caRow;
        }

        internal static void MapDocCARelationToDataRows(CorrespondenciaRAs cRA, GISADataset.FRDBaseRow frdRow, GISADataset.ControloAutRow caRow)
        {
            var ca = (RegistoAutoridadeInterno)cRA.EntidadeInterna;

            if (cRA.EstadoRelacaoPorOpcao[cRA.TipoOpcao] == TipoEstado.SemAlteracoes) return;

            var oldIndexFRDCARow = GisaDataSetHelper.GetInstance().IndexFRDCA.Cast<GISADataset.IndexFRDCARow>().SingleOrDefault(r => r.IDControloAut == caRow.ID && r.IDFRDBase == frdRow.ID);
            if (oldIndexFRDCARow != null) return;

            if (ca.TipoNoticiaAut == TipoNoticiaAut.TipologiaInformacional)
                GisaDataSetHelper.GetInstance().IndexFRDCA.Cast<GISADataset.IndexFRDCARow>()
                    .Where(r => r.IDFRDBase == frdRow.ID && r.ControloAutRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.TipologiaInformacional)
                    .ToList()
                    .ForEach(row => row.Delete());

            var indexFRDCARow = GisaDataSetHelper.GetInstance().IndexFRDCA.NewIndexFRDCARow();
            indexFRDCARow.ControloAutRow = caRow;
            indexFRDCARow.FRDBaseRow = frdRow;
            if (caRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.TipologiaInformacional)
                indexFRDCARow.Selector = -1;
            indexFRDCARow.Versao = new byte[] { };
            indexFRDCARow.isDeleted = 0;
            GisaDataSetHelper.GetInstance().IndexFRDCA.AddIndexFRDCARow(indexFRDCARow);
        }
        #endregion

        #region Save
        internal static string SaveCorrespondencias(List<CorrespondenciaDocs> correspondencias, ref CorrespondenciaDocs lastCorrespondencia)
        {
            lastCorrespondencia = null;
            var reportIntegracao = new StringBuilder();
            Dictionary<Entidade, DataRow> rows = new Dictionary<Entidade, DataRow>();
            foreach (var correspondencia in correspondencias)
            {
                var reportCurrentCorrespondencia = string.Empty;
                MapEntidadeExternaToDataRows(correspondencia, rows);

                var dg = correspondencia.EntidadeInterna as DocumentoGisa;

                if (dg != null)
                {
                    MapDocumentoGisaToDataRows(correspondencia, rows);
                    MapCorrespondencias(correspondencia, rows);

                    //reportCurrentCorrespondencia = RelatorioIntegracao(correspondencia, rows);
                }

                if (!Save(correspondencia, rows))
                {
                    lastCorrespondencia = correspondencia;
                    break;
                }

                StateCommit(correspondencia);
                //if (reportCurrentCorrespondencia != null && reportCurrentCorrespondencia.Length > 0)
                //    reportIntegracao.AppendLine(reportCurrentCorrespondencia);
            }
            PersistencyHelper.cleanDeletedData();
            return reportIntegracao.ToString();
        }

        private static void StateCommit(CorrespondenciaDocs correspondencia)
        {
            var dg = correspondencia.EntidadeInterna as DocumentoGisa;
            if (dg == null) return;

            correspondencia.EntidadeInterna.Estado = TipoEstado.SemAlteracoes;
            dg.CommitStateProperties();
            correspondencia.AllCorrespondenciasRAs.ForEach(c => { 
                if (c.TipoOpcao != TipoOpcao.Ignorar)
                    c.EntidadeInterna.Estado = TipoEstado.SemAlteracoes;
                if (c.EstadoRelacaoPorOpcao.ContainsKey(c.TipoOpcao))
                    c.EstadoRelacaoPorOpcao[c.TipoOpcao] = TipoEstado.SemAlteracoes; 
            });
        }

        private static string RelatorioIntegracao(CorrespondenciaDocs correspondencia, Dictionary<Entidade, DataRow> rows)
        {
            StringBuilder report = new StringBuilder();
            report.AppendLine(correspondencia.EntidadeExterna.IDExterno);
            report.AppendLine("Correspondências entre entidades DocInPorto e entidades Gisa:");

            report.AppendLine(AppendCorrespondencia(correspondencia, rows));

            //if (correspondencia.correspondenciaDocsCompostos != null)
            //    report.AppendLine(AppendCorrespondencia(correspondencia.correspondenciaDocsCompostos, rows));

            correspondencia.CorrespondenciasRAs.ForEach(c => report.AppendLine(AppendCorrespondencia(c, rows)));

            var nRow = rows[correspondencia.EntidadeInterna] as GISADataset.NivelRow;
            var rhRow = GisaDataSetHelper.GetInstance().RelacaoHierarquica.Cast<GISADataset.RelacaoHierarquicaRow>().Where(rh => rh.RowState == DataRowState.Added && rh.ID == nRow.ID).ToList();
            if (rhRow.Count > 0)
            {
                report.AppendLine("Relações hierárquicas superior:");
                report.AppendLine(AppendRelacaoHierarquica(rhRow[0]));
            }
            
            var indexFRDCARows = nRow.GetFRDBaseRows()[0].GetIndexFRDCARows().Where(r => r.RowState == DataRowState.Added).ToList();
            if (indexFRDCARows.Count > 0)
            {
                report.AppendLine("Associações novas entre documentos e controlos de autoridade:");
                indexFRDCARows.ForEach(r => report.AppendLine(AppendAssociacao(r)));
            }

            report.AppendLine(System.Environment.NewLine);

            return report.ToString();
        }

        private static string AppendCorrespondencia(CorrespondenciaDocs correspondencia, Dictionary<Entidade, DataRow> rows)
        {
            return string.Format("* {0}{2} -> {1}{3}",
                correspondencia.EntidadeExterna.IDExterno,
                correspondencia.EntidadeInterna.Titulo,
                Novo(rows[correspondencia.EntidadeExterna]),
                Novo(rows[correspondencia.EntidadeInterna]));
        }

        private static string AppendCorrespondencia(CorrespondenciaRAs correspondencia, Dictionary<Entidade, DataRow> rows)
        {
            var titulo = ((RegistoAutoridadeExterno)correspondencia.EntidadeExterna).Titulo;
            return string.Format("* {0}{2} -> {1}{3}",
                titulo != null && titulo.Length > 0 ? titulo : correspondencia.EntidadeExterna.IDExterno,
                correspondencia.EntidadeInterna.Titulo,
                Novo(rows[correspondencia.EntidadeExterna]),
                Novo(rows[correspondencia.EntidadeInterna]));
        }

        private static string AppendRelacaoHierarquica(GISADataset.RelacaoHierarquicaRow rhRow)
        {
            var nRowSuperior = rhRow.NivelRowByNivelRelacaoHierarquicaUpper;
            string nRowSuperiorTitulo = string.Empty;

            if (nRowSuperior.IDTipoNivel == TipoNivel.ESTRUTURAL)
                nRowSuperiorTitulo = nRowSuperior.GetNivelControloAutRows()[0].ControloAutRow
                    .GetControloAutDicionarioRows()
                    .Where(cad => cad.IDTipoControloAutForma == (int)TipoControloAutForma.FormaAutorizada)
                    .Single().DicionarioRow.Termo;
            else
                nRowSuperiorTitulo = nRowSuperior.GetNivelDesignadoRows()[0].Designacao;


            return string.Format("* {0}", nRowSuperiorTitulo);
        }

        private static string AppendAssociacao(GISADataset.IndexFRDCARow indexFRDCARow)
        {
            var tituloCA = indexFRDCARow.ControloAutRow
                    .GetControloAutDicionarioRows()
                    .Where(cad => cad.IDTipoControloAutForma == (int)TipoControloAutForma.FormaAutorizada)
                    .Single().DicionarioRow.Termo;

            return string.Format("* {0}", tituloCA);
        }

        private static string Novo(DataRow row)
        {
            return row.RowState == DataRowState.Added ? "(Novo)" : "";
        }

        internal static void MapCorrespondencias(CorrespondenciaDocs correspondenciaDoc, Dictionary<Entidade, DataRow> rows)
        {
            var data = System.Convert.ToDateTime(DateTime.Now.GetDateTimeFormats('G')[0]); // retirar os milisegundos ao tempo
            var relNivelRow = GisaDataSetHelper.GetInstance().Integ_RelacaoExternaNivel.NewInteg_RelacaoExternaNivelRow();
            relNivelRow.Data = correspondenciaDoc.EntidadeExterna.Timestamp;
            relNivelRow.DataIntegracao = data;
            relNivelRow.Integ_EntidadeExternaRow = (GISADataset.Integ_EntidadeExternaRow)rows[correspondenciaDoc.EntidadeExterna];
            relNivelRow.NivelRow = (GISADataset.NivelRow)rows[correspondenciaDoc.EntidadeInterna];
            relNivelRow.Versao = new byte[] { };
            relNivelRow.isDeleted = 0;

            var relsDoc = GisaDataSetHelper.GetInstance().Integ_RelacaoExternaNivel.Cast<GISADataset.Integ_RelacaoExternaNivelRow>().
                Where(r => r.IDNivel == ((GISADataset.NivelRow)rows[correspondenciaDoc.EntidadeInterna]).ID &&
                    r.IDEntidadeExterna == ((GISADataset.Integ_EntidadeExternaRow)rows[correspondenciaDoc.EntidadeExterna]).ID &&
                    r.Data == relNivelRow.Data).SingleOrDefault();

            if (relsDoc == null)
                GisaDataSetHelper.GetInstance().Integ_RelacaoExternaNivel.AddInteg_RelacaoExternaNivelRow(relNivelRow);

            foreach (var correspondenciaRA in correspondenciaDoc.CorrespondenciasRAs)
            {
                var relControloAutRow = GisaDataSetHelper.GetInstance().Integ_RelacaoExternaControloAut.NewInteg_RelacaoExternaControloAutRow();
                relControloAutRow.Data = correspondenciaRA.EntidadeExterna.Timestamp;
                relControloAutRow.DataIntegracao = data;
                relControloAutRow.Integ_EntidadeExternaRow = (GISADataset.Integ_EntidadeExternaRow)rows[correspondenciaRA.EntidadeExterna];
                relControloAutRow.ControloAutRow = (GISADataset.ControloAutRow)rows[correspondenciaRA.EntidadeInterna];
                relControloAutRow.Versao = new byte[] { };
                relControloAutRow.isDeleted = 0;
                
                var relsRA = GisaDataSetHelper.GetInstance().Integ_RelacaoExternaControloAut.Cast<GISADataset.Integ_RelacaoExternaControloAutRow>().
                    Where(r => r.IDControloAut == ((GISADataset.ControloAutRow)rows[correspondenciaRA.EntidadeInterna]).ID &&
                        r.IDEntidadeExterna == ((GISADataset.Integ_EntidadeExternaRow)rows[correspondenciaRA.EntidadeExterna]).ID &&
                        r.Data == relControloAutRow.Data).SingleOrDefault();

                if (relsRA == null)
                    GisaDataSetHelper.GetInstance().Integ_RelacaoExternaControloAut.AddInteg_RelacaoExternaControloAutRow(relControloAutRow);
            }
        }

        internal static void MapEntidadeExternaToDataRows(CorrespondenciaDocs correspondenciaDoc, Dictionary<Entidade, DataRow> rows)
        {
            // mapear documento externo
            var docExt = (DocumentoExterno)correspondenciaDoc.EntidadeExterna;
            rows[correspondenciaDoc.EntidadeExterna] = MapEntidadeExternaToDataRows(docExt, (int)docExt.Sistema, (int)docExt.Tipo);

            // mapear registos de autoridade externos desse documento
            foreach (var correspondenciaRA in correspondenciaDoc.CorrespondenciasRAs)
            {
                var entidadeExterna = (RegistoAutoridadeExterno)correspondenciaRA.EntidadeExterna;
                rows[correspondenciaRA.EntidadeExterna] = MapEntidadeExternaToDataRows(correspondenciaRA.EntidadeExterna, (int)entidadeExterna.Sistema, (int)entidadeExterna.Tipo);
            }
        }

        internal static DataRow MapEntidadeExternaToDataRows(EntidadeExterna ee, int IDSistema, int IDTipoEntidade)
        {
            // impedir que uma entidade externa seja mapeada mais do que uma vez
            var entExtRow = GisaDataSetHelper.GetInstance().Integ_EntidadeExterna.Cast<GISADataset.Integ_EntidadeExternaRow>().
                Where(r => r.IDExterno.Equals(ee.IDExterno) && r.IDSistema == IDSistema
                    && r.IDTipoEntidade == IDTipoEntidade).SingleOrDefault();

            if (entExtRow == null)
            {
                entExtRow = GisaDataSetHelper.GetInstance().Integ_EntidadeExterna.NewInteg_EntidadeExternaRow();
                entExtRow.IDExterno = ee.IDExterno;
                entExtRow.IDSistema = IDSistema;
                entExtRow.IDTipoEntidade = IDTipoEntidade;
                entExtRow.Versao = new byte[] { };
                entExtRow.isDeleted = 0;
                GisaDataSetHelper.GetInstance().Integ_EntidadeExterna.AddInteg_EntidadeExternaRow(entExtRow);
            }

            return entExtRow;
        }

        internal static bool Save(CorrespondenciaDocs correspondenciaDoc, Dictionary<Entidade, DataRow> rows)
        {
            var dg = (DocumentoGisa)correspondenciaDoc.EntidadeInterna;

            // listas de IDs tanto dos documentos como dos controlos de autoridade criados para serem usados na
            // actualização dos índices de pesquisa como também na atribuição de permissões (somente no caso
            // dos documentos)
            List<long> nIDs = new List<long>();
            List<string> produtoresID = new List<string>();
            List<string> assuntosID = new List<string>();
            List<string> tipologiasID = new List<string>();

            // documentos que vão ser-lhes actualizadas as permissões
            var nRows = new List<GISADataset.NivelRow>();
            nRows = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().Where(r => r.RowState == DataRowState.Added).ToList();

            // atribuição de permissões aos documentos novos
            nRows.ForEach(r => {
                var rhRow = r.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().FirstOrDefault();
                if (rhRow == null)
                    PermissoesHelper.AddNewNivelGrantPermissions(r);
                else
                    PermissoesHelper.AddNewNivelGrantPermissions(r, rhRow.NivelRowByNivelRelacaoHierarquicaUpper);
            });

            // manter uma lista de niveis e controlos de autoridade criados para que seja possível actualizar o 
            // índice de pesquisa
            var documentoRows = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().Where(r => r.RowState == DataRowState.Added && r.IDTipoNivel == (long)TipoNivel.DOCUMENTAL).ToList();
            var produtorRows = GisaDataSetHelper.GetInstance().ControloAut.Cast<GISADataset.ControloAutRow>().Where(r => r.IDTipoNoticiaAut == (int)TipoNoticiaAut.EntidadeProdutora && r.RowState == DataRowState.Added).ToList();
            var assuntoRows = GisaDataSetHelper.GetInstance().ControloAut.Cast<GISADataset.ControloAutRow>().Where(r => (r.IDTipoNoticiaAut == (int)TipoNoticiaAut.Onomastico || r.IDTipoNoticiaAut == (int)TipoNoticiaAut.Ideografico || r.IDTipoNoticiaAut == (int)TipoNoticiaAut.ToponimicoGeografico) && r.RowState == DataRowState.Added).ToList();
            var tipologiaRows = GisaDataSetHelper.GetInstance().ControloAut.Cast<GISADataset.ControloAutRow>().Where(r => r.IDTipoNoticiaAut == (int)TipoNoticiaAut.TipologiaInformacional && r.RowState == DataRowState.Added).ToList();
            
            // guardar nas listas os IDs dos documentos e dos controlos de autoridade que foram atribuidos na
            // base de dados durante o save e criar registos de adição/modificação dos niveis e dos 
            // controlos de autoridade
            GISADataset.TrusteeUserRow tuOperator = SessionHelper.GetGisaPrincipal().TrusteeUserOperator;
            DateTime data = DateTime.Now;
            GISADataset.TrusteeUserRow tuAuthor = null;

            var docRow = rows[correspondenciaDoc.EntidadeInterna];
            var docToReg = default(GISADataset.NivelRow);
            if (Concorrencia.WasRecordModified(docRow) || Concorrencia.WasRecordModified(((GISADataset.NivelRow)docRow).GetFRDBaseRows().Single()))
                docToReg = docRow as GISADataset.NivelRow;

            if (SessionHelper.GetGisaPrincipal().TrusteeUserAuthor != null && !(SessionHelper.GetGisaPrincipal().TrusteeUserAuthor.RowState == DataRowState.Detached))
                tuAuthor = SessionHelper.GetGisaPrincipal().TrusteeUserAuthor;

            // VALIDAÇÕES


            //validar documento novo
            PersistencyHelper.ValidaIntegDocExtPreSaveArguments psArgs = new PersistencyHelper.ValidaIntegDocExtPreSaveArguments();
            PersistencyHelper.ValidaIntegDocExtPreConcArguments pcArgs = new PersistencyHelper.ValidaIntegDocExtPreConcArguments();
            
            var pcArgsLst = new List<PersistencyHelper.ValidateNivelAddAndAssocNewUFPreConcArguments>();
            var psArgsNivelLst = new List<PersistencyHelper.SetNewCodigosPreSaveArguments>();
            foreach (var nRow in documentoRows)
            {
                var pcArgsNewNivel = new PersistencyHelper.ValidateNivelAddAndAssocNewUFPreConcArguments();
                var psArgsNivel = new PersistencyHelper.SetNewCodigosPreSaveArguments();
                var pcArgsNivelUniqueCode = new PersistencyHelper.EnsureUniqueCodigoNivelPreConcArguments();

                // dados que serão usados no delegate responsável pela criação do nível documental
                var rhRow = nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().Single();
                pcArgsNivelUniqueCode.nRowID = nRow.ID;
                pcArgsNivelUniqueCode.ndRowID = nRow.GetNivelDesignadoRows().Single().ID;
                pcArgsNivelUniqueCode.rhRowID = nRow.ID;
                pcArgsNivelUniqueCode.rhRowIDUpper = rhRow.IDUpper;
                pcArgsNivelUniqueCode.frdBaseID = nRow.GetFRDBaseRows().Single().ID;
                pcArgsNivelUniqueCode.testOnlyWithinNivel = true;

                pcArgsNewNivel.IDTipoNivelRelacionado = rhRow.IDTipoNivelRelacionado;
                pcArgsNewNivel.argsNivel = pcArgsNivelUniqueCode;

                psArgsNivel.createNewNivelCodigo = false;
                psArgsNivel.createNewUFCodigo = false;
                psArgsNivel.setNewCodigo = rhRow.IDTipoNivelRelacionado == TipoNivelRelacionado.SD;
                psArgsNivel.argsNivelDocSimples = NiveisHelper.AddNivelDocumentoSimplesWithDelegateArgs(nRow.GetNivelDesignadoRows().Single(), rhRow.IDUpper, rhRow.IDTipoNivelRelacionado);

                pcArgsLst.Add(pcArgsNewNivel);
                psArgsNivelLst.Add(psArgsNivel);
            }

            if (documentoRows.Count == 0 && dg != null)
            {
                var rhRowOld = GisaDataSetHelper.GetInstance().RelacaoHierarquica.Cast<GISADataset.RelacaoHierarquicaRow>().SingleOrDefault(r => r.RowState == DataRowState.Deleted);
                var rhRowNew = GisaDataSetHelper.GetInstance().RelacaoHierarquica.Cast<GISADataset.RelacaoHierarquicaRow>().SingleOrDefault(r => r.RowState == DataRowState.Added);
                if (rhRowOld != null && rhRowNew != null)
                {
                    var nRow = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().Single(r => r.ID == dg.Id);
                    var pcArgsNewNivel = new PersistencyHelper.ValidateNivelAddAndAssocNewUFPreConcArguments();
                    var psArgsNivel = new PersistencyHelper.SetNewCodigosPreSaveArguments();
                    var pcArgsNivelUniqueCode = new PersistencyHelper.EnsureUniqueCodigoNivelPreConcArguments();

                    pcArgsNewNivel.argsNivel = pcArgsNivelUniqueCode;

                    // dados que serão usados no delegate responsável pela criação do nível documental
                    pcArgsNivelUniqueCode.nRowID = nRow.ID;
                    pcArgsNivelUniqueCode.ndRowID = nRow.GetNivelDesignadoRows()[0].ID;
                    pcArgsNivelUniqueCode.rhRowID = nRow.ID;
                    pcArgsNivelUniqueCode.rhRowIDUpper = nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].IDUpper;
                    pcArgsNivelUniqueCode.frdBaseID = nRow.GetFRDBaseRows()[0].ID;
                    pcArgsNivelUniqueCode.testOnlyWithinNivel = true;

                    pcArgsNewNivel.IDTipoNivelRelacionado = TipoNivelRelacionado.D;

                    psArgsNivel.createNewNivelCodigo = false;
                    psArgsNivel.createNewUFCodigo = false;

                    pcArgsLst.Add(pcArgsNewNivel);
                    psArgsNivelLst.Add(psArgsNivel);
                }
            }

            pcArgs.newDocsList = pcArgsLst;
            psArgs.newDocArgs = psArgsNivelLst;

            // validar controlo de autoridade novo
            List<PersistencyHelper.NewControloAutPreSaveArguments> newControloAutArgs = new List<PersistencyHelper.NewControloAutPreSaveArguments>();
            foreach (var caRow in GisaDataSetHelper.GetInstance().ControloAut.Cast<GISADataset.ControloAutRow>().Where(c => c.RowState == DataRowState.Added))
            {
                PersistencyHelper.NewControloAutPreSaveArguments args = new PersistencyHelper.NewControloAutPreSaveArguments();
                if (caRow.TipoNoticiaAutRow.ID == Convert.ToInt64(TipoNoticiaAut.EntidadeProdutora))
                {
                    var nRow = caRow.GetNivelControloAutRows()[0].NivelRow;
                    args.nID = nRow.ID;
                    args.epCodigo = nRow.Codigo;
                }

                args.caID = caRow.ID;
                args.dID = caRow.GetControloAutDicionarioRows()[0].DicionarioRow.ID;
                args.dTermo = caRow.GetControloAutDicionarioRows()[0].DicionarioRow.Termo.Replace("'", "''");
                args.cadIDControloAut = caRow.GetControloAutDicionarioRows()[0].IDControloAut;
                args.cadIDDicionario = caRow.GetControloAutDicionarioRows()[0].IDDicionario;
                args.cadIDTipoControloAutForma = caRow.GetControloAutDicionarioRows()[0].IDTipoControloAutForma;

                newControloAutArgs.Add(args);
            }

            psArgs.newControloAutArgs = newControloAutArgs;

            // Atribuir permissões aos níveis criados
            PostSaveAction postSaveAction = new PostSaveAction();
            PersistencyHelper.UpdatePermissionsPostSaveArguments argsPostSave = new PersistencyHelper.UpdatePermissionsPostSaveArguments();
            postSaveAction.args = argsPostSave;

            postSaveAction.postSaveDelegate = delegate(PersistencyHelper.PostSaveArguments postSaveArgs)
            {
                
                if (!argsPostSave.cancelAction)
                {
                    if (docToReg != null)
                    {
                        var regNvl = GISA.Model.RecordRegisterHelper.CreateFRDBaseDataDeDescricaoRow(docToReg.GetFRDBaseRows()[0], tuOperator, tuAuthor, data);
                        GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao.AddFRDBaseDataDeDescricaoRow(regNvl);
                        PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao, new DataRow[] { regNvl }, postSaveArgs.tran);
                    }

                    var regCa = produtorRows.Select(caRow => GISA.Model.RecordRegisterHelper.CreateControlAutDataDeDescricaoRow(caRow, tuOperator, tuAuthor, data)).ToList();
                    regCa.AddRange(assuntoRows.Select(caRow => GISA.Model.RecordRegisterHelper.CreateControlAutDataDeDescricaoRow(caRow, tuOperator, tuAuthor, data)));
                    regCa.AddRange(tipologiaRows.Select(caRow => GISA.Model.RecordRegisterHelper.CreateControlAutDataDeDescricaoRow(caRow, tuOperator, tuAuthor, data)));
                    regCa.ToList().ForEach(r => GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao.AddControloAutDataDeDescricaoRow(r));
                    PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao, regCa.ToArray(), postSaveArgs.tran);
                }
            };

            PersistencyHelper.SaveResult saveResult =
                PersistencyHelper.save(ValidaIntegDocExt, pcArgs, ValidaIntegDocExt, psArgs, postSaveAction, true);

            if (saveResult == PersistencyHelper.SaveResult.unsuccessful)
            {
                var errorDetailsLst = pcArgsLst.Select(args => ((PersistencyHelper.EnsureUniqueCodigoNivelPreConcArguments)args.argsNivel).message)
                    .Where(msg => msg != null && msg.Length > 0).ToList();

                var errorDetails = new StringBuilder();
                errorDetailsLst.ForEach(str => errorDetails.AppendLine(str));


                // mostrar mensagem
                DialogResult dResult = MessageBox.Show(
                    errorDetails + System.Environment.NewLine +
                    "A gravação vai ser abortada.",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
            else if (saveResult == PersistencyHelper.SaveResult.cancel)
            {
                GisaDataSetHelper.GetInstance().RejectChanges();
                return false;
            }
            else
            {
                var err = psArgs.newControloAutArgs.FirstOrDefault(e => !e.successTermo || (e.epCodigo != null && !e.successCodigo));
                if (err != null)
                {
                    var errStr = (err.epCodigo != null && !err.successCodigo) ?
                                    string.Format("O código {0} já está a ser utilizado por outra entidade produtora.", err.epCodigo) :
                                    string.Format("O termo {0} já está a ser utilizado por outro controlo de autoridade.", err.dTermo);

                    // mostrar mensagem
                    DialogResult dResult = MessageBox.Show(
                        errStr + System.Environment.NewLine +
                        "A gravação vai ser abortada.",
                        "Erro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return false;
                }
            }

            // actualizar os IDs das entidades internas
            var entidadesInternas = rows.Keys.ToList().OfType<EntidadeInterna>().ToList();
            //var entidadesExternas = rows.Keys.ToList().OfType<EntidadeExterna>().ToList();
            entidadesInternas.ForEach(ent => ent.Id = (long)rows[ent]["ID"]);
            //entidadesExternas.ForEach(ent => ent.IDExterno = (long)rows[ent]["ID"]);

            // actualizar índices de pesquisa
            foreach (GISADataset.ControloAutRow caRow in produtorRows)
                produtoresID.Add(caRow.ID.ToString());

            foreach (GISADataset.ControloAutRow caRow in assuntoRows)
                assuntosID.Add(caRow.ID.ToString());

            foreach (GISADataset.ControloAutRow caRow in tipologiaRows)
                tipologiasID.Add(caRow.ID.ToString());

            var niveisDocumentais = nRows.Select(r => r.ID.ToString()).ToList();
            GISA.Search.Updater.updateNivelDocumental(niveisDocumentais);
            GISA.Search.Updater.updateNivelDocumentalComProdutores(niveisDocumentais);
            GISA.Search.Updater.updateProdutor(produtoresID);
            GISA.Search.Updater.updateAssunto(assuntosID);
            GISA.Search.Updater.updateTipologia(tipologiasID);

            return true;
        }

        public static void ValidaIntegDocExt(PersistencyHelper.PreConcArguments args)
        {
            GisaDataSetHelper.GetInstance().EnforceConstraints = false;
            foreach (var pcArg in ((PersistencyHelper.ValidaIntegDocExtPreConcArguments)args).newDocsList)
            {
                pcArg.tran = args.tran;
                pcArg.gisaBackup = args.gisaBackup;
                pcArg.continueSave = args.continueSave;
                DelegatesHelper.ValidateNivelAddAndAssocNewUF(pcArg);

                if (!pcArg.continueSave)
                {
                    GisaDataSetHelper.GetInstance().RejectChanges();
                    args.continueSave = false;
                    break;
                }
            }
            GisaDataSetHelper.GetInstance().EnforceConstraints = true;
        }

        public static void ValidaIntegDocExt(PersistencyHelper.PreSaveArguments args)
        {
            var psArgs = args as PersistencyHelper.ValidaIntegDocExtPreSaveArguments;
            foreach (var newDocArg in psArgs.newDocArgs)
            {
                newDocArg.tran = args.tran;
                DelegatesHelper.SetNewCodigos(newDocArg);
            }

            foreach (PersistencyHelper.PreSaveArguments newCAArgs in psArgs.newControloAutArgs)
            {
                newCAArgs.tran = args.tran;
                DelegatesHelper.validateCANewTermo(newCAArgs);

                var a = newCAArgs as PersistencyHelper.NewControloAutPreSaveArguments;
                var caRow = GisaDataSetHelper.GetInstance().ControloAut.Cast<GISADataset.ControloAutRow>().SingleOrDefault(ca => ca.ID == a.caID);

                if (caRow == null || caRow.RowState == DataRowState.Detached || (caRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.EntidadeProdutora && !a.successCodigo) || !a.successTermo)
                {
                    GisaDataSetHelper.GetInstance().RejectChanges();
                    break;
                }
            }
        }

        private static string LongToString(long id)
        {
            return id.ToString();
        }

        #endregion

        public static void FillDocumentoGisa(DocumentoSimples de, DocumentoGisa di, TipoOpcao opcao)
        {
            di.NumeroEspecifico.Escolhas[opcao] = de.NumeroEspecifico;
            di.Agrupador.Escolhas[opcao] = de.Processo.NUP + "; " + de.NUD;

            di.DataCriacao.Escolhas[opcao] = new DataIncompleta(de.DataCriacao, de.DataCriacao);
            di.DataCriacao.EstadoRelacaoPorOpcao[opcao] = di.DataCriacao.Escolhas.ContainsKey(TipoOpcao.Original) ? TipoEstado.Editar : TipoEstado.Novo;

            string rest = (de.NumeroEspecifico == null || de.NumeroEspecifico.Length == 0) ? string.Empty : " " + de.NumeroEspecifico;
            if (di.Id <= 0)
                di.TituloDoc.Escolhas[opcao] = de.Tipologia.Titulo + (de.Processo.Tipologia.Titulo.Equals(de.Assunto) ? "" : " : " + de.Assunto);

            di.Notas.Escolhas[opcao] = de.Notas;
            
            if (de.Assunto != null && de.Assunto.Length > 0)
                di.Assunto.Escolhas[opcao] = "Assunto: " + de.Assunto;

            string CodPostalLoc = null;

            if (de.CodPostal != null && de.CodPostal.Length > 0)
                CodPostalLoc += "Código postal: " + de.CodPostal + "; ";
            if (de.Localidade != null && de.Localidade.Length > 0)
                CodPostalLoc += "Localidade: " + de.Localidade + "; ";

            if (CodPostalLoc != null) di.CodPostalLoc.Escolhas[opcao] = CodPostalLoc;

            string NumLocalRefPred = null;

            if (de.NumPolicia != null && de.NumPolicia.Length > 0)
                NumLocalRefPred += "Número polícia: " + de.NumPolicia + "; ";
            if (de.Local != null && de.Local.Length > 0)
                NumLocalRefPred += "Local: " + de.Local + "; ";
            if (de.RefPredial != null && de.RefPredial.Length > 0)
                NumLocalRefPred += "Referência predial: " + de.RefPredial + "; ";

            if (NumLocalRefPred != null) di.NumLocalRefPred.Escolhas[opcao] = NumLocalRefPred;

            di.ObjDigitais.AddRange(
                de.Conteudos.Select(
                    c => new Model.EntidadesInternas.DocumentoGisa.ObjectosDigitais()
                    {
                        NomeFicheiro = c.Ficheiro,
                        NUD = de.NUD,
                        TipoDescricao = c.TipoDescricao,
                        Tipo = (c.Ficheiro == null || c.Ficheiro.Length == 0 ?
                                (int)ResourceAccessType.DICConteudo :
                                (int)ResourceAccessType.DICAnexo)
                    }));
        }

        public static void FillDocumentoGisa(DocumentoAnexo de, DocumentoGisa di, TipoOpcao opcao)
        {
            if (di.Id <= 0)
                di.TituloDoc.Escolhas[opcao] = de.TipoDescricao + (de.Processo.Tipologia.Equals(de.Assunto) ? "" : " : " + de.Assunto);
            di.Notas.Escolhas[opcao] = de.Descricao;
            di.Agrupador.Escolhas[opcao] = de.Processo.NUP + "; " + de.DocumentoSimples + "-" + de.NUD.Split('/').First();

            di.ObjDigitais.Add(new Model.EntidadesInternas.DocumentoGisa.ObjectosDigitais() { NomeFicheiro = de.Conteudos[0].Ficheiro, NUD = de.NUD, Tipo = (int)ResourceAccessType.DICAnexo, TipoDescricao = de.Conteudos[0].TipoDescricao });
        }

        public static void FillDocumentoGisa(DocumentoComposto de, DocumentoGisa di, TipoOpcao opcao)
        {
            di.DataCriacao.Escolhas[opcao] = new DataIncompleta(de.DataInicio, de.DataFim);
            di.DataCriacao.EstadoRelacaoPorOpcao[opcao] = di.DataCriacao.Escolhas.ContainsKey(TipoOpcao.Original) ? TipoEstado.Editar : TipoEstado.Novo;

            if (di.Id <= 0)
                di.TituloDoc.Escolhas[opcao] = de.Tipologia == null ? de.NUP : de.Tipologia.Titulo + " : " + de.NUP;

            di.Confidencialidade.Escolhas[opcao] = de.Confidencialidade;

            di.Requerentes.AddRange(de.RequerentesOuProprietariosIniciais.Select(r => new PropriedadeDocumentoGisaTemplate<string>() { Escolhas = new Dictionary<TipoOpcao, string>() { { opcao, r } } }).ToList());
            di.Averbamentos.AddRange(de.AverbamentosDeRequerenteOuProprietario.Select(a => new PropriedadeDocumentoGisaTemplate<string>() { Escolhas = new Dictionary<TipoOpcao, string>() { { opcao, a } } }).ToList());
        }

        internal static Dictionary<DocumentoSimples, DocumentoGisa> GetDocsCorrespondenciasAnteriores(List<DocumentoSimples> docsExternos, IDbConnection conn)
        {
            var result = new Dictionary<DocumentoSimples, DocumentoGisa>();
            var idsExternos = docsExternos.Select(d => new IntGestDocRule.EntidadeExterna { IDExterno = d.IDExterno, Sistema = (int)d.Sistema, TipoEntidade = (int)TipoEntidadeExterna.Documento }).ToList();
            var docsInfo = IntGestDocRule.Current.LoadDocsCorrespondenciasAnteriores(GisaDataSetHelper.GetInstance(), idsExternos, (int)TipoEntidadeExterna.Documento, conn);
            foreach (var docExterno in docsExternos)
            {
                var nivelRow = GisaDataSetHelper.GetInstance().Integ_RelacaoExternaNivel.Cast<GISADataset.Integ_RelacaoExternaNivelRow>()
                    .Where(r => r.Integ_EntidadeExternaRow.IDExterno.Equals(docExterno.IDExterno) 
                        && r.Integ_EntidadeExternaRow.IDSistema == (int)docExterno.Sistema 
                        && r.Integ_EntidadeExternaRow.IDTipoEntidade == (int)TipoEntidadeExterna.Documento)
                    .OrderByDescending(r => r.DataIntegracao)
                    .Select(row => row.NivelRow).FirstOrDefault();
                if (nivelRow != null)
                {
                    var docProds = docsInfo.Where(i => i.IDNivel == nivelRow.ID).Single().IDNivelProdutores;
                    var di = GetNewDocumentoGisa(nivelRow, docProds);
                    FillDocumentoGisa(docExterno, di, TipoOpcao.Sugerida);
                    result.Add(docExterno, di);
                }
            }

            return result; 
        }

        internal static Dictionary<DocumentoAnexo, DocumentoGisa> GetDocsCorrespondenciasAnteriores(List<DocumentoAnexo> docsExternos, IDbConnection conn)
        {
            var result = new Dictionary<DocumentoAnexo, DocumentoGisa>();
            var idsExternos = docsExternos.Select(d => new IntGestDocRule.EntidadeExterna { IDExterno = d.IDExterno, Sistema = (int)d.Sistema, TipoEntidade = (int)TipoEntidadeExterna.DocumentoAnexo }).ToList();
            var docsInfo = IntGestDocRule.Current.LoadDocsCorrespondenciasAnteriores(GisaDataSetHelper.GetInstance(), idsExternos, (int)TipoEntidadeExterna.DocumentoAnexo, conn);
            foreach (var docExterno in docsExternos)
            {
                var nivelRow = GisaDataSetHelper.GetInstance().Integ_RelacaoExternaNivel.Cast<GISADataset.Integ_RelacaoExternaNivelRow>()
                    .Where(r => r.Integ_EntidadeExternaRow.IDExterno.Equals(docExterno.IDExterno) 
                        && r.Integ_EntidadeExternaRow.IDSistema == (int)docExterno.Sistema 
                        && r.Integ_EntidadeExternaRow.IDTipoEntidade == (int)TipoEntidadeExterna.DocumentoAnexo)
                    .OrderByDescending(r => r.DataIntegracao)
                    .Select(row => row.NivelRow).FirstOrDefault();
                if (nivelRow != null)
                {
                    var docProds = docsInfo.Where(i => i.IDNivel == nivelRow.ID).Single().IDNivelProdutores;
                    var di = GetNewDocumentoAnexoGisa(nivelRow, docProds);
                    FillDocumentoGisa(docExterno, di, TipoOpcao.Sugerida);
                    result.Add(docExterno, di);
                }
            }

            return result;
        }        

        internal static Dictionary<DocumentoComposto, DocumentoGisa> GetDocsCorrespondenciasAnteriores(List<DocumentoComposto> docsExternos, IDbConnection conn)
        {
            var result = new Dictionary<DocumentoComposto, DocumentoGisa>();
            var idsExternos = docsExternos.Select(d => new IntGestDocRule.EntidadeExterna { IDExterno = d.IDExterno, Sistema = (int)d.Sistema, TipoEntidade = (int)TipoEntidadeExterna.DocumentoComposto }).ToList();
            var docsInfo = IntGestDocRule.Current.LoadDocsCorrespondenciasAnteriores(GisaDataSetHelper.GetInstance(), idsExternos, (int)TipoEntidadeExterna.DocumentoComposto, conn);
            foreach (var docExterno in docsExternos)
            {
                var nivelRow = GisaDataSetHelper.GetInstance().Integ_RelacaoExternaNivel.Cast<GISADataset.Integ_RelacaoExternaNivelRow>()
                    .Where(r => r.Integ_EntidadeExternaRow.IDExterno.Equals(docExterno.IDExterno) 
                        && r.Integ_EntidadeExternaRow.IDSistema == (int)docExterno.Sistema 
                        && r.Integ_EntidadeExternaRow.IDTipoEntidade == (int)TipoEntidadeExterna.DocumentoComposto)
                    .OrderByDescending(r => r.DataIntegracao)
                    .Select(row => row.NivelRow).FirstOrDefault();
                
                if (nivelRow != null)
                {
                    var docProds = docsInfo.Where(i => i.IDNivel == nivelRow.ID).Single().IDNivelProdutores;
                    var di = GetNewDocumentoCompostoGisa(nivelRow, docProds);
                    FillDocumentoGisa(docExterno, di, TipoOpcao.Sugerida);
                    result.Add(docExterno, di);
                }
            }

            return result;
        }

        internal static Dictionary<RegistoAutoridadeExterno, RegistoAutoridadeInterno> GetRAsCorrespondenciasAnteriores(List<RegistoAutoridadeExterno> raes, IDbConnection conn)
        {
            var result = new Dictionary<RegistoAutoridadeExterno, RegistoAutoridadeInterno>();
            var raesParamLst = raes.Select(ee => new IntGestDocRule.EntidadeExterna { IDExterno = ee.IDExterno, Sistema = (int)ee.Sistema, TipoEntidade = (int)ee.Tipo }).ToList();            
            IntGestDocRule.Current.LoadRAsCorrespondenciasAnteriores(GisaDataSetHelper.GetInstance(), raesParamLst, conn);
            foreach (var rae in raes)
            {
                var caRow = GisaDataSetHelper.GetInstance().Integ_RelacaoExternaControloAut.Cast<GISADataset.Integ_RelacaoExternaControloAutRow>()
                    .Where(r => r.Integ_EntidadeExternaRow.IDExterno.Equals(rae.IDExterno) 
                        && r.Integ_EntidadeExternaRow.IDSistema == (int)rae.Sistema 
                        && r.Integ_EntidadeExternaRow.IDTipoEntidade == (int)rae.Tipo)
                    .OrderByDescending(r => r.DataIntegracao)
                    .Select(row => row.ControloAutRow).FirstOrDefault();

                if (caRow != null)
                {
                    RegistoAutoridadeInterno rai;
                    rai = CreateRegistoAutoridadeInterno(caRow);
                    rai.Id = caRow.ID;
                    rai.Titulo = caRow.GetControloAutDicionarioRows().Where(cad => cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada).Single().DicionarioRow.Termo;
                    rai.Estado = TipoEstado.SemAlteracoes;
                    
                    result.Add(rae, rai);
                }
            }

            return result;
        }

        internal static Dictionary<DocumentoSimples, DocumentoGisa> GetDocsCorrespondenciasNovas(List<DocumentoSimples> docsExternos, IDbConnection conn)
        {
            var result = new Dictionary<DocumentoSimples, DocumentoGisa>();
            var docs = docsExternos.ToDictionary(d => d.IDExterno, d => d);
            var idsExternos = docsExternos.Select(d => d.IDExterno).ToList();
            var sugestoes = IntGestDocRule.Current.LoadDocsCorrespondenciasNovas(GisaDataSetHelper.GetInstance(), idsExternos, TipoNivelRelacionado.SD, conn);

            foreach (var sugestao in sugestoes)
            {
                var nivelRow = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().Where(r => r.ID == sugestao.Value.IDNivel).Single();
                var di = GetNewDocumentoGisa(nivelRow, sugestao.Value.IDNivelProdutores);
                FillDocumentoGisa(docs[sugestao.Key], di, TipoOpcao.Sugerida);
                result.Add(docs[sugestao.Key], di);
            }

            return result;
        }

        internal static Dictionary<DocumentoAnexo, DocumentoGisa> GetDocsCorrespondenciasNovas(List<DocumentoAnexo> docsExternos, IDbConnection conn)
        {
            var result = new Dictionary<DocumentoAnexo, DocumentoGisa>();
            var docs = docsExternos.ToDictionary(d => d.IDExterno, d => d);
            var idsExternos = docsExternos.Select(d => d.IDExterno).ToList();
            var sugestoes = IntGestDocRule.Current.LoadDocsCorrespondenciasNovas(GisaDataSetHelper.GetInstance(), idsExternos, TipoNivelRelacionado.SD, conn);

            foreach (var sugestao in sugestoes)
            {
                var nivelRow = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().Where(r => r.ID == sugestao.Value.IDNivel).Single();
                var di = GetNewDocumentoAnexoGisa(nivelRow, sugestao.Value.IDNivelProdutores);
                FillDocumentoGisa(docs[sugestao.Key], di, TipoOpcao.Sugerida);
                result.Add(docs[sugestao.Key], di);
            }

            return result;
        }

        internal static Dictionary<DocumentoComposto, DocumentoGisa> GetDocsCorrespondenciasNovas(List<DocumentoComposto> docsExternos, IDbConnection conn)
        {
            var result = new Dictionary<DocumentoComposto, DocumentoGisa>();
            var docs = docsExternos.ToDictionary(d => d.IDExterno, d => d);
            var idsExternos = docsExternos.Select(d => d.IDExterno).ToList();
            var sugestoes = IntGestDocRule.Current.LoadDocsCorrespondenciasNovas(GisaDataSetHelper.GetInstance(), idsExternos, TipoNivelRelacionado.D, conn);

            foreach (var sugestao in sugestoes)
            {
                var nivelRow = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().Where(r => r.ID == sugestao.Value.IDNivel).Single();
                var di = GetNewDocumentoCompostoGisa(nivelRow, sugestao.Value.IDNivelProdutores);
                FillDocumentoGisa(docs[sugestao.Key], di, TipoOpcao.Sugerida);
                result.Add(docs[sugestao.Key], di);
            }

            return result;
        }

        internal static Dictionary<RegistoAutoridadeExterno, RegistoAutoridadeInterno> GetRAsCorrespondenciasNovas(List<RegistoAutoridadeExterno> rasExternos, IDbConnection conn)
        {
            var result = new Dictionary<RegistoAutoridadeExterno, RegistoAutoridadeInterno>();

            // todos os registos de autoridade excepto os onomásticos
            long count = 0;
            var dicionarioRAEs = rasExternos.Where(rae => rae.GetType() != typeof(Model.EntidadesExternas.Onomastico)).ToDictionary(rae => count++, rae => rae);
            var dicionarioDAL_RAEs = dicionarioRAEs.Keys.ToDictionary(k => k, k => new IntGestDocRule.EntidadeExterna { IDExterno = dicionarioRAEs[k].IDExterno, Titulo = string.IsNullOrEmpty(dicionarioRAEs[k].Titulo) ? "" : dicionarioRAEs[k].Titulo, Sistema = (int)dicionarioRAEs[k].Sistema, TipoEntidade = (int)dicionarioRAEs[k].Tipo });
            var sugestoes = IntGestDocRule.Current.LoadRAsCorrespondenciasNovas(GisaDataSetHelper.GetInstance(), dicionarioDAL_RAEs, conn);

            foreach (var sugestao in sugestoes)
            {
                var caRow = GisaDataSetHelper.GetInstance().ControloAut.Cast<GISADataset.ControloAutRow>().Where(r => r.ID == sugestao.Value).Single();
                RegistoAutoridadeInterno rai = CreateRegistoAutoridadeInterno(caRow);
                rai.Estado = TipoEstado.SemAlteracoes;
                if (dicionarioRAEs.ContainsKey(sugestao.Key))
                {
                    var rae = dicionarioRAEs[sugestao.Key];
                    result.Add(rae, rai);
                }
                else
                {
                    Debug.WriteLine("key not found!!");
                    Debug.WriteLine(sugestao.Key.ToString());
                }
            }

            var aa = rasExternos.Where(rae => rae.GetType() != typeof(Model.EntidadesExternas.Onomastico));
            var dict = new Dictionary<IntGestDocRule.EntidadeExterna, RegistoAutoridadeExterno>();

            foreach (var ra in aa)
            {
                var ee = new IntGestDocRule.EntidadeExterna { IDExterno = ra.IDExterno, Titulo = ra.Titulo, Sistema = (int)ra.Sistema, TipoEntidade = (int)ra.Tipo };
                if (dict.ContainsKey(ee))
                    Console.WriteLine("");
                else
                    dict.Add(ee, ra);
            }

            // só os onomásticos
            // NOTA: a procura de correspondências para os onomásticos segue as regras seguintes:
            //      * se o Registo de Autoridade Externo tiver só o NIF ou o Título definidos procura-se por onomásticos que tenham esses valores
            //      * se tiver o Registo de Autoridade Externo tiver os dois, procura-se em primeiro lugar por um onomástico que tenha o NIF e só depois pelo Título. Se for encontrado o onomástico pelo só Título, o NIF deve ser acrescentado ao registo
            var dicionario_onomasticoRAEs = rasExternos.Where(rae => rae.GetType() == typeof(Model.EntidadesExternas.Onomastico)).Cast<Model.EntidadesExternas.Onomastico>().ToDictionary(rae => count++, rae => rae);
            var dicionario_onomasticoDAL_RAEs = dicionario_onomasticoRAEs.Keys
                .ToDictionary(k => k, k => new IntGestDocRule.EntidadeExterna { 
                    IDExterno = string.IsNullOrEmpty(dicionario_onomasticoRAEs[k].NIF) ? dicionario_onomasticoRAEs[k].Titulo : dicionario_onomasticoRAEs[k].NIF, 
                    Titulo = !string.IsNullOrEmpty(dicionario_onomasticoRAEs[k].NIF) && !string.IsNullOrEmpty(dicionario_onomasticoRAEs[k].Titulo) ? dicionario_onomasticoRAEs[k].Titulo : "", 
                    Sistema = (int)dicionario_onomasticoRAEs[k].Sistema, TipoEntidade = (int)dicionario_onomasticoRAEs[k].Tipo });
            var sugestoesOnomasticos = IntGestDocRule.Current.LoadRAsCorrespondenciasNovas(GisaDataSetHelper.GetInstance(), dicionario_onomasticoDAL_RAEs, conn);
            foreach (var sugestao in sugestoesOnomasticos)
            {
                var caRow = GisaDataSetHelper.GetInstance().ControloAut.Cast<GISADataset.ControloAutRow>().Where(r => r.ID == sugestao.Value).Single();
                RegistoAutoridadeInterno rai = CreateRegistoAutoridadeInterno(caRow);
                rai.Estado = TipoEstado.SemAlteracoes;

                if (dicionario_onomasticoRAEs.ContainsKey(sugestao.Key))
                {
                    var rae = dicionario_onomasticoRAEs[sugestao.Key];
                    result.Add(rae, rai);

                    //actualizar o NIF no caso de não estar definido no controlo de autoridade
                    if (!string.IsNullOrEmpty(rae.NIF) && (caRow.IsChaveColectividadeNull() || string.IsNullOrEmpty(caRow.ChaveColectividade)))
                    {
                        caRow.ChaveColectividade = rae.NIF;
                        rai.Estado = TipoEstado.Editar;
                    }
                }
                else
                {
                    Debug.WriteLine("key not found!!");
                    Debug.WriteLine(sugestao.Key.ToString());
                }
            }

            return result;
        }

        internal static RegistoAutoridadeInterno CreateRegistoAutoridadeInterno(GISADataset.ControloAutRow caRow)
        {
            RegistoAutoridadeInterno rai = null;
            switch (caRow.IDTipoNoticiaAut)
            {
                case (long)TipoNoticiaAut.EntidadeProdutora:
                    var produtor = new Model.EntidadesInternas.Produtor();
                    produtor.Codigo = caRow.GetNivelControloAutRows()[0].NivelRow.Codigo;
                    rai = produtor;
                    break;
                case (long)TipoNoticiaAut.TipologiaInformacional:
                    var tipologia = new Model.EntidadesInternas.Tipologia();
                    rai = tipologia;
                    break;
                case (long)TipoNoticiaAut.Onomastico:
                    var onomastico = new Model.EntidadesInternas.Onomastico();
                    if (!caRow.IsChaveColectividadeNull())
                        onomastico.Codigo = caRow.ChaveColectividade;
                    rai = onomastico;
                    break;
                case (long)TipoNoticiaAut.Ideografico:
                    var ideografico = new Model.EntidadesInternas.Ideografico();
                    rai = ideografico;
                    break;
                case (long)TipoNoticiaAut.ToponimicoGeografico:
                    var geografico = new Model.EntidadesInternas.Geografico();
                    rai = geografico;
                    break;
            }
            rai.Id = caRow.ID;
            rai.Titulo = caRow.GetControloAutDicionarioRows().Where(cad => cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada).Single().DicionarioRow.Termo;
            return rai;
        }

        private static DocumentoGisa GetNewDocumentoGisa(GISADataset.NivelRow nivelRow, List<long> IDNivelProdutores)
        {
            var di = new DocumentoGisa();
            di.Tipo = TipoEntidadeInterna.DocumentoSimples;
            di.Estado = TipoEstado.SemAlteracoes;
            di.Id = nivelRow.ID;
            di.Codigo = nivelRow.Codigo;
            di.Titulo = nivelRow.GetNivelDesignadoRows()[0].Designacao;

            var frdRow = nivelRow.GetFRDBaseRows()[0];
            var codigoRow = GisaDataSetHelper.GetInstance().Codigo.Cast<GISADataset.CodigoRow>().Where( row => ((GISADataset.CodigoRow)row).Codigo.StartsWith("ne:", StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault();
            if (codigoRow != null)
                di.NumeroEspecifico.AdicionaValorOriginal(codigoRow.Codigo);

            var dpRow = frdRow.GetSFRDDatasProducaoRows().SingleOrDefault();
            if (dpRow != null && !(dpRow.InicioAno.Length == 0 || dpRow.InicioMes.Length == 0 || dpRow.InicioDia.Length == 0 || dpRow.FimAno.Length == 0 || dpRow.FimMes.Length == 0 || dpRow.FimDia.Length == 0))
                di.DataCriacao.AdicionaValorOriginal(new DataIncompleta(dpRow.InicioAno, dpRow.InicioMes, dpRow.InicioDia, dpRow.FimAno, dpRow.FimMes, dpRow.FimDia));

            // preencher com Tipologia e termos de indexação
            var cadRows = GisaDataSetHelper.GetInstance().FRDBase.Cast<GISADataset.FRDBaseRow>().Where(r => r.IDNivel == nivelRow.ID).SelectMany(frd => frd.GetIndexFRDCARows()).SelectMany(r => r.ControloAutRow.GetControloAutDicionarioRows());
            var onomasticos = cadRows.Where(cad => cad.ControloAutRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.Onomastico && cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada);
            var ideograficos = cadRows.Where(cad => cad.ControloAutRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.Ideografico && cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada);
            var geograficos = cadRows.Where(cad => cad.ControloAutRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.ToponimicoGeografico && cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada);
            var tipologia = cadRows.Where(cad => cad.ControloAutRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.TipologiaInformacional && cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada).ToList().SingleOrDefault();

            di.Onomasticos.AddRange(onomasticos.Select(cad => cad.DicionarioRow.Termo));
            di.Ideograficos.AddRange(ideograficos.Select(cad => cad.DicionarioRow.Termo));
            di.Toponimias.AddRange(geograficos.Select(cad => cad.DicionarioRow.Termo));

            if (tipologia != null)
                di.Tipologia = tipologia.DicionarioRow.Termo;

            var cadRowProdutores = IDNivelProdutores.SelectMany(i => GisaDataSetHelper.GetInstance().NivelControloAut.Cast<GISADataset.NivelControloAutRow>().Where(r => r.ID == i)).SelectMany(nca => nca.ControloAutRow.GetControloAutDicionarioRows());
            di.Produtores.AddRange(cadRowProdutores.Where(cad => cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada).Select(cad => cad.DicionarioRow.Termo));

            return di;
        }

        private static DocumentoGisa GetNewDocumentoAnexoGisa(GISADataset.NivelRow nivelRow, List<long> docProds)
        {
            var di = new DocumentoGisa();
            di.Tipo = TipoEntidadeInterna.DocumentoSimples;
            di.Estado = TipoEstado.SemAlteracoes;
            di.Id = nivelRow.ID;
            di.Codigo = nivelRow.Codigo;
            di.Titulo = nivelRow.GetNivelDesignadoRows()[0].Designacao;       

            return di;
        }

        private static DocumentoGisa GetNewDocumentoCompostoGisa(GISADataset.NivelRow nivelRow, List<long> IDNivelProdutores)
        {
            var di = new DocumentoGisa();
            di.Tipo = TipoEntidadeInterna.DocumentoComposto;
            di.Estado = TipoEstado.SemAlteracoes;
            di.Id = nivelRow.ID;
            di.Codigo = nivelRow.Codigo;
            di.Titulo = nivelRow.GetNivelDesignadoRows()[0].Designacao;

            var cadRowProdutores = IDNivelProdutores.SelectMany(i => GisaDataSetHelper.GetInstance().NivelControloAut.Cast<GISADataset.NivelControloAutRow>().Where(r => r.ID == i)).SelectMany(nca => nca.ControloAutRow.GetControloAutDicionarioRows());
            di.Produtores.AddRange(cadRowProdutores.Where(cad => cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada).Select(cad => cad.DicionarioRow.Termo));

            var frdRow = nivelRow.GetFRDBaseRows().Single();
            var dpRow = frdRow.GetSFRDDatasProducaoRows().SingleOrDefault();
            if (dpRow != null && !(dpRow.InicioAno.Length == 0 || dpRow.InicioMes.Length == 0 || dpRow.InicioDia.Length == 0 || dpRow.FimAno.Length == 0 || dpRow.FimMes.Length == 0 || dpRow.FimDia.Length == 0))
                di.DataCriacao.AdicionaValorOriginal(new DataIncompleta(dpRow.InicioAno, dpRow.InicioMes, dpRow.InicioDia, dpRow.FimAno, dpRow.FimMes, dpRow.FimDia));

            return di;
        }

        public static void AddTipologiaOriginal(CorrespondenciaDocs correspondenciaDoc)
        {
            var nRow = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().SingleOrDefault(r => r.ID == correspondenciaDoc.EntidadeInterna.Id);
            if (nRow == null) return;

            var caRow = nRow.GetFRDBaseRows()[0].GetIndexFRDCARows().Select(i => i.ControloAutRow).SingleOrDefault(ca => ca.IDTipoNoticiaAut == (long)TipoNoticiaAut.TipologiaInformacional);
            if (caRow == null) return;

            var rai = CreateRegistoAutoridadeInterno(caRow);
            rai.Estado = TipoEstado.SemAlteracoes;
                        
            // os anexos não têm tipologia
            var correspTip = correspondenciaDoc.CorrespondenciasRAs.SingleOrDefault(cRa => cRa.EntidadeExterna.Tipo == TipoEntidadeExterna.TipologiaInformacional);
            if (correspTip != null)
                correspTip.AddEntidadeInternaOriginal(rai);            
        }

        //public static void AddProcessoOriginal(CorrespondenciaDocs correspondenciaDoc)
        //{
        //    var dg = correspondenciaDoc.EntidadeInterna as DocumentoGisa;
        //    if (correspondenciaDoc.EntidadeExterna.Tipo == TipoEntidadeExterna.DocumentoComposto) return;

        //    dg.Processo.RemoveValorOriginal();
        //    if (dg.Processo.TipoOpcao == TipoOpcao.Original && dg.Processo.Valor == null)
        //        dg.Processo.TipoOpcao = TipoOpcao.Sugerida;

        //    var nRow = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().SingleOrDefault(r => r.ID == correspondenciaDoc.EntidadeInterna.Id);
        //    if (nRow == null) return;

        //    var rhRowList = nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().Where(r => r.NivelRowByNivelRelacaoHierarquicaUpper.IDTipoNivel == TipoNivel.DOCUMENTAL).ToList();
        //    if (rhRowList.Count != 1) return;

        //    var proc = GetNewDocumentoCompostoGisa(rhRowList[0].NivelRowByNivelRelacaoHierarquicaUpper, new List<long>());
        //    proc.Estado = TipoEstado.SemAlteracoes;
        //    dg.Processo.AdicionaValorOriginal(proc);
        //}

        //public static void ReavaliaEstadoFromProcesso(CorrespondenciaDocs cDoc)
        //{
        //    ReavaliaEstadoRAs(cDoc);
        //    ReavaliaEstadoSerie(cDoc);
        //}

        public static void ReavaliaEstadoRAs(CorrespondenciaDocs cDoc)
        {
            cDoc.CorrespondenciasRAs.ForEach(cRA => { RemoveTipologiaOriginal(cRA); ReavaliaEstado(cDoc, cRA); });
            AddTipologiaOriginal(cDoc);
        }
        public static void ReavaliaEstadoSerie(CorrespondenciaDocs cDoc)
        {
            var dg = cDoc.EntidadeInterna as DocumentoGisa;
            if (dg.Serie.Valor != null)
            {
                //if (dg.Estado == TipoEstado.Novo || (dg.Estado != TipoEstado.Novo && dg.Processo != null && dg.Processo.Valor.Estado == TipoEstado.Novo))
                //{
                //    dg.Serie.EstadoRelacaoPorOpcao[dg.Serie.TipoOpcao] = TipoEstado.Novo;
                //    if (dg.Estado != TipoEstado.Novo)
                //    {
                //        var rhRow = GisaDataSetHelper.GetInstance().RelacaoHierarquica.Cast<GISADataset.RelacaoHierarquicaRow>().SingleOrDefault(r => r.ID == dg.Id && r.IDUpper == dg.Serie.Valor.Id);
                //        dg.Serie.EstadoRelacaoPorOpcao[dg.Serie.TipoOpcao] = rhRow != null ? TipoEstado.SemAlteracoes : TipoEstado.Novo;
                //    }
                //}
                //else
                //{
                //    dg.Serie.RemoveValor();
                //    dg.Serie.TipoOpcao = TipoOpcao.Ignorar;
                //}
            }
        }

        public static void ReavaliaEstado(CorrespondenciaDocs cDoc)
        {
            if (cDoc.TipoOpcao == TipoOpcao.Ignorar)
            {
            //    IgnoreAllOptions(cDoc);
                return;
            }

            ReavaliaEstadoRAs(cDoc);

            var dg = cDoc.EntidadeInterna as DocumentoGisa;

            // reavaliar data de produção            
            if (dg.Estado == TipoEstado.SemAlteracoes)
            {
                var nRow = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().Single(r => r.ID == dg.Id);
                var dpRow = nRow.GetFRDBaseRows().Single().GetSFRDDatasProducaoRows().SingleOrDefault();
                if (dpRow != null && !(dpRow.InicioAno.Length == 0 && dpRow.InicioMes.Length == 0 && dpRow.InicioDia.Length == 0 && dpRow.FimAno.Length == 0 && dpRow.FimMes.Length == 0 && dpRow.FimDia.Length == 0))
                    dg.DataCriacao.AdicionaValorOriginal(new DataIncompleta(dpRow.InicioAno, dpRow.InicioMes, dpRow.InicioDia, dpRow.FimAno, dpRow.FimMes, dpRow.FimDia));

                if (dg.DataCriacao.GetValor(TipoOpcao.Original) != null && dg.DataCriacao.GetValor(TipoOpcao.Original).Equals(dg.DataCriacao.GetValor(TipoOpcao.Sugerida)))
                    dg.DataCriacao.EstadoRelacaoPorOpcao[TipoOpcao.Sugerida] = TipoEstado.SemAlteracoes;
                else
                    dg.DataCriacao.EstadoRelacaoPorOpcao[TipoOpcao.Sugerida] = dg.DataCriacao.Escolhas.ContainsKey(TipoOpcao.Original) ? TipoEstado.Editar : TipoEstado.Novo;
                if (dg.DataCriacao.EstadoRelacaoPorOpcao.ContainsKey(TipoOpcao.Trocada))
                {
                    if (dg.DataCriacao.GetValor(TipoOpcao.Original).Equals(dg.DataCriacao.GetValor(TipoOpcao.Trocada)))
                        dg.DataCriacao.EstadoRelacaoPorOpcao[TipoOpcao.Trocada] = TipoEstado.SemAlteracoes;
                    else
                        dg.DataCriacao.EstadoRelacaoPorOpcao[TipoOpcao.Trocada] = dg.DataCriacao.Escolhas.ContainsKey(TipoOpcao.Original) ? TipoEstado.Editar : TipoEstado.Novo;
                }
            }
            else
            {
                dg.DataCriacao.RemoveValorOriginal();
                dg.DataCriacao.EstadoRelacaoPorOpcao[TipoOpcao.Sugerida] = TipoEstado.Novo;
                if (dg.DataCriacao.EstadoRelacaoPorOpcao.ContainsKey(TipoOpcao.Trocada))
                    dg.DataCriacao.EstadoRelacaoPorOpcao[TipoOpcao.Trocada] = TipoEstado.Novo;
            }

            //AddProcessoOriginal(cDoc);

            // reavaliar processo
            //if (dg.Processo.Valor != null)
            //{
            //    dg.Processo.EstadoRelacaoPorOpcao[dg.Processo.TipoOpcao] = TipoEstado.Novo;
            //    if (dg.Estado != TipoEstado.Novo && dg.Processo.Valor.Estado != TipoEstado.Novo)
            //    {
            //        var rhRow = GisaDataSetHelper.GetInstance().RelacaoHierarquica.Cast<GISADataset.RelacaoHierarquicaRow>().SingleOrDefault(r => r.ID == dg.Id && r.IDUpper == dg.Processo.Valor.Id);
            //        dg.Processo.EstadoRelacaoPorOpcao[dg.Processo.TipoOpcao] = rhRow != null ? TipoEstado.SemAlteracoes : TipoEstado.Editar;
            //    }
            //    else if (dg.Estado != TipoEstado.Novo && dg.Processo.Valor.Estado == TipoEstado.Novo)
            //        dg.Processo.EstadoRelacaoPorOpcao[dg.Processo.TipoOpcao] = TipoEstado.Editar;
            //    else if (dg.Processo.Valor.Estado != TipoEstado.Novo)
            //    {
            //        dg.Serie.RemoveValor();
            //        dg.Serie.TipoOpcao = TipoOpcao.Ignorar;
            //    }
            //}

            // reavaliar serie
            ReavaliaEstadoSerie(cDoc);
        }

        private static void IgnoreAllOptions(CorrespondenciaDocs cDoc)
        {
            cDoc.CorrespondenciasRAs.ForEach(cRA => cRA.TipoOpcao = TipoOpcao.Ignorar);
        }

        private static void ReavaliaEstado(CorrespondenciaDocs cDoc, CorrespondenciaRAs cRa)
        {
            foreach (TipoOpcao op in Enum.GetValues(typeof(TipoOpcao)))
            {
                if (cRa.TipoOpcao == TipoOpcao.Ignorar) continue;

                var ei = cRa.GetEntidadeInterna(op);
                if (ei == null) continue;

                var tnp = ((RegistoAutoridadeInterno)cRa.EntidadeInterna).TipoNoticiaAut;
                // se a entidade interna for uma EP e esta estiver relaccionada com um documento simples, o estado "novo" do documento não implica uma nova relação com o produtor
                // uma vez que o seu processo já pode ter essa associação
                if (tnp != TipoNoticiaAut.EntidadeProdutora && tnp != TipoNoticiaAut.TipologiaInformacional && (cDoc.EntidadeInterna.Estado == TipoEstado.Novo || ei.Estado == TipoEstado.Novo))
                {
                    cRa.EstadoRelacaoPorOpcao[op] = TipoEstado.Novo;
                    continue;
                }

                var dg = cDoc.EntidadeInterna as DocumentoGisa;
                var tipoEntidade = TipoEntidade.GetTipoEntidadeInterna(cRa.EntidadeExterna.Tipo);

                switch (tipoEntidade)
                {
                    case TipoEntidadeInterna.EntidadeProdutora:
                        if (dg.Processo == null && dg.Estado == TipoEstado.Novo)
                        {
                            cRa.EstadoRelacaoPorOpcao[op] = TipoEstado.Novo;
                            continue;
                        }

                        //if (dg.Processo.Valor != null)
                        //    dg = dg.Processo.Valor;

                        if (ei.Estado == TipoEstado.Novo)
                            cRa.EstadoRelacaoPorOpcao[op] = TipoEstado.Novo;
                        else
                        {
                            var nRowCA = GisaDataSetHelper.GetInstance().NivelControloAut.Cast<GISADataset.NivelControloAutRow>().Single(r => r.IDControloAut == ei.Id).NivelRow;
                            cRa.EstadoRelacaoPorOpcao[op] = IsProdutor(dg.Id, nRowCA.ID) ? TipoEstado.SemAlteracoes : TipoEstado.Novo;
                        }
                        break;
                    case TipoEntidadeInterna.Tipologia:
                        if (cDoc.EntidadeInterna.Estado == TipoEstado.Novo)
                            cRa.EstadoRelacaoPorOpcao[op] = TipoEstado.Novo;
                        else
                        {
                            var caRow = GisaDataSetHelper.GetInstance().FRDBase.Cast<GISADataset.FRDBaseRow>().Single(frd => frd.IDNivel == dg.Id).GetIndexFRDCARows().Select(r => r.ControloAutRow).SingleOrDefault(r => r.IDTipoNoticiaAut == (long)TipoNoticiaAut.TipologiaInformacional);
                            if (caRow == null)
                                cRa.EstadoRelacaoPorOpcao[op] = TipoEstado.Novo;
                            else
                                cRa.EstadoRelacaoPorOpcao[op] = caRow.ID == ei.Id ? TipoEstado.SemAlteracoes : TipoEstado.Editar;
                        }
                        break;
                    default:
                        var indexFRDCARow = GisaDataSetHelper.GetInstance().FRDBase.Cast<GISADataset.FRDBaseRow>().Single(frd => frd.IDNivel == dg.Id).GetIndexFRDCARows().SingleOrDefault(r => r.IDControloAut == ei.Id);
                        cRa.EstadoRelacaoPorOpcao[op] = indexFRDCARow != null ? TipoEstado.SemAlteracoes : TipoEstado.Novo;
                        break;
                }
            }
        }

        public static bool IsProdutor(long IDNivelDoc, long IDNivelCA)
        {
            var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            bool result = false;
            try
            {
                DBAbstractDataLayer.DataAccessRules.ControloAutRule.Current.LoadNivelFromControloAut(GisaDataSetHelper.GetInstance(), IDNivelCA, ho.Connection);
                var prods = DBAbstractDataLayer.DataAccessRules.IntGestDocRule.Current.GetProdutoresRelDirect(IDNivelDoc, ho.Connection);
                result = prods.Contains(IDNivelCA);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
            finally
            {
                ho.Dispose();
            }

            return result;
        }

        public static void LoadDocumentDetails(EntidadeInterna ei)
        {
            var dg = ei as DocumentoGisa;
            var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                DBAbstractDataLayer.DataAccessRules.IntGestDocRule.Current.LoadDocumentDetails(GisaDataSetHelper.GetInstance(), dg.Id, ho.Connection);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
            finally
            {
                ho.Dispose();
            }
        }

        private static void RemoveTipologiaOriginal(CorrespondenciaRAs cRA)
        {
            if (cRA.EntidadeExterna.Tipo != TipoEntidadeExterna.TipologiaInformacional) return;

            cRA.RemoveEntidadeInternaOriginal();
        }

        internal static bool CanSelectProcesso(CorrespondenciaDocs correspondenciaDocs, out string message)
        {
            message = string.Empty;

            if (correspondenciaDocs.EntidadeInterna.Estado != TipoEstado.Novo)
            {
                message = "Só é possível selecionar processos para documentos novos.";
                return false;
            }

            return true;
        }

        internal static bool CanSelectSerie(CorrespondenciaDocs correspondenciaDocs, out string message, out long produtorID)
        {
            message = string.Empty;
            produtorID = -1;
            var dg = correspondenciaDocs.EntidadeInterna as DocumentoGisa;

            //if (correspondenciaDocs.EntidadeInterna.Estado != TipoEstado.Novo && dg.Processo.Valor == null)
            //{
            //    message = "Só é possível selecionar séries para documentos novos.";
            //    return false;
            //}
            
            //if (dg.Processo.Valor != null && dg.Processo.Valor.Estado != TipoEstado.Novo)
            //{
            //    message = "Só não é possível selecionar uma série para documentos de processos já criados.";
            //    return false;
            //}

            var produtor = correspondenciaDocs.CorrespondenciasRAs.SingleOrDefault(c => ((RegistoAutoridadeExterno)c.EntidadeExterna).Tipo == TipoEntidadeExterna.Produtor);
            if (produtor == null || produtor.TipoOpcao == TipoOpcao.Ignorar)
            {
                message = "Para poder escolher uma série é necessário haver uma entidade produtora selecionada.";
                return false;
            }
            
            var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                if (produtor.EntidadeInterna.Estado != TipoEstado.Novo && produtor.EntidadeInterna.Id > 0)
                {
                    produtorID = GisaDataSetHelper.GetInstance().ControloAut.Cast<GISADataset.ControloAutRow>().Single(r => r.ID == produtor.EntidadeInterna.Id).GetNivelControloAutRows().Single().NivelRow.ID;
                    if (DBAbstractDataLayer.DataAccessRules.NivelRule.Current.HasSeries(produtorID, ho.Connection))
                        return true;
                }
                message = "O produtor selecionado não tem qualquer série associada.";
                return false;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
            finally
            {
                ho.Dispose();
            }
        }

        internal static long HasProdutorSeries(CorrespondenciaDocs correspondenciaDocs)
        {
            long produtorID = -1;
            var produtor = correspondenciaDocs.CorrespondenciasRAs.Single(c => ((RegistoAutoridadeExterno)c.EntidadeExterna).Tipo == TipoEntidadeExterna.Produtor);

            if (produtor.EntidadeInterna.Estado != TipoEstado.Novo && produtor.EntidadeInterna.Id > 0)
                produtorID = GisaDataSetHelper.GetInstance().ControloAut.Cast<GISADataset.ControloAutRow>().Single(r => r.ID == produtor.EntidadeInterna.Id).GetNivelControloAutRows().Single().NivelRow.ID;
            return produtorID;
        }

        // sempre que há uma alteração na selecção do produtor, a série escolhida é retirada das opções disponíveis
        internal static void SelectIgnorarSerie(CorrespondenciaDocs correspondenciaDocs)
        {
            var dg = correspondenciaDocs.EntidadeInterna as DocumentoGisa;
            dg.Serie.RemoveValor();
            dg.Serie.TipoOpcao = TipoOpcao.Ignorar;
        }

        internal static void RevertCorrespondencia(CorrespondenciaDocs correspondencia)
        {
            correspondencia.Edited = false;
            correspondencia.TipoOpcao = TipoOpcao.Sugerida;
            correspondencia.AllCorrespondenciasRAs.ForEach(cRa => cRa.TipoOpcao = TipoOpcao.Sugerida);
            var dg = correspondencia.EntidadeInterna as DocumentoGisa;
            dg.RevertProperties();
        }
    }
}