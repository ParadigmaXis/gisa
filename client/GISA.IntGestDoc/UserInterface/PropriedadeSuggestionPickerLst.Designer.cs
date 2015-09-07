namespace GISA.IntGestDoc.UserInterface
{
    partial class PropriedadeSuggestionPickerLst<T>
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
            this.lvCorrepondencias = new System.Windows.Forms.ListView();
            this.chNro = new System.Windows.Forms.ColumnHeader();
            this.chDesignacao = new System.Windows.Forms.ColumnHeader();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.propriedadeSuggestionPicker1 = new GISA.IntGestDoc.UserInterface.PropriedadeSuggestionPickerTemplate<T>();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvCorrepondencias
            // 
            this.lvCorrepondencias.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvCorrepondencias.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chNro,
            this.chDesignacao});
            this.lvCorrepondencias.FullRowSelect = true;
            this.lvCorrepondencias.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvCorrepondencias.HideSelection = false;
            this.lvCorrepondencias.Location = new System.Drawing.Point(0, 0);
            this.lvCorrepondencias.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lvCorrepondencias.MultiSelect = false;
            this.lvCorrepondencias.Name = "lvCorrepondencias";
            this.lvCorrepondencias.Size = new System.Drawing.Size(180, 92);
            this.lvCorrepondencias.TabIndex = 3;
            this.lvCorrepondencias.UseCompatibleStateImageBehavior = false;
            this.lvCorrepondencias.View = System.Windows.Forms.View.Details;
            this.lvCorrepondencias.SelectedIndexChanged += new System.EventHandler(this.lvCorrepondencias_SelectedIndexChanged);
            // 
            // chNro
            // 
            this.chNro.Text = "";
            this.chNro.Width = 40;
            // 
            // chDesignacao
            // 
            this.chDesignacao.Text = "Designação";
            this.chDesignacao.Width = 264;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lvCorrepondencias, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.propriedadeSuggestionPicker1, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(367, 92);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // propriedadeSuggestionPicker1
            // 
            this.propriedadeSuggestionPicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.propriedadeSuggestionPicker1.Propriedade = null;
            this.propriedadeSuggestionPicker1.Enabled = false;
            this.propriedadeSuggestionPicker1.Location = new System.Drawing.Point(186, 34);
            this.propriedadeSuggestionPicker1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.propriedadeSuggestionPicker1.Name = "propriedadeSuggestionPicker1";
            this.propriedadeSuggestionPicker1.Size = new System.Drawing.Size(181, 24);
            this.propriedadeSuggestionPicker1.TabIndex = 2;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // PropriedadeSuggestionPickerLst
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PropriedadeSuggestionPickerLst";
            this.Size = new System.Drawing.Size(367, 92);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvCorrepondencias;
        private System.Windows.Forms.ColumnHeader chNro;
        private System.Windows.Forms.ColumnHeader chDesignacao;
        private PropriedadeSuggestionPickerTemplate<T> propriedadeSuggestionPicker1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ImageList imageList1;
    }
}
