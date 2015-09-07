namespace TrueSoftware 
{
	using System;
	using System.Drawing;
	using System.Resources;
	using System.ComponentModel;
	using System.Windows.Forms;

	using TrueSoftware.Windows.Forms;

	class TestApp : Form 
	{
		private DropDownTreeView dropDownTree;
		private TextBox textBox;

		public TestApp() 
		{
			// setting form properties
			this.Size = new Size(400,400);
			this.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Drop Down TreeView Control";

			// setting controls size and location
			dropDownTree = new DropDownTreeView();
			dropDownTree.Size = new Size(180,100);
			dropDownTree.Location = new Point(100,100);


			// creating am imagelist
			ImageList images = new ImageList();
			images.ColorDepth = ColorDepth.Depth32Bit;
			images.Images.Add(Image.FromStream(this.GetType().Assembly.GetManifestResourceStream("TestApp.logoff.ico")));
			images.Images.Add(Image.FromStream(this.GetType().Assembly.GetManifestResourceStream("TestApp.Run.ico")));
			images.Images.Add(Image.FromStream(this.GetType().Assembly.GetManifestResourceStream("TestApp.MSN.ico")));

			// assigning the imagelist to controls imagelist
			dropDownTree.Imagelist = images;

			// adding some nodes to the treeview
			dropDownTree.Nodes.Add("Crew");
			dropDownTree.Nodes[0].ImageIndex = 0;
			AddNode(dropDownTree.Nodes[0],"James T. Kirk",0);
			AddNode(dropDownTree.Nodes[0],"Jean-Luc Picard",0);
			AddNode(dropDownTree.Nodes[0],"Benjamin Sisko",0);
			AddNode(dropDownTree.Nodes[0],"Kathryn Janeway",0);
			AddNode(dropDownTree.Nodes[0],"Jonathan Archer",0);
			
			dropDownTree.Nodes.Add("Programming Languages");
			dropDownTree.Nodes[1].ImageIndex = 1;
			AddNode(dropDownTree.Nodes[1],"C#.NET",1);
			AddNode(dropDownTree.Nodes[1],"ASP.NET",1);
			AddNode(dropDownTree.Nodes[1],"VisualBasic 6.0",1);
			AddNode(dropDownTree.Nodes[1],"PHP 4",1);

			dropDownTree.Nodes.Add("Oprating Systems");
			dropDownTree.Nodes[2].ImageIndex = 2;
			AddNode(dropDownTree.Nodes[2],"Windows XP",2);
			AddNode(dropDownTree.Nodes[2],"Windows 2000",2);
			AddNode(dropDownTree.Nodes[2],"Windows 98",2);
			AddNode(dropDownTree.Nodes[2],"Windows 95",2);
			AddNode(dropDownTree.Nodes[2],"Linux",2);

			// initializing a sample textbox for the show
			textBox = new TextBox();
			textBox.Text = DateTime.Now.ToString();
			textBox.Location = new Point(dropDownTree.Left,dropDownTree.Top + dropDownTree.Height + 4);
			textBox.Width = 160;
	
			// adding the controls to the form;
			this.Controls.AddRange(new Control[]{dropDownTree,textBox});
		}

		// simple void for adding items to the treeview
		private void AddNode(TreeNode parentNode,string Caption,int ImageIndex) 
		{
			TreeNode node = new TreeNode(Caption);
			node.ImageIndex = ImageIndex;
			parentNode.Nodes.Add(node);
		}


		// application entri point
		[STAThread]
		public static void Main(string[] args) 
		{
			Application.Run(new TestApp());
		}
	}
}