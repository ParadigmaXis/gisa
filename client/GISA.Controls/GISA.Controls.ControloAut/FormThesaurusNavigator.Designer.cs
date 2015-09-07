namespace GISA.Controls.ControloAut
{
    partial class FormThesaurusNavigator
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
            this.components = new System.ComponentModel.Container();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.grpNavegacao = new System.Windows.Forms.GroupBox();
            this.btnNavegar = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.controloAutList1 = new GISA.Controls.ControloAut.ControloAutList();
            this.controlTermosIndexacao1 = new GISA.Controls.ControloAut.ControlTermosIndexacao();
            this.grpNavegacao.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdicionar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAdicionar.Enabled = false;
            this.btnAdicionar.Location = new System.Drawing.Point(462, 337);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(72, 24);
            this.btnAdicionar.TabIndex = 5;
            this.btnAdicionar.Text = "A&dicionar";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(540, 337);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(72, 24);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "&Cancelar";
            // 
            // grpNavegacao
            // 
            this.grpNavegacao.Controls.Add(this.controlTermosIndexacao1);
            this.grpNavegacao.Location = new System.Drawing.Point(0, 0);
            this.grpNavegacao.Name = "grpNavegacao";
            this.grpNavegacao.Size = new System.Drawing.Size(622, 331);
            this.grpNavegacao.TabIndex = 7;
            this.grpNavegacao.TabStop = false;
            // 
            // btnNavegar
            // 
            this.btnNavegar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNavegar.Enabled = false;
            this.btnNavegar.Location = new System.Drawing.Point(12, 337);
            this.btnNavegar.Name = "btnNavegar";
            this.btnNavegar.Size = new System.Drawing.Size(72, 24);
            this.btnNavegar.TabIndex = 8;
            this.btnNavegar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnNavegar.Click += new System.EventHandler(this.btnNavegar_Click);
            // 
            // controloAutList1
            // 
            this.controloAutList1.AllowedNoticiaAutLocked = false;
            this.controloAutList1.CustomizedSorting = false;
            this.controloAutList1.FilterVisible = false;
            this.controloAutList1.Location = new System.Drawing.Point(0, 0);
            this.controloAutList1.MultiSelectListView = false;
            this.controloAutList1.Name = "controloAutList1";
            this.controloAutList1.Padding = new System.Windows.Forms.Padding(6);
            this.controloAutList1.Size = new System.Drawing.Size(622, 331);
            this.controloAutList1.TabIndex = 9;
            // 
            // controlTermosIndexacao1
            // 
            this.controlTermosIndexacao1.Location = new System.Drawing.Point(6, 12);
            this.controlTermosIndexacao1.Name = "controlTermosIndexacao1";
            this.controlTermosIndexacao1.NavigationMode = false;
            this.controlTermosIndexacao1.Size = new System.Drawing.Size(610, 313);
            this.controlTermosIndexacao1.TabIndex = 0;
            // 
            // FormThesaurusNavigator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 373);
            this.Controls.Add(this.controloAutList1);
            this.Controls.Add(this.btnNavegar);
            this.Controls.Add(this.grpNavegacao);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.btnCancelar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormThesaurusNavigator";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.grpNavegacao.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnAdicionar;
        internal System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox grpNavegacao;
        internal System.Windows.Forms.Button btnNavegar;
        private ControlTermosIndexacao controlTermosIndexacao1;
        private ControloAutList controloAutList1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}