using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA
{
	public class MasterPanelPermissoesModulos : GISA.MasterPanel
	{

	#region  Windows Form Designer generated code 

		public MasterPanelPermissoesModulos() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            trvwFuncionalidades.BeforeSelect += treeviews_BeforeSelect;

			LoadGroupsAndFunctions();
			SelectFirstNode();
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
		internal System.Windows.Forms.GroupBox grpFuncionalidades;
		internal System.Windows.Forms.TreeView trvwFuncionalidades;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.trvwFuncionalidades = new System.Windows.Forms.TreeView();
			this.grpFuncionalidades = new System.Windows.Forms.GroupBox();
			this.pnlToolbarPadding.SuspendLayout();
			this.grpFuncionalidades.SuspendLayout();
			this.SuspendLayout();
			//
			//lblFuncao
			//
			this.lblFuncao.Location = new System.Drawing.Point(0, 0);
			this.lblFuncao.Name = "lblFuncao";
			this.lblFuncao.Text = "Permissões por Módulo";
			//
			//pnlToolbarPadding
			//
			this.pnlToolbarPadding.DockPadding.Left = 5;
			this.pnlToolbarPadding.DockPadding.Right = 5;
			this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
			this.pnlToolbarPadding.Name = "pnlToolbarPadding";
			//
			//ToolBar
			//
			this.ToolBar.Name = "ToolBar";
			//
			//trvwFuncionalidades
			//
			this.trvwFuncionalidades.Dock = System.Windows.Forms.DockStyle.Fill;
			this.trvwFuncionalidades.HideSelection = false;
			this.trvwFuncionalidades.ImageIndex = -1;
			this.trvwFuncionalidades.Location = new System.Drawing.Point(3, 16);
			this.trvwFuncionalidades.Name = "trvwFuncionalidades";
			this.trvwFuncionalidades.SelectedImageIndex = -1;
			this.trvwFuncionalidades.Size = new System.Drawing.Size(594, 209);
			this.trvwFuncionalidades.TabIndex = 2;
			//
			//grpFuncionalidades
			//
			this.grpFuncionalidades.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.grpFuncionalidades.Controls.Add(this.trvwFuncionalidades);
			this.grpFuncionalidades.Location = new System.Drawing.Point(0, 52);
			this.grpFuncionalidades.Name = "grpFuncionalidades";
			this.grpFuncionalidades.Size = new System.Drawing.Size(600, 228);
			this.grpFuncionalidades.TabIndex = 3;
			this.grpFuncionalidades.TabStop = false;
			this.grpFuncionalidades.Text = "Funcionalidades";
			//
			//MasterPanelPermissoesModulos
			//
			this.Controls.Add(this.grpFuncionalidades);
			this.Name = "MasterPanelPermissoesModulos";
			this.Controls.SetChildIndex(this.grpFuncionalidades, 0);
			this.Controls.SetChildIndex(this.lblFuncao, 0);
			this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
			this.pnlToolbarPadding.ResumeLayout(false);
			this.grpFuncionalidades.ResumeLayout(false);
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		private void LoadGroupsAndFunctions()
		{
            List<TreeNode> nodesToBeAdded = new List<TreeNode>();
            Dictionary<GISADataset.TipoFunctionRow, GISADataset.TipoFunctionRow> tfRows = new Dictionary<GISADataset.TipoFunctionRow, GISADataset.TipoFunctionRow>();

            string modules = string.Empty;
            foreach (GISADataset.ModulesRow mr in SessionHelper.AppConfiguration.GetCurrentAppconfiguration().Modules)
            {
                modules += mr.ID + ",";
            }
            modules.TrimEnd(',');

            foreach (GISADataset.ProductFunctionRow pfRow in GisaDataSetHelper.GetInstance().ProductFunction.Select(
                string.Format("IDTipoServer={0} AND IDModule IN ({1})",
                    SessionHelper.AppConfiguration.GetCurrentAppconfiguration().TipoServer.ID, modules)))
            {
                tfRows.Add(pfRow.TipoFunctionRowParent, pfRow.TipoFunctionRowParent);
            }

            foreach (GISADataset.TipoFunctionGroupRow tfg in GisaDataSetHelper.GetInstance().TipoFunctionGroup.Select("GuiOrder>0", "GuiOrder"))
            {
                foreach (GISADataset.TipoFunctionRow tf in tfg.GetTipoFunctionRows())
                {
                    if (tfRows.ContainsKey(tf))
                    {
                        TreeNode node = new TreeNode(tf.Name);
                        node.Tag = tf;
                        nodesToBeAdded.Add(node);
                    }
                }

                if (nodesToBeAdded.Count > 0)
                {
                    TreeNode n = null;
                    n = trvwFuncionalidades.Nodes.Add(tfg.Name);
                    n.Tag = tfg;
                    n.Nodes.AddRange(nodesToBeAdded.ToArray());
                    nodesToBeAdded.Clear();
                    n.Expand();
                }
            }
		}

		private void SelectFirstNode()
		{
			if (trvwFuncionalidades.Nodes.Count > 0)
				trvwFuncionalidades.SelectedNode = trvwFuncionalidades.Nodes[0];
		}

		private void treeviews_BeforeSelect(object Sender, TreeViewCancelEventArgs e)
		{
			TreeNode node = null;
			node = e.Node;

			if (node != null)
			{
				GISADataset.TipoFunctionRow tfRow = null;
				if (node.Tag is GISADataset.TipoFunctionRow)
				{
					tfRow = (GISADataset.TipoFunctionRow)node.Tag;
					UpdateContext(tfRow);
				}
				else
					UpdateContext(tfRow);
			}
		}

        public override bool UpdateContext()
		{
			bool successfulSave = false;
			GISADataset.TipoFunctionRow row = null;
			TreeNode node = null;
			if (trvwFuncionalidades.SelectedNode != null)
			{
				node = trvwFuncionalidades.SelectedNode;
				if (node.Tag is GISADataset.TipoFunctionRow)
				{
					row = (GISADataset.TipoFunctionRow)node.Tag;
					successfulSave = UpdateContext(row);
				}
				else
				{
					row = null;
					successfulSave = UpdateContext(row);
				}
			}
			else
			{
				row = null;
				successfulSave = UpdateContext(row);
			}
            return successfulSave;
		}

		public bool UpdateContext(GISADataset.TipoFunctionRow row)
		{
			return CurrentContext.SetTipoFunction(row);
		}
	}

} //end of root namespace