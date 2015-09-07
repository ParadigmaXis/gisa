using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Model;
using GISA.IntGestDoc;
using GISA.IntGestDoc.Controllers;
using GISA.IntGestDoc.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

namespace GISA
{
    public partial class MasterPanelDocInPorto : GISA.MasterPanel
    {
        public static Bitmap FunctionImage
        {
            get
            {
                return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "Integracao_enabled_32x32.png");
            }
        }

        public MasterPanelDocInPorto()
        {
            InitializeComponent();

            this.lblFuncao.Text = "DocInPorto";
            base.ParentChanged += MasterPanelDocInPorto_ParentChanged;
        }
        
        public override void LoadData()
        {
            controlDocInPorto1.InitializeData();
            //controlDocInPorto1.LoadLastTimestamp();
        }

        public override void ModelToView()
        {
            //controlDocInPorto1.BuildSuggestions();
        }

        public override bool ViewToModel()
        {
            if (this.controlDocInPorto1.IsCorrespondenciaEdited())
            {
                var str = "Existem documentos marcados que ainda não foram integrados." + System.Environment.NewLine +
                    "Pretende integrar os documentos?";
                var result = MessageBox.Show(str, "Documentos marcados não integrados", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                    this.controlDocInPorto1.Save();
            }
            return true;
        }

        public override void Deactivate()
        {
        }

        private void MasterPanelDocInPorto_ParentChanged(object sender, System.EventArgs e)
        {
            if (this.Parent == null)
            {
                this.Visible = false;
                ViewToModel();
                Deactivate();
            }
            else
            {
                this.Visible = true;
                try
                {
                    LoadData();
                    ModelToView();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                    throw;
                }
            }
        }        
    }
}
