using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

using GISA.Controls;

using ZedGraph;

namespace GISA
{
    public partial class MasterPanelDepositos2 : GISA.MasterPanel
    {
        public MasterPanelDepositos2() {
            InitializeComponent();

            //Add any initialization after the InitializeComponent() call
            ToolBar.ButtonClick += Toolbar_ButtonClick;
            depList.BeforeNewListSelection += depList_BeforeNewListSelection;
            base.StackChanged += MasterPanelAutoEliminacao_StackChanged;

            GetExtraResources();

            depList.FilterVisible = btnFiltro.Pushed;
        }


        private void GetExtraResources() {
            ToolBar.ImageList = SharedResourcesOld.CurrentSharedResources.DepositosManipulacaoImageList;
            btnCriar.ImageIndex = 0;
            btnEditar.ImageIndex = 1;
            btnApagar.ImageIndex = 2;

            string[] strs = SharedResourcesOld.CurrentSharedResources.DepositosManipulacaoStrings;
            btnCriar.ToolTipText = strs[0];
            btnEditar.ToolTipText = strs[1];
            btnApagar.ToolTipText = strs[2];
        }

        /*
         * Refresh do painel de apresentacao das metricas de espaco + pieChart
         */
        private void Display_MetrosLineares() {

            DepositoRule.Info_UFs_Larguras ufs = new DepositoRule.Info_UFs_Larguras();            
            double Metros_lineares_totais = 0.0;
            
            // Metros lineares ocupados:
            double Metros_lineares_ocupados = 0.0;
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try {
                var iddeposito = CurrentContext.Deposito == null ? long.MinValue : CurrentContext.Deposito.ID;

                // Metros lineares totais:
                Metros_lineares_totais = DepositoRule.Current.GetMetrosLinearesTotais(iddeposito, ho.Connection);
                this.txt_metrosLinearesTotais.Text = Metros_lineares_totais.ToString();

                // UFS totais:                
                ufs = DepositoRule.Current.Get_Info_UFs_Larguras(iddeposito, ho.Connection);

                Metros_lineares_ocupados = DepositoRule.Current.GetMetrosLinearesOcupados(iddeposito, ho.Connection);
                this.txt_metrosLinearesOcupados.Text = Metros_lineares_ocupados.ToString();
            }
            catch (Exception e) {
                Debug.WriteLine(e);
                throw;
            }
            finally { ho.Dispose(); }

            // Metros lineares livres:
            double Metros_lineares_livres = Metros_lineares_totais - Metros_lineares_ocupados;
            this.txt_metrosLinearesLivres.Text = Metros_lineares_livres.ToString();
            this.lblLivres.ForeColor = (Metros_lineares_livres <= 0.0 ? Color.Red : Color.Black);

            // Estimativa de ocupacao para ufs sem largura:
            double Metros_ocupados_estimados = ufs.Media_largura * ufs.TotalUFs_semLargura;
            if (Metros_lineares_livres > 0 && Metros_lineares_ocupados > 0)
                CreateChart(this.zedGraphPieChartControl, Metros_lineares_livres, Metros_lineares_ocupados, Metros_ocupados_estimados);
            else
                CreateEmptyChart(this.zedGraphPieChartControl);

            // UFs totais:
            this.txt_UFsTotais.Text = ufs.TotalUFs.ToString();
            this.txt_UFsSemLargura.Text = ufs.TotalUFs_semLargura.ToString();
        }

        public void CreateChart(ZedGraphControl zgc, double Metros_lineares_livres, double Metros_lineares_ocupados, double Metros_ocupados_estimados) {
            GraphPane myPane = zgc.GraphPane;
            // Set the GraphPane title
            myPane.Title.Text = "";
            myPane.Fill.Color = Color.LightGray;

            myPane.Chart.Fill.Type = FillType.None;

            double total = Metros_lineares_livres + Metros_lineares_ocupados + Metros_ocupados_estimados;
            double percent_Livre = 0;
            double percent_ocupado = 0;
            double percent_ocupado_estimado = 0;

            if (total > 0) {
                percent_Livre = Metros_lineares_livres * 100 / total;
                percent_ocupado = Metros_lineares_ocupados * 100 / total;
                percent_ocupado_estimado = Metros_ocupados_estimados * 100 / total;
            }

            // Adicionar as fatias:
            myPane.CurveList.Clear();
            myPane.AddPieSlice(Metros_lineares_livres, Color.Green, Color.White, 45f, .05,
                percent_Livre > 0 ? string.Format("Livre ({0:.00}%)", percent_Livre) : "Livre");
            myPane.AddPieSlice(Metros_lineares_ocupados, Color.Red, Color.White, 45f, .0,
                percent_ocupado > 0 ? string.Format("Ocupado ({0:.00}%)", percent_ocupado) : "Ocupado");
            if (Metros_ocupados_estimados > 0)
                myPane.AddPieSlice(Metros_ocupados_estimados, Color.IndianRed, Color.White, 45f, .0, string.Format("Ocupado (estimado) ({0:.00}%)", percent_ocupado_estimado));

            zgc.AxisChange();
            zgc.Refresh();
        }

