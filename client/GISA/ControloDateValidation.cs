using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA
{
	public class ControloDateValidation : object
	{

		private TextBox txtAno;
		private TextBox txtMes;
		private TextBox txtDia;

		public ControloDateValidation(TextBox txtAno, TextBox txtMes, TextBox txtDia)
		{
			this.txtAno = txtAno;
			this.txtMes = txtMes;
			this.txtDia = txtDia;
			txtAno.Validating += txtAno_Validating;
			txtMes.Validating += txtMes_Validating;
			txtDia.Validating += txtDia_Validating;
			txtAno.KeyPress += data_KeyPress;
			txtMes.KeyPress += data_KeyPress;
			txtDia.KeyPress += data_KeyPress;
		}

		public void txtAno_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{

			if (! (IsValidYear(txtAno.Text)))
			{
				e.Cancel = true;
			}
			else
			{
				e.Cancel = false;
			}
		}

		public void txtMes_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{

			if (! (IsValidMonth(txtMes.Text)))
			{
				e.Cancel = true;
			}
			else
			{
				e.Cancel = false;
			}
		}

		public void txtDia_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{

			if (! (IsValidDay(txtMes.Text)))
			{
				e.Cancel = true;
			}
			else
			{
				e.Cancel = false;
			}
		}

		// este evento resolve a maioria dos problemas com a introdução 
		// de valores inválidos nas datas, no entanto, ficam por tratar 
		// outros modos de introdução dos dados (por exemplo fazendo "paste").
		// embora geralmente não seja um problema pode se-lo nos casos em
		// que se torne necessário forçar um endedit.
		public void data_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (! (char.IsDigit(e.KeyChar)) && ! (e.KeyChar == '?') && ! (char.IsControl(e.KeyChar)))
			{
				e.Handled = true;
			}
		}

		private bool IsValidYear(string txt)
		{
			System.Text.RegularExpressions.Regex exp = new System.Text.RegularExpressions.Regex("[1-9?][0-9?]?[0-9?]?[0-9?]?");

			if (exp.Match(txt).Value.Equals(txt))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private bool IsValidMonth(string txt)
		{
			System.Text.RegularExpressions.Regex exp = new System.Text.RegularExpressions.Regex("[0?]?[1-9?]$|[1?][0-2?]$");

			if (exp.Match(txt).Value.Equals(txt))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private bool IsValidDay(string txt)
		{
			System.Text.RegularExpressions.Regex exp = new System.Text.RegularExpressions.Regex("[0?]?[1-9?]$|[1-2?][0-9?]$|3[01?]$");

			if (exp.Match(txt).Value.Equals(txt))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private bool IsValid(string txt)
		{
			System.Text.RegularExpressions.Regex exp = new System.Text.RegularExpressions.Regex("[0-9?]*");
			if (exp.Match(txt).Value.Equals(txt))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private static bool IsCompleteNumber(string txt)
		{
			System.Text.RegularExpressions.Regex exp = new System.Text.RegularExpressions.Regex("[0-9]*");
			if (exp.Match(txt).Value.Equals(txt))
			{
				return true;
			}
			else
			{
				return false;
			}
		}


	#region  Logica partilhada 
		public enum PartialComparisonResult: int
		{
			Equal = 0,
			LessThan = -1,
			GreaterThan = 1,
			PartialyLessThan = -2,
			PartialyGreaterThan = 2,
			PartialyEqual = 4
		}

		public static PartialComparisonResult ComparePartialDate(string[] date1, string[] date2)
		{
			return ComparePartialDate(date1[0], date1[1], date1[2], date2[0], date2[1], date2[2]);
		}

		public static PartialComparisonResult ComparePartialDate(string AnoA, string MesA, string DiaA, string AnoB, string MesB, string DiaB)
		{

			PartialComparisonResult AnoResult = 0;
			PartialComparisonResult MesResult = 0;
			PartialComparisonResult DiaResult = 0;

			if (AnoA == null || AnoB == null)
			{
				return 0;
			}
			AnoResult = ComparePartialNumber(AnoA, AnoB);

			if (MesA == null || MesB == null)
			{
				return AnoResult;
			}
			MesResult = ComparePartialNumber(MesA, MesB);

			if (DiaA == null || DiaB == null)
			{
				return MesResult;
			}
			DiaResult = ComparePartialNumber(DiaA, DiaB);

			if (AnoResult == PartialComparisonResult.PartialyEqual)
			{
				return PartialComparisonResult.PartialyEqual;
			}
			if (AnoResult != PartialComparisonResult.Equal)
			{
				return AnoResult;
			}

			if (MesResult == PartialComparisonResult.PartialyEqual)
			{
				return PartialComparisonResult.PartialyEqual;
			}
			if (MesResult != PartialComparisonResult.Equal)
			{
				return MesResult;
			}

			return DiaResult;
		}

		private static PartialComparisonResult ComparePartialNumber(string A, string B)
		{
			bool @partial = false;

			while (A.Length > 0 && B.Length > 0 && (! (IsCompleteNumber(A)) || ! (IsCompleteNumber(B))))
			{
				A = A.Substring(0, A.Length - 1);
				B = B.Substring(0, B.Length - 1);
				@partial = true;
			}

			if (A.Length > 0 && B.Length > 0)
			{
				int Ai = System.Convert.ToInt32(A);
				int Bi = System.Convert.ToInt32(B);

				if (@partial)
				{
					if (Ai < Bi)
					{
						return PartialComparisonResult.PartialyLessThan;
					}
					else if (Ai > Bi)
					{
						return PartialComparisonResult.PartialyGreaterThan;
					}
					else
					{
						return PartialComparisonResult.PartialyEqual;
					}
				}
				else
				{
					if (Ai < Bi)
					{
						return PartialComparisonResult.LessThan;
					}
					else if (Ai > Bi)
					{
						return PartialComparisonResult.GreaterThan;
					}
					else
					{
						return PartialComparisonResult.Equal;
					}
				}
			}

			// chegamos aqui no caso de um dos numeros nao ser especificado (com "")
			return PartialComparisonResult.PartialyEqual;
		}


		public static string GetComposedDate(string Ano, string Mes, string Dia)
		{
			string Result = "";
			if (Ano == null || Ano.Length == 0)
			{
				Result += "????";
			}
			else
			{
				Result += Ano;
			}

			Result += "-";
			if (Mes == null || Mes.Length == 0)
			{
				Result += "??";
			}
			else
			{
				Result += Mes;
			}

			Result += "-";
			if (Dia == null || Dia.Length == 0)
			{
				Result += "??";
			}
			else
			{
				Result += Dia;
			}
			return Result;
		}

		public enum ComparisonResult: int
		{
			Equal = 0,
			FirstOne = 1,
			SecondOne = 2
		}

		private static ComparisonResult DetermineMostCompleteNumber(string A, string B)
		{

			// proteger de datas vazias ("")
			string trimmedA = A.Trim('?');
			string trimmedB = B.Trim('?');
			if (trimmedA.Length == 0 && trimmedB.Length == 0)
			{
				return ComparisonResult.Equal;
			}
			else if (trimmedA.Length == 0 && ! (trimmedB.Length == 0))
			{
				return ComparisonResult.SecondOne;
			}
			else if (trimmedB.Length == 0 && ! (trimmedA.Length == 0))
			{
				return ComparisonResult.FirstOne;
			}

			bool completeA = false;
			bool completeB = false;
			bool oldCompleteA = false;
			bool oldCompleteB = false;
			while (A.Length > 0 && B.Length > 0)
			{
				completeA = IsCompleteNumber(A);
				completeB = IsCompleteNumber(B);
				if (! completeA || ! completeB)
				{
					A = A.Substring(0, A.Length - 1);
					B = B.Substring(0, B.Length - 1);
					oldCompleteA = completeA;
					oldCompleteB = completeB;
				}
				else
				{
					break;
				}
			}

			// se o 1º a acabar foi o A quer dizer que o B é o mais completo
			if (oldCompleteA)
			{
				return ComparisonResult.SecondOne;
			}
			else if (oldCompleteB)
			{
				return ComparisonResult.FirstOne;
			}
			else
			{
				return ComparisonResult.Equal;
			}
		}

		public static ComparisonResult IsMoreCompleteThan(string ano1, string mes1, string dia1, string ano2, string mes2, string dia2)
		{

			ComparisonResult result = ComparisonResult.Equal;

			if (ano1.Trim(' ', '?').Length > 0 && ano2.Trim(' ', '?').Length > 0)
			{
				return ComparisonResult.Equal;
			}
			else if (ano1.Trim(' ', '?').Length > 0)
			{
				return ComparisonResult.SecondOne;
			}
			else if (ano2.Trim(' ', '?').Length > 0)
			{
				return ComparisonResult.FirstOne;
			}

			ComparisonResult a = DetermineMostCompleteNumber(ano1, ano2);
			if (a == ComparisonResult.Equal)
			{
				ComparisonResult m = DetermineMostCompleteNumber(mes1, mes2);
				if (m == ComparisonResult.Equal)
				{
					ComparisonResult d = DetermineMostCompleteNumber(dia1, dia2);
					result = d;
				}
				else
				{
					result = m;
				}
			}
			else
			{
				result = a;
			}

			return result;
		}

		public static ComparisonResult IsMoreCompleteThan(string[] data1, string[] data2)
		{
			return IsMoreCompleteThan(data1[0], data1[1], data1[2], data2[0], data2[1], data2[2]);
		}
	#endregion

		public class DateSorter : IComparer
		{

			public DateSorter() : this(false)
			{
			}

			public DateSorter(bool invert)
			{
				InvertSort = invert;
			}

			private bool InvertSort = false;

			public int Compare(object x, object y)
			{
				string[] date1 = (string[])x;
				string[] date2 = (string[])y;

				switch (ComparePartialDate(date1[0], date1[1], date1[2], date2[0], date2[1], date2[2]))
				{
					case PartialComparisonResult.Equal:
					case PartialComparisonResult.PartialyEqual:
						int resultAno = string.Compare(date1[0], date2[0]);
						int resultMes = string.Compare(date1[1], date2[1]);
						int resultDia = string.Compare(date1[2], date2[2]);
						if (resultAno != 0)
						{
							return resultAno;
						}
						else if (resultMes != 0)
						{
							return resultMes;
						}
						else
						{
							return resultDia;
						}
					case PartialComparisonResult.GreaterThan:
					case PartialComparisonResult.PartialyGreaterThan:
						if (! InvertSort)
						{
							return 1;
						}
						else
						{
							return -1;
						}
					case PartialComparisonResult.LessThan:
					case PartialComparisonResult.PartialyLessThan:
						if (! InvertSort)
						{
							return -1;
						}
						else
						{
							return 1;
						}
				}
				return 0;
			}

			// recebe um arraylist de arrays de datas
			public static void SortDates(ref ArrayList datas)
			{
				SortDates(ref datas, false);
			}

			public static void SortDates(ref ArrayList datas, bool invert)
			{
				datas.Sort(new DateSorter(invert));
			}
		}
	}
} //end of root namespace