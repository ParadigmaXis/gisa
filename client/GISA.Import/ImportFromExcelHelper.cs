using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Controls;
using GISA.Model;
using GISA.Search;
using GISA.Utils;


namespace GISA.Import
{
    public static class ImportFromExcelHelper
    {
        static Dictionary<string, UnidadeInformacional> unidadesInformacionais;
        static Dictionary<string, UnidadeFisica> unidadesFisicas;
        static Dictionary<GISADataset.FRDBaseRow, Registo> registos;
        static Dictionary<string, GISADataset.NivelRow> uiRows;
        static Dictionary<string, GISADataset.NivelRow> ufRows;

        static string TAG = "Importação";

        public static void ImportFromExcel(string fileLocation)
        {   
            ImportExcel imp = null;
            string fileExtension = fileLocation.Split('.').Last();

            if (fileExtension.Equals("xls")) 
                imp = new ImportExcel97to2003(fileLocation);
            else if (fileExtension.Equals("xlsx"))
                imp = new ImportExcel2007Up(fileLocation);
            else
            {
                MessageBox.Show("O formato '" + fileExtension + "' não é reconhecido.", TAG, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                imp.Import();

                var uiList = imp.GetUnidadesInformacionais;
                var ufList = imp.GetUnidadesFisicas;

                // validar campos obrigatorios
                uiList.ToList().ForEach(ui => ValidaCamposObrigatorios(ui));
                ufList.ToList().ForEach(uf => ValidaCamposObrigatorios(uf));

                // validar se não há identificadores repetidos
                var rep = ValidaIdentificadores(uiList.Select(ui => ui.identificador));
                if (rep != null)
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, string.Empty, ImportExcel.UI_IDENTIFICADOR, rep.First(), ExceptionHelper.ERR_VALOR_REPETIDO);

                rep = ValidaIdentificadores(ufList.Select(uf => uf.identificador));
                if (rep != null)
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_UNIDADES_FISICAS, string.Empty, ImportExcel.UI_IDENTIFICADOR, rep.First(), ExceptionHelper.ERR_VALOR_REPETIDO);

                // validar se não há códigos de referência repetidos por nível superior
                ValidaCodigosReferencia(uiList);

                unidadesInformacionais = uiList.ToDictionary(ui => ui.identificador, ui => ui);
                unidadesFisicas = ufList.ToDictionary(uf => uf.identificador, uf => uf);

                var niveisDoc = unidadesInformacionais.Values.Where(ui => ui.idNivelSuperior != null && ui.idNivelSuperior.Length > 0 && ui.idNivelSuperior.StartsWith("gisa:")).Select(ui => ui.idNivelSuperior.Replace("gisa:", "")).Distinct();
                var niveisUfAssoc = unidadesInformacionais.Values.SelectMany(ui => ui.unidadesFisicas).Where(uf => uf.StartsWith("gisa_uf:")).Select(uf => uf.Replace("gisa_uf:", "").Split('/')[1]).Distinct();
                var entidadesProdutoras = unidadesInformacionais.Values.SelectMany(ui => ui.entidadesProdutoras).Distinct();
                var autores = unidadesInformacionais.Values.SelectMany(ui => ui.autores).Distinct();
                var modelos = unidadesInformacionais.Values.SelectMany(ui => ui.modelo).Distinct();
                var diplomas = unidadesInformacionais.Values.SelectMany(ui => ui.diplomaLegal).Distinct();
                var onomasticos = unidadesInformacionais.Values.SelectMany(ui => ui.onomasticos).Distinct();
                var ideograficos = unidadesInformacionais.Values.SelectMany(ui => ui.ideograficos).Distinct();
                var geograficos = unidadesInformacionais.Values.SelectMany(ui => ui.geograficos).Distinct();
                var tipologias = unidadesInformacionais.Values.Where(ui => ui.tipoInformacional != null && ui.tipoInformacional.Length > 0).Select(val => val.tipoInformacional).Distinct();

                var cas = new List<string>();
                cas.AddRange(entidadesProdutoras);
                cas.AddRange(autores);
                cas.AddRange(modelos);
                cas.AddRange(diplomas);
                cas.AddRange(onomasticos);
                cas.AddRange(ideograficos);
                cas.AddRange(geograficos);
                cas.AddRange(tipologias);

                // load data
                LoadEDsInfo();
                LoadNiveisDoc(niveisDoc.ToList());
                LoadNiveisUfAssoc(niveisUfAssoc.ToList());
                LoadControlosAutoridade(cas.Distinct().ToList());
                LoadTrustees();
                LoadLocalConsulta();

                // importação para o data set
                GisaDataSetHelper.ManageDatasetConstraints(false);

                uiRows = new Dictionary<string, GISADataset.NivelRow>();
                ufRows = new Dictionary<string, GISADataset.NivelRow>();
                registos = new Dictionary<GISADataset.FRDBaseRow, Registo>();

                unidadesInformacionais.Keys.ToList().ForEach(key =>
                {
                    if (!uiRows.ContainsKey(key))
                        UIsToGISADatasetRow(unidadesInformacionais[key]);
                });

                unidadesFisicas.Keys.ToList().ForEach(key =>
                {
                    if (!ufRows.ContainsKey(key))
                        UFsToGISADatasetRows(unidadesFisicas[key]);
                });
                

                GisaDataSetHelper.ManageDatasetConstraints(true);

                var pcArgs = new PersistencyHelper.ValidaImportPreConcArguments();
                var psArgs = new PersistencyHelper.ValidaImportPreSaveArguments();

                var pcArgsLstUI = new List<PersistencyHelper.ValidateNivelAddAndAssocNewUFPreConcArguments>();
                var psArgsNivelLstUI = new List<PersistencyHelper.SetNewCodigosPreSaveArguments>();
                foreach (var nRow in uiRows.Values)
                {
                    var pcArgsNewNivel = new PersistencyHelper.ValidateNivelAddAndAssocNewUFPreConcArguments();
                    var psArgsNivel = new PersistencyHelper.SetNewCodigosPreSaveArguments();
                    var pcArgsNivelUniqueCode = new PersistencyHelper.EnsureUniqueCodigoNivelPreConcArguments();

                    // dados que serão usados no delegate responsável pela criação do nível documental
                    var rhRow = nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().Single();
                    pcArgsNivelUniqueCode.nRowID = nRow.ID;
                    pcArgsNivelUniqueCode.ndRowID = nRow.GetNivelDesignadoRows().Single().ID;
                    pcArgsNivelUniqueCode.rhRowID = nRow.ID;
                    pcArgsNivelUniqueCode.rhRowIDUpper = rhRow.IDUpper;
                    pcArgsNivelUniqueCode.frdBaseID = nRow.GetFRDBaseRows().Single().ID;
                    pcArgsNivelUniqueCode.testOnlyWithinNivel = true;

                    pcArgsNewNivel.IDTipoNivelRelacionado = rhRow.IDTipoNivelRelacionado;
                    pcArgsNewNivel.argsNivel = pcArgsNivelUniqueCode;

                    psArgsNivel.createNewNivelCodigo = false;
                    psArgsNivel.createNewUFCodigo = false;
                    psArgsNivel.setNewCodigo = rhRow.IDTipoNivelRelacionado == TipoNivelRelacionado.SD;
                    psArgsNivel.argsNivelDocSimples = NiveisHelper.AddNivelDocumentoSimplesWithDelegateArgs(nRow.GetNivelDesignadoRows().Single(), rhRow.IDUpper, rhRow.IDTipoNivelRelacionado);

                    pcArgsLstUI.Add(pcArgsNewNivel);
                    psArgsNivelLstUI.Add(psArgsNivel);
                }

                pcArgs.newDocsList = pcArgsLstUI;
                psArgs.newDocArgs = psArgsNivelLstUI;

                var pcArgsLstUF = new List<PersistencyHelper.AddEditUFPreConcArguments>();
                var psArgsLstUF = new List<PersistencyHelper.IsCodigoUFBeingUsedPreSaveArguments>();
                foreach (var nRow in ufRows.Values)
                {
                    var argsPC = new PersistencyHelper.AddEditUFPreConcArguments();
                    var argsPS = new PersistencyHelper.IsCodigoUFBeingUsedPreSaveArguments();

                    argsPC.Operation = PersistencyHelper.AddEditUFPreConcArguments.Operations.Create;

                    argsPC.nivelUFRowID = nRow.ID;
                    argsPC.ndufRowID = nRow.GetNivelDesignadoRows().First().ID;
                    argsPC.rhufRowID = nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First().ID;
                    argsPC.rhufRowIDUpper = nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First().IDUpper;
                    argsPC.nufufRowID = nRow.GetNivelDesignadoRows().First().GetNivelUnidadeFisicaRows().First().ID;
                    argsPC.frdufRowID = nRow.GetFRDBaseRows().First().ID;

                    argsPS.nivelUFRowID = nRow.ID;
                    argsPC.psa = argsPS;

                    pcArgsLstUF.Add(argsPC);
                    psArgsLstUF.Add(argsPS);
                }

                pcArgs.newUfsList = pcArgsLstUF;
                psArgs.newUfArgs = psArgsLstUF;

                // actualizar permissões implícitas
                var postSaveAction = new PostSaveAction();
                PersistencyHelper.UpdatePermissionsPostSaveArguments args = new PersistencyHelper.UpdatePermissionsPostSaveArguments();
                postSaveAction.args = args;

                postSaveAction.postSaveDelegate = delegate(PersistencyHelper.PostSaveArguments postSaveArgs)
                {
                    var regs = registos.Keys
                        .Select(key => RecordRegisterHelper.CreateFRDBaseDataDeDescricaoRow(registos[key].CurrentFRDBase, registos[key].tuOperator, registos[key].tuAuthor, registos[key].data, true))
                        .ToArray();

                    regs.ToList().ForEach(reg => GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao.AddFRDBaseDataDeDescricaoRow(reg));

                    PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao, regs, postSaveArgs.tran);
                };

