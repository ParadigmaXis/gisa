using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Model;
using GISA.SharedResources;

namespace GISA
{
    public partial class MasterPanelPermissoesDesposito : GISA.MasterPanelTrustee
    {
        public MasterPanelPermissoesDesposito()
        {
            InitializeComponent();

            this.ToolBar.Buttons.Clear();
        }

        protected override void UpdateTrustees(GISADataset.TrusteeRow tRow)
        {
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                TrusteeRule.Current.LoadTrusteesUsr(GisaDataSetHelper.GetInstance(), ho.Connection);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
            finally
            {
                ho.Dispose();
            }

            lstVwTrustees.Items.Clear();
            ListViewItem item = null;
            ListViewItem selItem = null;
            foreach (var t in GisaDataSetHelper.GetInstance().Trustee.Cast<GISADataset.TrusteeRow>().ToList())
            {
#if TESTING
               
                item = lstVwTrustees.Items.Add("");
                if (t == tRow)
                {
                    selItem = item;
                }
                UpdateListViewItem(item, t);
                if (t.BuiltInTrustee)
                {
                    item.ForeColor = System.Drawing.Color.Gray;
                }
#else
				if (! t.BuiltInTrustee && t.IsVisibleObject)
				{
					item = lstVwTrustees.Items.Add("");
					if (t == tRow)
					{
						selItem = item;
					}
					UpdateListViewItem(item, t);
				}
#endif
            }
            lstVwTrustees.Sort();
            if (selItem != null)
            {
                lstVwTrustees.EnsureVisible(selItem.Index);
                lstVwTrustees.selectItem(selItem);
            }
        }
    }
}
