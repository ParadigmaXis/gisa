namespace GISA
{
    partial class FormObjDigitalChangePermissions
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grpEscrever = new System.Windows.Forms.GroupBox();
            this.cbEscrever = new System.Windows.Forms.ComboBox();
            this.grpLer = new System.Windows.Forms.GroupBox();
            this.cbLer = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpEscrever.SuspendLayout();
            this.grpLer.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(23, 85);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "Confirmar";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(104, 85);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel1.Controls.Add(this.grpEscrever, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpLer, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(166, 48);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // grpEscrever
            // 
            this.grpEscrever.Controls.Add(this.cbEscrever);
            this.grpEscrever.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEscrever.Location = new System.Drawing.Point(86, 3);
            this.grpEscrever.Name = "grpEscrever";
            this.grpEscrever.Size = new System.Drawing.Size(77, 42);
            this.grpEscrever.TabIndex = 5;
            this.grpEscrever.TabStop = false;
            this.grpEscrever.Text = "Escrever";
            // 
            // cbEscrever
            // 
            this.cbEscrever.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbEscrever.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEscrever.FormattingEnabled = true;
            this.cbEscrever.Items.AddRange(new object[] {
            "",
            "Sim",
            "Não"});
            this.cbEscrever.Location = new System.Drawing.Point(3, 16);
            this.cbEscrever.Name = "cbEscrever";
            this.cbEscrever.Size = new System.Drawing.Size(71, 21);
            this.cbEscrever.TabIndex = 3;
            this.cbEscrever.SelectedIndexChanged += new System.EventHandler(this.cbEscrever_SelectedIndexChanged);
            // 
            // grpLer
            // 
            this.grpLer.Controls.Add(this.cbLer);
            this.grpLer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLer.Location = new System.Drawing.Point(3, 3);
            this.grpLer.Name = "grpLer";
            this.grpLer.Size = new System.Drawing.Size(77, 42);
            this.grpLer.TabIndex = 4;
            this.grpLer.TabStop = false;
            this.grpLer.Text = "Ler";
            // 
            // cbLer
            // 
            this.cbLer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbLer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLer.FormattingEnabled = true;
            this.cbLer.Items.AddRange(new object[] {
            "",
            "Sim",
            "Não"});
            this.cbLer.Location = new System.Drawing.Point(3, 16);
            this.cbLer.Name = "cbLer";
            this.cbLer.Size = new System.Drawing.Size(71, 21);
            this.cbLer.TabIndex = 3;
            this.cbLer.SelectedIndexChanged += new System.EventHandler(this.cbLer_SelectedIndexChanged);
            // 
            // FormObjDigitalChangePermissions
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(191, 120);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormObjDigitalChangePermissions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Editar permissões";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.grpEscrever.ResumeLayout(false);
            this.grpLer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox grpEscrever;
        private System.Windows.Forms.ComboBox cbEscrever;
        private System.Windows.Forms.GroupBox grpLer;
        private System.Windows.Forms.ComboBox cbLer;
    }
}