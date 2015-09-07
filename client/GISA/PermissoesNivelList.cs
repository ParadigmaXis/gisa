using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

using GISA.Controls;

namespace GISA
{
	public class PermissoesNivelList
#if DEBUG
 : MiddleClass
#else  
 : PaginatedListView 
#endif
    {
        private PaginatedLVGetItems returnedInfo;
		public GISADataset.TrusteeRow CurrentTrusteeRow = null;
        public GISADataset.NivelRow CurrentNivelRow = null;

        private TableLayoutPanel tableLayoutPanel1;
        private Label lblFiltro;
        private ComboBox cbFiltro;

        public delegate void Reload();
        private Reload theReload;
        public Reload TheReload
        {
            get { return this.theReload; }
            set { this.theReload = value; }
        }

	#region  Windows Form Designer generated code 

		public PermissoesNivelList() : base()
		{
			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            txtFiltroDesignacao.KeyDown += new KeyEventHandler(this.txtBox_KeyDown);
            lstVwPaginated.ItemSubItemClick += lstVwPaginated_ItemSubItemClick;
            lstVwPaginated.ContextFormEvent += listView_ContextFormEvent;
            lstVwPaginated.CustomizedSorting = false;
            lstVwPaginated.MultiSelect = true;

			lstVwPaginated.ReturnSubItemIndex = true;
			AddListViewColumns();
		}

		//UserControl overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}





		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
        internal System.Windows.Forms.TextBox txtFiltroDesignacao;
		internal System.Windows.Forms.Label lblFiltroDesignacao;
		internal System.Windows.Forms.ColumnHeader colDesignacao;
		internal GISA.Controls.PxComboBox cbTipoNivelRelacionado;
		internal System.Windows.Forms.Label lblTipoNivelRelacionado;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PermissoesNivelList));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblFiltroDesignacao = new System.Windows.Forms.Label();
            this.txtFiltroDesignacao = new System.Windows.Forms.TextBox();
            this.cbTipoNivelRelacionado = new GISA.Controls.PxComboBox();
            this.lblTipoNivelRelacionado = new System.Windows.Forms.Label();
            this.lblFiltro = new System.Windows.Forms.Label();
            this.cbFiltro = new System.Windows.Forms.ComboBox();
            this.colDesignacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpResultados.SuspendLayout();
            this.grpFiltro.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpResultados
            // 
            this.grpResultados.Location = new System.Drawing.Point(6, 75);
            this.grpResultados.Size = new System.Drawing.Size(620, 263);
            // 
            // txtNroPagina
            // 
            this.txtNroPagina.Location = new System.Drawing.Point(590, 61);
            // 
            // lstVwPaginated
            // 
            this.lstVwPaginated.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDesignacao});
            this.lstVwPaginated.Size = new System.Drawing.Size(576, 239);
            // 
            // btnProximo
            // 
            this.btnProximo.Location = new System.Drawing.Point(590, 89);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Location = new System.Drawing.Point(590, 29);
            // 
            // grpFiltro
            // 
            this.grpFiltro.Controls.Add(this.tableLayoutPanel1);
            this.grpFiltro.Size = new System.Drawing.Size(620, 69);
            this.grpFiltro.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.grpFiltro.Controls.SetChildIndex(this.btnAplicar, 0);
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(550, 40);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 273F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 114F));
            this.tableLayoutPanel1.Controls.Add(this.lblFiltroDesignacao, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtFiltroDesignacao, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbTipoNivelRelacionado, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTipoNivelRelacionado, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblFiltro, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbFiltro, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(536, 45);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // lblFiltroDesignacao
            // 
            this.lblFiltroDesignacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFiltroDesignacao.Location = new System.Drawing.Point(3, 0);
            this.lblFiltroDesignacao.Name = "lblFiltroDesignacao";
            this.lblFiltroDesignacao.Size = new System.Drawing.Size(143, 18);
            this.lblFiltroDesignacao.TabIndex = 0;
            this.lblFiltroDesignacao.Text = "Título";
            // 
            // txtFiltroDesignacao
            // 
            this.txtFiltroDesignacao.AcceptsReturn = true;
            this.txtFiltroDesignacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFiltroDesignacao.Location = new System.Drawing.Point(3, 21);
            this.txtFiltroDesignacao.Name = "txtFiltroDesignacao";
            this.txtFiltroDesignacao.Size = new System.Drawing.Size(143, 20);
            this.txtFiltroDesignacao.TabIndex = 1;
            // 
            // cbTipoNivelRelacionado
            // 
            this.cbTipoNivelRelacionado.BackColor = System.Drawing.SystemColors.Window;
            this.cbTipoNivelRelacionado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbTipoNivelRelacionado.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbTipoNivelRelacionado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoNivelRelacionado.DropDownWidth = 230;
            this.cbTipoNivelRelacionado.ImageIndexes = ((System.Collections.ArrayList)(resources.GetObject("cbTipoNivelRelacionado.ImageIndexes")));
            this.cbTipoNivelRelacionado.Location = new System.Drawing.Point(152, 21);
            this.cbTipoNivelRelacionado.MaxDropDownItems = 9;
            this.cbTipoNivelRelacionado.Name = "cbTipoNivelRelacionado";
            this.cbTipoNivelRelacionado.Size = new System.Drawing.Size(267, 21);
            this.cbTipoNivelRelacionado.TabIndex = 2;
            // 
            // lblTipoNivelRelacionado
            // 
            this.lblTipoNivelRelacionado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTipoNivelRelacionado.Location = new System.Drawing.Point(152, 0);
            this.lblTipoNivelRelacionado.Name = "lblTipoNivelRelacionado";
            this.lblTipoNivelRelacionado.Size = new System.Drawing.Size(267, 18);
            this.lblTipoNivelRelacionado.TabIndex = 2;
            this.lblTipoNivelRelacionado.Text = "Nível de Descrição";
            // 
            // lblFiltro
            // 
            this.lblFiltro.AutoSize = true;
            this.lblFiltro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFiltro.Location = new System.Drawing.Point(425, 0);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(108, 18);
            this.lblFiltro.TabIndex = 7;
            this.lblFiltro.Text = "Filtro";
            // 
            // cbFiltro
            // 
            this.cbFiltro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFiltro.FormattingEnabled = true;
            this.cbFiltro.Location = new System.Drawing.Point(425, 21);
            this.cbFiltro.Name = "cbFiltro";
            this.cbFiltro.Size = new System.Drawing.Size(108, 21);
            this.cbFiltro.TabIndex = 8;
            // 
            // colDesignacao
            // 
            this.colDesignacao.Text = "Título";
            this.colDesignacao.Width = 231;
            // 
            // PermissoesNivelList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.FilterVisible = true;
            this.Name = "PermissoesNivelList";
            this.Size = new System.Drawing.Size(632, 344);
            this.grpResultados.ResumeLayout(false);
            this.grpFiltro.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

		}

	#endregion

		// adicionar à listview as colunas correspondentes às operações possíveis sobre as funcionalidades da aplicação
		private void AddListViewColumns()
		{
			foreach (GISADataset.NivelTipoOperationRow nivelTipoOperation in GisaDataSetHelper.GetInstance().NivelTipoOperation.Select())
			{
				System.Windows.Forms.ColumnHeader col = new System.Windows.Forms.ColumnHeader();
				col.Text = nivelTipoOperation.TipoOperationRow.Name;
				col.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
				col.Width = 84;
                col.Name = "ch" + nivelTipoOperation.TipoOperationRow.Name;
				lstVwPaginated.Columns.Add(col);
			}
		}

		protected override void GetExtraResources()
		{
            base.GetExtraResources();
			lstVwPaginated.SmallImageList = TipoNivelRelacionado.GetImageList();
		}

        private string FiltroDesignacaoLike
		{
			get
			{
                return txtFiltroDesignacao.Text.Length == 0 ? string.Empty :
                    DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.buildLikeStatement("Designacao", string.Format("'{0}'", DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.sanitizeSearchTerm(txtFiltroDesignacao.Text)));
			}
		}

        private long FilterTipoNivelRelacionado
		{
			get
			{
				if (cbTipoNivelRelacionado.SelectedIndex == 0)
					return -1;
				else
					return (long)cbTipoNivelRelacionado.SelectedValue;
			}
		}

        private int FilterTipoOption
        {
            get {return cbFiltro.SelectedIndex;}
        }

        private long LoginTrusteeID
        {
            get {return SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID;}
        }

        private long CurrentTrusteeID
		{
			get
			{
				if (CurrentTrusteeRow == null || CurrentTrusteeRow.RowState == DataRowState.Detached)
					return -1;
				else
					return CurrentTrusteeRow.ID;
			}
		}

        private long CurrentNivelID
        {
            get
            {
                if (CurrentNivelRow == null || CurrentNivelRow.RowState == DataRowState.Detached)
                    return -1;
                else
                    return CurrentNivelRow.ID;
            }
        }

		protected override void CalculateOrderedItems(IDbConnection connection)
		{
            TrusteeRule.Current.CalculateOrderedItems(CurrentNivelID, CurrentTrusteeID, LoginTrusteeID, FiltroDesignacaoLike, FilterTipoNivelRelacionado, FilterTipoOption, connection);
		}

        protected override void GetItems(int pageNr, int itemsPerPage, IDbConnection connection)
		{
            ArrayList rowIds = new ArrayList();
            rowIds = TrusteeRule.Current.GetItems(GisaDataSetHelper.GetInstance(), pageNr, itemsPerPage, CurrentTrusteeID, connection);
            PaginatedLVGetItemsPN items = new PaginatedLVGetItemsPN(rowIds, CurrentTrusteeID);
            returnedInfo = items;
		}

        protected override void DeleteTemporaryResults(IDbConnection connection)
		{
            TrusteeRule.Current.DeleteTemporaryResults(connection);
		}

        protected override void AddItemsToList()
		{
            List<ListViewItem> itemsToBeAdded = new List<ListViewItem>();
            PaginatedLVGetItemsPN rInfo = (PaginatedLVGetItemsPN)returnedInfo;
            if (rInfo.rowsInfo != null)
            {
                foreach (TrusteeRule.Nivel row in rInfo.rowsInfo)
                {
                    var rowID = row.ID;
                    GISADataset.NivelRow nRow = (GISADataset.NivelRow)(GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + rowID.ToString())[0]);

                    var tnpRow = GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.Rows.Cast<GISADataset.TrusteeNivelPrivilegeRow>()
                        .SingleOrDefault(r => r.IDTrustee == rInfo.trusteeID && r.IDNivel == nRow.ID);

                    ListViewItem item = new ListViewItem();
                    item.SubItems.AddRange(new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });

                    // pelo facto de os niveis estruturais poderem ter várias classificações, e se não se estiver a filtrar por tipo,
                    // então o ícone que ilustrará esse nível será o hierarquicamente superior, caso contrário, o ícone corresponderá
                    // ao tipo definido como filtro
                    if (FilterTipoNivelRelacionado == -1)
                    {
                        if (nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().Length == 0)
                            item.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageBase(System.Convert.ToInt32(TipoNivelRelacionado.GetTipoNivelRelacionadoFromRelacaoHierarquica(nRow, null).GUIOrder));
                        else
                            item.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageBase(System.Convert.ToInt32(nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].TipoNivelRelacionadoRow.GUIOrder));
                    }
                    else
                        item.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageBase(System.Convert.ToInt32(FilterTipoNivelRelacionado));

                    item.StateImageIndex = 0;
                    item.SubItems[this.colDesignacao.Index].Text = row.Designacao.ToString();

                    foreach (GISADataset.NivelTipoOperationRow ntoRow in GisaDataSetHelper.GetInstance().NivelTipoOperation)
                    {
                        PermissoesHelper.PermissionType permissaoEfectiva = PermissoesHelper.PermissionType.ImplicitDeny;


                        if ((tnpRow != null) && (tnpRow[ntoRow.TipoOperationRow.Name] != DBNull.Value))
                            permissaoEfectiva =
                                (byte)tnpRow[ntoRow.TipoOperationRow.Name] == 1 ? PermissoesHelper.PermissionType.ExplicitGrant : PermissoesHelper.PermissionType.ExplicitDeny;
                        else
                            permissaoEfectiva = !row.Permissoes.ContainsKey(ntoRow.TipoOperationRow.Name) || row.Permissoes[ntoRow.TipoOperationRow.Name] == 0 ?
                                PermissoesHelper.PermissionType.ImplicitDeny : PermissoesHelper.PermissionType.ImplicitGrant;

                        string grant = string.Empty;
                        if (permissaoEfectiva == PermissoesHelper.PermissionType.ExplicitGrant ||
                            permissaoEfectiva == PermissoesHelper.PermissionType.ImplicitGrant)

                            grant = "Sim";
                        else if (permissaoEfectiva == PermissoesHelper.PermissionType.ExplicitDeny ||
                            permissaoEfectiva == PermissoesHelper.PermissionType.ImplicitDeny)

                            grant = "Não";

                        item.SubItems[this.GetColumnIndex(ntoRow.TipoOperationRow.Name)].Text = grant;

                        if (permissaoEfectiva == PermissoesHelper.PermissionType.ImplicitGrant ||
                            permissaoEfectiva == PermissoesHelper.PermissionType.ImplicitDeny)

                            item.SubItems[this.GetColumnIndex(ntoRow.TipoOperationRow.Name)].Font = PermissoesHelper.fontItalic;
                    }

                    item.Tag = nRow;
                    item.UseItemStyleForSubItems = false;
                    itemsToBeAdded.Add(item);
                }

                if (itemsToBeAdded.Count > 0)
                    this.lstVwPaginated.Items.AddRange(itemsToBeAdded.ToArray());
            }
		}

        private void lstVwPaginated_ItemSubItemClick(object sender, ItemSubItemClickEventArgs e)
		{
            this.lstVwPaginated.Cursor = Cursors.Default;

            if (e.ItemIndex < 0)
				return;

            ListViewItem item = lstVwPaginated.Items[e.ItemIndex];

            if (lstVwPaginated.SelectedItems.Contains(item) && PermissoesHelper.CanChangePermission(item, e.SubItemIndex) && e.MouseEvent == PxListView.MouseEventsTypes.MouseDown)
                PermissoesHelper.ChangePermission(CurrentTrusteeRow, (GISADataset.NivelRow)item.Tag, item, e.SubItemIndex);
            else
            {
                if (PermissoesHelper.CanChangePermission(item, e.SubItemIndex) && e.MouseEvent == PxListView.MouseEventsTypes.MouseMove)
                    this.lstVwPaginated.Cursor = Cursors.Hand;
                else
                    this.lstVwPaginated.Cursor = Cursors.Default;
            }
		}

        private void listView_ContextFormEvent(object sender, EventArgs e)
        {
            if (this.lstVwPaginated.SelectedItems.Count == 0) return;

            GISADataset.NivelRow nRow;
            FormChangePermissions frm = new FormChangePermissions();
            DialogResult res = frm.ShowDialog();
            if (res == DialogResult.OK)
            {
                foreach (ListViewItem item in this.lstVwPaginated.SelectedItems)
                {
                    nRow = (GISADataset.NivelRow)item.Tag;
                    PermissoesHelper.ChangeToPermission(CurrentTrusteeRow, nRow, item, item.ListView.Columns["chCriar"].Index, frm.Criar);
                    PermissoesHelper.ChangeToPermission(CurrentTrusteeRow, nRow, item, item.ListView.Columns["chLer"].Index, frm.Ler);
                    PermissoesHelper.ChangeToPermission(CurrentTrusteeRow, nRow, item, item.ListView.Columns["chEscrever"].Index, frm.Escrever);
                    PermissoesHelper.ChangeToPermission(CurrentTrusteeRow, nRow, item, item.ListView.Columns["chApagar"].Index, frm.Apagar);
                    PermissoesHelper.ChangeToPermission(CurrentTrusteeRow, nRow, item, item.ListView.Columns["chExpandir"].Index, frm.Expandir);
                }
            }
        }

		private void btnAplicar_Click(object Sender, EventArgs e)
		{
            this.TheReload();
			this.ReloadList();
		}

		public void PopulateTipoFiltro()
		{
			GISADataset.TipoNivelRelacionadoRow emptyRow = GisaDataSetHelper.GetInstance().TipoNivelRelacionado.NewTipoNivelRelacionadoRow();
			emptyRow.ID = 0;
			emptyRow.Designacao = "<Todos>";
			ArrayList ds = new ArrayList();
			ds.Add(emptyRow);
			ds.AddRange(GisaDataSetHelper.GetInstance().TipoNivelRelacionado.Select("ID < 11"));
			cbTipoNivelRelacionado.DataSource = ds;
			cbTipoNivelRelacionado.DisplayMember = "Designacao";
			cbTipoNivelRelacionado.ValueMember = "ID";
            cbTipoNivelRelacionado.SelectedIndex = 0;

			cbTipoNivelRelacionado.ImageList = TipoNivelRelacionado.GetImageList();
			cbTipoNivelRelacionado.ImageIndexes.Clear();
			cbTipoNivelRelacionado.ImageIndexes.Add(-1);

			foreach (GISADataset.TipoNivelRelacionadoRow tnrRow in GisaDataSetHelper.GetInstance().TipoNivelRelacionado.Select("ID < 11"))
				cbTipoNivelRelacionado.ImageIndexes.Add(SharedResourcesOld.CurrentSharedResources.NivelImageBase(System.Convert.ToInt32(tnrRow.GUIOrder)));
		}

        public void PopulateFiltro()
        {
            DataTable dTable = new DataTable();

            dTable.Columns.Add(new DataColumn("ID", typeof(int)));
            dTable.Columns.Add(new DataColumn("Designacao", typeof(string)));
            dTable.Rows.Add(new object[] { 0, "Próprio" });
            dTable.Rows.Add(new object[] { 1, "Todos documentais" });
            dTable.Rows.Add(new object[] { 2, "Todos" });            

            cbFiltro.DataSource = dTable;
            cbFiltro.DisplayMember = "Designacao";
            cbFiltro.ValueMember = "ID";
            cbFiltro.SelectedIndex = (int)TrusteeRule.DocsFilter.Proprio;
        }

		private int GetColumnIndex(string colName)
		{
			foreach (ColumnHeader col in this.lstVwPaginated.Columns)
			{
				if (col.Text.Equals(colName))
					return col.Index;
			}

			throw new Exception("Column not found!");
		}

		private int GetColumnIndex(int posX, int posY)
		{
			if (lstVwPaginated.SelectedItems.Count == 0)
				return -1;

			ListViewItem item = lstVwPaginated.GetItemAt(posX, posY);
			if (! (item == lstVwPaginated.SelectedItems[0]))
				return -1;

			if (item.SubItems.Count == 1)
				return -1;

			int width = 0;
			foreach (ColumnHeader col in lstVwPaginated.Columns)
			{
				width += col.Width;
				if (width > posX)
				{
					return col.Index;
				}
			}

			return -1;
		}	
	}
}