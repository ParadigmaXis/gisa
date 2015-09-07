using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using System.IO;
using GISA.Model;
using System.Web;
using System.Net;
using System.ComponentModel;
using System.Drawing.Imaging;
using GISA.SharedResources;
using GISA.GUIHelper;

namespace GISA
{
	public class FormImagem : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

        private string gisaId = null;
        private Panel pnlImgDocInPorto;
        private Label lblNUD;
        private TextBox txtNUD;
        private Panel pnlNUD;
        private Panel pnlNomeFicheiro;
        private Label lblNomeFicheiro;
        private TextBox txtNomeFicheiro;
        private Dictionary<string, string> labelToPid = new Dictionary<string, string>();

		public FormImagem() : base()
		{
			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            btnFicheiro.Click += btnFicheiro_Click;
            txtLocalizacao.TextChanged += txtLocalizacao_TextChanged;
            txtDescricao.TextChanged += txtDescricao_TextChanged;

			GetExtraResources();
			PopulateUserInterface();
		}

        public FormImagem(string gisaId): this() {
            this.gisaId = gisaId;
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
		internal System.Windows.Forms.Button btnFicheiro;
		internal System.Windows.Forms.Button btnOk;
		internal System.Windows.Forms.Button btnCancel;
		internal System.Windows.Forms.TextBox txtDescricao;
		internal System.Windows.Forms.GroupBox grpDescricao;
		internal GISA.ImageViewerControl ImageViewerControl1;
		internal System.Windows.Forms.GroupBox grpLocalizacao;
		internal System.Windows.Forms.TextBox txtLocalizacao;
		internal System.Windows.Forms.GroupBox gbTipoArmazenamento;
        internal System.Windows.Forms.ComboBox cbTipoAcessoRecurso;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.btnFicheiro = new System.Windows.Forms.Button();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.grpDescricao = new System.Windows.Forms.GroupBox();
            this.grpLocalizacao = new System.Windows.Forms.GroupBox();
            this.pnlImgDocInPorto = new System.Windows.Forms.Panel();
            this.pnlNUD = new System.Windows.Forms.Panel();
            this.lblNUD = new System.Windows.Forms.Label();
            this.txtNUD = new System.Windows.Forms.TextBox();
            this.pnlNomeFicheiro = new System.Windows.Forms.Panel();
            this.lblNomeFicheiro = new System.Windows.Forms.Label();
            this.txtNomeFicheiro = new System.Windows.Forms.TextBox();
            this.txtLocalizacao = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbTipoAcessoRecurso = new System.Windows.Forms.ComboBox();
            this.gbTipoArmazenamento = new System.Windows.Forms.GroupBox();
            this.ImageViewerControl1 = new GISA.ImageViewerControl();
            this.grpDescricao.SuspendLayout();
            this.grpLocalizacao.SuspendLayout();
            this.pnlImgDocInPorto.SuspendLayout();
            this.pnlNUD.SuspendLayout();
            this.pnlNomeFicheiro.SuspendLayout();
            this.gbTipoArmazenamento.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFicheiro
            // 
            this.btnFicheiro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFicheiro.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFicheiro.Location = new System.Drawing.Point(439, 14);
            this.btnFicheiro.Name = "btnFicheiro";
            this.btnFicheiro.Size = new System.Drawing.Size(24, 24);
            this.btnFicheiro.TabIndex = 3;
            // 
            // txtDescricao
            // 
            this.txtDescricao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescricao.Location = new System.Drawing.Point(8, 16);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(455, 20);
            this.txtDescricao.TabIndex = 4;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(510, 158);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            // 
            // grpDescricao
            // 
            this.grpDescricao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDescricao.Controls.Add(this.txtDescricao);
            this.grpDescricao.Location = new System.Drawing.Point(8, 104);
            this.grpDescricao.Name = "grpDescricao";
            this.grpDescricao.Size = new System.Drawing.Size(475, 44);
            this.grpDescricao.TabIndex = 9;
            this.grpDescricao.TabStop = false;
            this.grpDescricao.Text = "Descrição";
            // 
            // grpLocalizacao
            // 
            this.grpLocalizacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpLocalizacao.Controls.Add(this.btnFicheiro);
            this.grpLocalizacao.Controls.Add(this.pnlImgDocInPorto);
            this.grpLocalizacao.Controls.Add(this.txtLocalizacao);
            this.grpLocalizacao.Location = new System.Drawing.Point(8, 56);
            this.grpLocalizacao.Name = "grpLocalizacao";
            this.grpLocalizacao.Size = new System.Drawing.Size(475, 44);
            this.grpLocalizacao.TabIndex = 9;
            this.grpLocalizacao.TabStop = false;
            this.grpLocalizacao.Text = "Localização";
            // 
            // pnlImgDocInPorto
            // 
            this.pnlImgDocInPorto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlImgDocInPorto.Controls.Add(this.pnlNUD);
            this.pnlImgDocInPorto.Controls.Add(this.pnlNomeFicheiro);
            this.pnlImgDocInPorto.Location = new System.Drawing.Point(8, 17);
            this.pnlImgDocInPorto.Name = "pnlImgDocInPorto";
            this.pnlImgDocInPorto.Size = new System.Drawing.Size(419, 20);
            this.pnlImgDocInPorto.TabIndex = 11;
            // 
            // pnlNUD
            // 
            this.pnlNUD.Controls.Add(this.lblNUD);
            this.pnlNUD.Controls.Add(this.txtNUD);
            this.pnlNUD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNUD.Location = new System.Drawing.Point(0, 0);
            this.pnlNUD.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.pnlNUD.Name = "pnlNUD";
            this.pnlNUD.Size = new System.Drawing.Size(195, 20);
            this.pnlNUD.TabIndex = 11;
            // 
            // lblNUD
            // 
            this.lblNUD.AutoSize = true;
            this.lblNUD.Location = new System.Drawing.Point(3, 3);
            this.lblNUD.Name = "lblNUD";
            this.lblNUD.Size = new System.Drawing.Size(34, 13);
            this.lblNUD.TabIndex = 0;
            this.lblNUD.Text = "NUD:";
            // 
            // txtNUD
            // 
            this.txtNUD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNUD.Location = new System.Drawing.Point(43, 0);
            this.txtNUD.Name = "txtNUD";
            this.txtNUD.Size = new System.Drawing.Size(152, 20);
            this.txtNUD.TabIndex = 1;
            // 
            // pnlNomeFicheiro
            // 
            this.pnlNomeFicheiro.Controls.Add(this.lblNomeFicheiro);
            this.pnlNomeFicheiro.Controls.Add(this.txtNomeFicheiro);
            this.pnlNomeFicheiro.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlNomeFicheiro.Location = new System.Drawing.Point(195, 0);
            this.pnlNomeFicheiro.Name = "pnlNomeFicheiro";
            this.pnlNomeFicheiro.Size = new System.Drawing.Size(224, 20);
            this.pnlNomeFicheiro.TabIndex = 12;
            // 
            // lblNomeFicheiro
            // 
            this.lblNomeFicheiro.AutoSize = true;
            this.lblNomeFicheiro.Location = new System.Drawing.Point(3, 3);
            this.lblNomeFicheiro.Name = "lblNomeFicheiro";
            this.lblNomeFicheiro.Size = new System.Drawing.Size(47, 13);
            this.lblNomeFicheiro.TabIndex = 0;
            this.lblNomeFicheiro.Text = "Ficheiro:";
            // 
            // txtNomeFicheiro
            // 
            this.txtNomeFicheiro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNomeFicheiro.Location = new System.Drawing.Point(56, 0);
            this.txtNomeFicheiro.Name = "txtNomeFicheiro";
            this.txtNomeFicheiro.Size = new System.Drawing.Size(168, 20);
            this.txtNomeFicheiro.TabIndex = 1;
            // 
            // txtLocalizacao
            // 
            this.txtLocalizacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocalizacao.Location = new System.Drawing.Point(8, 17);
            this.txtLocalizacao.Name = "txtLocalizacao";
            this.txtLocalizacao.Size = new System.Drawing.Size(419, 20);
            this.txtLocalizacao.TabIndex = 2;
            this.txtLocalizacao.WordWrap = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(594, 158);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancelar";
            // 
            // cbTipoAcessoRecurso
            // 
            this.cbTipoAcessoRecurso.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTipoAcessoRecurso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoAcessoRecurso.Location = new System.Drawing.Point(8, 16);
            this.cbTipoAcessoRecurso.Name = "cbTipoAcessoRecurso";
            this.cbTipoAcessoRecurso.Size = new System.Drawing.Size(172, 21);
            this.cbTipoAcessoRecurso.TabIndex = 1;
            this.cbTipoAcessoRecurso.SelectedIndexChanged += new System.EventHandler(this.cbTipoAcessoRecurso_SelectedIndexChanged);
            // 
            // gbTipoArmazenamento
            // 
            this.gbTipoArmazenamento.Controls.Add(this.cbTipoAcessoRecurso);
            this.gbTipoArmazenamento.Location = new System.Drawing.Point(8, 8);
            this.gbTipoArmazenamento.Name = "gbTipoArmazenamento";
            this.gbTipoArmazenamento.Size = new System.Drawing.Size(186, 44);
            this.gbTipoArmazenamento.TabIndex = 9;
            this.gbTipoArmazenamento.TabStop = false;
            this.gbTipoArmazenamento.Text = "Tipo";
            // 
            // ImageViewerControl1
            // 
            this.ImageViewerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageViewerControl1.Location = new System.Drawing.Point(495, 8);
            this.ImageViewerControl1.Name = "ImageViewerControl1";
            this.ImageViewerControl1.OtherLocationParams = null;
            this.ImageViewerControl1.Size = new System.Drawing.Size(176, 144);
            this.ImageViewerControl1.SourceLocation = null;
            this.ImageViewerControl1.TabIndex = 10;
            this.ImageViewerControl1.TipoAcessoRecurso = GISA.Model.ResourceAccessType.Smb;
            // 
            // FormImagem
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(685, 188);
            this.ControlBox = false;
            this.Controls.Add(this.ImageViewerControl1);
            this.Controls.Add(this.gbTipoArmazenamento);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpLocalizacao);
            this.Controls.Add(this.grpDescricao);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormImagem";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Imagem";
            this.grpDescricao.ResumeLayout(false);
            this.grpDescricao.PerformLayout();
            this.grpLocalizacao.ResumeLayout(false);
            this.grpLocalizacao.PerformLayout();
            this.pnlImgDocInPorto.ResumeLayout(false);
            this.pnlNUD.ResumeLayout(false);
            this.pnlNUD.PerformLayout();
            this.pnlNomeFicheiro.ResumeLayout(false);
            this.pnlNomeFicheiro.PerformLayout();
            this.gbTipoArmazenamento.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

