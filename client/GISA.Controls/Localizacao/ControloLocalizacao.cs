using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using GISA.SharedResources;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Controls.Localizacao
{
	public class ControloLocalizacao : System.Windows.Forms.UserControl
	{

	#region  Windows Form Designer generated code 

		public ControloLocalizacao() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            trVwLocalizacao.BeforeSelect += trVwLocalizacao_BeforeSelect;
            trVwLocalizacao.AfterSelect += trVwLocalizacao_AfterSelect;
			GetExtraResources();
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
        private System.ComponentModel.IContainer components = null;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		public System.Windows.Forms.TreeView trVwLocalizacao;
		public System.Windows.Forms.GroupBox grpLocalizacao;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.grpLocalizacao = new System.Windows.Forms.GroupBox();
            this.trVwLocalizacao = new System.Windows.Forms.TreeView();
            this.grpLocalizacao.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLocalizacao
            // 
            this.grpLocalizacao.Controls.Add(this.trVwLocalizacao);
            this.grpLocalizacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLocalizacao.Location = new System.Drawing.Point(0, 0);
            this.grpLocalizacao.Name = "grpLocalizacao";
            this.grpLocalizacao.Size = new System.Drawing.Size(448, 224);
            this.grpLocalizacao.TabIndex = 0;
            this.grpLocalizacao.TabStop = false;
            // 
            // trVwLocalizacao
            // 
            this.trVwLocalizacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trVwLocalizacao.HideSelection = false;
            this.trVwLocalizacao.Location = new System.Drawing.Point(3, 16);
            this.trVwLocalizacao.Name = "trVwLocalizacao";
            this.trVwLocalizacao.Size = new System.Drawing.Size(442, 205);
            this.trVwLocalizacao.TabIndex = 0;
            // 
            // ControloLocalizacao
            // 
            this.Controls.Add(this.grpLocalizacao);
            this.Name = "ControloLocalizacao";
            this.Size = new System.Drawing.Size(448, 224);
            this.grpLocalizacao.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

		private void GetExtraResources()
		{
			trVwLocalizacao.ImageList = TipoNivelRelacionado.GetImageList();
		}

		public void BuildTree(long nivelID, IDbConnection connection, long trusteeUserOperatorID)
		{
			long start = 0;
			start = DateTime.Now.Ticks;

			trVwLocalizacao.BeginUpdate();
			trVwLocalizacao.Nodes.Clear();

			ArrayList lista = new ArrayList();
            lista = NivelRule.Current.GetNivelLocalizacao(nivelID, trusteeUserOperatorID, connection);

			Hashtable nodesCollection = new Hashtable();
			Hashtable relations = new Hashtable();
			ArrayList parentNodes = new ArrayList();
			foreach (NivelRule.MyNode n in lista)
			{
				TreeNode node = new TreeNode(n.Designacao);
                node.Tag = n;

				int iconIndex = System.Convert.ToInt32(n.TipoNivelRelacionado);
				switch (n.TipoNivel)
				{
					case "CA":
						if (iconIndex == -1)
						{
							node.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageIncognitoControloAut();
						}
						else
						{
							node.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageControloAut(iconIndex);
						}
						break;
					case "NVL":
						if (iconIndex == -1)
						{
							node.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageIncognito();
						}
						else
						{
							node.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageBase(iconIndex);
						}
						break;
				}

                if (n.AnoFim != null || n.AnoInicio != null)
                {
                    string data = FormatYearsInterval(n.AnoInicio, n.AnoFim);
                    if (data.Length > 0)
                    {
                        node.Text = string.Format("{0}   {1}", node.Text, data);
                    }
                }

				node.SelectedImageIndex = node.ImageIndex;
				string rel = n.IDNivel.ToString() + "," + n.IDNivelUpper.ToString();

				if (! (nodesCollection.Contains(n.IDNivelUpper)) && ! (nodesCollection.Contains(n.IDNivel)))
				{
					//colocar raizes
					trVwLocalizacao.Nodes.Add(node);
					ArrayList a = new ArrayList();
					a.Add(node);
					nodesCollection.Add(n.IDNivel, a);
					relations.Add(rel, rel);
				}
				else
				{
					if (nodesCollection.Contains(n.IDNivelUpper))
					{
						//nó pai já existe
						if (! (nodesCollection.Contains(n.IDNivel)))
						{
							//nó filho não está na árvore
							parentNodes = (ArrayList)(nodesCollection[n.IDNivelUpper]);
							ArrayList nodesToBeAdded = new ArrayList();
							foreach (TreeNode parentNode in parentNodes)
							{
								TreeNode no = (TreeNode)(node.Clone());
								parentNode.Nodes.Add(no);
								nodesToBeAdded.Add(no);
							}
							nodesCollection.Add(n.IDNivel, nodesToBeAdded);
							relations.Add(rel, rel);
						}
						else
						{
							if (! (relations.Contains(rel)))
							{
								parentNodes = (ArrayList)(nodesCollection[n.IDNivelUpper]);
								ArrayList nodesToBeAdded = new ArrayList();
								foreach (TreeNode parentNode in parentNodes)
								{
									TreeNode no = (TreeNode)(node.Clone());
									parentNode.Nodes.Add(no);
									nodesToBeAdded.Add(no);
								}
								((ArrayList)(nodesCollection[n.IDNivel])).AddRange(nodesToBeAdded.ToArray());
								relations.Add(rel, rel);
							}
						}
					}
				}
			}

			trVwLocalizacao.EndUpdate();
			trVwLocalizacao.ExpandAll();
			Trace.WriteLine("<<Calcular e popular árvore de caminhos>>: " + new TimeSpan(DateTime.Now.Ticks - start).ToString());

            // se a árvore só tiver como nó uma entidade detentora então não mostra nada
            if (trVwLocalizacao.Nodes.Count > 0)
                trVwLocalizacao.TopNode = trVwLocalizacao.Nodes[0];
		}

		public ArrayList AddNivel(TreeView trvw, GISADataset.NivelRow nRow, GISADataset.NivelRow nUpperRow, IDbConnection connection)
		{
			ArrayList result = new ArrayList();
			GISATreeNode node = null;
			// Para as EDs (em relação às quais não existem níveis superiores)
			if (nUpperRow == null)
			{
				node = FindTreeNodeByNivelRow(trvw.Nodes, nRow);
				if (node == null)
				{
					node = new GISATreeNode(Nivel.GetDesignacao(nRow));
					//Node.Tag = rhRow
					node.NivelRow = nRow;
					node.NivelUpperRow = null;

					GISADataset.TipoNivelRelacionadoRow tnrRow = null;
					int iconIndex = 0;
					tnrRow = TipoNivelRelacionado.GetTipoNivelRelacionadoFromRelacaoHierarquica(node.NivelRow, node.NivelUpperRow);
					iconIndex = -1;

					if (tnrRow != null)
					{
						iconIndex = System.Convert.ToInt32(tnrRow.GUIOrder);
					}
					if (nRow.CatCode.Trim().Equals("CA"))
					{
						if (iconIndex == -1)
						{
							node.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageIncognitoControloAut();
						}
						else
						{
							node.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageControloAut(iconIndex);
						}
					}
					else
					{
						if (iconIndex == -1)
						{
							node.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageIncognito();
						}
						else
						{
							node.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageBase(iconIndex);
						}
					}
					node.SelectedImageIndex = node.ImageIndex;
					trvw.Nodes.Add(node);
				}
				result.Add(node);
			}
			else
			{
				ArrayList ParentNodes = new ArrayList();
				GISADataset.RelacaoHierarquicaRow[] TempParentRelations = Nivel.GetParentRelations(nRow, nUpperRow, connection);

				//Obter os nós pai com base nas rows pai
				if (TempParentRelations.Length == 0)
				{
					ArrayList nodes = null;
					nodes = AddNivel(trvw, nUpperRow, null, connection);
					ParentNodes.AddRange(nodes);
				}
				foreach (GISADataset.RelacaoHierarquicaRow parentRhRow in TempParentRelations)
				{
					ArrayList nodes = null;
					nodes = AddNivel(trvw, parentRhRow.NivelRowByNivelRelacaoHierarquica, parentRhRow.NivelRowByNivelRelacaoHierarquicaUpper, connection);
					ParentNodes.AddRange(nodes);
				}

				// Neste ponto ParentNodes contém todos os nós a que um nó de nRow deverá ser adicionado
				foreach (TreeNode parentNode in ParentNodes)
				{
					node = FindTreeNodeByNivelRow(parentNode.Nodes, nRow);
					if (node == null)
					{
						node = new GISATreeNode(Nivel.GetDesignacao(nRow));
						//node.Tag = rhRow
						node.NivelRow = nRow;
						node.NivelUpperRow = nUpperRow;
						string data = FormatYearsInterval(node.RelacaoHierarquicaRow);
						if (data.Length > 0)
						{
							node.Text = string.Format("{0}   {1}", node.Text, data);
						}
						if (nRow.CatCode.Trim().Equals("CA"))
						{
							node.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageControloAut(System.Convert.ToInt32(TipoNivelRelacionado.GetTipoNivelRelacionadoFromRelacaoHierarquica(node.RelacaoHierarquicaRow).GUIOrder));
						}
						else
						{
							node.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageBase(System.Convert.ToInt32(TipoNivelRelacionado.GetTipoNivelRelacionadoFromRelacaoHierarquica(node.RelacaoHierarquicaRow).GUIOrder));
						}
						node.SelectedImageIndex = node.ImageIndex;
						parentNode.Nodes.Add(node);
					}
					result.Add(node);
				}
			}
			return result;
		}

		public static GISATreeNode FindTreeNodeByNivelRow(TreeNodeCollection Nodes, GISADataset.NivelRow nRow)
		{

			foreach (GISATreeNode node in Nodes)
			{
				if (node.NivelRow == nRow)
				{
					return node;
				}
			}
			return null;
		}

		public void ClearTree()
		{
			trVwLocalizacao.Nodes.Clear();
		}

		private void trVwLocalizacao_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{

		}

		private void trVwLocalizacao_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{

		}

        public string FormatYearsInterval(GISA.Model.GISADataset.RelacaoHierarquicaRow rhRow)
        {
            string anoInicio = GISA.Model.GisaDataSetHelper.GetDBNullableText(rhRow, "InicioAno");
            string anoFim = GISA.Model.GisaDataSetHelper.GetDBNullableText(rhRow, "FimAno");
            return FormatYearsInterval(anoInicio, anoFim);
        }

        public string FormatYearsInterval(string anoInicio, string anoFim)
        {
            if (anoInicio.Length == 0 && anoFim.Length > 0)
            {
                return string.Format("(até {0})", anoFim); //=
            }
            else if (anoInicio.Length > 0 && anoFim.Length == 0)
            {
                return string.Format("(a partir de {0})", anoInicio); // =
            }
            else if (anoInicio.Length == 0 && anoFim.Length == 0)
            {
                return string.Empty;
            }
            else
            {
                return string.Format("({0} - {1})", anoInicio, anoFim);
            }
        }
	}

} //end of root namespace