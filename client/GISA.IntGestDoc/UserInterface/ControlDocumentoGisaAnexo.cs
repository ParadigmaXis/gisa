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
using GISA.IntGestDoc.Model.EntidadesExternas;
using GISA.IntGestDoc.Model.EntidadesInternas;
using GISA.IntGestDoc.Model;
using GISA.Model;

namespace GISA.IntGestDoc.UserInterface
{
    public partial class ControlDocumentoGisaAnexo : UserControl
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

        public ControlDocumentoGisaAnexo()
        {
            InitializeComponent();
            this.suggestionPickerDocumento.DisplayMember = "CodigoComTitulo";

            this.suggestionPickerDocumento.IsIconComposed = false;

            this.suggestionPickerDocumento.CreateEntidadeInterna += new CorrespondenciaSuggestionPicker.CreateEntidadeInternaEventHandler(SuggestionPickerDocumento_CreateEntidadeInterna);

            this.suggestionPickerDocumento.BrowseEntidadeInterna += new CorrespondenciaSuggestionPicker.BrowseEntidadeInternaEventHandler(suggestionPickerDocumento_BrowseEntidadeInterna);

            this.suggestionPickerDocumento.SuggestionEdited += new CorrespondenciaSuggestionPicker.SuggestionEditedEventHandler(DocumentSuggestionEdited);

            this.propriedadeSuggestionPickerDescricao.IgnoreOption = true;
            this.propriedadeSuggestionPickerDescricao.SetPropertyMode();
            this.propriedadeSuggestionPickerDescricao.CreateSuggestion += new PropriedadeSuggestionPickerTemplate<string>.CreateSuggestionEventHandler(propriedadeSugestionPickerString_CreateSuggestion);
            this.propriedadeSuggestionPickerDescricao.SuggestionEdited += new PropriedadeSuggestionPickerTemplate<string>.SuggestionEditedEventHandler(DocumentSuggestionEdited);

            this.propriedadeSuggestionPickerAgrupador.IgnoreOption = true;
            this.propriedadeSuggestionPickerAgrupador.SetPropertyMode();
            this.propriedadeSuggestionPickerAgrupador.CreateSuggestion += new PropriedadeSuggestionPickerTemplate<string>.CreateSuggestionEventHandler(propriedadeSugestionPickerString_CreateSuggestion);
            this.propriedadeSuggestionPickerAgrupador.SuggestionEdited += new PropriedadeSuggestionPickerTemplate<string>.SuggestionEditedEventHandler(DocumentSuggestionEdited);
        }

        public void AddRefreshEvents()
        {
            this.suggestionPickerDocumento.SelectedOptionChanged += new CorrespondenciaSuggestionPicker.SelectedOptionChangedEventHandler(SuggestionPickerDocumento_SelectedOptionChanged);
        }

        public void RemoveRefreshEvents()
        {
            this.suggestionPickerDocumento.SelectedOptionChanged -= new CorrespondenciaSuggestionPicker.SelectedOptionChangedEventHandler(SuggestionPickerDocumento_SelectedOptionChanged);
        }

        void propriedadeSuggestionPickerSerie_SelectedOptionChanged(object sender)
        {
            Database.Database.ReavaliaEstadoRAs(this.correspDocumento);
            RemoveRefreshEvents();
            //this.suggestionPickerEP.RefreshOptions();
            AddRefreshEvents();
        }

        void SuggestionPickerEP_SelectedOptionChanged()
        {
            Database.Database.SelectIgnorarSerie(this.correspDocumento);
        }

        void SuggestionPickerDocumento_SelectedOptionChanged()
        {
            Database.Database.ReavaliaEstado(this.correspDocumento);
            UpdateOptions();
            RemoveRefreshEvents();
            RefreshControls();
            AddRefreshEvents();
        }

