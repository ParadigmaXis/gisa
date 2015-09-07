namespace TrueSoftware.Windows.Forms 
{
	using System;
	using System.Drawing;
	using System.Resources;
	using System.ComponentModel;
	using System.Windows.Forms;
	/// <summary>
	/// Get TreeNodeCollection collection, Herewith you can add and remove items to the treeview
	/// </summary>
	public class DropDownTreeView : UserControl
	{

		/// <summary>
		/// Get TreeNodeCollection collection, Herewith you can add and remove items to the treeview
		/// </summary>
		public event TreeViewEventHandler AfterSelect;

		/// <summary>
		/// Get TreeNodeCollection collection, Herewith you can add and remove items to the treeview
		/// </summary>
		protected string _GISAFunction;
		
		/// <summary>
		/// Get TreeNodeCollection collection, Herewith you can add and remove items to the treeview
		/// </summary>
		protected Button btnNext;
		/// <summary>
		/// Get TreeNodeCollection collection, Herewith you can add and remove items to the treeview
		/// </summary>
		protected Button btnPrevious;
		
		/// <summary>
		/// Get TreeNodeCollection collection, Herewith you can add and remove items to the treeview
		/// </summary>
		protected Label txtValue;
        /// <summary>
        /// Get TreeNodeCollection collection, Herewith you can add and remove items to the treeview
        /// </summary>
        protected Panel panel;
		/// <summary>
		/// Get TreeNodeCollection collection, Herewith you can add and remove items to the treeview
		/// </summary>
		protected TreeView treeView;
		/// <summary>
		/// Get TreeNodeCollection collection, Herewith you can add and remove items to the treeview
		/// </summary>
		protected bool TextValueSet;
		/// <summary>
		/// Get TreeNodeCollection collection, Herewith you can add and remove items to the treeview
		/// </summary>
		private LabelEx treeLabel;
		/// <summary>
		/// Get TreeNodeCollection collection, Herewith you can add and remove items to the treeview
		/// </summary>
		private new int Margin = 10;
		/// <summary>
		/// Get TreeNodeCollection collection, Herewith you can add and remove items to the treeview
		/// </summary>
		private int tvMinWidth=0;
		/// <summary>
		/// Get TreeNodeCollection collection, Herewith you can add and remove items to the treeview
		/// </summary>
        private int tvMinHeight = 150;
		/// <summary>
		/// Get TreeNodeCollection collection, Herewith you can add and remove items to the treeview
		/// </summary>
		private MouseEventArgs parentMouse;

		/// <summary>
		/// Get TreeNodeCollection collection, Herewith you can add and remove items to the treeview
		/// </summary>
		[
			Description("Gets the TreeView Nodes collection"),
			Category("TreeView")
		]
		public TreeNodeCollection Nodes 
		{
			get 
			{
				return treeView.Nodes;
			}
		}

		/// <summary>
		/// Get,set property that provides the TreeView's Selected Node
		/// </summary>
		[
			Description("Gets or sets the TreeView's Selected Node"),
			Category("TreeView")
		]
		public TreeNode SelectedNode 
		{
			get 
			{
				return treeView.SelectedNode;
			}
			set 
			{
				treeView.SelectedNode = value;
				SetTextValueToSelectedNode();
			}
		}		

		/// <summary>
		/// Get, set property thai is mapped to the Control's treeView control's ImageList property.
		/// </summary>
		public ImageList Imagelist 
		{
			get 
			{
				return treeView.ImageList;
			}
			set 
			{
				treeView.ImageList = value;
			}
		}

		/// <summary>
		/// Get, set property GISAFunction
		/// </summary>
		public string GISAFunction
		{
			get 
			{
				return _GISAFunction;
			}
			set 
			{
				_GISAFunction = value;
			}
		}

		/// <summary>
		/// Control's constuctor
		/// </summary>
		public DropDownTreeView() 
		{

			// General
			TextValueSet = false;

			// Initializing Controls
			InitControls();

			// Setting The locations
			SetSizeAndLocation();

			// Adding Controls to UserControl
			this.Controls.AddRange(new Control[]{btnPrevious,btnNext,txtValue,panel});

			// attaching events
			this.Resize += new EventHandler(ControlResize);
			this.LocationChanged += new EventHandler(ControlResize);

		}

		/// <summary>
		/// Control's resize handler, this handler is attached to the controls Resize and LocationChanged events
		/// </summary>
		/// <param name="sender">sender obcject</param>
		/// <param name="e">default EventArgs</param>
		private void ControlResize(object sender,EventArgs e) 
		{
			// Setting The locations
			SetSizeAndLocation();
		}


		/// <summary>
		/// Controls, sub control initializer, private void that handles the control initialization.
		/// </summary>
		private void InitControls() 
		{
			//value box settings;
			txtValue = new Label();

			this.txtValue.BackColor = System.Drawing.Color.Gray;
			this.txtValue.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.txtValue.Height = this.txtValue.Font.Height + 4;
			this.txtValue.ForeColor = System.Drawing.Color.White;
			this.txtValue.Location = new System.Drawing.Point(0, 0);
			this.txtValue.TextAlign = ContentAlignment.MiddleLeft;
			this.txtValue.Click += new EventHandler(ToggleTreeView);
 
			_GISAFunction = "Titulo";
			
			//select button settings
			btnNext = new Button();
			btnPrevious = new Button();
				
			btnNext.FlatStyle = FlatStyle.Flat;
			btnNext.ForeColor = System.Drawing.Color.White;
			btnNext.BackColor = System.Drawing.Color.Gray;
			btnNext.Click += new EventHandler(NextTreeItem);
			btnNext.Text=">";
			
			btnPrevious.FlatStyle = FlatStyle.Flat;
			btnPrevious.ForeColor = System.Drawing.Color.White;
			btnPrevious.BackColor = System.Drawing.Color.Gray;
			btnPrevious.Click += new EventHandler(PreviousTreeItem);
			btnPrevious.Text="<";

			treeView = new TreeView();
			//treeView.BorderStyle = BorderStyle.FixedSingle;
			treeView.Visible = true;
			treeView.AfterSelect += new TreeViewEventHandler(TreeViewNodeSelect);
		    treeView.Click += new EventHandler(TreeViewNodeDoubleClickSelect);
			treeView.DoubleClick += new EventHandler(TreeViewNodeDoubleClickSelect);
			treeView.MouseMove += new MouseEventHandler(TreeViewMouseMoveEventHandler);
			treeView.Leave += new EventHandler(TreeViewLeave);
			//treeView.Size = new Size(tvMinWidth,tvMinHeight);
            treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			treeView.HideSelection = false;
			treeLabel = new LabelEx();
			treeLabel.Size = new Size(16,16);
			treeLabel.BackColor = Color.Transparent;
			treeView.Controls.Add(treeLabel);

            panel = new Panel();
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Visible = false;
            panel.Size = new Size(tvMinWidth, tvMinHeight);
            panel.Controls.Add(treeView);

			SetHotSpot();

			this.BackColor = System.Drawing.Color.Gray;

			this.SetStyle(ControlStyles.DoubleBuffer,true);
			this.SetStyle(ControlStyles.ResizeRedraw,true);
		}


		/// <summary>
		/// private void for setting the HotSpot helper label.
		/// </summary>
		private void SetHotSpot() 
		{
			treeLabel.Top = treeView.Height - treeLabel.Height;
			treeLabel.Left = treeView.Width - treeLabel.Width;
		}

		/// <summary>
		/// TreeView node select handler, this is a protected event method 
		/// that is attached to the TreeView's AfterSelect event. It sets the 
		/// TextBox's Text property. By default It checks the selected treenode, 
		/// If the treenode dosn't have any child node then, this node's value 
		/// is assigned to the TextBox's Text Property. 
		/// You can implement your own AfterSelect handler by overriding this method.
		/// </summary>
		/// <param name="sender">sender object</param>
		/// <param name="e">default TreeViewEventArgs</param>
		protected void TreeViewNodeSelect(object sender,TreeViewEventArgs e) 
		{
			// chiching for none root node
			if (this.SelectedNode.Tag != null)
			{
				SetTextValueToSelectedNode();
			   AfterSelect(this, new TreeViewEventArgs(this.SelectedNode));
			} else {
				// this causes a reentry on this event handler,
				//  in principle with a non-null Tag
				NextTreeItem(sender, e);
			}
		}

		/// <summary>
		/// This event method is the control's TreeView Leave event handler.
		/// It is meant to close the TreeView when it looses focus.
		/// </summary>
		protected void TreeViewLeave(object sender, EventArgs e)
		{
			if (TextValueSet)
				this.ToggleTreeView(sender, null);
		}

		/// <summary>
		/// This event method is the control's TreeView's DoubleClick event handler.
		/// It is meant to close the TreeView when an item is DoubleClicked.
		/// This function calls the TreeViewNodeSelect event handler method, then if 
		/// the TextValueSet is set to true it toggles (closes) the TreeView
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TreeViewNodeDoubleClickSelect(object sender,EventArgs e) 
		{
			this.TreeViewNodeSelect(sender,null);
			if(TextValueSet) 
			{
				this.ToggleTreeView(sender,null);
			}
		}
		/// <summary>
		/// private void setting the control's TextBox's Text property.
		/// This method also sets the TextValueSet to true if a value 
		/// is assigned to the TextBox's Text property
		/// </summary>
		private void SetTextValueToSelectedNode() 
		{
			try 
			{
				LockNavigationButtons();
				TreeNode n = this.SelectedNode;
				while (n.Parent != null) n = n.Parent;
				txtValue.Text = _GISAFunction + " - " + n.Text;
				//txtValue.Text = _GISAFunction + " - " + this.SelectedNode.Text;
				TextValueSet = true;
				if (AfterSelect != null)
					AfterSelect(this, new TreeViewEventArgs(this.SelectedNode));
			} 
			catch(Exception ex) 
			{
				System.Diagnostics.Trace.WriteLine(ex); 
				TextValueSet = false;
			}
		}

		/// <summary>
		/// private event method, this method is attached to the control's Button's Clcick event.
		/// It handles the show/hide of the control's treeview
		/// </summary>
		/// <param name="sender">sender object</param>
		/// <param name="e">default EventArgs</param>
		private void ToggleTreeView(object sender,EventArgs e) 
		{
			if(!panel.Visible) 
			{
				SetTreeViewSizeAndLocation();
				try 
				{
					Parent.MouseMove += new MouseEventHandler(ParentMouseMoveHandler);
					panel.Visible = true;
					this.Parent.Controls.Add(panel);
					this.panel.BringToFront();
					treeView.Select();
					if(treeView.Width < this.Width || treeView.Height < this.Height) 
					{
						panel.Width = tvMinWidth;
						panel.Height = 150;
						SetTreeViewMinimumSize(); // JMV
						SetHotSpot();
					}
				} 
				catch 
				{
				}
			} 
			else 
			{
				try 
				{
					panel.Visible = false;
                    TextValueSet = false;
					this.Parent.Controls.Remove(panel);
					Parent.MouseMove -= new MouseEventHandler(ParentMouseMoveHandler);				} 
				catch 
				{
				}
			}
		}

		private void NextTreeItem(object sender, EventArgs e)
		{
			if (treeView.Nodes.Count == 0) return;
			TreeNode current = treeView.SelectedNode;
			do {
				if (current == null) {
					current = treeView.Nodes[0];
					btnPrevious.Enabled = false;
				} else {
					if (current.Nodes.Count > 0)
						current = current.Nodes[0];
					else {
						if (current.NextNode != null)
							current = current.NextNode;
						else {
							TreeNode n = FindNextNode(current);
							if (n != null)
								current = n.NextNode;
						}
					}
				}
			} while (current.Tag == null);
			treeView.SelectedNode = current;
			SetTextValueToSelectedNode();
		}

		private TreeNode FindNextNode(TreeNode current) {
			TreeNode n = current;
			while ((n != null) && (n.NextNode == null)) 
			{
				n = n.Parent;
			}
			return n;
		}

		private TreeNode FindPreviousNode(TreeNode current) 
		{
			TreeNode n = current;
			while ((n != null) && (n.PrevNode == null)) 
			{
				n = n.Parent;
			}
			return n;
		}

		private void PreviousTreeItem(object sender,EventArgs e) 
		{
			if (treeView.Nodes.Count == 0) return;
			TreeNode current = treeView.SelectedNode;
			do {
				if (current == null) 
						current = treeView.Nodes[0];
				else
					{
						if (current.PrevNode != null)
							{
								if (current.PrevNode.Nodes.Count > 0)
									current = current.PrevNode.LastNode;
								else
									current = current.PrevNode;
							}
						else
								current = current.Parent;
					}
			} while (current.Tag == null);
			treeView.SelectedNode = current;
			SetTextValueToSelectedNode();
		}

		private void LockNavigationButtons() {
            if (treeView.Nodes.Count > 0)
            {
                btnPrevious.Enabled = treeView.SelectedNode != treeView.Nodes[0] && (treeView.Nodes[0].Nodes.Count == 0 || treeView.SelectedNode != treeView.Nodes[0].Nodes[0]);
                btnNext.Enabled = treeView.SelectedNode.Nodes.Count > 0 ||
                    treeView.SelectedNode.NextNode != null ||
                    FindNextNode(treeView.SelectedNode) != null;
            }
		}
		private void TreeViewMouseMoveEventHandler(object sender,MouseEventArgs e) 
		{
			this.parentMouse = e;
			SetCursor(Cursors.Default);
		}

		private void ParentMouseMoveHandler(object sender,MouseEventArgs e) 
		{
			int tx,ty;
			this.parentMouse = e;

            tx = panel.Left + panel.Width;
            ty = panel.Top + panel.Height;

			SetHotSpot();

			if(e.Button == MouseButtons.Left) 
			{
				Margin = 50;
				treeLabel.BringToFront();
			} 
			else 
			{
				Margin = 4;
			}

			if ((is_in_range(e.Y,ty-Margin,ty+Margin)) && is_in_range(e.X,tx-Margin,tx+Margin) ) 
			{
				SetCursor(Cursors.SizeNWSE);
				if(e.Button == MouseButtons.Left) 
				{
                    panel.Height = e.Y - panel.Top;
                    panel.Width = e.X - panel.Left;
				} 
			}
			else 
			{
				SetCursor(Cursors.Default);
			}
		}

		private void SetCursor(Cursor crs) 
		{
			panel.Cursor = crs;
			Parent.Cursor = crs;
		}

		private bool is_in_range(int rvalue,int start,int end) 
		{
			if((rvalue >= start) && (rvalue <= end)) 
			{
				return true;
			} 
			else 
			{
				return false;
			}
		}

		private int padding { get { return this.btnNext.Right + this.btnPrevious.Left; } }


		/// <summary>
		/// 
		/// </summary>
		public void SetTreeViewMinimumSize() 
		{
			//JMV if((treeView.Width < this.Width) || (treeView.Height < 150)) 
			{
				panel.Width = this.Width - padding;
				panel.Height = 150;
			}
		}

		/// <summary>
		/// private method sets the control's TreeView's size and location.
		/// It is called when the control is resized to moved from it's place
		/// </summary>
		private void SetTreeViewSizeAndLocation() 
		{
			if(panel != null) 
			{
				panel.Location = new Point(this.Left + padding,this.Top + this.Height-0);
                SetTreeViewMinimumSize();
			} 
		}

		private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // DropDownTreeView
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.DropList;
            this.Name = "DropDownTreeView";
            this.Size = new System.Drawing.Size(328, 24);
            this.ResumeLayout(false);

		}

		/// <summary>
		/// private method, sets the controls size and location. 
		/// It sets the control's and sub control's sizes and locations
		/// </summary>
		private void SetSizeAndLocation() 
		{
			tvMinWidth = this.Width;
			btnPrevious.Size = new Size(txtValue.Height-4,txtValue.Height);
			btnNext.Size = btnPrevious.Size;
			
			txtValue.Width = this.Width -2 * btnPrevious.Width - 4;
			txtValue.Location = new Point(this.Width - txtValue.Width,0);
			btnNext.Left = btnPrevious.Right + 1;
			SetTreeViewSizeAndLocation();
			this.Height = txtValue.Height;
		}
	}


	/// <summary>
	/// Extended Label control with user paint.
	/// </summary>
	public class LabelEx : Label 
	{
		/// <summary>
		/// 
		/// </summary>
		public LabelEx() 
		{
			this.SetStyle(ControlStyles.UserPaint,true);
			this.SetStyle(ControlStyles.DoubleBuffer,true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint,true);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e) 
		{
			base.OnPaint(e);
			System.Windows.Forms.ControlPaint.DrawSizeGrip(e.Graphics,Color.Black, 0,0,16,16);
		}
	}	
}
