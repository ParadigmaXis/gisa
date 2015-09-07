using GISA.IntGestDoc.Model;
using GISA.IntGestDoc.Model.EntidadesInternas;

namespace GISA.IntGestDoc.UserInterface
{
    partial class ControlDocumentoGisaProcesso
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
            this.lblTecnicosObra = new System.Windows.Forms.Label();
            this.lblLocais = new System.Windows.Forms.Label();
            this.lblAverbamentos = new System.Windows.Forms.Label();
            this.lblTipologia = new System.Windows.Forms.Label();
            this.lblRequerentes = new System.Windows.Forms.Label();
            this.lblEntidadeProdutora = new System.Windows.Forms.Label();
            this.lblDP = new System.Windows.Forms.Label();
            this.lblConfidencialidade = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.SuggestionPickerLstTecObras = new CorrespondenciaSuggestionPickerList();
            this.SuggestionPickerLstLocais = new CorrespondenciaSuggestionPickerLstGeog();
            this.PropriedadeSuggestionPickerLstAverbamentos = new PropriedadeSuggestionPickerLst<string>();
            this.PropriedadeSuggestionPickerLstRequerentes = new PropriedadeSuggestionPickerLst<string>();
            this.propriedadeSugestionPickerData = new PropriedadeSuggestionPickerTemplate<DataIncompleta>();
            this.suggestionPickerTipologia = new GISA.IntGestDoc.UserInterface.CorrespondenciaSuggestionPicker();
            this.propriedadeSugestionPickerConfidencialidade = new PropriedadeSuggestionPickerTemplate<string>();
            this.suggestionPickerDocumento = new GISA.IntGestDoc.UserInterface.CorrespondenciaSuggestionPicker();
            this.suggestionPickerEP = new GISA.IntGestDoc.UserInterface.CorrespondenciaSuggestionPicker();
            this.propriedadeSugestionPickerSerie = new PropriedadeSuggestionPickerTemplate<DocumentoGisa>();
            this.lblSerie = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTecnicosObra
            // 
            this.lblTecnicosObra.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTecnicosObra.AutoSize = true;
            this.lblTecnicosObra.Location = new System.Drawing.Point(13, 510);
            this.lblTecnicosObra.Name = "lblTecnicosObra";
            this.lblTecnicosObra.Size = new System.Drawing.Size(93, 13);
            this.lblTecnicosObra.TabIndex = 108;
            this.lblTecnicosObra.Text = "Técnicos de obra";
            // 
            // lblLocais
            // 
            this.lblLocais.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLocais.AutoSize = true;
            this.lblLocais.Location = new System.Drawing.Point(13, 403);
            this.lblLocais.Name = "lblLocais";
            this.lblLocais.Size = new System.Drawing.Size(41, 13);
            this.lblLocais.TabIndex = 106;
            this.lblLocais.Text = "Locais";
            // 
            // lblAverbamentos
            // 
            this.lblAverbamentos.Location = new System.Drawing.Point(13, 296);
            this.lblAverbamentos.Name = "lblAverbamentos";
            this.lblAverbamentos.Size = new System.Drawing.Size(117, 30);
            this.lblAverbamentos.TabIndex = 104;
            this.lblAverbamentos.Text = "Requerentes (averbamento)";
            // 
            // lblTipologia
            // 
            this.lblTipologia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTipologia.AutoSize = true;
            this.lblTipologia.Location = new System.Drawing.Point(13, 157);
            this.lblTipologia.Name = "lblTipologia";
            this.lblTipologia.Size = new System.Drawing.Size(118, 13);
            this.lblTipologia.TabIndex = 98;
            this.lblTipologia.Text = "Tipologia informacional";
            // 
            // lblRequerentes
            // 
            this.lblRequerentes.Location = new System.Drawing.Point(13, 189);
            this.lblRequerentes.Name = "lblRequerentes";
            this.lblRequerentes.Size = new System.Drawing.Size(103, 30);
            this.lblRequerentes.TabIndex = 97;
            this.lblRequerentes.Text = "Requerentes (inicial)";
            // 
            // lblEntidadeProdutora
            // 
            this.lblEntidadeProdutora.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEntidadeProdutora.AutoSize = true;
            this.lblEntidadeProdutora.Location = new System.Drawing.Point(13, 37);
            this.lblEntidadeProdutora.Name = "lblEntidadeProdutora";
            this.lblEntidadeProdutora.Size = new System.Drawing.Size(100, 13);
            this.lblEntidadeProdutora.TabIndex = 95;
            this.lblEntidadeProdutora.Text = "Entidade produtora";
            // 
            // lblDP
            // 
            this.lblDP.AutoSize = true;
            this.lblDP.Location = new System.Drawing.Point(13, 97);
            this.lblDP.Name = "lblDP";
            this.lblDP.Size = new System.Drawing.Size(96, 13);
            this.lblDP.TabIndex = 94;
            this.lblDP.Text = "Data de produção";
            // 
            // lblConfidencialidade
            // 
            this.lblConfidencialidade.AutoSize = true;
            this.lblConfidencialidade.Location = new System.Drawing.Point(13, 127);
            this.lblConfidencialidade.Name = "lblConfidencialidade";
            this.lblConfidencialidade.Size = new System.Drawing.Size(112, 13);
            this.lblConfidencialidade.TabIndex = 93;
            this.lblConfidencialidade.Text = "Condições de acesso";
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
            // SuggestionPickerLstTecObras
            // 
            this.SuggestionPickerLstTecObras.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SuggestionPickerLstTecObras.CorrespondenciaDoc = null;
            this.SuggestionPickerLstTecObras.Location = new System.Drawing.Point(138, 507);
            this.SuggestionPickerLstTecObras.LstCorrespondencia = null;
            this.SuggestionPickerLstTecObras.Name = "SuggestionPickerLstTecObras";
            this.SuggestionPickerLstTecObras.Size = new System.Drawing.Size(294, 97);
            this.SuggestionPickerLstTecObras.TabIndex = 109;
            // 
            // SuggestionPickerLstLocais
            // 
            this.SuggestionPickerLstLocais.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SuggestionPickerLstLocais.CorrespondenciaDoc = null;
            this.SuggestionPickerLstLocais.Location = new System.Drawing.Point(138, 400);
            this.SuggestionPickerLstLocais.LstCorrespondencia = null;
            this.SuggestionPickerLstLocais.Name = "SuggestionPickerLstLocais";
            this.SuggestionPickerLstLocais.Size = new System.Drawing.Size(294, 97);
            this.SuggestionPickerLstLocais.TabIndex = 107;
            // 
            // PropriedadeSuggestionPickerLstAverbamentos
            // 
            this.PropriedadeSuggestionPickerLstAverbamentos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PropriedadeSuggestionPickerLstAverbamentos.Location = new System.Drawing.Point(138, 293);
            this.PropriedadeSuggestionPickerLstAverbamentos.LstPropriedade = null;
            this.PropriedadeSuggestionPickerLstAverbamentos.Name = "PropriedadeSuggestionPickerLstAverbamentos";
            this.PropriedadeSuggestionPickerLstAverbamentos.Size = new System.Drawing.Size(294, 97);
            this.PropriedadeSuggestionPickerLstAverbamentos.TabIndex = 105;
            // 
            // PropriedadeSuggestionPickerLstRequerentes
            // 
            this.PropriedadeSuggestionPickerLstRequerentes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PropriedadeSuggestionPickerLstRequerentes.Location = new System.Drawing.Point(138, 186);
            this.PropriedadeSuggestionPickerLstRequerentes.LstPropriedade = null;
            this.PropriedadeSuggestionPickerLstRequerentes.Name = "PropriedadeSuggestionPickerLstRequerentes";
            this.PropriedadeSuggestionPickerLstRequerentes.Size = new System.Drawing.Size(294, 97);
            this.PropriedadeSuggestionPickerLstRequerentes.TabIndex = 103;
            // 
            // propriedadeSugestionPickerData
            // 
            this.propriedadeSugestionPickerData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propriedadeSugestionPickerData.Propriedade = null;
            this.propriedadeSugestionPickerData.IsIconComposed = false;
            this.propriedadeSugestionPickerData.Location = new System.Drawing.Point(138, 92);
            this.propriedadeSugestionPickerData.Name = "propriedadeSugestionPickerData";
            this.propriedadeSugestionPickerData.Size = new System.Drawing.Size(294, 24);
            this.propriedadeSugestionPickerData.TabIndex = 102;
            // 
            // suggestionPickerTipologia
            // 
            this.suggestionPickerTipologia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.suggestionPickerTipologia.Correspondencia = null;
            this.suggestionPickerTipologia.IsIconComposed = true;
            this.suggestionPickerTipologia.Location = new System.Drawing.Point(138, 152);
            this.suggestionPickerTipologia.Name = "suggestionPickerTipologia";
            this.suggestionPickerTipologia.Size = new System.Drawing.Size(294, 24);
            this.suggestionPickerTipologia.TabIndex = 101;
            // 
            // propriedadeSugestionPickerConfidencialidade
            // 
            this.propriedadeSugestionPickerConfidencialidade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propriedadeSugestionPickerConfidencialidade.Propriedade = null;
            this.propriedadeSugestionPickerConfidencialidade.IsIconComposed = false;
            this.propriedadeSugestionPickerConfidencialidade.Location = new System.Drawing.Point(138, 122);
            this.propriedadeSugestionPickerConfidencialidade.Name = "propriedadeSugestionPickerConfidencialidade";
            this.propriedadeSugestionPickerConfidencialidade.Size = new System.Drawing.Size(294, 24);
            this.propriedadeSugestionPickerConfidencialidade.TabIndex = 100;
            // 
            // suggestionPickerDocumento
            // 
            this.suggestionPickerDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.suggestionPickerDocumento.Correspondencia = null;
            this.suggestionPickerDocumento.IsIconComposed = false;
            this.suggestionPickerDocumento.Location = new System.Drawing.Point(138, 2);
            this.suggestionPickerDocumento.Name = "suggestionPickerDocumento";
            this.suggestionPickerDocumento.Size = new System.Drawing.Size(294, 24);
            this.suggestionPickerDocumento.TabIndex = 96;
            // 
            // suggestionPickerEP
            // 
            this.suggestionPickerEP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.suggestionPickerEP.Correspondencia = null;
            this.suggestionPickerEP.IsIconComposed = true;
            this.suggestionPickerEP.Location = new System.Drawing.Point(138, 32);
            this.suggestionPickerEP.Name = "suggestionPickerEP";
            this.suggestionPickerEP.Size = new System.Drawing.Size(294, 24);
            this.suggestionPickerEP.TabIndex = 99;
            // 
            // propriedadeSugestionPickerSerie
            // 
            this.propriedadeSugestionPickerSerie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propriedadeSugestionPickerSerie.Propriedade = null;
            this.propriedadeSugestionPickerSerie.IsIconComposed = true;
            this.propriedadeSugestionPickerSerie.Location = new System.Drawing.Point(138, 62);
            this.propriedadeSugestionPickerSerie.Name = "propriedadeSugestionPickerSerie";
            this.propriedadeSugestionPickerSerie.Size = new System.Drawing.Size(294, 24);
            this.propriedadeSugestionPickerSerie.TabIndex = 128;
            // 
            // lblSerie
            // 
            this.lblSerie.AutoSize = true;
            this.lblSerie.Location = new System.Drawing.Point(13, 67);
            this.lblSerie.Name = "label1";
            this.lblSerie.Size = new System.Drawing.Size(34, 13);
            this.lblSerie.TabIndex = 127;
            this.lblSerie.Text = "(Sub) Série";
            // 
            // ControlDocumentoGisaProcesso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.propriedadeSugestionPickerSerie);
            this.Controls.Add(this.lblSerie);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.SuggestionPickerLstTecObras);
            this.Controls.Add(this.lblTecnicosObra);
            this.Controls.Add(this.SuggestionPickerLstLocais);
            this.Controls.Add(this.lblLocais);
            this.Controls.Add(this.PropriedadeSuggestionPickerLstAverbamentos);
            this.Controls.Add(this.lblAverbamentos);
            this.Controls.Add(this.PropriedadeSuggestionPickerLstRequerentes);
            this.Controls.Add(this.propriedadeSugestionPickerData);
            this.Controls.Add(this.suggestionPickerTipologia);
            this.Controls.Add(this.propriedadeSugestionPickerConfidencialidade);
            this.Controls.Add(this.suggestionPickerDocumento);
            this.Controls.Add(this.suggestionPickerEP);
            this.Controls.Add(this.lblTipologia);
            this.Controls.Add(this.lblRequerentes);
            this.Controls.Add(this.lblEntidadeProdutora);
            this.Controls.Add(this.lblDP);
            this.Controls.Add(this.lblConfidencialidade);
            this.Name = "ControlDocumentoGisaProcesso";
            this.Size = new System.Drawing.Size(442, 615);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CorrespondenciaSuggestionPickerList SuggestionPickerLstTecObras;
        private System.Windows.Forms.Label lblTecnicosObra;
        private CorrespondenciaSuggestionPickerLstGeog SuggestionPickerLstLocais;
        private System.Windows.Forms.Label lblLocais;
        private PropriedadeSuggestionPickerLst<string> PropriedadeSuggestionPickerLstAverbamentos;
        private System.Windows.Forms.Label lblAverbamentos;
        private PropriedadeSuggestionPickerLst<string> PropriedadeSuggestionPickerLstRequerentes;
        private PropriedadeSuggestionPickerTemplate<DataIncompleta> propriedadeSugestionPickerData;
        private CorrespondenciaSuggestionPicker suggestionPickerTipologia;
        private PropriedadeSuggestionPickerTemplate<string> propriedadeSugestionPickerConfidencialidade;
        private CorrespondenciaSuggestionPicker suggestionPickerDocumento;
        private CorrespondenciaSuggestionPicker suggestionPickerEP;
        private System.Windows.Forms.Label lblTipologia;
        private System.Windows.Forms.Label lblRequerentes;
        private System.Windows.Forms.Label lblEntidadeProdutora;
        private System.Windows.Forms.Label lblDP;
        private System.Windows.Forms.Label lblConfidencialidade;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblID;
        private PropriedadeSuggestionPickerTemplate<DocumentoGisa> propriedadeSugestionPickerSerie;
        private System.Windows.Forms.Label lblSerie;
    }
}
