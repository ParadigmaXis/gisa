namespace GISA
{
    partial class FormPickUser
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
            this.lstUtilizadores = new System.Windows.Forms.ListView();
            this.chTrusteeName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTipo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAmbito = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstUtilizadores
            // 
            this.lstUtilizadores.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstUtilizadores.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTrusteeName,
            this.chTipo,
            this.chAmbito});
            this.lstUtilizadores.FullRowSelect = true;
            this.lstUtilizadores.Location = new System.Drawing.Point(12, 12);
            this.lstUtilizadores.MultiSelect = false;
            this.lstUtilizadores.Name = "lstUtilizadores";
            this.lstUtilizadores.Size = new System.Drawing.Size(311, 242);
            this.lstUtilizadores.TabIndex = 0;
            this.lstUtilizadores.UseCompatibleStateImageBehavior = false;
            this.lstUtilizadores.View = System.Windows.Forms.View.Details;
            this.lstUtilizadores.SelectedIndexChanged += new System.EventHandler(this.lstUtilizadores_SelectedIndexChanged);
            // 
            // chTrusteeName
            // 
            this.chTrusteeName.Text = "Nome do utilizador";
            this.chTrusteeName.Width = 149;
            // 
            // chTipo
            // 
            this.chTipo.Text = "Tipo";
            this.chTipo.Width = 81;
            // 
            // chAmbito
            // 
            this.chAmbito.Text = "Âmbito";
            this.chAmbito.Width = 73;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(329, 12);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Seleccionar";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(329, 41);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FormPickUser
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(408, 266);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lstUtilizadores);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormPickUser";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccionar utilizador";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstUtilizadores;
        private System.Windows.Forms.ColumnHeader chTrusteeName;
        private System.Windows.Forms.ColumnHeader chTipo;
        private System.Windows.Forms.ColumnHeader chAmbito;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}