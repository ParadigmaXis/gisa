using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Fedora.FedoraHandler;
using GISA.Model;
using GISA.SharedResources;

namespace GISA.Model
{
    public class ObjetoDigitalFedoraHelper
    {
        public enum Contexto
        {
            nenhum = 0,
            objetosDigitais = 1,
            imagens = 2
        }

        public GISADataset.NivelRow currentNivel;
        public List<SubDocumento> docSimplesSemOD = new List<SubDocumento>();
        public List<ObjDigSimples> currentODSimples = new List<ObjDigSimples>();
        public ObjDigComposto currentODComp = null;
        public Contexto mContexto = Contexto.nenhum;
        public Dictionary<GISADataset.ObjetoDigitalRow, ObjDigital> newObjects = new Dictionary<GISADataset.ObjetoDigitalRow, ObjDigital>();

        public GISADataset.ObjetoDigitalRow currentObjetoDigitalRow = null;
        public GISADataset.ObjetoDigitalRow currentObjetoDigitalRowComp = null;

        public ObjetoDigitalFedoraHelper() { }

        public void LoadData()
        {
            currentODSimples = new List<ObjDigSimples>();
            docSimplesSemOD = new List<SubDocumento>();
            var tnrID = currentNivel.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First().IDTipoNivelRelacionado;

            // só é considerado válido um contexto definido por um nivel documental
            if (currentNivel.IDTipoNivel != TipoNivel.DOCUMENTAL)
            {
                mContexto = ObjetoDigitalFedoraHelper.Contexto.nenhum;
                return;
            }

            mContexto = IdentifyViewMode(currentNivel);

            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                GisaDataSetHelper.ManageDatasetConstraints(false);

                // carregar a informação da bd consuante o tipo nivel selecionado
                if (tnrID == (long)TipoNivelRelacionado.SD)
                {
                    var nUpperRow = currentNivel.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().Single().NivelRowByNivelRelacaoHierarquicaUpper;
                    DBAbstractDataLayer.DataAccessRules.FedoraRule.Current.LoadObjDigitalData(GisaDataSetHelper.GetInstance(), nUpperRow.ID, nUpperRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First().IDTipoNivelRelacionado, ho.Connection);
                }
                else
                    DBAbstractDataLayer.DataAccessRules.FedoraRule.Current.LoadObjDigitalData(GisaDataSetHelper.GetInstance(), currentNivel.ID, tnrID, ho.Connection);

                GisaDataSetHelper.ManageDatasetConstraints(true);
            }
            catch (Exception e)
            {
                // TODO: apanhar a excepção provocada por uma falha com a comunicação com o servidor
                Trace.WriteLine(e);
                throw;
            }
            finally
            {
                ho.Dispose();
            }

            List<string> pidsParaApagar = new List<string>(); // por algum motivo, no repositório, o objeto composto tem na sua estrutura ods simples que já foram apagados

            var frdRow = currentNivel.GetFRDBaseRows().Single();
            var imgRows = new List<GISADataset.SFRDImagemRow>();
            imgRows.AddRange(frdRow.GetSFRDImagemRows().Where(r => r.Tipo.Equals(FedoraHelper.typeFedora)));

