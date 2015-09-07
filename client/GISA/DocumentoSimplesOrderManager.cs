using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Fedora.FedoraHandler;
using GISA.Model;
using GISA.SharedResources;

namespace GISA
{
    public partial class DocumentoSimplesOrderManager : ListViewOrderManager
    {
        public DocumentoSimplesOrderManager()
        {
            InitializeComponent();
            ConfigListViewColumns();
            GetExtraResources();

            this.colDesignacao.Text = "Documento Subordinado";
            this.colDesignacao.Width = 270;

            btnCheckIntegrity.Visible = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsIntegridadeEnable();
        }

        private void ConfigListViewColumns()
        {
            this.lstVw.Columns.Clear();
            this.lstVw.Columns.Add(this.colObjDigFed);
            this.lstVw.Columns.Add(this.colDesignacaoOD);
            this.lstVw.Columns.Add(this.colPublicado);
            this.lstVw.Columns.Add(this.colDesignacao);
        }

        private void GetExtraResources()
        {
            btnAdd.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
            btnEdit.Image = SharedResourcesOld.CurrentSharedResources.Editar;
            btnRemove.Image = SharedResourcesOld.CurrentSharedResources.Apagar;
            btnCheckIntegrity.Image = SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedActionIcons), "ActionCheckIntegrity_enabled_16x16.png");

            CurrentToolTip.SetToolTip(btnAdd, SharedResourcesOld.CurrentSharedResources.AdicionarString);
            CurrentToolTip.SetToolTip(btnEdit, SharedResourcesOld.CurrentSharedResources.EditarString);
            CurrentToolTip.SetToolTip(btnRemove, SharedResourcesOld.CurrentSharedResources.ApagarString);
            CurrentToolTip.SetToolTip(btnCheckIntegrity, SharedResourceSets.CurrentSharedResourceSets.getTextResource(typeof(SharedActionIcons), "ActionCheckIntegrity"));
        }

        public event EventHandler<EventArgs> NewInvoked;
        protected virtual void OnNewInvoked(EventArgs e)
        {
            if (this.NewInvoked != null)
                NewInvoked(this, e);
        }

        public event EventHandler<EventArgs> EditInvoked;
        protected virtual void OnEditInvoked(EventArgs e)
        {
            if (this.EditInvoked != null)
                EditInvoked(this, e);
        }

        public event EventHandler<EventArgs> RemoveInvoked;
        protected virtual void OnRemoveInvoked(EventArgs e)
        {
            if (this.RemoveInvoked != null)
                RemoveInvoked(this, e);
        }

        public ListViewItem CreateItem(string nivelDesignacao, string odTitulo, string odPid, bool? isPublicado, Object tag)
        {
            var item = new ListViewItem();
            item.SubItems.AddRange(new string[] { string.Empty, string.Empty, string.Empty });
            item.Tag = tag;
            item.SubItems[colDesignacao.Index].Text = nivelDesignacao;

            RefreshItem(item, odTitulo, isPublicado, odPid);

            return item;
        }

        internal void RefreshItem(ListViewItem item, string odTitulo, bool? isPublicado, string odPid)
        {
            item.SubItems[colDesignacaoOD.Index].Text = odTitulo;
            item.SubItems[colPublicado.Index].Text = isPublicado.HasValue ? (isPublicado.Value ? "Sim" : "Não") : "";
            item.SubItems[colObjDigFed.Index].Text = odPid;
        }

        public void DisableToolBar()
        {
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnRemove.Enabled = false;
            btnBaixo.Enabled = false;
            btnCima.Enabled = false;
            btnInicio.Enabled = false;
            btnFim.Enabled = false;
            btnFullScreen.Enabled = lstVw.Items.Count > 0 && PermissoesHelper.AllowRead;
            btnCheckIntegrity.Enabled = false;
        }

        public void SetEditMixedMode()
        {
            base.updateToolBarButtons();
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnRemove.Enabled = false;
        }

        public override void updateToolBarButtons()
        {
            base.updateToolBarButtons();
            var odsSimplesCnt = lstVw.SelectedItems.Cast<ListViewItem>().Select(lvi => lvi.Tag).OfType<ObjDigSimples>().Count();
            btnAdd.Enabled = true;
            btnEdit.Enabled = odsSimplesCnt == 1 && !readOnlyMode;
            btnRemove.Enabled = odsSimplesCnt == 1 && !readOnlyMode;
            btnCheckIntegrity.Enabled = odsSimplesCnt == 1;
        }

        public override void updateItemCounter()
        {
            var odsSimplesCnt = lstVw.SelectedItems.Cast<ListViewItem>().Select(lvi => lvi.Tag).OfType<ObjDigSimples>().Count();
            if (odsSimplesCnt > 0) groupBox1.Text = String.Format("{0} ({1})", "Objetos Digitais Simples", odsSimplesCnt);
            else groupBox1.Text = "Objetos Digitais Simples";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnNewInvoked(EventArgs.Empty);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditInvoked(EventArgs.Empty);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            OnRemoveInvoked(EventArgs.Empty);
        }

        private void btnCheckIntegrity_Click(object sender, EventArgs e)
        {
            if (lstVw.SelectedItems.Count == 1)
            {
                Fedora.FedoraHandler.ObjDigSimples objDigital = lstVw.SelectedItems[0].Tag as Fedora.FedoraHandler.ObjDigSimples;
                SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.CheckIntegrity(objDigital);
            }
        }
    }
}
