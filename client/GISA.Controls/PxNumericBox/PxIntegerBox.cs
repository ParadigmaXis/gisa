using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using GISA.Utils;

namespace GISA.Controls
{
	/// <summary>
	/// Summary description for PxIntegerBox.
	/// </summary>
	public class PxIntegerBox : GISA.Controls.PxNumericBox
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PxIntegerBox()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			this.numericBox.LostFocus += new EventHandler(numericBox_LostFocus);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
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
			// 
			// PxIntegerBox
			// 
			this.Name = "PxIntegerBox";
			this.Size = new System.Drawing.Size(100, 20);
			this.numericBox.TextAlign = HorizontalAlignment.Center;

		}
		#endregion

        public override string Text {
            get { return this.lastText; }
			set {
				this.numericBox.Text = value;
                this.lastText = value;
			}
		}

        public HorizontalAlignment TextAlign
        {
            get {return this.numericBox.TextAlign;}
            set {this.numericBox.TextAlign = value;}
        }

		protected override bool isValidValue (string val)
		{
			return MathHelper.IsInteger(val);
		}
		
		private void numericBox_LostFocus(object sender,EventArgs ce)
		{
            this.numericBox.Text = this.lastText ;
		}

        protected virtual string GetLastValidText()
        {
            return this.lastText;
        }

        protected override void numBoxKeyPressed(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)13) {
                if (this.numericBox.Text.Length > 0) {
                    GoToPageEventArgs args1 = new GoToPageEventArgs(System.Convert.ToInt32(this.numericBox.Text), true);
                    this.OnGoToPage(args1);
                    if (args1.success)
                        this.lastText = this.numericBox.Text;
                    else
                        this.numericBox.Text = this.lastText;
                }
                else
                    this.numericBox.Text = this.lastText;

                this.OnPropagateEnterPressed(sender, new EventArgs());
            }
        }

        public event PropagateEnterPressedEventHandler PropagateEnterPressed;
        public delegate void PropagateEnterPressedEventHandler(object sender, EventArgs e);
        protected virtual void OnPropagateEnterPressed(object sender, EventArgs e)
        {
            if (this.PropagateEnterPressed != null)
            {
                PropagateEnterPressed(this, e);
            }
        }

        public event GoToPageEventHandler GoToPage;
        public delegate void GoToPageEventHandler(object sender, GoToPageEventArgs e);
        protected virtual void OnGoToPage(GoToPageEventArgs e)
        {
            if (this.GoToPage != null)
                GoToPage(this, e);
        }

	}

	// classe que define os argumentos para o evento de duplo click
	public class GoToPageEventArgs: EventArgs
	{
		public GoToPageEventArgs (int pageNr, bool success)
		{
			this.m_pageNr = pageNr;
			this.m_success = success;
		}

		private int m_pageNr;
		public int pageNr 
		{
			get {return this.m_pageNr;}
			set {this.m_pageNr = value;}
		}

		private bool m_success;
		public bool success 
		{
			get {return this.m_success;}
			set {this.m_success = value;}
		}
	}

    public class PxPageIntegerBox : PxIntegerBox
    {
        public string lastPageSelected = string.Empty;
        protected override void numBoxKeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (this.numericBox.Text.Length > 0)
                {
                    GoToPageEventArgs args1 = new GoToPageEventArgs(System.Convert.ToInt32(this.numericBox.Text), true);
                    this.OnGoToPage(args1);
                    if (args1.success)
                        lastPageSelected = this.numericBox.Text;
                    else
                        this.numericBox.Text = lastPageSelected;
                }
                else
                    this.numericBox.Text = lastPageSelected;
            }
        }

        protected override string GetLastValidText()
        {
            return lastPageSelected;
        }

        public override void Clear()
        {
            base.Clear();
            lastPageSelected = string.Empty;
        }
    }
}
