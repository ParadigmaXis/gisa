using System.Collections.Generic;
using System.Windows.Forms;
using System;

namespace GISA
{
    partial class MasterPanelDevolucoes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterPanelDevolucoes));
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
            this.tbCriar.ToolTipText = this.tbCriar.ToolTipText + " " + resources.GetString("Devolucao").ToLower();
            // 
            // tbEditar
            // 
            this.tbEditar.ImageIndex = 4;
            this.tbEditar.ToolTipText = this.tbEditar.ToolTipText + " " + resources.GetString("Devolucao").ToLower();
            // 
            // tbEliminar
            // 
            this.tbEliminar.ImageIndex = 2;
            this.tbEliminar.ToolTipText = this.tbEliminar.ToolTipText + " " + resources.GetString("Devolucao").ToLower();
            // 
            // tbFiltro
            // 
            this.tbFiltro.ImageIndex = 0;            
            // 
            // ilIcons
            // 
            this.ilIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcons.ImageStream")));
            this.ilIcons.Images.SetKeyName(0, "FiltroOn.bmp");
            this.ilIcons.Images.SetKeyName(1, "Relatorio.bmp");
            this.ilIcons.Images.SetKeyName(2, "Devolucao_eliminar_16x16.png");
            this.ilIcons.Images.SetKeyName(3, "Devolucao_criar_16x16.png");
            this.ilIcons.Images.SetKeyName(4, "Devolucao_editar_16x16.png");
            // 
            // lblFuncao
            // 
            this.lblFuncao.Text = "Devoluções";
            // 
            // comprovativoToolMenuItem
            // 
            this.comprovativoToolMenuItem.Index = 1;
            this.comprovativoToolMenuItem.Text = "Comprovativo";
            this.comprovativoToolMenuItem.Click += new System.EventHandler(this.comprovativoToolMenuItem_Click);
            // 
            // mPrint
            // 
            this.mPrint.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.comprovativoToolMenuItem});
            // 
            // MasterPanelDevolucoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "MasterPanelDevolucoes";
            this.pnlToolbarPadding.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem comprovativoToolMenuItem; 
        
    }
}
