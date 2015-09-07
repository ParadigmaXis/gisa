using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class FormDimensoes : System.Windows.Forms.Form
	{

		private GISADataset.FRDBaseRow mFrdBase;
		private bool mIsEdit;

	#region  Windows Form Designer generated code 


		public FormDimensoes(GISADataset.FRDBaseRow frdBase): this(frdBase, false)
		{
		}

//INSTANT C# NOTE: C# does not support optional parameters. Overloaded method(s) are created above.
//ORIGINAL LINE: Public Sub new(ByVal frdBase As GISADataset.FRDBaseRow, Optional ByVal isEdit As Boolean = false)
		public FormDimensoes(GISADataset.FRDBaseRow frdBase, bool isEdit) : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
			mFrdBase = frdBase;
			mIsEdit = isEdit;
			if (isEdit)
			{
				//DimensoesSuporte1.cbTipo.Enabled = False
				//DimensoesSuporte1.btnMaterialManager.Enabled = False
			}
			//UpdateButtonState()
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
		internal System.Windows.Forms.GroupBox grpEstadoConservacao;
		internal System.Windows.Forms.GroupBox grpQuantidade;
		internal System.Windows.Forms.TextBox txtQuantidade;
		internal System.Windows.Forms.GroupBox grpFormaSuporte;
		internal System.Windows.Forms.ComboBox cbFormaSuporte;
		internal System.Windows.Forms.ComboBox cbEstadoConservacao;
		internal System.Windows.Forms.GroupBox grpMateriais;
		internal System.Windows.Forms.CheckedListBox chkLstMateriais;
		internal System.Windows.Forms.GroupBox grpTecnicasRegisto;
		internal System.Windows.Forms.CheckedListBox chkLstTecnicasRegisto;
		internal System.Windows.Forms.Button btnMaterialManager;
		internal System.Windows.Forms.Button Button1;
		internal System.Windows.Forms.Button Button2;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.grpQuantidade = new System.Windows.Forms.GroupBox();
			this.txtQuantidade = new System.Windows.Forms.TextBox();
			this.grpFormaSuporte = new System.Windows.Forms.GroupBox();
			this.Button2 = new System.Windows.Forms.Button();
			this.cbFormaSuporte = new System.Windows.Forms.ComboBox();
			this.grpEstadoConservacao = new System.Windows.Forms.GroupBox();
			this.cbEstadoConservacao = new System.Windows.Forms.ComboBox();
			this.grpMateriais = new System.Windows.Forms.GroupBox();
			this.btnMaterialManager = new System.Windows.Forms.Button();
			this.chkLstMateriais = new System.Windows.Forms.CheckedListBox();
			this.grpTecnicasRegisto = new System.Windows.Forms.GroupBox();
			this.Button1 = new System.Windows.Forms.Button();
			this.chkLstTecnicasRegisto = new System.Windows.Forms.CheckedListBox();
			this.grpQuantidade.SuspendLayout();
			this.grpFormaSuporte.SuspendLayout();
			this.grpEstadoConservacao.SuspendLayout();
			this.grpMateriais.SuspendLayout();
			this.grpTecnicasRegisto.SuspendLayout();
			this.SuspendLayout();
			//
			//btnOk
			//
			this.btnOk.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnOk.Location = new System.Drawing.Point(257, 158);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 6;
			this.btnOk.Text = "Ok";
			//
			//btnCancel
			//
			this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(345, 158);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel";
			//
			//grpQuantidade
			//
			this.grpQuantidade.Controls.Add(this.txtQuantidade);
			this.grpQuantidade.Location = new System.Drawing.Point(4, 2);
			this.grpQuantidade.Name = "grpQuantidade";
			this.grpQuantidade.Size = new System.Drawing.Size(88, 46);
			this.grpQuantidade.TabIndex = 1;
			this.grpQuantidade.TabStop = false;
			this.grpQuantidade.Text = "Quantidade";
			//
			//txtQuantidade
			//
			this.txtQuantidade.Location = new System.Drawing.Point(8, 19);
			this.txtQuantidade.Name = "txtQuantidade";
			this.txtQuantidade.Size = new System.Drawing.Size(73, 20);
			this.txtQuantidade.TabIndex = 4;
			this.txtQuantidade.Text = "";
			//
			//grpFormaSuporte
			//
			this.grpFormaSuporte.Controls.Add(this.Button2);
			this.grpFormaSuporte.Controls.Add(this.cbFormaSuporte);
			this.grpFormaSuporte.Location = new System.Drawing.Point(97, 2);
			this.grpFormaSuporte.Name = "grpFormaSuporte";
			this.grpFormaSuporte.Size = new System.Drawing.Size(166, 46);
			this.grpFormaSuporte.TabIndex = 2;
			this.grpFormaSuporte.TabStop = false;
			this.grpFormaSuporte.Text = "Forma de suporte";
			//
			//Button2
			//
			this.Button2.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
			this.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.Button2.Location = new System.Drawing.Point(136, 16);
			this.Button2.Name = "Button2";
			this.Button2.Size = new System.Drawing.Size(24, 23);
			this.Button2.TabIndex = 2;
			//
			//cbFormaSuporte
			//
			this.cbFormaSuporte.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.cbFormaSuporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFormaSuporte.Location = new System.Drawing.Point(8, 16);
			this.cbFormaSuporte.Name = "cbFormaSuporte";
			this.cbFormaSuporte.Size = new System.Drawing.Size(123, 21);
			this.cbFormaSuporte.TabIndex = 1;
			//
			//grpEstadoConservacao
			//
			this.grpEstadoConservacao.Controls.Add(this.cbEstadoConservacao);
			this.grpEstadoConservacao.Location = new System.Drawing.Point(268, 2);
			this.grpEstadoConservacao.Name = "grpEstadoConservacao";
			this.grpEstadoConservacao.Size = new System.Drawing.Size(138, 46);
			this.grpEstadoConservacao.TabIndex = 3;
			this.grpEstadoConservacao.TabStop = false;
			this.grpEstadoConservacao.Text = "Estado conservação";
			//
			//cbEstadoConservacao
			//
			this.cbEstadoConservacao.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.cbEstadoConservacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbEstadoConservacao.Location = new System.Drawing.Point(8, 16);
			this.cbEstadoConservacao.Name = "cbEstadoConservacao";
			this.cbEstadoConservacao.Size = new System.Drawing.Size(123, 21);
			this.cbEstadoConservacao.TabIndex = 1;
			//
			//grpMateriais
			//
			this.grpMateriais.Controls.Add(this.btnMaterialManager);
			this.grpMateriais.Controls.Add(this.chkLstMateriais);
			this.grpMateriais.Location = new System.Drawing.Point(4, 52);
			this.grpMateriais.Name = "grpMateriais";
			this.grpMateriais.Size = new System.Drawing.Size(188, 91);
			this.grpMateriais.TabIndex = 4;
			this.grpMateriais.TabStop = false;
			this.grpMateriais.Text = "Materiais";
			//
			//btnMaterialManager
			//
			this.btnMaterialManager.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnMaterialManager.Location = new System.Drawing.Point(156, 16);
			this.btnMaterialManager.Name = "btnMaterialManager";
			this.btnMaterialManager.Size = new System.Drawing.Size(24, 23);
			this.btnMaterialManager.TabIndex = 2;
			//
			//chkLstMateriais
			//
			this.chkLstMateriais.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.chkLstMateriais.IntegralHeight = false;
			this.chkLstMateriais.Location = new System.Drawing.Point(8, 17);
			this.chkLstMateriais.MultiColumn = true;
			this.chkLstMateriais.Name = "chkLstMateriais";
			this.chkLstMateriais.Size = new System.Drawing.Size(144, 66);
			this.chkLstMateriais.TabIndex = 1;
			//
			//grpTecnicasRegisto
			//
			this.grpTecnicasRegisto.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.grpTecnicasRegisto.Controls.Add(this.Button1);
			this.grpTecnicasRegisto.Controls.Add(this.chkLstTecnicasRegisto);
			this.grpTecnicasRegisto.Location = new System.Drawing.Point(200, 52);
			this.grpTecnicasRegisto.Name = "grpTecnicasRegisto";
			this.grpTecnicasRegisto.Size = new System.Drawing.Size(206, 91);
			this.grpTecnicasRegisto.TabIndex = 5;
			this.grpTecnicasRegisto.TabStop = false;
			this.grpTecnicasRegisto.Text = "Técnicas de registo";
			//
			//Button1
			//
			this.Button1.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
			this.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.Button1.Location = new System.Drawing.Point(176, 16);
			this.Button1.Name = "Button1";
			this.Button1.Size = new System.Drawing.Size(24, 23);
			this.Button1.TabIndex = 2;
			//
			//chkLstTecnicasRegisto
			//
			this.chkLstTecnicasRegisto.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.chkLstTecnicasRegisto.IntegralHeight = false;
			this.chkLstTecnicasRegisto.Location = new System.Drawing.Point(8, 17);
			this.chkLstTecnicasRegisto.MultiColumn = true;
			this.chkLstTecnicasRegisto.Name = "chkLstTecnicasRegisto";
			this.chkLstTecnicasRegisto.Size = new System.Drawing.Size(162, 66);
			this.chkLstTecnicasRegisto.TabIndex = 1;
			//
			//FormDimensoes
			//
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(410, 187);
			this.ControlBox = false;
			this.Controls.Add(this.grpTecnicasRegisto);
			this.Controls.Add(this.grpMateriais);
			this.Controls.Add(this.grpEstadoConservacao);
			this.Controls.Add(this.grpFormaSuporte);
			this.Controls.Add(this.grpQuantidade);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormDimensoes";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Dimensões e suporte";
			this.grpQuantidade.ResumeLayout(false);
			this.grpFormaSuporte.ResumeLayout(false);
			this.grpEstadoConservacao.ResumeLayout(false);
			this.grpMateriais.ResumeLayout(false);
			this.grpTecnicasRegisto.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	#endregion
	}
} //end of root namespace