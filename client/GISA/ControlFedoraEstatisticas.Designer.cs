namespace GISA
{
    partial class ControlFedoraEstatisticas
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
            this.grpFedFiltro = new System.Windows.Forms.GroupBox();
            this.grpFedTotais = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUIsCorrespondentesE = new System.Windows.Forms.TextBox();
            this.txtUIsCorrespondentesC = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTotalEditados = new System.Windows.Forms.TextBox();
            this.txtTotalCriados = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.grpFedResultados = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.ch_username = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_criados = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_uisCorrespondentesCriados = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_editados = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_uisCorrespondentesEditados = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAplicar = new System.Windows.Forms.Button();
            this.cdbDataInicio = new GISA.Controls.PxCompleteDateBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cdbDataFim = new GISA.Controls.PxCompleteDateBox();
            this.grpFedFiltro.SuspendLayout();
            this.grpFedTotais.SuspendLayout();
            this.grpFedResultados.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpFedFiltro
            // 
            this.grpFedFiltro.Controls.Add(this.grpFedTotais);
            this.grpFedFiltro.Controls.Add(this.grpFedResultados);
            this.grpFedFiltro.Controls.Add(this.btnAplicar);
            this.grpFedFiltro.Controls.Add(this.cdbDataInicio);
            this.grpFedFiltro.Controls.Add(this.label1);
            this.grpFedFiltro.Controls.Add(this.label2);
            this.grpFedFiltro.Controls.Add(this.cdbDataFim);
            this.grpFedFiltro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpFedFiltro.Location = new System.Drawing.Point(0, 0);
            this.grpFedFiltro.Name = "grpFedFiltro";
            this.grpFedFiltro.Size = new System.Drawing.Size(689, 513);
            this.grpFedFiltro.TabIndex = 59;
            this.grpFedFiltro.TabStop = false;
            this.grpFedFiltro.Text = "Filtro";
            // 
            // grpFedTotais
            // 
            this.grpFedTotais.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFedTotais.Controls.Add(this.label4);
            this.grpFedTotais.Controls.Add(this.txtUIsCorrespondentesE);
            this.grpFedTotais.Controls.Add(this.txtUIsCorrespondentesC);
            this.grpFedTotais.Controls.Add(this.label3);
            this.grpFedTotais.Controls.Add(this.txtTotalEditados);
            this.grpFedTotais.Controls.Add(this.txtTotalCriados);
            this.grpFedTotais.Controls.Add(this.label7);
            this.grpFedTotais.Controls.Add(this.label6);
            this.grpFedTotais.Location = new System.Drawing.Point(6, 411);
            this.grpFedTotais.Name = "grpFedTotais";
            this.grpFedTotais.Size = new System.Drawing.Size(677, 96);
            this.grpFedTotais.TabIndex = 61;
            this.grpFedTotais.TabStop = false;
            this.grpFedTotais.Text = "Totais";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(68, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Objetos Digitais";
            // 
            // txtUIsCorrespondentesE
            // 
            this.txtUIsCorrespondentesE.Location = new System.Drawing.Point(177, 64);
            this.txtUIsCorrespondentesE.Name = "txtUIsCorrespondentesE";
            this.txtUIsCorrespondentesE.ReadOnly = true;
            this.txtUIsCorrespondentesE.Size = new System.Drawing.Size(100, 20);
            this.txtUIsCorrespondentesE.TabIndex = 7;
            // 
            // txtUIsCorrespondentesC
            // 
            this.txtUIsCorrespondentesC.Location = new System.Drawing.Point(177, 38);
            this.txtUIsCorrespondentesC.Name = "txtUIsCorrespondentesC";
            this.txtUIsCorrespondentesC.ReadOnly = true;
            this.txtUIsCorrespondentesC.Size = new System.Drawing.Size(100, 20);
            this.txtUIsCorrespondentesC.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(174, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "UIs Correspondentes";
            // 
            // txtTotalEditados
            // 
            this.txtTotalEditados.Location = new System.Drawing.Point(71, 64);
            this.txtTotalEditados.Name = "txtTotalEditados";
            this.txtTotalEditados.ReadOnly = true;
            this.txtTotalEditados.Size = new System.Drawing.Size(100, 20);
            this.txtTotalEditados.TabIndex = 3;
            // 
            // txtTotalCriados
            // 
            this.txtTotalCriados.Location = new System.Drawing.Point(71, 38);
            this.txtTotalCriados.Name = "txtTotalCriados";
            this.txtTotalCriados.ReadOnly = true;
            this.txtTotalCriados.Size = new System.Drawing.Size(100, 20);
            this.txtTotalCriados.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Editados:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Criados:";
            // 
            // grpFedResultados
            // 
            this.grpFedResultados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFedResultados.Controls.Add(this.listView1);
            this.grpFedResultados.Location = new System.Drawing.Point(6, 47);
            this.grpFedResultados.Name = "grpFedResultados";
            this.grpFedResultados.Size = new System.Drawing.Size(677, 358);
            this.grpFedResultados.TabIndex = 60;
            this.grpFedResultados.TabStop = false;
            this.grpFedResultados.Text = "Resultados";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_username,
            this.ch_criados,
            this.ch_uisCorrespondentesCriados,
            this.ch_editados,
            this.ch_uisCorrespondentesEditados});
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(6, 19);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(665, 333);
            this.listView1.TabIndex = 30;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // ch_username
            // 
            this.ch_username.Text = "Utilizador";
            this.ch_username.Width = 200;
            // 
            // ch_criados
            // 
            this.ch_criados.Text = "ODs Criados";
            this.ch_criados.Width = 100;
            // 
            // ch_uisCorrespondentesCriados
            // 
            this.ch_uisCorrespondentesCriados.Text = "UIs com ODs novos";
            this.ch_uisCorrespondentesCriados.Width = 120;
            // 
            // ch_editados
            // 
            this.ch_editados.Text = "ODs Editados";
            this.ch_editados.Width = 100;
            // 
            // ch_uisCorrespondentesEditados
            // 
            this.ch_uisCorrespondentesEditados.Text = "UIs com ODs editados";
            this.ch_uisCorrespondentesEditados.Width = 120;
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(487, 17);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(75, 23);
            this.btnAplicar.TabIndex = 15;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = true;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // cdbDataInicio
            // 
            this.cdbDataInicio.Checked = false;
            this.cdbDataInicio.Day = 1;
            this.cdbDataInicio.Location = new System.Drawing.Point(55, 19);
            this.cdbDataInicio.Month = 1;
            this.cdbDataInicio.Name = "cdbDataInicio";
            this.cdbDataInicio.Size = new System.Drawing.Size(167, 22);
            this.cdbDataInicio.TabIndex = 7;
            this.cdbDataInicio.Year = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 24);
            this.label1.TabIndex = 12;
            this.label1.Text = "entre";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(252, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 24);
            this.label2.TabIndex = 14;
            this.label2.Text = "e";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cdbDataFim
            // 
            this.cdbDataFim.Checked = false;
            this.cdbDataFim.Day = 1;
            this.cdbDataFim.Location = new System.Drawing.Point(274, 19);
            this.cdbDataFim.Month = 1;
            this.cdbDataFim.Name = "cdbDataFim";
            this.cdbDataFim.Size = new System.Drawing.Size(167, 22);
            this.cdbDataFim.TabIndex = 8;
            this.cdbDataFim.Year = 1;
            // 
            // ControlFedoraEstatisticas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpFedFiltro);
            this.Name = "ControlFedoraEstatisticas";
            this.Size = new System.Drawing.Size(689, 513);
            this.grpFedFiltro.ResumeLayout(false);
            this.grpFedTotais.ResumeLayout(false);
            this.grpFedTotais.PerformLayout();
            this.grpFedResultados.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFedFiltro;
        private System.Windows.Forms.GroupBox grpFedTotais;
        private System.Windows.Forms.TextBox txtTotalEditados;
        private System.Windows.Forms.TextBox txtTotalCriados;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox grpFedResultados;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader ch_username;
        private System.Windows.Forms.ColumnHeader ch_criados;
        private System.Windows.Forms.ColumnHeader ch_editados;
        private System.Windows.Forms.Button btnAplicar;
        internal Controls.PxCompleteDateBox cdbDataInicio;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        internal Controls.PxCompleteDateBox cdbDataFim;
        private System.Windows.Forms.TextBox txtUIsCorrespondentesE;
        private System.Windows.Forms.TextBox txtUIsCorrespondentesC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader ch_uisCorrespondentesCriados;
        private System.Windows.Forms.ColumnHeader ch_uisCorrespondentesEditados;
        private System.Windows.Forms.Label label4;
    }
}
