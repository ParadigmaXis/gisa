using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

using iTextSharp.text;

namespace GISA.Reports.Movimentos
{
    public class RelatorioTodosMovimentos : Relatorio
    {
        private List<Movimento> movimentos;

        public RelatorioTodosMovimentos(string FileName, ArrayList parameters, long IDTrustee) : base(FileName, parameters, IDTrustee) { }

        protected override string GetTitle()
        {
            return string.Empty;
        }

        protected override string GetSubTitle()
        {
            return "Relatório de movimentos";
        }

        protected override void LoadContents(IDbConnection connection, ref IDataReader reader)
        {
            try
            {
                var data_inicio = (DateTime)this.mParameters[0];
                var data_fim = (DateTime)this.mParameters[1];
                reader = DBAbstractDataLayer.DataAccessRules.MovimentoRule.Current.GetAllMovimentos(data_inicio, data_fim, connection);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

            var mov = default(Movimento);
            movimentos = new List<Movimento>();

            while (reader.Read())
            {
                mov = new Movimento();
                mov.IDDocumento = reader.GetInt64(0);
                mov.CodDocumento = reader.GetString(1);
                mov.TituloDocumento = reader.GetString(2);
                mov.IDMovimento = reader.GetInt64(3);
                mov.TipoMovimento = reader.GetString(4);
                mov.DataMovimento = reader.GetValue(5).ToString();
                mov.Entidade = reader.GetString(6);

                movimentos.Add(mov);
                DoAddedEntries(1);
            }
        }

        protected override void FillContents()
        {
            Table detailsTable = new Table(7, 1);

            //este offset deve passar novamente para 0 caso se volte a usar Tables em vez de PdfTables
            detailsTable.Offset = 3;
            detailsTable.Width = 100;
            detailsTable.BorderColor = iTextSharp.text.Color.LIGHT_GRAY;
            detailsTable.DefaultCellBorderColor = iTextSharp.text.Color.LIGHT_GRAY;
            detailsTable.Padding = 3;
            detailsTable.CellsFitPage = true;

            //float indentPercent = (0.0f + 5.0f) * 100f / 21.6f;
            // uma página A4 tem 21.6 cm
            detailsTable.Widths = new float[] { 8, 10, 15, 15, 8, 15, 100 - 8 - 10 - 15 - 15 - 8 - 15 };

            AddNewCell(detailsTable, "Ident. mov.", this.BodyFont);
            AddNewCell(detailsTable, "Tipo mov.", this.BodyFont);
            AddNewCell(detailsTable, "Data mov.", this.BodyFont);
            AddNewCell(detailsTable, "Entidade", this.BodyFont);
            AddNewCell(detailsTable, "Ident. doc.", this.BodyFont);
            AddNewCell(detailsTable, "Código Referência", this.BodyFont);
            AddNewCell(detailsTable, "Título doc.", this.BodyFont);

            foreach (Movimento mov in movimentos)
            {
                AddMovimento(detailsTable, mov);
                DoRemovedEntries(1);
            }

            AddTable(base.mDoc, detailsTable);
        }

        private void AddMovimento(Table detailsTable, Movimento mov)
        {
            AddNewCell(detailsTable, mov.IDMovimento.ToString(), this.ContentFont);
            AddNewCell(detailsTable, mov.TipoMovimento.Equals("REQ") ? "Requisição" : "Devolução", this.ContentFont);
            AddNewCell(detailsTable, mov.DataMovimento, this.ContentFont);
            AddNewCell(detailsTable, mov.Entidade, this.ContentFont);
            AddNewCell(detailsTable, mov.IDDocumento.ToString(), this.ContentFont);
            AddNewCell(detailsTable, mov.CodDocumento, this.ContentFont);
            AddNewCell(detailsTable, mov.TituloDocumento, this.ContentFont);
        }

        protected class Movimento
        {
            public long IDDocumento;
            public string CodDocumento;
            public string TituloDocumento;
            public long IDMovimento;
            public string TipoMovimento;
            public string DataMovimento;
            public string Entidade;
        }
    }
}
