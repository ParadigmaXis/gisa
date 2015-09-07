using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class SlavePanelTrustee : GISA.MultiPanelControl
	{

	#region  Windows Form Designer generated code 

		public SlavePanelTrustee() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
			TreeNode tempWith1 = this.DropDownTreeView1.Nodes.Add("Permissões atribuídas");
			tempWith1.Tag = PanelTrusteePermissions;
		}

		//Form overrides dispose to clean up the component list.
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
		protected internal GISA.PanelTrusteePermissions PanelTrusteePermissions;
		internal GISA.PanelMensagem PanelMensagem1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.PanelTrusteePermissions = new GISA.PanelTrusteePermissions();
			this.PanelMensagem1 = new PanelMensagem();
			this.GisaPanelScroller.SuspendLayout();
			//
			//DropDownTreeView1
			//
			this.DropDownTreeView1.Name = "DropDownTreeView1";
			//
			//GisaPanelScroller
			//
			this.GisaPanelScroller.Controls.Add(this.PanelMensagem1);
			this.GisaPanelScroller.Controls.Add(this.PanelTrusteePermissions);
			this.GisaPanelScroller.Location = new System.Drawing.Point(0, 51);
			this.GisaPanelScroller.Name = "GisaPanelScroller";
			this.GisaPanelScroller.Size = new System.Drawing.Size(600, 229);
			//
			//pnlToolbarPadding
			//
			this.pnlToolbarPadding.DockPadding.Left = 6;
			this.pnlToolbarPadding.DockPadding.Right = 6;
			this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 23);
			this.pnlToolbarPadding.Name = "pnlToolbarPadding";
			//
			//ToolBar
			//
			this.ToolBar.Name = "ToolBar";
			//
			//PanelPermissions
			//
			this.PanelTrusteePermissions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelTrusteePermissions.Location = new System.Drawing.Point(0, 0);
			this.PanelTrusteePermissions.Name = "PanelTrusteePermissions";
			this.PanelTrusteePermissions.Size = new System.Drawing.Size(600, 229);
			this.PanelTrusteePermissions.TabIndex = 8;
			this.PanelTrusteePermissions.Visible = false;
			//
			//PanelMensagem1
			//
			this.PanelMensagem1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelMensagem1.IsLoaded = false;
			this.PanelMensagem1.Location = new System.Drawing.Point(0, 0);
			this.PanelMensagem1.Name = "PanelMensagem1";
			this.PanelMensagem1.Size = new System.Drawing.Size(600, 364);
			this.PanelMensagem1.TabIndex = 22;
			this.PanelMensagem1.Visible = false;
			//
			//SlavePanelTrustee
			//
			this.Name = "SlavePanelTrustee";
			this.GisaPanelScroller.ResumeLayout(false);

		}

	#endregion

		private GISADataset.TrusteeRow CurrentTrustee;
		public override void LoadData()
		{
			//AddHandler CurrentContext.TrusteeChanged, AddressOf TrusteeChanged
			if (CurrentContext.Trustee == null)
			{
				CurrentTrustee = null;
				return;
			}

			this.CurrentTrustee = CurrentContext.Trustee;

			IDbConnection conn = GisaDataSetHelper.GetConnection();
			try
			{
				conn.Open();
				GISAPanel selectedPanel = (GISAPanel)this.DropDownTreeView1.SelectedNode.Tag;
				if (! selectedPanel.IsLoaded)
					selectedPanel.LoadData(CurrentTrustee, conn);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw;
			}
			finally
			{
				conn.Close();
			}
		}

		public override void ModelToView()
		{
			// se nao existir um contexto definido os slavepanels não devem ser apresentados
			if (CurrentTrustee == null || CurrentTrustee.RowState == DataRowState.Detached || CurrentContext.Trustee.RowState == DataRowState.Detached)
			{
				this.Visible = false;
				return;
			}

			this.Visible = true;
			GISAPanel selectedPanel = (GISAPanel)this.DropDownTreeView1.SelectedNode.Tag;
			if (selectedPanel.IsLoaded && ! selectedPanel.IsPopulated)
				selectedPanel.ModelToView();
			try
			{
				GisaPrincipalPermission tempWith1 = new GisaPrincipalPermission(SessionHelper.GetGisaPrincipal(), this.GetType().FullName, GisaPrincipalPermission.WRITE);
				tempWith1.Demand();
			}
			catch (System.Security.SecurityException)
			{
                GUIHelper.GUIHelper.makeReadOnly(this.GisaPanelScroller);
			}
		}

		public override void Deactivate()
		{
			DeactivatePanels();
			CurrentTrustee = null;
		}

		protected override bool isInnerContextValid()
		{
			return CurrentTrustee != null && ! (CurrentTrustee.RowState == DataRowState.Detached) && CurrentTrustee.isDeleted == 0;
		}

		protected override bool isOuterContextValid()
		{
			return CurrentContext.Trustee != null;
		}

		protected override bool isOuterContextDeleted()
		{
			Debug.Assert(CurrentContext.Trustee != null, "CurrentContext.Trustee Is Nothing");
			return CurrentContext.Trustee.RowState == DataRowState.Detached;
		}

		protected override void addContextChangeHandlers()
		{
			CurrentContext.TrusteeChanged += this.Recontextualize;
		}

		protected override void removeContextChangeHandlers()
		{
			CurrentContext.TrusteeChanged -= this.Recontextualize;
		}
	}
}