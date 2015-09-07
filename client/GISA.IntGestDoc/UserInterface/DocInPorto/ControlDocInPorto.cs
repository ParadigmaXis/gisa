using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Model;
using GISA.IntGestDoc.Controllers;
using GISA.IntGestDoc.Model;
using GISA.IntGestDoc.Model.EntidadesExternas;
using GISA.IntGestDoc.Model.EntidadesInternas;
using GISA.Controls.ControloAut;

namespace GISA.IntGestDoc.UserInterface.DocInPorto
{
    public partial class ControlDocInPorto : UserControl
    {
        private static Font fontBold = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
        private static Font fontNormal = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
        //private ToolStripControlHost dtTScomponent;

        public ControlDocInPorto()
        {
            InitializeComponent();

            //dtTScomponent = new ToolStripControlHost(dateTimePicker1);
            //toolStrip1.Items.Add(dtTScomponent);

            //this.dateTimePicker1.MaxDate = DateTime.Now;

            this.controlDocumentoGisa1.SuggestionEdited += new ControlDocumentoGisa.SuggestionEditedEventHandler(controlDocumentoGisa1_SuggestionEdited);
            this.controlDocumentoGisaAnexo1.SuggestionEdited += new ControlDocumentoGisaAnexo.SuggestionEditedEventHandler(controlDocumentoGisa1_SuggestionEdited);
            this.controlDocumentoGisaProcesso1.SuggestionEdited += new ControlDocumentoGisaProcesso.SuggestionEditedEventHandler(controlDocumentoGisa1_SuggestionEdited);

            this.splitContainerDetails.Resize += new System.EventHandler(this.splitContainerDetails_Resize);
            repositionSplitter();

            this.lvRelations.ItemHeight = 20;
            
            dipWS = new DocInPortoWS();
        }

        void controlDocumentoGisa1_SuggestionEdited(object sender)
        {   
            var item = this.lvRelations.SelectedNode;
            var correspondencia = item.Tag as CorrespondenciaDocs;

            if (!item.Checked || (!correspondencia.Edited && item.Checked))
            {
                correspondencia.Edited = item.Checked = true;
                item.NodeFont = fontBold;
                item.Text = item.Text.Insert(0, "* ");
            }
        }

        private DateTime lastTimeStamp = DateTime.MinValue;
        private InternalEntities internalEntitiesCreated;
        private DocInPortoWS dipWS;
        private List<DocumentoExterno> documentos = new List<DocumentoExterno>();
        public void InitializeData()
        {
            this.internalEntitiesCreated = new InternalEntities();
            this.controlDocumentoGisa1.InternalEntitiesLst = this.internalEntitiesCreated;
            this.controlDocumentoGisaAnexo1.InternalEntitiesLst = this.internalEntitiesCreated;
            this.controlDocumentoGisaProcesso1.InternalEntitiesLst = this.internalEntitiesCreated;

            this.lastTimeStamp = LoadLastTimeStamp();

            var maxDocs = ((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Rows[0])).MaxNumDocumentos;
            documentos = dipWS.GetDocumentos(this.lastTimeStamp, maxDocs);
            BuildSuggestions();
            UpdateToolbarButtons();
        }

        public void BuildSuggestions()
        {
            var sugestoes = new List<CorrespondenciaDocs>();
            if (documentos == null) documentos = new List<DocumentoExterno>();
            try
            {
                sugestoes = SuggestionsFactory.GetSuggestions(documentos);
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro inesperado.", "Integração", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sugestoes = dipWS.FilterPreviousIncorporations(sugestoes);
            this.LoadData(sugestoes);
        }

        private DateTime LoadLastTimeStamp()
        {
            var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                DBAbstractDataLayer.DataAccessRules.IntGestDocRule.Current.LoadInteg_Config(GisaDataSetHelper.GetInstance(), ho.Connection);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
                throw;
            }
            finally
            {
                ho.Dispose();
            }

            var ts = GisaDataSetHelper.GetInstance().Integ_Config.Cast<GISADataset.Integ_ConfigRow>().Select(r => r.LastTimeStamp).SingleOrDefault();

            if (ts == null)
                return DateTime.MinValue;
            else
                return ts;
        }

