using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GISA.Controls
{
	/// <summary>
	/// Summary description for PxNumericBox.
	/// </summary>
	public abstract class PxNumericBox : System.Windows.Forms.UserControl
	{
		protected System.Windows.Forms.TextBox numericBox;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PxNumericBox()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			this.numericBox.TextChanged+=new EventHandler(TextBox_TextChanged);
            this.numericBox.KeyPress += new KeyPressEventHandler(numericBox_KeyPress);
		}

        protected virtual void numBoxKeyPressed(object sender, KeyPressEventArgs e)
        {
        }

        protected void numericBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            numBoxKeyPressed(sender, e);
        }

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{            
			if( disposing )
			{
				if(components != null)
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.numericBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// numericBox
			// 
			this.numericBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.numericBox.CausesValidation = false;
			this.numericBox.Location = new System.Drawing.Point(0, 0);
			this.numericBox.Name = "numericBox";
			this.numericBox.TabIndex = 1;
			this.numericBox.Text = "";
			// 
			// PxNumericBox
			// 
			this.Controls.Add(this.numericBox);
			this.Name = "PxNumericBox";
			this.Size = new System.Drawing.Size(100, 20);
			this.ResumeLayout(false);

		}
		#endregion

		public override string Text
		{
            get { return this.numericBox.Text; }
            set { this.numericBox.Text = value; }
		}
        
		internal string lastText;
		private void TextBox_TextChanged(object sender,EventArgs e)
		{
            this.numericBox.TextChanged -= new EventHandler(TextBox_TextChanged);
            string val = this.numericBox.Text;
            val = val.Replace('.', ',');
			if (this.numericBox.Text == null || this.numericBox.Text.Length == 0 || isValidValue(val))
			{
				lastText = val;
                this.numericBox.Text = val;
			}
			else
				this.numericBox.Text = lastText;

            this.numericBox.SelectionStart = this.numericBox.Text.Length;
            this.numericBox.TextChanged += new EventHandler(TextBox_TextChanged);
		}

		protected abstract bool isValidValue(string val);

		public virtual void Clear()
		{
			this.numericBox.TextChanged-=new EventHandler(TextBox_TextChanged);
			this.numericBox.Text = string.Empty;
			this.lastText = string.Empty;
			this.numericBox.TextChanged+=new EventHandler(TextBox_TextChanged);
		}
	}
}
