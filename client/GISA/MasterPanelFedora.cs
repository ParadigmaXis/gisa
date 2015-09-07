using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Controls.Localizacao;
using GISA.Controls.Nivel;
using GISA.Fedora.FedoraHandler;
using GISA.GUIHelper;
using GISA.Model;
using GISA.SharedResources;

namespace GISA
{
    public partial class MasterPanelFedora : GISA.MasterPanelNiveis, INivelNavigatorProvider
    {
        private bool actualizaEstrutura = false;
        private ObjDigital objetoDigital = null;
        
        public MasterPanelFedora()
        {
            InitializeComponent();

            rhTable.RelacaoHierarquicaRowChanged += rhTable_RelacaoHierarquicaRowChangingRelacaoHierarquicaRowDeleting;
            rhTable.RelacaoHierarquicaRowDeleting += rhTable_RelacaoHierarquicaRowChangingRelacaoHierarquicaRowDeleting;
            base.StackChanged += MasterPanelSeries_StackChanged;

            this.lblFuncao.Text = "Objetos digitais fedora";

            ShowToolBarButtons();
            GetExtraResources();
        }

        private void ShowToolBarButtons()
        {
            this.ToolBarButtonCreateAny.Visible = true;
            this.ToolBarButtonEdit.Visible = true;
            this.ToolBarButtonRemove.Visible = true;
            this.ToolBarButtonSep2.Visible = true;
        }

        private void GetExtraResources()
        {
            ToolBar.ImageList = SharedResourcesOld.CurrentSharedResources.NVLManipulacaoImageList;
            ToolBarButtonCreateAny.ImageIndex = 1;
            ToolBarButtonEdit.ImageIndex = 2;
            ToolBarButtonRemove.ImageIndex = 3;

            string[] strs = SharedResourcesOld.CurrentSharedResources.NVLManipulacaoStrings;
            ToolBarButtonCreateAny.ToolTipText = strs[1];
            ToolBarButtonEdit.ToolTipText = strs[2];
            ToolBarButtonRemove.ToolTipText = strs[3];
        }

        public void coluna_Requisitado_Visible(bool visible)
        {
            this.nivelNavigator1.coluna_Requisitado_Visible(visible);
        }

        #region Selection change events
        // selecionar nivel estrutural
        protected override void beforeNewSelection_Action(ControloNivelListEstrutural.BeforeNewSelectionEventArgs e)
        {
            if (e.node == null)
            {
                GISADataset.NivelRow nRow = null;
                UpdateContext(nRow);
            }
            else
                UpdateContext(e.node.NivelRow);
        }

        // selecionar nivel documental
        protected override void NivelDocumentalListNavigator1_BeforeNewListSelection(object sender, GISA.Controls.BeforeNewSelectionEventArgs e)
        {
            var nRow = default(GISADataset.NivelRow);
            if (e.ItemToBeSelected != null)
                nRow = e.ItemToBeSelected.Tag as GISADataset.NivelRow;
            if (nRow != null && this.nivelNavigator1.PanelToggleState != GISA.Controls.Nivel.NivelNavigator.ToggleState.Estrutural)
                PermissoesHelper.LoadObjDigitalPermissions(nRow, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.TrusteeRow);

            base.NivelDocumentalListNavigator1_BeforeNewListSelection(sender, e);

            objetoDigital = null;
            if (e.SelectionChange)
                objetoDigital = CurrentContext.ObjetoDigital;
        }
        #endregion

        #region UpdateContext

        // Actualiza o contexto de acordo com o nível especificado
        protected override bool UpdateContext(GISADataset.NivelRow row)
        {
            bool result = CurrentContext.SetFedoraNivel(row);
            UpdateToolBarButtons();
            return result;
        }
        #endregion

