using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

using GISA.Model;
using System.Data.Common;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

using GISA.Controls;
using GISA.Controls.ControloAut;
using GISA.GUIHelper;

namespace GISA
{
	public class FormPickDiplomaModelo : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormPickDiplomaModelo() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            caList.BeforeNewListSelection += caList_BeforeNewListSelection;
            ToolBar1.ButtonClick += ToolBarButtonClickEvent;

            //caList.lstVwControloAut.Columns.Remove(caList.colValidado);
            //caList.lstVwControloAut.Columns.Remove(caList.colCompleto);
            //caList.lstVwControloAut.Columns.Remove(caList.colNoticiaAut);
            //caList.colDesignacao.Width = caList.lstVwControloAut.Width - 4;
            //caList.chkValidado.Visible = false;
            //caList.cbNoticiaAut.Visible = false;
            //caList.lblNoticiaAut.Visible = false;
            //caList.txtFiltroDesignacao.Width = caList.lstVwControloAut.Width - 42;
            //caList.grpFiltro.Visible = false;
            //caList.Enabled = false;
            //caList.originalLabel = "Registos encontrados";
            //caList.ListHandler = new ControloAutList.DefaultCAListHandler(caList);
            caList.ConfigureDiplomaModelo();
            caList.KeyDown += new KeyEventHandler(lstVwControloAut_KeyDown);

			GetExtraResources();

