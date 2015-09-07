using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using GISA.Fedora.FedoraHandler;
using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using System.Text;
using System.IO;
using GISA.SharedResources;
using GISA.GUIHelper;

namespace GISA
{
	public class PanelIndiceDocumento : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelIndiceDocumento() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

            //Add any initialization after the InitializeComponent() call
            lstVwIndiceDocumento.SelectedIndexChanged += lstVwIndiceDocumento_SelectedIndexChanged;
            lstVwIndiceDocumento.DragEnter += lstVwIndiceDocumento_DragEnter;
            lstVwIndiceDocumento.DragDrop += lstVwIndiceDocumento_DragDrop;
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnCima.Click += btnCima_Click;
            btnBaixo.Click += btnBaixo_Click;
            btnFim.Click += btnFim_Click;
            btnInicio.Click += btnInicio_Click;
            lstVwIndiceDocumento.BeforeLabelEdit += new LabelEditEventHandler(lstVwIndiceDocumento_BeforeLabelEdit);
            lstVwIndiceDocumento.AfterLabelEdit += lstVwIndiceDocumento_AfterLabelEdit;
            btnRemove.Click += btnRemove_Click;
            lstVwIndiceDocumento.KeyUp += lstVwIndiceDocumento_KeyUp;

			GetExtraResources();

            base.ParentChanged += PanelIndiceDocumento_ParentChanged;

			ImageViewerControl1.openFormImageViewerEvent += OpenFormImageViewer_action;
			ImageViewerControl1.controlerResizeEvent += ControlerResize_action;

