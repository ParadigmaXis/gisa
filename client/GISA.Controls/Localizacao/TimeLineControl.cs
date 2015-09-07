using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA.Controls.Localizacao
{
	public class TimeLineControl : System.Windows.Forms.UserControl
	{

	#region  Windows Form Designer generated code 

		public TimeLineControl() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
			this.ResizeRedraw = true;
			Markers = null;
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
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		internal System.Windows.Forms.ToolTip ttTimeLine;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.ttTimeLine = new System.Windows.Forms.ToolTip(this.components);
			//
			//TimeLineControl
			//
			this.Name = "TimeLineControl";
			this.Size = new System.Drawing.Size(368, 32);

		}

	#endregion

	#region  Events and EventArgs derived classes 
		public class CurrentMarkerCancelEventArgs : System.ComponentModel.CancelEventArgs
		{
			public System.DateTime Marker;
			public CurrentMarkerCancelEventArgs(System.DateTime Marker) : base()
			{
				this.Marker = Marker;
			}
		}

		public class CurrentMarkerEventArgs : System.ComponentModel.CancelEventArgs
		{
			public System.DateTime Marker;
			public CurrentMarkerEventArgs(System.DateTime Marker) : base()
			{
				this.Marker = Marker;
			}
		}

		public delegate void CurrentMarkerChangingEventHandler(object sender, CurrentMarkerCancelEventArgs e);
		public event CurrentMarkerChangingEventHandler CurrentMarkerChanging;
		public delegate void CurrentMarkerChangedEventHandler(object sender, CurrentMarkerEventArgs e);
		public event CurrentMarkerChangedEventHandler CurrentMarkerChanged;
	#endregion

	#region  Design-time properties 
		private void InitEmpty()
		{
			mMarkers = new System.DateTime[] {DateTime.MinValue, DateTime.MaxValue};
			mCurrent = mMarkers[0];
			if (CurrentMarkerChanged != null)
				CurrentMarkerChanged(this, new CurrentMarkerEventArgs(mCurrent));
		}
		private System.DateTime[] mMarkers;
		[System.ComponentModel.Category("Data")]
		public System.DateTime[] Markers
		{
			get
			{
				return mMarkers;
			}
			set
			{
				mMarkers = value;
				if (mMarkers == null)
				{
					InitEmpty();
					return;
				}
				// o array de entrada deve reservar duas posições para as pseudodatas -infinito e +infinito
				// logo se o array possui menos de 3 entradas, deve ser ignorado
				if (mMarkers.Length < 3)
				{
					InitEmpty();
				}
				else
				{
					Array.Sort(mMarkers, mMarkers.GetLowerBound(0) + 1, mMarkers.Length - 2);
//Nitro TODO: INSTANT C# TODO TASK: The return type of the tempWith1 variable must be corrected.
//ORIGINAL LINE: With mMarkers(mMarkers.GetLowerBound(0) + 1)
					DateTime tempWith1 = mMarkers[mMarkers.GetLowerBound(0) + 1];
					mMarkers[mMarkers.GetLowerBound(0)] = new System.DateTime(tempWith1.Year - 1, tempWith1.Month, tempWith1.Day);
//Nitro TODO: INSTANT C# TODO TASK: The return type of the tempWith2 variable must be corrected.
//ORIGINAL LINE: With mMarkers(mMarkers.GetUpperBound(0) - 1)
                    DateTime tempWith2 = mMarkers[mMarkers.GetUpperBound(0) - 1];
					mMarkers[mMarkers.GetUpperBound(0)] = new System.DateTime(tempWith2.Year + 1, tempWith2.Month, tempWith2.Day);
				}
			}
		}

		public void AddMarker(System.DateTime Marker)
		{
			if (mMarkers == null)
			{
				Markers = new System.DateTime[] {Marker};
				CurrentMarker = Marker;
			}
			else
			{
				int i = Array.IndexOf(mMarkers, Marker);

				if (i > mMarkers.GetLowerBound(0) & i < mMarkers.GetUpperBound(0))
				{
					return;
				}

				Array.Resize(ref mMarkers, mMarkers.Length + 1);
				mMarkers[mMarkers.GetUpperBound(0)] = mMarkers[mMarkers.GetUpperBound(0) - 1];
				mMarkers[mMarkers.GetUpperBound(0) - 1] = Marker;
				Array.Sort(mMarkers, mMarkers.GetLowerBound(0) + 1, mMarkers.Length - 2);
//Nitro TODO: INSTANT C# TODO TASK: The return type of the tempWith1 variable must be corrected.
//ORIGINAL LINE: With mMarkers(mMarkers.GetLowerBound(0) + 1)
                DateTime tempWith1 = mMarkers[mMarkers.GetLowerBound(0) + 1];
				mMarkers[mMarkers.GetLowerBound(0)] = new System.DateTime(tempWith1.Year - 1, tempWith1.Month, tempWith1.Day);
//Nitro TODO: INSTANT C# TODO TASK: The return type of the tempWith2 variable must be corrected.
//ORIGINAL LINE: With mMarkers(mMarkers.GetUpperBound(0) - 1)
                DateTime tempWith2 = mMarkers[mMarkers.GetUpperBound(0) - 1];
				mMarkers[mMarkers.GetUpperBound(0)] = new System.DateTime(tempWith2.Year + 1, tempWith2.Month, tempWith2.Day);
			}
			Invalidate();
			Update();
		}


		private System.DateTime mCurrent;
		[System.ComponentModel.Category("Data")]
		public System.DateTime CurrentMarker
		{
			get
			{
				return mCurrent;
			}
			set
			{
				CurrentMarkerCancelEventArgs e = new CurrentMarkerCancelEventArgs(value);
				if (CurrentMarkerChanging != null)
					CurrentMarkerChanging(this, e);

				if (e.Cancel)
				{
					return;
				}
				mCurrent = value;
				if (CurrentMarkerChanged != null)
					CurrentMarkerChanged(this, new CurrentMarkerEventArgs(value));
				Invalidate();
				Update();
			}
		}

		private System.DateTime mLowExtended;
		[System.ComponentModel.Category("Data")]
		public System.DateTime LowExtendedMarker
		{
			get
			{
				return mLowExtended;
			}
			set
			{
				mLowExtended = value;
				Invalidate();
				Update();
			}
		}

		private System.DateTime mHighExtended;
		[System.ComponentModel.Category("Data")]
		public System.DateTime HighExtendedMarker
		{
			get
			{
				return mHighExtended;
			}
			set
			{
				mHighExtended = value;
				Invalidate();
				Update();
			}
		}

		// FIXME
		// To optimize the component, cache the rectangles used by the drawing and clicking logic.
		// They should be generated on Property Markers Set method.
		//Private mIntervals As Rectangle()
		//Private mSeparators As Rectangle()

		private Color mCurrentIntervalColor;
		[System.ComponentModel.Description("The top color, used to highlight user selection"), System.ComponentModel.Category("Appearance")]
		public Color CurrentIntervalColor
		{
			get
			{
				return mCurrentIntervalColor;
			}
			set
			{
				mCurrentIntervalColor = value;
				this.Invalidate();
				this.Update();
			}
		}

		private Color mExtendedIntervalColor;
		[System.ComponentModel.Description("The bottom color, used to highlight tree selection"), System.ComponentModel.Category("Appearance")]
		public Color ExtendedIntervalColor
		{
			get
			{
				return mExtendedIntervalColor;
			}
			set
			{
				mExtendedIntervalColor = value;
				this.Invalidate();
				this.Update();
			}
		}

		private Color mMarkerColor;
		[System.ComponentModel.Category("Appearance")]
		public Color MarkerColor
		{
			get
			{
				return mMarkerColor;
			}
			set
			{
				mMarkerColor = value;
				this.Invalidate();
				this.Update();
			}
		}

	#endregion

	#region  Data range logic 

		public bool IsDateInRange(System.DateTime Value)
		{
			int idx = Array.IndexOf(mMarkers, mCurrent);
			bool AboveLowerMark = false;
			bool BellowUpperMark = false;
			if (idx == mMarkers.GetLowerBound(0))
			{
				AboveLowerMark = true;
			}
			else if (idx > mMarkers.GetLowerBound(0) & idx < mMarkers.GetUpperBound(0))
			{
				AboveLowerMark = mMarkers[idx].CompareTo(Value) <= 0;
			}
			else // impossível?
			{
				AboveLowerMark = false;
			}
			if (idx + 1 == mMarkers.GetUpperBound(0))
			{
				BellowUpperMark = true;
			}
			else if (idx + 1 > mMarkers.GetLowerBound(0) & idx + 1 < mMarkers.GetUpperBound(0))
			{
				BellowUpperMark = mMarkers[idx + 1].CompareTo(Value) > 0;
			}
			else // impossível?
			{
				BellowUpperMark = false;
			}
			return AboveLowerMark && BellowUpperMark;
		}

		public string GetIsDateInRangeSqlConstraint(string LowerField, string UpperField)
		{
			int idx = Array.IndexOf(mMarkers, mCurrent);
			string AboveLowerMark = null;
			string BellowUpperMark = null;
			if (idx == mMarkers.GetLowerBound(0))
			{
				AboveLowerMark = "";
			}
			else if (idx > mMarkers.GetLowerBound(0) & idx < mMarkers.GetUpperBound(0))
			{
				AboveLowerMark = LowerField + "<='" + mMarkers[idx].ToShortDateString() + "'";
			}
			else // impossível?
			{
				AboveLowerMark = "";
			}
			if (idx == mMarkers.GetUpperBound(0))
			{
				BellowUpperMark = "";
			}
			else if (idx + 1 > mMarkers.GetLowerBound(0) & idx + 1 < mMarkers.GetUpperBound(0))
			{
				BellowUpperMark = UpperField + ">'" + mMarkers[idx].ToShortDateString() + "'";
			}
			else // impossível?
			{
				BellowUpperMark = "";
			}
			if (AboveLowerMark.Length > 0)
			{
				AboveLowerMark = "(" + AboveLowerMark + " OR " + LowerField + " IS NULL)";
			}
			if (BellowUpperMark.Length > 0)
			{
				BellowUpperMark = "(" + BellowUpperMark + " OR " + UpperField + " IS NULL)";
			}
			if (AboveLowerMark.Length > 0 & BellowUpperMark.Length > 0)
			{
				return "(" + AboveLowerMark + " AND " + BellowUpperMark + ")";
			}
			else if (AboveLowerMark.Length > 0 | BellowUpperMark.Length > 0)
			{
				return "(" + AboveLowerMark + BellowUpperMark + ")";
			}
			else
			{
				return "";
			}
		}
	#endregion

	#region Drawing rules

		private int mPadding = 4;
		private Rectangle mMarker = new Rectangle(0, 0, 4, 10);

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);
			ControlPaint.DrawBorder3D(e.Graphics, this.ClientRectangle, Border3DStyle.Sunken);
			e.Graphics.DrawLine(new Pen(Color.Black), mPadding, mPadding + mMarker.Height / 2, Width - mPadding, mPadding + mMarker.Height / 2);

			if (mMarkers == null)
			{
				return;
			}
			if (mMarkers.Length <= 2)
			{
				return;
			}

			Rectangle r1 = Rectangle.Empty;
				Rectangle r2 = Rectangle.Empty;
			int idx = Array.IndexOf(mMarkers, mCurrent);
				int idx2 = 0;
			if (idx >= mMarkers.GetLowerBound(0) && idx < mMarkers.GetUpperBound(0))
			{
				r1 = DateTimePlacement(mMarkers[idx]);
				r2 = DateTimePlacement(mMarkers[idx + 1]);
				DrawCurrent(e.Graphics, r1, r2);
			}
			idx = Array.IndexOf(mMarkers, mLowExtended);
			idx2 = Array.IndexOf(mMarkers, mHighExtended);
			if (idx >= mMarkers.GetLowerBound(0) && idx <= mMarkers.GetUpperBound(0) && idx2 >= mMarkers.GetLowerBound(0) && idx2 <= mMarkers.GetUpperBound(0))
			{
				r1 = DateTimePlacement(mMarkers[idx]);
				r2 = DateTimePlacement(mMarkers[idx2]);
				DrawExtended(e.Graphics, r1, r2);
			}

			for (int i = mMarkers.GetLowerBound(0) + 1; i < mMarkers.GetUpperBound(0); i++)
			{
				DrawMarker(e.Graphics, DateTimePlacement(mMarkers[i]));
			}
		}

		private Rectangle DateTimePlacement(System.DateTime CurrentMarker)
		{
			long range = 0;

			if (mMarkers == null)
			{
				return Rectangle.Empty;
			}

			range = mMarkers[mMarkers.GetUpperBound(0)].Subtract(mMarkers[mMarkers.GetLowerBound(0)]).Days;
			if (range == 0 || range == Int64.MaxValue)
			{
				return Rectangle.Empty;
			}
			Rectangle r = Rectangle.Inflate(mMarker, 0, 0);
			r.Offset(System.Convert.ToInt32(mPadding + (this.Width - r.Width - mPadding * 2) * CurrentMarker.Subtract(mMarkers[0]).Days / range), mPadding);
			return r;
		}


		private void DrawMarker(Graphics g, Rectangle r)
		{
			DrawMarker(g, r, false);
		}

