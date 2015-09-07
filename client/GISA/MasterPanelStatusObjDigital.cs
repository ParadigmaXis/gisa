using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Fedora.FedoraHandler;
using GISA.Model;
using GISA.SharedResources;
using System.Diagnostics;

namespace GISA
{
    public partial class MasterPanelStatusObjDigital : GISA.SinglePanel
    {
        public MasterPanelStatusObjDigital() : base()
        {
            InitializeComponent();

            this.lblFuncao.Text = "Estado dos PDFs";
            base.ParentChanged += MasterPanelStatusObjDigital_ParentChanged;

            GetExtraResources();

            this.ToolBar.ButtonClick += ToolBarStatusObjDigital_ButtonClick;
            this.ConfigDataGrid();
            this.pxDataGridView1.columnClick_refreshData += _ColumnHeaderMouseClick; 
            this.ActivatePanelStatusObjDigitais(true);
        }

        public static Bitmap FunctionImage
        {
            get
            {
                return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "fedora-OD-status-32.png");
            }
        }

        public void GetExtraResources()
        {
            ToolBar.ImageList = SharedResourcesOld.CurrentSharedResources.StatusODImageList;
            ToolBarButtonRefresh.ImageIndex = 0;
            ToolBarButtonClearProcessed.ImageIndex = 1;

            string[] strs = SharedResourcesOld.CurrentSharedResources.StatusODImageListStrings;
            ToolBarButtonRefresh.ToolTipText = strs[0];
            ToolBarButtonClearProcessed.ToolTipText = strs[1];
        }

        public override void LoadData() { }
        public override void ModelToView() { }
        public override bool ViewToModel() { return true; }
        public override void Deactivate() { }
        public override PersistencyHelper.SaveResult Save() { return PersistencyHelper.SaveResult.successful; }

        private void MasterPanelStatusObjDigital_ParentChanged(object sender, System.EventArgs e)
        {
            this.Visible = this.Parent != null;
        }

        private const int PID = 0;
        private const int TITULO = 1;
        private const int QUALIDADE = 2;
        private const int ESTADO = 3;
        private const int DATA = 4;
        private const int IDNIVEL = 5;
        private const int TITULONIVEL = 6;

        private DataTable StatusObjDigitalDataTable;
        private void ConfigDataGrid()
        {
            StatusObjDigitalDataTable = build_StatusDataTable();
            pxDataGridView1.DataSource = StatusObjDigitalDataTable;

            pxDataGridView1.Columns[PID].Width = 100;
            //pxDataGridView1.Columns[PID].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            pxDataGridView1.Columns[TITULO].Width = 300;
            //pxDataGridView1.Columns[TITULO].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            pxDataGridView1.Columns[QUALIDADE].Width = 140;
            //pxDataGridView1.Columns[QUALIDADE].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            pxDataGridView1.Columns[ESTADO].Width = 120;
            //pxDataGridView1.Columns[ESTADO].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            pxDataGridView1.Columns[DATA].Width = 150;
            //pxDataGridView1.Columns[DATA].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            pxDataGridView1.Columns[IDNIVEL].Width = 100;
            //pxDataGridView1.Columns[IDNIVEL].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            pxDataGridView1.Columns[TITULONIVEL].Width = 300;
            //pxDataGridView1.Columns[TITULONIVEL].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            pxDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private string[] _colNames = { "pid", "Título", "Qualidade", "Estado", "Data", "Identificador da UI", "Título da UI" };
        private DataTable build_StatusDataTable()
        {
            DataTable _t = new DataTable();
            for (int i = 0; i < this._colNames.Count<string>(); i++)
                _t.Columns.Add(new DataColumn(this._colNames[i], typeof(string)));
            return _t;
        }
             
        private void ActivatePanelStatusObjDigitais(bool resetOrder)
        {
            pxDataGridView1.DataSource = null;
            if (resetOrder)
                pxDataGridView1.ResetOrder();

            List<ObjectoDigitalStatusRule.ObjectoDigitalStatusInfo> list_status = 
                new List<ObjectoDigitalStatusRule.ObjectoDigitalStatusInfo>();

            GisaDataSetHelper.ManageDatasetConstraints(false);
            long calc = DateTime.Now.Ticks;
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                long t1 = DateTime.Now.Ticks;
                list_status = ObjectoDigitalStatusRule.Current.GetObjectoDigitalStatusInfo(ho.Connection, this.pxDataGridView1.GetListSortDef());
                Debug.WriteLine("<<GetObjectoDigitalStatusInfo>>: " + new TimeSpan(DateTime.Now.Ticks - t1).ToString());
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                throw ex;
            }
            finally
            {
                ho.Dispose();
            }
            GisaDataSetHelper.ManageDatasetConstraints(true);

            long t2 = DateTime.Now.Ticks;
            StatusObjDigitalDataTable.BeginLoadData();
            StatusObjDigitalDataTable.Clear();
            foreach (var status in list_status)
            {
                var row = StatusObjDigitalDataTable.NewRow();
                row[PID] = status.pid;
                row[TITULO] = status.titulo;
                row[QUALIDADE] = FedoraHelper.TranslateQualityEnumEn(status.quality);
                row[ESTADO] = TranslateState(status.state);
                if (!status.date.Equals(DateTime.MinValue))
                    row[DATA] = status.date;
                else row[DATA] = "";
                row[IDNIVEL] = status.idNivel;
                row[TITULONIVEL] = status.designacaoNivel;
                StatusObjDigitalDataTable.Rows.Add(row);
            }
            StatusObjDigitalDataTable.EndLoadData();
            StatusObjDigitalDataTable.AcceptChanges();
            this.pxDataGridView1.DataSource = StatusObjDigitalDataTable;
            
            Debug.WriteLine("<<t2>>: " + new TimeSpan(DateTime.Now.Ticks - t2).ToString());
        }

        private string TranslateState(string code)
        {
            switch (code)
            {
                case "Pending":
                    return "Em fila";
                case "Error":
                    return "Erro";
                case "Processed":
                    return "Criado";
                default:
                    throw new NotSupportedException("Código desconhecido");
            }
        }

        private void _ColumnHeaderMouseClick(object sender, EventArgs e)
        {
            this.ActivatePanelStatusObjDigitais(false);
        }

        private void ClearPanelStatusObjDigitais()
        {
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try { ObjectoDigitalStatusRule.Current.removeOldODsFromQueue(ho.Connection); }
            catch (Exception e) { Trace.WriteLine(e.ToString()); }
            finally { ho.Dispose(); }

            this.ActivatePanelStatusObjDigitais(true);
        }

        private void ToolBarStatusObjDigital_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            if (e.Button == ToolBarButtonRefresh)
                ActivatePanelStatusObjDigitais(false);
            else if (e.Button == ToolBarButtonClearProcessed)
                ClearPanelStatusObjDigitais();
        }

        private void pxDataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var sel = pxDataGridView1.SelectedCells;
            if (sel.Count == 0)
                controloLocalizacao1.ClearTree();
            else
            {
                var nivelID = long.Parse(sel[0].OwningRow.Cells[IDNIVEL].Value.ToString());
                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try
                {
                    controloLocalizacao1.BuildTree(nivelID, ho.Connection, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
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
        }
    }
}
