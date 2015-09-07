using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using GISA.Utils;
using GISA.Controls;

namespace GISA
{
	public class DimensoesSuporte : System.Windows.Forms.UserControl
	{

	#region  Windows Form Designer generated code 

		public DimensoesSuporte() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
			PopulateControls();
		}

		//UserControl overrides dispose to clean up the component list.
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
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		internal System.Windows.Forms.ToolTip ToolTip1;
		internal System.Windows.Forms.GroupBox grpUnidade;
		internal System.Windows.Forms.ComboBox cbUnidade;
		internal System.Windows.Forms.GroupBox grpProfundidade;
		internal System.Windows.Forms.GroupBox grpAltura;
		internal System.Windows.Forms.GroupBox grpLargura;
		internal GISA.Controls.PxDecimalBox decimalBoxProfundidade;
		internal GISA.Controls.PxDecimalBox decimalBoxAltura;
		internal GISA.Controls.PxDecimalBox decimalBoxLargura;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grpUnidade = new System.Windows.Forms.GroupBox();
            this.cbUnidade = new System.Windows.Forms.ComboBox();
            this.grpProfundidade = new System.Windows.Forms.GroupBox();
            this.decimalBoxProfundidade = new GISA.Controls.PxDecimalBox();
            this.grpAltura = new System.Windows.Forms.GroupBox();
            this.decimalBoxAltura = new GISA.Controls.PxDecimalBox();
            this.grpLargura = new System.Windows.Forms.GroupBox();
            this.decimalBoxLargura = new GISA.Controls.PxDecimalBox();
            this.grpUnidade.SuspendLayout();
            this.grpProfundidade.SuspendLayout();
            this.grpAltura.SuspendLayout();
            this.grpLargura.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpUnidade
            // 
            this.grpUnidade.Controls.Add(this.cbUnidade);
            this.grpUnidade.Location = new System.Drawing.Point(278, 2);
            this.grpUnidade.Name = "grpUnidade";
            this.grpUnidade.Size = new System.Drawing.Size(69, 46);
            this.grpUnidade.TabIndex = 4;
            this.grpUnidade.TabStop = false;
            this.grpUnidade.Text = "Unidade";
            // 
            // cbUnidade
            // 
            this.cbUnidade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUnidade.Location = new System.Drawing.Point(8, 19);
            this.cbUnidade.Name = "cbUnidade";
            this.cbUnidade.Size = new System.Drawing.Size(54, 21);
            this.cbUnidade.TabIndex = 1;
            // 
            // grpProfundidade
            // 
            this.grpProfundidade.Controls.Add(this.decimalBoxProfundidade);
            this.grpProfundidade.Location = new System.Drawing.Point(186, 2);
            this.grpProfundidade.Name = "grpProfundidade";
            this.grpProfundidade.Size = new System.Drawing.Size(88, 46);
            this.grpProfundidade.TabIndex = 3;
            this.grpProfundidade.TabStop = false;
            this.grpProfundidade.Text = "Profundidade";
            // 
            // decimalBoxProfundidade
            // 
            this.decimalBoxProfundidade.DecimalNumbers = 3;
            this.decimalBoxProfundidade.Location = new System.Drawing.Point(8, 19);
            this.decimalBoxProfundidade.Name = "decimalBoxProfundidade";
            this.decimalBoxProfundidade.Size = new System.Drawing.Size(70, 20);
            this.decimalBoxProfundidade.TabIndex = 1;
            // 
            // grpAltura
            // 
            this.grpAltura.Controls.Add(this.decimalBoxAltura);
            this.grpAltura.Location = new System.Drawing.Point(3, 2);
            this.grpAltura.Name = "grpAltura";
            this.grpAltura.Size = new System.Drawing.Size(88, 46);
            this.grpAltura.TabIndex = 1;
            this.grpAltura.TabStop = false;
            this.grpAltura.Text = "Altura";
            // 
            // decimalBoxAltura
            // 
            this.decimalBoxAltura.DecimalNumbers = 3;
            this.decimalBoxAltura.Location = new System.Drawing.Point(8, 19);
            this.decimalBoxAltura.Name = "decimalBoxAltura";
            this.decimalBoxAltura.Size = new System.Drawing.Size(70, 20);
            this.decimalBoxAltura.TabIndex = 1;
            // 
            // grpLargura
            // 
            this.grpLargura.Controls.Add(this.decimalBoxLargura);
            this.grpLargura.Location = new System.Drawing.Point(96, 2);
            this.grpLargura.Name = "grpLargura";
            this.grpLargura.Size = new System.Drawing.Size(88, 46);
            this.grpLargura.TabIndex = 2;
            this.grpLargura.TabStop = false;
            this.grpLargura.Text = "Largura";
            // 
            // decimalBoxLargura
            // 
            this.decimalBoxLargura.DecimalNumbers = 3;
            this.decimalBoxLargura.Location = new System.Drawing.Point(8, 19);
            this.decimalBoxLargura.Name = "decimalBoxLargura";
            this.decimalBoxLargura.Size = new System.Drawing.Size(70, 20);
            this.decimalBoxLargura.TabIndex = 1;
            // 
            // DimensoesSuporte
            // 
            this.Controls.Add(this.grpUnidade);
            this.Controls.Add(this.grpProfundidade);
            this.Controls.Add(this.grpAltura);
            this.Controls.Add(this.grpLargura);
            this.Name = "DimensoesSuporte";
            this.Size = new System.Drawing.Size(349, 50);
            this.grpUnidade.ResumeLayout(false);
            this.grpProfundidade.ResumeLayout(false);
            this.grpAltura.ResumeLayout(false);
            this.grpLargura.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

