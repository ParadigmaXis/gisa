using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using GISA.Model;
using GISA.Utils;
using iTextSharp.text;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Reports
{
	/// <summary>
	/// Summary description for ControloAut.
	/// </summary>
	public class ControloAut : Relatorio
	{
		public ControloAut(string FileName, long IDTipoNoticiaAut, string subtitle, long IDTrustee): base(FileName, IDTrustee) {
			mParameters = new ArrayList();
			mParameters.Add(IDTipoNoticiaAut);
			subTitle = subtitle;
        }

        public ControloAut(string FileName, long[] IDsTipoNoticiaAut, string[] subtitle, long IDTrustee) : base(FileName, IDTrustee)
        {
            mParameters = new ArrayList();
            foreach (long ID in IDsTipoNoticiaAut)
                mParameters.Add(ID);

            subTitles = subtitle;
            mFields = null;
        }

		public ControloAut(string FileName, long[] IDsTipoNoticiaAut, string[] subtitle, List<ReportParameter> fields, long IDTrustee): this(FileName, IDsTipoNoticiaAut, subtitle, IDTrustee)
		{
            mFields = fields;
		}

		protected override void InitializeReport(IDbConnection connection)
		{
			DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.InitializeControloAut(mParameters, connection);
		}

		protected override void FinalizeReport(IDbConnection connection)
		{
			DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.FinalizeControloAut(connection);
		}
		
		//lista com as noticias de autoridade
		private Hashtable listCA = new Hashtable();
		private string subTitle; 
		private string[] subTitles;

		protected override void LoadContents(IDbConnection connection, ref IDataReader reader) 
		{            
            reader = DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.ReportControloAut(this.mFields, connection);

            if (this.mFields != null)
            {
                ControloAutoridade ep;
                long ID = 0;
                while (reader.Read())
                {
                    ID = System.Convert.ToInt64(reader.GetValue(0));
                    ep = CreateCAIfNonExistent(ID);
                    ep.Designacao = reader.GetValue(1).ToString();
                    ep.Codigo = reader.GetValue(2).ToString();

                    int i = 3;
                    foreach (ReportParameterRelEPs rp in this.mFields)
                    {
                        if (rp.RetType == ReportParameter.ReturnType.TextOnly)
                        {
                            if (rp.Campo == ReportParameterRelEPs.CamposRelEPs.DatasExistencia)
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
                                ep.InfoAdicional.Add(rp.Campo, info);
                            }
                            else
                            {
                                ArrayList info = new ArrayList();
                                info.Add(rp.Campo);
                                List<string> valores = new List<string>();
                                foreach (string coluna in rp.DBField)
                                {
                                    valores.Add(reader.GetValue(i).ToString());
                                    i++;
                                }
                                info.Add(valores);
                                ep.InfoAdicional.Add(rp.Campo, info);
                            }
                        }
                    }
                }
                reader.NextResult();

                List<ReportParameterRelEPs.CamposRelEPs> fields = Fields(this.mFields);
                if (fields.Contains(ReportParameterRelEPs.CamposRelEPs.FormaParalela) || fields.Contains(ReportParameterRelEPs.CamposRelEPs.FormaNormalizada) || fields.Contains(ReportParameterRelEPs.CamposRelEPs.OutrasFormas))
                {
                    while (reader.Read())
                    {
                        ep = GetExistentCA(System.Convert.ToInt64(reader.GetValue(0)));
                        if (ep != null)
                        {
                            int tipoNoticiaAut = System.Convert.ToInt32(reader.GetValue(2));
                            switch (tipoNoticiaAut)
                            {
                                case (int)TipoControloAutForma.FormaNormalizada:
                                    ep.FormasNormalizadas.Add(reader.GetValue(1).ToString());
                                    break;
                                case (int)TipoControloAutForma.FormaParalela:
                                    ep.FormasParalelas.Add(reader.GetValue(1).ToString());
                                    break;
                                case (int)TipoControloAutForma.OutraForma:
                                    ep.OutrasFormas.Add(reader.GetValue(1).ToString());
                                    break;
                            }
                        }
                    }
                }
                reader.NextResult();

                if (fields.Contains(ReportParameterRelEPs.CamposRelEPs.Relacoes))
                {
                    Relacao rel;
                    while (reader.Read())
                    {
                        rel = new Relacao();
                        ep = GetExistentCA(System.Convert.ToInt64(reader.GetValue(0)));
                        rel.FormaAutorizadaRelacionada = reader.GetValue(1).ToString();
                        rel.IdentificadorUnico = reader.GetValue(2).ToString();
                        rel.Categoria = reader.GetValue(3).ToString();
                        rel.InicioAno = reader.GetValue(4).ToString();
                        rel.InicioMes = reader.GetValue(5).ToString();
                        rel.InicioDia = reader.GetValue(6).ToString();
                        rel.FimAno = reader.GetValue(7).ToString();
                        rel.FimMes = reader.GetValue(8).ToString();
                        rel.FimDia = reader.GetValue(9).ToString();
                        rel.Descricao = reader.GetValue(10).ToString();
                        ep.Relacoes.Add(rel);
                    }
                }
            }
            else
            {
                ControloAutoridade ca;
                long ID = 0;
                while (reader.Read())
                {
                    ID = System.Convert.ToInt64(reader.GetValue(0));
                    ca = CreateCAIfNonExistent(ID);
                    ca.IDTipoNoticiaAut = System.Convert.ToInt64(reader.GetValue(1));
                    ca.TipoNoticiaAut = reader.GetValue(2).ToString();
                    ca.Designacao = reader.GetValue(3).ToString();
                }
            }
		}

        private List<ReportParameterRelEPs.CamposRelEPs> Fields(List<ReportParameter> parameters)
        {
            List<ReportParameterRelEPs.CamposRelEPs> fields = new List<ReportParameterRelEPs.CamposRelEPs>();
            foreach (ReportParameterRelEPs rp in parameters)
                fields.Add(rp.Campo);
            return fields;
        }


		protected override string GetTitle()
		{
			return "Notícias de Autoridade";
		}

		protected override void FillContents() 
		{
            foreach (long ID in caIDs)
            {
                if (this.mFields != null)
                    AddEntidadeProdutora(base.mDoc, cas[ID]);
                else
                {
                    AddOtherControloAutoridade(base.mDoc, cas[ID]);
                }
                DoRemovedEntries(1);

            }
		}

        // lista que preserva a ordenação dos items a apresentar nos relatórios
        private List<long> caIDs = new List<long>();
        // lista que preserva os items a apresentar nos relatórios
        private Dictionary<long, ControloAutoridade> cas = new Dictionary<long, ControloAutoridade>();        
        private ControloAutoridade CreateCAIfNonExistent(long ID)
        {
            ControloAutoridade ca;
            ca = GetExistentCA(ID);
            if (ca == null)
            {
                ca = new ControloAutoridade();
                ca.ID = ID;
                caIDs.Add(ID);
                cas.Add(ID, ca);                
                DoAddedEntries(1);
            }
            return ca;
        }

        private ControloAutoridade GetExistentCA(long ID)
        {
            if (cas.ContainsKey(ID))
                return (ControloAutoridade)cas[ID];
            else
                return null;
        }

        private long IDTipoNoticiaAut = -1;
        private void AddOtherControloAutoridade(Document doc, ControloAutoridade ca)
        {
            if (IDTipoNoticiaAut != ca.IDTipoNoticiaAut)
            {
                IDTipoNoticiaAut = ca.IDTipoNoticiaAut;
                Paragraph p = new Paragraph(CentimeterToPoint(0.5F), ca.Codigo + "   " + GISA.Utils.GUIHelper.CapitalizeFirstLetter(ca.TipoNoticiaAut), this.BodyFont);
                p.SpacingBefore = 10f;
                p.SpacingAfter = 5f;
                doc.Add(p);
            }

            IDTipoNoticiaAut = ca.IDTipoNoticiaAut;
            Paragraph p2 = new Paragraph(CentimeterToPoint(0.5F), ca.Codigo + "   " + GISA.Utils.GUIHelper.CapitalizeFirstLetter(ca.Designacao), this.ContentFont);
            doc.Add(p2);            
        }

        private void AddEntidadeProdutora(Document doc, ControloAutoridade ca)
        {
            Paragraph p = new Paragraph(CentimeterToPoint(0.5F), ca.Codigo + "   " + GISA.Utils.GUIHelper.CapitalizeFirstLetter(ca.Designacao), this.BodyFont);
            p.SpacingBefore = 10f;
            p.SpacingAfter = 5f;
            doc.Add(p);

            Table casTable = new Table(2, 1);
            casTable.BorderColor = iTextSharp.text.Color.WHITE;
            casTable.DefaultCellBorderColor = iTextSharp.text.Color.WHITE;
            casTable.Width = 100;
            casTable.Widths = new float[] { 28, 72 };
            casTable.Alignment = Element.ALIGN_CENTER;

            System.Text.StringBuilder agregados;
            ArrayList info;

            // percorrer a lista de campos selecionados pelo utilizador respeitando a ordem que aparecem na interface
            foreach (ReportParameterRelEPs param in this.mFields)
            {
                ReportParameterRelEPs.CamposRelEPs campo = param.Campo;
                if (param.RetType == ReportParameter.ReturnType.TextOnly)
                {
                    info = ca.InfoAdicional[campo];
                    List<string> campos = new List<string>();
                    switch (campo)
                    {
                        case ReportParameterRelEPs.CamposRelEPs.DatasExistencia:
                            ArrayList camposDP = new ArrayList();
                            camposDP = (ArrayList)info[1];
                            List<string> paragraphs = new List<string>();
                            if (camposDP[0].ToString().Length > 0 || camposDP[1].ToString().Length > 0 || camposDP[2].ToString().Length > 0 ||
                                camposDP[4].ToString().Length > 0 || camposDP[5].ToString().Length > 0 || camposDP[6].ToString().Length > 0)
                            {
                                string datasProducao = GISA.Utils.GUIHelper.FormatDateInterval(
                                    GISA.Utils.GUIHelper.FormatDate(camposDP[0].ToString(), camposDP[1].ToString(), camposDP[2].ToString(), System.Convert.ToBoolean(camposDP[3])),
                                    GISA.Utils.GUIHelper.FormatDate(camposDP[4].ToString(), camposDP[5].ToString(), camposDP[6].ToString(), System.Convert.ToBoolean(camposDP[7])));

                                AddNewCell(casTable, GetParameterName(param) + ":", this.HeaderFont);
                                paragraphs.Add(datasProducao);
                                paragraphs.Add(camposDP[8].ToString());
                                AddNewCell(casTable, paragraphs, this.ContentFont);
                            }
                            break;
                        case ReportParameterRelEPs.CamposRelEPs.LinguaAlfabeto:
                            campos = (List<string>)info[1];
                            if (campos[0].Length > 0 || campos[1].Length > 0)
                            {
                                AddNewCell(casTable, GetParameterName(param) + ":", this.HeaderFont);
                                AddNewCell(casTable, string.Format("{0} / {1} ", campos[0], campos[1]), this.ContentFont);
                            }
                            break;
                        default:
                            campos = (List<string>)info[1];
                            if (campos[0].Length > 0)
                            {
                                AddNewCell(casTable, GetParameterName(param) + ":", this.HeaderFont);
                                AddNewCell(casTable, campos[0], this.ContentFont);
                            }
                            break;
                    }
                }
                else
                {
                    agregados = new System.Text.StringBuilder();
                    switch (campo)
                    {
                        case ReportParameterRelEPs.CamposRelEPs.FormaParalela:
                            if (ca.FormasParalelas.Count > 0)
                            {
                                AddNewCell(casTable, GetParameterName(param) + ":", this.HeaderFont);
                                List<string> paragraphs = new List<string>();
                                foreach (string forma in ca.FormasParalelas)
                                    paragraphs.Add("• " + forma);

                                AddNewCell(casTable, paragraphs, this.ContentFont);
                            }
                            break;
                        case ReportParameterRelEPs.CamposRelEPs.FormaNormalizada:
                            if (ca.FormasNormalizadas.Count > 0)
                            {
                                AddNewCell(casTable, GetParameterName(param) + ":", this.HeaderFont);
                                List<string> paragraphs = new List<string>();
                                foreach (string forma in ca.FormasNormalizadas)
                                    paragraphs.Add("• " + forma);

                                AddNewCell(casTable, paragraphs, this.ContentFont);
                            }
                            break;
                        case ReportParameterRelEPs.CamposRelEPs.OutrasFormas:
                            if (ca.OutrasFormas.Count > 0)
                            {
                                AddNewCell(casTable, GetParameterName(param) + ":", this.HeaderFont);
                                List<string> paragraphs = new List<string>();
                                foreach (string forma in ca.OutrasFormas)
                                    paragraphs.Add("• " + forma);

                                AddNewCell(casTable, paragraphs, this.ContentFont);
                            }
                            break;
                        case ReportParameterRelEPs.CamposRelEPs.Relacoes:
                            if (ca.Relacoes.Count > 0)
                            {
                                AddNewCell(casTable, GetParameterName(param) + ":", this.HeaderFont);
                                List<string> paragraphs = new List<string>();
                                foreach (Relacao rel in ca.Relacoes)
                                {
                                    paragraphs.Add("• " + rel.FormaAutorizadaRelacionada);
                                    if (rel.IdentificadorUnico.Length > 0)
                                        paragraphs.Add("Identificador único: " + rel.IdentificadorUnico);
                                    paragraphs.Add("Categoria: " + rel.Categoria);
                                    if (rel.InicioAno.Length > 0 || rel.InicioMes.Length > 0 || rel.InicioDia.Length > 0 || rel.FimAno.Length > 0 || rel.FimMes.Length > 0 || rel.FimDia.Length > 0)
                                        paragraphs.Add("Data de relação: " + GISA.Utils.GUIHelper.FormatDateInterval(
                                                GISA.Utils.GUIHelper.FormatDate(rel.InicioAno, rel.InicioMes, rel.InicioDia),
                                                GISA.Utils.GUIHelper.FormatDate(rel.FimAno, rel.FimMes, rel.FimDia)));
                                    if (rel.Descricao.Length > 0)
                                        paragraphs.Add("Descrição: " + rel.Descricao);
                                }
                                AddNewCell(casTable, paragraphs, this.ContentFont);
                            }
                            break;
                    }
                }
            }
            
            casTable.Offset = 0f;
            AddTable(doc, casTable);
        }

        private class ControloAutoridade
        {
            public long ID;
            public string Designacao;
            //exclusico de entidades produtoras
            public string Codigo;
            public Dictionary<ReportParameterRelEPs.CamposRelEPs, ArrayList> InfoAdicional = new Dictionary<ReportParameterRelEPs.CamposRelEPs,ArrayList>();
            public List<string> FormasParalelas = new List<string>();
            public List<string> FormasNormalizadas = new List<string>();
            public List<string> OutrasFormas = new List<string>();
            public List<Relacao> Relacoes = new List<Relacao>();
            //exclusivo de conteúdos e tipologias
            public long IDTipoNoticiaAut;
            public string TipoNoticiaAut;
        }

        private class Relacao
        {
            public string FormaAutorizadaRelacionada;
            public string IdentificadorUnico;
            public string Categoria;
            public string InicioAno;
            public string InicioMes;
            public string InicioDia;
            public string FimAno;
            public string FimMes;
            public string FimDia;
            public string Descricao;
        }
	}
}
