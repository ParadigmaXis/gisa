using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;
using GISA.GUIHelper;

namespace GISA
{
	public class PanelAvaliacaoDocumentosUnidadesFisicas : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

        private System.Windows.Forms.Timer updateResultsTimer;
        private System.Windows.Forms.Timer updateDocumentosSelectionTimer;

		public PanelAvaliacaoDocumentosUnidadesFisicas() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            lvwDocumentos.SelectedIndexChanged += lvwDocumentos_SelectedIndexChanged;
            lvwUnidadesFisicas.ItemCheck += lvwUnidadesFisicas_ItemCheck;
            btnProximo.Click += btnProximo_Click;
            btnAnterior.Click += btnAnterior_Click;
            lvwSeleccaoUnidadesFisicas.SelectedIndexChanged += lvwSeleccaoUnidadesFisicas_SelectedIndexChanged;
            btnAdd.Click += btnAdd_Click;
            btnRemove.Click += btnRemove_Click;

            updateResultsTimer = new System.Windows.Forms.Timer();
            updateResultsTimer.Tick += updateResultsTimer_Tick;

            updateDocumentosSelectionTimer = new System.Windows.Forms.Timer();
            updateDocumentosSelectionTimer.Tick += updateDocumentosSelectionTimer_Tick;


			GetExtraResources();
			UpdateSelUFButtonState();
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
		internal System.Windows.Forms.GroupBox grpUnidadesFisicas;
		internal System.Windows.Forms.GroupBox grpDocumentos;
		internal System.Windows.Forms.ListView lvwUnidadesFisicas;
		internal System.Windows.Forms.ListView lvwDocumentos;
		internal System.Windows.Forms.ToolTip ToolTip1;
		internal System.Windows.Forms.Label lblPassoDesc;
		internal System.Windows.Forms.Label lblPassoNum;
		internal System.Windows.Forms.Button btnProximo;
		internal System.Windows.Forms.Button btnAnterior;
		internal System.Windows.Forms.ColumnHeader chUFDesignacao;
		internal System.Windows.Forms.ColumnHeader chUFDatasProducao;
		internal System.Windows.Forms.GroupBox grpResultado;
		internal System.Windows.Forms.GroupBox grpDestinoFinal;
		internal System.Windows.Forms.ComboBox cbDestinoFinal;
		internal System.Windows.Forms.GroupBox grpPublicacao;
		internal System.Windows.Forms.CheckBox chkPublicar;
		internal GISA.ControlAutoEliminacao ControlAutoEliminacao1;
		internal System.Windows.Forms.ColumnHeader chUFSeleccao;
		internal System.Windows.Forms.ColumnHeader chUFCodigo;
		internal System.Windows.Forms.ColumnHeader chDocDesignacao;
		internal System.Windows.Forms.ColumnHeader chDocSerieSubserie;
		internal System.Windows.Forms.ColumnHeader chDocDatasProducao;
		internal System.Windows.Forms.ColumnHeader chDocDestino;
		internal System.Windows.Forms.ColumnHeader chDocPublicado;
		internal System.Windows.Forms.ColumnHeader chDocAutoEliminacao;
		internal System.Windows.Forms.GroupBox grpConteudoPasso2;
		internal System.Windows.Forms.GroupBox grpAvaliacaoSeleccaoEliminacao;
		internal System.Windows.Forms.ListView lvwSeleccaoUnidadesFisicas;
		internal System.Windows.Forms.GroupBox grpConteudoPasso1;
		internal System.Windows.Forms.ColumnHeader chSelUFCodigoParcial;
		internal System.Windows.Forms.ColumnHeader chSelUFDesignacao;
		internal System.Windows.Forms.ColumnHeader chSelUFAutosEliminacaoDocumentos;
		internal System.Windows.Forms.ColumnHeader chSelUFAutosEliminacaoRestantes;
		internal System.Windows.Forms.GroupBox grpSeleccaoUnidadesFisicas;
		internal System.Windows.Forms.Button btnAdd;
		internal System.Windows.Forms.Button btnRemove;
		internal System.Windows.Forms.ColumnHeader chUFNumDocumentos;
		internal System.Windows.Forms.GroupBox grpFiltros;
		internal System.Windows.Forms.GroupBox grpFltDestinoFinal;
		internal System.Windows.Forms.GroupBox grpFltPrazo;
		internal System.Windows.Forms.GroupBox grpFltAuto;
		internal System.Windows.Forms.ComboBox cbFltDestinoFinal;
		internal System.Windows.Forms.ComboBox cbFltPrazo;
		internal System.Windows.Forms.ComboBox cbFltAuto;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.grpAvaliacaoSeleccaoEliminacao = new System.Windows.Forms.GroupBox();
            this.lblPassoDesc = new System.Windows.Forms.Label();
            this.lblPassoNum = new System.Windows.Forms.Label();
            this.btnProximo = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.grpConteudoPasso1 = new System.Windows.Forms.GroupBox();
            this.grpUnidadesFisicas = new System.Windows.Forms.GroupBox();
            this.lvwUnidadesFisicas = new System.Windows.Forms.ListView();
            this.chUFSeleccao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUFCodigo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUFDesignacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUFDatasProducao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUFNumDocumentos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpDocumentos = new System.Windows.Forms.GroupBox();
            this.lvwDocumentos = new System.Windows.Forms.ListView();
            this.chDocDesignacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDocSerieSubserie = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDocDatasProducao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDocDestino = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDocAutoEliminacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDocPublicado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpResultado = new System.Windows.Forms.GroupBox();
            this.grpDestinoFinal = new System.Windows.Forms.GroupBox();
            this.cbDestinoFinal = new System.Windows.Forms.ComboBox();
            this.grpPublicacao = new System.Windows.Forms.GroupBox();
            this.chkPublicar = new System.Windows.Forms.CheckBox();
            this.ControlAutoEliminacao1 = new GISA.ControlAutoEliminacao();
            this.grpFiltros = new System.Windows.Forms.GroupBox();
            this.grpFltDestinoFinal = new System.Windows.Forms.GroupBox();
            this.cbFltDestinoFinal = new System.Windows.Forms.ComboBox();
            this.grpFltPrazo = new System.Windows.Forms.GroupBox();
            this.cbFltPrazo = new System.Windows.Forms.ComboBox();
            this.grpFltAuto = new System.Windows.Forms.GroupBox();
            this.cbFltAuto = new System.Windows.Forms.ComboBox();
            this.grpConteudoPasso2 = new System.Windows.Forms.GroupBox();
            this.grpSeleccaoUnidadesFisicas = new System.Windows.Forms.GroupBox();
            this.lvwSeleccaoUnidadesFisicas = new System.Windows.Forms.ListView();
            this.chSelUFCodigoParcial = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSelUFDesignacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSelUFAutosEliminacaoDocumentos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSelUFAutosEliminacaoRestantes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grpAvaliacaoSeleccaoEliminacao.SuspendLayout();
            this.grpConteudoPasso1.SuspendLayout();
            this.grpUnidadesFisicas.SuspendLayout();
            this.grpDocumentos.SuspendLayout();
            this.grpResultado.SuspendLayout();
            this.grpDestinoFinal.SuspendLayout();
            this.grpPublicacao.SuspendLayout();
            this.grpFiltros.SuspendLayout();
            this.grpFltDestinoFinal.SuspendLayout();
            this.grpFltPrazo.SuspendLayout();
            this.grpFltAuto.SuspendLayout();
            this.grpConteudoPasso2.SuspendLayout();
            this.grpSeleccaoUnidadesFisicas.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpAvaliacaoSeleccaoEliminacao
            // 
            this.grpAvaliacaoSeleccaoEliminacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAvaliacaoSeleccaoEliminacao.Controls.Add(this.lblPassoDesc);
            this.grpAvaliacaoSeleccaoEliminacao.Controls.Add(this.lblPassoNum);
            this.grpAvaliacaoSeleccaoEliminacao.Controls.Add(this.btnProximo);
            this.grpAvaliacaoSeleccaoEliminacao.Controls.Add(this.btnAnterior);
            this.grpAvaliacaoSeleccaoEliminacao.Controls.Add(this.grpConteudoPasso1);
            this.grpAvaliacaoSeleccaoEliminacao.Controls.Add(this.grpConteudoPasso2);
            this.grpAvaliacaoSeleccaoEliminacao.Location = new System.Drawing.Point(3, 3);
            this.grpAvaliacaoSeleccaoEliminacao.Name = "grpAvaliacaoSeleccaoEliminacao";
            this.grpAvaliacaoSeleccaoEliminacao.Size = new System.Drawing.Size(794, 594);
            this.grpAvaliacaoSeleccaoEliminacao.TabIndex = 1;
            this.grpAvaliacaoSeleccaoEliminacao.TabStop = false;
            this.grpAvaliacaoSeleccaoEliminacao.Text = "3.2. Avaliação, seleção e eliminação";
            // 
            // lblPassoDesc
            // 
            this.lblPassoDesc.Location = new System.Drawing.Point(72, 17);
            this.lblPassoDesc.Name = "lblPassoDesc";
            this.lblPassoDesc.Size = new System.Drawing.Size(316, 16);
            this.lblPassoDesc.TabIndex = 3;
            this.lblPassoDesc.Text = "Avaliação e seleção dos conteúdos da unidade de descrição";
            // 
            // lblPassoNum
            // 
            this.lblPassoNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassoNum.Location = new System.Drawing.Point(14, 17);
            this.lblPassoNum.Name = "lblPassoNum";
            this.lblPassoNum.Size = new System.Drawing.Size(70, 16);
            this.lblPassoNum.TabIndex = 2;
            this.lblPassoNum.Text = "Passo 1:";
            // 
            // btnProximo
            // 
            this.btnProximo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProximo.Location = new System.Drawing.Point(704, 14);
            this.btnProximo.Name = "btnProximo";
            this.btnProximo.Size = new System.Drawing.Size(75, 20);
            this.btnProximo.TabIndex = 9;
            this.btnProximo.Text = "Próximo >>";
            // 
            // btnAnterior
            // 
            this.btnAnterior.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnterior.Location = new System.Drawing.Point(704, 14);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(75, 20);
            this.btnAnterior.TabIndex = 1;
            this.btnAnterior.Text = "<< Anterior";
            this.btnAnterior.Visible = false;
            // 
            // grpConteudoPasso1
            // 
            this.grpConteudoPasso1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpConteudoPasso1.Controls.Add(this.grpUnidadesFisicas);
            this.grpConteudoPasso1.Controls.Add(this.grpDocumentos);
            this.grpConteudoPasso1.Controls.Add(this.grpResultado);
            this.grpConteudoPasso1.Controls.Add(this.grpFiltros);
            this.grpConteudoPasso1.Location = new System.Drawing.Point(6, 19);
            this.grpConteudoPasso1.Name = "grpConteudoPasso1";
            this.grpConteudoPasso1.Size = new System.Drawing.Size(782, 569);
            this.grpConteudoPasso1.TabIndex = 4;
            this.grpConteudoPasso1.TabStop = false;
            // 
            // grpUnidadesFisicas
            // 
            this.grpUnidadesFisicas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpUnidadesFisicas.Controls.Add(this.lvwUnidadesFisicas);
            this.grpUnidadesFisicas.Location = new System.Drawing.Point(7, 16);
            this.grpUnidadesFisicas.Name = "grpUnidadesFisicas";
            this.grpUnidadesFisicas.Size = new System.Drawing.Size(766, 128);
            this.grpUnidadesFisicas.TabIndex = 5;
            this.grpUnidadesFisicas.TabStop = false;
            this.grpUnidadesFisicas.Text = "Unidades físicas";
            // 
            // lvwUnidadesFisicas
            // 
            this.lvwUnidadesFisicas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwUnidadesFisicas.CheckBoxes = true;
            this.lvwUnidadesFisicas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chUFSeleccao,
            this.chUFCodigo,
            this.chUFDesignacao,
            this.chUFDatasProducao,
            this.chUFNumDocumentos});
            this.lvwUnidadesFisicas.FullRowSelect = true;
            this.lvwUnidadesFisicas.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwUnidadesFisicas.HideSelection = false;
            this.lvwUnidadesFisicas.Location = new System.Drawing.Point(7, 18);
            this.lvwUnidadesFisicas.Name = "lvwUnidadesFisicas";
            this.lvwUnidadesFisicas.Size = new System.Drawing.Size(750, 103);
            this.lvwUnidadesFisicas.TabIndex = 0;
            this.lvwUnidadesFisicas.UseCompatibleStateImageBehavior = false;
            this.lvwUnidadesFisicas.View = System.Windows.Forms.View.Details;
            // 
            // chUFSeleccao
            // 
            this.chUFSeleccao.Text = "";
            this.chUFSeleccao.Width = 21;
            // 
            // chUFCodigo
            // 
            this.chUFCodigo.Text = "Código parcial";
            this.chUFCodigo.Width = 74;
            // 
            // chUFDesignacao
            // 
            this.chUFDesignacao.Text = "Designação";
            this.chUFDesignacao.Width = 269;
            // 
            // chUFDatasProducao
            // 
            this.chUFDatasProducao.Text = "Datas de Produção";
            this.chUFDatasProducao.Width = 137;
            // 
            // chUFNumDocumentos
            // 
            this.chUFNumDocumentos.Text = "Nº de documentos";
            this.chUFNumDocumentos.Width = 84;
            // 
            // grpDocumentos
            // 
            this.grpDocumentos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDocumentos.Controls.Add(this.lvwDocumentos);
            this.grpDocumentos.Location = new System.Drawing.Point(7, 150);
            this.grpDocumentos.Name = "grpDocumentos";
            this.grpDocumentos.Size = new System.Drawing.Size(603, 411);
            this.grpDocumentos.TabIndex = 7;
            this.grpDocumentos.TabStop = false;
            this.grpDocumentos.Text = "Documentos";
            // 
            // lvwDocumentos
            // 
            this.lvwDocumentos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwDocumentos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDocDesignacao,
            this.chDocSerieSubserie,
            this.chDocDatasProducao,
            this.chDocDestino,
            this.chDocAutoEliminacao,
            this.chDocPublicado});
            this.lvwDocumentos.FullRowSelect = true;
            this.lvwDocumentos.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwDocumentos.HideSelection = false;
            this.lvwDocumentos.Location = new System.Drawing.Point(8, 18);
            this.lvwDocumentos.Name = "lvwDocumentos";
            this.lvwDocumentos.Size = new System.Drawing.Size(587, 385);
            this.lvwDocumentos.TabIndex = 0;
            this.lvwDocumentos.UseCompatibleStateImageBehavior = false;
            this.lvwDocumentos.View = System.Windows.Forms.View.Details;
            // 
            // chDocDesignacao
            // 
            this.chDocDesignacao.Text = "Documento";
            this.chDocDesignacao.Width = 214;
            // 
            // chDocSerieSubserie
            // 
            this.chDocSerieSubserie.Text = "(Sub) Série";
            this.chDocSerieSubserie.Width = 105;
            // 
            // chDocDatasProducao
            // 
            this.chDocDatasProducao.Text = "Datas de Produção";
            this.chDocDatasProducao.Width = 137;
            // 
            // chDocDestino
            // 
            this.chDocDestino.Text = "Destino final";
            this.chDocDestino.Width = 75;
            // 
            // chDocAutoEliminacao
            // 
            this.chDocAutoEliminacao.Text = "Auto eliminação";
            this.chDocAutoEliminacao.Width = 87;
            // 
            // chDocPublicado
            // 
            this.chDocPublicado.Text = "Publicado";
            this.chDocPublicado.Width = 62;
            // 
            // grpResultado
            // 
            this.grpResultado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpResultado.Controls.Add(this.grpDestinoFinal);
            this.grpResultado.Controls.Add(this.grpPublicacao);
            this.grpResultado.Controls.Add(this.ControlAutoEliminacao1);
            this.grpResultado.Location = new System.Drawing.Point(616, 323);
            this.grpResultado.Name = "grpResultado";
            this.grpResultado.Size = new System.Drawing.Size(157, 178);
            this.grpResultado.TabIndex = 8;
            this.grpResultado.TabStop = false;
            this.grpResultado.Text = "Resultado conteúdos";
            // 
            // grpDestinoFinal
            // 
            this.grpDestinoFinal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDestinoFinal.Controls.Add(this.cbDestinoFinal);
            this.grpDestinoFinal.Location = new System.Drawing.Point(6, 19);
            this.grpDestinoFinal.Name = "grpDestinoFinal";
            this.grpDestinoFinal.Size = new System.Drawing.Size(146, 48);
            this.grpDestinoFinal.TabIndex = 11;
            this.grpDestinoFinal.TabStop = false;
            this.grpDestinoFinal.Text = "Destino final";
            // 
            // cbDestinoFinal
            // 
            this.cbDestinoFinal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDestinoFinal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDestinoFinal.Location = new System.Drawing.Point(8, 18);
            this.cbDestinoFinal.Name = "cbDestinoFinal";
            this.cbDestinoFinal.Size = new System.Drawing.Size(130, 21);
            this.cbDestinoFinal.TabIndex = 1;
            // 
            // grpPublicacao
            // 
            this.grpPublicacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPublicacao.Controls.Add(this.chkPublicar);
            this.grpPublicacao.Location = new System.Drawing.Point(6, 127);
            this.grpPublicacao.Name = "grpPublicacao";
            this.grpPublicacao.Size = new System.Drawing.Size(146, 46);
            this.grpPublicacao.TabIndex = 12;
            this.grpPublicacao.TabStop = false;
            this.grpPublicacao.Text = "Publicação";
            // 
            // chkPublicar
            // 
            this.chkPublicar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkPublicar.Location = new System.Drawing.Point(12, 17);
            this.chkPublicar.Name = "chkPublicar";
            this.chkPublicar.Size = new System.Drawing.Size(85, 24);
            this.chkPublicar.TabIndex = 0;
            this.chkPublicar.Text = "Publicar";
            // 
            // ControlAutoEliminacao1
            // 
            this.ControlAutoEliminacao1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ControlAutoEliminacao1.ContentsEnabled = true;
            this.ControlAutoEliminacao1.Location = new System.Drawing.Point(6, 73);
            this.ControlAutoEliminacao1.Name = "ControlAutoEliminacao1";
            this.ControlAutoEliminacao1.Size = new System.Drawing.Size(148, 48);
            this.ControlAutoEliminacao1.TabIndex = 17;
            // 
            // grpFiltros
            // 
            this.grpFiltros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFiltros.Controls.Add(this.grpFltDestinoFinal);
            this.grpFiltros.Controls.Add(this.grpFltPrazo);
            this.grpFiltros.Controls.Add(this.grpFltAuto);
            this.grpFiltros.Location = new System.Drawing.Point(616, 150);
            this.grpFiltros.Name = "grpFiltros";
            this.grpFiltros.Size = new System.Drawing.Size(157, 167);
            this.grpFiltros.TabIndex = 17;
            this.grpFiltros.TabStop = false;
            this.grpFiltros.Text = "Filtros";
            // 
            // grpFltDestinoFinal
            // 
            this.grpFltDestinoFinal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFltDestinoFinal.Controls.Add(this.cbFltDestinoFinal);
            this.grpFltDestinoFinal.Location = new System.Drawing.Point(6, 12);
            this.grpFltDestinoFinal.Name = "grpFltDestinoFinal";
            this.grpFltDestinoFinal.Size = new System.Drawing.Size(146, 44);
            this.grpFltDestinoFinal.TabIndex = 14;
            this.grpFltDestinoFinal.TabStop = false;
            this.grpFltDestinoFinal.Text = "Destino final";
            // 
            // cbFltDestinoFinal
            // 
            this.cbFltDestinoFinal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFltDestinoFinal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFltDestinoFinal.Location = new System.Drawing.Point(8, 16);
            this.cbFltDestinoFinal.Name = "cbFltDestinoFinal";
            this.cbFltDestinoFinal.Size = new System.Drawing.Size(130, 21);
            this.cbFltDestinoFinal.TabIndex = 2;
            // 
            // grpFltPrazo
            // 
            this.grpFltPrazo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFltPrazo.Controls.Add(this.cbFltPrazo);
            this.grpFltPrazo.Location = new System.Drawing.Point(6, 62);
            this.grpFltPrazo.Name = "grpFltPrazo";
            this.grpFltPrazo.Size = new System.Drawing.Size(146, 44);
            this.grpFltPrazo.TabIndex = 15;
            this.grpFltPrazo.TabStop = false;
            this.grpFltPrazo.Text = "Prazo";
            // 
            // cbFltPrazo
            // 
            this.cbFltPrazo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFltPrazo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFltPrazo.Enabled = false;
            this.cbFltPrazo.Items.AddRange(new object[] {
            "Todos",
            "Dentro do Prazo",
            "Fora do Prazo"});
            this.cbFltPrazo.Location = new System.Drawing.Point(8, 16);
            this.cbFltPrazo.Name = "cbFltPrazo";
            this.cbFltPrazo.Size = new System.Drawing.Size(130, 21);
            this.cbFltPrazo.TabIndex = 2;
            // 
            // grpFltAuto
            // 
            this.grpFltAuto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFltAuto.Controls.Add(this.cbFltAuto);
            this.grpFltAuto.Location = new System.Drawing.Point(6, 112);
            this.grpFltAuto.Name = "grpFltAuto";
            this.grpFltAuto.Size = new System.Drawing.Size(146, 44);
            this.grpFltAuto.TabIndex = 16;
            this.grpFltAuto.TabStop = false;
            this.grpFltAuto.Text = "Nº Auto de eliminação";
            // 
            // cbFltAuto
            // 
            this.cbFltAuto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFltAuto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFltAuto.Enabled = false;
            this.cbFltAuto.Items.AddRange(new object[] {
            "Todos",
            "Com Auto",
            "Sem Auto"});
            this.cbFltAuto.Location = new System.Drawing.Point(8, 16);
            this.cbFltAuto.Name = "cbFltAuto";
            this.cbFltAuto.Size = new System.Drawing.Size(130, 21);
            this.cbFltAuto.TabIndex = 2;
            // 
            // grpConteudoPasso2
            // 
            this.grpConteudoPasso2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpConteudoPasso2.Controls.Add(this.grpSeleccaoUnidadesFisicas);
            this.grpConteudoPasso2.Location = new System.Drawing.Point(6, 19);
            this.grpConteudoPasso2.Name = "grpConteudoPasso2";
            this.grpConteudoPasso2.Size = new System.Drawing.Size(782, 569);
            this.grpConteudoPasso2.TabIndex = 2;
            this.grpConteudoPasso2.TabStop = false;
            this.grpConteudoPasso2.Visible = false;
            // 
            // grpSeleccaoUnidadesFisicas
            // 
            this.grpSeleccaoUnidadesFisicas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSeleccaoUnidadesFisicas.Controls.Add(this.lvwSeleccaoUnidadesFisicas);
            this.grpSeleccaoUnidadesFisicas.Controls.Add(this.btnAdd);
            this.grpSeleccaoUnidadesFisicas.Controls.Add(this.btnRemove);
            this.grpSeleccaoUnidadesFisicas.Location = new System.Drawing.Point(7, 16);
            this.grpSeleccaoUnidadesFisicas.Name = "grpSeleccaoUnidadesFisicas";
            this.grpSeleccaoUnidadesFisicas.Size = new System.Drawing.Size(766, 545);
            this.grpSeleccaoUnidadesFisicas.TabIndex = 3;
            this.grpSeleccaoUnidadesFisicas.TabStop = false;
            this.grpSeleccaoUnidadesFisicas.Text = "Unidades físicas";
            // 
            // lvwSeleccaoUnidadesFisicas
            // 
            this.lvwSeleccaoUnidadesFisicas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwSeleccaoUnidadesFisicas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSelUFCodigoParcial,
            this.chSelUFDesignacao,
            this.chSelUFAutosEliminacaoDocumentos,
            this.chSelUFAutosEliminacaoRestantes});
            this.lvwSeleccaoUnidadesFisicas.FullRowSelect = true;
            this.lvwSeleccaoUnidadesFisicas.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwSeleccaoUnidadesFisicas.HideSelection = false;
            this.lvwSeleccaoUnidadesFisicas.Location = new System.Drawing.Point(7, 18);
            this.lvwSeleccaoUnidadesFisicas.Name = "lvwSeleccaoUnidadesFisicas";
            this.lvwSeleccaoUnidadesFisicas.Size = new System.Drawing.Size(725, 520);
            this.lvwSeleccaoUnidadesFisicas.TabIndex = 2;
            this.lvwSeleccaoUnidadesFisicas.UseCompatibleStateImageBehavior = false;
            this.lvwSeleccaoUnidadesFisicas.View = System.Windows.Forms.View.Details;
            // 
            // chSelUFCodigoParcial
            // 
            this.chSelUFCodigoParcial.Text = "Código parcial";
            this.chSelUFCodigoParcial.Width = 74;
            // 
            // chSelUFDesignacao
            // 
            this.chSelUFDesignacao.Text = "Designação";
            this.chSelUFDesignacao.Width = 227;
            // 
            // chSelUFAutosEliminacaoDocumentos
            // 
            this.chSelUFAutosEliminacaoDocumentos.Text = "Autos de eliminação dos documentos";
            this.chSelUFAutosEliminacaoDocumentos.Width = 160;
            // 
            // chSelUFAutosEliminacaoRestantes
            // 
            this.chSelUFAutosEliminacaoRestantes.Text = "Autos de eliminação restantes";
            this.chSelUFAutosEliminacaoRestantes.Width = 160;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(736, 36);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(24, 24);
            this.btnAdd.TabIndex = 3;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(736, 64);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(24, 24);
            this.btnRemove.TabIndex = 4;
            // 
            // PanelAvaliacaoDocumentosUnidadesFisicas
            // 
            this.Controls.Add(this.grpAvaliacaoSeleccaoEliminacao);
            this.MinSize = new System.Drawing.Size(560, 516);
            this.Name = "PanelAvaliacaoDocumentosUnidadesFisicas";
            this.grpAvaliacaoSeleccaoEliminacao.ResumeLayout(false);
            this.grpConteudoPasso1.ResumeLayout(false);
            this.grpUnidadesFisicas.ResumeLayout(false);
            this.grpDocumentos.ResumeLayout(false);
            this.grpResultado.ResumeLayout(false);
            this.grpDestinoFinal.ResumeLayout(false);
            this.grpPublicacao.ResumeLayout(false);
            this.grpFiltros.ResumeLayout(false);
            this.grpFltDestinoFinal.ResumeLayout(false);
            this.grpFltPrazo.ResumeLayout(false);
            this.grpFltAuto.ResumeLayout(false);
            this.grpConteudoPasso2.ResumeLayout(false);
            this.grpSeleccaoUnidadesFisicas.ResumeLayout(false);
            this.ResumeLayout(false);

		}
	#endregion

		private void GetExtraResources()
		{
			btnAdd.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
			btnRemove.Image = SharedResourcesOld.CurrentSharedResources.Apagar;

			if (! DesignMode)
				base.ParentChanged += PanelAvaliacaoDocumentosUnidadesFisicas_ParentChanged;
		}

		// runs only once. sets tooltips as soon as it's parent appears
		private void PanelAvaliacaoDocumentosUnidadesFisicas_ParentChanged(object sender, System.EventArgs e)
		{
			MultiPanel.CurrentToolTip.SetToolTip(btnAdd, SharedResourcesOld.CurrentSharedResources.AdicionarString);
			MultiPanel.CurrentToolTip.SetToolTip(btnRemove, SharedResourcesOld.CurrentSharedResources.ApagarString);
			base.ParentChanged -= PanelAvaliacaoDocumentosUnidadesFisicas_ParentChanged;
		}

		private GISADataset.FRDBaseRow CurrentFRDBase;
		private GISADataset.SFRDAvaliacaoRow CurrentSFRDAvaliacao = null;

		private bool isInactiveEstruturalPanel = false;

		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			isInactiveEstruturalPanel = false;
			CurrentFRDBase = (GISADataset.FRDBaseRow)CurrentDataRow;
			GISADataset.RelacaoHierarquicaRow rhRow = null;
			rhRow = CurrentFRDBase.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0];

            // não se carrega a row referente à avaliação do nível selecionado quando existe uma em memória e modificada; nos
            // casos restantes essa row é carregada ou criada uma nova
            GISADataset.SFRDAvaliacaoRow[] avaliacaoRows = null;
            avaliacaoRows = CurrentFRDBase.GetSFRDAvaliacaoRows();
            if (avaliacaoRows.Length != 0)
            {
                CurrentSFRDAvaliacao = CurrentFRDBase.GetSFRDAvaliacaoRows()[0];

                if (CurrentSFRDAvaliacao.RowState == DataRowState.Unchanged)
                    FRDRule.Current.LoadCurrentFRDAvaliacao(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, conn);
            }
            else
            {
                FRDRule.Current.LoadCurrentFRDAvaliacao(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, conn);

                avaliacaoRows = CurrentFRDBase.GetSFRDAvaliacaoRows();
                if (avaliacaoRows.Length != 0)
                    CurrentSFRDAvaliacao = CurrentFRDBase.GetSFRDAvaliacaoRows()[0];
                else
                {
                    // Criar um SFRDAvaliação caso não exista ainda
                    CurrentSFRDAvaliacao = GisaDataSetHelper.GetInstance().SFRDAvaliacao.NewSFRDAvaliacaoRow();
                    CurrentSFRDAvaliacao.FRDBaseRow = CurrentFRDBase;
                    CurrentSFRDAvaliacao.IDPertinencia = 1;
                    CurrentSFRDAvaliacao.IDDensidade = 1;
                    CurrentSFRDAvaliacao.IDSubdensidade = 1;
                    CurrentSFRDAvaliacao.AvaliacaoTabela = false;
                    CurrentSFRDAvaliacao.Publicar = false;
                    GisaDataSetHelper.GetInstance().SFRDAvaliacao.AddSFRDAvaliacaoRow(CurrentSFRDAvaliacao);
                }
            }

            // uma vez que nenhuma informação é apresentada, o booleano IsLoaded continua a false (para o 
            // caso das séries dá jeito porque o estarem avaliadas ou não influencia o facto do painel estar 
            // enabled ou não)
			if ( rhRow.IDTipoNivelRelacionado != TipoNivelRelacionado.SR && 
                 rhRow.IDTipoNivelRelacionado != TipoNivelRelacionado.SSR && 
                 rhRow.IDTipoNivelRelacionado != TipoNivelRelacionado.D && 
                 rhRow.NivelRowByNivelRelacaoHierarquicaUpper.IDTipoNivel == TipoNivel.ESTRUTURAL )
			{
				grpConteudoPasso1.Enabled = false;
				grpConteudoPasso2.Enabled = false;
				btnProximo.Enabled = false;
				btnAnterior.Enabled = false;
				return;
			}
			else
			{
                grpConteudoPasso1.Enabled = true;
                grpConteudoPasso2.Enabled = true;
                btnProximo.Enabled = true;
                btnAnterior.Enabled = true;
                // no caso do nível documental selecionado ser uma (sub)série, este painel só pode estar activo caso esta
                // esteja avaliada
                if ( !((rhRow.IDTipoNivelRelacionado == TipoNivelRelacionado.SR || 
                        rhRow.IDTipoNivelRelacionado == TipoNivelRelacionado.SSR ) &&
                    !CurrentSFRDAvaliacao.IsPreservarNull()) )
                {
                    isInactiveEstruturalPanel = true;
                    GUIHelper.GUIHelper.makeReadOnly(this);
                    return;
                }
			}
            
            if (CurrentFRDBase.NivelRow.TipoNivelRow.ID == TipoNivel.DOCUMENTAL)
			{
                ControlAutoEliminacao1.ContentsEnabled = false;

                DBAbstractDataLayer.DataAccessRules.NivelRule.Current.GetUfsEDocsAssociados(CurrentFRDBase.NivelRow.ID, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID, conn);
                DocumentosAssociados = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.GetDocsAssoc();
                UnidadesFisicasAssociadas = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.GetUfsAssoc();
                UfsDocsAssoc = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.GetUfsDocsAssoc();
                UfsSeriesAssoc = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.GetUfsSeriesAssoc();

                //fazer load de informação necessária cruzada com os resultados nas tabelas temporárias calculadas anteriormente
                long startTicks = DateTime.Now.Ticks;
                FRDRule.Current.LoadPanelAvaliacaoDocumentosUnidadesFisicasData(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, CurrentFRDBase.NivelRow.ID, Convert.ToInt64(CurrentFRDBase.IDTipoFRDBase), Convert.ToInt64(TipoFRDBase.FRDUnidadeFisica), PermissoesHelper.GrpAcessoPublicados.ID, conn);
                Debug.WriteLine("<<LoadPanelAvaliacaoDocumentosUnidadesFisicasData>>: " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());

                GUIHelper.GUIHelper.makeReadable(this);
			}
			else
			{
				isInactiveEstruturalPanel = true;
                GUIHelper.GUIHelper.makeReadOnly(this);
			}
			IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = false;
			if (! isInactiveEstruturalPanel)
			{
				cbFltPrazo.SelectedIndex = 0;
                cbFltPrazo.Enabled = true;
				cbFltAuto.SelectedIndex = 0;
				cbFltAuto.Enabled = false;
                
				DataTable dtDestino = new DataTable();
				dtDestino.Columns.Add("ID", typeof(int));
				dtDestino.Columns.Add("Designacao", typeof(string));
				dtDestino.Rows.Add(new object[] {-1, string.Empty});
				dtDestino.Rows.Add(new object[] {1, "Conservação"});
				dtDestino.Rows.Add(new object[] {0, "Eliminação"});

				cbDestinoFinal.DataSource = dtDestino;
				cbDestinoFinal.DisplayMember = "Designacao";
				cbDestinoFinal.ValueMember = "ID";

                dtDestino = new DataTable();
				dtDestino.Columns.Add("ID", typeof(int));
				dtDestino.Columns.Add("Designacao", typeof(string));
				dtDestino.Rows.Add(new object[] {-1, "Por avaliar"});
				dtDestino.Rows.Add(new object[] {1, "Conservação"});
				dtDestino.Rows.Add(new object[] {0, "Eliminação"});

                cbFltDestinoFinal.DataSource = dtDestino;
                cbFltDestinoFinal.DisplayMember = "Designacao";
                cbFltDestinoFinal.ValueMember = "ID";
                cbFltDestinoFinal.SelectedIndex = (int)EstadosFiltroDestinoFinal.PorAvaliar;

				ControlAutoEliminacao1.rebindToData();

                DestinoFinalAgregado = -2;
                PublicacaoAgregada = -2;
                AutoEliminacaoAgregado = Convert.ToInt64(AgregacaoAEResult.SemSeleccao);

                if (DocumentosAssociados != null)
                    CacheLVItemsDocs();

                PopulateLists();

                AddHandlers();
            }
			IsPopulated = true;
		}

		public override void ViewToModel()
		{

		}

		public override void Deactivate()
		{
			CurrentFRDBase = null;
			CurrentSFRDAvaliacao = null;
            GUIHelper.GUIHelper.clearField(lvwDocumentos);
            updateResultsTimer.Stop();
            RemoveHandlers();
			DestinoFinalAgregado = -2;
			PublicacaoAgregada = -2;
			AutoEliminacaoAgregado = Convert.ToInt64(AgregacaoAEResult.SemSeleccao);
			grpDocumentos.Text = "Documentos";
            GUIHelper.GUIHelper.clearField(lvwUnidadesFisicas);
			grpUnidadesFisicas.Text = "Unidades físicas";
			cbDestinoFinal.DataSource = null;
            GUIHelper.GUIHelper.clearField(cbDestinoFinal);
            GUIHelper.GUIHelper.clearField(chkPublicar);
			btnAnterior_Click(this, new System.EventArgs());
            GUIHelper.GUIHelper.clearField(lvwSeleccaoUnidadesFisicas);
			ControlAutoEliminacao1.cbAutoEliminacao.DataSource = null;
            GUIHelper.GUIHelper.clearField(ControlAutoEliminacao1.cbAutoEliminacao);
            
            if (DocumentosAssociados != null) 
                DocumentosAssociados.Clear();

            if (UnidadesFisicasAssociadas != null) 
                UnidadesFisicasAssociadas.Clear();

            if (lvItemsDocumentos != null) 
                lvItemsDocumentos.Clear();

            if (UfsDocsAssoc != null) 
                UfsDocsAssoc.Clear();

            if (UfsSeriesAssoc != null) 
                UfsSeriesAssoc.Clear();

            if (AvaliacaoDocs != null) 
                AvaliacaoDocs.Clear();
		}

        private void AddHandlers()
        {
            cbDestinoFinal.SelectedIndexChanged += cbDestinoFinal_SelectedIndexChanged;
            chkPublicar.CheckedChanged += chkPublicar_CheckedChanged;
            ControlAutoEliminacao1.cbAutoEliminacao.SelectedIndexChanged += cbAutoEliminacao_SelectedIndexChanged;

            cbFltDestinoFinal.SelectedIndexChanged += chkAnteriormenteAvaliados_CheckedChanged;
            cbFltPrazo.SelectedIndexChanged += chkAnteriormenteAvaliados_CheckedChanged;
            cbFltAuto.SelectedIndexChanged += chkAnteriormenteAvaliados_CheckedChanged;
        }

        private void RemoveHandlers()
        {
            cbDestinoFinal.SelectedIndexChanged -= cbDestinoFinal_SelectedIndexChanged;
            chkPublicar.CheckedChanged -= chkPublicar_CheckedChanged;
            ControlAutoEliminacao1.cbAutoEliminacao.SelectedIndexChanged -= cbAutoEliminacao_SelectedIndexChanged;

            cbFltDestinoFinal.SelectedIndexChanged -= chkAnteriormenteAvaliados_CheckedChanged;
            cbFltPrazo.SelectedIndexChanged -= chkAnteriormenteAvaliados_CheckedChanged;
            cbFltAuto.SelectedIndexChanged -= chkAnteriormenteAvaliados_CheckedChanged;
        }

		// O seguinte é um resumo das estruturas de dados em uso neste painel e o que contêm
		// DocumentosAssociados		    - Dicionário ordenado de todos os DocumentoAssociados
        // UnidadesFisicasAssociadas	    - Dicionário de todos os UnidadeFisicaAssociadas
        // lvItemsDocumentos 		    - Dicionário ordenado de items que será sujeita a filtros. nRows.ID -> items
        // UfsDocsAssoc          - Dicionário que mantém a lista de Documentos associados a uma dada Unidade Física
        // UfsSeriesAssoc          - Dicionário que mantém a lista de (Sub)Séries associadas a uma dada Unidade Física
        // AvaliacaoDocs        - Dicionário que contém as SFRDAvaliacaoRow de cada documento

	#region  Populate lists 
        private OrderedDictionary lvItemsDocumentos = new OrderedDictionary();
        private Dictionary<long, GISADataset.SFRDAvaliacaoRow> AvaliacaoDocs = new Dictionary<long, GISADataset.SFRDAvaliacaoRow>();
        private void CacheLVItemsDocs()
        {
            long startTicks = 0;
            ((frmMain)(this.TopLevelControl)).EnterWaitMode();
            try
            {
                startTicks = DateTime.Now.Ticks;

                List<ListViewItem> itemsDocs = new List<ListViewItem>();
                foreach (NivelRule.DocumentoAssociado doc in DocumentosAssociados.Values)
                    itemsDocs.Add(addDocumentoToList(doc));

                Debug.WriteLine("<<CacheLVItemsDocs>> " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                throw;
            }
            finally
            {
                ((frmMain)(this.TopLevelControl)).LeaveWaitMode();
            }
        }

        private void PopulateLists()
        {
            if (DocumentosAssociados != null)
                PopulateDocList();

            if (UnidadesFisicasAssociadas != null)
                PopulateUFList();
        }

        private void PopulateUFList()
        {
            long startTicks = DateTime.Now.Ticks;
            ((frmMain)(this.TopLevelControl)).EnterWaitMode();

            try
            {
                lvwUnidadesFisicas.BeginUpdate();
                lvwUnidadesFisicas.Items.Clear();
                List<ListViewItem> itemsUFs = new List<ListViewItem>();
                foreach (NivelRule.UnidadeFisicaAssociada uf in UnidadesFisicasAssociadas.Values)
                {
                    // adicionar apenas as unidades físicas que possam ser úteis como agrupadores de documentos
                    if (UfsDocsAssoc.ContainsKey(uf.IDNivel) && UfsDocsAssoc[uf.IDNivel].Count > 0 && !uf.IsNotDocRelated)
                        itemsUFs.Add(AddUnidadeFisicaToList(uf));
                }

                if (itemsUFs.Count > 0)
                    lvwUnidadesFisicas.Items.AddRange(itemsUFs.ToArray());

                lvwUnidadesFisicas.EndUpdate();
                Debug.WriteLine("<<UFs>> " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());
                grpUnidadesFisicas.Text = string.Format("Unidades físicas ({0})", itemsUFs.Count);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                throw;
            }
            finally
            {
                ((frmMain)(this.TopLevelControl)).LeaveWaitMode();
            }
        }

        // Os items a serem mostrados já devem estar de acordo com os filtros
		private void PopulateDocList()
		{
			long startTicks = 0;
			((frmMain)(this.TopLevelControl)).EnterWaitMode();
            try
            {
                startTicks = DateTime.Now.Ticks;
                lvwDocumentos.BeginUpdate();
                lvwDocumentos.Items.Clear();
                long totalDocs = 0;

                List<ListViewItem> itemsDocs = new List<ListViewItem>();
                foreach (NivelRule.DocumentoAssociado doc in DocumentosAssociados.Values)
                {
                    if (doc.IDTipoNivelRelacionado == TipoNivelRelacionado.D)
                    {
                        totalDocs += 1;

                        if ((((int)cbFltDestinoFinal.SelectedValue == -1 && doc.Preservar.Length == 0) || doc.Preservar.Equals(cbFltDestinoFinal.SelectedValue.ToString())) &&
                            (cbFltPrazo.SelectedIndex == 0 || (cbFltPrazo.SelectedIndex == 1 && (doc.Expirado.Length == 0 || doc.Expirado.Equals("0"))) || (cbFltPrazo.SelectedIndex == 2 && doc.Expirado.Equals("1"))) &&
                            (cbFltAuto.SelectedIndex == 0 || (cbFltAuto.SelectedIndex == 1 && doc.IDAutoEliminacao.Length > 0) || (cbFltAuto.SelectedIndex == 2 && doc.IDAutoEliminacao.Length == 0)))

                            itemsDocs.Add((ListViewItem)lvItemsDocumentos[doc.IDNivel]);
                    }
                }

                if (itemsDocs.Count > 0)
                    lvwDocumentos.Items.AddRange(itemsDocs.ToArray());

                lvwDocumentos.EndUpdate();
                Debug.WriteLine("<<PopulateDocList>> " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());
                grpDocumentos.Text = string.Format("Documentos ({0}/{1})", lvwDocumentos.Items.Count, totalDocs);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                throw;
            }
			finally
			{
				((frmMain)(this.TopLevelControl)).LeaveWaitMode();
			}
		}

		private void PopulateSeleccaoUnidadesFisicas()
		{
            long startTicks = DateTime.Now.Ticks;
            ((frmMain)(this.TopLevelControl)).EnterWaitMode();

            try
            {
                lvwSeleccaoUnidadesFisicas.BeginUpdate();
                lvwSeleccaoUnidadesFisicas.Items.Clear();

                List<ListViewItem> itemsUFs = new List<ListViewItem>();
                foreach (NivelRule.UnidadeFisicaAssociada uf in UnidadesFisicasAssociadas.Values)
                {
                    // adicionar apenas as unidades físicas associadas a séries e 
                    // subséries. apenas estas podem ser seleccionadas manualmente 
                    // para autos de eliminação
                    if (uf.IsSerieRelated)
                        itemsUFs.Add(AddSelUnidadeFisicaToList(uf));
                }

                if (itemsUFs.Count > 0)
                    lvwSeleccaoUnidadesFisicas.Items.AddRange(itemsUFs.ToArray());

                lvwSeleccaoUnidadesFisicas.EndUpdate();
                grpSeleccaoUnidadesFisicas.Text = string.Format("Unidades físicas ({0})", itemsUFs.Count);
                
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                throw;
            }
            finally
            {
                ((frmMain)(this.TopLevelControl)).LeaveWaitMode();
            }
		}
	#endregion

	#region  Add items to the lists and/or update their data 
        private ListViewItem addDocumentoToList(NivelRule.DocumentoAssociado doc)
		{
            ListViewItem item = new ListViewItem();
            for (int i = 0; i < lvwDocumentos.Columns.Count; i++)
                item.SubItems.Add(string.Empty);

            item.Tag = doc.IDNivel;
            item.Text = doc.Designacao;
            item.SubItems[chDocSerieSubserie.Index].Text = doc.DesignacaoUpper;
            item.SubItems[chDocDatasProducao.Index].Text =
                GISA.Utils.GUIHelper.FormatDateInterval(doc.InicioAno, doc.InicioMes, doc.InicioDia, doc.FimAno, doc.FimMes, doc.FimDia);

            GISADataset.NivelRow [] nRows = (GISADataset.NivelRow[])GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + doc.IDNivel);
            Debug.Assert(nRows.Length == 1);

            GISADataset.SFRDAvaliacaoRow frdAvaliacaoRow = null;
            frdAvaliacaoRow = getAvaliacao(nRows[0]);

            Debug.Assert(frdAvaliacaoRow != null);

            item.SubItems[chDocDestino.Index].Text = GUIHelper.GUIHelper.formatDestinoFinal(frdAvaliacaoRow);
            item.SubItems[chDocPublicado.Index].Text = TranslationHelper.FormatBoolean(frdAvaliacaoRow.Publicar);
            item.SubItems[chDocAutoEliminacao.Index].Text = GUIHelper.GUIHelper.formatAutoEliminacao(frdAvaliacaoRow);

            if (!AvaliacaoDocs.ContainsKey(doc.IDNivel))
                AvaliacaoDocs.Add(doc.IDNivel, frdAvaliacaoRow);

            lvItemsDocumentos.Add(doc.IDNivel, item);

            return item;
		}

        private ListViewItem AddUnidadeFisicaToList(NivelRule.UnidadeFisicaAssociada uf)
		{
            ListViewItem item = new ListViewItem();
            for (int i = 0; i < lvwUnidadesFisicas.Columns.Count; i++)
                item.SubItems.Add(string.Empty);

            item.Tag = uf.IDNivel;
            item.SubItems[chUFCodigo.Index].Text = uf.Codigo;
            item.SubItems[chUFDesignacao.Index].Text = uf.Designacao;
            item.SubItems[chUFDatasProducao.Index].Text = 
                GISA.Utils.GUIHelper.FormatDateInterval(uf.InicioAno, uf.InicioMes, uf.InicioDia, uf.FimAno, uf.FimMes, uf.FimDia);
            if (UfsDocsAssoc.ContainsKey(uf.IDNivel))
                item.SubItems[chUFNumDocumentos.Index].Text = UfsDocsAssoc[uf.IDNivel].Count.ToString();
            else
                item.SubItems[chUFNumDocumentos.Index].Text = string.Empty;

            return item;
		}

        private ListViewItem AddSelUnidadeFisicaToList(NivelRule.UnidadeFisicaAssociada uf)
		{
            ListViewItem item = new ListViewItem();
            for (int i = 0; i < lvwSeleccaoUnidadesFisicas.Columns.Count; i++)
                item.SubItems.Add(string.Empty);

            item.Tag = uf.IDNivel;
            item.SubItems[chSelUFCodigoParcial.Index].Text = uf.Codigo;
            item.SubItems[chSelUFDesignacao.Index].Text = uf.Designacao;
            item.SubItems[chSelUFAutosEliminacaoDocumentos.Index].Text = CollectTextAutosEliminacaoDocumentos(uf);
            item.SubItems[chSelUFAutosEliminacaoRestantes.Index].Text = CollectTextAutosEliminacaoRestantes(uf);

            return item;
		}
        
        private void refreshSelUFItem(ListViewItem item, int columnIndex)
		{
            NivelRule.UnidadeFisicaAssociada uf = (NivelRule.UnidadeFisicaAssociada)UnidadesFisicasAssociadas[(long)item.Tag];

            Debug.Assert(item.ListView == lvwSeleccaoUnidadesFisicas);

			if (columnIndex == -1)
			{
				item.SubItems[chSelUFCodigoParcial.Index].Text = uf.Codigo;
				item.SubItems[chSelUFDesignacao.Index].Text = uf.Designacao;
				item.SubItems[chSelUFAutosEliminacaoDocumentos.Index].Text = CollectTextAutosEliminacaoDocumentos(uf);
				item.SubItems[chSelUFAutosEliminacaoRestantes.Index].Text = CollectTextAutosEliminacaoRestantes(uf);
			}
			else if (columnIndex == chSelUFAutosEliminacaoRestantes.Index)
                item.SubItems[chSelUFAutosEliminacaoRestantes.Index].Text = CollectTextAutosEliminacaoRestantes(uf);
		}

        private string CollectTextAutosEliminacaoDocumentos(NivelRule.UnidadeFisicaAssociada uf)
		{
			GISADataset.SFRDAvaliacaoRow sfrdAvaliacaoRow = null;
			Hashtable aeRowsHT = new Hashtable();
            List<long> niveisDoc = new List<long>();
            
            if (UfsDocsAssoc.ContainsKey(uf.IDNivel))
                niveisDoc.AddRange(UfsDocsAssoc[uf.IDNivel]);
            if (UfsSeriesAssoc.ContainsKey(uf.IDNivel))
                niveisDoc.AddRange(UfsSeriesAssoc[uf.IDNivel]);

			// obter referencias aos autos de eliminacao dos documentos
			foreach (long docID in niveisDoc)
			{
                NivelRule.DocumentoAssociado doc = (NivelRule.DocumentoAssociado)DocumentosAssociados[docID];
				// Apenas considerar niveis documentais. A associação de UFs associadas a níveis 
				// estruturais é irrelevante na selecção.
				if (doc.IDTipoNivelRelacionado >= TipoNivelRelacionado.SR)
				{
                    sfrdAvaliacaoRow = AvaliacaoDocs[docID];
					if (sfrdAvaliacaoRow.AutoEliminacaoRow != null)
						aeRowsHT[sfrdAvaliacaoRow.AutoEliminacaoRow] = sfrdAvaliacaoRow.AutoEliminacaoRow;
				}
			}

			System.Text.StringBuilder autosEliminacaoDocumentos = null;
			foreach (GISADataset.AutoEliminacaoRow aeRow in aeRowsHT.Values)
			{
				if (autosEliminacaoDocumentos == null)
					autosEliminacaoDocumentos = new System.Text.StringBuilder();
				else
					autosEliminacaoDocumentos.Append("; ");

				autosEliminacaoDocumentos.Append(aeRow.Designacao);
			}

			if (autosEliminacaoDocumentos == null)
				return string.Empty;
			else
				return autosEliminacaoDocumentos.ToString();
		}

		private string CollectTextAutosEliminacaoRestantes(NivelRule.UnidadeFisicaAssociada uf)
		{
			GISADataset.FRDBaseRow[] frdRows = null;
			GISADataset.FRDBaseRow frdRow = null;
            GISADataset.NivelRow nRow = null;
            nRow = (GISADataset.NivelRow)GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + uf.IDNivel.ToString())[0];
			Hashtable aeRowsHT = new Hashtable();

			// obter referencias aos autos de eliminacao das unidades físicas
			frdRows = nRow.GetFRDBaseRows();
			if (frdRows.Length > 0)
				frdRow = getFRDUnidadeFisica(frdRows);

			if (frdRow != null)
			{
				foreach (GISADataset.SFRDUFAutoEliminacaoRow sfrdaeRow in frdRow.GetSFRDUFAutoEliminacaoRows())
					aeRowsHT[sfrdaeRow.AutoEliminacaoRow] = sfrdaeRow.AutoEliminacaoRow;
			}

			System.Text.StringBuilder autosEliminacaoRestantes = null;
			if (frdRow != null)
			{
				foreach (GISADataset.AutoEliminacaoRow aeRow in aeRowsHT.Values)
				{
					if (autosEliminacaoRestantes == null)
						autosEliminacaoRestantes = new System.Text.StringBuilder();
					else
						autosEliminacaoRestantes.Append("; ");
					autosEliminacaoRestantes.Append(aeRow.Designacao);
				}
			}

			if (autosEliminacaoRestantes == null)
				return string.Empty;
			else
				return autosEliminacaoRestantes.ToString();
		}
	#endregion

	#region  Obtenção de datarows do dataset de acordo com certas restrições 
		private static GISADataset.FRDBaseRow getFRDRecolha(GISADataset.FRDBaseRow[] rows)
		{
			foreach (GISADataset.FRDBaseRow row in rows)
			{
				if (row.TipoFRDBaseRow.ID == Convert.ToInt64(TipoFRDBase.FRDOIRecolha))
					return row;
			}
			return null;
		}

		private static GISADataset.FRDBaseRow getFRDUnidadeFisica(GISADataset.FRDBaseRow[] rows)
		{
			foreach (GISADataset.FRDBaseRow row in rows)
			{
				if (row.TipoFRDBaseRow.ID == Convert.ToInt64(TipoFRDBase.FRDUnidadeFisica))
					return row;
			}
			return null;
		}

		private GISADataset.SFRDAvaliacaoRow getAvaliacao(GISADataset.NivelRow nRow)
		{
			GISADataset.FRDBaseRow[] frdRows = null;
			GISADataset.FRDBaseRow frdRow = null;
			GISADataset.SFRDAvaliacaoRow[] frdAvaliacaoRows = null;
			GISADataset.SFRDAvaliacaoRow frdAvaliacaoRow = null;

			frdRows = nRow.GetFRDBaseRows();
			if (frdRows.Length > 0)
			{
				frdRow = getFRDRecolha(frdRows);
				frdAvaliacaoRows = frdRow.GetSFRDAvaliacaoRows();
				if (frdAvaliacaoRows.Length > 0)
					frdAvaliacaoRow = frdAvaliacaoRows[0];
			}

			if (frdRow == null)
			{
				// Criar uma nova FRD para este documento.
				frdRow = GisaDataSetHelper.GetInstance().FRDBase.NewFRDBaseRow();
				frdRow.NivelRow = nRow;
				frdRow.TipoFRDBaseRow = (GISADataset.TipoFRDBaseRow)(GisaDataSetHelper.GetInstance().TipoFRDBase.Select(string.Format("ID={0:d}", TipoFRDBase.FRDOIRecolha))[0]);
				frdRow.NotaDoArquivista = string.Empty;
				frdRow.RegrasOuConvencoes = string.Empty;
				GisaDataSetHelper.GetInstance().FRDBase.AddFRDBaseRow(frdRow);
			}

			if (frdAvaliacaoRow == null)
			{
				// criar uma nova SFRDAvaliacao para este documento
				frdAvaliacaoRow = GisaDataSetHelper.GetInstance().SFRDAvaliacao.NewSFRDAvaliacaoRow();
				frdAvaliacaoRow.FRDBaseRow = frdRow;
                CurrentSFRDAvaliacao.IDPertinencia = 1;
                CurrentSFRDAvaliacao.IDDensidade = 1;
                CurrentSFRDAvaliacao.IDSubdensidade = 1;
				frdAvaliacaoRow.Publicar = false;
                frdAvaliacaoRow.AvaliacaoTabela = false;
				// se não estivermos a usar avaliação todos os documentos/UFs são 
				// automaticamente marcados para conservação
				if (! GisaDataSetHelper.UsingGestaoIntegrada())
					frdAvaliacaoRow.Preservar = true;

				GisaDataSetHelper.GetInstance().SFRDAvaliacao.AddSFRDAvaliacaoRow(frdAvaliacaoRow);
			}

			return frdAvaliacaoRow;
		}
	#endregion

	#region  Actualizacao dos resultados de avaliação após uma mudança de contexto
		private void lvwDocumentos_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			RescheduleUpdateResults();
		}

		private void RescheduleUpdateResults()
		{
			updateResultsTimer.Stop();
			updateResultsTimer.Interval = 333; // 1/3 segundo
			updateResultsTimer.Start();
		}

		private void updateResultsTimer_Tick(object myObject, EventArgs myEventArgs)
		{
			updateResultsTimer.Stop();

            clearResults();

			// se o painel nãoe stiver carregado, ignorar actualizações.
			// isto pode acontecer quando são lançados eventos de alteração da selecção da listview durante o deactivate
            if (CurrentFRDBase != null)
                populateResultsContents();
		}
	#endregion

	#region  Selecção (checking) de unidades físicas e consequente selecção de documentos
		private void lvwUnidadesFisicas_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{
            RescheduleUpdateDocumentosSelection();
		}

		private void RescheduleUpdateDocumentosSelection()
		{
			updateDocumentosSelectionTimer.Stop();
			updateDocumentosSelectionTimer.Interval = 333; // 1/3 segundo
			updateDocumentosSelectionTimer.Start();
		}

		private void updateDocumentosSelectionTimer_Tick(object myObject, EventArgs myEventArgs)
		{
			updateDocumentosSelectionTimer.Stop();

			// TODO: deviamos actualizar apenas os documentos afectados pelo 
			// check que foi agora alterado, de forma a manter as alterações 
			// à selecção de documentos que possa entretanto ter siso feita 
			// "manualmente"
			refreshDocumentosSelection();
		}

		// Actualiza a lista de documentos de acordo com as unidades físicas seleccionadas
		private void refreshDocumentosSelection()
		{
            List<long> DocIDsParaAssociar = new List<long>();

			((frmMain)TopLevelControl).EnterWaitMode();
			lvwDocumentos.BeginUpdate();
			try
			{
				// Limpar os documentos selecionados
				lvwDocumentos.SelectedItems.Clear();

				// Para cada uma das unidades físicas checkadas
				foreach (ListViewItem ufItem in lvwUnidadesFisicas.CheckedItems)
				{
                    long UFID = ((NivelRule.UnidadeFisicaAssociada)UnidadesFisicasAssociadas[(long)ufItem.Tag]).IDNivel;
                    if (UfsDocsAssoc.ContainsKey(UFID))
                    {
                        foreach (long docID in UfsDocsAssoc[UFID])
                            ((ListViewItem)lvItemsDocumentos[docID]).Selected = true;
                    }
                }

				if (lvwDocumentos.SelectedItems.Count > 0)
					lvwDocumentos.EnsureVisible(lvwDocumentos.SelectedItems[lvwDocumentos.SelectedItems.Count - 1].Index);
			}
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                throw;
            }
			finally
			{
				lvwDocumentos.EndUpdate();
				((frmMain)TopLevelControl).LeaveWaitMode();
			}
		}
	#endregion

	#region  Manutenção dos documentos e unidades físicas associadas 
        private OrderedDictionary DocumentosAssociados;        

        private OrderedDictionary mUnidadesFisicasAssociadas;
        private OrderedDictionary UnidadesFisicasAssociadas
        {
            get { return mUnidadesFisicasAssociadas; }
            set { mUnidadesFisicasAssociadas = value; }
        }

        private Dictionary<long, List<long>> mUfsDocsAssoc;
        private Dictionary<long, List<long>> UfsDocsAssoc
        {
            get { return mUfsDocsAssoc; }
            set { mUfsDocsAssoc = value; }
        }

        private Dictionary<long, List<long>> mUfsSeriesAssoc;
        private Dictionary<long, List<long>> UfsSeriesAssoc
        {
            get { return mUfsSeriesAssoc; }
            set { mUfsSeriesAssoc = value; }
        }
	#endregion

	#region  Manutenção dos resultados agregados 
		private int mOriginalDestinoFinalAgregado;
		private int DestinoFinalAgregado
		{
			get {return mOriginalDestinoFinalAgregado;}
			set
			{
				mOriginalDestinoFinalAgregado = value;
				if (value < -1) // diferentes, valor de resultado nao editavel
				{
					cbDestinoFinal.SelectedIndex = -1;
					cbDestinoFinal.Enabled = false;
				}
				else
				{
					cbDestinoFinal.SelectedValue = value;
					cbDestinoFinal.Enabled = GisaDataSetHelper.UsingGestaoIntegrada();
				}
				updateResultsContentsState();
			}
		}

		private int mOriginalPublicacaoAgregada;
		private int PublicacaoAgregada
		{
			get {return mOriginalPublicacaoAgregada;}
			set
			{
				mOriginalPublicacaoAgregada = value;
				if (value < -1)
				{
					chkPublicar.Checked = false;
					chkPublicar.Enabled = false;
				}
				else
				{
					chkPublicar.Checked = (value == 1);
					chkPublicar.Enabled = true;
				}
			}
		}

		private long mOriginalAutoEliminacaoAgregado;
		private long AutoEliminacaoAgregado
		{
			get {return mOriginalAutoEliminacaoAgregado;}
			set
			{
				mOriginalAutoEliminacaoAgregado = value;
				if (value == (long)AgregacaoAEResult.selecionadosDiferentesAutosEliminacao || value == (long)AgregacaoAEResult.SemSeleccao) // diferentes ou simplesmente não especificados, valor de resultado nao editavel
				{
					ControlAutoEliminacao1.cbAutoEliminacao.SelectedIndex = -1;
					ControlAutoEliminacao1.ContentsEnabled = false;
				}
				else
				{
					ControlAutoEliminacao1.cbAutoEliminacao.SelectedValue = value;
					ControlAutoEliminacao1.ContentsEnabled = false;
				}
			}
		}

		private enum AgregacaoAEResult: long
		{
			SemSeleccao = long.MinValue + 1, // -2
			selecionadoNenhumAutoEliminacao = long.MinValue, // -1
			selecionadosDiferentesAutosEliminacao = long.MinValue + 2 // -3
		}
	#endregion

	#region  População dos resultados agregados 
		// Estabelece o contexto dos campos onde é definido o resultado da 
		// avaliação. Popula os controlos de resultado de avaliação com base na 
		// lista de items selecionados
		private void populateResultsContents()
		{
            cbDestinoFinal.SelectedIndexChanged -= cbDestinoFinal_SelectedIndexChanged;
            chkPublicar.CheckedChanged -= chkPublicar_CheckedChanged;
            ControlAutoEliminacao1.cbAutoEliminacao.SelectedIndexChanged -= cbAutoEliminacao_SelectedIndexChanged;

            GISADataset.SFRDAvaliacaoRow frdAvaliacaoRow = null;
            // -1  selecionado NULL. ocorre para o destino final "não avaliado" e para casos em que o auto de eliminação ainda não foi especificado. Nunca ocorre como valor da publicação.
            // -2  sem seleccao
            // -3  diferentes
            int DestinoFinalAgregadoTmp = -2;
            int PublicacaoAgregadaTmp = -2;
            long AutoEliminacaoAgregadoTmp = (long)AgregacaoAEResult.SemSeleccao;

            // Precorrer os items selecionados verificando no final se são todos iguais
            foreach (ListViewItem selectedItem in lvwDocumentos.SelectedItems)
            {
                NivelRule.DocumentoAssociado doc = (NivelRule.DocumentoAssociado)DocumentosAssociados[(long)selectedItem.Tag];
                if (!doc.PermEscrever)
                {
                    DestinoFinalAgregadoTmp = -2;
                    PublicacaoAgregadaTmp = -2;
                    AutoEliminacaoAgregadoTmp = (long)AgregacaoAEResult.SemSeleccao;
                    break;
                }

                frdAvaliacaoRow = AvaliacaoDocs[doc.IDNivel];
                
                DestinoFinalAgregadoTmp = sumDestinoFinal(DestinoFinalAgregadoTmp, translateDestinoFinal(frdAvaliacaoRow));
                PublicacaoAgregadaTmp = sumPublicacao(PublicacaoAgregadaTmp, Math.Abs(System.Convert.ToInt32(frdAvaliacaoRow.Publicar)));
                AutoEliminacaoAgregadoTmp = sumAutoEliminacao(AutoEliminacaoAgregadoTmp, translateAutoEliminacao(frdAvaliacaoRow));
            }

            

            // O valor -2 é usado como estado inicial e é usado como elemento 
            // absorvente no processo de agregação dos valore selecionados. 
            // Assim, o único caso em que poderá chegar -2 a uma das seguintes 
            // properties é se não existirem quaisquer documentos/ufs selecionados.
            PublicacaoAgregada = PublicacaoAgregadaTmp;
            AutoEliminacaoAgregado = AutoEliminacaoAgregadoTmp;

            // se forem detectados destinos finais diferentes consideramos os autos diferentes, para que a GUI não permita a atribuição de autos de eliminação a documentos que não tenham sequer sido ainda avaliados
            if (DestinoFinalAgregadoTmp == -3)
                AutoEliminacaoAgregado = Convert.ToInt64(AgregacaoAEResult.selecionadosDiferentesAutosEliminacao);

            DestinoFinalAgregado = DestinoFinalAgregadoTmp;

            cbDestinoFinal.SelectedIndexChanged += cbDestinoFinal_SelectedIndexChanged;
            chkPublicar.CheckedChanged += chkPublicar_CheckedChanged;
            ControlAutoEliminacao1.cbAutoEliminacao.SelectedIndexChanged += cbAutoEliminacao_SelectedIndexChanged;
		}

		private void clearResults()
		{
			cbDestinoFinal.SelectedIndexChanged -= cbDestinoFinal_SelectedIndexChanged;
			chkPublicar.CheckedChanged -= chkPublicar_CheckedChanged;
			ControlAutoEliminacao1.cbAutoEliminacao.SelectedIndexChanged -= cbAutoEliminacao_SelectedIndexChanged;

			cbDestinoFinal.SelectedIndex = -1;
			cbDestinoFinal.Enabled = false;
			chkPublicar.Checked = false;
			chkPublicar.Enabled = false;
			ControlAutoEliminacao1.cbAutoEliminacao.SelectedIndex = -1;
            ControlAutoEliminacao1.ContentsEnabled = false;
			//ControlAutoEliminacao1.cbAutoEliminacao.Enabled = false;

			cbDestinoFinal.SelectedIndexChanged += cbDestinoFinal_SelectedIndexChanged;
			chkPublicar.CheckedChanged += chkPublicar_CheckedChanged;
			ControlAutoEliminacao1.cbAutoEliminacao.SelectedIndexChanged += cbAutoEliminacao_SelectedIndexChanged;
		}

		private int translateDestinoFinal(GISADataset.SFRDAvaliacaoRow frdAvaliacaoRow)
		{
			if (frdAvaliacaoRow.IsPreservarNull())
				return -1;
			else if (frdAvaliacaoRow.Preservar)
				return 1;
			else
				return 0;
		}

		private long translateAutoEliminacao(GISADataset.SFRDAvaliacaoRow frdAvaliacaoRow)
		{
			if (frdAvaliacaoRow.IsIDAutoEliminacaoNull())
				return (long)AgregacaoAEResult.selecionadoNenhumAutoEliminacao;
			else
				return frdAvaliacaoRow.IDAutoEliminacao;
		}

		private void updateResultsContentsState()
		{
			// actualizar estado dos outros controlos de "detalhes"
			switch (cbDestinoFinal.SelectedIndex)
			{
				case 0: // por avaliar
					ControlAutoEliminacao1.cbAutoEliminacao.SelectedValue = AgregacaoAEResult.selecionadoNenhumAutoEliminacao;
					ControlAutoEliminacao1.ContentsEnabled = false;
					break;
				case 1: // para conservação
					ControlAutoEliminacao1.cbAutoEliminacao.SelectedValue = AgregacaoAEResult.selecionadoNenhumAutoEliminacao;
                    ControlAutoEliminacao1.ContentsEnabled = false;
					break;
				case 2: // para eliminação
					ControlAutoEliminacao1.cbAutoEliminacao.Enabled = AutoEliminacaoAgregado > 0 || AutoEliminacaoAgregado == Convert.ToInt64(AgregacaoAEResult.selecionadoNenhumAutoEliminacao);
                    ControlAutoEliminacao1.ContentsEnabled = true; //AutoEliminacaoAgregado > 0 || AutoEliminacaoAgregado == Convert.ToInt64(AgregacaoAEResult.selecionadoNenhumAutoEliminacao);
					break;
			}
		}
	#endregion

	#region  Cálculo dos resultados agregados do conjunto de documentos/UFs selecionados 
		// -2  sem seleccao (elemento neutro)
		// -3  diferentes (elemento absorvente)
		private int sumDestinoFinal(int df1, int df2)
		{
			if (df1 == -3 || df2 == -3)
				return -3;

			if (df1 == df2)
				return df1;
			else
			{
				if (df1 == -2)
					return df2;
				else if (df2 == -2)
					return df1;
				else
					return -3;
			}
		}

		private int sumPublicacao(int p1, int p2)
		{
			if (p1 == -3 || p2 == -3)
				return -3;

			if (p1 == p2)
				return p1;
			else
			{
				if (p1 == -2)
					return p2;
				else if (p2 == -2)
					return p1;
				else
					return -3;
			}
		}

		private long sumAutoEliminacao(long ae1, long ae2)
		{
			if (ae1 == (long)AgregacaoAEResult.selecionadosDiferentesAutosEliminacao || ae2 == (long)AgregacaoAEResult.selecionadosDiferentesAutosEliminacao)
				return (long)AgregacaoAEResult.selecionadosDiferentesAutosEliminacao;

			if (ae1 == ae2)
				return ae1;
			else
			{
				if (ae1 == (long)AgregacaoAEResult.SemSeleccao)
					return ae2;
				else if (ae2 == (long)AgregacaoAEResult.SemSeleccao)
					return ae1;
				else
					return (long)AgregacaoAEResult.selecionadosDiferentesAutosEliminacao;
			}
		}
	#endregion

	#region  Eventos de alteração dos resultados dos documentos/UFs  
		private void cbDestinoFinal_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            GISADataset.SFRDAvaliacaoRow frdAvaliacaoRow = null;

            // so actualizar os valores se se tratar de um estado válido. estados inválidos 
            // nunca deveria ocorrer uma vez que a GUI impede a sua ocorrencia
            if (cbDestinoFinal.SelectedValue == null || DestinoFinalAgregado < -1)
                return;
            
            ((frmMain)(this.TopLevelControl)).EnterWaitMode();
            try
            {
                int destinoNewValue = 0;
                destinoNewValue = System.Convert.ToInt32(cbDestinoFinal.SelectedValue);

                // actualizar dados alterados na lista de documentos
                foreach (ListViewItem item in lvwDocumentos.SelectedItems)
                {
                    NivelRule.DocumentoAssociado doc = (NivelRule.DocumentoAssociado)DocumentosAssociados[(long)item.Tag];
                    frdAvaliacaoRow = AvaliacaoDocs[doc.IDNivel];

                    if (((int)cbDestinoFinal.SelectedValue) != DestinoFinalAgregado)
                    {
                        if (destinoNewValue == -1)
                        {
                            frdAvaliacaoRow["Preservar"] = DBNull.Value;
                            doc.Preservar = string.Empty;
                        }
                        else
                        {
                            frdAvaliacaoRow.Preservar = System.Convert.ToBoolean(destinoNewValue);
                            doc.Preservar = destinoNewValue.ToString();
                        }

                        frdAvaliacaoRow["IDAutoEliminacao"] = DBNull.Value;
                        doc.IDAutoEliminacao = string.Empty;
                        item.SubItems[chDocAutoEliminacao.Index].Text = string.Empty;
                        DocumentosAssociados[doc.IDNivel] = doc;
                        AvaliacaoDocs[doc.IDNivel] = frdAvaliacaoRow;
                    }
                    item.SubItems[chDocDestino.Index].Text = GUIHelper.GUIHelper.formatDestinoFinal(frdAvaliacaoRow);
                    lvItemsDocumentos[(long)item.Tag] = item;
                }
                mOriginalDestinoFinalAgregado = destinoNewValue;

                updateResultsContentsState();
                PopulateDocList();
                populateResultsContents();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                throw;
            }
            finally
            {
                ((frmMain)(this.TopLevelControl)).LeaveWaitMode();
            }
		}

		private void chkPublicar_CheckedChanged(object sender, System.EventArgs e)
		{
            GISADataset.SFRDAvaliacaoRow frdAvaliacaoRow = null;

            // verificar se existem valores modificados
            if (PublicacaoAgregada >= -1)
            {
                foreach (ListViewItem item in lvwDocumentos.SelectedItems)
                {
                    NivelRule.DocumentoAssociado doc = (NivelRule.DocumentoAssociado)DocumentosAssociados[(long)item.Tag];
                    frdAvaliacaoRow = AvaliacaoDocs[doc.IDNivel];

                    if (chkPublicar.Checked ^ PublicacaoAgregada == 1)
                    {
                        frdAvaliacaoRow.Publicar = chkPublicar.Checked;
                        AvaliacaoDocs[doc.IDNivel] = frdAvaliacaoRow;
                    }

                    item.SubItems[chDocPublicado.Index].Text = TranslationHelper.FormatBoolean(frdAvaliacaoRow.Publicar);
                    lvItemsDocumentos[(long)item.Tag] = item;
                }
                if (chkPublicar.Checked)
                    mOriginalPublicacaoAgregada = 1;
                else
                    mOriginalPublicacaoAgregada = 0;
            }
		}

		private void cbAutoEliminacao_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            GISADataset.SFRDAvaliacaoRow frdAvaliacaoRow = null;

            if (ControlAutoEliminacao1.cbAutoEliminacao.SelectedValue == null)
                return;

            if (AutoEliminacaoAgregado != Convert.ToInt64(AgregacaoAEResult.selecionadosDiferentesAutosEliminacao) && AutoEliminacaoAgregado != Convert.ToInt64(AgregacaoAEResult.SemSeleccao))
            {
                ((frmMain)(this.TopLevelControl)).EnterWaitMode();
                try
                {
                    long autoNewValue = 0;
                    autoNewValue = (long)ControlAutoEliminacao1.cbAutoEliminacao.SelectedValue;

                    foreach (ListViewItem item in lvwDocumentos.SelectedItems)
                    {
                        NivelRule.DocumentoAssociado doc = (NivelRule.DocumentoAssociado)DocumentosAssociados[(long)item.Tag];
                        frdAvaliacaoRow = AvaliacaoDocs[doc.IDNivel];

                        if (((long)ControlAutoEliminacao1.cbAutoEliminacao.SelectedValue) != AutoEliminacaoAgregado)
                        {
                            if (autoNewValue == (long)AgregacaoAEResult.selecionadoNenhumAutoEliminacao)
                            {
                                frdAvaliacaoRow["IDAutoEliminacao"] = DBNull.Value;
                                doc.IDAutoEliminacao = string.Empty;
                            }
                            else
                            {
                                frdAvaliacaoRow.IDAutoEliminacao = autoNewValue;
                                doc.IDAutoEliminacao = autoNewValue.ToString();
                            }
                            DocumentosAssociados[doc.IDNivel] = doc;
                            AvaliacaoDocs[doc.IDNivel] = frdAvaliacaoRow;
                        }

                        if (frdAvaliacaoRow.AutoEliminacaoRow == null)
                            item.SubItems[chDocAutoEliminacao.Index].Text = string.Empty;
                        else
                            item.SubItems[chDocAutoEliminacao.Index].Text = frdAvaliacaoRow.AutoEliminacaoRow.Designacao;

                        lvItemsDocumentos[(long)item.Tag] = item;
                    }
                    mOriginalAutoEliminacaoAgregado = autoNewValue;

                    updateResultsContentsState();
                    PopulateDocList();
                    populateResultsContents();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                    throw;
                }
                finally
                {
                    ((frmMain)(this.TopLevelControl)).LeaveWaitMode();
                }
            }
		}
	#endregion

	#region  Filtro de documentos/UFs 
        private enum EstadosFiltroDestinoFinal
        {
            PorAvaliar = 0,
            Conservacao = 1,
            Eliminacao = 2
        }

		private void chkAnteriormenteAvaliados_CheckedChanged(object sender, System.EventArgs e)
		{
            EstadosFiltroDestinoFinal selectedState = (EstadosFiltroDestinoFinal)cbFltDestinoFinal.SelectedIndex;
            switch (selectedState)
            {
                case EstadosFiltroDestinoFinal.PorAvaliar:
                    cbFltAuto.Enabled = false;
                    cbFltPrazo.Enabled = true;
                    break;
                case EstadosFiltroDestinoFinal.Conservacao:
                    cbFltAuto.Enabled = false;
                    cbFltPrazo.Enabled = false;
                    break;
                case EstadosFiltroDestinoFinal.Eliminacao:
                    cbFltAuto.Enabled = true;
                    cbFltPrazo.Enabled = true;
                    break;
            }

            clearResults();
			PopulateDocList();
            refreshDocumentosSelection();
		}
	#endregion

	#region  Eventos de alteração de dados noutros locais da aplicação
        // o painel só está activo quando:
        //  - está seleccionada uma (sub)série;
        //  - a (sub)série está avaliada.

        public void OrderUpdate()
        {
            if (CurrentSFRDAvaliacao == null || CurrentSFRDAvaliacao.RowState == DataRowState.Detached || CurrentSFRDAvaliacao.RowState == DataRowState.Unchanged)
                return;

            IsLoaded = false;
            IsPopulated = false;

            Deactivate();
        }
	#endregion

		private void btnProximo_Click(object sender, System.EventArgs e)
		{
			grpConteudoPasso1.Visible = false;
			grpConteudoPasso2.Visible = true;
			lblPassoNum.Text = "Passo 2:";
			lblPassoDesc.Text = "Seleção das unidades físicas associadas";
			btnProximo.Visible = false;
			btnAnterior.Visible = true;
			PopulateSeleccaoUnidadesFisicas();
			UpdateSelUFButtonState();
		}

		private void btnAnterior_Click(object sender, System.EventArgs e)
		{
			grpConteudoPasso1.Visible = true;
			grpConteudoPasso2.Visible = false;
			lblPassoNum.Text = "Passo 1:";
			lblPassoDesc.Text = "Avaliação e seleção dos conteúdos da unidade de descrição";
			btnProximo.Visible = true;
			btnAnterior.Visible = false;
		}

	#region  Lógica específica ao passo 2 (selecção de unidades físicas) 

		private void lvwSeleccaoUnidadesFisicas_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UpdateSelUFButtonState();
		}

		private void UpdateSelUFButtonState()
		{
			btnAdd.Enabled = lvwSeleccaoUnidadesFisicas.SelectedItems.Count > 0;
			btnRemove.Enabled = lvwSeleccaoUnidadesFisicas.SelectedItems.Count > 0; //AndAlso areAutosEliminacaoTheSame
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			FormAutoEliminacaoPicker form = new FormAutoEliminacaoPicker();
			form.LoadData();
			if (form.ShowDialog(this) == DialogResult.Cancel)
				return;

			GISADataset.AutoEliminacaoRow[] aeRows = null;
			aeRows = form.SelectedAutosEliminacao;

			GISADataset.FRDBaseRow frdBaseUFRow = null;
			ArrayList frdBaseUFRowsList = new ArrayList();
			GISADataset.NivelRow nivelUFRow = null;
			GISADataset.SFRDUFAutoEliminacaoRow[] sfrdaeRows = null;
			GISADataset.SFRDUFAutoEliminacaoRow sfrdaeRow = null;

			foreach (ListViewItem item in lvwSeleccaoUnidadesFisicas.SelectedItems)
			{
                NivelRule.UnidadeFisicaAssociada uf = (NivelRule.UnidadeFisicaAssociada)UnidadesFisicasAssociadas[(long)item.Tag];

                GISADataset.NivelRow[] nRows = (GISADataset.NivelRow[])GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + uf.IDNivel);
                Debug.Assert(nRows.Length == 1);

                nivelUFRow = nRows[0];
				frdBaseUFRow = getFRDUnidadeFisica(nivelUFRow.GetFRDBaseRows());
				if (frdBaseUFRow == null)
				{
					frdBaseUFRow = GisaDataSetHelper.GetInstance().FRDBase.NewFRDBaseRow();
					frdBaseUFRow.NivelRow = nivelUFRow;
					frdBaseUFRow.TipoFRDBaseRow = (GISADataset.TipoFRDBaseRow)(GisaDataSetHelper.GetInstance().TipoFRDBase.Select(string.Format("ID={0:d}", TipoFRDBase.FRDUnidadeFisica))[0]);
					frdBaseUFRow.NotaDoArquivista = string.Empty;
					GisaDataSetHelper.GetInstance().FRDBase.AddFRDBaseRow(frdBaseUFRow);
				}
				foreach (GISADataset.AutoEliminacaoRow aeRow in aeRows)
				{
					// criar uma nova associação apenas caso ainda não exista
					if (GisaDataSetHelper.GetInstance().SFRDUFAutoEliminacao.Select(string.Format("IDFRDBase={0} AND IDAutoEliminacao={1}", frdBaseUFRow.ID, aeRow.ID)).Length == 0)
					{
						sfrdaeRows = (GISADataset.SFRDUFAutoEliminacaoRow[])(GisaDataSetHelper.GetInstance().SFRDUFAutoEliminacao.Select(string.Format("IDFRDBase={0} AND IDAutoEliminacao={1}", frdBaseUFRow.ID, aeRow.ID), string.Empty, DataViewRowState.Deleted));
						if (sfrdaeRows.Length > 0)
						{
							sfrdaeRow = sfrdaeRows[0];
							sfrdaeRow.RejectChanges();
						}
						else
						{
							sfrdaeRow = GisaDataSetHelper.GetInstance().SFRDUFAutoEliminacao.NewSFRDUFAutoEliminacaoRow();
							sfrdaeRow.FRDBaseRow = frdBaseUFRow;
							sfrdaeRow.AutoEliminacaoRow = aeRow;
							GisaDataSetHelper.GetInstance().SFRDUFAutoEliminacao.AddSFRDUFAutoEliminacaoRow(sfrdaeRow);
						}
					}
				}
                item.SubItems[chSelUFAutosEliminacaoRestantes.Index].Text = CollectTextAutosEliminacaoRestantes(uf);
				refreshSelUFItem(item, chSelUFAutosEliminacaoRestantes.Index);
			}
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			ArrayList sfrdaeOriginalRowsList = new ArrayList();
			Hashtable aeOriginalRowsHT = new Hashtable();
			ArrayList aeOriginalRowsList = new ArrayList();
			GISADataset.AutoEliminacaoRow[] aeOriginalRows = null;
			GISADataset.AutoEliminacaoRow[] aeDeletableRows = null;
			GISADataset.AutoEliminacaoRow aeDeletableRow = null;

			//preencher sfrdaeOriginalRowsList com todas as linhas, de todas as unidades fisicas seleccionadas
			foreach (ListViewItem item in lvwSeleccaoUnidadesFisicas.SelectedItems)
			{
                NivelRule.UnidadeFisicaAssociada uf = (NivelRule.UnidadeFisicaAssociada)UnidadesFisicasAssociadas[(long)item.Tag];

                GISADataset.NivelRow[] nRows = (GISADataset.NivelRow[])GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + uf.IDNivel);
                Debug.Assert(nRows.Length == 1);

                GISADataset.NivelRow nivelUFRow = nRows[0];
                GISADataset.FRDBaseRow frdBaseUFRow = getFRDUnidadeFisica(nivelUFRow.GetFRDBaseRows());

				foreach (GISADataset.SFRDUFAutoEliminacaoRow sfrdaeRow in frdBaseUFRow.GetSFRDUFAutoEliminacaoRows())
				{
					// ignora os autos de eliminação que sejam adicionados em duplicado
					aeOriginalRowsHT[sfrdaeRow.AutoEliminacaoRow] = sfrdaeRow.AutoEliminacaoRow;
					sfrdaeOriginalRowsList.Add(sfrdaeRow);
				}
			}
			aeOriginalRowsList.AddRange(aeOriginalRowsHT.Keys);
			aeOriginalRows = (GISADataset.AutoEliminacaoRow[])(aeOriginalRowsList.ToArray(typeof(GISADataset.AutoEliminacaoRow)));

			FormDeleteSeleccaoAutoEliminacao form = new FormDeleteSeleccaoAutoEliminacao();
			form.LoadData(aeOriginalRows);
			if (form.ShowDialog(this) == DialogResult.OK)
			{
				aeDeletableRows = form.GetChosenAutosEliminacao();
				// para cada uma das associações em análise entre uma unidade física e um auto de eliminação
				foreach (GISADataset.SFRDUFAutoEliminacaoRow sfrdaeOriginalRow in sfrdaeOriginalRowsList)
				{
					// No caso de se tratar de um dos autos escolhidos, elimina-se a associação
					aeDeletableRow = sfrdaeOriginalRow.AutoEliminacaoRow;
					if (Array.IndexOf(aeDeletableRows, aeDeletableRow) >= 0)
					{
						long UFID = sfrdaeOriginalRow.FRDBaseRow.NivelRow.ID;
						sfrdaeOriginalRow.Delete();
						//FIXME: estamos a actualizar multiplas vezes o mesmo item (UF), uma por cada auto removido que lá existisse
                        foreach (ListViewItem item in lvwSeleccaoUnidadesFisicas.SelectedItems)
                        {
                            if ((long) item.Tag == UFID)
                                refreshSelUFItem(item, chSelUFAutosEliminacaoRestantes.Index);
                        }
					}
				}
			}
		}
	#endregion
	}
}