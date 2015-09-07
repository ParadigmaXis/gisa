using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

namespace DBAbstractDataLayer.DataAccessRules
{
    public abstract class EstatisticasRule : DALRule
    {
        private static EstatisticasRule current = null;
        public static void ClearCurrent()
        {
            current = null;
        }
        public static EstatisticasRule Current
        {
            get
            {
                if (Object.ReferenceEquals(null, current))
                {
                    current = (EstatisticasRule)Create(typeof(EstatisticasRule));
                }
                return current;
            }
        }

        public class TotalTipo
        {
            public long ID;
            public string Designacao;
            public long Contador;
            public long Contador_Editadas = -1;
            public long Contador_Eliminadas = -1;
            public override string ToString()
            {
                return this.Designacao;
            }
        }

        /**
         * Assume que o result set em IDataReader reader contem na coluna 0 um ID (long), na coluna 1 a designacao (string),
         * na coluna 2 um valor (long) e, opcionalmente, na coluna 3 um outro valor (long).
         * 
         * Se existirem mais colunas, estas sao ignoradas.
         */
        protected List<TotalTipo> GetTotais(IDataReader reader) {
            List<TotalTipo> results = new List<TotalTipo>();
            TotalTipo tt;
            long total = 0;
            long total_editadas = 0;
            long total_eliminadas = 0;
            do {
                while (reader.Read()) {
                    tt = new TotalTipo();
                    tt.ID = System.Convert.ToInt64(reader.GetValue(0));
                    tt.Designacao = reader.GetValue(1).ToString();
                    tt.Contador = System.Convert.ToInt64(reader.GetValue(2));

                    // Quarta coluna (se existir) contem o valor de 'Editadas':
                    if (reader.FieldCount > 3) {
                        tt.Contador_Editadas = System.Convert.ToInt64(reader.GetValue(3));
                        total_editadas += tt.Contador_Editadas;
                        tt.Contador_Eliminadas = System.Convert.ToInt64(reader.GetValue(4));
                        total_eliminadas += tt.Contador_Eliminadas;
                    }
                    results.Add(tt);
                    total += tt.Contador;
                }
            } while (reader.NextResult());

            tt = new TotalTipo();
            tt.ID = -1;
            tt.Designacao = "Total";
            tt.Contador = total;
            tt.Contador_Editadas = total_editadas;
            tt.Contador_Eliminadas = total_eliminadas;
            results.Add(tt);
            return results;
        }

        #region "Periodo de Tempo"

        public List<TotalTipo> GetTotalCriacoesUAPorOper(long idTipoNivel, DateTime dataInicio, DateTime dataFim, bool excludeImport, bool sobrePesquisa, IDbConnection conn) {
            return GetTotal_NiveisPorOperador(idTipoNivel, false, dataInicio, dataFim, excludeImport, sobrePesquisa, conn);
        }

        public List<TotalTipo> GetTotalCriacoesUFPorOper(DateTime dataInicio, DateTime dataFim, bool excludeImport, bool sobrePesquisa, IDbConnection conn) {
            return GetTotal_NiveisPorOperador(-1, true, dataInicio, dataFim, excludeImport, sobrePesquisa, conn);
        }

