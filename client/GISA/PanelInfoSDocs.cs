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
using DBAbstractDataLayer.DataAccessRules;

namespace GISA
{
    public partial class PanelInfoSDocs : UserControl
    {
        private List<NivelRule.NivelDocumentalListItem> sdocs = new List<NivelRule.NivelDocumentalListItem>();
        private DataTable SDocsDataTable;

        private const int ID = 0;
        private const int DESIGNACAO = 1;
        private const int PRODUCAO = 2;

        public delegate string RFTBuilder(long IDNivel);
        private RFTBuilder theRTFBuilder;
        public RFTBuilder TheRTFBuilder
        {
            get { return this.theRTFBuilder; }
            set { this.theRTFBuilder = value; }
        }

        public PanelInfoSDocs()
        {
            InitializeComponent();

            ConfigDataGridView();

            dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
            this.richTextBox1.Resize += new System.EventHandler(this.richTextBox1_Resize);
        }

        private void ConfigDataGridView()
        {
            SDocsDataTable = new DataTable();
            SDocsDataTable.Columns.Add(new DataColumn("ID", typeof(long)));
            SDocsDataTable.Columns.Add(new DataColumn("Designação", typeof(string)));
            SDocsDataTable.Columns.Add(new DataColumn("Produção", typeof(string)));
        }

        public void GetSDocs(long IDNivel)
        {
            dataGridView1.SelectionChanged -= new EventHandler(dataGridView1_SelectionChanged);
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                sdocs = PesquisaRule.Current.GetSubDocumentos(IDNivel, ho.Connection);
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

            SDocsDataTable.Clear();
            sdocs.ForEach(sd =>
                {
                    var r = SDocsDataTable.NewRow();
                    r[ID] = sd.IDNivel;
                    r[DESIGNACAO] = sd.Designacao;
                    r[PRODUCAO] = GISA.Utils.GUIHelper.FormatDateInterval(
                        sd.InicioAno, sd.InicioMes, sd.InicioDia, sd.InicioAtribuida,
                        sd.FimAno, sd.FimMes, sd.FimDia, sd.FimAtribuida);
                    SDocsDataTable.Rows.Add(r);
                });

            SDocsDataTable.AcceptChanges();
            dataGridView1.DataSource = SDocsDataTable;

            dataGridView1.Columns[ID].Width = 60;
            dataGridView1.Columns[DESIGNACAO].Width = 500;
            dataGridView1.Columns[PRODUCAO].MinimumWidth = 140;
            dataGridView1.Columns[PRODUCAO].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.ClearSelection();

            dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
        }

        public void Clear()
        {
            this.SDocsDataTable.Clear();
            this.richTextBox1.Clear();
        }

        private void dataGridView1_SelectionChanged(object sender, System.EventArgs e)
        {
            var selItem = dataGridView1.SelectedRows.Count == 1;            

            if (selItem)
            {
                var item = dataGridView1.SelectedRows[0];
                var IDNivel = long.Parse(item.Cells[0].Value.ToString());

                this.Cursor = Cursors.WaitCursor;
                try
                {
                    this.richTextBox1.Clear();                    

                    if (this.TheRTFBuilder != null)
                    {
                        LoadNivel(IDNivel);
                        richTextBox1.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft Sans Serif;}}\\viewkind4\\uc1\\pard\\f0\\fs24 "
                            + this.TheRTFBuilder(IDNivel)
                            + "\\par}";
                    }
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void LoadNivel(long idNivel)
        {
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                DBAbstractDataLayer.DataAccessRules.NivelRule.Current.LoadNivelParents(idNivel, GisaDataSetHelper.GetInstance(), ho.Connection);
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
        }

        private void richTextBox1_Resize(object sender, EventArgs e)
        {
            this.richTextBox1.Invalidate();
        }
    }
}
