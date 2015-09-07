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
using GISA.Model;

namespace GISA.IntGestDoc.UserInterface
{
    public partial class DocumentParentSuggestionPicker : UserControl
    {
        public delegate void CreateEntidadeInternaEventHandler(object sender, ref PropriedadeDocumentoGisaTemplate<DocumentoGisa> prop, out bool cancel, out TipoEstado estado);
        public event CreateEntidadeInternaEventHandler CreateEntidadeInterna;
        public delegate void BrowseEntidadeInternaProcessoEventHandler(object sender, ref PropriedadeDocumentoGisaTemplate<DocumentoGisa> prop, out bool cancel, out TipoEstado estado);
        public event BrowseEntidadeInternaProcessoEventHandler BrowseEntidadeInternaProcesso;
        public delegate void BrowseEntidadeInternaSerieEventHandler(object sender, ref PropriedadeDocumentoGisaTemplate<DocumentoGisa> prop, out bool cancel, out TipoEstado estado);
        public event BrowseEntidadeInternaSerieEventHandler BrowseEntidadeInternaSerie;
        public delegate void SuggestionEditedEventHandler(object sender);
        public event SuggestionEditedEventHandler SuggestionEdited;
        public delegate void SelectedOptionChangedEventHandler(object sender);
        public event SelectedOptionChangedEventHandler SelectedOptionChanged;

        public DocumentParentSuggestionPicker()
        {
            InitializeComponent();

            this.propriedadeSuggestionPickerProcesso.DisplayMember = "CodigoComTitulo";
            this.propriedadeSuggestionPickerProcesso.IsIconComposed = true;
            this.propriedadeSuggestionPickerProcesso.IgnoreOption = false;
            this.propriedadeSuggestionPickerProcesso.SetEntityMode();

            this.propriedadeSuggestionPickerProcesso.CreateEntidadeInterna += new PropriedadeSuggestionPickerTemplate<DocumentoGisa>.CreateEntidadeInternaEventHandler(propriedadeSugestionPickerProcesso_CreateEntidadeInterna);
            this.propriedadeSuggestionPickerProcesso.BrowseEntidadeInterna += new PropriedadeSuggestionPickerTemplate<DocumentoGisa>.BrowseEntidadeInternaEventHandler(propriedadeSugestionPickerProcesso_BrowseEntidadeInterna);
            this.propriedadeSuggestionPickerProcesso.SuggestionEdited += new PropriedadeSuggestionPickerTemplate<DocumentoGisa>.SuggestionEditedEventHandler(DocumentSuggestionEdited);
            this.propriedadeSuggestionPickerProcesso.SelectedOptionChanged += new PropriedadeSuggestionPickerTemplate<DocumentoGisa>.SelectedOptionChangedEventHandler(propriedadeSuggestionPicker1_SelectedOptionChanged);

            this.propriedadeSuggestionPickerSerie.AllowChangeEnabledState = true;
            this.propriedadeSuggestionPickerSerie.AllowEmptyOptions = true;
            this.propriedadeSuggestionPickerSerie.IsIconComposed = true;
            this.propriedadeSuggestionPickerSerie.IgnoreOption = true;
            this.propriedadeSuggestionPickerSerie.SetSearchEntityMode();

            this.propriedadeSuggestionPickerSerie.BrowseEntidadeInterna += new PropriedadeSuggestionPickerTemplate<DocumentoGisa>.BrowseEntidadeInternaEventHandler(propriedadeSuggestionPickerSerie_BrowseEntidadeInterna);
            this.propriedadeSuggestionPickerSerie.SuggestionEdited += new PropriedadeSuggestionPickerTemplate<DocumentoGisa>.SuggestionEditedEventHandler(DocumentSuggestionEdited);
            this.propriedadeSuggestionPickerSerie.SelectedOptionChanged += new PropriedadeSuggestionPickerTemplate<DocumentoGisa>.SelectedOptionChangedEventHandler(propriedadeSuggestionPicker1_SelectedOptionChanged);
        }
        

