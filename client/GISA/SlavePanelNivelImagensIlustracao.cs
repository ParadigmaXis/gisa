using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Controls;
using GISA.Controls.Localizacao;
using GISA.Model;
using GISA.SharedResources;
using System.IO;

namespace GISA
{
    public partial class SlavePanelNivelImagensIlustracao : GISA.SinglePanel
    {
        GISADataset.NivelRow currentGARow;
        Image currentImage;
        GISADataset.NivelImagemIlustracaoRow currentImgRow;
        string currentFileName;
        bool updateImage = false;

        public SlavePanelNivelImagensIlustracao()
        {
            InitializeComponent();

            btnAdd.Click += btnAdd_Click;
            btnRemove.Click += btnRemove_Click;
            imageViewerControl1.controlerResizeEvent += ControlerResize_action;

            this.lblFuncao.Text = "Indicar imagem";

            GetExtraResources();
        }

        public static Bitmap FunctionImage
        {
            get { return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "ImagensIlustracao_32x32.png"); }
        }

        private void GetExtraResources()
        {
            btnAdd.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
            btnRemove.Image = SharedResourcesOld.CurrentSharedResources.Apagar;

            CurrentToolTip.SetToolTip(btnAdd, SharedResourcesOld.CurrentSharedResources.AdicionarString);
            CurrentToolTip.SetToolTip(btnRemove, SharedResourcesOld.CurrentSharedResources.ApagarString);
        }

        public override void LoadData()
        {
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                NivelRule.Current.LoadImagemIlustracao(CurrentContext.GrupoArquivo.ID, GisaDataSetHelper.GetInstance(), ho.Connection);
                currentGARow = CurrentContext.GrupoArquivo;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
            finally
            {
                ho.Dispose();
            }
        }

        public override void ModelToView()
        {
            currentImgRow = GisaDataSetHelper.GetInstance().NivelImagemIlustracao
                .Cast<GISADataset.NivelImagemIlustracaoRow>().SingleOrDefault(r => r.ID == currentGARow.ID && r.RowState != DataRowState.Deleted);

            if (currentImgRow != null && currentImgRow["Imagem"] != DBNull.Value && currentImgRow.Imagem != null)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream(currentImgRow.Imagem, 0, currentImgRow.Imagem.Length))
                    {
                        ms.Write(currentImgRow.Imagem, 0, currentImgRow.Imagem.Length);
                        currentImage = Image.FromStream(ms, true);
                        imageViewerControl1.UpdatePreviewImage(currentImage);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public override bool ViewToModel()
        {
            Image img = null;
            // nenhuma imagem foi carregada ou alterada
            if (!updateImage) return true;

            if (currentImgRow == null && currentImage == null)
            {
                // do nothing
            }
            else if (currentImgRow != null && currentImage == null)
            {
                currentImgRow.SetImagemNull();
            }
            else if (currentImgRow == null && currentImage != null)
            {
                try
                {
                    byte[] imageData = ReadFile(currentFileName);

                    currentImgRow = GisaDataSetHelper.GetInstance().NivelImagemIlustracao.NewNivelImagemIlustracaoRow();
                    currentImgRow.NivelRow = currentGARow;
                    currentImgRow.Imagem = ImageHelper.ConvertToJpegFormat(imageData, out img);
                    currentImgRow.Modificacao = DateTime.Now;
                    currentImgRow.Versao = new byte[] { };
                    currentImgRow.isDeleted = 0;
                    GisaDataSetHelper.GetInstance().NivelImagemIlustracao.AddNivelImagemIlustracaoRow(currentImgRow);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                    MessageBox.Show("Ocorreu um erro ao atualizar a imagem.", "Atualizar imagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    byte[] imageData = ReadFile(currentFileName);
                    currentImgRow.Imagem = ImageHelper.ConvertToJpegFormat(imageData, out img);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                    MessageBox.Show("Ocorreu um erro ao atualizar a imagem.", "Atualizar imagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return true;
        }

        private void Save()
        {
            Save(false);
        }

        public override PersistencyHelper.SaveResult Save(bool activateOpcaoCancelar)
        {
            PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save(activateOpcaoCancelar);

            return successfulSave;
        }

        public override void Deactivate()
        {
            currentGARow = null;
            currentImage = null;
            currentImgRow = null;
            updateImage = false;
            currentFileName = null;
            imageViewerControl1.UpdatePreviewImage(null);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Image img = null;
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.FileName = "";
            openFileDlg.Title = "Alteração de imagem";
            openFileDlg.RestoreDirectory = true;
            openFileDlg.Multiselect = false;
            openFileDlg.Filter = "Todos (*.*)|*.*";
            if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Image imagem = null;
                try
                {
                    imagem = ImageHelper.GetSmbImageResource(openFileDlg.FileName);
                }
                catch (ImageHelper.UnretrievableResourceException ex)
                {
                    MessageBox.Show("Ocorreu um erro ao abrir a imagem.", "Abrir imagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Trace.WriteLine(ex.ToString());
                }
                
                if (imagem != null)
                {
                    updateImage = true;
                    currentFileName = openFileDlg.FileName;
                    ImageHelper.ConvertToJpegFormat(ReadFile(currentFileName), out img); 
                    currentImage = img;
                    imageViewerControl1.UpdatePreviewImage(currentImage, openFileDlg.FileName);
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            updateImage = true;
            if (currentFileName != null) currentFileName = null;
            else if (currentImage != null) currentImage = null;
            imageViewerControl1.UpdatePreviewImage(null);
        }

        private void UpdateButtonsState()
        {
            btnAdd.Enabled = true;
            btnRemove.Enabled = currentImage != null;
        }

        //Open file in to a filestream and read data in a byte array.
        private byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes 
            //to read from file.
            //In this case we want to read entire file. 
            //So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);

            return data;
        }

        private void ControlerResize_action(object sender)
        {
            if (currentImage != null)
                imageViewerControl1.UpdatePreviewImage(currentImage);
        }

        protected override bool isInnerContextValid()
        {
            return currentGARow != null && !(currentGARow.RowState == DataRowState.Detached) && currentGARow.isDeleted == 0;
        }

        protected override bool isOuterContextValid()
        {
            return CurrentContext.GrupoArquivo != null;
        }

        protected override bool isOuterContextDeleted()
        {
            Debug.Assert(CurrentContext.GrupoArquivo != null, "CurrentContext.GrupoArquivo Is Nothing");
            return CurrentContext.GrupoArquivo.RowState == DataRowState.Detached;
        }

        protected override bool hasReadPermission()
        {
            return PermissoesHelper.AllowRead;
        }

        protected override void addContextChangeHandlers()
        {
            CurrentContext.GrupoArquivoChanged += this.Recontextualize;
        }

        protected override void removeContextChangeHandlers()
        {
            CurrentContext.GrupoArquivoChanged -= this.Recontextualize;
        }

        protected override PanelMensagem GetDeletedContextMessage()
        {
            PanelMensagem1.LblMensagem.Text = "Este grupo de arquivo foi eliminado não sendo, por isso, possível apresentar a sua informação.";
            return PanelMensagem1;
        }

        protected override PanelMensagem GetNoContextMessage()
        {
            PanelMensagem1.LblMensagem.Text = "Para visualizar a imagem de ilustração deverá selecionar um grupo de arquivo no painel superior.";
            return PanelMensagem1;
        }

        protected override PanelMensagem GetNoReadPermissionMessage()
        {
            PanelMensagem1.LblMensagem.Text = "Não tem permissão para visualizar os detalhes do grupo de arquivo selecionado no painel superior.";
            return PanelMensagem1;
        }
    }
}
