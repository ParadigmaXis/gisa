using GISA.IntGestDoc.Model;
using GISA.IntGestDoc.Model.EntidadesInternas;

namespace GISA.IntGestDoc.UserInterface
{
    partial class ControlDocumentoGisa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlDocumentoGisa));
            this.ilDocTypes = new System.Windows.Forms.ImageList(this.components);
            this.lblCodPostalLoc = new System.Windows.Forms.Label();
            this.lblOnomastico = new System.Windows.Forms.Label();
            this.lblMoradasNumRef = new System.Windows.Forms.Label();
            this.lblNotas = new System.Windows.Forms.Label();
            this.lblToponimia = new System.Windows.Forms.Label();
            this.lblTipologia = new System.Windows.Forms.Label();
            this.lblIdeografico = new System.Windows.Forms.Label();
            this.lblNumeroEspecifico = new System.Windows.Forms.Label();
            this.lblDP = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.lblAgrupador = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.suggestionPickerDocumento = new GISA.IntGestDoc.UserInterface.CorrespondenciaSuggestionPicker();
            this.suggestionPickerLstOnomastico = new GISA.IntGestDoc.UserInterface.CorrespondenciaSuggestionPickerList();
            this.propriedadeSuggestionPickerCodPostalLoc = new GISA.IntGestDoc.UserInterface.PropriedadeSuggestionPickerTemplate<string>();
            this.propriedadeSuggestionPickerAgrupador = new GISA.IntGestDoc.UserInterface.PropriedadeSuggestionPickerTemplate<string>();
            this.propriedadeSuggestionPickerNumFracRefPred = new GISA.IntGestDoc.UserInterface.PropriedadeSuggestionPickerTemplate<string>();
            this.propriedadeSuggestionPickerNotas = new GISA.IntGestDoc.UserInterface.PropriedadeSuggestionPickerTemplate<string>();
            this.suggestionPickerToponimia = new GISA.IntGestDoc.UserInterface.CorrespondenciaSuggestionPicker();
            this.suggestionPickerTipologia = new GISA.IntGestDoc.UserInterface.CorrespondenciaSuggestionPicker();
            this.propriedadeSuggestionPickerIdeografico = new GISA.IntGestDoc.UserInterface.PropriedadeSuggestionPickerTemplate<string>();
            this.propriedadeSuggestionPickerData = new GISA.IntGestDoc.UserInterface.PropriedadeSuggestionPickerTemplate<DataIncompleta>();
            this.propriedadeSuggestionPickerNumEsp = new GISA.IntGestDoc.UserInterface.PropriedadeSuggestionPickerTemplate<string>();
            this.SuspendLayout();
            // 
            // ilDocTypes
            // 
            this.ilDocTypes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilDocTypes.ImageStream")));
            this.ilDocTypes.TransparentColor = System.Drawing.Color.Transparent;
            this.ilDocTypes.Images.SetKeyName(0, "Documento");
            this.ilDocTypes.Images.SetKeyName(1, "EntidadeProdutora");
            this.ilDocTypes.Images.SetKeyName(2, "Ideografico");
            this.ilDocTypes.Images.SetKeyName(3, "Onomastico");
            this.ilDocTypes.Images.SetKeyName(4, "TipologiaInformacional");
            this.ilDocTypes.Images.SetKeyName(5, "Toponimia");
            // 
            // lblCodPostalLoc
            // 
            this.lblCodPostalLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCodPostalLoc.AutoSize = true;
            this.lblCodPostalLoc.Location = new System.Drawing.Point(13, 365);
            this.lblCodPostalLoc.Name = "lblCodPostalLoc";
            this.lblCodPostalLoc.Size = new System.Drawing.Size(121, 13);
            this.lblCodPostalLoc.TabIndex = 120;
            this.lblCodPostalLoc.Text = "Conteúdo informacional";
            // 
            // lblOnomastico
            // 
            this.lblOnomastico.AutoSize = true;
            this.lblOnomastico.Location = new System.Drawing.Point(13, 245);
            this.lblOnomastico.Name = "lblOnomastico";
            this.lblOnomastico.Size = new System.Drawing.Size(66, 13);
            this.lblOnomastico.TabIndex = 119;
            this.lblOnomastico.Text = "Onomástico";
            // 
            // lblMoradasNumRef
            // 
            this.lblMoradasNumRef.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMoradasNumRef.AutoSize = true;
            this.lblMoradasNumRef.Location = new System.Drawing.Point(13, 335);
            this.lblMoradasNumRef.Name = "lblMoradasNumRef";
            this.lblMoradasNumRef.Size = new System.Drawing.Size(121, 13);
            this.lblMoradasNumRef.TabIndex = 117;
            this.lblMoradasNumRef.Text = "Conteúdo informacional";
            // 
            // lblNotas
            // 
            this.lblNotas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNotas.AutoSize = true;
            this.lblNotas.Location = new System.Drawing.Point(13, 215);
            this.lblNotas.Name = "lblNotas";
            this.lblNotas.Size = new System.Drawing.Size(121, 13);
            this.lblNotas.TabIndex = 115;
            this.lblNotas.Text = "Conteúdo informacional";
            // 
            // lblToponimia
            // 
            this.lblToponimia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblToponimia.AutoSize = true;
            this.lblToponimia.Location = new System.Drawing.Point(13, 305);
            this.lblToponimia.Name = "lblToponimia";
            this.lblToponimia.Size = new System.Drawing.Size(61, 13);
            this.lblToponimia.TabIndex = 109;
            this.lblToponimia.Text = "Toponímia";
            // 
            // lblTipologia
            // 
            this.lblTipologia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTipologia.AutoSize = true;
            this.lblTipologia.Location = new System.Drawing.Point(13, 155);
            this.lblTipologia.Name = "lblTipologia";
            this.lblTipologia.Size = new System.Drawing.Size(53, 13);
            this.lblTipologia.TabIndex = 108;
            this.lblTipologia.Text = "Tipologia";
            // 
            // lblIdeografico
            // 
            this.lblIdeografico.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIdeografico.AutoSize = true;
            this.lblIdeografico.Location = new System.Drawing.Point(13, 185);
            this.lblIdeografico.Name = "lblIdeografico";
            this.lblIdeografico.Size = new System.Drawing.Size(63, 13);
            this.lblIdeografico.TabIndex = 107;
            this.lblIdeografico.Text = "Conteúdo informacional";
            // 
            // lblNumeroEspecifico
            // 
            this.lblNumeroEspecifico.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumeroEspecifico.AutoSize = true;
            this.lblNumeroEspecifico.Location = new System.Drawing.Point(13, 125);
            this.lblNumeroEspecifico.Name = "lblNumeroEspecifico";
            this.lblNumeroEspecifico.Size = new System.Drawing.Size(100, 13);
            this.lblNumeroEspecifico.TabIndex = 102;
            this.lblNumeroEspecifico.Text = "Número específico";
            // 
            // lblDP
            // 
            this.lblDP.AutoSize = true;
            this.lblDP.Location = new System.Drawing.Point(13, 95);
            this.lblDP.Name = "lblDP";
            this.lblDP.Size = new System.Drawing.Size(96, 13);
            this.lblDP.TabIndex = 101;
            this.lblDP.Text = "Data de produção";
            // 
            // lblAgrupador
            // 
            this.lblAgrupador.AutoSize = true;
            this.lblAgrupador.Location = new System.Drawing.Point(13, 65);
            this.lblAgrupador.Name = "lblAgrupador";
            this.lblAgrupador.Size = new System.Drawing.Size(54, 13);
            this.lblAgrupador.TabIndex = 100;
            this.lblAgrupador.Text = "Agrupador";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(13, 5);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(21, 13);
            this.lblID.TabIndex = 123;
            this.lblID.Text = "ID";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(40, 2);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(90, 20);
            this.txtID.TabIndex = 124;
            // 
            // SuggestionPickerDocumento
            // 
            this.suggestionPickerDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.suggestionPickerDocumento.Correspondencia = null;
            this.suggestionPickerDocumento.IsIconComposed = true;
            this.suggestionPickerDocumento.Location = new System.Drawing.Point(138, 2);
            this.suggestionPickerDocumento.Name = "SuggestionPickerDocumento";
            this.suggestionPickerDocumento.Size = new System.Drawing.Size(378, 24);
            this.suggestionPickerDocumento.TabIndex = 106;
            // 
            // propriedadeSuggestionPickerAgrupador
            // 
            this.propriedadeSuggestionPickerAgrupador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propriedadeSuggestionPickerAgrupador.Propriedade = null;
            this.propriedadeSuggestionPickerAgrupador.IsIconComposed = false;
            this.propriedadeSuggestionPickerAgrupador.Location = new System.Drawing.Point(138, 62);
            this.propriedadeSuggestionPickerAgrupador.Name = "propriedadeSuggestionPickerAgrupador";
            this.propriedadeSuggestionPickerAgrupador.Size = new System.Drawing.Size(378, 24);
            this.propriedadeSuggestionPickerAgrupador.TabIndex = 121;
            // 
            // SuggestionPickerLstOnomastico
            // 
            this.suggestionPickerLstOnomastico.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.suggestionPickerLstOnomastico.CorrespondenciaDoc = null;
            this.suggestionPickerLstOnomastico.Location = new System.Drawing.Point(138, 242);
            this.suggestionPickerLstOnomastico.LstCorrespondencia = null;
            this.suggestionPickerLstOnomastico.Name = "SuggestionPickerLstOnomastico";
            this.suggestionPickerLstOnomastico.Size = new System.Drawing.Size(378, 54);
            this.suggestionPickerLstOnomastico.TabIndex = 122;
            // 
            // propriedadeSuggestionPickerCodPostalLoc
            // 
            this.propriedadeSuggestionPickerCodPostalLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propriedadeSuggestionPickerCodPostalLoc.Propriedade = null;
            this.propriedadeSuggestionPickerCodPostalLoc.IsIconComposed = false;
            this.propriedadeSuggestionPickerCodPostalLoc.Location = new System.Drawing.Point(138, 362);
            this.propriedadeSuggestionPickerCodPostalLoc.Name = "propriedadeSuggestionPickerCodPostalLoc";
            this.propriedadeSuggestionPickerCodPostalLoc.Size = new System.Drawing.Size(378, 24);
            this.propriedadeSuggestionPickerCodPostalLoc.TabIndex = 121;
            // 
            // propriedadeSuggestionPickerNumFracRefPred
            // 
            this.propriedadeSuggestionPickerNumFracRefPred.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propriedadeSuggestionPickerNumFracRefPred.Propriedade = null;
            this.propriedadeSuggestionPickerNumFracRefPred.IsIconComposed = false;
            this.propriedadeSuggestionPickerNumFracRefPred.Location = new System.Drawing.Point(138, 332);
            this.propriedadeSuggestionPickerNumFracRefPred.Name = "propriedadeSuggestionPickerNumFracRefPred";
            this.propriedadeSuggestionPickerNumFracRefPred.Size = new System.Drawing.Size(378, 24);
            this.propriedadeSuggestionPickerNumFracRefPred.TabIndex = 118;
            // 
            // propriedadeSuggestionPickerNotas
            // 
            this.propriedadeSuggestionPickerNotas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propriedadeSuggestionPickerNotas.Propriedade = null;
            this.propriedadeSuggestionPickerNotas.IsIconComposed = false;
            this.propriedadeSuggestionPickerNotas.Location = new System.Drawing.Point(138, 212);
            this.propriedadeSuggestionPickerNotas.Name = "propriedadeSuggestionPickerNotas";
            this.propriedadeSuggestionPickerNotas.Size = new System.Drawing.Size(378, 24);
            this.propriedadeSuggestionPickerNotas.TabIndex = 116;
            // 
            // SuggestionPickerToponimia
            // 
            this.suggestionPickerToponimia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.suggestionPickerToponimia.Correspondencia = null;
            this.suggestionPickerToponimia.IsIconComposed = true;
            this.suggestionPickerToponimia.Location = new System.Drawing.Point(138, 302);
            this.suggestionPickerToponimia.Name = "SuggestionPickerToponimia";
            this.suggestionPickerToponimia.Size = new System.Drawing.Size(378, 24);
            this.suggestionPickerToponimia.TabIndex = 113;
            // 
            // SuggestionPickerTipologia
            // 
            this.suggestionPickerTipologia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.suggestionPickerTipologia.Correspondencia = null;
            this.suggestionPickerTipologia.IsIconComposed = true;
            this.suggestionPickerTipologia.Location = new System.Drawing.Point(138, 152);
            this.suggestionPickerTipologia.Name = "SuggestionPickerTipologia";
            this.suggestionPickerTipologia.Size = new System.Drawing.Size(378, 24);
            this.suggestionPickerTipologia.TabIndex = 112;
            // 
            // SuggestionPickerIdeografico
            // 
            this.propriedadeSuggestionPickerIdeografico.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propriedadeSuggestionPickerIdeografico.Propriedade = null;
            this.propriedadeSuggestionPickerIdeografico.IsIconComposed = false;
            this.propriedadeSuggestionPickerIdeografico.Location = new System.Drawing.Point(138, 182);
            this.propriedadeSuggestionPickerIdeografico.Name = "propriedadeSuggestionPickerIdeografico";
            this.propriedadeSuggestionPickerIdeografico.Size = new System.Drawing.Size(378, 24);
            this.propriedadeSuggestionPickerIdeografico.TabIndex = 111;
            // 
            // propriedadeSuggestionPickerData
            // 
            this.propriedadeSuggestionPickerData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propriedadeSuggestionPickerData.Propriedade = null;
            this.propriedadeSuggestionPickerData.IsIconComposed = false;
            this.propriedadeSuggestionPickerData.Location = new System.Drawing.Point(138, 92);
            this.propriedadeSuggestionPickerData.Name = "propriedadeSuggestionPickerData";
            this.propriedadeSuggestionPickerData.Size = new System.Drawing.Size(378, 24);
            this.propriedadeSuggestionPickerData.TabIndex = 104;
            // 
            // propriedadeSuggestionPickerNumEsp
            // 
            this.propriedadeSuggestionPickerNumEsp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propriedadeSuggestionPickerNumEsp.Propriedade = null;
            this.propriedadeSuggestionPickerNumEsp.IsIconComposed = false;
            this.propriedadeSuggestionPickerNumEsp.Location = new System.Drawing.Point(138, 122);
            this.propriedadeSuggestionPickerNumEsp.Name = "propriedadeSuggestionPickerNumEsp";
            this.propriedadeSuggestionPickerNumEsp.Size = new System.Drawing.Size(378, 24);
            this.propriedadeSuggestionPickerNumEsp.TabIndex = 103;
            // 
            // ControlDocumentoGisa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.suggestionPickerDocumento);
            this.Controls.Add(this.suggestionPickerLstOnomastico);
            this.Controls.Add(this.propriedadeSuggestionPickerCodPostalLoc);
            this.Controls.Add(this.lblCodPostalLoc);
            this.Controls.Add(this.lblOnomastico);
            this.Controls.Add(this.propriedadeSuggestionPickerNumFracRefPred);
            this.Controls.Add(this.lblMoradasNumRef);
            this.Controls.Add(this.propriedadeSuggestionPickerNotas);
            this.Controls.Add(this.lblNotas);
            this.Controls.Add(this.suggestionPickerToponimia);
            this.Controls.Add(this.suggestionPickerTipologia);
            this.Controls.Add(this.propriedadeSuggestionPickerIdeografico);
            this.Controls.Add(this.lblToponimia);
            this.Controls.Add(this.lblTipologia);
            this.Controls.Add(this.lblIdeografico);
            this.Controls.Add(this.lblNumeroEspecifico);
            this.Controls.Add(this.propriedadeSuggestionPickerData);
            this.Controls.Add(this.propriedadeSuggestionPickerNumEsp);
            this.Controls.Add(this.lblDP);
            this.Controls.Add(this.lblAgrupador);
            this.Controls.Add(this.propriedadeSuggestionPickerAgrupador);
            this.Name = "ControlDocumentoGisa";
            this.Size = new System.Drawing.Size(528, 427);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList ilDocTypes;
        private CorrespondenciaSuggestionPicker suggestionPickerDocumento;
        private CorrespondenciaSuggestionPickerList suggestionPickerLstOnomastico;
        private PropriedadeSuggestionPickerTemplate<string> propriedadeSuggestionPickerCodPostalLoc;
        private PropriedadeSuggestionPickerTemplate<string> propriedadeSuggestionPickerAgrupador;
        private System.Windows.Forms.Label lblAgrupador;
        private System.Windows.Forms.Label lblCodPostalLoc;
        private System.Windows.Forms.Label lblOnomastico;
        private PropriedadeSuggestionPickerTemplate<string> propriedadeSuggestionPickerNumFracRefPred;
        private System.Windows.Forms.Label lblMoradasNumRef;
        private PropriedadeSuggestionPickerTemplate<string> propriedadeSuggestionPickerNotas;
        private System.Windows.Forms.Label lblNotas;
        private CorrespondenciaSuggestionPicker suggestionPickerToponimia;
        private CorrespondenciaSuggestionPicker suggestionPickerTipologia;
        private PropriedadeSuggestionPickerTemplate<string> propriedadeSuggestionPickerIdeografico;
        private System.Windows.Forms.Label lblToponimia;
        private System.Windows.Forms.Label lblTipologia;
        private System.Windows.Forms.Label lblIdeografico;
        private System.Windows.Forms.Label lblNumeroEspecifico;
        private PropriedadeSuggestionPickerTemplate<DataIncompleta> propriedadeSuggestionPickerData;
        private PropriedadeSuggestionPickerTemplate<string> propriedadeSuggestionPickerNumEsp;
        private System.Windows.Forms.Label lblDP;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtID;
    }
}
