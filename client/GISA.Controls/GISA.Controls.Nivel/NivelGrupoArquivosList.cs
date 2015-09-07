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
using GISA.SharedResources;

namespace GISA.Controls.Nivel
{
    public partial class NivelGrupoArquivosList
#if DEBUG  
 : MiddleClass 
#else  
 : PaginatedListView 
#endif
    {
        private List<NivelRule.NivelDocumentalListItem> returnedInfo;
        private ListViewItem lstVwNiveisDocumentais_MouseMove_previousItem;

        public NivelGrupoArquivosList()
        {
            InitializeComponent();

            lstVwPaginated.CustomizedSorting = false;
            GrpResultadosLabel = "Grupos de Arquivo";
        }

        protected override void GetExtraResources()
        {
            base.GetExtraResources();
            lstVwPaginated.SmallImageList = TipoNivelRelacionado.GetImageList();
        }

        protected override void CalculateOrderedItems(IDbConnection connection)
        {
            NivelRule.Current.CalculateOrderedItemsGA(connection);
        }

        protected override void GetItems(int pageNr, int itemsPerPage, IDbConnection connection)
        {
            this.returnedInfo = NivelRule.Current.GetItemsGA(GisaDataSetHelper.GetInstance(), pageNr, itemsPerPage, connection);
        }

        protected override void DeleteTemporaryResults(IDbConnection connection)
        {
            DepositoRule.Current.DeleteTemporaryResults(connection);
        }

        private const string PREFIX_HACK = " ";
        protected override void AddItemsToList()
        {
            List<ListViewItem> itemsToBeAdded = new List<ListViewItem>();
            ListViewItem lvItem = null;

            foreach (var item in this.returnedInfo)
            {
                lvItem = new ListViewItem();

                lvItem.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageBase(item.GUIOrder);
                lvItem.StateImageIndex = 0;
                lvItem.SubItems.AddRange(new string[] { string.Empty });
                lvItem.Tag = GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + item.IDNivel.ToString())[0];
                lvItem.SubItems[chDesignacao.Index].Text = PREFIX_HACK + item.Designacao;

                itemsToBeAdded.Add(lvItem);
            }

            if (itemsToBeAdded.Count > 0)
                lstVwPaginated.Items.AddRange(itemsToBeAdded.ToArray());
        }

        public GISADataset.NivelRow GetSelectedNivel()
        {
            return lstVwPaginated.SelectedItems.Count > 0 ? lstVwPaginated.SelectedItems[0].Tag as GISADataset.NivelRow : null;
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

            // See if we have a valid item under mouse pointer
            if (currentItem == null)
            {
                if (setToolTipEvent != null)
                    setToolTipEvent(this, null);
            }
            else
            {
                if (setToolTipEvent != null)
                    setToolTipEvent(this, currentItem);
            }
        }
    }
}
