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

namespace GISA
{
    public partial class PanelConteudoInformacional : GISA.GISAPanel
    {
        private EventHandler txtConteudoInformacional_EventHandler;
        private EventHandler contInfLicencaObras_EventHandler;
        private long IDTipoNivelRelacionado;

        public PanelConteudoInformacional()
        {
            InitializeComponent();
        }

        private void txtConteudoInformacional_TextChanged(object sender, EventArgs e) {
            txtConteudoInformacional.TextChanged -= txtConteudoInformacional_EventHandler;
            this.contInfLicencaObras1.set_TextObservacoes(txtConteudoInformacional.Text);
            txtConteudoInformacional.TextChanged += txtConteudoInformacional_EventHandler;
        }

        private void contInfLicencaObras_TextChanged(object sender, EventArgs e) {
            this.txtConteudoInformacional.Text = this.contInfLicencaObras1.get_TextObservacoes();
        }

        private void txtConteudoInformacional_SD_TextChanged(object sender, EventArgs e)
        {
            txtConteudoInformacional.TextChanged -= txtConteudoInformacional_EventHandler;
            this.contInfLicencaObrasSD1.set_TextObservacoes(txtConteudoInformacional.Text);
            txtConteudoInformacional.TextChanged += txtConteudoInformacional_EventHandler;
        }

        private void contInfLicencaObras_SD_TextChanged(object sender, EventArgs e)
        {
            this.txtConteudoInformacional.Text = this.contInfLicencaObrasSD1.get_TextObservacoes();
        }

        private void ConfEvents()
        {
            if (this.contInfLicencaObras1.Visible)
            {
                this.txtConteudoInformacional_EventHandler = new EventHandler(txtConteudoInformacional_TextChanged);
                txtConteudoInformacional.TextChanged += txtConteudoInformacional_EventHandler;

                this.contInfLicencaObras_EventHandler = new EventHandler(contInfLicencaObras_TextChanged);
                this.contInfLicencaObras1.add_TextObservacoes_EventHandler(this.contInfLicencaObras_EventHandler);
            }
            else if (this.contInfLicencaObras1.Visible)
            {
                this.txtConteudoInformacional_EventHandler = new EventHandler(txtConteudoInformacional_SD_TextChanged);
                txtConteudoInformacional.TextChanged += txtConteudoInformacional_EventHandler;

                this.contInfLicencaObras_EventHandler = new EventHandler(contInfLicencaObras_SD_TextChanged);
                this.contInfLicencaObrasSD1.add_TextObservacoes_EventHandler(this.contInfLicencaObras_EventHandler);
            }
        }

        private void ResetConfEvents()
        {
            if (this.contInfLicencaObras1.Visible)
            {
                txtConteudoInformacional.TextChanged -= txtConteudoInformacional_EventHandler;
                this.contInfLicencaObras1.rem_TextObservacoes_EventHandler(this.contInfLicencaObras_EventHandler);
            }
            else if (this.contInfLicencaObras1.Visible)
            {
                txtConteudoInformacional.TextChanged -= txtConteudoInformacional_EventHandler;
                this.contInfLicencaObrasSD1.rem_TextObservacoes_EventHandler(this.contInfLicencaObras_EventHandler);
            }
        }


        private GISADataset.FRDBaseRow CurrentFRDBase;
        private GISADataset.SFRDConteudoEEstruturaRow CurrentSFRDConteudoEEstrutura;
        public override void LoadData(DataRow CurrentDataRow, IDbConnection conn) {
            IsLoaded = false;
            CurrentFRDBase = (GISADataset.FRDBaseRow)CurrentDataRow;

            var rhRow = CurrentFRDBase.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First();
            this.IDTipoNivelRelacionado = rhRow.IDTipoNivelRelacionado;

            string QueryFilter = "IDFRDBase=" + CurrentFRDBase.ID.ToString();

            ConfigureControlsVisibility(conn);

            if (this.IDTipoNivelRelacionado == (long)TipoNivelRelacionado.SD) 
                this.contInfLicencaObrasSD1.LoadData(CurrentFRDBase, conn);
            else
                this.contInfLicencaObras1.LoadData(CurrentFRDBase, conn);

            if (GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.Select(QueryFilter).Length != 0)
                CurrentSFRDConteudoEEstrutura = (GISADataset.SFRDConteudoEEstruturaRow)(GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.Select(QueryFilter)[0]);
            else
                CurrentSFRDConteudoEEstrutura = GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.AddSFRDConteudoEEstruturaRow(CurrentFRDBase, "", "", new byte[] { }, 0);

            IsLoaded = true;
        }

