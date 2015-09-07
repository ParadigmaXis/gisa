using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using iTextSharp.text;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Model;
using GISA.Utils;

namespace GISA.Reports
{
	public class UnidadesFisicas : Relatorio
	{
		public UnidadesFisicas(string FileName, long IDTrustee) : base(FileName, IDTrustee) {}

		public UnidadesFisicas(string FileName, ArrayList parameters, List<ReportParameter> fields, long IDTrustee) : base(FileName, parameters, IDTrustee) {
            this.mFields = fields;
        }

		protected override void InitializeReport(IDbConnection connection){
			if (mParameters != null && mParameters.Count == 0){
				return;
			}

			DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.InitializeListaUnidadesFisicas(mParameters, connection);
		}

		protected override void FinalizeReport(IDbConnection connection){
			if (mParameters != null && mParameters.Count == 0){
				return;
			}
			DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.FinalizeListaUnidadesFisicas(connection);
		}

		protected override void LoadContents(IDbConnection connection, ref IDataReader reader) {
            reader = DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.ReportUnidadesFisicas(this.mIDTrustee, this.mFields, connection);            

			UnidadeFisica uf = null;
			// ler as unidades físicas a apresentar na lista
			while (reader.Read()) {
				uf = new UnidadeFisica();
				uf.ID = System.Convert.ToInt64(reader.GetValue(0));
				uf.IDEntidadeDetentora = System.Convert.ToInt64(reader.GetValue(1));
				uf.CodigoCompleto = reader.GetString(2);
				uf.Designacao = reader.GetString(3);

                int i = 4;
                foreach (ReportParameterRelPesqUF rp in this.mFields)
                {
                    if (rp.RetType == ReportParameter.ReturnType.TextOnly)
                    {
                        if (rp.Campo == ReportParameterRelPesqUF.CamposRelPesqUF.DatasProducao)
                        {
                            // dois campos não são strings (são booleans)
                            ArrayList info = new ArrayList();
                            info.Add(rp.Campo);
                            ArrayList valores = new ArrayList();
                            foreach (string coluna in rp.DBField)
                            {
                                valores.Add(reader.GetValue(i));
                                i++;
                            }
                            info.Add(valores);
                            uf.InfoAdicional.Add(info);
                        }
                        else
                        {
                            // todos os campos são strings
                            ArrayList info = new ArrayList();
                            info.Add(rp.Campo);
                            List<string> valores = new List<string>();
                            foreach (string coluna in rp.DBField)
                            {
                                valores.Add(reader.GetValue(i).ToString());
                                i++;
                            }
                            info.Add(valores);
                            uf.InfoAdicional.Add(info);
                        }
                    }
                }

				unidadesFisicas.Add(uf);
                ufs.Add(uf.ID, uf);

                DoAddedEntries(1);
			}
            reader.NextResult();

            List<ReportParameterRelPesqUF.CamposRelPesqUF> fields = Fields(this.mFields);
            if (fields.Contains(ReportParameterRelPesqUF.CamposRelPesqUF.UnidadesInformacionaisAssociadas))
            {
                while (reader.Read())
                {
                    uf = GetExistentUF(System.Convert.ToInt64(reader.GetValue(0)));
                    if (uf != null)
                    {
                        uf.UnidadesInformacionais.Add(new ArrayList() { reader.GetValue(1), reader.GetValue(2) });
                    }
                }
                reader.NextResult();
            }
		}

        private List<ReportParameterRelPesqUF.CamposRelPesqUF> Fields(List<ReportParameter> parameters)
        {
            List<ReportParameterRelPesqUF.CamposRelPesqUF> fields = new List<ReportParameterRelPesqUF.CamposRelPesqUF>();
            foreach (ReportParameterRelPesqUF rp in parameters)
                fields.Add(rp.Campo);
            return fields;
        }
		protected override void FillContents() {
			// este paragrafo é necessário para contornar o facto (bug?)
			// de que uma tabela adicionada logo a seguir ao título principal
			// é apresentada sempre com uma distancia enorme deste.
			Paragraph p = new Paragraph(string.Empty);
			base.mDoc.Add(p);

			foreach (UnidadeFisica uf in unidadesFisicas){
				AddUnidadeFisica(base.mDoc, uf);
				DoRemovedEntries(1);
			}
		}

