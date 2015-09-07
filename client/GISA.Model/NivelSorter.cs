//INSTANT C# NOTE: Formerly VB.NET project-level imports:
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;

using GISA.Utils;

namespace GISA.Model
{
	public class NivelSorter : IComparer
	{

		public virtual int Compare(object x, object y)
		{
			try
			{
				// Possibilitar a recepção de nulls para ter em conta as EDs
				if (x == null && y == null)
				{
					return 0;
				}
				else if (x == null)
				{
					return -1;
				}
				else if (y == null)
				{
					return 1;
				}

				Debug.Assert(x is GISADataset.RelacaoHierarquicaRow);
				Debug.Assert(y is GISADataset.RelacaoHierarquicaRow);

				// se forem o mesmo objecto são garantidamente iguais
				if (x == y)
				{
					return 0;
				}

				GISADataset.RelacaoHierarquicaRow rhRow1 = (GISADataset.RelacaoHierarquicaRow)x;
				GISADataset.RelacaoHierarquicaRow rhRow2 = (GISADataset.RelacaoHierarquicaRow)y;

				decimal xgui = rhRow1.TipoNivelRelacionadoRow.GUIOrder;
				decimal ygui = rhRow2.TipoNivelRelacionadoRow.GUIOrder;

				if (xgui == ygui)
				{
					string anoInicio1 = GisaDataSetHelper.GetDBNullableText( rhRow1, "InicioAno");
					string mesInicio1 = GisaDataSetHelper.GetDBNullableText( rhRow1, "InicioMes");
					string diaInicio1 = GisaDataSetHelper.GetDBNullableText( rhRow1, "InicioDia");
					string anoInicio2 = GisaDataSetHelper.GetDBNullableText( rhRow2, "InicioAno");
					string mesInicio2 = GisaDataSetHelper.GetDBNullableText( rhRow2, "InicioMes");
					string diaInicio2 = GisaDataSetHelper.GetDBNullableText( rhRow2, "InicioDia");
					string dataInicio1 = string.Format("{0}{1}{2}", normalizeYear(anoInicio1), normalizeMonth(mesInicio1), normalizeYear(diaInicio1));
					string dataInicio2 = string.Format("{0}{1}{2}", normalizeYear(anoInicio2), normalizeMonth(mesInicio2), normalizeDay(diaInicio2));
					if (dataInicio1.CompareTo(dataInicio2) < 0)
					{
						return -1;
					}
					else if (dataInicio1.CompareTo(dataInicio2) > 0)
					{
						return 1;
					}

					//Dim xcod As String = rhRow1.NivelRowByNivelRelacaoHierarquica.Codigo
					//Dim ycod As String = rhRow2.NivelRowByNivelRelacaoHierarquica.Codigo

					//If MathHelper.IsNumber(xcod) AndAlso MathHelper.IsNumber(ycod) Then
					//    Dim xdec As Integer = Integer.Parse(xcod)
					//    Dim ydec As Integer = Integer.Parse(ycod)

					//    If xdec < ydec Then Return -1
					//    If xdec > ydec Then Return 1
					//Else
					//    If xcod < ycod Then Return -1
					//    If xcod > ycod Then Return 1
					//End If

					// Se o codigo for igual, comparar a designacao
					return CompareDesignacao(rhRow1, rhRow2);
				}
				else if (xgui < ygui)
				{
					return -1;
				}
				else //If xgui > ygui Then
				{
					return 1;
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
			}
			//INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
			return 0;
		}

		private string normalizeYear(string value)
		{
			normalizeDateMember(value, 4);
			//INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
			return null;
		}

		private string normalizeMonth(string value)
		{
			normalizeDateMember(value, 2);
			//INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
			return null;
		}

		private string normalizeDay(string value)
		{
			normalizeDateMember(value, 2);
			//INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
			return null;
		}

		private string normalizeDateMember(string value, int size)
		{
			value = value.Trim(' ', '?');
			if (value.Length > 0 && value.Length < 4)
			{
				value.PadLeft(size, '0');
			}
			return value;
		}

		private int CompareDesignacao(GISADataset.RelacaoHierarquicaRow rhRow1, GISADataset.RelacaoHierarquicaRow rhRow2)
		{
			string designacao1 = Nivel.GetDesignacao(rhRow1.NivelRowByNivelRelacaoHierarquica);
			string designacao2 = Nivel.GetDesignacao(rhRow2.NivelRowByNivelRelacaoHierarquica);
			return string.Compare(designacao1, designacao2, true);
		}

		public static ArrayList GetSortedNivelRow(IEnumerable RelacaoHierarquica)
		{
			ArrayList All = new ArrayList();
			foreach (GISADataset.RelacaoHierarquicaRow nr in RelacaoHierarquica)
			{
				All.Add(nr);
			}
			All.Sort(new NivelSorter());
			return All;
		}
	}

//Public Class RelacaoHierarquicaSorter
//    Inherits NivelSorter

//    Public Overrides Function Compare(ByVal x As Object, ByVal y As Object) As Integer
//        Debug.Assert(TypeOf x Is GISADataset.RelacaoHierarquicaRow)
//        Debug.Assert(TypeOf y Is GISADataset.RelacaoHierarquicaRow)
//        Dim xx As GISADataset.NivelRow = DirectCast(x, GISADataset.RelacaoHierarquicaRow).NivelRowByNivelRelacaoHierarquica
//        Dim yy As GISADataset.NivelRow = DirectCast(y, GISADataset.RelacaoHierarquicaRow).NivelRowByNivelRelacaoHierarquica
//        Return MyBase.Compare(xx, yy)
//    End Function

//    Public Shared Function GetSortedRelacaoHierarquicaRow(ByVal Nivel As IEnumerable) As ArrayList
//        Dim All As New ArrayList
//        For Each nhr As GISADataset.RelacaoHierarquicaRow In Nivel
//            All.Add(nhr)
//        Next
//        'Dim tn As TreeNode
//        All.Sort(New RelacaoHierarquicaSorter)
//        Return All
//    End Function
//End Class

	public class EntidadeDetentoraSorter : IComparer
	{

		public virtual int Compare(object x, object y)
		{
			GISADataset.NivelRow nRow1 = (GISADataset.NivelRow)x;
			GISADataset.NivelRow nRow2 = (GISADataset.NivelRow)y;

			string xcod = nRow1.Codigo;
			string ycod = nRow2.Codigo;
			if (MathHelper.IsInteger(xcod) && MathHelper.IsInteger(ycod))
			{
				int xdec = int.Parse(xcod);
				int ydec = int.Parse(ycod);

				if (xdec < ydec)
				{
					return -1;
				}
				if (xdec > ydec)
				{
					return 1;
				}
				return 0;
			}
			else
			{
				// Fall through if Codigo is not numeric            
				return string.Compare(xcod, ycod);
			}
		}

		public static ArrayList GetSortedEDRow(IEnumerable Nivel)
		{
			ArrayList All = new ArrayList();
			foreach (GISADataset.NivelRow nRow in Nivel)
			{
				All.Add(nRow);
			}
			All.Sort(new EntidadeDetentoraSorter());
			return All;
		}
	}
} //end of root namespace