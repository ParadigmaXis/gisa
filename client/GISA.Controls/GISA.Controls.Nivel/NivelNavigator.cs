using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Controls.Localizacao;
using GISA.Model;
using GISA.SharedResources;

namespace GISA.Controls.Nivel
{
    public partial class NivelNavigator : UserControl
    {
        public NivelNavigator()
        {
            InitializeComponent();

            this.nivelEstruturalList1.FilterVisible = true;
            this.nivelDocumentalListNavigator1.FilterVisible = false;
        }

        public List<ListViewItem> SelectedItems { get { return this.mPanelToggleState == ToggleState.Estrutural ? this.nivelEstruturalList1.SelectedItems.Cast<ListViewItem>().ToList() : this.nivelDocumentalListNavigator1.SelectedItems.Cast<ListViewItem>().ToList(); } }
        public List<ListViewItem> Items { get { return this.nivelDocumentalListNavigator1.Items.Cast<ListViewItem>().ToList(); } }
        public GISADataset.NivelRow SelectedNivel { get { return this.mPanelToggleState == ToggleState.Documental ? this.nivelDocumentalListNavigator1.GetSelectedNivel() : this.nivelEstruturalList1.GetSelectedNivel(); } }
        public List<GISADataset.NivelRow> SelectedNiveis { get { return this.nivelDocumentalListNavigator1.GetSelectedNiveis(); } }
        public long ContextBreadCrumbsPathID { get { return this.nivelDocumentalListNavigator1.ContextBreadCrumbsPathID; } }
        public long ContextBreadCrumbsPathIDUpper { get { return this.nivelDocumentalListNavigator1.ContextBreadCrumbsPathIDUpper; } }
        public bool FilterVisibility { 
            set {
                if (this.mPanelToggleState == ToggleState.Estrutural) {
                    this.nivelEstruturalList1.Visible = value;
                    this.mEPFilterMode = value;
                    this.controloNivelListEstrutural1.Visible = !value;
                }
                else
                    this.nivelDocumentalListNavigator1.FilterVisible = value; 
            }
            get
            {
                if (this.mPanelToggleState == ToggleState.Estrutural)
                    return this.mEPFilterMode;
                else
                    return this.nivelDocumentalListNavigator1.FilterVisible;
            }
        }
        private bool mEPFilterMode = false;
        public bool EPFilterMode { get { return mEPFilterMode; } }
        public bool ExcluirRequisitados { set { this.nivelDocumentalListNavigator1.ExcluirRequisitados = value; } }
        public bool IsParentSupport { set { this.nivelDocumentalListNavigator1.IsParentSupport = value; } }

        public GISATreeNode SelectedNode { 
            get { return this.controloNivelListEstrutural1.SelectedNode; }
            set { this.controloNivelListEstrutural1.trVwLocalizacao.SelectedNode = value; }
        }

        public bool MultiSelect {
            get { return this.nivelDocumentalListNavigator1.MultiSelectListView; } 
            set { this.nivelDocumentalListNavigator1.MultiSelectListView = value; } 
        }

        public long? MovimentoID
        {
            get {   // return this.nivelDocumentalListNavigator1.MovimentoID.Value;
                long? _movimentoID = this.nivelDocumentalListNavigator1.MovimentoID;
                if (_movimentoID != null)
                    return _movimentoID.Value;
                else
                    return null;
                }
            set { this.nivelDocumentalListNavigator1.MovimentoID = value; }
        }

        public void LoadVistaEstrutural()
        {
            this.controloNivelListEstrutural1.LoadContents();
        }

        public bool LoadSelectedNode()
        {
            return this.controloNivelListEstrutural1.LoadSelectedNode();
        }

        public void LoadVistaDocumental(GISADataset.RelacaoHierarquicaRow rhRow)
        {
            this.nivelDocumentalListNavigator1.InitialLoad(rhRow);
        }

        public void coluna_Requisitado_Visible(bool visible) {
            this.nivelDocumentalListNavigator1.coluna_Requisitado_Visible(visible);
        }

        public ArrayList GetCodigoCompletoCaminhoUnico(ListViewItem item)
        {
            return this.nivelDocumentalListNavigator1.GetCodigoCompletoCaminhoUnico(item);
        }

