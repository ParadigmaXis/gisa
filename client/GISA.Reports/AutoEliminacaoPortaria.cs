using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

using iTextSharp.text;

using GISA.Model;

namespace GISA.Reports
{
    public class AutoEliminacaoPortaria : Relatorio
    {
        private GISADataset.AutoEliminacaoRow mAutoEliminacaoRow;
		public AutoEliminacaoPortaria(string FileName, GISADataset.AutoEliminacaoRow aeRow, long IDTrustee) : base(FileName, IDTrustee) {
			this.mAutoEliminacaoRow = aeRow;
		}

        protected override string GetTitle()
        {
            return string.Empty;
        }

        private enum AutoEliminacaoPortariaColumns
        {
            NroOrdem = 0,
            IDSerie = 1,
            IDTipoAcond = 2,
            Designacao = 3,
            NroUfsPorTipo = 4,
            DataInicio = 5,
            DataFim = 6,
            Metragem = 7
        }

        private enum SerieInfoColumns
        {
            IDSerie = 0,
            Info = 1
        }

        private enum UFsCotasGuiasColumns
        {
            IDSerie = 0,
            IDTipoAcond = 1,
            Cota = 2,
            Guia = 3
        }

        private OrderedDictionary entries = new OrderedDictionary();
        private Dictionary<long, List<string>> serieSuportes = new Dictionary<long, List<string>>();
        private Dictionary<long, List<string>> serieRefsTab = new Dictionary<long, List<string>>();

        protected override void LoadContents(IDbConnection connection, ref IDataReader reader)
        {
            if (mAutoEliminacaoRow == null)
                throw new ArgumentNullException("Auto de eliminação não especificado");

            reader = DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.ReportAutoEliminacaoPortaria(this.mIDTrustee, mAutoEliminacaoRow.ID, connection);

            Entry entry ;
				
			long IDSerie = -1;
            long IDTipoAcond = -1;

            // carregar os suportes das séries
            while (reader.Read())
            {
                IDSerie = System.Convert.ToInt64(reader.GetValue((int)SerieInfoColumns.IDSerie));
                
                if (!serieSuportes.ContainsKey(IDSerie))
                    serieSuportes.Add(IDSerie, new List<string>());

                serieSuportes[IDSerie].Add(reader.GetValue((int)SerieInfoColumns.Info).ToString());
            }

            reader.NextResult();

            // carregar as referências na tabela das séries
            while (reader.Read())
            {
                IDSerie = System.Convert.ToInt64(reader.GetValue((int)SerieInfoColumns.IDSerie));

                if (!serieRefsTab.ContainsKey(IDSerie))
                    serieRefsTab.Add(IDSerie, new List<string>());

                serieRefsTab[IDSerie].Add(reader.GetValue((int)SerieInfoColumns.Info).ToString());
            }

            reader.NextResult();
			
            // carregar cada entrada referente na tabela do relatório
            string dataInicio = string.Empty;
            string dataFim = string.Empty;
            while (reader.Read())
            {
                IDSerie = System.Convert.ToInt64(reader.GetValue((int)AutoEliminacaoPortariaColumns.IDSerie));
                IDTipoAcond = System.Convert.ToInt64(reader.GetValue((int)AutoEliminacaoPortariaColumns.IDTipoAcond));
                entry = new Entry(IDSerie, IDTipoAcond);
                entries.Add(IDSerie + "," + IDTipoAcond, entry);

                entry.NroOrdem = System.Convert.ToInt64(reader.GetValue((int)AutoEliminacaoPortariaColumns.NroOrdem));
                entry.Designacao = reader.GetString((int)AutoEliminacaoPortariaColumns.Designacao);
                dataInicio = reader.GetValue((int)AutoEliminacaoPortariaColumns.DataInicio).ToString();
                dataFim = reader.GetValue((int)AutoEliminacaoPortariaColumns.DataFim).ToString();
                entry.DatasExtremas = GISA.Utils.GUIHelper.FormatDateInterval(
                    GISA.Utils.GUIHelper.FormatDate(
                        dataInicio.Substring(0,4),
                        dataInicio.Substring(4,2),
                        dataInicio.Substring(6,2)),
                    GISA.Utils.GUIHelper.FormatDate(
                        dataFim.Substring(0, 4),
                        dataFim.Substring(4, 2),
                        dataFim.Substring(6, 2)));
                entry.NroUfsPorTipo = reader.GetValue((int)AutoEliminacaoPortariaColumns.NroUfsPorTipo).ToString();
                entry.Metragem = reader.GetValue((int)AutoEliminacaoPortariaColumns.Metragem).ToString();
                if (serieSuportes.ContainsKey(IDSerie))
                    entry.SerieSuportes = serieSuportes[IDSerie];

                DoAddedEntries(1);
            }

            reader.NextResult();

            // carregar os guias e cotas da ufs
            string cota = string.Empty;
            string guia = string.Empty;
            while (reader.Read())
            {
                IDSerie = System.Convert.ToInt64(reader.GetValue((int)UFsCotasGuiasColumns.IDSerie));
                IDTipoAcond = System.Convert.ToInt64(reader.GetValue((int)UFsCotasGuiasColumns.IDTipoAcond));

                Debug.Assert(entries.Contains(IDSerie + "," + IDTipoAcond));

                entry = (Entry)entries[IDSerie + "," + IDTipoAcond];

                cota = reader.GetValue((int)UFsCotasGuiasColumns.Cota).ToString();
                guia = reader.GetValue((int)UFsCotasGuiasColumns.Guia).ToString();

                if (!entry.UFCotas.Contains(cota))
                    entry.UFCotas.Add(cota);

                if (!entry.UFGuias.Contains(guia))
                    entry.UFGuias.Add(guia);
            }
        }

