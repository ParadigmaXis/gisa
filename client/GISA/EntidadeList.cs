using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.Controls;
using GISA.SharedResources;
using GISA.GUIHelper;

namespace GISA {
    public partial class EntidadeList
#if DEBUG
 : MiddleClass
#else  
 : PaginatedListView 
#endif
    {
        public delegate void BeforeNewListSelectionEventHandler(object sender, BeforeNewSelectionEventArgs e);
        public new event EventHandler EntSelectedIndexChanged;

        PaginatedLVGetItems returnedInfo;

        public EntidadeList() {
            InitializeComponent();

            txtFiltroDesignacao.KeyDown += new KeyEventHandler(this.txtBox_KeyDown);
            txtFiltroCodigo.KeyDown += new KeyEventHandler(this.txtBox_KeyDown);
            lstVwPaginated.SelectedIndexChanged += new EventHandler(lstVwEntidades_SelectedIndexChanged);

            txtCodigo.TextChanged += new EventHandler(txtCodigo_TextChanged);
            txtDesignacao.TextChanged += new EventHandler(txtDesignacao_TextChanged);
            txtOutrosDados.TextChanged += new EventHandler(txtOutrosDados_TextChanged);
            chkActive.CheckedChanged += new EventHandler(chkActive_CheckedChanged);

            this.FilterVisible = false;
            
            DisableEdit();
        }

        public string CodigoFilter {
            get {
                return txtFiltroCodigo.Text.Length == 0 ? string.Empty :
                    DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.buildLikeStatement("Codigo", string.Format("'{0}'", DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.sanitizeSearchTerm(txtFiltroCodigo.Text)));
            }
        }

        public string EntidadeFilter {
            get {
                return txtFiltroDesignacao.Text.Length == 0 ? string.Empty :
                    DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.buildLikeStatement("Entidade", string.Format("'{0}'", DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.sanitizeSearchTerm(txtFiltroDesignacao.Text)));
            }
        }

        public string ActivoFilter {
            get {
                var state = grpFiltro.Visible ? chkActivo.CheckState : CheckState.Indeterminate;
                string tempAutorizado = null;
                switch (state)
                {
                    case CheckState.Checked:
                        tempAutorizado = "Activo=1";
                        break;
                    case CheckState.Indeterminate:
                        tempAutorizado = string.Empty;
                        break;
                    case CheckState.Unchecked:
                        tempAutorizado = "Activo=0";
                        break;
                    default:
                        //nunca deverá chegar aqui
                        tempAutorizado = string.Empty;
                        break;
                }
                return tempAutorizado;
            }
        }

        protected override void CalculateOrderedItems(IDbConnection connection)
        {
            MovimentoRule.Current.Entidade_CalculateOrderedItems(ActivoFilter, EntidadeFilter, CodigoFilter, connection);
        }

        protected override void GetItems(int pageNr, int itemsPerPage, IDbConnection connection)
        {
            returnedInfo = new PaginatedLVGetItemsCA(MovimentoRule.Current.Entidade_GetItems(GisaDataSetHelper.GetInstance(), pageNr, itemsPerPage, EntidadeFilter, connection));
        }

        protected override void DeleteTemporaryResults(IDbConnection connection)
        {
            MovimentoRule.Current.Entidade_DeleteTemporaryResults(connection);
        }

        protected override void AddItemsToList()
        {
            GISADataset.MovimentoEntidadeRow movEntRow = null;
            Font font = null;
            ArrayList items = new ArrayList();
            if (returnedInfo.rowsInfo != null)
            {
                foreach (DataRow rowInfo in returnedInfo.rowsInfo)
                {
                    movEntRow = (GISADataset.MovimentoEntidadeRow)rowInfo;

                    ListViewItem item = new ListViewItem(new string[] { movEntRow.Codigo }, 0, this.lstVwPaginated.ForeColor, this.lstVwPaginated.BackColor, font);
                    items.Add(item);
                    item.SubItems.Add(movEntRow.Entidade);
                    item.SubItems.Add(TranslationHelper.FormatBoolean(movEntRow.Activo));

                    item.Tag = movEntRow;
                }

                if (items.Count > 0)
                {
                    this.lstVwPaginated.BeginUpdate();
                    this.Items.AddRange((ListViewItem[])(items.ToArray(typeof(ListViewItem))));
                    this.lstVwPaginated.EndUpdate();
                }
            }
        }        

        protected override void ClearFilter() {
            txtFiltroCodigo.Text = string.Empty;
            txtFiltroDesignacao.Text = string.Empty;
            chkActivo.CheckState = CheckState.Indeterminate;
        }

        public void AddEntidadeMovimento() {
            byte[] Versao = null;
            GISADataset.MovimentoEntidadeRow[] rows = null;
            GISADataset.MovimentoEntidadeRow novaEntidade = null;
            ListViewItem novoLvi = null;

            rows = (GISADataset.MovimentoEntidadeRow[])(GisaDataSetHelper.GetInstance().MovimentoEntidade.Select("Codigo = ''"));
            if (rows.Length > 0) {
                novaEntidade = rows[0];
                foreach (ListViewItem lvi in lstVwPaginated.Items) if (lvi.Tag == novaEntidade) novoLvi = lvi;        
            } else {
                novaEntidade = GisaDataSetHelper.GetInstance().MovimentoEntidade.AddMovimentoEntidadeRow("", "Nova Entidade", true, string.Empty, Versao, 0);
                novoLvi = lstVwPaginated.Items.Add(novaEntidade.Codigo);
                novoLvi.SubItems.Add(novaEntidade.Entidade);
                novoLvi.SubItems.Add(TranslationHelper.FormatBoolean(novaEntidade.Activo));
                novoLvi.Tag = novaEntidade;
            }

            lstVwPaginated.selectItem(novoLvi);
        }

        public void EnableEdit() {
            panel1.Enabled = true;
            this.lstVwPaginated.Enabled = false;
            this.txtNroPagina.Enabled = false;
            this.btnProximo.Enabled = false;
            this.btnAnterior.Enabled = false;
            if (grpFiltro.Visible) grpFiltro.Enabled = false;
        }

        public void DisableEdit() {
            panel1.Enabled = false;
            this.lstVwPaginated.Enabled = true;
            this.txtNroPagina.Enabled = true;
            this.RefreshButtonsState();
            if (grpFiltro.Visible) grpFiltro.Enabled = true;
        }

        private void UpdateRowView(GISADataset.MovimentoEntidadeRow datasetRow, ListViewItem listRow) {
            listRow.SubItems[0].Text = datasetRow.Codigo;
            listRow.SubItems[1].Text = datasetRow.Entidade;
            listRow.SubItems[2].Text = TranslationHelper.FormatBoolean(datasetRow.Activo);
            listRow.Tag = datasetRow;
        }

        private void lstVwEntidades_SelectedIndexChanged(object sender, EventArgs e) {
            if (lstVwPaginated.SelectedItems.Count != 0)
            {
                GISADataset.MovimentoEntidadeRow movEntRow = (GISADataset.MovimentoEntidadeRow)lstVwPaginated.SelectedItems[0].Tag;
                txtCodigo.Text = movEntRow.Codigo;
                txtDesignacao.Text = movEntRow.Entidade;
                chkActive.Checked = movEntRow.Activo;
                if (movEntRow["OutrosDados"] == DBNull.Value)
                    txtOutrosDados.Text = "";
                else
                    txtOutrosDados.Text = movEntRow.OutrosDados;
            }

            if (this.EntSelectedIndexChanged != null)
                EntSelectedIndexChanged(sender, e);
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            txtCodigo.Enabled = chkActive.Checked;
            txtDesignacao.Enabled = chkActive.Checked;
            txtOutrosDados.Enabled = chkActive.Checked;

            if (lstVwPaginated.SelectedItems.Count == 1)
            {
                GISADataset.MovimentoEntidadeRow row = lstVwPaginated.SelectedItems[0].Tag as GISADataset.MovimentoEntidadeRow;
                row.Activo = chkActive.Checked;
                UpdateRowView(row, lstVwPaginated.SelectedItems[0]);
            }
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (lstVwPaginated.SelectedItems.Count == 1)
            {
                GISADataset.MovimentoEntidadeRow row = lstVwPaginated.SelectedItems[0].Tag as GISADataset.MovimentoEntidadeRow;
                row.Codigo = txtCodigo.Text;
                UpdateRowView(row, lstVwPaginated.SelectedItems[0]);
            }
        }

        private void txtDesignacao_TextChanged(object sender, EventArgs e)
        {
            if (lstVwPaginated.SelectedItems.Count == 1)
            {
                GISADataset.MovimentoEntidadeRow row = lstVwPaginated.SelectedItems[0].Tag as GISADataset.MovimentoEntidadeRow;
                row.Entidade = txtDesignacao.Text;
                UpdateRowView(row, lstVwPaginated.SelectedItems[0]);
            }
        }

        private void txtOutrosDados_TextChanged(object sender, EventArgs e)
        {
            if (lstVwPaginated.SelectedItems.Count == 1)
            {
                GISADataset.MovimentoEntidadeRow row = lstVwPaginated.SelectedItems[0].Tag as GISADataset.MovimentoEntidadeRow;
                row.OutrosDados = txtOutrosDados.Text;
            }
        }

        public void DeleteSelectedEntidade()
        {
            GUIHelper.GUIHelper.deleteSelectedLstVwItems(this.lstVwPaginated);
        }
    }
}
