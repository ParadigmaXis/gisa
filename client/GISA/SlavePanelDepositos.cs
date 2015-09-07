using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using System.Collections.Generic;
using DBAbstractDataLayer.DataAccessRules;

using GISA.Model;
using GISA.SharedResources;

namespace GISA
{
    public partial class SlavePanelDepositos : GISA.GISAControl
    {
        public SlavePanelDepositos() {
            InitializeComponent();
        }

        public static Bitmap FunctionImage {
            get {
                return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "GestaoDepositos_32x32.png");
            }
        }

        private GISADataset.AutoEliminacaoRow CurrentAutoEliminacao;
        private bool isLoaded = false;
        private const string grpUFsAssociadasText = "Unidades físicas associadas ({0}); Largura total: {1:0.000}m";
        decimal larguraTotal = 0;

        public override void LoadData() {
            try {
                ((frmMain)TopLevelControl).EnterWaitMode();
                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try {
                    if (!isLoaded) {
                        if (CurrentContext.AutoEliminacao == null) {
                            CurrentAutoEliminacao = null;
                            return;
                        }

                        CurrentAutoEliminacao = CurrentContext.AutoEliminacao;
                        GisaDataSetHelper.ManageDatasetConstraints(false);
                        // Recarregar o próprio auto de eliminacao para despistar 
                        // o caso em que alguem o possa entretanto já ter eliminado
                        AutoEliminacaoRule.Current.LoadAutoEliminacao(GisaDataSetHelper.GetInstance(), CurrentContext.AutoEliminacao.ID, ho.Connection);
                        if (CurrentAutoEliminacao == null || CurrentAutoEliminacao.RowState == DataRowState.Detached)
                            return;

                        GisaDataSetHelper.ManageDatasetConstraints(true);
                        isLoaded = true;
                    }
                    GisaDataSetHelper.ManageDatasetConstraints(false);
                    long startTicks = 0;
                    startTicks = DateTime.Now.Ticks;
                    Debug.WriteLine("Time dispend loading: " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());
                    GisaDataSetHelper.ManageDatasetConstraints(true);
                }
                catch (System.Data.ConstraintException Ex) {
                    Trace.WriteLine(Ex);
                    GisaDataSetHelper.FixDataSet(GisaDataSetHelper.GetInstance(), ho.Connection);
                }
                finally {
                    ho.Dispose();
                }
            }
            finally {
                ((frmMain)TopLevelControl).LeaveWaitMode();
            }
        }

        public override void ModelToView() {
            try {
                ((frmMain)TopLevelControl).EnterWaitMode();

                // se nao existir um contexto definido os slavepanels não devem ser apresentados
                if (CurrentAutoEliminacao == null || CurrentAutoEliminacao.RowState == DataRowState.Detached) {
                    this.Visible = false;
                    return;
                }

                this.Visible = true;
                
                long startTicks = 0;
                startTicks = DateTime.Now.Ticks;
                Debug.WriteLine("Time dispend model to view: " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());

                try {
                    GisaPrincipalPermission tempWith1 = new GisaPrincipalPermission(SessionHelper.GetGisaPrincipal(), this.GetType().FullName, GisaPrincipalPermission.WRITE);
                    tempWith1.Demand();
                }
                catch (System.Security.SecurityException)
                {
                    //GUIHelper.makeReadOnly(this.GisaPanelScroller);
                }

                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try {
                    // Carregar os detalhes deste auto de eliminacao:
                    List<AutoEliminacaoRule.AutoEliminacao_UFsEliminadas> ufs_auto_eliminacao = AutoEliminacaoRule.Current.LoadUnidadesFisicasAvaliadas(GisaDataSetHelper.GetInstance(), CurrentContext.AutoEliminacao.ID, ho.Connection);
                    List<ListViewItem> items = new List<ListViewItem>();
                    foreach (AutoEliminacaoRule.AutoEliminacao_UFsEliminadas uf in ufs_auto_eliminacao) {
                        ListViewItem item = new ListViewItem("");
                        item.Checked = uf.paraEliminar;
                        item.SubItems.Add(uf.codigo);
                        item.SubItems.Add(uf.designacao);
                        item.SubItems.Add(GUIHelper.GUIHelper.FormatDimensoes(uf.altura, uf.largura, uf.profundidade, uf.tipoMedida));
                        item.Tag = uf.IDNivel;
                        items.Add(item);

                        if (uf.largura.HasValue)
                            larguraTotal += uf.largura.Value;
                    }
                    this.pxListView_UnidadesFisicasDocs.Items.Clear();
                    this.pxListView_UnidadesFisicasDocs.Items.AddRange(items.ToArray());

                    grpUnidadesFisicasAvaliadas.Text = string.Format(grpUFsAssociadasText, this.pxListView_UnidadesFisicasDocs.Items.Count, larguraTotal);

                    // Notas de eliminacao:
                    if (CurrentAutoEliminacao.IsNotasEliminacaoNull())
                        txt_NotasEliminacao.Text = "";
                    else
                        txt_NotasEliminacao.Text = CurrentAutoEliminacao.NotasEliminacao;
                }
                catch (Exception e) {
                    Debug.WriteLine(e);
                    throw;
                }
                finally { ho.Dispose(); }
            }
            finally {
                ((frmMain)TopLevelControl).LeaveWaitMode();
            }
        }

