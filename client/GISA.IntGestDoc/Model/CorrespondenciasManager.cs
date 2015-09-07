using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.IntGestDoc.Model.EntidadesExternas;
using GISA.IntGestDoc.Model.EntidadesInternas;

namespace GISA.IntGestDoc.Model
{
    class CorrespondenciasManager
    {
        internal HashSet<CorrespondenciaDocs> Sugestoes { get; set; }
        internal HashSet<CorrespondenciaRAs> SugestoesCA { get; set; }
        internal Database.Database database = null;

        internal CorrespondenciasManager()
        {
            /*
            this.Sugestoes = new HashSet<Correspondencia>();

            // Get timeStamp from the last processed suggestion
            DateTime lastProcessed = GetLastProcessedTimeStamp();

            // Call new documents from web service
            IIntGestDocService ws = new Controllers.DocInPortoWS();  
          
            // Database service provider
            this.database = new GISA.IntGestDoc.Database.Database();
            /*
            // Get suggestions 
            var wsDocumentos = ws.GetDocumentos(lastProcessed);
            //var lstIDDocsExternos = wsDocumentos.Select(wsDoc => ((DocumentoExterno)wsDoc).NUD).ToList();
            //var lstIDProdsExternos = wsDocumentos.Select(wsDoc => wsDoc.EntidadesProdutoras[0].IDExterno).Distinct().ToList();
            //var lstIDIdeogsExternos = wsDocumentos.Select(wsDoc => wsDoc.Ideograficos[0].IDExterno).Distinct().ToList();
            //var lstIDOnomsExternos = wsDocumentos.Select(wsDoc => wsDoc.Onomasticos[0].IDExterno).Distinct().ToList();
            //var lstIDToposExternos = wsDocumentos.Select(wsDoc => wsDoc.Toponimias[0].IDExterno).Distinct().ToList();

            //var lstIDCAsExternos = new List<string>();
            //lstIDCAsExternos.AddRange(lstIDProdsExternos);
            //lstIDCAsExternos.AddRange(lstIDIdeogsExternos);
            //lstIDCAsExternos.AddRange(lstIDOnomsExternos);
            //lstIDCAsExternos.AddRange(lstIDToposExternos);

            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                //// Load de todas as entidades externas que vêm do webservice e já estão integradas no Gisa
                //IntGestDocRule.Current.LoadDocumentos(GisaDataSetHelper.GetInstance(), lstIDDocsExternos.ToArray(), ho.Connection);
                //IntGestDocRule.Current.LoadControloAuts(GisaDataSetHelper.GetInstance(), lstIDCAsExternos.ToArray(), ho.Connection);

                foreach (DocumentoExterno de in wsDocumentos)
                {
                    // Documento
                    var dg = this.database.GetSuggestion(de, ho.Connection);
                    var cd = new Correspondencia(de, dg);
                    this.Sugestoes.Add(cd);

                    // Entidade produtora
                    var epe = de.Produtor;
                    var sugCA = SugestoesCA.Where(sug => ((RegistoAutoridadeExterno)sug.DocumentoExterno).IDExterno == epe.IDExterno && ((RegistoAutoridadeExterno)sug.DocumentoExterno).Sistema == epe.Sistema && ((RegistoAutoridadeExterno)sug.DocumentoExterno).IDTipoNoticiaAut == epe.IDTipoNoticiaAut).SingleOrDefault();
                    if (sugCA == null)
                    {
                        dg.CorrespondenteEP = this.database.GetSuggestion(epe, ho.Connection);
                        this.SugestoesCA.Add(new Correspondencia(epe, dg.CorrespondenteEP));
                    }
                    else
                        dg.CorrespondenteEP = (RegistoAutoridadeInterno)sugCA.DocumentoGisa;
                    
                    
                    //this.Sugestoes.Add(new Correspondencia(epe, epi));

                    // Ideografico
                    RegistoAutoridadeExterno ideograficoe = de.Ideografico;
                    RegistoAutoridadeInterno ideograficoi = this.database.GetSuggestion(ideograficoe, ho.Connection);
                    dg.CorrespondenteIdeografico = ideograficoi;
                    //this.Sugestoes.Add(new Correspondencia(ideograficoe, ideograficoi));

                    // Onomastico
                    RegistoAutoridadeExterno onomasticoe = de.Onomastico;
                    RegistoAutoridadeInterno onomasticoi = this.database.GetSuggestion(onomasticoe, ho.Connection);
                    dg.CorrespondenteOnomastico = onomasticoi;
                    //this.Sugestoes.Add(new Correspondencia(onomasticoe, onomasticoi));

                    // Tipologia informacional
                    RegistoAutoridadeExterno tipologiae = de.Tipologia;
                    RegistoAutoridadeInterno tipologiai = this.database.GetSuggestion(tipologiae, ho.Connection);
                    dg.CorrespondenteTipologiaInformacional = tipologiai;
                    //this.Sugestoes.Add(new Correspondencia(tipologiae, tipologiai));

                    // Toponimia
                    RegistoAutoridadeExterno toponimiae = de.Toponimia;
                    RegistoAutoridadeInterno toponimiai = this.database.GetSuggestion(toponimiae, ho.Connection);
                    dg.CorrespondenteToponimico = toponimiai;
                    //this.Sugestoes.Add(new Correspondencia(toponimiae, toponimiai));

                }
            }
            catch (Exception) { }
            finally { ho.Dispose(); }
            */
        }

        private DateTime GetLastProcessedTimeStamp()
        {
            // TODO: implement!
            return new DateTime(2009, 10, 28);
        }
    }
}