        private List<CorrespondenciaSuggestionPicker> mMandatorySuggestionPickers = null;
        private List<CorrespondenciaSuggestionPicker> MandatorySuggestionPickers
        {
            get
            {
                if (mMandatorySuggestionPickers == null)
                    mMandatorySuggestionPickers = new List<CorrespondenciaSuggestionPicker>();

                return mMandatorySuggestionPickers;
            }
        }

        private List<PropriedadeSuggestionPicker> mMandatoryPropriedadeSuggestionPickers = null;
        private List<PropriedadeSuggestionPicker> MandatoryPropriedadeSuggestionPickers
        {
            get
            {
                mMandatoryPropriedadeSuggestionPickers = new List<PropriedadeSuggestionPicker>();
                return mMandatoryPropriedadeSuggestionPickers;
            }
        }

        private void UpdateOptions()
        {
            if (this.correspDocumento.TipoOpcao == TipoOpcao.Ignorar) return;

            if (this.correspDocumento.EntidadeInterna.Estado == TipoEstado.SemAlteracoes)
            {
                this.MandatorySuggestionPickers.ForEach(sp => sp.AddIgnoreOption());
                this.MandatoryPropriedadeSuggestionPickers.ForEach(c => c.IgnoreOption = true);
            }
            else
            {
                this.MandatorySuggestionPickers.ForEach(sp => sp.RemoveIgnoreOption());
                this.MandatoryPropriedadeSuggestionPickers.ForEach(c => c.IgnoreOption = false);
            }
        }

        private void RefreshControls()
        {
            var dg = this.correspDocumento.EntidadeInterna as DocumentoGisa;

            if (dg == null)
            {
                this.propriedadeSuggestionPickerAgrupador.Propriedade = null;
                this.propriedadeSuggestionPickerDescricao.Propriedade = null;
            }
            else
            {
                this.MandatorySuggestionPickers.ForEach(sp => sp.RefreshOptions());

                this.propriedadeSuggestionPickerDescricao.Propriedade = dg.Notas;
                this.propriedadeSuggestionPickerDescricao.SetEnabledState();
                this.propriedadeSuggestionPickerAgrupador.Propriedade = dg.Agrupador;
                this.propriedadeSuggestionPickerAgrupador.SetEnabledState();
            }
        }

        void DocumentSuggestionEdited(object sender)
        {
            this.SuggestionEdited(this);
        }

        public delegate void SuggestionEditedEventHandler(object sender);
        public event SuggestionEditedEventHandler SuggestionEdited;

