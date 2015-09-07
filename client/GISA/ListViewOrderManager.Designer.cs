namespace GISA
{
    partial class ListViewOrderManager
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFullScreen = new System.Windows.Forms.Button();
            this.btnFim = new System.Windows.Forms.Button();
            this.btnInicio = new System.Windows.Forms.Button();
            this.btnBaixo = new System.Windows.Forms.Button();
            this.btnCima = new System.Windows.Forms.Button();
            this.lstVw = new GISA.Controls.PxListView();
            this.colDesignacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CurrentToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnFullScreen);
            this.groupBox1.Controls.Add(this.btnFim);
            this.groupBox1.Controls.Add(this.btnInicio);
            this.groupBox1.Controls.Add(this.btnBaixo);
            this.groupBox1.Controls.Add(this.btnCima);
            this.groupBox1.Controls.Add(this.lstVw);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(440, 471);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // btnFullScreen
            // 
            this.btnFullScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFullScreen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFullScreen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFullScreen.Location = new System.Drawing.Point(407, 303);
            this.btnFullScreen.Name = "btnFullScreen";
            this.btnFullScreen.Size = new System.Drawing.Size(26, 26);
            this.btnFullScreen.TabIndex = 21;
            this.CurrentToolTip.SetToolTip(this.btnFullScreen, "Mostrar no ecrã todo");
            this.btnFullScreen.Click += new System.EventHandler(this.btnFullScreen_Click);
            // 
            // btnFim
            // 
            this.btnFim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFim.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFim.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFim.Location = new System.Drawing.Point(407, 239);
            this.btnFim.Name = "btnFim";
            this.btnFim.Size = new System.Drawing.Size(26, 26);
            this.btnFim.TabIndex = 16;
            this.btnFim.Click += new System.EventHandler(this.btnFim_Click);
            // 
            // btnInicio
            // 
            this.btnInicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInicio.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInicio.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInicio.Location = new System.Drawing.Point(407, 143);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(26, 26);
            this.btnInicio.TabIndex = 15;
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
            // 
            // btnBaixo
            // 
            this.btnBaixo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBaixo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBaixo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBaixo.Location = new System.Drawing.Point(407, 207);
            this.btnBaixo.Name = "btnBaixo";
            this.btnBaixo.Size = new System.Drawing.Size(26, 26);
            this.btnBaixo.TabIndex = 14;
            this.btnBaixo.Click += new System.EventHandler(this.btnBaixo_Click);
            // 
            // btnCima
            // 
            this.btnCima.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCima.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCima.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCima.Location = new System.Drawing.Point(407, 175);
            this.btnCima.Name = "btnCima";
            this.btnCima.Size = new System.Drawing.Size(26, 26);
            this.btnCima.TabIndex = 13;
            this.btnCima.Click += new System.EventHandler(this.btnCima_Click);
            // 
            // lstVw
            // 
            this.lstVw.AllowColumnReorder = true;
            this.lstVw.AllowDrop = true;
            this.lstVw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVw.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDesignacao});
            this.lstVw.CustomizedSorting = false;
            this.lstVw.FullRowSelect = true;
            this.lstVw.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstVw.HideSelection = false;
            this.lstVw.Location = new System.Drawing.Point(8, 16);
            this.lstVw.Name = "lstVw";
            this.lstVw.ReturnSubItemIndex = false;
            this.lstVw.Size = new System.Drawing.Size(395, 449);
            this.lstVw.TabIndex = 9;
            this.lstVw.UseCompatibleStateImageBehavior = false;
            this.lstVw.View = System.Windows.Forms.View.Details;
            // 
            // colDesignacao
            // 
            this.colDesignacao.Text = "Designação";
            this.colDesignacao.Width = 360;
            // 
            // CurrentToolTip
            // 
            this.CurrentToolTip.ShowAlways = true;
            // 
            // ListViewOrderManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ListViewOrderManager";
            this.Size = new System.Drawing.Size(440, 471);
            this.Load += new System.EventHandler(this.ListViewOrderManager_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.Button btnFim;
        internal System.Windows.Forms.Button btnInicio;
        internal System.Windows.Forms.Button btnBaixo;
        internal System.Windows.Forms.Button btnCima;
        internal System.Windows.Forms.ColumnHeader colDesignacao;
        protected internal System.Windows.Forms.ToolTip CurrentToolTip;
        protected GISA.Controls.PxListView lstVw;
        internal System.Windows.Forms.Button btnFullScreen;
    }
}
