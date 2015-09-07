using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA.Controls.Nivel
{
	public class FormNivelDocumental : FormAddNivel
	{

	#region  Windows Form Designer generated code 

		public FormNivelDocumental() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            CheckBox1.CheckedChanged += CheckBox1_CheckedChanged;
            txtDesignacaoUF.TextChanged += TextBox1_TextChanged;

			UpdateControls();
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
		internal System.Windows.Forms.CheckBox CheckBox1;
		internal System.Windows.Forms.TextBox txtDesignacaoUF;
        internal System.Windows.Forms.CheckBox CheckBox2;
		internal System.Windows.Forms.GroupBox grpUFAssociada;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.txtDesignacaoUF = new System.Windows.Forms.TextBox();
            this.CheckBox2 = new System.Windows.Forms.CheckBox();
            this.grpUFAssociada = new System.Windows.Forms.GroupBox();
            this.grpTitulo.SuspendLayout();
            this.grpCodigo.SuspendLayout();
            this.grpUFAssociada.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(6, 20);
            // 
            // grpCodigo
            // 
            this.grpCodigo.Size = new System.Drawing.Size(360, 48);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(456, 161);
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(376, 161);
            // 
            // CheckBox1
            // 
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CheckBox1.Location = new System.Drawing.Point(7, 0);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(146, 17);
            this.CheckBox1.TabIndex = 6;
            this.CheckBox1.Text = "Unidade física associada";
            this.CheckBox1.UseVisualStyleBackColor = true;
            // 
            // txtDesignacaoUF
            // 
            this.txtDesignacaoUF.Location = new System.Drawing.Point(6, 23);
            this.txtDesignacaoUF.Name = "txtDesignacaoUF";
            this.txtDesignacaoUF.Size = new System.Drawing.Size(461, 20);
            this.txtDesignacaoUF.TabIndex = 7;
            // 
            // CheckBox2
            // 
            this.CheckBox2.AutoSize = true;
            this.CheckBox2.Checked = true;
            this.CheckBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox2.Location = new System.Drawing.Point(473, 25);
            this.CheckBox2.Name = "CheckBox2";
            this.CheckBox2.Size = new System.Drawing.Size(78, 17);
            this.CheckBox2.TabIndex = 8;
            this.CheckBox2.Text = "Sincronizar";
            this.CheckBox2.UseVisualStyleBackColor = true;
            // 
            // grpUFAssociada
            // 
            this.grpUFAssociada.Controls.Add(this.txtDesignacaoUF);
            this.grpUFAssociada.Controls.Add(this.CheckBox1);
            this.grpUFAssociada.Controls.Add(this.CheckBox2);
            this.grpUFAssociada.Location = new System.Drawing.Point(5, 100);
            this.grpUFAssociada.Name = "grpUFAssociada";
            this.grpUFAssociada.Size = new System.Drawing.Size(559, 51);
            this.grpUFAssociada.TabIndex = 10;
            this.grpUFAssociada.TabStop = false;
            // 
            // FormNivelDocumental
            // 
            this.ClientSize = new System.Drawing.Size(570, 190);
            this.Controls.Add(this.grpUFAssociada);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormNivelDocumental";
            this.Controls.SetChildIndex(this.grpUFAssociada, 0);
            this.Controls.SetChildIndex(this.grpCodigo, 0);
            this.Controls.SetChildIndex(this.btnAccept, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.grpTitulo, 0);
            this.grpTitulo.ResumeLayout(false);
            this.grpTitulo.PerformLayout();
            this.grpCodigo.ResumeLayout(false);
            this.grpCodigo.PerformLayout();
            this.grpUFAssociada.ResumeLayout(false);
            this.grpUFAssociada.PerformLayout();
            this.ResumeLayout(false);

		}

	#endregion

        public bool CreateUFAssociada { get { return CheckBox1.Checked; } }

        public string DesignacaoUF { get { return CheckBox1.Checked ? txtDesignacaoUF.Text : string.Empty; } }

        private GISADataset.NivelRow mNivelRow;
        public GISADataset.NivelRow NivelRow
        {
            get { return mNivelRow; }
            set { mNivelRow = value; }
        }

		protected override void UpdateButtonState()
		{
			base.UpdateButtonState();
			btnAccept.Enabled = btnAccept.Enabled && !(txtDesignacaoUF.Text.Trim().Length == 0 && CheckBox1.Checked);
		}

        public override void LoadData()
        {
            var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                DBAbstractDataLayer.DataAccessRules.NivelRule.Current.LoadTipoDocumento(GisaDataSetHelper.GetInstance(), mNivelRow.ID, ho.Connection);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
            finally
            {
                ho.Dispose();
            }
        }

		private void txtCodigo_TextChanged(object sender, System.EventArgs e)
		{
			UpdateButtonState();
		}

		private void txtDesignacao_TextChanged(object sender, System.EventArgs e)
		{
			if (CheckBox2.Checked)
			{
				txtDesignacaoUF.Text = txtDesignacao.Text;
			}
			UpdateButtonState();
		}

		private void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			UpdateControls();
			UpdateButtonState();
		}

		private void UpdateControls()
		{
			if (CheckBox1.Checked)
			{
				txtDesignacaoUF.Enabled = true;
				CheckBox2.Enabled = true;
			}
			else
			{
				txtDesignacaoUF.Enabled = false;
				CheckBox2.Enabled = false;
			}
		}

		private void TextBox1_TextChanged(object sender, System.EventArgs e)
		{
			SynchronizeTextBoxs();
			UpdateButtonState();
		}

		protected override void SynchronizeTextBoxs()
		{
			if (CheckBox1.Checked && CheckBox2.Checked)
			{
                MyRemoveHandlers();
				if (txtDesignacao.Focused)
				{
					txtDesignacaoUF.Text = txtDesignacao.Text;
				}
				else
				{
					txtDesignacao.Text = txtDesignacaoUF.Text;
				}
                MyAddHandlers();
			}
		}

		protected void MyAddHandlers()
		{
			base.AddHandlers();
			txtDesignacaoUF.TextChanged += TextBox1_TextChanged;
		}

		protected void MyRemoveHandlers()
		{
			base.RemoveHandlers();
			txtDesignacaoUF.TextChanged -= TextBox1_TextChanged;
		}
	}

} //end of root namespace