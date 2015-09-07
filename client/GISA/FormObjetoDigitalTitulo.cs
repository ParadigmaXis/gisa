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
    public partial class FormObjetoDigitalTitulo : Form
    {
        public FormObjetoDigitalTitulo()
        {
            InitializeComponent();
        }

        public string Titulo { get { return txtODTitle.Text; } set { txtODTitle.Text = value; } }

        public void SetNewTitle()
        {
            this.Text = "Adcionar título";
        }

        public void SetEditTitle()
        {
            this.Text = "Editar título";
        }
    }
}
