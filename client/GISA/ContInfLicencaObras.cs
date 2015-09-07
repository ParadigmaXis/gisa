using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;
using GISA.GUIHelper;

using GISA.Controls.ControloAut;
using System.Diagnostics;

namespace GISA
{
    public partial class ContInfLicencaObras : GISA.GISAPanel {
        public ContInfLicencaObras() {
            InitializeComponent();

            lstVwLocalizacaoActual.ItemSelectionChanged += lstVw_SelectedIndexChanged;
            lstVwTecnicoObra.ItemSelectionChanged += lstVw_SelectedIndexChanged;
            lstVwRequerente.ItemSelectionChanged += lstVw_SelectedIndexChanged;
            lstVwAverbamentos.ItemSelectionChanged += lstVw_SelectedIndexChanged;
            lstVwAtestadoHab.ItemSelectionChanged += lstVw_SelectedIndexChanged;
            lstVwLocalizacaoAntiga.ItemSelectionChanged += lstVw_SelectedIndexChanged;
            lstVwDatas.ItemSelectionChanged += lstVw_SelectedIndexChanged;

            AddHandlers();
            GetExtraResources();
            UpdateButtonState();
        }

        private void GetExtraResources() {
            btnRemoveRequerente.Image = SharedResourcesOld.CurrentSharedResources.Apagar;
            btn_AddRequerenteInicial.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
            btn_EditRequerenteInicial.Image = SharedResourcesOld.CurrentSharedResources.Editar;

            btnRemoveAverbamento.Image = SharedResourcesOld.CurrentSharedResources.Apagar;
            btn_Add_Averbamento.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
            btn_Edit_Averbamento.Image = SharedResourcesOld.CurrentSharedResources.Editar;

            btnAdd_Localizacao_Actual.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
            btnEdit_Localizacao_Actual.Image = SharedResourcesOld.CurrentSharedResources.Editar;
            btnRemoveLocActual.Image = SharedResourcesOld.CurrentSharedResources.Apagar;

            btn_AddLocalizacaoAntiga.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
            btn_EditLocalizacaoAntiga.Image = SharedResourcesOld.CurrentSharedResources.Editar;
            btnRemoveLocAntiga.Image = SharedResourcesOld.CurrentSharedResources.Apagar;

            btnAddTecnicoObra.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
            btnRemoveTecnicoObra.Image = SharedResourcesOld.CurrentSharedResources.Apagar;

            btnRemoveAtestadoHab.Image = SharedResourcesOld.CurrentSharedResources.Apagar;
            btn_Add_AtestadoHab.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
            btn_Edit_AtestadoHab.Image = SharedResourcesOld.CurrentSharedResources.Editar;

            btnRemoveLocAntiga.Image = SharedResourcesOld.CurrentSharedResources.Apagar;

            btn_AddData.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
            btn_EditData.Image = SharedResourcesOld.CurrentSharedResources.Editar;
            btn_RemoverData.Image = SharedResourcesOld.CurrentSharedResources.Apagar;
        }

        private void AddHandlers() {
            lstVwLocalizacaoActual.KeyUp += lstVw_KeyUp;
            lstVwLocalizacaoAntiga.KeyUp += lstVw_KeyUp;
            lstVwTecnicoObra.KeyUp += lstVw_KeyUp;
            lstVwRequerente.KeyUp += lstVw_KeyUp;
            lstVwAverbamentos.KeyUp += lstVw_KeyUp;
            lstVwAtestadoHab.KeyUp += lstVw_KeyUp;
            lstVwLocalizacaoAntiga.KeyUp += lstVw_KeyUp;
            lstVwDatas.KeyUp += lstVw_KeyUp;
        }

        public void add_TextObservacoes_EventHandler(EventHandler txt_EventHandler) {
            this.txt_Observacoes.TextChanged += txt_EventHandler;
        }

        public void rem_TextObservacoes_EventHandler(EventHandler txt_EventHandler)
        {
            this.txt_Observacoes.TextChanged -= txt_EventHandler;
        }

        public void set_TextObservacoes(string observacoes) {
            this.txt_Observacoes.Text = observacoes;
        }

        public string get_TextObservacoes() {
            return this.txt_Observacoes.Text;
        }

        private GISADataset.FRDBaseRow CurrentFRDBase;
        private GISADataset.LicencaObraRow CurrentLicencaObra;

        public void LoadData(GISADataset.FRDBaseRow FRDBase, IDbConnection conn) {
            this.CurrentFRDBase = FRDBase;

            FRDRule.Current.LoadDadosLicencasDeObras(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, conn);
            var modRows = GisaDataSetHelper.GetInstance().IndexFRDCA.Cast<GISADataset.IndexFRDCARow>().Where(r => r.RowState != DataRowState.Unchanged).ToList();
            if (modRows.Count == 0)
                FRDRule.Current.LoadConteudoEEstrutura(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, conn);

            var licenca = GisaDataSetHelper.GetInstance().LicencaObra.Cast<GISADataset.LicencaObraRow>().Where(r => r.IDFRDBase == CurrentFRDBase.ID).SingleOrDefault();

            var tipoTipLicencaObra = GisaDataSetHelper.GetInstance().TipoTipologias.Cast<GISADataset.TipoTipologiasRow>().Single(r => r.BuiltInName.Equals("PROCESSO_DE_OBRAS"));
            var caLicencaObra = GisaDataSetHelper.GetInstance().IndexFRDCA.Cast<GISADataset.IndexFRDCARow>()
                .Where(idx => idx.RowState != DataRowState.Deleted && idx.IDFRDBase == CurrentFRDBase.ID && idx["Selector"] != DBNull.Value && idx.Selector == -1)
                .Select(idx => idx.ControloAutRow).SingleOrDefault(ca => ca.IDTipoNoticiaAut == (long)TipoNoticiaAut.TipologiaInformacional && ca.TipoTipologiasRow == tipoTipLicencaObra);

            if (licenca == null && caLicencaObra != null)
            {
                licenca = GisaDataSetHelper.GetInstance().LicencaObra.NewLicencaObraRow();
                licenca.FRDBaseRow = CurrentFRDBase;
                licenca.TipoObra = string.Empty;
                licenca.PropriedadeHorizontal = false;
                licenca.PHTexto = string.Empty;
                licenca.Versao = new byte[] { };
                licenca.isDeleted = 0;
                GisaDataSetHelper.GetInstance().LicencaObra.AddLicencaObraRow(licenca);
            }

            CurrentLicencaObra = licenca;
        }

