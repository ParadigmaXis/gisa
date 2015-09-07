namespace GISA.Controls.ControloAut
{
    partial class ControlTermosIndexacao
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
            this.trVwTermoIndexacao = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // trVwTermoIndexacao
            // 
            this.trVwTermoIndexacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trVwTermoIndexacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.trVwTermoIndexacao.HideSelection = false;
            this.trVwTermoIndexacao.Location = new System.Drawing.Point(0, 0);
            this.trVwTermoIndexacao.Name = "trVwTermoIndexacao";
            this.trVwTermoIndexacao.Size = new System.Drawing.Size(378, 365);
            this.trVwTermoIndexacao.TabIndex = 2;
            this.trVwTermoIndexacao.DoubleClick += new System.EventHandler(this.trVwTermoIndexacao_DoubleClick);
            // 
            // ControlTermosIndexacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trVwTermoIndexacao);
            this.Name = "ControlTermosIndexacao";
            this.Size = new System.Drawing.Size(378, 365);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TreeView trVwTermoIndexacao;
    }
}
