using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using GISA.SharedResources;

namespace GISA.Controls
{
	/// <summary>
	/// Summary description for ControloEdicaoOrdenacao.
	/// </summary>
	public class ControloEdicaoOrdenacao : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView lstVwColunas;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnAddAll;
		private System.Windows.Forms.Button btnRemoveAll;
		private System.Windows.Forms.ListView lstVwOrdenacao;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.DomainUpDown domainSort;
		private System.Windows.Forms.NumericUpDown numOrder;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ControloEdicaoOrdenacao()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			domainSort.SelectedIndex = 0;
			GetExtraResources();
		}

        public ControloEdicaoOrdenacao(List<ColumnHeader> columns, List<ListViewOrderedColumns.ColumnSortOrderInfo> orderedColumns)
            : this()
        {
            mListaColunas = columns;
            mListaColunasOrdenadas = orderedColumns;

            PopulateLists();
        }

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lstVwColunas = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnAddAll = new System.Windows.Forms.Button();
			this.btnRemoveAll = new System.Windows.Forms.Button();
			this.lstVwOrdenacao = new System.Windows.Forms.ListView();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.domainSort = new System.Windows.Forms.DomainUpDown();
			this.numOrder = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numOrder)).BeginInit();
			this.SuspendLayout();
			// 
			// lstVwColunas
			// 
			this.lstVwColunas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						   this.columnHeader1});
			this.lstVwColunas.FullRowSelect = true;
			this.lstVwColunas.HideSelection = false;
			this.lstVwColunas.Location = new System.Drawing.Point(16, 16);
			this.lstVwColunas.Name = "lstVwColunas";
			this.lstVwColunas.Size = new System.Drawing.Size(136, 208);
			this.lstVwColunas.TabIndex = 0;
			this.lstVwColunas.View = System.Windows.Forms.View.Details;
			this.lstVwColunas.SelectedIndexChanged += new System.EventHandler(this.lstVwColunas_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Colunas";
			this.columnHeader1.Width = 130;
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(168, 40);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(32, 24);
			this.btnAdd.TabIndex = 1;
			this.btnAdd.Text = "->";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(168, 80);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(32, 24);
			this.btnRemove.TabIndex = 2;
			this.btnRemove.Text = "<-";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnAddAll
			// 
			this.btnAddAll.Location = new System.Drawing.Point(168, 120);
			this.btnAddAll.Name = "btnAddAll";
			this.btnAddAll.Size = new System.Drawing.Size(32, 24);
			this.btnAddAll.TabIndex = 3;
			this.btnAddAll.Text = ">>";
			this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
			// 
			// btnRemoveAll
			// 
			this.btnRemoveAll.Location = new System.Drawing.Point(168, 160);
			this.btnRemoveAll.Name = "btnRemoveAll";
			this.btnRemoveAll.Size = new System.Drawing.Size(32, 24);
			this.btnRemoveAll.TabIndex = 4;
			this.btnRemoveAll.Text = "<<";
			this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
			// 
			// lstVwOrdenacao
			// 
			this.lstVwOrdenacao.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							 this.columnHeader2,
																							 this.columnHeader3,
																							 this.columnHeader4});
			this.lstVwOrdenacao.FullRowSelect = true;
			this.lstVwOrdenacao.HideSelection = false;
			this.lstVwOrdenacao.Location = new System.Drawing.Point(224, 16);
			this.lstVwOrdenacao.Name = "lstVwOrdenacao";
			this.lstVwOrdenacao.Size = new System.Drawing.Size(248, 208);
			this.lstVwOrdenacao.TabIndex = 5;
			this.lstVwOrdenacao.View = System.Windows.Forms.View.Details;
			this.lstVwOrdenacao.SelectedIndexChanged += new System.EventHandler(this.lstVwOrdenacao_SelectedIndexChanged);
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "";
			this.columnHeader2.Width = 20;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Colunas";
			this.columnHeader3.Width = 148;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Ordenação";
			this.columnHeader4.Width = 72;
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(477, 192);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(40, 24);
			this.btnOk.TabIndex = 6;
			this.btnOk.Text = "Ok";
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(528, 192);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(64, 24);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancelar";
			// 
			// domainSort
			// 
			this.domainSort.Items.Add("Asc");
			this.domainSort.Items.Add("Desc");
			this.domainSort.Location = new System.Drawing.Point(488, 80);
			this.domainSort.Name = "domainSort";
			this.domainSort.ReadOnly = true;
			this.domainSort.Size = new System.Drawing.Size(56, 20);
			this.domainSort.TabIndex = 8;
			this.domainSort.Tag = "";
			this.domainSort.Click += new System.EventHandler(this.domainSort_Click);
			// 
			// numOrder
			// 
			this.numOrder.Location = new System.Drawing.Point(488, 48);
			this.numOrder.Minimum = new System.Decimal(new int[] {
																	 1,
																	 0,
																	 0,
																	 0});
			this.numOrder.Name = "numOrder";
			this.numOrder.Size = new System.Drawing.Size(56, 20);
			this.numOrder.TabIndex = 9;
			this.numOrder.Value = new System.Decimal(new int[] {
																   1,
																   0,
																   0,
																   0});			
			this.numOrder.ValueChanged += new EventHandler(numOrder_ValueChanged);
			// 
			// ControloEdicaoOrdenacao
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(600, 232);
			this.ControlBox = false;
			this.Controls.Add(this.numOrder);
			this.Controls.Add(this.domainSort);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.lstVwOrdenacao);
			this.Controls.Add(this.btnRemoveAll);
			this.Controls.Add(this.btnAddAll);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.lstVwColunas);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "ControloEdicaoOrdenacao";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edição dos critérios de ordenação";
			((System.ComponentModel.ISupportInitialize)(this.numOrder)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void GetExtraResources() 
		{
			btnAdd.Image = SharedResourcesOld.CurrentSharedResources.AdicionaUm;
			btnAddAll.Image = SharedResourcesOld.CurrentSharedResources.AdicionaTodos;
			btnRemove.Image = SharedResourcesOld.CurrentSharedResources.RemoveUm;
			btnRemoveAll.Image = SharedResourcesOld.CurrentSharedResources.RemoveTodos;
		}
		
		#region Properties
        private List<ColumnHeader> mListaColunas = null;
        private List<ListViewOrderedColumns.ColumnSortOrderInfo> mListaColunasOrdenadas = null;
        public List<ListViewOrderedColumns.ColumnSortOrderInfo> ListaColunasOrdenadas 
		{
			get
			{
                mListaColunasOrdenadas = new List<ListViewOrderedColumns.ColumnSortOrderInfo>();
				foreach (ListViewItem item in lstVwOrdenacao.Items)
				{
                    mListaColunasOrdenadas.Add(
                        new ListViewOrderedColumns.ColumnSortOrderInfo(
                            (ColumnHeader)item.Tag,
                            (ListViewOrderedColumns.MySortOrder)Enum.Parse(typeof(ListViewOrderedColumns.MySortOrder), item.SubItems[2].Text),
                            lstVwOrdenacao.Items.IndexOf(item) + 1));
				}
				return mListaColunasOrdenadas;
			}
			set 
			{
				mListaColunasOrdenadas = value;
			}
		}
		#endregion

		public void PopulateLists()
		{
			lstVwOrdenacao.ListViewItemSorter = new ListaOrdenacaoSorter();
            foreach (GISA.Controls.ListViewOrderedColumns.ColumnSortOrderInfo col in mListaColunasOrdenadas) 
			{
				mListaColunas.Remove(col.column);
				AddItemOrdenacao(col);
			}
			
			lstVwColunas.ListViewItemSorter = new ListaColunasSorter();
            List<ColumnHeader> lstColunas = new List<ColumnHeader>();
			lstColunas = mListaColunas;
			foreach (ColumnHeader col in lstColunas)
			{
				AddItemColunas(col);
			}
			lstVwColunas.Sort();

			UpdateButtons();
			UpdateOrdenacaoControls();
		}

		private void UpdateButtons()
		{
			if (lstVwColunas.SelectedItems.Count > 0)
				btnAdd.Enabled = true;
			else
				btnAdd.Enabled = false;
			
            if (lstVwOrdenacao.SelectedItems.Count > 0)
				btnRemove.Enabled = true;
			else
				btnRemove.Enabled = false;
			
            if (lstVwColunas.Items.Count > 0)
				btnAddAll.Enabled = true;
			else
				btnAddAll.Enabled = false;
			
            if (lstVwOrdenacao.Items.Count > 0)
				btnRemoveAll.Enabled = true;
			else
				btnRemoveAll.Enabled = false;
		}

		private void UpdateOrdenacaoControls()
		{
			if (lstVwOrdenacao.SelectedItems.Count > 0) 
			{
				numOrder.Enabled = true;
				numOrder.Maximum = lstVwOrdenacao.Items.Count;
				domainSort.Enabled = true;
			} 
			else 
			{
				numOrder.Enabled = false;
				domainSort.Enabled = false;
			}
		}
		
		private void AddItemColunas(ColumnHeader col)
		{
			ListViewItem item = new ListViewItem();
			item.SubItems.AddRange(new string[]{string.Empty});
			item.SubItems[columnHeader2.Index].Text = col.Text;			
			item.Tag = col;
			lstVwColunas.Items.Add(item);
		}

		private void AddItemOrdenacao(ListViewOrderedColumns.ColumnSortOrderInfo col)
		{
			AddItemOrdenacao(col.column, col.columnSortOrder);
		}

		private void AddItemOrdenacao(ColumnHeader col, ListViewOrderedColumns.MySortOrder sort)
		{
			ListViewItem item = new ListViewItem();
			item.SubItems.AddRange(new string[]{string.Empty, string.Empty, string.Empty});			
			item.SubItems[columnHeader2.Index].Text = (lstVwOrdenacao.Items.Count + 1).ToString();
			item.SubItems[columnHeader3.Index].Text = col.Text;
			item.SubItems[columnHeader4.Index].Text = sort.ToString(); 
			item.Tag = col;
			lstVwOrdenacao.Items.Add(item);			
		}

		#region Controls Events
		private void lstVwColunas_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UpdateButtons();
		}

		private void lstVwOrdenacao_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.numOrder.ValueChanged -= new EventHandler(numOrder_ValueChanged);
			if (lstVwOrdenacao.SelectedItems.Count > 0) 
			{
				ListViewItem item = lstVwOrdenacao.SelectedItems[0];
				// popular os valores de ordenação da coluna seleccionada
                domainSort.SelectedIndex = (int)Enum.Parse(typeof(ListViewOrderedColumns.MySortOrder), item.SubItems[columnHeader4.Index].Text);
				numOrder.Value = System.Convert.ToDecimal(item.SubItems[columnHeader2.Index].Text);
			} 
			else 
			{	
				numOrder.Value = 1;
				domainSort.SelectedIndex = 0;
			}
			UpdateButtons();
			UpdateOrdenacaoControls();
			this.numOrder.ValueChanged += new EventHandler(numOrder_ValueChanged);
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			ColumnHeader colToBeAdded;
			colToBeAdded = ((ColumnHeader)(lstVwColunas.SelectedItems[0].Tag));
			if (!HasColumn(lstVwOrdenacao, colToBeAdded)) 
			{
                AddItemOrdenacao(colToBeAdded, ListViewOrderedColumns.MySortOrder.Ascendente);
			} 
			else 
			{
				// mensagem de erro... mas esta situação não deverá ocorrer
			}
			lstVwColunas.Items.Remove(lstVwColunas.SelectedItems[0]);
			lstVwOrdenacao.Sort();
			UpdateOrdenacaoControls();
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			ColumnHeader colToBeAdded;
			colToBeAdded = ((ColumnHeader)(lstVwOrdenacao.SelectedItems[0].Tag));
			if (!HasColumn(lstVwColunas, colToBeAdded)) 
			{
				AddItemColunas(colToBeAdded);
			} 
			else 
			{
				// mensagem de erro... mas esta situação não deverá ocorrer
			}
			lstVwOrdenacao.Items.Remove(lstVwOrdenacao.SelectedItems[0]);
			lstVwOrdenacao.Sort();
			RefreshOrdingNumbers();
			UpdateOrdenacaoControls();
		}

		private void btnAddAll_Click(object sender, System.EventArgs e)
		{
			foreach (ListViewItem item in lstVwColunas.Items) 
			{
				ColumnHeader colToBeAdded;
				colToBeAdded = ((ColumnHeader)(item.Tag));
				if (!HasColumn(lstVwOrdenacao, colToBeAdded)) 
				{
                    AddItemOrdenacao(colToBeAdded, ListViewOrderedColumns.MySortOrder.Ascendente);
				} 
				else 
				{
					// mensagem de erro... mas esta situação não deverá ocorrer
				}
			}
			lstVwColunas.Items.Clear();
			lstVwOrdenacao.Sort();
			UpdateButtons();
			UpdateOrdenacaoControls();
		}

		private void btnRemoveAll_Click(object sender, System.EventArgs e)
		{
			foreach (ListViewItem item in lstVwOrdenacao.Items) 
			{
				ColumnHeader colToBeAdded;
				colToBeAdded = ((ColumnHeader)(item.Tag));
				if (!HasColumn(lstVwColunas, colToBeAdded)) 
				{
					AddItemColunas(colToBeAdded);
				} 
				else 
				{
					// mensagem de erro... mas esta situação não deverá ocorrer
				}
			}
			lstVwOrdenacao.Items.Clear();
			lstVwColunas.Sort();
			UpdateButtons();
			UpdateOrdenacaoControls();
		}

		private void domainSort_Click(object sender, System.EventArgs e)
		{
			// actualizar a informação da linha
			ListViewItem item = lstVwOrdenacao.SelectedItems[0];
            item.SubItems[columnHeader4.Index].Text = ((ListViewOrderedColumns.MySortOrder)domainSort.SelectedIndex).ToString();
		}

		private void numOrder_ValueChanged(object sender, EventArgs e)
		{
			// actualizar a informação da linha e reordenar a lista
			int numOrderValue = System.Convert.ToInt32(numOrder.Value);
			ListViewItem item1 = lstVwOrdenacao.SelectedItems[0];
			ListViewItem item2 = lstVwOrdenacao.Items[numOrderValue - 1];
			item2.SubItems[columnHeader2.Index].Text = item1.SubItems[columnHeader2.Index].Text;			
			item1.SubItems[columnHeader2.Index].Text = numOrderValue.ToString();
			lstVwOrdenacao.Sort();
		}
		#endregion		

		private bool HasColumn(ListView lstView, ColumnHeader column)
		{
			foreach (ListViewItem item in lstView.Items) 
			{
				if (item.Tag == column) 
				{
					return true;
				}
			}
			return false;
		}

		private void RefreshOrdingNumbers()
		{
			for (int i = 0; i < lstVwOrdenacao.Items.Count; i++)
			{
				lstVwOrdenacao.Items[i].SubItems[columnHeader2.Index].Text = (i + 1).ToString();
			}
		}

		#region Sorter Classes
		public class ListaColunasSorter : IComparer
		{
			public int Compare(object obj1, object obj2)
			{
				if ( ((ColumnHeader)((ListViewItem)obj1).Tag).Index > ((ColumnHeader)((ListViewItem)obj2).Tag).Index)
				{
					return 1;
				} 
				else if ( ((ColumnHeader)((ListViewItem)obj1).Tag).Index < ((ColumnHeader)((ListViewItem)obj2).Tag).Index)
				{
					return -1;
				}
				return 0;
			}
		}

		public class ListaOrdenacaoSorter : IComparer
		{
			public int Compare(object obj1, object obj2)
			{
				int val1 = System.Convert.ToInt32(((ListViewItem)obj1).SubItems[0].Text);
				int val2 = System.Convert.ToInt32(((ListViewItem)obj2).SubItems[0].Text);				
				if (val1 > val2)
				{
					return 1;
				} 
				else if (val1 < val2)
				{
					return -1;
				}
				return 0;
			}
		}
		#endregion		
	}
}