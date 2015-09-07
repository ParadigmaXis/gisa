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
using GISA.Fedora.FedoraHandler;
using GISA.Fedora.FedoraHandler.Fedora.APIA;
using GISA.Model;

namespace GISA
{
    public partial class ControlFedoraEstatisticas : UserControl
    {
        public ControlFedoraEstatisticas()
        {
            InitializeComponent();
            InitializeDateControls();
            HideEditControls();
        }

        private List<ColumnHeader> columns;
        private void HideEditControls()
        {
            this.ch_editados.Width = 0;
            this.ch_uisCorrespondentesEditados.Width = 0;
            columns = listView1.Columns.OfType<ColumnHeader>().Where(ch => ch.Width > 0).ToList();

            txtTotalEditados.Visible = false;
            txtUIsCorrespondentesE.Visible = false;

            label7.Visible = false;
        }

        private void InitializeDateControls()
        {
            cdbDataInicio.Day = DateTime.Now.Day;
            cdbDataInicio.Month = DateTime.Now.Month;
            cdbDataInicio.Year = DateTime.Now.Year;

            cdbDataFim.Day = DateTime.Now.Day;
            cdbDataFim.Month = DateTime.Now.Month;
            cdbDataFim.Year = DateTime.Now.Year;
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            ClearTotals();
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (cdbDataInicio.Checked && cdbDataFim.Checked && cdbDataInicio.GetStandardMaskDate.CompareTo(cdbDataFim.GetStandardMaskDate) == 1)
                {
                    MessageBox.Show("A data de fim é anterior à data de início.", "Erro no filtro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (!SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.Connect())
                {
                    MessageBox.Show("Não foi possivel contactar o repositório.", "Ligação ao repositório", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                listView1.Items.Clear();

                var dataInicio = cdbDataInicio.Checked ? new DateTime(this.cdbDataInicio.Year, this.cdbDataInicio.Month, this.cdbDataInicio.Day) : DateTime.MinValue;
                var dataFim = cdbDataFim.Checked ? new DateTime(this.cdbDataFim.Year, this.cdbDataFim.Month, this.cdbDataFim.Day).AddHours(23).AddMinutes(59).AddSeconds(59) : DateTime.MaxValue;

                Dictionary<string, int> cnt_ods_created_per_user;
                Dictionary<string, int> cnt_od_edited_per_user;
                long ods_created_total;
                long ods_edited_total;
                Dictionary<string, int> cnt_uis_touched_ods_created_per_user;
                Dictionary<string, int> cnt_uis_touched_ods_edited_per_user;
                long uis_touched_ods_created_total;
                long uis_touched_ods_edited_total;
                var uisPid = new Dictionary<string, string>();

                var histLst = new Dictionary<string, List<Historico>>();
                var objs = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.Search(dataInicio, dataFim, out histLst, false);

                if (objs == null || objs.Count == 0) return;

                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try
                {
                    uisPid = EstatisticasRule.Current.GetGisaIDs(objs.Select(o => o.pid).ToList(), ho.Connection);
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

                ProcessData(dataInicio, dataFim, objs, histLst, out cnt_ods_created_per_user, out cnt_od_edited_per_user, out ods_created_total, out ods_edited_total,
                    out cnt_uis_touched_ods_created_per_user, out cnt_uis_touched_ods_edited_per_user, out uis_touched_ods_created_total, out uis_touched_ods_edited_total, uisPid);

                var items = new List<ListViewItem>();
#if DEBUG
                var trustees = GisaDataSetHelper.GetInstance().Trustee.Cast<GISADataset.TrusteeRow>().Where(r => r.IsActive && r.isDeleted == 0 && r.CatCode.Equals("USR"));
#else
                var trustees = GisaDataSetHelper.GetInstance().Trustee.Cast<GISADataset.TrusteeRow>().Where(r => !r.BuiltInTrustee && r.IsActive && r.isDeleted == 0 && r.CatCode.Equals("USR"));
#endif
                foreach (var user in trustees.OrderBy(t => t.Name))
                {
                    var lvItem = new ListViewItem(new string[] { user.Name, "0", "0", "0", "0" });
                    if (cnt_ods_created_per_user.ContainsKey(user.Name))
                        lvItem.SubItems[this.ch_criados.Index].Text = cnt_ods_created_per_user[user.Name].ToString();
                    if (cnt_uis_touched_ods_created_per_user.ContainsKey(user.Name))
                        lvItem.SubItems[this.ch_uisCorrespondentesCriados.Index].Text = cnt_uis_touched_ods_created_per_user[user.Name].ToString();
                    if (cnt_od_edited_per_user.ContainsKey(user.Name))
                        lvItem.SubItems[this.ch_editados.Index].Text = cnt_od_edited_per_user[user.Name].ToString();
                    if (cnt_uis_touched_ods_edited_per_user.ContainsKey(user.Name))
                        lvItem.SubItems[this.ch_uisCorrespondentesEditados.Index].Text = cnt_uis_touched_ods_edited_per_user[user.Name].ToString();
                    items.Add(lvItem);
                }

                listView1.BeginUpdate();
                listView1.Items.AddRange(items.ToArray());
                listView1.EndUpdate();

                txtTotalCriados.Text = ods_created_total.ToString();
                txtTotalEditados.Text = ods_edited_total.ToString();

                txtUIsCorrespondentesC.Text = uis_touched_ods_created_total.ToString();
                txtUIsCorrespondentesE.Text = uis_touched_ods_edited_total.ToString();
            }
            catch (Exception) { throw; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void ClearTotals()
        {
            txtTotalCriados.Text = "0";
            txtTotalEditados.Text = "0";
            txtUIsCorrespondentesC.Text = "0";
            txtUIsCorrespondentesE.Text = "0";
        }

        private const string TIMESTAMP_FORMAT = "yyyy-MM-dd\"T\"HH\":\"mm\":\"ss\".\"fff\"Z\"";
        private static void ProcessData(DateTime dataInicio, DateTime dataFim, List<ObjectFields> objs, Dictionary<string, List<Historico>> histLst,
            out Dictionary<string, int> cnt_ods_created_per_user, out Dictionary<string, int> cnt_ods_edited_per_user, out long ods_created_total, out long ods_edited_total,
            out Dictionary<string, int> cnt_uis_touched_ods_created_per_user, out Dictionary<string, int> cnt_uis_touched_ods_edited_per_user, 
            out long uis_touched_ods_created_total, out long uis_touched_ods_edited_total, Dictionary<string, string> uisPid)
        {
            cnt_ods_created_per_user = new Dictionary<string, int>();
            cnt_ods_edited_per_user = new Dictionary<string, int>();
            ods_created_total = 0;
            ods_edited_total = 0;
            cnt_uis_touched_ods_created_per_user = new Dictionary<string,int>();
            cnt_uis_touched_ods_edited_per_user = new Dictionary<string, int>();
            uis_touched_ods_created_total = 0;
            uis_touched_ods_edited_total = 0;

            var ods_created_per_user = new Dictionary<string, HashSet<string>>();
            var ods_edited_per_user = new Dictionary<string, HashSet<string>>();
            var ods_created = new HashSet<string>();
            var ods_edited = new HashSet<string>();

            var hist = new List<Historico>();
            var created = DateTime.MinValue;
            var modified = DateTime.MinValue;
            var timestamp = DateTime.MinValue;
            
            foreach (ObjectFields obj in objs)
            {
                if (!uisPid.ContainsKey(obj.pid))
                {
                    Trace.WriteLine("<<EstatísticasFedora>> O pid " + obj.pid + "não foi considerado porque não está registado no Gisa.");
                    continue;
                }

                created = DateTime.ParseExact(obj.cDate, TIMESTAMP_FORMAT, null);
                modified = DateTime.ParseExact(obj.mDate, TIMESTAMP_FORMAT, null);

                /*
                 * Contabilizações totais
                 */
 
                // contablização total; edits
                if (created != modified)
                    ods_edited.Add(obj.pid);

                // contablização total; creates
                if (created.CompareTo(dataInicio) >= 0 && created.CompareTo(dataFim) <= 0)
                    ods_created.Add(obj.pid);

                /*
                 * Contabilizações por utilizador
                 */

                hist = histLst.ContainsKey(obj.pid) ? histLst[obj.pid] : new List<Historico>();                

                // contabilizar create
                if (created.CompareTo(dataInicio) >= 0 && created.CompareTo(dataFim) <= 0)
                {
                    var usr = hist.Last().user; // a lista está ordenada por data de registo, da mais recente para a mais antiga
                    // contabilizar ods criados
                    CountFact(ods_created_per_user, obj.pid, usr);
                }

                // remover o registo referente à criação do objecto digital para não ser considerado como edição
                hist.RemoveAt(hist.Count - 1);

                // contabilizar edits
                foreach (var reg in hist)
                {
                    timestamp = DateTime.ParseExact(reg.timestamp, TIMESTAMP_FORMAT, null);
                    
                    //// não considerar a o registo referente à criação do OD
                    //// NOTA: O timestamp do primeiro registo do histórico (do mets) nunca é, potencialmente, igual ao do registo do OD. 
                    ////       Para se tentar identificar o registo correspondente ao da criação espera-se que a diferença seja sempre <= 999ms
                    //if (timestamp.CompareTo(created) == 0 || (timestamp - created).Duration().CompareTo(new TimeSpan(0, 0, 0, 0, 999)) <= 0)
                    //    continue;
                    
                    // considerar somente os registos que estejam dentro do intervalo definido
                    if (timestamp.CompareTo(dataInicio) >= 0 && timestamp.CompareTo(dataFim) <= 0)
                        // contabilizar ods editados
                        CountFact(ods_edited_per_user, obj.pid, reg.user);
                }
            }

            ods_created_total = ods_created.Count;
            ods_edited_total = ods_edited.Count;

            uis_touched_ods_created_total = ods_created.Select(od => uisPid[od]).Distinct().Count();
            uis_touched_ods_edited_total = ods_edited.Select(od => uisPid[od]).Distinct().Count();

            cnt_ods_created_per_user = ods_created_per_user.ToDictionary(usr => usr.Key, usr => usr.Value.Count);
            cnt_ods_edited_per_user = ods_edited_per_user.ToDictionary(usr => usr.Key, usr => usr.Value.Count);

            cnt_uis_touched_ods_created_per_user = ods_created_per_user.ToDictionary(usr => usr.Key, usr => usr.Value.Select(od => uisPid[od]).Distinct().Count());
            cnt_uis_touched_ods_edited_per_user = ods_edited_per_user.ToDictionary(usr => usr.Key, usr => usr.Value.Select(od => uisPid[od]).Distinct().Count());
        }

        private static void CountFact(Dictionary<string, HashSet<string>> dict, string value, string usr)
        {
            if (dict.ContainsKey(usr))
            {
                if (!dict[usr].Contains(value))
                    dict[usr].Add(value);
            }
            else
                dict[usr] = new HashSet<string>() { value };
        }

        public string GetStatisticsText()
        {
            StringBuilder periodo = new StringBuilder();
            var from = cdbDataInicio.Checked ? new DateTime(this.cdbDataInicio.Year, this.cdbDataInicio.Month, this.cdbDataInicio.Day) : DateTime.MinValue;
            var to = cdbDataFim.Checked ? new DateTime(this.cdbDataFim.Year, this.cdbDataFim.Month, this.cdbDataFim.Day) : DateTime.MaxValue;
            if (from != DateTime.MinValue || to != DateTime.MaxValue)
                periodo.Append(string.Format("Período:\t{0}\t{1}", from.ToShortDateString(), to.ToShortDateString()));

            return periodo.ToString() + "\n" + GetTotaisPeriodo() + "\n" + GetTotais() + "\n" +
                "Gerado em: " + DateTime.Now.ToString();
        }

        private string GetTotaisPeriodo()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("Objetos digitais no período:");
            foreach (ListViewItem item in listView1.Items)
            {
                var s = "";
                foreach (var col in columns)
                    s += item.SubItems[col.Index].Text + "\t";
                str.AppendLine(s);
            }
            str.AppendLine();

            return str.ToString();
        }

        private string GetTotais()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("Total objetos digitais (Unidades informacionais correspondentes): ");
            str.AppendLine(string.Format("   - Criados: {0}({1})", txtTotalCriados.Text, txtUIsCorrespondentesC.Text));
            //str.AppendLine(string.Format("   - Editados: {0}({1})", txtTotalEditados.Text, txtUIsCorrespondentesE.Text));
            return str.ToString();
        }
    }
}
