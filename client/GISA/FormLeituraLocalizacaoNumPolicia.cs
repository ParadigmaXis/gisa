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
using GISA.Controls.ControloAut;

namespace GISA {
    public partial class FormLeituraLocalizacaoNumPolicia : Form {

        private GISADataset.ControloAutDicionarioRow cadRow = null;
        private bool modoTextoLivre = false;

        public FormLeituraLocalizacaoNumPolicia() {
            InitializeComponent();
            this.Title = "Localização da obra";
            
            btnOk.Click += btnOk_Click;
            
            button_FormPickControloAut.Enabled = !modoTextoLivre;
            txtDesignacao.Enabled = modoTextoLivre;
            txtDesignacao.TextChanged += txtDesignacao_TextChanged;
            base.Activated += FormLeituraLocalizacaoNumPolicia_Activated;

            updateButtons();

        }

        public bool ModoTextoLivre {
            get {return modoTextoLivre; }

            set { modoTextoLivre = value; 
                  button_FormPickControloAut.Enabled = !modoTextoLivre;
                  txtDesignacao.Enabled = modoTextoLivre;
            }
        }

        public string Title {
            get { return this.Text; }
            set { this.Text = value; }
        }

        public GISADataset.ControloAutDicionarioRow ControloAutDicionarioRow {
            get { return this.cadRow; }
            set { this.cadRow = value; }
        }

        public string Designacao
        {
            get { return this.txtDesignacao.Text.Trim(); }
            set { this.txtDesignacao.Text = value; }
        }

        public string NumeroPolicia
        {
            get { return this.txtNumPolicia.Text.Trim(); }
            set { this.txtNumPolicia.Text = value; }
        }

        private void btnOk_Click(object sender, System.EventArgs e) {
        }

        private void txtDesignacao_TextChanged(object sender, System.EventArgs e) {
            updateButtons();
        }

        private void updateButtons() {
            btnOk.Enabled = txtDesignacao.Text.Length > 0;
        }

        private void button_FormPickControloAut_Click(object sender, System.EventArgs e) {
            FormPickControloAut frmPick = new FormPickControloAut();
            frmPick.Text = "Notícia de autoridade - Pesquisar registo de autoridade geográfico";
            frmPick.caList.AllowedNoticiaAut(TipoNoticiaAut.ToponimicoGeografico);
            frmPick.caList.ReloadList();

            if (frmPick.caList.Items.Count > 0)
                frmPick.caList.SelectItem(frmPick.caList.Items[0]);

            switch (frmPick.ShowDialog()) {
                case DialogResult.OK:
                    if (frmPick.caList.SelectedItems.Count > 0) {
                        var selCadRow = (GISADataset.ControloAutDicionarioRow)frmPick.caList.SelectedItems[0].Tag;
                        if (selCadRow.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada)
                            this.cadRow = selCadRow;
                        else
                        {
                            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                            try
                            {
                                ControloAutRule.Current.LoadDicionarioAndControloAutDicionario(GisaDataSetHelper.GetInstance(), selCadRow.IDControloAut, ho.Connection);                                
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
                            this.cadRow = selCadRow.ControloAutRow.GetControloAutDicionarioRows().Where(cad => cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada).Single();
                        }
                        this.txtDesignacao.Text = this.cadRow.DicionarioRow.Termo;
                        updateButtons();
                    }
                    break;
                default:
                    break;
            }
        }

        private void FormLeituraLocalizacaoNumPolicia_Activated(object sender, System.EventArgs e) {
            if (button_FormPickControloAut.Enabled)
                button_FormPickControloAut.Focus();
            else
                txtDesignacao.Focus();
        }

    }

}
