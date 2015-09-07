using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA.Controls
{
	public class PxCompleteDateBox : System.Windows.Forms.UserControl
	{

	#region  Windows Form Designer generated code 

		public PxCompleteDateBox() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            CheckEnable.CheckedChanged += CheckEnable_CheckedChanged;
            CDateBoxYear.Leave += CDateBoxYear_Leave;
            CDateBoxMonth.Leave += CDateBoxMonth_Leave;
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
		internal System.Windows.Forms.NumericUpDown CDateBoxYear;
		internal System.Windows.Forms.NumericUpDown CDateBoxMonth;
		internal System.Windows.Forms.NumericUpDown CDateBoxDay;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.CheckBox CheckEnable;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.CDateBoxYear = new System.Windows.Forms.NumericUpDown();
            this.CDateBoxMonth = new System.Windows.Forms.NumericUpDown();
            this.CDateBoxDay = new System.Windows.Forms.NumericUpDown();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.CheckEnable = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.CDateBoxYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CDateBoxMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CDateBoxDay)).BeginInit();
            this.SuspendLayout();
            // 
            // CDateBoxYear
            // 
            this.CDateBoxYear.Enabled = false;
            this.CDateBoxYear.Location = new System.Drawing.Point(16, 0);
            this.CDateBoxYear.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.CDateBoxYear.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CDateBoxYear.Name = "CDateBoxYear";
            this.CDateBoxYear.Size = new System.Drawing.Size(50, 20);
            this.CDateBoxYear.TabIndex = 0;
            this.CDateBoxYear.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // CDateBoxMonth
            // 
            this.CDateBoxMonth.Enabled = false;
            this.CDateBoxMonth.Location = new System.Drawing.Point(73, 0);
            this.CDateBoxMonth.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.CDateBoxMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CDateBoxMonth.Name = "CDateBoxMonth";
            this.CDateBoxMonth.Size = new System.Drawing.Size(43, 20);
            this.CDateBoxMonth.TabIndex = 1;
            this.CDateBoxMonth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // CDateBoxDay
            // 
            this.CDateBoxDay.Enabled = false;
            this.CDateBoxDay.Location = new System.Drawing.Point(123, 0);
            this.CDateBoxDay.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.CDateBoxDay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CDateBoxDay.Name = "CDateBoxDay";
            this.CDateBoxDay.Size = new System.Drawing.Size(43, 20);
            this.CDateBoxDay.TabIndex = 2;
            this.CDateBoxDay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(114, 2);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(13, 16);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "/";
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(64, 2);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(13, 16);
            this.Label2.TabIndex = 4;
            this.Label2.Text = "/";
            // 
            // CheckEnable
            // 
            this.CheckEnable.Location = new System.Drawing.Point(0, 2);
            this.CheckEnable.Name = "CheckEnable";
            this.CheckEnable.Size = new System.Drawing.Size(16, 16);
            this.CheckEnable.TabIndex = 5;
            // 
            // PxCompleteDateBox
            // 
            this.Controls.Add(this.CDateBoxDay);
            this.Controls.Add(this.CDateBoxMonth);
            this.Controls.Add(this.CDateBoxYear);
            this.Controls.Add(this.CheckEnable);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Name = "PxCompleteDateBox";
            this.Size = new System.Drawing.Size(166, 22);
            ((System.ComponentModel.ISupportInitialize)(this.CDateBoxYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CDateBoxMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CDateBoxDay)).EndInit();
            this.ResumeLayout(false);

		}

	#endregion

		private void updateCDateBoxDay()
		{
			CDateBoxDay.Maximum = System.DateTime.DaysInMonth(int.Parse(CDateBoxYear.Value.ToString()), int.Parse(CDateBoxMonth.Value.ToString()));
		}

		private void CheckEnable_CheckedChanged(object sender, System.EventArgs e)
		{
			CDateBoxYear.Enabled = CheckEnable.Checked;
			CDateBoxMonth.Enabled = CheckEnable.Checked;
			CDateBoxDay.Enabled = CheckEnable.Checked;
		}

		private void CDateBoxYear_Leave(object sender, System.EventArgs e)
		{
			updateCDateBoxDay();
		}

		private void CDateBoxMonth_Leave(object sender, System.EventArgs e)
		{
			updateCDateBoxDay();
		}

		public string GetStandardMaskString
		{
			get
			{
				return CDateBoxYear.Value.ToString() + "/" + CDateBoxMonth.Value.ToString() + "/" + CDateBoxDay.Value.ToString();
			}
		}

		public System.DateTime GetStandardMaskDate
		{
			get
			{
                int year = int.Parse(CDateBoxYear.Value.ToString());
                int month = int.Parse(CDateBoxMonth.Value.ToString());
                int day = int.Parse(CDateBoxDay.Value.ToString());
                							
				System.DateTime tmpDate = DateTime.MinValue;
				try
				{
					tmpDate = new System.DateTime(year, month, day);
				}
				catch (Exception)
				{
                    UpdateDate();
                    tmpDate = System.DateTime.Now.Date;
				}

				return tmpDate;
			}
		}

        public System.DateTime GetStandardMaskDateMet(bool isMin)
        {
            int year;
            int month;
            int day;

            if (isMin)
            {
                year = 0;
                month = 1;
                day = 1;
            }
            else
            {
                year = 9999;
                month = 12;
                day = 31;
            }

            if (CDateBoxYear.Value.ToString().Equals(CDateBoxYear.Text))
                year = int.Parse(CDateBoxYear.Value.ToString());

            if (CDateBoxMonth.Value.ToString().Equals(CDateBoxMonth.Text))
                month = int.Parse(CDateBoxMonth.Value.ToString());

            if (CDateBoxDay.Value.ToString().Equals(CDateBoxDay.Text))
                day = int.Parse(CDateBoxDay.Value.ToString());

            System.DateTime tmpDate = DateTime.MinValue;
            try
            {
                tmpDate = new System.DateTime(year, month, day);
            }
            catch (Exception)
            {
                UpdateDate();
                tmpDate = System.DateTime.Now.Date;
            }

            return tmpDate;            
        }

		public bool Checked
		{
			get {return this.CheckEnable.Checked;}
			set {this.CheckEnable.Checked = value;}
		}

		public int Day
		{
			get {return decimal.ToInt32(this.CDateBoxDay.Value);}
			set {this.CDateBoxDay.Value = value;}
		}

		public int Month
		{
			get {return decimal.ToInt32(this.CDateBoxMonth.Value);}
			set {this.CDateBoxMonth.Value = value;}
		}


		public int Year
		{
			get {return decimal.ToInt32(this.CDateBoxYear.Value);}
			set {this.CDateBoxYear.Value = value;}
		}

		public void UpdateDate()
		{
			this.Day = System.DateTime.Now.Day;
			this.Month = System.DateTime.Now.Month;
			this.Year = System.DateTime.Now.Year;
		}
	}
}