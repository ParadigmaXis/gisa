using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA
{
    public partial class PanelInfoEPs : UserControl 
    {
        public delegate string RFTBuilder(long IDNivel);
        private RFTBuilder theRTFBuilder;
        public RFTBuilder TheRTFBuilder {
            get { return this.theRTFBuilder; }
            set { this.theRTFBuilder = value; }
        }

        public PanelInfoEPs() {
            InitializeComponent();

            controloLocalizacao1.grpLocalizacao.Text = "Localização na estrutura arquivística";
            controloLocalizacao1.trVwLocalizacao.AfterSelect += trVwLocalizacao_AfterSelect;
        }

        public void BuildTree(GISADataset.NivelRow nivelRow, IDbConnection connection) {
            this.controloLocalizacao1.BuildTree(nivelRow.ID, connection, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
        }

        public void ClearAll() {
            this.controloLocalizacao1.ClearTree();
            this.richTextBox1.Clear();
        }

        private void trVwLocalizacao_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e) {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                this.richTextBox1.Clear();
                NivelRule.MyNode zeeNode = (NivelRule.MyNode)e.Node.Tag;

                if (zeeNode.TipoNivelRelacionado > 2 && e.Node.GetNodeCount(false) != 0 && this.TheRTFBuilder != null)
                {
                    LoadNivel(zeeNode.IDNivel);
                    richTextBox1.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft Sans Serif;}}\\viewkind4\\uc1\\pard\\f0\\fs24 "
                        + this.TheRTFBuilder(zeeNode.IDNivel)
                        + "\\par}";
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void LoadNivel(long idNivel)
        {
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try{
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
