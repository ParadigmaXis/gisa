using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Controls;
using GISA.Model;
using GISA.SharedResources;

namespace GISA
{
    public partial class SlavePanelPermissoesDesposito : GISA.SinglePanel
    {
        private GISADataset.TrusteeRow CurrentTrustee;

        public SlavePanelPermissoesDesposito()
        {
            InitializeComponent();

            lstvwPermissoes.ItemSubItemClick += PxListView_ItemSubItemClick;

            lstvwPermissoes.ReturnSubItemIndex = true;
            AddListViewColumns();
        }

        public static Bitmap FunctionImage
        {
            get { return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "PermissoesDepositos_32x32.png"); }
        }

        private void AddListViewColumns()
        {
            var operations = GisaDataSetHelper.GetInstance().DepositoTipoOperation.Cast<GISADataset.DepositoTipoOperationRow>().ToList();
            foreach (var tipoOperation in operations)
            {
                System.Windows.Forms.ColumnHeader col = new System.Windows.Forms.ColumnHeader();
                col.Text = tipoOperation.TipoOperationRow.Name;
                col.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                col.Width = 80 + tipoOperation.TipoOperationRow.Name.Length * 2;
                lstvwPermissoes.Columns.Add(col);
            }
        }

        public override void LoadData()
        {
            var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                CurrentTrustee = CurrentContext.Trustee;
                if (CurrentTrustee != null)
                    DepositoRule.Current.LoadDepositosPermissionsData(GisaDataSetHelper.GetInstance(), CurrentTrustee.ID, ho.Connection);
            }
            catch (Exception)
            {
                CurrentTrustee = null;
                return;
            }
            finally
            {
                ho.Dispose();
            }
        }

        public override void ModelToView()
        {
            PopulatePermissions();
        }

        public override bool ViewToModel()
        {
            return false;
        }

        public override void Deactivate()
        {
            GUIHelper.GUIHelper.clearField(lstvwPermissoes);
        }

        protected override bool isInnerContextValid()
        {
            return CurrentTrustee != null;
        }

        protected override bool isOuterContextValid()
        {
            return CurrentContext.Trustee != null;
        }

        protected override bool isOuterContextDeleted()
        {
            Debug.Assert(CurrentContext.Trustee != null, "CurrentContext.PermissoesDeposito Is Nothing");
            return CurrentContext.Trustee.RowState == DataRowState.Detached;
        }

        protected override void addContextChangeHandlers()
        {
            CurrentContext.TrusteeChanged += this.Recontextualize;
        }

        protected override void removeContextChangeHandlers()
        {
            CurrentContext.TrusteeChanged -= this.Recontextualize;
        }

        protected override PanelMensagem GetDeletedContextMessage()
        {
            PanelMensagem1.LblMensagem.Text = "Este utilizador foi removido não sendo por isso possível apresentá-lo.";
            return PanelMensagem1;
        }

        protected override PanelMensagem GetNoContextMessage()
        {
            PanelMensagem1.LblMensagem.Text = "Para visualizar os detalhes deverá selecionar um utilizador no painel superior.";
            return PanelMensagem1;
        }

        private void PopulatePermissions()
        {
            List<ListViewItem> itemsToBeAdded = new List<ListViewItem>();
            lstvwPermissoes.BeginUpdate();
            lstvwPermissoes.Items.Clear();

            GisaDataSetHelper.GetInstance().Deposito.Cast<GISADataset.DepositoRow>().ToList()
                .ForEach(depRow =>
                {
                    ListViewItem item = new ListViewItem();
                    item.UseItemStyleForSubItems = false;
                    item.SubItems[chDeposito.Index].Text = "     " + depRow.Designacao;
                    LoadOperationsAndPermissions(depRow, item);
                    item.Tag = depRow;
                    itemsToBeAdded.Add(item);
                }
            );

            lstvwPermissoes.Items.AddRange(itemsToBeAdded.ToArray());
            lstvwPermissoes.EndUpdate();
        }

        private void LoadOperationsAndPermissions(GISADataset.DepositoRow depRow, ListViewItem item)
        {
            Debug.Assert(CurrentTrustee != null);

            var operations = GisaDataSetHelper.GetInstance().DepositoTipoOperation.Cast<GISADataset.DepositoTipoOperationRow>().ToList();
            operations.ForEach(opRow =>
                {
                    item.SubItems.Add(string.Empty).Tag = opRow;                    
                    int colIndex = GetColumnIndex(opRow.TipoOperationRow.Name);
                    var permissaoEfectiva = PermissoesHelper.CalculateEffectivePermissions(CurrentTrustee, opRow.TipoOperationRow, depRow);
                    var tdpRow = GisaDataSetHelper.GetInstance().TrusteeDepositoPrivilege.Cast<GISADataset.TrusteeDepositoPrivilegeRow>()
                        .SingleOrDefault(r => r.IDDeposito == depRow.ID && r.IDTipoOperation == opRow.IDTipoOperation && r.IDTrustee == CurrentTrustee.ID);

                    //PermissoesHelper.PopulatePermission(item, colIndex, tdpRow, permissaoEfectiva);
                    if (permissaoEfectiva == PermissoesHelper.PermissionType.ExplicitGrant)
                        item.SubItems[colIndex].Text = "Sim";
                    else
                        item.SubItems[colIndex].Text = "Não";

                    if (GisaDataSetHelper.GetInstance().TrusteeDepositoPrivilege.Select(string.Format("IDTrustee={0} AND IDDeposito={1} AND IDTipoOperation={2}", CurrentTrustee.ID, depRow.ID, opRow.IDTipoOperation)).Length == 0)
                        item.SubItems[colIndex].Font = PermissoesHelper.fontItalic;
                });
        }

        private void PxListView_ItemSubItemClick(object sender, ItemSubItemClickEventArgs e)
        {
            if (e.SubItemIndex < 0 || e.ItemIndex < 0)
                return;

            ListViewItem item = lstvwPermissoes.Items[e.ItemIndex];
            if (lstvwPermissoes.SelectedItems.Contains(item) && (e.SubItemIndex > 0) && e.MouseEvent == PxListView.MouseEventsTypes.MouseDown)
                ChangePermission(CurrentTrustee, item, e.SubItemIndex);
            else
            {
                if ((e.SubItemIndex > 0) && e.MouseEvent == PxListView.MouseEventsTypes.MouseMove)
                    this.lstvwPermissoes.Cursor = Cursors.Hand;
                else
                    this.lstvwPermissoes.Cursor = Cursors.Default;
            }
        }

        private int GetColumnIndex(string colName)
        {
            foreach (ColumnHeader col in lstvwPermissoes.Columns)
            {
                if (col.Text.Equals(colName))
                    return col.Index;
            }
            throw new Exception("Column not found!");
        }

        private void ChangePermission(GISADataset.TrusteeRow user, ListViewItem item, int colIndex)
        {
            var depRow = item.Tag as GISADataset.DepositoRow;
            var opRow = item.SubItems[colIndex].Tag as GISADataset.DepositoTipoOperationRow;
            var tdpRow = GisaDataSetHelper.GetInstance().TrusteeDepositoPrivilege.Cast<GISADataset.TrusteeDepositoPrivilegeRow>()
                .SingleOrDefault(r => r.RowState != DataRowState.Deleted && r.IDDeposito == depRow.ID && r.IDTipoOperation == opRow.IDTipoOperation && r.IDTrustee == user.ID);

            if (tdpRow != null)
            {
                if (tdpRow.IsGrant)
                    tdpRow.IsGrant = false;
                else
                    tdpRow.Delete();
            }
            else
            {
                tdpRow = GisaDataSetHelper.GetInstance().TrusteeDepositoPrivilege.Cast<GISADataset.TrusteeDepositoPrivilegeRow>()
                    .SingleOrDefault(r => r.RowState == DataRowState.Deleted && (long)r["IDDeposito", DataRowVersion.Original] == depRow.ID && (byte)r["IDTipoOperation", DataRowVersion.Original] == opRow.IDTipoOperation && (long)r["IDTrustee", DataRowVersion.Original] == user.ID);

                if (tdpRow != null)
                {
                    tdpRow.RejectChanges();
                    tdpRow.IsGrant = true;
                }
                else
                    tdpRow = GisaDataSetHelper.GetInstance().TrusteeDepositoPrivilege.AddTrusteeDepositoPrivilegeRow(user, depRow, opRow, true, new byte[] { }, 0);
            }

            // popular as alterações
            PermissoesHelper.PermissionType permissaoEfectiva = PermissoesHelper.CalculateEffectivePermissions(CurrentTrustee, opRow.TipoOperationRow, depRow);

            //PermissoesHelper.PopulatePermission(item, colIndex, tdpRow, permissaoEfectiva);
            if (permissaoEfectiva == PermissoesHelper.PermissionType.ExplicitGrant)
                item.SubItems[colIndex].Text = "Sim";
            else
                item.SubItems[colIndex].Text = "Não";

            GISADataset.TrusteeDepositoPrivilegeRow[] tpRows = (GISADataset.TrusteeDepositoPrivilegeRow[])(GisaDataSetHelper.GetInstance().TrusteeDepositoPrivilege.Select(string.Format("IDTrustee={0} AND IDDeposito={1} AND IDTipoOperation={2}", user.ID, depRow.ID, opRow.IDTipoOperation)));

            if (tpRows.Length == 0)
            {
                tpRows = (GISADataset.TrusteeDepositoPrivilegeRow[])(GisaDataSetHelper.GetInstance().TrusteeDepositoPrivilege.Select(string.Format("IDTrustee={0} AND IDDeposito={1} AND IDTipoOperation={2}", user.ID, depRow.ID, opRow.IDTipoOperation), "", DataViewRowState.Deleted));

                if (tpRows.Length == 0)
                    item.SubItems[colIndex].Font = PermissoesHelper.fontItalic;
                else
                    item.SubItems[colIndex].Font = PermissoesHelper.fontBoldItalic;
            }
            else
            {
                if (tpRows[0].RowState == DataRowState.Modified && !(tpRows[0].IsGrant ^ (bool)(tpRows[0]["IsGrant", DataRowVersion.Original])))
                    item.SubItems[colIndex].Font = PermissoesHelper.fontRegular;
                else
                    item.SubItems[colIndex].Font = PermissoesHelper.fontBold;
            }
        }
    }
}