        public string BuildSQLQuery_GetTotal_NiveisPorOperador(bool unidadesFisicasOnly, long idTipoNivel, bool excluedImport, bool sobrePesquisa, string innerDateWhereClause) {
            string innerJoinPesquisa = string.Empty;
            string filtro_IDTipoNivelRelacionado = string.Empty;

#if DEBUG
            if (sobrePesquisa) throw new NotImplementedException("Estatistica sobre resultados de pesquisa");
#endif

            if (unidadesFisicasOnly)
                //filtro_IDTipoNivelRelacionado = "n.IDTipoNivel = 11 ";
                filtro_IDTipoNivelRelacionado = " IDTipoNivelRelacionado = 11 ";
            else {
                if (idTipoNivel > 0) 
                    //filtro_IDTipoNivelRelacionado = string.Format(" rh.IDTipoNivelRelacionado = {0} ", idTipoNivel);
                    filtro_IDTipoNivelRelacionado = build_Condicao_IDTipoNivelRelacionado(idTipoNivel);
                else
                    // NOTA: rh.IDTipoNivelRelacionado < 11 : todas excepto as unidades fisicas
                    filtro_IDTipoNivelRelacionado = " IDTipoNivelRelacionado < 11 ";
            }

            string sqlText = string.Format(
                "SELECT t.ID, t.Name, COALESCE(ddd1.countCriacoes, 0) CountCriacoes, COALESCE(ddd2.sumEdicoes, 0) CountEdicoes, COALESCE(ddd3.countEliminacoes, 0) CountEliminacoes " +
                " FROM Trustee t " +
                    " LEFT JOIN ( " +
                        " SELECT ddd.IDTrusteeOperator, COUNT(ddd.IDFRDBase) countCriacoes " +
                        " FROM FRDBaseDataDeDescricao ddd " +
                            " INNER JOIN ( " +
                                " SELECT IDFRDBase, IDTipoNivelRelacionado, MIN(DataEdicao) DataEdicao " +
                                " FROM FRDBaseDataDeDescricao " +
                                " WHERE isDeleted = 0 AND " + filtro_IDTipoNivelRelacionado +
                                " GROUP BY IDFRDBase, IDTipoNivelRelacionado " +
                                " HAVING COUNT(IDFRDBase) > 1 " +
                                " UNION " +
                                " SELECT ddd.IDFRDBase, ddd.IDTipoNivelRelacionado, MIN(ddd.DataEdicao) DataEdicao " +
                                " FROM FRDBaseDataDeDescricao ddd " +
                                    " INNER JOIN FRDBase frd ON frd.ID = ddd.IDFRDBase AND frd.isDeleted = 0 " +
                                " WHERE ddd.isDeleted = 0 AND " + filtro_IDTipoNivelRelacionado +
                                " GROUP BY ddd.IDFRDBase, ddd.IDTipoNivelRelacionado " +
                                " HAVING COUNT(ddd.IDFRDBase) = 1 " +
                            " ) dd ON dd.IDFRDBase = ddd.IDFRDBase AND dd.IDTipoNivelRelacionado = ddd.IDTipoNivelRelacionado AND dd.DataEdicao = ddd.DataEdicao " +
                            "{0} " +
                            "WHERE ddd.isDeleted = 0 {1} {2} " +
                        " GROUP BY ddd.IDTrusteeOperator " +
                    " ) ddd1 ON ddd1.IDTrusteeOperator = t.ID " +
                    " LEFT JOIN ( " +
                        " SELECT IDTrusteeOperator, SUM(ddd.CountEdicoes) sumEdicoes " +
                        " FROM ( " +
                            " SELECT dd.IDFRDBase, dd.IDTrusteeOperator, COUNT(dd.DataEdicao) CountEdicoes " +
                            " FROM ( " +
                                "SELECT ddd.IDFRDBase, ddd.IDTrusteeOperator, ddd.DataEdicao, ddd.isDeleted " +
			                    "FROM FRDBaseDataDeDescricao ddd " +  
			                        "LEFT JOIN ( " +
                                        "SELECT IDFRDBase, IDTipoNivelRelacionado, MIN(DataEdicao) DataEdicao " +
			                            "FROM FRDBaseDataDeDescricao " +
                                        "WHERE isDeleted = 0 AND " + filtro_IDTipoNivelRelacionado +
                                        "GROUP BY IDFRDBase, IDTipoNivelRelacionado " + 
                                        "UNION " +
                                        "SELECT IDFRDBase, ddd.IDTipoNivelRelacionado, MAX(DataEdicao) DataEdicao " +
                                        "FROM FRDBaseDataDeDescricao ddd " +
                                            "LEFT JOIN FRDBase frd ON frd.ID = ddd.IDFRDBase AND frd.isDeleted = 0 " +
                                        "WHERE frd.ID IS NULL AND ddd.isDeleted = 0 AND " + filtro_IDTipoNivelRelacionado +
                                        "GROUP BY IDFRDBase, ddd.IDTipoNivelRelacionado " +
                                    ") dd ON dd.IDFRDBase = ddd.IDFRDBase AND dd.IDTipoNivelRelacionado = ddd.IDTipoNivelRelacionado AND dd.DataEdicao = ddd.DataEdicao " +
                                "WHERE dd.IDFRDBase IS NULL AND ddd." + filtro_IDTipoNivelRelacionado +
                            ") dd " +
                            " WHERE dd.isDeleted = 0 {1} " +
                            " GROUP BY dd.IDFRDBase, dd.IDTrusteeOperator " +
                        " ) ddd " +
                        "{0} " +
                        " GROUP BY ddd.IDTrusteeOperator " +
                    " ) ddd2 ON ddd2.IDTrusteeOperator = t.ID " +
                    " LEFT JOIN ( " +
                        " SELECT ddd.IDTrusteeOperator, COUNT(ddd.IDFRDBase) countEliminacoes " +
                        " FROM FRDBaseDataDeDescricao ddd " +
                            " INNER JOIN ( " +
                                " SELECT IDFRDBase, IDTipoNivelRelacionado, MAX(DataEdicao) DataEdicao " +
                                " FROM FRDBaseDataDeDescricao " +
                                " WHERE isDeleted = 0 AND " + filtro_IDTipoNivelRelacionado +
                                " GROUP BY IDFRDBase, IDTipoNivelRelacionado " +
                            " ) dd ON dd.IDFRDBase = ddd.IDFRDBase AND dd.IDTipoNivelRelacionado = ddd.IDTipoNivelRelacionado AND dd.DataEdicao = ddd.DataEdicao " +
                            " LEFT JOIN FRDBase frd ON frd.ID = ddd.IDFRDBase AND frd.isDeleted = 0 " +
                            "{0} " +
                        " WHERE frd.ID is null AND ddd.isDeleted = 0 {1} " +
                        " GROUP BY ddd.IDTrusteeOperator " +
                    " ) ddd3 ON ddd3.IDTrusteeOperator = t.ID " +
#if DEBUG
                " WHERE  t.isDeleted = 0  AND t.CatCode = 'USR' AND t.IsActive = 1 " +
#else
                " WHERE t.BuiltInTrustee = 0 AND t.isDeleted = 0  AND t.CatCode = 'USR'  AND t.IsActive = 1 " +
#endif
                " ORDER BY t.CatCode, t.Name ",
                innerJoinPesquisa, innerDateWhereClause, excluedImport ? " AND ddd.Importacao = 0 " : "" );

            return sqlText;
        }

