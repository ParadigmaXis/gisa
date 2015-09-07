using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.SharedResources;
using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Controls.Nivel
{
    public partial class FormSelectNivel : Form
    {
        private bool isDocumentalView = false;
        private GISADataset.NivelRow selectedDocument = null;
        public GISADataset.NivelRow SelectedDocument
        {
            get { return selectedDocument; }
        }

        public FormSelectNivel()
        {
            InitializeComponent();
            GetExtraResources();

            // existe um modo só de vista documental onde o botão toggle está inactivo; assim força, quando em modo normal, que o botão está activo
            this.tsbToggleView.Enabled = true;

            this.nivelNavigator1.ViewToggled += new NivelNavigator.ViewToggledEventHandler(nivelNavigator1_ViewToggled);
            this.nivelNavigator1.BeforeNodeSelection += new NivelNavigator.BeforeNodeSelectionEventHandler(nivelNavigator1_BeforeNodeSelection);
            this.nivelNavigator1.BeforeListItemSelection += new NivelNavigator.BeforeListItemSelectionEventHandler(nivelNavigator1_BeforeListItemSelection);
            this.nivelNavigator1.UpdateToolBarButtonsEvent += new NivelNavigator.UpdateToolBarButtonsEventHandler(nivelNavigator1_UpdateToolBarButtonsEvent);
            this.nivelNavigator1.OtherInitializations();
            UpdateToolbarButtons();
        }

        void nivelNavigator1_UpdateToolBarButtonsEvent(EventArgs e)
        {
            UpdateToolbarButtons();
        }

        public List<long> SelectableType { get; set; }

        void UpdatePermissions()
        {
            this.nivelNavigator1.UpdatePermissions();
        }

        void nivelNavigator1_BeforeListItemSelection(object sender, BeforeNewSelectionEventArgs e)
        {
            selectedDocument = e.ItemToBeSelected.Tag as GISADataset.NivelRow;

            btnOk.Enabled = ((selectedDocument != null) && SelectableType.Contains(selectedDocument.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].IDTipoNivelRelacionado));
        }

        void nivelNavigator1_BeforeNodeSelection(GISA.Controls.Localizacao.ControloNivelList.BeforeNewSelectionEventArgs e)
        {
            btnOk.Enabled = false;
        }

        void nivelNavigator1_ViewToggled(NivelNavigator.ToggleState state)
        {
            UpdateToolbarButtons();
        }

        private void GetExtraResources()
        {
            toolStrip1.ImageList = SharedResourcesOld.CurrentSharedResources.NVLManipulacaoImageList;
            tsbToggleView.ImageIndex = 7; // 6/7
            tsbFilter.ImageIndex = 8;

            string[] strs = SharedResourcesOld.CurrentSharedResources.NVLManipulacaoStrings;
            tsbToggleView.ToolTipText = strs[7]; // 6/7
            tsbFilter.ToolTipText = strs[8];
        }

        public void UpdateToolbarButtons()
        {
            this.tsbToggleView.Enabled = this.nivelNavigator1.isTogglable() && PermissoesHelper.AllowExpand;
            this.tsbFilter.Enabled = this.nivelNavigator1.PanelToggleState == NivelNavigator.ToggleState.Documental;
        }

        private void tsbToggleView_Click(object sender, EventArgs e)
        {
            isDocumentalView = !isDocumentalView;

            if (isDocumentalView)
            {
                this.nivelNavigator1.LoadVistaDocumental(this.nivelNavigator1.SelectedNode.RelacaoHierarquicaRow);
            }
            else
            {
                this.nivelNavigator1.LoadSelectedNode();
                tsbFilter.Checked = false;
            }

            this.nivelNavigator1.FilterVisibility = tsbFilter.Pressed;
            this.nivelNavigator1.ToggleView(isDocumentalView);
            UpdateToolbarButtons();
        }

        private void tsbFilter_Click(object sender, EventArgs e)
        {
            this.nivelNavigator1.FilterVisibility = tsbFilter.Checked;
        }

        public void SetOnlyDocViewMode(long nivelIDProdutor)
        {
            NivelRule.Current.LoadNivelParents(nivelIDProdutor, GisaDataSetHelper.GetInstance(), GisaDataSetHelper.GetConnection());
            var prodRhRow = GisaDataSetHelper.GetInstance().RelacaoHierarquica.Cast<GISADataset.RelacaoHierarquicaRow>().FirstOrDefault(r => r.RowState != DataRowState.Deleted && r.ID == nivelIDProdutor);
            // prodRhRow pode ser null porque o produtor selecionado pode ainda não estar pendurado na estrutura orgânica
            if (prodRhRow != null)
                this.nivelNavigator1.LoadVistaDocumental(prodRhRow); // o IDUpper não é relevante para este caso
            this.nivelNavigator1.FilterVisibility = tsbFilter.Pressed;
            this.nivelNavigator1.ToggleView(true);
            UpdateToolbarButtons();
            this.tsbToggleView.Enabled = false;
        }
    }
}