        public void ModelToView() {
            PopulateRequerente_Inicial();
            PopulateRequerente_Averbamento();
            Populate_Actual_LocalizacaoObra();
            Populate_Antiga_LocalizacaoObra();
            PopulatePH();
            PopulateAtestadoHabitabilidade();

            PopulateTipoObra();
            PopulateTecnicoObra();

            PopulateDatas();
            PopulateObservacoes();
            UpdateButtonState();
        }


        #region Requerentes / proprietarios iniciais:
        private void btn_AddRequerenteInicial_Click(object sender, EventArgs e) {
            FormLeitura_Designacao formLeitura = new FormLeitura_Designacao();
            formLeitura.Title = "Requerente/proprietário inicial";
            formLeitura.LabelTitle = "Nome do requerente/proprietário";
            GISADataset.LicencaObraRequerentesRow requerente = null;

            switch (formLeitura.ShowDialog()) {
                case DialogResult.OK:
                    if (!existe_LicencaObraRequerenteActual(formLeitura.Designacao)) {
                        requerente = GisaDataSetHelper.GetInstance().LicencaObraRequerentes.NewLicencaObraRequerentesRow();
                        requerente.IDFRDBase = CurrentLicencaObra.IDFRDBase;
                        requerente.Nome = formLeitura.Designacao;
                        // TIPO = 'INICIAL'
                        requerente.Tipo = "INICIAL";
                        requerente.isDeleted = 0;
                        // Dados:
                        GisaDataSetHelper.GetInstance().LicencaObraRequerentes.AddLicencaObraRequerentesRow(requerente);
                        // GUI:
                        ListViewItem item = this.lstVwRequerente.Items.Add(requerente.Nome);
                        item.Tag = requerente;
                    }

                    break;
            }
        }

        private void btn_EditRequerenteInicial_Click(object sender, EventArgs e)
        {
            FormLeitura_Designacao formLeitura = new FormLeitura_Designacao();
            formLeitura.Title = "Requerente/proprietário inicial";
            formLeitura.LabelTitle = "Nome do requerente/proprietário";
            var requerente = (GISADataset.LicencaObraRequerentesRow)this.lstVwRequerente.SelectedItems[0].Tag;
            formLeitura.Designacao = requerente.Nome;

            switch (formLeitura.ShowDialog())
            {
                case DialogResult.OK:
                    if (!existe_LicencaObraRequerenteActual(formLeitura.Designacao))
                    {
                        requerente.Nome = formLeitura.Designacao;
                        // GUI:
                        this.lstVwRequerente.SelectedItems[0].Text = requerente.Nome;
                    }

                    break;
            }
        }

        private void PopulateRequerente_Inicial() {
            lstVwRequerente.Items.Clear();
            // TIPO = 'INICIAL'
            var requerentes =
                GisaDataSetHelper.GetInstance().LicencaObraRequerentes.Cast<GISADataset.LicencaObraRequerentesRow>().Where(r => r.RowState != DataRowState.Deleted && r.IDFRDBase == CurrentFRDBase.ID && r.Tipo.ToUpper().Trim().Equals("INICIAL") );

            foreach (GISADataset.LicencaObraRequerentesRow reqRow in requerentes) {
                ListViewItem item = this.lstVwRequerente.Items.Add(reqRow.Nome);
                item.Tag = reqRow;
            }
        }

        private bool existe_LicencaObraRequerenteActual(string novo_nome) {
            // TIPO = 'INICIAL'
            var requerentes =
                GisaDataSetHelper.GetInstance().LicencaObraRequerentes.Cast<GISADataset.LicencaObraRequerentesRow>().Where(r => r.RowState != DataRowState.Deleted && r.isDeleted == 0 && r.IDFRDBase == CurrentFRDBase.ID && r.Tipo.ToUpper().Trim().Equals("INICIAL"));

            foreach (GISADataset.LicencaObraRequerentesRow reqRow in requerentes)
                if (reqRow.isDeleted == 0 && reqRow.IDFRDBase == CurrentFRDBase.ID && reqRow.Nome.Equals(novo_nome))
                    return true;
            return false;
        }

        private void btnRemoveRequerente_Click(object sender, EventArgs e) {
            GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwRequerente);
            // registar eliminação
            ((frmMain)TopLevelControl).CurrentContext.RaiseRegisterModificationEvent(CurrentFRDBase);
        }

        #endregion

        #region Averbamentos de proprietarios / requerentes

        private void btn_Add_Averbamento_Click(object sender, EventArgs e) {
            FormLeitura_Designacao formLeitura = new FormLeitura_Designacao();
            formLeitura.Title = "Averbamento de requerente/proprietário";
            formLeitura.LabelTitle = "Nome do requerente/proprietário";
            GISADataset.LicencaObraRequerentesRow requerente = null;

            switch (formLeitura.ShowDialog()) {
                case DialogResult.OK:
                    if (!existe_LicencaObraRequerenteAvrb(formLeitura.Designacao)) {
                        requerente = GisaDataSetHelper.GetInstance().LicencaObraRequerentes.NewLicencaObraRequerentesRow();
                        requerente.LicencaObraRow = CurrentLicencaObra;
                        requerente.Nome = formLeitura.Designacao;
                        // TIPO = 'AVRB'
                        requerente.Tipo = "AVRB";
                        requerente.isDeleted = 0;
                        // Dados:
                        GisaDataSetHelper.GetInstance().LicencaObraRequerentes.AddLicencaObraRequerentesRow(requerente);
                        // GUI:
                        ListViewItem item = this.lstVwAverbamentos.Items.Add(requerente.Nome);
                        item.Tag = requerente;
                    }

                    break;
            }
        }

