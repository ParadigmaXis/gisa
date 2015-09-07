using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.GUIHelper;

namespace GISA.Controls.ControloAut
{
	public class FormCreateEntidadeProdutora : FormCreateControloAut
	{

	#region  Windows Form Designer generated code 

		public FormCreateEntidadeProdutora() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            txtCodigo.TextChanged += txtCodigo_TextChanged;
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
		public System.Windows.Forms.TextBox txtCodigo;
		public System.Windows.Forms.Label lblCodigo;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.txtCodigo = new System.Windows.Forms.TextBox();
			this.lblCodigo = new System.Windows.Forms.Label();
			this.SuspendLayout();
			//
			//grpTermo
			//
			this.grpTermo.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.grpTermo.Location = new System.Drawing.Point(4, 96);
			this.grpTermo.Name = "grpTermo";
			//
			//btnOk
			//
			this.btnOk.Location = new System.Drawing.Point(204, 332);
			this.btnOk.Name = "btnOk";
			//
			//btnCancel
			//
			this.btnCancel.Location = new System.Drawing.Point(300, 332);
			this.btnCancel.Name = "btnCancel";
			//
			//txtCodigo
			//
			this.txtCodigo.Location = new System.Drawing.Point(8, 68);
			this.txtCodigo.Name = "txtCodigo";
			this.txtCodigo.Size = new System.Drawing.Size(388, 20);
			this.txtCodigo.TabIndex = 1;
			this.txtCodigo.Text = "";
			//
			//lblCodigo
			//
			this.lblCodigo.Location = new System.Drawing.Point(8, 50);
			this.lblCodigo.Name = "lblCodigo";
			this.lblCodigo.Size = new System.Drawing.Size(384, 16);
			this.lblCodigo.TabIndex = 16;
			this.lblCodigo.Text = "Código parcial";
			//
			//FormCreateEntidadeProdutora
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(408, 385);
			this.Controls.Add(this.lblCodigo);
			this.Controls.Add(this.txtCodigo);
			this.Name = "FormCreateEntidadeProdutora";
			this.Controls.SetChildIndex(this.txtCodigo, 0);
			this.Controls.SetChildIndex(this.lblCodigo, 0);
			this.Controls.SetChildIndex(this.grpTermo, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		protected override void UpdateButtonState()
		{
			bool validTermo = false;
			validTermo = ListTermos.ValidAuthorizedForm != null;

			// criação
			if (OriginalCADRow == null)
			{
				if (cbNoticiaAut.SelectedIndex < 1 ||
                    !(GUIHelper.GUIHelper.CheckValidCodigoParcial(txtCodigo.Text)) ||
                    ListTermos.ValidAuthorizedForm == null || 
                    ExistsFormaAutorizada())
				{

					btnOk.Enabled = false;
				}
				else
				{
					btnOk.Enabled = true;
				}
			}
			else // edição
			{		
				if (cbNoticiaAut.SelectedIndex > 0 &&
                    GUIHelper.GUIHelper.CheckValidCodigoParcial(txtCodigo.Text) &&
                    (!(txtCodigo.Text.Trim().Equals(OriginalCADRow.ControloAutRow.GetNivelControloAutRows()[0].NivelRow.Codigo)) || (!(ListTermos.ValidAuthorizedForm == OriginalCADRow.DicionarioRow.ToString()) && !(ExistsFormaAutorizada()))) && ListTermos.ValidAuthorizedForm != null)
				{			
					btnOk.Enabled = true;
				}
				else
				{
					btnOk.Enabled = false;
				}
			}
		}

		private void txtCodigo_TextChanged(object sender, System.EventArgs e)
		{
			UpdateButtonState();
		}
	}

} //end of root namespace