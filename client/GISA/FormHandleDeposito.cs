using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GISA
{
    public partial class FormHandleDeposito : Form
    {
        private static string CreateTitle = "Criar depósito";
        private static string EditTitle = "Editar depósito";
        public FormHandleDeposito()
        {
            InitializeComponent();
        }

        public void SetCreateTitle()
        {
            this.Text = CreateTitle;
        }

        public void SetEditTitle()
        {
            this.Text = EditTitle;
        }

        public string Designacao { get { return txtDesignacao.Text; } set { txtDesignacao.Text = value; } }
        public string Metragem { get { return txtMetrosLineares.Text; } set { txtMetrosLineares.Text = value; } }
    }
}
