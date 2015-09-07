using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Model;
using GISA.SharedResources;
using GISA.Utils;

namespace GISA
{
    public partial class PanelDepIdentificacao : GISA.GISAPanel
    {
        private NivelDragDrop DragDropHandlerUnidFisicas;
        protected GISADataset.DepositoRow CurrentDeposito;
        private HashSet<UFRule.UnidadeFisicaInfo> ufsAssociadas;

        public PanelDepIdentificacao()
        {
            InitializeComponent();

            //Add any initialization after the InitializeComponent() call
            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                GetExtraResources();

                DragDropHandlerUnidFisicas = new NivelDragDrop(lstVwUnidadesFisicasAssoc, TipoNivel.OUTRO);
                DragDropHandlerUnidFisicas.AcceptNivelRow += AcceptNivelRow;

                lstVwUnidadesFisicasAssoc.SelectedIndexChanged += new EventHandler(lstVwUnidadesFisicasAssoc__SelectedIndexChanged);
                lstVwUnidadesFisicasAssoc.KeyUp += new KeyEventHandler(lstVwUnidadesFisicasAssoc_KeyUp);

                btnRemove.Click += new EventHandler(btnRemove_Click);

                UpdateListButtonsState();
            }
        }

        private void GetExtraResources()
        {
            btnRemove.Image = SharedResourcesOld.CurrentSharedResources.Apagar;

            base.ParentChanged += PanelDepIdentificacao_ParentChanged;
        }

        // runs only once. sets tooltip as soon as it's parent appears
        private void PanelDepIdentificacao_ParentChanged(object sender, System.EventArgs e)
        {
            MultiPanel.CurrentToolTip.SetToolTip(btnRemove, SharedResourcesOld.CurrentSharedResources.ApagarString);
            base.ParentChanged -= PanelDepIdentificacao_ParentChanged;
        }

        public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
        {
            IsLoaded = false;
            CurrentDeposito = (GISADataset.DepositoRow)CurrentDataRow;

            try
            {
                ufsAssociadas = DepositoRule.Current.LoadDepIdentificacaoData(GisaDataSetHelper.GetInstance(), CurrentDeposito.ID, conn);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw ex;
            }

            IsLoaded = true;
        }

        public override void ModelToView()
        {
            IsPopulated = false;
            txtDesignacao.Text = CurrentDeposito.Designacao;
            txtMetragem.Text = CurrentDeposito.MetrosLineares.ToString();

            PopulateAssociacoes(ufsAssociadas);
            IsPopulated = true;
        }

        private void PopulateAssociacoes(HashSet<UFRule.UnidadeFisicaInfo> ufsAssociadas)
        {
            var lst = new List<ListViewItem>();
            ufsAssociadas.ToList().ForEach(uf =>
            {
                lst.Add(new ListViewItem(new string[] { 
                    uf.Codigo , uf.Designacao, uf.Tipo, 
                    GUIHelper.GUIHelper.FormatDimensoes(uf.Altura, uf.Largura, uf.Profundidade, uf.Medida), uf.Cota, 
                    GISA.Utils.GUIHelper.FormatDateInterval(uf.InicioAno, uf.InicioMes, uf.InicioDia, uf.FimAno, uf.FimMes, uf.FimDia), 
                    uf.Eliminado.ToString() }) { Tag = uf });
            });
            lstVwUnidadesFisicasAssoc.BeginUpdate();
            lstVwUnidadesFisicasAssoc.Items.AddRange(lst.ToArray());
            lstVwUnidadesFisicasAssoc.EndUpdate();
        }

        public override void ViewToModel()
        {
            
        }

        public override void Deactivate()
        {
            GUIHelper.GUIHelper.clearField(lstVwUnidadesFisicasAssoc);
            GUIHelper.GUIHelper.clearField(txtDesignacao);
            GUIHelper.GUIHelper.clearField(txtMetragem);
            CurrentDeposito = null;
        }

        private void AcceptNivelRow(GISADataset.NivelRow NivelRow)
        {
            // aceitar o drop apenas se se tratar de uma UF ainda não associada
            var nufRow = NivelRow.GetNivelDesignadoRows()[0].GetNivelUnidadeFisicaRows()[0];
            var ufs = GetUFInfo(NivelRow);
            var nufdRow = nufRow.GetNivelUnidadeFisicaDepositoRows().SingleOrDefault();
            if (nufdRow == null)
            {
                nufdRow = GisaDataSetHelper.GetInstance().NivelUnidadeFisicaDeposito.NewNivelUnidadeFisicaDepositoRow();
                nufdRow.NivelUnidadeFisicaRow = nufRow;
                nufdRow.DepositoRow = CurrentDeposito;
                nufdRow.Versao = new byte[]{};
                nufdRow.isDeleted = 0;
                GisaDataSetHelper.GetInstance().NivelUnidadeFisicaDeposito.AddNivelUnidadeFisicaDepositoRow(nufdRow);
                PopulateAssociacoes(ufs);
                ufsAssociadas.Add(ufs.ToList().Single());
            }
            else if (nufdRow.IDDeposito != CurrentDeposito.ID)
            {
                // ToDo: deixa-se substituir?
            }
            else if (nufdRow.IDDeposito == CurrentDeposito.ID && !ufsAssociadas.Contains(ufs.ToList().Single()))
            {
                PopulateAssociacoes(ufs);
                ufsAssociadas.Add(ufs.ToList().Single());
            }
        }

