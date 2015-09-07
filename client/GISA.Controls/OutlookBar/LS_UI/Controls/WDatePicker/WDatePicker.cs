using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace LumiSoft.UI.Controls.WDatePicker
{
	/// <summary>
	/// Summary description for WDatePicker.
	/// </summary>
	public class WDatePicker : WButtonEdit
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public event System.EventHandler DateChanged = null;

		private WDatePickerPopUp m_WDatePickerPopUp = null;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public WDatePicker()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

			m_pTextBox.LostFocus += new System.EventHandler(this.m_pTextBox_OnLostFocus);

			this.Mask = WEditBox_Mask.Date;
			this.Value = DateTime.Today;

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			((System.ComponentModel.ISupportInitialize)(this.m_pTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// m_pTextBox
			// 
			this.m_pTextBox.Size = new System.Drawing.Size(63, 13);
			this.m_pTextBox.ProccessMessage += new LumiSoft.UI.Controls.WMessage_EventHandler(this.m_pTextBox_ProccessMessage);
			// 
			// WDatePicker
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.m_pTextBox});
			this.Name = "WDatePicker";
			this.Size = new System.Drawing.Size(88, 20);
			this.ViewStyle.BorderColor = System.Drawing.Color.DarkGray;
			this.ViewStyle.BorderHotColor = System.Drawing.Color.Black;
			this.ViewStyle.ButtonColor = System.Drawing.SystemColors.Control;
			this.ViewStyle.ButtonHotColor = System.Drawing.Color.FromArgb(((System.Byte)(182)), ((System.Byte)(193)), ((System.Byte)(214)));
			this.ViewStyle.ButtonPressedColor = System.Drawing.Color.FromArgb(((System.Byte)(210)), ((System.Byte)(218)), ((System.Byte)(232)));
			this.ViewStyle.ControlBackColor = System.Drawing.SystemColors.Control;
			this.ViewStyle.EditColor = System.Drawing.Color.White;
			this.ViewStyle.EditDisabledColor = System.Drawing.Color.Gainsboro;
			this.ViewStyle.EditFocusedColor = System.Drawing.Color.Beige;
			this.ViewStyle.EditReadOnlyColor = System.Drawing.Color.White;
			this.ViewStyle.FlashColor = System.Drawing.Color.Pink;
			((System.ComponentModel.ISupportInitialize)(this.m_pTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		#region Events handling

		#region function OnPopUp_SelectionChanged

		private void OnPopUp_SelectionChanged(object sender,WDateChanged_EventArgs e)
		{
			this.Value = e.Date;			
		}

		#endregion

		#region function OnPopUp_Closed

		private void OnPopUp_Closed(object sender,System.EventArgs e)
		{
			m_DroppedDown = false;
			m_WDatePickerPopUp.Dispose();
			Invalidate(false);

			if(!this.ContainsFocus){
				this.BackColor = m_ViewStyle.EditColor;
			}
		}

		#endregion


		#region function m_pTextBox_ProccessMessage

		private bool m_pTextBox_ProccessMessage(object sender,ref System.Windows.Forms.Message m)
		{
			if(m_DroppedDown && m_WDatePickerPopUp != null && IsNeeded(ref m)){
				
				// Forward message to PopUp Form
				m_WDatePickerPopUp.PostMessage(ref m);
				return true;
			}

			return false;
		}

		#endregion

		#region function m_pTextBox_OnLostFocus

		private void m_pTextBox_OnLostFocus(object sender, System.EventArgs e)
		{
			if(m_DroppedDown && m_WDatePickerPopUp != null && !m_WDatePickerPopUp.ClientRectangle.Contains(m_WDatePickerPopUp.PointToClient(Control.MousePosition))){
				m_WDatePickerPopUp.Close();
				m_DroppedDown = false;
			}
		}

		#endregion

		#endregion


		#region override OnButtonPressed
        
		protected override void OnButtonPressed()
		{
			if(m_DroppedDown || this.ReadOnly){
				return;
			}	
		
			ShowPopUp();			
		}

		#endregion

		#region override OnPlusKeyPressed
        
		protected override void OnPlusKeyPressed()
		{
			if(m_DroppedDown || this.ReadOnly){
				return;
			}	
		
			ShowPopUp();			
		}

		#endregion

		#region function OnEnterKeyPressed

		protected override void OnEnterKeyPressed()
		{				
			if(m_DroppedDown && m_WDatePickerPopUp != null){
				m_WDatePickerPopUp.RaiseSelectionChanged();
				m_WDatePickerPopUp.Close();
			}
			else{
				OnDateChanged();
			}
		}

		#endregion

		#region override OnEndedInitialize

		public override void OnEndedInitialize()
		{
			base.OnEndedInitialize();

			m_pTextBox.DateValue = DateTime.Today;
		}

		#endregion


		#region function ShowPopUp

		private void ShowPopUp()
		{
			Point pt = this.Parent.PointToScreen(new Point(this.Left,this.Bottom + 1));
			m_WDatePickerPopUp = new WDatePickerPopUp(this,m_ViewStyle,this.Value);
			m_WDatePickerPopUp.SelectionChanged += new DateSelectionChangedHandler(this.OnPopUp_SelectionChanged);
			m_WDatePickerPopUp.Closed += new System.EventHandler(this.OnPopUp_Closed);
	
			Rectangle screenRect = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
			if(screenRect.Bottom < pt.Y + m_WDatePickerPopUp.Height){
				pt.Y = pt.Y - m_WDatePickerPopUp.Height - this.Height - 1;
			}

			if(screenRect.Right < pt.X + m_WDatePickerPopUp.Width){
				pt.X = screenRect.Right - m_WDatePickerPopUp.Width - 2;
			}

			m_WDatePickerPopUp.Location = pt;

			User32.ShowWindow(m_WDatePickerPopUp.Handle,4);

			m_WDatePickerPopUp.m_Start = true;
			m_DroppedDown = true;
		}

		#endregion

		
		#region function IsNeeded

		private bool IsNeeded(ref  System.Windows.Forms.Message m)
		{
			if(m.Msg == (int)Msgs.WM_MOUSEWHEEL){
				return true;
			}

			if(m.Msg == (int)Msgs.WM_KEYUP || m.Msg == (int)Msgs.WM_KEYDOWN){
				return true;
			}

			if(m.Msg == (int)Msgs.WM_CHAR){
				return true;
			}

			return false;
		}

		#endregion


		#region Properties Implementation

		public DateTime Value
		{
			get{ return m_pTextBox.DateValue; }

			set{
				m_pTextBox.DateValue = value;

				OnDateChanged();
			}
		}

		#endregion

		#region Events Implementation

		protected void OnDateChanged()
		{
			if(this.DateChanged != null){
				this.DateChanged(this,new System.EventArgs());
			}
		}

		#endregion
		
	}
}
