using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.IntGestDoc.Model;

namespace GISA.IntGestDoc.UserInterface
{
    public partial class CorrespondenciaSuggestionPickerLstGeog : CorrespondenciaSuggestionPickerList
    {
        public CorrespondenciaSuggestionPickerLstGeog()
        {
            InitializeComponent();
        }

        protected internal override void UpdateList()
        {
            Clear();
            this.Enabled = false;

            if (this.mLstCorrespondencia == null || this.mLstCorrespondencia.Count == 0) return;

            this.Enabled = true;
            this.lvCorrepondencias.SmallImageList = new ImageList();
            this.lvCorrepondencias.SmallImageList.ImageSize = new Size(IconsHelper.ComposedIconWidth, 16);
            this.lvCorrepondencias.SmallImageList.Images.AddRange(this.mLstCorrespondencia.Select(c => c.TipoOpcao == TipoOpcao.Ignorar ? new Bitmap(16, 16) : IconsHelper.Instance.GetIcon(TipoEntidade.GetTipoEntidadeInterna(c.EntidadeExterna.Tipo), c.EntidadeInterna.Estado, c.TipoOpcao, c.EstadoRelacaoPorOpcao[c.TipoOpcao])).ToArray());

            int counter = 1;
            var docExt = this.CorrespondenciaDoc.EntidadeExterna as Model.EntidadesExternas.DocumentoComposto;
            this.lvCorrepondencias.Items.AddRange(
                this.mLstCorrespondencia.Select(c => new ListViewItem(new string[] { 
                    (counter++).ToString(), 
                    c.TipoOpcao == TipoOpcao.Ignorar ? "<<Ignorar>>" : c.EntidadeInterna.Titulo,
                    docExt.LocalizacoesObraDesignacaoActual.SingleOrDefault(l => l.LocalizacaoObraDesignacaoActual.Equals((Model.EntidadesExternas.Geografico)c.EntidadeExterna)).NroPolicia
                }, counter - 2) { Tag = c }).ToArray());
        }
    }
}
