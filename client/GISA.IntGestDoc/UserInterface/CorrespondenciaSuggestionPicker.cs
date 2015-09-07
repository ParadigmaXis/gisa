using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Controls.ControloAut;
using GISA.IntGestDoc.Model;
using GISA.IntGestDoc.Model.EntidadesInternas;
using GISA.Model;

namespace GISA.IntGestDoc.UserInterface
{
    public partial class CorrespondenciaSuggestionPicker : UserControl
    {
        private const string cbItemNovo = "<<Novo>>";
        private const string cbItemProcurar = "<<Procurar existente>>";
        private const string cbItemIgnorar = "<<Ignorar>>";

        private Correspondencia mCorrespondencia = null;
        public Correspondencia Correspondencia
        {
            get { return mCorrespondencia; }
            set { 
                mCorrespondencia = value; 
                UpdateList(); 
                SelectOption();
            }
        }

        void mCorrespondencia_CorrespondenciaChanged(object sender)
        {
            this.SuggestionEdited(this);
        }

        public string DisplayMember
        {
            set { cbOptions.DisplayMember = value; }
        }

        private bool mIsIconComposed = true;
        public bool IsIconComposed
        {
            get { return mIsIconComposed; }
            set
            {
                mIsIconComposed = value;
                if (value)
                    this.imageList1.ImageSize = new Size(IconsHelper.ComposedIconWidth, this.imageList1.ImageSize.Height);
                else
                    this.imageList1.ImageSize = new Size(IconsHelper.SingleIconWidth, this.imageList1.ImageSize.Height);
            }
        }

        private void SelectOption()
        {
            if (this.mCorrespondencia != null)
                SelectOption(this.mCorrespondencia.TipoOpcao);
        }

        private void SelectOption(TipoOpcao tipoCorrespondencia)
        {
            EntidadeInterna ei = null;
            if (this.mCorrespondencia != null)
            {
                this.mCorrespondencia.TipoOpcao = tipoCorrespondencia;
                ei = this.mCorrespondencia.EntidadeInterna;
            }
            if (this.cbOptions.SelectedItem != ei)
                this.cbOptions.SelectedItem = ei;
        }

        public CorrespondenciaSuggestionPicker()
        {
            InitializeComponent();
            
            this.cbOptions.DisplayMember = "Titulo";
            AddIgnoreOption();
        }

        internal void UpdateList()
        {
            Clear();
            imageList1.Images.Clear();
            this.cbOptions.ImageIndexes.Clear();
            this.Enabled = false;

            if (this.mCorrespondencia == null) return;

            foreach (TipoOpcao op in Enum.GetValues(typeof(TipoOpcao)))
            {
                var ei = this.Correspondencia.GetEntidadeInterna(op);
                if (ei == null || this.cbOptions.Items.Contains(ei)) continue;

                this.cbOptions.Items.Add(ei);

                var tipoEntidade = TipoEntidade.GetTipoEntidadeInterna(this.Correspondencia.EntidadeExterna.Tipo);
                if (IsIconComposed)
                    imageList1.Images.Add(IconsHelper.Instance.GetIcon(tipoEntidade, ei.Estado, op, this.Correspondencia.EstadoRelacaoPorOpcao[op]));
                else
                    imageList1.Images.Add(IconsHelper.Instance.GetIcon(tipoEntidade, ei.Estado, op));

                this.cbOptions.ImageIndexes.Add(imageList1.Images.Count - 1);
            }

            if (this.cbOptions.Items.Count == 0) return;
                
            this.Enabled = true;
            this.cbOptions.Items.AddRange(Options.ToArray());
        }

        internal void Clear()
        {
            cbOptions.Items.Clear();
        }

        internal void RefreshOptions()
        {
            this.cbOptions.SelectedIndexChanged -= new System.EventHandler(this.cbOptions_SelectedIndexChanged);
            this.Correspondencia = this.Correspondencia;
            this.cbOptions.SelectedIndexChanged += new System.EventHandler(this.cbOptions_SelectedIndexChanged);
        }

