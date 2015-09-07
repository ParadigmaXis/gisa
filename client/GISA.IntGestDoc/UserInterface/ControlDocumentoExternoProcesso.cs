using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.IntGestDoc.Model;
using GISA.IntGestDoc.Model.EntidadesExternas;

namespace GISA.IntGestDoc.UserInterface
{
    public partial class ControlDocumentoExternoProcesso : UserControl
    {
        private DocumentoComposto documento;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal DocumentoComposto Documento
        {
            get { return this.documento; }
            set
            {
                this.documento = value;
                UpdateView();
            }
        }

        public ControlDocumentoExternoProcesso()
        {
            InitializeComponent();
        }

        private void UpdateView()
        {
            this.txtTipoRegisto.Text = this.documento.Tipologia == null ? this.documento.NUP : this.documento.Tipologia.Titulo;
            this.txtNUP.Text = this.documento.NUP;
            this.txtDataRegisto.Text = new DataIncompleta(this.documento.DataInicio, this.documento.DataFim).ToString();
            this.txtUnidadeOrganica.Text = this.documento.Produtor.Codigo;
            this.txtConfidencialidade.Text = this.documento.Confidencialidade;
            int counter = 1;
            this.lvRequerentes.Items.AddRange(this.documento.RequerentesOuProprietariosIniciais.Select(c => new ListViewItem(new string[] { (counter++).ToString(), c })).ToArray());
            counter = 1;
            this.lstAverbamentos.Items.AddRange(this.documento.AverbamentosDeRequerenteOuProprietario.Select(c => new ListViewItem(new string[] { (counter++).ToString(), c })).ToArray());
            counter = 1;
            this.lstLocais.Items.AddRange(this.documento.LocalizacoesObraDesignacaoActual.Select(c => new ListViewItem(new string[] { (counter++).ToString(), c.LocalizacaoObraDesignacaoActual.Titulo, c.NroPolicia })).ToArray());
            counter = 1;
            this.lstTecnicosObra.Items.AddRange(this.documento.TecnicosDeObra.Select(c => new ListViewItem(new string[] { (counter++).ToString(), c.Titulo })).ToArray());
        }

        internal void Clear()
        {
            this.txtNUP.Clear();
            this.txtDataRegisto.Clear();
            this.txtConfidencialidade.Clear();
            this.txtTipoRegisto.Clear();
            this.txtUnidadeOrganica.Clear();
            this.lstAverbamentos.Items.Clear();
            this.lstLocais.Items.Clear();
            this.lstTecnicosObra.Items.Clear();
            this.lvRequerentes.Items.Clear();
        }
    }
}