        public override bool ViewToModel() {
            // Elementos que tem a checkBox 'checked':
            long IDNivel;
            foreach (ListViewItem item in this.pxListView_UnidadesFisicasDocs.Items) {
                IDNivel = (long) item.Tag;
                GISADataset.NivelUnidadeFisicaRow [] nuf = (GISADataset.NivelUnidadeFisicaRow [])GisaDataSetHelper.GetInstance().NivelUnidadeFisica.Select("ID = " + IDNivel.ToString());
                if (nuf.Length == 0) {
                    if (item.Checked) {
                        GISADataset.NivelUnidadeFisicaRow row = GisaDataSetHelper.GetInstance().NivelUnidadeFisica.NewNivelUnidadeFisicaRow(); // Linha nova
                        row.Eliminado = item.Checked;
                        row.ID = IDNivel;
                        GisaDataSetHelper.GetInstance().NivelUnidadeFisica.AddNivelUnidadeFisicaRow(row);
                    }
                }
                else if (item.Tag != null)
                    nuf[0].Eliminado = item.Checked;
            }
            // Notas de eliminacao:
            CurrentAutoEliminacao.NotasEliminacao = txt_NotasEliminacao.Text;
            return true;
        }

        public override void Deactivate() {
            isLoaded = false;
            CurrentAutoEliminacao = null;
            larguraTotal = 0;
            GUIHelper.GUIHelper.clearField(this.pxListView_UnidadesFisicasDocs);
            //existsModifiedData = false;
        }

        public override PersistencyHelper.SaveResult Save() {
            var IDsToUpdate = GisaDataSetHelper.GetInstance().NivelUnidadeFisica.Cast<GISADataset.NivelUnidadeFisicaRow>().Where(r => r.RowState == DataRowState.Added || r.RowState == DataRowState.Modified).Select(r => r.ID.ToString()).ToList();
            var res = Save(false);
            if (res == PersistencyHelper.SaveResult.successful) {
                GISA.Search.Updater.updateUnidadeFisica(IDsToUpdate);
            }
            return res;
        }

        public override PersistencyHelper.SaveResult Save(bool activateOpcaoCancelar) {
            var IDsToUpdate = GisaDataSetHelper.GetInstance().NivelUnidadeFisica.Cast<GISADataset.NivelUnidadeFisicaRow>().Where(r => r.RowState == DataRowState.Added || r.RowState == DataRowState.Modified).Select(r => r.ID.ToString()).ToList();
            var res = PersistencyHelper.save(activateOpcaoCancelar);
            if (res == PersistencyHelper.SaveResult.successful)
            {
                GISA.Search.Updater.updateUnidadeFisica(IDsToUpdate);
            }
            return res;
        }

        protected override bool isInnerContextValid()
        {
            return CurrentAutoEliminacao != null && !(CurrentAutoEliminacao.RowState == DataRowState.Detached) && CurrentAutoEliminacao.isDeleted == 0;
        }

        protected override bool isOuterContextValid()
        {
            return CurrentContext.AutoEliminacao != null;
        }

        protected override bool isOuterContextDeleted()
        {
            Debug.Assert(CurrentContext.AutoEliminacao != null, "CurrentContext.AutoEliminacao Is Nothing");
            return CurrentContext.AutoEliminacao.RowState == DataRowState.Detached;
        }

        protected override void addContextChangeHandlers()
        {
            CurrentContext.AutoEliminacaoChanged += this.Recontextualize;
        }

        protected override void removeContextChangeHandlers()
        {
            CurrentContext.AutoEliminacaoChanged -= this.Recontextualize;
        }

        protected override PanelMensagem GetDeletedContextMessage()
        {
            PanelMensagem1.LblMensagem.Text = "Este auto de eliminação foi removido não sendo por isso possível apresentá-lo.";
            return PanelMensagem1;
        }

        protected override PanelMensagem GetNoContextMessage()
        {
            PanelMensagem1.LblMensagem.Text = "Para visualizar os detalhes deverá selecionar um auto de eliminação no painel superior.";
            return PanelMensagem1;
        }

        public override void ActivateMessagePanel(GISAPanel messagePanel)
        {
            this.pnlToolbarPadding.Visible = false;

            PanelMensagem1.Visible = true;
            PanelMensagem1.BringToFront();
        }

        public override void DeactivateMessagePanel(GISAPanel messagePanel)
        {
            if (PanelMensagem1 != null) // este caso acontece se estivermos num local em que simplesmente não seja necessário um messagepanel (e por isso não exista um)
            {
                PanelMensagem1.Visible = false;
                PanelMensagem1.SendToBack();
            }

            this.pnlToolbarPadding.Visible = false;
        }
    }
}
