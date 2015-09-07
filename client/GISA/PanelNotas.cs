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
	public class PanelNotas : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelNotas() : base()
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
		internal System.Windows.Forms.GroupBox grpNotas;
		internal System.Windows.Forms.TextBox txtNotas;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.grpNotas = new System.Windows.Forms.GroupBox();
            this.txtNotas = new System.Windows.Forms.TextBox();
            this.grpNotas.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpNotas
            // 
            this.grpNotas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpNotas.Controls.Add(this.txtNotas);
            this.grpNotas.Location = new System.Drawing.Point(3, 3);
            this.grpNotas.Name = "grpNotas";
            this.grpNotas.Size = new System.Drawing.Size(794, 594);
            this.grpNotas.TabIndex = 1;
            this.grpNotas.TabStop = false;
            this.grpNotas.Text = "Notas";
            // 
            // txtNotas
            // 
            this.txtNotas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotas.Location = new System.Drawing.Point(8, 16);
            this.txtNotas.Multiline = true;
            this.txtNotas.Name = "txtNotas";
            this.txtNotas.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotas.Size = new System.Drawing.Size(778, 572);
            this.txtNotas.TabIndex = 0;
            // 
            // PanelNotas
            // 
            this.Controls.Add(this.grpNotas);
            this.Name = "PanelNotas";
            this.grpNotas.ResumeLayout(false);
            this.grpNotas.PerformLayout();
            this.ResumeLayout(false);

		}

	#endregion

        private GISADataset.FRDBaseRow CurrentFRDBase;
        private GISADataset.SFRDNotaGeralRow CurrentSFRDNotaGeral;

		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			CurrentFRDBase = (GISADataset.FRDBaseRow)CurrentDataRow;
			string QueryFilter = "IDFRDBase=" + CurrentFRDBase.ID.ToString();
			string WhereQueryFilter = "WHERE " + QueryFilter;

			FRDRule.Current.LoadNotasData(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, conn);

			IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = false;
			byte[] Versao = null;
			string QueryFilter = "IDFRDBase=" + CurrentFRDBase.ID.ToString();
			if (GisaDataSetHelper.GetInstance().SFRDNotaGeral.Select(QueryFilter).Length != 0)
			{
				CurrentSFRDNotaGeral = (GISADataset.SFRDNotaGeralRow)(GisaDataSetHelper.GetInstance().SFRDNotaGeral.Select(QueryFilter)[0]);
			}
			else
			{
				CurrentSFRDNotaGeral = GisaDataSetHelper.GetInstance().SFRDNotaGeral.AddSFRDNotaGeralRow(CurrentFRDBase, "", Versao, 0);
			}

			if (! (CurrentSFRDNotaGeral.IsNotaGeralNull()))
			{
				txtNotas.Text = CurrentSFRDNotaGeral.NotaGeral;
			}
			else
			{
				txtNotas.Text = "";
			}
			IsPopulated = true;
		}

		public override void ViewToModel()
		{
			if (CurrentSFRDNotaGeral == null || ! IsLoaded)
			{
				return;
			}

			CurrentSFRDNotaGeral.NotaGeral = txtNotas.Text;
		}

		public override void Deactivate()
		{
            GUIHelper.GUIHelper.clearField(txtNotas);
		}
	}

} //end of root namespace