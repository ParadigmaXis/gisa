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
	public class MasterPanelTrusteeGroup : GISA.MasterPanelTrustee
	{

	#region  Windows Form Designer generated code 

		public MasterPanelTrusteeGroup() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
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
		internal System.Windows.Forms.ImageList ImageList1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MasterPanelTrusteeGroup));
			this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
			//
			//lstVwTrustees
			//
			this.lstVwTrustees.Name = "lstVwTrustees";
			//
			//lblFuncao
			//
			this.lblFuncao.Name = "lblFuncao";
			this.lblFuncao.Text = "Grupos de utilizadores";
			//
			//ToolBar
			//
			this.ToolBar.ImageList = this.ImageList1;
			this.ToolBar.Name = "ToolBar";
			//
			//ImageList1
			//
			this.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.ImageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.ImageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream"));
			this.ImageList1.TransparentColor = System.Drawing.Color.Fuchsia;
			//
			//MasterPanelTrusteeGroup
			//
			this.Name = "MasterPanelTrusteeGroup";

		}

	#endregion

		private void GetExtraResources()
		{
			ToolBar.ImageList = SharedResourcesOld.CurrentSharedResources.GrUsrManipulacaoImageList;
			ToolBarButtonAdd.ImageIndex = 0;
			ToolBarButtonAdd.ToolTipText = SharedResourcesOld.CurrentSharedResources.GrUsrManipulacaoString[0];
			ToolBarButtonEdit.ImageIndex = 1;
			ToolBarButtonEdit.ToolTipText = SharedResourcesOld.CurrentSharedResources.GrUsrManipulacaoString[1];
			ToolBarButtonDelete.ImageIndex = 2;
			ToolBarButtonDelete.ToolTipText = SharedResourcesOld.CurrentSharedResources.GrUsrManipulacaoString[2];
		}

		protected override void AddTrustee()
		{
			GISADataset.TrusteeRow truRow = null;
			GISADataset.TrusteeGroupRow grpRow = null;

			FormCreateTrustee form = new FormCreateTrustee();
			form.Text = "Novo grupo de utilizadores";
			switch (form.ShowDialog())
			{
				case DialogResult.OK:
					truRow = GisaDataSetHelper.GetInstance().Trustee.NewTrusteeRow();
					truRow.Name = form.txtTrusteeName.Text;
					truRow.Description = string.Empty;
					truRow.CatCode = "GRP";
					truRow.BuiltInTrustee = false;
					truRow.IsActive = true;
					truRow.Versao = new byte[]{};
					truRow.isDeleted = 0;
					grpRow = GisaDataSetHelper.GetInstance().TrusteeGroup.NewTrusteeGroupRow();
					grpRow.Versao = new byte[]{};
					grpRow.isDeleted = 0;
					grpRow.TrusteeRow = truRow;

					GisaDataSetHelper.GetInstance().Trustee.AddTrusteeRow(truRow);
					GisaDataSetHelper.GetInstance().TrusteeGroup.AddTrusteeGroupRow(grpRow);

					PersistencyHelper.CreateTrusteePreConcArguments ctpca = new PersistencyHelper.CreateTrusteePreConcArguments();
					ctpca.truRowID = truRow.ID;
					ctpca.grpRowID = grpRow.ID;

					Trace.WriteLine("A criar o grupo de utilizador...");

                    PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save(addTrusteeIfUsernameDoesntExist, ctpca);
					PersistencyHelper.cleanDeletedData();

					if (! ctpca.successful)
					{
						MessageBox.Show("Este nome já existe atribuído a um utilizador ou grupo, " + Environment.NewLine + "por favor escolha outro nome.", form.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						UpdateTrustees(null);
					}
					else
						UpdateTrustees(truRow);

					break;
				case DialogResult.Cancel:
				break;
			}
		}

		protected override void EditTrustee()
		{
			if (lstVwTrustees.SelectedItems.Count == 0)
				return;

			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
				if (TrusteeRule.Current.hasUsers(((GISADataset.TrusteeRow)(lstVwTrustees.SelectedItems[0].Tag)).ID, ho.Connection))
					MessageBox.Show("Tenha em conta que o Grupo de Utilizadores a editar já tem Utilizadores associados.", "Utilizador", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

			ListViewItem item = null;
			GISADataset.TrusteeRow truRow = null;
			item = lstVwTrustees.SelectedItems[0];
			truRow = (GISADataset.TrusteeRow)item.Tag;
            
			FormCreateTrustee form = new FormCreateTrustee();
			form.Text = "Editar grupo de utilizadores";
			form.txtTrusteeName.Text = truRow.Name;
			switch (form.ShowDialog())
			{
				case DialogResult.OK:

					PersistencyHelper.EditTrusteePreConcArguments etpca = new PersistencyHelper.EditTrusteePreConcArguments();
					etpca.truRow = truRow;
					etpca.username = form.txtTrusteeName.Text;

					Trace.WriteLine("A editar o grupo de utilizador...");

					PersistencyHelper.save(editTrusteeIfUsernameDoesntExist, etpca);
					PersistencyHelper.cleanDeletedData();

					if (! etpca.successful)
					{
						MessageBox.Show("Este nome já existe atribuído a um utilizador ou grupo, " + Environment.NewLine + "por favor escolha outro nome.", form.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						UpdateTrustees(null);
					}
					else
						UpdateTrustees(truRow);

					break;
				case DialogResult.Cancel:
				break;
			}
		}

		private void addTrusteeIfUsernameDoesntExist(PersistencyHelper.PreConcArguments args)
		{
			PersistencyHelper.CreateTrusteePreConcArguments ctpca = null;
			ctpca = (PersistencyHelper.CreateTrusteePreConcArguments)args;
			GISADataset.TrusteeRow truRow = (GISADataset.TrusteeRow)(GisaDataSetHelper.GetInstance().Trustee.Select("ID=" + ctpca.truRowID.ToString())[0]);
			GISADataset.TrusteeGroupRow grpRow = (GISADataset.TrusteeGroupRow)(GisaDataSetHelper.GetInstance().TrusteeGroup.Select("ID=" + ctpca.grpRowID.ToString())[0]);

			if (! (DBAbstractDataLayer.DataAccessRules.TrusteeRule.Current.isValidNewTrustee(truRow.Name, ctpca.tran)))
			{
				System.Data.DataSet tempgisaBackup1 = ctpca.gisaBackup;
				PersistencyHelper.BackupRow(ref tempgisaBackup1, grpRow);
					ctpca.gisaBackup = tempgisaBackup1;
				System.Data.DataSet tempgisaBackup2 = ctpca.gisaBackup;
				PersistencyHelper.BackupRow(ref tempgisaBackup2, truRow);
					ctpca.gisaBackup = tempgisaBackup2;
				grpRow.RejectChanges();
				truRow.RejectChanges();
				ctpca.successful = false;
			}
			else
			{
				ctpca.successful = true;
			}
		}

		private void editTrusteeIfUsernameDoesntExist(PersistencyHelper.PreConcArguments args)
		{
			PersistencyHelper.EditTrusteePreConcArguments etpca = null;
			etpca = (PersistencyHelper.EditTrusteePreConcArguments)args;
			etpca.successful = false;
			DataSet gisaBackup = etpca.gisaBackup;

			if (DBAbstractDataLayer.DataAccessRules.TrusteeRule.Current.isValidNewTrustee(etpca.username, etpca.tran))
			{
				PersistencyHelper.BackupRow(ref gisaBackup, etpca.truRow);
				etpca.truRow.Name = etpca.username;
				etpca.successful = true;
			}
		}

		protected override void DeleteTrustee()
		{
			if (lstVwTrustees.SelectedItems.Count == 0)
				return;

            ((frmMain)TopLevelControl).EnterWaitMode();

            string msg = string.Empty;
			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
                if (TrusteeRule.Current.hasUsers(((GISADataset.TrusteeRow)(lstVwTrustees.SelectedItems[0].Tag)).ID, ho.Connection))
                    msg = "Tenha em conta que o Grupo de Utilizadores a eliminar já tem Utilizadores associados. Deseja continuar?";
                else
                    msg = "O grupo de utilizadores será removido, deseja continuar?";
			}
			catch (Exception ex)
			{
                ((frmMain)TopLevelControl).LeaveWaitMode();
				Trace.WriteLine(ex);
				throw;
			}
			finally
			{
				ho.Dispose();
			}

			switch (MessageBox.Show(msg, "Utilizador", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
			{
				case DialogResult.OK:
					Trace.WriteLine("A apagar grupo de utilizadores...");
					ListViewItem item = null;
					GISADataset.TrusteeRow truRow = null;
					item = lstVwTrustees.SelectedItems[0];
					truRow = (GISADataset.TrusteeRow)item.Tag;

					// após a mundaça de contexto anterior pode ter-se chegado à conclusão 
					// que a row já foi anteriormente eliminada por outro utilizador.
					if (truRow.RowState != DataRowState.Detached)
					{
						DeleteTrusteeAndRelatedRows(truRow);
					}

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

		protected override void UpdateTrustees(GISADataset.TrusteeRow tRow)
		{
			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
				TrusteeRule.Current.LoadTrusteesGrpForUpdate(GisaDataSetHelper.GetInstance(), ho.Connection);
			}
			finally
			{
				ho.Dispose();
			}

			if (lstVwTrustees.SelectedItems.Count > 0)
			{
				lstVwTrustees.clearItemSelection(lstVwTrustees.SelectedItems[0]);
			}
			lstVwTrustees.Items.Clear();

			ListViewItem item = null;
			ListViewItem selItem = null;
			foreach (GISADataset.TrusteeRow t in GisaDataSetHelper.GetInstance().Trustee)
			{
	#if TESTING
				if (t.CatCode == "GRP")
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
				if (t.CatCode == "GRP" && ! t.BuiltInTrustee)
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