        private List<string> Options = new List<string>() { cbItemNovo, cbItemProcurar };
        public void AddIgnoreOption()
        {
            if (Options.Contains(cbItemIgnorar)) return;
            Options.Add(cbItemIgnorar);
            UpdateList();
        }

        public void RemoveIgnoreOption()
        {
            if (!Options.Contains(cbItemIgnorar)) return;
            Options.Remove(cbItemIgnorar);
            UpdateList();
        }

        public delegate void CreateEntidadeInternaEventHandler(object sender, ref EntidadeInterna ei, TipoEntidadeExterna tee, out bool cancel, out TipoEstado estado);
        public event CreateEntidadeInternaEventHandler CreateEntidadeInterna;

        public delegate void BrowseEntidadeInternaEventHandler(object sender, ref EntidadeInterna ei, TipoEntidadeExterna tee, out bool cancel, out TipoEstado estado);
        public event BrowseEntidadeInternaEventHandler BrowseEntidadeInterna;

        public delegate void SuggestionEditedEventHandler(object sender);
        public event SuggestionEditedEventHandler SuggestionEdited;

        public delegate void SelectedOptionChangedEventHandler();
        public event SelectedOptionChangedEventHandler SelectedOptionChanged;

        private void cbOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool cancel = false;
            TipoEstado estado = TipoEstado.Novo;
            EntidadeInterna ei = this.Correspondencia.EntidadeInterna;
            if (object.ReferenceEquals(this.cbOptions.SelectedItem, cbItemNovo))
            {
                this.CreateEntidadeInterna(this, ref ei, this.Correspondencia.EntidadeExterna.Tipo, out cancel, out estado);
                if (!cancel)
                {
                    this.Correspondencia.TipoOpcao = TipoOpcao.Trocada;
                    this.Correspondencia.EntidadeInterna = ei;
                    this.Correspondencia.EstadoRelacaoPorOpcao[this.Correspondencia.TipoOpcao] = 
                        this.Correspondencia.EstadoRelacaoPorOpcao.ContainsKey(TipoOpcao.Original) ? TipoEstado.Editar: estado;
                    this.SuggestionEdited(this);
                }
                UpdateList();
                SelectOption(this.Correspondencia.TipoOpcao);
                return;
            }
            else if (object.ReferenceEquals(this.cbOptions.SelectedItem, cbItemProcurar))
            {
                this.BrowseEntidadeInterna(this, ref ei, this.Correspondencia.EntidadeExterna.Tipo, out cancel, out estado);
                if (!cancel)
                {
                    this.Correspondencia.TipoOpcao = TipoOpcao.Trocada;
                    this.Correspondencia.EntidadeInterna = ei;
                    this.Correspondencia.EstadoRelacaoPorOpcao[this.Correspondencia.TipoOpcao] = estado;
                    this.SuggestionEdited(this);
                }
                UpdateList();
                SelectOption(this.Correspondencia.TipoOpcao);
                return;
            }
            else if (object.ReferenceEquals(this.cbOptions.SelectedItem, cbItemIgnorar))
            {
                this.mCorrespondencia.TipoOpcao = TipoOpcao.Ignorar;
                this.SuggestionEdited(this);
            }
            else
                foreach (TipoOpcao op in Enum.GetValues(typeof(TipoOpcao)))
                    if (object.ReferenceEquals(this.cbOptions.SelectedItem, this.Correspondencia.GetEntidadeInterna(op)))
                    {
                        this.mCorrespondencia.TipoOpcao = op;
                        break;
                    }

            if (this.SelectedOptionChanged != null)
                this.SelectedOptionChanged();

            this.toolTip1.SetToolTip(this.cbOptions, this.cbOptions.Text);
        }
    }
}