        // Load only documents
        Dictionary<string, TreeNode> procs = new Dictionary<string, TreeNode>();
        Dictionary<string, TreeNode> nodesDct = new Dictionary<string, TreeNode>();
        public void LoadData(List<CorrespondenciaDocs> sugestoes)
        {
            this.lvRelations.AfterCheck -= new TreeViewEventHandler(lvRelations_AfterCheck);
            this.lvRelations.Nodes.Clear();
            
            nodesDct = new Dictionary<string, TreeNode>();
            procs = new Dictionary<string, TreeNode>();
            TreeNode node = null;
            var nud = "";
            this.lvRelations.SuspendLayout();
            this.lvRelations.BeginUpdate();

            foreach (Correspondencia c in sugestoes.OrderBy(c => ((DocumentoExterno)(c.EntidadeExterna)).OrderNr))
            {
                if (c.EntidadeExterna.Tipo == TipoEntidadeExterna.DocumentoComposto)
                {
                    node = ((DocumentoComposto)c.EntidadeExterna).Tipologia != null ? 
                        new TreeNode(c.EntidadeExterna.IDExterno + ", " + ((DocumentoComposto)c.EntidadeExterna).Tipologia.Titulo) :
                        new TreeNode(c.EntidadeExterna.IDExterno);
                    procs.Add(c.EntidadeExterna.IDExterno, node);
                }
                else
                {
                    if (c.EntidadeExterna.Tipo == TipoEntidadeExterna.Documento)
                    {
                        nud = ((DocumentoSimples)c.EntidadeExterna).Processo.IDExterno;
                        node = new TreeNode(c.EntidadeExterna.IDExterno + ", " + ((DocumentoSimples)c.EntidadeExterna).Tipologia.Titulo);
                    }
                    else if (c.EntidadeExterna.Tipo == TipoEntidadeExterna.DocumentoAnexo)
                    {
                        nud = ((DocumentoAnexo)c.EntidadeExterna).Processo.IDExterno;
                        node = new TreeNode(c.EntidadeExterna.IDExterno + ", " + ((DocumentoAnexo)c.EntidadeExterna).TipoDescricao);
                    }
                    else
                    {
                        Trace.Assert(false, "Tipo não tratado: " + c.EntidadeExterna.GetType().ToString());
                        continue;
                    }

                    if (!procs.ContainsKey(nud)) // processo já foi integrado... é preciso inclui-lo na lista mas só para contextualizar documentos que ainda não tenham sido integrados
                    {
                        //Debug.Assert(procs.ContainsKey(nud));
                        var tn = new TreeNode(nud);
                        tn.ForeColor = Color.Gray;
                        procs.Add(nud, tn);
                    }

                    procs[nud].Nodes.Add(node);
                }
                node.Tag = c;
                nodesDct.Add(c.EntidadeExterna.IDExterno, node);
            }
            this.lvRelations.Nodes.AddRange(procs.Values.ToArray());

            this.lvRelations.AfterCheck += new TreeViewEventHandler(lvRelations_AfterCheck);

            this.lvRelations.ResumeLayout(false);
            this.lvRelations.PerformLayout();
            this.lvRelations.EndUpdate();
        }

        private void lvRelations_AfterCheck(object sender, TreeViewEventArgs e)
        {
            this.lvRelations.AfterCheck -= new TreeViewEventHandler(lvRelations_AfterCheck);
            var root = e.Node.Parent ?? e.Node;
            root.Checked = e.Node.Checked;
            root.Nodes.Cast<TreeNode>().ToList().ForEach(tn => tn.Checked = root.Checked);
            UpdateToolbarButtons();
            this.lvRelations.AfterCheck += new TreeViewEventHandler(lvRelations_AfterCheck);
        }

        private void lvRelations_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            DeactivateControls();
            if (this.lvRelations.SelectedNode != null && this.lvRelations.SelectedNode.Tag != null)
                ShowDocument((CorrespondenciaDocs)this.lvRelations.SelectedNode.Tag);
        }

