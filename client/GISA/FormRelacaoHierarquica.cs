using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA
{
	public class FormRelacaoHierarquica : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormRelacaoHierarquica() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
			relacaoNvl.lblDescricao.Text = "Observações";
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
		internal GISA.RelacaoNivel relacaoNvl;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.relacaoNvl = new GISA.RelacaoNivel();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			//relacaoNvl
			//
			this.relacaoNvl.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.relacaoNvl.ContextNivelRow = null;
			this.relacaoNvl.Location = new System.Drawing.Point(4, 4);
			this.relacaoNvl.Name = "relacaoNvl";
			this.relacaoNvl.Size = new System.Drawing.Size(620, 160);
			this.relacaoNvl.TabIndex = 0;
			this.relacaoNvl.TipoNivelRelacionadoRow = null;
			//
			//btnOk
			//
			this.btnOk.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(428, 172);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "OK";
			//
			//btnCancel
			//
			this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(516, 172);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			//
			//FormRelacaoHierarquica
			//
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(628, 205);
			this.ControlBox = false;
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.relacaoNvl);
			this.Name = "FormRelacaoHierarquica";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Relação hierárquica";
			this.ResumeLayout(false);

		}

	#endregion

	}

} //end of root namespace