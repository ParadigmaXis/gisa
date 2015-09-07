using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Controls.ControloAut;
using GISA.Controls.Nivel;
using GISA.IntGestDoc.Controllers;
using GISA.IntGestDoc.Model;
using GISA.IntGestDoc.Model.EntidadesExternas;
using GISA.IntGestDoc.Model.EntidadesInternas;
using GISA.Model;

namespace GISA.IntGestDoc.UserInterface
{
    public partial class ControlDocumentoGisaProcesso : UserControl
    {
        private CorrespondenciaDocs correspDocumento;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal CorrespondenciaDocs CorrespondenciaDocumento
        {
            get { return this.correspDocumento; }
            set
            {
                this.correspDocumento = value;
                this.UpdateOptions();
                this.UpdateView();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal DocumentoGisa Documento
        {
            get { return (DocumentoGisa)this.correspDocumento.EntidadeInterna; }
        }

        internal InternalEntities InternalEntitiesLst { get; set; }

        public ControlDocumentoGisaProcesso()
        {
            InitializeComponent();
            this.suggestionPickerDocumento.DisplayMember = "CodigoComTitulo";

            this.suggestionPickerDocumento.IsIconComposed = false;
            this.propriedadeSugestionPickerSerie.AllowChangeEnabledState = true;

            this.suggestionPickerDocumento.CreateEntidadeInterna += new CorrespondenciaSuggestionPicker.CreateEntidadeInternaEventHandler(suggestionPickerDocumento_CreateEntidadeInterna);
            this.suggestionPickerTipologia.CreateEntidadeInterna += new CorrespondenciaSuggestionPicker.CreateEntidadeInternaEventHandler(suggestionPickerRA_CreateEntidadeInterna);
            this.suggestionPickerEP.CreateEntidadeInterna += new CorrespondenciaSuggestionPicker.CreateEntidadeInternaEventHandler(suggestionPickerRA_CreateEntidadeInterna);
            this.SuggestionPickerLstLocais.CreateEntidadeInterna += new CorrespondenciaSuggestionPickerList.CreateEntidadeInternaEventHandler(suggestionPickerRA_CreateEntidadeInterna);
            this.SuggestionPickerLstTecObras.CreateEntidadeInterna += new CorrespondenciaSuggestionPickerList.CreateEntidadeInternaEventHandler(suggestionPickerRA_CreateEntidadeInterna);

            this.suggestionPickerDocumento.BrowseEntidadeInterna += new CorrespondenciaSuggestionPicker.BrowseEntidadeInternaEventHandler(suggestionPickerDocumento_BrowseEntidadeInterna);
            this.suggestionPickerTipologia.BrowseEntidadeInterna += new CorrespondenciaSuggestionPicker.BrowseEntidadeInternaEventHandler(suggestionPickerRA_BrowseEntidadeInterna);
            this.suggestionPickerEP.BrowseEntidadeInterna += new CorrespondenciaSuggestionPicker.BrowseEntidadeInternaEventHandler(suggestionPickerRA_BrowseEntidadeInterna);
            this.SuggestionPickerLstLocais.BrowseEntidadeInterna += new CorrespondenciaSuggestionPickerList.BrowseEntidadeInternaEventHandler(suggestionPickerRA_BrowseEntidadeInterna);
            this.SuggestionPickerLstTecObras.BrowseEntidadeInterna += new CorrespondenciaSuggestionPickerList.BrowseEntidadeInternaEventHandler(suggestionPickerRA_BrowseEntidadeInterna);

            this.suggestionPickerDocumento.SuggestionEdited += new CorrespondenciaSuggestionPicker.SuggestionEditedEventHandler(DocumentSuggestionEdited);
            this.suggestionPickerTipologia.SuggestionEdited += new CorrespondenciaSuggestionPicker.SuggestionEditedEventHandler(DocumentSuggestionEdited);
            this.suggestionPickerEP.SuggestionEdited += new CorrespondenciaSuggestionPicker.SuggestionEditedEventHandler(DocumentSuggestionEdited);
            this.SuggestionPickerLstLocais.SuggestionEdited += new CorrespondenciaSuggestionPickerList.SuggestionEditedEventHandler(DocumentSuggestionEdited);
            this.SuggestionPickerLstTecObras.SuggestionEdited += new CorrespondenciaSuggestionPickerList.SuggestionEditedEventHandler(DocumentSuggestionEdited);

            this.propriedadeSugestionPickerSerie.IgnoreOption = true;
            this.propriedadeSugestionPickerSerie.AllowEmptyOptions = true;
            this.propriedadeSugestionPickerData.IgnoreOption = true;
            this.propriedadeSugestionPickerConfidencialidade.IgnoreOption = true;

            this.propriedadeSugestionPickerSerie.SetSearchEntityMode();
            this.propriedadeSugestionPickerData.SetPropertyMode();
            this.propriedadeSugestionPickerConfidencialidade.SetPropertyMode();

            this.propriedadeSugestionPickerSerie.BrowseEntidadeInterna += new PropriedadeSuggestionPickerTemplate<DocumentoGisa>.BrowseEntidadeInternaEventHandler(propriedadeSugestionPickerSerie_BrowseEntidadeInterna);

            this.propriedadeSugestionPickerData.CreateSuggestion += new PropriedadeSuggestionPickerTemplate<DataIncompleta>.CreateSuggestionEventHandler(propriedadeSugestionPickerDataIncompleta_CreateSuggestion);
            this.propriedadeSugestionPickerConfidencialidade.CreateSuggestion += new PropriedadeSuggestionPickerTemplate<string>.CreateSuggestionEventHandler(propriedadeSugestionPickerString_CreateSuggestion);
            this.PropriedadeSuggestionPickerLstRequerentes.CreateSuggestion += new PropriedadeSuggestionPickerLst<string>.CreateSuggestionEventHandler(propriedadeSugestionPickerString_CreateSuggestion);
            this.PropriedadeSuggestionPickerLstAverbamentos.CreateSuggestion += new PropriedadeSuggestionPickerLst<string>.CreateSuggestionEventHandler(propriedadeSugestionPickerString_CreateSuggestion);

            this.propriedadeSugestionPickerSerie.SuggestionEdited += new PropriedadeSuggestionPickerTemplate<DocumentoGisa>.SuggestionEditedEventHandler(DocumentSuggestionEdited);
            this.propriedadeSugestionPickerData.SuggestionEdited += new PropriedadeSuggestionPickerTemplate<DataIncompleta>.SuggestionEditedEventHandler(DocumentSuggestionEdited);
            this.propriedadeSugestionPickerConfidencialidade.SuggestionEdited += new PropriedadeSuggestionPickerTemplate<string>.SuggestionEditedEventHandler(DocumentSuggestionEdited);
            this.PropriedadeSuggestionPickerLstRequerentes.SuggestionEdited += new PropriedadeSuggestionPickerLst<string>.SuggestionEditedEventHandler(DocumentSuggestionEdited);
            this.PropriedadeSuggestionPickerLstAverbamentos.SuggestionEdited += new PropriedadeSuggestionPickerLst<string>.SuggestionEditedEventHandler(DocumentSuggestionEdited);
        }

        public void AddRefreshEvents()
        {
            this.suggestionPickerDocumento.SelectedOptionChanged += new CorrespondenciaSuggestionPicker.SelectedOptionChangedEventHandler(suggestionPickerDocumento_SelectedOptionChanged);
            this.suggestionPickerEP.SelectedOptionChanged += new CorrespondenciaSuggestionPicker.SelectedOptionChangedEventHandler(suggestionPickerEP_SelectedOptionChanged);
        }

        public void RemoveRefreshEvents()
        {
            this.suggestionPickerDocumento.SelectedOptionChanged -= new CorrespondenciaSuggestionPicker.SelectedOptionChangedEventHandler(suggestionPickerDocumento_SelectedOptionChanged);
            this.suggestionPickerEP.SelectedOptionChanged -= new CorrespondenciaSuggestionPicker.SelectedOptionChangedEventHandler(suggestionPickerEP_SelectedOptionChanged);
        }

        void suggestionPickerEP_SelectedOptionChanged()
        {
            Database.Database.SelectIgnorarSerie(this.correspDocumento);
            RemoveRefreshEvents();
            propriedadeSugestionPickerSerie.RefreshControl();
            AddRefreshEvents();
        }

        void suggestionPickerDocumento_SelectedOptionChanged()
        {
            Database.Database.ReavaliaEstado(this.correspDocumento);
            UpdateOptions();
            RemoveRefreshEvents();
            RefreshControls();
            AddRefreshEvents();
        }

        private List<CorrespondenciaSuggestionPicker> mMandatorySugestionPickers = null;
        private List<CorrespondenciaSuggestionPicker> MandatorySugestionPickers
        {
            get
            {
                if (mMandatorySugestionPickers == null)
                    mMandatorySugestionPickers = new List<CorrespondenciaSuggestionPicker>() { suggestionPickerEP };

                return mMandatorySugestionPickers;
            }
        }

        private void UpdateOptions()
        {
            if (this.correspDocumento.TipoOpcao == TipoOpcao.Ignorar) return;

            if (this.correspDocumento.EntidadeInterna.Estado == TipoEstado.SemAlteracoes)
                this.MandatorySugestionPickers.ForEach(sp => sp.AddIgnoreOption());
            else
                this.MandatorySugestionPickers.ForEach(sp => sp.RemoveIgnoreOption());
        }

        private void RefreshControls()
        {
            var dg = this.correspDocumento.EntidadeInterna as DocumentoGisa;

            if (dg == null)
            {
                this.propriedadeSugestionPickerData.Propriedade = null;
                this.propriedadeSugestionPickerConfidencialidade.Propriedade = null;
                this.propriedadeSugestionPickerSerie.Propriedade = null;
                this.suggestionPickerEP.Correspondencia = null;
                this.PropriedadeSuggestionPickerLstRequerentes.LstPropriedade = null;
                this.PropriedadeSuggestionPickerLstAverbamentos.LstPropriedade = null;
                this.SuggestionPickerLstLocais.LstCorrespondencia = null;
                this.SuggestionPickerLstTecObras.LstCorrespondencia = null;
                this.suggestionPickerTipologia.Correspondencia = null;
            }
            else
            {
                this.propriedadeSugestionPickerSerie.Propriedade = dg.Serie;
                if (dg.Serie.Valor != null && dg.Serie.Valor.Id < 0)
                    this.propriedadeSugestionPickerSerie.RefreshControl();
                this.propriedadeSugestionPickerData.Propriedade = ((DocumentoGisa)this.correspDocumento.EntidadeInterna).DataCriacao;
                this.propriedadeSugestionPickerConfidencialidade.Propriedade = ((DocumentoGisa)this.correspDocumento.EntidadeInterna).Confidencialidade;

                PopulateSuggestionPickers();

                //if (this.correspDocumento.TipoSugestao == TipoSugestao.Historico)
                //{
                //    this.suggestionPickerDocumento.Enabled = false;
                //    this.suggestionPickerEP.Enabled = false;
                //    this.propriedadeSugestionPickerSerie.SetEnabled(false);
                //    this.propriedadeSugestionPickerSerie.AllowChangeEnabledState = false;
                //    this.propriedadeSugestionPickerConfidencialidade.SetEnabled(false);
                //    this.propriedadeSugestionPickerConfidencialidade.AllowChangeEnabledState = false;
                //    this.suggestionPickerTipologia.Enabled = false;
                //}
                //else
                //{
                    this.propriedadeSugestionPickerData.SetEnabledState();
                    this.propriedadeSugestionPickerConfidencialidade.SetEnabledState();
                //}
            }
        }

        void DocumentSuggestionEdited(object sender)
        {
            this.SuggestionEdited(this);
        }

        public delegate void SuggestionEditedEventHandler(object sender);
        public event SuggestionEditedEventHandler SuggestionEdited;

        #region Para abstrair
        void propriedadeSugestionPickerSerie_BrowseEntidadeInterna(object sender, ref PropriedadeDocumentoGisaTemplate<DocumentoGisa> prop, out bool cancel, out TipoEstado estado)
        {
            estado = TipoEstado.Novo;
            var ei = prop.Valor as EntidadeInterna;
            cancel = false;

            var message = string.Empty;
            long produtorID;
            if (!Database.Database.CanSelectSerie(this.correspDocumento, out message, out produtorID))
            {
                cancel = true;
                MessageBox.Show(message, "Seleccionar série", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BrowseEntidadeSeries(sender, ref ei, out cancel, new List<long>() { TipoNivelRelacionado.SR, TipoNivelRelacionado.SSR }, produtorID);
            if (!cancel)
            {
                prop.TipoOpcao = TipoOpcao.Trocada;
                prop.Valor = ei as DocumentoGisa;
                prop.EstadoRelacaoPorOpcao[TipoOpcao.Trocada] = estado;
            }
        }

        void propriedadeSugestionPickerDataIncompleta_CreateSuggestion(object sender, ref PropriedadeDocumentoGisaTemplate<DataIncompleta> prop, out bool cancel, out TipoEstado estado)
        {
            cancel = false;
            estado = TipoEstado.Novo;
            FormNewValue form = new FormNewValue();
            if (prop.Valor != null)
            {
                form.NewValueStartDay = prop.Valor.DiaInicio;
                form.NewValueStartMonth = prop.Valor.MesInicio;
                form.NewValueStartYear = prop.Valor.AnoInicio;
                form.NewValueEndDay = prop.Valor.DiaFim;
                form.NewValueEndMonth = prop.Valor.MesFim;
                form.NewValueEndYear = prop.Valor.AnoFim;
            }
            form.ShowDateField(true);

            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    prop.TipoOpcao = TipoOpcao.Trocada;
                    prop.Valor = new DataIncompleta(form.NewValueStartYear, form.NewValueStartMonth, form.NewValueStartDay, form.NewValueEndYear, form.NewValueEndMonth, form.NewValueEndDay);
                    if (prop.EstadoRelacaoPorOpcao.ContainsKey(TipoOpcao.Original))
                        prop.EstadoRelacaoPorOpcao[prop.TipoOpcao] = TipoEstado.Editar;
                    else
                        prop.EstadoRelacaoPorOpcao[prop.TipoOpcao] = TipoEstado.Novo;
                    break;
                case DialogResult.Cancel:
                    cancel = true;
                    break;
            }
        }

        void propriedadeSugestionPickerString_CreateSuggestion(object sender, ref PropriedadeDocumentoGisaTemplate<string> prop, out bool cancel, out TipoEstado estado)
        {
            cancel = false;
            estado = TipoEstado.Novo;
            FormNewValue form = new FormNewValue();
            form.NewValueString = prop.Valor;
            form.ShowDateField(false);

            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    prop.TipoOpcao = TipoOpcao.Trocada;
                    prop.EstadoRelacaoPorOpcao[prop.TipoOpcao] = TipoEstado.Novo;
                    prop.Valor = form.NewValueString;
                    break;
                case DialogResult.Cancel:
                    cancel = true;
                    break;
            }
        }

        void suggestionPickerRA_CreateEntidadeInterna(object sender, ref EntidadeInterna ei, TipoEntidadeExterna tee, out bool cancel, out TipoEstado estado)
        {
            cancel = false;
            estado = TipoEstado.Novo;
            FormCreateControloAut form = null;
            var rai = (RegistoAutoridadeInterno)ei;
            var tna = TipoEntidade.GetTipoNoticiaAut(tee);
            if (tna == TipoNoticiaAut.EntidadeProdutora)
                form = new FormCreateEntidadeProdutora();
            else
                form = new FormCreateControloAut();

            if (tna == TipoNoticiaAut.Onomastico)
            {
                form.SetOptionalControlsVisible(true);
                form.NIF = ei != null ? ((Model.EntidadesInternas.Onomastico)ei).Codigo : "";
            }

            GISADataset.TipoNoticiaAutRow allNoticiaAut = null;
            allNoticiaAut = GisaDataSetHelper.GetInstance().TipoNoticiaAut.NewTipoNoticiaAutRow();
            allNoticiaAut.ID = -1;
            allNoticiaAut.Designacao = "<Todos>";

            List<GISADataset.TipoNoticiaAutRow> rows = new List<GISADataset.TipoNoticiaAutRow>();
            rows.Add(allNoticiaAut);
            rows.AddRange(GisaDataSetHelper.GetInstance().TipoNoticiaAut.Cast<GISADataset.TipoNoticiaAutRow>().Where(r => r.ID == (long)tna));

            form.cbNoticiaAut.BeginUpdate();
            form.cbNoticiaAut.DataSource = rows;
            form.cbNoticiaAut.DisplayMember = "Designacao";
            form.cbNoticiaAut.EndUpdate();
            if (form.cbNoticiaAut.Items.Count == 2)
            {
                form.cbNoticiaAut.SelectedIndex = 1;
                form.cbNoticiaAut.Enabled = false;
            }
            form.LoadData(true);

            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    var termo = form.ListTermos.ValidAuthorizedForm.Replace("'", "''");
                    switch (tna)
                    {
                        case TipoNoticiaAut.EntidadeProdutora:
                            var produtor = new Model.EntidadesInternas.Produtor();
                            produtor.Codigo = ((FormCreateEntidadeProdutora)form).txtCodigo.Text;
                            rai = produtor;
                            break;
                        case TipoNoticiaAut.Onomastico:
                            rai = new Model.EntidadesInternas.Onomastico();
                            ((Model.EntidadesInternas.Onomastico)rai).Codigo = form.NIF;
                            break;
                        case TipoNoticiaAut.Ideografico:
                            rai = new Model.EntidadesInternas.Ideografico();
                            break;
                        case TipoNoticiaAut.ToponimicoGeografico:
                            rai = new Model.EntidadesInternas.Geografico();
                            break;
                        case TipoNoticiaAut.TipologiaInformacional:
                            rai = new Model.EntidadesInternas.Tipologia();
                            break;
                    }
                    rai.Titulo = termo;
                    rai.Estado = TipoEstado.Novo;
                    ei = this.InternalEntitiesLst.AddInternalEntity(rai);
                    break;
                case DialogResult.Cancel:
                    cancel = true;
                    break;
            }
        }

        void suggestionPickerDocumento_CreateEntidadeInterna(object sender, ref EntidadeInterna ei, TipoEntidadeExterna tee, out bool cancel, out TipoEstado estado)
        {
            estado = TipoEstado.Novo;
            cancel = CreateDocumentoInterno(ref ei, TipoNivelRelacionado.D);
            if (!cancel) txtID.Text = "Não atribuido";
        }
        
        private bool CreateDocumentoInterno(ref EntidadeInterna ei, long tnr)
        {
            bool cancel = false;
            FormAddNivel form = new FormAddNivel();
            form.IDTipoNivelRelacionado = tnr;
            form.txtDesignacao.Text = ei != null ? ((DocumentoGisa)ei).Titulo : "";
            if (tnr == TipoNivelRelacionado.SD)
                form.txtCodigo.Text = ei != null ? ((DocumentoGisa)ei).Codigo : "";
            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    var documento = new DocumentoGisa();
                    documento.Tipo = TipoEntidadeInterna.DocumentoComposto;
                    documento.CopyProperties(this.correspDocumento);
                    documento.Titulo = form.txtDesignacao.Text;
                    documento.Codigo = form.txtCodigo.Text;
                    ei = this.InternalEntitiesLst.AddInternalEntity(documento);
                    break;
                case DialogResult.Cancel:
                    cancel = true;
                    break;
            }
            return cancel;
        }

