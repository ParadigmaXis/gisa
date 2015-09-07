using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using GISA.GUIHelper;
using GISA.Controls;

namespace GISA.PaginatedListviewController
{
	public class PaginatedListviewController : object
	{

		public delegate void CalculateOrderedItemsEventHandler(IDbConnection connection);
		public event CalculateOrderedItemsEventHandler CalculateOrderedItems;

		public delegate void GetPageForItemTagEventHandler(object itemTag, ref int pageNr, IDbConnection connection);
		public event GetPageForItemTagEventHandler GetPageForItemTag;

        public delegate void GetPagesCountEventHandler(object sender, int currentPage, ref int numberOfPages, ref int numberOfItems, IDbConnection connection);
		public event GetPagesCountEventHandler GetPagesCount;

		public delegate void GetItemsEventHandler(int pageNr, int itemsPerPage, ref PaginatedLVGetItems returnedInfo, IDbConnection connection);
		public event GetItemsEventHandler GetItems;

		public delegate void DeleteTemporaryResultsEventHandler(IDbConnection connection);
		public event DeleteTemporaryResultsEventHandler DeleteTemporaryResults;

		public delegate void AddItemsToListEventHandler(PaginatedLVGetItems returnedInfo);
		public event AddItemsToListEventHandler AddItemsToList;

		public delegate void BeforeNewListSelectionEventHandler(object sender, BeforeNewSelectionEventArgs e);
		public event BeforeNewListSelectionEventHandler BeforeNewListSelection;

		public delegate void DeeperLevelSelectionEventHandler(object sender, DeeperLevelSelectionEventArgs e);
		public event DeeperLevelSelectionEventHandler DeeperLevelSelection;

        //public delegate void GoToPageEventHandler(object sender, GoToPageEventArgs e);
        //public event GoToPageEventHandler GoToPage;

		private int CurrentPageNr;
        private bool contadorElementos = false;
        private int TotalElementosCount = 0;

        public PaginatedListviewController(PxListView listview, Button btnAnterior, Button btnProximo, PxPageIntegerBox txtNroPagina, GroupBox grpBox)
            : this(listview, btnAnterior, btnProximo, txtNroPagina, grpBox, false)
        { }

        public PaginatedListviewController(PxListView listview, Button btnAnterior, Button btnProximo, PxPageIntegerBox txtNroPagina, GroupBox grpBox, bool contadorElementos)
		{
            this.contadorElementos = contadorElementos;
			mListview = listview;
			this.btnAnterior = btnAnterior;
			this.btnProximo = btnProximo;
			this.grpBox = grpBox;
			this.mOriginalLabel = string.Empty + grpBox.Text;
			this.txtNroPagina = txtNroPagina;
			CurrentPageNr = 1;

            mListview.BeforeNewSelection += Listview_BeforeNewSelection;
            mListview.DeeperLevelSelection += Listview_DeeperLevelSelection;
            txtNroPagina.GoToPage += TxtNroPagina_BeforeNewSelection;
            btnAnterior.Click += btnAnterior_Click;
            btnProximo.Click += btnProximo_Click;
		}

		private PxListView mListview;
		private Button btnAnterior;
		private Button btnProximo;
        private PxPageIntegerBox txtNroPagina;
		private GroupBox grpBox;

		private string mOriginalLabel;
		public string originalLabel
		{
			get
			{
				return mOriginalLabel;
			}
			set
			{
				mOriginalLabel = value;
			}
		}

		private PxListView Listview
		{
			get
			{
				return mListview;
			}
		}

		private int ItemsCountLimit
		{
			get
			{
				return ((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Rows[0])).MaxNumResultados;
			}
		}

		private int TotalPaginasCount;
		//Private ReadOnly Property TotalPaginasCount() As Integer
		//    Get
		//        Dim count As Integer = CInt(TotalItemsCount / ItemsCountLimit)
		//        If TotalItemsCount > 0 Then
		//            Return count + 1
		//        Else
		//            Return count
		//        End If
		//    End Get
		//End Property