//INSTANT C# NOTE: C# does not support optional parameters. Overloaded method(s) are created above.
//ORIGINAL LINE: Private Sub DrawMarker(ByVal g As Graphics, ByVal r As Rectangle, Optional ByVal Filled As Boolean = false)
		private void DrawMarker(Graphics g, Rectangle r, bool Filled)
		{
			g.FillEllipse(new SolidBrush(mMarkerColor), r);
			//If Filled Then g.FillEllipse(New SolidBrush(mHighlightColor), r)
			g.DrawEllipse(new Pen(this.ForeColor), r);
		}

		private void DrawCurrent(Graphics g, Rectangle r, Rectangle rr)
		{
			Rectangle rrr = Rectangle.Union(r, rr);
			rrr.Inflate(-mMarker.Width / 2, 0);
			rrr.Height = mMarker.Height / 2;
			g.FillRectangle(new SolidBrush(mCurrentIntervalColor), rrr);
		}

		private void DrawExtended(Graphics g, Rectangle r, Rectangle rr)
		{
			Rectangle rrr = Rectangle.Union(r, rr);
			rrr.Inflate(-mMarker.Width / 2, 0);
			rrr.Height = mMarker.Height / 2;
			rrr.Offset(0, rrr.Height + 1);
			g.FillRectangle(new SolidBrush(mExtendedIntervalColor), rrr);
		}

	#endregion

	#region  Interaction event handlers 
		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			if (mMarkers == null)
			{
				return;
			}
			//#If DEBUG Then
			//        For i As Integer = mMarkers.GetLowerBound(0) To mMarkers.GetUpperBound(0)
			//#Else
			for (int i = mMarkers.GetLowerBound(0) + 1; i < mMarkers.GetUpperBound(0); i++)
			{
				//#End If
				if (DateTimePlacement(mMarkers[i]).Contains(e.X, e.Y))
				{
					this.ttTimeLine.SetToolTip(this, mMarkers[i].Date.ToString(System.Globalization.DateTimeFormatInfo.CurrentInfo.ShortDatePattern));
					break;
				}
			}
		}

		private Point mLastClick;
		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			mLastClick = new Point(e.X, e.Y);
		}

		// FIXME Invalidate only affected rectangles
		protected override void OnClick(System.EventArgs e)
		{
			if (mMarkers == null)
			{
				return;
			}
			if (mMarkers.Length <= 2)
			{
				return;
			}

			System.DateTime mOldCurrent = mCurrent;
			int i = 0;

			// Erase old interval
			i = Array.IndexOf(mMarkers, mCurrent);
			if (i >= mMarkers.GetLowerBound(0) && i <= mMarkers.GetUpperBound(0))
			{
				//Me.Invalidate(Rectangle.Union(DateTimePlacement(mMarkers(i)), DateTimePlacement(mMarkers(i + 1))))
			}

			// Paint new interval
			for (i = mMarkers.GetLowerBound(0); i < mMarkers.GetUpperBound(0); i++)
			{
				Rectangle r = Rectangle.Empty;
				//If DateTimePlacement(mMarkers(i)).Contains(mLastClick.X, mLastClick.Y) Then
				if (DateTimePlacement(mMarkers[i]).X < mLastClick.X & DateTimePlacement(mMarkers[i + 1]).X > mLastClick.X)
				{
					//Me.Invalidate(Rectangle.Union(DateTimePlacement(mMarkers(i)), DateTimePlacement(mMarkers(i + 1))))
					CurrentMarker = mMarkers[i];
				}
			}

			this.Invalidate();
			this.Update();
		}
	#endregion

	}


} //end of root namespace