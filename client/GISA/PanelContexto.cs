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
using GISA.GUIHelper;

namespace GISA
{
	public class PanelContexto : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelContexto() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            btnEdit.Click += btnEdit_Click;
            btnRemove.Click += btnRemoveProdutor_Click;
            lstVwProdutores.KeyUp += lstVwProdutores_KeyUp;
            lstVwProdutores.SelectedIndexChanged += lstVwProdutores_SelectedIndexChanged;

            button1.Click += btnRemoveAutor_Click;
            lstVwAutor.KeyUp += lstVwAutores_KeyUp;
            lstVwAutor.SelectedIndexChanged += lstVwAutores_SelectedIndexChanged;

			GetExtraResources();
			UpdateButtonsState();
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
		internal System.Windows.Forms.TabPage tabHistAdministrativa;
		internal System.Windows.Forms.GroupBox GrpBoxHistorAdminist;
		internal System.Windows.Forms.RadioButton rbSerieFechada;
		internal System.Windows.Forms.RadioButton rbSerieAberta;
		internal System.Windows.Forms.TextBox txtHistoriaAdministrativaBibliografica;
		internal System.Windows.Forms.TabPage tabHistArquivistica;
		internal System.Windows.Forms.GroupBox GrpBoxHistArquivista;
		internal System.Windows.Forms.TextBox txtHistoriaArquivista;
		internal System.Windows.Forms.TabPage tabFonteImediataAquisTransf;
		internal System.Windows.Forms.GroupBox GrpBoxFonteImediata;
		internal System.Windows.Forms.TextBox txtFonteImediataAquisicTransf;
		internal System.Windows.Forms.TabPage tabFluxograma;
		internal System.Windows.Forms.GroupBox GrpBoxFluxograma;
		internal System.Windows.Forms.TabControl TabControl;
		internal System.Windows.Forms.GroupBox grpProdutores;
		internal System.Windows.Forms.ListView lstVwProdutores;
		internal System.Windows.Forms.ColumnHeader clnDesignacao;
		internal System.Windows.Forms.ColumnHeader clnValidado;
		internal System.Windows.Forms.ColumnHeader clnCompleto;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Button btnRemove;
		internal System.Windows.Forms.Button btnEdit;
		internal System.Windows.Forms.TabPage tabDescricaoEPs;
		internal System.Windows.Forms.GroupBox grpDescricoesEPs;
		internal System.Windows.Forms.TextBox txtDescricoesEPs;
		internal System.Windows.Forms.TabControl TabControlHistoriaAdministrativa;
		internal System.Windows.Forms.TabPage tabDatasExistencia;
		internal System.Windows.Forms.TextBox txtDataExistencia;
		internal System.Windows.Forms.TabPage tabHistoria;
		internal System.Windows.Forms.TextBox txtHistoria;
		internal System.Windows.Forms.TabPage tabZonaGeografica;
		internal System.Windows.Forms.TextBox txtZonaGeografica;
		internal System.Windows.Forms.TabPage tabEstatutoLegal;
		internal System.Windows.Forms.TextBox txtEstatutoLegal;
		internal System.Windows.Forms.TabPage tabFuncoesOcupacActividades;
		internal System.Windows.Forms.TextBox txtFuncoesOcupacoesActividades;
		internal System.Windows.Forms.TabPage tabEnquadramentoLegal;
		internal System.Windows.Forms.TextBox txtEnquadramentoLegal;
		internal System.Windows.Forms.TabPage tabEstruturaInterna;
		internal System.Windows.Forms.TextBox txtEstruturaInterna;
		internal System.Windows.Forms.TabPage tabContextoGeral;
		internal System.Windows.Forms.TextBox txtContextoGeral;
		internal System.Windows.Forms.TabPage tabOutraInforRelevante;
		internal System.Windows.Forms.TextBox txtOutraInformRelevante;
		internal System.Windows.Forms.ColumnHeader colDatasExistencia;
        private GroupBox grpAutor;
        internal ListView lstVwAutor;
        internal ColumnHeader chAutorDesignacao;
        internal Button button1;
        private Panel panel1;
        private Panel panel2;
		internal System.Windows.Forms.ColumnHeader colDatasRelacao;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.tabFluxograma = new System.Windows.Forms.TabPage();
            this.GrpBoxFluxograma = new System.Windows.Forms.GroupBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.grpAutor = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lstVwAutor = new System.Windows.Forms.ListView();
            this.chAutorDesignacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpProdutores = new System.Windows.Forms.GroupBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lstVwProdutores = new System.Windows.Forms.ListView();
            this.clnDesignacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clnValidado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clnCompleto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDatasExistencia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDatasRelacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tabHistAdministrativa = new System.Windows.Forms.TabPage();
            this.TabControlHistoriaAdministrativa = new System.Windows.Forms.TabControl();
            this.tabDatasExistencia = new System.Windows.Forms.TabPage();
            this.txtDataExistencia = new System.Windows.Forms.TextBox();
            this.tabHistoria = new System.Windows.Forms.TabPage();
            this.txtHistoria = new System.Windows.Forms.TextBox();
            this.tabZonaGeografica = new System.Windows.Forms.TabPage();
            this.txtZonaGeografica = new System.Windows.Forms.TextBox();
            this.tabEstatutoLegal = new System.Windows.Forms.TabPage();
            this.txtEstatutoLegal = new System.Windows.Forms.TextBox();
            this.tabFuncoesOcupacActividades = new System.Windows.Forms.TabPage();
            this.txtFuncoesOcupacoesActividades = new System.Windows.Forms.TextBox();
            this.tabEnquadramentoLegal = new System.Windows.Forms.TabPage();
            this.txtEnquadramentoLegal = new System.Windows.Forms.TextBox();
            this.tabEstruturaInterna = new System.Windows.Forms.TabPage();
            this.txtEstruturaInterna = new System.Windows.Forms.TextBox();
            this.tabContextoGeral = new System.Windows.Forms.TabPage();
            this.txtContextoGeral = new System.Windows.Forms.TextBox();
            this.tabOutraInforRelevante = new System.Windows.Forms.TabPage();
            this.txtOutraInformRelevante = new System.Windows.Forms.TextBox();
            this.GrpBoxHistorAdminist = new System.Windows.Forms.GroupBox();
            this.txtHistoriaAdministrativaBibliografica = new System.Windows.Forms.TextBox();
            this.tabHistArquivistica = new System.Windows.Forms.TabPage();
            this.GrpBoxHistArquivista = new System.Windows.Forms.GroupBox();
            this.txtHistoriaArquivista = new System.Windows.Forms.TextBox();
            this.tabFonteImediataAquisTransf = new System.Windows.Forms.TabPage();
            this.GrpBoxFonteImediata = new System.Windows.Forms.GroupBox();
            this.txtFonteImediataAquisicTransf = new System.Windows.Forms.TextBox();
            this.tabDescricaoEPs = new System.Windows.Forms.TabPage();
            this.grpDescricoesEPs = new System.Windows.Forms.GroupBox();
            this.txtDescricoesEPs = new System.Windows.Forms.TextBox();
            this.rbSerieFechada = new System.Windows.Forms.RadioButton();
            this.rbSerieAberta = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabFluxograma.SuspendLayout();
            this.GrpBoxFluxograma.SuspendLayout();
            this.grpAutor.SuspendLayout();
            this.grpProdutores.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.tabHistAdministrativa.SuspendLayout();
            this.TabControlHistoriaAdministrativa.SuspendLayout();
            this.tabDatasExistencia.SuspendLayout();
            this.tabHistoria.SuspendLayout();
            this.tabZonaGeografica.SuspendLayout();
            this.tabEstatutoLegal.SuspendLayout();
            this.tabFuncoesOcupacActividades.SuspendLayout();
            this.tabEnquadramentoLegal.SuspendLayout();
            this.tabEstruturaInterna.SuspendLayout();
            this.tabContextoGeral.SuspendLayout();
            this.tabOutraInforRelevante.SuspendLayout();
            this.GrpBoxHistorAdminist.SuspendLayout();
            this.tabHistArquivistica.SuspendLayout();
            this.GrpBoxHistArquivista.SuspendLayout();
            this.tabFonteImediataAquisTransf.SuspendLayout();
            this.GrpBoxFonteImediata.SuspendLayout();
            this.tabDescricaoEPs.SuspendLayout();
            this.grpDescricoesEPs.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabFluxograma
            // 
            this.tabFluxograma.Controls.Add(this.GrpBoxFluxograma);
            this.tabFluxograma.Location = new System.Drawing.Point(4, 22);
            this.tabFluxograma.Name = "tabFluxograma";
            this.tabFluxograma.Size = new System.Drawing.Size(678, 238);
            this.tabFluxograma.TabIndex = 2;
            this.tabFluxograma.Text = "2.*. Fluxograma";
            // 
            // GrpBoxFluxograma
            // 
            this.GrpBoxFluxograma.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpBoxFluxograma.Controls.Add(this.Label1);
            this.GrpBoxFluxograma.Location = new System.Drawing.Point(0, 0);
            this.GrpBoxFluxograma.Name = "GrpBoxFluxograma";
            this.GrpBoxFluxograma.Size = new System.Drawing.Size(678, 239);
            this.GrpBoxFluxograma.TabIndex = 1;
            this.GrpBoxFluxograma.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            this.Label1.Location = new System.Drawing.Point(4, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(670, 226);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Não está ainda disponível nesta versão.";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpAutor
            // 
            this.grpAutor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAutor.Controls.Add(this.button1);
            this.grpAutor.Controls.Add(this.lstVwAutor);
            this.grpAutor.Location = new System.Drawing.Point(7, 3);
            this.grpAutor.Name = "grpAutor";
            this.grpAutor.Size = new System.Drawing.Size(586, 104);
            this.grpAutor.TabIndex = 6;
            this.grpAutor.TabStop = false;
            this.grpAutor.Text = "2.*. Autor(es)";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(555, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 24);
            this.button1.TabIndex = 4;
            // 
            // lstVwAutor
            // 
            this.lstVwAutor.AllowDrop = true;
            this.lstVwAutor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwAutor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAutorDesignacao});
            this.lstVwAutor.FullRowSelect = true;
            this.lstVwAutor.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstVwAutor.HideSelection = false;
            this.lstVwAutor.Location = new System.Drawing.Point(8, 19);
            this.lstVwAutor.Name = "lstVwAutor";
            this.lstVwAutor.Size = new System.Drawing.Size(541, 79);
            this.lstVwAutor.TabIndex = 2;
            this.lstVwAutor.UseCompatibleStateImageBehavior = false;
            this.lstVwAutor.View = System.Windows.Forms.View.Details;
            // 
            // chAutorDesignacao
            // 
            this.chAutorDesignacao.Text = "Designação";
            this.chAutorDesignacao.Width = 690;
            // 
            // grpProdutores
            // 
            this.grpProdutores.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpProdutores.Controls.Add(this.btnEdit);
            this.grpProdutores.Controls.Add(this.btnRemove);
            this.grpProdutores.Controls.Add(this.lstVwProdutores);
            this.grpProdutores.Location = new System.Drawing.Point(7, 113);
            this.grpProdutores.Name = "grpProdutores";
            this.grpProdutores.Size = new System.Drawing.Size(586, 110);
            this.grpProdutores.TabIndex = 1;
            this.grpProdutores.TabStop = false;
            this.grpProdutores.Text = "2.1. Entidade(s) produtora(s)";
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEdit.Location = new System.Drawing.Point(555, 34);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(24, 24);
            this.btnEdit.TabIndex = 2;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(555, 63);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(24, 24);
            this.btnRemove.TabIndex = 3;
            // 
            // lstVwProdutores
            // 
            this.lstVwProdutores.AllowDrop = true;
            this.lstVwProdutores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwProdutores.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clnDesignacao,
            this.clnValidado,
            this.clnCompleto,
            this.colDatasExistencia,
            this.colDatasRelacao});
            this.lstVwProdutores.FullRowSelect = true;
            this.lstVwProdutores.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstVwProdutores.HideSelection = false;
            this.lstVwProdutores.Location = new System.Drawing.Point(8, 16);
            this.lstVwProdutores.Name = "lstVwProdutores";
            this.lstVwProdutores.Size = new System.Drawing.Size(541, 87);
            this.lstVwProdutores.TabIndex = 1;
            this.lstVwProdutores.UseCompatibleStateImageBehavior = false;
            this.lstVwProdutores.View = System.Windows.Forms.View.Details;
            // 
            // clnDesignacao
            // 
            this.clnDesignacao.Text = "Designação";
            this.clnDesignacao.Width = 275;
            // 
            // clnValidado
            // 
            this.clnValidado.Text = "Validado";
            this.clnValidado.Width = 98;
            // 
            // clnCompleto
            // 
            this.clnCompleto.Text = "Completo";
            this.clnCompleto.Width = 78;
            // 
            // colDatasExistencia
            // 
            this.colDatasExistencia.Text = "Datas de existência";
            this.colDatasExistencia.Width = 120;
            // 
            // colDatasRelacao
            // 
            this.colDatasRelacao.Text = "Datas da relação";
            this.colDatasRelacao.Width = 120;
            // 
            // TabControl
            // 
            this.TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl.Controls.Add(this.tabHistAdministrativa);
            this.TabControl.Controls.Add(this.tabHistArquivistica);
            this.TabControl.Controls.Add(this.tabFonteImediataAquisTransf);
            this.TabControl.Controls.Add(this.tabDescricaoEPs);
            this.TabControl.ItemSize = new System.Drawing.Size(189, 18);
            this.TabControl.Location = new System.Drawing.Point(7, 229);
            this.TabControl.Name = "TabControl";
            this.TabControl.Padding = new System.Drawing.Point(10, 3);
            this.TabControl.SelectedIndex = 0;
            this.TabControl.ShowToolTips = true;
            this.TabControl.Size = new System.Drawing.Size(586, 179);
            this.TabControl.TabIndex = 1;
            // 
            // tabHistAdministrativa
            // 
            this.tabHistAdministrativa.Controls.Add(this.TabControlHistoriaAdministrativa);
            this.tabHistAdministrativa.Controls.Add(this.GrpBoxHistorAdminist);
            this.tabHistAdministrativa.Location = new System.Drawing.Point(4, 22);
            this.tabHistAdministrativa.Name = "tabHistAdministrativa";
            this.tabHistAdministrativa.Size = new System.Drawing.Size(578, 153);
            this.tabHistAdministrativa.TabIndex = 3;
            this.tabHistAdministrativa.Text = "2.2. História administrativa / biográfica";
            this.tabHistAdministrativa.UseVisualStyleBackColor = true;
            // 
            // TabControlHistoriaAdministrativa
            // 
            this.TabControlHistoriaAdministrativa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControlHistoriaAdministrativa.Controls.Add(this.tabDatasExistencia);
            this.TabControlHistoriaAdministrativa.Controls.Add(this.tabHistoria);
            this.TabControlHistoriaAdministrativa.Controls.Add(this.tabZonaGeografica);
            this.TabControlHistoriaAdministrativa.Controls.Add(this.tabEstatutoLegal);
            this.TabControlHistoriaAdministrativa.Controls.Add(this.tabFuncoesOcupacActividades);
            this.TabControlHistoriaAdministrativa.Controls.Add(this.tabEnquadramentoLegal);
            this.TabControlHistoriaAdministrativa.Controls.Add(this.tabEstruturaInterna);
            this.TabControlHistoriaAdministrativa.Controls.Add(this.tabContextoGeral);
            this.TabControlHistoriaAdministrativa.Controls.Add(this.tabOutraInforRelevante);
            this.TabControlHistoriaAdministrativa.Location = new System.Drawing.Point(5, 11);
            this.TabControlHistoriaAdministrativa.Multiline = true;
            this.TabControlHistoriaAdministrativa.Name = "TabControlHistoriaAdministrativa";
            this.TabControlHistoriaAdministrativa.SelectedIndex = 0;
            this.TabControlHistoriaAdministrativa.Size = new System.Drawing.Size(567, 138);
            this.TabControlHistoriaAdministrativa.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.TabControlHistoriaAdministrativa.TabIndex = 2;
            // 
            // tabDatasExistencia
            // 
            this.tabDatasExistencia.Controls.Add(this.txtDataExistencia);
            this.tabDatasExistencia.Location = new System.Drawing.Point(4, 40);
            this.tabDatasExistencia.Name = "tabDatasExistencia";
            this.tabDatasExistencia.Size = new System.Drawing.Size(559, 94);
            this.tabDatasExistencia.TabIndex = 0;
            this.tabDatasExistencia.Text = "Datas de existência";
            // 
            // txtDataExistencia
            // 
            this.txtDataExistencia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataExistencia.Location = new System.Drawing.Point(0, 2);
            this.txtDataExistencia.MaxLength = 2147483646;
            this.txtDataExistencia.Multiline = true;
            this.txtDataExistencia.Name = "txtDataExistencia";
            this.txtDataExistencia.ReadOnly = true;
            this.txtDataExistencia.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDataExistencia.Size = new System.Drawing.Size(559, 92);
            this.txtDataExistencia.TabIndex = 0;
            // 
            // tabHistoria
            // 
            this.tabHistoria.Controls.Add(this.txtHistoria);
            this.tabHistoria.Location = new System.Drawing.Point(4, 40);
            this.tabHistoria.Name = "tabHistoria";
            this.tabHistoria.Size = new System.Drawing.Size(567, 94);
            this.tabHistoria.TabIndex = 1;
            this.tabHistoria.Text = "História";
            this.tabHistoria.Visible = false;
            // 
            // txtHistoria
            // 
            this.txtHistoria.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHistoria.Location = new System.Drawing.Point(0, 2);
            this.txtHistoria.MaxLength = 2147483646;
            this.txtHistoria.Multiline = true;
            this.txtHistoria.Name = "txtHistoria";
            this.txtHistoria.ReadOnly = true;
            this.txtHistoria.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHistoria.Size = new System.Drawing.Size(567, 92);
            this.txtHistoria.TabIndex = 1;
            // 
            // tabZonaGeografica
            // 
            this.tabZonaGeografica.Controls.Add(this.txtZonaGeografica);
            this.tabZonaGeografica.Location = new System.Drawing.Point(4, 40);
            this.tabZonaGeografica.Name = "tabZonaGeografica";
            this.tabZonaGeografica.Size = new System.Drawing.Size(567, 94);
            this.tabZonaGeografica.TabIndex = 2;
            this.tabZonaGeografica.Text = "Zona geográfica";
            this.tabZonaGeografica.Visible = false;
            // 
            // txtZonaGeografica
            // 
            this.txtZonaGeografica.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtZonaGeografica.Location = new System.Drawing.Point(0, 2);
            this.txtZonaGeografica.MaxLength = 2147483646;
            this.txtZonaGeografica.Multiline = true;
            this.txtZonaGeografica.Name = "txtZonaGeografica";
            this.txtZonaGeografica.ReadOnly = true;
            this.txtZonaGeografica.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtZonaGeografica.Size = new System.Drawing.Size(567, 92);
            this.txtZonaGeografica.TabIndex = 1;
            // 
            // tabEstatutoLegal
            // 
            this.tabEstatutoLegal.Controls.Add(this.txtEstatutoLegal);
            this.tabEstatutoLegal.Location = new System.Drawing.Point(4, 40);
            this.tabEstatutoLegal.Name = "tabEstatutoLegal";
            this.tabEstatutoLegal.Size = new System.Drawing.Size(567, 94);
            this.tabEstatutoLegal.TabIndex = 3;
            this.tabEstatutoLegal.Text = "Estatuto legal";
            this.tabEstatutoLegal.Visible = false;
            // 
            // txtEstatutoLegal
            // 
            this.txtEstatutoLegal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEstatutoLegal.Location = new System.Drawing.Point(0, 2);
            this.txtEstatutoLegal.MaxLength = 2147483646;
            this.txtEstatutoLegal.Multiline = true;
            this.txtEstatutoLegal.Name = "txtEstatutoLegal";
            this.txtEstatutoLegal.ReadOnly = true;
            this.txtEstatutoLegal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEstatutoLegal.Size = new System.Drawing.Size(567, 92);
            this.txtEstatutoLegal.TabIndex = 1;
            // 
            // tabFuncoesOcupacActividades
            // 
            this.tabFuncoesOcupacActividades.Controls.Add(this.txtFuncoesOcupacoesActividades);
            this.tabFuncoesOcupacActividades.Location = new System.Drawing.Point(4, 40);
            this.tabFuncoesOcupacActividades.Name = "tabFuncoesOcupacActividades";
            this.tabFuncoesOcupacActividades.Size = new System.Drawing.Size(567, 94);
            this.tabFuncoesOcupacActividades.TabIndex = 4;
            this.tabFuncoesOcupacActividades.Text = "Funções, ocupações e atividades";
            this.tabFuncoesOcupacActividades.Visible = false;
            // 
            // txtFuncoesOcupacoesActividades
            // 
            this.txtFuncoesOcupacoesActividades.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFuncoesOcupacoesActividades.Location = new System.Drawing.Point(0, 2);
            this.txtFuncoesOcupacoesActividades.MaxLength = 2147483646;
            this.txtFuncoesOcupacoesActividades.Multiline = true;
            this.txtFuncoesOcupacoesActividades.Name = "txtFuncoesOcupacoesActividades";
            this.txtFuncoesOcupacoesActividades.ReadOnly = true;
            this.txtFuncoesOcupacoesActividades.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFuncoesOcupacoesActividades.Size = new System.Drawing.Size(567, 92);
            this.txtFuncoesOcupacoesActividades.TabIndex = 1;
            // 
            // tabEnquadramentoLegal
            // 
            this.tabEnquadramentoLegal.Controls.Add(this.txtEnquadramentoLegal);
            this.tabEnquadramentoLegal.Location = new System.Drawing.Point(4, 40);
            this.tabEnquadramentoLegal.Name = "tabEnquadramentoLegal";
            this.tabEnquadramentoLegal.Size = new System.Drawing.Size(567, 94);
            this.tabEnquadramentoLegal.TabIndex = 5;
            this.tabEnquadramentoLegal.Text = "Enquadramento legal";
            this.tabEnquadramentoLegal.Visible = false;
            // 
            // txtEnquadramentoLegal
            // 
            this.txtEnquadramentoLegal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEnquadramentoLegal.Location = new System.Drawing.Point(0, 2);
            this.txtEnquadramentoLegal.MaxLength = 2147483646;
            this.txtEnquadramentoLegal.Multiline = true;
            this.txtEnquadramentoLegal.Name = "txtEnquadramentoLegal";
            this.txtEnquadramentoLegal.ReadOnly = true;
            this.txtEnquadramentoLegal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEnquadramentoLegal.Size = new System.Drawing.Size(567, 92);
            this.txtEnquadramentoLegal.TabIndex = 1;
            // 
            // tabEstruturaInterna
            // 
            this.tabEstruturaInterna.Controls.Add(this.txtEstruturaInterna);
            this.tabEstruturaInterna.Location = new System.Drawing.Point(4, 40);
            this.tabEstruturaInterna.Name = "tabEstruturaInterna";
            this.tabEstruturaInterna.Size = new System.Drawing.Size(567, 94);
            this.tabEstruturaInterna.TabIndex = 6;
            this.tabEstruturaInterna.Text = "Estrutura interna";
            this.tabEstruturaInterna.Visible = false;
            // 
            // txtEstruturaInterna
            // 
            this.txtEstruturaInterna.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEstruturaInterna.Location = new System.Drawing.Point(0, 2);
            this.txtEstruturaInterna.MaxLength = 2147483646;
            this.txtEstruturaInterna.Multiline = true;
            this.txtEstruturaInterna.Name = "txtEstruturaInterna";
            this.txtEstruturaInterna.ReadOnly = true;
            this.txtEstruturaInterna.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEstruturaInterna.Size = new System.Drawing.Size(567, 92);
            this.txtEstruturaInterna.TabIndex = 1;
            // 
            // tabContextoGeral
            // 
            this.tabContextoGeral.Controls.Add(this.txtContextoGeral);
            this.tabContextoGeral.Location = new System.Drawing.Point(4, 40);
            this.tabContextoGeral.Name = "tabContextoGeral";
            this.tabContextoGeral.Size = new System.Drawing.Size(567, 94);
            this.tabContextoGeral.TabIndex = 7;
            this.tabContextoGeral.Text = "Contexto geral";
            this.tabContextoGeral.Visible = false;
            // 
            // txtContextoGeral
            // 
            this.txtContextoGeral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContextoGeral.Location = new System.Drawing.Point(0, 2);
            this.txtContextoGeral.MaxLength = 2147483646;
            this.txtContextoGeral.Multiline = true;
            this.txtContextoGeral.Name = "txtContextoGeral";
            this.txtContextoGeral.ReadOnly = true;
            this.txtContextoGeral.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContextoGeral.Size = new System.Drawing.Size(567, 92);
            this.txtContextoGeral.TabIndex = 1;
            // 
            // tabOutraInforRelevante
            // 
            this.tabOutraInforRelevante.Controls.Add(this.txtOutraInformRelevante);
            this.tabOutraInforRelevante.Location = new System.Drawing.Point(4, 40);
            this.tabOutraInforRelevante.Name = "tabOutraInforRelevante";
            this.tabOutraInforRelevante.Size = new System.Drawing.Size(567, 94);
            this.tabOutraInforRelevante.TabIndex = 8;
            this.tabOutraInforRelevante.Text = "Outra informação relevante";
            this.tabOutraInforRelevante.Visible = false;
            // 
            // txtOutraInformRelevante
            // 
            this.txtOutraInformRelevante.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutraInformRelevante.Location = new System.Drawing.Point(0, 2);
            this.txtOutraInformRelevante.MaxLength = 2147483646;
            this.txtOutraInformRelevante.Multiline = true;
            this.txtOutraInformRelevante.Name = "txtOutraInformRelevante";
            this.txtOutraInformRelevante.ReadOnly = true;
            this.txtOutraInformRelevante.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutraInformRelevante.Size = new System.Drawing.Size(567, 92);
            this.txtOutraInformRelevante.TabIndex = 1;
            // 
            // GrpBoxHistorAdminist
            // 
            this.GrpBoxHistorAdminist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpBoxHistorAdminist.BackColor = System.Drawing.SystemColors.Control;
            this.GrpBoxHistorAdminist.Controls.Add(this.txtHistoriaAdministrativaBibliografica);
            this.GrpBoxHistorAdminist.Location = new System.Drawing.Point(0, 1);
            this.GrpBoxHistorAdminist.Name = "GrpBoxHistorAdminist";
            this.GrpBoxHistorAdminist.Size = new System.Drawing.Size(578, 152);
            this.GrpBoxHistorAdminist.TabIndex = 0;
            this.GrpBoxHistorAdminist.TabStop = false;
            // 
            // txtHistoriaAdministrativaBibliografica
            // 
            this.txtHistoriaAdministrativaBibliografica.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHistoriaAdministrativaBibliografica.Location = new System.Drawing.Point(5, 11);
            this.txtHistoriaAdministrativaBibliografica.Multiline = true;
            this.txtHistoriaAdministrativaBibliografica.Name = "txtHistoriaAdministrativaBibliografica";
            this.txtHistoriaAdministrativaBibliografica.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHistoriaAdministrativaBibliografica.Size = new System.Drawing.Size(567, 137);
            this.txtHistoriaAdministrativaBibliografica.TabIndex = 3;
            // 
            // tabHistArquivistica
            // 
            this.tabHistArquivistica.Controls.Add(this.GrpBoxHistArquivista);
            this.tabHistArquivistica.Location = new System.Drawing.Point(4, 22);
            this.tabHistArquivistica.Name = "tabHistArquivistica";
            this.tabHistArquivistica.Size = new System.Drawing.Size(586, 153);
            this.tabHistArquivistica.TabIndex = 0;
            this.tabHistArquivistica.Text = "2.3. História arquivística";
            this.tabHistArquivistica.UseVisualStyleBackColor = true;
            // 
            // GrpBoxHistArquivista
            // 
            this.GrpBoxHistArquivista.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpBoxHistArquivista.Controls.Add(this.txtHistoriaArquivista);
            this.GrpBoxHistArquivista.Location = new System.Drawing.Point(0, 1);
            this.GrpBoxHistArquivista.Name = "GrpBoxHistArquivista";
            this.GrpBoxHistArquivista.Size = new System.Drawing.Size(586, 152);
            this.GrpBoxHistArquivista.TabIndex = 1;
            this.GrpBoxHistArquivista.TabStop = false;
            // 
            // txtHistoriaArquivista
            // 
            this.txtHistoriaArquivista.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHistoriaArquivista.Location = new System.Drawing.Point(5, 11);
            this.txtHistoriaArquivista.Multiline = true;
            this.txtHistoriaArquivista.Name = "txtHistoriaArquivista";
            this.txtHistoriaArquivista.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHistoriaArquivista.Size = new System.Drawing.Size(575, 137);
            this.txtHistoriaArquivista.TabIndex = 3;
            // 
            // tabFonteImediataAquisTransf
            // 
            this.tabFonteImediataAquisTransf.Controls.Add(this.GrpBoxFonteImediata);
            this.tabFonteImediataAquisTransf.Location = new System.Drawing.Point(4, 22);
            this.tabFonteImediataAquisTransf.Name = "tabFonteImediataAquisTransf";
            this.tabFonteImediataAquisTransf.Size = new System.Drawing.Size(586, 153);
            this.tabFonteImediataAquisTransf.TabIndex = 1;
            this.tabFonteImediataAquisTransf.Text = "2.4. Fonte imediata de aquisição ou transferência";
            this.tabFonteImediataAquisTransf.UseVisualStyleBackColor = true;
            // 
            // GrpBoxFonteImediata
            // 
            this.GrpBoxFonteImediata.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpBoxFonteImediata.Controls.Add(this.txtFonteImediataAquisicTransf);
            this.GrpBoxFonteImediata.Location = new System.Drawing.Point(0, 1);
            this.GrpBoxFonteImediata.Name = "GrpBoxFonteImediata";
            this.GrpBoxFonteImediata.Size = new System.Drawing.Size(586, 152);
            this.GrpBoxFonteImediata.TabIndex = 1;
            this.GrpBoxFonteImediata.TabStop = false;
            // 
            // txtFonteImediataAquisicTransf
            // 
            this.txtFonteImediataAquisicTransf.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFonteImediataAquisicTransf.Location = new System.Drawing.Point(5, 11);
            this.txtFonteImediataAquisicTransf.Multiline = true;
            this.txtFonteImediataAquisicTransf.Name = "txtFonteImediataAquisicTransf";
            this.txtFonteImediataAquisicTransf.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFonteImediataAquisicTransf.Size = new System.Drawing.Size(575, 137);
            this.txtFonteImediataAquisicTransf.TabIndex = 3;
            // 
            // tabDescricaoEPs
            // 
            this.tabDescricaoEPs.Controls.Add(this.grpDescricoesEPs);
            this.tabDescricaoEPs.Location = new System.Drawing.Point(4, 22);
            this.tabDescricaoEPs.Name = "tabDescricaoEPs";
            this.tabDescricaoEPs.Size = new System.Drawing.Size(586, 153);
            this.tabDescricaoEPs.TabIndex = 4;
            this.tabDescricaoEPs.Text = "2.*. Observações (produção)";
            this.tabDescricaoEPs.UseVisualStyleBackColor = true;
            // 
            // grpDescricoesEPs
            // 
            this.grpDescricoesEPs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDescricoesEPs.Controls.Add(this.txtDescricoesEPs);
            this.grpDescricoesEPs.Location = new System.Drawing.Point(0, 1);
            this.grpDescricoesEPs.Name = "grpDescricoesEPs";
            this.grpDescricoesEPs.Size = new System.Drawing.Size(586, 152);
            this.grpDescricoesEPs.TabIndex = 1;
            this.grpDescricoesEPs.TabStop = false;
            // 
            // txtDescricoesEPs
            // 
            this.txtDescricoesEPs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescricoesEPs.Location = new System.Drawing.Point(5, 11);
            this.txtDescricoesEPs.Multiline = true;
            this.txtDescricoesEPs.Name = "txtDescricoesEPs";
            this.txtDescricoesEPs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescricoesEPs.Size = new System.Drawing.Size(575, 137);
            this.txtDescricoesEPs.TabIndex = 3;
            // 
            // rbSerieFechada
            // 
            this.rbSerieFechada.Location = new System.Drawing.Point(135, 13);
            this.rbSerieFechada.Name = "rbSerieFechada";
            this.rbSerieFechada.Size = new System.Drawing.Size(112, 24);
            this.rbSerieFechada.TabIndex = 5;
            this.rbSerieFechada.Text = "Série fechada";
            // 
            // rbSerieAberta
            // 
            this.rbSerieAberta.Location = new System.Drawing.Point(17, 13);
            this.rbSerieAberta.Name = "rbSerieAberta";
            this.rbSerieAberta.Size = new System.Drawing.Size(112, 24);
            this.rbSerieAberta.TabIndex = 4;
            this.rbSerieAberta.Text = "Série aberta";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbSerieFechada);
            this.panel1.Controls.Add(this.rbSerieAberta);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 411);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 52);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.grpAutor);
            this.panel2.Controls.Add(this.grpProdutores);
            this.panel2.Controls.Add(this.TabControl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(600, 411);
            this.panel2.TabIndex = 1;
            // 
            // PanelContexto
            // 
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "PanelContexto";
            this.Size = new System.Drawing.Size(600, 463);
            this.tabFluxograma.ResumeLayout(false);
            this.GrpBoxFluxograma.ResumeLayout(false);
            this.grpAutor.ResumeLayout(false);
            this.grpProdutores.ResumeLayout(false);
            this.TabControl.ResumeLayout(false);
            this.tabHistAdministrativa.ResumeLayout(false);
            this.TabControlHistoriaAdministrativa.ResumeLayout(false);
            this.tabDatasExistencia.ResumeLayout(false);
            this.tabDatasExistencia.PerformLayout();
            this.tabHistoria.ResumeLayout(false);
            this.tabHistoria.PerformLayout();
            this.tabZonaGeografica.ResumeLayout(false);
            this.tabZonaGeografica.PerformLayout();
            this.tabEstatutoLegal.ResumeLayout(false);
            this.tabEstatutoLegal.PerformLayout();
            this.tabFuncoesOcupacActividades.ResumeLayout(false);
            this.tabFuncoesOcupacActividades.PerformLayout();
            this.tabEnquadramentoLegal.ResumeLayout(false);
            this.tabEnquadramentoLegal.PerformLayout();
            this.tabEstruturaInterna.ResumeLayout(false);
            this.tabEstruturaInterna.PerformLayout();
            this.tabContextoGeral.ResumeLayout(false);
            this.tabContextoGeral.PerformLayout();
            this.tabOutraInforRelevante.ResumeLayout(false);
            this.tabOutraInforRelevante.PerformLayout();
            this.GrpBoxHistorAdminist.ResumeLayout(false);
            this.GrpBoxHistorAdminist.PerformLayout();
            this.tabHistArquivistica.ResumeLayout(false);
            this.GrpBoxHistArquivista.ResumeLayout(false);
            this.GrpBoxHistArquivista.PerformLayout();
            this.tabFonteImediataAquisTransf.ResumeLayout(false);
            this.GrpBoxFonteImediata.ResumeLayout(false);
            this.GrpBoxFonteImediata.PerformLayout();
            this.tabDescricaoEPs.ResumeLayout(false);
            this.grpDescricoesEPs.ResumeLayout(false);
            this.grpDescricoesEPs.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

		private void GetExtraResources()
		{
			btnRemove.Image = SharedResourcesOld.CurrentSharedResources.Apagar;
			btnEdit.Image = SharedResourcesOld.CurrentSharedResources.Editar;

            button1.Image = SharedResourcesOld.CurrentSharedResources.Apagar;

			base.ParentChanged += PanelContexto_ParentChanged;
		}

		// runs only once. sets tooltip as soon as it's parent appears
		private void PanelContexto_ParentChanged(object sender, System.EventArgs e)
		{
			MultiPanel.CurrentToolTip.SetToolTip(btnEdit, SharedResourcesOld.CurrentSharedResources.EditarString);
			MultiPanel.CurrentToolTip.SetToolTip(btnRemove, SharedResourcesOld.CurrentSharedResources.ApagarString);
			base.ParentChanged -= PanelContexto_ParentChanged;
		}

		private GISADataset.FRDBaseRow CurrentFRDBase;
		private GISADataset.SFRDContextoRow CurrentSFRDContexto;
		private NivelOrganicoDragDrop DragDropHandlerProdutor;
        private ControloAutAutorDragDrop DragDropHandlerAutor;
		private MasterPanelSeries mpSeries = null;
        private ArrayList CAsList = new ArrayList();

		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			CurrentFRDBase = (GISADataset.FRDBaseRow)CurrentDataRow;

			FRDRule.Current.LoadContextoData(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, conn);

			if (GisaDataSetHelper.UsingNiveisOrganicos())
				FRDRule.Current.LoadProdutores(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, CurrentFRDBase.NivelRow.ID, CAsList, conn);

			OnShowPanel();
			IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = false;
			//Make sure this field makes sense in the selected context
			GISADataset.TipoNivelRelacionadoRow CurrentTnrRow = TipoNivelRelacionado.GetTipoNivelRelacionadoDaPrimeiraRelacaoEncontrada(CurrentFRDBase.NivelRow);
            this.panel1.Visible = (CurrentTnrRow.ID == TipoNivelRelacionado.SR || CurrentTnrRow.ID == TipoNivelRelacionado.SSR);

			if (DragDropHandlerProdutor == null)
			{
                DragDropHandlerProdutor = new NivelOrganicoDragDrop(lstVwProdutores, CurrentFRDBase, ((frmMain)TopLevelControl).MasterPanel);
                DragDropHandlerProdutor.AddControloAut += AddControloAutProdutor;
			}
			else
				DragDropHandlerProdutor.FRDBase = CurrentFRDBase;

            if (DragDropHandlerAutor == null)
            {
                DragDropHandlerAutor = new ControloAutAutorDragDrop(lstVwAutor, new TipoNoticiaAut[] { TipoNoticiaAut.EntidadeProdutora }, CurrentFRDBase);
                DragDropHandlerAutor.AddControloAut += AddControloAutAutor;
            }
            else
                DragDropHandlerAutor.FRDBase = CurrentFRDBase;

			byte[] Versao = null;
			string QueryFilter = "IDFRDBase=" + CurrentFRDBase.ID.ToString();
			if (GisaDataSetHelper.GetInstance().SFRDContexto. Select(QueryFilter).Length != 0)
				CurrentSFRDContexto = (GISADataset.SFRDContextoRow)(GisaDataSetHelper.GetInstance().SFRDContexto. Select(QueryFilter)[0]);
			else
				CurrentSFRDContexto = GisaDataSetHelper.GetInstance().SFRDContexto.AddSFRDContextoRow(CurrentFRDBase, "", "", "", false, Versao, 0);

			if (mpSeries == null)
                mpSeries = (MasterPanelSeries)(((frmMain)TopLevelControl).MasterPanel);

			// Se estivermos em modo de utilização de níveis temático-funcionais 
			// não é possível a especificação de EPs
            if (GisaDataSetHelper.UsingNiveisOrganicos())
            {
                lstVwProdutores.Enabled = true;
                PopulateProdutores();
            }
            else
                lstVwProdutores.Enabled = false;

            PopulateAutor();

			txtDescricoesEPs.ReadOnly = true;

			// para os niveis estruturais orgânicos
			if (! (TipoNivel.isNivelDocumental(CurrentFRDBase.NivelRow)) && Nivel.isNivelOrganico(CurrentFRDBase.NivelRow))
			{
				//conteudo do campo txtHistoriaAdministrativaBibliografica 
				//já foi populado 
				txtHistoriaAdministrativaBibliografica.Visible = false;
				TabControlHistoriaAdministrativa.Visible = true;
			}
			else // para os niveis documentais e para os níveis estruturais temático-funcionais
			{
				txtHistoriaAdministrativaBibliografica.Visible = true;
				TabControlHistoriaAdministrativa.Visible = false;

				if (CurrentSFRDContexto.IsHistoriaAdministrativaNull())
					txtHistoriaAdministrativaBibliografica.Text = "";
				else
					txtHistoriaAdministrativaBibliografica.Text = CurrentSFRDContexto.HistoriaAdministrativa;
			}

            if (TipoNivel.isNivelDocumental(CurrentFRDBase.NivelRow))
            {
                var rhRow = CurrentFRDBase.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First();
                lstVwAutor.Enabled = rhRow.IDTipoNivelRelacionado == TipoNivelRelacionado.D || rhRow.IDTipoNivelRelacionado == TipoNivelRelacionado.SD;
            }

			if (CurrentSFRDContexto.IsHistoriaCustodialNull())
				txtHistoriaArquivista.Text = "";
			else
				txtHistoriaArquivista.Text = CurrentSFRDContexto.HistoriaCustodial;

			if (CurrentSFRDContexto.IsFonteImediataDeAquisicaoNull())
				txtFonteImediataAquisicTransf.Text = "";
			else
				txtFonteImediataAquisicTransf.Text = CurrentSFRDContexto.FonteImediataDeAquisicao;

			rbSerieAberta.Checked = CurrentSFRDContexto.SerieAberta;
			rbSerieFechada.Checked = ! rbSerieAberta.Checked;

			UpdateButtonsState();
			IsPopulated = true;
		}

        private void UpdateButtonsState()
        {
            UpdateButtonStateAutor();
            UpdateButtonStateProdutor();
        }
		
		private void PopulateProdutores()
		{
			lstVwProdutores.Items.Clear();

			// caso existam ascendentes
			if (CAsList.Count > 0)
			{
				// passar para a interface as entidades produtoras herdadas
				foreach (string CAID in CAsList)
				{
					GISADataset.ControloAutRow caRow = null;
					GISADataset.RelacaoHierarquicaRow[] rhRows = null;
					caRow = (GISADataset.ControloAutRow)(GisaDataSetHelper.GetInstance().ControloAut.Select("ID=" + CAID.ToString())[0]);

					long IDContexto = CurrentFRDBase.NivelRow.ID;
                    long IDNivelCA = caRow.GetNivelControloAutRows()[0].NivelRow.ID;
                    // relacoes hierarquicas (na pratica actualmente nunca poderá existir mais que uma) entre esta série e a EP
                    rhRows = (GISADataset.RelacaoHierarquicaRow[])(GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", IDContexto, IDNivelCA)));

                    // Se for encontrada uma relação directa significa que esta 
                    // relação é editável. As relações não directas serão as 
                    // relações entre subséries e EPs ou entre documentos que 
                    // constituam série e EPs, estas não serão editáveis.
                    // Também não são editáveis as relações que estejam a 
                    // ser utilizadas como contexto, ie, não é permitido 
                    // remover a relação com uma dada EP se esta for actualmente 
                    // a raiz na vista documental
                    if (rhRows.Length > 0)
                        TempRelacaoHierarquicaRow = rhRows[0];
                    else
                        TempRelacaoHierarquicaRow = null;

                    // apresentar ou esconder a coluna de datas de relação, para já so é necessário que apareçam para as relações directas entre EPs e séries
                    if (CurrentFRDBase.NivelRow.IDTipoNivel == TipoNivel.DOCUMENTAL) {
                        if (colDatasRelacao.ListView == null)
                            lstVwProdutores.Columns.Add(colDatasRelacao);
                    }
                    else {
                        if (colDatasRelacao.ListView != null)
                            lstVwProdutores.Columns.Remove(colDatasRelacao);
                    }
					GisaDataSetHelper.VisitControloAutDicionario(caRow, DisplayEntidadeProdutoraFormaAutorizada);
				}
			}

			// recolher e popular a descrição das entidades produtoras apresentadas
			if (! (TipoNivel.isNivelDocumental(CurrentFRDBase.NivelRow)))
				RecolhePopulaDetalhesEntidadesProdutoras();

			txtDescricoesEPs.Text = RecolheDescricaoRelacoesEntidadesProdutoras();
		}

        private void PopulateAutor()
        {
            lstVwAutor.Items.Clear();

            var sfrdAutor = CurrentFRDBase.GetSFRDAutorRows().ToList();

            if (sfrdAutor.Count == 0) return;

            sfrdAutor.ForEach(autor =>
            {
                var termo = autor.ControloAutRow.GetControloAutDicionarioRows().Single(cad => cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada).DicionarioRow.Termo;
                lstVwAutor.Items.Add(new ListViewItem(termo) { Tag = autor });
            });
        }

		public override void ViewToModel()
		{
			if (CurrentSFRDContexto == null || ! IsLoaded)
				return;

			CurrentSFRDContexto.HistoriaAdministrativa = txtHistoriaAdministrativaBibliografica.Text;
			CurrentSFRDContexto.HistoriaCustodial = txtHistoriaArquivista.Text;
			CurrentSFRDContexto.FonteImediataDeAquisicao = txtFonteImediataAquisicTransf.Text;

			CurrentSFRDContexto.SerieAberta = rbSerieAberta.Checked;
		}

		public override void Deactivate()
		{
			// limpar o campo (quer tenha databindings quer não tenha)
            GUIHelper.GUIHelper.clearField(txtHistoriaAdministrativaBibliografica);
            GUIHelper.GUIHelper.clearField(txtHistoriaArquivista);
            GUIHelper.GUIHelper.clearField(txtFonteImediataAquisicTransf);

            GUIHelper.GUIHelper.clearField(txtDataExistencia);
            GUIHelper.GUIHelper.clearField(txtHistoria);
            GUIHelper.GUIHelper.clearField(txtZonaGeografica);
            GUIHelper.GUIHelper.clearField(txtEstatutoLegal);
            GUIHelper.GUIHelper.clearField(txtFuncoesOcupacoesActividades);
            GUIHelper.GUIHelper.clearField(txtEnquadramentoLegal);
            GUIHelper.GUIHelper.clearField(txtEstruturaInterna);
            GUIHelper.GUIHelper.clearField(txtContextoGeral);
            GUIHelper.GUIHelper.clearField(txtOutraInformRelevante);

            GUIHelper.GUIHelper.clearField(rbSerieAberta);
			CurrentFRDBase = null;
			CurrentSFRDContexto = null;

            // só é realmente necessário quando se muda da vista documental para a estrutural para esconder 
            // o botão que mostra o painel de apoio
            OnHidePanel();
		}

		private void RecolhePopulaDetalhesEntidadesProdutoras()
		{
			txtHistoria.Clear();
			txtDataExistencia.Clear();
			txtZonaGeografica.Clear();
			txtEstatutoLegal.Clear();
			txtFuncoesOcupacoesActividades.Clear();
			txtEnquadramentoLegal.Clear();
			txtEstruturaInterna.Clear();
			txtContextoGeral.Clear();
			txtOutraInformRelevante.Clear();

			GISADataset.ControloAutDicionarioRow cadRow = null;
			foreach (ListViewItem item in lstVwProdutores.Items)
			{
				if (item.Tag is GISADataset.ControloAutDicionarioRow)
					cadRow = (GISADataset.ControloAutDicionarioRow)item.Tag;
				else if (item.Tag is GISADataset.RelacaoHierarquicaRow)
					cadRow = ControloAutHelper.getFormaAutorizada(((GISADataset.RelacaoHierarquicaRow)item.Tag).NivelRowByNivelRelacaoHierarquicaUpper.GetNivelControloAutRows()[0].ControloAutRow);
				else
					Debug.Assert(false, "ControloAutRow or RelacaoHierarquicaRow expected");


				GISADataset.ControloAutDatasExistenciaRow[] cadeRows = null;
				cadeRows = cadRow.ControloAutRow.GetControloAutDatasExistenciaRows();
				if (cadeRows.Length > 0)
				{
					if (! (cadeRows[0].IsDescDatasExistenciaNull()) && cadeRows[0].DescDatasExistencia.Length > 0 || (! (cadeRows[0].IsInicioAnoNull()) && cadeRows[0].InicioAno.Length > 0) || (! (cadeRows[0].IsInicioMesNull()) && cadeRows[0].InicioMes.Length > 0) || (! (cadeRows[0].IsInicioDiaNull()) && cadeRows[0].InicioDia.Length > 0) || (! (cadeRows[0].IsFimAnoNull()) && cadeRows[0].FimAno.Length > 0) || (! (cadeRows[0].IsFimMesNull()) && cadeRows[0].FimMes.Length > 0) || (! (cadeRows[0].IsFimDiaNull()) && cadeRows[0].FimDia.Length > 0))
					{
                        txtDataExistencia.Text += " " + cadRow.DicionarioRow.Termo + Environment.NewLine + GUIHelper.GUIHelper.FormatDateInterval(cadeRows[0]);
						if (! (cadeRows[0].IsDescDatasExistenciaNull()) && cadeRows[0].DescDatasExistencia.Length > 0)
							txtDataExistencia.Text += Environment.NewLine + cadeRows[0].DescDatasExistencia;

						txtDataExistencia.Text += Environment.NewLine + Environment.NewLine;
					}
				}

				if (! cadRow.ControloAutRow.IsDescHistoriaNull() && cadRow.ControloAutRow.DescHistoria.Length > 0)
					txtHistoria.Text += " " + cadRow.DicionarioRow.Termo + Environment.NewLine + cadRow.ControloAutRow.DescHistoria + Environment.NewLine + Environment.NewLine;

				if (! cadRow.ControloAutRow.IsDescZonaGeograficaNull() && cadRow.ControloAutRow.DescZonaGeografica.Length > 0)
					txtZonaGeografica.Text += " " + cadRow.DicionarioRow.Termo + Environment.NewLine + cadRow.ControloAutRow.DescZonaGeografica + Environment.NewLine + Environment.NewLine;

				if (! cadRow.ControloAutRow.IsDescEstatutoLegalNull() && cadRow.ControloAutRow.DescEstatutoLegal.Length > 0)
					txtEstatutoLegal.Text += " " + cadRow.DicionarioRow.Termo + Environment.NewLine + cadRow.ControloAutRow.DescEstatutoLegal + Environment.NewLine + Environment.NewLine;

				if (! cadRow.ControloAutRow.IsDescOcupacoesActividadesNull() && cadRow.ControloAutRow.DescOcupacoesActividades.Length > 0)
					txtFuncoesOcupacoesActividades.Text += " " + cadRow.DicionarioRow.Termo + Environment.NewLine + cadRow.ControloAutRow.DescOcupacoesActividades + Environment.NewLine + Environment.NewLine;

				if (! cadRow.ControloAutRow.IsDescEnquadramentoLegalNull() && cadRow.ControloAutRow.DescEnquadramentoLegal.Length > 0)
					txtEnquadramentoLegal.Text += " " + cadRow.DicionarioRow.Termo + Environment.NewLine + cadRow.ControloAutRow.DescEnquadramentoLegal + Environment.NewLine + Environment.NewLine;

				if (! cadRow.ControloAutRow.IsDescEstruturaInternaNull() && cadRow.ControloAutRow.DescEstruturaInterna.Length > 0)
					txtEstruturaInterna.Text += " " + cadRow.DicionarioRow.Termo + Environment.NewLine + cadRow.ControloAutRow.DescEstruturaInterna + Environment.NewLine + Environment.NewLine;

				if (! cadRow.ControloAutRow.IsDescContextoGeralNull() && cadRow.ControloAutRow.DescContextoGeral.Length > 0)
					txtContextoGeral.Text += " " + cadRow.DicionarioRow.Termo + Environment.NewLine + cadRow.ControloAutRow.DescContextoGeral + Environment.NewLine + Environment.NewLine;

				if (! cadRow.ControloAutRow.IsDescOutraInformacaoRelevanteNull() && cadRow.ControloAutRow.DescOutraInformacaoRelevante.Length > 0)
					txtOutraInformRelevante.Text += " " + cadRow.DicionarioRow.Termo + Environment.NewLine + cadRow.ControloAutRow.DescOutraInformacaoRelevante + Environment.NewLine + Environment.NewLine;
			}
		}

		private string RecolheDescricaoRelacoesEntidadesProdutoras()
		{
			string resultado = string.Empty;
			GISADataset.RelacaoHierarquicaRow rhRow = null;
			foreach (ListViewItem item in lstVwProdutores.Items)
			{
				if (item.Tag is GISADataset.ControloAutDicionarioRow)
					rhRow = null;
				else if (item.Tag is GISADataset.RelacaoHierarquicaRow)
					rhRow = (GISADataset.RelacaoHierarquicaRow)item.Tag;
				else
					Debug.Assert(false, "ControloAutRow or RelacaoHierarquicaRow expected");

				if (rhRow != null && ! rhRow.IsDescricaoNull() && rhRow.Descricao.Length > 0)
					resultado += " " + ControloAutHelper.getFormaAutorizada(((GISADataset.RelacaoHierarquicaRow)item.Tag).NivelRowByNivelRelacaoHierarquicaUpper.GetNivelControloAutRows()[0].ControloAutRow).DicionarioRow.Termo + Environment.NewLine + rhRow.Descricao + Environment.NewLine + Environment.NewLine;
			}
			return resultado;
		}

		private GISADataset.RelacaoHierarquicaRow TempRelacaoHierarquicaRow;
		private void DisplayEntidadeProdutoraFormaAutorizada(GISADataset.ControloAutDicionarioRow ControloAutDicionario)
		{
			if (ControloAutDicionario.IDTipoControloAutForma == Convert.ToInt64(TipoControloAutForma.FormaAutorizada))
			{
				ListViewItem item = null;
				item = lstVwProdutores.Items.Add(ControloAutDicionario.DicionarioRow.Termo);

				item.SubItems.Add(TranslationHelper.FormatBoolean(ControloAutDicionario.ControloAutRow.Autorizado));
				item.SubItems.Add(TranslationHelper.FormatBoolean(ControloAutDicionario.ControloAutRow.Completo));

				GISADataset.ControloAutDatasExistenciaRow[] cadeRows = null;
				cadeRows = ControloAutDicionario.ControloAutRow.GetControloAutDatasExistenciaRows();
				if (cadeRows.Length == 0)
					item.SubItems.Add(string.Empty);
				else
                    item.SubItems.Add(GUIHelper.GUIHelper.FormatDateInterval(cadeRows[0]));

				// as datas de existencia sao actualizadas à posteriori, se necessário
				item.SubItems.Add("");

				// as relações não editáveis terão um tag de tipo diferente
				if (TempRelacaoHierarquicaRow == null)
				{
					item.ForeColor = System.Drawing.Color.Gray;
					item.Tag = ControloAutDicionario;
				}
				else
					item.Tag = TempRelacaoHierarquicaRow;

				updateDatasRelacao(item);
			}
		}

		private void updateDatasRelacao(ListViewItem item)
		{
			if (item.Tag == null || ! (item.Tag is GISADataset.RelacaoHierarquicaRow))
			{
				// para já apresentamos so as datas de relação para relações directas entre séries e niveis estruturais
				if (item.ListView.Columns.Contains(colDatasRelacao))
					item.SubItems[colDatasRelacao.Index].Text = string.Empty;
			}
			else
			{
				GISADataset.RelacaoHierarquicaRow rhRow = null;
				rhRow = (GISADataset.RelacaoHierarquicaRow)item.Tag;
				if (item.ListView.Columns.Contains(colDatasRelacao))
                    item.SubItems[colDatasRelacao.Index].Text = GUIHelper.GUIHelper.FormatDateInterval(rhRow);
			}
		}

        private void AddControloAutAutor(object Sender, ListViewItem item)
        {
        }

		private void AddControloAutProdutor(object Sender, ListViewItem ListViewItem)
		{
			GISADataset.RelacaoHierarquicaRow rhRow = null;
			rhRow = (GISADataset.RelacaoHierarquicaRow)ListViewItem.Tag;

			ListViewItem.SubItems.Add(TranslationHelper.FormatBoolean(rhRow.NivelRowByNivelRelacaoHierarquicaUpper.GetNivelControloAutRows()[0].ControloAutRow.Autorizado));
			ListViewItem.SubItems.Add(TranslationHelper.FormatBoolean(rhRow.NivelRowByNivelRelacaoHierarquicaUpper.GetNivelControloAutRows()[0].ControloAutRow.Completo));

			GISADataset.ControloAutDatasExistenciaRow[] cadeRows = null;
			cadeRows = rhRow.NivelRowByNivelRelacaoHierarquicaUpper.GetNivelControloAutRows()[0].ControloAutRow.GetControloAutDatasExistenciaRows();
			if (cadeRows.Length == 0)
				ListViewItem.SubItems.Add(string.Empty);
			else
                ListViewItem.SubItems.Add(GUIHelper.GUIHelper.FormatDateInterval(cadeRows[0]));

			ListViewItem.SubItems.Add("");
			updateDatasRelacao(ListViewItem);

			txtDescricoesEPs.Text = RecolheDescricaoRelacoesEntidadesProdutoras();
		}

		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			if (lstVwProdutores.SelectedItems.Count == 1 && lstVwProdutores.SelectedItems[0].Tag is GISADataset.RelacaoHierarquicaRow)
			{
				GISADataset.RelacaoHierarquicaRow rhRow = null;
				rhRow = (GISADataset.RelacaoHierarquicaRow)(lstVwProdutores.SelectedItems[0].Tag);

				// Apresentar form que permita editar a relação
				FormRelacaoHierarquica frmRh = new FormRelacaoHierarquica();
				frmRh.relacaoNvl.TipoNivelRelacionadoRow = rhRow.TipoNivelRelacionadoRow;
				frmRh.relacaoNvl.ContextNivelRow = rhRow.NivelRowByNivelRelacaoHierarquicaUpper;
				frmRh.relacaoNvl.txtDescricao.Text = GisaDataSetHelper.GetDBNullableText(rhRow, "Descricao");
				frmRh.relacaoNvl.dtRelacaoInicio.ValueYear = GisaDataSetHelper.GetDBNullableText(rhRow, "InicioAno");
				frmRh.relacaoNvl.dtRelacaoInicio.ValueMonth = GisaDataSetHelper.GetDBNullableText(rhRow, "InicioMes");
				frmRh.relacaoNvl.dtRelacaoInicio.ValueDay = GisaDataSetHelper.GetDBNullableText(rhRow, "InicioDia");
				frmRh.relacaoNvl.dtRelacaoFim.ValueYear = GisaDataSetHelper.GetDBNullableText(rhRow, "FimAno");
				frmRh.relacaoNvl.dtRelacaoFim.ValueMonth = GisaDataSetHelper.GetDBNullableText(rhRow, "FimMes");
				frmRh.relacaoNvl.dtRelacaoFim.ValueDay = GisaDataSetHelper.GetDBNullableText(rhRow, "FimDia");
				if (frmRh.ShowDialog() == DialogResult.Cancel)
					return;

				rhRow.Descricao = frmRh.relacaoNvl.txtDescricao.Text;
				rhRow.InicioAno = GISA.Utils.GUIHelper.ReadYear(frmRh.relacaoNvl.dtRelacaoInicio.ValueYear);
				rhRow.InicioMes = GISA.Utils.GUIHelper.ReadMonth(frmRh.relacaoNvl.dtRelacaoInicio.ValueMonth);
				rhRow.InicioDia = GISA.Utils.GUIHelper.ReadDay(frmRh.relacaoNvl.dtRelacaoInicio.ValueDay);
				rhRow.FimAno = GISA.Utils.GUIHelper.ReadYear(frmRh.relacaoNvl.dtRelacaoFim.ValueYear);
				rhRow.FimMes = GISA.Utils.GUIHelper.ReadMonth(frmRh.relacaoNvl.dtRelacaoFim.ValueMonth);
				rhRow.FimDia = GISA.Utils.GUIHelper.ReadDay(frmRh.relacaoNvl.dtRelacaoFim.ValueDay);
			}
			updateDatasRelacao(lstVwProdutores.SelectedItems[0]);
		}

        private void btnRemoveAutor_Click(object sender, System.EventArgs e)
        {
            deleteItemsAutor();
        }

        private void lstVwAutores_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == Convert.ToInt32(Keys.Delete))
                deleteItemsAutor();
        }

        private void deleteItemsAutor()
        {
            GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwAutor, null, null);
        }

        private void lstVwAutores_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateButtonStateAutor();
        }

        private void UpdateButtonStateAutor()
        {
            button1.Enabled = lstVwAutor.SelectedItems.Count > 0;
        }

        private bool canDeleteProdutor = false;
		private void btnRemoveProdutor_Click(object sender, System.EventArgs e)
		{
			deleteItemsProdutor();
		}

		private void lstVwProdutores_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == Convert.ToInt32(Keys.Delete))
                deleteItemsProdutor();
		}

        private void deleteItemsProdutor()
		{
            if (!canDeleteProdutor) return;
            GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwProdutores, null, typeof(GISADataset.ControloAutDicionarioRow));
			txtDescricoesEPs.Text = RecolheDescricaoRelacoesEntidadesProdutoras();
		}

		private void lstVwProdutores_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UpdateButtonStateProdutor();
		}

		private void UpdateButtonStateProdutor()
		{
			if (lstVwProdutores.SelectedItems.Count > 0)
			{
				if (lstVwProdutores.SelectedItems.Count == 1 && lstVwProdutores.SelectedItems[0].Tag is GISADataset.RelacaoHierarquicaRow)
					btnEdit.Enabled = true;
				else
					btnEdit.Enabled = false;

				btnRemove.Enabled = false;
                foreach (ListViewItem item in lstVwProdutores.SelectedItems)
                {
                    if (item.Tag is GISADataset.RelacaoHierarquicaRow)
                    {
                        GISADataset.RelacaoHierarquicaRow rhRow = null;
                        rhRow = (GISADataset.RelacaoHierarquicaRow)item.Tag;
                        long nUpperID = long.MinValue;
                        try
                        {
                            nUpperID = mpSeries.nivelNavigator1.ContextBreadCrumbsPathID;
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine(e.ToString());
#if DEBUG
                            throw;
#endif
                        }
                        btnRemove.Enabled = !(mpSeries.nivelNavigator1.ContextBreadCrumbsPathID == rhRow.IDUpper);
                        if (!btnRemove.Enabled) break;
                    }
				}
			}
			else
			{
				btnEdit.Enabled = false;
				btnRemove.Enabled = false;
			}
            canDeleteProdutor = btnRemove.Enabled;
		}

		private void ToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			if (e.Button == MultiPanel.ToolBarButtonAuxList)
				ToggleControloAutoridade(MultiPanel.ToolBarButtonAuxList.Pushed);
		}

		private void ToggleControloAutoridade(bool showIt)
		{
			if (showIt)
			{
				if (! (((frmMain)this.TopLevelControl).isSuportPanel))
				{
					// Make sure the button is pushed
					MultiPanel.ToolBarButtonAuxList.Pushed = true;

					// Indicação que um painel está a ser usado como suporte
					((frmMain)this.TopLevelControl).isSuportPanel = true;

					// Show the panel with all controlos autoridade
					((frmMain)this.TopLevelControl).PushMasterPanel(typeof(MasterPanelControloAut));

                    MasterPanelControloAut master = (MasterPanelControloAut)(((frmMain)this.TopLevelControl).MasterPanel);

                    master.caList.AllowedNoticiaAut(TipoNoticiaAut.EntidadeProdutora);
                    master.caList.AllowedNoticiaAutLocked = true;
                    master.caList.ReloadList();

                    master.UpdateSupoortPanelPermissions("GISA.FRDCAEntidadeProdutora");
                    master.UpdateToolBarButtons();
				}
			}
			else
			{
				// Make sure the button is not pushed            
				MultiPanel.ToolBarButtonAuxList.Pushed = false;

				// Remove the panel with all controlos autoridade
				if (this.TopLevelControl != null)
				{
					if (((frmMain)this.TopLevelControl). MasterPanel is MasterPanelControloAut)
					{
						// Indicação que um painel está a ser usado como suporte
						((frmMain)this.TopLevelControl).isSuportPanel = false;

                        MasterPanelControloAut tempWith2 = (MasterPanelControloAut)(((frmMain)this.TopLevelControl).MasterPanel);

						tempWith2.caList.AllowedNoticiaAutLocked = false;
						((frmMain)this.TopLevelControl).PopMasterPanel(typeof(MasterPanelControloAut));
					}
				}
			}
		}

        public override void OnShowPanel()
		{
			// Nunca mostrar o painel de suporte com EPs se o nível for 
			// estrutural e/ou se não estivermos em modo de utilização de EPs
			if (!(((frmMain)this.TopLevelControl).isSuportPanel))
            {
                MasterPanelSeries mpSeries = (MasterPanelSeries)(((frmMain)this.TopLevelControl).MasterPanel);

                if (mpSeries.nivelNavigator1.PanelToggleState == GISA.Controls.Nivel.NivelNavigator.ToggleState.Documental)
                {
                    if (CurrentFRDBase != null && 
                        ((CurrentFRDBase.NivelRow.IDTipoNivel == TipoNivel.DOCUMENTAL &&
                        CurrentFRDBase.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].NivelRowByNivelRelacaoHierarquicaUpper.IDTipoNivel == TipoNivel.ESTRUTURAL) ||
                        CurrentFRDBase.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].IDTipoNivelRelacionado == (long)TipoNivelRelacionado.D ||
                        CurrentFRDBase.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].IDTipoNivelRelacionado == (long)TipoNivelRelacionado.SD) &&
                        !TbBAuxListEventAssigned)
                    {
                        //Show the button that brings up the panel with controlos
                        //autoridade and select it by default.
                        MultiPanel.ToolBar.ButtonClick += ToolBar_ButtonClick;
                        MultiPanel.ToolBarButtonAuxList.Visible = true;

                        TbBAuxListEventAssigned = true;

                        lstVwProdutores.AllowDrop = CurrentFRDBase.NivelRow.IDTipoNivel == TipoNivel.DOCUMENTAL &&
                            CurrentFRDBase.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].NivelRowByNivelRelacaoHierarquicaUpper.IDTipoNivel == TipoNivel.ESTRUTURAL;
                        lstVwAutor.AllowDrop = CurrentFRDBase.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].IDTipoNivelRelacionado == (long)TipoNivelRelacionado.D ||
                            CurrentFRDBase.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].IDTipoNivelRelacionado == (long)TipoNivelRelacionado.SD;
                    }
                }
            }
		}

		public override void OnHidePanel()
		{
			// if seguinte serve exclusivamente para debug
			if (CurrentFRDBase != null && CurrentFRDBase.RowState == DataRowState.Detached)
				Debug.WriteLine("OCORREU SITUAÇÃO DE ERRO NO PAINEL CONTEXTO. EM PRINCIPIO NINGUEM DEU POR ELE.");

			//Deactivate Toolbar Buttons
            if (TbBAuxListEventAssigned)
            {
                MultiPanel.ToolBar.ButtonClick -= ToolBar_ButtonClick;
                MultiPanel.ToolBarButtonAuxList.Visible = false;

                ToggleControloAutoridade(false);
                TbBAuxListEventAssigned = false;
            }
		}
	}
}