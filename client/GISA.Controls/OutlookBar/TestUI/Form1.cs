using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using LumiSoft.UI;

namespace TestUI
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private LumiSoft.UI.Controls.WEditBox wEditBox1;
		private LumiSoft.UI.Controls.WSpinEdit wSpinEdit1;
		private LumiSoft.UI.Controls.WPictureBox wPictureBox1;
		private LumiSoft.UI.Controls.WButtonEdit wButtonEdit1;
		private LumiSoft.UI.Controls.WDatePicker.WDatePicker wDatePicker1;
		private LumiSoft.UI.Controls.WComboBox wComboBox1;
		private LumiSoft.UI.Controls.WCheckBox.WCheckBox wCheckBox1;
		private LumiSoft.UI.Controls.WToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton toolBarButton3;
		private System.Windows.Forms.ToolBarButton toolBarButton4;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolBarButton toolBarButton5;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private LumiSoft.UI.Controls.WEditBox wEditBox2;
		private LumiSoft.UI.Controls.WEditBox wEditBox3;
		private LumiSoft.UI.Controls.WSpinEdit wSpinEdit2;
		private LumiSoft.UI.Controls.WSpinEdit wSpinEdit3;
		private LumiSoft.UI.Controls.WButtonEdit wButtonEdit2;
		private LumiSoft.UI.Controls.WButtonEdit wButtonEdit3;
		private LumiSoft.UI.Controls.WComboBox wComboBox2;
		private LumiSoft.UI.Controls.WComboBox wComboBox3;
		private LumiSoft.UI.Controls.WDatePicker.WDatePicker wDatePicker2;
		private LumiSoft.UI.Controls.WDatePicker.WDatePicker wDatePicker3;
		private LumiSoft.UI.Controls.WCheckBox.WCheckBox wCheckBox2;
		private LumiSoft.UI.Controls.WCheckBox.WCheckBox wCheckBox3;
		private System.Windows.Forms.Button button1;
		private System.ComponentModel.IContainer components;

		public Form1(LumiSoft.UI.Controls.WFrame wFrame)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			
			PropertyGrid propertyGrid1 = new PropertyGrid();
			propertyGrid1.CommandsVisibleIfAvailable = true;
			propertyGrid1.Location = new Point(5, 25);
			propertyGrid1.Size = new System.Drawing.Size(200, 400);
			propertyGrid1.TabIndex = 1;
			propertyGrid1.Text = "Property Grid";

			this.Controls.Add(propertyGrid1);

			propertyGrid1.SelectedObject = LumiSoft.UI.ViewStyle.staticViewStyle;


			wComboBox1.Items.Add("fsg");
			wComboBox1.Items.Add("gddgddh");
			wComboBox1.Items.Add("fsdshg");
			wComboBox1.Items.Add("wrrurt");
			wComboBox1.Items.Add("iyytii","tag");

			wComboBox2.Items.Add("fsg");
			wComboBox2.Items.Add("gddgddh");
			wComboBox2.Items.Add("fsdshg");
			wComboBox2.Items.Add("wrrurt");
			wComboBox2.Items.Add("iyytii","tag");

			wFrame.Frame_TooBar = this.toolBar1;
		
		}

		#region function Dispose

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.wEditBox1 = new LumiSoft.UI.Controls.WEditBox();
			this.wSpinEdit1 = new LumiSoft.UI.Controls.WSpinEdit();
			this.wPictureBox1 = new LumiSoft.UI.Controls.WPictureBox();
			this.wButtonEdit1 = new LumiSoft.UI.Controls.WButtonEdit();
			this.wDatePicker1 = new LumiSoft.UI.Controls.WDatePicker.WDatePicker();
			this.wComboBox1 = new LumiSoft.UI.Controls.WComboBox();
			this.wCheckBox1 = new LumiSoft.UI.Controls.WCheckBox.WCheckBox();
			this.toolBar1 = new LumiSoft.UI.Controls.WToolBar();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.wEditBox2 = new LumiSoft.UI.Controls.WEditBox();
			this.wEditBox3 = new LumiSoft.UI.Controls.WEditBox();
			this.wSpinEdit2 = new LumiSoft.UI.Controls.WSpinEdit();
			this.wSpinEdit3 = new LumiSoft.UI.Controls.WSpinEdit();
			this.wButtonEdit2 = new LumiSoft.UI.Controls.WButtonEdit();
			this.wButtonEdit3 = new LumiSoft.UI.Controls.WButtonEdit();
			this.wComboBox2 = new LumiSoft.UI.Controls.WComboBox();
			this.wComboBox3 = new LumiSoft.UI.Controls.WComboBox();
			this.wDatePicker2 = new LumiSoft.UI.Controls.WDatePicker.WDatePicker();
			this.wDatePicker3 = new LumiSoft.UI.Controls.WDatePicker.WDatePicker();
			this.wCheckBox2 = new LumiSoft.UI.Controls.WCheckBox.WCheckBox();
			this.wCheckBox3 = new LumiSoft.UI.Controls.WCheckBox.WCheckBox();
			this.button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.wEditBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wSpinEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wPictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wButtonEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wDatePicker1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wComboBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wCheckBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wEditBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wEditBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wSpinEdit2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wSpinEdit3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wButtonEdit2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wButtonEdit3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wComboBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wComboBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wDatePicker2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wDatePicker3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wCheckBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wCheckBox3)).BeginInit();
			this.SuspendLayout();
			// 
			// wEditBox1
			// 
			this.wEditBox1.DecimalPlaces = 2;
			this.wEditBox1.DecMaxValue = 99999999;
			this.wEditBox1.DecMinValue = -99999999;
			this.wEditBox1.Location = new System.Drawing.Point(240, 40);
			this.wEditBox1.Mask = LumiSoft.UI.Controls.WEditBox_Mask.Text;
			this.wEditBox1.MaxLength = 32767;
			this.wEditBox1.Multiline = false;
			this.wEditBox1.Name = "wEditBox1";
			this.wEditBox1.PasswordChar = '\0';
			this.wEditBox1.ReadOnly = false;
			this.wEditBox1.Size = new System.Drawing.Size(100, 20);
			this.wEditBox1.TabIndex = 0;
			this.wEditBox1.Text = "Normal";
			this.wEditBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.wEditBox1.UseStaticViewStyle = true;
			// 
			// wSpinEdit1
			// 
			this.wSpinEdit1.BackColor = System.Drawing.Color.White;
			this.wSpinEdit1.ButtonsAlign = LumiSoft.UI.Controls.LeftRight.Right;
			this.wSpinEdit1.DecimalPlaces = 0;
			this.wSpinEdit1.DecMaxValue = 99999999;
			this.wSpinEdit1.DecMinValue = -99999999;
			this.wSpinEdit1.DecValue = new System.Decimal(new int[] {
																		0,
																		0,
																		0,
																		0});
			this.wSpinEdit1.Location = new System.Drawing.Point(240, 120);
			this.wSpinEdit1.Mask = LumiSoft.UI.Controls.WEditBox_Mask.Numeric;
			this.wSpinEdit1.MaxLength = 32767;
			this.wSpinEdit1.Name = "wSpinEdit1";
			this.wSpinEdit1.ReadOnly = false;
			this.wSpinEdit1.Size = new System.Drawing.Size(104, 20);
			this.wSpinEdit1.TabIndex = 1;
			this.wSpinEdit1.Text = "0";
			this.wSpinEdit1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.wSpinEdit1.UseStaticViewStyle = true;
			// 
			// wPictureBox1
			// 
			this.wPictureBox1.Image = null;
			this.wPictureBox1.Location = new System.Drawing.Point(384, 200);
			this.wPictureBox1.Name = "wPictureBox1";
			this.wPictureBox1.Size = new System.Drawing.Size(100, 64);
			this.wPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
			this.wPictureBox1.TabIndex = 2;
			this.wPictureBox1.UseStaticViewStyle = true;
			// 
			// wButtonEdit1
			// 
			this.wButtonEdit1.AcceptsPlussKey = true;
			this.wButtonEdit1.BackColor = System.Drawing.Color.White;
			this.wButtonEdit1.ButtonIcon = ((System.Drawing.Icon)(resources.GetObject("wButtonEdit1.ButtonIcon")));
			this.wButtonEdit1.ButtonWidth = 18;
			this.wButtonEdit1.Location = new System.Drawing.Point(240, 200);
			this.wButtonEdit1.Mask = LumiSoft.UI.Controls.WEditBox_Mask.Text;
			this.wButtonEdit1.MaxLength = 32767;
			this.wButtonEdit1.Name = "wButtonEdit1";
			this.wButtonEdit1.ReadOnly = false;
			this.wButtonEdit1.Size = new System.Drawing.Size(118, 20);
			this.wButtonEdit1.TabIndex = 3;
			this.wButtonEdit1.Text = "Normal";
			this.wButtonEdit1.UseStaticViewStyle = true;
			this.wButtonEdit1.ButtonPressed += new LumiSoft.UI.Controls.ButtonPressedEventHandler(this.wButtonEdit1_ButtonPressed);
			// 
			// wDatePicker1
			// 
			this.wDatePicker1.AcceptsPlussKey = true;
			this.wDatePicker1.BackColor = System.Drawing.Color.White;
			this.wDatePicker1.ButtonIcon = ((System.Drawing.Icon)(resources.GetObject("wDatePicker1.ButtonIcon")));
			this.wDatePicker1.ButtonWidth = 18;
			this.wDatePicker1.Location = new System.Drawing.Point(384, 120);
			this.wDatePicker1.Mask = LumiSoft.UI.Controls.WEditBox_Mask.Date;
			this.wDatePicker1.MaxLength = 32767;
			this.wDatePicker1.Name = "wDatePicker1";
			this.wDatePicker1.ReadOnly = false;
			this.wDatePicker1.Size = new System.Drawing.Size(88, 20);
			this.wDatePicker1.TabIndex = 4;
			this.wDatePicker1.Text = "04/17/2002";
			this.wDatePicker1.UseStaticViewStyle = true;
			this.wDatePicker1.Value = new System.DateTime(2002, 4, 17, 0, 0, 0, 0);
			// 
			// wComboBox1
			// 
			this.wComboBox1.AcceptsPlussKey = true;
			this.wComboBox1.BackColor = System.Drawing.Color.White;
			this.wComboBox1.ButtonIcon = ((System.Drawing.Icon)(resources.GetObject("wComboBox1.ButtonIcon")));
			this.wComboBox1.ButtonWidth = 18;
			this.wComboBox1.DropDownWidth = 120;
			this.wComboBox1.Location = new System.Drawing.Point(384, 40);
			this.wComboBox1.Mask = LumiSoft.UI.Controls.WEditBox_Mask.Text;
			this.wComboBox1.MaxLength = 32767;
			this.wComboBox1.Name = "wComboBox1";
			this.wComboBox1.ReadOnly = false;
			this.wComboBox1.Size = new System.Drawing.Size(120, 20);
			this.wComboBox1.TabIndex = 5;
			this.wComboBox1.Text = "Normal";
			this.wComboBox1.UseStaticViewStyle = true;
			this.wComboBox1.VisibleItems = 5;
			// 
			// wCheckBox1
			// 
			this.wCheckBox1.Checked = false;
			this.wCheckBox1.Location = new System.Drawing.Point(240, 280);
			this.wCheckBox1.Name = "wCheckBox1";
			this.wCheckBox1.ReadOnly = false;
			this.wCheckBox1.Size = new System.Drawing.Size(30, 22);
			this.wCheckBox1.TabIndex = 6;
			this.wCheckBox1.UseStaticViewStyle = true;
			// 
			// toolBar1
			// 
			this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.toolBarButton1,
																						this.toolBarButton2,
																						this.toolBarButton3,
																						this.toolBarButton4,
																						this.toolBarButton5});
			this.toolBar1.Divider = false;
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.imageList1;
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(536, 23);
			this.toolBar1.TabIndex = 7;
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.Enabled = false;
			this.toolBarButton1.ImageIndex = 0;
			// 
			// toolBarButton2
			// 
			this.toolBarButton2.Enabled = false;
			this.toolBarButton2.ImageIndex = 1;
			// 
			// toolBarButton3
			// 
			this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// toolBarButton4
			// 
			this.toolBarButton4.DropDownMenu = this.contextMenu1;
			this.toolBarButton4.ImageIndex = 3;
			this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Enable print";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Disable print";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// toolBarButton5
			// 
			this.toolBarButton5.ImageIndex = 2;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
			// 
			// wEditBox2
			// 
			this.wEditBox2.DecimalPlaces = 2;
			this.wEditBox2.DecMaxValue = 99999999;
			this.wEditBox2.DecMinValue = -99999999;
			this.wEditBox2.Location = new System.Drawing.Point(240, 64);
			this.wEditBox2.Mask = LumiSoft.UI.Controls.WEditBox_Mask.Text;
			this.wEditBox2.MaxLength = 32767;
			this.wEditBox2.Multiline = false;
			this.wEditBox2.Name = "wEditBox2";
			this.wEditBox2.PasswordChar = '\0';
			this.wEditBox2.ReadOnly = true;
			this.wEditBox2.Size = new System.Drawing.Size(100, 20);
			this.wEditBox2.TabIndex = 8;
			this.wEditBox2.Text = "ReadOnly";
			this.wEditBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.wEditBox2.UseStaticViewStyle = true;
			// 
			// wEditBox3
			// 
			this.wEditBox3.DecimalPlaces = 2;
			this.wEditBox3.DecMaxValue = 99999999;
			this.wEditBox3.DecMinValue = -99999999;
			this.wEditBox3.Enabled = false;
			this.wEditBox3.Location = new System.Drawing.Point(240, 88);
			this.wEditBox3.Mask = LumiSoft.UI.Controls.WEditBox_Mask.Text;
			this.wEditBox3.MaxLength = 32767;
			this.wEditBox3.Multiline = false;
			this.wEditBox3.Name = "wEditBox3";
			this.wEditBox3.PasswordChar = '\0';
			this.wEditBox3.ReadOnly = false;
			this.wEditBox3.Size = new System.Drawing.Size(100, 20);
			this.wEditBox3.TabIndex = 9;
			this.wEditBox3.Text = "Disabled";
			this.wEditBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.wEditBox3.UseStaticViewStyle = true;
			// 
			// wSpinEdit2
			// 
			this.wSpinEdit2.BackColor = System.Drawing.Color.White;
			this.wSpinEdit2.ButtonsAlign = LumiSoft.UI.Controls.LeftRight.Right;
			this.wSpinEdit2.DecimalPlaces = 0;
			this.wSpinEdit2.DecMaxValue = 99999999;
			this.wSpinEdit2.DecMinValue = -99999999;
			this.wSpinEdit2.DecValue = new System.Decimal(new int[] {
																		0,
																		0,
																		0,
																		0});
			this.wSpinEdit2.Location = new System.Drawing.Point(240, 144);
			this.wSpinEdit2.Mask = LumiSoft.UI.Controls.WEditBox_Mask.Numeric;
			this.wSpinEdit2.MaxLength = 32767;
			this.wSpinEdit2.Name = "wSpinEdit2";
			this.wSpinEdit2.ReadOnly = true;
			this.wSpinEdit2.Size = new System.Drawing.Size(104, 20);
			this.wSpinEdit2.TabIndex = 10;
			this.wSpinEdit2.Text = "0";
			this.wSpinEdit2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.wSpinEdit2.UseStaticViewStyle = true;
			// 
			// wSpinEdit3
			// 
			this.wSpinEdit3.BackColor = System.Drawing.Color.White;
			this.wSpinEdit3.ButtonsAlign = LumiSoft.UI.Controls.LeftRight.Right;
			this.wSpinEdit3.DecimalPlaces = 0;
			this.wSpinEdit3.DecMaxValue = 99999999;
			this.wSpinEdit3.DecMinValue = -99999999;
			this.wSpinEdit3.DecValue = new System.Decimal(new int[] {
																		0,
																		0,
																		0,
																		0});
			this.wSpinEdit3.Enabled = false;
			this.wSpinEdit3.Location = new System.Drawing.Point(240, 168);
			this.wSpinEdit3.Mask = LumiSoft.UI.Controls.WEditBox_Mask.Numeric;
			this.wSpinEdit3.MaxLength = 32767;
			this.wSpinEdit3.Name = "wSpinEdit3";
			this.wSpinEdit3.ReadOnly = false;
			this.wSpinEdit3.Size = new System.Drawing.Size(104, 20);
			this.wSpinEdit3.TabIndex = 11;
			this.wSpinEdit3.Text = "0";
			this.wSpinEdit3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.wSpinEdit3.UseStaticViewStyle = true;
			// 
			// wButtonEdit2
			// 
			this.wButtonEdit2.AcceptsPlussKey = true;
			this.wButtonEdit2.BackColor = System.Drawing.Color.White;
			this.wButtonEdit2.ButtonIcon = ((System.Drawing.Icon)(resources.GetObject("wButtonEdit2.ButtonIcon")));
			this.wButtonEdit2.ButtonWidth = 18;
			this.wButtonEdit2.Location = new System.Drawing.Point(240, 224);
			this.wButtonEdit2.Mask = LumiSoft.UI.Controls.WEditBox_Mask.Text;
			this.wButtonEdit2.MaxLength = 32767;
			this.wButtonEdit2.Name = "wButtonEdit2";
			this.wButtonEdit2.ReadOnly = true;
			this.wButtonEdit2.Size = new System.Drawing.Size(118, 20);
			this.wButtonEdit2.TabIndex = 12;
			this.wButtonEdit2.Text = "ReadOnly";
			this.wButtonEdit2.UseStaticViewStyle = true;
			// 
			// wButtonEdit3
			// 
			this.wButtonEdit3.AcceptsPlussKey = true;
			this.wButtonEdit3.BackColor = System.Drawing.Color.White;
			this.wButtonEdit3.ButtonIcon = ((System.Drawing.Icon)(resources.GetObject("wButtonEdit3.ButtonIcon")));
			this.wButtonEdit3.ButtonWidth = 18;
			this.wButtonEdit3.Enabled = false;
			this.wButtonEdit3.Location = new System.Drawing.Point(240, 248);
			this.wButtonEdit3.Mask = LumiSoft.UI.Controls.WEditBox_Mask.Text;
			this.wButtonEdit3.MaxLength = 32767;
			this.wButtonEdit3.Name = "wButtonEdit3";
			this.wButtonEdit3.ReadOnly = false;
			this.wButtonEdit3.Size = new System.Drawing.Size(118, 20);
			this.wButtonEdit3.TabIndex = 13;
			this.wButtonEdit3.Text = "Disabled";
			this.wButtonEdit3.UseStaticViewStyle = true;
			// 
			// wComboBox2
			// 
			this.wComboBox2.AcceptsPlussKey = true;
			this.wComboBox2.BackColor = System.Drawing.Color.White;
			this.wComboBox2.ButtonIcon = ((System.Drawing.Icon)(resources.GetObject("wComboBox2.ButtonIcon")));
			this.wComboBox2.ButtonWidth = 18;
			this.wComboBox2.DropDownWidth = 120;
			this.wComboBox2.Location = new System.Drawing.Point(384, 64);
			this.wComboBox2.Mask = LumiSoft.UI.Controls.WEditBox_Mask.Text;
			this.wComboBox2.MaxLength = 32767;
			this.wComboBox2.Name = "wComboBox2";
			this.wComboBox2.ReadOnly = true;
			this.wComboBox2.Size = new System.Drawing.Size(120, 20);
			this.wComboBox2.TabIndex = 14;
			this.wComboBox2.Text = "ReadOnly";
			this.wComboBox2.UseStaticViewStyle = true;
			this.wComboBox2.VisibleItems = 5;
			// 
			// wComboBox3
			// 
			this.wComboBox3.AcceptsPlussKey = true;
			this.wComboBox3.BackColor = System.Drawing.Color.White;
			this.wComboBox3.ButtonIcon = ((System.Drawing.Icon)(resources.GetObject("wComboBox3.ButtonIcon")));
			this.wComboBox3.ButtonWidth = 18;
			this.wComboBox3.DropDownWidth = 120;
			this.wComboBox3.Enabled = false;
			this.wComboBox3.Location = new System.Drawing.Point(384, 88);
			this.wComboBox3.Mask = LumiSoft.UI.Controls.WEditBox_Mask.Text;
			this.wComboBox3.MaxLength = 32767;
			this.wComboBox3.Name = "wComboBox3";
			this.wComboBox3.ReadOnly = false;
			this.wComboBox3.Size = new System.Drawing.Size(120, 20);
			this.wComboBox3.TabIndex = 15;
			this.wComboBox3.Text = "Disabled";
			this.wComboBox3.UseStaticViewStyle = true;
			this.wComboBox3.VisibleItems = 5;
			// 
			// wDatePicker2
			// 
			this.wDatePicker2.AcceptsPlussKey = true;
			this.wDatePicker2.BackColor = System.Drawing.Color.White;
			this.wDatePicker2.ButtonIcon = ((System.Drawing.Icon)(resources.GetObject("wDatePicker2.ButtonIcon")));
			this.wDatePicker2.ButtonWidth = 18;
			this.wDatePicker2.Location = new System.Drawing.Point(384, 144);
			this.wDatePicker2.Mask = LumiSoft.UI.Controls.WEditBox_Mask.Date;
			this.wDatePicker2.MaxLength = 32767;
			this.wDatePicker2.Name = "wDatePicker2";
			this.wDatePicker2.ReadOnly = true;
			this.wDatePicker2.Size = new System.Drawing.Size(88, 20);
			this.wDatePicker2.TabIndex = 16;
			this.wDatePicker2.Text = "04/17/2002";
			this.wDatePicker2.UseStaticViewStyle = true;
			this.wDatePicker2.Value = new System.DateTime(2002, 4, 17, 0, 0, 0, 0);
			// 
			// wDatePicker3
			// 
			this.wDatePicker3.AcceptsPlussKey = true;
			this.wDatePicker3.BackColor = System.Drawing.Color.White;
			this.wDatePicker3.ButtonIcon = ((System.Drawing.Icon)(resources.GetObject("wDatePicker3.ButtonIcon")));
			this.wDatePicker3.ButtonWidth = 18;
			this.wDatePicker3.Enabled = false;
			this.wDatePicker3.Location = new System.Drawing.Point(384, 168);
			this.wDatePicker3.Mask = LumiSoft.UI.Controls.WEditBox_Mask.Date;
			this.wDatePicker3.MaxLength = 32767;
			this.wDatePicker3.Name = "wDatePicker3";
			this.wDatePicker3.ReadOnly = false;
			this.wDatePicker3.Size = new System.Drawing.Size(88, 20);
			this.wDatePicker3.TabIndex = 17;
			this.wDatePicker3.Text = "04/17/2002";
			this.wDatePicker3.UseStaticViewStyle = true;
			this.wDatePicker3.Value = new System.DateTime(2002, 4, 17, 0, 0, 0, 0);
			// 
			// wCheckBox2
			// 
			this.wCheckBox2.Checked = false;
			this.wCheckBox2.Location = new System.Drawing.Point(280, 280);
			this.wCheckBox2.Name = "wCheckBox2";
			this.wCheckBox2.ReadOnly = true;
			this.wCheckBox2.Size = new System.Drawing.Size(30, 22);
			this.wCheckBox2.TabIndex = 18;
			this.wCheckBox2.UseStaticViewStyle = true;
			// 
			// wCheckBox3
			// 
			this.wCheckBox3.Checked = false;
			this.wCheckBox3.Enabled = false;
			this.wCheckBox3.Location = new System.Drawing.Point(320, 280);
			this.wCheckBox3.Name = "wCheckBox3";
			this.wCheckBox3.ReadOnly = false;
			this.wCheckBox3.Size = new System.Drawing.Size(30, 22);
			this.wCheckBox3.TabIndex = 19;
			this.wCheckBox3.UseStaticViewStyle = true;
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(400, 280);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(72, 32);
			this.button1.TabIndex = 20;
			this.button1.Text = "Flash controls";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(536, 325);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button1,
																		  this.wCheckBox3,
																		  this.wCheckBox2,
																		  this.wDatePicker3,
																		  this.wDatePicker2,
																		  this.wComboBox3,
																		  this.wComboBox2,
																		  this.wButtonEdit3,
																		  this.wButtonEdit2,
																		  this.wSpinEdit3,
																		  this.wSpinEdit2,
																		  this.wEditBox3,
																		  this.wEditBox2,
																		  this.toolBar1,
																		  this.wCheckBox1,
																		  this.wComboBox1,
																		  this.wDatePicker1,
																		  this.wButtonEdit1,
																		  this.wPictureBox1,
																		  this.wSpinEdit1,
																		  this.wEditBox1});
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.wEditBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wSpinEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wPictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wButtonEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wDatePicker1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wComboBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wCheckBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wEditBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wEditBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wSpinEdit2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wSpinEdit3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wButtonEdit2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wButtonEdit3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wComboBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wComboBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wDatePicker2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wDatePicker3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wCheckBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wCheckBox3)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		

		private void wButtonEdit1_ButtonPressed(object sender, System.EventArgs e)
		{
			MessageBox.Show("Button pressed");
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			wComboBox1.FlashControl();
			wEditBox1.FlashControl();
			wDatePicker1.FlashControl();
			wSpinEdit1.FlashControl();
			wButtonEdit1.FlashControl();
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			toolBarButton1.Enabled = true;
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			toolBarButton1.Enabled = false;
		}
	}
}
