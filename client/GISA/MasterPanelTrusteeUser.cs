using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

namespace GISA
{
	public class MasterPanelTrusteeUser : GISA.MasterPanelTrustee
	{

	#region  Windows Form Designer generated code 

		private ToolBarButton ToolBarButtonChangePassword = new ToolBarButton();

		public MasterPanelTrusteeUser() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            ToolBar.ButtonClick += ToolBal_ButtonClick;

			this.lstVwTrustees.Columns.Remove(ColumnHeaderDescription);
			this.lstVwTrustees.Columns.Add(ColumnHeaderDescription);
			GetExtraResources();
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
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		protected internal System.Windows.Forms.ColumnHeader ColumnHeaderFullName;
		internal System.Windows.Forms.ImageList ImageList1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MasterPanelTrusteeUser));
			this.ColumnHeaderFullName = new System.Windows.Forms.ColumnHeader();
			this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
			//
			//lstVwTrustees
			//
			this.lstVwTrustees.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {this.ColumnHeaderFullName});
			this.lstVwTrustees.Name = "lstVwTrustees";
			//
			//lblFuncao
			//
			this.lblFuncao.Name = "lblFuncao";
			this.lblFuncao.Text = "Utilizadores";
			//
			//ToolBar
			//
			this.ToolBar.ImageList = this.ImageList1;
			this.ToolBar.Name = "ToolBar";
			//
			//ColumnHeaderFullName
			//
			this.ColumnHeaderFullName.Text = "Nome completo";
			this.ColumnHeaderFullName.Width = 248;
			//
			//ImageList1
			//
			this.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.ImageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.ImageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream"));
			this.ImageList1.TransparentColor = System.Drawing.Color.Fuchsia;
			//
			//MasterPanelTrusteeUser
			//
			this.Name = "MasterPanelTrusteeUser";

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		private void GetExtraResources()
		{
			ToolBar.ImageList = SharedResourcesOld.CurrentSharedResources.UsrManipulacaoImageList;
			ToolBarButtonAdd.ImageIndex = 0;
			ToolBarButtonAdd.ToolTipText = SharedResourcesOld.CurrentSharedResources.UsrManipulacaoString[0];
			ToolBarButtonEdit.ImageIndex = 1;
			ToolBarButtonEdit.ToolTipText = SharedResourcesOld.CurrentSharedResources.UsrManipulacaoString[1];
			ToolBarButtonDelete.ImageIndex = 2;
			ToolBarButtonDelete.ToolTipText = SharedResourcesOld.CurrentSharedResources.UsrManipulacaoString[2];
			if (SessionHelper.GetGisaPrincipal().TrusteeUserOperator.TrusteeRow.BuiltInTrustee)
			{
				ToolBarButtonChangePassword.ImageIndex = 3;
				ToolBarButtonChangePassword.ToolTipText = SharedResourcesOld.CurrentSharedResources.UsrManipulacaoString[3];
				ToolBarButton seperatorButton = new ToolBarButton();
				seperatorButton.Style = ToolBarButtonStyle.Separator;
				ToolBar.Buttons.Add(seperatorButton);
				ToolBar.Buttons.Add(ToolBarButtonChangePassword);
			}
		}

        private void ToolBal_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            // todos os restantes clicks em botoes são tratados na classe pai
            if (e.Button == ToolBarButtonChangePassword)
            {
                this.ChangePassword();
            }
        }

		protected override void AddTrustee()
		{
            GISADataset.TrusteeRow truRow = null;
			GISADataset.TrusteeUserRow usrRow = null;

			FormCreateTrustee form = new FormCreateTrustee();
			form.Text = "Novo utilizador";
			switch (form.ShowDialog())
			{
				case DialogResult.OK:
                    ((frmMain)TopLevelControl).EnterWaitMode();
					truRow = GisaDataSetHelper.GetInstance().Trustee.NewTrusteeRow();
					truRow.Name = form.txtTrusteeName.Text;
					truRow.Description = string.Empty;
					truRow.CatCode = "USR";
					truRow.BuiltInTrustee = false;
					truRow.IsActive = true;
					truRow.Versao = new byte[]{};
					truRow.isDeleted = 0;
					usrRow = GisaDataSetHelper.GetInstance().TrusteeUser.NewTrusteeUserRow();
					usrRow.Password = "";
					usrRow.FullName = "";
					usrRow.IsAuthority = false;
					usrRow["IDTrusteeUserDefaultAuthority"] = DBNull.Value;
					usrRow.Versao = new byte[]{};
					usrRow.isDeleted = 0;
					usrRow.TrusteeRow = truRow;

                    // selecionar o grupo "TODOS"
                    GISADataset.TrusteeGroupRow tgRow = (GISADataset.TrusteeGroupRow)(((GISADataset.TrusteeRow)(GisaDataSetHelper.GetInstance().Trustee.Select("Name='ACESSO_COMPLETO'")[0])).GetTrusteeGroupRows()[0]);

                    // incluir o novo utilizador no grupo "TODOS" por omissão
					GisaDataSetHelper.GetInstance().Trustee.AddTrusteeRow(truRow);
					GisaDataSetHelper.GetInstance().TrusteeUser.AddTrusteeUserRow(usrRow);
					GisaDataSetHelper.GetInstance().UserGroups.AddUserGroupsRow(usrRow, tgRow, new byte[]{}, 0);

					PersistencyHelper.CreateTrusteePreConcArguments ctpca = new PersistencyHelper.CreateTrusteePreConcArguments();
					ctpca.truRowID = truRow.ID;
					ctpca.usrRowID = usrRow.ID;

					Trace.WriteLine("A criar utilizador...");

                    PersistencyHelper.save(AddTrusteeIfUsernameDoesntExist, ctpca);
					PersistencyHelper.cleanDeletedData();

					if (! ctpca.successful)
					{
						MessageBox.Show("Este nome já existe atribuído a um utilizador ou grupo, " + Environment.NewLine + "por favor escolha outro nome.", form.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						UpdateTrustees(null);
					}
					else
					{
                        // actualizar interface
						UpdateTrustees(truRow);
					}

                    ((frmMain)TopLevelControl).LeaveWaitMode();
                    break;
				case DialogResult.Cancel:
				break;
			}
		}

		protected override void EditTrustee()
		{
			if (lstVwTrustees.SelectedItems.Count == 0)
				return;

            ((frmMain)TopLevelControl).EnterWaitMode();
			ListViewItem item = null;
			GISADataset.TrusteeRow truRow = null;
			item = lstVwTrustees.SelectedItems[0];
			truRow = (GISADataset.TrusteeRow)item.Tag;
		
			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
				if (TrusteeRule.Current.hasRegistos(((GISADataset.TrusteeRow)(lstVwTrustees.SelectedItems[0].Tag)).ID, ho.Connection))
					MessageBox.Show("Tenha em conta que alterações no nome de utilizador terá reflexos no registos no sistema.", "Utilizador", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw;
			}
			finally
			{
				ho.Dispose();
			}

			FormCreateTrustee form = new FormCreateTrustee();
			form.Text = "Editar utilizador";
			form.txtTrusteeName.Text = truRow.Name;
			switch (form.ShowDialog())
			{
				case DialogResult.OK:
		
					PersistencyHelper.EditTrusteePreConcArguments ctpca = new PersistencyHelper.EditTrusteePreConcArguments();
					ctpca.truRow = truRow;
					ctpca.username = form.txtTrusteeName.Text;

					Trace.WriteLine("A editar utilizador...");

					PersistencyHelper.save(EditTrusteeIfUsernameDoesntExist, ctpca);
					PersistencyHelper.cleanDeletedData();

					if (! ctpca.successful)
					{
						MessageBox.Show("Este nome já existe atribuído a um utilizador ou grupo, " + Environment.NewLine + "por favor escolha outro nome.", form.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						UpdateTrustees(null);
					}
					else
					{
						UpdateTrustees(truRow);
						UpdateContext();
					}
					UpdateToolBarButtons();
					break;
				case DialogResult.Cancel:
				    break;
			}
            ((frmMain)TopLevelControl).LeaveWaitMode();
		}

		private void AddTrusteeIfUsernameDoesntExist(PersistencyHelper.PreConcArguments args)
		{
			PersistencyHelper.CreateTrusteePreConcArguments cetpca = null;
			cetpca = (PersistencyHelper.CreateTrusteePreConcArguments)args;
			GISADataset.TrusteeRow truRow = (GISADataset.TrusteeRow)(GisaDataSetHelper.GetInstance().Trustee.Select("ID=" + cetpca.truRowID.ToString())[0]);
			GISADataset.TrusteeUserRow usrRow = (GISADataset.TrusteeUserRow)(GisaDataSetHelper.GetInstance().TrusteeUser.Select("ID=" + cetpca.usrRowID.ToString())[0]);

			if (! (DBAbstractDataLayer.DataAccessRules.TrusteeRule.Current.isValidNewTrustee(truRow.Name, cetpca.tran)))
			{
				System.Data.DataSet tempgisaBackup1 = cetpca.gisaBackup;
				PersistencyHelper.BackupRow(ref tempgisaBackup1, usrRow);
					cetpca.gisaBackup = tempgisaBackup1;
				System.Data.DataSet tempgisaBackup2 = cetpca.gisaBackup;
				PersistencyHelper.BackupRow(ref tempgisaBackup2, truRow);
					cetpca.gisaBackup = tempgisaBackup2;
				usrRow.RejectChanges();
				truRow.RejectChanges();
				cetpca.successful = false;
			}
			else
				cetpca.successful = true;
		}
		
		private void EditTrusteeIfUsernameDoesntExist(PersistencyHelper.PreConcArguments args)
		{
			PersistencyHelper.EditTrusteePreConcArguments etpca = null;
			etpca = (PersistencyHelper.EditTrusteePreConcArguments)args;

			if (DBAbstractDataLayer.DataAccessRules.TrusteeRule.Current.isValidNewTrustee(etpca.username, etpca.tran))
			{
				etpca.truRow.Name = etpca.username;
				etpca.successful = true;
			}
		}

		protected override void DeleteTrustee()
		{
			if (lstVwTrustees.SelectedItems.Count == 0)
				return;

			Int64 idTrustee = ((GISADataset.TrusteeRow)(lstVwTrustees.SelectedItems[0].Tag)).ID;
			if (idTrustee == SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID)
			{
				MessageBox.Show("O utilizador não pode ser removido visto estar a ser usado atualmente na aplicação.", "Utilizador", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

            ((frmMain)TopLevelControl).EnterWaitMode();

			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
				if (TrusteeRule.Current.hasRegistos(idTrustee, ho.Connection))
				{
					MessageBox.Show("O utilizador não pode ser removido pois já criou registos no sistema.", "Utilizador", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw;
			}
			finally
			{
				ho.Dispose();
                ((frmMain)TopLevelControl).LeaveWaitMode();
			}

			switch (MessageBox.Show("O utilizador será removido, deseja continuar?", "Utilizador", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
			{
				case DialogResult.OK:
					Trace.WriteLine("A apagar utilizador...");
					ListViewItem item = null;
					GISADataset.TrusteeRow truRow = null;
					item = lstVwTrustees.SelectedItems[0];
					truRow = (GISADataset.TrusteeRow)item.Tag;

					// após a mundaça dfe contexto anterior pode ter-se chegado à conclusão 
					// que a row já foi anteriormente eliminada por outro utilizador.
					if (truRow.RowState != DataRowState.Detached)
						DeleteTrusteeAndRelatedRows(truRow);

					// Remover a selecção do item vai provocar uma mudança de contexto que 
					// por sua vez vai provocar uma gravação dos dados
					lstVwTrustees.clearItemSelection(item);
					item.Remove();
					break;
				case DialogResult.Cancel:
				break;
			}
            ((frmMain)TopLevelControl).LeaveWaitMode();
		}

		private void ChangePassword()
		{
			FormChangePassword form = new FormChangePassword();
			switch (form.ShowDialog())
			{
				case DialogResult.OK:
				break;
				case DialogResult.Cancel:
				break;
			}
		}

		protected override void UpdateListViewItem(ListViewItem li, GISADataset.TrusteeRow t)
		{
			base.UpdateListViewItem(li, t);
            //TODO: if(! (t.GetTrusteeUserRows()[0].IsFullNameNull()))
            if (t.GetTrusteeUserRows().Length > 0 && !(t.GetTrusteeUserRows()[0].IsFullNameNull()))
				li.SubItems[ColumnHeaderFullName.Index].Text = t.GetTrusteeUserRows()[0].FullName;
			else
				li.SubItems[ColumnHeaderFullName.Index].Text = "";
		}

		protected override void UpdateTrustees(GISADataset.TrusteeRow tRow)
		{
			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
				TrusteeRule.Current.LoadTrusteesUsr(GisaDataSetHelper.GetInstance(), ho.Connection);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw;
			}
			finally
			{
				ho.Dispose();
			}

			lstVwTrustees.Items.Clear();
			ListViewItem item = null;
			ListViewItem selItem = null;
			foreach (GISADataset.TrusteeRow t in GisaDataSetHelper.GetInstance().Trustee.Select("isDeleted = 0"))
			{
	#if TESTING
				if (t.CatCode == "USR")
				{
					item = lstVwTrustees.Items.Add("");
					if (t == tRow)
					{
						selItem = item;
					}
					UpdateListViewItem(item, t);
					if (t.BuiltInTrustee)
					{
						item.ForeColor = System.Drawing.Color.Gray;
					}
				}
	#else
				if (t.CatCode == "USR" && ! t.BuiltInTrustee)
				{
					item = lstVwTrustees.Items.Add("");
					if (t == tRow)
					{
						selItem = item;
					}
					UpdateListViewItem(item, t);
				}
	#endif
			}
			lstVwTrustees.Sort();
			if (selItem != null)
			{
				lstVwTrustees.EnsureVisible(selItem.Index);
				lstVwTrustees.selectItem(selItem);
			}
		}
	}

} //end of root namespace