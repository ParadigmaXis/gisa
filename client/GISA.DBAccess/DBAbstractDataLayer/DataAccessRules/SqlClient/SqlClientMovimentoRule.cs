using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Diagnostics;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
    public class SqlClientMovimentoRule : MovimentoRule
    {
        public override void LoadMovimento(long movID, DataSet currentDataSet, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@movID", movID);
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Movimento"],
                    "WHERE ID = @movID");
                da.Fill(currentDataSet, "Movimento");
            }
        }

        public override void CalculateOrderedItems(string catCode, string FiltroNroMovimento, string FiltroDataInicio, string FiltroDataFim, string FiltroEntidade, string FiltroCodigo, IDbConnection conn)
        {
            StringBuilder whereQuery = new StringBuilder();

            if (FiltroNroMovimento.Length > 0)
                whereQuery.AppendFormat("AND m.ID = {0} ", FiltroNroMovimento);

            if (FiltroDataInicio.Length > 0)
                whereQuery.AppendFormat("AND convert(varchar, Data, 121) >= convert(varchar, '{0} 00:00:00.00', 121) ", FiltroDataInicio);

            if (FiltroDataFim.Length > 0)
                whereQuery.AppendFormat("AND convert(varchar, Data, 121) <= convert(varchar, '{0} 23:59:59.99', 121) ", FiltroDataFim);

            if (FiltroEntidade.Length > 0)
                whereQuery.AppendFormat("AND {0} ", FiltroEntidade);

            if (FiltroCodigo.Length > 0)
                whereQuery.AppendFormat("AND {0} ", FiltroCodigo);

            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #OrderedItems (seq_id INT Identity(1,1) NOT NULL, ID BIGINT NOT NULL )";
                command.ExecuteNonQuery();

                command.CommandText = string.Format(
                    "INSERT INTO #OrderedItems " +
                    "SELECT m.ID " +
                    "FROM Movimento m " +
                    "INNER JOIN MovimentoEntidade me on me.ID = m.IDEntidade and me.isDeleted = 0 " +
                    "WHERE m.isDeleted = 0 AND m.CatCode = '{0}' {1} " +
                    "ORDER BY m.ID DESC", catCode, whereQuery.ToString());
                command.ExecuteNonQuery();
            }
        }

        public override ArrayList GetItems(DataSet currentDataSet, int pageNr, int itemsPerPage, System.Data.IDbConnection conn)
        {
            ArrayList rows = new ArrayList();
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #ItemsID (ID BIGINT, Data DATETIME, Entidade NVARCHAR(444), Codigo NVARCHAR(25))";
                command.ExecuteNonQuery();

                command.CommandText =
                    "INSERT INTO #ItemsID " +
                    "SELECT mov.ID, mov.Data, me.Entidade, me.Codigo " +
                    "FROM #OrderedItems oi " +
                        "INNER JOIN Movimento mov ON mov.ID = oi.ID " +
                        "INNER JOIN MovimentoEntidade me ON me.ID = mov.IDEntidade " +
                    "WHERE seq_id >= @seq_id1 AND seq_id <= @seq_id2 " +
                    "ORDER BY seq_id";
                command.Parameters.AddWithValue("@seq_id1", (pageNr - 1) * itemsPerPage + 1);
                command.Parameters.AddWithValue("@seq_id2", pageNr * itemsPerPage);
                command.ExecuteNonQuery();

                using(SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Movimento"],
                        "INNER JOIN #ItemsID ON #ItemsID.ID = Movimento.ID");
                    da.Fill(currentDataSet, "Movimento");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["MovimentoEntidade"],
                        @"INNER JOIN (
                        SELECT DISTINCT m.IDEntidade 
                        FROM Movimento m
                            INNER JOIN #ItemsID ON #ItemsID.ID = m.ID
                        WHERE m.isDeleted = @isDeleted) Entidades ON Entidades.IDEntidade = MovimentoEntidade.ID ");
                    da.Fill(currentDataSet, "MovimentoEntidade");
                }

                command.Parameters.Clear();
                command.CommandText = "SELECT * FROM #ItemsID;";
                SqlDataReader reader = command.ExecuteReader();

                ArrayList row;
                while (reader.Read())
                {
                    row = new ArrayList();
                    row.Add(reader.GetValue(0));
                    row.Add(reader.GetValue(1));
                    row.Add(reader.GetValue(2));
                    row.Add(reader.GetValue(3));
                    rows.Add(row);
                }

                reader.Close();
            }
            return rows;
        }

        public override void DeleteTemporaryResults(System.Data.IDbConnection conn)
        {
            using (SqlCommand command = new SqlCommand("DROP TABLE #ItemsID; DROP TABLE #OrderedItems;", (SqlConnection)conn))
            {
                command.ExecuteNonQuery();
            }
        }

        public override List<DocumentoMovimentado> GetDocumentos(long IDReq, DataSet currentDataSet, IDbConnection conn)
        {
            return this.GetDocumentos(IDReq, string.Empty, currentDataSet, conn);
        }

        public override List<DocumentoMovimentado> GetDocumentos(long IDReq, string filter, DataSet currentDataSet, IDbConnection conn)
        {
            List<DocumentoMovimentado> ret = new List<DocumentoMovimentado>();
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #NiveisTemp (IDNivel BIGINT); "+
                    "CREATE TABLE #SPParametersNiveis (IDNivel BIGINT); " +
                    "CREATE TABLE #SPResultsCodigos (IDNivel BIGINT, CodigoCompleto NVARCHAR(300));";
                command.ExecuteNonQuery();

                command.CommandText = 
                    "INSERT INTO #NiveisTemp " +
                    "SELECT dr.IDNivel " +
                    "FROM DocumentosMovimentados dr " +
                    "WHERE dr.IDMovimento = @IDReq AND dr.isDeleted=@isDeleted";
                command.Parameters.AddWithValue("@IDReq", IDReq);
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.ExecuteNonQuery();

                using(SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN #NiveisTemp ON #NiveisTemp.IDNivel = Nivel.ID");
                    da.Fill(currentDataSet, "Nivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["DocumentosMovimentados"],
                        "WHERE IDMovimento = @IDReq");
                    da.Fill(currentDataSet, "DocumentosMovimentados");
                }

                command.CommandText = "INSERT INTO #SPParametersNiveis " +
                    "SELECT IDNivel " +
                    "FROM #NiveisTemp " +
                    "EXEC sp_getCodigosCompletosNiveis ";
                command.ExecuteNonQuery();

                command.CommandText =
                    "SELECT DISTINCT nt.IDNivel, nd.Designacao, tnr.Designacao, dp.InicioAno, dp.InicioMes, dp.InicioDia, dp.FimAno, dp.FimMes, dp.FimDia, codigo.CodigoCompleto " +
                    "FROM #NiveisTemp nt " +
                        "INNER JOIN NivelDesignado nd ON nt.IDNivel = nd.ID AND nd.isDeleted = @isDeleted " +
                        "INNER JOIN RelacaoHierarquica rh ON nt.IDNivel = rh.ID AND rh.isDeleted = @isDeleted " +
                        "INNER JOIN TipoNivelRelacionado tnr ON rh.IDTipoNivelRelacionado = tnr.ID AND tnr.isDeleted = @isDeleted " +
                        "INNER JOIN FRDBase frd ON frd.IDNivel = nt.IDNivel AND frd.isDeleted = @isDeleted " +
                        "INNER JOIN ( " +
                            "SELECT #SPResultsCodigos.IDNivel, MIN(#SPResultsCodigos.CodigoCompleto) CodigoCompleto " +
                            "FROM #SPResultsCodigos " +
                                "INNER JOIN #NiveisTemp p ON p.IDNivel = #SPResultsCodigos.IDNivel " +
                            "GROUP BY #SPResultsCodigos.IDNivel " +
                        ") codigo ON codigo.IDNivel = nt.IDNivel " +
                        "LEFT JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frd.ID AND dp.isDeleted = @isDeleted ";

                if (filter != string.Empty)
                {
                    command.Parameters.AddWithValue("@ndDesignacao", filter);
                    command.CommandText += "WHERE nd.Designacao=@ndDesignacao";
                }

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DocumentoMovimentado dm = new DocumentoMovimentado();
                    dm.IDNivel = System.Convert.ToInt64(reader.GetValue(0));
                    dm.Designacao = reader.GetValue(1).ToString();
                    dm.NivelDescricao = reader.GetValue(2).ToString();

                    dm.AnoInicio = reader.GetValue(3).ToString();
                    dm.MesInicio = reader.GetValue(4).ToString();
                    dm.DiaInicio = reader.GetValue(5).ToString();

                    dm.AnoFim = reader.GetValue(6).ToString();
                    dm.MesFim = reader.GetValue(7).ToString();
                    dm.DiaFim = reader.GetValue(8).ToString();

                    dm.CodigoCompleto = reader.GetValue(9).ToString();

                    ret.Add(dm);
                }

                reader.Close();

                command.CommandText =
                    "DROP TABLE #NiveisTemp " +
                    "DROP TABLE #SPParametersNiveis " +
                    "DROP TABLE #SPResultsCodigos ";

                command.ExecuteNonQuery();
            }

            return ret;
        }

        public override bool IsDeletable(long IDMov, IDbConnection conn)        
        {            
            int movimentos = 0;
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.CommandText = @"
                SELECT Count(IDNivel)
                FROM DocumentosMovimentados
                WHERE IDNivel IN
                    (SELECT IDNivel
                    FROM DocumentosMovimentados
                    WHERE IDMovimento = @id AND isDeleted = @isDeleted)";
                command.Parameters.AddWithValue("@id", IDMov);
                command.Parameters.AddWithValue("@isDeleted", 0);

                movimentos = (int)command.ExecuteScalar();
            }

            return movimentos == 0;
        }

        public override bool CanDeleteEntity(long IDEntidade, IDbConnection conn)
        {
            int res = 0;
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.Parameters.AddWithValue("@IDEntidade", IDEntidade);
                command.Parameters.AddWithValue("@isDeleted", 0);

                command.CommandText = @"
                    SELECT COUNT(*)
                    FROM Movimento m 
                    WHERE m.isDeleted = @isDeleted AND m.IDEntidade = @IDEntidade";
                res = System.Convert.ToInt32(command.ExecuteScalar());
            }
            return res == 0;
        }

        public override bool estaRequisitado(long idNivel, IDbConnection conn) {
            SqlCommand command = new SqlCommand("", (SqlConnection)conn);
            return estaRequisitado(idNivel, command);
        }

        public override bool estaRequisitado(long idNivel, IDbTransaction tran)
        {
            SqlCommand command = new SqlCommand("", (SqlConnection)tran.Connection, (SqlTransaction) tran);
            return estaRequisitado(idNivel, command);
        }

        private bool estaRequisitado(long idNivel, SqlCommand command)
        {
            bool result = false;
            SqlDataReader reader;
            command.Parameters.AddWithValue("@idNivel", idNivel);
            command.Parameters.AddWithValue("@isDeleted", 0);
            command.CommandText =
                "SELECT n1.ID IDNivel, MAX(req.Data) Data_Req, MAX(dev.Data) Data_Dev " +
                "	FROM Nivel n1 " +
                "	LEFT JOIN DocumentosMovimentados dm ON dm.IDNivel = n1.ID AND dm.isDeleted = @isDeleted " +
                "	LEFT JOIN Movimento req ON req.ID = dm.IDMovimento and req.CatCode = 'REQ' AND req.isDeleted = @isDeleted " +
                "   LEFT JOIN Movimento dev ON dev.ID = dm.IDMovimento AND dev.CatCode = 'DEV' AND dev.isDeleted = @isDeleted " +
                " WHERE  n1.ID = @idNivel " +
                " GROUP BY n1.ID";

            try
            {
                reader = command.ExecuteReader();
                if (reader.Read())
                    result = (!reader.IsDBNull(1) && reader.IsDBNull(2)) ||
                             (!reader.IsDBNull(1) && !reader.IsDBNull(2) && reader.GetDateTime(1) >= reader.GetDateTime(2));
                reader.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public override bool temMovimentosPosteriores(long IDNivel, long IDMovimento, string MovimentoCatCode, IDbTransaction tran)
        {
            using (var command = new SqlCommand(string.Empty, (SqlConnection)tran.Connection, (SqlTransaction)tran))
            {
                command.Parameters.AddWithValue("@IDNivel", IDNivel);
                command.Parameters.AddWithValue("@IDMovimento", IDMovimento);
                command.Parameters.AddWithValue("@MovimentoCatCode", MovimentoCatCode);
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.CommandText = string.Format(@"
                    SELECT COUNT(ID) 
                    FROM Movimento m
	                    INNER JOIN DocumentosMovimentados dm ON dm.IDMovimento = m.ID AND dm.isDeleted = @isDeleted
                    WHERE dm.IDNivel = @IDNivel
	                    AND m.CatCode = @MovimentoCatCode
	                    AND m.Data > (SELECT Data FROM Movimento WHERE ID = @IDMovimento AND isDeleted = @isDeleted)
	                    AND m.isDeleted = @isDeleted", IDNivel, MovimentoCatCode, IDMovimento);
                return System.Convert.ToInt64(command.ExecuteScalar()) > 0;
            }
        }

        public override bool CanDeleteMovimento(long IDMovimento, string MovimentoCatCode, IDbTransaction tran)
        {
            using (var command = new SqlCommand(string.Empty, (SqlConnection)tran.Connection, (SqlTransaction)tran))
            {
                command.Parameters.AddWithValue("@IDMovimento", IDMovimento);
                command.Parameters.AddWithValue("@MovimentoCatCode", MovimentoCatCode);
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.CommandText = @"
                    SELECT COUNT(ID) 
                    FROM Movimento m WITH (UPDLOCK) 
	                    INNER JOIN DocumentosMovimentados dm ON dm.IDMovimento = m.ID AND dm.isDeleted = @isDeleted
                        INNER JOIN (
                            SELECT dmDoc.IDNivel
                            FROM Movimento mDoc
                                INNER JOIN DocumentosMovimentados dmDoc ON dmDoc.IDMovimento = mDoc.ID AND dmDoc.isDeleted = @isDeleted
                            WHERE mDoc.ID = @IDMovimento AND mDoc.isDeleted = @isDeleted
                        ) docs ON docs.IDNivel = dm.IDNivel
                    WHERE m.CatCode = @MovimentoCatCode
	                    AND m.Data > (SELECT Data FROM Movimento WHERE ID = @IDMovimento AND isDeleted = @isDeleted)
	                    AND m.isDeleted = @isDeleted";
                return System.Convert.ToInt64(command.ExecuteScalar()) > 0;
            }
        }

        public override RequisicaoInfo getRequisicaoInfo(long idNivel, IDbConnection conn)
        {
            RequisicaoInfo result = null;
            SqlDataReader reader;
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.Parameters.AddWithValue("@idNivel", idNivel);
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.Parameters.AddWithValue("@reqCatCode", "REQ");
                command.CommandText = string.Format(@"
                    SELECT TOP 1 n.ID IDNivel, dm.IDMovimento, me.Entidade, req.Data, req.Notas 
                    FROM Nivel n
	                    INNER JOIN DocumentosMovimentados dm ON dm.IDNivel = n.ID AND dm.isDeleted = @isDeleted 
	                    INNER JOIN Movimento req ON req.ID = dm.IDMovimento and req.CatCode = @reqCatCode AND req.isDeleted = @isDeleted 
	                    INNER JOIN MovimentoEntidade me ON me.ID = req.IDEntidade AND me.isDeleted = @isDeleted 
                    WHERE n.ID = @idNivel AND n.isDeleted = @isDeleted
                    ORDER BY req.Data DESC", idNivel);
                try
                {
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        result = new RequisicaoInfo();
                        result.idNivel = reader.GetInt64(0);
                        result.idMovimento = reader.GetInt64(1);
                        result.entidade = reader.GetString(2);
                        result.data = reader.GetDateTime(3);
                        result.notas = reader.GetValue(4).ToString();
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return result;
        }

        public override long getCountDocumentosNaoDevolvidos(IDbConnection conn) {
            long result = 0;
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.Parameters.AddWithValue("@reqCatCode", "REQ");
                command.CommandText =
                    "SELECT COUNT(DISTINCT n1.ID) " +
                    "FROM Nivel n1 " +
                    "INNER JOIN NivelDesignado nd ON nd.ID = n1.ID AND nd.isDeleted=@isDeleted " +
                    "INNER JOIN DocumentosMovimentados dm ON dm.IDNivel = n1.ID AND dm.isDeleted = @isDeleted " +
                    "LEFT JOIN Movimento req ON req.ID = dm.IDMovimento and req.CatCode = @reqCatCode AND req.isDeleted = @isDeleted " +
                    "WHERE NOT EXISTS ( " +
                    "	SELECT * FROM Movimento mov WHERE mov.ID = dm.IDMovimento AND mov.Data > req.Data ) ";
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                        result = System.Convert.ToInt64(reader.GetValue(0));

                    reader.Close();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return result;
        }
        public override List<DocumentoRequisicaoInfo> getDocumentosNaoDevolvidos(IDbConnection conn) {
            List<DocumentoRequisicaoInfo> resultSet = new List<DocumentoRequisicaoInfo>();
            DocumentoRequisicaoInfo result = null;

            SqlDataReader reader;
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.CommandText =
                    "CREATE TABLE #NiveisDoc (IDNivel BIGINT, Designacao NVARCHAR(768), IDMovimento BIGINT, Entidade NVARCHAR(444), Data DATETIME, Notas NTEXT) " +
                    "CREATE TABLE #SPParametersNiveis (IDNivel BIGINT); " +
                    "CREATE TABLE #SPResultsCodigos (IDNivel BIGINT,CodigoCompleto NVARCHAR(300)); ";
                command.ExecuteNonQuery();

                command.CommandText =
                    "INSERT INTO #NiveisDoc " +
                    "SELECT docs.ID, docs.Designacao, m.ID, me.Entidade, m.Data, m.Notas " +
                    "FROM (" +
                        "SELECT n.ID, nd.Designacao, MAX(req.Data) reqData " +
                        "FROM Nivel n " +
                            "LEFT JOIN DocumentosMovimentados dm ON dm.IDNivel = n.ID AND dm.isDeleted = @isDeleted " +
                            "LEFT JOIN Movimento req ON req.ID = dm.IDMovimento and req.CatCode = @reqCatCode AND req.isDeleted = @isDeleted " +
                            "LEFT JOIN Movimento dev ON dev.ID = dm.IDMovimento AND dev.CatCode = @devCatCode AND dev.isDeleted = @isDeleted " +
                            "INNER JOIN NivelDesignado nd ON nd.ID = n.ID " +
                        "GROUP BY n.ID, nd.Designacao " +
                        "HAVING NOT MAX(req.Data) IS NULL AND (MAX(dev.Data) IS NULL OR MAX(dev.Data) < MAX(req.Data))) docs, Movimento m " +
                        "INNER JOIN MovimentoEntidade me ON m.IDEntidade = me.ID  AND me.isDeleted = @isDeleted " +
                    "WHERE m.CatCode = @reqCatCode AND m.Data = docs.reqData AND m.isDeleted = @isDeleted";
                command.Parameters.AddWithValue("@reqCatCode", "REQ");
                command.Parameters.AddWithValue("@devCatCode", "DEV");
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.ExecuteNonQuery();

                command.CommandText = " INSERT INTO #SPParametersNiveis SELECT IDNivel FROM #NiveisDoc ";
                command.ExecuteNonQuery();

                command.CommandText = "EXEC sp_getCodigosCompletosNiveis ";
                command.ExecuteNonQuery();

                command.CommandText =
                    //             0,                     1,              2,           3, 
                    "SELECT  #NiveisDoc.IDNivel, codigo.CodigoCompleto,  Designacao, IDMovimento, " +
                    //      4,         5,     6
                    "Entidade,  Data, Notas " +
                    "FROM #NiveisDoc " +
                    "INNER JOIN ( " +
                            "SELECT #SPResultsCodigos.IDNivel, MIN(#SPResultsCodigos.CodigoCompleto) CodigoCompleto " +
                            "FROM #SPResultsCodigos " +
                                "INNER JOIN #NiveisDoc p ON p.IDNivel = #SPResultsCodigos.IDNivel " +
                            "GROUP BY #SPResultsCodigos.IDNivel " +
                        ") codigo ON codigo.IDNivel = #NiveisDoc.IDNivel " +
                    "ORDER BY Data ";
                try
                {
                    reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        result = new DocumentoRequisicaoInfo();

                        result.idNivel = reader.GetInt64(0);
                        result.Codigo_Completo = reader.GetString(1);
                        result.ND_Designacao = reader.GetString(2);

                        result.idMovimento = reader.GetInt64(3);
                        if (!reader.IsDBNull(4))
                            result.entidade = reader.GetString(4);
                        if (!reader.IsDBNull(5))
                            result.data = reader.GetDateTime(5);
                        if (!reader.IsDBNull(6))
                            result.notas = reader.GetString(6);

                        resultSet.Add(result);
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    throw e;
                }

                // Remover as tabelas temps:
                command.CommandText =
                    "DROP TABLE #NiveisDoc " +
                    "DROP TABLE #SPParametersNiveis " +
                    "DROP TABLE #SPResultsCodigos ";

                command.ExecuteNonQuery();
            }

            return resultSet;

        }

        #region MasterPanelSeries
        public override bool foiMovimentado(long IDNivel, IDbConnection conn)
        {
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.Parameters.AddWithValue("@IDNivel", IDNivel);
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.CommandText = 
                    "SELECT COUNT(*) " +
                    "FROM DocumentosMovimentados " +
                    "WHERE IDNivel = @IDNivel AND isDeleted = @isDeleted";

                long cont = System.Convert.ToInt64(command.ExecuteScalar());

                return cont > 0;
            }
        }
        #endregion

        #region EntidadeList
        public override void Entidade_CalculateOrderedItems(string activo, string FiltroEntidadeLike, string CodigoEntidadeLike, IDbConnection conn) {
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                // criar uma tabela com todos os IDs dos CAs ordenados a serem apresentados
                command.CommandText = "CREATE TABLE #OrderedItems (seq_id BIGINT IDENTITY  NOT NULL, ID BIGINT, Entidade NVARCHAR(444) NOT NULL,  Codigo varchar(25) ); ";
                command.ExecuteNonQuery();

                StringBuilder whereQuery = new StringBuilder();
                whereQuery.Append("WHERE me.isDeleted = 0 ");
                if (FiltroEntidadeLike.Length > 0)
                    whereQuery.Append("AND " + FiltroEntidadeLike + " ");
                if (activo.Length > 0)
                    whereQuery.Append(" AND " + activo + " ");
                if (CodigoEntidadeLike.Length > 0)
                    whereQuery.Append(" AND " + CodigoEntidadeLike + " ");

                command.CommandText = string.Format(
                    "INSERT INTO #OrderedItems (ID, Entidade, Codigo) " +
                    "SELECT me.ID, me.Entidade, me.Codigo " +
                    "FROM MovimentoEntidade me {0}  " +
                    "ORDER BY me.Entidade ", whereQuery.ToString());
                command.ExecuteNonQuery();
            }
        }

        public override ArrayList Entidade_GetItems(DataSet currentDataSet, int pageNr, int itemsPerPage, string FiltroTermoLike, IDbConnection conn) {
            ArrayList rows = new ArrayList();

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["MovimentoEntidade"]);
                da.Fill(currentDataSet, "MovimentoEntidade");

                command.CommandText = "SELECT * FROM  #OrderedItems  WHERE seq_id >= @seq_id1 AND seq_id <= @seq_id2 ORDER BY Entidade";
                command.Parameters.AddWithValue("@seq_id1", (pageNr - 1) * itemsPerPage + 1);
                command.Parameters.AddWithValue("@seq_id2", pageNr * itemsPerPage);
                SqlDataReader reader = command.ExecuteReader();

                DataRow[] nRows;
                while (reader.Read())
                {
                    nRows = currentDataSet.Tables["MovimentoEntidade"].Select(String.Format("ID = {0} ", reader.GetValue(1)));
                    if (nRows.Length > 0)
                        rows.Add(nRows[0]);
                }
                reader.Close();
            }
            
            return rows;
        }

        public override void Entidade_DeleteTemporaryResults(IDbConnection conn) {
            using (SqlCommand command = new SqlCommand("DROP TABLE #OrderedItems;", (SqlConnection)conn))
            {
                command.ExecuteNonQuery();
            }
        }

        #endregion

        public override IDataReader GetAllMovimentos(DateTime dataInicio, DateTime dataFim, IDbConnection conn)
        {
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {

                var whereClause = string.Empty;
                if (dataInicio != DateTime.MinValue)
                {
                    whereClause +="AND m.Data >= @dataInicio ";
                    command.Parameters.AddWithValue("@dataInicio", dataInicio);
                }

                if (dataFim != DateTime.MinValue)
                {
                    whereClause += "AND m.Data <= @dataFim ";
                    command.Parameters.AddWithValue("@dataFim", dataFim);
                }

                command.Parameters.AddWithValue("@isDeleted", 0);

                command.CommandText = string.Format(
                    "SELECT n.ID, n.Codigo, nd.Designacao, m.ID, m.CatCode, m.Data, me.Entidade " +
                    "FROM Movimento m " +
                        "INNER JOIN DocumentosMovimentados dm ON dm.IDMovimento = m.ID AND dm.isDeleted = @isDeleted " +
                        "INNER JOIN Nivel n ON n.ID = dm.IDNivel AND dm.isDeleted = @isDeleted " +
                        "INNER JOIN NivelDesignado nd ON nd.ID = n.ID AND nd.isDeleted = @isDeleted " +
                        "INNER JOIN MovimentoEntidade me ON me.ID = m.IDEntidade AND me.isDeleted = @isDeleted " +
                    "WHERE m.isDeleted = @isDeleted {0} " +
                    "ORDER BY m.ID DESC", whereClause);

                return command.ExecuteReader();
            }
        }
    }
}
