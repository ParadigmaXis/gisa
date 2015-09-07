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
	public class SlavePanelTrusteeGroup : GISA.SlavePanelTrustee
	{

	#region  Windows Form Designer generated code 

		public SlavePanelTrusteeGroup() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
			TreeNode node = new TreeNode("Detalhes do grupo");
			this.DropDownTreeView1.Nodes.Insert(0, node);
			node.Tag = PanelTrusteeDetailsGroup;

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
		internal GISA.PanelTrusteeDetailsGroup PanelTrusteeDetailsGroup;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.PanelTrusteeDetailsGroup = new GISA.PanelTrusteeDetailsGroup();
			this.GisaPanelScroller.SuspendLayout();
			//
			//PanelPermissions
			//
			this.PanelTrusteePermissions.Name = "PanelPermissions";
			//
			//DropDownTreeView1
			//
			this.DropDownTreeView1.GISAFunction = "Grupo de utilizadores";
			this.DropDownTreeView1.Name = "DropDownTreeView1";
			//
			//GisaPanelScroller
			//
			this.GisaPanelScroller.Controls.Add(this.PanelTrusteeDetailsGroup);
			this.GisaPanelScroller.Location = new System.Drawing.Point(0, 0);
			this.GisaPanelScroller.Name = "GisaPanelScroller";
			this.GisaPanelScroller.Size = new System.Drawing.Size(600, 280);
			this.GisaPanelScroller.Controls.SetChildIndex(this.PanelTrusteePermissions, 0);
			this.GisaPanelScroller.Controls.SetChildIndex(this.PanelTrusteeDetailsGroup, 0);
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
			//PanelTrusteeDetailsGroup
			//
			this.PanelTrusteeDetailsGroup.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelTrusteeDetailsGroup.Location = new System.Drawing.Point(0, 0);
			this.PanelTrusteeDetailsGroup.Name = "PanelTrusteeDetailsGroup";
			this.PanelTrusteeDetailsGroup.Size = new System.Drawing.Size(600, 280);
			this.PanelTrusteeDetailsGroup.TabIndex = 10;
			//
			//SlavePanelTrusteeGroup
			//
			this.Name = "SlavePanelTrusteeGroup";
			this.GisaPanelScroller.ResumeLayout(false);

		}

	#endregion

		public static Bitmap FunctionImage
		{
			get
			{
				return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "GrupoUtilizadores_enabled_32x32.png");
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

		protected override PanelMensagem GetDeletedContextMessage()
		{
			PanelMensagem1.LblMensagem.Text = "Este grupo de utilizadores foi removido não sendo por isso possível apresentá-lo.";
			return PanelMensagem1;
		}

		protected override PanelMensagem GetNoContextMessage()
		{
			PanelMensagem1.LblMensagem.Text = "Para visualizar os detalhes deverá selecionar um grupo de utilizadores no painel superior.";
			return PanelMensagem1;
		}

	}

} //end of root namespace