        private string build_Condicao_IDTipoNivelRelacionado(long idTipoNivel) {
            string filtro_IDTipoNivelRelacionado = string.Empty;

            if (idTipoNivel != 3)   // 3 : Entidade produtora (engloba os 3, 4, 5, 6); ver ::GetTotalCriacoesUANumInt()
                filtro_IDTipoNivelRelacionado = string.Format("IDTipoNivelRelacionado = {0} ", idTipoNivel);
            else
                filtro_IDTipoNivelRelacionado = "IDTipoNivelRelacionado IN (3, 4, 5, 6) ";

            return filtro_IDTipoNivelRelacionado;
        }

        protected abstract List<TotalTipo> GetTotal_NiveisPorOperador(long idTipoNivel, bool unidadesFisicasOnly, DateTime dataInicio, DateTime dataFim, bool excludeImport, bool sobrePesquisa, IDbConnection conn);


        public string BuildSQLQuery_GetTotalCriacoesRegAutPorOper(long idTipoNoticiaAut, string innerDateWhereClause) {
            string filtro_IDTipoNoticiaAut = "IDTipoNoticiaAut < 7 ";
            if (idTipoNoticiaAut == 1)
                filtro_IDTipoNoticiaAut = "IDTipoNoticiaAut BETWEEN 1 AND 3 ";
            else if (idTipoNoticiaAut > 0)
                filtro_IDTipoNoticiaAut = string.Format("IDTipoNoticiaAut = {0} ", idTipoNoticiaAut);

            string sqlText = string.Format(
                // So´ utilizadores (excluir os grupos)
                "SELECT t.ID, t.Name, COALESCE(ddd1.countCriacoes, 0) CountCriacoes, COALESCE(ddd2.sumEdicoes, 0) CountEdicoes, COALESCE(ddd3.countEliminacoes, 0) CountEliminacoes " +
                " FROM Trustee t " +
                    " LEFT JOIN ( " +
                        " SELECT ddd.IDTrusteeOperator, COUNT(ddd.IDControloAut) countCriacoes " +
                        " FROM ControloAutDataDeDescricao ddd " +
                            " INNER JOIN ( " +
                                " SELECT IDControloAut, IDTipoNoticiaAut, MIN(DataEdicao) DataEdicao " + 
                                " FROM ControloAutDataDeDescricao " +
                                " WHERE isDeleted = 0 AND " + filtro_IDTipoNoticiaAut +
                                " GROUP BY IDControloAut, IDTipoNoticiaAut " +
                                " HAVING COUNT(IDControloAut) > 1 " +
				                " UNION " +
                                " SELECT ddd.IDControloAut, ddd.IDTipoNoticiaAut, MIN(ddd.DataEdicao) DataEdicao " +
				                " FROM ControloAutDataDeDescricao ddd " +
                                    " INNER JOIN ControloAut ca ON ca.ID = ddd.IDControloAut AND ca.IDTipoNoticiaAut = ddd.IDTipoNoticiaAut AND ca.isDeleted = 0 " +
                                " WHERE ddd.isDeleted = 0 AND ddd." + filtro_IDTipoNoticiaAut +
                                " GROUP BY ddd.IDControloAut, ddd.IDTipoNoticiaAut " +
                                " HAVING COUNT(ddd.IDControloAut) = 1 " + // não contar como criado os CAs com 1 único registo, registo esse referente à sua eliminação
                            " ) dd ON dd.IDControloAut = ddd.IDControloAut AND dd.IDTipoNoticiaAut = ddd.IDTipoNoticiaAut AND dd.DataEdicao = ddd.DataEdicao " +
                        " WHERE ddd.isDeleted = 0 {0} " +
                        " GROUP BY ddd.IDTrusteeOperator " +
                    " ) ddd1 ON ddd1.IDTrusteeOperator = t.ID " +
                    " LEFT JOIN ( " +
                        " SELECT IDTrusteeOperator, SUM(ddd.CountEdicoes) sumEdicoes " +
                        " FROM ( " +
                            " SELECT dd.IDControloAut, dd.IDTrusteeOperator, COUNT(dd.DataEdicao) CountEdicoes " +
                            " FROM ( " +
                                "SELECT ddd.IDControloAut, ddd.IDTrusteeOperator, ddd.DataEdicao, ddd.isDeleted " +
                                "FROM ControloAutDataDeDescricao ddd " +
                                    "LEFT JOIN ( " +
                                        "SELECT IDControloAut, IDTipoNoticiaAut, MIN(DataEdicao) DataEdicao " +
                                        "FROM ControloAutDataDeDescricao " +
                                        "WHERE isDeleted = 0 AND " + filtro_IDTipoNoticiaAut +
                                        "GROUP BY IDControloAut, IDTipoNoticiaAut " +
                                        "UNION " +
                                        "SELECT ddd.IDControloAut, ddd.IDTipoNoticiaAut, MAX(ddd.DataEdicao) DataEdicao " +
                                        "FROM ControloAutDataDeDescricao ddd " +
                                            "LEFT JOIN ControloAut ca ON ca.ID = ddd.IDControloAut AND ca.IDTipoNoticiaAut = ddd.IDTipoNoticiaAut AND ca.isDeleted = 0 " +
                                        "WHERE ca.ID IS NULL AND ddd.isDeleted = 0 AND ddd." + filtro_IDTipoNoticiaAut +
                                        "GROUP BY ddd.IDControloAut, ddd.IDTipoNoticiaAut " +
                                    ") dd ON dd.IDControloAut = ddd.IDControloAut AND dd.IDTipoNoticiaAut = ddd.IDTipoNoticiaAut AND dd.DataEdicao = ddd.DataEdicao " +
                                "WHERE dd.IDControloAut IS NULL AND ddd." + filtro_IDTipoNoticiaAut +
                            ") dd " +
                            " WHERE dd.isDeleted = 0 {0} " +
                            " GROUP BY dd.IDControloAut, dd.IDTrusteeOperator " +
                        " ) ddd " +
                        " GROUP BY ddd.IDTrusteeOperator " +
                    " ) ddd2 ON ddd2.IDTrusteeOperator = t.ID " +
                    " LEFT JOIN ( " +
                        " SELECT ddd.IDTrusteeOperator, COUNT(ddd.IDControloAut) countEliminacoes " +
                        " FROM ControloAutDataDeDescricao ddd " +
                            " INNER JOIN ( " +
                                " SELECT IDControloAut, IDTipoNoticiaAut, MAX(DataEdicao) DataEdicao " +
                                " FROM ControloAutDataDeDescricao " +
                                " WHERE isDeleted = 0 AND " + filtro_IDTipoNoticiaAut +
                                " GROUP BY IDControloAut, IDTipoNoticiaAut " +
                            " ) dd ON dd.IDControloAut = ddd.IDControloAut AND dd.IDTipoNoticiaAut = ddd.IDTipoNoticiaAut AND dd.DataEdicao = ddd.DataEdicao " +
                            " LEFT JOIN ControloAut ca ON ca.ID = ddd.IDControloAut AND dd.IDTipoNoticiaAut = ddd.IDTipoNoticiaAut AND ca.isDeleted = 0 " +
                        " WHERE ca.ID IS NULL AND ddd.isDeleted = 0 {0} " +
                        " GROUP BY ddd.IDTrusteeOperator " +
                    " ) ddd3 ON ddd3.IDTrusteeOperator = t.ID " +
#if DEBUG
                " WHERE  t.isDeleted = 0  AND t.CatCode = 'USR' AND t.IsActive = 1 " +
#else
                " WHERE t.BuiltInTrustee = 0 AND t.isDeleted = 0  AND CatCode = 'USR' AND t.IsActive = 1 " +
#endif
                " ORDER BY  t.Name ",
                innerDateWhereClause);

            return sqlText;
        }
        public abstract List<TotalTipo> GetTotalCriacoesRegAutPorOper(long idTipoNoticiaAut, DateTime dataInicio, DateTime dataFim, IDbConnection conn);
        
