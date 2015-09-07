using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

namespace GISA
{
	public class PanelOIDimensoesSuporte : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelOIDimensoesSuporte() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
			if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
			{
				GetExtraResources();

                ConfigDataGridView();

                DragDropHandlerUnidFisicas = new NivelDragDrop(dataGridView1, TipoNivel.OUTRO);
                DragDropHandlerUnidFisicas.AcceptNivelRow += AcceptNivelRow;

                dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
                dataGridView1.KeyUp += new KeyEventHandler(dataGridView1_KeyUp);
                dataGridView1.CellMouseMove += new DataGridViewCellMouseEventHandler(dataGridView1_CellMouseMove);

                btnRemove.Click += new EventHandler(btnRemove_Click);

				UpdateListButtonsState();
			}
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
		private System.ComponentModel.IContainer components = null;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		internal System.Windows.Forms.GroupBox grpDimensoes;
		internal System.Windows.Forms.Button btnRemove;
		internal System.Windows.Forms.ColumnHeader chCodigo;
		internal System.Windows.Forms.ColumnHeader chDesignacao;
		internal System.Windows.Forms.ColumnHeader chProducao;
		internal System.Windows.Forms.ColumnHeader chTipo;
		internal System.Windows.Forms.ColumnHeader chDimensoes;
        private GroupBox grpDimensao;
        private TextBox txtDimensao;
        private GroupBox grpUFsAssociadas;
        internal ColumnHeader chEliminado;
        private Label lblCota;
        private TextBox txtCota;
        private Label lblInfoSuporte;
        private DataGridView dataGridView1;
		internal System.Windows.Forms.ColumnHeader chCota;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.grpDimensoes = new System.Windows.Forms.GroupBox();
            this.grpUFsAssociadas = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblInfoSuporte = new System.Windows.Forms.Label();
            this.lblCota = new System.Windows.Forms.Label();
            this.txtCota = new System.Windows.Forms.TextBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.grpDimensao = new System.Windows.Forms.GroupBox();
            this.txtDimensao = new System.Windows.Forms.TextBox();
            this.chCodigo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDesignacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTipo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDimensoes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCota = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chProducao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chEliminado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpDimensoes.SuspendLayout();
            this.grpUFsAssociadas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.grpDimensao.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDimensoes
            // 
            this.grpDimensoes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDimensoes.Controls.Add(this.grpUFsAssociadas);
            this.grpDimensoes.Controls.Add(this.grpDimensao);
            this.grpDimensoes.Location = new System.Drawing.Point(3, 3);
            this.grpDimensoes.Name = "grpDimensoes";
            this.grpDimensoes.Size = new System.Drawing.Size(794, 594);
            this.grpDimensoes.TabIndex = 61;
            this.grpDimensoes.TabStop = false;
            this.grpDimensoes.Text = "1.5. Dimensão e suporte";
            // 
            // grpUFsAssociadas
            // 
            this.grpUFsAssociadas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpUFsAssociadas.Controls.Add(this.dataGridView1);
            this.grpUFsAssociadas.Controls.Add(this.lblInfoSuporte);
            this.grpUFsAssociadas.Controls.Add(this.lblCota);
            this.grpUFsAssociadas.Controls.Add(this.txtCota);
            this.grpUFsAssociadas.Controls.Add(this.btnRemove);
            this.grpUFsAssociadas.Location = new System.Drawing.Point(8, 72);
            this.grpUFsAssociadas.Name = "grpUFsAssociadas";
            this.grpUFsAssociadas.Size = new System.Drawing.Size(780, 516);
            this.grpUFsAssociadas.TabIndex = 67;
            this.grpUFsAssociadas.TabStop = false;
            this.grpUFsAssociadas.Text = "Suporte";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(738, 465);
            this.dataGridView1.TabIndex = 69;
            // 
            // lblInfoSuporte
            // 
            this.lblInfoSuporte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfoSuporte.Location = new System.Drawing.Point(435, 0);
            this.lblInfoSuporte.Name = "lblInfoSuporte";
            this.lblInfoSuporte.Size = new System.Drawing.Size(309, 13);
            this.lblInfoSuporte.TabIndex = 68;
            this.lblInfoSuporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCota
            // 
            this.lblCota.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCota.AutoSize = true;
            this.lblCota.Location = new System.Drawing.Point(6, 493);
            this.lblCota.Name = "lblCota";
            this.lblCota.Size = new System.Drawing.Size(152, 13);
            this.lblCota.TabIndex = 67;
            this.lblCota.Text = "Referência na Unidade Física:";
            // 
            // txtCota
            // 
            this.txtCota.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCota.Enabled = false;
            this.txtCota.Location = new System.Drawing.Point(164, 490);
            this.txtCota.Name = "txtCota";
            this.txtCota.Size = new System.Drawing.Size(580, 20);
            this.txtCota.TabIndex = 66;
            this.txtCota.TextChanged += new System.EventHandler(this.txtCota_TextChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(750, 47);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(24, 24);
            this.btnRemove.TabIndex = 64;
            // 
            // grpDimensao
            // 
            this.grpDimensao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDimensao.Controls.Add(this.txtDimensao);
            this.grpDimensao.Location = new System.Drawing.Point(8, 19);
            this.grpDimensao.Name = "grpDimensao";
            this.grpDimensao.Size = new System.Drawing.Size(780, 47);
            this.grpDimensao.TabIndex = 66;
            this.grpDimensao.TabStop = false;
            this.grpDimensao.Text = "Dimensão";
            // 
            // txtDimensao
            // 
            this.txtDimensao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDimensao.Location = new System.Drawing.Point(6, 19);
            this.txtDimensao.Name = "txtDimensao";
            this.txtDimensao.Size = new System.Drawing.Size(768, 20);
            this.txtDimensao.TabIndex = 0;
            // 
            // PanelOIDimensoesSuporte
            // 
            this.Controls.Add(this.grpDimensoes);
            this.Name = "PanelOIDimensoesSuporte";
            this.grpDimensoes.ResumeLayout(false);
            this.grpUFsAssociadas.ResumeLayout(false);
            this.grpUFsAssociadas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.grpDimensao.ResumeLayout(false);
            this.grpDimensao.PerformLayout();
            this.ResumeLayout(false);

		}

