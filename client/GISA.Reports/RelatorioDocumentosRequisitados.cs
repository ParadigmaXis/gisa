using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Data;

using iTextSharp.text;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Reports
{
    public class RelatorioDocumentosRequisitados : Relatorio {

        private List<MovimentoRule.DocumentoRequisicaoInfo> documentos = new List<MovimentoRule.DocumentoRequisicaoInfo>(0);
 
        public RelatorioDocumentosRequisitados(string FileName, long IDTrustee) : base(FileName, IDTrustee) { }

        public override Font TitleFont {
            get {
                return base.SubSubTitleFont;
            }
        }

        protected override string GetTitle() {
            return String.Format("Documentos requisitados e não devolvidos em {0}", DateTime.Now.ToString("yyyy-MM-dd"));
        }

        public long getCeiling(IDbConnection connection) {
            return DBAbstractDataLayer.DataAccessRules.MovimentoRule.Current.getCountDocumentosNaoDevolvidos(connection);
        }

        protected override void LoadContents(IDbConnection connection, ref IDataReader reader) {
            try {
                this.documentos = DBAbstractDataLayer.DataAccessRules.MovimentoRule.Current.getDocumentosNaoDevolvidos(connection);
            }
            catch (Exception e) {
                Debug.WriteLine(e);
                throw;
            }

            foreach (MovimentoRule.DocumentoRequisicaoInfo doc in this.documentos)
                DoAddedEntries(1);
        }

        protected override void FillContents() {
            Table detailsTable = new Table(6, 1);

            //este offset deve passar novamente para 0 caso se volte a usar Tables em vez de PdfTables
            detailsTable.Offset = 3;
            detailsTable.Width = 100;
            detailsTable.BorderColor = iTextSharp.text.Color.LIGHT_GRAY;
            detailsTable.DefaultCellBorderColor = iTextSharp.text.Color.LIGHT_GRAY;
            detailsTable.Padding = 3;
            detailsTable.CellsFitPage = true;

            //float indentPercent = (0.0f + 5.0f) * 100f / 21.6f;
            // uma página A4 tem 21.6 cm
            //detailsTable.Widths = new float[] { 10, indentPercent, 19, 100 - 19 - 19 - 10 - indentPercent }; //, 19 };
            detailsTable.Widths = new float[] { 20, 20, 20, 20, 20, 20 };

            AddNewCell(detailsTable, "Identificador do Documento", this.BodyFont);
            AddNewCell(detailsTable, "Código Referência", this.BodyFont);
            AddNewCell(detailsTable, "Designação", this.BodyFont);
            AddNewCell(detailsTable, "Identificador do Movimento", this.BodyFont);
            AddNewCell(detailsTable, "Data de Requisição", this.BodyFont);
            AddNewCell(detailsTable, "Entidade e notas", this.BodyFont);

            foreach (MovimentoRule.DocumentoRequisicaoInfo doc in this.documentos) {
                AddDocumentoRequisitado(detailsTable, doc);
                DoRemovedEntries(1);
            }

            AddTable(base.mDoc, detailsTable);
        }

        private void AddDocumentoRequisitado(Table detailsTable, MovimentoRule.DocumentoRequisicaoInfo documento) {
            AddNewCell(detailsTable, documento.idNivel.ToString(), this.ContentFont);
            AddNewCell(detailsTable, documento.Codigo_Completo, this.ContentFont);
            AddNewCell(detailsTable, documento.ND_Designacao, this.ContentFont);
            // Identificador movimento
            AddNewCell(detailsTable, documento.idMovimento.ToString(), this.ContentFont);
            // Data:
            AddNewCell(detailsTable, documento.data.ToString(), this.ContentFont);
            // Entidade + Notas
            AddNewCell(detailsTable, documento.entidade + "\n" + documento.notas, this.ContentFont);
        }

    }
}
