using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

using GISA.Controls;

namespace GISA
{
    public abstract partial class MasterPanelMovimentos : GISA.MasterPanel
    {
        
        #region Protected Internal Properties

        protected internal string nomeMovimento;
        protected internal string NomeMovimento
        {
            get { return this.nomeMovimento; }            
        }

        protected internal string catCode;
        protected internal string CatCode
        {
            get { return this.catCode; }
        }

        #endregion
        
        public MasterPanelMovimentos()
        {                                 

            // UI initialization
            InitializeComponent();            
           
            // Event handlers
            base.StackChanged += MasterPanelRequisicoes_StackChanged;
            ToolBar.ButtonClick += Toolbar_ButtonClick;
            movList.BeforeNewListSelection += movList_BeforeNewListSelection;
                      
        }        

        public override void UpdateToolBarButtons()
        {
            UpdateToolBarButtons(null);
        }

        public override void UpdateToolBarButtons(ListViewItem item)
        {
            tbCriar.Enabled = AllowCreate;
            tbImprimir.Enabled = true;

            ListViewItem selectedItem = null;

            if (item != null && item.ListView != null)
                selectedItem = item;
            else if (item == null && movList.SelectedItems.Count == 1)
                selectedItem = movList.SelectedItems[0];

            if (selectedItem == null || ((DataRow)selectedItem.Tag).RowState == DataRowState.Detached)
            {
                tbEditar.Enabled = false;
                tbEliminar.Enabled = false;
            }
            else
            {
                tbEditar.Enabled = AllowEdit;
                tbEliminar.Enabled = AllowDelete;
            }
        }

        private void Toolbar_ButtonClick(object Sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == tbFiltro)
                ClickBtnFiltro();
            else if (e.Button == tbCriar)
                ClickTbCriar();
            else if (e.Button == tbEditar)
                ClickTbEditar();
            else if (e.Button == tbEliminar)
                ClickTbEliminar();
        }

        private void ClickBtnFiltro()
        {
            movList.FilterVisible = tbFiltro.Pushed;
        }