        /**
         * Grafico vazio: usado quando aparecem ocupacoes negativas (erros nos dados)
         */ 
        private void CreateEmptyChart(ZedGraphControl zgc) {
            GraphPane myPane = zgc.GraphPane;
            // Set the GraphPane title
            myPane.Title.Text = "";
            myPane.Fill.Color = Color.LightGray;

            myPane.Chart.Fill.Type = FillType.None;

            // Adicionar as fatias:
            myPane.CurveList.Clear();
            myPane.AddPieSlice(1, Color.LightSlateGray, Color.White, 45f, .0, "Ocupação não calculável");

            zgc.AxisChange();
            zgc.Refresh();
        }

        public override bool ViewToModel() {
            Display_MetrosLineares();
            return true;
        }

        public override void Deactivate() {
            //TODO: throw new InvalidOperationException(this.GetType().FullName + ": NOT IMPLEMENTED.");
        }

        public override PersistencyHelper.SaveResult Save(bool activateOpcaoCancelar) {
            return PersistencyHelper.SaveResult.successful;
        }

        private void Toolbar_ButtonClick(object Sender, ToolBarButtonClickEventArgs e) {
            if (e.Button == btnCriar)
                ClickBtnCriar();
            else if (e.Button == btnEditar)
                ClickBtnEditar();
            else if (e.Button == btnApagar)
                ClickBtnApagar();
            else if (e.Button == btnFiltro)
                ClickBtnFiltro();
        }

        private void ClickBtnCriar()
        {
            var formHandleDeposito = new FormHandleDeposito();
            formHandleDeposito.SetCreateTitle();

            PersistDepositoChanges(formHandleDeposito, false);
        }

        private void ClickBtnEditar()
        {
            var formHandleDeposito = new FormHandleDeposito();
            formHandleDeposito.SetEditTitle();
            formHandleDeposito.Designacao = CurrentContext.Deposito.Designacao;
            formHandleDeposito.Metragem = CurrentContext.Deposito.MetrosLineares.ToString();

            PersistDepositoChanges(formHandleDeposito, true);
        }

        private void ClickBtnApagar()
        {
            if (MessageBox.Show("Tem a certeza que pretende eliminar o depósito?", "Eliminar depósito", MessageBoxButtons.YesNo) == DialogResult.No) return;

            Trace.WriteLine("A apagar depósito...");

            var depItem = depList.SelectedItems[0];
            var depRow = depItem.Tag as GISADataset.DepositoRow;

            if (CurrentContext.Deposito.RowState == DataRowState.Detached)
                depList.ClearItemSelection(depItem);
            else
            {
                var args = new PersistencyHelper.DeleteDepositoPreConcArguments();
                args.IDDeposito = depRow.ID;

                depRow.Delete();

                PersistencyHelper.save(DeleteDeposito, args);
                PersistencyHelper.cleanDeletedData();
            }

            depItem.Remove();
            UpdateToolBarButtons();
            UpdateContext();
        }

        public static void DeleteDeposito(PersistencyHelper.PreConcArguments args)
        {
            var depPca = args as PersistencyHelper.DeleteDepositoPreConcArguments;
            var depRow = GisaDataSetHelper.GetInstance().Deposito.Cast<GISADataset.DepositoRow>()
                .Single(r => r.RowState == DataRowState.Deleted && (long)r["ID", DataRowVersion.Original] == depPca.IDDeposito);

            // Não é permitido eliminar requisições de documentos devolvidos posteriormente nem eliminar devoluções 
            // com requisições posteriores (sem devolução) dos mesmos documentos
            depPca.continueSave = DBAbstractDataLayer.DataAccessRules.DepositoRule.Current.CanDeleteDeposito(depPca.IDDeposito, args.tran);

            if (depPca.continueSave) return;

            System.Data.DataSet tempgisaBackup2 = depPca.gisaBackup;
            PersistencyHelper.BackupRow(ref tempgisaBackup2, depRow);
            depPca.gisaBackup = tempgisaBackup2;
            depRow.RejectChanges();
        }

        private void ClickBtnFiltro() {
            depList.FilterVisible = btnFiltro.Pushed;
        }

