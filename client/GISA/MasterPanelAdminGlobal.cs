using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Fedora.FedoraHandler;
using GISA.Model;
using GISA.SharedResources;

namespace GISA
{
	public class MasterPanelAdminGlobal : GISA.SinglePanel
	{

	#region  Windows Form Designer generated code 

		public MasterPanelAdminGlobal() : base()
		{
			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            txtMaxNumResultados.Validating += txtMaxNumResultados_Validating;
            txtMaxNumDocumentos.Validating += txtMaxNumResultados_Validating;
            base.ParentChanged += MasterPanelAdminGlobal_ParentChanged;
            chkActivar.CheckedChanged += chkActivar_CheckedChanged;
            btnAddLista.Click += btnAddLista_Click;
            btnEditLista.Click += btnEditLista_Click;
            btnRemoveLista.Click += btnRemoveLista_Click;
            LstVwListaModelos.SelectedIndexChanged += LstVwListaModelos_SelectedIndexChanged;
            btnAddModelo.Click += btnAddModelo_Click;
            btnEditModelo.Click += btnEditModelo_Click;
            btnRemoveModelo.Click += btnRemoveModelo_Click;
            LstVwModelos.SelectedIndexChanged += LstVwModelos_SelectedIndexChanged;

            VisibleConfig();
			GetExtraResources();
		}

