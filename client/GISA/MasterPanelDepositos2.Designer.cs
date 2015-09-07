namespace GISA
{
    partial class MasterPanelDepositos2 : GISA.MasterPanel
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
            this.btnRefresh = new System.Windows.Forms.ToolBarButton();
            this.btnFiltro = new System.Windows.Forms.ToolBarButton();
            this.btnCriar = new System.Windows.Forms.ToolBarButton();
            this.btnApagar = new System.Windows.Forms.ToolBarButton();
            this.btnSeparator2 = new System.Windows.Forms.ToolBarButton();
            this.btnSeparator3 = new System.Windows.Forms.ToolBarButton();
            this.btnEditar = new System.Windows.Forms.ToolBarButton();
            this.grp_InfoMetros = new System.Windows.Forms.GroupBox();
            this.txt_UFsSemLargura = new System.Windows.Forms.TextBox();
            this.lbl_UFsSemLargura = new System.Windows.Forms.Label();
            this.txt_UFsTotais = new System.Windows.Forms.TextBox();
            this.lbl_UFsTotais = new System.Windows.Forms.Label();
            this.zedGraphPieChartControl = new ZedGraph.ZedGraphControl();
            this.txt_metrosLinearesLivres = new System.Windows.Forms.TextBox();
            this.lblLivres = new System.Windows.Forms.Label();
            this.txt_metrosLinearesOcupados = new System.Windows.Forms.TextBox();
            this.lblOcupados = new System.Windows.Forms.Label();
            this.txt_metrosLinearesTotais = new System.Windows.Forms.TextBox();
            this.lbl_totais = new System.Windows.Forms.Label();
            this.depList = new GISA.DepositosList();
            this.pnlToolbarPadding.SuspendLayout();
            this.grp_InfoMetros.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFuncao
            // 
            this.lblFuncao.Location = new System.Drawing.Point(0, 0);
            this.lblFuncao.Size = new System.Drawing.Size(1072, 24);
            this.lblFuncao.Text = "Gestão de depósitos";
            // 
            // ToolBar
            // 
            this.ToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] { this.btnCriar, this.btnEditar, this.btnApagar, this.btnSeparator2, this.btnFiltro});
            this.ToolBar.Size = new System.Drawing.Size(8168, 24);
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
            this.pnlToolbarPadding.Size = new System.Drawing.Size(1072, 28);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Name = "btnRefresh";
            //
			//btnFiltro
			//
			this.btnFiltro.ImageIndex = 3;
			this.btnFiltro.Name = "btnFiltro";
			this.btnFiltro.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            //
            //btnCriar
            //
            this.btnCriar.ImageIndex = 0;
            this.btnCriar.Name = "btnCriar";
            this.btnCriar.ToolTipText = "Criar nova notícia de autoridade";
            //
            //btnApagar
            //
            this.btnApagar.Enabled = false;
            this.btnApagar.ImageIndex = 2;
            this.btnApagar.Name = "btnApagar";
            //
            //btnSeparator2
            //
            this.btnSeparator2.Name = "btnSeparator2";
            this.btnSeparator2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            //
            //btnEditar
            //
            this.btnEditar.ImageIndex = 1;
            this.btnEditar.Name = "btnEditar";
            // 
            // grp_InfoMetros
            // 
            this.grp_InfoMetros.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grp_InfoMetros.Controls.Add(this.txt_UFsSemLargura);
            this.grp_InfoMetros.Controls.Add(this.lbl_UFsSemLargura);
            this.grp_InfoMetros.Controls.Add(this.txt_UFsTotais);
            this.grp_InfoMetros.Controls.Add(this.lbl_UFsTotais);
            this.grp_InfoMetros.Controls.Add(this.zedGraphPieChartControl);
            this.grp_InfoMetros.Controls.Add(this.txt_metrosLinearesLivres);
            this.grp_InfoMetros.Controls.Add(this.lblLivres);
            this.grp_InfoMetros.Controls.Add(this.txt_metrosLinearesOcupados);
            this.grp_InfoMetros.Controls.Add(this.lblOcupados);
            this.grp_InfoMetros.Controls.Add(this.txt_metrosLinearesTotais);
            this.grp_InfoMetros.Controls.Add(this.lbl_totais);
            this.grp_InfoMetros.Location = new System.Drawing.Point(394, 55);
            this.grp_InfoMetros.Name = "grp_InfoMetros";
            this.grp_InfoMetros.Size = new System.Drawing.Size(675, 267);
            this.grp_InfoMetros.TabIndex = 3;
            this.grp_InfoMetros.TabStop = false;
            this.grp_InfoMetros.Text = "Metros lineares";
            // 
            // txt_UFsSemLargura
            // 
            this.txt_UFsSemLargura.Location = new System.Drawing.Point(542, 109);
            this.txt_UFsSemLargura.Name = "txt_UFsSemLargura";
            this.txt_UFsSemLargura.ReadOnly = true;
            this.txt_UFsSemLargura.Size = new System.Drawing.Size(127, 20);
            this.txt_UFsSemLargura.TabIndex = 10;
            // 
            // lbl_UFsSemLargura
            // 
            this.lbl_UFsSemLargura.AutoSize = true;
            this.lbl_UFsSemLargura.Location = new System.Drawing.Point(453, 116);
            this.lbl_UFsSemLargura.Name = "lbl_UFsSemLargura";
            this.lbl_UFsSemLargura.Size = new System.Drawing.Size(83, 13);
            this.lbl_UFsSemLargura.TabIndex = 9;
            this.lbl_UFsSemLargura.Text = "UFs sem largura";
            // 
            // txt_UFsTotais
            // 
            this.txt_UFsTotais.Location = new System.Drawing.Point(542, 85);
            this.txt_UFsTotais.Name = "txt_UFsTotais";
            this.txt_UFsTotais.ReadOnly = true;
            this.txt_UFsTotais.Size = new System.Drawing.Size(127, 20);
            this.txt_UFsTotais.TabIndex = 8;
            // 
            // lbl_UFsTotais
            // 
            this.lbl_UFsTotais.AutoSize = true;
            this.lbl_UFsTotais.Location = new System.Drawing.Point(453, 88);
            this.lbl_UFsTotais.Name = "lbl_UFsTotais";
            this.lbl_UFsTotais.Size = new System.Drawing.Size(86, 13);
            this.lbl_UFsTotais.TabIndex = 7;
            this.lbl_UFsTotais.Text = "UFs em depósito";
            // 
            // zedGraphPieChartControl
            // 
            this.zedGraphPieChartControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphPieChartControl.ForeColor = System.Drawing.SystemColors.Control;
            this.zedGraphPieChartControl.Location = new System.Drawing.Point(9, 19);
            this.zedGraphPieChartControl.Name = "zedGraphPieChartControl";
            this.zedGraphPieChartControl.ScrollGrace = 0;
            this.zedGraphPieChartControl.ScrollMaxX = 0;
            this.zedGraphPieChartControl.ScrollMaxY = 0;
            this.zedGraphPieChartControl.ScrollMaxY2 = 0;
            this.zedGraphPieChartControl.ScrollMinX = 0;
            this.zedGraphPieChartControl.ScrollMinY = 0;
            this.zedGraphPieChartControl.ScrollMinY2 = 0;
            this.zedGraphPieChartControl.Size = new System.Drawing.Size(438, 241);
            this.zedGraphPieChartControl.TabIndex = 6;
            // 
            // txt_metrosLinearesLivres
            // 
            this.txt_metrosLinearesLivres.Location = new System.Drawing.Point(510, 61);
            this.txt_metrosLinearesLivres.Name = "txt_metrosLinearesLivres";
            this.txt_metrosLinearesLivres.ReadOnly = true;
            this.txt_metrosLinearesLivres.Size = new System.Drawing.Size(159, 20);
            this.txt_metrosLinearesLivres.TabIndex = 5;
            // 
            // lblLivres
            // 
            this.lblLivres.AutoSize = true;
            this.lblLivres.Location = new System.Drawing.Point(453, 64);
            this.lblLivres.Name = "lblLivres";
            this.lblLivres.Size = new System.Drawing.Size(35, 13);
            this.lblLivres.TabIndex = 4;
            this.lblLivres.Text = "Livres";
            // 
            // txt_metrosLinearesOcupados
            // 
            this.txt_metrosLinearesOcupados.Location = new System.Drawing.Point(510, 37);
            this.txt_metrosLinearesOcupados.Name = "txt_metrosLinearesOcupados";
            this.txt_metrosLinearesOcupados.ReadOnly = true;
            this.txt_metrosLinearesOcupados.Size = new System.Drawing.Size(159, 20);
            this.txt_metrosLinearesOcupados.TabIndex = 3;
            // 
            // lblOcupados
            // 
            this.lblOcupados.AutoSize = true;
            this.lblOcupados.Location = new System.Drawing.Point(453, 38);
            this.lblOcupados.Name = "lblOcupados";
            this.lblOcupados.Size = new System.Drawing.Size(56, 13);
            this.lblOcupados.TabIndex = 2;
            this.lblOcupados.Text = "Ocupados";
            // 
            // txt_metrosLinearesTotais
            // 
            this.txt_metrosLinearesTotais.Location = new System.Drawing.Point(510, 13);
            this.txt_metrosLinearesTotais.Name = "txt_metrosLinearesTotais";
            this.txt_metrosLinearesTotais.ReadOnly = true;
            this.txt_metrosLinearesTotais.Size = new System.Drawing.Size(159, 20);
            this.txt_metrosLinearesTotais.TabIndex = 1;
            // 
            // lbl_totais
            // 
            this.lbl_totais.AutoSize = true;
            this.lbl_totais.Location = new System.Drawing.Point(453, 14);
            this.lbl_totais.Name = "lbl_totais";
            this.lbl_totais.Size = new System.Drawing.Size(36, 13);
            this.lbl_totais.TabIndex = 0;
            this.lbl_totais.Text = "Totais";
            // 
            // depList
            // 
            this.depList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.depList.FilterVisible = true;
            this.depList.Location = new System.Drawing.Point(0, 49);
            this.depList.Name = "depList";
            this.depList.Padding = new System.Windows.Forms.Padding(6);
            this.depList.Size = new System.Drawing.Size(388, 273);
            this.depList.TabIndex = 4;
            // 
            // MasterPanelDepositos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.depList);
            this.Controls.Add(this.grp_InfoMetros);
            this.Name = "MasterPanelDepositos";
            this.Size = new System.Drawing.Size(1072, 322);
            this.Controls.SetChildIndex(this.grp_InfoMetros, 0);
            this.Controls.SetChildIndex(this.lblFuncao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.Controls.SetChildIndex(this.depList, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.grp_InfoMetros.ResumeLayout(false);
            this.grp_InfoMetros.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolBarButton btnRefresh;
        private System.Windows.Forms.ToolBarButton btnFiltro;
        private System.Windows.Forms.ToolBarButton btnApagar;
        private System.Windows.Forms.ToolBarButton btnSeparator2;
        private System.Windows.Forms.ToolBarButton btnSeparator3;
        private System.Windows.Forms.ToolBarButton btnCriar;
        private System.Windows.Forms.ToolBarButton btnEditar;
        private System.Windows.Forms.GroupBox grp_InfoMetros;
        private System.Windows.Forms.TextBox txt_metrosLinearesTotais;
        private System.Windows.Forms.Label lbl_totais;
        private System.Windows.Forms.TextBox txt_metrosLinearesLivres;
        private System.Windows.Forms.Label lblLivres;
        private System.Windows.Forms.TextBox txt_metrosLinearesOcupados;
        private System.Windows.Forms.Label lblOcupados;
        private ZedGraph.ZedGraphControl zedGraphPieChartControl;
        private System.Windows.Forms.Label lbl_UFsTotais;
        private System.Windows.Forms.TextBox txt_UFsTotais;
        private System.Windows.Forms.TextBox txt_UFsSemLargura;
        private System.Windows.Forms.Label lbl_UFsSemLargura;
        private DepositosList depList;
    }
}