        void suggestionPickerRA_BrowseEntidadeInterna(object sender, ref EntidadeInterna ei, TipoEntidadeExterna tee, out bool cancel, out TipoEstado estado)
        {
            estado = TipoEstado.Novo;
            cancel = false;
            var rai = (RegistoAutoridadeInterno)ei;
            var tna = TipoEntidade.GetTipoNoticiaAut(tee);
            FormPickControloAut frmPick = new FormPickControloAut();
            frmPick.Text = "Notícia de autoridade - Pesquisar registo de autoridade";
            frmPick.caList.AllowedNoticiaAut(tna);
            frmPick.caList.ReloadList();

            if (frmPick.caList.Items.Count > 0)
                frmPick.caList.SelectItem(frmPick.caList.Items[0]);

            switch (frmPick.ShowDialog())
            {
                case DialogResult.OK:
                    var cadRow = (GISADataset.ControloAutDicionarioRow)frmPick.caList.SelectedItems[0].Tag;
                    switch (tna)
                    {
                        case TipoNoticiaAut.EntidadeProdutora:
                            var produtor = new Model.EntidadesInternas.Produtor();
                            var nRowCA = cadRow.ControloAutRow.GetNivelControloAutRows()[0].NivelRow;
                            produtor.Codigo = nRowCA.Codigo;
                            rai = produtor;
                            estado = Database.Database.IsProdutor(this.correspDocumento.EntidadeInterna.Id, nRowCA.ID) ? TipoEstado.SemAlteracoes : TipoEstado.Novo;
                            break;
                        case TipoNoticiaAut.Onomastico:
                            rai = new Model.EntidadesInternas.Onomastico();
                            break;
                        case TipoNoticiaAut.Ideografico:
                            rai = new Model.EntidadesInternas.Ideografico();
                            break;
                        case TipoNoticiaAut.ToponimicoGeografico:
                            rai = new Model.EntidadesInternas.Geografico();
                            break;
                        case TipoNoticiaAut.TipologiaInformacional:
                            rai = new Model.EntidadesInternas.Tipologia();
                            if (this.Documento.Id > 0)
                            {
                                var nRow = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().Single(r => r.ID == this.Documento.Id);
                                if (GisaDataSetHelper.GetInstance().IndexFRDCA.Cast<GISADataset.IndexFRDCARow>().SingleOrDefault(r => r.IDControloAut == cadRow.IDControloAut && r.IDFRDBase == nRow.GetFRDBaseRows()[0].ID) != null)
                                    estado = TipoEstado.SemAlteracoes;
                                else if(nRow.GetFRDBaseRows()[0].GetIndexFRDCARows().Select(r => r.ControloAutRow).Count(caRow => caRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.TipologiaInformacional) > 0)
                                    estado = TipoEstado.Editar;
                                else
                                    estado = TipoEstado.Novo;
                            }
                            break;
                    }
                    rai.Titulo = cadRow.DicionarioRow.Termo;
                    rai.Estado = TipoEstado.SemAlteracoes;
                    rai.Id = cadRow.IDControloAut;
                    ei = rai;
                    break;
                case DialogResult.Cancel:
                    cancel = true;
                    break;
            }
        }

