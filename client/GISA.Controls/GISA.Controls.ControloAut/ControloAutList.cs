using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.Controls;
using System.Text;
using GISA.SharedResources;
using GISA.GUIHelper;

namespace GISA.Controls.ControloAut
{
	public class ControloAutList
#if DEBUG
 : MiddleClass 
#else  
 : PaginatedListView 
#endif
	{
        private PaginatedLVGetItems returnedInfo;

	#region  Windows Form Designer generated code 

		public ControloAutList() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			this.GrpResultadosLabel = "Notícias de autoridade encontradas";
            this.lstVwPaginated.CustomizedSorting = false;
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
        public System.Windows.Forms.ColumnHeader colDesignacao;
        public System.Windows.Forms.ColumnHeader colNoticiaAut;
        public System.Windows.Forms.ColumnHeader colValidado;
        public System.Windows.Forms.ColumnHeader colCompleto;
        public System.Windows.Forms.ComboBox cbNoticiaAut;
		public System.Windows.Forms.Label lblNoticiaAut;
		public System.Windows.Forms.TextBox txtFiltroDesignacao;
        public System.Windows.Forms.Label lblFiltroDesignacao;
        public System.Windows.Forms.ToolTip ToolTip;
        public System.Windows.Forms.ColumnHeader colDatasExistencia;
        public System.Windows.Forms.CheckBox chkValidado;
        [System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.colDesignacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNoticiaAut = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colValidado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCompleto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDatasExistencia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkValidado = new System.Windows.Forms.CheckBox();
            this.cbNoticiaAut = new System.Windows.Forms.ComboBox();
            this.lblNoticiaAut = new System.Windows.Forms.Label();
            this.txtFiltroDesignacao = new System.Windows.Forms.TextBox();
            this.lblFiltroDesignacao = new System.Windows.Forms.Label();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grpResultados.SuspendLayout();
            this.grpFiltro.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpResultados
            // 
            this.grpResultados.Size = new System.Drawing.Size(620, 273);
            // 
            // txtNroPagina
            // 
            this.txtNroPagina.Location = new System.Drawing.Point(588, 64);
            // 
            // lstVwPaginated
            // 
            this.lstVwPaginated.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDesignacao,
            this.colNoticiaAut,
            this.colValidado,
            this.colCompleto,
            this.colDatasExistencia});
            this.lstVwPaginated.Size = new System.Drawing.Size(574, 249);
            // 
            // btnProximo
            // 
            this.btnProximo.Location = new System.Drawing.Point(588, 92);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Location = new System.Drawing.Point(588, 32);
            // 
            // grpFiltro
            // 
            this.grpFiltro.Controls.Add(this.chkValidado);
            this.grpFiltro.Controls.Add(this.cbNoticiaAut);
            this.grpFiltro.Controls.Add(this.lblNoticiaAut);
            this.grpFiltro.Controls.Add(this.txtFiltroDesignacao);
            this.grpFiltro.Controls.Add(this.lblFiltroDesignacao);
            this.grpFiltro.Size = new System.Drawing.Size(620, 59);
            this.grpFiltro.Controls.SetChildIndex(this.lblFiltroDesignacao, 0);
            this.grpFiltro.Controls.SetChildIndex(this.txtFiltroDesignacao, 0);
            this.grpFiltro.Controls.SetChildIndex(this.lblNoticiaAut, 0);
            this.grpFiltro.Controls.SetChildIndex(this.cbNoticiaAut, 0);
            this.grpFiltro.Controls.SetChildIndex(this.chkValidado, 0);
            this.grpFiltro.Controls.SetChildIndex(this.btnAplicar, 0);
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(548, 28);
            // 
            // colDesignacao
            // 
            this.colDesignacao.Text = "Designação";
            this.colDesignacao.Width = 231;
            // 
            // colNoticiaAut
            // 
            this.colNoticiaAut.Text = "Notícia de autoridade";
            this.colNoticiaAut.Width = 121;
            // 
            // colValidado
            // 
            this.colValidado.Text = "Validado";
            this.colValidado.Width = 98;
            // 
            // colCompleto
            // 
            this.colCompleto.Text = "Completo";
            this.colCompleto.Width = 78;
            // 
            // colDatasExistencia
            // 
            this.colDatasExistencia.Text = "Datas de existência";
            this.colDatasExistencia.Width = 120;
            // 
            // chkValidado
            // 
            this.chkValidado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkValidado.Checked = true;
            this.chkValidado.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkValidado.Location = new System.Drawing.Point(446, 30);
            this.chkValidado.Name = "chkValidado";
            this.chkValidado.Size = new System.Drawing.Size(96, 24);
            this.chkValidado.TabIndex = 3;
            this.chkValidado.Text = "Validado";
            this.chkValidado.ThreeState = true;
            // 
            // cbNoticiaAut
            // 
            this.cbNoticiaAut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbNoticiaAut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNoticiaAut.Location = new System.Drawing.Point(280, 32);
            this.cbNoticiaAut.Name = "cbNoticiaAut";
            this.cbNoticiaAut.Size = new System.Drawing.Size(160, 21);
            this.cbNoticiaAut.TabIndex = 2;
            // 
            // lblNoticiaAut
            // 
            this.lblNoticiaAut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNoticiaAut.Location = new System.Drawing.Point(280, 16);
            this.lblNoticiaAut.Name = "lblNoticiaAut";
            this.lblNoticiaAut.Size = new System.Drawing.Size(160, 15);
            this.lblNoticiaAut.TabIndex = 2;
            this.lblNoticiaAut.Text = "Notícia de autoridade";
            // 
            // txtFiltroDesignacao
            // 
            this.txtFiltroDesignacao.AcceptsReturn = true;
            this.txtFiltroDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFiltroDesignacao.Location = new System.Drawing.Point(8, 32);
            this.txtFiltroDesignacao.Name = "txtFiltroDesignacao";
            this.txtFiltroDesignacao.Size = new System.Drawing.Size(266, 20);
            this.txtFiltroDesignacao.TabIndex = 1;
            this.txtFiltroDesignacao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // lblFiltroDesignacao
            // 
            this.lblFiltroDesignacao.Location = new System.Drawing.Point(8, 16);
            this.lblFiltroDesignacao.Name = "lblFiltroDesignacao";
            this.lblFiltroDesignacao.Size = new System.Drawing.Size(77, 16);
            this.lblFiltroDesignacao.TabIndex = 0;
            this.lblFiltroDesignacao.Text = "Designação";
            // 
            // ToolTip
            // 
            this.ToolTip.ShowAlways = true;
            // 
            // ControloAutList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.FilterVisible = true;
            this.Name = "ControloAutList";
            this.Size = new System.Drawing.Size(632, 344);
            this.grpResultados.ResumeLayout(false);
            this.grpFiltro.ResumeLayout(false);
            this.grpFiltro.PerformLayout();
            this.ResumeLayout(false);

		}

	#endregion

        private string FiltroTermoLike
        {
            get
            {
                return txtFiltroDesignacao.Text.Length == 0 ? string.Empty :
                    //DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.buildLikeStatement("Termo", string.Format("'{0}'", DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.sanitizeSearchTerm(txtFiltroDesignacao.Text)));
                    txtFiltroDesignacao.Text;
            }
        }

        public long[] SelectedNoticiasAut
        {
            get
            {
                if (DesignMode)
                    return new long[] {};

                List<long> IDs = new List<long>();
                GISADataset.TipoNoticiaAutRow naRow = (GISADataset.TipoNoticiaAutRow)cbNoticiaAut.SelectedItem;
                Debug.Assert(naRow != null);

                if (naRow != null && naRow.ID != -1)
                    IDs.Add(naRow.ID);
                else
                {
                    foreach (GISADataset.TipoNoticiaAutRow naRowWithinLoop in cbNoticiaAut.Items)
                    {
                        naRow = naRowWithinLoop;
                        if (naRowWithinLoop.ID != -1)
                            IDs.Add(naRowWithinLoop.ID);
                    }
                }
                return IDs.ToArray();
            }
        }

        private long[] FiltroNoticiaAut { get { return SelectedNoticiasAut; } }

        public void SelectItem(GISADataset.ControloAutDicionarioRow cadRow)
        {
            ListViewItem item = null;
            item = GUIHelper.GUIHelper.findListViewItemByTag(cadRow, lstVwPaginated);
            if (item != null)
                SelectItem(item);
        }

        private string AutorizadoFilter
        {
            get
            {
                switch (chkValidado.CheckState)
                {
                    case CheckState.Checked:
                        return "1";
                    case CheckState.Indeterminate:
                        return string.Empty;
                    case CheckState.Unchecked:
                        return "0";
                    default:
                        //nunca deverá chegar aqui
                        return string.Empty;
                }
            }
        }

		private bool IsSelectedInCbNoticiaAut(long IDTipoNoticiaAut)
		{
			if (((GISADataset.TipoNoticiaAutRow)cbNoticiaAut.SelectedItem).ID == IDTipoNoticiaAut)
				return true;

			if (((GISADataset.TipoNoticiaAutRow)cbNoticiaAut.SelectedItem).ID == -1)
			{
				foreach (GISADataset.TipoNoticiaAutRow tna in cbNoticiaAut.Items)
				{
					if (tna.ID == IDTipoNoticiaAut)
                        return true;
				}
			}
			return false;
		}

        public void ConfigureDiplomaModelo()
        {
            this.lstVwPaginated.Columns.Remove(this.colValidado);
            this.lstVwPaginated.Columns.Remove(this.colCompleto);
            this.lstVwPaginated.Columns.Remove(this.colNoticiaAut);
            this.colDesignacao.Width = this.lstVwPaginated.Width - 4;
            this.chkValidado.Visible = false;
            this.cbNoticiaAut.Visible = false;
            this.lblNoticiaAut.Visible = false;
            this.txtFiltroDesignacao.Width = this.lstVwPaginated.Width - 42;
            this.grpFiltro.Visible = false;
            this.Enabled = false;
        }        

        protected override void CalculateOrderedItems(IDbConnection connection)
		{
            ControloAutRule.Current.CalculateOrderedItems(AutorizadoFilter, FiltroTermoLike, FiltroNoticiaAut, connection);
		}

        protected override int GetPageForItemTag(object itemTag, int pageNr, IDbConnection connection)
		{
			var cadRow = (GISADataset.ControloAutDicionarioRow)itemTag;
            return ControloAutRule.Current.GetPageForID(new long[] { cadRow.IDControloAut, cadRow.IDDicionario, cadRow.IDTipoControloAutForma }, pageNr, connection);
		}
        
        protected override void GetItems(int pageNr, int itemsPerPage, IDbConnection connection)
		{
            returnedInfo = new PaginatedLVGetItemsCA(ControloAutRule.Current.GetItems(GisaDataSetHelper.GetInstance(), pageNr, itemsPerPage, FiltroNoticiaAut, connection));
		}

        protected override void DeleteTemporaryResults(IDbConnection connection)
		{
            ControloAutRule.Current.DeleteTemporaryResults(connection);
		}

        protected override void AddItemsToList()
		{
            GISADataset.ControloAutDicionarioRow cadRow = null;
            Font font = null;
            ArrayList items = new ArrayList();
            if (returnedInfo.rowsInfo != null)
            {
                foreach (DataRow rowInfo in returnedInfo.rowsInfo)
                {
                    if (IsSelectedInCbNoticiaAut(((GISADataset.ControloAutDicionarioRow)rowInfo).ControloAutRow.IDTipoNoticiaAut))
                    {
                        cadRow = (GISADataset.ControloAutDicionarioRow)rowInfo;
                        string[] termo = { cadRow.DicionarioRow.Termo };
                        if (cadRow.IDTipoControloAutForma == Convert.ToInt64(TipoControloAutForma.FormaAutorizada))
                            font = this.lstVwPaginated.Font;
                        else
                            font = new Font(this.lstVwPaginated.Font, FontStyle.Italic);

                        ListViewItem item = new ListViewItem(termo, 0, this.lstVwPaginated.ForeColor, this.lstVwPaginated.BackColor, font);
                        items.Add(item);
                        item.SubItems.Add(cadRow.ControloAutRow.TipoNoticiaAutRow.Designacao);
                        item.SubItems.Add(TranslationHelper.FormatBoolean(cadRow.ControloAutRow.Autorizado));
                        item.SubItems.Add(TranslationHelper.FormatBoolean(cadRow.ControloAutRow.Completo));
                        GISADataset.ControloAutDatasExistenciaRow[] cadeRows = null;
                        cadeRows = cadRow.ControloAutRow.GetControloAutDatasExistenciaRows();
                        if (cadeRows.Length == 0)
                            item.SubItems.Add(string.Empty);
                        else
                            item.SubItems.Add(GUIHelper.GUIHelper.FormatDateInterval(cadeRows[0]));
                        item.Tag = cadRow;
                    }
                }
                if (items.Count > 0)
                    this.lstVwPaginated.Items.AddRange((ListViewItem[])(items.ToArray(typeof(ListViewItem))));
            }
		}

		// adicionar o item na sua posição à lista na posição correcta tendo em consideração o critério de ordenação; o
		// elemento novo é apresentado na sua página
		public void AddNivel(GISADataset.ControloAutDicionarioRow cadRow)
		{
			ListViewItem lvItem = null;
			ReloadList(cadRow);
            lvItem = GUIHelper.GUIHelper.findListViewItemByTag(cadRow, this.lstVwPaginated);

			// prever a situação em que o filtro está activo e o elemento a adicionar não respeita o critério desse filtro
			if (lvItem == null)
			{
				if (MessageBox.Show("A notícia de autoridade que pretende adicionar não respeita os critérios " + System.Environment.NewLine + "definidos no filtro e por esse motivo não poderá ser apresentada. " + System.Environment.NewLine + "Pretende limpar os critérios do filtro para dessa forma poder visualizar a notícia de autoridade criada?", "Mostrar Nova Notícia de Autoridade", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					ClearFiltro();
					ReloadList(cadRow);
                    lvItem = GUIHelper.GUIHelper.findListViewItemByTag(cadRow, this.lstVwPaginated);
                    this.lstVwPaginated.selectItem(lvItem);
                    this.lstVwPaginated.EnsureVisible(lvItem.Index);
				}
				else
					ReloadList();
			}
			else
			{
                this.lstVwPaginated.selectItem(lvItem);
                this.lstVwPaginated.EnsureVisible(lvItem.Index);
			}
		}

		public void ClearFiltro()
		{
            txtFiltroDesignacao.Text = string.Empty;
            if (cbNoticiaAut.Items.Count > 0)
                cbNoticiaAut.SelectedIndex = 0;
            chkValidado.CheckState = CheckState.Indeterminate;
		}

		private bool mAllowedNoticiaAutLocked;
		public bool AllowedNoticiaAutLocked
		{
			get {return mAllowedNoticiaAutLocked;}
			set {mAllowedNoticiaAutLocked = value;}
		}

		public void AllowedNoticiaAut(params TipoNoticiaAut[] NoticiaAutArray)
		{
			if (AllowedNoticiaAutLocked)
				return;

			string QueryFilter = string.Empty;

			foreach (TipoNoticiaAut tna in NoticiaAutArray)
			{
				if (QueryFilter.Length > 0)
					QueryFilter = QueryFilter + " OR ";

				QueryFilter = QueryFilter + "ID=" + System.Enum.Format(typeof(TipoNoticiaAut), tna, "D");
			}

			GISADataset.TipoNoticiaAutRow allNoticiaAut = null;
			allNoticiaAut = GisaDataSetHelper.GetInstance().TipoNoticiaAut. NewTipoNoticiaAutRow();
			allNoticiaAut.ID = -1;
			allNoticiaAut.Designacao = "<Todos>";

			DataRow[] DataRows = GisaDataSetHelper.GetInstance().TipoNoticiaAut.Select(QueryFilter);
			GISADataset.TipoNoticiaAutRow[] DataRowsEx = null;
			DataRowsEx = new GISADataset.TipoNoticiaAutRow[DataRows.Length + 1];
			DataRowsEx[0] = allNoticiaAut;
			Array.Copy(DataRows, 0, DataRowsEx, 1, DataRows.Length);

            cbNoticiaAut.BindingContext = new BindingContext();
			cbNoticiaAut.DataSource = DataRowsEx;
			cbNoticiaAut.DisplayMember = "Designacao";
            cbNoticiaAut.ValueMember = "ID";
			cbNoticiaAut.SelectedIndex = 0;
			Debug.WriteLine("Selected item: " + ((cbNoticiaAut.SelectedItem == null) ? "" : cbNoticiaAut.SelectedItem.ToString()).ToString());

			if (Array.IndexOf(NoticiaAutArray, TipoNoticiaAut.EntidadeProdutora) != -1)
			{
				if (colDatasExistencia.ListView == null)
                    this.lstVwPaginated.Columns.Add(colDatasExistencia);
			}
			else
			{
				if (colDatasExistencia.ListView != null)
                    this.lstVwPaginated.Columns.Remove(colDatasExistencia);
			}
		}

		public void UpdateEditedItems()
		{
			GISADataset.ControloAutDicionarioRow cadRow = null;
            foreach (ListViewItem li in this.lstVwPaginated.Items)
			{
				cadRow = (GISADataset.ControloAutDicionarioRow)li.Tag;
				if (! (li.Text.Equals(cadRow.DicionarioRow.Termo)))
					li.Text = cadRow.DicionarioRow.Termo;
			}
		}
	}
}