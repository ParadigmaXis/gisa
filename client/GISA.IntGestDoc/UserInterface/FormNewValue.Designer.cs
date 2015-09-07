namespace GISA.IntGestDoc.UserInterface
{
    partial class FormNewValue
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.pxDateBoxInicio = new GISA.Controls.PxDateBox();
            this.pxDateBoxFim = new GISA.Controls.PxDateBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(124, 114);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Confirmar";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(205, 114);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(12, 12);
            this.txtValue.Multiline = true;
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(268, 96);
            this.txtValue.TabIndex = 2;
            this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
            // 
            // pxDateBoxInicio
            // 
            this.pxDateBoxInicio.Location = new System.Drawing.Point(12, 10);
            this.pxDateBoxInicio.Name = "pxDateBoxInicio";
            this.pxDateBoxInicio.Size = new System.Drawing.Size(103, 22);
            this.pxDateBoxInicio.TabIndex = 3;
            this.pxDateBoxInicio.ValueDay = "";
            this.pxDateBoxInicio.ValueMonth = "";
            this.pxDateBoxInicio.ValueYear = "";
            this.pxDateBoxInicio.PxDateBoxTextChanged += new GISA.Controls.PxDateBox.PxDateBoxTextChangedEventHandler(this.pxDateBox1_PxDateBoxTextChanged);
            // 
            // pxDateBoxFim
            // 
            this.pxDateBoxFim.Location = new System.Drawing.Point(140, 10);
            this.pxDateBoxFim.Name = "pxDateBoxFim";
            this.pxDateBoxFim.Size = new System.Drawing.Size(103, 22);
            this.pxDateBoxFim.TabIndex = 4;
            this.pxDateBoxFim.ValueDay = "";
            this.pxDateBoxFim.ValueMonth = "";
            this.pxDateBoxFim.ValueYear = "";
            this.pxDateBoxFim.PxDateBoxTextChanged += new GISA.Controls.PxDateBox.PxDateBoxTextChangedEventHandler(this.pxDateBox1_PxDateBoxTextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "—";
            // 
            // FormNewValue
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(292, 149);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pxDateBoxFim);
            this.Controls.Add(this.pxDateBoxInicio);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "FormNewValue";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Valor novo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtValue;
        private GISA.Controls.PxDateBox pxDateBoxInicio;
        private GISA.Controls.PxDateBox pxDateBoxFim;
        private System.Windows.Forms.Label label1;
    }
}