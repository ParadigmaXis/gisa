using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA
{
	public class GISAPanel : System.Windows.Forms.UserControl
	{

	#region  Windows Form Designer generated code 

		public GISAPanel() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            this.Visible = false;
            this.MinimumSize = new Size(800, 600);
            this.AutoScroll = true;
            base.VisibleChanged += GisaPanel_VisibleChanged;
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
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			//
			//GISAPanel
			//
			this.Name = "GISAPanel";
			this.Size = new System.Drawing.Size(680, 328);
			

		}

	#endregion

		public MultiPanelControl MultiPanel
		{
			get
			{
				if (this.Parent == null)
					return null;

				return (MultiPanelControl)this.Parent.Parent;
			}
		}

		private bool mIsLoaded;
		public bool IsLoaded
		{
			get {return mIsLoaded;}
			set {mIsLoaded = value;}
		}

		private bool mIsPopulated;
		public bool IsPopulated
		{
			get {return mIsPopulated;}
			set {mIsPopulated = value;}
		}

        private bool mTbBAuxListEventAssigned;
        public bool TbBAuxListEventAssigned
        {
            get {return mTbBAuxListEventAssigned;}
            set {mTbBAuxListEventAssigned = value;}
        }

        public delegate void GenericDelegate();
        private GenericDelegate theGenericDelegate;
        public GenericDelegate TheGenericDelegate
        {
            get { return this.theGenericDelegate; }
            set { this.theGenericDelegate = value; }
        }

		public virtual void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			throw new InvalidOperationException(this.GetType().FullName + " NOT IMPLEMENTED.");
		}

		public virtual void ModelToView()
		{
			throw new InvalidOperationException(this.GetType().FullName + " NOT IMPLEMENTED.");
		}

		public virtual void ViewToModel()
		{
			throw new InvalidOperationException(this.GetType().FullName + " NOT IMPLEMENTED.");
		}

		public virtual void Deactivate()
		{
			throw new InvalidOperationException(this.GetType().FullName + " NOT IMPLEMENTED.");
		}

		public virtual void Save(IDbTransaction Trans)
		{
			throw new InvalidOperationException(this.GetType().FullName + " NOT IMPLEMENTED.");
		}

		private void GisaPanel_VisibleChanged(object Sender, EventArgs e)
		{
			// Most likely a change during component initialization/construction.
			// Ignore it.
			if (this.Parent == null)
				return;

			try
			{
				Control Ctrl = this;
				bool CtrlVisible = true;
				while (Ctrl != null)
				{
					CtrlVisible = CtrlVisible && Ctrl.Visible;
					Ctrl = Ctrl.Parent;
				}
				CtrlVisible = CtrlVisible && (this.TopLevelControl is frmMain);

				if (CtrlVisible)
				{
                    this.Update();
					OnShowPanel();
				}
				else
					OnHidePanel();
			}
			catch (ArgumentException)
			{
				// Ignore this deactivate
			}
		}

		public virtual void OnShowPanel()
		{
			//Override in child classes
		}

		public virtual void OnHidePanel()
		{
			//Override in child classes
		}

		private Size mMinSize = new Size(560, 420);
        public Size MinSize
		{
			get {return mMinSize;}
			set {mMinSize = value;}
		}
	}
}