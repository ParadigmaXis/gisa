namespace TrueSoftware.Windows.Forms 
{
	using System;
	using System.Drawing;
	using System.Resources;
	using System.ComponentModel;
	using System.Windows.Forms;

	/// <summary>
	/// DropDownTreeView class offers TreeView that is desined to act under a drop down control
	/// You can use this control when you need to select a value from a dynamic TreeView.
	/// <p>
	/// The sub controls of this control are set to protected. You can override the control
	/// to assign values to the subcontrol of change their behaviour.
	/// </p>
	/// 
	/// </summary>
	public class DropDownTreeView : UserControl
	{
		/// <summary>
		/// protected Button control, this is the control's toggle button
		/// </summary>
		protected Button btnSelect;
		/// <summary>
		/// protected TextBox control, this is control's value TextBox.
		/// The TextBox's ReadyOnly false is by default set ti true
		/// </summary>
		protected TextBox txtValue;
		/// <summary>
		/// protected TreeView control, this is the control's TreeView.
		/// </summary>
		protected TreeView treeView;

		/// <summary>
		/// protected value that is set to true of false within the
		///  SetTextValueToSelectedNode method.
		/// </summary>
		protected bool TextValueSet;

		/// <summary>
		/// the little red dot on the Control's TreeView control
		/// </summary>
		private Label treeLabel;

		/// <summary>
		/// This is the margin in pixels for resizing the TreeView
		/// </summary>
		private int Margin = 10;

		/// <summary>
		/// TreeView's minimum width
		/// </summary>
		private int tvMinWidth=0;

		/// <summary>
		/// TreeView's minimum height
		/// </summary>
		private int tvMinHeight = 150;

		/// <summary>
		/// General point to access MouseMove event for resizing the TreeView
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
			}
		}

		/// <summary>
		/// Get, set property of boolean type, this property is mapped to the ReadOnly property of the control's TextBox
		/// </summary>
		[
			Description("Gets or sets the TextBox's ReadOnly state"),
			Category("TextBox")
		]
		public bool TextReadOnly
		{
			set
			{
				txtValue.ReadOnly = value;
			}
			get 
			{
				return txtValue.ReadOnly;
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
			this.Controls.AddRange(new Control[]{btnSelect,txtValue,treeView});

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
			txtValue = new TextBox();
			this.TextReadOnly = true;

			//select button settings
			btnSelect = new Button();
			btnSelect.Font = new Font("System",7);
			btnSelect.Text = "+";
			btnSelect.TextAlign = ContentAlignment.MiddleCenter;
			btnSelect.ClientSize = btnSelect.Size;
			btnSelect.Click += new EventHandler(ToggleTreeView);

			treeView = new TreeView();
			treeView.BorderStyle = BorderStyle.FixedSingle;
			treeView.Visible = false;
			treeView.AfterSelect += new TreeViewEventHandler(TreeViewNodeSelect);
			treeView.DoubleClick += new EventHandler(TreeViewNodeDoubleClickSelect);
			treeView.MouseMove += new MouseEventHandler(TreeViewMouseMoveEventHandler);
			treeView.Size = new Size(tvMinWidth,tvMinHeight);
			treeLabel = new Label();
			treeLabel.Size = new Size(6,6);
			treeLabel.BackColor = Color.Red;
			treeView.Controls.Add(treeLabel);
			SetHotSpot();

			this.SetStyle(ControlStyles.DoubleBuffer,true);
			this.SetStyle(ControlStyles.ResizeRedraw,true);
		}


		/// <summary>
		/// private void for setting the HotSpot helper label.
		/// </summary>
		private void SetHotSpot() 
		{
			treeLabel.Top = treeView.Height - 8;
			treeLabel.Left = treeView.Width - 8;
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
			if(this.SelectedNode.Nodes.Count == 0) 
			{
				SetTextValueToSelectedNode();
			}
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
				txtValue.Text = this.SelectedNode.Text;
				TextValueSet = true;
			} 
			catch 
			{
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
			if(!treeView.Visible) 
			{

				SetTreeViewSizeAndLocation();
				try 
				{
					this.btnSelect.Text = "-";
					this.Parent.Controls.Add(treeView);
					this.treeView.BringToFront();
					treeView.Visible = true;
					treeView.Select();
					Parent.MouseMove += new MouseEventHandler(ParentMouseMoveHandler);
					if(treeView.Width < this.Width || treeView.Height < this.Height) 
					{
						treeView.Width = tvMinWidth;
						treeView.Height = 150;
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
					btnSelect.Text = "+";
					treeView.Visible = false;
					this.Parent.Controls.Remove(treeView);
					Parent.MouseMove -= new MouseEventHandler(ParentMouseMoveHandler);
				} 
				catch 
				{
				}
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

			tx = treeView.Left + treeView.Width;
			ty = treeView.Top + treeView.Height;

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
					treeView.Height = e.Y - treeView.Top;
					treeView.Width = e.X - treeView.Left;
				} 
			}
			else 
			{
				SetCursor(Cursors.Default);
			}
		}

		private void SetCursor(Cursor crs) 
		{
			treeView.Cursor = crs;
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

		public void SetTreeViewMinimumSize() 
		{
			if((treeView.Width < this.Width) || (treeView.Height < 150)) 
			{
				treeView.Width = this.Width;
				treeView.Height = 150;
			}
		}

		/// <summary>
		/// private method sets the control's TreeView's size and location.
		/// It is called when the control is resized to moved from it's place
		/// </summary>
		private void SetTreeViewSizeAndLocation() 
		{
			if(treeView != null) 
			{			
				/*if(blnFlip) 
				{
					treeView.Location = new Point(this.Left,this.Top - treeView.Height-1);
				} 
				else 
				{*/
					treeView.Location = new Point(this.Left,this.Top + this.Height+1);
				//}
			
			} 
		}

		/// <summary>
		/// private method, sets the controls size and location. 
		/// It sets the control's and sub control's sizes and locations
		/// </summary>
		private void SetSizeAndLocation() 
		{
			tvMinWidth = this.Width;
			if(txtValue != null && btnSelect != null) 
			{
				btnSelect.Size = new Size(txtValue.Height,txtValue.Height);
				txtValue.Width = this.Width - btnSelect.Width - 4;
				txtValue.Location = new Point(0,0);
				btnSelect.Left = txtValue.Width + 4;
				SetTreeViewSizeAndLocation();
				this.Height = txtValue.Height;
			}
		}
	}
}