        private void DeactivateControls()
        {
            this.controlDocumentoGisa1.Visible = false;
            this.controlDocumentoGisa1.Clear();
            this.controlDocumentoGisa1.RemoveRefreshEvents();
            this.controlDocumentoGisaProcesso1.Visible = false;
            this.controlDocumentoGisaProcesso1.Clear();
            this.controlDocumentoGisaProcesso1.RemoveRefreshEvents();
            this.controlDocumentoGisaAnexo1.Visible = false;
            this.controlDocumentoGisaAnexo1.Clear();
            this.controlDocumentoGisaAnexo1.RemoveRefreshEvents();
            this.controlDocumentoExterno1.Clear();
            this.controlDocumentoExterno1.Visible = false;
            this.controlDocumentoExternoAnexo1.Clear();
            this.controlDocumentoExternoAnexo1.Visible = false;
            this.controlDocumentoExternoProcesso1.Clear();
            this.controlDocumentoExternoProcesso1.Visible = false;
            this.gbDocInPorto.Text = "";
        }
        
        // guardar correspondências
        private void tsbGravar_Click(object sender, EventArgs e)
        {
            var itemsEditedNotChecked = this.nodesDct.Values.Where(tn => tn.Tag != null && ((CorrespondenciaDocs)tn.Tag).Edited && !tn.Checked).ToList();
            if (itemsEditedNotChecked.Count > 0)
            {
                var str = "Existem documentos editados não assinalados para gravar." + System.Environment.NewLine +
                    "Pretende continuar?";
                var result = MessageBox.Show(str, "Documentos editados não assinalados", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No) return;
            }

            this.Cursor = Cursors.WaitCursor;
            var checkedCorrespondencias = this.nodesDct.Values.Where(tn => tn.Tag != null && tn.Checked).Select(tn => ((Model.CorrespondenciaDocs)tn.Tag)).ToList();
            CorrespondenciaDocs lastCorrespondencia = null;
            var report = Database.Database.SaveCorrespondencias(checkedCorrespondencias, ref lastCorrespondencia);
            this.Cursor = Cursors.Default;

            if (report.Length > 0)
            {
                var frm = new FormIntergationReport();
                frm.Interrogacao =
                    lastCorrespondencia == null ? "Integração terminada com sucesso." : "Integração terminada com erros.";
                frm.Detalhes = report;
                frm.ShowDialog();
            }

            UpdateLastTimeStamp();
            SaveLastTimeStamp();
            DeactivateControls();

            // em caso de erro, selecionar o documento onde esse erro ocorreu
            if (lastCorrespondencia != null)
            {
                var nodeToSelect = nodesDct[lastCorrespondencia.EntidadeExterna.IDExterno];
                nodeToSelect.Checked = false;
                // retirar da lista os items já integrados
                var toDelete = this.nodesDct.Where(pair => pair.Value.Checked && pair.Value.Index < nodeToSelect.Index).ToList();
                toDelete.ForEach(pair => { this.lvRelations.Nodes.Remove(pair.Value); nodesDct.Remove(pair.Key); });
                this.lvRelations.SelectedNode = nodeToSelect;
                nodeToSelect.Checked = true;
            }
            else
            {
                // recalcular sugestões
                this.Cursor = Cursors.WaitCursor;
                documentos = this.nodesDct.Where(pair => !pair.Value.Checked).Select(pair => (DocumentoExterno)((CorrespondenciaDocs)pair.Value.Tag).EntidadeExterna).ToList();
                BuildSuggestions();
                UpdateToolbarButtons();
                this.Cursor = Cursors.Default;
            }

            UpdateToolbarButtons();
        }

