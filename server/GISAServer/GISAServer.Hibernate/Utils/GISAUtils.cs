using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using NHibernate;
using NHibernate.Cfg;
using log4net;

using GISAServer.Hibernate.Objects;

namespace GISAServer.Hibernate.Utils
{
    public class GISAUtils
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(GISAUtils));

        private static ISessionFactory sessionFactory;
        public static ISessionFactory SessionFactory
        {
            get { return sessionFactory; }
        }

        static GISAUtils()
        {
            sessionFactory = new Configuration().Configure().BuildSessionFactory();            
        }

        public static IList<long> getAllNivelDocumentalIds()
        {
            ISession session = null;
            IList<long> ret = new List<long>();
            try
            {
                session = sessionFactory.OpenSession();
                ret = session.CreateQuery("SELECT n.Id FROM NivelEntity AS n WHERE n.TipoNivel.Id = 3 AND n.IsDeleted = 0").List<long>();
            }
            catch (Exception) { throw; }
            finally { if (session != null) session.Close(); }

            return ret;
        }

        public static string getAllDocumentosInternetQuery = @"
CREATE TABLE #Produtores (IDNivel BIGINT PRIMARY KEY, Termos NVARCHAR(MAX));

WITH Temp (IDStart, ID, IDUpper)
AS (
    SELECT rh.ID, rh.ID, rh.IDUpper
    FROM RelacaoHierarquica rh
        {0}
    WHERE rh.IDTipoNivelRelacionado between 7 and 10 AND rh.isDeleted = 0
    
    UNION ALL
	
    SELECT Temp.IDStart, rh.ID, rh.IDUpper
    FROM RelacaoHierarquica rh
		INNER JOIN Temp ON Temp.IDUpper = rh.ID
    WHERE rh.isDeleted = 0 and rh.IDTipoNivelRelacionado >= 7
)

SELECT IDStart, ID, IDUpper
INTO #temp2
FROM Temp;

