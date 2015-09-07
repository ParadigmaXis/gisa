using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
	public class SqlClientFRDRule: FRDRule
	{
		#region " PanelAmbitoConteudo "
		public override void LoadConteudoEEstrutura(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn)
		{
			using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CurrentFRDBaseID", CurrentFRDBaseID);
                command.Parameters.AddWithValue("@IDTipoNoticiaAut5", 5);
                command.Parameters.AddWithValue("@IDTipoNoticiaAut8", 8);

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],
                    "INNER JOIN IndexFRDCA ON IndexFRDCA.IDControloAut = ControloAut.ID " +
                    "WHERE ControloAut.IDTipoNoticiaAut BETWEEN @IDTipoNoticiaAut5 AND @IDTipoNoticiaAut8 AND IndexFRDCA.IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "ControloAut");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"],
                    "INNER JOIN ControloAutDicionario ON ControloAutDicionario.IDDicionario = Dicionario.ID " +
                    "INNER JOIN ControloAut ON ControloAut.ID = ControloAutDicionario.IDControloAut " +
                    "INNER JOIN IndexFRDCA ON IndexFRDCA.IDControloAut = ControloAut.ID " +
                    "WHERE ControloAut.IDTipoNoticiaAut BETWEEN @IDTipoNoticiaAut5 AND @IDTipoNoticiaAut8 AND IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "Dicionario");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"],
                    "INNER JOIN IndexFRDCA ON IndexFRDCA.IDControloAut = ControloAutDicionario.IDControloAut " +
                    "INNER JOIN ControloAut ON ControloAut.ID = ControloAutDicionario.IDControloAut " +
                    "WHERE ControloAut.IDTipoNoticiaAut BETWEEN @IDTipoNoticiaAut5 AND @IDTipoNoticiaAut8 AND IndexFRDCA.IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "ControloAutDicionario");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["IndexFRDCA"],
                    "INNER JOIN ControloAut ON ControloAut.ID = IndexFRDCA.IDControloAut " +
                    "WHERE ControloAut.IDTipoNoticiaAut BETWEEN @IDTipoNoticiaAut5 AND @IDTipoNoticiaAut8 AND IndexFRDCA.IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "IndexFRDCA");
			}
		}

        public override void LoadDadosLicencasDeObras(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn) {

            string QueryFilter = "WHERE IDFRDBase=@CurrentFRDBaseID";
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection) conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.CommandText = "CREATE TABLE #CAIDs (ID BIGINT);";
                command.ExecuteNonQuery();

                command.Parameters.AddWithValue("@isDeleted", 0);
                command.Parameters.AddWithValue("@CurrentFRDBaseID", CurrentFRDBaseID);

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDConteudoEEstrutura"], QueryFilter);
                da.Fill(currentDataSet, "SFRDConteudoEEstrutura");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["LicencaObra"], QueryFilter);
                da.Fill(currentDataSet, "LicencaObra");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["LicencaObraAtestadoHabitabilidade"], QueryFilter);
                da.Fill(currentDataSet, "LicencaObraAtestadoHabitabilidade");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["LicencaObraDataLicencaConstrucao"], QueryFilter);
                da.Fill(currentDataSet, "LicencaObraDataLicencaConstrucao");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["LicencaObraLocalizacaoObraActual"], QueryFilter);
                da.Fill(currentDataSet, "LicencaObraLocalizacaoObraActual");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["LicencaObraLocalizacaoObraAntiga"], QueryFilter);
                da.Fill(currentDataSet, "LicencaObraLocalizacaoObraAntiga");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["LicencaObraRequerentes"], QueryFilter);
                da.Fill(currentDataSet, "LicencaObraRequerentes");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["LicencaObraTecnicoObra"], QueryFilter);
                da.Fill(currentDataSet, "LicencaObraTecnicoObra");

                command.CommandText = "INSERT INTO #CAIDs SELECT IDControloAut FROM LicencaObraTecnicoObra " + QueryFilter +
                    " UNION  SELECT IDControloAut FROM LicencaObraLocalizacaoObraActual " + QueryFilter;
                command.ExecuteNonQuery();

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],
                    "INNER JOIN #CAIDs ON #CAIDs.ID = ControloAut.ID");
                da.Fill(currentDataSet, "ControloAut");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"],
                    "INNER JOIN #CAIDs ON #CAIDs.ID = ControloAutDicionario.IDControloAut");
                da.Fill(currentDataSet, "ControloAutDicionario");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"],
                    "INNER JOIN ControloAutDicionario ON ControloAutDicionario.IDDicionario = Dicionario.ID " +
                    "INNER JOIN #CAIDs ON #CAIDs.ID = ControloAutDicionario.IDControloAut");
                da.Fill(currentDataSet, "Dicionario");

                command.CommandText = "DROP TABLE #CAIDs";
                command.ExecuteNonQuery();
            }

        }


        public override bool possuiDadosLicencaDeObras(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn) {
            bool mostraCampoEstruturado = false;
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@CurrentFRDBaseID", CurrentFRDBaseID);
                command.Parameters.AddWithValue("@IDTipoNoticiaAut", 5);
                command.Parameters.AddWithValue("@TipoTipologia", "PROCESSO_DE_OBRAS");
                command.Parameters.AddWithValue("@idxSelector", -1);
             
                command.CommandText = @"
SELECT COUNT(*) FROM IndexFRDCA idx 
INNER JOIN ControloAut ca ON ca.ID = idx.IDControloAut AND ca.IDTipoNoticiaAut = @IDTipoNoticiaAut AND ca.isDeleted = @isDeleted 
INNER JOIN TipoTipologias tt ON tt.ID = ca.IDTipoTipologia AND tt.BuiltInName = @TipoTipologia AND tt.isDeleted = @isDeleted 
WHERE idx.isDeleted = @isDeleted  AND idx.Selector = @idxSelector AND idx.IDFRDBase = @CurrentFRDBaseID";

                mostraCampoEstruturado = System.Convert.ToInt32(command.ExecuteScalar()) > 0;

                // carregar a tipologia associada
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],
                    "INNER JOIN IndexFRDCA ON IndexFRDCA.IDControloAut = ControloAut.ID " +
                    "WHERE ControloAut.IDTipoNoticiaAut = @IDTipoNoticiaAut AND IndexFRDCA.IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "ControloAut");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"],
                    "INNER JOIN ControloAutDicionario ON ControloAutDicionario.IDDicionario = Dicionario.ID " +
                    "INNER JOIN ControloAut ON ControloAut.ID = ControloAutDicionario.IDControloAut " +
                    "INNER JOIN IndexFRDCA ON IndexFRDCA.IDControloAut = ControloAut.ID " +
                    "WHERE ControloAut.IDTipoNoticiaAut = @IDTipoNoticiaAut AND IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "Dicionario");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"],
                    "INNER JOIN IndexFRDCA ON IndexFRDCA.IDControloAut = ControloAutDicionario.IDControloAut " +
                    "INNER JOIN ControloAut ON ControloAut.ID = ControloAutDicionario.IDControloAut " +
                    "WHERE ControloAut.IDTipoNoticiaAut = @IDTipoNoticiaAut AND IndexFRDCA.IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "ControloAutDicionario");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["IndexFRDCA"],
                    "INNER JOIN ControloAut ON ControloAut.ID = IndexFRDCA.IDControloAut " +
                    "WHERE ControloAut.IDTipoNoticiaAut = @IDTipoNoticiaAut AND IndexFRDCA.IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "IndexFRDCA");
            }

            return mostraCampoEstruturado;
        }

        public override bool isDocumentoProcessoObra(long CurrentFRDBaseID, IDbConnection conn)
        {
            using(var command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            {
                command.Parameters.AddWithValue("@idxSelector", -1);
                command.Parameters.AddWithValue("@IDTipoNivelRelacionado10", 10);
                command.Parameters.AddWithValue("@IDTipoNoticiaAut5", 5);
                command.Parameters.AddWithValue("@TipoTipologias", "PROCESSO_DE_OBRAS");
                command.Parameters.AddWithValue("@CurrentFRDBaseID", CurrentFRDBaseID);
                command.CommandText = @"
SELECT COUNT(frd.ID)
FROM FRDBase frdSDoc
	INNER JOIN Nivel nSDoc ON nSDoc.ID = frdSDoc.IDNivel AND nSDoc.isDeleted = @isDeleted
	INNER JOIN RelacaoHierarquica rh ON rh.ID = nSDoc.ID AND rh.IDTipoNivelRelacionado = @IDTipoNivelRelacionado10 AND rh.isDeleted = @isDeleted
	INNER JOIN Nivel n ON n.ID = rh.IDUpper AND n.isDeleted = @isDeleted
	INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = @isDeleted
	INNER JOIN IndexFRDCA idx ON idx.IDFRDBase = frd.ID AND idx.Selector = @idxSelector AND idx.isDeleted = @isDeleted
	INNER JOIN ControloAut ca ON ca.ID = idx.IDControloAut AND ca.IDTipoNoticiaAut = @IDTipoNoticiaAut5 AND ca.isDeleted = @isDeleted
	INNER JOIN TipoTipologias tt ON tt.ID = ca.IDTipoTipologia AND tt.BuiltInName = @TipoTipologias AND tt.isDeleted = @isDeleted 
WHERE frdSDoc.ID = @CurrentFRDBaseID AND frdSDoc.isDeleted = @isDeleted";
                return System.Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

		#endregion

		#region " PanelCondicoesAcesso "
		public override void LoadCondicoesAcessoData(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn)
		{
            string query = "WHERE IDFRDBase=@CurrentFRDBaseID";
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CurrentFRDBaseID", CurrentFRDBaseID);

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDCondicaoDeAcesso"], query);
				da.Fill(currentDataSet, "SFRDCondicaoDeAcesso");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDTecnicasDeRegisto"], query);
				da.Fill(currentDataSet, "SFRDTecnicasDeRegisto");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDFormaSuporteAcond"], query);
				da.Fill(currentDataSet, "SFRDFormaSuporteAcond");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDEstadoDeConservacao"], query);
				da.Fill(currentDataSet, "SFRDEstadoDeConservacao");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDMaterialDeSuporte"], query);
				da.Fill(currentDataSet, "SFRDMaterialDeSuporte");
			}
		}
		#endregion

		#region " PanelContexto "
		public override void LoadContextoData(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn)
		{
            string query = "WHERE IDFRDBase=@CurrentFRDBaseID";
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CurrentFRDBaseID", CurrentFRDBaseID);

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDContexto"], query);
				da.Fill(currentDataSet, "SFRDContexto");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],
                    "INNER JOIN SFRDAutor ON SFRDAutor.IDControloAut = ControloAut.ID " +
                    "WHERE SFRDAutor.IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "ControloAut");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDAutor"],
                    "WHERE SFRDAutor.IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "SFRDAutor");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"],
                    "INNER JOIN ControloAutDicionario ON ControloAutDicionario.IDDicionario = Dicionario.ID " +
                    "INNER JOIN ControloAut ON ControloAut.ID = ControloAutDicionario.IDControloAut " +
                    "INNER JOIN SFRDAutor ON SFRDAutor.IDControloAut = ControloAut.ID " +
                    "WHERE SFRDAutor.IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "Dicionario");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"],
                    "INNER JOIN ControloAut ON ControloAut.ID = ControloAutDicionario.IDControloAut " +
                    "INNER JOIN SFRDAutor ON SFRDAutor.IDControloAut = ControloAut.ID " +
                    "WHERE SFRDAutor.IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "ControloAutDicionario");
			}
		}
		
		public override ArrayList LoadProdutores(System.Data.DataSet currentDataSet, long CurrentFRDBaseID, long NivelRowID, ArrayList caList, System.Data.IDbConnection conn)
		{
			// verificar existencia de Entidades Produtoras. Não existindo entidades 
			// produtoras é necessário obter as entidades produtoras herdadas do 
			// ascendente mais próximo
			string NiveisFilter = null;
			string CAsFilter = null;
			string NVLCAsFilter = null;
			ArrayList NiveisList = new ArrayList();
			caList.Clear();
            using (SqlCommand command = new SqlCommand("sp_getEntidadesProdutorasHerdadas", (SqlConnection)conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@NivelID", SqlDbType.BigInt);
                command.Parameters[0].Value = NivelRowID;
                SqlDataReader dataReader;
                try
                {
                    Trace.WriteLine("<getEntidadesProdutorasHerdadas>");
                    dataReader = command.ExecuteReader();
                    Trace.WriteLine("</getEntidadesProdutorasHerdadas>");
                    while (dataReader.Read())
                    {
                        string IDNivel = null;
                        string IDControloAut = null;
                        if (!(dataReader.IsDBNull(0)) && !(dataReader.IsDBNull(1)))
                        {
                            IDNivel = dataReader.GetValue(0).ToString();
                            IDControloAut = dataReader.GetValue(1).ToString();

                            if (NiveisFilter == null)
                                NiveisFilter = "" + IDNivel;
                            else
                                NiveisFilter += ", " + IDNivel;

                            NiveisList.Add(IDNivel);

                            if (CAsFilter == null)
                                CAsFilter = "" + IDControloAut;
                            else
                                CAsFilter += ", " + IDControloAut;

                            caList.Add(IDControloAut);

                            if (NVLCAsFilter == null)
                                NVLCAsFilter = string.Format("(ID={0} AND IDControloAut={1})", IDNivel, IDControloAut);
                            else
                                NVLCAsFilter += string.Format("OR (ID={0} AND IDControloAut={1})", IDNivel, IDControloAut);
                        }
                    }
                    dataReader.Close();

                    if (caList.Count > 0)
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(command))
                        {
                            command.Parameters.Clear();
                            command.CommandType = CommandType.Text;
                            command.Parameters.AddWithValue("@isDeleted", 0);
                            command.Parameters.AddWithValue("@NivelRowID", NivelRowID);
                            command.Parameters.AddWithValue("@CurrentFRDBaseID", CurrentFRDBaseID);

                            da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                                "WHERE ID IN (" + NiveisFilter + ")");
                            da.Fill(currentDataSet, "Nivel");
                            da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                                string.Format("WHERE ID=@NivelRowID AND IDUpper IN ({0})", NiveisFilter));
                            da.Fill(currentDataSet, "RelacaoHierarquica");
                            da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],
                                "WHERE ID IN (" + CAsFilter + ")");
                            da.Fill(currentDataSet, "ControloAut");
                            da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDatasExistencia"],
                                "WHERE IDControloAut IN (" + CAsFilter + ")");
                            da.Fill(currentDataSet, "ControloAutDatasExistencia");
                            da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelControloAut"],
                                "WHERE " + NVLCAsFilter + "");
                            da.Fill(currentDataSet, "NivelControloAut");
                            da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"],
                                "WHERE ID IN (SELECT IDDicionario FROM ControloAutDicionario WHERE IDControloAut IN (" + CAsFilter + "))");
                            da.Fill(currentDataSet, "Dicionario");
                            da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"],
                                "WHERE IDControloAut IN (" + CAsFilter + ")");
                            da.Fill(currentDataSet, "ControloAutDicionario");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                    throw ex;
                }
            }
			return caList;
		}
		#endregion

		#region " PanelDocumentacaoAssociada "
		public override void LoadDocumentacaoAssociadaData(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CurrentFRDBaseID", CurrentFRDBaseID);

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDDocumentacaoAssociada"], 
                    "WHERE IDFRDBase=@CurrentFRDBaseID");
				da.Fill(currentDataSet, "SFRDDocumentacaoAssociada");
			}
		}
		#endregion

		#region " PanelIdentificacao "
		public override void LoadIdentificacaoData(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn)
		{
			string WhereQueryFilter = "WHERE IDFRDBase=@CurrentFRDBaseID";

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CurrentFRDBaseID", CurrentFRDBaseID);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDDatasProducao"], 
                    WhereQueryFilter);
				da.Fill(currentDataSet, "SFRDDatasProducao");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Codigo"],
                    WhereQueryFilter);
                da.Fill(currentDataSet, "Codigo");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDAgrupador"],
                    WhereQueryFilter);
                da.Fill(currentDataSet, "SFRDAgrupador");
			}
		}

        public override string UFsWithSameCota(long CurrentFRDBaseID, string cota, IDbConnection conn)
        {
            StringBuilder result = new StringBuilder();
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.CommandText = string.Format(@"
                SELECT n.Codigo, nd.Designacao 
                FROM SFRDUFCota c
	                INNER JOIN FRDBase frd ON frd.ID = c.IDFRDBase AND frd.isDeleted = 0
	                INNER JOIN Nivel n ON n.ID = frd.IDNivel AND n.isDeleted = 0
	                INNER JOIN NivelDesignado nd ON nd.ID = n.ID AND nd.isDeleted = 0
                WHERE c.IDFRDBase <> {0} AND UPPER(c.Cota) = UPPER('{1}') AND c.isDeleted = 0", CurrentFRDBaseID, cota);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    result.AppendFormat(System.Environment.NewLine + "{0} – {1}", reader.GetString(0), reader.GetString(1));

                reader.Close();
            }
            return result.ToString();
        }
		#endregion

		#region " PanelIncorporacoes "
		public override void LoadIncorporacoesData(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CurrentFRDBaseID", CurrentFRDBaseID);

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDConteudoEEstrutura"], 
                    "WHERE IDFRDBase=@CurrentFRDBaseID");
				da.Fill(currentDataSet, "SFRDConteudoEEstrutura");
			}
		}
		#endregion

		#region " PanelIndexacao "
		public override void LoadIndexacaoData(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn)
		{
			using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CurrentFRDBaseID", CurrentFRDBaseID);

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],
                    "INNER JOIN IndexFRDCA ON IndexFRDCA.IDControloAut = ControloAut.ID " +
                    "WHERE ControloAut.IDTipoNoticiaAut BETWEEN 1 AND 3 AND IndexFRDCA.IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "ControloAut");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["IndexFRDCA"],
                    "INNER JOIN ControloAut ON ControloAut.ID = IndexFRDCA.IDControloAut " +
                    "WHERE ControloAut.IDTipoNoticiaAut BETWEEN 1 AND 3 AND IndexFRDCA.IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "IndexFRDCA");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"],
                    "INNER JOIN ControloAutDicionario cad ON cad.IDDicionario = Dicionario.ID " +
                    "INNER JOIN ControloAut ca ON ca.ID = cad.IDControloAut " +
                    "INNER JOIN IndexFRDCA ON IndexFRDCA.IDControloAut = ca.ID " +
                    "WHERE ca.IDTipoNoticiaAut BETWEEN 1 AND 3 AND IndexFRDCA.IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "Dicionario");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"],
                    "INNER JOIN ControloAut ca ON ca.ID = ControloAutDicionario.IDControloAut " +
                    "INNER JOIN IndexFRDCA ON IndexFRDCA.IDControloAut = ca.ID " +
                    "WHERE ca.IDTipoNoticiaAut BETWEEN 1 AND 3 AND IndexFRDCA.IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "ControloAutDicionario");
			}
		}
		#endregion

		#region " PanelIndiceDocumento "
        public override void LoadIndiceDocumentoData(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@CurrentFRDBaseID", CurrentFRDBaseID);

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDImagemVolume"],
                    "INNER JOIN (SELECT DISTINCT IDSFDImagemVolume FROM SFRDImagem WHERE SFRDImagem.IDFRDBase = @CurrentFRDBaseID) SFRDImagem ON SFRDImagem.IDSFDImagemVolume = SFRDImagemVolume.ID ");
                da.Fill(currentDataSet, "SFRDImagemVolume");
                
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDImagem"],
                    "WHERE IDFRDBase = @CurrentFRDBaseID");
                da.Fill(currentDataSet, "SFRDImagem");
            }
        }

        public override List<string> FedoraIDs(IDbConnection conn)
        {
            var res = new List<string>();
            using (var command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.Parameters.AddWithValue("@Publicar", 1);
                command.Parameters.AddWithValue("@imgTipo", "Fedora");
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.CommandText = @"
                    SELECT img.Identificador
                    FROM FRDBase frd
                       INNER JOIN SFRDAvaliacao a ON a.IDFRDBase=frd.ID
                       INNER JOIN SFRDImagem img ON img.IDFRDBase = frd.ID
                    WHERE
                       Publicar=@Publicar AND
                       img.Tipo = @imgTipo AND
                       frd.isDeleted=@isDeleted AND a.isDeleted=@isDeleted AND img.isDeleted=@isDeleted";
                var reader = command.ExecuteReader();

                while (reader.Read()) { res.Add(reader.GetString(0)); }

                reader.Close();
            }

            return res;
        }
		#endregion

		#region " PanelNotas "
		public override void LoadNotasData(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CurrentFRDBaseID", CurrentFRDBaseID);

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDNotaGeral"], 
                    "WHERE IDFRDBase=@CurrentFRDBaseID");
				da.Fill(currentDataSet, "SFRDNotaGeral");
			}
		}
		#endregion

		#region " PanelOIControloDescricao "
		public override void LoadOIControloDescricaoData(DataSet currentDataSet, DataSet newDataSet, long CurrentFRDBaseID, IDbConnection conn)
		{
			string WhereQueryFilter = "WHERE IDFRDBase=@CurrentFRDBaseID";
			string WhereUserQueryFilter = "WHERE ID IN " + 
				"(SELECT IDTrusteeOperator FROM FRDBaseDataDeDescricao " + 
				WhereQueryFilter + " UNION " + 
				"SELECT IDTrusteeAuthority FROM FRDBaseDataDeDescricao " + 
				WhereQueryFilter + ")";

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CurrentFRDBaseID", CurrentFRDBaseID);
                command.Parameters.AddWithValue("@IsAuthority", 1);

				// Load Trustee.IsAuthority=1
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Trustee"],
                    "WHERE ID IN (SELECT ID FROM TrusteeUser WHERE IsAuthority=@IsAuthority)");
				da.Fill(currentDataSet, "Trustee");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeUser"],
                    "WHERE IsAuthority=@IsAuthority");
				da.Fill(currentDataSet, "TrusteeUser");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Trustee"], 
                    WhereUserQueryFilter);
				da.Fill(currentDataSet, "Trustee");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeUser"], 
                    WhereUserQueryFilter);
				da.Fill(currentDataSet, "TrusteeUser");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "INNER JOIN FRDBase frd ON frd.IDNivel = Nivel.ID " +
                    "WHERE frd.ID=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "Nivel");
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                    "WHERE ID=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "FRDBase");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBaseDataDeDescricao"],
                    WhereQueryFilter);
                da.Fill(currentDataSet, "FRDBaseDataDeDescricao");
			}
		}
		#endregion

		#region " PanelUFUnidadesDescricao "
		public override ArrayList LoadUFUnidadesDescricaoData(DataSet currentDataSet, long CurrentNivelID, IDbConnection conn)
		{
			ArrayList ordem = new ArrayList();			
			string WhereQueryFilter = " WHERE SFRDUnidadeFisica.IDNivel=@CurrentNivelID";

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CurrentNivelID", CurrentNivelID);
				// Niveis das FRD
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"], 
			        " INNER Join FRDBase frd on frd.IDNivel = Nivel.ID" +
					" INNER JOIN SFRDUnidadeFisica on SFRDUnidadeFisica.IDFRDBase = frd.ID " +
					WhereQueryFilter);
				da.Fill(currentDataSet, "Nivel");

				//FRDs
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"], 					
					" INNER JOIN SFRDUnidadeFisica on SFRDUnidadeFisica.IDFRDBase = FRDBase.ID " + WhereQueryFilter);
				da.Fill(currentDataSet, "FRDBase");				

				//SFRDUnidadeFisica
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDUnidadeFisica"], 					
					" INNER Join FRDBase on FRDBase.ID = SFRDUnidadeFisica.IDFRDBase " + WhereQueryFilter);
				da.Fill(currentDataSet, "SFRDUnidadeFisica");

                command.CommandText =
                "SELECT ud.IDFRDBase, ud.IDTipoNivelRelacionado " +
                "FROM ( " +
                    "SELECT UnidadesDescricao.IDFRDBase, min(rh.IDTipoNivelRelacionado) IDTipoNivelRelacionado, UnidadesDescricao.Designacao " +
                    "FROM ( " +
                        "SELECT SFRDUnidadeFisica.IDFRDBase, frd.IDNivel, d.Termo Designacao " +
                        "FROM SFRDUnidadeFisica " +
                            "INNER Join FRDBase frd ON frd.ID = SFRDUnidadeFisica.IDFRDBase AND frd.isDeleted=@isDeleted " +
                            "INNER JOIN NivelControloAut nca ON nca.ID = frd.IDNivel AND nca.isDeleted=@isDeleted " +
                            "INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = nca.IDControloAut AND cad.IDTipoControloAutForma=@IDTipoControloAutForma AND cad.isDeleted=@isDeleted " +
                            "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted=@isDeleted " +
                        "WHERE SFRDUnidadeFisica.isDeleted=@isDeleted AND SFRDUnidadeFisica.IDNivel = @CurrentNivelID " +
                        "UNION " +
                        "SELECT SFRDUnidadeFisica.IDFRDBase, frd.IDNivel, nd.Designacao " +
                        "FROM SFRDUnidadeFisica " +
                            "INNER Join FRDBase frd ON frd.ID = SFRDUnidadeFisica.IDFRDBase AND frd.isDeleted=@isDeleted " +
                            "INNER Join NivelDesignado nd ON nd.ID = frd.IDNivel AND nd.isDeleted=@isDeleted " +
                        "WHERE SFRDUnidadeFisica.isDeleted=@isDeleted AND SFRDUnidadeFisica.IDNivel = @CurrentNivelID " +
                    ") UnidadesDescricao " +
                    "INNER JOIN RelacaoHierarquica rh ON rh.ID = UnidadesDescricao.IDNivel AND rh.isDeleted=@isDeleted " +
                    "GROUP BY UnidadesDescricao.IDFRDBase, UnidadesDescricao.Designacao " +
                ") ud " +
                "ORDER BY ud.IDTipoNivelRelacionado ASC, ud.Designacao ASC";
                command.Parameters.AddWithValue("@IDTipoControloAutForma", 1);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    ordem.Add(reader.GetInt64(0));
                reader.Close();
			}
			
            return ordem;			
			
		}

		public override Hashtable LoadUFUnidadesDescricaoDetalhe(DataSet currentDataSet, long CurrentNivelID, long userID, IDbConnection conn)  
		{		
			Hashtable rez = new Hashtable();			
			long itemID;
			ArrayList tup;
			
			SqlCommand command = new SqlCommand(string.Empty, (SqlConnection) conn);
			command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@IDTrustee", SqlDbType.BigInt);
			command.Parameters[0].Value = userID;
            command.Parameters.Add("@IDNivel", SqlDbType.BigInt);
            command.Parameters[1].Value = CurrentNivelID;
			command.CommandText = "sp_loadUFUnidadesDescricao";
			SqlDataReader reader  = command.ExecuteReader();
					
			while (reader.Read())
			{
				itemID = System.Convert.ToInt64(reader.GetValue(0)); //IDNivel
					
				if (rez[itemID] == null) 
				{
					tup = new ArrayList();
					tup.Add(System.Convert.ToString(reader.GetValue(1))); //Designacao
					tup.Add(System.Convert.ToString(reader.GetValue(2))); //NivelDescricao
					tup.Add(System.Convert.ToString(reader.GetValue(3))); //InicioAno
					tup.Add(System.Convert.ToString(reader.GetValue(4))); //InicioMes
					tup.Add(System.Convert.ToString(reader.GetValue(5))); //InicioDia
					tup.Add(System.Convert.ToString(reader.GetValue(6))); //FimAno
					tup.Add(System.Convert.ToString(reader.GetValue(7))); //FimMes
					tup.Add(System.Convert.ToString(reader.GetValue(8))); //FimDia
					tup.Add(System.Convert.ToString(reader.GetValue(9))); //CodigoCompleto
                    if (reader.GetValue(10) == DBNull.Value)
                        tup.Add(0);
                    else
                        tup.Add(System.Convert.ToByte(reader.GetValue(10))); //Permissão de Leitura
                    tup.Add(System.Convert.ToBoolean(reader.GetValue(11))); //Requisitado
					rez.Add(itemID, tup);
				}
			}
			reader.Close();

			return rez;
		}

		public override ArrayList FilterUFUnidadesDescricao(string Des, long TNRid, long CurrentNivelID, IDbConnection conn) 
		{
			ArrayList rez = new ArrayList();
			SqlDataReader reader;
            string queryBase = " SELECT DISTINCT FRDBase.ID " +
								" FROM SFRDUnidadeFisica " + 
								" INNER Join FRDBase on FRDBase.ID = SFRDUnidadeFisica.IDFRDBase  ";			
			string docClause = " INNER JOIN NivelDesignado ON FRDBase.IDNivel = NivelDesignado.ID AND " + DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.buildLikeStatement("NivelDesignado.Designacao", String.Format("'{0}'", Des));
			string structClause = " INNER JOIN NivelControloAut ON NivelControloAut.ID = FRDBase.IDNivel " +
									" INNER JOIN ControloAutDicionario ON ControloAutDicionario.IDControloAut = NivelControloAut.IDControloAut " +
									" INNER JOIN Dicionario ON Dicionario.ID = ControloAutDicionario.IDDicionario AND " + DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.buildLikeStatement("Dicionario.Termo", String.Format("'{0}'", Des));
			string idtnrClause = " INNER JOIN RelacaoHierarquica ON RelacaoHierarquica.ID = FRDBase.IDNivel  AND RelacaoHierarquica.IDTipoNivelRelacionado = " + TNRid.ToString(); 
			string whereClause = " WHERE SFRDUnidadeFisica.IDNivel=" + CurrentNivelID.ToString() + " AND SFRDUnidadeFisica.isDeleted=0";;
			string cmd = queryBase;

			SqlCommand command = new SqlCommand(string.Empty, (SqlConnection) conn);
			if ( TNRid != -1 )
			{
                cmd += idtnrClause;
			}

			if ((Des.CompareTo(null) != 0) && (Des != String.Empty))
			{	
				command.CommandText = cmd + structClause + whereClause;
				reader = command.ExecuteReader();
				AddUFUDRez(reader, rez);
				reader.Close();
				
				command.CommandText = cmd + docClause + whereClause;
				reader = command.ExecuteReader();
				AddUFUDRez(reader, rez);
				reader.Close();

			}
			else
			{
                command.CommandText = cmd + whereClause;
				reader = command.ExecuteReader();
				AddUFUDRez(reader, rez);
				reader.Close();				
			}
			return rez;
		}

		private ArrayList AddUFUDRez(SqlDataReader reader, ArrayList rez) 
		{
			try 
			{
				while (reader.Read())
				{
					rez.Add(Convert.ToInt64(reader.GetValue(0)));
				}
			} 
			catch (Exception e)
			{
				Trace.WriteLine(e.ToString());
				throw e;
			}
			return rez;
		}


		public override void LoadFRD(DataSet currentDataSet, long CurrentNivelID, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CurrentNivelID", CurrentNivelID);
				da.SelectCommand.CommandText = 
                    "SELECT ID, IDNivel, IDTipoFRDBase, Versao, isDeleted " +
					"FROM FRDBase " +
                    "WHERE IDNivel = @CurrentNivelID AND isDeleted=@isDeleted";
				da.Fill(currentDataSet, "FRDBase");
			}
		}
		#endregion

		#region " PanelOIDimensoesSuporte "
        public override List<UFRule.UFsAssociadas> LoadOIDimensoesSuporteData(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn)
		{
            var result = new List<UFRule.UFsAssociadas>();
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@IDFRDBase", CurrentFRDBaseID);
                command.Parameters.AddWithValue("@NufEliminado", 1);
                command.Parameters.AddWithValue("@IDTipoNivel", 1);

                // carregamento das notas das associações
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDDimensaoSuporte"],
                    "WHERE SFRDDimensaoSuporte.IDFRDBase=@IDFRDBase");
                da.Fill(currentDataSet, "SFRDDimensaoSuporte");

                // carregar entidades detentoras
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "WHERE IDTipoNivel = @IDTipoNivel");
                da.Fill(currentDataSet, "Nivel");

                command.CommandText = @"
