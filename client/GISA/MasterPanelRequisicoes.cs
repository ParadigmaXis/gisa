using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA
{
    public partial class MasterPanelRequisicoes : GISA.MasterPanelMovimentos
    {        

        public MasterPanelRequisicoes()            
        {
            InitializeComponent();

            base.nomeMovimento = SharedResources.SharedResourcesOld.CurrentSharedResources.RequisicaoNome1String;
            base.catCode = SharedResources.SharedResourcesOld.CurrentSharedResources.RequisicaoCatCodeString;
            base.movList.CatCode = base.CatCode;
            base.movList.GrpResultadosLabel = SharedResources.SharedResourcesOld.CurrentSharedResources.RequisicaoNome2String;
        }

        private void documentosRequisitadosToolMenuItem_Click(object sender, EventArgs e)
        {
            Reports.RelatorioDocumentosRequisitados report =
                new Reports.RelatorioDocumentosRequisitados(string.Format("RelatorioDocumentosRequisitados_{0}", System.DateTime.Now.ToString("yyyyMMdd")),
                                                             SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);

            long ceiling = 1;

            object o = new Reports.BackgroundRunner(TopLevelControl, report, ceiling);
        }

        private void comprovativoToolMenuItem_Click(object sender, EventArgs e)
        {
            GISADataset.MovimentoRow movRow = (GISADataset.MovimentoRow)this.movList.SelectedItems[0].Tag;

            List<MovimentoRule.DocumentoMovimentado> documents = new List<MovimentoRule.DocumentoMovimentado>();
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                documents = MovimentoRule.Current.GetDocumentos(movRow.ID, GisaDataSetHelper.GetInstance(), ho.Connection);
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

            Reports.Movimentos.RelatorioMovimento report = new Reports.Movimentos.RelatorioMovimento(movRow, documents,
                string.Format("Requisicao_{0}", movRow.ID), SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
            
            object o = new Reports.BackgroundRunner(TopLevelControl, report, 1);
        }        

        public override void UpdateToolBarButtons(ListViewItem item)
        {
            base.UpdateToolBarButtons(item);
            this.documentosRequisitadosToolMenuItem.Enabled = true;
            this.comprovativoToolMenuItem.Enabled = (item != null && item.ListView != null) || (item == null && movList.SelectedItems.Count == 1);
            this.tbImprimir.Enabled = true;
        }
    }
}
