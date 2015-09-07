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
using GISA.SharedResources;
using DBAbstractDataLayer.DataAccessRules;
using System.Collections;

namespace GISA.Controls
{
    public partial class PaginatedDataGridView : UserControl
    {
        private int pagesCount;
        private int currentPageNr = 1;
        private int ItemsCountLimit { get { return ((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Rows[0])).MaxNumResultados; } }
        private int TotalElementosCount = 0;
        protected bool showItemsCount = false;
        public string GrpResultadosLabel = "Registos encontrados";

        protected virtual void GetItems(int pageNr, int itemsPerPage, IDbConnection connection) { throw new NotImplementedException("Must override method"); }
        protected virtual void CalculateOrderedItems(IDbConnection connection) { throw new NotImplementedException("Must override method"); }
        protected virtual void DeleteTemporaryResults(IDbConnection connection) { throw new NotImplementedException("Must override method"); }
        protected virtual void AddItemsToList() { throw new NotImplementedException("Must override method"); }
        protected virtual int CountPages(int itemsCountLimit, out int totalElementosCount, IDbConnection connection)  { throw new NotImplementedException("Must override method"); }

        protected virtual void GetExtraResources()
        {
            btnAnterior.Image = SharedResourcesOld.CurrentSharedResources.PaginaAnterior;
            btnProximo.Image = SharedResourcesOld.CurrentSharedResources.PaginaSeguinte;
        }


        public object DataSource
        {
            get { return this.dataGridVwPaginated.DataSource; }
            set { this.dataGridVwPaginated.DataSource = value; }
        }

        public void AutoResizeColumns()
        {
            this.dataGridVwPaginated.AutoResizeColumns();
        }

        public PaginatedDataGridView()
        {
            InitializeComponent();

            this.dataGridVwPaginated.columnClick_refreshData += _ColumnHeaderMouseClick;

            this.txtNroPagina.GoToPage += TxtNroPagina_BeforeNewSelection;
            // Botoes:
            this.btnAnterior.Click += btnAnterior_Click;
            this.btnProximo.Click += btnProximo_Click;
        }

        public void AddSelectionChangedHandler(EventHandler eh) 
        {
            this.dataGridVwPaginated.SelectionChanged += eh;
        }

        private void _ColumnHeaderMouseClick(object sender, EventArgs e)
        {
            if (this.dataGridVwPaginated.RowCount > 0)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.ReloadList();
                }
                catch (Exception) { throw; }
                finally { this.Cursor = Cursors.WaitCursor; }
            }
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

            if (!successfulClearSelection)
                return;

            currentPageNr = e.pageNr;

            // repopulate the listView
            LoadListData();

            Debug.WriteLine("<<Go to page>> total " + new TimeSpan(DateTime.Now.Ticks - click).ToString());
        }


        private void btnAnterior_Click(object Sender, EventArgs e)
        {
            if (currentPageNr > 1)
            {
                currentPageNr -= 1;
                LoadListData();
            }
        }

        private void btnProximo_Click(object Sender, EventArgs e)
        {
            currentPageNr += 1;
            LoadListData();
        }


        #region LoadData

        public void ReloadList()
        {
            ResetList();
            LoadListData();
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
                oldCursor = dataGridVwPaginated.Cursor;
                dataGridVwPaginated.Parent.TopLevelControl.Cursor = Cursors.WaitCursor;

                try
                {
                    GisaDataSetHelper.ManageDatasetConstraints(false);

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
                            /*
                            if (dr != null)
                            {
                                int pageNr = GetPageForItemTag(dr, this.ItemsCountLimit, ho.Connection);
                                currentPageNr = pageNr < 1 ? 1 : pageNr;
                            }*/

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
                dataGridVwPaginated.Parent.TopLevelControl.Cursor = oldCursor;
            }

            refreshNavigationState();

            Debug.WriteLine("<<LoadListData>> total " + new TimeSpan(DateTime.Now.Ticks - loadListDataTime).ToString());
        }
        #endregion


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

        public bool FilterVisible
        {
            get { return grpFiltro.Visible; }
            set { grpFiltro.Visible = value; }
        }


        
        public void ResetList()
        {
            currentPageNr = 1;
            // limpa explicitamente a selecção de modo a lançar um selectedindexchanged
            if (dataGridVwPaginated.SelectedRows.Count > 0)
                dataGridVwPaginated.ClearSelection();
            //dataGridVwPaginated.Items.Clear();
        }


    }
}
