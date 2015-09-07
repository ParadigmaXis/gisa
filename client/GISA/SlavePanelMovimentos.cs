using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using GISA.Controls;
using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

namespace GISA
{
    public partial class SlavePanelMovimentos : GISA.SinglePanel
    {
        public SlavePanelMovimentos() : base()
        {
            InitializeComponent();

            GetExtraResources();

            this.grpFiltro.Visible = ToolBarButtonFiltro.Pushed;
            lblFuncao.Text = "Documentos associados";
            ToolBarButtonFiltro.Visible = true;
            ToolBarButtonAuxList.Visible = true;

            UpdateListButtonsState();

            long[] @params = new long[] { TipoNivel.DOCUMENTAL };
            DragDropHandlerNiveis = new NivelDragDrop(lstVwNiveisAssoc, @params);

            DragDropHandlerNiveis.AcceptItem += AcceptItem;
        }

        public static Bitmap FunctionImage
        {
            get { return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "PermissoesPorClassificacao_enabled_32x32.png"); }
        }

        private void GetExtraResources()
        {
            btnRemove.Image = SharedResourcesOld.CurrentSharedResources.Apagar;
        }

        protected internal GISADataset.MovimentoRow CurrentMovimento;
        List<MovimentoRule.DocumentoMovimentado> documentos = null;
        private NivelDragDrop DragDropHandlerNiveis;
        private ArrayList filter = new ArrayList();

        public override void LoadData()
        {
            if (CurrentContext.Movimento == null)
            {
                CurrentMovimento = null;
                return;
            }

            CurrentMovimento = CurrentContext.Movimento;

            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetTempConnection());
            try
            {
                documentos = MovimentoRule.Current.GetDocumentos(CurrentMovimento.ID, GisaDataSetHelper.GetInstance(), ho.Connection);
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
        }

        public override void ModelToView()
        {
            if (CurrentMovimento.IsNotasNull())
                txtNotas.Text = "";
            else
                txtNotas.Text = CurrentMovimento.Notas;

            RepopulateDocumentosAssociados();
            OnShowPanel();
        }

        private void RepopulateDocumentosAssociados()
        {
            lstVwNiveisAssoc.BeginUpdate();
            lstVwNiveisAssoc.Items.Clear();
            List<ListViewItem> items = new List<ListViewItem>();
            ListViewItem item;
            foreach (MovimentoRule.DocumentoMovimentado dm in documentos)
            {
                item = DmToItem(dm);                
                items.Add(item);
            }

            if (items.Count > 0)
                lstVwNiveisAssoc.Items.AddRange(items.ToArray());

            lstVwNiveisAssoc.EndUpdate();
        }

        private ListViewItem DmToItem(MovimentoRule.DocumentoMovimentado dm) {
            //ListViewItem item = new ListViewItem(dm.Designacao);
            ListViewItem item = new ListViewItem(dm.IDNivel.ToString());

            item.Tag = GisaDataSetHelper.GetInstance().DocumentosMovimentados.Select(string.Format("IDMovimento={0} AND IDNivel={1}", CurrentMovimento.ID, dm.IDNivel))[0];

            //item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dm.IDNivel.ToString()));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dm.Designacao));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dm.CodigoCompleto));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dm.NivelDescricao));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, GISA.Utils.GUIHelper.FormatDateInterval(dm.AnoInicio, dm.MesInicio, dm.DiaInicio, dm.AnoFim, dm.MesFim, dm.DiaFim)));

            return item;
        }

        public override bool ViewToModel()
        {
            CurrentMovimento.Notas = txtNotas.Text;
            return true;
        }

        public override void Deactivate()
        {
            CurrentMovimento = null;
            GUIHelper.GUIHelper.clearField(lstVwNiveisAssoc);
            OnHidePanel();
        }

