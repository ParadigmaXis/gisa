using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace GISA.Controls
{
	/// <summary>
	/// Summary description for FormTestPxListView.
	/// </summary>
	public class FormTestPxListView : System.Windows.Forms.Form
	{
		private PxListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormTestPxListView()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("aaaa");
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("bbbb");
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("cccc");
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("dddd");
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("eeee");
			this.listView1 = new GISA.Controls.PxListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3});
			this.listView1.CustomizedSorting = false;
			this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
																					  listViewItem1,
																					  listViewItem2,
																					  listViewItem3,
																					  listViewItem4,
																					  listViewItem5});
			this.listView1.Location = new System.Drawing.Point(16, 16);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(264, 144);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			//this.listView1.SelectedIndexChanging += new GISA.Controls.PxListView.SelectedIndexChangingEventHandler(this.listView1_SelectedIndexChanging);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(16, 176);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(264, 136);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "";
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "dsfgd";
			// 
			// FormTestPxListView
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 317);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.listView1);
			this.Name = "FormTestPxListView";
			this.Text = "FormTestPxListView";
			this.ResumeLayout(false);

		}
		#endregion

		private void listView1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			textBox1.Text = textBox1.Text + Environment.NewLine + "listView1_SelectedIndexChanged";
		}

		private void listView1_SelectedIndexChanging(object sender, GISA.Controls.ItemChangingEventArgs e)
		{
			textBox1.Text = textBox1.Text + Environment.NewLine + "listView1_SelectedIndexChanging";		
		}		
	}
}
