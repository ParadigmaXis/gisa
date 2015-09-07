using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;


using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;
using GISA.Search;

using GISA.Controls;

using Lucene.Net.QueryParsers;

namespace GISA
{
	public class SlavePanelPesquisaUF : GISA.SinglePanel
	{

		public static Bitmap FunctionImage
		{
			get
			{
                return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "PesquisaUnidadesFisicas_enabled_32x32.png");
			}
		}

	#region  Windows Form Designer generated code 

		public SlavePanelPesquisaUF() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            if (!SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsReqEnable())
                this.lstVwEstruturaDocs.Columns.Remove(this.chRequisitado);

            MenuItemPrintUnidadesFisicasResumidas.Click += MenuItemPrint_Click;
            MenuItemPrintUnidadesFisicasDetalhadas.Click += MenuItemPrint_Click;
            lstVwEstruturaDocs.SelectedIndexChanged += lstVwEstruturaDocs_SelectedIndexChanged;
            //PesquisaUFList1.BeforeNewListSelection += PesquisaUFList1_BeforeNewListSelection;

            this.PesquisaUFList1.AddSelectionChangedHandler(new EventHandler(SelectionChanged));

            base.ParentChanged += SlavePanelPesquisaUF_ParentChanged;

            ToolBar.ButtonClick += ToolBar_ButtonClick;

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
		internal System.Windows.Forms.Panel pnlDetalhesTexto;
		internal System.Windows.Forms.RichTextBox rtfDetalhes;
		internal System.Windows.Forms.ToolBarButton ToolBarButton2;
		internal System.Windows.Forms.ToolBarButton ToolBarButton3;
		internal System.Windows.Forms.ToolBarButton ToolBarButton5;
		internal System.Windows.Forms.Panel pnlUnidadesFisicas;
		internal System.Windows.Forms.Panel pnlEstruturaDocs;
		internal System.Windows.Forms.ListView lstVwEstruturaDocs;
		internal System.Windows.Forms.GroupBox GroupBox2;
		internal System.Windows.Forms.GroupBox GroupBox4;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonSeparator;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonReports;
		internal System.Windows.Forms.ContextMenu ContextMenuPrint;
		internal System.Windows.Forms.MenuItem MenuItemPrintUnidadesFisicasResumidas;
		internal System.Windows.Forms.MenuItem MenuItemPrintUnidadesFisicasDetalhadas;
		internal System.Windows.Forms.ColumnHeader chCodigo;
		internal System.Windows.Forms.ColumnHeader chTipoNivel;
        internal System.Windows.Forms.ColumnHeader chDesignacao;
        private ColumnHeader chRequisitado;
        private PanelInfoEPs panelInfoEPs1;
		//internal GISA.PesquisaUFList PesquisaUFList1;
        internal GISA.PesquisaUFDataGrid PesquisaUFList1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.pnlEstruturaDocs = new System.Windows.Forms.Panel();
            this.panelInfoEPs1 = new GISA.PanelInfoEPs();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.lstVwEstruturaDocs = new System.Windows.Forms.ListView();
            this.chCodigo = new System.Windows.Forms.ColumnHeader();
            this.chTipoNivel = new System.Windows.Forms.ColumnHeader();
            this.chDesignacao = new System.Windows.Forms.ColumnHeader();
            this.chRequisitado = new System.Windows.Forms.ColumnHeader();
            this.pnlUnidadesFisicas = new System.Windows.Forms.Panel();
            //this.PesquisaUFList1 = new GISA.PesquisaUFList();
            this.PesquisaUFList1 = new GISA.PesquisaUFDataGrid();
            this.pnlDetalhesTexto = new System.Windows.Forms.Panel();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.rtfDetalhes = new System.Windows.Forms.RichTextBox();
            this.ToolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonSeparator = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonReports = new System.Windows.Forms.ToolBarButton();
            this.ContextMenuPrint = new System.Windows.Forms.ContextMenu();
            this.MenuItemPrintUnidadesFisicasResumidas = new System.Windows.Forms.MenuItem();
            this.MenuItemPrintUnidadesFisicasDetalhadas = new System.Windows.Forms.MenuItem();
            this.pnlToolbarPadding.SuspendLayout();
            this.pnlEstruturaDocs.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.pnlUnidadesFisicas.SuspendLayout();
            this.pnlDetalhesTexto.SuspendLayout();
            this.GroupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFuncao
            // 
            this.lblFuncao.Location = new System.Drawing.Point(0, 0);
            this.lblFuncao.Text = "Resultados da pesquisa";
            // 
            // ToolBar
            // 
            this.ToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.ToolBarButton2,
            this.ToolBarButton3,
            this.ToolBarButton5,
            this.ToolBarButtonSeparator,
            this.ToolBarButtonReports});
            this.ToolBar.ImageList = null;
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
            // 
            // pnlEstruturaDocs
            // 
            this.pnlEstruturaDocs.Controls.Add(this.panelInfoEPs1);
            this.pnlEstruturaDocs.Controls.Add(this.GroupBox2);
            this.pnlEstruturaDocs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEstruturaDocs.Location = new System.Drawing.Point(0, 52);
            this.pnlEstruturaDocs.Name = "pnlEstruturaDocs";
            this.pnlEstruturaDocs.Size = new System.Drawing.Size(600, 300);
            this.pnlEstruturaDocs.TabIndex = 2;
            // 
            // panelInfoEPs1
            // 
            this.panelInfoEPs1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelInfoEPs1.Location = new System.Drawing.Point(8, 173);
            this.panelInfoEPs1.Name = "panelInfoEPs1";
            this.panelInfoEPs1.Size = new System.Drawing.Size(584, 121);
            this.panelInfoEPs1.TabIndex = 4;
            this.panelInfoEPs1.TheRTFBuilder = null;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox2.Controls.Add(this.lstVwEstruturaDocs);
            this.GroupBox2.Location = new System.Drawing.Point(8, 8);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(584, 159);
            this.GroupBox2.TabIndex = 3;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Descrições associadas";
            // 
            // lstVwEstruturaDocs
            // 
            this.lstVwEstruturaDocs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chCodigo,
            this.chTipoNivel,
            this.chDesignacao,
            this.chRequisitado});
            this.lstVwEstruturaDocs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstVwEstruturaDocs.FullRowSelect = true;
            this.lstVwEstruturaDocs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstVwEstruturaDocs.HideSelection = false;
            this.lstVwEstruturaDocs.Location = new System.Drawing.Point(3, 16);
            this.lstVwEstruturaDocs.Name = "lstVwEstruturaDocs";
            this.lstVwEstruturaDocs.Size = new System.Drawing.Size(578, 140);
            this.lstVwEstruturaDocs.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstVwEstruturaDocs.TabIndex = 0;
            this.lstVwEstruturaDocs.UseCompatibleStateImageBehavior = false;
            this.lstVwEstruturaDocs.View = System.Windows.Forms.View.Details;
            // 
            // chCodigo
            // 
            this.chCodigo.Text = "Código de referência";
            this.chCodigo.Width = 114;
            // 
            // chTipoNivel
            // 
            this.chTipoNivel.Text = "Nível de descrição";
            this.chTipoNivel.Width = 150;
            // 
            // chDesignacao
            // 
            this.chDesignacao.Text = "Título";
            this.chDesignacao.Width = 300;
            // 
            // chRequisitado
            // 
            this.chRequisitado.Text = "Requisitado";
            this.chRequisitado.Width = 70;
            // 
            // pnlUnidadesFisicas
            // 
            this.pnlUnidadesFisicas.Controls.Add(this.PesquisaUFList1);
            this.pnlUnidadesFisicas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUnidadesFisicas.Location = new System.Drawing.Point(0, 52);
            this.pnlUnidadesFisicas.Name = "pnlUnidadesFisicas";
            this.pnlUnidadesFisicas.Size = new System.Drawing.Size(600, 300);
            this.pnlUnidadesFisicas.TabIndex = 1;
            // 
            // PesquisaUFList1
            // 
            this.PesquisaUFList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PesquisaUFList1.Location = new System.Drawing.Point(0, 0);
            this.PesquisaUFList1.Name = "PesquisaUFList1";
            this.PesquisaUFList1.Size = new System.Drawing.Size(600, 300);
            this.PesquisaUFList1.TabIndex = 1;
            // 
            // pnlDetalhesTexto
            // 
            this.pnlDetalhesTexto.Controls.Add(this.GroupBox4);
            this.pnlDetalhesTexto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetalhesTexto.Location = new System.Drawing.Point(0, 52);
            this.pnlDetalhesTexto.Name = "pnlDetalhesTexto";
            this.pnlDetalhesTexto.Size = new System.Drawing.Size(600, 300);
            this.pnlDetalhesTexto.TabIndex = 3;
            // 
            // GroupBox4
            // 
            this.GroupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox4.Controls.Add(this.rtfDetalhes);
            this.GroupBox4.Location = new System.Drawing.Point(8, 8);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(584, 286);
            this.GroupBox4.TabIndex = 1;
            this.GroupBox4.TabStop = false;
            this.GroupBox4.Text = "Descrição";
            // 
            // rtfDetalhes
            // 
            this.rtfDetalhes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtfDetalhes.Location = new System.Drawing.Point(8, 16);
            this.rtfDetalhes.Name = "rtfDetalhes";
            this.rtfDetalhes.ReadOnly = true;
            this.rtfDetalhes.Size = new System.Drawing.Size(568, 262);
            this.rtfDetalhes.TabIndex = 0;
            this.rtfDetalhes.Text = "RichTextBox1";
            // 
            // ToolBarButton2
            // 
            this.ToolBarButton2.ImageIndex = 0;
            this.ToolBarButton2.Name = "ToolBarButton2";
            this.ToolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // ToolBarButton3
            // 
            this.ToolBarButton3.ImageIndex = 1;
            this.ToolBarButton3.Name = "ToolBarButton3";
            this.ToolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // ToolBarButton5
            // 
            this.ToolBarButton5.ImageIndex = 2;
            this.ToolBarButton5.Name = "ToolBarButton5";
            this.ToolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // ToolBarButtonSeparator
            // 
            this.ToolBarButtonSeparator.Name = "ToolBarButtonSeparator";
            this.ToolBarButtonSeparator.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButtonReports
            // 
            this.ToolBarButtonReports.DropDownMenu = this.ContextMenuPrint;
            this.ToolBarButtonReports.ImageIndex = 3;
            this.ToolBarButtonReports.Name = "ToolBarButtonReports";
            // 
            // ContextMenuPrint
            // 
            this.ContextMenuPrint.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemPrintUnidadesFisicasResumidas,
            this.MenuItemPrintUnidadesFisicasDetalhadas});
            // 
            // MenuItemPrintUnidadesFisicasResumidas
            // 
            this.MenuItemPrintUnidadesFisicasResumidas.Index = 0;
            this.MenuItemPrintUnidadesFisicasResumidas.Text = "Unidades &físicas resumidas";
            this.MenuItemPrintUnidadesFisicasResumidas.Visible = true;
            // 
            // MenuItemPrintUnidadesFisicasDetalhadas
            // 
            this.MenuItemPrintUnidadesFisicasDetalhadas.Index = 1;
            this.MenuItemPrintUnidadesFisicasDetalhadas.Text = "Unidades fí&sicas detalhadas";
            // 
            // SlavePanelPesquisaUF
            // 
            this.Controls.Add(this.pnlEstruturaDocs);
            this.Controls.Add(this.pnlUnidadesFisicas);
            this.Controls.Add(this.pnlDetalhesTexto);
            this.Name = "SlavePanelPesquisaUF";
            this.Size = new System.Drawing.Size(600, 352);
            this.Controls.SetChildIndex(this.lblFuncao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.Controls.SetChildIndex(this.pnlDetalhesTexto, 0);
            this.Controls.SetChildIndex(this.pnlUnidadesFisicas, 0);
            this.Controls.SetChildIndex(this.pnlEstruturaDocs, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.pnlEstruturaDocs.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.pnlUnidadesFisicas.ResumeLayout(false);
            this.pnlDetalhesTexto.ResumeLayout(false);
            this.GroupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

		private void GetExtraResources()
		{
			lstVwEstruturaDocs.SmallImageList = TipoNivelRelacionado.GetImageList();
			ToolBar.ImageList = SharedResourcesOld.CurrentSharedResources.PesquisaUFsImageList;

			ToolBarButton2.ToolTipText = "Resultados";
			ToolBarButton3.ToolTipText = "Detalhes";
			ToolBarButton5.ToolTipText = "Níveis associados";
			ToolBarButtonReports.ToolTipText = "Relatórios";
		}

		internal FormImageViewer frmImgViewer = null;

        private MasterPanelPesquisaUF mMasterPanelPesquisaUF = null;
		private MasterPanelPesquisaUF MasterPanelPesquisaUF
		{
			get
			{
                if (mMasterPanelPesquisaUF == null)
                    mMasterPanelPesquisaUF = (MasterPanelPesquisaUF)(((frmMain)TopLevelControl).MasterPanel);

                return mMasterPanelPesquisaUF;
			}
		}

		private Image mImagem;
		private Image ImagemEscolhida
		{
			get { return mImagem; }
			set { mImagem = value; }
		}

		public override void LoadData()
		{
			MasterPanelPesquisaUF.ExecuteQuery += this.ExecuteQuery;
			MasterPanelPesquisaUF.ClearSearchResults += this.ClearSearchResults;
			ToolBar_ButtonClick(this, new ToolBarButtonClickEventArgs(ToolBarButton2));

			this.Visible = true;
		}

		//private GisaDataSetHelper.HoldOpen ho;
		public override void ModelToView()
		{
			MasterPanelPesquisaUF.ForceLoadContents();
		}

		public override bool ViewToModel()
		{
			return true;
		}

		public override void Deactivate()
		{
			MasterPanelPesquisaUF.ExecuteQuery -= this.ExecuteQuery;
		}

        public override PersistencyHelper.SaveResult Save()
		{
			return Save(false);
		}

        public override PersistencyHelper.SaveResult Save(bool activateOpcaoCancelar)
		{
            return PersistencyHelper.SaveResult.unsuccessful;
		}

		private string NullIfEmpty(string Text)
		{
			if (Text == null || Text.Length == 0)
			{
				return "null";
			}
			return string.Format("'{0}'", Text.Replace("'", "''"));
		}

		private object DBNullIfEmpty(string Text)
		{
			if (Text == null || Text.Length == 0)
			{
				return DBNull.Value;
			}
			return Text;
		}

		private object DBNullIfEmpty(int @int)
		{
			if (@int == int.MinValue)
			{
				return DBNull.Value;
			}
			return @int;
		}

		private object DBNullIfEmpty(long lng)
		{
			if (lng == long.MinValue)
			{
				return DBNull.Value;
			}
			return lng;
		}

		private void ClearSearchResults(MasterPanelPesquisaUF MasterPanel)
		{
			resultNumber = 0;
			this.lblFuncao.Text = "Resultados da pesquisa";
			PesquisaUFList1.ClearSearchResults();
			ToolBar_ButtonClick(this, new ToolBarButtonClickEventArgs(ToolBarButton2));
			rtfDetalhes.Clear();
			lstVwEstruturaDocs.Items.Clear();
            panelInfoEPs1.ClearAll();
			UpdateToolBarButtons();
		}

		private ArrayList itemsPesquisa = new ArrayList();
		private long resultNumber = 0;
		private void ExecuteQuery(MasterPanelPesquisaUF MasterPanel)
		{
			ToolBar_ButtonClick(this, new ToolBarButtonClickEventArgs(ToolBarButton2));
            List<string> resultadosDaPesquisa = new List<string>();
			try
			{
				((frmMain)TopLevelControl).EnterWaitMode();
				this.lblFuncao.Text = string.Format("Resultados da pesquisa (em curso)");
				this.lblFuncao.Update();

				GisaDataSetHelper.ManageDatasetConstraints(false);

                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
				try
				{
                    // TODO: Considerar retirar a dependência com Lucene.Net para fazer a validação dos campos: nem todos são validados....
					UnidadeFisicaSearch ufSearch = new UnidadeFisicaSearch();
                    Lucene.Net.Analysis.Analyzer analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
                    QueryParser qp = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, string.Empty, analyzer);
                    qp.AllowLeadingWildcard = true ;
                    StringBuilder errorMessage = new StringBuilder();

                    ufSearch.Numero = Helper.AddFieldToSearch(qp, "Código Parcial", MasterPanelPesquisaUF.txtCdReferencia.Text, ref errorMessage);
                    ufSearch.Designacao = Helper.AddFieldToSearch(qp, "Designação", MasterPanelPesquisaUF.txtDesignacao.Text, ref errorMessage);
                    ufSearch.Cota = Helper.AddFieldToSearch(qp, "Cota", Helper.EscapeSpecialCharactersCotaDocumento(MasterPanelPesquisaUF.txtCota.Text.ToLower()), ref errorMessage);
                    ufSearch.CodigoBarras = Helper.AddFieldToSearch(qp, "CodigoBarras", MasterPanelPesquisaUF.txtCodigoBarras.Text, ref errorMessage);
                    ufSearch.ConteudoInformacional = Helper.AddFieldToSearch(qp, "Conteúdo Informacional", MasterPanelPesquisaUF.txtConteudoInformacional.Text, ref errorMessage);

                    // Nota: a combo MasterPanelPesquisaUF.cbTipoAcond contem uma lista de GISADataset.TipoAcondicionamentoRow
                    GISADataset.TipoAcondicionamentoRow tipoAcondRow = (GISADataset.TipoAcondicionamentoRow)MasterPanelPesquisaUF.cbTipoAcond.SelectedItem;

                    string tipoUnidadeFisica = tipoAcondRow.Designacao;
                    if(tipoUnidadeFisica.ToLower().Equals("todos"))
                        tipoUnidadeFisica = "";
                    else if(tipoUnidadeFisica.ToLower().Equals("<desconhecido>"))
                        tipoUnidadeFisica = "desconhecido";

                    ufSearch.TipoUnidadeFisica = Helper.AddFieldToSearch(qp, "Tipo de Unidade Física", tipoUnidadeFisica, ref errorMessage);
                    ufSearch.GuiaIncorporacao = Helper.AddFieldToSearch(qp, "Guia incorporação", MasterPanelPesquisaUF.txtGuiaIncorporacao.Text, ref errorMessage);

                    ufSearch.Eliminado = MasterPanelPesquisaUF.chkFiltroUFsEliminadas.Checked ? "" : "nao";

                    if (MasterPanelPesquisaUF.cdbDataInicio.Checked)
                        ufSearch.DataProducaoInicioDoInicio = MasterPanelPesquisaUF.cdbDataInicio.GetStandardMaskDate.ToString("yyyyMMdd");
                    
                    if (MasterPanelPesquisaUF.cdbDataFim.Checked)
                        ufSearch.DataProducaoFimDoInicio = MasterPanelPesquisaUF.cdbDataFim.GetStandardMaskDate.ToString("yyyyMMdd");
                    
                    if (MasterPanelPesquisaUF.cdbInicioDoFim.Checked)
                        ufSearch.DataProducaoInicioDoFim = MasterPanelPesquisaUF.cdbInicioDoFim.GetStandardMaskDate.ToString("yyyyMMdd");
                    
                    if (MasterPanelPesquisaUF.cdbFimDoFim.Checked)
                        ufSearch.DataProducaoFimDoFim = MasterPanelPesquisaUF.cdbFimDoFim.GetStandardMaskDate.ToString("yyyyMMdd");

                    if (errorMessage.Length > 0)
                    {
                        MessageBox.Show("O(s) campo(s) seguinte(s) tem(êm) valor(es) incorrecto(s): " +
                            System.Environment.NewLine +
                            errorMessage.ToString());

                        return;
                    }

                    resultadosDaPesquisa.AddRange(SearchImpl.search(ufSearch.ToString(), "unidadeFisica"));

                    // filtragens adicionais sobre os resultados de pesquisa
                    string operador = string.Empty;
                    int anoEdicaoInicio = int.MinValue;
                    int mesEdicaoInicio = int.MinValue;
                    int diaEdicaoInicio = int.MinValue;
                    int anoEdicaoFim = int.MinValue;
                    int mesEdicaoFim = int.MinValue;
                    int diaEdicaoFim = int.MinValue;
                    long IDNivel = long.MinValue;
                    int assoc = int.MinValue;

                    operador = MasterPanelPesquisaUF.txtOperador.Text;
                    assoc = MasterPanelPesquisaUF.cbAssociacoes.SelectedIndex;

                    if (MasterPanelPesquisaUF.cdbDataEdicaoInicio.Checked)
                    {
                        anoEdicaoInicio = MasterPanelPesquisaUF.cdbDataEdicaoInicio.Year;
                        mesEdicaoInicio = MasterPanelPesquisaUF.cdbDataEdicaoInicio.Month;
                        diaEdicaoInicio = MasterPanelPesquisaUF.cdbDataEdicaoInicio.Day;
                    }
                    
                    if (MasterPanelPesquisaUF.cdbDataEdicaoFim.Checked)
                    {
                        anoEdicaoFim = MasterPanelPesquisaUF.cdbDataEdicaoFim.Year;
                        mesEdicaoFim = MasterPanelPesquisaUF.cdbDataEdicaoFim.Month;
                        diaEdicaoFim = MasterPanelPesquisaUF.cdbDataEdicaoFim.Day;
                    }
                    
                    if (MasterPanelPesquisaUF.chkEstruturaArquivistica.Checked && MasterPanelPesquisaUF.cnList.SelectedNivelRow != null)
                        IDNivel = MasterPanelPesquisaUF.cnList.SelectedNivelRow.ID;

                    PesquisaUFList1.SearchServerIDs = resultadosDaPesquisa;
                    PesquisaUFList1.operador = operador;
                    PesquisaUFList1.anoEdicaoInicio = anoEdicaoInicio;
                    PesquisaUFList1.mesEdicaoInicio = mesEdicaoInicio;
                    PesquisaUFList1.diaEdicaoInicio = diaEdicaoInicio;
                    PesquisaUFList1.anoEdicaoFim = anoEdicaoFim;
                    PesquisaUFList1.mesEdicaoFim = mesEdicaoFim;
                    PesquisaUFList1.diaEdicaoFim = diaEdicaoFim;
                    PesquisaUFList1.IDNivel = IDNivel;
                    PesquisaUFList1.assoc = assoc;
                    PesquisaUFList1.NewSearch = true;

                    resultNumber = 0;                    
                    PesquisaUFList1.ReloadList();
                    PesquisaUFList1.NewSearch = false;
                    resultNumber = PesquisaUFList1.NrResults;
                    PesquisaUFList1.Focus();

                    Trace.WriteLine(string.Format("Found {0} results.", resultNumber));
                    this.lblFuncao.Text = string.Format("Resultados da pesquisa ({0} {1})", resultNumber, ((resultNumber == 1) ? "descrição" : "descrições"));

                    UpdateToolBarButtons();
                }
				catch (Exception ex)
				{
					resultNumber = 0;
					Trace.WriteLine(ex);
					this.lblFuncao.Text = string.Format("Resultados da pesquisa");
				}
				finally
				{
                    ho.Dispose();
				}
				GisaDataSetHelper.ManageDatasetConstraints(true);
			}
			catch (Exception Ex)
			{
				Trace.WriteLine(Ex);
				this.lblFuncao.Text = string.Format("Resultados da pesquisa");
			}
			finally
			{
				((frmMain)TopLevelControl).LeaveWaitMode();
			}
		}

		private void PesquisaList1_MyColumnClick(object sender, MyColumnClickEventArgs e)
		{
			if (PesquisaUFList1.Items.Count > 0)
				PesquisaUFList1.ReloadList();
		}

		private void ActivateDetalhesTexto()
		{
			if (PesquisaUFList1.GetSelectedRows.Count() == 1)
			{

				rtfDetalhes.Clear();
				//rtfDetalhes.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft Sans Serif;}}\\viewkind4\\uc1\\pard\\f0\\fs24 " + GetFRDBaseAsRTF((GISADataset.NivelRow)(PesquisaUFList1.SelectedItems[0].Tag)) + "\\par}";
                rtfDetalhes.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft Sans Serif;}}\\viewkind4\\uc1\\pard\\f0\\fs24 " + 
                    GetFRDBaseAsRTF((GISADataset.NivelRow)(PesquisaUFList1.SelectedRow)) + "\\par}";
				Debug.WriteLine("");
				Debug.WriteLine(rtfDetalhes.Rtf);
				Debug.WriteLine("");
				pnlDetalhesTexto.BringToFront();
			}
			else
				ToolBar_ButtonClick(this, new ToolBarButtonClickEventArgs(ToolBarButton2));
		}

		public void ActivateEstruturaDocs()
		{
			if (PesquisaUFList1.GetSelectedRows.Count() == 1)
			{
				long ticks = DateTime.Now.Ticks;
                List<PesquisaRule.DocAssociado> docsAssociados = new List<PesquisaRule.DocAssociado>();
				//string ID = ((GISADataset.NivelRow)(PesquisaUFList1.SelectedItems[0].Tag)).ID.ToString();
                string ID = ((GISADataset.NivelRow)(PesquisaUFList1.SelectedRow)).ID.ToString();

				GisaDataSetHelper.ManageDatasetConstraints(false);

                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
				try
				{
					docsAssociados = PesquisaRule.Current.LoadEstruturaDocsData(GisaDataSetHelper.GetInstance(), ID, ho.Connection);
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

				panelInfoEPs1.ClearAll();

				lstVwEstruturaDocs.BeginUpdate();
				lstVwEstruturaDocs.Items.Clear();
				lstVwEstruturaDocs.SmallImageList = TipoNivelRelacionado.GetImageList();

                List<ListViewItem> items = new List<ListViewItem>();
				foreach (var dAssoc in docsAssociados)
				{
                    ListViewItem item = new ListViewItem(new string[] { string.Empty, string.Empty, string.Empty, string.Empty });
					item.SubItems[chCodigo.Index].Text = dAssoc.Codigo;
					item.SubItems[chTipoNivel.Index].Text = dAssoc.RelDesignacao;
					item.SubItems[chDesignacao.Index].Text = dAssoc.NivelDesignacao;
                    if (this.lstVwEstruturaDocs.Columns.Contains(this.chRequisitado))
                    {
                        if (dAssoc.Requisitado)
                            item.SubItems[chRequisitado.Index].Text = "Sim";
                        else
                            item.SubItems[chRequisitado.Index].Text = "Não";
                    }
					item.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageBase(dAssoc.GUIOrder);
					item.StateImageIndex = item.ImageIndex;
					item.Tag = (GISADataset.NivelRow)(GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + dAssoc.IDNivel.ToString())[0]);
					items.Add(item);
				}
				lstVwEstruturaDocs.Items.AddRange(items.ToArray());

				lstVwEstruturaDocs.EndUpdate();
				pnlEstruturaDocs.BringToFront();
				Debug.WriteLine("<<ActivateEstruturaDocs>>: " + new TimeSpan(DateTime.Now.Ticks - ticks).ToString());
			}
			else
				ToolBar_ButtonClick(this, new ToolBarButtonClickEventArgs(ToolBarButton2));
		}

		private void ActivateUnidadesFisicas()
		{
			pnlUnidadesFisicas.BringToFront();
		}

		private void ToolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			if (! (e.Button == ToolBarButtonReports))
			{
				foreach (ToolBarButton b in ToolBar.Buttons)
				{
					if (b.Style == ToolBarButtonStyle.ToggleButton)
						b.Pushed = e.Button == b;
				}
			}

			//Antes de permitir a visualização de qualquer informação acerca da Unidade Física seleccionada
			//é necessário garantir que esta ainda não foi apagada por algum utilizador
			bool ufIsDeleted = false;
			if (PesquisaUFList1.GetSelectedRows.Count() > 0)
			{
                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
				try
				{
					//ufIsDeleted = PesquisaRule.Current.isNivelDeleted(GisaDataSetHelper.GetInstance(), ((GISADataset.NivelRow)(PesquisaUFList1.SelectedItems[0].Tag)).ID, ho.Connection);
                    ufIsDeleted = PesquisaRule.Current.isNivelDeleted(GisaDataSetHelper.GetInstance(), ((GISADataset.NivelRow)(PesquisaUFList1.SelectedRow)).ID, ho.Connection);
				}
				catch (Exception ex)
				{
					Trace.WriteLine(ex);
					throw ex;
				}
				finally
				{
                    ho.Dispose();
				}
			}

			if (! ufIsDeleted)
			{
				if (e.Button == ToolBarButton2)
					ActivateUnidadesFisicas();
				else if (e.Button == ToolBarButton3)
					ActivateDetalhesTexto();
				else if (e.Button == ToolBarButton5)
					ActivateEstruturaDocs();
				else if (e.Button == ToolBarButtonReports)
				{
					if (e.Button.DropDownMenu != null && e.Button.DropDownMenu is ContextMenu)
						((ContextMenu)e.Button.DropDownMenu).Show(ToolBar, new System.Drawing.Point(e.Button.Rectangle.X, e.Button.Rectangle.Y + e.Button.Rectangle.Height));
				}
			}
			else
			{
				MessageBox.Show("O elemento selecionado foi apagado por outro utilizador." + System.Environment.NewLine + "Por esse motivo não é possível apresentar qualquer detalhe desse elemento", "Seleção de resultados da pesquisa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				ToolBar_ButtonClick(this, new ToolBarButtonClickEventArgs(ToolBarButton2));
			}

			this.Cursor = Cursors.Arrow;
		}

		private void MenuItemPrint_Click(object sender, System.EventArgs e)
		{
			try
			{
                //ArrayList searchResults = GetLastSearchResults()
				ArrayList searchResults = PesquisaUFList1.getAllIDsNivel();
				if (searchResults.Count == 0)
					MessageBox.Show("Não foram encontrados resultados na última pesquisa " + Environment.NewLine + "a partir dos quais possa ser construído um relatório.", "Relatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				else
				{
					if (sender == MenuItemPrintUnidadesFisicasResumidas)
					{
						Reports.UnidadesFisicasResumido report = new Reports.UnidadesFisicasResumido(string.Format("ListaDeUnidadesFisicasResumidas_{0}", DateTime.Now.ToString("yyyyMMdd")), long.MinValue);
						object o = new Reports.BackgroundRunner(TopLevelControl, report, resultNumber);
					}
					else if (sender == MenuItemPrintUnidadesFisicasDetalhadas)
					{
						FormCustomizableReports frm = new FormCustomizableReports();
						frm.AddParameters(DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.BuildParamListUF());
						switch (frm.ShowDialog())
						{
							case DialogResult.OK:
								Reports.UnidadesFisicasDetalhado report = new Reports.UnidadesFisicasDetalhado(string.Format("ListaDeUnidadesFisicasDetalhadas_{0}", DateTime.Now.ToString("yyyyMMdd")), null, frm.GetSelectedParameters(), SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
								object o = new Reports.BackgroundRunner(TopLevelControl, report, resultNumber);
								break;
							case DialogResult.Cancel:
							break;
						}
					}
				}
			}
			catch (Reports.OperationAbortedException)
			{
				// Cancelado pelo utilizador
			}
		}

        /*
         * Usar caso se pretenda usar o PesquisaUFList (ver MenuItemPrint_Click()):
         * 
		private ArrayList GetLastSearchResults()
		{
			ArrayList IDsNiveis = new ArrayList();
			foreach (ListViewItem item in PesquisaUFList1.Items)
				IDsNiveis.Add(((GISADataset.NivelRow)item.Tag).ID);

			return IDsNiveis;
		}
         */

        private string GetTermosIndexados(GISADataset.FRDBaseRow FRDBaseRow, params TipoNoticiaAut[] TipoNoticiaAut)
		{
			string Result = "";
			ArrayList ResultArray = new ArrayList();
			foreach (GISADataset.IndexFRDCARow index in FRDBaseRow.GetIndexFRDCARows())
			{
				if (Array.IndexOf(TipoNoticiaAut, System.Enum.ToObject(typeof(Model.TipoNoticiaAut), index.ControloAutRow.IDTipoNoticiaAut)) >= TipoNoticiaAut.GetLowerBound(0))
				{
					foreach (GISADataset.ControloAutDicionarioRow cadr in index.ControloAutRow.GetControloAutDicionarioRows())
					{
						if (cadr.IDTipoControloAutForma == Convert.ToInt64(TipoControloAutForma.FormaAutorizada))
							ResultArray.Add(cadr.DicionarioRow.Termo);
					}
				}
			}
			ResultArray.Sort();
			foreach (string s in ResultArray)
			{
				if (Result.Length > 0)
					Result = Result + "\\li128\\par\\li0{}";

				Result += s;
			}

			if (Result.Length > 0)
				Result = Result + "\\li128\\par\\li0{}";

			return Result;
		}

		private string GetConditionalText(string Prefix, string Text, string Suffix)
		{
			if (Text.Length > 0)
				return Prefix + Text.Replace(System.Environment.NewLine, "\\par{}") + Suffix;
			else
				return "";
		}

		private string GetFRDBaseAsRTF(GISADataset.NivelRow NivelRow)
		{
			GISADataset.FRDBaseRow FRDBaseRow = null;

			GisaDataSetHelper.ManageDatasetConstraints(false);
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
				PesquisaRule.Current.LoadFRDBaseUFData(GisaDataSetHelper.GetInstance(), NivelRow.ID, ho.Connection);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw ex;
			}
			finally
			{
                ho.Dispose();
			}

			GisaDataSetHelper.ManageDatasetConstraints(true);

			StringBuilder Result = new StringBuilder();
			if (GisaDataSetHelper.GetInstance().FRDBase.Select("IDNivel=" + NivelRow.ID.ToString()).Length > 0)
			{
				FRDBaseRow = (GISADataset.FRDBaseRow)(GisaDataSetHelper.GetInstance().FRDBase.Select("IDNivel=" + NivelRow.ID.ToString())[0]);

				// --Identificação--
				Result.Append("\\fs36\\b{}Identificação\\b0{}\\fs24\\sb196\\sa48\\par{}\\sb0\\sa0{}");
				// Entidade Detentora
				if (FRDBaseRow.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().Length == 1)
					Result.Append(GetConditionalText("\\i{}Entidade detentora: \\i0{}", FRDBaseRow.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0]. NivelRowByNivelRelacaoHierarquicaUpper.GetNivelDesignadoRows()[0]. Designacao, "\\par{}"));
				// Codigo de Referência
				Result.Append(GetConditionalText("\\i{}Número de recenseamento: \\i0{}", FRDBaseRow.NivelRow.Codigo, "\\par{}"));
				// Cota
				if (FRDBaseRow.GetSFRDUFCotaRows().Length == 1)
				{
					if (! (FRDBaseRow.GetSFRDUFCotaRows()[0].IsCotaNull()))
						Result.Append(GetConditionalText("\\i{}Cota: \\i0{}", FRDBaseRow.GetSFRDUFCotaRows()[0].Cota, "\\par{}"));
					else
						Result.Append(GetConditionalText("\\i{}Cota: \\i0{}", "", "\\par{}"));
				}
				// Título
				Result.Append("\\i{}" + TipoNivelRelacionado.GetTipoNivelRelacionadoDaPrimeiraRelacaoEncontrada(FRDBaseRow.NivelRow).Designacao + ": \\i0{}" + Nivel.GetDesignacao(FRDBaseRow.NivelRow) + "\\par{}");
				// Datas
				if (FRDBaseRow.GetSFRDDatasProducaoRows().Length == 1)
                    Result.Append("\\i{}Datas: \\i0{}" + GUIHelper.GUIHelper.FormatDateInterval(FRDBaseRow.GetSFRDDatasProducaoRows()[0]) + "\\par{}");
                // Tipo e dimensões
				if (FRDBaseRow.GetSFRDUFDescricaoFisicaRows().Length == 1)
				{
					GISADataset.SFRDUFDescricaoFisicaRow sfrdufRow = null;
					string tipoMedida = null;
					sfrdufRow = FRDBaseRow.GetSFRDUFDescricaoFisicaRows()[0];
                    Result.Append("\\i{}Tipo: \\i0{}" + sfrdufRow.TipoAcondicionamentoRow.Designacao + "\\par{}");
					if (sfrdufRow.TipoMedidaRow == null)
						tipoMedida = string.Empty;
					else
						tipoMedida = sfrdufRow.TipoMedidaRow.Designacao;

					Result.Append("\\i{}Dimensões: \\i0{}" + string.Format("{0} x {1} x {2} ({3})", GISA.Utils.GUIHelper.getStringifiedDecimal(sfrdufRow["MedidaLargura"]), GISA.Utils.GUIHelper.getStringifiedDecimal(sfrdufRow["MedidaAltura"]), GISA.Utils.GUIHelper.getStringifiedDecimal(sfrdufRow["MedidaProfundidade"]), tipoMedida) + "\\par{}");
				}

				// --ConteudoEstrutura--
				string ConteudoEstrutura = "";
				if (FRDBaseRow.GetSFRDConteudoEEstruturaRows().Length == 1)
				{
                    GISADataset.SFRDConteudoEEstruturaRow ceRow = (GISADataset.SFRDConteudoEEstruturaRow) FRDBaseRow.GetSFRDConteudoEEstruturaRows()[0];

                    if (ceRow.IsConteudoInformacionalNull())
                        ConteudoEstrutura = GetConditionalText("\\i{}Conteúdo informacional: \\i0{}\\par{}", "", "\\li128\\par\\li0{}");
                    else
                        ConteudoEstrutura = GetConditionalText("\\i{}Conteúdo informacional: \\i0{}\\par{}", ceRow.ConteudoInformacional, "\\li128\\par\\li0{}");
				}

				Result.Append(Section("\\fs36\\b{}Conteúdo e estrutura\\b0{}\\fs24\\sb196\\sa48\\par{}\\sb0\\sa0{}", GetConditionalText("\\i{}Tipologia informacional: \\i0{}\\par{}", GetTermosIndexados(FRDBaseRow, TipoNoticiaAut.TipologiaInformacional), ""), ConteudoEstrutura));

				// --Documentação associada--

				if (FRDBaseRow.GetSFRDDocumentacaoAssociadaRows().Length > 0)
					Result.Append(Section("\\fs36\\b{}Documentação associada\\b0{}\\fs24\\sb196\\sa48\\par{}\\sb0\\sa0{}", GetConditionalText("\\i{}Existência e localização de originais: \\i0{}\\par{}", FRDBaseRow.GetSFRDDocumentacaoAssociadaRows()[0].ExistenciaDeOriginais, "\\li128\\par\\li0{}") + GetConditionalText("\\i{}Existência e localização de cópias: \\i0{}\\par{}", FRDBaseRow.GetSFRDDocumentacaoAssociadaRows()[0].ExistenciaDeCopias, "\\li128\\par\\li0{}")));

				// --Indexação--
				Result.Append(Section("\\fs36\\b{}Indexação\\b0{}\\fs24\\sb196\\sa48\\par{}\\sb0\\sa0{}", GetConditionalText("\\i{}Conteúdos: \\i0{}\\par{}", GetTermosIndexados(FRDBaseRow, TipoNoticiaAut.Ideografico, TipoNoticiaAut.Onomastico, TipoNoticiaAut.ToponimicoGeografico), "")));
			}
			return Result.ToString();
		}

		private string Section(string Header, params string[] Items)
		{
			string Result = string.Empty;
			foreach (string s in Items)
				Result += s;

			if (Result.Length > 0)
				return Header + Result;

			return "";
		}

		private void lstVwEstruturaDocs_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstVwEstruturaDocs.SelectedItems.Count == 1)
			{
				// é utilizada a primeira relação encontrada
                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
				try
				{
					GISADataset.NivelRow nRow = null;
					nRow = (GISADataset.NivelRow)(lstVwEstruturaDocs.SelectedItems[0].Tag);
					panelInfoEPs1.BuildTree(nRow, ho.Connection);
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
			else if (lstVwEstruturaDocs.SelectedItems.Count == 0)
				panelInfoEPs1.ClearAll();

			UpdateToolBarButtons();
		}

        /* NAO USADO; 
		private void PesquisaUFList1_BeforeNewListSelection(object sender, BeforeNewSelectionEventArgs e)
		{
			UpdateToolBarButtons();
		}
         * 
         */

        private void SelectionChanged(object sender, EventArgs e)
        {
            UpdateToolBarButtons();
        }


		private void UpdateToolBarButtons()
		{
			if (PesquisaUFList1.Items.Count > 0)
			{
				MenuItemPrintUnidadesFisicasDetalhadas.Enabled = true;
				MenuItemPrintUnidadesFisicasResumidas.Enabled = true;
			}
			else
			{
				MenuItemPrintUnidadesFisicasDetalhadas.Enabled = false;
				MenuItemPrintUnidadesFisicasResumidas.Enabled = false;
			}
			if (PesquisaUFList1.GetSelectedRows.Count() > 0)
			{
				ToolBarButton2.Enabled = true;
				ToolBarButton3.Enabled = true;
				ToolBarButton5.Enabled = true;
			}
			else
			{
				ToolBarButton2.Enabled = false;
				ToolBarButton3.Enabled = false;
				ToolBarButton5.Enabled = false;
			}
		}

		private void SlavePanelPesquisaUF_ParentChanged(object sender, System.EventArgs e)
		{
			if (this.Parent == null)
			{
				this.Visible = false;
				ViewToModel();
				Save();
				Deactivate();
			}
			else
			{
				this.Visible = true;
				LoadData();
				ModelToView();
			}
		}
	}
}
