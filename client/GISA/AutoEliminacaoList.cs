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
    public partial class AutoEliminacaoList
#if DEBUG
 : MiddleClass
#else  
 : PaginatedListView 
#endif
    {
        private PaginatedLVGetItems returnedInfo;

        public AutoEliminacaoList()
        {
            InitializeComponent();

            //Add any initialization after the InitializeComponent() call
            this.txtFiltroDesignacao.KeyDown += this.txtBox_KeyDown;
            base.GrpResultadosLabel = "Autos de eliminação";
            lstVwPaginated.CustomizedSorting = false;
        }

        private string FiltroTermoLike
        {
            get
            {
                return txtFiltroDesignacao.Text.Length == 0 ? string.Empty :
                    DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.buildLikeStatement("Designacao", string.Format("'{0}'", DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.sanitizeSearchTerm(txtFiltroDesignacao.Text)));
            }
        }

        protected override void CalculateOrderedItems(IDbConnection connection)
        {
            AutoEliminacaoRule.Current.CalculateOrderedItems(FiltroTermoLike, connection);
        }

        protected override void GetItems(int pageNr, int itemsPerPage, IDbConnection connection)
        {
            returnedInfo = new PaginatedLVGetItemsCA(AutoEliminacaoRule.Current.GetItems(GisaDataSetHelper.GetInstance(), pageNr, itemsPerPage, connection));
        }

        protected override void DeleteTemporaryResults(IDbConnection connection)
        {
            AutoEliminacaoRule.Current.DeleteTemporaryResults(connection);
        }

        protected override void AddItemsToList()
        {
            GISADataset.AutoEliminacaoRow aeRow = null;
            List<ListViewItem> items = new List<ListViewItem>();
            if (returnedInfo.rowsInfo != null)
            {
                foreach (ArrayList rowInfo in returnedInfo.rowsInfo)
                {
                    aeRow = (GISADataset.AutoEliminacaoRow)GisaDataSetHelper.GetInstance().AutoEliminacao.Select("ID=" + rowInfo[0].ToString())[0];

                    ListViewItem item = new ListViewItem(rowInfo[1].ToString());
                    items.Add(item);
                    item.Tag = aeRow;
                }
                if (items.Count > 0)
                    this.lstVwPaginated.Items.AddRange(items.ToArray());
            }
        }

        protected override void ClearFilter()
        {
            txtFiltroDesignacao.Text = string.Empty;
        }
    }
}