	#endregion

        private void GetExtraResources()
		{
			btnRemove.Image = SharedResourcesOld.CurrentSharedResources.Apagar;

			base.ParentChanged += PanelIdentificacao_ParentChanged;
		}

		// runs only once. sets tooltip as soon as it's parent appears
		private void PanelIdentificacao_ParentChanged(object sender, System.EventArgs e)
		{
			MultiPanel.CurrentToolTip.SetToolTip(btnRemove, SharedResourcesOld.CurrentSharedResources.ApagarString);
			base.ParentChanged -= PanelIdentificacao_ParentChanged;
		}

		private NivelDragDrop DragDropHandlerUnidFisicas;
		protected GISADataset.FRDBaseRow CurrentFRDBase;
        private GISADataset.SFRDDimensaoSuporteRow currentDimensaoSuporteRow = null;
        private const string grpUFsAssociadasTextSeries = "Unidades físicas associadas ({0}); Largura total: {1:0.000}m";
        private const string grpUFsAssociadasTextOther = "Unidades físicas associadas ({0})";
        private string grpUFsAssociadasText = string.Empty;
        private decimal larguraTotal = 0;
        private List<UFRule.UFsAssociadas> ufsAssociadas = new List<UFRule.UFsAssociadas>();
        private DataTable UFsRelacionadasDataTable;

        private const int ID = 0;
        private const int CODIGO = 1;
        private const int DESIGNACAO = 2;
        private const int TIPO = 3;
        private const int DIMENSOES = 4;
        private const int COTA = 5;
        private const int PRODUCAO = 6;
        private const int ELIMINADA = 7;

