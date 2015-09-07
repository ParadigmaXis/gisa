using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

using GISA.Controls.ControloAut;

namespace GISA
{
	public class FormTermo : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormTermo() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            ListTermos.IncrementalSearchTextChanged += listTermos_IncrementalSearchTextChanged;
            ListTermos.TermoChanged += ListTermos_TermoChanged;

			ListTermos.LoadData();
			UpdateButtonState();
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
		internal System.Windows.Forms.Button btnCancel;
		internal System.Windows.Forms.Button btnOk;
		internal System.Windows.Forms.StatusBar StatusBar1;
		internal System.Windows.Forms.StatusBarPanel StatusBarIncrementalText;
		internal ListTermos ListTermos;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.StatusBar1 = new System.Windows.Forms.StatusBar();
			this.StatusBarIncrementalText = new System.Windows.Forms.StatusBarPanel();
			this.ListTermos = new ListTermos();
			((System.ComponentModel.ISupportInitialize)this.StatusBarIncrementalText).BeginInit();
			this.SuspendLayout();
			//
			//btnCancel
			//
			this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(308, 264);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(88, 24);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancelar";
			//
			//btnOk
			//
			this.btnOk.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(212, 264);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(88, 24);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "Aceitar";
			//
			//StatusBar1
			//
			this.StatusBar1.Location = new System.Drawing.Point(0, 295);
			this.StatusBar1.Name = "StatusBar1";
			this.StatusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {this.StatusBarIncrementalText});
			this.StatusBar1.ShowPanels = true;
			this.StatusBar1.Size = new System.Drawing.Size(408, 22);
			this.StatusBar1.SizingGrip = false;
			this.StatusBar1.TabIndex = 12;
			//
			//StatusBarIncrementalText
			//
			this.StatusBarIncrementalText.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.StatusBarIncrementalText.Width = 408;
			//
			//ListTermos
			//
			this.ListTermos.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.ListTermos.Location = new System.Drawing.Point(8, 4);
			this.ListTermos.Name = "ListTermos";
			this.ListTermos.ValidAuthorizedForm = null;
			this.ListTermos.Size = new System.Drawing.Size(396, 252);
			this.ListTermos.TabIndex = 1;
			//
			//FormTermo
			//
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(408, 317);
			this.ControlBox = false;
			this.Controls.Add(this.ListTermos);
			this.Controls.Add(this.StatusBar1);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.Name = "FormTermo";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Seleção de termo";
			((System.ComponentModel.ISupportInitialize)this.StatusBarIncrementalText).EndInit();
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		public FormTermo(long caID) : base()
		{
			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            ListTermos.IncrementalSearchTextChanged += listTermos_IncrementalSearchTextChanged;
            ListTermos.TermoChanged += ListTermos_TermoChanged;

			ListTermos.LoadData(caID);
			UpdateButtonState();
		}

		private void listTermos_IncrementalSearchTextChanged(string text)
		{
			StatusBarIncrementalText.Text = text;
		}

		private void ListTermos_TermoChanged()
		{
			UpdateButtonState();
		}

		private void UpdateButtonState()
		{
			if (ListTermos.ValidAuthorizedForm == null)
				btnOk.Enabled = false;
			else
				btnOk.Enabled = true;
		}
	}
}