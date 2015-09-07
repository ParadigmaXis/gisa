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
	public class PanelAmbitoConteudo : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelAmbitoConteudo() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            btnRemoveDiploma.Click += btnRemoveDiploma_Click;
            btnRemoveModelo.Click += btnRemoveModelo_Click;
            btnRemoveTipo.Click += btnRemoveTipo_Click;
            btnRemoveSubtipo.Click += btnRemoveSubtipo_Click;
            lstVwTipoInformacional.SelectedIndexChanged += lstVw_SelectedIndexChanged;
            lstVwSubTipoInformacional.SelectedIndexChanged += lstVw_SelectedIndexChanged;
            lstVwDiplomaLegalRegulamentacao.SelectedIndexChanged += lstVw_SelectedIndexChanged;
            lstVwModelo.SelectedIndexChanged += lstVw_SelectedIndexChanged;
            btnAddDiploma.Click += btnAddDiploma_Click;
            btnAddModelo.Click += btnAddModelo_Click;

			AddHandlers();
			GetExtraResources();
			UpdateButtonState();
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
		internal System.Windows.Forms.GroupBox grpDiplomaLegalRegulamentacao;
        internal System.Windows.Forms.GroupBox grpModelo;
		internal System.Windows.Forms.ListView lstVwModelo;
		internal System.Windows.Forms.ListView lstVwDiplomaLegalRegulamentacao;
		internal System.Windows.Forms.ListView lstVwSubTipoInformacional;
		internal System.Windows.Forms.GroupBox grpTipoInformacional;
		internal System.Windows.Forms.ListView lstVwTipoInformacional;
		internal System.Windows.Forms.ColumnHeader ColumnHeader1;
		internal System.Windows.Forms.ColumnHeader ColumnHeader2;
		internal System.Windows.Forms.ColumnHeader ColumnHeader3;
		internal System.Windows.Forms.ColumnHeader ColumnHeader4;
		internal System.Windows.Forms.ColumnHeader ColumnHeader5;
		internal System.Windows.Forms.ColumnHeader ColumnHeader6;
		internal System.Windows.Forms.ColumnHeader ColumnHeader7;
		internal System.Windows.Forms.ColumnHeader ColumnHeader10;
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.Button btnRemoveDiploma;
		internal System.Windows.Forms.Button btnAddDiploma;
		internal System.Windows.Forms.Button btnRemoveModelo;
		internal System.Windows.Forms.Button btnAddModelo;
        internal System.Windows.Forms.Button btnRemoveTipo;
        private TableLayoutPanel tableLayoutPanel1;
		internal System.Windows.Forms.Button btnRemoveSubtipo;



		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.grpDiplomaLegalRegulamentacao = new System.Windows.Forms.GroupBox();
            this.btnRemoveDiploma = new System.Windows.Forms.Button();
            this.btnAddDiploma = new System.Windows.Forms.Button();
            this.lstVwDiplomaLegalRegulamentacao = new System.Windows.Forms.ListView();
            this.ColumnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpModelo = new System.Windows.Forms.GroupBox();
            this.btnRemoveModelo = new System.Windows.Forms.Button();
            this.btnAddModelo = new System.Windows.Forms.Button();
            this.lstVwModelo = new System.Windows.Forms.ListView();
            this.ColumnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpTipoInformacional = new System.Windows.Forms.GroupBox();
            this.btnRemoveSubtipo = new System.Windows.Forms.Button();
            this.btnRemoveTipo = new System.Windows.Forms.Button();
            this.lstVwTipoInformacional = new System.Windows.Forms.ListView();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstVwSubTipoInformacional = new System.Windows.Forms.ListView();
            this.ColumnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grpDiplomaLegalRegulamentacao.SuspendLayout();
            this.grpModelo.SuspendLayout();
            this.grpTipoInformacional.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDiplomaLegalRegulamentacao
            // 
            this.grpDiplomaLegalRegulamentacao.Controls.Add(this.btnRemoveDiploma);
            this.grpDiplomaLegalRegulamentacao.Controls.Add(this.btnAddDiploma);
            this.grpDiplomaLegalRegulamentacao.Controls.Add(this.lstVwDiplomaLegalRegulamentacao);
            this.grpDiplomaLegalRegulamentacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDiplomaLegalRegulamentacao.Location = new System.Drawing.Point(3, 3);
            this.grpDiplomaLegalRegulamentacao.Name = "grpDiplomaLegalRegulamentacao";
            this.grpDiplomaLegalRegulamentacao.Size = new System.Drawing.Size(383, 268);
            this.grpDiplomaLegalRegulamentacao.TabIndex = 2;
            this.grpDiplomaLegalRegulamentacao.TabStop = false;
            this.grpDiplomaLegalRegulamentacao.Text = "Diploma legal / regulamentação";
            // 
            // btnRemoveDiploma
            // 
            this.btnRemoveDiploma.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveDiploma.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemoveDiploma.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemoveDiploma.Location = new System.Drawing.Point(351, 66);
            this.btnRemoveDiploma.Name = "btnRemoveDiploma";
            this.btnRemoveDiploma.Size = new System.Drawing.Size(24, 24);
            this.btnRemoveDiploma.TabIndex = 3;
            // 
            // btnAddDiploma
            // 
            this.btnAddDiploma.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddDiploma.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddDiploma.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddDiploma.Location = new System.Drawing.Point(351, 34);
            this.btnAddDiploma.Name = "btnAddDiploma";
            this.btnAddDiploma.Size = new System.Drawing.Size(24, 24);
            this.btnAddDiploma.TabIndex = 2;
            // 
            // lstVwDiplomaLegalRegulamentacao
            // 
            this.lstVwDiplomaLegalRegulamentacao.AllowDrop = true;
            this.lstVwDiplomaLegalRegulamentacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwDiplomaLegalRegulamentacao.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader10});
            this.lstVwDiplomaLegalRegulamentacao.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstVwDiplomaLegalRegulamentacao.HideSelection = false;
            this.lstVwDiplomaLegalRegulamentacao.Location = new System.Drawing.Point(8, 16);
            this.lstVwDiplomaLegalRegulamentacao.Name = "lstVwDiplomaLegalRegulamentacao";
            this.lstVwDiplomaLegalRegulamentacao.Size = new System.Drawing.Size(337, 244);
            this.lstVwDiplomaLegalRegulamentacao.TabIndex = 1;
            this.lstVwDiplomaLegalRegulamentacao.UseCompatibleStateImageBehavior = false;
            this.lstVwDiplomaLegalRegulamentacao.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader10
            // 
            this.ColumnHeader10.Text = "Designação";
            this.ColumnHeader10.Width = 315;
            // 
            // grpModelo
            // 
            this.grpModelo.Controls.Add(this.btnRemoveModelo);
            this.grpModelo.Controls.Add(this.btnAddModelo);
            this.grpModelo.Controls.Add(this.lstVwModelo);
            this.grpModelo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpModelo.Location = new System.Drawing.Point(392, 3);
            this.grpModelo.Name = "grpModelo";
            this.grpModelo.Size = new System.Drawing.Size(383, 268);
            this.grpModelo.TabIndex = 3;
            this.grpModelo.TabStop = false;
            this.grpModelo.Text = "Modelo";
            // 
            // btnRemoveModelo
            // 
            this.btnRemoveModelo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveModelo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemoveModelo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemoveModelo.Location = new System.Drawing.Point(351, 66);
            this.btnRemoveModelo.Name = "btnRemoveModelo";
            this.btnRemoveModelo.Size = new System.Drawing.Size(24, 24);
            this.btnRemoveModelo.TabIndex = 3;
            // 
            // btnAddModelo
            // 
            this.btnAddModelo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddModelo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddModelo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddModelo.Location = new System.Drawing.Point(351, 34);
            this.btnAddModelo.Name = "btnAddModelo";
            this.btnAddModelo.Size = new System.Drawing.Size(24, 24);
            this.btnAddModelo.TabIndex = 2;
            // 
            // lstVwModelo
            // 
            this.lstVwModelo.AllowDrop = true;
            this.lstVwModelo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwModelo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader7});
            this.lstVwModelo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstVwModelo.HideSelection = false;
            this.lstVwModelo.Location = new System.Drawing.Point(8, 16);
            this.lstVwModelo.Name = "lstVwModelo";
            this.lstVwModelo.Size = new System.Drawing.Size(337, 244);
            this.lstVwModelo.TabIndex = 1;
            this.lstVwModelo.UseCompatibleStateImageBehavior = false;
            this.lstVwModelo.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader7
            // 
            this.ColumnHeader7.Text = "Designação";
            this.ColumnHeader7.Width = 318;
            // 
            // grpTipoInformacional
            // 
            this.grpTipoInformacional.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTipoInformacional.Controls.Add(this.btnRemoveSubtipo);
            this.grpTipoInformacional.Controls.Add(this.btnRemoveTipo);
            this.grpTipoInformacional.Controls.Add(this.lstVwTipoInformacional);
            this.grpTipoInformacional.Controls.Add(this.lstVwSubTipoInformacional);
            this.grpTipoInformacional.Location = new System.Drawing.Point(8, 16);
            this.grpTipoInformacional.Name = "grpTipoInformacional";
            this.grpTipoInformacional.Size = new System.Drawing.Size(778, 292);
            this.grpTipoInformacional.TabIndex = 1;
            this.grpTipoInformacional.TabStop = false;
            this.grpTipoInformacional.Text = "Tipo informacional";
            // 
            // btnRemoveSubtipo
            // 
            this.btnRemoveSubtipo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveSubtipo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemoveSubtipo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemoveSubtipo.Location = new System.Drawing.Point(749, 119);
            this.btnRemoveSubtipo.Name = "btnRemoveSubtipo";
            this.btnRemoveSubtipo.Size = new System.Drawing.Size(24, 24);
            this.btnRemoveSubtipo.TabIndex = 4;
            // 
            // btnRemoveTipo
            // 
            this.btnRemoveTipo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveTipo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemoveTipo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemoveTipo.Location = new System.Drawing.Point(749, 32);
            this.btnRemoveTipo.Name = "btnRemoveTipo";
            this.btnRemoveTipo.Size = new System.Drawing.Size(24, 24);
            this.btnRemoveTipo.TabIndex = 2;
            // 
            // lstVwTipoInformacional
            // 
            this.lstVwTipoInformacional.AllowDrop = true;
            this.lstVwTipoInformacional.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwTipoInformacional.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader2,
            this.ColumnHeader3});
            this.lstVwTipoInformacional.FullRowSelect = true;
            this.lstVwTipoInformacional.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstVwTipoInformacional.HideSelection = false;
            this.lstVwTipoInformacional.Location = new System.Drawing.Point(8, 16);
            this.lstVwTipoInformacional.Name = "lstVwTipoInformacional";
            this.lstVwTipoInformacional.Size = new System.Drawing.Size(738, 82);
            this.lstVwTipoInformacional.TabIndex = 1;
            this.lstVwTipoInformacional.UseCompatibleStateImageBehavior = false;
            this.lstVwTipoInformacional.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Designação";
            this.ColumnHeader1.Width = 516;
            // 
            // ColumnHeader2
            // 
            this.ColumnHeader2.Text = "Validado";
            this.ColumnHeader2.Width = 82;
            // 
            // ColumnHeader3
            // 
            this.ColumnHeader3.Text = "Completo";
            // 
            // lstVwSubTipoInformacional
            // 
            this.lstVwSubTipoInformacional.AllowDrop = true;
            this.lstVwSubTipoInformacional.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwSubTipoInformacional.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader4,
            this.ColumnHeader5,
            this.ColumnHeader6});
            this.lstVwSubTipoInformacional.FullRowSelect = true;
            this.lstVwSubTipoInformacional.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstVwSubTipoInformacional.HideSelection = false;
            this.lstVwSubTipoInformacional.Location = new System.Drawing.Point(8, 104);
            this.lstVwSubTipoInformacional.Name = "lstVwSubTipoInformacional";
            this.lstVwSubTipoInformacional.Size = new System.Drawing.Size(738, 182);
            this.lstVwSubTipoInformacional.TabIndex = 3;
            this.lstVwSubTipoInformacional.UseCompatibleStateImageBehavior = false;
            this.lstVwSubTipoInformacional.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader4
            // 
            this.ColumnHeader4.Text = "Subtipos";
            this.ColumnHeader4.Width = 517;
            // 
            // ColumnHeader5
            // 
            this.ColumnHeader5.Text = "Validado";
            this.ColumnHeader5.Width = 80;
            // 
            // ColumnHeader6
            // 
            this.ColumnHeader6.Text = "Completo";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.Controls.Add(this.tableLayoutPanel1);
            this.GroupBox1.Controls.Add(this.grpTipoInformacional);
            this.GroupBox1.Location = new System.Drawing.Point(3, 3);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(794, 594);
            this.GroupBox1.TabIndex = 5;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "3.1. Âmbito e conteúdo";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.grpModelo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpDiplomaLegalRegulamentacao, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 314);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(778, 274);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // PanelAmbitoConteudo
            // 
            this.Controls.Add(this.GroupBox1);
            this.MinSize = new System.Drawing.Size(745, 714);
            this.Name = "PanelAmbitoConteudo";
            this.grpDiplomaLegalRegulamentacao.ResumeLayout(false);
            this.grpModelo.ResumeLayout(false);
            this.grpTipoInformacional.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

		private void GetExtraResources()
		{
			btnAddDiploma.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
			btnAddModelo.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
			btnRemoveDiploma.Image = SharedResourcesOld.CurrentSharedResources.Apagar;
			btnRemoveModelo.Image = SharedResourcesOld.CurrentSharedResources.Apagar;
			btnRemoveTipo.Image = SharedResourcesOld.CurrentSharedResources.Apagar;
			btnRemoveSubtipo.Image = SharedResourcesOld.CurrentSharedResources.Apagar;

			if (! DesignMode)
				base.ParentChanged += PanelAmbitoConteudo_ParentChanged;
		}

		// runs only once. sets tooltips as soon as it's parent appears
		private void PanelAmbitoConteudo_ParentChanged(object sender, System.EventArgs e)
		{
			MultiPanel.CurrentToolTip.SetToolTip(btnAddDiploma, SharedResourcesOld.CurrentSharedResources.AdicionarString);
			MultiPanel.CurrentToolTip.SetToolTip(btnAddModelo, SharedResourcesOld.CurrentSharedResources.AdicionarString);
			MultiPanel.CurrentToolTip.SetToolTip(btnRemoveDiploma, SharedResourcesOld.CurrentSharedResources.ApagarString);
			MultiPanel.CurrentToolTip.SetToolTip(btnRemoveModelo, SharedResourcesOld.CurrentSharedResources.ApagarString);
			base.ParentChanged -= PanelAmbitoConteudo_ParentChanged;
		}

		private void AddHandlers()
		{
			lstVwTipoInformacional.KeyUp += lstVw_KeyUp;
			lstVwSubTipoInformacional.KeyUp += lstVw_KeyUp;
			lstVwDiplomaLegalRegulamentacao.KeyUp += lstVw_KeyUp;
			lstVwModelo.KeyUp += lstVw_KeyUp;
		}

		private GISADataset.FRDBaseRow CurrentFRDBase;
		private ControloAutDragDrop DragDropHandlerTipoInformacional;
		private ControloAutDragDrop DragDropHandlerSubTipoInformacional;
		private ControloAutDragDrop DragDropHandlerDiploma;
		private ControloAutDragDrop DragDropHandlerModelo;

		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			CurrentFRDBase = (GISADataset.FRDBaseRow)CurrentDataRow;

			if (DragDropHandlerTipoInformacional == null)
			{
				DragDropHandlerTipoInformacional = new ControloAutDragDrop(lstVwTipoInformacional, new TipoNoticiaAut[] {TipoNoticiaAut.TipologiaInformacional}, CurrentFRDBase);
				DragDropHandlerSubTipoInformacional = new ControloAutDragDrop(lstVwSubTipoInformacional, new TipoNoticiaAut[] { TipoNoticiaAut.TipologiaInformacional}, CurrentFRDBase);
				DragDropHandlerDiploma = new ControloAutDragDrop(lstVwDiplomaLegalRegulamentacao, new TipoNoticiaAut[] {TipoNoticiaAut.Diploma}, CurrentFRDBase);
				DragDropHandlerModelo = new ControloAutDragDrop(lstVwModelo, new TipoNoticiaAut[] {TipoNoticiaAut.Modelo}, CurrentFRDBase);

                DragDropHandlerTipoInformacional.AddControloAut += AddControloAut;
                DragDropHandlerSubTipoInformacional.AddControloAut += AddControloAut;
                DragDropHandlerDiploma.AddControloAut += AddControloAut;
                DragDropHandlerModelo.AddControloAut += AddControloAut;
			}
			else
			{
				DragDropHandlerTipoInformacional.FRDBase = CurrentFRDBase;
				DragDropHandlerSubTipoInformacional.FRDBase = CurrentFRDBase;
			}

			FRDRule.Current.LoadConteudoEEstrutura(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, conn);

            OnShowPanel();
			IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = false;
			string QueryFilter = "IDFRDBase=" + CurrentFRDBase.ID.ToString();
			GISADataset tempWith1 = GisaDataSetHelper.GetInstance();
			
            PopulateTipologias();
			PopulateSubTipologias();
			PopulateDiplomas();
			PopulateModelos();            

			UpdateButtonState();
			IsPopulated = true;
		}

		private void PopulateTipologias()
		{
			FilterTipoNoticiaAut = (int)TipoNoticiaAut.TipologiaInformacional;
			FilterIndexFRDCASelector = -1;
			FilterListView = lstVwTipoInformacional;
			lstVwTipoInformacional.Items.Clear();
			GisaDataSetHelper.VisitIndexFRDCA(CurrentFRDBase, FilterControloAut);
		}

		private void PopulateSubTipologias()
		{
			FilterTipoNoticiaAut = (int)TipoNoticiaAut.TipologiaInformacional;
			FilterIndexFRDCASelector = 1;
			FilterListView = lstVwSubTipoInformacional;
			lstVwSubTipoInformacional.Items.Clear();
			GisaDataSetHelper.VisitIndexFRDCA(CurrentFRDBase, FilterControloAut);
		}

		private void PopulateDiplomas()
		{
			FilterTipoNoticiaAut = (int)TipoNoticiaAut.Diploma;
			FilterIndexFRDCASelector = -1;
			FilterListView = lstVwDiplomaLegalRegulamentacao;
			lstVwDiplomaLegalRegulamentacao.Items.Clear();
			GisaDataSetHelper.VisitIndexFRDCA(CurrentFRDBase, FilterControloAut);
		}

		private void PopulateModelos()
		{
			FilterTipoNoticiaAut = (int)TipoNoticiaAut.Modelo;
			FilterIndexFRDCASelector = -1;
			FilterListView = lstVwModelo;
			lstVwModelo.Items.Clear();
			GisaDataSetHelper.VisitIndexFRDCA(CurrentFRDBase, FilterControloAut);
		}

		public override void ViewToModel()
		{
            
		}

		public override void Deactivate()
		{
            GUIHelper.GUIHelper.clearField(lstVwTipoInformacional);
            GUIHelper.GUIHelper.clearField(lstVwSubTipoInformacional);
            GUIHelper.GUIHelper.clearField(lstVwDiplomaLegalRegulamentacao);
            GUIHelper.GUIHelper.clearField(lstVwModelo);

            OnHidePanel();
		}
		
		private GISADataset.IndexFRDCARow TmpIndexFRDCA;
		private int FilterTipoNoticiaAut;
		private int FilterIndexFRDCASelector;
		private ListView FilterListView;
		private void FilterControloAut(GISADataset.IndexFRDCARow IndexFRDCA)
		{
			if (IndexFRDCA.ControloAutRow.IDTipoNoticiaAut == FilterTipoNoticiaAut)
			{
				//if ((FilterIndexFRDCASelector == -1 && IndexFRDCA.IsNull("Selector")) || (! (FilterIndexFRDCASelector == -1) && ! (IndexFRDCA.IsNull("Selector")) && IndexFRDCA.Selector == FilterIndexFRDCASelector))
                if ((FilterIndexFRDCASelector == -1 && IndexFRDCA.IsNull("Selector")) || ( !(IndexFRDCA.IsNull("Selector")) && IndexFRDCA.Selector == FilterIndexFRDCASelector))
				{
					TmpIndexFRDCA = IndexFRDCA;
					GisaDataSetHelper.VisitControloAutDicionario(IndexFRDCA.ControloAutRow, DisplayControloAut);
				}
			}
		}

		private void DisplayControloAut(GISADataset.ControloAutDicionarioRow ControloAutDicionario)
		{
			if (ControloAutDicionario.IDTipoControloAutForma == Convert.ToInt64(TipoControloAutForma.FormaAutorizada))
			{
				ListViewItem tempWith1 = FilterListView.Items.Add(ControloAutDicionario.DicionarioRow.Termo);

				tempWith1.Tag = TmpIndexFRDCA;
				tempWith1.SubItems.Add(TranslationHelper.FormatBoolean(ControloAutDicionario.ControloAutRow.Autorizado));
				tempWith1.SubItems.Add(TranslationHelper.FormatBoolean(ControloAutDicionario.ControloAutRow.Completo));
			}
		}

		public void btnRemoveDiploma_Click(object sender, EventArgs e)
		{
            GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwDiplomaLegalRegulamentacao);
		}

		public void btnRemoveModelo_Click(object sender, EventArgs e)
		{
            GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwModelo);
		}

		public void btnRemoveTipo_Click(object sender, EventArgs e)
		{
            GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwTipoInformacional);

            // Mandar actualizar o PanelConteudoInformacional
            this.TheGenericDelegate.Invoke();
		}

		public void btnRemoveSubtipo_Click(object sender, EventArgs e)
		{
            GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwSubTipoInformacional);
		}

		private void lstVw_KeyUp(object sender, KeyEventArgs e)
		{
            if (e.KeyValue == Convert.ToInt32(Keys.Delete)) {
                GUIHelper.GUIHelper.deleteSelectedLstVwItems((ListView)sender);

                if (this.lstVwTipoInformacional.Equals((ListView)sender))
                    this.TheGenericDelegate.Invoke();
            }
		}

		private void lstVw_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UpdateButtonState();
		}

		private void UpdateButtonState()
		{
            btnRemoveDiploma.Enabled = lstVwDiplomaLegalRegulamentacao.SelectedItems.Count > 0;
            btnRemoveModelo.Enabled = lstVwModelo.SelectedItems.Count > 0;
			btnRemoveTipo.Enabled = lstVwTipoInformacional.SelectedItems.Count > 0;
            btnRemoveSubtipo.Enabled = lstVwSubTipoInformacional.SelectedItems.Count > 0;
		}

		private void AddControloAut(object Sender, ListViewItem ListViewItem)
		{
			GISADataset.IndexFRDCARow IndexFRDCARow = null;
			IndexFRDCARow = (GISADataset.IndexFRDCARow)ListViewItem.Tag;
			if (ListViewItem.ListView == lstVwSubTipoInformacional)
				IndexFRDCARow.Selector = 1;
            else if (ListViewItem.ListView == lstVwTipoInformacional)
                IndexFRDCARow.Selector = -1;

			ListViewItem.SubItems.Add(TranslationHelper.FormatBoolean(IndexFRDCARow.ControloAutRow.Autorizado));
			ListViewItem.SubItems.Add(TranslationHelper.FormatBoolean(IndexFRDCARow.ControloAutRow.Completo));

			// se se tratar de uma tipologia informacional garantir que existe apenas um item
            if (ListViewItem.ListView == lstVwTipoInformacional) {

                if (lstVwTipoInformacional.Items.Count > 1) {
                    GISADataset.IndexFRDCARow idxCARow = null;
                    switch (MessageBox.Show("Não pode existir mais que uma tipologia informacional. Deseja substituir a existente?", "Tipologia informacional", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)) {
                        case DialogResult.OK:
                            // eliminar todos as tipologias informacionais excepto a que acaba de ser adicionada
                            foreach (ListViewItem item in lstVwTipoInformacional.Items) {
                                if (!(item == ListViewItem)) {
                                    idxCARow = (GISADataset.IndexFRDCARow)item.Tag;
                                    item.Remove();
                                    idxCARow.Delete();
                                }
                            }
                            break;
                        case DialogResult.Cancel:
                            // eliminar a tipologia informacional que acaba de ser adicionada
                            idxCARow = (GISADataset.IndexFRDCARow)ListViewItem.Tag;
                            ListViewItem.Remove();
                            idxCARow.Delete();
                            break;
                    }
                }

                // Mandar actualizar o PanelConteudoInformacional
                this.TheGenericDelegate.Invoke();
            }
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

					// Show the panel with all notícias autoridade
					((frmMain)this.TopLevelControl).PushMasterPanel(typeof(MasterPanelControloAut));

                    MasterPanelControloAut master = (MasterPanelControloAut)(((frmMain)this.TopLevelControl).MasterPanel);

                    master.caList.AllowedNoticiaAut(TipoNoticiaAut.TipologiaInformacional);
                    master.caList.AllowedNoticiaAutLocked = true;
                    master.caList.ReloadList();

                    master.UpdateSupoortPanelPermissions("GISA.FRDCAConteudo");
                    master.UpdateToolBarButtons();
				}
			}
			else
			{
				// Make sure the button is not pushed            
				MultiPanel.ToolBarButtonAuxList.Pushed = false;

				// Remove the panel with all notícias autoridade
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
			// Show the button that brings up the support panel and select
			// it by default.
			if (! (((frmMain)this.TopLevelControl).isSuportPanel) && !TbBAuxListEventAssigned)
			{
				MultiPanel.ToolBar.ButtonClick += ToolBar_ButtonClick;
				MultiPanel.ToolBarButtonAuxList.Visible = true;

                TbBAuxListEventAssigned = true;
			}
		}

		public override void OnHidePanel()
		{
			//Deactivate Toolbar Buttons
            if (TbBAuxListEventAssigned)
            {
                MultiPanel.ToolBar.ButtonClick -= ToolBar_ButtonClick;
                MultiPanel.ToolBarButtonAuxList.Visible = false;

                ToggleControloAutoridade(false);

                TbBAuxListEventAssigned = false;
            }
		}

		private void btnAddDiploma_Click(object sender, System.EventArgs e)
		{
			FormPickDiplomaModelo f = new FormPickDiplomaModelo();
			f.caList.AllowedNoticiaAut(TipoNoticiaAut.Diploma); //, TipoNoticiaAut.Modelo
			f.Text = "Diplomas";
			f.LoadData();

			if (f.caList.Items.Count > 0)
				f.caList.SelectItem(f.caList.Items[0]);

			if (f.ShowDialog(this) == DialogResult.OK)
				CreateNewRelation(((GISADataset.ControloAutDicionarioRow)(f.caList.SelectedItems[0].Tag)).ControloAutRow);

			PopulateDiplomas();
		}

		private void btnAddModelo_Click(object sender, System.EventArgs e)
		{
			FormPickDiplomaModelo f = new FormPickDiplomaModelo();
			f.caList.AllowedNoticiaAut(TipoNoticiaAut.Modelo);
			f.Text = "Modelos";
			f.LoadData();

			if (f.caList.Items.Count > 0)
				f.caList.SelectItem(f.caList.Items[0]);

			if (f.ShowDialog(this) == DialogResult.OK)
				CreateNewRelation(((GISADataset.ControloAutDicionarioRow)(f.caList.SelectedItems[0].Tag)).ControloAutRow);

			PopulateModelos();
		}

		private void CreateNewRelation(GISADataset.ControloAutRow row)
		{
			if (row == null || PresentInIndex(row))
				return;

			GISADataset.IndexFRDCARow IndexFRDCARow = null;
			GISADataset.IndexFRDCARow[] IndexFRDCADeletedRows = null;

			// se a row de IndexFRDCA já existir em memória como Deleted resuscitamo-la
			IndexFRDCADeletedRows = (GISADataset.IndexFRDCARow[])(GisaDataSetHelper.GetInstance().IndexFRDCA.Select(string.Format("IDFRDBase = {0} AND IDControloAut = {1}", CurrentFRDBase.ID, row.ID), string.Empty, DataViewRowState.Deleted));
			if (IndexFRDCADeletedRows.Length > 0)
			{
				IndexFRDCARow = IndexFRDCADeletedRows[0];
				IndexFRDCARow.RejectChanges();
			}
			else
			{
				IndexFRDCARow = GisaDataSetHelper.GetInstance().IndexFRDCA.NewIndexFRDCARow();
				IndexFRDCARow.FRDBaseRow = CurrentFRDBase;
				IndexFRDCARow.ControloAutRow = row;
				IndexFRDCARow["Selector"] = DBNull.Value;
				GisaDataSetHelper.GetInstance().IndexFRDCA.AddIndexFRDCARow(IndexFRDCARow);
			}
		}

		private bool PresentInIndex(GISADataset.ControloAutRow ControloAutRow)
		{
			return GisaDataSetHelper.GetInstance().IndexFRDCA.Select("IDFRDBase=" + CurrentFRDBase.ID.ToString() + " AND IDControloAut=" + ControloAutRow.ID.ToString()).Length > 0;
		}
	}
}