        void suggestionPickerDocumento_BrowseEntidadeInterna(object sender, ref EntidadeInterna ei, TipoEntidadeExterna tee, out bool cancel, out TipoEstado estado)
        {
            estado = TipoEstado.SemAlteracoes;
            BrowseEntidadeDocumentos(sender, ref ei, out cancel, new List<long>() { TipoNivelRelacionado.D });
            if (!cancel) { txtID.Text = ei.Id.ToString(); Database.Database.LoadDocumentDetails(ei); }
        }

        void BrowseEntidadeSeries(object sender, ref EntidadeInterna ei, out bool cancel, List<long> tnrLst, long produtorID)
        {
            cancel = false;
            FormSelectNivel form = new FormSelectNivel();
            form.SelectableType = tnrLst;
            form.SetOnlyDocViewMode(produtorID);

            BrowseEntidadeInterna(form, ref ei, out cancel, tnrLst);
        }

        void BrowseEntidadeDocumentos(object sender, ref EntidadeInterna ei, out bool cancel, List<long> tnrLst)
        {
            cancel = false;
            FormSelectNivel form = new FormSelectNivel();
            form.SelectableType = tnrLst;
            form.nivelNavigator1.LoadVistaEstrutural();

            BrowseEntidadeInterna(form, ref ei, out cancel, tnrLst);
        }

