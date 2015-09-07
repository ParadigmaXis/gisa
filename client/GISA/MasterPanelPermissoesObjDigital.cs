using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
    public partial class MasterPanelPermissoesObjDigital : MasterPanelPermissoesPlanoClassificacao
    {
        public MasterPanelPermissoesObjDigital()
        {
            InitializeComponent();
        }

        protected override bool UpdateContext(GISADataset.NivelRow row)
        {
            bool result = CurrentContext.SetPermissoesObjDigital(row, currentUser);
            UpdateToolBarButtons();
            return result;
        }
    }
}