        public override void ModelToView()
        {
            IsPopulated = false;

            if (this.IDTipoNivelRelacionado == (long)TipoNivelRelacionado.SD)
                this.contInfLicencaObrasSD1.ModelToView();
            else
                this.contInfLicencaObras1.ModelToView();

            if (txtConteudoInformacional.Text.Equals(string.Empty)) {
                if (!CurrentSFRDConteudoEEstrutura.IsConteudoInformacionalNull() && !CurrentSFRDConteudoEEstrutura.ConteudoInformacional.Equals(string.Empty))
                    txtConteudoInformacional.Text = CurrentSFRDConteudoEEstrutura.ConteudoInformacional;
            }
            IsPopulated = true;
        }

        private void ConfigureControlsVisibility(IDbConnection conn) {
            if (!SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsLicObrEnable() && this.IDTipoNivelRelacionado != TipoNivelRelacionado.D && this.IDTipoNivelRelacionado != TipoNivelRelacionado.SD)
            {
                this.contInfLicencaObras1.Visible = false;
                this.contInfLicencaObrasSD1.Visible = false;
                this.txtConteudoInformacional.Visible = true;
                return;
            }

            // Pre-definicao:
            this.contInfLicencaObras1.Visible = false;
            this.contInfLicencaObrasSD1.Visible = false;
            this.txtConteudoInformacional.Visible = true;

            if (this.IDTipoNivelRelacionado == TipoNivelRelacionado.D)
            {
                var currentIndexFRDCA = GisaDataSetHelper.GetInstance().IndexFRDCA.Cast<GISADataset.IndexFRDCARow>().Where(
                    r => (r.RowState == DataRowState.Added || r.RowState == DataRowState.Modified || r.RowState == DataRowState.Unchanged)
                        && r.IDFRDBase == CurrentFRDBase.ID && !r.IsSelectorNull() && r.Selector == -1).SingleOrDefault();

                if (currentIndexFRDCA != null)
                {
                    GISADataset.ControloAutRow currentControloAut = currentIndexFRDCA.ControloAutRow;
                    if (IsRelatedToProcessoObras(currentControloAut))
                    {
                        this.contInfLicencaObras1.Visible = true;
                        this.contInfLicencaObrasSD1.Visible = false;
                        this.txtConteudoInformacional.Visible = false;
                    }
                }
                else
                {
                    var indexFRDCADeletedList = GisaDataSetHelper.GetInstance().IndexFRDCA.Cast<GISADataset.IndexFRDCARow>().Where(
                        r => r.RowState == DataRowState.Deleted
                            && (long)r["IDFRDBase", DataRowVersion.Original] == CurrentFRDBase.ID
                            && r["Selector", DataRowVersion.Original] != null
                            && r["Selector", DataRowVersion.Original].ToString().Equals("-1")).ToArray();

                    if (indexFRDCADeletedList.Length == 0 &&
                        FRDRule.Current.possuiDadosLicencaDeObras(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, conn))
                    {
                        this.contInfLicencaObras1.Visible = true;
                        this.contInfLicencaObrasSD1.Visible = false;
                        this.txtConteudoInformacional.Visible = false;
                    }
                }
            }
            else if (this.IDTipoNivelRelacionado == TipoNivelRelacionado.SD && FRDRule.Current.isDocumentoProcessoObra(CurrentFRDBase.ID, conn))
            {
                this.contInfLicencaObras1.Visible = false;
                this.contInfLicencaObrasSD1.Visible = true;
                this.txtConteudoInformacional.Visible = false;
            }
        }

