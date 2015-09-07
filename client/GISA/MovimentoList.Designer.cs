namespace GISA
{
    partial class MovimentoList
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
            this.columnHeaderID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderEntidade = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCodigo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblFiltroNroMovimento = new System.Windows.Forms.Label();
            this.txtFiltroCodigo = new System.Windows.Forms.TextBox();
            this.lblFiltroEntidade = new System.Windows.Forms.Label();
            this.txtFiltroNroMovimento = new GISA.Controls.PxIntegerBox();
            this.lblFiltroData = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFiltroEntidade = new System.Windows.Forms.TextBox();
            this.grpResultados.SuspendLayout();
            this.grpFiltro.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstVwMovimentos
            // 
            this.lstVwPaginated.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderID,
            this.columnHeaderData,
            this.columnHeaderEntidade,
            this.columnHeaderCodigo});            
            // 
            // columnHeaderID
            // 
            this.columnHeaderID.Text = "Nº do movimento";
            this.columnHeaderID.Width = 100;
            // 
            // columnHeaderData
            // 
            this.columnHeaderData.Text = "Data";
            this.columnHeaderData.Width = 150;
            // 
            // columnHeaderEntidade
            // 
            this.columnHeaderEntidade.DisplayIndex = 3;
            this.columnHeaderEntidade.Text = "Entidade";
            this.columnHeaderEntidade.Width = 230;
            // 
            // columnHeaderCodigo
            // 
            this.columnHeaderCodigo.DisplayIndex = 2;
            this.columnHeaderCodigo.Text = "Código";
            this.columnHeaderCodigo.Width = 70;
            // 
            // grpFiltro
            // 
            this.grpFiltro.Controls.Add(this.tableLayoutPanel1);
            this.grpFiltro.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFiltro.Location = new System.Drawing.Point(0, 0);
            this.grpFiltro.Name = "grpFiltro";
            this.grpFiltro.Size = new System.Drawing.Size(620, 64);
            this.grpFiltro.TabIndex = 3;
            this.grpFiltro.TabStop = false;
            this.grpFiltro.Text = "Filtro";
            this.grpFiltro.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 229F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.Controls.Add(this.lblFiltroNroMovimento, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtFiltroCodigo, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblFiltroEntidade, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtFiltroNroMovimento, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblFiltroData, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnAplicar, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtFiltroEntidade, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.77778F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.22222F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(614, 45);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // lblFiltroNroMovimento
            // 
            this.lblFiltroNroMovimento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFiltroNroMovimento.Location = new System.Drawing.Point(3, 0);
            this.lblFiltroNroMovimento.Name = "lblFiltroNroMovimento";
            this.lblFiltroNroMovimento.Size = new System.Drawing.Size(96, 17);
            this.lblFiltroNroMovimento.TabIndex = 0;
            this.lblFiltroNroMovimento.Text = "Nº de movimento";
            // 
            // txtFiltroCodigo
            // 
            this.txtFiltroCodigo.AcceptsReturn = true;
            this.txtFiltroCodigo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFiltroCodigo.Location = new System.Drawing.Point(334, 20);
            this.txtFiltroCodigo.Name = "txtFiltroCodigo";
            this.txtFiltroCodigo.Size = new System.Drawing.Size(96, 20);
            this.txtFiltroCodigo.TabIndex = 3;
            this.txtFiltroCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // lblFiltroEntidade
            // 
            this.lblFiltroEntidade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFiltroEntidade.Location = new System.Drawing.Point(334, 0);
            this.lblFiltroEntidade.Name = "lblFiltroEntidade";
            this.lblFiltroEntidade.Size = new System.Drawing.Size(96, 17);
            this.lblFiltroEntidade.TabIndex = 4;
            this.lblFiltroEntidade.Text = "Codigo";
            // 
            // txtFiltroNroMovimento
            // 
            this.txtFiltroNroMovimento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFiltroNroMovimento.Location = new System.Drawing.Point(3, 20);
            this.txtFiltroNroMovimento.Name = "txtFiltroNroMovimento";
            this.txtFiltroNroMovimento.Size = new System.Drawing.Size(96, 22);
            this.txtFiltroNroMovimento.TabIndex = 1;
            this.txtFiltroNroMovimento.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtFiltroNroMovimento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // lblFiltroData
            // 
            this.lblFiltroData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFiltroData.Location = new System.Drawing.Point(105, 0);
            this.lblFiltroData.Name = "lblFiltroData";
            this.lblFiltroData.Size = new System.Drawing.Size(223, 17);
            this.lblFiltroData.TabIndex = 6;
            this.lblFiltroData.Text = "Data";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dateTimePicker2);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(105, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(223, 22);
            this.panel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "——";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(126, 1);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.ShowCheckBox = true;
            this.dateTimePicker2.Size = new System.Drawing.Size(94, 20);
            this.dateTimePicker2.TabIndex = 1;
            this.dateTimePicker2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(3, 1);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowCheckBox = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(94, 20);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(436, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Entidade";
            // 
            // txtFiltroEntidade
            // 
            this.txtFiltroEntidade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFiltroEntidade.Location = new System.Drawing.Point(436, 20);
            this.txtFiltroEntidade.Name = "txtFiltroEntidade";
            this.txtFiltroEntidade.Size = new System.Drawing.Size(110, 20);
            this.txtFiltroEntidade.TabIndex = 9;
            // 
            // MovimentoList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "MovimentoList";
            this.Size = new System.Drawing.Size(620, 268);
            this.grpResultados.ResumeLayout(false);
            this.grpFiltro.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader columnHeaderID;
        private System.Windows.Forms.ColumnHeader columnHeaderData;
        private System.Windows.Forms.ColumnHeader columnHeaderEntidade;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        internal System.Windows.Forms.Label lblFiltroNroMovimento;
        internal System.Windows.Forms.TextBox txtFiltroCodigo;
        internal System.Windows.Forms.Label lblFiltroEntidade;
        internal GISA.Controls.PxIntegerBox txtFiltroNroMovimento;
        internal System.Windows.Forms.Label lblFiltroData;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFiltroEntidade;
        private System.Windows.Forms.ColumnHeader columnHeaderCodigo;
    }
}