            if (imgRows.Count == 0)
            {
                if (tnrID == (long)TipoNivelRelacionado.SD) // Identificar, caso exista, o OD Composto no documento/processo do subdocumento
                {
                    var nUpperRow = currentNivel.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().Single().NivelRowByNivelRelacaoHierarquicaUpper;
                    // determinar se o documento/processo tem um OD composto associado
                    var imgRowsUpper = nUpperRow.GetFRDBaseRows().Single().GetSFRDImagemRows().Where(r => r.Tipo.Equals(FedoraHelper.typeFedora)).ToList();

                    if (imgRowsUpper.Count != 1) return; // não existe nenhum OD composto para o OD simples associado ao subdocumento

                    // o documento/processo tem um OD associado. Determinar se esse OD é composto
                    var odRowUpper = imgRowsUpper[0].GetSFRDImagemObjetoDigitalRows().Single().ObjetoDigitalRow;

                    if (odRowUpper.GetObjetoDigitalRelacaoHierarquicaRowsByObjetoDigitalObjetoDigitalRelacaoHierarquicaUpper().Count() > 0)
                    {
                        // o OD é composto
                        currentODComp = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.LoadID(odRowUpper.pid, null) as ObjDigComposto;
                        currentObjetoDigitalRowComp = odRowUpper;

                        if (currentODComp == null) return; // não se conseguiu obter o OD do servidor...

                        // clona o próprio e todos os seus simples
                        currentODComp.original = currentODComp.Clone();
                    }
                }
                else if (tnrID == (long)TipoNivelRelacionado.D) // documento/processo sem ODs associados directamente mas com subdocumentos com ODs simples 
                {
                    GetSubDocsImgRows(imgRows);
                    GetODRows(imgRows);
                }
            }
            else if (imgRows.Count == 1) // existe 1 OD associado mas não se sabe se é simples ou composto
            {
                var odRow = imgRows[0].GetSFRDImagemObjetoDigitalRows().Single().ObjetoDigitalRow;
                var od = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.LoadID(odRow.pid, null);

                if (od == null)
                {
                    ObjectNotFound(odRow.pid);
                    return;
                }

                od.publicado = odRow.Publicado;

                if (tnrID == (long)TipoNivelRelacionado.SD) // o OD é garantidamente simples
                {
                    PermissoesHelper.LoadObjDigitalPermissions(currentNivel, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.TrusteeRow);
                    currentObjetoDigitalRow = odRow;
                    currentODSimples = new List<ObjDigSimples>() { od as ObjDigSimples };

                    var nUpperRow = currentNivel.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().Single().NivelRowByNivelRelacaoHierarquicaUpper;
                    // determinar se o documento/processo tem um OD composto associado
                    var imgRowsUpper = nUpperRow.GetFRDBaseRows().Single().GetSFRDImagemRows().Where(r => r.Tipo.Equals(FedoraHelper.typeFedora)).ToList();

                    if (imgRowsUpper.Count == 1)
                    {
                        // o documento/processo tem um OD associado. Determinar se esse OD é composto
                        var odRowUpper = imgRowsUpper[0].GetSFRDImagemObjetoDigitalRows().Single().ObjetoDigitalRow;

                        if (odRowUpper.GetObjetoDigitalRelacaoHierarquicaRowsByObjetoDigitalObjetoDigitalRelacaoHierarquicaUpper().Count() > 0)
                        {
                            // o OD é composto
                            currentODComp = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.LoadID(odRowUpper.pid, null) as ObjDigComposto;
                            currentObjetoDigitalRowComp = odRowUpper;

                            if (currentODComp == null) { ObjectNotFound(odRowUpper.pid); return; } // não se conseguiu obter o OD do servidor...

                            ((ObjDigSimples)od).parentDocumentTitle = nUpperRow.GetNivelDesignadoRows().Single().Designacao;
                            //currentODComp.publicado = currentObjetoDigitalRowComp.Publicado;
                            currentODComp.objSimples[currentODComp.objSimples.FindIndex(obj => obj.pid == od.pid)] = od as ObjDigSimples;

                            // clona o próprio e todos os seus simples
                            currentODComp.original = currentODComp.Clone();
                        }
                    }
                }
                else if (tnrID == (long)TipoNivelRelacionado.D) // se a UI selecionada for um documento/processo também tem que se ter em conta os OD simples de subdocumentos
                {
                    if (od.GetType() == typeof(ObjDigSimples))
                    {
                        currentObjetoDigitalRow = imgRows.Single().GetSFRDImagemObjetoDigitalRows().Single().ObjetoDigitalRow;
                        GetSubDocsImgRows(imgRows);
                        GetODRows(imgRows);
                    }
                    else
                    {
                        currentODComp = od as ObjDigComposto;
                        currentObjetoDigitalRowComp = odRow;
                        currentODComp.objSimples.ForEach(odSimples =>
                        {
                            var odSimplesRow = GisaDataSetHelper.GetInstance().ObjetoDigital.Cast<GISADataset.ObjetoDigitalRow>().SingleOrDefault(r => r.pid.Equals(odSimples.pid));
                            if (odSimplesRow != null)
                            {
                                odSimples.guiorder = odSimplesRow.GUIOrder;
                                odSimples.publicado = odSimplesRow.Publicado;
                            }
                            else if (odSimples.serverState == ServerState.Deleted)
                                pidsParaApagar.Add(odSimples.pid);
                        });

                        if (pidsParaApagar.Count > 0)
                            FedoraHelper.FixObjetoDigital(ref currentODComp, pidsParaApagar, frdRow, ref currentObjetoDigitalRowComp, ref currentObjetoDigitalRow);
                    }
                }
                else
                {
                    if (od.GetType() == typeof(ObjDigSimples))
                    {
                        currentODSimples = new List<ObjDigSimples>() { od as ObjDigSimples };
                        currentObjetoDigitalRow = odRow;
                    }
                    else
                    {
                        currentODComp = od as ObjDigComposto;
                        currentObjetoDigitalRowComp = odRow;
                    }
                }

                od.original = od.Clone();
            }
            else
            {
                // caso onde estão associados vários simples soltos e garantidamente a UI selecionada não é um subdocumento (um subdocumento só pode ter um OD simples associado)
                Trace.Assert(tnrID != (long)TipoNivelRelacionado.SD);
                GetSubDocsImgRows(imgRows);
                GetODRows(imgRows);
            }

