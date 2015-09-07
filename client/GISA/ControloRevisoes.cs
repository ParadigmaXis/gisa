using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA
{
	public class ControloRevisoes : System.Windows.Forms.UserControl
	{

	#region  Windows Form Designer generated code 

		public ControloRevisoes() : base()
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
		internal System.Windows.Forms.GroupBox grpAutor;
		internal System.Windows.Forms.DateTimePicker dtpRecolha;
		internal GISA.ControloAutores ControloAutores1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.grpAutor = new System.Windows.Forms.GroupBox();
			this.dtpRecolha = new System.Windows.Forms.DateTimePicker();
			this.ControloAutores1 = new GISA.ControloAutores();
			this.grpAutor.SuspendLayout();
			this.SuspendLayout();
			//
			//grpAutor
			//
			this.grpAutor.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.grpAutor.Controls.Add(this.ControloAutores1);
			this.grpAutor.Controls.Add(this.dtpRecolha);
			this.grpAutor.Location = new System.Drawing.Point(0, 0);
			this.grpAutor.Name = "grpAutor";
			this.grpAutor.Size = new System.Drawing.Size(423, 42);
			this.grpAutor.TabIndex = 13;
			this.grpAutor.TabStop = false;
			this.grpAutor.Text = "Data e autor da revisão atual";
			//
			//dtpRecolha
			//
			this.dtpRecolha.CustomFormat = "yyyy-MM-dd";
			this.dtpRecolha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpRecolha.Location = new System.Drawing.Point(8, 15);
			this.dtpRecolha.Name = "dtpRecolha";
			this.dtpRecolha.Size = new System.Drawing.Size(91, 20);
			this.dtpRecolha.TabIndex = 8;
			//
			//ControloAutores1
			//
			this.ControloAutores1.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.ControloAutores1.Location = new System.Drawing.Point(112, 15);
			this.ControloAutores1.Name = "ControloAutores1";
			this.ControloAutores1.SelectedAutor = null;
			this.ControloAutores1.Size = new System.Drawing.Size(304, 20);
			this.ControloAutores1.TabIndex = 9;
			//
			//ControloRevisoes
			//
			this.Controls.Add(this.grpAutor);
			this.Name = "ControloRevisoes";
			this.Size = new System.Drawing.Size(424, 40);
			this.grpAutor.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	#endregion
	}

} //end of root namespace