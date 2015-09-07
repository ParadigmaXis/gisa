using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

namespace GISA
{
    public partial class ControlLocalConsulta : UserControl
    {
        public ControlLocalConsulta()
        {
            InitializeComponent();
            btnLocalConsultaManager.Click += btnLocalConsultaManager_Click;
            GetExtraResources();
        }

        private void GetExtraResources()
        {
            this.btnLocalConsultaManager.Image = SharedResourcesOld.CurrentSharedResources.Editar;
            ToolTip1.SetToolTip(this.btnLocalConsultaManager, SharedResourcesOld.CurrentSharedResources.EditarString);
        }

        // Todos os autos de eliminação terão já de ter sido carregados para memória 
        // antes de ser pedido a população da combobox
        public void rebindToData()
        {
            cbLocalConsulta.DataSource = null;
            cbLocalConsulta.DataSource = getLocalConsultaDataSource();
            cbLocalConsulta.DisplayMember = "Designacao";
            cbLocalConsulta.ValueMember = "ID";
        }

        private DataTable getLocalConsultaDataSource()
        {
            DataTable dtLocalConsulta = new DataTable();
            dtLocalConsulta.Columns.Add("ID", typeof(long));
            dtLocalConsulta.Columns.Add("Designacao", typeof(string));
            dtLocalConsulta.Rows.Add(new object[] { long.MinValue, string.Empty }); // é utilizado o long.minvalue e não, por exemplo, -1 porque os novos autos de eliminação terão um id -1

            foreach (GISADataset.LocalConsultaRow row in GisaDataSetHelper.GetInstance().LocalConsulta.Select(string.Empty, "Designacao"))
                dtLocalConsulta.Rows.Add(new object[] { row["ID"], row["Designacao"] });

            return dtLocalConsulta;
        }

        private void btnLocalConsultaManager_Click(object sender, System.EventArgs e)
        {
            FormLocalConsultaEditor formAutoEditor = new FormLocalConsultaEditor();
            IDbConnection conn = GisaDataSetHelper.GetConnection();
            try
            {
                conn.Open();
                RelatorioRule.Current.LoadLocaisConsulta(GisaDataSetHelper.GetInstance(), conn);
            }
            finally
            {
                conn.Close();
            }

            formAutoEditor.LoadData(GisaDataSetHelper.GetInstance().LocalConsulta.Select(string.Empty, "Designacao"), "Designacao");
            formAutoEditor.ShowDialog();

            long selectedLocalConsultaID = 0;
            if (cbLocalConsulta.SelectedValue == null)
                selectedLocalConsultaID = long.MinValue;
            else
                selectedLocalConsultaID = (long)cbLocalConsulta.SelectedValue;

            rebindToData();
            cbLocalConsulta.SelectedValue = selectedLocalConsultaID;
        }
    }
}