        void BrowseEntidadeInterna(FormSelectNivel form, ref EntidadeInterna ei, out bool cancel, List<long> tnrLst)
        {
            cancel = false;

            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    var documento = new DocumentoGisa();
                    if (tnrLst.Contains((long)TipoNivelRelacionado.D))
                        documento.CopyProperties(this.correspDocumento);
                    documento.Titulo = form.SelectedDocument.GetNivelDesignadoRows()[0].Designacao;
                    documento.Codigo = form.SelectedDocument.Codigo;
                    documento.Tipo = TipoEntidade.GetTipoEntidadeInterna(form.SelectedDocument.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].IDTipoNivelRelacionado);
                    ei = documento;
                    ei.Estado = TipoEstado.SemAlteracoes;
                    ei.Id = form.SelectedDocument.ID;
                    break;
                case DialogResult.Cancel:
                    cancel = true;
                    break;
            }
        }
        #endregion

        private void UpdateView()
        {
            this.suggestionPickerDocumento.Correspondencia = this.correspDocumento;
            var dg = this.correspDocumento.EntidadeInterna as DocumentoGisa;
            txtID.Text = dg != null && dg.Id >= 0 ? dg.Id.ToString() : "Não atribuido";
            RefreshControls();
        }

        private void PopulateSuggestionPickers()
        {
            this.suggestionPickerEP.Correspondencia =
                this.correspDocumento.CorrespondenciasRAs.Where(
                    c => ((RegistoAutoridadeExterno)c.EntidadeExterna).Tipo == TipoEntidadeExterna.Produtor
                    ).SingleOrDefault();
            this.suggestionPickerTipologia.Correspondencia =
                this.correspDocumento.CorrespondenciasRAs.Where(
                    c => ((RegistoAutoridadeExterno)c.EntidadeExterna).Tipo == TipoEntidadeExterna.TipologiaInformacional
                    ).SingleOrDefault();
            this.PropriedadeSuggestionPickerLstRequerentes.LstPropriedade =
                ((DocumentoGisa)this.correspDocumento.EntidadeInterna).Requerentes;
            this.PropriedadeSuggestionPickerLstAverbamentos.LstPropriedade =
                ((DocumentoGisa)this.correspDocumento.EntidadeInterna).Averbamentos;
            this.SuggestionPickerLstLocais.CorrespondenciaDoc = this.correspDocumento;
            this.SuggestionPickerLstLocais.LstCorrespondencia =
                this.correspDocumento.CorrespondenciasRAs.Where(
                    c => ((RegistoAutoridadeExterno)c.EntidadeExterna).Tipo == TipoEntidadeExterna.Geografico
                    ).Cast<Correspondencia>().ToList();
            this.SuggestionPickerLstTecObras.LstCorrespondencia =
                this.correspDocumento.CorrespondenciasRAs.Where(
                    c => ((RegistoAutoridadeExterno)c.EntidadeExterna).Tipo == TipoEntidadeExterna.Onomastico
                    ).Cast<Correspondencia>().ToList();
        }

        internal void Clear()
        {
            RemoveRefreshEvents();
            this.suggestionPickerDocumento.Clear();
            this.suggestionPickerTipologia.Clear();
            this.suggestionPickerEP.Clear();
            this.propriedadeSugestionPickerData.Clear();
            this.propriedadeSugestionPickerConfidencialidade.Clear();
            this.SuggestionPickerLstLocais.Clear();
            this.SuggestionPickerLstTecObras.Clear();
            this.PropriedadeSuggestionPickerLstRequerentes.Clear();
            this.PropriedadeSuggestionPickerLstAverbamentos.Clear();
        }
    }
}
