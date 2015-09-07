using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Windows.Forms;

using GISA.Fedora.FedoraHandler;
using GISA.GUIHelper;
using GISA.Model;
using GISA.Webservices.DocInPorto;

namespace GISA
{
	public class ImageViewerControl : System.Windows.Forms.UserControl
	{

	#region  Windows Form Designer generated code 

		public ImageViewerControl() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            pictImagem.DoubleClick += pictImagem_DoubleClick;
            pictImagem.Resize += pictImagem_Resize;
            MenuItemVisual.Click += MenuItemVisual_Click;
            MenuItemBrowse.Click += MenuItemBrowse_Click;
		}

		//UserControl overrides dispose to clean up the component list.
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
		internal System.Windows.Forms.GroupBox grpImagem;
		internal System.Windows.Forms.PictureBox pictImagem;
		internal System.Windows.Forms.Panel pnlImageViewer;
		internal System.Windows.Forms.MenuItem MenuItemVisual;
		internal System.Windows.Forms.MenuItem MenuItemBrowse;
		internal System.Windows.Forms.ContextMenu ContextMenuImagem;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.grpImagem = new System.Windows.Forms.GroupBox();
            this.pictImagem = new System.Windows.Forms.PictureBox();
            this.pnlImageViewer = new System.Windows.Forms.Panel();
            this.ContextMenuImagem = new System.Windows.Forms.ContextMenu();
            this.MenuItemVisual = new System.Windows.Forms.MenuItem();
            this.MenuItemBrowse = new System.Windows.Forms.MenuItem();
            this.grpImagem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictImagem)).BeginInit();
            this.pnlImageViewer.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpImagem
            // 
            this.grpImagem.BackColor = System.Drawing.SystemColors.Control;
            this.grpImagem.Controls.Add(this.pictImagem);
            this.grpImagem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpImagem.Location = new System.Drawing.Point(0, 0);
            this.grpImagem.Name = "grpImagem";
            this.grpImagem.Size = new System.Drawing.Size(150, 111);
            this.grpImagem.TabIndex = 2;
            this.grpImagem.TabStop = false;
            this.grpImagem.Text = "Imagem";
            // 
            // pictImagem
            // 
            this.pictImagem.BackColor = System.Drawing.SystemColors.Control;
            this.pictImagem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictImagem.Location = new System.Drawing.Point(3, 16);
            this.pictImagem.Name = "pictImagem";
            this.pictImagem.Size = new System.Drawing.Size(144, 92);
            this.pictImagem.TabIndex = 0;
            this.pictImagem.TabStop = false;
            // 
            // pnlImageViewer
            // 
            this.pnlImageViewer.BackColor = System.Drawing.SystemColors.Control;
            this.pnlImageViewer.Controls.Add(this.grpImagem);
            this.pnlImageViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImageViewer.Location = new System.Drawing.Point(0, 0);
            this.pnlImageViewer.Name = "pnlImageViewer";
            this.pnlImageViewer.Size = new System.Drawing.Size(150, 111);
            this.pnlImageViewer.TabIndex = 0;
            // 
            // ContextMenuImagem
            // 
            this.ContextMenuImagem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemVisual,
            this.MenuItemBrowse});
            // 
            // MenuItemVisual
            // 
            this.MenuItemVisual.DefaultItem = true;
            this.MenuItemVisual.Index = 0;
            this.MenuItemVisual.Text = "Abrir em Visualizador Interno";
            // 
            // MenuItemBrowse
            // 
            this.MenuItemBrowse.Index = 1;
            this.MenuItemBrowse.Text = "Abrir Externamente";
            // 
            // ImageViewerControl
            // 
            this.Controls.Add(this.pnlImageViewer);
            this.Name = "ImageViewerControl";
            this.Size = new System.Drawing.Size(150, 111);
            this.grpImagem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictImagem)).EndInit();
            this.pnlImageViewer.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

		// Redimensionar e apresentar a imagem

		public void UpdatePreviewImage(Image imagem, string srcLocation)
		{
			UpdatePreviewImage(imagem, srcLocation, null, ResourceAccessType.Smb);
		}

		public void UpdatePreviewImage(Image imagem)
		{
			UpdatePreviewImage(imagem, null, null, ResourceAccessType.Smb);
		}

        public void UpdatePreviewImage(Image imagem, string srcLocation, ResourceAccessType srcTytpe)
        {
            UpdatePreviewImage(imagem, srcLocation, null, srcTytpe);
        }

        public void UpdatePreviewImage(Image imagem, string srcLocation, string otherLocationParams, ResourceAccessType srcTytpe)
        {
            if (imagem == null)
            {
                pictImagem.Image = null;
                SourceLocation = "";
                OtherLocationParams = "";
                TipoAcessoRecurso = ResourceAccessType.Smb;
            }
            else
            {
                var newSize = ImageHelper.getSizeSameAspectRatio(imagem.Size, grpImagem.Size);
                pictImagem.Image = FormImageViewer.resizeImage(imagem, newSize);

                SourceLocation = srcLocation;
                OtherLocationParams = otherLocationParams;
                TipoAcessoRecurso = srcTytpe;
            }
        }

	#region Propriedades que mantêm referência à fonte da imagem
        public string OtherLocationParams { get; set; }
        public string SourceLocation { get; set; }

		private ResourceAccessType mTipoAcessoRecurso = ResourceAccessType.Smb;
		public ResourceAccessType TipoAcessoRecurso
		{
			get {return mTipoAcessoRecurso;}
			set {mTipoAcessoRecurso = value;}
		}
	#endregion

		private void pictImagem_DoubleClick(object sender, System.EventArgs e)
		{
			MenuItemVisual.PerformClick();
		}

		public delegate void controlerResizeEventEventHandler(object sender);
		public event controlerResizeEventEventHandler controlerResizeEvent;
		private void pictImagem_Resize(object sender, System.EventArgs e)
		{
			if (controlerResizeEvent != null)
				controlerResizeEvent(this);
		}

		public delegate void openFormImageViewerEventEventHandler(object sender);
		public event openFormImageViewerEventEventHandler openFormImageViewerEvent;
		private void MenuItemVisual_Click(object sender, System.EventArgs e)
		{
			ShellOpenImg();
		}

		private void MenuItemBrowse_Click(object sender, System.EventArgs e)
		{
			ShellOpenImg();
		}

		private void ShellOpenImg()
		{
            if (this.pictImagem == null) return;

            this.Cursor = Cursors.WaitCursor;
			try
			{
				string sUrl = null;
				System.Diagnostics.Process p = new System.Diagnostics.Process();

                switch (TipoAcessoRecurso)
                {
                    case ResourceAccessType.Web:
                        if (((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Select()[0])).URLBaseActivo)
                            sUrl = string.Format(((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Select()[0])).URLBase.Replace("<id>", "{0}"), SourceLocation);
                        else
                            sUrl = SourceLocation;
                        break;
                    case ResourceAccessType.Smb:
                        sUrl = SourceLocation;
                        break;
                    case ResourceAccessType.Fedora:
                        var configRow = GisaDataSetHelper.GetInstance().GlobalConfig.Cast<GISADataset.GlobalConfigRow>().Single();
                        var qualidade = configRow.IsQualidadeImagemNull() ? Quality.Low : FedoraHelper.TranslateQualityEnum(configRow.QualidadeImagem);
                        bool success;
                        string errorMessage;
                        sUrl = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.GetDisseminatorByUrl(SourceLocation, qualidade, out success, out errorMessage);
                        if (String.IsNullOrEmpty(sUrl) || !File.Exists(sUrl) || !success) sUrl = null;
                        if (!success) MessageBox.Show(errorMessage, "Repositório Digital", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case ResourceAccessType.DICAnexo:
                        sUrl = DocInPortoHelper.getDocInPortoAnexo(SourceLocation, OtherLocationParams);//
                        break;
                    case ResourceAccessType.DICConteudo:
                        sUrl = DocInPortoHelper.getDocInPortoConteudo(SourceLocation);
                        break;
                }					

				if (sUrl != null && sUrl.Length > 0)
					p = System.Diagnostics.Process.Start(sUrl);
			}
			catch (Win32Exception ex)
			{
				Trace.WriteLine(ex.ToString());
				if (ex.NativeErrorCode == 2)
				{
					//ERROR FILE NOT FOUND
					MessageBox.Show("O caminho especificado não é válido.", "Imagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
            this.Cursor = Cursors.Default;
		}
	}
}