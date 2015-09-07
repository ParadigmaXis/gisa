namespace GISA.Controls
{
    abstract partial class PaginatedListView
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
            this.grpResultados = new System.Windows.Forms.GroupBox();
            this.txtNroPagina = new GISA.Controls.PxPageIntegerBox();
            this.lstVwPaginated = new GISA.Controls.PxListView();
            this.btnProximo = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.grpFiltro = new System.Windows.Forms.GroupBox();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grpResultados.SuspendLayout();
            this.grpFiltro.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpResultados
            // 
            this.grpResultados.Controls.Add(this.txtNroPagina);
            this.grpResultados.Controls.Add(this.lstVwPaginated);
            this.grpResultados.Controls.Add(this.btnProximo);
            this.grpResultados.Controls.Add(this.btnAnterior);
            this.grpResultados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpResultados.Location = new System.Drawing.Point(6, 65);
            this.grpResultados.Name = "grpResultados";
            this.grpResultados.Size = new System.Drawing.Size(839, 273);
            this.grpResultados.TabIndex = 4;
            this.grpResultados.TabStop = false;
            // 
            // txtNroPagina
            // 
            this.txtNroPagina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNroPagina.Location = new System.Drawing.Point(807, 64);
            this.txtNroPagina.Name = "txtNroPagina";
            this.txtNroPagina.Size = new System.Drawing.Size(24, 20);
            this.txtNroPagina.TabIndex = 6;
            this.txtNroPagina.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lstVwPaginated
            // 
            this.lstVwPaginated.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwPaginated.CustomizedSorting = true;
            this.lstVwPaginated.FullRowSelect = true;
            this.lstVwPaginated.HideSelection = false;
            this.lstVwPaginated.Location = new System.Drawing.Point(8, 16);
            this.lstVwPaginated.MultiSelect = false;
            this.lstVwPaginated.Name = "lstVwPaginated";
            this.lstVwPaginated.ReturnSubItemIndex = false;
            this.lstVwPaginated.Size = new System.Drawing.Size(791, 249);
            this.lstVwPaginated.TabIndex = 1;
            this.lstVwPaginated.UseCompatibleStateImageBehavior = false;
            this.lstVwPaginated.View = System.Windows.Forms.View.Details;
            // 
            // btnProximo
            // 
            this.btnProximo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProximo.Enabled = false;
            this.btnProximo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnProximo.Location = new System.Drawing.Point(807, 92);
            this.btnProximo.Name = "btnProximo";
            this.btnProximo.Size = new System.Drawing.Size(24, 24);
            this.btnProximo.TabIndex = 3;
            // 
            // btnAnterior
            // 
            this.btnAnterior.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnterior.Enabled = false;
            this.btnAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAnterior.Location = new System.Drawing.Point(807, 32);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(24, 24);
            this.btnAnterior.TabIndex = 2;
            // 
            // grpFiltro
            // 
            this.grpFiltro.Controls.Add(this.btnAplicar);
            this.grpFiltro.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFiltro.Location = new System.Drawing.Point(6, 6);
            this.grpFiltro.Name = "grpFiltro";
            this.grpFiltro.Size = new System.Drawing.Size(839, 59);
            this.grpFiltro.TabIndex = 3;
            this.grpFiltro.TabStop = false;
            this.grpFiltro.Text = "Filtro";
            this.grpFiltro.Visible = false;
            // 
            // btnAplicar
            // 
            this.btnAplicar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAplicar.Location = new System.Drawing.Point(767, 21);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(64, 24);
            this.btnAplicar.TabIndex = 3;
            this.btnAplicar.Text = "&Aplicar";
            // 
            // ToolTip
            // 
            this.ToolTip.ShowAlways = true;
            // 
            // PaginatedListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpResultados);
            this.Controls.Add(this.grpFiltro);
            this.Name = "PaginatedListView";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.Size = new System.Drawing.Size(851, 344);
            this.grpResultados.ResumeLayout(false);
            this.grpFiltro.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.GroupBox grpResultados;
        protected PxPageIntegerBox txtNroPagina;
        protected PxListView lstVwPaginated;
        protected System.Windows.Forms.Button btnProximo;
        protected System.Windows.Forms.Button btnAnterior;
        protected System.Windows.Forms.GroupBox grpFiltro;
        protected System.Windows.Forms.Button btnAplicar;
        protected System.Windows.Forms.ToolTip ToolTip;



    }
}