                var saveResult = PersistencyHelper.save(ValidateImport, pcArgs, ValidateImport, psArgs, postSaveAction, true);

                if (saveResult == PersistencyHelper.SaveResult.cancel || saveResult == PersistencyHelper.SaveResult.unsuccessful)
                {
                    string errorMessage = "";
                    string abortMessage = "A importação vai ser abortada.";
                    if (pcArgs.errorMessage.Length > 0)
                        errorMessage = pcArgs.errorMessage + System.Environment.NewLine + System.Environment.NewLine + abortMessage;
                    else if (psArgs.errorMessage.Length > 0)
                        errorMessage = psArgs.errorMessage + System.Environment.NewLine + System.Environment.NewLine + abortMessage;
                    else
                        errorMessage = "Ocorreu um problema inesperado." + System.Environment.NewLine + abortMessage;

                    MessageBox.Show(errorMessage, TAG, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    GisaDataSetHelper.GetInstance().RejectChanges();
                    return;
                }

                var nUiIds = uiRows.Values.Select(r => r.ID.ToString()).ToList();
                var nUfIds = ufRows.Values.Select(r => r.ID.ToString()).ToList();
                GISA.Search.Updater.updateNivelDocumental(nUiIds);
                GISA.Search.Updater.updateNivelDocumentalComProdutores(nUiIds);
                GISA.Search.Updater.updateUnidadeFisica(nUfIds);

                PersistencyHelper.cleanDeletedData();

                MessageBox.Show("Importação concluída com sucesso.", TAG, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                MessageBox.Show("Ocorreu um erro durante a importação." + System.Environment.NewLine + "A operação foi cancelada.", TAG, MessageBoxButtons.OK, MessageBoxIcon.Error);
                GisaDataSetHelper.GetInstance().RejectChanges();
            }
        }

        private static IEnumerable<string> ValidaIdentificadores(IEnumerable<string> list)
        {
            var a = list.GroupBy(v => v).Where(g => g.Count() > 1).FirstOrDefault();

            return a;
        }

        private static void ValidaCamposObrigatorios(UnidadeInformacional ui)
        {
            if (ui.identificador == null || ui.identificador.Length == 0)
                ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_IDENTIFICADOR, string.Empty, ExceptionHelper.ERR_VALOR_NAO_DEFINIDO);

            if (ui.nivel == null || ui.nivel.Length == 0)
                ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_NIVEL, string.Empty, ExceptionHelper.ERR_VALOR_NAO_DEFINIDO);

