using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

namespace GISA
{
    public partial class MasterPanelEstatisticas : GISA.SinglePanel
    {
        public MasterPanelEstatisticas() : base()
        {
            InitializeComponent();

            this.lblFuncao.Text = "Estatísticas";
            base.ParentChanged += MasterPanelEstatisticas_ParentChanged;

            if (!SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsFedoraEnable())
                tabControl1.TabPages.Remove(tabFedora);

            GetExtraResources();
            InitializeControls();
        }

        public static Bitmap FunctionImage
        {
            get
            {
                return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "Estatisticas_32x32.png");
            }
        }

        private void GetExtraResources()
        {
            ToolBar.ImageList = SharedResourcesOld.CurrentSharedResources.Estatiscas;
            toolBarCopyToClipboard.ImageIndex = 0;

            string[] strs = SharedResourcesOld.CurrentSharedResources.EstatisticasStrings;
            toolBarCopyToClipboard.ToolTipText = strs[0];
        }

        private void InitializeControls()
        {   
            // Combobox de Unidades Informacionais:
            Build_comboBox(this.comboBox_UnidadesInformacionais, this.cont_Total_UAs);

            // Combobox de Noticia de Autoridade
            Build_comboBox(this.comboBox_Noticia_Autoridade, this.cont_Total_RegAut);

            // No periodo de tempo indicado:
            this.Add_ListView_UFsPeriodo();
            this.Add_ListView_RegistosAutPeriodo();
            this.Add_ListView_UnidadesInformacionaisPeriodo();

            // Totais:
            this.Add_ListView_TotaisUFs();
            this.Add_ListView_TotaisRegistosAut();
            this.Add_ListView_TotaisUnidadesInformacionais();

            InitializeDateControls();
        }

        private void InitializeDateControls()
        {
            cdbDataInicio.Day = DateTime.Now.Day;
            cdbDataInicio.Month = DateTime.Now.Month;
            cdbDataInicio.Year = DateTime.Now.Year;

            cdbDataFim.Day = DateTime.Now.Day;
            cdbDataFim.Month = DateTime.Now.Month;
            cdbDataFim.Year = DateTime.Now.Year;
        }
        
        public override void LoadData() {
            //var excludeImport = chkExclImport.Checked;
            //GetData_Totais(excludeImport, false);
            //GetData_Periodo(DateTime.MinValue, DateTime.MinValue, excludeImport, false);
        }

        public override void ModelToView()
        {
            //base.ModelToView();
        }

        private void PopulateDataPeriodo()
        {
            // No periodo de tempo indicado:
            this.Add_ListView_UFsPeriodo();
            this.Add_ListView_RegistosAutPeriodo();
            this.Add_ListView_UnidadesInformacionaisPeriodo();

            // Totais:
            this.Add_ListView_TotaisUFs();
            this.Add_ListView_TotaisRegistosAut();
            this.Add_ListView_TotaisUnidadesInformacionais();
        }

        public override bool ViewToModel()
        {
            return true;
        }

        public override void Deactivate() 
        {
        }

        public override PersistencyHelper.SaveResult Save()
        {
            return PersistencyHelper.SaveResult.successful;
        }

        private void MasterPanelEstatisticas_ParentChanged(object sender, System.EventArgs e)
        {
            if (this.Parent == null)
            {
                this.Visible = false;
                //ViewToModel();
                //Save();
                //Deactivate();
            }
            else
            {
                this.Visible = true;
                //try
                //{
                //    LoadData();
                //    ModelToView();
                //}
                //catch (Exception ex)
                //{
                //    Trace.WriteLine(ex);
                //    throw;
                //}
            }
        }

        #region "Load data"
        // Totais:
        List<EstatisticasRule.TotalTipo> cont_Total_UAs = new List<EstatisticasRule.TotalTipo>();
        List<EstatisticasRule.TotalTipo> cont_Total_UFs = new List<EstatisticasRule.TotalTipo>();
        List<EstatisticasRule.TotalTipo> cont_Total_RegAut = new List<EstatisticasRule.TotalTipo>();

        // Por periodo:
        List<EstatisticasRule.TotalTipo> contPeriodoUAsPorOper = new List<EstatisticasRule.TotalTipo>();
        List<EstatisticasRule.TotalTipo> contPeriodoUFsPorOper = new List<EstatisticasRule.TotalTipo>();
        List<EstatisticasRule.TotalTipo> contPeriodoRegAutPorOper = new List<EstatisticasRule.TotalTipo>();