        private void ConfigDataGridView()
        {
            UFsRelacionadasDataTable = new DataTable();
            UFsRelacionadasDataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            UFsRelacionadasDataTable.Columns.Add(new DataColumn("Código", typeof(string)));
            UFsRelacionadasDataTable.Columns.Add(new DataColumn("Designação", typeof(string)));
            UFsRelacionadasDataTable.Columns.Add(new DataColumn("Tipo", typeof(string)));
            UFsRelacionadasDataTable.Columns.Add(new DataColumn("Dimensões", typeof(string)));
            UFsRelacionadasDataTable.Columns.Add(new DataColumn("Cota", typeof(string)));
            UFsRelacionadasDataTable.Columns.Add(new DataColumn("Produção", typeof(string)));
            UFsRelacionadasDataTable.Columns.Add(new DataColumn("Eliminada", typeof(string)));
        }

		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			CurrentFRDBase = (GISADataset.FRDBaseRow)CurrentDataRow;

			try
			{
                ufsAssociadas = FRDRule.Current.LoadOIDimensoesSuporteData(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, conn);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw ex;
			}

            if (CurrentFRDBase.GetSFRDDimensaoSuporteRows().Length > 0)
                currentDimensaoSuporteRow = CurrentFRDBase.GetSFRDDimensaoSuporteRows()[0];
            else
            {
                currentDimensaoSuporteRow = GisaDataSetHelper.GetInstance().SFRDDimensaoSuporte.NewSFRDDimensaoSuporteRow();
                currentDimensaoSuporteRow.FRDBaseRow = CurrentFRDBase;
                currentDimensaoSuporteRow.Nota = "";

                GisaDataSetHelper.GetInstance().SFRDDimensaoSuporte.AddSFRDDimensaoSuporteRow(currentDimensaoSuporteRow);
            }