        private void btn_Edit_Averbamento_Click(object sender, EventArgs e)
        {
            FormLeitura_Designacao formLeitura = new FormLeitura_Designacao();
            formLeitura.Title = "Averbamento de requerente/proprietário";
            formLeitura.LabelTitle = "Nome do requerente/proprietário";
            var requerente = (GISADataset.LicencaObraRequerentesRow)this.lstVwAverbamentos.SelectedItems[0].Tag;
            formLeitura.Designacao = requerente.Nome;

            switch (formLeitura.ShowDialog())
            {
                case DialogResult.OK:
                    if (!existe_LicencaObraRequerenteActual(formLeitura.Designacao))
                    {
                        requerente.Nome = formLeitura.Designacao;
                        // GUI:
                        this.lstVwAverbamentos.SelectedItems[0].Text = requerente.Nome;
                    }

                    break;
            }
        }

        private bool existe_LicencaObraRequerenteAvrb(string novo_nome) {
            // TIPO = 'AVRB'
            var requerentes =
                GisaDataSetHelper.GetInstance().LicencaObraRequerentes.Cast<GISADataset.LicencaObraRequerentesRow>().Where(r => r.RowState != DataRowState.Deleted && r.IDFRDBase == CurrentFRDBase.ID && r.Tipo.ToUpper().Trim().Equals("AVRB"));
            foreach (GISADataset.LicencaObraRequerentesRow reqRow in requerentes)
                if (reqRow.isDeleted == 0 && reqRow.IDFRDBase == CurrentFRDBase.ID && reqRow.Nome.Equals(novo_nome))
                    return true;
            return false;
        }

        private void PopulateRequerente_Averbamento() {
            lstVwAverbamentos.Items.Clear();
            // TIPO = 'AVRB'
            var requerentes =
                GisaDataSetHelper.GetInstance().LicencaObraRequerentes.Cast<GISADataset.LicencaObraRequerentesRow>().Where(r => r.RowState != DataRowState.Deleted && r.IDFRDBase == CurrentFRDBase.ID && r.Tipo.ToUpper().Trim().Equals("AVRB"));
            foreach (GISADataset.LicencaObraRequerentesRow reqRow in requerentes) {
                ListViewItem item = this.lstVwAverbamentos.Items.Add(reqRow.Nome);
                item.Tag = reqRow;
            }
        }

