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
using GISA.Controls;
using GISA.SharedResources;

using GISA.Controls.Localizacao;

namespace GISA
{
	public class PanelUFUnidadesDescricao : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelUFUnidadesDescricao() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

            if (!SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsReqEnable())
                this.lstVwNiveisAssoc.Columns.Remove(this.chRequisitado);

			GetExtraResources();

			long[] @params = new long[] {TipoNivel.DOCUMENTAL, TipoNivel.ESTRUTURAL};
			DragDropHandlerNiveis = new NivelDragDrop(lstVwNiveisAssoc, @params);

			//Add any initialization after the InitializeComponent() call
			DragDropHandlerNiveis.AcceptNode += AcceptNode;
			DragDropHandlerNiveis.AcceptItem += AcceptItem;
			UpdateListButtonsState();
			PopulateFiltro();
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
		internal System.Windows.Forms.GroupBox grpDesc;
		internal System.Windows.Forms.ListView lstVwNiveisAssoc;
		internal System.Windows.Forms.Button btnRemove;
		internal System.Windows.Forms.ColumnHeader chCodigo;
		internal System.Windows.Forms.ColumnHeader chDesignacao;
		internal System.Windows.Forms.ColumnHeader chNivelDesc;
		internal System.Windows.Forms.ColumnHeader chDatasProd;
		internal System.Windows.Forms.GroupBox grpFiltro;
		internal System.Windows.Forms.Button btnAplicar;
		internal GISA.Controls.PxComboBox cbTipoNivelRelacionado;
		internal System.Windows.Forms.Label lblTipoNivelRelacionado;
		internal System.Windows.Forms.TextBox txtFiltroDesignacao;
        private ColumnHeader chRequisitado;
        private ColumnHeader chCota;
		internal System.Windows.Forms.Label lblFiltroDesignacao;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PanelUFUnidadesDescricao));
            this.grpDesc = new System.Windows.Forms.GroupBox();
            this.lstVwNiveisAssoc = new System.Windows.Forms.ListView();
            this.chDesignacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCodigo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chNivelDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDatasProd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCota = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRequisitado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRemove = new System.Windows.Forms.Button();
            this.grpFiltro = new System.Windows.Forms.GroupBox();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.cbTipoNivelRelacionado = new GISA.Controls.PxComboBox();
            this.lblTipoNivelRelacionado = new System.Windows.Forms.Label();
            this.txtFiltroDesignacao = new System.Windows.Forms.TextBox();
            this.lblFiltroDesignacao = new System.Windows.Forms.Label();
            this.grpDesc.SuspendLayout();
            this.grpFiltro.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDesc
            // 
            this.grpDesc.AutoSize = true;
            this.grpDesc.Controls.Add(this.lstVwNiveisAssoc);
            this.grpDesc.Controls.Add(this.btnRemove);
            this.grpDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDesc.Location = new System.Drawing.Point(0, 64);
            this.grpDesc.Name = "grpDesc";
            this.grpDesc.Size = new System.Drawing.Size(800, 536);
            this.grpDesc.TabIndex = 62;
            this.grpDesc.TabStop = false;
            this.grpDesc.Text = "Unidades de informação associadas";
            // 
            // lstVwNiveisAssoc
            // 
            this.lstVwNiveisAssoc.AllowDrop = true;
            this.lstVwNiveisAssoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwNiveisAssoc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDesignacao,
            this.chCodigo,
            this.chNivelDesc,
            this.chDatasProd,
            this.chCota,
            this.chRequisitado});
            this.lstVwNiveisAssoc.FullRowSelect = true;
            this.lstVwNiveisAssoc.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstVwNiveisAssoc.HideSelection = false;
            this.lstVwNiveisAssoc.Location = new System.Drawing.Point(8, 20);
            this.lstVwNiveisAssoc.Name = "lstVwNiveisAssoc";
            this.lstVwNiveisAssoc.Size = new System.Drawing.Size(756, 512);
            this.lstVwNiveisAssoc.TabIndex = 65;
            this.lstVwNiveisAssoc.UseCompatibleStateImageBehavior = false;
            this.lstVwNiveisAssoc.View = System.Windows.Forms.View.Details;
            this.lstVwNiveisAssoc.SelectedIndexChanged += new System.EventHandler(this.lstVwNiveisAssoc_SelectedIndexChanged);
            this.lstVwNiveisAssoc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstVwNiveisAssoc_KeyUp);
            // 
            // chDesignacao
            // 
            this.chDesignacao.Text = "Título";
            this.chDesignacao.Width = 301;
            // 
            // chCodigo
            // 
            this.chCodigo.Text = "Código Completo";
            this.chCodigo.Width = 170;
            // 
            // chNivelDesc
            // 
            this.chNivelDesc.Text = "Nivel Descrição";
            this.chNivelDesc.Width = 140;
            // 
            // chDatasProd
            // 
            this.chDatasProd.Text = "Datas Produção";
            this.chDatasProd.Width = 160;
            // 
            // chCota
            // 
            this.chCota.Text = "Cota";
            this.chCota.Width = 200;
            // 
            // chRequisitado
            // 
            this.chRequisitado.Text = "Requisitado";
            this.chRequisitado.Width = 70;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(769, 34);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(24, 24);
            this.btnRemove.TabIndex = 64;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // grpFiltro
            // 
            this.grpFiltro.Controls.Add(this.btnAplicar);
            this.grpFiltro.Controls.Add(this.cbTipoNivelRelacionado);
            this.grpFiltro.Controls.Add(this.lblTipoNivelRelacionado);
            this.grpFiltro.Controls.Add(this.txtFiltroDesignacao);
            this.grpFiltro.Controls.Add(this.lblFiltroDesignacao);
            this.grpFiltro.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFiltro.Location = new System.Drawing.Point(0, 0);
            this.grpFiltro.Name = "grpFiltro";
            this.grpFiltro.Size = new System.Drawing.Size(800, 64);
            this.grpFiltro.TabIndex = 63;
            this.grpFiltro.TabStop = false;
            this.grpFiltro.Text = "Filtro";
            this.grpFiltro.Visible = false;
            // 
            // btnAplicar
            // 
            this.btnAplicar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAplicar.Location = new System.Drawing.Point(728, 32);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(64, 24);
            this.btnAplicar.TabIndex = 4;
            this.btnAplicar.Text = "&Aplicar";
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // cbTipoNivelRelacionado
            // 
            this.cbTipoNivelRelacionado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTipoNivelRelacionado.BackColor = System.Drawing.SystemColors.Window;
            this.cbTipoNivelRelacionado.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbTipoNivelRelacionado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoNivelRelacionado.DropDownWidth = 230;
            this.cbTipoNivelRelacionado.ImageIndexes = ((System.Collections.ArrayList)(resources.GetObject("cbTipoNivelRelacionado.ImageIndexes")));
            this.cbTipoNivelRelacionado.Location = new System.Drawing.Point(472, 32);
            this.cbTipoNivelRelacionado.MaxDropDownItems = 9;
            this.cbTipoNivelRelacionado.Name = "cbTipoNivelRelacionado";
            this.cbTipoNivelRelacionado.Size = new System.Drawing.Size(256, 21);
            this.cbTipoNivelRelacionado.TabIndex = 2;
            // 
            // lblTipoNivelRelacionado
            // 
            this.lblTipoNivelRelacionado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTipoNivelRelacionado.Location = new System.Drawing.Point(472, 16);
            this.lblTipoNivelRelacionado.Name = "lblTipoNivelRelacionado";
            this.lblTipoNivelRelacionado.Size = new System.Drawing.Size(256, 15);
            this.lblTipoNivelRelacionado.TabIndex = 2;
            this.lblTipoNivelRelacionado.Text = "Nível de Descrição";
            // 
            // txtFiltroDesignacao
            // 
            this.txtFiltroDesignacao.AcceptsReturn = true;
            this.txtFiltroDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFiltroDesignacao.Location = new System.Drawing.Point(8, 32);
            this.txtFiltroDesignacao.Name = "txtFiltroDesignacao";
            this.txtFiltroDesignacao.Size = new System.Drawing.Size(456, 20);
            this.txtFiltroDesignacao.TabIndex = 1;
            this.txtFiltroDesignacao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFiltroDesignacao_KeyDown);
            // 
            // lblFiltroDesignacao
            // 
            this.lblFiltroDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFiltroDesignacao.Location = new System.Drawing.Point(8, 16);
            this.lblFiltroDesignacao.Name = "lblFiltroDesignacao";
            this.lblFiltroDesignacao.Size = new System.Drawing.Size(508, 16);
            this.lblFiltroDesignacao.TabIndex = 0;
            this.lblFiltroDesignacao.Text = "Título";
            // 
            // PanelUFUnidadesDescricao
            // 
            this.Controls.Add(this.grpDesc);
            this.Controls.Add(this.grpFiltro);
            this.Name = "PanelUFUnidadesDescricao";
            this.grpDesc.ResumeLayout(false);
            this.grpFiltro.ResumeLayout(false);
            this.grpFiltro.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        

	#endregion

		private void GetExtraResources()
		{
			btnRemove.Image = SharedResourcesOld.CurrentSharedResources.Apagar;
			lstVwNiveisAssoc.SmallImageList = TipoNivelRelacionado.GetImageList();

			base.ParentChanged += PanelIdentificacao_ParentChanged;
		}

		// runs only once. sets tooltip as soon as it's parent appears
		private void PanelIdentificacao_ParentChanged(object sender, System.EventArgs e)
		{
			MultiPanel.CurrentToolTip.SetToolTip(btnRemove, SharedResourcesOld.CurrentSharedResources.ApagarString);
			base.ParentChanged -= PanelIdentificacao_ParentChanged;
		}

		//Vars globais
		private NivelDragDrop DragDropHandlerNiveis;
		private Hashtable detalhes = new Hashtable();
		private string nCod;
		private ArrayList filter = new ArrayList();
		private bool filtered = false;
		private ArrayList ordem = new ArrayList();
		protected GISADataset.NivelRow CurrentNivel; //Nivel da UF!! Não da selecção.

		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{

			IsLoaded = false;
			CurrentNivel = ((GISADataset.FRDBaseRow)CurrentDataRow).NivelRow;

			ordem = FRDRule.Current.LoadUFUnidadesDescricaoData(GisaDataSetHelper.GetInstance(), CurrentNivel.ID, conn);
			detalhes = FRDRule.Current.LoadUFUnidadesDescricaoDetalhe(GisaDataSetHelper.GetInstance(), CurrentNivel.ID, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID, conn);
            if (filtered && filter.Count > 0)
                filter = FRDRule.Current.FilterUFUnidadesDescricao(TextFilterDesignacao, FilterTipoNivelRelacionado, CurrentNivel.ID, conn);

			IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = false;
			RepopulateNiveisAssociados();

			if (Visible)
				OnShowPanel();

			IsPopulated = true;
		}

		private void RepopulateNiveisAssociados()
		{
			lstVwNiveisAssoc.BeginUpdate();
			lstVwNiveisAssoc.Items.Clear();
			GISADataset.TrusteeUserRow cUser = SessionHelper.GetGisaPrincipal().TrusteeUserOperator;

			long[] lUser = {System.Convert.ToInt64(cUser.ID)};
			ArrayList items = new ArrayList();
			GISADataset.SFRDUnidadeFisicaRow sfrdufRow = null;

			if ((filter.Count == 0) && (! filtered))
			{
				foreach (Int64 ID in ordem)
				{
					sfrdufRow = (GISADataset.SFRDUnidadeFisicaRow)(GisaDataSetHelper.GetInstance().SFRDUnidadeFisica.Select(string.Format("IDNivel={0} AND IDFRDBase={1}", CurrentNivel.ID, ID))[0]);
					if (((ArrayList)(detalhes[sfrdufRow.FRDBaseRow.IDNivel]))[9].ToString() == "1")
						items.Add(PopulateAssociacao(sfrdufRow));
				}
			}
			else
			{
				foreach (Int64 ID in filter)
				{
					sfrdufRow = (GISADataset.SFRDUnidadeFisicaRow)(GisaDataSetHelper.GetInstance().SFRDUnidadeFisica.Select(string.Format("IDNivel={0} AND IDFRDBase={1}", CurrentNivel.ID, ID))[0]);
					if (((ArrayList)(detalhes[sfrdufRow.FRDBaseRow.IDNivel]))[9].ToString() == "1")
						items.Add(PopulateAssociacao(sfrdufRow));
				}
			}

			lstVwNiveisAssoc.Items.AddRange((ListViewItem[])(items.ToArray(typeof(ListViewItem))));
			lstVwNiveisAssoc.EndUpdate();
		}

		private ListViewItem PopulateAssociacao(GISADataset.SFRDUnidadeFisicaRow sfrdufRow)
		{
			GISADataset.NivelRow nRow = null;
			GISADataset.TipoNivelRow tnRow = null;
			GISADataset.TipoNivelRelacionadoRow tnrRow = null;

			nRow = sfrdufRow.FRDBaseRow.NivelRow;
			tnRow = nRow.TipoNivelRow;
			tnrRow = (GISADataset.TipoNivelRelacionadoRow)(GisaDataSetHelper.GetInstance().TipoNivelRelacionado.Select(string.Format("ID = {0}", System.Convert.ToString(((ArrayList)(detalhes[sfrdufRow.FRDBaseRow.IDNivel]))[1])))[0]);

            ListViewItem item = new ListViewItem(new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });
			GISADataset.FRDBaseRow frdbaseRow = nRow.GetFRDBaseRows()[0];

            item.SubItems.AddRange(new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });
			item.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageBase(System.Convert.ToInt32(tnrRow.GUIOrder));
            item.StateImageIndex = 0;
			item.SubItems[chCodigo.Index].Text = System.Convert.ToString(((ArrayList)(detalhes[sfrdufRow.FRDBaseRow.IDNivel]))[8]);
			item.SubItems[chDesignacao.Index].Text = System.Convert.ToString(((ArrayList)(detalhes[sfrdufRow.FRDBaseRow.IDNivel]))[0]);
			item.SubItems[chNivelDesc.Index].Text = tnrRow.Designacao;
			item.SubItems[chDatasProd.Index].Text = GISA.Utils.GUIHelper.FormatDate(((ArrayList)(detalhes[sfrdufRow.FRDBaseRow.IDNivel]))[2].ToString(), ((ArrayList)(detalhes[sfrdufRow.FRDBaseRow.IDNivel]))[3].ToString(), ((ArrayList)(detalhes[sfrdufRow.FRDBaseRow.IDNivel]))[4].ToString(), false) + " - " + GISA.Utils.GUIHelper.FormatDate(((ArrayList)(detalhes[sfrdufRow.FRDBaseRow.IDNivel]))[5].ToString(), ((ArrayList)(detalhes[sfrdufRow.FRDBaseRow.IDNivel]))[6].ToString(), ((ArrayList)(detalhes[sfrdufRow.FRDBaseRow.IDNivel]))[7].ToString(), false);
            item.SubItems[chCota.Index].Text = sfrdufRow["Cota"] == DBNull.Value ? "" : sfrdufRow.Cota;

            if (this.lstVwNiveisAssoc.Columns.Contains(this.chRequisitado))
            {
                if (System.Convert.ToBoolean(((ArrayList)(detalhes[sfrdufRow.FRDBaseRow.IDNivel]))[10]))
                    item.SubItems[this.chRequisitado.Index].Text = "Sim";
                else
                    item.SubItems[this.chRequisitado.Index].Text = "Não";
            }
            
			item.Tag = sfrdufRow;
			return item;
		}

		public override void ViewToModel()
		{

		}

		public override void Deactivate()
		{
			this.ClearFilter();
            GUIHelper.GUIHelper.clearField(lstVwNiveisAssoc);
			CurrentNivel = null;
		}

		private void ToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			if (e.Button == MultiPanel.ToolBarButtonAuxList)
				ToggleNiveisSupportPanel(MultiPanel.ToolBarButtonAuxList.Pushed);
			else if (e.Button == MultiPanel.ToolBarButtonFiltro)
				grpFiltro.Visible = MultiPanel.ToolBarButtonFiltro.Pushed;
		}

        private long GetEntidadeDetentoraID(GISADataset.NivelRow nRow)
        {
            Debug.Assert(nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().Length > 0);

            GISADataset.NivelRow nED = nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].NivelRowByNivelRelacaoHierarquicaUpper;

            while (nED.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().Length > 0)
                nED = nED.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].NivelRowByNivelRelacaoHierarquicaUpper;

            return nED.ID;
        }

		private void ToggleNiveisSupportPanel(bool showIt)
		{
			if (showIt)
			{
				// Make sure the button is pushed
				MultiPanel.ToolBarButtonAuxList.Pushed = true;

				// Indicação que um painel está a ser usado como suporte
				((frmMain)this.TopLevelControl).isSuportPanel = true;

				// Show the panel with all unidades fisicas
                ((frmMain)this.TopLevelControl).PushMasterPanel(typeof(MasterPanelPermissoesPlanoClassificacao));

                Debug.Assert(((MasterPanelPermissoesPlanoClassificacao)(((frmMain)this.TopLevelControl).MasterPanel)).nivelNavigator1.MultiSelect);
                //MasterPanelPermissoesPlanoClassificacao mpPPC = (MasterPanelPermissoesPlanoClassificacao)(((frmMain)this.TopLevelControl).MasterPanel);
                //mpPPC.UpdateToolBarButtons();
                //mpPPC.NivelDocumentalListNavigator1.lstVwNiveisDocumentais.MultiSelect = true;
                //if (mpPPC.PanelToggleState == MasterPanelNiveis.ToggleState.Documental)
                //    mpPPC.NivelDocumentalListNavigator1.reloadList();
			}
			else
			{
				// Make sure the button is not pushed            
				MultiPanel.ToolBarButtonAuxList.Pushed = false;

				// Remove the panel with all unidades fisicas
				if (this.TopLevelControl != null)
				{
                    if (((frmMain)this.TopLevelControl).MasterPanel is MasterPanelPermissoesPlanoClassificacao)
					{
						// Indicação que nenhum painel está a ser usado como suporte
						((frmMain)this.TopLevelControl).isSuportPanel = false;
                        ((frmMain)this.TopLevelControl).PopMasterPanel(typeof(MasterPanelPermissoesPlanoClassificacao));
					}
				}
			}
		}

		private void AcceptItem(ListViewItem item)
		{
			nCod = GetCodigoCompleto(item);
			acceptAssociation((GISADataset.NivelRow)(((ListViewItem)item).Tag), nCod);
		}

		private void AcceptNode(GISATreeNode node)
		{
			string codigoCompleto = GetCodigoCompleto(node);
			acceptAssociation(((GISATreeNode)node).NivelRow, codigoCompleto);
		}

		private void acceptAssociation(GISADataset.NivelRow nRow, string codigoCompleto)
		{
			// validar a associação: só se pode associar um nível que pertença à mesma entidade detentadora da unidade física
            //ArrayList entidadesDetentoras = new ArrayList();
            //GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            //try
            //{
            //    entidadesDetentoras = DBAbstractDataLayer.DataAccessRules.UFRule.Current.GetEntidadeDetentoraForNivel(nRow.ID, ho.Connection);
            //}
            //catch (Exception ex)
            //{
            //    Trace.WriteLine(ex);
            //    throw;
            //}
            //finally
            //{
            //    ho.Dispose();
            //}

            //if (! (entidadesDetentoras.Contains(CurrentNivel.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].IDUpper)))
            //{
            //    MessageBox.Show("Não é permitido associar uma unidade de descrição de uma " + System.Environment.NewLine + "entidade detentora diferente da unidade física seleccionada.", "Adicionar Unidades de Descrição", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

			// aceitar o drop apenas se se tratar de um Nivel ainda não associado
			if (nRow.GetFRDBaseRows().Length > 0)
			{
				GISADataset.SFRDUnidadeFisicaRow[] frdufRows = (GISADataset.SFRDUnidadeFisicaRow[])(GisaDataSetHelper.GetInstance().SFRDUnidadeFisica.Select(string.Format("IDFRDBase={0} AND IDNivel={1}", nRow.GetFRDBaseRows()[0].ID, CurrentNivel.ID), "", DataViewRowState.Deleted));
				if (frdufRows.Length > 0)
				{
					frdufRows[0].RejectChanges();
                    AddDetalhe(nRow, nCod);
                    ListViewItem item = PopulateAssociacao(frdufRows[0]);
                    lstVwNiveisAssoc.Items.Insert(0, item);
                    item.EnsureVisible();
				}
				else
				{
					if (GisaDataSetHelper.GetInstance().SFRDUnidadeFisica.Select(string.Format("IDFRDBase={0} AND IDNivel={1}", nRow.GetFRDBaseRows()[0].ID, CurrentNivel.ID)).Length == 0)
					{
						GISADataset.SFRDUnidadeFisicaRow frdufRow = null;
						frdufRow = AssociaNivel(nRow);
						AddDetalhe(nRow, nCod);
						ordem.Insert(0, frdufRow.IDFRDBase);
						ListViewItem item = PopulateAssociacao(frdufRow);
						lstVwNiveisAssoc.Items.Insert(0, item);
						item.EnsureVisible();
					}
				}
			}
			else
			{
				var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
				try
				{
					FRDRule.Current.LoadFRD(GisaDataSetHelper.GetInstance(), nRow.ID, ho.Connection);
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

				if (nRow.GetFRDBaseRows().Length > 0 && nRow.GetFRDBaseRows()[0].isDeleted == 1)
					MessageBox.Show("O nível selecionado foi apagado por outro utilizador " + System.Environment.NewLine + "motivo pelo qual não é possível terminar a associação", "Unidades de Descrição", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				else
				{
					if (nRow.GetFRDBaseRows().Length == 0)
					{
						// Criar uma nova FRD para o nível em questão.
						GISADataset.FRDBaseRow frdRow = GisaDataSetHelper.GetInstance().FRDBase.NewFRDBaseRow();
						frdRow.NivelRow = nRow;
						frdRow.TipoFRDBaseRow = (GISADataset.TipoFRDBaseRow)(GisaDataSetHelper.GetInstance().TipoFRDBase.Select(string.Format("ID={0:d}", TipoFRDBase.FRDOIRecolha))[0]);
						frdRow.NotaDoArquivista = string.Empty;
						frdRow.RegrasOuConvencoes = string.Empty;
						GisaDataSetHelper.GetInstance().FRDBase.AddFRDBaseRow(frdRow);
					}

					GISADataset.SFRDUnidadeFisicaRow frdufRow = null;
					frdufRow = AssociaNivel(nRow);
					AddDetalhe(nRow, codigoCompleto);
					ordem.Insert(0, frdufRow.IDFRDBase);
					ListViewItem item = PopulateAssociacao(frdufRow);
					lstVwNiveisAssoc.Items.Insert(0, item);
					item.EnsureVisible();
				}
			}
		}

		private string GetCodigoCompleto(object element)
		{
            MasterPanelPermissoesPlanoClassificacao mpPPC = (MasterPanelPermissoesPlanoClassificacao)(((frmMain)this.TopLevelControl).MasterPanel);
			string nCod = null;

			if (element is GISATreeNode)
			{
				GISATreeNode node = (GISATreeNode)element;
				ArrayList pathEstrut = ControloNivelList.GetCodigoCompletoCaminhoUnico(node);
				if (pathEstrut.Count == 0)
					nCod = node.NivelRow.Codigo;
				else
					nCod = Nivel.buildPath(pathEstrut);
			}
			else if (element is ListViewItem)
			{
				ListViewItem item = (ListViewItem)element;
                GISADataset.RelacaoHierarquicaRow rhRow = (GISADataset.RelacaoHierarquicaRow)(GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", ((GISADataset.NivelRow)item.Tag).ID, mpPPC.nivelNavigator1.ContextBreadCrumbsPathID))[0]);

				GISADataset.TipoNivelRelacionadoRow tnrRow = TipoNivelRelacionado.GetTipoNivelRelacionadoFromRelacaoHierarquica(rhRow);

                ArrayList pathEstrut = ControloNivelList.GetCodigoCompletoCaminhoUnico(mpPPC.nivelNavigator1.SelectedNode);

                ArrayList pathDoc = mpPPC.nivelNavigator1.GetCodigoCompletoCaminhoUnico(item);
				if (pathDoc.Count == 0 && pathEstrut.Count == 0)
					nCod = tnrRow.Designacao;
				else
					nCod = string.Format("{0}/{1}", Nivel.buildPath(pathEstrut), Nivel.buildPath(pathDoc));
			}
			return nCod;
		}

		private void AddDetalhe(GISADataset.NivelRow nRow, string nCod)
		{
			if (detalhes[nRow.ID] == null)
			{
				ArrayList aux = new ArrayList();
				GISADataset.SFRDDatasProducaoRow sfrddprow = null;
				string ia = null;
				string im = null;
				string id = null;
				string fa = null;
				string fm = null;
				string fd = null;

				if (nRow.GetFRDBaseRows()[0].GetSFRDDatasProducaoRows().Length > 0)
				{
					sfrddprow = (GISADataset.SFRDDatasProducaoRow)(nRow.GetFRDBaseRows()[0].GetSFRDDatasProducaoRows()[0]);

					if (sfrddprow.IsInicioAnoNull())
						ia = "";
					else
						ia = sfrddprow.InicioAno;

					if (sfrddprow.IsInicioMesNull())
						im = "";
					else
						im = sfrddprow.InicioMes;

					if (sfrddprow.IsInicioDiaNull())
						id = "";
					else
						id = sfrddprow.InicioDia;

					if (sfrddprow.IsFimAnoNull())
						fa = "";
					else
						fa = sfrddprow.FimAno;

					if (sfrddprow.IsFimMesNull())
						fm = "";
					else
						fm = sfrddprow.FimMes;

					if (sfrddprow.IsFimDiaNull())
						fd = "";
					else
						fd = sfrddprow.FimDia;
				}
				else
				{
					ia = "";
					im = "";
					id = "";
					fa = "";
					fm = "";
					fd = "";
				}

                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try
                {
                    if (nRow.IDTipoNivel == 3)
                        aux.Add(nRow.GetNivelDesignadoRows()[0].Designacao);
                    else
                    {
                        DBAbstractDataLayer.DataAccessRules.NivelRule.Current.FillNivelControloAutRows(GisaDataSetHelper.GetInstance(), nRow.ID, ho.Connection);
                        
                        if (nRow.GetNivelControloAutRows().Length > 0)
                            aux.Add(nRow.GetNivelControloAutRows()[0].ControloAutRow.GetControloAutDicionarioRows()[0].DicionarioRow.Termo);
                        else if (nRow.GetNivelDesignadoRows().Length > 0)
                            aux.Add(nRow.GetNivelDesignadoRows()[0].Designacao);
                    }

                    aux.Add(nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].TipoNivelRelacionadoRow.ID);
                    aux.Add(ia);
                    aux.Add(im);
                    aux.Add(id);
                    aux.Add(fa);
                    aux.Add(fm);
                    aux.Add(fd);
                    aux.Add(nCod);
                    aux.Add("1"); // Se aparece aqui é porque temos permissao...

                    if (MovimentoRule.Current.estaRequisitado(nRow.ID, ho.Connection))
                        aux.Add(true);
                    else
                        aux.Add(false);
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

				detalhes.Add(nRow.ID, aux);
			}
		}

		private GISADataset.SFRDUnidadeFisicaRow AssociaNivel(GISADataset.NivelRow nRow)
		{
			GISADataset.SFRDUnidadeFisicaRow frdufRow = null;
			frdufRow = GisaDataSetHelper.GetInstance().SFRDUnidadeFisica.NewSFRDUnidadeFisicaRow();
			frdufRow.FRDBaseRow = nRow.GetFRDBaseRows()[0];
			frdufRow.NivelRow = CurrentNivel;
			GisaDataSetHelper.GetInstance().SFRDUnidadeFisica.AddSFRDUnidadeFisicaRow(frdufRow);
			return frdufRow;
		}

		public override void OnShowPanel()
		{
			//Show the button that brings up the support panel
			//and select it by default.
			MultiPanel.ToolBar.ButtonClick += ToolBar_ButtonClick;

			MultiPanel.ToolBarButtonAuxList.Visible = true;
			MultiPanel.ToolBarButtonFiltro.Visible = true;
			if (CurrentNivel != null)
				ToggleNiveisSupportPanel(false);
		}

		public override void OnHidePanel()
		{
			// if seguinte serve exclusivamente para debug
			if (CurrentNivel != null && CurrentNivel.RowState == DataRowState.Detached)
				Debug.WriteLine("OCORREU SITUAÇÃO DE ERRO NO PAINEL UFS ASSOCIADAS. EM PRINCIPIO NINGUEM DEU POR ELE.");

			ToggleNiveisSupportPanel(false);
			//Deactivate Toolbar Buttons
			MultiPanel.ToolBar.ButtonClick -= ToolBar_ButtonClick;
			MultiPanel.ToolBarButtonAuxList.Visible = false;
			MultiPanel.ToolBarButtonFiltro.Visible = false;
		}

		public void btnRemove_Click(object sender, EventArgs e)
		{
			DeleteItem();
		}

		private void lstVwNiveisAssoc_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == Convert.ToInt32(Keys.Delete))
				DeleteItem();
		}

		private void DeleteItem()
		{
            var lst = lstVwNiveisAssoc.SelectedItems.Cast<ListViewItem>()
                .Select(lvi => lvi.Tag).Cast<GISADataset.SFRDUnidadeFisicaRow>()
                .Select(sfrduf => sfrduf.FRDBaseRow.IDNivel).ToList();
            
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                DBAbstractDataLayer.DataAccessRules.NivelRule.Current.LoadNivelRelacoesHierarquicas(GisaDataSetHelper.GetInstance(), lst, ho.Connection);
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

            lstVwNiveisAssoc.SelectedItems.Cast<ListViewItem>().ToList()
                .ForEach(lvi =>
                {
                    var sfrduf = lvi.Tag as GISADataset.SFRDUnidadeFisicaRow;

                    ordem.Remove(sfrduf.IDFRDBase);
                    detalhes.Remove(sfrduf.FRDBaseRow.IDNivel);
                });

            GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwNiveisAssoc);
            
			UpdateListButtonsState();
		}

		private void lstVwNiveisAssoc_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UpdateListButtonsState();
		}

		private void UpdateListButtonsState()
		{
			if (lstVwNiveisAssoc.SelectedItems.Count == 0)
				btnRemove.Enabled = false;
			else
				btnRemove.Enabled = true;
		}

		//Filtro
		public void PopulateFiltro()
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
			SelectFirstTipo();

			cbTipoNivelRelacionado.ImageList = TipoNivelRelacionado.GetImageList();
			cbTipoNivelRelacionado.ImageIndexes.Clear();
			cbTipoNivelRelacionado.ImageIndexes.Add(-1);

			foreach (GISADataset.TipoNivelRelacionadoRow tnrRow in GisaDataSetHelper.GetInstance().TipoNivelRelacionado.Select("ID < 11"))
				cbTipoNivelRelacionado.ImageIndexes.Add(SharedResourcesOld.CurrentSharedResources.NivelImageBase(System.Convert.ToInt32(tnrRow.GUIOrder)));
		}

		private void SelectFirstTipo()
		{
			if (cbTipoNivelRelacionado.Items.Count > 0)
				cbTipoNivelRelacionado.SelectedIndex = 0;
		}

		public void ClearFilter()
		{
			txtFiltroDesignacao.Text = "";
		}

		public string TextFilterDesignacao
		{
			get
			{
				if (txtFiltroDesignacao.Text.Length == 0)
					return string.Empty;
				else
					return DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.sanitizeSearchTerm(txtFiltroDesignacao.Text);
			}
		}

		public long FilterTipoNivelRelacionado
		{
			get
			{
				if (cbTipoNivelRelacionado.SelectedIndex == 0)
					return -1;
				else
					return (long)cbTipoNivelRelacionado.SelectedValue;
			}
		}

        void txtFiltroDesignacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ExecuteFilter();
        }

		private void btnAplicar_Click(object sender, System.EventArgs e)
		{
            ExecuteFilter();
		}

        private void ExecuteFilter()
        {
            PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save();
            PersistencyHelper.cleanDeletedData();
            if (successfulSave == PersistencyHelper.SaveResult.unsuccessful)
                return;
            else if (successfulSave == PersistencyHelper.SaveResult.successful)
                GISA.Search.Updater.updateUnidadeFisica(CurrentNivel.GetFRDBaseRows()[0].NivelRow.ID);

            if ((FilterTipoNivelRelacionado == -1) && (TextFilterDesignacao == string.Empty))
            {
                filtered = false;
                filter.Clear();
                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try
                {
                    ordem = FRDRule.Current.LoadUFUnidadesDescricaoData(GisaDataSetHelper.GetInstance(), CurrentNivel.ID, ho.Connection);
                    detalhes = FRDRule.Current.LoadUFUnidadesDescricaoDetalhe(GisaDataSetHelper.GetInstance(), CurrentNivel.ID, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID, ho.Connection);
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
            }
            else
            {
                filtered = true;
                filter.Clear();
                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try
                {
                    ordem = FRDRule.Current.LoadUFUnidadesDescricaoData(GisaDataSetHelper.GetInstance(), CurrentNivel.ID, ho.Connection);
                    detalhes = FRDRule.Current.LoadUFUnidadesDescricaoDetalhe(GisaDataSetHelper.GetInstance(), CurrentNivel.ID, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID, ho.Connection);
                    filter = FRDRule.Current.FilterUFUnidadesDescricao(TextFilterDesignacao, FilterTipoNivelRelacionado, CurrentNivel.ID, ho.Connection);
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
            }

            RepopulateNiveisAssociados();
        }
	}
}