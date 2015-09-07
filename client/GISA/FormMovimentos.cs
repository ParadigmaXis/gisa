using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using GISA.Model;
using GISA.SharedResources;

namespace GISA
{
    public partial class FormMovimento : Form
    {
        public FormMovimento()
        {
            InitializeComponent();
            GetExtraResources();
            UpdateButtonState(null);

            entidadeList1.KeyUp += new KeyEventHandler(entidadeList1_KeyUp);
            entidadeList1.EntSelectedIndexChanged += new EventHandler(lstVwEntidades_SelectedIndexChanged);
            ToolBar1.ButtonClick += new ToolBarButtonClickEventHandler(ToolBar1_ButtonClick);
        }

        public GISADataset.MovimentoEntidadeRow Entidade {
            get {
                if (entidadeList1.SelectedItems.Count > 0) return entidadeList1.SelectedItems[0].Tag as GISADataset.MovimentoEntidadeRow;
                else return null;
            }
            set {
                entidadeList1.ReloadList(value);
            }
        }

        public GISADataset.MovimentoRow CurrentMovimento { get; set; }

        private GISADataset.MovimentoEntidadeRow mGhostEntity = null;
        private GISADataset.MovimentoEntidadeRow GhostEntity
        {
            get
            {
                if (mGhostEntity == null)
                {
                    mGhostEntity = GisaDataSetHelper.GetInstance().MovimentoEntidade.NewMovimentoEntidadeRow();
                    mGhostEntity.Activo = false;
                    mGhostEntity.Codigo = string.Empty;
                    mGhostEntity.Entidade = string.Empty;
                    GisaDataSetHelper.GetInstance().MovimentoEntidade.AddMovimentoEntidadeRow(mGhostEntity);
                }
                return mGhostEntity;
            }
        }
        
        public DateTime Data {
            get { return this.dtpData.Value; }
            set { this.dtpData.Value = value; }
        }

        private void lstVwEntidades_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateEditGroup();
        }

        private void entidadeList1_KeyUp(object sender, KeyEventArgs e) {
            UpdateEditGroup();
        }

        private void UpdateEditGroup() {
            if (entidadeList1.SelectedItems.Count > 0) UpdateButtonState(entidadeList1.SelectedItems[0]);
            else UpdateButtonState(null);
        }

        void ToolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e) {
            if (e.Button == ToolBarButtonFilter) {
                entidadeList1.FilterVisible = ToolBarButtonFilter.Pushed;
                ToolBarButtonNew.Pushed = false;
                ToolBarButtonEdit.Pushed = false;
                entidadeList1.DisableEdit();
            } else {
                if (e.Button == ToolBarButtonNew) {
                    entidadeList1.AddEntidadeMovimento();
                } else if (e.Button == ToolBarButtonEdit) {
                    if (e.Button.Pushed) {
                        entidadeList1.EnableEdit();
                    } else {
                        entidadeList1.DisableEdit();
                    }
                } else if (e.Button == ToolBarButtonDelete) {
                    var selectedEntity = entidadeList1.SelectedItems[0].Tag as GISADataset.MovimentoEntidadeRow;
                    if (CanDeleteEntity(selectedEntity))
                        entidadeList1.DeleteSelectedEntidade();
                    else
                        MessageBox.Show("Só é permitido apagar entidades requerentes que ainda não estejam associadas a requisições/devoluções.", "Eliminação de entidades requerentes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                Model.PersistencyHelper.save();
                Model.PersistencyHelper.cleanDeletedData();
            }

            UpdateEditGroup();
        }

        private bool CanDeleteEntity(GISADataset.MovimentoEntidadeRow movimentoEntidadeRow)
        {
            var canDelete = true;
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
                canDelete = DBAbstractDataLayer.DataAccessRules.MovimentoRule.Current.CanDeleteEntity(movimentoEntidadeRow.ID, ho.Connection);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            finally
            {
                ho.Dispose();
            }
            return canDelete;
        }

        public void LoadData() {
            if (CurrentMovimento != null)
            {
                var currentEntity = CurrentMovimento.MovimentoEntidadeRow as GISADataset.MovimentoEntidadeRow;
                entidadeList1.ReloadList(currentEntity);
            }
            else
                entidadeList1.ReloadList();

            entidadeList1.Enabled = true;
        }

        private void GetExtraResources() {
            ToolBar1.ImageList = SharedResourcesOld.CurrentSharedResources.DMManipulacaoImageList;

            string[] strs = SharedResourcesOld.CurrentSharedResources.EntidadeManipulacaoStrings;
            ToolBarButtonNew.ToolTipText = strs[0];
            ToolBarButtonEdit.ToolTipText = strs[1];
            ToolBarButtonDelete.ToolTipText = strs[2];
            ToolBarButtonFilter.ToolTipText = strs[3];
        }

        private void UpdateButtonState(ListViewItem item) {
            ToolBarButtonFilter.Enabled = !ToolBarButtonEdit.Pushed;
            ToolBarButtonNew.Enabled = !ToolBarButtonEdit.Pushed;
            ToolBarButtonEdit.Enabled = (item != null && item.ListView != null);
            ToolBarButtonDelete.Enabled = (item != null && item.ListView != null && !ToolBarButtonEdit.Pushed);
            btnConfirmar.Enabled = (item != null && item.ListView != null && !ToolBarButtonEdit.Pushed && !ToolBarButtonFilter.Pushed);
            btnCancelar.Enabled = CurrentMovimento == null || (CurrentMovimento != null && CurrentMovimento.MovimentoEntidadeRow != null);
            if (item != null) {
                GISADataset.MovimentoEntidadeRow eRow = item.Tag as GISADataset.MovimentoEntidadeRow;
                btnConfirmar.Enabled = eRow.Activo;
            }
        }
    }
}