			UpdateButtonState(null);
		}

        void lstVwControloAut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk.PerformClick();
        }

		//Form overrides dispose to clean up the component list.
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
		internal ControloAutList caList;
		internal System.Windows.Forms.ToolBar ToolBar1;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonEdit;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonNew;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonDelete;
		internal System.Windows.Forms.ToolBarButton ToolBarButton2;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonFilter;
		internal System.Windows.Forms.Button btnOk;
		internal System.Windows.Forms.Button btnCancel;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.caList = new ControloAutList();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ToolBar1 = new System.Windows.Forms.ToolBar();
            this.ToolBarButtonNew = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonEdit = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonDelete = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonFilter = new System.Windows.Forms.ToolBarButton();
            this.SuspendLayout();
            // 
            // caList
            // 
            this.caList.AllowedNoticiaAutLocked = false;
            this.caList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            //this.caList.ListHandler = null;
            this.caList.Location = new System.Drawing.Point(0, 26);
            this.caList.Name = "caList";
            //this.caList.originalLabel = "Notícias de autoridade encontradas";
            this.caList.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.caList.Size = new System.Drawing.Size(568, 264);
            this.caList.TabIndex = 3;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(376, 300);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Aceitar";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(464, 300);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancelar";
            // 
            // ToolBar1
            // 
            this.ToolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.ToolBar1.AutoSize = false;
            this.ToolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.ToolBarButtonNew,
            this.ToolBarButtonEdit,
            this.ToolBarButtonDelete,
            this.ToolBarButton2,
            this.ToolBarButtonFilter});
            this.ToolBar1.DropDownArrows = true;
            this.ToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ToolBar1.Name = "ToolBar1";
            this.ToolBar1.ShowToolTips = true;
            this.ToolBar1.Size = new System.Drawing.Size(568, 26);
            this.ToolBar1.TabIndex = 6;
            // 
            // ToolBarButtonNew
            // 
            this.ToolBarButtonNew.ImageIndex = 0;
            this.ToolBarButtonNew.Name = "ToolBarButtonNew";
            // 
            // ToolBarButtonEdit
            // 
            this.ToolBarButtonEdit.ImageIndex = 1;
            this.ToolBarButtonEdit.Name = "ToolBarButtonEdit";
            // 
            // ToolBarButtonDelete
            // 
            this.ToolBarButtonDelete.ImageIndex = 2;
            this.ToolBarButtonDelete.Name = "ToolBarButtonDelete";
            // 
            // ToolBarButton2
            // 
            this.ToolBarButton2.Name = "ToolBarButton2";
            this.ToolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButtonFilter
            // 
            this.ToolBarButtonFilter.ImageIndex = 3;
            this.ToolBarButtonFilter.Name = "ToolBarButtonFilter";
            this.ToolBarButtonFilter.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // FormPickDiplomaModelo
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(568, 333);
            this.ControlBox = false;
            this.Controls.Add(this.caList);
            this.Controls.Add(this.ToolBar1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.MaximizeBox = false;
            this.Name = "FormPickDiplomaModelo";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Diplomas e modelos";
            this.ResumeLayout(false);

		}

	#endregion

		private void GetExtraResources()
		{
			ToolBar1.ImageList = SharedResourcesOld.CurrentSharedResources.DMManipulacaoImageList;

			string[] strs = SharedResourcesOld.CurrentSharedResources.DMManipulacaoStrings;
			ToolBarButtonNew.ToolTipText = strs[0];
			ToolBarButtonEdit.ToolTipText = strs[1];
			ToolBarButtonDelete.ToolTipText = strs[2];
			ToolBarButtonFilter.ToolTipText = strs[3];
		}

		private void DoBackgroundLoad()
		{
			caList.ReloadList();
			caList.Enabled = true;
		}

		private void UpdateButtonState(ListViewItem item)
		{
			ToolBarButtonEdit.Enabled = (item != null && item.ListView != null);
			ToolBarButtonDelete.Enabled = (item != null && item.ListView != null);
			btnOk.Enabled = (item != null && item.ListView != null);
		}

		private void caList_BeforeNewListSelection(object sender, BeforeNewSelectionEventArgs e)
		{
			UpdateButtonState(e.ItemToBeSelected);
		}

	#region  Event handlers for inherited ToolBar 

		private void ToolBarButtonClickEvent(object sender, ToolBarButtonClickEventArgs e)
		{
			// obter o tipo de noticia em causa
			GISADataset.TipoNoticiaAutRow tnaRow = null;
			//tnaRow = DirectCast(GisaDataSetHelper.GetInstance().TipoNoticiaAut.Select("ID=" + DomainValuesHelper.stringifyEnumValue(TipoNoticiaAut.Diploma))(0), GISADataset.TipoNoticiaAutRow)
			tnaRow = (GISADataset.TipoNoticiaAutRow)caList.cbNoticiaAut.SelectedItem;
			if (tnaRow.ID == -1 && caList.cbNoticiaAut.Items.Count == 2)
			{
				tnaRow = (GISADataset.TipoNoticiaAutRow)(caList.cbNoticiaAut.Items[1]);
			}

			Trace.Assert(tnaRow != null);
			Trace.Assert(! (tnaRow.ID == -1));

			if (e.Button == ToolBarButtonNew)
			{
				FormCreateDiplomaModelo form = new FormCreateDiplomaModelo();
				if (tnaRow.ID == Convert.ToInt64(TipoNoticiaAut.Diploma))
				{
					form.Text = "Criar diploma";
				}
				else if (tnaRow.ID == Convert.ToInt64(TipoNoticiaAut.Modelo))
				{
					form.Text = "Criar modelo";
				}
				switch (form.ShowDialog())
				{
					case System.Windows.Forms.DialogResult.OK:

						//adicionar o novo controlo de autoridade ao modelo de dados e à interface
						GISADataset.ControloAutRow caRow = null;
						caRow = GisaDataSetHelper.GetInstance().ControloAut.NewControloAutRow();
						caRow.Autorizado = false;
						caRow.Completo = false;
						caRow.IDTipoNoticiaAut = tnaRow.ID;
						caRow.NotaExplicativa = "";
						caRow["IDIso639p2"] = DBNull.Value;
						caRow["IDIso15924"] = DBNull.Value;
						caRow.ChaveColectividade = "";
						caRow.ChaveRegisto = "";
						caRow.RegrasConvencoes = "";
						caRow.Observacoes = "";
						caRow.DescContextoGeral = "";
						caRow.DescEnquadramentoLegal = "";
						caRow.DescEstatutoLegal = "";
						caRow.DescEstruturaInterna = "";
						caRow.DescOcupacoesActividades = "";
						caRow.DescHistoria = "";
						caRow.DescOutraInformacaoRelevante = "";
						caRow.DescZonaGeografica = "";

						try
						{
							GisaDataSetHelper.GetInstance().ControloAut.AddControloAutRow(caRow);
							PersistencyHelper.ManageFormasAutorizadasPreConcArguments args = new PersistencyHelper.ManageFormasAutorizadasPreConcArguments();
							args.termo = form.Designacao.Trim().Replace("'", "''");
							args.caRowID = caRow.ID;

							PersistencyHelper.save(setNewTermo, args);
							PersistencyHelper.cleanDeletedData();

							caList.cbNoticiaAut.SelectedItem = tnaRow;
							caList.ReloadList();

							if (args.message.Length > 0)
							{
								MessageBox.Show(args.message, form.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
							}

							if (args.cadRow != null)
                                caList.SelectItem(args.cadRow);
						}
						catch (Exception ex)
						{
							Trace.WriteLine(ex);
							throw;
						}

						break;
					case System.Windows.Forms.DialogResult.Cancel:

					break;
					default:

					break;
				}
			}
			else if (e.Button == ToolBarButtonEdit)
			{
				GISADataset.ControloAutDicionarioRow cad = null;
				GISADataset.DicionarioRow d = null;
				cad = (GISADataset.ControloAutDicionarioRow)(caList.SelectedItems[0].Tag);
				Debug.Assert(cad.IDTipoControloAutForma == Convert.ToInt64(TipoControloAutForma.FormaAutorizada));
				d = cad.DicionarioRow;

				FormCreateDiplomaModelo form = new FormCreateDiplomaModelo();
				if (tnaRow.ID == Convert.ToInt64(TipoNoticiaAut.Diploma))
				{
					form.Text = "Editar diploma";
				}
				else if (tnaRow.ID == Convert.ToInt64(TipoNoticiaAut.Modelo))
				{
					form.Text = "Editar modelo";
				}
				form.Designacao = d.Termo;
				switch (form.ShowDialog())
				{
					case System.Windows.Forms.DialogResult.OK:
						try
						{
							PersistencyHelper.ManageFormasAutorizadasPreConcArguments args = new PersistencyHelper.ManageFormasAutorizadasPreConcArguments();
							args.termo = form.Designacao.Trim().Replace("'", "''");
							args.caRowID = cad.ControloAutRow.ID;

							PersistencyHelper.save(setNewTermo, args);
							PersistencyHelper.cleanDeletedData();

							caList.ReloadList();

							if (args.message.Length > 0)
								MessageBox.Show(args.message, form.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

							if (args.cadRow != null)
                                caList.SelectItem(args.cadRow);
						}
						catch (Exception ex)
						{
							Trace.WriteLine(ex);
							throw;
						}
						break;
					case System.Windows.Forms.DialogResult.Cancel:

					break;
					default:

					break;
				}
			}
			else if (e.Button == ToolBarButtonDelete)
				DeleteControloAut(caList, "Eliminar diplomas/modelos", "Os items serão removidos apesar das associações existentes. Pretende continuar?", "Deseja apagar os elementos selecionados?");
			else if (e.Button == ToolBarButtonFilter)
                caList.FilterVisible = ToolBarButtonFilter.Pushed;
			else
				Debug.Assert(false, "Unexpected button clicked in ToolBar.");
		}

		private void setNewTermo(PersistencyHelper.PreConcArguments args)
		{
			try
			{
				PersistencyHelper.ManageFormasAutorizadasPreConcArguments mfaPca = (PersistencyHelper.ManageFormasAutorizadasPreConcArguments)args;
				string termo = mfaPca.termo;
				GISADataset.ControloAutRow carow = (GISADataset.ControloAutRow)(GisaDataSetHelper.GetInstance().ControloAut.Select("ID=" + mfaPca.caRowID.ToString())[0]);
				GISADataset.TipoControloAutFormaRow tcafRowAutorizado = (GISADataset.TipoControloAutFormaRow)(GisaDataSetHelper.GetInstance().TipoControloAutForma.Select("ID=" + System.Enum.Format(typeof(TipoControloAutForma), TipoControloAutForma.FormaAutorizada, "D"))[0]);
				DataSet gisaBackup = mfaPca.gisaBackup;
				//Dim dataReader As IDataReader
				GISADataset.DicionarioRow dRowOrig = null;
				GISADataset.ControloAutDicionarioRow cadRowOrig = ControloAutHelper.getFormaAutorizada(carow);
				bool existsOrigCad = false;

				ArrayList termoUsed = DiplomaModeloRule.Current.GetDicionario(getCatCode(carow.TipoNoticiaAutRow), termo.Trim(), mfaPca.tran);

				// Distinguir entre criar e editar um Diploma/Modelo
				if (carow.ID < 0)
				{
					// Criar Diploma/Modelo:
					//  - verificar se o termo escolhido existe na base de dados e se nesse caso, verificar se
					//    está a ser utilizado noutro Diploma/Modelo
					if (((long)(termoUsed[0])) > 0 && (bool)(termoUsed[1]))
					{
						// A designação já existe na base de dados mas está marcada como apagada
						mfaPca.cadRow = manageDesignacaoDiplomaModelo(mfaPca.cadRow, mfaPca.termo, carow.TipoNoticiaAutRow, carow, tcafRowAutorizado);
					}
					else if (((long)(termoUsed[0])) > 0 && ! ((bool)(termoUsed[1])))
					{
						// A designação já existe na base de dados mas não está marcada como apagada; no entanto, 
						// é preciso saber se está a ser usada por outro Diploma/Modelo (tipicamente, nesta 
						// situação a designação está a ser utilizada por um Diploma/Modelo mas já aconteceu não 
						// estar a ser utilizada fruto de uma resolução de conflito de concorrência)
						if (DiplomaModeloRule.Current.ExistsControloAutDicionario((long)(termoUsed[0]), 1, carow.TipoNoticiaAutRow.ID, mfaPca.tran))
						{
							carow.RejectChanges();
							mfaPca.message = "A designação especificada já existe, deverá escolhê-la da lista caso a pretenda utilizar.";
						}
						else
						{
							mfaPca.cadRow = manageDesignacaoDiplomaModelo(mfaPca.cadRow, mfaPca.termo, carow.TipoNoticiaAutRow, carow, tcafRowAutorizado);
						}
					}
					else
					{
						// A designação não existe na base de dados
						mfaPca.cadRow = manageDesignacaoDiplomaModelo(mfaPca.cadRow, mfaPca.termo, carow.TipoNoticiaAutRow, carow, tcafRowAutorizado);
					}
				}
				else
				{
					// Editar Diploma/Modelo
					//  - verificar se entretanto outro utilizador já editou o Diploma/Modelo
					//  - verificar se o termo escolhido existe na base de dados e se nesse caso, verificar se
					//    está a ser utilizado noutro Diploma/Modelo
					existsOrigCad = DiplomaModeloRule.Current.ExistsControloAutDicionario(cadRowOrig.IDDicionario, cadRowOrig.IDTipoControloAutForma, carow.TipoNoticiaAutRow.ID, mfaPca.tran);

					dRowOrig = cadRowOrig.DicionarioRow;

					PersistencyHelper.BackupRow(ref gisaBackup, cadRowOrig);
					PersistencyHelper.BackupRow(ref gisaBackup, dRowOrig);

					cadRowOrig.Delete();
					// é permitido apagar o termo antigo uma vez que é único para diplomas/modelo
					dRowOrig.Delete();

					if (existsOrigCad)
					{
						// o Diploma/Modelo não foi editado por nenhum outro utilizador
						if (((long)(termoUsed[0])) > 0 && (bool)(termoUsed[1]))
						{
							// A designação já existe na base de dados mas está marcada como apagada
							mfaPca.cadRow = manageDesignacaoDiplomaModelo(mfaPca.cadRow, mfaPca.termo, carow.TipoNoticiaAutRow, carow, tcafRowAutorizado);
						}
						else if (((long)(termoUsed[0])) > 0 && ! ((bool)(termoUsed[1])))
						{
							// A designação já existe na base de dados mas não está marcada como apagada; no entanto, 
							// é preciso saber se está a ser usada por outro Diploma/Modelo (tipicamente, nesta 
							// situação a designação está a ser utilizada por um Diploma/Modelo mas já aconteceu não 
							// estar a ser utilizada fruto de uma resolução de conflito de concorrência)
							if (DiplomaModeloRule.Current.ExistsControloAutDicionario((long)(termoUsed[0]), 1, carow.TipoNoticiaAutRow.ID, mfaPca.tran))
							{
								dRowOrig.RejectChanges();
								cadRowOrig.RejectChanges();
								mfaPca.message = "A designação especificada já existe, deverá escolhê-la da lista caso a pretenda utilizar.";
							}
							else
							{
								mfaPca.cadRow = manageDesignacaoDiplomaModelo(mfaPca.cadRow, mfaPca.termo, carow.TipoNoticiaAutRow, carow, tcafRowAutorizado);
							}
						}
						else
						{
							// A designação não existe na base de dados
							mfaPca.cadRow = manageDesignacaoDiplomaModelo(mfaPca.cadRow, mfaPca.termo, carow.TipoNoticiaAutRow, carow, tcafRowAutorizado);
						}
					}
					else
					{
						// Outro utilizador já editou este Diploma/Modelo pelo que não é possível reeditá-lo
						dRowOrig.RejectChanges();
						cadRowOrig.RejectChanges();
						mfaPca.message = "Não foi possível executar a operação pretendida pois o controlo de autoridade foi alterado por outro utilizador.";
					}
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw;
			}
		}

		// Método que trata da atribuição de uma designação a um Diploma/Modelo durante uma criação ou edição
		private GISADataset.ControloAutDicionarioRow manageDesignacaoDiplomaModelo(GISADataset.ControloAutDicionarioRow cadRow, string termo, GISADataset.TipoNoticiaAutRow TipoNoticiaAutRow, GISADataset.ControloAutRow carow, GISADataset.TipoControloAutFormaRow tcafRowAutorizado)
		{

			// NOTA: é criada na mesma uma linha Dicionario; posteriormente no algoritmo que trata os
			//       conflitos de concorrência vai proceder à reutilização da linha existente na
			//       base de dados
			if (cadRow != null)
			{
				// se por algum motivo a transacção onde este método está inserido voltar a ser executada,
				// mfaPca.cadRow já não será nothing e por esse motivo deverá ser apagada a existente 
				// em memória seja qual for o seu rowstate
				GisaDataSetHelper.GetInstance().ControloAutDicionario.RemoveControloAutDicionarioRow(cadRow);
				GisaDataSetHelper.GetInstance().Dicionario.RemoveDicionarioRow(cadRow.DicionarioRow);
			}
			else if (GisaDataSetHelper.GetInstance().Dicionario.Select("Termo='" + termo.Replace("'", "''") + "' AND CatCode='" + getCatCode(carow.TipoNoticiaAutRow) + "'").Length > 0)
			{
				GISADataset.DicionarioRow dicionarioRow = (GISADataset.DicionarioRow)(GisaDataSetHelper.GetInstance().Dicionario.Select("Termo='" + termo.Replace("'", "''") + "' AND CatCode='" + getCatCode(carow.TipoNoticiaAutRow) + "'")[0]);
				GisaDataSetHelper.GetInstance().Dicionario.RemoveDicionarioRow(dicionarioRow);
			}
			GISADataset.DicionarioRow dRow = GisaDataSetHelper.GetInstance().Dicionario.AddDicionarioRow(termo, getCatCode(carow.TipoNoticiaAutRow), new byte[]{}, 0);
			return GisaDataSetHelper.GetInstance().ControloAutDicionario.AddControloAutDicionarioRow(carow, dRow, tcafRowAutorizado, new byte[]{}, 0);
		}


		private static string getCatCode(GISADataset.TipoNoticiaAutRow tnaRow)
		{
			if (tnaRow.ID == Convert.ToInt64(TipoNoticiaAut.Diploma))
			{
				return "DP";
			}
			else if (tnaRow.ID == Convert.ToInt64(TipoNoticiaAut.Modelo))
			{
				return "MD";
			}
			else
			{
				return "CA";
			}
		}


		public static void DeleteControloAut(ControloAutList caList, string Caption, string Interrogacao, string MessageBoxText)
		{
			DeleteControloAut(caList, Caption, Interrogacao, MessageBoxText, null);
		}

//INSTANT C# NOTE: C# does not support optional parameters. Overloaded method(s) are created above.
//ORIGINAL LINE: Public Shared Sub DeleteControloAut(ByVal caList As ControloAutList, ByVal Caption As String, ByVal Interrogacao As String, ByVal MessageBoxText As String, Optional ByVal caller As Control = null)
		public static void DeleteControloAut(ControloAutList caList, string Caption, string Interrogacao, string MessageBoxText, Control caller)
		{

			bool HasDocumentData = false;
            List<string> nivelIDsAssoc = new List<string>();
			string Detalhes = null;
			
            if (caller != null) ((frmMain)caller.TopLevelControl).EnterWaitMode();
            Detalhes = ControloAutHelper.GetControloAutUsage(caList.SelectedItems.Cast<ListViewItem>().Select(i => i.Tag as GISADataset.ControloAutDicionarioRow).ToList(), ref HasDocumentData, ref nivelIDsAssoc);
            if (caller != null) ((frmMain)caller.TopLevelControl).LeaveWaitMode();

			if (Detalhes.Length > 0)
			{
                FormDeletionReport form = new FormDeletionReport();
                form.Text = Caption;
                form.Interrogacao = Interrogacao;
                form.Detalhes = Detalhes;
				if (HasDocumentData)
				{
                    form.SetBtnOKVisible(false);
                    form.Interrogacao = "Este elemento não pode ser removido enquanto lhe existirem associados níveis documentais.";
				}

                if (form.ShowDialog() == DialogResult.Cancel) return;
			}
			else
			{
				switch (MessageBox.Show(MessageBoxText, Caption, MessageBoxButtons.OKCancel))
				{
					case System.Windows.Forms.DialogResult.OK:
					break;
					case System.Windows.Forms.DialogResult.Cancel:
						return;
					default:
						Debug.Assert(false, "Unexpected DialogResult.");
						break;
				}
			}

			Trace.WriteLine("A apagar notícia de autoridade...");

			ListViewItem selectedItem = null;
			if (caList.SelectedItems.Count > 0)
			{
				Debug.Assert(caList.SelectedItems.Count == 1, "Só deveria existir 1 item selecionado.");
				selectedItem = caList.SelectedItems[0];

				GISADataset.ControloAutDicionarioRow cadRow = null;
				GISADataset.ControloAutRow caRow = null;
				cadRow = (GISADataset.ControloAutDicionarioRow)selectedItem.Tag;
				caRow = cadRow.ControloAutRow;

				// Remover a selecção do item vai provocar uma mudança de contexto que 
				// por sua vez vai provocar uma gravação dos dados
				caList.ClearItemSelection(selectedItem);

                var ho = default(GisaDataSetHelper.HoldOpen);
                if (caller != null && caller.GetType() == typeof(MasterPanelControloAut))
                {
                    ((MasterPanelControloAut)caller).CurrentContext.RaiseRegisterModificationEvent(caRow);

                    if (caRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.EntidadeProdutora)
                    {
                        var frdRow = default(GISADataset.FRDBaseRow);
                        long IDTipoNivelRelacionado = -1;
                        ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                        try
                        {
                            DBAbstractDataLayer.DataAccessRules.FRDRule.Current.LoadFRD(GisaDataSetHelper.GetInstance(), caRow.GetNivelControloAutRows().Single().ID, ho.Connection);
                            frdRow = caRow.GetNivelControloAutRows().Single().NivelRow.GetFRDBaseRows().SingleOrDefault();
                            DBAbstractDataLayer.DataAccessRules.NivelRule.Current.LoadNivelParents(caRow.GetNivelControloAutRows().Single().ID, GisaDataSetHelper.GetInstance(), ho.Connection);
                            if (frdRow != null)
                                IDTipoNivelRelacionado = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.GetNivelLastIDTipoNivelRelacionado(frdRow.ID, ho.Connection);
                        }
                        finally
                        {
                            ho.Dispose();
                        }

                        if (frdRow != null) // frd pode ainda não ter sido criado
                        {
                            var caRegRow = GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao.Cast<GISADataset.ControloAutDataDeDescricaoRow>().Single(r => r.RowState == DataRowState.Added);
                            var nvlRegRow = RecordRegisterHelper
                                            .CreateFRDBaseDataDeDescricaoRow(frdRow,
                                                caRegRow.TrusteeUserRowByTrusteeUserControloAutDataDeDescricao,
                                                caRegRow.TrusteeUserRowByTrusteeUserControloAutDataDeDescricaoAuthority,
                                                caRegRow.DataAutoria,
                                                IDTipoNivelRelacionado);
                            nvlRegRow.DataEdicao = caRegRow.DataEdicao;

                            GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao.AddFRDBaseDataDeDescricaoRow(nvlRegRow);
                        }
                    }
                }

				selectedItem.Remove();

                ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                List<long> lowerIDsList = new List<long>();
                try {
                    lowerIDsList = DiplomaModeloRule.Current.GetIDLowers(caRow.ID, ho.Connection);
                } 
                finally {
                    ho.Dispose();
                }

				// Como os dados acabaram de ser gravados pode ter-se chegado à conclusão que o contexto existente já não existia, daí este teste
				if (caRow.RowState != DataRowState.Detached)
				{
					try
					{
                        var oldTermo = caRow.GetControloAutDicionarioRows().Single(cad => cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada).DicionarioRow.Termo;
                        var IDTipoNoticiaAut = caRow.IDTipoNoticiaAut;
						PersistencyHelper.DeleteIDXPreSaveArguments preSaveArgs = new PersistencyHelper.DeleteIDXPreSaveArguments();
						GISADataset.NivelControloAutRow[] ncaRows = null;
						GISADataset.NivelRow nRow = null;
						ncaRows = caRow.GetNivelControloAutRows();
						if (ncaRows.Length > 0)
						{
							nRow = ((GISADataset.NivelControloAutRow)(ncaRows[0])).NivelRow;
							preSaveArgs.ID = nRow.ID;
						}
						PersistencyHelper.DeleteCAXPreConcArguments preConcArgs = DeleteCAX(caRow);
                        PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save(deleteCAXTermos, preConcArgs, Nivel.DeleteNivelXInDataBase, preSaveArgs);
						PersistencyHelper.cleanDeletedData();

                        if (successfulSave == PersistencyHelper.SaveResult.successful) {
                            if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsFedoraEnable())
                                SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.ActualizaObjDigitaisPorNivel(nivelIDsAssoc, oldTermo, null, IDTipoNoticiaAut);
                            Search.Updater.updateNivelDocumental(nivelIDsAssoc);
                            Search.Updater.updateNivelDocumentalComProdutores(lowerIDsList.Select(t => t.ToString()).ToList<string>());
                        }
					}
					catch (Exception ex)
					{
						Trace.WriteLine(ex);
						throw;
					}
				}
                else
                    GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao.Cast<GISADataset.ControloAutDataDeDescricaoRow>().Single(r => r.RowState == DataRowState.Added && r.IDControloAut == caRow.ID).RejectChanges();
			}
		}

		//metodo responsavel por eliminar toda a nuvem do CA actual em memoria
		public static PersistencyHelper.DeleteCAXPreConcArguments DeleteCAX(GISADataset.ControloAutRow caRow)
		{
			GISADataset.ControloAutDicionarioRow[] cadRows = caRow.GetControloAutDicionarioRows();
			GISADataset.DicionarioRow dRow = null;
			PersistencyHelper.DeleteCAXPreConcArguments args = new PersistencyHelper.DeleteCAXPreConcArguments();

			// eliminar registos de "Dicionario" e de "ControloAutDicionario"
            cadRows.ToList().ForEach(cadRow =>
            {
                dRow = cadRow.DicionarioRow;
                args.termos.Add(dRow);
                cadRow.Delete();
            });
			args.caRowID = caRow.ID;
			args.catCode = getCatCode(caRow.TipoNoticiaAutRow);

			// eliminar registos de IndexFRDCA
            caRow.GetIndexFRDCARows().ToList().ForEach(idx => idx.Delete());

			// Somente para notícias de autoridade relacionaveis
            caRow.GetControloAutRelRowsByControloAutControloAutRel().ToList().ForEach(carRow => carRow.Delete());
            caRow.GetControloAutRelRowsByControloAutControloAutRelAlias().ToList().ForEach(carRow => carRow.Delete());
            caRow.GetControloAutEntidadeProdutoraRows().ToList().ForEach(caepRow => caepRow.Delete());
            caRow.GetControloAutDatasExistenciaRows().ToList().ForEach(cadeRow => cadeRow.Delete());

			caRow.Delete();
			return args;
		}


		//metodo responsavel por eliminar todos os termos de um CA que não estejam a ser utilizados por outros
		public static void deleteCAXTermos(PersistencyHelper.PreConcArguments args)
		{
			long caRowID = ((PersistencyHelper.DeleteCAXPreConcArguments)args).caRowID;
			string catCode = ((PersistencyHelper.DeleteCAXPreConcArguments)args).catCode;
			DataSet gisaBackup = ((PersistencyHelper.DeleteCAXPreConcArguments)args).gisaBackup;
			foreach (GISADataset.DicionarioRow drow in ((PersistencyHelper.DeleteCAXPreConcArguments)args).termos)
			{
				//Nota: é necessário verificar se o estado de drow é deleted pelo facto de ser possivel, numa situação de concorência
				//entre clientes oracle (só neste tipo de clientes é que esta situação se verificou) que obriga a re-executar a transacção;
				//apesar de existir um método que repõe os valores originais, este só é executado após todos os delegates PreConc o que
				//neste caso concreto vai apagar as datarows da tabela Dicionario
				if (! (DiplomaModeloRule.Current.isTermoUsedByOthers(caRowID, catCode, drow.Termo, true, args.tran)))
				{
					PersistencyHelper.BackupRow(ref gisaBackup, drow);
					drow.Delete();
				}
			}
		}

	#endregion

		public void LoadData()
		{
			DoBackgroundLoad();
		}
	}

} //end of root namespace