using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using GISA.Model;
using GISA.SharedResources;

using GISA.Controls.Localizacao;
using GISA.Controls.ControloAut;

namespace GISA
{
	public class MasterPanelPesquisa : GISA.MasterPanel, IControloNivelListProvider
	{

	#region  Windows Form Designer generated code 

		public MasterPanelPesquisa() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            ToolBar.ButtonClick += ToolBar_ButtonClick;
            ButtonEP.Click += ButtonEP_Click;
            buttonAutor.Click += ButtonAutor_Click;
            ButtonConteudos.Click += ButtonConteudos_Click;
            ButtonTI.Click += ButtonTI_Click;
            chkEstruturaArquivistica.CheckedChanged += chkEstruturaArquivistica_CheckedChanged;
            chkFormaSuporte.CheckedChanged += chkFormaSuporte_CheckedChanged;
            chkMaterialSuporte.CheckedChanged += chkMaterialSuporte_CheckedChanged;
            chkTecnicaRegisto.CheckedChanged += chkTecnicaRegisto_CheckedChanged;
            chkEstadoConservacao.CheckedChanged += chkEstadoConservacao_CheckedChanged;
            base.ParentChanged += MasterPanelPesquisa_ParentChanged;
			GetExtraResources();
			cdbDataInicio.Year = System.DateTime.Now.Year;
			cdbDataInicio.Month = System.DateTime.Now.Month;
			cdbDataInicio.Day = System.DateTime.Now.Day;
			cdbDataFim.Year = System.DateTime.Now.Year;
			cdbDataFim.Month = System.DateTime.Now.Month;
			cdbDataFim.Day = System.DateTime.Now.Day;
            cdbInicioDoFim.Year = System.DateTime.Now.Year;
            cdbInicioDoFim.Month = System.DateTime.Now.Month;
            cdbInicioDoFim.Day = System.DateTime.Now.Day;
            cdbFimDoFim.Year = System.DateTime.Now.Year;
            cdbFimDoFim.Month = System.DateTime.Now.Month;
            cdbFimDoFim.Day = System.DateTime.Now.Day;
            rhTable.RelacaoHierarquicaRowChanged += rhTable_RelacaoHierarquicaRowChangingRelacaoHierarquicaRowDeleting;
            rhTable.RelacaoHierarquicaRowDeleting += rhTable_RelacaoHierarquicaRowChangingRelacaoHierarquicaRowDeleting;
            
            if (!SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsLicObrEnable()) {
                this.TabControl1.TabPages.Remove(tabPage1);
            }

            if (!SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsObjDigEnable())
            {
                this.lblODs.Visible = false;
                this.cbODs.Visible = false;
            }

			if (! (((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Select()[0])).NiveisOrganicos))
			{
				txtEntidadeProdutora.Enabled = false;				
				ButtonEP.Enabled = false;
			}

			cnList.grpLocalizacao.Text = "";
			LoadContents();
			cnList.LoadContents();
			cnList.Enabled = false;
            cnList.mTipoNivelRelLimitExcl = TipoNivelRelacionado.SSR;
           
			//LoadNivelRoot(trvwEstrutura)
			//LoadTimeLine()

            SwitchTipoPesquisa();

			AddHandlers();

            this.pesqContInfLicencaObras1.TheControloAutSelectionRetriever = retrieveSelection;
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
		internal System.Windows.Forms.TabControl TabControl1;
		internal System.Windows.Forms.TabPage TabPage2;
		internal System.Windows.Forms.TabPage TabPage3;
		internal System.Windows.Forms.TextBox txtEntidadeProdutora;
        internal System.Windows.Forms.TextBox txtIndexacao;

        internal System.Windows.Forms.ListBox lstTecnicaRegisto;
		internal System.Windows.Forms.ComboBox cbTecnicaRegisto;
		internal System.Windows.Forms.TextBox txtTipologiaInformacional;



