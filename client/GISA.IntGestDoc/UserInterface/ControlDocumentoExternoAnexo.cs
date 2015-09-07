using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.IntGestDoc.Model.EntidadesExternas;

namespace GISA.IntGestDoc.UserInterface
{
    public partial class ControlDocumentoExternoAnexo : UserControl
    {
        public ControlDocumentoExternoAnexo()
        {
            InitializeComponent();
        }

        private DocumentoAnexo documento;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal DocumentoAnexo Documento
        {
            get { return this.documento; }
            set
            {
                this.documento = value;
                UpdateView();
            }
        }
        private void UpdateView()
        {
            this.txtNUD.Text = this.documento.NUD;
            this.txtTipoDesc.Text = this.documento.TipoDescricao;
            this.txtDesc.Text = this.documento.Descricao;
            this.txtNUP.Text = this.documento.Processo.NUP;
            this.lvConteudos.Items.AddRange(this.documento.Conteudos.Select(c => new ListViewItem(new string[] { c.Tipo, c.Titulo, c.Ficheiro })).ToArray());
        }

        internal void Clear()
        {
            this.txtNUD.Clear();
            this.txtTipoDesc.Clear();
            this.txtDesc.Clear();
            this.txtNUP.Clear();
            this.lvConteudos.Items.Clear();
        }
    }
}
