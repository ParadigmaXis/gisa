using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace LumiSoft.UI.Controls
{
	/// <summary>
	/// Summary description for WFrame.
	/// </summary>
	public class WFrame : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Panel m_pFormPanel;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel ControlPane;
		private System.Windows.Forms.Panel TopPane;
		private System.Windows.Forms.Panel ToolBarPanel;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button m_pSwitchHide;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Form    m_Form      = null;
		private ToolBar m_TooBar    = null;
		private Control m_BarCotrol = null;

		private Color m_SplitterColor    = Color.FromKnownColor(KnownColor.Control);
		private int   m_SplitterWidth    = 5;
		private int   m_SplitterMinSize  = 0;
		private int   m_SplitterMinExtra = 0;
		private int   m_TopPaneHeight    = 25;
		private Color m_TopPaneBkColor   = Color.FromKnownColor(KnownColor.Control);
		private bool  m_IsSplitterLocked = false;		
		private int   m_ControlPaneWidth = 120;
		private int   m_BarWidthCache    = 0;
		private bool  m_HideBar          = false;

		/// <summary>
		/// 
		/// </summary>
		public WFrame()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call
			
			m_pSwitchHide.Visible = false;
		}

		#region function Dispose

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.m_pFormPanel = new System.Windows.Forms.Panel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.ControlPane = new System.Windows.Forms.Panel();
			this.TopPane = new System.Windows.Forms.Panel();
			this.m_pSwitchHide = new System.Windows.Forms.Button();
			this.ToolBarPanel = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.TopPane.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_pFormPanel
			// 
			this.m_pFormPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_pFormPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_pFormPanel.Location = new System.Drawing.Point(103, 25);
			this.m_pFormPanel.Name = "m_pFormPanel";
			this.m_pFormPanel.Size = new System.Drawing.Size(193, 191);
			this.m_pFormPanel.TabIndex = 9;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(100, 25);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 191);
			this.splitter1.TabIndex = 8;
			this.splitter1.TabStop = false;
			this.splitter1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter1_SplitterMoved);
			// 
			// ControlPane
			// 
			this.ControlPane.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ControlPane.Dock = System.Windows.Forms.DockStyle.Left;
			this.ControlPane.Location = new System.Drawing.Point(0, 25);
			this.ControlPane.Name = "ControlPane";
			this.ControlPane.Size = new System.Drawing.Size(100, 191);
			this.ControlPane.TabIndex = 6;
			// 
			// TopPane
			// 
			this.TopPane.Controls.AddRange(new System.Windows.Forms.Control[] {
																				  this.m_pSwitchHide,
																				  this.ToolBarPanel,
																				  this.button1});
			this.TopPane.Dock = System.Windows.Forms.DockStyle.Top;
			this.TopPane.Name = "TopPane";
			this.TopPane.Size = new System.Drawing.Size(296, 25);
			this.TopPane.TabIndex = 7;
			// 
			// m_pSwitchHide
			// 
			this.m_pSwitchHide.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_pSwitchHide.Location = new System.Drawing.Point(56, 8);
			this.m_pSwitchHide.Name = "m_pSwitchHide";
			this.m_pSwitchHide.Size = new System.Drawing.Size(8, 8);
			this.m_pSwitchHide.TabIndex = 2;
			this.m_pSwitchHide.Text = "button2";
			this.m_pSwitchHide.Click += new System.EventHandler(this.m_pSwitchHide_Click);
			// 
			// ToolBarPanel
			// 
			this.ToolBarPanel.Location = new System.Drawing.Point(112, 0);
			this.ToolBarPanel.Name = "ToolBarPanel";
			this.ToolBarPanel.Size = new System.Drawing.Size(184, 24);
			this.ToolBarPanel.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.button1.Location = new System.Drawing.Point(72, 2);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(32, 16);
			this.button1.TabIndex = 0;
			this.button1.Text = "<<";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// WFrame
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.m_pFormPanel,
																		  this.splitter1,
																		  this.ControlPane,
																		  this.TopPane});
			this.Name = "WFrame";
			this.Size = new System.Drawing.Size(296, 216);
			this.TopPane.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		#region function splitter1_SplitterMoved

		/// <summary>
		/// Called after Splitter move, Set new ControlPane
		/// </summary>
		/// <param name="sender"> </param>
		/// <param name="e"> </param>
		protected void splitter1_SplitterMoved(object sender, System.Windows.Forms.SplitterEventArgs e)
		{
			m_ControlPaneWidth = ControlPane.Width;
			
			this.OnResize(new EventArgs());

			// Redraw splitter
			splitter1.Invalidate();			
		}

		#endregion

		#region function button1_Click

		protected void button1_Click(object sender, System.EventArgs e)
		{
			if(ControlPane.Width > 0){
				HideControlBar();	
			}
			else{
				ShowControlBar();
							
				if(m_BarCotrol != null){
					m_BarCotrol.Focus();
				}
			}

			this.OnResize(new EventArgs());
		}

		#endregion

		#region function OnBarControl_LostFocus

		private void OnBarControl_LostFocus(object sender,System.EventArgs e)
		{
	/*		Point mPt = this.PointToClient(Control.MousePosition);

			if(m_HideBar && !m_pSwitchHide.Bounds.Contains(mPt)){
				HideControlBar();

				this.OnResize(new EventArgs());
			}*/
		}

		#endregion

		#region function m_pSwitchHide_Click

		private void m_pSwitchHide_Click(object sender, System.EventArgs e)
		{
			
		}

		#endregion


		#region function ShowBar

		private void ShowControlBar()
		{
			this.ControlPaneWitdh = m_BarWidthCache;
			splitter1.Visible     = true;
	//		m_pSwitchHide.Visible = true;
				

			button1.Text = "<<";
		}

		#endregion

		#region function HideBar

		private void HideControlBar()
		{
			m_BarWidthCache       = this.ControlPaneWitdh;
			this.ControlPaneWitdh = 0;
			splitter1.Width       = 0;
			splitter1.Visible     = false;
	//		m_pSwitchHide.Visible = false;
				                
			button1.Text = ">>";		
		}

		#endregion

		
		#region override OnResize

		/// <summary>
		/// Resize panel control
		/// </summary>
		/// <param name="e"> </param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			if(this.ControlPaneWitdh > 0){
				ToolBarPanel.Left  = this.ControlPaneWitdh + this.SplitterWidth + 4;
				button1.Left       = this.ControlPaneWitdh - button1.Width;
			}
			else{
				ToolBarPanel.Left = button1.Width + 5;
				button1.Left      = 1;
			}
		
		//	if(m_BarCotrol != null){
		//		m_BarCotrol.Size = ControlPane.ClientSize;
		//	}
			
			// Resize form
			if(m_Form != null){				
				m_Form.Size = m_pFormPanel.ClientSize;				
			}
		}

		#endregion

		
		#region Properties Implementation

		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public Form Frame_Form
		{
			get{ return m_Form; }

			set{
				if(value != null){
					// If there is old Form, Dispose it.
					if(m_Form != null){
						m_Form.Dispose();
					}

					m_Form = value;
					
					m_Form.Location        = new Point(0,0);
					m_Form.TopLevel        = false;
					m_Form.TopMost         = false;
					m_Form.ControlBox      = false;
					m_Form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
					m_Form.StartPosition   = System.Windows.Forms.FormStartPosition.Manual;
					m_Form.Size            = m_pFormPanel.ClientSize;
					m_Form.Parent          = m_pFormPanel;
					m_Form.Visible         = true;
					m_Form.Focus();
				}
			}
		}

		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public ToolBar Frame_TooBar
		{
			get{ return m_TooBar; }

			set{
				if(value != null){
					// If there is old ToolBar, Dispose it.
					if(m_TooBar != null){
						m_TooBar.Dispose();
					}
					m_TooBar         = value;
					m_TooBar.Parent  = ToolBarPanel;
					m_TooBar.Visible = true;
				}
			}
		}

		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public Control Frame_BarControl
		{
			get{ return m_BarCotrol; }

			set{
				if(value != null && value.Handle.ToInt32() != 0){
					// If there is old bar Control, Dispose it.
					if(m_BarCotrol != null){
						m_BarCotrol.Dispose();
					}
				
					m_BarCotrol        = value;	
					ControlPane.Controls.Add(m_BarCotrol);
			
					// Set Control Location and Size 
					m_BarCotrol.Location = new Point(0,0);
					m_BarCotrol.Dock     = DockStyle.Fill;
				
					m_BarCotrol.Visible = true;

		//			m_BarCotrol.LostFocus += new System.EventHandler(this.OnBarControl_LostFocus);
				}
			}
		}

        
		//--- Splitter Window properties ---------------------------------
		[
		Category("SplitterWnd"),
		Description("Splitter window color"),
		DefaultValue(System.Drawing.KnownColor.Control),
		]
		public Color SplitterColor 
		{
			get{ return m_SplitterColor; }

			set {
				m_SplitterColor = value;
				splitter1.BackColor = m_SplitterColor;
				Invalidate();							
			}
		}



		[
		Category("SplitterWnd"),
		Description("Splitter window Width"),
		DefaultValue(5),
		]
		public int SplitterWidth 
		{
			get{ return m_SplitterWidth; }

			set{
				m_SplitterWidth = value;
				splitter1.Width = m_SplitterWidth;
				Invalidate();							
			}
		}


		[
		Category("SplitterWnd"),
		Description("Splitter window MinSize"),
		DefaultValue(25),
		]
		public int SplitterMinSize 
		{
			get{ return m_SplitterMinSize; }

			set{
				m_SplitterMinSize = value;
				splitter1.MinSize = m_SplitterMinSize;
				Invalidate();							
			}
		}



		[
		Category("SplitterWnd"),
		Description("Splitter window MinExtra"),
		DefaultValue(25),
		]
		public int SplitterMinExtra
		{
			get{ return m_SplitterMinExtra; }

			set{
				m_SplitterMinExtra = value;
				splitter1.MinExtra = m_SplitterMinExtra;
				Invalidate();							
			}
		}


		[
		Category("SplitterWnd"),
		Description("Locks Splitter window"),
		DefaultValue(false),
		]
		public bool LockSplitter
		{
			get{ return m_IsSplitterLocked; }

			set{ 
				m_IsSplitterLocked = value;
				splitter1.Enabled = !m_IsSplitterLocked;
				Invalidate();							
			}
		}
        
		//--- End of SplitterWnd Porperties --------------------------------



		//--- Top Pane properties ------------------------------------------
		[
		Category("TopPane"),
		Description("Top pane window Height"),
		DefaultValue(20),
		]
		public int TopPaneHeight
		{
			get{ return m_TopPaneHeight; }

			set{
				m_TopPaneHeight = value;
				TopPane.Height = m_TopPaneHeight;
				button1.Top = m_TopPaneHeight - button1.Height - 3;
				Invalidate();							
			}
		}


		[
		Category("TopPane"),
		Description("Top Pane BackRound color"),		
		]
		public Color TopPaneBkColor 
		{
			get{ return m_TopPaneBkColor; }

			set 
			{
				m_TopPaneBkColor = value;
				TopPane.BackColor = m_TopPaneBkColor;
				Invalidate();							
			}
		}		
		//--- Top Pane properties --------------------------------



		//--- Control Pane properties ------------------------------------------
		[
		Category("ControlPane"),
		Description("Control pane Width"),		
		]
		public int ControlPaneWitdh
		{
			get{ return m_ControlPaneWidth; }

			set{
				m_ControlPaneWidth = value;
				ControlPane.Width  = m_ControlPaneWidth;
				ToolBarPanel.Left  = m_ControlPaneWidth + this.SplitterWidth + 4;
				button1.Left       = m_ControlPaneWidth - button1.Width;
						
				if(m_BarCotrol != null){
					m_BarCotrol.Width = m_ControlPaneWidth - 5;
				}
						
				Invalidate();
										
			}
		}

		[
		Category("ControlPane"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public bool HideBar
		{
			get{ return m_HideBar; }

			set{ 
				m_HideBar = value; 

				if(!this.DesignMode){
					if(value){
						HideControlBar();
					}
					else{
						ShowControlBar();
					}
				}
			}
		}

		//--- Control Pane properties --------------------------------

		#endregion		

	}
}