            if (CurrentFRDBase.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].IDTipoNivelRelacionado == TipoNivelRelacionado.SR)
                grpUFsAssociadasText = grpUFsAssociadasTextSeries;
            else
                grpUFsAssociadasText = grpUFsAssociadasTextOther;

            OnShowPanel();
			IsLoaded = true;
		}

		public override void ModelToView()
		{
            IsPopulated = false;

            RepopulateUnidadesFisicasAssociadas();

            if (currentDimensaoSuporteRow.IsNotaNull())
                txtDimensao.Text = string.Empty;
            else
                txtDimensao.Text = currentDimensaoSuporteRow.Nota;

            IsPopulated = true;
		}

		private void RepopulateUnidadesFisicasAssociadas()
		{
            UFsRelacionadasDataTable.Clear();

            ufsAssociadas.ForEach(ufAssociada => {
                var r = UFsRelacionadasDataTable.NewRow();
                r[ID] = ufAssociada.ID.ToString();
                r[CODIGO] = ufAssociada.Codigo;
                r[DESIGNACAO] = ufAssociada.Designacao;
                r[TIPO] = ufAssociada.TipoAcondicionamento;
                r[DIMENSOES] = GUIHelper.GUIHelper.FormatDimensoes(ufAssociada.Altura, ufAssociada.Largura, ufAssociada.Profundidade, ufAssociada.TipoMedida);
                r[COTA] = ufAssociada.Cota;
                r[PRODUCAO] = GISA.Utils.GUIHelper.FormatDateInterval(
                    ufAssociada.DPInicioAno, ufAssociada.DPInicioMes, ufAssociada.DPInicioDia, ufAssociada.DPInicioAtribuida,
                    ufAssociada.DPFimAno, ufAssociada.DPFimMes, ufAssociada.DPFimDia, ufAssociada.DPFimAtribuida);
                r[ELIMINADA] = ufAssociada.Eliminado ? ufAssociada.AutosAssociados : "Não";
                UFsRelacionadasDataTable.Rows.Add(r);
                
                if (ufAssociada.Eliminado)
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Style.Font = new Font(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Style.Font, FontStyle.Strikeout);
                else
                    larguraTotal += ufAssociada.Largura;
            });

            UFsRelacionadasDataTable.AcceptChanges();
            dataGridView1.DataSource = UFsRelacionadasDataTable;

            
            dataGridView1.Columns[ID].Width = 70;
            dataGridView1.Columns[CODIGO].Width = 180;
            dataGridView1.Columns[DESIGNACAO].Width = 300;
            dataGridView1.Columns[TIPO].Width = 80;
            dataGridView1.Columns[DIMENSOES].Width = 110;
            dataGridView1.Columns[COTA].Width = 120;
            dataGridView1.Columns[PRODUCAO].Width = 140;
            dataGridView1.Columns[ELIMINADA].Width = 70;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.ClearSelection();

            UpdateInfoSuporte();
		}

        private void UpdateInfoSuporte()
        {
            if (CurrentFRDBase.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].IDTipoNivelRelacionado == TipoNivelRelacionado.SR)
                lblInfoSuporte.Text = string.Format(grpUFsAssociadasText, dataGridView1.Rows.Count, larguraTotal);
            else
                lblInfoSuporte.Text = string.Format(grpUFsAssociadasText, dataGridView1.Rows.Count);
        }

        private void PopulateAssociacao(GISADataset.SFRDUnidadeFisicaRow sfrdufRow, string aeAssociadas)
        {
            AddAssociacao(sfrdufRow, aeAssociadas);
            UpdateInfoSuporte();
        }

        private void AddAssociacao(GISADataset.SFRDUnidadeFisicaRow sfrdufRow, string aeAssociados)
		{
			GISADataset.FRDBaseRow[] frdbaseRows = null;
			GISADataset.SFRDUFDescricaoFisicaRow[] sfrddfRows = null;
			GISADataset.SFRDUFDescricaoFisicaRow sfrddfRow = null;
			GISADataset.SFRDDatasProducaoRow[] sfrddpRows = null;
			GISADataset.SFRDDatasProducaoRow sfrddpRow = null;
			GISADataset.SFRDUFCotaRow[] sfrdcRows = null;
			GISADataset.SFRDUFCotaRow sfrdcRow = null;

			frdbaseRows = sfrdufRow.NivelRow.GetFRDBaseRows();
			if (frdbaseRows.Length > 0) // embora o teste seja "> 0" nunca poderá ser mais do que uma...
			{
				sfrddfRows = frdbaseRows[0].GetSFRDUFDescricaoFisicaRows();
				if (sfrddfRows.Length > 0)
					sfrddfRow = sfrddfRows[0];

				sfrddpRows = frdbaseRows[0].GetSFRDDatasProducaoRows();
				if (sfrddpRows.Length > 0)
					sfrddpRow = sfrddpRows[0];

				sfrdcRows = frdbaseRows[0].GetSFRDUFCotaRows();
				if (sfrdcRows.Length > 0)
					sfrdcRow = sfrdcRows[0];
			}

            // Verificar se foi eliminado (NivelUnidadeFisica):
            GISADataset.NivelDesignadoRow[] ndRows = sfrdufRow.NivelRow.GetNivelDesignadoRows();
            GISADataset.NivelUnidadeFisicaRow[] nufRows = null;
            GISADataset.NivelUnidadeFisicaRow nufRow = null;
            if (ndRows.Length > 0) {
                nufRows = ndRows[0].GetNivelUnidadeFisicaRows();
                if (nufRows.Length > 0)
                    nufRow = nufRows[0];
            }
            
			var codNivel = sfrdufRow.NivelRow.Codigo;
            var codED = sfrdufRow.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First().NivelRowByNivelRelacaoHierarquicaUpper.Codigo;

            var r = UFsRelacionadasDataTable.NewRow();
            r[ID] = sfrdufRow.NivelRow.ID.ToString();
            r[CODIGO] = codED + '/' + codNivel;
            r[DESIGNACAO] = sfrdufRow.NivelRow.GetNivelDesignadoRows()[0].Designacao;
            if (sfrddfRow != null)
            {
                r[TIPO] = sfrddfRow.TipoAcondicionamentoRow.Designacao;
                r[DIMENSOES] = GUIHelper.GUIHelper.FormatDimensoes(sfrddfRow);
            }
            if (sfrdcRow != null && !(sfrdcRow.IsCotaNull()))
                r[COTA] = sfrdcRow.Cota;
            if (sfrddpRow != null)
                r[PRODUCAO] = GUIHelper.GUIHelper.FormatDateInterval(sfrdufRow.NivelRow.GetFRDBaseRows()[0].GetSFRDDatasProducaoRows()[0]);
            if (nufRow != null && nufRow["Eliminado"] != DBNull.Value && nufRow.Eliminado)
            {
                r[ELIMINADA] = aeAssociados;
                if (!sfrddfRow.IsMedidaLarguraNull())
                    larguraTotal += sfrddfRow.MedidaLargura;

                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Style.Font = new Font(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Style.Font, FontStyle.Strikeout);
            }
            else
                r[ELIMINADA] = "Não";
            UFsRelacionadasDataTable.Rows.Add(r);
		}

		public override void ViewToModel()
		{
            currentDimensaoSuporteRow.Nota = txtDimensao.Text;
		}

		public override void Deactivate()
		{
            GUIHelper.GUIHelper.clearField(dataGridView1);
            GUIHelper.GUIHelper.clearField(txtDimensao);
			CurrentFRDBase = null;
            UFsRelacionadasDataTable.Clear();
            larguraTotal = 0;

            OnHidePanel();
		}

		private void AcceptNivelRow(GISADataset.NivelRow NivelRow)
		{
			// aceitar o drop apenas se se tratar de uma UF ainda não associada
            if (GisaDataSetHelper.GetInstance().SFRDUnidadeFisica.Select(string.Format("IDFRDBase={0} AND IDNivel={1}", CurrentFRDBase.ID, NivelRow.ID), "", DataViewRowState.Deleted).Length > 0)
            {
                // prever o caso de a associação ter sido apagada ainda antes de ser gravada na BD e com isso evitar
                // erros durante o save (linhas com o rowstate deleted sao mudadas para modified e é mudado 
                // o booleano isDeleted para True e havendo uma linha com a mesma chave primária com estado added
                // ocorre um erro de conflito de chaves primárias)
                GISADataset.SFRDUnidadeFisicaRow frdufRow = null;
                frdufRow = (GISADataset.SFRDUnidadeFisicaRow)(GisaDataSetHelper.GetInstance().SFRDUnidadeFisica.Select(string.Format("IDFRDBase={0} AND IDNivel={1}", CurrentFRDBase.ID, NivelRow.ID), "", DataViewRowState.Deleted)[0]);
                frdufRow.RejectChanges();
                var aeAssociadas = LoadUFAutosAssociados(NivelRow);
                PopulateAssociacao(frdufRow, aeAssociadas);
            }
            else if (GisaDataSetHelper.GetInstance().SFRDUnidadeFisica.Select(string.Format("IDFRDBase={0} AND IDNivel={1}", CurrentFRDBase.ID, NivelRow.ID)).Length == 0)
            {
                GISADataset.SFRDUnidadeFisicaRow frdufRow = null;
                frdufRow = AssociaUnidadeFisica(NivelRow);
                var aeAssociadas = LoadUFAutosAssociados(NivelRow);
                PopulateAssociacao(frdufRow, aeAssociadas);
            }
		}

        private static string LoadUFAutosAssociados(GISADataset.NivelRow NivelRow)
        {
            string aeAssociadas = string.Empty;
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                aeAssociadas = DBAbstractDataLayer.DataAccessRules.UFRule.Current.LoadUFAutosAssociados(NivelRow.ID, ho.Connection);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            finally
            {
                ho.Dispose();
            }

            return aeAssociadas;
        }

        private void LoadUFRelacionada(long IDNivelUF)
        {
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                FRDRule.Current.LoadUFRelacionada(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, IDNivelUF, ho.Connection);
            }
            catch (Exception ex) { Debug.WriteLine(ex); throw; }
            finally { ho.Dispose(); }
        }

		private GISADataset.SFRDUnidadeFisicaRow AssociaUnidadeFisica(GISADataset.NivelRow nRow)
		{
			GISADataset.SFRDUnidadeFisicaRow frdufRow = null;
			frdufRow = GisaDataSetHelper.GetInstance().SFRDUnidadeFisica.NewSFRDUnidadeFisicaRow();
			frdufRow.FRDBaseRow = CurrentFRDBase;
			frdufRow.NivelRow = nRow;
			GisaDataSetHelper.GetInstance().SFRDUnidadeFisica.AddSFRDUnidadeFisicaRow(frdufRow);

			return frdufRow;
		}

		public void btnRemove_Click(object sender, EventArgs e)
		{
            removeSelectedItem(dataGridView1);
		}

		private void dataGridView1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyValue == Convert.ToInt32(Keys.Delete) && btnRemove.Enabled)
                removeSelectedItem((DataGridView)sender);
		}

        private void removeSelectedItem(DataGridView dataGrid)
        {
            string TitleMsgBox = "Remoção de item(s)";
            var ans = MessageBox.Show("Tem a certeza que deseja " + "eliminar o(s) item(s) selecionado(s)?", TitleMsgBox, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (ans == DialogResult.Cancel) return;

            decimal largura = 0;
            var gridRowsToDelete = dataGrid.SelectedRows.Cast<DataGridViewRow>();
            var ufIDs = gridRowsToDelete.Select(r => long.Parse(r.Cells[ID].Value.ToString())).ToArray();

            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                GisaDataSetHelper.ManageDatasetConstraints(false);
                largura = DBAbstractDataLayer.DataAccessRules.UFRule.Current.LoadDescricaoFisicaAndGetSomatorioLargura(GisaDataSetHelper.GetInstance(), ufIDs, ho.Connection);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            finally
            {
                ho.Dispose();
            }

            // actualizar largura total
            larguraTotal -= largura;

            // apagar rows
            object[] res = new object[2];
            res[0] = CurrentFRDBase.ID;
            DataView dv;
            DataRowView[] dr;
            dv = GisaDataSetHelper.GetInstance().SFRDUnidadeFisica.DefaultView;
            dv.ApplyDefaultSort = true;

            gridRowsToDelete.ToList().ForEach(r =>
            {
                res[1] = r.Cells[ID].Value;
                dr = dv.FindRows(res);
                dr[0].Row.Delete();
                ((DataRowView)r.DataBoundItem).Row.Delete();
            });

            UpdateListButtonsState();            

            // actualizar string
            UpdateInfoSuporte();

            // actualizar datatable bounded à griddataview
            UFsRelacionadasDataTable.AcceptChanges();
        }

        private void dataGridView1_SelectionChanged(object sender, System.EventArgs e)
		{
			UpdateListButtonsState();
            var selItem = dataGridView1.SelectedRows.Count == 1;
            txtCota.Enabled = selItem;
            
            this.txtCota.TextChanged -= new System.EventHandler(this.txtCota_TextChanged);
            if (selItem)
            {
                var item = dataGridView1.SelectedRows[0];
                var IDNivelUF = long.Parse(item.Cells[0].Value.ToString());
                LoadUFRelacionada(IDNivelUF);
                var sfrdufRow = GisaDataSetHelper.GetInstance().SFRDUnidadeFisica.Cast<GISADataset.SFRDUnidadeFisicaRow>().Single(r => r.RowState != DataRowState.Deleted && r.IDFRDBase == CurrentFRDBase.ID && r.IDNivel == IDNivelUF);
                txtCota.Text = sfrdufRow["Cota"] == DBNull.Value ? "" : sfrdufRow.Cota;
            }
            else
                txtCota.Text = "";
            
            this.txtCota.TextChanged += new System.EventHandler(this.txtCota_TextChanged);
		}


        // ToolTip
        private DataGridViewRow dataGridView1_MouseMove_previousRow;
        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var currentRow = dataGridView1.Rows[e.RowIndex];

            if (currentRow == dataGridView1_MouseMove_previousRow)
                return;

            dataGridView1_MouseMove_previousRow = currentRow;

            // See if we have a valid item under mouse pointer
            if (currentRow != null)
            {
                var IDNivelUF = long.Parse(currentRow.Cells[ID].Value.ToString());
                LoadUFRelacionada(IDNivelUF);
                var ufRow = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().Single(r => r.ID == IDNivelUF);
                currentRow.Cells[e.ColumnIndex].ToolTipText = UnidadesFisicasHelper.GetConteudoInformacional(GisaDataSetHelper.GetInstance(), ufRow);
            }
        }

		private void UpdateListButtonsState()
		{
            btnRemove.Enabled = dataGridView1.SelectedRows.Count > 0;
		}

		private void ToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			if (e.Button == MultiPanel.ToolBarButtonAuxList)
				ToggleUnidadesFisicasSupportPanel(MultiPanel.ToolBarButtonAuxList.Pushed);
		}
        
		private void ToggleUnidadesFisicasSupportPanel(bool showIt)
		{            
			if (showIt)
			{
				// Make sure the button is pushed
				MultiPanel.ToolBarButtonAuxList.Pushed = true;

				// Indicação que um painel está a ser usado como suporte
				((frmMain)this.TopLevelControl).isSuportPanel = true;

				// Show the panel with all unidades fisicas
				((frmMain)this.TopLevelControl).PushMasterPanel(typeof(MasterPanelUnidadesFisicas));

                MasterPanelUnidadesFisicas master = (MasterPanelUnidadesFisicas)(((frmMain)this.TopLevelControl).MasterPanel);
                master.ufList.ReloadList();
                master.ufList.MultiSelectListView = true;
                master.ufList.DefineShowItemToolTips = true;

                master.UpdateSupoortPanelPermissions("GISA.FRDUnidadeFisica");
                master.UpdateToolBarButtons();
			}
			else
			{
				// Make sure the button is not pushed            
				MultiPanel.ToolBarButtonAuxList.Pushed = false;

				// Remove the panel with all unidades fisicas
				if (this.TopLevelControl != null)
				{
					if (((frmMain)this.TopLevelControl). MasterPanel is MasterPanelUnidadesFisicas)
					{
                        MasterPanelUnidadesFisicas masterPanelUF = 
                            (MasterPanelUnidadesFisicas)(((frmMain)this.TopLevelControl).MasterPanel);

                        masterPanelUF.ufList.ContextNivelRow = null;
                        //masterPanelUF.ufList.ClearFilter();

						// Indicação que nenhum painel está a ser usado como suporte
						((frmMain)this.TopLevelControl).isSuportPanel = false;
						((frmMain)this.TopLevelControl).PopMasterPanel(typeof(MasterPanelUnidadesFisicas));

                        masterPanelUF.ufList.MultiSelectListView = false;
                        masterPanelUF.ufList.DefineShowItemToolTips = false;
					}
				}
			}
		}

		private GISADataset.NivelRow GetSelectedNivelRowED(GISATreeNode selectedNode)
		{
			GISATreeNode parentNode = selectedNode;
			while (parentNode.Parent != null)
				parentNode = (GISATreeNode)parentNode.Parent;
            
            return parentNode.NivelRow;
		}

		public override void OnShowPanel()
		{
			//Show the button that brings up the support panel
			//and select it by default.
            if (!(((frmMain)this.TopLevelControl).isSuportPanel) && !TbBAuxListEventAssigned)
            {
                MultiPanel.ToolBar.ButtonClick += ToolBar_ButtonClick;
                MultiPanel.ToolBarButtonAuxList.Visible = true;

                TbBAuxListEventAssigned = true;
            }
		}

		public override void OnHidePanel()
		{
			// if seguinte serve exclusivamente para debug
            if (TbBAuxListEventAssigned)
            {
                if (CurrentFRDBase != null && CurrentFRDBase.RowState == DataRowState.Detached)
                    Debug.WriteLine("OCORREU SITUAÇÃO DE ERRO NO PAINEL UFS ASSOCIADAS. EM PRINCIPIO NINGUEM DEU POR ELE.");

                ToggleUnidadesFisicasSupportPanel(false);
                //Deactivate Toolbar Buttons
                MultiPanel.ToolBar.ButtonClick -= ToolBar_ButtonClick;
                MultiPanel.ToolBarButtonAuxList.Visible = false;
             
                TbBAuxListEventAssigned = false;
            }
		}

        private void txtCota_TextChanged(object sender, EventArgs e)
        {
            var dgRow = dataGridView1.SelectedRows[0];

            object[] res = new object[2];
            res[0] = CurrentFRDBase.ID;
            res[1] = dgRow.Cells[ID].Value;
            DataView dv;
            DataRowView[] dr;
            dv = GisaDataSetHelper.GetInstance().SFRDUnidadeFisica.DefaultView;
            dv.ApplyDefaultSort = true;
            
            dr = dv.FindRows(res);
            ((GISADataset.SFRDUnidadeFisicaRow)dr[0].Row).Cota = txtCota.Text;
        }
	}
}