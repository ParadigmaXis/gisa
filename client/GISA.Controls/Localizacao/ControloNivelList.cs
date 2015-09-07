using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using GISA.SharedResources;
using GISA;

namespace GISA.Controls.Localizacao
{
	public class ControloNivelList : ControloLocalizacao
	{

	#region  Windows Form Designer generated code 

		public ControloNivelList() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            trVwLocalizacao.MouseMove += trVwLocalizacao_MouseMove;
            trVwLocalizacao.BeforeSelect += treeviews_BeforeSelect;
            trVwLocalizacao.AfterSelect += treeviews_AfterSelect;
            trVwLocalizacao.ShowNodeToolTips = true;
		}

		//UserControl overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		internal TimeLineControl TimeLineControl1;
		internal System.Windows.Forms.ToolTip ControloNivelListToolTip;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.TimeLineControl1 = new TimeLineControl();
			this.ControloNivelListToolTip = new System.Windows.Forms.ToolTip(this.components);
			//
			//TimeLineControl1
			//
			this.TimeLineControl1.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.TimeLineControl1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.TimeLineControl1.CurrentIntervalColor = System.Drawing.Color.PowderBlue;
			this.TimeLineControl1.CurrentMarker = new System.DateTime(System.Convert.ToInt64(0));
			this.TimeLineControl1.ExtendedIntervalColor = System.Drawing.Color.PaleGreen;
			this.TimeLineControl1.HighExtendedMarker = new System.DateTime(System.Convert.ToInt64(0));
			this.TimeLineControl1.Location = new System.Drawing.Point(8, 208);
			this.TimeLineControl1.LowExtendedMarker = new System.DateTime(System.Convert.ToInt64(0));
			this.TimeLineControl1.MarkerColor = System.Drawing.Color.White;
			this.TimeLineControl1.Markers = new System.DateTime[] {new System.DateTime(System.Convert.ToInt64(0)), new System.DateTime(9999, 12, 31, 23, 59, 59, 999)};
			this.TimeLineControl1.Name = "TimeLineControl1";
			this.TimeLineControl1.Size = new System.Drawing.Size(684, 20);
			this.TimeLineControl1.TabIndex = 2;
			this.TimeLineControl1.Visible = false;
			//
			//ControloNivelList
			//
			this.Name = "ControloNivelList";
			this.Size = new System.Drawing.Size(712, 248);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

        public long mTipoNivelRelLimitExcl = TipoNivelRelacionado.SD;
        public virtual long TipoNivelRelLimitExcl()
        {
            return mTipoNivelRelLimitExcl;
        }

		private GISATreeNode trVwLocalizacao_MouseMove_previousNode;
		public GISATreeNode SelectedNode
		{
			get	{return (GISATreeNode)trVwLocalizacao.SelectedNode;}
		}

		public delegate void setToolTipEventEventHandler(object sender, object item);
		public event setToolTipEventEventHandler setToolTipEvent;

		private void trVwLocalizacao_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
            // Find the node under the mouse.
			GISATreeNode currentNode = (GISATreeNode)(trVwLocalizacao.GetNodeAt(e.X, e.Y));

			if (currentNode == trVwLocalizacao_MouseMove_previousNode)
				return;

			trVwLocalizacao_MouseMove_previousNode = currentNode;

