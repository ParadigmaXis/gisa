using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

namespace GISA
{
	public class SlavePanelPermissoesModulos : GISA.SinglePanel
	{

	#region  Windows Form Designer generated code 

		public SlavePanelPermissoesModulos() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            lstvwUserPermissoes.MouseDown += lstvwPermissoes_MouseDown;
            lstvwUserPermissoes.MouseMove += lstvwPermissoes_MouseMove;

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
		internal GISA.PanelMensagem PanelMensagem1;
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.ListView lstvwUserPermissoes;
		internal System.Windows.Forms.ColumnHeader colUtilizador;
        internal System.Windows.Forms.ColumnHeader colTipoUtilizador;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.PanelMensagem1 = new GISA.PanelMensagem();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.lstvwUserPermissoes = new System.Windows.Forms.ListView();
            this.colUtilizador = new System.Windows.Forms.ColumnHeader();
            this.colTipoUtilizador = new System.Windows.Forms.ColumnHeader();
            this.pnlToolbarPadding.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFuncao
            // 
            this.lblFuncao.Location = new System.Drawing.Point(0, 0);
            this.lblFuncao.Text = "Definir Permissões";
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
            // 
            // PanelMensagem1
            // 
            this.PanelMensagem1.BackColor = System.Drawing.SystemColors.Control;
            this.PanelMensagem1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMensagem1.IsLoaded = false;
            this.PanelMensagem1.IsPopulated = false;
            this.PanelMensagem1.Location = new System.Drawing.Point(0, 0);
            this.PanelMensagem1.Name = "PanelMensagem1";
            this.PanelMensagem1.Size = new System.Drawing.Size(600, 352);
            this.PanelMensagem1.TabIndex = 24;
            this.PanelMensagem1.Visible = false;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.Controls.Add(this.lstvwUserPermissoes);
            this.GroupBox1.Location = new System.Drawing.Point(0, 48);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(600, 304);
            this.GroupBox1.TabIndex = 26;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Utilizadores e suas permissões";
            // 
            // lstvwUserPermissoes
            // 
            this.lstvwUserPermissoes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colUtilizador,
            this.colTipoUtilizador});
            this.lstvwUserPermissoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstvwUserPermissoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstvwUserPermissoes.FullRowSelect = true;
            this.lstvwUserPermissoes.GridLines = true;
            this.lstvwUserPermissoes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstvwUserPermissoes.Location = new System.Drawing.Point(3, 16);
            this.lstvwUserPermissoes.MultiSelect = false;
            this.lstvwUserPermissoes.Name = "lstvwUserPermissoes";
            this.lstvwUserPermissoes.Size = new System.Drawing.Size(594, 285);
            this.lstvwUserPermissoes.TabIndex = 26;
            this.lstvwUserPermissoes.UseCompatibleStateImageBehavior = false;
            this.lstvwUserPermissoes.View = System.Windows.Forms.View.Details;
            // 
            // colUtilizador
            // 
            this.colUtilizador.Text = "Designação";
            this.colUtilizador.Width = 270;
            // 
            // colTipoUtilizador
            // 
            this.colTipoUtilizador.Text = "Tipo";
            this.colTipoUtilizador.Width = 100;
            // 
            // SlavePanelPermissoesModulos
            // 
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.PanelMensagem1);
            this.Name = "SlavePanelPermissoesModulos";
            this.Size = new System.Drawing.Size(600, 352);
            this.Controls.SetChildIndex(this.PanelMensagem1, 0);
            this.Controls.SetChildIndex(this.lblFuncao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.Controls.SetChildIndex(this.GroupBox1, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion
        
        public static Bitmap FunctionImage
        {
            get
            {
                return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "PermissoesPorModulo_enabled_32x32.png");
            }
        }
		
		private GISADataset.TipoFunctionRow mCurrentTipoFunction;
		public GISADataset.TipoFunctionRow currentTipoFunction
		{
			get
			{
				return mCurrentTipoFunction;
			}
		}

		public override void LoadData()
		{
			if (CurrentContext.TipoFunction == null)
			{
				mCurrentTipoFunction = null;
				return;
			}

			this.mCurrentTipoFunction = CurrentContext.TipoFunction;

			IDbConnection conn = GisaDataSetHelper.GetConnection();
			try
			{
				conn.Open();
				PermissoesRule.Current.LoadDataModuloPermissoes(GisaDataSetHelper.GetInstance(), currentTipoFunction.IDTipoFunctionGroup, currentTipoFunction.idx, conn);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw;
			}
			finally
			{
				conn.Close();
			}
		}

		public override void ModelToView()
		{
			string filter = null;
	#if TESTING
			filter = string.Empty;
	#else
			filter = "BuiltInTrustee=0";
	#endif
			foreach (GISADataset.TrusteeRow tRow in GisaDataSetHelper.GetInstance().Trustee.Select(filter, "CatCode Asc, Name Asc"))
			{
				AddItem(tRow);
			}
		}

		public override bool ViewToModel()
		{
    		return false;
		}

		public override void Deactivate()
		{
            GUIHelper.GUIHelper.clearField(lstvwUserPermissoes);
		}

		protected override void addContextChangeHandlers()
		{
			CurrentContext.TipoFunctionChanged += this.Recontextualize;
		}

		protected override void removeContextChangeHandlers()
		{
			CurrentContext.TipoFunctionChanged -= this.Recontextualize;
		}

		protected override bool isInnerContextValid()
		{
			return currentTipoFunction != null;
		}

		protected override bool isOuterContextValid()
		{
			return CurrentContext.TipoFunction != null;
		}

		protected override PanelMensagem GetNoContextMessage()
		{
			PanelMensagem1.LblMensagem.Text = "Para visualizar as permissões deverá selecionar uma funcionalidade no painel superior.";
			return PanelMensagem1;
		}

		// adicionar à listview as colunas correspondentes às operações possíveis sobre as funcionalidades da aplicação
		private void AddListViewColumns()
		{
			foreach (GISADataset.TipoOperationRow tipoOperation in GisaDataSetHelper.GetInstance().TipoOperation.Select())
			{
				if (tipoOperation.GetFunctionOperationRows().Length > 0)
				{
					System.Windows.Forms.ColumnHeader col = new System.Windows.Forms.ColumnHeader();
					col.Text = tipoOperation.Name;
					col.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
					col.Width = 84;
					lstvwUserPermissoes.Columns.Add(col);
				}
			}
		}

		// adiciona um utilizador à lista
		public void AddItem(GISADataset.TrusteeRow tRow)
		{
			ListViewItem item = new ListViewItem();
			item.SubItems.AddRange(new string[] {string.Empty, string.Empty});
			item.SubItems[0].Text = tRow.Name;
			if (tRow.CatCode.Equals("USR"))
				item.SubItems[1].Text = "Utilizador";
			else
				item.SubItems[1].Text = "Grupo de utilizadores";

			item.Tag = tRow;
			item.UseItemStyleForSubItems = false;
			ShowEffectivePermissions(item);
			lstvwUserPermissoes.Items.Add(item);
		}

		private void clearUserPermissions(GISADataset.TrusteeRow tRow)
		{
			GISADataset.TipoFunctionRow tfRow = ((SlavePanelPermissoesModulos)this).currentTipoFunction;
			foreach (GISADataset.TrusteePrivilegeRow tpRow in GisaDataSetHelper.GetInstance().TrusteePrivilege.Select(string.Format("IDTrustee={0} AND IDTipoFunctionGroup={1} AND IdxTipoFunction={2}", tRow.ID, tfRow.IDTipoFunctionGroup, tfRow.idx)))
				tpRow.Delete();
		}

		private void ShowEffectivePermissions(ListViewItem item)
		{
			foreach (GISADataset.TipoOperationRow tipoOperation in GisaDataSetHelper.GetInstance().TipoOperation.Select(string.Empty, "ID"))
			{
				GISADataset.FunctionOperationRow[] functionOperations = (GISADataset.FunctionOperationRow[])(GisaDataSetHelper.GetInstance().FunctionOperation.Select(string.Format("IDTipoFunctionGroup={0} AND IdxTipoFunction = {1} AND IDTipoOperation = {2}", currentTipoFunction.IDTipoFunctionGroup, currentTipoFunction.idx, tipoOperation.ID)));

				item.SubItems.Add(string.Empty);

				if (functionOperations.Length > 0)
				{
					PermissoesHelper.PermissionType permissaoEfectiva = PermissoesHelper.CalculateEffectivePermissions((GISADataset.TrusteeRow)item.Tag, functionOperations[0]);

					int colIndex = GetColumnIndex(tipoOperation.Name);

					if (permissaoEfectiva == PermissoesHelper.PermissionType.ExplicitGrant)
						item.SubItems[colIndex].Text = "Sim";
					else
						item.SubItems[colIndex].Text = "Não";

					if (GisaDataSetHelper.GetInstance().TrusteePrivilege.Select(string.Format("IDTrustee={0} AND IDTipoFunctionGroup={1} AND IdxTipoFunction = {2} AND IDTipoOperation = {3}", ((GISADataset.TrusteeRow)item.Tag).ID, functionOperations[0].IDTipoFunctionGroup, functionOperations[0].IdxTipoFunction, functionOperations[0].IDTipoOperation)).Length == 0)
						item.SubItems[colIndex].Font = PermissoesHelper.fontItalic;
					else
                        item.SubItems[colIndex].Font = PermissoesHelper.fontRegular;
				}
			}
		}

		private int GetColumnIndex(string colName)
		{
			foreach (ColumnHeader col in lstvwUserPermissoes.Columns)
			{
				if (col.Text.Equals(colName))
					return col.Index;
			}
			throw new Exception("Column not found!");
		}

		private void lstvwPermissoes_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				int colIndex = GetColumnIndex(e.X, e.Y);
				if (colIndex > 0)
				{
					ListViewItem item = lstvwUserPermissoes.GetItemAt(e.X, e.Y);
					if (CanChangePermission(currentTipoFunction, colIndex))
						ChangePermission((GISADataset.TrusteeRow)item.Tag, currentTipoFunction, item, colIndex);
				}
			}
		}

		private void lstvwPermissoes_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			int colIndex = GetColumnIndex(e.X, e.Y);
			if (colIndex > 0)
			{
				ListViewItem item = lstvwUserPermissoes.GetItemAt(e.X, e.Y);
				if (CanChangePermission(currentTipoFunction, colIndex))
					this.lstvwUserPermissoes.Cursor = Cursors.Hand;

				return;
			}
            this.lstvwUserPermissoes.Cursor = Cursors.Default;
		}

		private int GetColumnIndex(int posX, int posY)
		{
			if (lstvwUserPermissoes.SelectedItems.Count == 0)
				return -1;

			ListViewItem item = lstvwUserPermissoes.GetItemAt(posX, posY);
			if (! (item == lstvwUserPermissoes.SelectedItems[0]))
				return -1;

			if (item.SubItems.Count == 1)
				return -1;

			int width = 0;
			foreach (ColumnHeader col in lstvwUserPermissoes.Columns)
			{
				width += col.Width;
				if (width > posX)
				{
					return col.Index;
				}
			}

			return -1;
		}

		private void ChangePermission(GISADataset.TrusteeRow user, GISADataset.TipoFunctionRow tipoFunction, ListViewItem item, int colIndex)
		{
			GISADataset.TipoOperationRow tipoOperation = (GISADataset.TipoOperationRow)(GisaDataSetHelper.GetInstance().TipoOperation.Select(string.Format("Name='{0}'", lstvwUserPermissoes.Columns[colIndex].Text))[0]);

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
				trusteePrivileges = (GISADataset.TrusteePrivilegeRow[])(GisaDataSetHelper.GetInstance().TrusteePrivilege.Select(query, string.Empty, DataViewRowState.Deleted));
				if (trusteePrivileges.Length > 0)
				{
					trusteePrivileges[0].RejectChanges();
					trusteePrivileges[0].IsGrant = true;
				}
				else
					GisaDataSetHelper.GetInstance().TrusteePrivilege.AddTrusteePrivilegeRow(user, tipoFunction.IDTipoFunctionGroup, tipoFunction.idx, tipoOperation.ID, true, new byte[]{}, 0);
			}

			// popular as alterações
			GISADataset.FunctionOperationRow functionOperation = (GISADataset.FunctionOperationRow)(GisaDataSetHelper.GetInstance().FunctionOperation.Select(string.Format("IDTipoFunctionGroup={0} AND IdxTipoFunction={1} AND IDTipoOperation={2}", tipoFunction.IDTipoFunctionGroup, tipoFunction.idx, tipoOperation.ID))[0]);

			PermissoesHelper.PermissionType permissaoEfectiva = PermissoesHelper.CalculateEffectivePermissions(user, functionOperation);

			if (permissaoEfectiva == PermissoesHelper.PermissionType.ExplicitGrant)
				item.SubItems[colIndex].Text = "Sim";
			else
				item.SubItems[colIndex].Text = "Não";

			GISADataset.TrusteePrivilegeRow[] tpRows = (GISADataset.TrusteePrivilegeRow[])(GisaDataSetHelper.GetInstance().TrusteePrivilege.Select(string.Format("IDTrustee={0} AND IDTipoFunctionGroup={1} AND IdxTipoFunction = {2} AND IDTipoOperation = {3}", user.ID, functionOperation.IDTipoFunctionGroup, functionOperation.IdxTipoFunction, functionOperation.IDTipoOperation)));

			if (tpRows.Length == 0)
			{
				tpRows = (GISADataset.TrusteePrivilegeRow[])(GisaDataSetHelper.GetInstance().TrusteePrivilege.Select(string.Format("IDTrustee={0} AND IDTipoFunctionGroup={1} AND IdxTipoFunction = {2} AND IDTipoOperation = {3}", user.ID, functionOperation.IDTipoFunctionGroup, functionOperation.IdxTipoFunction, functionOperation.IDTipoOperation), string.Empty, DataViewRowState.Deleted));

				if (tpRows.Length == 0)
                    item.SubItems[colIndex].Font = PermissoesHelper.fontItalic;
				else
                    item.SubItems[colIndex].Font = PermissoesHelper.fontBoldItalic;
			}
			else
			{
				if (tpRows[0].RowState == DataRowState.Modified && ! (tpRows[0].IsGrant ^ (bool)(tpRows[0]["IsGrant", DataRowVersion.Original])))
                    item.SubItems[colIndex].Font = PermissoesHelper.fontRegular;
				else
                    item.SubItems[colIndex].Font = PermissoesHelper.fontBold;
			}
		}

		private bool CanChangePermission(GISADataset.TipoFunctionRow tipoFunction, int colIndex)
		{
			GISADataset.TipoOperationRow[] tipoOperation = (GISADataset.TipoOperationRow[])(GisaDataSetHelper.GetInstance().TipoOperation.Select(string.Format("Name='{0}'", lstvwUserPermissoes.Columns[colIndex].Text)));

			if (tipoOperation.Length == 0)
				return false;

			GISADataset.FunctionOperationRow[] functionOperation = (GISADataset.FunctionOperationRow[])(GisaDataSetHelper.GetInstance().FunctionOperation.Select(string.Format("IDTipoFunctionGroup={0} AND IdxTipoFunction={1} AND IDTipoOperation={2}", tipoFunction.IDTipoFunctionGroup, tipoFunction.idx, tipoOperation[0].ID)));

			if (functionOperation.Length == 0)
				return false;
			else
				return true;
		}
	}

} //end of root namespace