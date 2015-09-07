using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using System.Collections.Generic;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA
{
	public class PanelUFControloDescricao : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelUFControloDescricao() : base()
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
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.ControloRevisoes1 = new GISA.ControloRevisoes();
            this.grpDescricoesAnteriores = new System.Windows.Forms.GroupBox();
            this.lstVwDataDescricao = new System.Windows.Forms.ListView();
            this.colData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDataRecolha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOperador = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colArquivista = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colImportado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GroupBox3.SuspendLayout();
            this.grpDescricoesAnteriores.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox3.Controls.Add(this.ControloRevisoes1);
            this.GroupBox3.Controls.Add(this.grpDescricoesAnteriores);
            this.GroupBox3.Location = new System.Drawing.Point(6, 4);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(788, 590);
            this.GroupBox3.TabIndex = 1;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Data de criação e/ou de revisão";
            // 
            // ControloRevisoes1
            // 
            this.ControloRevisoes1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ControloRevisoes1.Location = new System.Drawing.Point(8, 16);
            this.ControloRevisoes1.Name = "ControloRevisoes1";
            this.ControloRevisoes1.Size = new System.Drawing.Size(772, 44);
            this.ControloRevisoes1.TabIndex = 4;
            // 
            // grpDescricoesAnteriores
            // 
            this.grpDescricoesAnteriores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDescricoesAnteriores.Controls.Add(this.lstVwDataDescricao);
            this.grpDescricoesAnteriores.Location = new System.Drawing.Point(8, 64);
            this.grpDescricoesAnteriores.Name = "grpDescricoesAnteriores";
            this.grpDescricoesAnteriores.Size = new System.Drawing.Size(772, 518);
            this.grpDescricoesAnteriores.TabIndex = 2;
            this.grpDescricoesAnteriores.TabStop = false;
            this.grpDescricoesAnteriores.Text = "Registos anteriores";
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
            this.lstVwDataDescricao.Size = new System.Drawing.Size(756, 494);
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
            // PanelUFControloDescricao
            // 
            this.Controls.Add(this.GroupBox3);
            this.Name = "PanelUFControloDescricao";
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

			UFRule.Current.LoadUFControloDescricaoData(GisaDataSetHelper.GetInstance(), new GISADataset(), CurrentFRDBase.ID, conn);

			IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = false;

			ControloRevisoes1.ControloAutores1.LoadAndPopulateAuthors();
			if (SessionHelper.GetGisaPrincipal().TrusteeUserAuthor != null)
			{
				ControloRevisoes1.ControloAutores1.SelectedAutor = SessionHelper.GetGisaPrincipal().TrusteeUserAuthor.TrusteeRow;
			}

			GISADataset.FRDBaseDataDeDescricaoRow dddRow = null;

			lstVwDataDescricao.Items.Clear();
			ListViewItem item = null;
			foreach (DataRowView dddRowView in new DataView(GisaDataSetHelper.GetInstance(). FRDBaseDataDeDescricao, "IDFRDBase=" + CurrentFRDBase.ID.ToString(), "DataEdicao DESC", DataViewRowState.CurrentRows))
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
			LastFRDBaseDataDeDescricaoRows = (GISADataset.FRDBaseDataDeDescricaoRow[])(GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao.Select("IDFRDBase=" + CurrentFRDBase.ID.ToString(), "DataEdicao DESC"));
			if (LastFRDBaseDataDeDescricaoRows.Length > 0)
			{
				ControloRevisoes1.dtpRecolha.Value = LastFRDBaseDataDeDescricaoRows[0].DataAutoria;
			}
			else
			{
				ControloRevisoes1.dtpRecolha.Value = System.DateTime.Now;
			}

			IsPopulated = true;
		}

		public override void ViewToModel()
		{

		}

		public override void Deactivate()
		{
			CurrentFRDBase = null;
		}
	}

} //end of root namespace