using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using System.Reflection;

namespace GISA.GUIHelper
{
	public static class GUIHelper
	{
		public delegate void BeforeDeleteCallback(DataRow row);

	#region  Listviews 

		public static void deleteSelectedLstVwItems(ListView ListView, BeforeDeleteCallback callback)
		{
			deleteSelectedLstVwItems(ListView, callback, null);
		}

		public static void deleteSelectedLstVwItems(ListView ListView)
		{
			deleteSelectedLstVwItems(ListView, null, null);
		}

		public static void deleteSelectedLstVwItems(ListView ListView, BeforeDeleteCallback callback, Type undeletableType)
		{
			if (ListView.SelectedItems.Count == 0)
				return;

			string TitleMsgBox = "Remoção de item(s)";
			string AditionalMsg = string.Empty;
			int NonDeletableItemsCount = 0;

			foreach (ListViewItem item in ListView.SelectedItems)
			{
				if ((undeletableType == null && item.Tag == null) || (undeletableType != null && undeletableType.IsInstanceOfType(item.Tag)))
				{
					if (AditionalMsg.Length == 0)
						AditionalMsg = System.Environment.NewLine + "Existe pelo menos um item que não poderá " + "ser removido, serão removidos apenas os restantes.";
					
                    NonDeletableItemsCount = NonDeletableItemsCount + 1;
				}
			}

			// se todos os items forem não editáveis não vale a pela prosseguir
			if (NonDeletableItemsCount == ListView.SelectedItems.Count)
			{
				if (ListView.SelectedItems.Count == 1)
					MessageBox.Show("O item selecionado não pode ser removido.", TitleMsgBox, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				else
					MessageBox.Show("Os items selecionados não podem ser removidos.", TitleMsgBox, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			switch (MessageBox.Show("Tem a certeza que deseja " + "eliminar o(s) item(s) selecionado(s)?" + AditionalMsg, TitleMsgBox, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
			{
				case DialogResult.OK:
					//Get and delete the selected rows 
					foreach (ListViewItem item in ListView.SelectedItems)
					{
						if (! (undeletableType == null && item.Tag == null) && ! (undeletableType != null && undeletableType.IsInstanceOfType(item.Tag)))
						{
							if (callback != null)
								callback((DataRow)item.Tag);

							((DataRow)item.Tag).Delete();
							item.Remove();
						}
					}
					break;
				case DialogResult.Cancel:
					// do nothing
				break;
			}
		}

		public static ListViewItem findListViewItemByTag(object tag, ListView ListView)
		{
			foreach (ListViewItem item in ListView.Items)
			{
				if (item.Tag == tag)
					return item;
			}
			return null;
		}

		// Devolve um listviewitem com todas as colunas preenchidas com string vazia
		public static ListViewItem newListviewItem(ListView lvw)
		{
			ListViewItem item = new ListViewItem(string.Empty);
			for (int i = 2; i <= lvw.Columns.Count; i++)
				item.SubItems.Add(string.Empty);

			return item;
		}
	#endregion

	#region  Treeviews 

		public static int deleteSelectedTrVwItem(TreeView TreeView)
		{
			return deleteSelectedTrVwItem(TreeView, null);
		}

//INSTANT C# NOTE: C# does not support optional parameters. Overloaded method(s) are created above.
//ORIGINAL LINE: Public Shared Function deleteSelectedTrVwItem(ByVal TreeView As TreeView, Optional ByVal callback As BeforeDeleteCallback = null) As Integer
		public static int deleteSelectedTrVwItem(TreeView TreeView, BeforeDeleteCallback callback)
		{

			TreeNode node = TreeView.SelectedNode;
			// proteger contra o caso não existir um nó selecionado 
			// e impedir a remoção dos items não editáveis
			if (node == null || node.Tag == null)
			{
				return -1;
			}

			switch (MessageBox.Show("Tem a certeza que deseja " + "eliminar o(s) item(s) selecionado(s)?", "Eliminar item", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
			{

				case DialogResult.OK:
					((DataRow)node.Tag).Delete();
					node.Remove();
					//TreeView.SelectedNode = Nothing
					return (int)DialogResult.OK;
				case DialogResult.Cancel:
					// do nothing
					return (int)DialogResult.Cancel;
			}
			//INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
			return 0;
		}
	#endregion

		public static bool IsVisible(Control ctrl)
		{
			bool ctrlVisible = true;
			while (ctrl != null)
			{
				ctrlVisible = ctrlVisible && ctrl.Visible;
				ctrl = ctrl.Parent;
			}
			return ctrlVisible;
		}

		public static void makeReadOnly(Control ctrl)
		{
			foreach (Control subctrl in ctrl.Controls)
			{
				// se o controlo não se tratar de uma folha, avança-se para os seus filhos
				if (subctrl.Controls.Count == 0)
				{
					//tratar as listviews de forma diferente para que permaneçam scrollable
					try
					{
						// procurar uma property "Readonly"
						MemberInfo[] memberInfos = subctrl.GetType().GetMember("ReadOnly");
						if (memberInfos.Length > 0)
							((PropertyInfo)(memberInfos[0])).SetValue(subctrl, true, null);
						else
						{
							// procurar uma property "Enabled"
							memberInfos = subctrl.GetType().GetMember("Enabled");
							if (memberInfos.Length > 0)
								((PropertyInfo)(memberInfos[0])).SetValue(subctrl, false, null);
						}
					}
					catch (Exception ex)
					{
						Trace.WriteLine("Control deactivation canceled" + System.Environment.NewLine + ex.ToString());
					}
					//End If
				}
				else
					makeReadOnly(subctrl);
			}
		}

		public static void makeReadable(Control ctrl)
		{
			foreach (Control subctrl in ctrl.Controls)
			{
				// se o controlo não se tratar de uma folha, avança-se para os seus filhos
				if (subctrl.Controls.Count == 0)
				{
					//tratar as listviews de forma diferente para que permaneçam scrollable
					try
					{
						// procurar uma property "Readonly"
						MemberInfo[] memberInfos = subctrl.GetType().GetMember("ReadOnly");
						if (memberInfos.Length > 0)
							((PropertyInfo)(memberInfos[0])).SetValue(subctrl, false, null);
						else
						{
							// procurar uma property "Enabled"
							memberInfos = subctrl.GetType().GetMember("Enabled");
							if (memberInfos.Length > 0)
								((PropertyInfo)(memberInfos[0])).SetValue(subctrl, true, null);
						}
					}
					catch (Exception ex)
					{
						Trace.WriteLine("Control deactivation canceled" + System.Environment.NewLine + ex.ToString());
					}
				}
				else
					makeReadable(subctrl);
			}
		}

		// Limpa os databindings e os conteúdos do campo passado. No caso das comboboxes é selecionado o 1º item caso exista
		public static void clearField(Control ctrl)
		{
			ctrl.DataBindings.Clear();
			if (ctrl is TextBox)
				((TextBox)ctrl).Clear();
			else if (ctrl is CheckBox)
				((CheckBox)ctrl).Checked = false;
			else if (ctrl is ComboBox)
			{
				ComboBox combo = (ComboBox)ctrl;
				if (combo.Items.Count > 0)
					combo.SelectedIndex = 0;
				else
					combo.SelectedIndex = -1;
			}
			else if (ctrl is GISA.Controls.PxDateBox)
			{
				GISA.Controls.PxDateBox dateBox = null;
				dateBox = (GISA.Controls.PxDateBox)ctrl;
				dateBox.ValueYear = string.Empty;
				dateBox.ValueMonth = string.Empty;
				dateBox.ValueDay = string.Empty;
			}
			else if (ctrl is ListView)
			{
				((ListView)ctrl).SuspendLayout();
				((ListView)ctrl).SelectedItems.Clear();
				((ListView)ctrl).Items.Clear();
				((ListView)ctrl).ResumeLayout();
			}
			else if (ctrl is TreeView)
				((TreeView)ctrl).Nodes.Clear();
			else if (ctrl is GISA.Controls.PxDecimalBox)
				((GISA.Controls.PxDecimalBox)ctrl).Clear();
			else if (ctrl is ListBox)
				((ListBox)ctrl).Items.Clear();
			else if (ctrl is CheckedListBox)
				DeactivateCheckedListBox((CheckedListBox)ctrl);
		}

		private static void DeactivateCheckedListBox(CheckedListBox ListBox)
		{
			int i = 0;
			for (i = 0; i < ListBox.Items.Count; i++)
			{
				if (ListBox is CheckedListBox)
					((CheckedListBox)ListBox).SetItemChecked(i, false);
			}
		}

		public static string ClipText(string OriginalText)
		{
			string Text = OriginalText;
			if (Text.IndexOf(System.Environment.NewLine) >= 0)
				Text = Text.Substring(0, Text.IndexOf(System.Environment.NewLine));

			Text = Text.Substring(0, Math.Min(100, Text.Length));
			if (Text.Length < OriginalText.Length)
				Text = Text + " ...";

			return Text;
		}

	#region  Tratamento de datas 
		public static string FormatStartDate(GISA.Model.GISADataset.ControloAutRelRow carRow)
		{
			return FormatStart((DataRow)carRow);
		}

		public static string FormatEndDate(GISA.Model.GISADataset.ControloAutRelRow carRow)
		{
			return FormatEnd((DataRow)carRow);
		}

		public static string FormatStartDate(GISA.Model.GISADataset.RelacaoHierarquicaRow rhRow)
		{
			return FormatStart((DataRow)rhRow);
		}

		public static string FormatEndDate(GISA.Model.GISADataset.RelacaoHierarquicaRow rhRow)
		{
			return FormatEnd((DataRow)rhRow);
		}

		public static string FormatStartDate(GISA.Model.GISADataset.SFRDDatasProducaoRow dpRow)
		{
			return FormatStart((DataRow)dpRow, dpRow.InicioAtribuida);
		}

		public static string FormatEndDate(GISA.Model.GISADataset.SFRDDatasProducaoRow dpRow)
		{
			return FormatEnd((DataRow)dpRow, dpRow.FimAtribuida);
		}

		public static string FormatStartDate(GISA.Model.GISADataset.ControloAutDatasExistenciaRow cadeRow)
		{
			return FormatStart((DataRow)cadeRow, cadeRow.InicioAtribuida);
		}

		public static string FormatEndDate(GISA.Model.GISADataset.ControloAutDatasExistenciaRow cadeRow)
		{
			return FormatEnd((DataRow)cadeRow, cadeRow.FimAtribuida);
		}


		private static string FormatStart(DataRow row)
		{
			return FormatStart(row, false);
		}

        private static string FormatStart(DataRow row, bool isAtribuida)
		{
			string ano = GISA.Model.GisaDataSetHelper.GetDBNullableText(row, "InicioAno");
			string mes = GISA.Model.GisaDataSetHelper.GetDBNullableText(row, "InicioMes");
			string dia = GISA.Model.GisaDataSetHelper.GetDBNullableText(row, "InicioDia");
			return GISA.Utils.GUIHelper.FormatDate(ano, mes, dia, isAtribuida);
		}


		private static string FormatEnd(DataRow row)
		{
			return FormatEnd(row, false);
		}

		private static string FormatEnd(DataRow row, bool isAtribuida)
		{
			string ano = GISA.Model.GisaDataSetHelper.GetDBNullableText(row, "FimAno");
			string mes = GISA.Model.GisaDataSetHelper.GetDBNullableText(row, "FimMes");
			string dia = GISA.Model.GisaDataSetHelper.GetDBNullableText(row, "FimDia");
			return GISA.Utils.GUIHelper.FormatDate(ano, mes, dia, isAtribuida);
		}

		public static string FormatDate(GISA.Model.GISADataset.ListaModelosAvaliacaoRow lstModAvRow)
		{
			string ano = lstModAvRow.DataInicio.Year.ToString();
			string mes = lstModAvRow.DataInicio.Month.ToString();
			string dia = lstModAvRow.DataInicio.Day.ToString();
			return GISA.Utils.GUIHelper.FormatDate(ano, mes, dia);
		}

		public static string FormatDateInterval(GISA.Model.GISADataset.RelacaoHierarquicaRow rhRow)
		{
			return GISA.Utils.GUIHelper.FormatDateInterval(FormatStartDate(rhRow), FormatEndDate(rhRow));
		}

		public static string FormatDateInterval(GISA.Model.GISADataset.SFRDDatasProducaoRow dpRow)
		{
			return GISA.Utils.GUIHelper.FormatDateInterval(FormatStartDate(dpRow), FormatEndDate(dpRow));
		}

		public static string FormatDateInterval(GISA.Model.GISADataset.ControloAutDatasExistenciaRow cadeRow)
		{
			return GISA.Utils.GUIHelper.FormatDateInterval(FormatStartDate(cadeRow), FormatEndDate(cadeRow));
		}

		//Public Shared Function FormatDateInterval(ByVal startDate As String, ByVal endDate As String) As String
		//    Return String.Format("{0} - {1}", startDate, endDate)
		//End Function

	

		//Public Shared Function FormatDate(ByVal ano As String, ByVal mes As String, ByVal dia As String, Optional ByVal isAtribuida As Boolean = False) As String
		//    If ano.Length = 0 Then ano = "    "
		//    If mes.Length = 0 Then mes = "  "
		//    If dia.Length = 0 Then dia = "  "
		//    If isAtribuida Then
		//        Return String.Format("[{0}/{1}/{2}]", ano, mes, dia)
		//    Else
		//        Return String.Format("{0}/{1}/{2}", ano, mes, dia)
		//    End If
		//End Function

		//Public Shared Function ReadYear(ByVal str As String) As String
		//    ' ToDo: passar a usar expressoes regulares, supondo que será mais eficiente
		//    If str.Equals("????") OrElse str.Equals("???") OrElse str.Equals("??") OrElse str.Equals("?") Then
		//        Return ""
		//    Else
		//        Return str
		//    End If
		//End Function

		//Public Shared Function ReadMonth(ByVal str As String) As String
		//    ' ToDo: passar a usar expressoes regulares, supondo que será mais eficiente
		//    If str.Equals("??") OrElse str.Equals("?") Then
		//        Return ""
		//    Else
		//        Return str
		//    End If
		//End Function

		//Public Shared Function ReadDay(ByVal str As String) As String
		//    ' ToDo: passar a usar expressoes regulares, supondo que será mais eficiente
		//    If str.Equals("??") OrElse str.Equals("?") Then
		//        Return ""
		//    Else
		//        Return str
		//    End If
		//End Function

		public static void populateDataInicio(GISA.Controls.PxDateBox dateControl, GISA.Model.GISADataset.SFRDDatasProducaoRow dpRow)
		{
			populateData(dateControl, dpRow, "Inicio");
		}

		public static void populateDataFim(GISA.Controls.PxDateBox dateControl, GISA.Model.GISADataset.SFRDDatasProducaoRow dpRow)
		{
			populateData(dateControl, dpRow, "Fim");
		}


		public static void populateData(GISA.Controls.PxDateBox dateControl, GISA.Model.GISADataset.SFRDDatasProducaoRow dpRow)
		{
			populateData(dateControl, dpRow, "Inicio");
		}

//INSTANT C# NOTE: C# does not support optional parameters. Overloaded method(s) are created above.
//ORIGINAL LINE: Public Shared Sub populateData(ByVal dateControl As GISA.Controls.PxDateBox, ByVal dpRow As GISA.Model.GISADataset.SFRDDatasProducaoRow, Optional ByVal extremo As String = "Inicio")
		public static void populateData(GISA.Controls.PxDateBox dateControl, GISA.Model.GISADataset.SFRDDatasProducaoRow dpRow, string extremo)
		{

			genericPopulateData(dateControl, GISA.Model.GisaDataSetHelper.GetDBNullableText(dpRow, string.Format("{0}Ano", extremo)), GISA.Model.GisaDataSetHelper.GetDBNullableText(dpRow, string.Format("{0}Mes", extremo)), GISA.Model.GisaDataSetHelper.GetDBNullableText(dpRow, string.Format("{0}Dia", extremo)));
		}

		public static void populateData(GISA.Controls.PxDateBox dateControl, GISA.Model.GISADataset.ListaModelosAvaliacaoRow lstModAvRow)
		{

			genericPopulateData(dateControl, lstModAvRow.DataInicio.Year.ToString(), lstModAvRow.DataInicio.Month.ToString(), lstModAvRow.DataInicio.Day.ToString());
		}

		private static void genericPopulateData(GISA.Controls.PxDateBox dateControl, string ano, string mes, string dia)
		{

			dateControl.ValueYear = ano;
			dateControl.ValueMonth = mes;
			dateControl.ValueDay = dia;
		}

		public static void storeDataInicio(GISA.Controls.PxDateBox dateControl, GISA.Model.GISADataset.SFRDDatasProducaoRow dpRow)
		{
			storeData(dateControl, dpRow, "Inicio");
		}

		public static void storeDataFim(GISA.Controls.PxDateBox dateControl, GISA.Model.GISADataset.SFRDDatasProducaoRow dpRow)
		{
			storeData(dateControl, dpRow, "Fim");
		}


		private static void storeData(GISA.Controls.PxDateBox dateControl, GISA.Model.GISADataset.SFRDDatasProducaoRow dpRow)
		{
			storeData(dateControl, dpRow, "Inicio");
		}

//INSTANT C# NOTE: C# does not support optional parameters. Overloaded method(s) are created above.
//ORIGINAL LINE: Private Shared Sub storeData(ByVal dateControl As GISA.Controls.PxDateBox, ByVal dpRow As GISA.Model.GISADataset.SFRDDatasProducaoRow, Optional ByVal extremo As String = "Inicio")
		private static void storeData(GISA.Controls.PxDateBox dateControl, GISA.Model.GISADataset.SFRDDatasProducaoRow dpRow, string extremo)
		{
			if (! (dateControl.ValueYear.Equals(GISA.Model.GisaDataSetHelper.GetDBNullableText(dpRow, string.Format("{0}Ano", extremo)))) || ! (dateControl.ValueMonth.Equals(GISA.Model.GisaDataSetHelper.GetDBNullableText(dpRow, string.Format("{0}Mes", extremo)))) || ! (dateControl.ValueDay.Equals(GISA.Model.GisaDataSetHelper.GetDBNullableText(dpRow, string.Format("{0}Dia", extremo)))))
			{

				if (dateControl.ValueYear.Length > 0)
				{
					dpRow[string.Format("{0}Ano", extremo)] = dateControl.ValueYear;
				}
				else
				{
					dpRow[string.Format("{0}Ano", extremo)] = DBNull.Value;
				}
				if (dateControl.ValueMonth.Length > 0)
				{
					dpRow[string.Format("{0}Mes", extremo)] = dateControl.ValueMonth;
				}
				else
				{
					dpRow[string.Format("{0}Mes", extremo)] = DBNull.Value;
				}
				if (dateControl.ValueDay.Length > 0)
				{
					dpRow[string.Format("{0}Dia", extremo)] = dateControl.ValueDay;
				}
				else
				{
					dpRow[string.Format("{0}Dia", extremo)] = DBNull.Value;
				}
			}
		}

		public static void storeData(GISA.Controls.PxDateBox dateControl, GISA.Model.GISADataset.ListaModelosAvaliacaoRow lstModAvRow)
		{

			lstModAvRow.DataInicio = System.DateTime.Parse(dateControl.ValueDate);
		}

		public static System.DateTime GetData(GISA.Controls.PxDateBox dateControl)
		{
			return System.DateTime.Parse(dateControl.ValueDate);
		}
	#endregion

	#region  Tratamento de dimensões 
        public static string FormatDimensoes(GISA.Model.GISADataset.SFRDUFDescricaoFisicaRow dfRow)
		{
            bool hasLargura = false;
			bool hasAltura = false;
			bool hasProfundidade = false;
			System.Text.StringBuilder dimensoes = new System.Text.StringBuilder();

            if (!dfRow.IsMedidaAlturaNull())
            {
                dimensoes.AppendFormat("{0:0.000}", dfRow.MedidaAltura);
                hasAltura = true;
            }
            else
                dimensoes.Append("?");

			if (! dfRow.IsMedidaLarguraNull())
			{
				dimensoes.AppendFormat(" x {0:0.000}", dfRow.MedidaLargura);
				hasLargura = true;
			}
			else
                dimensoes.Append(" x ?");

			if (! dfRow.IsMedidaProfundidadeNull())
			{
				dimensoes.AppendFormat(" x {0:0.000}", dfRow.MedidaProfundidade);
				hasProfundidade = true;
			}
            else
                dimensoes.Append(" x ?");

			if ((hasLargura || hasAltura || hasProfundidade) && ! dfRow.IsIDTipoMedidaNull())
				dimensoes.AppendFormat(" {0}", dfRow.TipoMedidaRow.Designacao);

			return dimensoes.ToString();
		}
        public static string FormatDimensoes(decimal? altura, decimal? largura, decimal? profundidade, string tipoMedida)
        {
            bool hasLargura = false;
            bool hasAltura = false;
            bool hasProfundidade = false;
            System.Text.StringBuilder dimensoes = new System.Text.StringBuilder();

            if (altura.HasValue)
            {
                dimensoes.AppendFormat("{0:0.000}", altura.Value);
                hasAltura = true;
            }
            else
                dimensoes.Append("?");

            if (largura.HasValue)
            {
                dimensoes.AppendFormat(" x {0:0.000}", largura.Value);
                hasLargura = true;
            }
            else
                dimensoes.Append(" x ?");

            if (profundidade.HasValue)
            {
                dimensoes.AppendFormat(" x {0:0.000}", profundidade.Value);
                hasProfundidade = true;
            }
            else
                dimensoes.Append(" x ?");

            if ((hasLargura || hasAltura || hasProfundidade) && tipoMedida != null && tipoMedida.Length > 0)
                dimensoes.AppendFormat(" {0}", tipoMedida);

            return dimensoes.ToString();
        }
	#endregion

		private static string porAvaliar = "Por avaliar";
		private static string conservacao = "Conservação";
		private static string eliminacao = "Eliminação";
		public static string formatDestinoFinal(GISA.Model.GISADataset.SFRDAvaliacaoRow sfrdAvaliacaoRow)
		{
			if (sfrdAvaliacaoRow.IsPreservarNull())
				return porAvaliar;
			else if (sfrdAvaliacaoRow.Preservar)
				return conservacao;
			else
				return eliminacao;
		}

		public static string formatDestinoFinal(GISA.Model.GISADataset.ModelosAvaliacaoRow modAvRow)
		{
			if (modAvRow.IsPreservarNull())
				return porAvaliar;
			else if (modAvRow.Preservar)
				return conservacao;
			else
				return eliminacao;
		}

        public static string formatDestinoFinal(string destFinal)
        {
            if (destFinal == null || destFinal == string.Empty)
                return porAvaliar;
            else if (destFinal.Equals("1"))
                return conservacao;
            else
                return eliminacao;
        }

		public static string formatAutoEliminacao(GISA.Model.GISADataset.SFRDAvaliacaoRow sfrdAvaliacaoRow)
		{
			if (sfrdAvaliacaoRow.IsIDAutoEliminacaoNull())
				return string.Empty;
			else
				return sfrdAvaliacaoRow.AutoEliminacaoRow.Designacao;
		}

		public static string getApplicationVersionHeader()
		{
			return string.Format("GISA - Versão {0} {1} de {2}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Trim(), " SVN:" + Application.ProductVersion, new System.IO.FileInfo(Application.ExecutablePath).LastWriteTime.ToShortDateString());
		}

		//método responsável por apresentar a mensagem correspondente ao erro que eventualmente ocorreu durante o processo de login
		public static void MessageBoxLoginErrorMessages(int Index)
		{
            switch ((DBAbstractDataLayer.DataAccessRules.TrusteeRule.IndexErrorMessages)Index)
			{                    
                case DBAbstractDataLayer.DataAccessRules.TrusteeRule.IndexErrorMessages.CleaningRowsError:
					MessageBox.Show("Ocorreu um erro com a manutenção da informação na base de dados.", "Gisa", MessageBoxButtons.OK, MessageBoxIcon.Error);

					break;
                case DBAbstractDataLayer.DataAccessRules.TrusteeRule.IndexErrorMessages.InvalidUser:
					MessageBox.Show("O nome de utilizador ou a palavra-chave são inválidos.", "Gisa", MessageBoxButtons.OK, MessageBoxIcon.Warning);

					break;
				case DBAbstractDataLayer.DataAccessRules.TrusteeRule.IndexErrorMessages.InactiveUser:
					MessageBox.Show("O seu nome de utilizador encontra-se inativo, por favor " + Environment.NewLine + "contacte o administrador de sistema.", "Utilizador inativo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

					break;
                case DBAbstractDataLayer.DataAccessRules.TrusteeRule.IndexErrorMessages.UserWithoutPermissions:
					MessageBox.Show("O seu nome de utilizador não tem atribuídas permissões de acesso, " + Environment.NewLine + "por favor contacte o administrador de sistema.", "Utilizador sem permissões", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					break;
			}
		}

	#region  Validação de campos 

		private static System.Text.RegularExpressions.Regex codigoValidatorGeneric = null;
		private static System.Text.RegularExpressions.Regex codigoValidatorED = null;
		private static System.Text.RegularExpressions.Regex codigoValidatorDoc = null;
		public static bool CheckValidCodigoParcial(string codigo)
		{
			if (codigoValidatorGeneric == null)
				codigoValidatorGeneric = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9\\.\\:\\,\\;]+$", System.Text.RegularExpressions.RegexOptions.Compiled);

		    return codigoValidatorGeneric.Match(codigo).Success;
		}

        public static bool CheckValidCodigoParcialForTipo(string codigo, long IDTipoNivelRelacionado)
        {
            if (IDTipoNivelRelacionado == GISA.Model.TipoNivelRelacionado.ED)
            {
                if (codigoValidatorED == null)
                    codigoValidatorED = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9\\-]+$", System.Text.RegularExpressions.RegexOptions.Compiled);

                return codigoValidatorED.Match(codigo).Success;
            }
            else if (IDTipoNivelRelacionado == GISA.Model.TipoNivelRelacionado.D || IDTipoNivelRelacionado == GISA.Model.TipoNivelRelacionado.SD)
            {
                if (codigoValidatorDoc == null)
                    codigoValidatorDoc = new System.Text.RegularExpressions.Regex("^[^ ]+$", System.Text.RegularExpressions.RegexOptions.Compiled);

                return codigoValidatorDoc.Match(codigo).Success;
            }
            else
                return CheckValidCodigoParcial(codigo);
        }
	#endregion
	}
}