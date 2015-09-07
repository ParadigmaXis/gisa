using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using iTextSharp.text;
using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Reports
{
	/// <summary>
	/// Um InventarioDetalhado é um inventário com a presença de todos os 
	/// níveis documentais existentes e com a presença dos níveis estruturais 
	/// necessários para estabelecer o contexto desses mesmos níveis documentais.
	/// Um Inventario é, por omissão, completo (ou seja, toda a implementação de 
	/// um InventarioDetalhado está na realidade concentrada na class Inventario)
	/// </summary>
	public class InventarioDetalhado : Inventario
	{
		public InventarioDetalhado(string FileName, bool isTopDown, long idTrustee) : base(FileName, isTopDown, idTrustee) { }

        public InventarioDetalhado(string FileName, ArrayList parameters, long idTrustee) : base(FileName, parameters, true, idTrustee) { }

        public InventarioDetalhado(string FileName, ArrayList parameters, bool isTopDown, long idTrustee) : base(FileName, parameters, isTopDown, idTrustee) { }

        public InventarioDetalhado(string FileName, ArrayList parameters, List<ReportParameter> fields, bool isTopDown, long idTrustee) : base(FileName, parameters, fields, isTopDown, idTrustee) { }

        private List<ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet> Fields(List<ReportParameter> parameters)
        {
            List<ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet> fields = new List<ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet>();
            foreach (ReportParameterRelInvCatPesqDet rp in parameters)
                fields.Add(rp.Campo);

            return fields;
        }

		protected override void LoadContents(IDbConnection connection, ref IDataReader reader) {
			base.LoadContents (connection, ref reader);

            Nivel nvl;
            List<ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet> fields = Fields(this.mFields);

            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Autores))
            {
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    nvl.Autores.Add(reader.GetString(1));
                }
                reader.NextResult();
            }

            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.HistAdministrativaBiografica))
            {
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    nvl.HistAdministrativaBiografica.Add("História administrativa / biográfica", reader.GetValue(1).ToString());
                }
                
                reader.NextResult();
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));

                    string datasProducao = GISA.Utils.GUIHelper.FormatDateInterval(
                        GISA.Utils.GUIHelper.FormatDate(reader.GetValue(2).ToString(), reader.GetValue(3).ToString(), reader.GetValue(4).ToString(), GisaDataSetHelper.GetDBNullableBoolean(ref reader, 5)),
                        GISA.Utils.GUIHelper.FormatDate(reader.GetValue(6).ToString(), reader.GetValue(7).ToString(), reader.GetValue(8).ToString(), GisaDataSetHelper.GetDBNullableBoolean(ref reader, 9)));

                    if (reader.GetValue(2).ToString().Length > 0 || reader.GetValue(3).ToString().Length > 0 ||
                        reader.GetValue(4).ToString().Length > 0 || reader.GetValue(6).ToString().Length > 0 ||
                        reader.GetValue(7).ToString().Length > 0 || reader.GetValue(8).ToString().Length > 0)
                    {
                        if (reader.GetValue(1).ToString().Length > 0)
                            nvl.HistAdministrativaBiografica.Add("Datas de existência", datasProducao + System.Environment.NewLine + reader.GetValue(1).ToString());
                        else
                            nvl.HistAdministrativaBiografica.Add("Datas de existência", datasProducao);
                    }

                    nvl.HistAdministrativaBiografica.Add("História", reader.GetValue(10).ToString());
                    nvl.HistAdministrativaBiografica.Add("Zona geográfica", reader.GetValue(11).ToString());
                    nvl.HistAdministrativaBiografica.Add("Estatuto legal", reader.GetValue(12).ToString());
                    nvl.HistAdministrativaBiografica.Add("Funções, ocupações e atividades", reader.GetValue(13).ToString());
                    nvl.HistAdministrativaBiografica.Add("Enquadramento legal", reader.GetValue(14).ToString());
                    nvl.HistAdministrativaBiografica.Add("Estrutura interna", reader.GetValue(15).ToString());
                    nvl.HistAdministrativaBiografica.Add("Contexto geral", reader.GetValue(16).ToString());
                    nvl.HistAdministrativaBiografica.Add("Outras informações relevantes", reader.GetValue(17).ToString());
                }
                reader.NextResult();
            }

            // ler as tipologias informacionais, conteudos indexados, diplomas e modelos
            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Indexacao) || fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.TipologiaInformacional) || fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Diplomas) || fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Modelos))
            {
                int tipoNoticiaAut;
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    if (nvl != null)
                    {
                        tipoNoticiaAut = System.Convert.ToInt32(reader.GetValue(1));
                        switch (tipoNoticiaAut)
                        {
                            case (int) TipoNoticiaAut.Ideografico:
                            case (int) TipoNoticiaAut.Onomastico:
                            case (int) TipoNoticiaAut.ToponimicoGeografico:
                                nvl.Conteudos.Add(reader.GetValue(2).ToString());
                                break;
                            case (int) TipoNoticiaAut.TipologiaInformacional:
                                nvl.Tipologias.Add(reader.GetValue(2).ToString());
                                break;
                            case (int) TipoNoticiaAut.Diploma:
                                nvl.Diplomas.Add(reader.GetValue(2).ToString());
                                break;
                            case (int) TipoNoticiaAut.Modelo:
                                nvl.Modelos.Add(reader.GetValue(2).ToString());
                                break;
                        }
                    }
                }
                reader.NextResult();
            }

            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.DiplomaLegal))
            {
                int tipoNoticiaAut;
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    if (nvl != null)
                    {
                        tipoNoticiaAut = System.Convert.ToInt32(reader.GetValue(1));
                        if (tipoNoticiaAut == (int)TipoNoticiaAut.Diploma)
                            nvl.DiplomaLegal.Add(reader.GetValue(2).ToString());
                    }
                }
                reader.NextResult();
            }

            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.CotaDocumento))
            {
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    if (nvl != null)
                    {
                        var cotaUF = reader.GetString(1);
                        var cotaDoc = reader.GetString(2);

                        if (cotaDoc.Length > 0 && cotaUF.Length > 0)
                            nvl.CotaDocumento.Add(cotaUF + " - " + cotaDoc);
                        else if (cotaDoc.Length > 0)
                            nvl.CotaDocumento.Add(cotaDoc);
                    }
                }
                reader.NextResult();
            }

            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.UFsAssociadas))
            {
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    if (nvl != null)
                    {
                        string largura;
                        string altura;
                        string profundidade;

                        if (reader.GetValue(4) != null && reader.GetValue(4) != DBNull.Value)
                            largura = string.Format("{0:0.000}", System.Convert.ToDecimal(reader.GetValue(4)));
                        else
                            largura = "?";

                        if (reader.GetValue(5) != null && reader.GetValue(5) != DBNull.Value)
                            altura = string.Format("{0:0.000}", System.Convert.ToDecimal(reader.GetValue(5)));
                        else
                            altura = "?";

                        if (reader.GetValue(6) != null && reader.GetValue(6) != DBNull.Value)
                            profundidade = string.Format("{0:0.000}", System.Convert.ToDecimal(reader.GetValue(6)));
                        else
                            profundidade = "?";

                        nvl.UnidadesFisicas.Add(
                            new List<string> { 
                                reader.GetValue(1).ToString(),
                                reader.GetValue(2).ToString(),
                                reader.GetValue(3).ToString(),
                                largura,
                                altura,
                                profundidade,
                                reader.GetValue(7).ToString(),
                                reader.GetValue(8).ToString()
                            }
                        );
                    }
                }
                reader.NextResult();
            }

            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.TradicaoDocumental))
            {
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    if (nvl != null)
                        nvl.TradicaoDocumental.Add(reader.GetValue(1).ToString());
                }
                reader.NextResult();
            }

            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Ordenacao))
            {
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    if (nvl != null)
                        nvl.Ordenacao.Add(reader.GetValue(1).ToString());
                }
                reader.NextResult();
            }

            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.ObjectosDigitais))
            {
                // ler imagens por caminho de rede e web
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    if (nvl != null)
                    {
                        nvl.ObjectosDigitais.Add(
                            new List<string> { 
                                reader.GetValue(1).ToString(),
                                reader.GetValue(2).ToString(),
                                reader.GetValue(3).ToString()
                            }
                        );
                    }
                }
                reader.NextResult();

                // ler objetos digitais fedora
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    if (nvl != null)
                    {
                        nvl.ObjectosDigitaisFedora.Add(
                            new List<string> { 
                                reader.GetValue(1).ToString(),
                                reader.GetValue(2).ToString()
                            }
                        );
                    }
                }
                reader.NextResult();
            }

            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Lingua))
            {
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    if (nvl != null)
                        nvl.Lingua.Add(reader.GetValue(1).ToString());
                }
                reader.NextResult();
            }

            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Alfabeto))
            {
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    if (nvl != null)
                        nvl.Alfabeto.Add(reader.GetValue(1).ToString());
                }
                reader.NextResult();
            }

            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.FormaSuporteAcondicionamento))
            {
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    if (nvl != null)
                        nvl.FormaSuporteAcond.Add(reader.GetValue(1).ToString());
                }
                reader.NextResult();
            }

            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.MaterialSuporte))
            {
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    if (nvl != null)
                        nvl.MaterialSuporte.Add(reader.GetValue(1).ToString());
                }
                reader.NextResult();
            }

            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.TecnicaRegisto))
            {
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    if (nvl != null)
                        nvl.TecnicaRegisto.Add(reader.GetValue(1).ToString());
                }
                reader.NextResult();
            }

            #region Licença de obra

            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_RequerentesIniciais))
            {
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    nvl.LO_RequerentesIniciais.Add(reader.GetString(1));
                }
                reader.NextResult();
            }

            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_RequerentesAverbamentos))
            {
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    nvl.LO_RequerentesAverbamentos.Add(reader.GetString(1));
                }
                reader.NextResult();
            }

            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_DesignacaoNumPoliciaAct))
            {
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    nvl.LO_DesignacaoNumPoliciaAct.Add(reader.GetString(1) + " " + reader.GetString(2));
                }
                reader.NextResult();
            }

            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_DesignacaoNumPoliciaAntigo))
            {
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    nvl.LO_DesignacaoNumPoliciaAntigo.Add(reader.GetString(1) + " " + reader.GetString(2));
                }
                reader.NextResult();
            }

            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_TecnicoObra))
            {
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    nvl.LO_TecnicoObra.Add(reader.GetString(1));
                }
                reader.NextResult();
            }

            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_AtestHabit))
            {
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    nvl.LO_AtestHabit.Add(reader.GetString(1));
                }
                reader.NextResult();
            }

            if (fields.Contains(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_DataLicConst))
            {
                while (reader.Read())
                {
                    nvl = GetExistentNivel(System.Convert.ToInt64(reader.GetValue(0)));
                    nvl.LO_DataLicConst.Add(GISA.Utils.GUIHelper.FormatDate(reader.GetString(1), reader.GetString(2), reader.GetString(3).ToString()));
                }
                reader.NextResult();
            }

            #endregion
        }

        protected override void AddExtraDetails(Nivel nvl, Document doc, float indentation)
        {
			System.Text.StringBuilder agregados;
            ArrayList info;
            Table detailsTable;

            // percorrer a lista de campos selecionados pelo utilizador respeitando a ordem que aparecem na interface
            foreach (ReportParameterRelInvCatPesqDet param in this.mFields)
            {
                ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet campo = param.Campo;
                if (param.RetType == ReportParameter.ReturnType.TextOnly)
                {
                    info = nvl.InfoAdicional[campo];
                    List<string> campos = new List<string>();

                    if (((List<string>)info[0])[0].Length > 0)
                    {
                        detailsTable = CreateTable(indentation);
                        AddNewCell(detailsTable, "");
                        AddNewCell(detailsTable, GetParameterName(param) + ":", this.HeaderFont);
                        AddNewCell(detailsTable, ((List<string>)info[0])[0], this.ContentFont);
                        AddTable(doc, detailsTable);
                    }
                }
                else
                {
                    agregados = new System.Text.StringBuilder();
                    switch (campo)
                    {
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Autores:
                            AddInfoToRel(param, doc, indentation, nvl.Autores);
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.HistAdministrativaBiografica:
                            if (nvl.HistAdministrativaBiografica.Count > 0)
                            {
                                if (nvl.IDTipoNivel == TipoNivel.ESTRUTURAL)
                                {
                                    System.Text.StringBuilder infos = new System.Text.StringBuilder();                                    

                                    foreach (DictionaryEntry myDE in nvl.HistAdministrativaBiografica)
                                    {
                                        if (myDE.Value.ToString().Length > 0)
                                            infos.AppendLine(myDE.Key.ToString() + ": " + myDE.Value.ToString());
                                    }

                                    if (infos.Length > 0)
                                    {
                                        detailsTable = CreateTable(indentation);
                                        AddNewCell(detailsTable, "");
                                        AddNewCell(detailsTable, GetParameterName(param) + ":", this.HeaderFont);
                                        AddNewCell(detailsTable, infos.ToString(), this.ContentFont);
                                        AddTable(doc, detailsTable);
                                    }
                                }
                                else
                                {
                                    if (nvl.HistAdministrativaBiografica[0].ToString().Length > 0)
                                    {
                                        detailsTable = CreateTable(indentation);
                                        AddNewCell(detailsTable, "");
                                        AddNewCell(detailsTable, GetParameterName(param) + ":", this.HeaderFont);
                                        AddNewCell(detailsTable, nvl.HistAdministrativaBiografica[0].ToString(), this.ContentFont);
                                        AddTable(doc, detailsTable);
                                    }
                                }
                            }
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.TipologiaInformacional:
                            AddInfoToRel(param, doc, indentation, nvl.Tipologias);
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Indexacao:
                            AddInfoToRel(param, doc, indentation, nvl.Conteudos);
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Diplomas:
                            if (nvl.Diplomas.Count > 0)
                            {
                                List<string> paragraphs = new List<string>();
                                foreach (string diploma in nvl.Diplomas)
                                    paragraphs.Add("• " + diploma);

                                detailsTable = CreateTable(indentation);
                                AddNewCell(detailsTable, "");
                                AddNewCell(detailsTable, GetParameterName(param) + ":", this.HeaderFont);
                                AddNewCell(detailsTable, paragraphs, this.ContentFont);
                                AddTable(doc, detailsTable);
                            }
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Modelos:
                            if (nvl.Modelos.Count > 0)
                            {
                                List<string> paragraphs = new List<string>();
                                foreach (string modelo in nvl.Modelos)
                                    paragraphs.Add("• " + modelo);

                                detailsTable = CreateTable(indentation);
                                AddNewCell(detailsTable, "");
                                AddNewCell(detailsTable, GetParameterName(param) + ":", this.HeaderFont);
                                AddNewCell(detailsTable, paragraphs, this.ContentFont);
                                AddTable(doc, detailsTable);
                            }
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.DiplomaLegal:
                            if (nvl.Diplomas.Count > 0)
                            {
                                List<string> paragraphs = new List<string>();
                                foreach (string diploma in nvl.Diplomas)
                                    paragraphs.Add("• " + diploma);

                                detailsTable = CreateTable(indentation);
                                AddNewCell(detailsTable, "");
                                AddNewCell(detailsTable, GetParameterName(param) + ":", this.HeaderFont);
                                AddNewCell(detailsTable, paragraphs, this.ContentFont);
                                AddTable(doc, detailsTable);
                            }
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.CotaDocumento:
                            AddInfoToRel(param, doc, indentation, nvl.CotaDocumento);
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.UFsAssociadas:
                            if (nvl.UnidadesFisicas.Count > 0)
                            {
                                System.Text.StringBuilder infos;
                                bool addFieldName = true;
                                foreach (List<string> uf in nvl.UnidadesFisicas)
                                {
                                    infos = new System.Text.StringBuilder();
                                    detailsTable = CreateTable(indentation);
                                    AddNewCell(detailsTable, "");
                                    if (addFieldName)
                                    {
                                        AddNewCell(detailsTable, GetParameterName(param) + ":", this.HeaderFont);
                                        addFieldName = false;
                                    }
                                    else
                                        AddNewCell(detailsTable, "");

                                    infos.AppendLine("• " + uf[0]);
                                    infos.AppendLine("Código: " + uf[1]);
                                    if (uf[2].Length > 0)
                                        infos.AppendLine("Cota: " + uf[2]);
                                    infos.AppendLine("Tipo: " + uf[6]);
                                    infos.AppendLine(string.Format("Dimensões: {0} x {1} x {2} {3}", uf[3], uf[4], uf[5], uf[7]));


                                    AddNewCell(detailsTable, infos.ToString(), this.ContentFont);
                                    AddTable(doc, detailsTable);
                                }
                            }
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.TradicaoDocumental:
                            AddInfoToRel(param, doc, indentation, nvl.TradicaoDocumental);
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Ordenacao:
                            AddInfoToRel(param, doc, indentation, nvl.Ordenacao);
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.ObjectosDigitais:
                            if (nvl.ObjectosDigitais.Count > 0)
                            {
                                System.Text.StringBuilder infos;
                                bool addFieldName = true;
                                foreach (List<string> od in nvl.ObjectosDigitais)
                                {
                                    infos = new System.Text.StringBuilder();
                                    detailsTable = CreateTable(indentation);
                                    AddNewCell(detailsTable, "");
                                    if (addFieldName)
                                    {
                                        AddNewCell(detailsTable, GetParameterName(param) + ":", this.HeaderFont);
                                        addFieldName = false;
                                    }
                                    else
                                        AddNewCell(detailsTable, "");

                                    infos.AppendLine("• " + od[0]);
                                    infos.AppendLine("Descrição: " + od[1]);
                                    infos.AppendLine("Caminho: " + od[2]);

                                    AddNewCell(detailsTable, infos.ToString(), this.ContentFont);
                                    AddTable(doc, detailsTable);
                                }
                            }
                            if (nvl.ObjectosDigitaisFedora.Count > 0)
                            {
                                System.Text.StringBuilder infos;
                                bool addFieldName = true;
                                foreach (List<string> od in nvl.ObjectosDigitaisFedora)
                                {
                                    infos = new System.Text.StringBuilder();
                                    detailsTable = CreateTable(indentation);
                                    AddNewCell(detailsTable, "");
                                    if (addFieldName)
                                    {
                                        AddNewCell(detailsTable, GetParameterName(param) + ":", this.HeaderFont);
                                        addFieldName = false;
                                    }
                                    else
                                        AddNewCell(detailsTable, "");

                                    infos.AppendLine("• " + od[0]);
                                    infos.AppendLine("PID: " + od[1]);

                                    AddNewCell(detailsTable, infos.ToString(), this.ContentFont);
                                    AddTable(doc, detailsTable);
                                }
                            }
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Lingua:
                            AddInfoToRel(param, doc, indentation, nvl.Lingua);
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Alfabeto:
                            AddInfoToRel(param, doc, indentation, nvl.Alfabeto);
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.FormaSuporteAcondicionamento:
                            AddInfoToRel(param, doc, indentation, nvl.FormaSuporteAcond);
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.MaterialSuporte:
                            AddInfoToRel(param, doc, indentation, nvl.MaterialSuporte);
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.TecnicaRegisto:
                            AddInfoToRel(param, doc, indentation, nvl.TecnicaRegisto);
                            break;

                        #region Licença de obra
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_RequerentesIniciais:
                            if (nvl.LO_RequerentesIniciais.Count > 0)
                            {
                                List<string> paragraphs = new List<string>();
                                foreach (string LO_RequerentesIniciais in nvl.LO_RequerentesIniciais)
                                    paragraphs.Add("• " + LO_RequerentesIniciais);

                                detailsTable = CreateTable(indentation);
                                AddNewCell(detailsTable, "");
                                AddNewCell(detailsTable, GetParameterName(param) + ":", this.HeaderFont);
                                AddNewCell(detailsTable, paragraphs, this.ContentFont);
                                AddTable(doc, detailsTable);
                            }
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_RequerentesAverbamentos:
                            if (nvl.LO_RequerentesAverbamentos.Count > 0)
                            {
                                List<string> paragraphs = new List<string>();
                                foreach (string LO_RequerentesAverbamentos in nvl.LO_RequerentesAverbamentos)
                                    paragraphs.Add("• " + LO_RequerentesAverbamentos);

                                detailsTable = CreateTable(indentation);
                                AddNewCell(detailsTable, "");
                                AddNewCell(detailsTable, GetParameterName(param) + ":", this.HeaderFont);
                                AddNewCell(detailsTable, paragraphs, this.ContentFont);
                                AddTable(doc, detailsTable);
                            }
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_DesignacaoNumPoliciaAct:
                            if (nvl.LO_DesignacaoNumPoliciaAct.Count > 0)
                            {
                                List<string> paragraphs = new List<string>();
                                foreach (string LO_DesignacaoNumPoliciaAct in nvl.LO_DesignacaoNumPoliciaAct)
                                    paragraphs.Add("• " + LO_DesignacaoNumPoliciaAct);

                                detailsTable = CreateTable(indentation);
                                AddNewCell(detailsTable, "");
                                AddNewCell(detailsTable, GetParameterName(param) + ":", this.HeaderFont);
                                AddNewCell(detailsTable, paragraphs, this.ContentFont);
                                AddTable(doc, detailsTable);
                            }
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_DesignacaoNumPoliciaAntigo:
                            if (nvl.LO_DesignacaoNumPoliciaAntigo.Count > 0)
                            {
                                List<string> paragraphs = new List<string>();
                                foreach (string LO_DesignacaoNumPoliciaAntigo in nvl.LO_DesignacaoNumPoliciaAntigo)
                                    paragraphs.Add("• " + LO_DesignacaoNumPoliciaAntigo);

                                detailsTable = CreateTable(indentation);
                                AddNewCell(detailsTable, "");
                                AddNewCell(detailsTable, GetParameterName(param) + ":", this.HeaderFont);
                                AddNewCell(detailsTable, paragraphs, this.ContentFont);
                                AddTable(doc, detailsTable);
                            }
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_TecnicoObra:
                            if (nvl.LO_TecnicoObra.Count > 0)
                            {
                                List<string> paragraphs = new List<string>();
                                foreach (string LO_TecnicoObra in nvl.LO_TecnicoObra)
                                    paragraphs.Add("• " + LO_TecnicoObra);

                                detailsTable = CreateTable(indentation);
                                AddNewCell(detailsTable, "");
                                AddNewCell(detailsTable, GetParameterName(param) + ":", this.HeaderFont);
                                AddNewCell(detailsTable, paragraphs, this.ContentFont);
                                AddTable(doc, detailsTable);
                            }
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_AtestHabit:
                            if (nvl.LO_AtestHabit.Count > 0)
                            {
                                List<string> paragraphs = new List<string>();
                                foreach (string LO_AtestHabit in nvl.LO_AtestHabit)
                                    paragraphs.Add("• " + LO_AtestHabit);

                                detailsTable = CreateTable(indentation);
                                AddNewCell(detailsTable, "");
                                AddNewCell(detailsTable, GetParameterName(param) + ":", this.HeaderFont);
                                AddNewCell(detailsTable, paragraphs, this.ContentFont);
                                AddTable(doc, detailsTable);
                            }
                            break;
                        case ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_DataLicConst:
                            if (nvl.LO_DataLicConst.Count > 0)
                            {
                                List<string> paragraphs = new List<string>();
                                foreach (string LO_DataLicConst in nvl.LO_DataLicConst)
                                    paragraphs.Add("• " + LO_DataLicConst);

                                detailsTable = CreateTable(indentation);
                                AddNewCell(detailsTable, "");
                                AddNewCell(detailsTable, GetParameterName(param) + ":", this.HeaderFont);
                                AddNewCell(detailsTable, paragraphs, this.ContentFont);
                                AddTable(doc, detailsTable);
                            }
                            break;
                        #endregion
                    }
                }
            }
		}

        private void AddInfoToRel(ReportParameterRelInvCatPesqDet param, Document doc, float indentation, ArrayList info)
        {
            if (info.Count > 0)
            {
                Table detailsTable;
                System.Text.StringBuilder agregados = new System.Text.StringBuilder();
                foreach (string s in info)
                {
                    if (agregados.Length == 0)
                        agregados.Append(s);
                    else
                        agregados.AppendFormat("; {0}", s);
                }
                detailsTable = CreateTable(indentation);
                AddNewCell(detailsTable, "");
                AddNewCell(detailsTable, GetParameterName(param) + ":", this.HeaderFont);
                AddNewCell(detailsTable, agregados.ToString(), this.ContentFont);
                AddTable(doc, detailsTable);
            }
        }

		protected override bool IsDetalhado() {
			return true;
		}
	}
}
