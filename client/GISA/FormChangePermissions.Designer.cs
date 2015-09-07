namespace GISA
{
    partial class FormChangePermissions
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
            this.grpCriar = new System.Windows.Forms.GroupBox();
            this.cbCriar = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grpExpandir = new System.Windows.Forms.GroupBox();
            this.cbExpandir = new System.Windows.Forms.ComboBox();
            this.grpApagar = new System.Windows.Forms.GroupBox();
            this.cbApagar = new System.Windows.Forms.ComboBox();
            this.grpEscrever = new System.Windows.Forms.GroupBox();
            this.cbEscrever = new System.Windows.Forms.ComboBox();
            this.grpLer = new System.Windows.Forms.GroupBox();
            this.cbLer = new System.Windows.Forms.ComboBox();
            this.grpCriar.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpExpandir.SuspendLayout();
            this.grpApagar.SuspendLayout();
            this.grpEscrever.SuspendLayout();
            this.grpLer.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(270, 85);
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
            this.btnCancel.Location = new System.Drawing.Point(351, 85);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // grpCriar
            // 
            this.grpCriar.Controls.Add(this.cbCriar);
            this.grpCriar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCriar.Location = new System.Drawing.Point(3, 3);
            this.grpCriar.Name = "grpCriar";
            this.grpCriar.Size = new System.Drawing.Size(77, 42);
            this.grpCriar.TabIndex = 2;
            this.grpCriar.TabStop = false;
            this.grpCriar.Text = "Criar";
            // 
            // cbCriar
            // 
            this.cbCriar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbCriar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCriar.FormattingEnabled = true;
            this.cbCriar.Items.AddRange(new object[] {
            "",
            "Sim",
            "Não"});
            this.cbCriar.Location = new System.Drawing.Point(3, 16);
            this.cbCriar.Name = "cbCriar";
            this.cbCriar.Size = new System.Drawing.Size(71, 21);
            this.cbCriar.TabIndex = 3;
            this.cbCriar.SelectedIndexChanged += new System.EventHandler(this.cbCriar_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel1.Controls.Add(this.grpExpandir, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpApagar, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpEscrever, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpCriar, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpLer, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(413, 48);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // grpExpandir
            // 
            this.grpExpandir.Controls.Add(this.cbExpandir);
            this.grpExpandir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpExpandir.Location = new System.Drawing.Point(335, 3);
            this.grpExpandir.Name = "grpExpandir";
            this.grpExpandir.Size = new System.Drawing.Size(77, 42);
            this.grpExpandir.TabIndex = 5;
            this.grpExpandir.TabStop = false;
            this.grpExpandir.Text = "Expandir";
            // 
            // cbExpandir
            // 
            this.cbExpandir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbExpandir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExpandir.FormattingEnabled = true;
            this.cbExpandir.Items.AddRange(new object[] {
            "",
            "Sim",
            "Não"});
            this.cbExpandir.Location = new System.Drawing.Point(3, 16);
            this.cbExpandir.Name = "cbExpandir";
            this.cbExpandir.Size = new System.Drawing.Size(71, 21);
            this.cbExpandir.TabIndex = 3;
            this.cbExpandir.SelectedIndexChanged += new System.EventHandler(this.cbExpandir_SelectedIndexChanged);
            // 
            // grpApagar
            // 
            this.grpApagar.Controls.Add(this.cbApagar);
            this.grpApagar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpApagar.Location = new System.Drawing.Point(252, 3);
            this.grpApagar.Name = "grpApagar";
            this.grpApagar.Size = new System.Drawing.Size(77, 42);
            this.grpApagar.TabIndex = 5;
            this.grpApagar.TabStop = false;
            this.grpApagar.Text = "Apagar";
            // 
            // cbApagar
            // 
            this.cbApagar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbApagar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbApagar.FormattingEnabled = true;
            this.cbApagar.Items.AddRange(new object[] {
            "",
            "Sim",
            "Não"});
            this.cbApagar.Location = new System.Drawing.Point(3, 16);
            this.cbApagar.Name = "cbApagar";
            this.cbApagar.Size = new System.Drawing.Size(71, 21);
            this.cbApagar.TabIndex = 3;
            this.cbApagar.SelectedIndexChanged += new System.EventHandler(this.cbApagar_SelectedIndexChanged);
            // 
            // grpEscrever
            // 
            this.grpEscrever.Controls.Add(this.cbEscrever);
            this.grpEscrever.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEscrever.Location = new System.Drawing.Point(169, 3);
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
            this.grpLer.Location = new System.Drawing.Point(86, 3);
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
            // FormChangePermissions
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(438, 120);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormChangePermissions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Editar permissões";
            this.grpCriar.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.grpExpandir.ResumeLayout(false);
            this.grpApagar.ResumeLayout(false);
            this.grpEscrever.ResumeLayout(false);
            this.grpLer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpCriar;
        private System.Windows.Forms.ComboBox cbCriar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox grpExpandir;
        private System.Windows.Forms.ComboBox cbExpandir;
        private System.Windows.Forms.GroupBox grpApagar;
        private System.Windows.Forms.ComboBox cbApagar;
        private System.Windows.Forms.GroupBox grpEscrever;
        private System.Windows.Forms.ComboBox cbEscrever;
        private System.Windows.Forms.GroupBox grpLer;
        private System.Windows.Forms.ComboBox cbLer;
    }
}