        /**
         * Nao Usado:
         */
        public abstract List<TotalTipo> GetTotalCriacoesUANumInt(string dataInicio, string dataFim, bool sobrePesquisa, IDbConnection conn);


        #endregion

        #region "Totais na Actualidade"

        protected string BuildSQLQuery_GetTotalUF(bool sobrePesquisa) {
            // Pretende-se que as unidades fisicas eliminadas (na licença com gestao de depositos) nao sejam contadas:
            string leftJoin = " LEFT JOIN NivelUnidadeFisica nuf  ON nuf.ID = Nivel.ID AND nuf.isDeleted = 0 ";

            string innerJoin = string.Empty;
#if DEBUG
            if (sobrePesquisa) throw new NotImplementedException("Estatistica sobre resultados de pesquisa");
#endif

            string commandText = string.Format(
                "SELECT COUNT(Nivel.ID) " +
                "FROM Nivel {0} {1} " +
                "WHERE Nivel.IDTipoNivel = 4 " +
                    "AND Nivel.isDeleted = 0 AND (nuf.Eliminado is null OR nuf.Eliminado = 0) ", leftJoin, innerJoin);
            return commandText;
        }

        protected string BuildSQLQuery_GetTotalUFSemUIs(bool sobrePesquisa)
        {
            // Pretende-se que as unidades fisicas eliminadas (na licença com gestao de depositos) nao sejam contadas:
            string leftJoin = " LEFT JOIN NivelUnidadeFisica nuf  ON nuf.ID = Nivel.ID AND nuf.isDeleted = 0 ";

            string innerJoin = " LEFT JOIN SFRDUnidadeFisica sfrduf ON sfrduf.IDNivel = Nivel.ID AND sfrduf.isDeleted = 0 ";
            if (sobrePesquisa)
                innerJoin += "INNER JOIN SearchCache ON SearchCache.IDNivel = Nivel.ID ";

            string commandText = string.Format(
                "SELECT COUNT(Nivel.ID) " +
                "FROM Nivel {0} {1} " +
                "WHERE Nivel.IDTipoNivel = 4 " +
                    "AND Nivel.isDeleted = 0 AND (nuf.Eliminado is null OR nuf.Eliminado = 0) AND sfrduf.IDNivel IS NULL", leftJoin, innerJoin);
            return commandText;
        }
        public abstract long GetTotalUF(bool sobrePesquisa, IDbConnection conn);
        public abstract long GetTotalUFSemUIs(bool sobrePesquisa, IDbConnection conn);

