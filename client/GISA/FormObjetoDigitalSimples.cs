using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Controls;
using GISA.Fedora.FedoraHandler;
using GISA.Model;
using GISA.SharedResources;

namespace GISA
{
    public partial class frmObjetoDigitalSimples : Form
    {
        private List<Anexo> mImagens = new List<Anexo>();
        public List<Anexo> imagens { get { return mImagens; } set { mImagens = value; } }

        private string mTitulo = null;
        public string Titulo { get { return mTitulo; } set { mTitulo = value; } }

        private bool mPublicado = false;
        public bool Publicado { get { return mPublicado; } set { mPublicado = value; } }

        // preenchido só no modo de edição e para permitir mostrar as versões do objeto digital
        public ObjDigSimples objSimples { get; set; }

        public frmObjetoDigitalSimples()
        {
            InitializeComponent();
            ficheirosOrderManager1.OrderManagerSelectedIndexChangedEvent += new EventHandler<BeforeNewSelectionEventArgs>(OrderManagerSelectedIndexChangedEvent);
            ficheirosOrderManager1.FullScreenInvoked += new EventHandler<EventArgs>(FicheirosOrderManager1_FullScreenInvoked);
            ficheirosOrderManager1.AttachedFileAdded += new EventHandler<EventArgs>(FicheirosOrderManager1_AttachedFileAdded);
            ficheirosOrderManager1.AttachedFileDeleted += new EventHandler<EventArgs>(FicheirosOrderManager1_AttachedFileDeleted);

            versionControl.Enabled = false;

            ButtonTI.Image = SharedResourcesOld.CurrentSharedResources.ChamarPicker;
            CurrentToolTip.SetToolTip(ButtonTI, "Adicionar título predefinido");

            var configRow = GisaDataSetHelper.GetInstance().GlobalConfig.Cast<GISADataset.GlobalConfigRow>().Single();
            var defaultQuality = configRow.IsQualidadeImagemNull() ? Quality.Low : FedoraHelper.TranslateQualityEnum(configRow.QualidadeImagem);

            this.previewControl.SetupOtherMode();
        }

        public void ActivateReadOnlyMode()
        {
            chkPublicar.Enabled = false;
            txtTitulo.Enabled = false;
            ficheirosOrderManager1.ActivateReadOnlyMode();
        }

        public void Populate()
        {
            this.Cursor = Cursors.WaitCursor;
            txtTitulo.Text = mTitulo;
            chkPublicar.Checked = mPublicado;

            var itemsToBeAdded = new List<ListViewItem>();
            mImagens.ForEach(datastream =>
            {
                var item = ficheirosOrderManager1.CreateItem(datastream.url, datastream);
                itemsToBeAdded.Add(item);
            });

            if (itemsToBeAdded.Count > 0)
            {
                ficheirosOrderManager1.populateItems(itemsToBeAdded);
                //ficheirosOrderManager1.selectFirst();
            }

            if (this.objSimples.state != State.added)
            {
                versionControl.Load(objSimples);
                versionControl.Enabled = true;
            }

            this.Cursor = Cursors.Default;
        }

        private void OrderManagerSelectedIndexChangedEvent(object sender, BeforeNewSelectionEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            
            var item = e.ItemToBeSelected;
            if (item != null && item.Tag != null)
                previewControl.ShowAnexo(item.Tag as Anexo);
            else
            {
                var items = ficheirosOrderManager1.getTrulySelectedItems();
                if (items.Count != 1) { previewControl.Clear(); this.Cursor = Cursors.Default; return; }

                previewControl.ShowAnexo(items[0].Tag as Anexo);
            }
            
            this.Cursor = Cursors.Default;
        }

        private void FicheirosOrderManager1_FullScreenInvoked(object sender, EventArgs e)
        {
            // Verificar o contexto, mostrar ou não full screen mode
            ShowFullScreenMode(ficheirosOrderManager1.Items(), ficheirosOrderManager1.getSelectedItems().Count > 0 ? ficheirosOrderManager1.getSelectedItems().First().Index : -1);
        }

        private void FicheirosOrderManager1_AttachedFileDeleted(object sender, EventArgs e)
        {
            previewControl.Clear();
            grpODTitleAndPub.Enabled = ficheirosOrderManager1.Items().Count > 0;
        }