        private void SaveLastTimeStamp()
        {
            var ts = GisaDataSetHelper.GetInstance().Integ_Config.Cast<GISADataset.Integ_ConfigRow>().SingleOrDefault();
            if (ts == null)
            {
                var newRow = GisaDataSetHelper.GetInstance().Integ_Config.NewInteg_ConfigRow();
                newRow.ID = 1;
                newRow.LastTimeStamp = lastTimeStamp;
                newRow.Versao = new byte[] { };
                newRow.isDeleted = 0;
                GisaDataSetHelper.GetInstance().Integ_Config.AddInteg_ConfigRow(newRow);
            }
            else
                ts.LastTimeStamp = lastTimeStamp;

            PersistencyHelper.save();
            PersistencyHelper.cleanDeletedData();
        }

        private void UpdateLastTimeStamp()
        {
            var uncheckedCorrespondencias = this.nodesDct.Values.Where(tn => tn.Tag != null && !tn.Checked).Select(tn => ((Model.CorrespondenciaDocs)tn.Tag)).ToList();

            if (uncheckedCorrespondencias.Count > 0) // obter o timestamp mínimo das correspondências que sobraram
            {
                lastTimeStamp = uncheckedCorrespondencias.Select(c => ((DocumentoExterno)c.EntidadeExterna).Timestamp).Min();
            }
            else // obter o máximo timestamp caso não haja nenhuma correspondência por tratar
            {
                var checkedCorrespondencias = this.nodesDct.Values.Where(tn => tn.Tag != null &&  tn.Checked).Select(tn => ((Model.CorrespondenciaDocs)tn.Tag)).ToList();
                lastTimeStamp = checkedCorrespondencias.Select(c => ((DocumentoExterno)c.EntidadeExterna).Timestamp).Max();
            }
        }

        private void ShowDocument(CorrespondenciaDocs c)
        {
            switch (c.EntidadeExterna.Tipo)
            {
                case TipoEntidadeExterna.Documento:
                    this.controlDocumentoExterno1.Documento = (DocumentoSimples)c.EntidadeExterna;
                    this.controlDocumentoExterno1.BringToFront();
                    this.controlDocumentoExterno1.Visible = true;
                    this.controlDocumentoGisa1.CorrespondenciaDocumento = c;
                    this.controlDocumentoGisa1.BringToFront();
                    this.controlDocumentoGisa1.Visible = true;
                    this.controlDocumentoGisa1.AddRefreshEvents();
                    this.gbDocInPorto.Text = "Documento DocInPorto";
                    break;
                case TipoEntidadeExterna.DocumentoAnexo:
                    this.controlDocumentoExternoAnexo1.Documento = (DocumentoAnexo)c.EntidadeExterna;
                    this.controlDocumentoExternoAnexo1.BringToFront();
                    this.controlDocumentoExternoAnexo1.Visible = true;
                    this.controlDocumentoGisaAnexo1.CorrespondenciaDocumento = c;
                    this.controlDocumentoGisaAnexo1.BringToFront();
                    this.controlDocumentoGisaAnexo1.Visible = true;
                    this.controlDocumentoGisaAnexo1.AddRefreshEvents();
                    this.gbDocInPorto.Text = "Anexo DocInPorto";
                    break;
                case TipoEntidadeExterna.DocumentoComposto:
                    this.controlDocumentoExternoProcesso1.Documento = (DocumentoComposto)c.EntidadeExterna;
                    this.controlDocumentoExternoProcesso1.BringToFront();
                    this.controlDocumentoExternoProcesso1.Visible = true;
                    this.controlDocumentoGisaProcesso1.CorrespondenciaDocumento = c;
                    this.controlDocumentoGisaProcesso1.BringToFront();
                    this.controlDocumentoGisaProcesso1.Visible = true;
                    this.controlDocumentoGisaProcesso1.AddRefreshEvents();
                    this.gbDocInPorto.Text = "Processo DocInPorto";
                    break;
            }
        }

        private void UpdateToolbarButtons()
        {
            this.tsbGravar.Enabled = this.nodesDct.Values.Count(tn => tn.Checked) > 0;
        }

