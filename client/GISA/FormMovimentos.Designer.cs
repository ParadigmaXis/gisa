namespace GISA
{
    partial class FormMovimento
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
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.ToolBar1 = new System.Windows.Forms.ToolBar();
            this.ToolBarButtonNew = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonEdit = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonDelete = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonFilter = new System.Windows.Forms.ToolBarButton();
            this.grpData = new System.Windows.Forms.GroupBox();
            this.dtpData = new System.Windows.Forms.DateTimePicker();
            this.entidadeList1 = new GISA.EntidadeList();
            this.grpData.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirmar.Location = new System.Drawing.Point(433, 445);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(77, 23);
            this.btnConfirmar.TabIndex = 4;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(514, 445);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(82, 23);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // ToolBar1
            // 
            this.ToolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.ToolBar1.AutoSize = false;
            this.ToolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.ToolBarButtonNew,
            this.ToolBarButtonEdit,
            this.ToolBarButtonDelete,
            this.ToolBarButton2,
            this.ToolBarButtonFilter});
            this.ToolBar1.DropDownArrows = true;
            this.ToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ToolBar1.Name = "ToolBar1";
            this.ToolBar1.ShowToolTips = true;
            this.ToolBar1.Size = new System.Drawing.Size(608, 26);
            this.ToolBar1.TabIndex = 10;
            // 
            // ToolBarButtonNew
            // 
            this.ToolBarButtonNew.ImageIndex = 0;
            this.ToolBarButtonNew.Name = "ToolBarButtonNew";
            // 
            // ToolBarButtonEdit
            // 
            this.ToolBarButtonEdit.ImageIndex = 1;
            this.ToolBarButtonEdit.Name = "ToolBarButtonEdit";
            this.ToolBarButtonEdit.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // ToolBarButtonDelete
            // 
            this.ToolBarButtonDelete.ImageIndex = 2;
            this.ToolBarButtonDelete.Name = "ToolBarButtonDelete";
            // 
            // ToolBarButton2
            // 
            this.ToolBarButton2.Name = "ToolBarButton2";
            this.ToolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButtonFilter
            // 
            this.ToolBarButtonFilter.ImageIndex = 3;
            this.ToolBarButtonFilter.Name = "ToolBarButtonFilter";
            this.ToolBarButtonFilter.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // grpData
            // 
            this.grpData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpData.Controls.Add(this.dtpData);
            this.grpData.Location = new System.Drawing.Point(10, 383);
            this.grpData.Name = "grpData";
            this.grpData.Size = new System.Drawing.Size(586, 49);
            this.grpData.TabIndex = 11;
            this.grpData.TabStop = false;
            this.grpData.Text = "Data";
            // 
            // dtpData
            // 
            this.dtpData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpData.Location = new System.Drawing.Point(6, 19);
            this.dtpData.Name = "dtpData";
            this.dtpData.Size = new System.Drawing.Size(574, 20);
            this.dtpData.TabIndex = 3;
            // 
            // entidadeList1
            // 
            this.entidadeList1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.entidadeList1.Location = new System.Drawing.Point(10, 35);
            this.entidadeList1.Name = "entidadeList1";
            this.entidadeList1.Size = new System.Drawing.Size(586, 343);
            this.entidadeList1.TabIndex = 9;
            // 
            // FormMovimento
            // 
            this.AcceptButton = this.btnConfirmar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(608, 473);
            this.ControlBox = false;
            this.Controls.Add(this.grpData);
            this.Controls.Add(this.ToolBar1);
            this.Controls.Add(this.entidadeList1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnConfirmar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormMovimento";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.grpData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnCancelar;
        private EntidadeList entidadeList1 = null;
        internal System.Windows.Forms.ToolBar ToolBar1;
        internal System.Windows.Forms.ToolBarButton ToolBarButtonNew;
        internal System.Windows.Forms.ToolBarButton ToolBarButtonEdit;
        internal System.Windows.Forms.ToolBarButton ToolBarButtonDelete;
        internal System.Windows.Forms.ToolBarButton ToolBarButton2;
        internal System.Windows.Forms.ToolBarButton ToolBarButtonFilter;
        private System.Windows.Forms.GroupBox grpData;
        private System.Windows.Forms.DateTimePicker dtpData;
    }
}