        private void FicheirosOrderManager1_AttachedFileAdded(object sender, System.EventArgs e)
        {
            grpODTitleAndPub.Enabled = ficheirosOrderManager1.Items().Count > 0;
        }

        public void ShowFullScreenMode(List<ListViewItem> itemsToDisplay, int selectedItemIndex)
        {
            // Clonar a lista de nós a apresentar em modo full screen
            List<ListViewItem> clonedItemList = new List<ListViewItem>();
            clonedItemList.AddRange(itemsToDisplay.Select(item => item.Clone() as ListViewItem));

            // Instanciar uma janela modal para mostrar a lista clonada (passamos o identificador do objecto pai, caso exista) 
            FormFullScreenPdf ecraCompleto = new FormFullScreenPdf(clonedItemList, selectedItemIndex, FedoraHelper.TranslateQualityEnum(previewControl.Qualidade));
            ecraCompleto.ShowDialog();
        }

        private bool cancelCloseForm = false;
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!(ficheirosOrderManager1.Items().Count > 0 && txtTitulo.Text.Length > 0))
            {
                MessageBox.Show("É obrigatório o objeto digital ter o título preenchido " + System.Environment.NewLine + "e pelo menos uma imagem associada.", "Objeto digital", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cancelCloseForm = true;
                return;
            }

            if (mImagens == null)
                mImagens = new List<Anexo>();
            else
                mImagens.Clear();

            mImagens.AddRange(ficheirosOrderManager1.Items().Select(i => i.Tag as Anexo));
            mTitulo = txtTitulo.Text;
            mPublicado = chkPublicar.Checked;
        }

        private void frmObjetoDigital_FormClosed(object sender, FormClosedEventArgs e)
        {
            GUIHelper.GUIHelper.clearField(txtTitulo);
            GUIHelper.GUIHelper.clearField(chkPublicar);
            ficheirosOrderManager1.Deactivate();
            ImageHelper.DeleteFilteredFiles("*.jpg");
            ImageHelper.DeleteFilteredFiles("*.tmp");
        }

        private void frmObjetoDigitalSimples_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = cancelCloseForm;
            cancelCloseForm = false;
        }

        private void ButtonTI_Click(object sender, EventArgs e)
        {
            var frmPickTitulo = new FormPickTítulo();
            frmPickTitulo.LoadData();

            if (frmPickTitulo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txtTitulo.Text = frmPickTitulo.SelectedTitulo;
        }

        private void LoadVersion(int version)
        {
            this.Cursor = Cursors.WaitCursor;

            if (version > 0)
            {
                // Mostrar versão historica (e bloquear alterações)
                ObjDigSimples VersaoObjectoDigital = (ObjDigSimples)SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.LoadID(objSimples.pid, objSimples.historico[version].timestamp);
                ficheirosOrderManager1.Deactivate();
                ObjectToView(VersaoObjectoDigital);
                grpODTitleAndPub.Enabled = false;
                ficheirosOrderManager1.ActivateReadOnlyMode();
            }
            else
            {
                // Mostar versão original (desbloquear alterações)
                ficheirosOrderManager1.Deactivate();
                ObjectToView(objSimples);
                grpODTitleAndPub.Enabled = true;
                ficheirosOrderManager1.DeactivateReadOnlyMode();
            }

            //UpdateVersionLabels(false);
            this.Cursor = Cursors.Default;
        }

        private void ObjectToView(ObjDigSimples objDigital)
        {
            txtTitulo.Text = objDigital.titulo;
            chkPublicar.Checked = objDigital.publicado;

            var itemsToBeAdded = new List<ListViewItem>();
            objDigital.fich_associados.ForEach(f =>
            {
                var datastream = f as Anexo;
                var item = ficheirosOrderManager1.CreateItem(datastream.url, datastream);
                itemsToBeAdded.Add(item);
            });

            if (itemsToBeAdded.Count > 0)
            {
                ficheirosOrderManager1.populateItems(itemsToBeAdded);
                ficheirosOrderManager1.selectFirst();
            }
        }

        private void controlObjectoDigitalVersao1_VersionChanged(object sender, int newVersion)
        {
            LoadVersion(newVersion);
        }

        private void frmObjetoDigitalSimples_Shown(object sender, EventArgs e)
        {
            //if (ficheirosOrderManager1.Items().Count > 0)
            //    ficheirosOrderManager1.selectFirst();
        }
    }
}
