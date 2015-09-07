using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using GISA.SharedResources;

namespace GISA
{
	public class FormImageViewer : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormImageViewer() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            ToolBar1.ButtonClick += ToolBar_ButtonClick;

			GetExtraResources();
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
		private System.ComponentModel.IContainer components = null;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		internal System.Windows.Forms.Panel pnlImagem;
		internal System.Windows.Forms.PictureBox pbImagem;
		internal System.Windows.Forms.ToolBar ToolBar1;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonPreviousImage;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonNextImage;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonRotateLeft;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonRotateNone;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonRotateRight;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonZoomAll;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonZoomIn;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonZoomOut;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonSeparator1;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonSeparator2;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonZoomRealSize;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.pnlImagem = new System.Windows.Forms.Panel();
			this.pbImagem = new System.Windows.Forms.PictureBox();
			this.ToolBar1 = new System.Windows.Forms.ToolBar();
			this.ToolBarButtonPreviousImage = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButtonNextImage = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButtonSeparator2 = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButtonRotateLeft = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButtonRotateNone = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButtonRotateRight = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButtonSeparator1 = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButtonZoomOut = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButtonZoomIn = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButtonZoomAll = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButtonZoomRealSize = new System.Windows.Forms.ToolBarButton();
			this.pnlImagem.SuspendLayout();
			this.SuspendLayout();
			//
			//pnlImagem
			//
			this.pnlImagem.AutoScroll = true;
			this.pnlImagem.Controls.Add(this.pbImagem);
			this.pnlImagem.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlImagem.Location = new System.Drawing.Point(0, 28);
			this.pnlImagem.Name = "pnlImagem";
			this.pnlImagem.Size = new System.Drawing.Size(320, 313);
			this.pnlImagem.TabIndex = 0;
			//
			//pbImagem
			//
			this.pbImagem.Location = new System.Drawing.Point(0, 0);
			this.pbImagem.Name = "pbImagem";
			this.pbImagem.Size = new System.Drawing.Size(176, 168);
			this.pbImagem.TabIndex = 1;
			this.pbImagem.TabStop = false;
			//
			//ToolBar1
			//
			this.ToolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {this.ToolBarButtonPreviousImage, this.ToolBarButtonNextImage, this.ToolBarButtonSeparator2, this.ToolBarButtonRotateLeft, this.ToolBarButtonRotateNone, this.ToolBarButtonRotateRight, this.ToolBarButtonSeparator1, this.ToolBarButtonZoomOut, this.ToolBarButtonZoomIn, this.ToolBarButtonZoomAll, this.ToolBarButtonZoomRealSize});
			this.ToolBar1.ButtonSize = new System.Drawing.Size(20, 20);
			this.ToolBar1.DropDownArrows = true;
			this.ToolBar1.Location = new System.Drawing.Point(0, 0);
			this.ToolBar1.Name = "ToolBar1";
			this.ToolBar1.ShowToolTips = true;
			this.ToolBar1.Size = new System.Drawing.Size(320, 28);
			this.ToolBar1.TabIndex = 1;
			//
			//ToolBarButtonPreviousImage
			//
			this.ToolBarButtonPreviousImage.ImageIndex = 0;
			this.ToolBarButtonPreviousImage.ToolTipText = "Anterior";
			//
			//ToolBarButtonNextImage
			//
			this.ToolBarButtonNextImage.ImageIndex = 1;
			this.ToolBarButtonNextImage.ToolTipText = "Próxima";
			//
			//ToolBarButtonSeparator2
			//
			this.ToolBarButtonSeparator2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			//
			//ToolBarButtonRotateLeft
			//
			this.ToolBarButtonRotateLeft.ImageIndex = 2;
			this.ToolBarButtonRotateLeft.ToolTipText = "Rodar para a esquerda";
			//
			//ToolBarButtonRotateNone
			//
			this.ToolBarButtonRotateNone.ImageIndex = 3;
			this.ToolBarButtonRotateNone.ToolTipText = "Rotação original";
			//
			//ToolBarButtonRotateRight
			//
			this.ToolBarButtonRotateRight.ImageIndex = 4;
			this.ToolBarButtonRotateRight.ToolTipText = "Rodar para a direita";
			//
			//ToolBarButtonSeparator1
			//
			this.ToolBarButtonSeparator1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			//
			//ToolBarButtonZoomOut
			//
			this.ToolBarButtonZoomOut.ImageIndex = 6;
			this.ToolBarButtonZoomOut.ToolTipText = "Reduzir";
			//
			//ToolBarButtonZoomIn
			//
			this.ToolBarButtonZoomIn.ImageIndex = 5;
			this.ToolBarButtonZoomIn.ToolTipText = "Ampliar";
			//
			//ToolBarButtonZoomAll
			//
			this.ToolBarButtonZoomAll.ImageIndex = 7;
			this.ToolBarButtonZoomAll.ToolTipText = "Reduzir para visualização completa";
			//
			//ToolBarButtonZoomRealSize
			//
			this.ToolBarButtonZoomRealSize.ImageIndex = 8;
			this.ToolBarButtonZoomRealSize.ToolTipText = "Dimensão original";
			//
			//FormImageViewer
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(320, 341);
			this.Controls.Add(this.pnlImagem);
			this.Controls.Add(this.ToolBar1);
			this.Name = "FormImageViewer";
			this.ShowInTaskbar = false;
			this.Text = "GISA - Imagem";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.pnlImagem.ResumeLayout(false);
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		public class ImageViewerEventArgs : EventArgs
		{

			private Image mImage = null;
			private string mDescricao = null;
			private bool mExistsPrevious = false;
			private bool mExistsNext = false;

			public Image Imagem { get { return mImage; } set { mImage = value; } }
			public string Descricao { get { return mDescricao; } set { mDescricao = value; } }
			public bool ExistsPrevious { get { return mExistsPrevious; } set { mExistsPrevious = value; } }
			public bool ExistsNext { get { return mExistsNext; } set { mExistsNext = value; } }
		}

		public delegate void NextImageEventHandler(object sender, ImageViewerEventArgs e);
		public delegate void PreviousImageEventHandler(object sender, ImageViewerEventArgs e);
		public event NextImageEventHandler NextImage;
		public event PreviousImageEventHandler PreviousImage;

		private Image mImagem = null;
		private string mDescricao = "";
		private bool mExistsPrevious = true;
		private bool mExistsNext = false;
		private RotateFlipType mRotacao = RotateFlipType.RotateNoneFlipNone;
		private Size mAmpliacao = new Size();

		public Image Imagem
		{
			get
			{
				if (mImagem != null)
					return (Image)(new Bitmap(mImagem));
				else
					return null;
			}
			set
			{
				mImagem = value;
				updateImage(Imagem);
				if (mImagem != null)
					mAmpliacao = Imagem.Size;
			}
		}

		public string Descricao
		{
			get
			{
				return mDescricao;
			}
			set
			{
				mDescricao = value;
				this.Text = "GISA - " + value;
			}
		}

		public bool ExistsPrevious
		{
			get
			{
				return mExistsPrevious;
			}
			set
			{
				mExistsPrevious = value;
			}
		}

		public bool ExistsNext
		{
			get
			{
				return mExistsNext;
			}
			set
			{
				mExistsNext = value;
			}
		}

		private RotateFlipType Rotacao
		{
			get
			{
				return mRotacao;
			}
			set
			{
				mRotacao = value;
				pbImagem.Image.RotateFlip(value);
			}
		}

		private Size Ampliacao
		{
			get
			{
				return mAmpliacao;
			}
			set
			{
				
				mAmpliacao = value;

				if (Rotacao == RotateFlipType.RotateNoneFlipNone)
				{
					Image newImg = resizeImage(pbImagem.Image, value);
					updateImage(newImg);
				}
				else
				{
					Image newImg = resizeImage(pbImagem.Image, new Size(value.Height, value.Width));
					updateImage(newImg);
				}
			}
		}

		private void refreshRotacao()
		{
		}

		private void refreshAmpliacao()
		{
		}

		private void ToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{

			if (e.Button == ToolBarButtonPreviousImage)
			{
				ImageViewerEventArgs ea = new ImageViewerEventArgs();
				if (PreviousImage != null)
					PreviousImage(this, ea);
				applyImageSwitch(ea);
			}
			else if (e.Button == ToolBarButtonNextImage)
			{
				ImageViewerEventArgs ea = new ImageViewerEventArgs();
				if (NextImage != null)
					NextImage(this, ea);
				applyImageSwitch(ea);
			}
			else
			{
				if (Imagem == null)
				{
					return;
				}
				if (e.Button == ToolBarButtonRotateLeft)
				{
					updateImage(Imagem);
					Rotacao = RotateFlipType.Rotate270FlipNone;
					Ampliacao = Ampliacao;
					pbImagem.Refresh();
				}
				else if (e.Button == ToolBarButtonRotateNone)
				{
					updateImage(Imagem);
					Rotacao = RotateFlipType.RotateNoneFlipNone;
					Ampliacao = Ampliacao;
					pbImagem.Refresh();
				}
				else if (e.Button == ToolBarButtonRotateRight)
				{
					updateImage(Imagem);
					Rotacao = RotateFlipType.Rotate90FlipNone;
					Ampliacao = Ampliacao;
				}
				else if (e.Button == ToolBarButtonZoomIn)
				{
					updateImage(Imagem);
					Rotacao = Rotacao;
					Ampliacao = ImageHelper.getSizeSameAspectRatio(Ampliacao, (Ampliacao + ImageHelper.ZoomStep));
					pbImagem.Refresh();
				}
				else if (e.Button == ToolBarButtonZoomOut)
				{
					updateImage(Imagem);
					Rotacao = Rotacao;
					Ampliacao = ImageHelper.getSizeSameAspectRatio(Ampliacao, (Ampliacao - ImageHelper.ZoomStep));
					pbImagem.Refresh();
				}
				else if (e.Button == ToolBarButtonZoomRealSize)
				{
					updateImage(Imagem);
					mAmpliacao = Imagem.Size;
					Rotacao = RotateFlipType.RotateNoneFlipNone;
					Ampliacao = Imagem.Size;
					pbImagem.Refresh();
				}
				else if (e.Button == ToolBarButtonZoomAll)
				{
					updateImage(Imagem);
					mAmpliacao = Imagem.Size;
					Rotacao = Rotacao;
					if (Imagem.Width > pnlImagem.Width | Imagem.Height > pnlImagem.Height)
					{
						// se a imagem original for maior que o
						// painel encolher a imagem para o tamanho do painel.
						Size newSize = ImageHelper.getSizeSameAspectRatio(Imagem.Size, pnlImagem.Size);
						Ampliacao = newSize;
					}
					else
					{
						Ampliacao = Imagem.Size;
					}
					pbImagem.Refresh();
				}
			}
		}

		private bool ThumbnailCallback()
		{
			return true;
		}

		public static Image resizeImage(Image imagem, Size newSize)
		{
			Debug.Write("res:" + newSize.Width.ToString() + " " + newSize.Height.ToString() + System.Environment.NewLine);
			// protect against minimize window (and other situations when
			// the panel dimensions are 0,0)
			if (newSize.IsEmpty)
			{
				return null;
			}
			// create a new empty bitmap with the specified size
			Bitmap bmp = new Bitmap(newSize.Width, newSize.Height);
			// retrieve a canvas object that allows to draw on the empty bitmap
			Graphics graphics = System.Drawing.Graphics.FromImage((Image)bmp);
			// copy the original image on the canvas, and thus on the new bitmap,
			//  with the new size
			graphics.DrawImage(imagem, 0, 0, newSize.Width, newSize.Height);
			return (Image)bmp;
		}		

		private void updateImage(Image newImg)
		{
			if (pbImagem.Image != null)
				pbImagem.Image.Dispose();

			pbImagem.Image = newImg;
			if (pbImagem.Image.Size.Width > this.Size.Width & pbImagem.Image.Size.Height > this.Size.Height)
			{
				pbImagem.Size = pbImagem.Image.Size;
			}
			else
			{
				pbImagem.Size = this.Size;
			}
			// force a garbage collection to free the disposed memory
			GC.Collect();

			//        pbImagem.Size = pbImagem.Image.Size             
		}

		private void applyImageSwitch(ImageViewerEventArgs ea)
		{
			this.Imagem = ea.Imagem;
			this.Descricao = ea.Descricao;
			if (ea.ExistsPrevious)
			{
				ToolBarButtonPreviousImage.Enabled = true;
			}
			else
			{
				ToolBarButtonPreviousImage.Enabled = false;
			}

			if (ea.ExistsNext)
			{
				ToolBarButtonNextImage.Enabled = true;
			}
			else
			{
				ToolBarButtonNextImage.Enabled = false;
			}
		}

		private void GetExtraResources()
		{
			ToolBar1.ImageList = SharedResourcesOld.CurrentSharedResources.VisualizadorImagensImageList;
		}
	}

} //end of root namespace