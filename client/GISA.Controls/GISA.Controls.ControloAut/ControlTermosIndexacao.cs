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
using GISA.Model;

namespace GISA.Controls.ControloAut
{
    public partial class ControlTermosIndexacao : UserControl
    {
        private GISADataset.ControloAutRow currentControloAut;
        private Dictionary<DataRow, TreeNode> nodeList;
        private ArrayList CurrentTermosTopo = new ArrayList(); // ArrayList de ControloAutDicionarioRows

        public bool NavigationMode { get; set; }

        public event TreeViewEventHandler AfterSelect;
        public new event KeyEventHandler KeyUp;

        public ControlTermosIndexacao()
        {
            InitializeComponent();

            trVwTermoIndexacao.AfterSelect += trVwTermoIndexacao_AfterSelect;
            trVwTermoIndexacao.KeyUp += trVwTermoIndexacao_KeyUp;
        }

        public TreeNode SelectedNode { get { return trVwTermoIndexacao.SelectedNode; } }

        public int DeleteNode(TreeNode node)
        {
            var diagResult = GUIHelper.GUIHelper.deleteSelectedTrVwItem(trVwTermoIndexacao);

            if (diagResult == (int)DialogResult.OK)
                nodeList.Remove((DataRow)node.Tag);

            return diagResult;
        }

        #region Event handlers
        private void trVwTermoIndexacao_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (AfterSelect != null)
                AfterSelect(sender, e);
        }

