using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
	public class SqlClientTrusteeRule: TrusteeRule
	{
		#region " Trustee "
		// Determina se um determinado novo username ainda não existe na base de dados
		public override bool isValidNewTrustee(string name, System.Data.IDbTransaction tran) 
		{
			int count = 0;
        
			try
			{
				string Query = 
					string.Format("SELECT COUNT(t.ID) FROM Trustee t with (UPDLOCK) " +
					"WHERE t.Name='{0}' AND isDeleted = 0", name);
				
				System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(Query, (System.Data.SqlClient.SqlConnection) tran.Connection, (SqlTransaction) tran);
				count = (int)(command.ExecuteScalar());
			}
			catch (Exception e)
			{
				throw e;
			}
        		
			return count == 0;
		}

		public override TrusteeRule.IndexErrorMessages validateUser(string username, string password, System.Data.IDbConnection conn)
		{
			TrusteeRule.IndexErrorMessages messageCode;
			System.Data.SqlClient.SqlDataReader dataReader = null;
			System.Data.SqlClient.SqlCommand command;

			try 
			{
				if (!(username == null && password == null)) 
				{
					command = new System.Data.SqlClient.SqlCommand("sp_validateUser", (System.Data.SqlClient.SqlConnection) conn);
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar);
					command.Parameters[0].Value = username;
					command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar);
					command.Parameters[1].Value = password;
					dataReader = command.ExecuteReader();
				}
				dataReader.Read();
				messageCode = ((TrusteeRule.IndexErrorMessages)(System.Convert.ToInt32(dataReader.GetValue(0))));
				dataReader.Close();
			}
			catch ( Exception ex ) 
			{
				Trace.WriteLine(ex);
				throw ex;
			}
        
			return messageCode;
		}


        public override void deleteDeletedData(System.Data.IDbConnection conn)
        {
            try
            {
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("sp_deleteDeletedRows", (System.Data.SqlClient.SqlConnection)conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@return", System.Data.SqlDbType.Bit);
                command.Parameters["@return"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw ex;
            }
        }
             
        #endregion

        #region " FormManageAutoresDescricao "
        public override void LoadTrusteeUsers(System.Data.DataSet currentDataSet, System.Data.IDbConnection conn)
		{
			string tuFilter = " LEFT JOIN TrusteeUser ON {0} = TrusteeUser.ID WHERE (TrusteeUser.ID IS NULL OR TrusteeUser.IsAuthority=1 AND TrusteeUser.isDeleted=0) AND {1}=0";

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Trustee"], string.Format(tuFilter, "Trustee.ID", "Trustee.isDeleted"));
				da.Fill(currentDataSet, "Trustee");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeGroup"]);
				da.Fill(currentDataSet, "TrusteeGroup");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeUser"], " WHERE IsAuthority=1");
				da.Fill(currentDataSet, "TrusteeUser");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["UserGroups"], string.Format(tuFilter, "UserGroups.IDUser", "UserGroups.isDeleted"));
				da.Fill(currentDataSet, "UserGroups");
			}
		}

		public override bool IsUserInUse(long tuRowID, System.Data.IDbConnection conn)
		{
			// prever o caso do trustee ter acabado de ser criado; nessa situação 
			// não é necessário ir à BD para verificar que não existem ainda 
			// descrições onde ele exista relacionado
			if (tuRowID < 0) 
			{
				return false;
			}
			bool UserInUse = true;
			try 
			{
				string cmd = string.Format(
					"SELECT SUM(cnt) FROM (" + 
					"   SELECT COUNT(*) cnt FROM FRDBaseDataDeDescricao WHERE IDTrusteeOperator={0} OR IDTrusteeAuthority={0} " + 
					"   UNION " + 
					"   SELECT COUNT(*) FROM ControloAutDataDeDescricao WHERE IDTrusteeOperator={0} OR IDTrusteeAuthority={0}) DatasDescricao ", tuRowID);

				SqlCommand command = new SqlCommand(cmd, (SqlConnection) conn);				
				UserInUse = !(System.Convert.ToInt32(command.ExecuteScalar()).Equals(0));
			} 
			catch (Exception ex) 
			{
				Trace.WriteLine(ex);
				throw ex;
			}
			return UserInUse;
		}

		public override bool hasRegistos(long TrusteeID, System.Data.IDbConnection conn) 
		{
			int rez;
			string sql = "SELECT COUNT(*)" +
				" FROM FRDBaseDataDeDescricao" +
				" WHERE IDTrusteeOperator = " + TrusteeID +
				" OR IDTrusteeAuthority = " + TrusteeID +
				" UNION " +
				" SELECT COUNT(*)" +
				" FROM FRDBaseDataDeDescricao" +
				" WHERE IDTrusteeOperator = " + TrusteeID +
				" OR IDTrusteeAuthority = " + TrusteeID;
			SqlCommand command = new SqlCommand(sql, (SqlConnection) conn);
			command.CommandType = System.Data.CommandType.Text;
			rez = Convert.ToInt32(command.ExecuteScalar());
			if (rez > 0 )
				return true;
			else
				return false;		
		}

		public override bool hasUsers(long TrusteeID, System.Data.IDbConnection conn) 
		{			
			int rez;
			string sql = "SELECT COUNT(*)" +
				" FROM UserGroups" +
				" WHERE IDGroup = " + TrusteeID;
			SqlCommand command = new SqlCommand(sql, (SqlConnection) conn);
			command.CommandType = System.Data.CommandType.Text;
			rez = Convert.ToInt32(command.ExecuteScalar());
			if (rez > 0 )
				return true;
			else
				return false;			
		}
		#endregion

		#region " FormSwitchAuthors "
		public override void LoadAuthorsData(System.Data.DataSet currentDataSet, System.Data.IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@BuiltInTrustee", 0);
                command.Parameters.AddWithValue("@CatCode", "USR");
                command.Parameters.AddWithValue("@IsAuthority", 1);
                string trusteeFilter = "WHERE Trustee.BuiltInTrustee=@BuiltInTrustee AND Trustee.CatCode=@CatCode";
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Trustee"], trusteeFilter);
				da.Fill(currentDataSet, "Trustee");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeUser"],
                    string.Format("INNER JOIN Trustee ON Trustee.ID=TrusteeUser.ID {0} AND TrusteeUser.IsAuthority=@IsAuthority", trusteeFilter));
				da.Fill(currentDataSet, "TrusteeUser");
			}
		}
		#endregion

		#region " GisaPrincipal "
		public override void LoadGroups(System.Data.DataSet currentDataSet, long tuRowID, System.Data.IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@IDUser", tuRowID);
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Trustee"], "WHERE ID IN (SELECT IDGroup FROM UserGroups WHERE IDUser=@IDUser)");
				da.Fill(currentDataSet, "Trustee");
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeGroup"], "WHERE ID IN (SELECT IDGroup FROM UserGroups WHERE IDUser=@IDUser)");
				da.Fill(currentDataSet, "TrusteeGroup");
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["UserGroups"], "WHERE IDUser=@IDUser");
				da.Fill(currentDataSet, "UserGroups");
			}			
		}

		public override void LoadTrusteePrivilegeData(DataTable mTrusteePrivileges, string tsRowBuiltInName, string modules, long tuRowID, System.Data.IDbConnection conn, System.Data.IDbTransaction tran)
		{
            string IDUserKey = "IDUser=@IDUser";
            string IDTrusteeKey = "IDTrustee=@IDTrustee";
			string UserGroupsQuery = "SELECT IDGroup FROM UserGroups WHERE " + IDUserKey;
			
			// fetch grants to user OR grants to group without deny override to user. Also, filter them with the productType being used
			string query = 
				"     INNER JOIN (  " + 
				"	    SELECT IDTrustee, IDTipoFunctionGroup, IdxTipoFunction, IDTipoOperation  " + 
				"	    FROM TrusteePrivilege  " +
                "	    WHERE ((IsGrant=@IsGrant AND " + IDTrusteeKey + ")  " + 
				"		  OR  " +
                "		  (IsGrant=@IsGrant AND  " + 
				"		  IDTrustee IN (" + UserGroupsQuery + ") AND " + 
				"		  NOT EXISTS ( " + 
				"		      SELECT tpg.* FROM TrusteePrivilege tpg  " + 
				"			   INNER JOIN UserGroups ON IDGroup = tpg.IDTrustee " + 
				"		      WHERE " + IDUserKey + " AND " +
                "			    tpg.IsGrant = @NotGrant AND  " + 
				"			    tpg.IDTipoFunctionGroup = TrusteePrivilege.IDTipoFunctionGroup AND  " + 
				"			    tpg.IdxTipoFunction = TrusteePrivilege.IdxTipoFunction AND " + 
				"			    tpg.IDTipoOperation = TrusteePrivilege.IDTipoOperation AND " +
                "               tpg.isDeleted=@isDeleted " + 
				"		  ) AND " + 
				"		  NOT EXISTS( " + 
				"		      SELECT tpu.* FROM TrusteePrivilege tpu  " + 
				"		      WHERE tpu." + IDTrusteeKey + " AND " +
                "			    tpu.IsGrant = @NotGrant AND " + 
				"			    tpu.IDTipoFunctionGroup = TrusteePrivilege.IDTipoFunctionGroup AND  " + 
				"			    tpu.IdxTipoFunction = TrusteePrivilege.IdxTipoFunction AND " + 
				"			    tpu.IDTipoOperation = TrusteePrivilege.IDTipoOperation AND " +
                "               tpu.isDeleted=@isDeleted " + 
				"		  ) " +
                "         )) AND TrusteePrivilege.isDeleted=@isDeleted " + 
				"     ) tps ON " + 
				"	    TrusteePrivilege.IDTrustee = tps.IDTrustee AND " + 
				"	    TrusteePrivilege.IDTipoFunctionGroup = tps.IDTipoFunctionGroup AND " + 
				"	    TrusteePrivilege.IdxTipoFunction = tps.IdxTipoFunction AND " + 
				"	    TrusteePrivilege.IDTipoOperation = tps.IDTipoOperation " + 
				"     INNER JOIN ProductFunction ON " + 
				"	    ProductFunction.IDTipoFunctionGroup = tps.IDTipoFunctionGroup AND " + 
				"	    ProductFunction.IdxTipoFunction = tps.IdxTipoFunction " + 
				"      INNER JOIN TipoServer ON " + 
				" 	    TipoServer.ID = ProductFunction.IDTipoServer " + 
				"      INNER JOIN Modules ON " + 
				" 	    Modules.ID = ProductFunction.IDModule " + 
				"	WHERE TipoServer.BuiltInName LIKE '" + tsRowBuiltInName + "' AND " + 
				"		Modules.ID IN (" + modules + ") AND " +
                "       TrusteePrivilege.isDeleted=@isDeleted";

			SqlCommand command;

			if (tran == null)
				command = new System.Data.SqlClient.SqlCommand("", (System.Data.SqlClient.SqlConnection) conn);
			else
				command = new System.Data.SqlClient.SqlCommand("", (System.Data.SqlClient.SqlConnection) tran.Connection, (SqlTransaction) tran);

            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.Parameters.AddWithValue("@IDUser", tuRowID);
                command.Parameters.AddWithValue("@IDTrustee", tuRowID);
                command.Parameters.AddWithValue("@IsGrant", 1);
                command.Parameters.AddWithValue("@NotGrant", 0);

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(mTrusteePrivileges, query);
				da.Fill(mTrusteePrivileges);
			}		
		}

		#endregion

		#region " FormUserGroups "
		public override void LoadGroupsData(DataSet currentDataSet, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CatCode", "GRP");
                command.Parameters.AddWithValue("@BuiltInTrustee", 0);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Trustee"],
                    "WHERE CatCode=@CatCode AND BuiltInTrustee=@BuiltInTrustee");
				da.Fill(currentDataSet, "Trustee");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeGroup"],
                    "INNER JOIN Trustee t ON t.ID=TrusteeGroup.ID WHERE t.BuiltInTrustee=@BuiltInTrustee");
				da.Fill(currentDataSet, "TrusteeGroup");
			}
		}

		#endregion

		#region " frmMain "
		public override DataRow[] LoadCurrentOperatorData(DataSet currentDataSet, string username, IDbConnection conn)
		{
			string userFilter = string.Format("Name='{0}' AND CatCode='USR'", username);
            DataRow[] tRowOperators;
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Trustee"]);
				da.Fill(currentDataSet, "Trustee");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeUser"]);
				da.Fill(currentDataSet, "TrusteeUser");

				tRowOperators = currentDataSet.Tables["Trustee"].Select(userFilter);
			}

			return tRowOperators;
		}

		public override void saveTrustee(DataSet currentDataSet, DataRow[] rows, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Trustee"]);
				da.UpdateCommand = SqlSyntax.CreateUpdateCommand(currentDataSet.Tables["Trustee"]);
				da.InsertCommand = SqlSyntax.CreateInsertCommand(currentDataSet.Tables["Trustee"]);
				da.DeleteCommand = SqlSyntax.CreateDeleteCommand(currentDataSet.Tables["Trustee"]);
				da.Update(rows);
			}			
		}

		#endregion

		#region " MasterPanelTrusteeGroup "
		public override void LoadTrusteesGrpForUpdate(DataSet currentDataSet, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CatCode", "GRP");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Trustee"],
                    "WHERE CatCode=@CatCode");
				da.Fill(currentDataSet, "Trustee");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeGroup"]);
				da.Fill(currentDataSet, "TrusteeGroup");
			}			
		}
		#endregion

		#region " MasterPanelTrusteeUser "
		public override void LoadTrusteesUsr(DataSet currentDataSet, IDbConnection conn)
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
			}			
		}
		#endregion

		#region " PanelTrusteeDetailsGroup "
		public override void LoadMembership(DataSet CurrentDataSet, long IDTrustee, IDbConnection conn)
		{
			using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["Trustee"]);
				da.Fill(CurrentDataSet, "Trustee");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TrusteeGroup"]);
				da.Fill(CurrentDataSet, "TrusteeGroup");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TrusteeUser"]);
				da.Fill(CurrentDataSet, "TrusteeUser");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["UserGroups"]);
				da.Fill(CurrentDataSet, "UserGroups");
			}		
		}
		#endregion

		#region " PanelTrusteePermissions "
		public override void LoadPanelTrusteePermissionsData(DataSet currentDataSet, long CurrentTrusteeRowID, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@CurrentTrusteeRowID", CurrentTrusteeRowID);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteePrivilege"],
                    "WHERE IDTrustee=@CurrentTrusteeRowID");
				da.Fill(currentDataSet, "TrusteePrivilege");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Trustee"],
					" INNER JOIN UserGroups ON UserGroups.IDGroup = Trustee.ID" +
                    " WHERE UserGroups.IDUser=@CurrentTrusteeRowID");
				da.Fill(currentDataSet, "Trustee");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeGroup"],
					" INNER JOIN UserGroups ON UserGroups.IDGroup = TrusteeGroup.ID" +
                    " WHERE UserGroups.IDUser=@CurrentTrusteeRowID");
				da.Fill(currentDataSet, "TrusteeGroup");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["UserGroups"],
                    " WHERE IDUser=@CurrentTrusteeRowID");
				da.Fill(currentDataSet, "UserGroups");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteePrivilege"],
					" INNER JOIN UserGroups ON UserGroups.IDGroup = TrusteePrivilege.IDTrustee" +
                    " WHERE UserGroups.IDUser =@CurrentTrusteeRowID");
				da.Fill(currentDataSet, "TrusteePrivilege");
			}
		}

		#endregion

		#region " PanelTrusteeNivelPermissions " 
		public override void LoadNivelUserPermissions(DataSet currentDataSet, long trusteeRowID, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@trusteeRowID", trusteeRowID);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"], 
					" INNER JOIN TrusteeNivelPrivilege tnp ON tnp.IDNivel = Nivel.ID " +
                    " WHERE IDTrustee=@trusteeRowID");
				da.Fill(currentDataSet, "Nivel");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"],
					" INNER JOIN TrusteeNivelPrivilege tnp ON tnp.IDNivel = NivelDesignado.ID ");
				da.Fill(currentDataSet, "NivelDesignado");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
					" INNER JOIN RelacaoHierarquica rh on rh.IDUpper = Nivel.ID " + 
					" INNER JOIN TrusteeNivelPrivilege tnp ON tnp.IDNivel = rh.ID ");
				da.Fill(currentDataSet, "Nivel");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
					" INNER JOIN TrusteeNivelPrivilege tnp ON tnp.IDNivel = RelacaoHierarquica.ID ");
				da.Fill(currentDataSet, "RelacaoHierarquica");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeNivelPrivilege"], 
					"WHERE IDTrustee=@trusteeRowID");
				da.Fill(currentDataSet, "TrusteeNivelPrivilege");
			}
		}

		public override void CalculateOrderedItems(long IDNivel, long IDTrustee, long IDLoginTrustee, string FiltroDesignacaoLike, long IDTipoNivelRelacionado, int Filtro, IDbConnection conn)
		{
            string cmd = string.Empty;
			string designacaoQuery = string.Empty;
            string TipoNivelRelacionadoQuery = string.Empty;

            if (Filtro == (int)DocsFilter.Proprio)
            {
                cmd = string.Format(
                    "CREATE TABLE #Perm_Exp (ID BIGINT, IDUpper BIGINT, gen INT) " +
                    "INSERT INTO #Perm_Exp (ID, IDUpper, gen) VALUES ({0}, NULL, NULL)", IDNivel);
            }
            else
            {
                cmd = string.Format(
                    "CREATE TABLE #Perm_Exp (ID BIGINT, IDUpper BIGINT, gen INT) " +
                    "DECLARE @c_age TINYINT " +
                    "SET @c_age = 0 " +
                    "INSERT INTO #Perm_Exp " +
                    "SELECT rh.ID, rh.IDUpper, 0 " +
                    "FROM RelacaoHierarquica rh " +
                    "WHERE rh.isDeleted = 0 AND rh.IDTipoNivelRelacionado < 11 AND rh.IDUpper = {0} " +
                    "WHILE EXISTS(SELECT ID FROM #Perm_Exp WHERE gen = @c_age) " +
                    "BEGIN " +
                        "SET @c_age = @c_age + 1 " +
                        "INSERT INTO #Perm_Exp " +
                        "SELECT rh.ID, rh.IDUpper, @c_age " +
                        "FROM RelacaoHierarquica rh " +
                            "INNER JOIN #Perm_Exp t1 ON t1.ID = rh.IDUpper AND t1.gen = @c_age - 1 " +
                            "{1} " +
                            "LEFT JOIN #Perm_Exp t2 ON t2.ID = rh.ID " +
                        "WHERE rh.isDeleted = 0 AND t2.ID IS NULL {2} " +
                    "END ", IDNivel,
                    (Filtro == (int)DocsFilter.Todos ? string.Empty : "INNER JOIN Nivel nUpper ON nUpper.ID = rh.IDUpper"),
                    (Filtro == (int)DocsFilter.Todos ? string.Empty : "AND (nUpper.IDTipoNivel < 3 OR (nUpper.IDTipoNivel < 3 AND rh.IDTipoNivelRelacionado > 6))"));
                    
                if (Filtro == (int)DocsFilter.Todos)
                    cmd += string.Format("INSERT INTO #Perm_Exp (ID, IDUpper, gen) VALUES ({0}, NULL, NULL)", IDNivel);

                if (Filtro == (int)DocsFilter.TodosDocumentais)
                    cmd +=
                        "DELETE FROM #Perm_Exp " +
                        "FROM #Perm_Exp " +
                            "INNER JOIN Nivel n ON n.ID = #Perm_Exp.ID AND n.IDTipoNivel < 3 AND n.isDeleted = 0";

            }
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = cmd;
            command.ExecuteNonQuery();

            PermissoesRule.Current.GetEffectivePermissions(" FROM #Perm_Exp ", IDLoginTrustee, conn);

            command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = string.Format(@"
SELECT #Perm_Exp.ID
INTO #Niveis
FROM #Perm_Exp
    INNER JOIN #effective E ON E.IDNivel = #Perm_Exp.ID 
WHERE E.Escrever = 1", IDLoginTrustee);
            command.ExecuteNonQuery();

            if (IDTipoNivelRelacionado > 0)
                TipoNivelRelacionadoQuery = string.Format(" AND IDTipoNivelRelacionado = {0} ", IDTipoNivelRelacionado.ToString());
            
            if (FiltroDesignacaoLike.Length > 0)
                designacaoQuery = string.Format(" AND {0} ", FiltroDesignacaoLike);

            command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = "CREATE TABLE #OrderedItems ( seq_id INT Identity( 1, 1), ID BIGINT, Designacao NVARCHAR(768), IDTipoNivelRelacionado BIGINT); ";
            command.ExecuteNonQuery();

            command.CommandText = string.Format(
                "SELECT IDGroup ID INTO #UsersTMP FROM UserGroups ug WHERE IDUser = " + IDTrustee.ToString() + "; " +
                "INSERT INTO #UsersTMP VALUES (" + IDTrustee.ToString() + "); " +
                "INSERT INTO #OrderedItems (ID, Designacao, IDTipoNivelRelacionado) " +
                "SELECT DISTINCT ID, Designacao, IDTipoNivelRelacionado " +
                "FROM (" +
                    "SELECT n.ID, nd.Designacao, 1 IDTipoNivelRelacionado " +
                    "FROM #Niveis " +
                        "INNER JOIN Nivel n ON n.ID = #Niveis.ID AND n.IDTipoNivel = 1 AND n.isDeleted = 0 " +
                        "LEFT JOIN RelacaoHierarquica rh ON rh.ID = n.ID AND rh.isDeleted = 0 " +
                        "INNER JOIN NivelDesignado nd ON nd.ID = n.ID {0} " +
                    "WHERE rh.ID IS NULL AND nd.isDeleted = 0 {1}" +
                    "UNION " +
                    "SELECT #Niveis.ID, nd.Designacao Designacao, MIN(rh.IDTipoNivelRelacionado) IDTipoNivelRelacionado " +
                    "FROM #Niveis " +
                        "INNER JOIN RelacaoHierarquica rh ON rh.ID = #Niveis.ID {1} AND rh.isDeleted = 0 " +
                        "INNER JOIN NivelDesignado nd ON nd.ID = #Niveis.ID {0} AND nd.isDeleted = 0 " +
                    "GROUP BY #Niveis.ID, nd.Designacao " +
                    "UNION " +
                    "SELECT #Niveis.ID, d.Termo Designacao, MIN(rh.IDTipoNivelRelacionado) IDTipoNivelRelacionado " +
                    "FROM #Niveis " +
                        "INNER JOIN RelacaoHierarquica rh ON rh.ID = #Niveis.ID {1} AND rh.isDeleted = 0 " +
                        "INNER JOIN NivelControloAut nca ON nca.ID = #Niveis.ID AND nca.isDeleted = 0 " +
                        "INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = nca.IDControloAut AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0 " +
                        "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario {2} AND d.isDeleted = 0 " +
                    "GROUP BY #Niveis.ID, d.Termo) niveis " +
                "ORDER BY IDTipoNivelRelacionado, Designacao", designacaoQuery, TipoNivelRelacionadoQuery, designacaoQuery.Replace("Designacao", "Termo"));
            
            command.ExecuteNonQuery();

            command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = "DROP TABLE #Perm_Exp; DROP TABLE #Niveis";
            command.ExecuteNonQuery();

            PermissoesRule.Current.DropEffectivePermissionsTempTable(conn);
		}

        public override ArrayList GetItems(DataSet currentDataSet, int pageNr, int itemsPerPage, long IDTrustee, IDbConnection conn)
		{
            var res = new ArrayList();
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #ItemsID (ID BIGINT, Designacao NVARCHAR(2000))";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO #ItemsID SELECT ID, Designacao FROM #OrderedItems WHERE seq_id >= @seq_id1 AND seq_id <= @seq_id2 ORDER BY seq_id";
                command.Parameters.AddWithValue("@seq_id1", (pageNr - 1) * itemsPerPage + 1);
                command.Parameters.AddWithValue("@seq_id2", pageNr * itemsPerPage);
                command.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        " INNER JOIN #ItemsID ON #ItemsID.ID = Nivel.ID ");
                    da.Fill(currentDataSet, "Nivel");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"],
                        " INNER JOIN #ItemsID ON #ItemsID.ID = NivelDesignado.ID ");
                    da.Fill(currentDataSet, "NivelDesignado");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        " INNER JOIN RelacaoHierarquica rh ON rh.IDUpper = Nivel.ID " +
                        " INNER JOIN #ItemsID ON #ItemsID.ID = rh.ID ");
                    da.Fill(currentDataSet, "Nivel");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                        " INNER JOIN #ItemsID ON #ItemsID.ID = RelacaoHierarquica.ID ");
                    da.Fill(currentDataSet, "RelacaoHierarquica");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeNivelPrivilege"],
                        " INNER JOIN #ItemsID ON #ItemsID.ID = TrusteeNivelPrivilege.IDNivel " +
                        " INNER JOIN #UsersTMP usr ON usr.ID = TrusteeNivelPrivilege.IDTrustee ");
                    da.Fill(currentDataSet, "TrusteeNivelPrivilege");
                }

                PermissoesRule.Current.GetEffectivePermissions(" FROM #ItemsID ", IDTrustee, conn);

                string Query =
                    "SELECT I.ID, I.Designacao, E.Criar, E.Ler, E.Escrever, E.Apagar, E.Expandir FROM #ItemsID I INNER JOIN #effective E ON E.IDNivel = I.ID";
                command.CommandText = Query;
                SqlDataReader reader = command.ExecuteReader();
                TrusteeRule.Nivel item;

                while (reader.Read())
                {
                    item = new TrusteeRule.Nivel();
                    item.ID = reader.GetInt64(0);
                    item.Designacao = reader.GetString(1);
                    item.Permissoes = new Dictionary<string, byte>();
                    if (!reader.IsDBNull(2)) item.Permissoes.Add(reader.GetName(2), reader.GetByte(2));
                    if (!reader.IsDBNull(3)) item.Permissoes.Add(reader.GetName(3), reader.GetByte(3));
                    if (!reader.IsDBNull(4)) item.Permissoes.Add(reader.GetName(4), reader.GetByte(4));
                    if (!reader.IsDBNull(5)) item.Permissoes.Add(reader.GetName(5), reader.GetByte(5));
                    if (!reader.IsDBNull(6)) item.Permissoes.Add(reader.GetName(6), reader.GetByte(6));
                    res.Add(item);
                }
                reader.Close();
            }

			return res;
		}        

		public override void DeleteTemporaryResults(IDbConnection conn)
		{
            using (SqlCommand command = new SqlCommand("DROP TABLE #ItemsID; DROP TABLE #OrderedItems; DROP TABLE #UsersTMP;", (SqlConnection)conn))
            {
                command.ExecuteNonQuery();
            }
		}
		#endregion

        #region " FormPickUser "
        public override List<User> LoadUsers(DataSet currentDataSet, IDbConnection conn)
        {
            List<User> users = new List<User>();

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Trustee"]);
                da.Fill(currentDataSet, "Trustee");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeGroup"]);
                da.Fill(currentDataSet, "TrusteeGroup");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeUser"]);
                da.Fill(currentDataSet, "TrusteeUser");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["UserGroups"]);
                da.Fill(currentDataSet, "UserGroups");

                command.CommandText =
                    "SELECT t.ID, t.Name, CASE WHEN NOT tg.ID IS NULL THEN 'Grupo' WHEN NOT tu.ID IS NULL THEN 'Utilizador' END, 'Interno' " +
                    "FROM Trustee t " +
                        "LEFT JOIN TrusteeGroup tg ON tg.ID = t.ID AND tg.isDeleted = @isDeleted " +
                        "LEFT JOIN TrusteeUser tu ON tu.ID = t.ID AND tu.isDeleted = @isDeleted " +
                    "WHERE t.isDeleted = @isDeleted " +
                    "ORDER BY t.CatCode, t.Name";
                SqlDataReader reader = command.ExecuteReader();

                TrusteeRule.User u = null;
                while (reader.Read())
                {
                    u = new User();
                    u.userID = System.Convert.ToInt64(reader.GetValue(0));
                    u.userName = reader.GetValue(1).ToString();
                    u.userType = reader.GetValue(2).ToString();
                    u.userInternalExternal = reader.GetValue(3).ToString();
                    users.Add(u);
                }
                reader.Close();
            }

            return users;
        }
        #endregion

        #region PanelTrusteeObjetoDigitalPermissions
        public override void CalculateODOrderedItems(long IDNivel, long IDTrustee, long IDLoginTrustee, ArrayList ordenacao, IDbConnection conn)
        {
            StringBuilder orderByQuery = new StringBuilder();

            var command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = "CREATE TABLE #OrderedItems ( seq_id INT Identity( 1, 1), ID BIGINT PRIMARY KEY); ";
            command.ExecuteNonQuery();

            // identificar todos os niveis descendentes  de IDNivel
            command = new SqlCommand("CREATE TABLE #temp(ID BIGINT PRIMARY KEY)", (SqlConnection)conn);
            command.ExecuteNonQuery();

            command.CommandText = string.Format("INSERT INTO #temp VALUES ({0})", IDNivel);
            command.ExecuteNonQuery();

            command.CommandText = string.Format(@"
WITH Temp (ID, IDUpper)
AS (
    SELECT rh.ID, rh.IDUpper
    FROM RelacaoHierarquica rh
    WHERE rh.IDUpper = {0} AND rh.isDeleted = 0
    
    UNION ALL
	
    SELECT rh.ID, rh.IDUpper
    FROM RelacaoHierarquica rh
		INNER JOIN Temp ON Temp.ID = rh.IDUpper
    WHERE rh.isDeleted = 0
)
INSERT INTO #temp
SELECT Temp.ID 
FROM Temp", IDNivel);
            command.ExecuteNonQuery();

            // dos niveis identificados retirar aqueles para os quais IDLoginTrustee não tem permissões de leitura
            PermissoesRule.Current.GetEffectiveReadPermissions(" FROM #temp ", IDLoginTrustee, conn);

            command.CommandText = "DELETE FROM #temp WHERE ID IN (SELECT IDNivel FROM #effective WHERE Ler = 0 OR Ler IS NULL)";
            command.ExecuteNonQuery();

            PermissoesRule.Current.DropEffectivePermissionsTempTable(conn);

            string query = @"
FROM (
SELECT od.ID, od.pid, od.Titulo 
FROM #temp T
INNER JOIN FRDBase frd ON frd.IDNivel = T.ID AND frd.IDTipoFRDBase = 1 AND frd.isDeleted = 0
INNER JOIN SFRDImagem img ON img.IDFRDBase = frd.ID AND img.Tipo = 'Fedora' AND img.isDeleted = 0
INNER JOIN SFRDImagemObjetoDigital imgOD ON imgOD.IDFRDBase = img.IDFRDBase AND imgOD.idx = img.idx AND imgOD.isDeleted = 0
INNER JOIN ObjetoDigital od ON od.ID = imgOD.IDObjetoDigital AND od.isDeleted = 0
LEFT JOIN ObjetoDigitalRelacaoHierarquica odrh ON odrh.IDUpper = od.ID AND odrh.isDeleted = 0
WHERE odrh.IDUpper IS NULL
UNION ALL
SELECT odSimples.ID, odSimples.pid, odSimples.Titulo
FROM #temp T
INNER JOIN FRDBase frd ON frd.IDNivel = T.ID AND frd.IDTipoFRDBase = 1 AND frd.isDeleted = 0
INNER JOIN SFRDImagem img ON img.IDFRDBase = frd.ID AND img.Tipo = 'Fedora' AND img.isDeleted = 0
INNER JOIN SFRDImagemObjetoDigital imgOD ON imgOD.IDFRDBase = img.IDFRDBase AND imgOD.idx = img.idx AND imgOD.isDeleted = 0
INNER JOIN ObjetoDigital od ON od.ID = imgOD.IDObjetoDigital AND od.isDeleted = 0
INNER JOIN ObjetoDigitalRelacaoHierarquica odrh ON odrh.IDUpper = od.ID AND odrh.isDeleted = 0
INNER JOIN ObjetoDigital odSimples ON odSimples.ID = odrh.ID AND odSimples.isDeleted = 0
) ods";

            // identificar todos os objetos digitais associados ao conjunto de niveis identificado anteriormente
            if (ordenacao.Count > 0) // considerando ordernação
            {
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
                        //pid
                        case 0:
                            orderByQuery.AppendFormat(" pid {0}", order);
                            break;
                        //titulo
                        case 1:
                            orderByQuery.AppendFormat(" titulo {0}", order);
                            break;
                        //ler
                        case 2:
                            orderByQuery.AppendFormat(" ler {0}", order);
                            break;
                        //escrever
                        case 3:
                            orderByQuery.AppendFormat(" escrever {0}", order);
                            break;
                    }
                }

                command.CommandText = string.Format(@"
declare @t bigint
set @t = {0}

declare @trustees table (ID bigint)
insert into @trustees values (@t)
insert into @trustees select IDGroup from UserGroups where IDUser = @t

create table #ods (ID bigint primary key, pid nvarchar(20), titulo nvarchar(768), ler tinyint, escrever tinyint)
insert into #ods
select DISTINCT ID, pid, Titulo, null, null
{1}

update od
set ler = tnpL.IsGrant,
	escrever = tnpE.IsGrant
from #ods od
	inner join TrusteeObjetoDigitalPrivilege tnpL on tnpL.IDObjetoDigital = od.ID and tnpL.IDTrustee = @t and tnpL.IDTipoOperation = 2 and not tnpL.IsGrant is null
	inner join TrusteeObjetoDigitalPrivilege tnpE on tnpE.IDObjetoDigital = od.ID and tnpE.IDTrustee = @t and tnpE.IDTipoOperation = 3 and not tnpE.IsGrant is null
	
update od
set ler = ISNULL(od.ler, tnpL.ler),
	escrever = ISNULL(od.escrever, tnpE.escrever)
from #ods od
	inner join (
		select tnp.IDObjetoDigital, min(convert(tinyint, tnp.IsGrant)) ler
		from TrusteeObjetoDigitalPrivilege tnp 	
			inner join @trustees T on T.ID = tnp.IDTrustee
		where tnp.IDTipoOperation = 2
		group by tnp.IDObjetoDigital
	) tnpL on tnpL.IDObjetoDigital = od.ID
	inner join (
		select tnp.IDObjetoDigital, min(convert(tinyint, tnp.IsGrant)) escrever
		from TrusteeObjetoDigitalPrivilege tnp 	
			inner join @trustees T on T.ID = tnp.IDTrustee
		where tnp.IDTipoOperation = 3
		group by tnp.IDObjetoDigital
	) tnpE on tnpE.IDObjetoDigital = od.ID
where od.ler is null or od.escrever is null

insert into #OrderedItems
select ID from #ods order by {2}

drop table #ods", IDTrustee, query, orderByQuery.ToString());
                command.ExecuteNonQuery();
            }
            else // não considerando ordenação
            {
                command.CommandText = string.Format(@"
INSERT INTO #OrderedItems
SELECT DISTINCT ID
{0}
ORDER BY ID", query);
                command.ExecuteNonQuery();
            }

            // dos niveis identificados retirar aqueles para os quais IDLoginTrustee não tem permissões de leitura
            PermissoesRule.Current.GetODEffectivePermissions(" FROM #OrderedItems ", IDLoginTrustee, conn);

            command.CommandText = "DELETE FROM #OrderedItems WHERE ID IN (SELECT DISTINCT ID FROM #effective WHERE IsGrant = 0 OR IsGrant IS NULL)";
            command.ExecuteNonQuery();

            PermissoesRule.Current.DropEffectivePermissionsTempTable(conn);
        }

        public override int CountODPages(int itemsPerPage, IDbConnection conn)
        {
            SqlDataReader reader;
            SqlCommand command = new SqlCommand("", (SqlConnection)conn);

            command.CommandText = "SELECT COUNT(*) FROM #OrderedItems";

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

        public override ArrayList GetODItems(DataSet currentDataSet, int pageNr, int itemsPerPage, long IDTrustee, IDbConnection conn)
        {
            var res = new ArrayList();

            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #ItemsID (ID BIGINT, pid NVARCHAR(20), Titulo NVARCHAR(768))";
                command.ExecuteNonQuery();

                command.CommandText = 
                    "INSERT INTO #ItemsID " +
                    "SELECT od.ID, od.pid, od.Titulo " +                    
                    "FROM #OrderedItems " +
                    "INNER JOIN ObjetoDigital od ON od.ID = #OrderedItems.ID AND od.isDeleted = @isDeleted " +
                    "WHERE seq_id >= @seq_id1 AND seq_id <= @seq_id2 ORDER BY seq_id";
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.Parameters.AddWithValue("@seq_id1", (pageNr - 1) * itemsPerPage + 1);
                command.Parameters.AddWithValue("@seq_id2", pageNr * itemsPerPage);
                command.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["ObjetoDigital"],
                        "INNER JOIN #ItemsID ON #ItemsID.ID = ObjetoDigital.ID ");
                    da.Fill(currentDataSet, "ObjetoDigital");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeUser"]);
                    da.Fill(currentDataSet, "TrusteeUser");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeGroup"]);
                    da.Fill(currentDataSet, "TrusteeGroup");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["UserGroups"]);
                    da.Fill(currentDataSet, "UserGroups");

                    // carregar permissões
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeObjetoDigitalPrivilege"],
                        "INNER JOIN #ItemsID ON #ItemsID.ID = TrusteeObjetoDigitalPrivilege.IDObjetoDigital " +
                        "WHERE TrusteeObjetoDigitalPrivilege.IDTrustee = " + IDTrustee);
                    da.Fill(currentDataSet, "TrusteeObjetoDigitalPrivilege");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["TrusteeObjetoDigitalPrivilege"],
                        "INNER JOIN #ItemsID ON #ItemsID.ID = TrusteeObjetoDigitalPrivilege.IDObjetoDigital " +
                        "WHERE TrusteeObjetoDigitalPrivilege.IDTrustee IN (SELECT IDGroup FROM UserGroups WHERE IDUser = " + IDTrustee + ")");
                    da.Fill(currentDataSet, "TrusteeObjetoDigitalPrivilege");
                }

                PermissoesRule.Current.GetODEffectivePermissions(" FROM #ItemsID ", IDTrustee, conn);

                command.CommandText = "SELECT I.ID, I.pid, I.Titulo, E.IDTipoOperation, E.IsGrant FROM #ItemsID I INNER JOIN #effective E ON E.ID = I.ID";
                var reader = command.ExecuteReader();

                var od = new PermissoesRule.ObjDig();
                var readed_rows = new Dictionary<long, PermissoesRule.ObjDig>();
                long ID = 0;
                while (reader.Read())
                {
                    ID = reader.GetInt64(0);
                    if (readed_rows.ContainsKey(ID))
                    {
                        od = readed_rows[ID];
                        if (!reader.IsDBNull(4)) od.Permissoes.Add(reader.GetInt64(3), reader.GetByte(4));
                    }
                    else
                    {
                        od = new PermissoesRule.ObjDig();
                        od.ID = reader.GetInt64(0);
                        od.pid = reader.GetString(1);
                        od.titulo = reader.GetString(2);
                        od.Permissoes = new Dictionary<long, byte>();
                        if (!reader.IsDBNull(4)) od.Permissoes.Add(reader.GetInt64(3), reader.GetByte(4));
                        readed_rows.Add(ID, od);
                        res.Add(od);
                    }
                }
                reader.Close();

                PermissoesRule.Current.DropEffectivePermissionsTempTable(conn);
            }

            return res;
        }

        public override void DeleteODTemporaryResults(IDbConnection conn)
        {
            using (SqlCommand command = new SqlCommand("DROP TABLE #ItemsID; DROP TABLE #OrderedItems;", (SqlConnection)conn))
            {
                command.ExecuteNonQuery();
            }
        }
        #endregion
    }
}