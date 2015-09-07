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

using GISA.Controls;

namespace GISA.Controls.Nivel
{
	public class NivelDocumentalList
#if DEBUG
 : MiddleClass
#else  
 : PaginatedListView 
#endif
	{
        protected PaginatedLVGetItems returnedInfo;
        private ListViewItem lstVwNiveisDocumentais_MouseMove_previousItem;

	#region  Windows Form Designer generated code 

		public NivelDocumentalList() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            lstVwPaginated.MouseMove += lstVwNiveisDocumentais_MouseMove;
            this.VisibleChanged += new EventHandler(nDocList_VisibleChanged);

            lstVwPaginated.CustomizedSorting = true;
            txtFiltroCodigoParcial.KeyDown += new KeyEventHandler(this.txtBox_KeyDown);
            txtFiltroConteudo.KeyDown += new KeyEventHandler(this.txtBox_KeyDown);
            txtFiltroDesignacao.KeyDown += new KeyEventHandler(this.txtBox_KeyDown);
            txtFiltroID.PropagateEnterPressed += new PxIntegerBox.PropagateEnterPressedEventHandler(this.txtID_KeyDown);
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

        private System.ComponentModel.IContainer components = null;

        //Required by the Windows Form Designer

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
        protected System.Windows.Forms.ColumnHeader colDesignacao;
        protected System.Windows.Forms.ColumnHeader colDataProducao;
        protected TableLayoutPanel tableLayoutPanel1;
        protected Label lblFiltroDesignacao;
        protected TextBox txtFiltroCodigoParcial;
        protected Label lblCodigoParcial;
        protected TextBox txtFiltroDesignacao;
        protected Label lblID;
        protected Label lblConteudo;
        protected TextBox txtFiltroConteudo;
        protected PxIntegerBox txtFiltroID;
        protected ColumnHeader colID;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblFiltroDesignacao = new System.Windows.Forms.Label();
            this.txtFiltroCodigoParcial = new System.Windows.Forms.TextBox();
            this.lblCodigoParcial = new System.Windows.Forms.Label();
            this.txtFiltroDesignacao = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.lblConteudo = new System.Windows.Forms.Label();
            this.txtFiltroConteudo = new System.Windows.Forms.TextBox();
            this.txtFiltroID = new GISA.Controls.PxIntegerBox();
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDesignacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDataProducao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpResultados.SuspendLayout();
            this.grpFiltro.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpResultados
            // 
            this.grpResultados.Location = new System.Drawing.Point(6, 69);
            this.grpResultados.Size = new System.Drawing.Size(620, 309);
            // 
            // txtNroPagina
            // 
            this.txtNroPagina.Location = new System.Drawing.Point(588, 62);
            // 
            // lstVwPaginated
            // 
            this.lstVwPaginated.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colID,
            this.colDesignacao,
            this.colDataProducao});
            this.lstVwPaginated.Size = new System.Drawing.Size(574, 287);
            // 
            // btnProximo
            // 
            this.btnProximo.Location = new System.Drawing.Point(588, 90);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Location = new System.Drawing.Point(588, 30);
            // 
            // grpFiltro
            // 
            this.grpFiltro.Controls.Add(this.tableLayoutPanel1);
            this.grpFiltro.Location = new System.Drawing.Point(6, 0);
            this.grpFiltro.Size = new System.Drawing.Size(620, 69);
            this.grpFiltro.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.grpFiltro.Controls.SetChildIndex(this.btnAplicar, 0);
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(548, 35);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel1.Controls.Add(this.lblFiltroDesignacao, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtFiltroCodigoParcial, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCodigoParcial, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtFiltroDesignacao, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblConteudo, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtFiltroConteudo, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtFiltroID, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 18);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.77778F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.22222F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(534, 45);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // lblFiltroDesignacao
            // 
            this.lblFiltroDesignacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFiltroDesignacao.Location = new System.Drawing.Point(3, 0);
            this.lblFiltroDesignacao.Name = "lblFiltroDesignacao";
            this.lblFiltroDesignacao.Size = new System.Drawing.Size(239, 17);
            this.lblFiltroDesignacao.TabIndex = 0;
            this.lblFiltroDesignacao.Text = "Título";
            // 
            // txtFiltroCodigoParcial
            // 
            this.txtFiltroCodigoParcial.AcceptsReturn = true;
            this.txtFiltroCodigoParcial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFiltroCodigoParcial.Location = new System.Drawing.Point(321, 20);
            this.txtFiltroCodigoParcial.Name = "txtFiltroCodigoParcial";
            this.txtFiltroCodigoParcial.Size = new System.Drawing.Size(83, 20);
            this.txtFiltroCodigoParcial.TabIndex = 3;
            // 
            // lblCodigoParcial
            // 
            this.lblCodigoParcial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCodigoParcial.Location = new System.Drawing.Point(321, 0);
            this.lblCodigoParcial.Name = "lblCodigoParcial";
            this.lblCodigoParcial.Size = new System.Drawing.Size(83, 17);
            this.lblCodigoParcial.TabIndex = 4;
            this.lblCodigoParcial.Text = "Código Parcial";
            // 
            // txtFiltroDesignacao
            // 
            this.txtFiltroDesignacao.AcceptsReturn = true;
            this.txtFiltroDesignacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFiltroDesignacao.Location = new System.Drawing.Point(3, 20);
            this.txtFiltroDesignacao.Name = "txtFiltroDesignacao";
            this.txtFiltroDesignacao.Size = new System.Drawing.Size(239, 20);
            this.txtFiltroDesignacao.TabIndex = 1;
            // 
            // lblID
            // 
            this.lblID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblID.Location = new System.Drawing.Point(248, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(67, 17);
            this.lblID.TabIndex = 6;
            this.lblID.Text = "Identificador";
            // 
            // lblConteudo
            // 
            this.lblConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblConteudo.Location = new System.Drawing.Point(410, 0);
            this.lblConteudo.Name = "lblConteudo";
            this.lblConteudo.Size = new System.Drawing.Size(121, 17);
            this.lblConteudo.TabIndex = 7;
            this.lblConteudo.Text = "Conteúdo";
            // 
            // txtFiltroConteudo
            // 
            this.txtFiltroConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFiltroConteudo.Location = new System.Drawing.Point(410, 20);
            this.txtFiltroConteudo.Name = "txtFiltroConteudo";
            this.txtFiltroConteudo.Size = new System.Drawing.Size(121, 20);
            this.txtFiltroConteudo.TabIndex = 8;
            // 
            // txtFiltroID
            // 
            this.txtFiltroID.Location = new System.Drawing.Point(248, 20);
            this.txtFiltroID.Name = "txtFiltroID";
            this.txtFiltroID.Size = new System.Drawing.Size(67, 20);
            this.txtFiltroID.TabIndex = 9;
            this.txtFiltroID.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // colID
            // 
            this.colID.Text = "Identificador";
            this.colID.Width = 77;
            // 
            // colDesignacao
            // 
            this.colDesignacao.Text = "Título";
            this.colDesignacao.Width = 437;
            // 
            // colDataProducao
            // 
            this.colDataProducao.Text = "Data de Produção";
            this.colDataProducao.Width = 138;
            // 
            // NivelDocumentalList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.FilterVisible = true;
            this.Name = "NivelDocumentalList";
            this.Padding = new System.Windows.Forms.Padding(6, 0, 6, 6);
            this.Size = new System.Drawing.Size(632, 384);
            this.grpResultados.ResumeLayout(false);
            this.grpFiltro.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

		}

      
	#endregion

        

        void txtID_KeyDown(object Sender, EventArgs e)
        {
            this.ApplyFilter();
        }

        void nDocList_VisibleChanged(object sender, EventArgs e)
        {
            if ((this.Visible) && (this.FiltroDesignacaoLike != string.Empty) && this.IsParentSupport)
            {
                this.ClearFilter();
                this.ReloadList();
            }
            else
            {
                if ((!this.Visible) && (this.TopLevelControl != null) && this.IsParentSupport)
                {
                    this.ClearFilter();
                    this.ReloadList();
                }
            }
        }

		protected override void GetExtraResources()
		{
            base.GetExtraResources();
			lstVwPaginated.SmallImageList = TipoNivelRelacionado.GetImageList();
		}

        public virtual string FiltroDesignacaoLike
		{
			get
			{
                return txtFiltroDesignacao.Text.Length == 0 ? string.Empty :
                    txtFiltroDesignacao.Text;
                    //DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.buildLikeStatement("Designacao", string.Format("'{0}'", DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.sanitizeSearchTerm(txtFiltroDesignacao.Text)));
			}
		}

        public virtual string FiltroCodigoParcialLike
		{
			get
			{
                return txtFiltroCodigoParcial.Text.Length == 0 ? string.Empty :
                    txtFiltroCodigoParcial.Text;
                    //DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.buildLikeStatement("n.Codigo", string.Format("'{0}'", DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.sanitizeSearchTerm(txtFiltroCodigoParcial.Text)));
			}
		}

        public virtual string FiltroIDLike
        {
            get
            {
                if (this.txtFiltroID.Text == null || this.txtFiltroID.Text.Length == 0)
                    return string.Empty;
                else
                    return txtFiltroID.Text;
            }
        }

        public virtual string FiltroConteudoLike
        {
            get
            {
                return txtFiltroConteudo.Text.Length == 0 ? string.Empty :
                    txtFiltroConteudo.Text;
            }
        }

        private long idMovimento;
        public long IDMovimento
        {
            get { return idMovimento; }
            set { this.idMovimento = value; }
        }

        protected override void ClearFilter()
		{
            txtFiltroDesignacao.Text = string.Empty;
            txtFiltroCodigoParcial.Text = string.Empty;
            txtFiltroID.Text = string.Empty;
            txtFiltroConteudo.Text = string.Empty;
		}

        private bool mIsParentSupport = false;
        public bool IsParentSupport
        {
            get { return mIsParentSupport; }
            set { mIsParentSupport = value; this.lstVwPaginated.MultiSelect = value; }
        }

		public delegate void setToolTipEventEventHandler(object sender, object item);
		public event setToolTipEventEventHandler setToolTipEvent;
		private void lstVwNiveisDocumentais_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
            lstVwPaginated.ShowItemToolTips = true;
			// Find the item under the mouse.
			ListViewItem currentItem = (ListViewItem)(lstVwPaginated.GetItemAt(e.X, e.Y));

			if (currentItem == lstVwNiveisDocumentais_MouseMove_previousItem)
				return;

			lstVwNiveisDocumentais_MouseMove_previousItem = currentItem;

			if (setToolTipEvent != null)
                setToolTipEvent(this, currentItem);
		}

		protected override void CalculateOrderedItems(IDbConnection connection)
		{
            ArrayList ordenacao = this.GetListSortDef();
            NivelRule.Current.CalculateOrderedItems(ordenacao, FiltroDesignacaoLike, FiltroCodigoParcialLike, FiltroIDLike, FiltroConteudoLike, idMovimento, connection);
		}

        protected override void GetItems(int pageNr, int itemsPerPage, IDbConnection connection)
		{
            returnedInfo = new PaginatedLVGetItemsNvDoc(NivelRule.Current.GetItems(GisaDataSetHelper.GetInstance(), pageNr, TipoNivel.OUTRO, itemsPerPage, connection));
		}

        protected override void DeleteTemporaryResults(IDbConnection connection)
		{
            NivelRule.Current.DeleteTemporaryResults(connection);
		}

        protected override void AddItemsToList()
		{
            ArrayList itemsToBeAdded = new ArrayList();
            ListViewItem item = null;
            if (returnedInfo.rowsInfo != null)
            {
                foreach (NivelRule.NivelDocumentalListItem row in returnedInfo.rowsInfo)
                {
                    item = this.GetNewNivelItem(row);
                    itemsToBeAdded.Add(item);
                }

                if (itemsToBeAdded.Count > 0)
                    this.Items.AddRange((ListViewItem[])(itemsToBeAdded.ToArray(typeof(ListViewItem))));
            }
		}

		public GISADataset.NivelRow GetSelectedNivel()
		{
            return lstVwPaginated.SelectedItems.Count > 0 ? lstVwPaginated.SelectedItems[0].Tag as GISADataset.NivelRow : null;
		}

        public List<GISADataset.NivelRow> GetSelectedNiveis()
        {
            return lstVwPaginated.SelectedItems.Cast<ListViewItem>().Select(item => item.Tag as GISADataset.NivelRow).ToList();
        }

		//método chamdo aquando da eliminação de um nivel
		public void RemoveSelectedItem()
		{
			ListViewItem item = lstVwPaginated.SelectedItems[0];
			lstVwPaginated.clearItemSelection(item);
			lstVwPaginated.Items.Remove(item);
		}

        public virtual void InitialLoad(GISADataset.RelacaoHierarquicaRow rhRow)
		{
			//resetList();
            this.txtFiltroDesignacao.Text = string.Empty;
            NivelRule.Current.LoadDesignacaoNivel(rhRow.ID, GisaDataSetHelper.GetInstance(), GisaDataSetHelper.GetConnection());
			LoadListData();
		}

		public void AddNivel(GISADataset.NivelRow NivelRow)
		{
			ListViewItem lvItem = null;
			ReloadList(NivelRow);
            lvItem = GUIHelper.GUIHelper.findListViewItemByTag(NivelRow, lstVwPaginated);

			// prever a situação em que o filtro está activo e o elemento a adicionar não respeita o critério desse filtro
			if (lvItem == null)
			{
				if (MessageBox.Show("A unidade de descrição que pretende adicionar não respeita os critérios " + System.Environment.NewLine + "definidos no filtro e por esse motivo não poderá ser apresentada. " + System.Environment.NewLine + "Pretende limpar os critérios do filtro para dessa forma poder visualizar a unidade de descrição criada?", "Mostrar nível documental", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					ClearFilter();
					ReloadList(NivelRow);
                    lvItem = GUIHelper.GUIHelper.findListViewItemByTag(NivelRow, lstVwPaginated);
					lstVwPaginated.selectItem(lvItem);
					lstVwPaginated.EnsureVisible(lvItem.Index);
				}
				else
					ReloadList();
			}
			else
			{
				lstVwPaginated.selectItem(lvItem);
				lstVwPaginated.EnsureVisible(lvItem.Index);
			}
		}

        protected virtual ListViewItem GetNewNivelItem(NivelRule.NivelDocumentalListItem item)
        {
            ListViewItem lvItem = new ListViewItem();
            
            lvItem.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageBase(item.GUIOrder);
            lvItem.StateImageIndex = 0;
            lvItem.SubItems.AddRange(new string[] { string.Empty, string.Empty, string.Empty, string.Empty });
            lvItem.Tag = GisaDataSetHelper.GetInstance().Nivel.Select("ID="+item.IDNivel.ToString())[0];

            lvItem.SubItems[this.colID.Index].Text = item.IDNivel.ToString();
            lvItem.SubItems[colDesignacao.Index].Text = item.Designacao;
            lvItem.SubItems[colDataProducao.Index].Text = GISA.Utils.GUIHelper.FormatDateInterval(
                item.InicioAno, item.InicioMes, item.InicioDia, item.InicioAtribuida,
                item.FimAno, item.FimMes, item.FimDia, item.FimAtribuida);
            
            return lvItem;
        }       

		public void UpdateSelectedNodeName(string newName)
		{
			this.lstVwPaginated.SelectedItems[0].SubItems[1].Text = newName;
		}

		public virtual ArrayList GetCodigoCompletoCaminhoUnico(ListViewItem item)
		{
            return new ArrayList();
		}
	}
}
