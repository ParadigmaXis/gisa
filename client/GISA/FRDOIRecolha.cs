using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Fedora.FedoraHandler;
using GISA.Model;
using GISA.SharedResources;

namespace GISA
{
	public class FRDOIRecolha : FRDOrganizacaoInformacao
	{

		public static new Bitmap FunctionImage
		{
			get {return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "Documento_enabled_32x32.png");}
		}

	#region  Windows Form Designer generated code 

		public FRDOIRecolha() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call

			TreeNode tempWith1 = DropDownTreeView1.Nodes.Add("1. Identificação");
            TreeNode tempWith2 = tempWith1.Nodes.Add("Referência e datas de produção");
			tempWith2.Tag = PanelIdentificacao1;
            TreeNode tempWith3 = tempWith1.Nodes.Add("Dimensões e suporte");
			tempWith3.Tag = PanelOIDimensoesSuporte1;
            TreeNode tempWith4 = DropDownTreeView1.Nodes.Add("2. Contexto");
			tempWith4.Tag = PanelContexto1;
            TreeNode tempWith5 = DropDownTreeView1.Nodes.Add("3. Conteúdo e estrutura");
            TreeNode tempWith6 = tempWith5.Nodes.Add("3.1. Âmbito e conteúdo");
            TreeNode tempWith7 = tempWith6.Nodes.Add("Âmbito");
			tempWith7.Tag = PanelAmbitoConteudo1;
            TreeNode tempWith8 = tempWith6.Nodes.Add("Conteúdo informacional");
            tempWith8.Tag = PanelConteudoInformacional1;
            TreeNode tempWith9 = tempWith5.Nodes.Add("3.2. Avaliação, seleção e eliminação");
            TreeNode tempWith10 = tempWith9.Nodes.Add("Avaliação da unidade de descrição");
			tempWith10.Tag = PanelAvaliacaoSeleccaoEliminacao1;
            TreeNode tempWith11 = tempWith9.Nodes.Add("Avaliação do conteúdo da unidade de descrição");
			tempWith11.Tag = PanelAvaliacaoDocumentosUnidadesFisicas1;
            TreeNode tempWith12 = tempWith5.Nodes.Add("3.3. Incorporações");
			tempWith12.Tag = PanelIncorporacoes1;
            TreeNode tempWith13 = tempWith5.Nodes.Add("3.4. Organização e ordenação");
			tempWith13.Tag = PanelOrganizacaoOrdenacao1;
            TreeNode tempWith14 = tempWith5.Nodes.Add("3.*. Objetos digitais");
			tempWith14.Tag = PanelIndiceDocumento1;
            if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsFedoraEnable())
            {
                TreeNode tempWith22 = tempWith5.Nodes.Add("3.*. Objetos digitais fedora");
                tempWith22.Tag = PanelObjetoDigitalFedora1;
            }
            TreeNode tempWith15 = DropDownTreeView1.Nodes.Add("4. Condições de acesso e de utilização");
            TreeNode tempWith16 = tempWith15.Nodes.Add("Caracterização do acesso");
			tempWith16.Tag = PanelAVCondicoesAcesso1;
            TreeNode tempWith17 = tempWith15.Nodes.Add("Descrição do material");
			tempWith17.Tag = PanelCondicoesAcesso1;
            TreeNode tempWith18 = DropDownTreeView1.Nodes.Add("5. Documentação associada");
			tempWith18.Tag = PanelDocumentacaoAssociada1;
            TreeNode tempWith19 = DropDownTreeView1.Nodes.Add("6. Notas");
			tempWith19.Tag = PanelNotas1;
            TreeNode tempWith20 = DropDownTreeView1.Nodes.Add("7. Controlo de descrição");
			tempWith20.Tag = PanelOIControloDescricao1;
            TreeNode tempWith21 = DropDownTreeView1.Nodes.Add("*. Indexação");
			tempWith21.Tag = PanelIndexacao1;
			DropDownTreeView1.SelectedNode = DropDownTreeView1.Nodes[0].Nodes[0];
			foreach (TreeNode n in DropDownTreeView1.Nodes)
				n.ExpandAll();

            this.PanelAvaliacaoSeleccaoEliminacao1.TheGenericDelegate = this.PanelAvaliacaoDocumentosUnidadesFisicas1.OrderUpdate;

            this.PanelAmbitoConteudo1.TheGenericDelegate = this.PanelConteudoInformacional1.OrderUpdate;
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
		//    Friend WithEvents ToolBarButtonPrefix As System.Windows.Forms.ToolBarButton
		//    Friend WithEvents ToolBarButtonSave As System.Windows.Forms.ToolBarButton
		internal GISA.PanelIdentificacao PanelIdentificacao1;
		internal GISA.PanelOIDimensoesSuporte PanelOIDimensoesSuporte1;
		internal GISA.PanelContexto PanelContexto1;
		internal GISA.PanelAmbitoConteudo PanelAmbitoConteudo1;
        internal GISA.PanelConteudoInformacional PanelConteudoInformacional1;
		internal GISA.PanelAvaliacaoSeleccaoEliminacao PanelAvaliacaoSeleccaoEliminacao1;
		internal PanelAvaliacaoDocumentosUnidadesFisicas PanelAvaliacaoDocumentosUnidadesFisicas1;
		internal GISA.PanelIncorporacoes PanelIncorporacoes1;
		internal GISA.PanelOrganizacaoOrdenacao PanelOrganizacaoOrdenacao1;
        internal GISA.PanelObjetoDigitalFedora PanelObjetoDigitalFedora1;
        internal GISA.PanelIndiceDocumento PanelIndiceDocumento1;
		internal GISA.PanelCondicoesAcesso PanelCondicoesAcesso1;
		internal GISA.PanelAVCondicoesAcesso PanelAVCondicoesAcesso1;
		internal GISA.PanelDocumentacaoAssociada PanelDocumentacaoAssociada1;
		internal GISA.PanelNotas PanelNotas1;
		internal GISA.PanelOIControloDescricao PanelOIControloDescricao1;
		internal GISA.PanelIndexacao PanelIndexacao1;
		internal GISA.PanelMensagem PanelMensagem1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.PanelIdentificacao1 = new GISA.PanelIdentificacao();
			this.PanelOIDimensoesSuporte1 = new GISA.PanelOIDimensoesSuporte();
			this.PanelContexto1 = new GISA.PanelContexto();
			this.PanelAmbitoConteudo1 = new GISA.PanelAmbitoConteudo();
            this.PanelConteudoInformacional1 = new GISA.PanelConteudoInformacional();
			this.PanelAvaliacaoSeleccaoEliminacao1 = new GISA.PanelAvaliacaoSeleccaoEliminacao();
			this.PanelAvaliacaoDocumentosUnidadesFisicas1 = new GISA.PanelAvaliacaoDocumentosUnidadesFisicas();
			this.PanelIncorporacoes1 = new GISA.PanelIncorporacoes();
			this.PanelOrganizacaoOrdenacao1 = new GISA.PanelOrganizacaoOrdenacao();
			this.PanelIndiceDocumento1 = new GISA.PanelIndiceDocumento();
            this.PanelObjetoDigitalFedora1 = new GISA.PanelObjetoDigitalFedora();
			this.PanelCondicoesAcesso1 = new GISA.PanelCondicoesAcesso();
			this.PanelAVCondicoesAcesso1 = new GISA.PanelAVCondicoesAcesso();
			this.PanelDocumentacaoAssociada1 = new GISA.PanelDocumentacaoAssociada();
			this.PanelNotas1 = new GISA.PanelNotas();
			this.PanelOIControloDescricao1 = new GISA.PanelOIControloDescricao();
			this.PanelIndexacao1 = new GISA.PanelIndexacao();
			this.PanelMensagem1 = new GISA.PanelMensagem();
			this.GisaPanelScroller.SuspendLayout();
			this.pnlToolbarPadding.SuspendLayout();
			this.SuspendLayout();
			//
			//DropDownTreeView1
			//
			this.DropDownTreeView1.GISAFunction = "Descrição";
			this.DropDownTreeView1.Name = "DropDownTreeView1";
			//
			//GisaPanelScroller
			//
			this.GisaPanelScroller.Controls.Add(this.PanelAVCondicoesAcesso1);
			this.GisaPanelScroller.Controls.Add(this.PanelIndexacao1);
			this.GisaPanelScroller.Controls.Add(this.PanelOIControloDescricao1);
			this.GisaPanelScroller.Controls.Add(this.PanelAvaliacaoSeleccaoEliminacao1);
			this.GisaPanelScroller.Controls.Add(this.PanelAvaliacaoDocumentosUnidadesFisicas1);
			this.GisaPanelScroller.Controls.Add(this.PanelIndiceDocumento1);
            this.GisaPanelScroller.Controls.Add(this.PanelObjetoDigitalFedora1);
			this.GisaPanelScroller.Controls.Add(this.PanelNotas1);
			this.GisaPanelScroller.Controls.Add(this.PanelDocumentacaoAssociada1);
			this.GisaPanelScroller.Controls.Add(this.PanelCondicoesAcesso1);
			this.GisaPanelScroller.Controls.Add(this.PanelOrganizacaoOrdenacao1);
			this.GisaPanelScroller.Controls.Add(this.PanelAmbitoConteudo1);
            this.GisaPanelScroller.Controls.Add(this.PanelConteudoInformacional1);
			this.GisaPanelScroller.Controls.Add(this.PanelIncorporacoes1);
			this.GisaPanelScroller.Controls.Add(this.PanelContexto1);
			this.GisaPanelScroller.Controls.Add(this.PanelIdentificacao1);
			this.GisaPanelScroller.Controls.Add(this.PanelOIDimensoesSuporte1);
			this.GisaPanelScroller.Controls.Add(this.PanelMensagem1);
			this.GisaPanelScroller.Name = "GisaPanelScroller";
			this.GisaPanelScroller.Size = new System.Drawing.Size(600, 364);
			//
			//ToolBar
			//
			this.ToolBar.Name = "ToolBar";
			//
			//pnlToolbarPadding
			//
			this.pnlToolbarPadding.DockPadding.Left = 6;
			this.pnlToolbarPadding.DockPadding.Right = 6;
			this.pnlToolbarPadding.Name = "pnlToolbarPadding";
			//
			//PanelIdentificacao1
			//
			this.PanelIdentificacao1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelIdentificacao1.IsLoaded = false;
			this.PanelIdentificacao1.Location = new System.Drawing.Point(0, 0);
			this.PanelIdentificacao1.Name = "PanelIdentificacao1";
			this.PanelIdentificacao1.Size = new System.Drawing.Size(600, 364);
			this.PanelIdentificacao1.TabIndex = 8;
			//
			//PanelOIDimensoesSuporte1
			//
			this.PanelOIDimensoesSuporte1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelOIDimensoesSuporte1.IsLoaded = false;
			this.PanelOIDimensoesSuporte1.Location = new System.Drawing.Point(0, 0);
			this.PanelOIDimensoesSuporte1.Name = "PanelOIDimensoesSuporte1";
			this.PanelOIDimensoesSuporte1.Size = new System.Drawing.Size(600, 364);
			this.PanelOIDimensoesSuporte1.TabIndex = 9;
			this.PanelOIDimensoesSuporte1.Visible = false;
			//
			//PanelContexto1
			//
			this.PanelContexto1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelContexto1.IsLoaded = false;
			this.PanelContexto1.Location = new System.Drawing.Point(0, 0);
			this.PanelContexto1.Name = "PanelContexto1";
			this.PanelContexto1.Size = new System.Drawing.Size(600, 364);
			this.PanelContexto1.TabIndex = 10;
			this.PanelContexto1.Visible = false;
			//
			//PanelAmbitoConteudo1
			//
			this.PanelAmbitoConteudo1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelAmbitoConteudo1.IsLoaded = false;
			this.PanelAmbitoConteudo1.Location = new System.Drawing.Point(0, 0);
			this.PanelAmbitoConteudo1.Name = "PanelAmbitoConteudo1";
			this.PanelAmbitoConteudo1.Size = new System.Drawing.Size(600, 364);
			this.PanelAmbitoConteudo1.TabIndex = 11;
			this.PanelAmbitoConteudo1.Visible = false;
            //
            //PanelConteudoInformacional1
            //
            this.PanelConteudoInformacional1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelConteudoInformacional1.IsLoaded = false;
            this.PanelConteudoInformacional1.Location = new System.Drawing.Point(0, 0);
            this.PanelConteudoInformacional1.Name = "PanelConteudoInformacional1";
            this.PanelConteudoInformacional1.Size = new System.Drawing.Size(600, 364);
            this.PanelConteudoInformacional1.TabIndex = 12;
            this.PanelConteudoInformacional1.Visible = false;
			//
			//PanelAvaliacaoSeleccaoEliminacao1
			//
			this.PanelAvaliacaoSeleccaoEliminacao1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelAvaliacaoSeleccaoEliminacao1.IsLoaded = false;
			this.PanelAvaliacaoSeleccaoEliminacao1.Location = new System.Drawing.Point(0, 0);
			this.PanelAvaliacaoSeleccaoEliminacao1.Name = "PanelAvaliacaoSeleccaoEliminacao1";
			this.PanelAvaliacaoSeleccaoEliminacao1.Size = new System.Drawing.Size(600, 364);
			this.PanelAvaliacaoSeleccaoEliminacao1.TabIndex = 13;
			this.PanelAvaliacaoSeleccaoEliminacao1.Visible = false;
			//
			//PanelAvaliacaoDocumentosUnidadesFisicas1
			//
			this.PanelAvaliacaoDocumentosUnidadesFisicas1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelAvaliacaoDocumentosUnidadesFisicas1.IsLoaded = false;
			this.PanelAvaliacaoDocumentosUnidadesFisicas1.Location = new System.Drawing.Point(0, 0);
			this.PanelAvaliacaoDocumentosUnidadesFisicas1.Name = "PanelAvaliacaoDocumentosUnidadesFisicas1";
			this.PanelAvaliacaoDocumentosUnidadesFisicas1.Size = new System.Drawing.Size(600, 364);
			this.PanelAvaliacaoDocumentosUnidadesFisicas1.TabIndex = 14;
			this.PanelAvaliacaoDocumentosUnidadesFisicas1.Visible = false;
			//
			//PanelIncorporacoes1
			//
			this.PanelIncorporacoes1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelIncorporacoes1.IsLoaded = false;
			this.PanelIncorporacoes1.Location = new System.Drawing.Point(0, 0);
			this.PanelIncorporacoes1.Name = "PanelIncorporacoes1";
			this.PanelIncorporacoes1.Size = new System.Drawing.Size(600, 364);
			this.PanelIncorporacoes1.TabIndex = 15;
			this.PanelIncorporacoes1.Visible = false;
			//
			//PanelOrganizacaoOrdenacao1
			//
			this.PanelOrganizacaoOrdenacao1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelOrganizacaoOrdenacao1.IsLoaded = false;
			this.PanelOrganizacaoOrdenacao1.Location = new System.Drawing.Point(0, 0);
			this.PanelOrganizacaoOrdenacao1.Name = "PanelOrganizacaoOrdenacao1";
			this.PanelOrganizacaoOrdenacao1.Size = new System.Drawing.Size(600, 364);
			this.PanelOrganizacaoOrdenacao1.TabIndex = 16;
			this.PanelOrganizacaoOrdenacao1.Visible = false;
			//
			//PanelIndiceDocumento1
			//
			this.PanelIndiceDocumento1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelIndiceDocumento1.IsLoaded = false;
			this.PanelIndiceDocumento1.Location = new System.Drawing.Point(0, 0);
			this.PanelIndiceDocumento1.Name = "PanelIndiceDocumento1";
			this.PanelIndiceDocumento1.Size = new System.Drawing.Size(600, 364);
			this.PanelIndiceDocumento1.TabIndex = 17;
			this.PanelIndiceDocumento1.Visible = false;
            //
            //PanelObjetoDigitalFedora1
            //
            this.PanelObjetoDigitalFedora1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelObjetoDigitalFedora1.IsLoaded = false;
            this.PanelObjetoDigitalFedora1.Location = new System.Drawing.Point(0, 0);
            this.PanelObjetoDigitalFedora1.Name = "PanelObjetoDigitalFedora1";
            this.PanelObjetoDigitalFedora1.Size = new System.Drawing.Size(600, 364);
            this.PanelObjetoDigitalFedora1.TabIndex = 25;
            this.PanelObjetoDigitalFedora1.Visible = false;
			//
			//PanelCondicoesAcesso1
			//
			this.PanelCondicoesAcesso1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelCondicoesAcesso1.IsLoaded = false;
			this.PanelCondicoesAcesso1.Location = new System.Drawing.Point(0, 0);
			this.PanelCondicoesAcesso1.Name = "PanelCondicoesAcesso1";
			this.PanelCondicoesAcesso1.Size = new System.Drawing.Size(600, 364);
			this.PanelCondicoesAcesso1.TabIndex = 18;
			this.PanelCondicoesAcesso1.Visible = false;
			//
			//PanelAVCondicoesAcesso1
			//
			this.PanelAVCondicoesAcesso1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelAVCondicoesAcesso1.IsLoaded = false;
			this.PanelAVCondicoesAcesso1.Location = new System.Drawing.Point(0, 0);
			this.PanelAVCondicoesAcesso1.Name = "PanelAVCondicoesAcesso1";
			this.PanelAVCondicoesAcesso1.Size = new System.Drawing.Size(600, 364);
			this.PanelAVCondicoesAcesso1.TabIndex = 19;
			this.PanelAVCondicoesAcesso1.Visible = false;
			//
			//PanelDocumentacaoAssociada1
			//
			this.PanelDocumentacaoAssociada1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelDocumentacaoAssociada1.IsLoaded = false;
			this.PanelDocumentacaoAssociada1.Location = new System.Drawing.Point(0, 0);
			this.PanelDocumentacaoAssociada1.Name = "PanelDocumentacaoAssociada1";
			this.PanelDocumentacaoAssociada1.Size = new System.Drawing.Size(600, 364);
			this.PanelDocumentacaoAssociada1.TabIndex = 20;
			this.PanelDocumentacaoAssociada1.Visible = false;
			//
			//PanelNotas1
			//
			this.PanelNotas1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelNotas1.IsLoaded = false;
			this.PanelNotas1.Location = new System.Drawing.Point(0, 0);
			this.PanelNotas1.Name = "PanelNotas1";
			this.PanelNotas1.Size = new System.Drawing.Size(600, 364);
			this.PanelNotas1.TabIndex = 21;
			this.PanelNotas1.Visible = false;
			//
			//PanelOIControloDescricao1
			//
			this.PanelOIControloDescricao1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelOIControloDescricao1.IsLoaded = false;
			this.PanelOIControloDescricao1.Location = new System.Drawing.Point(0, 0);
			this.PanelOIControloDescricao1.Name = "PanelControloDescricao1";
			this.PanelOIControloDescricao1.Size = new System.Drawing.Size(700, 415);
			this.PanelOIControloDescricao1.TabIndex = 22;
			//
			//PanelIndexacao1
			//
			this.PanelIndexacao1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelIndexacao1.IsLoaded = false;
			this.PanelIndexacao1.Location = new System.Drawing.Point(0, 0);
			this.PanelIndexacao1.Name = "PanelIndexacao1";
			this.PanelIndexacao1.Size = new System.Drawing.Size(600, 364);
			this.PanelIndexacao1.TabIndex = 23;
			this.PanelIndexacao1.Visible = false;
			//
			//PanelMensagem1
			//
			this.PanelMensagem1.BackColor = System.Drawing.SystemColors.Control;
			this.PanelMensagem1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelMensagem1.IsLoaded = false;
			this.PanelMensagem1.Location = new System.Drawing.Point(0, 0);
			this.PanelMensagem1.Name = "PanelMensagem1";
			this.PanelMensagem1.Size = new System.Drawing.Size(600, 364);
			this.PanelMensagem1.TabIndex = 24;
			this.PanelMensagem1.Visible = false;
			//
			//FRDOIRecolha
			//
			this.Name = "FRDOIRecolha";
			this.Size = new System.Drawing.Size(600, 415);
			this.GisaPanelScroller.ResumeLayout(false);
			this.pnlToolbarPadding.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	#endregion

		private bool isLoaded = false;
		public override void LoadData()
		{
			try
			{
				((frmMain)TopLevelControl).EnterWaitMode();
				GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
				try
				{
                    GisaDataSetHelper.ManageDatasetConstraints(false);

					if (! isLoaded)
					{
						if (CurrentContext.NivelEstrututalDocumental == null)
						{
							CurrentFRDBase = null;
							return;
						}
						
						FRDRule.Current.ReloadPubNivelActualData(GisaDataSetHelper.GetInstance(), CurrentContext.NivelEstrututalDocumental.ID, ho.Connection);
						string QueryFilter = "IDNivel=" + CurrentContext.NivelEstrututalDocumental.ID.ToString() + " AND IDTipoFRDBase=" + System.Enum.Format(typeof(TipoFRDBase), TipoFRDBase.FRDOIRecolha, "D");
						try
						{
							// Tentar obter o FRD pretendido, caso não exista cria-se um vazio
							CurrentFRDBase = (GISADataset.FRDBaseRow)(GisaDataSetHelper.GetInstance().FRDBase.Select(QueryFilter)[0]);
						}
						catch (IndexOutOfRangeException)
						{
							CurrentFRDBase = GisaDataSetHelper.GetInstance().FRDBase.AddFRDBaseRow(CurrentContext.NivelEstrututalDocumental, (GISADataset.TipoFRDBaseRow)(GisaDataSetHelper.GetInstance().TipoFRDBase. Select("ID=" + DomainValuesHelper.stringifyEnumValue(TipoFRDBase.FRDOIRecolha))[0]), "", "", new byte[]{}, 0);
						}
						
						if (CurrentFRDBase == null || CurrentFRDBase.RowState == DataRowState.Detached || 
                            CurrentContext.NivelEstrututalDocumental.RowState == DataRowState.Detached)
							return;

						if (CurrentContext.NivelEstrututalDocumental.CatCode.Trim().Equals("CA"))
							FRDRule.Current.LoadFRDOIRecolhaData(GisaDataSetHelper.GetInstance(), CurrentContext.NivelEstrututalDocumental.ID, System.Enum.Format(typeof(TipoFRDBase), TipoFRDBase.FRDOIRecolha, "D"), ho.Connection);
						
                        isLoaded = true;
					}
                    
					GISAPanel selectedPanel = (GISAPanel)this.DropDownTreeView1.SelectedNode.Tag;
					if (! selectedPanel.IsLoaded)
					{
						long startTicks = 0;
						startTicks = DateTime.Now.Ticks;
						selectedPanel.LoadData(CurrentFRDBase, ho.Connection);
						Debug.WriteLine("Time dispend loading " + selectedPanel.ToString() + ": " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());
					}
					GisaDataSetHelper.ManageDatasetConstraints(true);
				}
				catch (System.Data.ConstraintException Ex)
				{
					Trace.WriteLine(Ex);
					GisaDataSetHelper.FixDataSet(GisaDataSetHelper.GetInstance(), ho.Connection);
				}
				catch (Exception e)
				{
					Trace.WriteLine(e);
					throw;
				}
				finally
				{
					ho.Dispose();
				}
			}
			finally
			{
				((frmMain)TopLevelControl).LeaveWaitMode();
			}
		}

		private bool mLastStateEditPermisson = true;
		private bool LastStateEditPermisson
		{
			get {return mLastStateEditPermisson;}
			set {mLastStateEditPermisson = value;}
		}

		public override void ModelToView()
		{
			try
			{
				((frmMain)TopLevelControl).EnterWaitMode();
				// se nao existir um contexto definido os slavepanels não devem ser apresentados
				if (CurrentFRDBase == null || CurrentFRDBase.RowState == DataRowState.Detached || CurrentContext.NivelEstrututalDocumental.RowState == DataRowState.Detached)
				{
					this.Visible = false;
					return;
				}

				this.Visible = true;
				GISAPanel selectedPanel = null;

				try
				{
					GisaPrincipalPermission tempWith1 = new GisaPrincipalPermission(SessionHelper.GetGisaPrincipal(), this.GetType().FullName, GisaPrincipalPermission.WRITE);
					tempWith1.Demand();
					if (! PermissoesHelper.AllowEdit)
						throw new System.Security.SecurityException();
					else if (! LastStateEditPermisson)
					{
						// Para garantir que o estado dos controlos está de acordo com as regras definidas quando a transição é feita
						// de um contexto sem permissão de edição para um com, é necessário colocar o estado desses controlos a true
						// para em seguida se redefinir esse estado no ModelToViewPanels()
						selectedPanel = (GISAPanel)this.DropDownTreeView1.SelectedNode.Tag;
						if (selectedPanel.IsLoaded && ! selectedPanel.IsPopulated)
							selectedPanel.ModelToView();

                        GUIHelper.GUIHelper.makeReadable(selectedPanel);
					}
					else
					{
						selectedPanel = (GISAPanel)this.DropDownTreeView1.SelectedNode.Tag;
						if (selectedPanel.IsLoaded && ! selectedPanel.IsPopulated)
						{
							long startTicks = 0;
							startTicks = DateTime.Now.Ticks;
							selectedPanel.ModelToView();
							Debug.WriteLine("Time dispend model to view " + selectedPanel.ToString() + ": " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());
						}
					}
					LastStateEditPermisson = true;
				}
				catch (System.Security.SecurityException)
				{
					selectedPanel = (GISAPanel)this.DropDownTreeView1.SelectedNode.Tag;
					selectedPanel.ModelToView();
					LastStateEditPermisson = false;
                    GUIHelper.GUIHelper.makeReadOnly(selectedPanel);
				}
			}
			finally
			{
				((frmMain)TopLevelControl).LeaveWaitMode();
			}
		}

		public override bool ViewToModel()
		{
			// Prever os casos em que estamos num nível sem FRD (EDs ou GAs) ou num nível que acaba de ser eliminado
			if (CurrentFRDBase != null && ! (CurrentFRDBase.RowState == DataRowState.Detached))
			{
				bool successful = base.ViewToModel();

				if (! successful)
					return successful;

				bool changesMade = PersistencyHelper.hasCurrentDatasetChanges();
				if (changesMade)
					AddRegistration(CurrentFRDBase, existsModifiedData);

				return successful;
			}
			else
				return true;
		}

        private void AddRegistration(GISADataset.FRDBaseRow frdBase, bool existsModifiedData)
        {
            GISADataset.TrusteeUserRow tuAuthor = GetSelectedAuthor();
            GISADataset.TrusteeUserRow tuOperator = SessionHelper.GetGisaPrincipal().TrusteeUserOperator;
            DateTime data;
            if (PanelOIControloDescricao1 != null)
                data = PanelOIControloDescricao1.ControloRevisoes1.dtpRecolha.Value;
            else
                data = DateTime.Now;

            GISA.Model.RecordRegisterHelper.RegisterRecordModificationFRD(frdBase, existsModifiedData, tuOperator, tuAuthor, data);
        }

        private GISADataset.TrusteeUserRow GetSelectedAuthor()
        {
            GISADataset.TrusteeUserRow tuAuthorRow = null;
            // pode não existir selecção, nesse caso a datarow encontrada foi uma "extra" adicionada e estará detached

            if (PanelOIControloDescricao1 != null &&
                PanelOIControloDescricao1.IsLoaded &&
                PanelOIControloDescricao1.ControloRevisoes1.ControloAutores1.SelectedAutor != null &&
                !(((DataRow)PanelOIControloDescricao1.ControloRevisoes1.ControloAutores1.SelectedAutor).RowState == DataRowState.Detached) &&
                ((GISADataset.TrusteeRow)(PanelOIControloDescricao1.ControloRevisoes1.ControloAutores1.SelectedAutor)).GetTrusteeUserRows().Length > 0)

                tuAuthorRow = ((GISADataset.TrusteeRow)PanelOIControloDescricao1.ControloRevisoes1.ControloAutores1.SelectedAutor).GetTrusteeUserRows()[0];
            else if (SessionHelper.GetGisaPrincipal().TrusteeUserAuthor != null && !(SessionHelper.GetGisaPrincipal().TrusteeUserAuthor.RowState == DataRowState.Detached))
                tuAuthorRow = SessionHelper.GetGisaPrincipal().TrusteeUserAuthor;

            return tuAuthorRow;
        }

		public override void Deactivate()
		{
			DeactivatePanels();
			isLoaded = false;
			CurrentFRDBase = null;
			existsModifiedData = false;
		}

        public override PersistencyHelper.SaveResult Save()
		{
			return Save(false);
		}

        public override PersistencyHelper.SaveResult Save(bool activateOpcaoCancelar)
		{
			if (this.CurrentFRDBase == null)
                return PersistencyHelper.SaveResult.unsuccessful;

            List<long> IDs = new List<long>();
            List<string> idsToUpdate = new List<string>();
			List<NivelRule.PublicacaoDocumentos> DocsID = new List<NivelRule.PublicacaoDocumentos> ();
			GISADataset.RelacaoHierarquicaRow rhRow = null;
            PersistencyHelper.SaveResult successfulSave = PersistencyHelper.SaveResult.unsuccessful;

			if ((CurrentFRDBase.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().Length > 0))
			{
				rhRow = CurrentFRDBase.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0];

				PersistencyHelper.PublishSubDocumentosPreSaveArguments psArgs1 = new PersistencyHelper.PublishSubDocumentosPreSaveArguments();
				PersistencyHelper.AvaliaDocumentosTabelaPreSaveArguments psArgs2 = new PersistencyHelper.AvaliaDocumentosTabelaPreSaveArguments();

                // publicação de subdocumentos baseado na publicação dos documentos respectivos
				if (rhRow.IDTipoNivelRelacionado == TipoNivelRelacionado.D)
				{
					//contexto é um documento
                    if (CurrentFRDBase.GetSFRDAvaliacaoRows().Length > 0)
                    {
                        GISADataset.SFRDAvaliacaoRow relRow = CurrentFRDBase.GetSFRDAvaliacaoRows()[0];

                        if ((relRow.RowState == DataRowState.Added && relRow.Publicar) || relRow.RowState == DataRowState.Modified)
                        {
                            DocsID.Add(new DBAbstractDataLayer.DataAccessRules.NivelRule.PublicacaoDocumentos(CurrentFRDBase.NivelRow.ID, CurrentFRDBase.GetSFRDAvaliacaoRows()[0].Publicar));
                            IDs.Add(CurrentFRDBase.NivelRow.ID);
                            PermissoesHelper.ChangeDocPermissionPublicados(CurrentFRDBase.NivelRow.ID, (CurrentFRDBase.GetSFRDAvaliacaoRows())[0].Publicar);
                        }
                    }
				}
                else if (rhRow.IDTipoNivelRelacionado == TipoNivelRelacionado.SR || rhRow.IDTipoNivelRelacionado == TipoNivelRelacionado.SSR)
                {
                    //o contexto é uma série ou subsérie e é alterado o estado de publicação de vários documentos simultaneamente
                    foreach (GISADataset.SFRDAvaliacaoRow sfrdAv in GisaDataSetHelper.GetInstance().SFRDAvaliacao.Select("", "", DataViewRowState.Added | DataViewRowState.ModifiedCurrent))
                    {
                        if (sfrdAv.FRDBaseRow.ID != CurrentFRDBase.ID)
                        {
                            DocsID.Add(new DBAbstractDataLayer.DataAccessRules.NivelRule.PublicacaoDocumentos(sfrdAv.FRDBaseRow.NivelRow.ID, sfrdAv.Publicar));
                            IDs.Add(sfrdAv.FRDBaseRow.NivelRow.ID);
                            PermissoesHelper.ChangeDocPermissionPublicados(sfrdAv.FRDBaseRow.NivelRow.ID, sfrdAv.Publicar);
                        }
                        else
                            IDs.Add(sfrdAv.FRDBaseRow.IDNivel);
                    }

                    if (!(CurrentFRDBase.GetSFRDAvaliacaoRows().Length == 0 || CurrentFRDBase.GetSFRDAvaliacaoRows()[0].IsIDModeloAvaliacaoNull() || (!(CurrentFRDBase.GetSFRDAvaliacaoRows()[0].RowState == DataRowState.Added) && !(CurrentFRDBase.GetSFRDAvaliacaoRows()[0]["IDModeloAvaliacao", DataRowVersion.Original] == DBNull.Value) && ((long)(CurrentFRDBase.GetSFRDAvaliacaoRows()[0]["IDModeloAvaliacao", DataRowVersion.Original])) == (long)(CurrentFRDBase.GetSFRDAvaliacaoRows()[0]["IDModeloAvaliacao", DataRowVersion.Current]))))
                    {
                        psArgs2.frdID = CurrentFRDBase.ID;
                        psArgs2.modeloAvaliacaoID = CurrentFRDBase.GetSFRDAvaliacaoRows()[0].IDModeloAvaliacao;
                        psArgs2.avaliacaoTabela = CurrentFRDBase.GetSFRDAvaliacaoRows()[0].AvaliacaoTabela;
                        if (CurrentFRDBase.GetSFRDAvaliacaoRows()[0].IsPrazoConservacaoNull())
                            psArgs2.prazoConservacao = 0;
                        else
                            psArgs2.prazoConservacao = CurrentFRDBase.GetSFRDAvaliacaoRows()[0].PrazoConservacao;

                        psArgs2.preservar = CurrentFRDBase.GetSFRDAvaliacaoRows()[0].Preservar;
                    }
                }
                else
                {
                    foreach (GISADataset.SFRDAvaliacaoRow sfrdAv in GisaDataSetHelper.GetInstance().SFRDAvaliacao.Select("", "", DataViewRowState.Added | DataViewRowState.ModifiedCurrent))
                    {
                        PermissoesHelper.ChangeDocPermissionPublicados(sfrdAv.FRDBaseRow.NivelRow.ID, sfrdAv.Publicar);
                        IDs.Add(sfrdAv.FRDBaseRow.IDNivel);
                    }
                }

                // actualização do objecto digital caso exista ou o módulo esteja activo
                rhRow = CurrentFRDBase.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First();
                var objDigital = default(ObjDigital);
                if (rhRow.IDTipoNivelRelacionado >= (long)TipoNivelRelacionado.D && SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsFedoraEnable())
                {
                    GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                    try
                    {
                        GisaDataSetHelper.ManageDatasetConstraints(false);
                        if (!PanelAmbitoConteudo1.IsLoaded) PanelAmbitoConteudo1.LoadData(CurrentFRDBase, ho.Connection);
                        if (!PanelIndexacao1.IsLoaded) PanelIndexacao1.LoadData(CurrentFRDBase, ho.Connection);
                        GisaDataSetHelper.ManageDatasetConstraints(true);
                    }
                    catch (System.Data.ConstraintException Ex)
                    {
                        Trace.WriteLine(Ex);
                        GisaDataSetHelper.FixDataSet(GisaDataSetHelper.GetInstance(), ho.Connection);
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(e);
                        throw;
                    }
                    finally
                    {
                        ho.Dispose();
                    }
                    
                    // verificar alterações na tipologia e indexação
                    var tipologia = string.Empty;
                    var hasNewTip = Nivel.HasTipologiaChanged(CurrentFRDBase, out tipologia);
                    var assuntos = Nivel.HasIndexacaoChanged(CurrentFRDBase);

                    if (hasNewTip || assuntos != null)
                    {
                        objDigital = FedoraHelper.UpdateTipAssuntos(CurrentFRDBase.NivelRow, hasNewTip ? tipologia : null, assuntos);
                    }
                }

                // actualizar objecto digital caso exista
                var preTransactionAction = new PreTransactionAction();
                var fedArgs = new PersistencyHelper.FedoraIngestPreTransactionArguments();
                preTransactionAction.args = fedArgs;

                preTransactionAction.preTransactionDelegate = delegate(PersistencyHelper.PreTransactionArguments preTransactionArgs)
                {
                    var odComp = this.PanelObjetoDigitalFedora1.GetObjDigitalComp();
                    var odSimples = this.PanelObjetoDigitalFedora1.GetObjDigitalSimples();
                    bool ingestSuccess = true;
                    string msg = null;

                    if (odComp != null)
                        ingestSuccess = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.Ingest(odComp, out msg);
                    else if (odSimples != null && odSimples.Count > 0)
                        odSimples.ForEach(od => ingestSuccess &= SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.Ingest(od, out msg));
                    
                    if (ingestSuccess)
                        this.PanelObjetoDigitalFedora1.odHelper.newObjects.Keys.ToList().ForEach(k => { k.pid = this.PanelObjetoDigitalFedora1.odHelper.newObjects[k].pid; });

                    preTransactionArgs.cancelAction = !ingestSuccess;
                    preTransactionArgs.message = msg;
                };

				psArgs1.DocsID = DocsID;

                PostSaveAction postSaveAction = new PostSaveAction();
                PersistencyHelper.UpdatePermissionsPostSaveArguments argsPSAction = new PersistencyHelper.UpdatePermissionsPostSaveArguments();
                argsPSAction.NiveisIDs = IDs;
                argsPSAction.TrusteeID = PermissoesHelper.GrpAcessoPublicados.ID;
                postSaveAction.args = argsPSAction;

                postSaveAction.postSaveDelegate = delegate(PersistencyHelper.PostSaveArguments postSaveArgs)
                {
                    if (!postSaveArgs.cancelAction && argsPSAction.NiveisIDs.Count > 0)
                    {
                        if (psArgs1 != null)
                            idsToUpdate = psArgs1.idsToUpdate;
                    }
                };

				PersistencyHelper.AvaliacaoPublicacaoPreSaveArguments args = new PersistencyHelper.AvaliacaoPublicacaoPreSaveArguments();
				args.psArgs1 = psArgs1;
				args.psArgs2 = psArgs2;

                successfulSave = PersistencyHelper.save(AvaliacaoPublicacao, args, postSaveAction, preTransactionAction, activateOpcaoCancelar);
			}
			else
				successfulSave = PersistencyHelper.save();

            if (successfulSave == PersistencyHelper.SaveResult.successful)
			{
				GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
				try
				{
                    if (CurrentFRDBase.NivelRow.IDTipoNivel == TipoNivel.DOCUMENTAL)
                    {
                        if (idsToUpdate == null) idsToUpdate = new List<string>();
                        idsToUpdate.Add(CurrentFRDBase.NivelRow.ID.ToString());
                        GISA.Search.Updater.updateNivelDocumental(idsToUpdate);
                    }
                    ((frmMain)TopLevelControl).SetServerStatus();
				}
                catch (GISA.Search.UpdateServerException)
                {
                    ((frmMain)TopLevelControl).SetServerStatus();
                }
				catch (Exception ex)
				{
					Trace.WriteLine(ex.ToString());
					throw;
				}
				finally
				{
					ho.Dispose();
				}
			}

			return successfulSave;
		}        

		private void AvaliacaoPublicacao(PersistencyHelper.PreSaveArguments args)
		{
			PersistencyHelper.PublishSubDocumentosPreSaveArguments psdPsa = null;
			psdPsa = (PersistencyHelper.PublishSubDocumentosPreSaveArguments)(((PersistencyHelper.AvaliacaoPublicacaoPreSaveArguments)args).psArgs1);
			psdPsa.tran = args.tran;
			PublishSubDocumentos(psdPsa);

			PersistencyHelper.AvaliaDocumentosTabelaPreSaveArguments adtPsa = null;
			adtPsa = (PersistencyHelper.AvaliaDocumentosTabelaPreSaveArguments)(((PersistencyHelper.AvaliacaoPublicacaoPreSaveArguments)args).psArgs2);
			adtPsa.tran = args.tran;
			AvaliaDocumentosTabela(adtPsa);
		}

		private void PublishSubDocumentos(PersistencyHelper.PreSaveArguments args)
		{
			PersistencyHelper.PublishSubDocumentosPreSaveArguments psdPsa = null;
			psdPsa = (PersistencyHelper.PublishSubDocumentosPreSaveArguments)args;

			if (psdPsa.DocsID.Count > 0)
			{
                psdPsa.idsToUpdate = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.ExecutePublishSubDocumentos(psdPsa.DocsID, PermissoesHelper.GrpAcessoPublicados.ID, psdPsa.tran);
			}
		}

		private void AvaliaDocumentosTabela(PersistencyHelper.PreSaveArguments args)
		{
			PersistencyHelper.AvaliaDocumentosTabelaPreSaveArguments adtPsa = null;
			adtPsa = (PersistencyHelper.AvaliaDocumentosTabelaPreSaveArguments)args;

			if (adtPsa.frdID > long.MinValue)
			{
				FRDRule.Current.ExecuteAvaliaDocumentosTabela(adtPsa.frdID, adtPsa.modeloAvaliacaoID, adtPsa.avaliacaoTabela, adtPsa.preservar, adtPsa.prazoConservacao, adtPsa.tran);
			}
		}

		protected override bool isInnerContextValid()
		{
            return CurrentFRDBase != null && !(CurrentFRDBase.RowState == DataRowState.Detached) && CurrentFRDBase.isDeleted == 0;
		}

		protected override bool isOuterContextValid()
		{
			return CurrentContext.NivelEstrututalDocumental != null;
		}

		protected override bool isOuterContextDeleted()
		{
			Debug.Assert(CurrentContext.NivelEstrututalDocumental != null, "CurrentContext.NivelEstrututalDocumental Is Nothing");
			return CurrentContext.NivelEstrututalDocumental.RowState == DataRowState.Detached;
		}

		protected override bool hasReadPermission()
		{
			return PermissoesHelper.AllowRead;
		}

		protected override void addContextChangeHandlers()
		{
			CurrentContext.NivelEstrututalDocumentalChanged += this.Recontextualize;
            CurrentContext.AddRevisionEvent += RegisterModification;
		}

		protected override void removeContextChangeHandlers()
		{
			CurrentContext.NivelEstrututalDocumentalChanged -= this.Recontextualize;
            CurrentContext.AddRevisionEvent -= RegisterModification;
		}

        protected override void RegisterModification(object o, RegisterModificationEventArgs e)
		{
            AddRegistration((GISADataset.FRDBaseRow)e.Context, true);
		}

		protected override PanelMensagem GetDeletedContextMessage()
		{
			PanelMensagem1.LblMensagem.Text = "Esta relação hierárquica foi eliminada não sendo, por isso, possível apresentar o nível em causa.";
			return PanelMensagem1;
		}

		protected override PanelMensagem GetNoContextMessage()
		{
			PanelMensagem1.LblMensagem.Text = "Para visualizar os detalhes deverá selecionar um nível de descrição no painel superior.";
			return PanelMensagem1;
		}

		protected override PanelMensagem GetNoReadPermissionMessage()
		{
			PanelMensagem1.LblMensagem.Text = "Não tem permissão para visualizar os detalhes do nível de descrição selecionado no painel superior.";
			return PanelMensagem1;
		}
	}

} //end of root namespace
