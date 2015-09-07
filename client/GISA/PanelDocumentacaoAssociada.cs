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
	public class PanelDocumentacaoAssociada : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelDocumentacaoAssociada() : base()
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
		internal System.Windows.Forms.GroupBox grpNotaPublicacao;
		internal System.Windows.Forms.TextBox txtNotaPublicacao;
		internal System.Windows.Forms.GroupBox grpUnidadeDescRelacionadas;
		internal System.Windows.Forms.TextBox txtUnidadesDescricaoRelacionadas;
		internal System.Windows.Forms.GroupBox grpExistLocalCopias;
		internal System.Windows.Forms.TextBox txtExistenciaLocalizacaoCopias;
		internal System.Windows.Forms.GroupBox grpExistLocalOrig;
		internal System.Windows.Forms.TextBox txtExistenciaLocalizacaoOriginais;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.grpNotaPublicacao = new System.Windows.Forms.GroupBox();
            this.txtNotaPublicacao = new System.Windows.Forms.TextBox();
            this.grpUnidadeDescRelacionadas = new System.Windows.Forms.GroupBox();
            this.txtUnidadesDescricaoRelacionadas = new System.Windows.Forms.TextBox();
            this.grpExistLocalCopias = new System.Windows.Forms.GroupBox();
            this.txtExistenciaLocalizacaoCopias = new System.Windows.Forms.TextBox();
            this.grpExistLocalOrig = new System.Windows.Forms.GroupBox();
            this.txtExistenciaLocalizacaoOriginais = new System.Windows.Forms.TextBox();
            this.grpNotaPublicacao.SuspendLayout();
            this.grpUnidadeDescRelacionadas.SuspendLayout();
            this.grpExistLocalCopias.SuspendLayout();
            this.grpExistLocalOrig.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpNotaPublicacao
            // 
            this.grpNotaPublicacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpNotaPublicacao.Controls.Add(this.txtNotaPublicacao);
            this.grpNotaPublicacao.Location = new System.Drawing.Point(3, 311);
            this.grpNotaPublicacao.Name = "grpNotaPublicacao";
            this.grpNotaPublicacao.Size = new System.Drawing.Size(794, 286);
            this.grpNotaPublicacao.TabIndex = 4;
            this.grpNotaPublicacao.TabStop = false;
            this.grpNotaPublicacao.Text = "5.4. Nota de publicação";
            // 
            // txtNotaPublicacao
            // 
            this.txtNotaPublicacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotaPublicacao.Location = new System.Drawing.Point(8, 16);
            this.txtNotaPublicacao.Multiline = true;
            this.txtNotaPublicacao.Name = "txtNotaPublicacao";
            this.txtNotaPublicacao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotaPublicacao.Size = new System.Drawing.Size(778, 263);
            this.txtNotaPublicacao.TabIndex = 1;
            // 
            // grpUnidadeDescRelacionadas
            // 
            this.grpUnidadeDescRelacionadas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpUnidadeDescRelacionadas.Controls.Add(this.txtUnidadesDescricaoRelacionadas);
            this.grpUnidadeDescRelacionadas.Location = new System.Drawing.Point(3, 205);
            this.grpUnidadeDescRelacionadas.Name = "grpUnidadeDescRelacionadas";
            this.grpUnidadeDescRelacionadas.Size = new System.Drawing.Size(793, 99);
            this.grpUnidadeDescRelacionadas.TabIndex = 3;
            this.grpUnidadeDescRelacionadas.TabStop = false;
            this.grpUnidadeDescRelacionadas.Text = "5.3. Unidades de descrição relacionadas";
            // 
            // txtUnidadesDescricaoRelacionadas
            // 
            this.txtUnidadesDescricaoRelacionadas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUnidadesDescricaoRelacionadas.Location = new System.Drawing.Point(8, 16);
            this.txtUnidadesDescricaoRelacionadas.Multiline = true;
            this.txtUnidadesDescricaoRelacionadas.Name = "txtUnidadesDescricaoRelacionadas";
            this.txtUnidadesDescricaoRelacionadas.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtUnidadesDescricaoRelacionadas.Size = new System.Drawing.Size(777, 76);
            this.txtUnidadesDescricaoRelacionadas.TabIndex = 1;
            // 
            // grpExistLocalCopias
            // 
            this.grpExistLocalCopias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpExistLocalCopias.Controls.Add(this.txtExistenciaLocalizacaoCopias);
            this.grpExistLocalCopias.Location = new System.Drawing.Point(3, 102);
            this.grpExistLocalCopias.Name = "grpExistLocalCopias";
            this.grpExistLocalCopias.Size = new System.Drawing.Size(793, 100);
            this.grpExistLocalCopias.TabIndex = 2;
            this.grpExistLocalCopias.TabStop = false;
            this.grpExistLocalCopias.Text = "5.2. Existência e localização das cópias";
            // 
            // txtExistenciaLocalizacaoCopias
            // 
            this.txtExistenciaLocalizacaoCopias.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExistenciaLocalizacaoCopias.Location = new System.Drawing.Point(8, 16);
            this.txtExistenciaLocalizacaoCopias.Multiline = true;
            this.txtExistenciaLocalizacaoCopias.Name = "txtExistenciaLocalizacaoCopias";
            this.txtExistenciaLocalizacaoCopias.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExistenciaLocalizacaoCopias.Size = new System.Drawing.Size(777, 77);
            this.txtExistenciaLocalizacaoCopias.TabIndex = 1;
            // 
            // grpExistLocalOrig
            // 
            this.grpExistLocalOrig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpExistLocalOrig.Controls.Add(this.txtExistenciaLocalizacaoOriginais);
            this.grpExistLocalOrig.Location = new System.Drawing.Point(3, 3);
            this.grpExistLocalOrig.Name = "grpExistLocalOrig";
            this.grpExistLocalOrig.Size = new System.Drawing.Size(794, 96);
            this.grpExistLocalOrig.TabIndex = 1;
            this.grpExistLocalOrig.TabStop = false;
            this.grpExistLocalOrig.Text = "5.1. Existência e localização de originais";
            // 
            // txtExistenciaLocalizacaoOriginais
            // 
            this.txtExistenciaLocalizacaoOriginais.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExistenciaLocalizacaoOriginais.Location = new System.Drawing.Point(8, 16);
            this.txtExistenciaLocalizacaoOriginais.Multiline = true;
            this.txtExistenciaLocalizacaoOriginais.Name = "txtExistenciaLocalizacaoOriginais";
            this.txtExistenciaLocalizacaoOriginais.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExistenciaLocalizacaoOriginais.Size = new System.Drawing.Size(778, 73);
            this.txtExistenciaLocalizacaoOriginais.TabIndex = 1;
            // 
            // PanelDocumentacaoAssociada
            // 
            this.Controls.Add(this.grpExistLocalOrig);
            this.Controls.Add(this.grpExistLocalCopias);
            this.Controls.Add(this.grpUnidadeDescRelacionadas);
            this.Controls.Add(this.grpNotaPublicacao);
            this.Name = "PanelDocumentacaoAssociada";
            this.grpNotaPublicacao.ResumeLayout(false);
            this.grpNotaPublicacao.PerformLayout();
            this.grpUnidadeDescRelacionadas.ResumeLayout(false);
            this.grpUnidadeDescRelacionadas.PerformLayout();
            this.grpExistLocalCopias.ResumeLayout(false);
            this.grpExistLocalCopias.PerformLayout();
            this.grpExistLocalOrig.ResumeLayout(false);
            this.grpExistLocalOrig.PerformLayout();
            this.ResumeLayout(false);

		}

	#endregion

		private GISADataset.FRDBaseRow CurrentFRDBase;
        private GISADataset.SFRDDocumentacaoAssociadaRow CurrentSFRDDocumentacaoAssociada;

		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			CurrentFRDBase = (GISADataset.FRDBaseRow)CurrentDataRow;
			FRDRule.Current.LoadDocumentacaoAssociadaData(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, conn);
			IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = false;
			byte[] Versao = null;
			string QueryFilter = "IDFRDBase=" + CurrentFRDBase.ID.ToString();
			if (GisaDataSetHelper.GetInstance().SFRDDocumentacaoAssociada.Select(QueryFilter).Length != 0)
			{
				CurrentSFRDDocumentacaoAssociada = (GISADataset.SFRDDocumentacaoAssociadaRow)(GisaDataSetHelper.GetInstance().SFRDDocumentacaoAssociada.Select(QueryFilter)[0]);
			}
			else
			{
				CurrentSFRDDocumentacaoAssociada = GisaDataSetHelper.GetInstance().SFRDDocumentacaoAssociada.AddSFRDDocumentacaoAssociadaRow(CurrentFRDBase, "", "", "", "", Versao, 0);
			}

			if (! (CurrentSFRDDocumentacaoAssociada.IsExistenciaDeOriginaisNull()))
			{
				txtExistenciaLocalizacaoOriginais.Text = CurrentSFRDDocumentacaoAssociada.ExistenciaDeOriginais;
			}
			else
			{
				txtExistenciaLocalizacaoOriginais.Text = "";
			}

			if (! (CurrentSFRDDocumentacaoAssociada.IsExistenciaDeCopiasNull()))
			{
				txtExistenciaLocalizacaoCopias.Text = CurrentSFRDDocumentacaoAssociada.ExistenciaDeCopias;
			}
			else
			{
				txtExistenciaLocalizacaoCopias.Text = "";
			}

			if (! (CurrentSFRDDocumentacaoAssociada.IsUnidadesRelacionadasNull()))
			{
				txtUnidadesDescricaoRelacionadas.Text = CurrentSFRDDocumentacaoAssociada.UnidadesRelacionadas;
			}
			else
			{
				txtUnidadesDescricaoRelacionadas.Text = "";
			}

			if (! (CurrentSFRDDocumentacaoAssociada.IsNotaDePublicacaoNull()))
			{
				txtNotaPublicacao.Text = CurrentSFRDDocumentacaoAssociada.NotaDePublicacao;
			}
			else
			{
				txtNotaPublicacao.Text = "";
			}
			IsPopulated = true;
		}

		public override void ViewToModel()
		{
			if (CurrentSFRDDocumentacaoAssociada == null || ! IsLoaded)
			{
				return;
			}

			CurrentSFRDDocumentacaoAssociada.ExistenciaDeOriginais = txtExistenciaLocalizacaoOriginais.Text;
			CurrentSFRDDocumentacaoAssociada.ExistenciaDeCopias = txtExistenciaLocalizacaoCopias.Text;
			CurrentSFRDDocumentacaoAssociada.UnidadesRelacionadas = txtUnidadesDescricaoRelacionadas.Text;
			CurrentSFRDDocumentacaoAssociada.NotaDePublicacao = txtNotaPublicacao.Text;
		}

		public override void Deactivate()
		{
            GUIHelper.GUIHelper.clearField(txtExistenciaLocalizacaoOriginais);
            GUIHelper.GUIHelper.clearField(txtExistenciaLocalizacaoCopias);
            GUIHelper.GUIHelper.clearField(txtUnidadesDescricaoRelacionadas);
            GUIHelper.GUIHelper.clearField(txtNotaPublicacao);
		}

	}

} //end of root namespace