		public string MedidaLargura
		{
			get
			{
				return decimalBoxLargura.Text.Trim();
			}
			set
			{
				decimalBoxLargura.Text = value.Trim();
			}
		}

		public string MedidaAltura
		{
			get
			{
				return decimalBoxAltura.Text.Trim();
			}
			set
			{
				decimalBoxAltura.Text = value.Trim();
			}
		}

		public string MedidaProfundidade
		{
			get
			{
				return decimalBoxProfundidade.Text.Trim();
			}
			set
			{
				decimalBoxProfundidade.Text = value.Trim();
			}
		}

		public GISADataset.TipoMedidaRow TipoMedida
		{
			get
			{
				try
				{
					if (MathHelper.IsDecimal(MedidaLargura) || MathHelper.IsDecimal(MedidaAltura) || MathHelper.IsDecimal(MedidaProfundidade))
					{
						return (GISADataset.TipoMedidaRow)cbUnidade.SelectedItem;
					}
					else
					{
						return null;
					}
				}
				catch (InvalidCastException)
				{
					return null;
				}
			}
			set
			{
				if (value != null)
				{
					cbUnidade.SelectedItem = value;
				}
			}
		}

		private void PopulateControls()
		{
			cbUnidade.DisplayMember = "Designacao";
			foreach (GISADataset.TipoMedidaRow rowMedida in GisaDataSetHelper.GetInstance().TipoMedida.Select("", "Designacao"))
			{
				cbUnidade.Items.Add(rowMedida);
			}
			if (cbUnidade.Items.Count > 0)
			{
				cbUnidade.SelectedIndex = 0;
			}
		}

		public void SelectFirstMedida()
		{
			if (cbUnidade.Items.Count > 0)
			{
				cbUnidade.SelectedIndex = 0;
			}
		}

		public void populateDimensoes(GISADataset.SFRDUFDescricaoFisicaRow descFisicaRow)
		{
			if (descFisicaRow.IsMedidaLarguraNull())
			{
				MedidaLargura = "";
			}
			else
			{
				MedidaLargura = descFisicaRow.MedidaLargura.ToString("0.000"); // três casas decimais
			}
			if (descFisicaRow.IsMedidaAlturaNull())
			{
				MedidaAltura = "";
			}
			else
			{
				MedidaAltura = descFisicaRow.MedidaAltura.ToString("0.000");
			}
			if (descFisicaRow.IsMedidaProfundidadeNull())
			{
				MedidaProfundidade = "";
			}
			else
			{
				MedidaProfundidade = descFisicaRow.MedidaProfundidade.ToString("0.000");
			}
		}

