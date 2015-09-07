using GISA.IntGestDoc.Model;
using GISA.IntGestDoc.Model.EntidadesInternas;

namespace GISA.IntGestDoc.UserInterface
{
    partial class ControlDocumentoGisaAnexo
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
            this.lblDescricao = new System.Windows.Forms.Label();
            this.lblAgrupador = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.propriedadeSuggestionPickerDescricao = new PropriedadeSuggestionPickerTemplate<string>();
            this.propriedadeSuggestionPickerAgrupador = new PropriedadeSuggestionPickerTemplate<string>();
            this.suggestionPickerDocumento = new CorrespondenciaSuggestionPicker();
            this.SuspendLayout();
            // 
            // lblDescricao
            // 
            this.lblDescricao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Location = new System.Drawing.Point(13, 125);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(121, 13);
            this.lblDescricao.TabIndex = 96;
            this.lblDescricao.Text = "Conteúdo informacional";
            // 
            // lblAgrupador
            // 
            this.lblAgrupador.AutoSize = true;
            this.lblAgrupador.Location = new System.Drawing.Point(13, 65);
            this.lblAgrupador.Name = "lblAgrupador";
            this.lblAgrupador.Size = new System.Drawing.Size(54, 13);
            this.lblAgrupador.TabIndex = 88;
            this.lblAgrupador.Text = "Agrupador";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(40, 2);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(90, 20);
            this.txtID.TabIndex = 126;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(13, 5);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(21, 13);
            this.lblID.TabIndex = 125;
            this.lblID.Text = "ID";
            // 
            // propriedadeSuggestionPickerDescricao
            // 
            this.propriedadeSuggestionPickerDescricao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propriedadeSuggestionPickerDescricao.Propriedade = null;
            this.propriedadeSuggestionPickerDescricao.IsIconComposed = false;
            this.propriedadeSuggestionPickerDescricao.Location = new System.Drawing.Point(138, 122);
            this.propriedadeSuggestionPickerDescricao.Name = "propriedadeSuggestionPickerDescricao";
            this.propriedadeSuggestionPickerDescricao.Size = new System.Drawing.Size(201, 24);
            this.propriedadeSuggestionPickerDescricao.TabIndex = 95;
            // 
            // suggestionPickerDocumento
            // 
            this.suggestionPickerDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.suggestionPickerDocumento.Correspondencia = null;
            this.suggestionPickerDocumento.IsIconComposed = false;
            this.suggestionPickerDocumento.Location = new System.Drawing.Point(138, 2);
            this.suggestionPickerDocumento.Name = "suggestionPickerDocumento";
            this.suggestionPickerDocumento.Size = new System.Drawing.Size(201, 24);
            this.suggestionPickerDocumento.TabIndex = 90;
            // 
            // propriedadeSuggestionPickerAgrupador
            // 
            this.propriedadeSuggestionPickerAgrupador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propriedadeSuggestionPickerAgrupador.Propriedade = null;
            this.propriedadeSuggestionPickerAgrupador.IsIconComposed = false;
            this.propriedadeSuggestionPickerAgrupador.Location = new System.Drawing.Point(138, 62);
            this.propriedadeSuggestionPickerAgrupador.Name = "propriedadeSuggestionPickerAgrupador";
            this.propriedadeSuggestionPickerAgrupador.Size = new System.Drawing.Size(201, 24);
            this.propriedadeSuggestionPickerAgrupador.TabIndex = 95;
            // 
            // ControlDocumentoGisaAnexo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.lblDescricao);
            this.Controls.Add(this.propriedadeSuggestionPickerDescricao);
            this.Controls.Add(this.propriedadeSuggestionPickerAgrupador);
            this.Controls.Add(this.suggestionPickerDocumento);
            this.Controls.Add(this.lblAgrupador);
            this.Name = "ControlDocumentoGisaAnexo";
            this.Size = new System.Drawing.Size(348, 182);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDescricao;
        private PropriedadeSuggestionPickerTemplate<string> propriedadeSuggestionPickerDescricao;
        private PropriedadeSuggestionPickerTemplate<string> propriedadeSuggestionPickerAgrupador;
        private CorrespondenciaSuggestionPicker suggestionPickerDocumento;
        private System.Windows.Forms.Label lblAgrupador;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblID;

    }
}
