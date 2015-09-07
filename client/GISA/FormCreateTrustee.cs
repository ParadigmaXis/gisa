using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class FormCreateTrustee : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormCreateTrustee() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            txtTrusteeName.TextChanged += txtTrusteeName_TextChanged;

			revalidateTrusteeName();
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
		protected internal System.Windows.Forms.TextBox txtTrusteeName;
		protected internal System.Windows.Forms.Label lblTrusteeName;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.txtTrusteeName = new System.Windows.Forms.TextBox();
			this.lblTrusteeName = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			//txtTrusteeName
			//
			this.txtTrusteeName.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.txtTrusteeName.Location = new System.Drawing.Point(56, 8);
			this.txtTrusteeName.Name = "txtTrusteeName";
			this.txtTrusteeName.Size = new System.Drawing.Size(176, 20);
			this.txtTrusteeName.TabIndex = 11;
			this.txtTrusteeName.Text = "";
			//
			//lblTrusteeName
			//
			this.lblTrusteeName.Location = new System.Drawing.Point(8, 8);
			this.lblTrusteeName.Name = "lblTrusteeName";
			this.lblTrusteeName.Size = new System.Drawing.Size(48, 16);
			this.lblTrusteeName.TabIndex = 10;
			this.lblTrusteeName.Text = "Nome:";
			//
			//btnOk
			//
			this.btnOk.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(56, 40);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 12;
			this.btnOk.Text = "Ok";
			//
			//btnCancel
			//
			this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(144, 40);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 13;
			this.btnCancel.Text = "Cancelar";
			//
			//FormCreateTrustee
			//
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(248, 69);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.txtTrusteeName);
			this.Controls.Add(this.lblTrusteeName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCreateTrustee";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		private void txtTrusteeName_TextChanged(object sender, System.EventArgs e)
		{
			revalidateTrusteeName();
		}

		private void revalidateTrusteeName()
		{
			if (isTrusteeNameWellFormed(txtTrusteeName.Text) && GisaDataSetHelper.GetInstance().Trustee.Select(string.Format("Name='{0}'", txtTrusteeName.Text)).Length == 0)
			{

				btnOk.Enabled = true;
			}
			else
			{
				btnOk.Enabled = false;
			}
		}

		private static System.Text.RegularExpressions.Regex regexpIsValidTrustee = null;
		private bool isTrusteeNameWellFormed(string name)
		{
			if (regexpIsValidTrustee == null)
			{
                regexpIsValidTrustee = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9_-]+$", System.Text.RegularExpressions.RegexOptions.Compiled);
			}
			System.Text.RegularExpressions.Match result = null;
			result = regexpIsValidTrustee.Match(name);
			return result.Success && result.Value.Equals(name);
		}
	}

} //end of root namespace