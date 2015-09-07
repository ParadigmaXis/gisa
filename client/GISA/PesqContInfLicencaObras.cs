using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Controls.ControloAut;
using GISA.SharedResources;
using GISA.Model;

namespace GISA {
    public partial class PesqContInfLicencaObras : UserControl {
        public PesqContInfLicencaObras() {
            InitializeComponent();
            GetExtraResources();
        }

        private void GetExtraResources() {
            btnLocalizacaoDeObra.Image = SharedResourcesOld.CurrentSharedResources.ChamarPicker;
            btnTecnicoDeObra.Image = SharedResourcesOld.CurrentSharedResources.ChamarPicker;
        }


        private MasterPanelPesquisa owner;
        public MasterPanelPesquisa Owner {
            get { return this.owner; }
            set { this.owner = value; }
        }

        public delegate void ControloAutSelectionRetriever(FormPickControloAut frmPick, TextBox txtBox);
        private ControloAutSelectionRetriever theControloAutSelectionRetriever;
        public ControloAutSelectionRetriever TheControloAutSelectionRetriever {
            get { return theControloAutSelectionRetriever; }
            set { theControloAutSelectionRetriever = value;  }
        }

        public void clearFields() {
            txtAtestadoHab.Clear();
            txtLocalizacao.Clear();
            txtNumPolicia.Clear();
            txtRequerente.Clear();
            txtTecnicoDeObra.Clear();
            txtTipoDeObra.Clear();
            txtBoxLocalizacaoAntiga.Clear();
            txtBoxNumPoliciaAntigo.Clear();

            chkPH.Checked = false;
            cdbDataInicio.Checked = false;
            cdbDataInicio.UpdateDate();
            cdbDataFim.Checked = false;
            cdbDataFim.UpdateDate();
        }

        public string get_Date_Inicio() {
            if (cdbDataInicio.Checked)
                return cdbDataInicio.GetStandardMaskDate.ToString("yyyyMMdd");
            else return string.Empty;
        }

        public string get_Date_Fim() {
            if (cdbDataFim.Checked)
                return cdbDataFim.GetStandardMaskDate.ToString("yyyyMMdd");
            else return string.Empty;
        }

        private void btnRequerentes_Click(object sender, EventArgs e) {

        }

        private void btnLocalizacaoDeObra_Click(object sender, EventArgs e) {
            FormPickControloAut frmPick = new FormPickControloAut();
            frmPick.Text = "Notícia de autoridade - Pesquisar registo de autoridade geográfico";
            frmPick.caList.AllowedNoticiaAut(TipoNoticiaAut.ToponimicoGeografico);
            frmPick.caList.ReloadList();
            TheControloAutSelectionRetriever(frmPick, txtLocalizacao);
        }

        private void btnTecnicoDeObra_Click(object sender, EventArgs e) {
            FormPickControloAut frmPick = new FormPickControloAut();
            frmPick.Text = "Notícia de autoridade - Pesquisar registo de autoridade onomástico";
            frmPick.caList.AllowedNoticiaAut(TipoNoticiaAut.Onomastico);
            frmPick.caList.ReloadList();
            TheControloAutSelectionRetriever(frmPick, txtTecnicoDeObra);
        }

        public delegate void ExecuteQueryEventHandler();
        public event ExecuteQueryEventHandler ExecuteQuery;

        private void PesqContInfLicencaObras_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyValue == Convert.ToInt32(Keys.Enter))
                if (this.ExecuteQuery != null)
                    ExecuteQuery();
        }
    }
}
