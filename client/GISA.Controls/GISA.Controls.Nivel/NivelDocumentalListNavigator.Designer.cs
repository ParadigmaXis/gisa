namespace GISA.Controls.Nivel
{
    partial class NivelDocumentalListNavigator
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
            this.grpBreadCrumbsPath = new System.Windows.Forms.GroupBox();
            this.BreadCrumbsPath1 = new GISA.Controls.BreadCrumbsPath();
            this.colRequisitado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAgrupador = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkHideIndirected = new System.Windows.Forms.CheckBox();
            this.grpResultados.SuspendLayout();
            this.grpFiltro.SuspendLayout();
            this.grpBreadCrumbsPath.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpResultados
            // 
            this.grpResultados.Location = new System.Drawing.Point(6, 110);
            this.grpResultados.Size = new System.Drawing.Size(815, 268);
            // 
            // txtNroPagina
            // 
            this.txtNroPagina.Location = new System.Drawing.Point(785, 64);
            // 
            // lstVwPaginated
            // 
            this.lstVwPaginated.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colRequisitado,
            this.colAgrupador});
            this.lstVwPaginated.Size = new System.Drawing.Size(771, 246);
            // 
            // btnProximo
            // 
            this.btnProximo.Location = new System.Drawing.Point(785, 90);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Location = new System.Drawing.Point(785, 34);
            // 
            // grpFiltro
            // 
            this.grpFiltro.Controls.Add(this.chkHideIndirected);
            this.grpFiltro.Location = new System.Drawing.Point(6, 41);
            this.grpFiltro.Size = new System.Drawing.Size(815, 69);
            this.grpFiltro.Controls.SetChildIndex(this.chkHideIndirected, 0);
            this.grpFiltro.Controls.SetChildIndex(this.btnAplicar, 0);
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(745, 35);
            // 
            // grpBreadCrumbsPath
            // 
            this.grpBreadCrumbsPath.BackColor = System.Drawing.SystemColors.Control;
            this.grpBreadCrumbsPath.Controls.Add(this.BreadCrumbsPath1);
            this.grpBreadCrumbsPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpBreadCrumbsPath.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpBreadCrumbsPath.Location = new System.Drawing.Point(6, 0);
            this.grpBreadCrumbsPath.Name = "grpBreadCrumbsPath";
            this.grpBreadCrumbsPath.Size = new System.Drawing.Size(815, 41);
            this.grpBreadCrumbsPath.TabIndex = 1;
            this.grpBreadCrumbsPath.TabStop = false;
            // 
            // BreadCrumbsPath1
            // 
            this.BreadCrumbsPath1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BreadCrumbsPath1.BackColor = System.Drawing.Color.White;
            this.BreadCrumbsPath1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BreadCrumbsPath1.Location = new System.Drawing.Point(2, 7);
            this.BreadCrumbsPath1.Name = "BreadCrumbsPath1";
            this.BreadCrumbsPath1.Size = new System.Drawing.Size(811, 30);
            this.BreadCrumbsPath1.TabIndex = 1;
            // 
            // colRequisitado
            // 
            this.colRequisitado.Text = "Requisitado";
            this.colRequisitado.Width = 80;
            // 
            // colAgrupador
            // 
            this.colAgrupador.Text = "Agrupador";
            this.colAgrupador.Width = 300;
            // 
            // chkHideIndirected
            // 
            this.chkHideIndirected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkHideIndirected.Checked = true;
            this.chkHideIndirected.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHideIndirected.Location = new System.Drawing.Point(638, 18);
            this.chkHideIndirected.Name = "chkHideIndirected";
            this.chkHideIndirected.Size = new System.Drawing.Size(101, 45);
            this.chkHideIndirected.TabIndex = 10;
            this.chkHideIndirected.Text = "Esconder niveis não diretos";
            this.chkHideIndirected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkHideIndirected.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.Size = new System.Drawing.Size(625, 45);
            // 
            // NivelDocumentalListNavigator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpBreadCrumbsPath);
            this.Name = "NivelDocumentalListNavigator";
            this.Size = new System.Drawing.Size(827, 384);
            this.Controls.SetChildIndex(this.grpBreadCrumbsPath, 0);
            this.Controls.SetChildIndex(this.grpFiltro, 0);
            this.Controls.SetChildIndex(this.grpResultados, 0);
            this.grpResultados.ResumeLayout(false);
            this.grpFiltro.ResumeLayout(false);
            this.grpBreadCrumbsPath.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public GISA.Controls.BreadCrumbsPath BreadCrumbsPath1;
        internal System.Windows.Forms.GroupBox grpBreadCrumbsPath;
        private System.Windows.Forms.ColumnHeader colRequisitado;
        private System.Windows.Forms.ColumnHeader colAgrupador;
        private System.Windows.Forms.CheckBox chkHideIndirected;

    }
}
