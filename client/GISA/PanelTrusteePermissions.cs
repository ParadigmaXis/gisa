using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

using GISA.Controls;

namespace GISA
{
	public class PanelTrusteePermissions : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelTrusteePermissions() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            lstvwPermissoes.ItemSubItemClick += PxListView_ItemSubItemClick;

			lstvwPermissoes.ReturnSubItemIndex = true;
			AddListViewColumns();
		}

		//Form overrides dispose to clean up the component list.
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
		private System.ComponentModel.IContainer components = null;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		internal GISA.Controls.PxListView lstvwPermissoes;
		internal System.Windows.Forms.ColumnHeader colModulo;
		internal System.Windows.Forms.GroupBox GroupBox1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.lstvwPermissoes = new GISA.Controls.PxListView();
            this.colModulo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstvwPermissoes
            // 
            this.lstvwPermissoes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvwPermissoes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colModulo});
            this.lstvwPermissoes.CustomizedSorting = false;
            this.lstvwPermissoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstvwPermissoes.FullRowSelect = true;
            this.lstvwPermissoes.GridLines = true;
            this.lstvwPermissoes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstvwPermissoes.Location = new System.Drawing.Point(8, 16);
            this.lstvwPermissoes.MultiSelect = false;
            this.lstvwPermissoes.Name = "lstvwPermissoes";
            this.lstvwPermissoes.ReturnSubItemIndex = false;
            this.lstvwPermissoes.Size = new System.Drawing.Size(784, 576);
            this.lstvwPermissoes.TabIndex = 3;
            this.lstvwPermissoes.UseCompatibleStateImageBehavior = false;
            this.lstvwPermissoes.View = System.Windows.Forms.View.Details;
            // 
            // colModulo
            // 
            this.colModulo.Text = "Módulo";
            this.colModulo.Width = 258;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.Controls.Add(this.lstvwPermissoes);
            this.GroupBox1.Location = new System.Drawing.Point(0, 0);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(800, 600);
            this.GroupBox1.TabIndex = 4;
            this.GroupBox1.TabStop = false;
            // 
            // PanelTrusteePermissions
            // 
            this.Controls.Add(this.GroupBox1);
            this.Name = "PanelTrusteePermissions";
            this.GroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

		private void AddListViewColumns()
		{
			foreach (GISADataset.TipoOperationRow tipoOperation in GisaDataSetHelper.GetInstance().TipoOperation.Select())
			{
				if (tipoOperation.GetFunctionOperationRows().Length > 0)
				{
					System.Windows.Forms.ColumnHeader col = new System.Windows.Forms.ColumnHeader();
					col.Text = tipoOperation.Name;
					col.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
					col.Width = 80 + tipoOperation.Name.Length * 2;
					lstvwPermissoes.Columns.Add(col);
				}
			}
		}

		private void LoadGroupsAndFunctions()
		{
            List<ListViewItem> itemsToBeAdded = new List<ListViewItem>();
			lstvwPermissoes.BeginUpdate();
			lstvwPermissoes.Items.Clear();

            string modules = string.Empty;
            foreach(GISADataset.ModulesRow mr in SessionHelper.AppConfiguration.GetCurrentAppconfiguration().Modules)
            {
                modules += mr.ID + ",";
            }
            modules.TrimEnd(',');

            Dictionary<GISADataset.TipoFunctionRow, GISADataset.TipoFunctionRow> tfRows = new Dictionary<GISADataset.TipoFunctionRow, GISADataset.TipoFunctionRow>();
            foreach (GISADataset.ProductFunctionRow pfRow in GisaDataSetHelper.GetInstance().ProductFunction.Select(
                string.Format("IDTipoServer={0} AND IDModule IN ({1})",
                    SessionHelper.AppConfiguration.GetCurrentAppconfiguration().TipoServer.ID, modules)))
            {
                tfRows.Add(pfRow.TipoFunctionRowParent, pfRow.TipoFunctionRowParent);
            }


			foreach (GISADataset.TipoFunctionGroupRow tfg in GisaDataSetHelper.GetInstance().TipoFunctionGroup.Select("GuiOrder>0", "GuiOrder"))
			{
				foreach (GISADataset.TipoFunctionRow tf in tfg.GetTipoFunctionRows())
                {
                    if (tfRows.ContainsKey(tf))
                    {
                        ListViewItem fgItem = new ListViewItem();
                        fgItem.UseItemStyleForSubItems = false;
                        fgItem.SubItems[colModulo.Index].Text = "     " + tf.Name;
                        LoadOperationsAndPermissions(tf, fgItem);
                        fgItem.Tag = tf;
                        itemsToBeAdded.Add(fgItem);
                    }
                }

                if (itemsToBeAdded.Count > 0)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = tfg.Name;
                    item.Tag = tfg;
                    lstvwPermissoes.Items.Add(item);
                    lstvwPermissoes.Items.AddRange(itemsToBeAdded.ToArray());
                    itemsToBeAdded.Clear();
                }
			}
			lstvwPermissoes.EndUpdate();
		}

		private void LoadOperationsAndPermissions(GISADataset.TipoFunctionRow CurrentFunctionGroup, ListViewItem fgItem)
		{
			Debug.Assert(CurrentTrusteeRow != null);

			foreach (GISADataset.FunctionOperationRow fo in GisaDataSetHelper.GetInstance().FunctionOperation)
			{
				fgItem.SubItems.Add(string.Empty);
				if (CurrentFunctionGroup.IDTipoFunctionGroup == fo.IDTipoFunctionGroup && CurrentFunctionGroup.idx == fo.IdxTipoFunction)
				{
					int colIndex = GetColumnIndex(fo.TipoOperationRow.Name);

					PermissoesHelper.PermissionType permissaoEfectiva = PermissoesHelper.CalculateEffectivePermissions(CurrentTrusteeRow, fo);

					if (permissaoEfectiva == PermissoesHelper.PermissionType.ExplicitGrant)
						fgItem.SubItems[colIndex].Text = "Sim";
					else
						fgItem.SubItems[colIndex].Text = "Não";

					if (GisaDataSetHelper.GetInstance().TrusteePrivilege.Select(string.Format("IDTrustee={0} AND IDTipoFunctionGroup={1} AND IdxTipoFunction = {2} AND IDTipoOperation = {3}", CurrentTrusteeRow.ID, fo.IDTipoFunctionGroup, fo.IdxTipoFunction, fo.IDTipoOperation)).Length == 0)
						fgItem.SubItems[colIndex].Font = PermissoesHelper.fontItalic;
				}
			}
		}

		private GISADataset.TrusteeRow CurrentTrusteeRow;
		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			try
			{
				CurrentTrusteeRow = (GISADataset.TrusteeRow)CurrentDataRow;
				if (CurrentTrusteeRow != null)
					TrusteeRule.Current.LoadPanelTrusteePermissionsData(GisaDataSetHelper.GetInstance(), CurrentTrusteeRow.ID, conn);

			}
			catch (InvalidCastException)
			{
				CurrentTrusteeRow = null;
				return;
			}

			IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = false;
			LoadGroupsAndFunctions();
			IsPopulated = true;
		}

		public override void ViewToModel()
		{

		}

		public override void Deactivate()
		{
            GUIHelper.GUIHelper.clearField(lstvwPermissoes);
		}

		private void PxListView_ItemSubItemClick(object sender, ItemSubItemClickEventArgs e)
		{
			if (e.SubItemIndex < 0 || e.ItemIndex < 0)
				return;

			ListViewItem item = lstvwPermissoes.Items[e.ItemIndex];
			if (lstvwPermissoes.SelectedItems.Contains(item) && (e.SubItemIndex > 0) && CanChangePermission(item, e.SubItemIndex) && e.MouseEvent == PxListView.MouseEventsTypes.MouseDown)
                ChangePermission(CurrentTrusteeRow, item, e.SubItemIndex);
			else
			{
                if ((e.SubItemIndex > 0) && CanChangePermission(item, e.SubItemIndex) && e.MouseEvent == PxListView.MouseEventsTypes.MouseMove)
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
            GISADataset.TipoFunctionRow tipoFunction = (GISADataset.TipoFunctionRow)item.Tag;
            GISADataset.TipoOperationRow tipoOperation = (GISADataset.TipoOperationRow)(GisaDataSetHelper.GetInstance().TipoOperation.Select(string.Format("Name='{0}'", lstvwPermissoes.Columns[colIndex].Text))[0]);

            string query = "IDTrustee={0} AND IDTipoFunctionGroup={1} AND IdxTipoFunction = {2} AND IDTipoOperation = {3}";
            query = string.Format(query, user.ID, tipoFunction.IDTipoFunctionGroup, tipoFunction.idx, tipoOperation.ID);

            GISADataset.TrusteePrivilegeRow[] trusteePrivileges = null;
            trusteePrivileges = (GISADataset.TrusteePrivilegeRow[])(GisaDataSetHelper.GetInstance().TrusteePrivilege.Select(query));

            // alterar permissões no dataset
            if (trusteePrivileges.Length > 0)
            {
                if (trusteePrivileges[0].IsGrant)
                    trusteePrivileges[0].IsGrant = false;
                else
                    trusteePrivileges[0].Delete();
            }
            else
            {
                trusteePrivileges = (GISADataset.TrusteePrivilegeRow[])(GisaDataSetHelper.GetInstance().TrusteePrivilege.Select(query, "", DataViewRowState.Deleted));
                if (trusteePrivileges.Length > 0)
                {
                    trusteePrivileges[0].RejectChanges();
                    trusteePrivileges[0].IsGrant = true;
                }
                else
                    GisaDataSetHelper.GetInstance().TrusteePrivilege.AddTrusteePrivilegeRow(user, tipoFunction.IDTipoFunctionGroup, tipoFunction.idx, tipoOperation.ID, true, new byte[] { }, 0);
            }

            // popular as alterações
            GISADataset.FunctionOperationRow functionOperation = (GISADataset.FunctionOperationRow)(GisaDataSetHelper.GetInstance().FunctionOperation.Select(string.Format("IDTipoFunctionGroup={0} AND IdxTipoFunction={1} AND IDTipoOperation={2}", tipoFunction.IDTipoFunctionGroup, tipoFunction.idx, tipoOperation.ID))[0]);

            PermissoesHelper.PermissionType permissaoEfectiva = PermissoesHelper.CalculateEffectivePermissions(CurrentTrusteeRow, functionOperation);

            if (permissaoEfectiva == PermissoesHelper.PermissionType.ExplicitGrant)
                item.SubItems[functionOperation.TipoOperationRow.ID].Text = "Sim";
            else
                item.SubItems[functionOperation.TipoOperationRow.ID].Text = "Não";

            GISADataset.TrusteePrivilegeRow[] tpRows = (GISADataset.TrusteePrivilegeRow[])(GisaDataSetHelper.GetInstance().TrusteePrivilege.Select(string.Format("IDTrustee={0} AND IDTipoFunctionGroup={1} AND IdxTipoFunction = {2} AND IDTipoOperation = {3}", user.ID, functionOperation.IDTipoFunctionGroup, functionOperation.IdxTipoFunction, functionOperation.IDTipoOperation)));

            if (tpRows.Length == 0)
            {
                tpRows = (GISADataset.TrusteePrivilegeRow[])(GisaDataSetHelper.GetInstance().TrusteePrivilege.Select(string.Format("IDTrustee={0} AND IDTipoFunctionGroup={1} AND IdxTipoFunction = {2} AND IDTipoOperation = {3}", user.ID, functionOperation.IDTipoFunctionGroup, functionOperation.IdxTipoFunction, functionOperation.IDTipoOperation), "", DataViewRowState.Deleted));

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

        private bool CanChangePermission(ListViewItem item, int colIndex)
        {
            if (colIndex == 0 || item.Tag is GISADataset.TipoFunctionGroupRow)
                return false;

            GISADataset.TipoFunctionRow tipoFunction = (GISADataset.TipoFunctionRow)item.Tag;
            GISADataset.TipoOperationRow tipoOperation = (GISADataset.TipoOperationRow)(GisaDataSetHelper.GetInstance().TipoOperation.Select(string.Format("Name='{0}'", lstvwPermissoes.Columns[colIndex].Text))[0]);
            GISADataset.FunctionOperationRow[] functionOperation = (GISADataset.FunctionOperationRow[])(GisaDataSetHelper.GetInstance().FunctionOperation.Select(string.Format("IDTipoFunctionGroup={0} AND IdxTipoFunction={1} AND IDTipoOperation={2}", tipoFunction.IDTipoFunctionGroup, tipoFunction.idx, tipoOperation.ID)));

            if (functionOperation.Length == 0)
                return false;
            else
                return true;
        }
	}
} //end of root namespace