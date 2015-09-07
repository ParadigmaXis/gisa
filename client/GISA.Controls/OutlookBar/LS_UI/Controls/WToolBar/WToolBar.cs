using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using LumiSoft.UI;

namespace LumiSoft.UI.Controls
{
	/// <summary>
	/// Summary description for WToolBar.
	/// </summary>
	public class WToolBar : System.Windows.Forms.ToolBar
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WToolBar()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

			SetStyle(ControlStyles.UserPaint,true);
			SetStyle(ControlStyles.DoubleBuffer,true);
			SetStyle(ControlStyles.AllPaintingInWmPaint,true);

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
			components = new System.ComponentModel.Container();
		}
		#endregion


		#region function OnPaint

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			Point mPt = this.PointToClient(Control.MousePosition);		
			foreach(ToolBarButton btn in this.Buttons){
				bool hot = btn.Rectangle.Contains(mPt);

				if(btn.Style == ToolBarButtonStyle.Separator){
					DrawSeparator(e.Graphics,btn);
				}
				else{
					DrawButton(e.Graphics,hot,btn.Rectangle,btn);
				}
			}
		}

		#endregion


		#region function DrawButton

		private void DrawButton(Graphics g,bool hot,Rectangle buttonRect,ToolBarButton btn)
		{
			buttonRect = new Rectangle(buttonRect.X,buttonRect.Y,buttonRect.Width-1,buttonRect.Height-1);
			
			Point mPt = this.PointToClient(Control.MousePosition);
			bool mouseInDropDown = false;
			bool pressed = (Control.MouseButtons == MouseButtons.Left && btn.Rectangle.Contains(mPt));

			// Get if mouse is in DropDownButton.
			if(btn.Style == ToolBarButtonStyle.DropDownButton){
				Rectangle dropDownRect = new Rectangle(buttonRect.X+buttonRect.Width-12,buttonRect.Y,12,buttonRect.Height);
				mouseInDropDown = dropDownRect.Contains(mPt);
			}

			if(hot && btn.Enabled){
				// Fill button backround with hot color
				g.FillRectangle(new SolidBrush(Color.FromArgb(182,193,214)),buttonRect);

				// If button is pressed, replace hot color with pressed color in button rect.
				// For DropDownButton, don't draw pressed color for right dropDown section.
				if(pressed && !mouseInDropDown){					
					g.FillRectangle(new SolidBrush(Color.FromArgb(210,218,232)),buttonRect.X,buttonRect.Y,this.ButtonSize.Width,this.ButtonSize.Height);
				}
				
				g.DrawRectangle(new Pen(Color.Black),buttonRect);

				// Draw vertical(|) separator for DropDownButton
				if(btn.Style == ToolBarButtonStyle.DropDownButton){					
					if(!(mouseInDropDown && pressed)){
						g.DrawLine(new Pen(Color.Black),buttonRect.Right-13,buttonRect.Top,buttonRect.Right- 13,buttonRect.Bottom);
					}
				}
			}
			else{
				g.FillRectangle(new SolidBrush(Color.FromArgb(219, 216, 209)),buttonRect);
			}
			
			// Draw image for button
			DrawImage(g,btn.ImageIndex,pressed,!btn.Enabled,buttonRect);

			// If dropDown button drow arrow
			if(btn.Style == ToolBarButtonStyle.DropDownButton){
				DrawArrow(g,btn);
			}			
		}

		#endregion

		#region function DrawImage

		private void DrawImage(Graphics g,int index,bool pressed,bool grayed,Rectangle buttonRect)
		{
			if(this.ImageList != null && this.ImageList.Images.Count > index){
				Image img = this.ImageList.Images[index];

				int iWidth  = this.ImageSize.Width;
				int bWidth  = buttonRect.Height;	
				int iHeight = this.ImageSize.Height;
				buttonRect =  new Rectangle(buttonRect.X+2+(bWidth-iWidth)/2,buttonRect.Y+(bWidth-iHeight)/2,buttonRect.Height,buttonRect.Height);
		
				if(pressed){
					buttonRect = new Rectangle(buttonRect.X+1,buttonRect.Y+1,buttonRect.Height,buttonRect.Height);
				}

				// Darw grayed image
				if(grayed){
					ControlPaint.DrawImageDisabled(g,img,buttonRect.X,buttonRect.Y,Color.Transparent);
				}
				else{
					g.DrawImageUnscaled(img,buttonRect);
				}
			}
		}

		#endregion

		#region function DrawArrow

		private void DrawArrow(Graphics g,ToolBarButton btn)
		{
			int tX = btn.Rectangle.X + btn.Rectangle.Width  - 9;
			int tY = btn.Rectangle.Y + btn.Rectangle.Height - 12;

			// Draw triangle
			if(btn.Enabled){					
				g.FillPolygon(new SolidBrush(Color.Black),new Point[]{new Point(tX,tY),new Point(tX+5,tY),new Point(tX+2,tY+3)});
			}
			else{
				g.FillPolygon(new SolidBrush(Color.FromArgb(166,166,166)),new Point[]{new Point(tX,tY),new Point(tX+5,tY),new Point(tX+2,tY+3)});				
			}
		}
		
		#endregion

		#region function DrawSeparator

		private void DrawSeparator(Graphics g,ToolBarButton btn)
		{
			g.DrawLine(new Pen(Color.FromArgb(166, 166, 166)),btn.Rectangle.X+1,btn.Rectangle.Y+2,btn.Rectangle.X+1,btn.Rectangle.Bottom-4);		
		}

		#endregion


		#region override OnButtonClick

		protected override void OnButtonClick(ToolBarButtonClickEventArgs e)
		{			
			base.OnButtonClick(e);
			this.Invalidate();
		}

		#endregion

		#region override OnMouseUp

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.Invalidate();
		}

		#endregion

	}
}