        #region Para abstrair
        void propriedadeSugestionPickerDataIncompleta_CreateSuggestion(object sender, ref PropriedadeDocumentoGisaTemplate<DataIncompleta> prop, out bool cancel, out TipoEstado estado)
        {
            cancel = false;
            estado = TipoEstado.Novo;
            FormNewValue form = new FormNewValue();
            form.NewValueStartDay = prop.Valor.DiaInicio;
            form.NewValueStartMonth = prop.Valor.MesInicio;
            form.NewValueStartYear = prop.Valor.AnoInicio;
            form.ShowDateField(true);

            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    prop.TipoOpcao = TipoOpcao.Trocada;
                    if (prop.EstadoRelacaoPorOpcao.ContainsKey(TipoOpcao.Original))
                        prop.EstadoRelacaoPorOpcao[prop.TipoOpcao] = TipoEstado.Editar;
                    else
                        prop.EstadoRelacaoPorOpcao[prop.TipoOpcao] = TipoEstado.Novo;
                    prop.Valor = new DataIncompleta(form.NewValueStartYear, form.NewValueStartMonth, form.NewValueStartDay, form.NewValueEndYear, form.NewValueEndMonth, form.NewValueEndDay);
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

        void SuggestionPickerDocumento_CreateEntidadeInterna(object sender, ref EntidadeInterna ei, TipoEntidadeExterna tee, out bool cancel, out TipoEstado estado)
        {
            estado = TipoEstado.Novo;
            cancel = CreateDocumentoInterno(ref ei, TipoNivelRelacionado.SD);
            if (!cancel)
            {
                txtID.Text = "Não atribuido";
                if (this.correspDocumento.EntidadeInterna != null) // se for igual a null quer dizer que não há nenhum documento selecionado
                {
                    var dg = this.correspDocumento.EntidadeInterna as DocumentoGisa;
                    dg.Processo.EstadoRelacaoPorOpcao[TipoOpcao.Trocada] = estado;
                }
            }
        }

        void propriedadeSuggestionPickerProcesso_CreateEntidadeInterna(object sender, ref PropriedadeDocumentoGisaTemplate<DocumentoGisa> prop, out bool cancel, out TipoEstado estado)
        {
            estado = TipoEstado.Novo;
            var ei = prop.Valor as EntidadeInterna;
            cancel = CreateDocumentoInterno(ref ei, TipoNivelRelacionado.D);
            if (!cancel)
            {
                prop.TipoOpcao = TipoOpcao.Trocada;
                prop.Valor = ei as DocumentoGisa;
                prop.EstadoRelacaoPorOpcao[TipoOpcao.Trocada] = estado;
            }
        }

        private bool CreateDocumentoInterno(ref EntidadeInterna ei, long tnr)
        {
            bool cancel = false;
            FormAddNivel form = new FormAddNivel();
            form.IDTipoNivelRelacionado = tnr;
            form.txtDesignacao.Text = ((DocumentoGisa)ei).Titulo;
            if (tnr == TipoNivelRelacionado.SD)
                form.txtCodigo.Text = ((DocumentoGisa)ei).Codigo;
            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    var documento = new DocumentoGisa();
                    documento.Tipo = tnr == (long)TipoNivelRelacionado.SD ? TipoEntidadeInterna.DocumentoSimples : TipoEntidadeInterna.DocumentoComposto;
                    if (tnr == (long)TipoNivelRelacionado.SD)
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
                                else if (nRow.GetFRDBaseRows()[0].GetIndexFRDCARows().Select(r => r.ControloAutRow).Count(caRow => caRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.TipologiaInformacional) > 0)
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

        void propriedadeSuggestionPickerSerie_BrowseEntidadeInterna(object sender, ref PropriedadeDocumentoGisaTemplate<DocumentoGisa> prop, out bool cancel, out TipoEstado estado)
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

        void propriedadeSuggestionPickerProcesso_BrowseEntidadeInterna(object sender, ref PropriedadeDocumentoGisaTemplate<DocumentoGisa> prop, out bool cancel, out TipoEstado estado)
        {
            estado = TipoEstado.SemAlteracoes;
            var ei = prop.Valor as EntidadeInterna;
            cancel = false;

            BrowseEntidadeDocumentos(sender, ref ei, out cancel, new List<long>() { TipoNivelRelacionado.D });
            if (!cancel)
            {
                Database.Database.LoadDocumentDetails(ei);
                prop.TipoOpcao = TipoOpcao.Trocada;
                prop.Valor = ei as DocumentoGisa;
                estado = prop.GetValor(TipoOpcao.Original) != null ? TipoEstado.Editar : TipoEstado.Novo;
                prop.EstadoRelacaoPorOpcao[TipoOpcao.Trocada] = estado;
            }
        }

        void suggestionPickerDocumento_BrowseEntidadeInterna(object sender, ref EntidadeInterna ei, TipoEntidadeExterna tee, out bool cancel, out TipoEstado estado)
        {
            estado = TipoEstado.SemAlteracoes;
            BrowseEntidadeDocumentos(sender, ref ei, out cancel, new List<long>() { TipoNivelRelacionado.SD });
            if (!cancel)
            {
                txtID.Text = ei.Id.ToString();
                Database.Database.LoadDocumentDetails(ei);
            }
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
                    if (tnrLst.Contains((long)TipoNivelRelacionado.SD))
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

        internal void Clear()
        {
            RemoveRefreshEvents();
            this.suggestionPickerDocumento.Clear();
            this.propriedadeSuggestionPickerAgrupador.Clear();
            this.propriedadeSuggestionPickerDescricao.Clear();
        }
    }
}
