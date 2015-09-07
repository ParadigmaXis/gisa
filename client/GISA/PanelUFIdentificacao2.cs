using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

namespace GISA
{
    public class PanelUFIdentificacao2 : GISA.GISAPanel
    {

        #region  Windows Form Designer generated code

        public PanelUFIdentificacao2()
            : base()
        {

            //This call is required by the Windows Form Designer.
            InitializeComponent();

            //Add any initialization after the InitializeComponent() call
            btnMaterialManager.Click += btnMaterialManager_Click;

            clickTypeSelectorTimer.Tick += new EventHandler(OnTimedEvent);
            clickTypeSelectorTimer.Interval = 750;

            GetExtraResources();
        }

        //Form overrides dispose to clean up the component list.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        //Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.  
        //Do not modify it using the code editor.
        internal System.Windows.Forms.GroupBox grpGuia;
        internal System.Windows.Forms.TextBox txtGuia;
        internal System.Windows.Forms.GroupBox grpCodigoBarras;
        internal System.Windows.Forms.GroupBox grpCota;
        internal System.Windows.Forms.TextBox txtCota;
        internal System.Windows.Forms.GroupBox grpCodigo;
        internal System.Windows.Forms.TextBox txtCodigoDeReferencia;
        internal System.Windows.Forms.GroupBox grpFim;
        internal System.Windows.Forms.CheckBox chkAtribuidaFim;
        internal System.Windows.Forms.GroupBox grpInicio;
        internal System.Windows.Forms.CheckBox chkAtribuidaInicio;
        internal System.Windows.Forms.GroupBox grpDatasProducao;
        internal System.Windows.Forms.GroupBox grpConteudo;
        internal System.Windows.Forms.TextBox txtConteudoInformacional;
        internal System.Windows.Forms.GroupBox grpTitulo;
        internal System.Windows.Forms.TextBox txtTitulo;
        internal GISA.Controls.PxDateBox dtProducaoFim;
        internal GISA.Controls.PxDateBox dtProducaoInicio;
        internal System.Windows.Forms.GroupBox grpDimensoes;
        internal System.Windows.Forms.GroupBox grpTipo;
        internal System.Windows.Forms.ComboBox cbTipo;
        internal System.Windows.Forms.Button btnMaterialManager;
        private GISA.Controls.PxIntegerBox txtCodigoBarras;
        internal GroupBox grpTipoEntrega;
        internal ComboBox cbTipoEntrega;
        private GroupBox grpEntrega;
        private Label label1;
        private ToolTip toolTip1;
        private ControlLocalConsulta controlLocalConsulta1;
        internal GISA.DimensoesSuporte DimensoesSuporte1;
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PanelUFIdentificacao2));
            this.grpGuia = new System.Windows.Forms.GroupBox();
            this.txtGuia = new System.Windows.Forms.TextBox();
            this.grpCodigoBarras = new System.Windows.Forms.GroupBox();
            this.txtCodigoBarras = new GISA.Controls.PxIntegerBox();
            this.grpCota = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCota = new System.Windows.Forms.TextBox();
            this.grpCodigo = new System.Windows.Forms.GroupBox();
            this.txtCodigoDeReferencia = new System.Windows.Forms.TextBox();
            this.grpDimensoes = new System.Windows.Forms.GroupBox();
            this.DimensoesSuporte1 = new GISA.DimensoesSuporte();
            this.grpFim = new System.Windows.Forms.GroupBox();
            this.chkAtribuidaFim = new System.Windows.Forms.CheckBox();
            this.dtProducaoFim = new GISA.Controls.PxDateBox();
            this.grpInicio = new System.Windows.Forms.GroupBox();
            this.dtProducaoInicio = new GISA.Controls.PxDateBox();
            this.chkAtribuidaInicio = new System.Windows.Forms.CheckBox();
            this.grpDatasProducao = new System.Windows.Forms.GroupBox();
            this.grpConteudo = new System.Windows.Forms.GroupBox();
            this.txtConteudoInformacional = new System.Windows.Forms.TextBox();
            this.grpTitulo = new System.Windows.Forms.GroupBox();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.grpTipo = new System.Windows.Forms.GroupBox();
            this.btnMaterialManager = new System.Windows.Forms.Button();
            this.cbTipo = new System.Windows.Forms.ComboBox();
            this.grpTipoEntrega = new System.Windows.Forms.GroupBox();
            this.cbTipoEntrega = new System.Windows.Forms.ComboBox();
            this.grpEntrega = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.controlLocalConsulta1 = new GISA.ControlLocalConsulta();
            this.grpGuia.SuspendLayout();
            this.grpCodigoBarras.SuspendLayout();
            this.grpCota.SuspendLayout();
            this.grpCodigo.SuspendLayout();
            this.grpDimensoes.SuspendLayout();
            this.grpFim.SuspendLayout();
            this.grpInicio.SuspendLayout();
            this.grpDatasProducao.SuspendLayout();
            this.grpConteudo.SuspendLayout();
            this.grpTitulo.SuspendLayout();
            this.grpTipo.SuspendLayout();
            this.grpTipoEntrega.SuspendLayout();
            this.grpEntrega.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpGuia
            // 
            this.grpGuia.Controls.Add(this.txtGuia);
            this.grpGuia.Location = new System.Drawing.Point(6, 19);
            this.grpGuia.Name = "grpGuia";
            this.grpGuia.Size = new System.Drawing.Size(117, 50);
            this.grpGuia.TabIndex = 11;
            this.grpGuia.TabStop = false;
            this.grpGuia.Text = "Guia nº";
            // 
            // txtGuia
            // 
            this.txtGuia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGuia.Location = new System.Drawing.Point(6, 19);
            this.txtGuia.Name = "txtGuia";
            this.txtGuia.Size = new System.Drawing.Size(105, 20);
            this.txtGuia.TabIndex = 1;
            // 
            // grpCodigoBarras
            // 
            this.grpCodigoBarras.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCodigoBarras.Controls.Add(this.txtCodigoBarras);
            this.grpCodigoBarras.Location = new System.Drawing.Point(496, 100);
            this.grpCodigoBarras.Name = "grpCodigoBarras";
            this.grpCodigoBarras.Size = new System.Drawing.Size(297, 43);
            this.grpCodigoBarras.TabIndex = 2;
            this.grpCodigoBarras.TabStop = false;
            this.grpCodigoBarras.Text = "Código de barras";
            // 
            // txtCodigoBarras
            // 
            this.txtCodigoBarras.Location = new System.Drawing.Point(8, 16);
            this.txtCodigoBarras.Name = "txtCodigoBarras";
            this.txtCodigoBarras.Size = new System.Drawing.Size(280, 20);
            this.txtCodigoBarras.TabIndex = 0;
            this.txtCodigoBarras.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // grpCota
            // 
            this.grpCota.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCota.Controls.Add(this.label1);
            this.grpCota.Controls.Add(this.txtCota);
            this.grpCota.Location = new System.Drawing.Point(6, 100);
            this.grpCota.Name = "grpCota";
            this.grpCota.Size = new System.Drawing.Size(482, 43);
            this.grpCota.TabIndex = 1;
            this.grpCota.TabStop = false;
            this.grpCota.Text = "Cota";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.Location = new System.Drawing.Point(456, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "     ";
            // 
            // txtCota
            // 
            this.txtCota.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCota.Location = new System.Drawing.Point(8, 16);
            this.txtCota.MaxLength = 50;
            this.txtCota.Name = "txtCota";
            this.txtCota.Size = new System.Drawing.Size(442, 20);
            this.txtCota.TabIndex = 1;
            // 
            // grpCodigo
            // 
            this.grpCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCodigo.Controls.Add(this.txtCodigoDeReferencia);
            this.grpCodigo.Location = new System.Drawing.Point(6, 4);
            this.grpCodigo.Name = "grpCodigo";
            this.grpCodigo.Size = new System.Drawing.Size(482, 43);
            this.grpCodigo.TabIndex = 10;
            this.grpCodigo.TabStop = false;
            this.grpCodigo.Text = "Código de referência";
            // 
            // txtCodigoDeReferencia
            // 
            this.txtCodigoDeReferencia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodigoDeReferencia.Location = new System.Drawing.Point(8, 16);
            this.txtCodigoDeReferencia.Name = "txtCodigoDeReferencia";
            this.txtCodigoDeReferencia.ReadOnly = true;
            this.txtCodigoDeReferencia.Size = new System.Drawing.Size(468, 20);
            this.txtCodigoDeReferencia.TabIndex = 1;
            // 
            // grpDimensoes
            // 
            this.grpDimensoes.Controls.Add(this.DimensoesSuporte1);
            this.grpDimensoes.Location = new System.Drawing.Point(6, 220);
            this.grpDimensoes.Name = "grpDimensoes";
            this.grpDimensoes.Size = new System.Drawing.Size(386, 67);
            this.grpDimensoes.TabIndex = 4;
            this.grpDimensoes.TabStop = false;
            this.grpDimensoes.Text = "Dimensões";
            // 
            // DimensoesSuporte1
            // 
            this.DimensoesSuporte1.Location = new System.Drawing.Point(6, 12);
            this.DimensoesSuporte1.MedidaAltura = "";
            this.DimensoesSuporte1.MedidaLargura = "";
            this.DimensoesSuporte1.MedidaProfundidade = "";
            this.DimensoesSuporte1.Name = "DimensoesSuporte1";
            this.DimensoesSuporte1.Size = new System.Drawing.Size(366, 50);
            this.DimensoesSuporte1.TabIndex = 1;
            this.DimensoesSuporte1.TipoMedida = null;
            // 
            // grpFim
            // 
            this.grpFim.Controls.Add(this.chkAtribuidaFim);
            this.grpFim.Controls.Add(this.dtProducaoFim);
            this.grpFim.Location = new System.Drawing.Point(202, 15);
            this.grpFim.Name = "grpFim";
            this.grpFim.Size = new System.Drawing.Size(178, 44);
            this.grpFim.TabIndex = 2;
            this.grpFim.TabStop = false;
            this.grpFim.Text = "Fim";
            // 
            // chkAtribuidaFim
            // 
            this.chkAtribuidaFim.Location = new System.Drawing.Point(97, 16);
            this.chkAtribuidaFim.Name = "chkAtribuidaFim";
            this.chkAtribuidaFim.Size = new System.Drawing.Size(72, 20);
            this.chkAtribuidaFim.TabIndex = 2;
            this.chkAtribuidaFim.Text = "Atribuída";
            // 
            // dtProducaoFim
            // 
            this.dtProducaoFim.Location = new System.Drawing.Point(7, 14);
            this.dtProducaoFim.Name = "dtProducaoFim";
            this.dtProducaoFim.Size = new System.Drawing.Size(82, 22);
            this.dtProducaoFim.TabIndex = 1;
            this.dtProducaoFim.ValueDay = "";
            this.dtProducaoFim.ValueMonth = "";
            this.dtProducaoFim.ValueYear = "";
            // 
            // grpInicio
            // 
            this.grpInicio.Controls.Add(this.dtProducaoInicio);
            this.grpInicio.Controls.Add(this.chkAtribuidaInicio);
            this.grpInicio.Location = new System.Drawing.Point(8, 15);
            this.grpInicio.Name = "grpInicio";
            this.grpInicio.Size = new System.Drawing.Size(182, 44);
            this.grpInicio.TabIndex = 1;
            this.grpInicio.TabStop = false;
            this.grpInicio.Text = "Início";
            // 
            // dtProducaoInicio
            // 
            this.dtProducaoInicio.Location = new System.Drawing.Point(7, 14);
            this.dtProducaoInicio.Name = "dtProducaoInicio";
            this.dtProducaoInicio.Size = new System.Drawing.Size(82, 22);
            this.dtProducaoInicio.TabIndex = 1;
            this.dtProducaoInicio.ValueDay = "";
            this.dtProducaoInicio.ValueMonth = "";
            this.dtProducaoInicio.ValueYear = "";
            // 
            // chkAtribuidaInicio
            // 
            this.chkAtribuidaInicio.Location = new System.Drawing.Point(95, 16);
            this.chkAtribuidaInicio.Name = "chkAtribuidaInicio";
            this.chkAtribuidaInicio.Size = new System.Drawing.Size(73, 18);
            this.chkAtribuidaInicio.TabIndex = 2;
            this.chkAtribuidaInicio.Text = "Atribuída";
            // 
            // grpDatasProducao
            // 
            this.grpDatasProducao.Controls.Add(this.grpInicio);
            this.grpDatasProducao.Controls.Add(this.grpFim);
            this.grpDatasProducao.Location = new System.Drawing.Point(6, 148);
            this.grpDatasProducao.Name = "grpDatasProducao";
            this.grpDatasProducao.Size = new System.Drawing.Size(386, 67);
            this.grpDatasProducao.TabIndex = 3;
            this.grpDatasProducao.TabStop = false;
            this.grpDatasProducao.Text = "Datas de produção";
            // 
            // grpConteudo
            // 
            this.grpConteudo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpConteudo.Controls.Add(this.txtConteudoInformacional);
            this.grpConteudo.Location = new System.Drawing.Point(400, 148);
            this.grpConteudo.Name = "grpConteudo";
            this.grpConteudo.Size = new System.Drawing.Size(393, 443);
            this.grpConteudo.TabIndex = 6;
            this.grpConteudo.TabStop = false;
            this.grpConteudo.Text = "Conteúdo informacional";
            // 
            // txtConteudoInformacional
            // 
            this.txtConteudoInformacional.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConteudoInformacional.Location = new System.Drawing.Point(8, 16);
            this.txtConteudoInformacional.Multiline = true;
            this.txtConteudoInformacional.Name = "txtConteudoInformacional";
            this.txtConteudoInformacional.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConteudoInformacional.Size = new System.Drawing.Size(376, 419);
            this.txtConteudoInformacional.TabIndex = 1;
            // 
            // grpTitulo
            // 
            this.grpTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTitulo.Controls.Add(this.txtTitulo);
            this.grpTitulo.Location = new System.Drawing.Point(6, 52);
            this.grpTitulo.Name = "grpTitulo";
            this.grpTitulo.Size = new System.Drawing.Size(482, 43);
            this.grpTitulo.TabIndex = 12;
            this.grpTitulo.TabStop = false;
            this.grpTitulo.Text = "Título";
            // 
            // txtTitulo
            // 
            this.txtTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitulo.Location = new System.Drawing.Point(8, 16);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.ReadOnly = true;
            this.txtTitulo.Size = new System.Drawing.Size(468, 20);
            this.txtTitulo.TabIndex = 1;
            // 
            // grpTipo
            // 
            this.grpTipo.Controls.Add(this.btnMaterialManager);
            this.grpTipo.Controls.Add(this.cbTipo);
            this.grpTipo.Location = new System.Drawing.Point(6, 288);
            this.grpTipo.Name = "grpTipo";
            this.grpTipo.Size = new System.Drawing.Size(386, 42);
            this.grpTipo.TabIndex = 5;
            this.grpTipo.TabStop = false;
            this.grpTipo.Text = "Tipo";
            // 
            // btnMaterialManager
            // 
            this.btnMaterialManager.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMaterialManager.Location = new System.Drawing.Point(356, 12);
            this.btnMaterialManager.Name = "btnMaterialManager";
            this.btnMaterialManager.Size = new System.Drawing.Size(24, 23);
            this.btnMaterialManager.TabIndex = 2;
            // 
            // cbTipo
            // 
            this.cbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipo.Location = new System.Drawing.Point(6, 14);
            this.cbTipo.Name = "cbTipo";
            this.cbTipo.Size = new System.Drawing.Size(344, 21);
            this.cbTipo.TabIndex = 1;
            // 
            // grpTipoEntrega
            // 
            this.grpTipoEntrega.Controls.Add(this.cbTipoEntrega);
            this.grpTipoEntrega.Location = new System.Drawing.Point(129, 19);
            this.grpTipoEntrega.Name = "grpTipoEntrega";
            this.grpTipoEntrega.Size = new System.Drawing.Size(159, 50);
            this.grpTipoEntrega.TabIndex = 12;
            this.grpTipoEntrega.TabStop = false;
            this.grpTipoEntrega.Text = "Tipo";
            // 
            // cbTipoEntrega
            // 
            this.cbTipoEntrega.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoEntrega.FormattingEnabled = true;
            this.cbTipoEntrega.Location = new System.Drawing.Point(6, 19);
            this.cbTipoEntrega.Name = "cbTipoEntrega";
            this.cbTipoEntrega.Size = new System.Drawing.Size(147, 21);
            this.cbTipoEntrega.TabIndex = 0;
            // 
            // grpEntrega
            // 
            this.grpEntrega.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpEntrega.Controls.Add(this.grpGuia);
            this.grpEntrega.Controls.Add(this.grpTipoEntrega);
            this.grpEntrega.Location = new System.Drawing.Point(496, 4);
            this.grpEntrega.Name = "grpEntrega";
            this.grpEntrega.Size = new System.Drawing.Size(297, 90);
            this.grpEntrega.TabIndex = 13;
            this.grpEntrega.TabStop = false;
            this.grpEntrega.Text = "Entrega";
            // 
            // toolTip1
            // 
            this.toolTip1.Active = false;
            this.toolTip1.AutoPopDelay = 60000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ShowAlways = true;
            // 
            // controlLocalConsulta1
            // 
            this.controlLocalConsulta1.Location = new System.Drawing.Point(6, 336);
            this.controlLocalConsulta1.Name = "controlLocalConsulta1";
            this.controlLocalConsulta1.Size = new System.Drawing.Size(388, 48);
            this.controlLocalConsulta1.TabIndex = 14;
            // 
            // PanelUFIdentificacao2
            // 
            this.Controls.Add(this.controlLocalConsulta1);
            this.Controls.Add(this.grpEntrega);
            this.Controls.Add(this.grpTipo);
            this.Controls.Add(this.grpTitulo);
            this.Controls.Add(this.grpConteudo);
            this.Controls.Add(this.grpDatasProducao);
            this.Controls.Add(this.grpDimensoes);
            this.Controls.Add(this.grpCota);
            this.Controls.Add(this.grpCodigo);
            this.Controls.Add(this.grpCodigoBarras);
            this.Name = "PanelUFIdentificacao2";
            this.grpGuia.ResumeLayout(false);
            this.grpGuia.PerformLayout();
            this.grpCodigoBarras.ResumeLayout(false);
            this.grpCota.ResumeLayout(false);
            this.grpCota.PerformLayout();
            this.grpCodigo.ResumeLayout(false);
            this.grpCodigo.PerformLayout();
            this.grpDimensoes.ResumeLayout(false);
            this.grpFim.ResumeLayout(false);
            this.grpInicio.ResumeLayout(false);
            this.grpDatasProducao.ResumeLayout(false);
            this.grpConteudo.ResumeLayout(false);
            this.grpConteudo.PerformLayout();
            this.grpTitulo.ResumeLayout(false);
            this.grpTitulo.PerformLayout();
            this.grpTipo.ResumeLayout(false);
            this.grpTipoEntrega.ResumeLayout(false);
            this.grpEntrega.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private void GetExtraResources()
        {
            btnMaterialManager.Image = SharedResourcesOld.CurrentSharedResources.Editar;

            base.ParentChanged += PanelIdentificacao_ParentChanged;
        }

        // runs only once. sets tooltip as soon as it's parent appears
        private void PanelIdentificacao_ParentChanged(object sender, System.EventArgs e)
        {
            MultiPanel.CurrentToolTip.SetToolTip(btnMaterialManager, SharedResourcesOld.CurrentSharedResources.EditarString);
            base.ParentChanged -= PanelIdentificacao_ParentChanged;
        }

        protected GISADataset.FRDBaseRow CurrentUFFRDBase;
        private GISADataset.SFRDDatasProducaoRow CurrentUFSFRDDatasProducao;
        private GISADataset.SFRDUFCotaRow CurrentUFSFRDCota;
        private GISADataset.SFRDUFDescricaoFisicaRow CurrentUFSFRDDescricaoFisica;
        private GISADataset.SFRDConteudoEEstruturaRow CurrentUFSFRDConteudoEEstrutura;
        private GISADataset.NivelUnidadeFisicaRow CurrentNivelUnidadeFisica;

        public string actGuia
        {
            get { return ""; }
            set { this.txtGuia.Text = value; }
        }

        public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
        {
            IsLoaded = false;
            CurrentUFFRDBase = (GISADataset.FRDBaseRow)CurrentDataRow;

            try
            {
                UFRule.Current.LoadUFIdentificacao2Data(GisaDataSetHelper.GetInstance(), CurrentUFFRDBase.ID, conn);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
            IsLoaded = true;
        }

        public override void ModelToView()
        {
            IsPopulated = false;
            string QueryFilter = string.Format("IDFRDBase={0}", CurrentUFFRDBase.ID);
            GISADataset.SFRDUFCotaRow[] cotaRows = null;
            cotaRows = (GISADataset.SFRDUFCotaRow[])(GisaDataSetHelper.GetInstance().SFRDUFCota.Select(QueryFilter));

            if (cotaRows.Length == 0)
                CurrentUFSFRDCota = GisaDataSetHelper.GetInstance().SFRDUFCota.AddSFRDUFCotaRow(CurrentUFFRDBase, "", new byte[] { }, 0);
            else
                CurrentUFSFRDCota = cotaRows[0];

            GISADataset.SFRDConteudoEEstruturaRow[] conteudoRows = null;
            conteudoRows = (GISADataset.SFRDConteudoEEstruturaRow[])(GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.Select(QueryFilter));
            if (conteudoRows.Length == 0)
                CurrentUFSFRDConteudoEEstrutura = GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.AddSFRDConteudoEEstruturaRow(CurrentUFFRDBase, "", "", new byte[] { }, 0);
            else
                CurrentUFSFRDConteudoEEstrutura = conteudoRows[0];

            GISADataset.SFRDDatasProducaoRow[] datasProducaoRows = null;
            datasProducaoRows = (GISADataset.SFRDDatasProducaoRow[])(GisaDataSetHelper.GetInstance().SFRDDatasProducao.Select(QueryFilter));
            if (datasProducaoRows.Length == 0)
            {
                CurrentUFSFRDDatasProducao = GisaDataSetHelper.GetInstance().SFRDDatasProducao.NewSFRDDatasProducaoRow();
                CurrentUFSFRDDatasProducao.FRDBaseRow = CurrentUFFRDBase;
                CurrentUFSFRDDatasProducao.InicioAtribuida = false;
                CurrentUFSFRDDatasProducao.FimAtribuida = false;
                GisaDataSetHelper.GetInstance().SFRDDatasProducao.AddSFRDDatasProducaoRow(CurrentUFSFRDDatasProducao);
            }
            else
                CurrentUFSFRDDatasProducao = datasProducaoRows[0];

            GISADataset.SFRDUFDescricaoFisicaRow[] descricaoFisicaRows = null;
            descricaoFisicaRows = (GISADataset.SFRDUFDescricaoFisicaRow[])(GisaDataSetHelper.GetInstance().SFRDUFDescricaoFisica.Select(QueryFilter));
            if (descricaoFisicaRows.Length == 0)
            {
                CurrentUFSFRDDescricaoFisica = GisaDataSetHelper.GetInstance().SFRDUFDescricaoFisica.NewSFRDUFDescricaoFisicaRow();
                CurrentUFSFRDDescricaoFisica.FRDBaseRow = CurrentUFFRDBase;
                CurrentUFSFRDDescricaoFisica.IDTipoMedida = 1;
                CurrentUFSFRDDescricaoFisica.TipoAcondicionamentoRow = (GISADataset.TipoAcondicionamentoRow)(GisaDataSetHelper.GetInstance().TipoAcondicionamento.Select(string.Format("ID={0:d}", TipoAcondicionamento.Pasta))[0]);
                GisaDataSetHelper.GetInstance().SFRDUFDescricaoFisica.AddSFRDUFDescricaoFisicaRow(CurrentUFSFRDDescricaoFisica);
            }
            else
                CurrentUFSFRDDescricaoFisica = descricaoFisicaRows[0];

            GISADataset.NivelUnidadeFisicaRow[] nivelUFRows = null;
            nivelUFRows = (GISADataset.NivelUnidadeFisicaRow[])(GisaDataSetHelper.GetInstance().NivelUnidadeFisica.Select(string.Format("ID={0}", CurrentUFFRDBase.NivelRow.ID)));
            if (nivelUFRows.Length == 0)
            {
                GISADataset.NivelUnidadeFisicaRow newNivelUFRow = null;
                newNivelUFRow = GisaDataSetHelper.GetInstance().NivelUnidadeFisica.NewNivelUnidadeFisicaRow();
                newNivelUFRow.NivelDesignadoRow = CurrentUFFRDBase.NivelRow.GetNivelDesignadoRows()[0];
                GisaDataSetHelper.GetInstance().NivelUnidadeFisica.AddNivelUnidadeFisicaRow(newNivelUFRow);

                CurrentNivelUnidadeFisica = newNivelUFRow;
            }
            else
                CurrentNivelUnidadeFisica = (GISADataset.NivelUnidadeFisicaRow)(nivelUFRows[0]);

            GUIHelper.GUIHelper.populateDataInicio(dtProducaoInicio, CurrentUFSFRDDatasProducao);
            GUIHelper.GUIHelper.populateDataFim(dtProducaoFim, CurrentUFSFRDDatasProducao);

            DimensoesSuporte1.populateDimensoes(CurrentUFSFRDDescricaoFisica);

            cbTipo.DisplayMember = "Designacao";
            cbTipo.Items.Clear();
            cbTipo.Items.AddRange(GisaDataSetHelper.GetInstance().TipoAcondicionamento.Select());
            cbTipo.SelectedItem = CurrentUFSFRDDescricaoFisica.TipoAcondicionamentoRow;

            chkAtribuidaInicio.Checked = CurrentUFSFRDDatasProducao.InicioAtribuida;
            chkAtribuidaFim.Checked = CurrentUFSFRDDatasProducao.FimAtribuida;

            if (CurrentNivelUnidadeFisica.IsCodigoBarrasNull())
                txtCodigoBarras.Text = "";
            else
                txtCodigoBarras.Text = CurrentNivelUnidadeFisica.CodigoBarras.ToString();

            if (CurrentNivelUnidadeFisica.IsGuiaIncorporacaoNull())
                txtGuia.Text = "";
            else
                txtGuia.Text = CurrentNivelUnidadeFisica.GuiaIncorporacao;

            if (CurrentUFSFRDCota.IsCotaNull())
                txtCota.Text = "";
            else
                txtCota.Text = CurrentUFSFRDCota.Cota;

            this.txtCota.TextChanged += new System.EventHandler(this.txtCota_TextChanged);

            if (!(CurrentUFSFRDConteudoEEstrutura.IsConteudoInformacionalNull()))
                txtConteudoInformacional.Text = CurrentUFSFRDConteudoEEstrutura.ConteudoInformacional;
            else
                txtConteudoInformacional.Text = "";

            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                txtCodigoDeReferencia.Text = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.GetCodigoOfNivel(CurrentUFFRDBase.NivelRow.ID, ho.Connection)[0].ToString();
            }
            finally
            {
                ho.Dispose();
            }
            txtTitulo.Text = Nivel.GetDesignacao(CurrentUFFRDBase.NivelRow);

            // Tipo entrega
            cbTipoEntrega.Items.Add(string.Empty);
            cbTipoEntrega.Items.AddRange(GisaDataSetHelper.GetInstance().TipoEntrega.Rows.Cast<GISADataset.TipoEntregaRow>().ToArray());
            cbTipoEntrega.DisplayMember = "Designacao";

            if (CurrentNivelUnidadeFisica.IsIDTipoEntregaNull())
                cbTipoEntrega.SelectedIndex = 0;
            else
                cbTipoEntrega.SelectedItem = CurrentNivelUnidadeFisica.TipoEntregaRow;

            // Local consulta
            controlLocalConsulta1.rebindToData();
            if (CurrentNivelUnidadeFisica.IsIDLocalConsultaNull())
                controlLocalConsulta1.cbLocalConsulta.SelectedIndex = 0;
            else
                controlLocalConsulta1.cbLocalConsulta.SelectedValue = CurrentNivelUnidadeFisica.IDLocalConsulta;

            IsPopulated = true;
        }

        public override void ViewToModel()
        {
            if (CurrentUFFRDBase == null || CurrentUFFRDBase.RowState == DataRowState.Detached || !IsLoaded)
            {
                Debug.Assert(false, "Informação da UF não ser gravada");
                return;
            }

            if (!IsLoaded)
                return;

            CurrentNivelUnidadeFisica.CodigoBarras = txtCodigoBarras.Text;            

            CurrentUFSFRDCota.Cota = txtCota.Text;
            CurrentUFSFRDConteudoEEstrutura.ConteudoInformacional = txtConteudoInformacional.Text;

            CurrentUFSFRDDatasProducao.InicioAtribuida = chkAtribuidaInicio.Checked;
            CurrentUFSFRDDatasProducao.FimAtribuida = chkAtribuidaFim.Checked;

            if (CurrentUFSFRDDatasProducao != null)
            {
                GUIHelper.GUIHelper.storeDataInicio(dtProducaoInicio, CurrentUFSFRDDatasProducao);
                GUIHelper.GUIHelper.storeDataFim(dtProducaoFim, CurrentUFSFRDDatasProducao);
            }

            if (CurrentUFSFRDDescricaoFisica != null)
                DimensoesSuporte1.storeDimensoes(ref CurrentUFSFRDDescricaoFisica, false);

            CurrentUFSFRDDescricaoFisica.TipoAcondicionamentoRow = (GISADataset.TipoAcondicionamentoRow)cbTipo.SelectedItem;

            // Tipo entrega
            if (cbTipoEntrega.SelectedIndex == 0)
                CurrentNivelUnidadeFisica.TipoEntregaRow = null;
            else
                CurrentNivelUnidadeFisica.TipoEntregaRow = (GISADataset.TipoEntregaRow)cbTipoEntrega.SelectedItem;

            // Guia Incorporacao
            CurrentNivelUnidadeFisica.GuiaIncorporacao = txtGuia.Text;

            // Local consulta
            if (controlLocalConsulta1.cbLocalConsulta.SelectedIndex == 0)
                CurrentNivelUnidadeFisica.LocalConsultaRow = null;
            else
                CurrentNivelUnidadeFisica.LocalConsultaRow = GisaDataSetHelper.GetInstance().LocalConsulta.Cast<GISADataset.LocalConsultaRow>().Single(lcRow => lcRow.ID == (long)controlLocalConsulta1.cbLocalConsulta.SelectedValue);
        }

        public override void Deactivate()
        {
            this.txtCota.TextChanged -= new System.EventHandler(this.txtCota_TextChanged);
            GUIHelper.GUIHelper.clearField(txtCodigoDeReferencia);
            GUIHelper.GUIHelper.clearField(txtGuia);
            GUIHelper.GUIHelper.clearField(txtTitulo);
            GUIHelper.GUIHelper.clearField(txtCota);
            GUIHelper.GUIHelper.clearField(txtCodigoBarras);
            GUIHelper.GUIHelper.clearField(dtProducaoInicio);
            GUIHelper.GUIHelper.clearField(dtProducaoFim);
            GUIHelper.GUIHelper.clearField(chkAtribuidaInicio);
            GUIHelper.GUIHelper.clearField(chkAtribuidaFim);
            GUIHelper.GUIHelper.clearField(txtCodigoBarras);
            GUIHelper.GUIHelper.clearField(txtGuia);
            GUIHelper.GUIHelper.clearField(txtConteudoInformacional);
            GUIHelper.GUIHelper.clearField(cbTipo);
            cbTipoEntrega.Items.Clear();

            DimensoesSuporte1.clear();

            CurrentNivelUnidadeFisica = null;
            CurrentUFSFRDDatasProducao = null;
            CurrentUFSFRDCota = null;
            CurrentUFSFRDDescricaoFisica = null;
            CurrentUFSFRDConteudoEEstrutura = null;

            label1.Enabled = false;
            toolTip1.Active = false;
        }

        private void btnMaterialManager_Click(object sender, System.EventArgs e)
        {
            if (CurrentUFFRDBase.RowState == DataRowState.Detached)
            {
                MessageBox.Show("A unidade física selecionada foi apagada por outro utilizador motivo pelo qual " + System.Environment.NewLine + "não é possível editar os Tipos de Material.", "Unidade física apagada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IDbConnection conn = GisaDataSetHelper.GetConnection();
            try
            {
                conn.Open();
                UFRule.Current.LoadTipoAcondicionamento(GisaDataSetHelper.GetInstance(), conn);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
            finally
            {
                conn.Close();
            }
            FormMateriaisEditor formMatEditor = new FormMateriaisEditor();
            formMatEditor.LoadData(GisaDataSetHelper.GetInstance().TipoAcondicionamento.Select("NOT Designacao = '<Desconhecido>'"), "Designacao");
            formMatEditor.ShowDialog();

            GISA.Model.PersistencyHelper.ManageDescFisicasPreConcArguments args = new GISA.Model.PersistencyHelper.ManageDescFisicasPreConcArguments();
            args.frdID = CurrentUFFRDBase.ID;
            ArrayList rowList = new ArrayList();
            foreach (DataRow row in GisaDataSetHelper.GetInstance().Tables["TipoAcondicionamento"].Select("", "", DataViewRowState.Deleted))
                rowList.Add(row["ID", DataRowVersion.Original]);

            args.quant = rowList;

            PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save(TipoAcondicionamentoIsBeingUsedByOthers, args);
            PersistencyHelper.cleanDeletedData();

            if (args.aResult == PersistencyHelper.ManageDescFisicasPreConcArguments.ActionResult.quantidadeUsedByOthers)
                MessageBox.Show("Um ou mais tipos de acondicionamento não puderam ser" + Environment.NewLine + "removidos por estarem atualmente em uso.", "Remoção de Elementos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (successfulSave == PersistencyHelper.SaveResult.successful)
                GISA.Search.Updater.updateUnidadeFisica(CurrentUFFRDBase.NivelRow.ID);

            GISADataset.TipoAcondicionamentoRow selectedTipoAcondicionamento = (GISADataset.TipoAcondicionamentoRow)cbTipo.SelectedItem;
            cbTipo.Items.Clear();
            cbTipo.DisplayMember = "Designacao";
            cbTipo.Items.AddRange(GisaDataSetHelper.GetInstance().TipoAcondicionamento.Select("", "Designacao"));

            if (selectedTipoAcondicionamento != null)
                cbTipo.SelectedItem = selectedTipoAcondicionamento;
            else
                cbTipo.SelectedItem = (GISADataset.TipoAcondicionamentoRow)(GisaDataSetHelper.GetInstance().TipoAcondicionamento.Select(string.Format("ID={0:d}", TipoAcondicionamento.Pasta))[0]);
        }

        // garantir que o tipo de quantidade da descrição física existe aquando da sua gravação na BD
        private void TipoAcondicionamentoIsBeingUsedByOthers(GISA.Model.PersistencyHelper.PreConcArguments args)
        {
            GISA.Model.PersistencyHelper.ManageDescFisicasPreConcArguments mcfPca = null;
            mcfPca = (GISA.Model.PersistencyHelper.ManageDescFisicasPreConcArguments)args;
            GISADataset.FRDBaseRow frd = (GISADataset.FRDBaseRow)(GisaDataSetHelper.GetInstance().FRDBase.Select("ID=" + mcfPca.frdID.ToString())[0]);
            ArrayList quantList = mcfPca.quant;

            int nTQuant = 0;
            GISADataset.TipoAcondicionamentoRow row = null;
            try
            {
                foreach (long quant in quantList)
                {
                    row = (GISADataset.TipoAcondicionamentoRow)(GisaDataSetHelper.GetInstance().TipoAcondicionamento.Select("ID=" + quant.ToString(), "", DataViewRowState.Deleted)[0]);
                    nTQuant = FRDRule.Current.CountUFDimensoesAcumuladas(quant, mcfPca.tran);
                    System.Data.DataSet tempgisaBackup1 = mcfPca.gisaBackup;
                    PersistencyHelper.BackupRow(ref tempgisaBackup1, row);
                    mcfPca.gisaBackup = tempgisaBackup1;
                    if (nTQuant > 0)
                    {
                        row.RejectChanges();
                        mcfPca.aResult = PersistencyHelper.ManageDescFisicasPreConcArguments.ActionResult.quantidadeUsedByOthers;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw ex;
            }
        }
        
        private void txtCota_TextChanged(object sender, EventArgs e)
        {
            clickTypeSelectorTimer.Stop();
            clickTypeSelectorTimer.Start();
        }

        private System.Windows.Forms.Timer clickTypeSelectorTimer = new System.Windows.Forms.Timer();
		private void OnTimedEvent(object source, EventArgs e)
		{
            clickTypeSelectorTimer.Stop();
            string ufs = string.Empty;
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                if (this.txtCota.Text.Trim().Length > 0)
                    ufs = FRDRule.Current.UFsWithSameCota(CurrentUFFRDBase.ID, txtCota.Text, ho.Connection);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
            finally
            {
                ho.Dispose();
            }

            if (ufs.Length == 0)
            {
                label1.Enabled = false;
                toolTip1.Active = false;
            }
            else
            {
                label1.Enabled = true;
                toolTip1.Active = true;
                toolTip1.SetToolTip(label1, "Unidades físicas com a mesma cota:" + ufs);
            }
		}
    }
}