		internal System.Windows.Forms.ToolBarButton ToolBarButtonExecutar;
		internal System.Windows.Forms.Button ButtonEP;
		internal System.Windows.Forms.Button ButtonConteudos;
		internal System.Windows.Forms.Button ButtonTI;
		internal System.Windows.Forms.CheckBox chkTecnicaRegisto;
		internal System.Windows.Forms.TextBox txtConteudoInformacional;
		internal System.Windows.Forms.CheckBox chkEstadoConservacao;
		internal System.Windows.Forms.CheckBox chkMaterialSuporte;
        internal System.Windows.Forms.CheckBox chkFormaSuporte;
		internal System.Windows.Forms.ListBox lstEstadoConservacao;
		internal System.Windows.Forms.ComboBox cbMaterialSuporte;
		internal System.Windows.Forms.ListBox lstMaterialSuporte;
		internal System.Windows.Forms.ComboBox cbFormaSuporte;
        internal System.Windows.Forms.ListBox lstFormaSuporte;
		internal System.Windows.Forms.TextBox txtCota;
		internal ControloNivelList cnList;
		internal System.Windows.Forms.CheckBox chkEstruturaArquivistica;
		internal System.Windows.Forms.GroupBox grpMaterialSuporte;
		internal System.Windows.Forms.GroupBox grpEstadoConservacao;
		internal System.Windows.Forms.GroupBox grpTecnicaRegisto;
		internal System.Windows.Forms.GroupBox grpFormaSuporte;
		internal System.Windows.Forms.Label lblModulo;
		internal System.Windows.Forms.ComboBox cbModulo;
		internal System.Windows.Forms.Label lblCota;
		internal System.Windows.Forms.Label lblConteudoInformacional;
		internal System.Windows.Forms.Label lblTipologiaInformacional;
		internal System.Windows.Forms.Label lblIndexacao;
		internal System.Windows.Forms.Label lblEntidadeProdutora;
		internal System.Windows.Forms.TextBox txtNotas;
		internal System.Windows.Forms.Label lblNotas;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonAjuda;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonLimpar;
		internal System.Windows.Forms.Label lblDesignacao;
		internal System.Windows.Forms.TextBox txtDesignacao;
		internal System.Windows.Forms.Label lblDataProducaoFim;
		internal System.Windows.Forms.Label lblDataProducaoInicio;
		internal System.Windows.Forms.Label lblDatasProducao;
		internal GISA.Controls.PxCompleteDateBox cdbDataFim;
        internal GISA.Controls.PxCompleteDateBox cdbDataInicio;
		internal System.Windows.Forms.TextBox txtCodigoParcial;
		internal System.Windows.Forms.Label lblCodigoParcial;
        internal ToolBarButton ToolBarButtonTipoPesquisa;
        internal Panel panel1;
        internal Label label2;
        internal TextBox txtPesquisaSimples;
        private ToolBarButton ToolBarButtonSep1;
        internal Label lblID;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        internal GISA.Controls.PxCompleteDateBox cdbInicioDoFim;
        internal Label label1;
        internal Label label3;
        internal GISA.Controls.PxCompleteDateBox cdbFimDoFim;
        internal TextBox txtID;
        private GroupBox grpNiveisDescricao;
        internal ListBox lstNiveisDocumentais;
        private TabPage tabPage1;
        private PesqContInfLicencaObras pesqContInfLicencaObras1;
        internal TextBox txtAutor;
        internal Label lblAutor;
        internal Button buttonAutor;
        internal TextBox txtAgrupador;
        internal Label lblAgrupador;
        internal ComboBox cbODs;
        private Label lblODs;
		internal System.Windows.Forms.CheckBox chkApenasDataElimExp;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.chkEstadoConservacao = new System.Windows.Forms.CheckBox();
            this.txtAgrupador = new System.Windows.Forms.TextBox();
            this.lblAgrupador = new System.Windows.Forms.Label();
            this.chkTecnicaRegisto = new System.Windows.Forms.CheckBox();
            this.chkMaterialSuporte = new System.Windows.Forms.CheckBox();
            this.chkFormaSuporte = new System.Windows.Forms.CheckBox();
            this.txtAutor = new System.Windows.Forms.TextBox();
            this.lblAutor = new System.Windows.Forms.Label();
            this.buttonAutor = new System.Windows.Forms.Button();
            this.grpNiveisDescricao = new System.Windows.Forms.GroupBox();
            this.lstNiveisDocumentais = new System.Windows.Forms.ListBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cdbInicioDoFim = new GISA.Controls.PxCompleteDateBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cdbFimDoFim = new GISA.Controls.PxCompleteDateBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cdbDataInicio = new GISA.Controls.PxCompleteDateBox();
            this.lblDataProducaoInicio = new System.Windows.Forms.Label();
            this.lblDataProducaoFim = new System.Windows.Forms.Label();
            this.cdbDataFim = new GISA.Controls.PxCompleteDateBox();
            this.lblID = new System.Windows.Forms.Label();
            this.chkApenasDataElimExp = new System.Windows.Forms.CheckBox();
            this.txtCodigoParcial = new System.Windows.Forms.TextBox();
            this.lblCodigoParcial = new System.Windows.Forms.Label();
            this.lblDatasProducao = new System.Windows.Forms.Label();
            this.txtDesignacao = new System.Windows.Forms.TextBox();
            this.lblDesignacao = new System.Windows.Forms.Label();
            this.txtNotas = new System.Windows.Forms.TextBox();
            this.lblNotas = new System.Windows.Forms.Label();
            this.cbModulo = new System.Windows.Forms.ComboBox();
            this.lblModulo = new System.Windows.Forms.Label();
            this.grpMaterialSuporte = new System.Windows.Forms.GroupBox();
            this.cbMaterialSuporte = new System.Windows.Forms.ComboBox();
            this.lstMaterialSuporte = new System.Windows.Forms.ListBox();
            this.grpEstadoConservacao = new System.Windows.Forms.GroupBox();
            this.lstEstadoConservacao = new System.Windows.Forms.ListBox();
            this.grpTecnicaRegisto = new System.Windows.Forms.GroupBox();
            this.cbTecnicaRegisto = new System.Windows.Forms.ComboBox();
            this.lstTecnicaRegisto = new System.Windows.Forms.ListBox();
            this.grpFormaSuporte = new System.Windows.Forms.GroupBox();
            this.cbFormaSuporte = new System.Windows.Forms.ComboBox();
            this.lstFormaSuporte = new System.Windows.Forms.ListBox();
            this.txtCota = new System.Windows.Forms.TextBox();
            this.lblCota = new System.Windows.Forms.Label();
            this.txtConteudoInformacional = new System.Windows.Forms.TextBox();
            this.lblConteudoInformacional = new System.Windows.Forms.Label();
            this.ButtonTI = new System.Windows.Forms.Button();
            this.ButtonConteudos = new System.Windows.Forms.Button();
            this.ButtonEP = new System.Windows.Forms.Button();
            this.txtTipologiaInformacional = new System.Windows.Forms.TextBox();
            this.lblTipologiaInformacional = new System.Windows.Forms.Label();
            this.txtIndexacao = new System.Windows.Forms.TextBox();
            this.txtEntidadeProdutora = new System.Windows.Forms.TextBox();
            this.lblIndexacao = new System.Windows.Forms.Label();
            this.lblEntidadeProdutora = new System.Windows.Forms.Label();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.chkEstruturaArquivistica = new System.Windows.Forms.CheckBox();
            this.cnList = new GISA.Controls.Localizacao.ControloNivelList();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pesqContInfLicencaObras1 = new GISA.PesqContInfLicencaObras();
            this.ToolBarButtonExecutar = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonAjuda = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonLimpar = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonTipoPesquisa = new System.Windows.Forms.ToolBarButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPesquisaSimples = new System.Windows.Forms.TextBox();
            this.ToolBarButtonSep1 = new System.Windows.Forms.ToolBarButton();
            this.lblODs = new System.Windows.Forms.Label();
            this.cbODs = new System.Windows.Forms.ComboBox();
            this.pnlToolbarPadding.SuspendLayout();
            this.TabControl1.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.grpNiveisDescricao.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpMaterialSuporte.SuspendLayout();
            this.grpEstadoConservacao.SuspendLayout();
            this.grpTecnicaRegisto.SuspendLayout();
            this.grpFormaSuporte.SuspendLayout();
            this.TabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFuncao
            // 
            this.lblFuncao.Location = new System.Drawing.Point(0, 0);
            this.lblFuncao.Size = new System.Drawing.Size(852, 24);
            this.lblFuncao.Text = "Pesquisa";
            // 
            // ToolBar
            // 
            this.ToolBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.ToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.ToolBarButtonExecutar,
            this.ToolBarButtonLimpar,
            this.ToolBarButtonTipoPesquisa,
            this.ToolBarButtonSep1,
            this.ToolBarButtonAjuda});
            this.ToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolBar.ImageList = null;
            this.ToolBar.Location = new System.Drawing.Point(5, 0);
            this.ToolBar.Size = new System.Drawing.Size(842, 26);
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
            this.pnlToolbarPadding.Size = new System.Drawing.Size(852, 28);
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Controls.Add(this.TabPage3);
            this.TabControl1.Controls.Add(this.tabPage1);
            this.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl1.Location = new System.Drawing.Point(0, 52);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(852, 589);
            this.TabControl1.TabIndex = 1;
            // 
            // TabPage2
            // 
            this.TabPage2.AutoScroll = true;
            this.TabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.TabPage2.Controls.Add(this.cbODs);
            this.TabPage2.Controls.Add(this.lblODs);
            this.TabPage2.Controls.Add(this.chkEstadoConservacao);
            this.TabPage2.Controls.Add(this.txtAgrupador);
            this.TabPage2.Controls.Add(this.lblAgrupador);
            this.TabPage2.Controls.Add(this.chkTecnicaRegisto);
            this.TabPage2.Controls.Add(this.chkMaterialSuporte);
            this.TabPage2.Controls.Add(this.chkFormaSuporte);
            this.TabPage2.Controls.Add(this.txtAutor);
            this.TabPage2.Controls.Add(this.lblAutor);
            this.TabPage2.Controls.Add(this.buttonAutor);
            this.TabPage2.Controls.Add(this.grpNiveisDescricao);
            this.TabPage2.Controls.Add(this.txtID);
            this.TabPage2.Controls.Add(this.groupBox2);
            this.TabPage2.Controls.Add(this.groupBox1);
            this.TabPage2.Controls.Add(this.lblID);
            this.TabPage2.Controls.Add(this.chkApenasDataElimExp);
            this.TabPage2.Controls.Add(this.txtCodigoParcial);
            this.TabPage2.Controls.Add(this.lblCodigoParcial);
            this.TabPage2.Controls.Add(this.lblDatasProducao);
            this.TabPage2.Controls.Add(this.txtDesignacao);
            this.TabPage2.Controls.Add(this.lblDesignacao);
            this.TabPage2.Controls.Add(this.txtNotas);
            this.TabPage2.Controls.Add(this.lblNotas);
            this.TabPage2.Controls.Add(this.cbModulo);
            this.TabPage2.Controls.Add(this.lblModulo);
            this.TabPage2.Controls.Add(this.grpMaterialSuporte);
            this.TabPage2.Controls.Add(this.grpEstadoConservacao);
            this.TabPage2.Controls.Add(this.grpTecnicaRegisto);
            this.TabPage2.Controls.Add(this.grpFormaSuporte);
            this.TabPage2.Controls.Add(this.txtCota);
            this.TabPage2.Controls.Add(this.lblCota);
            this.TabPage2.Controls.Add(this.txtConteudoInformacional);
            this.TabPage2.Controls.Add(this.lblConteudoInformacional);
            this.TabPage2.Controls.Add(this.ButtonTI);
            this.TabPage2.Controls.Add(this.ButtonConteudos);
            this.TabPage2.Controls.Add(this.ButtonEP);
            this.TabPage2.Controls.Add(this.txtTipologiaInformacional);
            this.TabPage2.Controls.Add(this.lblTipologiaInformacional);
            this.TabPage2.Controls.Add(this.txtIndexacao);
            this.TabPage2.Controls.Add(this.txtEntidadeProdutora);
            this.TabPage2.Controls.Add(this.lblIndexacao);
            this.TabPage2.Controls.Add(this.lblEntidadeProdutora);
            this.TabPage2.Location = new System.Drawing.Point(4, 22);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Size = new System.Drawing.Size(844, 563);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Descrição";
            // 
            // chkEstadoConservacao
            // 
            this.chkEstadoConservacao.Location = new System.Drawing.Point(632, 423);
            this.chkEstadoConservacao.Name = "chkEstadoConservacao";
            this.chkEstadoConservacao.Size = new System.Drawing.Size(139, 16);
            this.chkEstadoConservacao.TabIndex = 23;
            this.chkEstadoConservacao.Text = "Estado de conservação";
            // 
            // txtAgrupador
            // 
            this.txtAgrupador.Location = new System.Drawing.Point(126, 344);
            this.txtAgrupador.Name = "txtAgrupador";
            this.txtAgrupador.Size = new System.Drawing.Size(312, 20);
            this.txtAgrupador.TabIndex = 17;
            this.txtAgrupador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterPanelPesquisa_KeyDown);
            // 
            // lblAgrupador
            // 
            this.lblAgrupador.Location = new System.Drawing.Point(3, 341);
            this.lblAgrupador.Name = "lblAgrupador";
            this.lblAgrupador.Size = new System.Drawing.Size(128, 24);
            this.lblAgrupador.TabIndex = 62;
            this.lblAgrupador.Text = "Agrupador:";
            this.lblAgrupador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkTecnicaRegisto
            // 
            this.chkTecnicaRegisto.Location = new System.Drawing.Point(425, 423);
            this.chkTecnicaRegisto.Name = "chkTecnicaRegisto";
            this.chkTecnicaRegisto.Size = new System.Drawing.Size(120, 16);
            this.chkTecnicaRegisto.TabIndex = 22;
            this.chkTecnicaRegisto.Text = "Técnica de registo";
            // 
            // chkMaterialSuporte
            // 
            this.chkMaterialSuporte.Location = new System.Drawing.Point(220, 423);
            this.chkMaterialSuporte.Name = "chkMaterialSuporte";
            this.chkMaterialSuporte.Size = new System.Drawing.Size(120, 16);
            this.chkMaterialSuporte.TabIndex = 21;
            this.chkMaterialSuporte.Text = "Material de suporte";
            // 
            // chkFormaSuporte
            // 
            this.chkFormaSuporte.Location = new System.Drawing.Point(14, 423);
            this.chkFormaSuporte.Name = "chkFormaSuporte";
            this.chkFormaSuporte.Size = new System.Drawing.Size(167, 16);
            this.chkFormaSuporte.TabIndex = 20;
            this.chkFormaSuporte.Text = "Suporte e acondicionamento";
            // 
            // txtAutor
            // 
            this.txtAutor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAutor.Location = new System.Drawing.Point(128, 87);
            this.txtAutor.Name = "txtAutor";
            this.txtAutor.Size = new System.Drawing.Size(364, 20);
            this.txtAutor.TabIndex = 4;
            this.txtAutor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterPanelPesquisa_KeyDown);
            // 
            // lblAutor
            // 
            this.lblAutor.Location = new System.Drawing.Point(3, 84);
            this.lblAutor.Name = "lblAutor";
            this.lblAutor.Size = new System.Drawing.Size(128, 24);
            this.lblAutor.TabIndex = 61;
            this.lblAutor.Text = "Autor:";
            this.lblAutor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonAutor
            // 
            this.buttonAutor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAutor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonAutor.ImageIndex = 1;
            this.buttonAutor.Location = new System.Drawing.Point(498, 86);
            this.buttonAutor.Name = "buttonAutor";
            this.buttonAutor.Size = new System.Drawing.Size(24, 20);
            this.buttonAutor.TabIndex = 5;
            // 
            // grpNiveisDescricao
            // 
            this.grpNiveisDescricao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpNiveisDescricao.Controls.Add(this.lstNiveisDocumentais);
            this.grpNiveisDescricao.Location = new System.Drawing.Point(542, 27);
            this.grpNiveisDescricao.Name = "grpNiveisDescricao";
            this.grpNiveisDescricao.Size = new System.Drawing.Size(248, 81);
            this.grpNiveisDescricao.TabIndex = 58;
            this.grpNiveisDescricao.TabStop = false;
            this.grpNiveisDescricao.Text = "Nivel de descrição";
            // 
            // lstNiveisDocumentais
            // 
            this.lstNiveisDocumentais.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstNiveisDocumentais.FormattingEnabled = true;
            this.lstNiveisDocumentais.Location = new System.Drawing.Point(3, 16);
            this.lstNiveisDocumentais.Name = "lstNiveisDocumentais";
            this.lstNiveisDocumentais.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstNiveisDocumentais.Size = new System.Drawing.Size(242, 62);
            this.lstNiveisDocumentais.TabIndex = 0;
            // 
            // txtID
            // 
            this.txtID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID.Location = new System.Drawing.Point(410, 36);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(82, 20);
            this.txtID.TabIndex = 2;
            this.txtID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterPanelPesquisa_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cdbInicioDoFim);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cdbFimDoFim);
            this.groupBox2.Location = new System.Drawing.Point(361, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(226, 77);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fim";
            // 
            // cdbInicioDoFim
            // 
            this.cdbInicioDoFim.Checked = false;
            this.cdbInicioDoFim.Day = 1;
            this.cdbInicioDoFim.Location = new System.Drawing.Point(47, 19);
            this.cdbInicioDoFim.Month = 1;
            this.cdbInicioDoFim.Name = "cdbInicioDoFim";
            this.cdbInicioDoFim.Size = new System.Drawing.Size(166, 22);
            this.cdbInicioDoFim.TabIndex = 1;
            this.cdbInicioDoFim.Year = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "entre";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(11, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "e";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cdbFimDoFim
            // 
            this.cdbFimDoFim.Checked = false;
            this.cdbFimDoFim.Day = 1;
            this.cdbFimDoFim.Location = new System.Drawing.Point(47, 47);
            this.cdbFimDoFim.Month = 1;
            this.cdbFimDoFim.Name = "cdbFimDoFim";
            this.cdbFimDoFim.Size = new System.Drawing.Size(166, 22);
            this.cdbFimDoFim.TabIndex = 3;
            this.cdbFimDoFim.Year = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cdbDataInicio);
            this.groupBox1.Controls.Add(this.lblDataProducaoInicio);
            this.groupBox1.Controls.Add(this.lblDataProducaoFim);
            this.groupBox1.Controls.Add(this.cdbDataFim);
            this.groupBox1.Location = new System.Drawing.Point(128, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 77);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Início";
            // 
            // cdbDataInicio
            // 
            this.cdbDataInicio.Checked = false;
            this.cdbDataInicio.Day = 1;
            this.cdbDataInicio.Location = new System.Drawing.Point(47, 19);
            this.cdbDataInicio.Month = 1;
            this.cdbDataInicio.Name = "cdbDataInicio";
            this.cdbDataInicio.Size = new System.Drawing.Size(166, 22);
            this.cdbDataInicio.TabIndex = 1;
            this.cdbDataInicio.Year = 1;
            // 
            // lblDataProducaoInicio
            // 
            this.lblDataProducaoInicio.Location = new System.Drawing.Point(11, 19);
            this.lblDataProducaoInicio.Name = "lblDataProducaoInicio";
            this.lblDataProducaoInicio.Size = new System.Drawing.Size(44, 24);
            this.lblDataProducaoInicio.TabIndex = 0;
            this.lblDataProducaoInicio.Text = "entre";
            this.lblDataProducaoInicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDataProducaoFim
            // 
            this.lblDataProducaoFim.Location = new System.Drawing.Point(11, 43);
            this.lblDataProducaoFim.Name = "lblDataProducaoFim";
            this.lblDataProducaoFim.Size = new System.Drawing.Size(16, 24);
            this.lblDataProducaoFim.TabIndex = 2;
            this.lblDataProducaoFim.Text = "e";
            this.lblDataProducaoFim.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cdbDataFim
            // 
            this.cdbDataFim.Checked = false;
            this.cdbDataFim.Day = 1;
            this.cdbDataFim.Location = new System.Drawing.Point(47, 47);
            this.cdbDataFim.Month = 1;
            this.cdbDataFim.Name = "cdbDataFim";
            this.cdbDataFim.Size = new System.Drawing.Size(166, 22);
            this.cdbDataFim.TabIndex = 3;
            this.cdbDataFim.Year = 1;
            // 
            // lblID
            // 
            this.lblID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblID.Location = new System.Drawing.Point(331, 35);
            this.lblID.MaximumSize = new System.Drawing.Size(80, 24);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(73, 20);
            this.lblID.TabIndex = 55;
            this.lblID.Text = "Identificador:";
            this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkApenasDataElimExp
            // 
            this.chkApenasDataElimExp.Location = new System.Drawing.Point(4, 396);
            this.chkApenasDataElimExp.Name = "chkApenasDataElimExp";
            this.chkApenasDataElimExp.Size = new System.Drawing.Size(370, 16);
            this.chkApenasDataElimExp.TabIndex = 19;
            this.chkApenasDataElimExp.Text = "Incluir apenas resultados com prazo de conservação ultrapassado";
            // 
            // txtCodigoParcial
            // 
            this.txtCodigoParcial.Location = new System.Drawing.Point(128, 36);
            this.txtCodigoParcial.MaximumSize = new System.Drawing.Size(312, 20);
            this.txtCodigoParcial.Name = "txtCodigoParcial";
            this.txtCodigoParcial.Size = new System.Drawing.Size(197, 20);
            this.txtCodigoParcial.TabIndex = 1;
            this.txtCodigoParcial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterPanelPesquisa_KeyDown);
            // 
            // lblCodigoParcial
            // 
            this.lblCodigoParcial.Location = new System.Drawing.Point(3, 33);
            this.lblCodigoParcial.Name = "lblCodigoParcial";
            this.lblCodigoParcial.Size = new System.Drawing.Size(128, 24);
            this.lblCodigoParcial.TabIndex = 51;
            this.lblCodigoParcial.Text = "Código parcial:";
            this.lblCodigoParcial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDatasProducao
            // 
            this.lblDatasProducao.Location = new System.Drawing.Point(3, 139);
            this.lblDatasProducao.Name = "lblDatasProducao";
            this.lblDatasProducao.Size = new System.Drawing.Size(128, 24);
            this.lblDatasProducao.TabIndex = 48;
            this.lblDatasProducao.Text = "Data de produção:";
            this.lblDatasProducao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDesignacao
            // 
            this.txtDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDesignacao.Location = new System.Drawing.Point(128, 60);
            this.txtDesignacao.Name = "txtDesignacao";
            this.txtDesignacao.Size = new System.Drawing.Size(364, 20);
            this.txtDesignacao.TabIndex = 3;
            this.txtDesignacao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterPanelPesquisa_KeyDown);
            // 
            // lblDesignacao
            // 
            this.lblDesignacao.Location = new System.Drawing.Point(3, 57);
            this.lblDesignacao.Name = "lblDesignacao";
            this.lblDesignacao.Size = new System.Drawing.Size(128, 24);
            this.lblDesignacao.TabIndex = 46;
            this.lblDesignacao.Text = "Título:";
            this.lblDesignacao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNotas
            // 
            this.txtNotas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotas.Location = new System.Drawing.Point(126, 294);
            this.txtNotas.Name = "txtNotas";
            this.txtNotas.Size = new System.Drawing.Size(662, 20);
            this.txtNotas.TabIndex = 15;
            this.txtNotas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterPanelPesquisa_KeyDown);
            // 
            // lblNotas
            // 
            this.lblNotas.Location = new System.Drawing.Point(3, 291);
            this.lblNotas.Name = "lblNotas";
            this.lblNotas.Size = new System.Drawing.Size(128, 24);
            this.lblNotas.TabIndex = 22;
            this.lblNotas.Text = "Notas:";
            this.lblNotas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbModulo
            // 
            this.cbModulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModulo.Items.AddRange(new object[] {
            "Pesquisa total",
            "Pesquisa pública",
            "Pesquisa não publicados"});
            this.cbModulo.Location = new System.Drawing.Point(128, 8);
            this.cbModulo.Name = "cbModulo";
            this.cbModulo.Size = new System.Drawing.Size(197, 21);
            this.cbModulo.TabIndex = 0;
            // 
            // lblModulo
            // 
            this.lblModulo.Location = new System.Drawing.Point(3, 5);
            this.lblModulo.Name = "lblModulo";
            this.lblModulo.Size = new System.Drawing.Size(128, 24);
            this.lblModulo.TabIndex = 0;
            this.lblModulo.Text = "Módulo:";
            this.lblModulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpMaterialSuporte
            // 
            this.grpMaterialSuporte.Controls.Add(this.cbMaterialSuporte);
            this.grpMaterialSuporte.Controls.Add(this.lstMaterialSuporte);
            this.grpMaterialSuporte.Enabled = false;
            this.grpMaterialSuporte.Location = new System.Drawing.Point(211, 424);
            this.grpMaterialSuporte.Name = "grpMaterialSuporte";
            this.grpMaterialSuporte.Size = new System.Drawing.Size(200, 112);
            this.grpMaterialSuporte.TabIndex = 20;
            this.grpMaterialSuporte.TabStop = false;
            // 
            // cbMaterialSuporte
            // 
            this.cbMaterialSuporte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMaterialSuporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMaterialSuporte.Items.AddRange(new object[] {
            "E",
            "Ou"});
            this.cbMaterialSuporte.Location = new System.Drawing.Point(144, 24);
            this.cbMaterialSuporte.Name = "cbMaterialSuporte";
            this.cbMaterialSuporte.Size = new System.Drawing.Size(46, 21);
            this.cbMaterialSuporte.TabIndex = 1;
            // 
            // lstMaterialSuporte
            // 
            this.lstMaterialSuporte.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstMaterialSuporte.IntegralHeight = false;
            this.lstMaterialSuporte.Location = new System.Drawing.Point(8, 24);
            this.lstMaterialSuporte.Name = "lstMaterialSuporte";
            this.lstMaterialSuporte.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstMaterialSuporte.Size = new System.Drawing.Size(128, 80);
            this.lstMaterialSuporte.TabIndex = 0;
            // 
            // grpEstadoConservacao
            // 
            this.grpEstadoConservacao.Controls.Add(this.lstEstadoConservacao);
            this.grpEstadoConservacao.Enabled = false;
            this.grpEstadoConservacao.Location = new System.Drawing.Point(624, 423);
            this.grpEstadoConservacao.Name = "grpEstadoConservacao";
            this.grpEstadoConservacao.Size = new System.Drawing.Size(166, 112);
            this.grpEstadoConservacao.TabIndex = 45;
            this.grpEstadoConservacao.TabStop = false;
            // 
            // lstEstadoConservacao
            // 
            this.lstEstadoConservacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstEstadoConservacao.IntegralHeight = false;
            this.lstEstadoConservacao.Location = new System.Drawing.Point(8, 24);
            this.lstEstadoConservacao.Name = "lstEstadoConservacao";
            this.lstEstadoConservacao.Size = new System.Drawing.Size(150, 80);
            this.lstEstadoConservacao.TabIndex = 0;
            // 
            // grpTecnicaRegisto
            // 
            this.grpTecnicaRegisto.Controls.Add(this.cbTecnicaRegisto);
            this.grpTecnicaRegisto.Controls.Add(this.lstTecnicaRegisto);
            this.grpTecnicaRegisto.Enabled = false;
            this.grpTecnicaRegisto.Location = new System.Drawing.Point(417, 424);
            this.grpTecnicaRegisto.Name = "grpTecnicaRegisto";
            this.grpTecnicaRegisto.Size = new System.Drawing.Size(200, 112);
            this.grpTecnicaRegisto.TabIndex = 42;
            this.grpTecnicaRegisto.TabStop = false;
            // 
            // cbTecnicaRegisto
            // 
            this.cbTecnicaRegisto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTecnicaRegisto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTecnicaRegisto.Items.AddRange(new object[] {
            "E",
            "Ou"});
            this.cbTecnicaRegisto.Location = new System.Drawing.Point(144, 24);
            this.cbTecnicaRegisto.Name = "cbTecnicaRegisto";
            this.cbTecnicaRegisto.Size = new System.Drawing.Size(48, 21);
            this.cbTecnicaRegisto.TabIndex = 1;
            // 
            // lstTecnicaRegisto
            // 
            this.lstTecnicaRegisto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstTecnicaRegisto.IntegralHeight = false;
            this.lstTecnicaRegisto.Location = new System.Drawing.Point(8, 24);
            this.lstTecnicaRegisto.Name = "lstTecnicaRegisto";
            this.lstTecnicaRegisto.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstTecnicaRegisto.Size = new System.Drawing.Size(128, 80);
            this.lstTecnicaRegisto.TabIndex = 0;
            // 
            // grpFormaSuporte
            // 
            this.grpFormaSuporte.Controls.Add(this.cbFormaSuporte);
            this.grpFormaSuporte.Controls.Add(this.lstFormaSuporte);
            this.grpFormaSuporte.Enabled = false;
            this.grpFormaSuporte.Location = new System.Drawing.Point(5, 424);
            this.grpFormaSuporte.Name = "grpFormaSuporte";
            this.grpFormaSuporte.Size = new System.Drawing.Size(200, 112);
            this.grpFormaSuporte.TabIndex = 36;
            this.grpFormaSuporte.TabStop = false;
            // 
            // cbFormaSuporte
            // 
            this.cbFormaSuporte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFormaSuporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormaSuporte.Items.AddRange(new object[] {
            "E",
            "Ou"});
            this.cbFormaSuporte.Location = new System.Drawing.Point(144, 24);
            this.cbFormaSuporte.Name = "cbFormaSuporte";
            this.cbFormaSuporte.Size = new System.Drawing.Size(48, 21);
            this.cbFormaSuporte.TabIndex = 1;
            // 
            // lstFormaSuporte
            // 
            this.lstFormaSuporte.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstFormaSuporte.IntegralHeight = false;
            this.lstFormaSuporte.Location = new System.Drawing.Point(8, 24);
            this.lstFormaSuporte.Name = "lstFormaSuporte";
            this.lstFormaSuporte.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstFormaSuporte.Size = new System.Drawing.Size(128, 80);
            this.lstFormaSuporte.TabIndex = 0;
            // 
            // txtCota
            // 
            this.txtCota.Location = new System.Drawing.Point(126, 318);
            this.txtCota.Name = "txtCota";
            this.txtCota.Size = new System.Drawing.Size(312, 20);
            this.txtCota.TabIndex = 16;
            this.txtCota.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterPanelPesquisa_KeyDown);
            // 
            // lblCota
            // 
            this.lblCota.Location = new System.Drawing.Point(3, 315);
            this.lblCota.Name = "lblCota";
            this.lblCota.Size = new System.Drawing.Size(128, 24);
            this.lblCota.TabIndex = 10;
            this.lblCota.Text = "Cota da unidade física:";
            this.lblCota.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtConteudoInformacional
            // 
            this.txtConteudoInformacional.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConteudoInformacional.Location = new System.Drawing.Point(126, 270);
            this.txtConteudoInformacional.Name = "txtConteudoInformacional";
            this.txtConteudoInformacional.Size = new System.Drawing.Size(662, 20);
            this.txtConteudoInformacional.TabIndex = 14;
            this.txtConteudoInformacional.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterPanelPesquisa_KeyDown);
            // 
            // lblConteudoInformacional
            // 
            this.lblConteudoInformacional.Location = new System.Drawing.Point(3, 267);
            this.lblConteudoInformacional.Name = "lblConteudoInformacional";
            this.lblConteudoInformacional.Size = new System.Drawing.Size(128, 24);
            this.lblConteudoInformacional.TabIndex = 20;
            this.lblConteudoInformacional.Text = "Conteúdo informacional:";
            this.lblConteudoInformacional.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ButtonTI
            // 
            this.ButtonTI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonTI.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ButtonTI.ImageIndex = 1;
            this.ButtonTI.Location = new System.Drawing.Point(794, 222);
            this.ButtonTI.Name = "ButtonTI";
            this.ButtonTI.Size = new System.Drawing.Size(24, 20);
            this.ButtonTI.TabIndex = 11;
            // 
            // ButtonConteudos
            // 
            this.ButtonConteudos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonConteudos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ButtonConteudos.ImageIndex = 1;
            this.ButtonConteudos.Location = new System.Drawing.Point(794, 246);
            this.ButtonConteudos.Name = "ButtonConteudos";
            this.ButtonConteudos.Size = new System.Drawing.Size(24, 20);
            this.ButtonConteudos.TabIndex = 13;
            // 
            // ButtonEP
            // 
            this.ButtonEP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonEP.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ButtonEP.ImageIndex = 1;
            this.ButtonEP.Location = new System.Drawing.Point(498, 112);
            this.ButtonEP.Name = "ButtonEP";
            this.ButtonEP.Size = new System.Drawing.Size(24, 20);
            this.ButtonEP.TabIndex = 7;
            // 
            // txtTipologiaInformacional
            // 
            this.txtTipologiaInformacional.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTipologiaInformacional.Location = new System.Drawing.Point(126, 222);
            this.txtTipologiaInformacional.Name = "txtTipologiaInformacional";
            this.txtTipologiaInformacional.Size = new System.Drawing.Size(662, 20);
            this.txtTipologiaInformacional.TabIndex = 10;
            this.txtTipologiaInformacional.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterPanelPesquisa_KeyDown);
            // 
            // lblTipologiaInformacional
            // 
            this.lblTipologiaInformacional.Location = new System.Drawing.Point(3, 218);
            this.lblTipologiaInformacional.Name = "lblTipologiaInformacional";
            this.lblTipologiaInformacional.Size = new System.Drawing.Size(128, 24);
            this.lblTipologiaInformacional.TabIndex = 16;
            this.lblTipologiaInformacional.Text = "Tipologia informacional:";
            this.lblTipologiaInformacional.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIndexacao
            // 
            this.txtIndexacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIndexacao.Location = new System.Drawing.Point(126, 246);
            this.txtIndexacao.Name = "txtIndexacao";
            this.txtIndexacao.Size = new System.Drawing.Size(662, 20);
            this.txtIndexacao.TabIndex = 12;
            this.txtIndexacao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterPanelPesquisa_KeyDown);
            // 
            // txtEntidadeProdutora
            // 
            this.txtEntidadeProdutora.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntidadeProdutora.Location = new System.Drawing.Point(128, 113);
            this.txtEntidadeProdutora.Name = "txtEntidadeProdutora";
            this.txtEntidadeProdutora.Size = new System.Drawing.Size(364, 20);
            this.txtEntidadeProdutora.TabIndex = 6;
            this.txtEntidadeProdutora.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterPanelPesquisa_KeyDown);
            // 
            // lblIndexacao
            // 
            this.lblIndexacao.Location = new System.Drawing.Point(3, 244);
            this.lblIndexacao.Name = "lblIndexacao";
            this.lblIndexacao.Size = new System.Drawing.Size(128, 24);
            this.lblIndexacao.TabIndex = 6;
            this.lblIndexacao.Text = "Termos de indexação:";
            this.lblIndexacao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEntidadeProdutora
            // 
            this.lblEntidadeProdutora.Location = new System.Drawing.Point(3, 110);
            this.lblEntidadeProdutora.Name = "lblEntidadeProdutora";
            this.lblEntidadeProdutora.Size = new System.Drawing.Size(128, 24);
            this.lblEntidadeProdutora.TabIndex = 2;
            this.lblEntidadeProdutora.Text = "Entidade produtora:";
            this.lblEntidadeProdutora.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TabPage3
            // 
            this.TabPage3.AutoScroll = true;
            this.TabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.TabPage3.Controls.Add(this.chkEstruturaArquivistica);
            this.TabPage3.Controls.Add(this.cnList);
            this.TabPage3.Location = new System.Drawing.Point(4, 22);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(844, 563);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Estrutura";
            // 
            // chkEstruturaArquivistica
            // 
            this.chkEstruturaArquivistica.Location = new System.Drawing.Point(16, 0);
            this.chkEstruturaArquivistica.Name = "chkEstruturaArquivistica";
            this.chkEstruturaArquivistica.Size = new System.Drawing.Size(72, 16);
            this.chkEstruturaArquivistica.TabIndex = 3;
            this.chkEstruturaArquivistica.Text = "Estrutura";
            // 
            // cnList
            // 
            this.cnList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cnList.Location = new System.Drawing.Point(-1, 0);
            this.cnList.Name = "cnList";
            this.cnList.Size = new System.Drawing.Size(794, 563);
            this.cnList.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.pesqContInfLicencaObras1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(844, 563);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Licenças de obras";
            // 
            // pesqContInfLicencaObras1
            // 
            this.pesqContInfLicencaObras1.Location = new System.Drawing.Point(6, 6);
            this.pesqContInfLicencaObras1.Name = "pesqContInfLicencaObras1";
            this.pesqContInfLicencaObras1.Owner = null;
            this.pesqContInfLicencaObras1.Size = new System.Drawing.Size(821, 323);
            this.pesqContInfLicencaObras1.TabIndex = 0;
            this.pesqContInfLicencaObras1.TheControloAutSelectionRetriever = null;
            // 
            // ToolBarButtonExecutar
            // 
            this.ToolBarButtonExecutar.Name = "ToolBarButtonExecutar";
            // 
            // ToolBarButtonAjuda
            // 
            this.ToolBarButtonAjuda.Name = "ToolBarButtonAjuda";
            // 
            // ToolBarButtonLimpar
            // 
            this.ToolBarButtonLimpar.Name = "ToolBarButtonLimpar";
            // 
            // ToolBarButtonTipoPesquisa
            // 
            this.ToolBarButtonTipoPesquisa.Name = "ToolBarButtonTipoPesquisa";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtPesquisaSimples);
            this.panel1.Location = new System.Drawing.Point(0, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(852, 589);
            this.panel1.TabIndex = 54;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 24);
            this.label2.TabIndex = 52;
            this.label2.Text = "Pesquisa simples:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPesquisaSimples
            // 
            this.txtPesquisaSimples.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPesquisaSimples.Location = new System.Drawing.Point(109, 12);
            this.txtPesquisaSimples.Name = "txtPesquisaSimples";
            this.txtPesquisaSimples.Size = new System.Drawing.Size(731, 20);
            this.txtPesquisaSimples.TabIndex = 1;
            this.txtPesquisaSimples.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterPanelPesquisa_KeyDown);
            // 
            // ToolBarButtonSep1
            // 
            this.ToolBarButtonSep1.Name = "ToolBarButtonSep1";
            this.ToolBarButtonSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // lblODs
            // 
            this.lblODs.AutoSize = true;
            this.lblODs.Location = new System.Drawing.Point(3, 373);
            this.lblODs.Name = "lblODs";
            this.lblODs.Size = new System.Drawing.Size(81, 13);
            this.lblODs.TabIndex = 64;
            this.lblODs.Text = "Objetos digitais:";
            // 
            // cbODs
            // 
            this.cbODs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbODs.FormattingEnabled = true;
            this.cbODs.Items.AddRange(new object[] {
            "<Escolher opção>",
            "Com objetos digitais",
            "Com objetos digitais publicados",
            "Com objetos digitais não publicados"});
            this.cbODs.Location = new System.Drawing.Point(126, 370);
            this.cbODs.Name = "cbODs";
            this.cbODs.Size = new System.Drawing.Size(312, 21);
            this.cbODs.TabIndex = 18;
            // 
            // MasterPanelPesquisa
            // 
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "MasterPanelPesquisa";
            this.Size = new System.Drawing.Size(852, 641);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.lblFuncao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.Controls.SetChildIndex(this.TabControl1, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.TabControl1.ResumeLayout(false);
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            this.grpNiveisDescricao.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.grpMaterialSuporte.ResumeLayout(false);
            this.grpEstadoConservacao.ResumeLayout(false);
            this.grpTecnicaRegisto.ResumeLayout(false);
            this.grpFormaSuporte.ResumeLayout(false);
            this.TabPage3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

	#endregion

		private void AddHandlers()
		{
			cnList.trVwLocalizacao.AfterSelect += cnList_AfterSelect;
            this.pesqContInfLicencaObras1.ExecuteQuery += new PesqContInfLicencaObras.ExecuteQueryEventHandler(pesqContInfLicencaObras1_ExecuteQuery);
		}

        void pesqContInfLicencaObras1_ExecuteQuery() {
            if (ExecuteQuery != null)
                ExecuteQuery(this);
        }

		private void GetExtraResources()
		{
			ToolBar.ImageList = SharedResourcesOld.CurrentSharedResources.PesquisarImageList;
			ToolBarButtonExecutar.ImageIndex = 0;
			ToolBarButtonExecutar.ToolTipText = SharedResourcesOld.CurrentSharedResources.ExecutarPesquisaString;
			ToolBarButtonLimpar.ImageIndex = 1;
			ToolBarButtonLimpar.ToolTipText = SharedResourcesOld.CurrentSharedResources.LimparPesquisaString;
			ToolBarButtonAjuda.ImageIndex = 2;
			ToolBarButtonAjuda.ToolTipText = SharedResourcesOld.CurrentSharedResources.AjudaString;
            ToolBarButtonTipoPesquisa.ImageIndex = 3;
            ToolBarButtonTipoPesquisa.ToolTipText = SharedResourcesOld.CurrentSharedResources.PesquisaAvancadaString;

			ButtonEP.Image = SharedResourcesOld.CurrentSharedResources.ChamarPicker;
            buttonAutor.Image = SharedResourcesOld.CurrentSharedResources.ChamarPicker;
			ButtonConteudos.Image = SharedResourcesOld.CurrentSharedResources.ChamarPicker;
			ButtonTI.Image = SharedResourcesOld.CurrentSharedResources.ChamarPicker;

			CurrentToolTip.SetToolTip(ButtonEP, SharedResourcesOld.CurrentSharedResources.ChamarPickerString);
            CurrentToolTip.SetToolTip(buttonAutor, SharedResourcesOld.CurrentSharedResources.ChamarPickerString);
			CurrentToolTip.SetToolTip(ButtonConteudos, SharedResourcesOld.CurrentSharedResources.ChamarPickerString);
			CurrentToolTip.SetToolTip(ButtonTI, SharedResourcesOld.CurrentSharedResources.ChamarPickerString);
		}

        public enum TipoPesquisa
        {
            simples = 0,
            avancada = 1
        }

        private TipoPesquisa mTipoPesquisaSeleccionada = TipoPesquisa.avancada;
        public TipoPesquisa TipoPesquisaSeleccionada
        {
            get { return mTipoPesquisaSeleccionada; }            
        }

		private void LoadContents()
		{
            lstNiveisDocumentais.DataSource = new DataView(GisaDataSetHelper.GetInstance().TipoNivelRelacionado, "ID > 6 AND ID < 11", "ID", DataViewRowState.CurrentRows);
            lstNiveisDocumentais.DisplayMember = "Designacao";

			lstTecnicaRegisto.DataSource = new DataView(GisaDataSetHelper.GetInstance().TipoTecnicasDeRegisto, "", "Designacao", DataViewRowState.CurrentRows);
			lstTecnicaRegisto.DisplayMember = "Designacao";

			lstFormaSuporte.DataSource = new DataView(GisaDataSetHelper.GetInstance().TipoFormaSuporteAcond, "", "Designacao", DataViewRowState.CurrentRows);
			lstFormaSuporte.DisplayMember = "Designacao";

			lstMaterialSuporte.DataSource = new DataView(GisaDataSetHelper.GetInstance().TipoMaterialDeSuporte, "", "Designacao", DataViewRowState.CurrentRows);
			lstMaterialSuporte.DisplayMember = "Designacao";

			lstEstadoConservacao.DataSource = new DataView(GisaDataSetHelper.GetInstance().TipoEstadoDeConservacao, "", "Designacao", DataViewRowState.CurrentRows);
			lstEstadoConservacao.DisplayMember = "Designacao";
			
			cbFormaSuporte.SelectedItem = cbFormaSuporte.Items[0];			
			cbMaterialSuporte.SelectedItem = cbMaterialSuporte.Items[0];
			cbTecnicaRegisto.SelectedItem = cbTecnicaRegisto.Items[0];			
		}

        public delegate void ExecuteQueryEventHandler(MasterPanelPesquisa MasterPanel);
        public event ExecuteQueryEventHandler ExecuteQuery;
		public delegate void ShowSelectionEventHandler(MasterPanelPesquisa MasterPanel);
		public event ShowSelectionEventHandler ShowSelection;
		public delegate void ClearSearchResultsEventHandler(MasterPanelPesquisa MasterPanel);
		public event ClearSearchResultsEventHandler ClearSearchResults;
		private void ToolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
            if (e.Button == ToolBarButtonExecutar)
            {
                if (ExecuteQuery != null)
                    ExecuteQuery(this);
            }
            else if (e.Button == ToolBarButtonLimpar)
            {
                clearFields();
                if (ClearSearchResults != null)
                    ClearSearchResults(this);
            }
            else if (e.Button == ToolBarButtonAjuda)
            {
                FormTextDisplayer form = new FormTextDisplayer();
                System.Reflection.Assembly gisaAssembly = System.Reflection.Assembly.GetAssembly(typeof(MasterPanelPesquisa));
                System.IO.Stream helpFileStream = null;
                helpFileStream = gisaAssembly.GetManifestResourceStream(gisaAssembly.GetName().Name + ".DocumentosAjuda.AjudaPesquisaDocumentos.rtf");
                if (helpFileStream != null)
                {
                    form.rtbText.LoadFile(helpFileStream, RichTextBoxStreamType.RichText);
                    helpFileStream.Close();
                }
                form.ShowDialog();
            }
            else if (e.Button == ToolBarButtonTipoPesquisa)
                SwitchTipoPesquisa();
		}

        public string get_Nome_LicencaObraRequerentes() {
            return this.pesqContInfLicencaObras1.txtRequerente.Text.Trim();
        }

        public string get_LocalizacaoObra_Actual() {
            return this.pesqContInfLicencaObras1.txtLocalizacao.Text.Trim();
        }
        public string get_NumPolicia_Actual() {
            return this.pesqContInfLicencaObras1.txtNumPolicia.Text.Trim();
        }

        public string get_LocalizacaoObra_Antiga() {
            return this.pesqContInfLicencaObras1.txtBoxLocalizacaoAntiga.Text.Trim();
        }
        public string get_NumPolicia_Antigo() {
            return this.pesqContInfLicencaObras1.txtBoxNumPoliciaAntigo.Text.Trim();
        }

        public string get_TipoObra() {
            return this.pesqContInfLicencaObras1.txtTipoDeObra.Text.Trim();
        }

        public string get_TecnicoObra() {
            return this.pesqContInfLicencaObras1.txtTecnicoDeObra.Text.Trim();
        }

        public string get_CodigosAtestadoHabitabilidade() {
            return this.pesqContInfLicencaObras1.txtAtestadoHab.Text.Trim();
        }

        public string get_Datas_LicencaObraDataLicencaConstrucao_Inicio() {
            return this.pesqContInfLicencaObras1.get_Date_Inicio();
        }

        public string get_Datas_LicencaObraDataLicencaConstrucao_Fim() {
            return this.pesqContInfLicencaObras1.get_Date_Fim();
        }

        public bool get_PH_checked() {
            return this.pesqContInfLicencaObras1.chkPH.Checked;
        }

        private void SwitchTipoPesquisa()
        {
            clearFields();
            if (mTipoPesquisaSeleccionada == TipoPesquisa.simples)
            {
                mTipoPesquisaSeleccionada = TipoPesquisa.avancada;
                TabControl1.BringToFront();
                ToolBarButtonTipoPesquisa.ImageIndex = 3;
                ToolBarButtonTipoPesquisa.ToolTipText = SharedResourcesOld.CurrentSharedResources.PesquisaSimplesString;
            }
            else
            {
                mTipoPesquisaSeleccionada = TipoPesquisa.simples;
                panel1.BringToFront();
                ToolBarButtonTipoPesquisa.ImageIndex = 4;
                ToolBarButtonTipoPesquisa.ToolTipText = SharedResourcesOld.CurrentSharedResources.PesquisaAvancadaString;
            }
        }

		private void clearFields()
		{
            if (cbModulo.Items.Count > 0)
			    cbModulo.SelectedIndex = 0;

			txtDesignacao.Clear();
            txtAutor.Clear();
			txtEntidadeProdutora.Clear();			
			txtIndexacao.Clear();			
			txtCota.Clear();
            txtAgrupador.Clear();
			txtTipologiaInformacional.Clear();			
			txtConteudoInformacional.Clear();
			txtNotas.Clear();
			chkApenasDataElimExp.Checked = false;
            cbODs.SelectedIndex = 0;
            txtCodigoParcial.Clear();

            for (int i = 0; i < lstNiveisDocumentais.Items.Count; i++)
                lstNiveisDocumentais.SelectedItems.Add(lstNiveisDocumentais.Items[i]);

			cdbDataInicio.Checked = false;
            cdbDataInicio.UpdateDate();
			cdbDataFim.Checked = false;
            cdbDataFim.UpdateDate();
            cdbInicioDoFim.Checked = false;
            cdbInicioDoFim.UpdateDate();
            cdbFimDoFim.Checked = false;
            cdbFimDoFim.UpdateDate();
			chkFormaSuporte.Checked = false;
            chkFormaSuporte.Enabled = true;
			cbFormaSuporte.SelectedIndex = 0;
			chkEstadoConservacao.Checked = false;
            chkEstadoConservacao.Enabled = true;
			chkMaterialSuporte.Checked = false;
            chkMaterialSuporte.Enabled = true;
			cbMaterialSuporte.SelectedIndex = 0;
			chkTecnicaRegisto.Checked = false;
            chkTecnicaRegisto.Enabled = true;
			cbTecnicaRegisto.SelectedIndex = 0;
			chkEstruturaArquivistica.Checked = false;
            chkEstruturaArquivistica.Enabled = true;
			lstTecnicaRegisto.ClearSelected();
			lstTecnicaRegisto.SelectedItem = lstTecnicaRegisto.Items[0];
			lstEstadoConservacao.ClearSelected();
			lstEstadoConservacao.SelectedItem = lstEstadoConservacao.Items[0];
			lstMaterialSuporte.ClearSelected();
			lstMaterialSuporte.SelectedItem = lstMaterialSuporte.Items[0];
			lstFormaSuporte.ClearSelected();
			lstFormaSuporte.SelectedItem = lstFormaSuporte.Items[0];
            txtPesquisaSimples.Clear();
            txtID.Clear();
            pesqContInfLicencaObras1.clearFields();
		}

		private void cnList_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if (cnList.SelectedRelacaoHierarquica != null)
			{
				if (ShowSelection != null)
					ShowSelection(this);
			}
		}

        private void ButtonAutor_Click(object sender, EventArgs e)
        {
            FormPickControloAut frmPick = new FormPickControloAut();
            frmPick.Text = "Controlo de autoridade - Pesquisa por entidade produtora";
            frmPick.caList.AllowedNoticiaAut(TipoNoticiaAut.EntidadeProdutora);
            frmPick.caList.txtFiltroDesignacao.Clear();
            frmPick.caList.ReloadList();
            retrieveSelection(frmPick, txtAutor);
        }

		private void ButtonEP_Click(object sender, EventArgs e)
		{
			FormPickControloAut frmPick = new FormPickControloAut();
			frmPick.Text = "Controlo de autoridade - Pesquisa por entidade produtora";
			frmPick.caList.AllowedNoticiaAut(TipoNoticiaAut.EntidadeProdutora);
            frmPick.caList.txtFiltroDesignacao.Clear();
            frmPick.caList.ReloadList();
			retrieveSelection(frmPick, txtEntidadeProdutora);
		}

		private void ButtonConteudos_Click(object sender, EventArgs e)
		{
            FormThesaurusNavigator frmPick = new FormThesaurusNavigator();
			frmPick.Text = "Controlo de autoridade - Pesquisa por conteúdos";
            frmPick.AllowedNoticiaAut = new TipoNoticiaAut[] { TipoNoticiaAut.Ideografico, TipoNoticiaAut.Onomastico, TipoNoticiaAut.ToponimicoGeografico };
			retrieveSelection(frmPick, txtIndexacao);
		}

		private void ButtonTI_Click(object sender, EventArgs e)
		{
            FormThesaurusNavigator frmPick = new FormThesaurusNavigator();
			frmPick.Text = "Controlo de autoridade - Pesquisa por tipologia informacional";
            frmPick.AllowedNoticiaAut = new TipoNoticiaAut[] { TipoNoticiaAut.TipologiaInformacional };
			retrieveSelection(frmPick, txtTipologiaInformacional);
		}

        private void retrieveSelection(FormThesaurusNavigator frmPick, TextBox txtBox)
        {
            switch (frmPick.ShowDialog())
            {
                case DialogResult.OK:
                    foreach (var cadRow in frmPick.SelectTermos)
                        AddTermoToSearchField(txtBox, cadRow);
                    break;
                case DialogResult.Cancel:
                    break;
            }
        }

        private static void AddTermoToSearchField(TextBox txtBox, GISADataset.ControloAutDicionarioRow cadRow)
        {
            // if more termos already exist, use a space to separate
            if (!(txtBox.Text.Equals("")))
                txtBox.Text += " ";

            string composedTermo = cadRow.DicionarioRow.Termo.Replace("\"", "%");
            if (composedTermo.IndexOf(" ") != -1)
                txtBox.Text += "\"" + composedTermo + "\"";
            else
                txtBox.Text += composedTermo;
        }

		private void retrieveSelection(FormPickControloAut frmPick, TextBox txtBox)
		{
			switch (frmPick.ShowDialog())
			{
				case DialogResult.OK:
					foreach (ListViewItem li in frmPick.caList.SelectedItems)
					{
						Debug.Assert(li.Tag is GISADataset.ControloAutDicionarioRow);

						var ca = ((GISADataset.ControloAutDicionarioRow)li.Tag).ControloAutRow;
						var cadRows = GisaDataSetHelper.GetInstance().ControloAutDicionario.Select("IDControloAut=" + ca.ID.ToString() + " AND IDTipoControloAutForma=" + System.Enum.Format(typeof(TipoControloAutForma), TipoControloAutForma.FormaAutorizada, "D"));
						if (cadRows.Length > 0)
                            AddTermoToSearchField(txtBox, (GISADataset.ControloAutDicionarioRow)cadRows[0]);
					}
					break;
				case DialogResult.Cancel:
				break;
			}
		}

        private void enterKeyPressed( KeyEventArgs e)
        {
            if (e.KeyValue == Convert.ToInt32(Keys.Enter))
            {
                if (ExecuteQuery != null)
                    ExecuteQuery(this);
            }
        }


		private void chkEstruturaArquivistica_CheckedChanged(object sender, System.EventArgs e)
		{
			cnList.Enabled = chkEstruturaArquivistica.Checked;
		}

		private void chkFormaSuporte_CheckedChanged(object sender, System.EventArgs e)
		{
			grpFormaSuporte.Enabled = chkFormaSuporte.Checked;
		}

		private void chkMaterialSuporte_CheckedChanged(object sender, System.EventArgs e)
		{
			grpMaterialSuporte.Enabled = chkMaterialSuporte.Checked;
		}

		private void chkTecnicaRegisto_CheckedChanged(object sender, System.EventArgs e)
		{
			grpTecnicaRegisto.Enabled = chkTecnicaRegisto.Checked;
		}

		private void chkEstadoConservacao_CheckedChanged(object sender, System.EventArgs e)
		{
			grpEstadoConservacao.Enabled = chkEstadoConservacao.Checked;
		}

		private void MasterPanelPesquisa_ParentChanged(object sender, System.EventArgs e)
		{

		}

        private void MasterPanelPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            enterKeyPressed(e);
        }

        private GISADataset.RelacaoHierarquicaDataTable rhTable = GisaDataSetHelper.GetInstance().RelacaoHierarquica;
        private void rhTable_RelacaoHierarquicaRowChangingRelacaoHierarquicaRowDeleting(object sender, GISADataset.RelacaoHierarquicaRowChangeEvent e)
        {
            NavigatorHelper.ForceRefresh(e, this, (frmMain)TopLevelControl);
        }

        public ControloNivelList ControloNivelList
        {
            get { return this.cnList; }
        }
    }
}