        private void ToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == ToolBarButtonAuxList)
                ToggleNiveisSupportPanel(ToolBarButtonAuxList.Pushed);
            else if (e.Button == ToolBarButtonFiltro)
                this.grpFiltro.Visible = ToolBarButtonFiltro.Pushed;
        }

        protected internal virtual void ToggleNiveisSupportPanel(bool showIt)
        {
            
        }

        private void AcceptItem(ListViewItem item)
        {            
            GISADataset.NivelRow nRow = (GISADataset.NivelRow)(((ListViewItem)item).Tag);

            LoadNivelDocumental(nRow.ID);

            // validar a associação: só se pode associar documentos e subdocumentos
            if (!(nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].IDTipoNivelRelacionado == TipoNivelRelacionado.D ||
                nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].IDTipoNivelRelacionado == TipoNivelRelacionado.SD))
            {
                MessageBox.Show("Só é permitido associar documentos e subdocumentos.", "Requisição/Devolução de documentos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GISADataset.DocumentosMovimentadosRow[] docMovRows = (GISADataset.DocumentosMovimentadosRow[])
                       (GisaDataSetHelper.GetInstance().DocumentosMovimentados.Select(string.Format("IDMovimento={0} AND IDNivel={1}", CurrentMovimento.ID, nRow.ID)));

            // aceitar o drop apenas se se tratar de um Nivel ainda não associado
            if (docMovRows.Length == 0)
            {
                MovimentoRule.DocumentoMovimentado dm = new MovimentoRule.DocumentoMovimentado();                

                GISADataset.DocumentosMovimentadosRow[] docMovDelRows = (GISADataset.DocumentosMovimentadosRow[])
                       (GisaDataSetHelper.GetInstance().DocumentosMovimentados.Select(string.Format("IDMovimento={0} AND IDNivel={1}", CurrentMovimento.ID, nRow.ID), "", DataViewRowState.Deleted));

                string nCod = GetCodigoCompleto(item);

                if (docMovDelRows.Length > 0)
                {
                    if (docMovDelRows[0].NivelRow.GetFRDBaseRows().Length > 0 &&
                        docMovDelRows[0].NivelRow.GetFRDBaseRows()[0].GetSFRDDatasProducaoRows().Length > 0)
                    {
                        dm.AnoFim = GisaDataSetHelper.GetDBNullableText(docMovDelRows[0].NivelRow.GetFRDBaseRows()[0].GetSFRDDatasProducaoRows()[0], "FimAno");
                        dm.MesFim = GisaDataSetHelper.GetDBNullableText(docMovDelRows[0].NivelRow.GetFRDBaseRows()[0].GetSFRDDatasProducaoRows()[0], "FimMes");
                        dm.DiaFim = GisaDataSetHelper.GetDBNullableText(docMovDelRows[0].NivelRow.GetFRDBaseRows()[0].GetSFRDDatasProducaoRows()[0], "FimDia");

                        dm.AnoInicio = GisaDataSetHelper.GetDBNullableText(docMovDelRows[0].NivelRow.GetFRDBaseRows()[0].GetSFRDDatasProducaoRows()[0], "InicioAno");
                        dm.MesInicio = GisaDataSetHelper.GetDBNullableText(docMovDelRows[0].NivelRow.GetFRDBaseRows()[0].GetSFRDDatasProducaoRows()[0], "InicioMes");
                        dm.DiaInicio = GisaDataSetHelper.GetDBNullableText(docMovDelRows[0].NivelRow.GetFRDBaseRows()[0].GetSFRDDatasProducaoRows()[0], "FimDia");
                    }

                    dm.CodigoCompleto = nCod;
                    dm.NivelDescricao = docMovDelRows[0].NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].TipoNivelRelacionadoRow.Designacao;
                    dm.IDNivel = docMovDelRows[0].NivelRow.ID;
                    dm.Designacao = docMovDelRows[0].NivelRow.GetNivelDesignadoRows()[0].Designacao;

                    docMovDelRows[0].RejectChanges();                                        
                }
                else
                {
                    GISADataset.DocumentosMovimentadosRow newDocMovRow =
                        GisaDataSetHelper.GetInstance().DocumentosMovimentados.NewDocumentosMovimentadosRow();

                    newDocMovRow.NivelRow = nRow;
                    newDocMovRow.MovimentoRow = CurrentMovimento;
                    newDocMovRow.Versao = new byte[] { };

                    GisaDataSetHelper.GetInstance().DocumentosMovimentados.AddDocumentosMovimentadosRow(newDocMovRow);

                    if (newDocMovRow.NivelRow.GetFRDBaseRows().Length > 0 &&
                        newDocMovRow.NivelRow.GetFRDBaseRows()[0].GetSFRDDatasProducaoRows().Length > 0)
                    {
                        dm.AnoFim = GisaDataSetHelper.GetDBNullableText(newDocMovRow.NivelRow.GetFRDBaseRows()[0].GetSFRDDatasProducaoRows()[0], "FimAno");
                        dm.MesFim = GisaDataSetHelper.GetDBNullableText(newDocMovRow.NivelRow.GetFRDBaseRows()[0].GetSFRDDatasProducaoRows()[0], "FimMes");
                        dm.DiaFim = GisaDataSetHelper.GetDBNullableText(newDocMovRow.NivelRow.GetFRDBaseRows()[0].GetSFRDDatasProducaoRows()[0], "FimDia");

                        dm.AnoInicio = GisaDataSetHelper.GetDBNullableText(newDocMovRow.NivelRow.GetFRDBaseRows()[0].GetSFRDDatasProducaoRows()[0], "InicioAno");
                        dm.MesInicio = GisaDataSetHelper.GetDBNullableText(newDocMovRow.NivelRow.GetFRDBaseRows()[0].GetSFRDDatasProducaoRows()[0], "InicioMes");
                        dm.DiaInicio = GisaDataSetHelper.GetDBNullableText(newDocMovRow.NivelRow.GetFRDBaseRows()[0].GetSFRDDatasProducaoRows()[0], "FimDia");
                    }

                    dm.CodigoCompleto = nCod;
                    dm.NivelDescricao = newDocMovRow.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].TipoNivelRelacionadoRow.Designacao;
                    dm.IDNivel = newDocMovRow.NivelRow.ID;                    
                    dm.Designacao = newDocMovRow.NivelRow.GetNivelDesignadoRows()[0].Designacao;
                }

                ListViewItem newItem = this.DmToItem(dm);
                this.lstVwNiveisAssoc.Items.Insert(0, newItem);
                newItem.EnsureVisible();                
            }
        }

        private void LoadNivelDocumental(long nRowID)
        {
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                DBAbstractDataLayer.DataAccessRules.NivelRule.Current.LoadNivelDocumental(GisaDataSetHelper.GetInstance(), nRowID, ho.Connection);
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
        }

        private string GetCodigoCompleto(object element)
        {
            string nCod = string.Empty;
            ListViewItem item = (ListViewItem)element;

            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                nCod = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.GetCodigoCompletoNivel(((GISADataset.NivelRow)item.Tag).ID, ho.Connection);                
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

            return nCod;
        }

        public void btnRemove_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        private void lstVwNiveisAssoc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == Convert.ToInt32(Keys.Delete))
                DeleteItem();
        }

        private void DeleteItem()
        {
            ListViewItem item = this.lstVwNiveisAssoc.SelectedItems[0];
            GISADataset.DocumentosMovimentadosRow dmRow = (GISADataset.DocumentosMovimentadosRow)item.Tag;

            var args = new PersistencyHelper.ValidateMovimentoDeleteItemPreConcArguments();
            args.IDMovimento = dmRow.IDMovimento;
            args.IDNivel = dmRow.IDNivel;
            args.CatCode = dmRow.MovimentoRow.CatCode.Equals("REQ") ? "DEV" : "REQ";

            dmRow.Delete();

            PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save(ValidateMovimentoDelete, args);
            PersistencyHelper.cleanDeletedData();

            if (!args.continueSave)
            {
                var message = dmRow.MovimentoRow.CatCode.Equals("REQ") 
                    ? "Não é permitido eliminar a associação de documentos a requisições com devolução posterior" 
                    : "Não é permitido eliminar a associação de documentos a devoluções com requisições posteriores mas sem devolução";
                MessageBox.Show(message, "Eliminar a associação de documento com a requisição/devolução", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                
            this.lstVwNiveisAssoc.Items.Remove(item);
            UpdateListButtonsState();
        }

        public static void ValidateMovimentoDelete(PersistencyHelper.PreConcArguments args)
        {
            var vmdiPsa = args as PersistencyHelper.ValidateMovimentoDeleteItemPreConcArguments;
            var dmRow = GisaDataSetHelper.GetInstance().DocumentosMovimentados.Cast<GISADataset.DocumentosMovimentadosRow>()
                .SingleOrDefault(r => r.RowState == DataRowState.Deleted && (long)r["IDNivel", DataRowVersion.Original] == vmdiPsa.IDNivel && (long)r["IDMovimento", DataRowVersion.Original] == vmdiPsa.IDMovimento);

            // este caso acontece quando a linha está detached (o documento foi adicionado, e antes de ser gravado, foi removido da requisição/devolução)
            if (dmRow == null) return;

            // não é permitido eliminar a associação de documentos a devoluções com requisições posteriores mas sem devolução
            // não é permitido eliminar a associação de documentos a requisições com devolução posterior
            vmdiPsa.continueSave = !DBAbstractDataLayer.DataAccessRules.MovimentoRule.Current.temMovimentosPosteriores(vmdiPsa.IDNivel, vmdiPsa.IDMovimento, vmdiPsa.CatCode, args.tran);

            if (vmdiPsa.continueSave) return;

            System.Data.DataSet tempgisaBackup2 = vmdiPsa.gisaBackup;
            PersistencyHelper.BackupRow(ref tempgisaBackup2, dmRow);
            vmdiPsa.gisaBackup = tempgisaBackup2;
            dmRow.RejectChanges();

        }

        public void OnShowPanel()
        {
            //Show the button that brings up the support panel
            //and select it by default.
            ToolBar.ButtonClick += ToolBar_ButtonClick;

            ToolBarButtonAuxList.Visible = true;
            ToolBarButtonFiltro.Visible = true;
            if (CurrentMovimento != null)
                ToggleNiveisSupportPanel(false);
        }

        public void OnHidePanel()
        {
            // if seguinte serve exclusivamente para debug
            if (CurrentMovimento != null && CurrentMovimento.RowState == DataRowState.Detached)
            {
                Debug.WriteLine("OCORREU SITUAÇÃO DE ERRO NO PAINEL UFS ASSOCIADAS. EM PRINCIPIO NINGUEM DEU POR ELE.");
            }

            ToggleNiveisSupportPanel(false);
            //Deactivate Toolbar Buttons
            ToolBar.ButtonClick -= ToolBar_ButtonClick;
            ToolBarButtonAuxList.Visible = false;
            ToolBarButtonFiltro.Visible = false;
        }

        private void lstVwNiveisAssoc_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateListButtonsState();
        }

        private void UpdateListButtonsState()
        {
            if (lstVwNiveisAssoc.SelectedItems.Count == 0)
                btnRemove.Enabled = false;
            else
                btnRemove.Enabled = true;
        }

        // Filtro
        public void ClearFilter()
        {
            //txtFiltroDesignacao.Text = "";
        }

        public string TextFilterDesignacao
        {
            get
            {
                return (txtFiltroDesignacao.Text == null || txtFiltroDesignacao.Text.Length == 0) ? string.Empty : txtFiltroDesignacao.Text;
            }
        }

        void txtFiltroDesignacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ExecuteFilter();
        }

        private void ExecuteFilter()
        {
            PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save();
            PersistencyHelper.cleanDeletedData();
            if (successfulSave == PersistencyHelper.SaveResult.unsuccessful)
                return;
            
            if (TextFilterDesignacao == string.Empty)
            {
                filter.Clear();
                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try
                {
                    documentos = MovimentoRule.Current.GetDocumentos(CurrentMovimento.ID, GisaDataSetHelper.GetInstance(), ho.Connection);
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
            }
            else
            {
                filter.Clear();
                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try
                {
                    documentos = MovimentoRule.Current.GetDocumentos(CurrentMovimento.ID, TextFilterDesignacao, GisaDataSetHelper.GetInstance(), ho.Connection);
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
            }

            RepopulateDocumentosAssociados();
        }

        protected override bool isInnerContextValid()
        {
            return CurrentMovimento != null;
        }

        protected override bool isOuterContextValid()
        {
            return CurrentContext.Movimento != null;
        }

        protected override bool isOuterContextDeleted()
        {
            Debug.Assert(CurrentContext.Movimento != null, "CurrentContext.Requisicao Is Nothing");
            return CurrentContext.Movimento.RowState == DataRowState.Detached;
        }

        protected override void addContextChangeHandlers()
        {
            CurrentContext.MovimentoChanged += this.Recontextualize;
        }

        protected override void removeContextChangeHandlers()
        {
            CurrentContext.MovimentoChanged -= this.Recontextualize;
        }

        protected override PanelMensagem GetDeletedContextMessage()
        {
            PanelMensagem1.LblMensagem.Text = "Esta requisição/devolução foi eliminada não sendo, por isso, possível apresentar a sua informação.";
            return PanelMensagem1;
        }

        protected override PanelMensagem GetNoContextMessage()
        {
            PanelMensagem1.LblMensagem.Text = "Para visualizar os documentos associados deverá selecionar uma requisição/devolução no painel superior.";
            return PanelMensagem1;
        }

        protected override PanelMensagem GetNoReadPermissionMessage()
        {
            PanelMensagem1.LblMensagem.Text = "Não tem permissão para visualizar os detalhes da requisição/devolução selecionada no painel superior.";
            return PanelMensagem1;
        }

        private void txtBox_KeyDown(object Sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ExecuteFilter();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            ExecuteFilter();
        }        
    }
}