INSERT INTO #Produtores
SELECT t1.IDStart, Produtores = LEFT(o1.list, LEN(o1.list))
FROM (SELECT DISTINCT IDStart FROM #temp2) t1
	CROSS APPLY ( 
		SELECT d.Termo + ' ' AS [text()] 
		FROM Dicionario d
			INNER JOIN ControloAutDicionario cad ON cad.IDDicionario = d.ID AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0
			INNER JOIN ControloAut ca ON ca.ID = cad.IDControloAut AND ca.isDeleted = 0 AND ca.IDTipoNoticiaAut = 4 AND ca.Autorizado = 1
			INNER JOIN NivelControloAut nca ON nca.IDControloAut = ca.ID AND nca.isDeleted = 0
			INNER JOIN #temp2 t2 ON t2.IDUpper = nca.ID
		WHERE d.isDeleted = 0 AND t2.IDStart = t1.IDStart
		FOR XML PATH('') 
	) o1 (list);

SELECT n.ID, rh.Designacao TipoNivelRelacionado, n.Codigo, nd.Designacao, COALESCE(dp.InicioAno, '') InicioAno, COALESCE(dp.FimAno, '') FimAno, 
    COALESCE(dp.InicioMes, '') InicioMes, COALESCE(dp.FimMes, '') FimMes, COALESCE(dp.InicioDia, '') InicioDia, COALESCE(dp.FimDia, '') FimDia, 
	COALESCE(av.Publicar, 0) Publicar, COALESCE(ce.ConteudoInformacional, '') ConteudoInformacional,
	COALESCE(lo.TipoObra, '') TipoObra, COALESCE(lo.PHTexto, '') PHTexto,
	TermosIndexacao = LEFT(o1.list, LEN(o1.list)),
	TermosTipologia = LEFT(o2.list, LEN(o2.list)),
	Autores = LEFT(o3.list, LEN(o3.list)),
	CodigosAtestadoHabitabilidade = LEFT(o4.list, LEN(o4.list)),
	Termo_LicencaObraLocalizacaoObraActual = LEFT(o5.list, LEN(o5.list)),
	Nome_LicencaObraRequerentes = LEFT(o6.list, LEN(o6.list)),
	Termo_LicencaObraTecnicoObra = LEFT(o7.list, LEN(o7.list)),
	numObjDigital = LEFT(o8.list, LEN(o8.list)),
	COALESCE(notas.NotaGeral, '') NotaGeral,
	NomeLocal_LicencaObraLocalizacaoObraAntiga = LEFT(o9.list, LEN(o9.list)),
    COALESCE(#Produtores.Termos, '') Produtores,
    IdsControlosAutoridade = LEFT(o10.list, LEN(o10.list)),
    LocObraNumPoliciaAnt = LEFT(o11.list, LEN(o11.list)),
    LocObraNumPoliciaAct = LEFT(o12.list, LEN(o12.list)),
    IDUpper = LEFT(o13.list, LEN(o13.list))
FROM Nivel n
     {1}
	INNER JOIN (
		SELECT DISTINCT rh.ID, tnr.Designacao
		FROM RelacaoHierarquica rh
            {0}
			INNER JOIN TipoNivelRelacionado tnr ON tnr.ID = rh.IDTipoNivelRelacionado AND tnr.isDeleted = 0
		WHERE rh.isDeleted = 0
	) rh ON rh.ID = n.ID
	INNER JOIN NivelDesignado nd ON nd.ID = n.ID AND nd.isDeleted = 0
	INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
    LEFT JOIN #Produtores ON #Produtores.IDNivel = n.ID
	LEFT JOIN SFRDAvaliacao av ON av.IDFRDBase = frd.ID AND av.isDeleted = 0
	LEFT JOIN SFRDConteudoEEstrutura ce ON ce.IDFRDBase = frd.ID AND ce.isDeleted = 0
	LEFT JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frd.ID AND dp.isDeleted = 0
	LEFT JOIN LicencaObra lo ON lo.IDFRDBase = frd.ID AND lo.isDeleted = 0
	LEFT JOIN SFRDNotaGeral notas ON notas.IDFRDBase = frd.ID AND notas.isDeleted = 0
	-- indexação
	CROSS APPLY ( 
		SELECT Termo + ' ' AS [text()] 
		FROM Dicionario d
			INNER JOIN ControloAutDicionario cad ON cad.IDDicionario = d.ID AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0
			INNER JOIN ControloAut ca ON ca.ID = cad.IDControloAut AND ca.isDeleted = 0 AND ca.Autorizado = 1 AND (ca.IDTipoNoticiaAut = 1 OR ca.IDTipoNoticiaAut = 2 OR ca.IDTipoNoticiaAut = 3)
			INNER JOIN IndexFRDCA idx ON idx.IDControloAut = ca.ID AND idx.isDeleted = 0
		WHERE d.isDeleted = 0 AND idx.IDFRDBase = frd.ID
		FOR XML PATH('') 
	) o1 (list)
	-- tipologia
	CROSS APPLY ( 
		SELECT Termo + ' ' AS [text()] 
		FROM Dicionario d
			INNER JOIN ControloAutDicionario cad ON cad.IDDicionario = d.ID AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0
			INNER JOIN ControloAut ca ON ca.ID = cad.IDControloAut AND ca.isDeleted = 0 AND ca.Autorizado = 1 AND ca.IDTipoNoticiaAut = 5
			INNER JOIN IndexFRDCA idx ON idx.IDControloAut = ca.ID AND idx.isDeleted = 0
		WHERE d.isDeleted = 0 AND idx.IDFRDBase = frd.ID
		FOR XML PATH('') 
	) o2 (list)
	-- autores
	CROSS APPLY ( 
		SELECT Termo + ' ' AS [text()] 
		FROM Dicionario d
			INNER JOIN ControloAutDicionario cad ON cad.IDDicionario = d.ID AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0
			INNER JOIN ControloAut ca ON ca.ID = cad.IDControloAut AND ca.isDeleted = 0 AND ca.Autorizado = 1
			INNER JOIN SFRDAutor aut ON aut.IDControloAut = ca.ID AND aut.isDeleted = 0
		WHERE d.isDeleted = 0 AND aut.IDFRDBase = frd.ID
		FOR XML PATH('') 
	) o3 (list)
	-- Codigos Atestado Habitabilidade 
	CROSS APPLY ( 
		SELECT Codigo + ' ' AS [text()] 
		FROM LicencaObraAtestadoHabitabilidade loah
		WHERE loah.isDeleted = 0 AND loah.IDFRDBase = lo.IDFRDBase
		FOR XML PATH('') 
	) o4 (list)
	-- Termo_LicencaObraLocalizacaoObraActual
	CROSS APPLY ( 
		SELECT Termo + ' ' AS [text()] 
		FROM Dicionario d
			INNER JOIN ControloAutDicionario cad ON cad.IDDicionario = d.ID AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0
			INNER JOIN ControloAut ca ON ca.ID = cad.IDControloAut AND ca.isDeleted = 0 AND ca.Autorizado = 1
			INNER JOIN LicencaObraLocalizacaoObraActual loloa ON loloa.IDControloAut = ca.ID AND loloa.isDeleted = 0
		WHERE d.isDeleted = 0 AND loloa.IDFRDBase = lo.IDFRDBase
		FOR XML PATH('') 
	) o5 (list)
	-- Nome_LicencaObraRequerentes 
	CROSS APPLY ( 
		SELECT Nome + ' ' AS [text()] 
		FROM LicencaObraRequerentes lor
		WHERE lor.isDeleted = 0 AND lor.IDFRDBase = lo.IDFRDBase
		FOR XML PATH('') 
	) o6 (list)
	-- Termo_LicencaObraTecnicoObra
	CROSS APPLY ( 
		SELECT Termo + ' ' AS [text()] 
		FROM Dicionario d
			INNER JOIN ControloAutDicionario cad ON cad.IDDicionario = d.ID AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0
			INNER JOIN ControloAut ca ON ca.ID = cad.IDControloAut AND ca.isDeleted = 0 AND ca.Autorizado = 1
			INNER JOIN LicencaObraTecnicoObra loto ON loto.IDControloAut = ca.ID AND loto.isDeleted = 0
		WHERE d.isDeleted = 0 AND loto.IDFRDBase = lo.IDFRDBase
		FOR XML PATH('') 
	) o7 (list)
	-- número de objetos digitais
	CROSS APPLY ( 
		SELECT COUNT(img.idx) + ' ' AS [text()] 
		FROM SFRDImagem img
            LEFT JOIN SFRDImagemObjetoDigital imgOD ON imgOD.IDFRDBase = img.IDFRDBase AND imgOD.idx = img.idx AND imgOD.isDeleted = 0
            LEFT JOIN ObjetoDigital od ON od.ID = imgOD.IDObjetoDigital AND od.isDeleted = 0
		WHERE img.isDeleted = 0 AND img.IDFRDBase = frd.ID AND (img.Tipo = 'Web' OR (img.Tipo = 'Fedora' AND od.Publicado = 1))
		FOR XML PATH('') 
	) o8 (list)
	-- NomeLocal_LicencaObraLocalizacaoObraAntiga
	CROSS APPLY ( 
		SELECT NomeLocal + ' ' AS [text()] 
		FROM LicencaObraLocalizacaoObraAntiga loloa
		WHERE loloa.isDeleted = 0 AND loloa.IDFRDBase = lo.IDFRDBase
		FOR XML PATH('') 
	) o9 (list)
    -- Ids dos controlos de autoridade, autorizados, directamente relacionados
	CROSS APPLY ( 
		SELECT convert(nvarchar(16), ID) + ' ' AS [text()]
        FROM (
            SELECT ca.ID
		    FROM ControloAut ca
			    INNER JOIN IndexFRDCA idx ON idx.IDControloAut = ca.ID AND idx.isDeleted = 0
		    WHERE ca.isDeleted = 0 AND ca.Autorizado = 1 AND idx.IDFRDBase = frd.ID
            UNION
            SELECT ca.ID
		    FROM ControloAut ca
			    INNER JOIN SFRDAutor aut ON aut.IDControloAut = ca.ID AND aut.isDeleted = 0
		    WHERE ca.isDeleted = 0 AND ca.Autorizado = 1 AND aut.IDFRDBase = frd.ID
            UNION
            SELECT ca.ID
		    FROM ControloAut ca
			    INNER JOIN NivelControloAut nca ON nca.IDControloAut = ca.ID AND nca.isDeleted = 0
                INNER JOIN Nivel n ON n.ID = nca.ID AND n.IDTipoNivel = 2 AND n.isDeleted = 0
                INNER JOIN RelacaoHierarquica rh ON rh.IDUpper = n.ID AND rh.isDeleted = 0
                INNER JOIN Nivel nDoc on nDoc.ID = rh.ID AND nDoc.IDTipoNivel = 3 AND nDoc.isDeleted = 0
                INNER JOIN FRDBase frdDoc ON frdDoc.IDNivel = nDoc.ID AND frdDoc.IDTipoFRDBase = 1 AND frdDoc.isDeleted = 0
		    WHERE ca.isDeleted = 0 AND ca.Autorizado = 1 AND frdDoc.ID = frd.ID
            UNION
            SELECT loa.IDControloAut
            FROM LicencaObraLocalizacaoObraActual loa
            WHERE loa.isDeleted = 0 AND loa.IDFRDBase = frd.ID
            UNION
            SELECT loto.IDControloAut
            FROM LicencaObraTecnicoObra loto
            WHERE loto.isDeleted = 0 AND loto.IDFRDBase = frd.ID
        ) cas
		FOR XML PATH('') 
	) o10 (list)
    CROSS APPLY (
	    SELECT NumPolicia + ' ' AS [text()]  
	    FROM LicencaObraLocalizacaoObraAntiga loa
	    WHERE loa.isDeleted = 0 AND lo.IDFRDBase = loa.IDFRDBase AND NOT NumPolicia is null AND LEN(NumPolicia) <> 0
	    FOR XML PATH('') 
    ) o11 (list)
    CROSS APPLY (
	    SELECT NumPolicia + ' ' AS [text()]
	    FROM LicencaObraLocalizacaoObraActual loa
	    WHERE loa.isDeleted = 0 AND frd.ID = loa.IDFRDBase AND NOT NumPolicia is null AND LEN(NumPolicia) <> 0
	    FOR XML PATH('') 
    ) o12 (list)
    CROSS APPLY (
	    SELECT IDUpper + ' ' AS [text()]
	    FROM RelacaoHierarquica rh
            {0}
	    WHERE rh.isDeleted = 0 AND n.ID = rh.ID AND rh.IDTipoNivelRelacionado > 7 
	    FOR XML PATH('') 
    ) o13 (list)
WHERE n.IDTipoNivel = 3 AND n.isDeleted = 0;

DROP TABLE #temp;
DROP TABLE #Produtores;";

        public static Dictionary<long, NivelDocumentalInternet> DocumentosInternet = null;
        public static IList<long> getAllNivelDocumentalInternetIds()
        {
            return GetIdsNiveisDocumentaisInternet(null);
        }

        public static List<long> GetIdsNiveisDocumentaisInternet(List<string> idsNivel)
        {
            ISession session = null;
            DocumentosInternet = new Dictionary<long, NivelDocumentalInternet>();
            
            try
            {
                session = GISAUtils.SessionFactory.OpenSession();
                var query = string.Empty;

                if (idsNivel == null)
                {
                    query = string.Format(getAllDocumentosInternetQuery, "", "");
                    var docs = session.CreateSQLQuery(query);
                    docs.SetTimeout(1000);

                    foreach (var res in docs.List())
                        ParseResults(res as object[]);

                    docs = null;
                }
                else
                {
                    ImportIDs(idsNivel.ToArray(), (SqlConnection)session.Connection);

                    query = string.Format(getAllDocumentosInternetQuery,
                        " INNER JOIN #temp ON #temp.ID = rh.ID ",
                        " INNER JOIN #temp ON #temp.ID = n.ID ");

                    var docs = session.CreateSQLQuery(query);
                    docs.SetTimeout(1000);

                    foreach (var res in docs.List())
                        ParseResults(res as object[]);

                    docs = null;
                }
            }
            catch (Exception) { throw; }
            finally
            {
                if (session != null) session.Close();
            }

            return DocumentosInternet.Keys.ToList();
        }

        private static void ParseResults(object[] r)
        {
            var doc = new NivelDocumentalInternet();
            doc.Id = r[0].ToString();
            doc.DesignacaoTipoNivelRelacionado = r[1].ToString();
            doc.Codigo = r[2].ToString();
            doc.DesignacaoNivelDesignado = r[3].ToString();
            doc.InicioAno = r[4].ToString();
            doc.FimAno = r[5].ToString();
            doc.InicioMes = r[6].ToString();
            doc.FimMes = r[7].ToString();
            doc.InicioDia = r[8].ToString();
            doc.FimDia = r[9].ToString();
            doc.DataInicioProd = GISAUtils.DataInicioProdFormatada(doc.InicioAno, doc.InicioMes, doc.InicioDia);
            doc.DataFimProd = GISAUtils.DataFimProdFormatada(doc.FimAno, doc.FimMes, doc.FimDia);
            doc.Publicar = r[10].ToString().Equals("1") ? "sim" : "nao";
            doc.ConteudoInformacional = r[11].ToString();
            doc.TipoObra = r[12].ToString();
            doc.PHTexto = r[13].ToString();
            doc.TermosDeIndexacao = r[14] != null ? r[14].ToString() : "";
            doc.TipologiaInformacional = r[15] != null ? r[15].ToString() : "";
            doc.Autor = r[16] != null ? r[16].ToString() : "";
            doc.CodigosAtestadoHabitabilidade = r[17] != null ? r[17].ToString() : "";
            doc.Termo_LicencaObraLocalizacaoObraActual = r[18] != null ? r[18].ToString() : "";
            doc.Nome_LicencaObraRequerentes = r[19] != null ? r[19].ToString() : "";
            doc.Termo_LicencaObraTecnicoObra = r[20] != null ? r[20].ToString() : "";
            int numObjetos = 0;
            int.TryParse(r[21].ToString(), out numObjetos);
            doc.NumImagens = numObjetos > 0 ? "sim" : "nao";
            doc.NotaGeral = r[22].ToString();
            doc.NomeLocal_LicencaObraLocalizacaoObraAntiga = r[23] != null ? r[23].ToString() : "";
            doc.EntidadeProdutora = r[24].ToString();
            doc.IdsControlosAutoridade = r[25] != null ? r[25].ToString() : "";
            doc.NumPolicia_LicencaObraLocalizacaoObraAntiga = r[26] != null ? r[26].ToString() : "";
            doc.NumPolicia_LicencaObraLocalizacaoObraActual = r[27] != null ? r[27].ToString() : "";
            doc.IdUpper = r[28] != null ? r[28].ToString() : "";
            DocumentosInternet[System.Convert.ToInt64(doc.Id)] = doc;
        }

        public static Dictionary<long, SPGetDocumentosComProdutores> DocumentosComProdutores = null;
        public static IList<long> getAllNivelDocumentalComProdutoresIds()
        {
            return GetIdsNiveisDocumentaisComProdutores(null);
        }

        public static List<long> GetIdsNiveisDocumentaisComProdutores(long? idProdutor)
        {
            var ticks = DateTime.Now.Ticks;

            ISession session = null;
            try
            {
                session = GISAUtils.SessionFactory.OpenSession();
                IQuery gdcp = session.GetNamedQuery("sp_getDocumentosComProdutores");
                gdcp.SetTimeout(2000);
                gdcp.SetParameter("ProdutorId", idProdutor, NHibernateUtil.Int64);
                DocumentosComProdutores = gdcp.List<SPGetDocumentosComProdutores>().ToDictionary(v => v.IDDocumento, v => v);
            }
            catch (Exception) { throw; }
            finally
            {
                if (session != null) session.Close();
            }

            log.Debug("GetIdsNiveisDocumentaisComProdutores (" + (idProdutor.HasValue ? idProdutor.Value.ToString() : "").ToString() + "): " + new TimeSpan(DateTime.Now.Ticks - ticks).ToString());

            return DocumentosComProdutores.Keys.ToList();
        }

        public static string getAllUnidadesFisicasQuery = @"
            select n.ID id, n.Codigo numero, nd.Designacao designacao, coalesce(nuf.CodigoBarras,'') codigoBarras, coalesce(nuf.GuiaIncorporacao,'') guiaIncorporacao, coalesce(nuf.Eliminado, 0) eliminado, coalesce(ct.Cota,'') cota,
                coalesce(dt.InicioAno,'') inicioAno, coalesce(dt.InicioMes,'') inicioMes, coalesce(dt.InicioDia,'') inicioDia, coalesce(dt.FimAno,'') fimAno, coalesce(dt.FimMes,'') fimMes, coalesce(dt.FimDia,'') fimDia, 
                coalesce(ce.ConteudoInformacional,'') conteudoInformacional, coalesce(ta.Designacao,'') tipoDesignacao
            from Nivel n
                inner join NivelDesignado nd on nd.ID = n.ID and nd.isDeleted = 0
                left join NivelUnidadeFisica nuf on nuf.ID = nd.ID and nuf.isDeleted = 0
                left join FRDBase frd on frd.IDNivel = n.ID and frd.isDeleted = 0
                left join SFRDUFCota ct on ct.IDFRDBase = frd.ID and ct.isDeleted = 0
                left join SFRDDatasProducao dt on dt.IDFRDBase = frd.ID and dt.isDeleted = 0
                left join SFRDConteudoEEstrutura ce on ce.IDFRDBase = frd.ID and ce.isDeleted = 0
                left join SFRDUFDescricaoFisica df on df.IDFRDBase = frd.ID and df.isDeleted = 0
                left join TipoAcondicionamento ta on ta.ID = df.IDTipoAcondicionamento and ta.isDeleted = 0
            where n.IDTipoNivel = 4 and n.isDeleted = 0";

        public static List<UnidadeFisica> getAllUnidadesFisicas()
        {
            var ret = new List<UnidadeFisica>();
            ISession session = null;
            var doc = default(UnidadeFisica);
            try
            {

                session = GISAUtils.SessionFactory.OpenSession();
                var ufs = session.CreateSQLQuery(getAllUnidadesFisicasQuery);
                ufs.SetTimeout(1000);

                foreach (var ufa in ufs.List())
                {
                    var uf = ufa as object[];
                    doc = new UnidadeFisica();
                    doc.Id = uf[0].ToString();
                    doc.Numero = uf[1].ToString();
                    doc.Designacao = uf[2].ToString();
                    doc.CodigoBarras = uf[3].ToString();
                    doc.GuiaIncorporacao = uf[4].ToString();
                    doc.Eliminado = uf[5].ToString().Equals("1") ? "Sim" : "Não";
                    doc.Cota = uf[6].ToString();
                    doc.DataInicioProd = GISAUtils.DataInicioProdFormatada(uf[7].ToString(), uf[8].ToString(), uf[9].ToString());
                    doc.DataFimProd = GISAUtils.DataInicioProdFormatada(uf[10].ToString(), uf[11].ToString(), uf[12].ToString());
                    doc.ConteudoInformacional = uf[13].ToString();
                    doc.TipoUnidadeFisica = uf[14].ToString();
                    ret.Add(doc);
                }
                ufs = null;
                session.Close();
            }
            catch (Exception) { throw; }
            finally
            {
                if (session != null) session.Close();
            }
            return ret;
        }

        public static IList<long> getAllUnidadesFisicasIds()
        {
            ISession session = null;
            IList<long> ret = new List<long>();
            try
            {
                session = sessionFactory.OpenSession();
                ret = session.CreateQuery("SELECT n.Id FROM NivelEntity AS n WHERE n.TipoNivel.Id = 4 AND n.IsDeleted = 0").List<long>();
            }
            catch (Exception) { throw; }
            finally { if (session != null) session.Close(); }

            return ret;
        }

        public static IList<long> getAllProdutoresIds()
        {
            ISession session = null;
            IList<long> ret = new List<long>();
            try
            {
                session = sessionFactory.OpenSession();
                ret = session.CreateQuery("SELECT controloAut.Id FROM ControloAutEntity AS controloAut WHERE controloAut.TipoNoticiaAut.Id = 4 AND controloAut.IsDeleted = 0").List<long>();
            }
            catch (Exception) { throw; }
            finally { if (session != null) session.Close(); }

            return ret;
        }

        public static IList<long> getAllAssuntosIds()
        {
            ISession session = null;
            IList<long> ret = new List<long>();
            try
            {
                session = sessionFactory.OpenSession();
                ret = session.CreateQuery("SELECT ca.Id FROM ControloAutEntity AS ca WHERE ca.TipoNoticiaAut.Id in (1,2,3) AND ca.IsDeleted = 0").List<long>();
            }
            catch (Exception) { throw; }
            finally { if (session != null) session.Close(); }

            return ret;
        }

        public static IList<long> getAllTipologiasIds()
        {
            ISession session = null;
            IList<long> ret = new List<long>();
            try
            {
                session = sessionFactory.OpenSession();
                ret = session.CreateQuery("SELECT ca.Id FROM ControloAutEntity AS ca WHERE ca.TipoNoticiaAut.Id = 5 AND ca.IsDeleted = 0").List<long>();
            }
            catch (Exception) { throw; }
            finally { if (session != null) session.Close(); }

            return ret;
        }



        public static string buildOperatorSearchString(ref string searchText)
        {
            Match operadorMatch = Regex.Match(searchText, @"(AND|OR)? *operador\ *:\ *(\(([^\.]+)\)|([^ ^\.]+))");

            // Exemplo: "(name = 'TODOS' OR (name = 'xpt' OR name = 'xptz' AND name = 'TODOS'))"
            string operador = "";

            if (operadorMatch.Success)
            {
                operador = operadorMatch.ToString();
                searchText = searchText.Replace(operador, "");
                operador = operador.Split(':')[1];
                if (operador.ToUpper().Contains(" AND ") || operador.ToUpper().Contains(" OR "))
                {
                    Regex regex = new Regex(@"(\w+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    operador = regex.Replace(operador, delegate(Match m) { return (m.Value.ToUpper() != "AND" && m.Value.ToUpper() != "OR") ? "t.Name = '" + m.Value + "'" : m.Value; });
                }
                else
                {
                    operador = string.Format(" t.Name = '{0}'", operador);
                }

            }
            return operador;
        }


        public static string buildDataInicialDataFinalSearchString(string searchText, out DateTime? inicio, out DateTime? fim)
        {
            inicio = null;
            fim = null;

            Match dataEdicaoMatch = Regex.Match(searchText, @"(AND|OR)? *dataEdicao *: *\[ *(?<inicioAno>\d{4})(?<inicioMes>\d{2})(?<inicioDia>\d{2}) (to|To|tO|TO) (?<fimAno>\d{4})(?<fimMes>\d{2})(?<fimDia>\d{2}) *\]");
            if (dataEdicaoMatch.Success)
            {
                searchText = searchText.Replace(dataEdicaoMatch.ToString(), "");
                inicio = new DateTime(int.Parse(dataEdicaoMatch.Groups["inicioAno"].Value), int.Parse(dataEdicaoMatch.Groups["inicioMes"].Value), int.Parse(dataEdicaoMatch.Groups["inicioDia"].Value));
                fim = new DateTime(int.Parse(dataEdicaoMatch.Groups["fimAno"].Value), int.Parse(dataEdicaoMatch.Groups["fimMes"].Value), int.Parse(dataEdicaoMatch.Groups["fimDia"].Value));
            }

            searchText = searchText.Trim();
            if (searchText.StartsWith("AND", StringComparison.OrdinalIgnoreCase))
            {
                searchText = searchText.Remove(0, 3);
            }
            if (searchText.StartsWith("OR", StringComparison.OrdinalIgnoreCase))
            {
                searchText = searchText.Remove(0, 2);
            }
            return searchText;
        }



        public static IList<long> GetTrusteeUsersIds(string operador)
        {
            ISession session = null;
            IList<long> ret = new List<long>();
            try
            {
                session = GISAUtils.SessionFactory.OpenSession();
                IQuery q = session.CreateQuery("FROM TrusteeEntity t WHERE " + operador);
                q.SetTimeout(1000);

                foreach (TrusteeEntity t in q.Enumerable<TrusteeEntity>())
                {
                    if (t.CatCode.Equals("GRP"))
                    {
                        IQuery qGRP = session.CreateQuery("FROM UserGroupsEntity ug WHERE ug.Group.Id = " + t.Id);
                        qGRP.SetTimeout(1000);
                        foreach (UserGroupsEntity ug in qGRP.Enumerable<UserGroupsEntity>())
                        {
                            ret.Add(ug.User.Id);
                        }
                    }
                    else
                    {
                        ret.Add(t.Id);
                    }

                }
            }
            catch (Exception) { throw; }
            finally
            {
                if (session != null) session.Close();
            }

            return ret;
        }

        public static List<string> GetNivelIds(string operador, DateTime? inicio, DateTime? fim)
        {
            if ((operador == null || operador.Length == 0) && inicio == null && fim == null)
                return null;

            string qOperador = string.Empty;
            if (operador != null && operador.Length > 0)
            {
                IList<long> trusteeUsers = GetTrusteeUsersIds(operador);
                if (trusteeUsers.Count > 0)
                {
                    // Se não existir operador, não devolve resultados  
                    return new List<string>();
                }

                string[] ids = new string[trusteeUsers.Count];
                for (int i = 0; i < ids.Length; i++)
                {
                    ids[i] = trusteeUsers[i].ToString();
                }
                qOperador = "fddd.TrusteeOperator.ID in (" + string.Join(",", ids) + ") AND";
            }

            // NOTA: fddd.FRDBase.TipoFRDBase.Id = 2 : Unidade Física
            string query = string.Format("select distinct fddd.FRDBase.Nivel.Id from FRDBaseDataDeDescricaoEntity fddd where {0} fddd.IsDeleted = 0 AND fddd.FRDBase.TipoFRDBase.Id = 2", qOperador);

            if (inicio != null)
            {
                query += " AND fddd.DataEdicao >= :dataInicial";
            }

            if (fim != null)
            {
                query += " AND fddd.DataEdicao <= :dataFinal";
            }

            ISession session = null;
            List<string> ret;
            try
            {
                session = GISAUtils.SessionFactory.OpenSession();
                IQuery q = session.CreateQuery(query);
                q.SetTimeout(1000);

                if (inicio != null)
                    q.SetDateTime("dataInicial", (DateTime)inicio);

                if (fim != null)
                    q.SetDateTime("dataFinal", (DateTime)fim);

                ret = q.List<string>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (session != null) session.Close();
            }            
            
            return ret;

        }

        // Exemplo: 'id:(100 OR 200 OR 300)'
        public static string buildList_Id_OR_Id(string searchText, List<string> nivelIds)
        {
            StringBuilder newSearchString = new StringBuilder();
            newSearchString.Append(searchText);

            newSearchString.Append(" id:(");
            for (int i = 0; i < nivelIds.Count; i++)
            {
                if (i != 0)
                {
                    newSearchString.Append(" OR ");
                }
                newSearchString.Append(nivelIds[i]);
            }
            newSearchString.Append(")");
            string a = newSearchString.ToString();
            return a;
        }

        public static string DataInicioProdFormatada(SFRDDatasProducaoEntity sfrddp)
        {
            if (sfrddp != null && !sfrddp.IsDeleted)
            {
                int inicioAno = 1, inicioMes = 1, inicioDia = 1;
                int.TryParse(sfrddp.InicioAno, out inicioAno);
                int.TryParse(sfrddp.InicioMes, out inicioMes);
                int.TryParse(sfrddp.InicioDia, out inicioDia);

                // Se o valor da string for empty, devolve 0.
                // Como queremos que no minimo seja um, temos de fazer os testes

                if (inicioAno < 1)
                    inicioAno = 1;

                if (inicioMes < 1)
                    inicioMes = 1;

                if (inicioDia < 1)
                    inicioDia = 1;

                return string.Format("{0:0000}{1:00}{2:00}", inicioAno, inicioMes, inicioDia);
            }
            else
                return "00000101";
        }

        public static string DataFimProdFormatada(SFRDDatasProducaoEntity sfrddp)
        {
            if (sfrddp != null && !sfrddp.IsDeleted)
            {
                int fimAno = 9999, fimMes = 12, fimDia = 31;
                int.TryParse(sfrddp.FimAno, out fimAno);
                int.TryParse(sfrddp.FimMes, out fimMes);
                int.TryParse(sfrddp.FimDia, out fimDia);

                // Fazer os testes para o máximo permitido
                // No ano, no mês e no dia

                if (fimAno > 9999)
                    fimAno = 9999;

                if (fimMes > 12)
                    fimMes = 12;

                if (fimDia > 31)
                    fimDia = 31;

                return string.Format("{0:0000}{1:00}{2:00}", Math.Abs(fimAno), Math.Abs(fimMes), Math.Abs(fimDia));
            }
            else
                return "99991231";
        }

        public static string DataInicioProdFormatada(string iniAno, string iniMes, string iniDia)
        {
            if (iniAno.Length > 0 || iniMes.Length > 0 || iniDia.Length > 0)
            {
                int inicioAno = 1, inicioMes = 1, inicioDia = 1;
                int.TryParse(iniAno, out inicioAno);
                int.TryParse(iniMes, out inicioMes);
                int.TryParse(iniDia, out inicioDia);

                // Se o valor da string for empty, devolve 0.
                // Como queremos que no minimo seja um, temos de fazer os testes

                if (inicioAno < 1)
                {
                    inicioAno = 1;
                }

                if (inicioMes < 1)
                {
                    inicioMes = 1;
                }

                if (inicioDia < 1)
                {
                    inicioDia = 1;
                }

                return string.Format("{0:0000}{1:00}{2:00}", inicioAno, inicioMes, inicioDia);
            }
            else
            {
                return "00000101";
            }
        }

        public static string DataFimProdFormatada(string fAno, string fMes, string fDia)
        {
            if (fAno.Length > 0 || fMes.Length > 0 || fDia.Length > 0)
            {
                int fimAno = 9999, fimMes = 12, fimDia = 31;
                int.TryParse(fAno, out fimAno);
                int.TryParse(fMes, out fimMes);
                int.TryParse(fDia, out fimDia);

                // Fazer os testes para o máximo permitido
                // No ano, no mês e no dia

                if (fimAno > 9999)
                {
                    fimAno = 9999;
                }

                if (fimMes > 12)
                {
                    fimMes = 12;
                }

                if (fimDia > 31)
                {
                    fimDia = 31;
                }

                return string.Format("{0:0000}{1:00}{2:00}", Math.Abs(fimAno), Math.Abs(fimMes), Math.Abs(fimDia));
            }
            else
            {
                return "99991231";
            }
        }

        public static void ImportIDs(string[] IDs, SqlConnection conn)
        {
            long start = DateTime.Now.Ticks;

            DataTable t = new DataTable();
            DataColumn c = new DataColumn("ID", typeof(long));
            t.Columns.Add(c);
            DataColumn c2 = new DataColumn("seq_nr", typeof(long));
            c2.AutoIncrementSeed = 1;
            c2.AutoIncrementStep = 1;
            c2.AutoIncrement = true;
            t.Columns.Add(c2);

            foreach (string id in IDs)
            {
                DataRow dr = t.NewRow();
                dr[0] = id;
                t.Rows.Add(dr);
            }

            var command = new SqlCommand(string.Empty, conn);
            command.CommandText = "IF OBJECT_ID(N'tempdb..#temp', N'U') IS NOT NULL " +
                                    "DROP TABLE #temp " +
                                    "CREATE TABLE #temp(ID BIGINT, seq_nr BIGINT); CREATE INDEX ix ON #temp (ID);";
            command.ExecuteNonQuery();

            SqlBulkCopy copy = new SqlBulkCopy(command.Connection, SqlBulkCopyOptions.UseInternalTransaction, null);
            copy.DestinationTableName = "#temp";
            copy.WriteToServer(t);

            t.Dispose();
        }
    }
}
