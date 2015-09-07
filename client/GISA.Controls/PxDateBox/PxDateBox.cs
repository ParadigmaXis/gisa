using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GISA.Controls {

		public class PxDateBox : System.Windows.Forms.UserControl
		{

	#region  Windows Form Designer generated code 

			public PxDateBox() : base()
			{

				//This call is required by the Windows Form Designer.
				InitializeComponent();

				//Add any initialization after the InitializeComponent() call
                txtAno.NextFocus += txtAno_NextFocus;
                txtMes.NextFocus += txtMes_NextFocus;
                txtMes.PreviousFocus += txtMes_PreviousFocus;
                txtDia.PreviousFocus += txtDia_PreviousFocus;
                base.EnabledChanged += MyBase_EnabledChanged;
                txtAno.TextChanged += MyBase_TextChanged;
                txtDia.TextChanged += MyBase_TextChanged;
                txtMes.TextChanged += MyBase_TextChanged;
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
			private System.Windows.Forms.Panel pnlBorder;
			private System.Windows.Forms.Label lblUnderlines;
			private GISA.Controls.PxDateBoxField txtAno;
			private GISA.Controls.PxDateBoxField txtMes;
			private GISA.Controls.PxDateBoxField txtDia;
			[System.Diagnostics.DebuggerStepThrough()]
			private void InitializeComponent()
			{
				this.pnlBorder = new System.Windows.Forms.Panel();
				this.txtDia = new GISA.Controls.PxDateBoxField();
				this.txtMes = new GISA.Controls.PxDateBoxField();
				this.txtAno = new GISA.Controls.PxDateBoxField();
				this.lblUnderlines = new System.Windows.Forms.Label();
				this.pnlBorder.SuspendLayout();
				this.SuspendLayout();
				//
				//pnlBorder
				//
				this.pnlBorder.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
				this.pnlBorder.BackColor = System.Drawing.SystemColors.Window;
				this.pnlBorder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
				this.pnlBorder.Controls.Add(this.txtDia);
				this.pnlBorder.Controls.Add(this.txtMes);
				this.pnlBorder.Controls.Add(this.txtAno);
				this.pnlBorder.Controls.Add(this.lblUnderlines);
				this.pnlBorder.Location = new System.Drawing.Point(1, 1);
				this.pnlBorder.Name = "pnlBorder";
				this.pnlBorder.Size = new System.Drawing.Size(80, 22);
				this.pnlBorder.TabIndex = 0;
				//
				//txtDia
				//
				this.txtDia.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right));
				this.txtDia.BorderStyle = System.Windows.Forms.BorderStyle.None;
				this.txtDia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
				this.txtDia.Location = new System.Drawing.Point(60, 2);
				this.txtDia.MaxLength = 2;
				this.txtDia.Name = "txtDia";
				this.txtDia.Size = new System.Drawing.Size(12, 13);
				this.txtDia.TabIndex = 4;
				this.txtDia.Text = "";
				this.txtDia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
				this.txtDia.Type = GISA.Controls.PxDateBoxFieldType.Day;
				//
				//txtMes
				//
				this.txtMes.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right));
				this.txtMes.BorderStyle = System.Windows.Forms.BorderStyle.None;
				this.txtMes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
				this.txtMes.Location = new System.Drawing.Point(37, 2);
				this.txtMes.MaxLength = 2;
				this.txtMes.Name = "txtMes";
				this.txtMes.Size = new System.Drawing.Size(12, 13);
				this.txtMes.TabIndex = 3;
				this.txtMes.Text = "";
				this.txtMes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
				this.txtMes.Type = GISA.Controls.PxDateBoxFieldType.Month;
				//
				//txtAno
				//
				this.txtAno.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right));
				this.txtAno.BorderStyle = System.Windows.Forms.BorderStyle.None;
				this.txtAno.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
				this.txtAno.Location = new System.Drawing.Point(2, 2);
				this.txtAno.MaxLength = 4;
				this.txtAno.Name = "txtAno";
				this.txtAno.Size = new System.Drawing.Size(24, 13);
				this.txtAno.TabIndex = 2;
				this.txtAno.Text = "";
				this.txtAno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
				this.txtAno.Type = GISA.Controls.PxDateBoxFieldType.Year;
				//
				//lblUnderlines
				//
				this.lblUnderlines.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
				this.lblUnderlines.Location = new System.Drawing.Point(0, 3);
				this.lblUnderlines.Name = "lblUnderlines";
				this.lblUnderlines.Size = new System.Drawing.Size(79, 16);
				this.lblUnderlines.TabIndex = 1;
				this.lblUnderlines.Text = "____ / __ / __";
				//
				//PxDateBox
				//
				this.Controls.Add(this.pnlBorder);
				this.Name = "PxDateBox";
				this.Size = new System.Drawing.Size(80, 22);
				this.pnlBorder.ResumeLayout(false);
				this.ResumeLayout(false);

				//INSTANT C# NOTE: Converted event handlers:
				

			}

	#endregion

			[Description("The date's year"), Category("Behavior"), RefreshProperties(RefreshProperties.All)]
			public string ValueYear
			{
				get
				{
					return txtAno.Text.Trim();
				}
				set
				{
					txtAno.Text = value;
					if (DesignMode && ! (txtAno.IsValid()))
					{
						txtAno.Text = "";
					}
				}
			}

			[Description("The date's month"), Category("Behavior"), RefreshProperties(RefreshProperties.All)]
			public string ValueMonth
			{
				get
				{
					return txtMes.Text.Trim();
				}
				set
				{
					txtMes.Text = value;
					if (DesignMode && ! (txtMes.IsValid()))
					{
						txtMes.Text = "";
					}
				}
			}

			[Description("The date's day"), Category("Behavior"), RefreshProperties(RefreshProperties.All)]
			public string ValueDay
			{
				get
				{
					return txtDia.Text.Trim();
				}
				set
				{
					txtDia.Text = value;
					if (DesignMode && ! (txtDia.IsValid()))
					{
						txtDia.Text = "";
					}
				}
			}

			[Description("The date value"), Category("Behavior")]
			public string ValueDate
			{
				get
				{
					return string.Format("{0}/{1}/{2}", ((ValueYear.Length == 0) ? "  " : ValueYear), ((ValueMonth.Length == 0) ? "  " : ValueMonth), ((ValueDay.Length == 0) ? "  " : ValueDay));
				}
			}

			protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
			{
				base.SetBoundsCore(x, y, width, 22, specified);
			}

			private void txtAno_NextFocus(int length)
			{
				txtMes.Focus();
			}

			private void txtMes_NextFocus(int length)
			{
				txtDia.Focus();
			}

			private void txtMes_PreviousFocus(int length)
			{
				txtAno.Focus();
			}

			private void txtDia_PreviousFocus(int length)
			{
				txtMes.Focus();
			}



			//TODO: existe um bug que impede os usercontrols de serem databoundable: http://www.dotnet247.com/247reference/msgs/23/115774.aspx			

			private void MyBase_EnabledChanged(object sender, System.EventArgs e)
			{

				if (this.Enabled)
				{
					pnlBorder.BackColor = System.Drawing.SystemColors.Window;
				}
				else
				{
					pnlBorder.BackColor = System.Drawing.SystemColors.InactiveBorder;
				}
			}

			public delegate void PxDateBoxTextChangedEventHandler();
			public event PxDateBoxTextChangedEventHandler PxDateBoxTextChanged;
			private void MyBase_TextChanged(object sender, System.EventArgs e)
			{

				if (PxDateBoxTextChanged != null)
					PxDateBoxTextChanged();
			}
		}
	

} //end of root namespace