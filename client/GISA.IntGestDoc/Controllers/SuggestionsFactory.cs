using System;
using System.Data;

using System.Collections.Generic;
using System.Linq;
using System.Text;

using GISA.Model;
using GISA.IntGestDoc.Model;
using GISA.IntGestDoc.Model.EntidadesExternas;
using GISA.IntGestDoc.Model.EntidadesInternas;

using log4net;

namespace GISA.IntGestDoc.Controllers
{
    public static class SuggestionsFactory
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SuggestionsFactory));

        public struct EntidadeExterna
        {
            public string IDExterno;
            public int IDTipo;
            public int IDSistema;
        }

        public static List<CorrespondenciaDocs> GetSuggestions(List<DocumentoExterno> deList)
        {
            var ticks = DateTime.Now.Ticks;

            var raes = new HashSet<RegistoAutoridadeExterno>();
            var decs = new HashSet<DocumentoComposto>();
            var deas = new HashSet<DocumentoAnexo>();
            var des = new HashSet<DocumentoSimples>();
            foreach (var de in deList.OfType<DocumentoSimples>())
            {
                des.Add(de);

                if (de.Tipologia != null) raes.Add(de.Tipologia);
                if (de.Onomastico != null) raes.Add(de.Onomastico);
                if (de.Toponimia != null) raes.Add(de.Toponimia);
                if (de.Ideografico != null) raes.Add(de.Ideografico);
                if (de.TecnicoDeObra != null) raes.Add(de.TecnicoDeObra);
            }

            foreach (var de in deList.OfType<DocumentoAnexo>())
                deas.Add(de);

            foreach (var de in deList.OfType<DocumentoComposto>())
            {
                decs.Add(de);
                if (de.Produtor != null) raes.Add(de.Produtor);
                if (de.Tipologia != null) raes.Add(de.Tipologia);
                de.LocalizacoesObraDesignacaoActual.ToList().ForEach(g => raes.Add(g.LocalizacaoObraDesignacaoActual));
                de.TecnicosDeObra.ToList().ForEach(o => raes.Add(o));
            }

            Dictionary<DocumentoSimples, DocumentoGisa> docsComCorrespAnteriores;
            Dictionary<DocumentoAnexo, DocumentoGisa> docsAnexosComCorrespAnteriores;
            Dictionary<DocumentoComposto, DocumentoGisa> docsCompostosComCorrespAnteriores;
            Dictionary<RegistoAutoridadeExterno, RegistoAutoridadeInterno> rasComCorrespAnteriores;
            Dictionary<DocumentoSimples, DocumentoGisa> docsComNovasCorrespEncontradas;
            Dictionary<DocumentoAnexo, DocumentoGisa> docsAnexosComNovasCorrespEncontradas;
            Dictionary<DocumentoComposto, DocumentoGisa> docsCompostosComNovasCorrespEncontradas;
            Dictionary<RegistoAutoridadeExterno, RegistoAutoridadeInterno> rasComNovasCorrespEncontradas;
            Dictionary<DocumentoSimples, DocumentoGisa> docsSimplesNovos;
            Dictionary<DocumentoAnexo, DocumentoGisa> docsAnexosNovos;
            Dictionary<DocumentoComposto, DocumentoGisa> docsCompostosNovos;
            Dictionary<RegistoAutoridadeExterno, RegistoAutoridadeInterno> rasNovos;

            var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                GisaDataSetHelper.ManageDatasetConstraints(false);

                //obter dados gisa, com base nos dados externos
                docsComCorrespAnteriores =
                    Database.Database.GetDocsCorrespondenciasAnteriores(des.Distinct().ToList(), ho.Connection);
                docsAnexosComCorrespAnteriores =
                    Database.Database.GetDocsCorrespondenciasAnteriores(deas.Distinct().ToList(), ho.Connection);
                docsCompostosComCorrespAnteriores =
                    Database.Database.GetDocsCorrespondenciasAnteriores(decs.Distinct().ToList(), ho.Connection);
                rasComCorrespAnteriores =
                    Database.Database.GetRAsCorrespondenciasAnteriores(raes.Distinct().ToList(), ho.Connection);

                //recolher lista dos que nao têm match
                List<DocumentoSimples> docsSemCorrespAnteriores =
                    des.Where(doc => !docsComCorrespAnteriores.ContainsKey(doc)).ToList();
                List<DocumentoAnexo> docsAnexosSemCorrespAnteriores =
                    deas.Where(doc => !docsAnexosComCorrespAnteriores.ContainsKey(doc)).ToList();
                List<DocumentoComposto> docsCompostosSemCorrespAnteriores =
                    decs.Where(doc => !docsCompostosComCorrespAnteriores.ContainsKey(doc)).ToList();
                List<RegistoAutoridadeExterno> rasSemCorrespAnteriores =
                    raes.Where(rae => !rasComCorrespAnteriores.ContainsKey(rae)).ToList();

                //criar correspondencias com documentos já existentes
                docsComNovasCorrespEncontradas =
                    Database.Database.GetDocsCorrespondenciasNovas(docsSemCorrespAnteriores, ho.Connection);
                docsAnexosComNovasCorrespEncontradas =
                    Database.Database.GetDocsCorrespondenciasNovas(docsAnexosSemCorrespAnteriores, ho.Connection);
                docsCompostosComNovasCorrespEncontradas =
                    Database.Database.GetDocsCorrespondenciasNovas(docsCompostosSemCorrespAnteriores, ho.Connection);
                rasComNovasCorrespEncontradas =
                    Database.Database.GetRAsCorrespondenciasNovas(rasSemCorrespAnteriores, ho.Connection);

                //recolher lista dos que nao têm match
                List<DocumentoSimples> docsSemCorrespNovasEncontradas =
                    des.Where(doc => !docsComCorrespAnteriores.ContainsKey(doc) && !docsComNovasCorrespEncontradas.ContainsKey(doc)).ToList();
                List<DocumentoAnexo> docsAnexosSemCorrespNovasEncontradas =
                    deas.Where(doc => !docsAnexosComCorrespAnteriores.ContainsKey(doc) && !docsAnexosComNovasCorrespEncontradas.ContainsKey(doc)).ToList();
                List<DocumentoComposto> docsCompostosSemCorrespNovasEncontradas =
                    decs.Where(doc => !docsCompostosComCorrespAnteriores.ContainsKey(doc) && !docsCompostosComNovasCorrespEncontradas.ContainsKey(doc)).ToList();
                List<RegistoAutoridadeExterno> rasSemCorrespNovasEncontradas =
                    raes.Where(ra => !rasComCorrespAnteriores.ContainsKey(ra) && !rasComNovasCorrespEncontradas.ContainsKey(ra)).ToList();

                //criar documentos novos para os que não tinham match
                var docsNovos = InternalEntitiesFactory.CreateInternalEntities(docsSemCorrespNovasEncontradas.Cast<DocumentoExterno>().ToList());
                docsSimplesNovos = docsNovos.ToDictionary(doc => (DocumentoSimples)doc.Key, doc => (DocumentoGisa)doc.Value);
                docsNovos = InternalEntitiesFactory.CreateInternalEntities(docsAnexosSemCorrespNovasEncontradas.Cast<DocumentoExterno>().ToList());
                docsAnexosNovos = docsNovos.ToDictionary(doc => (DocumentoAnexo)doc.Key, doc => (DocumentoGisa)doc.Value);
                docsNovos = InternalEntitiesFactory.CreateInternalEntities(docsCompostosSemCorrespNovasEncontradas.Cast<DocumentoExterno>().ToList());
                docsCompostosNovos = docsNovos.ToDictionary(doc => (DocumentoComposto)doc.Key, doc => (DocumentoGisa)doc.Value);
                rasNovos = InternalEntitiesFactory.CreateInternalEntities(rasSemCorrespNovasEncontradas);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            finally
            {
                GisaDataSetHelper.ManageDatasetConstraints(true);
                ho.Dispose();
            }

            var dis = new List<DocumentoGisa>();
            dis.AddRange(docsComCorrespAnteriores.Values.ToArray());
            dis.AddRange(docsComNovasCorrespEncontradas.Values.ToArray());
            dis.AddRange(docsSimplesNovos.Values.ToArray());

            var rais = new List<RegistoAutoridadeInterno>();
            rais.AddRange(rasComCorrespAnteriores.Values.ToArray());
            rais.AddRange(rasComNovasCorrespEncontradas.Values.ToArray());
            rais.AddRange(rasNovos.Values.ToArray());

            // Dependências existentes entre objectos correspondencia:
            //  - o objecto correspondencia referente à tipologia do processo é a mesma no objectos correspondencia desse processo referenciados nos documentos simples
            //  - em cada documento simples, o objecto correspondencia referente ao produtor é o mesmo referenciado no seu processo
            //  - cada documento simples ou composto tem na sua propriedade 'Processo' a referência para o 'CorrespondenciaDocs' do seu processo
            var correspondenciasDocs = new List<CorrespondenciaDocs>();
            var tipologias = new Dictionary<DocumentoComposto, CorrespondenciaRAs>();
            var processos = new Dictionary<DocumentoComposto, CorrespondenciaDocs>();

            foreach (var de in decs)
            {
                CorrespondenciaDocs correspondenciaDocComposto =
                    (CorrespondenciaDocs)AssembleCorrespondencia(de, docsCompostosComCorrespAnteriores, docsCompostosComNovasCorrespEncontradas, docsCompostosNovos, CreateCorrespondenciaDocsCompostos);
                correspondenciasDocs.Add(correspondenciaDocComposto);
                processos.Add(de, correspondenciaDocComposto);

                foreach (var rae in de.RegistosAutoridade)
                {
                    CorrespondenciaRAs correspondenciaRAs =
                        (CorrespondenciaRAs)AssembleCorrespondencia(rae, rasComCorrespAnteriores, rasComNovasCorrespEncontradas, rasNovos, CreateCorrespondenciaRAs);
                    correspondenciaDocComposto.AddCorrespondenciaRA(correspondenciaRAs);

                    DefineStatesCorrespondenciaRAs(correspondenciaDocComposto, correspondenciaRAs);
                }

                var correspTipologia = correspondenciaDocComposto.CorrespondenciasRAs.SingleOrDefault(c => ((RegistoAutoridadeInterno)c.EntidadeInterna).TipoNoticiaAut == TipoNoticiaAut.TipologiaInformacional);
                if (correspTipologia != null)
                    tipologias.Add(de, correspTipologia);

                // definir entidades internas para a tipologia que já esteja associada ao documento (se existir alguma associada)
                if (correspondenciaDocComposto.EntidadeInterna.Id > 0 && docsCompostosComNovasCorrespEncontradas.ContainsKey((DocumentoComposto)correspondenciaDocComposto.EntidadeExterna))
                    Database.Database.AddTipologiaOriginal(correspondenciaDocComposto);
            }

            //documentos simples
            foreach (var de in des)
            {
                CorrespondenciaDocs correspondenciaDocs =
                    (CorrespondenciaDocs)AssembleCorrespondencia(de, docsComCorrespAnteriores, docsComNovasCorrespEncontradas, docsSimplesNovos, CreateCorrespondenciaDocs);
                correspondenciasDocs.Add(correspondenciaDocs);

                ((DocumentoGisa)correspondenciaDocs.EntidadeInterna).Processo = processos[de.Processo];

                foreach (var rae in de.RegistosAutoridade)
                {
                    CorrespondenciaRAs correspondenciaRAs =
                        (CorrespondenciaRAs)AssembleCorrespondencia(rae, rasComCorrespAnteriores, rasComNovasCorrespEncontradas, rasNovos, CreateCorrespondenciaRAs);
                    correspondenciaDocs.AddCorrespondenciaRA(correspondenciaRAs);

                    DefineStatesCorrespondenciaRAs(correspondenciaDocs, correspondenciaRAs);
                }

                if (correspondenciaDocs.TipoSugestao != TipoSugestao.Historico)
                {
                    // definir entidades internas para a tipologia que já esteja associada ao documento (se existir alguma associada)
                    if (correspondenciaDocs.EntidadeInterna.Id > 0 && docsComNovasCorrespEncontradas.ContainsKey((DocumentoSimples)correspondenciaDocs.EntidadeExterna))
                        Database.Database.AddTipologiaOriginal(correspondenciaDocs);
                }
            }

            //anexos
            foreach (var de in deas)
            {
                CorrespondenciaDocs correspondenciaDocsAnexos =
                    (CorrespondenciaDocs)AssembleCorrespondencia(de, docsAnexosComCorrespAnteriores, docsAnexosComNovasCorrespEncontradas, docsAnexosNovos, CreateCorrespondenciaDocsAnexos);
                correspondenciasDocs.Add(correspondenciaDocsAnexos);

                ((DocumentoGisa)correspondenciaDocsAnexos.EntidadeInterna).Processo = processos[de.Processo];
            }

            System.Diagnostics.Debug.WriteLine(">> " + new TimeSpan(DateTime.Now.Ticks - ticks).ToString());

            return correspondenciasDocs;
        }

        private static void DefineStatesCorrespondenciaRAs(CorrespondenciaDocs cDoc, CorrespondenciaRAs cRa)
        {
            bool RelatedToDocument;
            bool DeletePreviousRelation;

            if (cRa.EntidadeInterna.GetType() == typeof(Model.EntidadesInternas.Tipologia))
            {
                cRa.EstadoRelacaoPorOpcao[cRa.TipoOpcao] = TipoEstado.Novo;
                if (cDoc.EntidadeInterna.Estado == TipoEstado.SemAlteracoes)
                {
                    // verificar se o documento já tem uma tipologia
                    var caRow = GisaDataSetHelper.GetInstance().FRDBase
                        .Cast<GISADataset.FRDBaseRow>()
                        .Where(r => r.IDNivel == cDoc.EntidadeInterna.Id)
                        .SingleOrDefault().GetIndexFRDCARows()
                        .Cast<GISADataset.IndexFRDCARow>().ToList()
                        .SingleOrDefault(index => index.ControloAutRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.TipologiaInformacional);

                    DeletePreviousRelation = caRow != null;

                    var caRow2 = GisaDataSetHelper.GetInstance().FRDBase
                        .Cast<GISADataset.FRDBaseRow>()
                        .Where(r => r.IDNivel == cDoc.EntidadeInterna.Id)
                        .SingleOrDefault().GetIndexFRDCARows()
                        .Cast<GISADataset.IndexFRDCARow>().ToList()
                        .SingleOrDefault(index => index.ControloAutRow.ID == cRa.EntidadeInterna.Id);

                    RelatedToDocument = caRow2 != null;

                    if (RelatedToDocument)
                        cRa.EstadoRelacaoPorOpcao[cRa.TipoOpcao] = TipoEstado.SemAlteracoes;
                    else if (!RelatedToDocument && DeletePreviousRelation)
                        cRa.EstadoRelacaoPorOpcao[cRa.TipoOpcao] = TipoEstado.Editar;
                    else
                        cRa.EstadoRelacaoPorOpcao[cRa.TipoOpcao] = TipoEstado.Novo;
                }
            }
            else if (cRa.EntidadeInterna.GetType() == typeof(Model.EntidadesInternas.Produtor))
            {
                var entidadeInterna = cDoc.EntidadeInterna;
                cRa.EstadoRelacaoPorOpcao[cRa.TipoOpcao] = TipoEstado.Novo;

                if (cRa.EntidadeInterna.Estado == TipoEstado.SemAlteracoes && entidadeInterna.Estado == TipoEstado.SemAlteracoes)
                {
                    var caRow = GisaDataSetHelper.GetInstance().ControloAut.Cast<GISADataset.ControloAutRow>()
                        .SingleOrDefault(r => r.ID == cRa.EntidadeInterna.Id);
                    var nRowCA = caRow.GetNivelControloAutRows().Single().NivelRow;

                    cRa.EstadoRelacaoPorOpcao[cRa.TipoOpcao] = Database.Database.IsProdutor(entidadeInterna.Id, nRowCA.ID) ? TipoEstado.SemAlteracoes : TipoEstado.Novo;
                }
            }
            else
            {
                cRa.EstadoRelacaoPorOpcao[cRa.TipoOpcao] = TipoEstado.Novo;
                if (cDoc.EntidadeInterna.Estado == TipoEstado.SemAlteracoes)
                {
                    var caRow = GisaDataSetHelper.GetInstance().FRDBase
                        .Cast<GISADataset.FRDBaseRow>()
                        .Where(r => r.IDNivel == cDoc.EntidadeInterna.Id)
                        .SingleOrDefault().GetIndexFRDCARows()
                        .Cast<GISADataset.IndexFRDCARow>().ToList()
                        .Select(index => index.ControloAutRow)
                        .SingleOrDefault(ca => ca.ID == cRa.EntidadeInterna.Id);

                    cRa.EstadoRelacaoPorOpcao[cRa.TipoOpcao] = caRow != null ? TipoEstado.SemAlteracoes : TipoEstado.Novo;
                }
            }
        }

        private static Correspondencia AssembleCorrespondencia<EE, EI>(
            EE ee,
            Dictionary<EE, EI> eisComCorrespAnteriores,
            Dictionary<EE, EI> eisComNovasCorrespEncontradas,
            Dictionary<EE, EI> eisNovos,
            Func<Model.EntidadeExterna, EntidadeInterna, TipoSugestao, Correspondencia> factoryMethod)
            where EE : Model.EntidadeExterna
            where EI : EntidadeInterna
        {
            EI ei = null;
            TipoSugestao tipoSugestao = TipoSugestao.NaoSugerido;
            if (eisComCorrespAnteriores.ContainsKey(ee))
            {
                ei = eisComCorrespAnteriores[ee];
                tipoSugestao = TipoSugestao.Historico;
            }
            else if (eisComNovasCorrespEncontradas.ContainsKey(ee))
            {
                ei = eisComNovasCorrespEncontradas[ee];
                tipoSugestao = TipoSugestao.Heuristica;
            }
            else if (eisNovos.ContainsKey(ee))
            {
                ei = eisNovos[ee];
                tipoSugestao = TipoSugestao.Criacao;
            }
            else
            {
                throw new InvalidOperationException("Entidade externa sem correspondencia.");
            }

            Correspondencia correspondencia = factoryMethod(ee, ei, tipoSugestao);

            return correspondencia;
        }

        private static Correspondencia CreateCorrespondenciaDocs(Model.EntidadeExterna ee, EntidadeInterna ei, TipoSugestao tipoSugestao)
        {
            return new CorrespondenciaDocs((DocumentoSimples)ee, (DocumentoGisa)ei, tipoSugestao);
        }
        private static Correspondencia CreateCorrespondenciaDocsAnexos(Model.EntidadeExterna ee, EntidadeInterna ei, TipoSugestao tipoSugestao)
        {
            return new CorrespondenciaDocs((DocumentoAnexo)ee, (DocumentoGisa)ei, tipoSugestao);
        }
        private static Correspondencia CreateCorrespondenciaDocsCompostos(Model.EntidadeExterna ee, EntidadeInterna ei, TipoSugestao tipoSugestao)
        {
            return new CorrespondenciaDocs((DocumentoComposto)ee, (DocumentoGisa)ei, tipoSugestao);
        }
        private static Correspondencia CreateCorrespondenciaRAs(Model.EntidadeExterna ee, EntidadeInterna ei, TipoSugestao tipoSugestao)
        {
            return new CorrespondenciaRAs((RegistoAutoridadeExterno)ee, (RegistoAutoridadeInterno)ei, tipoSugestao); ;
        }
    }
}
