namespace GISA
{
    partial class PanelObjetoDigitalFedora
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
            this.controlObjetoDigital1 = new GISA.ControlObjetoDigital();
            this.grpODFedora = new System.Windows.Forms.GroupBox();
            this.trvODsFedora = new System.Windows.Forms.TreeView();
            this.btnFullScreen = new System.Windows.Forms.Button();
            this.splitContainerOdsReadOnly = new System.Windows.Forms.SplitContainer();
            this.grpVisualizacao = new System.Windows.Forms.GroupBox();
            this.controlFedoraPdfViewer1 = new GISA.ControlFedoraPdfViewer();
            this.grpODFedora.SuspendLayout();
            this.splitContainerOdsReadOnly.Panel1.SuspendLayout();
            this.splitContainerOdsReadOnly.Panel2.SuspendLayout();
            this.splitContainerOdsReadOnly.SuspendLayout();
            this.grpVisualizacao.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlObjetoDigital1
            // 
            this.controlObjetoDigital1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlObjetoDigital1.CurrentAnexo = null;
            this.controlObjetoDigital1.CurrentODComp = null;
            this.controlObjetoDigital1.CurrentODSimples = null;
            this.controlObjetoDigital1.Location = new System.Drawing.Point(3, 3);
            this.controlObjetoDigital1.Name = "controlObjetoDigital1";
            this.controlObjetoDigital1.Size = new System.Drawing.Size(794, 594);
            this.controlObjetoDigital1.TabIndex = 1;
            this.controlObjetoDigital1.Titulo = "";
            this.controlObjetoDigital1.ViewMode = GISA.Model.ObjetoDigitalFedoraHelper.Contexto.nenhum;
            // 
            // grpODFedora
            // 
            this.grpODFedora.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpODFedora.Controls.Add(this.trvODsFedora);
            this.grpODFedora.Controls.Add(this.btnFullScreen);
            this.grpODFedora.Location = new System.Drawing.Point(6, 6);
            this.grpODFedora.Margin = new System.Windows.Forms.Padding(6);
            this.grpODFedora.Name = "grpODFedora";
            this.grpODFedora.Size = new System.Drawing.Size(466, 585);
            this.grpODFedora.TabIndex = 24;
            this.grpODFedora.TabStop = false;
            this.grpODFedora.Text = "Objetos digitais fedora";
            // 
            // trvODsFedora
            // 
            this.trvODsFedora.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trvODsFedora.FullRowSelect = true;
            this.trvODsFedora.HideSelection = false;
            this.trvODsFedora.Location = new System.Drawing.Point(6, 19);
            this.trvODsFedora.Name = "trvODsFedora";
            this.trvODsFedora.Size = new System.Drawing.Size(424, 560);
            this.trvODsFedora.TabIndex = 23;
            this.trvODsFedora.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvODsFedora_BeforeSelect);
            // 
            // btnFullScreen
            // 
            this.btnFullScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFullScreen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFullScreen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFullScreen.Location = new System.Drawing.Point(436, 41);
            this.btnFullScreen.Name = "btnFullScreen";
            this.btnFullScreen.Size = new System.Drawing.Size(24, 24);
            this.btnFullScreen.TabIndex = 22;
            this.btnFullScreen.Click += new System.EventHandler(this.btnFullScreen_Click);
            // 
            // splitContainerOdsReadOnly
            // 
            this.splitContainerOdsReadOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerOdsReadOnly.Location = new System.Drawing.Point(3, 3);
            this.splitContainerOdsReadOnly.Name = "splitContainerOdsReadOnly";
            // 
            // splitContainerOdsReadOnly.Panel1
            // 
            this.splitContainerOdsReadOnly.Panel1.Controls.Add(this.grpODFedora);
            // 
            // splitContainerOdsReadOnly.Panel2
            // 
            this.splitContainerOdsReadOnly.Panel2.Controls.Add(this.grpVisualizacao);
            this.splitContainerOdsReadOnly.Size = new System.Drawing.Size(794, 594);
            this.splitContainerOdsReadOnly.SplitterDistance = 477;
            this.splitContainerOdsReadOnly.TabIndex = 25;
            // 
            // grpVisualizacao
            // 
            this.grpVisualizacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpVisualizacao.Controls.Add(this.controlFedoraPdfViewer1);
            this.grpVisualizacao.Location = new System.Drawing.Point(3, 3);
            this.grpVisualizacao.Name = "grpVisualizacao";
            this.grpVisualizacao.Size = new System.Drawing.Size(307, 588);
            this.grpVisualizacao.TabIndex = 0;
            this.grpVisualizacao.TabStop = false;
            this.grpVisualizacao.Text = "Visualização";
            // 
            // controlFedoraPdfViewer1
            // 
            this.controlFedoraPdfViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlFedoraPdfViewer1.Location = new System.Drawing.Point(6, 19);
            this.controlFedoraPdfViewer1.Name = "controlFedoraPdfViewer1";
            this.controlFedoraPdfViewer1.Qualidade = "Baixa";
            this.controlFedoraPdfViewer1.Size = new System.Drawing.Size(295, 563);
            this.controlFedoraPdfViewer1.TabIndex = 24;
            // 
            // PanelObjetoDigitalFedora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerOdsReadOnly);
            this.Controls.Add(this.controlObjetoDigital1);
            this.Name = "PanelObjetoDigitalFedora";
            this.grpODFedora.ResumeLayout(false);
            this.splitContainerOdsReadOnly.Panel1.ResumeLayout(false);
            this.splitContainerOdsReadOnly.Panel2.ResumeLayout(false);
            this.splitContainerOdsReadOnly.ResumeLayout(false);
            this.grpVisualizacao.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ControlObjetoDigital controlObjetoDigital1;
        private System.Windows.Forms.GroupBox grpODFedora;
        internal System.Windows.Forms.TreeView trvODsFedora;
        internal System.Windows.Forms.Button btnFullScreen;
        private System.Windows.Forms.SplitContainer splitContainerOdsReadOnly;
        private System.Windows.Forms.GroupBox grpVisualizacao;
        private ControlFedoraPdfViewer controlFedoraPdfViewer1;
    }
}
