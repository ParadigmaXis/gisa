using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class FormDomainValueEditor : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormDomainValueEditor() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            btnAdicionar.Click += btnAdicionar_Click;
            btnEditar.Click += btnEditar_Click;
            btnRemover.Click += btnRemover_Click;
            btnAplicar.Click += btnAplicar_Click;
            btnCancelar.Click += btnCancelar_Click;
            btnFechar.Click += btnFechar_Click;
            lstValores.SelectedIndexChanged += lstValores_SelectedIndexChanged;
            txtDesignacao.TextChanged += txtDesignacao_TextChanged;

			LeaveEditMode();
			lstValores.SelectedItem = null;
			if (lstValores.Items.Count > 0)
			{
				lstValores.SelectedIndex = 0;
			}
			if (dataRows == null)
			{
				grpValores.Enabled = false;
			}
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
		internal System.Windows.Forms.GroupBox grpValores;
		internal System.Windows.Forms.ListBox lstValores;
		internal System.Windows.Forms.GroupBox grpEdicao;
		internal System.Windows.Forms.TextBox txtDesignacao;
		internal System.Windows.Forms.Button btnAdicionar;
		internal System.Windows.Forms.Button btnEditar;
		internal System.Windows.Forms.Button btnRemover;
		internal System.Windows.Forms.Button btnFechar;
		internal System.Windows.Forms.Button btnAplicar;
		internal System.Windows.Forms.Button btnCancelar;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.btnFechar = new System.Windows.Forms.Button();
			this.grpValores = new System.Windows.Forms.GroupBox();
			this.lstValores = new System.Windows.Forms.ListBox();
			this.btnEditar = new System.Windows.Forms.Button();
			this.btnRemover = new System.Windows.Forms.Button();
			this.btnAdicionar = new System.Windows.Forms.Button();
			this.btnCancelar = new System.Windows.Forms.Button();
			this.grpEdicao = new System.Windows.Forms.GroupBox();
			this.btnAplicar = new System.Windows.Forms.Button();
			this.txtDesignacao = new System.Windows.Forms.TextBox();
			this.grpValores.SuspendLayout();
			this.grpEdicao.SuspendLayout();
			this.SuspendLayout();
			//
			//btnFechar
			//
			this.btnFechar.Location = new System.Drawing.Point(384, 288);
			this.btnFechar.Name = "btnFechar";
			this.btnFechar.TabIndex = 3;
			this.btnFechar.Text = "Fechar";
			//
			//grpValores
			//
			this.grpValores.Controls.Add(this.lstValores);
			this.grpValores.Controls.Add(this.btnEditar);
			this.grpValores.Controls.Add(this.btnRemover);
			this.grpValores.Controls.Add(this.btnAdicionar);
			this.grpValores.Location = new System.Drawing.Point(8, 8);
			this.grpValores.Name = "grpValores";
			this.grpValores.Size = new System.Drawing.Size(448, 192);
			this.grpValores.TabIndex = 1;
			this.grpValores.TabStop = false;
			this.grpValores.Text = "Designações";
			//
			//lstValores
			//
			this.lstValores.Location = new System.Drawing.Point(8, 16);
			this.lstValores.Name = "lstValores";
			this.lstValores.Size = new System.Drawing.Size(432, 134);
			this.lstValores.TabIndex = 1;
			//
			//btnEditar
			//
			this.btnEditar.Location = new System.Drawing.Point(288, 160);
			this.btnEditar.Name = "btnEditar";
			this.btnEditar.Size = new System.Drawing.Size(72, 24);
			this.btnEditar.TabIndex = 3;
			this.btnEditar.Text = "Editar";
			//
			//btnRemover
			//
			this.btnRemover.Location = new System.Drawing.Point(368, 160);
			this.btnRemover.Name = "btnRemover";
			this.btnRemover.Size = new System.Drawing.Size(72, 24);
			this.btnRemover.TabIndex = 4;
			this.btnRemover.Text = "Remover";
			//
			//btnAdicionar
			//
			this.btnAdicionar.Location = new System.Drawing.Point(208, 160);
			this.btnAdicionar.Name = "btnAdicionar";
			this.btnAdicionar.Size = new System.Drawing.Size(72, 24);
			this.btnAdicionar.TabIndex = 2;
			this.btnAdicionar.Text = "Adicionar";
			//
			//btnCancelar
			//
			this.btnCancelar.Location = new System.Drawing.Point(368, 40);
			this.btnCancelar.Name = "btnCancelar";
			this.btnCancelar.Size = new System.Drawing.Size(72, 24);
			this.btnCancelar.TabIndex = 3;
			this.btnCancelar.Text = "Cancelar";
			//
			//grpEdicao
			//
			this.grpEdicao.Controls.Add(this.btnAplicar);
			this.grpEdicao.Controls.Add(this.txtDesignacao);
			this.grpEdicao.Controls.Add(this.btnCancelar);
			this.grpEdicao.Location = new System.Drawing.Point(8, 208);
			this.grpEdicao.Name = "grpEdicao";
			this.grpEdicao.Size = new System.Drawing.Size(448, 72);
			this.grpEdicao.TabIndex = 2;
			this.grpEdicao.TabStop = false;
			//
			//btnAplicar
			//
			this.btnAplicar.Location = new System.Drawing.Point(288, 40);
			this.btnAplicar.Name = "btnAplicar";
			this.btnAplicar.Size = new System.Drawing.Size(72, 24);
			this.btnAplicar.TabIndex = 2;
			this.btnAplicar.Text = "Aplicar";
			//
			//txtDesignacao
			//
			this.txtDesignacao.Location = new System.Drawing.Point(8, 16);
			this.txtDesignacao.Name = "txtDesignacao";
			this.txtDesignacao.Size = new System.Drawing.Size(432, 20);
			this.txtDesignacao.TabIndex = 1;
			this.txtDesignacao.Text = "";
			//
			//FormDomainValueEditor
			//
			this.AcceptButton = this.btnAplicar;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(466, 319);
			this.Controls.Add(this.btnFechar);
			this.Controls.Add(this.grpValores);
			this.Controls.Add(this.grpEdicao);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDomainValueEditor";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edição de lista";
			this.grpValores.ResumeLayout(false);
			this.grpEdicao.ResumeLayout(false);
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		private ArrayList dataRows = null;
		private ArrayList deletedDataRows = new ArrayList();
		private string mDisplayMember = null;

		private void btnAdicionar_Click(object sender, System.EventArgs e)
		{
			EnterEditMode();
			txtDesignacao.Tag = null;
			txtDesignacao.Text = "";
			txtDesignacao.Focus();
			UpdateButtonStateDesignacao();
		}

		private void btnEditar_Click(object sender, System.EventArgs e)
		{
			txtDesignacao.Tag = lstValores.SelectedItem;
			txtDesignacao.Text = lstValores.GetItemText(lstValores.SelectedItem);
			EnterEditMode();
			UpdateButtonStateDesignacao();
		}

		private void btnRemover_Click(object sender, System.EventArgs e)
		{

			//Notas: não é permitido remover Tipos de Quantidade se estiverem a ser utilizados noutras descrições físicas.
			//       Inicialmente estava pensado, nestas situações, o tipo de quantidade passaria a "desconhecido", mas 
			//       como não são permitidas descrições físicas que utilizem tipos de quantidade comuns no mesmo
			//       nível documental, esta acção é impedida. No caso das unidades físicas, como para cada nível desse tipo
			//       só existe uma descrição física, neste tipo de situações o tipo de quantidade pode passar a ser
			//       "desconhecido"
			DataRow row = (DataRow)lstValores.SelectedItem;
			DataRow[] dependentRows = GetRowDependencies(row);

			string interrogacao = null;
			string detalhes = GetDependenciesReport(dependentRows);

			if (dependentRows.Length <= 0)
			{
				interrogacao = "O item \"" + lstValores.GetItemText(lstValores.SelectedItem) + "\" será removido. Pretende continuar?";
				switch (MessageBox.Show(interrogacao, "Remoção de item", MessageBoxButtons.YesNo))
				{
					case System.Windows.Forms.DialogResult.Yes:
					case System.Windows.Forms.DialogResult.OK:
						deletedDataRows.Add(row);

						// se se tratar da descrição de uma UF pode-se o tipo de quantidade
						// da descrição física para "desconhecido"
						//changeRowTipoQuantidadeUF(row)

						disconnectDataSource();
						dataRows.Remove(row);
						row.Delete();
						connectDataSource();
						break;
				}
			}
			else
			{
                interrogacao = "O item \"" + lstValores.GetItemText(lstValores.SelectedItem) + "\" não será removido visto que está a ser utilizado noutras descrições físicas";
                MessageBox.Show(detalhes != null && detalhes.Length > 0 ? detalhes : interrogacao, "Remoção de item", MessageBoxButtons.OK);
			}

			//TODO: persistir os materiais alterados - apagar?
			//GisaDataSetHelper.GetTipoQuantidadeDataAdapter().Update(GetData())
			//GisaDataSetHelper.GetTipoQuantidadeDataAdapter().Update(GetDeletedData())
		}		

		private void btnAplicar_Click(object sender, System.EventArgs e)
		{

			DataRow row = null;
			if (txtDesignacao.Tag == null)
			{
				row = GetNewDataRow(txtDesignacao.Text);
				if (row != null) // a criação da nova row pode falar
				{
					dataRows.Add(row);
				}
                //TODO: apagar?
				//lstValores.items.Add(GetNewDataRow())
				//lstValores.Items   txtDesignacao.Text
			}
			else
			{
                DataRow rowWithinLoop;
                for (int i=0; i < lstValores.Items.Count; i++)				
				{
                    rowWithinLoop = lstValores.Items[i] as DataRow;
				    row = rowWithinLoop;
					if (rowWithinLoop == (DataRow)txtDesignacao.Tag)
					{
						rowWithinLoop = GetUpdatedDataRow(rowWithinLoop, txtDesignacao.Text);
						break;
					}
				}				
			}
			disconnectDataSource();
			connectDataSource();
			lstValores.SelectedItem = row;
			LeaveEditMode();
		}

		private void disconnectDataSource()
		{			
			lstValores.ClearSelected();
			lstValores.DataSource = null;
		}

		private void connectDataSource()
		{
			lstValores.DataSource = this.dataRows;
			lstValores.DisplayMember = mDisplayMember;		
		}

		private void btnCancelar_Click(object sender, System.EventArgs e)
		{

			LeaveEditMode();
		}

		private void btnFechar_Click(object sender, System.EventArgs e)
		{

			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private void lstValores_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UpdateButtonState();
		}

		private void txtDesignacao_TextChanged(object sender, System.EventArgs e)
		{
			UpdateButtonStateDesignacao();
		}

		private void EnterEditMode()
		{
			lstValores.Enabled = false;
			btnAdicionar.Enabled = false;
			btnEditar.Enabled = false;
			btnRemover.Enabled = false;

			txtDesignacao.Enabled = true;
			btnAplicar.Enabled = true;
			this.AcceptButton = btnAplicar;
			btnCancelar.Enabled = true;
		}

		private void LeaveEditMode()
		{
			lstValores.Enabled = true;
			btnAdicionar.Enabled = true;
			btnEditar.Enabled = true;
			btnRemover.Enabled = true;
			UpdateButtonState();

			txtDesignacao.Enabled = false;
			btnAplicar.Enabled = false;
			this.AcceptButton = btnAdicionar;
			btnCancelar.Enabled = false;

			txtDesignacao.Text = "";
			lstValores.Focus();
		}

		private void UpdateButtonState()
		{
            //TODO: apagar? e o método?
			//btnEditar.Enabled = lstValores.SelectedIndex >= 0
			//btnRemover.Enabled = btnEditar.Enabled
		}

		private void UpdateButtonStateDesignacao()
		{
			string newText = txtDesignacao.Text.Trim();
			btnAplicar.Enabled = newText.Length > 0 && ! (alreadyExists(newText)); // Not lstValores.Items.Contains(newText)
		}

		private bool alreadyExists(string txt)
		{
			foreach (DataRow item in lstValores.Items)
			{
				if (item[lstValores.DisplayMember].ToString().Equals(txt))
				{
					return true;
				}
			}
			return false;
		}

		public void LoadData(DataRow[] DataRows, string DisplayMember)
		{
			disconnectDataSource();
			this.dataRows = new ArrayList(DataRows);
			mDisplayMember = DisplayMember;
			connectDataSource();
			grpValores.Enabled = true;
		}

		public void LoadData(ArrayList DataRows, string DisplayMember)
		{
			disconnectDataSource();
			this.dataRows = new ArrayList(DataRows);
			mDisplayMember = DisplayMember;
			connectDataSource();
			grpValores.Enabled = true;
		}

		public DataRow[] GetData()
		{
			return (DataRow[])(dataRows.ToArray(typeof(DataRow)));
		}

		public DataRow[] GetDeletedData()
		{
			return (DataRow[])(deletedDataRows.ToArray(typeof(DataRow)));
		}

		protected virtual DataRow GetNewDataRow(string Value)
		{
			// override to return a row of the right type
			return null;
		}

		protected virtual DataRow GetUpdatedDataRow(DataRow row, string NewValue)
		{
			// override to return a row updated with the new values
			return null;
		}

		protected virtual DataRow[] GetRowDependencies(DataRow row)
		{
			// override to return an array of the rows that refer the current row
			return null;
		}

		protected virtual string GetDependenciesReport(DataRow[] rows)
		{
			// override to return a string describing the "rows"
			return null;
		}
	}

} //end of root namespace