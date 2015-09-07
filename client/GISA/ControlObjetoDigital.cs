using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
    public partial class ControlObjetoDigital : UserControl
    {
        private List<ObjDigSimples> mCurrentODSimples = new List<ObjDigSimples>();
        public List<ObjDigSimples> CurrentODSimples { get { return mCurrentODSimples; } set { mCurrentODSimples = value; } }
        private ObjDigComposto mCurrentODComp = null;
        public ObjDigComposto CurrentODComp { get { return mCurrentODComp; } set { mCurrentODComp = value; } }
        private Anexo mCurrentAnexo = null;
        public Anexo CurrentAnexo { get { return mCurrentAnexo; } set { mCurrentAnexo = value; } }
        public List<SubDocumento> docSimplesSemOD;
        private ObjetoDigitalFedoraHelper.Contexto mViewMode = ObjetoDigitalFedoraHelper.Contexto.nenhum;
        public ObjetoDigitalFedoraHelper.Contexto ViewMode { get { return mViewMode; } set { mViewMode = value; } }
        private string mTitulo = string.Empty;
        public string Titulo { get { return mTitulo; } set { mTitulo = value; } }
        public bool disableSave = false;

        Dictionary<GISADataset.SFRDImagemRow, ObjDigital> newObjects = new Dictionary<GISADataset.SFRDImagemRow, ObjDigital>();

        public ControlObjetoDigital()
        {
            InitializeComponent();

            DocumentoSimplesOrderManager1.OrderManagerSelectedIndexChangedEvent += new EventHandler<BeforeNewSelectionEventArgs>(OrderManagerSelectedIndexChangedEvent);
            DocumentoSimplesOrderManager1.FullScreenInvoked += new EventHandler<EventArgs>(DocumentoSimplesOrderManager1_FullScreenInvoked);
            DocumentoSimplesOrderManager1.NewInvoked += new EventHandler<EventArgs>(DocumentoSimplesOrderManager1_NewInvoked);
            DocumentoSimplesOrderManager1.EditInvoked += new EventHandler<EventArgs>(DocumentoSimplesOrderManager1_EditInvoked);
            DocumentoSimplesOrderManager1.RemoveInvoked += new EventHandler<EventArgs>(DocumentoSimplesOrderManager1_RemoveInvoked);
            FicheirosOrderManager1.OrderManagerSelectedIndexChangedEvent += new EventHandler<BeforeNewSelectionEventArgs>(OrderManagerSelectedIndexChangedEvent);
            FicheirosOrderManager1.FullScreenInvoked += new EventHandler<EventArgs>(FicheirosOrderManager1_FullScreenInvoked);
            FicheirosOrderManager1.AttachedFileAdded += new EventHandler<EventArgs>(FicheirosOrderManager1_AttachedFileAdded);
            FicheirosOrderManager1.AttachedFileDeleted += new EventHandler<EventArgs>(FicheirosOrderManager1_AttachedFileDeleted);

            var configRow = GisaDataSetHelper.GetInstance().GlobalConfig.Cast<GISADataset.GlobalConfigRow>().Single();
            var defaultQuality = configRow.IsQualidadeImagemNull() ? Quality.Low : FedoraHelper.TranslateQualityEnum(configRow.QualidadeImagem);
            this.previewControl.SetupOtherMode();
        }

        private void LoadVersion(int version)
        {
            this.Cursor = Cursors.WaitCursor;
            var objSimples = mCurrentODSimples[0];

            if (version > 0)
            {
                // Mostrar versão historica (e bloquear alterações)
                ObjDigSimples VersaoObjectoDigital = (ObjDigSimples)SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.LoadID(objSimples.pid, objSimples.historico[version].timestamp);
                FicheirosOrderManager1.Deactivate();
                ObjectToView(VersaoObjectoDigital);
                grpODTitleAndPub.Enabled = false;
                FicheirosOrderManager1.ActivateReadOnlyMode();
            }
            else
            {
                // Mostar versão original (desbloquear alterações)
                FicheirosOrderManager1.Deactivate();
                ObjectToView(objSimples);
                grpODTitleAndPub.Enabled = true;
                if (FedoraHelper.HasObjDigWritePermission(objSimples.pid))
                    FicheirosOrderManager1.DeactivateReadOnlyMode();
            }

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
                var item = FicheirosOrderManager1.CreateItem(datastream.url, datastream);
                itemsToBeAdded.Add(item);
            });

            if (itemsToBeAdded.Count > 0)
            {
                FicheirosOrderManager1.populateItems(itemsToBeAdded);
                FicheirosOrderManager1.selectFirst();
            }
        }

        public void ModelToView()
        {
            grpNiveisOrObjFed.Visible = true;

            if (mViewMode == ObjetoDigitalFedoraHelper.Contexto.objetosDigitais)
            {
                grpODTitleAndPub.Enabled = true;
                // configurar check que permite criar ou não um OD composto
                chkKeepODComposto.Visible = true;
                chkPublicar.Enabled = false;

                txtTitulo.Text = mTitulo;

                // configurar lista de ODs
                DocumentoSimplesOrderManager1.Visible = true;
                
                var itemsToBeAdded = new List<ListViewItem>();
                if (mCurrentODComp != null)
                {
                    chkPublicar.Checked = mCurrentODComp.publicado;
                    mCurrentODComp.objSimples.ForEach(objSimples => itemsToBeAdded.Add(AddODSimplesToList(objSimples)));
                }
                else
                    mCurrentODSimples.ForEach(objSimples => itemsToBeAdded.Add(AddODSimplesToList(objSimples)));

                // adicionar os subdocumentos sem objeto digital à lista
                docSimplesSemOD.ForEach(docSimples =>
                {
                    if ((int)docSimples.guiorder - 1 < itemsToBeAdded.Count)
                        itemsToBeAdded.Insert((int)docSimples.guiorder - 1, AddDocSimplesToList(docSimples));
                    else
                        itemsToBeAdded.Add(AddDocSimplesToList(docSimples));
                });


                if (itemsToBeAdded.Count > 0)
                {
                    DocumentoSimplesOrderManager1.populateItems(itemsToBeAdded);
                    DocumentoSimplesOrderManager1.selectFirst();
                }

                if (itemsToBeAdded.Count(lvi => lvi.Tag.GetType() == typeof(ObjDigital) && ((ObjDigital)lvi.Tag).state == State.notFound) > 0)
                {
                    disableSave = true;
                    DocumentoSimplesOrderManager1.DisableToolBar();
                    grpODTitleAndPub.Enabled = false;
                    chkKeepODComposto.Enabled = false;
                    MessageBox.Show("A unidade informacional selecionada tem associado(s) objeto(s) " + System.Environment.NewLine +
                        "digital(ais) o(s) qual(ais) não foi possivel obter do repositório. " + System.Environment.NewLine +
                        "Por esse motivo não será possível efetuar qualquer alteração nesta área. " + System.Environment.NewLine +
                        "Contacte o administrador de sistemas.", "Objeto(s) digital(ais) não encontrado(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (itemsToBeAdded.Count > 0 && itemsToBeAdded[0].Tag.GetType() == typeof(SubDocumento))
                    DocumentoSimplesOrderManager1.SetEditMixedMode();
                else
                    DocumentoSimplesOrderManager1.updateToolBarButtons();

                UpdateGrpODComposto();
            }
            else if (mViewMode == ObjetoDigitalFedoraHelper.Contexto.imagens)
            {
                chkKeepODComposto.Visible = false;
                FicheirosOrderManager1.Visible = true;
                FicheirosOrderManager1.updateToolBarButtons();
                grpNiveisOrObjFed.Visible = true;
                versionControl.Visible = true;
                chkPublicar.Enabled = true;
                txtTitulo.Enabled = true;
                txtTitulo.Text = mTitulo;
                FicheirosOrderManager1.Enabled = true;
                versionControl.Enabled = true;

                if (mCurrentODSimples.Count == 0)
                {
                    grpODTitleAndPub.Enabled = false;
                    versionControl.Reset();
                    versionControl.Enabled = false;
                    return;
                }

                var odSimples = mCurrentODSimples[0];
                txtTitulo.Text = odSimples.titulo;
                chkPublicar.Checked = odSimples.publicado;

                if (!FedoraHelper.HasObjDigReadPermission(odSimples.pid)) { FicheirosOrderManager1.Enabled = false; return; }

                var itemsToBeAdded = new List<ListViewItem>();
                odSimples.fich_associados.ForEach(f =>
                {
                    var datastream = f as Anexo;
                    var item = FicheirosOrderManager1.CreateItem(datastream.url, datastream);
                    itemsToBeAdded.Add(item);
                });

                versionControl.Load(odSimples);

                if (!FedoraHelper.HasObjDigWritePermission(odSimples.pid)) FicheirosOrderManager1.ActivateReadOnlyMode();
            }
            else
                grpNiveisOrObjFed.Visible = false;
        }

        private void UpdateGrpODComposto()
        {
            if (mViewMode != ObjetoDigitalFedoraHelper.Contexto.objetosDigitais) return;

            chkKeepODComposto.Checked = CurrentODComp != null;
            chkKeepODComposto.Enabled = (CurrentODComp != null && DocumentoSimplesOrderManager1.Items().Count >= 2) || (CurrentODComp == null && DocumentoSimplesOrderManager1.Items().Count >= 2);
            txtTitulo.Enabled = chkKeepODComposto.Checked;

            if (CurrentODComp != null && DocumentoSimplesOrderManager1.Items().Count < 2)
                chkKeepODComposto.Checked = false;
        }

        private ListViewItem AddODSimplesToList(ObjDigSimples objSimples)
        {
            var hasReadPermission = true;
            if (objSimples.state != State.added)
                hasReadPermission = FedoraHelper.HasObjDigReadPermission(objSimples.pid);

            var item = DocumentoSimplesOrderManager1.CreateItem(FedoraHelper.GetDesignacaoUI(objSimples.pid), objSimples.titulo, objSimples.state == State.added ? "" : objSimples.pid, objSimples.publicado, objSimples);

            // marcar os objetos simples para os quais não existe permissão de leitura
            if (!hasReadPermission)
                item.ForeColor = Color.Gray;

            if (objSimples.state == State.notFound)
                item.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));

            return item;
        }

        private ListViewItem AddDocSimplesToList(SubDocumento docSimples)
        {
            return DocumentoSimplesOrderManager1.CreateItem(docSimples.designacao, "", "", null, docSimples);
        }

        public void ViewToModel()
        {
            if (mViewMode == ObjetoDigitalFedoraHelper.Contexto.objetosDigitais)
            {
                // neste modo não deveria haver qualquer mudança neste ponto
                if (disableSave) return;

                if (mCurrentODComp == null)
                {
                    var hasChanges = false;
                    UpdateObjectsList(ref mCurrentODSimples, out hasChanges);

                    if (chkKeepODComposto.Checked && mCurrentODSimples.Count > 0)
                    {
                        mCurrentODComp = new ObjDigComposto();
                        mCurrentODComp.state = State.added;
                        mCurrentODComp.pid = "-1";
                        mCurrentODComp.objSimples.AddRange(mCurrentODSimples);
                        mCurrentODComp.titulo = mTitulo;
                        UpdateODSimplesParentTitle(mTitulo);
                        mCurrentODComp.publicado = chkPublicar.Checked;
                    }
                }
                else
                {
                    // actualizar ODs simples sem se considerar o OD Composto
                    var hasChanges = false;
                    UpdateObjectsList(ref mCurrentODComp.objSimples, out hasChanges);
                    if (hasChanges)
                        mCurrentODComp.state = State.modified;

                    mCurrentODComp.publicado = chkPublicar.Checked;
                    
                    if (!chkKeepODComposto.Checked)
                    {
                        // foi decidido não manter o OD Composto
                        mCurrentODComp.state = State.deleted;
                        UpdateODSimplesParentTitle("");
                    }
                    else if (!mCurrentODComp.titulo.Equals(txtTitulo.Text))
                    {
                        mCurrentODComp.titulo = txtTitulo.Text;
                        mCurrentODComp.state = State.modified;
                    }
                }
            }
            else if (mViewMode == ObjetoDigitalFedoraHelper.Contexto.imagens)
            {
                var items = FicheirosOrderManager1.Items();
                if (mCurrentODSimples.Count == 0)
                {
                    if (items.Count == 0)
                        return;
                    else
                    {
                        var odSimples = new ObjDigSimples();
                        odSimples.state = State.added;
                        odSimples.pid = "-1";
                        odSimples.fich_associados.AddRange(items.Select(i => i.Tag as Anexo));
                        odSimples.nextDatastreamId = 1;
                        odSimples.gisa_id = "-1";
                        odSimples.titulo = txtTitulo.Text.Trim().Length == 0 ? mTitulo : txtTitulo.Text.Trim();
                        odSimples.publicado = chkPublicar.Checked;

                        if (mCurrentODComp != null)
                        {
                            mCurrentODComp.state = State.modified;
                            // o objeto digital será adicionado ao composto no ModelToView do SlavePanelFedora
                            //mCurrentODComp.objSimples.Add(odSimples);
                        }

                        mCurrentODSimples = new List<ObjDigSimples>() { odSimples };
                    }
                }
                else
                {
                    var odSimples = mCurrentODSimples[0];

                    if (items.Count == 0)
                    {
                        // objeto simples sem ficheiros
                        odSimples.state = State.deleted;
                        if (mCurrentODComp != null)
                            mCurrentODComp.state = ((ObjDigComposto)mCurrentODComp).objSimples.Count(od => od.state != State.deleted) > 1 ? State.modified : State.deleted;
                    }
                    else
                    {
                        // actualizar a ordem dos ficheiros dentro do objeto digital simples
                        var anexos = items.Select(i => i.Tag as Anexo).ToList();
                        UpdateObjSimples(mCurrentODComp, odSimples, anexos, txtTitulo.Text, chkPublicar.Checked);
                        // actualizar estado publicado do documento composto caso exista
                        
                        if (mCurrentODComp != null)
                            mCurrentODComp.publicado = mCurrentODComp.objSimples.FirstOrDefault(od => od.publicado) != null;
                    }
                }
            }
        }

        // o método tem que indicar se houveram alterações na ordem de objetos digitais, se foram acrescentados objetos digitais ou eliminados e se mudou a ordem de algum subdocumento
        private void UpdateObjectsList(ref List<ObjDigSimples> odsSimples, out bool hasChanges)
        {
            hasChanges = false;
            // identificar os ods simples apagados para depois serem adicionados no final da lista para permitir a re-ingestão do composto e atualizar as datarows respetivas
            var odsDeleted = new List<ObjDigSimples>();
            odsDeleted.AddRange(odsSimples.Where(od => od.state == State.deleted));

            // verificar se houve alteração na ordem somente entre os objetos digitais
            var objsDig = DocumentoSimplesOrderManager1.Items().Where(i => i.Tag.GetType() == typeof(ObjDigSimples)).Select(i => i.Tag as ObjDigSimples).ToList();
            if (objsDig.Count != odsSimples.Count || odsDeleted.Count > 0)
            {
                odsSimples.Clear();
                odsSimples.AddRange(objsDig);
                odsSimples.AddRange(odsDeleted);
                hasChanges = true;
            }
            else
            {
                for (int i = 0; i < objsDig.Count; i++)
                {
                    var objSimples = objsDig[i];
                    if (objSimples.pid != odsSimples[i].pid)
                    {
                        odsSimples[i] = objSimples as ObjDigSimples;
                        hasChanges = true;
                    }
                }
            }
            
            // actualizar a ordem tendo em conta objetos digitais e subdocumentos sem objeto digital
            var odSimples = default(ObjDigSimples);
            var subDoc = default(SubDocumento);
            foreach (var item in DocumentoSimplesOrderManager1.Items())
            {
                var newOrder = item.Index + 1;

                if (item.Tag.GetType() == typeof(ObjDigSimples))
                {
                    odSimples = (ObjDigSimples)item.Tag;
                    odSimples.guiorder = newOrder;
                }
                else if (item.Tag.GetType() == typeof(SubDocumento))
                {
                    subDoc = (SubDocumento)item.Tag;
                    subDoc.guiorder = newOrder;
                }
            }
        }

        private void UpdateODSimplesParentTitle(string titulo)
        {
            mCurrentODComp.objSimples.Where(obj => obj.state != State.deleted).ToList().ForEach(obj =>
            {
                obj.parentDocumentTitle = titulo;
                if (obj.state != State.added)
                    obj.state = State.modified;
            });
        }

        public void Deactivate()
        {
            DocumentoSimplesOrderManager1.Deactivate();
            DocumentoSimplesOrderManager1.Visible = false;
            FicheirosOrderManager1.Deactivate();
            FicheirosOrderManager1.Visible = false;
            versionControl.Visible = false;
            mViewMode = ObjetoDigitalFedoraHelper.Contexto.nenhum;
            mCurrentODComp = null;
            mCurrentODSimples = null;
            previewControl.Clear();
            newObjects.Clear();
            GUIHelper.GUIHelper.clearField(txtTitulo);
            GUIHelper.GUIHelper.clearField(chkPublicar);

            disableSave = false;

            // Garantir que apagamos os JPGs e PDFs (e TMPs) todos que possam ter sido descarregados durante este contexto
            ImageHelper.DeleteFilteredFiles("*.jpg");
            ImageHelper.DeleteFilteredFiles("*.tmp");
            ImageHelper.DeleteFilteredFiles("*.pdf");
        }

        public void ShowFullScreenMode(List<ListViewItem> itemsToDisplay, int selectedItemIndex)
        {
            // Clonar a lista de nós a apresentar em modo full screen
            List<ListViewItem> clonedItemList = new List<ListViewItem>();
            FormFullScreenPdf ecraCompleto = null;

            if (mViewMode == ObjetoDigitalFedoraHelper.Contexto.imagens)
            {
                clonedItemList.AddRange(itemsToDisplay.Select(item => item.Clone() as ListViewItem));

                // Instanciar uma janela modal para mostrar a lista clonada (passamos o identificador do objeto pai, caso exista) 
                ecraCompleto = new FormFullScreenPdf(clonedItemList, selectedItemIndex, FedoraHelper.TranslateQualityEnum(previewControl.Qualidade));
            }
            else
            {
                clonedItemList.AddRange(itemsToDisplay
                    .Where(item => item.Tag.GetType() == typeof(ObjDigSimples) && FedoraHelper.HasObjDigReadPermission(((ObjDigSimples)item.Tag).pid))
                    .Select(item => new ListViewItem(item.SubItems[DocumentoSimplesOrderManager1.colDesignacaoOD.Index].Text) {Tag = item.Tag}));

                int newSelectedItemIndex = -1;
                if (selectedItemIndex >= 0)
                {
                    var od = itemsToDisplay[selectedItemIndex].Tag as ObjDigSimples;
                    newSelectedItemIndex = od == null ? -1 : clonedItemList.FindIndex(item => ((ObjDigSimples)item.Tag).pid.Equals(od.pid));
                }

                // Instanciar uma janela modal para mostrar a lista clonada (passamos o identificador do objeto pai, caso exista) 
                ecraCompleto = new FormFullScreenPdf(clonedItemList, newSelectedItemIndex, FedoraHelper.TranslateQualityEnum(previewControl.Qualidade));
            }
            
            ecraCompleto.ShowDialog();
        }

        private void OrderManagerSelectedIndexChangedEvent(object sender, BeforeNewSelectionEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (sender == DocumentoSimplesOrderManager1)
                {
                    var item = e.ItemToBeSelected;
                    var selectedOD = default(ObjDigital);
                    if (item != null && item.Tag != null)
                        selectedOD = item.Tag as ObjDigital;
                    else
                    {
                        // Limpar browser se nada estiver seleccionado
                        var items = DocumentoSimplesOrderManager1.getTrulySelectedItems();
                        if (items.Count != 1) { previewControl.Clear(true); DocumentoSimplesOrderManager1.updateToolBarButtons(); return; }
                        selectedOD = items[0].Tag as ObjDigital;
                    }

                    if (selectedOD == null || (selectedOD != null && selectedOD.state == State.added)) { previewControl.Clear(); DocumentoSimplesOrderManager1.updateToolBarButtons(); return; }                    

                    if (!FedoraHelper.HasObjDigWritePermission(selectedOD.pid))
                        DocumentoSimplesOrderManager1.ActivateReadOnlyMode();
                    else
                        DocumentoSimplesOrderManager1.DeactivateReadOnlyMode();

                    if (FedoraHelper.HasObjDigReadPermission(selectedOD.pid))
                    {
                        previewControl.Clear(true);
                        previewControl.ShowPDF(selectedOD.pid);
                    }
                    else
                        previewControl.Clear(true);

                    if (disableSave)
                        DocumentoSimplesOrderManager1.DisableToolBar();
                }
                else if (sender == FicheirosOrderManager1)
                {
                    var item = e.ItemToBeSelected;
                    if (item != null && item.Tag != null)
                        previewControl.ShowAnexo(item.Tag as Anexo);
                    else
                    {
                        var items = FicheirosOrderManager1.getTrulySelectedItems();
                        if (items.Count != 1) { previewControl.Clear(true); return; }

                        previewControl.ShowAnexo(items[0].Tag as Anexo);
                    }
                    FicheirosOrderManager1.updateToolBarButtons();
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void DocumentoSimplesOrderManager1_FullScreenInvoked(object sender, EventArgs e)
        {
            // Verificar o contexto, mostrar ou não full screen mode
            ShowFullScreenMode(DocumentoSimplesOrderManager1.Items(), DocumentoSimplesOrderManager1.getSelectedItems().Count > 0 ? DocumentoSimplesOrderManager1.getSelectedItems().First().Index : -1);
        }

        private void FicheirosOrderManager1_FullScreenInvoked(object sender, EventArgs e)
        {
            // Verificar o contexto, mostrar ou não full screen mode
            ShowFullScreenMode(FicheirosOrderManager1.Items(), FicheirosOrderManager1.getSelectedItems().Count > 0 ? FicheirosOrderManager1.getSelectedItems().First().Index : -1);
        }

        private void FicheirosOrderManager1_AttachedFileDeleted(object sender, EventArgs e)
        {
            previewControl.Clear();
            grpODTitleAndPub.Enabled = FicheirosOrderManager1.Items().Count > 0;
        }

        private void FicheirosOrderManager1_AttachedFileAdded(object sender, System.EventArgs e)
        {
            grpODTitleAndPub.Enabled = FicheirosOrderManager1.Items().Count > 0;
        }

        private void DocumentoSimplesOrderManager1_NewInvoked(object sender, EventArgs e)
        {
            var frm = new frmObjetoDigitalSimples();
            var result = frm.ShowDialog();
            if (result == DialogResult.Cancel) return;

            var odSimples = new ObjDigSimples();
            odSimples.pid = "-1";
            odSimples.gisa_id = "";
            odSimples.titulo = frm.Titulo;
            odSimples.publicado = frm.Publicado;
            odSimples.fich_associados = frm.imagens;
            odSimples.state = State.added;

            var item = AddODSimplesToList(odSimples);
            DocumentoSimplesOrderManager1.addNewItem(item);

            UpdateODCompostoStatePublicado(); 
            UpdateGrpODComposto();
        }

        private void DocumentoSimplesOrderManager1_EditInvoked(object sender, EventArgs e)
        {
            if (DocumentoSimplesOrderManager1.getSelectedItems().Count != 1) return;

            var item = DocumentoSimplesOrderManager1.getSelectedItems().Single();
            var odSimples = item.Tag as ObjDigSimples;

            if (odSimples.state != State.added && odSimples.original == null) odSimples.original = odSimples.Clone();

            var frm = new frmObjetoDigitalSimples();
            frm.Titulo = odSimples.titulo;
            frm.Publicado = odSimples.publicado;
            frm.imagens.Clear();
            frm.imagens.AddRange(odSimples.fich_associados);
            frm.objSimples = odSimples;
            var currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try { frm.Populate(); }
            catch (Exception ex) { Trace.WriteLine(ex.ToString()); }
            finally { this.Cursor = currentCursor; }


            var result = frm.ShowDialog();
            if (result == DialogResult.Cancel) return;

            UpdateObjSimples(mCurrentODComp, odSimples, frm.imagens, frm.Titulo, frm.Publicado);
            DocumentoSimplesOrderManager1.RefreshItem(item, odSimples.titulo, odSimples.publicado, odSimples.state == State.added ? "" : odSimples.pid);

            UpdateODCompostoStatePublicado();
            UpdateGrpODComposto();
        }

        private void DocumentoSimplesOrderManager1_RemoveInvoked(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Tem a certeza que pretende apagar o(s) objeto(s) digital(ais) selecionado(s)?", "Apagar objeto(s) digital(ais)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            // TODO: avisar se existem ods associadados a subdocumentos

            var odsSimples = new List<ObjDigSimples>();
            if (mCurrentODComp != null)
                odsSimples = mCurrentODComp.objSimples;
            else
                odsSimples = mCurrentODSimples;

            DocumentoSimplesOrderManager1.getSelectedItems().ForEach(item =>
            {
                var selectedODSimples = item.Tag as ObjDigSimples;
                var odSimples = odsSimples.SingleOrDefault(od => od.pid.Equals(selectedODSimples.pid));
                if (odSimples != null) // se OD está com o estado "added" basta removê-lo da lista; caso contrário é necessário atualizá-lo
                    odSimples.state = State.deleted;

                var nRow = FedoraHelper.GetRelatedNivelDoc(selectedODSimples.pid);
                if (nRow != null)
                {
                    DocumentoSimplesOrderManager1.RefreshItem(item, "", null, "");
                    var docSimples = new SubDocumento();                    
                    docSimples.nRow = nRow;
                    docSimples.id = nRow.ID;
                    docSimples.guiorder = item.Index + 1;
                    docSimples.designacao = nRow.GetNivelDesignadoRows().Single().Designacao;
                    item.Tag = docSimples;
                }
                item.Remove();
            });

            UpdateODCompostoStatePublicado();
            UpdateGrpODComposto();

            if (DocumentoSimplesOrderManager1.getSelectedItems().Count == 1 && DocumentoSimplesOrderManager1.getSelectedItems()[0].Tag.GetType() == typeof(SubDocumento))
                DocumentoSimplesOrderManager1.SetEditMixedMode();
        }

        private void UpdateODCompostoStatePublicado()
        {
            if (mViewMode != ObjetoDigitalFedoraHelper.Contexto.objetosDigitais) return;

            chkPublicar.Checked = chkKeepODComposto.Checked ? DocumentoSimplesOrderManager1.Items().Where(item => item.Tag.GetType() == typeof(ObjDigSimples) && ((ObjDigSimples)item.Tag).state != State.deleted).Select(lvItem => lvItem.Tag as ObjDigSimples).FirstOrDefault(od => od.publicado) != null : false;
        }

        private static void UpdateObjSimples(ObjDigComposto odComposto, ObjDigSimples odSimples, List<Anexo> anexos, string titulo, bool publicado)
        {
            if (anexos.Count != odSimples.fich_associados.Count)
            {
                odSimples.fich_associados.Clear();
                odSimples.fich_associados.AddRange(anexos);
                odSimples.state = odSimples.state != State.added ? State.modified : State.added;

                if (odComposto != null)
                    odComposto.state = State.poked;
            }
            else
            {
                for (int i = 0; i < anexos.Count; i++)
                {
                    if (!anexos[i].url.Equals(odSimples.fich_associados[i].url))
                    {
                        odSimples.fich_associados[i] = anexos[i];
                        odSimples.state = odSimples.state != State.added ? State.modified : State.added;

                        if (odComposto != null && odComposto.state == State.unchanged)
                            odComposto.state = State.poked;
                    }
                }
            }

            if (!odSimples.titulo.Equals(titulo) || odSimples.publicado != publicado)
            {
                odSimples.titulo = titulo.Length > 0 ? titulo : odSimples.titulo;
                odSimples.publicado = publicado;
                odSimples.state = odSimples.state != State.added ? State.modified : State.added;

                if (odComposto != null)
                    odComposto.state = odComposto.state != State.added ? State.modified : State.added;
            }
        }

        private void chkKeepODComposto_CheckedChanged(object sender, EventArgs e)
        {
            txtTitulo.Enabled = mViewMode == ObjetoDigitalFedoraHelper.Contexto.objetosDigitais ? chkKeepODComposto.Checked : true;
            if (chkKeepODComposto.Checked)
                UpdateODCompostoStatePublicado();
            else
                chkPublicar.Checked = false;
        }

        private void versionControl_VersionChanged(object sender, int newVersion)
        {
            LoadVersion(newVersion);
        }
    }
}
