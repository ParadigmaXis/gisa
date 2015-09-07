using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA
{
	public class PanelCondicoesAcesso : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelCondicoesAcesso() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call

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
		internal System.Windows.Forms.TabControl tabCondicoes;
		internal System.Windows.Forms.TabPage tabFormaSuporteAcondicionamento;
		internal System.Windows.Forms.GroupBox grpTipo;
		internal System.Windows.Forms.CheckedListBox chkLstFormaSuporteAcondicionamento;
		internal System.Windows.Forms.TabPage tabTecnicasRegisto;
		internal System.Windows.Forms.GroupBox grpTecnicaRegisto;
		internal System.Windows.Forms.CheckedListBox chkLstTecnicaRegisto;
		internal System.Windows.Forms.TabPage tabEstadoConservacao;
		internal System.Windows.Forms.GroupBox grpEstadoConservacao;
		internal System.Windows.Forms.CheckedListBox chkLstEstadoConservacao;
		internal System.Windows.Forms.TabPage tabMaterialSuporte;
		internal System.Windows.Forms.GroupBox grpMatSup;
		internal System.Windows.Forms.CheckedListBox chkLstMaterialSuporte;
		internal System.Windows.Forms.GroupBox grpAuxiliaresPesquisa;
		internal System.Windows.Forms.TextBox txtAuxiliaresPesquisa;
		internal System.Windows.Forms.GroupBox grpCaracteristicasFisicas;


		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.grpCaracteristicasFisicas = new System.Windows.Forms.GroupBox();
            this.tabCondicoes = new System.Windows.Forms.TabControl();
            this.tabFormaSuporteAcondicionamento = new System.Windows.Forms.TabPage();
            this.grpTipo = new System.Windows.Forms.GroupBox();
            this.chkLstFormaSuporteAcondicionamento = new System.Windows.Forms.CheckedListBox();
            this.tabMaterialSuporte = new System.Windows.Forms.TabPage();
            this.grpMatSup = new System.Windows.Forms.GroupBox();
            this.chkLstMaterialSuporte = new System.Windows.Forms.CheckedListBox();
            this.tabTecnicasRegisto = new System.Windows.Forms.TabPage();
            this.grpTecnicaRegisto = new System.Windows.Forms.GroupBox();
            this.chkLstTecnicaRegisto = new System.Windows.Forms.CheckedListBox();
            this.tabEstadoConservacao = new System.Windows.Forms.TabPage();
            this.grpEstadoConservacao = new System.Windows.Forms.GroupBox();
            this.chkLstEstadoConservacao = new System.Windows.Forms.CheckedListBox();
            this.grpAuxiliaresPesquisa = new System.Windows.Forms.GroupBox();
            this.txtAuxiliaresPesquisa = new System.Windows.Forms.TextBox();
            this.grpCaracteristicasFisicas.SuspendLayout();
            this.tabCondicoes.SuspendLayout();
            this.tabFormaSuporteAcondicionamento.SuspendLayout();
            this.grpTipo.SuspendLayout();
            this.tabMaterialSuporte.SuspendLayout();
            this.grpMatSup.SuspendLayout();
            this.tabTecnicasRegisto.SuspendLayout();
            this.grpTecnicaRegisto.SuspendLayout();
            this.tabEstadoConservacao.SuspendLayout();
            this.grpEstadoConservacao.SuspendLayout();
            this.grpAuxiliaresPesquisa.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCaracteristicasFisicas
            // 
            this.grpCaracteristicasFisicas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCaracteristicasFisicas.Controls.Add(this.tabCondicoes);
            this.grpCaracteristicasFisicas.Location = new System.Drawing.Point(3, 3);
            this.grpCaracteristicasFisicas.Name = "grpCaracteristicasFisicas";
            this.grpCaracteristicasFisicas.Size = new System.Drawing.Size(794, 293);
            this.grpCaracteristicasFisicas.TabIndex = 1;
            this.grpCaracteristicasFisicas.TabStop = false;
            this.grpCaracteristicasFisicas.Text = "4.4. Características físicas e requisitos técnicos";
            // 
            // tabCondicoes
            // 
            this.tabCondicoes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCondicoes.Controls.Add(this.tabFormaSuporteAcondicionamento);
            this.tabCondicoes.Controls.Add(this.tabMaterialSuporte);
            this.tabCondicoes.Controls.Add(this.tabTecnicasRegisto);
            this.tabCondicoes.Controls.Add(this.tabEstadoConservacao);
            this.tabCondicoes.Location = new System.Drawing.Point(8, 16);
            this.tabCondicoes.Name = "tabCondicoes";
            this.tabCondicoes.SelectedIndex = 0;
            this.tabCondicoes.Size = new System.Drawing.Size(778, 269);
            this.tabCondicoes.TabIndex = 1;
            // 
            // tabFormaSuporteAcondicionamento
            // 
            this.tabFormaSuporteAcondicionamento.Controls.Add(this.grpTipo);
            this.tabFormaSuporteAcondicionamento.Location = new System.Drawing.Point(4, 22);
            this.tabFormaSuporteAcondicionamento.Name = "tabFormaSuporteAcondicionamento";
            this.tabFormaSuporteAcondicionamento.Size = new System.Drawing.Size(770, 243);
            this.tabFormaSuporteAcondicionamento.TabIndex = 0;
            this.tabFormaSuporteAcondicionamento.Text = "Forma de suporte e/ou acondicionamento";
            // 
            // grpTipo
            // 
            this.grpTipo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTipo.Controls.Add(this.chkLstFormaSuporteAcondicionamento);
            this.grpTipo.Location = new System.Drawing.Point(8, 8);
            this.grpTipo.Name = "grpTipo";
            this.grpTipo.Size = new System.Drawing.Size(754, 224);
            this.grpTipo.TabIndex = 2;
            this.grpTipo.TabStop = false;
            // 
            // chkLstFormaSuporteAcondicionamento
            // 
            this.chkLstFormaSuporteAcondicionamento.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLstFormaSuporteAcondicionamento.IntegralHeight = false;
            this.chkLstFormaSuporteAcondicionamento.Location = new System.Drawing.Point(8, 17);
            this.chkLstFormaSuporteAcondicionamento.MultiColumn = true;
            this.chkLstFormaSuporteAcondicionamento.Name = "chkLstFormaSuporteAcondicionamento";
            this.chkLstFormaSuporteAcondicionamento.Size = new System.Drawing.Size(738, 199);
            this.chkLstFormaSuporteAcondicionamento.TabIndex = 1;
            // 
            // tabMaterialSuporte
            // 
            this.tabMaterialSuporte.Controls.Add(this.grpMatSup);
            this.tabMaterialSuporte.Location = new System.Drawing.Point(4, 22);
            this.tabMaterialSuporte.Name = "tabMaterialSuporte";
            this.tabMaterialSuporte.Size = new System.Drawing.Size(592, 238);
            this.tabMaterialSuporte.TabIndex = 1;
            this.tabMaterialSuporte.Text = "Material de suporte";
            this.tabMaterialSuporte.Visible = false;
            // 
            // grpMatSup
            // 
            this.grpMatSup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMatSup.Controls.Add(this.chkLstMaterialSuporte);
            this.grpMatSup.Location = new System.Drawing.Point(8, 8);
            this.grpMatSup.Name = "grpMatSup";
            this.grpMatSup.Size = new System.Drawing.Size(576, 224);
            this.grpMatSup.TabIndex = 2;
            this.grpMatSup.TabStop = false;
            // 
            // chkLstMaterialSuporte
            // 
            this.chkLstMaterialSuporte.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLstMaterialSuporte.IntegralHeight = false;
            this.chkLstMaterialSuporte.Location = new System.Drawing.Point(8, 17);
            this.chkLstMaterialSuporte.MultiColumn = true;
            this.chkLstMaterialSuporte.Name = "chkLstMaterialSuporte";
            this.chkLstMaterialSuporte.Size = new System.Drawing.Size(560, 199);
            this.chkLstMaterialSuporte.TabIndex = 1;
            // 
            // tabTecnicasRegisto
            // 
            this.tabTecnicasRegisto.Controls.Add(this.grpTecnicaRegisto);
            this.tabTecnicasRegisto.Location = new System.Drawing.Point(4, 22);
            this.tabTecnicasRegisto.Name = "tabTecnicasRegisto";
            this.tabTecnicasRegisto.Size = new System.Drawing.Size(592, 238);
            this.tabTecnicasRegisto.TabIndex = 2;
            this.tabTecnicasRegisto.Text = "Técnica de registo";
            this.tabTecnicasRegisto.Visible = false;
            // 
            // grpTecnicaRegisto
            // 
            this.grpTecnicaRegisto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTecnicaRegisto.Controls.Add(this.chkLstTecnicaRegisto);
            this.grpTecnicaRegisto.Location = new System.Drawing.Point(8, 8);
            this.grpTecnicaRegisto.Name = "grpTecnicaRegisto";
            this.grpTecnicaRegisto.Size = new System.Drawing.Size(576, 224);
            this.grpTecnicaRegisto.TabIndex = 2;
            this.grpTecnicaRegisto.TabStop = false;
            // 
            // chkLstTecnicaRegisto
            // 
            this.chkLstTecnicaRegisto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLstTecnicaRegisto.IntegralHeight = false;
            this.chkLstTecnicaRegisto.Location = new System.Drawing.Point(8, 17);
            this.chkLstTecnicaRegisto.MultiColumn = true;
            this.chkLstTecnicaRegisto.Name = "chkLstTecnicaRegisto";
            this.chkLstTecnicaRegisto.Size = new System.Drawing.Size(560, 199);
            this.chkLstTecnicaRegisto.TabIndex = 1;
            // 
            // tabEstadoConservacao
            // 
            this.tabEstadoConservacao.Controls.Add(this.grpEstadoConservacao);
            this.tabEstadoConservacao.Location = new System.Drawing.Point(4, 22);
            this.tabEstadoConservacao.Name = "tabEstadoConservacao";
            this.tabEstadoConservacao.Size = new System.Drawing.Size(592, 238);
            this.tabEstadoConservacao.TabIndex = 3;
            this.tabEstadoConservacao.Text = "Estado de conservação";
            this.tabEstadoConservacao.Visible = false;
            // 
            // grpEstadoConservacao
            // 
            this.grpEstadoConservacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpEstadoConservacao.Controls.Add(this.chkLstEstadoConservacao);
            this.grpEstadoConservacao.Location = new System.Drawing.Point(8, 8);
            this.grpEstadoConservacao.Name = "grpEstadoConservacao";
            this.grpEstadoConservacao.Size = new System.Drawing.Size(576, 224);
            this.grpEstadoConservacao.TabIndex = 2;
            this.grpEstadoConservacao.TabStop = false;
            // 
            // chkLstEstadoConservacao
            // 
            this.chkLstEstadoConservacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLstEstadoConservacao.IntegralHeight = false;
            this.chkLstEstadoConservacao.Location = new System.Drawing.Point(8, 17);
            this.chkLstEstadoConservacao.MultiColumn = true;
            this.chkLstEstadoConservacao.Name = "chkLstEstadoConservacao";
            this.chkLstEstadoConservacao.Size = new System.Drawing.Size(560, 199);
            this.chkLstEstadoConservacao.TabIndex = 1;
            // 
            // grpAuxiliaresPesquisa
            // 
            this.grpAuxiliaresPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAuxiliaresPesquisa.Controls.Add(this.txtAuxiliaresPesquisa);
            this.grpAuxiliaresPesquisa.Location = new System.Drawing.Point(3, 304);
            this.grpAuxiliaresPesquisa.Name = "grpAuxiliaresPesquisa";
            this.grpAuxiliaresPesquisa.Size = new System.Drawing.Size(794, 293);
            this.grpAuxiliaresPesquisa.TabIndex = 5;
            this.grpAuxiliaresPesquisa.TabStop = false;
            this.grpAuxiliaresPesquisa.Text = "4.5. Instrumentos de pesquisa";
            // 
            // txtAuxiliaresPesquisa
            // 
            this.txtAuxiliaresPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAuxiliaresPesquisa.Location = new System.Drawing.Point(8, 16);
            this.txtAuxiliaresPesquisa.Multiline = true;
            this.txtAuxiliaresPesquisa.Name = "txtAuxiliaresPesquisa";
            this.txtAuxiliaresPesquisa.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAuxiliaresPesquisa.Size = new System.Drawing.Size(778, 269);
            this.txtAuxiliaresPesquisa.TabIndex = 0;
            // 
            // PanelCondicoesAcesso
            // 
            this.Controls.Add(this.grpAuxiliaresPesquisa);
            this.Controls.Add(this.grpCaracteristicasFisicas);
            this.Name = "PanelCondicoesAcesso";
            this.grpCaracteristicasFisicas.ResumeLayout(false);
            this.tabCondicoes.ResumeLayout(false);
            this.tabFormaSuporteAcondicionamento.ResumeLayout(false);
            this.grpTipo.ResumeLayout(false);
            this.tabMaterialSuporte.ResumeLayout(false);
            this.grpMatSup.ResumeLayout(false);
            this.tabTecnicasRegisto.ResumeLayout(false);
            this.grpTecnicaRegisto.ResumeLayout(false);
            this.tabEstadoConservacao.ResumeLayout(false);
            this.grpEstadoConservacao.ResumeLayout(false);
            this.grpAuxiliaresPesquisa.ResumeLayout(false);
            this.grpAuxiliaresPesquisa.PerformLayout();
            this.ResumeLayout(false);

		}

	#endregion

		private GISADataset.FRDBaseRow CurrentFRDBase;
		private GISADataset.SFRDCondicaoDeAcessoRow CurrentCondicaoDeAcesso;
		private DomainValueListBoxController TecnicaRegistoController;
		private DomainValueListBoxController FormaSuporteAcondicionamentoController;
		private DomainValueListBoxController EstadoConservacaoController;
		private DomainValueListBoxController MaterialSuporteController;

		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			CurrentFRDBase = (GISADataset.FRDBaseRow)CurrentDataRow;

			grpCaracteristicasFisicas.Update();

			FRDRule.Current.LoadCondicoesAcessoData(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, conn);

			IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = true;
			byte[] Versao = null;
			string QueryFilter = "IDFRDBase=" + CurrentFRDBase.ID.ToString();
			if (GisaDataSetHelper.GetInstance().SFRDCondicaoDeAcesso. Select(QueryFilter).Length != 0)
			{

				CurrentCondicaoDeAcesso = (GISADataset.SFRDCondicaoDeAcessoRow)(GisaDataSetHelper.GetInstance(). SFRDCondicaoDeAcesso.Select(QueryFilter)[0]);
			}
			else
			{
				CurrentCondicaoDeAcesso = GisaDataSetHelper.GetInstance().SFRDCondicaoDeAcesso. AddSFRDCondicaoDeAcessoRow(CurrentFRDBase, "", "", "", "", Versao, 0);
			}

			TecnicaRegistoController = new DomainValueListBoxController(CurrentFRDBase, GisaDataSetHelper.GetInstance().TipoTecnicasDeRegisto, GisaDataSetHelper.GetInstance().SFRDTecnicasDeRegisto, "IDTipoTecnicasDeRegisto", chkLstTecnicaRegisto);
			TecnicaRegistoController.ModelToView();

			FormaSuporteAcondicionamentoController = new DomainValueListBoxController(CurrentFRDBase, GisaDataSetHelper.GetInstance().TipoFormaSuporteAcond, GisaDataSetHelper.GetInstance().SFRDFormaSuporteAcond, "IDTipoFormaSuporteAcond", chkLstFormaSuporteAcondicionamento);
			FormaSuporteAcondicionamentoController.ModelToView();

			EstadoConservacaoController = new DomainValueListBoxController(CurrentFRDBase, GisaDataSetHelper.GetInstance().TipoEstadoDeConservacao, GisaDataSetHelper.GetInstance().SFRDEstadoDeConservacao, "IDTipoEstadoDeConservacao", chkLstEstadoConservacao, true);
			EstadoConservacaoController.ModelToView();

			MaterialSuporteController = new DomainValueListBoxController(CurrentFRDBase, GisaDataSetHelper.GetInstance().TipoMaterialDeSuporte, GisaDataSetHelper.GetInstance().SFRDMaterialDeSuporte, "IDTipoMaterialDeSuporte", chkLstMaterialSuporte);
			MaterialSuporteController.ModelToView();

			if (! CurrentCondicaoDeAcesso.IsAuxiliarDePesquisaNull())
			{
				txtAuxiliaresPesquisa.Text = CurrentCondicaoDeAcesso.AuxiliarDePesquisa;
			}
			else
			{
				txtAuxiliaresPesquisa.Text = "";
			}
			IsPopulated = true;
		}

		public override void ViewToModel()
		{
			if (CurrentFRDBase == null || CurrentFRDBase.RowState == DataRowState.Detached || ! IsLoaded)
			{
				return;
			}

			//Actualizar as modificações no modelo
			TecnicaRegistoController.ViewToModel(CurrentFRDBase);
			FormaSuporteAcondicionamentoController.ViewToModel(CurrentFRDBase);
			EstadoConservacaoController.ViewToModel(CurrentFRDBase);
			MaterialSuporteController.ViewToModel(CurrentFRDBase);
			CurrentCondicaoDeAcesso.AuxiliarDePesquisa = txtAuxiliaresPesquisa.Text;
		}

		public override void Deactivate()
		{
            GUIHelper.GUIHelper.clearField(chkLstEstadoConservacao);
            GUIHelper.GUIHelper.clearField(chkLstFormaSuporteAcondicionamento);
            GUIHelper.GUIHelper.clearField(chkLstMaterialSuporte);
            GUIHelper.GUIHelper.clearField(chkLstTecnicaRegisto);
            GUIHelper.GUIHelper.clearField(txtAuxiliaresPesquisa);
		}
	}

} //end of root namespace