using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.IntGestDoc.Model;
using GISA.IntGestDoc.Model.EntidadesInternas;

namespace GISA.IntGestDoc.UserInterface
{
    class PropriedadeSuggestionPickerTemplate<T> : PropriedadeSuggestionPicker
    {
        private const string cbItemEditar = "<<Editar>>";
        private const string cbItemNovo = "<<Novo>>";
        private const string cbItemProcurar = "<<Procurar existente>>";
        private const string cbItemIgnorar = "<<Ignorar>>";

        private ToolTip toolTip1;
        private System.ComponentModel.IContainer components;
        private GISA.Controls.PxComboBox cbOptions;
        private System.Windows.Forms.ImageList imageList1;
        private ControlMode mActiveMode;
        private List<string> Options = new List<string>();

        private enum ControlMode
        {
            PropertyMode = 1,
            EntityMode = 2
        }

        public PropriedadeSuggestionPickerTemplate()
        {
            InitializeComponent();
            cbOptions.DisplayMember = "Titulo";

            // só para garantir que existe um modo definido (no entanto é configurável fora da classe)
            SetPropertyMode();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cbOptions = new GISA.Controls.PxComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // cbOptions
            // 
            this.cbOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOptions.BackColor = System.Drawing.SystemColors.Window;
            this.cbOptions.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOptions.FormattingEnabled = true;
            this.cbOptions.ImageList = this.imageList1;
            this.cbOptions.ItemHeight = 16;
            this.cbOptions.Location = new System.Drawing.Point(0, 0);
            this.cbOptions.Name = "cbOptions";
            this.cbOptions.Size = new System.Drawing.Size(150, 21);
            this.cbOptions.TabIndex = 0;
            this.cbOptions.SelectedIndexChanged += new System.EventHandler(this.cbOptions_SelectedIndexChanged);
            // 
            // PropriedadeSuggestionPicker
            // 
            this.Controls.Add(this.cbOptions);
            this.Name = "PropriedadeSuggestionPicker";
            this.Size = new System.Drawing.Size(150, 21);
            this.ResumeLayout(false);

        }

        public bool AllowEmptyOptions = false;

        private bool mAllowChangeEnabledState = false;
        public bool AllowChangeEnabledState { get { return mAllowChangeEnabledState; } set { mAllowChangeEnabledState = value; } }

        private bool mIgnoreOption = true;
        public override bool IgnoreOption
        {
            get { return mIgnoreOption; }
            set
            {
                mIgnoreOption = value;
                if (mIgnoreOption)
                    AddIgnoreOption();
                else
                    RemoveIgnoreOption();
            }
        }

        public void SetEnabledState()
        {
            var val = this.Propriedade.Valor;
            this.Enabled = val != null || this.Propriedade.TipoOpcao == TipoOpcao.Ignorar;
        }

        public void SetEnabled(bool state)
        {
            if (!AllowChangeEnabledState) return;

            this.Enabled = state;
        }

        public void SetPropertyMode()
        {
            if (mActiveMode == PropriedadeSuggestionPickerTemplate<T>.ControlMode.PropertyMode) return;
            Options.Clear();
            Options.Add(cbItemEditar);
            if (IgnoreOption) Options.Add(cbItemIgnorar);
            mActiveMode = PropriedadeSuggestionPickerTemplate<T>.ControlMode.PropertyMode;
        }

        public void SetEntityMode()
        {
            if (mActiveMode == PropriedadeSuggestionPickerTemplate<T>.ControlMode.EntityMode) return;
            Options.Clear();
            Options.Add(cbItemNovo);
            Options.Add(cbItemProcurar);
            if (IgnoreOption) Options.Add(cbItemIgnorar);
            mActiveMode = PropriedadeSuggestionPickerTemplate<T>.ControlMode.EntityMode;
        }

        public void SetSearchEntityMode()
        {
            if (mActiveMode == PropriedadeSuggestionPickerTemplate<T>.ControlMode.EntityMode) return;
            Options.Clear();
            Options.Add(cbItemProcurar);
            if (IgnoreOption) Options.Add(cbItemIgnorar);
            mActiveMode = PropriedadeSuggestionPickerTemplate<T>.ControlMode.EntityMode;
        }

        private void AddIgnoreOption()
        {
            if (Options.Contains(cbItemIgnorar)) return;
            Options.Add(cbItemIgnorar);
            UpdateList();
        }

        private void RemoveIgnoreOption()
        {
            if (!Options.Contains(cbItemIgnorar)) return;
            Options.Remove(cbItemIgnorar);
            UpdateList();
        }

