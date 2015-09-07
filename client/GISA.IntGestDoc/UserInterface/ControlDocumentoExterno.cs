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
    public partial class ControlDocumentoExterno : UserControl
    {
        private DocumentoSimples documento;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal DocumentoSimples Documento
        {
            get { return this.documento; }
            set 
            {               
                this.documento = value;
                UpdateView();                                 
            }
        }
        
        public ControlDocumentoExterno()
        {            
            InitializeComponent();
        }
      
        private void UpdateView()
        {
            this.txtTipoRegisto.Text = this.documento.Tipologia.Titulo;
            this.txtNUD.Text = this.documento.NUD;
            this.txtNUP.Text = this.documento.Processo.NUP;
            this.txtNumEspecifico.Text = this.documento.NumeroEspecifico;
            this.txtDataRegisto.Text = DataIncompleta.FormatDateToString(this.documento.DataCriacao);
            this.txtAssunto.Text = this.documento.Assunto;
            this.txtEntidadeNome.Text = this.documento.Onomastico.Titulo;
            this.txtMorada.Text = this.documento.Toponimia != null ? this.documento.Toponimia.Titulo : string.Empty;
            this.lvConteudos.Items.Clear();
            this.lvConteudos.Items.AddRange(this.documento.Conteudos.Select(c => new ListViewItem(new string[] { c.Tipo, c.Titulo, c.Ficheiro })).ToArray());
            this.txtNotas.Text = this.documento.Notas;
            this.txtTecnicoNome.Text = this.documento.TecnicoDeObra != null ? this.documento.TecnicoDeObra.Titulo : string.Empty;
            this.txtEntidadeNIF.Text = this.documento.Onomastico.NIF;
            this.txtTecnicoNIF.Text = this.documento.TecnicoDeObra != null ? this.documento.TecnicoDeObra.NIF : string.Empty;
            this.txtNumPolicia.Text = this.documento.NumPolicia;
            this.txtLocal.Text = this.documento.Local;
            this.txtRefPredial.Text = this.documento.RefPredial;
        }

        internal void Clear()
        {
            this.txtAssunto.Clear();
            this.txtCodigoPostal.Clear();
            this.txtDataRegisto.Clear();
            this.txtEntidadeNome.Clear();
            this.txtLocal.Clear();
            this.txtLocalidade.Clear();
            this.txtMorada.Clear();
            this.txtNotas.Clear();
            this.txtNUD.Clear();
            this.txtNumEspecifico.Clear();
            this.txtNumPolicia.Clear();
            this.txtNUP.Clear();
            this.txtRefPredial.Clear();
            this.txtTipoRegisto.Clear();
            this.lvConteudos.Items.Clear();
            this.txtTecnicoNome.Clear();
            this.txtTecnicoNome.Clear();
            this.txtEntidadeNIF.Clear();
            this.txtTecnicoNIF.Clear();
        }
    }
}