        protected string BuildSQLQuery_GetTotalObjsDigitaisFedSimples() {
            return @" 
SELECT COUNT(*)
FROM (
	SELECT od.ID 
	FROM ObjetoDigital od 
		LEFT JOIN  ObjetoDigitalRelacaoHierarquica odrh ON odrh.IDUpper = od.ID AND odrh.isDeleted = 0 
	WHERE od.isDeleted = 0 AND odrh.IDUpper IS NULL
	UNION
	SELECT od.ID 
	FROM ObjetoDigital od 
		INNER JOIN  ObjetoDigitalRelacaoHierarquica odrh ON odrh.ID = od.ID AND odrh.isDeleted = 0 
	WHERE od.isDeleted = 0
) od
";
        }
        public abstract long GetTotalObjsDigitaisFedSimples(IDbConnection conn);

        protected string BuildSQLQuery_GetTotalObjsDigitaisFedCompostos()
        {
            return @" 
SELECT COUNT(od.ID)
FROM (
	SELECT od.ID 
	FROM ObjetoDigital od 
		INNER JOIN  ObjetoDigitalRelacaoHierarquica odrh ON odrh.IDUpper = od.ID AND odrh.isDeleted = 0 
	WHERE od.isDeleted = 0
	GROUP BY od.ID 
) od";
        }
        public abstract long GetTotalObjsDigitaisFedCompostos(IDbConnection conn);

