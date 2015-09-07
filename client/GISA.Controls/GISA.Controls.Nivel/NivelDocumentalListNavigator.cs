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

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

using GISA.Controls;

namespace GISA.Controls.Nivel
{
    public partial class NivelDocumentalListNavigator : NivelDocumentalList
    {
        public NivelDocumentalListNavigator()
        {
            InitializeComponent();

            this.GrpResultadosLabel = "Descrições encontradas";
            this.showItemsCount = true;

            lstVwPaginated.DeeperLevelSelection += paginatedList_DeeperLevelSelection;

            BreadCrumbsPath1.NewBreadCrumbSelected += NewBreadCrumbSelected_Action;
            BreadCrumbsPath.mImgList = TipoNivelRelacionado.GetImageList();

            if (!SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsReqEnable())
                this.lstVwPaginated.Columns.Remove(this.colRequisitado);

            // Esta chamada é necessária para permitir a inserção dos ícones de ordenação da listview. A inserção 
            // está dependente das colunas associadas na listview, que, na class parent onde este método é chamado, 
            // essas colunas ainda não foram adicionadas á listview
            base.GetExtraResources();
        }

        public void coluna_Requisitado_Visible(bool visible) {
            if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsReqEnable()) {
                if (visible) {
                    if (!this.lstVwPaginated.Columns.Contains(this.colRequisitado)) {
                        this.lstVwPaginated.Columns.Add(this.colRequisitado);
                    }
                }
                else
                    if (this.lstVwPaginated.Columns.Contains(this.colRequisitado)) {
                        this.lstVwPaginated.Columns.Remove(this.colRequisitado);
                    }
            }
        }

        public long ContextBreadCrumbsPathID
        {
            get {return BreadCrumbsPath1.getBreadCrumbsPathContextID;}
        }

        public long ContextBreadCrumbsPathIDUpper
        {
            get {return BreadCrumbsPath1.getBreadCrumbsPathContextIDUpper;}
        }

        #region Filtros
        #region Filtros Requisições / Devoluções

        public bool ExcluirRequisitados = false;
        public long? MovimentoID = null;
        #endregion
        private bool FiltroHideIndirectDocs
        {
            get { return !this.chkHideIndirected.Checked; }
        }
        #endregion

        #region Events overrided
        protected override void CalculateOrderedItems(IDbConnection connection)
        {
            ArrayList ordenacao = this.GetListSortDef();

            NivelRule.Current.CalculateOrderedItemsNav(GisaDataSetHelper.GetInstance(),
                ordenacao,
                this.BreadCrumbsPath1.getBreadCrumbsPathContextID,
                FiltroDesignacaoLike,
                FiltroCodigoParcialLike,
                FiltroIDLike,
                FiltroConteudoLike,
                IDMovimento,
                ExcluirRequisitados,
                FiltroHideIndirectDocs,
                connection);
        }

        protected override void GetItems(int pageNr, int itemsPerPage, IDbConnection connection)
        {
            returnedInfo = new PaginatedLVGetItemsNvDoc(NivelRule.Current.GetItemsNav(GisaDataSetHelper.GetInstance(), pageNr, itemsPerPage, connection));
        }
        #endregion

        protected override void ClearFilter() 
        {
            base.ClearFilter();
            chkHideIndirected.CheckState = CheckState.Checked;
        }