with TMP (ID, IDUpper) as (
select ID, IDUpper FROM RelacaoHierarquica where ID = (select IDNivel from FRDBase where ID = @IDFRDBase) and isDeleted=@isDeleted
union all
select rh.ID, rh.IDUpper from RelacaoHierarquica rh
inner join TMP on TMP.IDUpper = rh.ID
where rh.isDeleted = @isDeleted
)
select Codigo from Nivel where ID in (
select distinct TMP.IDUpper from TMP
left join RelacaoHierarquica rh on rh.ID = TMP.IDUpper and rh.isDeleted=0
where rh.ID is null) and IDTipoNivel = 1";
                var edCod = command.ExecuteScalar().ToString();
                command.Parameters.AddWithValue("@EdCod", edCod);

                command.CommandText = @"
SELECT n.ID IDNivel, @EdCod + '/' + n.Codigo Codigo, nd.Designacao Designacao, COALESCE(ta.Designacao, '') TipoAcondicionamento, COALESCE(df.MedidaLargura, 0,000) Largura, COALESCE(df.MedidaAltura, 0,000) Altura, 
    COALESCE(df.MedidaProfundidade, 0,000) Profundidade, COALESCE(ct.Cota, '') Cota, COALESCE(dp.InicioAno, '') InicioAno, COALESCE(dp.InicioMes, '') InicioMes, COALESCE(dp.InicioDia, '') InicioDia, 
    COALESCE(dp.InicioAtribuida, 0) InicioAtribuida, COALESCE(dp.FimAno, '') FimAno, COALESCE(dp.FimMes, '') FimMes, COALESCE(dp.FimDia, '') FimDia, COALESCE(dp.FimAtribuida, 0) FimAtribuida, 
    COALESCE(nuf.Eliminado, 0) Eliminado, COALESCE(tm.Designacao, '') Medida, '' Autos, frduf.ID IDFRDBase
