using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Model;
using GISA.SharedResources;

namespace GISA.Controls.ControloAut
{
    public partial class FormThesaurusNavigator : Form
    {
        private State currentState = State.filter;
        private enum  State {
            filter = 0,
            navigation = 1
        }
        public FormThesaurusNavigator()
        {
            InitializeComponent();

            this.controlTermosIndexacao1.NavigationMode = true;

            this.grpNavegacao.Visible = false;
            this.controloAutList1.Visible = true;
            this.controloAutList1.FilterVisible = true;

            this.controloAutList1.BeforeNewListSelection += new PaginatedListView.BeforeNewListSelectionEventHandler(controloAutList1_BeforeNewListSelection);
            this.controlTermosIndexacao1.AfterSelect += new TreeViewEventHandler(controlTermosIndexacao1_AfterSelect);

            this.btnNavegar.Text = "Tesauro";
            this.btnNavegar.ImageList = SharedResourcesOld.CurrentSharedResources.NavThesaurusImageList;
            this.btnNavegar.ImageIndex = 0;
            this.toolTip1.SetToolTip(this.btnNavegar, SharedResourcesOld.CurrentSharedResources.NavThesaurusStrings[0]);
        }

        public TipoNoticiaAut[] AllowedNoticiaAut { set { this.controloAutList1.AllowedNoticiaAut(value); this.controloAutList1.txtFiltroDesignacao.Clear(); this.controloAutList1.ReloadList(); } }

        private void btnNavegar_Click(object sender, EventArgs e)
        {
            currentState = currentState == State.filter ? State.navigation : State.filter;
            this.grpNavegacao.Visible = currentState == State.navigation;
            this.controloAutList1.Visible = currentState == State.filter;
            this.btnNavegar.ImageIndex = currentState == State.filter ? 0 : 1;
            this.btnNavegar.Text = currentState == State.filter ? "Tesauro" : "Voltar";
            this.toolTip1.SetToolTip(this.btnNavegar, currentState == State.filter ? SharedResourcesOld.CurrentSharedResources.NavThesaurusStrings[0] : SharedResourcesOld.CurrentSharedResources.NavThesaurusStrings[1]);

            if (currentState == State.navigation)
            {
                var cadRow = this.controloAutList1.SelectedItems[0].Tag as GISADataset.ControloAutDicionarioRow;

                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try
                {
                    this.controlTermosIndexacao1.trVwTermoIndexacao.Nodes.Clear();
                    this.controlTermosIndexacao1.LoadData(cadRow.ControloAutRow, ho.Connection);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    ho.Dispose();
                }
            }
        }

        private void controlTermosIndexacao1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //UpdateButtons();
            btnAdicionar.Enabled = this.controlTermosIndexacao1.SelectedNode != null
                    && (this.controlTermosIndexacao1.SelectedNode.Tag is GISADataset.ControloAutDicionarioRow || this.controlTermosIndexacao1.SelectedNode.Tag is GISADataset.ControloAutRelRow);
            btnNavegar.Enabled = currentState == State.navigation;
        }

        private void controloAutList1_BeforeNewListSelection(object sender, BeforeNewSelectionEventArgs e)
        {
            //UpdateButtons();
            btnAdicionar.Enabled = this.controloAutList1.SelectedItems.Count > 0 || e.ItemToBeSelected != null;
            btnNavegar.Enabled = currentState == State.filter && (this.controloAutList1.SelectedItems.Count == 1 || (e.ItemToBeSelected != null && e.ItemToBeSelected.ListView != null));
        }

        //private void UpdateButtons()
        //{
        //    if (currentState == State.filter)
        //        btnAdicionar.Enabled = this.controloAutList1.SelectedItems.Count > 0;
        //    else
        //        btnAdicionar.Enabled = this.controlTermosIndexacao1.SelectedNode != null 
        //            && (this.controlTermosIndexacao1.SelectedNode.Tag is GISADataset.ControloAutDicionarioRow || this.controlTermosIndexacao1.SelectedNode.Tag is GISADataset.ControloAutRelRow);

        //    btnNavegar.Enabled = (currentState == State.filter && this.controloAutList1.SelectedItems.Count == 1) || currentState == State.navigation;
        //}

        public List<GISADataset.ControloAutDicionarioRow> SelectTermos
        {
            get
            {
                if (currentState == State.filter)
                    return this.controloAutList1.SelectedItems.Select(item => (GISADataset.ControloAutDicionarioRow)item.Tag).ToList();
                else
                {
                    var dataRow = this.controlTermosIndexacao1.SelectedNode.Tag;
                    if (dataRow is GISADataset.ControloAutRelRow)
                    {
                        var cadRow = ((GISADataset.ControloAutRelRow)dataRow).ControloAutRowByControloAutControloAutRelAlias.GetControloAutDicionarioRows().Where(cad => cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada).SingleOrDefault();
                        if (cadRow != null)
                        {
                            if (cadRow.DicionarioRow.Termo.Equals(this.controlTermosIndexacao1.SelectedNode.Text))
                                return new List<GISADataset.ControloAutDicionarioRow>() { cadRow };
                            else
                            {
                                cadRow = ((GISADataset.ControloAutRelRow)dataRow).ControloAutRowByControloAutControloAutRel.GetControloAutDicionarioRows().Where(cad => cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada).SingleOrDefault();
                                if (cadRow != null)
                                    return new List<GISADataset.ControloAutDicionarioRow>() { cadRow };
                            }
                        }
                    }
                    else if (dataRow is GISADataset.ControloAutDicionarioRow)
                        return new List<GISADataset.ControloAutDicionarioRow>() { (GISADataset.ControloAutDicionarioRow)dataRow };
                    return new List<GISADataset.ControloAutDicionarioRow>();
                }
            }
        }
    }
}