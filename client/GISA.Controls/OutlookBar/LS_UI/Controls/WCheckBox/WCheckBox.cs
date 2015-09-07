using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace LumiSoft.UI.Controls.WCheckBox
{
	/// <summary>
	/// Summary description for WCheckBox.
	/// </summary>
	public class WCheckBox : WControlBase
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public event System.EventHandler CheckedChanged = null;

		private LeftRight m_CheckAlign = LeftRight.Left;
		private Icon      m_Icon       = null;
		private bool      m_Checked    = false;
		private bool      m_ReadOnly   = false;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public WCheckBox()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

			m_Icon = Core.LoadIcon("check.ico");

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
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// WCheckBox
			// 
			this.Name = "WCheckBox";
			this.Size = new System.Drawing.Size(30, 22);
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
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WCheckBox_MouseUp);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.WCheckBox_KeyUp);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}
		#endregion


		#region Events handling
		
		#region function WCheckBox_MouseUp

		private void WCheckBox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{	
			if(!this.ReadOnly){
				if(this.Checked){
					this.Checked = false;
				}
				else{
					this.Checked = true;
				}

				OnCheckedChanged();
			}
		}

		#endregion

		#region function WCheckBox_KeyUp

		private void WCheckBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(!this.ReadOnly && e.KeyData == Keys.Space){
				if(this.Checked){
					this.Checked = false;
				}
				else{
					this.Checked = true;
				}

				OnCheckedChanged();
			}
		}

		#endregion

		#endregion


		#region function DrawControl

		protected override void DrawControl(Graphics g,bool hot)
		{
			Pen pen = new Pen(m_ViewStyle.GetBorderColor(hot));

			// Draw Button Backround
			g.Clear(m_ViewStyle.ControlBackColor);
			
			Rectangle checkRect = GetCheckRect();

			if(this.Focused){
				g.FillRectangle(new SolidBrush(m_ViewStyle.EditFocusedColor),checkRect);
			}
			else{
				g.FillRectangle(new SolidBrush(m_ViewStyle.GetEditColor(this.ReadOnly,this.Enabled)),checkRect);
			}

			//---- Draw icon --------------------------------------------//
			if(m_Icon != null && m_Checked){

				//------ Adjust Icon sizes and location ----------------------------------//
				Rectangle drawRect = new Rectangle(checkRect.X + 2,checkRect.Y + 2,9,9);
				
				Painter.DrawIcon(g,m_Icon,drawRect,!this.Enabled,false);								
			}

			// Draw rect around control
			g.DrawRectangle(pen,checkRect);
		}		

		#endregion

        
		#region function GetCheckRect

		private Rectangle GetCheckRect()
		{
			Rectangle rect;
			if(m_CheckAlign == LeftRight.Left){
				rect = new Rectangle(0,(this.Height - 12)/2,12,12);
			}
			else{
				rect = new Rectangle((this.Width - 9)/2,(this.Height - 9)/2,12,12);
			}			
			return rect;
		}

		#endregion
				
		
		#region Properties Implementation

		public bool Checked
		{
			get{ return m_Checked; }

			set{
				m_Checked = value;
				this.Invalidate(false);				
			}
		}

		public bool ReadOnly
		{
			get{ return m_ReadOnly; }

			set{
				m_ReadOnly = value;
				this.Invalidate(false);
			}
		}
		
		#endregion

		#region Events Implementation

		protected void OnCheckedChanged()
		{
			if(this.CheckedChanged != null){
				this.CheckedChanged(this,new System.EventArgs());
			}
		}

		#endregion

	}
}
