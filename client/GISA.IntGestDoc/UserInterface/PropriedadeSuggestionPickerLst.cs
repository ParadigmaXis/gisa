using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.IntGestDoc.Model;
using GISA.IntGestDoc.Model.EntidadesInternas;

namespace GISA.IntGestDoc.UserInterface
{
    public partial class PropriedadeSuggestionPickerLst<T> : UserControl
    {
        public delegate void CreateSuggestionEventHandler(object sender, ref PropriedadeDocumentoGisaTemplate<T> prop, out bool cancel, out TipoEstado estado);
        public event CreateSuggestionEventHandler CreateSuggestion;

        public delegate void SuggestionEditedEventHandler(object sender);
        public event SuggestionEditedEventHandler SuggestionEdited;

        public PropriedadeSuggestionPickerLst()
        {
            InitializeComponent();

            this.propriedadeSuggestionPicker1.IgnoreOption = true;
            this.propriedadeSuggestionPicker1.SetPropertyMode();

            this.propriedadeSuggestionPicker1.CreateSuggestion += new PropriedadeSuggestionPickerTemplate<T>.CreateSuggestionEventHandler(propriedadeSuggestionPicker1_CreateSuggestion);
            this.propriedadeSuggestionPicker1.SuggestionEdited += new PropriedadeSuggestionPickerTemplate<T>.SuggestionEditedEventHandler(propriedadeSuggestionPicker1_SuggestionEdited);
            this.propriedadeSuggestionPicker1.SelectedOptionChanged += new PropriedadeSuggestionPickerTemplate<T>.SelectedOptionChangedEventHandler(propriedadeSuggestionPicker1_SelectedOptionChanged);
        }

        void propriedadeSuggestionPicker1_SelectedOptionChanged(object sender)
        {
            if (this.lvCorrepondencias.SelectedItems.Count != 1) return;

            this.lvCorrepondencias.BeginUpdate();
            var item = this.lvCorrepondencias.SelectedItems[0];
            var prop = item.Tag as PropriedadeDocumentoGisaTemplate<T>;
            if (prop.Valor != null)
            {
                var newIcon = IconsHelper.Instance.GetIcon(prop.EstadoRelacaoPorOpcao[prop.TipoOpcao], prop.TipoOpcao);
                item.SubItems[chDesignacao.Index].Text = prop.Valor.ToString();
                this.lvCorrepondencias.SmallImageList.Images[item.ImageIndex] = newIcon;
            }
            else
            {
                item.SubItems[chDesignacao.Index].Text = "<<Ignorar>>";
                this.lvCorrepondencias.SmallImageList.Images[item.ImageIndex] = new Bitmap(16, 16);
            }

            this.lvCorrepondencias.EndUpdate();
        }

        void propriedadeSuggestionPicker1_SuggestionEdited(object sender)
        {
            this.SuggestionEdited(this);
        }

        void propriedadeSuggestionPicker1_CreateSuggestion(object sender, ref PropriedadeDocumentoGisaTemplate<T> prop, out bool cancel, out TipoEstado estado)
        {
            this.CreateSuggestion(this, ref prop, out cancel, out estado);
        }

        private List<PropriedadeDocumentoGisaTemplate<T>> mLstPropriedade = null;
        public List<PropriedadeDocumentoGisaTemplate<T>> LstPropriedade
        {
            get { return mLstPropriedade; }
            set { mLstPropriedade = value; UpdateList(); }
        }

        private void UpdateList()
        {
            Clear();
            this.Enabled = false;

            if (this.mLstPropriedade == null || this.mLstPropriedade.Count == 0) return;

            this.Enabled = true;

            this.lvCorrepondencias.SmallImageList = new ImageList();
            this.lvCorrepondencias.SmallImageList.ImageSize = new Size(IconsHelper.SingleIconWidth, 16);
            this.lvCorrepondencias.SmallImageList.Images.AddRange(this.mLstPropriedade.Select(c => c.TipoOpcao == TipoOpcao.Ignorar ? new Bitmap(16,16) : IconsHelper.Instance.GetIcon(c.EstadoRelacaoPorOpcao[c.TipoOpcao], c.TipoOpcao)).ToArray());

            int counter = 1;
            this.lvCorrepondencias.Items.AddRange(this.mLstPropriedade.Select(p => new ListViewItem(new string[] { (counter++).ToString(), p.TipoOpcao == TipoOpcao.Ignorar ? "<<Ignorar>>" : p.Valor.ToString()}, counter - 2) { Tag = p }).ToArray());
        }

        internal void Clear()
        {
            this.lvCorrepondencias.Items.Clear();
            this.propriedadeSuggestionPicker1.Clear();
        }

        private void lvCorrepondencias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvCorrepondencias.SelectedItems.Count == 1)
                this.propriedadeSuggestionPicker1.Propriedade = lvCorrepondencias.SelectedItems[0].Tag as PropriedadeDocumentoGisaTemplate<T>;
            else
                this.propriedadeSuggestionPicker1.Propriedade = null;
        }

        internal void AddIgnoreOption()
        {
            this.propriedadeSuggestionPicker1.IgnoreOption = true;
        }

        internal void RemoveIgnoreOption()
        {
            this.propriedadeSuggestionPicker1.IgnoreOption = false;
        }
    }
}
