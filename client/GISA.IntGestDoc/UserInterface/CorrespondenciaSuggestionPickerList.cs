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
    public partial class CorrespondenciaSuggestionPickerList : UserControl
    {
        public delegate void CreateEntidadeInternaEventHandler(object sender, ref EntidadeInterna ei, TipoEntidadeExterna tee, out bool cancel, out TipoEstado estado);
        public event CreateEntidadeInternaEventHandler CreateEntidadeInterna;

        public delegate void BrowseEntidadeInternaEventHandler(object sender, ref EntidadeInterna ei, TipoEntidadeExterna tee, out bool cancel, out TipoEstado estado);
        public event BrowseEntidadeInternaEventHandler BrowseEntidadeInterna;

        public delegate void SuggestionEditedEventHandler(object sender);
        public event SuggestionEditedEventHandler SuggestionEdited;

        public CorrespondenciaSuggestionPickerList()
        {
            InitializeComponent();

            this.correspondenciaSuggestionPicker1.AddIgnoreOption();

            this.correspondenciaSuggestionPicker1.CreateEntidadeInterna += new CorrespondenciaSuggestionPicker.CreateEntidadeInternaEventHandler(correspondenciaSuggestionPicker1_CreateEntidadeInterna);
            this.correspondenciaSuggestionPicker1.BrowseEntidadeInterna += new CorrespondenciaSuggestionPicker.BrowseEntidadeInternaEventHandler(correspondenciaSuggestionPicker1_BrowseEntidadeInterna);
            this.correspondenciaSuggestionPicker1.SuggestionEdited += new CorrespondenciaSuggestionPicker.SuggestionEditedEventHandler(correspondenciaSuggestionPicker1_SuggestionEdited);
            this.correspondenciaSuggestionPicker1.SelectedOptionChanged += new CorrespondenciaSuggestionPicker.SelectedOptionChangedEventHandler(correspondenciaSuggestionPicker1_SelectedOptionChanged);
        }

        void correspondenciaSuggestionPicker1_SelectedOptionChanged()
        {
            if (this.lvCorrepondencias.SelectedItems.Count != 1) return;

            this.lvCorrepondencias.BeginUpdate();
            var item = this.lvCorrepondencias.SelectedItems[0];
            var corresp = item.Tag as Correspondencia;
            if (corresp.EntidadeInterna != null)
            {
                var newIcon = IconsHelper.Instance.GetIcon(TipoEntidade.GetTipoEntidadeInterna(corresp.EntidadeExterna.Tipo), corresp.EntidadeInterna.Estado, corresp.TipoOpcao, corresp.EstadoRelacaoPorOpcao[corresp.TipoOpcao]);
                item.SubItems[chDesignacao.Index].Text = corresp.EntidadeInterna.Titulo;
                this.lvCorrepondencias.SmallImageList.Images[item.ImageIndex] = newIcon;
            }
            else
            {
                item.SubItems[chDesignacao.Index].Text = "<<Ignorar>>";
                this.lvCorrepondencias.SmallImageList.Images[item.ImageIndex] = new Bitmap(16, 16);
            }

            this.lvCorrepondencias.EndUpdate();
        }

        void correspondenciaSuggestionPicker1_SuggestionEdited(object sender)
        {
            this.SuggestionEdited(this);
        }

        void correspondenciaSuggestionPicker1_BrowseEntidadeInterna(object sender, ref EntidadeInterna ei, TipoEntidadeExterna tee, out bool cancel, out TipoEstado estado)
        {
            this.BrowseEntidadeInterna(this, ref ei, tee, out cancel, out estado);
        }

        void correspondenciaSuggestionPicker1_CreateEntidadeInterna(object sender, ref EntidadeInterna ei, TipoEntidadeExterna tee, out bool cancel, out TipoEstado estado)
        {
            this.CreateEntidadeInterna(this, ref ei, tee, out cancel, out estado);
        }

        public CorrespondenciaDocs CorrespondenciaDoc { get; set; }

        internal protected List<Correspondencia> mLstCorrespondencia = null;
        public List<Correspondencia> LstCorrespondencia
        {
            get { return mLstCorrespondencia; }
            set { mLstCorrespondencia = value; UpdateList(); }
        }

        internal protected virtual void UpdateList()
        {
            Clear();
            this.Enabled = false;

            if (this.mLstCorrespondencia == null || this.mLstCorrespondencia.Count == 0) return;

            this.Enabled = true;
            this.lvCorrepondencias.SmallImageList = new ImageList();
            this.lvCorrepondencias.SmallImageList.ImageSize = new Size(IconsHelper.ComposedIconWidth, 16);
            this.lvCorrepondencias.SmallImageList.Images.AddRange(this.mLstCorrespondencia.Select(c => c.TipoOpcao == TipoOpcao.Ignorar ? new Bitmap(16,16) : IconsHelper.Instance.GetIcon(TipoEntidade.GetTipoEntidadeInterna(c.EntidadeExterna.Tipo), c.EntidadeInterna.Estado, c.TipoOpcao, c.EstadoRelacaoPorOpcao[c.TipoOpcao])).ToArray());

            int counter = 1;
            this.lvCorrepondencias.Items.AddRange(this.mLstCorrespondencia.Select(c => new ListViewItem(new string[] { (counter++).ToString(), c.TipoOpcao == TipoOpcao.Ignorar ? "<<Ignorar>>" : c.EntidadeInterna.Titulo }, counter - 2) { Tag = c }).ToArray());
        }

        internal void Clear()
        {
            this.lvCorrepondencias.Items.Clear();
            this.correspondenciaSuggestionPicker1.Correspondencia = null;
        }

        private void lvCorrepondencias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvCorrepondencias.SelectedItems.Count == 1)
                this.correspondenciaSuggestionPicker1.Correspondencia = lvCorrepondencias.SelectedItems[0].Tag as Correspondencia;
            else
                this.correspondenciaSuggestionPicker1.Correspondencia = null;
        }

        private void lvCorrepondencias_MouseMove(object sender, MouseEventArgs e)
        {
            var item = lvCorrepondencias.GetItemAt(e.X, e.Y);
            if (item == null) return;
            var c = item.Tag as Correspondencia;
            this.toolTip1.SetToolTip(this.lvCorrepondencias, item.SubItems[chDesignacao.Index].Text);
        }

        internal void RefreshOptions()
        {
            this.correspondenciaSuggestionPicker1.RefreshOptions();
        }

        internal void AddIgnoreOption()
        {
            this.correspondenciaSuggestionPicker1.AddIgnoreOption();
        }

        internal void RemoveIgnoreOption()
        {
            this.correspondenciaSuggestionPicker1.RemoveIgnoreOption();
        }
    }
}
