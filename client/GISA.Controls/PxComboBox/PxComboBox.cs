using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using System.ComponentModel;

namespace GISA.Controls
{	
		public class PxComboBox : ComboBox
		{

			public PxComboBox()
			{
				base.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
				base.DropDownStyle = ComboBoxStyle.DropDownList;
			}

			private ImageList mImageList = new ImageList();
			[DefaultValue((String)null), Description("ComboBox with an icon for each item"), Category("Behavior")]
			public ImageList ImageList
			{
				get
				{
					return this.mImageList;
				}
				set
				{
					if (value == this.mImageList)
					{
						return;
					}
					this.mImageList = value;
					if (this.mImageList != null)
					{
						this.ItemHeight = mImageList.ImageSize.Height;
					}
				}
			}

			private ArrayList mImageIndexes = new ArrayList();
			[Description("Image indexes, each position of the array contains the corresponding image index for that same position on the ComboBox."), Category("Behavior")]
			public ArrayList ImageIndexes
			{
				get
				{
					return this.mImageIndexes;
				}
				set
				{
					if (value == this.mImageIndexes)
					{
						return;
					}
					this.mImageIndexes = value;
				}
			}

			private int mImagePadding = 2;
			[DefaultValue((int)2), Description("Padding around icons."), Category("Behavior")]
			public int ImagePadding
			{
				get
				{
					return this.mImagePadding;
				}
				set
				{
					this.mImagePadding = value;
				}
			}

			protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
			{
				if (! Enabled)
				{
					this.BackColor = System.Drawing.SystemColors.Control;
				}
				else
				{
					this.BackColor = SystemColors.Window;
				}
				e.DrawBackground();
				e.DrawFocusRectangle();

				if (e.Index != -1)
				{
					string itemText = null;
					if (DisplayMember != null && DisplayMember.Length != 0)
					{
						itemText = FilterItemOnProperty(Items[e.Index], DisplayMember).ToString();
					}
					else
					{
						itemText = this.Items[e.Index].ToString();
					}
					Size imageSize = new Size();
					int imgIdx = 0;
					if (mImageList != null && ! (mImageList.Images.Count == 0))
					{
						imageSize = mImageList.ImageSize;
						imgIdx = getImageIndex(e.Index);
					}

					if (ImageIndexes != null && ! (ImageIndexes.Count == 0) && imgIdx != -1)
					{

						this.ImageList.Draw(e.Graphics, e.Bounds.Left + mImagePadding, e.Bounds.Top, imgIdx);
						e.Graphics.DrawString(itemText, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + mImagePadding + imageSize.Width + mImagePadding, e.Bounds.Top + mImagePadding);
					}
					else
					{
						e.Graphics.DrawString(itemText, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left, e.Bounds.Top);
					}
				}
				else
				{
					e.Graphics.DrawString(Text, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left, e.Bounds.Top);
				}

				base.OnDrawItem(e);
			}

			private int getImageIndex(int itemIndex)
			{
				if (itemIndex < 0 || itemIndex >= mImageIndexes.Count)
				{
					return -1;
				}
				return (int)(mImageIndexes[itemIndex]);
			}
		}
	
} //end of root namespace