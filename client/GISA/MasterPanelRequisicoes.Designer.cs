namespace GISA
{
    partial class MasterPanelRequisicoes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterPanelRequisicoes));
            this.documentosRequisitadosToolMenuItem = new System.Windows.Forms.MenuItem();
            this.comprovativoToolMenuItem = new System.Windows.Forms.MenuItem();
            this.pnlToolbarPadding.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbImprimir
            // 
            this.tbImprimir.DropDownMenu = this.mPrint;
            this.tbImprimir.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            // 
            // tbCriar
            // 
            this.tbCriar.ImageIndex = 3;
            this.tbCriar.ToolTipText = "Criar requisição";
            // 
            // tbEditar
            // 
            this.tbEditar.ImageIndex = 4;
            this.tbEditar.ToolTipText = "Editar requisição";
            // 
            // tbEliminar
            // 
            this.tbEliminar.ImageIndex = 2;
            this.tbEliminar.ToolTipText = "Eliminar requisição";
            // 
            // ilIcons
            // 
            this.ilIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcons.ImageStream")));
            this.ilIcons.Images.SetKeyName(0, "FiltroOn.bmp");
            this.ilIcons.Images.SetKeyName(1, "Relatorio.bmp");
            this.ilIcons.Images.SetKeyName(2, "Requisicao_eliminar_16x16.png");
            this.ilIcons.Images.SetKeyName(3, "Requisicao_criar_16x16.png");
            this.ilIcons.Images.SetKeyName(4, "Requisicao_editar_16x16.png");
            // 
            // lblFuncao
            // 
            this.lblFuncao.Text = "Requisições";
            // 
            // documentosRequisitadosToolMenuItem
            // 
            this.documentosRequisitadosToolMenuItem.Index = 1;
            this.documentosRequisitadosToolMenuItem.Text = "Documentos requisitados";
            this.documentosRequisitadosToolMenuItem.Click += new System.EventHandler(this.documentosRequisitadosToolMenuItem_Click);
            // 
            // comprovativoToolMenuItem
            // 
            this.comprovativoToolMenuItem.Index = 2;
            this.comprovativoToolMenuItem.Text = "Comprovativo";
            this.comprovativoToolMenuItem.Click += new System.EventHandler(this.comprovativoToolMenuItem_Click);
            // 
            // mPrint
            // 
            this.mPrint.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.documentosRequisitadosToolMenuItem,
            this.comprovativoToolMenuItem});
            // 
            // MasterPanelRequisicoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "MasterPanelRequisicoes";
            this.pnlToolbarPadding.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem documentosRequisitadosToolMenuItem;
        private System.Windows.Forms.MenuItem comprovativoToolMenuItem;

    }
}