        private void EditNivel()
        {
            var nRow = this.nivelNavigator1.SelectedNivel;
            var frdRow = nRow.GetFRDBaseRows().Single();
            var rhRow = nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First();
            var tnrRow = TipoNivelRelacionado.GetTipoNivelRelacionadoFromRelacaoHierarquica(rhRow);

            var frm = new FormNivelDocumentalFedora();
            frm.IDTipoNivelRelacionado = tnrRow.ID;

            string WindowTitle = string.Format("Editar {0}", tnrRow.Designacao);
            

            frm.Text = WindowTitle;
            frm.txtCodigo.Text = nRow.Codigo;
            frm.txtDesignacao.Text = Nivel.GetDesignacao(nRow);
            frm.FRDBaseRow = nRow.GetFRDBaseRows().Single();
            frm.LoadData();

            var idxFRDCARow = frdRow.GetIndexFRDCARows().SingleOrDefault(r => r["Selector"] != DBNull.Value && r.Selector == -1);

            // show form and receive user feedback
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Trace.WriteLine("A editar nível...");
                GISADataset.NivelDesignadoRow ndRow = null;
                nRow.Codigo = frm.txtCodigo.Text;


                // Um Nivel documental deve ter obrigatoriamente um NivelDesignado.
                Debug.Assert(nRow.GetNivelDesignadoRows().Length > 0);
                ndRow = nRow.GetNivelDesignadoRows()[0];
                ndRow.Designacao = frm.txtDesignacao.Text;

                var tipologiaSelected = ((FormNivelDocumentalFedora)frm).Tipologia;

                if (tipologiaSelected == null && idxFRDCARow != null)
                    idxFRDCARow.Delete();
                else if (tipologiaSelected != null && idxFRDCARow == null)
                    idxFRDCARow = GisaDataSetHelper.GetInstance().IndexFRDCA.AddIndexFRDCARow(frdRow, ((FormNivelDocumentalFedora)frm).Tipologia.ControloAutRow, -1, new byte[] { }, 0);
                else if (tipologiaSelected != null && idxFRDCARow != null && idxFRDCARow.IDControloAut != tipologiaSelected.IDControloAut)
                {
                    idxFRDCARow.Delete();
                    idxFRDCARow = GisaDataSetHelper.GetInstance().IndexFRDCA.AddIndexFRDCARow(frdRow, ((FormNivelDocumentalFedora)frm).Tipologia.ControloAutRow, -1, new byte[] { }, 0);
                }

                string termoToCompare = frm.Tipologia == null ? null : ((FormNivelDocumentalFedora)frm).Tipologia.DicionarioRow.Termo;
                if (objetoDigital != null && objetoDigital.tipologia != termoToCompare)
                {
                    objetoDigital.tipologia = tipologiaSelected != null ? tipologiaSelected.DicionarioRow.Termo : "";
                    objetoDigital.state = State.modified;
                }

                // registar a edição do item selecionado
                CurrentContext.RaiseRegisterModificationEvent(nRow.GetFRDBaseRows()[0]);

                PersistencyHelper.EnsureUniqueCodigoNivelPreConcArguments pcArgs = new PersistencyHelper.EnsureUniqueCodigoNivelPreConcArguments();
                pcArgs.nRowID = nRow.ID;
                pcArgs.ndRowID = ndRow.ID;
                // Se se tratar de uma entidade detentora não passar os Ids de uma relação
                // hierárquica para um nível superior pois não existe nenhum.
                if (nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().Length > 0)
                {
                    pcArgs.rhRowID = nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].ID;
                    pcArgs.rhRowIDUpper = nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].IDUpper;
                }
                pcArgs.testOnlyWithinNivel = true;

                // actualizar objecto digital caso exista
                var preTransactionAction = new PreTransactionAction();
                var args = new PersistencyHelper.FedoraIngestPreTransactionArguments();
                preTransactionAction.args = args;

                preTransactionAction.preTransactionDelegate = delegate(PersistencyHelper.PreTransactionArguments preTransactionArgs)
                {
                    string msg = null;
                    if (objetoDigital != null)
                        preTransactionArgs.cancelAction = !SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.Ingest(objetoDigital, out msg);
                    preTransactionArgs.message = msg;
                };

                PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save(DelegatesHelper.ensureUniqueCodigo, pcArgs, preTransactionAction);
                if (!pcArgs.successful)
                    MessageBox.Show(pcArgs.message, "Edição da unidade de descrição", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (successfulSave == PersistencyHelper.SaveResult.successful)
                {
                    GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                    try
                    {
                        List<string> IDNiveis = new List<string>();
                        IDNiveis.Add(nRow.ID.ToString());
                        GISA.Search.Updater.updateNivelDocumental(IDNiveis);
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
                }

                PersistencyHelper.cleanDeletedData();

                // Actualizar a interface com os novos valores. Se editarmos a 
                // raiz (estrutural) da vista documental é necessário actualizar 
                // automaticamente também a vista estrutural.

                if (!(nRow.RowState == DataRowState.Detached))
                {
                    if (this.nivelNavigator1.PanelToggleState == NivelNavigator.ToggleState.Estrutural)
                        this.nivelNavigator1.UpdateSelectedNodeName(Nivel.GetDesignacao(nRow));
                    else
                        this.nivelNavigator1.UpdateSelectedListItemName(Nivel.GetDesignacao(nRow));
                }

                // Forçar a gravação do documento
                CurrentContext.SetNivelEstrututalDocumental(null);
                CurrentContext.SetNivelEstrututalDocumental(nRow);
            }
        }

