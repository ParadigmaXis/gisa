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
	public class PanelUFConteudoEstrutura : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelUFConteudoEstrutura() : base()
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
		internal System.Windows.Forms.GroupBox grpConteudoEstrutura;
		internal System.Windows.Forms.TextBox txtConteudoInformacional;
		internal System.Windows.Forms.GroupBox grpObservacoes;
		internal System.Windows.Forms.TextBox txtObservacoes;
		internal System.Windows.Forms.GroupBox GroupBox1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.grpConteudoEstrutura = new System.Windows.Forms.GroupBox();
			this.txtConteudoInformacional = new System.Windows.Forms.TextBox();
			this.grpObservacoes = new System.Windows.Forms.GroupBox();
			this.txtObservacoes = new System.Windows.Forms.TextBox();
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.grpConteudoEstrutura.SuspendLayout();
			this.grpObservacoes.SuspendLayout();
			this.GroupBox1.SuspendLayout();
			this.SuspendLayout();
			//
			//grpConteudoEstrutura
			//
			this.grpConteudoEstrutura.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.grpConteudoEstrutura.Controls.Add(this.txtConteudoInformacional);
			this.grpConteudoEstrutura.Location = new System.Drawing.Point(8, 16);
			this.grpConteudoEstrutura.Name = "grpConteudoEstrutura";
			this.grpConteudoEstrutura.Size = new System.Drawing.Size(664, 224);
			this.grpConteudoEstrutura.TabIndex = 11;
			this.grpConteudoEstrutura.TabStop = false;
			this.grpConteudoEstrutura.Text = "Conteúdo informacional";
			//
			//txtConteudoInformacional
			//
			this.txtConteudoInformacional.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.txtConteudoInformacional.Location = new System.Drawing.Point(8, 16);
			this.txtConteudoInformacional.Multiline = true;
			this.txtConteudoInformacional.Name = "txtConteudoInformacional";
			this.txtConteudoInformacional.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtConteudoInformacional.Size = new System.Drawing.Size(648, 201);
			this.txtConteudoInformacional.TabIndex = 2;
			this.txtConteudoInformacional.Text = "";
			//
			//grpObservacoes
			//
			this.grpObservacoes.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.grpObservacoes.Controls.Add(this.txtObservacoes);
			this.grpObservacoes.Location = new System.Drawing.Point(8, 248);
			this.grpObservacoes.Name = "grpObservacoes";
			this.grpObservacoes.Size = new System.Drawing.Size(664, 144);
			this.grpObservacoes.TabIndex = 10;
			this.grpObservacoes.TabStop = false;
			this.grpObservacoes.Text = "Observações";
			//
			//txtObservacoes
			//
			this.txtObservacoes.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.txtObservacoes.Location = new System.Drawing.Point(8, 16);
			this.txtObservacoes.Multiline = true;
			this.txtObservacoes.Name = "txtObservacoes";
			this.txtObservacoes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtObservacoes.Size = new System.Drawing.Size(648, 121);
			this.txtObservacoes.TabIndex = 2;
			this.txtObservacoes.Text = "";
			//
			//GroupBox1
			//
			this.GroupBox1.Controls.Add(this.grpConteudoEstrutura);
			this.GroupBox1.Controls.Add(this.grpObservacoes);
			this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GroupBox1.Location = new System.Drawing.Point(6, 6);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(684, 404);
			this.GroupBox1.TabIndex = 12;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "3.1. Âmbito e conteúdo";
			//
			//PanelUFConteudoEstrutura
			//
			this.Controls.Add(this.GroupBox1);
			this.DockPadding.All = 6;
			this.Name = "PanelUFConteudoEstrutura";
			this.Size = new System.Drawing.Size(696, 416);
			this.grpConteudoEstrutura.ResumeLayout(false);
			this.grpObservacoes.ResumeLayout(false);
			this.GroupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

	#endregion

		private GISADataset.FRDBaseRow CurrentFRDBase;
		private GISADataset.SFRDConteudoEEstruturaRow CurrentSFRDConteudoEEstrutura;
		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			CurrentFRDBase = (GISADataset.FRDBaseRow)CurrentDataRow;

			UFRule.Current.LoadUFConteudoEstruturaData(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, conn);

			IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = false;
			byte[] Versao = null;
			if (GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.Select("IDFRDBase=" + CurrentFRDBase.ID.ToString()).Length != 0)
			{
				CurrentSFRDConteudoEEstrutura = (GISADataset.SFRDConteudoEEstruturaRow)(GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.Select("IDFRDBase=" + CurrentFRDBase.ID.ToString())[0]);
			}
			else
			{
				CurrentSFRDConteudoEEstrutura = GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.AddSFRDConteudoEEstruturaRow(CurrentFRDBase, "", "", Versao, 0);
			}

			if (! (CurrentSFRDConteudoEEstrutura.IsConteudoInformacionalNull()))
			{
				txtConteudoInformacional.Text = CurrentSFRDConteudoEEstrutura.ConteudoInformacional;
			}
			else
			{
				txtConteudoInformacional.Text = "";
			}

			if (! (CurrentSFRDConteudoEEstrutura.IsIncorporacaoNull()))
			{
				txtObservacoes.Text = CurrentSFRDConteudoEEstrutura.Incorporacao;
			}
			else
			{
				txtObservacoes.Text = "";
			}
			IsPopulated = true;
		}

		public override void ViewToModel()
		{
			if (CurrentSFRDConteudoEEstrutura == null || ! IsLoaded)
			{
				return;
			}
			CurrentSFRDConteudoEEstrutura.ConteudoInformacional = txtConteudoInformacional.Text;
			CurrentSFRDConteudoEEstrutura.Incorporacao = txtObservacoes.Text;
		}

		public override void Deactivate()
		{
            GUIHelper.GUIHelper.clearField(txtConteudoInformacional);
            GUIHelper.GUIHelper.clearField(txtObservacoes);
		}
	}

} //end of root namespace