//INSTANT C# NOTE: Formerly VB.NET project-level imports:
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;

using System.Windows.Forms;
//ORIGINAL LINE: Imports DBAbstractDataLayer.DataAccessRules.GISATreeNodeRule
//INSTANT C# NOTE: The following line has been modified since C# non-aliased 'using' statements only operate on namespaces:
using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Model
{
	public class GISATreeNode : TreeNode
	{

		public GISATreeNode() : base()
		{
		}

		public GISATreeNode(string text) : base(text)
		{
		}

		//Public Sub New(ByVal text As String, ByVal children As TreeNode())
		//Public Sub New(text as String, imageIndex as Integer, selectedImageIndex as Integer)
		//Public Sub New(text as String, imageIndex as Integer, selectedImageIndex as Integer, children as TreeNode())

		public GISADataset.RelacaoHierarquicaRow RelacaoHierarquicaRow
		{
			get
			{
				if (mNivelUpperRow == null || mNivelRow == null)
				{
					return null;
				}

				DataRow[] rhRows = GISATreeNodeRule.Current.SelectRelacaoHierarquicaRow(GisaDataSetHelper.GetInstance(), mNivelRow.ID, mNivelUpperRow.ID);
				if (rhRows.Length == 0)
				{
					return null;
				}
				else
				{
					return (GISADataset.RelacaoHierarquicaRow)(rhRows[0]);
				}
			}
		}

		private GISADataset.NivelRow mNivelRow;
		public GISADataset.NivelRow NivelRow
		{
			get
			{
				return mNivelRow;
			}
			set
			{
				mNivelRow = value;
			}
		}

		private GISADataset.NivelRow mNivelUpperRow;
		public GISADataset.NivelRow NivelUpperRow
		{
			get
			{
				return mNivelUpperRow;
			}
			set
			{
				mNivelUpperRow = value;
			}
		}

		//Private WithEvents nivelTable As GISADataset.NivelDataTable = GisaDataSetHelper.GetInstance().Nivel
		//Private WithEvents rhTable As GISADataset.RelacaoHierarquicaDataTable = GisaDataSetHelper.GetInstance().RelacaoHierarquica

		//Private Sub nivelTable_NivelRowChangedNivelRowDeleted(ByVal sender As Object, ByVal e As GISADataset.NivelRowChangeEvent) _
		//    Handles nivelTable.NivelRowChanged, nivelTable.NivelRowDeleted

		//    If Not (e.Action = DataRowAction.Add OrElse e.Action = DataRowAction.Delete) Then Exit Sub
		//    If Not (e.Row.RowState = DataRowState.Added OrElse e.Row.RowState = DataRowState.Deleted OrElse e.Row.RowState = DataRowState.Detached) Then Exit Sub

		//    If (Not NivelRow Is Nothing AndAlso e.Row Is NivelRow) OrElse _
		//        (Not NivelUpperRow Is Nothing AndAlso e.Row Is NivelUpperRow) Then

		//        Me.Parent.Collapse()
		//    End If
		//End Sub

		//Private Sub rhTable_RelacaoHierarquicaRowChangingRelacaoHierarquicaRowDeleting(ByVal sender As Object, ByVal e As GISADataset.RelacaoHierarquicaRowChangeEvent) _
		//     Handles rhTable.RelacaoHierarquicaRowChanged, rhTable.RelacaoHierarquicaRowDeleting

		//    Try
		//        If Not (e.Action = DataRowAction.Add OrElse e.Action = DataRowAction.Delete) Then Exit Sub
		//        'If Not (e.Row.RowState = DataRowState.Added OrElse e.Row.RowState = DataRowState.Deleted OrElse e.Row.RowState = DataRowState.Detached OrElse e.Row.RowState = DataRowState.Modified) Then Exit Sub

		//        If Me.TreeView Is Nothing Then Return ' se a trview for nothing é sinal que o nó já foi removido da arvore ou ainda nao foi adicionado

		//        Dim ctrl As Control = Me.TreeView.Parent.Parent ' inicia-se no pai de forma a que não seja tida em conta a visibilidade da propria treeview (a tree da estrutura pode ser invisivel se a de documentos estiver visivel)
		//        Dim ctrlVisible As Boolean = True
		//        Do While Not ctrl Is Nothing
		//            ctrlVisible = ctrlVisible And ctrl.Visible
		//            ctrl = ctrl.Parent
		//        Loop

		//        If Not ctrlVisible Then
		//            Dim changingNivelRow As GISADataset.NivelRow = e.Row.NivelRowByNivelRelacaoHierarquica
		//            Dim changingNivelUpperRow As GISADataset.NivelRow = e.Row.NivelRowByNivelRelacaoHierarquicaUpper

		//            If (Not NivelRow Is Nothing AndAlso Not changingNivelRow Is Nothing AndAlso changingNivelUpperRow Is NivelRow) Then
		//                ' fechar o nó apenas quando estivermos a mexer nestes dados por outra via que não este treenode

		//                If Me.Parent Is Nothing Then
		//                    Me.Collapse() ' para as relações com EDs
		//                Else
		//                    Me.Parent.Collapse() ' para as outras relações colapsar a relação pai de forma a contemplar o caso de não existirem filhos
		//                End If
		//            End If
		//        End If
		//    Catch ex As Exception
		//        Trace.WriteLine(ex)
		//        Throw ex
		//    End Try
		//End Sub
	}

} //end of root namespace