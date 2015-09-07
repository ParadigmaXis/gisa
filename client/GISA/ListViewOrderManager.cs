using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.SharedResources;
using GISA.Controls;

namespace GISA
{
    public partial class ListViewOrderManager : UserControl
    {
        private object lastSelectedItem;
        protected bool readOnlyMode = false;

        public ListViewOrderManager()
        {
            InitializeComponent();
            GetExtraResources();

            lastSelectedItem = null;
            lstVw.BeforeNewSelection += new EventHandler<BeforeNewSelectionEventArgs>(lstVw_BeforeNewSelection);
        }

        void lstVw_BeforeNewSelection(object sender, BeforeNewSelectionEventArgs e)
        {
            var item = e.ItemToBeSelected;
            if (lastSelectedItem != e.ItemToBeSelected)
            {
                this.OnOrderManagerSelectedIndexChangedEvent(e);
                //updateToolBarButtons();
                lastSelectedItem = item;
            }
        }

        private void GetExtraResources()
        {
            btnCima.Image = SharedResourcesOld.CurrentSharedResources.PrioridadeAumentar;
            btnBaixo.Image = SharedResourcesOld.CurrentSharedResources.PrioridadeDiminuir;
            btnInicio.Image = SharedResourcesOld.CurrentSharedResources.PrioridadeMax;
            btnFim.Image = SharedResourcesOld.CurrentSharedResources.PrioridadeMin;
            btnFullScreen.Image = SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedActionIcons), "ActionFullScreen_enabled_16x16.png");

