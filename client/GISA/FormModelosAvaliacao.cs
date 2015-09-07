using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA
{
	public class FormModelosAvaliacao : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormModelosAvaliacao() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            txtDesignacaoListaModelos.TextChanged += txtDesignacaoListaModelos_TextChanged;
            cbDestinoFinal.SelectedIndexChanged += cbDestinoFinal_SelectedIndexChanged;

			FillDestinoFinal();
			UpdateButtons();
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
		internal System.Windows.Forms.Button btnOk;
		internal System.Windows.Forms.Button btnCancel;
		internal System.Windows.Forms.GroupBox grpDesignacaoListaModelos;
		internal System.Windows.Forms.TextBox txtDesignacaoListaModelos;
		internal System.Windows.Forms.GroupBox grpDestinoFinal;
		internal System.Windows.Forms.ComboBox cbDestinoFinal;
		internal System.Windows.Forms.GroupBox grpPrazoConservacao;
		internal System.Windows.Forms.NumericUpDown nudPrazoConservacao;
		internal System.Windows.Forms.Label lblAnos;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.grpDesignacaoListaModelos = new System.Windows.Forms.GroupBox();
			this.txtDesignacaoListaModelos = new System.Windows.Forms.TextBox();
			this.grpDestinoFinal = new System.Windows.Forms.GroupBox();
			this.cbDestinoFinal = new System.Windows.Forms.ComboBox();
			this.grpPrazoConservacao = new System.Windows.Forms.GroupBox();
			this.nudPrazoConservacao = new System.Windows.Forms.NumericUpDown();
			this.lblAnos = new System.Windows.Forms.Label();
			this.grpDesignacaoListaModelos.SuspendLayout();
			this.grpDestinoFinal.SuspendLayout();
			this.grpPrazoConservacao.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.nudPrazoConservacao).BeginInit();
			this.SuspendLayout();
			//
			//btnOk
			//
			this.btnOk.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(104, 136);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(88, 24);
			this.btnOk.TabIndex = 3;
			this.btnOk.Text = "Aceitar";
			//
			//btnCancel
			//
			this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(200, 136);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(88, 24);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancelar";
			//
			//grpDesignacaoListaModelos
			//
			this.grpDesignacaoListaModelos.Controls.Add(this.txtDesignacaoListaModelos);
			this.grpDesignacaoListaModelos.Location = new System.Drawing.Point(7, 16);
			this.grpDesignacaoListaModelos.Name = "grpDesignacaoListaModelos";
			this.grpDesignacaoListaModelos.Size = new System.Drawing.Size(288, 44);
			this.grpDesignacaoListaModelos.TabIndex = 12;
			this.grpDesignacaoListaModelos.TabStop = false;
			this.grpDesignacaoListaModelos.Text = "Designação";
			//
			//txtDesignacaoListaModelos
			//
			this.txtDesignacaoListaModelos.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.txtDesignacaoListaModelos.Location = new System.Drawing.Point(8, 16);
			this.txtDesignacaoListaModelos.Name = "txtDesignacaoListaModelos";
			this.txtDesignacaoListaModelos.Size = new System.Drawing.Size(272, 20);
			this.txtDesignacaoListaModelos.TabIndex = 0;
			this.txtDesignacaoListaModelos.Text = "";
			//
			//grpDestinoFinal
			//
			this.grpDestinoFinal.Controls.Add(this.cbDestinoFinal);
			this.grpDestinoFinal.Location = new System.Drawing.Point(150, 72);
			this.grpDestinoFinal.Name = "grpDestinoFinal";
			this.grpDestinoFinal.Size = new System.Drawing.Size(144, 48);
			this.grpDestinoFinal.TabIndex = 14;
			this.grpDestinoFinal.TabStop = false;
			this.grpDestinoFinal.Text = "Destino final";
			//
			//cbDestinoFinal
			//
			this.cbDestinoFinal.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.cbDestinoFinal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDestinoFinal.Location = new System.Drawing.Point(8, 18);
			this.cbDestinoFinal.Name = "cbDestinoFinal";
			this.cbDestinoFinal.Size = new System.Drawing.Size(128, 21);
			this.cbDestinoFinal.TabIndex = 2;
			//
			//grpPrazoConservacao
			//
			this.grpPrazoConservacao.Controls.Add(this.nudPrazoConservacao);
			this.grpPrazoConservacao.Controls.Add(this.lblAnos);
			this.grpPrazoConservacao.Location = new System.Drawing.Point(8, 72);
			this.grpPrazoConservacao.Name = "grpPrazoConservacao";
			this.grpPrazoConservacao.Size = new System.Drawing.Size(124, 48);
			this.grpPrazoConservacao.TabIndex = 15;
			this.grpPrazoConservacao.TabStop = false;
			this.grpPrazoConservacao.Text = "Prazo conservação";
			//
			//nudPrazoConservacao
			//
			this.nudPrazoConservacao.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.nudPrazoConservacao.Location = new System.Drawing.Point(8, 18);
			this.nudPrazoConservacao.Maximum = new decimal(new int[] {999, 0, 0, 0});
			this.nudPrazoConservacao.Name = "nudPrazoConservacao";
			this.nudPrazoConservacao.Size = new System.Drawing.Size(79, 20);
			this.nudPrazoConservacao.TabIndex = 4;
			//
			//lblAnos
			//
			this.lblAnos.Location = new System.Drawing.Point(89, 20);
			this.lblAnos.Name = "lblAnos";
			this.lblAnos.Size = new System.Drawing.Size(30, 17);
			this.lblAnos.TabIndex = 3;
			this.lblAnos.Text = "anos";
			//
			//FormModelosAvaliacao
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(306, 174);
			this.ControlBox = false;
			this.Controls.Add(this.grpPrazoConservacao);
			this.Controls.Add(this.grpDestinoFinal);
			this.Controls.Add(this.grpDesignacaoListaModelos);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormModelosAvaliacao";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Modelo de Avaliação";
			this.grpDesignacaoListaModelos.ResumeLayout(false);
			this.grpDestinoFinal.ResumeLayout(false);
			this.grpPrazoConservacao.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.nudPrazoConservacao).EndInit();
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		private void FillDestinoFinal()
		{
			int originalDestinoFinal = -1;
			DataTable dt = new DataTable();
			dt.Columns.Add("ID", typeof(int));
			dt.Columns.Add("Designacao", typeof(string));

			dt.Rows.Add(new object[] {-1, ""});
			dt.Rows.Add(new object[] {1, "Conservação"});
			dt.Rows.Add(new object[] {0, "Eliminação"});

			cbDestinoFinal.DisplayMember = "Designacao";
			cbDestinoFinal.ValueMember = "ID";
			cbDestinoFinal.DataSource = dt;
			cbDestinoFinal.SelectedValue = originalDestinoFinal;
		}

		private void UpdateButtons()
		{
			if (txtDesignacaoListaModelos.Text.Length == 0 && cbDestinoFinal.SelectedIndex == 0)
			{
				btnOk.Enabled = false;
			}
			else
			{
				btnOk.Enabled = true;
			}
		}

		private void txtDesignacaoListaModelos_TextChanged(object sender, System.EventArgs e)
		{
			UpdateButtons();
		}

		private void cbDestinoFinal_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cbDestinoFinal.SelectedIndex == 2)
			{
				nudPrazoConservacao.Enabled = true;
			}
			else
			{
				nudPrazoConservacao.Value = 0;
				nudPrazoConservacao.Enabled = false;
			}
		}
	}

} //end of root namespace