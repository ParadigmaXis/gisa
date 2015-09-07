using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.IntGestDoc.Model.EntidadesInternas;

namespace GISA.IntGestDoc.UserInterface
{
    public abstract partial class PropriedadeSuggestionPicker : UserControl
    {
        public PropriedadeSuggestionPicker()
        {
            InitializeComponent();
        }

        public abstract bool IgnoreOption { get; set; }
        public abstract void RefreshControl();
    }
}