        protected string BuildSQLQuery_GetTotalObjsDigitaisOutros()
        {
            return " SELECT count(i.idx) FROM SFRDImagem i  WHERE i.isDeleted = 0 AND Tipo <> 'Fedora'";
        }
        public abstract long GetTotalObjsDigitaisOutros(IDbConnection conn);

        protected string BuildSQLQuery_GetTotalCAPorTipo() {
            // NOTA: (tna.ID < 6) exlui os: 'Subtipologia informacional', 'Diploma' , 'Modelo'.
            return 
                "SELECT 1, 'Assuntos', COUNT(ca.ID) " +
                "FROM TipoNoticiaAut tna " +
                    "LEFT JOIN ControloAut ca ON ca.IDTipoNoticiaAut = tna.ID AND ca.isDeleted = 0 " +
                "WHERE tna.ID BETWEEN 1 AND 3 " +
                "SELECT tna.ID, tna.Designacao, COUNT(ca.ID) " +
                "FROM TipoNoticiaAut tna " +
                    "LEFT JOIN ControloAut ca ON ca.IDTipoNoticiaAut = tna.ID AND ca.isDeleted = 0 " +
                "WHERE tna.ID BETWEEN 4 AND 6 " +
                "GROUP BY tna.ID, tna.Designacao " +
                "ORDER BY tna.ID ";
        }

        public abstract List<TotalTipo> GetTotalCAPorTipo(IDbConnection conn);

        public abstract List<TotalTipo> GetTotalNivelPorTipoDesc(bool sobrePesquisa, IDbConnection conn);
        #endregion

        #region Fedora
        public abstract Dictionary<string, string> GetGisaIDs(List<string> pids, IDbConnection conn);
        #endregion
    }
}