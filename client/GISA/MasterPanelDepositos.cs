using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

using GISA.Controls;

using ZedGraph;

namespace GISA
{
    public partial class MasterPanelDepositos : GISA.MasterPanel
    {
        private GISADataset.GlobalConfigRow globalConfigRow;

        public MasterPanelDepositos() {
            InitializeComponent();

            //Add any initialization after the InitializeComponent() call
            ToolBar.ButtonClick += Toolbar_ButtonClick;
            aeList.BeforeNewListSelection += aeList_BeforeNewListSelection;
            base.StackChanged += MasterPanelAutoEliminacao_StackChanged;

            GetExtraResources();

            aeList.FilterVisible = btnFiltro.Pushed;

            Display_MetrosLineares();
        }


        private void GetExtraResources() {
            ToolBar.ImageList = SharedResourcesOld.CurrentSharedResources.DEPImageList;
            btnFiltro.ImageIndex = 0;
            btnRefresh.ImageIndex = 1;

            string[] strs = SharedResourcesOld.CurrentSharedResources.DEPStrings;
            btnFiltro.ToolTipText = strs[0];
            btnRefresh.ToolTipText = strs[1];
        }

        /*
         * Refresh do painel de apresentacao das metricas de espaco + pieChart
         */
        private void Display_MetrosLineares() {
            AutoEliminacaoRule.Info_UFs_Larguras ufs = new AutoEliminacaoRule.Info_UFs_Larguras();

            // Metros lineares totais:
            double Metros_lineares_totais = 0.0;
            this.globalConfigRow = (GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Rows[0]);
            if (this.globalConfigRow.IsMetrosLinearesTotaisNull())
                this.txt_metrosLinearesTotais.Text = "";
            else {
                Metros_lineares_totais = (double)this.globalConfigRow.MetrosLinearesTotais;
                this.txt_metrosLinearesTotais.Text = Metros_lineares_totais.ToString();
            }

            // Metros lineares ocupados:
            double Metros_lineares_ocupados = 0.0;
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try {
                // UFS totais:
                ufs = AutoEliminacaoRule.Current.Get_Info_UFs_Larguras(ho.Connection);

                Metros_lineares_ocupados = AutoEliminacaoRule.Current.GetMetrosLinearesOcupados(ho.Connection);
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
            //TODO: throw new InvalidOperationException(this.GetType().FullName + ": NOT IMPLEMENTED.");
            return true;
        }

        public override void Deactivate() {
            //TODO: throw new InvalidOperationException(this.GetType().FullName + ": NOT IMPLEMENTED.");
        }

        public override PersistencyHelper.SaveResult Save(bool activateOpcaoCancelar) {
            return PersistencyHelper.SaveResult.successful;
        }

        private void Toolbar_ButtonClick(object Sender, ToolBarButtonClickEventArgs e) {
            if (e.Button == btnFiltro)
                ClickBtnFiltro();
            else if (e.Button == btnRefresh)
                Refresh();
        }

        private void ClickBtnFiltro() {
            aeList.FilterVisible = btnFiltro.Pushed;
        }

        private void Refresh()
        {
            try
            {
                ((frmMain)TopLevelControl).EnterWaitMode();

                // Armazenar antes do refresh (nao esperar pela mudança de contexto)
                if (aeList.SelectedItems.Count > 0)
                    this.aeList.SelectItem(aeList.SelectedItems[0]);

                Display_MetrosLineares();
            }
            finally
            {
                ((frmMain)TopLevelControl).LeaveWaitMode();
            }
        }

        private void aeList_BeforeNewListSelection(object sender, BeforeNewSelectionEventArgs e) {
            try {
                Debug.WriteLine("aeList_BeforeNewListSelection");
                e.SelectionChange = UpdateContext(e.ItemToBeSelected);
                if (e.SelectionChange) {
                    updateContextStatusBar();
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

            if (item == null) {
                if (aeList.SelectedItems.Count == 1) {
                    // Apesar da contagem de items ser "1" pode acontecer, no caso de 
                    // items que tenham sido entretanto eliminados, que o SelectedItems 
                    // se encontre vazio. Nesse caso consideramos sempre que não existe selecção.
                    try {
                        selectedItem = aeList.SelectedItems[0];
                    }
                    catch (ArgumentException)
                    {
                        selectedItem = null;
                    }
                }
            }
            else if (item.ListView != null)
                selectedItem = item;

            if (selectedItem != null) {
                GISADataset.AutoEliminacaoRow aeRow = null;
                aeRow = (GISADataset.AutoEliminacaoRow)selectedItem.Tag;
                successfulSave = CurrentContext.SetAutoEliminacao(aeRow);
                DelayedRemoveDeletedItems(aeList.Items);
            }
            else
                successfulSave = CurrentContext.SetAutoEliminacao(null);

            // Update do painel de metricas:
            this.Display_MetrosLineares();

            return successfulSave;
        }

        private void MasterPanelAutoEliminacao_StackChanged(frmMain.StackOperation stackOperation, bool isSupport) {
            switch (stackOperation) {
                case frmMain.StackOperation.Push:
                    if (!isSupport) {
                        aeList.ReloadList();
                    }
                    UpdateToolBarButtons();
                    break;
                case frmMain.StackOperation.Pop:
                    break;
            }
        }

        private void updateContextStatusBar()
        {
        }
    }
}
