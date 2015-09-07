using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA
{
	public class FormCreateDiplomaModelo : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormCreateDiplomaModelo() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            txtDesignacao.TextChanged += txtDesignacao_TextChanged;
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
		internal System.Windows.Forms.TextBox txtDesignacao;
		internal System.Windows.Forms.Label lblDesignacao;
		internal System.Windows.Forms.Button btnOk;
		internal System.Windows.Forms.Button btnCancel;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.txtDesignacao = new System.Windows.Forms.TextBox();
			this.lblDesignacao = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			//txtDesignacao
			//
			this.txtDesignacao.Location = new System.Drawing.Point(80, 8);
			this.txtDesignacao.Name = "txtDesignacao";
			this.txtDesignacao.Size = new System.Drawing.Size(244, 20);
			this.txtDesignacao.TabIndex = 0;
			this.txtDesignacao.Text = "";
			//
			//lblDesignacao
			//
			this.lblDesignacao.Location = new System.Drawing.Point(8, 12);
			this.lblDesignacao.Name = "lblDesignacao";
			this.lblDesignacao.Size = new System.Drawing.Size(68, 16);
			this.lblDesignacao.TabIndex = 1;
			this.lblDesignacao.Text = "Designação:";
			//
			//btnOk
			//
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Enabled = false;
			this.btnOk.Location = new System.Drawing.Point(148, 40);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "OK";
			//
			//btnCancel
			//
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(232, 40);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancelar";
			//
			//FormCreateDiplomaModelo
			//
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(332, 73);
			this.ControlBox = false;
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.lblDesignacao);
			this.Controls.Add(this.txtDesignacao);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "FormCreateDiplomaModelo";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Diploma/Modelo";
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		public string Designacao
		{
			get
			{
				return txtDesignacao.Text.Trim();
			}
			set
			{
				txtDesignacao.Text = value;
			}
		}

		private void txtDesignacao_TextChanged(object sender, System.EventArgs e)
		{
			UpdateButtonState();
		}

		private void UpdateButtonState()
		{
			if (txtDesignacao.Text.Length > 0)
			{
				btnOk.Enabled = true;
			}
			else
			{
				btnOk.Enabled = false;
			}
		}
	}

} //end of root namespace