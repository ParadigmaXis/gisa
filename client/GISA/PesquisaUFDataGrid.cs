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
    public partial class PesquisaUFDataGrid 

#if DEBUG        
        : PaginatedDataGridView_MiddleClass
#else
        : PaginatedDataGridView
#endif

    {
        private PaginatedLVGetItems returnedInfo;
        private const int NUM_COLS_XTRA = 2;
        private const int NUM_COLS_BASE = 8;    // 0..7

        private const int NUM_COLS_TOTAL = NUM_COLS_BASE + NUM_COLS_XTRA;
        private const int COL_GUIORDER = 8;
        private const int COL_NIVEL = 9;

        private struct ColHeaderInfo { public string Name; public int Width; public ColHeaderInfo(string n, int w) { this.Name = n; this.Width = w; } };

        //
        // NOTA: em SqlClientPesquisaRule.CalculateOrderedItemsUF(...) estao indicadas as colunas usadas nos resultados da pesquisa
        private ColHeaderInfo[] dataGridColsInfo =  { 
                // 0
                new ColHeaderInfo("Código", 200 ),
                new ColHeaderInfo("Título",500),
                // 2
                new ColHeaderInfo("Data de produção início",137),   //90
                new ColHeaderInfo("Data de produção fim",137),
                // 4
                new ColHeaderInfo("Cota",100),
                new ColHeaderInfo("Guia de incorporação",300),
                // 6
                new ColHeaderInfo("Eliminada",80),
                new ColHeaderInfo("Código de barras",100)
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
            get
            {
                DataGridViewRow _r = this.GetSelectedRows.First();
                return ((GISADataset.NivelRow)_r.Cells[PesquisaUFDataGrid.COL_NIVEL].Value);
            }
        }

        public ArrayList getAllIDsNivel()
        {
            ArrayList _ = new ArrayList();

            for (int i = 0; i < this.dataGridVwPaginated.Rows.Count; i++)
            {
                DataGridViewRow _r = this.dataGridVwPaginated.Rows[i];
                GISADataset.NivelRow _nr = (GISADataset.NivelRow)(_r.Cells[PesquisaUFDataGrid.COL_NIVEL].Value);
                _.Add(System.Convert.ToInt32(_nr.ID) );
            }
            return _;
        }


        public PesquisaUFDataGrid()
            : base()
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
            this.dataGridVwPaginated.Columns[COL_NIVEL].Visible = false;
        }

        protected override void GetExtraResources()
        {
            base.GetExtraResources();
            dataGridVwPaginated.SmallImageList = TipoNivelRelacionado.GetImageList();
        }

        public List<string> SearchServerIDs { get; set; }
        //public long UserID { get; set; }
        //public bool SoDocExpirados { get; set; }
        public bool NewSearch { get; set; }
        public long NrResults { get; set; }
        //public Int64? IDNivelEstrutura { get; set; }

        //public bool MultiSelectListView { get; set; }

        /// 
        ///

        public string operador { get; set; }
        public int anoEdicaoInicio { get; set; }
        public int mesEdicaoInicio { get; set; }
        public int diaEdicaoInicio { get; set; }
        public int anoEdicaoFim { get; set; }
        public int mesEdicaoFim { get; set; }
        public int diaEdicaoFim { get; set; }
        public long IDNivel { get; set; }
        public int assoc { get; set; }
        //public bool NewSearch { get; set; }
        //public long NrResults { get; set; }


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

        
        private System.Data.DataTable init_dataGridVwPaginated()
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

            // DataColumn para o NivelRow:
            _c = new DataColumn();
            _c.DataType = System.Type.GetType("System.Object");
            _c.ReadOnly = true;
            table.Columns.Add(_c);

            return table;
        }

        protected override void GetItems(int pageNr, int itemsPerPage, IDbConnection connection)
        {
            returnedInfo = new PaginatedLVGetItemsPesq(PesquisaRule.Current.GetItemsUF(GisaDataSetHelper.GetInstance(), pageNr, itemsPerPage, TipoNivel.OUTRO, connection));
        }


        protected override void CalculateOrderedItems(IDbConnection connection)
        {
            ArrayList ordenacao = this.dataGridVwPaginated.GetListSortDef();
            long nrResults = 0;
            PesquisaRule.Current.CalculateOrderedItemsUF(ordenacao, SearchServerIDs, operador, anoEdicaoInicio, mesEdicaoInicio, diaEdicaoInicio, anoEdicaoFim, mesEdicaoFim, diaEdicaoFim, IDNivel, assoc, NewSearch, out nrResults, connection);
            NrResults = nrResults;
        }

        protected override int CountPages(int itemsCountLimit, out int totalElementosCount, IDbConnection connection)
        {
            totalElementosCount = 0;
            return PesquisaRule.Current.CountPages(itemsCountLimit, connection);
        }

        protected override void DeleteTemporaryResults(IDbConnection connection)
        {
            PesquisaRule.Current.DeleteTemporaryResultsUF(connection);
        }

        protected override void AddItemsToList()
        {
            System.Data.DataTable table = this.init_dataGridVwPaginated();

            if (returnedInfo.rowsInfo != null)
            {
                foreach (ArrayList rowInfo in returnedInfo.rowsInfo)
                {
                    GISADataset.TipoNivelRelacionadoRow tnrRow = (GISADataset.TipoNivelRelacionadoRow)(GisaDataSetHelper.GetInstance().TipoNivelRelacionado.Select("ID=" + TipoNivelRelacionado.UF.ToString())[0]);
                    DataRow row = table.NewRow();

                    //item.Tag = GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + rowInfo[0].ToString())[0];
                    row[COL_NIVEL] = GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + rowInfo[0].ToString())[0];

                    //item.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageBase(System.Convert.ToInt32(tnrRow.GUIOrder));
                    row[COL_GUIORDER] = SharedResourcesOld.CurrentSharedResources.NivelImageBase(System.Convert.ToInt32(tnrRow.GUIOrder));

                    /*
                    item.StateImageIndex = 0;
                    item.SubItems.AddRange(new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });
                    */

                    //item.SubItems[this.chUFCodigo.Index].Text = rowInfo[1].ToString();
                    // "Código"
                    row[0] = rowInfo[1].ToString();

                    //item.SubItems[this.chUFDesignacao.Index].Text = rowInfo[2].ToString();
                    // "Título"
                    row[1] = rowInfo[2].ToString();

                    //item.SubItems[this.chUFDPInicio.Index].Text = GISA.Utils.GUIHelper.FormatDate(rowInfo[3].ToString(), rowInfo[4].ToString(), rowInfo[5].ToString());
                    // "Data de produção início"
                    row[2] = GISA.Utils.GUIHelper.FormatDate(rowInfo[3].ToString(), rowInfo[4].ToString(), rowInfo[5].ToString());

                    //item.SubItems[this.chUFDPFim.Index].Text = GISA.Utils.GUIHelper.FormatDate(rowInfo[6].ToString(), rowInfo[7].ToString(), rowInfo[8].ToString());
                    // "Data de produção fim"
                    row[3] = GISA.Utils.GUIHelper.FormatDate(rowInfo[6].ToString(), rowInfo[7].ToString(), rowInfo[8].ToString());

                    //item.SubItems[this.chUFCota.Index].Text = rowInfo[9].ToString();
                    // "Cota"
                    row[4] = rowInfo[9].ToString();

                    //item.SubItems[this.chUFGuiaIncorporacao.Index].Text = rowInfo[10].ToString();
                    // "Guia Incorporação"
                    row[5] = rowInfo[10].ToString();

                    // Em deposito:
                    if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsDepEnable())
                    {
                        if ((bool)rowInfo[11])  // Eliminado ?
                        {
                            //item.SubItems[this.chUFEliminada.Index].Text = rowInfo[13].ToString();
                            row[6] = rowInfo[13].ToString();
                            //item.Font = new Font(item.Font, FontStyle.Strikeout);
                        }
                        else
                            //item.SubItems[this.chUFEliminada.Index].Text = "Não";
                            row[6] = "Não";
                    }

                    // CodigoBarras:
                    //item.SubItems[this.chUFCodBarras.Index].Text = rowInfo[12].ToString();
                    row[7] = rowInfo[12].ToString();

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
        
        protected void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;

            // NOTA: icones de 16*16:
            var _hb = new Rectangle(e.RowBounds.Left + grid.RowHeadersWidth - 16-1, e.RowBounds.Top+1, 16, 16);
            DataGridViewRow _r = grid.Rows[e.RowIndex];
            int _ix = SharedResourcesOld.CurrentSharedResources.NivelImageBase(System.Convert.ToInt32(_r.Cells[COL_GUIORDER].Value)-1);
            Image _i = this.dataGridVwPaginated.SmallImageList.Images[_ix];
            e.Graphics.DrawImage(_i, _hb);
        }

    }
}