into #ufs
FROM FRDBase frd
    INNER JOIN SFRDUnidadeFisica sfrduf ON sfrduf.IDFRDBase = frd.ID AND sfrduf.isDeleted=@isDeleted
    INNER JOIN Nivel n ON n.ID = sfrduf.IDNivel AND n.isDeleted=@isDeleted
    INNER JOIN NivelDesignado nd ON nd.ID = n.ID AND nd.isDeleted=@isDeleted
    INNER JOIN NivelUnidadeFisica nuf ON nuf.ID = n.ID AND nuf.isDeleted=@isDeleted
    INNER JOIN FRDBase frduf ON frduf.IDNivel = n.ID AND frduf.isDeleted=@isDeleted
    LEFT JOIN SFRDUFDescricaoFisica df ON df.IDFRDBase = frduf.ID AND df.isDeleted=@isDeleted
    LEFT JOIN SFRDUFCota ct ON ct.IDFRDBase = frduf.ID AND ct.isDeleted=@isDeleted
    LEFT JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frduf.ID AND dp.isDeleted=@isDeleted
    LEFT JOIN TipoAcondicionamento ta ON ta.ID = df.IDTipoAcondicionamento
    LEFT JOIN TipoMedida tm ON tm.ID = df.IDTipoMedida
WHERE frd.isDeleted=@isDeleted AND frd.ID=@IDFRDBase

