using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using LumiSoft.UI.Controls.WOutlookBar;

namespace TestUI
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private LumiSoft.UI.Controls.WFrame wFrame1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ImageList imageList1;

		private LumiSoft.UI.Controls.WOutlookBar.WOutlookBar outlookBar = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			InitBar();

			wFrame1.Frame_BarControl = outlookBar;
			wFrame1.Frame_Form = new Form1(wFrame1);
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.wFrame1 = new LumiSoft.UI.Controls.WFrame();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// wFrame1
			// 
			this.wFrame1.ControlPaneWitdh = 140;
			this.wFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wFrame1.Name = "wFrame1";
			this.wFrame1.Size = new System.Drawing.Size(664, 485);
			this.wFrame1.SplitterColor = System.Drawing.SystemColors.Control;
			this.wFrame1.SplitterMinExtra = 0;
			this.wFrame1.SplitterMinSize = 0;
			this.wFrame1.TabIndex = 0;
			this.wFrame1.TopPaneBkColor = System.Drawing.SystemColors.Control;
			this.wFrame1.TopPaneHeight = 25;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(664, 485);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.wFrame1});
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.ResumeLayout(false);

		}
		#endregion


		#region OutlookBar Click stuff

		/// <summary>
		/// OutlookBar Click
		/// </summary>
		private void wOutlookBar_ItemClicked(object sender, LumiSoft.UI.Controls.WOutlookBar.ItemClicked_EventArgs e)
		{
			MessageBox.Show(e.Item.Caption);
		}

		#endregion


		#region function InitBar

		private void InitBar()
		{
			outlookBar = new LumiSoft.UI.Controls.WOutlookBar.WOutlookBar();
			outlookBar.ImageList = this.imageList1;
			outlookBar.ItemClicked += new LumiSoft.UI.Controls.WOutlookBar.ItemClickedEventHandler(this.wOutlookBar_ItemClicked);
			
			Item it = null;
			Bar bar = null;
	//		Bar a = outlookBar.Bars.Add("ViewStyle");
	//		a.Items.Add("Set ViewStyle",0);

			Bar stuckingTest = outlookBar.Bars.Add("Stucking test");
			stuckingTest.Items.Add("Can stuck",0);
			stuckingTest.Items.Add("Can stuck",0);

			it = stuckingTest.Items.Add("Can't stuck",0);
			it.AllowStuck = false;

			stuckingTest.Items.Add("Can't stuck",0);

			bar = outlookBar.Bars.Add("Full item select");
			bar.ItemsStyle = ItemsStyle.FullSelect;
			bar.Items.Add("Item a",0);
			bar.Items.Add("Item b",0);

			bar = outlookBar.Bars.Add("This is multi line bar text test");
			bar.Items.Add("This is multiline item caption test",0);
			bar.Items.Add("For some reason many comercical Outlook bars wont do it.",0);
			bar.Items.Add("Is it nicer to see ...",0);
		}

		#endregion
	}
}
