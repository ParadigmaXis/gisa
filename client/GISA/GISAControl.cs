using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class GISAControl : System.Windows.Forms.UserControl
	{

	#region  Windows Form Designer generated code 

		public GISAControl() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
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
		internal System.Windows.Forms.ImageList ImgList;
		protected internal System.Windows.Forms.ToolBar ToolBar;
		protected internal System.Windows.Forms.Panel pnlToolbarPadding;
		protected internal System.Windows.Forms.ToolTip CurrentToolTip;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.ToolBar = new System.Windows.Forms.ToolBar();
			this.ImgList = new System.Windows.Forms.ImageList(this.components);
			this.pnlToolbarPadding = new System.Windows.Forms.Panel();
			this.CurrentToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.pnlToolbarPadding.SuspendLayout();
			this.SuspendLayout();
			//
			//ToolBar
			//
			this.ToolBar.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.ToolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.ToolBar.AutoSize = false;
			this.ToolBar.ButtonSize = new System.Drawing.Size(16, 16);
			this.ToolBar.Divider = false;
			this.ToolBar.Dock = System.Windows.Forms.DockStyle.None;
			this.ToolBar.DropDownArrows = true;
			this.ToolBar.ImageList = this.ImgList;
			this.ToolBar.Location = new System.Drawing.Point(8, 1);
			this.ToolBar.Name = "ToolBar";
			this.ToolBar.ShowToolTips = true;
			this.ToolBar.Size = new System.Drawing.Size(580, 26);
			this.ToolBar.TabIndex = 0;
			//
			//ImgList
			//
			this.ImgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
			this.ImgList.ImageSize = new System.Drawing.Size(16, 16);
			this.ImgList.TransparentColor = System.Drawing.Color.Transparent;
			//
			//pnlToolbarPadding
			//
			this.pnlToolbarPadding.Controls.Add(this.ToolBar);
			this.pnlToolbarPadding.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlToolbarPadding.DockPadding.Left = 8;
			this.pnlToolbarPadding.DockPadding.Right = 8;
			this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 0);
			this.pnlToolbarPadding.Name = "pnlToolbarPadding";
			this.pnlToolbarPadding.Size = new System.Drawing.Size(600, 28);
			this.pnlToolbarPadding.TabIndex = 1;
			//
			//CurrentToolTip
			//
			this.CurrentToolTip.ShowAlways = true;
			//
			//GISAControl
			//
			this.Controls.Add(this.pnlToolbarPadding);
			this.Name = "GISAControl";
			this.Size = new System.Drawing.Size(600, 280);
			this.pnlToolbarPadding.ResumeLayout(false);
			this.ResumeLayout(false);

		}

	#endregion

		private bool mListeningToContextChanges = false;
		public bool ListeningToContextChanges
		{
			get
			{
				return mListeningToContextChanges;
			}
			set
			{
				if (mListeningToContextChanges ^ value)
				{
					if (value)
						addContextChangeHandlers();
					else
						removeContextChangeHandlers();
					mListeningToContextChanges = value;
				}
			}
		}

		public void Recontextualize()
		{
			Recontextualize(null);
		}

		public void Recontextualize(SaveArgs s)
		{
			//quando se está a fechar a aplicação, o timer da pxlistview pode disparar depois de os contextos estarem limpos (e consequentemente lançar uma excepção; só ocorre quando o timer dispara quando a execução está dentro do método que trata o fecho da aplicação)
            if (((frmMain)TopLevelControl) == null || ((frmMain)TopLevelControl).ActiveControl is MasterPanelAdminGlobal || ((frmMain)TopLevelControl).ActiveControl is MasterPanelEstatisticas)
				return;

			((frmMain)TopLevelControl).EnterWaitMode();
			try
			{
				if (isInnerContextValid())
				{
					ViewToModel();
					if (s != null)
					{
                        if (s.psAction != null)
                            s.save = Save(s.psAction, true);
                        else
                            s.save = Save(true);
					}
					else
						Save();
				}

                if (s != null && s.save == PersistencyHelper.SaveResult.unsuccessful)
				{
					((frmMain)TopLevelControl).LeaveWaitMode();
					return;
				}

				Deactivate();

				//Remove-se de memória todas as linhas marcadas com isDeleted=true na BD
				//esta chamada não se encontra imediatamente a seguir ao save pois existem
				//situações que o método recontextualize é chamado propositadamente
				PersistencyHelper.cleanDeletedData();

				if (! (isOuterContextValid()))
				{
					PanelMensagem panel = GetNoContextMessage();
					if (panel != null)
					{
                        this.Visible = true;
						ActivateMessagePanel(panel);
					}                    
				}                
				else
				{
					PanelMensagem panel = null;
					if (isOuterContextDeleted())
					{
						panel = GetDeletedContextMessage();
						ActivateMessagePanel(panel);
						this.Visible = true;
					}
					else if (! (hasReadPermission()))
					{
						panel = GetNoReadPermissionMessage();
						ActivateMessagePanel(panel);
						this.Visible = true;
					}
                    else if (!(isConnected()))
                    {
                        panel = GetNoConnectionMessage();
                        if (panel != null)
                        {
                            this.Visible = true;
                            ActivateMessagePanel(panel);
                        }
                    }
					else
					{
						DeactivateMessagePanel(GetNoContextMessage());

						try
						{
                            RefreshPanelSelection();
							long start = DateTime.Now.Ticks;
							LoadData();
							Trace.WriteLine("LoadData: " + new TimeSpan(DateTime.Now.Ticks - start).ToString());
							start = DateTime.Now.Ticks;
							ModelToView();
							Trace.WriteLine("ModelToView: " + new TimeSpan(DateTime.Now.Ticks - start).ToString());
						}
						catch (Exception ex)
						{
							Trace.WriteLine(ex);
							throw;
						}

						this.Visible = true;
					}
				}
			}
			finally
			{
				((frmMain)TopLevelControl).LeaveWaitMode();
			}
		}

		protected virtual PanelMensagem GetDeletedContextMessage()
		{
			return null;
		}

		protected virtual PanelMensagem GetNoContextMessage()
		{
			return null;
		}

		protected virtual PanelMensagem GetNoReadPermissionMessage()
		{
			return null;
		}

        protected virtual PanelMensagem GetNoConnectionMessage()
        {
            return null;
        }

		// Este metodo deverá ser reimplementado em classes descendentes se 
		// o comportamento típico destas for o de alterar dados associados 
		// aos contexto que recebam. 
		// Os dados são persistidos sempre que for detectada uma mudança de 
		// contexto e o contexto local actual for válido.
		protected virtual bool isInnerContextValid()
		{
			return false;
		}

		protected virtual bool isOuterContextValid()
		{
			return false;
		}

		protected virtual bool isOuterContextDeleted()
		{
			return false;
		}

		protected virtual bool hasReadPermission()
		{
			return true;
		}
        protected virtual bool isConnected()
        {
            return true;
        }

		public virtual void LoadData()
		{
			throw new InvalidOperationException(this.GetType().FullName + ": NOT IMPLEMENTED.");
		}

		public virtual void ModelToView()
		{
			throw new InvalidOperationException(this.GetType().FullName + ": NOT IMPLEMENTED.");
		}

		public virtual bool ViewToModel()
		{
			throw new InvalidOperationException(this.GetType().FullName + ": NOT IMPLEMENTED.");
		}

		public virtual void Deactivate()
		{
			throw new InvalidOperationException(this.GetType().FullName + ": NOT IMPLEMENTED.");
		}

        public virtual PersistencyHelper.SaveResult Save()
		{
			return Save(false);
		}

        public virtual PersistencyHelper.SaveResult Save(bool activateOpcaoCancelar)
		{
			throw new InvalidOperationException(this.GetType().FullName + ": NOT IMPLEMENTED.");
		}

        public PersistencyHelper.SaveResult Save(PostSaveAction postSaveAction, bool activateOpcaoCancelar)
        {
            return PersistencyHelper.save(postSaveAction, activateOpcaoCancelar);
        }

		public frmMain.Context CurrentContext
		{
			get
			{
				if (TopLevelControl != null && TopLevelControl.GetType() == typeof(frmMain))
					return ((frmMain)TopLevelControl).CurrentContext;

				if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
					throw new InvalidOperationException(this.GetType().FullName + " is not contained by " + typeof(frmMain).FullName + ".");

				//nunca deve chegar aqui
				return null;
			}
		}


		// The DataBinding doesn't update the DataSource while the Control has
		// focus. So we manually find what control is focused on the current
		// TopLevelControl (usually Form) and force an EndCurrentEdit on all
		// its bindings.
		public static void EndCurrentEdit(ContainerControl CC)
		{
			while (CC.ActiveControl is ContainerControl)
				CC = (ContainerControl)CC.ActiveControl;

			if (CC.ActiveControl != null)
			{
				foreach (Binding Binding in CC.ActiveControl.DataBindings)
					Binding.BindingManagerBase.EndCurrentEdit();
			}
		}

		public virtual void ActivateMessagePanel(GISAPanel messagePanel)
		{
			// override in child classes
		}

		public virtual void DeactivateMessagePanel(GISAPanel messagePanel)
		{
			// override in child classes
		}

        public virtual void RefreshPanelSelection()
        {
            // override in child classes
        }

		public class SaveArgs : EventArgs
		{
            private PersistencyHelper.SaveResult mSave;
            public Model.PostSaveAction psAction;
            public SaveArgs(PersistencyHelper.SaveResult save) { this.mSave = save; }

            public PersistencyHelper.SaveResult save
            { 
                get {return mSave;}
				set {mSave = value;}
			}
		}

		protected virtual void addContextChangeHandlers()
		{
			// override in child classes
		}

		protected virtual void removeContextChangeHandlers()
		{
			// override in child classes
		}
	}
}