            // preencher o estado publicado nos objetos digitais
            if (currentODComp != null)
            {
                var odRow = default(GISADataset.ObjetoDigitalRow);
                currentODComp.objSimples.ToList().ForEach(odSimples =>
                {
                    odRow = GisaDataSetHelper.GetInstance().ObjetoDigital.Cast<GISADataset.ObjetoDigitalRow>().SingleOrDefault(r => r.pid.Equals(odSimples.pid));
                    odSimples.publicado = odRow.Publicado;
                });

                odRow = GisaDataSetHelper.GetInstance().ObjetoDigital.Cast<GISADataset.ObjetoDigitalRow>().Single(r => r.pid.Equals(currentODComp.pid));
                currentODComp.publicado = odRow.Publicado;
            }
            else
            {
                currentODSimples.ToList().ForEach(odSimples =>
                {
                    var odRow = GisaDataSetHelper.GetInstance().ObjetoDigital.Cast<GISADataset.ObjetoDigitalRow>().Single(r => r.pid.Equals(odSimples.pid));
                    odSimples.publicado = odRow.Publicado;
                });
            }

            // obter documentos simples sem objeto digital para efeitos de ordenação
            GetSubDocsSemODs();
        }

        private ObjetoDigitalFedoraHelper.Contexto IdentifyViewMode(GISADataset.NivelRow nRow)
        {
            if (nRow.IDTipoNivel != (long)TipoNivel.DOCUMENTAL) return ObjetoDigitalFedoraHelper.Contexto.nenhum;

            var rhRow = currentNivel.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First();

            return rhRow.IDTipoNivelRelacionado == TipoNivelRelacionado.SD ? ObjetoDigitalFedoraHelper.Contexto.imagens : ObjetoDigitalFedoraHelper.Contexto.objetosDigitais;
        }

        private void GetSubDocsImgRows(List<GISADataset.SFRDImagemRow> imgRows)
        {
            if (currentNivel.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First().IDTipoNivelRelacionado != TipoNivelRelacionado.D) return;

            currentNivel.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquicaUpper().ToList().ForEach(rh =>
            {
                var imgRow = rh.NivelRowByNivelRelacaoHierarquica.GetFRDBaseRows().Single().GetSFRDImagemRows().SingleOrDefault(r => r.Tipo.Equals(FedoraHelper.typeFedora));
                if (imgRow != null)
                    imgRows.Add(imgRow);
            });
        }

