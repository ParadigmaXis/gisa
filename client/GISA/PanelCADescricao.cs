using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA;
using GISA.Model;
using GISA.Controls;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA
{
	public class PanelCADescricao : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelCADescricao() : base()
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
		internal System.Windows.Forms.TabControl tabDescricao;
		internal System.Windows.Forms.TabPage tabDatasExistencia;
		internal System.Windows.Forms.TabPage tabHistoria;
		internal System.Windows.Forms.TabPage tabZonaGeografica;
		internal System.Windows.Forms.TabPage tabEstatutoLegal;
		internal System.Windows.Forms.TabPage tabFuncoesOcupacActividades;
		internal System.Windows.Forms.TabPage tabEnquadramentoLegal;
		internal System.Windows.Forms.TabPage tabEstruturaInterna;
		internal System.Windows.Forms.TabPage tabContextoGeral;
		internal System.Windows.Forms.TabPage tabOutraInforRelevante;
		internal System.Windows.Forms.TextBox txtDataExistencia;
		internal System.Windows.Forms.TextBox txtHistoria;
		internal System.Windows.Forms.TextBox txtZonaGeografica;
		internal System.Windows.Forms.TextBox txtEstatutoLegal;
		internal System.Windows.Forms.TextBox txtFuncoesOcupacoesActividades;
		internal System.Windows.Forms.TextBox txtOutraInformRelevante;
		internal System.Windows.Forms.TextBox txtContextoGeral;
		internal System.Windows.Forms.TextBox txtEstruturaInterna;
		internal System.Windows.Forms.TextBox txtEnquadramentoLegal;
		internal GISA.AttributableDate AttributableDateInicio;
		internal GISA.AttributableDate AttributableDateFim;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.tabDescricao = new System.Windows.Forms.TabControl();
            this.tabDatasExistencia = new System.Windows.Forms.TabPage();
            this.AttributableDateFim = new GISA.AttributableDate();
            this.AttributableDateInicio = new GISA.AttributableDate();
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
            this.tabDescricao.SuspendLayout();
            this.tabDatasExistencia.SuspendLayout();
            this.tabHistoria.SuspendLayout();
            this.tabZonaGeografica.SuspendLayout();
            this.tabEstatutoLegal.SuspendLayout();
            this.tabFuncoesOcupacActividades.SuspendLayout();
            this.tabEnquadramentoLegal.SuspendLayout();
            this.tabEstruturaInterna.SuspendLayout();
            this.tabContextoGeral.SuspendLayout();
            this.tabOutraInforRelevante.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabDescricao
            // 
            this.tabDescricao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabDescricao.Controls.Add(this.tabDatasExistencia);
            this.tabDescricao.Controls.Add(this.tabHistoria);
            this.tabDescricao.Controls.Add(this.tabZonaGeografica);
            this.tabDescricao.Controls.Add(this.tabEstatutoLegal);
            this.tabDescricao.Controls.Add(this.tabFuncoesOcupacActividades);
            this.tabDescricao.Controls.Add(this.tabEnquadramentoLegal);
            this.tabDescricao.Controls.Add(this.tabEstruturaInterna);
            this.tabDescricao.Controls.Add(this.tabContextoGeral);
            this.tabDescricao.Controls.Add(this.tabOutraInforRelevante);
            this.tabDescricao.Location = new System.Drawing.Point(3, 3);
            this.tabDescricao.Multiline = true;
            this.tabDescricao.Name = "tabDescricao";
            this.tabDescricao.SelectedIndex = 0;
            this.tabDescricao.Size = new System.Drawing.Size(794, 594);
            this.tabDescricao.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabDescricao.TabIndex = 1;
            // 
            // tabDatasExistencia
            // 
            this.tabDatasExistencia.Controls.Add(this.AttributableDateFim);
            this.tabDatasExistencia.Controls.Add(this.AttributableDateInicio);
            this.tabDatasExistencia.Controls.Add(this.txtDataExistencia);
            this.tabDatasExistencia.Location = new System.Drawing.Point(4, 40);
            this.tabDatasExistencia.Name = "tabDatasExistencia";
            this.tabDatasExistencia.Size = new System.Drawing.Size(786, 550);
            this.tabDatasExistencia.TabIndex = 0;
            this.tabDatasExistencia.Text = "2.1. Datas de existência";
            // 
            // AttributableDateFim
            // 
            this.AttributableDateFim.Location = new System.Drawing.Point(199, 7);
            this.AttributableDateFim.Name = "AttributableDateFim";
            this.AttributableDateFim.Size = new System.Drawing.Size(180, 44);
            this.AttributableDateFim.TabIndex = 3;
            this.AttributableDateFim.Title = "Data";
            // 
            // AttributableDateInicio
            // 
            this.AttributableDateInicio.Location = new System.Drawing.Point(8, 7);
            this.AttributableDateInicio.Name = "AttributableDateInicio";
            this.AttributableDateInicio.Size = new System.Drawing.Size(180, 44);
            this.AttributableDateInicio.TabIndex = 2;
            this.AttributableDateInicio.Title = "Data";
            // 
            // txtDataExistencia
            // 
            this.txtDataExistencia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataExistencia.Location = new System.Drawing.Point(0, 59);
            this.txtDataExistencia.MaxLength = 2147483646;
            this.txtDataExistencia.Multiline = true;
            this.txtDataExistencia.Name = "txtDataExistencia";
            this.txtDataExistencia.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDataExistencia.Size = new System.Drawing.Size(786, 491);
            this.txtDataExistencia.TabIndex = 4;
            // 
            // tabHistoria
            // 
            this.tabHistoria.Controls.Add(this.txtHistoria);
            this.tabHistoria.Location = new System.Drawing.Point(4, 40);
            this.tabHistoria.Name = "tabHistoria";
            this.tabHistoria.Size = new System.Drawing.Size(680, 362);
            this.tabHistoria.TabIndex = 1;
            this.tabHistoria.Text = "2.2. História";
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
            this.txtHistoria.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHistoria.Size = new System.Drawing.Size(680, 360);
            this.txtHistoria.TabIndex = 1;
            // 
            // tabZonaGeografica
            // 
            this.tabZonaGeografica.Controls.Add(this.txtZonaGeografica);
            this.tabZonaGeografica.Location = new System.Drawing.Point(4, 40);
            this.tabZonaGeografica.Name = "tabZonaGeografica";
            this.tabZonaGeografica.Size = new System.Drawing.Size(680, 362);
            this.tabZonaGeografica.TabIndex = 2;
            this.tabZonaGeografica.Text = "2.3. Zona geográfica";
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
            this.txtZonaGeografica.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtZonaGeografica.Size = new System.Drawing.Size(680, 360);
            this.txtZonaGeografica.TabIndex = 1;
            // 
            // tabEstatutoLegal
            // 
            this.tabEstatutoLegal.Controls.Add(this.txtEstatutoLegal);
            this.tabEstatutoLegal.Location = new System.Drawing.Point(4, 40);
            this.tabEstatutoLegal.Name = "tabEstatutoLegal";
            this.tabEstatutoLegal.Size = new System.Drawing.Size(680, 362);
            this.tabEstatutoLegal.TabIndex = 3;
            this.tabEstatutoLegal.Text = "2.4. Estatuto legal";
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
            this.txtEstatutoLegal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEstatutoLegal.Size = new System.Drawing.Size(680, 360);
            this.txtEstatutoLegal.TabIndex = 1;
            // 
            // tabFuncoesOcupacActividades
            // 
            this.tabFuncoesOcupacActividades.Controls.Add(this.txtFuncoesOcupacoesActividades);
            this.tabFuncoesOcupacActividades.Location = new System.Drawing.Point(4, 40);
            this.tabFuncoesOcupacActividades.Name = "tabFuncoesOcupacActividades";
            this.tabFuncoesOcupacActividades.Size = new System.Drawing.Size(680, 362);
            this.tabFuncoesOcupacActividades.TabIndex = 4;
            this.tabFuncoesOcupacActividades.Text = "2.5. Funções, ocupações e atividades";
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
            this.txtFuncoesOcupacoesActividades.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFuncoesOcupacoesActividades.Size = new System.Drawing.Size(680, 360);
            this.txtFuncoesOcupacoesActividades.TabIndex = 1;
            // 
            // tabEnquadramentoLegal
            // 
            this.tabEnquadramentoLegal.Controls.Add(this.txtEnquadramentoLegal);
            this.tabEnquadramentoLegal.Location = new System.Drawing.Point(4, 40);
            this.tabEnquadramentoLegal.Name = "tabEnquadramentoLegal";
            this.tabEnquadramentoLegal.Size = new System.Drawing.Size(680, 362);
            this.tabEnquadramentoLegal.TabIndex = 5;
            this.tabEnquadramentoLegal.Text = "2.6. Enquadramento legal";
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
            this.txtEnquadramentoLegal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEnquadramentoLegal.Size = new System.Drawing.Size(680, 360);
            this.txtEnquadramentoLegal.TabIndex = 1;
            // 
            // tabEstruturaInterna
            // 
            this.tabEstruturaInterna.Controls.Add(this.txtEstruturaInterna);
            this.tabEstruturaInterna.Location = new System.Drawing.Point(4, 40);
            this.tabEstruturaInterna.Name = "tabEstruturaInterna";
            this.tabEstruturaInterna.Size = new System.Drawing.Size(680, 362);
            this.tabEstruturaInterna.TabIndex = 6;
            this.tabEstruturaInterna.Text = "2.7. Estrutura interna";
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
            this.txtEstruturaInterna.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEstruturaInterna.Size = new System.Drawing.Size(680, 360);
            this.txtEstruturaInterna.TabIndex = 1;
            // 
            // tabContextoGeral
            // 
            this.tabContextoGeral.Controls.Add(this.txtContextoGeral);
            this.tabContextoGeral.Location = new System.Drawing.Point(4, 40);
            this.tabContextoGeral.Name = "tabContextoGeral";
            this.tabContextoGeral.Size = new System.Drawing.Size(680, 362);
            this.tabContextoGeral.TabIndex = 7;
            this.tabContextoGeral.Text = "2.8. Contexto geral";
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
            this.txtContextoGeral.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContextoGeral.Size = new System.Drawing.Size(680, 360);
            this.txtContextoGeral.TabIndex = 1;
            // 
            // tabOutraInforRelevante
            // 
            this.tabOutraInforRelevante.Controls.Add(this.txtOutraInformRelevante);
            this.tabOutraInforRelevante.Location = new System.Drawing.Point(4, 40);
            this.tabOutraInforRelevante.Name = "tabOutraInforRelevante";
            this.tabOutraInforRelevante.Size = new System.Drawing.Size(680, 362);
            this.tabOutraInforRelevante.TabIndex = 8;
            this.tabOutraInforRelevante.Text = "2.9. Outra informação relevante";
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
            this.txtOutraInformRelevante.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutraInformRelevante.Size = new System.Drawing.Size(680, 360);
            this.txtOutraInformRelevante.TabIndex = 1;
            // 
            // PanelCADescricao
            // 
            this.Controls.Add(this.tabDescricao);
            this.Name = "PanelCADescricao";
            this.tabDescricao.ResumeLayout(false);
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
            this.ResumeLayout(false);

		}

	#endregion

		private GISADataset.ControloAutRow CurrentControloAut = null;
		private GISADataset.ControloAutDatasExistenciaRow CurrentCadeRow;
		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			byte[] Versao = null;
			CurrentControloAut = (GISADataset.ControloAutRow)CurrentDataRow;
			if (CurrentControloAut == null)
			{
				return;
			}

			ControloAutRule.Current.LoadDataPanelCADescricao(GisaDataSetHelper.GetInstance(), CurrentControloAut.ID, conn);

			GISADataset.ControloAutDatasExistenciaRow[] cadeRows = null;
			cadeRows = (GISADataset.ControloAutDatasExistenciaRow[])(GisaDataSetHelper.GetInstance().ControloAutDatasExistencia.Select(string.Format("IDControloAut = {0}", CurrentControloAut.ID)));

			if (cadeRows.Length > 0)
			{
				CurrentCadeRow = cadeRows[0];
			}
			else
			{
				CurrentCadeRow = GisaDataSetHelper.GetInstance().ControloAutDatasExistencia.AddControloAutDatasExistenciaRow(CurrentControloAut, "", "", "", "", false, "", "", "", false, Versao, 0);
			}

			IsLoaded = true;
		}

		public override void Deactivate()
		{
			AttributableDateInicio.dtData.DataBindings.Clear();
			AttributableDateFim.dtData.DataBindings.Clear();
			AttributableDateInicio.chkAtribuida.DataBindings.Clear();
			AttributableDateFim.chkAtribuida.DataBindings.Clear();
            GUIHelper.GUIHelper.clearField(txtDataExistencia);
            GUIHelper.GUIHelper.clearField(txtHistoria);
            GUIHelper.GUIHelper.clearField(txtZonaGeografica);
            GUIHelper.GUIHelper.clearField(txtEstatutoLegal);
            GUIHelper.GUIHelper.clearField(txtFuncoesOcupacoesActividades);
            GUIHelper.GUIHelper.clearField(txtEnquadramentoLegal);
            GUIHelper.GUIHelper.clearField(txtEstruturaInterna);
            GUIHelper.GUIHelper.clearField(txtContextoGeral);
            GUIHelper.GUIHelper.clearField(txtOutraInformRelevante);
		}

		public override void ModelToView()
		{
			IsPopulated = true;
			AttributableDateInicio.dtData.ValueYear = GisaDataSetHelper.GetDBNullableText(CurrentCadeRow, "InicioAno");
			AttributableDateInicio.dtData.ValueMonth = GisaDataSetHelper.GetDBNullableText(CurrentCadeRow, "InicioMes");
			AttributableDateInicio.dtData.ValueDay = GisaDataSetHelper.GetDBNullableText(CurrentCadeRow, "InicioDia");
			AttributableDateFim.dtData.ValueYear = GisaDataSetHelper.GetDBNullableText(CurrentCadeRow, "FimAno");
			AttributableDateFim.dtData.ValueMonth = GisaDataSetHelper.GetDBNullableText(CurrentCadeRow, "FimMes");
			AttributableDateFim.dtData.ValueDay = GisaDataSetHelper.GetDBNullableText(CurrentCadeRow, "FimDia");

			if (! (CurrentCadeRow.IsDescDatasExistenciaNull()))
			{
				txtDataExistencia.Text = CurrentCadeRow.DescDatasExistencia;
			}
			else
			{
				txtDataExistencia.Text = "";
			}

			AttributableDateInicio.chkAtribuida.Checked = CurrentCadeRow.InicioAtribuida;
			AttributableDateFim.chkAtribuida.Checked = CurrentCadeRow.FimAtribuida;

			if (! (CurrentControloAut.IsDescHistoriaNull()))
			{
				txtHistoria.Text = CurrentControloAut.DescHistoria;
			}
			else
			{
				txtHistoria.Text = "";
			}

			if (! (CurrentControloAut.IsDescZonaGeograficaNull()))
			{
				txtZonaGeografica.Text = CurrentControloAut.DescZonaGeografica;
			}
			else
			{
				txtZonaGeografica.Text = "";
			}

			if (! (CurrentControloAut.IsDescEstatutoLegalNull()))
			{
				txtEstatutoLegal.Text = CurrentControloAut.DescEstatutoLegal;
			}
			else
			{
				txtEstatutoLegal.Text = "";
			}

			if (! (CurrentControloAut.IsDescOcupacoesActividadesNull()))
			{
				txtFuncoesOcupacoesActividades.Text = CurrentControloAut.DescOcupacoesActividades;
			}
			else
			{
				txtFuncoesOcupacoesActividades.Text = "";
			}

			if (! (CurrentControloAut.IsDescEnquadramentoLegalNull()))
			{
				txtEnquadramentoLegal.Text = CurrentControloAut.DescEnquadramentoLegal;
			}
			else
			{
				txtEnquadramentoLegal.Text = "";
			}

			if (! (CurrentControloAut.IsDescEstruturaInternaNull()))
			{
				txtEstruturaInterna.Text = CurrentControloAut.DescEstruturaInterna;
			}
			else
			{
				txtEstruturaInterna.Text = "";
			}

			if (! (CurrentControloAut.IsDescContextoGeralNull()))
			{
				txtContextoGeral.Text = CurrentControloAut.DescContextoGeral;
			}
			else
			{
				txtContextoGeral.Text = "";
			}

			if (! (CurrentControloAut.IsDescOutraInformacaoRelevanteNull()))
			{
				txtOutraInformRelevante.Text = CurrentControloAut.DescOutraInformacaoRelevante;
			}
			else
			{
				txtOutraInformRelevante.Text = "";
			}
			IsPopulated = true;
		}

		public override void ViewToModel()
		{
			if (CurrentControloAut == null || CurrentControloAut.RowState == DataRowState.Detached || ! IsLoaded)
			{
				return;
			}

			CurrentCadeRow.DescDatasExistencia = txtDataExistencia.Text;
			CurrentControloAut.DescHistoria = txtHistoria.Text;
			CurrentControloAut.DescZonaGeografica = txtZonaGeografica.Text;
			CurrentControloAut.DescEstatutoLegal = txtEstatutoLegal.Text;
			CurrentControloAut.DescOcupacoesActividades = txtFuncoesOcupacoesActividades.Text;
			CurrentControloAut.DescEnquadramentoLegal = txtEnquadramentoLegal.Text;
			CurrentControloAut.DescEstruturaInterna = txtEstruturaInterna.Text;
			CurrentControloAut.DescContextoGeral = txtContextoGeral.Text;
			CurrentControloAut.DescOutraInformacaoRelevante = txtOutraInformRelevante.Text;

			CurrentCadeRow.InicioAtribuida = AttributableDateInicio.chkAtribuida.Checked;
			CurrentCadeRow.FimAtribuida = AttributableDateFim.chkAtribuida.Checked;

			if (AttributableDateInicio.dtData.ValueYear != GisaDataSetHelper.GetDBNullableText(CurrentCadeRow, "InicioAno") || AttributableDateInicio.dtData.ValueMonth != GisaDataSetHelper.GetDBNullableText(CurrentCadeRow, "InicioMes") || AttributableDateInicio.dtData.ValueDay != GisaDataSetHelper.GetDBNullableText(CurrentCadeRow, "InicioDia"))
			{

				CurrentCadeRow.InicioAno = AttributableDateInicio.dtData.ValueYear;
				CurrentCadeRow.InicioMes = AttributableDateInicio.dtData.ValueMonth;
				CurrentCadeRow.InicioDia = AttributableDateInicio.dtData.ValueDay;
			}

			if (AttributableDateFim.dtData.ValueYear != GisaDataSetHelper.GetDBNullableText(CurrentCadeRow, "FimAno") || AttributableDateFim.dtData.ValueMonth != GisaDataSetHelper.GetDBNullableText(CurrentCadeRow, "FimMes") || AttributableDateFim.dtData.ValueDay != GisaDataSetHelper.GetDBNullableText(CurrentCadeRow, "FimDia"))
			{

				CurrentCadeRow.FimAno = AttributableDateFim.dtData.ValueYear;
				CurrentCadeRow.FimMes = AttributableDateFim.dtData.ValueMonth;
				CurrentCadeRow.FimDia = AttributableDateFim.dtData.ValueDay;
			}

		}
	}

} //end of root namespace