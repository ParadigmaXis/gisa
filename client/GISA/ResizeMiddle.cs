using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA
{
	public class ResizeMiddle
	{

		public enum ResizeOrientation: int
		{
			Horizontal = 1,
			Vertical = 2
		}


		private Control A;
		private Control B;
		private Control C;
		private Control BaseControl;
		private ResizeOrientation Orientation;

		public ResizeMiddle(Control BaseControl, Control A, Control B): this(BaseControl, A, B, ResizeOrientation.Horizontal)
		{
            BaseControl.Resize += OnResize;
		}


		public ResizeMiddle(Control BaseControl, Control A, Control B, ResizeOrientation Orientation) : base()

		{
			this.BaseControl = BaseControl;
			this.A = A;
			this.B = B;
			this.Orientation = Orientation;
		}


		public ResizeMiddle(Control BaseControl, Control A, Control B, Control C): this(BaseControl, A, B, C, ResizeOrientation.Horizontal)
		{
		}

		public ResizeMiddle(Control BaseControl, Control A, Control B, Control C, ResizeOrientation Orientation) : base()

		{
			this.BaseControl = BaseControl;
			this.A = A;
			this.B = B;
			this.C = C;
			this.Orientation = Orientation;
		}

		private void OnResize(object sender, EventArgs e)
		{
			if (C == null)
			{
				//2 args

				if (this.Orientation == ResizeOrientation.Horizontal)
				{
					A.Width = (BaseControl.Width - A.Left * 3) / 2;
					B.Width = A.Width;
					B.Left = BaseControl.Width - A.Left - B.Width;
				}
				else
				{
					A.Height = (BaseControl.Height - A.Top * 3) / 2;
					B.Height = A.Height;
					B.Top = BaseControl.Height - A.Top - B.Height;
				}
			}
			else
			{
				//3 args
				if (this.Orientation == ResizeOrientation.Horizontal)
				{
					A.Width = (BaseControl.Width - A.Left) / 3;
					B.Width = A.Width;
					C.Width = A.Width;
					B.Left = BaseControl.Width - A.Left - B.Width;
					C.Left = BaseControl.Width - A.Left - B.Width - C.Width;
				}
				else
				{
					A.Height = (BaseControl.Height - A.Top) / 3;
					B.Height = A.Height;
					C.Height = A.Height;
					B.Top = BaseControl.Height - A.Top - B.Height;
					C.Top = BaseControl.Height - A.Top - B.Height - C.Height;
				}

			}

		}
	}

} //end of root namespace