        private static bool IsRelatedToProcessoObras(GISADataset.ControloAutRow currentControloAut)
        {
            return currentControloAut.TipoTipologiasRow != null &&
                                currentControloAut.TipoTipologiasRow.BuiltInName.Equals("PROCESSO_DE_OBRAS");
        }

        public override void ViewToModel()
        {
            if (CurrentFRDBase == null || CurrentFRDBase.RowState == DataRowState.Detached || !IsLoaded)
                return;

            // Nos subdocumentos, o campo estruturado fica activo quando o seu documento/processo tem a tipologia "processo de obras" associada
            GISADataset.FRDBaseRow frdRow = this.IDTipoNivelRelacionado == (long)TipoNivelRelacionado.SD ?
                GisaDataSetHelper.GetInstance().RelacaoHierarquica.Cast<GISADataset.RelacaoHierarquicaRow>().Single(r => r.ID == CurrentFRDBase.IDNivel)
                    .NivelRowByNivelRelacaoHierarquicaUpper.GetFRDBaseRows().Single() :
                CurrentFRDBase;

            System.Diagnostics.Debug.Assert((this.IDTipoNivelRelacionado == (long)TipoNivelRelacionado.SD && !CurrentFRDBase.Equals(frdRow)) || this.IDTipoNivelRelacionado != (long)TipoNivelRelacionado.SD);

            var currentIndexFRDCA = GisaDataSetHelper.GetInstance().IndexFRDCA.Cast<GISADataset.IndexFRDCARow>().Where(
                r => (r.RowState == DataRowState.Added || r.RowState == DataRowState.Modified || r.RowState == DataRowState.Unchanged)
                    && r.IDFRDBase == frdRow.ID && !r.IsSelectorNull() && r.Selector == -1).SingleOrDefault();

            var indexFRDCADeletedList = GisaDataSetHelper.GetInstance().IndexFRDCA.Cast<GISADataset.IndexFRDCARow>().Where(
                    r => r.RowState == DataRowState.Deleted
                        && (long)r["IDFRDBase", DataRowVersion.Original] == frdRow.ID
                        && r["Selector", DataRowVersion.Original] != DBNull.Value
                        && (int)r["Selector", DataRowVersion.Original] == -1).ToArray();

            var possuiDadosLicencaDeObras = false;

            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                possuiDadosLicencaDeObras = FRDRule.Current.possuiDadosLicencaDeObras(GisaDataSetHelper.GetInstance(), frdRow.ID, ho.Connection);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
                throw;
            }
            finally
            {
                ho.Dispose();
            }

            if ((currentIndexFRDCA != null && IsRelatedToProcessoObras(currentIndexFRDCA.ControloAutRow)) ||
                (indexFRDCADeletedList.Length == 0 && possuiDadosLicencaDeObras))
            {
                if (this.IDTipoNivelRelacionado == (long)TipoNivelRelacionado.SD)
                    this.contInfLicencaObrasSD1.ViewToModel();
                else
                    this.contInfLicencaObras1.ViewToModel();
            }
            else
                CurrentSFRDConteudoEEstrutura.ConteudoInformacional = txtConteudoInformacional.Text;

        }

        public override void Deactivate()
        {
            ResetConfEvents();
            GUIHelper.GUIHelper.clearField(txtConteudoInformacional);
            this.contInfLicencaObras1.Deactivate();
            this.contInfLicencaObrasSD1.Deactivate();
        }

        #region  Eventos de alteração de dados noutros locais da aplicação
        public void OrderUpdate()
        {
            IsLoaded = false;
            IsPopulated = false;

            //Deactivate();
        }
        #endregion
    }
}