        public void SelectParentNode(GISATreeNode parentNode)
        {
            this.controloNivelListEstrutural1.SelectParentNode(parentNode);
        }

        public void ClearFiltro()
        {
            if (this.mPanelToggleState == ToggleState.Estrutural)
                this.nivelEstruturalList1.ClearFiltro();
            else
                this.nivelDocumentalListNavigator1.ClearFiltro();
        }

        public void ReloadList()
        {
            ReloadList(null);
        }

        public void ReloadList(DataRow dr)
        {
            if (this.mPanelToggleState == ToggleState.Estrutural)
                this.nivelEstruturalList1.ReloadList(dr);
            else
                this.nivelDocumentalListNavigator1.ReloadList(dr);
        }

        public void UpdateSelectedNodeName(string designacao)
        {
            this.controloNivelListEstrutural1.UpdateSelectedNodeName(designacao);
        }

        public void UpdateSelectedListItemName(string designacao)
        {
            this.nivelDocumentalListNavigator1.UpdateSelectedNodeName(designacao);
        }

        public void RemoveFromTreeview(GISATreeNode node, string key)
        {
            this.controloNivelListEstrutural1.RemoveFromTreeview(node, key);
        }

        public void RemoveSelectedLVItem()
        {
            this.nivelDocumentalListNavigator1.RemoveSelectedItem();
        }

