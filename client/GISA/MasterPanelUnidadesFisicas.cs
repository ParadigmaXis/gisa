using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using System.Collections.Generic;
using DBAbstractDataLayer.DataAccessRules;
using GISA.Model;
using GISA.SharedResources;

using GISA.Controls;
using GISA.Controls.Nivel;
using GISA.GUIHelper;

namespace GISA
{
	public class MasterPanelUnidadesFisicas : GISA.MasterPanel
	{

	#region  Windows Form Designer generated code 

		public MasterPanelUnidadesFisicas() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            ufList.BeforeNewListSelection += ufList_BeforeNewListSelection;
            ufList.ItemDrag += ufList_ItemDrag;
            ToolBar.ButtonClick += Toolbar_ButtonClick;
            base.StackChanged += MasterPanelUnidadesFisicas_StackChanged;
            ufList.KeyUpDelete += new EventHandler(ufList_KeyUpDelete);

			GetExtraResources();            

			UpdateToolBarButtons();
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
		internal System.Windows.Forms.ToolBarButton ToolBarButtonCreateUF;
		internal GISA.UnidadeFisicaList ufList;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonSeparator1;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonFiltro;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonRemoveUF;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonEditUF;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonSeparator2;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonPrint;
		internal System.Windows.Forms.ContextMenu ContextMenuPrint;
		internal System.Windows.Forms.MenuItem MenuItemPrintNaoAgregadas;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonCreateLikeUF;
		internal System.Windows.Forms.MenuItem MenuItemPrintIncorporacoes;



		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.ToolBarButtonCreateUF = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButtonRemoveUF = new System.Windows.Forms.ToolBarButton();
			this.ufList = new GISA.UnidadeFisicaList();
			this.ToolBarButtonSeparator1 = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButtonFiltro = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButtonEditUF = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButtonSeparator2 = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButtonPrint = new System.Windows.Forms.ToolBarButton();
			this.ContextMenuPrint = new System.Windows.Forms.ContextMenu();
			this.MenuItemPrintNaoAgregadas = new System.Windows.Forms.MenuItem();
			this.MenuItemPrintIncorporacoes = new System.Windows.Forms.MenuItem();
			this.ToolBarButtonCreateLikeUF = new System.Windows.Forms.ToolBarButton();
			this.pnlToolbarPadding.SuspendLayout();
			this.SuspendLayout();
			//
			//lblFuncao
			//
			this.lblFuncao.Location = new System.Drawing.Point(0, 0);
			this.lblFuncao.Text = "Unidades físicas";
			//
			//ToolBar
			//
			this.ToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {this.ToolBarButtonCreateUF, this.ToolBarButtonCreateLikeUF, this.ToolBarButtonEditUF, this.ToolBarButtonRemoveUF, this.ToolBarButtonSeparator1, this.ToolBarButtonFiltro, this.ToolBarButtonSeparator2, this.ToolBarButtonPrint});
			this.ToolBar.ImageList = null;
			//
			//pnlToolbarPadding
			//
			this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
			//
			//ToolBarButtonCreateUF
			//
			this.ToolBarButtonCreateUF.ImageIndex = 0;
			this.ToolBarButtonCreateUF.Name = "ToolBarButtonCreateUF";
			//
			//ToolBarButtonRemoveUF
			//
			this.ToolBarButtonRemoveUF.ImageIndex = 0;
			this.ToolBarButtonRemoveUF.Name = "ToolBarButtonRemoveUF";
			//
			//ufList
			//
			this.ufList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ufList.Location = new System.Drawing.Point(0, 52);
			this.ufList.Name = "ufList";
			this.ufList.Padding = new System.Windows.Forms.Padding(6);
			this.ufList.Size = new System.Drawing.Size(600, 228);
			this.ufList.TabIndex = 2;
			//
			//ToolBarButtonSeparator1
			//
			this.ToolBarButtonSeparator1.Name = "ToolBarButtonSeparator1";
			this.ToolBarButtonSeparator1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			//
			//ToolBarButtonFiltro
			//
			this.ToolBarButtonFiltro.ImageIndex = 3;
			this.ToolBarButtonFiltro.Name = "ToolBarButtonFiltro";
			this.ToolBarButtonFiltro.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			//
			//ToolBarButtonEditUF
			//
			this.ToolBarButtonEditUF.ImageIndex = 1;
			this.ToolBarButtonEditUF.Name = "ToolBarButtonEditUF";
			//
			//ToolBarButtonSeparator2
			//
			this.ToolBarButtonSeparator2.Name = "ToolBarButtonSeparator2";
			this.ToolBarButtonSeparator2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			//
			//ToolBarButtonPrint
			//
			this.ToolBarButtonPrint.DropDownMenu = this.ContextMenuPrint;
			this.ToolBarButtonPrint.ImageIndex = 1;
			this.ToolBarButtonPrint.Name = "ToolBarButtonPrint";
			this.ToolBarButtonPrint.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
			this.ToolBarButtonPrint.ToolTipText = "Gerar relatório";
			this.ToolBarButtonPrint.Visible = false;
			//
			//ContextMenuPrint
			//
			this.ContextMenuPrint.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {this.MenuItemPrintNaoAgregadas, this.MenuItemPrintIncorporacoes});
			//
			//MenuItemPrintNaoAgregadas
			//
			this.MenuItemPrintNaoAgregadas.DefaultItem = true;
			this.MenuItemPrintNaoAgregadas.Index = 0;
			this.MenuItemPrintNaoAgregadas.Text = "Não agregadas";
			//
			//MenuItemPrintIncorporacoes
			//
			this.MenuItemPrintIncorporacoes.Index = 1;
			this.MenuItemPrintIncorporacoes.Text = "Incorporações";
			this.MenuItemPrintIncorporacoes.Visible = false;
			//
			//ToolBarButtonCreateLikeUF
			//
			this.ToolBarButtonCreateLikeUF.Name = "ToolBarButtonCreateLikeUF";
			//
			//MasterPanelUnidadesFisicas
			//
			this.Controls.Add(this.ufList);
			this.Name = "MasterPanelUnidadesFisicas";
			this.Controls.SetChildIndex(this.lblFuncao, 0);
			this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
			this.Controls.SetChildIndex(this.ufList, 0);
			this.pnlToolbarPadding.ResumeLayout(false);
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

        private long ShowUFDialog_edID = long.MinValue;
		private string ShowUFDialog_designacao = string.Empty;
		private string ShowUFDialog_guia = string.Empty;

		private void GetExtraResources()
		{
			ToolBar.ImageList = SharedResourcesOld.CurrentSharedResources.UFManipulacaoImageList;
			ToolBarButtonCreateUF.ImageIndex = 0;
			ToolBarButtonCreateLikeUF.ImageIndex = 1;
			ToolBarButtonEditUF.ImageIndex = 2;
			ToolBarButtonRemoveUF.ImageIndex = 3;
			ToolBarButtonFiltro.ImageIndex = 4;
			ToolBarButtonPrint.ImageIndex = 5;

			string[] strs = SharedResourcesOld.CurrentSharedResources.UFManipulacaoStrings;
			ToolBarButtonCreateUF.ToolTipText = strs[0];
			ToolBarButtonCreateLikeUF.ToolTipText = strs[1];
			ToolBarButtonEditUF.ToolTipText = strs[2];
			ToolBarButtonRemoveUF.ToolTipText = strs[3];
			ToolBarButtonFiltro.ToolTipText = strs[4];
			ToolBarButtonPrint.ToolTipText = strs[5];
		}

		public override void UpdateToolBarButtons()
		{
			UpdateToolBarButtons(null);
		}

		public override void UpdateToolBarButtons(ListViewItem item)
		{
			ToolBarButtonCreateUF.Enabled = AllowCreate;
			ListViewItem selectedItem = null;

			if (item != null && item.ListView != null)
				selectedItem = item;
			else if (item == null && ufList.SelectedItems.Count == 1)
				selectedItem = ufList.SelectedItems[0];

			if (selectedItem == null || ((DataRow)selectedItem.Tag).RowState == DataRowState.Detached)
			{
				ToolBarButtonCreateLikeUF.Enabled = false;
				ToolBarButtonEditUF.Enabled = false;
				ToolBarButtonRemoveUF.Enabled = false;
			}
			else
			{
				ToolBarButtonCreateLikeUF.Enabled = AllowCreate;
				ToolBarButtonEditUF.Enabled = AllowEdit;
				ToolBarButtonRemoveUF.Enabled = AllowDelete;
			}
		}

		public void disableAddEditAndRemove()
		{
			ToolBarButtonCreateLikeUF.Enabled = false;
			ToolBarButtonCreateUF.Enabled = false;
			ToolBarButtonEditUF.Enabled = false;
			ToolBarButtonRemoveUF.Enabled = false;
		}

		private void ufList_BeforeNewListSelection(object sender, BeforeNewSelectionEventArgs e)
		{
			e.SelectionChange = UpdateContext(e.ItemToBeSelected);
			if (e.SelectionChange)
				UpdateToolBarButtons(e.ItemToBeSelected);
		}

		private void ufList_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
		{
			// Só é permitido fazer drag quando este painel é usado como suporte
			if (! (! (MasterPanel.isContextPanel(this)) || ((frmMain)TopLevelControl).isSuportPanel))
				return;

			object DragDropObject = null;

			LoadUFData();

			if (ufList.SelectedItems.Count > 1)
				DragDropObject = GetlstVwNivelRowArray();
			else if (e.Item != null)
				DragDropObject = (GISADataset.NivelRow)(((ListViewItem)e.Item).Tag);

			Trace.WriteLine("Dragging " + DragDropObject.ToString().GetType().FullName);
			DoDragDrop(DragDropObject, DragDropEffects.Link);
		}

		private void UnidadeFisicaList1_MyColumnClick(object sender, MyColumnClickEventArgs e)
		{
			if (ufList.Items.Count > 0)
				ufList.ReloadList();
		}

		private void LoadUFData()
		{
			GISADataset.NivelRow nRow = null;
			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
				GisaDataSetHelper.ManageDatasetConstraints(false);
				foreach (ListViewItem lvItem in ufList.SelectedItems)
				{
					nRow = (GISADataset.NivelRow)lvItem.Tag;
					DBAbstractDataLayer.DataAccessRules.UFRule.Current.LoadUFData(GisaDataSetHelper.GetInstance(), nRow.ID, ho.Connection);
				}
				GisaDataSetHelper.ManageDatasetConstraints(true);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				throw;
			}
			finally
			{
				ho.Dispose();
			}
		}

		private GISADataset.NivelRow[] GetlstVwNivelRowArray()
		{
			GISADataset.NivelRow[] ns = null;

            ns = new GISADataset.NivelRow[ufList.SelectedItems.Count];

			int i = 0;
			foreach (ListViewItem li in ufList.SelectedItems)
			{
				ns[i] = (GISADataset.NivelRow)li.Tag;
				i = i + 1;
			}
			return ns;
		}

        private void ufList_KeyUpDelete(object sender, EventArgs e)
        {
            if (ufList.SelectedItems.Count == 1)
                RemoveUF();
        }

		private void Toolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			if (e.Button == ToolBarButtonFiltro)
                ufList.FilterVisible = ToolBarButtonFiltro.Pushed;
			else if (e.Button == ToolBarButtonCreateLikeUF)
				DuplicateUF();
			else if (e.Button == ToolBarButtonCreateUF)
				ShowUFDialog(true);
			else if (e.Button == ToolBarButtonEditUF)
				ShowUFDialog(false);
			else if (e.Button == ToolBarButtonRemoveUF)
                RemoveUF();
		}

        private void RemoveUF()
        {
            //TODO: FIXME O botão devia estar inibido
            if (ufList.SelectedItems.Count > 0)
            {
                ListViewItem item = null;
                if (((GISADataset.NivelRow)(ufList.SelectedItems[0].Tag)).RowState == DataRowState.Detached)
                {
                    item = ufList.SelectedItems[0];
                    ufList.ClearItemSelection(item);
                    item.Remove();
                    UpdateToolBarButtons();
                }
                else
                {
                    GISADataset.NivelRow nr = null;
                    item = ufList.SelectedItems[0];
                    nr = (GISADataset.NivelRow)item.Tag;
                    var uaAssociadas = new List<string>();
                    var uaFrdAssociadas = new List<UnidadesFisicasHelper.UaInfo>();

                    switch (ConfirmCascadeDeletion("A Unidade física \"" + nr.GetNivelDesignadoRows()[0].Designacao + "\" será removida. Pretende continuar?", "A Unidade física \"" + nr.GetNivelDesignadoRows()[0].Designacao + "\" será removida apesar das associações existentes. " + "Pretende continuar?", nr, ref uaAssociadas, ref uaFrdAssociadas))
                    {

                        case DialogResult.Yes:
                        case DialogResult.OK:
                            Trace.WriteLine("A apagar unidade física...");
                            ufList.ClearItemSelection(item);

                            if (nr.RowState != DataRowState.Detached)
                            {
                                // registar a eliminação da unidade física
                                CurrentContext.RaiseRegisterModificationEvent(nr.GetFRDBaseRows()[0]);
                                item.Remove();

                                deleteUF(nr, uaAssociadas);
                            }

                            UpdateToolBarButtons();
                            break;
                    }
                }
            }
        }

        private void deleteUF(GISADataset.NivelRow nr, List<string> uaAssociadas)
		{
			((frmMain)TopLevelControl).EnterWaitMode();
			try
			{
                long ufID = nr.ID;
                PersistencyHelper.SaveResult successfulSave = Nivel.CascadeDeleteNivel(nr);
                if (successfulSave == PersistencyHelper.SaveResult.successful)
                {
                    GISA.Search.Updater.updateNivelDocumental(uaAssociadas);
                    GISA.Search.Updater.updateUnidadeFisica(ufID);

                    ufList.decrementItemCounter();
                }
			}
            
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw;
			}
			finally
			{
				((frmMain)TopLevelControl).LeaveWaitMode();
			}
		}

        private bool ValidateSelection()
        {
            if (ufList.SelectedItems.Count != 1)
            {
                MessageBox.Show("Não foi detetada uma unidade física selecionada." + System.Environment.NewLine + 
                    "A operação vai ser abortada.", 
                    "Duplicação da unidade física",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Trace.WriteLine("UF duplication error: selected UFs: " + ufList.SelectedItems.Count.ToString());
                return false;
            }

            var selUF = ufList.SelectedItems[0];

            if (selUF.Tag == null || selUF.Tag as GISADataset.NivelRow == null)
            {
                MessageBox.Show("Ocorreu um erro inesperado com a duplicação da unidade física selecionada." + System.Environment.NewLine + 
                   "A operação vai ser abortada.",
                   "Duplicação da unidade física",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);
                if (selUF.Tag == null)
                    Trace.WriteLine("UF duplication error: listviewitem tag is null: " + selUF.ToString());
                else
                    Trace.WriteLine("UF duplication error: listviewitem tag is not NivelRow: " + selUF.Tag.GetType());
                return false;
            }

            return true;
        }

		private void DuplicateUF()
		{
            if (!ValidateSelection())
                return;

			GISADataset.NivelRow nufRow = (GISADataset.NivelRow)(ufList.SelectedItems[0].Tag);

            //forçar uma mudança de contexto para que a unidade física seleccionada seja gravada
            ufList.ClearItemSelection(ufList.SelectedItems[0]);

            //testar se a unidade física não foi apagada por outro utilizador entretanto
            if (nufRow == null || nufRow.RowState == DataRowState.Detached)
            {
                MessageBox.Show("A unidade física a ser duplicada foi eliminada por outro utilizador " + System.Environment.NewLine + 
                    "pelo que a operação não pode ser concluída.",
                    "Duplicação da unidade física", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);

                return;
            }

            Trace.WriteLine("A duplicar unidade física id: " + nufRow.ID);

            PersistencyHelper.AddEditUFPreConcArguments argsPC = new PersistencyHelper.AddEditUFPreConcArguments();
            PersistencyHelper.IsCodigoUFBeingUsedPreSaveArguments argsPS = new PersistencyHelper.IsCodigoUFBeingUsedPreSaveArguments();

			//carregar toda a informação da unidade física a ser duplicada
			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
                GisaDataSetHelper.ManageDatasetConstraints(false);
				DBAbstractDataLayer.DataAccessRules.UFRule.Current.LoadUF(GisaDataSetHelper.GetInstance(), nufRow.ID, ho.Connection);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
				throw;
			}
			finally
			{
				ho.Dispose();
                GisaDataSetHelper.ManageDatasetConstraints(true);
			}

			//duplicar uf seleccionada
			GISADataset.TipoNivelRelacionadoRow tnrRow = (GISADataset.TipoNivelRelacionadoRow)(GisaDataSetHelper.GetInstance().TipoNivelRelacionado. Select(string.Format("ID={0}", TipoNivelRelacionado.UF))[0]);

			GISADataset.NivelDesignadoRow ndufRow = nufRow.GetNivelDesignadoRows()[0];
			GISADataset.NivelUnidadeFisicaRow nivelUFRow = ndufRow.GetNivelUnidadeFisicaRows()[0];
			GISADataset.RelacaoHierarquicaRow rhufRow = nufRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0];
			GISADataset.NivelRow nedRow = rhufRow.NivelRowByNivelRelacaoHierarquicaUpper;
			GISADataset.FRDBaseRow[] frdufRows = nufRow.GetFRDBaseRows();
			GISADataset.FRDBaseRow newfrdufRow = null;
            GISADataset.LocalConsultaRow newLocalConsultaRow = nivelUFRow.IsIDLocalConsultaNull() ? null : nivelUFRow.LocalConsultaRow;

			GISADataset.NivelRow newNufRow = GisaDataSetHelper.GetInstance().Nivel.AddNivelRow(nufRow.TipoNivelRow, nufRow.Codigo, nufRow.CatCode, new byte[]{}, 0);
			ndufRow = GisaDataSetHelper.GetInstance().NivelDesignado.AddNivelDesignadoRow(newNufRow, ndufRow.Designacao, new byte[]{}, 0);
            GISADataset.TipoEntregaRow teRow = null;
            if (!nivelUFRow.IsIDTipoEntregaNull())
                teRow = (GISADataset.TipoEntregaRow)
                    (GisaDataSetHelper.GetInstance().TipoEntrega.Select("ID=" + nivelUFRow.IDTipoEntrega)[0]);
            nivelUFRow = GisaDataSetHelper.GetInstance().NivelUnidadeFisica.AddNivelUnidadeFisicaRow(ndufRow, GisaDataSetHelper.GetDBNullableText(nivelUFRow, "GuiaIncorporacao"), GisaDataSetHelper.GetDBNullableText(nivelUFRow, "CodigoBarras"), new byte[] { }, 0, false, teRow, newLocalConsultaRow);
			rhufRow = GisaDataSetHelper.GetInstance().RelacaoHierarquica.AddRelacaoHierarquicaRow(newNufRow, nedRow, tnrRow, GisaDataSetHelper.GetDBNullableText(rhufRow, "Descricao"), GisaDataSetHelper.GetDBNullableText(rhufRow, "InicioAno"), GisaDataSetHelper.GetDBNullableText(rhufRow, "InicioMes"), GisaDataSetHelper.GetDBNullableText(rhufRow, "InicioDia"), GisaDataSetHelper.GetDBNullableText(rhufRow, "FimAno"), GisaDataSetHelper.GetDBNullableText(rhufRow, "FimMes"), GisaDataSetHelper.GetDBNullableText(rhufRow, "FimDia"), new byte[]{}, 0);

			GISADataset.SFRDDatasProducaoRow dpufRow = null;
			GISADataset.SFRDUFCotaRow cufRow = null;
			GISADataset.SFRDConteudoEEstruturaRow conteudoufRow = null;
            GISADataset.SFRDDatasProducaoRow[] dpufRows;
            GISADataset.SFRDUFCotaRow[] cufRows;
            GISADataset.SFRDConteudoEEstruturaRow[] conteudoufRows;
            GISADataset.SFRDUFDescricaoFisicaRow[] descricaoFisicaRows;
			GISADataset.SFRDUFDescricaoFisicaRow newDescricaoFisicaRow = null;
			if (frdufRows.Length > 0)
			{
				newfrdufRow = GisaDataSetHelper.GetInstance().FRDBase.AddFRDBaseRow(newNufRow, frdufRows[0].TipoFRDBaseRow, GisaDataSetHelper.GetDBNullableText(frdufRows[0], "NotaDoArquivista"), GisaDataSetHelper.GetDBNullableText(frdufRows[0], "RegrasOuConvencoes"), new byte[]{}, 0);
				argsPC.frdufRowID = newfrdufRow.ID;
				dpufRows = frdufRows[0].GetSFRDDatasProducaoRows();
				cufRows = frdufRows[0].GetSFRDUFCotaRows();
				conteudoufRows = frdufRows[0].GetSFRDConteudoEEstruturaRows();
				descricaoFisicaRows = frdufRows[0].GetSFRDUFDescricaoFisicaRows();

                if (dpufRows.Length > 0)
                    dpufRow = GisaDataSetHelper.GetInstance().SFRDDatasProducao.AddSFRDDatasProducaoRow(
                        newfrdufRow, 
                        GisaDataSetHelper.GetDBNullableText(dpufRows[0], "InicioTexto"),
                        GisaDataSetHelper.GetDBNullableText(dpufRows[0], "InicioAno"), 
                        GisaDataSetHelper.GetDBNullableText(dpufRows[0], "InicioMes"), 
                        GisaDataSetHelper.GetDBNullableText(dpufRows[0], "InicioDia"), 
                        dpufRows[0].InicioAtribuida, 
                        GisaDataSetHelper.GetDBNullableText(dpufRows[0], "FimTexto"), 
                        GisaDataSetHelper.GetDBNullableText(dpufRows[0], "FimAno"), 
                        GisaDataSetHelper.GetDBNullableText(dpufRows[0], "FimMes"), 
                        GisaDataSetHelper.GetDBNullableText(dpufRows[0], "FimDia"), 
                        dpufRows[0].FimAtribuida, 
                        new byte[] { }, 0);

                if (cufRows.Length > 0)
                    cufRow = GisaDataSetHelper.GetInstance().SFRDUFCota.AddSFRDUFCotaRow(
                        newfrdufRow, 
                        GisaDataSetHelper.GetDBNullableText(cufRows[0], "Cota"), 
                        new byte[]{}, 0);

                if (conteudoufRows.Length > 0)
                    conteudoufRow = GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.AddSFRDConteudoEEstruturaRow(
                        newfrdufRow, 
                        GisaDataSetHelper.GetDBNullableText(conteudoufRows[0], "ConteudoInformacional"), 
                        GisaDataSetHelper.GetDBNullableText(conteudoufRows[0], "Incorporacao"), 
                        new byte[]{}, 0);

				newDescricaoFisicaRow = GisaDataSetHelper.GetInstance().SFRDUFDescricaoFisica.NewSFRDUFDescricaoFisicaRow();
				newDescricaoFisicaRow.FRDBaseRow = newfrdufRow;

                if (descricaoFisicaRows.Length > 0)
                {
                    newDescricaoFisicaRow.TipoAcondicionamentoRow = descricaoFisicaRows[0].TipoAcondicionamentoRow;
                    newDescricaoFisicaRow.TipoMedidaRow = descricaoFisicaRows[0].TipoMedidaRow;
                    newDescricaoFisicaRow.Versao = new byte[] { };
                    newDescricaoFisicaRow.isDeleted = 0;
                    if (!(descricaoFisicaRows[0]["MedidaLargura"] == DBNull.Value))
                        newDescricaoFisicaRow.MedidaLargura = descricaoFisicaRows[0].MedidaLargura;

                    if (!(descricaoFisicaRows[0]["MedidaAltura"] == DBNull.Value))
                        newDescricaoFisicaRow.MedidaAltura = descricaoFisicaRows[0].MedidaAltura;

                    if (!(descricaoFisicaRows[0]["MedidaProfundidade"] == DBNull.Value))
                        newDescricaoFisicaRow.MedidaProfundidade = descricaoFisicaRows[0].MedidaProfundidade;
                }

				GisaDataSetHelper.GetInstance().SFRDUFDescricaoFisica.AddSFRDUFDescricaoFisicaRow(newDescricaoFisicaRow);
			}

			List<long> uaAssociadas = new List<long>();
			foreach (GISADataset.SFRDUnidadeFisicaRow sfrdUA in nufRow.GetSFRDUnidadeFisicaRows())
			{
				GisaDataSetHelper.GetInstance().SFRDUnidadeFisica.AddSFRDUnidadeFisicaRow(sfrdUA.FRDBaseRow, newNufRow, null, new byte[]{}, 0);
				uaAssociadas.Add(sfrdUA.FRDBaseRow.ID);
			}

			argsPC.Operation = PersistencyHelper.AddEditUFPreConcArguments.Operations.CreateLike;
			argsPC.nivelUFRowID = newNufRow.ID;
			argsPC.ndufRowID = ndufRow.ID;
			argsPC.rhufRowID = rhufRow.ID;
			argsPC.rhufRowIDUpper = rhufRow.IDUpper;
			argsPC.nufufRowID = nivelUFRow.ID;
			argsPC.uaAssociadas = uaAssociadas;

			argsPS.nivelUFRowID = nivelUFRow.ID;
			argsPC.psa = argsPS;

            var postSaveAction = new PostSaveAction();
            PersistencyHelper.UpdatePermissionsPostSaveArguments args = new PersistencyHelper.UpdatePermissionsPostSaveArguments();
            postSaveAction.args = args;

            postSaveAction.postSaveDelegate = delegate(PersistencyHelper.PostSaveArguments postSaveArgs)
            {
                if (!postSaveArgs.cancelAction && argsPC.OperationError == PersistencyHelper.AddEditUFPreConcArguments.OperationErrors.NoError)
                {
                    CurrentContext.RaiseRegisterModificationEvent(newfrdufRow);
                    PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao,
                        GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao.Cast<GISADataset.FRDBaseDataDeDescricaoRow>().Where(frd => frd.RowState == DataRowState.Added).ToArray(), postSaveArgs.tran);
                }
            };

            PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save(DelegatesHelper.HandleUF, argsPC, DelegatesHelper.SetCodigo, argsPS, postSaveAction);
            PersistencyHelper.cleanDeletedData(PersistencyHelper.determinaNuvem("NivelUnidadeFisica"));

			try
			{
				if (argsPC.OperationError == PersistencyHelper.AddEditUFPreConcArguments.OperationErrors.NewUF)
				{
					MessageBox.Show(argsPC.message, "Criar unidade física", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				else
				{
                    // adicionar a unidade física à lista
					ufList.AddNivel(newNufRow);

                    // actualizar o índice
                    if (successfulSave == PersistencyHelper.SaveResult.successful)
                    {
                        ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                        try
                        {
                            List<string> IDNiveis = DBAbstractDataLayer.DataAccessRules.UFRule.Current.GetNiveisDocAssociados(newNufRow.ID, ho.Connection);
                            GISA.Search.Updater.updateNivelDocumental(IDNiveis);
                            GISA.Search.Updater.updateUnidadeFisica(newfrdufRow.NivelRow.ID);
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine(ex.ToString());
                            throw;
                        }
                        finally
                        {
                            ho.Dispose();
                        }
                    }
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine("Exception while refreshing data: " + ex.ToString());
				throw;
			}
		}

		private void ShowUFDialog(bool CreateNew)
		{
			GISADataset.TipoNivelRelacionadoRow tnrRow = (GISADataset.TipoNivelRelacionadoRow)(GisaDataSetHelper.GetInstance().TipoNivelRelacionado. Select(string.Format("ID={0}", TipoNivelRelacionado.UF))[0]);

			GISADataset.NivelRow nufRow = null;
			GISADataset.NivelDesignadoRow ndufRow = null;
			GISADataset.NivelDesignadoRow ndedRow = null;
			GISADataset.RelacaoHierarquicaRow rhufRow = null;
			GISADataset.NivelUnidadeFisicaRow nufufRow = null;
			GISADataset.FRDBaseRow frdufRow = null;
            GISADataset.SFRDUFCotaRow cota = null;
            GISADataset.SFRDUFDescricaoFisicaRow df = null;
            GISADataset.SFRDDatasProducaoRow dp = null;
            GISADataset.SFRDConteudoEEstruturaRow ce = null;
            List<GISADataset.NivelDesignadoRow> edsNdRRows = null;

            if (CreateNew && ((frmMain)TopLevelControl).isSuportPanel && !PersistencyHelper.hasCurrentDatasetChanges())
            {
                try
                {
                    Trace.WriteLine("Saving before creating new UF in suport panel...");
                    PersistencyHelper.save();
                    PersistencyHelper.cleanDeletedData();
                }
                catch (Exception)
                {
                    MessageBox.Show("Ocorreu um erro ao tentar abrir o formulário de criação." + System.Environment.NewLine + "Por favor contacte o administrador de sistema.", "Criação de unidade física", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                
                using (GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection()))
                {
                    if (CurrentContext.NivelEstrututalDocumental == null)
                    {
                        MessageBox.Show("Ocorreu um erro ao tentar abrir o formulário de criação." + System.Environment.NewLine + "Por favor contacte o administrador de sistema.", "Criação de unidade física", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Trace.WriteLine("Pincipal context not found!!");
                        return;
                    }
                    DBAbstractDataLayer.DataAccessRules.NivelRule.Current.LoadEntidadesDetentoras(GisaDataSetHelper.GetInstance(), ho.Connection);
                    var edsIds = UFRule.Current.GetEntidadeDetentoraForNivel(CurrentContext.NivelEstrututalDocumental.ID, ho.Connection);

                    var ndRows=GisaDataSetHelper.GetInstance().NivelDesignado.Cast<GISADataset.NivelDesignadoRow>().Where(r=>r.RowState != DataRowState.Deleted);
                    edsNdRRows = edsIds.Cast<long>().Select(id => ndRows.Single(r=>r.ID==id)).ToList();
                }
            }

			using (GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection()))
            {
				GisaDataSetHelper.ManageDatasetConstraints(false);
				DBAbstractDataLayer.DataAccessRules.NivelRule.Current.LoadUFsRelatedData(GisaDataSetHelper.GetInstance(), ho.Connection);
				GisaDataSetHelper.ManageDatasetConstraints(true);
			}

			PersistencyHelper.AddEditUFPreConcArguments argsPC = new PersistencyHelper.AddEditUFPreConcArguments();
			PersistencyHelper.IsCodigoUFBeingUsedPreSaveArguments argsPS = new PersistencyHelper.IsCodigoUFBeingUsedPreSaveArguments();

			FormCreateUF frm = new FormCreateUF();
            if (CreateNew)
            {
                argsPC.Operation = PersistencyHelper.AddEditUFPreConcArguments.Operations.Create;
                // nivel
                nufRow = GisaDataSetHelper.GetInstance().Nivel.NewNivelRow();
                // nivelDesignado
                ndufRow = GisaDataSetHelper.GetInstance().NivelDesignado.NewNivelDesignadoRow();
                // RelacaoHierarquica
                rhufRow = GisaDataSetHelper.GetInstance().RelacaoHierarquica.NewRelacaoHierarquicaRow();
                // NivelUnidadeFisicaRow
                nufufRow = GisaDataSetHelper.GetInstance().NivelUnidadeFisica.NewNivelUnidadeFisicaRow();
                // FRDBaseRow
                frdufRow = GisaDataSetHelper.GetInstance().FRDBase.NewFRDBaseRow();
                // nivel da entidade detentora 
                ndedRow = null;

                cota = GisaDataSetHelper.GetInstance().SFRDUFCota.NewSFRDUFCotaRow();
                df = GisaDataSetHelper.GetInstance().SFRDUFDescricaoFisica.NewSFRDUFDescricaoFisicaRow();
                dp = GisaDataSetHelper.GetInstance().SFRDDatasProducao.NewSFRDDatasProducaoRow();
                ce = GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.NewSFRDConteudoEEstruturaRow();

                frm.Text = "Criar " + tnrRow.Designacao;
                if (CreateNew && ((frmMain)TopLevelControl).isSuportPanel && !PersistencyHelper.hasCurrentDatasetChanges())
                    frm.EntidadeDetentoraList = edsNdRRows;

                frm.ReloadData();
                foreach (GISADataset.NivelDesignadoRow row in frm.cbEntidadeDetentora.Items)
                {
                    if (row.ID == ShowUFDialog_edID)
                    {
                        frm.cbEntidadeDetentora.SelectedItem = row;
                        break;
                    }
                }

                frm.txtDesignacao.Text = ShowUFDialog_designacao;
                frm.txtDesignacao.SelectAll();
            }
            else
            {
                if (ufList.SelectedItems.Count == 0)
                {
                    return;
                }
                if (((GISADataset.NivelRow)(ufList.SelectedItems[0].Tag)).RowState == DataRowState.Detached)
                {
                    MessageBox.Show("Não é possível editar a unidade física selecionada " + System.Environment.NewLine + "uma vez que foi apagada por outro utilizador.", "Edição de unidades físicas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                argsPC.Operation = PersistencyHelper.AddEditUFPreConcArguments.Operations.Edit;
                // nivel
                nufRow = (GISADataset.NivelRow)(ufList.SelectedItems[0].Tag);
                argsPC.nivelUFRowID = nufRow.ID;
                // nivelDesignado
                ndufRow = nufRow.GetNivelDesignadoRows()[0];
                argsPC.ndufRowID = ndufRow.ID;
                // RelacaoHierarquica
                rhufRow = nufRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0];
                argsPC.rhufRowID = rhufRow.ID;
                argsPC.rhufRowIDUpper = rhufRow.IDUpper;
                // NivelUnidadeFisicaRow
                if (ndufRow.GetNivelUnidadeFisicaRows().Length > 0)
                    nufufRow = ndufRow.GetNivelUnidadeFisicaRows()[0];
                else
                {
                    // via conversão de dados esta linha pode não ser criada, mas pela aplicação é garantido que a row é criada
                    nufufRow = GisaDataSetHelper.GetInstance().NivelUnidadeFisica.NewNivelUnidadeFisicaRow();
                    nufufRow.GuiaIncorporacao = "";
                    nufufRow.NivelDesignadoRow = ndufRow;
                    nufufRow.isDeleted = 0;
                    GisaDataSetHelper.GetInstance().NivelUnidadeFisica.AddNivelUnidadeFisicaRow(nufufRow);
                }
                argsPC.nufufRowID = nufufRow.ID;

                // nivel da entidade detentora
                ndedRow = rhufRow.NivelRowByNivelRelacaoHierarquicaUpper.GetNivelDesignadoRows()[0];

                frm.Text = "Editar " + tnrRow.Designacao;
                frm.ReloadData();
                frm.EntidadeDetentora = ndedRow;

                frm.NivelDesignado = ndufRow;

                frm.Codigo = nufRow.Codigo;

                frdufRow = nufRow.GetFRDBaseRows().First();
            }

			switch (frm.ShowDialog())
			{
				case DialogResult.OK:
					ndufRow.Designacao = frm.Designacao;

                    List<string> uaAssociadas = new List<string>();

					if (CreateNew)
					{
						Trace.WriteLine("A criar unidade física...");
						nufRow.TipoNivelRow = tnrRow.TipoNivelRow;
						nufRow.Codigo = frm.Codigo;
						nufRow.CatCode = "NVL";

						ndufRow.NivelRow = nufRow;
						ndufRow.Designacao = frm.Designacao;
						ShowUFDialog_edID = frm.EntidadeDetentora.ID;
						ShowUFDialog_designacao = frm.Designacao;

						rhufRow.NivelRowByNivelRelacaoHierarquica = nufRow;
                        rhufRow.TipoNivelRelacionadoRow = tnrRow;
						rhufRow["InicioAno"] = DBNull.Value;
						rhufRow["InicioMes"] = DBNull.Value;
						rhufRow["InicioDia"] = DBNull.Value;
						rhufRow["FimAno"] = DBNull.Value;
						rhufRow["FimMes"] = DBNull.Value;
						rhufRow["FimDia"] = DBNull.Value;
						rhufRow.NivelRowByNivelRelacaoHierarquicaUpper = frm.EntidadeDetentora.NivelRow;

						nufufRow.NivelDesignadoRow = ndufRow;

						frdufRow.NivelRow = nufRow;
						frdufRow.NotaDoArquivista = string.Empty;
						frdufRow.TipoFRDBaseRow = (GISADataset.TipoFRDBaseRow)(GisaDataSetHelper.GetInstance().TipoFRDBase.Select(string.Format("ID={0}", System.Enum.Format(typeof(TipoFRDBase), TipoFRDBase.FRDUnidadeFisica, "D")))[0]);
						frdufRow.RegrasOuConvencoes = string.Empty;

                        cota.FRDBaseRow = frdufRow;
                        cota.Cota = string.Empty;

                        df.FRDBaseRow = frdufRow;
                        df.IDTipoMedida = 1;
                        df.TipoAcondicionamentoRow = (GISADataset.TipoAcondicionamentoRow)(GisaDataSetHelper.GetInstance().TipoAcondicionamento.Select(string.Format("ID={0:d}", TipoAcondicionamento.Pasta))[0]);
                        
                        dp.FRDBaseRow = frdufRow;
                        dp.FimAno = string.Empty;
                        dp.FimMes = string.Empty;
                        dp.FimDia = string.Empty;
                        dp.FimAtribuida = false;
                        dp.InicioAno = string.Empty;
                        dp.InicioMes = string.Empty;
                        dp.InicioDia = string.Empty;
                        dp.InicioAtribuida = false;

                        ce.FRDBaseRow = frdufRow;
                        ce.ConteudoInformacional = string.Empty;
                        ce.Incorporacao = string.Empty;

						GisaDataSetHelper.GetInstance().Nivel.AddNivelRow(nufRow);
						GisaDataSetHelper.GetInstance().NivelDesignado.AddNivelDesignadoRow(ndufRow);
						GisaDataSetHelper.GetInstance().RelacaoHierarquica.AddRelacaoHierarquicaRow(rhufRow);
						GisaDataSetHelper.GetInstance().NivelUnidadeFisica.AddNivelUnidadeFisicaRow(nufufRow);
						GisaDataSetHelper.GetInstance().FRDBase.AddFRDBaseRow(frdufRow);
                        GisaDataSetHelper.GetInstance().SFRDUFCota.AddSFRDUFCotaRow(cota);
                        GisaDataSetHelper.GetInstance().SFRDUFDescricaoFisica.AddSFRDUFDescricaoFisicaRow(df);
                        GisaDataSetHelper.GetInstance().SFRDDatasProducao.AddSFRDDatasProducaoRow(dp);
                        GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.AddSFRDConteudoEEstruturaRow(ce);

						argsPC.nivelUFRowID = nufRow.ID;
						argsPC.ndufRowID = ndufRow.ID;
						argsPC.rhufRowID = rhufRow.ID;
						argsPC.rhufRowIDUpper = rhufRow.IDUpper;
						argsPC.nufufRowID = nufufRow.ID;
						argsPC.frdufRowID = frdufRow.ID;
					}
					else
					{
							// se a entidade detentora da unidade fisica for alterada
							// é necessário actualizar o Codigo
						if (frm.EntidadeDetentora.ID != ndedRow.ID)
						{
                            var uasInfo = new List<UnidadesFisicasHelper.UaInfo>();
                            var res = MessageBox.Show("A entidade detentora foi alterada. Por essa razão será " + "atribuído um novo código a esta unidade física. Deseja prosseguir?", "Edição da unidade física", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            // switch (ConfirmCascadeDeletion("A entidade detentora foi alterada. Por essa razão será " + "atribuído um novo código a esta unidade física. Deseja prosseguir?", "A entidade detentora foi alterada. Por essa razão será " + "atribuído um novo código a esta unidade física " + "e as referências existentes serão perdidas." + "Deseja prosseguir?", nufRow, ref uaAssociadas, ref uasInfo))
                            switch (res)
							{
								case DialogResult.OK:
								case DialogResult.Yes:
									Trace.WriteLine("A editar unidade física...");
									nufRow.Codigo = frm.Codigo;

								        //nova RelacaoHierarquicaRow
									GISADataset.RelacaoHierarquicaRow newRhufRow = null;
									newRhufRow = GisaDataSetHelper.GetInstance().RelacaoHierarquica.NewRelacaoHierarquicaRow();
									newRhufRow.ID = rhufRow.ID;
									newRhufRow.NivelRowByNivelRelacaoHierarquicaUpper = frm.EntidadeDetentora.NivelRow;
									newRhufRow.IDTipoNivelRelacionado = rhufRow.IDTipoNivelRelacionado;
									GisaDataSetHelper.GetInstance().RelacaoHierarquica.AddRelacaoHierarquicaRow(newRhufRow);
									argsPC.newRhufRowID = newRhufRow.ID;
									argsPC.newRhufRowIDUpper = newRhufRow.IDUpper;
									argsPC.nufufRowID = nufufRow.ID;

									    //apagar a relacao antiga
									rhufRow.Delete();
									break;
								case DialogResult.Cancel:
								case DialogResult.No:
										// cancelar toda a operação de edição
                                    MessageBox.Show("A unidade física não foi alterada.", "Edição da unidade física");
										// FIXME: exit sub?
									return;
							}
						}
					}

					argsPS.nivelUFRowID = nufufRow.ID;
					argsPC.psa = argsPS;

                    var postSaveAction = new PostSaveAction();
                    var argsPostSave = new PersistencyHelper.UpdatePermissionsPostSaveArguments();
                    postSaveAction.args = argsPostSave;

                    postSaveAction.postSaveDelegate = delegate(PersistencyHelper.PostSaveArguments postSaveArgs)
                    {
                        if (!postSaveArgs.cancelAction && argsPC.OperationError == PersistencyHelper.AddEditUFPreConcArguments.OperationErrors.NoError)
                        {
                            if (CreateNew && ((frmMain)TopLevelControl).isSuportPanel)
                            {
                                GISADataset.TrusteeUserRow tuAuthorRow = null;
                                if (SessionHelper.GetGisaPrincipal().TrusteeUserAuthor != null && !(SessionHelper.GetGisaPrincipal().TrusteeUserAuthor.RowState == DataRowState.Detached))
                                    tuAuthorRow = SessionHelper.GetGisaPrincipal().TrusteeUserAuthor;
                                var r = GISA.Model.RecordRegisterHelper.CreateFRDBaseDataDeDescricaoRow(frdufRow, SessionHelper.GetGisaPrincipal().TrusteeUserOperator, tuAuthorRow, DateTime.Now);
                                GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao.AddFRDBaseDataDeDescricaoRow(r);
                            }
                            else
                                CurrentContext.RaiseRegisterModificationEvent(frdufRow);
                            PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao,
                                    GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao.Cast<GISADataset.FRDBaseDataDeDescricaoRow>().Where(frd => frd.RowState == DataRowState.Added).ToArray(), postSaveArgs.tran);
                        }
                    };

                    PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save(DelegatesHelper.HandleUF, argsPC, DelegatesHelper.SetCodigo, argsPS, postSaveAction);
                    PersistencyHelper.cleanDeletedData(PersistencyHelper.determinaNuvem("NivelUnidadeFisica"));

					try
					{
						if (CreateNew)
						{
							if (argsPC.OperationError == PersistencyHelper.AddEditUFPreConcArguments.OperationErrors.NewUF)
							{
								MessageBox.Show(argsPC.message, "Criar unidade física", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							}
							else
							{
                                // adicionar unidade física à lista
								ufList.AddNivel(nufRow);

                                if (successfulSave == PersistencyHelper.SaveResult.successful)
                                {
                                    GISA.Search.Updater.updateNivelDocumental(uaAssociadas);
                                    GISA.Search.Updater.updateUnidadeFisica(frdufRow.NivelRow.ID);
                                }
							}
						}
						else
						{
							if (argsPC.OperationError == PersistencyHelper.AddEditUFPreConcArguments.OperationErrors.EditEDAndDesignacao)
							{
								MessageBox.Show(argsPC.message, "Editar unidade física", MessageBoxButtons.OK, MessageBoxIcon.Warning);
								ufList.ClearItemSelection(ufList.SelectedItems[0]);
							}
							else if (argsPC.OperationError == PersistencyHelper.AddEditUFPreConcArguments.OperationErrors.EditNewEd)
							{
								MessageBox.Show(argsPC.message, "Editar unidade física", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							}
							else if (argsPC.OperationError == PersistencyHelper.AddEditUFPreConcArguments.OperationErrors.EditOriginalEd)
							{
								MessageBox.Show(argsPC.message, "Editar unidade física", MessageBoxButtons.OK, MessageBoxIcon.Warning);
									// recarregar a UF de forma a actualizar a sua informação
								ListViewItem lvItem = ufList.SelectedItems[0];
								ufList.ClearItemSelection(lvItem);
								ufList.SelectItem(lvItem);
								ufList.UpdateNivel(ufList.SelectedItems[0]);
							}
							else
							{
								ufList.UpdateNivel(ufList.SelectedItems[0]);

                                frdufRow = ((GISADataset.NivelRow)ufList.SelectedItems[0].Tag).GetFRDBaseRows()[0];

                                if (successfulSave == PersistencyHelper.SaveResult.successful)
                                {
                                    GISA.Search.Updater.updateNivelDocumental(uaAssociadas);
                                    GISA.Search.Updater.updateUnidadeFisica(frdufRow.NivelRow.ID);
                                }

								// force a context update so that lower panel gets updated
								CurrentContext.SetNivelUnidadeFisica(null);
								UpdateContext();
							}
						}
					}
					catch (Exception ex)
					{
						Trace.WriteLine("Exception while refreshing data: " + ex.ToString());
						throw;
					}
					break;
			}
		}

        private DialogResult ConfirmCascadeDeletion(string message, string messageRelations, GISADataset.NivelRow nivelRow, ref List<string> uaAssociadas, ref List<UnidadesFisicasHelper.UaInfo> uaFrdAssociadas)
		{
            string detalhes = GetUFRelatedDataReport(nivelRow, ref uaAssociadas, ref uaFrdAssociadas);
			DialogResult formResult = 0;
			if (detalhes == null || detalhes.Length == 0)
				formResult = MessageBox.Show(message, "Unidade física", MessageBoxButtons.YesNo);
			else
			{
				FormDeletionReport formReport = new FormDeletionReport();
				formReport.Text = "Unidade física";
				formReport.Interrogacao = messageRelations;
				formReport.Detalhes = detalhes;
				formResult = formReport.ShowDialog();
			}
			return formResult;
		}

        private string GetUFRelatedDataReport(GISADataset.NivelRow nivelRow, ref List<string> uaAssociadas, ref List<UnidadesFisicasHelper.UaInfo> uaFrdAssociadas)
		{
            StringBuilder Report = new StringBuilder();
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
				IDataReader dr = null;
				dr = NivelRule.Current.GetUFReport(nivelRow.ID, ho.Connection);

				while (dr.Read())
				{
					Report.Append("  " + dr.GetString(2) + ": " + dr.GetString(3) + System.Environment.NewLine);
                    uaFrdAssociadas.Add(new UnidadesFisicasHelper.UaInfo() { frdID = dr.GetInt64(4), IDTipoNivelRelacionado = dr.GetInt64(5) });
                    if (System.Convert.ToInt64(dr.GetValue(1)) == TipoNivel.DOCUMENTAL)
                        uaAssociadas.Add(dr.GetValue(0).ToString());
				}

                dr.Close();
                dr = null;

				if (Report.Length > 0)
					Report.Insert(0, "Foram encontrados os seguintes níveis associados a esta unidade física:" + System.Environment.NewLine + System.Environment.NewLine);
            }
			finally
			{
                ho.Dispose();
			}

			return Report.ToString();
		}

		private void MasterPanelUnidadesFisicas_StackChanged(frmMain.StackOperation stackOperation, bool isSupport)
		{
			switch (stackOperation)
			{
				case frmMain.StackOperation.Push:
					this.ufList.MultiSelectListView = false;
					if (! isSupport)
						ufList.ReloadList();
					this.ufList.ContextNivelRow = null;
					break;
				case frmMain.StackOperation.Pop:
					/*if (! isSupport)
						ufList.reloadList();*/
					break;
			}
		}

		public override bool UpdateContext()
		{
			return UpdateContext(null);
		}

		public override bool UpdateContext(ListViewItem item)
		{
			ListViewItem selectedItem = null;
			bool successfulSave = false;

			if (item == null)
			{
				if (ufList.SelectedItems.Count == 1)
				{
					// Apesar da contagem de items ser "1" pode acontecer, no caso de 
					// items que tenham sido entretanto eliminados, que o SelectedItems 
					// se encontre vazio. Nesse caso consideramos sempre que não existe selecção.
					try
					{
						selectedItem = ufList.SelectedItems[0];
					}
					catch (ArgumentException)
					{
						selectedItem = null;
					}
				}
			}
			else if (item.ListView != null)
				selectedItem = item;

			if (selectedItem != null)
			{
				successfulSave = CurrentContext.SetNivelUnidadeFisica((GISADataset.NivelRow)selectedItem.Tag, false, this.ufList.MultiSelectListView);
				DelayedRemoveDeletedItems(ufList.Items);
			}
			else
			{
				if (((frmMain)this.TopLevelControl).isSuportPanel)
				{
					// prever a situação onde este painel está a ser usado como suporte na área 
					// de organização de informação e onde é permitida múltipla selecção;
					// neste caso não é necessário que a informação referente aos items selecionados
					// seja carregada
					successfulSave = CurrentContext.SetNivelUnidadeFisica(null, false, true);
				}
				else
					successfulSave = CurrentContext.SetNivelUnidadeFisica(null);
			}
			if (successfulSave)
				updateContextStatusBar();

			return successfulSave;
		}

		private void updateContextStatusBar()
		{
			if (! (MasterPanel.isContextPanel(this)) || ((frmMain)TopLevelControl).isSuportPanel)
				return;

			if (CurrentContext.NivelUnidadeFisica == null || CurrentContext.NivelUnidadeFisica.RowState == DataRowState.Detached)
				((frmMain)TopLevelControl).StatusBarPanelHint.Text = "";
			else
				((frmMain)TopLevelControl).StatusBarPanelHint.Text = "  Unidade física: " + CurrentContext.NivelUnidadeFisica.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].NivelRowByNivelRelacaoHierarquicaUpper.Codigo + "/" + CurrentContext.NivelUnidadeFisica.Codigo;
		}
	}
}