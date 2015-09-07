using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using GISA.SharedResources;

using GISA.Controls.Localizacao;

namespace GISA
{
	public class MasterPanelPesquisaUF : GISA.MasterPanel
	{

	#region  Windows Form Designer generated code 

		public MasterPanelPesquisaUF() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();


			//Add any initialization after the InitializeComponent() call
            ToolBar.ButtonClick += ToolBar_ButtonClick;
            chkEstruturaArquivistica.CheckedChanged += chkEstruturaArquivistica_CheckedChanged;

			GetExtraResources();
			cdbDataEdicaoInicio.Year = System.DateTime.Now.Year;
			cdbDataEdicaoInicio.Month = System.DateTime.Now.Month;
			cdbDataEdicaoInicio.Day = System.DateTime.Now.Day;
			cdbDataEdicaoFim.Year = System.DateTime.Now.Year;
			cdbDataEdicaoFim.Month = System.DateTime.Now.Month;
			cdbDataEdicaoFim.Day = System.DateTime.Now.Day;
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

			cnList.grpLocalizacao.Text = string.Empty;
			cnList.LoadContents();
			cnList.Enabled = false;

            cnList.mTipoNivelRelLimitExcl = TipoNivelRelacionado.SSR;

            if (!SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsDepEnable())
            {
                //this.ufList.lstVwUnidadesFisicas.Columns.Remove(this.ufList.chUFEmDeposito);                
                this.chkFiltroUFsEliminadas.Visible = false;                
            }

			//AddHandlers()
			LoadContents();
		}

		private void LoadContents()
		{

			GISADataset.TipoAcondicionamentoRow all = (GisaDataSetHelper.GetInstance()).TipoAcondicionamento.NewTipoAcondicionamentoRow();

			all.ID = 0;
			all.Designacao = "Todos";
			DataRow[] DataRows = GisaDataSetHelper.GetInstance().TipoAcondicionamento.Select();
			GISADataset.TipoAcondicionamentoRow[] DataRowsEx = null;
            DataRowsEx = new GISADataset.TipoAcondicionamentoRow[DataRows.Length + 1];
			DataRowsEx[0] = all;
			Array.Copy(DataRows, 0, DataRowsEx, 1, DataRows.Length);
			this.cbTipoAcond.DataSource = DataRowsEx;
			this.cbTipoAcond.DisplayMember = "Designacao";
			this.cbTipoAcond.ValueMember = "ID";


			this.cbAssociacoes.SelectedItem = this.cbAssociacoes.Items[0];

		}

