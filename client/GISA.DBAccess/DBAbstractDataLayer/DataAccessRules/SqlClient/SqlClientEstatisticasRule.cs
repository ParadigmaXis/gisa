using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
    /// <summary>
    /// Summary description for SqlClientEstatisticasRule.
    /// </summary>
    public class SqlClientEstatisticasRule : EstatisticasRule
    {
        #region "Totais na Actualidade"
        public override List<TotalTipo> GetTotalNivelPorTipoDesc(bool sobrePesquisa, IDbConnection conn)
        {
            string innerJoin = string.Empty;

#if DEBUG
            if (sobrePesquisa) throw new NotImplementedException("Estatistica sobre resultados de pesquisa");
#endif

            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = string.Format(
                "SELECT 1, 'Entidade Detentora', COUNT(n.ID) FROM Nivel n " +
                    "LEFT JOIN RelacaoHierarquica rh ON rh.ID = n.ID AND rh.isDeleted = 0 " +
                    "{0} " +
                "WHERE n.IDTipoNivel = 1 AND n.isDeleted = 0 AND rh.ID IS NULL; " +
                "SELECT DISTINCT tnr.ID, tnr.Designacao, COUNT(n.ID) " +
                "FROM Nivel n " +
                    "{0} " +
                    "INNER JOIN RelacaoHierarquica rh ON rh.ID = n.ID AND rh.isDeleted = 0 " +
                    "INNER JOIN TipoNivelRelacionado tnr ON tnr.ID = rh.IDTipoNivelRelacionado AND tnr.isDeleted = 0 " +
                "WHERE n.IDTipoNivel = 1 AND n.isDeleted = 0 " +
                "GROUP BY tnr.ID, tnr.Designacao; " +
                "SELECT 3 ID, 'Entidade Produtora', COUNT(n.ID) " +
                "FROM Nivel n " +
                    "{0} " +
                "WHERE n.IDTipoNivel = 2 AND n.isDeleted = 0; " +
                "SELECT tnr.ID, tnr.Designacao, COUNT(rh.ID) " +
                "FROM TipoNivelRelacionado tnr " +
                    "LEFT JOIN RelacaoHierarquica rh ON rh.IDTipoNivelRelacionado = tnr.ID AND rh.isDeleted = 0 " +
                    "{0} " +
                "WHERE tnr.ID > 6 AND tnr.ID < 11 AND tnr.isDeleted = 0 " +
                "GROUP BY tnr.ID, tnr.Designacao " +
                "ORDER BY tnr.ID; ", innerJoin);
            SqlDataReader reader = command.ExecuteReader();
            List<TotalTipo> results = GetTotais(reader);
            reader.Close();

            return results;
        }

        public override long GetTotalUF(bool sobrePesquisa, IDbConnection conn) {
            SqlCommand command = new SqlCommand(this.BuildSQLQuery_GetTotalUF(sobrePesquisa), (SqlConnection)conn);
            return System.Convert.ToInt64(command.ExecuteScalar());        
        }

        public override long GetTotalUFSemUIs(bool sobrePesquisa, IDbConnection conn)
        {
            SqlCommand command = new SqlCommand(this.BuildSQLQuery_GetTotalUFSemUIs(sobrePesquisa), (SqlConnection)conn);
            return System.Convert.ToInt64(command.ExecuteScalar());
        }

        public override long GetTotalObjsDigitaisFedSimples(IDbConnection conn) {
            SqlCommand command = new SqlCommand(this.BuildSQLQuery_GetTotalObjsDigitaisFedSimples(), (SqlConnection)conn);
            return System.Convert.ToInt64(command.ExecuteScalar());        
        }

        public override long GetTotalObjsDigitaisFedCompostos(IDbConnection conn)
        {
            SqlCommand command = new SqlCommand(this.BuildSQLQuery_GetTotalObjsDigitaisFedCompostos(), (SqlConnection)conn);
            return System.Convert.ToInt64(command.ExecuteScalar());
        }

        public override long GetTotalObjsDigitaisOutros(IDbConnection conn)
        {
            SqlCommand command = new SqlCommand(this.BuildSQLQuery_GetTotalObjsDigitaisOutros(), (SqlConnection)conn);
            return System.Convert.ToInt64(command.ExecuteScalar());
        }


        public override List<TotalTipo> GetTotalCAPorTipo(IDbConnection conn) {
            SqlCommand command = new SqlCommand(this.BuildSQLQuery_GetTotalCAPorTipo(), (SqlConnection)conn);
            SqlDataReader reader = command.ExecuteReader();
            List<TotalTipo> results = GetTotais(reader);
            reader.Close();
            return results;
        }
        #endregion

        #region Totais por periodo

        public override List<TotalTipo> GetTotalCriacoesUANumInt(string dataInicio, string dataFim, bool sobrePesquisa, IDbConnection conn)
        {
            string innerJoin = string.Empty;
            string innerWhereClause = "WHERE dd.isDeleted = 0 ";

#if DEBUG
            if (sobrePesquisa) throw new NotImplementedException("Estatistica sobre resultados de pesquisa");
#endif

            if (dataInicio.Length > 0)
                innerWhereClause +=
                    string.Format("AND dd.DataEdicao >= '{0}' ", dataInicio);

            if (dataFim.Length > 0)
                innerWhereClause +=
                    string.Format("AND dd.DataEdicao <= '{0}' ", dataFim);

            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = string.Format(
                "SELECT tnr.ID, tnr.Designacao, COUNT(dd.IDFRDBase) " +
                "FROM TipoNivelRelacionado tnr " +
                    "LEFT JOIN ( " +
                        "SELECT ID, IDTipoNivelRelacionado " +
                        "FROM RelacaoHierarquica " +
                        "WHERE isDeleted = 0 " +
                        "GROUP BY ID, IDTipoNivelRelacionado " +
                    ") rh ON rh.IDTipoNivelRelacionado = tnr.ID " + // UAs com tipo único
                    "LEFT JOIN FRDBase frd ON frd.IDNivel = rh.ID AND frd.isDeleted = 0 " +
                    "LEFT JOIN ( " +
                        "SELECT IDFRDBase, MIN(DataEdicao) DataEdicao " +
                        "FROM FRDBaseDataDeDescricao dd " +
                        "{0} " +
                        "GROUP BY dd.IDFRDBase " +
                    ") dd ON dd.IDFRDBase = frd.ID " +
                    "{1} " +
                "WHERE rh.IDTipoNivelRelacionado BETWEEN 3 AND 10 " +
                "GROUP BY tnr.ID, tnr.Designacao " +
                "ORDER BY tnr.ID; " +
                "SELECT 0, 'Indefinidos', COUNT(dd.IDFRDBase) " +
                "FROM Nivel n " +
                    "INNER JOIN ( " +
                        "SELECT n.ID FROM ( " +
                            "SELECT ID, IDTipoNivelRelacionado FROM RelacaoHierarquica " +
                            "GROUP BY ID, IDTipoNivelRelacionado " +
                        ") n " +
                        "GROUP BY n.ID " +
                        "HAVING COUNT(n.IDTipoNivelRelacionado) > 1 " +
                    ") niveis ON niveis.ID = n.ID " +
                    "LEFT JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0 " +
                    "LEFT JOIN ( " +
                        "SELECT IDFRDBase, MIN(DataEdicao) DataEdicao " +
                        "FROM FRDBaseDataDeDescricao dd " +
                        "{0} " +
                        "GROUP BY dd.IDFRDBase " +
                    ") dd ON dd.IDFRDBase = frd.ID " +
                    "{1}; " +
                "SELECT 0, 'Outros', COUNT(n.ID) " +
                "FROM Nivel n " +
                    "LEFT JOIN RelacaoHierarquica rh ON rh.ID = n.ID AND rh.isDeleted = 0 " +
                    "LEFT JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0 " +
                "WHERE rh.ID IS NULL AND frd.ID IS NULL AND n.IDTipoNivel = 2 AND n.isDeleted = 0; ",
                innerWhereClause, innerJoin);
            SqlDataReader reader = command.ExecuteReader();
            List<TotalTipo> results = GetTotais(reader);
            reader.Close();

            return results;
        }


        protected override List<TotalTipo> GetTotal_NiveisPorOperador(long idTipoNivel, bool unidadesFisicasOnly, DateTime dataInicio, DateTime dataFim, bool excludeImport, bool sobrePesquisa, IDbConnection conn) {
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            string innerDateWhereClause = build_DateWhereClause(dataInicio, dataFim, ref command);
            
            command.CommandText = this.BuildSQLQuery_GetTotal_NiveisPorOperador(unidadesFisicasOnly, idTipoNivel, excludeImport, sobrePesquisa, innerDateWhereClause);

            SqlDataReader reader = command.ExecuteReader();
            List<TotalTipo> results = GetTotais(reader);
            reader.Close();

            return results;
        }

        public override List<TotalTipo> GetTotalCriacoesRegAutPorOper(long idTipoNoticiaAut, DateTime dataInicio, DateTime dataFim, IDbConnection conn)
        {
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            string innerDateWhereClause = build_DateWhereClause(dataInicio, dataFim, ref command);
            
            command.CommandText = this.BuildSQLQuery_GetTotalCriacoesRegAutPorOper(idTipoNoticiaAut, innerDateWhereClause);

            SqlDataReader reader = command.ExecuteReader();
            List<TotalTipo> results = GetTotais(reader);
            reader.Close();

            return results;
        }

        private string build_DateWhereClause(DateTime dataInicio, DateTime dataFim, ref SqlCommand command)
        {
            string innerDateWhereClause = string.Empty;

            if (dataInicio != DateTime.MinValue && dataFim != DateTime.MinValue)
            {
                innerDateWhereClause = "AND dd.DataEdicao >= @dataInicio AND dd.DataEdicao <= @dataFim ";
                command.Parameters.Add(new SqlParameter("dataInicio", dataInicio));
                command.Parameters.Add(new SqlParameter("dataFim", dataFim));
            }
            else if (dataInicio != DateTime.MinValue)
            {
                innerDateWhereClause = "AND dd.DataEdicao >= @dataInicio ";
                command.Parameters.Add(new SqlParameter("dataInicio", dataInicio));
            }
            else if (dataFim != DateTime.MinValue)
            {
                innerDateWhereClause = "AND dd.DataEdicao <= @dataFim ";
                command.Parameters.Add(new SqlParameter("dataFim", dataFim));
            }

            return innerDateWhereClause;
        }

        #endregion

        #region Fedora
        public override Dictionary<string, string> GetGisaIDs(List<string> pids, IDbConnection conn)
        {
            var gisa_ids = new Dictionary<string, string>();

            GisaDataSetHelperRule.ImportDesignacoes(pids.Distinct().ToArray(), conn);

            var command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 500;
            command.CommandText = @"
create table #ODTemp (ID bigint)

insert into #ODTemp
select od.ID
from ObjetoDigital od 
    inner join #temp on #temp.Designacao COLLATE LATIN1_GENERAL_CS_AS = od.pid COLLATE LATIN1_GENERAL_CS_AS
where od.isDeleted = 0";
            command.ExecuteNonQuery();

            command.CommandText = @"
select od.pid, frd.IDNivel 
from ObjetoDigital od 
	inner join #ODTemp T on T.ID = od.ID
	inner join SFRDImagemObjetoDigital imgOD on imgOD.IDObjetoDigital = od.ID and imgOD.isDeleted = 0
	inner join SFRDImagem img on img.IDFRDBase = imgOD.IDFRDBase and img.isDeleted = 0
	inner join FRDBase frd on frd.ID = img.IDFRDBase and frd.isDeleted = 0
where od.isDeleted = 0

select od.pid, frd.IDNivel 
from ObjetoDigital od 
	inner join #ODTemp T on T.ID = od.ID
	inner join ObjetoDigitalRelacaoHierarquica odrh on odrh.ID = od.ID and odrh.isDeleted = 0
	inner join ObjetoDigital odComp on odComp.ID = odrh.IDUpper and odComp.isDeleted = 0
	inner join SFRDImagemObjetoDigital imgOD on imgOD.IDObjetoDigital = odComp.ID and imgOD.isDeleted = 0
	inner join SFRDImagem img on img.IDFRDBase = imgOD.IDFRDBase and img.isDeleted = 0
	inner join FRDBase frd on frd.ID = img.IDFRDBase and frd.isDeleted = 0
where od.isDeleted = 0
";
            var reader = command.ExecuteReader();

            while (reader.Read())
                gisa_ids[reader.GetString(0)] = reader.GetInt64(1).ToString();

            reader.NextResult();

            while (reader.Read())
                gisa_ids[reader.GetString(0)] = reader.GetInt64(1).ToString();

            reader.Close();

            return gisa_ids;
        }
        #endregion

    }
}