        private void PersistDepositoChanges(FormHandleDeposito formHandleDeposito, bool editMode)
        {
            switch (formHandleDeposito.ShowDialog())
            {
                case DialogResult.OK:
                    ((frmMain)TopLevelControl).EnterWaitMode();
                    var depRow = default(GISADataset.DepositoRow);
                    if (!editMode)
                    {
                        depRow = GisaDataSetHelper.GetInstance().Deposito.NewDepositoRow();
                        depRow.Designacao = formHandleDeposito.Designacao;
                        depRow.MetrosLineares = System.Convert.ToDecimal(formHandleDeposito.Metragem);
                        depRow.Versao = new byte[] { };
                        depRow.isDeleted = 0;
                        GisaDataSetHelper.GetInstance().Deposito.AddDepositoRow(depRow);

                        Trace.WriteLine("A criar depósito...");
                    }
                    else
                    {
                        depRow = CurrentContext.Deposito;
                        depRow.Designacao = formHandleDeposito.Designacao;
                        depRow.MetrosLineares = System.Convert.ToDecimal(formHandleDeposito.Metragem);

                        Trace.WriteLine("A editar depósito...");
                    }

                    var saveResult = PersistencyHelper.save();
                    PersistencyHelper.cleanDeletedData();

                    if (saveResult == PersistencyHelper.SaveResult.successful)
                        depList.AddItem(depRow);
                    else
                    {
                        // ToDo: já existe um depósito com essa designação
                    }

                    ((frmMain)TopLevelControl).LeaveWaitMode();
                    break;
                case DialogResult.Cancel:
                    break;
            }
        }

        private void depList_BeforeNewListSelection(object sender, BeforeNewSelectionEventArgs e) {
            try {
                Debug.WriteLine("depList_BeforeNewListSelection");
                e.SelectionChange = UpdateContext(e.ItemToBeSelected);
                if (e.SelectionChange) {
                    updateContextStatusBar();
                    UpdateToolBarButtons(e.ItemToBeSelected);
                }
            }
            catch (Exception ex) {
                Trace.WriteLine(ex);
                throw;
            }
        }

        public override bool UpdateContext() {
            return UpdateContext(null);
        }

        public override bool UpdateContext(ListViewItem item) {
            ListViewItem selectedItem = null;
            bool successfulSave = false;

            if (item == null) 
                selectedItem = depList.SelectedItems.SingleOrDefault();
            else if (item.ListView != null)
                selectedItem = item;

            if (selectedItem != null) {
                var depRow = selectedItem.Tag as GISADataset.DepositoRow;
                successfulSave = CurrentContext.SetDeposito(depRow);
                DelayedRemoveDeletedItems(depList.Items);
            }
            else
                successfulSave = CurrentContext.SetDeposito(null);

            // Update do painel de metricas:
            this.Display_MetrosLineares();

            return successfulSave;
        }

        private void MasterPanelAutoEliminacao_StackChanged(frmMain.StackOperation stackOperation, bool isSupport) {
            switch (stackOperation) {
                case frmMain.StackOperation.Push:
                    if (!isSupport)
                        depList.ReloadList();

                    UpdateToolBarButtons();
                    break;
                case frmMain.StackOperation.Pop:
                    if (!isSupport)
                        depList.ReloadList();

                    break;
            }
        }

        public override void UpdateToolBarButtons()
        {
            UpdateToolBarButtons(null);
        }

        public override void UpdateToolBarButtons(ListViewItem item)
        {
            btnCriar.Enabled = AllowCreate;

            ListViewItem selectedItem = null;

            if (item != null && item.ListView != null)
                selectedItem = item;
            else if (item == null && depList.SelectedItems.Count == 1)
                selectedItem = depList.SelectedItems[0];

            if (selectedItem == null || ((DataRow)selectedItem.Tag).RowState == DataRowState.Detached)
            {
                btnEditar.Enabled = false;
                btnApagar.Enabled = false;
            }
            else
            {
                btnEditar.Enabled = AllowEdit;
                btnApagar.Enabled = AllowDelete;
            }

            // as seguintes linhas deverão eventualmente desaparecer
            if (!(((frmMain)TopLevelControl) == null) && ((frmMain)TopLevelControl).MasterPanelCount > 1)
                disableAddEditAndRemove();
            else if (((frmMain)TopLevelControl) == null)
                disableAddEditAndRemove();
        }

        public void disableAddEditAndRemove()
        {
            btnCriar.Enabled = false;
            btnEditar.Enabled = false;
            btnApagar.Enabled = false;
        }

        private void updateContextStatusBar()
        {
            if (((frmMain)TopLevelControl).isSuportPanel)
                return;

            ((frmMain)TopLevelControl).StatusBarPanelHint.Text = CurrentContext.Deposito == null ? 
                string.Empty : "  Depósito: " + CurrentContext.Deposito.Designacao;
        }
    }
}
