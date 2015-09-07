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
	/// Summary description for PxDecimalBox.
	/// </summary>
	public class PxDecimalBox : GISA.Controls.PxNumericBox
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;        

		public PxDecimalBox()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call			
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
			this.SuspendLayout();
			// 
			// PxDecimalBox
			// 
			this.Name = "PxDecimalBox";
			this.Size = new System.Drawing.Size(100, 20);
			this.ResumeLayout(false);

		}
		#endregion

        private int mDecimalNumbers = int.MinValue;
        public int DecimalNumbers
        {
            get { return mDecimalNumbers; }
            set { mDecimalNumbers = value; }
        }

		protected override bool isValidValue (string val)
		{
		    return MathHelper.IsDecimal(val) && !ExceedDecimalNumbers(val);
		}

        private bool ExceedDecimalNumbers(string val)
        {
            if (DecimalNumbers > 0 && val.Contains(","))
            {
                int startindex = val.IndexOf(",") + 1;
                int last = (val.Length) - startindex;
                return (val.Substring(startindex, last).Length > DecimalNumbers);
            }
            else
                return false;
        }
	}
}
