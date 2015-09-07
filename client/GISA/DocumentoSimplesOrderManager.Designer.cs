namespace GISA
{
    partial class DocumentoSimplesOrderManager : ListViewOrderManager
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
            this.colObjDigFed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPublicado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDesignacaoOD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCheckIntegrity = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCheckIntegrity);
            this.groupBox1.Controls.Add(this.btnRemove);
            this.groupBox1.Controls.Add(this.btnEdit);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Size = new System.Drawing.Size(437, 471);
            this.groupBox1.Text = "Objetos Digitais Simples";
            this.groupBox1.Controls.SetChildIndex(this.lstVw, 0);
            this.groupBox1.Controls.SetChildIndex(this.btnAdd, 0);
            this.groupBox1.Controls.SetChildIndex(this.btnEdit, 0);
            this.groupBox1.Controls.SetChildIndex(this.btnRemove, 0);
            this.groupBox1.Controls.SetChildIndex(this.btnCheckIntegrity, 0);
            // 
            // lstVw
            // 
            this.lstVw.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDesignacaoOD,
            this.colPublicado,
            this.colObjDigFed});
            // 
            // colObjDigFed
            // 
            this.colObjDigFed.Text = "Identificador";
            this.colObjDigFed.Width = 80;
            // 
            // colPublicado
            // 
            this.colPublicado.Text = "Publicado";
            // 
            // colDesignacaoOD
            // 
            this.colDesignacaoOD.Text = "Título";
            this.colDesignacaoOD.Width = 360;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(407, 101);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(24, 24);
            this.btnRemove.TabIndex = 24;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEdit.Location = new System.Drawing.Point(407, 71);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(24, 24);
            this.btnEdit.TabIndex = 23;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(407, 41);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(24, 24);
            this.btnAdd.TabIndex = 22;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCheckIntegrity
            // 
            this.btnCheckIntegrity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckIntegrity.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheckIntegrity.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCheckIntegrity.Location = new System.Drawing.Point(407, 335);
            this.btnCheckIntegrity.Name = "btnCheckIntegrity";
            this.btnCheckIntegrity.Size = new System.Drawing.Size(26, 24);
            this.btnCheckIntegrity.TabIndex = 25;
            this.btnCheckIntegrity.Click += new System.EventHandler(this.btnCheckIntegrity_Click);
            // 
            // DocumentoSimplesOrderManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "DocumentoSimplesOrderManager";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader colObjDigFed;
        private System.Windows.Forms.ColumnHeader colPublicado;
        internal System.Windows.Forms.ColumnHeader colDesignacaoOD;
        internal System.Windows.Forms.Button btnRemove;
        internal System.Windows.Forms.Button btnEdit;
        internal System.Windows.Forms.Button btnAdd;
        internal System.Windows.Forms.Button btnCheckIntegrity;

    }
}