        /**
         * Dados referentes a totais independentes do periodo:
         */
        private void GetData_Totais(bool excludeImport, bool sobrePesquisa) {
            // TODO: aplicar o filtro das importações
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try {
                this.cont_Total_UAs = EstatisticasRule.Current.GetTotalNivelPorTipoDesc(sobrePesquisa, ho.Connection);
                this.cont_Total_UFs = Build_Total_UFs(ho.Connection);
                this.cont_Total_RegAut = EstatisticasRule.Current.GetTotalCAPorTipo(ho.Connection);

                // Combobox de Unidades Informacionais:
                Build_comboBox(this.comboBox_UnidadesInformacionais, this.cont_Total_UAs);

                // Combobox de Noticia de Autoridade
                Build_comboBox(this.comboBox_Noticia_Autoridade, this.cont_Total_RegAut);
            }
            catch (Exception ex) {
                Trace.WriteLine(ex);
                throw;
            }
            finally { ho.Dispose(); }
        }

        private const long ID_EMPTY_FILTRO = -10;

        private void Build_comboBox(ComboBox cb, List<EstatisticasRule.TotalTipo> source) {
            EstatisticasRule.TotalTipo empty = new EstatisticasRule.TotalTipo();
            empty.ID = ID_EMPTY_FILTRO;
            empty.Designacao = " <Sem filtro> ";
            empty.Contador = 0;
            cb.Items.Add(empty);
            foreach (EstatisticasRule.TotalTipo ua in source) {   
                if (ua.ID != -1)
                cb.Items.Add(ua);
            }
            cb.SelectedItem = empty;
        }


        /**
         * Contruir uma lista com um contador de Unidades Fisicas
         */
        private List<EstatisticasRule.TotalTipo> Build_Total_UFs(IDbConnection conn) {
            List<EstatisticasRule.TotalTipo> Total_UFs = new List<EstatisticasRule.TotalTipo>();

            // 1. Unidades Fisicas
            EstatisticasRule.TotalTipo tipo_uf1 = new EstatisticasRule.TotalTipo();
            tipo_uf1.ID = 1;
            tipo_uf1.Designacao = "Unidades Fisicas em depósito:";
            tipo_uf1.Contador = EstatisticasRule.Current.GetTotalUF(false, conn);
            Total_UFs.Add(tipo_uf1);            

            EstatisticasRule.TotalTipo tipo_uf6 = new EstatisticasRule.TotalTipo();
            tipo_uf6.ID = 6;
            tipo_uf6.Designacao = "    - sem unidades informacionais";
            tipo_uf6.Contador = EstatisticasRule.Current.GetTotalUFSemUIs(false, conn);
            Total_UFs.Add(tipo_uf6);

            EstatisticasRule.TotalTipo tipo_uf5 = new EstatisticasRule.TotalTipo();
            tipo_uf5.ID = 5;
            tipo_uf5.Designacao = "    - com unidades informacionais";
            tipo_uf5.Contador = tipo_uf1.Contador - tipo_uf6.Contador;
            Total_UFs.Add(tipo_uf5);

            // 2. objetos Digitais:
            EstatisticasRule.TotalTipo tipo_uf2 = new EstatisticasRule.TotalTipo();
            tipo_uf2.ID = 2;
            tipo_uf2.Designacao = "Objetos Digitais Fedora Simples";
            tipo_uf2.Contador = EstatisticasRule.Current.GetTotalObjsDigitaisFedSimples(conn); ;
            Total_UFs.Add(tipo_uf2);

            EstatisticasRule.TotalTipo tipo_uf3 = new EstatisticasRule.TotalTipo();
            tipo_uf3.ID = 3;
            tipo_uf3.Designacao = "Objetos Digitais Fedora Compostos";
            tipo_uf3.Contador = EstatisticasRule.Current.GetTotalObjsDigitaisFedCompostos(conn); ;
            Total_UFs.Add(tipo_uf3);

            EstatisticasRule.TotalTipo tipo_uf4 = new EstatisticasRule.TotalTipo();
            tipo_uf4.ID = 4;
            tipo_uf4.Designacao = "Outros Objetos Digitais";
            tipo_uf4.Contador = EstatisticasRule.Current.GetTotalObjsDigitaisOutros(conn); ;
            Total_UFs.Add(tipo_uf4);

            return Total_UFs;
        }


