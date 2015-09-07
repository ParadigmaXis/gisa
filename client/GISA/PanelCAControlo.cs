using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using System.Collections.Generic;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;
using GISA.Search;

using GISA.Controls.ControloAut;

namespace GISA
{
	public class PanelCAControlo : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelCAControlo() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            lstVwIdentidadeInstituicoes.SelectedIndexChanged += lstVwIdentidadeInstituicoes_SelectedIndexChanged;
            lstVwIdentidadeInstituicoes.KeyUp += lstVwIdentidadeInstituicoes_KeyUp;
            btnRemove.Click += btnRemove_Click;
            btnAdd.Click += btnAdd_Click;
            chkCompleta.CheckStateChanged += chkbox_CheckStateChanged;
            chkDefinitivo.CheckStateChanged += chkbox_CheckStateChanged;

			GetExtraResources();
			if (! (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().TipoServer.ID == Convert.ToInt64(TipoServer.ClienteServidor)))
			{
				lstVwDataCriacaoRevisao.Columns.Remove(colOperator);
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
		internal System.Windows.Forms.GroupBox grpIdentificadorRegisto;
		internal System.Windows.Forms.TextBox txtIdentificadorRegisto;
		internal System.Windows.Forms.ListView lstVwIdentidadeInstituicoes;
		internal System.Windows.Forms.GroupBox grpIdentidadeInstituicoes;
		internal System.Windows.Forms.GroupBox grpObservacoes;
		internal System.Windows.Forms.GroupBox grpRegrasConvencoes;
		internal System.Windows.Forms.Button btnAdd;
		internal System.Windows.Forms.ColumnHeader colFormaAutorizada;
		internal System.Windows.Forms.ColumnHeader colIdentificadorUnico;
		internal System.Windows.Forms.TextBox txtRegrasConvencoes;
		internal System.Windows.Forms.TextBox txtObservacoes;
		internal System.Windows.Forms.GroupBox grpDataCriacaoRevisao;
		internal System.Windows.Forms.ListView lstVwDataCriacaoRevisao;
		internal System.Windows.Forms.ComboBox cbLingua;
		internal System.Windows.Forms.ComboBox cbAlfabeto;
		internal System.Windows.Forms.ColumnHeader colOperator;
		internal System.Windows.Forms.ColumnHeader colAuthority;
		internal System.Windows.Forms.Button btnRemove;
//    Friend WithEvents btnEdit As System.Windows.Forms.Button
		internal System.Windows.Forms.GroupBox GroupBox3;
		internal System.Windows.Forms.CheckBox chkCompleta;
		internal System.Windows.Forms.CheckBox chkDefinitivo;
		internal System.Windows.Forms.GroupBox grpLinguaAlfabeto;
		internal System.Windows.Forms.ColumnHeader colDataDescricao;
		internal System.Windows.Forms.ColumnHeader colDataRegisto;
        internal System.Windows.Forms.GroupBox grpAnteriores;
		internal GISA.ControloRevisoes ControloRevisoes1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.grpIdentificadorRegisto = new System.Windows.Forms.GroupBox();
            this.txtIdentificadorRegisto = new System.Windows.Forms.TextBox();
            this.grpIdentidadeInstituicoes = new System.Windows.Forms.GroupBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lstVwIdentidadeInstituicoes = new System.Windows.Forms.ListView();
            this.colFormaAutorizada = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIdentificadorUnico = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpObservacoes = new System.Windows.Forms.GroupBox();
            this.txtObservacoes = new System.Windows.Forms.TextBox();
            this.grpLinguaAlfabeto = new System.Windows.Forms.GroupBox();
            this.cbLingua = new System.Windows.Forms.ComboBox();
            this.cbAlfabeto = new System.Windows.Forms.ComboBox();
            this.grpRegrasConvencoes = new System.Windows.Forms.GroupBox();
            this.txtRegrasConvencoes = new System.Windows.Forms.TextBox();
            this.grpDataCriacaoRevisao = new System.Windows.Forms.GroupBox();
            this.ControloRevisoes1 = new GISA.ControloRevisoes();
            this.grpAnteriores = new System.Windows.Forms.GroupBox();
            this.lstVwDataCriacaoRevisao = new System.Windows.Forms.ListView();
            this.colDataRegisto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDataDescricao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOperator = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAuthority = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.chkCompleta = new System.Windows.Forms.CheckBox();
            this.chkDefinitivo = new System.Windows.Forms.CheckBox();
            this.grpIdentificadorRegisto.SuspendLayout();
            this.grpIdentidadeInstituicoes.SuspendLayout();
            this.grpObservacoes.SuspendLayout();
            this.grpLinguaAlfabeto.SuspendLayout();
            this.grpRegrasConvencoes.SuspendLayout();
            this.grpDataCriacaoRevisao.SuspendLayout();
            this.grpAnteriores.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpIdentificadorRegisto
            // 
            this.grpIdentificadorRegisto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpIdentificadorRegisto.Controls.Add(this.txtIdentificadorRegisto);
            this.grpIdentificadorRegisto.Location = new System.Drawing.Point(6, 2);
            this.grpIdentificadorRegisto.Name = "grpIdentificadorRegisto";
            this.grpIdentificadorRegisto.Size = new System.Drawing.Size(788, 43);
            this.grpIdentificadorRegisto.TabIndex = 1;
            this.grpIdentificadorRegisto.TabStop = false;
            this.grpIdentificadorRegisto.Text = "4.1. Identificador de registo";
            // 
            // txtIdentificadorRegisto
            // 
            this.txtIdentificadorRegisto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIdentificadorRegisto.Enabled = false;
            this.txtIdentificadorRegisto.Location = new System.Drawing.Point(8, 15);
            this.txtIdentificadorRegisto.Name = "txtIdentificadorRegisto";
            this.txtIdentificadorRegisto.Size = new System.Drawing.Size(772, 20);
            this.txtIdentificadorRegisto.TabIndex = 1;
            // 
            // grpIdentidadeInstituicoes
            // 
            this.grpIdentidadeInstituicoes.Controls.Add(this.btnRemove);
            this.grpIdentidadeInstituicoes.Controls.Add(this.btnAdd);
            this.grpIdentidadeInstituicoes.Controls.Add(this.lstVwIdentidadeInstituicoes);
            this.grpIdentidadeInstituicoes.Location = new System.Drawing.Point(6, 48);
            this.grpIdentidadeInstituicoes.Name = "grpIdentidadeInstituicoes";
            this.grpIdentidadeInstituicoes.Size = new System.Drawing.Size(442, 112);
            this.grpIdentidadeInstituicoes.TabIndex = 2;
            this.grpIdentidadeInstituicoes.TabStop = false;
            this.grpIdentidadeInstituicoes.Text = "4.2. Identidade das instituições";
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(413, 62);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(24, 24);
            this.btnRemove.TabIndex = 4;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(413, 32);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(24, 24);
            this.btnAdd.TabIndex = 3;
            // 
            // lstVwIdentidadeInstituicoes
            // 
            this.lstVwIdentidadeInstituicoes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwIdentidadeInstituicoes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFormaAutorizada,
            this.colIdentificadorUnico});
            this.lstVwIdentidadeInstituicoes.FullRowSelect = true;
            this.lstVwIdentidadeInstituicoes.Location = new System.Drawing.Point(6, 16);
            this.lstVwIdentidadeInstituicoes.Name = "lstVwIdentidadeInstituicoes";
            this.lstVwIdentidadeInstituicoes.Size = new System.Drawing.Size(402, 88);
            this.lstVwIdentidadeInstituicoes.TabIndex = 2;
            this.lstVwIdentidadeInstituicoes.UseCompatibleStateImageBehavior = false;
            this.lstVwIdentidadeInstituicoes.View = System.Windows.Forms.View.Details;
            // 
            // colFormaAutorizada
            // 
            this.colFormaAutorizada.Text = "Designação";
            this.colFormaAutorizada.Width = 257;
            // 
            // colIdentificadorUnico
            // 
            this.colIdentificadorUnico.Text = "Identificador único";
            this.colIdentificadorUnico.Width = 128;
            // 
            // grpObservacoes
            // 
            this.grpObservacoes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpObservacoes.Controls.Add(this.txtObservacoes);
            this.grpObservacoes.Location = new System.Drawing.Point(456, 220);
            this.grpObservacoes.Name = "grpObservacoes";
            this.grpObservacoes.Size = new System.Drawing.Size(336, 372);
            this.grpObservacoes.TabIndex = 7;
            this.grpObservacoes.TabStop = false;
            this.grpObservacoes.Text = "4.8. Fontes / 4.9. Observações";
            // 
            // txtObservacoes
            // 
            this.txtObservacoes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObservacoes.Location = new System.Drawing.Point(8, 16);
            this.txtObservacoes.Multiline = true;
            this.txtObservacoes.Name = "txtObservacoes";
            this.txtObservacoes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservacoes.Size = new System.Drawing.Size(320, 348);
            this.txtObservacoes.TabIndex = 13;
            // 
            // grpLinguaAlfabeto
            // 
            this.grpLinguaAlfabeto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpLinguaAlfabeto.Controls.Add(this.cbLingua);
            this.grpLinguaAlfabeto.Controls.Add(this.cbAlfabeto);
            this.grpLinguaAlfabeto.Location = new System.Drawing.Point(456, 168);
            this.grpLinguaAlfabeto.Name = "grpLinguaAlfabeto";
            this.grpLinguaAlfabeto.Size = new System.Drawing.Size(336, 47);
            this.grpLinguaAlfabeto.TabIndex = 6;
            this.grpLinguaAlfabeto.TabStop = false;
            this.grpLinguaAlfabeto.Text = "4.7. Língua e alfabeto";
            // 
            // cbLingua
            // 
            this.cbLingua.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLingua.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLingua.Location = new System.Drawing.Point(9, 18);
            this.cbLingua.Name = "cbLingua";
            this.cbLingua.Size = new System.Drawing.Size(95, 21);
            this.cbLingua.TabIndex = 11;
            // 
            // cbAlfabeto
            // 
            this.cbAlfabeto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAlfabeto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAlfabeto.Location = new System.Drawing.Point(112, 17);
            this.cbAlfabeto.Name = "cbAlfabeto";
            this.cbAlfabeto.Size = new System.Drawing.Size(213, 21);
            this.cbAlfabeto.TabIndex = 12;
            // 
            // grpRegrasConvencoes
            // 
            this.grpRegrasConvencoes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRegrasConvencoes.Controls.Add(this.txtRegrasConvencoes);
            this.grpRegrasConvencoes.Location = new System.Drawing.Point(456, 48);
            this.grpRegrasConvencoes.Name = "grpRegrasConvencoes";
            this.grpRegrasConvencoes.Size = new System.Drawing.Size(336, 112);
            this.grpRegrasConvencoes.TabIndex = 3;
            this.grpRegrasConvencoes.TabStop = false;
            this.grpRegrasConvencoes.Text = "4.3. Regras e/ou convenções";
            // 
            // txtRegrasConvencoes
            // 
            this.txtRegrasConvencoes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegrasConvencoes.Location = new System.Drawing.Point(8, 16);
            this.txtRegrasConvencoes.Multiline = true;
            this.txtRegrasConvencoes.Name = "txtRegrasConvencoes";
            this.txtRegrasConvencoes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRegrasConvencoes.Size = new System.Drawing.Size(320, 88);
            this.txtRegrasConvencoes.TabIndex = 5;
            // 
            // grpDataCriacaoRevisao
            // 
            this.grpDataCriacaoRevisao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpDataCriacaoRevisao.Controls.Add(this.ControloRevisoes1);
            this.grpDataCriacaoRevisao.Controls.Add(this.grpAnteriores);
            this.grpDataCriacaoRevisao.Location = new System.Drawing.Point(8, 220);
            this.grpDataCriacaoRevisao.Name = "grpDataCriacaoRevisao";
            this.grpDataCriacaoRevisao.Size = new System.Drawing.Size(440, 372);
            this.grpDataCriacaoRevisao.TabIndex = 5;
            this.grpDataCriacaoRevisao.TabStop = false;
            this.grpDataCriacaoRevisao.Text = "4.6. Data de criação e/ou revisão";
            // 
            // ControloRevisoes1
            // 
            this.ControloRevisoes1.Location = new System.Drawing.Point(8, 16);
            this.ControloRevisoes1.Name = "ControloRevisoes1";
            this.ControloRevisoes1.Size = new System.Drawing.Size(424, 44);
            this.ControloRevisoes1.TabIndex = 14;
            // 
            // grpAnteriores
            // 
            this.grpAnteriores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAnteriores.Controls.Add(this.lstVwDataCriacaoRevisao);
            this.grpAnteriores.Location = new System.Drawing.Point(8, 60);
            this.grpAnteriores.Name = "grpAnteriores";
            this.grpAnteriores.Size = new System.Drawing.Size(422, 308);
            this.grpAnteriores.TabIndex = 13;
            this.grpAnteriores.TabStop = false;
            this.grpAnteriores.Text = "Registos anteriores";
            // 
            // lstVwDataCriacaoRevisao
            // 
            this.lstVwDataCriacaoRevisao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwDataCriacaoRevisao.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDataRegisto,
            this.colDataDescricao,
            this.colOperator,
            this.colAuthority});
            this.lstVwDataCriacaoRevisao.FullRowSelect = true;
            this.lstVwDataCriacaoRevisao.Location = new System.Drawing.Point(8, 16);
            this.lstVwDataCriacaoRevisao.Name = "lstVwDataCriacaoRevisao";
            this.lstVwDataCriacaoRevisao.Size = new System.Drawing.Size(408, 286);
            this.lstVwDataCriacaoRevisao.TabIndex = 10;
            this.lstVwDataCriacaoRevisao.UseCompatibleStateImageBehavior = false;
            this.lstVwDataCriacaoRevisao.View = System.Windows.Forms.View.Details;
            // 
            // colDataRegisto
            // 
            this.colDataRegisto.Text = "Data registo";
            this.colDataRegisto.Width = 111;
            // 
            // colDataDescricao
            // 
            this.colDataDescricao.Text = "Data descrição";
            this.colDataDescricao.Width = 84;
            // 
            // colOperator
            // 
            this.colOperator.Text = "Operador";
            this.colOperator.Width = 138;
            // 
            // colAuthority
            // 
            this.colAuthority.Text = "Autor da descrição";
            this.colAuthority.Width = 138;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.chkCompleta);
            this.GroupBox3.Controls.Add(this.chkDefinitivo);
            this.GroupBox3.Location = new System.Drawing.Point(8, 164);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(440, 50);
            this.GroupBox3.TabIndex = 4;
            this.GroupBox3.TabStop = false;
            // 
            // chkCompleta
            // 
            this.chkCompleta.Location = new System.Drawing.Point(8, 27);
            this.chkCompleta.Name = "chkCompleta";
            this.chkCompleta.Size = new System.Drawing.Size(192, 21);
            this.chkCompleta.TabIndex = 7;
            this.chkCompleta.Text = "4.5. Completo (nível de detalhe)";
            // 
            // chkDefinitivo
            // 
            this.chkDefinitivo.Location = new System.Drawing.Point(8, 8);
            this.chkDefinitivo.Name = "chkDefinitivo";
            this.chkDefinitivo.Size = new System.Drawing.Size(246, 21);
            this.chkDefinitivo.TabIndex = 6;
            this.chkDefinitivo.Text = "4.4. Validado (estado completo do registo)";
            // 
            // PanelCAControlo
            // 
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.grpDataCriacaoRevisao);
            this.Controls.Add(this.grpRegrasConvencoes);
            this.Controls.Add(this.grpLinguaAlfabeto);
            this.Controls.Add(this.grpObservacoes);
            this.Controls.Add(this.grpIdentidadeInstituicoes);
            this.Controls.Add(this.grpIdentificadorRegisto);
            this.Name = "PanelCAControlo";
            this.grpIdentificadorRegisto.ResumeLayout(false);
            this.grpIdentificadorRegisto.PerformLayout();
            this.grpIdentidadeInstituicoes.ResumeLayout(false);
            this.grpObservacoes.ResumeLayout(false);
            this.grpObservacoes.PerformLayout();
            this.grpLinguaAlfabeto.ResumeLayout(false);
            this.grpRegrasConvencoes.ResumeLayout(false);
            this.grpRegrasConvencoes.PerformLayout();
            this.grpDataCriacaoRevisao.ResumeLayout(false);
            this.grpAnteriores.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

		private void GetExtraResources()
		{
			btnAdd.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
			btnRemove.Image = SharedResourcesOld.CurrentSharedResources.Apagar;
		}

		private GISADataset.ControloAutRow CurrentControloAut;
		private string QueryFilter;
		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			CurrentControloAut = (GISADataset.ControloAutRow)CurrentDataRow;
			if (CurrentControloAut == null)
				return;

			ControloAutRule.Current.LoadDataPanelCAControlo(GisaDataSetHelper.GetInstance(), CurrentControloAut.ID, conn);

			QueryFilter = "IDControloAut=" + CurrentControloAut.ID.ToString();
			IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = false;

			ControloRevisoes1.ControloAutores1.LoadAndPopulateAuthors();
			if (SessionHelper.GetGisaPrincipal().TrusteeUserAuthor != null)
				ControloRevisoes1.ControloAutores1.SelectedAutor = SessionHelper.GetGisaPrincipal().TrusteeUserAuthor.TrusteeRow;

			txtIdentificadorRegisto.Text = CurrentControloAut.ID.ToString();

			if (! (CurrentControloAut.IsRegrasConvencoesNull()))
				txtRegrasConvencoes.Text = CurrentControloAut.RegrasConvencoes;
			else
				txtRegrasConvencoes.Text = "";

			if (! (CurrentControloAut.IsObservacoesNull()))
				txtObservacoes.Text = CurrentControloAut.Observacoes;
			else
				txtObservacoes.Text = "";

			lstVwIdentidadeInstituicoes.Items.Clear();
			foreach (GISADataset.ControloAutRelRow carRow in GisaDataSetHelper.GetInstance().ControloAutRel.Select(QueryFilter))
				AddIdentidadeInstituicao(carRow);

			UpdateLstVwIdentidadeInstituicoesButtonsState();

			// populate datas criacao history
			lstVwDataCriacaoRevisao.Items.Clear();
			ListViewItem item = null;
			foreach (GISADataset.ControloAutDataDeDescricaoRow cadddRow in GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao. Select(QueryFilter, "DataEdicao DESC"))
			{
				item = lstVwDataCriacaoRevisao.Items.Add("");
				item.SubItems.AddRange(new string[] {"", "", ""});
				item.SubItems[colDataRegisto.Index].Text = cadddRow.DataEdicao.ToString();
				item.SubItems[colDataDescricao.Index].Text = string.Format("{0:yyyy}-{0:MM}-{0:dd}", cadddRow.DataAutoria);

				if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().TipoServer.ID == Convert.ToInt64(TipoServer.ClienteServidor))
					item.SubItems[colOperator.Index].Text = cadddRow.TrusteeUserRowByTrusteeUserControloAutDataDeDescricao.TrusteeRow.Name;

				if (! cadddRow.IsIDTrusteeAuthorityNull())
					item.SubItems[colAuthority.Index].Text = cadddRow.TrusteeUserRowByTrusteeUserControloAutDataDeDescricaoAuthority.TrusteeRow.Name;

				item.Tag = cadddRow;
			}

			lstVwDataCriacaoRevisao.SelectedItems.Clear();
			// the following should ensure the list was scrolled all the
			// way to the bottom. it doesn't always not working though.
			if (lstVwDataCriacaoRevisao.Items.Count > 0)
			{
				lstVwDataCriacaoRevisao.EnsureVisible(0);
			}

			// populate linguas
			ArrayList Iso639Data = new ArrayList();
			GISADataset.Iso639Row i639Row = (GISADataset.Iso639Row)(GisaDataSetHelper.GetInstance().Iso639.NewRow());
			i639Row.ID = -1;
			i639Row.LanguageNameEnglish = "";
			Iso639Data.Add(i639Row);
			Iso639Data.AddRange(GisaDataSetHelper.GetInstance().Iso639.Select());
			cbLingua.DataSource = Iso639Data;
			cbLingua.DisplayMember = "LanguageNameEnglish";
			cbLingua.ValueMember = "ID";
			try
			{
				cbLingua.SelectedValue = CurrentControloAut.IDIso639p2;
			}
			catch (StrongTypingException)
			{
				cbLingua.SelectedValue = -1;
			}

			// populate alfabetos
			ArrayList Iso15924Data = new ArrayList();
			GISADataset.Iso15924Row i15924Row = (GISADataset.Iso15924Row)(GisaDataSetHelper.GetInstance().Iso15924.NewRow());
			i15924Row.ID = -1;
			i15924Row.ScriptNameEnglish = "";
			Iso15924Data.Add(i15924Row);
			Iso15924Data.AddRange(GisaDataSetHelper.GetInstance().Iso15924.Select());
			cbAlfabeto.DataSource = Iso15924Data;
			cbAlfabeto.DisplayMember = "ScriptNameEnglish";
			cbAlfabeto.ValueMember = "ID";
			try
			{
				cbAlfabeto.SelectedValue = CurrentControloAut.IDIso15924;
			}
			catch (StrongTypingException)
			{
				cbAlfabeto.SelectedValue = -1;
			}

			// populate checkboxes
			chkDefinitivo.Checked = CurrentControloAut.Autorizado;
			chkCompleta.Checked = CurrentControloAut.Completo;
			IsPopulated = true;
		}

		public override void ViewToModel()
		{
			if (CurrentControloAut == null || CurrentControloAut.RowState == DataRowState.Detached || ! IsLoaded)
				return;

			CurrentControloAut.RegrasConvencoes = txtRegrasConvencoes.Text;
			CurrentControloAut.Observacoes = txtObservacoes.Text;

			if (cbLingua.SelectedValue == null || ((long)cbLingua.SelectedValue) == -1)
			{
				if (! CurrentControloAut.IsIDIso639p2Null())
					CurrentControloAut["IDIso639p2"] = DBNull.Value;
			}
			else
			{
				if (CurrentControloAut.IsIDIso639p2Null() || CurrentControloAut.IDIso639p2 != ((GISADataset.Iso639Row)cbLingua.SelectedItem).ID)
					CurrentControloAut.IDIso639p2 = ((GISADataset.Iso639Row)cbLingua.SelectedItem).ID;
			}
			
            if (cbAlfabeto.SelectedValue == null || ((long)cbAlfabeto.SelectedValue) == -1)
			{
				if (! CurrentControloAut.IsIDIso639p2Null())
					CurrentControloAut["IDIso15924"] = DBNull.Value;
			}
			else
			{
				if (CurrentControloAut.IsIDIso15924Null() || CurrentControloAut.IDIso15924 != ((GISADataset.Iso15924Row)cbAlfabeto.SelectedItem).ID)
					CurrentControloAut.IDIso15924 = ((GISADataset.Iso15924Row)cbAlfabeto.SelectedItem).ID;
			}

			CurrentControloAut.Autorizado = chkDefinitivo.Checked;
			CurrentControloAut.Completo = chkCompleta.Checked;
		}

		public override void Deactivate()
		{
            GUIHelper.GUIHelper.clearField(txtIdentificadorRegisto);
            GUIHelper.GUIHelper.clearField(txtRegrasConvencoes);
            GUIHelper.GUIHelper.clearField(txtObservacoes);
            GUIHelper.GUIHelper.clearField(chkDefinitivo);
            GUIHelper.GUIHelper.clearField(chkCompleta);
		}

		private void lstVwIdentidadeInstituicoes_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateLstVwIdentidadeInstituicoesButtonsState();
		}

		private void UpdateLstVwIdentidadeInstituicoesButtonsState()
		{
			btnRemove.Enabled = lstVwIdentidadeInstituicoes.SelectedItems.Count > 0;
		}

		private void lstVwIdentidadeInstituicoes_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == Convert.ToInt32(Keys.Delete))
                GUIHelper.GUIHelper.deleteSelectedLstVwItems((ListView)sender);
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
            GUIHelper.GUIHelper.deleteSelectedLstVwItems((ListView)lstVwIdentidadeInstituicoes);

            // registar eliminação
            ((frmMain)TopLevelControl).CurrentContext.RaiseRegisterModificationEvent(CurrentControloAut);
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			if (CurrentControloAut.RowState == DataRowState.Detached)
			{
				MessageBox.Show("A notícia de autoridade selecionada como contexto foi " + System.Environment.NewLine + "apagada por outro utilizador e por esse motivo não pode ser editada.", "Edição de Notícia de Autoridade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			FormPickControloAut frmPick = new FormPickControloAut(CurrentControloAut);
            frmPick.Text = "Notícia de autoridade - Pesquisar entidades produtoras";
			frmPick.caList.AllowedNoticiaAut(TipoNoticiaAut.EntidadeProdutora);
			frmPick.caList.ReloadList();

			if (frmPick.caList.Items.Count > 0)
				frmPick.caList.SelectItem(frmPick.caList.Items[0]);

			GISADataset.ControloAutRow caRow = null;
			GISADataset.ControloAutRelRow carRow = null;
			GISADataset.TipoControloAutRelRow tcarRow = null;
			switch (frmPick.ShowDialog())
			{
				case DialogResult.OK:
					foreach (ListViewItem li in frmPick.caList.SelectedItems)
					{
						caRow = ((GISADataset.ControloAutDicionarioRow)li.Tag).ControloAutRow;

						// Proteger contra relações repetidas
						string query = string.Format("({0} OR {1}) AND IDTipoRel = {2}", GetRelConstraint(caRow, CurrentControloAut), GetRelConstraint(CurrentControloAut, caRow), System.Enum.Format(typeof(TipoControloAutRel), TipoControloAutRel.Instituicao, "D"));

						if ((GisaDataSetHelper.GetInstance().ControloAutRel.Select(query)).Length == 0)
						{
							tcarRow = (GISADataset.TipoControloAutRelRow)(GisaDataSetHelper.GetInstance().TipoControloAutRel.Select("ID=" + System.Enum.Format(typeof(TipoControloAutRel), TipoControloAutRel.Instituicao, "D"))[0]);
							carRow = GisaDataSetHelper.GetInstance().ControloAutRel.NewControloAutRelRow();
							carRow.ControloAutRowByControloAutControloAutRel = CurrentControloAut;
							carRow.ControloAutRowByControloAutControloAutRelAlias = caRow;
							carRow.TipoControloAutRelRow = tcarRow;
							carRow["Descricao"] = DBNull.Value;
							carRow["InicioAno"] = DBNull.Value;
							carRow["InicioMes"] = DBNull.Value;
							carRow["InicioDia"] = DBNull.Value;
							carRow["FimAno"] = DBNull.Value;
							carRow["FimMes"] = DBNull.Value;
							carRow["FimDia"] = DBNull.Value;

							GisaDataSetHelper.GetInstance().ControloAutRel.AddControloAutRelRow(carRow);

							PersistencyHelper.VerifyRelExistencePreConcArguments pcArgs = new PersistencyHelper.VerifyRelExistencePreConcArguments();
							pcArgs.ID = caRow.ID;
							pcArgs.IDUpper = CurrentControloAut.ID;
							pcArgs.IDTipoRel = Convert.ToInt64(TipoControloAutRel.Instituicao);

						 	PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save(GetRelPanelCAControl, pcArgs);
							PersistencyHelper.cleanDeletedData();

							if (carRow.RowState == DataRowState.Detached)
								MessageBox.Show("Não foi possível estabelecer a relação uma vez que a notícia de autoridade " + System.Environment.NewLine + "que pretende associar foi apagada por outro utilizador.", "Erro ao estabelecer relação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
							else
							{
								AddIdentidadeInstituicao(carRow);

                                // registar inserção
                                ((frmMain)TopLevelControl).CurrentContext.RaiseRegisterModificationEvent(CurrentControloAut);

                                if (successfulSave == PersistencyHelper.SaveResult.successful)
                                {
                                    UpdateCA(CurrentControloAut);
                                    UpdateCA(caRow);
                                }
							}
						}
						else
							MessageBox.Show("A relação que pretende adicionar já existe.", "Erro ao estabelecer relação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
					}
					break;
				case DialogResult.Cancel:
				break;
			}
		}

		private string GetRelConstraint(GISADataset.ControloAutRow a, GISADataset.ControloAutRow b)
		{
			return "(IDControloAut=" + a.ID.ToString() + " AND IDControloAutAlias=" + b.ID.ToString() + ")";
		}

		private void GetRelPanelCAControl(PersistencyHelper.PreConcArguments args)
		{
			GISADataset.ControloAutRelRow newCARRow = null;
			GISADataset.ControloAutRelRow delCARRow = null;
			PersistencyHelper.VerifyRelExistencePreConcArguments vrePsa = null;
			vrePsa = (PersistencyHelper.VerifyRelExistencePreConcArguments)args;

			//Added
			if (GisaDataSetHelper.GetInstance().ControloAutRel.Select(string.Format("(IDControloAut={0} AND IDControloAutAlias={1}) OR (IDControloAut={1} AND IDControloAutAlias={0})", vrePsa.ID, vrePsa.IDUpper)).Length > 0)
				newCARRow = (GISADataset.ControloAutRelRow)(GisaDataSetHelper.GetInstance().ControloAutRel.Select(string.Format("(IDControloAut={0} AND IDControloAutAlias={1}) OR (IDControloAut={1} AND IDControloAutAlias={0})", vrePsa.ID, vrePsa.IDUpper))[0]);

			//Del 
			if (GisaDataSetHelper.GetInstance().ControloAutRel.Select(string.Format("(IDControloAut={0} AND IDControloAutAlias={1}) OR (IDControloAut={1} AND IDControloAutAlias={0})", vrePsa.ID, vrePsa.IDUpper), null, DataViewRowState.Deleted).Length > 0)
				delCARRow = (GISADataset.ControloAutRelRow)(GisaDataSetHelper.GetInstance().ControloAutRel.Select(string.Format("(IDControloAut={0} AND IDControloAutAlias={1}) OR (IDControloAut={1} AND IDControloAutAlias={0})", vrePsa.ID, vrePsa.IDUpper), null, DataViewRowState.Deleted)[0]);

			//bd
			int result = ControloAutRule.Current.ExistsRel(vrePsa.ID, vrePsa.IDUpper, vrePsa.IDTipoRel, vrePsa.isCARRow, vrePsa.tran);
			switch (result)
			{
				case 0:
					vrePsa.CreateEditResult = PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.NoError;
					break;
				case 1:
					vrePsa.CreateEditResult = PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.RelationAlreadyExists;
					break;
				case 2:
					vrePsa.CreateEditResult = PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.CyclicRelation;
					break;
				case 3:
					vrePsa.CreateEditResult = PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.CADeleted;
					break;
			}

			if (newCARRow != null && delCARRow == null && result == Convert.ToInt64(PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.NoError))
			{
				//Comportamento normal: a relação é criada
			}
			else if (newCARRow != null && delCARRow != null && ! (result == Convert.ToInt64(PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.NoError)))
			{
				delCARRow.AcceptChanges();
			}
			else if (newCARRow != null && delCARRow == null && result == Convert.ToInt64(PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.NoError))
			{
				newCARRow.AcceptChanges();
			}
			else if (newCARRow != null && delCARRow != null && result == Convert.ToInt64(PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.NoError))
			{
				delCARRow.AcceptChanges();
				newCARRow.AcceptChanges();
			}
		}

		private void AddIdentidadeInstituicao(GISADataset.ControloAutRelRow carRow)
		{
			if (carRow.IDTipoRel != Convert.ToInt64(TipoControloAutRel.Instituicao))
				return;

			DataRow[] cadRows = GisaDataSetHelper.GetInstance(). ControloAutDicionario. Select("IDControloAut=" + carRow.ControloAutRowByControloAutControloAutRelAlias.ID.ToString() + " AND IDTipoControloAutForma=" + System.Enum.Format(typeof(TipoControloAutForma), TipoControloAutForma.FormaAutorizada, "D"));

			if (cadRows.Length > 0)
			{
				Debug.Assert(cadRows.Length == 1);
				GISADataset.ControloAutDicionarioRow cadRow = (GISADataset.ControloAutDicionarioRow)(cadRows[0]);
				if (! (alreadyExistInList(carRow)))
				{
					ListViewItem tempWith1 = lstVwIdentidadeInstituicoes.Items.Add(cadRow.DicionarioRow.Termo);

					if (((GISADataset.ControloAutDicionarioRow)cadRow).ControloAutRow.IsChaveColectividadeNull())
						tempWith1.SubItems.Add(string.Empty);
					else
						tempWith1.SubItems.Add(((GISADataset.ControloAutDicionarioRow)cadRow). ControloAutRow.ChaveColectividade);

					tempWith1.Tag = carRow;
				}
			}
		}

		private bool alreadyExistInList(DataRow carRow)
		{
			foreach (ListViewItem item in lstVwIdentidadeInstituicoes.Items)
				if (item.Tag == carRow)
					return true;

			return false;
		}

		// não esperar que a checkbox perca o focus para actualizar os dados
		private void chkbox_CheckStateChanged(object sender, System.EventArgs e)
		{
			if (((CheckBox)sender).DataBindings.Count > 0)
				((CheckBox)sender).DataBindings[0].BindingManagerBase.EndCurrentEdit();
		}

        private void UpdateCA(GISADataset.ControloAutRow caRow)
        {
            switch (caRow.IDTipoNoticiaAut)
            {
                case (long)TipoNoticiaAut.EntidadeProdutora:
                    GISA.Search.Updater.updateProdutor(caRow.ID);
                    GISA.Search.Updater.updateNivelDocumentalComProdutores(caRow.GetNivelControloAutRows()[0].ID);
                    break;
                case (long)TipoNoticiaAut.Onomastico:
                case (long)TipoNoticiaAut.Ideografico:
                case (long)TipoNoticiaAut.ToponimicoGeografico:
                    GISA.Search.Updater.updateAssunto(caRow.ID);
                    break;
                case (long)TipoNoticiaAut.TipologiaInformacional:
                    GISA.Search.Updater.updateTipologia(caRow.ID);
                    break;
            }
        }
	}
}