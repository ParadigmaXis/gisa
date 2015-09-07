namespace GISA
{
    partial class MasterPanelMovimentos
    {

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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterPanelMovimentos));
            this.movList = new GISA.MovimentoList();
            this.tbCriar = new System.Windows.Forms.ToolBarButton();
            this.tbEditar = new System.Windows.Forms.ToolBarButton();
            this.tbEliminar = new System.Windows.Forms.ToolBarButton();
            this.tbFiltro = new System.Windows.Forms.ToolBarButton();
            this.tbSeparatorEliminarFiltro = new System.Windows.Forms.ToolBarButton();
            this.tbSeparatorFiltroImprimir = new System.Windows.Forms.ToolBarButton();
            this.mPrint = new System.Windows.Forms.ContextMenu();
            this.tbImprimir = new System.Windows.Forms.ToolBarButton();
            this.todosMovimentosToolMenuItem = new System.Windows.Forms.MenuItem();
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.pnlToolbarPadding.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFuncao
            // 
            this.lblFuncao.Location = new System.Drawing.Point(0, 0);
            this.lblFuncao.Text = "";
            // 
            // ToolBar
            // 
            this.ToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbCriar,
            this.tbEditar,
            this.tbEliminar,
            this.tbSeparatorEliminarFiltro,
            this.tbFiltro,
            this.tbSeparatorFiltroImprimir,
            this.tbImprimir});
            this.ToolBar.ImageList = this.ilIcons;
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
            // 
            // movList
            // 
            this.movList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.movList.Location = new System.Drawing.Point(0, 55);
            this.movList.Name = "movList";
            this.movList.Size = new System.Drawing.Size(600, 225);
            this.movList.TabIndex = 2;
            // 
            // tbCriar
            // 
            this.tbCriar.Name = "tbCriar";
            this.tbCriar.ToolTipText = resources.GetString("Criar");
            // 
            // tbEditar
            // 
            this.tbEditar.Name = "tbEditar";
            this.tbEditar.ToolTipText = resources.GetString("Editar");
            // 
            // tbEliminar
            // 
            this.tbEliminar.Name = "tbEliminar";
            this.tbEliminar.ToolTipText = resources.GetString("Eliminar");
            // 
            // tbFiltro
            // 
            this.tbFiltro.ImageIndex = 0;
            this.tbFiltro.Name = "tbFiltro";
            this.tbFiltro.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbFiltro.ToolTipText = resources.GetString("Filtro");
            // 
            // tbSeparatorEliminarFiltro
            // 
            this.tbSeparatorEliminarFiltro.Name = "tbSeparatorEliminarFiltro";
            this.tbSeparatorEliminarFiltro.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbSeparatorFiltroImprimir
            // 
            this.tbSeparatorFiltroImprimir.Name = "tbSeparatorFiltroImprimir";
            this.tbSeparatorFiltroImprimir.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            this.tbImprimir.ToolTipText = resources.GetString("Imprimir");
            // 
            // tbImprimir
            // 
            this.tbImprimir.ImageIndex = 1;
            this.tbImprimir.Name = "tbImprimir";
            this.mPrint.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.todosMovimentosToolMenuItem});
            // 
            // todosMovimentosToolMenuItem
            // 
            this.todosMovimentosToolMenuItem.Index = 0;
            this.todosMovimentosToolMenuItem.Text = "Todos movimentos";
            this.todosMovimentosToolMenuItem.Click += new System.EventHandler(this.todosMovimentosToolMenuItemm_Click);
            // 
            // ilIcons
            // 
            this.ilIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcons.ImageStream")));
            this.ilIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilIcons.Images.SetKeyName(0, "FiltroOn.bmp");
            this.ilIcons.Images.SetKeyName(1, "Relatorio.bmp");
            // 
            // MasterPanelMovimentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.movList);
            this.Name = "MasterPanelMovimentos";
            this.Controls.SetChildIndex(this.movList, 0);
            this.Controls.SetChildIndex(this.lblFuncao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal MovimentoList movList;     
        internal System.Windows.Forms.ToolBarButton tbSeparatorEliminarFiltro;
        internal System.Windows.Forms.ToolBarButton tbSeparatorFiltroImprimir;
        protected internal System.Windows.Forms.ToolBarButton tbImprimir;
        protected internal System.Windows.Forms.ToolBarButton tbCriar;
        protected internal System.Windows.Forms.ToolBarButton tbEditar;
        protected internal System.Windows.Forms.ToolBarButton tbEliminar;
        protected internal System.Windows.Forms.ToolBarButton tbFiltro;
        protected internal System.Windows.Forms.ImageList ilIcons;
        protected internal System.Windows.Forms.ContextMenu mPrint;
        private System.Windows.Forms.MenuItem todosMovimentosToolMenuItem;

    }
}
