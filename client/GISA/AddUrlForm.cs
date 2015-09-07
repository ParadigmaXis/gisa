using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.SharedResources;

namespace GISA
{
    public partial class AddUrlForm : Form
    {
        public AddUrlForm()
        {
            InitializeComponent();
            GetExtraResources();
        }

        private void GetExtraResources()
        {
            this.Text = SharedResourcesOld.CurrentSharedResources.AdicionarUrl;
            label1.Text = SharedResourcesOld.CurrentSharedResources.AdicionarUrlLabel;
        }

        public string URL { get { return textBox1.Text; } }
    }
}