		private void AddUnidadeFisica(Document doc, UnidadeFisica uf) {
			Paragraph p = new Paragraph(CentimeterToPoint(0.5F), uf.CodigoCompleto + "   " + GISA.Utils.GUIHelper.CapitalizeFirstLetter(uf.Designacao), this.BodyFont);
			p.SpacingBefore = 10f;
			p.SpacingAfter = 5f;
			doc.Add(p);

			Table ufsTable = new Table(2, 5);
            ufsTable.BorderColor = iTextSharp.text.Color.WHITE;
			ufsTable.DefaultCellBorderColor = iTextSharp.text.Color.WHITE;
			ufsTable.Width = 100;
			ufsTable.Widths = new float[] {28, 72};
			ufsTable.Alignment = Element.ALIGN_CENTER;
            foreach (ArrayList info in uf.InfoAdicional)
            {                
                List<string> campos = new List<string>();
                ReportParameterRelPesqUF.CamposRelPesqUF param = (ReportParameterRelPesqUF.CamposRelPesqUF)info[0];
                //campos = (List<string>)info[1];
                switch (param)
                {
                    case ReportParameterRelPesqUF.CamposRelPesqUF.CotaCodigoBarras:
                        campos = (List<string>)info[1];
                        if (campos[0].Length > 0 || campos[1].Length > 0)
                        {
                            AddNewCell(ufsTable, GetParameterName(param) + ":", this.HeaderFont);
                            AddNewCell(ufsTable, string.Format("{0} — {1}", campos[0], campos[1]), this.ContentFont);
                        }
                        break;
                    case ReportParameterRelPesqUF.CamposRelPesqUF.DatasProducao:
                        ArrayList camposDP = new ArrayList();
                        camposDP = (ArrayList)info[1];
                        if (camposDP[0].ToString().Length > 0 || camposDP[1].ToString().Length > 0 || camposDP[2].ToString().Length > 0 ||
                            camposDP[4].ToString().Length > 0 || camposDP[5].ToString().Length > 0 || camposDP[6].ToString().Length > 0)
                        {
                            string datasProducao = GISA.Utils.GUIHelper.FormatDateInterval(
                                GISA.Utils.GUIHelper.FormatDate(camposDP[0].ToString(), camposDP[1].ToString(), camposDP[2].ToString(), System.Convert.ToBoolean(camposDP[3])),
                                GISA.Utils.GUIHelper.FormatDate(camposDP[4].ToString(), camposDP[5].ToString(), camposDP[6].ToString(), System.Convert.ToBoolean(camposDP[7])));

                            AddNewCell(ufsTable, GetParameterName(param) + ":", this.HeaderFont);
                            AddNewCell(ufsTable, datasProducao, this.ContentFont);
                        }
                        break;
                    case ReportParameterRelPesqUF.CamposRelPesqUF.TipoDimensoes:
                        campos = (List<string>)info[1];
                        if (campos[0].Length > 0 || campos[1].Length > 0 || campos[2].Length > 0 || campos[3].Length > 0)
                        {
                            AddNewCell(ufsTable, GetParameterName(param) + ":", this.HeaderFont);
                            AddNewCell(ufsTable, string.Format("{0} com {1} x {2} x {3} m", campos[0], campos[1], campos[2], campos[3]), this.ContentFont);
                        }
                        break;
                    case ReportParameterRelPesqUF.CamposRelPesqUF.UltimaAlteracao:
                        campos = (List<string>)info[1];
                        if (campos[0].Length > 0)
                        {
                            AddNewCell(ufsTable, GetParameterName(param) + ":", this.HeaderFont);
                            AddNewCell(ufsTable, System.Convert.ToDateTime(campos[0]).ToString("yyyy-MM-dd"), this.ContentFont);
                        }
                        else
                        {
                            AddNewCell(ufsTable, GetParameterName(param) + ":", this.HeaderFont);
                            AddNewCell(ufsTable, string.Empty, this.ContentFont);
                        }
                        break;
                    case ReportParameterRelPesqUF.CamposRelPesqUF.Eliminada:
                        campos = (List<string>)info[1];
                        if (campos[0].Length > 0)
                        {
                            AddNewCell(ufsTable, GetParameterName(param) + ":", this.HeaderFont);
                            if (campos[0].ToLower().Equals("true"))
                                AddNewCell(ufsTable, campos[1], this.ContentFont);
                            else
                                AddNewCell(ufsTable, "Não", this.ContentFont);
                        }
                        break;                    
                    default:
                        campos = (List<string>)info[1];
                        if (campos[0].Length > 0)
                        {
                            AddNewCell(ufsTable, GetParameterName(param) + ":", this.HeaderFont);
                            AddNewCell(ufsTable, campos[0], this.ContentFont);
                        }
                        break;
                }
            }

            if (uf.UnidadesInformacionais.Count > 0)
            {
                System.Text.StringBuilder agregados = new System.Text.StringBuilder();
                AddNewCell(ufsTable, "Unidades Informacionais:", this.HeaderFont);
                List<string> paragraphs = new List<string>();
                foreach (ArrayList ua in uf.UnidadesInformacionais)
                    paragraphs.Add(string.Format("{1}: {0}", ua[0], ua[1]));

                AddNewCell(ufsTable, paragraphs, this.ContentFont);
            }
			ufsTable.Offset = 0f;

            AddTable(doc, ufsTable);            
		}

        private Hashtable ufs = new Hashtable();
        private UnidadeFisica GetExistentUF(long IDNivel)
        {
            if (ufs.ContainsKey(IDNivel))
            {
                return (UnidadeFisica)ufs[IDNivel];
            }
            else
            {
                return null;
            }
        }

		private ArrayList unidadesFisicas = new ArrayList();

		protected virtual bool IsDetalhado() {
			return false;
		}

		protected override string GetTitle(){
			return "Unidades físicas";			
		}

		private class UnidadeFisica {
			public long ID;
			public long IDEntidadeDetentora;
			public string CodigoCompleto;
			public string Designacao;
            public ArrayList InfoAdicional = new ArrayList();
            public ArrayList UnidadesInformacionais = new ArrayList();
		}
	}
}