        protected override void FillContents()
        {
            Paragraph identification = new Paragraph();
            identification.Add(string.Format("Auto de eliminação n.º {0}", this.mAutoEliminacaoRow.Designacao));
            identification.Alignment = Element.ALIGN_CENTER;
            identification.Font = this.SubTitleFont;

            this.mDoc.Add(identification);
            this.mDoc.Add(new Paragraph(" "));
            this.mDoc.Add(Introduction());
            this.mDoc.Add(new Paragraph(" "));
            this.mDoc.Add(new Paragraph(" "));
            Table detailsTable = Table();

            foreach (Entry entry in entries.Values)
            {
                AddNewCell(detailsTable, entry.NroOrdem.ToString(), this.ContentFont, Element.ALIGN_CENTER);
                AddNewCell(detailsTable, entry.RefTab, this.ContentFont);
                AddNewCell(detailsTable, entry.Designacao, this.ContentFont);
                AddNewCell(detailsTable, entry.NroUfsPorTipo.Replace("<","").Replace(">",""), this.ContentFont, Element.ALIGN_CENTER);
                AddNewCell(detailsTable, entry.SerieSuportes, this.ContentFont);
                AddNewCell(detailsTable, entry.DatasExtremas, this.ContentFont);
                AddNewCell(detailsTable, entry.UFGuias, this.ContentFont);
                AddNewCell(detailsTable, entry.Metragem, this.ContentFont, Element.ALIGN_CENTER);
                AddNewCell(detailsTable, entry.UFCotas, this.ContentFont);

                DoRemovedEntries(1);
            }

            AddTable(this.mDoc, detailsTable);

            this.mDoc.Add(new Paragraph(" "));
            this.mDoc.Add(new Paragraph(" "));
            foreach (Paragraph p in Signatures())
                this.mDoc.Add(p);
            this.mDoc.Add(new Paragraph(" "));
            foreach (Paragraph p in Notes())
                this.mDoc.Add(p);
        }

