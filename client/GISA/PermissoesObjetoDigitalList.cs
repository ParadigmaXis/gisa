using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

using GISA.Controls;

namespace GISA
{
    public partial class PermissoesObjetoDigitalList
#if DEBUG
 : MiddleClass
#else  
 : PaginatedListView 
#endif
    {
        public GISADataset.TrusteeRow CurrentTrusteeRow = null;
        public GISADataset.NivelRow CurrentNivelRow = null;
        private Dictionary<string, byte> currentNivelPerms = null;
        private PaginatedLVGetItems returnedInfo;

        public PermissoesObjetoDigitalList()
        {
            InitializeComponent();

            lstVwPaginated.ItemSubItemClick += lstVwPaginated_ItemSubItemClick;
            lstVwPaginated.ContextFormEvent += listView_ContextFormEvent;
            lstVwPaginated.CustomizedSorting = true;
            lstVwPaginated.MultiSelect = true;
            lstVwPaginated.ReturnSubItemIndex = true;
            lstVwPaginated.SmallImageList = new ImageList();

            AddListViewColumns();
        }

        // adicionar à listview as colunas correspondentes às operações possíveis sobre as funcionalidades da aplicação
        private void AddListViewColumns()
        {
            foreach (GISADataset.ObjetoDigitalTipoOperationRow odTipoOperation in GisaDataSetHelper.GetInstance().ObjetoDigitalTipoOperation.Select())
            {
                System.Windows.Forms.ColumnHeader col = new System.Windows.Forms.ColumnHeader();
                col.Text = odTipoOperation.TipoOperationRow.Name;
                col.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                col.Width = 84;
                col.Name = "ch" + odTipoOperation.TipoOperationRow.Name;
                lstVwPaginated.Columns.Add(col);
            }
        }

        protected override void GetExtraResources()
        {
            base.GetExtraResources();
            lstVwPaginated.SmallImageList = TipoNivelRelacionado.GetImageList();
        }

        private long LoginTrusteeID
        {
            get { return SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID; }
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
            ArrayList ordenacao = this.GetListSortDef();
            TrusteeRule.Current.CalculateODOrderedItems(CurrentNivelID, CurrentTrusteeID, LoginTrusteeID, ordenacao, connection);
        }

        protected override void GetItems(int pageNr, int itemsPerPage, IDbConnection connection)
        {
            ArrayList rowIds = new ArrayList();
            rowIds = TrusteeRule.Current.GetODItems(GisaDataSetHelper.GetInstance(), pageNr, itemsPerPage, CurrentTrusteeID, connection);
            var nivelPerms = PermissoesRule.Current.CalculateEffectivePermissions(new List<long>() { CurrentNivelID }, CurrentTrusteeID, connection);
            currentNivelPerms = nivelPerms[CurrentNivelID];
            PaginatedLVGetItemsPN items = new PaginatedLVGetItemsPN(rowIds, CurrentTrusteeID);
            returnedInfo = items;
        }

        protected override void DeleteTemporaryResults(IDbConnection connection)
        {
            TrusteeRule.Current.DeleteODTemporaryResults(connection);
        }

        protected override void AddItemsToList()
        {
            List<ListViewItem> itemsToBeAdded = new List<ListViewItem>();
            PaginatedLVGetItemsPN rInfo = (PaginatedLVGetItemsPN)returnedInfo;
            if (rInfo.rowsInfo != null)
            {
                foreach (PermissoesRule.ObjDig row in rInfo.rowsInfo)
                {
                    var rowID = row.ID;
                    GISADataset.ObjetoDigitalRow odRow = (GISADataset.ObjetoDigitalRow)(GisaDataSetHelper.GetInstance().ObjetoDigital.Select("ID=" + rowID.ToString())[0]);

                    ListViewItem item = new ListViewItem();
                    item.SubItems.AddRange(new string[] { string.Empty, string.Empty, string.Empty, string.Empty });
                    item.SubItems[this.colDesignacao.Index].Text = row.titulo;
                    item.SubItems[this.colIdentificador.Index].Text = row.pid;

                    foreach (GISADataset.ObjetoDigitalTipoOperationRow odtoRow in GisaDataSetHelper.GetInstance().ObjetoDigitalTipoOperation)
                    {
                        var tnpRow = GisaDataSetHelper.GetInstance().TrusteeObjetoDigitalPrivilege.Rows.Cast<GISADataset.TrusteeObjetoDigitalPrivilegeRow>()
                            .SingleOrDefault(r => r.IDTrustee == rInfo.trusteeID && r.IDObjetoDigital == odRow.ID && r.IDTipoOperation == odtoRow.IDTipoOperation);
                        PermissoesHelper.PermissionType permissaoEfectiva = PermissoesHelper.PermissionType.ImplicitDeny;
                        item.SubItems[this.GetColumnIndex(odtoRow.TipoOperationRow.Name)].Tag = odtoRow;

                        if (tnpRow != null)
                            permissaoEfectiva =
                                tnpRow.IsGrant ? PermissoesHelper.PermissionType.ExplicitGrant : PermissoesHelper.PermissionType.ExplicitDeny;
                        else
                        {
                            if (row.Permissoes.ContainsKey(odtoRow.IDTipoOperation))
                                permissaoEfectiva = row.Permissoes[odtoRow.IDTipoOperation] == 0 ?
                                    PermissoesHelper.PermissionType.ImplicitDeny : PermissoesHelper.PermissionType.ImplicitGrant;
                            else
                                permissaoEfectiva = !currentNivelPerms.ContainsKey(odtoRow.TipoOperationRow.Name) || currentNivelPerms[odtoRow.TipoOperationRow.Name] == 0 ?
                                    PermissoesHelper.PermissionType.ImplicitDeny : PermissoesHelper.PermissionType.ImplicitGrant;
                        }

                        string grant = string.Empty;
                        if (permissaoEfectiva == PermissoesHelper.PermissionType.ExplicitGrant ||
                            permissaoEfectiva == PermissoesHelper.PermissionType.ImplicitGrant)

                            grant = "Sim";
                        else if (permissaoEfectiva == PermissoesHelper.PermissionType.ExplicitDeny ||
                            permissaoEfectiva == PermissoesHelper.PermissionType.ImplicitDeny)

                            grant = "Não";

                        item.SubItems[this.GetColumnIndex(odtoRow.TipoOperationRow.Name)].Text = grant;

                        if (permissaoEfectiva == PermissoesHelper.PermissionType.ImplicitGrant ||
                            permissaoEfectiva == PermissoesHelper.PermissionType.ImplicitDeny)

                            item.SubItems[this.GetColumnIndex(odtoRow.TipoOperationRow.Name)].Font = PermissoesHelper.fontItalic;
                    }

                    item.Tag = odRow;
                    item.UseItemStyleForSubItems = false;
                    itemsToBeAdded.Add(item);
                }

                if (itemsToBeAdded.Count > 0)
                    this.lstVwPaginated.Items.AddRange(itemsToBeAdded.ToArray());
            }
        }

        private void lstVwPaginated_ItemSubItemClick(object sender, ItemSubItemClickEventArgs e)
        {
            if (e.SubItemIndex < 0 || e.ItemIndex < 0)
                return;

            ListViewItem item = lstVwPaginated.Items[e.ItemIndex];
            if (lstVwPaginated.SelectedItems.Contains(item) && (e.SubItemIndex > 0) && e.MouseEvent == PxListView.MouseEventsTypes.MouseDown && item.SubItems[e.SubItemIndex].Tag != null)
                PermissoesHelper.ChangeODPermission(CurrentTrusteeRow, CurrentNivelRow, item, e.SubItemIndex);
            else
            {
                if ((e.SubItemIndex > 0) && e.MouseEvent == PxListView.MouseEventsTypes.MouseMove && item.SubItems[e.SubItemIndex].Tag != null)
                    this.lstVwPaginated.Cursor = Cursors.Hand;
                else
                    this.lstVwPaginated.Cursor = Cursors.Default;
            }
        }

        private void listView_ContextFormEvent(object sender, EventArgs e)
        {
            if (this.lstVwPaginated.SelectedItems.Count == 0) return;

            var frm = new FormObjDigitalChangePermissions();
            var res = frm.ShowDialog();
            if (res == DialogResult.OK)
            {
                foreach (ListViewItem item in this.lstVwPaginated.SelectedItems)
                {
                    var odRow = item.Tag as GISADataset.ObjetoDigitalRow;
                    UpdatePermission(CurrentTrusteeRow, odRow, item, GetColumnIndex(PermissoesHelper.ObjDigOpREAD.TipoOperationRow.Name), PermissoesHelper.ObjDigOpREAD, frm.Ler);
                    UpdatePermission(CurrentTrusteeRow, odRow, item, GetColumnIndex(PermissoesHelper.ObjDigOpWRITE.TipoOperationRow.Name), PermissoesHelper.ObjDigOpWRITE, frm.Escrever);
                }
            }
        }

        private void UpdatePermission(GISADataset.TrusteeRow user, GISADataset.ObjetoDigitalRow odRow, ListViewItem item, int colIndex, GISADataset.ObjetoDigitalTipoOperationRow opRow, string permValue)
        {
            var todpRow = GisaDataSetHelper.GetInstance().TrusteeObjetoDigitalPrivilege.Cast<GISADataset.TrusteeObjetoDigitalPrivilegeRow>()
                .SingleOrDefault(r => r.RowState != DataRowState.Deleted && r.IDObjetoDigital == odRow.ID && r.IDTipoOperation == opRow.IDTipoOperation && r.IDTrustee == user.ID);

            if (todpRow == null)
            {
                todpRow = GisaDataSetHelper.GetInstance().TrusteeObjetoDigitalPrivilege.Cast<GISADataset.TrusteeObjetoDigitalPrivilegeRow>()
                    .SingleOrDefault(r => r.RowState == DataRowState.Deleted && (long)r["IDObjetoDigital", DataRowVersion.Original] == odRow.ID && (byte)r["IDTipoOperation", DataRowVersion.Original] == opRow.IDTipoOperation && (long)r["IDTrustee", DataRowVersion.Original] == user.ID);

                if (todpRow != null) todpRow.RejectChanges();
                else todpRow = GisaDataSetHelper.GetInstance().TrusteeObjetoDigitalPrivilege.AddTrusteeObjetoDigitalPrivilegeRow(user, odRow, opRow, true, new byte[] { }, 0);
            }

            if (permValue == null || permValue == string.Empty)
                todpRow.Delete();
            else if (permValue.Equals("Sim"))
                todpRow.IsGrant = true;
            else if (permValue.Equals("Não"))
                todpRow.IsGrant = false;

            // popular as alterações
            PermissoesHelper.PermissionType permissaoEfectiva = PermissoesHelper.CalculateEffectivePermissions(odRow, CurrentTrusteeRow, CurrentNivelRow, opRow.TipoOperationRow);
            PermissoesHelper.PopulatePermission(item, colIndex, todpRow, permissaoEfectiva);
        }

        private int GetColumnIndex(string colName)
        {
            foreach (ColumnHeader col in lstVwPaginated.Columns)
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
            if (!(item == lstVwPaginated.SelectedItems[0]))
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