            this.controlFedoraPdfViewer1.SetupOtherMode();
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
        internal System.Windows.Forms.GroupBox GroupBox1;
        private ControlFedoraPdfViewer controlFedoraPdfViewer1;
        private SplitContainer splitContainer1;
        private GroupBox grpVisualizacao;
        internal GroupBox grpObjectosDigitais;
        internal ListView lstVwIndiceDocumento;
        internal ColumnHeader colDescricao;
        internal ColumnHeader colPath;
        internal ColumnHeader colFicheiro;
        internal Button btnFim;
        internal Button btnAdd;
        internal Button btnInicio;
        internal Button btnEdit;
        internal Button btnBaixo;
        internal Button btnRemove;
        internal Button btnCima;
		internal GISA.ImageViewerControl ImageViewerControl1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grpObjectosDigitais = new System.Windows.Forms.GroupBox();
            this.lstVwIndiceDocumento = new System.Windows.Forms.ListView();
            this.colDescricao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFicheiro = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnFim = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnInicio = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnBaixo = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnCima = new System.Windows.Forms.Button();
            this.grpVisualizacao = new System.Windows.Forms.GroupBox();
            this.ImageViewerControl1 = new GISA.ImageViewerControl();
            this.controlFedoraPdfViewer1 = new GISA.ControlFedoraPdfViewer();
            this.GroupBox1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpObjectosDigitais.SuspendLayout();
            this.grpVisualizacao.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.Controls.Add(this.splitContainer1);
            this.GroupBox1.Location = new System.Drawing.Point(3, 3);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(794, 594);
            this.GroupBox1.TabIndex = 4;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "3.*. Objetos digitais";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grpObjectosDigitais);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpVisualizacao);
            this.splitContainer1.Size = new System.Drawing.Size(788, 575);
            this.splitContainer1.SplitterDistance = 511;
            this.splitContainer1.TabIndex = 11;
            // 
            // grpObjectosDigitais
            // 
            this.grpObjectosDigitais.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpObjectosDigitais.Controls.Add(this.lstVwIndiceDocumento);
            this.grpObjectosDigitais.Controls.Add(this.btnFim);
            this.grpObjectosDigitais.Controls.Add(this.btnAdd);
            this.grpObjectosDigitais.Controls.Add(this.btnInicio);
            this.grpObjectosDigitais.Controls.Add(this.btnEdit);
            this.grpObjectosDigitais.Controls.Add(this.btnBaixo);
            this.grpObjectosDigitais.Controls.Add(this.btnRemove);
            this.grpObjectosDigitais.Controls.Add(this.btnCima);
            this.grpObjectosDigitais.Location = new System.Drawing.Point(0, 0);
            this.grpObjectosDigitais.Name = "grpObjectosDigitais";
            this.grpObjectosDigitais.Size = new System.Drawing.Size(511, 575);
            this.grpObjectosDigitais.TabIndex = 10;
            this.grpObjectosDigitais.TabStop = false;
            this.grpObjectosDigitais.Text = "Objetos digitais";
            // 
            // lstVwIndiceDocumento
            // 
            this.lstVwIndiceDocumento.AllowColumnReorder = true;
            this.lstVwIndiceDocumento.AllowDrop = true;
            this.lstVwIndiceDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwIndiceDocumento.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDescricao,
            this.colPath,
            this.colFicheiro});
            this.lstVwIndiceDocumento.FullRowSelect = true;
            this.lstVwIndiceDocumento.HideSelection = false;
            this.lstVwIndiceDocumento.LabelEdit = true;
            this.lstVwIndiceDocumento.Location = new System.Drawing.Point(6, 19);
            this.lstVwIndiceDocumento.Name = "lstVwIndiceDocumento";
            this.lstVwIndiceDocumento.Size = new System.Drawing.Size(469, 550);
            this.lstVwIndiceDocumento.TabIndex = 17;
            this.lstVwIndiceDocumento.UseCompatibleStateImageBehavior = false;
            this.lstVwIndiceDocumento.View = System.Windows.Forms.View.Details;
            // 
            // colDescricao
            // 
            this.colDescricao.Text = "Descrição";
            this.colDescricao.Width = 360;
            // 
            // colPath
            // 
            this.colPath.Text = "Localização";
            this.colPath.Width = 100;
            // 
            // colFicheiro
            // 
            this.colFicheiro.Text = "Identificador";
            this.colFicheiro.Width = 250;
            // 
            // btnFim
            // 
            this.btnFim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFim.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFim.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFim.Location = new System.Drawing.Point(481, 234);
            this.btnFim.Name = "btnFim";
            this.btnFim.Size = new System.Drawing.Size(24, 24);
            this.btnFim.TabIndex = 24;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(481, 36);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(24, 24);
            this.btnAdd.TabIndex = 18;
            // 
            // btnInicio
            // 
            this.btnInicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInicio.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInicio.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInicio.Location = new System.Drawing.Point(481, 144);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(24, 24);
            this.btnInicio.TabIndex = 23;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEdit.Location = new System.Drawing.Point(481, 66);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(24, 24);
            this.btnEdit.TabIndex = 19;
            // 
            // btnBaixo
            // 
            this.btnBaixo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBaixo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBaixo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBaixo.Location = new System.Drawing.Point(481, 204);
            this.btnBaixo.Name = "btnBaixo";
            this.btnBaixo.Size = new System.Drawing.Size(24, 24);
            this.btnBaixo.TabIndex = 22;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(481, 96);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(24, 24);
            this.btnRemove.TabIndex = 20;
            // 
            // btnCima
            // 
            this.btnCima.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCima.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCima.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCima.Location = new System.Drawing.Point(481, 174);
            this.btnCima.Name = "btnCima";
            this.btnCima.Size = new System.Drawing.Size(24, 24);
            this.btnCima.TabIndex = 21;
            // 
            // grpVisualizacao
            // 
            this.grpVisualizacao.Controls.Add(this.ImageViewerControl1);
            this.grpVisualizacao.Controls.Add(this.controlFedoraPdfViewer1);
            this.grpVisualizacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpVisualizacao.Location = new System.Drawing.Point(0, 0);
            this.grpVisualizacao.Name = "grpVisualizacao";
            this.grpVisualizacao.Size = new System.Drawing.Size(273, 575);
            this.grpVisualizacao.TabIndex = 3;
            this.grpVisualizacao.TabStop = false;
            this.grpVisualizacao.Text = "Visualização";
            // 
            // ImageViewerControl1
            // 
            this.ImageViewerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageViewerControl1.Location = new System.Drawing.Point(3, 16);
            this.ImageViewerControl1.Name = "ImageViewerControl1";
            this.ImageViewerControl1.OtherLocationParams = null;
            this.ImageViewerControl1.Size = new System.Drawing.Size(267, 556);
            this.ImageViewerControl1.SourceLocation = null;
            this.ImageViewerControl1.TabIndex = 2;
            this.ImageViewerControl1.TipoAcessoRecurso = GISA.Model.ResourceAccessType.Smb;
            // 
            // controlFedoraPdfViewer1
            // 
            this.controlFedoraPdfViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlFedoraPdfViewer1.Location = new System.Drawing.Point(3, 16);
            this.controlFedoraPdfViewer1.Name = "controlFedoraPdfViewer1";
            this.controlFedoraPdfViewer1.Qualidade = "Baixa";
            this.controlFedoraPdfViewer1.Size = new System.Drawing.Size(267, 556);
            this.controlFedoraPdfViewer1.TabIndex = 0;
            // 
            // PanelIndiceDocumento
            // 
            this.Controls.Add(this.GroupBox1);
            this.Name = "PanelIndiceDocumento";
            this.GroupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.grpObjectosDigitais.ResumeLayout(false);
            this.grpVisualizacao.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

		internal FormImageViewer frmImgViewer = null;

		private void GetExtraResources()
		{
			btnAdd.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
			btnEdit.Image = SharedResourcesOld.CurrentSharedResources.Editar;
			btnRemove.Image = SharedResourcesOld.CurrentSharedResources.Apagar;
			btnCima.Image = SharedResourcesOld.CurrentSharedResources.PrioridadeAumentar;
			btnBaixo.Image = SharedResourcesOld.CurrentSharedResources.PrioridadeDiminuir;
            btnInicio.Image = SharedResourcesOld.CurrentSharedResources.PrioridadeMax;
            btnFim.Image = SharedResourcesOld.CurrentSharedResources.PrioridadeMin;
		}

		// runs only once. sets tooltip as soon as it's parent appears
		private void PanelIndiceDocumento_ParentChanged(object sender, System.EventArgs e)
		{
			MultiPanel.CurrentToolTip.SetToolTip(btnAdd, SharedResourcesOld.CurrentSharedResources.AdicionarString);
			MultiPanel.CurrentToolTip.SetToolTip(btnEdit, SharedResourcesOld.CurrentSharedResources.EditarString);
			MultiPanel.CurrentToolTip.SetToolTip(btnRemove, SharedResourcesOld.CurrentSharedResources.ApagarString);
			MultiPanel.CurrentToolTip.SetToolTip(btnCima, SharedResourcesOld.CurrentSharedResources.PrioridadeAumentarString);
			MultiPanel.CurrentToolTip.SetToolTip(btnBaixo, SharedResourcesOld.CurrentSharedResources.PrioridadeDiminuirString);
            MultiPanel.CurrentToolTip.SetToolTip(btnInicio, SharedResourcesOld.CurrentSharedResources.PrioridadeMaxString);
            MultiPanel.CurrentToolTip.SetToolTip(btnFim, SharedResourcesOld.CurrentSharedResources.PrioridadeMinString);

			base.ParentChanged -= PanelIndiceDocumento_ParentChanged;
		}

		private Image mImagem = null;
		private Image ImagemEscolhida
		{
			get {return mImagem;}
			set {mImagem = value;}
		}

		private GISADataset.FRDBaseRow CurrentFRDBase;
		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			CurrentFRDBase = (GISADataset.FRDBaseRow)CurrentDataRow;

            var rhRow = CurrentFRDBase.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First();

			FRDRule.Current.LoadIndiceDocumentoData(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, conn);
            
			IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = false;
			RefreshImagesList();
			RefreshDetails();
			RefreshButtonsState();
            //SelectFirstImage();

			IsPopulated = true;
		}

		public override void ViewToModel()
		{
			if (CurrentFRDBase == null || CurrentFRDBase.RowState == DataRowState.Detached || ! IsLoaded)
				return;

			GisaDataSetHelper.ManageDatasetConstraints(false);

			// actualizar as "ordens"
			GISADataset.SFRDImagemRow imgRow = null;
			int ordem = 1;
			foreach (ListViewItem item in lstVwIndiceDocumento.Items)
			{
				imgRow = (GISADataset.SFRDImagemRow)item.Tag;

				imgRow.GUIOrder = ordem;
				ordem = ordem + 1;
			}
			try
			{
				GisaDataSetHelper.ManageDatasetConstraints(true);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw ex;
			}
		}

		public override void Deactivate()
		{
			lstVwIndiceDocumento.SelectedItems.Clear();
            controlFedoraPdfViewer1.Clear();
			OnHidePanel();
		}

		private void lstVwIndiceDocumento_SelectedIndexChanged(object sender, EventArgs e)
		{
            this.Cursor = Cursors.WaitCursor;

            ImageViewerControl1.BringToFront();

            RefreshDetails();
            RefreshButtonsState();

            this.Cursor = Cursors.Default;
		}

		private void ControlerResize_action(object sender)
		{
            if (ImagemEscolhida == null || lstVwIndiceDocumento.SelectedItems.Count == 0)
				return;

			GISADataset.SFRDImagemRow imgRow = (GISADataset.SFRDImagemRow)(lstVwIndiceDocumento.SelectedItems[0].Tag);
			string caminhoFicheiro = imgRow.SFRDImagemVolumeRow.Mount + imgRow.Identificador;
			ImageViewerControl1.UpdatePreviewImage(ImagemEscolhida, caminhoFicheiro, TranslationHelper.FormatTipoAcessoTextToTipoAcessoEnum(imgRow.Tipo));
		}

		private void lstVwIndiceDocumento_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{

			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void lstVwIndiceDocumento_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				foreach (string imgPath in (string[])(e.Data.GetData(DataFormats.FileDrop)))
                    AddIndiceDocumento(getFilenameFromFullPath(imgPath, ResourceAccessType.Smb).Split(new char[] {'.'})[0], getPathFromFullPath(imgPath, ResourceAccessType.Smb), getFilenameFromFullPath(imgPath, ResourceAccessType.Smb), ResourceAccessType.Smb);
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
            FormImagem form = new FormImagem(CurrentFRDBase.IDNivel.ToString());
			form.Text = "Adicionar Imagem / Objeto Digital";
			form.Descricao = "Imagem sem descrição";
			form.Identificador = string.Empty;

            if (form.ShowDialog() == DialogResult.OK) 
            {
                if(form.TipoAcessoRecurso == ResourceAccessType.DICAnexo)
                    AddIndiceDocumento(form.Descricao, form.NUDDIP, form.NomeFicheiroDIP, form.TipoAcessoRecurso);
                else if (form.TipoAcessoRecurso == ResourceAccessType.DICConteudo)
                    AddIndiceDocumento(form.Descricao, null, form.NUDDIP, form.TipoAcessoRecurso);
                else {
                    string filePath = getPathFromFullPath(form.Identificador, form.TipoAcessoRecurso);
                    string fileName = getFilenameFromFullPath(form.Identificador, form.TipoAcessoRecurso);
                    AddIndiceDocumento(form.Descricao, filePath, fileName, form.TipoAcessoRecurso);
                }
            }
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (lstVwIndiceDocumento.SelectedItems.Count == 1)
			{
				GISADataset.SFRDImagemRow imgRow = null;
				imgRow = (GISADataset.SFRDImagemRow)(lstVwIndiceDocumento.SelectedItems[0].Tag);

                FormImagem form = new FormImagem(CurrentFRDBase.IDNivel.ToString());
				form.Text = "Alterar Imagem / Objeto Digital";
				form.Descricao = imgRow.Descricao;
				form.cbTipoAcessoRecurso.SelectedItem = imgRow.Tipo;
                if (imgRow.Tipo.Equals(TranslationHelper.FormatTipoAcessoEnumToTipoAcessoText(ResourceAccessType.DICAnexo))) {
                    form.NomeFicheiroDIP = imgRow.Identificador;
                    form.NUDDIP = imgRow.SFRDImagemVolumeRow.Mount;
                    form.ValidLocation = form.NomeFicheiroDIP;
                    form.ValidLocationParams = form.NUDDIP;
                }
                else if (imgRow.Tipo.Equals(TranslationHelper.FormatTipoAcessoEnumToTipoAcessoText(ResourceAccessType.DICConteudo)))
                {
                    form.NUDDIP = imgRow.Identificador;
                    form.ValidLocation = form.NUDDIP;
                }
                else
                {
                    form.Identificador = imgRow.SFRDImagemVolumeRow.Mount + imgRow.Identificador;
                    form.ValidLocation = form.Identificador;
                }

				Image currentImage = null;
				Size currentImageSize = new Size();
				Size viewportSize = new Size();
				currentImage = ImageViewerControl1.pictImagem.Image;

				if (currentImage != null)
				{
					currentImageSize = currentImage.Size;
					viewportSize = form.ImageViewerControl1.grpImagem.Size;

					Size newSize = ImageHelper.getSizeSameAspectRatio(currentImageSize, viewportSize);
					Image newImg = FormImageViewer.resizeImage(currentImage, newSize);

					form.ImageViewerControl1.pictImagem.Image = newImg;
					form.ImageViewerControl1.pictImagem.Size = form.ImageViewerControl1.grpImagem.Size;
				}

				if (form.ShowDialog() == DialogResult.OK)
				{
                    string identificador, descricao, caminho;
                    identificador = form.Identificador;
                    descricao = form.Descricao;
                    caminho = form.Identificador;

					ListViewItem item = null;
					item = lstVwIndiceDocumento.SelectedItems[0];
					item.SubItems[0].Text = descricao;
                    item.SubItems[1].Text = getPathFromFullPath(caminho, form.TipoAcessoRecurso);
                    item.SubItems[2].Text = getFilenameFromFullPath(identificador, form.TipoAcessoRecurso);
					ViewToModel(item, TranslationHelper.FormatTipoAcessoEnumToTipoAcessoText(form.TipoAcessoRecurso));
					RefreshDetails();
					RefreshButtonsState();
				}
			}
		}

		private void btnCima_Click(object sender, System.EventArgs e)
		{
            List<ListViewItem> selectedItems = new List<ListViewItem>();            
            foreach (ListViewItem lvItem in lstVwIndiceDocumento.SelectedItems)
            {
                if (lvItem.Index == 0)
                    return;

                selectedItems.Add(lvItem);
            }

            lstVwIndiceDocumento.BeginUpdate();
            int index = 0;
            foreach (ListViewItem lvItem in selectedItems)
            {
                index = lstVwIndiceDocumento.Items.IndexOf(lvItem);
                lstVwIndiceDocumento.Items.Remove(lvItem);                
                lstVwIndiceDocumento.Items.Insert(index - 1, lvItem);
            }
            lstVwIndiceDocumento.EndUpdate();
		}

		private void btnBaixo_Click(object sender, System.EventArgs e)
		{
            List<ListViewItem> selectedItems = new List<ListViewItem>();
            foreach (ListViewItem lvItem in lstVwIndiceDocumento.SelectedItems)
            {
                if (lvItem.Index == lstVwIndiceDocumento.Items.Count - 1)
				    return;

                selectedItems.Add(lvItem);
            }

            selectedItems.Reverse();

            lstVwIndiceDocumento.BeginUpdate();
            int index = 0;
            foreach (ListViewItem lvItem in selectedItems)
            {
                index = lstVwIndiceDocumento.Items.IndexOf(lvItem);
                lstVwIndiceDocumento.Items.Remove(lvItem);                
                lstVwIndiceDocumento.Items.Insert(index + 1, lvItem);
            }
            lstVwIndiceDocumento.EndUpdate();
		}

        private void btnInicio_Click(object sender, EventArgs e)
        {
            List<ListViewItem> selectedItems = new List<ListViewItem>();
            foreach (ListViewItem lvItem in lstVwIndiceDocumento.SelectedItems)
            {
                if (lvItem.Index == 0)
                    return;

                selectedItems.Add(lvItem);
            }

            lstVwIndiceDocumento.BeginUpdate();
            int index = 0;
            foreach (ListViewItem lvItem in selectedItems)
            {
                lstVwIndiceDocumento.Items.Remove(lvItem);
                lstVwIndiceDocumento.Items.Insert(index, lvItem);
                index++;
            }
            lstVwIndiceDocumento.EndUpdate();
        }

        private void btnFim_Click(object sender, EventArgs e)
        {
            List<ListViewItem> selectedItems = new List<ListViewItem>();
            foreach (ListViewItem lvItem in lstVwIndiceDocumento.SelectedItems)
            {
                if (lvItem.Index == lstVwIndiceDocumento.Items.Count - 1)
                    return;

                selectedItems.Add(lvItem);
            }

            lstVwIndiceDocumento.BeginUpdate();
            foreach (ListViewItem lvItem in selectedItems)
            {
                lstVwIndiceDocumento.Items.Remove(lvItem);
                lstVwIndiceDocumento.Items.Insert(lstVwIndiceDocumento.Items.Count, lvItem);
            }
            lstVwIndiceDocumento.EndUpdate();
        }

        void lstVwIndiceDocumento_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            var imgRow = lstVwIndiceDocumento.Items[e.Item].Tag as GISADataset.SFRDImagemRow;
            if (imgRow.Tipo.Equals(FedoraHelper.typeFedora))
                e.CancelEdit = true;
        }

		private void lstVwIndiceDocumento_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			if (e.Label == null)
				e.CancelEdit = true;
			else
			{
				lstVwIndiceDocumento.Items[e.Item].Text = e.Label;
				ViewToModel(lstVwIndiceDocumento.Items[e.Item]);
			}
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
            GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwIndiceDocumento, OnDeletingIndice);
		}

		private void lstVwIndiceDocumento_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == Convert.ToInt32(Keys.Delete))
                GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwIndiceDocumento, OnDeletingIndice);
		}

		private ArrayList mDeletedImages = new ArrayList();
		private void OnDeletingIndice(DataRow row)
		{
			mDeletedImages.Add(row);
		}

		private void AddIndiceDocumento(string descricao, string caminhoFicheiro, string nomeFicheiro, ResourceAccessType tipoAcessoRecurso)
		{
            if ((tipoAcessoRecurso == ResourceAccessType.Smb || tipoAcessoRecurso == ResourceAccessType.Web) && !(ImageHelper.isValidImageResource(caminhoFicheiro + nomeFicheiro, tipoAcessoRecurso)))
                return;

            if ((tipoAcessoRecurso == ResourceAccessType.DICAnexo || tipoAcessoRecurso == ResourceAccessType.DICConteudo) && !(ImageHelper.isValidImageResource(caminhoFicheiro, nomeFicheiro, tipoAcessoRecurso)))
                return;

            GISADataset.SFRDImagemVolumeRow imgVolRow = getImagemVolume(caminhoFicheiro);

			long maxOrdem = GetImgMaxOrdem();

			GISADataset.SFRDImagemRow imgRow = null;
            imgRow = GisaDataSetHelper.GetInstance().SFRDImagem.AddSFRDImagemRow(CurrentFRDBase, maxOrdem + 1, TranslationHelper.FormatTipoAcessoEnumToTipoAcessoText(tipoAcessoRecurso), descricao, imgVolRow, nomeFicheiro, new byte[] { }, 0);

			ListViewItem item = null;
			item = lstVwIndiceDocumento.Items.Add(descricao);
			item.SubItems.Add(caminhoFicheiro);
			item.SubItems.Add(nomeFicheiro);
			item.Tag = imgRow;
		}

		private long GetImgMaxOrdem()
		{
			long maxOrdem = 0;
			try
			{
				maxOrdem = System.Convert.ToInt64(GisaDataSetHelper.GetInstance().SFRDImagem. Compute("Max(GUIOrder)", null));
			}
			catch
			{
				maxOrdem = 0;
			}
			return maxOrdem;
		}

		private GISADataset.SFRDImagemVolumeRow getImagemVolume(string caminhoFicheiro)
		{
            if (caminhoFicheiro == null || caminhoFicheiro.Length == 0) return null;
			byte[] Versao = null;
			// search SFRDImagemVolume for the image's path. If it
			// does not yet exist create a new one.
			GISADataset.SFRDImagemVolumeRow[] imgVolRows = null;
			GISADataset.SFRDImagemVolumeRow imgVolRow = null;
			imgVolRows = (GISADataset.SFRDImagemVolumeRow[])(GisaDataSetHelper.GetInstance(). SFRDImagemVolume.Select("Mount = '" + caminhoFicheiro.Replace("'","''") + "'"));

			//If the volume already exists use the existing entry
			if (imgVolRows.Length > 0)
				imgVolRow = imgVolRows[0];
			else
			{   
                //Create a new entry for the new path
				imgVolRow = GisaDataSetHelper.GetInstance().SFRDImagemVolume. AddSFRDImagemVolumeRow(caminhoFicheiro, Versao, 0);
			}
			return imgVolRow;
		}

		private void RefreshButtonsState()
		{
            if (lstVwIndiceDocumento.SelectedItems.Count == 1)
			{
                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
			}
			else if (lstVwIndiceDocumento.SelectedItems.Count > 1)
			{
                btnEdit.Enabled = false;
                btnRemove.Enabled = true;
			}
			else
			{
				btnEdit.Enabled = false;
				btnRemove.Enabled = false;
			}
		}

		private void RefreshImagesList()
		{
			// populate images' paths and filenames
			lstVwIndiceDocumento.Items.Clear();
            foreach (var imgRow in CurrentFRDBase.GetSFRDImagemRows().Where(r => !r.Tipo.Equals(FedoraHelper.typeFedora)).OrderBy(r => r.GUIOrder))
            {
                if (imgRow.IsDescricaoNull())
                    imgRow.Descricao = string.Empty;

                var item = new ListViewItem();
                item.SubItems.AddRange(new string[] { string.Empty, string.Empty, string.Empty });
                item.Tag = imgRow;
                item.SubItems[colDescricao.Index].Text = imgRow.Descricao;
                item.SubItems[colFicheiro.Index].Text = imgRow.Identificador;
                item.SubItems[colPath.Index].Text = imgRow.SFRDImagemVolumeRow.Mount;
                lstVwIndiceDocumento.Items.Add(item);
            }
		}

        private void RefreshDetails()
		{
            ImageViewerControl1.BringToFront();
            if (lstVwIndiceDocumento.SelectedItems.Count != 1)
			{
                ClearPreview();
                return;
			}

            GISADataset.SFRDImagemRow imgRow = (GISADataset.SFRDImagemRow)(lstVwIndiceDocumento.SelectedItems[0].Tag);
            RefreshPreview(imgRow);
		}

        private void SelectFirstImage()
        {
            if (lstVwIndiceDocumento.Items.Count > 0)
            {
                lstVwIndiceDocumento.Focus();
                lstVwIndiceDocumento.Items[0].Selected = true;
            }
        }

        private void ClearPreview() {
            ImageViewerControl1.pictImagem.Image = null;
            ImageViewerControl1.SourceLocation = string.Empty;
            ImageViewerControl1.OtherLocationParams = string.Empty;
        }

        private void RefreshPreview(GISADataset.SFRDImagemRow imgRow)
        {
            Image imagem = null;
            string caminhoFicheiro = string.Empty;
            string outroParametro = string.Empty;
            var tipo = TranslationHelper.FormatTipoAcessoTextToTipoAcessoEnum(imgRow.Tipo);

            try
            {
                switch (tipo)
                {
                    case ResourceAccessType.Smb:
                        caminhoFicheiro = imgRow.SFRDImagemVolumeRow.Mount + imgRow.Identificador;
                        imagem = ImageHelper.GetSmbImageResource(caminhoFicheiro);
                        ImageViewerControl1.UpdatePreviewImage(imagem, caminhoFicheiro, tipo);
                        break;
                    case ResourceAccessType.Web:
                        caminhoFicheiro = imgRow.SFRDImagemVolumeRow.Mount + imgRow.Identificador;
                        imagem = ImageHelper.GetWebImageResource(caminhoFicheiro);
                        ImageViewerControl1.UpdatePreviewImage(imagem, caminhoFicheiro, tipo);
                        break;
                    case ResourceAccessType.DICAnexo:
                    case ResourceAccessType.DICConteudo:
                        imagem = ImageHelper.GetDICImageResource(imgRow.Identificador, imgRow.SFRDImagemVolumeRow.Mount, tipo);
                        ImageViewerControl1.UpdatePreviewImage(imagem, imgRow.Identificador, imgRow.SFRDImagemVolumeRow.Mount, tipo);
                        break;
                }
            }
            catch (ImageHelper.UnretrievableResourceException ex) { Trace.WriteLine(ex.ToString()); }

            ImagemEscolhida = imagem;
            ImageViewerControl1.Visible = true;
        }

		private void OpenFormImageViewer_action(object sender)
		{
			if (lstVwIndiceDocumento.SelectedItems.Count != 0)
			{
				GISADataset.SFRDImagemRow sfrdimg = (GISADataset.SFRDImagemRow)(lstVwIndiceDocumento.SelectedItems[0].Tag);
				if (ImagemEscolhida != null)
				{
					frmImgViewer = new FormImageViewer();
                    frmImgViewer.NextImage += FormImageViewer_NextImage;
                    frmImgViewer.PreviousImage += FormImageViewer_PreviousImage;
					frmImgViewer.Imagem = ImagemEscolhida;
					frmImgViewer.Descricao = sfrdimg.Descricao;
					if (lstVwIndiceDocumento.SelectedIndices.Count > 0)
					{
						frmImgViewer.ToolBarButtonPreviousImage.Enabled = lstVwIndiceDocumento.SelectedIndices[0] > 0;
						frmImgViewer.ToolBarButtonNextImage.Enabled = lstVwIndiceDocumento.SelectedIndices[0] < lstVwIndiceDocumento.Items.Count - 1;
					}
					else
					{
						frmImgViewer.ToolBarButtonPreviousImage.Enabled = false;
						frmImgViewer.ToolBarButtonNextImage.Enabled = false;
					}
					frmImgViewer.ShowDialog();
                    frmImgViewer.NextImage -= FormImageViewer_NextImage;
                    frmImgViewer.PreviousImage -= FormImageViewer_PreviousImage;
					frmImgViewer.Dispose();
					frmImgViewer = null;
				}
			}
		}

		private void FormImageViewer_NextImage(object sender, FormImageViewer.ImageViewerEventArgs e)
		{
			// make sure an item is seleted and that there is a next item to select
			if (lstVwIndiceDocumento.SelectedIndices.Count != 0 && lstVwIndiceDocumento.SelectedIndices[0] < lstVwIndiceDocumento.Items.Count - 1)
			{
				ListViewItem selItem = lstVwIndiceDocumento.Items[lstVwIndiceDocumento.SelectedIndices[0] + 1];
				GISADataset.SFRDImagemRow sfrdimg = (GISADataset.SFRDImagemRow)selItem.Tag;
				selItem.Selected = true;

				e.Imagem = ImagemEscolhida;
				e.Descricao = sfrdimg.Descricao;
				e.ExistsPrevious = true;
				e.ExistsNext = (lstVwIndiceDocumento.SelectedIndices[0] < lstVwIndiceDocumento.Items.Count - 1);
			}
		}

		private void FormImageViewer_PreviousImage(object sender, FormImageViewer.ImageViewerEventArgs e)
		{
			// make sure an item is seleted and that there is a previous
			// item to select
			if (lstVwIndiceDocumento.SelectedIndices.Count != 0 && lstVwIndiceDocumento.SelectedIndices[0] > 0)
			{
				ListViewItem selItem = lstVwIndiceDocumento.Items[lstVwIndiceDocumento.SelectedIndices[0] - 1];
				GISADataset.SFRDImagemRow sfrdimg = (GISADataset.SFRDImagemRow)selItem.Tag;
				selItem.Selected = true;

				e.Imagem = ImagemEscolhida;
				e.Descricao = sfrdimg.Descricao;
				e.ExistsPrevious = (lstVwIndiceDocumento.SelectedIndices[0] > 0);
				e.ExistsNext = true;
			}
		}

		private void ViewToModel(ListViewItem item)
		{
			ViewToModel(item, null);
		}

		private void ViewToModel(ListViewItem item, string localizacao)
		{
			GISADataset.SFRDImagemRow imgRow = null;
			imgRow = (GISADataset.SFRDImagemRow)item.Tag;

			if (! (imgRow.Descricao.Equals(item.Text)))
				imgRow.Descricao = item.Text;

			if (localizacao != null)
				imgRow.Tipo = localizacao;

            if (!imgRow.IsIDSFDImagemVolumeNull())
            {
                // if the volume was changed then other volume entry should be used
                if (!(imgRow.SFRDImagemVolumeRow.Mount.Equals(item.SubItems[1].Text)))
                {
                    // find the intended volume or create one if it does not yet exist
                    // change imgRow.SFRDImagemVolumeRow to use it
                    imgRow.SFRDImagemVolumeRow = getImagemVolume(item.SubItems[1].Text);
                }
            }

			// if the filename was changed update it
			if (! (imgRow.Identificador.Equals(item.SubItems[2].Text)))
				imgRow.Identificador = item.SubItems[2].Text;
		}

		private string getFilenameFromFullPath(string FullPath, ResourceAccessType tipoAcessoRecurso)
		{
            switch (tipoAcessoRecurso)
            {
                case ResourceAccessType.Smb:
                    return new FileInfo(FullPath).Name;
                case ResourceAccessType.Web:
                    string[] a = FullPath.Split('/');
				    return a[a.Length - 1];
                case ResourceAccessType.DICAnexo:
                case ResourceAccessType.DICConteudo:
                    return FullPath;
            }
            return string.Empty;
		}

		private string getPathFromFullPath(string FullPath, ResourceAccessType tipoAcessoRecurso)
		{
            switch (tipoAcessoRecurso)
            {
                case ResourceAccessType.Smb:
                    DirectoryInfo directory = null;
                    directory = new FileInfo(FullPath).Directory;
                    if (Path.GetPathRoot(directory.FullName).Equals(directory.FullName) && (FullPath.Substring(0, 2) != "\\\\"))
                        return new FileInfo(FullPath).Directory.FullName;
                    else
                        return new FileInfo(FullPath).Directory.FullName + "\\";
                case ResourceAccessType.Web:
                    return FullPath.Substring(0, FullPath.LastIndexOf("/") + 1);
                case ResourceAccessType.DICAnexo:
                case ResourceAccessType.DICConteudo:
                    return FullPath;
            }
            return string.Empty;
		}
    }
}