        private Paragraph Introduction()
        {
            string space = " ";
            Paragraph introduction = new Paragraph(20f);
            introduction.Alignment = Element.ALIGN_JUSTIFIED;

            Font underline = new Font(introduction.Font);
            underline.SetStyle(Font.UNDERLINE);

            introduction.Add("Aos ");

            Phrase day = new Phrase(space.PadLeft(4), underline);
            introduction.Add(day);

            introduction.Add(" dias do mês de ");

            Phrase month = new Phrase(space.PadLeft(20), underline);
            introduction.Add(month);

            introduction.Add(" de ");

            Phrase year = new Phrase(space.PadLeft(8), underline);
            introduction.Add(year);

            introduction.Add(" no(a) ");

            Phrase space6 = new Phrase(space.PadLeft(50), underline);
            introduction.Add(space6);
            introduction.Add(new Phrase("  "));
            Phrase space7 = new Phrase(space.PadLeft(130), underline);
            introduction.Add(space7);

            introduction.Add(" na presença dos abaixo assinados, procedeu-se à inutilização por de acordo com o(s) artigo(s) ");

            Phrase space10 = new Phrase(space.PadLeft(20), underline);
            introduction.Add(space10);

            introduction.Add(" da Portaria n.º ");

            introduction.Add(year);
            introduction.Add(" / ");
            introduction.Add(year);

            introduction.Add(" e disposições da tabela de seleção, dos documentos, a seguir identificados: ");

            return introduction;
        }

        private List<Paragraph> Signatures()
        {
            Paragraph signature1 = new Paragraph(20f);
            Paragraph signature2 = new Paragraph(20f);
            Paragraph signature3 = new Paragraph(20f);

            Font underline = new Font(signature1.Font);
            underline.SetStyle(Font.UNDERLINE);

            string spaces = " ";
            spaces = spaces.PadLeft(70);
            Phrase line = new Phrase(spaces, underline);
            signature1.Add(line);
            signature1.Add(new Phrase("(1)", this.ContentFont));
            signature2.Add(line);
            signature2.Add(new Phrase("(2)", this.ContentFont));
            signature3.Add(line);
            signature3.Add(new Phrase("(3)", this.ContentFont));

            List<Paragraph> signatures = new List<Paragraph>() { signature1, signature2, signature3 };            
            return signatures;
        }

        private List<Paragraph> Notes()
        {
            Paragraph note1 = new Paragraph(8f, "(1) Responsável pelo serviço produtor", this.ContentBoldFont);
            note1.FirstLineIndent = 30f;
            Paragraph note2 = new Paragraph(8f, "(2) Responsável pelo Arquivo", this.ContentBoldFont);
            note2.FirstLineIndent = 30f;
            Paragraph note3 = new Paragraph(8f, "(3) Representante da Autarquia Local", this.ContentBoldFont);
            note3.FirstLineIndent = 30f;

            return new List<Paragraph>() { note1, note2, note3 };
        }

        private Table Table()
        {
            Table t = new Table(9);
            t.Padding = 3f;
            t.Width = 115;
            t.Widths = new float[] { 5, 6, 20, 11, 7, 8, 14, 8, 9 };

            AddNewCell(t, "N.º de ordem", this.HeaderFont, Element.ALIGN_CENTER);
            AddNewCell(t, "N.º de Ref.ª da tabela", this.HeaderFont, Element.ALIGN_CENTER);
            AddNewCell(t, "Título da série ou subsérie", this.HeaderFont, Element.ALIGN_CENTER);
            AddNewCell(t, "N.º e tipo de unidades de instalação", this.HeaderFont, Element.ALIGN_CENTER);
            AddNewCell(t, "Suporte", this.HeaderFont, Element.ALIGN_CENTER);
            AddNewCell(t, "Datas extremas", this.HeaderFont, Element.ALIGN_CENTER);
            AddNewCell(t, "N.º de Guia de Remessa", this.HeaderFont, Element.ALIGN_CENTER);
            AddNewCell(t, "Metragem", this.HeaderFont, Element.ALIGN_CENTER);
            AddNewCell(t, "Cota", this.HeaderFont, Element.ALIGN_CENTER);

            return t;
        }

        private class Entry
        {
            public Entry(long IDSerie, long IDTipoSuporte)
            {
                this.IDSerie = IDSerie;
                this.IDTipoAcond = IDTipoSuporte;
            }
            public long NroOrdem;
            public long IDSerie;
            public long IDTipoAcond;
            public string Designacao;
            public string DatasExtremas;
            public List<string> RefTab = new List<string>();
            public string NroUfsPorTipo;
            public List<string> UFCotas = new List<string>();
            public List<string> UFGuias = new List<string>();
            public List<string> SerieSuportes = new List<string>();
            public string Metragem;
        }
    }
}