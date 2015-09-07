using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA
{
	public class PanelMensagem : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelMensagem() : base()
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
		internal System.Windows.Forms.Label LblMensagem;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.LblMensagem = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LblMensagem
            // 
            this.LblMensagem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblMensagem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            this.LblMensagem.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMensagem.Location = new System.Drawing.Point(0, 0);
            this.LblMensagem.Name = "LblMensagem";
            this.LblMensagem.Size = new System.Drawing.Size(797, 600);
            this.LblMensagem.TabIndex = 2;
            this.LblMensagem.Text = "mensagem";
            this.LblMensagem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PanelMensagem
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.LblMensagem);
            this.Name = "PanelMensagem";
            this.ResumeLayout(false);

		}

	#endregion

	}

} //end of root namespace