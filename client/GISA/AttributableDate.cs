//INSTANT C# NOTE: Formerly VB.NET project-level imports:
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class AttributableDate : System.Windows.Forms.UserControl
	{

	#region  Windows Form Designer generated code 

		public AttributableDate() : base()
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
		internal System.Windows.Forms.GroupBox grpTitulo;
		internal GISA.Controls.PxDateBox dtData;
		internal System.Windows.Forms.CheckBox chkAtribuida;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.grpTitulo = new System.Windows.Forms.GroupBox();
            this.dtData = new GISA.Controls.PxDateBox();
            this.chkAtribuida = new System.Windows.Forms.CheckBox();
            this.grpTitulo.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpTitulo
            // 
            this.grpTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTitulo.Controls.Add(this.dtData);
            this.grpTitulo.Controls.Add(this.chkAtribuida);
            this.grpTitulo.Location = new System.Drawing.Point(0, 0);
            this.grpTitulo.Name = "grpTitulo";
            this.grpTitulo.Size = new System.Drawing.Size(179, 44);
            this.grpTitulo.TabIndex = 1;
            this.grpTitulo.TabStop = false;
            this.grpTitulo.Text = "Data";
            // 
            // dtData
            // 
            this.dtData.Location = new System.Drawing.Point(8, 14);
            this.dtData.Name = "dtData";
            this.dtData.Size = new System.Drawing.Size(82, 22);
            this.dtData.TabIndex = 1;
            this.dtData.ValueDay = "";
            this.dtData.ValueMonth = "";
            this.dtData.ValueYear = "";
            // 
            // chkAtribuida
            // 
            this.chkAtribuida.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAtribuida.Location = new System.Drawing.Point(104, 18);
            this.chkAtribuida.Name = "chkAtribuida";
            this.chkAtribuida.Size = new System.Drawing.Size(69, 16);
            this.chkAtribuida.TabIndex = 2;
            this.chkAtribuida.Text = "Atribuída";
            // 
            // AttributableDate
            // 
            this.Controls.Add(this.grpTitulo);
            this.Name = "AttributableDate";
            this.Size = new System.Drawing.Size(179, 44);
            this.grpTitulo.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

		[System.ComponentModel.Browsable(true), System.ComponentModel.Category("Appearance")]
		public string Title
		{
			get
			{
				return grpTitulo.Text;
			}
			set
			{
				grpTitulo.Text = value;
			}
		}
	}

} //end of root namespace