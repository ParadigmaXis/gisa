using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace LumiSoft.UI.Controls
{
	/// <summary>
	/// Summary description for WLabel.
	/// </summary>
	public class WLabel : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private string      m_Text           = "";
		//private HzAlignment m_TextAlignment  = HzAlignment.Center;
		private Color       m_BorderColor    = Color.DarkGray;
		private Color       m_BorderHotColor = Color.Green;
		private bool        m_DrawHot        = false;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public WLabel()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

			SetStyle(ControlStyles.ResizeRedraw,true);
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
			// 
			// WLabel
			// 
			this.Name = "WLabel";
			this.Size = new System.Drawing.Size(150, 24);
			this.MouseEnter += new System.EventHandler(this.WLabel_MouseEnter);
			this.MouseLeave += new System.EventHandler(this.WLabel_MouseLeave);

		}
		#endregion


		#region function OnPaint

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Rectangle rect = new Rectangle(this.ClientRectangle.Location,new Size(this.ClientRectangle.Width - 1,this.ClientRectangle.Height - 1));

	//		Painter.DrawBorder(e.Graphics,

			if(m_DrawHot && IsMouseInControl()){
				Pen pen = new Pen(m_BorderHotColor);
				
				// Draw rect around control
				e.Graphics.DrawRectangle(pen,rect);
			}

			SizeF txtSize = e.Graphics.MeasureString(m_Text,this.Font);
	//	DrawString(txtSize.Width.ToString(),e.Graphics,10,10);		
		//	if(txtSize.Width > this.Width){
				string[] lines = GetVisibleLines(m_Text,e.Graphics,this.Font);
				int x = 0;
				int y = 0;
				foreach(string line in lines){
					DrawString(line,e.Graphics,x,y);
					y += (int)txtSize.Height;
				}
		//	}
		//	else{
		//		DrawString(this.Text,e.Graphics,0,10);
		//	}
					
		//	e.Graphics.DrawString(m_Text,this.Font,new SolidBrush(Color.Black),textRect);
		}

		#endregion

		private void DrawString(string text,Graphics g,int x,int y)
		{
			SizeF txtSize = g.MeasureString(text,this.Font);

			x = (int)(this.Width - txtSize.Width)/2;

			g.DrawString(text,this.Font,new SolidBrush(Color.Black),x,y);
		}

		#region function GetLines

		private string[] GetVisibleLines(string text,Graphics g,Font font)
		{
			//---- Split to words -----------------------//
			string[] wordsArray = null;			
			wordsArray = text.Split(new char[]{' '});			
			//-------------------------------------------//
			
			ArrayList vLines = new ArrayList();
			string buff = "";			
			foreach(string word in wordsArray){				
				if(g.MeasureString(buff + word,font).Width > this.Width){

					// If text in buffer is smaller than drawing area.
					// Usually constructed line. Eg. 'aaa bbb ccc'
					string str = buff.TrimEnd();			   
					if(g.MeasureString(str,font).Width < this.Width && buff.Length > 0){
						vLines.Add(str);

						buff = word;
					}
					// If text in buffer is bigger than control.
					// Add maximum number of letters to line and carry on next line
					else{
						if(buff.Length > 0){
							buff += " ";
						}
						buff += word;

						// Construct lines by splitting word
						while(true){

							//-- Take maximum chars for line ----------------------//
							int i = 0;
							for(i=0;i<buff.Length;i++){								
								if(g.MeasureString(buff.Substring(0,i),font).Width > this.Width){
									break;
								}
							}
							
							string splittedLine = buff.Substring(0,i);

							// Splitted line doesn't cover all line, some room is left.
							if(g.MeasureString(buff.Substring(0,i),font).Width < this.Width){
								buff = splittedLine;
								break;
							}
							else{
								// Add line
								vLines.Add(splittedLine);

								buff = buff.Substring(i);
							}
							//----------------------------------------------------//
						}
					}					
				}
				else{
					if(buff.Length > 0){
						buff += " ";					
					}				
					buff += word;
				}								
			}

			if(buff.Length > 0){
				vLines.Add(buff);
			}

			string[] retVal = new string[vLines.Count];
			vLines.CopyTo(retVal);
			return retVal;
		}

		#endregion

		#region function IsMouseInButtonRect

		private bool IsMouseInControl()
		{
			Rectangle rectButton = this.ClientRectangle;
			Point mPos = Control.MousePosition;
			if(rectButton.Contains(this.PointToClient(mPos))){
				return true;
			}
			else{
				return false;
			}
		}

		#endregion


		#region Events handling

		#region function WLabel_MouseEnter

		private void WLabel_MouseEnter(object sender, System.EventArgs e)
		{
			this.Invalidate();
		}

		#endregion

		#region function WLabel_MouseLeave

		private void WLabel_MouseLeave(object sender, System.EventArgs e)
		{
			this.Invalidate();
		}

		#endregion

		#endregion


		#region Properties Implementation

		/// <summary>
		/// 
		/// </summary>
		[
		Browsable(true),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
		]
		public override string Text
		{
			get{ return m_Text; }

			set{
				m_Text = value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool DrawHot
		{
			get{ return m_DrawHot; }

			set{ m_DrawHot = value; }
		}

		#endregion

	}
}
