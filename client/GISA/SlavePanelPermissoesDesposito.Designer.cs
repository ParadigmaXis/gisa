namespace GISA
{
    partial class SlavePanelPermissoesDesposito
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
            this.PanelMensagem1 = new GISA.PanelMensagem();
            this.lstvwPermissoes = new GISA.Controls.PxListView();
            this.chDeposito = new System.Windows.Forms.ColumnHeader();
            this.pnlToolbarPadding.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFuncao
            // 
            this.lblFuncao.Location = new System.Drawing.Point(0, 0);
            this.lblFuncao.Text = "Definir Permissões";
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
            // 
            // PanelMensagem1
            // 
            this.PanelMensagem1.BackColor = System.Drawing.SystemColors.Control;
            this.PanelMensagem1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMensagem1.IsLoaded = false;
            this.PanelMensagem1.IsPopulated = false;
            this.PanelMensagem1.Location = new System.Drawing.Point(0, 52);
            this.PanelMensagem1.Name = "PanelMensagem1";
            this.PanelMensagem1.Size = new System.Drawing.Size(600, 284);
            this.PanelMensagem1.TabIndex = 24;
            this.PanelMensagem1.TbBAuxListEventAssigned = false;
            this.PanelMensagem1.TheGenericDelegate = null;
            this.PanelMensagem1.Visible = false;
            // 
            // lstvwPermissoes
            // 
            this.lstvwPermissoes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvwPermissoes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDeposito});
            this.lstvwPermissoes.CustomizedSorting = false;
            this.lstvwPermissoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstvwPermissoes.FullRowSelect = true;
            this.lstvwPermissoes.GridLines = true;
            this.lstvwPermissoes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstvwPermissoes.Location = new System.Drawing.Point(0, 52);
            this.lstvwPermissoes.MultiSelect = false;
            this.lstvwPermissoes.Name = "lstvwPermissoes";
            this.lstvwPermissoes.ReturnSubItemIndex = false;
            this.lstvwPermissoes.Size = new System.Drawing.Size(597, 281);
            this.lstvwPermissoes.TabIndex = 25;
            this.lstvwPermissoes.UseCompatibleStateImageBehavior = false;
            this.lstvwPermissoes.View = System.Windows.Forms.View.Details;
            // 
            // chDeposito
            // 
            this.chDeposito.Text = "Depósito";
            this.chDeposito.Width = 258;
            // 
            // SlavePanelPermissoesDesposito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstvwPermissoes);
            this.Controls.Add(this.PanelMensagem1);
            this.Name = "SlavePanelPermissoesDesposito";
            this.Size = new System.Drawing.Size(600, 336);
            this.Controls.SetChildIndex(this.lblFuncao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.Controls.SetChildIndex(this.PanelMensagem1, 0);
            this.Controls.SetChildIndex(this.lstvwPermissoes, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        internal GISA.PanelMensagem PanelMensagem1;
        internal GISA.Controls.PxListView lstvwPermissoes;
        internal System.Windows.Forms.ColumnHeader chDeposito;
    }
}
