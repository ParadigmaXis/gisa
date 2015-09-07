using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;

using DBAbstractDataLayer.DataAccessRules;
using GISA.GUIHelper;
using GISA.Model;
using GISA.SharedResources;
using GISA.Search;

using GISA.Controls;

using Lucene.Net.QueryParsers;

namespace GISA
{
	public class SlavePanelPesquisa : GISA.SinglePanel
	{

		public static Bitmap FunctionImage
		{
			get
			{
				return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "PesquisaDocumentos_enabled_32x32.png");
			}
		}

	#region  Windows Form Designer generated code 

		public SlavePanelPesquisa() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
			GetExtraResources();
			UpdateToolBarButtons();
            ConfigDataGridUFsAssociadas();

			ImageViewerControl1.openFormImageViewerEvent += OpenFormImageViewer_action;
			ImageViewerControl1.controlerResizeEvent += ControlerResize_action;
            ToolBar.ButtonClick += ToolBar_ButtonClick;
            MenuItemPrintInventarioResumido.Click += MenuItemPrint_Click;
            MenuItemPrintInventarioDetalhado.Click += MenuItemPrint_Click;
            MenuItemPrintCatalogoResumido.Click += MenuItemPrint_Click;
            MenuItemPrintCatalogoDetalhado.Click += MenuItemPrint_Click;
            MenuItemPrintResultadosPesquisaResumidos.Click += MenuItemPrint_Click;
            MenuItemPrintResultadosPesquisaDetalhados.Click += MenuItemPrint_Click;
            base.ParentChanged += SlavePanelPesquisa_ParentChanged;
            //PesquisaList1.BeforeNewListSelection += PesquisaList1_BeforeNewListSelection;
            this.panelInfoEPs1.TheRTFBuilder = RTFBuilder;
            this.panelInfoSDocs1.TheRTFBuilder = RTFBuilder;

