namespace GISA
{
    partial class FRDDepositos
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
            components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.PanelMensagem1 = new PanelMensagem();
            this.PanelDepIdentificacao1 = new GISA.PanelDepIdentificacao();
            this.PanelDepUFEliminadas1 = new GISA.PanelDepUFEliminadas();
            this.SuspendLayout();
            //
            //DropDownTreeView1
            //
            this.DropDownTreeView1.GISAFunction = "Detalhes de depósito ";
            this.DropDownTreeView1.Name = "DropDownTreeView1";
            //
            //ToolBar
            //
            this.ToolBar.Name = "ToolBar";
            //
            //PanelDepIdentificacao1
            //
            this.PanelDepIdentificacao1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelDepIdentificacao1.Location = new System.Drawing.Point(0, 49);
            this.PanelDepIdentificacao1.Name = "PanelDepIdentificacao1";
            this.PanelDepIdentificacao1.Size = new System.Drawing.Size(700, 366);
            this.PanelDepIdentificacao1.TabIndex = 0;
            //
            //PanelDepUFEliminadas1
            //
            this.PanelDepUFEliminadas1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelDepUFEliminadas1.Location = new System.Drawing.Point(0, 49);
            this.PanelDepUFEliminadas1.Name = "PanelDepUFEliminadas1";
            this.PanelDepUFEliminadas1.Size = new System.Drawing.Size(700, 366);
            this.PanelDepUFEliminadas1.TabIndex = 1;
            //
            //FRDUnidadeFisica
            //
            this.GisaPanelScroller.Controls.Add(this.PanelMensagem1);
            this.GisaPanelScroller.Controls.Add(this.PanelDepIdentificacao1);
            this.GisaPanelScroller.Controls.Add(this.PanelDepUFEliminadas1);
            this.Name = "FRDDepósito";
            this.Size = new System.Drawing.Size(700, 415);
            this.ResumeLayout(false);
        }

        internal GISA.PanelMensagem PanelMensagem1;
        internal GISA.PanelDepIdentificacao PanelDepIdentificacao1;
        internal GISA.PanelDepUFEliminadas PanelDepUFEliminadas1;

        #endregion
    }
}
