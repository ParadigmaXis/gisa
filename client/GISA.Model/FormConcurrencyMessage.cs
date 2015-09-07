//INSTANT C# NOTE: Formerly VB.NET project-level imports:
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace GISA.Model
{
	public class FormConcurrencyMessage : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormConcurrencyMessage() : base()
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
		internal System.Windows.Forms.Button btnOK;
		internal System.Windows.Forms.Label lblQuestion;
		internal System.Windows.Forms.Button btnCancel;
		internal System.Windows.Forms.Button Button1;
		//Friend WithEvents Panel2 As System.Windows.Forms.Panel
		//Friend WithEvents txtReport As System.Windows.Forms.TextBox
		internal System.Windows.Forms.Panel Panel1;
		internal System.Windows.Forms.RichTextBox textBD;
		internal System.Windows.Forms.RichTextBox textUser;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label Label2;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.btnOK = new System.Windows.Forms.Button();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.textBD = new System.Windows.Forms.RichTextBox();
            this.textUser = new System.Windows.Forms.RichTextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnOK.Location = new System.Drawing.Point(354, 363);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(97, 34);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "Versão no servidor";
            // 
            // lblQuestion
            // 
            this.lblQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQuestion.Location = new System.Drawing.Point(12, 12);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(520, 36);
            this.lblQuestion.TabIndex = 10;
            this.lblQuestion.Text = "A operação que pretende executar vai afectar informação que foi alterada por outr" +
                "o utilizador. Indique, por favor, a versão que pretende manter.\r\n";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(457, 363);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 34);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancelar";
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Button1.Location = new System.Drawing.Point(276, 363);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(72, 34);
            this.Button1.TabIndex = 12;
            this.Button1.Text = "Versão local";
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.textBD);
            this.Panel1.Controls.Add(this.textUser);
            this.Panel1.Location = new System.Drawing.Point(8, 84);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(528, 273);
            this.Panel1.TabIndex = 14;
            // 
            // textBD
            // 
            this.textBD.Location = new System.Drawing.Point(268, 4);
            this.textBD.Name = "textBD";
            this.textBD.ReadOnly = true;
            this.textBD.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.textBD.Size = new System.Drawing.Size(256, 252);
            this.textBD.TabIndex = 1;
            this.textBD.Text = "";
            // 
            // textUser
            // 
            this.textUser.Location = new System.Drawing.Point(4, 4);
            this.textUser.Name = "textUser";
            this.textUser.ReadOnly = true;
            this.textUser.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.textUser.Size = new System.Drawing.Size(256, 252);
            this.textUser.TabIndex = 0;
            this.textUser.Text = "";
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(16, 60);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(252, 16);
            this.Label1.TabIndex = 15;
            this.Label1.Text = "Versão local";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(280, 60);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(252, 16);
            this.Label2.TabIndex = 16;
            this.Label2.Text = "Versão no servidor";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormConcurrencyMessage
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(546, 407);
            this.ControlBox = false;
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblQuestion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormConcurrencyMessage";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dados originais alterados";
            this.Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion


		private void lnkAlteracoes_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			// apresentar dialogo com o RTF de diff
			FormConcurrencyChanges form = new FormConcurrencyChanges();
			form.ShowDialog();
		}

		private void btnDetails_Click(object sender, System.EventArgs e)
		{
			//If Panel2.Visible Then
			//    Panel2.Visible = False
			//    Me.Height = 140
			//Else
			//    Panel2.Visible = True
			//    Me.Height = 140 + 165
			//End If
		}

		public string DetalhesUser
		{
			get
			{
				return textUser.Text;
			}
			set
			{
				textUser.Text = value;
			}
		}

		public string DetalhesBD
		{
			get
			{
				return textBD.Text;
			}
			set
			{
				textBD.Text = value;
			}
		}
	}

} //end of root namespace