			// See if we have a valid node under mouse pointer
            if (setToolTipEvent != null)
                setToolTipEvent(this, currentNode);
		}

        public void CollapseAllNodes()
        {
            this.trVwLocalizacao.CollapseAll();
        }

		public static ArrayList GetCodigoCompletoCaminhoUnico(GISATreeNode currentNode)
		{
			GISADataset.RelacaoHierarquicaRow rhCurrentRow = null;

			ArrayList result = new ArrayList();
			while (currentNode != null)
			{
				// prever o caso em que um dos nós do caminho foi eliminado (por 
				// outro utilizador). nesse caso não é possível calcular o caminho
				if (currentNode.NivelRow.RowState == DataRowState.Detached)
					return null;

				rhCurrentRow = currentNode.RelacaoHierarquicaRow;
				if (rhCurrentRow != null)
					result.Insert(0, rhCurrentRow);

				currentNode = (GISATreeNode)currentNode.Parent;
			}
			return result;
		}

		public void SelectFirstNode()
		{
			if (trVwLocalizacao.Nodes.Count > 0)
				trVwLocalizacao.SelectedNode = trVwLocalizacao.Nodes[0];
		}

		public bool LoadSelectedNode()
		{
			TreeViewCancelEventArgs e = null;
			e = new TreeViewCancelEventArgs(trVwLocalizacao.SelectedNode, false, TreeViewAction.Unknown);

			if (trVwLocalizacao.SelectedNode != null)
				this.treeviews_BeforeSelect(trVwLocalizacao, e);

			if (! e.Cancel)
				this.treeviews_AfterSelect(trVwLocalizacao, new TreeViewEventArgs(trVwLocalizacao.SelectedNode));

			return e.Cancel;
		}

		private void treeviews_BeforeSelect(object Sender, TreeViewCancelEventArgs e)
		{
			long start = 0;
			start = DateTime.Now.Ticks;
			GISATreeNode node = (GISATreeNode)e.Node;

            PermissoesHelper.UpdateNivelPermissions(node.NivelRow, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);

			try
			{
				bool selectionChange = beforeNewSelection(node);
				if (! selectionChange)
					e.Cancel = true;
			}
			catch (InvalidOperationException)
			{
				e.Cancel = true;
			}

			Debug.WriteLine("<<treeviews_BeforeSelect>>: " + new TimeSpan(DateTime.Now.Ticks - start).ToString());
		}

		public event beforeNewSelectionEventHandler beforeNewSelectionEvent;
		public delegate void beforeNewSelectionEventHandler(BeforeNewSelectionEventArgs e);

		private bool beforeNewSelection(GISATreeNode node)
		{
			bool selectionChange = false;

			// prever a situações onde não se está dentro do frmMain
            this.Cursor = Cursors.WaitCursor;

			trVwLocalizacao.BeginUpdate();
			try
			{
				BeforeNewSelectionEventArgs e = new BeforeNewSelectionEventArgs(null, null);
				if (node == null)
				{
					e.nivel = null;
					e.node = null;
					if (beforeNewSelectionEvent != null)
						beforeNewSelectionEvent(e);
					selectionChange = e.selectionChange;
				}
				else
				{
					GISADataset.NivelRow nRow = node.NivelRow;
					e.nivel = nRow;
					e.node = node;
					if (beforeNewSelectionEvent != null)
						beforeNewSelectionEvent(e);
					selectionChange = e.selectionChange;
				}
			}
			finally
			{
				trVwLocalizacao.EndUpdate();
                this.Cursor = Cursors.Default;
			}
			return selectionChange;
		}

		private void treeviews_AfterSelect(object sender, TreeViewEventArgs e)
		{

			long start = 0;
			start = DateTime.Now.Ticks;

			afterNewSelection((GISATreeNode)e.Node);


			Debug.WriteLine("<<treeviews_AfterSelect>>: " + new TimeSpan(DateTime.Now.Ticks - start).ToString());
		}

		public event UpdateToolBarButtonsEventHandler UpdateToolBarButtonsEvent;
		public delegate void UpdateToolBarButtonsEventHandler(EventArgs e);
		private void afterNewSelection(GISATreeNode node)
		{
			if (UpdateToolBarButtonsEvent != null)
				UpdateToolBarButtonsEvent(new EventArgs());

			if (node != null && node.NivelRow.RowState != DataRowState.Detached && node.Nodes.Count == 0)
				AddToTable(node);
		}

		public void UpdateSelectedNodeName(string newName)
		{
			SelectedNode.Text = newName;
		}

		public void RefreshTreeViewControlSelectedBranch()
		{
			trVwLocalizacao.SelectedNode.Collapse();
			if (trVwLocalizacao.SelectedNode.Nodes.Count == 0)
			{
				trVwLocalizacao.SelectedNode.Nodes.Add("<A atualizar...>");
			}
			trVwLocalizacao.SelectedNode.Collapse();
			trVwLocalizacao.SelectedNode.Expand();
		}

		public void SelectParentNode(GISATreeNode parentNode)
		{
			// parentNode será nothing no caso de eliminarmos o último 
			// nível apresentado (pode ser a ED na vista da estrutura ou 
			// pode ser um nível de estrutura que seja usado como raiz 
			// na vista de documentos)
			trVwLocalizacao.SelectedNode = parentNode;
			if (trVwLocalizacao.SelectedNode != null)
			{
				trVwLocalizacao.SelectedNode.Collapse();
				if (trVwLocalizacao.SelectedNode.Nodes.Count == 0)
				{
					trVwLocalizacao.SelectedNode.Nodes.Add("<A atualizar...>");
				}
				trVwLocalizacao.SelectedNode.Collapse();
				trVwLocalizacao.SelectedNode.Expand();
			}
			else
			{
				// Chegamos a este ponto se estivermos na vista estrutural e 
				// se não existir um nó pai para selecionar após a eliminação 
				// do nó actual. No entanto, na vista estrutural podemos ter 
				// vários nós raiz (várias EDs), por isso ao eliminarmos uma 
				// das raizes podemos tentar selecionar uma outra que possa existir
				if (trVwLocalizacao.Nodes.Count > 0)
				{
					trVwLocalizacao.SelectedNode = trVwLocalizacao.Nodes[0];
				}
				else
				{
					// Apenas no caso de termos removido o último nó existente 
					// é que se dará o caso de não existir uma selecção.
					// Colocar o SelectedNode a Nothing não é o suficiente 
					// para provocar um BeforeSelect na treeview, por isso 
					// forçamos um.
					TreeViewCancelEventArgs cea = null;
					cea = new TreeViewCancelEventArgs(null, false, TreeViewAction.Unknown);
					this.treeviews_BeforeSelect(this, cea);
				}
			}
		}

	#region  Adição e remoção de nós das treeviews 

		// As seguintes hashtables sao utilizadas para acelerar o processo de 
		// encontrar determinado nó na treeview sempre que for detectada uma 
		// modificação numa RelacaoHierarquicaRow que nela exista.
		// Não é garantido que uma relação referida numa das hashtables exista ainda 
		// efectivamente na árvore, nesses casos os nós respectivos terão a propriedade 
		// "treeview" a nothing
		// Cada relação existente poderá ocorrer na árvore multiplas vezes se num 
		// ponto mais acima existir uma bifurcação que faça este nó surgir várias 
		// vezes na árvore. Por esta razão os items armazenados nas hashtables pode ser 
		// listas, nesses casos existirão vários nós para uma mesma relação referida
		private Hashtable tableEstrutural = new Hashtable();

		private void AddToTable(GISATreeNode node)
		{
			Hashtable table = tableEstrutural;

			if (table != null)
			{
				string key = getKey(node);
				object somethingAlreadyThere = null;
				somethingAlreadyThere = table[key];
				// se já existe um ou vários nós
				if (somethingAlreadyThere != null)
				{
					GISATreeNode nodeAlreadyThere = null;
					ArrayList nodesAlreadyThere = null;

					// se se tratar de um nó individual
					if (somethingAlreadyThere is GISATreeNode)
					{
						nodesAlreadyThere = new ArrayList();
						nodeAlreadyThere = (GISATreeNode)somethingAlreadyThere;
						if (nodeAlreadyThere.TreeView != null)
						{
							nodesAlreadyThere.Add(nodeAlreadyThere); // adicionar o nó que já existia naquele ponto
						}
						nodesAlreadyThere.Add(node); // adicionar o novo nó
						table.Remove(key); // remover o nó individual que existia
						table.Add(key, nodesAlreadyThere); // adicionar a arraylist contendo o nó individual anterior e o novo nó a adicionar
					}
					else // trata-se de um arraylist
					{
						Debug.Assert(somethingAlreadyThere is ArrayList);
						nodesAlreadyThere = (ArrayList)somethingAlreadyThere;

						// remover os nós ainda incluidos na hashtable mas que já nao pertençam à treeview
						int i = 0;
						while (i < nodesAlreadyThere.Count)
						{
							nodeAlreadyThere = (GISATreeNode)(nodesAlreadyThere[i]);
							if (nodeAlreadyThere.TreeView == null)
							{
								nodesAlreadyThere.Remove(nodeAlreadyThere);
							}
							else
							{
								i = i + 1;
							}
						}

						if (nodesAlreadyThere.Count > 0)
						{
							nodesAlreadyThere.Add(node);
						}
						else
						{
							table.Remove(key); // remover a arraylist vazia
							table.Add(key, node); // adicionar o novo nó
						}
					}
				}
				else
				{
					table.Add(key, node);
				}
			}

		}


		private void AddToTreeview(TreeNodeCollection nodes, GISATreeNode node)
		{
			AddToTreeview(nodes, node, -1);
		}

