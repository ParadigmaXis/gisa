using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;
using System.Timers;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace GISA.Controls
{
	#region Class Events
	public class ItemChangingEventArgs :  CancelEventArgs
	{
		public ItemChangingEventArgs(int index)
		{
			this.m_index = index;
		}

		private int m_index;
		public int Index
		{
			get {return this.m_index;}
		}
	}

	public class ItemSubItemClickEventArgs : EventArgs
	{
		protected int mItemIndex;
		protected int mSubItemIndex;
		protected PxListView.MouseEventsTypes mMouseEvent;
		public ItemSubItemClickEventArgs(int ItemIndex, int SubItemIndex, PxListView.MouseEventsTypes mouseEvent)
		{
			this.mItemIndex = ItemIndex;
			this.mSubItemIndex = SubItemIndex;
			this.mMouseEvent = mouseEvent;
		}
		
		public int ItemIndex
		{
			get {return this.mItemIndex;}
		}
		
		public int SubItemIndex
		{
			get {return this.mSubItemIndex;}
		}

		public PxListView.MouseEventsTypes MouseEvent
		{
			get {return this.mMouseEvent;}
		}
	}

	// ToDo: BeforeNewSelectionEventArgs e DeeperLevelSelectionEventArgs são exactamente iguais
	//      -> passar a usar somente uma delas e se possível alterar o nome para algo mais genérico
	//		   de forma a se adequar às situações onde são usadas

	// classe que define os argumentos utilizados durante os processos de mudança de contexto
	public class BeforeNewSelectionEventArgs: EventArgs 
	{
		public BeforeNewSelectionEventArgs(ListViewItem itemToBeSelected, bool selectionChange)
		{
			this.m_itemToBeSelected = itemToBeSelected;
			this.m_selectionChange = selectionChange;
		}

		//indica qual o item que vai ser selecionado (usado pelo UpdateContext())
		private ListViewItem m_itemToBeSelected;
		public ListViewItem ItemToBeSelected 
		{
			get {return this.m_itemToBeSelected;}
			set {this.m_itemToBeSelected = value;}
		}

		//indica se a gravação foi bem sucedida ou não
		private bool m_selectionChange;
		public bool SelectionChange
		{
			get {return this.m_selectionChange;}
			set {m_selectionChange = value;}
		}
	}

	// classe que define os argumentos para o evento de duplo click
	public class DeeperLevelSelectionEventArgs: EventArgs
	{
		public DeeperLevelSelectionEventArgs (ListViewItem itemToBeSelected, bool selectionChange)
		{
			this.m_itemToBeSelected = itemToBeSelected;
			this.m_selectionChange = selectionChange;
		}

		private ListViewItem m_itemToBeSelected;
		public ListViewItem ItemToBeSelected 
		{
			get {return this.m_itemToBeSelected;}
			set {this.m_itemToBeSelected = value;}
		}

		//indica se a gravação foi bem sucedida ou não
		private bool m_selectionChange;
		public bool SelectionChange
		{
			get {return this.m_selectionChange;}
			set {m_selectionChange = value;}
		}
	}

	// classe que define os argumentos para o evento de click no cabeçalho de uma coluna
	public class MyColumnClickEventArgs: EventArgs
	{
        public MyColumnClickEventArgs(SortedDictionary<int, ListViewOrderedColumns.ColumnSortOrderInfo> sortColumnOrder)
		{
			this.m_sortColumnOrder = sortColumnOrder;				
		}

        private SortedDictionary<int, ListViewOrderedColumns.ColumnSortOrderInfo> m_sortColumnOrder;
        public SortedDictionary<int, ListViewOrderedColumns.ColumnSortOrderInfo> SortColumnOrder
        {
            get {return this.m_sortColumnOrder;}
            set {this.m_sortColumnOrder = value;}
        }			
	}
	#endregion

	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class PxListView : System.Windows.Forms.ListView
	{
		// Fields
		public const int WM_NOTIFY = 0x4e;
		public const int NM_FIRST = 0;
		public const int NM_RCLICK = NM_FIRST - 5;

		private System.Windows.Forms.ContextMenu OrdenacaoContextMenu;
		private System.Windows.Forms.MenuItem LimparOrdenacao;
        private readonly ListViewItem EMPTY_ITEM = new ListViewItem("empty selection");

		// Nested Types
		[StructLayout(LayoutKind.Sequential)]
			public struct NMHDR
		{
			public IntPtr hwndFrom;
			public int idFrom;
			public int code;
		}

		private ListViewOrderedColumns lstVwOC = null;
		public PxListView()
		{
			clickTypeSelectorTimer.Tick += new EventHandler(OnTimedEvent);
			clickTypeSelectorTimer.Interval = 300;

			this.OrdenacaoContextMenu = new System.Windows.Forms.ContextMenu();
			this.LimparOrdenacao = new System.Windows.Forms.MenuItem();

			//
			// LimparOrdenacao
			//
			this.LimparOrdenacao.Index = 0;
			this.LimparOrdenacao.Text = "Limpar Ordenação";
			this.LimparOrdenacao.Click += new EventHandler(LimparOrdenacao_Click);
			//
			// OrdenacaoContextMenu
			//
			this.OrdenacaoContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {this.LimparOrdenacao});
		}

        // adicionar ícones para os cabeçalhos da listview
        private void AppendOrderIcons(ref ImageList imageList)
        {
            Bitmap bmp = null;            
            Font f = new Font(System.Drawing.FontFamily.GenericSerif, 8);
            for (int i = 1; i <= this.Columns.Count; i++)
            {
                bmp = BuildIcon(IconOrderDown, f, i.ToString());
                imageList.Images.Add(bmp);

                bmp = BuildIcon(IconOrderUp, f, i.ToString());
                imageList.Images.Add(bmp);
            }
        }

        private Bitmap BuildIcon(Bitmap iconBase, Font f, string label)
        {
            Graphics g = Graphics.FromImage(iconBase);
            g.DrawString(label, f, SystemBrushes.ControlDarkDark, 9, 1);
            g.Dispose();

            return iconBase;
        }

        #region API stuff
        //
        // The API function ChildWindowFromPoint is used to find out the window handle of
        // the columns header.
        //
        [DllImport("user32")]
        public static extern System.IntPtr 
			ChildWindowFromPoint(System.IntPtr hwnd, int xPoint, int yPoint);

		[DllImport("user32")] public static extern int 
			GetMessagePos();
		#endregion

        int contextMenuSet = -1;
		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case WM_NOTIFY:
                    //NMHDR nm = (NMHDR)m.GetLParam(typeof(NMHDR));
                    //if (nm.code == NM_RCLICK)
                    //{
                    //    if (mCustomizedSorting)
                    //    {
                    //        Point msgPos = LParamToPoint((int)GetMessagePos());
                    //        msgPos = this.PointToClient(msgPos);
                    //        OrdenacaoContextMenu.Show(this, msgPos);
                    //    }
                    //}
                    break;
                case 0x210: //WM_PARENTNOTIFY
                    contextMenuSet = 1;
                    break;
                case 0x21: //WM_MOUSEACTIVATE
                    contextMenuSet++;
                    break;
                case 0x7b: //WM_CONTEXTMENU
                    if (contextMenuSet == 2 && mCustomizedSorting)
                    {
                        Point msgPos = LParamToPoint((int)GetMessagePos());
                        msgPos = this.PointToClient(msgPos);
                        OrdenacaoContextMenu.Show(this, msgPos);
                    }
                    break;
            }
		}

		public static Point LParamToPoint(int lParam)
		{
			uint ulParam = (uint)lParam;
			return new Point(
				(int)(ulParam & 0x0000ffff),
				(int)((ulParam & 0xffff0000) >> 16));
		}
		private bool mReturnSubItemIndex = false;
		public bool ReturnSubItemIndex 
		{
			get {return mReturnSubItemIndex;}
			set {mReturnSubItemIndex = value;}
		}

		#region Properties
		private bool mSuccessfullSave = true;
		private bool SucessfullSave
		{
			get {return mSuccessfullSave;}
			set {mSuccessfullSave = value;}
		}


		private System.Windows.Forms.ListViewItem mLastItem = null;
		private System.Windows.Forms.ListViewItem LastItem
		{
			get {return mLastItem;}
			set {mLastItem = value;}
		}

		private System.Windows.Forms.ListViewItem mNewItem = null;
		private System.Windows.Forms.ListViewItem NewItem
		{
			get {return mNewItem;}
			set {mNewItem = value;}
		}

		private bool mCustomizedSorting = false;
		public bool CustomizedSorting 
		{
			get {return mCustomizedSorting;}
            set 
			{
				if (value)
				{
					if (!mCustomizedSorting)
					{
						base.ColumnClick += new ColumnClickEventHandler(MyBase_ColumnClick);
                        this.lstVwOC = new ListViewOrderedColumns();
                        this.HeaderStyle = ColumnHeaderStyle.Clickable;
					}
				}
				else
				{					
					base.ColumnClick -= new ColumnClickEventHandler(MyBase_ColumnClick);
                    this.lstVwOC = null;
                    this.HeaderStyle = ColumnHeaderStyle.Nonclickable;
				}

				mCustomizedSorting = value;
			}
		}

        private Bitmap mIconOrderUp = null;
        private Bitmap IconOrderUp
        {
            get
            {
                if (mIconOrderUp == null)
                {
                    Bitmap newBitmap = new Bitmap(16, 16, PixelFormat.Format32bppPArgb);
                    Graphics g = Graphics.FromImage(newBitmap);

                    PointF[] Triangle = new PointF[3];

                    Triangle[0].X = 0;
                    Triangle[0].Y = 11;
                    Triangle[1].X = 10;
                    Triangle[1].Y = 11;
                    Triangle[2].X = 5;
                    Triangle[2].Y = 4;

                    g.FillRectangle(SystemBrushes.Control, new Rectangle(0, 0, 16, 16));
                    g.FillPolygon(SystemBrushes.ControlDark, Triangle);

                    mIconOrderUp = newBitmap;

                    return (Bitmap)mIconOrderUp.Clone();
                }
                else
                    return (Bitmap)mIconOrderUp.Clone();
            }
        }

        private Bitmap mIconOrderDown = null;
        private Bitmap IconOrderDown
        {
            get
            {
                if (mIconOrderDown == null)
                {
                    Bitmap newBitmap = new Bitmap(16, 16, PixelFormat.Format32bppPArgb);
                    Graphics g = Graphics.FromImage(newBitmap);

                    PointF[] Triangle = new PointF[3];

                    Triangle[0].X = 0;
                    Triangle[0].Y = 5;
                    Triangle[1].X = 9;
                    Triangle[1].Y = 5;
                    Triangle[2].X = 4.5f;
                    Triangle[2].Y = 11;

                    g.FillRectangle(SystemBrushes.Control, new Rectangle(0, 0, 16, 16));
                    g.FillPolygon(SystemBrushes.ControlDark, Triangle);

                    mIconOrderDown = newBitmap;

                    return (Bitmap)mIconOrderDown.Clone();
                }
                else
                    return (Bitmap)mIconOrderDown.Clone();
            }
        }

        private int firstOrderIconIndex = 0;
        public new ImageList SmallImageList
        {
            get { return ((ListView)this).SmallImageList; }
            set {
                firstOrderIconIndex = value.Images.Count;
                ImageList imageList = value;
                AppendOrderIcons(ref imageList);
                ((ListView)this).SmallImageList = imageList; 
            }
        }
		#endregion

        public event EventHandler<EventArgs> ContextFormEvent;
        protected virtual void OnContextFormEvent(EventArgs e)
        {
            if (this.ContextFormEvent != null)
                ContextFormEvent(this, e);
        }

        public event EventHandler<BeforeNewSelectionEventArgs> BeforeNewSelection;
		protected virtual void OnBeforeNewSelection(BeforeNewSelectionEventArgs e)
		{
			if (this.BeforeNewSelection != null)
                BeforeNewSelection(this, e);
		}

        public event EventHandler<DeeperLevelSelectionEventArgs> DeeperLevelSelection;
		protected virtual void OnDeeperLevelSelection(DeeperLevelSelectionEventArgs e)
		{
            if (this.DeeperLevelSelection != null)
                DeeperLevelSelection(this, e);
            else
                e.SelectionChange = false;
		}

		public event MyColumnClickEventHandler MyColumnClick;
		public delegate void MyColumnClickEventHandler(object sender, MyColumnClickEventArgs e);
		protected virtual void OnMyColumnClick(MyColumnClickEventArgs e)
		{
			if (this.MyColumnClick != null)
                MyColumnClick(this, e);
		}

		public enum MouseEventsTypes
		{
			MouseDown = 0,
			MouseMove = 1
		}

		//booleanos que garantem que os eventos são tratados isolada e coerentemente entre si
		//(não são tratados eventos do teclado ao mesmo tempo dos de rato e vice-versa; após um mouseDown
		//só uns dos eventos mouseUp e mouseLeave é que são tratados)
		private static bool execMouseLeave = false;
		private static bool execMouseUp = false;
		private static bool execMouseDown = true;
		private static bool doubleClick = false;
		private static bool execKeyDown = true;
		private static bool execKeyUp = false;
		#region Mouse events
	
		// listview
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
			private struct LVHITTESTINFO
		{
			public Point pt;
			public int flags;
			public int iItem;
			public int iSubItem;
		}
		private const int LVM_FIRST = 0x1000;
		private const int LVM_SUBITEMHITTEST = LVM_FIRST + 57; 
        private const int LVM_GETHEADER = LVM_FIRST + 31; 

		[DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
		private static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, IntPtr lParam);
		// overloaded for wParam type
		[DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
		private static extern IntPtr SendMessage(IntPtr hWnd, int uMsg, IntPtr wParam, IntPtr lParam);

		private void ListView_SubItemHitTest(MouseEventsTypes met)
		{
			LVHITTESTINFO lvh = new LVHITTESTINFO();
			lvh.pt = PointToClient(Control.MousePosition);

			IntPtr ptr = Marshal.AllocHGlobal( Marshal.SizeOf(lvh) );
			Marshal.StructureToPtr(lvh, ptr, true);
								
			SendMessage(Handle, LVM_SUBITEMHITTEST, IntPtr.Zero, ptr);
								
			lvh = (LVHITTESTINFO)Marshal.PtrToStructure(ptr, typeof(LVHITTESTINFO));
			Marshal.FreeHGlobal(ptr);			

			// check if the item is valid
            if ((lvh.iItem >= 0) || (lvh.iSubItem >= 0))
            {
                ItemSubItemClickEventArgs args = new ItemSubItemClickEventArgs(lvh.iItem, lvh.iSubItem, met);
                this.OnItemSubItemClick(args);
            }
            else
                this.Cursor = Cursors.Default;
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown (e);

            if (e.Button == MouseButtons.Right)
            {
                EventArgs args = new EventArgs();
                this.OnContextFormEvent(args);
                return;
            }

			if (ReturnSubItemIndex)
			{
				ListView_SubItemHitTest(MouseEventsTypes.MouseDown);
			}			

			if (execMouseDown)
			{
				execKeyDown = false;
				if (e.Clicks > 1)
					doubleClick = true;				

				int x = e.X;
				int y = e.Y;

				var item = this.GetItemAt(x, y);

                if (item == null)
                    item = EMPTY_ITEM;

				Debug.WriteLine(">> Initializing selecting process for " + item.Text);

				mNewItem = item;
			}
			execMouseLeave = true;
			execMouseUp = true;
		}		

		public event ItemSubItemClickEventHandler ItemSubItemClick;
		public delegate void ItemSubItemClickEventHandler(object sender, ItemSubItemClickEventArgs e);
		protected virtual void OnItemSubItemClick(ItemSubItemClickEventArgs e)
		{
			if (this.ItemSubItemClick != null)
			{
				ItemSubItemClick(this, e);
			}
		}

		//private long lastClick = 0;
		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseUp (e);
			execMouseLeave = false;
			execMouseDown = false;

			if (execMouseUp)
			{
				if (!doubleClick)
					clickTypeSelectorTimer.Start();
				else
				{
					clickTypeSelectorTimer.Stop();
					doubleClick = false;
					selectDeeperLevel();
				}
			}
			execMouseUp = false;
			execMouseDown = true;
			execKeyDown = true;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove (e);

			if (ReturnSubItemIndex)
				ListView_SubItemHitTest(MouseEventsTypes.MouseMove);
		}


		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave (e);
			execMouseUp = false;
			execMouseDown = false;

			if (execMouseLeave)
			{
				BeforeNewSelectionEventArgs args1 = new BeforeNewSelectionEventArgs(mNewItem, mSuccessfullSave);
				this.OnBeforeNewSelection(args1);

				mSuccessfullSave = args1.SelectionChange;

				if (mSuccessfullSave)
				{
					mNewItem.Selected = true;
					mLastItem = mNewItem;
				}

				rollbackItemSelected();

				this.EndUpdate();
			}
			execMouseLeave = false;
			execKeyDown = true;
			execMouseDown = true;
		}

		private System.Windows.Forms.Timer clickTypeSelectorTimer = new System.Windows.Forms.Timer();
		private void OnTimedEvent(object source, EventArgs e)
		{
			selectNewItem();
		}

		private void selectNewItem() 
		{
			clickTypeSelectorTimer.Stop();
            if (mNewItem.Equals(mLastItem) || (mLastItem == null && mNewItem.Equals(EMPTY_ITEM))) 
                return;
            this.Cursor = Cursors.WaitCursor;
            long start = DateTime.Now.Ticks;
			this.BeginUpdate();
			BeforeNewSelectionEventArgs args1 = new BeforeNewSelectionEventArgs(mNewItem, mSuccessfullSave);
			this.OnBeforeNewSelection(args1);

			mSuccessfullSave = args1.SelectionChange; 

			if (mSuccessfullSave)
			{
				mNewItem.Selected = true;
				mLastItem = mNewItem;
			}

			rollbackItemSelected();	
	
			this.EndUpdate();
            this.Cursor = Cursors.Default;
            Trace.WriteLine(string.Format("Select New Item ({0}) in: {1}", args1.ItemToBeSelected.Text, new TimeSpan(DateTime.Now.Ticks - start).ToString()));
		}

		private void selectDeeperLevel()
		{
			this.BeginUpdate();
			execMouseUp = false; // impedir que o evento passe a ser tratado com um click normal
			DeeperLevelSelectionEventArgs args = new DeeperLevelSelectionEventArgs(mNewItem, mSuccessfullSave);
			this.OnDeeperLevelSelection(args);
			mSuccessfullSave = args.SelectionChange; 
			rollbackItemSelected();	
			this.EndUpdate();
		}
		#endregion

		#region Keyboard events
		protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
		{
            if (!execKeyDown)
            {
                e.Handled = true;
                base.OnKeyDown(e);
            }
            else
            {
                base.OnKeyDown(e);
                execMouseDown = false;
                execMouseLeave = false;
                execMouseUp = false;
                doubleClick = false;
                execKeyUp = true;

                System.Windows.Forms.ListViewItem item = null;

                if (this.SelectedItems.Count == 1)
                {
                    if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Home || e.KeyCode == Keys.End)
                    {
                        item = new ListViewItem();
                        switch (e.KeyCode)
                        {
                            case Keys.Up:
                                if (this.Items.IndexOf(this.SelectedItems[0]) > 0)
                                    item = this.Items[this.Items.IndexOf(this.SelectedItems[0]) - 1];
                                else
                                    item = this.Items[0];
                                break;
                            case Keys.Down:
                                if (this.Items.IndexOf(this.SelectedItems[0]) < this.Items.Count - 1)
                                    item = this.Items[this.Items.IndexOf(this.SelectedItems[0]) + 1];
                                else
                                    item = this.Items[this.Items.Count - 1];
                                break;
                            case Keys.Home:
                                item = this.Items[0];
                                break;
                            case Keys.End:
                                item = this.Items[this.Items.Count - 1];
                                break;
                        }

                        BeforeNewSelectionEventArgs args1 = new BeforeNewSelectionEventArgs(item, mSuccessfullSave);
                        this.OnBeforeNewSelection(args1);

                        mSuccessfullSave = args1.SelectionChange;

                        if (mSuccessfullSave)
                        {
                            item.Selected = true;
                            mLastItem = item;
                        }
                    }
                    else if (e.KeyCode == Keys.Alt || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey)
                        execMouseDown = true; //permitir selecionar mais do que um elemento da lista usando ctrl/shift

                    else //if (!e.Alt && !e.Control && !e.Shift  && ((e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9) || (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9) || (e.KeyCode < Keys.Z || e.KeyCode > Keys.A)))
                    {
                        KeysConverter kc = new KeysConverter();
                        string keychar = kc.ConvertToString(e.KeyCode);

                        if (keychar.Contains("NumPad"))
                            keychar = keychar.Replace("NumPad", "");
                        if (keychar.Contains("OemMinus"))
                            keychar = keychar.Replace("OemMinus", "_");

                        int itemIndex = -1;
                        List<ListViewItem> matchItems = new List<ListViewItem>();
                        foreach (ListViewItem lvItem in this.Items)
                        {
                            if (lvItem.Text.ToLower().StartsWith(keychar.ToLower()))
                            {
                                matchItems.Add(lvItem);
                                if (this.SelectedItems.Count > 0 && this.SelectedItems[0] == lvItem)
                                    itemIndex = matchItems.Count - 1;
                            }
                        }

                        if (matchItems.Count == 1 && this.SelectedItems[0] != matchItems[0])
                            item = matchItems[0];
                        else if (matchItems.Count > 1)
                        {
                            if (itemIndex < 0)
                                item = matchItems[0];
                            else
                            {
                                if (itemIndex == matchItems.Count - 1)
                                    item = matchItems[0];
                                else
                                    item = matchItems[itemIndex + 1];
                            }
                        }

                        if (item != null)
                        {
                            BeforeNewSelectionEventArgs args1 = new BeforeNewSelectionEventArgs(item, mSuccessfullSave);
                            this.OnBeforeNewSelection(args1);

                            mSuccessfullSave = args1.SelectionChange;

                            if (mSuccessfullSave)
                            {
                                item.Selected = true;
                                mLastItem = item;
                            }
                            execMouseDown = true;
                        }
                    }
                }
                else if (e.Alt || e.Control || e.Shift)
                    execMouseDown = true; //permitir selecionar mais do que um elemento da lista usando ctrl/shift
                else
                    mSuccessfullSave = false;
            }
		}

		protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
		{
            base.OnKeyUp (e);

			if (execKeyUp)
			{
				rollbackItemSelected();
				execKeyUp = false;
				execMouseDown = true;
			}
		}
		#endregion

		#region Column Header events
		private void MyBase_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
            ListViewOrderedColumns.ColumnSortOrderInfo colSOI = lstVwOC.ManageColumnToSort(this.Columns[e.Column]);
            RefreshColumnIcon(colSOI);
            MyColumnClickEventArgs args = new MyColumnClickEventArgs(lstVwOC.GetOrder());
			this.OnMyColumnClick(args);
		}

        public void RefreshAllColumnsIcons()
        {
            foreach (ListViewOrderedColumns.ColumnSortOrderInfo colSOI in lstVwOC.GetOrder().Values)
                RefreshColumnIcon(colSOI);
        }

        private void RefreshColumnIcon(ListViewOrderedColumns.ColumnSortOrderInfo colSOI)
        {
            if (colSOI.columnSortOrder == ListViewOrderedColumns.MySortOrder.Descendente)
                colSOI.column.ImageIndex = firstOrderIconIndex + (2 * (colSOI.order - 1));
            else
                colSOI.column.ImageIndex = firstOrderIconIndex + (2 * (colSOI.order - 1)) + 1;

        }

        public SortedDictionary<int, ListViewOrderedColumns.ColumnSortOrderInfo> GetOrder()
        {
            return lstVwOC.GetOrder();
        }
		#endregion

		#region Column Header Right Click events
		private void LimparOrdenacao_Click(object sender, System.EventArgs e)
		{
            ClearSort();
		}

        public void ClearSort()
        {
            lstVwOC.ClearSort();
            foreach (ColumnHeader col in this.Columns)
            {
                col.ImageKey = null;
                col.TextAlign = HorizontalAlignment.Left;
            }
        }
		#endregion

		private void rollbackItemSelected()
		{
			if (!mSuccessfullSave)
			{
				System.Diagnostics.Trace.WriteLine("Rollbacking PxListView selection...");
				foreach (System.Windows.Forms.ListViewItem item in this.SelectedItems)
					item.Selected = false;

				if (mLastItem != null)
				{
					mNewItem = mLastItem;
					mLastItem.Selected = true;
                    mLastItem.EnsureVisible();
                    mLastItem.Focused = true;
				}
				mSuccessfullSave = true;
			}
		}

		public void selectItem(ListViewItem item) {
			if (item == null){
				return;
			}
            long click = DateTime.Now.Ticks;
			this.BeginUpdate();
			BeforeNewSelectionEventArgs args1 = new BeforeNewSelectionEventArgs(item, true);
			this.OnBeforeNewSelection(args1);
			if (args1.SelectionChange) {
				item.Selected = true;
				mLastItem = item;
			}
			this.EndUpdate();
            Debug.WriteLine("<<ListSelect " + item.Text +  ">> " + new TimeSpan(DateTime.Now.Ticks - click).ToString());
		}

		public bool clearItemSelection(ListViewItem item) {
			if (item == null){
				return false;
			}
			this.BeginUpdate();
			BeforeNewSelectionEventArgs args1 = new BeforeNewSelectionEventArgs(new ListViewItem(), true);
			this.OnBeforeNewSelection(args1);
			if (args1.SelectionChange)
				item.Selected = false;
			this.EndUpdate();
			return args1.SelectionChange;
		}

        public void AddColumnToSort(ColumnHeader col, ListViewOrderedColumns.MySortOrder sort, int order)
        {
            ListViewOrderedColumns.ColumnSortOrderInfo colSOI = lstVwOC.AddColumnToSortList(col, sort, order);
            RefreshColumnIcon(colSOI);
        }
	}

    public class ListViewOrderedColumns
    {
        public ListViewOrderedColumns() { }

        public enum MySortOrder
        {
            Ascendente = 0,
            Descendente = 1
        }

        // estrutura de dados que mantem a informação sobre o sentido de ordenação de uma coluna e qual a sua ordem relativamente a
        // as outras
        public struct ColumnSortOrderInfo
        {
            public ColumnHeader column;
            public MySortOrder columnSortOrder;
            public int order;

            public ColumnSortOrderInfo(ColumnHeader col, MySortOrder colSortOrder, int order)
            {
                this.column = col;
                this.columnSortOrder = colSortOrder;
                this.order = order;
            }
        }

        private Dictionary<ColumnHeader, ColumnSortOrderInfo> mColumns = new Dictionary<ColumnHeader, ColumnSortOrderInfo>();
        public Dictionary<ColumnHeader, ColumnSortOrderInfo> Columns { 
            get { return mColumns; }
            set { mColumns = value; }
        }

        public ColumnSortOrderInfo ManageColumnToSort(ColumnHeader col)
        {
            ColumnSortOrderInfo colSOI;
            if (mColumns.ContainsKey(col))
            {
                colSOI = mColumns[col];
                mColumns.Remove(col);
                if (colSOI.columnSortOrder == MySortOrder.Ascendente)
                    colSOI.columnSortOrder = MySortOrder.Descendente;
                else
                    colSOI.columnSortOrder = MySortOrder.Ascendente;
                mColumns.Add(col, colSOI);
            }
            else
                colSOI = AddColumnToSortList(col, MySortOrder.Ascendente, mColumns.Count + 1);
            
            return colSOI;
        }

        internal ColumnSortOrderInfo AddColumnToSortList (ColumnHeader col, MySortOrder sortOrder, int order) {
            ColumnSortOrderInfo colSOI;
            colSOI = new ColumnSortOrderInfo(col, sortOrder, order);
            mColumns.Add(col, colSOI);
            return colSOI;
        }

        //Limpar critérios de ordenação
        internal void ClearSort()
        {
            mColumns.Clear();            
        }

        //Obter os critérios de ordenação definidos
        internal SortedDictionary<int, ColumnSortOrderInfo> GetOrder()
        {
            SortedDictionary<int, ColumnSortOrderInfo> orderedColumns = new SortedDictionary<int, ColumnSortOrderInfo>();
            foreach (ColumnSortOrderInfo colSOI in mColumns.Values)
                orderedColumns.Add(colSOI.order, colSOI);

            return orderedColumns;
        }
    }
}
