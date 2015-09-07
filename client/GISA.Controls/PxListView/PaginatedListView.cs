using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

using GISA.Model;
using GISA.SharedResources;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Controls
{
    public abstract partial class PaginatedListView : UserControl
    {
        public delegate void BeforeNewListSelectionEventHandler(object sender, BeforeNewSelectionEventArgs e);
        public event BeforeNewListSelectionEventHandler BeforeNewListSelection;

        //public delegate void DeeperLevelSelectionEventHandler(object sender, DeeperLevelSelectionEventArgs e);
        //public event DeeperLevelSelectionEventHandler DeeperLevelSelection;

        public delegate void ItemDragEventHandler(object sender, ItemDragEventArgs e);
        public event ItemDragEventHandler ItemDrag;

        //public delegate void ItemSubItemClickEventHandler(object sender, ItemSubItemClickEventArgs e);
        //public event ItemSubItemClickEventHandler ItemSubItemClick;

        //public delegate void ContextFormEventHandler(object sender, EventArgs e);
        //public event ContextFormEventHandler ContextFormEvent;

        public event EventHandler KeyUpDelete;

        private int pagesCount;
        private int currentPageNr = 1;
        private int ItemsCountLimit { get { return ((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Rows[0])).MaxNumResultados; } }
        private int TotalElementosCount = 0;
        protected bool showItemsCount = false;
        protected virtual string FilterMessageBody { set; get; }
        protected virtual string FilterMessageTitle { set; get; }
        public string GrpResultadosLabel = "Registos encontrados";
        public bool FilterVisible
        {
            get { return grpFiltro.Visible; }
            set { grpFiltro.Visible = value; }
        }
        public List<ListViewItem> SelectedItems { get { return lstVwPaginated.SelectedItems.Cast<ListViewItem>().ToList(); } }
        public ListView.ListViewItemCollection Items { get { return lstVwPaginated.Items; } }
        public bool MultiSelectListView { 
            get { return this.lstVwPaginated.MultiSelect; } 
            set { this.lstVwPaginated.MultiSelect = value; } }
        public bool CustomizedSorting { get { return this.lstVwPaginated.CustomizedSorting; } set { this.lstVwPaginated.CustomizedSorting = value; } }

        public PaginatedListView()
        {
            InitializeComponent();

            //Add any initialization after the InitializeComponent() call
            lstVwPaginated.MyColumnClick += PxListView_MyColumnClick;
            lstVwPaginated.ItemDrag += lstVwPaginated_ItemDrag;
            lstVwPaginated.BeforeNewSelection += lstVwPaginated_BeforeNewSelection;
            //lstVwPaginated.DeeperLevelSelection += lstVwPaginated_DeeperLevelSelection;
            //lstVwPaginated.ItemSubItemClick += lstVwPaginated_ItemSubItemClick;
            lstVwPaginated.KeyUp += new KeyEventHandler(lstVwUnidadesFisicas_KeyUp);
            btnAplicar.Click += btnAplicar_Click;

            txtNroPagina.GoToPage += TxtNroPagina_BeforeNewSelection;
            btnAnterior.Click += btnAnterior_Click;
            btnProximo.Click += btnProximo_Click;

            GetExtraResources();
        }

        protected virtual int GetPageForItemTag(object itemTag, int pageNr, IDbConnection connection) {
            return PaginatedListRule.Current.GetPageForID(System.Convert.ToInt64(((DataRow)itemTag)["ID"]), this.ItemsCountLimit, connection);
        }

        protected virtual int CountPages(int itemsCountLimit, out int totalElementosCount, IDbConnection connection)
        {
            return PaginatedListRule.Current.CountPages(ItemsCountLimit, out totalElementosCount, connection);
        }
        protected virtual void CalculateOrderedItems(IDbConnection connection) { throw new NotImplementedException("Must override method"); }
        protected virtual void GetItems(int pageNr, int itemsPerPage, IDbConnection connection) { throw new NotImplementedException("Must override method"); }
        protected virtual void AddItemsToList() { throw new NotImplementedException("Must override method"); }
        protected virtual void DeleteTemporaryResults(IDbConnection connection) { throw new NotImplementedException("Must override method"); }
        protected virtual void ClearFilter() { throw new NotImplementedException("Must override method"); }

        protected virtual void GetExtraResources()
        {
            btnAnterior.Image = SharedResourcesOld.CurrentSharedResources.PaginaAnterior;
            btnProximo.Image = SharedResourcesOld.CurrentSharedResources.PaginaSeguinte;
            ToolTip.SetToolTip(btnAnterior, SharedResourcesOld.CurrentSharedResources.PaginaAnteriorString);
            ToolTip.SetToolTip(btnProximo, SharedResourcesOld.CurrentSharedResources.PaginaSeguinteString);
        }

        protected virtual void ApplyFilter() { ClearItemSelection(); ReloadList(); }

        #region EventHandlers
        private void PxListView_MyColumnClick(object sender, MyColumnClickEventArgs e)
        {
            if (this.Items.Count > 0)
                this.ReloadList();
        }

        private void lstVwPaginated_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {
            if (ItemDrag != null)
                ItemDrag(sender, e);
        }

        protected virtual void lstVwPaginated_BeforeNewSelection(object sender, BeforeNewSelectionEventArgs e)
        {
            if (BeforeNewListSelection != null) 
                BeforeNewListSelection(sender, e);
        }

        //private void lstVwPaginated_ItemSubItemClick(object sender, ItemSubItemClickEventArgs e)
        //{
        //    if (ItemSubItemClick != null)
        //        ItemSubItemClick(sender, e);
        //}

        private void lstVwUnidadesFisicas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == Convert.ToInt32(Keys.Delete) && KeyUpDelete != null)
                KeyUpDelete(sender, e);
        }

        private void btnAplicar_Click(object Sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void TxtNroPagina_BeforeNewSelection(object sender, GoToPageEventArgs e)
        {
            if (e.pageNr == 0 || e.pageNr > pagesCount || e.pageNr == currentPageNr)
            {
                e.success = false;
                return;
            }

            long click = 0;
            click = DateTime.Now.Ticks;

            bool successfulClearSelection = true;

            // esta operação tem como objectivo permitir que haja a possibilidade de cancelar a mudança de página
            if (lstVwPaginated.SelectedItems.Count > 0)
                successfulClearSelection = lstVwPaginated.clearItemSelection(lstVwPaginated.SelectedItems[0]);

            if (!successfulClearSelection)
                return;

            currentPageNr = e.pageNr;

            // repopulate the listView
            lstVwPaginated.Items.Clear();
            LoadListData();

            if (lstVwPaginated.Items.Count == 1)
                lstVwPaginated.selectItem(lstVwPaginated.Items[0]);
            else if (lstVwPaginated.Items.Count > 0)
                lstVwPaginated.selectItem(null);

            Debug.WriteLine("<<Go to page>> total " + new TimeSpan(DateTime.Now.Ticks - click).ToString());
        }

        private void btnAnterior_Click(object Sender, EventArgs e)
        {
            long click = 0;
            click = DateTime.Now.Ticks;

            bool successfulClearSelection = true;

            // permitir o cancelamento da mudança de página
            if (lstVwPaginated.SelectedItems.Count > 0)
                successfulClearSelection = lstVwPaginated.clearItemSelection(lstVwPaginated.SelectedItems[0]);

            if (!successfulClearSelection)
                return;

            currentPageNr -= 1;

            // repopulate the listView
            lstVwPaginated.Items.Clear();
            LoadListData();

            if (lstVwPaginated.Items.Count == 1)
                lstVwPaginated.selectItem(lstVwPaginated.Items[0]);
            else if (lstVwPaginated.Items.Count > 0)
                lstVwPaginated.selectItem(null);

            Debug.WriteLine("<<Button Previous>> total " + new TimeSpan(DateTime.Now.Ticks - click).ToString());
        }

        private void btnProximo_Click(object Sender, EventArgs e)
        {
            long click = 0;
            click = DateTime.Now.Ticks;

            bool successfulClearSelection = true;

            if (lstVwPaginated.SelectedItems.Count > 0)
                successfulClearSelection = lstVwPaginated.clearItemSelection(lstVwPaginated.SelectedItems[0]);

            if (!successfulClearSelection)
                return;

            currentPageNr += 1;

            long carregar = 0;
            carregar = DateTime.Now.Ticks;

            // repopulate the listView
            lstVwPaginated.Items.Clear();
            LoadListData();

            Debug.WriteLine("<<Button Next>> total " + new TimeSpan(DateTime.Now.Ticks - click).ToString());
        }

        protected internal void txtBox_KeyDown(object Sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ApplyFilter();
        }
        #endregion

        #region LoadData
        public void LoadListData(DataRow dr)
        {
            LoadListData(dr, true);
        }

        public void LoadListData()
        {
            LoadListData(null, true);
        }

        public void LoadListData(DataRow dr, bool selectFirstItem)
        {
            long loadListDataTime = DateTime.Now.Ticks;
            Cursor oldCursor = null;
            try
            {
                oldCursor = lstVwPaginated.Cursor;
                lstVwPaginated.Parent.TopLevelControl.Cursor = Cursors.WaitCursor;

                try
                {
                    GisaDataSetHelper.ManageDatasetConstraints(false);
                    lstVwPaginated.BeginUpdate();

                    bool deadlockOccurred = true;
                    while (deadlockOccurred)
                    {
                        GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                        try
                        {
                            long calculate = DateTime.Now.Ticks;
                            CalculateOrderedItems(ho.Connection);
                            Debug.WriteLine("<<CalculateOrderedItems>> total " + new TimeSpan(DateTime.Now.Ticks - calculate).ToString());


                            // selectedItemTag virá preenchido se se pretender selecionar 
                            // um item específico após a população do lista
                            if (dr != null)
                            {
                                int pageNr = GetPageForItemTag(dr, this.ItemsCountLimit, ho.Connection);
                                currentPageNr = pageNr < 1 ? 1 : pageNr;
                            }

                            long count = DateTime.Now.Ticks;
                            pagesCount = CountPages(ItemsCountLimit, out this.TotalElementosCount, ho.Connection);
                            Debug.WriteLine("<<countItems>> total " + new TimeSpan(DateTime.Now.Ticks - count).ToString());

                            long obterElementos = 0;
                            obterElementos = DateTime.Now.Ticks;
                            GetItems(currentPageNr, ItemsCountLimit, ho.Connection);

                            Debug.WriteLine("<<obterElementos>> total " + new TimeSpan(DateTime.Now.Ticks - obterElementos).ToString());

                            //limpar
                            long delete = DateTime.Now.Ticks;
                            DeleteTemporaryResults(ho.Connection);
                            Debug.WriteLine("<<DeleteTemporaryResults>> total " + new TimeSpan(DateTime.Now.Ticks - delete).ToString());

                            deadlockOccurred = false;
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine(ex);
                            if (DBAbstractDataLayer.DataAccessRules.ExceptionHelper.isDeadlockException(ex))
                                deadlockOccurred = true;
                            else
                                throw;
                        }
                        finally
                        {
                            ho.Dispose();
                        }
                    }
                    long adicionarElementosLista = DateTime.Now.Ticks;
                    AddItemsToList();
                    Debug.WriteLine("<<adicionarElementosLista>> total " + new TimeSpan(DateTime.Now.Ticks - adicionarElementosLista).ToString());
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                    throw;
                }
                finally
                {
                    lstVwPaginated.EndUpdate();

                    try
                    {
                        GisaDataSetHelper.ManageDatasetConstraints(true);
                    }
                    catch (ConstraintException ex)
                    {
                        IDbConnection conn = GisaDataSetHelper.GetTempConnection();
                        conn.Open();

                        Trace.WriteLine("<EnforceContraints>");
                        Trace.WriteLine(ex.ToString());
                        GisaDataSetHelper.FixDataSet(GisaDataSetHelper.GetInstance(), conn);

                        conn.Close();
#if DEBUG
                        throw;
#endif
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                        throw;
                    }
                }
            }
            finally
            {
                lstVwPaginated.Parent.TopLevelControl.Cursor = oldCursor;
            }

            refreshNavigationState();

            if (lstVwPaginated.Items.Count > 0)
            {
                if (dr != null)
                {
                    // Se, por alguma razão, acontecer o item procurado não 
                    // chegar a ser encontrado também não será selecionado 
                    // nenhum item
                    lstVwPaginated.selectItem(GUIHelper.GUIHelper.findListViewItemByTag(dr, lstVwPaginated));
                }
                else if (selectFirstItem && lstVwPaginated.Items.Count == 1)
                    lstVwPaginated.selectItem(lstVwPaginated.Items[0]);
            }

            Debug.WriteLine("<<LoadListData>> total " + new TimeSpan(DateTime.Now.Ticks - loadListDataTime).ToString());
        }
        #endregion

        public void decrementItemCounter()
        {
            this.TotalElementosCount--;
            refreshNavigationState();
        }

        public void incrementItemCounter()
        {
            this.TotalElementosCount++;
            refreshNavigationState();
        }

        protected ArrayList GetListSortDef()
        {
            ArrayList ordenacao = new ArrayList();
            foreach (ListViewOrderedColumns.ColumnSortOrderInfo column in this.lstVwPaginated.GetOrder().Values)
            {
                ordenacao.Add(column.column.Index);
                ordenacao.Add(column.columnSortOrder == ListViewOrderedColumns.MySortOrder.Ascendente);
            }
            return ordenacao;
        }

        private void refreshNavigationState()
        {
            txtNroPagina.Text = currentPageNr.ToString();
            txtNroPagina.lastPageSelected = txtNroPagina.Text; // isto só deve ser feito quando o controlo está a ser inicializado

            RefreshButtonsState();

            // update label
            if (!this.showItemsCount)
                grpResultados.Text = string.Format(GrpResultadosLabel + " (Página {0} em {1})", currentPageNr, ((pagesCount == 0) ? 1 : pagesCount));
            else
                grpResultados.Text = string.Format(GrpResultadosLabel + ": {2} (Página {0} em {1})", currentPageNr, ((pagesCount == 0) ? 1 : pagesCount), this.TotalElementosCount);
        }

        public void RefreshButtonsState()
        {
            btnAnterior.Enabled = (currentPageNr > 1);
            btnProximo.Enabled = (pagesCount - currentPageNr > 0);
        }

        public void ResetList()
		{
			currentPageNr = 1;
            lstVwPaginated.Items.Clear();
		}

        public void ClearItemSelection()
        {
            ClearItemSelection(null);
        }

        public void ClearItemSelection(ListViewItem selectedItem)
        {
            if (selectedItem == null)
            {
                if (lstVwPaginated.SelectedItems.Count > 0)
                    lstVwPaginated.clearItemSelection(lstVwPaginated.SelectedItems[0]);
            }
            else
                this.lstVwPaginated.clearItemSelection(selectedItem);
        }

        public void ReloadList()
        {
            ReloadList(null);
        }

        public void ReloadList(DataRow selectedItemTag)
        {
            ResetList();
            LoadListData(selectedItemTag);
        }

        public void SelectItem(ListViewItem item)
        {
            this.lstVwPaginated.selectItem(item);
        }

        public void AddItem(DataRow row)
        {
            ListViewItem lvItem = null;
            ReloadList(row);
            lvItem = GUIHelper.GUIHelper.findListViewItemByTag(row, lstVwPaginated);

            // prever a situação em que o filtro está activo e o elemento a adicionar não respeita o critério desse filtro
            if (lvItem == null)
            {
                if (MessageBox.Show(FilterMessageBody, FilterMessageTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ClearFilter();
                    ReloadList(row);
                    lvItem = GUIHelper.GUIHelper.findListViewItemByTag(row, lstVwPaginated);
                    lstVwPaginated.selectItem(lvItem);
                    lstVwPaginated.EnsureVisible(lvItem.Index);
                }
                else
                    ReloadList();
            }
            else
            {
                lstVwPaginated.selectItem(lvItem);
                lstVwPaginated.EnsureVisible(lvItem.Index);
            }
        }
    }
}