        private void GetSubDocsSemODs()
        {
            if (currentNivel.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First().IDTipoNivelRelacionado != TipoNivelRelacionado.D) return;

            currentNivel.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquicaUpper().ToList().ForEach(rh =>
            {
                var imgRow = rh.NivelRowByNivelRelacaoHierarquica.GetFRDBaseRows().Single().GetSFRDImagemRows().SingleOrDefault(r => r.Tipo.Equals(FedoraHelper.typeFedora));
                if (imgRow == null)
                {
                    var docSimples = new SubDocumento();
                    var nRow = rh.NivelRowByNivelRelacaoHierarquica;
                    docSimples.nRow = nRow;
                    docSimples.id = nRow.ID;
                    docSimples.guiorder = nRow.GetNivelDesignadoRows().Single().GetNivelDocumentoSimplesRows().Single().GUIOrder;
                    docSimples.designacao = nRow.GetNivelDesignadoRows().Single().Designacao;
                    docSimplesSemOD.Add(docSimples);
                }
            });
        }

        private void ObjectNotFound(string pid)
        {
            MessageBox.Show("A unidade informacional selecionada tem associado um objeto " + System.Environment.NewLine +
                "digital (" + pid + ") o qual não foi possivel obter do repositório. " + System.Environment.NewLine +
                "Contacte o administrador de sistemas.", "Objeto digital não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //currentNivel = null;
            mContexto = ObjetoDigitalFedoraHelper.Contexto.nenhum;

            Trace.WriteLine("OD não encontrado: " + pid);
        }

        private void GetODRows(List<GISADataset.SFRDImagemRow> imgRows)
        {
            currentODSimples = new List<ObjDigSimples>();

            foreach (var odRow in imgRows.Select(imgRow => imgRow.GetSFRDImagemObjetoDigitalRows().Single().ObjetoDigitalRow).OrderBy(r => r.GUIOrder))
            {
                var od = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.LoadID(odRow.pid, null);

                if (od != null)
                {
                    var odSimples = (ObjDigSimples)od;
                    odSimples.guiorder = odRow.GUIOrder;
                    odSimples.publicado = odRow.Publicado;
                    odSimples.original = od.Clone();
                    currentODSimples.Add(odSimples);
                }
                else
                {
                    currentODSimples.Add(new ObjDigSimples() { pid = odRow.pid, titulo = odRow.Titulo, state = State.notFound });
                    Trace.WriteLine("OD não encontrado: " + odRow.pid);
                }
            }
        }

        public void ViewToModel(Contexto viewMode, bool disableSave)
        {
            var tnrID = currentNivel.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First().IDTipoNivelRelacionado;

            if (viewMode == ObjetoDigitalFedoraHelper.Contexto.imagens)
            {
                // neste modo, o contexto é sempre um subdocumento e como tal tem que se ter em consideração informação sobre o subdocumento e o seu documento/processo
                Trace.Assert(tnrID == (long)TipoNivelRelacionado.SD);

                var repoName = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.GetRepositoryName();
                var imgVolRow = FedoraHelper.GetRepositoryImagemVolumeRow();

                Debug.Assert(currentODSimples.Count <= 1); // no contexto de um subdocumento só pode haver 1 OD no máximo
                if (currentODSimples.Count == 1)
                {
                    var odSimples = currentODSimples[0];
                    if (odSimples.state == State.added)
                    {
                        var frdRow = currentNivel.GetFRDBaseRows().Single();
                        odSimples.gisa_id = FedoraHelper.gisaPrefix + currentNivel.ID;

                        var idxTip = frdRow.GetIndexFRDCARows().SingleOrDefault(idx => idx["Selector"] != DBNull.Value && idx.Selector == -1);
                        if (idxTip != null)
                            odSimples.tipologia = idxTip.ControloAutRow.GetControloAutDicionarioRows().Single(cad => cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada).DicionarioRow.Termo;

                        var assuntos = frdRow.GetIndexFRDCARows().Where(idx => idx.ControloAutRow.IDTipoNoticiaAut < 4).SelectMany(idx => idx.ControloAutRow.GetControloAutDicionarioRows()).Where(cad => cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada).Select(cad => cad.DicionarioRow.Termo);
                        if (assuntos.Count() > 0)
                            odSimples.assuntos = assuntos.ToList();

                        var imgRow = GisaDataSetHelper.GetInstance().SFRDImagem.NewSFRDImagemRow();
                        imgRow.Identificador = "";
                        imgRow.FRDBaseRow = frdRow;
                        imgRow.Tipo = FedoraHelper.typeFedora;
                        imgRow.Versao = new byte[] { };
                        imgRow.isDeleted = 0;
                        imgRow.GUIOrder = long.MaxValue;
                        imgRow.Descricao = "";
                        imgRow.SFRDImagemVolumeRow = imgVolRow;
                        GisaDataSetHelper.GetInstance().SFRDImagem.AddSFRDImagemRow(imgRow);

                        if (currentODComp != null)
                        {
                            Debug.Assert(currentObjetoDigitalRowComp != null);

                            var nUpperRow = currentNivel.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().Single().NivelRowByNivelRelacaoHierarquicaUpper;
                            odSimples.parentDocumentTitle = nUpperRow.GetNivelDesignadoRows().Single().Designacao;
                            var orderNr = currentNivel.GetNivelDesignadoRows().Single().GetNivelDocumentoSimplesRows().Single().GUIOrder;
                            odSimples.guiorder = orderNr;
                            currentObjetoDigitalRow = GisaDataSetHelper.GetInstance().ObjetoDigital.AddObjetoDigitalRow(odSimples.pid, odSimples.titulo, odSimples.publicado, orderNr, new byte[] { }, 0);
                            GisaDataSetHelper.GetInstance().ObjetoDigitalRelacaoHierarquica.AddObjetoDigitalRelacaoHierarquicaRow(currentObjetoDigitalRow, currentObjetoDigitalRowComp, new byte[] { }, 0);

                            currentODComp.objSimples.Insert((int)odSimples.guiorder - 1, odSimples);
                        }
                        else
                        {
                            var ndsRow = currentNivel.GetNivelDesignadoRows().Single().GetNivelDocumentoSimplesRows().Single();
                            currentObjetoDigitalRow = GisaDataSetHelper.GetInstance().ObjetoDigital.AddObjetoDigitalRow(odSimples.pid, odSimples.titulo, odSimples.publicado, ndsRow.GUIOrder, new byte[] { }, 0);
                        }

                        GisaDataSetHelper.GetInstance().SFRDImagemObjetoDigital.AddSFRDImagemObjetoDigitalRow(imgRow.IDFRDBase, imgRow.idx, currentObjetoDigitalRow, new byte[] { }, 0);
                        //PermissoesHelper.AddNewObjDigGrantPermissions(currentObjetoDigitalRow, currentNivel);
                        newObjects.Add(currentObjetoDigitalRow, odSimples);
                    }
                    else if (odSimples.state == State.modified)
                    {
                        currentObjetoDigitalRow.Titulo = odSimples.titulo;
                        currentObjetoDigitalRow.Publicado = odSimples.publicado;

                        if (currentODComp != null)
                            currentObjetoDigitalRowComp.Publicado = currentODComp.publicado;
                    }
                    else if (odSimples.state == State.deleted)
                    {
                        FedoraHelper.DeleteObjDigitalRows(currentObjetoDigitalRow);
                        if (currentODComp != null && currentODComp.state == State.deleted)
                            FedoraHelper.DeleteObjDigitalRows(currentObjetoDigitalRowComp);
                    }
                }
            }
            else if (mContexto == ObjetoDigitalFedoraHelper.Contexto.objetosDigitais)
            {
                if (disableSave) return;

                if (currentODComp != null) // caso onde existe um OD composto associado à UI
                {
                    var odRowComp = default(GISADataset.ObjetoDigitalRow);
                    switch (currentODComp.state)
                    {
                        case State.added:
                            currentODComp.gisa_id = FedoraHelper.gisaPrefix + currentNivel.ID;
                            var frdRow = currentNivel.GetFRDBaseRows().Single();
                            var imgVolRow = FedoraHelper.GetRepositoryImagemVolumeRow();
                            var imgRow = GisaDataSetHelper.GetInstance().SFRDImagem.NewSFRDImagemRow();
                            imgRow.Identificador = "";
                            imgRow.FRDBaseRow = frdRow;
                            imgRow.Tipo = FedoraHelper.typeFedora;
                            imgRow.Versao = new byte[] { };
                            imgRow.isDeleted = 0;
                            imgRow.GUIOrder = long.MaxValue;
                            imgRow.Descricao = "";
                            imgRow.SFRDImagemVolumeRow = imgVolRow;
                            GisaDataSetHelper.GetInstance().SFRDImagem.AddSFRDImagemRow(imgRow);

                            currentObjetoDigitalRowComp = GisaDataSetHelper.GetInstance().ObjetoDigital.AddObjetoDigitalRow(currentODComp.pid, currentODComp.titulo, currentODComp.publicado, 1, new byte[] { }, 0);
                            GisaDataSetHelper.GetInstance().SFRDImagemObjetoDigital.AddSFRDImagemObjetoDigitalRow(imgRow.IDFRDBase, imgRow.idx, currentObjetoDigitalRowComp, new byte[] { }, 0);

                            //PermissoesHelper.AddNewObjDigGrantPermissions(currentObjetoDigitalRowComp, currentNivel);

                            newObjects.Add(currentObjetoDigitalRowComp, currentODComp);

                            var idxTip = frdRow.GetIndexFRDCARows().SingleOrDefault(idx => idx["Selector"] != DBNull.Value && idx.Selector == -1);
                            if (idxTip != null)
                                currentODComp.tipologia = idxTip.ControloAutRow.GetControloAutDicionarioRows().Single(cad => cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada).DicionarioRow.Termo;

                            var assuntos = frdRow.GetIndexFRDCARows().Where(idx => idx.ControloAutRow.IDTipoNoticiaAut < 4).SelectMany(idx => idx.ControloAutRow.GetControloAutDicionarioRows()).Where(cad => cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada).Select(cad => cad.DicionarioRow.Termo);
                            if (assuntos.Count() > 0)
                                currentODComp.assuntos = assuntos.ToList();

                            break;
                        case State.modified:
                            currentObjetoDigitalRowComp.Titulo = currentODComp.titulo;
                            currentObjetoDigitalRowComp.Publicado = currentODComp.publicado;
                            break;
                        case State.deleted:
                            FedoraHelper.DeleteObjDigitalRows(currentObjetoDigitalRowComp);
                            break;
                    }

                    odRowComp = currentObjetoDigitalRowComp;

                    ViewToModelObjsSimples(currentODComp.objSimples, currentODComp, odRowComp);
                }
                else // caso onde só existem ODs simples soltos associados à UI
                    ViewToModelObjsSimples(currentODSimples);
            }


            // atribuir permissões aos objectos digitais novos
            var odRows = GisaDataSetHelper.GetInstance().ObjetoDigital.Cast<GISADataset.ObjetoDigitalRow>().Where(r => r.RowState == DataRowState.Added).ToList();
            PermissoesHelper.AddNewObjDigGrantPermissions(odRows, currentNivel);

            // atribuir permissão de leitura ao grupo publicados consoante o valor da flag publicado definida nas datarows GISADataset.ObjetoDigitalRow que foram adicionadas ou editadas
            GisaDataSetHelper.GetInstance().ObjetoDigital.Cast<GISADataset.ObjetoDigitalRow>().Where(r => r.RowState == DataRowState.Added || r.RowState == DataRowState.Modified).ToList()
                .ForEach(odRow => PermissoesHelper.ChangeObjDigPermissionPublicados(odRow));

            // actualizar ordem dos subdocumentos sem objeto digital
            docSimplesSemOD.ForEach(docSimples => docSimples.nRow.GetNivelDesignadoRows().Single().GetNivelDocumentoSimplesRows().Single().GUIOrder = docSimples.guiorder);
        }

        private void ViewToModelObjsSimples(List<ObjDigSimples> odsSimples)
        {
            ViewToModelObjsSimples(odsSimples, null, null);
        }

        private void ViewToModelObjsSimples(List<ObjDigSimples> odsSimples, ObjDigComposto odComp, GISADataset.ObjetoDigitalRow odRowComp)
        {
            odsSimples.ForEach(odSimples =>
            {
                var odRow = default(GISADataset.ObjetoDigitalRow);
                switch (odSimples.state)
                {
                    case State.added:
                        if (odComp != null && odComp.state != State.deleted)
                            CreateDatabaseObjDigSimples(odComp, odRowComp, odSimples);
                        else
                            CreateDatabaseObjDigSimplesSolto(odSimples);
                        break;
                    case State.modified:
                        odRow = GisaDataSetHelper.GetInstance().ObjetoDigital.Cast<GISADataset.ObjetoDigitalRow>().Single(r => r.RowState != DataRowState.Deleted && r.pid.Equals(odSimples.pid));
                        odRow.Titulo = odSimples.titulo;
                        odRow.Publicado = odSimples.publicado;
                        odRow.GUIOrder = odSimples.guiorder;

                        UpdateNvlDocSimplesOrderNr(odSimples);

                        if (odComp != null)
                            AddOrRemoveODRelations(odComp, odSimples, odRow);
                        break;
                    case State.deleted:
                        odRow = GisaDataSetHelper.GetInstance().ObjetoDigital.Cast<GISADataset.ObjetoDigitalRow>().Single(r => r.RowState != DataRowState.Deleted && r.pid.Equals(odSimples.pid));
                        FedoraHelper.DeleteObjDigitalRows(odRow);
                        break;
                    case State.unchanged: // apanhar o caso de a ordem do OD Simples ter mudado
                        odRow = GisaDataSetHelper.GetInstance().ObjetoDigital.Cast<GISADataset.ObjetoDigitalRow>().Single(r => r.RowState != DataRowState.Deleted && r.pid.Equals(odSimples.pid));
                        odRow.GUIOrder = odSimples.guiorder;

                        UpdateNvlDocSimplesOrderNr(odSimples);

                        if (odComp != null)
                            AddOrRemoveODRelations(odComp, odSimples, odRow);
                        break;
                }
            });

            // atualizar a tipologia e assuntos dos objetos simples
            // - se houver um objeto composto, nenhum dos seus simples não pode ter tipologia e assuntos definidos
            // - se não houver um objeto composto:
            //   * se o nro de objetos simples for um, então esse objeto simples pode ter tipologia e assuntos definidos
            //   * se o nro de objetos simples for maior que um, então nenhum desses objetos pode ter tipologia e assuntos definidos
            if (odComp == null || odComp.state == State.deleted)
            {
                var ods = odsSimples.Where(od => od.state != State.deleted);
                if (ods.Count() == 1)
                {
                    var odSimples = ods.Single();
                    var frdRow = currentNivel.GetFRDBaseRows().Single();
                    var idxTip = frdRow.GetIndexFRDCARows().SingleOrDefault(idx => idx["Selector"] != DBNull.Value && idx.Selector == -1);
                    if (idxTip != null)
                        odSimples.tipologia = idxTip.ControloAutRow.GetControloAutDicionarioRows().Single(cad => cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada).DicionarioRow.Termo;

                    var assuntos = frdRow.GetIndexFRDCARows().Where(idx => idx.ControloAutRow.IDTipoNoticiaAut < 4).SelectMany(idx => idx.ControloAutRow.GetControloAutDicionarioRows()).Where(cad => cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada).Select(cad => cad.DicionarioRow.Termo);
                    if (assuntos.Count() > 0)
                        odSimples.assuntos = assuntos.ToList();
                }
                else
                {
                    odsSimples.Where(od => od.state != State.deleted).ToList().ForEach(od =>
                    {
                        od.tipologia = "";
                        od.assuntos.Clear();
                        if (od.state == State.unchanged)
                            od.state = State.modified;
                    });
                }
            }
            else
            {
                odsSimples.Where(od => od.state != State.deleted).ToList().ForEach(od =>
                {
                    od.tipologia = "";
                    od.assuntos.Clear();
                    if (od.state == State.unchanged)
                        od.state = State.modified;
                });
            }
        }

        private static void UpdateNvlDocSimplesOrderNr(ObjDigSimples odSimples)
        {
            if (odSimples.gisa_id != null && odSimples.gisa_id.Length > 0)
            {
                var nds = GisaDataSetHelper.GetInstance().NivelDocumentoSimples.Cast<GISADataset.NivelDocumentoSimplesRow>().SingleOrDefault(r => r.RowState != DataRowState.Deleted && r.ID == FedoraHelper.GetGisaID(odSimples.gisa_id));

                if (nds != null)
                    nds.GUIOrder = odSimples.guiorder;
            }
        }

        private void AddOrRemoveODRelations(ObjDigComposto odComp, ObjDigSimples odSimples, GISADataset.ObjetoDigitalRow odRow)
        {
            var frdRow = currentNivel.GetFRDBaseRows().Single();
            if (odComp.state == State.added)
            {
                var imgODRow = odRow.GetSFRDImagemObjetoDigitalRows().SingleOrDefault(r => r.IDFRDBase == frdRow.ID);
                if (imgODRow != null) // esta row é nula no caso dos ODs Simples associados a subdocumentos
                {
                    imgODRow.SFRDImagemRowParent.Delete();
                    imgODRow.Delete();
                }
                GisaDataSetHelper.GetInstance().ObjetoDigitalRelacaoHierarquica.AddObjetoDigitalRelacaoHierarquicaRow(odRow, currentObjetoDigitalRowComp, new byte[] { }, 0);
            }
            else if (odComp.state == State.deleted)
            {
                if (odRow.GetSFRDImagemObjetoDigitalRows().Count() == 0) // se se tratar de um od simples sem estar relacionado com um subdocumento cria-se relacao com a UI selecionada
                    FedoraHelper.RelateODtoUI(odSimples, odRow, currentNivel.GetFRDBaseRows().Single());
            }
        }

        private void CreateDatabaseObjDigSimples(ObjDigComposto odComp, GISADataset.ObjetoDigitalRow odRowComp, ObjDigSimples odSimples)
        {
            Debug.Assert(odComp != null);
            Debug.Assert(odSimples != null);
            Debug.Assert(odRowComp != null && odRowComp.RowState != DataRowState.Deleted);

            var orderNr = odComp.objSimples.IndexOf(odSimples);
            var odRow = GisaDataSetHelper.GetInstance().ObjetoDigital.AddObjetoDigitalRow(odSimples.pid, odSimples.titulo, odSimples.publicado, (orderNr + 1), new byte[] { }, 0);


            GisaDataSetHelper.GetInstance().ObjetoDigitalRelacaoHierarquica.AddObjetoDigitalRelacaoHierarquicaRow(odRow, odRowComp, new byte[] { }, 0);
            //PermissoesHelper.AddNewObjDigGrantPermissions(odRow, currentNivel);

            newObjects.Add(odRow, odSimples);
        }

        private void CreateDatabaseObjDigSimplesSolto(ObjDigSimples odSimples)
        {
            Debug.Assert(odSimples.guiorder > 0);
            var odRow = GisaDataSetHelper.GetInstance().ObjetoDigital.AddObjetoDigitalRow(odSimples.pid, odSimples.titulo, odSimples.publicado, odSimples.guiorder, new byte[] { }, 0);
            //PermissoesHelper.AddNewObjDigGrantPermissions(odRow, currentNivel);

            FedoraHelper.RelateODtoUI(odSimples, odRow, currentNivel.GetFRDBaseRows().Single());

            newObjects.Add(odRow, odSimples);
        }

        public void Deactivate()
        {
            mContexto = ObjetoDigitalFedoraHelper.Contexto.nenhum;
            currentODComp = null;
            currentODSimples = null;
            currentObjetoDigitalRow = null;
            currentObjetoDigitalRowComp = null;
            docSimplesSemOD = null;
            newObjects.Clear();
        }
    }
}