        void propriedadeSugestionPickerProcesso_CreateEntidadeInterna(object sender, ref PropriedadeDocumentoGisaTemplate<DocumentoGisa> prop, out bool cancel, out TipoEstado estado)
        {
            // mandar o evento para o parent control
            this.CreateEntidadeInterna(this, ref prop, out cancel, out estado);
        }

        void propriedadeSuggestionPickerSerie_BrowseEntidadeInterna(object sender, ref PropriedadeDocumentoGisaTemplate<DocumentoGisa> prop, out bool cancel, out TipoEstado estado)
        {
            // mandar o evento para o parent control
            this.BrowseEntidadeInternaSerie(this, ref prop, out cancel, out estado);
        }

        void propriedadeSugestionPickerProcesso_BrowseEntidadeInterna(object sender, ref PropriedadeDocumentoGisaTemplate<DocumentoGisa> prop, out bool cancel, out TipoEstado estado)
        {
            // mandar o evento para o parent control
            this.BrowseEntidadeInternaProcesso(this, ref prop, out cancel, out estado);
        }

        void DocumentSuggestionEdited(object sender)
        {
            // mandar o evento para o parent control
            this.SuggestionEdited(this);
        }

        void propriedadeSuggestionPicker1_SelectedOptionChanged(object sender)
        {
            if (this.SelectedOptionChanged != null)
                this.SelectedOptionChanged(this);
        }

        private PropriedadeDocumentoGisaTemplate<DocumentoGisa> mPropriedadeSerie = null;
        public PropriedadeDocumentoGisaTemplate<DocumentoGisa> PropriedadeSerie
        {
            get { return mPropriedadeSerie; }
            set { mPropriedadeSerie = value; UpdateListSerie(); }
        }

        private void UpdateListSerie()
        {
            this.propriedadeSuggestionPickerSerie.Clear();

            if (this.mPropriedadeSerie == null) return;

            this.propriedadeSuggestionPickerSerie.Propriedade = this.mPropriedadeSerie;
        }

        private PropriedadeDocumentoGisaTemplate<DocumentoGisa> mPropriedadeProcesso = null;
        public PropriedadeDocumentoGisaTemplate<DocumentoGisa> PropriedadeProcesso
        {
            get { return mPropriedadeProcesso; }
            set { mPropriedadeProcesso = value; UpdateListProcesso(); }
        }

        private void UpdateListProcesso()
        {
            this.propriedadeSuggestionPickerProcesso.Clear();

            if (this.mPropriedadeProcesso == null) return;

            this.propriedadeSuggestionPickerProcesso.Propriedade = this.mPropriedadeProcesso;
        }

        internal void Clear()
        {
            this.propriedadeSuggestionPickerProcesso.Clear();
            this.propriedadeSuggestionPickerSerie.Clear();
        }

        internal void RefreshProcesso()
        {
            this.propriedadeSuggestionPickerProcesso.SelectedOptionChanged -= new PropriedadeSuggestionPickerTemplate<DocumentoGisa>.SelectedOptionChangedEventHandler(propriedadeSuggestionPicker1_SelectedOptionChanged);
            this.propriedadeSuggestionPickerProcesso.RefreshControl();
            this.propriedadeSuggestionPickerProcesso.SelectedOptionChanged += new PropriedadeSuggestionPickerTemplate<DocumentoGisa>.SelectedOptionChangedEventHandler(propriedadeSuggestionPicker1_SelectedOptionChanged);
        }

        internal void RefreshSerie()
        {
            this.propriedadeSuggestionPickerSerie.SelectedOptionChanged -= new PropriedadeSuggestionPickerTemplate<DocumentoGisa>.SelectedOptionChangedEventHandler(propriedadeSuggestionPicker1_SelectedOptionChanged);
            this.propriedadeSuggestionPickerSerie.RefreshControl();
            this.propriedadeSuggestionPickerSerie.SelectedOptionChanged += new PropriedadeSuggestionPickerTemplate<DocumentoGisa>.SelectedOptionChangedEventHandler(propriedadeSuggestionPicker1_SelectedOptionChanged);
        }

        internal void RefreshControl()
        {
            RefreshProcesso();
            RefreshSerie();
        }
    }
}