            CurrentToolTip.SetToolTip(btnCima, SharedResourcesOld.CurrentSharedResources.PrioridadeAumentarString);
            CurrentToolTip.SetToolTip(btnBaixo, SharedResourcesOld.CurrentSharedResources.PrioridadeDiminuirString);
            CurrentToolTip.SetToolTip(btnInicio, SharedResourcesOld.CurrentSharedResources.PrioridadeMaxString);
            CurrentToolTip.SetToolTip(btnFim, SharedResourcesOld.CurrentSharedResources.PrioridadeMinString);
            this.CurrentToolTip.SetToolTip(this.btnFullScreen, "Mostrar no ecrã todo");
        }

        public event EventHandler<BeforeNewSelectionEventArgs> OrderManagerSelectedIndexChangedEvent;
        protected virtual void OnOrderManagerSelectedIndexChangedEvent(BeforeNewSelectionEventArgs e)
        {
            if (this.OrderManagerSelectedIndexChangedEvent != null)
                OrderManagerSelectedIndexChangedEvent(this, e);
        }

        public event EventHandler<EventArgs> FullScreenInvoked;
        protected virtual void OnFullScreenInvoked(EventArgs e)
        {
            if (this.FullScreenInvoked != null)
                FullScreenInvoked(this, e);
        }

        public ListViewItem CreateItem(string val, Object tag)
        {
            var item = new ListViewItem();
            item.SubItems.AddRange(new string[] { string.Empty });
            item.Tag = tag;
            item.SubItems[colDesignacao.Index].Text = val;
            return item;
        }

        public void addNewItem(ListViewItem item)
        {
            item.Font = PermissoesHelper.fontItalic;
            populateItems(new List<ListViewItem> {item});
        }

        public void addItem(ListViewItem item)
        {
            populateItems(new List<ListViewItem> { item });
        }

        public void populateItems(List<ListViewItem> items)
        {
            lstVw.BeginUpdate();
            lstVw.Items.AddRange(items.ToArray());
            lstVw.EndUpdate();
        }

        public List<ListViewItem> Items()
        {
            return lstVw.Items.Cast<ListViewItem>().ToList();
        }

        public void Deactivate()
        {
            lastSelectedItem = null;
            lstVw.Items.Clear();
            updateItemCounter();
        }

        private void btnCima_Click(object sender, System.EventArgs e)
        {
            var selectedItems = new List<ListViewItem>();
            foreach (ListViewItem lvItem in lstVw.SelectedItems)
            {
                if (lvItem.Index == 0)
                    return;

                selectedItems.Add(lvItem);
            }

            lstVw.BeginUpdate();
            int index = 0;
            foreach (ListViewItem lvItem in selectedItems)
            {
                index = lstVw.Items.IndexOf(lvItem);
                lstVw.Items.Remove(lvItem);
                lstVw.Items.Insert(index - 1, lvItem);
            }
            lstVw.EndUpdate();
        }

        private void btnBaixo_Click(object sender, System.EventArgs e)
        {
            List<ListViewItem> selectedItems = new List<ListViewItem>();
            foreach (ListViewItem lvItem in lstVw.SelectedItems)
            {
                if (lvItem.Index == lstVw.Items.Count - 1)
                    return;

                selectedItems.Add(lvItem);
            }

            selectedItems.Reverse();

            lstVw.BeginUpdate();
            int index = 0;
            foreach (ListViewItem lvItem in selectedItems)
            {
                index = lstVw.Items.IndexOf(lvItem);
                lstVw.Items.Remove(lvItem);
                lstVw.Items.Insert(index + 1, lvItem);
            }
            lstVw.EndUpdate();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            List<ListViewItem> selectedItems = new List<ListViewItem>();
            foreach (ListViewItem lvItem in lstVw.SelectedItems)
            {
                if (lvItem.Index == 0)
                    return;

                selectedItems.Add(lvItem);
            }

            lstVw.BeginUpdate();
            int index = 0;
            foreach (ListViewItem lvItem in selectedItems)
            {
                lstVw.Items.Remove(lvItem);
                lstVw.Items.Insert(index, lvItem);
                index++;
            }
            lstVw.EndUpdate();
        }

        private void btnFim_Click(object sender, EventArgs e)
        {
            List<ListViewItem> selectedItems = new List<ListViewItem>();
            foreach (ListViewItem lvItem in lstVw.SelectedItems)
            {
                if (lvItem.Index == lstVw.Items.Count - 1)
                    return;

                selectedItems.Add(lvItem);
            }

            lstVw.BeginUpdate();
            foreach (ListViewItem lvItem in selectedItems)
            {
                lstVw.Items.Remove(lvItem);
                lstVw.Items.Insert(lstVw.Items.Count, lvItem);
            }
            lstVw.EndUpdate();
        }

        public List<ListViewItem> getSelectedItems()
        {
            return lstVw.SelectedItems.Cast<ListViewItem>().ToList();
        }

        public List<ListViewItem> getTrulySelectedItems()
        {
            List<ListViewItem> trulySelectedItemList = new List<ListViewItem>();
            foreach (ListViewItem item in lstVw.Items) { if (item.Selected) trulySelectedItemList.Add(item); }
            return trulySelectedItemList;
        }

        public void selectFirst()
        {
            if (lstVw.Items.Count > 0)
            {
                lstVw.Focus();
                lstVw.Items[0].Selected = true;
                lastSelectedItem = lstVw.Items[0];
                this.OnOrderManagerSelectedIndexChangedEvent(new BeforeNewSelectionEventArgs(lstVw.Items[0], true));
                this.updateToolBarButtons();
            }
        }

        public virtual void updateToolBarButtons()
        {
            btnBaixo.Enabled = lstVw.SelectedItems.Count > 0 && !readOnlyMode;
            btnCima.Enabled = lstVw.SelectedItems.Count > 0 && !readOnlyMode;
            btnInicio.Enabled = lstVw.SelectedItems.Count > 0 && !readOnlyMode;
            btnFim.Enabled = lstVw.SelectedItems.Count > 0 && !readOnlyMode;
            btnFullScreen.Enabled = lstVw.Items.Count > 0 && PermissoesHelper.AllowRead;
            updateItemCounter();
        }

        public virtual void updateItemCounter()
        {
            if (lstVw.Items.Count > 0) groupBox1.Text = String.Format("{0} ({1})", "Documentos", lstVw.Items.Count);
            else groupBox1.Text = "Documentos";
        }

        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            OnFullScreenInvoked(EventArgs.Empty);
        }

        private void ListViewOrderManager_Load(object sender, EventArgs e)
        {
            updateToolBarButtons();
        }

        public void ActivateReadOnlyMode()
        {
            readOnlyMode = true;
            updateToolBarButtons();
        }

        public void DeactivateReadOnlyMode()
        {
            readOnlyMode = false;
            updateToolBarButtons();
        }
    }
}