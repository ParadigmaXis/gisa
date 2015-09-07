using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GISA {
    public partial class FormLeituraPxDateBox : Form {
        public FormLeituraPxDateBox() {
            InitializeComponent();

            pxDateBox1.PxDateBoxTextChanged += data_TextChanged;
            base.Activated += FormLeituraPxDateBox_Activated;
            updateButtons();
        }

        private void data_TextChanged() {
            updateButtons();
        }

        private void updateButtons() {
            btnOk.Enabled = (!pxDateBox1.ValueYear.Equals(string.Empty) ||
                !pxDateBox1.ValueMonth.Equals(string.Empty) ||
                !pxDateBox1.ValueDay.Equals(string.Empty));
        }

        public string ValueYear {
            get { return this.pxDateBox1.ValueYear; }
            set { this.pxDateBox1.ValueYear = value; }
        }

        public string ValueMonth {
            get { return this.pxDateBox1.ValueMonth; }
            set { this.pxDateBox1.ValueMonth = value; }
        }

        public string ValueDay {
            get { return this.pxDateBox1.ValueDay;  }
            set { this.pxDateBox1.ValueDay = value; }
        }

        private void FormLeituraPxDateBox_Activated(object sender, System.EventArgs e) {
            pxDateBox1.Focus();
        }
    }
}
