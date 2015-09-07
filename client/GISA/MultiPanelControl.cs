using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public abstract class MultiPanelControl : GISA.GISAControl
	{

		public bool existsModifiedData = false;

	#region  Windows Form Designer generated code 

		public MultiPanelControl() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            DropDownTreeView1.AfterSelect += DropDownTreeView1_AfterSelect;
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
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.    
		protected internal TrueSoftware.Windows.Forms.DropDownTreeView DropDownTreeView1;
		internal System.Windows.Forms.ImageList ImageList1;
		protected internal GISA.GISAPanelScroller GisaPanelScroller;
		protected internal System.Windows.Forms.ToolBarButton ToolBarButtonAuxList;
		protected internal System.Windows.Forms.ToolBarButton ToolBarButtonFiltro;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiPanelControl));
            this.DropDownTreeView1 = new TrueSoftware.Windows.Forms.DropDownTreeView();
            this.ToolBarButtonAuxList = new System.Windows.Forms.ToolBarButton();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.GisaPanelScroller = new GISA.GISAPanelScroller();
            this.ToolBarButtonFiltro = new System.Windows.Forms.ToolBarButton();
            this.pnlToolbarPadding.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolBar
            // 
            this.ToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.ToolBarButtonAuxList,
            this.ToolBarButtonFiltro});
            this.ToolBar.ImageList = this.ImageList1;
            this.ToolBar.Location = new System.Drawing.Point(6, 1);
            this.ToolBar.Size = new System.Drawing.Size(584, 26);
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 23);
            this.pnlToolbarPadding.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            // 
            // DropDownTreeView1
            // 
            this.DropDownTreeView1.BackColor = System.Drawing.Color.Gray;
            this.DropDownTreeView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.DropDownTreeView1.ForeColor = System.Drawing.Color.Gray;
            this.DropDownTreeView1.GISAFunction = "Título";
            this.DropDownTreeView1.Imagelist = null;
            this.DropDownTreeView1.Location = new System.Drawing.Point(0, 0);
            this.DropDownTreeView1.Name = "DropDownTreeView1";
            this.DropDownTreeView1.SelectedNode = null;
            this.DropDownTreeView1.Size = new System.Drawing.Size(600, 23);
            this.DropDownTreeView1.TabIndex = 7;
            // 
            // ToolBarButtonAuxList
            // 
            this.ToolBarButtonAuxList.ImageIndex = 1;
            this.ToolBarButtonAuxList.Name = "ToolBarButtonAuxList";
            this.ToolBarButtonAuxList.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.ToolBarButtonAuxList.ToolTipText = "Apresentar/esconder painel de apoio";
            this.ToolBarButtonAuxList.Visible = false;
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.ImageList1.Images.SetKeyName(0, "");
            this.ImageList1.Images.SetKeyName(1, "");
            this.ImageList1.Images.SetKeyName(2, "");
            this.ImageList1.Images.SetKeyName(3, "");
            // 
            // GisaPanelScroller
            // 
            this.GisaPanelScroller.BackColor = System.Drawing.SystemColors.Control;
            this.GisaPanelScroller.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GisaPanelScroller.Location = new System.Drawing.Point(0, 51);
            this.GisaPanelScroller.Name = "GisaPanelScroller";
            this.GisaPanelScroller.Size = new System.Drawing.Size(600, 229);
            this.GisaPanelScroller.TabIndex = 8;
            // 
            // ToolBarButtonFiltro
            // 
            this.ToolBarButtonFiltro.ImageIndex = 3;
            this.ToolBarButtonFiltro.Name = "ToolBarButtonFiltro";
            this.ToolBarButtonFiltro.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.ToolBarButtonFiltro.ToolTipText = "Apresentar/Esconder campos de filtro";
            this.ToolBarButtonFiltro.Visible = false;
            // 
            // MultiPanelControl
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.GisaPanelScroller);
            this.Controls.Add(this.DropDownTreeView1);
            this.Name = "MultiPanelControl";
            this.Controls.SetChildIndex(this.DropDownTreeView1, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.Controls.SetChildIndex(this.GisaPanelScroller, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

        private void DropDownTreeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node == null)
            {
                Trace.WriteLine(string.Format("{0}: panel switching without selected node.", this.GetType().FullName));
                return;
            }

            Trace.WriteLine(string.Format("{0}: panel switching.", this.GetType().FullName));

            if (e.Node.Tag == null) return;

            try
            {
                GISAPanel panel = (GISAPanel)e.Node.Tag;
                // Avoid multiple calls on the same selected TreeNode.
                // This had a performance hit when the panel
                //  caused reactivation of a master panel multiple times.
                if (panel.Visible)
                {
                    Trace.WriteLine(string.Format("{0}: panel {1} already active.", this.GetType().FullName, e.Node.Tag.GetType().FullName));
                    return;
                }

                SortedList DocumentControls = new SortedList();
                CollectDocumentControls(DropDownTreeView1.Nodes, DocumentControls);

                // esconder controlos
                DocumentControls.Values.OfType<GISAPanel>().ToList().ForEach(p => p.Visible = false);

                //carregar dados do painel e populá-los
                LoadData();
                ModelToView();

                // mostrar os controlos novos
                panel.Visible = true;

                Trace.WriteLine(string.Format("{0}: switching to non-null panel {1}.", this.GetType().FullName, e.Node.Tag.GetType().FullName));
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }


        }

		private void CollectDocumentControls(TreeNodeCollection Nodes, SortedList DocumentControls)
		{
			foreach (TreeNode n in Nodes)
			{
				if (n.Tag != null)
				{
	#if DEBUG
					try
					{
						DocumentControls.Add(((Control)n.Tag).TabIndex, n.Tag);
					}
					catch (ArgumentException ex)
					{
						Debug.Assert(false, "Repeated tab indexes were found.");
					}
	#else
					DocumentControls.Add(((Control)n.Tag).TabIndex, n.Tag);
	#endif
				}
				if (n.Nodes.Count > 0)
				{
					CollectDocumentControls(n.Nodes, DocumentControls);
				}
			}
		}


        public override PersistencyHelper.SaveResult Save()
		{
			return Save(false);
		}

        public override PersistencyHelper.SaveResult Save(bool activateOpcaoCancelar)
		{
			return PersistencyHelper.save(activateOpcaoCancelar);
		}

		public override void ActivateMessagePanel(GISAPanel messagePanel)
		{
			this.pnlToolbarPadding.Visible = false;
			this.DropDownTreeView1.Visible = false;

			SortedList DocumentControls = new SortedList();
			CollectDocumentControls(DropDownTreeView1.Nodes, DocumentControls);

            DocumentControls.Values.OfType<GISAPanel>().ToList().ForEach(p => p.Visible = false);
			messagePanel.Visible = true;
		}

		public override void DeactivateMessagePanel(GISAPanel messagePanel)
		{
			this.pnlToolbarPadding.Visible = true;
			this.DropDownTreeView1.Visible = true;
			if (messagePanel != null) // este caso acontece se estivermos num local em que simplesmente não seja necessário um messagepanel (e por isso não exista um)
                messagePanel.Visible = false;
		}

        public override void RefreshPanelSelection()
        {
            if (DropDownTreeView1.SelectedNode != null)
            {
                TreeNode tmpNode = DropDownTreeView1.SelectedNode;
                DropDownTreeView1.SelectedNode = tmpNode;
            }
        }

		protected void DeactivatePanels()
		{
			GISAPanel panel = null;
			SortedList DocumentControls = new SortedList();
			CollectDocumentControls(DropDownTreeView1.Nodes, DocumentControls);
			foreach (Control ctrl in DocumentControls.Values)
			{
				try
				{
					panel = (GISAPanel)ctrl;
					if (panel.IsLoaded)
					{
						panel.Deactivate();
						panel.IsLoaded = false;
						panel.IsPopulated = false;
					}
				}
				catch (InvalidCastException)
				{
					// Ignore ctrl, not a GisaPanel
				}
			}
		}

		public override bool ViewToModel()
		{
			GISAControl.EndCurrentEdit((ContainerControl)this.TopLevelControl);

			GISAPanel panel = null;
			SortedList DocumentControls = new SortedList();
			CollectDocumentControls(DropDownTreeView1.Nodes, DocumentControls);
			foreach (Control ctrl in DocumentControls.Values)
			{
				try
				{
					panel = (GISAPanel)ctrl;
					if (panel.IsLoaded)
					{
						((GISAPanel)ctrl).ViewToModel();
					}
				}
				catch (InvalidCastException)
				{
					// Ignore ctrl, not a GisaPanel
				}
			}
			return true;
		}

        protected virtual void RegisterModification(object o, RegisterModificationEventArgs e)
		{

		}
	}

} //end of root namespace