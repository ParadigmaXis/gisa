using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Controls;
using GISA.Model;
using GISA.SharedResources;

namespace GISA
{
    public partial class MasterPanelNivelImagensIlustracao : GISA.MasterPanel
    {
        public MasterPanelNivelImagensIlustracao()
        {
            InitializeComponent();

            base.StackChanged += MasterPanelNivelImagensIlustracao_StackChanged;
            nivelGrupoArquivosList1.BeforeNewListSelection += nivelGrupoArquivosList1_BeforeNewListSelection;

            this.lblFuncao.Text = "Imagens de ilustração";
        }

        public override void LoadData()
        {
            this.nivelGrupoArquivosList1.ReloadList();
        }

        private void nivelGrupoArquivosList1_BeforeNewListSelection(object sender, BeforeNewSelectionEventArgs e)
        {
            if (e.ItemToBeSelected.Tag != null)
                PermissoesHelper.UpdateNivelPermissions((GISADataset.NivelRow)e.ItemToBeSelected.Tag, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);

            try
            {
                Debug.WriteLine("nivelGrupoArquivosList1_BeforeNewListSelection");

                e.SelectionChange = UpdateContext(e.ItemToBeSelected);
                if (e.SelectionChange)
                {
                    updateContextStatusBar();
                    UpdateToolBarButtons(e.ItemToBeSelected);
                }
            }
            catch (GISA.Search.UpdateServerException)
            { }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
        }

        private void MasterPanelNivelImagensIlustracao_StackChanged(frmMain.StackOperation stackOperation, bool isSupport)
        {
            switch (stackOperation)
            {
                case frmMain.StackOperation.Push:
                    if (!isSupport)
                        nivelGrupoArquivosList1.ReloadList();
                    break;
                case frmMain.StackOperation.Pop:
                    break;
            }
        }

        public override bool UpdateContext()
        {
            return UpdateContext(null);
        }

        public override bool UpdateContext(ListViewItem item)
        {
            ListViewItem selectedItem = null;
            bool successfulSave = false;

            if (item == null)
            {
                if (nivelGrupoArquivosList1.SelectedItems.Count == 1)
                {
                    // Apesar da contagem de items ser "1" pode acontecer, no caso de 
                    // items que tenham sido entretanto eliminados, que o SelectedItems 
                    // se encontre vazio. Nesse caso consideramos sempre que não existe selecção.
                    try
                    {
                        selectedItem = nivelGrupoArquivosList1.SelectedItems[0];
                    }
                    catch (ArgumentException)
                    {
                        selectedItem = null;
                    }
                }
            }
            else if (item.ListView != null)
                selectedItem = item;

            if (selectedItem != null)
            {
                GISADataset.NivelRow gaRow = null;
                gaRow = (GISADataset.NivelRow)selectedItem.Tag;
                successfulSave = CurrentContext.SetGrupoArquivo(gaRow); //, cadRow.RowState = DataRowState.Detached)
                DelayedRemoveDeletedItems(nivelGrupoArquivosList1.Items);
            }
            else
                successfulSave = CurrentContext.SetGrupoArquivo(null); //, False)
            return successfulSave;
        }

        private void updateContextStatusBar()
        {
            if (!(MasterPanel.isContextPanel(this)) || ((frmMain)TopLevelControl).isSuportPanel)
                return;

            if (CurrentContext.GrupoArquivo == null)
                ((frmMain)TopLevelControl).StatusBarPanelHint.Text = "";
            else
            {
                if (CurrentContext.GrupoArquivo.RowState == DataRowState.Detached)
                    ((frmMain)TopLevelControl).StatusBarPanelHint.Text = string.Empty;
                else
                    ((frmMain)TopLevelControl).StatusBarPanelHint.Text = "  " + CurrentContext.GrupoArquivo.GetNivelDesignadoRows()[0].Designacao;
            }
        }
    }
}
