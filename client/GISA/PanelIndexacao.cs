using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;
using GISA.GUIHelper;

namespace GISA
{
	public class PanelIndexacao : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelIndexacao() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            btnRemove.Click += btnRemove_Click;
            lstVwIndexacao.KeyUp += lstVwIndexacao_KeyUp;
            lstVwIndexacao.SelectedIndexChanged += lstVwIndexacao_SelectedIndexChanged;

			GetExtraResources();
			UpdateButtonState();
			//AddHandler GisaDataSetHelper.GetInstance().ControloAut.RowDeleting, AddressOf CADataRowChangedHandler
			//AddHandler GisaDataSetHelper.GetInstance().ControloAutDicionario.RowChanged, AddressOf CADDataRowChangedHandler
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
		internal System.Windows.Forms.GroupBox grpIndexacao;
		internal System.Windows.Forms.ListView lstVwIndexacao;
		internal System.Windows.Forms.ColumnHeader ColumnHeader1;
		internal System.Windows.Forms.ColumnHeader ColumnHeader2;
		internal System.Windows.Forms.ColumnHeader ColumnHeader3;
		internal System.Windows.Forms.ColumnHeader ColumnHeader4;
		internal System.Windows.Forms.Button btnRemove;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.grpIndexacao = new System.Windows.Forms.GroupBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lstVwIndexacao = new System.Windows.Forms.ListView();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpIndexacao.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpIndexacao
            // 
            this.grpIndexacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpIndexacao.Controls.Add(this.btnRemove);
            this.grpIndexacao.Controls.Add(this.lstVwIndexacao);
            this.grpIndexacao.Location = new System.Drawing.Point(3, 3);
            this.grpIndexacao.Name = "grpIndexacao";
            this.grpIndexacao.Size = new System.Drawing.Size(794, 594);
            this.grpIndexacao.TabIndex = 0;
            this.grpIndexacao.TabStop = false;
            this.grpIndexacao.Text = "Conteúdos";
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(766, 36);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(24, 24);
            this.btnRemove.TabIndex = 2;
            // 
            // lstVwIndexacao
            // 
            this.lstVwIndexacao.AllowDrop = true;
            this.lstVwIndexacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwIndexacao.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader2,
            this.ColumnHeader3,
            this.ColumnHeader4});
            this.lstVwIndexacao.FullRowSelect = true;
            this.lstVwIndexacao.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstVwIndexacao.HideSelection = false;
            this.lstVwIndexacao.Location = new System.Drawing.Point(8, 16);
            this.lstVwIndexacao.Name = "lstVwIndexacao";
            this.lstVwIndexacao.Size = new System.Drawing.Size(754, 570);
            this.lstVwIndexacao.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstVwIndexacao.TabIndex = 1;
            this.lstVwIndexacao.UseCompatibleStateImageBehavior = false;
            this.lstVwIndexacao.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Designação";
            this.ColumnHeader1.Width = 383;
            // 
            // ColumnHeader2
            // 
            this.ColumnHeader2.Text = "Notícia de autoridade";
            this.ColumnHeader2.Width = 126;
            // 
            // ColumnHeader3
            // 
            this.ColumnHeader3.Text = "Validado";
            this.ColumnHeader3.Width = 75;
            // 
            // ColumnHeader4
            // 
            this.ColumnHeader4.Text = "Completo";
            // 
            // PanelIndexacao
            // 
            this.Controls.Add(this.grpIndexacao);
            this.Name = "PanelIndexacao";
            this.grpIndexacao.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

		private void GetExtraResources()
		{
			btnRemove.Image = SharedResourcesOld.CurrentSharedResources.Apagar;

			base.ParentChanged += PanelIndexacao_ParentChanged;
		}

		// runs only once. sets tooltip as soon as it's parent appears
		private void PanelIndexacao_ParentChanged(object sender, System.EventArgs e)
		{
			MultiPanel.CurrentToolTip.SetToolTip(btnRemove, SharedResourcesOld.CurrentSharedResources.ApagarString);
			base.ParentChanged -= PanelIndexacao_ParentChanged;
		}

		private GISADataset.FRDBaseRow CurrentFRDBase;
		private ControloAutDragDrop DragDropHandler1;

		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			CurrentFRDBase = (GISADataset.FRDBaseRow)CurrentDataRow;

			if (DragDropHandler1 == null)
			{
				DragDropHandler1 = new ControloAutDragDrop(lstVwIndexacao, new TipoNoticiaAut[] {TipoNoticiaAut.Ideografico, TipoNoticiaAut.Onomastico, TipoNoticiaAut.ToponimicoGeografico}, CurrentFRDBase);
                DragDropHandler1.AddControloAut += AddControloAut;
			}
			else
				DragDropHandler1.FRDBase = CurrentFRDBase;

			string QueryFilter = "IDFRDBase=" + CurrentFRDBase.ID.ToString();
			string WhereQueryFilter = "WHERE " + QueryFilter;

			FRDRule.Current.LoadIndexacaoData(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, conn);

            OnShowPanel();
			IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = false;
			lstVwIndexacao.Items.Clear();

			GisaDataSetHelper.VisitIndexFRDCA(CurrentFRDBase, FilterControloAut);

			UpdateButtonState();
			IsPopulated = true;
		}

		public override void ViewToModel()
		{

		}

		public override void Deactivate()
		{
            OnHidePanel();
		}

		private GISADataset.IndexFRDCARow TempIndexFRDCA;
		private void FilterControloAut(GISADataset.IndexFRDCARow IndexFRDCA)
		{
			if (IndexFRDCA.ControloAutRow.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.Ideografico) || IndexFRDCA.ControloAutRow.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.Onomastico) || IndexFRDCA.ControloAutRow.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.ToponimicoGeografico) )
			{
				TempIndexFRDCA = IndexFRDCA;
				GisaDataSetHelper.VisitControloAutDicionario(IndexFRDCA.ControloAutRow, DisplayEntidadeProdutora);
			}
		}

		private void DisplayEntidadeProdutora(GISADataset.ControloAutDicionarioRow ControloAutDicionario)
		{
			if (ControloAutDicionario.IDTipoControloAutForma == Convert.ToInt64(TipoControloAutForma.FormaAutorizada))
			{
                ListViewItem tempWith1 = lstVwIndexacao.Items.Add(ControloAutDicionario.DicionarioRow.Termo);
				tempWith1.SubItems.Add(ControloAutDicionario.ControloAutRow. TipoNoticiaAutRow.Designacao.ToString());
				tempWith1.SubItems.Add(TranslationHelper.FormatBoolean(ControloAutDicionario.ControloAutRow.Autorizado));
				tempWith1.SubItems.Add(TranslationHelper.FormatBoolean(ControloAutDicionario.ControloAutRow.Completo));
				tempWith1.Tag = TempIndexFRDCA;
			}
		}

		private void AddControloAut(object Sender, ListViewItem ListViewItem)
		{
			GISADataset.IndexFRDCARow IndexFRDCARow = null;
			IndexFRDCARow = (GISADataset.IndexFRDCARow)ListViewItem.Tag;
			ListViewItem.SubItems.Add(IndexFRDCARow.ControloAutRow. TipoNoticiaAutRow.Designacao.ToString());
			ListViewItem.SubItems.Add(TranslationHelper.FormatBoolean(IndexFRDCARow.ControloAutRow.Autorizado));
			ListViewItem.SubItems.Add(TranslationHelper.FormatBoolean(IndexFRDCARow.ControloAutRow.Completo));
		}

		private GISADataset.ControloAutRow[] GetlstVwIndexacaoControloAutRowArray()
		{
			GISADataset.ControloAutRow[] cas = null;
			cas = new GISADataset.ControloAutRow[lstVwIndexacao.SelectedItems.Count];

			int i = 0;
			foreach (ListViewItem li in lstVwIndexacao.SelectedItems)
			{
				cas[i] = ((GISADataset.IndexFRDCARow)li.Tag).ControloAutRow;
				i = i + 1;
			}
			return cas;
		}

		public void btnRemove_Click(object sender, EventArgs e)
		{
            deleteIndex();
		}

		private void lstVwIndexacao_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == Convert.ToInt32(Keys.Delete))
                deleteIndex();    
		}

        private void deleteIndex()
        {
            GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwIndexacao);
        }

		private void lstVwIndexacao_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UpdateButtonState();
		}

		private void UpdateButtonState()
		{
			if (lstVwIndexacao.SelectedItems.Count == 0)
				btnRemove.Enabled = false;
			else
				btnRemove.Enabled = true;
		}

		private void ToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			if (e.Button == MultiPanel.ToolBarButtonAuxList)
				ToggleControloAutoridade(MultiPanel.ToolBarButtonAuxList.Pushed);
		}

		private void ToggleControloAutoridade(bool showIt)
		{
			if (showIt)
			{
				if (! (((frmMain)this.TopLevelControl).isSuportPanel))
				{
					// Make sure the button is pushed
					MultiPanel.ToolBarButtonAuxList.Pushed = true;

					// Indicação que um painel está a ser usado como suporte
					((frmMain)this.TopLevelControl).isSuportPanel = true;

                    // Show the panel with all controlos autoridade
					((frmMain)this.TopLevelControl).PushMasterPanel(typeof(MasterPanelControloAut));

                    MasterPanelControloAut master = (MasterPanelControloAut)(((frmMain)this.TopLevelControl).MasterPanel);

                    master.caList.AllowedNoticiaAut(TipoNoticiaAut.Ideografico, TipoNoticiaAut.Onomastico, TipoNoticiaAut.ToponimicoGeografico);
                    master.caList.AllowedNoticiaAutLocked = true;
                    master.caList.ReloadList();

                    master.UpdateSupoortPanelPermissions("GISA.FRDCATipologiaInformacional");
                    master.UpdateToolBarButtons();
				}
			}
			else
			{
				// Make sure the button is not pushed            
				MultiPanel.ToolBarButtonAuxList.Pushed = false;

				// Remove the panel with all controlos autoridade
				if (this.TopLevelControl != null)
				{
					if (((frmMain)this.TopLevelControl). MasterPanel is MasterPanelControloAut)
					{
						// Indicação que um painel está a ser usado como suporte
						((frmMain)this.TopLevelControl).isSuportPanel = false;

                        //TODO: INSTANT C# TODO TASK: The return type of the tempWith2 variable must be corrected.
                        //ORIGINAL LINE: With DirectCast(DirectCast(this.TopLevelControl, frmMain). MasterPanel, MasterPanelControloAut)
                        MasterPanelControloAut tempWith2 = (MasterPanelControloAut)(((frmMain)this.TopLevelControl).MasterPanel);

						tempWith2.caList.AllowedNoticiaAutLocked = false;
						((frmMain)this.TopLevelControl).PopMasterPanel(typeof(MasterPanelControloAut));
					}
				}
			}
		}

		public override void OnShowPanel()
		{
			//Show the button that brings up the panel with controlos
			//autoridade and select it by default.
			if (! (((frmMain)this.TopLevelControl).isSuportPanel) && !TbBAuxListEventAssigned)
			{
				MultiPanel.ToolBar.ButtonClick += ToolBar_ButtonClick;
				MultiPanel.ToolBarButtonAuxList.Visible = true;

                TbBAuxListEventAssigned = true;
			}
		}

		public override void OnHidePanel()
		{
			//Deactivate Toolbar Buttons
            if (TbBAuxListEventAssigned)
            {
                MultiPanel.ToolBar.ButtonClick -= ToolBar_ButtonClick;
                MultiPanel.ToolBarButtonAuxList.Visible = false;

                ToggleControloAutoridade(false);

                TbBAuxListEventAssigned = false;
            }
		}


		private void CADDataRowChangedHandler(object sender, DataRowChangeEventArgs e)
		{
			if (e.Action == DataRowAction.Add)
			{
				GISADataset.ControloAutDicionarioRow changedCadRow = null;
				changedCadRow = (GISADataset.ControloAutDicionarioRow)e.Row;
				GISADataset.ControloAutRow changedCaRow = null;
				changedCaRow = changedCadRow.ControloAutRow;

				Debug.WriteLine("[CONTROLO AUT DICIONARIO ADDED]" + new StackFrame(true).ToString());

				Trace.Assert(e.Row is GISADataset.ControloAutDicionarioRow);

				foreach (ListViewItem item in lstVwIndexacao.Items)
				{
					GISADataset.IndexFRDCARow ifrdcaRow = null;
					ifrdcaRow = (GISADataset.IndexFRDCARow)item.Tag;

					if (changedCaRow == ifrdcaRow.ControloAutRow)
					{
						// actualizar a interface com a forma autorizada do CA em causa
						foreach (GISADataset.ControloAutDicionarioRow cadRow in GisaDataSetHelper.GetInstance().ControloAutDicionario.Select(string.Format("IDControloAut={0}", changedCaRow.ID)))
						{
							if (cadRow.TipoControloAutFormaRow.ID == Convert.ToInt64(TipoControloAutForma.FormaAutorizada))
							{
								lstVwIndexacao.BeginUpdate();
								item.SubItems[0].Text = cadRow.DicionarioRow.Termo;
								lstVwIndexacao.EndUpdate();
								break;
							}
						}
					}
				}
			}
		}

		private void CADataRowChangedHandler(object sender, DataRowChangeEventArgs e)
		{			
			if (e.Action == DataRowAction.Delete)
			{		
				Trace.Assert(e.Row is GISADataset.ControloAutRow);

				GISADataset.ControloAutRow changedCaRow = null;
				changedCaRow = (GISADataset.ControloAutRow)e.Row;

				foreach (ListViewItem item in lstVwIndexacao.Items)
				{
					GISADataset.IndexFRDCARow ifrdcaRow = null;
					ifrdcaRow = (GISADataset.IndexFRDCARow)item.Tag;

					// prever os que ainda nao tivessem sido persistidos e os que tinham sido persistidos mas foram agora eliminados
					if (ifrdcaRow.RowState == DataRowState.Detached || System.Convert.ToInt32(changedCaRow["ID", DataRowVersion.Original]) == System.Convert.ToInt32(ifrdcaRow["IDControloAut", DataRowVersion.Original]))
					{
						//GisaDataSetHelper.GetInstance().IndexFRDCA.Rows.Remove(ifrdcaRow)
						// a row ja foi eliminada no modelo de dados. elimina-se agora da interface
						item.Remove();
					}
				}
			}
		}
	}
} //end of root namespace