        private void VisibleConfig()
        {
            this.grpConfig_Dep.Visible = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsDepEnable();
            this.grpBackup.Visible = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().isMonoposto();
            this.grpFedora.Visible = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsObjDigEnable();
            this.grpIntegração.Visible = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsIntegridadeEnable();
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
		internal System.Windows.Forms.GroupBox grpFRDs;
		internal System.Windows.Forms.GroupBox grpGeral;
		internal System.Windows.Forms.Label lblMaxNumResultados;
		internal System.Windows.Forms.TextBox txtMaxNumResultados;
		internal System.Windows.Forms.GroupBox grpAvaliacao;
		internal System.Windows.Forms.RadioButton rbNiveisEPs;
		internal System.Windows.Forms.RadioButton rbNiveisTFs;
		internal System.Windows.Forms.RadioButton rbGestaoIntegrada;
		internal System.Windows.Forms.RadioButton rbGestaoNaoIntegrada;
		internal System.Windows.Forms.GroupBox grpImagensWeb;
		internal System.Windows.Forms.TextBox txtURLBase;
		internal System.Windows.Forms.Label lblUrlBase;
		internal System.Windows.Forms.CheckBox chkActivar;
		internal System.Windows.Forms.RichTextBox rtbNotaPaginaWebImagens;
		internal System.Windows.Forms.GroupBox grpGestaoModelos;
		internal System.Windows.Forms.GroupBox grpListaModelos;
		internal System.Windows.Forms.ListView LstVwListaModelos;
		internal System.Windows.Forms.GroupBox grpModelos;
		internal System.Windows.Forms.ListView LstVwModelos;
		internal System.Windows.Forms.Button btnRemoveLista;
		internal System.Windows.Forms.Button btnEditLista;
		internal System.Windows.Forms.Button btnAddLista;
		internal System.Windows.Forms.Button btnEditModelo;
		internal System.Windows.Forms.Button btnAddModelo;
		internal System.Windows.Forms.ColumnHeader colDesignacao;
		internal System.Windows.Forms.ColumnHeader colDataInicio;
		internal System.Windows.Forms.ColumnHeader colDesignacaoModelo;
		internal System.Windows.Forms.ColumnHeader colPrazo;
		internal System.Windows.Forms.ColumnHeader colDestino;
        private GroupBox grpBackup;
        private Button btnBackup;
        internal GroupBox grpConfig_Dep;
        internal Label lbl_gestaoDep;
        private GISA.Controls.PxDecimalBox txt_metrosLinearesTotais;
        private GroupBox grpFedora;
        private TextBox txtServerUrl;
        private Label lblServerUrl;
        private TextBox txtPassword;
        private Label label2;
        private TextBox txtUsername;
        private Label lblUsername;
        private TableLayoutPanel tblLayout;
        private FlowLayoutPanel flwPanel;
        private GroupBox grpGisaInternet;
        private Label lblGisaInternet;
        private TextBox txtGisaInternet;
        private Label lblQualidadeImagem;
        private ComboBox cbQualidadeImagem;
        private GroupBox grpValoresOmissão;
        internal GroupBox grpIdioma;
        internal GroupBox grpAlfabeto;
        internal Button btnRemoveAlfabeto;
        internal Button btnAddAlfabeto;
        internal Controls.PxListView lstVwAlfabeto;
        internal ColumnHeader colAlfabeto;
        internal GroupBox grpLingua;
        internal Button btnRemoveLingua;
        internal Button btnAddLingua;
        internal Controls.PxListView lstVwLanguage;
        internal ColumnHeader colLingua;
        internal GroupBox grpCondicoesReproducao;
        internal TextBox txtCondicoesReproducao;
        internal GroupBox grpCondicoesAcesso;
        internal TextBox txtCondicoesAcesso;
        private CheckBox chkValoresOmissao;
        internal GroupBox grpIntegração;
        internal TextBox txtMaxNumDocumentos;
        internal Label label1;
		internal System.Windows.Forms.Button btnRemoveModelo;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.grpFRDs = new System.Windows.Forms.GroupBox();
            this.rbNiveisTFs = new System.Windows.Forms.RadioButton();
            this.rbNiveisEPs = new System.Windows.Forms.RadioButton();
            this.grpGeral = new System.Windows.Forms.GroupBox();
            this.txtMaxNumResultados = new System.Windows.Forms.TextBox();
            this.lblMaxNumResultados = new System.Windows.Forms.Label();
            this.grpAvaliacao = new System.Windows.Forms.GroupBox();
            this.rbGestaoNaoIntegrada = new System.Windows.Forms.RadioButton();
            this.rbGestaoIntegrada = new System.Windows.Forms.RadioButton();
            this.tblLayout = new System.Windows.Forms.TableLayoutPanel();
            this.grpImagensWeb = new System.Windows.Forms.GroupBox();
            this.txtURLBase = new System.Windows.Forms.TextBox();
            this.chkActivar = new System.Windows.Forms.CheckBox();
            this.lblUrlBase = new System.Windows.Forms.Label();
            this.rtbNotaPaginaWebImagens = new System.Windows.Forms.RichTextBox();
            this.grpGestaoModelos = new System.Windows.Forms.GroupBox();
            this.grpListaModelos = new System.Windows.Forms.GroupBox();
            this.btnRemoveLista = new System.Windows.Forms.Button();
            this.btnEditLista = new System.Windows.Forms.Button();
            this.btnAddLista = new System.Windows.Forms.Button();
            this.LstVwListaModelos = new System.Windows.Forms.ListView();
            this.colDesignacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDataInicio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpModelos = new System.Windows.Forms.GroupBox();
            this.btnRemoveModelo = new System.Windows.Forms.Button();
            this.btnEditModelo = new System.Windows.Forms.Button();
            this.btnAddModelo = new System.Windows.Forms.Button();
            this.LstVwModelos = new System.Windows.Forms.ListView();
            this.colDesignacaoModelo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPrazo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDestino = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpBackup = new System.Windows.Forms.GroupBox();
            this.btnBackup = new System.Windows.Forms.Button();
            this.grpConfig_Dep = new System.Windows.Forms.GroupBox();
            this.txt_metrosLinearesTotais = new GISA.Controls.PxDecimalBox();
            this.lbl_gestaoDep = new System.Windows.Forms.Label();
            this.grpFedora = new System.Windows.Forms.GroupBox();
            this.lblQualidadeImagem = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbQualidadeImagem = new System.Windows.Forms.ComboBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtServerUrl = new System.Windows.Forms.TextBox();
            this.lblServerUrl = new System.Windows.Forms.Label();
            this.flwPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.grpIntegração = new System.Windows.Forms.GroupBox();
            this.txtMaxNumDocumentos = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpGisaInternet = new System.Windows.Forms.GroupBox();
            this.txtGisaInternet = new System.Windows.Forms.TextBox();
            this.lblGisaInternet = new System.Windows.Forms.Label();
            this.grpValoresOmissão = new System.Windows.Forms.GroupBox();
            this.grpIdioma = new System.Windows.Forms.GroupBox();
            this.grpAlfabeto = new System.Windows.Forms.GroupBox();
            this.btnRemoveAlfabeto = new System.Windows.Forms.Button();
            this.btnAddAlfabeto = new System.Windows.Forms.Button();
            this.lstVwAlfabeto = new GISA.Controls.PxListView();
            this.colAlfabeto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpLingua = new System.Windows.Forms.GroupBox();
            this.btnRemoveLingua = new System.Windows.Forms.Button();
            this.btnAddLingua = new System.Windows.Forms.Button();
            this.lstVwLanguage = new GISA.Controls.PxListView();
            this.colLingua = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpCondicoesReproducao = new System.Windows.Forms.GroupBox();
            this.txtCondicoesReproducao = new System.Windows.Forms.TextBox();
            this.grpCondicoesAcesso = new System.Windows.Forms.GroupBox();
            this.txtCondicoesAcesso = new System.Windows.Forms.TextBox();
            this.chkValoresOmissao = new System.Windows.Forms.CheckBox();
            this.pnlToolbarPadding.SuspendLayout();
            this.grpFRDs.SuspendLayout();
            this.grpGeral.SuspendLayout();
            this.grpAvaliacao.SuspendLayout();
            this.grpImagensWeb.SuspendLayout();
            this.grpGestaoModelos.SuspendLayout();
            this.grpListaModelos.SuspendLayout();
            this.grpModelos.SuspendLayout();
            this.grpBackup.SuspendLayout();
            this.grpConfig_Dep.SuspendLayout();
            this.grpFedora.SuspendLayout();
            this.flwPanel.SuspendLayout();
            this.grpIntegração.SuspendLayout();
            this.grpGisaInternet.SuspendLayout();
            this.grpValoresOmissão.SuspendLayout();
            this.grpIdioma.SuspendLayout();
            this.grpAlfabeto.SuspendLayout();
            this.grpLingua.SuspendLayout();
            this.grpCondicoesReproducao.SuspendLayout();
            this.grpCondicoesAcesso.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFuncao
            // 
            this.lblFuncao.Location = new System.Drawing.Point(0, 0);
            this.lblFuncao.Size = new System.Drawing.Size(1011, 24);
            this.lblFuncao.Text = "Configuração global";
            // 
            // ToolBar
            // 
            this.ToolBar.Size = new System.Drawing.Size(7416, 26);
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
            this.pnlToolbarPadding.Size = new System.Drawing.Size(1011, 28);
            // 
            // grpFRDs
            // 
            this.grpFRDs.Controls.Add(this.rbNiveisTFs);
            this.grpFRDs.Controls.Add(this.rbNiveisEPs);
            this.grpFRDs.Location = new System.Drawing.Point(3, 460);
            this.grpFRDs.Name = "grpFRDs";
            this.grpFRDs.Size = new System.Drawing.Size(296, 72);
            this.grpFRDs.TabIndex = 3;
            this.grpFRDs.TabStop = false;
            this.grpFRDs.Text = "Estrutura arquivística";
            this.grpFRDs.Visible = false;
            // 
            // rbNiveisTFs
            // 
            this.rbNiveisTFs.Location = new System.Drawing.Point(16, 40);
            this.rbNiveisTFs.Name = "rbNiveisTFs";
            this.rbNiveisTFs.Size = new System.Drawing.Size(264, 24);
            this.rbNiveisTFs.TabIndex = 9;
            this.rbNiveisTFs.Text = "Níveis temático-funcionais";
            // 
            // rbNiveisEPs
            // 
            this.rbNiveisEPs.Location = new System.Drawing.Point(16, 16);
            this.rbNiveisEPs.Name = "rbNiveisEPs";
            this.rbNiveisEPs.Size = new System.Drawing.Size(264, 24);
            this.rbNiveisEPs.TabIndex = 8;
            this.rbNiveisEPs.Text = "Níveis orgânicos, baseados em entidades prod.";
            // 
            // grpGeral
            // 
            this.grpGeral.Controls.Add(this.txtMaxNumResultados);
            this.grpGeral.Controls.Add(this.lblMaxNumResultados);
            this.grpGeral.Location = new System.Drawing.Point(3, 3);
            this.grpGeral.Name = "grpGeral";
            this.grpGeral.Size = new System.Drawing.Size(296, 57);
            this.grpGeral.TabIndex = 2;
            this.grpGeral.TabStop = false;
            this.grpGeral.Text = "Geral";
            // 
            // txtMaxNumResultados
            // 
            this.txtMaxNumResultados.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxNumResultados.Location = new System.Drawing.Point(200, 22);
            this.txtMaxNumResultados.Name = "txtMaxNumResultados";
            this.txtMaxNumResultados.Size = new System.Drawing.Size(80, 20);
            this.txtMaxNumResultados.TabIndex = 7;
            // 
            // lblMaxNumResultados
            // 
            this.lblMaxNumResultados.Location = new System.Drawing.Point(16, 24);
            this.lblMaxNumResultados.Name = "lblMaxNumResultados";
            this.lblMaxNumResultados.Size = new System.Drawing.Size(176, 16);
            this.lblMaxNumResultados.TabIndex = 6;
            this.lblMaxNumResultados.Text = "Número de resultados por página:";
            // 
            // grpAvaliacao
            // 
            this.grpAvaliacao.Controls.Add(this.rbGestaoNaoIntegrada);
            this.grpAvaliacao.Controls.Add(this.rbGestaoIntegrada);
            this.grpAvaliacao.Controls.Add(this.tblLayout);
            this.grpAvaliacao.Location = new System.Drawing.Point(3, 382);
            this.grpAvaliacao.Name = "grpAvaliacao";
            this.grpAvaliacao.Size = new System.Drawing.Size(296, 72);
            this.grpAvaliacao.TabIndex = 4;
            this.grpAvaliacao.TabStop = false;
            this.grpAvaliacao.Text = "Modo de avaliação";
            this.grpAvaliacao.Visible = false;
            // 
            // rbGestaoNaoIntegrada
            // 
            this.rbGestaoNaoIntegrada.Location = new System.Drawing.Point(16, 40);
            this.rbGestaoNaoIntegrada.Name = "rbGestaoNaoIntegrada";
            this.rbGestaoNaoIntegrada.Size = new System.Drawing.Size(272, 24);
            this.rbGestaoNaoIntegrada.TabIndex = 11;
            this.rbGestaoNaoIntegrada.Text = "Não utilizar avaliação";
            // 
            // rbGestaoIntegrada
            // 
            this.rbGestaoIntegrada.Location = new System.Drawing.Point(16, 16);
            this.rbGestaoIntegrada.Name = "rbGestaoIntegrada";
            this.rbGestaoIntegrada.Size = new System.Drawing.Size(272, 24);
            this.rbGestaoIntegrada.TabIndex = 10;
            this.rbGestaoIntegrada.Text = "Utilizar avaliação";
            // 
            // tblLayout
            // 
            this.tblLayout.ColumnCount = 1;
            this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLayout.Location = new System.Drawing.Point(302, 0);
            this.tblLayout.Name = "tblLayout";
            this.tblLayout.RowCount = 1;
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLayout.Size = new System.Drawing.Size(114, 57);
            this.tblLayout.TabIndex = 10;
            // 
            // grpImagensWeb
            // 
            this.grpImagensWeb.Controls.Add(this.txtURLBase);
            this.grpImagensWeb.Controls.Add(this.chkActivar);
            this.grpImagensWeb.Controls.Add(this.lblUrlBase);
            this.grpImagensWeb.Controls.Add(this.rtbNotaPaginaWebImagens);
            this.grpImagensWeb.Location = new System.Drawing.Point(3, 200);
            this.grpImagensWeb.Name = "grpImagensWeb";
            this.grpImagensWeb.Size = new System.Drawing.Size(296, 176);
            this.grpImagensWeb.TabIndex = 5;
            this.grpImagensWeb.TabStop = false;
            this.grpImagensWeb.Text = "Acesso Web a imagens";
            // 
            // txtURLBase
            // 
            this.txtURLBase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtURLBase.Enabled = false;
            this.txtURLBase.Location = new System.Drawing.Point(80, 38);
            this.txtURLBase.Name = "txtURLBase";
            this.txtURLBase.Size = new System.Drawing.Size(200, 20);
            this.txtURLBase.TabIndex = 14;
            // 
            // chkActivar
            // 
            this.chkActivar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkActivar.Location = new System.Drawing.Point(16, 16);
            this.chkActivar.Name = "chkActivar";
            this.chkActivar.Size = new System.Drawing.Size(264, 24);
            this.chkActivar.TabIndex = 12;
            this.chkActivar.Text = "Usar página de apresentação externa";
            // 
            // lblUrlBase
            // 
            this.lblUrlBase.Location = new System.Drawing.Point(16, 40);
            this.lblUrlBase.Name = "lblUrlBase";
            this.lblUrlBase.Size = new System.Drawing.Size(64, 24);
            this.lblUrlBase.TabIndex = 13;
            this.lblUrlBase.Text = "URL base:";
            // 
            // rtbNotaPaginaWebImagens
            // 
            this.rtbNotaPaginaWebImagens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbNotaPaginaWebImagens.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbNotaPaginaWebImagens.DetectUrls = false;
            this.rtbNotaPaginaWebImagens.Enabled = false;
            this.rtbNotaPaginaWebImagens.Location = new System.Drawing.Point(16, 64);
            this.rtbNotaPaginaWebImagens.Name = "rtbNotaPaginaWebImagens";
            this.rtbNotaPaginaWebImagens.ReadOnly = true;
            this.rtbNotaPaginaWebImagens.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtbNotaPaginaWebImagens.Size = new System.Drawing.Size(264, 100);
            this.rtbNotaPaginaWebImagens.TabIndex = 15;
            this.rtbNotaPaginaWebImagens.Text = "";
            // 
            // grpGestaoModelos
            // 
            this.grpGestaoModelos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpGestaoModelos.Controls.Add(this.grpListaModelos);
            this.grpGestaoModelos.Controls.Add(this.grpModelos);
            this.grpGestaoModelos.Location = new System.Drawing.Point(349, 594);
            this.grpGestaoModelos.Name = "grpGestaoModelos";
            this.grpGestaoModelos.Size = new System.Drawing.Size(552, 238);
            this.grpGestaoModelos.TabIndex = 6;
            this.grpGestaoModelos.TabStop = false;
            this.grpGestaoModelos.Text = "Gestão de Modelos de Avaliação";
            this.grpGestaoModelos.Visible = false;
            // 
            // grpListaModelos
            // 
            this.grpListaModelos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpListaModelos.Controls.Add(this.btnRemoveLista);
            this.grpListaModelos.Controls.Add(this.btnEditLista);
            this.grpListaModelos.Controls.Add(this.btnAddLista);
            this.grpListaModelos.Controls.Add(this.LstVwListaModelos);
            this.grpListaModelos.Location = new System.Drawing.Point(295, 61);
            this.grpListaModelos.Name = "grpListaModelos";
            this.grpListaModelos.Size = new System.Drawing.Size(229, 145);
            this.grpListaModelos.TabIndex = 0;
            this.grpListaModelos.TabStop = false;
            this.grpListaModelos.Text = "Listas de Modelos de Avaliação";
            // 
            // btnRemoveLista
            // 
            this.btnRemoveLista.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveLista.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemoveLista.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemoveLista.Location = new System.Drawing.Point(199, 98);
            this.btnRemoveLista.Name = "btnRemoveLista";
            this.btnRemoveLista.Size = new System.Drawing.Size(24, 24);
            this.btnRemoveLista.TabIndex = 7;
            // 
            // btnEditLista
            // 
            this.btnEditLista.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditLista.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEditLista.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEditLista.Location = new System.Drawing.Point(199, 66);
            this.btnEditLista.Name = "btnEditLista";
            this.btnEditLista.Size = new System.Drawing.Size(24, 24);
            this.btnEditLista.TabIndex = 6;
            // 
            // btnAddLista
            // 
            this.btnAddLista.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddLista.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddLista.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddLista.Location = new System.Drawing.Point(199, 34);
            this.btnAddLista.Name = "btnAddLista";
            this.btnAddLista.Size = new System.Drawing.Size(24, 24);
            this.btnAddLista.TabIndex = 5;
            // 
            // LstVwListaModelos
            // 
            this.LstVwListaModelos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LstVwListaModelos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDesignacao,
            this.colDataInicio});
            this.LstVwListaModelos.FullRowSelect = true;
            this.LstVwListaModelos.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LstVwListaModelos.HideSelection = false;
            this.LstVwListaModelos.Location = new System.Drawing.Point(8, 20);
            this.LstVwListaModelos.MultiSelect = false;
            this.LstVwListaModelos.Name = "LstVwListaModelos";
            this.LstVwListaModelos.Size = new System.Drawing.Size(184, 126);
            this.LstVwListaModelos.TabIndex = 0;
            this.LstVwListaModelos.UseCompatibleStateImageBehavior = false;
            this.LstVwListaModelos.View = System.Windows.Forms.View.Details;
            // 
            // colDesignacao
            // 
            this.colDesignacao.Text = "Designação";
            this.colDesignacao.Width = 262;
            // 
            // colDataInicio
            // 
            this.colDataInicio.Text = "Data de Início";
            this.colDataInicio.Width = 90;
            // 
            // grpModelos
            // 
            this.grpModelos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpModelos.Controls.Add(this.btnRemoveModelo);
            this.grpModelos.Controls.Add(this.btnEditModelo);
            this.grpModelos.Controls.Add(this.btnAddModelo);
            this.grpModelos.Controls.Add(this.LstVwModelos);
            this.grpModelos.Location = new System.Drawing.Point(7, 50);
            this.grpModelos.Name = "grpModelos";
            this.grpModelos.Size = new System.Drawing.Size(229, 139);
            this.grpModelos.TabIndex = 1;
            this.grpModelos.TabStop = false;
            this.grpModelos.Text = "Modelos de Avaliação";
            this.grpModelos.Visible = false;
            // 
            // btnRemoveModelo
            // 
            this.btnRemoveModelo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveModelo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemoveModelo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemoveModelo.Location = new System.Drawing.Point(199, 98);
            this.btnRemoveModelo.Name = "btnRemoveModelo";
            this.btnRemoveModelo.Size = new System.Drawing.Size(24, 24);
            this.btnRemoveModelo.TabIndex = 10;
            // 
            // btnEditModelo
            // 
            this.btnEditModelo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditModelo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEditModelo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEditModelo.Location = new System.Drawing.Point(199, 66);
            this.btnEditModelo.Name = "btnEditModelo";
            this.btnEditModelo.Size = new System.Drawing.Size(24, 24);
            this.btnEditModelo.TabIndex = 9;
            // 
            // btnAddModelo
            // 
            this.btnAddModelo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddModelo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddModelo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddModelo.Location = new System.Drawing.Point(199, 34);
            this.btnAddModelo.Name = "btnAddModelo";
            this.btnAddModelo.Size = new System.Drawing.Size(24, 24);
            this.btnAddModelo.TabIndex = 8;
            // 
            // LstVwModelos
            // 
            this.LstVwModelos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LstVwModelos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDesignacaoModelo,
            this.colPrazo,
            this.colDestino});
            this.LstVwModelos.FullRowSelect = true;
            this.LstVwModelos.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LstVwModelos.HideSelection = false;
            this.LstVwModelos.Location = new System.Drawing.Point(8, 20);
            this.LstVwModelos.MultiSelect = false;
            this.LstVwModelos.Name = "LstVwModelos";
            this.LstVwModelos.Size = new System.Drawing.Size(184, 120);
            this.LstVwModelos.TabIndex = 0;
            this.LstVwModelos.UseCompatibleStateImageBehavior = false;
            this.LstVwModelos.View = System.Windows.Forms.View.Details;
            // 
            // colDesignacaoModelo
            // 
            this.colDesignacaoModelo.Text = "Designação";
            this.colDesignacaoModelo.Width = 186;
            // 
            // colPrazo
            // 
            this.colPrazo.Text = "Prazo (Anos)";
            this.colPrazo.Width = 75;
            // 
            // colDestino
            // 
            this.colDestino.Text = "Destino";
            this.colDestino.Width = 90;
            // 
            // grpBackup
            // 
            this.grpBackup.Controls.Add(this.btnBackup);
            this.grpBackup.Location = new System.Drawing.Point(3, 538);
            this.grpBackup.Name = "grpBackup";
            this.grpBackup.Size = new System.Drawing.Size(296, 82);
            this.grpBackup.TabIndex = 7;
            this.grpBackup.TabStop = false;
            this.grpBackup.Text = "Backup da base de dados";
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(6, 19);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(133, 40);
            this.btnBackup.TabIndex = 0;
            this.btnBackup.Text = "Criar backup";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // grpConfig_Dep
            // 
            this.grpConfig_Dep.Controls.Add(this.txt_metrosLinearesTotais);
            this.grpConfig_Dep.Controls.Add(this.lbl_gestaoDep);
            this.grpConfig_Dep.Location = new System.Drawing.Point(3, 137);
            this.grpConfig_Dep.Name = "grpConfig_Dep";
            this.grpConfig_Dep.Size = new System.Drawing.Size(296, 57);
            this.grpConfig_Dep.TabIndex = 8;
            this.grpConfig_Dep.TabStop = false;
            this.grpConfig_Dep.Text = "Gestão de depósitos";
            // 
            // txt_metrosLinearesTotais
            // 
            this.txt_metrosLinearesTotais.DecimalNumbers = -2147483648;
            this.txt_metrosLinearesTotais.Location = new System.Drawing.Point(198, 19);
            this.txt_metrosLinearesTotais.Name = "txt_metrosLinearesTotais";
            this.txt_metrosLinearesTotais.Size = new System.Drawing.Size(82, 20);
            this.txt_metrosLinearesTotais.TabIndex = 7;
            // 
            // lbl_gestaoDep
            // 
            this.lbl_gestaoDep.Location = new System.Drawing.Point(16, 24);
            this.lbl_gestaoDep.Name = "lbl_gestaoDep";
            this.lbl_gestaoDep.Size = new System.Drawing.Size(176, 16);
            this.lbl_gestaoDep.TabIndex = 6;
            this.lbl_gestaoDep.Text = "Metros lineares Totais";
            // 
            // grpFedora
            // 
            this.grpFedora.Controls.Add(this.lblQualidadeImagem);
            this.grpFedora.Controls.Add(this.txtPassword);
            this.grpFedora.Controls.Add(this.label2);
            this.grpFedora.Controls.Add(this.cbQualidadeImagem);
            this.grpFedora.Controls.Add(this.txtUsername);
            this.grpFedora.Controls.Add(this.lblUsername);
            this.grpFedora.Controls.Add(this.txtServerUrl);
            this.grpFedora.Controls.Add(this.lblServerUrl);
            this.grpFedora.Location = new System.Drawing.Point(3, 626);
            this.grpFedora.Name = "grpFedora";
            this.grpFedora.Size = new System.Drawing.Size(296, 128);
            this.grpFedora.TabIndex = 9;
            this.grpFedora.TabStop = false;
            this.grpFedora.Text = "Configurações de acesso Fedora";
            // 
            // lblQualidadeImagem
            // 
            this.lblQualidadeImagem.AutoSize = true;
            this.lblQualidadeImagem.Location = new System.Drawing.Point(6, 100);
            this.lblQualidadeImagem.Name = "lblQualidadeImagem";
            this.lblQualidadeImagem.Size = new System.Drawing.Size(112, 13);
            this.lblQualidadeImagem.TabIndex = 13;
            this.lblQualidadeImagem.Text = "Qualidade de imagem:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(90, 71);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(198, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password:";
            // 
            // cbQualidadeImagem
            // 
            this.cbQualidadeImagem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQualidadeImagem.FormattingEnabled = true;
            this.cbQualidadeImagem.Location = new System.Drawing.Point(124, 97);
            this.cbQualidadeImagem.Name = "cbQualidadeImagem";
            this.cbQualidadeImagem.Size = new System.Drawing.Size(164, 21);
            this.cbQualidadeImagem.TabIndex = 12;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(90, 45);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(198, 20);
            this.txtUsername.TabIndex = 3;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(6, 48);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "Username:";
            // 
            // txtServerUrl
            // 
            this.txtServerUrl.Location = new System.Drawing.Point(90, 19);
            this.txtServerUrl.Name = "txtServerUrl";
            this.txtServerUrl.Size = new System.Drawing.Size(198, 20);
            this.txtServerUrl.TabIndex = 1;
            // 
            // lblServerUrl
            // 
            this.lblServerUrl.AutoSize = true;
            this.lblServerUrl.Location = new System.Drawing.Point(6, 22);
            this.lblServerUrl.Name = "lblServerUrl";
            this.lblServerUrl.Size = new System.Drawing.Size(78, 13);
            this.lblServerUrl.TabIndex = 0;
            this.lblServerUrl.Text = "Url do servidor:";
            // 
            // flwPanel
            // 
            this.flwPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flwPanel.Controls.Add(this.grpGeral);
            this.flwPanel.Controls.Add(this.grpIntegração);
            this.flwPanel.Controls.Add(this.grpConfig_Dep);
            this.flwPanel.Controls.Add(this.grpImagensWeb);
            this.flwPanel.Controls.Add(this.grpAvaliacao);
            this.flwPanel.Controls.Add(this.grpFRDs);
            this.flwPanel.Controls.Add(this.grpBackup);
            this.flwPanel.Controls.Add(this.grpFedora);
            this.flwPanel.Controls.Add(this.grpGisaInternet);
            this.flwPanel.Location = new System.Drawing.Point(4, 56);
            this.flwPanel.Name = "flwPanel";
            this.flwPanel.Size = new System.Drawing.Size(304, 827);
            this.flwPanel.TabIndex = 11;
            // 
            // grpIntegração
            // 
            this.grpIntegração.Controls.Add(this.txtMaxNumDocumentos);
            this.grpIntegração.Controls.Add(this.label1);
            this.grpIntegração.Location = new System.Drawing.Point(3, 66);
            this.grpIntegração.Name = "grpIntegração";
            this.grpIntegração.Size = new System.Drawing.Size(296, 65);
            this.grpIntegração.TabIndex = 8;
            this.grpIntegração.TabStop = false;
            this.grpIntegração.Text = "Integração";
            // 
            // txtMaxNumDocumentos
            // 
            this.txtMaxNumDocumentos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxNumDocumentos.Location = new System.Drawing.Point(200, 22);
            this.txtMaxNumDocumentos.Name = "txtMaxNumDocumentos";
            this.txtMaxNumDocumentos.Size = new System.Drawing.Size(80, 20);
            this.txtMaxNumDocumentos.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 30);
            this.label1.TabIndex = 6;
            this.label1.Text = "Número de documentos para importar:";
            // 
            // grpGisaInternet
            // 
            this.grpGisaInternet.Controls.Add(this.txtGisaInternet);
            this.grpGisaInternet.Controls.Add(this.lblGisaInternet);
            this.grpGisaInternet.Location = new System.Drawing.Point(3, 760);
            this.grpGisaInternet.Name = "grpGisaInternet";
            this.grpGisaInternet.Size = new System.Drawing.Size(296, 54);
            this.grpGisaInternet.TabIndex = 10;
            this.grpGisaInternet.TabStop = false;
            this.grpGisaInternet.Text = "Gisa Internet";
            // 
            // txtGisaInternet
            // 
            this.txtGisaInternet.Location = new System.Drawing.Point(92, 19);
            this.txtGisaInternet.Name = "txtGisaInternet";
            this.txtGisaInternet.Size = new System.Drawing.Size(198, 20);
            this.txtGisaInternet.TabIndex = 3;
            // 
            // lblGisaInternet
            // 
            this.lblGisaInternet.AutoSize = true;
            this.lblGisaInternet.Location = new System.Drawing.Point(8, 22);
            this.lblGisaInternet.Name = "lblGisaInternet";
            this.lblGisaInternet.Size = new System.Drawing.Size(78, 13);
            this.lblGisaInternet.TabIndex = 2;
            this.lblGisaInternet.Text = "Url do servidor:";
            // 
            // grpValoresOmissão
            // 
            this.grpValoresOmissão.Controls.Add(this.grpIdioma);
            this.grpValoresOmissão.Controls.Add(this.grpCondicoesReproducao);
            this.grpValoresOmissão.Controls.Add(this.grpCondicoesAcesso);
            this.grpValoresOmissão.Location = new System.Drawing.Point(314, 59);
            this.grpValoresOmissão.Name = "grpValoresOmissão";
            this.grpValoresOmissão.Size = new System.Drawing.Size(694, 498);
            this.grpValoresOmissão.TabIndex = 12;
            this.grpValoresOmissão.TabStop = false;
            // 
            // grpIdioma
            // 
            this.grpIdioma.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpIdioma.Controls.Add(this.grpAlfabeto);
            this.grpIdioma.Controls.Add(this.grpLingua);
            this.grpIdioma.Location = new System.Drawing.Point(7, 388);
            this.grpIdioma.Name = "grpIdioma";
            this.grpIdioma.Size = new System.Drawing.Size(681, 104);
            this.grpIdioma.TabIndex = 10;
            this.grpIdioma.TabStop = false;
            this.grpIdioma.Text = "4.3. Língua e alfabeto";
            // 
            // grpAlfabeto
            // 
            this.grpAlfabeto.Controls.Add(this.btnRemoveAlfabeto);
            this.grpAlfabeto.Controls.Add(this.btnAddAlfabeto);
            this.grpAlfabeto.Controls.Add(this.lstVwAlfabeto);
            this.grpAlfabeto.Location = new System.Drawing.Point(344, 16);
            this.grpAlfabeto.Name = "grpAlfabeto";
            this.grpAlfabeto.Size = new System.Drawing.Size(330, 80);
            this.grpAlfabeto.TabIndex = 12;
            this.grpAlfabeto.TabStop = false;
            this.grpAlfabeto.Text = "Alfabeto";
            // 
            // btnRemoveAlfabeto
            // 
            this.btnRemoveAlfabeto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveAlfabeto.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemoveAlfabeto.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemoveAlfabeto.Location = new System.Drawing.Point(300, 48);
            this.btnRemoveAlfabeto.Name = "btnRemoveAlfabeto";
            this.btnRemoveAlfabeto.Size = new System.Drawing.Size(24, 24);
            this.btnRemoveAlfabeto.TabIndex = 5;
            this.btnRemoveAlfabeto.Click += new System.EventHandler(this.btnRemoveAlfabeto_Click);
            // 
            // btnAddAlfabeto
            // 
            this.btnAddAlfabeto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddAlfabeto.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddAlfabeto.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddAlfabeto.Location = new System.Drawing.Point(300, 16);
            this.btnAddAlfabeto.Name = "btnAddAlfabeto";
            this.btnAddAlfabeto.Size = new System.Drawing.Size(24, 24);
            this.btnAddAlfabeto.TabIndex = 4;
            this.btnAddAlfabeto.Click += new System.EventHandler(this.btnAddAlfabeto_Click);
            // 
            // lstVwAlfabeto
            // 
            this.lstVwAlfabeto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwAlfabeto.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colAlfabeto});
            this.lstVwAlfabeto.CustomizedSorting = false;
            this.lstVwAlfabeto.FullRowSelect = true;
            this.lstVwAlfabeto.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstVwAlfabeto.HideSelection = false;
            this.lstVwAlfabeto.Location = new System.Drawing.Point(8, 16);
            this.lstVwAlfabeto.MultiSelect = false;
            this.lstVwAlfabeto.Name = "lstVwAlfabeto";
            this.lstVwAlfabeto.ReturnSubItemIndex = false;
            this.lstVwAlfabeto.Size = new System.Drawing.Size(285, 56);
            this.lstVwAlfabeto.TabIndex = 1;
            this.lstVwAlfabeto.UseCompatibleStateImageBehavior = false;
            this.lstVwAlfabeto.View = System.Windows.Forms.View.Details;
            // 
            // colAlfabeto
            // 
            this.colAlfabeto.Width = 121;
            // 
            // grpLingua
            // 
            this.grpLingua.Controls.Add(this.btnRemoveLingua);
            this.grpLingua.Controls.Add(this.btnAddLingua);
            this.grpLingua.Controls.Add(this.lstVwLanguage);
            this.grpLingua.Location = new System.Drawing.Point(8, 16);
            this.grpLingua.Name = "grpLingua";
            this.grpLingua.Size = new System.Drawing.Size(330, 80);
            this.grpLingua.TabIndex = 11;
            this.grpLingua.TabStop = false;
            this.grpLingua.Text = "Língua";
            // 
            // btnRemoveLingua
            // 
            this.btnRemoveLingua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveLingua.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemoveLingua.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemoveLingua.Location = new System.Drawing.Point(300, 48);
            this.btnRemoveLingua.Name = "btnRemoveLingua";
            this.btnRemoveLingua.Size = new System.Drawing.Size(24, 24);
            this.btnRemoveLingua.TabIndex = 12;
            this.btnRemoveLingua.Click += new System.EventHandler(this.btnRemoveLingua_Click);
            // 
            // btnAddLingua
            // 
            this.btnAddLingua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddLingua.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddLingua.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddLingua.Location = new System.Drawing.Point(300, 16);
            this.btnAddLingua.Name = "btnAddLingua";
            this.btnAddLingua.Size = new System.Drawing.Size(24, 24);
            this.btnAddLingua.TabIndex = 11;
            this.btnAddLingua.Click += new System.EventHandler(this.btnAddLingua_Click);
            // 
            // lstVwLanguage
            // 
            this.lstVwLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwLanguage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLingua});
            this.lstVwLanguage.CustomizedSorting = false;
            this.lstVwLanguage.FullRowSelect = true;
            this.lstVwLanguage.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstVwLanguage.HideSelection = false;
            this.lstVwLanguage.Location = new System.Drawing.Point(8, 16);
            this.lstVwLanguage.MultiSelect = false;
            this.lstVwLanguage.Name = "lstVwLanguage";
            this.lstVwLanguage.ReturnSubItemIndex = false;
            this.lstVwLanguage.Size = new System.Drawing.Size(285, 56);
            this.lstVwLanguage.TabIndex = 10;
            this.lstVwLanguage.UseCompatibleStateImageBehavior = false;
            this.lstVwLanguage.View = System.Windows.Forms.View.Details;
            // 
            // colLingua
            // 
            this.colLingua.Width = 121;
            // 
            // grpCondicoesReproducao
            // 
            this.grpCondicoesReproducao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCondicoesReproducao.Controls.Add(this.txtCondicoesReproducao);
            this.grpCondicoesReproducao.Location = new System.Drawing.Point(7, 249);
            this.grpCondicoesReproducao.Name = "grpCondicoesReproducao";
            this.grpCondicoesReproducao.Size = new System.Drawing.Size(681, 125);
            this.grpCondicoesReproducao.TabIndex = 9;
            this.grpCondicoesReproducao.TabStop = false;
            this.grpCondicoesReproducao.Text = "4.2. Condições de reprodução";
            // 
            // txtCondicoesReproducao
            // 
            this.txtCondicoesReproducao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCondicoesReproducao.Location = new System.Drawing.Point(8, 14);
            this.txtCondicoesReproducao.Multiline = true;
            this.txtCondicoesReproducao.Name = "txtCondicoesReproducao";
            this.txtCondicoesReproducao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCondicoesReproducao.Size = new System.Drawing.Size(665, 103);
            this.txtCondicoesReproducao.TabIndex = 0;
            // 
            // grpCondicoesAcesso
            // 
            this.grpCondicoesAcesso.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCondicoesAcesso.Controls.Add(this.txtCondicoesAcesso);
            this.grpCondicoesAcesso.Location = new System.Drawing.Point(7, 19);
            this.grpCondicoesAcesso.Name = "grpCondicoesAcesso";
            this.grpCondicoesAcesso.Size = new System.Drawing.Size(681, 224);
            this.grpCondicoesAcesso.TabIndex = 8;
            this.grpCondicoesAcesso.TabStop = false;
            this.grpCondicoesAcesso.Text = "4.1. Condições de acesso";
            // 
            // txtCondicoesAcesso
            // 
            this.txtCondicoesAcesso.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCondicoesAcesso.Location = new System.Drawing.Point(8, 14);
            this.txtCondicoesAcesso.Multiline = true;
            this.txtCondicoesAcesso.Name = "txtCondicoesAcesso";
            this.txtCondicoesAcesso.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCondicoesAcesso.Size = new System.Drawing.Size(665, 202);
            this.txtCondicoesAcesso.TabIndex = 0;
            // 
            // chkValoresOmissao
            // 
            this.chkValoresOmissao.AutoSize = true;
            this.chkValoresOmissao.Location = new System.Drawing.Point(321, 59);
            this.chkValoresOmissao.Name = "chkValoresOmissao";
            this.chkValoresOmissao.Size = new System.Drawing.Size(120, 17);
            this.chkValoresOmissao.TabIndex = 11;
            this.chkValoresOmissao.Text = "Valores por omissão";
            this.chkValoresOmissao.UseVisualStyleBackColor = true;
            this.chkValoresOmissao.CheckedChanged += new System.EventHandler(this.chkValoresOmissao_CheckedChanged);
            // 
            // MasterPanelAdminGlobal
            // 
            this.Controls.Add(this.chkValoresOmissao);
            this.Controls.Add(this.grpValoresOmissão);
            this.Controls.Add(this.grpGestaoModelos);
            this.Controls.Add(this.flwPanel);
            this.Name = "MasterPanelAdminGlobal";
            this.Size = new System.Drawing.Size(1011, 886);
            this.Controls.SetChildIndex(this.flwPanel, 0);
            this.Controls.SetChildIndex(this.lblFuncao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.Controls.SetChildIndex(this.grpGestaoModelos, 0);
            this.Controls.SetChildIndex(this.grpValoresOmissão, 0);
            this.Controls.SetChildIndex(this.chkValoresOmissao, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.grpFRDs.ResumeLayout(false);
            this.grpGeral.ResumeLayout(false);
            this.grpGeral.PerformLayout();
            this.grpAvaliacao.ResumeLayout(false);
            this.grpImagensWeb.ResumeLayout(false);
            this.grpImagensWeb.PerformLayout();
            this.grpGestaoModelos.ResumeLayout(false);
            this.grpListaModelos.ResumeLayout(false);
            this.grpModelos.ResumeLayout(false);
            this.grpBackup.ResumeLayout(false);
            this.grpConfig_Dep.ResumeLayout(false);
            this.grpFedora.ResumeLayout(false);
            this.grpFedora.PerformLayout();
            this.flwPanel.ResumeLayout(false);
            this.grpIntegração.ResumeLayout(false);
            this.grpIntegração.PerformLayout();
            this.grpGisaInternet.ResumeLayout(false);
            this.grpGisaInternet.PerformLayout();
            this.grpValoresOmissão.ResumeLayout(false);
            this.grpIdioma.ResumeLayout(false);
            this.grpAlfabeto.ResumeLayout(false);
            this.grpLingua.ResumeLayout(false);
            this.grpCondicoesReproducao.ResumeLayout(false);
            this.grpCondicoesReproducao.PerformLayout();
            this.grpCondicoesAcesso.ResumeLayout(false);
            this.grpCondicoesAcesso.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

	#endregion

		public static Bitmap FunctionImage
		{
			get
			{
				return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "ConfiguracaoGlobal_enabled_32x32.png");
			}
		}

		public void GetExtraResources()
		{
			btnAddLista.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
			btnEditLista.Image = SharedResourcesOld.CurrentSharedResources.Editar;
			btnRemoveLista.Image = SharedResourcesOld.CurrentSharedResources.Apagar;
			btnAddModelo.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
			btnEditModelo.Image = SharedResourcesOld.CurrentSharedResources.Editar;
			btnRemoveModelo.Image = SharedResourcesOld.CurrentSharedResources.Apagar;

			System.IO.Stream helpFileStream = null;
			System.Reflection.Assembly gisaAssembly = System.Reflection.Assembly.GetAssembly(typeof(MasterPanelAdminGlobal));
            helpFileStream = gisaAssembly.GetManifestResourceStream(gisaAssembly.GetName().Name + ".DocumentosAjuda.NotaPaginaWebImagens.rtf");
			if (helpFileStream != null)
			{
				rtbNotaPaginaWebImagens.LoadFile(helpFileStream, RichTextBoxStreamType.RichText);
				helpFileStream.Close();
			}

            btnAddLingua.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
            btnRemoveLingua.Image = SharedResourcesOld.CurrentSharedResources.Apagar;
            btnAddAlfabeto.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
            btnRemoveAlfabeto.Image = SharedResourcesOld.CurrentSharedResources.Apagar;

            CurrentToolTip.SetToolTip(btnAddAlfabeto, SharedResourcesOld.CurrentSharedResources.AdicionarString);
            CurrentToolTip.SetToolTip(btnAddLingua, SharedResourcesOld.CurrentSharedResources.AdicionarString);
            CurrentToolTip.SetToolTip(btnRemoveAlfabeto, SharedResourcesOld.CurrentSharedResources.ApagarString);
            CurrentToolTip.SetToolTip(btnRemoveLingua, SharedResourcesOld.CurrentSharedResources.ApagarString);
		}

		private GISADataset.GlobalConfigRow GlobalConfigRow;
		public override void LoadData()
		{
			GlobalConfigRow = (GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Rows[0]);
			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
				NivelRule.Current.LoadModelosAvaliacao(GisaDataSetHelper.GetInstance(), ho.Connection);
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

		public override void ModelToView()
		{
            FillQualidadeImagem();

			rbNiveisEPs.Checked = GlobalConfigRow.NiveisOrganicos;
			rbNiveisTFs.Checked = ! GlobalConfigRow.NiveisOrganicos;
			rbGestaoIntegrada.Checked = GlobalConfigRow.GestaoIntegrada;
			rbGestaoNaoIntegrada.Checked = ! GlobalConfigRow.GestaoIntegrada;
			txtMaxNumResultados.Text = GlobalConfigRow.MaxNumResultados.ToString();
            txtMaxNumDocumentos.Text = GlobalConfigRow.MaxNumDocumentos.ToString();

			txtURLBase.Text = GlobalConfigRow.IsURLBaseNull() ? "" : GlobalConfigRow.URLBase;
            txt_metrosLinearesTotais.Text = GlobalConfigRow.IsMetrosLinearesTotaisNull() ? "" : GlobalConfigRow.MetrosLinearesTotais.ToString();
            txtServerUrl.Text = GlobalConfigRow.IsFedoraServerUrlNull() ? "" : GlobalConfigRow.FedoraServerUrl;
            txtUsername.Text = GlobalConfigRow.IsFedoraUsernameNull() ? "" : GlobalConfigRow.FedoraUsername;
            txtPassword.Text = GlobalConfigRow.IsFedoraPasswordNull() ? "" : GlobalConfigRow.FedoraPassword;
            txtGisaInternet.Text = GlobalConfigRow.IsUrlGisaInternetNull() ? "" : GlobalConfigRow.UrlGisaInternet;
            cbQualidadeImagem.SelectedItem = GlobalConfigRow.IsQualidadeImagemNull() ? FedoraHelper.TranslateQualityEnum(Quality.Low) : GlobalConfigRow.QualidadeImagem;

			chkActivar.Checked = GlobalConfigRow.URLBaseActivo;

            // Valores por omissão
            chkValoresOmissao.Checked = GlobalConfigRow.IsApplyDefaultValuesNull() ? false : GlobalConfigRow.ApplyDefaultValues;
            txtCondicoesAcesso.Text = GlobalConfigRow.IsCondicaoDeAcessoNull() ? "" : GlobalConfigRow.CondicaoDeAcesso;
            txtCondicoesReproducao.Text = GlobalConfigRow.IsCondicaoDeReproducaoNull() ? "" :  GlobalConfigRow.CondicaoDeReproducao;

            LoadAlfabetoAndLingua();

            grpValoresOmissão.Enabled = chkValoresOmissao.Checked;

			IDbConnection conn = GisaDataSetHelper.GetConnection();
			long nivelEstruturalCount = 0;
			try
			{
				conn.Open();

				Trace.WriteLine("<getNivelEstruturalCount>");

				nivelEstruturalCount = NivelRule.Current.GetNivelEstruturalCount(conn);

				Trace.WriteLine("</getNivelEstruturalCount>");

				if (nivelEstruturalCount > 0)
				{
					grpFRDs.Enabled = false;
					grpAvaliacao.Enabled = false;
				}

			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}


			try
			{
				GisaPrincipalPermission tempWith1 = new GisaPrincipalPermission(SessionHelper.GetGisaPrincipal(), this.GetType().FullName, GisaPrincipalPermission.WRITE);
				tempWith1.Demand();
			}
			catch (System.Security.SecurityException)
			{
	#if ! ALLPRODUCTPERMISSIONS
				this.Enabled = false;
	#endif
			}

			// este handler tem de ser adicionado no fim do activate para que a população inicial do controlo não lance o evento
			rbNiveisEPs.CheckedChanged += rbNiveisEPs_CheckedChanged;

			// carregar informação referente aos modelos de avaliação
			PopulateListaModelosAvaliacao();
			UpdateToolBarButtonsLista();
			UpdateToolBarButtonsModelos();
			this.Visible = true;
		}

        private void FillQualidadeImagem()
        {
            foreach (Quality item in System.Enum.GetValues(typeof(Quality)))
                cbQualidadeImagem.Items.Insert(0, FedoraHelper.TranslateQualityEnum(item));
        }


		public override bool ViewToModel()
		{
			if (rbNiveisEPs.Checked != GlobalConfigRow.NiveisOrganicos)
				GlobalConfigRow.NiveisOrganicos = rbNiveisEPs.Checked;
			if (rbGestaoIntegrada.Checked != GlobalConfigRow.GestaoIntegrada)
				GlobalConfigRow.GestaoIntegrada = rbGestaoIntegrada.Checked;
			if (txtMaxNumResultados.Text.Length > 0)
                GlobalConfigRow.MaxNumResultados = System.Convert.ToInt32(txtMaxNumResultados.Text);
            if (txtMaxNumDocumentos.Text.Length > 0)
                GlobalConfigRow.MaxNumDocumentos = System.Convert.ToInt32(txtMaxNumDocumentos.Text);
            if  (txt_metrosLinearesTotais.Text.Equals(string.Empty))
                GlobalConfigRow.MetrosLinearesTotais = 0;
            else
                GlobalConfigRow.MetrosLinearesTotais = System.Convert.ToDecimal(txt_metrosLinearesTotais.Text);

			GlobalConfigRow.URLBase = txtURLBase.Text;
			GlobalConfigRow.URLBaseActivo = chkActivar.Checked;

            GlobalConfigRow.FedoraServerUrl = txtServerUrl.Text.EndsWith("/") ? txtServerUrl.Text : txtServerUrl.Text + "/";
            GlobalConfigRow.FedoraUsername = txtUsername.Text;
            GlobalConfigRow.FedoraPassword = txtPassword.Text;
            GlobalConfigRow.QualidadeImagem = cbQualidadeImagem.SelectedItem.ToString();

            GlobalConfigRow.UrlGisaInternet= txtGisaInternet.Text;

            // valores por omissão
            GlobalConfigRow.ApplyDefaultValues = chkValoresOmissao.Checked;
            GlobalConfigRow.CondicaoDeAcesso = txtCondicoesAcesso.Text;
            GlobalConfigRow.CondicaoDeReproducao = txtCondicoesReproducao.Text;

			GISAControl.EndCurrentEdit(this);
			return ! (GlobalConfigRow.RowState != DataRowState.Unchanged);
		}

        #region Valores por omissão Língua/Alfabeto
        private void LoadAlfabetoAndLingua()
        {
            this.lstVwAlfabeto.BeginUpdate();
            this.lstVwAlfabeto.Items.Clear();

            if (GlobalConfigRow.GetConfigAlfabetoRows().Length > 0)
            {
                foreach (GISADataset.ConfigAlfabetoRow alfRow in GlobalConfigRow.GetConfigAlfabetoRows())
                {
                    ListViewItem lstVw = new ListViewItem();
                    lstVw.SubItems[colAlfabeto.Index].Text = alfRow.Iso15924Row.ScriptNameEnglish.Trim();
                    lstVw.Tag = alfRow;
                    this.lstVwAlfabeto.Items.Add(lstVw);
                }
            }
            this.lstVwAlfabeto.EndUpdate();
            this.lstVwAlfabeto.Enabled = true;

            this.lstVwLanguage.BeginUpdate();
            this.lstVwLanguage.Items.Clear();
            if (GlobalConfigRow.GetConfigLinguaRows().Length > 0)
            {
                foreach (GISADataset.ConfigLinguaRow langRow in GlobalConfigRow.GetConfigLinguaRows())
                {
                    ListViewItem lstVw = new ListViewItem();
                    lstVw.Tag = langRow;
                    lstVw.SubItems[colLingua.Index].Text = langRow.Iso639Row.LanguageNameEnglish.Trim();
                    this.lstVwLanguage.Items.Add(lstVw);
                }
            }
            this.lstVwLanguage.EndUpdate();
        }

        private void chkValoresOmissao_CheckedChanged(object sender, EventArgs e)
        {
            grpValoresOmissão.Enabled = chkValoresOmissao.Checked;
        }

        private void btnAddLingua_Click(object sender, System.EventArgs e)
        {
            FormPickISOs frmPick = new FormPickISOs(false);
            frmPick.Title = "Linguagens";
            frmPick.reloadList();

            switch (frmPick.ShowDialog())
            {
                case DialogResult.OK:
                    foreach (ListViewItem li in frmPick.SelectedItems)
                    {
                        GISADataset.Iso639Row langRow = null;
                        GISADataset.ConfigLinguaRow sLRow = null;
                        langRow = (GISADataset.Iso639Row)li.Tag;
                        string query = string.Format("IDIso639={0} AND IDGlobalConfig={1}", langRow.ID, GlobalConfigRow.ID);

                        if ((GisaDataSetHelper.GetInstance().ConfigLingua.Select(query)).Length == 0)
                        {
                            sLRow = GisaDataSetHelper.GetInstance().ConfigLingua.NewConfigLinguaRow();
                            sLRow.IDIso639 = langRow.ID;
                            sLRow.GlobalConfigRow = GlobalConfigRow;
                            sLRow.isDeleted = 0;

                            GisaDataSetHelper.GetInstance().ConfigLingua.AddConfigLinguaRow(sLRow);

                            if (sLRow.RowState == DataRowState.Detached)
                                MessageBox.Show("Não foi possível estabelecer a relação uma vez que a Linguagem " + System.Environment.NewLine + "que pretende associar foi apagada por outro utilizador.", "Erro ao estabelecer relação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }
                        else
                            MessageBox.Show("A relação que pretende adicionar já existe.", "Erro ao estabelecer relação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                    LoadAlfabetoAndLingua();
                    break;
                case DialogResult.Cancel:
                    break;
            }
        }

        private void btnRemoveLingua_Click(object sender, System.EventArgs e)
        {
            if (lstVwLanguage.SelectedItems.Count > 0)
                GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwLanguage);
        }

        private void btnAddAlfabeto_Click(object sender, System.EventArgs e)
        {
            FormPickISOs frmPick = new FormPickISOs(true);
            frmPick.Title = "Alfabetos";
            frmPick.reloadList();

            switch (frmPick.ShowDialog())
            {
                case DialogResult.OK:
                    foreach (ListViewItem li in frmPick.SelectedItems)
                    {
                        GISADataset.Iso15924Row alphaRow = null;
                        GISADataset.ConfigAlfabetoRow sARow = null;
                        alphaRow = (GISADataset.Iso15924Row)li.Tag;
                        string query = string.Format("IDIso15924={0} AND IDGlobalConfig={1}", alphaRow.ID, GlobalConfigRow.ID);

                        if ((GisaDataSetHelper.GetInstance().ConfigAlfabeto.Select(query)).Length == 0)
                        {
                            sARow = GisaDataSetHelper.GetInstance().ConfigAlfabeto.NewConfigAlfabetoRow();
                            sARow.IDIso15924 = alphaRow.ID;
                            sARow.GlobalConfigRow = GlobalConfigRow;
                            sARow.isDeleted = 0;

                            GisaDataSetHelper.GetInstance().ConfigAlfabeto.AddConfigAlfabetoRow(sARow);

                            if (sARow.RowState == DataRowState.Detached)
                                MessageBox.Show("Não foi possível estabelecer a relação uma vez que a Linguagem " + System.Environment.NewLine + "que pretende associar foi apagada por outro utilizador.", "Erro ao estabelecer relação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }
                        else
                            MessageBox.Show("A relação que pretende adicionar já existe.", "Erro ao estabelecer relação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                    LoadAlfabetoAndLingua();
                    break;
                case DialogResult.Cancel:
                    break;
            }
        }

        private void btnRemoveAlfabeto_Click(object sender, System.EventArgs e)
        {
            if (lstVwAlfabeto.SelectedItems.Count > 0)
                GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwAlfabeto);
        }

        private void lstVwLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void lstVwAlfabeto_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            btnRemoveLingua.Enabled = lstVwLanguage.SelectedItems.Count > 0;
            btnRemoveAlfabeto.Enabled = lstVwAlfabeto.SelectedItems.Count > 0;
        }
        #endregion

        public override void Deactivate()
		{
			Enabled = false;
            GUIHelper.GUIHelper.clearField(rbNiveisEPs);
            GUIHelper.GUIHelper.clearField(rbNiveisTFs);
            GUIHelper.GUIHelper.clearField(rbGestaoIntegrada);
            GUIHelper.GUIHelper.clearField(rbGestaoNaoIntegrada);
            GUIHelper.GUIHelper.clearField(txtMaxNumResultados);
            GUIHelper.GUIHelper.clearField(txtMaxNumDocumentos);
            GUIHelper.GUIHelper.clearField(txtURLBase);
            GUIHelper.GUIHelper.clearField(chkActivar);
            GUIHelper.GUIHelper.clearField(LstVwListaModelos);
            GUIHelper.GUIHelper.clearField(LstVwModelos);
		}


        public override PersistencyHelper.SaveResult Save()
		{
			return Save(false);
		}

        public override PersistencyHelper.SaveResult Save(bool activateOpcaoCancelar)
		{
			if (rbNiveisEPs.Checked != GlobalConfigRow.NiveisOrganicos)
				GlobalConfigRow.NiveisOrganicos = rbNiveisEPs.Checked;
			if (rbGestaoIntegrada.Checked != GlobalConfigRow.GestaoIntegrada)
				GlobalConfigRow.GestaoIntegrada = rbGestaoIntegrada.Checked;
			PersistencyHelper.save();
			PersistencyHelper.cleanDeletedData();
            return PersistencyHelper.SaveResult.unsuccessful;
		}

		private void txtMaxNumResultados_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
            var txtBox = (TextBox)sender;
            System.Text.RegularExpressions.Regex exp = new System.Text.RegularExpressions.Regex("0*[1-9]+[0-9]*");
            System.Text.RegularExpressions.Match match = exp.Match(txtBox.Text);
            if (!(match.Success && match.Value.Equals(txtBox.Text)))
			{
				MessageBox.Show("O número de resultados por página tem que ser um valor numérico maior que zero.", "Valor errado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				e.Cancel = true;
			}
		}

		private void rbNiveisEPs_CheckedChanged(object sender, System.EventArgs e)
		{
			MessageBox.Show("Para que alterações a esta opção sejam aplicadas o GISA terá de ser reiniciado.", "Aplicação de alterações", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void MasterPanelAdminGlobal_ParentChanged(object sender, System.EventArgs e)
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
				try
				{
					LoadData();
					ModelToView();
				}
				catch (Exception ex)
				{
					Trace.WriteLine(ex);
					throw;
				}
			}
		}

		private void chkActivar_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkActivar.Checked)
			{
				txtURLBase.Enabled = true;
			}
			else
			{
				txtURLBase.Enabled = false;
			}
		}

	#region Gestão de Modelos de Avaliação
		private void PopulateListaModelosAvaliacao()
		{
			LstVwListaModelos.BeginUpdate();
			foreach (GISADataset.ListaModelosAvaliacaoRow lstModAvRow in GisaDataSetHelper.GetInstance().ListaModelosAvaliacao.Select("", "DataInicio DESC, Designacao ASC"))
			{
				ListViewItem item = NewListaModeloItem(lstModAvRow);
				LstVwListaModelos.Items.Add(item);
			}
			LstVwListaModelos.EndUpdate();
		}

		private void btnAddLista_Click(object sender, System.EventArgs e)
		{
			FormListaModelosAvaliacao frm = new FormListaModelosAvaliacao();
			frm.Text = "Criar " + frm.Text;
			switch (frm.ShowDialog())
			{
				case DialogResult.OK:
				{
					int dResult = (int)DialogResult.OK;
					if (LstVwListaModelos.Items.Count > 0)
					{
						GISADataset.ListaModelosAvaliacaoRow lstModAvRow = GetMostRecentList();
                        if (lstModAvRow.DataInicio < GUIHelper.GUIHelper.GetData(frm.PxDateBox1))
						{
							dResult = Convert.ToInt32(MessageBox.Show("Está prestes a criar uma nova lista de modelos de avaliação. Todos as séries e sub-séries avaliadas perderão a avaliacão segundo modelos de avaliação da lista antiga. Pretende continuar?", "Nova Lista de Modelos", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning));
						}
					}
					if (dResult == (int)DialogResult.OK)
					{
						GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
						try
						{
                            DBAbstractDataLayer.DataAccessRules.NivelRule.Current.ClearAvaliacaoTabelaSeries(GUIHelper.GUIHelper.GetData(frm.PxDateBox1), ho.Connection);
							GISADataset.ListaModelosAvaliacaoRow lstModAvRow = null;
							lstModAvRow = GisaDataSetHelper.GetInstance().ListaModelosAvaliacao.NewListaModelosAvaliacaoRow();
							lstModAvRow.Designacao = frm.txtDesignacaoListaModelos.Text;
                            GUIHelper.GUIHelper.storeData(frm.PxDateBox1, lstModAvRow);
							lstModAvRow.Versao = new byte[]{};
							GisaDataSetHelper.GetInstance().ListaModelosAvaliacao.AddListaModelosAvaliacaoRow(lstModAvRow);

							ListViewItem item = NewListaModeloItem(lstModAvRow);
							LstVwListaModelos.Items.Add(item);
							item.Selected = true;
						}
						catch (Exception ex)
						{
							Trace.WriteLine(ex);
							MessageBox.Show("Não foi possível completar a operação. Tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						finally
						{
							ho.Dispose();
						}
					}
					break;
				}
				case DialogResult.Cancel:
				{
					return;
				}
			}
		}

		private void btnEditLista_Click(object sender, System.EventArgs e)
		{
			ListViewItem item = LstVwListaModelos.SelectedItems[0];
			GISADataset.ListaModelosAvaliacaoRow lstModAvRow = (GISADataset.ListaModelosAvaliacaoRow)item.Tag;
			FormListaModelosAvaliacao frm = new FormListaModelosAvaliacao();
			frm.Text = "Editar " + frm.Text;
			frm.txtDesignacaoListaModelos.Text = lstModAvRow.Designacao;
            GUIHelper.GUIHelper.populateData(frm.PxDateBox1, lstModAvRow);
			switch (frm.ShowDialog())
			{
				case DialogResult.OK:
					if (! (lstModAvRow.RowState == DataRowState.Added))
					{
						int dResult = (int)DialogResult.OK;
						if (LstVwListaModelos.Items.Count > 0)
						{
							GISADataset.ListaModelosAvaliacaoRow mostRecentLstModAvRow = GetMostRecentList();
                            if (mostRecentLstModAvRow.DataInicio < GUIHelper.GUIHelper.GetData(frm.PxDateBox1))
							{
                                dResult = Convert.ToInt32(MessageBox.Show("Está prestes a alterar a data de início de uma nova lista de modelos de avaliação. Todos as séries e sub-séries avaliadas perderão a avaliacão segundo modelos de avaliação da lista antiga. Pretende continuar?", "Nova Lista de Modelos", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning));
							}
						}
						GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
						try
						{
							bool result = false;
                            result = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.ManageListasModelosAvaliacao(false, lstModAvRow.ID, frm.txtDesignacaoListaModelos.Text, GUIHelper.GUIHelper.GetData(frm.PxDateBox1), ho.Connection);
							if (result)
							{
								lstModAvRow.Designacao = frm.txtDesignacaoListaModelos.Text;
                                GUIHelper.GUIHelper.storeData(frm.PxDateBox1, lstModAvRow);
								UpdateListaModeloItem(item, lstModAvRow);
								lstModAvRow.AcceptChanges();
							}
							else
							{
								MessageBox.Show("A lista de modelos de avaliação selecionada não pode ser alterada uma vez que já está a ser utilizada na avaliação de níveis documentais.", "Editar Lista de Avaliações", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							}
						}
						catch (Exception ex)
						{
							Trace.WriteLine(ex);
							MessageBox.Show("Não foi possível completar a operação. Tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						finally
						{
							ho.Dispose();
						}
					}
					else
					{
						lstModAvRow.Designacao = frm.txtDesignacaoListaModelos.Text;
                        GUIHelper.GUIHelper.storeData(frm.PxDateBox1, lstModAvRow);
						UpdateListaModeloItem(item, lstModAvRow);
					}
					break;
				case DialogResult.Cancel:
					return;
			}
		}

		private ListViewItem NewListaModeloItem(GISADataset.ListaModelosAvaliacaoRow lstModAvRow)
		{
			ListViewItem item = new ListViewItem();
			item.SubItems.AddRange(new string[] {colDesignacao.Text, colDataInicio.Text});
			UpdateListaModeloItem(item, lstModAvRow);
			item.Tag = lstModAvRow;
			return item;
		}

		private void UpdateListaModeloItem(ListViewItem item, GISADataset.ListaModelosAvaliacaoRow lstModAvRow)
		{
			item.SubItems[colDesignacao.Index].Text = lstModAvRow.Designacao;
            item.SubItems[colDataInicio.Index].Text = GUIHelper.GUIHelper.FormatDate(lstModAvRow);
		}

		private void btnRemoveLista_Click(object sender, System.EventArgs e)
		{
			//não é permitido editar ou eliminar uma lista se algum dos seus modelos estiver a ser usado nalguma avaliação
			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
				bool result = false;
				GISADataset.ListaModelosAvaliacaoRow lstModAvRow = (GISADataset.ListaModelosAvaliacaoRow)(LstVwListaModelos.SelectedItems[0].Tag);
				result = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.ManageListasModelosAvaliacao(true, lstModAvRow.ID, null, DateTime.Now, ho.Connection);
				if (result)
				{
					lstModAvRow.Delete();
					lstModAvRow.AcceptChanges();
					LstVwListaModelos.Items.Remove(LstVwListaModelos.SelectedItems[0]);
					LstVwModelos.Items.Clear();
				}
				else
				{
					MessageBox.Show("A lista de modelos de avaliação selecionada não pode ser apagada uma vez que já está a ser utilizada na avaliação de níveis documentais.", "Apagar Lista de Avaliações", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				MessageBox.Show("Não foi possível completar a operação. Tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				ho.Dispose();
			}
		}

		private void UpdateToolBarButtonsLista()
		{
			if (LstVwListaModelos.SelectedItems.Count > 0)
			{
				btnEditLista.Enabled = true;
				btnRemoveLista.Enabled = true;
			}
			else
			{
				btnEditLista.Enabled = false;
				btnRemoveLista.Enabled = false;
			}
		}

		private void LstVwListaModelos_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (LstVwListaModelos.SelectedItems.Count == 0)
			{
				LstVwModelos.Items.Clear();
			}
			else
			{
				//carregar modelos da lista seleccionada
				//mostrar elementos
				PopulateModelosAvaliacao(((GISADataset.ListaModelosAvaliacaoRow)(LstVwListaModelos.SelectedItems[0].Tag)).ID);
			}
			UpdateToolBarButtonsLista();
			UpdateToolBarButtonsModelos();
		}

		private void PopulateModelosAvaliacao(long IDLista)
		{
			LstVwModelos.BeginUpdate();
			LstVwModelos.Items.Clear();
			foreach (GISADataset.ModelosAvaliacaoRow modAvRow in GisaDataSetHelper.GetInstance().ModelosAvaliacao.Select("IDListaModelosAvaliacao=" + IDLista.ToString()))
			{
				ListViewItem item = NewModeloItem(modAvRow);
				LstVwModelos.Items.Add(item);
			}
			LstVwModelos.EndUpdate();
		}

		private ListViewItem NewModeloItem(GISADataset.ModelosAvaliacaoRow modAvRow)
		{
			ListViewItem item = new ListViewItem();
			item.SubItems.AddRange(new string[] {colDesignacaoModelo.Text, colDestino.Text, colPrazo.Text});
			UpdateModeloItem(item, modAvRow);
			item.Tag = modAvRow;
			return item;
		}

		private void UpdateModeloItem(ListViewItem item, GISADataset.ModelosAvaliacaoRow modAvRow)
		{
			item.SubItems[colDesignacao.Index].Text = modAvRow.Designacao;
            item.SubItems[colDestino.Index].Text = GUIHelper.GUIHelper.formatDestinoFinal(modAvRow);
			item.SubItems[colPrazo.Index].Text = modAvRow.PrazoConservacao.ToString();
		}

		private void btnAddModelo_Click(object sender, System.EventArgs e)
		{
			FormModelosAvaliacao frm = new FormModelosAvaliacao();
			frm.Text = "Criar " + frm.Text;
			switch (frm.ShowDialog())
			{
				case DialogResult.OK:
					GISADataset.ModelosAvaliacaoRow modAvRow = null;
					modAvRow = GisaDataSetHelper.GetInstance().ModelosAvaliacao.NewModelosAvaliacaoRow();
					modAvRow.IDListaModelosAvaliacao = ((GISADataset.ListaModelosAvaliacaoRow)(LstVwListaModelos.SelectedItems[0].Tag)).ID;
					modAvRow.Designacao = frm.txtDesignacaoListaModelos.Text;
					modAvRow.PrazoConservacao = System.Convert.ToInt16(frm.nudPrazoConservacao.Text);
					if (((int)frm.cbDestinoFinal.SelectedValue) < 0)
					{
						modAvRow.Preservar = false;
					}
					else
					{
						modAvRow.Preservar = System.Convert.ToBoolean(frm.cbDestinoFinal.SelectedValue);
					}
					modAvRow.Versao = new byte[]{};
					GisaDataSetHelper.GetInstance().ModelosAvaliacao.AddModelosAvaliacaoRow(modAvRow);

					ListViewItem item = NewModeloItem(modAvRow);
					LstVwModelos.Items.Add(item);
					break;
				case DialogResult.Cancel:
					return;
			}
		}

		private void btnEditModelo_Click(object sender, System.EventArgs e)
		{
			ListViewItem item = LstVwModelos.SelectedItems[0];
			GISADataset.ModelosAvaliacaoRow modAvRow = (GISADataset.ModelosAvaliacaoRow)item.Tag;
			FormModelosAvaliacao frm = new FormModelosAvaliacao();
			frm.Text = "Editar " + frm.Text;
			frm.txtDesignacaoListaModelos.Text = modAvRow.Designacao;
			frm.nudPrazoConservacao.Value = modAvRow.PrazoConservacao;
			if (modAvRow.IsPreservarNull())
			{
				frm.cbDestinoFinal.SelectedValue = -1;
			}
			else
			{
				frm.cbDestinoFinal.SelectedValue = modAvRow.Preservar;
			}
			switch (frm.ShowDialog())
			{
				case DialogResult.OK:
					if (! (modAvRow.RowState == DataRowState.Added))
					{
						GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
						try
						{
							bool result = false;
							bool preservar = false;
							if (frm.cbDestinoFinal.SelectedIndex > 0)
							{
								preservar = System.Convert.ToBoolean(frm.cbDestinoFinal.SelectedValue);
							}
							result = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.ManageModelosAvaliacao(false, modAvRow.ID, frm.txtDesignacaoListaModelos.Text, System.Convert.ToInt16(frm.nudPrazoConservacao.Text), preservar, ho.Connection);
							if (result)
							{
								modAvRow.Designacao = frm.txtDesignacaoListaModelos.Text;
								modAvRow.PrazoConservacao = System.Convert.ToInt16(frm.nudPrazoConservacao.Text);
								if (frm.cbDestinoFinal.SelectedIndex == 0)
								{
									modAvRow["Preservar"] = DBNull.Value;
								}
								else
								{
									modAvRow.Preservar = System.Convert.ToBoolean(frm.cbDestinoFinal.SelectedValue);
								}
								UpdateModeloItem(item, modAvRow);
								modAvRow.AcceptChanges();
							}
							else
							{
								MessageBox.Show("O modelo de avaliação selecionado não pode ser alterado uma vez que já está a ser utilizado na avaliação de níveis documentais.", "Editar Modelo de Avaliações", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							}
						}
						catch (Exception ex)
						{
							Trace.WriteLine(ex);
							MessageBox.Show("Não foi possível completar a operação. Tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						finally
						{
							ho.Dispose();
						}
					}
					else
					{
						modAvRow.Designacao = frm.txtDesignacaoListaModelos.Text;
						modAvRow.PrazoConservacao = System.Convert.ToInt16(frm.nudPrazoConservacao.Text);
						if (frm.cbDestinoFinal.SelectedIndex == 0)
						{
							modAvRow["Preservar"] = DBNull.Value;
						}
						else
						{
							modAvRow.Preservar = System.Convert.ToBoolean(frm.cbDestinoFinal.SelectedValue);
						}
						UpdateModeloItem(item, modAvRow);
					}
					break;
				case DialogResult.Cancel:
					return;
			}
		}

		private void btnRemoveModelo_Click(object sender, System.EventArgs e)
		{
			//não é permitido editar ou eliminar uma lista se algum dos seus modelos estiver a ser usado nalguma avaliação
			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
				bool result = false;
				GISADataset.ModelosAvaliacaoRow modAvRow = (GISADataset.ModelosAvaliacaoRow)(LstVwModelos.SelectedItems[0].Tag);
				result = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.ManageModelosAvaliacao(true, modAvRow.ID, null, short.MinValue, false, ho.Connection);
				if (result)
				{
					modAvRow.Delete();
					modAvRow.AcceptChanges();
					LstVwModelos.Items.Remove(LstVwModelos.SelectedItems[0]);
				}
				else
				{
					MessageBox.Show("O modelo de avaliação selecionado não pode ser apagado uma vez que já está a ser utilizado na avaliação de níveis documentais.", "Apagar Modelo de Avaliações", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				MessageBox.Show("Não foi possível completar a operação. Tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				ho.Dispose();
			}
		}

		private void UpdateToolBarButtonsModelos()
		{
			if (LstVwListaModelos.SelectedItems.Count > 0)
			{
				btnAddModelo.Enabled = true;
			}
			else
			{
				btnAddModelo.Enabled = false;
			}
			if (LstVwModelos.SelectedItems.Count > 0)
			{
				btnEditModelo.Enabled = true;
				btnRemoveModelo.Enabled = true;
			}
			else
			{
				btnEditModelo.Enabled = false;
				btnRemoveModelo.Enabled = false;
			}
		}

		private void LstVwModelos_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UpdateToolBarButtonsModelos();
		}

		private GISADataset.ListaModelosAvaliacaoRow GetMostRecentList()
		{
			GISADataset.ListaModelosAvaliacaoRow lstModAvRow = null;
			lstModAvRow = (GISADataset.ListaModelosAvaliacaoRow)(GisaDataSetHelper.GetInstance().ListaModelosAvaliacao.Select("", "DataInicio DESC")[0]);
			return lstModAvRow;
		}
	#endregion

        private void btnBackup_Click(object sender, EventArgs e)
        {
            SaveFileDialog mSaveDialog = new SaveFileDialog();
            mSaveDialog.FileName = "GISA_Backup_" + DateTime.Now.ToString("yyyyMMdd_HHmm");
            mSaveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            mSaveDialog.AddExtension = true;
            mSaveDialog.DefaultExt = "bak";
            mSaveDialog.Filter = "Backup (*.bak)|*.bak";
            mSaveDialog.OverwritePrompt = true;
            mSaveDialog.ValidateNames = true;

            switch (mSaveDialog.ShowDialog())
            {
                case DialogResult.OK:
                    mSaveDialog.InitialDirectory = new System.IO.FileInfo(mSaveDialog.FileName).Directory.ToString();

                    IDbConnection conn = GisaDataSetHelper.GetConnection();
                    try
                    {
                        conn.Open();
                        GisaInstallerRule.Current.ExecuteBackupDatabase(mSaveDialog.FileName, conn);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine("BACKUP ERROR: " + ex.ToString());
                        MessageBox.Show("Ocorreu um erro inesperado e a operação foi abortada.", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        conn.Close();
                    }

                    break;
                case DialogResult.Cancel:
                    break;
            }
        }
	}
}