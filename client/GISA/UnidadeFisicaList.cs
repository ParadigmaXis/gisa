using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

using GISA.Controls;

namespace GISA
{
	public class UnidadeFisicaList
#if DEBUG
 : MiddleClass 
#else  
 : PaginatedListView 
#endif
	{
        internal TextBox txtFiltroConteudo;
        internal Label lblConteudo;
        internal ColumnHeader chCodigoBarras;
        private TextBox txtFiltroCodigoBarras;
        private Label lblCodigoBarras;
        internal CheckBox chkFiltroUFsEliminadas;
        internal ColumnHeader chUFEmDeposito;
		public GISADataset.NivelRow ContextNivelRow = null;

	#region  Windows Form Designer generated code 

        public UnidadeFisicaList()
		{
            this.showItemsCount = true;

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            this.GrpResultadosLabel = "Unidades físicas encontradas";

            if (!SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsDepEnable())
            {
                this.lstVwPaginated.Columns.Remove(this.chUFEmDeposito);                
                this.chkFiltroUFsEliminadas.Visible = false;
            }

            GetExtraResources();
		}

		//UserControl overrides dispose to clean up the component list.
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
        internal System.Windows.Forms.TextBox txtFiltroDesignacao;
        internal System.Windows.Forms.CheckBox chkFiltroSoNaoAssociadas;
        internal System.Windows.Forms.Label lblFiltroDesignacao;
        internal System.Windows.Forms.ColumnHeader chID;
        internal System.Windows.Forms.ColumnHeader chCodigo;
		internal System.Windows.Forms.ColumnHeader chDesignacao;
		internal System.Windows.Forms.ColumnHeader chCota;
		internal System.Windows.Forms.ColumnHeader chProducao;
		internal System.Windows.Forms.ColumnHeader chNumUnidadesDescricao;
		internal System.Windows.Forms.Label lblFiltroCodigo;
		internal System.Windows.Forms.TextBox txtFiltroCodigo;
		internal System.Windows.Forms.Label lblCota;
		internal System.Windows.Forms.TextBox txtFiltroCota;
        [System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.chID = new System.Windows.Forms.ColumnHeader();
            this.chCodigo = new System.Windows.Forms.ColumnHeader();
            this.chDesignacao = new System.Windows.Forms.ColumnHeader();
            this.chCota = new System.Windows.Forms.ColumnHeader();
            this.chCodigoBarras = new System.Windows.Forms.ColumnHeader();
            this.chProducao = new System.Windows.Forms.ColumnHeader();
            this.chNumUnidadesDescricao = new System.Windows.Forms.ColumnHeader();
            this.chUFEmDeposito = new System.Windows.Forms.ColumnHeader();
            this.chkFiltroUFsEliminadas = new System.Windows.Forms.CheckBox();
            this.txtFiltroCodigoBarras = new System.Windows.Forms.TextBox();
            this.lblCodigoBarras = new System.Windows.Forms.Label();
            this.txtFiltroConteudo = new System.Windows.Forms.TextBox();
            this.lblConteudo = new System.Windows.Forms.Label();
            this.txtFiltroCota = new System.Windows.Forms.TextBox();
            this.lblCota = new System.Windows.Forms.Label();
            this.txtFiltroCodigo = new System.Windows.Forms.TextBox();
            this.lblFiltroCodigo = new System.Windows.Forms.Label();
            this.txtFiltroDesignacao = new System.Windows.Forms.TextBox();
            this.chkFiltroSoNaoAssociadas = new System.Windows.Forms.CheckBox();
            this.lblFiltroDesignacao = new System.Windows.Forms.Label();
            this.grpResultados.SuspendLayout();
            this.grpFiltro.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstVwUnidadesFisicas
            // 
            this.lstVwPaginated.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwPaginated.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chID,
            this.chCodigo,
            this.chDesignacao,
            this.chCota,
            this.chCodigoBarras,
            this.chProducao,
            this.chNumUnidadesDescricao,
            this.chUFEmDeposito});
            this.lstVwPaginated.CustomizedSorting = true;
            this.lstVwPaginated.FullRowSelect = true;
            this.lstVwPaginated.HideSelection = false;
            this.lstVwPaginated.Location = new System.Drawing.Point(8, 16);
            this.lstVwPaginated.MultiSelect = false;
            this.lstVwPaginated.Name = "lstVwUnidadesFisicas";
            this.lstVwPaginated.ReturnSubItemIndex = false;
            this.lstVwPaginated.Size = new System.Drawing.Size(791, 249);
            this.lstVwPaginated.TabIndex = 1;
            this.lstVwPaginated.UseCompatibleStateImageBehavior = false;
            this.lstVwPaginated.View = System.Windows.Forms.View.Details;
            this.lstVwPaginated.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lstVwUnidadesFisicas_MouseMove);
            // 
            // chID
            // 
            this.chID.Text = "Identificador";
            this.chID.Width = 80;
            // 
            // chCodigo
            // 
            this.chCodigo.Text = "Código";
            this.chCodigo.Width = 130;
            // 
            // chDesignacao
            // 
            this.chDesignacao.Text = "Título";
            this.chDesignacao.Width = 190;
            // 
            // chCota
            // 
            this.chCota.Text = "Cota";
            this.chCota.Width = 80;
            // 
            // chCodigoBarras
            // 
            this.chCodigoBarras.Text = "Código de barras";
            this.chCodigoBarras.Width = 100;
            // 
            // chProducao
            // 
            this.chProducao.Text = "Produção";
            this.chProducao.Width = 137;
            // 
            // chNumUnidadesDescricao
            // 
            this.chNumUnidadesDescricao.Text = "Nº unidades descrição";
            this.chNumUnidadesDescricao.Width = 120;
            // 
            // chUFEmDeposito
            // 
            this.chUFEmDeposito.Text = "Eliminada";
            this.chUFEmDeposito.Width = 80;
            // 
            // grpFiltro
            // 
            this.grpFiltro.Controls.Add(this.chkFiltroUFsEliminadas);
            this.grpFiltro.Controls.Add(this.txtFiltroCodigoBarras);
            this.grpFiltro.Controls.Add(this.lblCodigoBarras);
            this.grpFiltro.Controls.Add(this.txtFiltroConteudo);
            this.grpFiltro.Controls.Add(this.lblConteudo);
            this.grpFiltro.Controls.Add(this.txtFiltroCota);
            this.grpFiltro.Controls.Add(this.lblCota);
            this.grpFiltro.Controls.Add(this.txtFiltroCodigo);
            this.grpFiltro.Controls.Add(this.lblFiltroCodigo);
            this.grpFiltro.Controls.Add(this.btnAplicar);
            this.grpFiltro.Controls.Add(this.txtFiltroDesignacao);
            this.grpFiltro.Controls.Add(this.chkFiltroSoNaoAssociadas);
            this.grpFiltro.Controls.Add(this.lblFiltroDesignacao);
            this.grpFiltro.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFiltro.Location = new System.Drawing.Point(6, 6);
            this.grpFiltro.Name = "grpFiltro";
            this.grpFiltro.Size = new System.Drawing.Size(839, 59);
            this.grpFiltro.TabIndex = 1;
            this.grpFiltro.TabStop = false;
            this.grpFiltro.Text = "Filtro";
            // 
            // chkFiltroUFsEliminadas
            // 
            this.chkFiltroUFsEliminadas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkFiltroUFsEliminadas.Location = new System.Drawing.Point(656, 21);
            this.chkFiltroUFsEliminadas.Name = "chkFiltroUFsEliminadas";
            this.chkFiltroUFsEliminadas.Size = new System.Drawing.Size(105, 32);
            this.chkFiltroUFsEliminadas.TabIndex = 12;
            this.chkFiltroUFsEliminadas.Text = "Mostrar eliminadas";
            // 
            // txtFiltroCodigoBarras
            // 
            this.txtFiltroCodigoBarras.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFiltroCodigoBarras.Location = new System.Drawing.Point(295, 32);
            this.txtFiltroCodigoBarras.Name = "txtFiltroCodigoBarras";
            this.txtFiltroCodigoBarras.Size = new System.Drawing.Size(87, 20);
            this.txtFiltroCodigoBarras.TabIndex = 11;
            this.txtFiltroCodigoBarras.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // lblCodigoBarras
            // 
            this.lblCodigoBarras.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCodigoBarras.AutoSize = true;
            this.lblCodigoBarras.Location = new System.Drawing.Point(295, 16);
            this.lblCodigoBarras.Name = "lblCodigoBarras";
            this.lblCodigoBarras.Size = new System.Drawing.Size(87, 13);
            this.lblCodigoBarras.TabIndex = 10;
            this.lblCodigoBarras.Text = "Código de barras";
            // 
            // txtFiltroConteudo
            // 
            this.txtFiltroConteudo.AcceptsReturn = true;
            this.txtFiltroConteudo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFiltroConteudo.Location = new System.Drawing.Point(388, 32);
            this.txtFiltroConteudo.Name = "txtFiltroConteudo";
            this.txtFiltroConteudo.Size = new System.Drawing.Size(159, 20);
            this.txtFiltroConteudo.TabIndex = 9;
            this.txtFiltroConteudo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // lblConteudo
            // 
            this.lblConteudo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConteudo.Location = new System.Drawing.Point(388, 16);
            this.lblConteudo.Name = "lblConteudo";
            this.lblConteudo.Size = new System.Drawing.Size(159, 16);
            this.lblConteudo.TabIndex = 8;
            this.lblConteudo.Text = "Conteúdo";
            // 
            // txtFiltroCota
            // 
            this.txtFiltroCota.AcceptsReturn = true;
            this.txtFiltroCota.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFiltroCota.Location = new System.Drawing.Point(220, 32);
            this.txtFiltroCota.Name = "txtFiltroCota";
            this.txtFiltroCota.Size = new System.Drawing.Size(69, 20);
            this.txtFiltroCota.TabIndex = 7;
            this.txtFiltroCota.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // lblCota
            // 
            this.lblCota.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCota.Location = new System.Drawing.Point(220, 16);
            this.lblCota.Name = "lblCota";
            this.lblCota.Size = new System.Drawing.Size(69, 16);
            this.lblCota.TabIndex = 6;
            this.lblCota.Text = "Cota";
            // 
            // txtFiltroCodigo
            // 
            this.txtFiltroCodigo.AcceptsReturn = true;
            this.txtFiltroCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFiltroCodigo.Location = new System.Drawing.Point(83, 32);
            this.txtFiltroCodigo.Name = "txtFiltroCodigo";
            this.txtFiltroCodigo.Size = new System.Drawing.Size(131, 20);
            this.txtFiltroCodigo.TabIndex = 5;
            this.txtFiltroCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // lblFiltroCodigo
            // 
            this.lblFiltroCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFiltroCodigo.Location = new System.Drawing.Point(83, 16);
            this.lblFiltroCodigo.Name = "lblFiltroCodigo";
            this.lblFiltroCodigo.Size = new System.Drawing.Size(131, 16);
            this.lblFiltroCodigo.TabIndex = 4;
            this.lblFiltroCodigo.Text = "Código";
            // 
            // txtFiltroDesignacao
            // 
            this.txtFiltroDesignacao.AcceptsReturn = true;
            this.txtFiltroDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFiltroDesignacao.Location = new System.Drawing.Point(8, 32);
            this.txtFiltroDesignacao.Name = "txtFiltroDesignacao";
            this.txtFiltroDesignacao.Size = new System.Drawing.Size(69, 20);
            this.txtFiltroDesignacao.TabIndex = 1;
            this.txtFiltroDesignacao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // chkFiltroSoNaoAssociadas
            // 
            this.chkFiltroSoNaoAssociadas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkFiltroSoNaoAssociadas.Location = new System.Drawing.Point(553, 21);
            this.chkFiltroSoNaoAssociadas.Name = "chkFiltroSoNaoAssociadas";
            this.chkFiltroSoNaoAssociadas.Size = new System.Drawing.Size(97, 32);
            this.chkFiltroSoNaoAssociadas.TabIndex = 2;
            this.chkFiltroSoNaoAssociadas.Text = "Sem unidades descrição";
            // 
            // lblFiltroDesignacao
            // 
            this.lblFiltroDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFiltroDesignacao.Location = new System.Drawing.Point(5, 16);
            this.lblFiltroDesignacao.Name = "lblFiltroDesignacao";
            this.lblFiltroDesignacao.Size = new System.Drawing.Size(72, 16);
            this.lblFiltroDesignacao.TabIndex = 0;
            this.lblFiltroDesignacao.Text = "Título";
            // 
            // UnidadeFisicaList
            // 
            this.Name = "UnidadeFisicaList";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.Size = new System.Drawing.Size(851, 344);
            this.grpResultados.ResumeLayout(false);
            this.grpFiltro.ResumeLayout(false);
            this.grpFiltro.PerformLayout();
            this.ResumeLayout(false);

		}

	#endregion

        #region Filtros
        private string FiltroDesignacaoLike
        {
            get
            {
                return txtFiltroDesignacao.Text.Length == 0 ? string.Empty :
                    DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.buildLikeStatement("Designacao", string.Format("'{0}'", DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.sanitizeSearchTerm(txtFiltroDesignacao.Text)));
            }
        }

        private string FiltroCodigoLike
        {
            get
            {
                return txtFiltroCodigo.Text.Length == 0 ? string.Empty :
                    DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.buildLikeStatement("(nED.Codigo + '/' + Nivel.Codigo)", string.Format("'{0}'", DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.sanitizeSearchTerm(txtFiltroCodigo.Text)));
            }
        }

        private string FiltroCotaLike
        {
            get
            {
                return txtFiltroCota.Text.Length == 0 ? string.Empty :
                    DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.buildLikeStatement("SFRDUFCota.Cota", string.Format("'{0}'", DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.sanitizeSearchTerm(txtFiltroCota.Text)));
            }
        }

        private string FiltroConteudoLike
        {
            get
            {
                return txtFiltroConteudo.Text.Length == 0 ? string.Empty :
                    DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.buildLikeStatement("SFRDConteudoEEstrutura.ConteudoInformacional", string.Format("'{0}'", DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.sanitizeSearchTerm(txtFiltroConteudo.Text)));
            }
        }

        private string FiltroCodigoBarrasLike
        {
            get
            {
                return txtFiltroCodigoBarras.Text.Length == 0 ? string.Empty :
                    DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.buildLikeStatement("CONVERT(NVARCHAR, NivelUnidadeFisica.CodigoBarras)", string.Format("'{0}'", DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.sanitizeSearchTerm(txtFiltroCodigoBarras.Text)));
            }
        }

        private string FiltroAndParentNivel
        {
            get
            {
                if (this.ContextNivelRow == null) return string.Empty;

                // Podem existir vários detentores, e precisamos de todos eles
                System.Text.StringBuilder result = null;
                ArrayList entidadesDetentoras = null;
                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try
                {
                    entidadesDetentoras = UFRule.Current.GetEntidadeDetentoraForNivel(this.ContextNivelRow.ID, ho.Connection);
                }
                finally
                {
                    ho.Dispose();
                }
                foreach (long edID in entidadesDetentoras)
                {
                    if (result == null)
                    {
                        result = new System.Text.StringBuilder();
                        result.Append(" AND (");
                    }
                    else
                        result.Append(" OR ");

                    result.AppendFormat("ParentNivel.ID = {0}", edID);
                }

                if (result != null)
                {
                    result.Append(")");
                    return result.ToString();
                }

                return string.Empty;
            }
        }

        public void ClearFilter()
        {
            txtFiltroDesignacao.Text = string.Empty;
            txtFiltroCodigo.Text = string.Empty;
            txtFiltroCota.Text = string.Empty;
            txtFiltroCodigoBarras.Text = string.Empty;
            txtFiltroConteudo.Text = string.Empty;
            chkFiltroSoNaoAssociadas.CheckState = CheckState.Unchecked;
            chkFiltroUFsEliminadas.CheckState = CheckState.Unchecked;
        }
        #endregion

        #region ListView
        protected override void CalculateOrderedItems(IDbConnection connection)
        {
            ArrayList ordenacao = this.GetListSortDef();

            if (ordenacao.Count == 0)
            {
                // definir como ordenação por omissão código descendente
                ordenacao.Add(this.chID.Index);
                ordenacao.Add(false);
            }

            UFRule.Current.CalculateOrderedItems(TipoNivelRelacionado.UF, FiltroDesignacaoLike, FiltroCodigoLike, FiltroCotaLike, FiltroCodigoBarrasLike, FiltroConteudoLike, FiltroAndParentNivel, this.chkFiltroSoNaoAssociadas.Checked, this.chkFiltroUFsEliminadas.Checked, ordenacao, connection);
        }

        private PaginatedLVGetItems returnedInfo;
        protected override void GetItems(int pageNr, int itemsPerPage, IDbConnection connection)
        {
            ArrayList rowIds = new ArrayList();
            rowIds = UFRule.Current.GetItems(GisaDataSetHelper.GetInstance(), pageNr, itemsPerPage, connection);
            PaginatedLVGetItemsUF items = new PaginatedLVGetItemsUF(rowIds);
            returnedInfo = items;
        }

        protected override void DeleteTemporaryResults(IDbConnection connection)
        {
            UFRule.Current.DeleteTemporaryResults(connection);
        }

        protected override void AddItemsToList()
        {
            ArrayList itemsToBeAdded = new ArrayList();
            itemsToBeAdded.Clear();
            if (returnedInfo.rowsInfo != null)
            {
                GISADataset.TipoNivelRelacionadoRow tnrRow = (GISADataset.TipoNivelRelacionadoRow)(GisaDataSetHelper.GetInstance().TipoNivelRelacionado.Select("ID=" + TipoNivelRelacionado.UF.ToString())[0]);
                foreach (ArrayList rowInfo in returnedInfo.rowsInfo)
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.AddRange(new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });
                    item.Tag = GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + rowInfo[0].ToString())[0];
                    item.SubItems[this.chID.Index].Text = rowInfo[0].ToString();
                    item.SubItems[this.chCodigo.Index].Text = rowInfo[1].ToString();
                    item.SubItems[this.chDesignacao.Index].Text = rowInfo[2].ToString();
                    item.SubItems[this.chCota.Index].Text = rowInfo[3].ToString();
                    item.SubItems[this.chCodigoBarras.Index].Text = rowInfo[4].ToString();
                    item.SubItems[this.chProducao.Index].Text = GISA.Utils.GUIHelper.FormatDateInterval(rowInfo[5].ToString(), rowInfo[6].ToString(), rowInfo[7].ToString(), System.Convert.ToBoolean(((rowInfo[8] == DBNull.Value) ? false : rowInfo[8])), rowInfo[9].ToString(), rowInfo[10].ToString(), rowInfo[11].ToString(), System.Convert.ToBoolean(((rowInfo[12] == DBNull.Value) ? false : rowInfo[12])));
                    item.SubItems[this.chNumUnidadesDescricao.Index].Text = rowInfo[13].ToString();
                    // Em deposito:
                    if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsDepEnable())
                    {
                        if ((bool)rowInfo[14]) // Eliminado ?
                        {
                            item.SubItems[this.chUFEmDeposito.Index].Text = rowInfo[15].ToString();
                            item.Font = new Font(item.Font, FontStyle.Strikeout);
                        }
                        else
                            item.SubItems[this.chUFEmDeposito.Index].Text = "Não";
                    }
                    item.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageBase(System.Convert.ToInt32(tnrRow.GUIOrder));
                    item.StateImageIndex = 0;
                    itemsToBeAdded.Add(item);
                }

                if (itemsToBeAdded.Count > 0)
                    this.lstVwPaginated.Items.AddRange((ListViewItem[])(itemsToBeAdded.ToArray(typeof(ListViewItem))));
            }
        }
        #endregion

        protected override void GetExtraResources()
        {
            base.GetExtraResources();
            lstVwPaginated.SmallImageList = TipoNivelRelacionado.GetImageList();
        }
        
        public bool DefineShowItemToolTips { get { return this.lstVwPaginated.ShowItemToolTips; } set { this.lstVwPaginated.ShowItemToolTips = value; } }
        
        private ListViewItem lstVwUnidadesFisicas_MouseMove_previousItem;
        private void lstVwUnidadesFisicas_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.lstVwPaginated.ShowItemToolTips = true;
            // Find the item under the mouse.
            ListViewItem currentItem = (ListViewItem)(this.lstVwPaginated.GetItemAt(e.X, e.Y));

            if (currentItem == lstVwUnidadesFisicas_MouseMove_previousItem)
                return;

            lstVwUnidadesFisicas_MouseMove_previousItem = currentItem;

            // See if we have a valid item under mouse pointer
            if (currentItem != null)
                currentItem.ToolTipText = UnidadesFisicasHelper.GetConteudoInformacional(GisaDataSetHelper.GetInstance(), currentItem.Tag as GISADataset.NivelRow);
        }

		private void btnAplicar_Click(object Sender, EventArgs e)
		{
			ReloadList();
		}

		public void AddNivel(GISADataset.NivelRow NivelRow)
		{
			ListViewItem lvItem = null;
			ReloadList(NivelRow);
            lvItem = GUIHelper.GUIHelper.findListViewItemByTag(NivelRow, this.lstVwPaginated);

			// prever a situação em que o filtro está activo e o elemento a adicionar não respeita o critério desse filtro
			if (lvItem == null)
			{
				if (MessageBox.Show("A unidade física que pretende adicionar não respeita os critérios " + System.Environment.NewLine + "definidos no filtro e por esse motivo não poderá ser apresentada. " + System.Environment.NewLine + "Pretende limpar os critérios do filtro para dessa forma poder visualizar a unidade física criada?", "Mostrar Nova Unidade Física", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					ClearFilter();
					ReloadList(NivelRow);
                    lvItem = GUIHelper.GUIHelper.findListViewItemByTag(NivelRow, this.lstVwPaginated);
                    SelectItem(lvItem);
                    this.lstVwPaginated.EnsureVisible(lvItem.Index);
				}
				else
					ReloadList();
			}
			else
			{
				SelectItem(lvItem);
                this.lstVwPaginated.EnsureVisible(lvItem.Index);
			}
		}

		public ListViewItem UpdateNivel(ListViewItem lvItem)
		{
			return UpdateNivel(lvItem, null, null);
		}

		public ListViewItem UpdateNivel(ListViewItem lvItem, string cod, string nAssoc)
		{
			GISADataset.NivelRow NivelRow = null;
			NivelRow = (GISADataset.NivelRow)lvItem.Tag;

			string codigo = null;
			string numUnidadesDescricao = null;

			if (cod == null && nAssoc == null)
			{
				GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
				try
				{
					codigo = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.GetCodigoOfNivel(NivelRow.ID, ho.Connection)[0].ToString();
					numUnidadesDescricao = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.getUnidadesDescricaoCountForUnidadeFisica(NivelRow.ID, ho.Connection).ToString();
				}
				catch (Exception ex)
				{
					Trace.WriteLine(ex.ToString());
					throw ex;
				}
				finally
				{
					ho.Dispose();
				}
			}
			else
			{
				codigo = cod;
				numUnidadesDescricao = nAssoc;
			}

			if (codigo != null)
				lvItem.SubItems[chCodigo.Index].Text = codigo;

			lvItem.SubItems[chDesignacao.Index].Text = NivelRow.GetNivelDesignadoRows()[0].Designacao;
            if (!NivelRow.GetNivelDesignadoRows()[0].GetNivelUnidadeFisicaRows()[0].IsCodigoBarrasNull())
                lvItem.SubItems[chCodigoBarras.Index].Text = NivelRow.GetNivelDesignadoRows()[0].GetNivelUnidadeFisicaRows()[0].CodigoBarras.ToString();
            else
                lvItem.SubItems[chCodigoBarras.Index].Text = "";

			GISADataset.FRDBaseRow[] FRDs = NivelRow.GetFRDBaseRows();
			if (FRDs.Length == 1)
			{
				GISADataset.SFRDDatasProducaoRow[] DPs = FRDs[0].GetSFRDDatasProducaoRows();
				if (DPs.Length == 1)
                    lvItem.SubItems[chProducao.Index].Text = GUIHelper.GUIHelper.FormatDateInterval(DPs[0]);
				else
					lvItem.SubItems[chProducao.Index].Text = string.Empty;

				GISADataset.SFRDUFCotaRow[] Cs = FRDs[0].GetSFRDUFCotaRows();
				if (Cs.Length == 1 && ! (Cs[0].IsCotaNull()))
					lvItem.SubItems[chCota.Index].Text = Cs[0].Cota;
				else
					lvItem.SubItems[chCota.Index].Text = string.Empty;
			}
			else if (FRDs.Length == 0)
			{
				lvItem.SubItems[chProducao.Index].Text = "";
				lvItem.SubItems[chCota.Index].Text = "";
			}
			else
			{
				// this scenario must never happen
				Trace.WriteLine("Encontrado nível com mais que um FRD (!) Foi assumido o primeiro encontrado.");
			}

			if (numUnidadesDescricao != null)
				lvItem.SubItems[chNumUnidadesDescricao.Index].Text = numUnidadesDescricao;

			return lvItem;
		}
	}
}