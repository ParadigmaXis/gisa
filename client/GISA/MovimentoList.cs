using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

using GISA.Controls;

namespace GISA
{
    public partial class MovimentoList
#if DEBUG
 : MiddleClass
#else  
 : PaginatedListView 
#endif 
    {
        public string CatCode = string.Empty;
        PaginatedLVGetItems returnedInfo;

        public MovimentoList()
        {
            InitializeComponent();

            //Add any initialization after the InitializeComponent() call

            txtFiltroCodigo.KeyDown += new KeyEventHandler(this.txtBox_KeyDown);
            txtFiltroEntidade.KeyDown += new KeyEventHandler(this.txtBox_KeyDown);
            txtFiltroNroMovimento.PropagateEnterPressed += new PxIntegerBox.PropagateEnterPressedEventHandler(this.txtID_KeyDown);
            
            dateTimePicker1.Checked = false;
            dateTimePicker2.Checked = false;
        }

        void txtID_KeyDown(object Sender, EventArgs e)
        {
            this.ReloadList();
        }

        #region Filtros
        public string FiltroNroMovimento
        {
            get
            {
                if (this.txtFiltroNroMovimento.Text == null || this.txtFiltroNroMovimento.Text.Length == 0)
                    return string.Empty;
                else
                    return txtFiltroNroMovimento.Text;
            }
        }

        public string FiltroDataInicio
        {
            get
            {
                if (dateTimePicker1.Text.Length == 0 || !dateTimePicker1.Checked)
                    return string.Empty;
                else
                    return dateTimePicker1.Text;
            }
        }

        public string FiltroDataFim
        {
            get
            {
                if (dateTimePicker2.Text.Length == 0 || !dateTimePicker2.Checked)
                    return string.Empty;
                else
                    return dateTimePicker2.Text;
            }
        }

        public string FiltroEntidade
        {
            get
            {
                return txtFiltroEntidade.Text.Length == 0 ? string.Empty :
                    DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.buildLikeStatement("me.Entidade", string.Format("'{0}'", DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.sanitizeSearchTerm(txtFiltroEntidade.Text)));
            }
        }

        public string FiltroCodigo 
        {
            get 
            {
                return txtFiltroCodigo.Text.Length == 0 ? string.Empty :
                    DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.buildLikeStatement("me.Codigo", string.Format("'{0}'", DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.sanitizeSearchTerm(txtFiltroCodigo.Text)));
            }
        }
        #endregion

        protected override void CalculateOrderedItems(IDbConnection connection)
        {
            MovimentoRule.Current.CalculateOrderedItems(CatCode, FiltroNroMovimento, FiltroDataInicio, FiltroDataFim, FiltroEntidade, FiltroCodigo, connection);
        }

        protected override void GetItems(int pageNr, int itemsPerPage, IDbConnection connection)
        {
            ArrayList rowIds = new ArrayList();
            rowIds = MovimentoRule.Current.GetItems(GisaDataSetHelper.GetInstance(), pageNr, itemsPerPage, connection);
            PaginatedLVGetItemsUF items = new PaginatedLVGetItemsUF(rowIds);
            returnedInfo = items;
        }

        protected override void DeleteTemporaryResults(IDbConnection connection)
        {
            MovimentoRule.Current.DeleteTemporaryResults(connection);
        }

        protected override void AddItemsToList()
        {
            List<ListViewItem> itemsToBeAdded = new List<ListViewItem>();
            itemsToBeAdded.Clear();
            if (returnedInfo.rowsInfo != null)
            {

                foreach (ArrayList rowInfo in returnedInfo.rowsInfo)
                {
                    GISADataset.MovimentoRow movRow = (GISADataset.MovimentoRow)(GisaDataSetHelper.GetInstance().Movimento.Select("ID=" + rowInfo[0].ToString())[0]);
                    ListViewItem item = new ListViewItem();
                    item.SubItems.AddRange(new string[] { string.Empty, string.Empty, string.Empty });
                    item.Tag = movRow;
                    item.SubItems[this.columnHeaderID.Index].Text = rowInfo[0].ToString();
                    item.SubItems[this.columnHeaderData.Index].Text = rowInfo[1].ToString();
                    item.SubItems[this.columnHeaderEntidade.Index].Text = rowInfo[2].ToString();
                    item.SubItems[this.columnHeaderCodigo.Index].Text = rowInfo[3].ToString();

                    itemsToBeAdded.Add(item);
                }

                if (itemsToBeAdded.Count > 0)
                    this.Items.AddRange(itemsToBeAdded.ToArray());
            }
        }

        protected override void ClearFilter() {
            txtFiltroNroMovimento.Text = string.Empty;
            txtFiltroCodigo.Text = string.Empty;
            dateTimePicker1.Checked = false;
            dateTimePicker2.Checked = false;
        }
    }
}
