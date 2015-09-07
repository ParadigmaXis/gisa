using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using System.Collections.Generic;
using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA
{
	public class PanelOIControloDescricao : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelOIControloDescricao() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
			if (! (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().TipoServer.ID == Convert.ToInt64(TipoServer.ClienteServidor)))
			{
				lstVwDataDescricao.Columns.Remove(colOperador);
			}
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
		internal System.Windows.Forms.GroupBox grpRegrasConvencoes;
		internal System.Windows.Forms.TextBox txtRegrasConvencoes;
		internal System.Windows.Forms.GroupBox grpNotaArquivista;
		internal System.Windows.Forms.TextBox txtNotasArquivista;
		internal System.Windows.Forms.GroupBox GroupBox3;
		//    Friend WithEvents grpComentarios As System.Windows.Forms.GroupBox
		//    Friend WithEvents txtComentarios As System.Windows.Forms.TextBox
		internal System.Windows.Forms.ListView lstVwDataDescricao;
		internal System.Windows.Forms.ColumnHeader colOperador;
		internal System.Windows.Forms.ColumnHeader colArquivista;
		internal System.Windows.Forms.ColumnHeader colData;
		internal System.Windows.Forms.ColumnHeader colDataRecolha;
		internal System.Windows.Forms.GroupBox grpDescricoesAnteriores;
        private ColumnHeader colImportado;
		internal GISA.ControloRevisoes ControloRevisoes1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.grpRegrasConvencoes = new System.Windows.Forms.GroupBox();
            this.txtRegrasConvencoes = new System.Windows.Forms.TextBox();
            this.grpNotaArquivista = new System.Windows.Forms.GroupBox();
            this.txtNotasArquivista = new System.Windows.Forms.TextBox();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.ControloRevisoes1 = new GISA.ControloRevisoes();
            this.grpDescricoesAnteriores = new System.Windows.Forms.GroupBox();
            this.lstVwDataDescricao = new System.Windows.Forms.ListView();
            this.colData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDataRecolha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOperador = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colArquivista = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colImportado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpRegrasConvencoes.SuspendLayout();
            this.grpNotaArquivista.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.grpDescricoesAnteriores.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpRegrasConvencoes
            // 
            this.grpRegrasConvencoes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRegrasConvencoes.Controls.Add(this.txtRegrasConvencoes);
            this.grpRegrasConvencoes.Location = new System.Drawing.Point(3, 101);
            this.grpRegrasConvencoes.Name = "grpRegrasConvencoes";
            this.grpRegrasConvencoes.Size = new System.Drawing.Size(794, 282);
            this.grpRegrasConvencoes.TabIndex = 2;
            this.grpRegrasConvencoes.TabStop = false;
            this.grpRegrasConvencoes.Text = "7.2. Regras ou convenções";
            // 
            // txtRegrasConvencoes
            // 
            this.txtRegrasConvencoes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegrasConvencoes.Location = new System.Drawing.Point(8, 16);
            this.txtRegrasConvencoes.Multiline = true;
            this.txtRegrasConvencoes.Name = "txtRegrasConvencoes";
            this.txtRegrasConvencoes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRegrasConvencoes.Size = new System.Drawing.Size(778, 259);
            this.txtRegrasConvencoes.TabIndex = 1;
            // 
            // grpNotaArquivista
            // 
            this.grpNotaArquivista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpNotaArquivista.Controls.Add(this.txtNotasArquivista);
            this.grpNotaArquivista.Location = new System.Drawing.Point(3, 3);
            this.grpNotaArquivista.Name = "grpNotaArquivista";
            this.grpNotaArquivista.Size = new System.Drawing.Size(794, 96);
            this.grpNotaArquivista.TabIndex = 1;
            this.grpNotaArquivista.TabStop = false;
            this.grpNotaArquivista.Text = "7.1. Nota do arquivista";
            // 
            // txtNotasArquivista
            // 
            this.txtNotasArquivista.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotasArquivista.Location = new System.Drawing.Point(8, 16);
            this.txtNotasArquivista.Multiline = true;
            this.txtNotasArquivista.Name = "txtNotasArquivista";
            this.txtNotasArquivista.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotasArquivista.Size = new System.Drawing.Size(778, 73);
            this.txtNotasArquivista.TabIndex = 1;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox3.Controls.Add(this.ControloRevisoes1);
            this.GroupBox3.Controls.Add(this.grpDescricoesAnteriores);
            this.GroupBox3.Location = new System.Drawing.Point(3, 389);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(794, 208);
            this.GroupBox3.TabIndex = 3;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "7.3. Data(s) de descrição";
            // 
            // ControloRevisoes1
            // 
            this.ControloRevisoes1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ControloRevisoes1.Location = new System.Drawing.Point(8, 16);
            this.ControloRevisoes1.Name = "ControloRevisoes1";
            this.ControloRevisoes1.Size = new System.Drawing.Size(778, 44);
            this.ControloRevisoes1.TabIndex = 15;
            // 
            // grpDescricoesAnteriores
            // 
            this.grpDescricoesAnteriores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDescricoesAnteriores.Controls.Add(this.lstVwDataDescricao);
            this.grpDescricoesAnteriores.Location = new System.Drawing.Point(8, 64);
            this.grpDescricoesAnteriores.Name = "grpDescricoesAnteriores";
            this.grpDescricoesAnteriores.Size = new System.Drawing.Size(778, 136);
            this.grpDescricoesAnteriores.TabIndex = 4;
            this.grpDescricoesAnteriores.TabStop = false;
            this.grpDescricoesAnteriores.Text = "Descrições anteriores";
            // 
            // lstVwDataDescricao
            // 
            this.lstVwDataDescricao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwDataDescricao.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colData,
            this.colDataRecolha,
            this.colOperador,
            this.colArquivista,
            this.colImportado});
            this.lstVwDataDescricao.FullRowSelect = true;
            this.lstVwDataDescricao.Location = new System.Drawing.Point(8, 16);
            this.lstVwDataDescricao.Name = "lstVwDataDescricao";
            this.lstVwDataDescricao.Size = new System.Drawing.Size(762, 112);
            this.lstVwDataDescricao.TabIndex = 1;
            this.lstVwDataDescricao.UseCompatibleStateImageBehavior = false;
            this.lstVwDataDescricao.View = System.Windows.Forms.View.Details;
            // 
            // colData
            // 
            this.colData.Text = "Data registo";
            this.colData.Width = 118;
            // 
            // colDataRecolha
            // 
            this.colDataRecolha.Text = "Data descrição";
            this.colDataRecolha.Width = 118;
            // 
            // colOperador
            // 
            this.colOperador.Text = "Operador";
            this.colOperador.Width = 210;
            // 
            // colArquivista
            // 
            this.colArquivista.Text = "Autor da descrição";
            this.colArquivista.Width = 210;
            // 
            // colImportado
            // 
            this.colImportado.Text = "Importado";
            // 
            // PanelOIControloDescricao
            // 
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.grpNotaArquivista);
            this.Controls.Add(this.grpRegrasConvencoes);
            this.Name = "PanelOIControloDescricao";
            this.grpRegrasConvencoes.ResumeLayout(false);
            this.grpRegrasConvencoes.PerformLayout();
            this.grpNotaArquivista.ResumeLayout(false);
            this.grpNotaArquivista.PerformLayout();
            this.GroupBox3.ResumeLayout(false);
            this.grpDescricoesAnteriores.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

		private GISADataset.FRDBaseRow CurrentFRDBase;

		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			CurrentFRDBase = (GISADataset.FRDBaseRow)CurrentDataRow;

			FRDRule.Current.LoadOIControloDescricaoData(GisaDataSetHelper.GetInstance(), new GISADataset(), CurrentFRDBase.ID, conn);

			IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = false;

			ControloRevisoes1.ControloAutores1.LoadAndPopulateAuthors();
			if (SessionHelper.GetGisaPrincipal().TrusteeUserAuthor != null)
				ControloRevisoes1.ControloAutores1.SelectedAutor = SessionHelper.GetGisaPrincipal().TrusteeUserAuthor.TrusteeRow;

			string QueryFilter = "IDFRDBase=" + CurrentFRDBase.ID.ToString();
			GISADataset.FRDBaseDataDeDescricaoRow dddRow = null;

			lstVwDataDescricao.Items.Clear();
			ListViewItem item = null;
			foreach (DataRowView dddRowView in new DataView(GisaDataSetHelper.GetInstance(). FRDBaseDataDeDescricao, QueryFilter, "DataEdicao DESC", DataViewRowState.CurrentRows))
			{
				dddRow = (GISADataset.FRDBaseDataDeDescricaoRow)dddRowView.Row;
				item = lstVwDataDescricao.Items.Add("");
				item.SubItems.AddRange(new string[] {"", "", "", ""});
                item.SubItems[colData.Index].Text = dddRow.DataEdicao.ToString();
				item.SubItems[colDataRecolha.Index].Text = string.Format("{0:yyyy}-{0:MM}-{0:dd}", dddRow.DataAutoria);
				if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().TipoServer.ID == Convert.ToInt64(TipoServer.ClienteServidor))
					item.SubItems[colOperador.Index].Text = dddRow. TrusteeUserRowByTrusteeUserFRDBaseDataDeDescricao.TrusteeRow.Name;
				if (! dddRow.IsIDTrusteeAuthorityNull())
					item.SubItems[colArquivista.Index].Text = dddRow.TrusteeUserRowByTrusteeUserFRDBaseDataDeDescricaoAuthority. TrusteeRow.Name;
                item.SubItems[colImportado.Index].Text = dddRow.Importacao ? "Sim" : "Não";
			}

			GISADataset.FRDBaseDataDeDescricaoRow[] LastFRDBaseDataDeDescricaoRows = null;
			LastFRDBaseDataDeDescricaoRows = (GISADataset.FRDBaseDataDeDescricaoRow[])(GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao.Select(QueryFilter, "DataEdicao DESC"));
			if (LastFRDBaseDataDeDescricaoRows.Length > 0)
				ControloRevisoes1.dtpRecolha.Value = LastFRDBaseDataDeDescricaoRows[0].DataAutoria;
			else
				ControloRevisoes1.dtpRecolha.Value = System.DateTime.Now;

			if (! (CurrentFRDBase.IsRegrasOuConvencoesNull()))
				txtRegrasConvencoes.Text = CurrentFRDBase.RegrasOuConvencoes;
			else
				txtRegrasConvencoes.Text = "";

			if (! (CurrentFRDBase.IsNotaDoArquivistaNull()))
				txtNotasArquivista.Text = CurrentFRDBase.NotaDoArquivista;
			else
				txtNotasArquivista.Text = "";

			IsPopulated = true;
		}

		public override void ViewToModel()
		{
			if (CurrentFRDBase == null || CurrentFRDBase.RowState == DataRowState.Detached || ! IsLoaded)
				return;

			CurrentFRDBase.RegrasOuConvencoes = txtRegrasConvencoes.Text;
			CurrentFRDBase.NotaDoArquivista = txtNotasArquivista.Text;
		}

		public override void Deactivate()
		{
            GUIHelper.GUIHelper.clearField(txtRegrasConvencoes);
            GUIHelper.GUIHelper.clearField(txtNotasArquivista);
			CurrentFRDBase = null;
		}
	}
}