        private void RemoveNivel()
        {
            GISADataset.NivelRow nUpperRow = null;
            GISADataset.RelacaoHierarquicaRow rhRow = null;
            GISADataset.NivelRow nRow = null;
            

            nRow = this.nivelNavigator1.SelectedNivel;
            // Verificar se a relacção hierárquica ainda é a mesma apresentada na interface (se o
            // utilizador estiver a ver a lista que contem o nível a apagar e entretanto outro utilizador
            // o ter colocado noutro ponto da árvore, a relação hierárquica presente em memória deixa
            // de corresponder com aquela que é apresentada na interface quando esse nível é selecionado;
            // quando o nível é selecionado a informação no DataSet de trabalho é actualizado mas não
            // actualiza a interface)
            if (GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", nRow.ID, this.nivelNavigator1.ContextBreadCrumbsPathID)).Length > 0)
                rhRow = (GISADataset.RelacaoHierarquicaRow)(GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", nRow.ID, this.nivelNavigator1.ContextBreadCrumbsPathID))[0]);
            else
            {
                MessageBox.Show("Esta operação não pode ser concluída pelo facto de a localização na estrutura " + System.Environment.NewLine + "do nível selecionado ter sido alterada por outro utilizador.", "Eliminar Nível", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            nUpperRow = (GISADataset.NivelRow)(GisaDataSetHelper.GetInstance().Nivel.Select(string.Format("ID={0}", this.nivelNavigator1.ContextBreadCrumbsPathID))[0]);

            var assocODs = FedoraHelper.GetAssociatedODsDetailsMsg(nRow.ID);
            if (assocODs.Length > 0)
            {
                FormDeletionReport form = new FormDeletionReport();
                form.Text = "Eliminação de unidade de informação";
                form.Interrogacao = "A unidade de informação selecionada tem objeto(s) digital(ais) associado(s). " + System.Environment.NewLine +
                        "Se eliminar esta unidade de informação, os objeto(s) digital(ais) " + System.Environment.NewLine + " também serão eliminado(s)." + System.Environment.NewLine +
                        "Pretende continuar?";
                form.Detalhes = assocODs;

                if (form.ShowDialog() == DialogResult.Cancel) return;
            }
            else if (MessageBox.Show("Tem a certeza que deseja eliminar o nível selecionado?", "Eliminação de relação", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;

            if (!FedoraHelper.CanDeleteODsAssociated2UI(nRow, out objetoDigital))
                return;

            Trace.WriteLine("A apagar nível...");

            if (TipoNivel.isNivelDocumental(nRow) && TipoNivel.isNivelOrganico(nUpperRow))
            {
                // Verificar que existem várias relações hierárquicas deste 
                // nível documental a entidades produtoras superiores. Nesse 
                // caso deverá ser removida a relação, caso contrário, se não 
                // existirem subníveis documentais, será eliminado o próprio 
                // nível(documental)
                int numRHs = nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().Length;
                if (numRHs > 1)
                {
                    if (MessageBox.Show(
                        "Por favor tenha em atenção que são vários os produtores deste " + System.Environment.NewLine +
                        "nível documental. O nível documental propriamente dito não " + System.Environment.NewLine +
                        "será eliminado, apenas a sua relação ao nível orgânico " + System.Environment.NewLine +
                        "superior o será.", "Eliminação de relação", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                        return;

                    ((frmMain)TopLevelControl).EnterWaitMode();

                    CurrentContext.RaiseRegisterModificationEvent(nRow.GetFRDBaseRows()[0]);

                    PersistencyHelper.canDeleteRHRowPreConcArguments args = new PersistencyHelper.canDeleteRHRowPreConcArguments();
                    args.nRowID = nRow.ID;
                    args.nUpperRowID = nUpperRow.ID;
                    args.rhRowID = rhRow.ID;
                    args.rhRowIDUpper = rhRow.IDUpper;
                    PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save(DelegatesHelper.verifyIfCanDeleteRH, args);
                    PersistencyHelper.cleanDeletedData(PersistencyHelper.determinaNuvem("RelacaoHierarquica"));
                    if (args.deleteSuccessful)
                    {
                        if (successfulSave == PersistencyHelper.SaveResult.successful)
                        {
                            List<string> IDNiveis = new List<string>();
                            IDNiveis.Add(args.nRowID.ToString());
                            GISA.Search.Updater.updateNivelDocumental(IDNiveis);
                            GISA.Search.Updater.updateNivelDocumentalComProdutores(nRow.ID);
                        }
                        this.nivelNavigator1.RemoveSelectedLVItem();
                    }
                    else
                        MessageBox.Show(args.message, "Eliminação de relação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ((frmMain)TopLevelControl).LeaveWaitMode();
                }
                else if (numRHs == 1)
                {
                    // Verificar que não existem subníveis documentais
                    int numSubRHs = GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("IDUpper={0}", nRow.ID)).Length;
                    if (numSubRHs > 0)
                    {
                        MessageBox.Show("Só é possível eliminar os níveis que não tenham outros directamente associados", "Eliminação de relação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        if (MessageBox.Show(
                            "Por favor tenha em atenção que este nível documental é produzido" + System.Environment.NewLine +
                            "por apenas uma entidade. Ao remover esta relação será perdida " + System.Environment.NewLine +
                            "não só a relação como o nível documental propriamente dito.", "Eliminação de relação", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                            return;

                        ((frmMain)TopLevelControl).EnterWaitMode();

                        CurrentContext.RaiseRegisterModificationEvent(nRow.GetFRDBaseRows()[0]);

                        PersistencyHelper.canDeleteRHRowPreConcArguments argsPca = new PersistencyHelper.canDeleteRHRowPreConcArguments();
                        argsPca.nRowID = nRow.ID;
                        argsPca.nUpperRowID = nUpperRow.ID;
                        argsPca.rhRowID = 0;
                        argsPca.rhRowIDUpper = 0;
                        PersistencyHelper.DeleteIDXPreSaveArguments argsPsa = new PersistencyHelper.DeleteIDXPreSaveArguments();
                        argsPsa.ID = nRow.ID;

                        // actualizar objecto digital caso exista
                        var preTransactionAction = new PreTransactionAction();
                        var args = new PersistencyHelper.FedoraIngestPreTransactionArguments();
                        preTransactionAction.args = args;

                        preTransactionAction.preTransactionDelegate = delegate(PersistencyHelper.PreTransactionArguments preTransactionArgs)
                        {
                            bool ingestSuccess = true;
                            string msg = null;

                            var odsToIngest = FedoraHelper.DeleteObjDigital(nRow);
                            odsToIngest.ForEach(od => ingestSuccess &= SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.Ingest(od, out msg));

                            preTransactionArgs.cancelAction = !ingestSuccess;
                            preTransactionArgs.message = msg;
                        };

                        PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save(DelegatesHelper.verifyIfCanDeleteRH, argsPca, Nivel.DeleteNivelXInDataBase, argsPsa, preTransactionAction);
                        if (argsPca.deleteSuccessful)
                        {
                            if (successfulSave == PersistencyHelper.SaveResult.successful)
                            {
                                List<string> IDNiveis = new List<string>();
                                IDNiveis.Add(nRow.ID.ToString());
                                GISA.Search.Updater.updateNivelDocumental(IDNiveis);
                                GISA.Search.Updater.updateNivelDocumentalComProdutores(nRow.ID);
                            }
                            this.nivelNavigator1.RemoveSelectedLVItem();
                        }
                        else
                        {
                            // se o nível a eliminar se tratar de uma série ou documento solto mas que 
                            // por motivos de conflito de concorrência não foi possível executar, 
                            // o refrescamento dos botões é feito tendo como o contexto o próprio
                            // nível que se pretendeu eliminar para desta forma o estado dos mesmos
                            // estar correcta (caso contrário o estado dos botões referir-se-ia a 
                            // não haver qualquer item selecionado
                            MessageBox.Show(argsPca.message, "Eliminação de relação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            UpdateToolBarButtons(this.nivelNavigator1.SelectedItems[0]);
                        }
                        PersistencyHelper.cleanDeletedData(new List<TableDepthOrdered.TableCloudType>(new TableDepthOrdered.TableCloudType[] { PersistencyHelper.determinaNuvem("RelacaoHierarquica"), PersistencyHelper.determinaNuvem("FRDBase"), PersistencyHelper.determinaNuvem("ObjetoDigital") }));

                        ((frmMain)TopLevelControl).LeaveWaitMode();
                    }
                }
                else
                    Debug.Assert(false, "Should never happen. There must be a relation with an upper Nivel.");
            }
            else
            {
                // Entre todos os outros tipos de nível proceder normalmente
                if ((nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].IDTipoNivelRelacionado == TipoNivelRelacionado.D ||
                    nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].IDTipoNivelRelacionado == TipoNivelRelacionado.SD) &&
                    NiveisHelper.NivelFoiMovimentado(nRow.ID))
                {
                    if (MessageBox.Show(
                            "Por favor tenha em atenção que este nível documental já foi " + System.Environment.NewLine +
                            "requisitado/devolvido. Ao remover nível documental serão perdidos " + System.Environment.NewLine +
                            "todos os seus registos referentes a requisições e devoluções.", "Eliminação de nível documental", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                        return;
                }

                ((frmMain)TopLevelControl).EnterWaitMode();

                CurrentContext.RaiseRegisterModificationEvent(nRow.GetFRDBaseRows()[0]);

                PersistencyHelper.canDeleteRHRowPreConcArguments argsPca = new PersistencyHelper.canDeleteRHRowPreConcArguments();
                argsPca.nRowID = nRow.ID;
                argsPca.nUpperRowID = nUpperRow.ID;
                argsPca.rhRowID = 0;
                argsPca.rhRowIDUpper = 0;
                PersistencyHelper.DeleteIDXPreSaveArguments argsPsa = new PersistencyHelper.DeleteIDXPreSaveArguments();
                argsPsa.ID = nRow.ID;

                // actualizar objecto digital caso exista
                var preTransactionAction = new PreTransactionAction();
                var args = new PersistencyHelper.FedoraIngestPreTransactionArguments();
                preTransactionAction.args = args;

                preTransactionAction.preTransactionDelegate = delegate(PersistencyHelper.PreTransactionArguments preTransactionArgs)
                {
                    bool ingestSuccess = true;
                    string msg = null;

                    var odsToIngest = FedoraHelper.DeleteObjDigital(nRow);
                    odsToIngest.ForEach(od => ingestSuccess &= SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.Ingest(od, out msg));

                    preTransactionArgs.cancelAction = !ingestSuccess;
                    preTransactionArgs.message = msg;
                };

                PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save(DelegatesHelper.verifyIfCanDeleteRH, argsPca, Nivel.DeleteNivelXInDataBase, argsPsa, preTransactionAction);
                PersistencyHelper.cleanDeletedData(new List<TableDepthOrdered.TableCloudType>(new TableDepthOrdered.TableCloudType[] { PersistencyHelper.determinaNuvem("RelacaoHierarquica"), PersistencyHelper.determinaNuvem("FRDBase") }));
                if (argsPca.deleteSuccessful)
                {
                    if (successfulSave == PersistencyHelper.SaveResult.successful)
                    {
                        List<string> IDNiveis = new List<string>();
                        IDNiveis.Add(argsPsa.ID.ToString());
                        GISA.Search.Updater.updateNivelDocumental(IDNiveis);
                        if (nRow.RowState == DataRowState.Detached)
                            GISA.Search.Updater.updateNivelDocumentalComProdutores(argsPsa.ID);
                        else
                            GISA.Search.Updater.updateNivelDocumentalComProdutores(nRow.ID);

                        this.nivelNavigator1.RemoveSelectedLVItem();
                    }
                }
                else
                    MessageBox.Show(argsPca.message, "Eliminação de relação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ((frmMain)TopLevelControl).LeaveWaitMode();
            }
            
            UpdateToolBarButtons();
        }

        protected override void ToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e) //Handles ToolBar.ButtonClick
        {
            base.ToolBar_ButtonClick(sender, e);

            if (e.Button == ToolBarButtonCreateAny)
            {
                if (e.Button.DropDownMenu != null && e.Button.DropDownMenu is ContextMenu)
                    ((ContextMenu)e.Button.DropDownMenu).Show(ToolBar, new System.Drawing.Point(e.Button.Rectangle.X, e.Button.Rectangle.Y + e.Button.Rectangle.Height));
            }
            else if (e.Button == ToolBarButtonEdit)
                EditNivel();
            else if (e.Button == ToolBarButtonRemove)
                RemoveNivel();
        }        

        public override void UpdateToolBarButtons(ListViewItem item)
        {
            base.UpdateToolBarButtons(item);

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

            if (this.nivelNavigator1.PanelToggleState == NivelNavigator.ToggleState.Estrutural)
            {
                //vista estrutural
                if (this.nivelNavigator1.EPFilterMode) // modo filtro
                {
                    if (item != null && item.ListView != null && !(((GISADataset.NivelRow)item.Tag).RowState == DataRowState.Detached))
                        //contexto da listview
                        nRow = (GISADataset.NivelRow)item.Tag;
                }
                else //modo árvore
                {
                    //vista estrutural
                    selectedNode = (GISATreeNode)this.nivelNavigator1.SelectedNode;
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
                if (item != null && item.ListView != null && !(((GISADataset.NivelRow)item.Tag).RowState == DataRowState.Detached))
                {
                    //contexto da listview
                    nRow = (GISADataset.NivelRow)item.Tag;
                    nUpperRow = nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].NivelRowByNivelRelacaoHierarquicaUpper;
                }

                //contexto do breadcrumbspath
                nRowBC = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().SingleOrDefault(r => r.RowState != DataRowState.Deleted && r.ID == this.nivelNavigator1.ContextBreadCrumbsPathID);
                nUpperRowBC = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().SingleOrDefault(r => r.RowState != DataRowState.Deleted && r.ID == this.nivelNavigator1.ContextBreadCrumbsPathIDUpper);
            }

            if (nRow != null && !(nRow.RowState == DataRowState.Detached) && nUpperRow == null)
                tnrRow = TipoNivelRelacionado.GetTipoNivelRelacionadoFromRelacaoHierarquica(null);
            else if (nUpperRow != null && !(nUpperRow.RowState == DataRowState.Detached) && nRow != null && !(nRow.RowState == DataRowState.Detached))
            {
                if (this.nivelNavigator1.PanelToggleState == NivelNavigator.ToggleState.Estrutural)
                {
                    rhRow = selectedNode.RelacaoHierarquicaRow;
                    // excluimos desta forma as relacoes hirarquicas entretanto eliminadas (as que seriam NULL mas cujo nó respectivo teria um NivelUpper)
                    if (selectedNode.NivelUpperRow != null && rhRow != null)
                        tnrRow = TipoNivelRelacionado.GetTipoNivelRelacionadoFromRelacaoHierarquica(rhRow);
                }
                else
                {
                    if (item != null && item.ListView != null)
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

            if (nRow == null && this.nivelNavigator1.PanelToggleState == NivelNavigator.ToggleState.Documental && nRowBC != null && nUpperRowBC != null)
            {
                GISADataset.RelacaoHierarquicaRow[] bcRHRow = (GISADataset.RelacaoHierarquicaRow[])(GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", nRowBC.ID.ToString(), nUpperRowBC.ID.ToString())));
                if (bcRHRow.Length > 0)
                {
                    if (bcRHRow[0].IDTipoNivelRelacionado < TipoNivelRelacionado.SD)
                        ToolBarButtonCreateAny.Enabled = AllowCreate && PermissoesHelper.AllowCreate;
                    else
                        ToolBarButtonCreateAny.Enabled = false;
                    rhRowBC = (GISADataset.RelacaoHierarquicaRow)(GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", nRowBC.ID.ToString(), nUpperRowBC.ID.ToString()))[0]);
                    tnrRowBC = TipoNivelRelacionado.GetTipoNivelRelacionadoFromRelacaoHierarquica(rhRowBC);
                    ConfigureContextMenu(nRowBC, nUpperRowBC, tnrRowBC, tnrRowBC);
                    ToolBarButtonEdit.Enabled = false;
                    ToolBarButtonRemove.Enabled = false;
                }
                else
                {
                    ToolBarButtonCreateAny.Enabled = false;
                    ToolBarButtonPaste.Enabled = false;
                }
            }
            else if (TipoNivel.isNivelDocumental(nRow))
            {
                ToolBarButtonCreateAny.Enabled = AllowCreate && PermissoesHelper.AllowCreate;
                ConfigureContextMenu(nRowBC, nUpperRowBC, tnrRow, tnrRowBC);
                if (rhRow.IDTipoNivelRelacionado == TipoNivelRelacionado.D)
                {
                    ToolBarButtonEdit.Enabled = AllowEdit && PermissoesHelper.AllowEdit;
                    ToolBarButtonRemove.Enabled = NiveisHelper.isRemovable(nRow, nUpperRow, false) && AllowDelete && PermissoesHelper.AllowDelete;
                }
                else if (rhRow.IDTipoNivelRelacionado == TipoNivelRelacionado.SD)
                {
                    ToolBarButtonEdit.Enabled = AllowEdit && PermissoesHelper.AllowEdit;
                    ToolBarButtonRemove.Enabled = NiveisHelper.isRemovable(nRow, nUpperRow, false) && AllowDelete && PermissoesHelper.AllowDelete && PermissoesHelper.ObjDigAllowWrite;
                }
                else
                {
                    ToolBarButtonEdit.Enabled = false;
                    ToolBarButtonRemove.Enabled = false;
                }
            }
            else
            {
                ToolBarButtonCreateAny.Enabled = false;
                ToolBarButtonEdit.Enabled = false;
                ToolBarButtonRemove.Enabled = false;
            }
        }

        private void ConfigureContextMenu(GISADataset.NivelRow NivelRow, GISADataset.NivelRow NivelUpperRow, GISADataset.TipoNivelRelacionadoRow tnrRow, GISADataset.TipoNivelRelacionadoRow tnrRowBC)
        {
            ContextMenu CurrentMenu = new ContextMenu();
            ToolBarButtonCreateAny.DropDownMenu = CurrentMenu;

            // force an update to the button's icon
            int i = ToolBarButtonCreateAny.ImageIndex;
            ToolBarButtonCreateAny.ImageIndex = -1;
            ImageList toolbarImageList = ToolBar.ImageList;
            ImageList niveisImageList = TipoNivelRelacionado.GetImageList();
            toolbarImageList.Images[2] = niveisImageList.Images[SharedResourcesOld.CurrentSharedResources.NivelImageEditar(System.Convert.ToInt32(tnrRow.GUIOrder))];
            toolbarImageList.Images[3] = niveisImageList.Images[SharedResourcesOld.CurrentSharedResources.NivelImageEliminar(System.Convert.ToInt32(tnrRow.GUIOrder))];
            ToolBarButtonCreateAny.ImageIndex = i;
            if (tnrRowBC == null)
                TipoNivelRelacionado.ConfigureMenu(GisaDataSetHelper.GetInstance(), tnrRow, ref ToolBarButtonCreateAny, ToolBarButtonCreateMenuItemClick); // IsDocumentView)
            else
                TipoNivelRelacionado.ConfigureMenu(GisaDataSetHelper.GetInstance(), tnrRowBC, ref ToolBarButtonCreateAny, ToolBarButtonCreateMenuItemClick); // IsDocumentView)
        }

        private void ToolBarButtonCreateMenuItemClick(object sender, EventArgs e)
        {
            ((frmMain)TopLevelControl).EnterWaitMode();

            GISADataset.TipoNivelRelacionadoRow tnrRow = ((TipoNivelMenuItem)sender).Row;
            var nivelRow = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().Single(r => r.ID == this.nivelNavigator1.ContextBreadCrumbsPathID);

            var frm = new FormNivelDocumentalFedora();
            frm.IDTipoNivelRelacionado = tnrRow.ID; // necessário para validação do código parcial

            bool successfulSave = handleNewNivel(frm, nivelRow, tnrRow);            

            ((frmMain)TopLevelControl).LeaveWaitMode();

            // O UpdateToolBarButtons não é executado quando é adicionado um novo nível documental pois 
            // esta operação já é feita durante a inserção do nó (caso essa inserção não aconteça por 
            // motivos de conflito de concorrência o método pode ser executado de forma a impedir o 
            // acesso de opções ao utilizador que poderiam levar a um crash da aplicação). Executar o 
            // UpdateToolBarButtons neste ponto para a situação acima mencionada vai alterar 
            // erradamente o estado dos botões
            if ((this.nivelNavigator1.PanelToggleState == NivelNavigator.ToggleState.Estrutural && successfulSave) || (this.nivelNavigator1.PanelToggleState == NivelNavigator.ToggleState.Documental && !successfulSave))
            {
                if (this.nivelNavigator1.SelectedItems.Count > 0)
                    UpdateToolBarButtons(this.nivelNavigator1.SelectedItems[0]);
                else
                    UpdateToolBarButtons();
            }
        }

        // Trata a criação de novos níveis e respectivas relações. Caso se trate 
        // de um nível orgânico (estrutural e que esteja associado a uma EP) o 
        // nível correspondente deverá já existir e não será por isso criado, 
        // será criada apenas a relação.
        private bool handleNewNivel(Form frm, GISADataset.NivelRow parentNivelRow, GISADataset.TipoNivelRelacionadoRow tnrRow)
        {
            frm.Text = "Criar " + tnrRow.Designacao;

            bool successfulSave = true;
            switch (frm.ShowDialog())
            {
                case DialogResult.OK:

                    Trace.WriteLine("A criar nível...");

                    long click = DateTime.Now.Ticks;
                    string designacaoUFAssociada = string.Empty;
                    PostSaveAction postSaveAction = null;
                    var nRow = GisaDataSetHelper.GetInstance().Nivel.AddNivelRow(tnrRow.TipoNivelRow, ((FormAddNivel)frm).txtCodigo.Text.Trim(), "NVL", new byte[] { }, 0);
                    var ndRow = GisaDataSetHelper.GetInstance().NivelDesignado.AddNivelDesignadoRow(nRow, ((FormAddNivel)frm).txtDesignacao.Text.Trim(), new byte[] { }, 0);
                    var frdRow = GisaDataSetHelper.GetInstance().FRDBase.AddFRDBaseRow(nRow, (GISADataset.TipoFRDBaseRow)(GisaDataSetHelper.GetInstance().TipoFRDBase.Select("ID=" + DomainValuesHelper.stringifyEnumValue(TipoFRDBase.FRDOIRecolha))[0]), "", "", new byte[] { }, 0);
                    var rhRow = GisaDataSetHelper.GetInstance().RelacaoHierarquica.AddRelacaoHierarquicaRow(nRow, parentNivelRow, tnrRow, null, null, null, null, null, null, null, new byte[] { }, 0);

                    //valores por omissão
                    var globalConfig = GisaDataSetHelper.GetInstance().GlobalConfig.Cast<GISADataset.GlobalConfigRow>().Single();
                    if (globalConfig.ApplyDefaultValues)
                    {
                        var sfrdcaRow = GisaDataSetHelper.GetInstance().SFRDCondicaoDeAcesso
                            .AddSFRDCondicaoDeAcessoRow(frdRow, "", globalConfig.IsCondicaoDeAcessoNull() ? "" : globalConfig.CondicaoDeAcesso, 
                            globalConfig.IsCondicaoDeReproducaoNull() ? "" : globalConfig.CondicaoDeReproducao, "", new byte[] {}, 0);

                        foreach (GISADataset.ConfigLinguaRow r in globalConfig.GetConfigLinguaRows())
                            GisaDataSetHelper.GetInstance().SFRDLingua.AddSFRDLinguaRow(sfrdcaRow, r.Iso639Row, new byte[] { }, 0);

                        foreach (GISADataset.ConfigAlfabetoRow r in globalConfig.GetConfigAlfabetoRows())
                            GisaDataSetHelper.GetInstance().SFRDAlfabeto.AddSFRDAlfabetoRow(sfrdcaRow, r.Iso15924Row, new byte[] { }, 0);
                    }

                    var selectedTipologia = ((FormNivelDocumentalFedora)frm).Tipologia;
                    if (selectedTipologia != null)
                        GisaDataSetHelper.GetInstance().IndexFRDCA.AddIndexFRDCARow(frdRow, selectedTipologia.ControloAutRow, -1, new byte[] { }, 0);
                    
                    // Só adicionar permissões ao grupo TODOS dos níveis lógicos e a níveis documentais imediatamente
                    // abaixo de níveis orgânicos (Documentos soltos e séries); caso se se trate de um nível estrutural 
                    // controlado, as permissões já foram atribuidas aquando da criação do controlo de autoridade 
                    if (nRow.IDTipoNivel == TipoNivel.DOCUMENTAL && parentNivelRow.IDTipoNivel == TipoNivel.ESTRUTURAL)
                    {
                        var nUpperRow = rhRow == null ? default(GISADataset.NivelRow) : rhRow.NivelRowByNivelRelacaoHierarquicaUpper;
                        PermissoesHelper.AddNewNivelGrantPermissions(nRow, nUpperRow);
                    }

                    postSaveAction = new PostSaveAction();
                    var args = new PersistencyHelper.GenericPostSaveArguments();
                    postSaveAction.args = args;
                    postSaveAction.postSaveDelegate = delegate(PersistencyHelper.PostSaveArguments postSaveArgs)
                    {
                        if (!postSaveArgs.cancelAction && nRow != null && nRow.RowState != DataRowState.Detached && nRow.RowState != DataRowState.Deleted)
                        {
                            // registar a criação do nivel documental
                            GISADataset.FRDBaseRow frdDocRow = null;
                            GISADataset.FRDBaseRow[] frdDocRows = nRow.GetFRDBaseRows();
                            if (frdDocRows.Length > 0)
                                frdDocRow = frdDocRows[0];
                            CurrentContext.RaiseRegisterModificationEvent(frdDocRow);

                            PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao,
                                GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao.Cast<GISADataset.FRDBaseDataDeDescricaoRow>().Where(frd => frd.RowState == DataRowState.Added).ToArray(), postSaveArgs.tran);
                        }
                    };

                    // se se tratar de um (sub)documento é necessário garantir que se trata de um código 
                    // único dentro da sua série (se constituir série) ou nivel estrutural superior
                    PersistencyHelper.ValidateNivelAddAndAssocNewUFPreConcArguments pcArgs = new PersistencyHelper.ValidateNivelAddAndAssocNewUFPreConcArguments();
                    PersistencyHelper.SetNewCodigosPreSaveArguments psArgs = new PersistencyHelper.SetNewCodigosPreSaveArguments();
                    PersistencyHelper.EnsureUniqueCodigoNivelPreConcArguments pcArgsNivel = new PersistencyHelper.EnsureUniqueCodigoNivelPreConcArguments();
                    
                    pcArgs.argsNivel = pcArgsNivel;

                    // dados que serão usados no delegate responsável pela criação do nível documental
                    pcArgsNivel.nRowID = nRow.ID;
                    pcArgsNivel.ndRowID = ndRow.ID;
                    pcArgsNivel.rhRowID = rhRow.ID;
                    pcArgsNivel.rhRowIDUpper = rhRow.IDUpper;
                    pcArgsNivel.frdBaseID = frdRow.ID;
                    pcArgsNivel.testOnlyWithinNivel = true;

                    // permitir ao delegate selecionar o delegate correspondente ao tipo de nível que se está a criar
                    pcArgs.IDTipoNivelRelacionado = tnrRow.ID;

                    psArgs.createNewNivelCodigo = false;
                    psArgs.createNewUFCodigo = false;
                    psArgs.setNewCodigo = rhRow.IDTipoNivelRelacionado == TipoNivelRelacionado.SD;
                    psArgs.argsNivelDocSimples = NiveisHelper.AddNivelDocumentoSimplesWithDelegateArgs(nRow.GetNivelDesignadoRows().Single(), rhRow.IDUpper, rhRow.IDTipoNivelRelacionado);

                    PersistencyHelper.save(DelegatesHelper.ValidateNivelAddAndAssocNewUF, pcArgs, DelegatesHelper.SetNewCodigos, psArgs, postSaveAction);
                    if (!pcArgsNivel.successful)
                    {
                        successfulSave = false;
                        MessageBox.Show(pcArgsNivel.message, "Criação de unidade de descrição", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (parentNivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].TipoNivelRelacionadoRow.ID == TipoNivelRelacionado.SR)
                    {
                        GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                        try
                        {
                            DBAbstractDataLayer.DataAccessRules.FRDRule.Current.LoadSFRDAvaliacaoData(GisaDataSetHelper.GetInstance(), parentNivelRow.ID, ho.Connection);
                        }
                        finally
                        {
                            ho.Dispose();
                        }
                    }

                    PersistencyHelper.cleanDeletedData(new List<TableDepthOrdered.TableCloudType>(new TableDepthOrdered.TableCloudType[] { PersistencyHelper.determinaNuvem("RelacaoHierarquica"), PersistencyHelper.determinaNuvem("FRDBase") }));

                    if (!successfulSave)
                        return successfulSave;

                    GISA.Search.Updater.updateNivelDocumentalComProdutores(nRow.ID);
                    GISA.Search.Updater.updateNivelDocumental(nRow.ID);

                    this.nivelNavigator1.AddNivel(nRow);

                    Debug.WriteLine("<<A criar nível...>> " + new TimeSpan(DateTime.Now.Ticks - click).ToString());
                    break;
                case DialogResult.Cancel:
                    successfulSave = false;
                    break;
            }

            return successfulSave;
        }

        private void MasterPanelSeries_StackChanged(frmMain.StackOperation stackOperation, bool isSupport)
        {
            switch (stackOperation)
            {
                case frmMain.StackOperation.Push:
                    this.nivelNavigator1.MultiSelect = false;
                    this.nivelNavigator1.IsParentSupport = isSupport;
                    if (actualizaEstrutura)
                    {
                        resetEstrutura();
                        actualizaEstrutura = false;
                    }

                    if (this.nivelNavigator1.PanelToggleState == NivelNavigator.ToggleState.Documental && this.nivelNavigator1.ContextBreadCrumbsPathID > 0)
                        this.nivelNavigator1.ReloadList();
                    break;

                case frmMain.StackOperation.Pop:
                    
                    break;
            }
        }

        #region  Adição e remoção de nós das treeviews
        private GISADataset.RelacaoHierarquicaDataTable rhTable = GisaDataSetHelper.GetInstance().RelacaoHierarquica;
        private void rhTable_RelacaoHierarquicaRowChangingRelacaoHierarquicaRowDeleting(object sender, GISADataset.RelacaoHierarquicaRowChangeEvent e)
        {
            NavigatorHelper.ForceRefresh(e, this, (frmMain)TopLevelControl);
        }

        public NivelNavigator NivelNavigator
        {
            get { return this.nivelNavigator1; }
        }

        #endregion

        
    }
}
