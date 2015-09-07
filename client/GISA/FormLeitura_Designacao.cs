using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GISA {
    public partial class FormLeitura_Designacao : Form {

        public FormLeitura_Designacao() {
            InitializeComponent();

            btnOk.Click += btnOk_Click;
            txtDesignacao.TextChanged += txtDesignacao_TextChanged;
            base.Activated += FormLeitura_Designacao_Activated;
            //txtDesignacao.Focus();
            updateButtons();

        }

        public string Title {
            get { return this.Text; }
            set { this.Text = value; }
        }

        public string LabelTitle {
            get { return this.lblDesignacao.Text; }
            set { this.lblDesignacao.Text = value; }
        }

        public string Designacao
        {
            get { return this.txtDesignacao.Text.Trim(); }
            set { this.txtDesignacao.Text = value; }
        }

        private void btnOk_Click(object sender, System.EventArgs e) {
        }

        private void txtDesignacao_TextChanged(object sender, System.EventArgs e) {
            updateButtons();
        }

        private void updateButtons() {
            btnOk.Enabled = txtDesignacao.Text.Length > 0;
        }

        private void FormLeitura_Designacao_Activated(object sender, System.EventArgs e) {
            txtDesignacao.Focus();
        }

    }
}
