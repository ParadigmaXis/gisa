using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
    class SqlClientImportRule : ImportRule
    {
        public override void LoadDocumentos(DataSet currentDataSet, string[] designacoes, IDbConnection conn)
        {
            GisaDataSetHelperRule.ImportDesignacoes(designacoes, conn);

            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #aux (ID BIGINT, Termo NVARCHAR(768))";
                command.ExecuteNonQuery();

                command.Parameters.AddWithValue("@isDeleted", 0);

                command.CommandText = @"
                    INSERT INTO #aux
                    SELECT n.ID, nd.Designacao
                    FROM Nivel n
                        INNER JOIN #temp ON #temp.Designacao = n.ID
                        INNER JOIN NivelDesignado nd ON nd.ID = n.ID AND nd.isDeleted = @isDeleted
                    WHERE n.isDeleted = @isDeleted";
                command.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN #aux ON #aux.ID = Nivel.ID ");
                    da.Fill(currentDataSet, "Nivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"],
                        "INNER JOIN #aux ON #aux.ID = NivelDesignado.ID ");
                    da.Fill(currentDataSet, "NivelDesignado");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                        "INNER JOIN #aux ON #aux.ID = RelacaoHierarquica.ID ");
                    da.Fill(currentDataSet, "RelacaoHierarquica");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN RelacaoHierarquica ON RelacaoHierarquica.IDUpper = Nivel.ID " +
                        "INNER JOIN #aux ON #aux.ID = RelacaoHierarquica.ID ");
                    da.Fill(currentDataSet, "Nivel");
                }

                command.CommandText = "DROP TABLE #aux";
                command.ExecuteNonQuery();
            }
        }

        public override void LoadUnidadesFisicas(DataSet currentDataSet, string[] codigos, IDbConnection conn)
        {
            GisaDataSetHelperRule.ImportDesignacoes(codigos, conn);

            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #aux (ID BIGINT, Termo NVARCHAR(50))";
                command.ExecuteNonQuery();

                command.Parameters.AddWithValue("@isDeleted", 0);
                command.CommandText = @"
                    INSERT INTO #aux
                    SELECT n.ID, n.Codigo
                    FROM Nivel n
                        INNER JOIN #temp ON #temp.Designacao COLLATE LATIN1_GENERAL_CS_AS LIKE n.Codigo COLLATE LATIN1_GENERAL_CS_AS
                    WHERE n.isDeleted = @isDeleted";
                command.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN #aux ON #aux.ID = Nivel.ID ");
                    da.Fill(currentDataSet, "Nivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"],
                        "INNER JOIN #aux ON #aux.ID = NivelDesignado.ID ");
                    da.Fill(currentDataSet, "NivelDesignado");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                        "INNER JOIN #aux ON #aux.ID = FRDBase.IDNivel ");
                    da.Fill(currentDataSet, "FRDBase");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                        "INNER JOIN #aux ON #aux.ID = RelacaoHierarquica.ID ");
                    da.Fill(currentDataSet, "RelacaoHierarquica");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN RelacaoHierarquica ON RelacaoHierarquica.IDUpper = Nivel.ID " +
                        "INNER JOIN #aux ON #aux.ID = RelacaoHierarquica.ID ");
                    da.Fill(currentDataSet, "Nivel");
                }

                command.CommandText = "DROP TABLE #aux";
                command.ExecuteNonQuery();
            }
        }

        public override void LoadControloAuts(DataSet currentDataSet, string[] designacoes, IDbConnection conn)
        {
            GisaDataSetHelperRule.ImportDesignacoes(designacoes, conn);

            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #aux (ID BIGINT, Termo NVARCHAR(768));";
                command.ExecuteNonQuery();

                command.Parameters.AddWithValue("@isDeleted", 0);
                command.CommandText =
                    "INSERT INTO #aux " +
                    "SELECT ID, Termo " +
                    "FROM Dicionario " +
                        "INNER JOIN #temp ON #temp.Designacao COLLATE LATIN1_GENERAL_CS_AS LIKE Termo COLLATE LATIN1_GENERAL_CS_AS " +
                    "WHERE isDeleted = @isDeleted";
                command.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"],
                        "INNER JOIN #aux ON #aux.ID = Dicionario.ID ");
                    da.Fill(currentDataSet, "Dicionario");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],
                        "INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = ControloAut.ID AND cad.isDeleted = @isDeleted " +
                        "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted = @isDeleted " +
                        "INNER JOIN #aux ON #aux.ID = d.ID ");
                    da.Fill(currentDataSet, "ControloAut");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"],
                        "INNER JOIN Dicionario d ON d.ID = ControloAutDicionario.IDDicionario AND d.isDeleted = @isDeleted " +
                        "INNER JOIN #aux ON #aux.ID = d.ID ");
                    da.Fill(currentDataSet, "ControloAutDicionario");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN NivelControloAut nca ON nca.ID = Nivel.ID AND nca.isDeleted = @isDeleted " +
                        "INNER JOIN ControloAut ca ON ca.ID = nca.IDControloAut AND ca.isDeleted = @isDeleted " +
                        "INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = ca.ID AND cad.isDeleted = @isDeleted " +
                        "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted = @isDeleted " +
                        "INNER JOIN #aux ON #aux.ID = d.ID ");
                    da.Fill(currentDataSet, "Nivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelControloAut"],
                        "INNER JOIN ControloAut ca ON ca.ID = NivelControloAut.IDControloAut AND ca.isDeleted = @isDeleted " +
                        "INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = ca.ID AND cad.isDeleted = @isDeleted " +
                        "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted = @isDeleted " +
                        "INNER JOIN #aux ON #aux.ID = d.ID ");
                    da.Fill(currentDataSet, "NivelControloAut");
                }

                command.CommandText = "DROP TABLE #aux";
                command.ExecuteNonQuery();
            }
        }
    }
}
