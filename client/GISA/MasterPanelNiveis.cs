using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

using GISA.Controls;
using GISA.Controls.Localizacao;
using GISA.Controls.Nivel;

namespace GISA
{
	public class MasterPanelNiveis : GISA.MasterPanel
	{
	    #region  Windows Form Designer generated code 

        public MasterPanelNiveis() : base()
		{
			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            ToolBar.ButtonClick += ToolBar_ButtonClick;
			OtherInitializations();
		}

		//Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components = null;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		internal System.Windows.Forms.ToolBarButton ToolBarButtonEdit;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonCreateAny;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonSep2;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonSep3;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonCut;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonPaste;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonSep4;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonPrint;
		internal System.Windows.Forms.ContextMenu ContextMenuPrint;
		internal System.Windows.Forms.MenuItem MenuItem1;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonRemove;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonCreateED;
		internal System.Windows.Forms.MenuItem MenuItemPrintAutoEliminacao;
        internal System.Windows.Forms.MenuItem MenuItemPrintAutoEliminacaoPortaria;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonSep1;
        internal System.Windows.Forms.ToolBarButton ToolBarButtonSep5;
        internal System.Windows.Forms.ToolBarButton ToolBarButtonSep6;
        internal System.Windows.Forms.ToolBarButton ToolBarButtonEAD;
        internal System.Windows.Forms.ToolBarButton ToolBarButtonSep7;
        internal System.Windows.Forms.ToolBarButton ToolBarButtonImportExcel;
        internal System.Windows.Forms.MenuItem MenuItemPrintInventarioResumido;
		internal System.Windows.Forms.MenuItem MenuItemPrintInventarioDetalhado;
		internal System.Windows.Forms.MenuItem MenuItemPrintCatalogoResumido;
		internal System.Windows.Forms.MenuItem MenuItemPrintCatalogoDetalhado;
		internal System.Windows.Forms.MenuItem MenuItemPrintUnidadeFisicaAssociadas;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonToggleEstruturaSeries;
        internal NivelNavigator nivelNavigator1;
        internal System.Windows.Forms.ToolBarButton ToolBarButtonFiltro;
    
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.ToolBarButtonEdit = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonCreateAny = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonSep2 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonSep3 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonCut = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonPaste = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonSep4 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonPrint = new System.Windows.Forms.ToolBarButton();
            this.ContextMenuPrint = new System.Windows.Forms.ContextMenu();
            this.MenuItemPrintInventarioResumido = new System.Windows.Forms.MenuItem();
            this.MenuItemPrintInventarioDetalhado = new System.Windows.Forms.MenuItem();
            this.MenuItemPrintCatalogoResumido = new System.Windows.Forms.MenuItem();
            this.MenuItemPrintCatalogoDetalhado = new System.Windows.Forms.MenuItem();
            this.MenuItemPrintUnidadeFisicaAssociadas = new System.Windows.Forms.MenuItem();
            this.MenuItem1 = new System.Windows.Forms.MenuItem();
            this.MenuItemPrintAutoEliminacao = new System.Windows.Forms.MenuItem();
            this.MenuItemPrintAutoEliminacaoPortaria = new System.Windows.Forms.MenuItem();
            this.ToolBarButtonRemove = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonCreateED = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonSep1 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonSep5 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonSep6 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonEAD = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonSep7 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonImportExcel = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonToggleEstruturaSeries = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonFiltro = new System.Windows.Forms.ToolBarButton();
            this.nivelNavigator1 = new GISA.Controls.Nivel.NivelNavigator();
            this.pnlToolbarPadding.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFuncao
            // 
            this.lblFuncao.Location = new System.Drawing.Point(0, 0);
            this.lblFuncao.Text = "Permissões por Plano de Classificação";
            // 
            // ToolBar
            // 
            this.ToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.ToolBarButtonCreateED,
            this.ToolBarButtonSep1,
            this.ToolBarButtonCreateAny,
            this.ToolBarButtonEdit,
            this.ToolBarButtonRemove,
            this.ToolBarButtonSep2,
            this.ToolBarButtonCut,
            this.ToolBarButtonPaste,
            this.ToolBarButtonSep3,
            this.ToolBarButtonToggleEstruturaSeries,
            this.ToolBarButtonSep4,
            this.ToolBarButtonFiltro,
            this.ToolBarButtonSep5,
            this.ToolBarButtonPrint,
            this.ToolBarButtonSep6,
            this.ToolBarButtonEAD,
            this.ToolBarButtonSep7,
            this.ToolBarButtonImportExcel});
            this.ToolBar.ImageList = null;
            this.ToolBar.Location = new System.Drawing.Point(5, 0);
            this.ToolBar.Size = new System.Drawing.Size(590, 24);
            this.ToolBar.TabIndex = 1;
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
            // 
            // ToolBarButtonEdit
            // 
            this.ToolBarButtonEdit.ImageIndex = 2;
            this.ToolBarButtonEdit.Name = "ToolBarButtonEdit";
            // 
            // ToolBarButtonCreateAny
            // 
            this.ToolBarButtonCreateAny.ImageIndex = 1;
            this.ToolBarButtonCreateAny.Name = "ToolBarButtonCreateAny";
            // 
            // ToolBarButtonSep2
            // 
            this.ToolBarButtonSep2.Name = "ToolBarButtonSep2";
            this.ToolBarButtonSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButtonSep3
            // 
            this.ToolBarButtonSep3.Name = "ToolBarButtonSep3";
            this.ToolBarButtonSep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButtonCut
            // 
            this.ToolBarButtonCut.Enabled = false;
            this.ToolBarButtonCut.ImageIndex = 4;
            this.ToolBarButtonCut.Name = "ToolBarButtonCut";
            // 
            // ToolBarButtonPaste
            // 
            this.ToolBarButtonPaste.Enabled = false;
            this.ToolBarButtonPaste.ImageIndex = 5;
            this.ToolBarButtonPaste.Name = "ToolBarButtonPaste";
            // 
            // ToolBarButtonSep4
            // 
            this.ToolBarButtonSep4.Name = "ToolBarButtonSep4";
            this.ToolBarButtonSep4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButtonPrint
            // 
            this.ToolBarButtonPrint.DropDownMenu = this.ContextMenuPrint;
            this.ToolBarButtonPrint.ImageIndex = 6;
            this.ToolBarButtonPrint.Name = "ToolBarButtonPrint";
            // 
            // ContextMenuPrint
            // 
            this.ContextMenuPrint.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemPrintInventarioResumido,
            this.MenuItemPrintInventarioDetalhado,
            this.MenuItemPrintCatalogoResumido,
            this.MenuItemPrintCatalogoDetalhado,
            this.MenuItemPrintUnidadeFisicaAssociadas,
            this.MenuItem1,
            this.MenuItemPrintAutoEliminacao,
            this.MenuItemPrintAutoEliminacaoPortaria});
            // 
            // MenuItemPrintInventarioResumido
            // 
            this.MenuItemPrintInventarioResumido.Index = 0;
            this.MenuItemPrintInventarioResumido.Text = "&Inventário resumido";
            // 
            // MenuItemPrintInventarioDetalhado
            // 
            this.MenuItemPrintInventarioDetalhado.Index = 1;
            this.MenuItemPrintInventarioDetalhado.Text = "I&nventário detalhado";
            // 
            // MenuItemPrintCatalogoResumido
            // 
            this.MenuItemPrintCatalogoResumido.Index = 2;
            this.MenuItemPrintCatalogoResumido.Text = "&Catálogo resumido";
            // 
            // MenuItemPrintCatalogoDetalhado
            // 
            this.MenuItemPrintCatalogoDetalhado.Index = 3;
            this.MenuItemPrintCatalogoDetalhado.Text = "C&atálogo detalhado";
            // 
            // MenuItemPrintUnidadeFisicaAssociadas
            // 
            this.MenuItemPrintUnidadeFisicaAssociadas.Index = 4;
            this.MenuItemPrintUnidadeFisicaAssociadas.Text = "Unidades Físicas Associadas";
            this.MenuItemPrintUnidadeFisicaAssociadas.Visible = false;
            // 
            // MenuItem1
            // 
            this.MenuItem1.Index = 5;
            this.MenuItem1.Text = "-";
            // 
            // MenuItemPrintAutoEliminacao
            // 
            this.MenuItemPrintAutoEliminacao.Index = 6;
            this.MenuItemPrintAutoEliminacao.Text = "Auto de &eliminação";
            // 
            // MenuItemPrintAutoEliminacaoPortaria
            // 
            this.MenuItemPrintAutoEliminacaoPortaria.Index = 7;
            this.MenuItemPrintAutoEliminacaoPortaria.Text = "Auto de eliminação por &portaria";
            // 
            // ToolBarButtonRemove
            // 
            this.ToolBarButtonRemove.ImageIndex = 3;
            this.ToolBarButtonRemove.Name = "ToolBarButtonRemove";
            // 
            // ToolBarButtonCreateED
            // 
            this.ToolBarButtonCreateED.Name = "ToolBarButtonCreateED";
            // 
            // ToolBarButtonSep1
            // 
            this.ToolBarButtonSep1.Name = "ToolBarButtonSep1";
            this.ToolBarButtonSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButtonSep5
            // 
            this.ToolBarButtonSep5.Name = "ToolBarButtonSep5";
            this.ToolBarButtonSep5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButtonSep6
            // 
            this.ToolBarButtonSep6.Name = "ToolBarButtonSep6";
            this.ToolBarButtonSep6.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButtonEAD
            // 
            this.ToolBarButtonEAD.ImageIndex = 10;
            this.ToolBarButtonEAD.Name = "ToolBarButtonEAD";
            // 
            // ToolBarButtonSep7
            // 
            this.ToolBarButtonSep7.Name = "ToolBarButtonSep7";
            this.ToolBarButtonSep7.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButtonImportExcel
            // 
            this.ToolBarButtonImportExcel.ImageIndex = 11;
            this.ToolBarButtonImportExcel.Name = "ToolBarButtonImportExcel";
            // 
            // ToolBarButtonToggleEstruturaSeries
            // 
            this.ToolBarButtonToggleEstruturaSeries.ImageIndex = 2;
            this.ToolBarButtonToggleEstruturaSeries.Name = "ToolBarButtonToggleEstruturaSeries";
            // 
            // ToolBarButtonFiltro
            // 
            this.ToolBarButtonFiltro.Enabled = false;
            this.ToolBarButtonFiltro.Name = "ToolBarButtonFiltro";
            this.ToolBarButtonFiltro.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // nivelNavigator1
            // 
            this.nivelNavigator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nivelNavigator1.Location = new System.Drawing.Point(0, 52);
            this.nivelNavigator1.Name = "nivelNavigator1";
            this.nivelNavigator1.Size = new System.Drawing.Size(600, 252);
            this.nivelNavigator1.TabIndex = 2;
            // 
            // MasterPanelNiveis
            // 
            this.Controls.Add(this.nivelNavigator1);
            this.Name = "MasterPanelNiveis";
            this.Size = new System.Drawing.Size(600, 304);
            this.Controls.SetChildIndex(this.lblFuncao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.Controls.SetChildIndex(this.nivelNavigator1, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.ResumeLayout(false);

		}
	#endregion

        internal enum SelectionState : int
        {
            NoSelection,
            NoNivelDocSelection,
            DeletedSelection,
            ED,
            GA,
            EstruturalOrganico,
            EstruturalTematicoFuncional,
            Documental
        }

        public delegate void ActualizaEstrutura();
        private ActualizaEstrutura theActualizaEstrutura;
        public ActualizaEstrutura TheActualizaEstrutura
        {
            get { return this.theActualizaEstrutura; }
            set { this.theActualizaEstrutura = value; }
        }

        public override void LoadData()
        {
            this.nivelNavigator1.LoadVistaEstrutural();
        }

		public void a() { this.theActualizaEstrutura(); }

		protected virtual void OtherInitializations()
		{
            this.nivelNavigator1.OtherInitializations();

            AddHandlers();
            
			GetExtraResources();
			HideToolBarButtons();
		}

        protected virtual void AddHandlers()
        {
            this.nivelNavigator1.BeforeNodeSelection += beforeNewSelection_Action;
            this.nivelNavigator1.BeforeListItemSelection += NivelDocumentalListNavigator1_BeforeNewListSelection;
            this.nivelNavigator1.UpdateToolBarButtonsEvent += UpdateToolBarButtons_Action;
            this.nivelNavigator1.ViewToggled += nivelNavigator1_ViewToggled;
        }


        protected virtual void RemoveHandlers()
        {
            this.nivelNavigator1.BeforeNodeSelection -= beforeNewSelection_Action;
            this.nivelNavigator1.BeforeListItemSelection -= NivelDocumentalListNavigator1_BeforeNewListSelection;
            this.nivelNavigator1.UpdateToolBarButtonsEvent -= UpdateToolBarButtons_Action;
            this.nivelNavigator1.ViewToggled -= nivelNavigator1_ViewToggled;
        }

        protected virtual void NivelDocumentalListNavigator1_BeforeNewListSelection(object sender, BeforeNewSelectionEventArgs e)
        {
            var topLevelControl = (frmMain)TopLevelControl;
            topLevelControl.EnterWaitMode();

            var nRow = default(GISADataset.NivelRow);
            var nUpperRow = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().FirstOrDefault(r => r.RowState != DataRowState.Deleted && r.ID == this.nivelNavigator1.ContextBreadCrumbsPathID);

            if (e.ItemToBeSelected != null)
                nRow = e.ItemToBeSelected.Tag as GISADataset.NivelRow;

            if (nUpperRow != null)
                PermissoesHelper.UpdateNivelPermissions(nRow, nUpperRow, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);

            if (topLevelControl != null && topLevelControl.MasterPanelCount == 1)
            {
                try
                {
                    Debug.WriteLine("NivelDocumentalListNavigator1_BeforeNewListSelection");
                    e.SelectionChange = UpdateContext(e.ItemToBeSelected);

                    if (e.SelectionChange)
                    {
                        UpdateToolBarButtons(e.ItemToBeSelected);
                        updateContextStatusBar(e.ItemToBeSelected);
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                    throw;
                }
            }

            ((frmMain)TopLevelControl).LeaveWaitMode();
        }

		private void HideToolBarButtons()
		{
			foreach (ToolBarButton button in this.ToolBar.Buttons)
				button.Visible = false;

			this.ToolBarButtonFiltro.Visible = true;
			this.ToolBarButtonToggleEstruturaSeries.Visible = true;
		}

		private void GetExtraResources()
		{
			ToolBar.ImageList = SharedResourcesOld.CurrentSharedResources.NVLManipulacaoImageList;
			ToolBarButtonToggleEstruturaSeries.ImageIndex = 7; // 6/7
			ToolBarButtonFiltro.ImageIndex = 8;

			string[] strs = SharedResourcesOld.CurrentSharedResources.NVLManipulacaoStrings;
			ToolBarButtonToggleEstruturaSeries.ToolTipText = strs[7]; // 6/7
			ToolBarButtonFiltro.ToolTipText = strs[8];
            ToolBarButtonEAD.ToolTipText = strs[10];
            ToolBarButtonImportExcel.ToolTipText = strs[11];
		}

		public override void Deactivate()
		{
			this.nivelNavigator1.RemoveHandlers();
		}

		protected virtual void beforeNewSelection_Action(ControloNivelList.BeforeNewSelectionEventArgs e)
		{
            this.nivelNavigator1.SelectFirstNode();
		}

		private void UpdateToolBarButtons_Action(EventArgs e) {
			UpdateToolBarButtons();
		}

		public override void UpdateToolBarButtons() {
			UpdateToolBarButtons(null);
		}

		public override void UpdateToolBarButtons(ListViewItem item) {
            if (this.nivelNavigator1.PanelToggleState == NivelNavigator.ToggleState.Estrutural)
            {
                //vista estrutural
                GISADataset.NivelRow nRow = null;
                GISADataset.TipoNivelRelacionadoRow tnrRow = null;
                if (this.nivelNavigator1.EPFilterMode) // modo filtro
                {
                    if (item != null && item.ListView != null && !(((GISADataset.NivelRow)item.Tag).RowState == DataRowState.Detached))
                        //contexto da listview
                        nRow = (GISADataset.NivelRow)item.Tag;
                    if (nRow != null && !(nRow.RowState == DataRowState.Detached))
                        tnrRow = TipoNivelRelacionado.GetTipoNivelRelacionadoFromRelacaoHierarquica(nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().FirstOrDefault());
                }
                this.ToolBarButtonToggleEstruturaSeries.Enabled = this.nivelNavigator1.isTogglable(nRow, tnrRow) && PermissoesHelper.AllowExpand;
            }
            else
                this.ToolBarButtonToggleEstruturaSeries.Enabled = true;
			SetButtonFiltroState();
		}

		// Actualiza o contexto de acordo com o nó actualmente selecionado
		public override bool UpdateContext()
		{
            if (this.nivelNavigator1.PanelToggleState == NivelNavigator.ToggleState.Estrutural)
            {
                if (this.nivelNavigator1.EPFilterMode)
                {
                    ListViewItem item = null;
                    if (this.nivelNavigator1.SelectedItems.Count > 0)
                        item = this.nivelNavigator1.SelectedItems[0];

                    return UpdateContext(item);
                }
                else
                {
                    GISATreeNode node = this.nivelNavigator1.SelectedNode;
                    return UpdateContext(node);
                }
            }
            else
            {
                ListViewItem item = null;
                if (this.nivelNavigator1.SelectedItems.Count > 0)
                    item = this.nivelNavigator1.SelectedItems[0];

                return UpdateContext(item);
            }
		}

        protected virtual bool UpdateContext(ListViewItem item)
		{
			GISADataset.NivelRow nRow = null;
			bool successfulSave = false;

            if ((item != null) && (this.nivelNavigator1.EPFilterMode || this.nivelNavigator1.PanelToggleState == NivelNavigator.ToggleState.Documental))
			{
				nRow = (GISADataset.NivelRow)item.Tag;
				successfulSave = UpdateContext(nRow);
				DelayedRemoveDeletedItems(this.nivelNavigator1.Items);
			}
			else
			{
				nRow = null;
				successfulSave = UpdateContext(nRow);
			}

			return successfulSave;
		}

        protected virtual bool UpdateContext(GISATreeNode node)
        {
            // foi selecionado um nível estrutural
            bool successfulSave = false;
            GISADataset.NivelRow nRow = null;

            if (this.nivelNavigator1.SelectedNode != null)
            {
                node = this.nivelNavigator1.SelectedNode;
                nRow = ((GISATreeNode)node).NivelRow;
                successfulSave = UpdateContext(nRow);
            }
            else
            {
                nRow = null;
                successfulSave = UpdateContext(nRow);
            }

            if (successfulSave)
                updateContextStatusBar(node);

            return successfulSave;
        }

		// Actualiza o contexto de acordo com o nível especificado
        protected virtual bool UpdateContext(GISADataset.NivelRow row)
		{
            bool result = CurrentContext.SetNivelEstrututalDocumental(row);
			UpdateToolBarButtons();
			return result;
		}

        protected virtual void ToggleTreeViews(bool ShowSeries)
        {
            bool cancel = false;
            string newtxt = null;
            if (ShowSeries)
            {
                newtxt = "Estrutura orgânica";
                var nRow = this.nivelNavigator1.EPFilterMode ? this.nivelNavigator1.SelectedNivel : ((GISATreeNode)this.nivelNavigator1.SelectedNode).NivelRow;
                var rhRow = this.nivelNavigator1.EPFilterMode ? nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().FirstOrDefault() : ((GISATreeNode)this.nivelNavigator1.SelectedNode).RelacaoHierarquicaRow;

                this.nivelNavigator1.PanelToggleState = NivelNavigator.ToggleState.Documental;

                // limpar contexto referente ao nó selecionado para prever situações 
                // onde o nível estrutural sobre o qual se faz "toggle" não tem nenhum 
                // nível documental sobre ele (nessa situação a vista muda mas o contexto
                // continua o mesmo)
                cancel = !(UpdateContext());
                if (!cancel && nRow.RowState != DataRowState.Detached)
                {
                    // Obter lista de níveis documentais com o nível estrutural actual
                    this.nivelNavigator1.LoadVistaDocumental(rhRow);
                }
                else if (nRow.RowState == DataRowState.Detached)
                {
                    // Prever a situação de o nó (estrutural) sobre o qual se pretende fazer "toggle"
                    // ter sido apagado por outro utilizador
                    this.nivelNavigator1.PanelToggleState = NivelNavigator.ToggleState.Estrutural;
                    cancel = true;
                    MessageBox.Show("Não é possível mudar para a vista documental uma vez que " + System.Environment.NewLine + "o nível de descrição atual foi apagado por outro utilizador.", "Mudança de vista", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    UpdateContext(nRow);
                    UpdateToolBarButtons();
                }
            }
            else
            {
                newtxt = "Estrutura orgânica";                
                this.nivelNavigator1.ClearFiltro();
                this.nivelNavigator1.PanelToggleState = NivelNavigator.ToggleState.Estrutural;

                cancel = !nivelNavigator1.EPFilterMode ? this.nivelNavigator1.LoadSelectedNode() : !(UpdateContext());
                if (nivelNavigator1.EPFilterMode)
                {
                    var nRow = this.nivelNavigator1.SelectedNivel;
                    if (!cancel && nRow.RowState != DataRowState.Detached)
                        this.nivelNavigator1.ReloadList(nRow);
                    else
                        this.nivelNavigator1.ReloadList();
                }
            }

            if (!cancel)
            {
                this.nivelNavigator1.ToggleView(ShowSeries);

                if (!ShowSeries)
                {
                    newtxt = "Estrutura orgânica";
                    this.nivelNavigator1.PanelToggleState = NivelNavigator.ToggleState.Estrutural;
                    this.ToolBarButtonFiltro.Pushed = this.nivelNavigator1.FilterVisibility;
                }
                else
                {
                    newtxt = "Estrutura documental";

                    if (this.nivelNavigator1.SelectedNivel == null)
                        UpdateToolBarButtons(new ListViewItem());

                    this.ToolBarButtonFiltro.Pushed = this.nivelNavigator1.FilterVisibility;

                    this.nivelNavigator1.PanelToggleState = NivelNavigator.ToggleState.Documental;
                }
                lblFuncao.Text = newtxt;
            }
        }

		// Assinatura para os paineis que usem outros de suporte
		protected virtual void updateContextStatusBar()
		{
            if (this.nivelNavigator1.PanelToggleState == NivelNavigator.ToggleState.Documental && this.nivelNavigator1.SelectedItems.Count > 0)
			{
				ListViewItem item = this.nivelNavigator1.SelectedItems[0];
				updateContextStatusBar(item);
			}
            else if (this.nivelNavigator1.SelectedNode != null)
			{
                TreeNode node = this.nivelNavigator1.SelectedNode;
				updateContextStatusBar((GISATreeNode)node);
			}
		}

		protected virtual void updateContextStatusBar(object obj)
		{
			if (obj is GISATreeNode)
				updateContextStatusBar((GISATreeNode)obj);
			else if (obj is ListViewItem)
				updateContextStatusBar((ListViewItem)obj);
		}

		protected virtual void updateContextStatusBar(GISATreeNode node)
		{
			if (! (MasterPanel.isContextPanel(this)))
				return;
			if (node == null || node.NivelRow.RowState == DataRowState.Detached)
				((frmMain)TopLevelControl).StatusBarPanelHint.Text = string.Empty;
			else
			{
				GISADataset.RelacaoHierarquicaRow rhRow = ((GISATreeNode)node).RelacaoHierarquicaRow;
				GISADataset.TipoNivelRelacionadoRow tnrRow = TipoNivelRelacionado.GetTipoNivelRelacionadoFromRelacaoHierarquica(rhRow);

				ArrayList pathEstrut = ControloNivelList.GetCodigoCompletoCaminhoUnico(node);
				if (pathEstrut.Count == 0)
					((frmMain)TopLevelControl).StatusBarPanelHint.Text = string.Format("  {0}: {1}", tnrRow.Designacao, node.NivelRow.Codigo);
				else
					((frmMain)TopLevelControl).StatusBarPanelHint.Text = string.Format("  {0}: {1}", tnrRow.Designacao, Nivel.buildPath(pathEstrut));
			}
		}

		protected virtual void updateContextStatusBar(ListViewItem listViewItem)
		{
            if (!(MasterPanel.isContextPanel(this)))
				return;

			if (listViewItem == null || (listViewItem != null && listViewItem.ListView == null) || ((GISADataset.NivelRow)listViewItem.Tag).RowState == DataRowState.Detached)
				((frmMain)TopLevelControl).StatusBarPanelHint.Text = string.Empty;
			else
			{
				// prever a situação onde, concorrentemente, outro utilizador fez cut/paste do nível em(questão)
				// mudando assim, o caminho actual do mesmo
				if (GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", ((GISADataset.NivelRow)listViewItem.Tag).ID, this.nivelNavigator1.ContextBreadCrumbsPathID)).Length > 0)
				{
					GISADataset.RelacaoHierarquicaRow rhRow = (GISADataset.RelacaoHierarquicaRow)(GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", ((GISADataset.NivelRow)listViewItem.Tag).ID, this.nivelNavigator1.ContextBreadCrumbsPathID))[0]);

					GISADataset.TipoNivelRelacionadoRow tnrRow = TipoNivelRelacionado.GetTipoNivelRelacionadoFromRelacaoHierarquica(rhRow);

					ArrayList pathEstrut = ControloNivelList.GetCodigoCompletoCaminhoUnico(this.nivelNavigator1.SelectedNode);
					ArrayList pathDoc = this.nivelNavigator1.GetCodigoCompletoCaminhoUnico(listViewItem);
					if (pathDoc.Count == 0 && pathEstrut.Count == 0)
						((frmMain)TopLevelControl).StatusBarPanelHint.Text = string.Format("  {0}", tnrRow.Designacao);
					else
						((frmMain)TopLevelControl).StatusBarPanelHint.Text = string.Format("  {0}: {1}/{2}", tnrRow.Designacao, Nivel.buildPath(pathEstrut), Nivel.buildPath(pathDoc));
				}
				else
					((frmMain)TopLevelControl).StatusBarPanelHint.Text = string.Empty;
			}
		}

		protected virtual void SetButtonFiltroState()
		{
			SetButtonFiltroState(-1);
		}

		protected virtual void SetButtonFiltroState(int newState)
		{
			// é permitido passar como argumento o estado a definir ao botão de filtro uma vez que não se pretende
			// que o utilizador tenha a possibilidade de filtrar a lista de niveis documentais sabendo que o 
			// contexto selecionado na breadcrumbspath já foi apagado por outro utilizador
			ToolBarButtonFiltro.Enabled = newState != 0;
		}

        void nivelNavigator1_ViewToggled(NivelNavigator.ToggleState state)
        {
            string[] strs = SharedResourcesOld.CurrentSharedResources.NVLManipulacaoStrings;
            if (state == NivelNavigator.ToggleState.Estrutural)
            {
                this.ToolBarButtonToggleEstruturaSeries.ImageIndex = 7;
                this.ToolBarButtonToggleEstruturaSeries.ToolTipText = strs[7];
                this.ToolBarButtonToggleEstruturaSeries.Visible = true;
            }
            else
            {
                this.ToolBarButtonToggleEstruturaSeries.ImageIndex = 6;
                this.ToolBarButtonToggleEstruturaSeries.ToolTipText = strs[6];
            }
        }

		protected virtual void ToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			if (e.Button == ToolBarButtonToggleEstruturaSeries)
                ToggleTreeViews(this.nivelNavigator1.PanelToggleState == NivelNavigator.ToggleState.Estrutural);
            else if (e.Button == ToolBarButtonFiltro)
            {
                this.nivelNavigator1.FilterVisibility = ToolBarButtonFiltro.Pushed;
                if (this.nivelNavigator1.PanelToggleState == NivelNavigator.ToggleState.Estrutural)
                {
                    if (this.nivelNavigator1.EPFilterMode)
                    {
                        this.nivelNavigator1.ReloadList();
                        if (this.nivelNavigator1.SelectedItems.Count == 1)
                            UpdateToolBarButtons(this.nivelNavigator1.SelectedItems.First());
                        else
                            UpdateToolBarButtons();
                    }
                    else
                    {
                        // Forçar mudança de selecção na tree de forma a garantir que os paineis de descrição são recarregados para o nível seleccionado na treeview 
                        var node = this.nivelNavigator1.SelectedNode;
                        this.nivelNavigator1.SelectedNode = null;
                        this.nivelNavigator1.SelectedNode = node;
                        UpdateToolBarButtons();
                    }
                    
                }
            }
		}

        protected virtual void resetEstrutura()
        {
            ToggleTreeViews(false);
            this.nivelNavigator1.resetEstrutura();
        }
	}
}