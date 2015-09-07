using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA
{
	public class FormListaModelosAvaliacao : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormListaModelosAvaliacao() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            txtDesignacaoListaModelos.TextChanged += txtDesignacaoListaModelos_TextChanged;
            PxDateBox1.PxDateBoxTextChanged += PxDateBox1_TextChanged;
            btnOk.Click += btnOk_Click;

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
		internal System.Windows.Forms.GroupBox grpDataInicio;
		internal GISA.Controls.PxDateBox PxDateBox1;
		internal System.Windows.Forms.GroupBox grpDesignacaoListaModelos;
		internal System.Windows.Forms.TextBox txtDesignacaoListaModelos;
		internal System.Windows.Forms.Button btnOk;
		internal System.Windows.Forms.Button btnCancel;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.grpDataInicio = new System.Windows.Forms.GroupBox();
			this.PxDateBox1 = new GISA.Controls.PxDateBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.grpDesignacaoListaModelos = new System.Windows.Forms.GroupBox();
			this.txtDesignacaoListaModelos = new System.Windows.Forms.TextBox();
			this.grpDataInicio.SuspendLayout();
			this.grpDesignacaoListaModelos.SuspendLayout();
			this.SuspendLayout();
			//
			//grpDataInicio
			//
			this.grpDataInicio.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
			this.grpDataInicio.Controls.Add(this.PxDateBox1);
			this.grpDataInicio.Location = new System.Drawing.Point(408, 16);
			this.grpDataInicio.Name = "grpDataInicio";
			this.grpDataInicio.Size = new System.Drawing.Size(96, 44);
			this.grpDataInicio.TabIndex = 11;
			this.grpDataInicio.TabStop = false;
			this.grpDataInicio.Text = "Data de Início";
			//
			//PxDateBox1
			//
			this.PxDateBox1.Location = new System.Drawing.Point(8, 16);
			this.PxDateBox1.Name = "PxDateBox1";
			this.PxDateBox1.Size = new System.Drawing.Size(80, 22);
			this.PxDateBox1.TabIndex = 1;
			this.PxDateBox1.ValueDay = "";
			this.PxDateBox1.ValueMonth = "";
			this.PxDateBox1.ValueYear = "";
			//
			//btnOk
			//
			this.btnOk.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(312, 67);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(88, 24);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "Aceitar";
			//
			//btnCancel
			//
			this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(408, 67);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(88, 24);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancelar";
			//
			//grpDesignacaoListaModelos
			//
			this.grpDesignacaoListaModelos.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.grpDesignacaoListaModelos.Controls.Add(this.txtDesignacaoListaModelos);
			this.grpDesignacaoListaModelos.Location = new System.Drawing.Point(8, 16);
			this.grpDesignacaoListaModelos.Name = "grpDesignacaoListaModelos";
			this.grpDesignacaoListaModelos.Size = new System.Drawing.Size(394, 44);
			this.grpDesignacaoListaModelos.TabIndex = 10;
			this.grpDesignacaoListaModelos.TabStop = false;
			this.grpDesignacaoListaModelos.Text = "Designação";
			//
			//txtDesignacaoListaModelos
			//
			this.txtDesignacaoListaModelos.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.txtDesignacaoListaModelos.Location = new System.Drawing.Point(8, 16);
			this.txtDesignacaoListaModelos.Name = "txtDesignacaoListaModelos";
			this.txtDesignacaoListaModelos.Size = new System.Drawing.Size(378, 20);
			this.txtDesignacaoListaModelos.TabIndex = 0;
			this.txtDesignacaoListaModelos.Text = "";
			//
			//FormListaModelosAvaliacao
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(512, 102);
			this.ControlBox = false;
			this.Controls.Add(this.grpDataInicio);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.grpDesignacaoListaModelos);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormListaModelosAvaliacao";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Lista de Modelos de Avaliação";
			this.grpDataInicio.ResumeLayout(false);
			this.grpDesignacaoListaModelos.ResumeLayout(false);
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		private void UpdateButtons()
		{
			if (txtDesignacaoListaModelos.Text.Length == 0 || PxDateBox1.ValueDay.Length == 0 || PxDateBox1.ValueMonth.Length == 0 || PxDateBox1.ValueYear.Length == 0)
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

		private void PxDateBox1_TextChanged()
		{
			UpdateButtons();
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			if (System.Convert.ToInt32(PxDateBox1.ValueYear) < 1753)
			{
				PxDateBox1.ValueYear = "1753";
			}
		}
	}

} //end of root namespace