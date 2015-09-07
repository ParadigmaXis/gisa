using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
	public class SqlClientUFRule: UFRule
	{
		#region " PanelUFConteudoEstrutura "
		public override void LoadUFConteudoEstruturaData(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn)
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

		#region " PanelUFControloDescricao "
		public override void LoadUFControloDescricaoData(DataSet currentDataSet, DataSet newDataSet, long CurrentFRDBaseID, IDbConnection conn)
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
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Trustee"], "WHERE ID IN (SELECT ID FROM TrusteeUser WHERE IsAuthority=@IsAuthority)");
				da.Fill(currentDataSet, "Trustee");
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeUser"], "WHERE IsAuthority=@IsAuthority");
				da.Fill(currentDataSet, "TrusteeUser");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Trustee"], WhereUserQueryFilter);
				da.Fill(currentDataSet, "Trustee");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeUser"], WhereUserQueryFilter);
				da.Fill(currentDataSet, "TrusteeUser");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBaseDataDeDescricao"], WhereQueryFilter);
                da.Fill(currentDataSet, "FRDBaseDataDeDescricao");
			}
		}

        public override List<string> GetNiveisDocAssociados(long CurrentUFID, IDbConnection conn)
        {
            List<string> Ids = new List<string>();
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText =
                "SELECT FRDBase.IDNivel " +
                "FROM SFRDUnidadeFisica " +
                    "INNER JOIN FRDBase ON FRDBase.ID = SFRDUnidadeFisica.IDFRDBase " +
                    "INNER JOIN Nivel ON Nivel.ID = FRDBase.IDNivel " +
                "WHERE SFRDUnidadeFisica.IDNivel = " + CurrentUFID.ToString() + " " +
                    "AND Nivel.IDTipoNivel = 3 " +
                    "AND SFRDUnidadeFisica.isDeleted=0 " +
                    "AND FRDBase.isDeleted=0 " +
                    "AND Nivel.isDeleted=0";
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
                Ids.Add(reader.GetValue(0).ToString());

            reader.Close();

            return Ids;
        }
		#endregion

		#region " PanelUFIdentificacao2 "
		public override void LoadUFIdentificacao2Data(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn)
		{
			string WhereQueryFilter = "WHERE IDFRDBase=" + CurrentFRDBaseID.ToString();
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CurrentFRDBaseID", CurrentFRDBaseID);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDDatasProducao"], WhereQueryFilter);
				da.Fill(currentDataSet, "SFRDDatasProducao");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDUFCota"], WhereQueryFilter);
				da.Fill(currentDataSet, "SFRDUFCota");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDUFDescricaoFisica"], WhereQueryFilter);
				da.Fill(currentDataSet, "SFRDUFDescricaoFisica");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDConteudoEEstrutura"], WhereQueryFilter);
				da.Fill(currentDataSet, "SFRDConteudoEEstrutura");
			}			
		}

		public override void LoadTipoAcondicionamento(DataSet currentDataSet, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TipoAcondicionamento"]);
				da.Fill(currentDataSet, "TipoAcondicionamento");
			}
		}
		#endregion

		#region UnidadeFisicaList
        bool uaCalculated;
        bool autosCalculated;
        public override void CalculateOrderedItems(long TipoNivelRelacionadoUF, string FiltroDesignacaoLike, string FiltroCodigoLike, string FiltroCotaLike, string FiltroCodigoBarrasLike, string FiltroConteudoLike, string FiltroAndParentNivel, bool onlyNotAssociadas, bool mostrarEliminados, ArrayList ordenacao, IDbConnection conn)
		{
            // aplicar o filtro
            StringBuilder innerJoinQuery = new StringBuilder();
            StringBuilder leftJoinQuery = new StringBuilder();
            StringBuilder whereQuery = new StringBuilder();
            string ufsAssociadasQuery = string.Empty;
            string autosAssociadosQuery = string.Empty;
            uaCalculated = false;

            // se não tivermos um contexto todas as unidades físicas serão
            // tidas em conta, caso contrário apenas as unidades físicas
            // que pertençam à entidade detentora de contexto serão tidas
            // em conta.
            if (FiltroAndParentNivel.Length > 0)
            {
                innerJoinQuery.Append("INNER JOIN RelacaoHierarquica ON Nivel.ID=RelacaoHierarquica.ID ");
                innerJoinQuery.Append("INNER JOIN Nivel ParentNivel ON ParentNivel.ID = RelacaoHierarquica.IDUpper ");
                whereQuery.Append(" AND RelacaoHierarquica.isDeleted = 0 ");
                whereQuery.Append(" AND ParentNivel.isDeleted = 0 ");
                whereQuery.Append(FiltroAndParentNivel);
            }

            if (FiltroDesignacaoLike.Length > 0 || FiltroCodigoBarrasLike.Length > 0)
                innerJoinQuery.Append(" INNER JOIN NivelDesignado ON Nivel.ID = NivelDesignado.ID ");

			if (FiltroDesignacaoLike.Length > 0) 
				whereQuery.AppendFormat(" AND {0}", FiltroDesignacaoLike);

            if (FiltroCodigoBarrasLike.Length > 0)
            {
                innerJoinQuery.Append(" INNER JOIN NivelUnidadeFisica ON NivelDesignado.ID = NivelUnidadeFisica.ID ");
                whereQuery.AppendFormat(" AND {0}", FiltroCodigoBarrasLike);
            }

            if (FiltroCodigoLike.Length > 0)
            {
                innerJoinQuery.Append(" INNER JOIN RelacaoHierarquica rh ON rh.ID = Nivel.ID AND rh.isDeleted = 0 ");
                innerJoinQuery.Append(" INNER JOIN Nivel nED ON nED.ID = rh.IDUpper and nED.isDeleted = 0 ");
                whereQuery.AppendFormat(" AND {0}", FiltroCodigoLike);
            }

            if (FiltroCotaLike.Length > 0 || FiltroConteudoLike.Length > 0)
                innerJoinQuery.Append(" INNER JOIN FRDBase ON FRDBase.IDNivel = Nivel.ID ");

            if (FiltroCotaLike.Length > 0)
            {
                innerJoinQuery.Append(" INNER JOIN SFRDUFCota ON SFRDUFCota.IDFRDBase = FRDBase.ID ");
                whereQuery.AppendFormat(" AND {0}", FiltroCotaLike);
            }

            if (FiltroConteudoLike.Length > 0)
            {
                innerJoinQuery.Append(" INNER JOIN SFRDConteudoEEstrutura ON SFRDConteudoEEstrutura.IDFRDBase = FRDBase.ID ");
                whereQuery.AppendFormat(" AND {0}", FiltroConteudoLike);
            }

            if (!mostrarEliminados)
                leftJoinQuery.Append(" INNER JOIN NivelUnidadeFisica nuf ON nuf.ID = Nivel.ID AND nuf.isDeleted = 0 AND (nuf.Eliminado IS NULL OR nuf.Eliminado = 0) ");
            else
            {
                autosAssociadosQuery = @"
                    INSERT INTO #autosAssociados
                    SELECT n.ID ufID, AutosEliminacao = LEFT(o1.list, LEN(o1.list)-1)
                    FROM Nivel n
                        INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                        CROSS APPLY ( 
                            SELECT CONVERT(VARCHAR(200), autos.Designacao) + '; ' AS [text()]
                            FROM (
                                SELECT ae.Designacao
                                FROM SFRDUFAutoEliminacao ufae
                                    INNER JOIN AutoEliminacao ae ON ae.ID = ufae.IDAutoEliminacao AND ae.isDeleted = 0
                                WHERE ufae.isDeleted = 0 AND ufae.IDFRDBase = frd.ID
                                UNION
                                SELECT ae.Designacao
                                FROM SFRDUnidadeFisica sfrduf
                                    INNER JOIN FRDBase frdNvlDoc ON frdNvlDoc.ID = sfrduf.IDFRDBase AND frdNvlDoc.isDeleted = 0
                                    INNER JOIN SFRDAvaliacao av ON av.IDFRDBase = frdNvlDoc.ID AND av.isDeleted = 0
                                    INNER JOIN AutoEliminacao ae ON ae.ID = av.IDAutoEliminacao AND ae.isDeleted = 0
                                WHERE sfrduf.isDeleted = 0 AND sfrduf.IDNivel = frd.IDNivel
                            ) autos
                            GROUP BY autos.Designacao
                            FOR XML PATH('') 
                        ) o1 (list)
                    WHERE n.IDTipoNivel = 4 ";
                autosCalculated = true;
            }

            if (onlyNotAssociadas)
            {
                // criar tabela temporária com o numero de unidades informacionais associadas a
                // cada unidade física pretendida
                ufsAssociadasQuery =
                    "SELECT sfrduf.IDNivel, COUNT(*) NumUnidadesInformacionais " +
                    "INTO #NumUnidadesInformacionais " +
                    "FROM Nivel n " +
                        "INNER JOIN SFRDUnidadeFisica sfrduf ON sfrduf.IDNivel = n.ID " +
                    "WHERE sfrduf.isDeleted = 0 " +
                    "GROUP BY sfrduf.IDNivel; ";

                leftJoinQuery.Append(" LEFT JOIN #NumUnidadesInformacionais num ON num.IDNivel = Nivel.ID ");
                whereQuery.Append(" AND num.NumUnidadesInformacionais IS NULL ");
                uaCalculated = true;
            }

			SqlCommand command = new SqlCommand("", (SqlConnection) conn);
            command.CommandText = 
                "CREATE TABLE #FilteredUFs (ID BIGINT PRIMARY KEY); " +
                "CREATE TABLE #autosAssociados (ufID BIGINT, AutosEliminacao NVARCHAR(4000)); " +
                autosAssociadosQuery +
                ufsAssociadasQuery +
                "INSERT INTO #FilteredUFs " +
                "SELECT Nivel.ID " +
                "FROM Nivel " +
                    innerJoinQuery.ToString() +
                    leftJoinQuery.ToString() +
                "WHERE Nivel.IDTipoNivel = 4 " + 
                    "AND Nivel.isDeleted = 0 " +
                    whereQuery.ToString();
            command.ExecuteNonQuery();

            // ordenar as unidades físicas segundo o critério definido pelo utilizador
            innerJoinQuery = new StringBuilder();
            leftJoinQuery = new StringBuilder();
            whereQuery = new StringBuilder();
            StringBuilder orderByQuery = new StringBuilder();
            bool joinFRDBase = false;
            ufsAssociadasQuery = string.Empty;
            autosAssociadosQuery = string.Empty;
            for (int i = 0; i < ordenacao.Count; i = i + 2)
            {
                Object a = ordenacao[i];
                string order = string.Empty;
                if ((bool)ordenacao[i + 1])
                    order = "ASC";
                else
                    order = "DESC";

                if (orderByQuery.Length > 0)
                    orderByQuery.Append(", ");
                
                switch ((int)a)
                {
                    //identificador
                    case 0:
                        orderByQuery.AppendFormat(" n.ID {0}", order);
                        break;
                    //código
                    case 1:
                        innerJoinQuery.Append(
                            " INNER JOIN RelacaoHierarquica rh ON rh.ID = n.ID AND rh.IDTipoNivelRelacionado = 11 AND rh.isDeleted = 0 " +
                            " INNER JOIN Nivel nED ON nED.ID = rh.IDUpper AND nED.IDTipoNivel = 1 AND nED.isDeleted = 0 ");
                        orderByQuery.AppendFormat(
                            " nED.Codigo {0}," +
                            " CONVERT(SMALLINT, SUBSTRING(n.Codigo, 3, 4)) {0}," +
                            " CONVERT(BIGINT, SUBSTRING(n.Codigo, 8,10)) {0}", order);
                        break;
                    //designação
                    case 2:
                        innerJoinQuery.Append(" INNER JOIN NivelDesignado nd ON nd.ID = n.ID");
                        orderByQuery.AppendFormat(" nd.Designacao {0}", order);
                        whereQuery.Append(" AND nd.isDeleted = 0");
                        break;
                    //cota
                    case 3:
                        if (!joinFRDBase)
                        {
                            innerJoinQuery.Append(" INNER JOIN FRDBase frd ON frd.IDNivel = n.ID");
                            whereQuery.Append(" AND frd.isDeleted = 0");
                            joinFRDBase = true;
                        }
                        leftJoinQuery.Append(" LEFT JOIN SFRDUFCota cota ON cota.IDFRDBase = frd.ID");
                        orderByQuery.AppendFormat(" cota.Cota {0}", order);
                        whereQuery.Append(" AND (cota.isDeleted IS NULL OR cota.isDeleted = 0)");
                        break;
                    //código de barras
                    case 4:
                        leftJoinQuery.Append(" LEFT JOIN NivelUnidadeFisica nuf ON nuf.ID = n.ID");
                        orderByQuery.AppendFormat(" nuf.CodigoBarras {0}", order);
                        whereQuery.Append(" AND (nuf.isDeleted IS NULL OR nuf.isDeleted = 0)");
                        break;
                    //datas de produção
                    case 5:
                        if (!joinFRDBase)
                        {
                            innerJoinQuery.Append(" INNER JOIN FRDBase frd ON frd.IDNivel = n.ID");
                            whereQuery.Append(" AND frd.isDeleted = 0");
                            joinFRDBase = true;
                        }
                        leftJoinQuery.Append(" LEFT JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frd.ID");
                        orderByQuery.AppendFormat(" dp.InicioAno {0}, dp.InicioMes {0}, dp.InicioDia {0}, dp.FimAno {0}, dp.FimMes {0}, dp.FimDia {0}", order);
                        whereQuery.Append(" AND (dp.isDeleted IS NULL OR dp.isDeleted = 0)");
                        break;
                    //número de unidades de descrição associadas
                    case 6:
                        // criar tabela temporária com o numero de unidades informacionais associadas a
                        // cada unidade física pretendida
                        if (!uaCalculated)
                        {
                            ufsAssociadasQuery =
                                "SELECT sfrduf.IDNivel, COUNT(*) NumUnidadesInformacionais " +
                                "INTO #NumUnidadesInformacionais " +
                                "FROM #FilteredUFs " +
                                    "INNER JOIN SFRDUnidadeFisica sfrduf ON sfrduf.IDNivel = #FilteredUFs.ID " +
                                    "INNER JOIN FRDBase frd ON frd.ID = sfrduf.IDFRDBase " +
                                "WHERE sfrduf.isDeleted = 0 AND frd.isDeleted = 0 " +
                                "GROUP BY sfrduf.IDNivel; ";
                            uaCalculated = true;
                        }

                        leftJoinQuery.Append(" LEFT JOIN #NumUnidadesInformacionais num ON num.IDNivel = n.ID ");
                        orderByQuery.AppendFormat(" COALESCE(num.NumUnidadesInformacionais, 0) {0}", order);
                        break;
                    // Em deposito:
                    case 7:
                        if (mostrarEliminados && !autosCalculated)
                        {
                            autosAssociadosQuery = @"
                                INSERT INTO #autosAssociados
                                SELECT autos.ufID, AutosEliminacao = LEFT(o1.list, LEN(o1.list)-1)
                                FROM #FilteredUFs n
                                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0
                                    CROSS APPLY ( 
                                        SELECT CONVERT(VARCHAR(200), autos.Designacao) + '; ' AS [text()]
                                        FROM (
                                            SELECT ae.Designacao
                                            FROM SFRDUFAutoEliminacao ufae
                                                INNER JOIN AutoEliminacao ae ON ae.ID = ufae.IDAutoEliminacao AND ae.isDeleted = 0
                                            WHERE ufae.isDeleted = 0 AND ufae.IDFRDBase = frd.ID
                                            UNION
                                            SELECT ae.Designacao
                                            FROM SFRDUnidadeFisica sfrduf
                                                INNER JOIN FRDBase frdNvlDoc ON frdNvlDoc.ID = sfrduf.IDFRDBase AND frdNvlDoc.isDeleted = 0
	                                            INNER JOIN SFRDAvaliacao av ON av.IDFRDBase = frdNvlDoc.ID AND av.isDeleted = 0
	                                            INNER JOIN AutoEliminacao ae ON ae.ID = av.IDAutoEliminacao AND ae.isDeleted = 0
                                            WHERE sfrduf.isDeleted = 0 AND sfrduf.IDNivel = frd.IDNivel
                                        ) autos
                                        GROUP BY autos.Designacao
                                        FOR XML PATH('') 
                                    ) o1 (list) ";
                        }
                        leftJoinQuery.Append(" LEFT JOIN NivelUnidadeFisica nuf_1 ON nuf_1.ID = n.ID");
                        leftJoinQuery.Append(" LEFT JOIN #autosAssociados autos ON autos.ufID = n.ID");
                        orderByQuery.AppendFormat(" nuf_1.Eliminado {0}, COALESCE(autos.AutosEliminacao, '') {0}", order);
                        break;
                }
            }

            StringBuilder com = new StringBuilder();
            // criar tabela para calcular a ordenação dos items a serem apresentados na lista
            com.Append("CREATE TABLE #OrderedItems (seq_id INT Identity(1,1) NOT NULL, ID BIGINT NOT NULL ); ");

            // calcular a ordenação dos items a serem apresentados na pesquisa
            if (ordenacao.Count > 0)
                com.AppendFormat(
                    autosAssociadosQuery + 
                    ufsAssociadasQuery +
                    "INSERT INTO #OrderedItems (ID) " +
                    "SELECT n.ID " +
                    "FROM #FilteredUFs fUF " + 
                        "INNER JOIN Nivel n ON n.ID = fUF.ID " +
                        "{0} " + 
                        "{1} " + 
                    "WHERE n.IDTipoNivel = 4 AND n.isDeleted = 0 {2} " + 
                    "ORDER BY {3}; ", innerJoinQuery, leftJoinQuery, whereQuery, orderByQuery);
            else
                com.Append(
                    "INSERT INTO #OrderedItems (ID) " +
                    "SELECT n.ID " +
                    "FROM #FilteredUFs n; ");

            command.CommandType = CommandType.Text;
            command.CommandText = com.ToString();
            command.ExecuteNonQuery();
		}

		public override ArrayList GetItems(DataSet currentDataSet, int pageNr, int itemsPerPage, IDbConnection conn)
		{
			ArrayList rows = new ArrayList();

            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                if (!uaCalculated)
                {
                    command.CommandText = "CREATE TABLE #NumUnidadesInformacionais(IDNivel BIGINT, NumUnidadesInformacionais BIGINT)";
                    command.ExecuteNonQuery();

                    command.CommandText = 
                        "INSERT INTO #NumUnidadesInformacionais " +
                        "SELECT sfrduf.IDNivel, COUNT(*) NumUnidadesInformacionais " +
                        "FROM #OrderedItems oi " +
                            "INNER JOIN SFRDUnidadeFisica sfrduf ON sfrduf.IDNivel = oi.ID " +
                        "WHERE seq_id >= @seq_id1 AND seq_id <= @seq_id2 AND sfrduf.isDeleted = @isDeleted " +
                        "GROUP BY sfrduf.IDNivel";
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    command.Parameters.AddWithValue("@seq_id1", (pageNr - 1) * itemsPerPage + 1);
                    command.Parameters.AddWithValue("@seq_id2", pageNr * itemsPerPage);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    uaCalculated = true;
                }

                command.CommandText = "CREATE TABLE #ItemsID (ID BIGINT, Codigo NVARCHAR(110), Designacao NVARCHAR(768), Cota NVARCHAR(300), CodigoBarras NVARCHAR(20), InicioAno NVARCHAR(4), " + 
                    "InicioMes NVARCHAR(2), InicioDia NVARCHAR(2), InicioAtribuida BIT, FimAno NVARCHAR(4), FimMes NVARCHAR(2), FimDia NVARCHAR(2), FimAtribuida BIT, NumUnidadesInformacionais BIGINT, " +
                    "Eliminado BIT, AutosEliminacao NVARCHAR(MAX))";
                command.ExecuteNonQuery();

                command.CommandText = 
                    "INSERT INTO #ItemsID " +
                    "SELECT n.ID, nED.Codigo + '/' + n.Codigo Codigo, nd.Designacao, cota.Cota, nuf.CodigoBarras, dp.InicioAno, dp.InicioMes, " +
                        "dp.InicioDia, dp.InicioAtribuida, dp.FimAno, dp.FimMes, dp.FimDia, dp.FimAtribuida, " +
                        "COALESCE(num.NumUnidadesInformacionais, 0) NumUnidadesInformacionais, nuf.Eliminado, autos.AutosEliminacao " +
                    "FROM #OrderedItems oi " +
                        "INNER JOIN Nivel n ON n.ID = oi.ID " +
                        "INNER JOIN NivelDesignado nd ON nd.ID = n.ID " +
                        "INNER JOIN RelacaoHierarquica rh ON rh.ID = n.ID " +
                        "INNER JOIN Nivel nED ON nED.ID = rh.IDUpper " +
                        "INNER JOIN FRDBase frd ON frd.IDNivel = n.ID " +
                        "LEFT JOIN NivelUnidadeFisica nuf ON nuf.ID = nd.ID " +
                        "LEFT JOIN SFRDUFCota cota ON cota.IDFRDBase = frd.ID " +
                        "LEFT JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frd.ID " +
                        "LEFT JOIN #NumUnidadesInformacionais num ON num.IDNivel = n.ID " +
                        "LEFT JOIN #autosAssociados autos ON autos.ufID = n.ID " +
                    "WHERE seq_id >= @seq_id1 AND seq_id <= @seq_id2 " +
                        "AND n.isDeleted = @isDeleted " +
                        "AND nd.isDeleted = @isDeleted " +
                        "AND rh.isDeleted = @isDeleted " +
                        "AND nED.isDeleted = @isDeleted " +
                        "AND frd.isDeleted = @isDeleted " +
                        "AND (cota.isDeleted IS NULL OR cota.isDeleted = @isDeleted) " +
                        "AND (nuf.isDeleted IS NULL OR nuf.isDeleted = @isDeleted) " +
                        "AND (dp.isDeleted IS NULL OR dp.isDeleted = @isDeleted) " +
                    "ORDER BY seq_id";
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.Parameters.AddWithValue("@seq_id1", (pageNr - 1) * itemsPerPage + 1);
                command.Parameters.AddWithValue("@seq_id2", pageNr * itemsPerPage);
                command.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN #ItemsID ON #ItemsID.ID = Nivel.ID");
                    da.Fill(currentDataSet, "Nivel");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                        "INNER JOIN #ItemsID ON #ItemsID.ID = FRDBase.IDNivel");
                    da.Fill(currentDataSet, "FRDBase");

                    // é necessário carregar esta informação para mostrar o código de uma unidade física seleccionada para o caso
                    // só existir uma única na lista
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN RelacaoHierarquica rh ON rh.IDUpper = Nivel.ID " +
                        "INNER JOIN #ItemsID ON #ItemsID.ID = rh.ID");
                    da.Fill(currentDataSet, "Nivel");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                        "INNER JOIN #ItemsID ON #ItemsID.ID = RelacaoHierarquica.ID");
                    da.Fill(currentDataSet, "RelacaoHierarquica");
                }

                command.CommandText = "SELECT * FROM #ItemsID;";
                command.Parameters.Clear();
                SqlDataReader reader = command.ExecuteReader();

                ArrayList row;
                while (reader.Read())
                {
                    row = new ArrayList();
                    row.Add(reader.GetValue(0));
                    row.Add(reader.GetValue(1));
                    row.Add(reader.GetValue(2));
                    row.Add(reader.GetValue(3));
                    row.Add(reader.GetValue(4));
                    row.Add(reader.GetValue(5));
                    row.Add(reader.GetValue(6));
                    row.Add(reader.GetValue(7));
                    row.Add(reader.GetValue(8));
                    row.Add(reader.GetValue(9));
                    row.Add(reader.GetValue(10));
                    row.Add(reader.GetValue(11));
                    row.Add(reader.GetValue(12));
                    row.Add(reader.GetValue(13));
                    row.Add(reader.IsDBNull(14) ? false : reader.GetValue(14)); // Eliminado: pode ser BDNull
                    row.Add(reader.IsDBNull(15) ? string.Empty : reader.GetValue(15));

                    rows.Add(row);
                }

                reader.Close();
            }
			
            return rows;
		}

		public override void DeleteTemporaryResults(IDbConnection conn)
		{
            using (SqlCommand command = new SqlCommand("DROP TABLE #FilteredUFs; DROP TABLE #NumUnidadesInformacionais; DROP TABLE #ItemsID; DROP TABLE #OrderedItems;", (SqlConnection)conn))
            {
                command.ExecuteNonQuery();
            }
		}
		#endregion

		#region " MasterPanelUnidadesFisicas "
		public override decimal IsCodigoUFBeingUsed (long idNivel, decimal ano, IDbTransaction tran)
		{
			decimal newCounterValue = 0;
			string cmd = 
				string.Format("SELECT * FROM NivelUnidadeFisicaCodigo WITH (UPDLOCK) WHERE ID={0} AND Ano={1}", idNivel, ano);
			SqlCommand command = new SqlCommand(cmd, (SqlConnection) tran.Connection, (SqlTransaction) tran);
			SqlDataReader dr = command.ExecuteReader();
			
			while(dr.Read())
			{
				newCounterValue = System.Convert.ToDecimal(dr.GetValue(2)) + 1;
			}
			dr.Close();

			if (newCounterValue != 0) 
			{
				cmd = string.Format("UPDATE NivelUnidadeFisicaCodigo SET Contador={0} WHERE ID={1} AND Ano={2}", newCounterValue, idNivel, ano);
				command.CommandText = cmd;
				command.ExecuteNonQuery();
			}
			else
			{
				cmd = string.Format("INSERT INTO NivelUnidadeFisicaCodigo (ID, Ano, Contador, isDeleted) VALUES ({0}, {1}, 1, 0)", idNivel, ano);
				command.CommandText = cmd;
				command.ExecuteNonQuery();
			}

			return newCounterValue;			
		}

		public override bool isNivelRowDeleted(long nivelID, IDbTransaction tran)
		{
			string cmd = string.Format("SELECT COUNT(*) FROM Nivel with (UPDLOCK) WHERE ID = {0} AND isDeleted = 1", nivelID.ToString());
			SqlCommand command = new SqlCommand(cmd, (SqlConnection) tran.Connection, (SqlTransaction) tran);
			SqlDataReader reader = command.ExecuteReader();
			reader.Read();
			int count = ((int)(reader.GetValue(0)));
			reader.Close();
			return count > 0;
		}

		public override bool isRelacaoHierarquicaDeleted(long nivelID, long nivelIDUpper, IDbTransaction tran)
		{
			string cmd = string.Format("SELECT COUNT(*) FROM RelacaoHierarquica with (UPDLOCK) WHERE ID = {0} AND IDUpper = {1} AND isDeleted = 1", nivelID.ToString(), nivelIDUpper.ToString());
			SqlCommand command = new SqlCommand(cmd, (SqlConnection) tran.Connection, (SqlTransaction) tran);
			SqlDataReader reader = command.ExecuteReader();
			reader.Read();
			int count = ((int)(reader.GetValue(0)));
			reader.Close();
			return count > 0;			
		}

		public override void InsertOrUpdateUF(DataRow ufRow, IDbTransaction tran)
		{
			if ((long)ufRow["ID"] < 0)
			{
				string cmd = string.Format("INSERT INTO Nivel (IDTipoNivel, Codigo, CatCode, isDeleted) Values (4, '{0}', 'NVL', 1); SELECT * FROM Nivel WHERE ID = @@IDENTITY;", ufRow["Codigo"]);
				SqlCommand command = new SqlCommand(cmd, (SqlConnection) tran.Connection, (SqlTransaction) tran);
				SqlDataReader reader = command.ExecuteReader();
				reader.Read();
				ufRow.Table.Columns["ID"].ReadOnly = false;
				ufRow["ID"] = System.Convert.ToInt64(reader.GetValue(0));
				ufRow.Table.Columns["ID"].ReadOnly = true;
				ufRow.Table.Columns["Versao"].ReadOnly = false;
				ufRow["Versao"] = (byte[]) reader.GetValue(4);
				ufRow.Table.Columns["Versao"].ReadOnly = true;
				reader.Close();
			}
			else
			{
				string cmd = string.Format("UPDATE Nivel SET Codigo = '{0}' WHERE ID = {1}", ufRow["Codigo"], ufRow["ID"]);
				SqlCommand command = new SqlCommand(cmd, (SqlConnection) tran.Connection, (SqlTransaction) tran);
				command.ExecuteNonQuery();
			}
		}

		public override void ReloadNivelUFCodigo(DataSet currentDataSet, long idNivel, decimal ano, IDbTransaction tran)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlTransaction)tran))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@idNivel", idNivel);
                command.Parameters.AddWithValue("@ano", ano);
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelUnidadeFisicaCodigo"], 
                    "WHERE ID=@idNivel AND Ano=@ano");
				da.Fill(currentDataSet, "NivelUnidadeFisicaCodigo");
			}
		}

        public override void LoadUF(DataSet currentDataSet, long idNivel, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@idNivel", idNivel);
                // nuvem nível
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "WHERE ID=@idNivel");
                da.Fill(currentDataSet, "Nivel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"],
                    "WHERE ID=@idNivel");
                da.Fill(currentDataSet, "NivelDesignado");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["LocalConsulta"]);
                da.Fill(currentDataSet, "LocalConsulta");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelUnidadeFisica"],
                    "WHERE ID=@idNivel");
                da.Fill(currentDataSet, "NivelUnidadeFisica");

                // entidade detentora
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                    "WHERE ID=@idNivel");
                da.Fill(currentDataSet, "RelacaoHierarquica");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "INNER JOIN RelacaoHierarquica rh ON rh.ID = Nivel.ID " +
                    "INNER JOIN Nivel nED ON nED.ID = rh.IDUpper " +
                    "WHERE Nivel.ID=@idNivel");
                da.Fill(currentDataSet, "Nivel");

                // nuvem frdbase
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                    "WHERE FRDBase.IDNivel=@idNivel");
                da.Fill(currentDataSet, "FRDBase");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDDatasProducao"],
                    "INNER JOIN FRDBase frd ON frd.ID = SFRDDatasProducao.IDFRDBase " +
                    "WHERE frd.IDNivel=@idNivel");
                da.Fill(currentDataSet, "SFRDDatasProducao");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDUFCota"],
                    "INNER JOIN FRDBase frd ON frd.ID = SFRDUFCota.IDFRDBase " +
                    "WHERE frd.IDNivel=@idNivel");
                da.Fill(currentDataSet, "SFRDUFCota");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDConteudoEEstrutura"],
                    "INNER JOIN FRDBase frd ON frd.ID = SFRDConteudoEEstrutura.IDFRDBase " +
                    "WHERE frd.IDNivel=@idNivel");
                da.Fill(currentDataSet, "SFRDConteudoEEstrutura");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDUFDescricaoFisica"],
                    "INNER JOIN FRDBase frd ON frd.ID = SFRDUFDescricaoFisica.IDFRDBase " +
                    "WHERE frd.IDNivel=@idNivel");
                da.Fill(currentDataSet, "SFRDUFDescricaoFisica");

                // unidades informacionais associadas
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "INNER JOIN FRDBase ON FRDBase.IDNivel = Nivel.ID " +
                    "INNER JOIN SFRDUnidadeFisica ON SFRDUnidadeFisica.IDFRDBase = FRDBase.ID " +
                    "WHERE SFRDUnidadeFisica.IDNivel=@idNivel");
                da.Fill(currentDataSet, "Nivel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                    "INNER JOIN FRDBase ON FRDBase.IDNivel = RelacaoHierarquica.ID " +
                    "INNER JOIN SFRDUnidadeFisica ON SFRDUnidadeFisica.IDFRDBase = FRDBase.ID " +
                    "WHERE SFRDUnidadeFisica.IDNivel=@idNivel");
                da.Fill(currentDataSet, "RelacaoHierarquica");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "INNER JOIN RelacaoHierarquica ON RelacaoHierarquica.IDUpper = Nivel.ID " + 
                    "INNER JOIN FRDBase ON FRDBase.IDNivel = RelacaoHierarquica.ID " +
                    "INNER JOIN SFRDUnidadeFisica ON SFRDUnidadeFisica.IDFRDBase = FRDBase.ID " +
                    "WHERE SFRDUnidadeFisica.IDNivel=@idNivel");
                da.Fill(currentDataSet, "Nivel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                    "INNER JOIN SFRDUnidadeFisica ON SFRDUnidadeFisica.IDFRDBase = FRDBase.ID " +
                    "WHERE SFRDUnidadeFisica.IDNivel=@idNivel");
                da.Fill(currentDataSet, "FRDBase");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDUnidadeFisica"],
                    "WHERE IDNivel=@idNivel");
                da.Fill(currentDataSet, "SFRDUnidadeFisica");
            }
        }
		#endregion

		#region " PanelOIDimensoesSuporte "
        public override void LoadUFData(DataSet currentDataSet, long nivelID, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@nivelID", nivelID);

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"],
                    "WHERE ID=@nivelID");
                da.Fill(currentDataSet, "NivelDesignado");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelUnidadeFisica"],
                    "WHERE ID=@nivelID");
                da.Fill(currentDataSet, "NivelUnidadeFisica");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                    "WHERE ID=@nivelID");
                da.Fill(currentDataSet, "RelacaoHierarquica");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                    "WHERE IDNivel=@nivelID");
                da.Fill(currentDataSet, "FRDBase");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDUFCota"],
                    "INNER JOIN FRDBase frd ON frd.ID = SFRDUFCota.IDFRDBase " +
                    "WHERE frd.IDNivel=@nivelID");
                da.Fill(currentDataSet, "SFRDUFCota");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDUFDescricaoFisica"], 
					"INNER JOIN FRDBase frd ON frd.ID = SFRDUFDescricaoFisica.IDFRDBase " +
                    "WHERE frd.IDNivel=@nivelID");
				da.Fill(currentDataSet, "SFRDUFDescricaoFisica");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDDatasProducao"],
                    "INNER JOIN FRDBase frd ON frd.ID = SFRDDatasProducao.IDFRDBase " +
                    "WHERE frd.IDNivel=@nivelID");
                da.Fill(currentDataSet, "SFRDDatasProducao");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TipoAcondicionamento"]);
                da.Fill(currentDataSet, "TipoAcondicionamento");
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TipoMedida"]);
                da.Fill(currentDataSet, "TipoMedida");
			}
		}

        public override decimal LoadDescricaoFisicaAndGetSomatorioLargura(DataSet currentDataSet, long[] ufIDs, IDbConnection conn)
        {
            GisaDataSetHelperRule.ImportIDs(ufIDs, conn);

            decimal total = 0;
            var cmd = @"
SELECT COUNT(df.MedidaLargura) 
FROM FRDBase frd 
    INNER JOIN #temp ON #temp.ID = frd.IDNivel
    LEFT JOIN SFRDUFDescricaoFisica df ON df.IDFRDBase = frd.ID AND df.isDeleted = @isDeleted
WHERE frd.isDeleted = @isDeleted";
            using (var command = new SqlCommand(cmd, (SqlConnection)conn))
            using (var da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@isDeleted", 0);

                total = System.Convert.ToDecimal(command.ExecuteScalar());

                // carregar informação dos niveis documentais associados às ufs
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "INNER JOIN FRDBase ON FRDBase.IDNivel = Nivel.ID " +
                    "INNER JOIN SFRDUnidadeFisica ON SFRDUnidadeFisica.IDFRDBase = FRDBase.ID " +
                    "INNER JOIN #temp ON #temp.ID = SFRDUnidadeFisica.IDNivel");
                da.Fill(currentDataSet, "Nivel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                    "INNER JOIN SFRDUnidadeFisica ON SFRDUnidadeFisica.IDFRDBase = FRDBase.ID " +
                    "INNER JOIN #temp ON #temp.ID = SFRDUnidadeFisica.IDNivel");
                da.Fill(currentDataSet, "FRDBase");

                // carregar informação das ufs
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "INNER JOIN #temp ON #temp.ID = Nivel.ID");
                da.Fill(currentDataSet, "Nivel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDUnidadeFisica"],
                    "INNER JOIN #temp ON #temp.ID = SFRDUnidadeFisica.IDNivel");
                da.Fill(currentDataSet, "SFRDUnidadeFisica");
            }

            return total;
        }

        public override string LoadUFAutosAssociados(long nivelID, IDbConnection conn)
        {
            string cmd = string.Format(@"
                SELECT AutosEliminacao = LEFT(o1.list, LEN(o1.list)-1)
                FROM Nivel n
                    INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = 0 
                    CROSS APPLY ( 
                        SELECT DISTINCT CONVERT(VARCHAR(200), autos.Designacao) + '; ' AS [text()]
                        FROM (
                            SELECT ae.Designacao
                            FROM SFRDUFAutoEliminacao ufae
                                INNER JOIN AutoEliminacao ae ON ae.ID = ufae.IDAutoEliminacao AND ae.isDeleted = 0
                            WHERE ufae.isDeleted = 0 AND ufae.IDFRDBase = frd.ID
                            UNION
                            SELECT ae.Designacao
                            FROM SFRDUnidadeFisica sfrduf
                                INNER JOIN FRDBase frdNvlDoc ON frdNvlDoc.ID = sfrduf.IDFRDBase AND frdNvlDoc.isDeleted = 0
	                            INNER JOIN SFRDAvaliacao av ON av.IDFRDBase = frdNvlDoc.ID AND av.isDeleted = 0
	                            INNER JOIN AutoEliminacao ae ON ae.ID = av.IDAutoEliminacao AND ae.isDeleted = 0
                            WHERE sfrduf.isDeleted = 0 AND sfrduf.IDNivel = frd.IDNivel
                        ) autos
                        GROUP BY autos.Designacao
                        FOR XML PATH('')
                    ) o1 (list) 
                WHERE n.ID = {0} AND n.isDeleted = 0", nivelID);
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = cmd;
            return command.ExecuteScalar().ToString();
        }

		#endregion

		public override ArrayList GetEntidadeDetentoraForNivel(long nivelID, IDbConnection conn){
			SqlCommand command = new SqlCommand("sp_getEntidadeDetentoraForNivel", (SqlConnection) conn);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@nivelID", SqlDbType.BigInt);
			command.Parameters[0].Value = nivelID;
			
			ArrayList result = new ArrayList();
			SqlDataReader reader = null;
			try {
				reader = command.ExecuteReader();
				while (reader.Read()){
					result.Add(reader.GetInt64(0));
				}
			} catch (Exception ex) {
				Trace.WriteLine(ex);
				throw;
			}finally{
				if (reader != null){
					reader.Close();
				}
			}
			return result;
		}
	}
}
