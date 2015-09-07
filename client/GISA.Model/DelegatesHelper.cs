using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Model
{
    public class DelegatesHelper
    {
        #region MasterPanelSeries
        public static void ValidateNivelAddAndAssocNewUF(PersistencyHelper.PreConcArguments args)
        {
            PersistencyHelper.ValidateNivelAddAndAssocNewUFPreConcArguments pcArgs = null;
            pcArgs = (PersistencyHelper.ValidateNivelAddAndAssocNewUFPreConcArguments)args;

            bool addSuccessful = false;

            pcArgs.argsNivel.tran = pcArgs.tran;
            pcArgs.argsNivel.gisaBackup = pcArgs.gisaBackup;

            var frdID = long.MinValue;
            if (pcArgs.IDTipoNivelRelacionado == TipoNivelRelacionado.SR || pcArgs.IDTipoNivelRelacionado == TipoNivelRelacionado.SSR)
            {
                var argsNivel = pcArgs.argsNivel as PersistencyHelper.VerifyIfRHNivelUpperExistsPreConcArguments;
                EnsureNivelUpperExists(pcArgs.argsNivel);
                pcArgs.message = argsNivel.message;
                addSuccessful = ((PersistencyHelper.VerifyIfRHNivelUpperExistsPreConcArguments)pcArgs.argsNivel).RHNivelUpperExists;
                frdID = argsNivel.frdBaseID;
            }
            else if (pcArgs.IDTipoNivelRelacionado == TipoNivelRelacionado.D || pcArgs.IDTipoNivelRelacionado == TipoNivelRelacionado.SD)
            {
                var argsNivel = pcArgs.argsNivel as PersistencyHelper.EnsureUniqueCodigoNivelPreConcArguments;
                ensureUniqueCodigo(pcArgs.argsNivel);
                pcArgs.message = argsNivel.message;
                addSuccessful = ((PersistencyHelper.EnsureUniqueCodigoNivelPreConcArguments)pcArgs.argsNivel).successful;
                frdID = argsNivel.frdBaseID;
            }
            
            if (addSuccessful)
            {
                GISADataset.FRDBaseRow frdNivelDocRow = (GISADataset.FRDBaseRow)(GisaDataSetHelper.GetInstance().FRDBase.Select("ID=" + frdID.ToString())[0]);
                var sfrdDatasProducaoRow = GisaDataSetHelper.GetInstance().SFRDDatasProducao.Cast<GISADataset.SFRDDatasProducaoRow>().Where(r => r.IDFRDBase == frdNivelDocRow.ID).SingleOrDefault();
                if (sfrdDatasProducaoRow == null)
                    GisaDataSetHelper.GetInstance().SFRDDatasProducao.AddSFRDDatasProducaoRow(frdNivelDocRow, "", "", "", "", false, "", "", "", "", false, new byte[] { }, 0);
                var sfrdConteudoEEstruturaRow = GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.Cast<GISADataset.SFRDConteudoEEstruturaRow>().Where(r => r.IDFRDBase == frdNivelDocRow.ID).SingleOrDefault();
                if (sfrdConteudoEEstruturaRow == null)
                    GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.AddSFRDConteudoEEstruturaRow(frdNivelDocRow, "", "", new byte[] { }, 0);
                var sfrdContextoRow = GisaDataSetHelper.GetInstance().SFRDContexto.Cast<GISADataset.SFRDContextoRow>().Where(r => r.IDFRDBase == frdNivelDocRow.ID).SingleOrDefault();
                if (sfrdContextoRow == null)
                    GisaDataSetHelper.GetInstance().SFRDContexto.AddSFRDContextoRow(frdNivelDocRow, "", "", "", false, new byte[] { }, 0);
                var sfrdDocumentacaoAssociadaRow = GisaDataSetHelper.GetInstance().SFRDDocumentacaoAssociada.Cast<GISADataset.SFRDDocumentacaoAssociadaRow>().Where(r => r.IDFRDBase == frdNivelDocRow.ID).SingleOrDefault();
                if (sfrdDocumentacaoAssociadaRow == null)
                    GisaDataSetHelper.GetInstance().SFRDDocumentacaoAssociada.AddSFRDDocumentacaoAssociadaRow(frdNivelDocRow, "", "", "", "", new byte[] { }, 0);
                var sfrdDimensaoSuporteRow = GisaDataSetHelper.GetInstance().SFRDDimensaoSuporte.Cast<GISADataset.SFRDDimensaoSuporteRow>().Where(r => r.IDFRDBase == frdNivelDocRow.ID).SingleOrDefault();
                if (sfrdDimensaoSuporteRow == null)
                    GisaDataSetHelper.GetInstance().SFRDDimensaoSuporte.AddSFRDDimensaoSuporteRow(frdNivelDocRow, "", new byte[] { }, 0);
                var sfrdNotaGeralRow = GisaDataSetHelper.GetInstance().SFRDNotaGeral.Cast<GISADataset.SFRDNotaGeralRow>().Where(r => r.IDFRDBase == frdNivelDocRow.ID).SingleOrDefault();
                if (sfrdNotaGeralRow == null)
                    GisaDataSetHelper.GetInstance().SFRDNotaGeral.AddSFRDNotaGeralRow(frdNivelDocRow, "", new byte[] { }, 0);
                var sfrdAgrupadorRow = GisaDataSetHelper.GetInstance().SFRDAgrupador.Cast<GISADataset.SFRDAgrupadorRow>().Where(r => r.IDFRDBase == frdNivelDocRow.ID).SingleOrDefault();
                if (sfrdAgrupadorRow == null)
                    GisaDataSetHelper.GetInstance().SFRDAgrupador.AddSFRDAgrupadorRow(frdNivelDocRow, "", new byte[] { }, 0);
                var sfrdAvaliacaoRow = GisaDataSetHelper.GetInstance().SFRDAvaliacao.Cast<GISADataset.SFRDAvaliacaoRow>().Where(r => r.IDFRDBase == frdNivelDocRow.ID).SingleOrDefault();
                if (sfrdAvaliacaoRow == null)
                {
                    var CurrentSFRDAvaliacao = GisaDataSetHelper.GetInstance().SFRDAvaliacao.NewSFRDAvaliacaoRow();
                    CurrentSFRDAvaliacao.FRDBaseRow = frdNivelDocRow;
                    CurrentSFRDAvaliacao.IDPertinencia = 1;
                    CurrentSFRDAvaliacao.IDDensidade = 1;
                    CurrentSFRDAvaliacao.IDSubdensidade = 1;
                    CurrentSFRDAvaliacao.Publicar = false;
                    CurrentSFRDAvaliacao.Observacoes = "";
                    CurrentSFRDAvaliacao.AvaliacaoTabela = false;
                    GisaDataSetHelper.GetInstance().SFRDAvaliacao.AddSFRDAvaliacaoRow(CurrentSFRDAvaliacao);
                }
                var sfrdCondicaoDeAcessoRow = GisaDataSetHelper.GetInstance().SFRDCondicaoDeAcesso.Cast<GISADataset.SFRDCondicaoDeAcessoRow>().Where(r => r.IDFRDBase == frdNivelDocRow.ID).SingleOrDefault();
                if (sfrdCondicaoDeAcessoRow == null)
                    GisaDataSetHelper.GetInstance().SFRDCondicaoDeAcesso.AddSFRDCondicaoDeAcessoRow(frdNivelDocRow, "", "", "", "", new byte[] { }, 0);
            }

            if (addSuccessful && pcArgs.addNewUF)
            {
                GISADataset.FRDBaseRow frdNivelDocRow = (GISADataset.FRDBaseRow)(GisaDataSetHelper.GetInstance().FRDBase.Select("ID=" + pcArgs.IDFRDBaseNivelDoc.ToString())[0]);
                GISADataset.NivelRow nivelEDRow = NiveisHelper.GetNivelED(pcArgs.produtor);
                GISADataset.NivelRow nivelUFRow = UnidadesFisicasHelper.CreateUF(nivelEDRow, pcArgs.designacaoUFAssociada);

                PersistencyHelper.AddEditUFPreConcArguments argsPCNewUF = (PersistencyHelper.AddEditUFPreConcArguments)pcArgs.argsUF;
                PersistencyHelper.IsCodigoUFBeingUsedPreSaveArguments argsPSNewUF = (PersistencyHelper.IsCodigoUFBeingUsedPreSaveArguments)argsPCNewUF.psa;

                argsPCNewUF.nivelUFRowID = nivelUFRow.ID;
                argsPCNewUF.ndufRowID = nivelUFRow.ID;
                argsPCNewUF.rhufRowID = nivelUFRow.ID;
                argsPCNewUF.rhufRowIDUpper = nivelEDRow.ID;
                argsPCNewUF.nufufRowID = nivelUFRow.ID;
                argsPCNewUF.tran = pcArgs.tran;

                argsPSNewUF.nivelUFRowID = nivelUFRow.ID;

                HandleUF(argsPCNewUF);

                pcArgs.message = argsPCNewUF.message;

                if (argsPCNewUF.OperationError == PersistencyHelper.AddEditUFPreConcArguments.OperationErrors.NoError)
                    GisaDataSetHelper.GetInstance().SFRDUnidadeFisica.AddSFRDUnidadeFisicaRow(frdNivelDocRow, nivelUFRow, null, new byte[] { }, 0);
            }
            else if (!addSuccessful && pcArgs.addNewUF)
            {
                // caso onde o nível não foi criado e pretendia-se criar unidade física; neste caso cancela-se a atribuição do código
                // à unidade física
                PersistencyHelper.AddEditUFPreConcArguments argsPCNewUF = (PersistencyHelper.AddEditUFPreConcArguments)pcArgs.argsUF;
                PersistencyHelper.IsCodigoUFBeingUsedPreSaveArguments argsPSNewUF = (PersistencyHelper.IsCodigoUFBeingUsedPreSaveArguments)argsPCNewUF.psa;
                argsPSNewUF.cancelSetNewCodigo = true;
            }

            pcArgs.continueSave = addSuccessful;
        }

        public static void SetNewCodigos(PersistencyHelper.PreSaveArguments args)
        {
            PersistencyHelper.SetNewCodigosPreSaveArguments sncPsa = null;
            sncPsa = (PersistencyHelper.SetNewCodigosPreSaveArguments)args;

            if (sncPsa.createNewNivelCodigo)
            {
                sncPsa.argsNivel.tran = sncPsa.tran;
                createNewCodigoSerie(sncPsa.argsNivel);
            }

            if (sncPsa.createNewUFCodigo)
            {
                sncPsa.argsUF.tran = sncPsa.tran;
                SetCodigo(sncPsa.argsUF);
            }

            if (sncPsa.argsNivelDocSimples != null)
            {
                sncPsa.argsNivelDocSimples.tran = sncPsa.tran;
                SetOrdemDocSimples(sncPsa.argsNivelDocSimples.nRowID, sncPsa.argsNivelDocSimples.nRowIDUpper, sncPsa.argsNivelDocSimples.tran);
            }
        }

        public static void EnsureNivelUpperExists(PersistencyHelper.PreConcArguments args)
        {
            PersistencyHelper.VerifyIfRHNivelUpperExistsPreConcArguments pcArgs = null;
            pcArgs = (PersistencyHelper.VerifyIfRHNivelUpperExistsPreConcArguments)args;

            GISADataset.NivelRow nRow = (GISADataset.NivelRow)(GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + pcArgs.nRowID.ToString())[0]);
            long rhRowIDUpper = nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].IDUpper;

            if (rhRowIDUpper < 0)
                pcArgs.RHNivelUpperExists = true;
            else
                // antes de obter o código verificar se adição de um novo nível ainda é possível (por razões de concorrência)
                pcArgs.RHNivelUpperExists = NivelRule.Current.isNivelDeleted(rhRowIDUpper, args.tran);

            if (!pcArgs.RHNivelUpperExists)
            {
                GISADataset.RelacaoHierarquicaRow rhRow = (GISADataset.RelacaoHierarquicaRow)(GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", pcArgs.rhRowID, pcArgs.rhRowIDUpper))[0]);
                GISADataset.NivelDesignadoRow ndRow = (GISADataset.NivelDesignadoRow)(GisaDataSetHelper.GetInstance().NivelDesignado.Select("ID=" + pcArgs.ndRowID.ToString())[0]);
                GISADataset.FRDBaseRow frdBaseRow = (GISADataset.FRDBaseRow)(GisaDataSetHelper.GetInstance().FRDBase.Select("ID=" + pcArgs.frdBaseID.ToString())[0]);

                System.Data.DataSet tempgisaBackup1 = pcArgs.gisaBackup;
                PersistencyHelper.BackupRow(ref tempgisaBackup1, rhRow);
                pcArgs.gisaBackup = tempgisaBackup1;
                rhRow.RejectChanges();
                System.Data.DataSet tempgisaBackup2 = pcArgs.gisaBackup;
                PersistencyHelper.BackupRow(ref tempgisaBackup2, ndRow);
                pcArgs.gisaBackup = tempgisaBackup2;
                ndRow.RejectChanges();
                System.Data.DataSet tempgisaBackup3 = pcArgs.gisaBackup;
                PersistencyHelper.BackupRow(ref tempgisaBackup3, nRow);
                pcArgs.gisaBackup = tempgisaBackup3;
                nRow.RejectChanges();
                System.Data.DataSet tempgisaBackup4 = pcArgs.gisaBackup;
                PersistencyHelper.BackupRow(ref tempgisaBackup4, frdBaseRow);
                pcArgs.gisaBackup = tempgisaBackup4;
                frdBaseRow.RejectChanges();
                pcArgs.message = "Não foi possível criar a unidade informacional uma" + Environment.NewLine + "vez que a unidade superior foi apagada por outro utilizador.";
            }
        }

        public static void createNewCodigoSerie(PersistencyHelper.PreSaveArguments args)
        {
            try
            {
                GISADataset.NivelRow nRow = null;
                nRow = (GISADataset.NivelRow)(GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + ((PersistencyHelper.FetchLastCodigoSeriePreSaveArguments)args).nRowID.ToString())[0]);
                // antes de obter o código verificar se adição de um novo nível ainda é possível (por razões de concorrência)
                bool RHNivelUpperExists = ((PersistencyHelper.VerifyIfRHNivelUpperExistsPreConcArguments)(((PersistencyHelper.FetchLastCodigoSeriePreSaveArguments)args).pcArgs)).RHNivelUpperExists;
                if (RHNivelUpperExists)
                {
                    NivelRule.Current.FillTipoNivelRelacionadoCodigo(GisaDataSetHelper.GetInstance(), args.tran);
                    nRow.Codigo = NiveisHelper.getNextSeriesCodigo(true);
                    // estamos dentro de um save e de uma transaccao por isso podemos fazer um update
                    DBAbstractDataLayer.DataAccessRules.PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().TipoNivelRelacionadoCodigo, GisaDataSetHelper.GetInstance().TipoNivelRelacionadoCodigo.Select(), args.tran);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
        }

        public static void SetOrdemDocSimples(long ID, long IDUpper, IDbTransaction tran)
        {
            try
            {
                var nextGUIOrder = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.GetDocNextGUIOrder(IDUpper, tran);
                var ndsRow = GisaDataSetHelper.GetInstance().NivelDocumentoSimples.Cast<GISADataset.NivelDocumentoSimplesRow>().Single(r => r.ID == ID);
                ndsRow.GUIOrder = nextGUIOrder;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
        }

        public static void verificaCodigosRepetidos(PersistencyHelper.PreConcArguments args)
        {
            var pcArgs = (PersistencyHelper.EnsureUniqueCodigoNivelPreConcArguments) args;
            var rhRow = GisaDataSetHelper.GetInstance().RelacaoHierarquica.Cast<GISADataset.RelacaoHierarquicaRow>().SingleOrDefault(r => r.RowState != DataRowState.Deleted && r.ID == pcArgs.rhRowID && r.IDUpper == pcArgs.rhRowIDUpper);

            if (rhRow != null)
            {
                var codigo = rhRow.NivelRowByNivelRelacaoHierarquica.Codigo;
                var ID = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.GetIDCodigoRepetido(codigo, rhRow.ID, pcArgs.tran, pcArgs.testOnlyWithinNivel, rhRow.NivelRowByNivelRelacaoHierarquicaUpper.ID);

                if (ID > long.MinValue)
                {
                    // erro
                    pcArgs.message = string.Format(
                        "Não foi possível completar a operação uma vez que" + Environment.NewLine +
                        "por debaixo da entidade produtora selecionada" + Environment.NewLine +
                        "o código parcial '{0}' já é utilizado pela unidade " + Environment.NewLine +
                        "informacional com o identificador {1}.", codigo, rhRow.ID);

                    var tempgisaBackup3 = pcArgs.gisaBackup;
                    PersistencyHelper.BackupRow(ref tempgisaBackup3, rhRow);
                    pcArgs.gisaBackup = tempgisaBackup3;
                    rhRow.RejectChanges();
                }
                else
                    pcArgs.successful = true;
            }
        }

        public static void ensureUniqueCodigo(PersistencyHelper.PreConcArguments args)
        {
            PersistencyHelper.EnsureUniqueCodigoNivelPreConcArguments pcArgs = null;
            pcArgs = (PersistencyHelper.EnsureUniqueCodigoNivelPreConcArguments)args;
            GISADataset.NivelRow nRow = (GISADataset.NivelRow)(GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + pcArgs.nRowID.ToString())[0]);
            GISADataset.NivelDesignadoRow ndRow = (GISADataset.NivelDesignadoRow)(GisaDataSetHelper.GetInstance().NivelDesignado.Select("ID=" + pcArgs.ndRowID.ToString())[0]);
            GISADataset.RelacaoHierarquicaRow rhRow = null;

            if (GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", pcArgs.rhRowID, pcArgs.rhRowIDUpper)).Length > 0)
            {
                rhRow = (GISADataset.RelacaoHierarquicaRow)(GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", pcArgs.rhRowID, pcArgs.rhRowIDUpper))[0]);
            }

            // Se o nível em questão for uma entidade detentora
            if (rhRow == null && nRow.TipoNivelRow.ID == TipoNivel.LOGICO)
            {
                if (DBAbstractDataLayer.DataAccessRules.NivelRule.Current.isUniqueCodigo(nRow.Codigo, nRow.ID, pcArgs.tran, pcArgs.testOnlyWithinNivel))
                {
                    pcArgs.successful = true;
                }
                else
                {
                    var cod = nRow.Codigo;
                    System.Data.DataSet tempgisaBackup1 = pcArgs.gisaBackup;
                    PersistencyHelper.BackupRow(ref tempgisaBackup1, ndRow);
                    pcArgs.gisaBackup = tempgisaBackup1;
                    System.Data.DataSet tempgisaBackup2 = pcArgs.gisaBackup;
                    PersistencyHelper.BackupRow(ref tempgisaBackup2, nRow);
                    pcArgs.gisaBackup = tempgisaBackup2;
                    ndRow.RejectChanges();
                    PermissoesHelper.UndoAddNivelGrantPermissions(nRow);
                    nRow.RejectChanges();
                    pcArgs.message = string.Format(
                        "Não é possível completar a operação porque não é permitido " + Environment.NewLine +
                        "que duas unidades de informação tenham o mesmo código " + Environment.NewLine +
                        "parcial ({0}) no mesmo nivel de descrição.", cod);
                }
            }
            else
            {
                // antes de obter o código verificar se adição de um novo nível ainda é possível (por razões
                // de concorrência é necessário garantir que tanto o nível acima mantém-se na base de dados 
                // até o save estar terminado)
                bool upperRelationExists;
                if (rhRow.IDUpper < 0)
                    upperRelationExists = true;
                else
                    upperRelationExists = NivelRule.Current.isNivelDeleted(rhRow.IDUpper, args.tran);

                if (!upperRelationExists)
                {
                    var frdRow = GisaDataSetHelper.GetInstance().FRDBase.Cast<GISADataset.FRDBaseRow>().Single(r => r.ID == pcArgs.frdBaseID);
                    Nivel.DeleteInDataSet(frdRow, false, pcArgs.gisaBackup);
                    Nivel.DeleteInDataSet(nRow, false, pcArgs.gisaBackup);

                    pcArgs.message = "Não foi possível criar/editar a unidade informacional uma" + Environment.NewLine + "vez que a unidade superior foi apagada por outro utilizador.";
                }
                else
                {
                    if (DBAbstractDataLayer.DataAccessRules.NivelRule.Current.isUniqueCodigo(nRow.Codigo, nRow.ID, pcArgs.tran, pcArgs.testOnlyWithinNivel, rhRow.NivelRowByNivelRelacaoHierarquicaUpper.ID))
                        pcArgs.successful = true;
                    else
                    {
                        var cod = nRow.Codigo;
                        if (nRow.RowState == DataRowState.Modified)
                        {
                            nRow.RejectChanges();
                            GisaDataSetHelper.GetInstance().IndexFRDCA.Cast<GISADataset.IndexFRDCARow>()
                                .Where(r => r.RowState != DataRowState.Unchanged).ToList().ForEach(r => r.RejectChanges());
                        }
                        else if (nRow.RowState == DataRowState.Added)
                        {
                            var frdRow = GisaDataSetHelper.GetInstance().FRDBase.Cast<GISADataset.FRDBaseRow>().Single(r => r.ID == pcArgs.frdBaseID);
                            Nivel.DeleteInDataSet(frdRow, false, pcArgs.gisaBackup);
                            Nivel.DeleteInDataSet(nRow, false, pcArgs.gisaBackup);
                        }
                        pcArgs.message = string.Format(
                            "Não é possível completar a operação porque não é permitido " + Environment.NewLine +
                            "que duas unidades de informação tenham o mesmo código " + Environment.NewLine +
                            "parcial ({0}) no mesmo nivel de descrição.", cod);
                    }
                }
            }
        }

        // utilizado no contexto das eliminações
        public static void verifyIfCanDeleteRH(PersistencyHelper.PreConcArguments args)
        {
            PersistencyHelper.canDeleteRHRowPreConcArguments cdrhPca = null;
            cdrhPca = (PersistencyHelper.canDeleteRHRowPreConcArguments)args;

            GISADataset.NivelRow nRow = (GISADataset.NivelRow)(GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + cdrhPca.nRowID.ToString())[0]);
            GISADataset.NivelRow nUpperRow = null;
            if (GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + cdrhPca.nUpperRowID.ToString()).Length > 0)
            {
                nUpperRow = (GISADataset.NivelRow)(GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + cdrhPca.nUpperRowID.ToString())[0]);
            }
            GISADataset.RelacaoHierarquicaRow rhRow = null;
            if (GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", cdrhPca.rhRowID, cdrhPca.rhRowIDUpper)).Length > 0)
            {
                rhRow = (GISADataset.RelacaoHierarquicaRow)(GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", cdrhPca.rhRowID, cdrhPca.rhRowIDUpper))[0]);
            }

            // Permitir apenas a eliminação de folhas e de níveis cuja 
            // a funcionalidade eliminação não elimina o nível propriamente 
            // dito mas sim a sua relação com o nível superior
            int parentCount = 0;
            int directChildCount = 0;
            bool moreThenOneParent = false;
            bool notExistsDirectChild = false;

            parentCount = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.getParentCount(cdrhPca.nRowID.ToString(), cdrhPca.tran);
            directChildCount = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.getDirectChildCount(cdrhPca.nRowID.ToString(), string.Empty, cdrhPca.tran);
            moreThenOneParent = parentCount > 1;
            notExistsDirectChild = directChildCount == 0;

            Trace.WriteLine("parentCount: " + parentCount.ToString());
            Trace.WriteLine("directChildCount: " + directChildCount.ToString());

            if (!(!(TipoNivel.isNivelOrganico(nRow) && TipoNivel.isNivelOrganico(nUpperRow)) && (TipoNivel.isNivelOrganico(nRow) || (TipoNivel.isNivelOrganico(nUpperRow) && moreThenOneParent) || (notExistsDirectChild))))
            {
                string filter = string.Format("rh.IDTipoNivelRelacionado != {0:d}", TipoNivelRelacionado.UF);
                parentCount = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.getParentCount(cdrhPca.nRowID.ToString(), cdrhPca.tran);
                directChildCount = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.getDirectChildCount(cdrhPca.nRowID.ToString(), filter, cdrhPca.tran);
                moreThenOneParent = parentCount > 1;
                notExistsDirectChild = directChildCount == 0;

                Trace.WriteLine("parentCount: " + parentCount.ToString());
                Trace.WriteLine("directChildCount: " + directChildCount.ToString());

                cdrhPca.deleteSuccessful = false;
                cdrhPca.continueSave = false;

                if (!(!(TipoNivel.isNivelOrganico(nRow) && TipoNivel.isNivelOrganico(nUpperRow)) && (TipoNivel.isNivelOrganico(nRow) || (TipoNivel.isNivelOrganico(nUpperRow) && moreThenOneParent) || (notExistsDirectChild))))
                    cdrhPca.message = "Só é possível eliminar os níveis que não tenham outros níveis directamente associados";
                else
                    cdrhPca.message = "Existem unidades físicas associadas a este nível não podendo por isso ser eliminado.";
            }
            else
            {
                //ToDo: simplificar este IF: o facto de rhrow ser nothing indica o caso onde é necessario executar o 
                // o método DeleteInDataSet
                if (rhRow != null)
                {
                    System.Data.DataSet tempgisaBackup1 = cdrhPca.gisaBackup;
                    PersistencyHelper.BackupRow(ref tempgisaBackup1, rhRow);
                    cdrhPca.gisaBackup = tempgisaBackup1;
                    rhRow.Delete();
                }
                else
                    Nivel.DeleteInDataSet(nRow, false, cdrhPca.gisaBackup); // é possível que esta linha não seja já precisa uma vez que o cleandeleteddata seguinte irá limpar do DS de trabalho as linhas que já não existam
            }
        }
        #endregion

        #region MasterPanelUnidadesFisicas
        public static void HandleUF(PersistencyHelper.PreConcArguments args)
        {
            PersistencyHelper.AddEditUFPreConcArguments aeufpca = null;
            PersistencyHelper.IsCodigoUFBeingUsedPreSaveArguments psa = null;
            aeufpca = (PersistencyHelper.AddEditUFPreConcArguments)args;
            psa = (PersistencyHelper.IsCodigoUFBeingUsedPreSaveArguments)aeufpca.psa;
            psa.cancelSetNewCodigo = false;
            aeufpca.message = string.Empty;

            GISADataset.RelacaoHierarquicaRow rhufRow = null;
            if (GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", aeufpca.rhufRowID, aeufpca.rhufRowIDUpper)).Length > 0)
            {
                rhufRow = (GISADataset.RelacaoHierarquicaRow)(GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", aeufpca.rhufRowID, aeufpca.rhufRowIDUpper))[0]);
            }
            else if (GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", aeufpca.rhufRowID, aeufpca.rhufRowIDUpper), "", DataViewRowState.Deleted).Length > 0)
            {
                rhufRow = (GISADataset.RelacaoHierarquicaRow)(GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", aeufpca.rhufRowID, aeufpca.rhufRowIDUpper), "", DataViewRowState.Deleted)[0]);
            }

            GISADataset.NivelDesignadoRow ndufRow = (GISADataset.NivelDesignadoRow)(GisaDataSetHelper.GetInstance().NivelDesignado.Select("ID=" + aeufpca.ndufRowID.ToString())[0]);
            GISADataset.NivelRow nivelUFRow = (GISADataset.NivelRow)(GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + aeufpca.nivelUFRowID.ToString())[0]);

            if (aeufpca.Operation == PersistencyHelper.AddEditUFPreConcArguments.Operations.Create || aeufpca.Operation == PersistencyHelper.AddEditUFPreConcArguments.Operations.CreateLike)
            {

                GISADataset.NivelUnidadeFisicaRow nufufRow = (GISADataset.NivelUnidadeFisicaRow)(GisaDataSetHelper.GetInstance().NivelUnidadeFisica.Select("ID=" + aeufpca.nufufRowID.ToString())[0]);
                // validar criação de UF
                if (DBAbstractDataLayer.DataAccessRules.UFRule.Current.isNivelRowDeleted(rhufRow.NivelRowByNivelRelacaoHierarquicaUpper.ID, aeufpca.tran))
                {
                    var tempgisaBackup = aeufpca.gisaBackup;
                    PersistencyHelper.BackupRow(ref tempgisaBackup, nufufRow);
                    aeufpca.gisaBackup = tempgisaBackup;
                    nufufRow.RejectChanges();

                    PersistencyHelper.BackupRow(ref tempgisaBackup, rhufRow);
                    aeufpca.gisaBackup = tempgisaBackup;
                    rhufRow.RejectChanges();

                    PersistencyHelper.BackupRow(ref tempgisaBackup, ndufRow);
                    aeufpca.gisaBackup = tempgisaBackup;
                    ndufRow.RejectChanges();

                    PersistencyHelper.BackupRow(ref tempgisaBackup, nivelUFRow);
                    aeufpca.gisaBackup = tempgisaBackup;
                    nivelUFRow.RejectChanges();

                    List<DataRow> lstRows = new List<DataRow>();
                    // recolher rows para fazer backup caso a operação seja "CREATE LIKE"
                    if (aeufpca.Operation == PersistencyHelper.AddEditUFPreConcArguments.Operations.CreateLike)
                    {
                        lstRows.AddRange(GisaDataSetHelper.GetInstance().SFRDDatasProducao.Select("IDFRDBase=" + aeufpca.frdufRowID.ToString()));
                        lstRows.AddRange(GisaDataSetHelper.GetInstance().SFRDUFCota.Select("IDFRDBase=" + aeufpca.frdufRowID.ToString()));
                        lstRows.AddRange(GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.Select("IDFRDBase=" + aeufpca.frdufRowID.ToString()));
                        lstRows.AddRange(GisaDataSetHelper.GetInstance().SFRDUFDescricaoFisica.Select("IDFRDBase=" + aeufpca.frdufRowID.ToString()));

                        foreach (long uaAssociadaID in aeufpca.uaAssociadas)
                        {
                            lstRows.AddRange(GisaDataSetHelper.GetInstance().SFRDUnidadeFisica.Select(string.Format("IDFRDBase={0} AND IDNivel={1}", uaAssociadaID, aeufpca.nivelUFRowID)));
                        }
                    }
                    lstRows.AddRange(GisaDataSetHelper.GetInstance().FRDBase.Select("ID=" + aeufpca.frdufRowID.ToString()));

                    PersistencyHelper.BackupRows(ref tempgisaBackup, lstRows);
                    aeufpca.gisaBackup = tempgisaBackup;

                    aeufpca.OperationError = PersistencyHelper.AddEditUFPreConcArguments.OperationErrors.NewUF;
                    aeufpca.message = "A entidade produtora que pretende associar à unidade física foi eliminada por outro utilizador. Esta operação não poderá, por isso, ser concluída.";
                    psa.cancelSetNewCodigo = true;
                    aeufpca.continueSave = false;
                }
            }
            else
            {
                // validar edição de UF
                bool isUFDeleted = DBAbstractDataLayer.DataAccessRules.UFRule.Current.isNivelRowDeleted(nivelUFRow.ID, aeufpca.tran);
                // verificar se a UF a editar não foi apagada por outro utilizador
                if (isUFDeleted)
                {
                    System.Data.DataSet tempgisaBackup6 = aeufpca.gisaBackup;
                    PersistencyHelper.BackupRow(ref tempgisaBackup6, ndufRow);
                    aeufpca.gisaBackup = tempgisaBackup6;
                    ndufRow.RejectChanges();
                    System.Data.DataSet tempgisaBackup7 = aeufpca.gisaBackup;
                    PersistencyHelper.BackupRow(ref tempgisaBackup7, nivelUFRow);
                    aeufpca.gisaBackup = tempgisaBackup7;
                    nivelUFRow.RejectChanges();
                    aeufpca.OperationError = PersistencyHelper.AddEditUFPreConcArguments.OperationErrors.EditEDAndDesignacao;
                    aeufpca.message = "A unidade física em edição foi eliminada por outro utilizador. " + Environment.NewLine + "Esta operação não poderá, por isso, ser concluída.";
                    psa.cancelSetNewCodigo = true;
                    aeufpca.continueSave = false;
                    return;
                }

                // Verificar se se pretende alterar a entidade detentora associada
                bool isRelacaoHierarquicaDeleted = false;
                if (rhufRow.RowState == DataRowState.Deleted)
                {
                    isRelacaoHierarquicaDeleted = DBAbstractDataLayer.DataAccessRules.UFRule.Current.isRelacaoHierarquicaDeleted(aeufpca.rhufRowID, aeufpca.rhufRowIDUpper, aeufpca.tran);
                }
                else
                {
                    // a operação pretendida é editar a designação da UF e por esse motivo não é necessário
                    // atribuir um novo código (a operação não é create e a entidade detentora não foi alterada)
                    psa.cancelSetNewCodigo = true;
                    aeufpca.continueSave = false;
                }

                GISADataset.RelacaoHierarquicaRow[] newRhufRow = (GISADataset.RelacaoHierarquicaRow[])(GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", aeufpca.newRhufRowID, aeufpca.newRhufRowIDUpper)));
                // Verificar se outro utilizador também alterou (concorrentemente) a entidade detentora associada
                if (isRelacaoHierarquicaDeleted)
                {
                    System.Data.DataSet tempgisaBackup8 = aeufpca.gisaBackup;
                    PersistencyHelper.BackupRow(ref tempgisaBackup8, newRhufRow[0]);
                    aeufpca.gisaBackup = tempgisaBackup8;
                    newRhufRow[0].RejectChanges();
                    System.Data.DataSet tempgisaBackup9 = aeufpca.gisaBackup;
                    PersistencyHelper.BackupRow(ref tempgisaBackup9, rhufRow);
                    aeufpca.gisaBackup = tempgisaBackup9;
                    rhufRow.RejectChanges();
                    System.Data.DataSet tempgisaBackup10 = aeufpca.gisaBackup;
                    PersistencyHelper.BackupRow(ref tempgisaBackup10, nivelUFRow);
                    aeufpca.gisaBackup = tempgisaBackup10;
                    nivelUFRow.RejectChanges();
                    aeufpca.OperationError = PersistencyHelper.AddEditUFPreConcArguments.OperationErrors.EditOriginalEd;
                    aeufpca.message = "A entidade detentora da unidade física em edição foi alterada por outro utilizador." + Environment.NewLine + "Esta operação não poderá, por isso, ser concluída.";
                    psa.cancelSetNewCodigo = true;
                    aeufpca.continueSave = false;
                }
                else if (newRhufRow.Length > 0) // se tivermos alterado a entidade detentora da uf
                {
                    // verificar se a ED a associar à UF não foi apagada por outro utilizador
                    if (DBAbstractDataLayer.DataAccessRules.UFRule.Current.isNivelRowDeleted(newRhufRow[0].NivelRowByNivelRelacaoHierarquicaUpper.ID, aeufpca.tran))
                    {
                        System.Data.DataSet tempgisaBackup11 = aeufpca.gisaBackup;
                        PersistencyHelper.BackupRow(ref tempgisaBackup11, newRhufRow[0]);
                        aeufpca.gisaBackup = tempgisaBackup11;
                        newRhufRow[0].RejectChanges();
                        System.Data.DataSet tempgisaBackup12 = aeufpca.gisaBackup;
                        PersistencyHelper.BackupRow(ref tempgisaBackup12, rhufRow);
                        aeufpca.gisaBackup = tempgisaBackup12;
                        rhufRow.RejectChanges();
                        System.Data.DataSet tempgisaBackup13 = aeufpca.gisaBackup;
                        PersistencyHelper.BackupRow(ref tempgisaBackup13, nivelUFRow);
                        aeufpca.gisaBackup = tempgisaBackup13;
                        nivelUFRow.RejectChanges();
                        if (!isUFDeleted)
                        {
                            // UF existe mas a ED que se pretende adicionar não
                            aeufpca.OperationError = PersistencyHelper.AddEditUFPreConcArguments.OperationErrors.EditNewEd;
                            aeufpca.message = "A entidade produtora que pretende associar à unidade física foi eliminada por outro utilizador. Esta operação não poderá, por isso, ser concluída.";
                        }
                        psa.cancelSetNewCodigo = true;
                        aeufpca.continueSave = false;
                    }
                    //else if (!isUFDeleted)
                    //{
                    //    // Tanto a UF como a ED existem;
                    //    NivelRule.Current.DeleteSFRDUnidadeFisica(nivelUFRow.ID, aeufpca.tran);
                    //    foreach (GISADataset.SFRDUnidadeFisicaRow relacaoRow in nivelUFRow.GetSFRDUnidadeFisicaRows())
                    //    {

                    //        System.Data.DataSet tempgisaBackup14 = aeufpca.gisaBackup;
                    //        PersistencyHelper.BackupRow(ref tempgisaBackup14, relacaoRow);
                    //        aeufpca.gisaBackup = tempgisaBackup14;
                    //        relacaoRow.Delete();
                    //    }
                    //}
                }
            }
        }

        public static void SetCodigo(PersistencyHelper.PreSaveArguments args)
        {
            PersistencyHelper.IsCodigoUFBeingUsedPreSaveArguments psa = null;
            psa = (PersistencyHelper.IsCodigoUFBeingUsedPreSaveArguments)args;
            GISADataset.NivelUnidadeFisicaCodigoRow codRow = null;

            // a atribuição de um código de referência ocorre quando se cria uma 
            // UF nova ou se está a mudar a ED de uma UF e é atribuído só se não houver
            // qualquer conflito de concorrência
            if (!psa.cancelSetNewCodigo)
            {
                GISADataset.NivelRow nivelUFRow = (GISADataset.NivelRow)(GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + psa.nivelUFRowID.ToString())[0]);
                codRow = UnidadesFisicasHelper.GetNewCodigoRow(nivelUFRow, System.DateTime.Now.Year);
                decimal newCounterValue = DBAbstractDataLayer.DataAccessRules.UFRule.Current.IsCodigoUFBeingUsed(codRow.ID, codRow.Ano, psa.tran);
                if (newCounterValue != 0M)
                    nivelUFRow.Codigo = "UF" + codRow.Ano.ToString() + "-" + newCounterValue.ToString();
                else
                    nivelUFRow.Codigo = "UF" + codRow.Ano.ToString() + "-" + codRow.Contador.ToString();

                //quer tenha sido adicionada uma entrada na tabela NivelUnidadeFisicaCodigo quer so tenha sido actualizado
                //o contador de uma das linhas, essa operação foi executada directamente na base de dados pelo que para 
                //manter a coerência é necessário confirmar a mesma operação do lado do dataset
                codRow.AcceptChanges();
                DBAbstractDataLayer.DataAccessRules.UFRule.Current.ReloadNivelUFCodigo(GisaDataSetHelper.GetInstance(), codRow.ID, codRow.Ano, psa.tran);
            }
        }
        #endregion

        #region MasterPanelControloAut
        public static void validateCANewTermo(PersistencyHelper.PreSaveArguments args)
        {
            PersistencyHelper.NewControloAutPreSaveArguments ncaPsa = null;
            ncaPsa = (PersistencyHelper.NewControloAutPreSaveArguments)args;
            GISADataset.DicionarioRow dicionarioRow = (GISADataset.DicionarioRow)(GisaDataSetHelper.GetInstance().Dicionario.Select(string.Format("ID={0} OR Termo='{1}'", ncaPsa.dID.ToString(), ncaPsa.dTermo))[0]);
            GISADataset.ControloAutRow caRow = (GISADataset.ControloAutRow)(GisaDataSetHelper.GetInstance().ControloAut.Select("ID=" + ncaPsa.caID.ToString())[0]);
            //É usado a coluna ID do dicionarioRow uma vez que, se a execução deste método corresponder a uma re-execução da transacção na qual está inserida, o valor do ID passado como argumento pode já ter sido alterado no algoritmo de detecção de conflitos de concorrência (neste caso concreto já existir na base de dados o termo na tabela Dicionario)
            GISADataset.ControloAutDicionarioRow cadRow = null;
            if (GisaDataSetHelper.GetInstance().ControloAutDicionario.Select(string.Format("IDControloAut={0} AND IDDicionario={1} AND IDTipoControloAutForma={2}", caRow.ID, dicionarioRow.ID, ncaPsa.cadIDTipoControloAutForma)).Length > 0)
                cadRow = (GISADataset.ControloAutDicionarioRow)(GisaDataSetHelper.GetInstance().ControloAutDicionario.Select(string.Format("IDControloAut={0} AND IDDicionario={1} AND IDTipoControloAutForma={2}", caRow.ID, dicionarioRow.ID, ncaPsa.cadIDTipoControloAutForma))[0]);
            else if (GisaDataSetHelper.GetInstance().ControloAutDicionario.Select(string.Format("IDControloAut={0} AND IDDicionario={1} AND IDTipoControloAutForma={2}", ncaPsa.cadIDControloAut, ncaPsa.cadIDDicionario, ncaPsa.cadIDTipoControloAutForma)).Length > 0)
                cadRow = (GISADataset.ControloAutDicionarioRow)(GisaDataSetHelper.GetInstance().ControloAutDicionario.Select(string.Format("IDControloAut={0} AND IDDicionario={1} AND IDTipoControloAutForma={2}", ncaPsa.cadIDControloAut, ncaPsa.cadIDDicionario, ncaPsa.cadIDTipoControloAutForma))[0]);
            else
                Debug.Assert(false, "Situação imprevista!!");

            GISADataset.NivelControloAutRow ncaRow = null;
            GISADataset.NivelRow nRow = null;
            if (caRow.TipoNoticiaAutRow.ID == Convert.ToInt64(TipoNoticiaAut.EntidadeProdutora))
            {
                ncaRow = (GISADataset.NivelControloAutRow)(GisaDataSetHelper.GetInstance().NivelControloAut.Select("ID=" + ncaPsa.nID.ToString())[0]);
                nRow = (GISADataset.NivelRow)(GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + ncaPsa.nID.ToString())[0]);
            }

            if (!(DBAbstractDataLayer.DataAccessRules.DiplomaModeloRule.Current.isTermoUsedByOthers(caRow.ID, dicionarioRow.CatCode, dicionarioRow.Termo.Trim().Replace("'", "''"), false, caRow.TipoNoticiaAutRow.ID, ncaPsa.tran)))
                ncaPsa.successTermo = true;

            if (caRow.TipoNoticiaAutRow.ID == Convert.ToInt64(TipoNoticiaAut.EntidadeProdutora) && DBAbstractDataLayer.DataAccessRules.NivelRule.Current.isUniqueCodigo(nRow.Codigo, nRow.ID, ncaPsa.tran))
                ncaPsa.successCodigo = true;

            if (!ncaPsa.successTermo || (caRow.TipoNoticiaAutRow.ID == Convert.ToInt64(TipoNoticiaAut.EntidadeProdutora) && !ncaPsa.successCodigo))
            {
                cadRow.RejectChanges();
                if (caRow.TipoNoticiaAutRow.ID == Convert.ToInt64(TipoNoticiaAut.EntidadeProdutora))
                {
                    ncaRow.RejectChanges();

                    nRow.GetTrusteeNivelPrivilegeRows().ToList().ForEach(r => r.RejectChanges());
                    nRow.GetFRDBaseRows().ToList().ForEach(r => r.RejectChanges());
                    nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquicaUpper().ToList().ForEach(r => r.RejectChanges());

                    nRow.RejectChanges();

                    caRow.GetControloAutEntidadeProdutoraRows().ToList().ForEach(r => r.RejectChanges());
                    caRow.GetControloAutDatasExistenciaRows().ToList().ForEach(r => r.RejectChanges());
                }
                GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao.Cast<GISADataset.ControloAutDataDeDescricaoRow>().Where(r => r.IDControloAut == caRow.ID).ToList().ForEach(r => r.RejectChanges());
                caRow.GetInteg_RelacaoExternaControloAutRows().ToList().ForEach(r => r.RejectChanges());
                caRow.GetIndexFRDCARows().ToList().ForEach(r => r.RejectChanges());
                caRow.RejectChanges();
                dicionarioRow.RejectChanges();
            }
        }
        #endregion
    }
}