            if (ui.codigoRef == null || ui.codigoRef.Length == 0)
                ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_CODIGOREF, string.Empty, ExceptionHelper.ERR_VALOR_NAO_DEFINIDO);

            if (ui.titulo == null || ui.titulo.Length == 0)
                ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_TITULO, string.Empty, ExceptionHelper.ERR_VALOR_NAO_DEFINIDO);

            if ((ui.idNivelSuperior == null || ui.idNivelSuperior.Length == 0) && (ui.entidadesProdutoras == null || ui.entidadesProdutoras.Count == 0))
                ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_IDNIVELSUPERIOR + ", " + ImportExcel.UI_ENTIDADESPRODUTORAS, string.Empty, ExceptionHelper.ERR_VALOR_NAO_DEFINIDO);
        }

        private static void ValidaCamposObrigatorios(UnidadeFisica uf)
        {
            if (uf.identificador == null || uf.identificador.Length == 0)
                ExceptionHelper.ThrowException(ExceptionHelper.TAB_UNIDADES_FISICAS, uf.identificador, ImportExcel.UF_IDENTIFICADOR, string.Empty, ExceptionHelper.ERR_VALOR_NAO_DEFINIDO);

            if (uf.titulo == null || uf.titulo.Length == 0)
                ExceptionHelper.ThrowException(ExceptionHelper.TAB_UNIDADES_FISICAS, uf.identificador, ImportExcel.UF_TITULO, string.Empty, ExceptionHelper.ERR_VALOR_NAO_DEFINIDO);

            if (uf.entidadeDetentora == null || uf.entidadeDetentora.Length == 0)
                ExceptionHelper.ThrowException(ExceptionHelper.TAB_UNIDADES_FISICAS, uf.identificador, ImportExcel.UF_ENTIDADEDETENTORA, string.Empty, ExceptionHelper.ERR_VALOR_NAO_DEFINIDO);
        }

        private static void ValidaCodigosReferencia(List<UnidadeInformacional> uiList)
        {
            var rels = new Dictionary<string, HashSet<UnidadeInformacional>>();

            uiList.ForEach(ui =>
            {
                ui.entidadesProdutoras.ForEach(ep => 
                {
                    if (!rels.ContainsKey(ep))
                        rels[ep] = new HashSet<UnidadeInformacional>();

                    rels[ep].Add(ui);
                });

                if (ui.idNivelSuperior.Length > 0)
                {
                    if (!rels.ContainsKey(ui.idNivelSuperior))
                        rels[ui.idNivelSuperior] = new HashSet<UnidadeInformacional>();

                    rels[ui.idNivelSuperior].Add(ui);
                }
            });

            foreach (var rel in rels)
            {
                var rep = rel.Value.ToList().Select(ui => ui.codigoRef).GroupBy(v => v).Where(g => g.Count() > 1).FirstOrDefault();
                if (rep != null) {
                    var uis = uiList.Where(ui => ui.codigoRef.Equals(rep.Key)).Select(ui => ui.identificador);
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, String.Join(", ", uis.ToArray()), ImportExcel.UI_CODIGOREF, rep.Key, ExceptionHelper.ERR_VALOR_REPETIDO);
                }
            }
        }

        public static string GetFileToImport()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Excel 97-2003 (*.xls)|*.xls|Excel (*.xlsx)|*.xlsx";
            //openFileDialog1.Filter = "Excel 97-2003 (*.xls)|*.xls";
            openFileDialog1.Title = "Selecione o ficheiro";
            openFileDialog1.Multiselect = false;
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                return openFileDialog1.FileName;
            return "";
        }

        #region Load information
        private static void LoadEDsInfo()
        {
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                GisaDataSetHelper.ManageDatasetConstraints(false);
                NivelRule.Current.LoadUFsRelatedData(GisaDataSetHelper.GetInstance(), ho.Connection);
                GisaDataSetHelper.ManageDatasetConstraints(true);
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
        }

        private static void LoadNiveisDoc(List<string> niveisDoc)
        {
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                GisaDataSetHelper.ManageDatasetConstraints(false);
                ImportRule.Current.LoadDocumentos(GisaDataSetHelper.GetInstance(), niveisDoc.ToArray(), ho.Connection);
                GisaDataSetHelper.ManageDatasetConstraints(true);
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
        }

        private static void LoadNiveisUfAssoc(List<string> niveisUfAssoc)
        {
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                GisaDataSetHelper.ManageDatasetConstraints(false);
                ImportRule.Current.LoadUnidadesFisicas(GisaDataSetHelper.GetInstance(), niveisUfAssoc.ToArray(), ho.Connection);
                GisaDataSetHelper.ManageDatasetConstraints(true);
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
        }

        private static void LoadControlosAutoridade(List<string> cas)
        {
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                GisaDataSetHelper.ManageDatasetConstraints(false);
                ImportRule.Current.LoadControloAuts(GisaDataSetHelper.GetInstance(), cas.ToArray(), ho.Connection);
                GisaDataSetHelper.ManageDatasetConstraints(true);
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
        }

        private static void LoadTrustees()
        {
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                GisaDataSetHelper.ManageDatasetConstraints(false);
                TrusteeRule.Current.LoadTrusteesUsr(GisaDataSetHelper.GetInstance(), ho.Connection);
                GisaDataSetHelper.ManageDatasetConstraints(true);
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
        }

        private static void LoadLocalConsulta()
        {
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                GisaDataSetHelper.ManageDatasetConstraints(false);
                RelatorioRule.Current.LoadLocaisConsulta(GisaDataSetHelper.GetInstance(), ho.Connection);
                GisaDataSetHelper.ManageDatasetConstraints(true);
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
        }
        #endregion

        public static void ValidateImport(PersistencyHelper.PreConcArguments args)
        {
            try
            {
                GisaDataSetHelper.GetInstance().EnforceConstraints = false;
                var vipca = (PersistencyHelper.ValidaImportPreConcArguments)args;
                foreach (var pcArg in vipca.newDocsList)
                {
                    pcArg.tran = vipca.tran;
                    pcArg.gisaBackup = vipca.gisaBackup;
                    pcArg.continueSave = vipca.continueSave;
                    DelegatesHelper.ValidateNivelAddAndAssocNewUF(pcArg);

                    if (!pcArg.continueSave)
                    {
                        GisaDataSetHelper.GetInstance().RejectChanges();
                        vipca.continueSave = false;
                        vipca.errorMessage = pcArg.message;
                        return;
                    }
                }

                foreach (var pcArg in vipca.newUfsList)
                {
                    pcArg.tran = vipca.tran;
                    pcArg.gisaBackup = vipca.gisaBackup;
                    pcArg.continueSave = vipca.continueSave;
                    DelegatesHelper.HandleUF(pcArg);

                    if (!pcArg.continueSave)
                    {
                        GisaDataSetHelper.GetInstance().RejectChanges();
                        vipca.continueSave = false;
                        vipca.errorMessage = pcArg.message;
                        break;
                    }
                }
            }
            finally
            {
                GisaDataSetHelper.GetInstance().EnforceConstraints = true;
            }
        }

        public static void ValidateImport(PersistencyHelper.PreSaveArguments args)
        {
            var psArgs = args as PersistencyHelper.ValidaImportPreSaveArguments;
            foreach (var newDocArg in psArgs.newDocArgs)
            {
                newDocArg.tran = args.tran;
                DelegatesHelper.SetNewCodigos(newDocArg);
            }

            foreach (var newUfArg in psArgs.newUfArgs)
            {
                newUfArg.tran = args.tran;
                DelegatesHelper.SetCodigo(newUfArg);
            }
        }

        private static void UFsToGISADatasetRows(UnidadeFisica uf)
        {
            var ndEDRow = GisaDataSetHelper.GetInstance().NivelDesignado.Cast<GISADataset.NivelDesignadoRow>().SingleOrDefault(r => r.Designacao.Equals(uf.entidadeDetentora));
            if (ndEDRow == null)
                ExceptionHelper.ThrowException(ExceptionHelper.TAB_UNIDADES_FISICAS, uf.identificador, ImportExcel.UF_ENTIDADEDETENTORA, uf.entidadeDetentora, ExceptionHelper.ERR_VALOR_INVALIDO);

            var nRow = createNivelRow();
            nRow.IDTipoNivel = TipoNivel.OUTRO;
            nRow.Codigo = UnidadesFisicasHelper.GenerateNewCodigoString(ndEDRow.NivelRow, System.DateTime.Now.Year);
            createRelacaoHierarquicaRow(nRow, ndEDRow.NivelRow, TipoNivelRelacionado.UF);
            var ndRow = createNivelDesignadoRow(); // titulo
            ndRow.Designacao = uf.titulo;
            ndRow.NivelRow = nRow;
            var nufRow = createNivelUnidadeFisicaRow(uf); // tipo entrega, guia, cod. barras, local consulta
            nufRow.NivelDesignadoRow = ndRow;
            if (uf.tipoEntrega.Length > 0)
            {
                var tipoEntregaRow = GisaDataSetHelper.GetInstance().TipoEntrega.Cast<GISADataset.TipoEntregaRow>().SingleOrDefault(r => r.Designacao.Equals(uf.tipoEntrega));
                if (tipoEntregaRow == null)
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_UNIDADES_FISICAS, uf.identificador, ImportExcel.UF_TIPOENTREGA, uf.tipoEntrega, ExceptionHelper.ERR_VALOR_INVALIDO);
                else
                    nufRow.TipoEntregaRow = tipoEntregaRow;
            }
            var frdRow = createFRDBaseRow();
            frdRow.IDTipoFRDBase = 2;
            frdRow.NivelRow = nRow;

            var sfrdufcotaRow = createSFRDUFCotaRow(uf); // cota
            sfrdufcotaRow.FRDBaseRow = frdRow;

            var sfrdufdfRow = createSFRDUFDescricaoFisicaRow(uf); // altura, largura, profundidade, tipo
            sfrdufdfRow.FRDBaseRow = frdRow;

            var sfrdufceRow = createSFRDConteudoEEstruturaRow(); // conteudo informacional
            sfrdufceRow.ConteudoInformacional = uf.conteudoInformacional;
            sfrdufceRow.FRDBaseRow = frdRow;

            var sfrddtRow = createSFRDDatasProducaoRow(uf);
            sfrddtRow.FRDBaseRow = frdRow;

            if (uf.localConsulta.Length > 0)
            {
                var localConsultaRow = GisaDataSetHelper.GetInstance().LocalConsulta.Cast<GISADataset.LocalConsultaRow>().SingleOrDefault(r => r.Designacao.Equals(uf.localConsulta));
                if (localConsultaRow == null)
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_UNIDADES_FISICAS, uf.identificador, ImportExcel.UF_LOCALCONSULTA, uf.localConsulta, ExceptionHelper.ERR_VALOR_INVALIDO);
                else
                    nufRow.LocalConsultaRow = localConsultaRow;
            }

            registos.Add(frdRow, new Registo() { CurrentFRDBase = frdRow, tuOperator = SessionHelper.GetGisaPrincipal().TrusteeUserOperator, tuAuthor = SessionHelper.GetGisaPrincipal().TrusteeUserAuthor ?? default(GISADataset.TrusteeUserRow), data = System.DateTime.Now });

            ufRows[uf.identificador] = nRow;
        }

        private static void UIsToGISADatasetRow(UnidadeInformacional ui)
        {
            var nRow = createNivelRow();
            nRow.IDTipoNivel = TipoNivel.DOCUMENTAL;

            if (!GUIHelper.GUIHelper.CheckValidCodigoParcialForTipo(ui.codigoRef, TipoNivelRelacionado.D))
                ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_CODIGOREF, ui.codigoRef, ExceptionHelper.ERR_VALOR_INVALIDO);
            nRow.Codigo = ui.codigoRef;

            var ndRow = createNivelDesignadoRow(); // titulo
            ndRow.Designacao = ui.titulo;
            ndRow.NivelRow = nRow;

            var frdRow = createFRDBaseRow();
            frdRow.IDTipoFRDBase = 1;
            frdRow.NivelRow = nRow;
            frdRow.NotaDoArquivista = ui.notaArquivista;
            frdRow.RegrasOuConvencoes = ui.regras;

            var sfrddtRow = createSFRDDatasProducaoRow(ui);
            sfrddtRow.FRDBaseRow = frdRow;

            var sfrddsRow = createSFRDDimensaoSuporteRow(ui);
            sfrddsRow.FRDBaseRow = frdRow;

            var sfrdcRow = createSFRDContextoRow(ui);
            sfrdcRow.FRDBaseRow = frdRow;

            var sfrdaRow = createSFRDAvaliacaoRow(ui);
            sfrdaRow.FRDBaseRow = frdRow;

            var sfrdceRow = createSFRDConteudoEEstruturaRow();
            sfrdceRow.FRDBaseRow = frdRow;
            sfrdceRow.ConteudoInformacional = ui.conteudoInformacional;
            sfrdceRow.Incorporacao = ui.incorporacoes;

            if (ui.tradicaoDocumental.Count > 0)
            {
                ui.tradicaoDocumental.ForEach(td =>
                {
                    var ttdRow = GisaDataSetHelper.GetInstance().TipoTradicaoDocumental.Cast<GISADataset.TipoTradicaoDocumentalRow>().SingleOrDefault(ttd => ttd.Designacao.Equals(td));
                    if (ttdRow == null)
                        ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_TRADICAODOCUMENTAL, td, ExceptionHelper.ERR_VALOR_INVALIDO);

                    var sfrdttdRow = createSFRDTradicaoDocumentalRow();
                    sfrdttdRow.FRDBaseRow = frdRow;
                    sfrdttdRow.TipoTradicaoDocumentalRow = ttdRow;
                });
            }

            if (ui.ordenacao.Count > 0)
            {
                ui.ordenacao.ForEach(o =>
                {
                    var ordRow = GisaDataSetHelper.GetInstance().TipoOrdenacao.Cast<GISADataset.TipoOrdenacaoRow>().SingleOrDefault(ord => ord.Designacao.Equals(o));
                    if (ordRow == null)
                        ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_ORDENACAO, o, ExceptionHelper.ERR_VALOR_INVALIDO);

                    var sfrdordRow = createSFRDOrdenacaoRow();
                    sfrdordRow.FRDBaseRow = frdRow;
                    sfrdordRow.TipoOrdenacaoRow = ordRow;
                });
            }

            var sfrdcaRow = createSFRDCondicaoDeAcessoRow(ui);
            sfrdcaRow.FRDBaseRow = frdRow;

            if (ui.lingua.Count > 0)
            {
                ui.lingua.ForEach(lg =>
                {
                    var lRow = GisaDataSetHelper.GetInstance().Iso639.Cast<GISADataset.Iso639Row>().SingleOrDefault(l => l.LanguageNameEnglish.Equals(lg));
                    if (lRow == null)
                        ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_LINGUA, lg, ExceptionHelper.ERR_VALOR_INVALIDO);

                    var sfrdlRow = createSFRDLinguaRow();
                    sfrdlRow.SFRDCondicaoDeAcessoRow = sfrdcaRow;
                    sfrdlRow.Iso639Row = lRow;
                });
            }

            if (ui.alfabeto.Count > 0)
            {
                ui.alfabeto.ForEach(alf =>
                {
                    var aRow = GisaDataSetHelper.GetInstance().Iso15924.Cast<GISADataset.Iso15924Row>().SingleOrDefault(a => a.ScriptNameEnglish.Equals(alf));
                    if (aRow == null)
                        ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_ALFABETO, alf, ExceptionHelper.ERR_VALOR_INVALIDO);

                    var sfrdalfRow = createSFRDAlfabetoRow();
                    sfrdalfRow.SFRDCondicaoDeAcessoRow = sfrdcaRow;
                    sfrdalfRow.Iso15924Row = aRow;
                });
            }

            if (ui.formaSuporte.Count > 0)
            {
                ui.formaSuporte.ForEach(fs =>
                {
                    var fsRow = GisaDataSetHelper.GetInstance().TipoFormaSuporteAcond.Cast<GISADataset.TipoFormaSuporteAcondRow>().SingleOrDefault(tfsa => tfsa.Designacao.Equals(fs));
                    if (fsRow == null)
                        ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_FORMASUPORTE, fs, ExceptionHelper.ERR_VALOR_INVALIDO);

                    var sfrdtfsaRow = createSFRDFormaSuporteAcondRow();
                    sfrdtfsaRow.SFRDCondicaoDeAcessoRow = sfrdcaRow;
                    sfrdtfsaRow.TipoFormaSuporteAcondRow = fsRow;
                });
            }

            if (ui.materialSuporte.Count > 0)
            {
                ui.materialSuporte.ForEach(ms =>
                {
                    var msRow = GisaDataSetHelper.GetInstance().TipoMaterialDeSuporte.Cast<GISADataset.TipoMaterialDeSuporteRow>().SingleOrDefault(tms => tms.Designacao.Equals(ms));
                    if (msRow == null)
                        ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_MATERIALSUPORTE, ms, ExceptionHelper.ERR_VALOR_INVALIDO);

                    var sfrdtmsRow = createSFRDMaterialDeSuporteRow();
                    sfrdtmsRow.SFRDCondicaoDeAcessoRow = sfrdcaRow;
                    sfrdtmsRow.TipoMaterialDeSuporteRow = msRow;
                });
            }

            if (ui.tecnicaRegisto.Count > 0)
            {
                ui.tecnicaRegisto.ForEach(tr =>
                {
                    var trRow = GisaDataSetHelper.GetInstance().TipoTecnicasDeRegisto.Cast<GISADataset.TipoTecnicasDeRegistoRow>().SingleOrDefault(ttr => ttr.Designacao.Equals(tr));
                    if (trRow == null)
                        ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_TECNICAREGISTO, tr, ExceptionHelper.ERR_VALOR_INVALIDO);

                    var sfrdtrRow = createSFRDTecnicasDeRegistoRow();
                    sfrdtrRow.SFRDCondicaoDeAcessoRow = sfrdcaRow;
                    sfrdtrRow.TipoTecnicasDeRegistoRow = trRow;
                });
            }

            if (ui.estadoConservacao.Length > 0)
            {
                var tecRow = GisaDataSetHelper.GetInstance().TipoEstadoDeConservacao.Cast<GISADataset.TipoEstadoDeConservacaoRow>().SingleOrDefault(tec => tec.Designacao.Equals(ui.estadoConservacao));
                if (tecRow == null)
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_ESTADOCONSERVACAO, ui.estadoConservacao, ExceptionHelper.ERR_VALOR_INVALIDO);

                var sfrdecRow = createSFRDEstadoDeConservacaoRow();
                sfrdecRow.SFRDCondicaoDeAcessoRow = sfrdcaRow;
                sfrdecRow.TipoEstadoDeConservacaoRow = tecRow;
            }

            var sfrddaRow = createSFRDDocumentacaoAssociadaRow(ui);
            sfrddaRow.FRDBaseRow = frdRow;

            var sfrdngRow = createSFRDNotaGeralRow(ui);
            sfrdngRow.FRDBaseRow = frdRow;

            var tUserRow = SessionHelper.GetGisaPrincipal().TrusteeUserAuthor ?? default(GISADataset.TrusteeUserRow);
            if (ui.autorDescricao != null && ui.autorDescricao.Length > 0)
            {
                var tRow = GisaDataSetHelper.GetInstance().Trustee.Cast<GISADataset.TrusteeRow>().SingleOrDefault(t => t.Name.Equals(ui.autorDescricao));
                if (tRow == null)
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_AUTORDESCRICAO, ui.autorDescricao, ExceptionHelper.ERR_VALOR_INVALIDO);
                else if (!tRow.GetTrusteeUserRows().First().IsAuthority)
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_AUTORDESCRICAO, ui.autorDescricao, ExceptionHelper.ERR_VALOR_INVALIDO);
                    
            }

            DateTime data = DateTime.Now;
            if (ui.dataAutoria != null && ui.dataAutoria.Length > 0)
            {
                var convertedDate = ConvertToDateTime(ui.dataAutoria);

                if (convertedDate == default(DateTime))
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_DATAAUTORIA, ui.dataAutoria, ExceptionHelper.ERR_VALOR_INVALIDO);
                else
                    data = convertedDate;
            }

            if (ui.entidadesProdutoras != null && ui.entidadesProdutoras.Count > 0 && ui.idNivelSuperior != null && ui.idNivelSuperior.Length > 0)
                ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_IDNIVELSUPERIOR + ", " + ImportExcel.UI_ENTIDADESPRODUTORAS, ui.entidadesProdutoras + ", " + ui.idNivelSuperior, ExceptionHelper.ERR_VALOR_INVALIDO);

            // TODO: validar hierarquia (impedir que existam casos tipo: documento > sub-documento > sub-documento)
            if (ui.entidadesProdutoras != null && ui.entidadesProdutoras.Count > 0)
            {
                ui.entidadesProdutoras.ForEach(ep =>
                {
                    var d = GisaDataSetHelper.GetInstance().Dicionario.Cast<GISADataset.DicionarioRow>().SingleOrDefault(r => r.Termo.Equals(ep));

                    if (d == null)
                        ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_ENTIDADESPRODUTORAS, ep, ExceptionHelper.ERR_VALOR_INVALIDO);

                    var nCARow = d.GetControloAutDicionarioRows()
                                    .Single(cad => cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada)
                                    .ControloAutRow.GetNivelControloAutRows().Single().NivelRow;

                    createRelacaoHierarquicaRow(nRow, nCARow, TipoNivelRelacionado.D);
                    //createTipoDocumentoRow(ui, nRow, TipoNivelRelacionado.D);
                });
            }
            else if (ui.idNivelSuperior != null && ui.idNivelSuperior.Length > 0)
            {
                if (ui.idNivelSuperior.StartsWith("gisa:"))
                {
                    var nUpperRow = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().SingleOrDefault(r => r.ID == System.Convert.ToInt64(ui.idNivelSuperior.Replace("gisa:", "")));
                    if (nUpperRow == null)
                        ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_IDNIVELSUPERIOR, ui.idNivelSuperior, ExceptionHelper.ERR_VALOR_INVALIDO);

                    var rhRow = nUpperRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First();
                    var idNTR = rhRow.IDTipoNivelRelacionado;

                    if (idNTR != TipoNivelRelacionado.SR && idNTR != TipoNivelRelacionado.SSR && idNTR != TipoNivelRelacionado.D)
                        ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_IDNIVELSUPERIOR2, ui.idNivelSuperior, ExceptionHelper.ERR_VALOR_INVALIDO);
                    
                    var idTipoNivelRelacionado = rhRow.IDTipoNivelRelacionado == TipoNivelRelacionado.D ? TipoNivelRelacionado.SD : TipoNivelRelacionado.D;

                    createRelacaoHierarquicaRow(nRow, nUpperRow, idTipoNivelRelacionado);
                }
                else
                {
                    if (!unidadesInformacionais.ContainsKey(ui.idNivelSuperior))
                        ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_IDNIVELSUPERIOR, ui.idNivelSuperior, ExceptionHelper.ERR_ID_NAO_LISTADO);

                    if (!uiRows.ContainsKey(ui.idNivelSuperior))
                        UIsToGISADatasetRow(unidadesInformacionais[ui.idNivelSuperior]);
                    
                    var nUpperRow = uiRows[ui.idNivelSuperior];
                    createRelacaoHierarquicaRow(nRow, nUpperRow, TipoNivelRelacionado.SD);
                    //createNivelDocumentoSimples(ui, nRow);
                }
            }
            else
                ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_IDNIVELSUPERIOR + ", " + ImportExcel.UI_ENTIDADESPRODUTORAS, string.Empty, ExceptionHelper.ERR_VALOR_NAO_DEFINIDO);

            if (ui.unidadesFisicas.Count > 0)
            {
                ui.unidadesFisicas.ForEach(uf =>
                {
                    if (uf.StartsWith("gisa_uf:"))
                    {
                        var codeParts = uf.Replace("gisa_uf:", "").Split('/');
                        var codeED = codeParts[0];
                        var codeUF = codeParts[1];

                        var ufRows = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().Where(r => r.Codigo.Equals(codeUF));
                        if (ufRows.Count() == 0) 
                            ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_UNIDADESFISICAS, uf, ExceptionHelper.ERR_VALOR_INVALIDO);
                            
                        var ufRow = ufRows.SingleOrDefault(row => row.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().Single().NivelRowByNivelRelacaoHierarquicaUpper.Codigo.Equals(codeED));
                        if (ufRow == null)
                            ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_UNIDADESFISICAS, uf, ExceptionHelper.ERR_VALOR_INVALIDO);

                        var sfrdufRow = createSFRDUnidadeFisicaRow(frdRow, ufRow);
                        if (ui.unidadesFisicas.Count() == 1)
                            sfrdufRow.Cota = ui.cotaDoc;
                    }
                    else
                    {
                        if (!unidadesFisicas.ContainsKey(uf))
                            ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_UNIDADESFISICAS, uf, ExceptionHelper.ERR_ID_NAO_LISTADO);

                        if (!ufRows.ContainsKey(uf))
                            UFsToGISADatasetRows(unidadesFisicas[uf]);

                        var ufRow = ufRows[uf];
                        var sfrdufRow = createSFRDUnidadeFisicaRow(frdRow, ufRow);
                        if (ui.unidadesFisicas.Count() == 1)
                            sfrdufRow.Cota = ui.cotaDoc;
                    }
                });
            }

            MapControloAuts(frdRow, ui.autores, null, ImportExcel.UI_AUTORES, TipoNoticiaAut.EntidadeProdutora, ui);
            MapControloAuts(frdRow, ui.modelo, null, ImportExcel.UI_MODELO, TipoNoticiaAut.Modelo, ui);
            MapControloAuts(frdRow, ui.diplomaLegal, null, ImportExcel.UI_DIPLOMALEGAL, TipoNoticiaAut.Diploma, ui);
            MapControloAuts(frdRow, ui.onomasticos, null, ImportExcel.UI_ONOMASTICOS, TipoNoticiaAut.Onomastico, ui);
            MapControloAuts(frdRow, ui.geograficos, null, ImportExcel.UI_GEOGRAFICOS, TipoNoticiaAut.ToponimicoGeografico, ui);
            MapControloAuts(frdRow, ui.ideograficos, null, ImportExcel.UI_IDEOGRAFICOS, TipoNoticiaAut.Ideografico, ui);
            if (ui.tipoInformacional.Length > 0)
                MapControloAuts(frdRow, new List<string>() { ui.tipoInformacional }, -1, ImportExcel.UI_TIPOINFORMACIONAL, TipoNoticiaAut.TipologiaInformacional, ui);

            registos.Add(frdRow, new Registo() { CurrentFRDBase = frdRow, tuOperator = SessionHelper.GetGisaPrincipal().TrusteeUserOperator, tuAuthor = tUserRow, data = data });

            GisaDataSetHelper.GetInstance().SFRDAgrupador.AddSFRDAgrupadorRow(frdRow, "", new byte[] { }, 0);

            PermissoesHelper.AddNewNivelGrantPermissions(nRow, nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First().NivelRowByNivelRelacaoHierarquicaUpper);
            if (sfrdaRow.Publicar)
                PermissoesHelper.ChangeDocPermissionPublicados(nRow.ID, sfrdaRow.Publicar);

            uiRows[ui.identificador] = nRow;
        }

        private static DateTime ConvertToDateTime(string val)
        {
            var date = default(DateTime);

            if (val.Length != 8) return date;

            var ano = val.Substring(0, 4);
            var mes = val.Substring(4, 2);
            var dia = val.Substring(6, 2);

            try
            {
                date = new DateTime(System.Convert.ToInt32(ano), System.Convert.ToInt32(mes), System.Convert.ToInt32(dia));
            }
            catch (Exception e)  {
                Trace.WriteLine("Data inválida. " + e);
            }

            return date;
        }

        
        private static void MapControloAuts(GISADataset.FRDBaseRow frdRow, List<string> cas, Int32? selector, string messageTag, TipoNoticiaAut tna, UnidadeInformacional ui)
        {
            if (cas != null && cas.Count > 0)
            {
                cas.ForEach(termo =>
                {
                    var d = GisaDataSetHelper.GetInstance().Dicionario.Cast<GISADataset.DicionarioRow>().SingleOrDefault(r => r.Termo.Equals(termo));

                    if (d == null)
                        ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, messageTag, termo, ExceptionHelper.ERR_VALOR_INVALIDO);

                    var caRow = d.GetControloAutDicionarioRows()
                                    .Where(cad => cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada)
                                    .Select(cad => cad.ControloAutRow)
                                    .SingleOrDefault(ca => ca.IDTipoNoticiaAut == (int)tna);

                    if (caRow == null)
                        ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, messageTag, termo, ExceptionHelper.ERR_VALOR_INVALIDO);

                    if (caRow.IDTipoNoticiaAut == (int)TipoNoticiaAut.EntidadeProdutora)
                        createSFRDAutorRow(frdRow, caRow);
                    else
                        createIndexFRDCARow(frdRow, caRow, selector);
                });
            }
        }

        private static GISADataset.NivelRow createNivelRow()
        {
            var nRow = GisaDataSetHelper.GetInstance().Nivel.NewNivelRow();
            nRow.CatCode = "NVL";
            nRow.Codigo = "";
            nRow.isDeleted = 0;
            nRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().Nivel.AddNivelRow(nRow);
            return nRow;
        }

        private static GISADataset.NivelDesignadoRow createNivelDesignadoRow()
        {
            var ndRow = GisaDataSetHelper.GetInstance().NivelDesignado.NewNivelDesignadoRow();
            ndRow.isDeleted = 0;
            ndRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().NivelDesignado.AddNivelDesignadoRow(ndRow);
            return ndRow;
        }

        private static GISADataset.NivelUnidadeFisicaRow createNivelUnidadeFisicaRow(UnidadeFisica uf)
        {
            var nufRow = GisaDataSetHelper.GetInstance().NivelUnidadeFisica.NewNivelUnidadeFisicaRow();
            nufRow.GuiaIncorporacao = uf.guia;

            nufRow.CodigoBarras = "";
            if (uf.codigoBarras.Length > 0)
            {
                if (MathHelper.IsInteger(uf.codigoBarras))
                    nufRow.CodigoBarras = uf.codigoBarras;
                else
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_UNIDADES_FISICAS, uf.identificador, ImportExcel.UF_CODIGOBARRAS, uf.codigoBarras, ExceptionHelper.ERR_VALOR_INVALIDO);
            }
            nufRow.isDeleted = 0;
            nufRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().NivelUnidadeFisica.AddNivelUnidadeFisicaRow(nufRow);
            return nufRow;
        }

        private static GISADataset.FRDBaseRow createFRDBaseRow()
        {
            var frdRow = GisaDataSetHelper.GetInstance().FRDBase.NewFRDBaseRow();
            frdRow.isDeleted = 0;
            frdRow.NotaDoArquivista = "";
            frdRow.RegrasOuConvencoes = "";
            frdRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().FRDBase.AddFRDBaseRow(frdRow);
            return frdRow;
        }

        private static GISADataset.SFRDUFCotaRow createSFRDUFCotaRow(UnidadeFisica uf)
        {
            var sfrdufcotaRow = GisaDataSetHelper.GetInstance().SFRDUFCota.NewSFRDUFCotaRow();
            sfrdufcotaRow.Cota = uf.cota;
            sfrdufcotaRow.isDeleted = 0;
            sfrdufcotaRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDUFCota.AddSFRDUFCotaRow(sfrdufcotaRow);
            return sfrdufcotaRow;
        }

        private static GISADataset.SFRDUFDescricaoFisicaRow createSFRDUFDescricaoFisicaRow(UnidadeFisica uf)
        {
            var sfrdufdfRow = default(GISADataset.SFRDUFDescricaoFisicaRow);
            sfrdufdfRow = GisaDataSetHelper.GetInstance().SFRDUFDescricaoFisica.NewSFRDUFDescricaoFisicaRow();

            sfrdufdfRow["MedidaAltura"] = DBNull.Value;
            if (uf.altura.Length > 0)
            {
                if (MathHelper.IsDecimal(uf.altura))
                    sfrdufdfRow.MedidaAltura = System.Convert.ToDecimal(uf.altura);
                else
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_UNIDADES_FISICAS, uf.identificador, ImportExcel.UF_ALTURA, uf.altura, ExceptionHelper.ERR_VALOR_INVALIDO);
            }

            sfrdufdfRow["MedidaLargura"] = DBNull.Value;
            if (uf.largura.Length > 0)
            {
                if (MathHelper.IsDecimal(uf.largura))
                    sfrdufdfRow.MedidaLargura = System.Convert.ToDecimal(uf.largura);
                else
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_UNIDADES_FISICAS, uf.identificador, ImportExcel.UF_LARGURA, uf.largura, ExceptionHelper.ERR_VALOR_INVALIDO);
            }

            sfrdufdfRow["MedidaProfundidade"] = DBNull.Value;
            if (uf.profundidade.Length > 0)
            {
                if (MathHelper.IsDecimal(uf.profundidade))
                    sfrdufdfRow.MedidaProfundidade = System.Convert.ToDecimal(uf.profundidade);
                else
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_UNIDADES_FISICAS, uf.identificador, ImportExcel.UF_PROFUNDIDADE, uf.profundidade, ExceptionHelper.ERR_VALOR_INVALIDO);
            }

            sfrdufdfRow.TipoAcondicionamentoRow = GisaDataSetHelper.GetInstance().TipoAcondicionamento.Cast<GISADataset.TipoAcondicionamentoRow>().First();
            if (uf.tipo.Length > 0)
            {
                var tipoAcondicionamentoRow = GisaDataSetHelper.GetInstance().TipoAcondicionamento.Cast<GISADataset.TipoAcondicionamentoRow>().SingleOrDefault(r => r.Designacao.Equals(uf.tipo));
                if (tipoAcondicionamentoRow == null)
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_UNIDADES_FISICAS, uf.identificador, ImportExcel.UF_TIPO, uf.tipo, ExceptionHelper.ERR_VALOR_INVALIDO);
                else
                    sfrdufdfRow.TipoAcondicionamentoRow = tipoAcondicionamentoRow;
            }

            sfrdufdfRow.IDTipoMedida = 1;
            sfrdufdfRow.isDeleted = 0;
            sfrdufdfRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDUFDescricaoFisica.AddSFRDUFDescricaoFisicaRow(sfrdufdfRow);

            return sfrdufdfRow;
        }

        private static GISADataset.SFRDConteudoEEstruturaRow createSFRDConteudoEEstruturaRow()
        {
            var sfrdceRow = GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.NewSFRDConteudoEEstruturaRow();
            sfrdceRow.ConteudoInformacional = "";
            sfrdceRow.Incorporacao = "";
            sfrdceRow.isDeleted = 0;
            sfrdceRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.AddSFRDConteudoEEstruturaRow(sfrdceRow);
            return sfrdceRow;
        }

        private static string[] dataIncerta = new string[] { "Antes de", "Depois de", "Cerca de" };
        private static GISADataset.SFRDDatasProducaoRow createSFRDDatasProducaoRow(UnidadeInformacional ui)
        {
            var sfrddtRow = GisaDataSetHelper.GetInstance().SFRDDatasProducao.NewSFRDDatasProducaoRow();
            if (ui.anoInicio.Length > 0)
            {
                if (!DateHelper.IsValidYear(ui.anoInicio))
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_ANOINICIO, ui.anoInicio, ExceptionHelper.ERR_VALOR_INVALIDO);
                sfrddtRow.InicioAno = ui.anoInicio;
            }

            if (ui.mesInicio.Length > 0)
            {
                if (!DateHelper.IsValidMonth(ui.mesInicio))
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_MESINICIO, ui.mesInicio, ExceptionHelper.ERR_VALOR_INVALIDO);
                sfrddtRow.InicioMes = ui.mesInicio;
            }

            if (ui.diaInicio.Length > 0)
            {
                if (!DateHelper.IsValidDay(ui.diaInicio))
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_DIAINICIO, ui.diaInicio, ExceptionHelper.ERR_VALOR_INVALIDO);
                sfrddtRow.InicioDia = ui.diaInicio;
            }

            if (ui.atribuidaInicio.Length > 0)
            {
                if (!isValidBool(ui.atribuidaInicio))
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_ATRIBUIDAINICIO, ui.atribuidaInicio, ExceptionHelper.ERR_VALOR_INVALIDO);
                sfrddtRow.InicioAtribuida = System.Convert.ToBoolean(System.Convert.ToInt32(ui.atribuidaInicio));
            }
            else
                sfrddtRow.InicioAtribuida = false;

            if (ui.dataIncerta.Length > 0)
            {
                if (!dataIncerta.Contains(ui.dataIncerta))
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_DATAINCERTA, ui.dataIncerta, ExceptionHelper.ERR_VALOR_INVALIDO);
                sfrddtRow.InicioTexto = ui.dataIncerta;
            }

            if (ui.anoFim.Length > 0)
            {
                if (!DateHelper.IsValidYear(ui.anoFim))
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_ANOFIM, ui.anoFim, ExceptionHelper.ERR_VALOR_INVALIDO);
                sfrddtRow.FimAno = ui.anoFim;
            }

            if (ui.mesFim.Length > 0)
            {
                if (!DateHelper.IsValidMonth(ui.mesFim))
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_MESFIM, ui.mesFim, ExceptionHelper.ERR_VALOR_INVALIDO);
                sfrddtRow.FimMes = ui.mesFim;
            }

            if (ui.diaFim.Length > 0)
            {
                if (!DateHelper.IsValidDay(ui.diaFim))
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_DIAFIM, ui.diaFim, ExceptionHelper.ERR_VALOR_INVALIDO);
                sfrddtRow.FimDia = ui.diaFim;
            }

            if (ui.atribuidaFim.Length > 0)
            {
                if (!isValidBool(ui.atribuidaInicio))
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_ATRIBUIDAFIM, ui.atribuidaFim, ExceptionHelper.ERR_VALOR_INVALIDO);
                sfrddtRow.FimAtribuida = System.Convert.ToBoolean(System.Convert.ToInt32(ui.atribuidaFim));
            }
            else
                sfrddtRow.FimAtribuida = false;


            sfrddtRow.FimTexto = "";
            sfrddtRow.isDeleted = 0;
            sfrddtRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDDatasProducao.AddSFRDDatasProducaoRow(sfrddtRow);
            return sfrddtRow;
        }

        private static GISADataset.SFRDDatasProducaoRow createSFRDDatasProducaoRow(UnidadeFisica uf)
        {
            var sfrddtRow = GisaDataSetHelper.GetInstance().SFRDDatasProducao.NewSFRDDatasProducaoRow();
            if (uf.anoInicio.Length > 0)
            {
                if (!DateHelper.IsValidYear(uf.anoInicio))
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_UNIDADES_FISICAS, uf.identificador, ImportExcel.UF_ANOINICIO, uf.anoInicio, ExceptionHelper.ERR_VALOR_INVALIDO);
                sfrddtRow.InicioAno = uf.anoInicio;
            }

            if (uf.mesInicio.Length > 0)
            {
                if (!DateHelper.IsValidMonth(uf.mesInicio))
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_UNIDADES_FISICAS, uf.identificador, ImportExcel.UF_MESINICIO, uf.mesInicio, ExceptionHelper.ERR_VALOR_INVALIDO);
                sfrddtRow.InicioMes = uf.mesInicio;
            }

            if (uf.diaInicio.Length > 0)
            {
                if (!DateHelper.IsValidDay(uf.diaInicio))
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_UNIDADES_FISICAS, uf.identificador, ImportExcel.UF_DIAINICIO, uf.diaInicio, ExceptionHelper.ERR_VALOR_INVALIDO);
                sfrddtRow.InicioDia = uf.diaInicio;
            }

            if (uf.atribuidaInicio.Length > 0)
            {
                if (!isValidBool(uf.atribuidaInicio))
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_UNIDADES_FISICAS, uf.identificador, ImportExcel.UF_ATRIBUIDAINICIO, uf.atribuidaInicio, ExceptionHelper.ERR_VALOR_INVALIDO);
                sfrddtRow.InicioAtribuida = System.Convert.ToBoolean(System.Convert.ToInt32(uf.atribuidaInicio));
            }
            else
                sfrddtRow.InicioAtribuida = false;

            if (uf.anoFim.Length > 0)
            {
                if (!DateHelper.IsValidYear(uf.anoFim))
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_UNIDADES_FISICAS, uf.identificador, ImportExcel.UF_ANOFIM, uf.anoFim, ExceptionHelper.ERR_VALOR_INVALIDO);
                sfrddtRow.FimAno = uf.anoFim;
            }

            if (uf.mesFim.Length > 0)
            {
                if (!DateHelper.IsValidMonth(uf.mesFim))
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_UNIDADES_FISICAS, uf.identificador, ImportExcel.UF_MESFIM, uf.mesFim, ExceptionHelper.ERR_VALOR_INVALIDO);
                sfrddtRow.FimMes = uf.mesFim;
            }

            if (uf.diaFim.Length > 0)
            {
                if (!DateHelper.IsValidDay(uf.diaFim))
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_UNIDADES_FISICAS, uf.identificador, ImportExcel.UF_DIAFIM, uf.diaFim, ExceptionHelper.ERR_VALOR_INVALIDO);
                sfrddtRow.FimDia = uf.diaFim;
            }

            if (uf.atribuidaFim.Length > 0)
            {
                if (!isValidBool(uf.atribuidaFim))
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_UNIDADES_FISICAS, uf.identificador, ImportExcel.UF_ATRIBUIDAFIM, uf.atribuidaFim, ExceptionHelper.ERR_VALOR_INVALIDO);
                sfrddtRow.FimAtribuida = System.Convert.ToBoolean(System.Convert.ToInt32(uf.atribuidaFim));
            }
            else
                sfrddtRow.FimAtribuida = false;

            sfrddtRow.InicioAtribuida = false;
            sfrddtRow.FimAtribuida = false;
            sfrddtRow.InicioTexto = "";
            sfrddtRow.FimTexto = "";
            sfrddtRow.isDeleted = 0;
            sfrddtRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDDatasProducao.AddSFRDDatasProducaoRow(sfrddtRow);
            return sfrddtRow;
        }

        private static GISADataset.SFRDDimensaoSuporteRow createSFRDDimensaoSuporteRow(UnidadeInformacional ui)
        {
            var sfrddsRow = GisaDataSetHelper.GetInstance().SFRDDimensaoSuporte.NewSFRDDimensaoSuporteRow();
            sfrddsRow.Nota = ui.dimensaoUnidadeInformacional;
            sfrddsRow.isDeleted = 0;
            sfrddsRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDDimensaoSuporte.AddSFRDDimensaoSuporteRow(sfrddsRow);
            return sfrddsRow;
        }

        private static GISADataset.SFRDNotaGeralRow createSFRDNotaGeralRow(UnidadeInformacional ui)
        {
            var sfrdngRow = GisaDataSetHelper.GetInstance().SFRDNotaGeral.NewSFRDNotaGeralRow();
            sfrdngRow.NotaGeral = ui.notas;
            sfrdngRow.isDeleted = 0;
            sfrdngRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDNotaGeral.AddSFRDNotaGeralRow(sfrdngRow);
            return sfrdngRow;
        }

        private static GISADataset.SFRDContextoRow createSFRDContextoRow(UnidadeInformacional ui)
        {
            var sfrdcRow = GisaDataSetHelper.GetInstance().SFRDContexto.NewSFRDContextoRow();
            sfrdcRow.HistoriaAdministrativa = ui.historiaAdministrativa;
            sfrdcRow.HistoriaCustodial = ui.historiaArquivistica;
            sfrdcRow.FonteImediataDeAquisicao = ui.fonteAquisicaoOuTransferencia;
            sfrdcRow.SerieAberta = true;
            sfrdcRow.isDeleted = 0;
            sfrdcRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDContexto.AddSFRDContextoRow(sfrdcRow);
            return sfrdcRow;
        }

        private static string[] destinoFinal = new string[] { "Conservação", "Eliminação" };
        private static GISADataset.SFRDAvaliacaoRow createSFRDAvaliacaoRow(UnidadeInformacional ui)
        {
            var sfrdaRow = GisaDataSetHelper.GetInstance().SFRDAvaliacao.NewSFRDAvaliacaoRow();

            if (ui.destinoFinal.Length > 0)
            {
                if (!destinoFinal.Contains(ui.destinoFinal))
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_DESTINOFINAL, ui.destinoFinal, ExceptionHelper.ERR_VALOR_INVALIDO);
                else
                    sfrdaRow.Preservar = ui.destinoFinal.Equals("Conservação") ? true : false;
            }
            else
                sfrdaRow["Preservar"] = DBNull.Value;

            if (ui.publicacao.Length > 0)
            {
                if (!isValidBool(ui.publicacao))
                    ExceptionHelper.ThrowException(ExceptionHelper.TAB_DOCUMENTOS, ui.identificador, ImportExcel.UI_PUBLICACAO, ui.publicacao, ExceptionHelper.ERR_VALOR_INVALIDO);
                sfrdaRow.Publicar = System.Convert.ToBoolean(System.Convert.ToInt32(ui.publicacao));
            }
            else
                sfrdaRow.Publicar = false;

            sfrdaRow.IDPertinencia = 1;
            sfrdaRow.IDDensidade = 1;
            sfrdaRow.IDSubdensidade = 1;
            sfrdaRow.Observacoes = "";
            sfrdaRow.AvaliacaoTabela = false;
            sfrdaRow.isDeleted = 0;
            sfrdaRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDAvaliacao.AddSFRDAvaliacaoRow(sfrdaRow);
            return sfrdaRow;
        }

        private static GISADataset.SFRDTradicaoDocumentalRow createSFRDTradicaoDocumentalRow()
        {
            var sfrdttdRow = GisaDataSetHelper.GetInstance().SFRDTradicaoDocumental.NewSFRDTradicaoDocumentalRow();
            sfrdttdRow.isDeleted = 0;
            sfrdttdRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDTradicaoDocumental.AddSFRDTradicaoDocumentalRow(sfrdttdRow);
            return sfrdttdRow;
        }

        private static GISADataset.SFRDOrdenacaoRow createSFRDOrdenacaoRow()
        {
            var sfrdordRow = GisaDataSetHelper.GetInstance().SFRDOrdenacao.NewSFRDOrdenacaoRow();
            sfrdordRow.isDeleted = 0;
            sfrdordRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDOrdenacao.AddSFRDOrdenacaoRow(sfrdordRow);
            return sfrdordRow;
        }

        private static GISADataset.SFRDCondicaoDeAcessoRow createSFRDCondicaoDeAcessoRow(UnidadeInformacional ui)
        {
            var sfrdcaRow = GisaDataSetHelper.GetInstance().SFRDCondicaoDeAcesso.NewSFRDCondicaoDeAcessoRow();
            sfrdcaRow.CondicaoDeAcesso = ui.condicoesAcesso;
            sfrdcaRow.CondicaoDeReproducao = ui.condicoesReproducao;
            sfrdcaRow.AuxiliarDePesquisa = ui.instrumentosPesquisa;
            sfrdcaRow.EstatutoLegal = "";
            sfrdcaRow.isDeleted = 0;
            sfrdcaRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDCondicaoDeAcesso.AddSFRDCondicaoDeAcessoRow(sfrdcaRow);
            return sfrdcaRow;
        }

        private static GISADataset.SFRDLinguaRow createSFRDLinguaRow()
        {
            var sfrdlRow = GisaDataSetHelper.GetInstance().SFRDLingua.NewSFRDLinguaRow();
            sfrdlRow.isDeleted = 0;
            sfrdlRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDLingua.AddSFRDLinguaRow(sfrdlRow);
            return sfrdlRow;
        }

        private static GISADataset.SFRDAlfabetoRow createSFRDAlfabetoRow()
        {
            var sfrdaRow = GisaDataSetHelper.GetInstance().SFRDAlfabeto.NewSFRDAlfabetoRow();
            sfrdaRow.isDeleted = 0;
            sfrdaRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDAlfabeto.AddSFRDAlfabetoRow(sfrdaRow);
            return sfrdaRow;
        }

        private static GISADataset.SFRDFormaSuporteAcondRow createSFRDFormaSuporteAcondRow()
        {
            var sfrdtfsaRow = GisaDataSetHelper.GetInstance().SFRDFormaSuporteAcond.NewSFRDFormaSuporteAcondRow();
            sfrdtfsaRow.isDeleted = 0;
            sfrdtfsaRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDFormaSuporteAcond.AddSFRDFormaSuporteAcondRow(sfrdtfsaRow);
            return sfrdtfsaRow;
        }

        private static GISADataset.SFRDMaterialDeSuporteRow createSFRDMaterialDeSuporteRow()
        {
            var sfrdtmsRow = GisaDataSetHelper.GetInstance().SFRDMaterialDeSuporte.NewSFRDMaterialDeSuporteRow();
            sfrdtmsRow.isDeleted = 0;
            sfrdtmsRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDMaterialDeSuporte.AddSFRDMaterialDeSuporteRow(sfrdtmsRow);
            return sfrdtmsRow;
        }

        private static GISADataset.SFRDTecnicasDeRegistoRow createSFRDTecnicasDeRegistoRow()
        {
            var sfrdtrRow = GisaDataSetHelper.GetInstance().SFRDTecnicasDeRegisto.NewSFRDTecnicasDeRegistoRow();
            sfrdtrRow.isDeleted = 0;
            sfrdtrRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDTecnicasDeRegisto.AddSFRDTecnicasDeRegistoRow(sfrdtrRow);
            return sfrdtrRow;
        }

        private static GISADataset.SFRDEstadoDeConservacaoRow createSFRDEstadoDeConservacaoRow()
        {
            var sfrdecRow = GisaDataSetHelper.GetInstance().SFRDEstadoDeConservacao.NewSFRDEstadoDeConservacaoRow();
            sfrdecRow.isDeleted = 0;
            sfrdecRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDEstadoDeConservacao.AddSFRDEstadoDeConservacaoRow(sfrdecRow);
            return sfrdecRow;
        }

        private static GISADataset.SFRDDocumentacaoAssociadaRow createSFRDDocumentacaoAssociadaRow(UnidadeInformacional ui)
        {
            var sfrddaRow = GisaDataSetHelper.GetInstance().SFRDDocumentacaoAssociada.NewSFRDDocumentacaoAssociadaRow();
            sfrddaRow.ExistenciaDeOriginais = ui.existenciaOriginais;
            sfrddaRow.ExistenciaDeCopias = ui.existenciaCopias;
            sfrddaRow.NotaDePublicacao = ui.notaPublicacao;
            sfrddaRow.UnidadesRelacionadas = ui.unidadesDescricaoRelacionadas;
            sfrddaRow.isDeleted = 0;
            sfrddaRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDDocumentacaoAssociada.AddSFRDDocumentacaoAssociadaRow(sfrddaRow);
            return sfrddaRow;
        }

        private static void createRelacaoHierarquicaRow(GISADataset.NivelRow nivelRow, GISADataset.NivelRow nivelUpperRow, long idTipoNivelRelacionado)
        {
            var rhRow = GisaDataSetHelper.GetInstance().RelacaoHierarquica.NewRelacaoHierarquicaRow();
            rhRow.NivelRowByNivelRelacaoHierarquica = nivelRow;
            rhRow.NivelRowByNivelRelacaoHierarquicaUpper = nivelUpperRow;
            rhRow.IDTipoNivelRelacionado = idTipoNivelRelacionado;
            rhRow.isDeleted = 0;
            rhRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().RelacaoHierarquica.AddRelacaoHierarquicaRow(rhRow);
        }

        private static GISADataset.SFRDUnidadeFisicaRow createSFRDUnidadeFisicaRow(GISADataset.FRDBaseRow frdRow, GISADataset.NivelRow ufRow)
        {
            var sfrdufRow = GisaDataSetHelper.GetInstance().SFRDUnidadeFisica.NewSFRDUnidadeFisicaRow();
            sfrdufRow.FRDBaseRow = frdRow;
            sfrdufRow.NivelRow = ufRow;
            sfrdufRow.isDeleted = 0;
            sfrdufRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDUnidadeFisica.AddSFRDUnidadeFisicaRow(sfrdufRow);
            return sfrdufRow;
        }

        private static void createSFRDAutorRow(GISADataset.FRDBaseRow frdRow, GISADataset.ControloAutRow caRow)
        {
            var sfrdaRow = GisaDataSetHelper.GetInstance().SFRDAutor.NewSFRDAutorRow();
            sfrdaRow.ControloAutRow = caRow;
            sfrdaRow.FRDBaseRow = frdRow;
            sfrdaRow.isDeleted = 0;
            sfrdaRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().SFRDAutor.AddSFRDAutorRow(sfrdaRow);
        }

        private static void createIndexFRDCARow(GISADataset.FRDBaseRow frdRow, GISADataset.ControloAutRow caRow, Int32? selector)
        {
            var idxRow = GisaDataSetHelper.GetInstance().IndexFRDCA.NewIndexFRDCARow();
            idxRow.ControloAutRow = caRow;
            idxRow.FRDBaseRow = frdRow;
            if (selector == null)
                idxRow["Selector"] = DBNull.Value;
            else
                idxRow.Selector = (int)selector;
            idxRow.isDeleted = 0;
            idxRow.Versao = new byte[] { };
            GisaDataSetHelper.GetInstance().IndexFRDCA.AddIndexFRDCARow(idxRow);
        }

        private static string[] tipoDocumento = new string[] { "Simples", "Composto" };
        //private static void createTipoDocumentoRow(UnidadeInformacional ui, GISADataset.NivelRow nRow, long tnrID)
        //{   
        //    if (tnrID == TipoNivelRelacionado.SD)
        //    {
        //        createNivelDocumentoSimples(ui, nRow);
        //    }
        //}

        //private static void createNivelDocumentoSimples(UnidadeInformacional ui, GISADataset.NivelRow nRow)
        //{
        //    GisaDataSetHelper.GetInstance().NivelDocumentoSimples.AddNivelDocumentoSimplesRow(nRow.GetNivelDesignadoRows().Single(), -1, new byte[] { }, 0);
        //}

        private static bool isValidBool(string txt)
        {
            System.Text.RegularExpressions.Regex exp = new System.Text.RegularExpressions.Regex("^[0-1?]$");
            return exp.IsMatch(txt);
        }
    }
}
