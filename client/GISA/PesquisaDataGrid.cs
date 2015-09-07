using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GISA.Controls;
using DBAbstractDataLayer.DataAccessRules;
using GISA.Model;
using System.Collections;
using GISA.SharedResources;


namespace GISA
{
    public partial class PesquisaDataGrid 
#if DEBUG        
        : PaginatedDataGridView_MiddleClass
#else
        : PaginatedDataGridView
#endif
    {
        private PaginatedLVGetItems returnedInfo;
        private const int NUM_COLS_XTRA = 2;
        private const int NUM_COLS_BASE = 14;

        private const int NUM_COLS_TOTAL = NUM_COLS_BASE + NUM_COLS_XTRA;
        private const int COL_GUIORDER = 14;
        private const int COL_FRDBASE = 15;

        private struct ColHeaderInfo { public string Name; public int Width; public ColHeaderInfo(string n, int w) { this.Name = n; this.Width = w; } };

        // NOTA: em SqlClientPesquisaRule.CalculateOrderedItems(...) estao indicadas as colunas usadas nos resultados da pesquisa
        private ColHeaderInfo[] dataGridColsInfo =  { 
                // 0
                new ColHeaderInfo("Identificador", 90 ),
                new ColHeaderInfo("Código referência",163), 
                new ColHeaderInfo("Nível de descrição",150),
                new ColHeaderInfo("Título",300),
                // 4
                new ColHeaderInfo("Data de produção início",90),
                new ColHeaderInfo("Data de produção fim",90),
                new ColHeaderInfo("Requisitado",70),
                new ColHeaderInfo("Agrupador",100),
                // 8
                new ColHeaderInfo("Requerentes iniciais",200),
                new ColHeaderInfo("Localização da obra (atual)",200),
                new ColHeaderInfo("Num. polícia (atual)",100),
                new ColHeaderInfo("Localização da obra (antiga)",200),
                new ColHeaderInfo("Num. polícia (antigo)",100),
                new ColHeaderInfo("Tipo de obra",100),
            };

        public IEnumerable<DataGridViewRow> GetSelectedRows
        {
            get
            {
                var selRows = dataGridVwPaginated.SelectedRows;
                if (selRows.Count > 0)
                    return selRows.OfType<DataGridViewRow>();
                else
                {
                    var cells = dataGridVwPaginated.SelectedCells;
                    if (cells.Count > 0)
                    {
                        var rows = cells.OfType<DataGridViewCell>().Select(c => c.OwningRow);
                        return rows.Count() > 1 ? new List<DataGridViewRow>() : rows;
                    }
                    else
                        return new List<DataGridViewRow>();
                }
            }
        }
        
        public object SelectedRow
        {
            // Devolve um GISADataset.FRDBaseRow
            get
            {
                DataGridViewRow _r = this.GetSelectedRows.First();
                return ((GISADataset.FRDBaseRow)_r.Cells[PesquisaDataGrid.COL_FRDBASE].Value);
            }
        }


        public PesquisaDataGrid() : base()
        {
            
            InitializeComponent();
            // Nao aparece filtro:
            this.grpFiltro.Hide();
            // Desenhar os icones:
            this.dataGridVwPaginated.RowPostPaint += dataGridView_RowPostPaint;

            GetExtraResources();

            // Apresentar o cabecalho com o nome das colunas antes de preencher com os resultados:
            init_empty_columnsHeader();
        }


        private void init_empty_columnsHeader()
        {
            this.dataGridVwPaginated.ColumnCount = NUM_COLS_TOTAL;
            this.init_header();
        }

        private void init_header()
        {
            dataGridVwPaginated.ColumnHeadersVisible = true;

            for (int i = 0; i < NUM_COLS_BASE; i++)
            {
                this.dataGridVwPaginated.Columns[i].Name = dataGridColsInfo[i].Name;
                this.dataGridVwPaginated.Columns[i].Width = dataGridColsInfo[i].Width;
                this.dataGridVwPaginated.Columns[i].Visible = true;
            }

            this.dataGridVwPaginated.Columns[COL_GUIORDER].Visible = false;
            this.dataGridVwPaginated.Columns[COL_FRDBASE].Visible = false;
        }

        protected override void GetExtraResources()
        {
            base.GetExtraResources();
            dataGridVwPaginated.SmallImageList = TipoNivelRelacionado.GetImageList();
        }

        public List<string> SearchServerIDs { get; set; }
        public long UserID { get; set; }
        public bool SoDocExpirados { get; set; }
        public bool NewSearch { get; set; }
        public long NrResults { get; set; }
        public Int64? IDNivelEstrutura { get; set; }

        public bool MultiSelectListView { get; set; }

        public DataGridViewRowCollection Items
        {
            get { return this.dataGridVwPaginated.Rows; }
        }

        public bool CustomizedSorting { get; set; }

        
        internal void ClearSearchResults()
        {
            txtNroPagina.Text = "";
            btnAnterior.Enabled = false;
            btnProximo.Enabled = false;
            //this.init_dataGridVwPaginated();
            this.dataGridVwPaginated.DataSource = null;
            this.init_empty_columnsHeader();
            this.grpResultados.Text = "";
        }

        protected System.Data.DataTable init_dataGridVwPaginated()
        {
            // Limpar a dataGrid:
            this.dataGridVwPaginated.DataSource = null;

            System.Data.DataTable table = new System.Data.DataTable();

            for (int i = 0; i < NUM_COLS_BASE; i++)
            {
                DataColumn column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.Caption = dataGridColsInfo[i].Name;
                column.ReadOnly = true;
                table.Columns.Add(column);
            }

            // DataColumn para manter o GUIOrder:
            DataColumn _c = new DataColumn();
            _c.DataType = System.Type.GetType("System.Int32");
            _c.ReadOnly = true;
            table.Columns.Add(_c);

            // DataColumn para o FRDBase:
            _c = new DataColumn();
            _c.DataType = System.Type.GetType("System.Object");
            _c.ReadOnly = true;
            table.Columns.Add(_c);

            return table;
        }

        protected override void GetItems(int pageNr, int itemsPerPage, IDbConnection connection)
        {
            returnedInfo = new PaginatedLVGetItemsPesq(PesquisaRule.Current.GetItems(GisaDataSetHelper.GetInstance(), pageNr, itemsPerPage, connection));
        }

        protected override void CalculateOrderedItems(IDbConnection connection) 
        {
            ArrayList ordenacao = this.dataGridVwPaginated.GetListSortDef();
            long nrResults = 0;
            PesquisaRule.Current.CalculateOrderedItems(ordenacao, SearchServerIDs, IDNivelEstrutura, UserID, SoDocExpirados, NewSearch, out nrResults, connection);
            NrResults = nrResults;
        }

        protected override int CountPages(int itemsCountLimit, out int totalElementosCount, IDbConnection connection)
        {
            totalElementosCount = 0;
            return PesquisaRule.Current.CountPages(itemsCountLimit, connection);
        }

        protected override void DeleteTemporaryResults(IDbConnection connection)
        {
            PesquisaRule.Current.DeleteTemporaryResults(connection);
        }

        protected override void AddItemsToList()
        {
            System.Data.DataTable table = this.init_dataGridVwPaginated();

            if (returnedInfo.rowsInfo != null)
            {
                foreach (PesquisaRule.NivelDocumental pItem in returnedInfo.rowsInfo)
                {
                    GISADataset.TipoNivelRelacionadoRow tnrRow = (GISADataset.TipoNivelRelacionadoRow)(GisaDataSetHelper.GetInstance().TipoNivelRelacionado.Select("ID=" + pItem.IDTipoNivelRelacionado.ToString())[0]);
                    DataRow row = table.NewRow();

                    row[COL_FRDBASE] = GisaDataSetHelper.GetInstance().FRDBase.Select("ID=" + pItem.IDFRDBase.ToString())[0];

                    row[COL_GUIORDER] = SharedResourcesOld.CurrentSharedResources.NivelImageBase(System.Convert.ToInt32(tnrRow.GUIOrder));

                    // "Identificador"
                    row[0] = pItem.IDNivel.ToString();

                    // "Código referência"
                    row[1] = pItem.CodigoCompleto.ToString();

                    // "Nível de descrição"
                    row[2] = tnrRow.Designacao;

                    // "Título"
                    row[3] = pItem.Designacao;

                    // "Data de produção início"
                    row[4] = GISA.Utils.GUIHelper.FormatDate(pItem.InicioAno, pItem.InicioMes, pItem.InicioDia, pItem.InicioAtribuida);

                    // "Data de produção fim"
                    row[5] = GISA.Utils.GUIHelper.FormatDate(pItem.FimAno, pItem.FimMes, pItem.FimDia, pItem.FimAtribuida);

                    
                    // requisições
                    if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsReqEnable())
                    {
                        if (pItem.Requisitado)
                            row[6] = "Sim";
                        else
                            row[6] = "Não";
                    }

                    // agrupador
                    row[7] = pItem.Agrupador;

                    // licença de obra
                    if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsLicObrEnable())
                    {
                        row[8] = pItem.RequerentesIniciais;
                        row[9] = pItem.LocObraDesignacaoAct;
                        row[10] = pItem.LocObraNumPoliciaAct;
                        row[11] = pItem.LocObraDesignacaoAnt;
                        row[12] = pItem.LocObraNumPoliciaAnt;
                        row[13] = pItem.TipoObra;
                    }
                    table.Rows.Add(row);
                }

                System.Windows.Forms.BindingSource dbBindSource = new System.Windows.Forms.BindingSource();
                dbBindSource.DataSource = table;

                // Reset:
                this.dataGridVwPaginated.ColumnCount = 0;
                this.dataGridVwPaginated.DataSource = dbBindSource;
            }

            this.init_header();
        }

        // RowPostPaint event:
        protected virtual void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;

            // Apresentar o numero de linha:
            //var rowIdx = (e.RowIndex + 1).ToString();
            //var centerFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            //var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            //e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);

            // NOTA: icones de 16*16:
            var _hb = new Rectangle(e.RowBounds.Left + grid.RowHeadersWidth - 16-1, e.RowBounds.Top+1, 16, 16);
            DataGridViewRow _r = grid.Rows[e.RowIndex];
            int _ix = SharedResourcesOld.CurrentSharedResources.NivelImageBase(System.Convert.ToInt32(_r.Cells[COL_GUIORDER].Value)-1);
            Image _i = this.dataGridVwPaginated.SmallImageList.Images[_ix];
            e.Graphics.DrawImage(_i, _hb);
        }


    }
}
