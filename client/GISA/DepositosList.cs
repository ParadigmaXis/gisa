using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Controls;
using GISA.Model;

namespace GISA
{
    public partial class DepositosList
#if DEBUG
 : MiddleClass
#else  
 : PaginatedListView 
#endif 
    {
        public DepositosList()
        {
            InitializeComponent();

            base.GrpResultadosLabel = "Depósitos encontrados";
            
            this.FilterMessageTitle = "Mostrar Novo Depósito";
            this.FilterMessageBody = "O depósito que pretende adicionar não respeita os critérios " + System.Environment.NewLine +
                                                "definidos no filtro e por esse motivo não poderá ser apresentada. " + System.Environment.NewLine +
                                                "Pretende limpar os critérios do filtro para dessa forma poder visualizar o depósito criado?";

            this.txtFiltroDesignacao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            lstVwPaginated.CustomizedSorting = false;
            this.FilterVisible = true;
        }

        protected override void GetExtraResources()
        {
            base.GetExtraResources();
        }

        private string FiltroDesignacaoLike
        {
            get
            {
                return txtFiltroDesignacao.Text.Length == 0 ? string.Empty :
                    DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.buildLikeStatement("Designacao", "'" + txtFiltroDesignacao.Text + "'");
            }
        }

        protected override void CalculateOrderedItems(IDbConnection connection)
        {
            DepositoRule.Current.CalculateOrderedItems(FiltroDesignacaoLike, connection);
        }

        private PaginatedLVGetItemsDep items;
        protected override void GetItems(int pageNr, int itemsPerPage, IDbConnection connection)
        {
            var rowIds = DepositoRule.Current.GetItems(GisaDataSetHelper.GetInstance(), pageNr, itemsPerPage, connection);
            this.items = new PaginatedLVGetItemsDep(rowIds);            
        }

        protected override void DeleteTemporaryResults(IDbConnection connection)
        {
            DepositoRule.Current.DeleteTemporaryResults(connection);
        }

        protected override void AddItemsToList()
        {
            ArrayList itemsToBeAdded = new ArrayList();
            itemsToBeAdded.Clear();
            if (this.items.rowsInfo != null)
            {
                GISADataset.TipoNivelRelacionadoRow tnrRow = (GISADataset.TipoNivelRelacionadoRow)(GisaDataSetHelper.GetInstance().TipoNivelRelacionado.Select("ID=" + TipoNivelRelacionado.UF.ToString())[0]);
                foreach (ArrayList rowInfo in this.items.rowsInfo)
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.AddRange(new string[] { string.Empty });
                    item.Tag = GisaDataSetHelper.GetInstance().Deposito.Cast<GISADataset.DepositoRow>().Single(d => d.ID == (long)rowInfo[0]);
                    item.SubItems[this.chDesignacao.Index].Text = rowInfo[1].ToString();                    
                    itemsToBeAdded.Add(item);
                }

                if (itemsToBeAdded.Count > 0)
                    lstVwPaginated.Items.AddRange((ListViewItem[])(itemsToBeAdded.ToArray(typeof(ListViewItem))));
            }
        }

        protected override void ClearFilter()
        {
            txtFiltroDesignacao.Text = string.Empty;
        }

        protected override string FilterMessageBody { get; set; }
        protected override string FilterMessageTitle { get; set; }
    }
}
