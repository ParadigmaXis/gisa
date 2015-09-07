using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class FormChangePassword : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormChangePassword() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            btnOk.Click += btnOk_Click;
            txtNovaPalavraChave.TextChanged += txtNovaPalavraChave_TextChanged;
			updateButtons();
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
		internal System.Windows.Forms.Label lblNovaPalavraChave;
		internal System.Windows.Forms.TextBox txtNovaPalavraChave;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblNovaPalavraChave = new System.Windows.Forms.Label();
			this.txtNovaPalavraChave = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			//
			//btnOk
			//
			this.btnOk.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(98, 40);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "Ok";
			//
			//btnCancel
			//
			this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(186, 40);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancelar";
			//
			//lblNovaPalavraChave
			//
			this.lblNovaPalavraChave.Location = new System.Drawing.Point(9, 8);
			this.lblNovaPalavraChave.Name = "lblNovaPalavraChave";
			this.lblNovaPalavraChave.Size = new System.Drawing.Size(108, 20);
			this.lblNovaPalavraChave.TabIndex = 2;
			this.lblNovaPalavraChave.Text = "Nova palavra chave:";
			//
			//txtNovaPalavraChave
			//
			this.txtNovaPalavraChave.Location = new System.Drawing.Point(123, 5);
			this.txtNovaPalavraChave.Name = "txtNovaPalavraChave";
			this.txtNovaPalavraChave.PasswordChar = (char)(42);
			this.txtNovaPalavraChave.Size = new System.Drawing.Size(149, 20);
			this.txtNovaPalavraChave.TabIndex = 1;
			this.txtNovaPalavraChave.Text = "";
			//
			//FormChangePassword
			//
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(282, 75);
			this.Controls.Add(this.txtNovaPalavraChave);
			this.Controls.Add(this.lblNovaPalavraChave);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormChangePassword";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Alteração de palavra chave";
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			if (! (SessionHelper.GetGisaPrincipal().TrusteeUserOperator.Password.Equals(txtNovaPalavraChave.Text)))
			{
				SessionHelper.GetGisaPrincipal().TrusteeUserOperator.Password = CryptographyHelper.GetMD5(txtNovaPalavraChave.Text);
				GISA.Model.PersistencyHelper.save();
				GISA.Model.PersistencyHelper.cleanDeletedData();
			}
		}

		private void txtNovaPalavraChave_TextChanged(object sender, System.EventArgs e)
		{
			updateButtons();
		}

		private void updateButtons()
		{
			btnOk.Enabled = txtNovaPalavraChave.Text.Length > 0;
		}
	}

} //end of root namespace