        private HashSet<UFRule.UnidadeFisicaInfo> GetUFInfo(GISADataset.NivelRow NivelRow)
        {
            var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            var ufs = new HashSet<UFRule.UnidadeFisicaInfo>();
            try
            {
                ufs = DepositoRule.Current.LoadUFData(NivelRow.ID, ho.Connection);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
            finally
            {
                ho.Dispose();
            }
            return ufs;            
        }

        public void btnRemove_Click(object sender, EventArgs e)
        {
            removeSelectedItem();
        }

        private void lstVwUnidadesFisicasAssoc_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyValue == Convert.ToInt32(Keys.Delete) && btnRemove.Enabled)
                removeSelectedItem();
        }

        private void removeSelectedItem()
        {
            lstVwUnidadesFisicasAssoc.SelectedItems.Cast<ListViewItem>().ToList().ForEach(item =>
            {
                var uf = item.Tag as UFRule.UnidadeFisicaInfo;
                var nufRow = GisaDataSetHelper.GetInstance().NivelUnidadeFisica.Cast<GISADataset.NivelUnidadeFisicaRow>()
                    .Single(nuf => nuf.ID == uf.ID);
                nufRow.GetNivelUnidadeFisicaDepositoRows().Single().Delete();
                ufsAssociadas.Remove(uf);
                item.Remove();
            });

            UpdateListButtonsState();
        }

        private void lstVwUnidadesFisicasAssoc__SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateListButtonsState();
        }

        private void UpdateListButtonsState()
        {
            btnRemove.Enabled = lstVwUnidadesFisicasAssoc.SelectedItems.Count > 0;
        }

        private void ToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == MultiPanel.ToolBarButtonAuxList)
                ToggleUnidadesFisicasSupportPanel(MultiPanel.ToolBarButtonAuxList.Pushed);
        }

        private void ToggleUnidadesFisicasSupportPanel(bool showIt)
        {
            if (showIt)
            {
                // Make sure the button is pushed
                MultiPanel.ToolBarButtonAuxList.Pushed = true;

                // Indicação que um painel está a ser usado como suporte
                ((frmMain)this.TopLevelControl).isSuportPanel = true;

                // Show the panel with all unidades fisicas
                ((frmMain)this.TopLevelControl).PushMasterPanel(typeof(MasterPanelUnidadesFisicas));

                MasterPanelUnidadesFisicas masterPanelUF =
                    (MasterPanelUnidadesFisicas)(((frmMain)this.TopLevelControl).MasterPanel);
                //masterPanelUF.ufList.ContextNivelRow = CurrentFRDBase.NivelRow;
                masterPanelUF.ufList.ReloadList();

                // é necessário actualizar o estado dos botões neste ponto uma vez que
                // nenhuma unidade física é definida como contexto automaticamente (a primeira a
                // ser apresentada na lista)
                masterPanelUF.UpdateToolBarButtons();
                masterPanelUF.ufList.MultiSelectListView = true;
            }
            else
            {
                // Make sure the button is not pushed            
                MultiPanel.ToolBarButtonAuxList.Pushed = false;

                // Remove the panel with all unidades fisicas
                if (this.TopLevelControl != null)
                {
                    if (((frmMain)this.TopLevelControl).MasterPanel is MasterPanelUnidadesFisicas)
                    {
                        MasterPanelUnidadesFisicas masterPanelUF =
                            (MasterPanelUnidadesFisicas)(((frmMain)this.TopLevelControl).MasterPanel);

                        masterPanelUF.ufList.ContextNivelRow = null;
                        //masterPanelUF.ufList.ClearFilter();

                        // Indicação que nenhum painel está a ser usado como suporte
                        ((frmMain)this.TopLevelControl).isSuportPanel = false;
                        ((frmMain)this.TopLevelControl).PopMasterPanel(typeof(MasterPanelUnidadesFisicas));
                    }
                }
            }
        }

        public override void OnShowPanel()
        {
            //Show the button that brings up the support panel
            //and select it by default.
            MultiPanel.ToolBar.ButtonClick += ToolBar_ButtonClick;
            MultiPanel.ToolBarButtonAuxList.Visible = true;
        }

        public override void OnHidePanel()
        {
            ToggleUnidadesFisicasSupportPanel(false);
            //Deactivate Toolbar Buttons
            MultiPanel.ToolBar.ButtonClick -= ToolBar_ButtonClick;
            MultiPanel.ToolBarButtonAuxList.Visible = false;
        }
    }
}