        private void btnRemoveAverbamento_Click(object sender, EventArgs e) {
            GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwAverbamentos);
            // registar eliminação
            ((frmMain)TopLevelControl).CurrentContext.RaiseRegisterModificationEvent(CurrentFRDBase);
        }

        #endregion

        #region Tipo de obra
        private void PopulateTipoObra() {
            GISADataset.LicencaObraRow[] licencas =
                (GISADataset.LicencaObraRow[])(GisaDataSetHelper.GetInstance().Tables["LicencaObra"].Select("IDFRDBase = " + CurrentFRDBase.ID ));
            if (licencas.Count() > 0)
                textBox_TipoObra.Text = licencas[0].TipoObra;
            else
                textBox_TipoObra.Text = string.Empty;
        }

        #endregion

        #region PH
        private void PopulatePH() {
            GISADataset.LicencaObraRow[] licencas =
                (GISADataset.LicencaObraRow[])(GisaDataSetHelper.GetInstance().Tables["LicencaObra"].Select("IDFRDBase = " + CurrentFRDBase.ID));
            if (licencas.Count() > 0) {
                chkBoxPH.Checked = licencas[0].PropriedadeHorizontal;
                txtPH.Text = licencas[0].PHTexto;
            }
            else {
                chkBoxPH.Checked = false;
                txtPH.Text = string.Empty;
            }
        }

        #endregion

        #region Atestado Habitabilidade

        private void btn_Add_AtestadoHab_Click(object sender, EventArgs e) {
            FormLeitura_Designacao formLeitura = new FormLeitura_Designacao();
            formLeitura.Title = "Atestado de Habitabilidade";
            formLeitura.LabelTitle = "Código do atestado";
            GISADataset.LicencaObraAtestadoHabitabilidadeRow atestado = null;
            switch (formLeitura.ShowDialog()) {
                case DialogResult.OK:
                    if (!existe_AtestadoHab(formLeitura.Designacao)) {
                        atestado = GisaDataSetHelper.GetInstance().LicencaObraAtestadoHabitabilidade.NewLicencaObraAtestadoHabitabilidadeRow();
                        atestado.LicencaObraRow = CurrentLicencaObra;
                        atestado.Codigo = formLeitura.Designacao;
                        atestado.isDeleted = 0;
                        // Dados:
                        GisaDataSetHelper.GetInstance().LicencaObraAtestadoHabitabilidade.AddLicencaObraAtestadoHabitabilidadeRow(atestado);
                        // GUI:
                        ListViewItem item = this.lstVwAtestadoHab.Items.Add(atestado.Codigo);
                        item.Tag = atestado;
                    }
                    break;
            }
        }

        private void btn_Edit_AtestadoHab_Click(object sender, EventArgs e)
        {
            FormLeitura_Designacao formLeitura = new FormLeitura_Designacao();
            formLeitura.Title = "Atestado de Habitabilidade";
            formLeitura.LabelTitle = "Código do atestado";
            var atestado = (GISADataset.LicencaObraAtestadoHabitabilidadeRow)this.lstVwAtestadoHab.SelectedItems[0].Tag;
            formLeitura.Designacao = atestado.Codigo;

            switch (formLeitura.ShowDialog())
            {
                case DialogResult.OK:
                    if (!existe_AtestadoHab(formLeitura.Designacao))
                    {
                        atestado.Codigo = formLeitura.Designacao;
                        // GUI:
                        this.lstVwAtestadoHab.SelectedItems[0].Text = atestado.Codigo;
                    }

                    break;
            }
        }

        private bool existe_AtestadoHab(string novo_codigo) {
            GISADataset.LicencaObraAtestadoHabitabilidadeRow[] atestados =
                (GISADataset.LicencaObraAtestadoHabitabilidadeRow[])(GisaDataSetHelper.GetInstance().Tables["LicencaObraAtestadoHabitabilidade"].Select("IDFRDBase = " + CurrentFRDBase.ID));
            foreach (GISADataset.LicencaObraAtestadoHabitabilidadeRow row in atestados)
                if (row.isDeleted == 0 && row.IDFRDBase == CurrentFRDBase.ID && row.Codigo.Equals(novo_codigo))
                    return true;
            return false;
        }

        private void PopulateAtestadoHabitabilidade() {
            this.lstVwAtestadoHab.Items.Clear();
            GISADataset.LicencaObraAtestadoHabitabilidadeRow[] atestados =
                (GISADataset.LicencaObraAtestadoHabitabilidadeRow[])(GisaDataSetHelper.GetInstance().Tables["LicencaObraAtestadoHabitabilidade"].Select("IDFRDBase = " + CurrentFRDBase.ID));

            foreach (GISADataset.LicencaObraAtestadoHabitabilidadeRow habRow in atestados) {
                ListViewItem item = this.lstVwAtestadoHab.Items.Add(habRow.Codigo);
                item.Tag = habRow;
            }
        }

        private void btnRemoveAtestadoHab_Click(object sender, EventArgs e) {
            GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwAtestadoHab);
            // registar eliminação
            ((frmMain)TopLevelControl).CurrentContext.RaiseRegisterModificationEvent(CurrentFRDBase);
        }

        #endregion

        #region Data da licenca de construccao
        private void PopulateDatas() {
            this.lstVwDatas.Items.Clear();
            GISADataset.LicencaObraDataLicencaConstrucaoRow[] datas =
                (GISADataset.LicencaObraDataLicencaConstrucaoRow[])(GisaDataSetHelper.GetInstance().Tables["LicencaObraDataLicencaConstrucao"].Select("IDFRDBase = " + CurrentFRDBase.ID));
            foreach (GISADataset.LicencaObraDataLicencaConstrucaoRow row in datas)
                Add_ViewDatas(row);
        }

        private void btn_AddData_Click(object sender, EventArgs e) {
            FormLeituraPxDateBox formLeitura = new FormLeituraPxDateBox();
            GISADataset.LicencaObraDataLicencaConstrucaoRow new_data = null;

            switch (formLeitura.ShowDialog()) {
                case DialogResult.OK:
                    new_data = GisaDataSetHelper.GetInstance().LicencaObraDataLicencaConstrucao.NewLicencaObraDataLicencaConstrucaoRow();
                    new_data.LicencaObraRow = CurrentLicencaObra;
                    new_data.Ano = formLeitura.ValueYear;
                    new_data.Mes = formLeitura.ValueMonth;
                    new_data.Dia = formLeitura.ValueDay;
                    new_data.isDeleted = 0;
                    if (!existe_DataLicencaObra(formLeitura.ValueYear, formLeitura.ValueMonth, formLeitura.ValueDay)) {
                        GisaDataSetHelper.GetInstance().LicencaObraDataLicencaConstrucao.AddLicencaObraDataLicencaConstrucaoRow(new_data);
                        Add_ViewDatas(new_data);
                    }
                    break;
            }
        }

        private void btn_EditData_Click(object sender, EventArgs e)
        {
            FormLeituraPxDateBox formLeitura = new FormLeituraPxDateBox();
            var new_data = (GISADataset.LicencaObraDataLicencaConstrucaoRow)this.lstVwDatas.SelectedItems[0].Tag;
            formLeitura.ValueYear = new_data.Ano;
            formLeitura.ValueMonth = new_data.Mes;
            formLeitura.ValueDay = new_data.Dia;

            switch (formLeitura.ShowDialog())
            {
                case DialogResult.OK:
                    new_data.Ano = formLeitura.ValueYear;
                    new_data.Mes = formLeitura.ValueMonth;
                    new_data.Dia = formLeitura.ValueDay;
                    if (!existe_DataLicencaObra(formLeitura.ValueYear, formLeitura.ValueMonth, formLeitura.ValueDay))
                    {
                        var item = this.lstVwDatas.SelectedItems[0];
                        string str_data = build_str_data(new_data.IsAnoNull() ? string.Empty : new_data.Ano,
                                            new_data.IsMesNull() ? string.Empty : new_data.Mes,
                                            new_data.IsDiaNull() ? string.Empty : new_data.Dia);
                        item.Text = str_data;
                    }
                    break;
            }
        }

        private bool existe_DataLicencaObra(string ano, string mes, string dia) {
            GISADataset.LicencaObraDataLicencaConstrucaoRow[] datas =
                (GISADataset.LicencaObraDataLicencaConstrucaoRow[])(GisaDataSetHelper.GetInstance().Tables["LicencaObraDataLicencaConstrucao"].Select("IDFRDBase = " + CurrentFRDBase.ID));
            foreach (GISADataset.LicencaObraDataLicencaConstrucaoRow dataRow in datas) {
                bool eqAno = (ano.Equals(string.Empty) && dataRow.IsAnoNull()) || ano.Equals(dataRow.Ano);
                bool eqMes = (mes.Equals(string.Empty) && dataRow.IsMesNull()) || mes.Equals(dataRow.Mes);
                bool eqDia = (dia.Equals(string.Empty) && dataRow.IsDiaNull()) || mes.Equals(dataRow.Dia);
                if (eqAno && eqMes && eqDia)
                    return true;
            }
            return false;
        }

        private void Add_ViewDatas(GISADataset.LicencaObraDataLicencaConstrucaoRow new_data) {
            string str_data = build_str_data(new_data.IsAnoNull() ? string.Empty : new_data.Ano,
                new_data.IsMesNull() ? string.Empty : new_data.Mes,
                new_data.IsDiaNull() ? string.Empty : new_data.Dia);
            ListViewItem item = this.lstVwDatas.Items.Add(str_data);
            item.Tag = new_data;

        }
        private string build_str_data(string Ano, string Mes, string Dia) {
            return (Ano.Equals(string.Empty) ? "____" : Ano) + "/" +
                   (Mes.Equals(string.Empty) ? "__" : Mes) + "/" +
                   (Dia.Equals(string.Empty) ? "__" : Dia);
        }


        private void btn_RemoverData_Click(object sender, EventArgs e) {
            GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwDatas);
            // registar eliminação
            ((frmMain)TopLevelControl).CurrentContext.RaiseRegisterModificationEvent(CurrentFRDBase);
        }

        #endregion

        private void PopulateObservacoes() {

            if (this.txt_Observacoes.Text.Equals(string.Empty)) {
                GISADataset.SFRDConteudoEEstruturaRow[] conteudo =
                    (GISADataset.SFRDConteudoEEstruturaRow[])(GisaDataSetHelper.GetInstance().Tables["SFRDConteudoEEstrutura"].Select("IDFRDBase = " + CurrentFRDBase.ID));

                if (conteudo.Count() > 0 && !conteudo[0].IsConteudoInformacionalNull() && !conteudo[0].ConteudoInformacional.Equals(string.Empty)) {
                    this.txt_Observacoes.Text = conteudo[0].ConteudoInformacional;
                }
                //else
                //    this.txt_Observacoes.Text = string.Empty;
            }
        }


        public void ViewToModel() {
            GISADataset.SFRDConteudoEEstruturaRow[] conteudo =
                (GISADataset.SFRDConteudoEEstruturaRow[])(GisaDataSetHelper.GetInstance().Tables["SFRDConteudoEEstrutura"].Select("IDFRDBase = " + CurrentFRDBase.ID));
            if (conteudo.Count() > 0) {
                conteudo[0].ConteudoInformacional = this.txt_Observacoes.Text;
            }
            else {
                GISADataset.SFRDConteudoEEstruturaRow newRow = GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.NewSFRDConteudoEEstruturaRow();
                newRow.FRDBaseRow = CurrentFRDBase;
                newRow.ConteudoInformacional = this.txt_Observacoes.Text;
            }

            // Licenca de obra: tipo de obra e PH
            CurrentLicencaObra.TipoObra = textBox_TipoObra.Text;
            CurrentLicencaObra.PropriedadeHorizontal = this.chkBoxPH.Checked;
            CurrentLicencaObra.PHTexto = txtPH.Text.Trim();
        }

        public void Deactivate()
        {
            GUIHelper.GUIHelper.clearField(lstVwRequerente);
            GUIHelper.GUIHelper.clearField(lstVwLocalizacaoActual);
            GUIHelper.GUIHelper.clearField(lstVwLocalizacaoAntiga);
            GUIHelper.GUIHelper.clearField(lstVwAverbamentos);
            GUIHelper.GUIHelper.clearField(lstVwAtestadoHab);
            GUIHelper.GUIHelper.clearField(textBox_TipoObra);
            GUIHelper.GUIHelper.clearField(lstVwTecnicoObra);
            GUIHelper.GUIHelper.clearField(lstVwDatas);
            GUIHelper.GUIHelper.clearField(txt_Observacoes);
        }

        private void lstVw_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyValue == Convert.ToInt32(Keys.Delete)) {
                GUIHelper.GUIHelper.deleteSelectedLstVwItems((ListView)sender);
                // registar eliminação
                ((frmMain)TopLevelControl).CurrentContext.RaiseRegisterModificationEvent(CurrentFRDBase);
            }
        }

        private void lstVw_SelectedIndexChanged(object sender, System.EventArgs e) {
            UpdateButtonState();
        }

        private void UpdateButtonState() {
            btn_EditRequerenteInicial.Enabled = lstVwRequerente.SelectedItems.Count > 0;
            btn_Edit_Averbamento.Enabled = lstVwAverbamentos.SelectedItems.Count > 0;
            btn_Edit_AtestadoHab.Enabled = lstVwAtestadoHab.SelectedItems.Count > 0;
            btn_EditLocalizacaoAntiga.Enabled = lstVwLocalizacaoAntiga.SelectedItems.Count > 0;

            btnEdit_Localizacao_Actual.Enabled = lstVwLocalizacaoActual.SelectedItems.Count > 0;
            btn_EditData.Enabled = lstVwDatas.SelectedItems.Count > 0;

            btnRemoveRequerente.Enabled = lstVwRequerente.SelectedItems.Count > 0;
            btnRemoveAverbamento.Enabled = lstVwAverbamentos.SelectedItems.Count > 0;
            btnRemoveAtestadoHab.Enabled = lstVwAtestadoHab.SelectedItems.Count > 0;
            btnRemoveLocAntiga.Enabled = lstVwLocalizacaoAntiga.SelectedItems.Count > 0;

            btnRemoveLocActual.Enabled = lstVwLocalizacaoActual.SelectedItems.Count > 0;
            btnRemoveTecnicoObra.Enabled = lstVwTecnicoObra.SelectedItems.Count > 0;
            btn_RemoverData.Enabled = lstVwDatas.SelectedItems.Count > 0;
        }

        #region Localizacao da obra (actual)
        private void Populate_Actual_LocalizacaoObra() {
            lstVwLocalizacaoActual.Items.Clear();
            GISADataset.LicencaObraLocalizacaoObraActualRow[] licencas =
                (GISADataset.LicencaObraLocalizacaoObraActualRow[])(GisaDataSetHelper.GetInstance().Tables["LicencaObraLocalizacaoObraActual"].Select("IDFRDBase = " + CurrentFRDBase.ID));

            foreach (GISADataset.LicencaObraLocalizacaoObraActualRow locActualRow in licencas) {
                GISADataset.ControloAutDicionarioRow[] dictRowList = (GISADataset.ControloAutDicionarioRow[])GisaDataSetHelper.GetInstance().ControloAutDicionario.Select("IDControloAut = " + locActualRow.IDControloAut + " AND IDTipoControloAutForma = 1");
                if (dictRowList.Length > 0)
                    Add_ViewLocalizacaoActual(dictRowList[0], locActualRow);
            }
        }

        // Adicionar uma localizacao actual (registo de autoridade geográfico):
        private void btnAdd_Localizacao_Actual_Click(object sender, EventArgs e) {
            GISADataset.ControloAutRow caRow = null;
            GISADataset.LicencaObraLocalizacaoObraActualRow locActualRow = null;
            GISADataset.ControloAutDicionarioRow dicionarioRow = null;

            FormLeituraLocalizacaoNumPolicia formLeitura = new FormLeituraLocalizacaoNumPolicia();
            formLeitura.ModoTextoLivre = false;

            switch (formLeitura.ShowDialog()) {
                case DialogResult.OK:
                    dicionarioRow = formLeitura.ControloAutDicionarioRow;
                    caRow = dicionarioRow.ControloAutRow;
                    if (!existe_LicencaObraLocalizacaoObraActual(caRow, formLeitura.NumeroPolicia)) {
                        locActualRow = GisaDataSetHelper.GetInstance().LicencaObraLocalizacaoObraActual.NewLicencaObraLocalizacaoObraActualRow();
                        locActualRow.LicencaObraRow = CurrentLicencaObra;
                        locActualRow.ControloAutRow = caRow;
                        locActualRow.NumPolicia = formLeitura.NumeroPolicia;
                        locActualRow.isDeleted = 0;
                        // Dados:
                        Add_Row_LocalizacaoActual(locActualRow);                        
                        // GUI: adicionar `a lista:
                        Add_ViewLocalizacaoActual(dicionarioRow, locActualRow);
                        UpdateButtonState();
                    }

                    break;
                default:
                    break;
            }

        }

        private void btnEdit_Localizacao_Actual_Click(object sender, EventArgs e)
        {
            var locActualRow = (GISADataset.LicencaObraLocalizacaoObraActualRow)this.lstVwLocalizacaoActual.SelectedItems[0].Tag;
            var caRow = locActualRow.ControloAutRow;
            var cadRow = caRow.GetControloAutDicionarioRows().Single(cad => cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada);

            FormLeituraLocalizacaoNumPolicia formLeitura = new FormLeituraLocalizacaoNumPolicia();
            formLeitura.ModoTextoLivre = false;
            formLeitura.Designacao = cadRow.DicionarioRow.Termo;
            formLeitura.NumeroPolicia = locActualRow.NumPolicia;
            formLeitura.ControloAutDicionarioRow = cadRow;

            switch (formLeitura.ShowDialog())
            {
                case DialogResult.OK:
                    cadRow = formLeitura.ControloAutDicionarioRow;
                    caRow = cadRow.ControloAutRow;
                    if (!existe_LicencaObraLocalizacaoObraActual(caRow, formLeitura.NumeroPolicia))
                    {
                        locActualRow.ControloAutRow = caRow;
                        locActualRow.NumPolicia = formLeitura.NumeroPolicia;
                        locActualRow.isDeleted = 0;
                        // GUI: actualizar
                        var item = this.lstVwLocalizacaoActual.SelectedItems[0];
                        item.Text = cadRow.DicionarioRow.Termo;
                        item.SubItems[1].Text = locActualRow.NumPolicia;
                        UpdateButtonState();
                    }

                    break;
                default:
                    break;
            }
        }

        // A verificacao de igualdade baseia-se nos IDs do ControloAut e FRDBase e no numero de policia:
        private bool existe_LicencaObraLocalizacaoObraActual(GISADataset.ControloAutRow caRow, string numeroPolicia) {
            GISADataset.LicencaObraLocalizacaoObraActualRow[] licencasLoc =
                (GISADataset.LicencaObraLocalizacaoObraActualRow[])(GisaDataSetHelper.GetInstance().Tables["LicencaObraLocalizacaoObraActual"].Select("IDFRDBase = " + CurrentFRDBase.ID));
            foreach (GISADataset.LicencaObraLocalizacaoObraActualRow locRow in licencasLoc)
                if (locRow.isDeleted == 0 && locRow.NumPolicia.Equals(numeroPolicia) && locRow.IDFRDBase == CurrentFRDBase.ID && locRow.IDControloAut == caRow.ID)
                    return true;
            return false;
        }

        private void Add_Row_LocalizacaoActual(GISADataset.LicencaObraLocalizacaoObraActualRow new_locActualRow) {
            bool exists = false;
            GISADataset.LicencaObraLocalizacaoObraActualRow[] licencasLoc =
                (GISADataset.LicencaObraLocalizacaoObraActualRow[])(GisaDataSetHelper.GetInstance().Tables["LicencaObraLocalizacaoObraActual"].Select("IDFRDBase = " + CurrentFRDBase.ID));

            foreach (GISADataset.LicencaObraLocalizacaoObraActualRow locRow in licencasLoc) {
                if (locRow.NumPolicia.Equals(new_locActualRow.NumPolicia) && locRow.IDFRDBase == CurrentFRDBase.ID && locRow.IDControloAut == new_locActualRow.IDControloAut) {
                    exists = true;
                    if (locRow.isDeleted == 1)
                        locRow.isDeleted = 0;
                    break;
                }
            }
            if (!exists)
                GisaDataSetHelper.GetInstance().LicencaObraLocalizacaoObraActual.AddLicencaObraLocalizacaoObraActualRow(new_locActualRow);
        }

        private void Add_ViewLocalizacaoActual(GISADataset.ControloAutDicionarioRow dictRow, GISADataset.LicencaObraLocalizacaoObraActualRow new_locActualRow) {
            // Nome:
            ListViewItem item = new ListViewItem(dictRow.DicionarioRow.Termo);
            // Numero:
            item.SubItems.Add(new_locActualRow.NumPolicia);
            item.Tag = new_locActualRow;
            this.lstVwLocalizacaoActual.Items.Add(item);
        }


        private void btnRemoveLocActual_Click(object sender, EventArgs e) {
            GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwLocalizacaoActual);
            // registar eliminação
            ((frmMain)TopLevelControl).CurrentContext.RaiseRegisterModificationEvent(CurrentFRDBase);
        }

        #endregion

        #region Localizacao de obra (antiga)

        private void Populate_Antiga_LocalizacaoObra() {
            lstVwLocalizacaoAntiga.Items.Clear();
            GISADataset.LicencaObraLocalizacaoObraAntigaRow[] licencas =
                (GISADataset.LicencaObraLocalizacaoObraAntigaRow[])(GisaDataSetHelper.GetInstance().Tables["LicencaObraLocalizacaoObraAntiga"].Select("IDFRDBase = " + CurrentFRDBase.ID));

            foreach (GISADataset.LicencaObraLocalizacaoObraAntigaRow locRow in licencas) {
                ListViewItem item = new ListViewItem(locRow.NomeLocal);
                item.SubItems.Add(locRow.NumPolicia.ToString());
                item.Tag = locRow;
                this.lstVwLocalizacaoAntiga.Items.Add(item);
            }
        }

        // Adicionar uma localizacao antiga
        private void btn_AddLocalizacaoAntiga_Click(object sender, EventArgs e) {
            FormLeituraLocalizacaoNumPolicia formLeitura = new FormLeituraLocalizacaoNumPolicia();
            formLeitura.Title = "Designação antiga do local";
            formLeitura.ModoTextoLivre = true;
            GISADataset.LicencaObraLocalizacaoObraAntigaRow locAntigaRow = null;

            string designacao = string.Empty;
            string numeroPolicia = string.Empty;

            switch (formLeitura.ShowDialog()) {
                case DialogResult.OK:
                    designacao = formLeitura.Designacao;
                    numeroPolicia = formLeitura.NumeroPolicia;
                    if (!existe_LicencaObraLocalizacaoObraAntiga(designacao, numeroPolicia)) {
                        locAntigaRow = GisaDataSetHelper.GetInstance().LicencaObraLocalizacaoObraAntiga.NewLicencaObraLocalizacaoObraAntigaRow();
                        locAntigaRow.LicencaObraRow = CurrentLicencaObra;
                        locAntigaRow.NomeLocal = designacao;
                        locAntigaRow.NumPolicia = numeroPolicia;
                        locAntigaRow.isDeleted = 0;
                        GisaDataSetHelper.GetInstance().LicencaObraLocalizacaoObraAntiga.AddLicencaObraLocalizacaoObraAntigaRow(locAntigaRow);
                        // GUI: adicionar `a lista:
                        Add_ViewLocalizacaoAntiga(locAntigaRow);
                    }

                    break;
                default:
                    break;
            }
        }

        private void btn_EditLocalizacaoAntiga_Click(object sender, EventArgs e)
        {
            FormLeituraLocalizacaoNumPolicia formLeitura = new FormLeituraLocalizacaoNumPolicia();
            formLeitura.Title = "Designação antiga do local";
            formLeitura.ModoTextoLivre = true;
            var locAntigaRow = (GISADataset.LicencaObraLocalizacaoObraAntigaRow)this.lstVwLocalizacaoAntiga.SelectedItems[0].Tag;
            formLeitura.NumeroPolicia = locAntigaRow.NumPolicia;
            formLeitura.Designacao = locAntigaRow.NomeLocal;

            string designacao = string.Empty;
            string numeroPolicia = string.Empty;

            switch (formLeitura.ShowDialog())
            {
                case DialogResult.OK:
                    designacao = formLeitura.Designacao;
                    numeroPolicia = formLeitura.NumeroPolicia;
                    if (!existe_LicencaObraLocalizacaoObraAntiga(designacao, numeroPolicia))
                    {
                        locAntigaRow.NomeLocal = designacao;
                        locAntigaRow.NumPolicia = numeroPolicia;
                        // GUI: actualizar
                        var item = this.lstVwLocalizacaoAntiga.SelectedItems[0];
                        item.Text = designacao;
                        item.SubItems[1].Text = numeroPolicia;
                    }

                    break;
                default:
                    break;
            }
        }

        private bool existe_LicencaObraLocalizacaoObraAntiga(string designacao, string numeroPolicia) {
            GISADataset.LicencaObraLocalizacaoObraAntigaRow[] licencasLoc =
                (GISADataset.LicencaObraLocalizacaoObraAntigaRow[])(GisaDataSetHelper.GetInstance().Tables["LicencaObraLocalizacaoObraAntiga"].Select("IDFRDBase = " + CurrentFRDBase.ID));
            foreach (GISADataset.LicencaObraLocalizacaoObraAntigaRow locRow in licencasLoc)
                if (locRow.NomeLocal.Equals(designacao) && locRow.NumPolicia.Equals(numeroPolicia))
                    return true;
            return false;
        }

        private void Add_ViewLocalizacaoAntiga(GISADataset.LicencaObraLocalizacaoObraAntigaRow locAntigaRow) {
            // Nome:
            ListViewItem item = new ListViewItem(locAntigaRow.NomeLocal);
            // Numero:
            item.SubItems.Add(locAntigaRow.NumPolicia);
            item.Tag = locAntigaRow;
            this.lstVwLocalizacaoAntiga.Items.Add(item);
        }

        private void btnRemoveLocAntiga_Click(object sender, EventArgs e) {
            GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwLocalizacaoAntiga);
            // registar eliminação
            ((frmMain)TopLevelControl).CurrentContext.RaiseRegisterModificationEvent(CurrentFRDBase);
        }
        #endregion

        #region Tecnico de obra (registo de autoridade onomástico)

        private void PopulateTecnicoObra() {
            this.lstVwTecnicoObra.Items.Clear();
            GISADataset.LicencaObraTecnicoObraRow[] tecnicos =
                (GISADataset.LicencaObraTecnicoObraRow[])(GisaDataSetHelper.GetInstance().Tables["LicencaObraTecnicoObra"].Select("IDFRDBase = " + CurrentFRDBase.ID));

            GISADataset.ControloAutDicionarioRow[] dicionarioRow_array;
            //GISADataset.ControloAutDicionarioRow dicionarioRow = null;

            foreach (GISADataset.LicencaObraTecnicoObraRow tecnico in tecnicos) {
                dicionarioRow_array = ((GISADataset.ControloAutDicionarioRow[])
                    //(GisaDataSetHelper.GetInstance().ControloAutDicionario.Select("IDControloAut = " + tecnico.ControloAutRow.ID + " AND IDTipoControloAutForma = 1")));
                    (GisaDataSetHelper.GetInstance().ControloAutDicionario.Select("IDControloAut = " + tecnico.IDControloAut + " AND IDTipoControloAutForma = 1")));
                if (dicionarioRow_array.Length > 0) {
                    //dicionarioRow = dicionarioRow_array[0];
                    Add_ViewTecnicoObra(dicionarioRow_array[0], tecnico);
                }
            }
        }

        // Adicionar um tecnico de obra (registo de autoridade onomastico):
        private void btnAddTecnicoObra_Click(object sender, EventArgs e) {
            FormPickControloAut frmPick = new FormPickControloAut();
            frmPick.Text = "Notícia de autoridade - Pesquisar registo de autoridade onomástico";
            frmPick.caList.AllowedNoticiaAut(TipoNoticiaAut.Onomastico);
            frmPick.caList.ReloadList();

            GISADataset.ControloAutRow caRow = null;
            GISADataset.LicencaObraTecnicoObraRow tecnicoRow = null;
            GISADataset.ControloAutDicionarioRow cadRow = null;

            if (frmPick.caList.Items.Count > 0)
                frmPick.caList.SelectItem(frmPick.caList.Items[0]);

            switch (frmPick.ShowDialog()) {
                case DialogResult.OK:
                    foreach (ListViewItem li in frmPick.caList.SelectedItems) {
                        cadRow = (GISADataset.ControloAutDicionarioRow)li.Tag;
                        caRow = cadRow.ControloAutRow;
                        if (!existe_LicencaObraTecnicoObra(caRow)) {

                            if (cadRow.IDTipoControloAutForma != (long)TipoControloAutForma.FormaAutorizada)
                            {
                                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                                try
                                {
                                    ControloAutRule.Current.LoadDicionarioAndControloAutDicionario(GisaDataSetHelper.GetInstance(), cadRow.IDControloAut, ho.Connection);
                                }
                                catch (Exception ex)
                                {
                                    Trace.WriteLine(ex);
                                    throw;
                                }
                                finally
                                {
                                    ho.Dispose();
                                }
                                cadRow = caRow.GetControloAutDicionarioRows().Where(cad => cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada).Single();
                            }

                            tecnicoRow = GisaDataSetHelper.GetInstance().LicencaObraTecnicoObra.NewLicencaObraTecnicoObraRow();
                            tecnicoRow.LicencaObraRow = CurrentLicencaObra;
                            tecnicoRow.ControloAutRow = caRow;
                            tecnicoRow.isDeleted = 0;
                            // Dados:
                            Add_RowTecnicoObra(tecnicoRow);
                            // GUI: adicionar `a lista:
                            Add_ViewTecnicoObra(cadRow, tecnicoRow);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private bool existe_LicencaObraTecnicoObra(GISADataset.ControloAutRow caRow) {
            GISADataset.LicencaObraTecnicoObraRow[] tecnicos =
                (GISADataset.LicencaObraTecnicoObraRow[])(GisaDataSetHelper.GetInstance().Tables["LicencaObraTecnicoObra"].Select("IDFRDBase = " + CurrentFRDBase.ID));
            foreach (GISADataset.LicencaObraTecnicoObraRow tRow in tecnicos)
                if (tRow.isDeleted == 0 && tRow.IDFRDBase == CurrentFRDBase.ID && tRow.IDControloAut == caRow.ID)
                    return true;
            return false;
        }

        private void Add_RowTecnicoObra(GISADataset.LicencaObraTecnicoObraRow new_tecnicoRow) {
            bool exists = false;
            GISADataset.LicencaObraTecnicoObraRow[] tecnicos =
                (GISADataset.LicencaObraTecnicoObraRow[])(GisaDataSetHelper.GetInstance().Tables["LicencaObraTecnicoObra"].Select("IDFRDBase = " + CurrentFRDBase.ID));

            foreach (GISADataset.LicencaObraTecnicoObraRow tRow in tecnicos) {
                if (tRow.IDFRDBase == CurrentFRDBase.ID && tRow.IDControloAut == new_tecnicoRow.IDControloAut) {
                    exists = true;
                    if (tRow.isDeleted == 1)
                        tRow.isDeleted = 0;
                    break;
                }
            }
            if (!exists)
                GisaDataSetHelper.GetInstance().LicencaObraTecnicoObra.AddLicencaObraTecnicoObraRow(new_tecnicoRow);
        }

        private void Add_ViewTecnicoObra(GISADataset.ControloAutDicionarioRow dictRow, GISADataset.LicencaObraTecnicoObraRow new_tecnicoRow) {
            // Nome do tecnico:
            ListViewItem item = new ListViewItem(dictRow.DicionarioRow.Termo);
            item.Tag = new_tecnicoRow;
            this.lstVwTecnicoObra.Items.Add(item);
        }

        public void btnRemoveTecnicoObra_Click(object sender, EventArgs e) {
            GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwTecnicoObra);
            // registar eliminação
            ((frmMain)TopLevelControl).CurrentContext.RaiseRegisterModificationEvent(CurrentFRDBase);
        }

        #endregion

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == copiarToolStripMenuItem)
            {
                var cms = sender as ContextMenuStrip;
                var lst = cms.SourceControl as ListView;

                var str = new StringBuilder();


                if (lst != null && lst.SelectedItems.Count > 0)
                    CopyToClipboard(lst.SelectedItems.Cast<ListViewItem>().ToList());
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            var cms = sender as ContextMenuStrip;
            var lst = cms.SourceControl as ListView;

            if (lst != null && lst.SelectedItems.Count == 0)
                e.Cancel = true;
        }

        private void lstVw_KeyDown(object sender, KeyEventArgs e)
        {
            var lst = sender as ListView;

            if (lst != null && lst.SelectedItems.Count > 0 && e.Control && e.KeyCode == Keys.C)
                CopyToClipboard(lst.SelectedItems.Cast<ListViewItem>().ToList());
        }

        private void CopyToClipboard(List<ListViewItem> lstItems)
        {
            var result = new StringBuilder();
            var strLine = new StringBuilder();

            foreach (var item in lstItems)
            {
                strLine = new StringBuilder();
                for (int i = 0; i < item.SubItems.Count; i++)
                    strLine.Append(item.SubItems[i].Text + "\t");

                result.AppendLine(strLine.ToString().TrimEnd('\t'));
            }

            Clipboard.SetText(result.ToString().TrimEnd(System.Environment.NewLine.ToCharArray()));
        }
    }
}