//INSTANT C# NOTE: Formerly VB.NET project-level imports:
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;

using System.Drawing;
using System.Windows.Forms;

namespace GISA.Model
{
	public class PXMenuItem : MenuItem
	{

		private Bitmap Ico = null;
		private const int NORMALITEMHEIGHT = 20;
		private const int SEPITEMHEIGHT = 12;
		private const int EXTRAWIDTH = 30;
		private Font CurrentFont = new Font("Arial", 8);

		public PXMenuItem(string text) : base(text)
		{
			this.OwnerDraw = true;
			//DONE
			//TODO: INSTANT C# TODO TASK: Insert the following converted event handlers at the end of the 'InitializeComponent' method for forms or into a constructor for other classes:
			base.DrawItem += new DrawItemEventHandler(DrawItemHandler);
			base.MeasureItem += new MeasureItemEventHandler(MesureItemHandler);
		}

		public PXMenuItem(string Text, Bitmap Ico) : base(Text)
		{
			this.Ico = Ico;
			this.OwnerDraw = true;
		}

		public Bitmap Icon
		{
			get
			{
				return Ico;
			}
			set
			{
				Ico = value;
			}
		}

		private void DrawItemHandler(object sender, DrawItemEventArgs e)
		{
			RectangleF rc = new RectangleF((float)(e.Bounds.X), (float)(e.Bounds.Y), (float)(e.Bounds.Width), (float)(e.Bounds.Height));
			e.Graphics.FillRectangle(new SolidBrush(SystemColors.Control), rc);
			MenuItem s = (MenuItem)sender;
			string s1 = s.Text;
			StringFormat sf = new StringFormat();
			RectangleF rcText = rc;
			rcText.Y += 2;
			rcText.X += 24;
			rcText.Width -= 20;
			if (e.State == (DrawItemState.NoAccelerator | DrawItemState.Selected))
			{
				e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight), rc);
				e.Graphics.DrawString(s1, CurrentFont, new SolidBrush(SystemColors.HighlightText), rcText, sf);
				e.DrawFocusRectangle();
			}
			else
			{
				e.Graphics.DrawString(s1, CurrentFont, new SolidBrush(SystemColors.ControlText), rcText, sf);
			}

			if (Ico != null)
			{
				e.Graphics.DrawImage(Ico, System.Convert.ToInt32(e.Bounds.X + 5), System.Convert.ToInt32((e.Bounds.Bottom + e.Bounds.Top) / 2 - Ico.Size.Height / 2));
			}
		}

		private void MesureItemHandler(object sender, MeasureItemEventArgs e)
		{
			//Dim ThisMenuItem_Strings As String() = Me.Text.Split(",")
			SizeF TextSize = e.Graphics.MeasureString(this.Text.Replace("&", ""), CurrentFont);
			e.ItemWidth = System.Convert.ToInt32(TextSize.Width) + EXTRAWIDTH;
			if (this.Text == "-")
			{
				e.ItemHeight = SEPITEMHEIGHT;
			}
			else
			{
				e.ItemHeight = NORMALITEMHEIGHT;
			}
			e.ItemHeight = e.ItemHeight;
			e.ItemWidth = e.ItemWidth;
		}


	}

} //end of root namespace