		private int RemainingPaginasCount
		{
			get
			{
				int count = System.Convert.ToInt32(RemainingItemsCount / (double)ItemsCountLimit);
				// + 1 because we want to count the last page even if it's
				// item count does not reach the limit of items per page.
				if (RemainingItemsCount > 0)
				{
					return count + 1;
				}
				else
				{
					return 0;
				}
			}
		}

		// use a cached count. this means the count must be updated when needed
		private int mTotalItemsCount = -1;
		private int TotalItemsCount
		{
			get
			{
				return mTotalItemsCount;
			}
			set
			{
				mTotalItemsCount = value;
			}
		}

		// use a cached count. this means the count must be updated when needed    
		private int mRemainingItemsCount = -1;
		private int RemainingItemsCount
		{
			get
			{
				return mRemainingItemsCount;
			}
			set
			{
				if (value > 0)
				{
					mRemainingItemsCount = value;
				}
				else
				{
					mRemainingItemsCount = 0;
				}
			}
		}

		private int CurrentItemsCount
		{
			get
			{
				return Listview.Items.Count;
			}
		}

		// retrieves the number of items starting at the specified text
		private int countPages(IDbConnection connection, int currentPage)
		{
			int count = 0;
            
			if (GetPagesCount != null)
                GetPagesCount(this, currentPage, ref count, ref this.TotalElementosCount, connection);
			return count;
		}


		public void LoadListData(object selectedItemTag)
		{
			LoadListData(selectedItemTag, true);
		}

