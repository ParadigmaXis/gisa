using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.Controls.Localizacao;
using GISA.Controls.Nivel;

namespace GISA
{
    public partial class MasterPanelPermissoesPlanoClassificacao : GISA.MasterPanelNiveis
    {
        private bool actualizaEstrutura = false;
        private GISADataset.NivelDataTable nTable = GisaDataSetHelper.GetInstance().Nivel;
        public MasterPanelPermissoesPlanoClassificacao()
        {
            InitializeComponent();

            nTable.NivelRowChanging += new GISADataset.NivelRowChangeEventHandler(nTable_NivelRowChanging);
            base.StackChanged += MasterPanelSeries_StackChanged;
        }

        public void coluna_Requisitado_Visible(bool visible) {
            this.nivelNavigator1.coluna_Requisitado_Visible(visible);
        }

        void nTable_NivelRowChanging(object sender, GISADataset.NivelRowChangeEvent e)
        {
            if (e.Row.RowState == DataRowState.Added && e.Action == DataRowAction.Commit && e.Row.IDTipoNivel == TipoNivel.LOGICO && e.Row.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().Length == 0)
                actualizaEstrutura = true;
        }

        protected GISADataset.TrusteeRow currentUser = null;

        #region Selection change events
        // selecionar nivel estrutural
        protected override void beforeNewSelection_Action(ControloNivelListEstrutural.BeforeNewSelectionEventArgs e)
        {
            if (e.node == null)
            {
                GISADataset.NivelRow nRow = null;
                UpdateContext(nRow);
            }
            else
                UpdateContext(e.node.NivelRow);
        }

        // selecionar um grupo/utilizador
        private void button1_Click(object sender, EventArgs e)
        {
            FormPickUser frm = new FormPickUser();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                currentUser = frm.tRow;
                txtSelectedUser.Text = currentUser.Name;
                UpdateContext();
            }
        }
        #endregion

        #region UpdateContext
        // Actualiza o contexto de acordo com o nível especificado
        protected override bool UpdateContext(GISADataset.NivelRow row)
        {
            bool result = CurrentContext.SetPermissoes(row, currentUser);
            UpdateToolBarButtons();
            return result;
        }
        #endregion

        private void MasterPanelSeries_StackChanged(frmMain.StackOperation stackOperation, bool isSupport)
        {
            switch (stackOperation)
            {
                case frmMain.StackOperation.Push:
                    this.nivelNavigator1.MultiSelect = false;
                    this.nivelNavigator1.IsParentSupport = isSupport;
                    if (actualizaEstrutura)
                    {
                        resetEstrutura();
                        actualizaEstrutura = false;
                    }

                    if (isSupport)
                    {
                        AddDragHandlers();
                        groupBox1.Visible = false;
                    }
                    //ToolBarButtonFiltro.Pushed = false;
                    //ToolBar_ButtonClick(this, new System.Windows.Forms.ToolBarButtonClickEventArgs(ToolBarButtonFiltro));

                    //this.nivelNavigator1.ClearFiltro();
                    if (this.nivelNavigator1.PanelToggleState == NivelNavigator.ToggleState.Documental && this.nivelNavigator1.ContextBreadCrumbsPathID > 0)
                        this.nivelNavigator1.ReloadList();
                    break;

                case frmMain.StackOperation.Pop:
                    if (isSupport)
                    {
                        RemoveDragHandlers();
                        groupBox1.Visible = true;
                    }
                    //ToolBarButtonFiltro.Pushed = false;
                    //ToolBar_ButtonClick(this, new System.Windows.Forms.ToolBarButtonClickEventArgs(ToolBarButtonFiltro));

                    //this.nivelNavigator1.ClearFiltro();

                    currentUser = null;
                    txtSelectedUser.Text = string.Empty;

                    break;
            }
        }

        protected void AddDragHandlers()
        {
            this.nivelNavigator1.AddDragHandlers();
        }

        protected void RemoveDragHandlers()
        {
            this.nivelNavigator1.RemoveDragHandlers();
        }
	}
}