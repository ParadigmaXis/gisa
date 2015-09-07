using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.SharedResources;
using GISA.Model;

namespace GISA
{
	public class SlavePanelTrusteeUser : GISA.SlavePanelTrustee
	{

	#region  Windows Form Designer generated code 

		public SlavePanelTrusteeUser() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
			TreeNode node = new TreeNode("Detalhes do utilizador");
			this.DropDownTreeView1.Nodes.Insert(0, node);
			node.Tag = PanelTrusteeDetailsUser;

			DropDownTreeView1.SelectedNode = DropDownTreeView1.Nodes[0];
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
		internal GISA.PanelTrusteeDetailsUser PanelTrusteeDetailsUser;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.PanelTrusteeDetailsUser = new GISA.PanelTrusteeDetailsUser();
			this.GisaPanelScroller.SuspendLayout();
			//
			//PanelPermissions
			//
			this.PanelTrusteePermissions.Name = "PanelTrusteePermissions";
			//
			//PanelNivelPermissions
			//
			this.PanelTrusteePermissions.Name = "PanelTrusteeNivelPermissions";
			//
			//DropDownTreeView1
			//
			this.DropDownTreeView1.GISAFunction = "Utilizador";
			this.DropDownTreeView1.Name = "DropDownTreeView1";
			//
			//GisaPanelScroller
			//
			this.GisaPanelScroller.Controls.Add(this.PanelTrusteeDetailsUser);
			this.GisaPanelScroller.Location = new System.Drawing.Point(0, 0);
			this.GisaPanelScroller.Name = "GisaPanelScroller";
			this.GisaPanelScroller.Size = new System.Drawing.Size(600, 280);
			this.GisaPanelScroller.Controls.SetChildIndex(this.PanelTrusteePermissions, 0);
			this.GisaPanelScroller.Controls.SetChildIndex(this.PanelTrusteeDetailsUser, 0);
			//
			//pnlToolbarPadding
			//
			this.pnlToolbarPadding.DockPadding.Left = 6;
			this.pnlToolbarPadding.DockPadding.Right = 6;
			this.pnlToolbarPadding.Name = "pnlToolbarPadding";
			//
			//ToolBar
			//
			this.ToolBar.Name = "ToolBar";
			//
			//PanelTrusteeDetailsUser
			//
			this.PanelTrusteeDetailsUser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelTrusteeDetailsUser.Location = new System.Drawing.Point(0, 0);
			this.PanelTrusteeDetailsUser.Name = "PanelTrusteeDetailsUser";
			this.PanelTrusteeDetailsUser.Size = new System.Drawing.Size(600, 280);
			this.PanelTrusteeDetailsUser.TabIndex = 11;
			//
			//SlavePanelTrusteeUser
			//
			this.Name = "SlavePanelTrusteeUser";
			this.GisaPanelScroller.ResumeLayout(false);

		}

	#endregion

		public static Bitmap FunctionImage
		{
			get
			{
				return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "Utilizadores_enabled_32x32.png");
			}
		}

		public override void LoadData()
		{
			try
			{
				GisaPrincipalPermission tempWith1 = new GisaPrincipalPermission(SessionHelper.GetGisaPrincipal(), this.GetType().FullName, GisaPrincipalPermission.WRITE);
				tempWith1.Demand();
			}
			catch (System.Security.SecurityException)
			{
	#if ! ALLPRODUCTPERMISSIONS
                GUIHelper.GUIHelper.makeReadOnly(this.GisaPanelScroller);
	#endif
			}

			base.LoadData();
		}

		public override void Deactivate()
		{
			SessionHelper.GetGisaPrincipal().RecalculatePrivileges();
			base.Deactivate();
		}

		protected override PanelMensagem GetDeletedContextMessage()
		{
			PanelMensagem1.LblMensagem.Text = "Este utilizador foi removido não sendo por isso possível apresentá-lo.";
			return PanelMensagem1;
		}

		protected override PanelMensagem GetNoContextMessage()
		{
			PanelMensagem1.LblMensagem.Text = "Para visualizar os detalhes deverá selecionar um utilizador no painel superior.";
			return PanelMensagem1;
		}
	}

} //end of root namespace