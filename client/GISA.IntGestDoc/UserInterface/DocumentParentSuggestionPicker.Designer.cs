using GISA.IntGestDoc.Model.EntidadesInternas;

namespace GISA.IntGestDoc.UserInterface
{
    partial class DocumentParentSuggestionPicker
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
            this.propriedadeSuggestionPickerProcesso = new PropriedadeSuggestionPickerTemplate<DocumentoGisa>();
            this.propriedadeSuggestionPickerSerie = new PropriedadeSuggestionPickerTemplate<DocumentoGisa>();
            this.SuspendLayout();
            // 
            // propriedadeSuggestionPickerProcesso
            // 
            this.propriedadeSuggestionPickerProcesso.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propriedadeSuggestionPickerProcesso.Propriedade = null;
            this.propriedadeSuggestionPickerProcesso.Enabled = true;
            this.propriedadeSuggestionPickerProcesso.IsIconComposed = true;
            this.propriedadeSuggestionPickerProcesso.Location = new System.Drawing.Point(0, 0);
            this.propriedadeSuggestionPickerProcesso.Name = "correspondenciaSuggestionPicker1";
            this.propriedadeSuggestionPickerProcesso.Size = new System.Drawing.Size(87, 22);
            this.propriedadeSuggestionPickerProcesso.TabIndex = 0;
            // 
            // propriedadeSuggestionPickerSerie
            // 
            this.propriedadeSuggestionPickerSerie.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propriedadeSuggestionPickerSerie.Propriedade = null;
            this.propriedadeSuggestionPickerSerie.Enabled = true;
            this.propriedadeSuggestionPickerSerie.IsIconComposed = true;
            this.propriedadeSuggestionPickerSerie.Location = new System.Drawing.Point(0, 30);
            this.propriedadeSuggestionPickerSerie.Name = "correspondenciaSuggestionPicker2";
            this.propriedadeSuggestionPickerSerie.Size = new System.Drawing.Size(87, 22);
            this.propriedadeSuggestionPickerSerie.TabIndex = 1;
            // 
            // DocumentParentSuggestionPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.propriedadeSuggestionPickerSerie);
            this.Controls.Add(this.propriedadeSuggestionPickerProcesso);
            this.Name = "DocumentParentSuggestionPicker";
            this.Size = new System.Drawing.Size(87, 52);
            this.ResumeLayout(false);

        }

        #endregion

        private PropriedadeSuggestionPickerTemplate<DocumentoGisa> propriedadeSuggestionPickerProcesso;
        private PropriedadeSuggestionPickerTemplate<DocumentoGisa> propriedadeSuggestionPickerSerie;
    }
}
