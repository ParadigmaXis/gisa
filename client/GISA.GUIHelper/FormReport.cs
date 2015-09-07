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
    public partial class FormReport : Form
    {
        public FormReport()
        {
            InitializeComponent();

            btnDetails.Click += btnDetails_Click;
        }

        virtual internal void btnDetails_Click(object sender, System.EventArgs e)
        {
            if (Panel2.Visible)
            {
                Panel2.Visible = false;
                this.Height = 140;
            }
            else
            {
                Panel2.Visible = true;
                this.Height = 140 + 165;
            }
        }

        public void SetBtnOKVisible(bool visible) { this.btnOK.Visible = visible; }

        public string Detalhes
        {
            get { return txtReport.Text; }
            set { txtReport.Text = value; }
        }
    }
}
