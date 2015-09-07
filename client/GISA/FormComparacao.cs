using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using GISA.Controls.Localizacao;

namespace GISA
{
	public class FormComparacao : System.Windows.Forms.Form
	{

		private System.Windows.Forms.Label lblSubDensidade;
		private System.Windows.Forms.Label lblDensidade;
		private System.Windows.Forms.ComboBox cbSubDensidade;
		private System.Windows.Forms.ComboBox cbPonderacao;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.ComboBox cbDensidade;
		private System.Windows.Forms.Label lblPonderacao;
		private System.Windows.Forms.Button btnCancel;

		public FormComparacao()
		{
			InitializeComponent();

            btnOk.Click += btnOk_Click;
            btnCancel.Click += btnCancel_Click;

            cnList.mTipoNivelRelLimitExcl = TipoNivelRelacionado.D;

			refreshButtonState();
		}
		internal ControloNivelList cnList;

		public void InitializeComponent()
		{
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblPonderacao = new System.Windows.Forms.Label();
			this.cbDensidade = new System.Windows.Forms.ComboBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.cbPonderacao = new System.Windows.Forms.ComboBox();
			this.cbSubDensidade = new System.Windows.Forms.ComboBox();
			this.lblDensidade = new System.Windows.Forms.Label();
			this.lblSubDensidade = new System.Windows.Forms.Label();
			this.cnList = new ControloNivelList();
			this.SuspendLayout();
			//
			//btnCancel
			//
			this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(480, 384);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(88, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Cancelar";
			//
			//lblPonderacao
			//
			this.lblPonderacao.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
			this.lblPonderacao.Location = new System.Drawing.Point(48, 358);
			this.lblPonderacao.Name = "lblPonderacao";
			this.lblPonderacao.Size = new System.Drawing.Size(72, 16);
			this.lblPonderacao.TabIndex = 3;
			this.lblPonderacao.Text = "Ponderação:";
			//
			//cbDensidade
			//
			this.cbDensidade.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
			this.cbDensidade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDensidade.Location = new System.Drawing.Point(128, 328);
			this.cbDensidade.Name = "cbDensidade";
			this.cbDensidade.Size = new System.Drawing.Size(121, 21);
			this.cbDensidade.TabIndex = 2;
			//
			//btnOk
			//
			this.btnOk.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnOk.Location = new System.Drawing.Point(384, 384);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(88, 23);
			this.btnOk.TabIndex = 5;
			this.btnOk.Text = "Adicionar";
			//
			//cbPonderacao
			//
			this.cbPonderacao.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
			this.cbPonderacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPonderacao.Location = new System.Drawing.Point(128, 358);
			this.cbPonderacao.Name = "cbPonderacao";
			this.cbPonderacao.Size = new System.Drawing.Size(121, 21);
			this.cbPonderacao.TabIndex = 4;
			//
			//cbSubDensidade
			//
			this.cbSubDensidade.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
			this.cbSubDensidade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSubDensidade.Location = new System.Drawing.Point(368, 328);
			this.cbSubDensidade.Name = "cbSubDensidade";
			this.cbSubDensidade.Size = new System.Drawing.Size(121, 21);
			this.cbSubDensidade.TabIndex = 3;
			//
			//lblDensidade
			//
			this.lblDensidade.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
			this.lblDensidade.Location = new System.Drawing.Point(24, 328);
			this.lblDensidade.Name = "lblDensidade";
			this.lblDensidade.Size = new System.Drawing.Size(96, 16);
			this.lblDensidade.TabIndex = 6;
            this.lblDensidade.Text = "Tipo de produção:";
			//
			//lblSubDensidade
			//
			this.lblSubDensidade.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
			this.lblSubDensidade.Location = new System.Drawing.Point(264, 328);
			this.lblSubDensidade.Name = "lblSubDensidade";
			this.lblSubDensidade.Size = new System.Drawing.Size(104, 16);
			this.lblSubDensidade.TabIndex = 5;
			this.lblSubDensidade.Text = "Grau de densidade:";
			//
			//cnList
			//
			this.cnList.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.cnList.Location = new System.Drawing.Point(0, 0);
			this.cnList.Name = "cnList";
			this.cnList.Size = new System.Drawing.Size(578, 320);
			this.cnList.TabIndex = 1;
			//
			//FormComparacao
			//
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(578, 415);
			this.ControlBox = false;
			this.Controls.Add(this.cnList);
			this.Controls.Add(this.cbSubDensidade);
			this.Controls.Add(this.cbDensidade);
			this.Controls.Add(this.lblDensidade);
			this.Controls.Add(this.lblSubDensidade);
			this.Controls.Add(this.cbPonderacao);
			this.Controls.Add(this.lblPonderacao);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MinimizeBox = false;
			this.Name = "FormComparacao";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Comparação";
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

		private void PopulateControls()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("ID", typeof(decimal));
			dt.Columns.Add("Designacao", typeof(string));
			dt.Rows.Add(new object[] {System.Convert.ToDecimal(0), "0 (Zero)"});
			dt.Rows.Add(new object[] {System.Convert.ToDecimal(1), "1 (Um)"});

			cbPonderacao.DataSource = dt;
			cbPonderacao.DisplayMember = "Designacao";
			cbPonderacao.ValueMember = "ID";
			SelectFirstPonderacao();

			cbDensidade.DataSource = GisaDataSetHelper.GetInstance().TipoDensidade;
			cbDensidade.DisplayMember = "Designacao";
			cbDensidade.ValueMember = "ID";
			SelectFirstDensidade();

			PopulateSubDensidade();
			SelectFirstSubDensidade();
		}


		public void LoadData()
		{
			LoadData(false);
		}

		public void LoadData(bool EditMode)
		{
            cnList.LoadContents(EditMode);
			PopulateControls();
			refreshButtonState();
			AddHandlers();
			if (EditMode)
			{
				btnOk.Text = "Atualizar";
			}
			else
			{
				btnOk.Text = "Adicionar";
			}
		}

		public GISADataset.RelacaoHierarquicaRow RelacaoHierarquica
		{
			get
			{
				return cnList.SelectedRelacaoHierarquica;
			}
			set
			{
				cnList.SetSelectedRelacaoHierarquica(value.NivelRowByNivelRelacaoHierarquica, value.NivelRowByNivelRelacaoHierarquicaUpper);
				if (cnList.SelectedRelacaoHierarquica == null)
				{
					throw new Exception("Item does not exist.");
				}
				refreshButtonState();
			}
		}

		public GISADataset.TipoDensidadeRow Densidade
		{
			get
			{
				return (GISADataset.TipoDensidadeRow)(((DataRowView)cbDensidade.SelectedItem).Row);
			}
			set
			{
				cbDensidade.SelectedValue = value.ID;
				if (cbDensidade.SelectedItem == null)
				{
					throw new Exception("Item does not exist.");
				}
				refreshButtonState();
			}
		}

		public GISADataset.TipoSubDensidadeRow SubDensidade
		{
			get
			{
				return (GISADataset.TipoSubDensidadeRow)cbSubDensidade.SelectedItem;
			}
			set
			{
				cbSubDensidade.SelectedValue = value.ID;
				if (cbSubDensidade.SelectedItem == null)
				{
					throw new Exception("Item does not exist.");
				}
				refreshButtonState();
			}
		}

		public decimal Ponderacao
		{
			get
			{
				return System.Convert.ToDecimal(cbPonderacao.SelectedValue);
			}
			set
			{
				cbPonderacao.SelectedValue = value;
				if (cbPonderacao.SelectedItem == null)
				{
					throw new Exception("Item does not exist.");
				}
				refreshButtonState();
			}
		}

		private void AddHandlers()
		{
			cbDensidade.SelectedIndexChanged += cbDensidade_SelectedIndexChanged;
			cbSubDensidade.SelectedIndexChanged += cbSubDensidade_SelectedIndexChanged;
			cbPonderacao.SelectedIndexChanged += cbPonderacao_SelectedIndexChanged;
			cnList.trVwLocalizacao.AfterSelect += trvwEstrutura_AfterSelect;

		}

		private void PopulateSubDensidade()
		{
			cbSubDensidade.DataSource = GisaDataSetHelper.GetInstance().TipoSubDensidade. Select("IDTipoDensidade = " + System.Convert.ToString(Densidade.ID));
			cbSubDensidade.DisplayMember = "Designacao";
			cbSubDensidade.ValueMember = "ID";
		}

		private void SelectFirstDensidade()
		{
			if (cbDensidade.Items.Count > 0)
			{
				cbDensidade.SelectedIndex = 0;
			}
		}

		private void SelectFirstSubDensidade()
		{
			if (cbSubDensidade.Items.Count > 0)
			{
				cbSubDensidade.SelectedIndex = 0;
			}
		}

		private void SelectFirstPonderacao()
		{
			if (cbPonderacao.Items.Count > 0)
			{
				cbPonderacao.SelectedIndex = 0;
			}
		}

		private void cbDensidade_SelectedIndexChanged(object sender, EventArgs e)
		{

			PopulateSubDensidade();
			refreshButtonState();
		}

		private void cbSubDensidade_SelectedIndexChanged(object sender, EventArgs e)
		{

			refreshButtonState();
		}

		private void cbPonderacao_SelectedIndexChanged(object sender, EventArgs e)
		{

			refreshButtonState();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{

			DialogResult = System.Windows.Forms.DialogResult.OK;
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{

			DialogResult = System.Windows.Forms.DialogResult.Cancel;
			Close();
		}

		private void trvwEstrutura_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{

			refreshButtonState();
		}

		private void refreshButtonState()
		{
			if (cnList.SelectedRelacaoHierarquica != null && (cnList.SelectedRelacaoHierarquica.IDTipoNivelRelacionado == TipoNivelRelacionado.SR || cnList.SelectedRelacaoHierarquica.IDTipoNivelRelacionado == TipoNivelRelacionado.SSR || (cnList.SelectedRelacaoHierarquica.IDTipoNivelRelacionado == TipoNivelRelacionado.D && cnList.SelectedRelacaoHierarquica.NivelRowByNivelRelacaoHierarquicaUpper.IDTipoNivel == TipoNivel.ESTRUTURAL)) && cbDensidade.SelectedValue != null && cbSubDensidade.SelectedValue != null && cbPonderacao.SelectedValue != null)
			{

				btnOk.Enabled = true;
			}
			else
			{
				btnOk.Enabled = false;
			}
		}
	}

} //end of root namespace