        private void ClickTbCriar()
        {
            FormMovimento frm = new FormMovimento();
            frm.LoadData();
            frm.CurrentMovimento = null;
            frm.Text = "Criar nova " + this.NomeMovimento;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                // ler os valores do form e gravá-los
                GISADataset.MovimentoRow newReqRow = GisaDataSetHelper.GetInstance().Movimento.NewMovimentoRow();
                newReqRow.CatCode = this.CatCode;
                newReqRow.MovimentoEntidadeRow = frm.Entidade;
                newReqRow.Data = frm.Data;
                newReqRow.Versao = new byte[] { };
                GisaDataSetHelper.GetInstance().Movimento.AddMovimentoRow(newReqRow);

                PersistencyHelper.save();
                PersistencyHelper.cleanDeletedData();

                movList.ReloadList(newReqRow);
            }
        }

        private void ClickTbEditar()
        {
            FormMovimento frm = new FormMovimento();
            frm.Text = "Editar " + this.NomeMovimento;

            //obter row seleccionada e colocar a informação no form
            GISADataset.MovimentoRow reqRow = (GISADataset.MovimentoRow)movList.SelectedItems[0].Tag;
            frm.Entidade = reqRow.MovimentoEntidadeRow;
            frm.Data = reqRow.Data;
            frm.CurrentMovimento = reqRow;
            frm.LoadData();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                // ler os valores do form e gravá-los
                reqRow.MovimentoEntidadeRow = frm.Entidade;
                reqRow.Data = frm.Data;

                PersistencyHelper.save();
                PersistencyHelper.cleanDeletedData();

                movList.ReloadList(reqRow);
            }
        }

        private void ClickTbEliminar()
        {
            // obter a row selecciona e apresentar uma messagebox a perguntar se o utilizador quer realmente apagar a requisição
            if (MessageBox.Show("Tem a certeza que deseja eliminar a " + this.NomeMovimento + " selecionada?", "Eliminação de " + this.NomeMovimento, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;

            // Dúvida: deve ser permitido apagar requisições com documentos associados?

            ListViewItem movItem = null;
            GISADataset.MovimentoRow movRow = null;
            movItem = movList.SelectedItems[0];
            movRow = (GISADataset.MovimentoRow)movItem.Tag;

            if (movRow.RowState == DataRowState.Detached)
                movList.ClearItemSelection(movItem);
            else
            {
                var args = new PersistencyHelper.DeleteMovimentoPreConcArguments();
                args.CatCode = movRow.CatCode.Equals("REQ") ? "DEV" : "REQ";
                args.IDMovimento = movRow.ID;

                movRow.Delete();

                PersistencyHelper.save(DeleteMovimento, args);
                PersistencyHelper.cleanDeletedData();

                if (!args.continueSave)
                {
                    var message = movRow.CatCode.Equals("REQ")
                        ? "Não é permitido eliminar requisições de documentos devolvidos posteriormente"
                        : "Não é permitido eliminar devoluções com requisições posteriores (sem devolução) dos mesmos documentos";
                    MessageBox.Show(message, "Eliminar " + this.NomeMovimento, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            movItem.Remove();
            UpdateToolBarButtons();
            UpdateContext();
        }

        public static void DeleteMovimento(PersistencyHelper.PreConcArguments args)
        {
            var dmPca = args as PersistencyHelper.DeleteMovimentoPreConcArguments;
            var dmRow = GisaDataSetHelper.GetInstance().Movimento.Cast<GISADataset.MovimentoRow>()
                .Single(r => r.RowState == DataRowState.Deleted && (long)r["ID", DataRowVersion.Original] == dmPca.IDMovimento);

            // Não é permitido eliminar requisições de documentos devolvidos posteriormente nem eliminar devoluções 
            // com requisições posteriores (sem devolução) dos mesmos documentos
            dmPca.continueSave = !DBAbstractDataLayer.DataAccessRules.MovimentoRule.Current.CanDeleteMovimento(dmPca.IDMovimento, dmPca.CatCode, args.tran);

            if (dmPca.continueSave) return;

            System.Data.DataSet tempgisaBackup2 = dmPca.gisaBackup;
            PersistencyHelper.BackupRow(ref tempgisaBackup2, dmRow);
            dmPca.gisaBackup = tempgisaBackup2;
            dmRow.RejectChanges();
        }

        private void todosMovimentosToolMenuItemm_Click(object sender, EventArgs e)
        {
            var form = new FormRelatorioInput();
            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    break;
                case DialogResult.Cancel:
                    break;
            }
        }

        private void movList_BeforeNewListSelection(object sender, BeforeNewSelectionEventArgs e)
        {
            try
            {
                Debug.WriteLine("movList_BeforeNewListSelection");

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

        private void MasterPanelRequisicoes_StackChanged(frmMain.StackOperation stackOperation, bool isSupport)
        {
            switch (stackOperation)
            {
                case frmMain.StackOperation.Push:
                    if (!isSupport) {
                        //movList.ClearFiltro();
                        //tbFiltro.Pushed = false;
                        //Toolbar_ButtonClick(this, new System.Windows.Forms.ToolBarButtonClickEventArgs(tbFiltro));
                        movList.ReloadList();
                    }
                    UpdateToolBarButtons();
                    break;
                case frmMain.StackOperation.Pop:
                    //movList.ClearFiltro();
                    //movList.grpFiltro.Visible = tbFiltro.Pushed;
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
                if (movList.SelectedItems.Count == 1)
                {
                    // Apesar da contagem de items ser "1" pode acontecer, no caso de 
                    // items que tenham sido entretanto eliminados, que o SelectedItems 
                    // se encontre vazio. Nesse caso consideramos sempre que não existe selecção.
                    try
                    {
                        selectedItem = movList.SelectedItems[0];
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
                GISADataset.MovimentoRow reqRow = null;
                reqRow = (GISADataset.MovimentoRow)selectedItem.Tag;
                successfulSave = CurrentContext.SetMovimento(reqRow); //, cadRow.RowState = DataRowState.Detached)
                DelayedRemoveDeletedItems(movList.Items);
            }
            else
                successfulSave = CurrentContext.SetMovimento(null); //, False)
            return successfulSave;
        }

        private void updateContextStatusBar()
        {
            if (!(MasterPanel.isContextPanel(this)) || ((frmMain)TopLevelControl).isSuportPanel)
                return;

            if (CurrentContext.Movimento == null)
                ((frmMain)TopLevelControl).StatusBarPanelHint.Text = "";
            else
            {
                if (CurrentContext.Movimento.RowState == DataRowState.Detached)
                    ((frmMain)TopLevelControl).StatusBarPanelHint.Text = string.Empty;
                else
                    ((frmMain)TopLevelControl).StatusBarPanelHint.Text = "  " + this.NomeMovimento + ": " + CurrentContext.Movimento.MovimentoEntidadeRow.Entidade;
            }
        }
    }
}