		//guardar a descricao fisica das unidades fisicas

		public void storeDimensoes(ref GISADataset.SFRDUFDescricaoFisicaRow descFisicaRow)
		{
			storeDimensoes(ref descFisicaRow, true);
		}

		public void storeDimensoes(ref GISADataset.SFRDUFDescricaoFisicaRow descFisicaRow, bool showWarnings)
		{
			bool valoresInvalidos = false;

			try
			{
				if (MathHelper.IsDecimal(MedidaLargura))
				{
					if (descFisicaRow.IsMedidaLarguraNull() || descFisicaRow.MedidaLargura != System.Convert.ToDecimal(MedidaLargura))
					{

						descFisicaRow.MedidaLargura = System.Convert.ToDecimal(MedidaLargura);
					}
				}
				else
				{
					throw new Exception("invalid decimal");
				}
			}
			catch (Exception)
			{
				if (! descFisicaRow.IsMedidaLarguraNull())
				{
					descFisicaRow["MedidaLargura"] = DBNull.Value;
					if (MedidaLargura.Length > 0)
					{
						valoresInvalidos = true;
					}
				}
			}

			try
			{
				if (MathHelper.IsDecimal(MedidaAltura))
				{
					if (descFisicaRow.IsMedidaAlturaNull() || descFisicaRow.MedidaAltura != System.Convert.ToDecimal(MedidaAltura))
					{
						descFisicaRow.MedidaAltura = System.Convert.ToDecimal(MedidaAltura);
					}
				}
				else
				{
					throw new Exception("invalid decimal");
				}
			}
			catch (Exception)
			{
				if (! descFisicaRow.IsMedidaAlturaNull())
				{
					descFisicaRow["MedidaAltura"] = DBNull.Value;
					if (MedidaAltura.Length != 0)
					{
						valoresInvalidos = true;
					}
				}
			}

			try
			{
				if (MathHelper.IsDecimal(MedidaProfundidade))
				{
					if (descFisicaRow.IsMedidaProfundidadeNull() || descFisicaRow.MedidaProfundidade != System.Convert.ToDecimal(MedidaProfundidade))
					{
						descFisicaRow.MedidaProfundidade = System.Convert.ToDecimal(MedidaProfundidade);
					}
				}
				else
				{
					throw new Exception("invalid decimal");
				}
			}
			catch (Exception)
			{
				if (! descFisicaRow.IsMedidaProfundidadeNull())
				{
					descFisicaRow["MedidaProfundidade"] = DBNull.Value;
					if (MedidaProfundidade.Length > 0)
					{
						valoresInvalidos = true;
					}
				}
			}

			if (descFisicaRow["MedidaLargura"] == DBNull.Value && descFisicaRow["MedidaAltura"] == DBNull.Value && descFisicaRow["MedidaProfundidade"] == DBNull.Value)
			{

				/*if (! descFisicaRow.IsIDTipoMedidaNull())
				{
					descFisicaRow["IDTipoMedida"] = DBNull.Value;
				}*/
			}
			else
			{
				try
				{
					if (descFisicaRow.IsIDTipoMedidaNull() || descFisicaRow.IDTipoMedida != TipoMedida.ID)
					{
						descFisicaRow.IDTipoMedida = TipoMedida.ID;
					}
				}
				catch (Exception)
				{
					if (! descFisicaRow.IsIDTipoMedidaNull())
					{
						descFisicaRow["IDTipoMedida"] = DBNull.Value;
					}
				}
			}

			if (valoresInvalidos && showWarnings)
			{
				MessageBox.Show("Foram especificados valores inválidos nas dimensões e/ou suporte.", "Dimensões", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		public void clear()
		{
            GUIHelper.GUIHelper.clearField(decimalBoxLargura);
            GUIHelper.GUIHelper.clearField(decimalBoxAltura);
            GUIHelper.GUIHelper.clearField(decimalBoxProfundidade);
            GUIHelper.GUIHelper.clearField(cbUnidade);
		}
	}

} //end of root namespace