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
	public class PanelIncorporacoes : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelIncorporacoes() : base()
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
		internal System.Windows.Forms.GroupBox grpIncorporacoes;
		internal System.Windows.Forms.TextBox txtIncorporacoes;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.grpIncorporacoes = new System.Windows.Forms.GroupBox();
            this.txtIncorporacoes = new System.Windows.Forms.TextBox();
            this.grpIncorporacoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpIncorporacoes
            // 
            this.grpIncorporacoes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpIncorporacoes.Controls.Add(this.txtIncorporacoes);
            this.grpIncorporacoes.Location = new System.Drawing.Point(3, 3);
            this.grpIncorporacoes.Name = "grpIncorporacoes";
            this.grpIncorporacoes.Size = new System.Drawing.Size(794, 594);
            this.grpIncorporacoes.TabIndex = 0;
            this.grpIncorporacoes.TabStop = false;
            this.grpIncorporacoes.Text = "3.3. Incorporações";
            // 
            // txtIncorporacoes
            // 
            this.txtIncorporacoes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIncorporacoes.Location = new System.Drawing.Point(8, 16);
            this.txtIncorporacoes.Multiline = true;
            this.txtIncorporacoes.Name = "txtIncorporacoes";
            this.txtIncorporacoes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIncorporacoes.Size = new System.Drawing.Size(778, 572);
            this.txtIncorporacoes.TabIndex = 0;
            // 
            // PanelIncorporacoes
            // 
            this.Controls.Add(this.grpIncorporacoes);
            this.Name = "PanelIncorporacoes";
            this.grpIncorporacoes.ResumeLayout(false);
            this.grpIncorporacoes.PerformLayout();
            this.ResumeLayout(false);

		}

	#endregion

		private GISADataset.FRDBaseRow CurrentFRDBase;
        private GISADataset.SFRDConteudoEEstruturaRow CurrentSFRDConteudoEEstrutura;

		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			CurrentFRDBase = (GISADataset.FRDBaseRow)CurrentDataRow;

			FRDRule.Current.LoadIncorporacoesData(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, conn);

			IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = false;
			byte[] Versao = null;
			string QueryFilter = "IDFRDBase=" + CurrentFRDBase.ID.ToString();
			if (GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura. Select(QueryFilter).Length != 0)
			{

				CurrentSFRDConteudoEEstrutura = (GISADataset.SFRDConteudoEEstruturaRow)(GisaDataSetHelper.GetInstance(). SFRDConteudoEEstrutura.Select(QueryFilter)[0]);
			}
			else
			{
				CurrentSFRDConteudoEEstrutura = GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura. AddSFRDConteudoEEstruturaRow(CurrentFRDBase, "", "", Versao, 0);
			}

			if (! (CurrentSFRDConteudoEEstrutura.IsIncorporacaoNull()))
			{
				txtIncorporacoes.Text = CurrentSFRDConteudoEEstrutura.Incorporacao;
			}
			else
			{
				txtIncorporacoes.Text = "";
			}
			IsPopulated = true;
		}

		public override void ViewToModel()
		{
			if (CurrentSFRDConteudoEEstrutura == null || ! IsLoaded)
			{
				return;
			}

			CurrentSFRDConteudoEEstrutura.Incorporacao = txtIncorporacoes.Text;
		}

		public override void Deactivate()
		{
            GUIHelper.GUIHelper.clearField(txtIncorporacoes);
		}

	}
} //end of root namespace