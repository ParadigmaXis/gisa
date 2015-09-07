using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace LumiSoft.UI.Controls
{
	/// <summary>
	/// Summary description for WPictureBox.
	/// </summary>
	public class WPictureBox : WControlBase
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WPictureBox()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.pictureBox1.Location = new System.Drawing.Point(1, 1);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(98, 98);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// WPictureBox
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pictureBox1});
			this.Name = "WPictureBox";
			this.Size = new System.Drawing.Size(100, 100);
			this.ViewStyle.BorderColor = System.Drawing.Color.DarkGray;
			this.ViewStyle.BorderHotColor = System.Drawing.Color.Black;
			this.ViewStyle.ButtonColor = System.Drawing.SystemColors.Control;
			this.ViewStyle.ButtonHotColor = System.Drawing.Color.FromArgb(((System.Byte)(182)), ((System.Byte)(193)), ((System.Byte)(214)));
			this.ViewStyle.ButtonPressedColor = System.Drawing.Color.FromArgb(((System.Byte)(210)), ((System.Byte)(218)), ((System.Byte)(232)));
			this.ViewStyle.ControlBackColor = System.Drawing.SystemColors.Control;
			this.ViewStyle.EditColor = System.Drawing.Color.White;
			this.ViewStyle.EditFocusedColor = System.Drawing.Color.Beige;
			this.ViewStyle.EditReadOnlyColor = System.Drawing.Color.White;
			this.ViewStyle.FlashColor = System.Drawing.Color.Pink;
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		#region Events handling

		#endregion

								
		#region Properties Implementation

		public Image Image
		{
			get{ return pictureBox1.Image; }

			set{ 
				pictureBox1.Image = value; 

				if(pictureBox1.SizeMode == PictureBoxSizeMode.AutoSize){
					base.Size = new Size(pictureBox1.Width + 2,pictureBox1.Height + 2);
				}
			}
		}

		public PictureBoxSizeMode SizeMode
		{
			get{ return pictureBox1.SizeMode; }

			set{ 
				pictureBox1.SizeMode = value; 

				if(value == PictureBoxSizeMode.AutoSize){
					base.Size = new Size(pictureBox1.Width + 2,pictureBox1.Height + 2);
				}
			}
		}

		public new Size Size
		{
			get{ return base.Size; }

			set{
				if(pictureBox1.SizeMode == PictureBoxSizeMode.AutoSize){
					base.Size = new Size(pictureBox1.Width + 2,pictureBox1.Height + 2);
				}
				else{                    
					base.Size = value;
				}
			}
		}

		public new int Width
		{
			get{ return base.Width; }

			set{
				if(pictureBox1.SizeMode == PictureBoxSizeMode.AutoSize){
					base.Width = pictureBox1.Width + 2;
				}
				else{
					base.Width = value;
				}
			}
		}

		public new int Height
		{
			get{ return base.Height; }

			set{
				if(pictureBox1.SizeMode == PictureBoxSizeMode.AutoSize){
					base.Height = pictureBox1.Height + 2;
				}
				else{
					base.Height = value;
				}
			}
		}

		#endregion

	}
}
