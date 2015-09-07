namespace GISA {
    partial class EntidadeList {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.txtFiltroCodigo = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.txtFiltroDesignacao = new System.Windows.Forms.TextBox();
            this.lblFiltroDesignacao = new System.Windows.Forms.Label();
            this.colCodigo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDesignacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colActivo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtOutrosDados = new System.Windows.Forms.TextBox();
            this.lblOutrosDados = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.txtDesignacao = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpResultados.SuspendLayout();
            this.grpFiltro.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpResultados
            // 
            this.grpResultados.Controls.Add(this.panel1);
            this.grpResultados.Size = new System.Drawing.Size(620, 380);
            this.grpResultados.Controls.SetChildIndex(this.btnAnterior, 0);
            this.grpResultados.Controls.SetChildIndex(this.btnProximo, 0);
            this.grpResultados.Controls.SetChildIndex(this.lstVwPaginated, 0);
            this.grpResultados.Controls.SetChildIndex(this.txtNroPagina, 0);
            this.grpResultados.Controls.SetChildIndex(this.panel1, 0);
            // 
            // txtNroPagina
            // 
            this.txtNroPagina.Location = new System.Drawing.Point(590, 63);
            // 
            // lstVwPaginated
            // 
            this.lstVwPaginated.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCodigo,
            this.colDesignacao,
            this.colActivo});
            this.lstVwPaginated.Location = new System.Drawing.Point(6, 19);
            this.lstVwPaginated.Size = new System.Drawing.Size(578, 220);
            // 
            // btnProximo
            // 
            this.btnProximo.Location = new System.Drawing.Point(590, 91);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Location = new System.Drawing.Point(590, 31);
            // 
            // grpFiltro
            // 
            this.grpFiltro.Controls.Add(this.txtFiltroCodigo);
            this.grpFiltro.Controls.Add(this.lblCodigo);
            this.grpFiltro.Controls.Add(this.chkActivo);
            this.grpFiltro.Controls.Add(this.txtFiltroDesignacao);
            this.grpFiltro.Controls.Add(this.lblFiltroDesignacao);
            this.grpFiltro.Size = new System.Drawing.Size(620, 59);
            this.grpFiltro.Controls.SetChildIndex(this.lblFiltroDesignacao, 0);
            this.grpFiltro.Controls.SetChildIndex(this.txtFiltroDesignacao, 0);
            this.grpFiltro.Controls.SetChildIndex(this.btnAplicar, 0);
            this.grpFiltro.Controls.SetChildIndex(this.chkActivo, 0);
            this.grpFiltro.Controls.SetChildIndex(this.lblCodigo, 0);
            this.grpFiltro.Controls.SetChildIndex(this.txtFiltroCodigo, 0);
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(550, 29);
            // 
            // txtFiltroCodigo
            // 
            this.txtFiltroCodigo.AcceptsReturn = true;
            this.txtFiltroCodigo.Location = new System.Drawing.Point(10, 32);
            this.txtFiltroCodigo.Name = "txtFiltroCodigo";
            this.txtFiltroCodigo.Size = new System.Drawing.Size(97, 20);
            this.txtFiltroCodigo.TabIndex = 7;
            // 
            // lblCodigo
            // 
            this.lblCodigo.Location = new System.Drawing.Point(13, 15);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(84, 16);
            this.lblCodigo.TabIndex = 6;
            this.lblCodigo.Text = "Código";
            // 
            // chkActivo
            // 
            this.chkActivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkActivo.Checked = true;
            this.chkActivo.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkActivo.Location = new System.Drawing.Point(494, 30);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(50, 24);
            this.chkActivo.TabIndex = 5;
            this.chkActivo.Text = "Ativo";
            this.chkActivo.ThreeState = true;
            // 
            // txtFiltroDesignacao
            // 
            this.txtFiltroDesignacao.AcceptsReturn = true;
            this.txtFiltroDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFiltroDesignacao.Location = new System.Drawing.Point(124, 32);
            this.txtFiltroDesignacao.Name = "txtFiltroDesignacao";
            this.txtFiltroDesignacao.Size = new System.Drawing.Size(358, 20);
            this.txtFiltroDesignacao.TabIndex = 1;
            // 
            // lblFiltroDesignacao
            // 
            this.lblFiltroDesignacao.Location = new System.Drawing.Point(125, 16);
            this.lblFiltroDesignacao.Name = "lblFiltroDesignacao";
            this.lblFiltroDesignacao.Size = new System.Drawing.Size(272, 16);
            this.lblFiltroDesignacao.TabIndex = 0;
            this.lblFiltroDesignacao.Text = "Nome";
            // 
            // colCodigo
            // 
            this.colCodigo.Text = "Código";
            this.colCodigo.Width = 80;
            // 
            // colDesignacao
            // 
            this.colDesignacao.Text = "Nome";
            this.colDesignacao.Width = 280;
            // 
            // colActivo
            // 
            this.colActivo.Text = "Ativo";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtOutrosDados);
            this.panel1.Controls.Add(this.lblOutrosDados);
            this.panel1.Controls.Add(this.chkActive);
            this.panel1.Controls.Add(this.txtDesignacao);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtCodigo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(3, 245);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(614, 132);
            this.panel1.TabIndex = 19;
            // 
            // txtOutrosDados
            // 
            this.txtOutrosDados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutrosDados.Location = new System.Drawing.Point(93, 57);
            this.txtOutrosDados.Multiline = true;
            this.txtOutrosDados.Name = "txtOutrosDados";
            this.txtOutrosDados.Size = new System.Drawing.Size(488, 72);
            this.txtOutrosDados.TabIndex = 30;
            // 
            // lblOutrosDados
            // 
            this.lblOutrosDados.Location = new System.Drawing.Point(14, 60);
            this.lblOutrosDados.Name = "lblOutrosDados";
            this.lblOutrosDados.Size = new System.Drawing.Size(84, 17);
            this.lblOutrosDados.TabIndex = 29;
            this.lblOutrosDados.Text = "Outros dados";
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(199, 7);
            this.chkActive.Name = "chkActive";
            this.chkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkActive.Size = new System.Drawing.Size(50, 17);
            this.chkActive.TabIndex = 26;
            this.chkActive.Text = "Ativo";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // txtDesignacao
            // 
            this.txtDesignacao.AcceptsReturn = true;
            this.txtDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDesignacao.Location = new System.Drawing.Point(93, 31);
            this.txtDesignacao.Name = "txtDesignacao";
            this.txtDesignacao.Size = new System.Drawing.Size(488, 20);
            this.txtDesignacao.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 28;
            this.label1.Text = "Código";
            // 
            // txtCodigo
            // 
            this.txtCodigo.AcceptsReturn = true;
            this.txtCodigo.Location = new System.Drawing.Point(93, 5);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(91, 20);
            this.txtCodigo.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 17);
            this.label2.TabIndex = 27;
            this.label2.Text = "Nome";
            // 
            // EntidadeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.FilterVisible = true;
            this.Name = "EntidadeList";
            this.Size = new System.Drawing.Size(632, 451);
            this.grpResultados.ResumeLayout(false);
            this.grpFiltro.ResumeLayout(false);
            this.grpFiltro.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TextBox txtFiltroDesignacao;
        public System.Windows.Forms.Label lblFiltroDesignacao;
        public System.Windows.Forms.CheckBox chkActivo;
        public System.Windows.Forms.TextBox txtFiltroCodigo;
        public System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.ColumnHeader colCodigo;
        public System.Windows.Forms.ColumnHeader colDesignacao;
        private System.Windows.Forms.ColumnHeader colActivo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtOutrosDados;
        public System.Windows.Forms.Label lblOutrosDados;
        private System.Windows.Forms.CheckBox chkActive;
        public System.Windows.Forms.TextBox txtDesignacao;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtCodigo;
        public System.Windows.Forms.Label label2;
    }
}