		public void ForceLoadContents()
		{
			LoadContents();
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


		internal System.Windows.Forms.ToolBarButton ToolBarButtonExecutar;
		internal System.Windows.Forms.TextBox txtConteudoInformacional;
		internal System.Windows.Forms.TextBox txtCota;
		internal System.Windows.Forms.TextBox txtCdReferencia;
		internal System.Windows.Forms.TextBox txtDesignacao;
		internal System.Windows.Forms.CheckBox chkEstruturaArquivistica;
		internal ControloNivelList cnList;
        internal System.Windows.Forms.Label lblDatasProducao;
		internal System.Windows.Forms.Label lblDesignacao;
		internal System.Windows.Forms.Label lblNumero;
		internal System.Windows.Forms.Label lblCota;
		internal System.Windows.Forms.Label lblConteudo;
		internal System.Windows.Forms.TextBox txtOperador;
		internal System.Windows.Forms.Label lblOperador;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label lblDataEdicaoInicio;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonLimpar;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonAjuda;
        internal GISA.Controls.PxCompleteDateBox cdbDataEdicaoInicio;
        internal GISA.Controls.PxCompleteDateBox cdbDataEdicaoFim;
        internal System.Windows.Forms.Label lblDataEdicaoFim;
		internal System.Windows.Forms.ComboBox cbTipoAcond;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.ComboBox cbAssociacoes;
        private GroupBox groupBox2;
        internal GISA.Controls.PxCompleteDateBox cdbInicioDoFim;
        internal Label label4;
        internal Label label5;
        internal GISA.Controls.PxCompleteDateBox cdbFimDoFim;
        private GroupBox groupBox1;
        internal GISA.Controls.PxCompleteDateBox cdbDataInicio;
        internal Label lblDataProducaoInicio;
        internal Label lblDataProducaoFim;
        internal GISA.Controls.PxCompleteDateBox cdbDataFim;
        internal Label lblCodigoBarras;
        internal TextBox txtCodigoBarras;
        internal TextBox txtGuiaIncorporacao;
        internal Label lblGuiaIncorporacao;
        internal CheckBox chkFiltroUFsEliminadas;
		internal System.Windows.Forms.Label Label2;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.chkFiltroUFsEliminadas = new System.Windows.Forms.CheckBox();
            this.txtGuiaIncorporacao = new System.Windows.Forms.TextBox();
            this.lblGuiaIncorporacao = new System.Windows.Forms.Label();
            this.txtCodigoBarras = new System.Windows.Forms.TextBox();
            this.lblCodigoBarras = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cdbInicioDoFim = new GISA.Controls.PxCompleteDateBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cdbFimDoFim = new GISA.Controls.PxCompleteDateBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cdbDataInicio = new GISA.Controls.PxCompleteDateBox();
            this.lblDataProducaoInicio = new System.Windows.Forms.Label();
            this.lblDataProducaoFim = new System.Windows.Forms.Label();
            this.cdbDataFim = new GISA.Controls.PxCompleteDateBox();
            this.cbTipoAcond = new System.Windows.Forms.ComboBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.cbAssociacoes = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.cdbDataEdicaoInicio = new GISA.Controls.PxCompleteDateBox();
            this.cdbDataEdicaoFim = new GISA.Controls.PxCompleteDateBox();
            this.lblDataEdicaoFim = new System.Windows.Forms.Label();
            this.lblDataEdicaoInicio = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtOperador = new System.Windows.Forms.TextBox();
            this.lblOperador = new System.Windows.Forms.Label();
            this.lblDatasProducao = new System.Windows.Forms.Label();
            this.txtDesignacao = new System.Windows.Forms.TextBox();
            this.lblDesignacao = new System.Windows.Forms.Label();
            this.txtCdReferencia = new System.Windows.Forms.TextBox();
            this.lblNumero = new System.Windows.Forms.Label();
            this.txtCota = new System.Windows.Forms.TextBox();
            this.lblCota = new System.Windows.Forms.Label();
            this.txtConteudoInformacional = new System.Windows.Forms.TextBox();
            this.lblConteudo = new System.Windows.Forms.Label();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.chkEstruturaArquivistica = new System.Windows.Forms.CheckBox();
            this.cnList = new ControloNivelList();
            this.ToolBarButtonExecutar = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonLimpar = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonAjuda = new System.Windows.Forms.ToolBarButton();
            this.pnlToolbarPadding.SuspendLayout();
            this.TabControl1.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.TabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFuncao
            // 
            this.lblFuncao.Location = new System.Drawing.Point(0, 0);
            this.lblFuncao.Size = new System.Drawing.Size(800, 24);
            this.lblFuncao.Text = "Pesquisa";
            // 
            // ToolBar
            // 
            this.ToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.ToolBarButtonExecutar,
            this.ToolBarButtonLimpar,
            this.ToolBarButtonAjuda});
            this.ToolBar.ImageList = null;
            this.ToolBar.Size = new System.Drawing.Size(3624, 26);
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
            this.pnlToolbarPadding.Size = new System.Drawing.Size(800, 28);
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Controls.Add(this.TabPage3);
            this.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl1.Location = new System.Drawing.Point(0, 52);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(800, 403);
            this.TabControl1.TabIndex = 1;
            // 
            // TabPage2
            // 
            this.TabPage2.AutoScroll = true;
            this.TabPage2.Controls.Add(this.chkFiltroUFsEliminadas);
            this.TabPage2.Controls.Add(this.txtGuiaIncorporacao);
            this.TabPage2.Controls.Add(this.lblGuiaIncorporacao);
            this.TabPage2.Controls.Add(this.txtCodigoBarras);
            this.TabPage2.Controls.Add(this.lblCodigoBarras);
            this.TabPage2.Controls.Add(this.groupBox2);
            this.TabPage2.Controls.Add(this.groupBox1);
            this.TabPage2.Controls.Add(this.cbTipoAcond);
            this.TabPage2.Controls.Add(this.Label3);
            this.TabPage2.Controls.Add(this.cbAssociacoes);
            this.TabPage2.Controls.Add(this.Label2);
            this.TabPage2.Controls.Add(this.cdbDataEdicaoInicio);
            this.TabPage2.Controls.Add(this.cdbDataEdicaoFim);
            this.TabPage2.Controls.Add(this.lblDataEdicaoFim);
            this.TabPage2.Controls.Add(this.lblDataEdicaoInicio);
            this.TabPage2.Controls.Add(this.Label1);
            this.TabPage2.Controls.Add(this.txtOperador);
            this.TabPage2.Controls.Add(this.lblOperador);
            this.TabPage2.Controls.Add(this.lblDatasProducao);
            this.TabPage2.Controls.Add(this.txtDesignacao);
            this.TabPage2.Controls.Add(this.lblDesignacao);
            this.TabPage2.Controls.Add(this.txtCdReferencia);
            this.TabPage2.Controls.Add(this.lblNumero);
            this.TabPage2.Controls.Add(this.txtCota);
            this.TabPage2.Controls.Add(this.lblCota);
            this.TabPage2.Controls.Add(this.txtConteudoInformacional);
            this.TabPage2.Controls.Add(this.lblConteudo);
            this.TabPage2.Location = new System.Drawing.Point(4, 22);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Size = new System.Drawing.Size(792, 377);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Descrição";
            // 
            // chkFiltroUFsEliminadas
            // 
            this.chkFiltroUFsEliminadas.AutoSize = true;
            this.chkFiltroUFsEliminadas.Checked = false;
            this.chkFiltroUFsEliminadas.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this.chkFiltroUFsEliminadas.Location = new System.Drawing.Point(398, 10);
            this.chkFiltroUFsEliminadas.Name = "chkFiltroUFsEliminadas";
            this.chkFiltroUFsEliminadas.Size = new System.Drawing.Size(214, 17);
            this.chkFiltroUFsEliminadas.TabIndex = 99;
            this.chkFiltroUFsEliminadas.Text = "Incluir unidades físicas eliminadas";
            this.chkFiltroUFsEliminadas.UseVisualStyleBackColor = true;
            // 
            // txtGuiaIncorporacao
            // 
            this.txtGuiaIncorporacao.Location = new System.Drawing.Point(128, 329);
            this.txtGuiaIncorporacao.MaxLength = 2000;
            this.txtGuiaIncorporacao.Name = "txtGuiaIncorporacao";
            this.txtGuiaIncorporacao.Size = new System.Drawing.Size(568, 20);
            this.txtGuiaIncorporacao.TabIndex = 97;
            this.txtGuiaIncorporacao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterPanelPesquisaUF_KeyDown);
            // 
            // lblGuiaIncorporacao
            // 
            this.lblGuiaIncorporacao.Location = new System.Drawing.Point(8, 326);
            this.lblGuiaIncorporacao.Name = "lblGuiaIncorporacao";
            this.lblGuiaIncorporacao.Size = new System.Drawing.Size(128, 24);
            this.lblGuiaIncorporacao.TabIndex = 98;
            this.lblGuiaIncorporacao.Text = "Guia de incorporação";
            this.lblGuiaIncorporacao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCodigoBarras
            // 
            this.txtCodigoBarras.Location = new System.Drawing.Point(128, 86);
            this.txtCodigoBarras.MaxLength = 2000;
            this.txtCodigoBarras.Name = "txtCodigoBarras";
            this.txtCodigoBarras.Size = new System.Drawing.Size(252, 20);
            this.txtCodigoBarras.TabIndex = 96;
            this.txtCodigoBarras.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterPanelPesquisaUF_KeyDown);
            // 
            // lblCodigoBarras
            // 
            this.lblCodigoBarras.Location = new System.Drawing.Point(8, 83);
            this.lblCodigoBarras.Name = "lblCodigoBarras";
            this.lblCodigoBarras.Size = new System.Drawing.Size(128, 24);
            this.lblCodigoBarras.TabIndex = 95;
            this.lblCodigoBarras.Text = "Código de barras:";
            this.lblCodigoBarras.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cdbInicioDoFim);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cdbFimDoFim);
            this.groupBox2.Location = new System.Drawing.Point(351, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(217, 77);
            this.groupBox2.TabIndex = 94;
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
            this.cdbInicioDoFim.Size = new System.Drawing.Size(160, 22);
            this.cdbInicioDoFim.TabIndex = 9;
            this.cdbInicioDoFim.Year = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 24);
            this.label4.TabIndex = 12;
            this.label4.Text = "entre";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(11, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 24);
            this.label5.TabIndex = 14;
            this.label5.Text = "e";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cdbFimDoFim
            // 
            this.cdbFimDoFim.Checked = false;
            this.cdbFimDoFim.Day = 1;
            this.cdbFimDoFim.Location = new System.Drawing.Point(47, 47);
            this.cdbFimDoFim.Month = 1;
            this.cdbFimDoFim.Name = "cdbFimDoFim";
            this.cdbFimDoFim.Size = new System.Drawing.Size(160, 22);
            this.cdbFimDoFim.TabIndex = 10;
            this.cdbFimDoFim.Year = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cdbDataInicio);
            this.groupBox1.Controls.Add(this.lblDataProducaoInicio);
            this.groupBox1.Controls.Add(this.lblDataProducaoFim);
            this.groupBox1.Controls.Add(this.cdbDataFim);
            this.groupBox1.Location = new System.Drawing.Point(128, 112);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 77);
            this.groupBox1.TabIndex = 93;
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
            this.cdbDataInicio.Size = new System.Drawing.Size(160, 22);
            this.cdbDataInicio.TabIndex = 7;
            this.cdbDataInicio.Year = 1;
            // 
            // lblDataProducaoInicio
            // 
            this.lblDataProducaoInicio.Location = new System.Drawing.Point(11, 19);
            this.lblDataProducaoInicio.Name = "lblDataProducaoInicio";
            this.lblDataProducaoInicio.Size = new System.Drawing.Size(44, 24);
            this.lblDataProducaoInicio.TabIndex = 12;
            this.lblDataProducaoInicio.Text = "entre";
            this.lblDataProducaoInicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDataProducaoFim
            // 
            this.lblDataProducaoFim.Location = new System.Drawing.Point(11, 43);
            this.lblDataProducaoFim.Name = "lblDataProducaoFim";
            this.lblDataProducaoFim.Size = new System.Drawing.Size(16, 24);
            this.lblDataProducaoFim.TabIndex = 14;
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
            this.cdbDataFim.Size = new System.Drawing.Size(160, 22);
            this.cdbDataFim.TabIndex = 8;
            this.cdbDataFim.Year = 1;
            // 
            // cbTipoAcond
            // 
            this.cbTipoAcond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoAcond.Location = new System.Drawing.Point(128, 221);
            this.cbTipoAcond.Name = "cbTipoAcond";
            this.cbTipoAcond.Size = new System.Drawing.Size(252, 21);
            this.cbTipoAcond.TabIndex = 92;
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(8, 218);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(128, 24);
            this.Label3.TabIndex = 91;
            this.Label3.Text = "Tipo de Unidade Fisica:";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbAssociacoes
            // 
            this.cbAssociacoes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAssociacoes.Items.AddRange(new object[] {
            "Todas",
            "Com unidades informacionais",
            "Sem unidades informacionais"});
            this.cbAssociacoes.Location = new System.Drawing.Point(128, 302);
            this.cbAssociacoes.Name = "cbAssociacoes";
            this.cbAssociacoes.Size = new System.Drawing.Size(252, 21);
            this.cbAssociacoes.TabIndex = 89;
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(8, 299);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(128, 24);
            this.Label2.TabIndex = 90;
            this.Label2.Text = "Unidades informacionais:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cdbDataEdicaoInicio
            // 
            this.cdbDataEdicaoInicio.Checked = false;
            this.cdbDataEdicaoInicio.Day = 1;
            this.cdbDataEdicaoInicio.Location = new System.Drawing.Point(164, 274);
            this.cdbDataEdicaoInicio.Month = 1;
            this.cdbDataEdicaoInicio.Name = "cdbDataEdicaoInicio";
            this.cdbDataEdicaoInicio.Size = new System.Drawing.Size(171, 22);
            this.cdbDataEdicaoInicio.TabIndex = 80;
            this.cdbDataEdicaoInicio.Year = 1;
            // 
            // cdbDataEdicaoFim
            // 
            this.cdbDataEdicaoFim.Checked = false;
            this.cdbDataEdicaoFim.Day = 1;
            this.cdbDataEdicaoFim.Location = new System.Drawing.Point(365, 273);
            this.cdbDataEdicaoFim.Month = 1;
            this.cdbDataEdicaoFim.Name = "cdbDataEdicaoFim";
            this.cdbDataEdicaoFim.Size = new System.Drawing.Size(166, 22);
            this.cdbDataEdicaoFim.TabIndex = 79;
            this.cdbDataEdicaoFim.Year = 1;
            // 
            // lblDataEdicaoFim
            // 
            this.lblDataEdicaoFim.Location = new System.Drawing.Point(341, 271);
            this.lblDataEdicaoFim.Name = "lblDataEdicaoFim";
            this.lblDataEdicaoFim.Size = new System.Drawing.Size(16, 24);
            this.lblDataEdicaoFim.TabIndex = 76;
            this.lblDataEdicaoFim.Text = "e";
            this.lblDataEdicaoFim.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDataEdicaoInicio
            // 
            this.lblDataEdicaoInicio.Location = new System.Drawing.Point(125, 271);
            this.lblDataEdicaoInicio.Name = "lblDataEdicaoInicio";
            this.lblDataEdicaoInicio.Size = new System.Drawing.Size(44, 24);
            this.lblDataEdicaoInicio.TabIndex = 62;
            this.lblDataEdicaoInicio.Text = "entre";
            this.lblDataEdicaoInicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(8, 272);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(128, 24);
            this.Label1.TabIndex = 61;
            this.Label1.Text = "Data de edição:";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOperador
            // 
            this.txtOperador.Location = new System.Drawing.Point(128, 248);
            this.txtOperador.MaxLength = 2000;
            this.txtOperador.Name = "txtOperador";
            this.txtOperador.Size = new System.Drawing.Size(252, 20);
            this.txtOperador.TabIndex = 8;
            this.txtOperador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterPanelPesquisaUF_KeyDown);
            // 
            // lblOperador
            // 
            this.lblOperador.Location = new System.Drawing.Point(8, 245);
            this.lblOperador.Name = "lblOperador";
            this.lblOperador.Size = new System.Drawing.Size(128, 24);
            this.lblOperador.TabIndex = 55;
            this.lblOperador.Text = "Operador / Grupo:";
            this.lblOperador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDatasProducao
            // 
            this.lblDatasProducao.Location = new System.Drawing.Point(8, 128);
            this.lblDatasProducao.Name = "lblDatasProducao";
            this.lblDatasProducao.Size = new System.Drawing.Size(128, 24);
            this.lblDatasProducao.TabIndex = 54;
            this.lblDatasProducao.Text = "Data de produção:";
            this.lblDatasProducao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDesignacao
            // 
            this.txtDesignacao.Location = new System.Drawing.Point(128, 34);
            this.txtDesignacao.MaxLength = 2000;
            this.txtDesignacao.Name = "txtDesignacao";
            this.txtDesignacao.Size = new System.Drawing.Size(568, 20);
            this.txtDesignacao.TabIndex = 3;
            this.txtDesignacao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterPanelPesquisaUF_KeyDown);
            // 
            // lblDesignacao
            // 
            this.lblDesignacao.Location = new System.Drawing.Point(8, 30);
            this.lblDesignacao.Name = "lblDesignacao";
            this.lblDesignacao.Size = new System.Drawing.Size(128, 24);
            this.lblDesignacao.TabIndex = 48;
            this.lblDesignacao.Text = "Título:";
            this.lblDesignacao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCdReferencia
            // 
            this.txtCdReferencia.Location = new System.Drawing.Point(128, 8);
            this.txtCdReferencia.MaxLength = 2000;
            this.txtCdReferencia.Name = "txtCdReferencia";
            this.txtCdReferencia.Size = new System.Drawing.Size(252, 20);
            this.txtCdReferencia.TabIndex = 2;
            this.txtCdReferencia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterPanelPesquisaUF_KeyDown);
            // 
            // lblNumero
            // 
            this.lblNumero.Location = new System.Drawing.Point(8, 5);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(128, 24);
            this.lblNumero.TabIndex = 46;
            this.lblNumero.Text = "Código parcial:";
            this.lblNumero.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCota
            // 
            this.txtCota.Location = new System.Drawing.Point(128, 60);
            this.txtCota.MaxLength = 2000;
            this.txtCota.Name = "txtCota";
            this.txtCota.Size = new System.Drawing.Size(568, 20);
            this.txtCota.TabIndex = 4;
            this.txtCota.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterPanelPesquisaUF_KeyDown);
            // 
            // lblCota
            // 
            this.lblCota.Location = new System.Drawing.Point(8, 57);
            this.lblCota.Name = "lblCota";
            this.lblCota.Size = new System.Drawing.Size(128, 24);
            this.lblCota.TabIndex = 44;
            this.lblCota.Text = "Cota:";
            this.lblCota.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtConteudoInformacional
            // 
            this.txtConteudoInformacional.Location = new System.Drawing.Point(128, 195);
            this.txtConteudoInformacional.MaxLength = 2000;
            this.txtConteudoInformacional.Name = "txtConteudoInformacional";
            this.txtConteudoInformacional.Size = new System.Drawing.Size(568, 20);
            this.txtConteudoInformacional.TabIndex = 7;
            this.txtConteudoInformacional.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterPanelPesquisaUF_KeyDown);
            // 
            // lblConteudo
            // 
            this.lblConteudo.Location = new System.Drawing.Point(8, 192);
            this.lblConteudo.Name = "lblConteudo";
            this.lblConteudo.Size = new System.Drawing.Size(128, 24);
            this.lblConteudo.TabIndex = 21;
            this.lblConteudo.Text = "Conteúdo informacional:";
            this.lblConteudo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TabPage3
            // 
            this.TabPage3.AutoScroll = true;
            this.TabPage3.Controls.Add(this.chkEstruturaArquivistica);
            this.TabPage3.Controls.Add(this.cnList);
            this.TabPage3.Location = new System.Drawing.Point(4, 22);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(792, 377);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Associações";
            // 
            // chkEstruturaArquivistica
            // 
            this.chkEstruturaArquivistica.Location = new System.Drawing.Point(16, 0);
            this.chkEstruturaArquivistica.Name = "chkEstruturaArquivistica";
            this.chkEstruturaArquivistica.Size = new System.Drawing.Size(72, 16);
            this.chkEstruturaArquivistica.TabIndex = 11;
            this.chkEstruturaArquivistica.Text = "Estrutura";
            // 
            // cnList
            // 
            this.cnList.Dock = System.Windows.Forms.DockStyle.Top;
            this.cnList.Location = new System.Drawing.Point(0, 0);
            this.cnList.Name = "cnList";
            this.cnList.Size = new System.Drawing.Size(775, 458);
            this.cnList.TabIndex = 12;
            // 
            // ToolBarButtonExecutar
            // 
            this.ToolBarButtonExecutar.Name = "ToolBarButtonExecutar";
            // 
            // ToolBarButtonLimpar
            // 
            this.ToolBarButtonLimpar.Name = "ToolBarButtonLimpar";
            // 
            // ToolBarButtonAjuda
            // 
            this.ToolBarButtonAjuda.Name = "ToolBarButtonAjuda";
            // 
            // MasterPanelPesquisaUF
            // 
            this.Controls.Add(this.TabControl1);
            this.Name = "MasterPanelPesquisaUF";
            this.Size = new System.Drawing.Size(800, 455);
            this.Controls.SetChildIndex(this.lblFuncao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.Controls.SetChildIndex(this.TabControl1, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.TabControl1.ResumeLayout(false);
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.TabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

		private void GetExtraResources()
		{
			ToolBar.ImageList = SharedResourcesOld.CurrentSharedResources.PesquisarImageList;
			ToolBarButtonExecutar.ImageIndex = 0;
			ToolBarButtonExecutar.ToolTipText = SharedResourcesOld.CurrentSharedResources.ExecutarPesquisaString;
			ToolBarButtonLimpar.ImageIndex = 1;
			ToolBarButtonLimpar.ToolTipText = SharedResourcesOld.CurrentSharedResources.LimparPesquisaString;
			ToolBarButtonAjuda.ImageIndex = 2;
			ToolBarButtonAjuda.ToolTipText = SharedResourcesOld.CurrentSharedResources.AjudaString;
		}

		public delegate void ExecuteQueryEventHandler(MasterPanelPesquisaUF MasterPanel);
		public event ExecuteQueryEventHandler ExecuteQuery;
		public delegate void ClearSearchResultsEventHandler(MasterPanelPesquisaUF MasterPanel);
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
                helpFileStream = gisaAssembly.GetManifestResourceStream(gisaAssembly.GetName().Name + ".DocumentosAjuda.AjudaPesquisaUnidadesFisicas.rtf");
				if (helpFileStream != null)
				{
					form.rtbText.LoadFile(helpFileStream, RichTextBoxStreamType.RichText);
					helpFileStream.Close();
				}
				form.ShowDialog();
			}
		}

		private void clearFields()
		{
			txtCdReferencia.Clear();
			txtDesignacao.Clear();
			txtCota.Clear();
            txtCodigoBarras.Clear();
            cdbDataInicio.Checked = false;
			cdbDataInicio.UpdateDate();
			cdbDataFim.Checked = false;
			cdbDataFim.UpdateDate();
            cdbInicioDoFim.Checked = false;
            cdbInicioDoFim.UpdateDate();
            cdbFimDoFim.Checked = false;
            cdbFimDoFim.UpdateDate();			            
            cdbDataEdicaoInicio.Checked = false;
			cdbDataEdicaoInicio.UpdateDate();
			cdbDataEdicaoFim.Checked = false;
			cdbDataEdicaoFim.UpdateDate();
			txtConteudoInformacional.Clear();
			txtOperador.Clear();
			chkEstruturaArquivistica.Checked = false;
			cbTipoAcond.SelectedIndex = 0;
			cbAssociacoes.SelectedIndex = 0;
            txtGuiaIncorporacao.Clear();
            chkFiltroUFsEliminadas.Checked = false;
		}

		private void chkEstruturaArquivistica_CheckedChanged(object sender, System.EventArgs e)
		{
			cnList.Enabled = chkEstruturaArquivistica.Checked;
		}

        private void MasterPanelPesquisaUF_KeyDown(object sender, KeyEventArgs e)
        {
            enterKeyPressed(e);
        }

        private void enterKeyPressed(KeyEventArgs e)
        {
            if (e.KeyValue == Convert.ToInt32(Keys.Enter))
            {
                if (ExecuteQuery != null)
                    ExecuteQuery(this);
            }
        }
	}
}