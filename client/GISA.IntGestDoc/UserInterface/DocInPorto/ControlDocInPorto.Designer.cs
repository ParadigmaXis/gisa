namespace GISA.IntGestDoc.UserInterface.DocInPorto
{
    partial class ControlDocInPorto
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlDocInPorto));
            this.ilDocTypes = new System.Windows.Forms.ImageList(this.components);
            this.ilTSIcons = new System.Windows.Forms.ImageList(this.components);
            this.grpRelations = new System.Windows.Forms.GroupBox();
            this.lvRelations = new GISA.IntGestDoc.UserInterface.DocInPorto.DebuggedTreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.marcarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desmarcarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reverterOpçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.marcarTodosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.desmarcarTodosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.chCheck = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDataArquivo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chIdentificador = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTitulo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chProcesso = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbMarcacoes = new System.Windows.Forms.ToolStripDropDownButton();
            this.marcarTodosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desmarcarTodosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inverterMarcaçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbGravar = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainerDetails = new System.Windows.Forms.SplitContainer();
            this.gbDocInPorto = new System.Windows.Forms.GroupBox();
            this.controlDocumentoExterno1 = new GISA.IntGestDoc.UserInterface.ControlDocumentoExterno();
            this.controlDocumentoExternoAnexo1 = new GISA.IntGestDoc.UserInterface.ControlDocumentoExternoAnexo();
            this.controlDocumentoExternoProcesso1 = new GISA.IntGestDoc.UserInterface.ControlDocumentoExternoProcesso();
            this.grpGisa = new System.Windows.Forms.GroupBox();
            this.controlDocumentoGisa1 = new GISA.IntGestDoc.UserInterface.ControlDocumentoGisa();
            this.controlDocumentoGisaAnexo1 = new GISA.IntGestDoc.UserInterface.ControlDocumentoGisaAnexo();
            this.controlDocumentoGisaProcesso1 = new GISA.IntGestDoc.UserInterface.ControlDocumentoGisaProcesso();
            this.grpRelations.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.splitContainerDetails.Panel1.SuspendLayout();
            this.splitContainerDetails.Panel2.SuspendLayout();
            this.splitContainerDetails.SuspendLayout();
            this.gbDocInPorto.SuspendLayout();
            this.grpGisa.SuspendLayout();
            this.SuspendLayout();
            // 
            // ilDocTypes
            // 
            this.ilDocTypes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilDocTypes.ImageStream")));
            this.ilDocTypes.TransparentColor = System.Drawing.Color.Transparent;
            this.ilDocTypes.Images.SetKeyName(0, "Documento");
            this.ilDocTypes.Images.SetKeyName(1, "EntidadeProdutora");
            this.ilDocTypes.Images.SetKeyName(2, "Ideografico");
            this.ilDocTypes.Images.SetKeyName(3, "Onomastico");
            this.ilDocTypes.Images.SetKeyName(4, "TipologiaInformacional");
            this.ilDocTypes.Images.SetKeyName(5, "Toponimia");
            // 
            // ilTSIcons
            // 
            this.ilTSIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTSIcons.ImageStream")));
            this.ilTSIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTSIcons.Images.SetKeyName(0, "CheckAll");
            this.ilTSIcons.Images.SetKeyName(1, "UncheckAll");
            this.ilTSIcons.Images.SetKeyName(2, "Save");
            // 
            // grpRelations
            // 
            this.grpRelations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRelations.Controls.Add(this.lvRelations);
            this.grpRelations.Location = new System.Drawing.Point(3, 3);
            this.grpRelations.Name = "grpRelations";
            this.grpRelations.Size = new System.Drawing.Size(1106, 164);
            this.grpRelations.TabIndex = 3;
            this.grpRelations.TabStop = false;
            this.grpRelations.Text = "Documentos para arquivo";
            // 
            // lvRelations
            // 
            this.lvRelations.CheckBoxes = true;
            this.lvRelations.ContextMenuStrip = this.contextMenuStrip1;
            this.lvRelations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvRelations.FullRowSelect = true;
            this.lvRelations.HideSelection = false;
            this.lvRelations.Location = new System.Drawing.Point(3, 16);
            this.lvRelations.Name = "lvRelations";
            this.lvRelations.Size = new System.Drawing.Size(1100, 145);
            this.lvRelations.TabIndex = 1;
            this.lvRelations.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.lvRelations_AfterSelect);
            this.lvRelations.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvRelations_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.marcarToolStripMenuItem,
            this.desmarcarToolStripMenuItem,
            this.reverterOpçõesToolStripMenuItem,
            this.toolStripSeparator1,
            this.marcarTodosToolStripMenuItem1,
            this.desmarcarTodosToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(164, 120);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // marcarToolStripMenuItem
            // 
            this.marcarToolStripMenuItem.Name = "marcarToolStripMenuItem";
            this.marcarToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.marcarToolStripMenuItem.Text = "Marcar";
            this.marcarToolStripMenuItem.Click += new System.EventHandler(this.marcarToolStripMenuItem_Click);
            // 
            // desmarcarToolStripMenuItem
            // 
            this.desmarcarToolStripMenuItem.Name = "desmarcarToolStripMenuItem";
            this.desmarcarToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.desmarcarToolStripMenuItem.Text = "Desmarcar";
            this.desmarcarToolStripMenuItem.Click += new System.EventHandler(this.desmarcarToolStripMenuItem_Click);
            // 
            // reverterOpçõesToolStripMenuItem
            // 
            this.reverterOpçõesToolStripMenuItem.Name = "reverterOpçõesToolStripMenuItem";
            this.reverterOpçõesToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.reverterOpçõesToolStripMenuItem.Text = "Reverter opções";
            this.reverterOpçõesToolStripMenuItem.Click += new System.EventHandler(this.reverterOpçõesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(160, 6);
            // 
            // marcarTodosToolStripMenuItem1
            // 
            this.marcarTodosToolStripMenuItem1.Name = "marcarTodosToolStripMenuItem1";
            this.marcarTodosToolStripMenuItem1.Size = new System.Drawing.Size(163, 22);
            this.marcarTodosToolStripMenuItem1.Text = "Marcar todos";
            this.marcarTodosToolStripMenuItem1.Click += new System.EventHandler(this.marcarTodosToolStripMenuItem1_Click);
            // 
            // desmarcarTodosToolStripMenuItem1
            // 
            this.desmarcarTodosToolStripMenuItem1.Name = "desmarcarTodosToolStripMenuItem1";
            this.desmarcarTodosToolStripMenuItem1.Size = new System.Drawing.Size(163, 22);
            this.desmarcarTodosToolStripMenuItem1.Text = "Desmarcar todos";
            this.desmarcarTodosToolStripMenuItem1.Click += new System.EventHandler(this.desmarcarTodosToolStripMenuItem1_Click);
            // 
            // chCheck
            // 
            this.chCheck.Text = "";
            this.chCheck.Width = 24;
            // 
            // chDataArquivo
            // 
            this.chDataArquivo.Text = "Data arquivo";
            this.chDataArquivo.Width = 128;
            // 
            // chIdentificador
            // 
            this.chIdentificador.Text = "Identificador";
            this.chIdentificador.Width = 106;
            // 
            // chTitulo
            // 
            this.chTitulo.Text = "Título";
            this.chTitulo.Width = 363;
            // 
            // chProcesso
            // 
            this.chProcesso.Text = "Processo";
            this.chProcesso.Width = 106;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMarcacoes,
            this.tsbRefresh,
            this.tsbGravar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1116, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "Marcações";
            // 
            // tsbMarcacoes
            // 
            this.tsbMarcacoes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMarcacoes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.marcarTodosToolStripMenuItem,
            this.desmarcarTodosToolStripMenuItem,
            this.inverterMarcaçãoToolStripMenuItem});
            this.tsbMarcacoes.Image = global::GISA.IntGestDoc.Properties.Resources.action_check;
            this.tsbMarcacoes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMarcacoes.Name = "tsbMarcacoes";
            this.tsbMarcacoes.Size = new System.Drawing.Size(29, 22);
            this.tsbMarcacoes.Text = "Marcações";
            // 
            // marcarTodosToolStripMenuItem
            // 
            this.marcarTodosToolStripMenuItem.Name = "marcarTodosToolStripMenuItem";
            this.marcarTodosToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.marcarTodosToolStripMenuItem.Text = "Marcar todos";
            this.marcarTodosToolStripMenuItem.Click += new System.EventHandler(this.marcacaoToolStripMenuItem_Click);
            // 
            // desmarcarTodosToolStripMenuItem
            // 
            this.desmarcarTodosToolStripMenuItem.Name = "desmarcarTodosToolStripMenuItem";
            this.desmarcarTodosToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.desmarcarTodosToolStripMenuItem.Text = "Desmarcar todos";
            this.desmarcarTodosToolStripMenuItem.Click += new System.EventHandler(this.marcacaoToolStripMenuItem_Click);
            // 
            // inverterMarcaçãoToolStripMenuItem
            // 
            this.inverterMarcaçãoToolStripMenuItem.Name = "inverterMarcaçãoToolStripMenuItem";
            this.inverterMarcaçãoToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.inverterMarcaçãoToolStripMenuItem.Text = "Inverter marcação";
            this.inverterMarcaçãoToolStripMenuItem.Click += new System.EventHandler(this.marcacaoToolStripMenuItem_Click);
            // 
            // tsbGravar
            // 
            this.tsbGravar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbGravar.Enabled = false;
            this.tsbGravar.Image = global::GISA.IntGestDoc.Properties.Resources.integrar3;
            this.tsbGravar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGravar.Name = "tsbGravar";
            this.tsbGravar.Size = new System.Drawing.Size(23, 22);
            this.tsbGravar.Text = "Integrar todos os documentos marcados";
            this.tsbGravar.Click += new System.EventHandler(this.tsbGravar_Click);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefresh.Image = global::GISA.IntGestDoc.Properties.Resources.Actualizar;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsbRefresh.Text = "Actualizar dados";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 25);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.grpRelations);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerDetails);
            this.splitContainerMain.Size = new System.Drawing.Size(1116, 749);
            this.splitContainerMain.SplitterDistance = 172;
            this.splitContainerMain.SplitterWidth = 2;
            this.splitContainerMain.TabIndex = 8;
            // 
            // splitContainerDetails
            // 
            this.splitContainerDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainerDetails.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerDetails.Location = new System.Drawing.Point(0, 0);
            this.splitContainerDetails.Name = "splitContainerDetails";
            // 
            // splitContainerDetails.Panel1
            // 
            this.splitContainerDetails.Panel1.Controls.Add(this.gbDocInPorto);
            // 
            // splitContainerDetails.Panel2
            // 
            this.splitContainerDetails.Panel2.Controls.Add(this.grpGisa);
            this.splitContainerDetails.Size = new System.Drawing.Size(1114, 573);
            this.splitContainerDetails.SplitterDistance = 400;
            this.splitContainerDetails.SplitterWidth = 3;
            this.splitContainerDetails.TabIndex = 1;
            // 
            // gbDocInPorto
            // 
            this.gbDocInPorto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDocInPorto.Controls.Add(this.controlDocumentoExterno1);
            this.gbDocInPorto.Controls.Add(this.controlDocumentoExternoAnexo1);
            this.gbDocInPorto.Controls.Add(this.controlDocumentoExternoProcesso1);
            this.gbDocInPorto.Location = new System.Drawing.Point(2, 3);
            this.gbDocInPorto.Name = "gbDocInPorto";
            this.gbDocInPorto.Size = new System.Drawing.Size(392, 563);
            this.gbDocInPorto.TabIndex = 7;
            this.gbDocInPorto.TabStop = false;
            // 
            // controlDocumentoExterno1
            // 
            this.controlDocumentoExterno1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlDocumentoExterno1.AutoScroll = true;
            this.controlDocumentoExterno1.Location = new System.Drawing.Point(6, 19);
            this.controlDocumentoExterno1.Name = "controlDocumentoExterno1";
            this.controlDocumentoExterno1.Size = new System.Drawing.Size(380, 538);
            this.controlDocumentoExterno1.TabIndex = 0;
            this.controlDocumentoExterno1.Visible = false;
            // 
            // controlDocumentoExternoAnexo1
            // 
            this.controlDocumentoExternoAnexo1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlDocumentoExternoAnexo1.AutoScroll = true;
            this.controlDocumentoExternoAnexo1.Location = new System.Drawing.Point(6, 19);
            this.controlDocumentoExternoAnexo1.Name = "controlDocumentoExternoAnexo1";
            this.controlDocumentoExternoAnexo1.Size = new System.Drawing.Size(380, 538);
            this.controlDocumentoExternoAnexo1.TabIndex = 0;
            this.controlDocumentoExternoAnexo1.Visible = false;
            // 
            // controlDocumentoExternoProcesso1
            // 
            this.controlDocumentoExternoProcesso1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlDocumentoExternoProcesso1.AutoScroll = true;
            this.controlDocumentoExternoProcesso1.Location = new System.Drawing.Point(6, 19);
            this.controlDocumentoExternoProcesso1.Name = "controlDocumentoExternoProcesso1";
            this.controlDocumentoExternoProcesso1.Size = new System.Drawing.Size(380, 538);
            this.controlDocumentoExternoProcesso1.TabIndex = 0;
            this.controlDocumentoExternoProcesso1.Visible = false;
            // 
            // grpGisa
            // 
            this.grpGisa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpGisa.Controls.Add(this.controlDocumentoGisa1);
            this.grpGisa.Controls.Add(this.controlDocumentoGisaAnexo1);
            this.grpGisa.Controls.Add(this.controlDocumentoGisaProcesso1);
            this.grpGisa.Location = new System.Drawing.Point(3, 3);
            this.grpGisa.Name = "grpGisa";
            this.grpGisa.Size = new System.Drawing.Size(701, 565);
            this.grpGisa.TabIndex = 6;
            this.grpGisa.TabStop = false;
            this.grpGisa.Text = "Documento Gisa";
            // 
            // controlDocumentoGisa1
            // 
            this.controlDocumentoGisa1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlDocumentoGisa1.AutoScroll = true;
            this.controlDocumentoGisa1.Location = new System.Drawing.Point(6, 19);
            this.controlDocumentoGisa1.Name = "controlDocumentoGisa1";
            this.controlDocumentoGisa1.Size = new System.Drawing.Size(689, 538);
            this.controlDocumentoGisa1.TabIndex = 0;
            this.controlDocumentoGisa1.Visible = false;
            // 
            // controlDocumentoGisaAnexo1
            // 
            this.controlDocumentoGisaAnexo1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlDocumentoGisaAnexo1.AutoScroll = true;
            this.controlDocumentoGisaAnexo1.Location = new System.Drawing.Point(6, 19);
            this.controlDocumentoGisaAnexo1.Name = "controlDocumentoGisaAnexo1";
            this.controlDocumentoGisaAnexo1.Size = new System.Drawing.Size(689, 538);
            this.controlDocumentoGisaAnexo1.TabIndex = 0;
            this.controlDocumentoGisaAnexo1.Visible = false;
            // 
            // controlDocumentoGisaProcesso1
            // 
            this.controlDocumentoGisaProcesso1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlDocumentoGisaProcesso1.AutoScroll = true;
            this.controlDocumentoGisaProcesso1.Location = new System.Drawing.Point(6, 19);
            this.controlDocumentoGisaProcesso1.Name = "controlDocumentoGisaProcesso1";
            this.controlDocumentoGisaProcesso1.Size = new System.Drawing.Size(689, 538);
            this.controlDocumentoGisaProcesso1.TabIndex = 0;
            this.controlDocumentoGisaProcesso1.Visible = false;
            // 
            // ControlDocInPorto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ControlDocInPorto";
            this.Size = new System.Drawing.Size(1116, 774);
            this.grpRelations.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerDetails.Panel1.ResumeLayout(false);
            this.splitContainerDetails.Panel2.ResumeLayout(false);
            this.splitContainerDetails.ResumeLayout(false);
            this.gbDocInPorto.ResumeLayout(false);
            this.grpGisa.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList ilDocTypes;
        private System.Windows.Forms.ImageList ilTSIcons;
        private System.Windows.Forms.GroupBox grpRelations;
        private DebuggedTreeView lvRelations;
        private System.Windows.Forms.ColumnHeader chDataArquivo;
        private System.Windows.Forms.ColumnHeader chIdentificador;
        private System.Windows.Forms.ColumnHeader chProcesso;
        private System.Windows.Forms.ColumnHeader chTitulo;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbGravar;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.ToolStripDropDownButton tsbMarcacoes;
        private System.Windows.Forms.ToolStripMenuItem marcarTodosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desmarcarTodosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inverterMarcaçãoToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader chCheck;
        private System.Windows.Forms.SplitContainer splitContainerDetails;
        private System.Windows.Forms.GroupBox gbDocInPorto;
        private ControlDocumentoExterno controlDocumentoExterno1;
        private ControlDocumentoExternoAnexo controlDocumentoExternoAnexo1;
        private ControlDocumentoExternoProcesso controlDocumentoExternoProcesso1;
        private System.Windows.Forms.GroupBox grpGisa;
        private ControlDocumentoGisa controlDocumentoGisa1;
        private ControlDocumentoGisaAnexo controlDocumentoGisaAnexo1;
        private ControlDocumentoGisaProcesso controlDocumentoGisaProcesso1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem marcarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desmarcarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem marcarTodosToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem desmarcarTodosToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem reverterOpçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