        private void trVwTermoIndexacao_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.KeyUp != null)
                this.KeyUp(sender, e);
        }
        #endregion

        public void LoadData(GISADataset.ControloAutRow caRow, IDbConnection conn)
        {
            GisaDataSetHelper.HoldOpen ho = null;
            TreeNode node = null;
            currentControloAut = caRow;
            nodeList = new Dictionary<DataRow, TreeNode>();

            ControloAutRule.Current.LoadThesaurus(GisaDataSetHelper.GetInstance(), currentControloAut.ID, conn);
            ReloadTermoDeTopo(conn);

            // adicionar ramos
            trVwTermoIndexacao.BeginUpdate();
            foreach (GISADataset.TipoNoticiaATipoControloAFormaRow nacaf in GisaDataSetHelper.GetInstance().TipoNoticiaATipoControloAForma.Select(string.Format("IDTipoNoticiaAut={0}", caRow.IDTipoNoticiaAut)))
            {
                node = trVwTermoIndexacao.Nodes.Add(nacaf.TipoControloAutFormaRow.Designacao);
                node.Tag = nacaf.TipoControloAutFormaRow;
            }

            if (caRow.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.Ideografico) || caRow.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.Onomastico) || caRow.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.ToponimicoGeografico))
            {
                // Activate thesaurus relations
                trVwTermoIndexacao.Nodes.Add("Termo de topo");
                foreach (GISADataset.TipoControloAutRelRow tcar in GisaDataSetHelper.GetInstance().TipoControloAutRel.Select("Thesaurus=1"))
                {
                    node = trVwTermoIndexacao.Nodes.Add(tcar.Designacao);
                    node.Tag = tcar;
                    if (tcar.Designacao != tcar.DesignacaoInversa)
                    {
                        node = trVwTermoIndexacao.Nodes.Add(tcar.DesignacaoInversa);
                        node.Tag = tcar;
                    }
                }

                ClearThesaurus();

                ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try
                {
                    PopulateThesaurus(ho.Connection);
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

                RepopulateTermoDeTopo();
            }
            else if (caRow.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.TipologiaInformacional))
            {
                // activate "Termo Relacionado"
                foreach (GISADataset.TipoControloAutRelRow tcar in GisaDataSetHelper.GetInstance().TipoControloAutRel.Select("Thesaurus=1"))
                {
                    if (tcar.Designacao == tcar.DesignacaoInversa)
                    {
                        //TODO: INSTANT C# TODO TASK: The return type of the tempWith4 variable must be corrected.
                        //ORIGINAL LINE: With trVwTermoIndexacao.Nodes.Add(tcar.Designacao)
                        node = trVwTermoIndexacao.Nodes.Add(tcar.Designacao);
                        node.Tag = tcar;
                    }
                }

                ClearThesaurus();

                ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try
                {
                    PopulateThesaurus(ho.Connection);
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

                RepopulateTermoDeTopo();
            }

            GisaDataSetHelper.VisitControloAutDicionario(caRow, PutDicionarioInTreeView);

            trVwTermoIndexacao.ExpandAll();
            trVwTermoIndexacao.EndUpdate();
        }

        private void PopulateThesaurus(IDbConnection conn)
        {
            GISADataset.ControloAutRelRow carRow = null;
            ArrayList CurrentTermos = new ArrayList();
            CurrentTermos = ControloAutRule.Current.GetTermos(currentControloAut.ID, conn);

            trVwTermoIndexacao.BeginUpdate();
            foreach (ArrayList Termo in CurrentTermos)
            {
                carRow = (GISADataset.ControloAutRelRow)(GisaDataSetHelper.GetInstance().ControloAutRel.Select(string.Format("(IDControloAut={0} AND IDControloAutAlias={1}) AND IDTipoRel IN (1, 2) AND isDeleted = 0 ", Termo[0].ToString(), Termo[1].ToString()))[0]);
                PutControloAutRelInTreeView(carRow, null, false);
            }
            trVwTermoIndexacao.EndUpdate();
        }

        public void Deactivate()
        {
            trVwTermoIndexacao.Nodes.Clear();
        }

        private Font mNormalFont = null;
        private Font NormalFont
        {
            get
            {
                if (mNormalFont == null)
                    mNormalFont = new Font(trVwTermoIndexacao.Font, trVwTermoIndexacao.Font.Style ^ FontStyle.Bold);

                return mNormalFont;
            }
        }

        private void PutDicionarioInTreeView(GISADataset.ControloAutDicionarioRow ControloAutDicionario)
        {
            foreach (TreeNode Node in trVwTermoIndexacao.Nodes)
            {
                if (ControloAutDicionario.IDTipoControloAutForma == ((GISADataset.TipoControloAutFormaRow)Node.Tag).ID)
                {
                    RegisterNode(Node, ControloAutDicionario);
                    return;
                }
            }
        }

        public void RegisterNode(TreeNode Node, GISADataset.ControloAutDicionarioRow ControloAutDicionario)
        {
            RegisterNode(Node, ControloAutDicionario, true);
        }

        public void RegisterNode(TreeNode Node, GISADataset.ControloAutDicionarioRow ControloAutDicionario, bool selectIt)
        {
            RegisterNode(Node, ControloAutDicionario.DicionarioRow.Termo, ControloAutDicionario, selectIt);
        }

        public void RegisterNode(TreeNode Node, GISADataset.ControloAutRelRow ControloAutRel, GISADataset.ControloAutDicionarioRow ControloAutDicionario)
        {
            RegisterNode(Node, ControloAutRel, ControloAutDicionario, true);
        }

        public void RegisterNode(TreeNode Node, GISADataset.ControloAutRelRow ControloAutRel, GISADataset.ControloAutDicionarioRow ControloAutDicionario, bool selectIt)
        {
            RegisterNode(Node, ControloAutDicionario.DicionarioRow.Termo, ControloAutRel, selectIt);
        }

        public void RegisterNode(TreeNode Node, string Termo, DataRow row)
        {
            RegisterNode(Node, Termo, row, true);
        }

        public void RegisterNode(TreeNode Node, string Termo, DataRow row, bool selectIt)
        {
            TreeNode addedNode = new TreeNode(Termo);
            addedNode.Tag = row;
            addedNode.NodeFont = NormalFont;
            Node.Nodes.Add(addedNode);
            Node.Expand();
            if (selectIt)
                trVwTermoIndexacao.SelectedNode = addedNode;

            if (!nodeList.ContainsKey(row))
                nodeList.Add(row, addedNode);
        }

        public TreeNode GetBranchNode(TreeNode node)
        {
            if (node == null)
                throw new ArgumentException("Can't be null", "node");

            while (node.Parent != null)
                node = node.Parent;

            return node;
        }

        public bool NodeAlreadyExists(GISADataset.DicionarioRow dr)
        {
            foreach (TreeNode existingBranch in trVwTermoIndexacao.Nodes)
            {
                if (existingBranch.Tag is GISADataset.TipoControloAutFormaRow)
                {
                    foreach (TreeNode existingNode in existingBranch.Nodes)
                    {
                        if (dr.ID == ((GISADataset.ControloAutDicionarioRow)existingNode.Tag).IDDicionario) 
                            return true;
                    }
                }
            }
            return false;
        }

        public void UpdateNodesName()
        {
            foreach (TreeNode existingBranch in trVwTermoIndexacao.Nodes)
            {
                foreach (TreeNode existingNode in existingBranch.Nodes)
                {
                    if (existingBranch.Tag == null)
                    {
                        //TODO: FIXME Contains "Termo de topo"
                    }
                    else if (existingBranch.Tag is GISADataset.TipoControloAutFormaRow)
                        existingNode.Text = ((GISADataset.ControloAutDicionarioRow)existingNode.Tag).DicionarioRow.Termo;
                    else if (existingBranch.Tag is GISADataset.TipoControloAutRelRow)
                    {
                        foreach (GISADataset.ControloAutDicionarioRow cad in ((GISADataset.ControloAutRelRow)existingNode.Tag).ControloAutRowByControloAutControloAutRel.GetControloAutDicionarioRows())
                        {
                            if (cad.IDTipoControloAutForma == Convert.ToInt64(TipoControloAutForma.FormaAutorizada))
                                existingNode.Text = cad.DicionarioRow.Termo;
                        }
                    }
                    else
                    {
                        StackTrace st = new StackTrace(true);
                        Trace.WriteLine("Unexpected TreeNode.Tag at " + st.ToString());
                    }
                }
            }
        }

        #region  Relacoes de thesaurus...
        public void ReloadTermoDeTopo(IDbConnection conn)
        {
            var ids = ControloAutRule.Current.LoadTermos(GisaDataSetHelper.GetInstance(), currentControloAut.ID, conn);

            CurrentTermosTopo.Clear();

            GISADataset.ControloAutDicionarioRow[] cadrs = null;
            foreach (var id in ids)
            {
                // só poderão ser devolvidos varios ControloAutDicionarios no 
                // caso de existirem várias formas autorizadas para o mesmo 
                // registo de autoridade (!). Tal nunca deveria acontecer, 
                // mas é possível em casos limite de concorrência
                cadrs = (GISADataset.ControloAutDicionarioRow[])(GisaDataSetHelper.GetInstance().ControloAutDicionario.Select(string.Format("IDTipoControloAutForma={0:d} AND IDControloAut={1}", TipoControloAutForma.FormaAutorizada, id)));
                CurrentTermosTopo.AddRange(cadrs);
            }
        }

        public void RepopulateTermoDeTopo()
        {
            TreeNode Root = null;
            TreeNode Topos = null;

            foreach (TreeNode Node in trVwTermoIndexacao.Nodes)
            {
                if (Node.Tag == null)
                    Topos = Node;
                else
                {
                    if (Node.Tag is GISADataset.TipoControloAutRelRow)
                    {
                        GISADataset.TipoControloAutRelRow DataRow = (GISADataset.TipoControloAutRelRow)Node.Tag;
                        if (Node.Text == DataRow.Designacao && DataRow.Designacao != DataRow.DesignacaoInversa)
                            Root = Node;
                    }
                }
            }

            if (Root == null | Topos == null)
                return;

            Topos.Nodes.Clear();
            foreach (GISADataset.ControloAutDicionarioRow cadRow in CurrentTermosTopo)
            {
                TreeNode newNode = Topos.Nodes.Add(cadRow.DicionarioRow.Termo);
                newNode.Tag = cadRow;
                newNode.NodeFont = NormalFont;
            }
            Topos.Expand();
        }

        private void ClearThesaurus()
        {
            trVwTermoIndexacao.BeginUpdate();
            foreach (TreeNode Node in trVwTermoIndexacao.Nodes)
            {
                if (Node.Tag is GISADataset.TipoControloAutRelRow)
                    Node.Nodes.Clear();
            }
            trVwTermoIndexacao.EndUpdate();
            nodeList.Clear();
        }

        public void RepopulateThesaurus(bool justTermoDeTopo)
        {
            RepopulateThesaurus(null, justTermoDeTopo);
        }

        public void RepopulateThesaurus(GISADataset.ControloAutRelRow newCarRows)
        {
            RepopulateThesaurus(newCarRows, false);
        }

        public void RepopulateThesaurus(GISADataset.ControloAutRelRow newCarRows, bool justTermoDeTopo)
        {
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                if (!justTermoDeTopo)
                {
                    ClearThesaurus();
                    PopulateThesaurus(ho.Connection);
                }

                if (newCarRows != null && newCarRows.RowState != DataRowState.Detached)
                    trVwTermoIndexacao.SelectedNode = nodeList[newCarRows];

                try
                {
                    ReloadTermoDeTopo(ho.Connection);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                    throw;
                }

                RepopulateTermoDeTopo();
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

        private void PutControloAutRelInTreeView(GISADataset.ControloAutRelRow carRow, GISADataset.ControloAutDicionarioRow ControloAutDicionario)
        {
            PutControloAutRelInTreeView(carRow, ControloAutDicionario, true);
        }

        private void PutControloAutRelInTreeView(GISADataset.ControloAutRelRow carRow, GISADataset.ControloAutDicionarioRow ControloAutDicionario, bool selectNewItem)
        {
            GISADataset.TipoControloAutRelRow tcarRow = null;
            DataRow[] cadRows = null;

            // procurar o ramo correcto onde adicionar a relação
            foreach (TreeNode Node in trVwTermoIndexacao.Nodes)
            {
                if (Node.Tag is GISADataset.TipoControloAutRelRow)
                {
                    try
                    {
                        tcarRow = (GISADataset.TipoControloAutRelRow)Node.Tag;
                        if (carRow.IDTipoRel == tcarRow.ID)
                        {
                            long ControloAutID = -1;
                            if (carRow.IDControloAut == currentControloAut.ID && carRow.TipoControloAutRelRow.Designacao.Equals(Node.Text))
                                ControloAutID = carRow.ControloAutRowByControloAutControloAutRelAlias.ID;
                            else if (carRow.IDControloAutAlias == currentControloAut.ID && carRow.TipoControloAutRelRow.DesignacaoInversa.Equals(Node.Text))
                                ControloAutID = carRow.ControloAutRowByControloAutControloAutRel.ID;

                            if (ControloAutID != -1)
                            {
                                // procurar a forma autorizada
                                cadRows = GisaDataSetHelper.GetInstance().ControloAutDicionario.Select(string.Format("IDControloAut={0} AND IDTipoControloAutForma=1", ControloAutID));
                                if (cadRows.Length > 0)
                                {
                                    // adicionar o CA à árvore
                                    RegisterNode(Node, carRow, (GISADataset.ControloAutDicionarioRow)(cadRows[0]), selectNewItem);
                                    break;
                                }
                            }
                        }
                    }
                    catch (NullReferenceException ex)
                    {
                        // Ignore the Null Pointer, it's "Termo de Topo"
                        if (string.Compare(Node.Text, "termo de topo", true) != 0)
                        {
                            throw ex;
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                        throw;
                    }
                }
            }
        }        
        #endregion

        private void trVwTermoIndexacao_DoubleClick(object sender, EventArgs e)
        {
            if (!NavigationMode || this.SelectedNode == null || this.SelectedNode.Tag == null) return;

            var branchNode = GetBranchNode(this.SelectedNode);

            // Permite-se navegar só nos ControloAutRels (Excepto ControloAutDicionario que seja termo de topo)
            if (branchNode == this.SelectedNode || !(this.SelectedNode.Tag is GISADataset.ControloAutRelRow || (this.SelectedNode.Tag is GISADataset.ControloAutDicionarioRow && branchNode.Text.Equals("Termo de topo")))) return;

            if (this.SelectedNode.Tag is GISADataset.ControloAutDicionarioRow)
                currentControloAut = ((GISADataset.ControloAutDicionarioRow)this.SelectedNode.Tag).ControloAutRow;
            else
            {
                var tcarRow = this.SelectedNode.Tag as GISADataset.ControloAutRelRow;
                currentControloAut = currentControloAut.ID == tcarRow.ControloAutRowByControloAutControloAutRelAlias.ID ? tcarRow.ControloAutRowByControloAutControloAutRel : tcarRow.ControloAutRowByControloAutControloAutRelAlias;
            }

            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                this.trVwTermoIndexacao.Nodes.Clear();
                this.LoadData(currentControloAut, ho.Connection);
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
}