        private void GetData_Periodo(DateTime dataInicio, DateTime dataFim, bool excludeImport, bool sobrePesquisa) {
            // TODO: aplicar o filtro das importações
            EstatisticasRule.TotalTipo regAut = (EstatisticasRule.TotalTipo)this.comboBox_Noticia_Autoridade.SelectedItem;
            EstatisticasRule.TotalTipo undArq = (EstatisticasRule.TotalTipo)this.comboBox_UnidadesInformacionais.SelectedItem;

            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try {
                this.contPeriodoUFsPorOper = EstatisticasRule.Current.GetTotalCriacoesUFPorOper(dataInicio, dataFim, excludeImport, sobrePesquisa, ho.Connection);
                this.contPeriodoRegAutPorOper = EstatisticasRule.Current.GetTotalCriacoesRegAutPorOper(regAut.ID, dataInicio, dataFim, ho.Connection);
                this.contPeriodoUAsPorOper = EstatisticasRule.Current.GetTotalCriacoesUAPorOper(undArq.ID, dataInicio, dataFim, excludeImport, sobrePesquisa, ho.Connection);
            }
            catch (Exception ex) {
                Trace.WriteLine(ex);
                throw;
            }
            finally { ho.Dispose(); }
        }

        #endregion

        #region "Build stats controls and populate data"

        /**
         * Unidades Fisicas no periodo:
         */
        private void Add_ListView_UFsPeriodo() {
            this.tableLayoutPanel_UnidadesFisicas.Controls.Clear();
            ListView lView = this.BuildListView(new string[] {"Utilizador / Grupo", "Criadas", "Editadas", "Eliminadas" }, contPeriodoUFsPorOper);
            this.tableLayoutPanel_UnidadesFisicas.Controls.Add(new Panel());
            this.tableLayoutPanel_UnidadesFisicas.Controls.Add(lView);
        }

        /**
         * Registos de Autoridade no periodo:
         */
        private void Add_ListView_RegistosAutPeriodo() {
            this.TableLayoutPanel_RegAutPeriodo.Controls.Clear();
            ListView lView = this.BuildListView(new string[] { "Utilizador / Grupo", "Criadas", "Editadas", "Eliminadas" }, contPeriodoRegAutPorOper);
            this.TableLayoutPanel_RegAutPeriodo.Controls.Add(this.comboBox_Noticia_Autoridade);
            this.TableLayoutPanel_RegAutPeriodo.Controls.Add(lView);
        }

        /**
         * Unidades Informacionais no periodo:
         */
        private void Add_ListView_UnidadesInformacionaisPeriodo() {
            this.tableLayoutPanel_UAsPeriodo.Controls.Clear();
            ListView lView = this.BuildListView(new string[] { "Utilizador / Grupo", "Criadas", "Editadas", "Eliminadas" }, contPeriodoUAsPorOper);
            this.tableLayoutPanel_UAsPeriodo.Controls.Add(this.comboBox_UnidadesInformacionais);
            this.tableLayoutPanel_UAsPeriodo.Controls.Add(lView);
        }

        /**
         * Totais por Unidades Fisicas:
         */
        private void Add_ListView_TotaisUFs() {
            this.gBox_Tot_UnidadesFisicas.Controls.Clear();
            ListView lView = this.BuildListView(new string[] { "Tipo", "Valor" }, cont_Total_UFs);
            this.gBox_Tot_UnidadesFisicas.Controls.Add(lView);
        }

        /**
         * Totais por Registos de Autoridade 
         */
        private void Add_ListView_TotaisRegistosAut() {
            this.gBox_Tot_RegistosAutotidade.Controls.Clear();
            ListView lView = this.BuildListView(new string[] { "Tipo", "Valor" }, cont_Total_RegAut);
            this.gBox_Tot_RegistosAutotidade.Controls.Add(lView);
        }

        /**
         * Totais por Unidades Informacionais
         */
        private void Add_ListView_TotaisUnidadesInformacionais() {
            this.gBox_Tot_UnidadesInformacionais.Controls.Clear();
            ListView lView = this.BuildListView(new string[] { "Tipo", "Valor" }, cont_Total_UAs);
            this.gBox_Tot_UnidadesInformacionais.Controls.Add(lView);
        }

        #endregion

        #region "Generic controls"

