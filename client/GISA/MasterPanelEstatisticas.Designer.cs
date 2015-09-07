namespace GISA
{
    partial class MasterPanelEstatisticas
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
            this.toolBarCopyToClipboard = new System.Windows.Forms.ToolBarButton();
            this.GrpBx_Periodo = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chkExclImport = new System.Windows.Forms.CheckBox();
            this.cdbDataInicio = new GISA.Controls.PxCompleteDateBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gBox_UnidadesFisicas = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel_UnidadesFisicas = new System.Windows.Forms.TableLayoutPanel();
            this.gBox_RegistosAutoridade = new System.Windows.Forms.GroupBox();
            this.TableLayoutPanel_RegAutPeriodo = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox_Noticia_Autoridade = new System.Windows.Forms.ComboBox();
            this.gBox_UnidadesInformacionais = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel_UAsPeriodo = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox_UnidadesInformacionais = new System.Windows.Forms.ComboBox();
            this.lblDataProducaoInicio = new System.Windows.Forms.Label();
            this.lblDataProducaoFim = new System.Windows.Forms.Label();
            this.cdbDataFim = new GISA.Controls.PxCompleteDateBox();
            this.gBox_Totais = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gBox_Tot_UnidadesInformacionais = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.gBox_Tot_RegistosAutotidade = new System.Windows.Forms.GroupBox();
            this.gBox_Tot_UnidadesFisicas = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabGisa = new System.Windows.Forms.TabPage();
            this.tabFedora = new System.Windows.Forms.TabPage();
            this.controlFedoraEstatisticas1 = new GISA.ControlFedoraEstatisticas();
            this.pnlToolbarPadding.SuspendLayout();
            this.GrpBx_Periodo.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gBox_UnidadesFisicas.SuspendLayout();
            this.gBox_RegistosAutoridade.SuspendLayout();
            this.TableLayoutPanel_RegAutPeriodo.SuspendLayout();
            this.gBox_UnidadesInformacionais.SuspendLayout();
            this.tableLayoutPanel_UAsPeriodo.SuspendLayout();
            this.gBox_Totais.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabGisa.SuspendLayout();
            this.tabFedora.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFuncao
            // 
            this.lblFuncao.Location = new System.Drawing.Point(0, 0);
            this.lblFuncao.Size = new System.Drawing.Size(771, 24);
            // 
            // ToolBar
            // 
            this.ToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarCopyToClipboard});
            this.ToolBar.Size = new System.Drawing.Size(12334, 26);
            this.ToolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.ToolBar_ButtonClick);
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
            this.pnlToolbarPadding.Size = new System.Drawing.Size(771, 28);
            // 
            // toolBarCopyToClipboard
            // 
            this.toolBarCopyToClipboard.Name = "toolBarCopyToClipboard";
            this.toolBarCopyToClipboard.ToolTipText = "Copiar";
            // 
            // GrpBx_Periodo
            // 
            this.GrpBx_Periodo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpBx_Periodo.Controls.Add(this.button1);
            this.GrpBx_Periodo.Controls.Add(this.chkExclImport);
            this.GrpBx_Periodo.Controls.Add(this.cdbDataInicio);
            this.GrpBx_Periodo.Controls.Add(this.tableLayoutPanel1);
            this.GrpBx_Periodo.Controls.Add(this.lblDataProducaoInicio);
            this.GrpBx_Periodo.Controls.Add(this.lblDataProducaoFim);
            this.GrpBx_Periodo.Controls.Add(this.cdbDataFim);
            this.GrpBx_Periodo.Location = new System.Drawing.Point(3, 3);
            this.GrpBx_Periodo.Name = "GrpBx_Periodo";
            this.GrpBx_Periodo.Size = new System.Drawing.Size(754, 177);
            this.GrpBx_Periodo.TabIndex = 57;
            this.GrpBx_Periodo.TabStop = false;
            this.GrpBx_Periodo.Text = "Filtro";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(569, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Aplicar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // chkExclImport
            // 
            this.chkExclImport.AutoSize = true;
            this.chkExclImport.Checked = true;
            this.chkExclImport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExclImport.Location = new System.Drawing.Point(440, 21);
            this.chkExclImport.Name = "chkExclImport";
            this.chkExclImport.Size = new System.Drawing.Size(113, 17);
            this.chkExclImport.TabIndex = 16;
            this.chkExclImport.Text = "Excluir Importação";
            this.chkExclImport.UseVisualStyleBackColor = true;
            // 
            // cdbDataInicio
            // 
            this.cdbDataInicio.Checked = false;
            this.cdbDataInicio.Day = 1;
            this.cdbDataInicio.Location = new System.Drawing.Point(44, 19);
            this.cdbDataInicio.Month = 1;
            this.cdbDataInicio.Name = "cdbDataInicio";
            this.cdbDataInicio.Size = new System.Drawing.Size(167, 22);
            this.cdbDataInicio.TabIndex = 7;
            this.cdbDataInicio.Year = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.gBox_UnidadesFisicas, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gBox_RegistosAutoridade, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.gBox_UnidadesInformacionais, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(11, 47);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(737, 127);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // gBox_UnidadesFisicas
            // 
            this.gBox_UnidadesFisicas.Controls.Add(this.tableLayoutPanel_UnidadesFisicas);
            this.gBox_UnidadesFisicas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gBox_UnidadesFisicas.Location = new System.Drawing.Point(3, 3);
            this.gBox_UnidadesFisicas.Name = "gBox_UnidadesFisicas";
            this.gBox_UnidadesFisicas.Size = new System.Drawing.Size(239, 121);
            this.gBox_UnidadesFisicas.TabIndex = 0;
            this.gBox_UnidadesFisicas.TabStop = false;
            this.gBox_UnidadesFisicas.Text = "Unidades Físicas";
            // 
            // tableLayoutPanel_UnidadesFisicas
            // 
            this.tableLayoutPanel_UnidadesFisicas.ColumnCount = 1;
            this.tableLayoutPanel_UnidadesFisicas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_UnidadesFisicas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_UnidadesFisicas.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel_UnidadesFisicas.Name = "tableLayoutPanel_UnidadesFisicas";
            this.tableLayoutPanel_UnidadesFisicas.RowCount = 2;
            this.tableLayoutPanel_UnidadesFisicas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel_UnidadesFisicas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_UnidadesFisicas.Size = new System.Drawing.Size(233, 102);
            this.tableLayoutPanel_UnidadesFisicas.TabIndex = 0;
            // 
            // gBox_RegistosAutoridade
            // 
            this.gBox_RegistosAutoridade.Controls.Add(this.TableLayoutPanel_RegAutPeriodo);
            this.gBox_RegistosAutoridade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gBox_RegistosAutoridade.Location = new System.Drawing.Point(248, 3);
            this.gBox_RegistosAutoridade.Name = "gBox_RegistosAutoridade";
            this.gBox_RegistosAutoridade.Size = new System.Drawing.Size(239, 121);
            this.gBox_RegistosAutoridade.TabIndex = 1;
            this.gBox_RegistosAutoridade.TabStop = false;
            this.gBox_RegistosAutoridade.Text = "Registos de Autoridade";
            // 
            // TableLayoutPanel_RegAutPeriodo
            // 
            this.TableLayoutPanel_RegAutPeriodo.ColumnCount = 1;
            this.TableLayoutPanel_RegAutPeriodo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel_RegAutPeriodo.Controls.Add(this.comboBox_Noticia_Autoridade, 0, 0);
            this.TableLayoutPanel_RegAutPeriodo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanel_RegAutPeriodo.Location = new System.Drawing.Point(3, 16);
            this.TableLayoutPanel_RegAutPeriodo.Name = "TableLayoutPanel_RegAutPeriodo";
            this.TableLayoutPanel_RegAutPeriodo.RowCount = 2;
            this.TableLayoutPanel_RegAutPeriodo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.TableLayoutPanel_RegAutPeriodo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel_RegAutPeriodo.Size = new System.Drawing.Size(233, 102);
            this.TableLayoutPanel_RegAutPeriodo.TabIndex = 0;
            // 
            // comboBox_Noticia_Autoridade
            // 
            this.comboBox_Noticia_Autoridade.Dock = System.Windows.Forms.DockStyle.Right;
            this.comboBox_Noticia_Autoridade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Noticia_Autoridade.DropDownWidth = 231;
            this.comboBox_Noticia_Autoridade.FormattingEnabled = true;
            this.comboBox_Noticia_Autoridade.Location = new System.Drawing.Point(3, 3);
            this.comboBox_Noticia_Autoridade.Name = "comboBox_Noticia_Autoridade";
            this.comboBox_Noticia_Autoridade.Size = new System.Drawing.Size(227, 21);
            this.comboBox_Noticia_Autoridade.TabIndex = 0;
            this.comboBox_Noticia_Autoridade.SelectedIndexChanged += new System.EventHandler(this.comboBox_Noticia_Autoridade_SelectedIndexChanged);
            // 
            // gBox_UnidadesInformacionais
            // 
            this.gBox_UnidadesInformacionais.Controls.Add(this.tableLayoutPanel_UAsPeriodo);
            this.gBox_UnidadesInformacionais.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gBox_UnidadesInformacionais.Location = new System.Drawing.Point(493, 3);
            this.gBox_UnidadesInformacionais.Name = "gBox_UnidadesInformacionais";
            this.gBox_UnidadesInformacionais.Size = new System.Drawing.Size(241, 121);
            this.gBox_UnidadesInformacionais.TabIndex = 2;
            this.gBox_UnidadesInformacionais.TabStop = false;
            this.gBox_UnidadesInformacionais.Text = "Unidades Informacionais";
            // 
            // tableLayoutPanel_UAsPeriodo
            // 
            this.tableLayoutPanel_UAsPeriodo.ColumnCount = 1;
            this.tableLayoutPanel_UAsPeriodo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_UAsPeriodo.Controls.Add(this.comboBox_UnidadesInformacionais, 0, 0);
            this.tableLayoutPanel_UAsPeriodo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_UAsPeriodo.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel_UAsPeriodo.Name = "tableLayoutPanel_UAsPeriodo";
            this.tableLayoutPanel_UAsPeriodo.RowCount = 2;
            this.tableLayoutPanel_UAsPeriodo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel_UAsPeriodo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_UAsPeriodo.Size = new System.Drawing.Size(235, 102);
            this.tableLayoutPanel_UAsPeriodo.TabIndex = 0;
            // 
            // comboBox_UnidadesInformacionais
            // 
            this.comboBox_UnidadesInformacionais.Dock = System.Windows.Forms.DockStyle.Right;
            this.comboBox_UnidadesInformacionais.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_UnidadesInformacionais.FormattingEnabled = true;
            this.comboBox_UnidadesInformacionais.Location = new System.Drawing.Point(3, 3);
            this.comboBox_UnidadesInformacionais.Name = "comboBox_UnidadesInformacionais";
            this.comboBox_UnidadesInformacionais.Size = new System.Drawing.Size(229, 21);
            this.comboBox_UnidadesInformacionais.TabIndex = 0;
            this.comboBox_UnidadesInformacionais.SelectedIndexChanged += new System.EventHandler(this.comboBox_UnidadesInformacionais_SelectedIndexChanged);
            // 
            // lblDataProducaoInicio
            // 
            this.lblDataProducaoInicio.Location = new System.Drawing.Point(8, 16);
            this.lblDataProducaoInicio.Name = "lblDataProducaoInicio";
            this.lblDataProducaoInicio.Size = new System.Drawing.Size(43, 24);
            this.lblDataProducaoInicio.TabIndex = 12;
            this.lblDataProducaoInicio.Text = "entre";
            this.lblDataProducaoInicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDataProducaoFim
            // 
            this.lblDataProducaoFim.Location = new System.Drawing.Point(232, 16);
            this.lblDataProducaoFim.Name = "lblDataProducaoFim";
            this.lblDataProducaoFim.Size = new System.Drawing.Size(16, 24);
            this.lblDataProducaoFim.TabIndex = 14;
            this.lblDataProducaoFim.Text = "e";
            this.lblDataProducaoFim.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cdbDataFim
            // 
            this.cdbDataFim.Checked = false;
            this.cdbDataFim.Day = 1;
            this.cdbDataFim.Location = new System.Drawing.Point(254, 19);
            this.cdbDataFim.Month = 1;
            this.cdbDataFim.Name = "cdbDataFim";
            this.cdbDataFim.Size = new System.Drawing.Size(167, 22);
            this.cdbDataFim.TabIndex = 8;
            this.cdbDataFim.Year = 1;
            // 
            // gBox_Totais
            // 
            this.gBox_Totais.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gBox_Totais.Controls.Add(this.tableLayoutPanel2);
            this.gBox_Totais.Location = new System.Drawing.Point(3, 183);
            this.gBox_Totais.Name = "gBox_Totais";
            this.gBox_Totais.Size = new System.Drawing.Size(754, 291);
            this.gBox_Totais.TabIndex = 0;
            this.gBox_Totais.TabStop = false;
            this.gBox_Totais.Text = "Totais";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.gBox_Tot_UnidadesInformacionais, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(748, 272);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // gBox_Tot_UnidadesInformacionais
            // 
            this.gBox_Tot_UnidadesInformacionais.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gBox_Tot_UnidadesInformacionais.Location = new System.Drawing.Point(377, 3);
            this.gBox_Tot_UnidadesInformacionais.Name = "gBox_Tot_UnidadesInformacionais";
            this.gBox_Tot_UnidadesInformacionais.Size = new System.Drawing.Size(368, 266);
            this.gBox_Tot_UnidadesInformacionais.TabIndex = 0;
            this.gBox_Tot_UnidadesInformacionais.TabStop = false;
            this.gBox_Tot_UnidadesInformacionais.Text = "Unidades Informacionais";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.gBox_Tot_RegistosAutotidade, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.gBox_Tot_UnidadesFisicas, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(368, 266);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // gBox_Tot_RegistosAutotidade
            // 
            this.gBox_Tot_RegistosAutotidade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gBox_Tot_RegistosAutotidade.Location = new System.Drawing.Point(3, 136);
            this.gBox_Tot_RegistosAutotidade.Name = "gBox_Tot_RegistosAutotidade";
            this.gBox_Tot_RegistosAutotidade.Size = new System.Drawing.Size(362, 127);
            this.gBox_Tot_RegistosAutotidade.TabIndex = 1;
            this.gBox_Tot_RegistosAutotidade.TabStop = false;
            this.gBox_Tot_RegistosAutotidade.Text = "Registos de Autoridade";
            // 
            // gBox_Tot_UnidadesFisicas
            // 
            this.gBox_Tot_UnidadesFisicas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gBox_Tot_UnidadesFisicas.Location = new System.Drawing.Point(3, 3);
            this.gBox_Tot_UnidadesFisicas.Name = "gBox_Tot_UnidadesFisicas";
            this.gBox_Tot_UnidadesFisicas.Size = new System.Drawing.Size(362, 127);
            this.gBox_Tot_UnidadesFisicas.TabIndex = 0;
            this.gBox_Tot_UnidadesFisicas.TabStop = false;
            this.gBox_Tot_UnidadesFisicas.Text = "Unidades Físicas / Objetos Digitais";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabGisa);
            this.tabControl1.Controls.Add(this.tabFedora);
            this.tabControl1.Location = new System.Drawing.Point(0, 57);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(771, 503);
            this.tabControl1.TabIndex = 17;
            // 
            // tabGisa
            // 
            this.tabGisa.Controls.Add(this.GrpBx_Periodo);
            this.tabGisa.Controls.Add(this.gBox_Totais);
            this.tabGisa.Location = new System.Drawing.Point(4, 22);
            this.tabGisa.Name = "tabGisa";
            this.tabGisa.Padding = new System.Windows.Forms.Padding(3);
            this.tabGisa.Size = new System.Drawing.Size(763, 477);
            this.tabGisa.TabIndex = 0;
            this.tabGisa.Text = "Gisa";
            this.tabGisa.UseVisualStyleBackColor = true;
            // 
            // tabFedora
            // 
            this.tabFedora.Controls.Add(this.controlFedoraEstatisticas1);
            this.tabFedora.Location = new System.Drawing.Point(4, 22);
            this.tabFedora.Name = "tabFedora";
            this.tabFedora.Padding = new System.Windows.Forms.Padding(3);
            this.tabFedora.Size = new System.Drawing.Size(763, 477);
            this.tabFedora.TabIndex = 1;
            this.tabFedora.Text = "Fedora";
            this.tabFedora.UseVisualStyleBackColor = true;
            // 
            // controlFedoraEstatisticas1
            // 
            this.controlFedoraEstatisticas1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.controlFedoraEstatisticas1.Location = new System.Drawing.Point(6, 6);
            this.controlFedoraEstatisticas1.Name = "controlFedoraEstatisticas1";
            this.controlFedoraEstatisticas1.Size = new System.Drawing.Size(751, 465);
            this.controlFedoraEstatisticas1.TabIndex = 0;
            // 
            // MasterPanelEstatisticas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "MasterPanelEstatisticas";
            this.Size = new System.Drawing.Size(771, 560);
            this.Controls.SetChildIndex(this.lblFuncao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.GrpBx_Periodo.ResumeLayout(false);
            this.GrpBx_Periodo.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.gBox_UnidadesFisicas.ResumeLayout(false);
            this.gBox_RegistosAutoridade.ResumeLayout(false);
            this.TableLayoutPanel_RegAutPeriodo.ResumeLayout(false);
            this.gBox_UnidadesInformacionais.ResumeLayout(false);
            this.tableLayoutPanel_UAsPeriodo.ResumeLayout(false);
            this.gBox_Totais.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabGisa.ResumeLayout(false);
            this.tabFedora.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolBarButton toolBarCopyToClipboard;
        private System.Windows.Forms.GroupBox GrpBx_Periodo;
        internal GISA.Controls.PxCompleteDateBox cdbDataInicio;
        internal System.Windows.Forms.Label lblDataProducaoInicio;
        internal System.Windows.Forms.Label lblDataProducaoFim;
        internal GISA.Controls.PxCompleteDateBox cdbDataFim;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox gBox_Totais;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox gBox_UnidadesFisicas;
        private System.Windows.Forms.GroupBox gBox_RegistosAutoridade;
        private System.Windows.Forms.GroupBox gBox_UnidadesInformacionais;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox gBox_Tot_UnidadesInformacionais;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox gBox_Tot_UnidadesFisicas;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_UAsPeriodo;
        private System.Windows.Forms.ComboBox comboBox_UnidadesInformacionais;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanel_RegAutPeriodo;
        private System.Windows.Forms.ComboBox comboBox_Noticia_Autoridade;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_UnidadesFisicas;
        private System.Windows.Forms.CheckBox chkExclImport;
        private System.Windows.Forms.GroupBox gBox_Tot_RegistosAutotidade;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabGisa;
        private System.Windows.Forms.TabPage tabFedora;
        private ControlFedoraEstatisticas controlFedoraEstatisticas1;
    }
}
