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
	public class ControloAutores : System.Windows.Forms.UserControl
	{

	#region  Windows Form Designer generated code 

		public ControloAutores() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call

		}

		//UserControl overrides dispose to clean up the component list.
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
		internal System.Windows.Forms.ComboBox cbAutor;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.cbAutor = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			//
			//cbAutor
			//
			this.cbAutor.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.cbAutor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbAutor.Location = new System.Drawing.Point(0, 0);
			this.cbAutor.Name = "cbAutor";
			this.cbAutor.Size = new System.Drawing.Size(209, 21);
			this.cbAutor.TabIndex = 3;
			//
			//ControloAutores
			//
			this.Controls.Add(this.cbAutor);
			this.Name = "ControloAutores";
			this.Size = new System.Drawing.Size(209, 21);
			this.ResumeLayout(false);

		}

	#endregion

		public object SelectedAutor
		{
			get
			{
				return cbAutor.SelectedItem;
			}
			set
			{
				cbAutor.SelectedItem = value;
			}
		}

		public void LoadAndPopulateAuthors()
		{
			cbAutor.Items.Clear();
			LoadAuthors();

			ArrayList tAuthors = Trustee.GetAvailableAuthors(true);
			cbAutor.DisplayMember = "Name";

			GISADataset.TrusteeRow emptyTRow = GisaDataSetHelper.GetInstance().Trustee.NewTrusteeRow();
			emptyTRow.Name = "";
			cbAutor.Items.Add(emptyTRow);
			cbAutor.Items.AddRange(tAuthors.ToArray());
		}

		private void LoadAuthors()
		{
			// carregar informação da base de dados
			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
				TrusteeRule.Current.LoadAuthorsData(GisaDataSetHelper.GetInstance(), ho.Connection);
			}
			finally
			{
				ho.Dispose();
			}
		}
	}

} //end of root namespace