        public ListViewItem UpdatePermissions()
        {
            ListViewItem item = null;
            // Actualizar as permissões do Nível quando este é uma entidade detentora e é selecionado quando se acede
            // à área da Recolha
            if (this.mPanelToggleState == ToggleState.Estrutural && !mEPFilterMode)
                PermissoesHelper.UpdateNivelPermissions(this.controloNivelListEstrutural1.SelectedNivelRow, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
            else if (this.mPanelToggleState == ToggleState.Estrutural && mEPFilterMode)
            {
                if (this.nivelDocumentalListNavigator1.SelectedItems.Count > 0)
                {
                    PermissoesHelper.UpdateNivelPermissions((GISADataset.NivelRow)(this.nivelDocumentalListNavigator1.SelectedItems[0].Tag), SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
                    item = this.nivelDocumentalListNavigator1.SelectedItems[0];
                }
            }
            else
            {
                if (this.nivelDocumentalListNavigator1.SelectedItems.Count > 0)
                {
                    PermissoesHelper.UpdateNivelPermissions((GISADataset.NivelRow)(this.nivelDocumentalListNavigator1.SelectedItems[0].Tag), SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
                    item = this.nivelDocumentalListNavigator1.SelectedItems[0];
                }
                else
                {
                    // não há nenhum nível documental selecionado
                    var nRow = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().Where(r => r.ID == this.nivelDocumentalListNavigator1.BreadCrumbsPath1.getBreadCrumbsPathContextID).SingleOrDefault();
                    if (nRow != null)
                        PermissoesHelper.UpdateNivelPermissions(nRow, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
                    else
                    {
                        // esta situação nunca deveria ocorrer
                        Debug.WriteLine("NivelRow not found!!! (ID = " + this.nivelDocumentalListNavigator1.BreadCrumbsPath1.getBreadCrumbsPathContextID.ToString());
                        PermissoesHelper.UpdateNivelPermissions(null, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
                    }
                }
            }
            return item;
        }

        public void OtherInitializations()
        {
            this.controloNivelListEstrutural1.mTipoNivelRelLimitExcl = TipoNivelRelacionado.SR;

            AddHandlers();
            resetEstrutura();
        }

        protected void SetToolTip_action(object sender, object item)
        {
            if (item != null)
            {
                if (sender == this.nivelEstruturalList1)
                {
                    ListViewItem lvItem = (ListViewItem)item;
                    if (!(((GISATreeNode)item).NivelRow == null) && !(((GISATreeNode)item).NivelRow.RowState == DataRowState.Detached))
                    {
                        var nRow = ((GISATreeNode)item).NivelRow;
                        var cod = string.Empty;
                        GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                        try
                        {
                            cod = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.GetCodigoCompletoNivel(nRow.ID, ho.Connection);
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine(ex);
                            throw;
                        }
                        finally { ho.Dispose(); }

                        lvItem.ToolTipText = cod;
                    }
                }
                else if (sender == this.nivelDocumentalListNavigator1)
                {
                    ListViewItem lvItem = (ListViewItem)item;
                    if (!(((GISADataset.NivelRow)lvItem.Tag).RowState == DataRowState.Detached) || !(this.controloNivelListEstrutural1.SelectedNode.NivelRow.RowState == DataRowState.Detached))
                    {
                        ArrayList pathEstrut = ControloNivelList.GetCodigoCompletoCaminhoUnico(this.controloNivelListEstrutural1.SelectedNode);
                        ArrayList pathDoc = this.nivelDocumentalListNavigator1.GetCodigoCompletoCaminhoUnico(lvItem);
                        lvItem.ToolTipText = string.Format("{0}/{1}", GISA.Model.Nivel.buildPath(pathEstrut), GISA.Model.Nivel.buildPath(pathDoc));
                    }
                }
                else
                {
                    if (!(((GISATreeNode)item).NivelRow == null) && !(((GISATreeNode)item).NivelRow.RowState == DataRowState.Detached))
                    {
                        ArrayList pathEstrut = ControloNivelList.GetCodigoCompletoCaminhoUnico((GISATreeNode)item);

                        if (pathEstrut.Count == 0)
                            ((GISATreeNode)item).ToolTipText = ((GISATreeNode)item).NivelRow.Codigo;
                        else
                            ((GISATreeNode)item).ToolTipText = string.Format("{0}", GISA.Model.Nivel.buildPath(pathEstrut));
                    }
                }
            }
        }

        public void AddHandlers()
        {
            this.nivelEstruturalList1.BeforeNewListSelection += NivelDocumentalListNavigator1_BeforeNewListSelection;
            this.nivelDocumentalListNavigator1.BeforeNewListSelection += NivelDocumentalListNavigator1_BeforeNewListSelection;
            this.controloNivelListEstrutural1.beforeNewSelectionEvent += beforeNewSelection_Action;

            this.nivelDocumentalListNavigator1.setToolTipEvent += SetToolTip_action;
            this.nivelEstruturalList1.setToolTipEvent += SetToolTip_action;
            this.controloNivelListEstrutural1.setToolTipEvent += SetToolTip_action;

            this.nivelDocumentalListNavigator1.UpdateToolBarButtonsEvent += UpdateToolBarButtons_Action;
            this.controloNivelListEstrutural1.UpdateToolBarButtonsEvent += UpdateToolBarButtons_Action;
        }

        public void RemoveHandlers()
        {
            this.nivelEstruturalList1.BeforeNewListSelection -= NivelDocumentalListNavigator1_BeforeNewListSelection;
            this.nivelDocumentalListNavigator1.BeforeNewListSelection -= NivelDocumentalListNavigator1_BeforeNewListSelection;
            this.controloNivelListEstrutural1.beforeNewSelectionEvent -= beforeNewSelection_Action;

            this.nivelDocumentalListNavigator1.setToolTipEvent -= SetToolTip_action;
            this.nivelEstruturalList1.setToolTipEvent -= SetToolTip_action;
            this.controloNivelListEstrutural1.setToolTipEvent -= SetToolTip_action;

            this.nivelDocumentalListNavigator1.UpdateToolBarButtonsEvent -= UpdateToolBarButtons_Action;
            this.controloNivelListEstrutural1.UpdateToolBarButtonsEvent -= UpdateToolBarButtons_Action;
        }

        public delegate void BeforeNodeSelectionEventHandler(ControloNivelList.BeforeNewSelectionEventArgs e);
        public event BeforeNodeSelectionEventHandler BeforeNodeSelection;

        private void beforeNewSelection_Action(ControloNivelList.BeforeNewSelectionEventArgs e)
        {
            if (BeforeNodeSelection != null)
                BeforeNodeSelection(e);
        }

        public delegate void AfterNodeSelectionEventHandler();
        public event AfterNodeSelectionEventHandler AfterNodeSelection;

        private void afterNewSelection_Action()
        {
            if (AfterNodeSelection != null)
                AfterNodeSelection();
        }

        public delegate void BeforeListItemSelectionEventHandler(object sender, BeforeNewSelectionEventArgs e);
        public event BeforeListItemSelectionEventHandler BeforeListItemSelection;

        private void NivelDocumentalListNavigator1_BeforeNewListSelection(object sender, BeforeNewSelectionEventArgs e)
        {
            if (BeforeListItemSelection != null)
                BeforeListItemSelection(this, e);
        }

        public event UpdateToolBarButtonsEventHandler UpdateToolBarButtonsEvent;
        public delegate void UpdateToolBarButtonsEventHandler(EventArgs e);

        private void UpdateToolBarButtons_Action(EventArgs e)
        {
            if (UpdateToolBarButtonsEvent != null)
                UpdateToolBarButtonsEvent(new EventArgs());
        }

        public event KeyUpDeleteEventHandler KeyUpDeleteEvent;
        public delegate void KeyUpDeleteEventHandler(EventArgs e);

        private void KeyUpDelete_Action(EventArgs e)
        {
            if (KeyUpDeleteEvent != null)
                KeyUpDeleteEvent(new EventArgs());
        }


        public void AddDragHandlers()
        {
            this.controloNivelListEstrutural1.trVwLocalizacao.ItemDrag += dragNV_Action;
            this.nivelDocumentalListNavigator1.ItemDrag += dragNV_Action;
        }

        public void RemoveDragHandlers()
        {
            this.controloNivelListEstrutural1.trVwLocalizacao.ItemDrag -= dragNV_Action;
            this.nivelDocumentalListNavigator1.ItemDrag -= dragNV_Action;
        }

        private void dragNV_Action(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {
            object DragDropObject = null;
            GISADataset.NivelRow nRow = null;
            this.Cursor = Cursors.WaitCursor;

            if (e.Item == null)
                return;

            if (e.Item is GISATreeNode)
            {
                DragDropObject = (GISATreeNode)e.Item;
                nRow = (GISADataset.NivelRow)(((GISATreeNode)DragDropObject).NivelRow);
                PermissoesHelper.UpdateNivelPermissions(nRow, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
                if (!PermissoesHelper.AllowRead)
                    return;
            }
            else if (e.Item is ListViewItem)
            {
                if (this.nivelDocumentalListNavigator1.SelectedItems.Count > 1)
                {
                    var lst = new List<ListViewItem>();
                    var dick = new Dictionary<long, ListViewItem>();
                    var nivelIDs = new List<long>();
                    var perms = new Dictionary<long, Dictionary<string, byte>>();
                    long tmpID;
                    foreach (ListViewItem lvItem in this.nivelDocumentalListNavigator1.SelectedItems)
                    {
                        tmpID = ((GISADataset.NivelRow)lvItem.Tag).ID;
                        nivelIDs.Add(tmpID);
                        dick[tmpID] = lvItem;
                    }

                    GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                    try
                    {
                        perms = PermissoesRule.Current.CalculateEffectivePermissions(nivelIDs, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID, ho.Connection);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex.ToString());
                        throw;
                    }
                    finally
                    {
                        ho.Dispose();
                    }

                    foreach (long idNivel in perms.Keys)
                    {
                        var nperm = perms[idNivel];
                        if (nperm.ContainsKey("Ler") && nperm["Ler"] == 1)
                            lst.Add(dick[idNivel]);
                    }
                    DragDropObject = lst.ToArray();
                }
                else if (e.Item != null)
                {
                    DragDropObject = (ListViewItem)e.Item;
                    nRow = (GISADataset.NivelRow)(((ListViewItem)DragDropObject).Tag);
                    PermissoesHelper.UpdateNivelPermissions(nRow, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
                    if (!PermissoesHelper.AllowRead)
                        return;
                }
            }

            if (DragDropObject == null)
                return;

            this.Cursor = Cursors.Default;
            Trace.WriteLine("Dragging " + DragDropObject.ToString().GetType().FullName);
            DoDragDrop(DragDropObject, DragDropEffects.Link);
        }

        public void SelectFirstNode()
        {
            if (this.controloNivelListEstrutural1.trVwLocalizacao.Nodes.Count > 0)
                this.controloNivelListEstrutural1.trVwLocalizacao.SelectedNode = this.controloNivelListEstrutural1.trVwLocalizacao.Nodes[0];
        }

        private void SeleccaoActual(out GISADataset.NivelRow NivelRow, out GISADataset.TipoNivelRelacionadoRow TnrRow)
        {
            //Obter selecção actual
            GISATreeNode selectedNode = null;
            GISADataset.NivelRow nRow = null;
            GISADataset.NivelRow nUpperRow = null;
            GISADataset.RelacaoHierarquicaRow rhRow = null;
            GISADataset.TipoNivelRelacionadoRow tnrRow = null;

            // Estas variaveis identificam o contexto definido pelo breadcrumbspath da vista documental
            // (só são usadas quando a vista actual é a documental)
            GISADataset.NivelRow nRowBC = null;
            GISADataset.NivelRow nUpperRowBC = null;
            GISADataset.RelacaoHierarquicaRow rhRowBC = null;
            GISADataset.TipoNivelRelacionadoRow tnrRowBC = null;

            if (this.mPanelToggleState == ToggleState.Estrutural)
            {
                //vista estrutural
                if (mEPFilterMode)
                {
                    var selItem = this.nivelEstruturalList1.SelectedItems.FirstOrDefault();
                    if (selItem != null)
                        nRow = selItem.Tag as GISADataset.NivelRow;
                }
                else
                {
                    selectedNode = (GISATreeNode)this.controloNivelListEstrutural1.SelectedNode;
                    if (selectedNode != null && !(selectedNode.NivelRow.RowState == DataRowState.Detached))
                    {
                        nRow = selectedNode.NivelRow;
                        nUpperRow = selectedNode.NivelUpperRow;
                    }
                }
            }
            else
            {
                //vista documental
                if (!(this.nivelDocumentalListNavigator1.SelectedItems.Count == 0))
                    nRow = (GISADataset.NivelRow)(this.nivelDocumentalListNavigator1.SelectedItems[0].Tag);

                if (nRow != null && !(nRow.RowState == DataRowState.Detached))
                    nUpperRow = nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].NivelRowByNivelRelacaoHierarquicaUpper;
            }

            if (nRow != null && !(nRow.RowState == DataRowState.Detached) && nUpperRow == null)
            {
                if (this.mPanelToggleState == ToggleState.Estrutural && this.mEPFilterMode)
                {
                    rhRow = nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().FirstOrDefault();
                    tnrRow = TipoNivelRelacionado.GetTipoNivelRelacionadoFromRelacaoHierarquica(rhRow);
                }
                else
                    tnrRow = TipoNivelRelacionado.GetTipoNivelRelacionadoFromRelacaoHierarquica(null);
            }
            else if (nUpperRow != null && !(nUpperRow.RowState == DataRowState.Detached) && nRow != null && !(nRow.RowState == DataRowState.Detached))
            {
                if (this.mPanelToggleState == ToggleState.Estrutural)
                {
                    rhRow = selectedNode.RelacaoHierarquicaRow;
                    // excluimos desta forma as relacoes hirarquicas entretanto eliminadas (as que seriam NULL mas cujo nó respectivo teria um NivelUpper)
                    if (selectedNode.NivelUpperRow != null && rhRow != null)
                        tnrRow = TipoNivelRelacionado.GetTipoNivelRelacionadoFromRelacaoHierarquica(rhRow);
                }
                else
                {
                    //Documental
                    if (nRow != null)
                    {
                        DataRow[] rhRows = GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", nRow.ID.ToString(), nUpperRow.ID.ToString()));
                        // A relação pode ter desaparecido por algum motivo (ie, concorrencia).
                        if (rhRows.Length > 0)
                        {
                            rhRow = (GISADataset.RelacaoHierarquicaRow)(rhRows[0]);
                            tnrRow = TipoNivelRelacionado.GetTipoNivelRelacionadoFromRelacaoHierarquica(rhRow);
                        }
                    }
                    if (nRowBC != null && nUpperRowBC != null)
                    {
                        DataRow[] rhRowBCs = GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", nRowBC.ID.ToString(), nUpperRowBC.ID.ToString()));
                        // A relação pode ter desaparecido por algum motivo (ie, concorrencia).
                        if (rhRowBCs.Length > 0)
                        {
                            rhRowBC = (GISADataset.RelacaoHierarquicaRow)(rhRowBCs[0]);
                            tnrRowBC = TipoNivelRelacionado.GetTipoNivelRelacionadoFromRelacaoHierarquica(rhRowBC);
                        }
                    }
                }
            }

            NivelRow = nRow;
            TnrRow = tnrRow;
        }

        public bool isTogglable()
        {
            return isTogglable(null, null);
        }

        public bool isTogglable(GISADataset.NivelRow nivelRow, GISADataset.TipoNivelRelacionadoRow tnrRow)
        {
            if (this.nivelDocumentalListNavigator1.Visible)
                return true;

            if (nivelRow == null && tnrRow == null)
            {
                SeleccaoActual(out nivelRow, out tnrRow);

                if (nivelRow == null || nivelRow.IDTipoNivel == TipoNivel.LOGICO)
                    return false;
            }

            bool result = false;
            // Determinar se existe pelo menos 1 nível documental 
            // pendurado no nível selecionado 
            foreach (GISADataset.RelacaoHierarquicaRow rhRow in GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("IDUpper={0}", nivelRow.ID)))
            {
                if (rhRow.NivelRowByNivelRelacaoHierarquica.TipoNivelRow.IsDocument)
                {
                    result = true;
                    break;
                }
            }

            // No caso de não existirem níveis documentais pendurados no 
            // nível selecionado verificamos se é possível faze-lo. Caso
            // seja podemos da mesma forma passar à vista documental onde 
            // poderemos criar novos níveis.

            if (!result)
            {
                if (TipoNivelRelacionado.GetSubTipoNivelRelacionado(GisaDataSetHelper.GetInstance(), tnrRow).Length == 0)
                    return result;

                foreach (GISADataset.TipoNivelRelacionadoRow subtnrRow in TipoNivelRelacionado.GetSubTipoNivelRelacionado(GisaDataSetHelper.GetInstance(), tnrRow))
                {
                    if (subtnrRow.TipoNivelRow.IsDocument)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public void resetEstrutura()
        {
            this.controloNivelListEstrutural1.LoadContents();
            this.controloNivelListEstrutural1.SelectFirstNode();
        }

        public void ToggleView(bool showDocumental)
        {
            this.nivelDocumentalListNavigator1.Visible = showDocumental;
            this.controloNivelListEstrutural1.Visible = !showDocumental && !mEPFilterMode;
            this.nivelEstruturalList1.Visible = !showDocumental && mEPFilterMode;

            if (!showDocumental)
            {
                if (mEPFilterMode)
                {
                    this.nivelEstruturalList1.Focus();
                    this.nivelEstruturalList1.BringToFront();
                }
                else
                {
                    this.controloNivelListEstrutural1.Focus();
                    this.controloNivelListEstrutural1.BringToFront();
                }
                
                this.PanelToggleState = ToggleState.Estrutural;
                this.nivelDocumentalListNavigator1.ResetNivelNavigator();
            }
            else
            {
                this.nivelDocumentalListNavigator1.Focus();
                this.nivelDocumentalListNavigator1.BringToFront();
                this.PanelToggleState = ToggleState.Documental;
            }
        }

        public void AddNivel(GISADataset.NivelRow nivelRow)
        {
            this.nivelDocumentalListNavigator1.AddNivel(nivelRow);
        }

        public void RefreshTreeViewControlSelectedBranch()
        {
            this.controloNivelListEstrutural1.RefreshTreeViewControlSelectedBranch();
        }

        public ArrayList getNodeRepresentations(string ID, string IDUpper)
        {
            return this.controloNivelListEstrutural1.getNodeRepresentations(ID, IDUpper);
        }

        public void CollapseNodes(ArrayList foundNodes)
        {
            this.controloNivelListEstrutural1.CollapseNodes(foundNodes);
        }

        public void CollapseAllNodes()
        {
            this.controloNivelListEstrutural1.CollapseAllNodes();
        }

        public enum ToggleState : int
        {
            Estrutural = 0,
            Documental = 1
        }        

        protected void FlipToggleState()
        {
            if (PanelToggleState == ToggleState.Estrutural)
                PanelToggleState = ToggleState.Documental;
            else
                PanelToggleState = ToggleState.Estrutural;
        }

        public delegate void ViewToggledEventHandler(ToggleState state);
        public event ViewToggledEventHandler ViewToggled;

        protected ToggleState mPanelToggleState = ToggleState.Estrutural;
        public ToggleState PanelToggleState
        {
            get { return mPanelToggleState; }
            set
            {
                if (mPanelToggleState == value)
                    return;
                mPanelToggleState = value;
                ViewToggled(mPanelToggleState);
            }
        }
    }
}
