using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using GISA.SharedResources;

namespace GISA.Controls
{
    public class PxDataGridView : System.Windows.Forms.DataGridView
    {
        public ImageList SmallImageList { get; set; }
        private List<_Ordering_info> sort_columns;
        private System.Windows.Forms.ContextMenu OrdenacaoContextMenu;
        private System.Windows.Forms.MenuItem LimparOrdenacao;
        private System.Windows.Forms.MenuItem EditarOrdenacao;
        public PxDataGridView()
        {
            this.AllowUserToAddRows = false;
            this.sort_columns = new List<_Ordering_info>();
            this.DataBindingComplete += _DataBindingComplete;
            this.ColumnHeaderMouseClick += dataGridView1_ColumnHeaderMouseClick;

            this.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.MultiSelect = false;
            this.CellBorderStyle = DataGridViewCellBorderStyle.None;

            this.BackgroundColor = System.Drawing.SystemColors.Window;

            this.OrdenacaoContextMenu = new System.Windows.Forms.ContextMenu();
			this.LimparOrdenacao = new System.Windows.Forms.MenuItem();
			this.EditarOrdenacao = new System.Windows.Forms.MenuItem();

            //
            // LimparOrdenacao
            //
            this.LimparOrdenacao.Index = 0;
            this.LimparOrdenacao.Text = "Limpar Ordenação";
            this.LimparOrdenacao.Click += new EventHandler(LimparOrdenacao_Click);
            //
            // OrdenacaoContextMenu
            //
            this.OrdenacaoContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { this.LimparOrdenacao });
        }

        internal class _Ordering_info
        {
            public string columnName;
            public bool asc;
            public int idx_DataGrid;

            public _Ordering_info(string c, bool a, int i) 
            { 
                this.columnName = c; 
                this.asc = a;
                this.idx_DataGrid = i;
            }

            // Compara os indices das colunas:
            public override bool Equals(System.Object obj)
            {
                _Ordering_info _oObj = obj as _Ordering_info;
                bool eq = this.idx_DataGrid == _oObj.idx_DataGrid;
                return eq;
            }
            public override int GetHashCode() { return this.columnName.GetHashCode(); }
        }

        #region Column Header Right Click events
        private void LimparOrdenacao_Click(object sender, System.EventArgs e)
        {
            ResetOrder();
        }
        #endregion


        private void _DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Programmatic sort mode:
            foreach (DataGridViewColumn column in this.Columns)
            {
                column.HeaderCell = new Px_DataGridViewColumnHeaderCell();

                column.SortMode = DataGridViewColumnSortMode.Programmatic;

                int i = this.sort_columns.IndexOf(new _Ordering_info(column.Name, true, column.Index));
                if (i >= 0)
                {
                    ((Px_DataGridViewColumnHeaderCell)column.HeaderCell).number = i + 1;
                    if (this.sort_columns[i].asc)
                        column.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Ascending;
                    else if (!this.sort_columns[i].asc)
                        column.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Descending;
                }
                else
                {
                    column.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
                }
            }
        }

        /**
         * O ArrayList e uma lista de [ int (indice da coluna no DataGrid), bool (ASC==0, DESC==1), ...]
         */
        public ArrayList GetListSortDef()
        {
            ArrayList ordenacao = new ArrayList();
            SortedDictionary<int, _Ordering_info> order = this.GetOrder();
            foreach (int _i_column in order.Keys)
            {
                ordenacao.Add(order[_i_column].idx_DataGrid);
                ordenacao.Add(order[_i_column].asc);
            }
            return ordenacao;

        }

        // Obter os critérios de ordenação definidos
        internal SortedDictionary<int, _Ordering_info> GetOrder()
        {
            SortedDictionary<int, _Ordering_info> orderedColumns = new SortedDictionary<int, _Ordering_info>();
            for (int i = 0; i < this.sort_columns.Count; i++ )
                orderedColumns.Add(i, this.sort_columns[i]);

            return orderedColumns;
        }

        public void ResetOrder()
        {
            this.sort_columns = new List<_Ordering_info>();
            this.updateHeaders();
            // Sort pelas colunas dentro do Array
            if (columnClick_refreshData != null)
                columnClick_refreshData(this, new EventArgs());
        }

        public delegate void PxDataGridColumnClickEventHandler(object sender, EventArgs e);
        public event PxDataGridColumnClickEventHandler columnClick_refreshData;

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                OrdenacaoContextMenu.Show(this, e.Location);
            }
            else
            {
                DataGridViewColumn theColumn = this.Columns[e.ColumnIndex];
                _Ordering_info _ = new _Ordering_info(theColumn.Name, true, theColumn.Index);
                int i = this.sort_columns.IndexOf(_);
                if (i < 0)
                {
                    this.sort_columns.Add(_);
                    // Alterar o header:
                    theColumn.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Ascending;
                }
                else
                {
                    if (this.sort_columns[i].asc)
                    {
                        this.sort_columns[i].asc = false;
                        theColumn.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Descending;
                    }
                    else
                    {
                        this.sort_columns[i].asc = true;
                        theColumn.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Ascending;
                    }
                }
                this.updateHeaders();
                // Sort pelas colunas dentro do Array
                if (columnClick_refreshData != null)
                    columnClick_refreshData(this, e);
            }
        }

        private void updateHeaders()
        {
            for (int i_sortCol = 0; i_sortCol < this.sort_columns.Count; i_sortCol++)
            {
                _Ordering_info _ = this.sort_columns[i_sortCol];
                int _i = dataGridView_indexOf(this.Columns, _.columnName);
                DataGridViewColumn theColumn = this.Columns[_i];
                int _h_idx = i_sortCol + 1;
                //theColumn.HeaderText = _._Orig_Header + " (" + _h_idx + ")" ;
                //((Px_DataGridViewColumnHeaderCell)theColumn.HeaderCell).number = _h_idx;
                theColumn.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
            }
        }

        private int dataGridView_indexOf(DataGridViewColumnCollection cols, string colName)
        {
            int _i = 0;
            bool found = false;

            for (; _i < cols.Count; _i++)
                if (cols[_i].Name == colName) { found = true; break; }

            return found ? _i : -1;
        }


        class Px_DataGridViewColumnHeaderCell : DataGridViewColumnHeaderCell
        {
            public int number { get; set; }

            protected override void Paint(System.Drawing.Graphics graphics,
                        System.Drawing.Rectangle clipBounds,
                        System.Drawing.Rectangle cellBounds,
                        int rowIndex,
                        DataGridViewElementStates dataGridViewElementState,
                        object value,
                        object formattedValue,
                        string errorText,
                        DataGridViewCellStyle cellStyle,
                        DataGridViewAdvancedBorderStyle advancedBorderStyle,
                        DataGridViewPaintParts paintParts)
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                    dataGridViewElementState, value,
                    formattedValue, errorText, cellStyle,
                    advancedBorderStyle, paintParts);
                if (this.number > 0)
                {
                    Size _s = TextRenderer.MeasureText(this.number.ToString(), Control.DefaultFont);
                    Point _p = new Point();
                    _p.X = cellBounds.Location.X + (cellBounds.Width) - (_s.Width * (int)(2));
                    _p.Y = cellBounds.Location.Y + (cellBounds.Height / 2) - (_s.Height / 2);
                    TextRenderer.DrawText(graphics, this.number.ToString(), Control.DefaultFont, new Rectangle(_p, _s), Control.DefaultForeColor);
                }
            }
        }

    }
}
