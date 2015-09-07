using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA
{
    public partial class MasterPanelDevolucoes : GISA.MasterPanelMovimentos
    {        
        public MasterPanelDevolucoes()            
        {            
            InitializeComponent();

            // Globalized strings
            System.Resources.ResourceManager rm = new System.ComponentModel.ComponentResourceManager(typeof(MasterPanelDevolucoes));

            base.nomeMovimento = SharedResources.SharedResourcesOld.CurrentSharedResources.DevolucaoNome1String;
            base.catCode = SharedResources.SharedResourcesOld.CurrentSharedResources.DevolucaoCatCodeString;
            base.movList.CatCode = base.CatCode;
            base.movList.GrpResultadosLabel = SharedResources.SharedResourcesOld.CurrentSharedResources.DevolucaoNome2String;           
        }

        /// <summary>
        /// Toolbar button imprimir handler
        /// </summary>
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
                string.Format("Devolucao_{0}", movRow.ID), SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);

            long ceiling = 1;
            //GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetTempConnection());
            //try { ceiling = report.getCeiling(ho.Connection); }
            //catch (Exception ex) {
            //    Trace.WriteLine(ex);
            //    throw;
            //}
            //finally {
            //    ho.Dispose();
            //}

            object o = new Reports.BackgroundRunner(TopLevelControl, report, ceiling);
        }

        public override void UpdateToolBarButtons(ListViewItem item)
        {
            base.UpdateToolBarButtons(item);
            this.comprovativoToolMenuItem.Enabled = this.movList.SelectedItems.Count > 0;
            this.tbImprimir.Enabled = true;
        }
    }
}