		// Property usada para garantir o máximo possível que cada 
		// identificador especificado pelo utilizador é válido, ou 
		// seja, é um caminho que aponta para uma imagem.
		public string ValidLocation { get; set; }
        public string ValidLocationParams { get; set; }

		private int[] mLocalizacoes;
        public int[] Localizacoes { get { return mLocalizacoes; } set { mLocalizacoes = value; } }
		public string Descricao { get { return txtDescricao.Text; } set { txtDescricao.Text = value; } }
		public ResourceAccessType TipoAcessoRecurso { get { return TranslationHelper.FormatTipoAcessoTextToTipoAcessoEnum(cbTipoAcessoRecurso.SelectedItem.ToString()); } }
		public string Identificador { get { return txtLocalizacao.Text; } set { txtLocalizacao.Text = value; } }
        public string NomeFicheiroDIP { get { return txtNomeFicheiro.Text; } set { txtNomeFicheiro.Text = value; } }
        public string NUDDIP { get { return txtNUD.Text; } set { txtNUD.Text = value; } }

		private void GetExtraResources()
		{
			btnFicheiro.Image = SharedResourcesOld.CurrentSharedResources.ProcurarImagem;
		}

		private void PopulateUserInterface()
		{
			cbTipoAcessoRecurso.Items.Add(TranslationHelper.FormatTipoAcessoEnumToTipoAcessoText(ResourceAccessType.Web));

            if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsDocInPortoEnable())
            {
                cbTipoAcessoRecurso.Items.Add(TranslationHelper.FormatTipoAcessoEnumToTipoAcessoText(ResourceAccessType.DICAnexo));
                cbTipoAcessoRecurso.Items.Add(TranslationHelper.FormatTipoAcessoEnumToTipoAcessoText(ResourceAccessType.DICConteudo));
            }

