using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GISA.IntGestDoc.UserInterface
{
    public partial class FormNewValue : Form
    {
        public FormNewValue()
        {
            InitializeComponent();
        }

        public string NewValueString
        {
            get { return txtValue.Text; }
            set { txtValue.Text = value; }
        }

        public string NewValueStartDay
        {
            get { return pxDateBoxInicio.ValueDay; }
            set { pxDateBoxInicio.ValueDay = value; }
        }

        public string NewValueStartMonth
        {
            get { return pxDateBoxInicio.ValueMonth; }
            set { pxDateBoxInicio.ValueMonth = value; }
        }

        public string NewValueStartYear
        {
            get { return pxDateBoxInicio.ValueYear; }
            set { pxDateBoxInicio.ValueYear = value; }
        }

        public string NewValueEndDay
        {
            get { return pxDateBoxFim.ValueDay; }
            set { pxDateBoxFim.ValueDay = value; }
        }

        public string NewValueEndMonth
        {
            get { return pxDateBoxFim.ValueMonth; }
            set { pxDateBoxFim.ValueMonth = value; }
        }

        public string NewValueEndYear
        {
            get { return pxDateBoxFim.ValueYear; }
            set { pxDateBoxFim.ValueYear = value; }
        }

        public void ShowDateField(bool show)
        {
            txtValue.Visible = !show;
            pxDateBoxInicio.Visible = show;
            pxDateBoxFim.Visible = show;
            label1.Visible = show;
        }

        private void pxDateBox1_PxDateBoxTextChanged()
        {
            btnOk.Enabled = (pxDateBoxInicio.Visible && (pxDateBoxInicio.ValueDay.Length > 0 || pxDateBoxInicio.ValueMonth.Length > 0 || pxDateBoxInicio.ValueYear.Length > 0)
                && pxDateBoxFim.Visible && (pxDateBoxFim.ValueDay.Length > 0 || pxDateBoxFim.ValueMonth.Length > 0 || pxDateBoxFim.ValueYear.Length > 0));
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = (txtValue.Visible && txtValue.Text.Length > 0);
        }
    }
}
