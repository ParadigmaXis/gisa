using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GISA.Controls {
	/// <summary>
	/// Summary description for DoubleProgressBar.
	/// </summary>
	public class DoubleProgressBar : System.Windows.Forms.UserControl {

		private System.ComponentModel.Container components = null;
		public DoubleProgressBar() {
			InitializeComponent();
			this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
			this.calculateMaximumRation();
			this.calculateCurrentRation();
		}

		protected override void Dispose(bool disposing) {
			if ((disposing) && (!(components == null))) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region "Member Fields"
		private Brush mAlternateBackgroundColorBrush = Brushes.WhiteSmoke;
		private Brush mBackgroundColorBrush = Brushes.White;
		private Brush mMaximumColorBrush = new SolidBrush(Color.FromArgb(80, Color.SteelBlue));
		private Brush mCurrentColorBrush = Brushes.SteelBlue;
		private Color mAlternateBackgroundColor = Color.WhiteSmoke;
		private Color mBackgroundColor = Color.White;
		private int mMaximumTransparency = 80;
		private Color mCurrentColor = Color.SteelBlue;
		private int mWorkBarDelay = 100;
		private int mWorkBarWidth = 20;
		private int mWorkBarDoubleWidth = 40;
		private long mCeiling = 100;
		private long mMaximum = 80;
		private long mCurrent = 20;
		private float mMaximumRatio;
		private float mCurrentRatio;
		private bool mIsAnimated = false;
		private int mAnimateState = 0;
        private bool mShowCurrent = true;
		private System.Threading.Thread t;
		#endregion 

		[Category("Appearance"), DefaultValue(typeof(Color), "WhiteSmoke")]
		public Color AlternateBackgroundColor {
			get {
				return mAlternateBackgroundColor;
			}
			set {
				mAlternateBackgroundColor = value;
				this.Invalidate();
				this.Update();
			}
		}

		[Category("Appearance"), DefaultValue(typeof(Color), "White")]
		public Color BackgroundColor {
			get {
				return mBackgroundColor;
			}
			set {
				mBackgroundColor = value;
				this.mBackgroundColorBrush = new SolidBrush(value);
				this.Invalidate();
				this.Update();
			}
		}

		[Category("Appearance"), DefaultValue(typeof(int), "80")]
		public int Transparency {
			get {
				return mMaximumTransparency;
			}
			set {
				mMaximumTransparency = value;
				this.mMaximumColorBrush = new SolidBrush(Color.FromArgb(value, this.mCurrentColor));
				this.Invalidate();
				this.Update();
			}
		}

		[Category("Appearance"), DefaultValue(typeof(Color), "SteelBlue")]
		public Color CurrentColor {
			get {
				return mCurrentColor;
			}
			set {
				mCurrentColor = value;
				this.mCurrentColorBrush = new SolidBrush(value);
				this.mMaximumColorBrush = new SolidBrush(Color.FromArgb(this.mMaximumTransparency, value));
				this.Invalidate();
				this.Update();
			}
		}

		[Category("Appearance"), DefaultValue(typeof(int), "100")]
		public int WorkBarDelay {
			get {
				return mWorkBarDelay;
			}
			set {
				mWorkBarDelay = value;
			}
		}

		[Category("Appearance"), DefaultValue(typeof(int), "20")]
		public int WorkBarWidth {
			get {
				return mWorkBarWidth;
			}
			set {
				mWorkBarWidth = value;
				this.mWorkBarDoubleWidth = value * 2;
			}
		}

        [Category("Appearance"), DefaultValue(typeof(bool), "true")]
        public bool ShowCurrent {
            get {
                return mShowCurrent;
            }
            set {
                mShowCurrent = value;
            }
        }

		private void calculateMaximumRation() {
			this.mMaximumRatio = (float)(this.mMaximum * this.ClientRectangle.Width) / this.mCeiling;
		}

		private void calculateCurrentRation() {
			this.mCurrentRatio = (float)(this.mCurrent * this.ClientRectangle.Width) / this.mCeiling;
		}

		[Category("Behavior"), DefaultValue(typeof(long), "100")]
		public long Ceiling {
			get {
				return this.mCeiling;
			}
			set {
				this.mCeiling = value;
				this.calculateMaximumRation();
				this.calculateCurrentRation();
				this.Invalidate();
				this.Update();
			}
		}

		[Category("Behavior"), DefaultValue(typeof(int), "80")]
		public long Maximum {
			get {
				return this.mMaximum;
			}
			set {
				this.mMaximum = value;
				//quanto mais algarismos existirem maior é o intervalo de actualização
				if (this.mMaximum % (this.mMaximum.ToString().Length * 2) == 0){
					if (mCeiling < mMaximum) {
						Ceiling = mMaximum;
					} else {
						this.calculateMaximumRation();
					}
					this.Invalidate();
					this.Update();
				}
			}
		}

		[Category("Behavior"), DefaultValue(typeof(int), "20")]
		public long Current {
			get {
				return this.mCurrent;
			}
			set {
				this.mCurrent = value;
				//quanto mais algarismos existirem maior é o intervalo de actualização
				if (this.mCurrent % (this.mMaximum.ToString().Length * 2) == 0){
					this.calculateCurrentRation();
					this.Invalidate();
					this.Update();
				}
			}
		}

		private void InitializeComponent() {
			this.Name = "DoubleProgressBar";
			this.Size = new System.Drawing.Size(288, 32);
		}

		protected override void OnPaint(PaintEventArgs pe) {
			pe.Graphics.FillRectangle(mBackgroundColorBrush, ClientRectangle);
			if (mIsAnimated) {
				int loop1 = this.mAnimateState - mWorkBarDoubleWidth;
				while (loop1 < this.ClientRectangle.Width) {
					pe.Graphics.FillRectangle(mAlternateBackgroundColorBrush, this.ClientRectangle.X + loop1, this.ClientRectangle.Y, mWorkBarWidth, this.ClientRectangle.Height);
					loop1 += mWorkBarDoubleWidth;
				}
			}
			pe.Graphics.FillRectangle(mMaximumColorBrush, this.ClientRectangle.X, this.ClientRectangle.Y, this.mMaximumRatio, this.ClientRectangle.Height);
			pe.Graphics.FillRectangle(mCurrentColorBrush, this.ClientRectangle.X, this.ClientRectangle.Y, this.mCurrentRatio, this.ClientRectangle.Height);

            string label;
            if (this.mShowCurrent)
			    label = string.Format("{0} em {1} (máx. {2})", this.mCurrent, this.Maximum, this.mCeiling);
            else
                label = string.Format("{0} em {1}", this.Maximum, this.mCeiling);
			
			Size labelSize = pe.Graphics.MeasureString(label, this.Font).ToSize();
			
			int x = (this.ClientRectangle.Width - labelSize.Width) / 2;
			
			int y = (this.ClientRectangle.Height - labelSize.Height) / 2;
			pe.Graphics.DrawString(label, this.Font, new SolidBrush(this.ForeColor), x, y);
			
			System.Drawing.Drawing2D.GraphicsState gs = pe.Graphics.Save();
			pe.Graphics.SetClip(new RectangleF(this.ClientRectangle.X, this.ClientRectangle.Y, this.mCurrentRatio, this.ClientRectangle.Height));
			pe.Graphics.DrawString(label, this.Font, this.mBackgroundColorBrush, x, y);
			pe.Graphics.Restore(gs);
		}

		public void StartAnimation() {
			if (!(t == null)) {
				throw new InvalidOperationException();
			}
			t = new System.Threading.Thread(new System.Threading.ThreadStart(DoAnimation));
			t.Start();
			this.mIsAnimated = true;
		}

		private void DoAnimation() {
			try { 
				while (true) {
					System.Threading.Thread.Sleep(mWorkBarDelay);
					this.mAnimateState = (this.mAnimateState + 1) % mWorkBarDoubleWidth;
					this.Invalidate();
				}	 
			} catch (System.Threading.ThreadInterruptedException generatedExceptionVariable0) {
				System.Diagnostics.Trace.WriteLine(generatedExceptionVariable0);
			}
		}
		public void StopAnimation() {
			this.mIsAnimated = false;
			t.Interrupt();
			t.Join();
			t = null;
		}
		protected override CreateParams CreateParams {
			get {
				CreateParams cparams;
				cparams = base.CreateParams;
				cparams.ExStyle &= -513;
				cparams.Style &= -8388609;
				cparams.ExStyle |= 0x200;
				return cparams;
			}
		}
	}
}