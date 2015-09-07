using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Data.SqlClient;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
    class SqlClientAutoEliminacaoRule : AutoEliminacaoRule
    {
        public override void CalculateOrderedItems(string FiltroDesignacao, IDbConnection conn) {
            string where = " WHERE isDeleted = 0 ";
            
            if (FiltroDesignacao.Length > 0)
                where += " AND " + FiltroDesignacao;

            SqlCommand command = new SqlCommand("", (SqlConnection)conn);
            command.CommandText = "CREATE TABLE #OrderedItems (seq_id INT IDENTITY(1,1) NOT NULL, ID BIGINT NOT NULL, Designacao NVARCHAR(400) NOT NULL ); ";
            command.ExecuteNonQuery();
            command.CommandText = String.Format("INSERT INTO #OrderedItems  SELECT ID, Designacao FROM AutoEliminacao {0}  ORDER BY Designacao ", where);
            command.ExecuteNonQuery();
        }

        public override ArrayList GetItems(DataSet currentDataSet, int pageNr, int itemsPerPage, IDbConnection conn) {
            ArrayList rows = new ArrayList();
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #ItemsID (ID BIGINT, Designacao NVARCHAR(768))";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO #ItemsID SELECT ID, Designacao FROM #OrderedItems WHERE seq_id >= @seq_id1 AND seq_id <= @seq_id2 ORDER BY seq_id";
                command.Parameters.AddWithValue("@seq_id1", (pageNr - 1) * itemsPerPage + 1);
                command.Parameters.AddWithValue("@seq_id2", pageNr * itemsPerPage);
                command.ExecuteNonQuery();
                command.Parameters.Clear();

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["AutoEliminacao"]);
                    da.Fill(currentDataSet, "AutoEliminacao");
                }

                command.CommandText = "SELECT * FROM #ItemsID";
                SqlDataReader reader = command.ExecuteReader();

                ArrayList row;
                while (reader.Read())
                {
                    row = new ArrayList();
                    row.Add(reader.GetValue(0));
                    row.Add(reader.GetValue(1));
                    rows.Add(row);
                }
                reader.Close();
            }
            return rows;
        }

        public override void DeleteTemporaryResults(IDbConnection conn) {
            using (SqlCommand command = new SqlCommand("DROP TABLE #ItemsID; DROP TABLE #OrderedItems; ", (SqlConnection)conn))
            {
                command.ExecuteNonQuery();
            }
        }

        public override void LoadAutoEliminacao(DataSet currentDataSet, long aeID, IDbConnection conn) {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@aeID", aeID);
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["AutoEliminacao"],
                    "WHERE ID=@aeID");
                da.Fill(currentDataSet, "AutoEliminacao");
            }
        }

        public override void LoadAutoEliminacaoUFsID(DataSet currentDataSet, long aeID, IDbConnection conn)
        {
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #NiveisTemp (IDNivel bigint); ";
                command.ExecuteNonQuery();

                command.CommandText =
                    "INSERT INTO #NiveisTemp " +
                    "SELECT nUFs.ID " +
                    "FROM AutoEliminacao ae " +
                    "LEFT JOIN SFRDUFAutoEliminacao srfdafae ON  srfdafae.IDAutoEliminacao = ae.ID AND srfdafae.isDeleted = @isDeleted  " +
                    "LEFT JOIN FRDBase frd ON frd.ID = srfdafae.IDFRDBase AND frd.isDeleted = @isDeleted " +
                    "LEFT JOIN Nivel nUFs on nUFs.ID = frd.IDNivel AND nUFs.isDeleted = @isDeleted  " +
                    "WHERE ae.ID = @aeID AND ae.isDeleted = 0 " +
                    " UNION " +
                    "SELECT nUFs.ID " +
                    "FROM AutoEliminacao ae " +
                    "LEFT JOIN SFRDAvaliacao sfrda ON sfrda.IDAutoEliminacao = ae.ID AND sfrda.isDeleted = @isDeleted " +
                    "LEFT JOIN SFRDUFAutoEliminacao srfdafae ON  srfdafae.IDAutoEliminacao = ae.ID AND srfdafae.isDeleted = @isDeleted " +
                    "LEFT JOIN FRDBase frd ON frd.ID = sfrda.IDFRDBase AND sfrda.isDeleted = @isDeleted " +
                    "INNER JOIN SFRDUnidadeFisica sfrduf ON sfrduf.IDFRDBase = frd.ID AND sfrduf.isDeleted = @isDeleted " +
                    "LEFT JOIN Nivel nUFs on nUFs.ID = sfrduf.IDNivel AND nUFs.isDeleted = @isDeleted " +
                    "WHERE ae.ID = @aeID AND ae.isDeleted = @isDeleted";
                command.Parameters.AddWithValue("@aeID", aeID);
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.ExecuteNonQuery();
            }
        }

        public override List<AutoEliminacao_UFsEliminadas> LoadUnidadesFisicasAvaliadas(DataSet currentDataSet, long aeID, IDbConnection conn) {
            this.LoadAutoEliminacaoUFsID(currentDataSet, aeID, (SqlConnection)conn);

            List<AutoEliminacao_UFsEliminadas> rows = new List<AutoEliminacao_UFsEliminadas>();

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "INNER JOIN #NiveisTemp ON #NiveisTemp.IDNivel = Nivel.ID ");
                da.Fill(currentDataSet, "Nivel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"],
                    "INNER JOIN #NiveisTemp ON #NiveisTemp.IDNivel = NivelDesignado.ID ");
                da.Fill(currentDataSet, "NivelDesignado");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["LocalConsulta"]);
                da.Fill(currentDataSet, "LocalConsulta");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelUnidadeFisica"],
                    "INNER JOIN #NiveisTemp ON #NiveisTemp.IDNivel = NivelUnidadeFisica.ID ");
                da.Fill(currentDataSet, "NivelUnidadeFisica");

                command.CommandText =
                "SELECT temp.IDNivel, nEDs.Codigo + '/' + nUFs.Codigo Codigo, ndUFs.Designacao, COALESCE(nuf.Eliminado, 0), df.MedidaAltura, df.MedidaLargura, df.MedidaProfundidade, tp.Designacao " +
                "FROM #NiveisTemp temp " +
                    "INNER JOIN Nivel nUFs ON nUFs.ID = temp.IDNivel AND nUFs.isDeleted = @isDeleted " +
                    "INNER JOIN NivelDesignado ndUFs ON ndUFs.ID = temp.IDNivel AND ndUFs.isDeleted = @isDeleted  " +
                    "INNER JOIN RelacaoHierarquica rh ON rh.ID = temp.IDNivel AND rh.isDeleted = @isDeleted  " +
                    "INNER JOIN Nivel nEDs ON nEDs.ID = rh.IDUpper AND nEDs.isDeleted = @isDeleted " +
                    "LEFT JOIN NivelUnidadeFisica nuf ON nuf.ID = temp.IDNivel AND nuf.isDeleted = @isDeleted " +
                    "LEFT JOIN FRDBase frd ON frd.IDNivel = nUFs.ID AND frd.isDeleted = @isDeleted " +
                    "LEFT JOIN SFRDUFDescricaoFisica df ON df.IDFRDBase = frd.ID AND df.isDeleted = @isDeleted " +
                    "LEFT JOIN TipoMedida tp ON tp.ID = df.IDTipoMedida AND tp.isDeleted = @isDeleted";

                SqlDataReader reader = command.ExecuteReader();
                AutoEliminacao_UFsEliminadas ae;
                while (reader.Read())
                {
                    ae = new AutoEliminacao_UFsEliminadas();
                    ae.IDNivel = reader.GetInt64(0);    // IDNivel
                    ae.codigo = reader.GetString(1);    // Codigo
                    ae.designacao = reader.GetString(2);    // Designacao
                    ae.paraEliminar = Convert.ToBoolean(reader.GetValue(3));    // Eliminar
                    if (reader.GetValue(4) != DBNull.Value)
                        ae.altura = System.Convert.ToDecimal(reader.GetValue(4));  // Altura
                    if (reader.GetValue(5) != DBNull.Value)
                        ae.largura = System.Convert.ToDecimal(reader.GetValue(5)); // Largura
                    if (reader.GetValue(6) != DBNull.Value)
                        ae.profundidade = System.Convert.ToDecimal(reader.GetValue(6));    // Profundidade
                    ae.tipoMedida = reader.GetValue(7).ToString();    // TipoMedida

                    rows.Add(ae);
                }
                reader.Close();

                command.CommandText = "DROP TABLE #NiveisTemp; ";
                command.ExecuteNonQuery();
            }

            return rows;
        }

        #region " PARA APAGAR QUANDO FOR RETIRADO O MÓDULO DE DEPÓSITOS ANTIGO "
        public override double GetMetrosLinearesOcupados(IDbConnection conn)
        {
            SqlCommand command = new SqlCommand("", (SqlConnection)conn);
            command.CommandText = this.Build_SQL_Largura_todas_ufs_nao_eliminadas();

            double largura_todas_ufs_naoElm = 0;
            object obj_largura_todas_ufs_nao_elm = command.ExecuteScalar();
            if (obj_largura_todas_ufs_nao_elm != DBNull.Value)
                largura_todas_ufs_naoElm = System.Convert.ToDouble(obj_largura_todas_ufs_nao_elm);

            return largura_todas_ufs_naoElm;
        }

        public override Info_UFs_Larguras Get_Info_UFs_Larguras(IDbConnection conn)
        {
            SqlCommand command = new SqlCommand("", (SqlConnection)conn);
            command.CommandText = this.Build_SQL_GetTotalUFs();
            SqlDataReader reader = command.ExecuteReader();

            Info_UFs_Larguras ret = new Info_UFs_Larguras();

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

            return ret;
        }
        #endregion
    }
}
