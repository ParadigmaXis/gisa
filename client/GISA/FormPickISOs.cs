using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using GISA.Controls;

namespace GISA
{
	public class FormPickISOs : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

        public bool isAlphabet;

		private FormPickISOs() : base()
		{
            InitializeComponent();
            txtFiltroDesignacao.KeyDown += txtFiltroDesignacao_KeyDown;
		}

        public FormPickISOs(bool isAlphabet): base() {
            this.isAlphabet = isAlphabet;

            InitializeComponent();
            txtFiltroDesignacao.KeyDown += txtFiltroDesignacao_KeyDown;

            // Alterar as colunas com base no tipo de form que vamos apresentar
            if (isAlphabet) {
                this.lstVwAlf.Columns.Clear();

                this.colColuna1.Text = "Alfabeto";
                this.colColuna1.Width = 295;
                this.colColuna2.Text = "Cód.Alfa 2";
                this.colColuna2.Width = 80;
                this.colColuna3.Text = "Cód.Alfa 3";
                this.colColuna3.Width = 80;
                this.colColuna4.Text = "Cód.Numérico";
                this.colColuna4.Width = 80;

                this.lstVwAlf.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.colColuna1, this.colColuna2, this.colColuna3, this.colColuna4 });
            }
            else {
                this.lstVwAlf.Columns.Clear();

                this.colColuna1.Text = "Linguagem";
                this.colColuna1.Width = 295;
                this.colColuna2.Text = "Cód. Bibliográfico";
                this.colColuna2.Width = 121;
                this.colColuna3.Text = "Cód. Terminologia";
                this.colColuna3.Width = 121;

                this.lstVwAlf.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.colColuna1, this.colColuna2, this.colColuna3 });
            }
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
		internal System.Windows.Forms.Button btnAdicionar;
        internal GroupBox grpResultados;
        internal ListView lstVwAlf;
        internal ColumnHeader colColuna1;
        internal ColumnHeader colColuna2;
        internal ColumnHeader colColuna3;
        internal ColumnHeader colColuna4;
        internal GroupBox grpFiltro;
        internal Button btnAplicar;
        internal TextBox txtFiltroDesignacao;
        internal Label lblFiltroDesignacao;
		internal System.Windows.Forms.Button btnCancelar;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.grpResultados = new System.Windows.Forms.GroupBox();
            this.lstVwAlf = new System.Windows.Forms.ListView();
            this.colColuna1 = new System.Windows.Forms.ColumnHeader();
            this.colColuna2 = new System.Windows.Forms.ColumnHeader();
            this.colColuna3 = new System.Windows.Forms.ColumnHeader();
            this.colColuna4 = new System.Windows.Forms.ColumnHeader();
            this.grpFiltro = new System.Windows.Forms.GroupBox();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.txtFiltroDesignacao = new EnterEnabledTextBox(reloadList);
            this.lblFiltroDesignacao = new System.Windows.Forms.Label();
            this.grpResultados.SuspendLayout();
            this.grpFiltro.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdicionar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAdicionar.Enabled = false;
            this.btnAdicionar.Location = new System.Drawing.Point(476, 347);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(72, 24);
            this.btnAdicionar.TabIndex = 2;
            this.btnAdicionar.Text = "A&dicionar";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(557, 347);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(72, 24);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "&Cancelar";
            // 
            // grpResultados
            // 
            this.grpResultados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpResultados.Controls.Add(this.lstVwAlf);
            this.grpResultados.Location = new System.Drawing.Point(2, 72);
            this.grpResultados.Name = "grpResultados";
            this.grpResultados.Size = new System.Drawing.Size(630, 268);
            this.grpResultados.TabIndex = 5;
            this.grpResultados.TabStop = false;
            this.grpResultados.Text = "Alfabetos encontrados";
            // 
            // lstVwAlf
            // 
            this.lstVwAlf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstVwAlf.FullRowSelect = true;
            this.lstVwAlf.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstVwAlf.HideSelection = false;
            this.lstVwAlf.Location = new System.Drawing.Point(3, 16);
            this.lstVwAlf.Name = "lstVwAlf";
            this.lstVwAlf.Size = new System.Drawing.Size(624, 249);
            this.lstVwAlf.TabIndex = 1;
            this.lstVwAlf.UseCompatibleStateImageBehavior = false;
            this.lstVwAlf.View = System.Windows.Forms.View.Details;
            this.lstVwAlf.SelectedIndexChanged += new System.EventHandler(this.lstVwAlf_SelectedIndexChanged);
            // 
            // grpFiltro
            // 
            this.grpFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFiltro.Controls.Add(this.btnAplicar);
            this.grpFiltro.Controls.Add(this.txtFiltroDesignacao);
            this.grpFiltro.Controls.Add(this.lblFiltroDesignacao);
            this.grpFiltro.Location = new System.Drawing.Point(2, 2);
            this.grpFiltro.Name = "grpFiltro";
            this.grpFiltro.Size = new System.Drawing.Size(630, 64);
            this.grpFiltro.TabIndex = 4;
            this.grpFiltro.TabStop = false;
            this.grpFiltro.Text = "Filtro";
            // 
            // btnAplicar
            // 
            this.btnAplicar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAplicar.Location = new System.Drawing.Point(558, 32);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(64, 24);
            this.btnAplicar.TabIndex = 4;
            this.btnAplicar.Text = "&Aplicar";
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // txtFiltroDesignacao
            // 
            this.txtFiltroDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFiltroDesignacao.Location = new System.Drawing.Point(8, 32);
            this.txtFiltroDesignacao.Name = "txtFiltroDesignacao";
            this.txtFiltroDesignacao.Size = new System.Drawing.Size(538, 20);
            this.txtFiltroDesignacao.TabIndex = 1;
            // 
            // lblFiltroDesignacao
            // 
            this.lblFiltroDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFiltroDesignacao.Location = new System.Drawing.Point(8, 16);
            this.lblFiltroDesignacao.Name = "lblFiltroDesignacao";
            this.lblFiltroDesignacao.Size = new System.Drawing.Size(270, 16);
            this.lblFiltroDesignacao.TabIndex = 0;
            this.lblFiltroDesignacao.Text = "Designação";
            // 
            // FormPickAlphabet
            // 
            this.AcceptButton = this.btnAdicionar;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(634, 383);
            this.Controls.Add(this.grpResultados);
            this.Controls.Add(this.grpFiltro);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPickAlphabet";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.grpResultados.ResumeLayout(false);
            this.grpFiltro.ResumeLayout(false);
            this.grpFiltro.PerformLayout();
            this.ResumeLayout(false);

		}

	#endregion

		public string Title
		{
			get {return this.Text;}
			set {this.Text = value;}
		}

        private ListView.SelectedListViewItemCollection mSelectedItems;
        public ListView.SelectedListViewItemCollection SelectedItems {
            get { return mSelectedItems; }
            set { mSelectedItems = value; }
        }

        public string TextFilter
        {
            get
            {
                if (txtFiltroDesignacao.Text.Length == 0)
                    return string.Empty;
                else {
                    if (isAlphabet) return "ScriptNameEnglish LIKE '" + GisaDataSetHelper.EscapeLikeValue(txtFiltroDesignacao.Text) + "'";
                    else return "LanguageNameEnglish LIKE '" + GisaDataSetHelper.EscapeLikeValue(txtFiltroDesignacao.Text) + "'";
                }
            }
        }

        private void btnAplicar_Click(object Sender, EventArgs e)
        {
            reloadList();
            UpdateButton();
        }

        public void reloadList()
        {
            List<ListViewItem> items = new List<ListViewItem>();
            ListViewItem item = null;

            lstVwAlf.BeginUpdate();
            lstVwAlf.Items.Clear();

            if (isAlphabet) {
                // criar clone da tabela para permitir filtrar em case insensitive
                GISADataset.Iso15924DataTable dt = new GISADataset.Iso15924DataTable();
                dt.IDColumn.AutoIncrementSeed = 1;
                dt.CaseSensitive = false;
                foreach (GISADataset.Iso15924Row row in GisaDataSetHelper.GetInstance().Iso15924.Select())
                {
                    GISADataset.Iso15924Row rowIso15924Row = ((GISADataset.Iso15924Row)(dt.NewRow()));
                    object[] columnValuesArray = new object[] {
                        row.ID,
                        row.ScriptNameEnglish,
                        row.CodeAlpha2,
                        row.CodeAlpha3,
                        row.CodeNumeric,
                        row.Versao,
                        row.isDeleted};
                    rowIso15924Row.ItemArray = columnValuesArray;
                    dt.AddIso15924Row(rowIso15924Row);
                }
                dt.AcceptChanges();

                foreach (GISADataset.Iso15924Row row in dt.Select(TextFilter, "ScriptNameEnglish")) {
                    item = new ListViewItem();
                    items.Add(item);
                    item.Text = row.ScriptNameEnglish;
                    item.SubItems.Add(row.CodeAlpha2);
                    item.SubItems.Add(row.CodeAlpha3);
                    item.SubItems.Add(row.CodeNumeric.ToString());
                    item.Tag = row;
                }
            }
            else {
                // criar clone da tabela para permitir filtrar em case insensitive
                GISADataset.Iso639DataTable dt = new GISADataset.Iso639DataTable();
                dt.IDColumn.AutoIncrementSeed = 1;
                dt.CaseSensitive = false;
                foreach (GISADataset.Iso639Row row in GisaDataSetHelper.GetInstance().Iso639.Select("", "ID"))
                {
                    GISADataset.Iso639Row rowIso639Row = ((GISADataset.Iso639Row)(dt.NewRow()));
                    object[] columnValuesArray = new object[] {
                        row.ID,
                        row.LanguageNameEnglish,
                        row.BibliographicCodeAlpha3,
                        row.TerminologyCodeAlpha3,
                        row.Versao,
                        row.isDeleted};
                    rowIso639Row.ItemArray = columnValuesArray;
                    dt.AddIso639Row(rowIso639Row);
                }
                dt.AcceptChanges();

                foreach (GISADataset.Iso639Row row in dt.Select(TextFilter, "LanguageNameEnglish")) {
                    item = new ListViewItem();
                    items.Add(item);
                    item.Text = row.LanguageNameEnglish;
                    item.SubItems.Add(row.BibliographicCodeAlpha3);
                    item.SubItems.Add(row.TerminologyCodeAlpha3);
                    item.Tag = row;
                }
            }

            if (items.Count > 0)
                lstVwAlf.Items.AddRange(items.ToArray());

            lstVwAlf.EndUpdate();
        }

        private void txtFiltroDesignacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == Convert.ToInt32(Keys.Enter))
                reloadList();
        }

        private void lstVwAlf_SelectedIndexChanged(object sender, EventArgs e)
        {
            mSelectedItems = lstVwAlf.SelectedItems;
            UpdateButton();            
        }

        private void UpdateButton()
        {
            btnAdicionar.Enabled = lstVwAlf.SelectedItems.Count > 0;
        }

        internal class EnterEnabledTextBox : TextBox
        {
            internal delegate void EnterDelegate();
            private EnterDelegate enterHandle;
            internal EnterEnabledTextBox(EnterDelegate d) : base()
            {
                this.enterHandle = d;
            }
            
            protected override bool ProcessCmdKey(ref 
              System.Windows.Forms.Message m,
                  System.Windows.Forms.Keys k)
            {
                // detect the pushing (Msg) of Enter Key (k)

                if (m.Msg == 256 && k ==
                       System.Windows.Forms.Keys.Enter)
                {
                    this.enterHandle();
                    return true;
                }
                else
                {
                    return base.ProcessCmdKey(ref m, k);
                }
            }
        }
	}    
}