        private static Font fontBold = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));

        /**
         * ValueColumnTitles : titulo para a primeira, segunda e (opcional) terceira coluna
         */
        private ListView BuildListView(string[] ValueColumnTitles, List<EstatisticasRule.TotalTipo> source) {
            ListView listView = new ListView();
            listView.Dock = DockStyle.Fill;
            listView.View = View.Details;
            
            // Colunas:
            Int32 col_1_size = ValueColumnTitles.Length >= 3 ? 150 : 250;
            Int32 col_2_size = ValueColumnTitles.Length >= 3 ? 75 : 150;
            for (int i = 0; i < ValueColumnTitles.Length; i++) {
                if (i == 0)
                    listView.Columns.Add(ValueColumnTitles[i], col_1_size, HorizontalAlignment.Center);
                else
                    listView.Columns.Add(ValueColumnTitles[i], col_2_size, HorizontalAlignment.Right);
            }

            foreach (EstatisticasRule.TotalTipo tt in source) {
                ListViewItem item = new ListViewItem(tt.Designacao);
                item.SubItems.Add(tt.Contador.ToString());
                if (ValueColumnTitles.Length >= 3)
                {
                    item.SubItems.Add(tt.Contador_Editadas.ToString());
                    item.SubItems.Add(tt.Contador_Eliminadas.ToString());
                }
                if (tt.ID == -1)    // Total:
                    item.Font = fontBold;
                listView.Items.Add(item);
            }

            return listView;
        }

        #endregion

        #region "Destroy stats controls"
        private void DestroyControls()
        {
            //Control c;
            //while (flowLayoutPanel1.Controls.Count > 0)
            //{
            //    c = flowLayoutPanel1.Controls[0];
            //    if (c.Controls.Count > 0)
            //        DestroyControls(c);
            //    else
            //    {
            //        flowLayoutPanel1.Controls.Remove(c);
            //        c.Dispose();
            //    }
            //}
        }

        private void DestroyControls(Control ctrl)
        {
            Control c;
            while (ctrl.Controls.Count > 0)
            {
                c = ctrl.Controls[0];
                if (c.Controls.Count > 0)
                    DestroyControls(c);
                else
                {
                    ctrl.Controls.Remove(c);
                    c.Dispose();
                }
            }
        }
        #endregion        

        #region "Stats report"

        private string GetStatisticsText() {
            if (tabControl1.SelectedTab.Equals(this.tabGisa))
            {
                StringBuilder periodo = new StringBuilder();
                var from = this.Build_dataInicio();
                var to = this.Build_dataFim();
                if (from != DateTime.MinValue || to != DateTime.MinValue)
                    periodo.Append(string.Format("Período:\t{0}\t{1}", from.ToShortDateString(), to.ToShortDateString()));

                return periodo.ToString() + "\n" + GetTotaisPeriodo() + "\n" + GetTotais() + "\n" +
                    "Gerado em: " + DateTime.Now.ToString();
            }
            else
            {
                return controlFedoraEstatisticas1.GetStatisticsText();
            }
        }

        private string GetTotaisPeriodo() {
            StringBuilder str = new StringBuilder();
            str.AppendLine("Unidades Físicas no período:");
            str.AppendLine(List_to_ClipboardString(this.contPeriodoUFsPorOper, true));
            str.AppendLine();

            str.AppendLine("Registos de Autoridade no período:" + FiltroToString(this.comboBox_Noticia_Autoridade));
            str.AppendLine(List_to_ClipboardString(this.contPeriodoRegAutPorOper, true));
            str.AppendLine();

            str.AppendLine("Unidades Informacionais no período:" + FiltroToString(this.comboBox_UnidadesInformacionais));
            str.AppendLine(List_to_ClipboardString(this.contPeriodoUAsPorOper, true));
            str.AppendLine();

            return str.ToString();
        }

        private string GetTotais() {
            StringBuilder str = new StringBuilder();
            str.AppendLine("Total Unidades Informacionais:");
            str.AppendLine(List_to_ClipboardString(this.cont_Total_UAs, false));
            str.AppendLine();

            str.AppendLine("Total unidades físicas e Objetos Digitais:");
            str.AppendLine(List_to_ClipboardString(this.cont_Total_UFs, false));
            str.AppendLine();

            str.AppendLine("Total registos de autoridade:");
            str.AppendLine(List_to_ClipboardString(this.cont_Total_RegAut, false));

            return str.ToString();
        }

        private string FiltroToString(ComboBox thing) {
            EstatisticasRule.TotalTipo tt = (EstatisticasRule.TotalTipo)thing.SelectedItem;
            if (tt.ID > 0) return "\nFiltro:\t" + tt.Designacao;
            return "";
        }

        private string List_to_ClipboardString(List<EstatisticasRule.TotalTipo> source, bool tem_3_colunas) {
            StringBuilder str = new StringBuilder();
            foreach (EstatisticasRule.TotalTipo tt in source)
                str.AppendLine(tt.Designacao + "\t" + tt.Contador.ToString() + (tem_3_colunas ? "\t" + tt.Contador_Editadas.ToString() : ""));
            return str.ToString();
        }

        #endregion

        private void ToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == toolBarCopyToClipboard)
            {
                Clipboard.SetText(GetStatisticsText());
            }
        }

        
        /**
         * Datas do filtro de datas:
         */
        private DateTime Build_dataInicio() {
            if (this.cdbDataInicio.Checked)
                return new DateTime(this.cdbDataInicio.Year, this.cdbDataInicio.Month, this.cdbDataInicio.Day);
            return DateTime.MinValue;
        }
        private DateTime Build_dataFim() {
            if (this.cdbDataFim.Checked)
                // adicionadas horas, minutos e segundos para que o filtro não exclua os acontecimentos ocorridos em data_fim
                return new DateTime(this.cdbDataFim.Year, this.cdbDataFim.Month, this.cdbDataFim.Day).AddHours(23).AddMinutes(59).AddSeconds(59);
            return DateTime.MinValue;
        }


        /**
         * Aplicar o filtro por datas:
         */
        private void button1_Click_1(object sender, EventArgs e) {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                var excludeImport = chkExclImport.Checked;
                GetData_Totais(excludeImport, false);
                GetData_Periodo(Build_dataInicio(), Build_dataFim(), excludeImport, false);
                PopulateDataPeriodo();
            }
            catch (Exception) { throw; }
            finally { this.Cursor = Cursors.Default; }
        }


        private long filtro_comboBox_Noticia_Autoridade = ID_EMPTY_FILTRO;
        private long filtro_comboBox_UnidadesInformacionais = ID_EMPTY_FILTRO;

        /**
         * Aplicar o filtro de Registo de Autoridade (datas opcionais):
         */
        private void comboBox_Noticia_Autoridade_SelectedIndexChanged(object sender, EventArgs e) {
            EstatisticasRule.TotalTipo regAut = (EstatisticasRule.TotalTipo)this.comboBox_Noticia_Autoridade.SelectedItem;
            if (this.filtro_comboBox_Noticia_Autoridade != regAut.ID) {
                this.filtro_comboBox_Noticia_Autoridade = regAut.ID;
                var from = Build_dataInicio();
                var to = Build_dataFim();
                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try {
                    this.contPeriodoRegAutPorOper = EstatisticasRule.Current.GetTotalCriacoesRegAutPorOper(regAut.ID, from, to, ho.Connection);
                    this.Add_ListView_RegistosAutPeriodo();
                }
                catch (Exception ex) {
                    Trace.WriteLine(ex);
                    throw;
                }
                finally { ho.Dispose(); }
            }
        }

        /**
         * Aplicar o filtro de Unidades Informacionais (datas opcionais):
         */
        private void comboBox_UnidadesInformacionais_SelectedIndexChanged(object sender, EventArgs e) {
            EstatisticasRule.TotalTipo undArq = (EstatisticasRule.TotalTipo)this.comboBox_UnidadesInformacionais.SelectedItem;
            if (this.filtro_comboBox_UnidadesInformacionais != undArq.ID) {
                this.filtro_comboBox_UnidadesInformacionais = undArq.ID;
                var from = Build_dataInicio();
                var to = Build_dataFim();
                var excludeImport = chkExclImport.Checked;
                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try {
                    this.contPeriodoUAsPorOper = EstatisticasRule.Current.GetTotalCriacoesUAPorOper(undArq.ID, from, to, excludeImport, false, ho.Connection);
                    this.Add_ListView_UnidadesInformacionaisPeriodo();
                }
                catch (Exception ex) {
                    Trace.WriteLine(ex);
                    throw;
                }
                finally { ho.Dispose(); }
            }
        }
    }
}
