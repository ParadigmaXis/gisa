using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
    public class SqlClientDepositoRule : DepositoRule
    {
        #region " DepositoList "
        public override void CalculateOrderedItems(string FiltroDesignacaoLike, IDbConnection conn)
        {
            string whereClause = string.Empty;
            if (FiltroDesignacaoLike.Length > 0)
                whereClause = "AND " + FiltroDesignacaoLike;

            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection) conn);
            command.CommandText =
                "CREATE TABLE #OrderedItems (seq_id INT Identity(1,1) NOT NULL, IDDeposito BIGINT NOT NULL, Designacao NVARCHAR(200) NULL ); " +
                "INSERT INTO #OrderedItems " +
                "SELECT ID, Designacao " +
                "FROM Deposito " +
                "WHERE isDeleted = 0 " + whereClause + " " +
                "ORDER BY Designacao";
            command.ExecuteNonQuery();
        }

        public override int GetPageForID(long depositoID, int pageLimit, IDbConnection conn)
        {
            SqlCommand command = new SqlCommand("", (SqlConnection)conn);
            command.CommandText =
                string.Format("SELECT seq_id FROM #OrderedItems WHERE IDDeposito={0}", depositoID);

            int id_seq = System.Convert.ToInt32(command.ExecuteScalar());

            if (id_seq % pageLimit != 0)
                return id_seq / pageLimit + 1;
            else
                return id_seq / pageLimit;
        }

        public override int CountPages(int itemsPerPage, ref int numberOfItems, IDbConnection conn)
        {
            SqlDataReader reader;
            SqlCommand command = new SqlCommand("", (SqlConnection)conn);

            command.CommandText = string.Format(
                "SELECT COUNT(*) FROM #OrderedItems");

            int count = 0;

            try
            {
                reader = command.ExecuteReader();
                reader.Read();
                count = ((int)(reader.GetValue(0)));
                reader.Close();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                throw e;
            }
            if (count % itemsPerPage != 0)
                return System.Convert.ToInt32(count / itemsPerPage) + 1;
            else
                return System.Convert.ToInt32(count / itemsPerPage);
        }

        public override ArrayList GetItems(DataSet currentDataSet, int pageNr, int itemsPerPage, IDbConnection conn)
        {
            ArrayList rows = new ArrayList();
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #ItemsID (IDDeposito BIGINT, Designacao NVARCHAR(200)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO #ItemsID SELECT IDDeposito, Designacao FROM #OrderedItems WHERE seq_id >= @seq_id1 AND seq_id <= @seq_id2 ORDER BY seq_id";
                command.Parameters.AddWithValue("@seq_id1", (pageNr - 1) * itemsPerPage + 1);
                command.Parameters.AddWithValue("@seq_id2", pageNr * itemsPerPage);
                command.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Deposito"],
                        "INNER JOIN #ItemsID ON #ItemsID.IDDeposito = Deposito.ID ");
                    da.Fill(currentDataSet, "Deposito");
                }

                command.CommandText = "SELECT * FROM #ItemsID";
                SqlDataReader reader = command.ExecuteReader();

                ArrayList row;
                while (reader.Read())
                {
                    row = new ArrayList();
                    row.Add(reader.GetInt64(0));
                    row.Add(reader.GetString(1));
                    rows.Add(row);
                }
                reader.Close();
            }
            return rows;
        }

        public override void DeleteTemporaryResults(IDbConnection conn)
        {
            using (SqlCommand command = new SqlCommand("DROP TABLE #ItemsID; DROP TABLE #OrderedItems;", (SqlConnection)conn))
            {
                command.ExecuteNonQuery();
            }
        }
        #endregion

        #region " MasterPanelDepositos "
        public override double GetMetrosLinearesTotais(long depositoID, IDbConnection conn)
        {
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.CommandText = "SELECT SUM(MetrosLineares) FROM Deposito WHERE isDeleted = @isDeleted";
                if (depositoID != long.MinValue)
                {
                    command.Parameters.AddWithValue("@depositoID", depositoID);
                    command.CommandText += " AND ID = @depositoID";
                }

                var result = command.ExecuteScalar();

                return result == DBNull.Value ? 0 : System.Convert.ToDouble(result);
            }
        }

        public override double GetMetrosLinearesOcupados(long depositoID, IDbConnection conn)
        {
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.CommandText = this.Build_SQL_Largura_todas_ufs_nao_eliminadas(depositoID);

                double largura_todas_ufs_naoElm = 0;
                object obj_largura_todas_ufs_nao_elm = command.ExecuteScalar();
                if (obj_largura_todas_ufs_nao_elm != DBNull.Value)
                    largura_todas_ufs_naoElm = System.Convert.ToDouble(obj_largura_todas_ufs_nao_elm);

                return largura_todas_ufs_naoElm;
            }
        }

        public override Info_UFs_Larguras Get_Info_UFs_Larguras(long depositoID, IDbConnection conn)
        {
            Info_UFs_Larguras ret = new Info_UFs_Larguras();

            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.CommandText = this.Build_SQL_GetTotalUFs(depositoID);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.GetValue(0) != DBNull.Value)
                        ret.TotalUFs = Convert.ToInt64(reader.GetValue(0));
                    if (reader.GetValue(1) != DBNull.Value)
                        ret.TotalUFs_semLargura = Convert.ToInt64(reader.GetValue(1));
                    if (reader.GetValue(2) != DBNull.Value)     // Media da largura
                        ret.Media_largura = Convert.ToDouble(reader.GetValue(2));
                }
                reader.Close();
            }

            return ret;
        }
        #endregion

        #region " MasterPanelDepositos "
        public override void LoadDepositoData(DataSet currentDataSet, long depositoID, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@depositoID", depositoID);
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Deposito"],
                    "WHERE ID = @depositoID");
                da.Fill(currentDataSet, "Deposito");
            }
        }

        public override void LoadDepositodDataForUpdate(DataSet currentDataSet, long depositoID, IDbTransaction tran)
        {
            using (SqlCommand command = new SqlCommand("", (SqlConnection)tran.Connection, (SqlTransaction)tran))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@depositoID", depositoID);
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Deposito"],
                    " WITH (UPDLOCK) WHERE ID = @depositoID");
                da.Fill(currentDataSet, "Deposito");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelUnidadeFisica"],
                    " WITH (UPDLOCK) WHERE IDDeposito = @depositoID");
                da.Fill(currentDataSet, "NivelUnidadeFisica");
            }
        }

        public override bool CanDeleteDeposito(long depositoID, IDbTransaction tran)
        {
            using (var command = new SqlCommand(string.Empty, (SqlConnection)tran.Connection, (SqlTransaction)tran))
            {
                command.CommandText = string.Format(@"
                SELECT COUNT(ID) 
                FROM Deposito dep WITH (UPDLOCK) 
                WHERE dep.ID = {0}
	                AND dep.isDeleted = 0", depositoID);
                return System.Convert.ToInt64(command.ExecuteScalar()) > 0;
            }
        }
        #endregion

        #region " PanelDepIdentificao "
        private HashSet<UFRule.UnidadeFisicaInfo> GetUFsInfo(string appendWhereClause, IDbConnection conn)
        {
            var result = new HashSet<UFRule.UnidadeFisicaInfo>();

            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.CommandText = string.Format(@"
                SELECT n.ID, nED.Codigo + '/' + n.Codigo, nd.Designacao, ta.Designacao, COALESCE(df.MedidaAltura, 0), 
	                COALESCE(df.MedidaLargura, 0), COALESCE(df.MedidaProfundidade, 0), COALESCE(tm.Designacao, ''), c.Cota, 
                    COALESCE(dt.FimAno, ''), COALESCE(dt.FimMes, ''), COALESCE(dt.FimDia, ''), COALESCE(dt.InicioAno, ''), 
                    COALESCE(dt.InicioMes, ''), COALESCE(dt.InicioDia, ''), COALESCE(nuf.Eliminado, 0)
                FROM NivelUnidadeFisica nuf
	                INNER JOIN NivelDesignado nd ON nd.ID = nuf.ID AND nd.isDeleted = 0
	                INNER JOIN Nivel n ON n.ID = nd.ID AND n.isDeleted = 0
	                INNER JOIN RelacaoHierarquica rh ON rh.ID = n.ID AND rh.isDeleted = 0
	                INNER JOIN Nivel nED ON nED.ID = rh.IDUpper AND nED.isDeleted = 0
	                INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                    LEFT JOIN NivelUnidadeFisicaDeposito ON NivelUnidadeFisicaDeposito.IDNivelUnidadeFisica = nuf.ID AND NivelUnidadeFisicaDeposito.isDeleted = 0
	                LEFT JOIN SFRDDatasProducao dt ON dt.IDFRDBase = frd.ID AND dt.isDeleted = 0
	                LEFT JOIN SFRDUFDescricaoFisica df ON df.IDFRDBase = frd.ID AND df.isDeleted = 0
	                LEFT JOIN TipoAcondicionamento ta ON ta.ID = df.IDTipoAcondicionamento AND ta.isDeleted = 0
                    LEFT JOIN TipoMedida tm ON tm.ID = df.IDTipoMedida AND df.isDeleted = 0
	                LEFT JOIN SFRDUFCota c ON c.IDFRDBase = frd.ID AND c.isDeleted = 0
                WHERE nuf.isDeleted = 0 {0}", appendWhereClause);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var uf = new UFRule.UnidadeFisicaInfo();
                    uf.ID = reader.GetInt64(0);
                    uf.Codigo = reader.GetString(1);
                    uf.Designacao = reader.GetString(2);
                    uf.Tipo = reader.GetString(3);
                    uf.Altura = System.Convert.ToDecimal(reader.GetValue(4));
                    uf.Largura = System.Convert.ToDecimal(reader.GetValue(5));
                    uf.Profundidade = System.Convert.ToDecimal(reader.GetValue(6));
                    uf.Medida = reader.GetString(7);
                    uf.Cota = reader.GetString(8);
                    uf.FimAno = reader.GetString(9);
                    uf.FimMes = reader.GetString(10);
                    uf.FimDia = reader.GetString(11);
                    uf.InicioAno = reader.GetString(12);
                    uf.InicioMes = reader.GetString(13);
                    uf.InicioDia = reader.GetString(14);
                    uf.Eliminado = System.Convert.ToInt16(reader.GetValue(15)) == 0 ? false : true;
                    result.Add(uf);
                }
                reader.Close();
            }

            return result;
        }

        public override HashSet<UFRule.UnidadeFisicaInfo> LoadDepIdentificacaoData(DataSet currentDataSet, long depositoID, IDbConnection conn)
        {
            LoadDepositoData(currentDataSet, depositoID, conn);

            var result = GetUFsInfo("AND NivelUnidadeFisicaDeposito.IDDeposito = " + depositoID.ToString(), conn);

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@depositoID", depositoID);
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Deposito"],
                    "WHERE Deposito.ID = @depositoID");
                da.Fill(currentDataSet, "Deposito");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelUnidadeFisica"],
                    "INNER JOIN NivelUnidadeFisicaDeposito nufd ON nufd.IDNivelUnidadeFisica = NivelUnidadeFisica.ID " +
                    "WHERE nufd.IDDeposito = @depositoID");
                da.Fill(currentDataSet, "NivelUnidadeFisica");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelUnidadeFisicaDeposito"],
                    "WHERE NivelUnidadeFisicaDeposito.IDDeposito = @depositoID");
                da.Fill(currentDataSet, "NivelUnidadeFisicaDeposito");
            }

            return result;
        }

        public override HashSet<UFRule.UnidadeFisicaInfo> LoadUFData(long nivelID, IDbConnection conn)
        {
            return GetUFsInfo("AND nuf.ID = " + nivelID.ToString(), conn);
        }
        #endregion

        #region " PanelDepUFEliminadas "
        public override void LoadAutosEliminacao(DataSet currentDataSet, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["AutoEliminacao"]);
                da.Fill(currentDataSet, "AutoEliminacao");
            }
        }
        #endregion

        #region " SlavePanelPermissoesDesposito "
        public override void LoadDepositosPermissionsData(DataSet currentDataSet, long trusteeID, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Trustee"]);
                da.Fill(currentDataSet, "Trustee");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeUser"]);
                da.Fill(currentDataSet, "TrusteeUser");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeGroup"]);
                da.Fill(currentDataSet, "TrusteeGroup");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["UserGroups"]);
                da.Fill(currentDataSet, "UserGroups");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Deposito"]);
                da.Fill(currentDataSet, "Deposito");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeDepositoPrivilege"]);
                da.Fill(currentDataSet, "TrusteeDepositoPrivilege");
            }
        }
        #endregion
    }
}