		public void LoadListData()
		{
			LoadListData(null, true);
		}

//INSTANT C# NOTE: C# does not support optional parameters. Overloaded method(s) are created above.
//ORIGINAL LINE: Public Sub LoadListData(Optional ByVal selectedItemTag As Object = null, Optional ByVal selectFirstItem As Boolean = true)
		public void LoadListData(object selectedItemTag, bool selectFirstItem)
		{
			long loadListDataTime = DateTime.Now.Ticks;
			Cursor oldCursor = null;
			try
			{
				oldCursor = Listview.Cursor;
				Listview.Parent.TopLevelControl.Cursor = Cursors.WaitCursor;

				try
				{
					GisaDataSetHelper.ManageDatasetConstraints(false);
					Listview.BeginUpdate();
					PaginatedLVGetItems returnedRows = null;

					bool deadlockOccurred = true;
					while (deadlockOccurred)
					{
						GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
						try
						{
							long calculate = DateTime.Now.Ticks;
							if (CalculateOrderedItems != null)
								CalculateOrderedItems(ho.Connection);
							Debug.WriteLine("<<CalculateOrderedItems>> total " + new TimeSpan(DateTime.Now.Ticks - calculate).ToString());


							// selectedItemTag virá preenchido se se pretender seleccionar 
							// um item específico após a população do lista
							if (selectedItemTag != null)
							{
								int pageNr = 0;
								// no caso deste evento não ser tratado por ninguém 
								// consideramos simplesmente que não é suportada a 
								// selecção de um item específico 
								if (GetPageForItemTag != null)
									GetPageForItemTag(selectedItemTag, ref pageNr, ho.Connection);
								CurrentPageNr = pageNr;
							}

							long count = DateTime.Now.Ticks;
							TotalPaginasCount = countPages(ho.Connection, ItemsCountLimit);
							//RemainingItemsCount = countItems(ho.Connection, LastBottomID) - ItemsCountLimit
							Debug.WriteLine("<<countItems>> total " + new TimeSpan(DateTime.Now.Ticks - count).ToString());

							long obterElementos = 0;
							obterElementos = DateTime.Now.Ticks;
							if (GetItems != null)
								GetItems(CurrentPageNr, ItemsCountLimit, ref returnedRows, ho.Connection);
							Debug.WriteLine("<<obterElementos>> total " + new TimeSpan(DateTime.Now.Ticks - obterElementos).ToString());

							//limpar
							long delete = DateTime.Now.Ticks;
							if (DeleteTemporaryResults != null)
								DeleteTemporaryResults(ho.Connection);
							Debug.WriteLine("<<DeleteTemporaryResults>> total " + new TimeSpan(DateTime.Now.Ticks - delete).ToString());

							deadlockOccurred = false;
						}
						catch (Exception ex)
						{
							Trace.WriteLine(ex);
							if (DBAbstractDataLayer.DataAccessRules.ExceptionHelper.isDeadlockException(ex))
							{
								returnedRows = null;
								deadlockOccurred = true;
							}
							else
								throw;
						}
						finally
						{
							ho.Dispose();
						}
					}
					long adicionarElementosLista = DateTime.Now.Ticks;
					if (AddItemsToList != null)
						AddItemsToList(returnedRows);
					Debug.WriteLine("<<adicionarElementosLista>> total " + new TimeSpan(DateTime.Now.Ticks - adicionarElementosLista).ToString());
				}
				catch (Exception ex)
				{
					Trace.WriteLine(ex);
					throw;
				}
				finally
				{
					Listview.EndUpdate();

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
				Listview.Parent.TopLevelControl.Cursor = oldCursor;
			}

			refreshNavigationState();

			if (Listview.Items.Count == 1)
			{
				if (selectedItemTag != null)
				{
					// Se, por alguma razão, acontecer o item procurado não 
					// chegar a ser encontrado também não será seleccionado 
					// nenhum item
                    Listview.selectItem(GUIHelper.GUIHelper.findListViewItemByTag(selectedItemTag, Listview));
				}
				else if (selectFirstItem)
				{
					//Listview.Focus()
					Listview.selectItem(Listview.Items[0]);
				}
			}
			else if (Listview.Items.Count > 0)
			{
				Listview.selectItem(null);
			}

			Debug.WriteLine("<<LoadListData>> total " + new TimeSpan(DateTime.Now.Ticks - loadListDataTime).ToString());
		}

		private void Listview_BeforeNewSelection(object sender, BeforeNewSelectionEventArgs e)
		{

			if (BeforeNewListSelection != null)
				BeforeNewListSelection(sender, e);
		}

		private void Listview_DeeperLevelSelection(object sender, DeeperLevelSelectionEventArgs e)
		{

			if (DeeperLevelSelection != null)
				DeeperLevelSelection(sender, e);
		}

		private void TxtNroPagina_BeforeNewSelection(object sender, GoToPageEventArgs e)
		{

			if (e.pageNr == 0 || e.pageNr > TotalPaginasCount || e.pageNr == CurrentPageNr)
			{
				e.success = false;
				return;
			}

			long click = 0;
			click = DateTime.Now.Ticks;

			bool successfulClearSelection = true;

			// esta operação tem como objectibo permitir que haja a possibilidade de cancelar a mudança de página
			if (Listview.SelectedItems.Count > 0)
			{
				successfulClearSelection = Listview.clearItemSelection(Listview.SelectedItems[0]);
			}

			if (! successfulClearSelection)
			{
				return;
			}

			CurrentPageNr = e.pageNr;

			// repopulate the listView
			Listview.Items.Clear();
			LoadListData();

			//If Listview.Items.Count > 0 Then
			//    Listview.selectItem(Listview.Items(0))
			//End If

			if (Listview.Items.Count == 1)
			{
				Listview.selectItem(Listview.Items[0]);
			}
			else if (Listview.Items.Count > 0)
			{
				Listview.selectItem(null);
			}

			//update buttons state and label text
			//refreshNavigationState()

			Debug.WriteLine("<<Go to page>> total " + new TimeSpan(DateTime.Now.Ticks - click).ToString());
		}

		//Private Sub Listview_SelectedIndexChanging(ByVal sender As Object, _
		//    ByVal e As ItemChangingEventArgs) _
		//    'Handles mListview.SelectedIndexChanged

		//    RaiseEvent ListSelectionChanged(sender, e)
		//End Sub


		private void btnAnterior_Click(object Sender, EventArgs e)
		{

			long click = 0;
			click = DateTime.Now.Ticks;

			bool successfulClearSelection = true;

			// esta operação tem como objectibo permitir que haja a possibilidade de cancelar a mudança de página
			if (Listview.SelectedItems.Count > 0)
			{
				successfulClearSelection = Listview.clearItemSelection(Listview.SelectedItems[0]);
			}

			if (! successfulClearSelection)
			{
				return;
			}

			CurrentPageNr -= 1;

			// repopulate the listView
			Listview.Items.Clear();
			LoadListData();

			//If Listview.Items.Count > 0 Then
			//    Listview.selectItem(Listview.Items(0))
			//End If

			if (Listview.Items.Count == 1)
			{
				Listview.selectItem(Listview.Items[0]);
			}
			else if (Listview.Items.Count > 0)
			{
				Listview.selectItem(null);
			}

			//update buttons state and label text
			//refreshNavigationState()

			Debug.WriteLine("<<Button Previous>> total " + new TimeSpan(DateTime.Now.Ticks - click).ToString());
		}

		private void btnProximo_Click(object Sender, EventArgs e)
		{

			long click = 0;
			click = DateTime.Now.Ticks;

			//Dim limparlista As Long
			//limparlista = DateTime.Now.Ticks
			//Debug.WriteLine("<<limparlista>> before " + limparlista.ToString())


			bool successfulClearSelection = true;

			if (Listview.SelectedItems.Count > 0)
			{
				successfulClearSelection = Listview.clearItemSelection(Listview.SelectedItems[0]);
			}

			//Debug.WriteLine("<<limparlista>> after " + DateTime.Now.Ticks.ToString())
			//Debug.WriteLine("<<limparlista>> total " + New TimeSpan(DateTime.Now.Ticks - limparlista).ToString())


			if (! successfulClearSelection)
			{
				return;
			}

			CurrentPageNr += 1;


			long carregar = 0;
			carregar = DateTime.Now.Ticks;

			// repopulate the listView
			Listview.Items.Clear();
			LoadListData();

			Debug.WriteLine("<<repopulate the listView>>: " + new TimeSpan(DateTime.Now.Ticks - carregar).ToString());


			//Dim seleccionar As Long
			//seleccionar = DateTime.Now.Ticks
			//Debug.WriteLine("<<seleccionar>> before " + seleccionar.ToString())

			//If Listview.Items.Count > 0 Then
			//    Listview.selectItem(Listview.Items(0))
			//End If

			//update buttons state and label text
			//refreshNavigationState()


			//Debug.WriteLine("<<seleccionar>> after " + DateTime.Now.Ticks.ToString())
			//Debug.WriteLine("<<seleccionar>> total " + New TimeSpan(DateTime.Now.Ticks - seleccionar).ToString())

			Debug.WriteLine("<<Button Next>> total " + new TimeSpan(DateTime.Now.Ticks - click).ToString());

		}

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

		private void refreshNavigationState()
		{
			txtNroPagina.Text = CurrentPageNr.ToString();
            txtNroPagina.lastPageSelected = txtNroPagina.Text; // isto só deve ser feito quando o controlo está a ser inicializado

			btnAnterior.Enabled = (CurrentPageNr > 1);

			// get remaining number of items
			btnProximo.Enabled = (TotalPaginasCount - CurrentPageNr > 0);

			// update label
            if (!this.contadorElementos)
			    grpBox.Text = string.Format(originalLabel + " (Página {0} em {1})", CurrentPageNr, ((TotalPaginasCount == 0) ? 1 : TotalPaginasCount));
            else
                grpBox.Text = string.Format(originalLabel + ": {2} (Página {0} em {1})", CurrentPageNr, ((TotalPaginasCount == 0) ? 1 : TotalPaginasCount), this.TotalElementosCount);

		}

		//Private Sub resetPagesStack()
		//    stackLastTexts.Clear()
		//    stackLastTexts.Push(String.Empty)
		//End Sub

		public void resetList()
		{
			CurrentPageNr = 1;
			//resetPagesStack()
			// limpa explicitamente a selecção de modo a lançar um selectedindexchanged
			if (Listview.SelectedItems.Count > 0)
			{
				Listview.clearItemSelection(Listview.SelectedItems[0]);
			}
			Listview.Items.Clear();
		}

	}


} //end of root namespace