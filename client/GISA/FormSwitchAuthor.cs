using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class FormSwitchAuthor : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormSwitchAuthor() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            btnOk.Click += btnOk_Click;

			ControloAutores1.LoadAndPopulateAuthors();
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
		internal System.Windows.Forms.Button btnOk;
		internal System.Windows.Forms.Button btnCancel;
		internal System.Windows.Forms.Label lblAuthor;
		internal System.Windows.Forms.Label lblOperator;
		internal System.Windows.Forms.Label lblTextOperator;
		internal GISA.ControloAutores ControloAutores1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblAuthor = new System.Windows.Forms.Label();
			this.lblOperator = new System.Windows.Forms.Label();
			this.lblTextOperator = new System.Windows.Forms.Label();
			this.ControloAutores1 = new GISA.ControloAutores();
			this.SuspendLayout();
			//
			//btnOk
			//
			this.btnOk.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(92, 64);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "Ok";
			//
			//btnCancel
			//
			this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(176, 64);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			//
			//lblAuthor
			//
			this.lblAuthor.Location = new System.Drawing.Point(8, 29);
			this.lblAuthor.Name = "lblAuthor";
			this.lblAuthor.Size = new System.Drawing.Size(55, 16);
			this.lblAuthor.TabIndex = 3;
			this.lblAuthor.Text = "Autor:";
			//
			//lblOperator
			//
			this.lblOperator.Location = new System.Drawing.Point(8, 8);
			this.lblOperator.Name = "lblOperator";
			this.lblOperator.Size = new System.Drawing.Size(55, 16);
			this.lblOperator.TabIndex = 4;
			this.lblOperator.Text = "Operador:";
			//
			//lblTextOperator
			//
			this.lblTextOperator.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.lblTextOperator.Location = new System.Drawing.Point(70, 8);
			this.lblTextOperator.Name = "lblTextOperator";
			this.lblTextOperator.Size = new System.Drawing.Size(204, 16);
			this.lblTextOperator.TabIndex = 5;
			//
			//ControloAutores1
			//
			this.ControloAutores1.Location = new System.Drawing.Point(70, 29);
			this.ControloAutores1.Name = "ControloAutores1";
			this.ControloAutores1.SelectedAutor = null;
			this.ControloAutores1.Size = new System.Drawing.Size(204, 20);
			this.ControloAutores1.TabIndex = 0;
			//
			//FormSwitchAuthor
			//
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(286, 99);
			this.Controls.Add(this.lblTextOperator);
			this.Controls.Add(this.lblOperator);
			this.Controls.Add(this.lblAuthor);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.ControloAutores1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSwitchAuthor";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Alteração do autor";
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			GISADataset.TrusteeUserRow lastTrusteeUserRowParent = SessionHelper.GetGisaPrincipal().TrusteeUserAuthor;
            if (ControloAutores1.SelectedAutor != null && ((GISADataset.TrusteeRow)ControloAutores1.SelectedAutor).GetTrusteeUserRows().Length > 0)
			{
				SessionHelper.GetGisaPrincipal().TrusteeUserAuthor = ((GISADataset.TrusteeRow)ControloAutores1.SelectedAutor).GetTrusteeUserRows()[0];
				SessionHelper.GetGisaPrincipal().TrusteeUserOperator.TrusteeUserRowParent = SessionHelper.GetGisaPrincipal().TrusteeUserAuthor;
			}
			else
			{
				SessionHelper.GetGisaPrincipal().TrusteeUserAuthor = null; //DirectCast(ControloAutores1.cbAutor.Items(0), GISADataset.TrusteeRow)
				SessionHelper.GetGisaPrincipal().TrusteeUserOperator["IDTrusteeUserDefaultAuthority"] = DBNull.Value;
			}

            PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save();
			PersistencyHelper.cleanDeletedData();
			SessionHelper.GetGisaPrincipal().TrusteeUserAuthor = SessionHelper.GetGisaPrincipal().TrusteeUserOperator.TrusteeUserRowParent;
		}
	}
} //end of root namespace