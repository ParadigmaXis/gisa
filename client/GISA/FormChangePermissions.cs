using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISA
{
    public partial class FormChangePermissions : Form
    {
        public FormChangePermissions()
        {
            InitializeComponent();
        }

        private void cbCriar_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshProperty(ref mCriar, (ComboBox)sender);
        }

        private void cbLer_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshProperty(ref mLer, (ComboBox)sender);
        }

        private void cbEscrever_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshProperty(ref mEscrever, (ComboBox)sender);
        }

        private void cbApagar_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshProperty(ref mApagar, (ComboBox)sender);
        }

        private void cbExpandir_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshProperty(ref mExpandir, (ComboBox)sender);
        }

        private void RefreshProperty(ref string property, ComboBox control)
        {
            if (control.SelectedItem != null)
                property = control.SelectedItem.ToString();
        }

        private string mCriar;
        private string mLer;
        private string mEscrever;
        private string mApagar;
        private string mExpandir;

        public string Criar { get { return mCriar; } }
        public string Ler { get { return mLer; } }
        public string Escrever { get { return mEscrever; } }
        public string Apagar { get { return mApagar; } }
        public string Expandir { get { return mExpandir; } }
    }
}
