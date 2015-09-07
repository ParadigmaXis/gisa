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
	public sealed class SqlClientControloAutRule: ControloAutRule
	{
		#region " ControloAut "
		public override void LoadFormaAutorizada(long caRowID, DataSet ds, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@caRowID", caRowID);

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(ds.Tables["Dicionario"], 
                    "INNER JOIN ControloAutDicionario cad ON cad.IDDicionario = Dicionario.ID WHERE cad.IDControloAut = @caRowID");
				da.Fill(ds, "Dicionario");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(ds.Tables["ControloAutDicionario"], 
                    "WHERE IDControloAut = @caRowID");
				da.Fill(ds, "ControloAutDicionario");
			}
		}

		//Verificar se uma linha da tabela ControloAutRel ou RelacaoHierarquica existe na base de dados
		public override int ExistsRel(long ID, long IDUpper, long IDTipoRel, bool isCarRow, IDbTransaction tran)
		{
			int count;
            using (SqlCommand command = new SqlCommand("", (SqlConnection)tran.Connection, (SqlTransaction)tran))
            {
                // as duas entidades produtoras existem na base de dados
                if (isCarRow)
                {
                    command.CommandText = string.Format("SELECT COUNT(ID) FROM ControloAut WITH (UPDLOCK) WHERE (ID = {0} OR ID = {1}) AND isDeleted = 0", ID, IDUpper);
                    count = System.Convert.ToInt32(command.ExecuteScalar());

                    if (count == 2)
                    {
                        string query = string.Format(
                            "SELECT COUNT(*) " +
                            "FROM ControloAutRel WITH (UPDLOCK) " +
                            "WHERE ((IDControloAut={0} AND IDControloAutAlias={1}) " +
                            "OR (IDControloAut={1} AND IDControloAutAlias={0})) " +
                            "AND IDTipoRel = {2} " +
                            "AND isDeleted = 0", ID, IDUpper, IDTipoRel);
                        command.CommandText = query;
                        count = System.Convert.ToInt32(command.ExecuteScalar());

                        if (count > 0)
                            return 1;
                        else
                            return 0;
                    }
                    else
                    {
                        // pelo menos uma das entidades produtoras está marcada como apagaa ou não existe na base de dados
                        return 3;
                    }
                }
                else
                {
                    command.CommandText = string.Format("SELECT COUNT(ID) FROM Nivel WITH (UPDLOCK) WHERE (ID = {0} OR ID = {1}) AND isDeleted = 0", ID, IDUpper);
                    count = System.Convert.ToInt32(command.ExecuteScalar());

                    if (count == 2)
                    {
                        string query = string.Format(
                            "SELECT COUNT(*) " +
                            "FROM RelacaoHierarquica WITH (UPDLOCK) " +
                            "WHERE ID={0} AND IDUpper={1} " +
                            "AND IDTipoNivelRelacionado = {2} AND isDeleted = 0", ID, IDUpper, IDTipoRel);
                        command.CommandText = query;
                        count = System.Convert.ToInt32(command.ExecuteScalar());

                        if (count > 0)
                            return 1;
                        else
                        {
                            command.CommandText = "sp_canCreateRH";
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = ID;
                            command.Parameters.Add("@IdUpper", SqlDbType.BigInt).Value = IDUpper;
                            int canCreate = System.Convert.ToInt32(command.ExecuteScalar());

                            if (canCreate == 0)
                                return 2;
                            else
                                return 0;
                        }
                    }
                    else
                    {
                        // pelo menos uma das entidades produtoras está marcada como apagaa ou não existe na base de dados
                        return 3;
                    }
                }
            }
		}

        public override void LoadControloAutFromNivel(DataSet currentDataSet, long IDNivel, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@IDNivel", IDNivel);
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"], 
                    "INNER JOIN NivelControloAut nca ON nca.IDControloAut = ControloAut.ID " +
                    "WHERE nca.ID=@IDNivel");
                da.Fill(currentDataSet, "ControloAut");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDatasExistencia"],
                    "INNER JOIN ControloAut ca ON ca.ID = ControloAutDatasExistencia.IDControloAut " +
                    "INNER JOIN NivelControloAut nca ON nca.IDControloAut = ca.ID " +
                    "WHERE nca.ID=@IDNivel");
                da.Fill(currentDataSet, "ControloAutDatasExistencia");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelControloAut"],
                    "WHERE NivelControloAut.ID=@IDNivel");
                da.Fill(currentDataSet, "NivelControloAut");
            }
        }

        public override void LoadNivelFromControloAut(DataSet currentDataSet, long IDNivelCA, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@IDNivelCA", IDNivelCA);
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "WHERE Nivel.ID = @IDNivelCA");
                da.Fill(currentDataSet, "Nivel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelControloAut"],
                    "WHERE NivelControloAut.ID = @IDNivelCA");
                da.Fill(currentDataSet, "NivelControloAut");
            }
        }
		#endregion

		#region " PanelCAControlo "
		public override void LoadDataPanelCAControlo(System.Data.DataSet currentDataSet, long CurrentControloAutID, System.Data.IDbConnection conn)
		{
			string QueryFilter = "IDControloAut=" + CurrentControloAutID.ToString();
			string WhereQueryFilter = "WHERE " + QueryFilter;

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CurrentControloAutID", CurrentControloAutID);
                command.Parameters.AddWithValue("@IsAuthority", 1);
				da.SelectCommand.CommandText =
					SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],					
					"INNER JOIN (" +
					"SELECT IDControloAut, IDControloAutAlias " +
					"FROM ControloAutRel " +
                    "WHERE IDControloAut = @CurrentControloAutID " +
                    "OR IDControloAutAlias = @CurrentControloAutID" +
					") cars on cars.IDControloAut = ID");
				da.Fill(currentDataSet, "ControloAut");

				da.SelectCommand.CommandText =
					SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],				
					"INNER JOIN (" +
					"SELECT IDControloAut, IDControloAutAlias " +
					"FROM ControloAutRel " +
                    "WHERE IDControloAut = @CurrentControloAutID " +
                    "OR IDControloAutAlias = @CurrentControloAutID" +
					") cars on cars.IDControloAutAlias = ID");
				da.Fill(currentDataSet, "ControloAut");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDataDeDescricao"],
                    "WHERE IDControloAut = @CurrentControloAutID ");
				da.Fill(currentDataSet, "ControloAutDataDeDescricao");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Trustee"]) +
					"ORDER BY CatCode, Name";
				da.Fill(currentDataSet, "Trustee");
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeUser"], 
                    "WHERE IsAuthority=@IsAuthority");
				da.Fill(currentDataSet, "TrusteeUser");
			}			
		}

        public override List<string> GetNiveisDocAssociados(long CurrentControloAutID, IDbConnection conn)
        {
            List<string> Ids = new List<string>();
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = string.Format(
                "CREATE TABLE #NiveisTemp (IDNivel BIGINT); " +
                "INSERT INTO #NiveisTemp VALUES ({0})", CurrentControloAutID.ToString());
            command.ExecuteNonQuery();

            command.CommandText = "sp_getCANiveisDocAssociados";
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Ids.Add(reader.GetValue(0).ToString());
            }
            reader.Close();

            command.CommandText = "DROP TABLE #NiveisTemp";
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();

            return Ids;
        }
		#endregion

		#region " PanelCADescricao "
		public override void LoadDataPanelCADescricao(System.Data.DataSet currentDataSet, long CurrentControloAutID, System.Data.IDbConnection conn) 
		{
			string caWhereQueryFilter = "WHERE ID = @CurrentControloAutID";
			string deWhereQueryFilter = "WHERE IDControloAut = @CurrentControloAutID";

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CurrentControloAutID", CurrentControloAutID);

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"], caWhereQueryFilter);
				da.Fill(currentDataSet, "ControloAut");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDatasExistencia"], deWhereQueryFilter);
				da.Fill(currentDataSet, "ControloAutDatasExistencia");
			}			
		}
		#endregion

		#region " PanelCAIdentificacao "

		public override void FillControloAutEntidadeProdutora(System.Data.DataSet currentDataSet, long CurrentControloAutID, System.Data.IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CurrentControloAutID", CurrentControloAutID);

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutEntidadeProdutora"],
                    "WHERE IDControloAut=@CurrentControloAutID");
				da.Fill(currentDataSet, "ControloAutEntidadeProdutora");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
					"INNER JOIN NivelControloAut nca ON nca.ID = Nivel.ID " +
                    "WHERE nca.IDControloAut=@CurrentControloAutID");
				da.Fill(currentDataSet, "Nivel");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelControloAut"],
                    "WHERE IDControloAut=@CurrentControloAutID");
				da.Fill(currentDataSet, "NivelControloAut");				
			}
		}

		public override void LoadThesaurus(System.Data.DataSet currentDataSet, long CurrentControloAutID, System.Data.IDbConnection conn)
		{
			string relatedQuery = "SELECT DISTINCT CA.ID FROM ControloAut CA" + 
									" INNER JOIN ControloAutRel ON" + 
									" (CA.ID=ControloAutRel.IDControloAut" +
                                    "  AND ControloAutRel.IDControloAutAlias=@CurrentControloAutID) " + 
									"UNION " + 
									"SELECT DISTINCT CA.ID FROM ControloAut CA" + 
									" INNER JOIN ControloAutRel ON" + 
									" (CA.ID=ControloAutRel.IDControloAutAlias" +
                                    "  AND ControloAutRel.IDControloAut=@CurrentControloAutID)";

			long startTicks = DateTime.Now.Ticks;

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CurrentControloAutID", CurrentControloAutID);

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"], "INNER JOIN (" + relatedQuery + ") R ON R.ID=ControloAut.ID");
				da.Fill(currentDataSet, "ControloAut");
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutRel"], "WHERE IDControloAut=@CurrentControloAutID OR IDControloAutAlias=@CurrentControloAutID");
				da.Fill(currentDataSet, "ControloAutRel");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"], "inner join ControloAutDicionario on ControloAutDicionario.IDDicionario=Dicionario.ID" + " inner join (" + relatedQuery + ") R on R.ID=ControloAutDicionario.IDControloAut");
				da.Fill(currentDataSet, "Dicionario");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"], "inner join (" + relatedQuery + ") R on R.ID=ControloAutDicionario.IDControloAut");
				da.Fill(currentDataSet, "ControloAutDicionario");
			}
			System.Diagnostics.Debug.WriteLine("Loaded thesaurus in " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());
		}

		public override List<long> LoadTermos(DataSet currentDataSet, long CurrentControloAutID, IDbConnection conn)
		{
            var res = new List<long>();
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                string Query = "CREATE TABLE #ControloAutRelTopo( " + "	IDControloAut BIGINT, " +
                    "	IDControloAutAliasChild BIGINT, " + "	IDControloAutAlias BIGINT, " +
                    "	Distance INT, " +
                    "	PRIMARY KEY NONCLUSTERED ( " +
                    "		IDControloAut, " +
                    "		IDControloAutAliasChild,  " +
                    "		IDControloAutAlias " +
                    "	) " +
                    "); "+
                    "CREATE TABLE #ControloAutTarget (IDControloAutAlias BIGINT);";
                command.CommandText = Query;
                command.ExecuteNonQuery();

                Query = "INSERT INTO #ControloAutRelTopo VALUES (@CurrentControloAutID, @CurrentControloAutID, @CurrentControloAutID, @value);" +
                    "WHILE (@@ROWCOUNT > 0) " +
                    "	INSERT INTO #ControloAutRelTopo  " +
                    "		SELECT DISTINCT TT.IDControloAut, car.IDControloAut, car.IDControloAutAlias, TT.Distance+1 " +
                    "		FROM ControloAutRel car  " +
                    "			INNER JOIN #ControloAutRelTopo TT ON car.IDControloAut=TT.IDControloAutAlias  " +
                    "			LEFT JOIN #ControloAutRelTopo TT2 ON car.IDControloAut=TT2.IDControloAutAliasChild AND " +
                    "				car.IDControloAutAlias=TT2.IDControloAutAlias " +
                    "		WHERE TT2.IDControloAut IS NULL AND " +
                    "			car.IDTipoRel=@IDTipoRel AND " +
                    "           car.isDeleted=@isDeleted;" +
                    "INSERT INTO #ControloAutTarget SELECT DISTINCT TT.IDControloAutAlias " +
                    "FROM #ControloAutRelTopo TT  " +
                    "	LEFT JOIN #ControloAutRelTopo TT2 ON TT.IDControloAutAlias=TT2.IDControloAutAliasChild  " +
                    "WHERE TT2.IDControloAut IS NULL; " +
                    "DROP TABLE #ControloAutRelTopo;";
                command.CommandText = Query;
                command.Parameters.AddWithValue("@CurrentControloAutID", CurrentControloAutID);
                command.Parameters.AddWithValue("@value", 0);
                command.Parameters.AddWithValue("@IDTipoRel", 1);
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(command);
                {
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"], "INNER JOIN #ControloAutTarget CAT on ControloAut.ID=CAT.IDControloAutAlias");
                    da.Fill(currentDataSet, "ControloAut");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"], "INNER JOIN ControloAutDicionario on Dicionario.ID=ControloAutDicionario.IDDicionario INNER JOIN #ControloAutTarget CAT on ControloAutDicionario.IDControloAut=CAT.IDControloAutAlias");
                    da.Fill(currentDataSet, "Dicionario");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"], "INNER JOIN #ControloAutTarget CAT on ControloAutDicionario.IDControloAut=CAT.IDControloAutAlias");
                    da.Fill(currentDataSet, "ControloAutDicionario");
                }

                command.CommandText =
                    "SELECT cat.IDControloAutAlias FROM #ControloAutTarget cat " +
                        "INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = cat.IDControloAutAlias " +
                            "AND cad.IDTipoControloAutForma = @IDTipoControloAutForma AND cad.isDeleted = @isDeleted " +
                        "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario " +
                    "ORDER BY d.Termo ASC; " +
                    "DROP TABLE #ControloAutTarget;";
                command.Parameters.AddWithValue("@IDTipoControloAutForma", 1);

                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                    res.Add(dr.GetInt64(0));
                dr.Close();
            }

            return res;
		}

		public override void SetUpIsReachable(DataSet currentDataSet, long CurrentControloAutID, IDbTransaction tran)
		{
			SqlCommand command = new SqlCommand(string.Empty, (SqlConnection) tran.Connection, (SqlTransaction) tran);
			command.CommandText = "create table #TempControloAutRel (IDControloAutRoot numeric, IDControloAut numeric, IDControloAutAlias numeric, Distance numeric)";			
			command.ExecuteNonQuery();
			
			System.Text.StringBuilder inserts = new System.Text.StringBuilder();
			foreach (DataRow dr in currentDataSet.Tables["ControloAutRel"].Select(string.Format("(IDControloAut={0} OR IDControloAutAlias={0}) AND IDTipoRel = 1 ", CurrentControloAutID)))
			{
				if (dr.RowState != DataRowState.Added) 
					inserts.AppendFormat("insert into #TempControloAutRel values ({0}, {0}, {1}, {2});{3}", dr["IDControloAut"], dr["IDControloAutAlias"], (((long) dr["IDControloAut"]) == CurrentControloAutID ? 1 : -1), Environment.NewLine);
			}
			if (inserts.Length > 0) 
			{
				command.CommandText = inserts.ToString();
				command.ExecuteNonQuery();				
			}
		}

		public override void ComputeIsReachable(IDbTransaction tran)
		{
			// expandir termos especificos
			string cmd = 
				"declare @n numeric;" + 
				"set @n=1;" + 
				"while (@n>0)" + 
				"begin " + 
				"   insert into #TempControloAutRel" + 
				"   select TCAR.IDControloAutRoot, CAR.IDControloAut, CAR.IDControloAutAlias, TCAR.Distance + 1" + 
				"   from ControloAutRel CAR" + 
				"   inner join #TempControloAutRel TCAR on CAR.IDControloAut=TCAR.IDControloAutAlias and CAR.IDTipoRel=1" + 
				"   left outer join #TempControloAutRel OLD on OLD.IDControloAutRoot=TCAR.IDControloAutRoot and OLD.IDControloAut=CAR.IDControloAut and OLD.IDControloAutAlias=CAR.IDControloAutAlias" + 
				"   where OLD.Distance is null and TCAR.Distance = (select max(Distance) from #TempControloAutRel) and CAR.isDeleted=0;" + 
				"   set @n=@@rowcount;" + 
				"end;";

			Trace.WriteLine(cmd);

			SqlCommand command = new SqlCommand(cmd, (SqlConnection) tran.Connection, (SqlTransaction) tran);
			command.ExecuteNonQuery();
			
			// expandir termos genericos
			cmd = 
				"declare @n numeric;" + 
				"set @n=1;" + 
				"while (@n>0)" + 
				"begin " + 
				"   insert into #TempControloAutRel" + 
				"   select TCAR.IDControloAutRoot, CAR.IDControloAut, CAR.IDControloAutAlias, TCAR.Distance - 1" + 
				"   from ControloAutRel CAR" + "   inner join #TempControloAutRel TCAR on CAR.IDControloAutAlias=TCAR.IDControloAut and CAR.IDTipoRel=1" + 
				"   left outer join #TempControloAutRel OLD on OLD.IDControloAutRoot=TCAR.IDControloAutRoot and OLD.IDControloAut=CAR.IDControloAut and OLD.IDControloAutAlias=CAR.IDControloAutAlias" + 
				"   where OLD.Distance is null  and TCAR.Distance = (select min(Distance) from #TempControloAutRel);" + 
				"   set @n=@@rowcount;" + 
				"end;";

			Trace.WriteLine(cmd);
			command.CommandText = cmd;
			command.ExecuteNonQuery();			
		}

		public override bool IsReachable(long ID, IDbTransaction tran)
		{
			string cmd = string.Format("select count(*) from (select distinct IDControloAutRoot, Distance from #TempControloAutRel where IDControloAut={0} or IDControloAutAlias={0}) M", ID);
			Trace.WriteLine(cmd);
			SqlCommand command = new SqlCommand(cmd, (SqlConnection) tran.Connection, (SqlTransaction) tran); 
			long count = System.Convert.ToInt64(command.ExecuteScalar());
			return count != 0;
		}

		public override void TearDownIsReachable(IDbTransaction tran)
		{
			string cmd = "drop table #TempControloAutRel;";
			Trace.WriteLine(cmd);
			SqlCommand command = new SqlCommand(cmd, (SqlConnection) tran.Connection, (SqlTransaction) tran);
			command.ExecuteNonQuery();
		}

		public override bool isCarInDataBase(DataRow carRow, IDbTransaction tran)
		{
			SqlCommand command = new SqlCommand(string.Empty, (SqlConnection) tran.Connection, (SqlTransaction) tran);			

			command.CommandText = string.Format("SELECT COUNT(*) FROM ControloAutRel WITH (UPDLOCK) WHERE ((IDControloAut = {0} AND IDControloAutAlias = {1}) OR (IDControloAut = {1} AND IDControloAutAlias = {0})) AND IDTipoRel = {2} AND isDeleted = 0", carRow[0], carRow[1], carRow[2]);
			long count = System.Convert.ToInt64(command.ExecuteScalar());
			if (count > 0)
				// a relacção existe ou existe uma mas com a direcção inversa
				return true;

			command.CommandText = string.Format("SELECT COUNT(*) FROM ControloAutRel WITH (UPDLOCK)  WHERE ((IDControloAut = {0} AND IDControloAutAlias = {1}) OR (IDControloAut = {1} AND IDControloAutAlias = {0})) AND IDTipoRel = {2} AND isDeleted = 1", carRow[0], carRow[1], carRow[2]);
			count = System.Convert.ToInt64(command.ExecuteScalar());
			if (count == 2)
				// a relacção e a sua equivalente mas com a direcção inversa existem na base de 
				// dados mas estão marcadas como apagadas; pode-se reutilizar a relacção pretendida
				// caso os controlos de autoridade envolvidos também existam na base de dados
				return false;

			command.CommandText = string.Format("SELECT COUNT(*) FROM ControloAutRel WITH (UPDLOCK) WHERE IDControloAut = {0} AND IDControloAutAlias = {1} AND IDTipoRel = {2} AND isDeleted = 0", carRow[0], carRow[1], carRow[2]);
			count = System.Convert.ToInt64(command.ExecuteScalar());
			if (count > 0)
				// a relacção que se pretende criar já existe na base de dados
				return true;
			else
			{
				command.CommandText = string.Format("SELECT COUNT(*) FROM ControloAutRel WITH (UPDLOCK) WHERE IDControloAut = {1} AND IDControloAutAlias = {0} AND IDTipoRel = {2} AND isDeleted = 0", carRow[0], carRow[1], carRow[2]);
				count = System.Convert.ToInt64(command.ExecuteScalar());
				if (count > 0)
					// já existe na base de dados uma relacção equivalente àquela que se pretende 
					// adicionar mas com direcção inversa
					return true;
				else									
					return false;
			}
		}

		public override bool isControloAutInDataBase (long [] CaIDs, IDbTransaction tran)
		{
			SqlCommand command = new SqlCommand(string.Empty, (SqlConnection) tran.Connection, (SqlTransaction) tran);
			command.CommandText = string.Format(
				"SELECT COUNT(*) " +
				"FROM ControloAut " +
				"WITH (UPDLOCK) " +
				"WHERE (ID = {0} AND isDeleted = 0) OR (ID = {1} AND isDeleted = 0)", CaIDs[0], CaIDs[1]);
			long count = System.Convert.ToInt64(command.ExecuteScalar());
			if (count == 2)
				return true;
			else
				return false;
		}

		public override ArrayList GetTermos (long CurrentControloAutID , System.Data.IDbConnection conn) 
		{
			ArrayList rez = new ArrayList();
			ArrayList tup = new ArrayList();
			string relatedQuery = 
				"SELECT DISTINCT car.IDControloAut, car.IDControloAutAlias, d.Termo " +
				"FROM ControloAutRel car " +
                    "INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = car.IDControloAut AND cad.isDeleted = 0 " +
                    "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted = 0 " +
				"WHERE car.IDControloAutAlias = {0} " +
					"AND cad.IDTipoControloAutForma = 1 " +
					"AND car.isDeleted = 0 " +
				"UNION " +
				"SELECT DISTINCT car.IDControloAut, car.IDControloAutAlias, d.Termo " +
				"FROM ControloAutRel car " +
                    "INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = car.IDControloAutAlias AND cad.isDeleted = 0 " +
                    "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted = 0 " +
				"WHERE car.IDControloAut = {0} " +
					"AND cad.IDTipoControloAutForma = 1 " +
					"AND car.isDeleted = 0 " +
				"ORDER BY d.Termo ASC";

			relatedQuery = string.Format(relatedQuery, CurrentControloAutID);
			SqlCommand command = new SqlCommand(relatedQuery, (SqlConnection) conn);
			command.CommandType = CommandType.Text;
			SqlDataReader dr = command.ExecuteReader();
			while ( dr.Read()) 
			{
				tup = new ArrayList();
				tup.Add(Convert.ToInt64(dr.GetValue(0)));
				tup.Add(Convert.ToInt64(dr.GetValue(1)));
				rez.Add(tup);
			}
			dr.Close();

			return rez;
		}
		#endregion

		#region " PanelCARelacoes "		
		public override void LoadControloAutRel(DataSet currentDataSet, long IDControloAut, long IDControloAutAlias, long IDTipoRel, IDbConnection conn)
		{
			LoadControloAut(currentDataSet, IDControloAutAlias, conn);
			LoadFormaAutorizada (IDControloAutAlias, currentDataSet, conn);

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@IDControloAut", IDControloAut);
                command.Parameters.AddWithValue("@IDControloAut", IDControloAutAlias);
                command.Parameters.AddWithValue("@IDTipoRel", IDTipoRel);
				
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutRel"],
                    " WHERE ((IDControloAut = @IDControloAut AND IDControloAutAlias = @IDControloAut) OR (IDControloAut = @IDControloAut AND IDControloAutAlias = @IDControloAut)) AND IDTipoRel = @IDTipoRel");
				da.Fill(currentDataSet, "ControloAutRel");
			}
		}

		public override void LoadRelacaoHierarquica(DataSet currentDataSet, long ID, long IDUpper, long IDTipoNivelRelacionado, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@IDUpper", IDUpper);
                command.Parameters.AddWithValue("@IDTipoNivelRelacionado", IDTipoNivelRelacionado);

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                    " WHERE ((ID = @ID AND IDUpper = @IDUpper) OR (ID = @IDUpper AND IDUpper = @ID)) AND IDTipoNivelRelacionado = @IDTipoNivelRelacionado");
				da.Fill(currentDataSet, "RelacaoHierarquica");
			}
		}
		#endregion

		#region " FRDCA "
		public override void LoadControloAut(DataSet currentDataSet, long controloAutID, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@controloAutID", controloAutID);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAut"],
                    "WHERE ID=@controloAutID");
				da.Fill(currentDataSet, "ControloAut");
			}
		}


		public override void LoadControloAutData(DataSet currentDataSet, long controloAutID, IDbConnection conn)
		{
            string WhereQueryFilter = "WHERE IDControloAut=@controloAutID ";

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@controloAutID", controloAutID);

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutEntidadeProdutora"], WhereQueryFilter);
				da.Fill(currentDataSet, "ControloAutEntidadeProdutora");

				// ToDo: Limitar o load de informação das tabelas Trustee e TrusteeUser ao estritamente necessario
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Trustee"]) +
					" ORDER BY CatCode, Name";
				da.Fill(currentDataSet, "Trustee");

				da.SelectCommand.CommandText =
					SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeUser"]);
				da.Fill(currentDataSet, "TrusteeUser");

				da.SelectCommand.CommandText = 
					SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDataDeDescricao"], 
					WhereQueryFilter);
				da.Fill(currentDataSet, "ControloAutDataDeDescricao");

				da.SelectCommand.CommandText = 
					SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"], 
					"INNER JOIN ControloAutDicionario cad ON cad.IDDicionario = ID " + WhereQueryFilter);
				da.Fill(currentDataSet, "Dicionario");

				da.SelectCommand.CommandText = 
					SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"], 
					WhereQueryFilter);
				da.Fill(currentDataSet, "ControloAutDicionario");
				
				// carregar os controlos de autoridade relacionados com o actual
				da.SelectCommand.CommandText =
					//SqlSyntax.CreateSelectCommand(currentDataSet.Tables["ControloAut"],
                    "SELECT ControloAut.ID, ControloAut.isDeleted, ControloAut.Autorizado, ControloAut.Completo, ControloAut.IDTipoNoticiaAut, ControloAut.IDIso639p2, ControloAut.IDIso15924, ControloAut.ChaveColectividade, ControloAut.Versao, ControloAut.IDTipoTipologia FROM ControloAut " + 
					"INNER JOIN (" +
					"SELECT IDControloAut, IDControloAutAlias " +
					"FROM ControloAutRel " +
                    "WHERE IDControloAut = @controloAutID " +
                    "OR IDControloAutAlias = @controloAutID" +
					") cars on cars.IDControloAut = ID";
				da.Fill(currentDataSet, "ControloAut");

				da.SelectCommand.CommandText =
					//SqlSyntax.CreateSelectCommand(currentDataSet.Tables["ControloAut"],
                    "SELECT ControloAut.ID, ControloAut.isDeleted, ControloAut.Autorizado, ControloAut.Completo, ControloAut.IDTipoNoticiaAut, ControloAut.IDIso639p2, ControloAut.IDIso15924, ControloAut.ChaveColectividade, ControloAut.Versao, ControloAut.IDTipoTipologia FROM ControloAut " + 
					"INNER JOIN (" +
					"SELECT IDControloAut, IDControloAutAlias " +
					"FROM ControloAutRel " +
                    "WHERE IDControloAut = @controloAutID " +
                    "OR IDControloAutAlias = @controloAutID" +
					") cars on cars.IDControloAutAlias = ID";
				da.Fill(currentDataSet, "ControloAut");
						
				da.SelectCommand.CommandText = 
					SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutRel"],
                    string.Format("WHERE ControloAutRel.IDControloAut=@controloAutID OR ControloAutRel.IDControloAutAlias=@controloAutID", 
					controloAutID));
				da.Fill(currentDataSet, "ControloAutRel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelControloAut"],
                    "WHERE IDControloAut=" + controloAutID);
                da.Fill(currentDataSet, "NivelControloAut");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "INNER JOIN NivelControloAut nca ON nca.ID = Nivel.ID " +
                    "WHERE IDControloAut=" + controloAutID);
                da.Fill(currentDataSet, "Nivel");
			}
		}

		#endregion

		#region " MasterPanelControloAut "

		public override void LoadDicionarioAndControloAutDicionario(DataSet currentDataSet, long caRowID, IDbConnection conn)
		{
			string QueryFilterNewDicionario = 
				"INNER JOIN ControloAutDicionario cad ON Dicionario.ID = cad.IDDicionario " +
                    "WHERE cad.IDControloAut = @caRowID AND cad.IDTipoControloAutForma = @IDTipoControloAutForma";
			
			string QueryFilterNewCad =
                "IDControloAut = @caRowID AND IDTipoControloAutForma = @IDTipoControloAutForma";

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@caRowID", caRowID);
                command.Parameters.AddWithValue("@IDTipoControloAutForma", 1);

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"], QueryFilterNewDicionario);
				da.Fill(currentDataSet, "Dicionario");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"], "WHERE " + QueryFilterNewCad);
				da.Fill(currentDataSet, "ControloAutDicionario");
			}
		}

		public override bool isCADeleted(string IDControloAut, IDbTransaction tran)
		{
			long usageCount;
			bool isDeleted = false;
			SqlCommand command = new SqlCommand("", (SqlConnection) tran.Connection, (SqlTransaction) tran);
			command.CommandText = string.Format("SELECT COUNT(*) FROM ControloAut WITH (UPDLOCK) " + 
                            "WHERE ID = {0} AND isDeleted = 1", IDControloAut);

			usageCount = System.Convert.ToInt64(command.ExecuteScalar());

			if (usageCount == 0) 
			{
				command.CommandText = string.Format("SELECT COUNT(*) FROM ControloAut WITH (UPDLOCK) " + 
                                            "WHERE ID = {0}", IDControloAut);

				usageCount = System.Convert.ToInt64(command.ExecuteScalar());

				if (usageCount == 0)
					isDeleted = true;
			}
			else
				isDeleted = true;

			return isDeleted;
		}

		public override bool isCADDeleted(long IDControloAut, long IDDicionario, long IDTipoControloAutForma, IDbTransaction tran)
		{
			long usageCount;
			bool isDeleted = false;
			string whereQuery = "WHERE IDControloAut = {0} AND IDDicionario = {1} AND IDTipoControloAutForma = {2}";

			SqlCommand command = new SqlCommand(string.Empty, (SqlConnection) tran.Connection, (SqlTransaction) tran);
			command.CommandText = string.Format("SELECT COUNT(*) FROM ControloAutDicionario WITH (UPDLOCK) " + 
				whereQuery + " AND isDeleted = 1", IDControloAut, IDDicionario, IDTipoControloAutForma);

			usageCount = System.Convert.ToInt64(command.ExecuteScalar());

			if (usageCount == 0) 
			{
				command.CommandText = string.Format("SELECT COUNT(*) FROM ControloAutDicionario WITH (UPDLOCK) " + 
					whereQuery, IDControloAut, IDDicionario, IDTipoControloAutForma);

				usageCount = System.Convert.ToInt64(command.ExecuteScalar());

				if (usageCount == 0)
					isDeleted = true;
			}
			else
				isDeleted = true;

			return isDeleted;
		}
		#endregion

		#region " FormCreateControloAut "
		public override bool ExistsControloAutDicionario(long IDDicionario, long IDTipoControloAutForma, long IDTipoNoticiaAut, IDbConnection conn)
		{
			SqlCommand command = new SqlCommand("sp_ExistsControloAutDicionario", (SqlConnection) conn);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@IDDicionario", SqlDbType.BigInt);
			command.Parameters[0].Value = IDDicionario;
			command.Parameters.Add("@IDTipoControloAutForma", SqlDbType.BigInt);
			command.Parameters[1].Value = IDTipoControloAutForma;
			command.Parameters.Add("@IDTipoNoticiaAut", SqlDbType.BigInt);
			command.Parameters[2].Value = IDTipoNoticiaAut;
			SqlDataReader reader = command.ExecuteReader();
			reader.Read();
			bool existsOrigCad = System.Convert.ToBoolean(reader.GetValue(0));
			reader.Close();
			return existsOrigCad;
		}

		#endregion

		#region " ControloAutList "
		public override void CalculateOrderedItems(string autorizado, string FiltroTermoLike, long[] FiltroNoticiaAut, IDbConnection conn)
		{
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                // a criação da tabela temporária deve ser feita antes de qualquer query que contenha parâmetros caso contrário a tabela temporária não é "encontrada" (scopes diferentes...)
                command.CommandText = "CREATE TABLE #OrderedItems (seq_id INT IDENTITY(1,1) NOT NULL, ID BIGINT NOT NULL, Termo NVARCHAR(444) NOT NULL,  IDControloAut BIGINT NOT NULL, IDTipoControloAutForma BigInt NOT NULL ); ";
                command.ExecuteNonQuery();

                StringBuilder innerJoinQuery = new StringBuilder(
                    "INNER JOIN ControloAutDicionario ON Dicionario.ID=ControloAutDicionario.IDDicionario " +
                    "INNER JOIN ControloAut ON ControloAutDicionario.IDControloAut=ControloAut.ID ");

                StringBuilder whereQuery = new StringBuilder(
                    "WHERE Dicionario.isDeleted=@isDeleted " +
                    "AND ControloAutDicionario.isDeleted=@isDeleted " +
                    "AND ControloAut.isDeleted=@isDeleted ");

                string[] filterParts = new string[FiltroNoticiaAut.Length];
                if (FiltroNoticiaAut.Length > 0)
                {
                    for (int i = 0; i < FiltroNoticiaAut.Length; i++)
                    {
                        filterParts[i] = "IDTipoNoticiaAut=@param" + i.ToString();
                        command.Parameters.AddWithValue("@param" + i.ToString(), FiltroNoticiaAut[i]);
                    }
                    whereQuery.Append("AND (" + string.Join(" OR ", filterParts) + ")");
                }

                if (autorizado.Length > 0)
                {
                    command.Parameters.AddWithValue("@Autorizado", autorizado);
                    whereQuery.Append(" AND ControloAut.Autorizado = @Autorizado ");
                }

                if (FiltroTermoLike.Length > 0)
                {
                    command.Parameters.AddWithValue("@Termo", FiltroTermoLike);
                    whereQuery.Append(" AND " + PesquisaRule.Current.buildLikeStatement("Dicionario.Termo", "@Termo"));
                }

                command.Parameters.AddWithValue("@isDeleted", 0);
                    
                // criar uma tabela com todos os IDs dos CAs ordenados a serem apresentados
                command.CommandText = string.Format(
                    "INSERT INTO #OrderedItems (ID, Termo, IDControloAut, IDTipoControloAutForma) " +
                    "SELECT Dicionario.ID, Dicionario.Termo, " +
                    "ControloAutDicionario.IDControloAut, ControloAutDicionario.IDTipoControloAutForma " +
                    "FROM Dicionario {0} {1} " +
                    "ORDER BY Dicionario.Termo", innerJoinQuery.ToString(), whereQuery.ToString());

                command.ExecuteNonQuery();
            }
		}

		public override int CountPages(int itemsPerPage, IDbConnection conn)
		{
            int count = 0;
			SqlDataReader reader;
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.CommandText = "SELECT COUNT(*) FROM #OrderedItems";

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
            }
			if (count % itemsPerPage != 0)
				return System.Convert.ToInt32(count/itemsPerPage) + 1;
			else
				return System.Convert.ToInt32(count/itemsPerPage);
		}

		public override int GetPageForID(long [] cad, int pageLimit, IDbConnection conn)
		{
            int id_seq = 0;
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.Parameters.AddWithValue("@ID", cad[1]);
                command.Parameters.AddWithValue("@IDControloAut", cad[0]);
                command.Parameters.AddWithValue("@IDTipoControloAutForma", cad[2]);
                command.CommandText = "SELECT seq_id FROM #OrderedItems WHERE ID=@ID AND IDControloAut=@IDControloAut AND IDTipoControloAutForma=@IDTipoControloAutForma";

                id_seq = System.Convert.ToInt32(command.ExecuteScalar());
            }

			if (id_seq % pageLimit != 0)
				return id_seq/pageLimit + 1;
			else
				return id_seq/pageLimit;
		}

		public override ArrayList GetItems(DataSet currentDataSet, int pageNr, int itemsPerPage, long[] FiltroNoticiaAut, IDbConnection conn)
		{
            ArrayList rows = new ArrayList();
			SqlDataReader reader;
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #ItemsID (ID BIGINT, IDControloAut BIGINT, IDTipoControloAutForma BIGINT)";
                command.ExecuteNonQuery();

                command.Parameters.AddWithValue("@seq_id1", (pageNr - 1) * itemsPerPage + 1);
                command.Parameters.AddWithValue("@seq_id2", pageNr * itemsPerPage);
                command.CommandText =
                    "INSERT INTO #ItemsID SELECT ID, IDControloAut, IDTipoControloAutForma FROM #OrderedItems WHERE seq_id >= @seq_id1 AND seq_id <= @seq_id2 ORDER BY seq_id";
                command.ExecuteNonQuery();

                string[] filterParts = new string[FiltroNoticiaAut.Length];
                string filter = "";
                if (FiltroNoticiaAut.Length > 0)
                {
                    for (int i = 0; i < FiltroNoticiaAut.Length; i++)
                    {
                        filterParts[i] = "IDTipoNoticiaAut=@param" + i.ToString();
                        command.Parameters.AddWithValue("@param" + i.ToString(), FiltroNoticiaAut[i]);
                    }
                    filter = "(" + string.Join(" OR ", filterParts) + ")";
                }

                string constraintsD = "INNER JOIN (SELECT DISTINCT #ItemsID.ID FROM #ItemsID) termos ON termos.ID = Dicionario.ID";

                string constraintsCAD =
                    "INNER JOIN ControloAut ON ControloAutDicionario.IDControloAut = ControloAut.ID " +
                    "INNER JOIN (SELECT Dicionario.ID " +
                    "FROM Dicionario " + constraintsD + ") Dicionario " +
                    "ON ControloAutDicionario.IDDicionario=Dicionario.ID " +
                    "WHERE " + filter;

                string constraintsCA =
                    "INNER JOIN (" +
                    "SELECT DISTINCT IDControloAut " +
                    "FROM ControloAutDicionario " +
                    constraintsCAD +
                    ") DistinctCAs ON {0}=DistinctCAs.IDControloAut";

                // as proximas strings que compõem as respectivas queries são necessárias para quando 
                // se adicionar noticias de autoridade como termos relacionados na área dos conteúdos
                string constraintsCADAutorizado =
                    "INNER JOIN (" +
                    "SELECT ID FROM ControloAut " + constraintsCA +
                    " ) ca ON ca.ID = ControloAutDicionario.IDControloAut " +
                    "WHERE ControloAutDicionario.IDTipoControloAutForma = @IDTipoControloAutForma ";

                string constraintsDAutorizado =
                    "INNER JOIN (" +
                    "SELECT IDDicionario FROM ControloAutDicionario " + constraintsCADAutorizado +
                    " ) cad ON cad.IDDicionario = Dicionario.ID ";

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    command.Parameters.AddWithValue("@IDTipoControloAutForma", 1);
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"],
                        constraintsD);
                    da.Fill(currentDataSet, "Dicionario");
                    da.SelectCommand.CommandText = //SqlSyntax.CreateSelectCommand(currentDataSet.Tables["ControloAut"], 
                        string.Format("SELECT ControloAut.ID, ControloAut.ChaveColectividade, ControloAut.isDeleted, ControloAut.Autorizado, ControloAut.Completo, ControloAut.IDTipoNoticiaAut, ControloAut.IDIso639p2, ControloAut.IDIso15924, ControloAut.Versao, ControloAut.IDTipoTipologia  FROM ControloAut " + constraintsCA + " WHERE ControloAut.isDeleted = @isDeleted", "ControloAut.ID");
                    da.Fill(currentDataSet, "ControloAut");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"],
                        constraintsCAD);
                    da.Fill(currentDataSet, "ControloAutDicionario");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDatasExistencia"],
                        string.Format(constraintsCA, "ControloAutDatasExistencia.IDControloAut"));
                    da.Fill(currentDataSet, "ControloAutDatasExistencia");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"],
                        string.Format(constraintsDAutorizado, "ControloAut.ID"));
                    da.Fill(currentDataSet, "Dicionario");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"],
                        string.Format(constraintsCADAutorizado, "ControloAut.ID"));
                    da.Fill(currentDataSet, "ControloAutDicionario");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelControloAut"],
                        "INNER JOIN ControloAut ON ControloAut.ID = NivelControloAut.IDControloAut " +
                        string.Format(constraintsCA, "ControloAut.ID"));
                    da.Fill(currentDataSet, "NivelControloAut");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN NivelControloAut ON NivelControloAut.ID = Nivel.ID " +
                        "INNER JOIN ControloAut ON ControloAut.ID = NivelControloAut.IDControloAut " +
                        string.Format(constraintsCA, "ControloAut.ID"));
                    da.Fill(currentDataSet, "Nivel");
                }

                command.Parameters.Clear();

                command.CommandText = "SELECT * FROM #ItemsID";
                reader = command.ExecuteReader();

                object[] res = new object[3];
                DataView dv;
                DataRowView[] dr;
                dv = currentDataSet.Tables["ControloAutDicionario"].DefaultView;
                dv.ApplyDefaultSort = true;
                
                while (reader.Read())
                {
                    res[0] = reader.GetValue(1);
                    res[1] = reader.GetValue(0);
                    res[2] = reader.GetValue(2);
                    dr = dv.FindRows(res);
                    if (dr.Length > 0)
                        rows.Add(dr[0].Row);
                }
                reader.Close();
            }
			return rows;
		}

		public override void DeleteTemporaryResults(IDbConnection conn)
		{
			SqlCommand command = new SqlCommand("DROP TABLE #ItemsID; DROP TABLE #OrderedItems;", (SqlConnection) conn);
			command.ExecuteNonQuery();
		}

		#endregion

		#region " FormNivelEstrutural "
		public override void LoadFormaAutorizada(DataSet currentDataSet, long caRowID, string TipoControloAutForma, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@caRowID", caRowID);
                command.Parameters.AddWithValue("@TipoControloAutForma", TipoControloAutForma);

                string query = "WHERE IDControloAut = @caRowID AND IDTipoControloAutForma = @TipoControloAutForma";

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"], "INNER JOIN ControloAutDicionario ON IDDicionario = Dicionario.ID " + query);
				da.Fill(currentDataSet, "Dicionario");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ControloAutDicionario"], query);
				da.Fill(currentDataSet, "ControloAutDicionario");
			}
			
		}

		#endregion

		#region ListTermos
		public override ArrayList LoadTermosData(DataSet currentDataSet, bool excludeAutorizados, long excludeAutorizadosTipoNoticiaAut, ArrayList includeOthers, long caID, IDbConnection conn) {
			System.Text.StringBuilder query = new System.Text.StringBuilder();

			if (excludeAutorizados){
				// todos os termos do dicionario excepto as que sejam formas 
				// autorizadas deste tipo de notícia de autoridade
				query.AppendFormat(
					"LEFT JOIN (" + 
					"	SELECT Dicionario.ID FROM Dicionario " + 
					"	INNER JOIN ControloAutDicionario cad ON cad.IDDicionario = Dicionario.ID AND cad.isDeleted = @isDeleted " +
                    "	INNER JOIN TipoControloAutForma tcaf ON tcaf.ID = cad.IDTipoControloAutForma AND tcaf.isDeleted = @isDeleted " +
                    "	INNER JOIN ControloAut ca ON ca.ID = cad.IDControloAut AND ca.isDeleted = @isDeleted " +
					"	WHERE Dicionario.CatCode = 'CA' AND ca.IDTipoNoticiaAut = {0} AND tcaf.ID = 1 " +
					") formasAutorizadas ON formasAutorizadas.ID = Dicionario.ID " + 
					"WHERE (formasAutorizadas.ID IS NULL ", excludeAutorizadosTipoNoticiaAut);
			}

			if (excludeAutorizados /*|| excludeOthers*/){
				if (includeOthers != null)
				{ // excepções às exclusões
					query.Append("OR (");
					foreach (long dID in includeOthers)
					{
						if (dID != (long)includeOthers[0])
							query.Append("OR ");

						query.AppendFormat("Dicionario.ID = {0} ", dID);
					}
					query.Append("))");
				}
				else 
					query.Append(")");
			} else {
				query.Append(
					"LEFT JOIN (" +
						"SELECT d.ID " +
						"FROM Dicionario d " +
							"INNER JOIN ControloAutDicionario cad ON cad.IDDicionario = d.ID " +
                                "AND cad.isDeleted = @isDeleted " +
							"INNER JOIN ControloAut ca ON ca.ID = cad.IDControloAut " +
                                "AND ca.isDeleted = @isDeleted " +
                        "WHERE ca.ID = @caID " +
                            "AND d.isDeleted = @isDeleted" +
					") formasAutorizadas ON formasAutorizadas.ID = Dicionario.ID " +
					"WHERE formasAutorizadas.ID IS NULL ");
			}

            ArrayList termosID = new ArrayList();

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"]);
                da.Fill(currentDataSet, "Dicionario");

                command.CommandText = string.Format("SELECT DISTINCT Dicionario.ID, Dicionario.Termo FROM Dicionario {0} AND Dicionario.isDeleted = @isDeleted AND Dicionario.CatCode = 'CA' ORDER BY Dicionario.Termo", query.ToString());
                command.Parameters.AddWithValue("@caID", caID);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    termosID.Add(reader.GetValue(1));
                }
                reader.Close();
            }
			
			return termosID;
		}
		#endregion
	}
}
