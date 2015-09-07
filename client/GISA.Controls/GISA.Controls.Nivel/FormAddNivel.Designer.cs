using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA.Controls.Nivel
{
	[Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
	public partial class FormAddNivel : System.Windows.Forms.Form
	{

		//Form overrides dispose to clean up the component list.
		public FormAddNivel()
		{
			InitializeComponent();
            AddHandlers();
		}
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && components != null)
					components.Dispose();
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components = null;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.grpTitulo = new System.Windows.Forms.GroupBox();
            this.txtDesignacao = new System.Windows.Forms.TextBox();
            this.grpCodigo = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.grpTitulo.SuspendLayout();
            this.grpCodigo.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(6, 16);
            this.txtCodigo.MaxLength = 40;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(344, 20);
            this.txtCodigo.TabIndex = 1;
            // 
            // grpTitulo
            // 
            this.grpTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTitulo.Controls.Add(this.txtDesignacao);
            this.grpTitulo.Location = new System.Drawing.Point(5, 53);
            this.grpTitulo.Name = "grpTitulo";
            this.grpTitulo.Size = new System.Drawing.Size(559, 43);
            this.grpTitulo.TabIndex = 7;
            this.grpTitulo.TabStop = false;
            this.grpTitulo.Text = "Título";
            // 
            // txtDesignacao
            // 
            this.txtDesignacao.Location = new System.Drawing.Point(6, 16);
            this.txtDesignacao.MaxLength = 768;
            this.txtDesignacao.Name = "txtDesignacao";
            this.txtDesignacao.Size = new System.Drawing.Size(545, 20);
            this.txtDesignacao.TabIndex = 2;
            // 
            // grpCodigo
            // 
            this.grpCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCodigo.Controls.Add(this.txtCodigo);
            this.grpCodigo.Location = new System.Drawing.Point(5, 5);
            this.grpCodigo.Name = "grpCodigo";
            this.grpCodigo.Size = new System.Drawing.Size(360, 43);
            this.grpCodigo.TabIndex = 6;
            this.grpCodigo.TabStop = false;
            this.grpCodigo.Text = "Código parcial";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(456, 103);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancelar";
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Enabled = false;
            this.btnAccept.Location = new System.Drawing.Point(376, 103);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 8;
            this.btnAccept.Text = "Aceitar";
            // 
            // FormAddNivel
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(568, 138);
            this.Controls.Add(this.grpTitulo);
            this.Controls.Add(this.grpCodigo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormAddNivel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.grpTitulo.ResumeLayout(false);
            this.grpTitulo.PerformLayout();
            this.grpCodigo.ResumeLayout(false);
            this.grpCodigo.PerformLayout();
            this.ResumeLayout(false);

		}
		public System.Windows.Forms.TextBox txtCodigo;
        public System.Windows.Forms.GroupBox grpTitulo;
        public System.Windows.Forms.TextBox txtDesignacao;
        public System.Windows.Forms.GroupBox grpCodigo;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnAccept;
	}

} //end of root namespace