//INSTANT C# NOTE: C# does not support optional parameters. Overloaded method(s) are created above.
//ORIGINAL LINE: Private Sub AddToTreeview(ByVal nodes As TreeNodeCollection, ByVal node As GISATreeNode, Optional ByVal insertAt As Integer = -1)
		private void AddToTreeview(TreeNodeCollection nodes, GISATreeNode node, int insertAt)
		{

			if (insertAt == -1)
			{
				nodes.Add(node);
			}
			else
			{
				nodes.Insert(insertAt, node);
			}
			AddToTable(node);
		}


		public void RemoveFromTreeview(GISATreeNode node)
		{
			RemoveFromTreeview(node, null);
		}

//INSTANT C# NOTE: C# does not support optional parameters. Overloaded method(s) are created above.
//ORIGINAL LINE: Public Sub RemoveFromTreeview(ByVal node As GISATreeNode, Optional ByVal key As String = null)
		public void RemoveFromTreeview(GISATreeNode node, string key)
		{
			Hashtable table = tableEstrutural;

			node.Remove();
			if (table != null)
			{
				object somethingAlreadyThere = null;
				ArrayList nodesAlreadyThere = null;
				GISATreeNode nodeAlreadyThere = null;
				if (key == null)
				{
					somethingAlreadyThere = table[getKey(node)];
				}
				else
				{
					somethingAlreadyThere = table[key];
				}
				if (somethingAlreadyThere is GISATreeNode)
				{
					table.Remove(somethingAlreadyThere);
				}
				else // trata-se de um arraylist
				{
					Debug.Assert(somethingAlreadyThere is ArrayList);
					nodesAlreadyThere = (ArrayList)somethingAlreadyThere;
					//For Each nodeAlreadyThere As GISATreeNode In nodesAlreadyThere
					//    If nodeAlreadyThere Is node Then
					//        nodesAlreadyThere.Remove(nodeAlreadyThere)
					//    End If
					//Next


					int i = 0;
					while (i < nodesAlreadyThere.Count)
					{
						nodeAlreadyThere = (GISATreeNode)(nodesAlreadyThere[i]);
						if (nodeAlreadyThere == node)
						{
							nodesAlreadyThere.Remove(nodeAlreadyThere);
						}
						else
						{
							i = i + 1;
						}
					}
				}
			}
		}

		// constroi uma chave que permite indexar um nó na hashtable
		public static string getKey(GISATreeNode node)
		{
			if (node.NivelUpperRow == null)
			{
				return node.NivelRow.ID.ToString() + ":";
			}
			else
			{
				return node.NivelRow.ID.ToString() + ":" + node.NivelUpperRow.ID.ToString();
			}
		}

		private string getKey(GISADataset.RelacaoHierarquicaRow rhRow)
		{
			return getKey(rhRow.NivelRowByNivelRelacaoHierarquica.ID.ToString(), rhRow.NivelRowByNivelRelacaoHierarquicaUpper.ID.ToString());
		}

		private string getKey(string ID, string IDUpper)
		{
			return ID + ":" + IDUpper;
		}

		public void CollapseNodes(ArrayList foundNodes)
		{
			try
			{
				GISATreeNode collapsableNode = null;
				foreach (GISATreeNode foundNode in foundNodes)
				{
					if (foundNode.Parent == null)
					{
						collapsableNode = foundNode; // para as relações com EDs
					}
					else
					{
						collapsableNode = (GISATreeNode)foundNode.Parent; // para as outras relações colapsar a relação pai de forma a contemplar o caso de não existirem filhos
					}
					collapsableNode.Collapse();
				}
				// simular uma selecção do último nó colapsado, no caso de ter sido colapsado algum
				if (collapsableNode != null)
				{
					beforeNewSelection(collapsableNode); //, CurrentTreeView Is trVwSeries)
					afterNewSelection(collapsableNode);
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				Debug.Assert(false, ex.ToString());
				throw ex;
			}
		}

		// Devolve todas as aparições de determinada relação na árvore.
		// Uma dada relação pode constar várias vezes no caso de um determinado nó 
		// superior ter vários pais
		public ArrayList getNodeRepresentations(string ID, string IDUpper)
		{
			string key = getKey(ID, IDUpper);
			object somethingEstrutural = tableEstrutural[key];

			GISATreeNode foundNode = null;
			ArrayList foundNodes = null;

			// Ignorar o nó se este não for encontrado em nenhuma das hashtables 
			if (somethingEstrutural != null)
			{

				if (somethingEstrutural != null)
				{
					if (somethingEstrutural is GISATreeNode)
					{
						foundNode = (GISATreeNode)somethingEstrutural;
					}
					else
					{
						foundNodes = (ArrayList)somethingEstrutural;
					}
				}

				if (foundNode != null)
				{
					foundNodes = new ArrayList();
					foundNodes.Add(foundNode);
				}
			}
			if (foundNodes == null)
			{
				foundNodes = new ArrayList();
			}
			return foundNodes;
		}

		// Verifica se determinada relação se encontra visivel na árvore
		public bool isRelationInTree(string ID, string IDUpper)
		{
			foreach (GISATreeNode foundNode in getNodeRepresentations(ID, IDUpper))
			{
				if (foundNode.TreeView != null)
				{
					return true;
				}
			}
			return false;
		}
	#endregion

		public class BeforeNewSelectionEventArgs : EventArgs
		{

			public GISATreeNode node;
			public GISADataset.NivelRow nivel;
			public bool selectionChange = true;

			public BeforeNewSelectionEventArgs(GISATreeNode node, GISADataset.NivelRow nivel) : base()
			{
				this.node = node;
				this.nivel = nivel;
			}
		}

		public class itemDragSelectionEventArgs : EventArgs
		{

			public GISATreeNode node;
			public GISADataset.NivelRow nivel;
			public bool selectionChange = true;

			public itemDragSelectionEventArgs(GISATreeNode node, GISADataset.NivelRow nivel)
			{
				this.node = node;
				this.nivel = nivel;
			}
		}

	#region  Pesquisa 
		public GISADataset.NivelRow SelectedNivelRow
		{
            get { return trVwLocalizacao.SelectedNode != null ? ((GISATreeNode)trVwLocalizacao.SelectedNode).NivelRow : null; }
		}

		public GISADataset.NivelRow SelectedNivelUpperRow
		{
            get { return trVwLocalizacao.SelectedNode != null ? ((GISATreeNode)trVwLocalizacao.SelectedNode).NivelUpperRow : null; }
		}

		public GISADataset.RelacaoHierarquicaRow SelectedRelacaoHierarquica
		{
            get { return trVwLocalizacao.SelectedNode != null ? ((GISATreeNode)trVwLocalizacao.SelectedNode).RelacaoHierarquicaRow : null; }
		}

        public string SelectedNivelDesignacao
        {
            get { return trVwLocalizacao.SelectedNode != null ? ((GISATreeNode)trVwLocalizacao.SelectedNode).Text : ""; }
        }

		public void SetSelectedRelacaoHierarquica(GISADataset.NivelRow nRow, GISADataset.NivelRow nUpperRow)
		{
			//PanelIdentificacao.FindTreeNodeByNivelRow?
			ArrayList ns = AddSingleNivel((GISADataset.RelacaoHierarquicaRow)(GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", nRow.ID, nUpperRow.ID))[0]));
			trVwLocalizacao.SelectedNode = (TreeNode)(ns[0]);
		}

		private ArrayList AddSingleNivel(GISADataset.RelacaoHierarquicaRow rhRow)
		{
			ArrayList result = new ArrayList();
			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
				result = AddNivel(trVwLocalizacao, rhRow.NivelRowByNivelRelacaoHierarquica, rhRow.NivelRowByNivelRelacaoHierarquicaUpper, ho.Connection);
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

			return result;
		}
	#endregion


		public void LoadContents()
		{
			LoadContents(false);
		}

//INSTANT C# NOTE: C# does not support optional parameters. Overloaded method(s) are created above.
//ORIGINAL LINE: Public Sub LoadContents(Optional ByVal EditMode As Boolean = false)
		public void LoadContents(bool EditMode)
		{
			trVwLocalizacao.ImageList = TipoNivelRelacionado.GetImageList();
			if (EditMode)
			{
				this.Enabled = false;
			}
			else
			{
				this.Enabled = true;
				LoadNivelRoot(trVwLocalizacao);
				//LoadTimeLine()
				trVwLocalizacao.BeforeExpand += trvwEstrutura_BeforeExpand;
			}
		}

		private void LoadNivelRoot(TreeView trvw)
		{
			long startTicks = DateTime.Now.Ticks;

			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
				long[] edIDs = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.LoadEntidadesDetentoras(GisaDataSetHelper.GetInstance(), ho.Connection);
				trvw.Nodes.Clear();
				GISADataset.NivelRow nRow = null;
				GISADataset.TipoNivelRelacionadoRow tnrRow = null;
				foreach (long edID in edIDs)
				{
					nRow = (GISADataset.NivelRow)(GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + edID.ToString())[0]);                    
					tnrRow = (GISADataset.TipoNivelRelacionadoRow)(GisaDataSetHelper.GetInstance().TipoNivelRelacionado.Select(string.Format("ID={0}", TipoNivelRelacionado.ED))[0]);
					AddNivelDesignado(trvw.Nodes, nRow, null, tnrRow, ho.Connection);
				}
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

			Debug.WriteLine("<<LoadNivelRoot>>: " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());
		}

		private void ExpandNivel(GISATreeNode node)
		{
			GISADataset.NivelRow CurrentNivelUpperRow = node.NivelUpperRow;
			GISADataset.NivelRow CurrentNivelRow = node.NivelRow;
			long startTicks = 0;

			if (CurrentNivelRow == null || CurrentNivelRow.RowState == DataRowState.Detached)
				node.Nodes.Clear();
			else
			{
				try
				{
					// prever a situações onde não se está dentro do frmMain
                    this.Cursor = Cursors.WaitCursor;
					GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
					try
					{
						GisaDataSetHelper.ManageDatasetConstraints(false);

						startTicks = DateTime.Now.Ticks;
						
                        var filhos = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.GetNivelChildren(CurrentNivelRow.ID, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID, TipoNivelRelLimitExcl(), ho.Connection);
						DBAbstractDataLayer.DataAccessRules.NivelRule.Current.LoadNivelChildren(CurrentNivelRow.ID, true, GisaDataSetHelper.GetInstance(), ho.Connection);
						Debug.WriteLine("<<Get Tree Nodes>>: " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());

						GisaDataSetHelper.ManageDatasetConstraints(true);

						startTicks = DateTime.Now.Ticks;

						trVwLocalizacao.BeginUpdate();
						node.Nodes.Clear();
						accum = 0;
						int iconIndex = 0;

						foreach (DBAbstractDataLayer.DataAccessRules.NivelRule.MyNode filho in filhos) //DBAbstractDataLayer.DataAccessRules.NivelRule.Current.GetSortedData(CurrentNivelRow.ID, ho.Connection)
						{
							iconIndex = System.Convert.ToInt32(filho.TipoNivelRelacionado);
							GISATreeNode newNode = new GISATreeNode(filho.Designacao);

							if (filho.TipoNivel.Trim().Equals("CA"))
                                newNode.ImageIndex = iconIndex == -1 ? SharedResourcesOld.CurrentSharedResources.NivelImageIncognitoControloAut() : SharedResourcesOld.CurrentSharedResources.NivelImageControloAut(iconIndex);
							else
                                newNode.ImageIndex = iconIndex == -1 ? SharedResourcesOld.CurrentSharedResources.NivelImageIncognito() : SharedResourcesOld.CurrentSharedResources.NivelImageBase(iconIndex);

							newNode.SelectedImageIndex = newNode.ImageIndex;

							if (filho.AnoFim != null || filho.AnoInicio != null)
							{
								string data = FormatYearsInterval(filho.AnoInicio, filho.AnoFim);
								if (data.Length > 0)
									newNode.Text = string.Format("{0}   {1}", newNode.Text, data);
							}

							try
							{
								long a = DateTime.Now.Ticks;
								if (filho.Age > 0)
									newNode.Nodes.Add("<A actualizar...>");

								long b = DateTime.Now.Ticks;
								accum += (b - a);
							}
							catch (Exception ex)
							{
								Trace.WriteLine(ex);
								throw;
							}

							newNode.NivelRow = (GISADataset.NivelRow)(GisaDataSetHelper.GetInstance().Nivel.Select("ID = " + filho.IDNivel.ToString())[0]);
							newNode.NivelUpperRow = node.NivelRow;

							node.Nodes.Add(newNode);
                            AddToTable(newNode);
						}
						trVwLocalizacao.EndUpdate();
						Debug.WriteLine("<<EstimateChildCount>>: " + new TimeSpan(accum).ToString());
						Debug.WriteLine("<<Populate Tree Nodes>>: " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());
					}
					catch (System.Data.ConstraintException ex)
					{
						Trace.WriteLine(ex);
						Debug.Assert(false, ex.ToString());
						GisaDataSetHelper.FixDataSet(GisaDataSetHelper.GetInstance(), ho.Connection);
					}
					finally
					{
						ho.Dispose();
					}
				}
				finally
				{
                    this.Cursor = Cursors.Default;
				}
			}
		}

		public virtual bool isShowable(GISADataset.NivelRow nivel)
		{
			return isShowable(nivel, null);
		}

		public virtual bool isShowable(GISADataset.NivelRow nivel, GISADataset.NivelRow nivelUpper)
		{
			return (nivelUpper != null && nivelUpper.TipoNivelRow.IsStructure) || nivel.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].TipoNivelRelacionadoRow.ID < TipoNivelRelacionado.D;
		}

		private long accum;
		private void AddNivelDesignado(TreeNodeCollection TargetNodes, GISADataset.NivelRow nRow, GISADataset.NivelRow nUpperRow, GISADataset.TipoNivelRelacionadoRow tnrRow, IDbConnection connection)
		{
			GISATreeNode node = new GISATreeNode(Nivel.GetDesignacao(nRow));
			node.NivelRow = nRow;
			node.NivelUpperRow = nUpperRow;
			if (node.RelacaoHierarquicaRow != null)
			{
				string data = FormatYearsInterval(node.RelacaoHierarquicaRow);
				if (data.Length > 0)
					node.Text = string.Format("{0}   {1}", node.Text, data);
			}

			int iconIndex = 0;
			iconIndex = -1;
			if (tnrRow != null)
				iconIndex = System.Convert.ToInt32(tnrRow.GUIOrder);

			if (nRow.CatCode.Trim().Equals("CA"))
                node.ImageIndex = iconIndex == -1 ? SharedResourcesOld.CurrentSharedResources.NivelImageIncognitoControloAut() : SharedResourcesOld.CurrentSharedResources.NivelImageControloAut(iconIndex);
			else
                node.ImageIndex = iconIndex == -1 ? SharedResourcesOld.CurrentSharedResources.NivelImageIncognito() : SharedResourcesOld.CurrentSharedResources.NivelImageBase(iconIndex);

			node.SelectedImageIndex = node.ImageIndex;
			try
			{
				if (nUpperRow == null)
				{
                    if (DBAbstractDataLayer.DataAccessRules.NivelRule.Current.getDirectChildCount(nRow.ID.ToString(), "rh.IDTipoNivelRelacionado != 11", connection) > 0)
                            node.Nodes.Add("<A atualizar...>");
				}
				else
				{
					long a = DateTime.Now.Ticks;
					if (DBAbstractDataLayer.DataAccessRules.NivelRule.Current.EstimateChildCount(nUpperRow.ID.ToString(), true, connection).Contains(nRow.ID))
						node.Nodes.Add("<A atualizar...>");
					long b = DateTime.Now.Ticks;
					accum += (b - a);
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw;
			}
			AddToTreeview(TargetNodes, node, -1);
		}        

		private void trvwEstrutura_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			long startTicks = DateTime.Now.Ticks;

			Application.DoEvents();
			this.ExpandNivel((GISATreeNode)e.Node);

			Debug.WriteLine("<<trVw_BeforeExpand>> " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());
		}        
	}

} //end of root namespace