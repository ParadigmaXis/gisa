namespace GISA
{
    partial class ControlLocalConsulta
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
            this.components = new System.ComponentModel.Container();
            this.grpLocalConsulta = new System.Windows.Forms.GroupBox();
            this.btnLocalConsultaManager = new System.Windows.Forms.Button();
            this.cbLocalConsulta = new System.Windows.Forms.ComboBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grpLocalConsulta.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLocalConsulta
            // 
            this.grpLocalConsulta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpLocalConsulta.Controls.Add(this.btnLocalConsultaManager);
            this.grpLocalConsulta.Controls.Add(this.cbLocalConsulta);
            this.grpLocalConsulta.Location = new System.Drawing.Point(0, 0);
            this.grpLocalConsulta.Name = "grpLocalConsulta";
            this.grpLocalConsulta.Size = new System.Drawing.Size(140, 48);
            this.grpLocalConsulta.TabIndex = 2;
            this.grpLocalConsulta.TabStop = false;
            this.grpLocalConsulta.Text = "Local de consulta";
            // 
            // btnLocalConsultaManager
            // 
            this.btnLocalConsultaManager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLocalConsultaManager.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLocalConsultaManager.Location = new System.Drawing.Point(110, 20);
            this.btnLocalConsultaManager.Name = "btnLocalConsultaManager";
            this.btnLocalConsultaManager.Size = new System.Drawing.Size(24, 23);
            this.btnLocalConsultaManager.TabIndex = 2;
            // 
            // cbLocalConsulta
            // 
            this.cbLocalConsulta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLocalConsulta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLocalConsulta.DropDownWidth = 200;
            this.cbLocalConsulta.Location = new System.Drawing.Point(8, 20);
            this.cbLocalConsulta.Name = "cbLocalConsulta";
            this.cbLocalConsulta.Size = new System.Drawing.Size(96, 21);
            this.cbLocalConsulta.TabIndex = 1;
            // 
            // ControlLocalConsulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpLocalConsulta);
            this.Name = "ControlLocalConsulta";
            this.Size = new System.Drawing.Size(140, 48);
            this.grpLocalConsulta.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox grpLocalConsulta;
        internal System.Windows.Forms.Button btnLocalConsultaManager;
        internal System.Windows.Forms.ComboBox cbLocalConsulta;
        internal System.Windows.Forms.ToolTip ToolTip1;
    }
}