update #ufs 
set Autos = LEFT(o1.list, LEN(o1.list)-1)
from #ufs U
	CROSS APPLY ( 
        SELECT CONVERT(VARCHAR(200), autos.Designacao) + '; ' AS [text()]
        FROM (
            SELECT ae.Designacao
            FROM SFRDUFAutoEliminacao ufae
                INNER JOIN AutoEliminacao ae ON ae.ID = ufae.IDAutoEliminacao AND ae.isDeleted=@isDeleted
            WHERE ufae.isDeleted=@isDeleted AND ufae.IDFRDBase = U.IDFRDBase
            UNION
            SELECT ae.Designacao
            FROM SFRDUnidadeFisica sfrduf
                INNER JOIN FRDBase frdNvlDoc ON frdNvlDoc.ID = sfrduf.IDFRDBase AND frdNvlDoc.isDeleted=@isDeleted
                INNER JOIN SFRDAvaliacao av ON av.IDFRDBase = frdNvlDoc.ID AND av.isDeleted=@isDeleted
                INNER JOIN AutoEliminacao ae ON ae.ID = av.IDAutoEliminacao AND ae.isDeleted=@isDeleted
            WHERE sfrduf.isDeleted=@isDeleted AND sfrduf.IDNivel = U.IDNivel
        ) autos
        GROUP BY autos.Designacao
        FOR XML PATH('') 
    ) o1 (list)