            cbTipoAcessoRecurso.Items.Add(TranslationHelper.FormatTipoAcessoEnumToTipoAcessoText(ResourceAccessType.Smb));
			cbTipoAcessoRecurso.SelectedItem = TranslationHelper.FormatTipoAcessoEnumToTipoAcessoText(ResourceAccessType.Smb);
		}

		private void btnFicheiro_Click(object sender, System.EventArgs e)
		{
			if (TipoAcessoRecurso == ResourceAccessType.Smb)
				AbrirFicheiroSmb();
            else if (TipoAcessoRecurso == ResourceAccessType.Web)
				AbrirFicheiroWeb();
            else if (TipoAcessoRecurso == ResourceAccessType.DICAnexo || TipoAcessoRecurso == ResourceAccessType.DICConteudo)
                AbrirFicheiroDocInPorto();
		}

		private void txtLocalizacao_TextChanged(object sender, System.EventArgs e)
		{
			UpdateButtonState();
		}

		private void txtDescricao_TextChanged(object sender, System.EventArgs e)
		{
			// É passada a image para permitir a edição da descrição da imagem. Se for passado um argumento
			// com o valor nothing e se a localização for Web não é possível terminar a edição da informação
			// referente à imagem.
			UpdateButtonState(ImageViewerControl1.pictImagem.Image);
		}

		private void AbrirFicheiroSmb()
		{
			OpenFileDialog openFileDlg = new OpenFileDialog();

			string ficheiroEscolhido = txtLocalizacao.Text;
			try
			{
				FileInfo fi = new FileInfo(ficheiroEscolhido);
				if (fi.Exists)
					openFileDlg.FileName = fi.FullName;
				else
				{
					DirectoryInfo di = new DirectoryInfo(ficheiroEscolhido);
					if (! di.Exists)
					{
						if (! fi.Directory.Exists)
							openFileDlg.FileName = "";
						else
							openFileDlg.InitialDirectory = fi.Directory.FullName;
					}
					else
						openFileDlg.InitialDirectory = di.FullName + "\\\\";
				}
			}
			catch (ArgumentException)
			{
				openFileDlg.FileName = "";
			}

			openFileDlg.Title = "Alteração de imagem";
			openFileDlg.RestoreDirectory = true;
			openFileDlg.Multiselect = false;
			openFileDlg.Filter = "Todos (*.*)|*.*";
			if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				txtLocalizacao.Text = openFileDlg.FileName;

			    Image imagem = null;
			    try
			    {
				    imagem = ImageHelper.GetSmbImageResource(txtLocalizacao.Text);
			    }
			    catch (ImageHelper.UnretrievableResourceException)
			    {
				    ValidLocation = string.Empty;
			    }

			    ImageViewerControl1.UpdatePreviewImage(imagem, txtLocalizacao.Text, TipoAcessoRecurso);
			    if (imagem != null)
			    {
				    ValidLocation = txtLocalizacao.Text;
                    if (txtDescricao.Text.Equals("Imagem sem descrição"))
                        txtDescricao.Text = openFileDlg.SafeFileName.Split(new char[] {'.'})[0];
			    }
			    UpdateButtonState(imagem);
			}
		}

		private void AbrirFicheiroWeb()
		{
			this.Cursor = Cursors.WaitCursor;
			Image imagem = null;
			try
			{
				imagem = ImageHelper.GetWebImageResource(txtLocalizacao.Text);
				ImageViewerControl1.UpdatePreviewImage(imagem, txtLocalizacao.Text, TipoAcessoRecurso);
				if (imagem != null)
					ValidLocation = txtLocalizacao.Text;

				UpdateButtonState(imagem);
			}
			catch (ImageHelper.UnretrievableResourceException)
			{
				ValidLocation = string.Empty;
			}
			finally
			{
				this.Cursor = Cursors.Arrow;
			}
		}

        private void AbrirFicheiroDocInPorto()
        {
            this.Cursor = Cursors.WaitCursor;
            Image imagem = null;
            try
            {
                imagem = ImageHelper.GetDICImageResource(txtNomeFicheiro.Text, txtNUD.Text, TipoAcessoRecurso);
                ImageViewerControl1.UpdatePreviewImage(imagem, txtNomeFicheiro.Text, txtNUD.Text, TipoAcessoRecurso);
                if (imagem != null)
                {
                    ValidLocation = txtNomeFicheiro.Text;
                    ValidLocationParams = txtNUD.Text;
                }

                UpdateButtonState(imagem);
            }
            catch (ImageHelper.UnretrievableResourceException)
            {
                ValidLocation = string.Empty;
                ValidLocationParams = string.Empty;
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private void UpdateButtonState()
		{
			UpdateButtonState(null);
		}

		private void UpdateButtonState(Image imagem)
		{
            switch (TipoAcessoRecurso)
            {
                case ResourceAccessType.Smb:
                    btnOk.Enabled = txtDescricao.Text.Length > 0 && txtLocalizacao.Text.Length > 0 && txtLocalizacao.Text.Equals(ValidLocation);
                    break;
                case ResourceAccessType.Web:
                    btnOk.Enabled = txtDescricao.Text.Length > 0 && txtLocalizacao.Text.Length > 0 && imagem != null && txtLocalizacao.Text.Equals(ValidLocation);
                    break;
                case ResourceAccessType.DICAnexo:
                    btnOk.Enabled = txtDescricao.Text.Length > 0 &&  imagem != null && txtNomeFicheiro.Text.Equals(ValidLocation) && txtNUD.Text.Equals(ValidLocationParams);
                    break;
                case ResourceAccessType.DICConteudo:
                    btnOk.Enabled = txtDescricao.Text.Length > 0 && imagem != null && txtNUD.Text.Equals(ValidLocation);
                    break;
            }
		}

        private void cbTipoAcessoRecurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TipoAcessoRecurso == ResourceAccessType.Smb)
            {
                btnFicheiro.Image = SharedResourcesOld.CurrentSharedResources.ProcurarImagem;
                txtLocalizacao.Enabled = false;
                txtDescricao.Visible = true;
                pnlImgDocInPorto.Visible = false;
                txtLocalizacao.Clear();
            }
            else if (TipoAcessoRecurso == ResourceAccessType.DICAnexo || TipoAcessoRecurso == ResourceAccessType.DICConteudo)
            {
                btnFicheiro.Image = SharedResourcesOld.CurrentSharedResources.Actualizar;
                txtLocalizacao.Enabled = true;
                txtDescricao.Visible = true;
                txtLocalizacao.Clear();
                pnlImgDocInPorto.Visible = true;
                pnlNomeFicheiro.Visible = TipoAcessoRecurso == ResourceAccessType.DICAnexo;
            }
            else
            {
                btnFicheiro.Image = SharedResourcesOld.CurrentSharedResources.Actualizar;
                txtLocalizacao.Enabled = true;
                txtDescricao.Visible = true;
                txtLocalizacao.Clear();
                pnlImgDocInPorto.Visible = false;
            }
            UpdateButtonState();
        }
	}
} 