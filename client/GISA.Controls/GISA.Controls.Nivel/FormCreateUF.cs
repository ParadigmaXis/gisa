using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using GISA.Model;

namespace GISA.Controls.Nivel
{
	public class FormCreateUF : FormAddNivel
	{

	#region  Windows Form Designer generated code 

		public FormCreateUF() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            cbEntidadeDetentora.SelectedIndexChanged += cbEntidadeDetentora_SelectedIndexChanged;
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
        public System.Windows.Forms.ComboBox cbEntidadeDetentora;
        public System.Windows.Forms.GroupBox GroupBox1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.cbEntidadeDetentora = new System.Windows.Forms.ComboBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.grpTitulo.SuspendLayout();
            this.grpCodigo.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.Enabled = false;
            // 
            // grpTitulo
            // 
            this.grpTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.grpTitulo.Location = new System.Drawing.Point(6, 100);
            this.grpTitulo.TabIndex = 3;
            // 
            // txtDesignacao
            // 
            this.txtDesignacao.TabIndex = 1;
            // 
            // grpCodigo
            // 
            this.grpCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.grpCodigo.Location = new System.Drawing.Point(6, 52);
            this.grpCodigo.TabIndex = 2;
            this.grpCodigo.Text = "Código";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(488, 157);
            this.btnCancel.TabIndex = 6;
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.Location = new System.Drawing.Point(408, 157);
            this.btnAccept.TabIndex = 5;
            // 
            // cbEntidadeDetentora
            // 
            this.cbEntidadeDetentora.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEntidadeDetentora.Location = new System.Drawing.Point(6, 16);
            this.cbEntidadeDetentora.Name = "cbEntidadeDetentora";
            this.cbEntidadeDetentora.Size = new System.Drawing.Size(542, 21);
            this.cbEntidadeDetentora.Sorted = true;
            this.cbEntidadeDetentora.TabIndex = 1;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.cbEntidadeDetentora);
            this.GroupBox1.Location = new System.Drawing.Point(6, 3);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(554, 43);
            this.GroupBox1.TabIndex = 1;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Entidade detentora";
            // 
            // FormCreateUF
            // 
            this.ClientSize = new System.Drawing.Size(570, 188);
            this.Controls.Add(this.GroupBox1);
            this.Name = "FormCreateUF";
            this.Controls.SetChildIndex(this.grpCodigo, 0);
            this.Controls.SetChildIndex(this.grpTitulo, 0);
            this.Controls.SetChildIndex(this.GroupBox1, 0);
            this.Controls.SetChildIndex(this.btnAccept, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.grpTitulo.ResumeLayout(false);
            this.grpTitulo.PerformLayout();
            this.grpCodigo.ResumeLayout(false);
            this.grpCodigo.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

        private List<GISADataset.NivelDesignadoRow> mEntidadeDetentoraList = null;
        public List<GISADataset.NivelDesignadoRow> EntidadeDetentoraList
        {
            set
            {
                mEntidadeDetentoraList = value;
            }
        }

		public GISADataset.NivelDesignadoRow EntidadeDetentora
		{
			get {return (GISADataset.NivelDesignadoRow)cbEntidadeDetentora.SelectedItem;}
			set
			{
				// keep original value
				cbEntidadeDetentora.Tag = value;
				// select it in the combo
				cbEntidadeDetentora.SelectedItem = value;
			}
		}

		public string Designacao
		{
			get {return txtDesignacao.Text;}
		}

		public GISADataset.NivelDesignadoRow NivelDesignado
		{
			set 
			{
				txtDesignacao.Tag = value;
				txtDesignacao.Text = value.Designacao;
			}
		}

		private string mCodigo = "";
		public string Codigo
		{
			get {return mCodigo;}
			set
			{
				mCodigo = value;

				string CodigoED = ((GISADataset.NivelDesignadoRow)cbEntidadeDetentora.SelectedItem).NivelRow.Codigo;
				txtCodigo.Text = CodigoED + "/" + value;
			}
		}

		public void ReloadData()
		{
            if (mEntidadeDetentoraList == null)
                mEntidadeDetentoraList = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>()
                    .Where(n => (n.RowState != DataRowState.Deleted || n.RowState != DataRowState.Detached) && n.GetNivelDesignadoRows().Count() == 1 
                        && n.IDTipoNivel == TipoNivel.LOGICO && n.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().Length == 0)
                    .Select(n => n.GetNivelDesignadoRows().First()).ToList();
            cbEntidadeDetentora.Items.AddRange(mEntidadeDetentoraList.ToArray());
			cbEntidadeDetentora.DisplayMember = "Designacao";
			if (cbEntidadeDetentora.Items.Count > 0)
				cbEntidadeDetentora.SelectedIndex = 0;
        }

		protected override void UpdateButtonState()
		{
			btnAccept.Enabled = Codigo.Length > 0 && txtDesignacao.Text.Length > 0 && cbEntidadeDetentora.SelectedItem != null;
		}

		private void cbEntidadeDetentora_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UpdateButtonState();
			Codigo = GetNextCodigoString(((GISADataset.NivelDesignadoRow)cbEntidadeDetentora.SelectedItem).NivelRow, System.DateTime.Now.Year);
		}

		private string GetNextCodigoString(GISADataset.NivelRow nivelEDRow, int ano)
		{
			// if it is a new row. generate new code
			// *OR*
			// it is an existing row being edited. generate new code only if entidade detentora is diferent than the original
			if (txtDesignacao.Tag == null || ((GISADataset.NivelDesignadoRow)txtDesignacao.Tag).ID == -1 || (((GISADataset.NivelDesignadoRow)txtDesignacao.Tag).ID != -1 && ((GISADataset.NivelDesignadoRow)cbEntidadeDetentora.SelectedItem).ID != ((GISADataset.NivelDesignadoRow)cbEntidadeDetentora.Tag).ID))
				return UnidadesFisicasHelper.GenerateNewCodigoString(nivelEDRow, ano);
			else
			{
				// restore original
				return ((GISADataset.NivelDesignadoRow)txtDesignacao.Tag).NivelRow.Codigo;
			}
		}

		protected override void FocusFirstField()
		{
			cbEntidadeDetentora.Focus();
		}
	}
}