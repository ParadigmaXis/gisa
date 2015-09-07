using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;


namespace GISA.Controls
{
    partial class PaginatedDataGridView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpResultados = new System.Windows.Forms.GroupBox();
            this.dataGridVwPaginated = new GISA.Controls.PxDataGridView();
            this.txtNroPagina = new GISA.Controls.PxPageIntegerBox();
            this.btnProximo = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.grpFiltro = new System.Windows.Forms.GroupBox();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.grpResultados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVwPaginated)).BeginInit();
            this.grpFiltro.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpResultados
            // 
            this.grpResultados.Controls.Add(this.dataGridVwPaginated);
            this.grpResultados.Controls.Add(this.txtNroPagina);
            this.grpResultados.Controls.Add(this.btnProximo);
            this.grpResultados.Controls.Add(this.btnAnterior);
            this.grpResultados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpResultados.Location = new System.Drawing.Point(0, 59);
            this.grpResultados.Name = "grpResultados";
            this.grpResultados.Size = new System.Drawing.Size(851, 285);
            this.grpResultados.TabIndex = 5;
            this.grpResultados.TabStop = false;
            // 
            // dataGridVwPaginated
            // 
            this.dataGridVwPaginated.AllowUserToAddRows = false;
            this.dataGridVwPaginated.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridVwPaginated.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridVwPaginated.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridVwPaginated.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridVwPaginated.Location = new System.Drawing.Point(6, 19);
            this.dataGridVwPaginated.MultiSelect = false;
            this.dataGridVwPaginated.Name = "dataGridVwPaginated";
            this.dataGridVwPaginated.Size = new System.Drawing.Size(807, 260);
            this.dataGridVwPaginated.SmallImageList = null;
            this.dataGridVwPaginated.TabIndex = 7;
            // 
            // txtNroPagina
            // 
            this.txtNroPagina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNroPagina.Location = new System.Drawing.Point(819, 64);
            this.txtNroPagina.Name = "txtNroPagina";
            this.txtNroPagina.Size = new System.Drawing.Size(24, 20);
            this.txtNroPagina.TabIndex = 6;
            this.txtNroPagina.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnProximo
            // 
            this.btnProximo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProximo.Enabled = false;
            this.btnProximo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnProximo.Location = new System.Drawing.Point(819, 92);
            this.btnProximo.Name = "btnProximo";
            this.btnProximo.Size = new System.Drawing.Size(24, 24);
            this.btnProximo.TabIndex = 3;
            this.btnProximo.UseVisualStyleBackColor = true;
            // 
            // btnAnterior
            // 
            this.btnAnterior.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnterior.Enabled = false;
            this.btnAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAnterior.Location = new System.Drawing.Point(819, 32);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(24, 24);
            this.btnAnterior.TabIndex = 2;
            this.btnAnterior.UseVisualStyleBackColor = true;
            // 
            // grpFiltro
            // 
            this.grpFiltro.Controls.Add(this.btnAplicar);
            this.grpFiltro.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFiltro.Location = new System.Drawing.Point(0, 0);
            this.grpFiltro.Name = "grpFiltro";
            this.grpFiltro.Size = new System.Drawing.Size(851, 59);
            this.grpFiltro.TabIndex = 6;
            this.grpFiltro.TabStop = false;
            this.grpFiltro.Text = "Filtro";
            this.grpFiltro.Visible = false;
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(767, 21);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(64, 24);
            this.btnAplicar.TabIndex = 3;
            this.btnAplicar.Text = "&Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = true;
            // 
            // PaginatedDataGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpResultados);
            this.Controls.Add(this.grpFiltro);
            this.Name = "PaginatedDataGridView";
            this.Size = new System.Drawing.Size(851, 344);
            this.grpResultados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVwPaginated)).EndInit();
            this.grpFiltro.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.GroupBox grpResultados;
        protected System.Windows.Forms.GroupBox grpFiltro;
        protected System.Windows.Forms.Button btnAplicar;
        protected System.Windows.Forms.Button btnAnterior;
        protected System.Windows.Forms.Button btnProximo;
        protected PxPageIntegerBox txtNroPagina;
        protected GISA.Controls.PxDataGridView dataGridVwPaginated;

        //internal System.Windows.Forms.ToolTip ToolTip;
    }
}
