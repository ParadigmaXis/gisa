using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using GISA.Model;
using System.Collections.Generic;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;
using GISA.GUIHelper;

using GISA.Controls;
using GISA.Controls.ControloAut;


namespace GISA
{
	public class MasterPanelControloAut : GISA.MasterPanel
	{

	#region  Windows Form Designer generated code 

		public MasterPanelControloAut() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            ToolBar.ButtonClick += Toolbar_ButtonClick;
            caList.BeforeNewListSelection += caList_BeforeNewListSelection;
            caList.ItemDrag += caList_ItemDrag;
            base.StackChanged += MasterPanelControloAut_StackChanged;
            MenuItemPrintNoticiasAutoridades.Click += MenuItemPrint_Click;
            caList.KeyUpDelete += new EventHandler(caList_KeyUpDelete);

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
		internal System.Windows.Forms.ToolBarButton btnFiltro;
		internal System.Windows.Forms.ToolBarButton btnApagar;
		internal System.Windows.Forms.ToolBarButton btnSeparator2;
		internal System.Windows.Forms.ToolBarButton btnSeparator3;
		internal System.Windows.Forms.ToolBarButton btnCriar;
		internal System.Windows.Forms.ToolBarButton btnEditar;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonReports;
		internal System.Windows.Forms.ContextMenu ContextMenuPrint;
		internal System.Windows.Forms.MenuItem MenuItemPrintNoticiasAutoridades;
		internal ControloAutList caList;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.btnFiltro = new System.Windows.Forms.ToolBarButton();
			this.btnCriar = new System.Windows.Forms.ToolBarButton();
			this.btnApagar = new System.Windows.Forms.ToolBarButton();
			this.btnSeparator2 = new System.Windows.Forms.ToolBarButton();
			this.btnSeparator3 = new System.Windows.Forms.ToolBarButton();
			this.btnEditar = new System.Windows.Forms.ToolBarButton();
			this.ContextMenuPrint = new System.Windows.Forms.ContextMenu();
			this.MenuItemPrintNoticiasAutoridades = new System.Windows.Forms.MenuItem();
			this.ToolBarButtonReports = new System.Windows.Forms.ToolBarButton();
			this.caList = new ControloAutList();
			this.pnlToolbarPadding.SuspendLayout();
			this.SuspendLayout();
			//
			//lblFuncao
			//
			this.lblFuncao.Location = new System.Drawing.Point(0, 0);
			this.lblFuncao.Text = "Controlo de autoridade";
			//
			//ToolBar
			//
			this.ToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {this.btnCriar, this.btnEditar, this.btnApagar, this.btnSeparator2, this.btnFiltro, this.btnSeparator3, this.ToolBarButtonReports});
			this.ToolBar.ImageList = null;
			//
			//pnlToolbarPadding
			//
			this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
			//
			//btnFiltro
			//
			this.btnFiltro.ImageIndex = 3;
			this.btnFiltro.Name = "btnFiltro";
			this.btnFiltro.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			//
			//btnCriar
			//
			this.btnCriar.ImageIndex = 0;
			this.btnCriar.Name = "btnCriar";
			this.btnCriar.ToolTipText = "Criar nova notícia de autoridade";
			//
			//btnApagar
			//
			this.btnApagar.Enabled = false;
			this.btnApagar.ImageIndex = 2;
			this.btnApagar.Name = "btnApagar";
			//
			//btnSeparator2
			//
			this.btnSeparator2.Name = "btnSeparator2";
			this.btnSeparator2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			//
			//btnSeparator3
			//
			this.btnSeparator3.Name = "btnSeparator3";
			this.btnSeparator3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			//
			//btnEditar
			//
			this.btnEditar.ImageIndex = 1;
			this.btnEditar.Name = "btnEditar";
			//
			//ContextMenuPrint
			//
			this.ContextMenuPrint.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {this.MenuItemPrintNoticiasAutoridades});
			//
			//MenuItemPrintNoticiasAutoridades
			//
			this.MenuItemPrintNoticiasAutoridades.Index = 0;
			this.MenuItemPrintNoticiasAutoridades.Text = "&Noticias Autoridade";
			//
			//ToolBarButtonReports
			//
			this.ToolBarButtonReports.DropDownMenu = this.ContextMenuPrint;
			this.ToolBarButtonReports.ImageIndex = 3;
			this.ToolBarButtonReports.Name = "ToolBarButtonReports";
			//
			//caList
			//
			this.caList.AllowedNoticiaAutLocked = false;
			this.caList.Dock = System.Windows.Forms.DockStyle.Fill;
			//this.caList.ListHandler = null;
			this.caList.Location = new System.Drawing.Point(0, 52);
			this.caList.Name = "caList";
			//this.caList.originalLabel = "Notícias de autoridade encontradas";
			this.caList.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.caList.Size = new System.Drawing.Size(600, 228);
			this.caList.TabIndex = 2;
			//
			//MasterPanelControloAut
			//
			this.Controls.Add(this.caList);
			this.Name = "MasterPanelControloAut";
			this.Controls.SetChildIndex(this.lblFuncao, 0);
			this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
			this.Controls.SetChildIndex(this.caList, 0);
			this.pnlToolbarPadding.ResumeLayout(false);
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		private void GetExtraResources()
		{
			ToolBar.ImageList = SharedResourcesOld.CurrentSharedResources.CAManipulacaoImageList;
			btnCriar.ImageIndex = 0;
			btnEditar.ImageIndex = 1;
			btnApagar.ImageIndex = 2;
			btnFiltro.ImageIndex = 3;
			ToolBarButtonReports.ImageIndex = 4;

			string[] strs = SharedResourcesOld.CurrentSharedResources.CAManipulacaoStrings;
			btnCriar.ToolTipText = strs[0];
			btnEditar.ToolTipText = strs[1];
			btnApagar.ToolTipText = strs[2];
			btnFiltro.ToolTipText = strs[3];
			ToolBarButtonReports.ToolTipText = strs[4];
		}


		public override void UpdateToolBarButtons()
		{
			UpdateToolBarButtons(null);
		}

		public override void UpdateToolBarButtons(ListViewItem item)
		{
			ToolBarButtonReports.Enabled = true;
			btnCriar.Enabled = AllowCreate;

			ListViewItem selectedItem = null;

			if (item != null && item.ListView != null)
				selectedItem = item;
			else if (item == null && caList.SelectedItems.Count == 1)
				selectedItem = caList.SelectedItems[0];

			if (selectedItem == null || ((DataRow)selectedItem.Tag).RowState == DataRowState.Detached || ! (AreAllSelectedItemsFormasAutorizadas(selectedItem)))
			{
				btnEditar.Enabled = false;
				btnApagar.Enabled = false;
			}
			else
			{
				btnEditar.Enabled = AllowEdit;
				btnApagar.Enabled = AllowDelete;
			}

			// situação onde o painel está a ser usado como suporte
            var top = (frmMain)TopLevelControl;
            if (top == null || top.MasterPanelCount > 1)
                ToolBarButtonReports.Enabled = false;
		}

        public override void UpdateSupoortPanelPermissions(string classFullName)
        {
            // por definição não é permitido criar entidades produtoras em paineis de suporte
            if (classFullName.Equals("GISA.FRDCAEntidadeProdutora"))
            {
                AllowCreate = false;
                AllowEdit = false;
                AllowDelete = false;
            }
            else
                base.UpdateSupoortPanelPermissions(classFullName);
        }

        private bool AreAllSelectedItemsFormasAutorizadas()
		{
			return AreAllSelectedItemsFormasAutorizadas(null);
		}

		private bool AreAllSelectedItemsFormasAutorizadas(ListViewItem item)
		{
			GISADataset.ControloAutDicionarioRow cadRow = null;

			if (item == null || (item != null && item.ListView == null))
			{
				foreach (ListViewItem lvi in caList.SelectedItems)
				{
					cadRow = (GISADataset.ControloAutDicionarioRow)lvi.Tag;
					if (! (cadRow.IDTipoControloAutForma == Convert.ToInt64(TipoControloAutForma.FormaAutorizada)))
						return false;
				}
			}
			else
			{
				cadRow = (GISADataset.ControloAutDicionarioRow)item.Tag;
				if (! (cadRow.IDTipoControloAutForma == Convert.ToInt64(TipoControloAutForma.FormaAutorizada)))
					return false;
			}
			return true;
		}

		private void Toolbar_ButtonClick(object Sender, ToolBarButtonClickEventArgs e)
		{
			if (e.Button == btnFiltro)
				ClickBtnFiltro();
			else if (e.Button == btnCriar)
				ClickBtnCriar();
			else if (e.Button == btnEditar)
				ClickBtnEditar();
			else if (e.Button == btnApagar)
				ClickBtnApagar();
			else if (e.Button == ToolBarButtonReports)
			{
				if (e.Button.DropDownMenu != null && e.Button.DropDownMenu is ContextMenu)
					((ContextMenu)e.Button.DropDownMenu).Show(ToolBar, new System.Drawing.Point(e.Button.Rectangle.X, e.Button.Rectangle.Y + e.Button.Rectangle.Height));
			}
		}

        private void caList_KeyUpDelete(object sender, EventArgs e)
        {
            if (caList.SelectedItems.Count == 1)
                ClickBtnApagar();
        }

		private void ClickBtnFiltro()
		{
			caList.FilterVisible = btnFiltro.Pushed;
			caList.AllowedNoticiaAut(GetCurrentTipoNoticiaAut());
		}

		private TipoNoticiaAut[] GetCurrentTipoNoticiaAut()
		{
			frmMain main = (frmMain)TopLevelControl;
			LumiSoft.UI.Controls.WOutlookBar.WOutlookBar ob = (LumiSoft.UI.Controls.WOutlookBar.WOutlookBar)(main.PanelOutlookBar.Controls[0]);

			if (ob.StuckenItem.Bar.Index == 0)
			{
                //TODO: INSTANT C# TODO TASK: The return type of the tempWith1 variable must be corrected.
                //ORIGINAL LINE: With DirectCast(ob.StuckenItem.Tag, GISADataset.TipoFunctionRow)
                GISADataset.TipoFunctionRow tempWith1 = (GISADataset.TipoFunctionRow)ob.StuckenItem.Tag;
					//FIXME use function group
				switch (tempWith1.idx)
				{
					case 1: // Entidade produtora
						return new TipoNoticiaAut[] {TipoNoticiaAut.EntidadeProdutora};
					case 2: // Conteúdos
						return new TipoNoticiaAut[] {TipoNoticiaAut.Onomastico, TipoNoticiaAut.Ideografico, TipoNoticiaAut.ToponimicoGeografico};
				}
			}
			else
			{
				GISAPanel activePanel = null;
				activePanel = findPanel(main.SlavePanel);
				if (activePanel is PanelContexto) // entidades produtoras
					return new TipoNoticiaAut[] {TipoNoticiaAut.EntidadeProdutora};
				else if (activePanel is PanelIndexacao) // indexacao de conteudos
					return new TipoNoticiaAut[] {TipoNoticiaAut.Onomastico, TipoNoticiaAut.Ideografico, TipoNoticiaAut.ToponimicoGeografico};
			}

			return new TipoNoticiaAut[] {TipoNoticiaAut.TipologiaInformacional};
		}

		private GISAPanel findPanel(GISAControl ctrl)
		{
			foreach (Panel pnl in ctrl.Controls)
			{
				if (pnl is Panel)
				{
					if (pnl is GISAPanelScroller)
					{
						foreach (Control innerCtrl in pnl.Controls)
						{
							if (innerCtrl is GISAPanel && innerCtrl.Visible)
								return (GISAPanel)innerCtrl;
						}
						return null;
					}
					else
						return null;
				}
			}
			return null;
		}

		private void ClickBtnCriar()
		{
            ((frmMain)TopLevelControl).EnterWaitMode();

            if (((frmMain)TopLevelControl).isSuportPanel && !PersistencyHelper.hasCurrentDatasetChanges())
            {
                if (GetCurrentTipoNoticiaAut()[0] == TipoNoticiaAut.EntidadeProdutora)
                {
                    MessageBox.Show("Não é permitido criar entidades produtores a partir de um painel de suporte." + System.Environment.NewLine +
                        "Selecione a área de menu referente às entidades produtoras para efetuar a operação", "Criação de entidade produtora", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try {
                    Trace.WriteLine("Saving before creating new CA in suport panel...");
                    PersistencyHelper.save();
                    PersistencyHelper.cleanDeletedData();
                }
                catch (Exception) {
                    MessageBox.Show("Ocorreu um erro ao tentar abrir o formulário de criação." + System.Environment.NewLine + "Por favor contacte o administrador de sistema.", "Criação de controlo de autoridade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

			FormCreateControloAut form = null;
			try
			{
				if (GetCurrentTipoNoticiaAut()[0] == TipoNoticiaAut.EntidadeProdutora)
					form = new FormCreateEntidadeProdutora();
				else
					form = new FormCreateControloAut();

				form.cbNoticiaAut.BeginUpdate();
				form.cbNoticiaAut.DataSource = caList.cbNoticiaAut.DataSource;
				form.cbNoticiaAut.DisplayMember = caList.cbNoticiaAut.DisplayMember;
				form.cbNoticiaAut.EndUpdate();
				if (form.cbNoticiaAut.Items.Count == 2)
				{
					form.cbNoticiaAut.SelectedIndex = 1;
					form.cbNoticiaAut.Enabled = false;
				}
				form.LoadData(true);
			}
			finally
			{
				((frmMain)TopLevelControl).LeaveWaitMode();
			}

			switch (form.ShowDialog())
			{
				case DialogResult.OK:
					Trace.WriteLine("A criar notícia de autoridade...");
					GISADataset.DicionarioRow dicionarioRow = null;
					if (GisaDataSetHelper.GetInstance().Dicionario.Select(string.Format("Termo = '{0}'", form.ListTermos.ValidAuthorizedForm.Replace("'", "''"))).Length > 0 && form.ListTermos.ValidAuthorizedForm != null)
						dicionarioRow = (GISADataset.DicionarioRow)(GisaDataSetHelper.GetInstance().Dicionario.Select(string.Format("Termo = '{0}'", form.ListTermos.ValidAuthorizedForm.Replace("'", "''")))[0]);
					else if (form.ListTermos.ValidAuthorizedForm != null && form.ListTermos.ValidAuthorizedForm.Length > 0)
					{
						dicionarioRow = GisaDataSetHelper.GetInstance().Dicionario.NewDicionarioRow();
                        dicionarioRow.Termo = form.ListTermos.ValidAuthorizedForm;
						dicionarioRow.CatCode = "CA";
						dicionarioRow.Versao = new byte[]{};
					}

					// excluir selecção de termos inválidos. nunca deveria acontecer
					Trace.Assert(dicionarioRow != null);

					// se o estado da row for detached trata-se de um novo termo 
					// criado. nesse caso é necessário adiciona-lo ao dataset.
					if (dicionarioRow.RowState == DataRowState.Detached)
					{
						dicionarioRow.isDeleted = 0;
						GisaDataSetHelper.GetInstance().Dicionario.AddDicionarioRow(dicionarioRow);
					}

					// obter o tipo de noticia de autoridade escolhido
					GISADataset.TipoNoticiaAutRow tnaRow = null;
					tnaRow = (GISADataset.TipoNoticiaAutRow)form.cbNoticiaAut.SelectedItem;
					Trace.Assert(tnaRow != null);

					// adicionar a nova notícia de autoridade ao modelo de dados e à interface
					GISADataset.ControloAutRow caRow = GisaDataSetHelper.GetInstance().ControloAut.NewControloAutRow();
					caRow.Autorizado = false;
					caRow.Completo = false;
					caRow.IDTipoNoticiaAut = tnaRow.ID;
					caRow.NotaExplicativa = "";
					caRow.IDIso639p2 = 348L; // language: portuguese
					caRow.IDIso15924 = 60L; // script: latin
					caRow.ChaveColectividade = "";
					caRow.ChaveRegisto = "";
					caRow.RegrasConvencoes = "";
					caRow.Observacoes = "";
					caRow.DescContextoGeral = "";
					caRow.DescEnquadramentoLegal = "";
					caRow.DescEstatutoLegal = "";
					caRow.DescEstruturaInterna = "";
					caRow.DescOcupacoesActividades = "";
					caRow.DescHistoria = "";
					caRow.DescOutraInformacaoRelevante = "";
					caRow.DescZonaGeografica = "";
					caRow.isDeleted = 0;

					GisaDataSetHelper.GetInstance().ControloAut.AddControloAutRow(caRow);

					GISADataset.TipoControloAutFormaRow tcafRowAutorizado = null;
					tcafRowAutorizado = (GISADataset.TipoControloAutFormaRow)(GisaDataSetHelper.GetInstance().TipoControloAutForma.Select("ID=" + System.Enum.Format(typeof(TipoControloAutForma), TipoControloAutForma.FormaAutorizada, "D"))[0]);

					// criar um registo em ControloAutDicionario usando o termo obtido e com TipoControloAutforma "autorizado"
					byte[] Versao = null;
					GISADataset.ControloAutDicionarioRow cadRow = null;
					cadRow = GisaDataSetHelper.GetInstance().ControloAutDicionario.AddControloAutDicionarioRow(caRow, dicionarioRow, tcafRowAutorizado, Versao, 0);

                    //Ao criar uma EP criar também um nível correspondente, de forma a ser possível adicionar "RelacaoHierarquica"s
					GISADataset.NivelRow nRow = null;
					GISADataset.NivelControloAutRow ncaRow = null;
					var args = new PersistencyHelper.NewControloAutPreSaveArguments();
                    var postSaveAction = new PostSaveAction();
                    var argsPostSave = new PersistencyHelper.UpdatePermissionsPostSaveArguments();
                    postSaveAction.args = argsPostSave;

                    postSaveAction.postSaveDelegate = delegate(PersistencyHelper.PostSaveArguments postSaveArgs)
                    {
                        if ((tnaRow.ID == (long)TipoNoticiaAut.EntidadeProdutora && !args.successCodigo) || !args.successTermo)
                            return;
 
                        // registar a criação do controlo de autoridade (o registo deve ser feito à parte do save visto que a tabela ControloAutDataDeExistencia não está ligada a ControloAut e por esse motivo, o ID na tabela de registo ser igual em ControloAut em vez de ser negativo)
                        if (((frmMain)TopLevelControl).isSuportPanel)
                        {
                            GISADataset.TrusteeUserRow tuAuthorRow = null;
                            if (SessionHelper.GetGisaPrincipal().TrusteeUserAuthor != null && !(SessionHelper.GetGisaPrincipal().TrusteeUserAuthor.RowState == DataRowState.Detached))
                                tuAuthorRow = SessionHelper.GetGisaPrincipal().TrusteeUserAuthor;
                            var r = GISA.Model.RecordRegisterHelper.CreateControlAutDataDeDescricaoRow(caRow, SessionHelper.GetGisaPrincipal().TrusteeUserOperator, tuAuthorRow, DateTime.Now);
                            GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao.AddControloAutDataDeDescricaoRow(r);
                        }
                        else
                            CurrentContext.RaiseRegisterModificationEvent(caRow);

                        PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao,
                                GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao.Cast<GISADataset.ControloAutDataDeDescricaoRow>().Where(ca => ca.RowState == DataRowState.Added).ToArray(), postSaveArgs.tran);
                    };

					if (caRow.TipoNoticiaAutRow.ID == Convert.ToInt64(TipoNoticiaAut.EntidadeProdutora))
					{
                        var tep = GisaDataSetHelper.GetInstance().TipoEntidadeProdutora.Cast<GISADataset.TipoEntidadeProdutoraRow>().First();
                        GisaDataSetHelper.GetInstance().ControloAutEntidadeProdutora.AddControloAutEntidadeProdutoraRow(caRow, tep, new byte[] { }, 0);
                        GisaDataSetHelper.GetInstance().ControloAutDatasExistencia.AddControloAutDatasExistenciaRow(caRow, "", "", "", "", false, "", "", "", false, new byte[] { }, 0);

						CreateAssociatedNivel(caRow, ref nRow, ref ncaRow);
						nRow.Codigo = ((FormCreateEntidadeProdutora)form).txtCodigo.Text;
						args.nID = nRow.ID;
					}

					try
					{
						args.caID = caRow.ID;
						args.dID = dicionarioRow.ID;
                        args.dTermo = dicionarioRow.Termo.Replace("'", "''");
						args.cadIDControloAut = cadRow.IDControloAut;
						args.cadIDDicionario = cadRow.IDDicionario;
						args.cadIDTipoControloAutForma = cadRow.IDTipoControloAutForma;

                        PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save(DelegatesHelper.validateCANewTermo, args, postSaveAction);
                        PersistencyHelper.cleanDeletedData(new List<TableDepthOrdered.TableCloudType>(new TableDepthOrdered.TableCloudType[] { PersistencyHelper.determinaNuvem("Nivel"), PersistencyHelper.determinaNuvem("ControloAut") }));

                        if (args.successTermo && (!(form is FormCreateEntidadeProdutora) || args.successCodigo))
						{
                            if (successfulSave == PersistencyHelper.SaveResult.successful)
                            {
                                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                                try
                                {
                                    List<string> IDNiveis = ControloAutRule.Current.GetNiveisDocAssociados(caRow.ID, ho.Connection);
                                    GISA.Search.Updater.updateNivelDocumental(IDNiveis);
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

							caList.AddNivel(cadRow);
						}
						else
						{
							if (form is FormCreateEntidadeProdutora)
								MessageBox.Show("Não foi possível criar a notícia de autoridade " + Environment.NewLine + "uma vez que essa designação e/ou código já" + Environment.NewLine + "é utilizada.", "Nova Notícia de autoridade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							else
								MessageBox.Show("Não foi possível criar a notícia de autoridade " + Environment.NewLine + "uma vez que já existe uma com essa designação.", "Nova Notícia de autoridade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
					}
					catch (Exception ex)
					{
						Trace.WriteLine(ex);
						throw;
					}
					break;
				case DialogResult.Cancel:
				break;
			}
		}

		public static void CreateAssociatedNivel(GISADataset.ControloAutRow caRow, ref GISADataset.NivelRow nRow, ref GISADataset.NivelControloAutRow ncaRow)
		{
			byte[] Versao = null;
			GISADataset.TipoNivelRow tnRow = null;
			tnRow = (GISADataset.TipoNivelRow)(GisaDataSetHelper.GetInstance().TipoNivel.Select(string.Format("ID={0}", TipoNivel.ESTRUTURAL))[0]);
			nRow = GisaDataSetHelper.GetInstance().Nivel.AddNivelRow(tnRow, " ", "CA", Versao, 0);
			ncaRow = GisaDataSetHelper.GetInstance().NivelControloAut.AddNivelControloAutRow(nRow, caRow, Versao, 0);

            // Adicionar Permissões
            PermissoesHelper.AddNewNivelGrantPermissions(nRow);
		}

		// NOTA: este processo evolui normalmente partindo sempre do princípio que o CA a 
		//       editar existe na BD. Se não for o caso, essa informação não existe, 
		//       neste ponto de execução tendo de ser verificada somente aquando da gravação dos dados
		private void ClickBtnEditar()
		{
			((frmMain)TopLevelControl).EnterWaitMode();
			FormCreateControloAut form = null;
			ListViewItem cadItem = null;
			GISADataset.ControloAutDicionarioRow contextCadRow = null;
			GISADataset.TipoNoticiaAutRow contextNoticiaAutRow = null;
			try
			{
				if (caList.SelectedItems.Count < 1)
					return;

				cadItem = caList.SelectedItems[0];
				contextCadRow = (GISADataset.ControloAutDicionarioRow)cadItem.Tag;

				if (contextCadRow.RowState == DataRowState.Detached)
				{
					MessageBox.Show("Não é possível editar a notícia de autoridade selecionada " + System.Environment.NewLine + "uma vez que foi apagada por outro utilizador.", "Edição de notícias de autoridade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				// so é possível editar termos autorizados
				if (contextCadRow.IDTipoControloAutForma != Convert.ToInt64(TipoControloAutForma.FormaAutorizada))
					return;

				contextNoticiaAutRow = contextCadRow.ControloAutRow.TipoNoticiaAutRow;

				if (GetCurrentTipoNoticiaAut()[0] == TipoNoticiaAut.EntidadeProdutora)
				{
					form = new FormCreateEntidadeProdutora();
					((FormCreateEntidadeProdutora)form).txtCodigo.Text = contextCadRow.ControloAutRow.GetNivelControloAutRows()[0].NivelRow.Codigo;
				}
				else
					form = new FormCreateControloAut();

				form.Text = "Editar notícia de autoridade";
				form.cbNoticiaAut.DataSource = caList.cbNoticiaAut.DataSource;
				form.cbNoticiaAut.DisplayMember = caList.cbNoticiaAut.DisplayMember;
				form.cbNoticiaAut.SelectedItem = contextNoticiaAutRow;
				if (form.cbNoticiaAut.Items.Count == 2)
					form.cbNoticiaAut.Enabled = false;

				form.ListTermos.rbEscolher.Checked = true;
				form.SetControloAutDicionarioRow(contextCadRow);
				form.LoadData(true);
			}
			finally
			{
				((frmMain)TopLevelControl).LeaveWaitMode();
			}

			bool updateIndex = false;
			switch (form.ShowDialog())
			{
				case DialogResult.OK:
					Trace.WriteLine("A editar notícia de autoridade...");
					GISADataset.DicionarioRow dicionarioRow = null;
					if (GisaDataSetHelper.GetInstance().Dicionario.Select(string.Format("Termo = '{0}'", form.ListTermos.ValidAuthorizedForm.Replace("'", "''"))).Length > 0 && form.ListTermos.ValidAuthorizedForm != null)
						dicionarioRow = (GISADataset.DicionarioRow)(GisaDataSetHelper.GetInstance().Dicionario.Select(string.Format("Termo = '{0}'", form.ListTermos.ValidAuthorizedForm.Replace("'", "''")))[0]);
					else if (form.ListTermos.ValidAuthorizedForm != null && form.ListTermos.ValidAuthorizedForm.Length > 0)
					{
						dicionarioRow = GisaDataSetHelper.GetInstance().Dicionario.NewDicionarioRow();
						dicionarioRow.Termo = form.ListTermos.ValidAuthorizedForm;//.Replace("'", "''");
						dicionarioRow.CatCode = "CA";
						dicionarioRow.Versao = new byte[]{};
                        dicionarioRow.isDeleted = 0;
					}

					GISADataset.ControloAutRow caRow = contextCadRow.ControloAutRow;

					// excluir selecção de termos inválidos. nunca deveria acontecer
					Trace.Assert(dicionarioRow != null);

					// obter o tipo de noticia de autoridade escolhido
					GISADataset.TipoNoticiaAutRow tnaRow = null;
					tnaRow = form.SelectedTipoNoticiaAut;
                    if (caRow.IDTipoNoticiaAut != tnaRow.ID)
                        caRow.TipoNoticiaAutRow = tnaRow;

					GISADataset.NivelRow nRow = null;
					if (GetCurrentTipoNoticiaAut()[0] == TipoNoticiaAut.EntidadeProdutora)
						nRow = caRow.GetNivelControloAutRows()[0].NivelRow;

					// excluir selecção de tipos de noticia de autoridade inválidos. nunca deveria acontecer
					Trace.Assert(tnaRow != null);

					// se estiver a ser usado o mesmo termo e o mesmo tipo de noticia de 
					// autoridade não é necessária mais nenhuma acção
					if (dicionarioRow == contextCadRow.DicionarioRow && tnaRow == contextNoticiaAutRow && (GetCurrentTipoNoticiaAut()[0] != TipoNoticiaAut.EntidadeProdutora || ((FormCreateEntidadeProdutora)form).txtCodigo.Text.Equals(nRow.Codigo)))
						return;

					//Dim QueryFilter As String
					// se o estado da row for detached trata-se de um novo termo 
					// criado. nesse caso é necessário adiciona-lo ao dataset.
					if (dicionarioRow.RowState == DataRowState.Detached)
						GisaDataSetHelper.GetInstance().Dicionario.AddDicionarioRow(dicionarioRow);

					if (GetCurrentTipoNoticiaAut()[0] == TipoNoticiaAut.EntidadeProdutora)
						nRow.Codigo = ((FormCreateEntidadeProdutora)form).txtCodigo.Text;

                    // Marcar que houve uma edição para posteriormente ser adicinado um registo na lista de revisões
                    CurrentContext.RaiseRegisterModificationEvent(caRow);

					GISADataset.TipoControloAutFormaRow tcafRowAutorizado = null;
					tcafRowAutorizado = (GISADataset.TipoControloAutFormaRow)(GisaDataSetHelper.GetInstance().TipoControloAutForma.Select("ID=" + System.Enum.Format(typeof(TipoControloAutForma), TipoControloAutForma.FormaAutorizada, "D"))[0]);

					PersistencyHelper.EditControloAutPreConcArguments args = new PersistencyHelper.EditControloAutPreConcArguments();
                    var oldTermo = string.Empty;
                    var newTermo = string.Empty;

					if (contextCadRow.IDDicionario != dicionarioRow.ID)
					{
                        oldTermo = contextCadRow.DicionarioRow.Termo;
                        newTermo = dicionarioRow.Termo;

						args.origDRowID = contextCadRow.DicionarioRow.ID;
						// alterar o registo de ControloAutDicionario com TipoControloAutforma "autorizado" para que faça uso do novo termo
						contextCadRow.Delete();

						GISADataset.ControloAutDicionarioRow cadRow = null;
						cadRow = GisaDataSetHelper.GetInstance().ControloAutDicionario.AddControloAutDicionarioRow(caRow, dicionarioRow, tcafRowAutorizado, new byte[]{}, 0);

						try
						{
							args.newCadRowIDControloAut = cadRow.IDControloAut;
							args.newCadRowIDDicionario = cadRow.IDDicionario;
							args.newCadRowIDTipoControloAutForma = cadRow.IDTipoControloAutForma;
							args.newDRowID = dicionarioRow.ID;
							args.origCadRowIDControloAut = (long)(contextCadRow["IDControloAut", DataRowVersion.Original]);
							args.origCadRowIDDicionario = (long)(contextCadRow["IDDicionario", DataRowVersion.Original]);
							args.origCadRowIDTipoControloAutForma = (long)(contextCadRow["IDTipoControloAutForma", DataRowVersion.Original]);
							if (caRow.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.EntidadeProdutora))
								args.nRowID = nRow.ID;

							args.caRowID = caRow.ID;

							PersistencyHelper.save(editCA, args);

							if (args.editCAError == PersistencyHelper.EditControloAutPreConcArguments.EditCAErrors.NoError)
							{
								updateIndex = true;

								cadItem.Tag = cadRow;
								cadItem.Text = cadRow.DicionarioRow.Termo;
								cadItem.SubItems[2].Text = TranslationHelper.FormatBoolean(cadRow.ControloAutRow.Autorizado);
								cadItem.SubItems[3].Text = TranslationHelper.FormatBoolean(cadRow.ControloAutRow.Completo);

								// se o item da GUI tiver sido removido pelo handler de rowchange tornamos a readiciona-lo
								if (cadItem.ListView == null)
								{
									caList.Items.Add(cadItem);
								}

								// é forçada uma mudança de contexto por forma a proceder-se à actualização do código de
								// no painel de descrição das entidades produtoras
								//if (caRow.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.EntidadeProdutora))
								//{
									((PxListView)cadItem.ListView).clearItemSelection(cadItem);
								//}
                                
								((PxListView)cadItem.ListView).selectItem(cadItem);								
							}
							else
							{
								if (args.editCAError == PersistencyHelper.EditControloAutPreConcArguments.EditCAErrors.OrigCadDeleted)
								{

									IDbConnection conn = GisaDataSetHelper.GetConnection();
									try
									{
										conn.Open();
										ControloAutRule.Current.LoadDicionarioAndControloAutDicionario(GisaDataSetHelper.GetInstance(), caRow.ID, conn);
									}
									finally
									{
										conn.Close();
									}

									string QueryFilterNewCad = string.Format("IDControloAut = {0} AND NOT IDDicionario = {1} AND IDTipoControloAutForma = 1", caRow.ID, contextCadRow.IDDicionario);

									GISADataset.ControloAutDicionarioRow newCad = (GISADataset.ControloAutDicionarioRow)(GisaDataSetHelper.GetInstance().ControloAutDicionario.Select(QueryFilterNewCad)[0]);
									cadItem.Tag = newCad;
									cadItem.Text = newCad.DicionarioRow.Termo;
								}
								else if (args.editCAError == PersistencyHelper.EditControloAutPreConcArguments.EditCAErrors.CADeleted)
								{
									// forçar uma perda de contexto uma vez que o contexto actual não é válido
									caList.ClearItemSelection(caList.SelectedItems[0]);
								}
								else if (args.editCAError == PersistencyHelper.EditControloAutPreConcArguments.EditCAErrors.AlreadyUsedCodigo)
								{
									// não está prevista qualquer operação nesta situação
								}
								MessageBox.Show(args.message, "Editar notícia de autoridade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							}
						}
						catch (Exception ex)
						{
							Trace.WriteLine(ex);
							throw;
						}
						finally
						{
                            PersistencyHelper.cleanDeletedData(new List<TableDepthOrdered.TableCloudType>(new TableDepthOrdered.TableCloudType[] { PersistencyHelper.determinaNuvem("Nivel"), PersistencyHelper.determinaNuvem("ControloAut") }));
						}
					}
					else if (nRow != null && nRow.RowState == DataRowState.Modified)
					{
						try
						{
							//só o código da Entidade Produtora foi alterado 
							args.nRowID = nRow.ID;
							args.caRowID = -1; //indicar que a operação só envolve a edição do código
							PersistencyHelper.save(editCA, args);
							if (args.editCAError == PersistencyHelper.EditControloAutPreConcArguments.EditCAErrors.CADeleted)
							{
								MessageBox.Show(args.message, "Editar notícia de autoridade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
								// forçar uma perda de contexto uma vez que o contexto actual não é válido
								caList.ClearItemSelection(caList.SelectedItems[0]);
							}
							else if (args.editCAError == PersistencyHelper.EditControloAutPreConcArguments.EditCAErrors.AlreadyUsedCodigo)
								MessageBox.Show(args.message, "Editar notícia de autoridade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							else if (args.editCAError == PersistencyHelper.EditControloAutPreConcArguments.EditCAErrors.NoError)
							{
								updateIndex = true;
								((PxListView)cadItem.ListView).clearItemSelection(cadItem);
								((PxListView)cadItem.ListView).selectItem(cadItem);
							}
						}
						catch (Exception ex)
						{
							Trace.WriteLine(ex);
							throw;
						}
						finally
						{
                            PersistencyHelper.cleanDeletedData(new List<TableDepthOrdered.TableCloudType>(new TableDepthOrdered.TableCloudType[] { PersistencyHelper.determinaNuvem("Nivel"), PersistencyHelper.determinaNuvem("ControloAut") }));
						}
					}
					else
					{
						try
						{
							//só o tipo de notícia de autoridade do conteúdo foi alterado 
							args.caRowID = caRow.ID;

							PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save(editCA, args);
							if (args.editCAError == PersistencyHelper.EditControloAutPreConcArguments.EditCAErrors.CADeleted)
							{
								MessageBox.Show(args.message, "Editar notícia de autoridade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
								// forçar uma perda de contexto uma vez que o contexto actual não é válido
								caList.ClearItemSelection(caList.SelectedItems[0]);
							}
							else
							{
                                updateIndex = successfulSave == PersistencyHelper.SaveResult.successful;
                                ((PxListView)cadItem.ListView).clearItemSelection(cadItem);
                                ((PxListView)cadItem.ListView).selectItem(cadItem);
							}
						}
						catch (Exception ex)
						{
							Trace.WriteLine(ex);
							throw;
						}
						finally
						{
                            PersistencyHelper.cleanDeletedData(new List<TableDepthOrdered.TableCloudType>(new TableDepthOrdered.TableCloudType[] { PersistencyHelper.determinaNuvem("Nivel"), PersistencyHelper.determinaNuvem("ControloAut") }));
						}
					}

					if (updateIndex)
					{
						GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
						try
						{
							List<string> IDNiveis = ControloAutRule.Current.GetNiveisDocAssociados(caRow.ID, ho.Connection);
							GISA.Search.Updater.updateNivelDocumental(IDNiveis);
                            switch (caRow.IDTipoNoticiaAut)
                            {
                                case (long)TipoNoticiaAut.EntidadeProdutora:
                                    GISA.Search.Updater.updateProdutor(caRow.ID);
                                    GISA.Search.Updater.updateNivelDocumentalComProdutores(caRow.GetNivelControloAutRows()[0].ID);
                                    break;
                                case (long)TipoNoticiaAut.Onomastico:
                                case (long)TipoNoticiaAut.Ideografico:
                                case (long)TipoNoticiaAut.ToponimicoGeografico:
                                    GISA.Search.Updater.updateAssunto(caRow.ID);
                                    if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsFedoraEnable())
                                        SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.ActualizaObjDigitaisPorNivel(IDNiveis, oldTermo, newTermo, caRow.IDTipoNoticiaAut);
                                    break;
                                case (long)TipoNoticiaAut.TipologiaInformacional:
                                    GISA.Search.Updater.updateTipologia(caRow.ID);
                                    if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsFedoraEnable())
                                        SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.ActualizaObjDigitaisPorNivel(IDNiveis, oldTermo, newTermo, caRow.IDTipoNoticiaAut);
                                    break;
                            }
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

					if (! (tnaRow == contextNoticiaAutRow))
						cadItem.SubItems[1].Text = tnaRow.Designacao;
					break;
				case DialogResult.Cancel:
				break;
			}
		}

		// método responsável por garantir que quando se edita um controlo de autoridade todas as suas informações originais
		// ainda existem na base de dados e é garantido que a forma autorizada é única entre os controlos de autoridade e
		// o código atribuído é único entre os niveis estruturais
		private void editCA(PersistencyHelper.PreConcArguments args)
		{
			PersistencyHelper.EditControloAutPreConcArguments ntcaPca = null;
			ntcaPca = (PersistencyHelper.EditControloAutPreConcArguments)args;

			if (ntcaPca.caRowID < 0)
			{
				// só se pretende editar o código do nivel estrutural
				GISADataset.NivelRow nRow = (GISADataset.NivelRow)(GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + ntcaPca.nRowID.ToString())[0]);
                var ddd = GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao.Cast<GISADataset.ControloAutDataDeDescricaoRow>().ToList().Where(r => r.RowState == DataRowState.Added && r.IDControloAut == nRow.GetNivelControloAutRows()[0].IDControloAut);

				//verificar se o nivel existe (e consequentemente a sua entidade produtora)
				if (! (DBAbstractDataLayer.DataAccessRules.NivelRule.Current.existsNivel(ntcaPca.nRowID.ToString(), ntcaPca.tran)))
				{
					System.Data.DataSet tempgisaBackup1 = ntcaPca.gisaBackup;
					PersistencyHelper.BackupRow(ref tempgisaBackup1, nRow);
						ntcaPca.gisaBackup = tempgisaBackup1;
					nRow.RejectChanges();

                    System.Data.DataSet tempgisaBackup = ntcaPca.gisaBackup;
                    ddd.ToList().ForEach(r =>
                    {
                        PersistencyHelper.BackupRow(ref tempgisaBackup, r);
                        ntcaPca.gisaBackup = tempgisaBackup;
                        r.RejectChanges();
                    });

					ntcaPca.editCAError = PersistencyHelper.EditControloAutPreConcArguments.EditCAErrors.CADeleted;
					ntcaPca.message = "A notícia de autoridade em edição foi eliminada por outro utilizador. " + Environment.NewLine + "Esta operação não poderá, por isso, ser concluída.";
					return;
				}

				// verificar se outro nivel estrutural tem o código a atribuir
				if (! (DBAbstractDataLayer.DataAccessRules.NivelRule.Current.isUniqueCodigo(nRow.Codigo, nRow.ID, ntcaPca.tran)))
				{
					System.Data.DataSet tempgisaBackup2 = ntcaPca.gisaBackup;
					PersistencyHelper.BackupRow(ref tempgisaBackup2, nRow);
						ntcaPca.gisaBackup = tempgisaBackup2;
					nRow.RejectChanges();

                    System.Data.DataSet tempgisaBackup = ntcaPca.gisaBackup;
                    ddd.ToList().ForEach(r =>
                    {
                        PersistencyHelper.BackupRow(ref tempgisaBackup, r);
                        ntcaPca.gisaBackup = tempgisaBackup;
                        r.RejectChanges();
                    });

					ntcaPca.editCAError = PersistencyHelper.EditControloAutPreConcArguments.EditCAErrors.AlreadyUsedCodigo;
					ntcaPca.message = "Este código não pode ser atribuído uma vez que já é usado " + Environment.NewLine + "noutro nível estrutural.";
					return;
				}
			}
			else if (ntcaPca.newDRowID == 0)
			{
				// só se pretende editar o tipo de notícia de autoridade do conteúdo
				GISADataset.ControloAutRow caRow = (GISADataset.ControloAutRow)(GisaDataSetHelper.GetInstance().ControloAut.Select("ID=" + ntcaPca.caRowID.ToString())[0]);
                var ddd = GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao.Cast<GISADataset.ControloAutDataDeDescricaoRow>().ToList().Where(r => r.IDControloAut == caRow.ID && r.RowState == DataRowState.Added);

				if (ControloAutRule.Current.isCADeleted(caRow.ID.ToString(), ntcaPca.tran))
				{
					System.Data.DataSet tempgisaBackup3 = ntcaPca.gisaBackup;
					PersistencyHelper.BackupRow(ref tempgisaBackup3, caRow);
						ntcaPca.gisaBackup = tempgisaBackup3;
					caRow.RejectChanges();

                    System.Data.DataSet tempgisaBackup = ntcaPca.gisaBackup;
                    ddd.ToList().ForEach(r => 
                    {
                        PersistencyHelper.BackupRow(ref tempgisaBackup, r);
                        ntcaPca.gisaBackup = tempgisaBackup;
                        r.RejectChanges();
                    });
                    

					ntcaPca.editCAError = PersistencyHelper.EditControloAutPreConcArguments.EditCAErrors.CADeleted;
					ntcaPca.message = "A notícia de autoridade em edição foi eliminada por outro utilizador. " + Environment.NewLine + "Esta operação não poderá, por isso, ser concluída.";
					return;
				}
			}
			else
			{
				// edição que envolve a mudança da forma autorizada e do código do nivel estrutural
				GISADataset.DicionarioRow origDRow = (GISADataset.DicionarioRow)(GisaDataSetHelper.GetInstance().Dicionario.Select("ID=" + ntcaPca.origDRowID.ToString())[0]);
				GISADataset.DicionarioRow newDRow = (GISADataset.DicionarioRow)(GisaDataSetHelper.GetInstance().Dicionario.Select("ID=" + ntcaPca.newDRowID.ToString())[0]);

				// origCadRow terá sempre o seu rowstate definido como deleted
				GISADataset.ControloAutDicionarioRow origCadRow = (GISADataset.ControloAutDicionarioRow)(GisaDataSetHelper.GetInstance().ControloAutDicionario.Select(string.Format("IDControloAut = {0} AND IDDicionario = {1} AND IDTipoControloAutForma = {2}", ntcaPca.origCadRowIDControloAut, ntcaPca.origCadRowIDDicionario, ntcaPca.origCadRowIDTipoControloAutForma), "", DataViewRowState.Deleted)[0]);
				GISADataset.ControloAutDicionarioRow newCadRow = (GISADataset.ControloAutDicionarioRow)(GisaDataSetHelper.GetInstance().ControloAutDicionario.Select(string.Format("IDControloAut = {0} AND IDDicionario = {1} AND IDTipoControloAutForma = {2}", ntcaPca.newCadRowIDControloAut, ntcaPca.newCadRowIDDicionario, ntcaPca.newCadRowIDTipoControloAutForma))[0]);

				GISADataset.ControloAutRow caRow = (GISADataset.ControloAutRow)(GisaDataSetHelper.GetInstance().ControloAut.Select("ID=" + ntcaPca.caRowID.ToString())[0]);
                var ddd = GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao.Cast<GISADataset.ControloAutDataDeDescricaoRow>().ToList().Where(r => r.RowState == DataRowState.Added && r.IDControloAut == caRow.ID);

				GISADataset.NivelRow nRow = null;
				if (caRow.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.EntidadeProdutora))
					nRow = (GISADataset.NivelRow)(GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + ntcaPca.nRowID.ToString())[0]);

				//verificar se o CA existe na BD
				if (ControloAutRule.Current.isCADeleted(caRow.ID.ToString(), ntcaPca.tran))
				{
					System.Data.DataSet tempgisaBackup4 = ntcaPca.gisaBackup;
					PersistencyHelper.BackupRow(ref tempgisaBackup4, newCadRow);
						ntcaPca.gisaBackup = tempgisaBackup4;
					newCadRow.RejectChanges();
					System.Data.DataSet tempgisaBackup5 = ntcaPca.gisaBackup;
					PersistencyHelper.BackupRow(ref tempgisaBackup5, origCadRow);
						ntcaPca.gisaBackup = tempgisaBackup5;
					origCadRow.RejectChanges();
					System.Data.DataSet tempgisaBackup6 = ntcaPca.gisaBackup;
					PersistencyHelper.BackupRow(ref tempgisaBackup6, newDRow);
						ntcaPca.gisaBackup = tempgisaBackup6;
					newDRow.RejectChanges();
					if (nRow != null)
					{
						System.Data.DataSet tempgisaBackup7 = ntcaPca.gisaBackup;
						PersistencyHelper.BackupRow(ref tempgisaBackup7, nRow);
							ntcaPca.gisaBackup = tempgisaBackup7;
						nRow.RejectChanges(); // so necessário nas EPs
					}
					System.Data.DataSet tempgisaBackup8 = ntcaPca.gisaBackup;
					PersistencyHelper.BackupRow(ref tempgisaBackup8, caRow);
						ntcaPca.gisaBackup = tempgisaBackup8;
					caRow.RejectChanges();
                    System.Data.DataSet tempgisaBackup = ntcaPca.gisaBackup;
                    ddd.ToList().ForEach(r =>
                    {
                        PersistencyHelper.BackupRow(ref tempgisaBackup, r);
                        ntcaPca.gisaBackup = tempgisaBackup;
                        r.RejectChanges();
                    });
					ntcaPca.editCAError = PersistencyHelper.EditControloAutPreConcArguments.EditCAErrors.CADeleted;
					ntcaPca.message = "A notícia de autoridade em edição foi eliminada por outro utilizador. " + Environment.NewLine + "Esta operação não poderá, por isso, ser concluída.";
					return;
				}

				// verificar se outro utilizador editou o controlo de autoridade (se o ControloAutDicionario original não existir 
				// na base de dados é sinal que outro utilizador mudou a forma autorizada deste controlo de autoridade)
                bool isOrigCadDeleted = ControloAutRule.Current.isCADDeleted(Convert.ToInt64(origCadRow["IDControloAut", DataRowVersion.Original]), Convert.ToInt64(origCadRow["IDDicionario", DataRowVersion.Original]), Convert.ToInt64(TipoControloAutForma.FormaAutorizada), ntcaPca.tran);

				// no caso de outro utilizador ter editado a forma autorizada do controlo de autoridade esta operação é abortada
				if (isOrigCadDeleted)
				{
					System.Data.DataSet tempgisaBackup9 = ntcaPca.gisaBackup;
					PersistencyHelper.BackupRow(ref tempgisaBackup9, newCadRow);
						ntcaPca.gisaBackup = tempgisaBackup9;
					newCadRow.RejectChanges();
					System.Data.DataSet tempgisaBackup10 = ntcaPca.gisaBackup;
					PersistencyHelper.BackupRow(ref tempgisaBackup10, origCadRow);
						ntcaPca.gisaBackup = tempgisaBackup10;
					origCadRow.RejectChanges();
					System.Data.DataSet tempgisaBackup11 = ntcaPca.gisaBackup;
					PersistencyHelper.BackupRow(ref tempgisaBackup11, newDRow);
						ntcaPca.gisaBackup = tempgisaBackup11;
					newDRow.RejectChanges();
					if (nRow != null)
					{
						System.Data.DataSet tempgisaBackup12 = ntcaPca.gisaBackup;
						PersistencyHelper.BackupRow(ref tempgisaBackup12, nRow);
							ntcaPca.gisaBackup = tempgisaBackup12;
						nRow.RejectChanges(); // so necessário nas EPs
					}
                    System.Data.DataSet tempgisaBackup = ntcaPca.gisaBackup;
                    ddd.ToList().ForEach(r =>
                    {
                        PersistencyHelper.BackupRow(ref tempgisaBackup, r);
                        ntcaPca.gisaBackup = tempgisaBackup;
                        r.RejectChanges();
                    });
					ntcaPca.editCAError = PersistencyHelper.EditControloAutPreConcArguments.EditCAErrors.OrigCadDeleted;
					ntcaPca.message = "Não foi possível alterar a forma autorizada desta notícia de autoridade " + Environment.NewLine + "uma vez que foi já alterada por outro utilizador.";
					return;
				}

				//verificar se o termo novo está a ser utilizado por outro CA
				if (DBAbstractDataLayer.DataAccessRules.DiplomaModeloRule.Current.isTermoUsedByOthers(caRow.ID, newDRow.CatCode, newDRow.Termo.Trim(), false, caRow.TipoNoticiaAutRow.ID, ntcaPca.tran))
				{
					System.Data.DataSet tempgisaBackup13 = ntcaPca.gisaBackup;
					PersistencyHelper.BackupRow(ref tempgisaBackup13, newCadRow);
						ntcaPca.gisaBackup = tempgisaBackup13;
					newCadRow.RejectChanges();
					System.Data.DataSet tempgisaBackup14 = ntcaPca.gisaBackup;
					PersistencyHelper.BackupRow(ref tempgisaBackup14, origCadRow);
						ntcaPca.gisaBackup = tempgisaBackup14;
					origCadRow.RejectChanges();
					System.Data.DataSet tempgisaBackup15 = ntcaPca.gisaBackup;
					PersistencyHelper.BackupRow(ref tempgisaBackup15, newDRow);
						ntcaPca.gisaBackup = tempgisaBackup15;
					newDRow.RejectChanges();
					if (nRow != null)
					{
						System.Data.DataSet tempgisaBackup16 = ntcaPca.gisaBackup;
						PersistencyHelper.BackupRow(ref tempgisaBackup16, nRow);
							ntcaPca.gisaBackup = tempgisaBackup16;
						nRow.RejectChanges(); // so necessário nas EPs
					}
                    System.Data.DataSet tempgisaBackup = ntcaPca.gisaBackup;
                    ddd.ToList().ForEach(r =>
                    {
                        PersistencyHelper.BackupRow(ref tempgisaBackup, r);
                        ntcaPca.gisaBackup = tempgisaBackup;
                        r.RejectChanges();
                    });
					ntcaPca.editCAError = PersistencyHelper.EditControloAutPreConcArguments.EditCAErrors.AlreadyUsedTermo;
					ntcaPca.message = "Este termo não pode ser escolhido uma vez que é já usado " + Environment.NewLine + "como forma autorizada de outra notícia de autoridade.";
					return;
				}

				// verificar se o código atribuído já o foi a outro nível estrutural
				if (nRow != null && ! (DBAbstractDataLayer.DataAccessRules.NivelRule.Current.isUniqueCodigo(nRow.Codigo, nRow.ID, ntcaPca.tran)))
				{
					System.Data.DataSet tempgisaBackup17 = ntcaPca.gisaBackup;
					PersistencyHelper.BackupRow(ref tempgisaBackup17, newCadRow);
						ntcaPca.gisaBackup = tempgisaBackup17;
					newCadRow.RejectChanges();
					System.Data.DataSet tempgisaBackup18 = ntcaPca.gisaBackup;
					PersistencyHelper.BackupRow(ref tempgisaBackup18, origCadRow);
						ntcaPca.gisaBackup = tempgisaBackup18;
					origCadRow.RejectChanges();
					System.Data.DataSet tempgisaBackup19 = ntcaPca.gisaBackup;
					PersistencyHelper.BackupRow(ref tempgisaBackup19, newDRow);
						ntcaPca.gisaBackup = tempgisaBackup19;
					newDRow.RejectChanges();
					System.Data.DataSet tempgisaBackup20 = ntcaPca.gisaBackup;
					PersistencyHelper.BackupRow(ref tempgisaBackup20, nRow);
						ntcaPca.gisaBackup = tempgisaBackup20;
					nRow.RejectChanges();
                    System.Data.DataSet tempgisaBackup = ntcaPca.gisaBackup;
                    ddd.ToList().ForEach(r =>
                    {
                        PersistencyHelper.BackupRow(ref tempgisaBackup, r);
                        ntcaPca.gisaBackup = tempgisaBackup;
                        r.RejectChanges();
                    });
					ntcaPca.editCAError = PersistencyHelper.EditControloAutPreConcArguments.EditCAErrors.AlreadyUsedCodigo;
					ntcaPca.message = "Este código não pode ser atribuído uma vez que já é usado " + Environment.NewLine + "noutro nível estrutural.";
					return;
				}

				// verificar se o termo antigo pode ser apagado (só pode ser apagado se não estiver a ser usado)
				if (! (DBAbstractDataLayer.DataAccessRules.DiplomaModeloRule.Current.isTermoUsedByOthers(caRow.ID, newDRow.CatCode, origDRow.Termo.Trim().Replace("'", "''"), true, ntcaPca.tran)))
				{
					System.Data.DataSet tempgisaBackup21 = ntcaPca.gisaBackup;
					PersistencyHelper.BackupRow(ref tempgisaBackup21, origDRow);
						ntcaPca.gisaBackup = tempgisaBackup21;
					origDRow.Delete();
				}
			}
		}

		private void ClickBtnApagar()
		{
			// This prevents a deactivation when the selection leaves the deleted item.
			// An error would occurr as Deactivate calls Save.
			// Save never verifies if the rows were deleted, thus originating an error.

			ListViewItem cadItem = null;
			GISADataset.ControloAutDicionarioRow contextCadRow = null;
			cadItem = caList.SelectedItems[0];
			contextCadRow = (GISADataset.ControloAutDicionarioRow)cadItem.Tag;

            if (contextCadRow.RowState == DataRowState.Detached)
            {
                caList.ClearItemSelection(cadItem);
                cadItem.Remove();
                UpdateToolBarButtons();
            }
            else
                FormPickDiplomaModelo.DeleteControloAut(this.caList, "Remover notícia de autoridade", "Os elementos selecionados serão removidos apesar das associações existentes. Pretende continuar?", "Deseja apagar os elementos selecionados?", this);
		}

		private void caList_BeforeNewListSelection(object sender, BeforeNewSelectionEventArgs e)
		{
			try
			{
				Debug.WriteLine("caList_BeforeNewListSelection");

				e.SelectionChange = UpdateContext(e.ItemToBeSelected);
				if (e.SelectionChange)
				{
					updateContextStatusBar();
					UpdateToolBarButtons(e.ItemToBeSelected);
				}
			}
            catch(GISA.Search.UpdateServerException)
            {}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw;
			}
		}

		private void caList_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
		{
			object DragDropObject = null;

			if (caList.SelectedItems.Count > 1)
				DragDropObject = GetlstVwControloAutRowArray();
			else if (e.Item != null)
				DragDropObject = ((GISADataset.ControloAutDicionarioRow)(((ListViewItem)e.Item).Tag)).ControloAutRow;

			if (DragDropObject == null)
				Trace.WriteLine("Dragged Nothing");
			else
			{
				Trace.WriteLine("Dragged " + DragDropObject.ToString().GetType().FullName);
				DoDragDrop(DragDropObject, DragDropEffects.Link);
			}
		}

		private GISADataset.ControloAutRow[] GetlstVwControloAutRowArray()
		{
			GISADataset.ControloAutRow[] cas = null;
			cas = new GISADataset.ControloAutRow[caList.SelectedItems.Count];
			int i = 0;
			foreach (ListViewItem li in caList.SelectedItems)
			{
				cas[i] = ((GISADataset.ControloAutDicionarioRow)li.Tag).ControloAutRow;
				i = i + 1;
			}
			return cas;
		}

		private void MasterPanelControloAut_StackChanged(frmMain.StackOperation stackOperation, bool isSupport)
		{
			switch (stackOperation)
			{
				case frmMain.StackOperation.Push:
                    if (!isSupport)
                    {
                        //caList.ClearFiltro();
                        caList.AllowedNoticiaAutLocked = false;
                        //btnFiltro.Pushed = false;
                        Toolbar_ButtonClick(this, new System.Windows.Forms.ToolBarButtonClickEventArgs(btnFiltro));
                        caList.ReloadList();
                    }
					UpdateToolBarButtons();
					break;
				case frmMain.StackOperation.Pop:
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
				if (caList.SelectedItems.Count == 1)
				{
					// Apesar da contagem de items ser "1" pode acontecer, no caso de 
					// items que tenham sido entretanto eliminados, que o SelectedItems 
					// se encontre vazio. Nesse caso consideramos sempre que não existe selecção.
					try
					{
						selectedItem = caList.SelectedItems[0];
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
				GISADataset.ControloAutDicionarioRow cadRow = null;
				cadRow = (GISADataset.ControloAutDicionarioRow)selectedItem.Tag;
				successfulSave = CurrentContext.SetControloAutDicionario(cadRow); //, cadRow.RowState = DataRowState.Detached)
				DelayedRemoveDeletedItems(caList.Items);
			}
			else
				successfulSave = CurrentContext.SetControloAutDicionario(null); //, False)
			return successfulSave;
		}

		private void updateContextStatusBar()
		{
			if (! (MasterPanel.isContextPanel(this)) || ((frmMain)TopLevelControl).isSuportPanel)
				return;

			if (CurrentContext.ControloAutDicionario == null) //OrElse CurrentContext.ControloAut.RowState = DataRowState.Detached Then
				((frmMain)TopLevelControl).StatusBarPanelHint.Text = "";
			else
			{
				if (CurrentContext.ControloAutDicionario.RowState == DataRowState.Detached)
					((frmMain)TopLevelControl).StatusBarPanelHint.Text = string.Empty;
				else
					((frmMain)TopLevelControl).StatusBarPanelHint.Text = "  Notícia de autoridade: " + CurrentContext.ControloAutDicionario.DicionarioRow.Termo;
			}
		}

		private void MenuItemPrint_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (sender == MenuItemPrintNoticiasAutoridades)
				{
					string[] args = null;
                    args = new string[caList.SelectedNoticiasAut.Length + 1];

                    foreach (long ID in caList.SelectedNoticiasAut)
                        args[Array.IndexOf(caList.SelectedNoticiasAut, ID)] = ((GISADataset.TipoNoticiaAutRow)(GisaDataSetHelper.GetInstance().TipoNoticiaAut.Select(string.Format("ID={0}", ID.ToString()))[0])).Designacao;

					// se o relatório for sobre entidades produtoras
                    if (((long)(caList.SelectedNoticiasAut[0])) == 4)
					{
						FormCustomizableReports frm = new FormCustomizableReports();
						frm.AddParameters(DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.BuildParamListControloAut());
						switch (frm.ShowDialog())
						{
							case DialogResult.OK:
                                Reports.ControloAut report = new Reports.ControloAut(string.Format("NoticiaAutoridade_{0}", DateTime.Now.ToString("yyyyMMdd")), caList.SelectedNoticiasAut, args, frm.GetSelectedParameters(), SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
								object o = new Reports.BackgroundRunner(TopLevelControl, report, this.caList.Items.Count);
								break;
							case DialogResult.Cancel:
							break;
						}
					}
					else
					{
                        Reports.ControloAut report = new Reports.ControloAut(string.Format("NoticiaAutoridade_{0}", DateTime.Now.ToString("yyyyMMdd")), caList.SelectedNoticiasAut, args, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
						object o = new Reports.BackgroundRunner(TopLevelControl, report, this.caList.Items.Count);
					}
					//se for sobre conteúdos e tipologias
				}
			}
			catch (Reports.OperationAbortedException)
			{
				// User canceled
			}
		}        
	}
} //end of root namespace