where U.Eliminado=@NufEliminado

select * from #ufs order by IDNivel desc

drop table #ufs";
                SqlDataReader reader = command.ExecuteReader();
                
                var ufAssociada = default(UFRule.UFsAssociadas);

                while (reader.Read())
                {
                    ufAssociada = new UFRule.UFsAssociadas();
                    ufAssociada.ID = reader.GetInt64(0);
                    ufAssociada.Codigo = reader.GetString(1);
                    ufAssociada.Designacao = reader.GetString(2);
                    ufAssociada.TipoAcondicionamento = reader.GetString(3);
                    ufAssociada.Largura = reader.GetDecimal(4);
                    ufAssociada.Altura = reader.GetDecimal(5);
                    ufAssociada.Profundidade = reader.GetDecimal(6);
                    ufAssociada.Cota = reader.GetString(7);
                    ufAssociada.DPInicioAno = reader.GetString(8);
                    ufAssociada.DPInicioMes = reader.GetString(9);
                    ufAssociada.DPInicioDia = reader.GetString(10);
                    ufAssociada.DPInicioAtribuida = System.Convert.ToBoolean(reader.GetValue(11));
                    ufAssociada.DPFimAno = reader.GetString(12);
                    ufAssociada.DPFimMes = reader.GetString(13);
                    ufAssociada.DPFimDia = reader.GetString(14);
                    ufAssociada.DPFimAtribuida = System.Convert.ToBoolean(reader.GetValue(15));
                    ufAssociada.Eliminado = System.Convert.ToBoolean(reader.GetValue(16));
                    ufAssociada.TipoMedida = reader.GetString(17);
                    ufAssociada.AutosAssociados = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                    result.Add(ufAssociada);
                }
                reader.Close();
			}

            return result;
		}

        public override void LoadUFRelacionada(DataSet currentDataSet, long CurrentFRDBaseID, long IDNivelUF, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@IDFRDBase", CurrentFRDBaseID);
                command.Parameters.AddWithValue("@IDNivel", IDNivelUF);

                // carregamento das unidades físicas associadas
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"], 
                    "WHERE ID=@IDNivel");
                da.Fill(currentDataSet, "Nivel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDUnidadeFisica"],
                    "WHERE IDFRDBase=@IDFRDBase AND IDNivel=@IDNivel");
				da.Fill(currentDataSet, "SFRDUnidadeFisica");

                // carregamento de vários detalhes das unidades físicas associadas
                //frd da uf
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                    "WHERE IDNivel=@IDNivel");
                da.Fill(currentDataSet, "FRDBase");
            }
        }
		
		public override int CountUFDimensoesAcumuladas(long IDTipoAcondicionamento, IDbTransaction tran) {
			string cmdText = String.Format("SELECT COUNT(*) FROM SFRDUFDescricaoFisica WITH (UPDLOCK) WHERE IDTipoAcondicionamento = {0}", IDTipoAcondicionamento);
            using (SqlCommand command = new SqlCommand(cmdText, (SqlConnection)tran.Connection, (SqlTransaction)tran))
            {
                return Convert.ToInt32(command.ExecuteScalar());
            }
		}
		#endregion

		#region " PanelOrganizacaoOrdenacao "
		public override void LoadOrganizacaoOrdenacaoData(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CurrentFRDBaseID", CurrentFRDBaseID);

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDTradicaoDocumental"], "WHERE IDFRDBase=@CurrentFRDBaseID");
				da.Fill(currentDataSet, "SFRDTradicaoDocumental");
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDOrdenacao"], "WHERE IDFRDBase=@CurrentFRDBaseID");
				da.Fill(currentDataSet, "SFRDOrdenacao");
			}
		}
		#endregion

		#region " FRDOIRecolha "
		public override void ReloadPubNivelActualData(DataSet currentDataSet, long NivelEstrututalDocumentalID, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@NivelEstrututalDocumentalID", NivelEstrututalDocumentalID);

				// recarregar nível actual, os níveis pais e as relações entre eles
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "WHERE ID=@NivelEstrututalDocumentalID");
				da.Fill(currentDataSet, "Nivel");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "INNER JOIN RelacaoHierarquica rh ON rh.IDUpper = Nivel.ID WHERE rh.ID=@NivelEstrututalDocumentalID");
				da.Fill(currentDataSet, "Nivel");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                    "WHERE ID=@NivelEstrututalDocumentalID");
				da.Fill(currentDataSet, "RelacaoHierarquica");
				// obter nao so o frdbase em causa como também os outros FRDs deste nível
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                    "WHERE IDNivel=@NivelEstrututalDocumentalID");
				da.Fill(currentDataSet, "FRDBase");
			}
		}

		public override void LoadFRDOIRecolhaData(DataSet currentDataSet, long NivelEstrututalDocumentalID, string TipoFRDBase, IDbConnection conn)
		{
			using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@NivelEstrututalDocumentalID", NivelEstrututalDocumentalID);
                command.Parameters.AddWithValue("@TipoFRDBase", TipoFRDBase);

				// Add-on
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],
                    "INNER JOIN NivelControloAut ON NivelControloAut.IDControloAut=ControloAut.ID  WHERE NivelControloAut.ID = @NivelEstrututalDocumentalID");
				da.Fill(currentDataSet, "ControloAut");
				//

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelControloAut"],
                    "WHERE ID=@NivelEstrututalDocumentalID");
				da.Fill(currentDataSet, "NivelControloAut");

				DataRow ncaRow = currentDataSet.Tables["NivelControloAut"].Select(
					string.Format("ID={0}", NivelEstrututalDocumentalID))[0];

                command.Parameters.AddWithValue("@IDControloAut", ncaRow["IDControloAut"]);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["IndexFRDCA"],
                    "WHERE IDFRDBase IN (SELECT ID FROM FRDBase WHERE IDNivel=@NivelEstrututalDocumentalID AND IDTipoFRDBase=@TipoFRDBase) AND IDControloAut=@IDControloAut");
				da.Fill(currentDataSet, "IndexFRDCA");
			}
		}
		#endregion

		#region " FRDUnidadeFisica "
		public override void LoadFRDUnidadeFisicaData(DataSet currentDataSet, long NivelUnidadeFisicaID, string TipoFRDBase, IDbConnection conn)
		{
			// Recarregar a uf actual e guardar um contexto localmente
			string nivelQuery = "WHERE ID=@NivelUnidadeFisicaID";
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@NivelUnidadeFisicaID", NivelUnidadeFisicaID);
                command.Parameters.AddWithValue("@TipoFRDBase", TipoFRDBase);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"], 
					nivelQuery);
				da.Fill(currentDataSet, "Nivel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"],
                    nivelQuery);
                da.Fill(currentDataSet, "NivelDesignado");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelUnidadeFisica"],
                    nivelQuery);
                da.Fill(currentDataSet, "NivelUnidadeFisica");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "INNER JOIN RelacaoHierarquica rh ON rh.IDUpper=Nivel.ID WHERE rh.ID=@NivelUnidadeFisicaID");
				da.Fill(currentDataSet, "Nivel");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"], 
					nivelQuery);
				da.Fill(currentDataSet, "RelacaoHierarquica");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                    "WHERE IDNivel=@NivelUnidadeFisicaID AND IDTipoFRDBase=@TipoFRDBase");
				da.Fill(currentDataSet, "FRDBase");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["LocalConsulta"]);
                da.Fill(currentDataSet, "LocalConsulta");
			}
		}
		#endregion

		#region " PanelAvaliacaoDocumentosUnidadesFisicas "
        public override void LoadCurrentFRDAvaliacao(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@CurrentFRDBaseID", CurrentFRDBaseID);
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDAvaliacao"],
                    "WHERE IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "SFRDAvaliacao");
            }
        }
		public override void LoadPanelAvaliacaoDocumentosUnidadesFisicasData(DataSet currentDataSet, long CurrentFRDBaseID, long CurrentFRDBaseNivelRowID, long CurrentIDTipoFRDBase, long FRDUFIDTipoFRDBase, long grpAcPubID, IDbConnection conn)
		{
			using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.CommandText =
                    "CREATE TABLE #NiveisTemp (IDNivel BIGINT); " +
                    "INSERT INTO #NiveisTemp SELECT DISTINCT ID FROM #TempRelacaoHierarquica " +
                    "INSERT INTO #NiveisTemp SELECT DISTINCT IDUF FROM #UFRelated";
                command.ExecuteNonQuery();

                command.Parameters.AddWithValue("@isDeleted", 0);
                command.Parameters.AddWithValue("@CurrentFRDBaseID", CurrentFRDBaseID);
                command.Parameters.AddWithValue("@grpAcPubID", grpAcPubID);

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["AutoEliminacao"]);
                da.Fill(currentDataSet, "AutoEliminacao");

                // carregar niveis e frds de Niveis Documentais e Unidades Físicas
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "INNER JOIN #NiveisTemp ON #NiveisTemp.IDNivel = Nivel.ID");
                da.Fill(currentDataSet, "Nivel");

                da.SelectCommand.CommandText =                    
                    "SELECT FRDBase.ID, FRDBase.IDNivel, FRDBase.IDTipoFRDBase, FRDBase.Versao, FRDBase.isDeleted FROM FRDBase " +
                    "INNER JOIN #NiveisTemp ON #NiveisTemp.IDNivel = FRDBase.IDNivel " +
                    "WHERE FRDBase.isDeleted = @isDeleted";
                da.Fill(currentDataSet, "FRDBase");

                // carregar informação referente a níveis documentais
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDAvaliacao"],
                    " INNER JOIN FRDBase frd ON frd.ID = SFRDAvaliacao.IDFRDBase " +
                    " INNER JOIN (SELECT DISTINCT ID FROM #TempRelacaoHierarquica) Docs ON Docs.ID = frd.IDNivel " +
                    " WHERE SFRDAvaliacao.IDFRDBase <> @CurrentFRDBaseID");
                da.Fill(currentDataSet, "SFRDAvaliacao");

                // carregar informação referente a  unidades físicas associadas aos níveis documentais
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDUFAutoEliminacao"],
                    " INNER JOIN FRDBase frd ON frd.ID = SFRDUFAutoEliminacao.IDFRDBase " +
                    " INNER JOIN (SELECT DISTINCT IDUF FROM #UFRelated) UFs ON UFs.IDUF = frd.IDNivel ");
                da.Fill(currentDataSet, "SFRDUFAutoEliminacao");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeNivelPrivilege"],
                    " INNER JOIN (SELECT DISTINCT ID FROM #TempRelacaoHierarquica) Docs ON Docs.ID = TrusteeNivelPrivilege.IDNivel " +
                    " WHERE IDTrustee = @grpAcPubID");
                da.Fill(currentDataSet, "TrusteeNivelPrivilege");

                command.CommandText = "DROP TABLE #TempRelacaoHierarquica; DROP TABLE #UFRelated; DROP TABLE #NiveisTemp";
                command.ExecuteNonQuery();
			}
		}
		#endregion

		#region " PanelAVCondicoesAcesso "
		public override void LoadPanelAVCondicoesAcessoData(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CurrentFRDBaseID", CurrentFRDBaseID);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDCondicaoDeAcesso"],
                    "WHERE IDFRDBase=@CurrentFRDBaseID");
				da.Fill(currentDataSet, "SFRDCondicaoDeAcesso");
				

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDLingua"],
                    "WHERE SFRDLingua.IDFRDBase=@CurrentFRDBaseID");
				da.Fill(currentDataSet, "SFRDLingua");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Iso639"], 
					"INNER JOIN SFRDLingua on Iso639.ID = SFRDLingua.IDIso639 " +
                    "WHERE SFRDLingua.IDFRDBase=@CurrentFRDBaseID");
				da.Fill(currentDataSet, "Iso639");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDAlfabeto"],
                    "WHERE SFRDAlfabeto.IDFRDBase=@CurrentFRDBaseID");
				da.Fill(currentDataSet, "SFRDAlfabeto");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Iso15924"], 
					"INNER JOIN SFRDAlfabeto on Iso15924.ID = SFRDAlfabeto.IDIso15924 " +
                    "WHERE SFRDAlfabeto.IDFRDBase=@CurrentFRDBaseID");
				da.Fill(currentDataSet, "Iso15924");
			}
		}
		#endregion

		#region " PanelCARelacoes "
		public override void LoadRetrieveSelectionData(DataSet currentDataSet, long cadRowIDControloAut, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@cadRowIDControloAut", cadRowIDControloAut);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    " INNER JOIN NivelControloAut nca ON nca.ID=Nivel.ID WHERE IDControloAut=@cadRowIDControloAut");
				da.Fill(currentDataSet, "Nivel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                    " INNER JOIN NivelControloAut nca ON nca.ID=FRDBase.IDNivel WHERE IDControloAut=@cadRowIDControloAut");
                da.Fill(currentDataSet, "FRDBase");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelControloAut"],
                    "WHERE IDControloAut=@cadRowIDControloAut");
				da.Fill(currentDataSet, "NivelControloAut");
			}
		}
		#endregion

		#region " PanelAvaliacaoSeleccaoEliminacao "		
		public override void LoadPanelAvaliacaoSeleccaoEliminacaoData(DataSet currentDataSet, long CurrentFRDBaseID, long CurrentNivelID, long grpAcPubID, IDbConnection conn)
		{
            string WhereQueryFilter = "WHERE IDFRDBase=@CurrentFRDBaseID";

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CurrentFRDBaseID", CurrentFRDBaseID);
                command.Parameters.AddWithValue("@grpAcPubID", grpAcPubID);
                command.Parameters.AddWithValue("@CurrentNivelID", CurrentNivelID);
                command.Parameters.AddWithValue("@IDTipoNoticiaAut1", 7);
                command.Parameters.AddWithValue("@IDTipoNoticiaAut2", 8);

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ListaModelosAvaliacao"]);
				da.Fill(currentDataSet, "ListaModelosAvaliacao");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ModelosAvaliacao"]);
				da.Fill(currentDataSet, "ModelosAvaliacao");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDAvaliacao"],
					WhereQueryFilter);
				da.Fill(currentDataSet, "SFRDAvaliacao");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
					"INNER JOIN SFRDAvaliacaoRel ON IDNivel = Nivel.ID " + WhereQueryFilter);
				da.Fill(currentDataSet, "Nivel");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDAvaliacaoRel"],
					WhereQueryFilter);
				da.Fill(currentDataSet, "SFRDAvaliacaoRel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "INNER JOIN FRDBase frd ON frd.IDNivel = Nivel.ID " +
                    "WHERE frd.ID = @CurrentFRDBaseID");
                da.Fill(currentDataSet, "Nivel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeNivelPrivilege"],
                    " WHERE TrusteeNivelPrivilege.IDNivel = @CurrentNivelID AND TrusteeNivelPrivilege.IDTrustee = @grpAcPubID ");
                da.Fill(currentDataSet, "TrusteeNivelPrivilege");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],
                    "INNER JOIN IndexFRDCA ON IndexFRDCA.IDControloAut = ControloAut.ID " +
                    "WHERE ControloAut.IDTipoNoticiaAut BETWEEN @IDTipoNoticiaAut1 AND @IDTipoNoticiaAut2 AND IndexFRDCA.IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "ControloAut");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"],
                    "INNER JOIN ControloAutDicionario ON ControloAutDicionario.IDDicionario = Dicionario.ID " +
                    "INNER JOIN ControloAut ON ControloAut.ID = ControloAutDicionario.IDControloAut " +
                    "INNER JOIN IndexFRDCA ON IndexFRDCA.IDControloAut = ControloAut.ID " +
                    "WHERE ControloAut.IDTipoNoticiaAut BETWEEN @IDTipoNoticiaAut1 AND @IDTipoNoticiaAut2 AND IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "Dicionario");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"],
                    "INNER JOIN ControloAut ON ControloAut.ID = ControloAutDicionario.IDControloAut " +
                    "INNER JOIN IndexFRDCA ON IndexFRDCA.IDControloAut = ControloAut.ID " +
                    "WHERE ControloAut.IDTipoNoticiaAut BETWEEN @IDTipoNoticiaAut1 AND @IDTipoNoticiaAut2 AND IndexFRDCA.IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "ControloAutDicionario");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["IndexFRDCA"],
                    "INNER JOIN ControloAut ON ControloAut.ID = IndexFRDCA.IDControloAut " +
                    "WHERE ControloAut.IDTipoNoticiaAut BETWEEN @IDTipoNoticiaAut1 AND @IDTipoNoticiaAut2 AND IndexFRDCA.IDFRDBase=@CurrentFRDBaseID");
                da.Fill(currentDataSet, "IndexFRDCA");
			}
		}

		public override void ExecuteAvaliaDocumentosTabela(long frdID, long modeloAvaliacaoID, bool avaliacaoTabela, bool preservar, short prazoConservacao, IDbTransaction tran)
		{
			SqlCommand command = new SqlCommand("sp_avaliaDocumetosTabela", (SqlConnection) tran.Connection, (SqlTransaction) tran);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@frdID", SqlDbType.BigInt).Value = frdID;
			command.Parameters.Add("@modeloAvaliacaoID", SqlDbType.BigInt).Value = modeloAvaliacaoID;
			command.Parameters.Add("@avaliacaoTabela", SqlDbType.Bit).Value = avaliacaoTabela;
			command.Parameters.Add("@preservar", SqlDbType.Bit).Value = preservar;
			command.Parameters.Add("@prazoConservacao", SqlDbType.SmallInt).Value = prazoConservacao;
			command.ExecuteNonQuery();
		}

		#endregion

		#region " MasterPanelSeries "
		public override void LoadNivelAvaliacaoData(DataSet currentDataSet, long nivelID, IDbTransaction tran)
		{
            using (SqlCommand command = new SqlCommand("", (SqlConnection) tran.Connection, (SqlTransaction) tran))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.Parameters.AddWithValue("@nivelID", nivelID);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                    "WHERE IDNivel = @nivelID");
				da.Fill(currentDataSet, "FRDBase");
				long frdID = (long) currentDataSet.Tables["FRDBase"].Select(string.Format("IDNivel = {0} AND IDTipoFRDBase = 1", nivelID.ToString()))[0]["ID"];
                command.Parameters.AddWithValue("@frdID", frdID);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDAvaliacao"],
                    "WHERE IDFRDBase = @frdID");
				da.Fill(currentDataSet, "SFRDAvaliacao");
			}
		}


		public override void LoadSFRDAvaliacaoData(DataSet currentDataSet, long nivelID, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@nivelID", nivelID);

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                    "WHERE IDNivel = @nivelID");
				da.Fill(currentDataSet, "FRDBase");
				long frdID = (long) currentDataSet.Tables["FRDBase"].Select(string.Format("IDNivel = {0} AND IDTipoFRDBase = 1", nivelID.ToString()))[0]["ID"];
                command.Parameters.AddWithValue("@frdID", frdID);
                string WhereQueryFilter = " WHERE SFRDAvaliacao.IDFRDBase=@frdID";
				
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ListaModelosAvaliacao"],
					" INNER JOIN ModelosAvaliacao ON ModelosAvaliacao.IDListaModelosAvaliacao = ListaModelosAvaliacao.ID " +
					" INNER JOIN SFRDAvaliacao ON SFRDAvaliacao.IDModeloAvaliacao = ModelosAvaliacao.ID " + 
					WhereQueryFilter);
				da.Fill(currentDataSet, "ListaModelosAvaliacao");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ModelosAvaliacao"],
					" INNER JOIN SFRDAvaliacao ON SFRDAvaliacao.IDModeloAvaliacao = ModelosAvaliacao.ID " + 
					WhereQueryFilter);
				da.Fill(currentDataSet, "ModelosAvaliacao");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TipoPertinencia"],
					" INNER JOIN SFRDAvaliacao ON SFRDAvaliacao.IDPertinencia = TipoPertinencia.ID " + 
					WhereQueryFilter);
				da.Fill(currentDataSet, "TipoPertinencia");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TipoDensidade"],
					" INNER JOIN SFRDAvaliacao ON SFRDAvaliacao.IDDensidade = TipoDensidade.ID " + 
					WhereQueryFilter);
				da.Fill(currentDataSet, "TipoDensidade");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TipoSubDensidade"],
					" INNER JOIN SFRDAvaliacao ON SFRDAvaliacao.IDSubdensidade = TipoSubDensidade.ID " + 
					WhereQueryFilter);
				da.Fill(currentDataSet, "TipoSubDensidade");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["AutoEliminacao"],
					" INNER JOIN SFRDAvaliacao ON SFRDAvaliacao.IDAutoEliminacao = AutoEliminacao.ID " + 
					WhereQueryFilter);
				da.Fill(currentDataSet, "AutoEliminacao");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDAvaliacao"],
					WhereQueryFilter);
				da.Fill(currentDataSet, "SFRDAvaliacao");
			}
		}
		#endregion
	}
}