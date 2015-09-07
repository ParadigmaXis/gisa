using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
    public sealed class SqlClientIntGestDocRule: IntGestDocRule
    {
        private void LoadEntidadeExterna(DataSet currentDataSet, long id, int IDSistema, int IDTipoEntidade, IDbConnection conn)
        {
            string whereClause = @"WHERE ID = @id AND IDSistema = @IDSistema AND IDTipoEntidade = @IDTipoEntidade AND isDeleted = @isDeleted";

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@IDSistema", IDSistema);
                command.Parameters.AddWithValue("@IDTipoEntidade", IDTipoEntidade);                

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Integ_EntidadeExterna"],
                    whereClause);
                da.Fill(currentDataSet, "Integ_EntidadeExterna");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "INNER JOIN Integ_RelacaoExternaNivel ON Integ_RelacaoExternaNivel.IDNivel = Nivel.ID " +
                    whereClause);
                da.Fill(currentDataSet, "Nivel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"],
                    "INNER JOIN Integ_RelacaoExternaNivel ON Integ_RelacaoExternaNivel.IDNivel = Nivel.ID " +
                    whereClause);
                da.Fill(currentDataSet, "NivelDesignado");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],
                    "INNER JOIN Integ_RelacaoExternaControloAut ON Integ_RelacaoExternaControloAut.IDControloAut = ControloAut.ID " +
                    whereClause);
                da.Fill(currentDataSet, "ControloAut");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"],
                    "INNER JOIN ControloAutDicionario ON ControloAutDicionario.IDDicionario = Dicionario.ID AND ControloAutDicionario.isDeleted = @isDeleted " +
                    "INNER JOIN ControloAut ON ControloAut.ID = ControloAutDicionario.IDControloAut AND ControloAut.isDeleted = @isDeleted " +
                    "INNER JOIN Integ_RelacaoExternaControloAut ON Integ_RelacaoExternaControloAut.IDControloAut = ControloAut.ID " +
                    whereClause);
                da.Fill(currentDataSet, "Dicionario");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"],
                    "INNER JOIN ControloAut ON ControloAut.ID = ControloAutDicionario.IDControloAut AND ControloAut.isDeleted = @isDeleted " +
                    "INNER JOIN Integ_RelacaoExternaControloAut ON Integ_RelacaoExternaControloAut.IDControloAut = ControloAut.ID " +
                    whereClause);
                da.Fill(currentDataSet, "ControloAutDicionario");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Integ_RelacaoExternaNivel"],
                    whereClause);
                da.Fill(currentDataSet, "Integ_RelacaoExternaNivel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Integ_RelacaoExternaControloAut"],
                    whereClause);
                da.Fill(currentDataSet, "Integ_RelacaoExternaControloAut");
            }
        }

        public bool VerifyDocs(long[] docIDs, IDbTransaction tran)
        {
            GisaDataSetHelperRule.ImportIDs(docIDs, tran);

            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)tran.Connection, (SqlTransaction)tran);
            command.CommandText =
                "SELECT COUNT(n.ID) " +
                "FROM Nivel n WITH (UPDLOCK) " +
                    "INNER JOIN #temp ON #temp.ID = n.ID " +
                "WHERE n.isDeleted = 0";
            long count = System.Convert.ToInt64(command.ExecuteScalar());

            return count == docIDs.Length;
        }

        public bool VerifyCAs(long[] caIDs, IDbTransaction tran)
        {
            GisaDataSetHelperRule.ImportIDs(caIDs, tran);

            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)tran.Connection, (SqlTransaction)tran);
            command.CommandText =
                "SELECT COUNT(ca.ID) " +
                "FROM ControloAut ca WITH (UPDLOCK) " +
                    "INNER JOIN #temp ON #temp.ID = ca.ID " +
                "WHERE ca.isDeleted = 0";
            long count = System.Convert.ToInt64(command.ExecuteScalar());

            return count == caIDs.Length;
        }

        public override long GetSerie(DataSet currentDataSet, long ID, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            {
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@IDTipoNivelRelacionado6", 6);
                command.Parameters.AddWithValue("@IDTipoNivelRelacionado7", 7);
                command.CommandText = @"
                    WITH Temp (ID, IDUpper, IDTipoNivelRelacionado)
                    AS
                    (
	                    SELECT ID, IDUpper, IDTipoNivelRelacionado FROM RelacaoHierarquica WHERE ID = @ID
	                    UNION ALL
                    	
	                    SELECT RelacaoHierarquica.ID, RelacaoHierarquica.IDUpper, RelacaoHierarquica.IDTipoNivelRelacionado
	                    FROM RelacaoHierarquica
		                    INNER JOIN Temp ON Temp.IDUpper = RelacaoHierarquica.ID
	                    WHERE RelacaoHierarquica.IDTipoNivelRelacionado > @IDTipoNivelRelacionado6
                    )
                    SELECT Temp.ID 
                    FROM Temp 
                        INNER JOIN NivelDesignado ON NivelDesignado.ID = Temp.ID
                    WHERE IDTipoNivelRelacionado = @IDTipoNivelRelacionado7";

                var serie = command.ExecuteScalar();
                if (serie != null)
                {
                    serie = (long)serie;
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        command.Parameters.AddWithValue("@serie", serie);
                        da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                            "WHERE ID = @serie");
                        da.Fill(currentDataSet, "Nivel");

                        da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"],
                            "WHERE ID = @serie");
                        da.Fill(currentDataSet, "NivelDesignado");
                    }
                }
                else
                    serie = -1;

                return System.Convert.ToInt64(serie);
            }
        }

        public override List<DocGisaInfo> LoadDocsCorrespondenciasAnteriores(DataSet currentDataSet, List<EntidadeExterna> docsExternos, int IDTipoEntidade, IDbConnection conn)
        {
            DataTable t = new DataTable();
            DataColumn c1 = new DataColumn("IDExterno", typeof(string));
            DataColumn c2= new DataColumn("IDSistema", typeof(int));
            t.Columns.Add(c1);
            t.Columns.Add(c2);

            //var a = new StringBuilder();

            foreach (var doc in docsExternos)
            {
                DataRow dr = t.NewRow();
                dr[0] = doc.IDExterno;
                dr[1] = doc.Sistema;
                t.Rows.Add(dr);

                //a.AppendLine("INSERT INTO #temp VALUES ('" + doc.IDExterno + "', " + doc.Sistema + ");");
            }

            List<DocGisaInfo> result = new List<DocGisaInfo>();

            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.CommandText =
                    "CREATE TABLE #Relacoes(IDEntidadeExterna BIGINT, IDNivel BIGINT);" +
                    "CREATE TABLE #temp(IDExterno NVARCHAR(200) COLLATE Latin1_General_CS_AS NOT NULL, IDSistema INT);";
                command.ExecuteNonQuery();

                command.CommandText = string.Format(@"
                    SELECT * INTO #temp FROM @Integ_DocExterno;
                    INSERT INTO #Relacoes
                    SELECT re.IDEntidadeExterna, re.IDNivel
                    FROM #temp 
	                    INNER JOIN Integ_EntidadeExterna ee ON ee.IDExterno COLLATE Latin1_General_CS_AS = #temp.IDExterno COLLATE Latin1_General_CS_AS
		                    AND ee.IDSistema = #temp.IDSistema
		                    AND ee.IDTipoEntidade = {0}
	                    INNER JOIN Integ_RelacaoExternaNivel re ON re.IDEntidadeExterna = ee.ID
                    GROUP BY re.IDEntidadeExterna, re.IDNivel", IDTipoEntidade);
                SqlParameter paramIds = command.Parameters.AddWithValue("@Integ_DocExterno", t);
                paramIds.SqlDbType = SqlDbType.Structured;
                paramIds.TypeName = "Integ_DocExterno";
                command.ExecuteNonQuery();
                command.Parameters.Clear();

                string innerJoinEntExt =
                    "INNER JOIN (SELECT DISTINCT IDEntidadeExterna FROM #Relacoes) ee ON ee.IDEntidadeExterna = Integ_EntidadeExterna.ID ";
                string innerJoinNivelDoc =
                    "INNER JOIN (SELECT DISTINCT IDNivel FROM #Relacoes) ee ON ee.IDNivel = {0}.{1} ";

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    command.CommandType = CommandType.Text;
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Integ_EntidadeExterna"],
                        innerJoinEntExt);
                    da.Fill(currentDataSet, "Integ_EntidadeExterna");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        string.Format(innerJoinNivelDoc, "Nivel", "ID"));
                    da.Fill(currentDataSet, "Nivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Integ_RelacaoExternaNivel"],
                        "INNER JOIN #Relacoes ON #Relacoes.IDEntidadeExterna = Integ_RelacaoExternaNivel.IDEntidadeExterna AND #Relacoes.IDNivel = Integ_RelacaoExternaNivel.IDNivel ");
                    da.Fill(currentDataSet, "Integ_RelacaoExternaNivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"],
                        string.Format(innerJoinNivelDoc, "NivelDesignado", "ID"));
                    da.Fill(currentDataSet, "NivelDesignado");

                    if (IDTipoEntidade >= 6)
                    {
                        // nivel upper do nivel documental
                        da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN RelacaoHierarquica ON RelacaoHierarquica.IDUpper = Nivel.ID " +
                        string.Format(innerJoinNivelDoc, "RelacaoHierarquica", "ID"));
                        da.Fill(currentDataSet, "Nivel");

                        da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"],
                            "INNER JOIN RelacaoHierarquica ON RelacaoHierarquica.IDUpper = NivelDesignado.ID " +
                            string.Format(innerJoinNivelDoc, "RelacaoHierarquica", "ID"));
                        da.Fill(currentDataSet, "NivelDesignado");

                        da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                            string.Format(innerJoinNivelDoc, "RelacaoHierarquica", "ID"));
                        da.Fill(currentDataSet, "RelacaoHierarquica");

                        // nuvem FRDBase do nivel documental
                        da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                            string.Format(innerJoinNivelDoc, "FRDBase", "IDNivel"));
                        da.Fill(currentDataSet, "FRDBase");

                        da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDDatasProducao"],
                            "INNER JOIN FRDBase ON FRDBase.ID = SFRDDatasProducao.IDFRDBase " +
                            string.Format(innerJoinNivelDoc, "FRDBase", "IDNivel"));
                        da.Fill(currentDataSet, "SFRDDatasProducao");

                        da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDConteudoEEstrutura"],
                            "INNER JOIN FRDBase ON FRDBase.ID = SFRDConteudoEEstrutura.IDFRDBase " +
                            string.Format(innerJoinNivelDoc, "FRDBase", "IDNivel"));
                        da.Fill(currentDataSet, "SFRDConteudoEEstrutura");

                        da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDCondicaoDeAcesso"],
                            "INNER JOIN FRDBase ON FRDBase.ID = SFRDCondicaoDeAcesso.IDFRDBase " +
                            string.Format(innerJoinNivelDoc, "FRDBase", "IDNivel"));
                        da.Fill(currentDataSet, "SFRDCondicaoDeAcesso");

                        da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Codigo"],
                            "INNER JOIN FRDBase ON FRDBase.ID = Codigo.IDFRDBase " +
                            string.Format(innerJoinNivelDoc, "FRDBase", "IDNivel"));
                        da.Fill(currentDataSet, "Codigo");

                        // carregar informação referente aos controlos de autoridade associados
                        da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],
                            @"INNER JOIN (
				            SELECT DISTINCT IndexFRDCA.IDControloAut
				            FROM IndexFRDCA			
					            INNER JOIN FRDBase ON FRDBase.ID = IndexFRDCA.IDFRDBase " +
                                    string.Format(innerJoinNivelDoc, "FRDBase", "IDNivel") +
                            ") IndexFRDCA ON IndexFRDCA.IDControloAut = ControloAut.ID ");
                        da.Fill(currentDataSet, "ControloAut");

                        da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["IndexFRDCA"],
                            "INNER JOIN FRDBase ON FRDBase.ID = IndexFRDCA.IDFRDBase " +
                            string.Format(innerJoinNivelDoc, "FRDBase", "IDNivel"));
                        da.Fill(currentDataSet, "IndexFRDCA");

                        da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"],
                            "INNER JOIN (" +
                                "SELECT DISTINCT cad.IDDicionario " +
                                "FROM ControloAutDicionario cad " +
                                    "INNER JOIN ControloAut ON ControloAut.ID = cad.IDControloAut " +
                                    @"INNER JOIN (
				                    SELECT DISTINCT IndexFRDCA.IDControloAut
				                    FROM IndexFRDCA			
					                    INNER JOIN FRDBase ON FRDBase.ID = IndexFRDCA.IDFRDBase " +
                                            string.Format(innerJoinNivelDoc, "FRDBase", "IDNivel") +
                                    ") IndexFRDCA ON IndexFRDCA.IDControloAut = ControloAut.ID " +
                             ")cads ON cads.IDDicionario = Dicionario.ID");
                        da.Fill(currentDataSet, "Dicionario");

                        da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"],
                            "INNER JOIN ControloAut ON ControloAut.ID = ControloAutDicionario.IDControloAut " +
                            @"INNER JOIN (
				            SELECT DISTINCT IndexFRDCA.IDControloAut
				            FROM IndexFRDCA			
					            INNER JOIN FRDBase ON FRDBase.ID = IndexFRDCA.IDFRDBase " +
                                    string.Format(innerJoinNivelDoc, "FRDBase", "IDNivel") +
                            ") IndexFRDCA ON IndexFRDCA.IDControloAut = ControloAut.ID ");
                        da.Fill(currentDataSet, "ControloAutDicionario");
                    }
                    // carregar informação referente aos produtores
                    command.Parameters.Clear();
                    command.CommandText = "CREATE TABLE #NvlIDs (IDNivelDoc BIGINT, IDNivelProdutor BIGINT); " + 
                        "CREATE TABLE #Prods (IDNivelProdutor BIGINT); ";
                    command.ExecuteNonQuery();

                    string query = @"
                        WITH Temp (IDStart, ID, IDUpper, IDTipoNivelRelacionado)
                        AS
                        (
	                        SELECT rh.ID, rh.ID, rh.IDUpper, rh.IDTipoNivelRelacionado 
                            FROM RelacaoHierarquica rh " +
                                    string.Format(innerJoinNivelDoc, "rh", "ID") +
                              @"WHERE rh.isDeleted = @isDeleted
	                        UNION ALL
                    	
	                        SELECT Temp.IDStart, RelacaoHierarquica.ID, RelacaoHierarquica.IDUpper, RelacaoHierarquica.IDTipoNivelRelacionado
	                        FROM RelacaoHierarquica
		                        INNER JOIN Temp ON Temp.IDUpper = RelacaoHierarquica.ID
	                        WHERE RelacaoHierarquica.IDTipoNivelRelacionado > @IDTipoNivelRelacionado6
                        )
                    
                        INSERT INTO #NvlIDs
                        SELECT IDStart, IDUpper
                        FROM Temp
                        WHERE IDTipoNivelRelacionado > @IDTipoNivelRelacionado6";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    command.Parameters.AddWithValue("@IDTipoNivelRelacionado6", 6);

                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO #Prods SELECT DISTINCT IDNivelProdutor FROM #NvlIDs";
                    command.ExecuteNonQuery();

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN #Prods ON #Prods.IDNivelProdutor = Nivel.ID ");
                    da.Fill(currentDataSet, "Nivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],
                        "INNER JOIN NivelControloAut ON NivelControloAut.IDControloAut = ControloAut.ID " +
                        "INNER JOIN Nivel ON Nivel.ID = NivelControloAut.ID " +
                        "INNER JOIN #Prods ON #Prods.IDNivelProdutor = Nivel.ID ");
                    da.Fill(currentDataSet, "ControloAut");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelControloAut"],
                        "INNER JOIN #Prods ON #Prods.IDNivelProdutor = NivelControloAut.ID ");
                    da.Fill(currentDataSet, "NivelControloAut");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"],
                        "INNER JOIN ControloAutDicionario ON ControloAutDicionario.IDDicionario = Dicionario.ID " +
                        "INNER JOIN ControloAut ON ControloAut.ID = ControloAutDicionario.IDControloAut " +
                        "INNER JOIN NivelControloAut ON NivelControloAut.IDControloAut = ControloAut.ID " +
                        "INNER JOIN Nivel ON Nivel.ID = NivelControloAut.ID " +
                        "INNER JOIN #Prods ON #Prods.IDNivelProdutor = Nivel.ID ");
                    da.Fill(currentDataSet, "Dicionario");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"],
                        "INNER JOIN ControloAut ON ControloAut.ID = ControloAutDicionario.IDControloAut " +
                        "INNER JOIN NivelControloAut ON NivelControloAut.IDControloAut = ControloAut.ID " +
                        "INNER JOIN Nivel ON Nivel.ID = NivelControloAut.ID " +
                        "INNER JOIN #Prods ON #Prods.IDNivelProdutor = Nivel.ID ");
                    da.Fill(currentDataSet, "ControloAutDicionario");
                }

                command.Parameters.Clear();
                command.CommandText = "SELECT * FROM #NvlIDs ORDER BY IDNivelDoc";
                SqlDataReader reader = command.ExecuteReader();

                long currentID = -1;
                long id = 0;
                List<long> idProd = new List<long>();
                DocGisaInfo dgInfo = new DocGisaInfo();
                while (reader.Read())
                {
                    id = reader.GetInt64(0);
                    if (currentID != id)
                    {
                        currentID = id;
                        dgInfo = new DocGisaInfo();
                        dgInfo.IDNivelProdutores = new List<long>();
                        dgInfo.IDNivel = currentID;
                        result.Add(dgInfo);
                    }
                    dgInfo.IDNivelProdutores.Add(reader.GetInt64(1));
                }

                reader.Close();

                command.CommandText = "DROP TABLE #temp; DROP TABLE #Relacoes; DROP TABLE #NvlIDs; DROP TABLE #Prods";
                command.ExecuteNonQuery();
            }

            return result;
        }

        public override Dictionary<string, DocGisaInfo> LoadDocsCorrespondenciasNovas(DataSet currentDataSet, List<string> idsExternos, long IDTipoNivelRelacionado, IDbConnection conn)
        {
            DataTable t = new DataTable();
            DataColumn c1 = new DataColumn("IDExterno", typeof(string));
            DataColumn c2 = new DataColumn("IDSistema", typeof(int));
            c2.AllowDBNull = true;
            t.Columns.Add(c1);
            t.Columns.Add(c2);

            //System.Diagnostics.Trace.WriteLine("LoadDocsCorrespondenciasNovas");

            foreach (string idexterno in idsExternos)
            {
                DataRow dr = t.NewRow();
                dr[0] = idexterno;
                dr[1] = DBNull.Value;
                t.Rows.Add(dr);

                //System.Diagnostics.Trace.WriteLine("INSERT INTO #temp VALUES (" + idexterno + ")");
            }

            Dictionary<string, DocGisaInfo> result = new Dictionary<string, DocGisaInfo>();
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #temp(IDExterno NVARCHAR(200) COLLATE Latin1_General_CS_AS NOT NULL); CREATE TABLE #NiveisSugest(IDExterno NVARCHAR(200), IDNivel BIGINT);";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO #temp SELECT IDExterno FROM @Integ_DocExterno";
                SqlParameter paramIds = command.Parameters.AddWithValue("@Integ_DocExterno", t);
                paramIds.SqlDbType = SqlDbType.Structured;
                paramIds.TypeName = "Integ_DocExterno";
                command.ExecuteNonQuery();
                command.Parameters.Clear();

                command.CommandText = @"
                    INSERT INTO #NiveisSugest
                    SELECT #temp.IDExterno, n.ID
                    FROM Nivel n
                        INNER JOIN #temp ON #temp.IDExterno COLLATE Latin1_General_CS_AS = n.Codigo COLLATE Latin1_General_CS_AS
                        INNER JOIN RelacaoHierarquica rh ON rh.ID = n.ID AND rh.IDTipoNivelRelacionado = @IDTipoNivelRelacionado
                    WHERE n.isDeleted = @isDeleted";
                command.Parameters.AddWithValue("@IDTipoNivelRelacionado", IDTipoNivelRelacionado);
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(command);
                {
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN #NiveisSugest ON #NiveisSugest.IDNivel = Nivel.ID");
                    da.Fill(currentDataSet, "Nivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"],
                        "INNER JOIN #NiveisSugest ON #NiveisSugest.IDNivel = NivelDesignado.ID");
                    da.Fill(currentDataSet, "NivelDesignado");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN RelacaoHierarquica ON RelacaoHierarquica.IDUpper = Nivel.ID " +
                        "INNER JOIN #NiveisSugest ON #NiveisSugest.IDNivel = RelacaoHierarquica.ID ");
                    da.Fill(currentDataSet, "Nivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"],
                        "INNER JOIN RelacaoHierarquica ON RelacaoHierarquica.IDUpper = NivelDesignado.ID " +
                        "INNER JOIN #NiveisSugest ON #NiveisSugest.IDNivel = RelacaoHierarquica.ID ");
                    da.Fill(currentDataSet, "NivelDesignado");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                        "INNER JOIN #NiveisSugest ON #NiveisSugest.IDNivel = RelacaoHierarquica.ID");
                    da.Fill(currentDataSet, "RelacaoHierarquica");

                    StringBuilder innerQuery = new StringBuilder("INNER JOIN #NiveisSugest ON #NiveisSugest.IDNivel = FRDBase.IDNivel ");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                        innerQuery.ToString());
                    da.Fill(currentDataSet, "FRDBase");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDDatasProducao"],
                        "INNER JOIN FRDBase ON FRDBase.ID = SFRDDatasProducao.IDFRDBase " +
                        innerQuery.ToString());
                    da.Fill(currentDataSet, "SFRDDatasProducao");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Codigo"],
                        "INNER JOIN FRDBase ON FRDBase.ID = Codigo.IDFRDBase " +
                        innerQuery.ToString());
                    da.Fill(currentDataSet, "Codigo");

                    // carregar FRDBase do processo do documento (se for caso disso)
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                        "INNER JOIN RelacaoHierarquica ON RelacaoHierarquica.IDUpper = FRDBase.IDNivel " +
                        "INNER JOIN #NiveisSugest ON #NiveisSugest.IDNivel = RelacaoHierarquica.ID ");
                    da.Fill(currentDataSet, "FRDBase");

                    // carregar informação referente aos registos de autoridade associados
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["IndexFRDCA"],
                        innerQuery.Insert(0, "INNER JOIN FRDBase ON FRDBase.ID = IndexFRDCA.IDFRDBase ").ToString());
                    da.Fill(currentDataSet, "IndexFRDCA");

                    var temp = innerQuery.ToString();
                    innerQuery = new StringBuilder(string.Format("INNER JOIN (SELECT DISTINCT IndexFRDCA.IDControloAut FROM IndexFRDCA {0}) IndexFRDCA ON IndexFRDCA.IDControloAut = ControloAut.ID ", temp));

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],
                        innerQuery.ToString());
                    da.Fill(currentDataSet, "ControloAut");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"],
                        innerQuery.Insert(0, "INNER JOIN ControloAut ON ControloAut.ID = ControloAutDicionario.IDControloAut AND ControloAutDicionario.IDTipoControloAutForma = 1 ").ToString());
                    da.Fill(currentDataSet, "ControloAutDicionario");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"],
                        innerQuery.Insert(0, "INNER JOIN ControloAutDicionario ON ControloAutDicionario.IDDicionario = Dicionario.ID ").ToString());
                    da.Fill(currentDataSet, "Dicionario");

                    // carregar informação referente aos produtores
                    command.Parameters.Clear();
                    command.CommandText = "CREATE TABLE #NvlIDs (IDNivelDoc BIGINT, IDNivelProdutor BIGINT)";
                    command.ExecuteNonQuery();

                    command.CommandText = @"
                        WITH Temp (IDStart, ID, IDUpper, IDTipoNivelRelacionado)
                        AS
                        (
	                        SELECT rh.ID, rh.ID, rh.IDUpper, rh.IDTipoNivelRelacionado 
                            FROM RelacaoHierarquica rh
                                INNER JOIN #NiveisSugest ON #NiveisSugest.IDNivel = rh.ID
                            WHERE rh.isDeleted = @isDeleted
	                        UNION ALL
                    	
	                        SELECT Temp.IDStart, RelacaoHierarquica.ID, RelacaoHierarquica.IDUpper, RelacaoHierarquica.IDTipoNivelRelacionado
	                        FROM RelacaoHierarquica
		                        INNER JOIN Temp ON Temp.IDUpper = RelacaoHierarquica.ID
	                        WHERE RelacaoHierarquica.IDTipoNivelRelacionado > @IDTipoNivelRelacionado6
                        )
                    
                        INSERT INTO #NvlIDs
                        SELECT IDStart, IDUpper
                        FROM Temp
                        WHERE IDTipoNivelRelacionado > @IDTipoNivelRelacionado6";
                    command.Parameters.AddWithValue("@IDTipoNivelRelacionado6", 6);
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    command.ExecuteNonQuery();

                    innerQuery = new StringBuilder("INNER JOIN (SELECT DISTINCT IDNivelProdutor FROM #NvlIDs) NvlIDs ON NvlIDs.IDNivelProdutor = Nivel.ID ");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        innerQuery.ToString());
                    da.Fill(currentDataSet, "Nivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN (" +
                            "SELECT DISTINCT IDUpper " +
                            "FROM RelacaoHierarquica rh " +
                                "INNER JOIN Nivel ON Nivel.ID = rh.ID " +
                                innerQuery.ToString() +
                        ")ids ON ids.IDUpper = Nivel.ID");
                    da.Fill(currentDataSet, "Nivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                        "INNER JOIN Nivel ON Nivel.ID = RelacaoHierarquica.ID " +
                        innerQuery.ToString());
                    da.Fill(currentDataSet, "RelacaoHierarquica");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelControloAut"],
                        innerQuery.Insert(0, "INNER JOIN Nivel ON Nivel.ID = NivelControloAut.ID ").ToString());
                    da.Fill(currentDataSet, "NivelControloAut");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],
                        innerQuery.Insert(0, "INNER JOIN NivelControloAut ON NivelControloAut.IDControloAut = ControloAut.ID ").ToString());
                    da.Fill(currentDataSet, "ControloAut");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"],
                        innerQuery.Insert(0, "INNER JOIN ControloAut ON ControloAut.ID = ControloAutDicionario.IDControloAut AND ControloAutDicionario.IDTipoControloAutForma = 1 ").ToString());
                    da.Fill(currentDataSet, "ControloAutDicionario");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"],
                        innerQuery.Insert(0, "INNER JOIN ControloAutDicionario ON ControloAutDicionario.IDDicionario = Dicionario.ID ").ToString());
                    da.Fill(currentDataSet, "Dicionario");
                }

                var res = new Dictionary<string, long>();
                command.CommandText = @"
                SELECT #NiveisSugest.IDExterno, #NvlIDs.IDNivelDoc, #NvlIDs.IDNivelProdutor
                FROM #NiveisSugest 
                    INNER JOIN #NvlIDs ON #NvlIDs.IDNivelDoc = #NiveisSugest.IDNivel
                ORDER BY #NvlIDs.IDNivelDoc";
                SqlDataReader reader = command.ExecuteReader();

                long currentID = -1;
                long id = 0;
                DocGisaInfo dgInfo = new DocGisaInfo();
                while (reader.Read())
                {
                    id = reader.GetInt64(1);
                    if (currentID != id)
                    {
                        currentID = id;
                        dgInfo = new DocGisaInfo();
                        dgInfo.IDNivelProdutores = new List<long>();
                        dgInfo.IDNivel = currentID;
                        result[reader.GetString(0)] = dgInfo;
                    }
                    dgInfo.IDNivelProdutores.Add(reader.GetInt64(2));
                }

                reader.Close();

                command.CommandText = "DROP TABLE #temp; DROP TABLE #NvlIDs; DROP TABLE #NiveisSugest";
                command.ExecuteNonQuery();
            }

            return result;
        }

        public override void LoadRAsCorrespondenciasAnteriores(DataSet currentDataSet, List<EntidadeExterna> rasExternos, IDbConnection conn)
        {
            DataTable t = new DataTable();
            DataColumn c1 = new DataColumn("_ID", typeof(string));
            DataColumn c2 = new DataColumn("IDExterno", typeof(string));
            DataColumn c3 = new DataColumn("Titulo", typeof(string));
            DataColumn c4 = new DataColumn("IDSistema", typeof(int));
            DataColumn c5 = new DataColumn("IDTipoEntidade", typeof(int));
            t.Columns.Add(c1);
            t.Columns.Add(c2);
            t.Columns.Add(c3);
            t.Columns.Add(c4);
            t.Columns.Add(c5);

            StringBuilder str = new StringBuilder("CREATE TABLE #temp(_ID BIGINT, IDExterno NVARCHAR(200) COLLATE Latin1_General_CS_AS NOT NULL, Titulo NVARCHAR(200), IDSistema INT, IDTipoEntidade INT);");

            foreach (var ra in rasExternos)
            {
                DataRow dr = t.NewRow();
                dr[0] = "";
                dr[1] = ra.IDExterno;
                dr[2] = ra.Titulo;
                dr[3] = ra.Sistema;
                dr[4] = ra.TipoEntidade;
                t.Rows.Add(dr);

                //str.AppendLine(string.Format("INSERT INTO #temp VALUES ({0}, '{1}', '{2}', {3}, {4});", "", ra.IDExterno, ra.Titulo, ra.Sistema, ra.TipoEntidade));
            }

            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.CommandText =
                    "CREATE TABLE #temp(_ID BIGINT, IDExterno NVARCHAR(200) COLLATE Latin1_General_CS_AS NOT NULL, Titulo NVARCHAR(200), IDSistema INT, IDTipoEntidade INT);" +
                    "CREATE TABLE #Relacoes(IDEntidadeExterna BIGINT, IDControloAut BIGINT);";
                command.ExecuteNonQuery();

                command.CommandText = @"
                    SELECT * INTO #temp FROM @Integ_RAExterno;
                    INSERT INTO #Relacoes
                    SELECT re.IDEntidadeExterna, re.IDControloAut
                    FROM #temp 
	                    INNER JOIN Integ_EntidadeExterna ee ON ee.IDExterno COLLATE Latin1_General_CS_AS = #temp.IDExterno COLLATE Latin1_General_CS_AS
		                    AND ee.IDSistema = #temp.IDSistema
		                    AND ee.IDTipoEntidade = #temp.IDTipoEntidade
	                    INNER JOIN Integ_RelacaoExternaControloAut re ON re.IDEntidadeExterna = ee.ID
                    GROUP BY re.IDEntidadeExterna, re.IDControloAut";
                SqlParameter paramIds = command.Parameters.AddWithValue("@Integ_RAExterno", t);
                paramIds.SqlDbType = SqlDbType.Structured;
                paramIds.TypeName = "Integ_RAExterno";
                command.ExecuteNonQuery();
                command.Parameters.Clear();

                string innerJoinEntExt =
                    "INNER JOIN (SELECT DISTINCT IDEntidadeExterna FROM #Relacoes) ee ON ee.IDEntidadeExterna = Integ_EntidadeExterna.ID ";
                string innerJoinControloAut =
                    "INNER JOIN (SELECT DISTINCT IDControloAut FROM #Relacoes) ee ON ee.IDControloAut = {0}.{1} ";

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Integ_EntidadeExterna"],
                        innerJoinEntExt);
                    da.Fill(currentDataSet, "Integ_EntidadeExterna");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],
                        string.Format(innerJoinControloAut, "ControloAut", "ID"));
                    da.Fill(currentDataSet, "ControloAut");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Integ_RelacaoExternaControloAut"],
                        "INNER JOIN #Relacoes ON #Relacoes.IDEntidadeExterna = Integ_RelacaoExternaControloAut.IDEntidadeExterna AND #Relacoes.IDControloAut = Integ_RelacaoExternaControloAut.IDControloAut ");
                    da.Fill(currentDataSet, "Integ_RelacaoExternaControloAut");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"],
                        "INNER JOIN ControloAutDicionario ON ControloAutDicionario.IDDicionario = Dicionario.ID " +
                        "INNER JOIN ControloAut ON ControloAut.ID = ControloAutDicionario.IDControloAut " +
                        string.Format(innerJoinControloAut, "ControloAut", "ID"));
                    da.Fill(currentDataSet, "Dicionario");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"],
                        "INNER JOIN ControloAut ON ControloAut.ID = ControloAutDicionario.IDControloAut " +
                        string.Format(innerJoinControloAut, "ControloAut", "ID"));
                    da.Fill(currentDataSet, "ControloAutDicionario");

                    // só é válido para as entidades produtoras
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN NivelControloAut ON NivelControloAut.ID = Nivel.ID " +
                        "INNER JOIN ControloAut ON ControloAut.ID = NivelControloAut.IDControloAut " +
                        string.Format(innerJoinControloAut, "ControloAut", "ID"));
                    da.Fill(currentDataSet, "Nivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN (" +
                            "SELECT DISTINCT IDUpper " +
                            "FROM RelacaoHierarquica rh " +
                                "INNER JOIN Nivel n ON n.ID = rh.ID " +
                                "INNER JOIN NivelControloAut ON NivelControloAut.ID = n.ID " +
                                "INNER JOIN ControloAut ON ControloAut.ID = NivelControloAut.IDControloAut " +
                                string.Format(innerJoinControloAut, "ControloAut", "ID") +
                        ")ids ON ids.IDUpper = Nivel.ID ");
                    da.Fill(currentDataSet, "Nivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                        "INNER JOIN Nivel ON Nivel.ID = RelacaoHierarquica.ID " +
                        "INNER JOIN NivelControloAut ON NivelControloAut.ID = Nivel.ID " +
                        "INNER JOIN ControloAut ON ControloAut.ID = NivelControloAut.IDControloAut " +
                        string.Format(innerJoinControloAut, "ControloAut", "ID"));
                    da.Fill(currentDataSet, "RelacaoHierarquica");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelControloAut"],
                        "INNER JOIN ControloAut ON ControloAut.ID = NivelControloAut.IDControloAut " +
                        string.Format(innerJoinControloAut, "ControloAut", "ID"));
                    da.Fill(currentDataSet, "NivelControloAut");
                }

                command.CommandText = "DROP TABLE #temp; DROP TABLE #Relacoes;";
                command.ExecuteNonQuery();
            }
        }

        public override Dictionary<long, long> LoadRAsCorrespondenciasNovas(DataSet currentDataSet, Dictionary<long, EntidadeExterna> dalraes, IDbConnection conn)
        {
            var result = new Dictionary<long, long>();
            var cmd = @"
                INSERT INTO #ControloAutSugest
                SELECT dt._ID, dt.IDExterno, dt.Titulo, dt.IDSistema, dt.IDTipoEntidade, ca.ID IDControloAut, dAutorizado.ID IDDicionario, dAutorizado.Termo
                FROM Dicionario d
                    INNER JOIN @raes dt ON RTRIM(LTRIM(dt.Titulo)) COLLATE Latin1_General_CI_AI = d.Termo COLLATE Latin1_General_CI_AI
                    INNER JOIN ControloAutDicionario cad ON cad.IDDicionario = d.ID AND cad.isDeleted = 0
                    INNER JOIN ControloAut ca ON ca.ID = cad.IDControloAut AND ca.IDTipoNoticiaAut = dt.IDTipoEntidade AND ca.isDeleted = 0
                    INNER JOIN ControloAutDicionario cadAutorizado ON cadAutorizado.IDControloAut = ca.ID AND cadAutorizado.IDTipoControloAutForma = 1 AND cadAutorizado.isDeleted = 0
                    INNER JOIN Dicionario dAutorizado ON dAutorizado.ID = cadAutorizado.IDDicionario AND dAutorizado.isDeleted = 0
                WHERE d.isDeleted = 0
                ORDER BY IDControloAut
                ";
            var lstRAs = dalraes.Where(ra => ra.Value.TipoEntidade != 4 && ra.Value.TipoEntidade != 2).ToDictionary(p => p.Key, p => p.Value);

            CalculateCorrespRAs(cmd, lstRAs, result, conn);
            LoadData(currentDataSet, conn);

            // produtores
            cmd = @"
                INSERT INTO #ControloAutSugest
                SELECT dt._ID, dt.IDExterno, dt.Titulo, dt.IDSistema, dt.IDTipoEntidade, ca.ID IDControloAut, d.ID IDDicionario, d.Termo
                FROM @raes dt
                    INNER JOIN Nivel n ON n.Codigo COLLATE Latin1_General_CS_AS = RTRIM(LTRIM(dt.Titulo)) COLLATE Latin1_General_CS_AS AND n.isDeleted = 0
                    INNER JOIN NivelControloAut nca ON nca.ID = n.ID AND nca.isDeleted = 0
                    INNER JOIN ControloAut ca ON ca.ID = nca.IDControloAut AND ca.IDTipoNoticiaAut = 4 AND ca.isDeleted = 0
                    INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = ca.ID AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0
                    INNER JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted = 0
                ORDER BY IDControloAut
                ";
            lstRAs = dalraes.Where(ra => ra.Value.TipoEntidade == 4).ToDictionary(p => p.Key, p => p.Value);

            CalculateCorrespRAs(cmd, lstRAs, result, conn);
            LoadData(currentDataSet, conn);

            // onomásticos
            cmd = @"
                INSERT INTO #ControloAutSugest
                SELECT dt._ID, dt.IDExterno, dt.Titulo, dt.IDSistema, dt.IDTipoEntidade, ca.ID IDControloAut, d.ID IDDicionario, d.Termo
                FROM @raes dt
                    INNER JOIN ControloAut ca ON ca.ChaveColectividade COLLATE Latin1_General_CS_AS = RTRIM(LTRIM(dt.IDExterno)) COLLATE Latin1_General_CS_AS AND ca.IDTipoNoticiaAut = 2 AND ca.isDeleted = 0
                    INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = ca.ID AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0
                    INNER JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted = 0
                
                INSERT INTO #ControloAutSugest
                SELECT dt._ID, dt.IDExterno, dt.Titulo, dt.IDSistema, dt.IDTipoEntidade, ca.ID IDControloAut, dAutorizado.ID IDDicionario, dAutorizado.Termo
                FROM @raes dt
                    INNER JOIN Dicionario d ON d.Termo COLLATE Latin1_General_CI_AI = RTRIM(LTRIM(dt.IDExterno)) COLLATE Latin1_General_CI_AI
                    INNER JOIN ControloAutDicionario cad ON cad.IDDicionario = d.ID AND cad.isDeleted = 0
                    INNER JOIN ControloAut ca ON ca.ID = cad.IDControloAut AND ca.IDTipoNoticiaAut = 2 AND ca.isDeleted = 0
                    INNER JOIN ControloAutDicionario cadAutorizado ON cadAutorizado.IDControloAut = ca.ID AND cadAutorizado.IDTipoControloAutForma = 1 AND cadAutorizado.isDeleted = 0
                    INNER JOIN Dicionario dAutorizado ON dAutorizado.ID = cadAutorizado.IDDicionario AND dAutorizado.isDeleted = 0
                    LEFT JOIN #ControloAutSugest caNIFs ON caNIFs.IDControloAut = ca.ID
                WHERE d.isDeleted = 0 AND caNIFs.IDControloAut IS NULL
                ORDER BY IDControloAut

                INSERT INTO #ControloAutSugest
                SELECT dt._ID, dt.IDExterno, dt.Titulo, dt.IDSistema, dt.IDTipoEntidade, ca.ID IDControloAut, dAutorizado.ID IDDicionario, dAutorizado.Termo
                FROM @raes dt
                    INNER JOIN Dicionario d ON d.Termo COLLATE Latin1_General_CI_AI = RTRIM(LTRIM(dt.Titulo)) COLLATE Latin1_General_CI_AI
                    INNER JOIN ControloAutDicionario cad ON cad.IDDicionario = d.ID AND cad.isDeleted = 0
                    INNER JOIN ControloAut ca ON ca.ID = cad.IDControloAut AND ca.IDTipoNoticiaAut = 2 AND ca.isDeleted = 0
                    INNER JOIN ControloAutDicionario cadAutorizado ON cadAutorizado.IDControloAut = ca.ID AND cadAutorizado.IDTipoControloAutForma = 1 AND cadAutorizado.isDeleted = 0
                    INNER JOIN Dicionario dAutorizado ON dAutorizado.ID = cadAutorizado.IDDicionario AND dAutorizado.isDeleted = 0
                    LEFT JOIN #ControloAutSugest caNIFs ON caNIFs.IDControloAut = ca.ID
                WHERE d.isDeleted = 0 AND caNIFs.IDControloAut IS NULL
                ORDER BY IDControloAut
                ";
            lstRAs = dalraes.Where(ra => ra.Value.TipoEntidade == 2).ToDictionary(p => p.Key, p => p.Value);

            CalculateCorrespRAs(cmd, lstRAs, result, conn);
            LoadData(currentDataSet, conn);

            return result;
        }

        private void CalculateCorrespRAs(string cmd, Dictionary<long, EntidadeExterna> lstRAs, Dictionary<long, long> result, IDbConnection conn)
        {
            DataTable t = new DataTable();
            DataColumn c1 = new DataColumn("_ID", typeof(string));
            DataColumn c2 = new DataColumn("IDExterno", typeof(string));
            DataColumn c3 = new DataColumn("Titulo", typeof(string));
            DataColumn c4 = new DataColumn("IDSistema", typeof(int));
            DataColumn c5 = new DataColumn("IDTipoEntidade", typeof(int));
            t.Columns.Add(c1);
            t.Columns.Add(c2);
            t.Columns.Add(c3);
            t.Columns.Add(c4);
            t.Columns.Add(c5);

            StringBuilder str = new StringBuilder(
                "CREATE TABLE #temp(_ID BIGINT, IDExterno NVARCHAR(200) COLLATE Latin1_General_CS_AS NOT NULL, Titulo NVARCHAR(200), IDSistema INT, IDTipoEntidade INT);" +
                "CREATE TABLE #ControloAutSugest(_ID BIGINT, IDExterno NVARCHAR(200) COLLATE Latin1_General_CS_AS NOT NULL, IDTitulo NVARCHAR(200) COLLATE Latin1_General_CS_AS, IDSistema INT, IDTipoEntidade INT, IDControloAut BIGINT, IDDicionarioAutorizado BIGINT, Termo NVARCHAR(768) COLLATE Latin1_General_CS_AS NOT NULL);");

            foreach (var ra in lstRAs)
            {
                DataRow dr = t.NewRow();
                dr[0] = ra.Key;
                dr[1] = ra.Value.IDExterno;
                dr[2] = ra.Value.Titulo;
                dr[3] = ra.Value.Sistema;
                dr[4] = ra.Value.TipoEntidade;
                t.Rows.Add(dr);

                //str.AppendLine(string.Format("INSERT INTO #temp VALUES ({0}, '{1}', '{2}', {3}, {4});", ra.Key, ra.Value.IDExterno, ra.Value.Titulo, ra.Value.Sistema, ra.Value.TipoEntidade));
            }

            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = "CREATE TABLE #ControloAutSugest(_ID BIGINT, IDExterno NVARCHAR(200) COLLATE Latin1_General_CS_AS NOT NULL, IDTitulo NVARCHAR(200) COLLATE Latin1_General_CS_AS, IDSistema INT, IDTipoEntidade INT, IDControloAut BIGINT, IDDicionarioAutorizado BIGINT, Termo NVARCHAR(768) COLLATE Latin1_General_CS_AS NOT NULL);";
            command.ExecuteNonQuery();

            command.CommandText = cmd;
            SqlParameter paramIds = command.Parameters.Add("@raes", SqlDbType.Structured);
            paramIds.TypeName = "Integ_RAExterno";
            paramIds.Value = t;
            command.ExecuteNonQuery();

            command = new SqlCommand("SELECT * FROM #ControloAutSugest", (SqlConnection)conn);
            SqlDataReader reader = command.ExecuteReader();
            var rasEncontrados = new Dictionary<long, string>();

            while (reader.Read())
            {
                var _ID = reader.GetInt64(0);
                var dalrae = lstRAs[_ID];
                var raTermoAutorizado = reader.GetString(7);
                var IDControloAut = reader.GetInt64(5);

                rasEncontrados[IDControloAut] = raTermoAutorizado;

                // prevenir a situação seguinte: 
                //  - para um Entidade Externa existir mais do que 2 match
                //    + a sugestão a apresentar deve ser o RA com o termo autorizado igual ao título da Entidade Externa
                //    + ou então, o primeiro RA encontrado que tenha o título da Entidade Externa como outra forma qualquer
                if (!result.ContainsKey(_ID))
                {
                    result.Add(_ID, IDControloAut);
                }
                else
                {
                    if (dalrae.Titulo.Equals(raTermoAutorizado) && !rasEncontrados[result[_ID]].Equals(raTermoAutorizado))
                        result[_ID] = IDControloAut;
                }
            }

            reader.Close();
        }

        private void LoadData(DataSet currentDataSet, IDbConnection conn)
        {
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #CaIds(IDControloAut BIGINT); INSERT INTO #CaIds SELECT DISTINCT IDControloAut FROM #ControloAutSugest;";
                command.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],
                        "INNER JOIN #CaIds ON #CaIds.IDControloAut = ControloAut.ID");
                    da.Fill(currentDataSet, "ControloAut");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"],
                        "INNER JOIN ControloAutDicionario ON ControloAutDicionario.IDDicionario = Dicionario.ID AND ControloAutDicionario.IDTipoControloAutForma = 1 " +
                        "INNER JOIN #CaIds ON #CaIds.IDControloAut = ControloAutDicionario.IDControloAut");
                    da.Fill(currentDataSet, "Dicionario");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"],
                        "INNER JOIN #CaIds ON #CaIds.IDControloAut = ControloAutDicionario.IDControloAut " +
                        "WHERE ControloAutDicionario.IDTipoControloAutForma = 1 ");
                    da.Fill(currentDataSet, "ControloAutDicionario");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN NivelControloAut ON NivelControloAut.ID = Nivel.ID " +
                        "INNER JOIN #CaIds ON #CaIds.IDControloAut = NivelControloAut.IDControloAut");
                    da.Fill(currentDataSet, "Nivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelControloAut"],
                        "INNER JOIN #CaIds ON #CaIds.IDControloAut = NivelControloAut.IDControloAut");
                    da.Fill(currentDataSet, "NivelControloAut");
                }

                command.CommandText = "DROP TABLE #ControloAutSugest; DROP TABLE #CaIds;";
                command.ExecuteNonQuery();
            }
        }

        public override List<DocInPortoRecord> FilterPreviousIncorporations(List<DocInPortoRecord> diprecords, IDbConnection conn)
        {
            DataTable t = new DataTable();
            t.Columns.Add(new DataColumn("IDSistema", typeof(int)));
            t.Columns.Add(new DataColumn("IDTipoEntidade", typeof(int)));
            t.Columns.Add(new DataColumn("IDExterno", typeof(string)));
            t.Columns.Add(new DataColumn("DataArquivo", typeof(DateTime)));

            foreach (var diprecord in diprecords)
            {
                DataRow dr = t.NewRow();
                dr[0] = diprecord.IDSistema;
                dr[1] = diprecord.IDTipoEntidade;
                dr[2] = diprecord.IDExterno;
                dr[3] = diprecord.DataArquivo;
                t.Rows.Add(dr);
            }

            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = @"
                CREATE TABLE #order (IDTipoEntidadeExterna INT, GUIOrder INT);
                INSERT INTO #order VALUES (7,1);
                INSERT INTO #order VALUES (6,2);
                INSERT INTO #order VALUES (8,3);";
            command.ExecuteNonQuery();

            command.CommandText = @"
                SELECT rec.IDExterno, MAX(rec.DataArquivo) dtArquivo, rec.IDTipoEntidade, rec.IDSistema
                FROM @records rec
	                LEFT JOIN (
		                SELECT r.IDExterno, r.DataArquivo, r.IDTipoEntidade, r.IDSistema
		                FROM Integ_EntidadeExterna
			                INNER JOIN Integ_RelacaoExternaNivel ON Integ_RelacaoExternaNivel.IDEntidadeExterna = Integ_EntidadeExterna.ID AND Integ_RelacaoExternaNivel.isDeleted = 0
			                INNER JOIN @records r ON r.IDExterno COLLATE Latin1_General_CS_AS = Integ_EntidadeExterna.IDExterno COLLATE Latin1_General_CS_AS
				                AND r.DataArquivo = Integ_RelacaoExternaNivel.Data
                                AND r.IDTipoEntidade = Integ_EntidadeExterna.IDTipoEntidade
                                AND r.IDSistema = Integ_EntidadeExterna.IDSistema
		                WHERE Integ_EntidadeExterna.IDTipoEntidade != 7 AND Integ_EntidadeExterna.isDeleted = 0
	                ) r ON r.IDExterno = rec.IDExterno
		                AND r.DataArquivo = rec.DataArquivo 
                        AND r.IDTipoEntidade = rec.IDTipoEntidade
                        AND r.IDSistema = rec.IDSistema
                    INNER JOIN #order ON #order.IDTipoEntidadeExterna = rec.IDTipoEntidade
                WHERE r.IDExterno IS NULL
                GROUP BY rec.IDExterno, rec.IDTipoEntidade, rec.IDSistema, #order.GUIOrder
                ORDER BY dtArquivo, #order.GUIOrder;
                DROP TABLE #order;"; // descartar documentos repetidos não integrados (mantém-se na lista o mais recente)
            SqlParameter paramIds = command.Parameters.Add("@records", SqlDbType.Structured);
            paramIds.TypeName = "DocInPortoRecord";
            paramIds.Value = t;
            SqlDataReader reader = command.ExecuteReader();

            var res = new List<DocInPortoRecord>();
            while (reader.Read())
            {
                var diprecord = new DocInPortoRecord();
                diprecord.IDExterno = reader.GetString(0);
                diprecord.DataArquivo = reader.GetDateTime(1);
                diprecord.IDTipoEntidade = reader.GetInt32(2);
                diprecord.IDSistema = reader.GetInt32(3);
                res.Add(diprecord);
            }
            reader.Close();

            return res;
        }

        public override void LoadInteg_Config(DataSet currentDataSet, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Integ_Config"]);
                da.Fill(currentDataSet, "Integ_Config");
            }
        }

        public override List<long> GetProdutoresRelDirect(long IDNivel, IDbConnection conn)
        {
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = string.Format(@"
                WITH temp (ID, IDUpper, IDTipoNivelRelacionado) AS (
	                SELECT ID, IDUpper, IDTipoNivelRelacionado FROM RelacaoHierarquica WHERE ID = {0} AND isDeleted = 0
                	
	                UNION ALL
                	
	                SELECT rh.ID, rh.IDUpper, rh.IDTipoNivelRelacionado FROM RelacaoHierarquica rh
	                INNER JOIN temp ON temp.IDUpper = rh.ID
                )
                SELECT nUpper.ID from temp
                INNER JOIN Nivel n ON n.ID = temp.ID AND n.IDTipoNivel = 3
                INNER JOIN Nivel nUpper ON nUpper.ID = temp.IDUpper AND nUpper.IDTipoNivel = 2", IDNivel);
            SqlDataReader reader = command.ExecuteReader();

            var result = new List<long>();
            while (reader.Read())
                result.Add(reader.GetInt64(0));
            reader.Close();

            return result;
        }

        public override void LoadDocumentDetails(DataSet currentDataSet, long IDNivelDoc, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@IDNivelDoc", IDNivelDoc);
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "INNER JOIN RelacaoHierarquica ON RelacaoHierarquica.IDUpper = Nivel.ID " +
                    "WHERE RelacaoHierarquica.ID = @IDNivelDoc");
                da.Fill(currentDataSet, "Nivel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                    "WHERE ID = @IDNivelDoc");
                da.Fill(currentDataSet, "RelacaoHierarquica");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                    "WHERE IDNivel = @IDNivelDoc");
                da.Fill(currentDataSet, "FRDBase");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDAgrupador"],
                    "INNER JOIN FRDBase ON FRDBase.ID = SFRDAgrupador.IDFRDBase " +
                    "WHERE IDNivel = @IDNivelDoc");
                da.Fill(currentDataSet, "SFRDAgrupador");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Codigo"],
                    "INNER JOIN FRDBase ON FRDBase.ID = Codigo.IDFRDBase " +
                    "WHERE IDNivel = @IDNivelDoc");
                da.Fill(currentDataSet, "Codigo");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDConteudoEEstrutura"],
                    "INNER JOIN FRDBase ON FRDBase.ID = SFRDConteudoEEstrutura.IDFRDBase " +
                    "WHERE IDNivel = @IDNivelDoc");
                da.Fill(currentDataSet, "SFRDConteudoEEstrutura");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDDatasProducao"],
                    "INNER JOIN FRDBase ON FRDBase.ID = SFRDDatasProducao.IDFRDBase " +
                    "WHERE IDNivel = @IDNivelDoc");
                da.Fill(currentDataSet, "SFRDDatasProducao");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDCondicaoDeAcesso"],
                    "INNER JOIN FRDBase ON FRDBase.ID = SFRDCondicaoDeAcesso.IDFRDBase " +
                    "WHERE IDNivel = @IDNivelDoc");
                da.Fill(currentDataSet, "SFRDCondicaoDeAcesso");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],
                    "INNER JOIN IndexFRDCA ON IndexFRDCA.IDControloAut = ControloAut.ID " +
                    "INNER JOIN FRDBase ON FRDBase.ID = IndexFRDCA.IDFRDBase " +
                    "WHERE FRDBase.IDNivel = @IDNivelDoc");
                da.Fill(currentDataSet, "ControloAut");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"],
                    "INNER JOIN ControloAutDicionario on ControloAutDicionario.IDDicionario = Dicionario.ID " + 
                    "INNER JOIN ControloAut ON ControloAut.ID = ControloAutDicionario.IDControloAut " +
                    "INNER JOIN IndexFRDCA ON IndexFRDCA.IDControloAut = ControloAut.ID " +
                    "INNER JOIN FRDBase ON FRDBase.ID = IndexFRDCA.IDFRDBase " +
                    "WHERE FRDBase.IDNivel = @IDNivelDoc");
                da.Fill(currentDataSet, "Dicionario");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"],
                    "INNER JOIN ControloAut ON ControloAut.ID = ControloAutDicionario.IDControloAut " +
                    "INNER JOIN IndexFRDCA ON IndexFRDCA.IDControloAut = ControloAut.ID " +
                    "INNER JOIN FRDBase ON FRDBase.ID = IndexFRDCA.IDFRDBase " +
                    "WHERE FRDBase.IDNivel = @IDNivelDoc");
                da.Fill(currentDataSet, "ControloAutDicionario");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["IndexFRDCA"],
                    "INNER JOIN FRDBase ON FRDBase.ID = IndexFRDCA.IDFRDBase " +
                    "WHERE FRDBase.IDNivel = @IDNivelDoc");
                da.Fill(currentDataSet, "IndexFRDCA");
            }
        }
    }
}