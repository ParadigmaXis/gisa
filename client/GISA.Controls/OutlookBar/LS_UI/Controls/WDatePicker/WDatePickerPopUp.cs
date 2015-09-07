using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace LumiSoft.UI.Controls
{
	/// <summary>
	/// 
	/// </summary>
	public delegate void DateSelectionChangedHandler(object sender,WDateChanged_EventArgs e);

	#region public class WDateChanged_EventArgs

	public class WDateChanged_EventArgs
	{
		private DateTime m_SelectedDate;

		public WDateChanged_EventArgs(DateTime selectedDate)
		{
			m_SelectedDate = selectedDate;
		}

		#region Properties Implementation
		
		public DateTime Date
		{
			get{ return m_SelectedDate; }
		}

		#endregion

	}

	#endregion

	/// <summary>
	/// Summary description for WDatePickerPopUp.
	/// </summary>
	public class WDatePickerPopUp : LumiSoft.UI.Controls.WPopUpFormBase
	{
		private WMonthCalendar monthCalendar1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public event DateSelectionChangedHandler SelectionChanged = null;

		private ViewStyle m_pViewStyle = null;

		/// <summary>
		/// 
		/// </summary>
		public WDatePickerPopUp(Control parent,ViewStyle viewStyle,DateTime date) : base(parent)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			m_pViewStyle = viewStyle;
			monthCalendar1.SetDate(date);
		}

		#region function Dispose

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

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.monthCalendar1 = new WMonthCalendar();
			this.SuspendLayout();
			// 
			// monthCalendar1
			// 
			this.monthCalendar1.Location = new System.Drawing.Point(1, 1);
			this.monthCalendar1.Name = "monthCalendar1";
			this.monthCalendar1.TabIndex = 0;
			this.monthCalendar1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.monthCalendar1_MouseDown);
			// 
			// WDatePickerPopUp
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(194, 157);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.monthCalendar1});
			this.Name = "WDatePickerPopUp";
			this.Text = "WDatePickerPopUp";
			this.ResumeLayout(false);

		}
		#endregion

		
		#region Events handling

		private void monthCalendar1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			System.Windows.Forms.MonthCalendar.HitTestInfo hTest = monthCalendar1.HitTest(this.PointToClient(Control.MousePosition));
			System.Windows.Forms.MonthCalendar.HitArea hArea = hTest.HitArea;

			if(hArea == System.Windows.Forms.MonthCalendar.HitArea.Date){
				OnSelectionChanged();

				this.Close();
			}
		}

		#endregion


		#region function OnPaint

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			Rectangle rect = new Rectangle(this.ClientRectangle.Location,new Size(this.ClientRectangle.Width - 1,this.ClientRectangle.Height - 1));
			Pen pen = new Pen(m_pViewStyle.BorderHotColor);

			e.Graphics.DrawRectangle(pen,rect);
		}

		#endregion

		#region function PostMessage

		public override void PostMessage(ref Message m)
		{
			Message msg = new Message();
			msg.HWnd    = monthCalendar1.Handle;
			msg.LParam  = m.LParam;
			msg.Msg     = m.Msg;
			msg.Result  = m.Result;
			msg.WParam  = m.WParam;

			// Forward message to ListBox
			monthCalendar1.PostMessage(ref msg);
		}

		#endregion


		#region function RaiseSelectionChanged

		public void RaiseSelectionChanged()
		{
			OnSelectionChanged();
		}

		#endregion
		
		#region Events Implementation

		protected void OnSelectionChanged()
		{
			if(this.SelectionChanged != null){
				this.SelectionChanged(this,new WDateChanged_EventArgs(monthCalendar1.SelectionStart));
			}
		}

		#endregion

	}
}
