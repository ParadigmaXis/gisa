using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA
{
	public class PanelIdentificacao : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelIdentificacao() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
			ControloLocalizacao1.grpLocalizacao.Text = "1.2. Título / Localização na estrutura arquivística";
		}

		//Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		internal System.Windows.Forms.GroupBox grpDataProducao;
        internal System.Windows.Forms.ComboBox cbIntervalo;
		internal GISA.AttributableDate AttributableDateInicio;
        internal GISA.AttributableDate AttributableDateFim;
        private Button button1;
        private Button button2;
        internal GroupBox GroupBox1;
        internal TextBox txtNivelDeDescricao;
        private Panel pnlBottom;
        private Panel pnlMiddle;
        private Panel pnlTop;
        internal GroupBox grpCodigo;
        private Label lblIdentificador;
        internal TextBox txtIdentificador;
        internal ListBox lstBoxOutrosCodigos;
        private Label lblOutrosCodigos;
        private Label lblCodigoLocalizacao;
        internal ListBox lstBoxCodigoDeReferencia;
        private Label lblAgrupador;
        internal TextBox txtAgrupador;
        private ContextMenuStrip lstBoxContextMenuStrip;
        private ToolStripMenuItem toolStripMenuItemCopiar;
		protected internal GISA.Controls.Localizacao.ControloLocalizacao ControloLocalizacao1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.grpDataProducao = new System.Windows.Forms.GroupBox();
            this.AttributableDateFim = new GISA.AttributableDate();
            this.AttributableDateInicio = new GISA.AttributableDate();
            this.cbIntervalo = new System.Windows.Forms.ComboBox();
            this.ControloLocalizacao1 = new GISA.Controls.Localizacao.ControloLocalizacao();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNivelDeDescricao = new System.Windows.Forms.TextBox();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlMiddle = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.grpCodigo = new System.Windows.Forms.GroupBox();
            this.lblAgrupador = new System.Windows.Forms.Label();
            this.txtAgrupador = new System.Windows.Forms.TextBox();
            this.lblIdentificador = new System.Windows.Forms.Label();
            this.txtIdentificador = new System.Windows.Forms.TextBox();
            this.lstBoxOutrosCodigos = new System.Windows.Forms.ListBox();
            this.lblOutrosCodigos = new System.Windows.Forms.Label();
            this.lblCodigoLocalizacao = new System.Windows.Forms.Label();
            this.lstBoxCodigoDeReferencia = new System.Windows.Forms.ListBox();
            this.lstBoxContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemCopiar = new System.Windows.Forms.ToolStripMenuItem();
            this.grpDataProducao.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlMiddle.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.grpCodigo.SuspendLayout();
            this.lstBoxContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDataProducao
            // 
            this.grpDataProducao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDataProducao.Controls.Add(this.AttributableDateFim);
            this.grpDataProducao.Controls.Add(this.AttributableDateInicio);
            this.grpDataProducao.Controls.Add(this.cbIntervalo);
            this.grpDataProducao.Location = new System.Drawing.Point(8, 6);
            this.grpDataProducao.Name = "grpDataProducao";
            this.grpDataProducao.Size = new System.Drawing.Size(686, 64);
            this.grpDataProducao.TabIndex = 3;
            this.grpDataProducao.TabStop = false;
            this.grpDataProducao.Text = "1.3. Data(s) de produção";
            // 
            // AttributableDateFim
            // 
            this.AttributableDateFim.Location = new System.Drawing.Point(373, 13);
            this.AttributableDateFim.Name = "AttributableDateFim";
            this.AttributableDateFim.Size = new System.Drawing.Size(180, 44);
            this.AttributableDateFim.TabIndex = 3;
            this.AttributableDateFim.Title = "Fim";
            // 
            // AttributableDateInicio
            // 
            this.AttributableDateInicio.Location = new System.Drawing.Point(184, 13);
            this.AttributableDateInicio.Name = "AttributableDateInicio";
            this.AttributableDateInicio.Size = new System.Drawing.Size(183, 44);
            this.AttributableDateInicio.TabIndex = 2;
            this.AttributableDateInicio.Title = "Início";
            // 
            // cbIntervalo
            // 
            this.cbIntervalo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIntervalo.Items.AddRange(new object[] {
            "",
            "Antes de",
            "Depois de",
            "Cerca de"});
            this.cbIntervalo.Location = new System.Drawing.Point(16, 26);
            this.cbIntervalo.Name = "cbIntervalo";
            this.cbIntervalo.Size = new System.Drawing.Size(144, 21);
            this.cbIntervalo.TabIndex = 1;
            // 
            // ControloLocalizacao1
            // 
            this.ControloLocalizacao1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ControloLocalizacao1.Location = new System.Drawing.Point(8, 0);
            this.ControloLocalizacao1.Name = "ControloLocalizacao1";
            this.ControloLocalizacao1.Size = new System.Drawing.Size(686, 108);
            this.ControloLocalizacao1.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(103, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.Controls.Add(this.txtNivelDeDescricao);
            this.GroupBox1.Location = new System.Drawing.Point(8, 76);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(686, 46);
            this.GroupBox1.TabIndex = 6;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "1.4. Nível de descrição";
            // 
            // txtNivelDeDescricao
            // 
            this.txtNivelDeDescricao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNivelDeDescricao.Location = new System.Drawing.Point(8, 16);
            this.txtNivelDeDescricao.Name = "txtNivelDeDescricao";
            this.txtNivelDeDescricao.ReadOnly = true;
            this.txtNivelDeDescricao.Size = new System.Drawing.Size(670, 20);
            this.txtNivelDeDescricao.TabIndex = 1;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.GroupBox1);
            this.pnlBottom.Controls.Add(this.grpDataProducao);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 293);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(700, 133);
            this.pnlBottom.TabIndex = 7;
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Controls.Add(this.ControloLocalizacao1);
            this.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMiddle.Location = new System.Drawing.Point(0, 179);
            this.pnlMiddle.Name = "pnlMiddle";
            this.pnlMiddle.Size = new System.Drawing.Size(700, 114);
            this.pnlMiddle.TabIndex = 8;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.grpCodigo);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(700, 179);
            this.pnlTop.TabIndex = 9;
            // 
            // grpCodigo
            // 
            this.grpCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCodigo.Controls.Add(this.lblAgrupador);
            this.grpCodigo.Controls.Add(this.txtAgrupador);
            this.grpCodigo.Controls.Add(this.lblIdentificador);
            this.grpCodigo.Controls.Add(this.txtIdentificador);
            this.grpCodigo.Controls.Add(this.lstBoxOutrosCodigos);
            this.grpCodigo.Controls.Add(this.lblOutrosCodigos);
            this.grpCodigo.Controls.Add(this.lblCodigoLocalizacao);
            this.grpCodigo.Controls.Add(this.lstBoxCodigoDeReferencia);
            this.grpCodigo.Location = new System.Drawing.Point(8, 0);
            this.grpCodigo.Name = "grpCodigo";
            this.grpCodigo.Size = new System.Drawing.Size(686, 167);
            this.grpCodigo.TabIndex = 1;
            this.grpCodigo.TabStop = false;
            this.grpCodigo.Text = "1.1. Código(s) de referência";
            // 
            // lblAgrupador
            // 
            this.lblAgrupador.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAgrupador.AutoSize = true;
            this.lblAgrupador.Location = new System.Drawing.Point(297, 143);
            this.lblAgrupador.Name = "lblAgrupador";
            this.lblAgrupador.Size = new System.Drawing.Size(59, 13);
            this.lblAgrupador.TabIndex = 7;
            this.lblAgrupador.Text = "Agrupador:";
            // 
            // txtAgrupador
            // 
            this.txtAgrupador.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAgrupador.Location = new System.Drawing.Point(362, 140);
            this.txtAgrupador.Name = "txtAgrupador";
            this.txtAgrupador.Size = new System.Drawing.Size(318, 20);
            this.txtAgrupador.TabIndex = 6;
            // 
            // lblIdentificador
            // 
            this.lblIdentificador.AutoSize = true;
            this.lblIdentificador.Location = new System.Drawing.Point(6, 143);
            this.lblIdentificador.Name = "lblIdentificador";
            this.lblIdentificador.Size = new System.Drawing.Size(71, 13);
            this.lblIdentificador.TabIndex = 5;
            this.lblIdentificador.Text = "Identificador: ";
            // 
            // txtIdentificador
            // 
            this.txtIdentificador.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIdentificador.Location = new System.Drawing.Point(104, 140);
            this.txtIdentificador.Name = "txtIdentificador";
            this.txtIdentificador.ReadOnly = true;
            this.txtIdentificador.Size = new System.Drawing.Size(187, 20);
            this.txtIdentificador.TabIndex = 4;
            // 
            // lstBoxOutrosCodigos
            // 
            this.lstBoxOutrosCodigos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBoxOutrosCodigos.BackColor = System.Drawing.SystemColors.Control;
            this.lstBoxOutrosCodigos.ContextMenuStrip = this.lstBoxContextMenuStrip;
            this.lstBoxOutrosCodigos.Location = new System.Drawing.Point(104, 78);
            this.lstBoxOutrosCodigos.Name = "lstBoxOutrosCodigos";
            this.lstBoxOutrosCodigos.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstBoxOutrosCodigos.Size = new System.Drawing.Size(576, 56);
            this.lstBoxOutrosCodigos.Sorted = true;
            this.lstBoxOutrosCodigos.TabIndex = 3;
            // 
            // lblOutrosCodigos
            // 
            this.lblOutrosCodigos.AutoSize = true;
            this.lblOutrosCodigos.Location = new System.Drawing.Point(6, 78);
            this.lblOutrosCodigos.Name = "lblOutrosCodigos";
            this.lblOutrosCodigos.Size = new System.Drawing.Size(84, 13);
            this.lblOutrosCodigos.TabIndex = 2;
            this.lblOutrosCodigos.Text = "Outros códigos: ";
            // 
            // lblCodigoLocalizacao
            // 
            this.lblCodigoLocalizacao.AutoSize = true;
            this.lblCodigoLocalizacao.Location = new System.Drawing.Point(6, 16);
            this.lblCodigoLocalizacao.Name = "lblCodigoLocalizacao";
            this.lblCodigoLocalizacao.Size = new System.Drawing.Size(92, 13);
            this.lblCodigoLocalizacao.TabIndex = 1;
            this.lblCodigoLocalizacao.Text = "Código completo: ";
            // 
            // lstBoxCodigoDeReferencia
            // 
            this.lstBoxCodigoDeReferencia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBoxCodigoDeReferencia.BackColor = System.Drawing.SystemColors.Control;
            this.lstBoxCodigoDeReferencia.ContextMenuStrip = this.lstBoxContextMenuStrip;
            this.lstBoxCodigoDeReferencia.Location = new System.Drawing.Point(104, 16);
            this.lstBoxCodigoDeReferencia.Name = "lstBoxCodigoDeReferencia";
            this.lstBoxCodigoDeReferencia.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstBoxCodigoDeReferencia.Size = new System.Drawing.Size(576, 56);
            this.lstBoxCodigoDeReferencia.Sorted = true;
            this.lstBoxCodigoDeReferencia.TabIndex = 0;
            // 
            // lstBoxContextMenuStrip
            // 
            this.lstBoxContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopiar});
            this.lstBoxContextMenuStrip.Name = "contextMenuStrip1";
            this.lstBoxContextMenuStrip.Size = new System.Drawing.Size(153, 48);
            this.lstBoxContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.lstBoxContextMenuStrip_Opening);
            // 
            // toolStripMenuItemCopiar
            // 
            this.toolStripMenuItemCopiar.Name = "toolStripMenuItemCopiar";
            this.toolStripMenuItemCopiar.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemCopiar.Text = "Copiar";
            this.toolStripMenuItemCopiar.Click += new System.EventHandler(this.toolStripMenuItemCopiar_Click);
            // 
            // PanelIdentificacao
            // 
            this.Controls.Add(this.pnlMiddle);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlBottom);
            this.Name = "PanelIdentificacao";
            this.Size = new System.Drawing.Size(700, 426);
            this.grpDataProducao.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlMiddle.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.grpCodigo.ResumeLayout(false);
            this.grpCodigo.PerformLayout();
            this.lstBoxContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

		protected GISADataset.FRDBaseRow CurrentFRDBase;
		private GISADataset.SFRDDatasProducaoRow CurrentSFRDDatasProducao;

		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			CurrentFRDBase = (GISADataset.FRDBaseRow)CurrentDataRow;

			try
			{
				FRDRule.Current.LoadIdentificacaoData(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, conn);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw;
			}
			IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = false;
			PopulateDatasProducao();

			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
				lstBoxCodigoDeReferencia.Items.Clear();
				ArrayList codigos = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.GetCodigoOfNivel(CurrentFRDBase.NivelRow.ID, ho.Connection);
				foreach (string codigo in codigos)
				{
					lstBoxCodigoDeReferencia.Items.Add(codigo);
				}

                lstBoxOutrosCodigos.Items.AddRange(GisaDataSetHelper.GetInstance().Codigo.Cast<GISADataset.CodigoRow>().Where(r => r.IDFRDBase == CurrentFRDBase.ID).Select(r => r.Codigo).ToArray());

				txtNivelDeDescricao.Text = string.Empty;
				foreach (GISADataset.RelacaoHierarquicaRow rhRow in CurrentFRDBase.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica())
				{

					// popular o nivel de descrição
					if (txtNivelDeDescricao.Text.Length > 0)
					{
						txtNivelDeDescricao.Text += "; ";
					}
					txtNivelDeDescricao.Text += rhRow.TipoNivelRelacionadoRow.Designacao;
				}

				// popular a árvore de "localização na estrutura"
                ControloLocalizacao1.BuildTree(CurrentFRDBase.NivelRow.ID, ho.Connection, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);

                //Preencher Identificador
                bool visible = CurrentFRDBase.NivelRow.IDTipoNivel == 3;
                this.lblAgrupador.Visible = visible;
                this.txtAgrupador.Visible = visible;
                this.lblIdentificador.Visible = visible;
                this.txtIdentificador.Visible = visible;
                this.lblOutrosCodigos.Visible = visible;
                this.lstBoxOutrosCodigos.Visible = visible;

                if (CurrentFRDBase.NivelRow.IDTipoNivel == 3)
                {
                    this.txtIdentificador.Text = CurrentFRDBase.IDNivel.ToString();
                    this.txtAgrupador.Text = CurrentFRDBase.GetSFRDAgrupadorRows()[0].Agrupador;

                    this.pnlTop.Height = this.txtIdentificador.Top + this.txtIdentificador.Height + 18;
                }
                else
                {
                    this.txtIdentificador.Text = string.Empty;
                    this.txtAgrupador.Text = string.Empty;
                    this.lstBoxOutrosCodigos.Items.Clear();

                    this.pnlTop.Height = this.lstBoxOutrosCodigos.Top + 12;
                }
			}
			finally
			{
				ho.Dispose();
			}
			IsPopulated = true;
		}

		private void PopulateDatasProducao()
		{
			byte[] Versao = null;
			if (CurrentFRDBase.GetSFRDDatasProducaoRows().Length == 0)
			{
				CurrentSFRDDatasProducao = GisaDataSetHelper.GetInstance().SFRDDatasProducao. AddSFRDDatasProducaoRow(CurrentFRDBase, "", "", "", "", false, "", "", "", "", false, Versao, 0);
			}
			else
			{
				CurrentSFRDDatasProducao = CurrentFRDBase.GetSFRDDatasProducaoRows()[0];
			}

			// A partir do momento que existe uma EP as datas de produção 
			// são calculadas com base na sua data de existencia
			if (CurrentFRDBase.NivelRow.CatCode.Trim().Equals("CA"))
			{
				// TODO: obter datas de produção das datas de existencia e defini-las como não editáveis
				//CalcularDatasProducaoPelaDataDeExistencia(CurrentFRDBase)
			}
			else
			{
				bool InGestaoNaoIntegrada = ! (((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Select()[0])).GestaoIntegrada);
				// A partir do momento em que se exclui a necessidade das 
				// datas de produção serem calculadas das datas de existência 
				// de uma EP, o único caso em que as datas de produção podem 
				// ser calculadas (das UFs agregadas) é nos níveis documentais 
				// caso operemos em modo de "Gestão integrada".
				if (! CurrentFRDBase.NivelRow.TipoNivelRow.IsDocument || InGestaoNaoIntegrada)
				{
					// TODO: permitir definição manual das datas de produção
				}
				else if (CurrentFRDBase.NivelRow.TipoNivelRow.IsDocument)
				{
					// TODO: Nível documental em gestão integrada
					// TODO: calcular datas de produção com base nas UFs agregadas e defini-las como não editáveis
					// TODO: deverá ser algo parecido com o que segue em baixo, no entanto, será conveniente destacar o calculo com base nas UFs por forma a poder ser utilizado noutros pontos (passar esse calculo para SP?). CalcularDatasProducaoPelasUfsAgregadas()
				}
			}
		
			Populate();

			AttributableDateInicio.chkAtribuida.Checked = CurrentSFRDDatasProducao.InicioAtribuida;
			AttributableDateFim.chkAtribuida.Checked = CurrentSFRDDatasProducao.FimAtribuida;
		}

		// devolve a data mais vaga das datas ordenadas especificadas. as datas não preenchidas são ignoradas.
		// "datas" é um ArrayList de string(). cada uma das datas é um string() de 3 posicoes
		private string[] GetVaguerFromTopDates(ArrayList datas)
		{
			string[] DataMaisVaga = null;
			if (datas.Count == 0)
			{
				throw new ArgumentException("'datas' must have at least one item");
			}
			else if (datas.Count > 0)
			{
				int i = 0;
				int count = datas.Count;
				string[] dataActual = null;
		
				DataMaisVaga = (string[])(datas[i]);
				i = i + 1;

				while (i < count)
				{
					dataActual = (string[])(datas[i]);

					ControloDateValidation.PartialComparisonResult result = ControloDateValidation.ComparePartialDate(DataMaisVaga, dataActual);
					if (result == ControloDateValidation.PartialComparisonResult.Equal || result == ControloDateValidation.PartialComparisonResult.PartialyEqual)
					{
						// no caso de as datas serem parcialmente iguais é necessário
						// escolher a que for mais vaga que, para este efeito, é a 
						// mais(correcta)
						switch (ControloDateValidation.IsMoreCompleteThan(DataMaisVaga, dataActual))
						{
							case ControloDateValidation.ComparisonResult.Equal:
								// sao igualmente vagas. ignorar
							break;
							case ControloDateValidation.ComparisonResult.FirstOne:
								// "DataMaisVaga" é a data mais completa, trocamos para ficarmos com a data mais vaga
								DataMaisVaga = dataActual;
								break;
							case ControloDateValidation.ComparisonResult.SecondOne:
								// "DataMaisVaga" já é a data mais vaga. ignorar
							break;
						}
					}
					else
					{
						// só interessa procurar a data mais vaga de entre as datas 
						// parcialmente iguais.
						break;
					}
					i = i + 1;
				}
			}
			return DataMaisVaga;
		}

		public static GISATreeNode FindTreeNodeByTag(TreeNodeCollection Nodes, object nRow)
		{
			return null;
		}

		public override void ViewToModel()
		{
			if (CurrentFRDBase == null || CurrentFRDBase.RowState == DataRowState.Detached || ! IsLoaded)
				return;

            if (cbIntervalo.SelectedIndex == -1)
                CurrentSFRDDatasProducao["InicioTexto"] = DBNull.Value;
            else
                CurrentSFRDDatasProducao.InicioTexto = cbIntervalo.SelectedItem.ToString();

			CurrentSFRDDatasProducao.InicioAtribuida = AttributableDateInicio.chkAtribuida.Checked;
			CurrentSFRDDatasProducao.FimAtribuida = AttributableDateFim.chkAtribuida.Checked;

			// interessa guardar as datas apenas se se tratarem das datas do nível em causa.
			// não devem ser consideradas caso se tratem de datas herdadas das unidades físicas
			if (grpDataProducao.Enabled == true)
			{
				if (AttributableDateInicio.dtData.ValueYear != GisaDataSetHelper.GetDBNullableText(CurrentSFRDDatasProducao, "InicioAno") || AttributableDateInicio.dtData.ValueMonth != GisaDataSetHelper.GetDBNullableText(CurrentSFRDDatasProducao, "InicioMes") || AttributableDateInicio.dtData.ValueDay != GisaDataSetHelper.GetDBNullableText(CurrentSFRDDatasProducao, "InicioDia"))
				{
					CurrentSFRDDatasProducao.InicioAno = AttributableDateInicio.dtData.ValueYear;
					CurrentSFRDDatasProducao.InicioMes = AttributableDateInicio.dtData.ValueMonth;
					CurrentSFRDDatasProducao.InicioDia = AttributableDateInicio.dtData.ValueDay;
				}
				if (AttributableDateFim.dtData.ValueYear != GisaDataSetHelper.GetDBNullableText(CurrentSFRDDatasProducao, "FimAno") || AttributableDateFim.dtData.ValueMonth != GisaDataSetHelper.GetDBNullableText(CurrentSFRDDatasProducao, "FimMes") || AttributableDateFim.dtData.ValueDay != GisaDataSetHelper.GetDBNullableText(CurrentSFRDDatasProducao, "FimDia"))
				{
					CurrentSFRDDatasProducao.FimAno = AttributableDateFim.dtData.ValueYear;
					CurrentSFRDDatasProducao.FimMes = AttributableDateFim.dtData.ValueMonth;
					CurrentSFRDDatasProducao.FimDia = AttributableDateFim.dtData.ValueDay;
				}
			}
            if (CurrentFRDBase.NivelRow.IDTipoNivel == TipoNivel.DOCUMENTAL)
                CurrentFRDBase.GetSFRDAgrupadorRows()[0].Agrupador = txtAgrupador.Text;
		}

		private void Populate()
		{
			if (CurrentSFRDDatasProducao.IsInicioTextoNull())
				cbIntervalo.SelectedItem = -1;
			else
				cbIntervalo.SelectedItem = CurrentSFRDDatasProducao.InicioTexto;

			// interessa apresentar as datas apenas se se tratarem das datas do nível em causa,
			// não devem ser consideradas caso se tratem de datas herdadas das unidades físicas
			if (grpDataProducao.Enabled == true)
			{
				AttributableDateInicio.dtData.ValueYear = GisaDataSetHelper.GetDBNullableText(CurrentSFRDDatasProducao, "InicioAno");
				AttributableDateInicio.dtData.ValueMonth = GisaDataSetHelper.GetDBNullableText(CurrentSFRDDatasProducao, "InicioMes");
				AttributableDateInicio.dtData.ValueDay = GisaDataSetHelper.GetDBNullableText(CurrentSFRDDatasProducao, "InicioDia");
				AttributableDateFim.dtData.ValueYear = GisaDataSetHelper.GetDBNullableText(CurrentSFRDDatasProducao, "FimAno");
				AttributableDateFim.dtData.ValueMonth = GisaDataSetHelper.GetDBNullableText(CurrentSFRDDatasProducao, "FimMes");
				AttributableDateFim.dtData.ValueDay = GisaDataSetHelper.GetDBNullableText(CurrentSFRDDatasProducao, "FimDia");
			}
		}

		public override void Deactivate()
		{
            GUIHelper.GUIHelper.clearField(cbIntervalo);
            GUIHelper.GUIHelper.clearField(AttributableDateInicio.dtData);
            GUIHelper.GUIHelper.clearField(AttributableDateFim.dtData);
            GUIHelper.GUIHelper.clearField(AttributableDateInicio.chkAtribuida);
            GUIHelper.GUIHelper.clearField(AttributableDateFim.chkAtribuida);
            GUIHelper.GUIHelper.clearField(txtNivelDeDescricao);
            GUIHelper.GUIHelper.clearField(txtAgrupador);
            GUIHelper.GUIHelper.clearField(this.lstBoxOutrosCodigos);
			ControloLocalizacao1.ClearTree();
            GUIHelper.GUIHelper.clearField(lstBoxCodigoDeReferencia);
			CurrentFRDBase = null;
			CurrentSFRDDatasProducao = null;
		}

		// recalcular datas de producao quando o painel é apresentado. podem 
		// já ter sido agragadas e/ou desagregadas unidades físicas.
		public override void OnShowPanel()
		{
			if (CurrentFRDBase == null || CurrentFRDBase.RowState == DataRowState.Detached || CurrentSFRDDatasProducao == null)
                return;

			PopulateDatasProducao();
		}

		public override void OnHidePanel()
		{
			// TODO: if seguinte serve exclusivamente para debug - fazer um #if ?
			if (CurrentFRDBase != null && CurrentFRDBase.RowState == DataRowState.Detached)
				Debug.WriteLine("OCORREU SITUAÇÃO DE ERRO NO PAINEL IDENTIFICACAO. EM PRINCIPIO NINGUEM DEU POR ELE.");

			if (CurrentFRDBase == null || CurrentFRDBase.RowState == DataRowState.Detached || CurrentFRDBase.RowState == DataRowState.Detached || CurrentSFRDDatasProducao == null)
				return;

			ViewToModel();
		}

        private void toolStripMenuItemCopiar_Click(object sender, EventArgs e)
        {
            if (currentListBox == null) return;

            ListBox.SelectedObjectCollection items = null;
            var str = new StringBuilder();

            if (currentListBox == lstBoxCodigoDeReferencia)
            {
                items = lstBoxCodigoDeReferencia.SelectedItems;
            }
            else if (currentListBox == lstBoxOutrosCodigos)
            {
                items = lstBoxOutrosCodigos.SelectedItems;
            }

            if (items == null) return;

            foreach (var item in items)
                str.AppendLine(item.ToString());

            Clipboard.SetText(str.ToString());
            currentListBox = null;
        }

        private ListBox currentListBox = null;
        private void lstBoxContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((ContextMenuStrip)sender).SourceControl == lstBoxCodigoDeReferencia)
                currentListBox = lstBoxCodigoDeReferencia;
            else if (((ContextMenuStrip)sender).SourceControl == lstBoxOutrosCodigos)
                currentListBox = lstBoxOutrosCodigos;

            if (currentListBox.SelectedItems.Count == 0)
                e.Cancel = true;
        }
	}
}