        private void marcacaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.lvRelations.AfterCheck -= new TreeViewEventHandler(lvRelations_AfterCheck);
            foreach(TreeNode node in this.nodesDct.Values)
                node.Checked = remarcacao(sender, node.Checked);
            this.lvRelations.AfterCheck += new TreeViewEventHandler(lvRelations_AfterCheck);
            UpdateToolbarButtons();
        }

        private bool remarcacao(object sender, bool marcacaoActual) 
        {
            if (sender == marcarTodosToolStripMenuItem)
                return true;
            else if (sender == desmarcarTodosToolStripMenuItem)
                return false;
            else if (sender == inverterMarcaçãoToolStripMenuItem)
                return !marcacaoActual;
            else
                return marcacaoActual;
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            // se houver dados alterados indicar ao utilizador se pretende gravá-los e avançar, avançar sem gravar ou cancelar a operação
            var itemsEditedNotChecked = this.nodesDct.Values.Count(tn => tn.Tag != null && ((CorrespondenciaDocs)tn.Tag).Edited && !tn.Checked); //
            if (itemsEditedNotChecked > 0)
            {
                var str = "Existem documentos marcados que ainda não foram integrados." + System.Environment.NewLine +
                    "Pretende integrar os documentos?";
                var result = MessageBox.Show(str, "Documentos marcados não integrados", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                switch (result)
                {
                    case DialogResult.Yes:
                        tsbGravar_Click(sender, e);
                        InitializeData();
                        break;
                    case DialogResult.No:
                        DeactivateControls();
                        InitializeData();
                        break;
                    case DialogResult.Cancel:
                        break;
                }
            }
            else
            {
                DeactivateControls();
                InitializeData();
                UpdateToolbarButtons();
            }
            this.Cursor = Cursors.Default;
        }

        private void splitContainerDetails_Resize(object sender, EventArgs e)
        {
            repositionSplitter();
        }

        private void repositionSplitter()
        {
            if (this.splitContainerDetails.Width < 1000)
                this.splitContainerDetails.SplitterDistance = this.splitContainerDetails.Width / 2;
        }

        public bool IsCorrespondenciaEdited()
        {
            return this.nodesDct.Values.Count(tn => tn.Tag != null && ((CorrespondenciaDocs)tn.Tag).Edited) > 0;
        }

        public void Save()
        {
            this.nodesDct.Values.Where(tn => tn.Tag != null && ((CorrespondenciaDocs)tn.Tag).Edited && !tn.Checked).ToList().ForEach(item => item.Checked = true);
            tsbGravar_Click(this, new EventArgs());
        }

        private void lvRelations_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            RefreshToolStripMenuItemState();

            this.contextMenuStrip1.Show();
        }

        private void RefreshToolStripMenuItemState()
        {
            var state = this.lvRelations.SelectedNode != null;
            marcarToolStripMenuItem.Enabled = state;
            desmarcarToolStripMenuItem.Enabled = state;
            reverterOpçõesToolStripMenuItem.Enabled = state;
        }

        private void marcarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lvRelations.SelectedNode != null)
                this.lvRelations.SelectedNode.Checked = true;
        }

        private void desmarcarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lvRelations.SelectedNode != null)
                this.lvRelations.SelectedNode.Checked = false;
        }

        private void marcarTodosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.lvRelations.AfterCheck -= new TreeViewEventHandler(lvRelations_AfterCheck);
            this.nodesDct.Values.ToList().ForEach(tn => tn.Checked = true);
            this.lvRelations.AfterCheck += new TreeViewEventHandler(lvRelations_AfterCheck);
        }

        private void desmarcarTodosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.lvRelations.AfterCheck -= new TreeViewEventHandler(lvRelations_AfterCheck);
            this.nodesDct.Values.ToList().ForEach(tn => tn.Checked = false);
            this.lvRelations.AfterCheck += new TreeViewEventHandler(lvRelations_AfterCheck);
        }

        private void reverterOpçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tn = this.lvRelations.SelectedNode;
            tn.NodeFont = fontNormal;
            tn.Checked = false;
            if (tn.Tag == null) return;            
            var correspondencia = tn.Tag as CorrespondenciaDocs;
            Database.Database.RevertCorrespondencia(correspondencia);
            DeactivateControls();
            ShowDocument(correspondencia);            
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            RefreshToolStripMenuItemState();
        }
    }
}