        public event UpdateToolBarButtonsEventHandler UpdateToolBarButtonsEvent;
        public delegate void UpdateToolBarButtonsEventHandler(EventArgs e);
        private void paginatedList_DeeperLevelSelection(object sender, DeeperLevelSelectionEventArgs e)
        {
            // prever a situação onde o utilizador faz duplo click dentro da listview mas sem ser sobre um item dessa lista;
            // nesta situação pretende-se que o contexto seja perdido
            if (e.ItemToBeSelected.ListView == null)
            {
                lstVwPaginated_BeforeNewSelection(sender, new BeforeNewSelectionEventArgs(new ListViewItem(), true));

                return;
            }

            PermissoesHelper.UpdateNivelPermissions((GISADataset.NivelRow)e.ItemToBeSelected.Tag, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
            if (!PermissoesHelper.AllowExpand)
            {
                MessageBox.Show("Não tem permissão para visualizar os sub-níveis do nível documental selecionado.", "Permissões", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.SelectionChange = false;
            }
            else
            {
                if (e.SelectionChange && !(((GISADataset.NivelRow)e.ItemToBeSelected.Tag).RowState == DataRowState.Detached))
                {
                    GISADataset.NivelRow dr = (GISADataset.NivelRow)e.ItemToBeSelected.Tag;

                    // determinar se o nivel actual é um nivel de topo e se está directamente abaixo do produtor de contexto


                    var firstBreadCrumb = BreadCrumbsPath1.Path[0];
                    var rhRow = dr.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First();

                    if (rhRow.NivelRowByNivelRelacaoHierarquica.IDTipoNivel == TipoNivel.DOCUMENTAL && rhRow.NivelRowByNivelRelacaoHierarquicaUpper.IDTipoNivel == TipoNivel.ESTRUTURAL
                        && dr.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().SingleOrDefault(r => r.IDUpper == firstBreadCrumb.idNivel) == null)
                    {
                        // o nivel seleccionado é um nivel de topo e não está directamente abaixo do produtor usado como contexto na vista documental

                        BreadCrumbsPath1.ResetBreadCrumbsPath();

                        var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                        try
                        {
                            NivelRule.Current.LoadNivelLocalizacao(GisaDataSetHelper.GetInstance(), dr.ID, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID, ho.Connection);
                        }
                        catch (Exception ex) { Trace.WriteLine(ex.ToString()); throw; }
                        finally { ho.Dispose(); }

                        // identificar os produtores intermédios até chegar ao nivel documental e acrescentá-los na BreadCrumbsPath (método de pesquisa em profundidade: por cada nível identificam-se os níveis hierarquicamente inferiores e dentro desse conjunto e a partir do primeiro, encontram-se os seus descendentes)
                        rhRow = GisaDataSetHelper.GetInstance().RelacaoHierarquica.Cast<GISADataset.RelacaoHierarquicaRow>().Single(r => r.ID == firstBreadCrumb.idNivel && r.IDUpper == firstBreadCrumb.idUpperNivel);

                        var q = new Stack<GISADataset.RelacaoHierarquicaRow>();
                        rhRow.NivelRowByNivelRelacaoHierarquica.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquicaUpper().Where(r => r.IDTipoNivelRelacionado < 7).ToList().ForEach(r => q.Push(r));

                        var path = new LinkedList<GISADataset.RelacaoHierarquicaRow>();
                        var currentNode = path.AddFirst(rhRow);

                        while (q.Count > 0)
                        {
                            rhRow = q.Pop();

                            // se o caminho seguido não levar ao documento pretendido, deve-se retirar todos os nós até àquele que representa o IDUpper de rhRow
                            while (path.Last.Value.ID != rhRow.IDUpper)
                            {
                                path.RemoveLast();
                                currentNode = path.Last;
                            }

                            if (rhRow.ID != dr.ID)
                            {
                                var rhRows = rhRow.NivelRowByNivelRelacaoHierarquica.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquicaUpper()
                                    .Where(r => r.IDTipoNivelRelacionado < 7 || (r.IDTipoNivelRelacionado >= 7 && r.ID == dr.ID)).ToList();

                                if (rhRows.Count > 0)
                                {
                                    currentNode = path.AddAfter(currentNode, rhRow);
                                    rhRows.ForEach(r => q.Push(r));
                                }
                            }
                            else
                            {
                                path.AddAfter(currentNode, rhRow);
                                break;
                            }
                        }

                        path.ToList().ForEach(rh =>
                        {
                            var imgIndex = SharedResourcesOld.CurrentSharedResources.NivelImageBase(System.Convert.ToInt32(rh.TipoNivelRelacionadoRow.GUIOrder));
                            BreadCrumbsPath1.AddBreadCrumb(GISA.Model.Nivel.GetDesignacao(rh.NivelRowByNivelRelacaoHierarquica), rh.ID, rh.IDUpper, imgIndex);
                        });
                    }
                    else
                    {
                        // acrescentar o nivel documental na BreadCrumbsPath
                        int imageIndex = 0;
                        imageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageBase(System.Convert.ToInt32(dr.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].TipoNivelRelacionadoRow.GUIOrder));
                        BreadCrumbsPath1.AddBreadCrumb(GISA.Model.Nivel.GetDesignacao(dr), dr.ID, BreadCrumbsPath1.getBreadCrumbsPathContextID, imageIndex);
                    }

                    ClearFilter();
                    ReloadList();

                    // Quando não existem elementos a apresentar na lista deve-se forçar uma mudança de contexto para garantir que os paineis de descrição são gravados
                    if (this.Items.Count == 0)
                        lstVwPaginated_BeforeNewSelection(sender, new BeforeNewSelectionEventArgs(new ListViewItem(), true));
                    else if (this.Items.Count > 1)
                    {
                        lstVwPaginated_BeforeNewSelection(sender, new BeforeNewSelectionEventArgs(new ListViewItem(), true));
                        PermissoesHelper.UpdateNivelPermissions((GISADataset.NivelRow)e.ItemToBeSelected.Tag, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
                        if (UpdateToolBarButtonsEvent != null)
                            UpdateToolBarButtonsEvent(new EventArgs());
                    }

                    e.SelectionChange = true;
                }
                else
                    e.SelectionChange = false;
            }
        }

        private void NewBreadCrumbSelected_Action(object sender, BreadCrumbsPath.NewBreadCrumbSelectedEventArgs e)
        {
            GISADataset.NivelRow nvlRow = null;
            if (e.mIdNivelLVContext > 0)
            {
                if (GisaDataSetHelper.GetInstance().Nivel.Select(string.Format("ID={0}", e.mIdNivelLVContext)).Length > 0)
                    nvlRow = (GISADataset.NivelRow)(GisaDataSetHelper.GetInstance().Nivel.Select(string.Format("ID={0}", e.mIdNivelLVContext))[0]);
            }
            ClearFilter();

            // Verificar se o contexto na breadcrumbspath ainda existe em memória (pode ter sido apagado 
            // concorrentemente por outro utilizador)
            if (GisaDataSetHelper.GetInstance().Nivel.Select(string.Format("ID={0}", e.mIdNivelBCContext)).Length > 0)
                ReloadList(nvlRow);
            else
            {
                ClearItemSelection(null);
                ResetList();
            }
        }

        protected override void ApplyFilter()
        {
            // Verificar se o contexto na breadcrumbspath ainda existe em memória (pode ter sido apagado 
            // concorrentemente por outro utilizador)
            if (GisaDataSetHelper.GetInstance().Nivel.Select(string.Format("ID={0}", BreadCrumbsPath1.getBreadCrumbsPathContextID)).Length > 0)
            {
                long click = 0;
                click = DateTime.Now.Ticks;
                ClearItemSelection();
                ReloadList();
                Debug.WriteLine("<<Filtrar niveis documentais>> " + new TimeSpan(DateTime.Now.Ticks - click).ToString());
            }
            else
                lstVwPaginated_BeforeNewSelection(this.btnAplicar, new BeforeNewSelectionEventArgs(new ListViewItem(), true));
        }

        public void ResetNivelNavigator()
        {
            BreadCrumbsPath1.ResetBreadCrumbsPath();
            ResetList();
        }

        public override void InitialLoad(GISADataset.RelacaoHierarquicaRow rhRow)
        {
            this.txtFiltroDesignacao.Text = string.Empty;
            var nivelRow = rhRow.NivelRowByNivelRelacaoHierarquica;
            int imageIndex = -1;
            if (nivelRow.CatCode.Trim() == "CA")
                imageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageControloAut(System.Convert.ToInt32(rhRow.TipoNivelRelacionadoRow.GUIOrder));
            else
                imageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageBase(System.Convert.ToInt32(rhRow.TipoNivelRelacionadoRow.GUIOrder));

            BreadCrumbsPath1.AddBreadCrumb(GISA.Model.Nivel.GetDesignacao(nivelRow), rhRow.ID, rhRow.IDUpper, imageIndex);
            LoadListData();
        }

        public override ArrayList GetCodigoCompletoCaminhoUnico(ListViewItem item)
        {
            GISADataset.RelacaoHierarquicaRow rhCurrentRow = null;
            ArrayList result = new ArrayList();

            foreach (BreadCrumbsPath.BreadCrumb bc in BreadCrumbsPath1.Path)
            {
                if (!(BreadCrumbsPath1.Path[0] == bc))
                {
                    // No caso de faltar uma parte do código returnar um arraylist vazio
                    if (GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", bc.idNivel, bc.idUpperNivel)).Length > 0)
                    {
                        rhCurrentRow = (GISADataset.RelacaoHierarquicaRow)(GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", bc.idNivel, bc.idUpperNivel))[0]);
                        result.Add(rhCurrentRow);
                    }
                    else
                        return new ArrayList();
                }
            }

            GISADataset.NivelRow nRow = (GISADataset.NivelRow)(item.Tag);

            if (nRow.RowState != DataRowState.Detached)
            {
                if (GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", nRow.ID, BreadCrumbsPath1.getBreadCrumbsPathContextID)).Length > 0)
                {
                    rhCurrentRow = (GISADataset.RelacaoHierarquicaRow)(GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", nRow.ID, BreadCrumbsPath1.getBreadCrumbsPathContextID))[0]);
                    result.Add(rhCurrentRow);
                }
            }

            return result;
        }

        protected override ListViewItem GetNewNivelItem(NivelRule.NivelDocumentalListItem item)
        {            
            ListViewItem lvItem = base.GetNewNivelItem(item);
            if (this.lstVwPaginated.Columns.Contains(this.colRequisitado))
                lvItem.SubItems[this.colRequisitado.Index].Text = item.Requisitado;
            if (this.lstVwPaginated.Columns.Contains(this.colAgrupador))
                lvItem.SubItems[this.colAgrupador.Index].Text = item.Agrupador;

            return lvItem;
        }

        internal void ClearFiltro()
        {
            ClearFilter();
        }
    }
}
