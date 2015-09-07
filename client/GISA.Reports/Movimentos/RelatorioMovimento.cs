using System;
using System.Collections.Generic;
using System.Text;

using iTextSharp.text;
using iTextSharp.text.pdf.draw;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Reports.Movimentos
{
    public class RelatorioMovimento : Relatorio
    {
        private GISADataset.MovimentoRow movimento;
        private List<MovimentoRule.DocumentoMovimentado> documents;

        public RelatorioMovimento(GISADataset.MovimentoRow movimento, List<MovimentoRule.DocumentoMovimentado> documents, string fileName, long idTrustee)
            :base(fileName, idTrustee)
        {
            this.movimento = movimento;
            this.documents = documents;
        }

        protected override string GetTitle()
        {            
            return "";
        }

        protected override string GetSubTitle()
        {
            // Globalized strings
            System.Resources.ResourceManager rm = new System.ComponentModel.ComponentResourceManager(typeof(RelatorioMovimento));
          
            string nomeMovimento = "";

            if (this.movimento.CatCode.Trim().Equals("DEV"))
            {
                nomeMovimento = rm.GetString("Devolucao");
            }
            else if (this.movimento.CatCode.Trim().Equals("REQ"))
            {
                nomeMovimento = rm.GetString("Requisicao");
            }

            return string.Format("{0} Nº {1}", nomeMovimento, this.movimento.ID);
        }

        protected override void FillContents()
        {
                        

            Paragraph dateParagraph = new Paragraph("Data: " + this.movimento.Data.ToShortDateString());
            this.mDoc.Add(dateParagraph);

            Paragraph entityParagraph = new Paragraph("Entidade: " + this.movimento.MovimentoEntidadeRow.Entidade);
            this.mDoc.Add(entityParagraph);

            Paragraph notesParagraph = new Paragraph("Notas: " + (this.movimento["Notas"] == DBNull.Value ? "" : this.movimento.Notas));
            this.mDoc.Add(notesParagraph);

            Paragraph documentsParagraph = new Paragraph("Documentos: ");
            this.mDoc.Add(documentsParagraph);

            for (int i = 0; i < this.documents.Count; i++)
            {
                string id = this.documents[i].IDNivel.ToString();
                string des = this.documents[i].Designacao;
                Paragraph docParagraph = new Paragraph("     " + id + " - " + des);
                this.mDoc.Add(docParagraph);
            } 
            
            //Signatures                                              

            Paragraph separator = new Paragraph("");            
            separator.SpacingBefore = 80;

            LineSeparator hline = new LineSeparator();
            hline.Percentage = 50;
            hline.LineWidth = 0.5f;            

            Paragraph signServicoArquivo = new Paragraph("(Serviço de Arquivo)");
            signServicoArquivo.Alignment = Element.ALIGN_CENTER;            

            Paragraph signServicoProdutor = new Paragraph("(Serviço Produtor)");
            signServicoProdutor.Alignment = Element.ALIGN_CENTER;            

            this.mDoc.Add(separator);
            this.mDoc.Add(hline);            

            if (this.movimento.CatCode.Trim().Equals("DEV"))
            {
                this.mDoc.Add(signServicoArquivo);                                
            }
            else
            {
                this.mDoc.Add(signServicoProdutor);                
            }
            
            this.mDoc.Add(separator);
            this.mDoc.Add(hline);

            if (this.movimento.CatCode.Trim().Equals("DEV"))
            {
                this.mDoc.Add(signServicoProdutor);
            }
            else
            {
                this.mDoc.Add(signServicoArquivo);
            }            
            
        }
    }
}
