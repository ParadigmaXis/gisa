using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GISA.GUIHelper
{
    public partial class FormImportErrorReport : FormReport
    {
        public FormImportErrorReport()
        {
            InitializeComponent();
            this.btnOK.Visible = false;
            this.Panel1.Controls.Add(this.tableLayoutPanel1);
        }

        internal override void btnDetails_Click(object sender, EventArgs e)
        {
            if (Panel2.Visible)
            {
                Panel2.Visible = false;
                this.Height = 158;
            }
            else
            {
                Panel2.Visible = true;
                this.Height = 165 + 158;
            }
        }

        public void SetTitle(string val) { this.Text = val; }
        public void SetTabela(string val) { this.lblTabelaVal.Text = val; }
        public void SetIdentificador(string val) { this.lblIdentificadorVal.Text = val; }
        public void SetColuna(string val) { this.lblColunaVal.Text = val; }
        public void SetValor(string val) { this.lblValorVal.Text = val; }
        public void SetErro(string val) { this.lblErroVal.Text = val; }
        public void SetAjuda(string val) { this.Detalhes = val; }
    }
}