        public delegate void CreateEntidadeInternaEventHandler(object sender, ref PropriedadeDocumentoGisaTemplate<T> prop, out bool cancel, out TipoEstado estado);
        public event CreateEntidadeInternaEventHandler CreateEntidadeInterna;
        public delegate void BrowseEntidadeInternaEventHandler(object sender, ref PropriedadeDocumentoGisaTemplate<T> prop, out bool cancel, out TipoEstado estado);
        public event BrowseEntidadeInternaEventHandler BrowseEntidadeInterna;
        public delegate void CreateSuggestionEventHandler(object sender, ref PropriedadeDocumentoGisaTemplate<T> prop, out bool cancel, out TipoEstado estado);
        public event CreateSuggestionEventHandler CreateSuggestion;
        public delegate void SuggestionEditedEventHandler(object sender);
        public event SuggestionEditedEventHandler SuggestionEdited;
        public delegate void SelectedOptionChangedEventHandler(object sender);
        public event SelectedOptionChangedEventHandler SelectedOptionChanged;

        private PropriedadeDocumentoGisaTemplate<T> mPropriedade = null;
        public PropriedadeDocumentoGisaTemplate<T> Propriedade
        {
            get { return mPropriedade; }
            set { mPropriedade = value; RefreshControl(); }
        }

        public override void RefreshControl()
        {
            UpdateList(); SelectOption();
        }

        public string DisplayMember
        {
            set { cbOptions.DisplayMember = value; }
        }

        private bool mIsIconComposed = false;
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
            if (this.mPropriedade != null)
                SelectOption(this.mPropriedade.TipoOpcao);
        }

        private void SelectOption(TipoOpcao tipoOpcao)
        {
            T propriedade = default(T);
            if (this.mPropriedade != null)
            {
                this.mPropriedade.TipoOpcao = tipoOpcao;
                propriedade = this.mPropriedade.Valor;
            }
            this.cbOptions.SelectedItem = propriedade;
        }

        internal void UpdateList()
        {
            Clear();
            imageList1.Images.Clear();
            this.cbOptions.ImageIndexes.Clear();
            this.Enabled = false;

            if (this.mPropriedade == null) { return; }

            var items = new List<object>();
            foreach (TipoOpcao op in Enum.GetValues(typeof(TipoOpcao)))
            {
                var prop = this.Propriedade.GetValor(op);
                if (prop == null || this.cbOptions.Items.Contains(prop)) continue;

                this.cbOptions.Items.Add(prop);

                if (IsIconComposed)
                {
                    var proc = prop as DocumentoGisa;
                    imageList1.Images.Add(IconsHelper.Instance.GetIcon(proc.Tipo, proc.Estado, op, this.Propriedade.EstadoRelacaoPorOpcao[op]));
                }
                else
                    imageList1.Images.Add(IconsHelper.Instance.GetIcon(this.Propriedade.EstadoRelacaoPorOpcao[op], op));

                cbOptions.ImageIndexes.Add(imageList1.Images.Count - 1);
            }

            if (this.cbOptions.Items.Count == 0 && !AllowEmptyOptions) return;

            this.Enabled = true;
            this.cbOptions.Items.AddRange(Options.ToArray());
        }

        internal void Clear()
        {
            cbOptions.Items.Clear();
        }

        private void cbOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool cancel = false;
            TipoEstado estado = TipoEstado.Novo;
            var prop = this.Propriedade;
            if (object.ReferenceEquals(this.cbOptions.SelectedItem, cbItemEditar))
            {
                this.CreateSuggestion(this, ref prop, out cancel, out estado);
                if (!cancel)
                    this.SuggestionEdited(this);

                UpdateList();
                SelectOption(this.Propriedade.TipoOpcao);
                return;
            }
            else if (object.ReferenceEquals(this.cbOptions.SelectedItem, cbItemNovo))
            {
                EntidadeInterna ei = this.Propriedade.Valor as EntidadeInterna;
                this.CreateEntidadeInterna(this, ref prop, out cancel, out estado);
                if (!cancel)
                   this.SuggestionEdited(this);
                UpdateList();
                SelectOption(this.Propriedade.TipoOpcao);
                return;
            }
            else if (object.ReferenceEquals(this.cbOptions.SelectedItem, cbItemProcurar))
            {
                EntidadeInterna ei = this.Propriedade.Valor as EntidadeInterna;
                this.BrowseEntidadeInterna(this, ref prop, out cancel, out estado);
                if (!cancel)
                    this.SuggestionEdited(this);
                UpdateList();
                SelectOption(this.Propriedade.TipoOpcao);
                return;
            }
            else if (object.ReferenceEquals(this.cbOptions.SelectedItem, cbItemIgnorar))
            {
                this.mPropriedade.TipoOpcao = TipoOpcao.Ignorar;
                this.SuggestionEdited(this);
            }
            else
                foreach (TipoOpcao op in Enum.GetValues(typeof(TipoOpcao)))
                    if (this.Propriedade.Escolhas.ContainsKey(op) && this.Propriedade.Escolhas[op].Equals(this.cbOptions.SelectedItem))
                    {
                        this.mPropriedade.TipoOpcao = op;
                        break;
                    }

            if (this.SelectedOptionChanged != null)
                this.SelectedOptionChanged(this);

            this.toolTip1.SetToolTip(this.cbOptions, this.cbOptions.Text);
        }
    }
}