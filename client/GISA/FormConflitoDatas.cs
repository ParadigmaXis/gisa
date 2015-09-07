using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA
{
	public class FormConflitoDatas : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormConflitoDatas() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call

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
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.TextBox txtDataInicioUF;
		internal System.Windows.Forms.TextBox txtDataInicioNivel;
		internal System.Windows.Forms.RadioButton rbDataInicioUF;
		internal System.Windows.Forms.RadioButton rbDataInicioNivel;
		internal System.Windows.Forms.TextBox txtDataFimUF;
		internal System.Windows.Forms.TextBox txtDataFimNivel;
		internal System.Windows.Forms.RadioButton rbDataFimUF;
		internal System.Windows.Forms.RadioButton rbDataFimNivel;
		internal System.Windows.Forms.GroupBox grpDataInicio;
		internal System.Windows.Forms.GroupBox grpDataFim;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.Label1 = new System.Windows.Forms.Label();
			this.grpDataInicio = new System.Windows.Forms.GroupBox();
			this.txtDataInicioUF = new System.Windows.Forms.TextBox();
			this.txtDataInicioNivel = new System.Windows.Forms.TextBox();
			this.rbDataInicioUF = new System.Windows.Forms.RadioButton();
			this.rbDataInicioNivel = new System.Windows.Forms.RadioButton();
			this.grpDataFim = new System.Windows.Forms.GroupBox();
			this.txtDataFimUF = new System.Windows.Forms.TextBox();
			this.txtDataFimNivel = new System.Windows.Forms.TextBox();
			this.rbDataFimUF = new System.Windows.Forms.RadioButton();
			this.rbDataFimNivel = new System.Windows.Forms.RadioButton();
			this.grpDataInicio.SuspendLayout();
			this.grpDataFim.SuspendLayout();
			this.SuspendLayout();
			//
			//btnOk
			//
			this.btnOk.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(320, 156);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "Aceitar";
			//
			//btnCancel
			//
			this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(408, 156);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancelar";
			this.btnCancel.Visible = false;
			//
			//Label1
			//
			this.Label1.Location = new System.Drawing.Point(16, 16);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(488, 32);
			this.Label1.TabIndex = 8;
			this.Label1.Text = "Alguns dos conflitos entre datas de produção não foram resolvidos automaticamente" + ".  Por favor escolha a(s) data(s) que devem ser usadas.";
			//
			//grpDataInicio
			//
			this.grpDataInicio.Controls.Add(this.txtDataInicioUF);
			this.grpDataInicio.Controls.Add(this.txtDataInicioNivel);
			this.grpDataInicio.Controls.Add(this.rbDataInicioUF);
			this.grpDataInicio.Controls.Add(this.rbDataInicioNivel);
			this.grpDataInicio.Location = new System.Drawing.Point(8, 56);
			this.grpDataInicio.Name = "grpDataInicio";
			this.grpDataInicio.Size = new System.Drawing.Size(240, 88);
			this.grpDataInicio.TabIndex = 1;
			this.grpDataInicio.TabStop = false;
			this.grpDataInicio.Text = "Data de início de produção";
			//
			//txtDataInicioUF
			//
			this.txtDataInicioUF.Location = new System.Drawing.Point(116, 52);
			this.txtDataInicioUF.Name = "txtDataInicioUF";
			this.txtDataInicioUF.ReadOnly = true;
			this.txtDataInicioUF.Size = new System.Drawing.Size(104, 20);
			this.txtDataInicioUF.TabIndex = 4;
			this.txtDataInicioUF.Text = "";
			//
			//txtDataInicioNivel
			//
			this.txtDataInicioNivel.Location = new System.Drawing.Point(116, 20);
			this.txtDataInicioNivel.Name = "txtDataInicioNivel";
			this.txtDataInicioNivel.ReadOnly = true;
			this.txtDataInicioNivel.Size = new System.Drawing.Size(104, 20);
			this.txtDataInicioNivel.TabIndex = 2;
			this.txtDataInicioNivel.Text = "";
			//
			//rbDataInicioUF
			//
			this.rbDataInicioUF.Location = new System.Drawing.Point(20, 52);
			this.rbDataInicioUF.Name = "rbDataInicioUF";
			this.rbDataInicioUF.TabIndex = 3;
			this.rbDataInicioUF.Text = "Unidade física:";
			//
			//rbDataInicioNivel
			//
			this.rbDataInicioNivel.Checked = true;
			this.rbDataInicioNivel.Location = new System.Drawing.Point(20, 20);
			this.rbDataInicioNivel.Name = "rbDataInicioNivel";
			this.rbDataInicioNivel.TabIndex = 1;
			this.rbDataInicioNivel.TabStop = true;
			this.rbDataInicioNivel.Text = "Atual:";
			//
			//grpDataFim
			//
			this.grpDataFim.Controls.Add(this.txtDataFimUF);
			this.grpDataFim.Controls.Add(this.txtDataFimNivel);
			this.grpDataFim.Controls.Add(this.rbDataFimUF);
			this.grpDataFim.Controls.Add(this.rbDataFimNivel);
			this.grpDataFim.Location = new System.Drawing.Point(256, 56);
			this.grpDataFim.Name = "grpDataFim";
			this.grpDataFim.Size = new System.Drawing.Size(240, 88);
			this.grpDataFim.TabIndex = 2;
			this.grpDataFim.TabStop = false;
			this.grpDataFim.Text = "Data de fim de produção";
			//
			//txtDataFimUF
			//
			this.txtDataFimUF.Location = new System.Drawing.Point(116, 52);
			this.txtDataFimUF.Name = "txtDataFimUF";
			this.txtDataFimUF.ReadOnly = true;
			this.txtDataFimUF.Size = new System.Drawing.Size(104, 20);
			this.txtDataFimUF.TabIndex = 4;
			this.txtDataFimUF.Text = "";
			//
			//txtDataFimNivel
			//
			this.txtDataFimNivel.Location = new System.Drawing.Point(116, 20);
			this.txtDataFimNivel.Name = "txtDataFimNivel";
			this.txtDataFimNivel.ReadOnly = true;
			this.txtDataFimNivel.Size = new System.Drawing.Size(104, 20);
			this.txtDataFimNivel.TabIndex = 2;
			this.txtDataFimNivel.Text = "";
			//
			//rbDataFimUF
			//
			this.rbDataFimUF.Location = new System.Drawing.Point(20, 52);
			this.rbDataFimUF.Name = "rbDataFimUF";
			this.rbDataFimUF.TabIndex = 3;
			this.rbDataFimUF.Text = "Unidade física:";
			//
			//rbDataFimNivel
			//
			this.rbDataFimNivel.Checked = true;
			this.rbDataFimNivel.Location = new System.Drawing.Point(20, 20);
			this.rbDataFimNivel.Name = "rbDataFimNivel";
			this.rbDataFimNivel.TabIndex = 1;
			this.rbDataFimNivel.TabStop = true;
			this.rbDataFimNivel.Text = "Atual:";
			//
			//FormConflitoDatas
			//
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 189);
			this.ControlBox = false;
			this.Controls.Add(this.grpDataFim);
			this.Controls.Add(this.grpDataInicio);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormConflitoDatas";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Conflitos entre datas de produção";
			this.grpDataInicio.ResumeLayout(false);
			this.grpDataFim.ResumeLayout(false);
			this.ResumeLayout(false);

		}

	#endregion

		public enum TipoDataInicio: int
		{
			DataInicioNivel = 1,
			DataInicioUF = 2
		}

		public enum TipoDataFim: int
		{
			DataFimNivel = 1,
			DataFimUF = 2
		}

	#region  datas escolhidas 

		public void setDataInicioEscolhida(TipoDataInicio tipoData)
		{
			if (tipoData == TipoDataInicio.DataInicioNivel)
			{
				rbDataInicioNivel.Checked = true;
			}
			else if (tipoData == TipoDataInicio.DataInicioUF)
			{
				rbDataInicioUF.Checked = true;
			}
		}
		public void setDataFimEscolhida(TipoDataFim tipoData)
		{
			if (tipoData == TipoDataFim.DataFimNivel)
			{
				rbDataFimNivel.Checked = true;
			}
			else if (tipoData == TipoDataFim.DataFimUF)
			{
				rbDataFimUF.Checked = true;
			}
		}

		public string AnoInicioEscolhido
		{
			get
			{
				if (rbDataInicioNivel.Checked)
				{
					return mAnoInicioNivel;
				}
				else if (rbDataInicioUF.Checked)
				{
					return mAnoInicioUF;
				}
				// should never happen
				return null;
			}
		}

		public string MesInicioEscolhido
		{
			get
			{
				if (rbDataInicioNivel.Checked)
				{
					return mMesInicioNivel;
				}
				else if (rbDataInicioUF.Checked)
				{
					return mMesInicioUF;
				}
				// should never happen
				return null;
			}
		}

		public string DiaInicioEscolhido
		{
			get
			{
				if (rbDataInicioNivel.Checked)
				{
					return mMesInicioNivel;
				}
				else if (rbDataInicioUF.Checked)
				{
					return mMesInicioUF;
				}
				// should never happen
				return null;
			}
		}

		public string AnoFimEscolhido
		{
			get
			{
				if (rbDataFimNivel.Checked)
				{
					return mAnoFimNivel;
				}
				else if (rbDataFimUF.Checked)
				{
					return mAnoFimUF;
				}
				// should never happen
				return null;
			}
		}

		public string MesFimEscolhido
		{
			get
			{
				if (rbDataFimNivel.Checked)
				{
					return mMesFimNivel;
				}
				else if (rbDataFimUF.Checked)
				{
					return mMesFimUF;
				}
				// should never happen
				return null;
			}
		}

		public string DiaFimEscolhido
		{
			get
			{
				if (rbDataFimNivel.Checked)
				{
					return mDiaFimNivel;
				}
				else if (rbDataFimUF.Checked)
				{
					return mDiaFimUF;
				}
				// should never happen
				return null;
			}
		}
	#endregion

	#region  Propriedades da data de fim do nivel 
		private string mAnoFimNivel;
		public string AnoFimNivel
		{
			get
			{
				return mAnoFimNivel;
			}
			set
			{
				mAnoFimNivel = value;
			}
		}

		private string mMesFimNivel;
		public string MesFimNivel
		{
			get
			{
				return mMesFimNivel;
			}
			set
			{
				mMesFimNivel = value;
			}
		}

		private string mDiaFimNivel;
		public string DiaFimNivel
		{
			get
			{
				return mDiaFimNivel;
			}
			set
			{
				mDiaFimNivel = value;
			}
		}

	#endregion

	#region  Propriedades da data de inicio do nivel 
		private string mAnoInicioNivel;
		public string AnoInicioNivel
		{
			get
			{
				return mAnoInicioNivel;
			}
			set
			{
				mAnoInicioNivel = value;
			}
		}

		private string mMesInicioNivel;
		public string MesInicioNivel
		{
			get
			{
				return mMesInicioNivel;
			}
			set
			{
				mMesInicioNivel = value;
			}
		}

		private string mDiaInicioNivel;
		public string DiaInicioNivel
		{
			get
			{
				return mDiaInicioNivel;
			}
			set
			{
				mDiaInicioNivel = value;
			}
		}
	#endregion

	#region  Propriedades da data de inicio das UFs 
		private string mAnoInicioUF;
		public string AnoInicioUF
		{
			get
			{
				return mAnoInicioUF;
			}
			set
			{
				mAnoInicioUF = value;
			}
		}

		private string mMesInicioUF;
		public string MesInicioUF
		{
			get
			{
				return mMesInicioUF;
			}
			set
			{
				mMesInicioUF = value;
			}
		}

		private string mDiaInicioUF;
		public string DiaInicioUF
		{
			get
			{
				return mDiaInicioUF;
			}
			set
			{
				mDiaInicioUF = value;
			}
		}
	#endregion

	#region  Propriedades da data de fim da UF 
		private string mAnoFimUF;
		public string AnoFimUF
		{
			get
			{
				return mAnoFimUF;
			}
			set
			{
				mAnoFimUF = value;
			}
		}

		private string mMesFimUF;
		public string MesFimUF
		{
			get
			{
				return mMesFimUF;
			}
			set
			{
				mMesFimUF = value;
			}
		}

		private string mDiaFimUF;
		public string DiaFimUF
		{
			get
			{
				return mDiaFimUF;
			}
			set
			{
				mDiaFimUF = value;
			}
		}

	#endregion


		public void CalculateDates()
		{
			txtDataInicioNivel.Text = ControloDateValidation.GetComposedDate(mAnoInicioNivel, mMesInicioNivel, mDiaInicioNivel);
			txtDataFimNivel.Text = ControloDateValidation.GetComposedDate(mAnoFimNivel, mMesFimNivel, mDiaFimNivel);
			txtDataInicioUF.Text = ControloDateValidation.GetComposedDate(mAnoInicioUF, mMesInicioUF, mDiaInicioUF);
			txtDataFimUF.Text = ControloDateValidation.GetComposedDate(mAnoFimUF, mMesFimUF, mDiaFimUF);
		}
	}


} //end of root namespace