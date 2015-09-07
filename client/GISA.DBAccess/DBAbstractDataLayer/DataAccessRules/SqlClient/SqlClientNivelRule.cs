using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
	public sealed class SqlClientNivelRule: NivelRule {

        public override string GetCodigoCompletoNivel(long NivelRowID, IDbConnection conn)
        {
            string cod = string.Empty;
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = "sp_getCodigoCompletoNivel";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@nivelID", SqlDbType.BigInt).Value = NivelRowID;

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read()) 
                if (reader.GetValue(1) != null)
                    cod =  reader.GetValue(1).ToString();

            reader.Close();

            return cod;
        }

        public override void LoadNivelDocumental(DataSet CurrentDataSet, long NivelRowID, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@NivelRowID", NivelRowID);
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["Nivel"],
                    "WHERE ID=@NivelRowID");
                da.Fill(CurrentDataSet, "Nivel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["NivelDesignado"],
                    "WHERE ID=@NivelRowID");
                da.Fill(CurrentDataSet, "NivelDesignado");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["Nivel"],
                    "INNER JOIN RelacaoHierarquica rh ON rh.IDUpper = Nivel.ID " +
                    "WHERE rh.ID=@NivelRowID");
                da.Fill(CurrentDataSet, "Nivel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["RelacaoHierarquica"],
                    "WHERE ID=@NivelRowID");
                da.Fill(CurrentDataSet, "RelacaoHierarquica");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["FRDBase"],
                    "WHERE IDNivel=@NivelRowID");
                da.Fill(CurrentDataSet, "FRDBase");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["SFRDDatasProducao"],
                    "INNER JOIN FRDBase frd ON frd.ID = SFRDDatasProducao.IDFRDBase " +
                    "WHERE frd.IDNivel=@NivelRowID");
                da.Fill(CurrentDataSet, "SFRDDatasProducao");
            }
        }

		#region "Delete Nivel"
		public override void DeleteNivelInDataBase(long NivelID, IDbTransaction tran) {
			SqlCommand command = new SqlCommand("sp_deleteNivel", (SqlConnection) tran.Connection, (SqlTransaction) tran);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@IDNivel", SqlDbType.BigInt);
			command.Parameters[0].Value = NivelID;
			
			try {
				command.ExecuteNonQuery();				
			} 
			catch (Exception ex) {
				Trace.WriteLine(ex);
				throw;
			}
		}

		public override void DeleteFRDBaseInDataBase(long FRDRowID, IDbTransaction tran) {
			SqlCommand command = new SqlCommand("sp_deleteFRD", (SqlConnection) tran.Connection, (SqlTransaction) tran);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@IDFRDBase", SqlDbType.BigInt);
			command.Parameters[0].Value = FRDRowID;
			//command.Transaction = tran;

			try {
				command.ExecuteNonQuery();				
			} 
			catch (Exception) {
				throw;
			}
		}
		#endregion
		
		// Collect the children of NivelRow that have children of their own
		public override Hashtable EstimateChildCount(string NivelRowID, bool shouldShowSeries, IDbConnection conn) {
			Hashtable ChildCount = new Hashtable();
		
			try {
				StringBuilder Query = new StringBuilder();
				Query.Append(
					"SELECT DISTINCT rhPai.ID " +
					"FROM RelacaoHierarquica rhPai " +
					"INNER JOIN RelacaoHierarquica rhFilho ON rhPai.ID=rhFilho.IDUpper " +
					"INNER JOIN Nivel nivelPai ON nivelPai.ID=rhPai.ID " +
					"INNER JOIN TipoNivel tnPai ON nivelPai.IDTipoNivel=tnPai.ID " +
					"INNER JOIN Nivel nivelFilho ON nivelFilho.ID=rhFilho.ID " +
					"INNER JOIN TipoNivel tnFilho ON nivelFilho.IDTipoNivel=tnFilho.ID " +
					"WHERE rhPai.IDUpper=" + NivelRowID + " " +					
					"AND rhPai.isDeleted=0 AND rhFilho.isDeleted=0");

				if (!shouldShowSeries) {
					//considerar somente níveis estruturais
					Query.Append(
						" AND tnPai.IsDocument=0 " +
						"AND tnFilho.IsDocument=0");
				}
				else {
					//considerar até aos documentos exclusivé
					Query.Append(" AND rhFilho.IDTipoNivelRelacionado < 9");
				}

				SqlCommand command = new SqlCommand(Query.ToString(), (SqlConnection) conn);
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read()) {
					object result = reader.GetValue(0);
					ChildCount.Add(result, result);
				}
				reader.Close();
			}			
			catch (Exception ex) {
				Trace.WriteLine(ex);
				throw;
			}
			return ChildCount;
		}

		public override int getDirectChildCount(string nivelRowID, string extraFilter, IDbConnection conn)
		{
			int count = 0;
			try 
			{
				string Query = 
					"SELECT COUNT(rh.ID) " + 
					"FROM RelacaoHierarquica rh " + 
					"INNER JOIN Nivel nLower ON nLower.ID = rh.ID " + 
					"WHERE rh.IDUpper = " + nivelRowID + 
					" AND rh.isDeleted = 0";

				if (extraFilter.Length != 0) 
				{
					Query += " AND " + extraFilter;
				}
				SqlCommand command = new SqlCommand(Query, (SqlConnection) conn);
				count = System.Convert.ToInt32(command.ExecuteScalar());
			} 
			catch (Exception e) 
			{
				Trace.WriteLine(e);
				throw;
			}
			return count;
		}

		public override int getDirectChildCount(string nivelRowID, string extraFilter, IDbTransaction tran) 
		{
			int count = 0;
			try {
				string Query = 
					"SELECT COUNT(rh.ID) " + 
					"FROM RelacaoHierarquica rh WITH (UPDLOCK) " + 
					"INNER JOIN Nivel nLower ON nLower.ID = rh.ID " + 
					"WHERE rh.IDUpper = " + nivelRowID + 
					" AND rh.isDeleted = 0";

				if (extraFilter.Length != 0) {
					Query += " AND " + extraFilter;
				}
				SqlCommand command = new SqlCommand(Query, (SqlConnection) tran.Connection, (SqlTransaction) tran);
				count = System.Convert.ToInt32(command.ExecuteScalar());
			} catch (Exception e) {
				Trace.WriteLine(e);
				throw;
			}
			return count;
		}

		public override int getParentCount(string nivelRowID, IDbConnection conn)
		{
			int count = 0;
			string Query; 
			try 
			{
				Query = 
					"SELECT COUNT(rh.ID) FROM RelacaoHierarquica rh " + 
					"WHERE rh.ID = " + nivelRowID + 
					" AND rh.isDeleted = 0";
				
				SqlCommand command = new SqlCommand(Query, (SqlConnection) conn);
				count = System.Convert.ToInt32(command.ExecuteScalar());
			} 
			catch (Exception ex) 
			{
				Trace.WriteLine(ex);
				throw;
			}
			return count;
		}


		

		public override int getParentCount(string nivelRowID, IDbTransaction tran)
		{
			int count = 0;
			string Query; 
			try {				
				Query = 
					"SELECT COUNT(rh.ID) FROM RelacaoHierarquica rh WITH (UPDLOCK) " + 
					"WHERE rh.ID = " + nivelRowID + 
					" AND rh.isDeleted = 0";

				SqlCommand command = new SqlCommand(Query, (SqlConnection) tran.Connection, (SqlTransaction) tran);
				count = System.Convert.ToInt32(command.ExecuteScalar());
			} catch (Exception ex) {
				Trace.WriteLine(ex);
				throw;
			}
			return count;
		}

		public override ArrayList GetCodigoOfNivel(long nivelRowID, IDbConnection conn){
			ArrayList codigos = new ArrayList();
			try 
			{
				SqlCommand command = new SqlCommand("CREATE TABLE #SPParametersNiveis (IDNivel BIGINT);CREATE TABLE #SPResultsCodigos(IDNivel BIGINT, CodigoCompleto NVARCHAR(300));", (SqlConnection) conn);
				command.CommandType = CommandType.Text;
				command.ExecuteNonQuery();

				command.CommandText = string.Format("INSERT INTO #SPParametersNiveis VALUES({0});", nivelRowID);
				command.ExecuteNonQuery();

				command.CommandText = "sp_getCodigosCompletosNiveis";
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Clear();
				command.ExecuteNonQuery();

				command.CommandText = "SELECT * FROM #SPResultsCodigos";
				command.CommandType = CommandType.Text;
				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{				
					codigos.Add(reader.GetValue(1));
				}
				reader.Close();

				// limpar tabelas temporárias que serviram de suporte ao processo de cálculo
				command = new SqlCommand("DROP TABLE #SPParametersNiveis; DROP TABLE #SPResultsCodigos", (SqlConnection) conn);
				command.CommandType = CommandType.Text;
				command.ExecuteNonQuery();
			}  
			catch (Exception ex) 
			{
				Trace.WriteLine(ex);
				throw;
			}
			return codigos;
		}

		public override bool existsRelacaoHierarquica(string IDUpper, string ID, IDbTransaction tran) {
			int count = 0;
			try {
				string Query = string.Format("SELECT COUNT(rh.ID) " + 
					"FROM RelacaoHierarquica rh with (UPDLOCK) " + 
					"WHERE rh.IDUpper = {0} AND rh.ID={1}" + 
					" AND rh.isDeleted = 0", IDUpper, ID);

				SqlCommand command = new SqlCommand(Query, (SqlConnection) tran.Connection, (SqlTransaction) tran);
				count = System.Convert.ToInt32(command.ExecuteScalar());
			} catch (Exception e) {
				Trace.WriteLine(e);
				throw;
			}
			return count > 0;
		}

		public override bool existsNivel(string nRowID, IDbTransaction tran) {
			int count = 0;
			try {
				string Query = string.Format("SELECT COUNT(n.ID) " + 
					"FROM Nivel n with (UPDLOCK) " + 
					"WHERE n.ID = {0} AND n.isDeleted = 0", nRowID);
				SqlCommand command = new SqlCommand(Query, (SqlConnection) tran.Connection, (SqlTransaction) tran);
				count = System.Convert.ToInt32(command.ExecuteScalar());
			} catch (Exception e) {
				Trace.WriteLine(e);
				throw;
			}
			return count > 0;
		}

		public override bool isUniqueCodigo(string Codigo, long IDNivel, IDbTransaction tran){
			return isUniqueCodigo(Codigo, IDNivel, tran, false, long.MinValue);
		}

		public override bool isUniqueCodigo(string Codigo, long IDNivel, IDbTransaction tran, bool testOnlyWithinNivel)
		{
			return isUniqueCodigo(Codigo, IDNivel, tran, testOnlyWithinNivel, long.MinValue);
		}

		public override bool isUniqueCodigo(string Codigo, long IDNivel, IDbTransaction tran, bool testOnlyWithinNivel, long IDNivelUpper)
		{
			int count = 0;
		
			string Query;
			if (testOnlyWithinNivel) 
			{
				if (IDNivelUpper > 0)
                    Query = string.Format(
						"SELECT COUNT(n.ID) " +
						"FROM Nivel n WITH (UPDLOCK) " +
							"LEFT JOIN RelacaoHierarquica rh ON rh.ID=n.ID " +
						"WHERE n.Codigo='{0}' " +
							"AND n.ID != {1} " +
							"AND rh.IDUpper = {2} " +
							"AND n.isDeleted=0 " +
							"AND rh.isDeleted=0", Codigo, IDNivel, IDNivelUpper);
				else
					// Entidades detentoras
					Query = string.Format(
						"SELECT COUNT(n.ID) " +
						"FROM Nivel n WITH (UPDLOCK) " +						
						"WHERE n.Codigo='{0}' " +
						"AND n.ID != {1} " +
						"AND IDTipoNivel = 1 " +
						"AND n.isDeleted=0 ", Codigo, IDNivel);
			}
			else {
				// verificar se dentro de todos os niveis estruturais não existem códigos iguais
				Query = string.Format(
					"SELECT COUNT(n.ID) " +
					"FROM Nivel n WITH (UPDLOCK) " +
					"WHERE n.Codigo='{0}' " +
						"AND n.ID != {1} " +
						"AND n.IDTipoNivel = 2 " +
						"AND n.isDeleted=0", Codigo, IDNivel);
			}
			
			SqlCommand command = new SqlCommand(Query, (SqlConnection) tran.Connection, (SqlTransaction) tran);			
			count = System.Convert.ToInt32(command.ExecuteScalar());

			return count == 0;
		}

        public override long GetIDCodigoRepetido(string Codigo, long IDNivel, IDbTransaction tran, bool testOnlyWithinNivel, long IDNivelUpper)
        {
            string query = string.Format(
                        "SELECT n.ID " +
                        "FROM Nivel n WITH (UPDLOCK) " +
                            "INNER JOIN RelacaoHierarquica rh ON rh.ID = n.ID " +
                        "WHERE n.Codigo='{0}' " +
                            "AND n.ID != {1} " +
                            "AND rh.IDUpper = {2} " +
                            "AND n.isDeleted=0 " +
                            "AND rh.isDeleted=0", Codigo, IDNivel, IDNivelUpper);

            var ID = long.MinValue;
            SqlCommand command = new SqlCommand(query, (SqlConnection)tran.Connection, (SqlTransaction)tran);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                ID = reader.GetInt64(0);
                break;
            }
            reader.Close();

            return ID;
        }

		public override int getUnidadesDescricaoCountForUnidadeFisica(long IDNivelUF, IDbConnection conn){
			int count = 0;
		
			string Query = string.Format("SELECT COUNT(*) FROM SFRDUnidadeFisica sfrduf INNER JOIN FRDBase frd ON frd.ID = sfrduf.IDFRDBase WHERE sfrduf.IDNivel = {0} AND frd.IDTipoFRDBase=1", IDNivelUF); //, TipoFRDBase.FRDOIRecolha);
			SqlCommand command = new SqlCommand(Query, (SqlConnection)conn);
			count = System.Convert.ToInt32(command.ExecuteScalar());

			return count;
		}

		#region Carregamento de niveis e notícias de autoridade
		// Carrega as entidades detentoras existentes
		public override long[] LoadEntidadesDetentoras(DataSet dataSet, IDbConnection conn) {
            List<long> Ids = new List<long>();

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@IDTipoNivel", 1);
				
                string suffix = "LEFT JOIN RelacaoHierarquica rh ON rh.ID = Nivel.ID " +
                    "WHERE rh.ID IS NULL AND Nivel.IDTipoNivel = @IDTipoNivel";
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["Nivel"], suffix);
				da.Fill(dataSet, "Nivel");
				
                suffix = "INNER JOIN Nivel ON Nivel.ID = NivelDesignado.ID " + suffix;
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["NivelDesignado"], suffix);
				da.Fill(dataSet, "NivelDesignado");

                command.CommandText =
                    "SELECT n.ID " +
                    "FROM Nivel n " +
                        "INNER JOIN NivelDesignado nd ON nd.ID = n.ID AND nd.isDeleted=@isDeleted " +
                        "LEFT JOIN RelacaoHierarquica rh ON rh.ID = n.ID AND rh.isDeleted=@isDeleted " +
                    "WHERE rh.ID IS NULL " +
                        "AND n.IDTipoNivel = @IDTipoNivel " +
                        "AND n.isDeleted = @isDeleted " +
                    "ORDER BY nd.Designacao";
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                    Ids.Add(System.Convert.ToInt64(reader.GetValue(0)));
                reader.Close();
			}

            return Ids.ToArray();
		}

		// Carrega um determinado nível a partir do seu ID
        public override void LoadNivel(long nivelID, DataSet dataSet, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@nivelID", nivelID);
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["Nivel"],
                    "WHERE ID=@nivelID");
				da.Fill(dataSet, "Nivel");
			}
		}
        public override void LoadDesignacaoNivel(long nivelID, DataSet dataSet, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@nivelID", nivelID);
                command.Parameters.AddWithValue("@IDTipoControloAutForma", 1);
				// Caso se trate de um nível designado
                string suffix = "WHERE ID=@nivelID";
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["NivelDesignado"], suffix);
				da.Fill(dataSet, "NivelDesignado");

				// Caso se trate de um nivel controlado
                suffix = "INNER JOIN NivelControloAut nca ON nca.IDControloAut = ControloAut.ID WHERE nca.ID = @nivelID";
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["ControloAut"], suffix);
				da.Fill(dataSet, "ControloAut");

                suffix = "WHERE ID = @nivelID";
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["NivelControloAut"], suffix);
				da.Fill(dataSet, "NivelControloAut");

                suffix = string.Format("INNER JOIN ControloAutDicionario ON ControloAutDicionario.IDDicionario = Dicionario.ID INNER JOIN NivelControloAut nca ON nca.IDControloAut = ControloAutDicionario.IDControloAut WHERE nca.ID = @nivelID AND ControloAutDicionario.IDTipoControloAutForma = @IDTipoControloAutForma", nivelID);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["Dicionario"], suffix);
				da.Fill(dataSet, "Dicionario");

                suffix = string.Format("INNER JOIN NivelControloAut nca ON nca.IDControloAut = ControloAutDicionario.IDControloAut WHERE nca.ID = @nivelID AND ControloAutDicionario.IDTipoControloAutForma = @IDTipoControloAutForma", nivelID);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["ControloAutDicionario"], suffix);
				da.Fill(dataSet, "ControloAutDicionario");
			}
		}

		// Carrega um determinado CA a partir do seu ID, carrega também, caso exista, o nível associado
        public override void LoadNivelByControloAut(long controloAutID, DataSet dataSet, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@controloAutID", controloAutID);
				// A própria noticia de autoridade
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["ControloAut"], 
                    "WHERE ID=@controloAutID");
				da.Fill(dataSet, "ControloAut");	
		
				// O Nivel respectivo
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["Nivel"],
                    "INNER JOIN NivelControloAut nca ON Nivel.ID=nca.ID WHERE nca.IDControloAut=@controloAutID");
				da.Fill(dataSet, "Nivel");

				// O NivelControloAut que relaciona o nivel com o CA
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["NivelControloAut"],
                    "WHERE IDControloAut=@controloAutID");
				da.Fill(dataSet, "NivelControloAut");
			}
		}
		#endregion

		#region Carregamento de níveis e notícias de autoridade descendentes
		// Carrega um determinado nível, todos os seus subordinados e as relações que os ligam. Neste processo podem ser carregadas também notícias de autoridade caso seja especificado que devem ser carregadas também as designações
		public override void LoadNivelChildren(long nivelID, DataSet dataSet, IDbConnection connection) {
			LoadNivelChildren(nivelID, false, dataSet, connection);
		}
        public override void LoadNivelChildren(long nivelID, bool alsoLoadDesignacoes, DataSet dataSet, IDbConnection conn)
        {
			// O próprio nível
			LoadNivel(nivelID, dataSet, conn);
            FRDRule.Current.LoadFRD(dataSet, nivelID, conn);

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@nivelID", nivelID);

				// Os níveis seus subordinados 
				var tnFilter = alsoLoadDesignacoes ? " != 4" : "  NOT IN (3, 4)";
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["Nivel"],
                    "INNER JOIN RelacaoHierarquica rh ON rh.ID = Nivel.ID WHERE rh.IDUpper = @nivelID AND Nivel.IDTipoNivel " + tnFilter);
				da.Fill(dataSet, "Nivel");

				// As relações entre o nível e os seus subordinados
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["RelacaoHierarquica"], 
                    "INNER JOIN Nivel ON Nivel.ID = RelacaoHierarquica.ID " +
                    "WHERE RelacaoHierarquica.IDUpper=@nivelID" + 
					" AND Nivel.IDTipoNivel " + tnFilter);
				da.Fill(dataSet, "RelacaoHierarquica");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["FRDBase"],
                    "INNER JOIN Nivel n ON n.ID = FRDBase.IDNivel AND n.isDeleted=@isDeleted " +
                    "INNER JOIN RelacaoHierarquica rh ON rh.ID = n.ID AND rh.isDeleted=@isDeleted " + 
                    "WHERE rh.IDUpper = @nivelID AND n.IDTipoNivel " + tnFilter);
                da.Fill(dataSet, "FRDBase");
			}
		}

		// Carrega um determinado CA bem como todos os CAs que lhe sejam subordinados por meio de relações hierarquicas
        public override void LoadControloAutChildren(long controloAutID, DataSet dataSet, IDbConnection conn)
        {
            LoadControloAutChildren(controloAutID, true, dataSet, conn);
		}
        public override void LoadControloAutChildren(long controloAutID, bool alsoLoadNiveis, DataSet dataSet, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@controloAutID", controloAutID);
				
                // A própria notícia de autoridade
                string suffix = "WHERE ID=@controloAutID";
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["ControloAut"], suffix); 
				da.Fill(dataSet, "ControloAut");
				// As notícias de autoridade subordinadas
				string CAQueryFilter;
				CAQueryFilter = 
					"INNER JOIN NivelControloAut nca ON {0} = nca.IDControloAut " + 
					"INNER JOIN RelacaoHierarquica rh ON rh.ID = nca.ID " + 
					"INNER JOIN NivelControloAut ncaUpper ON ncaUpper.ID = rh.IDUpper " +
                    "WHERE ncaUpper.IDControloAut = @controloAutID";
				string DicionarioQueryFilter;
				DicionarioQueryFilter = 
					"INNER JOIN ControloAutDicionario cad ON cad.IDDicionario = Dicionario.ID " + 
					"INNER JOIN NivelControloAut nca ON cad.IDControloAut = nca.IDControloAut " + 
					"INNER JOIN RelacaoHierarquica rh ON rh.ID = nca.ID " + 
					"INNER JOIN NivelControloAut ncaUpper ON ncaUpper.ID = rh.IDUpper " +
                    "WHERE ncaUpper.IDControloAut = @controloAutID";

				suffix = string.Format(CAQueryFilter, "ControloAut.ID");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["ControloAut"], suffix); 
				da.Fill(dataSet, "ControloAut");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["Dicionario"], DicionarioQueryFilter);;
				da.Fill(dataSet, "Dicionario");

				suffix = string.Format(CAQueryFilter, "ControloAutDicionario.IDControloAut");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["ControloAutDicionario"], suffix); 
				da.Fill(dataSet, "ControloAutDicionario");
			
				if (alsoLoadNiveis) {
                    LoadNivelChildren(GetNivelID(controloAutID, conn), dataSet, conn);
				}
			}
		}

		// Carrega as associações entre determinado CA e o seu Nível 
		// bem como as associações do mesmo tipo que existam em eventuais 
		// CAs subordinados.
		// Parte do principio que quer os CAs quer os Níveis envolvidos 
		// se encontram já carregados
        public override void LoadNivelControloAutChildrenByCA(long controloAutID, DataSet dataSet, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@controloAutID", controloAutID);

				// O NivelControloAut do próprio CA
                string suffix = "WHERE IDControloAut=@controloAutID";
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["NivelControloAut"], suffix); 
				da.Fill(dataSet, "NivelControloAut");

				// Os NivelControloAuts dos CAs subordinados
				suffix = 
					"INNER JOIN RelacaoHierarquica rh ON rh.ID = NivelControloAut.ID " + 
					"INNER JOIN NivelControloAut ncaUpper ON ncaUpper.ID = rh.IDUpper " +
                    "WHERE ncaUpper.IDControloAut = @controloAutID";
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["NivelControloAut"], suffix);
				da.Fill(dataSet, "NivelControloAut");
			}
		}
		#endregion

		#region Carregamento de níveis e notícias de autoridade ascendentes
		// Carrega um determinado nível bem como todos os seus pais
        public override void LoadNivelParents(long nivelID, DataSet dataSet, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@nivelID", nivelID);
				// O próprio nível
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["Nivel"], 
                    "WHERE ID=@nivelID");
				da.Fill(dataSet, "Nivel");

				// Os níveis seus superiores 
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["Nivel"], 
                    "INNER JOIN RelacaoHierarquica rh ON rh.IDUpper = Nivel.ID WHERE rh.ID=@nivelID");
				da.Fill(dataSet, "Nivel");

				// As relações entre o nível e os seus superiores
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["RelacaoHierarquica"],
                    "WHERE ID=@nivelID");
				da.Fill(dataSet, "RelacaoHierarquica");
			}
		}

		// Carrega um determinado nível bem como todos os seus pais e avós
        public override void LoadNivelGrandparents(long nivelID, DataSet dataSet, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@nivelID", nivelID);
				// O próprio nível
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["Nivel"],
                    "WHERE ID=@nivelID");
				da.Fill(dataSet, "Nivel");

				// Os níveis pais
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["Nivel"], 
                    "INNER JOIN RelacaoHierarquica rh ON rh.IDUpper = Nivel.ID WHERE rh.ID = ");
				da.Fill(dataSet, "Nivel");

				// Os níveis avós
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["Nivel"],
                    "INNER JOIN RelacaoHierarquica rhPai ON rhPai.IDUpper = Nivel.ID INNER JOIN RelacaoHierarquica rh ON rh.IDUpper = rhPai.ID WHERE rh.ID = @nivelID");
				da.Fill(dataSet, "Nivel");

				// As relações entre o nível e os seus pais
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["RelacaoHierarquica"],
                    "WHERE ID=@nivelID");
				da.Fill(dataSet, "RelacaoHierarquica");

				// As relações entre os níveis pais e os níveis avós
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["RelacaoHierarquica"],
                    "INNER JOIN RelacaoHierarquica rh ON rh.IDUpper = RelacaoHierarquica.ID WHERE rh.ID=@nivelID");
				da.Fill(dataSet, "RelacaoHierarquica");
			}
		}

		public override void LoadControloAutParents(long controloAutID, DataSet dataSet, IDbConnection connection) {
			LoadControloAutParents(controloAutID, true, dataSet, connection);
		}

		// Carrega um determinado CA bem como todos os CAs que lhe sejam subordinados por meio de relações hierarquicas superiores
        public override void LoadControloAutParents(long controloAutID, bool alsoLoadNiveis, DataSet dataSet, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@controloAutID", controloAutID);
				// A porópria notícia de autoridade
                string suffix = "WHERE ID=@controloAutID";
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["ControloAut"], suffix);
				da.Fill(dataSet, "ControloAut");

				// As notícias de autoridade superiores
				string CAQueryFilter =
					"INNER JOIN NivelControloAut ncaUpper ON {0} = ncaUpper.IDControloAut " + 
					"INNER JOIN RelacaoHierarquica rh ON rh.IDUpper = ncaUpper.ID " + 
					"INNER JOIN NivelControloAut nca ON nca.ID = rh.ID " +
                    "WHERE nca.IDControloAut = @controloAutID";

				string DicionarioQueryFilter = 
					string.Format("INNER JOIN ControloAutDicionario cad ON cad.IDDicionario = Dicionario.ID " + 
					"INNER JOIN NivelControloAut ncaUpper ON cad.IDControloAut = ncaUpper.IDControloAut " + 
					"INNER JOIN RelacaoHierarquica rh ON rh.IDUpper = ncaUpper.ID " + 
					"INNER JOIN NivelControloAut nca ON nca.ID = rh.ID " +
                    "WHERE nca.IDControloAut = @controloAutID", controloAutID);
				
				suffix = string.Format(CAQueryFilter, "ControloAut.ID");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["ControloAut"], suffix);
				da.Fill(dataSet, "ControloAut");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["Dicionario"], DicionarioQueryFilter);
				da.Fill(dataSet, "Dicionario");

				suffix = string.Format(CAQueryFilter, "ControloAutDicionario.IDControloAut");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["ControloAutDicionario"], suffix);
				da.Fill(dataSet, "ControloAutDicionario");

				if (alsoLoadNiveis) {
                    LoadNivelParents(GetNivelID(controloAutID, conn), dataSet, conn);
				}
			}
		}

		// Carrega as associações entre determinado CA e o seu Nível 
		// bem como as associações do mesmo tipo que existam em eventuais 
		// CAs superiores.
		// Parte do principio que quer os CAs quer os Níveis envolvidos 
		// se encontram já carregados
        public override void LoadNivelControloAutParentsByCA(long controloAutID, DataSet dataSet, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@controloAutID", controloAutID);
				// O NivelControloAut do próprio CA
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["NivelControloAut"],
                    "WHERE IDControloAut=@controloAutID");
				da.Fill(dataSet, "NivelControloAut");

				// Os NivelControloAuts dos CAs superiores
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["NivelControloAut"],
                    "INNER JOIN RelacaoHierarquica rh ON rh.IDUpper = NivelControloAut.ID " +
                    "INNER JOIN NivelControloAut nca ON nca.ID = rh.ID " +
                    "WHERE nca.IDControloAut = @controloAutID");
				da.Fill(dataSet, "NivelControloAut");
			}
		}

		// Carrega as associações entre determinado Nível e o seu CA 
		// bem como as associações do mesmo tipo que existam em eventuais 
		// Níveis superiores
		// Parte do principio que quer os CAs quer os Níveis envolvidos 
		// se encontram já carregados
        public override void LoadNivelControloAutParentsByNivel(long nivelID, DataSet dataSet, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@nivelID", nivelID);
				// O NivelControloAut do próprio Nível
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["NivelControloAut"],
                    "WHERE ID=@nivelID");
				da.Fill(dataSet, "NivelControloAut");

				// Os NivelControloAuts dos Níveis subordinados
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(dataSet.Tables["NivelControloAut"],
                    "INNER JOIN RelacaoHierarquica rh ON rh.IDUpper = NivelControloAut.ID " + 
                    "WHERE rh.ID = @nivelID");
				da.Fill(dataSet, "NivelControloAut");
			}
		}
		#endregion

		#region Obtenção de documentos e unidades físicas para efeitos de avaliação
        public override void GetUfsEDocsAssociados(long nivelId, long userID, IDbConnection conn)
        {
            long ticks = DateTime.Now.Ticks;
            UfsDocsAssoc = new Dictionary<long, List<long>>();
            UfsSeriesAssoc = new Dictionary<long, List<long>>();
            UfsAssoc = new OrderedDictionary();
            DocsAssoc = new OrderedDictionary();

            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.CommandText =
                    "CREATE TABLE #TempRelacaoHierarquica (ID BIGINT, IDUpper BIGINT, IDTipoNivelRelacionado BIGINT) " +
                    "CREATE TABLE #UFRelated (IDUF BIGINT, IDNDoc BIGINT, IsNivelDoc TINYINT) " +
                    "CREATE INDEX iduf_ix ON #UFRelated (IDUF)";
                command.ExecuteNonQuery();

                command.CommandText = "sp_getUFsEDocsAssociados";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@nivelID", SqlDbType.BigInt);
                command.Parameters.Add("@TrusteeID", SqlDbType.BigInt);
                command.Parameters[0].Value = nivelId;
                command.Parameters[1].Value = userID;
                SqlDataReader reader = command.ExecuteReader();

                long item;
                long nivelID;
                while (reader.Read())
                {
                    item = System.Convert.ToInt64(reader.GetValue(0));
                    nivelID = System.Convert.ToInt64(reader.GetValue(1));
                    if (System.Convert.ToInt64(reader.GetValue(2)) == 1)
                    {
                        if (UfsSeriesAssoc.ContainsKey(item))
                            UfsSeriesAssoc[item].Add(nivelID);
                        else
                            UfsSeriesAssoc.Add(item, new List<long>() { nivelID });
                    }
                    else
                    {
                        if (UfsDocsAssoc.ContainsKey(item))
                            UfsDocsAssoc[item].Add(nivelID);
                        else
                            UfsDocsAssoc.Add(item, new List<long>() { nivelID });
                    }
                }
                reader.NextResult();

                while (reader.Read())
                {
                    item = System.Convert.ToInt64(reader.GetValue(0));
                    if (!UfsAssoc.Contains(item))
                    {
                        UnidadeFisicaAssociada uf = new UnidadeFisicaAssociada();
                        uf.IDNivel = item;
                        uf.Codigo = reader.GetValue(1).ToString();
                        uf.Designacao = reader.GetValue(2).ToString();
                        uf.FimAno = reader.GetValue(3).ToString();
                        uf.FimMes = reader.GetValue(4).ToString();
                        uf.FimDia = reader.GetValue(5).ToString();
                        uf.InicioAno = reader.GetValue(6).ToString();
                        uf.InicioMes = reader.GetValue(7).ToString();
                        uf.InicioDia = reader.GetValue(8).ToString();
                        uf.IsNotDocRelated = System.Convert.ToBoolean(reader.GetValue(9));
                        uf.IsSerieRelated = System.Convert.ToBoolean(reader.GetValue(10));
                        UfsAssoc.Add(item, uf);
                    }
                }
                reader.NextResult();

                while (reader.Read())
                {
                    item = System.Convert.ToInt64(reader.GetValue(0));
                    if (!DocsAssoc.Contains(item))
                    {
                        DocumentoAssociado doc = new DocumentoAssociado();
                        doc.IDNivel = item;
                        doc.IDNivelUpper = System.Convert.ToInt64(reader.GetValue(1));
                        doc.IDTipoNivelRelacionado = System.Convert.ToInt32(reader.GetValue(2));
                        doc.IDFRD = System.Convert.ToInt64(reader.GetValue(3));
                        doc.FimAno = reader.GetValue(4).ToString();
                        doc.FimMes = reader.GetValue(5).ToString();
                        doc.FimDia = reader.GetValue(6).ToString();
                        doc.InicioAno = reader.GetValue(7).ToString();
                        doc.InicioMes = reader.GetValue(8).ToString();
                        doc.InicioDia = reader.GetValue(9).ToString();
                        doc.Codigo = reader.GetValue(10).ToString();
                        doc.DesignacaoUpper = reader.GetValue(11).ToString();
                        doc.Designacao = reader.GetValue(12).ToString();
                        if (reader.GetValue(13) == DBNull.Value)
                            doc.Preservar = string.Empty;
                        else if (System.Convert.ToBoolean(reader.GetValue(13)))
                            doc.Preservar = "1";
                        else
                            doc.Preservar = "0";
                        doc.IDAutoEliminacao = reader.GetValue(14).ToString();
                        doc.Expirado = reader.GetValue(15).ToString();
                        doc.PermEscrever = reader.IsDBNull(16) ? false : System.Convert.ToBoolean(reader.GetValue(16));
                        DocsAssoc.Add(item, doc);
                    }
                }

                reader.Close();
            }
            Trace.WriteLine("<<sp_getUFsEDocsAssociados>>: " + new TimeSpan(DateTime.Now.Ticks - ticks).ToString());
        }
        #endregion

		#region Publicação
		public override DataRow[] SelectFRDBase (DataSet dataSet, long nRowID, int TipoFRDBase) {
			string query = string.Format("IDNivel={0} AND IDTipoFRDBase={1:d}", nRowID, TipoFRDBase);
			return dataSet.Tables["FRDBase"].Select(query);
		}

		public override void ExecutePublishNivel(long NivelID, IDbTransaction tran) {
			SqlCommand command = new SqlCommand("sp_publishNivel", (SqlConnection) tran.Connection, (SqlTransaction) tran);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@IDNivel", SqlDbType.BigInt);
			command.Parameters[0].Value = NivelID;
			
			try {
				command.ExecuteNonQuery();
			} 
			catch (Exception ex) {
				Trace.WriteLine(ex);
				throw;
			} 			
		}

        public override List<string> ExecutePublishSubDocumentos(List<PublicacaoDocumentos> DocsID, long IDTrustee, IDbTransaction tran) 
		{
            var idsToUpdate = new HashSet<string>(); // estrutura que vai ter a lista de IDs de niveis a serem atualizados no servidor de pesquisa
			StringBuilder cmd = new StringBuilder("CREATE TABLE #NiveisDoc (ID BIGINT, Publicar BIT); ");
			foreach (PublicacaoDocumentos doc in DocsID)
			{
				cmd.AppendFormat("INSERT INTO #NiveisDoc VALUES ({0}, {1}); ", doc.IDNivelDoc, System.Convert.ToSByte(doc.Publicar));
                idsToUpdate.Add(doc.IDNivelDoc.ToString());
			}
			SqlCommand command = new SqlCommand(cmd.ToString(), (SqlConnection) tran.Connection, (SqlTransaction) tran);
			command.ExecuteNonQuery();

			command.CommandText = "sp_publishSubDocumentos";
			command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@IDTrustee", SqlDbType.BigInt);
            command.Parameters[0].Value = IDTrustee;
			
			try 
			{
				command.ExecuteNonQuery();
			} 
			catch (Exception ex) 
			{
				Trace.WriteLine(ex);
				throw;
            }

            command.CommandText = "SELECT rh.ID FROM RelacaoHierarquica rh INNER JOIN #NiveisDoc ON #NiveisDoc.ID = rh.IDUpper WHERE rh.isDeleted = 0 ";
            command.CommandType = CommandType.Text;
            var reader = command.ExecuteReader();

            while (reader.Read())
                idsToUpdate.Add(reader.GetValue(0).ToString());

            reader.Close();

			command.CommandText = "DROP TABLE #NiveisDoc";
			command.CommandType = CommandType.Text;
			command.ExecuteNonQuery();

            var res = new List<string>();
            res.AddRange(idsToUpdate);

            return res;
		}
		#endregion

        public override long GetNivelLastIDTipoNivelRelacionado(long frdID, IDbConnection conn)
        {
            long lastIDTipoNivelRelacionado;
            using (SqlCommand command = new SqlCommand("SELECT TOP 1 IDTipoNivelRelacionado FROM FRDBaseDataDeDescricao WHERE IDFRDBase = @frdID ORDER BY DataEdicao DESC", (SqlConnection)conn))
            {
                command.Parameters.AddWithValue("@frdID", frdID);
                try
                {
                    lastIDTipoNivelRelacionado = System.Convert.ToInt64(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                    throw;
                }
            }
            return lastIDTipoNivelRelacionado;
        }

		public override long GetNivelID(long controloAutID, IDbConnection conn) {
			long nivelID;
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID FROM NivelControloAut WHERE IDControloAut = @controloAutID AND isDeleted = @isDeleted", controloAutID), (SqlConnection)conn))
            {
                command.Parameters.AddWithValue("@controloAutID", controloAutID);
                command.Parameters.AddWithValue("@isDeleted", 0);
                try
                {
                    nivelID = System.Convert.ToInt64(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                    throw;
                }
            }
			return nivelID;
		}

		public override void FillNivelDesignado (DataSet CurrentDataSet, long CurrentNivelID, IDbConnection conn) {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@CurrentNivelID", CurrentNivelID);
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["NivelDesignado"],
                    "WHERE ID = @CurrentNivelID");
				da.Fill(CurrentDataSet, "NivelDesignado");				
			}
		}

		public override void FillNivelControloAutRows(DataSet CurrentDataSet, long CurrentNivelID, IDbConnection conn) {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@CurrentNivelID", CurrentNivelID);

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["ControloAut"], 
                    "WHERE ID IN (SELECT IDControloAut FROM NivelControloAut WHERE ID=@CurrentNivelID)");
				da.Fill(CurrentDataSet, "ControloAut");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["NivelControloAut"], "WHERE ID=@CurrentNivelID");
				da.Fill(CurrentDataSet, "NivelControloAut");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["Dicionario"], 
                    "WHERE ID IN (SELECT IDDicionario FROM ControloAutDicionario WHERE IDControloAut IN (SELECT IDControloAut FROM NivelControloAut WHERE ID=@CurrentNivelID))");
				da.Fill(CurrentDataSet, "Dicionario");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["ControloAutDicionario"], 
                    "WHERE IDControloAut IN (SELECT IDControloAut FROM NivelControloAut WHERE ID=@CurrentNivelID)");
				da.Fill(CurrentDataSet, "ControloAutDicionario");			
			}
		}

		public override void FillTipoNivelRelacionadoCodigo(DataSet CurrentDataSet, IDbConnection conn) {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoNivelRelacionadoCodigo"], " WITH (UPDLOCK) ");
				da.Fill(CurrentDataSet, "TipoNivelRelacionadoCodigo");
			}
		}

		public override void FillTipoNivelRelacionadoCodigo(DataSet CurrentDataSet, IDbTransaction transaction) {
            using (SqlCommand command = new SqlCommand("", (SqlConnection)transaction.Connection, (SqlTransaction)transaction))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@isDeleted", 0);
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoNivelRelacionadoCodigo"], " WITH (UPDLOCK) ");
				da.Fill(CurrentDataSet, "TipoNivelRelacionadoCodigo");
			}
		}

		#region MasterPanelAdminGlobal
		public override long GetNivelEstruturalCount(IDbConnection conn) {
			SqlCommand command = new SqlCommand("sp_getNivelEstruturalCount", (SqlConnection) conn);
			command.CommandType = CommandType.StoredProcedure;			
			return System.Convert.ToInt64(command.ExecuteScalar());
		}

		public override void LoadModelosAvaliacao(DataSet CurrentDataSet, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["ListaModelosAvaliacao"]);
				da.Fill(CurrentDataSet, "ListaModelosAvaliacao");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["ModelosAvaliacao"]);
				da.Fill(CurrentDataSet, "ModelosAvaliacao");
			}
		}

		public override void ClearAvaliacaoTabelaSeries(DateTime data, IDbConnection connection)
		{
			SqlCommand command = new SqlCommand(string.Empty, (SqlConnection) connection);
			command.CommandText = "sp_clearAvaliacaoTabela";
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@data", SqlDbType.DateTime).Value = data;
			command.ExecuteNonQuery();
		}

		public override bool ManageModelosAvaliacao(bool Operacao, long IDModeloAvaliacao, string Designacao, short PrazoConservacao, bool Preservar, IDbConnection connection)
		{
			SqlCommand command = new SqlCommand(string.Empty, (SqlConnection) connection);
			command.CommandText = "sp_manageModelosAvaliacao";
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@Operation", SqlDbType.Bit).Value = Operacao;
			command.Parameters.Add("@IDModeloAvaliacao", SqlDbType.BigInt).Value = IDModeloAvaliacao;
			command.Parameters.Add("@Designacao", SqlDbType.NVarChar).Value = Designacao;
			command.Parameters.Add("@PrazoConservacao", SqlDbType.SmallInt).Value = PrazoConservacao;
			command.Parameters.Add("@Preservar", SqlDbType.Bit).Value = Preservar;
			SqlDataReader dataReader = command.ExecuteReader();
			dataReader.Read();
			bool result = System.Convert.ToBoolean(dataReader.GetValue(0));
			dataReader.Close();

			return result;
		}

		public override bool ManageListasModelosAvaliacao(bool Operacao, long IDListaModeloAvaliacao, string Designacao, DateTime DataInicio, IDbConnection connection)
		{
			SqlCommand command = new SqlCommand(string.Empty, (SqlConnection) connection);
			command.CommandText = "sp_manageListaModelosAvaliacao";
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@Operation", SqlDbType.Bit).Value = Operacao;
			command.Parameters.Add("@IDListaModelosAvaliacao", SqlDbType.BigInt).Value = IDListaModeloAvaliacao;
			command.Parameters.Add("@Designacao", SqlDbType.NVarChar).Value = Designacao;
			command.Parameters.Add("@DataInicio", SqlDbType.DateTime).Value = DataInicio;			
			SqlDataReader dataReader = command.ExecuteReader();
			dataReader.Read();
			bool result = System.Convert.ToBoolean(dataReader.GetValue(0));
			dataReader.Close();

			return result;
		}
		#endregion

		#region MasterPanelSeries
		public override ArrayList ExpandTreeView(long NivelID, long ExceptTipoNivel, IDbConnection conn)
		{
			SqlCommand command = new SqlCommand(string.Empty, (SqlConnection) conn);
			command.CommandText = 
				"SELECT rh.ID, n, COALESCE(nd.Designacao, d.Termo), rh.IDTipoNivelRelacionado, tnr.GUIOrder " +
				"FROM Nivel " +
				"INNER JOIN RelacaoHierarquica rh ON rh.IDUpper = n.ID " +
				"INNER JOIN TipoNivelRelacionado tnr ON tnr.ID = rh.IDTipoNivelRelacionado " +
				"LEFT JOIN NivelDesignado nd ON nd.ID = n.ID " +
				"LEFT JOIN NivelControloAut nca ON nca.ID = n.ID " +
				"LEFT JOIN ControloAut ca ON ca.ID = nca.IDControloAut " +
				"LEFT JOIN ControloAutDatasExistencia cade ON cade.IDControloAut = ca.ID " +
				"LEFT JOIN ControloAutDicionario cad ON cad.IDControloAut = ca.ID " +
				"LEFT JOIN Dicionario d ON d.ID = cad.IDDicionario " +
				"LEFT JOIN FRDBase frd ON frd.IDNivel = n.ID " +
				"LEFT JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frd.ID " +
				"WHERE " +
				"n.ID = " + NivelID.ToString() + " AND " +
				"rh.IDTipoNivelRelacionado != 11 AND " +
				"(cad.IDTipoControloAutForma IS NULL OR cad.IDTipoControloAutForma = 1)  AND -- se se tratar de um nível controlado, obter apenas a forma autorizada " +
				"(cad.IDDicionario IS NULL OR cad.isDeleted = 0)  AND " +
				"(frd.IDTipoFRDBase IS NULL OR frd.IDTipoFRDBase = 1) AND " +
				"rh.isDeleted = 0 AND " +
				"n.isDeleted = 0 " +
				"ORDER BY " +
				"rh.IDTipoNivelRelacionado, " +
				"CASE WHEN rh.IDTipoNivelRelacionado = 9 THEN n.Codigo ELSE NULL END, " +
				"CASE WHEN rh.IDTipoNivelRelacionado = 9 THEN NULL WHEN rh.IDTipoNivelRelacionado = 3 THEN fn_AddPaddingToDateMember_new(cade.InicioAno, 4) ELSE fn_AddPaddingToDateMember_new(case when nUpper.IDTipoNivel = 2 then rh.InicioAno else dp.InicioAno end , 4) END, " +
				"CASE WHEN rh.IDTipoNivelRelacionado = 9 THEN NULL WHEN rh.IDTipoNivelRelacionado = 3 THEN fn_AddPaddingToDateMember_new(cade.InicioMes, 2) ELSE fn_AddPaddingToDateMember_new(case when nUpper.IDTipoNivel = 2 then rh.InicioMes else dp.InicioMes end , 2) END, " +
				"CASE WHEN rh.IDTipoNivelRelacionado = 9 THEN NULL WHEN rh.IDTipoNivelRelacionado = 3 THEN fn_AddPaddingToDateMember_new(cade.InicioDia, 2) ELSE fn_AddPaddingToDateMember_new(case when nUpper.IDTipoNivel = 2 then rh.InicioDia else dp.InicioDia end , 2) END, " +
				"CASE WHEN rh.IDTipoNivelRelacionado = 9 THEN NULL WHEN rh.IDTipoNivelRelacionado = 3 THEN fn_AddPaddingToDateMember_new(cade.FimAno, 4) ELSE fn_AddPaddingToDateMember_new(case when nUpper.IDTipoNivel = 2 then rh.FimAno else dp.FimAno end , 4) END, " +
				"CASE WHEN rh.IDTipoNivelRelacionado = 9 THEN NULL WHEN rh.IDTipoNivelRelacionado = 3 THEN fn_AddPaddingToDateMember_new(cade.FimMes, 2) ELSE fn_AddPaddingToDateMember_new(case when nUpper.IDTipoNivel = 2 then rh.FimMes else dp.FimMes end , 2) END, " +
				"CASE WHEN rh.IDTipoNivelRelacionado = 9 THEN NULL WHEN rh.IDTipoNivelRelacionado = 3 THEN fn_AddPaddingToDateMember_new(cade.FimDia, 2) ELSE fn_AddPaddingToDateMember_new(case when nUpper.IDTipoNivel = 2 then rh.FimDia else dp.FimDia end , 2) END, " +
				"COALESCE(nd.Designacao, d.Termo)";
			SqlDataReader reader = command.ExecuteReader();

			ArrayList result = new ArrayList();
			ExpandNivelStruct nivel;
			while (reader.Read())
			{
				nivel = new ExpandNivelStruct();
				nivel.IDNivel = System.Convert.ToInt64(reader.GetValue(0));
				nivel.Desigancao = reader.GetValue(1).ToString();
				nivel.IDTipoNivelDesignado = System.Convert.ToInt64(reader.GetValue(2));
				nivel.GUIOrder = System.Convert.ToInt32(reader.GetValue(3));
				result.Add(nivel);
			}
			reader.Close();

			return result;
		}

		public override void LoadDataBeforeExpand(DataSet currentDataSet, long NivelID, long ExceptTipoNivel, IDbConnection conn)
		{
			using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@NivelID", NivelID);
                command.Parameters.AddWithValue("@ExceptTipoNivel", ExceptTipoNivel);
				string ConstraintNivel =
                    "INNER JOIN RelacaoHierarquica rh ON rh.ID = Nivel.ID WHERE rh.IDUpper=@NivelID" +
                    " AND Nivel.IDTipoNivel != @ExceptTipoNivel";
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"], ConstraintNivel);
				da.Fill(currentDataSet, "Nivel");

				string Constraint = 
					"INNER JOIN Nivel ON Nivel.ID = NivelDesignado.ID " + ConstraintNivel;
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"], Constraint);
				da.Fill(currentDataSet, "NivelDesignado");
				
				Constraint = 
					"INNER JOIN Nivel ON Nivel.ID = RelacaoHierarquica.ID " +
                    "WHERE RelacaoHierarquica.IDUpper=@NivelID" +
                    " AND Nivel.IDTipoNivel != @ExceptTipoNivel";
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"], Constraint);
				da.Fill(currentDataSet, "RelacaoHierarquica");
			}			
		}

		public override bool isNivelDeleted(long nivelID, IDbTransaction tran) {
			int count;
			string cmd = 
				string.Format("SELECT COUNT(*) FROM Nivel WITH (UPDLOCK) WHERE ID={0} AND isDeleted = 0", nivelID.ToString());

			SqlCommand command = new SqlCommand(cmd, (SqlConnection) tran.Connection, (SqlTransaction) tran);
			SqlDataReader dr = command.ExecuteReader();
			dr.Read();
			count = ((int)(dr.GetValue(0)));
			dr.Close();

			return count > 0;
		}

        public override void LoadNivelRelacoesHierarquicas(DataSet currentDataSet, List<long> IDNivel, IDbConnection conn)
        {
            GisaDataSetHelperRule.ImportIDs(IDNivel.ToArray(), conn);

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "INNER JOIN RelacaoHierarquica ON RelacaoHierarquica.IDUpper = Nivel.ID " +
                    "INNER JOIN #temp ON #temp.ID = RelacaoHierarquica.ID");
                da.Fill(currentDataSet, "Nivel");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                    "INNER JOIN #temp ON #temp.ID = RelacaoHierarquica.ID");
                da.Fill(currentDataSet, "RelacaoHierarquica");
            }
        }

		#endregion

		#region MasterPanelUnidadesFisicas
		public override void LoadUFsRelatedData(DataSet currentDataSet, IDbConnection conn) {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@IDTipoNivel", 1);

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                    "LEFT JOIN RelacaoHierarquica ON Nivel.ID=RelacaoHierarquica.ID WHERE RelacaoHierarquica.ID IS NULL AND Nivel.IDTipoNivel = @IDTipoNivel");
				da.Fill(currentDataSet, "Nivel");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"],
                    "INNER JOIN Nivel ON Nivel.ID=NivelDesignado.ID LEFT JOIN RelacaoHierarquica ON RelacaoHierarquica.ID=Nivel.ID WHERE RelacaoHierarquica.ID IS NULL  AND Nivel.IDTipoNivel = @IDTipoNivel");
				da.Fill(currentDataSet, "NivelDesignado");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelUnidadeFisicaCodigo"]);
				da.Fill(currentDataSet, "NivelUnidadeFisicaCodigo");
			}
		}

		public override IDataReader GetUFReport(long NivelID, IDbConnection conn) {
			string cmdText =
				"SELECT DISTINCT rh.ID, nivel.IDTipoNivel, tnr.Designacao, " +
                    "CASE WHEN nivel.CatCode = 'NVL' THEN niveld.Designacao ELSE d.Termo END Designacao, FRDBase.ID, tnr.ID " +
				"FROM Nivel niveluf " +
				    "INNER JOIN SFRDUnidadeFisica ON SFRDUnidadeFisica.IDNivel = niveluf.ID " +
				    "INNER JOIN FRDBase ON FRDBase.ID = SFRDUnidadeFisica.IDFRDBase " + 
				    "INNER JOIN Nivel nivel ON nivel.ID = FRDBase.IDNivel " + 
				    "LEFT JOIN NivelDesignado niveld ON niveld.ID = nivel.ID " +
				    "LEFT JOIN NivelControloAut nca ON nca.ID = nivel.ID " +
				    "LEFT JOIN ControloAut ca ON ca.ID = nca.IDControloAut " +
				    "LEFT JOIN ControloAutDicionario cad ON cad.IDControloAut = ca.ID " +
				    "LEFT JOIN TipoControloAutForma tcaf ON tcaf.ID = cad.IDTipoControloAutForma " +
				    "LEFT JOIN Dicionario d ON d.ID = cad.IDDicionario " +
				    "INNER JOIN RelacaoHierarquica rh ON rh.ID = nivel.ID " +
				    "INNER JOIN TipoNivelRelacionado tnr ON tnr.ID = rh.IDTipoNivelRelacionado " +
                "WHERE niveluf.ID = @NivelID AND " +
                    "(tcaf.ID IS NULL OR tcaf.ID = @tcafID) " +
                    "AND niveluf.isDeleted = @isDeleted " +
                    "AND SFRDUnidadeFisica.isDeleted = @isDeleted " +
                    "AND FRDBase.isDeleted = @isDeleted " +
                    "AND nivel.isDeleted = @isDeleted " +
                    "AND (niveld.isDeleted IS NULL OR niveld.isDeleted = @isDeleted) " +
                    "AND (nca.isDeleted IS NULL OR nca.isDeleted = @isDeleted) " +
                    "AND (ca.isDeleted IS NULL OR ca.isDeleted = @isDeleted) " +
                    "AND (cad.isDeleted IS NULL OR cad.isDeleted = @isDeleted) " +
                    "AND (tcaf.isDeleted IS NULL OR tcaf.isDeleted = @isDeleted) " +
                    "AND (d.isDeleted IS NULL OR d.isDeleted = @isDeleted) " +
                    "AND rh.isDeleted = @isDeleted " +
                    "AND tnr.isDeleted = @isDeleted ";

			SqlCommand command = new SqlCommand(cmdText, (SqlConnection) conn);
            command.Parameters.AddWithValue("@isDeleted", 0);
            command.Parameters.AddWithValue("@tcafID", 1);
            command.Parameters.AddWithValue("@NivelID", NivelID);
			SqlDataReader reader = command.ExecuteReader();
			return reader;
		}
		#endregion

        #region MasterPanelFedora
        public override long GetDocNextGUIOrder(long DocCompostoNivelID, IDbTransaction tran)
        {
            string cmd = string.Format(@"
                SELECT MAX(nds.GUIOrder)
                FROM RelacaoHierarquica rh WITH (UPDLOCK)
	                INNER JOIN NivelDocumentoSimples nds ON nds.ID = rh.ID AND nds.isDeleted = 0
                WHERE rh.isDeleted = 0 AND rh.IDUpper = {0}; 
                
                SELECT MAX(odSimples.GUIOrder)
                FROM FRDBase frd WITH (UPDLOCK)
	                INNER JOIN SFRDImagem img ON img.IDFRDBase = frd.ID AND img.Tipo = 'Fedora' AND img.isDeleted = 0
                    INNER JOIN SFRDImagemObjetoDigital imgOD ON imgOD.IDFRDBase = img.IDFRDBase AND imgOD.idx = img.idx AND imgOD.isDeleted = 0
                    INNER JOIN ObjetoDigital od ON od.ID = imgOD.IDObjetoDigital AND od.isDeleted = 0
                    INNER JOIN ObjetoDigitalRelacaoHierarquica rhod ON rhod.IDUpper = od.ID AND rhod.isDeleted = 0
                    INNER JOIN ObjetoDigital odSimples ON odSimples.ID = rhod.ID AND odSimples.isDeleted = 0
                WHERE frd.isDeleted = 0 AND frd.IDNivel = {0}; 

                SELECT MAX(od.GUIOrder)
                FROM FRDBase frd WITH (UPDLOCK)
	                INNER JOIN SFRDImagem img ON img.IDFRDBase = frd.ID AND img.Tipo = 'Fedora' AND img.isDeleted = 0
                    INNER JOIN SFRDImagemObjetoDigital imgOD ON imgOD.IDFRDBase = img.IDFRDBase AND imgOD.idx = img.idx AND imgOD.isDeleted = 0
                    INNER JOIN ObjetoDigital od ON od.ID = imgOD.IDObjetoDigital AND od.isDeleted = 0
                WHERE frd.isDeleted = 0 AND frd.IDNivel = {0}", DocCompostoNivelID);

            SqlCommand command = new SqlCommand(cmd, (SqlConnection)tran.Connection, (SqlTransaction)tran);
            SqlDataReader reader = command.ExecuteReader();

            long res = 0;
            long val = 0;
            do
            {
                if (reader.Read() && reader.GetValue(0) != null && !reader.IsDBNull(0))
                {
                    val = System.Convert.ToInt64(reader.GetValue(0));
                    res = val > res ? val : res;
                }
            } while (reader.NextResult());

            reader.Close();

            return res + 1;
        }

        public override void LoadTipoDocumento(DataSet CurrentDataSet, long CurrentNivelID, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@CurrentNivelID", CurrentNivelID);
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["NivelDocumentoSimples"],
                    "WHERE NivelDocumentoSimples.ID = @CurrentNivelID");
                da.Fill(CurrentDataSet, "NivelDocumentoSimples");
            }
        }
        #endregion

        #region NivelEstruturalList
        private const string JOIN_TITULO = "INNER JOIN NivelControloAut nca ON nca.ID = n.ID AND nca.isDeleted = 0 " +
                    "INNER JOIN ControloAut ca ON ca.ID = nca.IDControloAut AND ca.IDTipoNoticiaAut = 4 AND ca.isDeleted = 0 " +
                    "INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = ca.ID AND cad.IDTipoControloAutForma = 1 AND cad.isDeleted = 0 " +
                    "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted = 0 ";
        private const string JOIN_DATAS_PRODUCAO = "INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.IDTipoFRDBase = 1 AND frd.isDeleted = 0 " +
                    "LEFT JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frd.ID AND dp.isDeleted = 0 ";
        public override void CalculateOrderedItemsEstrutural(ArrayList ordenacao, string filtroDesignacaoLike, IDbConnection conn)
        {
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);

            StringBuilder selectQuery = new StringBuilder("SELECT n.ID ");
            string from = "FROM Nivel n ";
            StringBuilder innerJoin = new StringBuilder("INNER JOIN RelacaoHierarquica rh ON rh.ID = n.ID ");
            StringBuilder where = new StringBuilder("WHERE n.IDTipoNivel = 2 AND n.isDeleted = 0 ");
            StringBuilder orderByQuery = new StringBuilder("ORDER BY IDTipoNivelRelacionado ");

            if (filtroDesignacaoLike.Length > 0)
            {
                innerJoin.Append(JOIN_TITULO);
                where.AppendFormat(" AND {0}", filtroDesignacaoLike);
            }
            command.CommandText = "CREATE TABLE #OrderedItems ( seq_id INT IDENTITY(1, 1) NOT NULL, ID BIGINT NOT NULL ); ";
            command.ExecuteNonQuery();

            // Obter lista de IDs filtrados com o valor mínimo para o IDTipoNivelRelacionado (um nivel produtor pode ter mais do que um tipo de relacionamento)
            command.CommandText =
                "CREATE TABLE #temp(ID BIGINT, IDTipoNivelRelacionado BIGINT) " +
                "INSERT INTO #temp (ID, IDTipoNivelRelacionado) " +
                "SELECT DISTINCT n.ID, MIN(rh.IDTipoNivelRelacionado) " +
                from +
                innerJoin.ToString() +
                where.ToString() +
                "GROUP BY n.ID";
            command.ExecuteNonQuery();

            // ordenar as unidades físicas segundo o critério definido pelo utilizador
            innerJoin = new StringBuilder();
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
                    //Designação
                    case 0:
                        innerJoin.Append(JOIN_TITULO);
                        orderByQuery.AppendFormat(" d.Termo {0} ", order);
                        break;
                    //Datas produção
                    case 1:
                        innerJoin.Append(JOIN_DATAS_PRODUCAO);
                        orderByQuery.AppendFormat(@" dbo.fn_AddPaddingToDateMember_new(dp.InicioAno, 4) {0}, 
dbo.fn_AddPaddingToDateMember_new(dp.InicioMes, 2) {0}, 
dbo.fn_AddPaddingToDateMember_new(dp.InicioDia, 2) {0}, 
dbo.fn_AddPaddingToDateMember_new(dp.FimAno, 4) {0}, 
dbo.fn_AddPaddingToDateMember_new(dp.FimMes, 2) {0}, 
dbo.fn_AddPaddingToDateMember_new(dp.FimDia, 2) {0} ", order);
                        break;
                }
            }

            command.CommandText =
                "INSERT INTO #OrderedItems (ID) " +
                selectQuery.ToString() +
                "FROM #temp n " +
                innerJoin.ToString() +
                orderByQuery.ToString() + 
                ";DROP TABLE #temp";
            command.ExecuteNonQuery();
        }

        public override List<NivelDocumentalListItem> GetItemsEstrutural(DataSet currentDataSet, int pageNr, int itemsPerPage, IDbConnection conn)
        {
            List<NivelDocumentalListItem> rows = new List<NivelDocumentalListItem>();
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #ItemsID (ID BIGINT, seq_id INT)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO #ItemsID SELECT ID,seq_id FROM #OrderedItems WHERE seq_id >= @seq_id1 AND seq_id <= @seq_id2";
                command.Parameters.AddWithValue("@seq_id1", (pageNr - 1) * itemsPerPage + 1);
                command.Parameters.AddWithValue("@seq_id2", pageNr * itemsPerPage);
                command.ExecuteNonQuery();

                // Carregamento de informação dos IDs calculados e obtidos anteriormente
                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN #ItemsID ON #ItemsID.ID = Nivel.ID ");
                    da.Fill(currentDataSet, "Nivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                        "INNER JOIN #ItemsID ON #ItemsID.ID = RelacaoHierarquica.ID ");
                    da.Fill(currentDataSet, "RelacaoHierarquica");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN RelacaoHierarquica ON RelacaoHierarquica.IDUpper = Nivel.ID " +
                        "INNER JOIN #ItemsID ON #ItemsID.ID = RelacaoHierarquica.ID ");
                    da.Fill(currentDataSet, "Nivel");
                }

                command.CommandText =
                    "SELECT n.ID, d.Termo, tnr.ID, " +
                        "dp.FimAno, " +
                        "dp.FimMes, " +
                        "dp.FimDia, " +
                        "dp.InicioAno, " +
                        "dp.InicioMes, " +
                        "dp.InicioDia, " +
                        "dp.FimAtribuida, " +
                        "dp.InicioAtribuida, " +
                        "tnr.GUIOrder " +
                    "FROM Nivel n " +
                        "INNER JOIN #ItemsID ON #ItemsID.ID = n.ID " +
                        "INNER JOIN (" +
                            "SELECT rh.ID, MIN(rh.IDTipoNivelRelacionado) IDTipoNivelRelacionado " +
                            "FROM #ItemsID " +
                                "INNER JOIN RelacaoHierarquica rh ON rh.ID = #ItemsID.ID AND rh.isDeleted = @isDeleted " +
                            "GROUP BY rh.ID " +
                        ") TipoNivelRelacionado ON TipoNivelRelacionado.ID = n.ID " +
                        "INNER JOIN TipoNivelRelacionado tnr ON tnr.ID = TipoNivelRelacionado.IDTipoNivelRelacionado AND tnr.isDeleted = @isDeleted " +
                        "INNER JOIN NivelControloAut nca ON nca.ID = n.ID AND nca.isDeleted = @isDeleted " +
                        "INNER JOIN ControloAut ca ON ca.ID = nca.IDControloAut AND ca.IDTipoNoticiaAut = @IDTipoNoticiaAut AND ca.isDeleted = @isDeleted " +
                        "INNER JOIN ControloAutDicionario cad ON cad.IDControloAut = ca.ID AND cad.IDTipoControloAutForma = @IDTipoControloAutForma AND cad.isDeleted = @isDeleted " +
                        "INNER JOIN Dicionario d ON d.ID = cad.IDDicionario AND d.isDeleted = @isDeleted " +
                        "INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.IDTipoFRDBase = @IDTipoFRDBase AND frd.isDeleted = @isDeleted " +
                        "LEFT JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frd.ID AND dp.isDeleted = @isDeleted " +
                    "WHERE n.isDeleted = @isDeleted AND n.IDTipoNivel = @IDTipoNivel " +
                    "ORDER BY seq_id";
                command.Parameters.AddWithValue("@IDTipoNivel", 2);
                command.Parameters.AddWithValue("@IDTipoFRDBase", 1);
                command.Parameters.AddWithValue("@IDTipoNoticiaAut", 4);
                command.Parameters.AddWithValue("@IDTipoControloAutForma", 1);
                SqlDataReader reader = command.ExecuteReader();

                NivelDocumentalListItem row;
                while (reader.Read())
                {
                    row = new NivelDocumentalListItem();
                    row.IDNivel = reader.GetInt64(0);
                    row.Designacao = reader.GetString(1);
                    row.IDTipoNivelRelacionado = reader.GetInt64(2);
                    row.FimAno = reader.GetValue(3).ToString();
                    row.FimMes = reader.GetValue(4).ToString();
                    row.FimDia = reader.GetValue(5).ToString();
                    row.InicioAno = reader.GetValue(6).ToString();
                    row.InicioMes = reader.GetValue(7).ToString();
                    row.InicioDia = reader.GetValue(8).ToString();
                    if (!reader.IsDBNull(9)) row.FimAtribuida = reader.GetBoolean(9);
                    if (!reader.IsDBNull(10)) row.InicioAtribuida = reader.GetBoolean(10);
                    row.GUIOrder = System.Convert.ToInt32(reader.GetValue(11));
                    rows.Add(row);
                }
                reader.Close();
            }
            return rows;
        }
        #endregion

        #region NivelGrupoArquivosList
        public override void CalculateOrderedItemsGA(IDbConnection conn)
        {
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #OrderedItems ( seq_id INT IDENTITY(1, 1) NOT NULL, ID BIGINT NOT NULL ); ";
                command.ExecuteNonQuery();

                command.CommandText =
                    "INSERT INTO #OrderedItems (ID) " +
                    "SELECT rh.ID " +
                    "FROM RelacaoHierarquica rh " +
                        "INNER JOIN NivelDesignado nd ON nd.ID = rh.ID AND nd.isDeleted = @isDeleted " +
                    "WHERE rh.IDTipoNivelRelacionado = @IDTipoNivelRelacionado AND rh.isDeleted = @isDeleted " +
                    "ORDER BY nd.Designacao";
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.Parameters.AddWithValue("@IDTipoNivelRelacionado", 2);
                command.ExecuteNonQuery();
            }
        }

        public override List<NivelDocumentalListItem> GetItemsGA(DataSet currentDataSet, int pageNr, int itemsPerPage, IDbConnection conn)
        {
            List<NivelDocumentalListItem> rows = new List<NivelDocumentalListItem>();
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #ItemsID (ID BIGINT, seq_id INT)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO #ItemsID SELECT ID,seq_id FROM #OrderedItems WHERE seq_id >= @seq_id1 AND seq_id <= @seq_id2";
                command.Parameters.AddWithValue("@seq_id1", (pageNr - 1) * itemsPerPage + 1);
                command.Parameters.AddWithValue("@seq_id2", pageNr * itemsPerPage);
                command.ExecuteNonQuery();

                // Carregamento de informação dos IDs calculados e obtidos anteriormente
                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN #ItemsID ON #ItemsID.ID = Nivel.ID ");
                    da.Fill(currentDataSet, "Nivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"],
                        "INNER JOIN #ItemsID ON #ItemsID.ID = NivelDesignado.ID ");
                    da.Fill(currentDataSet, "NivelDesignado");
                }

                command.CommandText =
                    "SELECT n.ID, nd.Designacao, tnr.ID, " +
                        "tnr.GUIOrder " +
                    "FROM Nivel n " +
                        "INNER JOIN #ItemsID ON #ItemsID.ID = n.ID " +
                        "INNER JOIN (" +
                            "SELECT rh.ID, MIN(rh.IDTipoNivelRelacionado) IDTipoNivelRelacionado " +
                            "FROM #ItemsID " +
                                "INNER JOIN RelacaoHierarquica rh ON rh.ID = #ItemsID.ID AND rh.isDeleted = @isDeleted " +
                            "GROUP BY rh.ID " +
                        ") TipoNivelRelacionado ON TipoNivelRelacionado.ID = n.ID " +
                        "INNER JOIN TipoNivelRelacionado tnr ON tnr.ID = TipoNivelRelacionado.IDTipoNivelRelacionado AND tnr.isDeleted = @isDeleted " +
                        "INNER JOIN NivelDesignado nd ON nd.ID = n.ID AND nd.isDeleted = @isDeleted " +
                    "WHERE n.isDeleted = @isDeleted AND n.IDTipoNivel = @IDTipoNivel " +
                    "ORDER BY seq_id";
                command.Parameters.AddWithValue("@IDTipoNivel", 1);
                SqlDataReader reader = command.ExecuteReader();

                NivelDocumentalListItem row;
                while (reader.Read())
                {
                    row = new NivelDocumentalListItem();
                    row.IDNivel = reader.GetInt64(0);
                    row.Designacao = reader.GetString(1);
                    row.IDTipoNivelRelacionado = reader.GetInt64(2);
                    row.GUIOrder = System.Convert.ToInt32(reader.GetValue(3));
                    rows.Add(row);
                }
                reader.Close();
            }
            return rows;
        }
        #endregion

        #region NivelDocumentalList
        // Pré calcular os níveis documentais a serem apresentados na listview
        public override void CalculateOrderedItems(ArrayList ordenacao, string filtroDesignacaoLike, string filtroCodigoParcialLike, string filtroIDLike, string filtroConteudoLike, long idMovimento, IDbConnection conn)
        {
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                // a criação da tabela temporária deve ser feita antes de qualquer query que contenha parâmetros caso contrário a tabela temporária não é "encontrada" (scopes diferentes...)
                command.CommandText = "CREATE TABLE #OrderedItems ( seq_id INT IDENTITY(1, 1) NOT NULL, ID BIGINT NOT NULL ); ";
                command.ExecuteNonQuery();

                StringBuilder joinQuery = new StringBuilder(
                    "INNER JOIN RelacaoHierarquica rh ON rh.ID = n.ID " +
                    "INNER JOIN Nivel nUpper ON nUpper.ID = rh.IDUpper " +
                    "INNER JOIN NivelDesignado nd ON n.ID = nd.ID ");

                StringBuilder whereQuery = new StringBuilder(
                    "WHERE " +
                    "rh.IDTipoNivelRelacionado BETWEEN 9 AND 10 AND " +
                    "rh.isDeleted = 0 AND " +
                    "n.isDeleted = 0");

                if (filtroDesignacaoLike.Length > 0)
                {
                    command.Parameters.AddWithValue("@Designacao", filtroDesignacaoLike);
                    whereQuery.Append(" AND " + PesquisaRule.Current.buildLikeStatement("Designacao", "@Designacao"));
                }

                if (filtroCodigoParcialLike.Length > 0)
                {
                    command.Parameters.AddWithValue("@Codigo", filtroCodigoParcialLike);
                    whereQuery.Append(" AND " + PesquisaRule.Current.buildLikeStatement("n.Codigo", "@Codigo"));
                }

                if (filtroIDLike.Length > 0)
                {
                    command.Parameters.AddWithValue("@ID", filtroIDLike);
                    whereQuery.Append(" AND n.ID=@ID");
                }

                joinQuery.Append(
                    " LEFT JOIN FRDBase frd ON frd.IDNivel = n.ID" +
                    " LEFT JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frd.ID ");
                whereQuery.Append(
                    " AND (frd.IDTipoFRDBase IS NULL OR frd.IDTipoFRDBase = 1) " +
                    " AND (dp.IDFRDBase IS NULL OR dp.isDeleted = 0) " +
                    " AND n.IDTipoNivel = 3");

                if (filtroConteudoLike.Length > 0)
                {
                    joinQuery.Append(
                        " LEFT JOIN SFRDConteudoEEstrutura ON SFRDConteudoEEstrutura.IDFRDBase = frd.ID AND SFRDConteudoEEstrutura.isDeleted=@isDeleted ");
                    whereQuery.Append(" AND " + PesquisaRule.Current.buildLikeStatement("SFRDConteudoEEstrutura.ConteudoInformacional", "@ConteudoInformacional"));
                    command.Parameters.AddWithValue("@ConteudoInformacional", filtroConteudoLike);
                }

                // criar tabela temporária contendo, para cada nível documental, os elementos que compoem a data
                var filter = string.Format(
                    "SELECT DISTINCT n.ID, nd.Designacao, rh.IDTipoNivelRelacionado, " +
                        "dbo.fn_AddPaddingToDateMember_new(dp.FimAno, 4) as FimAno, " +
                        "dbo.fn_AddPaddingToDateMember_new(dp.FimMes, 2) as FimMes, " +
                        "dbo.fn_AddPaddingToDateMember_new(dp.FimDia, 2) as FimDia, " +
                        "dbo.fn_AddPaddingToDateMember_new(dp.InicioAno, 4) as InicioAno, " +
                        "dbo.fn_AddPaddingToDateMember_new(dp.InicioMes, 2) as InicioMes, " +
                        "dbo.fn_AddPaddingToDateMember_new(dp.InicioDia, 2) as InicioDia, " +
                        "dp.FimAtribuida, dp.InicioAtribuida " +
                    "INTO #temp " +
                    "FROM Nivel n " +
                        "INNER JOIN (" +
                            "SELECT n.ID " +
                            "FROM Nivel n " +
                                "LEFT JOIN DocumentosMovimentados dm ON dm.IDNivel = n.ID AND dm.isDeleted = 0 " +
                                "LEFT JOIN Movimento req ON req.ID = dm.IDMovimento and req.CatCode = 'REQ' AND req.isDeleted = 0 " +
                                "LEFT JOIN Movimento dev ON dev.ID = dm.IDMovimento AND dev.CatCode = 'DEV' AND dev.isDeleted = 0 " +
                            "GROUP BY n.ID " +
                            "HAVING NOT MAX(req.Data) IS NULL AND (MAX(dev.Data) IS NULL OR MAX(dev.Data) < MAX(req.Data)) AND (NOT MAX(req.Data) IS NULL AND MAX(req.ID) < {0}) " +
                        ") niveisReq ON niveisReq.ID = n.ID " +
                        joinQuery + whereQuery, idMovimento);

                // ordenar as unidades físicas segundo o critério definido pelo utilizador
                StringBuilder orderByQuery = new StringBuilder();
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
                        //Identificador
                        case 0:
                            orderByQuery.AppendFormat(" ID {0}", order);
                            break;
                        //Designação
                        case 1:
                            orderByQuery.AppendFormat(" Designacao {0}", order);
                            break;
                        //Datas produção
                        case 2:
                            orderByQuery.AppendFormat(" InicioAno {0}, InicioMes {0}, InicioDia {0}, FimAno {0}, FimMes {0}, FimDia {0}", order);
                            break;
                    }
                }
                command.CommandText = filter;
                if (ordenacao.Count > 0)
                    command.CommandText += string.Format(
                        "INSERT INTO #OrderedItems (ID) " +
                        "SELECT ID " +
                        "FROM #temp " +
                        "ORDER BY {0}; " +
                        "DROP TABLE #temp;", orderByQuery);
                else
                    command.CommandText +=
                        "INSERT INTO #OrderedItems (ID) " +
                        "SELECT ID " +
                        "FROM #temp " +
                        "ORDER BY IDTipoNivelRelacionado, FimAno DESC, FimMes DESC, FimDia DESC, InicioAno DESC, InicioMes DESC, InicioDia DESC, Designacao; " +
                        "DROP TABLE #temp;";
                command.ExecuteNonQuery();
            }
		}

        //public override List<NivelDocumentalListItem> GetItems(DataSet currentDataSet, long parentNivelID, int pageNr, long exceptTipoNivel, int itemsPerPage, IDbConnection conn) {
        public override ArrayList GetItems(DataSet currentDataSet, int pageNr, long exceptTipoNivel, int itemsPerPage, IDbConnection conn)
        {
			ArrayList rows = new ArrayList();
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #ItemsID (ID BIGINT, seq_id INT)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO #ItemsID SELECT ID,seq_id FROM #OrderedItems WHERE seq_id >= @seq_id1 AND seq_id <= @seq_id2";
                command.Parameters.AddWithValue("@seq_id1", (pageNr - 1) * itemsPerPage + 1);
                command.Parameters.AddWithValue("@seq_id2", pageNr * itemsPerPage);
                command.ExecuteNonQuery();

                // Carregamento de informação dos IDs calculados e obtidos anteriormente
                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN #ItemsID ON #ItemsID.ID = Nivel.ID ");
                    da.Fill(currentDataSet, "Nivel");
                }

                command.CommandText =
                    "SELECT n.ID, nd.Designacao, tnr.ID, " +     // 0; 1; 2
                        "dp.FimAno, " +
                        "dp.FimMes, " +
                        "dp.FimDia, " +
                        "dp.InicioAno, " +
                        "dp.InicioMes, " +
                        "dp.InicioDia, " +
                        "dp.FimAtribuida, " +
                        "dp.InicioAtribuida, " +
                        "tnr.GUIOrder " +               // 4
                    "FROM Nivel n " +
                        "INNER JOIN #ItemsID ON #ItemsID.ID = n.ID " +
                        "INNER JOIN (" +
                            "SELECT rh.ID, MIN(rh.IDTipoNivelRelacionado) IDTipoNivelRelacionado " +
                            "FROM #ItemsID " +
                                "INNER JOIN RelacaoHierarquica rh ON rh.ID = #ItemsID.ID AND rh.isDeleted = @isDeleted " +
                            "GROUP BY rh.ID " +
                        ") TipoNivelRelacionado ON TipoNivelRelacionado.ID = n.ID " +
                        "INNER JOIN NivelDesignado nd ON n.ID = nd.ID " +
                        "INNER JOIN TipoNivelRelacionado tnr ON tnr.ID = TipoNivelRelacionado.IDTipoNivelRelacionado " +
                        "LEFT JOIN FRDBase frd ON frd.IDNivel = n.ID " +
                        "LEFT JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frd.ID " +
                    "WHERE n.isDeleted = 0 AND nd.isDeleted = @isDeleted " +
                        "AND (frd.IDTipoFRDBase IS NULL OR frd.IDTipoFRDBase = @IDTipoFRDBase) " +
                        "AND (dp.IDFRDBase IS NULL OR dp.isDeleted = @isDeleted) " +
                    "ORDER BY seq_id";
                command.Parameters.AddWithValue("@IDTipoFRDBase", 1);
                SqlDataReader reader = command.ExecuteReader();

                NivelDocumentalListItem row;
                while (reader.Read())
                {
                    row = new NivelDocumentalListItem();
                    row.IDNivel = System.Convert.ToInt64(reader.GetValue(0));
                    row.Designacao = reader.GetString(1);
                    row.IDTipoNivelRelacionado = System.Convert.ToInt64(reader.GetValue(2));
                    row.FimAno = reader.GetValue(3).ToString();
                    row.FimMes = reader.GetValue(4).ToString();
                    row.FimDia = reader.GetValue(5).ToString();
                    row.InicioAno = reader.GetValue(6).ToString();
                    row.InicioMes = reader.GetValue(7).ToString();
                    row.InicioDia = reader.GetValue(8).ToString();
                    if (reader.GetValue(9) != null && reader.GetValue(9) != DBNull.Value)
                        row.FimAtribuida = Convert.ToBoolean(reader.GetValue(9));
                    if (reader.GetValue(10) != null && reader.GetValue(10) != DBNull.Value)
                        row.InicioAtribuida = Convert.ToBoolean(reader.GetValue(10));
                    row.GUIOrder = System.Convert.ToInt32(reader.GetValue(11));
                    rows.Add(row);
                }
                reader.Close();
            }
			return rows;
		}

		public override void DeleteTemporaryResults(IDbConnection conn) {
            using (SqlCommand command = new SqlCommand("DROP TABLE #ItemsID; DROP TABLE #OrderedItems;", (SqlConnection)conn))
            {
                command.ExecuteNonQuery();
            }
		}
		#endregion

        #region NivelDocumentalListNavigator
        public override void CalculateOrderedItemsNav(DataSet currentDataSet, ArrayList ordenacao, long IDNivel, string filtroDesignacaoLike, string filtroCodigoParcialLike, string filtroIDLike, string filtroConteudoLike, long? movimentoID, bool filtroExcluirRequisitados, bool showAllDocTopo, IDbConnection conn)
        {
            List<string> joins;
            List<string> where;
            List<string> orderBy;

            using (var command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                // create temp tables over non-parameterized queries 
                command.CommandText = "CREATE TABLE #OrderedItems ( seq_id INT IDENTITY(1, 1) NOT NULL, ID BIGINT NOT NULL ); " +
                    "CREATE TABLE #NiveisFiltered (ID BIGINT, IDTipoNivelRelacionado BIGINT); " +
                    "CREATE TABLE #NiveisTopo (ID BIGINT, IDTipoNivelRelacionado BIGINT);";
                command.ExecuteNonQuery();

                // UNIVERSO
                if (showAllDocTopo)
                {
                    command.CommandText = string.Format(@"
                    WITH Temp (ID, IDUpper, IDTipoNivelRelacionado)
                    AS (
	                    SELECT rh.ID, rh.IDUpper, rh.IDTipoNivelRelacionado
	                    FROM RelacaoHierarquica rh
                            INNER JOIN Nivel n ON n.ID = rh.ID AND n.isDeleted=@isDeleted
	                    WHERE rh.IDUpper = @IDNivel AND rh.isDeleted=@isDeleted
	
	                    UNION ALL
                    	
	                    SELECT RelacaoHierarquica.ID, RelacaoHierarquica.IDUpper, RelacaoHierarquica.IDTipoNivelRelacionado
	                    FROM RelacaoHierarquica
		                    INNER JOIN Temp ON Temp.ID = RelacaoHierarquica.IDUpper
		                    INNER JOIN Nivel n ON n.ID = RelacaoHierarquica.ID AND n.isDeleted=@isDeleted
		                    INNER JOIN Nivel nUpper ON nUpper.ID = RelacaoHierarquica.IDUpper AND nUpper.isDeleted=@isDeleted
	                    WHERE (nUpper.IDTipoNivel = @IDTipoNivel2 AND (n.IDTipoNivel = @IDTipoNivel2 OR n.IDTipoNivel = @IDTipoNivel3)) AND RelacaoHierarquica.isDeleted=@isDeleted
                    )

                    INSERT INTO #NiveisTopo
                    SELECT DISTINCT ID, IDTipoNivelRelacionado 
                    FROM Temp
                    WHERE IDTipoNivelRelacionado >= @IDTipoNivelRelacionado7", IDNivel);

                    command.Parameters.AddWithValue("@isDeleted", 0);
                    command.Parameters.AddWithValue("@IDNivel", IDNivel);
                    command.Parameters.AddWithValue("@IDTipoNivel2", 2);
                    command.Parameters.AddWithValue("@IDTipoNivel3", 3);
                    command.Parameters.AddWithValue("@IDTipoNivelRelacionado7", 7);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
                else
                {
                    command.CommandText = "INSERT INTO #NiveisTopo SELECT DISTINCT ID, IDTipoNivelRelacionado FROM RelacaoHierarquica WHERE IDUpper=@IDNivel AND IDTipoNivelRelacionado>@IDTipoNivelRelacionado6 AND isDeleted=@isDeleted";
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    command.Parameters.AddWithValue("@IDNivel", IDNivel);
                    command.Parameters.AddWithValue("@IDTipoNivelRelacionado6", 6);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
#if DEBUG
                command.CommandText = "select count(*) from #NiveisTopo";
                Debug.WriteLine("#NiveisTopo: " + command.ExecuteScalar().ToString());
#endif

                // FILTRAGEM
                if (filtroDesignacaoLike.Length == 0 && filtroIDLike.Length == 0 && filtroCodigoParcialLike.Length == 0 && filtroConteudoLike.Length == 0 && movimentoID == null)
                {
                    command.CommandText = string.Format("INSERT INTO #NiveisFiltered SELECT ID, IDTipoNivelRelacionado FROM #NiveisTopo");
                    command.ExecuteNonQuery();
                }
                else
                {
                    joins = new List<string>();
                    where = new List<string>();

                    if (filtroDesignacaoLike.Length > 0)
                    {
                        joins.Add("INNER JOIN NivelDesignado nd ON nd.ID = n.ID AND nd.isDeleted = @isDeleted");
                        where.Add(PesquisaRule.Current.buildLikeStatement("nd.Designacao", "@Designacao"));
                        command.Parameters.AddWithValue("@Designacao", filtroDesignacaoLike);
                    }

                    if (filtroIDLike.Length > 0)
                    {
                        where.Add("n.ID = @filtroIDLike");
                        command.Parameters.AddWithValue("@filtroIDLike", filtroIDLike);
                    }

                    if (filtroCodigoParcialLike.Length > 0)
                    {
                        joins.Add("INNER JOIN Nivel nvl ON nvl.ID = n.ID AND nvl.isDeleted = @isDeleted");
                        where.Add(PesquisaRule.Current.buildLikeStatement("nvl.Codigo", "@Codigo"));
                        command.Parameters.AddWithValue("@Codigo", filtroCodigoParcialLike);
                    }

                    if (filtroConteudoLike.Length > 0)
                    {
                        joins.Add("INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted=@isDeleted");
                        joins.Add("LEFT JOIN SFRDConteudoEEstrutura sfrdce ON sfrdce.IDFRDBase = frd.ID AND sfrdce.isDeleted=@isDeleted");
                        where.Add(PesquisaRule.Current.buildLikeStatement("sfrdce.ConteudoInformacional", "@ConteudoInformacional"));
                        command.Parameters.AddWithValue("@ConteudoInformacional", filtroConteudoLike);
                    }

                    if (filtroExcluirRequisitados && movimentoID != null)
                    {
                        joins.Add(
                            "LEFT JOIN ( " +
                                "SELECT n.ID " +
                                "FROM Nivel n " +
                                    "LEFT JOIN DocumentosMovimentados dm ON dm.IDNivel = n.ID AND dm.isDeleted=@isDeleted " +
                                    "LEFT JOIN Movimento req ON req.ID = dm.IDMovimento and req.CatCode = 'REQ' AND req.isDeleted=@isDeleted " +
                                    "LEFT JOIN Movimento dev ON dev.ID = dm.IDMovimento AND dev.CatCode = 'DEV' AND dev.isDeleted=@isDeleted " +
                                "WHERE n.isDeleted=@isDeleted " +
                                "GROUP BY n.ID " +
                                "HAVING NOT MAX(req.Data) IS NULL AND (MAX(dev.Data) IS NULL OR MAX(dev.Data) < MAX(req.Data)) AND (NOT MAX(req.Data) IS NULL AND MAX(req.ID) <= @IDMov) " +
                            ") reqDocs ON reqDocs.ID = n.ID ");
                        where.Add("reqDocs.ID IS NULL");
                        command.Parameters.AddWithValue("@IDMov", movimentoID);
                    }

                    command.CommandText = string.Format("INSERT INTO #NiveisFiltered SELECT n.ID, n.IDTipoNivelRelacionado FROM #NiveisTopo n {0} {2} {1}",
                        string.Join(" ", joins.ToArray()),
                        string.Join(" AND ", where.ToArray()),
                        where.Count>0 ? "WHERE" : "");
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
#if DEBUG
                command.CommandText = "select count(*) from #NiveisFiltered";
                Debug.WriteLine("#NiveisFiltered: " + command.ExecuteScalar().ToString());
#endif

                // ORDENACAO
                if (ordenacao.Count == 0)
                {
                    command.CommandText =
                       "INSERT INTO #OrderedItems (ID) " +
                       "SELECT n.ID " +
                       "FROM #NiveisFiltered n " +
                       "LEFT JOIN NivelDocumentoSimples nds ON nds.ID = n.ID " +
                       "ORDER BY n.IDTipoNivelRelacionado, nds.GUIOrder, n.ID DESC";
                    command.ExecuteNonQuery();
                }
                else
                {
                    joins = new List<string>();
                    orderBy = new List<string>();

                    for (int i = 0; i < ordenacao.Count; i = i + 2)
                    {
                        Object a = ordenacao[i];
                        string order = string.Empty;
                        if ((bool)ordenacao[i + 1])
                            order = "ASC";
                        else
                            order = "DESC";

                        switch ((int)a)
                        {
                            //Identificador
                            case 0:
                                orderBy.Add("n.ID " + order);
                                break;
                            //Designação
                            case 1:
                                joins.Add("INNER JOIN NivelDesignado nd ON nd.ID=n.ID AND nd.isDeleted=@isDeleted");
                                orderBy.Add("nd.Designacao " + order);
                                break;
                            //Datas produção
                            case 2:
                                joins.Add("INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.IDTipoFRDBase=@IDTipoFRDBase AND frd.isDeleted=@isDeleted");
                                joins.Add("LEFT JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frd.ID AND frd.isDeleted=@isDeleted");
                                orderBy.Add(string.Format("dbo.fn_AddPaddingToDateMember_new(dp.InicioAno, 4) {0}, dbo.fn_AddPaddingToDateMember_new(dp.InicioMes, 2) {0}, " +
                                                        "dbo.fn_AddPaddingToDateMember_new(dp.InicioDia, 2) {0}, dbo.fn_AddPaddingToDateMember_new(dp.FimAno, 4) {0}, " +
                                                        "dbo.fn_AddPaddingToDateMember_new(dp.FimMes, 2) {0}, dbo.fn_AddPaddingToDateMember_new(dp.FimDia, 2) {0}", order));
                                break;
                            // Requisitado:
                            case 3:
                                joins.Add(
    @"INNER JOIN (
    SELECT n.ID, Requisitado = CASE WHEN Mov.Data_Req IS NULL OR (Mov.Data_Dev IS NOT NULL AND Mov.Data_Req < Mov.Data_Dev) THEN 'Não' ELSE 'Sim' END 
    FROM #NiveisFiltered n
        LEFT JOIN ( 
            SELECT n1.ID IDNivel, MAX(req.Data) Data_Req, MAX(dev.Data) Data_Dev 
            FROM Nivel n1
                LEFT JOIN DocumentosMovimentados dm ON dm.IDNivel = n1.ID AND dm.isDeleted=@isDeleted
                LEFT JOIN Movimento req ON req.ID = dm.IDMovimento and req.CatCode = @reqCatCode AND req.isDeleted=@isDeleted
                LEFT JOIN Movimento dev ON dev.ID = dm.IDMovimento AND dev.CatCode = @devCatCode AND dev.isDeleted=@isDeleted
            GROUP BY n1.ID
        ) Mov ON Mov.IDNivel = n.ID
) Req ON Req.ID = n.ID");
                                orderBy.Add("Req.Requisitado " + order);
                                break;
                            // Agrupador:
                            case 4:
                                joins.Add("INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.IDTipoFRDBase=@IDTipoFRDBase AND frd.isDeleted=@isDeleted");
                                joins.Add("LEFT JOIN SFRDAgrupador agr ON agr.IDFRDBase = frd.ID AND agr.isDeleted=@isDeleted");
                                orderBy.Add("agr.Agrupador " + order);
                                break;
                        }
                    }

                    command.Parameters.AddWithValue("@isDeleted", 0);
                    command.Parameters.AddWithValue("@IDTipoFRDBase", 1);
                    command.Parameters.AddWithValue("@reqCatCode", "REQ");
                    command.Parameters.AddWithValue("@devCatCode", "DEV");

                    command.CommandText = string.Format("INSERT INTO #OrderedItems (ID) SELECT n.ID FROM #NiveisFiltered n {0} {2} {1}",
                        string.Join(" ", joins.Distinct().ToArray()),
                        string.Join(", ", orderBy.ToArray()),
                        orderBy.Count > 0 ? "ORDER BY" : "");
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
#if DEBUG
                command.CommandText = "select count(*) from #OrderedItems";
                Debug.WriteLine("#OrderedItems: " + command.ExecuteScalar().ToString());
#endif
                command.CommandText = "DROP TABLE #NiveisTopo; DROP TABLE #NiveisFiltered;";
                command.ExecuteNonQuery();
            }
        }

        public override ArrayList GetItemsNav(DataSet currentDataSet, int pageNr, int itemsPerPage, IDbConnection conn)
        {
            ArrayList rows = new ArrayList();
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #ItemsID (ID BIGINT, seq_id BIGINT)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO #ItemsID SELECT ID,seq_id FROM #OrderedItems WHERE seq_id >= @seq_id1 AND seq_id <= @seq_id2";
                command.Parameters.AddWithValue("@seq_id1", (pageNr - 1) * itemsPerPage + 1);
                command.Parameters.AddWithValue("@seq_id2", pageNr * itemsPerPage);
                command.ExecuteNonQuery();

                // Carregamento de informação dos IDs calculados e obtidos anteriormente
                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN #ItemsID ON #ItemsID.ID = Nivel.ID");
                    da.Fill(currentDataSet, "Nivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDesignado"],
                        "INNER JOIN #ItemsID ON #ItemsID.ID = NivelDesignado.ID");
                    da.Fill(currentDataSet, "NivelDesignado");

                    //da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelDocumentoSimples"], 
                    //    "INNER JOIN #ItemsID ON #ItemsID.ID = NivelDocumentoSimples.ID");
                    //da.Fill(currentDataSet, "NivelDocumentoSimples");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                        "INNER JOIN #ItemsID ON #ItemsID.ID = RelacaoHierarquica.ID");
                    da.Fill(currentDataSet, "RelacaoHierarquica");

                    // carregar niveis dos parents para o caso do filtro "esconder niveis não directos" nao estar activo
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"], 
                        "INNER JOIN RelacaoHierarquica rh ON rh.IDUpper = Nivel.ID " +
                        "INNER JOIN #ItemsID ON #ItemsID.ID = rh.ID");
                    da.Fill(currentDataSet, "Nivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["FRDBase"],
                        "INNER JOIN #ItemsID ON #ItemsID.ID = FRDBase.IDNivel");
                    da.Fill(currentDataSet, "FRDBase");

                    //necessário apenas para a integração
                    //constraint =
                    //    "INNER JOIN FRDBase frd ON frd.ID = SFRDDatasProducao.IDFRDBase " +
                    //    "INNER JOIN #ItemsID ON #ItemsID.ID = frd.IDNivel";
                    //da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["SFRDDatasProducao"], constraint);
                    //da.Fill(currentDataSet, "SFRDDatasProducao");
                }

                command.CommandText =
                        "SELECT n.ID, nd.Designacao, rh.IDTipoNivelRelacionado, " +     // 0; 1; 2
                        "Requisitado = " +              // 3
                        "	CASE " +
                        "		WHEN Mov.Data_Req IS NULL OR (Mov.Data_Dev IS NOT NULL AND Mov.Data_Req < Mov.Data_Dev) THEN 'Não' " +
                        "		ELSE 'Sim' " +
                        "	END, " +
                        "tnr.GUIOrder, " +               // 4
                        "dp.FimAno, " +
                        "dp.FimMes, " +
                        "dp.FimDia, " +
                        "dp.InicioAno, " +
                        "dp.InicioMes, " +
                        "dp.InicioDia, " +
                        "dp.FimAtribuida, " +
                        "dp.InicioAtribuida, " +
                        "agr.Agrupador " +
                    "FROM Nivel n " +
                    "LEFT JOIN ( " +
                    "	SELECT n1.ID IDNivel, MAX(req.Data) Data_Req, MAX(dev.Data) Data_Dev " +
                    "	FROM Nivel n1 " +
                    "	LEFT JOIN DocumentosMovimentados dm ON dm.IDNivel = n1.ID AND dm.isDeleted = @isDeleted " +
                    "	LEFT JOIN Movimento req ON req.ID = dm.IDMovimento and req.CatCode = 'REQ' AND req.isDeleted = @isDeleted " +
                    "    LEFT JOIN Movimento dev ON dev.ID = dm.IDMovimento AND dev.CatCode = 'DEV' AND dev.isDeleted = @isDeleted " +
                    "	GROUP BY n1.ID " +
                    ") Mov ON Mov.IDNivel = n.ID " +

                    "INNER JOIN #ItemsID ON #ItemsID.ID = n.ID " +

                    "INNER JOIN (" +
                        "SELECT DISTINCT rh.ID, rh.IDTipoNivelRelacionado " +
                        "FROM #ItemsID " +
                            "INNER JOIN RelacaoHierarquica rh ON rh.ID = #ItemsID.ID AND rh.isDeleted = @isDeleted " +
                    ") rh ON rh.ID = n.ID " +
                    "INNER JOIN NivelDesignado nd ON n.ID = nd.ID " +
                    "INNER JOIN TipoNivelRelacionado tnr ON tnr.ID = rh.IDTipoNivelRelacionado " +
                    "INNER JOIN FRDBase frd ON frd.IDNivel = n.ID AND frd.isDeleted = @isDeleted " +
                    "LEFT JOIN SFRDDatasProducao dp ON dp.IDFRDBase = frd.ID " +
                    "LEFT JOIN SFRDAgrupador agr ON agr.IDFRDBase = frd.ID " +

                    " WHERE " +
                    " n.isDeleted = @isDeleted " +
                    " AND (dp.IDFRDBase IS NULL OR dp.isDeleted = @isDeleted) " +
                    " AND n.IDTipoNivel = @IDTipoNivel " +
                    "ORDER BY #ItemsID.seq_id ";
                command.Parameters.AddWithValue("@IDTipoNivel", 3);

                SqlDataReader reader = command.ExecuteReader();

                NivelDocumentalListItem row;
                while (reader.Read())
                {
                    row = new NivelDocumentalListItem();
                    row.IDNivel = System.Convert.ToInt64(reader.GetValue(0));
                    row.Designacao = reader.GetString(1);
                    row.IDTipoNivelRelacionado = System.Convert.ToInt64(reader.GetValue(2));

                    row.Requisitado = reader.GetString(3);
                    row.GUIOrder = System.Convert.ToInt32(reader.GetValue(4));

                    row.FimAno = reader.GetValue(5).ToString();
                    row.FimMes = reader.GetValue(6).ToString();
                    row.FimDia = reader.GetValue(7).ToString();
                    row.InicioAno = reader.GetValue(8).ToString();
                    row.InicioMes = reader.GetValue(9).ToString();
                    row.InicioDia = reader.GetValue(10).ToString();
                    if (reader.GetValue(11) != null && reader.GetValue(11) != DBNull.Value)
                        row.FimAtribuida = Convert.ToBoolean(reader.GetValue(11));
                    if (reader.GetValue(12) != null && reader.GetValue(12) != DBNull.Value)
                        row.InicioAtribuida = Convert.ToBoolean(reader.GetValue(12));
                    row.Agrupador = !reader.IsDBNull(13) ? reader.GetString(13) : "";

                    rows.Add(row);
                }
                reader.Close();
            }
            return rows;
        }
        #endregion

        #region Controlo Localização
        private SqlDataReader CalculateNivelLocalizacao(long NivelID, long TrusteeID, IDbConnection conn)
        {
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = "sp_genTree";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@NivelID", SqlDbType.BigInt).Value = NivelID;
            command.Parameters.Add("@IDTrustee", SqlDbType.BigInt).Value = TrusteeID;
            return command.ExecuteReader();
        }

        public override void LoadNivelLocalizacao(DataSet currentDataSet, long NivelID, long TrusteeID, IDbConnection conn)
        {
            SqlDataReader reader = CalculateNivelLocalizacao(NivelID, TrusteeID, conn);
            var cmd = new StringBuilder("CREATE TABLE #temp (ID BIGINT, IDUpper BIGINT);");
            
            while (reader.Read())
            {
                if (reader.GetValue(1) != DBNull.Value) {
                    cmd.Append(string.Format("INSERT INTO #temp VALUES ({0}, {1});", reader.GetInt64(0), reader.GetInt64(1)));
                }
            }

            reader.Close();

            using (var command = new SqlCommand(cmd.ToString(), (SqlConnection)conn))
            {
                command.ExecuteNonQuery();

                command.CommandText = "SELECT DISTINCT ID, IDUpper INTO #rhTemp FROM #temp";
                command.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN #rhTemp ON #rhTemp.ID = Nivel.ID");
                    da.Fill(currentDataSet, "Nivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"],
                        "INNER JOIN #rhTemp ON #rhTemp.IDUpper = Nivel.ID");
                    da.Fill(currentDataSet, "Nivel");

                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["RelacaoHierarquica"],
                        "INNER JOIN #rhTemp ON #rhTemp.ID = RelacaoHierarquica.ID AND #rhTemp.IDUpper = RelacaoHierarquica.IDUpper");
                    da.Fill(currentDataSet, "RelacaoHierarquica");
                }

                command.CommandText = "DROP TABLE #temp; DROP TABLE #rhTemp;";
                command.ExecuteNonQuery();
            }
        }

        public override ArrayList GetNivelLocalizacao(long NivelID, long TrusteeID, IDbConnection conn)
		{
			SqlDataReader reader = CalculateNivelLocalizacao(NivelID, TrusteeID, conn);

			ArrayList result = new ArrayList();
			MyNode node;
			while (reader.Read())
			{
				node = new MyNode();
				node.IDNivel = System.Convert.ToInt64(reader.GetValue(0));
				if (reader.GetValue(1) != DBNull.Value)
					node.IDNivelUpper = System.Convert.ToInt64(reader.GetValue(1));
                node.Age = System.Convert.ToInt32(reader.GetValue(2));
				node.TipoNivelRelacionado = System.Convert.ToInt64(reader.GetValue(3));
				node.TipoNivel = reader.GetValue(4).ToString();
				node.Designacao = reader.GetValue(5).ToString();
                node.AnoInicio = reader.GetValue(6).ToString();
                node.AnoFim = reader.GetValue(7).ToString();

				result.Add(node);
			}
			reader.Close();

			return result;
		}

        public override ArrayList GetNivelChildren(long NivelID, long TrusteeID, long IDTipoNivelRelLimit, IDbConnection conn) 
		{
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection) conn);
			command.CommandText = "sp_genTreeLevel";
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@NivelID", SqlDbType.BigInt).Value = NivelID;
			command.Parameters.Add("@TrusteeID", SqlDbType.BigInt).Value = TrusteeID;
            command.Parameters.Add("@MaxExceptIDTipoNivelRel", SqlDbType.Int).Value = IDTipoNivelRelLimit;
			SqlDataReader reader = command.ExecuteReader();

			ArrayList result = new ArrayList();
			MyNode node;
			while (reader.Read())
			{
				node = new MyNode();
				node.IDNivel = System.Convert.ToInt64(reader.GetValue(0));
				if (reader.GetValue(1) != DBNull.Value)
					node.IDNivelUpper = System.Convert.ToInt64(reader.GetValue(1));
				node.Age = System.Convert.ToInt32(reader.GetValue(2));
				node.TipoNivelRelacionado = System.Convert.ToInt64(reader.GetValue(3));
				node.TipoNivel = reader.GetValue(4).ToString();
				node.Designacao = reader.GetValue(5).ToString();
				node.AnoInicio = reader.GetValue(6).ToString();
				node.AnoFim = reader.GetValue(7).ToString();

				result.Add(node);
			}

			reader.Close();

			return result;
		}
		#endregion

        #region " NivelDocumentalListNavigator "
        public override void LoadImagemIlustracao(long nID, DataSet currentDataSet, IDbConnection conn)
        {
            using (var command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (var da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@nID", nID);
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["NivelImagemIlustracao"],
                        "WHERE ID=@nID");
                da.Fill(currentDataSet, "NivelImagemIlustracao");
            }
        }
        #endregion

        public override bool HasSeries(long produtorID, IDbConnection conn)
        {
            long res = 0;
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.Parameters.AddWithValue("@produtorID", produtorID);
                command.Parameters.AddWithValue("@IDTipoNivelRelacionado", 7);
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.CommandText = "SELECT COUNT(*) FROM RelacaoHierarquica WHERE IDUpper = @produtorID AND IDTipoNivelRelacionado = @IDTipoNivelRelacionado AND isDeleted = @isDeleted";
                res = System.Convert.ToInt64(command.ExecuteScalar());
            }
            return res > 0;
        }
	}
}
