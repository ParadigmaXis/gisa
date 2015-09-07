using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISA
{
    public partial class FormObjDigitalChangePermissions : Form
    {
        public FormObjDigitalChangePermissions()
        {
            InitializeComponent();
        }

        private void cbLer_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshProperty(ref mLer, (ComboBox)sender);
        }

        private void cbEscrever_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshProperty(ref mEscrever, (ComboBox)sender);
        }

        private void RefreshProperty(ref string property, ComboBox control)
        {
            if (control.SelectedItem != null)
                property = control.SelectedItem.ToString();
        }

        private string mLer;
        private string mEscrever;

        public string Ler { get { return mLer; } }
        public string Escrever { get { return mEscrever; } }
    }
}
