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
using GISA.Fedora.FedoraHandler;
using GISA.Controls;
using GISA.Model;
using GISA.SharedResources;

namespace GISA
{
    public partial class PanelObjetoDigitalFedora : GISA.GISAPanel
    {
        public ObjetoDigitalFedoraHelper odHelper;
        private GISADataset.FRDBaseRow CurrentFRDBase;
        private bool IsReadOnlyMode = false;

        public PanelObjetoDigitalFedora()
        {
            InitializeComponent();

            base.ParentChanged += PanelIndiceDocumento_ParentChanged;

            GetExtraResources();

            odHelper = new ObjetoDigitalFedoraHelper();
            this.controlObjetoDigital1.ViewMode = ObjetoDigitalFedoraHelper.Contexto.nenhum;
        }

        private void GetExtraResources()
        {
            btnFullScreen.Image = SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedActionIcons), "ActionFullScreen_enabled_16x16.png");
            trvODsFedora.ImageList = SharedResources.SharedResourcesOld.CurrentSharedResources.FedoraImageList;
        }

        // runs only once. sets tooltip as soon as it's parent appears
        private void PanelIndiceDocumento_ParentChanged(object sender, System.EventArgs e)
        {
            MultiPanel.CurrentToolTip.SetToolTip(this.btnFullScreen, "Mostrar no ecrã todo");
            base.ParentChanged -= PanelIndiceDocumento_ParentChanged;
        }

        public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
        {
            IsLoaded = false;
            CurrentFRDBase = (GISADataset.FRDBaseRow)CurrentDataRow;

            try
            {
                GisaPrincipalPermission gpp = new GisaPrincipalPermission(SessionHelper.GetGisaPrincipal(), "GISA.SlavePanelFedora", GisaPrincipalPermission.READ);
                gpp.Demand();
            }
            catch (System.Security.SecurityException)
            {
                IsReadOnlyMode = true;
            }

            splitContainerOdsReadOnly.Visible = IsReadOnlyMode;
            controlObjetoDigital1.Visible = !IsReadOnlyMode;

            if (IsReadOnlyMode)
            {
                if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsFedoraEnable())
                {
                    var rhRow = CurrentFRDBase.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First();
                    FedoraRule.Current.LoadObjDigitalData(GisaDataSetHelper.GetInstance(), CurrentFRDBase.IDNivel, rhRow.IDTipoNivelRelacionado, conn);
                }
            }
            else
            {
                odHelper.currentNivel = CurrentFRDBase.NivelRow;
                odHelper.LoadData();
            }

            IsLoaded = true;
        }

        public override void ModelToView()
        {
            IsPopulated = false;

            if (IsReadOnlyMode)
            {
                trvODsFedora.Nodes.Clear();
                if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsFedoraEnable())
                {
                    var odRows = FedoraHelper.GetObjetosDigitais(CurrentFRDBase);

                    foreach (var odRow in odRows.OrderBy(r => r.GUIOrder))
                        AddObjetoDigital(odRow);
                }
            }
            else
            {
                odHelper.currentNivel = CurrentFRDBase.NivelRow;
                odHelper.LoadData();

                controlObjetoDigital1.ViewMode = odHelper.mContexto;
                controlObjetoDigital1.CurrentODSimples = odHelper.currentODSimples;
                controlObjetoDigital1.CurrentODComp = odHelper.currentODComp;
                controlObjetoDigital1.docSimplesSemOD = odHelper.docSimplesSemOD;

                switch (odHelper.mContexto)
                {
                    case ObjetoDigitalFedoraHelper.Contexto.objetosDigitais:
                        //this.lblFuncao.Text = "Objetos Digitais";
                        controlObjetoDigital1.Titulo = controlObjetoDigital1.CurrentODComp != null ? controlObjetoDigital1.CurrentODComp.titulo : odHelper.currentNivel.GetNivelDesignadoRows().Single().Designacao;
                        break;
                    case ObjetoDigitalFedoraHelper.Contexto.imagens:
                        //this.lblFuncao.Text = "Objeto Digital Simples";
                        controlObjetoDigital1.Titulo = odHelper.currentODSimples.Count > 0 ? odHelper.currentODSimples[0].titulo : odHelper.currentNivel.GetNivelDesignadoRows().Single().Designacao;
                        break;
                    default: // igual a ObjetoDigitalFedoraHelper.Contexto.objetosDigitais.nenhum
                        //this.lblFuncao.Text = "Objeto(s) Digital(ais) associado(s) não encontrado(s)";
                        break;
                }

                controlObjetoDigital1.ModelToView();
            }

            IsPopulated = true;
        }

        public override void ViewToModel()
        {
            if (CurrentFRDBase == null || CurrentFRDBase.RowState == DataRowState.Detached || !IsLoaded)
                return;

            if (IsReadOnlyMode)
            {

            }
            else
            {
                controlObjetoDigital1.ViewToModel();

                odHelper.currentODSimples = controlObjetoDigital1.CurrentODSimples;
                odHelper.currentODComp = controlObjetoDigital1.CurrentODComp;
                odHelper.docSimplesSemOD = controlObjetoDigital1.docSimplesSemOD;

                odHelper.ViewToModel(controlObjetoDigital1.ViewMode, controlObjetoDigital1.disableSave);
            }
        }

        public override void Deactivate()
        {
            odHelper.Deactivate();
            controlObjetoDigital1.Deactivate();
            ClearPreview();
            trvODsFedora.Nodes.Clear();
            OnHidePanel();
        }

        public Fedora.FedoraHandler.ObjDigComposto GetObjDigitalComp()
        {
            return odHelper.currentODComp;
        }

        public List<Fedora.FedoraHandler.ObjDigSimples> GetObjDigitalSimples()
        {
            return odHelper.currentODSimples;
        }

        #region Fedora read only
        private void AddObjetoDigital(GISADataset.ObjetoDigitalRow odRow)
        {
            var node = new TreeNode();
            node.ImageIndex = 3;
            node.SelectedImageIndex = 3;
            node.Text = odRow.Titulo;
            node.Tag = odRow;
            var odRowsSimples = odRow.GetObjetoDigitalRelacaoHierarquicaRowsByObjetoDigitalObjetoDigitalRelacaoHierarquicaUpper().Select(r => r.ObjetoDigitalRowByObjetoDigitalObjetoDigitalRelacaoHierarquica).ToList();
            if (odRowsSimples.Count > 0)
            {
                foreach (var odRowSimples in odRowsSimples.OrderBy(r => r.GUIOrder))
                {
                    var perm = PermissoesHelper.CalculateEffectivePermissions(odRowSimples, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.TrusteeRow, CurrentFRDBase.NivelRow, PermissoesHelper.ObjDigOpREAD.TipoOperationRow);
                    if (perm == PermissoesHelper.PermissionType.ExplicitDeny || perm == PermissoesHelper.PermissionType.ImplicitDeny) continue;

                    var subDocNode = new TreeNode();
                    subDocNode.Text = odRowSimples.Titulo;
                    subDocNode.Tag = odRowSimples;
                    subDocNode.ImageIndex = 3;
                    subDocNode.SelectedImageIndex = 3;

                    node.Nodes.Add(subDocNode);
                }

                if (node.Nodes.Count == 0) return;

                node.ForeColor = Color.Gray;
                node.Expand();
            }
            else
            {
                var perm = PermissoesHelper.CalculateEffectivePermissions(odRow, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.TrusteeRow, CurrentFRDBase.NivelRow, PermissoesHelper.ObjDigOpREAD.TipoOperationRow);
                if (perm == PermissoesHelper.PermissionType.ExplicitDeny || perm == PermissoesHelper.PermissionType.ImplicitDeny) return;
            }

            trvODsFedora.Nodes.Add(node);
        }

        private void trvODsFedora_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            ClearPreview();
            this.Cursor = Cursors.WaitCursor;
            RefreshDetails(e.Node);
            this.Cursor = Cursors.Default;
        }

        private void RefreshDetails(TreeNode node)
        {
            controlFedoraPdfViewer1.BringToFront();
            if (node != null && node.Nodes.Count == 0) // não mostrar pdf do composto
            {
                var odRow = node.Tag as GISADataset.ObjetoDigitalRow;
                if (FedoraHelper.HasObjDigReadPermission(odRow.pid))
                    this.controlFedoraPdfViewer1.ShowPDF(odRow.pid);
            }
            else
                ClearPreview();
        }

        private void ClearPreview()
        {
            controlFedoraPdfViewer1.Clear();
        }

        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            // Clonar a lista de nós a apresentar em modo full screen
            List<ListViewItem> clonedItemList = new List<ListViewItem>();
            var nodesList = trvODsFedora.Nodes.Count > 1 ? trvODsFedora.Nodes : trvODsFedora.Nodes[0].Nodes;
            if (trvODsFedora.Nodes.Count == 1 && nodesList.Count == 0)
            {
                var item = new ListViewItem(trvODsFedora.Nodes[0].Text) { Tag = new ObjDigSimples() { pid = ((GISADataset.ObjetoDigitalRow)trvODsFedora.Nodes[0].Tag).pid } };
                FormFullScreenPdf ecraCompleto = new FormFullScreenPdf(new List<ListViewItem>() { item }, 0, FedoraHelper.TranslateQualityEnum(controlFedoraPdfViewer1.Qualidade));
                ecraCompleto.ShowDialog();
            }
            else
            {
                clonedItemList.AddRange(nodesList.Cast<TreeNode>().Select(node => new ListViewItem(node.Text) { Tag = new ObjDigSimples() { pid = ((GISADataset.ObjetoDigitalRow)node.Tag).pid } }));

                var selectedItemIndex = trvODsFedora.SelectedNode != null ? trvODsFedora.SelectedNode.Index : -1;

                // Instanciar uma janela modal para mostrar a lista clonada (passamos o identificador do objecto pai, caso exista) 
                FormFullScreenPdf ecraCompleto = new FormFullScreenPdf(clonedItemList, selectedItemIndex, FedoraHelper.TranslateQualityEnum(controlFedoraPdfViewer1.Qualidade));
                ecraCompleto.ShowDialog();
            }
        }

        private void RefreshButtonState()
        {
            btnFullScreen.Enabled = trvODsFedora.Nodes.Count > 0;
        }
        #endregion
    }
}
