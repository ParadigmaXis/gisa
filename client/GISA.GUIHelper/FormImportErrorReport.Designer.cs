namespace GISA.GUIHelper
{
    partial class FormImportErrorReport
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlErroVal = new System.Windows.Forms.Panel();
            this.lblErroVal = new System.Windows.Forms.Label();
            this.pnlErroLbl = new System.Windows.Forms.Panel();
            this.lblErro = new System.Windows.Forms.Label();
            this.pnlContextoLbl = new System.Windows.Forms.Panel();
            this.lblColuna = new System.Windows.Forms.Label();
            this.lblIdentificador = new System.Windows.Forms.Label();
            this.lblTabela = new System.Windows.Forms.Label();
            this.lblValor = new System.Windows.Forms.Label();
            this.pnlContextoVal = new System.Windows.Forms.Panel();
            this.lblColunaVal = new System.Windows.Forms.Label();
            this.lblIdentificadorVal = new System.Windows.Forms.Label();
            this.lblTabelaVal = new System.Windows.Forms.Label();
            this.lblValorVal = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlErroVal.SuspendLayout();
            this.pnlErroLbl.SuspendLayout();
            this.pnlContextoLbl.SuspendLayout();
            this.pnlContextoVal.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pnlErroVal, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlErroLbl, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlContextoLbl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlContextoVal, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(440, 0);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // pnlErroVal
            // 
            this.pnlErroVal.Controls.Add(this.lblErroVal);
            this.pnlErroVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlErroVal.Location = new System.Drawing.Point(73, 63);
            this.pnlErroVal.Name = "pnlErroVal";
            this.pnlErroVal.Size = new System.Drawing.Size(364, 1);
            this.pnlErroVal.TabIndex = 10;
            // 
            // lblErroVal
            // 
            this.lblErroVal.AutoSize = true;
            this.lblErroVal.Location = new System.Drawing.Point(3, 2);
            this.lblErroVal.Name = "lblErroVal";
            this.lblErroVal.Size = new System.Drawing.Size(29, 13);
            this.lblErroVal.TabIndex = 4;
            this.lblErroVal.Text = "Erro:";
            // 
            // pnlErroLbl
            // 
            this.pnlErroLbl.Controls.Add(this.lblErro);
            this.pnlErroLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlErroLbl.Location = new System.Drawing.Point(3, 63);
            this.pnlErroLbl.Name = "pnlErroLbl";
            this.pnlErroLbl.Size = new System.Drawing.Size(64, 1);
            this.pnlErroLbl.TabIndex = 9;
            // 
            // lblErro
            // 
            this.lblErro.AutoSize = true;
            this.lblErro.Location = new System.Drawing.Point(3, 2);
            this.lblErro.Name = "lblErro";
            this.lblErro.Size = new System.Drawing.Size(29, 13);
            this.lblErro.TabIndex = 3;
            this.lblErro.Text = "Erro:";
            // 
            // pnlContextoLbl
            // 
            this.pnlContextoLbl.Controls.Add(this.lblColuna);
            this.pnlContextoLbl.Controls.Add(this.lblIdentificador);
            this.pnlContextoLbl.Controls.Add(this.lblTabela);
            this.pnlContextoLbl.Controls.Add(this.lblValor);
            this.pnlContextoLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContextoLbl.Location = new System.Drawing.Point(3, 3);
            this.pnlContextoLbl.Name = "pnlContextoLbl";
            this.pnlContextoLbl.Size = new System.Drawing.Size(64, 54);
            this.pnlContextoLbl.TabIndex = 7;
            // 
            // lblColuna
            // 
            this.lblColuna.AutoSize = true;
            this.lblColuna.Location = new System.Drawing.Point(3, 30);
            this.lblColuna.Name = "lblColuna";
            this.lblColuna.Size = new System.Drawing.Size(43, 13);
            this.lblColuna.TabIndex = 2;
            this.lblColuna.Text = "Coluna:";
            // 
            // lblIdentificador
            // 
            this.lblIdentificador.AutoSize = true;
            this.lblIdentificador.Location = new System.Drawing.Point(3, 15);
            this.lblIdentificador.Name = "lblIdentificador";
            this.lblIdentificador.Size = new System.Drawing.Size(68, 13);
            this.lblIdentificador.TabIndex = 1;
            this.lblIdentificador.Text = "Identificador:";
            // 
            // lblTabela
            // 
            this.lblTabela.AutoSize = true;
            this.lblTabela.Location = new System.Drawing.Point(3, 0);
            this.lblTabela.Name = "lblTabela";
            this.lblTabela.Size = new System.Drawing.Size(43, 13);
            this.lblTabela.TabIndex = 0;
            this.lblTabela.Text = "Tabela:";
            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.Location = new System.Drawing.Point(3, 45);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(34, 13);
            this.lblValor.TabIndex = 2;
            this.lblValor.Text = "Valor:";
            // 
            // pnlContextoVal
            // 
            this.pnlContextoVal.Controls.Add(this.lblColunaVal);
            this.pnlContextoVal.Controls.Add(this.lblIdentificadorVal);
            this.pnlContextoVal.Controls.Add(this.lblTabelaVal);
            this.pnlContextoVal.Controls.Add(this.lblValorVal);
            this.pnlContextoVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContextoVal.Location = new System.Drawing.Point(73, 3);
            this.pnlContextoVal.Name = "pnlContextoVal";
            this.pnlContextoVal.Size = new System.Drawing.Size(364, 54);
            this.pnlContextoVal.TabIndex = 8;
            // 
            // lblColunaVal
            // 
            this.lblColunaVal.AutoSize = true;
            this.lblColunaVal.Location = new System.Drawing.Point(3, 30);
            this.lblColunaVal.Name = "lblColunaVal";
            this.lblColunaVal.Size = new System.Drawing.Size(43, 13);
            this.lblColunaVal.TabIndex = 3;
            this.lblColunaVal.Text = "Coluna:";
            // 
            // lblIdentificadorVal
            // 
            this.lblIdentificadorVal.AutoSize = true;
            this.lblIdentificadorVal.Location = new System.Drawing.Point(3, 15);
            this.lblIdentificadorVal.Name = "lblIdentificadorVal";
            this.lblIdentificadorVal.Size = new System.Drawing.Size(68, 13);
            this.lblIdentificadorVal.TabIndex = 3;
            this.lblIdentificadorVal.Text = "Identificador:";
            // 
            // lblTabelaVal
            // 
            this.lblTabelaVal.AutoSize = true;
            this.lblTabelaVal.Location = new System.Drawing.Point(3, 0);
            this.lblTabelaVal.Name = "lblTabelaVal";
            this.lblTabelaVal.Size = new System.Drawing.Size(43, 13);
            this.lblTabelaVal.TabIndex = 1;
            this.lblTabelaVal.Text = "Tabela:";
            // 
            // lblValorVal
            // 
            this.lblValorVal.AutoSize = true;
            this.lblValorVal.Location = new System.Drawing.Point(3, 45);
            this.lblValorVal.Name = "lblValorVal";
            this.lblValorVal.Size = new System.Drawing.Size(34, 13);
            this.lblValorVal.TabIndex = 3;
            this.lblValorVal.Text = "Valor:";
            //
            // Panel1
            //
            this.Panel1.Controls.Add(tableLayoutPanel1);
            //
            // btnDetails
            //
            this.btnDetails.Location = new System.Drawing.Point(8, 91);
            //
            // btnCancel
            //
            this.btnCancel.Location = new System.Drawing.Point(344, 91);
            // 
            // FormImportErrorReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 130);
            this.Name = "FormImportErrorReport";
            this.Text = "FormImportErrorReport";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlErroVal.ResumeLayout(false);
            this.pnlErroVal.PerformLayout();
            this.pnlErroLbl.ResumeLayout(false);
            this.pnlErroLbl.PerformLayout();
            this.pnlContextoLbl.ResumeLayout(false);
            this.pnlContextoLbl.PerformLayout();
            this.pnlContextoVal.ResumeLayout(false);
            this.pnlContextoVal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlContextoLbl;
        private System.Windows.Forms.Label lblIdentificador;
        private System.Windows.Forms.Label lblTabela;
        private System.Windows.Forms.Label lblColuna;
        private System.Windows.Forms.Panel pnlErroVal;
        private System.Windows.Forms.Panel pnlErroLbl;
        private System.Windows.Forms.Label lblErro;
        private System.Windows.Forms.Panel pnlContextoVal;
        private System.Windows.Forms.Label lblErroVal;
        private System.Windows.Forms.Label lblColunaVal;
        private System.Windows.Forms.Label lblIdentificadorVal;
        private System.Windows.Forms.Label lblTabelaVal;
        private System.Windows.Forms.Label lblValorVal;
        private System.Windows.Forms.Label lblValor;
    }
}