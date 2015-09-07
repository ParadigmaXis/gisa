using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA
{
    public partial class FormPickUser : Form
    {
        public FormPickUser()
        {
            InitializeComponent();

            LoadUtilizadores();
            UpdateButtons();            
        }

        private void LoadUtilizadores()
        {
            long start = 0;
            start = DateTime.Now.Ticks;

            List<TrusteeRule.User> users = new List<TrusteeRule.User>();
            // carregar todos os utilizadores para memória
            IDbConnection conn = GisaDataSetHelper.GetConnection();
            try
            {
                conn.Open();
                bool constraints = GisaDataSetHelper.GetInstance().EnforceConstraints;
                GisaDataSetHelper.ManageDatasetConstraints(false);
                users = TrusteeRule.Current.LoadUsers(GisaDataSetHelper.GetInstance(), conn);
                GisaDataSetHelper.ManageDatasetConstraints(constraints);
            }
            catch (ConstraintException ex)
            {
                Trace.WriteLine(ex);
                Debug.Assert(false, ex.ToString());
                GisaDataSetHelper.FixDataSet(GisaDataSetHelper.GetInstance(), conn);
            }
            finally
            {
                conn.Close();
            }

            Debug.WriteLine("<<Load Users>>: " + new TimeSpan(DateTime.Now.Ticks - start).ToString());

            start = DateTime.Now.Ticks;

            // popular a lista
            lstUtilizadores.BeginUpdate();
            ListViewItem item;
            GISADataset.TrusteeRow[] tRow;
            foreach (TrusteeRule.User u in users)
            {
                item = new ListViewItem(new string[] { string.Empty, string.Empty, string.Empty });

                tRow = (GISADataset.TrusteeRow[])GisaDataSetHelper.GetInstance().Trustee.Select("ID=" + u.userID.ToString());
                Debug.Assert(tRow.Length > 0);
                item.Tag = tRow[0];
                
                item.SubItems[chTrusteeName.Index].Text = u.userName;
                item.SubItems[chTipo.Index].Text = u.userType;
                item.SubItems[chAmbito.Index].Text = u.userInternalExternal;
                lstUtilizadores.Items.Add(item);
            }
            lstUtilizadores.EndUpdate();

            Debug.WriteLine("<<Populate Termos>>: " + new TimeSpan(DateTime.Now.Ticks - start).ToString());
        }

        private void UpdateButtons()
        {
            btnOk.Enabled = lstUtilizadores.SelectedItems.Count > 0;
        }

        public GISADataset.TrusteeRow tRow = null;
        private void lstUtilizadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstUtilizadores.SelectedItems.Count > 0)
                tRow = (GISADataset.TrusteeRow)lstUtilizadores.SelectedItems[0].Tag;

            UpdateButtons();
        }
    }
}
