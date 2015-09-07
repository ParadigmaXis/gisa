namespace GISA
{
    partial class MasterPanelPermissoesPlanoClassificacao : MasterPanelNiveis
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
            this.txtSelectedUser = new System.Windows.Forms.TextBox();
            this.lblSelectUser = new System.Windows.Forms.Label();
            this.btnSelectUser = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlToolbarPadding.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSelectedUser
            // 
            this.txtSelectedUser.Location = new System.Drawing.Point(105, 15);
            this.txtSelectedUser.Name = "txtSelectedUser";
            this.txtSelectedUser.ReadOnly = true;
            this.txtSelectedUser.Size = new System.Drawing.Size(236, 20);
            this.txtSelectedUser.TabIndex = 6;
            // 
            // lblSelectUser
            // 
            this.lblSelectUser.AutoSize = true;
            this.lblSelectUser.Location = new System.Drawing.Point(6, 18);
            this.lblSelectUser.Name = "lblSelectUser";
            this.lblSelectUser.Size = new System.Drawing.Size(93, 13);
            this.lblSelectUser.TabIndex = 7;
            this.lblSelectUser.Text = "Utilizador / Grupo:";
            // 
            // btnSelectUser
            // 
            this.btnSelectUser.Location = new System.Drawing.Point(347, 13);
            this.btnSelectUser.Name = "btnSelectUser";
            this.btnSelectUser.Size = new System.Drawing.Size(75, 23);
            this.btnSelectUser.TabIndex = 8;
            this.btnSelectUser.Text = "Seleccionar";
            this.btnSelectUser.UseVisualStyleBackColor = true;
            this.btnSelectUser.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSelectUser);
            this.groupBox1.Controls.Add(this.txtSelectedUser);
            this.groupBox1.Controls.Add(this.btnSelectUser);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 328);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(600, 41);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operador";
            // 
            // MasterPanelPermissoesPlanoClassificacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "MasterPanelPermissoesPlanoClassificacao";
            this.Size = new System.Drawing.Size(600, 369);
            this.Controls.SetChildIndex(this.lblFuncao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.nivelNavigator1, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtSelectedUser;
        private System.Windows.Forms.Label lblSelectUser;
        private System.Windows.Forms.Button btnSelectUser;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