            this.PesquisaList1.AddSelectionChangedHandler(new EventHandler(SelectionChanged));

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
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		internal System.Windows.Forms.Panel pnlResultados;
        internal System.Windows.Forms.Panel pnlDetalhesTexto;
		internal System.Windows.Forms.ToolBarButton ToolBarButton3;
		internal System.Windows.Forms.ToolBarButton ToolBarButton4;
		internal System.Windows.Forms.Panel pnlDetalhesImagem;
		internal System.Windows.Forms.ListBox lstImagens;
		internal System.Windows.Forms.ToolBarButton ToolBarButton5;
        internal System.Windows.Forms.Panel pnlUnidadesFisicas;
		internal System.Windows.Forms.GroupBox GroupBox3;
		internal System.Windows.Forms.GroupBox GroupBox4;
        internal System.Windows.Forms.GroupBox grpObjetosDigitais;
		internal System.Windows.Forms.ToolBarButton ToolBarButton2;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonSeparator;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonReports;
        internal System.Windows.Forms.ToolBarButton ToolBarButtonSDocs;
		internal System.Windows.Forms.ContextMenu ContextMenuPrint;
		internal System.Windows.Forms.MenuItem MenuItem1;
		internal System.Windows.Forms.MenuItem MenuItemPrintInventarioResumido;
		internal System.Windows.Forms.MenuItem MenuItemPrintCatalogoResumido;
		internal System.Windows.Forms.MenuItem MenuItemPrintInventarioDetalhado;
		internal System.Windows.Forms.MenuItem MenuItemPrintCatalogoDetalhado;
        internal System.Windows.Forms.MenuItem MenuItemPrintResultadosPesquisaResumidos;
		internal GISA.ImageViewerControl ImageViewerControl1;
		internal System.Windows.Forms.MenuItem MenuItemPrintUnidadesFisicasAssociadas;
		internal System.Windows.Forms.MenuItem MenuItemPrintResultadosPesquisaDetalhados;
        private ContextMenuStrip ContextMenuRichText;
        private ToolStripMenuItem copiarToolStripMenuItem;
        private PanelInfoEPs panelInfoEPs1;
        private PanelInfoSDocs panelInfoSDocs1;
        private ControlFedoraPdfViewer controlFedoraPdfViewer;
        internal RichTextBox rtfDetalhes;
        private SplitContainer splitContainer1;
        private TreeView trvODsFedora;
        private GroupBox grpODsFedora;
        private TableLayoutPanel tableLayoutPanelODs;
        internal Button btnFullScreen;
        internal Button btnActualizar;
        //private DataGridView dataGridView1;
        private PxDataGridView dataGridView1;
        private ToolBarButton ToolBarButton_InfoEPs;
        internal Button btnExportUFs;
        //internal GISA.PesquisaList PesquisaList1;
        internal GISA.PesquisaDataGrid PesquisaList1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlResultados = new System.Windows.Forms.Panel();
            this.PesquisaList1 = new GISA.PesquisaDataGrid();
            this.pnlUnidadesFisicas = new System.Windows.Forms.Panel();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.btnExportUFs = new System.Windows.Forms.Button();
            this.dataGridView1 = new GISA.Controls.PxDataGridView();
            this.pnlDetalhesTexto = new System.Windows.Forms.Panel();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.rtfDetalhes = new System.Windows.Forms.RichTextBox();
            this.ContextMenuRichText = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copiarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolBarButtonSDocs = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.pnlDetalhesImagem = new System.Windows.Forms.Panel();
            this.grpObjetosDigitais = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanelODs = new System.Windows.Forms.TableLayoutPanel();
            this.grpODsFedora = new System.Windows.Forms.GroupBox();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnFullScreen = new System.Windows.Forms.Button();
            this.trvODsFedora = new System.Windows.Forms.TreeView();
            this.lstImagens = new System.Windows.Forms.ListBox();
            this.ImageViewerControl1 = new GISA.ImageViewerControl();
            this.controlFedoraPdfViewer = new GISA.ControlFedoraPdfViewer();
            this.ToolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonSeparator = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonReports = new System.Windows.Forms.ToolBarButton();
            this.ContextMenuPrint = new System.Windows.Forms.ContextMenu();
            this.MenuItemPrintInventarioResumido = new System.Windows.Forms.MenuItem();
            this.MenuItemPrintInventarioDetalhado = new System.Windows.Forms.MenuItem();
            this.MenuItemPrintCatalogoResumido = new System.Windows.Forms.MenuItem();
            this.MenuItemPrintCatalogoDetalhado = new System.Windows.Forms.MenuItem();
            this.MenuItem1 = new System.Windows.Forms.MenuItem();
            this.MenuItemPrintUnidadesFisicasAssociadas = new System.Windows.Forms.MenuItem();
            this.MenuItemPrintResultadosPesquisaResumidos = new System.Windows.Forms.MenuItem();
            this.MenuItemPrintResultadosPesquisaDetalhados = new System.Windows.Forms.MenuItem();
            this.ToolBarButton_InfoEPs = new System.Windows.Forms.ToolBarButton();
            this.panelInfoEPs1 = new GISA.PanelInfoEPs();
            this.panelInfoSDocs1 = new GISA.PanelInfoSDocs();
            this.pnlToolbarPadding.SuspendLayout();
            this.pnlResultados.SuspendLayout();
            this.pnlUnidadesFisicas.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pnlDetalhesTexto.SuspendLayout();
            this.GroupBox4.SuspendLayout();
            this.ContextMenuRichText.SuspendLayout();
            this.pnlDetalhesImagem.SuspendLayout();
            this.grpObjetosDigitais.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanelODs.SuspendLayout();
            this.grpODsFedora.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFuncao
            // 
            this.lblFuncao.Location = new System.Drawing.Point(0, 0);
            this.lblFuncao.Text = "Resultados da pesquisa";
            // 
            // ToolBar
            // 
            this.ToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.ToolBarButton2,
            this.ToolBarButtonSDocs,
            this.ToolBarButton3,
            this.ToolBarButton_InfoEPs,
            this.ToolBarButton4,
            this.ToolBarButton5,
            this.ToolBarButtonSeparator,
            this.ToolBarButtonReports});
            this.ToolBar.ImageList = null;
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
            // 
            // pnlResultados
            // 
            this.pnlResultados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlResultados.Controls.Add(this.PesquisaList1);
            this.pnlResultados.Location = new System.Drawing.Point(0, 52);
            this.pnlResultados.Name = "pnlResultados";
            this.pnlResultados.Size = new System.Drawing.Size(600, 498);
            this.pnlResultados.TabIndex = 2;
            // 
            // PesquisaList1
            // 
            this.PesquisaList1.CustomizedSorting = true;
            this.PesquisaList1.DataSource = null;
            this.PesquisaList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PesquisaList1.FilterVisible = false;
            this.PesquisaList1.IDNivelEstrutura = null;
            this.PesquisaList1.Location = new System.Drawing.Point(0, 0);
            this.PesquisaList1.MultiSelectListView = false;
            this.PesquisaList1.Name = "PesquisaList1";
            this.PesquisaList1.NewSearch = false;
            this.PesquisaList1.NrResults = ((long)(0));
            this.PesquisaList1.Padding = new System.Windows.Forms.Padding(6);
            this.PesquisaList1.SearchServerIDs = null;
            this.PesquisaList1.Size = new System.Drawing.Size(600, 498);
            this.PesquisaList1.SoDocExpirados = false;
            this.PesquisaList1.TabIndex = 1;
            // 
            // pnlUnidadesFisicas
            // 
            this.pnlUnidadesFisicas.Controls.Add(this.GroupBox3);
            this.pnlUnidadesFisicas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUnidadesFisicas.Location = new System.Drawing.Point(0, 52);
            this.pnlUnidadesFisicas.Name = "pnlUnidadesFisicas";
            this.pnlUnidadesFisicas.Size = new System.Drawing.Size(600, 498);
            this.pnlUnidadesFisicas.TabIndex = 1;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox3.Controls.Add(this.btnExportUFs);
            this.GroupBox3.Controls.Add(this.dataGridView1);
            this.GroupBox3.Location = new System.Drawing.Point(8, 8);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(584, 481);
            this.GroupBox3.TabIndex = 2;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Unidades físicas agregadas";
            // 
            // btnExportUFs
            // 
            this.btnExportUFs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportUFs.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExportUFs.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExportUFs.Location = new System.Drawing.Point(554, 41);
            this.btnExportUFs.Name = "btnExportUFs";
            this.btnExportUFs.Size = new System.Drawing.Size(24, 24);
            this.btnExportUFs.TabIndex = 71;
            this.btnExportUFs.Click += new System.EventHandler(this.btnExportUFs_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 19);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(536, 450);
            this.dataGridView1.SmallImageList = null;
            this.dataGridView1.TabIndex = 70;
            // 
            // pnlDetalhesTexto
            // 
            this.pnlDetalhesTexto.Controls.Add(this.GroupBox4);
            this.pnlDetalhesTexto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetalhesTexto.Location = new System.Drawing.Point(0, 52);
            this.pnlDetalhesTexto.Name = "pnlDetalhesTexto";
            this.pnlDetalhesTexto.Size = new System.Drawing.Size(600, 498);
            this.pnlDetalhesTexto.TabIndex = 3;
            // 
            // GroupBox4
            // 
            this.GroupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox4.Controls.Add(this.rtfDetalhes);
            this.GroupBox4.Location = new System.Drawing.Point(8, 8);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(584, 481);
            this.GroupBox4.TabIndex = 1;
            this.GroupBox4.TabStop = false;
            this.GroupBox4.Text = "Descrição";
            // 
            // rtfDetalhes
            // 
            this.rtfDetalhes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtfDetalhes.ContextMenuStrip = this.ContextMenuRichText;
            this.rtfDetalhes.Location = new System.Drawing.Point(8, 16);
            this.rtfDetalhes.Name = "rtfDetalhes";
            this.rtfDetalhes.ReadOnly = true;
            this.rtfDetalhes.Size = new System.Drawing.Size(570, 459);
            this.rtfDetalhes.TabIndex = 3;
            this.rtfDetalhes.Text = "";
            this.rtfDetalhes.Resize += new System.EventHandler(this.rtfDetalhes_Resize);
            // 
            // ContextMenuRichText
            // 
            this.ContextMenuRichText.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copiarToolStripMenuItem});
            this.ContextMenuRichText.Name = "ContextMenuRichText";
            this.ContextMenuRichText.Size = new System.Drawing.Size(110, 26);
            this.ContextMenuRichText.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuRichText_Opening);
            // 
            // copiarToolStripMenuItem
            // 
            this.copiarToolStripMenuItem.Name = "copiarToolStripMenuItem";
            this.copiarToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.copiarToolStripMenuItem.Text = "Copiar";
            this.copiarToolStripMenuItem.Click += new System.EventHandler(this.copiarToolStripMenuItem_Click);
            // 
            // ToolBarButtonSDocs
            // 
            this.ToolBarButtonSDocs.ImageIndex = 6;
            this.ToolBarButtonSDocs.Name = "ToolBarButtonSDocs";
            this.ToolBarButtonSDocs.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // ToolBarButton2
            // 
            this.ToolBarButton2.ImageIndex = 0;
            this.ToolBarButton2.Name = "ToolBarButton2";
            this.ToolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // ToolBarButton3
            // 
            this.ToolBarButton3.ImageIndex = 1;
            this.ToolBarButton3.Name = "ToolBarButton3";
            this.ToolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // ToolBarButton4
            // 
            this.ToolBarButton4.ImageIndex = 2;
            this.ToolBarButton4.Name = "ToolBarButton4";
            this.ToolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // pnlDetalhesImagem
            // 
            this.pnlDetalhesImagem.Controls.Add(this.grpObjetosDigitais);
            this.pnlDetalhesImagem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetalhesImagem.Location = new System.Drawing.Point(0, 52);
            this.pnlDetalhesImagem.Name = "pnlDetalhesImagem";
            this.pnlDetalhesImagem.Size = new System.Drawing.Size(600, 498);
            this.pnlDetalhesImagem.TabIndex = 4;
            // 
            // grpObjetosDigitais
            // 
            this.grpObjetosDigitais.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpObjetosDigitais.Controls.Add(this.splitContainer1);
            this.grpObjetosDigitais.Location = new System.Drawing.Point(8, 8);
            this.grpObjetosDigitais.Name = "grpObjetosDigitais";
            this.grpObjetosDigitais.Size = new System.Drawing.Size(584, 481);
            this.grpObjetosDigitais.TabIndex = 3;
            this.grpObjetosDigitais.TabStop = false;
            this.grpObjetosDigitais.Text = "Objetos digitais";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanelODs);
            this.splitContainer1.Panel1MinSize = 100;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ImageViewerControl1);
            this.splitContainer1.Panel2.Controls.Add(this.controlFedoraPdfViewer);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(578, 462);
            this.splitContainer1.SplitterDistance = 192;
            this.splitContainer1.TabIndex = 11;
            // 
            // tableLayoutPanelODs
            // 
            this.tableLayoutPanelODs.ColumnCount = 1;
            this.tableLayoutPanelODs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelODs.Controls.Add(this.grpODsFedora, 0, 1);
            this.tableLayoutPanelODs.Controls.Add(this.lstImagens, 0, 0);
            this.tableLayoutPanelODs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelODs.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelODs.Name = "tableLayoutPanelODs";
            this.tableLayoutPanelODs.RowCount = 2;
            this.tableLayoutPanelODs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelODs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelODs.Size = new System.Drawing.Size(192, 462);
            this.tableLayoutPanelODs.TabIndex = 6;
            // 
            // grpODsFedora
            // 
            this.grpODsFedora.Controls.Add(this.btnActualizar);
            this.grpODsFedora.Controls.Add(this.btnFullScreen);
            this.grpODsFedora.Controls.Add(this.trvODsFedora);
            this.grpODsFedora.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpODsFedora.Location = new System.Drawing.Point(3, 234);
            this.grpODsFedora.Name = "grpODsFedora";
            this.grpODsFedora.Size = new System.Drawing.Size(186, 225);
            this.grpODsFedora.TabIndex = 5;
            this.grpODsFedora.TabStop = false;
            this.grpODsFedora.Text = "Objetos digitais fedora";
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnActualizar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnActualizar.Location = new System.Drawing.Point(154, 45);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(26, 26);
            this.btnActualizar.TabIndex = 23;
            this.CurrentToolTip.SetToolTip(this.btnActualizar, "Mostrar no ecrã todo");
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnFullScreen
            // 
            this.btnFullScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFullScreen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFullScreen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFullScreen.Location = new System.Drawing.Point(154, 77);
            this.btnFullScreen.Name = "btnFullScreen";
            this.btnFullScreen.Size = new System.Drawing.Size(26, 26);
            this.btnFullScreen.TabIndex = 22;
            this.CurrentToolTip.SetToolTip(this.btnFullScreen, "Mostrar no ecrã todo");
            this.btnFullScreen.Click += new System.EventHandler(this.btnFullScreen_Click);
            // 
            // trvODsFedora
            // 
            this.trvODsFedora.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trvODsFedora.HideSelection = false;
            this.trvODsFedora.Location = new System.Drawing.Point(6, 19);
            this.trvODsFedora.MinimumSize = new System.Drawing.Size(0, 100);
            this.trvODsFedora.Name = "trvODsFedora";
            this.trvODsFedora.Size = new System.Drawing.Size(142, 200);
            this.trvODsFedora.TabIndex = 11;
            this.trvODsFedora.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvODsFedora_BeforeSelect);
            // 
            // lstImagens
            // 
            this.lstImagens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstImagens.Location = new System.Drawing.Point(3, 3);
            this.lstImagens.MinimumSize = new System.Drawing.Size(0, 100);
            this.lstImagens.Name = "lstImagens";
            this.lstImagens.Size = new System.Drawing.Size(186, 225);
            this.lstImagens.TabIndex = 1;
            this.lstImagens.SelectedIndexChanged += new System.EventHandler(this.lstImagens_SelectedIndexChanged);
            // 
            // ImageViewerControl1
            // 
            this.ImageViewerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageViewerControl1.Location = new System.Drawing.Point(0, 0);
            this.ImageViewerControl1.Name = "ImageViewerControl1";
            this.ImageViewerControl1.OtherLocationParams = null;
            this.ImageViewerControl1.Size = new System.Drawing.Size(382, 462);
            this.ImageViewerControl1.SourceLocation = null;
            this.ImageViewerControl1.TabIndex = 4;
            this.ImageViewerControl1.TipoAcessoRecurso = GISA.Model.ResourceAccessType.Smb;
            // 
            // controlFedoraPdfViewer
            // 
            this.controlFedoraPdfViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.controlFedoraPdfViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlFedoraPdfViewer.Location = new System.Drawing.Point(0, 0);
            this.controlFedoraPdfViewer.Name = "controlFedoraPdfViewer";
            this.controlFedoraPdfViewer.Qualidade = "Média";
            this.controlFedoraPdfViewer.Size = new System.Drawing.Size(382, 462);
            this.controlFedoraPdfViewer.TabIndex = 0;
            // 
            // ToolBarButton5
            // 
            this.ToolBarButton5.ImageIndex = 3;
            this.ToolBarButton5.Name = "ToolBarButton5";
            this.ToolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // ToolBarButtonSeparator
            // 
            this.ToolBarButtonSeparator.Name = "ToolBarButtonSeparator";
            this.ToolBarButtonSeparator.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButtonReports
            // 
            this.ToolBarButtonReports.DropDownMenu = this.ContextMenuPrint;
            this.ToolBarButtonReports.ImageIndex = 4;
            this.ToolBarButtonReports.Name = "ToolBarButtonReports";
            // 
            // ContextMenuPrint
            // 
            this.ContextMenuPrint.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemPrintInventarioResumido,
            this.MenuItemPrintInventarioDetalhado,
            this.MenuItemPrintCatalogoResumido,
            this.MenuItemPrintCatalogoDetalhado,
            this.MenuItem1,
            this.MenuItemPrintUnidadesFisicasAssociadas,
            this.MenuItemPrintResultadosPesquisaResumidos,
            this.MenuItemPrintResultadosPesquisaDetalhados});
            // 
            // MenuItemPrintInventarioResumido
            // 
            this.MenuItemPrintInventarioResumido.Index = 0;
            this.MenuItemPrintInventarioResumido.Text = "&Inventário resumido";
            this.MenuItemPrintInventarioResumido.Visible = false;
            // 
            // MenuItemPrintInventarioDetalhado
            // 
            this.MenuItemPrintInventarioDetalhado.Index = 1;
            this.MenuItemPrintInventarioDetalhado.Text = "&Inventário detalhado";
            this.MenuItemPrintInventarioDetalhado.Visible = false;
            // 
            // MenuItemPrintCatalogoResumido
            // 
            this.MenuItemPrintCatalogoResumido.Index = 2;
            this.MenuItemPrintCatalogoResumido.Text = "&Catálogo resumido";
            this.MenuItemPrintCatalogoResumido.Visible = false;
            // 
            // MenuItemPrintCatalogoDetalhado
            // 
            this.MenuItemPrintCatalogoDetalhado.Index = 3;
            this.MenuItemPrintCatalogoDetalhado.Text = "&Catálogo detalhado";
            this.MenuItemPrintCatalogoDetalhado.Visible = false;
            // 
            // MenuItem1
            // 
            this.MenuItem1.Index = 4;
            this.MenuItem1.Text = "-";
            this.MenuItem1.Visible = false;
            // 
            // MenuItemPrintUnidadesFisicasAssociadas
            // 
            this.MenuItemPrintUnidadesFisicasAssociadas.Index = 5;
            this.MenuItemPrintUnidadesFisicasAssociadas.Text = "&Unidades Físicas Associadas";
            this.MenuItemPrintUnidadesFisicasAssociadas.Visible = false;
            // 
            // MenuItemPrintResultadosPesquisaResumidos
            // 
            this.MenuItemPrintResultadosPesquisaResumidos.Index = 6;
            this.MenuItemPrintResultadosPesquisaResumidos.Text = "&Resultados da Pesquisa Resumidos";
            // 
            // MenuItemPrintResultadosPesquisaDetalhados
            // 
            this.MenuItemPrintResultadosPesquisaDetalhados.Index = 7;
            this.MenuItemPrintResultadosPesquisaDetalhados.Text = "Resultados de Pesquisa Detalhados";
            // 
            // ToolBarButton_InfoEPs
            // 
            this.ToolBarButton_InfoEPs.ImageIndex = 5;
            this.ToolBarButton_InfoEPs.Name = "ToolBarButton_InfoEPs";
            this.ToolBarButton_InfoEPs.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // panelInfoEPs1
            // 
            this.panelInfoEPs1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInfoEPs1.Location = new System.Drawing.Point(0, 0);
            this.panelInfoEPs1.Name = "panelInfoEPs1";
            this.panelInfoEPs1.Size = new System.Drawing.Size(600, 550);
            this.panelInfoEPs1.TabIndex = 2;
            this.panelInfoEPs1.TheRTFBuilder = null;
            // 
            // panelInfoSDocs1
            // 
            this.panelInfoSDocs1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInfoSDocs1.Location = new System.Drawing.Point(0, 0);
            this.panelInfoSDocs1.Name = "panelInfoSDocs1";
            this.panelInfoSDocs1.Size = new System.Drawing.Size(600, 550);
            this.panelInfoSDocs1.TabIndex = 2;
            this.panelInfoSDocs1.TheRTFBuilder = null;
            // 
            // SlavePanelPesquisa
            // 
            this.Controls.Add(this.pnlResultados);
            this.Controls.Add(this.pnlUnidadesFisicas);
            this.Controls.Add(this.pnlDetalhesImagem);
            this.Controls.Add(this.pnlDetalhesTexto);
            this.Controls.Add(this.panelInfoSDocs1);
            this.Controls.Add(this.panelInfoEPs1);
            this.Name = "SlavePanelPesquisa";
            this.Size = new System.Drawing.Size(600, 550);
            this.Controls.SetChildIndex(this.panelInfoEPs1, 0);
            this.Controls.SetChildIndex(this.panelInfoSDocs1, 0);
            this.Controls.SetChildIndex(this.lblFuncao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.Controls.SetChildIndex(this.pnlDetalhesTexto, 0);
            this.Controls.SetChildIndex(this.pnlDetalhesImagem, 0);
            this.Controls.SetChildIndex(this.pnlUnidadesFisicas, 0);
            this.Controls.SetChildIndex(this.pnlResultados, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.pnlResultados.ResumeLayout(false);
            this.pnlUnidadesFisicas.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pnlDetalhesTexto.ResumeLayout(false);
            this.GroupBox4.ResumeLayout(false);
            this.ContextMenuRichText.ResumeLayout(false);
            this.pnlDetalhesImagem.ResumeLayout(false);
            this.grpObjetosDigitais.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanelODs.ResumeLayout(false);
            this.grpODsFedora.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

		private void GetExtraResources()
		{
			ToolBarButton2.ToolTipText = "Resultados";
            ToolBarButtonSDocs.ToolTipText = "SubDocumentos";
			ToolBarButton3.ToolTipText = "Detalhes";
			ToolBarButton4.ToolTipText = "Imagens";
			ToolBarButton5.ToolTipText = "Unidades físicas";
			ToolBarButtonReports.ToolTipText = "Relatórios";
            ToolBarButton_InfoEPs.ToolTipText = "Informação da entidade produtora";
            this.btnFullScreen.Image = SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedActionIcons), "ActionFullScreen_enabled_16x16.png");
            this.btnActualizar.Image = SharedResourcesOld.CurrentSharedResources.Actualizar;

            ToolBar.ImageList = SharedResourcesOld.CurrentSharedResources.PesquisaResultadosImageList;
            trvODsFedora.ImageList = SharedResources.SharedResourcesOld.CurrentSharedResources.FedoraImageList;

            this.CurrentToolTip.SetToolTip(this.btnFullScreen, "Mostrar no ecrã todo");
            this.CurrentToolTip.SetToolTip(this.btnActualizar, SharedResourcesOld.CurrentSharedResources.ActualizarString);

            this.btnExportUFs.Image = SharedResourcesOld.CurrentSharedResources.Export;
            this.CurrentToolTip.SetToolTip(this.btnExportUFs, SharedResourcesOld.CurrentSharedResources.ExportString);
		}

		internal FormImageViewer frmImgViewer = null;
		private MasterPanelPesquisa mMasterPanelPesquisa = null;
		private ArrayList EPs;
        private ArrayList Autores;

		private MasterPanelPesquisa MasterPanelPesquisa
		{
			get
			{
				if (mMasterPanelPesquisa == null)
					mMasterPanelPesquisa = (MasterPanelPesquisa)(((frmMain)TopLevelControl).MasterPanel);

				return mMasterPanelPesquisa;
			}
		}

		private Image mImagem = null;
		private Image ImagemEscolhida
		{
			get {return mImagem;}
			set {mImagem = value;}
		}

		public override void LoadData()
		{
			MasterPanelPesquisa.ExecuteQuery += this.ExecuteQuery;
			MasterPanelPesquisa.ShowSelection += this.ShowSelection;
			MasterPanelPesquisa.ClearSearchResults += this.ClearSearchResults;
			ToolBar_ButtonClick(this, new ToolBarButtonClickEventArgs(ToolBarButton2));
            
			this.Visible = true;
		}

		public override void ModelToView()
		{

		}

		public override bool ViewToModel()
		{
			return true;
		}

		public override void Deactivate()
		{
			MasterPanelPesquisa.ExecuteQuery -= this.ExecuteQuery;
            controlFedoraPdfViewer.Clear();
            UFsRelacionadasDataTable.Clear();
            ImageHelper.DeleteFilteredFiles("*.pdf");
		}

        public override PersistencyHelper.SaveResult Save()
		{
			return Save(false);
		}

        public override PersistencyHelper.SaveResult Save(bool activateOpcaoCancelar)
		{
            return PersistencyHelper.SaveResult.nothingToSave;
		}

		private ArrayList itemsPesquisa = new ArrayList();
		private long resultNumber = 0;
		private void ExecuteQuery(MasterPanelPesquisa MasterPanel)
		{
			ToolBar_ButtonClick(this, new ToolBarButtonClickEventArgs(ToolBarButton2));

			try
			{
				((frmMain)TopLevelControl).EnterWaitMode();
				this.lblFuncao.Text = string.Format("Resultados da pesquisa (em curso)");
				this.lblFuncao.Update();

				GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
				try
				{
					resultNumber = 0;
					bool ShowResults = true;

					long calc = DateTime.Now.Ticks;

					try
					{
						long user = SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID;
						resultNumber = ExecuteSearch(ho.Connection);
					}
					catch (Exception ex)
					{
                        MessageBox.Show("Não foi possível completar a pesquisa solicitada. " + Environment.NewLine + "Erro: " + ex.Message, "Determinação dos resultados de pesquisa", MessageBoxButtons.OK, MessageBoxIcon.Warning);						
						ShowResults = false;
						Debug.WriteLine(ex);
                        throw;
					}
					Debug.WriteLine("<<cálculo da pesquisa>>: " + new TimeSpan(DateTime.Now.Ticks - calc).ToString());

					if (ShowResults)
					{
						Trace.WriteLine(string.Format("Found {0} results from search server.", resultNumber));

						calc = DateTime.Now.Ticks;
						PesquisaList1.ReloadList();
                        PesquisaList1.NewSearch = false;
                        resultNumber = PesquisaList1.NrResults;

                        Trace.WriteLine(string.Format("{0} results after filter permissions and expired docs.", resultNumber));

						Debug.WriteLine("<<Popular resultados da pesquisa>>: " + new TimeSpan(DateTime.Now.Ticks - calc).ToString());

						this.lblFuncao.Text = string.Format("Resultados da pesquisa ({0} {1})", resultNumber, ((resultNumber == 1) ? "descrição" : "descrições"));
					}
					else
					{
						resultNumber = 0;
						this.lblFuncao.Text = string.Format("Resultados da pesquisa");
					}
				}
				catch (Exception ex)
				{
					Trace.WriteLine(ex);
					this.lblFuncao.Text = string.Format("Resultados da pesquisa");
				}
				finally
				{
					ho.Dispose();
				}
			}
			catch (Exception Ex)
			{
				Trace.WriteLine(Ex);
				this.lblFuncao.Text = string.Format("Resultados da pesquisa");
			}
			finally
			{
				((frmMain)TopLevelControl).LeaveWaitMode();
			}
		}

		private void PesquisaList1_MyColumnClick(object sender, MyColumnClickEventArgs e)
		{
			if (PesquisaList1.Items.Count > 0)
				PesquisaList1.ReloadList();
		}

        private void SelectionChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (this.PesquisaList1.GetSelectedRows.Count() > 0)
            {
                GISADataset.FRDBaseRow frdRow = null;
                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try
                {
                    //GISADataset.NivelRow nivelRow = ((GISADataset.FRDBaseRow)e.ItemToBeSelected.Tag).NivelRow;
                    //GISADataset.NivelRow nivelRow = ((GISADataset.FRDBaseRow)_r.Cells[PesquisaDataGrid.COL_FRDBASE].Value).NivelRow;
                    frdRow = (GISADataset.FRDBaseRow)(this.PesquisaList1.SelectedRow);


                    bool nvIsDeleted = PesquisaRule.Current.isNivelDeleted(GisaDataSetHelper.GetInstance(), frdRow.NivelRow.ID, ho.Connection);
                    PesquisaRule.Current.LoadRHParentsSelectedResult(GisaDataSetHelper.GetInstance(), frdRow.NivelRow.ID, ho.Connection);
                    if (!nvIsDeleted)
                    {
                        panelInfoEPs1.ClearAll();
                        panelInfoEPs1.BuildTree(frdRow.NivelRow, ho.Connection);
                    }
                    else
                        MessageBox.Show("O elemento selecionado foi apagado por outro utilizador." + System.Environment.NewLine + "Por esse motivo não é possível apresentar qualquer detalhe sobre esse elemento", "Seleção de resultados da pesquisa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                UpdateToolBarButtons(frdRow.NivelRow);
            }
            else
            {
                panelInfoEPs1.ClearAll();
                UpdateToolBarButtons();
            }
            
            this.Cursor = Cursors.Default;

        }

		private void ClearSearchResults(MasterPanelPesquisa MasterPanel)
		{
			resultNumber = 0;
			this.lblFuncao.Text = "Resultados da pesquisa";
			PesquisaList1.ClearSearchResults();
			ToolBar_ButtonClick(this, new ToolBarButtonClickEventArgs(ToolBarButton2));
			rtfDetalhes.Clear();
			lstImagens.Items.Clear();
            trvODsFedora.Nodes.Clear();
            UFsRelacionadasDataTable.Clear();
			UpdateToolBarButtons();
		}

		private void ShowSelection(MasterPanelPesquisa MasterPanel)
		{
			ToolBar_ButtonClick(this, new ToolBarButtonClickEventArgs(ToolBarButton2));
			GISADataset.RelacaoHierarquicaRow rhRow = null;

			var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
				if (MasterPanel.cnList.SelectedRelacaoHierarquica != null)
				{
					rhRow = MasterPanel.cnList.SelectedRelacaoHierarquica;

					if (TipoNivelRelacionado.GetTipoNivelRelacionadoFromRelacaoHierarquica(rhRow).IDTipoNivel != TipoNivel.DOCUMENTAL)
						return;

					PesquisaRule.Current.LoadSelectedData(GisaDataSetHelper.GetInstance(), rhRow.ID, Convert.ToInt64(TipoFRDBase.FRDOIRecolha), ho.Connection);
				}

				// se não existir um nó selecionado para o nivel do nó selecionado ignora-se a chamada a este metodo
				if (rhRow == null)
					return;

				try
				{
					// este teste relevou-se necessário para algumas sequencias de acções faziam o TopLevelControl ser null quando chegavamos a este ponto
					if (TopLevelControl != null)
						((frmMain)TopLevelControl).EnterWaitMode();
					this.lblFuncao.Text = string.Format("selecionado {0}", rhRow.TipoNivelRelacionadoRow.Designacao);
					this.lblFuncao.Update();					

				}
				catch (Exception Ex)
				{
					Trace.WriteLine(Ex);
				}
				finally
				{
					if (TopLevelControl != null)
						((frmMain)TopLevelControl).LeaveWaitMode();
				}
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
		}

		private string[] BreakStrings(string Text)
		{
			Text = Text.Trim();
			if (Text.Length == 0)
			{
				return new string[]{};
			}
			Trace.WriteLine(string.Format("Using input {0}", Text));
			ArrayList Texts = new ArrayList();
			while (Text.Length > 0)
			{
				Regex tempWith1 = new Regex("^(?<Termo>(\"[^\"]+\")|([^ \"]+))");
				Match Result = tempWith1.Match(Text);
				if (! Result.Success)
				{
					throw new ArgumentException("syntax error", "Text");
				}
					Texts.Add(Result.Groups["Termo"].Value);					
				Trace.WriteLine(string.Format("Using '{0}' as search expression.", Texts[Texts.Count - 1]));
				Text = Text.Substring(Result.Length).TrimStart(' ');
			}
			string[] TextsOut = null;
			TextsOut = new string[Texts.Count];
			Texts.CopyTo(TextsOut);
			return TextsOut;
		}

		private long ExecuteSearch(IDbConnection Conn)
		{
            List<string> resultadosDaPesquisa = new List<string>();
            long countResults = 0;
            try
            {
                // TODO: Considerar retirar a dependência com Lucene.Net para fazer a validação dos campos: nem todos são validados....
                NivelDocumentalSearch ndSearch = new NivelDocumentalSearch();
                Lucene.Net.Analysis.Analyzer analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
                QueryParser qp = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, string.Empty, analyzer);
                qp.AllowLeadingWildcard = true;
                StringBuilder errorMessage = new StringBuilder();

                if (Helper.IsValidTxtID(qp, MasterPanelPesquisa.txtID.Text))
                    ndSearch.Id = MasterPanelPesquisa.txtID.Text;
                else
                    errorMessage.AppendLine("Identificador: " + MasterPanelPesquisa.txtID.Text);

                ndSearch.TextoLivre = Helper.AddFieldToSearch(qp, "Texto Livre", MasterPanelPesquisa.txtPesquisaSimples.Text, ref errorMessage);
                
                if (MasterPanelPesquisa.cbModulo.Items.Count == 1)
                    ndSearch.Modulo = 1;
                else
                    ndSearch.Modulo = MasterPanelPesquisa.cbModulo.SelectedIndex;

                ndSearch.CodigoParcial = Helper.AddFieldToSearch(qp, "Código Parcial", MasterPanelPesquisa.txtCodigoParcial.Text, ref errorMessage);
                ndSearch.Designacao = Helper.AddFieldToSearch(qp, "Designação", MasterPanelPesquisa.txtDesignacao.Text, ref errorMessage);
                ndSearch.Autor = Helper.AddFieldToSearch(qp, "Autor", MasterPanelPesquisa.txtAutor.Text, ref errorMessage);
                ndSearch.EntidadeProdutora = Helper.AddFieldToSearch(qp, "Entidade Produtora", MasterPanelPesquisa.txtEntidadeProdutora.Text, ref errorMessage);

                if (MasterPanelPesquisa.lstNiveisDocumentais.SelectedItems.Count != MasterPanelPesquisa.lstNiveisDocumentais.Items.Count)
                {
                    List<string> str = new List<string>();
                    foreach (DataRowView item in MasterPanelPesquisa.lstNiveisDocumentais.SelectedItems)
                        str.Add(item.Row["Designacao"].ToString().ToLower());

                    ndSearch.NiveisDocumentais = str.ToArray();
                    ndSearch.NiveisDocumentaisOP = 0;
                }

                if (MasterPanelPesquisa.cdbDataInicio.Checked)
                    ndSearch.DataProducaoInicio = MasterPanelPesquisa.cdbDataInicio.GetStandardMaskDate.ToString("yyyyMMdd");

                if (MasterPanelPesquisa.cdbDataFim.Checked)
                    ndSearch.DataProducaoFim = MasterPanelPesquisa.cdbDataFim.GetStandardMaskDate.ToString("yyyyMMdd");

                if (MasterPanelPesquisa.cdbInicioDoFim.Checked)
                    ndSearch.DataProducaoInicioDoFim = MasterPanelPesquisa.cdbInicioDoFim.GetStandardMaskDate.ToString("yyyyMMdd");

                if (MasterPanelPesquisa.cdbFimDoFim.Checked)
                    ndSearch.DataProducaoFimDoFim = MasterPanelPesquisa.cdbFimDoFim.GetStandardMaskDate.ToString("yyyyMMdd");

                ndSearch.TipologiaInformacional = Helper.AddFieldToSearch(qp, "Tipologia Informacional", MasterPanelPesquisa.txtTipologiaInformacional.Text, ref errorMessage);
                ndSearch.TermosIndexacao = Helper.AddFieldToSearch(qp, "Indexação", MasterPanelPesquisa.txtIndexacao.Text, ref errorMessage);
                ndSearch.ConteudoInformacional = Helper.AddFieldToSearch(qp, "Conteúdo Informacional", MasterPanelPesquisa.txtConteudoInformacional.Text, ref errorMessage);
                ndSearch.Notas = Helper.AddFieldToSearch(qp, "Notas", MasterPanelPesquisa.txtNotas.Text, ref errorMessage);
                ndSearch.Cota = Helper.AddFieldToSearch(qp, "Cota", Helper.EscapeSpecialCharactersCotaDocumento(MasterPanelPesquisa.txtCota.Text.ToLower()), ref errorMessage);
                ndSearch.Agrupador = Helper.AddFieldToSearch(qp, "Agrupador", MasterPanelPesquisa.txtAgrupador.Text, ref errorMessage);
                ndSearch.SoComODs = string.Empty;
                ndSearch.SoComODsPub = string.Empty;
                ndSearch.SoComODsNaoPub = string.Empty;
                switch (MasterPanelPesquisa.cbODs.SelectedIndex)
                {
                    case 1:
                        ndSearch.SoComODs = Helper.AddFieldToSearch(qp, "objetos", "sim", ref errorMessage);
                        break;
                    case 2:
                        ndSearch.SoComODsPub = Helper.AddFieldToSearch(qp, "objetosPublicados", "sim", ref errorMessage);
                        break;
                    case 3:
                        ndSearch.SoComODsNaoPub = Helper.AddFieldToSearch(qp, "objetosNaoPublicados", "sim", ref errorMessage);
                        break;
                }
                if (MasterPanelPesquisa.chkFormaSuporte.Checked)
                {
                    StringBuilder str = new StringBuilder();
                    foreach (DataRowView item in MasterPanelPesquisa.lstFormaSuporte.SelectedItems)
                    {
                        str.Append(item.Row["Designacao"].ToString());
                        str.Append(" ");
                    }
                    ndSearch.SuporteEAcondicionamento = BreakStrings(str.ToString().ToLower());
                    ndSearch.SuporteEAcondicionamentoOP = MasterPanelPesquisa.cbFormaSuporte.SelectedIndex;
                }

                if (MasterPanelPesquisa.chkMaterialSuporte.Checked)
                {
                    StringBuilder str = new StringBuilder();
                    foreach (DataRowView item in MasterPanelPesquisa.lstMaterialSuporte.SelectedItems)
                    {
                        str.Append(item.Row["Designacao"].ToString());
                        str.Append(" ");
                    }
                    ndSearch.MaterialDeSuporte = BreakStrings(str.ToString().ToLower());
                    ndSearch.MaterialDeSuporteOP = MasterPanelPesquisa.cbMaterialSuporte.SelectedIndex;
                }

                if (MasterPanelPesquisa.chkTecnicaRegisto.Checked)
                {
                    StringBuilder str = new StringBuilder();
                    foreach (DataRowView item in MasterPanelPesquisa.lstTecnicaRegisto.SelectedItems)
                    {
                        str.Append(item.Row["Designacao"].ToString());
                        str.Append(" ");
                    }
                    ndSearch.TecnicaRegisto = BreakStrings(str.ToString().ToLower());
                    ndSearch.TecnicaRegistoOP = MasterPanelPesquisa.cbTecnicaRegisto.SelectedIndex;
                }

                if (MasterPanelPesquisa.chkEstadoConservacao.Checked)
                {
                    StringBuilder str = new StringBuilder();
                    foreach (DataRowView item in MasterPanelPesquisa.lstEstadoConservacao.SelectedItems)
                    {
                        str.Append(item.Row["Designacao"].ToString());
                        str.Append(" ");
                    }
                    ndSearch.EstadoConservacao = BreakStrings(str.ToString().ToLower());
                    ndSearch.EstadoConservacaoOP = 0;
                }

                #region Licencas de obra
                if (MasterPanelPesquisa.get_Nome_LicencaObraRequerentes().Length > 0)
                    ndSearch.Nome_LicencaObraRequerentes = MasterPanelPesquisa.get_Nome_LicencaObraRequerentes();

                if (MasterPanelPesquisa.get_LocalizacaoObra_Actual().Length > 0)
                    ndSearch.LocalizacaoObra_Actual = MasterPanelPesquisa.get_LocalizacaoObra_Actual();
                if (MasterPanelPesquisa.get_NumPolicia_Actual().Length > 0)
                    ndSearch.NumPolicia_Actual = MasterPanelPesquisa.get_NumPolicia_Actual();

                if (MasterPanelPesquisa.get_LocalizacaoObra_Antiga().Length > 0)
                    ndSearch.LocalizacaoObra_Antiga = MasterPanelPesquisa.get_LocalizacaoObra_Antiga();
                if (MasterPanelPesquisa.get_NumPolicia_Antigo().Length > 0)
                    ndSearch.NumPolicia_Antigo = MasterPanelPesquisa.get_NumPolicia_Antigo();

                if (MasterPanelPesquisa.get_TipoObra().Length > 0)
                    ndSearch.LicencaObra_TipoObra = MasterPanelPesquisa.get_TipoObra();

                if (MasterPanelPesquisa.get_TecnicoObra().Length > 0)
                    ndSearch.Termo_LicencaObraTecnicoObra = MasterPanelPesquisa.get_TecnicoObra();

                if (MasterPanelPesquisa.get_CodigosAtestadoHabitabilidade().Length > 0)
                    ndSearch.CodigosAtestadoHabitabilidade = MasterPanelPesquisa.get_CodigosAtestadoHabitabilidade();

                if (MasterPanelPesquisa.get_Datas_LicencaObraDataLicencaConstrucao_Inicio().Length > 0)
                    ndSearch.Datas_LicencaObraDataLicencaConstrucao_Inicio = MasterPanelPesquisa.get_Datas_LicencaObraDataLicencaConstrucao_Inicio();
                if (MasterPanelPesquisa.get_Datas_LicencaObraDataLicencaConstrucao_Fim().Length > 0)
                    ndSearch.Datas_LicencaObraDataLicencaConstrucao_Fim = MasterPanelPesquisa.get_Datas_LicencaObraDataLicencaConstrucao_Fim();

                if (MasterPanelPesquisa.get_PH_checked())
                    ndSearch.LicencaObra_PHSimNao = MasterPanelPesquisa.get_PH_checked();

                #endregion

                if (errorMessage.Length > 0)
                {
                    MessageBox.Show("O(s) campo(s) seguinte(s) tem(êm) valor(es) incorrecto(s): " +
                        System.Environment.NewLine +
                        errorMessage.ToString());

                    return 0;
                }
                
                // impedir efectuar uma pesquisa no servidor de pesquisa quando, na pesquisa avançada, não existe nenhum critério definido excepto um nivel a partir da estrutura
                if (!ndSearch.IsCriteriaEmpty() || !MasterPanelPesquisa.chkEstruturaArquivistica.Checked)
                    resultadosDaPesquisa.AddRange(SearchImpl.search(ndSearch.ToString(), "nivelDocumental", SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID.ToString()));
                else
                {
                    resultadosDaPesquisa = null;
                    countResults = -1;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro na conexão com o servidor de pesquisa", "Gisa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            

            PesquisaList1.SearchServerIDs = resultadosDaPesquisa;
            PesquisaList1.UserID = SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID;
            PesquisaList1.SoDocExpirados = MasterPanelPesquisa.chkApenasDataElimExp.Checked;
            PesquisaList1.NewSearch = true;

            if (MasterPanelPesquisa.chkEstruturaArquivistica.Checked && MasterPanelPesquisa.cnList.SelectedNivelRow != null)
                PesquisaList1.IDNivelEstrutura = MasterPanelPesquisa.cnList.SelectedNivelRow.ID;
            else
            {
                countResults = resultadosDaPesquisa.Count;
                PesquisaList1.IDNivelEstrutura = null;
            }

            PesquisaList1.Focus();

            return countResults;
		}

		private void ActivateDetalhesTexto() {
            if (PesquisaList1.GetSelectedRows.Count() == 1)
            {
				rtfDetalhes.Clear();
				rtfDetalhes.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft Sans Serif;}}\\viewkind4\\uc1\\pard\\f0\\fs24 " +
                    GetFRDBaseAsRTF((GISADataset.FRDBaseRow)PesquisaList1.SelectedRow) + "\\par}";
                pnlDetalhesTexto.BringToFront();
			}
			else
				ToolBar_ButtonClick(this, new ToolBarButtonClickEventArgs(ToolBarButton2));
		}

        private const int CODIGO = 0;
        private const int DESIGNACAO = 1;
        private const int PRODUCAO = 2;
        private const int COTA = 3;
        private const int COTADOCUMETO = 4;
        private DataTable UFsRelacionadasDataTable;
        private void ConfigDataGridUFsAssociadas()
        {
            UFsRelacionadasDataTable = build_UFsRelacionadasDataTable();
            dataGridView1.DataSource = UFsRelacionadasDataTable;

            dataGridView1.Columns[CODIGO].MinimumWidth = 180;
            dataGridView1.Columns[DESIGNACAO].MinimumWidth = 300;
            dataGridView1.Columns[PRODUCAO].MinimumWidth = 140;
            dataGridView1.Columns[COTA].MinimumWidth = 120;
            dataGridView1.Columns[COTADOCUMETO].MinimumWidth = 300;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private string[] _colNames = { "Código", "Designação", "Produção", "Cota", "Cota Documento" };
        private DataTable build_UFsRelacionadasDataTable()
        {
            DataTable _t = new DataTable();
            for (int i = 0; i < this._colNames.Count<string>(); i++)
                _t.Columns.Add(new DataColumn(this._colNames[i], typeof(string)));
            return _t;
        }

        public void ActivateDetalhesUnidadesFisicas()
		{
			if (PesquisaList1.GetSelectedRows.Count() == 1)
			{
                List<UFRule.UFsAssociadas> ufsAssociadas = new List<UFRule.UFsAssociadas>();
                string ID = ((GISADataset.FRDBaseRow)PesquisaList1.SelectedRow).ID.ToString();

				GisaDataSetHelper.ManageDatasetConstraints(false);

				long calc = DateTime.Now.Ticks;
				GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
				try
				{
                    long t1 = DateTime.Now.Ticks;
					ufsAssociadas = PesquisaRule.Current.LoadDetalhesUF(GisaDataSetHelper.GetInstance(), ID, ho.Connection);
                    Debug.WriteLine("<<t1>>: " + new TimeSpan(DateTime.Now.Ticks - t1).ToString());
				}
				catch (Exception ex)
				{
					Trace.WriteLine(ex.ToString());
					throw ex;
				}
				finally
				{
					ho.Dispose();
				}

                GisaDataSetHelper.ManageDatasetConstraints(true);

                long t2 = DateTime.Now.Ticks;
                dataGridView1.DataSource = null;
                dataGridView1.ResetOrder();
                UFsRelacionadasDataTable.BeginLoadData();
                UFsRelacionadasDataTable.Clear();
                foreach (var ufAssociada in ufsAssociadas)
                {
                    var row = UFsRelacionadasDataTable.NewRow();
                    row[CODIGO] = ufAssociada.Codigo;
                    row[DESIGNACAO] = ufAssociada.Designacao;
                    row[PRODUCAO] = GISA.Utils.GUIHelper.FormatDate(ufAssociada.DPInicioAno, ufAssociada.DPInicioMes, ufAssociada.DPInicioDia, ufAssociada.DPInicioAtribuida) + " - " + 
                        GISA.Utils.GUIHelper.FormatDate(ufAssociada.DPFimAno, ufAssociada.DPFimMes, ufAssociada.DPFimDia, ufAssociada.DPFimAtribuida);
                    row[COTA] = ufAssociada.Cota;
                    row[COTADOCUMETO] = ufAssociada.CotaDocumento;
                    UFsRelacionadasDataTable.Rows.Add(row);
                }
                UFsRelacionadasDataTable.EndLoadData();
                UFsRelacionadasDataTable.AcceptChanges();
                this.dataGridView1.DataSource = UFsRelacionadasDataTable;
                Debug.WriteLine("<<t2>>: " + new TimeSpan(DateTime.Now.Ticks - t2).ToString());

                this.dataGridView1.columnClick_refreshData += _ColumnHeaderMouseClick; 

                this.GroupBox3.Text = string.Format("Unidades físicas agregadas ({0})", dataGridView1.Rows.Count);

                pnlUnidadesFisicas.BringToFront();

                this.btnExportUFs.Enabled = dataGridView1.Rows.Count > 0;

				Trace.WriteLine("<<ActivateDetalhesUnidadesFisicas>>: " + new TimeSpan(DateTime.Now.Ticks - calc).ToString());
			}
			else
			{
				ToolBar_ButtonClick(this, new ToolBarButtonClickEventArgs(ToolBarButton2));
			}
		}

        // Header: altera a ordenação
        private void _ColumnHeaderMouseClick(object sender, EventArgs e)
        {
            DataTable ordered = build_UFsRelacionadasDataTable();
            DataRow[] rows = this.UFsRelacionadasDataTable.Select("", this.build_order_by());

            foreach(DataRow row in rows) 
            {
                DataRow _nr = ordered.NewRow();
                _nr[CODIGO] = row.ItemArray.ElementAt(CODIGO);
                _nr[DESIGNACAO] = row.ItemArray.ElementAt(DESIGNACAO);
                _nr[PRODUCAO] = row.ItemArray.ElementAt(PRODUCAO);
                _nr[COTA] = row.ItemArray.ElementAt(COTA);
                _nr[COTADOCUMETO] = row.ItemArray.ElementAt(COTADOCUMETO);

                ordered.Rows.Add(_nr);
            }
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = ordered;

        }

        // Clausula de ordenação:
        private string build_order_by()
        {
            string _o = "";
            ArrayList _oinfo = this.dataGridView1.GetListSortDef();
            for (int i = 0; i < _oinfo.Count; )
            {
                int _ix = (int) _oinfo[i];
                string _asc = (bool)_oinfo[i+1] ? " ASC " : " DESC ";
                _o += " " + this._colNames[_ix] + _asc;
                if (i+2 < _oinfo.Count)
                    _o += ", ";

                i += 2;
            }
            return _o;
        }


        private void btnExportUFs_Click(object sender, EventArgs e)
        {
            SaveFileDialog mSaveDialog = new SaveFileDialog();
            mSaveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            mSaveDialog.AddExtension = true;
            mSaveDialog.DefaultExt = "csv";
            mSaveDialog.Filter = "Comma Separated Values (*.csv)|*.csv";
            mSaveDialog.OverwritePrompt = true;
            mSaveDialog.ValidateNames = true;

            mSaveDialog.FileName = "UnidadesFisicasAssociadas_" + DateTime.Now.ToString("yyyyMMdd");
            switch (mSaveDialog.ShowDialog())
            {
                case DialogResult.OK:
                    mSaveDialog.InitialDirectory = new System.IO.FileInfo(mSaveDialog.FileName).Directory.ToString();
                    break;
                case DialogResult.Cancel:
                    return;
            }

            this.Cursor = Cursors.WaitCursor;
            try
            {
                using (StreamWriter sw = new StreamWriter(mSaveDialog.FileName, false, UTF8Encoding.UTF8))
                {
                    string delimiter = ";";
                    int iColCount = UFsRelacionadasDataTable.Columns.Count;

                    // export column names
                    var columnNames = UFsRelacionadasDataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();
                    sw.Write(string.Join(delimiter, columnNames));
                    sw.Write("\r\n");

                    foreach (DataGridViewRow r in dataGridView1.Rows)
                    {
                        for (int i = 0; i < iColCount - 1; i++)
                        {
                            sw.Write("\"" + r.Cells[i].Value.ToString() + "\"");
                            sw.Write(delimiter);
                        }
                        sw.Write("\r\n");
                    }
                    sw.Flush();
                    sw.Close();
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

		private void ActivateResultados()
		{
            PesquisaList1.Select();
			pnlResultados.BringToFront();
		}

        private void ActivateDetalhesSDocs()
        {
            if (PesquisaList1.GetSelectedRows.Count() == 1)
            {
                List<UFRule.UFsAssociadas> ufsAssociadas = new List<UFRule.UFsAssociadas>();
                //var IDNivel = ((GISADataset.FRDBaseRow)(PesquisaList1.SelectedItems[0].Tag)).IDNivel;
                var IDNivel = ((GISADataset.FRDBaseRow)PesquisaList1.SelectedRow).IDNivel;

                this.panelInfoSDocs1.GetSDocs(IDNivel);
                this.panelInfoSDocs1.BringToFront();
            }
            else
                ToolBar_ButtonClick(this, new ToolBarButtonClickEventArgs(ToolBarButton2));
        }

        private void ActivateDetalhesEP() {
            if (PesquisaList1.GetSelectedRows.Count() == 1)
                this.panelInfoEPs1.BringToFront();
            else
                ToolBar_ButtonClick(this, new ToolBarButtonClickEventArgs(ToolBarButton2));
        }

		private void ToolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
			this.Cursor = Cursors.WaitCursor;
			if (! (e.Button == ToolBarButtonReports)) {
				foreach (ToolBarButton b in ToolBar.Buttons) {
					if (b.Style == ToolBarButtonStyle.ToggleButton)
						b.Pushed = e.Button == b;
				}
			}

			//Antes de permitir a visualização de qualquer informação acerca da Unidade Física seleccionada
			//é necessário garantir que esta ainda não foi apagada por algum utilizador
			bool nvIsDeleted = false;
			if (PesquisaList1.GetSelectedRows.Count() > 0)
			{
				GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
				try
				{
					//nvIsDeleted = PesquisaRule.Current.isNivelDeleted(GisaDataSetHelper.GetInstance(), ((GISADataset.FRDBaseRow)(PesquisaList1.SelectedItems[0].Tag)).IDNivel, ho.Connection);
                    nvIsDeleted = PesquisaRule.Current.isNivelDeleted(GisaDataSetHelper.GetInstance(), ((GISADataset.FRDBaseRow)PesquisaList1.SelectedRow).IDNivel, ho.Connection);
				}
				catch (Exception ex)
				{
					Trace.WriteLine(ex);
					throw ex;
				}
				finally
				{
					ho.Dispose();
				}
			}

			if (! nvIsDeleted)
			{
                // Limpar browser e PDFs temporários
                controlFedoraPdfViewer.Clear();
                ImageHelper.DeleteFilteredFiles("*.pdf");

				if (e.Button == ToolBarButton2)
					ActivateResultados();
				else if (e.Button == ToolBarButton3)
					ActivateDetalhesTexto();
				else if (e.Button == ToolBarButton4)
					ActivateDetalhesImagem();
				else if (e.Button == ToolBarButton5)
					ActivateDetalhesUnidadesFisicas();
				else if (e.Button == ToolBarButtonReports)
				{
					if (e.Button.DropDownMenu != null && e.Button.DropDownMenu is ContextMenu)
						((ContextMenu)e.Button.DropDownMenu).Show(ToolBar, new System.Drawing.Point(e.Button.Rectangle.X, e.Button.Rectangle.Y + e.Button.Rectangle.Height));
				}
                else if (e.Button == ToolBarButton_InfoEPs)
                    ActivateDetalhesEP();
                else if (e.Button == ToolBarButtonSDocs)
                    ActivateDetalhesSDocs();
			}
			else
			{
				MessageBox.Show("O elemento selecionado foi apagado por outro utilizador." + System.Environment.NewLine + "Por esse motivo não é possível apresentar qualquer detalhe sobre esse elemento", "Seleção de resultados da pesquisa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				ToolBar_ButtonClick(this, new ToolBarButtonClickEventArgs(ToolBarButton2));
			}

			this.Cursor = Cursors.Arrow;
		}

		private void MenuItemPrint_Click(object sender, System.EventArgs e)
		{
            //TODO: verificar se esta variável é precisa uma vez que nunca é preenchida neste método..
            ArrayList searchResults = null;
			try
			{
                if (PesquisaList1.Items.Count == 0)
					MessageBox.Show("Não foram encontrados resultados na última pesquisa " + Environment.NewLine + 
                        "a partir dos quais possa ser construído um relatório.", "Relatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				else
				{
					if (sender == MenuItemPrintInventarioResumido)
					{
						Reports.InventarioResumido report = new Reports.InventarioResumido(string.Format("InventarioResumido_{0}", DateTime.Now.ToString("yyyyMMdd")), searchResults, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
						object o = new Reports.BackgroundRunner(TopLevelControl, report, resultNumber);
					}
					else if (sender == MenuItemPrintInventarioDetalhado)
					{

					}
					else if (sender == MenuItemPrintCatalogoResumido)
					{
						Reports.CatalogoResumido report = new Reports.CatalogoResumido(string.Format("CatalogoResumido_{0}", DateTime.Now.ToString("yyyyMMdd")), searchResults, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
						object o = new Reports.BackgroundRunner(TopLevelControl, report, resultNumber);
					}
					else if (sender == MenuItemPrintCatalogoDetalhado)
					{
						FormCustomizableReports frm = new FormCustomizableReports();
                        frm.AddParameters(DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.BuildParamListInventCat(SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsLicObrEnable()));
						switch (frm.ShowDialog())
						{
							case DialogResult.OK:
								Reports.CatalogoDetalhado report = new Reports.CatalogoDetalhado(string.Format("CatalogoDetalhado_{0}", DateTime.Now.ToString("yyyyMMdd")), searchResults, frm.GetSelectedParameters(), false, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
								object o = new Reports.BackgroundRunner(TopLevelControl, report, resultNumber);
								break;
							case DialogResult.Cancel:
							break;
						}
					}
					else if (sender == MenuItemPrintResultadosPesquisaResumidos)
					{
                        Dictionary<string, string> criteriosDePesquisa = GetCriteriosDePesquisa();
						Reports.ResultadosPesquisa report = new Reports.ResultadosPesquisa(
                            string.Format("ResultadosPesquisaResumido_{0}", 
                            DateTime.Now.ToString("yyyyMMdd")), 
                            searchResults, 
                            SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID,
                            SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsLicObrEnable(),
                            SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsReqEnable());
                        report.CriteriosDePesquisa = criteriosDePesquisa;
                        object o = new Reports.BackgroundRunner(TopLevelControl, report, resultNumber);

					}
					else if (sender == MenuItemPrintResultadosPesquisaDetalhados)
					{
                        Dictionary<string, string> criteriosDePesquisa = GetCriteriosDePesquisa();
						FormCustomizableReports frm = new FormCustomizableReports();
                        frm.AddParameters(DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.BuildParamListInventCat(SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsLicObrEnable()));
						switch (frm.ShowDialog())
						{
							case DialogResult.OK:
								Reports.ResultadosPesquisaDetalhados report = new Reports.ResultadosPesquisaDetalhados(string.Format("ResultadosPesquisaDetalhado_{0}", DateTime.Now.ToString("yyyyMMdd")), searchResults, frm.GetSelectedParameters(), false, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
                                report.CriteriosDePesquisa = criteriosDePesquisa;
								object o = new Reports.BackgroundRunner(TopLevelControl, report, resultNumber);
								break;
							case DialogResult.Cancel:
							break;
						}
					}
				}
			}
			catch (Reports.OperationAbortedException)
			{
				// Cancelado pelo utilizador
			}
		}

        private Dictionary<string, string> GetCriteriosDePesquisa()
        {
            //TODO: passar as string para resources
            //Fazer para todos os campos
            Dictionary<string, string> criteriosDePesquisa = new Dictionary<string, string>();
            if (MasterPanelPesquisa.TipoPesquisaSeleccionada == MasterPanelPesquisa.TipoPesquisa.simples)
            {
                criteriosDePesquisa.Add("Pesquisa simples", MasterPanelPesquisa.txtPesquisaSimples.Text);
            }
            else
            {
                criteriosDePesquisa.Add("Módulo", MasterPanelPesquisa.cbModulo.SelectedItem.ToString());

                if (!MasterPanelPesquisa.txtCodigoParcial.Text.Equals(string.Empty))
                {
                    criteriosDePesquisa.Add("Código parcial", MasterPanelPesquisa.txtCodigoParcial.Text);
                }
                if (!MasterPanelPesquisa.txtID.Text.Equals(string.Empty))
                {
                    criteriosDePesquisa.Add("Identificador", MasterPanelPesquisa.txtID.Text);
                }
                if (!MasterPanelPesquisa.txtDesignacao.Text.Equals(string.Empty))
                {
                    criteriosDePesquisa.Add("Designação", MasterPanelPesquisa.txtDesignacao.Text);
                }
                if (!MasterPanelPesquisa.txtAutor.Text.Equals(string.Empty))
                {
                    criteriosDePesquisa.Add("Autor", MasterPanelPesquisa.txtAutor.Text);
                }
                if (!MasterPanelPesquisa.txtEntidadeProdutora.Text.Equals(string.Empty))
                {
                    criteriosDePesquisa.Add("Entidade produtora", MasterPanelPesquisa.txtEntidadeProdutora.Text);
                }
                if (MasterPanelPesquisa.lstNiveisDocumentais.SelectedIndices.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach(DataRowView item in MasterPanelPesquisa.lstNiveisDocumentais.SelectedItems)
                    {
                        sb.Append(MasterPanelPesquisa.lstNiveisDocumentais.GetItemText(item));
                        sb.Append(" Ou ");
                    }
                    string final = sb.ToString().TrimEnd();
                    criteriosDePesquisa.Add("Nível de descrição", final.Remove(final.Length-3));
                }

                StringBuilder datasProducao = new StringBuilder();
                if (MasterPanelPesquisa.cdbDataInicio.Checked)
                {                    
                    datasProducao.Append("Inicio do intervalo inicial: ");
                    datasProducao.Append(MasterPanelPesquisa.cdbDataInicio.Year);
                    datasProducao.Append("/");
                    datasProducao.Append(MasterPanelPesquisa.cdbDataInicio.Month);
                    datasProducao.Append("/");
                    datasProducao.Append(MasterPanelPesquisa.cdbDataInicio.Day);                    
                }
                if (MasterPanelPesquisa.cdbDataFim.Checked)
                {
                    if (datasProducao.Length > 0)
                    {
                        datasProducao.Append(" ");
                    }

                    datasProducao.Append("Fim do intervalo inicial: ");
                    datasProducao.Append(MasterPanelPesquisa.cdbDataFim.Year);
                    datasProducao.Append("/");
                    datasProducao.Append(MasterPanelPesquisa.cdbDataFim.Month);
                    datasProducao.Append("/");
                    datasProducao.Append(MasterPanelPesquisa.cdbDataFim.Day);
                }
                if (MasterPanelPesquisa.cdbInicioDoFim.Checked)
                {
                    if (datasProducao.Length > 0)
                    {
                        datasProducao.Append(" ");
                    }

                    datasProducao.Append("Inicio do intervalo final: ");
                    datasProducao.Append(MasterPanelPesquisa.cdbInicioDoFim.Year);
                    datasProducao.Append("/");
                    datasProducao.Append(MasterPanelPesquisa.cdbInicioDoFim.Month);
                    datasProducao.Append("/");
                    datasProducao.Append(MasterPanelPesquisa.cdbInicioDoFim.Day);
                }
                if (MasterPanelPesquisa.cdbFimDoFim.Checked)
                {
                    if (datasProducao.Length > 0)
                    {
                        datasProducao.Append(" ");
                    }

                    datasProducao.Append("Fim do intervalo final: ");
                    datasProducao.Append(MasterPanelPesquisa.cdbFimDoFim.Year);
                    datasProducao.Append("/");
                    datasProducao.Append(MasterPanelPesquisa.cdbFimDoFim.Month);
                    datasProducao.Append("/");
                    datasProducao.Append(MasterPanelPesquisa.cdbFimDoFim.Day);
                }
                if(datasProducao.Length > 0)
                {
                    criteriosDePesquisa.Add("Datas de produção", datasProducao.ToString());
                }
                if (!MasterPanelPesquisa.txtTipologiaInformacional.Text.Equals(string.Empty))
                {
                    criteriosDePesquisa.Add("Tipologia informacional", MasterPanelPesquisa.txtTipologiaInformacional.Text);
                }
                if (!MasterPanelPesquisa.txtIndexacao.Text.Equals(string.Empty))
                {
                    criteriosDePesquisa.Add("Termos de indexação", MasterPanelPesquisa.txtIndexacao.Text);
                }
                if (!MasterPanelPesquisa.txtConteudoInformacional.Text.Equals(string.Empty))
                {
                    criteriosDePesquisa.Add("Conteúdo informacional", MasterPanelPesquisa.txtConteudoInformacional.Text);
                }
                if (!MasterPanelPesquisa.txtNotas.Text.Equals(string.Empty))
                {
                    criteriosDePesquisa.Add("Notas", MasterPanelPesquisa.txtNotas.Text);
                }
                if (!MasterPanelPesquisa.txtCota.Text.Equals(string.Empty))
                {
                    criteriosDePesquisa.Add("Cota", MasterPanelPesquisa.txtCota.Text);
                }
                if (!MasterPanelPesquisa.txtAgrupador.Text.Equals(string.Empty))
                {
                    criteriosDePesquisa.Add("Agrupador", MasterPanelPesquisa.txtCota.Text);
                }
                if (MasterPanelPesquisa.chkApenasDataElimExp.Checked)
                {
                    criteriosDePesquisa.Add("Prazo", MasterPanelPesquisa.chkApenasDataElimExp.Text);
                }
                switch (MasterPanelPesquisa.cbODs.SelectedIndex)
                {
                    case 1:
                        criteriosDePesquisa.Add("objetos", MasterPanelPesquisa.cbODs.Items[MasterPanelPesquisa.cbODs.SelectedIndex].ToString());
                        break;
                    case 2:
                        criteriosDePesquisa.Add("objetosPublicados", MasterPanelPesquisa.cbODs.Items[MasterPanelPesquisa.cbODs.SelectedIndex].ToString());
                        break;
                    case 3:
                        criteriosDePesquisa.Add("objetosNaoPublicados", MasterPanelPesquisa.cbODs.Items[MasterPanelPesquisa.cbODs.SelectedIndex].ToString());
                        break;
                }
                if (MasterPanelPesquisa.chkFormaSuporte.Checked)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (DataRowView item in MasterPanelPesquisa.lstFormaSuporte.SelectedItems)
                    {
                        sb.Append(MasterPanelPesquisa.lstFormaSuporte.GetItemText(item));
                        sb.Append(" ");
                        sb.Append(MasterPanelPesquisa.cbFormaSuporte.SelectedItem.ToString());
                        sb.Append(" ");
                    }
                    string final = sb.ToString().TrimEnd();
                    criteriosDePesquisa.Add("Suporte e acondicionamento", final.Remove(final.Length - 3));
                }
                if (MasterPanelPesquisa.chkMaterialSuporte.Checked)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (DataRowView item in MasterPanelPesquisa.lstMaterialSuporte.SelectedItems)
                    {
                        sb.Append(MasterPanelPesquisa.lstMaterialSuporte.GetItemText(item));
                        sb.Append(" ");
                        sb.Append(MasterPanelPesquisa.cbMaterialSuporte.SelectedItem.ToString());
                        sb.Append(" ");
                    }
                    string final = sb.ToString().TrimEnd();
                    criteriosDePesquisa.Add("Material de suporte", final.Remove(final.Length - 3));
                }
                if (MasterPanelPesquisa.chkTecnicaRegisto.Checked)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (DataRowView item in MasterPanelPesquisa.lstTecnicaRegisto.SelectedItems)
                    {
                        sb.Append(MasterPanelPesquisa.lstTecnicaRegisto.GetItemText(item));
                        sb.Append(" ");
                        sb.Append(MasterPanelPesquisa.cbTecnicaRegisto.SelectedItem.ToString());
                        sb.Append(" ");
                    }
                    string final = sb.ToString().TrimEnd();
                    criteriosDePesquisa.Add("Técnica de registo", final.Remove(final.Length - 3));
                }
                if (MasterPanelPesquisa.chkEstadoConservacao.Checked)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (DataRowView item in MasterPanelPesquisa.lstEstadoConservacao.SelectedItems)
                    {
                        sb.Append(MasterPanelPesquisa.lstEstadoConservacao.GetItemText(item));
                        sb.Append(" Ou ");                        
                    }
                    string final = sb.ToString().TrimEnd();
                    criteriosDePesquisa.Add("Estado de conservação", final.Remove(final.Length - 3));
                }
                if (MasterPanelPesquisa.chkEstruturaArquivistica.Checked)
                    criteriosDePesquisa.Add("Pesquisa por estrutura", MasterPanelPesquisa.cnList.SelectedNivelDesignacao);
                
            }
            return criteriosDePesquisa;
        }

        private void GetAutores(GISADataset.NivelRow NivelRow)
        {
            Autores = new ArrayList();

            var ncaRows = NivelRow.GetFRDBaseRows().Single().GetSFRDAutorRows().SelectMany(aut => aut.ControloAutRow.GetNivelControloAutRows());
            Autores.AddRange(ncaRows.Select(nca => nca.NivelRow).ToArray());
        }

		private void GetEntidadesProdutoras(GISADataset.NivelRow NivelRow)
		{
			EPs = new ArrayList();

			foreach (GISADataset.RelacaoHierarquicaRow rh in NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica())
			{
				if (rh.NivelRowByNivelRelacaoHierarquicaUpper.GetNivelControloAutRows().Length > 0)
				{
					foreach (GISADataset.NivelControloAutRow n in rh.NivelRowByNivelRelacaoHierarquicaUpper.GetNivelControloAutRows())
					{
						if (n.ControloAutRow.TipoNoticiaAutRow.ID == Convert.ToInt64(TipoNoticiaAut.EntidadeProdutora))
							EPs.Add(n.NivelRow);
					}
				}
				else
					GetEntidadesProdutoras(rh.NivelRowByNivelRelacaoHierarquicaUpper);
			}
		}

		private string printCAs(ArrayList lst)
		{
			string rez = "";
			foreach (GISADataset.NivelRow n in lst)
			{
				foreach (GISADataset.NivelControloAutRow nca in n.GetNivelControloAutRows())
				{
					foreach (GISADataset.ControloAutDicionarioRow cad in nca.ControloAutRow.GetControloAutDicionarioRows())
					{
						if (cad.IDTipoControloAutForma == Convert.ToInt64(TipoControloAutForma.FormaAutorizada))
							rez = rez + cad.DicionarioRow.Termo + "\\par{}";
					}
				}
			}
			return rez;
        }

        #region Processo de Obras
        private bool IsRelatedToProcessoObras(GISADataset.FRDBaseRow FRDBaseRow, IDbConnection conn) {
            return FRDRule.Current.possuiDadosLicencaDeObras(GisaDataSetHelper.GetInstance(), FRDBaseRow.ID, conn);
        }

        private string gen_content_PROCESSO_OBRAS(long IDNivel, IDbConnection conn) {
            StringBuilder Result = new StringBuilder();
            EADGeneratorRule.ScopeContent_PROCESSO_DE_OBRAS scpCon_PROC_OBRAS = EADGeneratorRule.Current.get_scopecontent_PROCESSO_DE_OBRAS(IDNivel, conn);

            // "\\i{}\\par{}BlaBla: \\i0{}\\par{}"

            if (scpCon_PROC_OBRAS.requerentes.Count > 0)
                Result.Append("\\i{}Requerentes/proprietários (iniciais): \\i0{}\\par{}").Append(GetConditionalText("  ", gen_list(scpCon_PROC_OBRAS.requerentes), ""));

            if (scpCon_PROC_OBRAS.averbamentos.Count > 0)
                Result.Append("\\i{}\\par{}Requerentes/proprietários (averbamento): \\i0{}\\par{}").Append(GetConditionalText("  ", gen_list(scpCon_PROC_OBRAS.averbamentos), ""));

            // Localizacao com forma autorizada e outras formas:
            if (scpCon_PROC_OBRAS.loc_actual.Count > 0)
                Result.Append("\\i{}\\par{}Localização da obra (designação atual): \\i0{}\\par{}")
                    .Append(GetConditionalText("  ", gen_list(scpCon_PROC_OBRAS.loc_actual_OutrasFormas), ""));

            if (scpCon_PROC_OBRAS.loc_antiga.Count > 0)
                Result.Append("\\i{}\\par{}Localização da obra (designação antiga): \\i0{}\\par{}")
                    .Append(GetConditionalText("  ", gen_list(scpCon_PROC_OBRAS.loc_antiga), ""));

            if (!scpCon_PROC_OBRAS.tipo_obra.Equals(string.Empty))
                Result.Append("\\i{}\\par{}Tipo de obra: \\i0{}\\par{}")
                    .Append(GetConditionalText("  ", scpCon_PROC_OBRAS.tipo_obra, ""));

            if (!scpCon_PROC_OBRAS.strPH.Equals(string.Empty))
                Result.Append("\\i{}\\par{}Propriedade horizontal: \\i0{}\\par{}")
                    .Append(GetConditionalText("  ", scpCon_PROC_OBRAS.strPH, ""));

            // Tecnico com forma autorizada e outras:
            if (scpCon_PROC_OBRAS.tecnicoObra.Count > 0)
                Result.Append("\\i{}\\par{}Técnico de obra: \\i0{}\\par{}")
                    .Append(GetConditionalText("  ", gen_list(scpCon_PROC_OBRAS.tecnicoObra_OutrasFormas), ""));

            if (scpCon_PROC_OBRAS.atestado.Count > 0)
                Result.Append("\\i{}\\par{}Atestado de habitabilidade: \\i0{}\\par{}")
                    .Append(GetConditionalText("  ", gen_list(scpCon_PROC_OBRAS.atestado), ""));

            if (scpCon_PROC_OBRAS.datasLicenca.Count > 0)
                Result.Append("\\i{}\\par{}Data da licença de construção: \\i0{}\\par{}")
                    .Append(GetConditionalText("  ", gen_list(scpCon_PROC_OBRAS.datasLicenca), ""));

            Result.Append("\\par{}");
            return Result.ToString();
        }

        private string gen_list(List<string> list) {
            StringBuilder Result = new StringBuilder();
            int i = 0;
            foreach (string str in list)
                Result.Append(i++ == 0 ? str : "; " + str);

            return Result.ToString();
        }

        /*
         * Gera uma string com o termo autorizado e os outros entre ( e ) 
         * seguido dos outros termos autorizados da lista
         */
        private string gen_list(List<EADGeneratorRule.Termo_Outras_Formas> list_Termo_Outras_Formas) {
            StringBuilder Result = new StringBuilder();
            int i = 0;
            foreach (EADGeneratorRule.Termo_Outras_Formas termo_Outras_Formas in list_Termo_Outras_Formas) {
                Result.Append(i++ == 0 ? termo_Outras_Formas.Termo : "; " + termo_Outras_Formas.Termo);
                if (!termo_Outras_Formas.Outras_Formas.Equals(string.Empty))
                    Result.Append(" \\i{}Usado por: \\i0{}" + termo_Outras_Formas.Outras_Formas);
            }

            return Result.ToString();
        }

        #endregion

        private string GetTermosIndexados(GISADataset.FRDBaseRow FRDBaseRow, params TipoNoticiaAut[] TipoNoticiaAut) {
			StringBuilder Result = new StringBuilder();
			ArrayList ResultArray = new ArrayList();
			foreach (GISADataset.IndexFRDCARow index in FRDBaseRow.GetIndexFRDCARows()) {
				if (Array.IndexOf(TipoNoticiaAut, System.Enum.ToObject(typeof(Model.TipoNoticiaAut), index.ControloAutRow.IDTipoNoticiaAut)) >= TipoNoticiaAut.GetLowerBound(0)) {
					foreach (GISADataset.ControloAutDicionarioRow cadr in index.ControloAutRow.GetControloAutDicionarioRows()) {
						if (cadr.IDTipoControloAutForma == Convert.ToInt64(TipoControloAutForma.FormaAutorizada)) {
							ResultArray.Add(cadr.DicionarioRow.Termo);
						}
					}
				}
			}
			ResultArray.Sort();
			foreach (string s in ResultArray)
			{
				if (Result.Length > 0)
				{
					Result.Append("\\li128\\par\\li0{}");
				}
				Result.Append(s);
			}
			if (Result.Length > 0)
			{
				Result.Append("\\li128\\par\\li0{}");
			}
			return Result.ToString();
		}


        private string GetTermosIndexados_OutrasFormas(GISADataset.FRDBaseRow FRDBaseRow) {
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            StringBuilder Result = new StringBuilder();
            List<PesquisaRule.TermosIndexacao> termosIdx = null;
            try
            {                
                termosIdx = PesquisaRule.Current.GetTermosIndexacao(FRDBaseRow.IDNivel, ho.Connection);
            }
            finally
            {
                ho.Dispose();
            }

            foreach (PesquisaRule.TermosIndexacao termo in termosIdx)
            {
                Result.Append(termo.Termo + "\\li128\\par\\li0{}");
                if (!termo.Outras_Formas.Equals(string.Empty))
                    Result.Append("     \\i{}" + termo.Outras_Formas + "\\li128\\par\\li0{}\\i0{}");
            }

            return Result.ToString();
        }


		private string GetConditionalText(string Prefix, string Text, string Suffix)
		{
            if (Text == null ||Text.Length == 0)
                return "";
			else
                return Prefix + Text.Replace(System.Environment.NewLine, "\\par{}") + Suffix;
		}

        private string RTFBuilder(long IDNivel) {
            GisaDataSetHelper.ManageDatasetConstraints(false);
 			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                string filter = string.Format("IDNivel = {0} ", IDNivel);                
                PesquisaRule.Current.LoadSelectedData(GisaDataSetHelper.GetInstance(), IDNivel, Convert.ToInt64(TipoFRDBase.FRDOIRecolha), ho.Connection);
                DataRow[] frdbaseRows = frdbaseRows = GisaDataSetHelper.GetInstance().FRDBase.Select(filter);

                if (frdbaseRows.Length > 0)
                    return this.GetFRDBaseAsRTF((GISADataset.FRDBaseRow)(frdbaseRows[0]));

                return "";
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
            finally
            {
                ho.Dispose();
                GisaDataSetHelper.ManageDatasetConstraints(true);
            }

        }

		private string GetFRDBaseAsRTF(GISADataset.FRDBaseRow FRDBaseRow) {
			GisaDataSetHelper.ManageDatasetConstraints(false);

			StringBuilder Result = new StringBuilder();
            var cotas = new List<string>();
			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
				string IDFRDbase = FRDBaseRow.ID.ToString();
				PesquisaRule.Current.LoadFRDBaseData(GisaDataSetHelper.GetInstance(), IDFRDbase, ho.Connection);

                if (FRDBaseRow.NivelRow.IDTipoNivel == TipoNivel.ESTRUTURAL)
                    DBAbstractDataLayer.DataAccessRules.ControloAutRule.Current.LoadControloAutFromNivel(GisaDataSetHelper.GetInstance(), FRDBaseRow.NivelRow.ID, ho.Connection);

                // Obter info sobre cota se for documento ou subdocumento
                var idTipoNivelRelacionado = FRDBaseRow.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First().IDTipoNivelRelacionado;
                if (idTipoNivelRelacionado == TipoNivelRelacionado.D || idTipoNivelRelacionado == TipoNivelRelacionado.SD)
                    cotas = PesquisaRule.Current.LoadDocumentoCotas(IDFRDbase, ho.Connection);

				// --Identificação--
				Result.Append("\\fs36\\b{}Identificação\\b0{}\\fs24\\sb196\\sa48\\par{}\\sb0\\sa0{}");
				// Codigo de Referência
				Result.Append(GetConditionalText("\\i{}Código de referencia: \\i0{}", 
                    DBAbstractDataLayer.DataAccessRules.NivelRule.Current.GetCodigoOfNivel(FRDBaseRow.NivelRow.ID, ho.Connection)[0].ToString(), "\\par{}"));
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
			}
			finally
			{
				ho.Dispose();
                GisaDataSetHelper.ManageDatasetConstraints(true);
			}

			// Nivel de descrição (TipoNivel)
			Result.Append(GetConditionalText("\\i{}Nível de descrição: \\i0{}", TipoNivelRelacionado.GetTipoNivelRelacionadoDaPrimeiraRelacaoEncontrada(FRDBaseRow.NivelRow).Designacao, "\\par{}"));
			// Título
			//Result.Append("\\i{}Designação: \\i0{}" + Nivel.GetDesignacao(FRDBaseRow.NivelRow) + "\\par{}");
            Result.Append("\\i{}Título: \\i0{}" + Nivel.GetDesignacao(FRDBaseRow.NivelRow) + "\\par{}");
			// Datas
			if (FRDBaseRow.GetSFRDDatasProducaoRows().Length == 1)
			{
                string inicioTexto = string.Empty;
                if (!FRDBaseRow.GetSFRDDatasProducaoRows()[0].IsInicioTextoNull())
                    inicioTexto = FRDBaseRow.GetSFRDDatasProducaoRows()[0].InicioTexto + " ";

                Result.Append("\\i{}Data(s) de produção: \\i0{}" + inicioTexto + GUIHelper.GUIHelper.FormatDateInterval(FRDBaseRow.GetSFRDDatasProducaoRows()[0]) + "\\par{}");
			}

            // Agrupador
            if (FRDBaseRow.GetSFRDAgrupadorRows().Length == 1)
                Result.Append(GetConditionalText("\\i{}Agrupador: \\i0{}", FRDBaseRow.GetSFRDAgrupadorRows()[0].Agrupador, "\\par{}"));
            
            //Dimensão do documento
            var dimSup = GisaDataSetHelper.GetInstance().SFRDDimensaoSuporte.Cast<GISADataset.SFRDDimensaoSuporteRow>()
                .SingleOrDefault(r => r.IDFRDBase == FRDBaseRow.ID);
            if (dimSup != null)
                Result.Append(GetConditionalText("\\i{}Dimensão: \\i0{}", dimSup["Nota"] == DBNull.Value ? "" : dimSup.Nota, "\\par{}"));

            // Cota do documento na UF
            if (cotas.Count > 0)
            {
                Result.AppendLine("\\i{}Cota: \\i0{}\\par{}");
                var cotasStr = new StringBuilder();
                cotas.ForEach(c => {
                    if (cotasStr.Length > 0)
                        cotasStr.Append("\\b, \\b0");
                    cotasStr.Append(c);
                });
                Result.Append(cotasStr);
                Result.Append("\\par{}");
            }

			// --Contexto--
			StringBuilder Contexto = new StringBuilder();
            if (FRDBaseRow.GetSFRDContextoRows().Length == 1)
			{
                if (FRDBaseRow.NivelRow.IDTipoNivel != TipoNivel.ESTRUTURAL)
                    Contexto.Append(GetConditionalText("\\i{}História administrativa: \\i0{}\\par{}", string.Format("{0}", FRDBaseRow.GetSFRDContextoRows()[0]["HistoriaAdministrativa"]), "\\par{}"));
                else
                {
                    GISADataset.ControloAutRow caRow = FRDBaseRow.NivelRow.GetNivelControloAutRows()[0].ControloAutRow;
                    GISADataset.ControloAutDatasExistenciaRow cadeRow = null;

                    if (caRow.GetControloAutDatasExistenciaRows().Length > 0)
                    {
                        cadeRow = caRow.GetControloAutDatasExistenciaRows()[0];
                        Contexto.Append(GetConditionalText("\\i{}Datas de existência: \\i0{}\\par{}", GUIHelper.GUIHelper.FormatDateInterval(cadeRow), "\\par{}"));
                    }

                    Contexto.Append(GetConditionalText("\\i{}História: \\i0{}\\par{}", string.Format("{0}", caRow["DescHistoria"]), "\\par{}"));
                    Contexto.Append(GetConditionalText("\\i{}Zona geográfica: \\i0{}\\par{}", string.Format("{0}", caRow["DescZonaGeografica"]), "\\par{}"));
                    Contexto.Append(GetConditionalText("\\i{}Estatuto legal: \\i0{}\\par{}", string.Format("{0}", caRow["DescEstatutoLegal"]), "\\par{}"));
                    Contexto.Append(GetConditionalText("\\i{}Funções, ocupações e atividades: \\i0{}\\par{}", string.Format("{0}", caRow["DescOcupacoesActividades"]), "\\par{}"));
                    Contexto.Append(GetConditionalText("\\i{}Enquadramento legal: \\i0{}\\par{}", string.Format("{0}", caRow["DescEnquadramentoLegal"]), "\\par{}"));
                    Contexto.Append(GetConditionalText("\\i{}Estrutura interna: \\i0{}\\par{}", string.Format("{0}", caRow["DescEstruturaInterna"]), "\\par{}"));
                    Contexto.Append(GetConditionalText("\\i{}Contexto geral: \\i0{}\\par{}", string.Format("{0}", caRow["DescContextoGeral"]), "\\par{}"));
                    Contexto.Append(GetConditionalText("\\i{}Outras informações relevantes: \\i0{}\\par{}", string.Format("{0}", caRow["DescOutraInformacaoRelevante"]), "\\par{}"));
                }

                Contexto.Append(GetConditionalText("\\i{}História arquivística: \\i0{}\\par{}", string.Format("{0}", FRDBaseRow.GetSFRDContextoRows()[0]["HistoriaCustodial"]), "\\par{}"));
                Contexto.Append(GetConditionalText("\\i{}Fonte imediata de aquisicao: \\i0{}\\par{}", string.Format("{0}", FRDBaseRow.GetSFRDContextoRows()[0]["FonteImediataDeAquisicao"]), "\\par{}"));
                GISADataset.RelacaoHierarquicaRow[] rhrows = FRDBaseRow.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica();
                if (rhrows.Length > 0)
                {
                    Int64 idT = FRDBaseRow.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].TipoNivelRelacionadoRow.ID;
                    if ((idT == TipoNivelRelacionado.SR) || (idT == TipoNivelRelacionado.SSR))
                    {
                        if (System.Convert.ToBoolean(FRDBaseRow.GetSFRDContextoRows()[0]["SerieAberta"]))
                            Contexto.Append(GetConditionalText("\\i{}Condição da Série: \\i0{}\\par{}", string.Format("{0}", "Aberta"), "\\par{}"));
                        else
                            Contexto.Append(GetConditionalText("\\i{}Condição da Série: \\i0{}\\par{}", string.Format("{0}", "Fechada"), "\\par{}"));
                    }
                }
			}

            if (FRDBaseRow.NivelRow.IDTipoNivel == TipoNivel.ESTRUTURAL)
            {
                Result.Append(Section("\\fs36\\b{}Contexto\\b0{}\\fs24\\sb196\\sa48\\par{}\\sb0\\sa0{}", Contexto.ToString()));
            }
            else
            {
                GetAutores(FRDBaseRow.NivelRow);
                GetEntidadesProdutoras(FRDBaseRow.NivelRow);

                Result.Append(Section("\\fs36\\b{}Contexto\\b0{}\\fs24\\sb196\\sa48\\par{}\\sb0\\sa0{}",
                    GetConditionalText("\\i{}Autores: \\i0{}", printCAs(Autores), Contexto.ToString()),
                    GetConditionalText("\\i{}Entidade produtora: \\i0{}", printCAs(EPs), Contexto.ToString())
                    ));
            }

            // --ConteudoEstrutura--

            Result.Append(Section("\\fs36\\b{}Conteúdo e estrutura\\b0{}\\fs24\\sb196\\sa48\\par{}\\sb0\\sa0{}",
                GetConditionalText("\\i{}Tipologia informacional: \\i0{}\\par{}", GetTermosIndexados(FRDBaseRow, TipoNoticiaAut.TipologiaInformacional), "")));

            // -- Conteudo caso seja um processo de obras --
            ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            bool processoObras = IsRelatedToProcessoObras(FRDBaseRow, ho.Connection);
            // Dados estruturados de processos de obras:

            try {
                if (processoObras) {
                    //Result.Append(GetConditionalText("", "\\i{}Conteúdo informacional: \\i0{}\\par{}", "\\li128\\par\\li0{}"));
                    Result.Append(GetConditionalText("", "\\i{}Conteúdo informacional: \\i0{}\\par{}", ""));
                    Result.Append(gen_content_PROCESSO_OBRAS(FRDBaseRow.IDNivel, ho.Connection));
                }
            }
            catch (Exception ex) {
                Trace.WriteLine(ex);
            }
            finally {
                ho.Dispose();
                GisaDataSetHelper.ManageDatasetConstraints(true);
            }

            string ConteudoEstrutura = "";

			if (FRDBaseRow.GetSFRDConteudoEEstruturaRows().Length == 1)
			{
                if (!processoObras)
                    ConteudoEstrutura += GetConditionalText("\\i{}Conteúdo informacional: \\i0{}\\par{}", string.Format(" {0}", FRDBaseRow.GetSFRDConteudoEEstruturaRows()[0]["ConteudoInformacional"]), "\\li128\\par\\li0{}");
                else
                    ConteudoEstrutura += GetConditionalText("\\i{}Observações: \\i0{}\\par{}", string.Format(" {0}", FRDBaseRow.GetSFRDConteudoEEstruturaRows()[0]["ConteudoInformacional"]), "\\li128\\par\\li0{}");

				ConteudoEstrutura += GetConditionalText("\\i{}Diploma: \\i0{}\\par{}", GetTermosIndexados(FRDBaseRow, TipoNoticiaAut.Diploma), "\\li128\\li0{}");
				ConteudoEstrutura += GetConditionalText("\\i{}Modelo: \\i0{}\\par{}", GetTermosIndexados(FRDBaseRow, TipoNoticiaAut.Modelo), "\\li128\\li0{}");

				//Avaliação
				if (FRDBaseRow.GetSFRDAvaliacaoRows().Length > 0)
				{
					string ava = null;
                    var sfrda = FRDBaseRow.GetSFRDAvaliacaoRows()[0];

                    ConteudoEstrutura += GetConditionalText("\\i{}Observações/Enquadramento legal: \\i0{}\\par{}", string.Format("{0}", sfrda["Observacoes"]), "\\li128\\par\\li0{}");

                    if (sfrda.IsPreservarNull() && sfrda.IsPrazoConservacaoNull())
						ava = "";
					else
					{
                        if (sfrda.Preservar)
							ava = "Preservar.";
						else
                            ava = "Eliminar após " + (sfrda.IsPrazoConservacaoNull() ? "0" : sfrda.PrazoConservacao.ToString()) + " ano(s).";
					}    

					if (ava.CompareTo("") != 0)
						ConteudoEstrutura += GetConditionalText("\\i{}Avaliação: \\i0{}\\par{}", ava, "\\li128\\li0{}\\par{}");

                    ConteudoEstrutura += "\\i{}Publicado: \\i0{}\\par{}" + Concorrencia.translateBoolean(FRDBaseRow.GetSFRDAvaliacaoRows()[0].Publicar) + "\\li128\\par\\li0{}";
                    ConteudoEstrutura += GetConditionalText("\\i{}Referência na tabela de avaliação: \\i0{}\\par{}", string.Format("{0}", FRDBaseRow.GetSFRDAvaliacaoRows()[0]["RefTabelaAvaliacao"]), "\\li128\\li0{}\\par{}");

                    if (!sfrda.IsIDAutoEliminacaoNull())
                        ConteudoEstrutura += "\\i{}Auto de eliminação: \\i0{}\\par{}" + sfrda.AutoEliminacaoRow.Designacao + "\\li128\\li0{}\\par{}";
				}

                ConteudoEstrutura += GetConditionalText("\\i{}Incorporações: \\i0{}\\par{}", string.Format("{0}", FRDBaseRow.GetSFRDConteudoEEstruturaRows()[0]["Incorporacao"]), "\\li128\\par\\li0{}");

                Result.Append(ConteudoEstrutura);
			}

			// --Condições de acesso e de utilização--
			if (FRDBaseRow.GetSFRDCondicaoDeAcessoRows().Length == 1)
			{
				// Linguas
				string langs = "";
                foreach (GISADataset.SFRDLinguaRow lang in FRDBaseRow.GetSFRDCondicaoDeAcessoRows()[0].GetSFRDLinguaRows())
					langs += lang.Iso639Row.LanguageNameEnglish + ", ";
			
                if (langs.Length > 2)
					langs = langs.Substring(0, langs.Length - 2);

				//Alfabetos
				string alfs = "";
                foreach (GISADataset.SFRDAlfabetoRow alf in FRDBaseRow.GetSFRDCondicaoDeAcessoRows()[0].GetSFRDAlfabetoRows())
					alfs += alf.Iso15924Row.ScriptNameEnglish + ", ";
				
                if (alfs.Length > 2)
					alfs = alfs.Substring(0, alfs.Length - 2);

				//Formas Suporte/Acondicionamento
				string fsas = "";
                foreach (GISADataset.SFRDFormaSuporteAcondRow fsa in FRDBaseRow.GetSFRDCondicaoDeAcessoRows()[0].GetSFRDFormaSuporteAcondRows())
					fsas += fsa.TipoFormaSuporteAcondRow.Designacao + ", ";
				
                if (fsas.Length > 2)
					fsas = fsas.Substring(0, fsas.Length - 2);

				//Materiais de Suporte
				string mats = "";
                foreach (GISADataset.SFRDMaterialDeSuporteRow mat in FRDBaseRow.GetSFRDCondicaoDeAcessoRows()[0].GetSFRDMaterialDeSuporteRows())
					mats += mat.TipoMaterialDeSuporteRow.Designacao + ", ";

				if (mats.Length > 2)
					mats = mats.Substring(0, mats.Length - 2);


				//Tecnicas de Registo
				string tecs = "";
                foreach (GISADataset.SFRDTecnicasDeRegistoRow tec in FRDBaseRow.GetSFRDCondicaoDeAcessoRows()[0].GetSFRDTecnicasDeRegistoRows())
					tecs += tec.TipoTecnicasDeRegistoRow.Designacao + ", ";

				if (tecs.Length > 2)
					tecs = tecs.Substring(0, tecs.Length - 2);

				//Estado de Conservação
				string cons = "";
                foreach (GISADataset.SFRDEstadoDeConservacaoRow con in FRDBaseRow.GetSFRDCondicaoDeAcessoRows()[0].GetSFRDEstadoDeConservacaoRows())
					cons += con.TipoEstadoDeConservacaoRow.Designacao + ", ";

				if (cons.Length > 2)
					cons = cons.Substring(0, cons.Length - 2);

				Result.Append(Section("\\fs36\\b{}Condições de acesso e de utilização\\b0{}\\fs24\\sb196\\sa48\\par{}\\sb0\\sa0{}", GetConditionalText("\\i{}Condições de acesso: \\i0{}\\par{}", string.Format("{0}", FRDBaseRow.GetSFRDCondicaoDeAcessoRows()[0]["CondicaoDeAcesso"]), "\\li128\\par\\li0{}"), GetConditionalText("\\i{}Condições de reprodução: \\i0{}\\par{}", string.Format("{0}", FRDBaseRow.GetSFRDCondicaoDeAcessoRows()[0]["CondicaoDeReproducao"]), "\\li128\\par\\li0{}"), GetConditionalText("\\i{}Linguas: \\i0{}\\par{}", string.Format("{0}", langs), "\\li128\\par\\li0{}"), GetConditionalText("\\i{}Alfabetos: \\i0{}\\par{}", string.Format("{0}", alfs), "\\li128\\par\\li0{}"), GetConditionalText("\\i{}Formas de Suporte / Acondicionamento: \\i0{}\\par{}", string.Format("{0}", fsas), "\\li128\\par\\li0{}"), GetConditionalText("\\i{}Materiais de Suporte: \\i0{}\\par{}", string.Format("{0}", mats), "\\li128\\par\\li0{}"), GetConditionalText("\\i{}Técnicas de Registo: \\i0{}\\par{}", string.Format("{0}", tecs), "\\li128\\par\\li0{}"), GetConditionalText("\\i{}Estado de Conservação: \\i0{}\\par{}", string.Format("{0}", cons), "\\li128\\par\\li0{}"), GetConditionalText("\\i{}Instrumentos de pesquisa: \\i0{}\\par{}", string.Format("{0}", FRDBaseRow.GetSFRDCondicaoDeAcessoRows()[0]["AuxiliarDePesquisa"]), "\\li128\\par\\li0{}")));
            }

			// --Documentação associada--
			if (FRDBaseRow.GetSFRDDocumentacaoAssociadaRows().Length > 0)
				Result.Append(Section("\\fs36\\b{}Documentação associada\\b0{}\\fs24\\sb196\\sa48\\par{}\\sb0\\sa0{}", GetConditionalText("\\i{}Existência e localização de originais: \\i0{}\\par{}", string.Format("{0}", FRDBaseRow.GetSFRDDocumentacaoAssociadaRows()[0]["ExistenciaDeOriginais"]), "\\li128\\par\\li0{}") + GetConditionalText("\\i{}Existência e localização de cópias: \\i0{}\\par{}", string.Format("{0}", FRDBaseRow.GetSFRDDocumentacaoAssociadaRows()[0]["ExistenciaDeCopias"]), "\\li128\\par\\li0{}") + GetConditionalText("\\i{}Unidades de Descrição: \\i0{}\\par{}", string.Format("{0}", FRDBaseRow.GetSFRDDocumentacaoAssociadaRows()[0]["UnidadesRelacionadas"]), "\\li128\\par\\li0{}") + GetConditionalText("\\i{}Notas de Publicação: \\i0{}\\par{}", string.Format("{0}", FRDBaseRow.GetSFRDDocumentacaoAssociadaRows()[0]["NotaDePublicacao"]), "\\li128\\par\\li0{}")));

            // --Notas--
			if (FRDBaseRow.GetSFRDNotaGeralRows().Length > 0)
				Result.Append(Section("\\fs36\\b{}Nota Geral\\b0{}\\fs24\\sb196\\sa48\\par{}\\sb0\\sa0{}", GetConditionalText("", string.Format("{0}", FRDBaseRow.GetSFRDNotaGeralRows()[0]["NotaGeral"]), "\\li128\\par\\li0{}")));

			// --Controlo de Descrição--
			if (! (FRDBaseRow.IsRegrasOuConvencoesNull()))
			{
				string lastEdit = "";
				if (GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao.Select("IdFRDBase =" + FRDBaseRow.ID.ToString(), "DataEdicao DESC").Length > 0)
					lastEdit = ((GISADataset.FRDBaseDataDeDescricaoRow)(GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao.Select("IdFRDBase =" + FRDBaseRow.ID.ToString(), "DataEdicao DESC")[0])).DataEdicao.ToString();

				Result.Append(Section("\\fs36\\b{}Controlo de Descrição\\b0{}\\fs24\\sb196\\sa48\\par{}\\sb0\\sa0{}", 
                    GetConditionalText("\\i{}Regras e Convenções: \\i0{}\\par{}", string.Format("{0}", FRDBaseRow.RegrasOuConvencoes), "\\li128\\par\\li0{}") +
                    GetConditionalText("\\i{}Notas do arquivista: \\i0{}\\par{}", string.Format("{0}", FRDBaseRow.NotaDoArquivista), "\\li128\\par\\li0{}") + 
                    GetConditionalText("\\i{}Data da última edição: \\i0{}\\par{}", string.Format("{0}", lastEdit), "\\li128\\par\\li0{}")));
			}

			// --Indexação--
            Result.Append(Section("\\fs36\\b{}Indexação\\b0{}\\fs24\\sb196\\sa48\\par{}\\sb0\\sa0{}",
                GetConditionalText("\\i{}Conteúdos: \\i0{}\\par{}", GetTermosIndexados_OutrasFormas(FRDBaseRow), "")));
            
            // Acrescentar informação nos detalhes dos resultados da pesquisa de UAs:
            // Informação de requisição, se estiver requisitada e não devolvida. Qual a requisição, a data e a entidade e as notas.
            ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                //string ID = FRDBaseRow.ID.ToString();
                if (MovimentoRule.Current.estaRequisitado(FRDBaseRow.IDNivel, ho.Connection))
                {
                    MovimentoRule.RequisicaoInfo requisicao = MovimentoRule.Current.getRequisicaoInfo(FRDBaseRow.IDNivel, ho.Connection);

                    Result.Append("\\fs36\\b{}Requisição:\\b0{}\\fs24\\sb196\\sa48\\par{}\\sb0\\sa0{}");
                    // Codigo de movimento
                    Result.Append(GetConditionalText("\\i{}Movimento: \\i0{}", requisicao.idMovimento.ToString(), "\\par{}"));
                    Result.Append(GetConditionalText("\\i{}Data: \\i0{}", requisicao.data.ToString(), "\\par{}"));
                    Result.Append(GetConditionalText("\\i{}Entidade: \\i0{}", requisicao.entidade, "\\par{}"));
                    Result.Append(GetConditionalText("\\i{}Notas: \\i0{}", requisicao.notas, "\\par{}"));
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            finally
            {
                ho.Dispose();
            }

			return Result.ToString();
		}

		private string Section(string Header, params string[] Items)
		{
			StringBuilder Result = new StringBuilder();
			foreach (string s in Items)
				Result.Append(s);

			if (Result.Length > 0)
				return Header + Result.ToString();

			return "";
		}

        private void rtfDetalhes_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

            }
        }

        #region Objetos digitais
        private void ActivateDetalhesImagem()
        {
            ClearPreview();

            if (PesquisaList1.GetSelectedRows.Count() == 1)
            {
                ImagemEscolhida = null;
                lstImagens.Items.Clear();
                ClearPreview();
                trvODsFedora.Nodes.Clear();
                lstImagens.Items.Clear();
                lstImagens.DisplayMember = "Descricao";
                //var frdRow = PesquisaList1.SelectedItems[0].Tag as GISADataset.FRDBaseRow;
                var frdRow = PesquisaList1.SelectedRow as GISADataset.FRDBaseRow;
                var rhRow = frdRow.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First();

                GisaDataSetHelper.ManageDatasetConstraints(false);

                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try
                {
                    DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.LoadImagemVolume(GisaDataSetHelper.GetInstance(), frdRow.ID, ho.Connection);
                    DBAbstractDataLayer.DataAccessRules.FedoraRule.Current.LoadObjDigitalData(GisaDataSetHelper.GetInstance(), frdRow.IDNivel, rhRow.IDTipoNivelRelacionado, ho.Connection);
                }
                finally
                {
                    ho.Dispose();
                }

                GisaDataSetHelper.ManageDatasetConstraints(true);

                var isModoPublicadoOnly = MasterPanelPesquisa.cbModulo.SelectedItem.Equals(TranslationHelper.FormatModPesquisaIntToText(ModuloPesquisa.Publicacao));

                // listar imagens que não do tipo Fedora
                lstImagens.Items.AddRange(frdRow.GetSFRDImagemRows().Where(r => !r.Tipo.Equals(FedoraHelper.typeFedora)).OrderBy(r => r.GUIOrder).ToArray());

                // listar imagens do tipo fedora
                var odRows = FedoraHelper.GetObjetosDigitais(frdRow);

                foreach (var odRow in odRows.OrderBy(r => r.GUIOrder))
                {
                    var node = new TreeNode(); ;
                    node.ImageIndex = 3;
                    node.SelectedImageIndex = 3;
                    node.Text = odRow.Titulo;
                    node.Tag = odRow;
                    var odRowsSimples = odRow.GetObjetoDigitalRelacaoHierarquicaRowsByObjetoDigitalObjetoDigitalRelacaoHierarquicaUpper().Select(r => r.ObjetoDigitalRowByObjetoDigitalObjetoDigitalRelacaoHierarquica).ToList();
                    if (odRowsSimples.Count > 0)
                    {
                        foreach (var odRowSimples in odRowsSimples.OrderBy(r => r.GUIOrder))
                        {
                            var perm = PermissoesHelper.CalculateEffectivePermissions(odRowSimples, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.TrusteeRow, frdRow.NivelRow, PermissoesHelper.ObjDigOpREAD.TipoOperationRow);
                            if (perm == PermissoesHelper.PermissionType.ExplicitDeny || perm == PermissoesHelper.PermissionType.ImplicitDeny || (isModoPublicadoOnly && !odRowSimples.Publicado)) continue;

                            var subDocNode = new TreeNode();
                            subDocNode.Text = odRowSimples.Titulo;
                            subDocNode.Tag = odRowSimples;
                            subDocNode.ImageIndex = 3;
                            subDocNode.SelectedImageIndex = 3;

                            node.Nodes.Add(subDocNode);
                        }

                        if (node.Nodes.Count == 0) continue;

                        if (!isModoPublicadoOnly || !odRow.Publicado)
                            node.ForeColor = Color.Gray;

                        node.Expand();
                    }
                    else
                    {
                        var perm = PermissoesHelper.CalculateEffectivePermissions(odRow, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.TrusteeRow, frdRow.NivelRow, PermissoesHelper.ObjDigOpREAD.TipoOperationRow);
                        if (perm == PermissoesHelper.PermissionType.ExplicitDeny || perm == PermissoesHelper.PermissionType.ImplicitDeny || (isModoPublicadoOnly && !odRow.Publicado)) continue;
                    }

                    trvODsFedora.Nodes.Add(node);
                }

                pnlDetalhesImagem.BringToFront();
            }
            else
                ToolBar_ButtonClick(this, new ToolBarButtonClickEventArgs(ToolBarButton2));
        }

        private void lstImagens_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ImagemEscolhida = null;
            RefreshDetails();
            this.Cursor = Cursors.Default;
        }

        private void RefreshDetails()
        {
            trvODsFedora.SelectedNode = null;
            ImageViewerControl1.BringToFront();
            if (lstImagens.SelectedItems.Count != 1)
            {
                ClearPreview();
                return;
            }

            GISADataset.SFRDImagemRow imgRow = (GISADataset.SFRDImagemRow)(lstImagens.SelectedItems[0]);
            RefreshPreview(imgRow);
        }

        private void trvODsFedora_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            ClearPreview();
            this.Cursor = Cursors.WaitCursor;
            ImagemEscolhida = null;
            RefreshDetails(e.Node);
            this.Cursor = Cursors.Default;
        }

        private void RefreshDetails(TreeNode node)
        {
            lstImagens.SelectedItems.Clear();
            controlFedoraPdfViewer.BringToFront();
            var isModoPublicadoOnly = MasterPanelPesquisa.cbModulo.SelectedItem.Equals(TranslationHelper.FormatModPesquisaIntToText(ModuloPesquisa.Publicacao));
            if (node != null && node.Nodes.Count == 0) // mostrar mostrar pdf do nó selecionado caso não corresponda a um composto
            {
                var odRow = node.Tag as GISADataset.ObjetoDigitalRow;
                if (FedoraHelper.HasObjDigReadPermission(odRow.pid))
                    this.controlFedoraPdfViewer.ShowPDF(odRow.pid);
                this.btnFullScreen.Enabled = true;
                this.btnActualizar.Enabled = true;
            }
            else if (node != null && node.Nodes.Count > 0 && isModoPublicadoOnly) // se se tratar de um composto só mostrar se a pesquisa estiver definida para mostrar ODs publicados e esse OD estar publicado
            {
                var odRow = node.Tag as GISADataset.ObjetoDigitalRow;
                if (FedoraHelper.HasObjDigReadPermission(odRow.pid) && odRow.Publicado)
                {
                    this.controlFedoraPdfViewer.ShowPDF(odRow.pid);
                    this.btnFullScreen.Enabled = true;
                    this.btnActualizar.Enabled = true;
                }
            }
        }

        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            var node = trvODsFedora.SelectedNode;
            List<ListViewItem> itemsList = new List<ListViewItem>();
            int selectedIndex = -1;
            var isModoPublicadoOnly = MasterPanelPesquisa.cbModulo.SelectedItem.Equals(TranslationHelper.FormatModPesquisaIntToText(ModuloPesquisa.Publicacao));
            TreeNodeCollection nodes;
            if (node != null && node.Nodes.Count == 0) {
                if (node.Parent != null)
                {
                    selectedIndex = node.Parent.Nodes.IndexOf(node);
                    nodes = node.Parent.Nodes;
                }
                else
                {
                    selectedIndex = trvODsFedora.Nodes.IndexOf(node);
                    nodes = trvODsFedora.Nodes;
                }

                foreach (TreeNode n in nodes)
                    itemsList.Add(new ListViewItem(n.Text) { Tag = n.Tag, ForeColor = node.ForeColor });
            }
            else if (node != null && node.Nodes.Count > 0 && isModoPublicadoOnly)
            {
                var item = new ListViewItem(node.Text);
                item.Tag = node.Tag;
                selectedIndex = 0;
                itemsList.Add(item);
            }

            var ecraCompleto = new FormFullScreenPdf(itemsList, selectedIndex, FedoraHelper.TranslateQualityEnum(controlFedoraPdfViewer.Qualidade));
            ecraCompleto.ShowDialog();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (trvODsFedora.SelectedNode == null) return;

            ClearPreview();
            this.Cursor = Cursors.WaitCursor;
            ImagemEscolhida = null;

            RefreshDetails(trvODsFedora.SelectedNode);
            this.Cursor = Cursors.Default;
        }

        private void ClearPreview()
        {
            ImagemEscolhida = null;
            ImageViewerControl1.pictImagem.Image = null;
            ImageViewerControl1.SourceLocation = string.Empty;
            controlFedoraPdfViewer.Clear(true);
            this.btnFullScreen.Enabled = false;
            this.btnActualizar.Enabled = false;
        }

        private void RefreshPreview(GISADataset.SFRDImagemRow imgRow)
        {
            Image imagem = null;
            string caminhoFicheiro = string.Empty;
            string outroParametro = string.Empty;
            var tipo = TranslationHelper.FormatTipoAcessoTextToTipoAcessoEnum(imgRow.Tipo);

            try
            {
                switch (tipo)
                {
                    case ResourceAccessType.Smb:
                        caminhoFicheiro = imgRow.SFRDImagemVolumeRow.Mount + imgRow.Identificador;
                        imagem = ImageHelper.GetSmbImageResource(caminhoFicheiro);
                        ImageViewerControl1.UpdatePreviewImage(imagem, caminhoFicheiro, tipo);
                        break;
                    case ResourceAccessType.Web:
                        caminhoFicheiro = imgRow.SFRDImagemVolumeRow.Mount + imgRow.Identificador;
                        imagem = ImageHelper.GetWebImageResource(caminhoFicheiro);
                        ImageViewerControl1.UpdatePreviewImage(imagem, caminhoFicheiro, tipo);
                        break;
                    case ResourceAccessType.DICAnexo:
                    case ResourceAccessType.DICConteudo:
                        imagem = ImageHelper.GetDICImageResource(imgRow.Identificador, imgRow.SFRDImagemVolumeRow.Mount, tipo);
                        ImageViewerControl1.UpdatePreviewImage(imagem, imgRow.Identificador, imgRow.SFRDImagemVolumeRow.Mount, tipo);
                        break;
                }
            }
            catch (ImageHelper.UnretrievableResourceException ex) { Trace.WriteLine(ex.ToString()); }

            ImagemEscolhida = imagem;
            ImageViewerControl1.Visible = true;
        }

        private void OpenFormImageViewer_action(object sender)
        {
            if (lstImagens.SelectedItem == null) return;

            var sfrdimg = lstImagens.SelectedItem as GISADataset.SFRDImagemRow;
            if (ImagemEscolhida == null) return;

            frmImgViewer = new FormImageViewer();
            frmImgViewer.Imagem = ImagemEscolhida;
            frmImgViewer.Descricao = sfrdimg.Descricao;
            frmImgViewer.ToolBarButtonPreviousImage.Enabled = lstImagens.SelectedIndex > 0;
            frmImgViewer.ToolBarButtonNextImage.Enabled = lstImagens.SelectedIndex < lstImagens.Items.Count - 1;
            frmImgViewer.NextImage += FormImageViewer_NextImage;
            frmImgViewer.PreviousImage += FormImageViewer_PreviousImage;
            frmImgViewer.ShowDialog();
            frmImgViewer.NextImage -= FormImageViewer_NextImage;
            frmImgViewer.PreviousImage -= FormImageViewer_PreviousImage;
            frmImgViewer.Dispose();
            frmImgViewer = null;
        }

        private void ControlerResize_action(object sender)
        {
            if (ImagemEscolhida == null)
                return;

            var imgRow = lstImagens.SelectedItem as GISADataset.SFRDImagemRow;
            if (imgRow == null) return;

            string caminhoFicheiro = imgRow.SFRDImagemVolumeRow.Mount + imgRow.Identificador;
            ImageViewerControl1.UpdatePreviewImage(ImagemEscolhida, caminhoFicheiro, TranslationHelper.FormatTipoAcessoTextToTipoAcessoEnum(imgRow.Tipo));
        }

        private void FormImageViewer_NextImage(object sender, FormImageViewer.ImageViewerEventArgs e)
        {
            // make sure an item is seleted and that there is a next item to select
            if (!(lstImagens.SelectedIndex == -1) && lstImagens.SelectedIndex < lstImagens.Items.Count - 1)
            {
                GISADataset.SFRDImagemRow sfrdimg = (GISADataset.SFRDImagemRow)(lstImagens.Items[lstImagens.SelectedIndex + 1]);

                lstImagens.SelectedItem = sfrdimg;

                e.Imagem = ImagemEscolhida;
                e.Descricao = sfrdimg.Descricao;
                e.ExistsPrevious = true;
                e.ExistsNext = (lstImagens.SelectedIndex < lstImagens.Items.Count - 1);
            }
        }

        private void FormImageViewer_PreviousImage(object sender, FormImageViewer.ImageViewerEventArgs e)
        {

            // make sure an item is seleted and that there is a previous
            // item to select
            if (!(lstImagens.SelectedIndex == -1) && lstImagens.SelectedIndex > 0)
            {

                GISADataset.SFRDImagemRow sfrdimg = (GISADataset.SFRDImagemRow)(lstImagens.Items[lstImagens.SelectedIndex + -1]);

                lstImagens.SelectedItem = sfrdimg;

                e.Imagem = ImagemEscolhida;
                e.Descricao = sfrdimg.Descricao;
                e.ExistsPrevious = (lstImagens.SelectedIndex > 0);
                e.ExistsNext = true;
            }
        }
        #endregion

		private void UpdateToolBarButtons()
		{
			UpdateToolBarButtons(null);
		}

        private void UpdateToolBarButtons(GISADataset.NivelRow nRow)
        {
            if (PesquisaList1.Items.Count > 0)
            {
                MenuItemPrintInventarioResumido.Enabled = true;
                MenuItemPrintInventarioDetalhado.Enabled = true;
                MenuItemPrintCatalogoResumido.Enabled = true;
                MenuItemPrintCatalogoDetalhado.Enabled = true;
                MenuItemPrintResultadosPesquisaResumidos.Enabled = true;
                MenuItemPrintResultadosPesquisaDetalhados.Enabled = true;
            }
            else
            {
                MenuItemPrintInventarioResumido.Enabled = false;
                MenuItemPrintInventarioDetalhado.Enabled = false;
                MenuItemPrintCatalogoResumido.Enabled = false;
                MenuItemPrintCatalogoDetalhado.Enabled = false;
                MenuItemPrintResultadosPesquisaResumidos.Enabled = false;
                MenuItemPrintResultadosPesquisaDetalhados.Enabled = false;
            }

            if (nRow == null)
            {
                if (PesquisaList1.GetSelectedRows.Count() > 0)
                {
                    ToolBarButton2.Enabled = true;
                    ToolBarButton3.Enabled = true;
                    ToolBarButton4.Enabled = true;
                    ToolBarButton5.Enabled = true;
                    ToolBarButton_InfoEPs.Enabled = true;
                    ToolBarButtonSDocs.Enabled = true;
                }
                else
                {
                    ToolBarButton2.Enabled = false;
                    ToolBarButton3.Enabled = false;
                    ToolBarButton4.Enabled = false;
                    ToolBarButton5.Enabled = false;
                    ToolBarButton_InfoEPs.Enabled = false;
                    ToolBarButtonSDocs.Enabled = false;
                }
            }
            else
            {
                if (PesquisaList1.GetSelectedRows.Count() > 0)
                {
                    ToolBarButton2.Enabled = true;
                    ToolBarButton3.Enabled = true;
                    ToolBarButton4.Enabled = true;
                    ToolBarButton5.Enabled = true;
                    ToolBarButton_InfoEPs.Enabled = true;
                    ToolBarButtonSDocs.Enabled = HasSDocs(nRow.ID);
                }
                else
                {
                    ToolBarButton2.Enabled = false;
                    ToolBarButton3.Enabled = false;
                    ToolBarButton4.Enabled = false;
                    ToolBarButton5.Enabled = false;
                    ToolBarButton_InfoEPs.Enabled = false;
                    ToolBarButtonSDocs.Enabled = false;
                }
            }
        }

        public bool HasSDocs(long IDNivel)
        {
            long numSubDocs = 0;
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                numSubDocs = PesquisaRule.Current.CountSubDocumentos(IDNivel, ho.Connection);
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

            return numSubDocs > 0;
        }

		private void SlavePanelPesquisa_ParentChanged(object sender, System.EventArgs e)
		{
			if (this.Parent == null)
			{
				this.Visible = false;
				ViewToModel();
				Save();
				Deactivate();
			}
			else
			{
				this.Visible = true;
				LoadData();
				ModelToView();
			}
		}

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.rtfDetalhes.Copy();
        }

        private void ContextMenuRichText_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.copiarToolStripMenuItem.Enabled = (this.rtfDetalhes.SelectedText.Length > 0);
        }

        private void rtfDetalhes_Resize(object sender, EventArgs e)
        {
            this.rtfDetalhes.Invalidate();
        }

        
	}

} //end of root namespace
