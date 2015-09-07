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

namespace GISA
{
	public class PanelCARelacoes : GISA.GISAPanel
	{

		//Private WithEvents DragDropHandler1 As ControloAutDragDrop

	#region  Windows Form Designer generated code 

		public PanelCARelacoes() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            btnRemove.Click += btnRemove_Click;
            lstVwRelacoes.KeyUp += lstVwRelacoes_KeyUp;
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            lstVwRelacoes.SelectedIndexChanged += lstVwRelacoes_SelectedIndexChanged;

			//DragDropHandler1 = New ControloAutDragDrop(lstVwRelacoes, _
			//    New TipoNoticiaAut() { _
			//    TipoNoticiaAut.EntidadeProdutora}, _
			//    CurrentControloAut)

			GetExtraResources();
			RefreshButtonState();
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
		internal System.Windows.Forms.GroupBox grpRelacoes;
		internal System.Windows.Forms.GroupBox grpDescricaoRelacao;
		internal System.Windows.Forms.TextBox txtDescricaoRelacao;
		internal System.Windows.Forms.ListView lstVwRelacoes;
		internal System.Windows.Forms.ColumnHeader colFormaAutorizada;
		internal System.Windows.Forms.ColumnHeader colIdentificador;
		internal System.Windows.Forms.ColumnHeader colCategoria;
		internal System.Windows.Forms.ColumnHeader colDescricao;
		//    Friend WithEvents btnAdd As System.Windows.Forms.Button
		internal System.Windows.Forms.Button btnRemove;
		internal System.Windows.Forms.Button btnEdit;
		internal System.Windows.Forms.Button btnAdd;
		internal System.Windows.Forms.ColumnHeader colDataInicio;
		internal System.Windows.Forms.ColumnHeader colDataFim;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.grpRelacoes = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.lstVwRelacoes = new System.Windows.Forms.ListView();
            this.colFormaAutorizada = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIdentificador = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCategoria = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDataInicio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDataFim = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescricao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpDescricaoRelacao = new System.Windows.Forms.GroupBox();
            this.txtDescricaoRelacao = new System.Windows.Forms.TextBox();
            this.grpRelacoes.SuspendLayout();
            this.grpDescricaoRelacao.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpRelacoes
            // 
            this.grpRelacoes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRelacoes.Controls.Add(this.btnAdd);
            this.grpRelacoes.Controls.Add(this.btnRemove);
            this.grpRelacoes.Controls.Add(this.btnEdit);
            this.grpRelacoes.Controls.Add(this.lstVwRelacoes);
            this.grpRelacoes.Location = new System.Drawing.Point(8, 2);
            this.grpRelacoes.Name = "grpRelacoes";
            this.grpRelacoes.Size = new System.Drawing.Size(788, 262);
            this.grpRelacoes.TabIndex = 1;
            this.grpRelacoes.TabStop = false;
            this.grpRelacoes.Text = "3.1. - 3.4. Relações";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(759, 40);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(24, 24);
            this.btnAdd.TabIndex = 2;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(759, 104);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(24, 24);
            this.btnRemove.TabIndex = 4;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEdit.Location = new System.Drawing.Point(759, 72);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(24, 24);
            this.btnEdit.TabIndex = 3;
            // 
            // lstVwRelacoes
            // 
            this.lstVwRelacoes.AllowDrop = true;
            this.lstVwRelacoes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwRelacoes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFormaAutorizada,
            this.colIdentificador,
            this.colCategoria,
            this.colDataInicio,
            this.colDataFim,
            this.colDescricao});
            this.lstVwRelacoes.FullRowSelect = true;
            this.lstVwRelacoes.HideSelection = false;
            this.lstVwRelacoes.Location = new System.Drawing.Point(8, 16);
            this.lstVwRelacoes.Name = "lstVwRelacoes";
            this.lstVwRelacoes.Size = new System.Drawing.Size(748, 238);
            this.lstVwRelacoes.TabIndex = 1;
            this.lstVwRelacoes.UseCompatibleStateImageBehavior = false;
            this.lstVwRelacoes.View = System.Windows.Forms.View.Details;
            // 
            // colFormaAutorizada
            // 
            this.colFormaAutorizada.Text = "Forma autorizada";
            this.colFormaAutorizada.Width = 170;
            // 
            // colIdentificador
            // 
            this.colIdentificador.Text = "Identificador único";
            this.colIdentificador.Width = 130;
            // 
            // colCategoria
            // 
            this.colCategoria.Text = "Categoria";
            this.colCategoria.Width = 197;
            // 
            // colDataInicio
            // 
            this.colDataInicio.Text = "Data início";
            this.colDataInicio.Width = 80;
            // 
            // colDataFim
            // 
            this.colDataFim.Text = "Data fim";
            this.colDataFim.Width = 80;
            // 
            // colDescricao
            // 
            this.colDescricao.Text = "Descrição";
            this.colDescricao.Width = 180;
            // 
            // grpDescricaoRelacao
            // 
            this.grpDescricaoRelacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDescricaoRelacao.Controls.Add(this.txtDescricaoRelacao);
            this.grpDescricaoRelacao.Location = new System.Drawing.Point(8, 264);
            this.grpDescricaoRelacao.Name = "grpDescricaoRelacao";
            this.grpDescricaoRelacao.Size = new System.Drawing.Size(788, 329);
            this.grpDescricaoRelacao.TabIndex = 2;
            this.grpDescricaoRelacao.TabStop = false;
            this.grpDescricaoRelacao.Text = "3.3 Descrição da relação";
            // 
            // txtDescricaoRelacao
            // 
            this.txtDescricaoRelacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescricaoRelacao.Enabled = false;
            this.txtDescricaoRelacao.Location = new System.Drawing.Point(8, 16);
            this.txtDescricaoRelacao.Multiline = true;
            this.txtDescricaoRelacao.Name = "txtDescricaoRelacao";
            this.txtDescricaoRelacao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescricaoRelacao.Size = new System.Drawing.Size(772, 305);
            this.txtDescricaoRelacao.TabIndex = 5;
            // 
            // PanelCARelacoes
            // 
            this.Controls.Add(this.grpDescricaoRelacao);
            this.Controls.Add(this.grpRelacoes);
            this.Name = "PanelCARelacoes";
            this.grpRelacoes.ResumeLayout(false);
            this.grpDescricaoRelacao.ResumeLayout(false);
            this.grpDescricaoRelacao.PerformLayout();
            this.ResumeLayout(false);

		}

	#endregion

		private void GetExtraResources()
		{
			btnAdd.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
			btnEdit.Image = SharedResourcesOld.CurrentSharedResources.Editar;
			btnRemove.Image = SharedResourcesOld.CurrentSharedResources.Apagar;

			base.ParentChanged += PanelCARelacoes_ParentChanged;
		}

		// runs only once. sets tooltip as soon as it's parent appears
		private void PanelCARelacoes_ParentChanged(object sender, System.EventArgs e)
		{
			MultiPanel.CurrentToolTip.SetToolTip(btnAdd, SharedResourcesOld.CurrentSharedResources.AdicionarString);
			MultiPanel.CurrentToolTip.SetToolTip(btnEdit, SharedResourcesOld.CurrentSharedResources.EditarString);
			MultiPanel.CurrentToolTip.SetToolTip(btnRemove, SharedResourcesOld.CurrentSharedResources.ApagarString);

			base.ParentChanged -= PanelCARelacoes_ParentChanged;
		}

		private GISADataset.ControloAutRow CurrentControloAut;
		private string QueryFilter;
		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			CurrentControloAut = (GISADataset.ControloAutRow)CurrentDataRow;

			DBAbstractDataLayer.DataAccessRules.NivelRule.Current.LoadControloAutChildren(CurrentControloAut.ID, GisaDataSetHelper.GetInstance(), conn);
            DBAbstractDataLayer.DataAccessRules.NivelRule.Current.LoadNivelControloAutChildrenByCA(CurrentControloAut.ID, GisaDataSetHelper.GetInstance(), conn);
            DBAbstractDataLayer.DataAccessRules.NivelRule.Current.LoadControloAutParents(CurrentControloAut.ID, GisaDataSetHelper.GetInstance(), conn);
            DBAbstractDataLayer.DataAccessRules.NivelRule.Current.LoadNivelControloAutParentsByCA(CurrentControloAut.ID, GisaDataSetHelper.GetInstance(), conn);

			IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = false;
			lstVwRelacoes.Items.Clear();

			// Apresentar relações não hierarquicas
			string carId = CurrentControloAut.ID.ToString();
			QueryFilter = "IDControloAut=" + carId + " OR IDControloAutAlias=" + carId;
			foreach (GISADataset.ControloAutRelRow caRel in GisaDataSetHelper.GetInstance().ControloAutRel.Select(QueryFilter, "InicioAno, InicioMes, InicioDia, FimAno, FimMes, FimDia"))
			{
				if (caRel.TipoControloAutRelRow.ID != Convert.ToInt64(TipoControloAutRel.Instituicao)) //System.Enum.Format(GetType(TipoControloAutRel), TipoControloAutRel.Instituicao, "D") Then
				{
					GISADataset.ControloAutRow OtherControloAut = null;
					if (caRel.ControloAutRowByControloAutControloAutRelAlias.ID != CurrentControloAut.ID)
						OtherControloAut = caRel.ControloAutRowByControloAutControloAutRelAlias;
					else
						OtherControloAut = caRel.ControloAutRowByControloAutControloAutRel;
					foreach (GISADataset.ControloAutDicionarioRow cad in OtherControloAut.GetControloAutDicionarioRows())
					{
						if (cad.IDTipoControloAutForma == Convert.ToInt64(TipoControloAutForma.FormaAutorizada))
						{
							ListViewItem tempWith1 = lstVwRelacoes.Items.Add(cad.DicionarioRow.Termo);
							tempWith1.Tag = caRel;
							if (cad.ControloAutRow.IsChaveColectividadeNull())
								tempWith1.SubItems.Add(string.Empty);
							else
								tempWith1.SubItems.Add(cad.ControloAutRow.ChaveColectividade);
							string rezDesTipoRel = null;
							if (caRel.ControloAutRowByControloAutControloAutRelAlias.ID != CurrentControloAut.ID)
								rezDesTipoRel = caRel.TipoControloAutRelRow.DesignacaoInversa;
							else
								rezDesTipoRel = caRel.TipoControloAutRelRow.Designacao;
							tempWith1.SubItems.Add(rezDesTipoRel);
                            tempWith1.SubItems.Add(GUIHelper.GUIHelper.FormatStartDate(caRel));
                            tempWith1.SubItems.Add(GUIHelper.GUIHelper.FormatEndDate(caRel));
                            tempWith1.SubItems.Add(GUIHelper.GUIHelper.ClipText(caRel.Descricao));
						}
					}
				}
			}

			// Apresentar relações hierarquicas
			GISADataset.NivelRow nRow = null;
			GISADataset.NivelControloAutRow[] ncaRows = null;
			GISADataset.NivelControloAutRow ncaRow = null;
			ncaRows = CurrentControloAut.GetNivelControloAutRows();
			if (ncaRows.Length == 0)
			{
				MasterPanelControloAut.CreateAssociatedNivel(CurrentControloAut, ref nRow, ref ncaRow);
				try
				{
					PersistencyHelper.save();
					PersistencyHelper.cleanDeletedData();
				}
				catch (Exception ex)
				{
					Trace.WriteLine(ex);
					throw;
				}
			}
			else
			{
				ncaRow = ncaRows[0];
				nRow = ncaRow.NivelRow;
			}

			foreach (GISADataset.RelacaoHierarquicaRow rhRow in GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} OR IDUpper={0}", nRow.ID), "InicioAno, InicioMes, InicioDia, FimAno, FimMes, FimDia"))
			{
				// Só adicionar relações hierárquicas existentes entre níveis estruturais. Este teste não seria suficiente no caso de existirem níveis estruturais orgânicos relacionados com níveis estruturais temático-funcionais, mas tal não poderá acontecer em qualquer situação.
				if (rhRow.NivelRowByNivelRelacaoHierarquica.TipoNivelRow.ID == TipoNivel.ESTRUTURAL && rhRow.NivelRowByNivelRelacaoHierarquicaUpper.TipoNivelRow.ID == TipoNivel.ESTRUTURAL)
				{
					GISADataset.ControloAutRow caRow = null;
					caRow = rhRow.NivelRowByNivelRelacaoHierarquica.GetNivelControloAutRows()[0].ControloAutRow;
					// Se estivermos a ver o extremo errado da relação hierarquica trocamos para o outro extremo
					if (CurrentControloAut == caRow)
						caRow = rhRow.NivelRowByNivelRelacaoHierarquicaUpper.GetNivelControloAutRows()[0].ControloAutRow;

					GISADataset.ControloAutDicionarioRow cadRow = null;
					cadRow = ControloAutHelper.getFormaAutorizada(caRow);

					ListViewItem newItem = lstVwRelacoes.Items.Add(cadRow.DicionarioRow.Termo);
					newItem.Tag = rhRow;
					AddRelacaoHierarquicaToList(newItem);
				}
			}

			RefreshButtonState();
			PopulateDescricaoText();
			IsPopulated = true;
		}

		public override void ViewToModel()
		{

		}

		public override void Deactivate()
		{

		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
            DeleteRelacao(lstVwRelacoes);
		}

		private void lstVwRelacoes_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == Convert.ToInt32(Keys.Delete))
                DeleteRelacao((ListView)sender);
		}

        private void DeleteRelacao(ListView lstVw)
        {
            var IDNivel = new List<string>();
            var IDControloAut = new List<string>();
            var frdRow = default(GISADataset.FRDBaseRow);
            long IDTipoNivelRelacionado = -1;
            foreach (ListViewItem item in lstVw.SelectedItems)
            {
                if (item.Tag.GetType() == typeof(GISADataset.ControloAutRelRow))
                {
                    var carRow = (GISADataset.ControloAutRelRow)item.Tag;
                    IDControloAut.Add(carRow.IDControloAut.ToString());
                    IDControloAut.Add(carRow.IDControloAutAlias.ToString());
                }
                else
                {
                    var rhRow = (GISADataset.RelacaoHierarquicaRow)item.Tag;
                    IDControloAut.Add(rhRow.NivelRowByNivelRelacaoHierarquica.GetNivelControloAutRows()[0].IDControloAut.ToString());
                    IDControloAut.Add(rhRow.NivelRowByNivelRelacaoHierarquicaUpper.GetNivelControloAutRows()[0].IDControloAut.ToString());
                    IDNivel.Add(rhRow.ID.ToString());
                    frdRow = rhRow.NivelRowByNivelRelacaoHierarquica.GetFRDBaseRows().Single();
                    IDTipoNivelRelacionado = rhRow.IDTipoNivelRelacionado;
                }
            }

            GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVw);

            var postSaveAction = new PostSaveAction();
            PersistencyHelper.UpdatePermissionsPostSaveArguments args = new PersistencyHelper.UpdatePermissionsPostSaveArguments();
            postSaveAction.args = args;

            postSaveAction.postSaveDelegate = delegate(PersistencyHelper.PostSaveArguments postSaveArgs)
            {
                if (!postSaveArgs.cancelAction)
                {
                    // registar eliminação
                    ((frmMain)TopLevelControl).CurrentContext.RaiseRegisterModificationEvent(CurrentControloAut);
                    var caRegRow = GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao.Cast<GISADataset.ControloAutDataDeDescricaoRow>().Single(r => r.RowState == DataRowState.Added);

                    PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao,
                            GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao.Cast<GISADataset.ControloAutDataDeDescricaoRow>().Where(r => r.RowState == DataRowState.Added).ToArray(), postSaveArgs.tran);

                    if (frdRow != null)
                    {
                        var nvlRegRow = RecordRegisterHelper
                                    .CreateFRDBaseDataDeDescricaoRow(frdRow,
                                        caRegRow.TrusteeUserRowByTrusteeUserControloAutDataDeDescricao,
                                        caRegRow.TrusteeUserRowByTrusteeUserControloAutDataDeDescricaoAuthority,
                                        caRegRow.DataAutoria);
                        nvlRegRow.DataEdicao = caRegRow.DataEdicao;
                        nvlRegRow.IDTipoNivelRelacionado = IDTipoNivelRelacionado;

                        GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao.AddFRDBaseDataDeDescricaoRow(nvlRegRow);

                        PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao,
                                GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao.Cast<GISADataset.FRDBaseDataDeDescricaoRow>().Where(r => r.RowState == DataRowState.Added).ToArray(), postSaveArgs.tran);
                    }
                }
            };
            var successfulSave = PersistencyHelper.save(postSaveAction);

            PersistencyHelper.cleanDeletedData();

            if (successfulSave == PersistencyHelper.SaveResult.successful)
            {
                GISA.Search.Updater.updateProdutor(IDControloAut);
                GISA.Search.Updater.updateNivelDocumentalComProdutores(IDNivel);
            }

            
        }

		public static string GetRelConstraint(GISADataset.ControloAutRow a, GISADataset.ControloAutRow b)
		{
			return "(IDControloAut=" + a.ID.ToString() + " AND IDControloAutAlias=" + b.ID.ToString() + ")";
		}

		private void AddRelacaoHierarquicaToList(ListViewItem ListViewItem)
		{
			GISADataset.RelacaoHierarquicaRow rhRow = null;
			rhRow = (GISADataset.RelacaoHierarquicaRow)ListViewItem.Tag;
			GISADataset.ControloAutRow caRow = null;
			GISADataset.ControloAutRow caUpperRow = null;
			caRow = rhRow.NivelRowByNivelRelacaoHierarquica.GetNivelControloAutRows()[0].ControloAutRow;
			caUpperRow = rhRow.NivelRowByNivelRelacaoHierarquicaUpper.GetNivelControloAutRows()[0].ControloAutRow;
			GISADataset.TipoControloAutRelRow tcarHierarquicaRow = null;
			tcarHierarquicaRow = (GISADataset.TipoControloAutRelRow)(GisaDataSetHelper.GetInstance().TipoControloAutRel.Select(string.Format("ID={0:d}", TipoControloAutRel.Hierarquica))[0]);


			if (caRow.ID != CurrentControloAut.ID)
			{
				if (caRow.IsChaveColectividadeNull())
					ListViewItem.SubItems.Add(string.Empty);
				else
					ListViewItem.SubItems.Add(caRow.ChaveColectividade);
			}
			else
			{
				if (caUpperRow.IsChaveColectividadeNull())
					ListViewItem.SubItems.Add(string.Empty);
				else
					ListViewItem.SubItems.Add(caUpperRow.ChaveColectividade);
			}
			string tipoRelacao = null;
			if (caRow.ID != CurrentControloAut.ID)
				tipoRelacao = string.Format("{0} ({1})", tcarHierarquicaRow.DesignacaoInversa, rhRow.TipoNivelRelacionadoRow.Designacao);
			else
				tipoRelacao = string.Format("{0} ({1})", tcarHierarquicaRow.Designacao, rhRow.TipoNivelRelacionadoRow.Designacao);

			ListViewItem.SubItems.Add(tipoRelacao);
            ListViewItem.SubItems.Add(GUIHelper.GUIHelper.FormatStartDate(rhRow));
            ListViewItem.SubItems.Add(GUIHelper.GUIHelper.FormatEndDate(rhRow));
            ListViewItem.SubItems.Add(GUIHelper.GUIHelper.ClipText(GISA.Model.GisaDataSetHelper.GetDBNullableText(rhRow, "Descricao")));
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			((frmMain)TopLevelControl).EnterWaitMode();
			try
			{
				// se após uma gravação o contexto ficar inválido
				if (CurrentControloAut.RowState == DataRowState.Detached || CurrentControloAut.isDeleted == 1)
				{
					MessageBox.Show("A notícia de autoridade que está a utilizar foi eliminada " + Environment.NewLine + "por outro utilizador não sendo por isso possível continuar a " + Environment.NewLine + "editá-la.", "Edição de Entidade Produtora", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
				}

				try
				{
					GisaDataSetHelper.ManageDatasetConstraints(false);

					IDbConnection conn = GisaDataSetHelper.GetConnection();
					try
					{
						conn.Open();
						LoadData(CurrentControloAut, conn);
					}
					finally
					{
						conn.Close();
					}

					GisaDataSetHelper.ManageDatasetConstraints(true);
				}
				catch (ConstraintException ex)
				{
					Debug.Assert(false, ex.Message);
				}
			}
			finally
			{
				((frmMain)TopLevelControl).LeaveWaitMode();
			}

			FormControloAutRel frm = null;
			((frmMain)TopLevelControl).EnterWaitMode();
			try
			{
                frm = new FormControloAutRel(CurrentControloAut, ((frmMain)TopLevelControl).MasterPanel);
				frm.caList.AllowedNoticiaAut(TipoNoticiaAut.EntidadeProdutora);
				frm.caList.ReloadList();
				frm.caList.txtFiltroDesignacao.Clear();

				if (frm.caList.Items.Count > 0)
					frm.caList.SelectItem(frm.caList.Items[0]);
			}
			finally
			{
				((frmMain)TopLevelControl).LeaveWaitMode();
			}

			RetrieveSelection(frm);
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{

			DataRow relRow = null;
			relRow = (DataRow)(lstVwRelacoes.SelectedItems[0].Tag);

			// verificar se tanto a entidade produtora seleccionada como contexto como a relação seleccionada
			// para edição não foram apagadas concorrentemente por outro utilizador
			if (CurrentControloAut.RowState == DataRowState.Detached)
			{
				MessageBox.Show("Não é possível editar qualquer relação da entidade produtora " + System.Environment.NewLine + "selecionada uma vez que foi apagada por outro utilizador.", "Edição de Relações", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				lstVwRelacoes.SelectedItems.Clear();
				lstVwRelacoes.Items.Clear();
				return;
			}
			else if (relRow.RowState == DataRowState.Detached)
			{
				MessageBox.Show("Não é possível editar a relação selecionada uma " + System.Environment.NewLine + "vez que foi apagada por outro utilizador.", "Edição de Relações", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				ListViewItem lvItem = lstVwRelacoes.SelectedItems[0];
				lstVwRelacoes.SelectedItems.Clear();
				lstVwRelacoes.Items.Remove(lvItem);
				return;
			}

			GISADataset.ControloAutRow caRow = null;
			GISADataset.ControloAutRelRow carRow = null;
			GISADataset.RelacaoHierarquicaRow rhRow = null;

			FormControloAutRel frm = null;

			if (relRow is GISADataset.ControloAutRelRow)
			{
                frm = new FormControloAutRel(CurrentControloAut, ((frmMain)TopLevelControl).MasterPanel);

				carRow = (GISADataset.ControloAutRelRow)relRow;
				if (carRow.ControloAutRowByControloAutControloAutRel == CurrentControloAut)
					caRow = carRow.ControloAutRowByControloAutControloAutRelAlias;
				else if (carRow.ControloAutRowByControloAutControloAutRelAlias == CurrentControloAut)
				{
					caRow = carRow.ControloAutRowByControloAutControloAutRel;
					// nas edições, ao visualizar uma relação pelo outro extremo é necessário alterar a designação da relação apresentada
					frm.relacaoCA.cbTipoControloAutRel.DisplayMember = "Designacao";
				}
			}
			else if (relRow is GISADataset.RelacaoHierarquicaRow)
			{
				rhRow = (GISADataset.RelacaoHierarquicaRow)relRow;
				caRow = rhRow.NivelRowByNivelRelacaoHierarquica.GetNivelControloAutRows()[0].ControloAutRow;
				// Se estivermos a ver o extremo errado da relação hierarquica trocamos para o outro extremo
				if (CurrentControloAut == caRow)
				{
					caRow = rhRow.NivelRowByNivelRelacaoHierarquicaUpper.GetNivelControloAutRows()[0].ControloAutRow;

                    frm = new FormControloAutRel(caRow, ((frmMain)TopLevelControl).MasterPanel);
					// nas edições, ao visualizar uma relação pelo outro extremo é necessário alterar a designação da relação apresentada
					frm.relacaoCA.cbTipoControloAutRel.DisplayMember = "Designacao";
				}
				else
                    frm = new FormControloAutRel(CurrentControloAut, ((frmMain)TopLevelControl).MasterPanel);
			}

			GISADataset.ControloAutDicionarioRow cadRow = null;
			string termo = null;
			cadRow = ControloAutHelper.getFormaAutorizada(caRow);
			termo = cadRow.DicionarioRow.Termo;

			frm.ContextRel = relRow;
			frm.caList.AllowedNoticiaAut(TipoNoticiaAut.EntidadeProdutora);
			frm.caList.SelectedItems.Clear();
			frm.caList.txtFiltroDesignacao.Text = termo.Trim();
			frm.caList.ReloadList();
            frm.caList.SelectItem(cadRow);

			frm.caList.txtFiltroDesignacao.Clear();
			// popular datas
			frm.relacaoCA.dtRelacaoInicio.ValueYear = GisaDataSetHelper.GetDBNullableText(relRow, "InicioAno");
			frm.relacaoCA.dtRelacaoInicio.ValueMonth = GisaDataSetHelper.GetDBNullableText(relRow, "InicioMes");
			frm.relacaoCA.dtRelacaoInicio.ValueDay = GisaDataSetHelper.GetDBNullableText(relRow, "InicioDia");
			frm.relacaoCA.dtRelacaoFim.ValueYear = GisaDataSetHelper.GetDBNullableText(relRow, "FimAno");
			frm.relacaoCA.dtRelacaoFim.ValueMonth = GisaDataSetHelper.GetDBNullableText(relRow, "FimMes");
			frm.relacaoCA.dtRelacaoFim.ValueDay = GisaDataSetHelper.GetDBNullableText(relRow, "FimDia");
			frm.relacaoCA.txtDescricao.Text = GisaDataSetHelper.GetDBNullableText(relRow, "Descricao");

			// Encontrar e selecionar o TipoControloAutRel correcto
			if (relRow is GISADataset.ControloAutRelRow)
			{
				foreach (object drView in frm.relacaoCA.cbTipoControloAutRel.Items)
					if (((GISADataset.TipoControloAutRelRow)drView).ID == carRow.TipoControloAutRelRow.ID)
						frm.relacaoCA.cbTipoControloAutRel.SelectedItem = drView;
			}
			else if (relRow is GISADataset.RelacaoHierarquicaRow)
			{
				foreach (object drView in frm.relacaoCA.cbTipoControloAutRel.Items)
				{
					if (((GISADataset.TipoControloAutRelRow)drView).ID == Convert.ToInt64(TipoControloAutRel.Hierarquica))
					{
						frm.relacaoCA.cbTipoControloAutRel.SelectedItem = drView;
						break;
					}
				}

				bool foundTipoNivel = false;
				foreach (TipoNivelRelacionado.PossibleSubNivel subNivel in frm.relacaoCA.cbTipoNivel.Items)
				{
					if (subNivel.SubIDTipoNivelRelacionado == rhRow.IDTipoNivelRelacionado)
					{
						frm.relacaoCA.cbTipoNivel.SelectedItem = subNivel;
						foundTipoNivel = true;
						break;
					}
				}

				// O tipo de nivel já definido pode nao ser encontrado entre os possiveis 
				// se o tipo de nivel do pai tiver entretanto também ele mudado. Nesse caso 
				// adicionamos mais esse tipo de nivel de forma a ser mantida a selecção 
				// anteriormente existente.
				if (! foundTipoNivel)
				{
					ArrayList data = null;
					data = (ArrayList)frm.relacaoCA.cbTipoNivel.DataSource;
					TipoNivelRelacionado.PossibleSubNivel subTipoNivel = null;
					string inicioAno = string.Empty;
					string inicioMes = string.Empty;
					string inicioDia = string.Empty;
					string fimAno = string.Empty;
					string fimMes = string.Empty;
					string fimDia = string.Empty;

					if (! rhRow.IsInicioAnoNull())
						inicioAno = rhRow.InicioAno;
					if (! rhRow.IsInicioMesNull())
						inicioMes = rhRow.InicioMes;
					if (! rhRow.IsInicioDiaNull())
						inicioDia = rhRow.InicioDia;

					if (! rhRow.IsFimAnoNull())
						fimAno = rhRow.FimAno;
					if (! rhRow.IsFimMesNull())
						fimMes = rhRow.FimMes;
                    if (! rhRow.IsFimDiaNull())
						fimDia = rhRow.FimDia;

					subTipoNivel = new TipoNivelRelacionado.PossibleSubNivel(rhRow.IDTipoNivelRelacionado, rhRow.TipoNivelRelacionadoRow.Designacao, inicioAno, inicioMes, inicioDia, fimAno, fimMes, fimDia, true);
					int idx = data.Add(subTipoNivel);

					frm.relacaoCA.cbTipoNivel.ImageIndexes.Add(SharedResourcesOld.CurrentSharedResources.NivelImageBase(System.Convert.ToInt32(subTipoNivel.SubIDTipoNivelRelacionado)));
					frm.relacaoCA.cbTipoNivel.DataSource = null;
					frm.relacaoCA.cbTipoNivel.DataSource = data;
					frm.relacaoCA.cbTipoNivel.DisplayMember = "DesignacaoComposta";
					frm.relacaoCA.cbTipoNivel.SelectedItem = subTipoNivel;
				}
			}
			RetrieveSelection(frm, relRow);
		}

		// O parametro relRow tanto poderá ser um controloAutRelRow como um RelacaoHierarquicaRow
		private void RetrieveSelection(FormControloAutRel frm)
		{
			RetrieveSelection(frm, null);
		}

		private void RetrieveSelection(FormControloAutRel frm, DataRow relRow)
		{
			try
			{
				if (relRow != null) // edição
					frm.SetEditMode();

                frm.ShowDialog();

				GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
				try
				{
					LoadData(CurrentControloAut, ho.Connection);
					ModelToView();
				}
				finally
				{
					ho.Dispose();
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw ex;
			}
		}

		private void UpdateEditedItem(ListViewItem item)
		{
			// prever a situação de se ter tentado editar uma relação entretanto apagada por
			// outro utilizador (nesse caso, a interface não é actualizada)
			if (((DataRow)item.Tag).RowState == DataRowState.Detached)
				return;

			if (item.Tag is GISADataset.ControloAutRelRow)
			{
				GISADataset.ControloAutRelRow carRow = null;
				carRow = (GISADataset.ControloAutRelRow)item.Tag;

                if (carRow.ControloAutRowByControloAutControloAutRelAlias.IsChaveColectividadeNull())
					item.SubItems[colIdentificador.Index].Text = string.Empty;
				else
					item.SubItems[colIdentificador.Index].Text = carRow.ControloAutRowByControloAutControloAutRelAlias.ChaveColectividade;
				item.SubItems[colCategoria.Index].Text = carRow.TipoControloAutRelRow.Designacao;
				item.SubItems[colDataInicio.Index].Text = GISA.Utils.GUIHelper.FormatDate(carRow.InicioAno, carRow.InicioMes, carRow.InicioDia);
				item.SubItems[colDataFim.Index].Text = GISA.Utils.GUIHelper.FormatDate(carRow.FimAno, carRow.FimMes, carRow.FimDia);
                item.SubItems[colDescricao.Index].Text = GUIHelper.GUIHelper.ClipText(carRow.Descricao);
				txtDescricaoRelacao.Text = carRow.Descricao;
			}
			else if (item.Tag is GISADataset.RelacaoHierarquicaRow)
			{
				GISADataset.RelacaoHierarquicaRow rhRow = null;
				rhRow = (GISADataset.RelacaoHierarquicaRow)item.Tag;
				GISADataset.ControloAutRow relatedCA = null;
				relatedCA = rhRow.NivelRowByNivelRelacaoHierarquica.GetNivelControloAutRows()[0].ControloAutRow;
				GISADataset.TipoControloAutRelRow tcarHierarquicaRow = null;
				tcarHierarquicaRow = (GISADataset.TipoControloAutRelRow)(GisaDataSetHelper.GetInstance().TipoControloAutRel.Select(string.Format("ID={0:d}", TipoControloAutRel.Hierarquica))[0]);

				if (relatedCA.IsChaveColectividadeNull())
					item.SubItems[colIdentificador.Index].Text = string.Empty;
				else
					item.SubItems[colIdentificador.Index].Text = relatedCA.ChaveColectividade;

				// nao actualizar a categoria uma vez que este valor nunca poderá ser editado nas relações hierarquicas
				item.SubItems[colDataInicio.Index].Text = GISA.Utils.GUIHelper.FormatDate(rhRow.InicioAno, rhRow.InicioMes, rhRow.InicioDia);
				item.SubItems[colDataFim.Index].Text = GISA.Utils.GUIHelper.FormatDate(rhRow.FimAno, rhRow.FimMes, rhRow.FimDia);
                item.SubItems[colDescricao.Index].Text = GUIHelper.GUIHelper.ClipText(rhRow.Descricao);
				txtDescricaoRelacao.Text = rhRow.Descricao;
			}
		}

		private void lstVwRelacoes_SelectedIndexChanged(object sender, EventArgs e)
		{

			PopulateDescricaoText();
			RefreshButtonState();
		}

		private void RefreshButtonState()
		{
			if (lstVwRelacoes.SelectedItems.Count == 1)
			{
				btnEdit.Enabled = true;
				btnRemove.Enabled = true;
			}
			else if (lstVwRelacoes.SelectedItems.Count > 1)
			{
				btnEdit.Enabled = false;
				btnRemove.Enabled = true;
			}
			else
			{
				btnEdit.Enabled = false;
				btnRemove.Enabled = false;
			}
		}

		private void PopulateDescricaoText()
		{
			if (lstVwRelacoes.SelectedItems.Count == 1)
			{
				object row = lstVwRelacoes.SelectedItems[0].Tag;
				if (((DataRow)row).RowState == DataRowState.Detached)
					return;

				if (row is GISADataset.ControloAutRelRow)
				{
					GISADataset.ControloAutRelRow carRow = null;
					carRow = (GISADataset.ControloAutRelRow)row;
					txtDescricaoRelacao.Text = carRow.Descricao;
				}
				else if (row is GISADataset.RelacaoHierarquicaRow)
				{
					GISADataset.RelacaoHierarquicaRow rhRow = null;
					rhRow = (GISADataset.RelacaoHierarquicaRow)row;
					if (rhRow.IsDescricaoNull())
						txtDescricaoRelacao.Text = string.Empty;
					else
						txtDescricaoRelacao.Text = rhRow.Descricao;
				}
			}
			else
				txtDescricaoRelacao.Text = string.Empty;
		}
	}
}