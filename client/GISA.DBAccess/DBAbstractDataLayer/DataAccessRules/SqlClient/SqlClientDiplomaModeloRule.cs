using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
	public class SqlClientDiplomaModeloRule: DiplomaModeloRule
	{
		#region " FormPickDiplomaModelo "
		public override bool ExistsControloAutDicionario(long IDDicionario, long IDTipoControloAutForma, long IDTipoNoticiaAut, System.Data.IDbTransaction tran)
		{
			SqlCommand command = new SqlCommand("sp_ExistsControloAutDicionario", (SqlConnection) tran.Connection, (SqlTransaction) tran);
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

		public override ArrayList GetDicionario(string catCode, string Termo, System.Data.IDbTransaction tran)
		{
			SqlDataReader reader = null;

			try 
			{			
				long ID = -1;
				bool isDeleted = false;
				ArrayList result = new ArrayList();

				SqlCommand command = new SqlCommand(string.Format("SELECT * FROM Dicionario WITH (UPDLOCK) WHERE LTRIM(RTRIM(CatCode)) LIKE '{0}' AND Termo LIKE '{1}'", catCode, Termo), (SqlConnection) tran.Connection, (SqlTransaction) tran);
				reader = command.ExecuteReader();			
			
				// só é esperada uma iteração uma vez que a query é feita tendo como critério de pesquisa a 
				// restrição única que engloba as colunas Termo e CatCode
				while (reader.Read())
				{
					ID = System.Convert.ToInt64(reader.GetValue(0));
					isDeleted = System.Convert.ToBoolean(reader.GetValue(4));
				}
				reader.Close();

				result.Add(ID);
				result.Add(isDeleted);
				return result;
			}
			catch (SqlException e)
			{
				if (reader != null && !reader.IsClosed)
					reader.Close();
				Trace.WriteLine(e.ToString());
				throw e;
			}
		}

		public override bool isTermoUsedByOthers(long CurrentIdCA, string catCode, string termo, bool actionDelete, IDbTransaction tran)
		{
			return isTermoUsedByOthers(CurrentIdCA, catCode, termo, actionDelete, -1, tran);
		}

		public override bool isTermoUsedByOthers(long CurrentIdCA, string catCode, string termo, bool actionDelete, long tnaID, IDbTransaction tran)
		{
			string comText = string.Empty;
            string termo_esc = termo.Trim().Replace("'", "''");

			if (actionDelete) 
			{
				comText = string.Format("SELECT COUNT(*) FROM Dicionario d WITH (UPDLOCK) " + 
										"INNER JOIN ControloAutDicionario cad ON d.ID = cad.IDDicionario " + 
										"WHERE LTRIM(RTRIM(d.CatCode)) = '{0}' AND d.Termo = '{1}' AND cad.IDControloAut != {2} " +
                                        "AND cad.isDeleted = 0", catCode, termo_esc, CurrentIdCA);
			} 
			else if (!(actionDelete) & !(tnaID < 0)) 
			{
				comText = string.Format("SELECT COUNT(*) FROM Dicionario d WITH (UPDLOCK) " + 
										"INNER JOIN ControloAutDicionario cad ON d.ID = cad.IDDicionario " + 
										"INNER JOIN ControloAut ca ON ca.ID = cad.IDControloAut " + 
										"INNER JOIN TipoNoticiaAut tna ON tna.ID = ca.IDTipoNoticiaAut " + 
										"INNER JOIN TipoControloAutForma tcaf ON tcaf.ID = cad.IDTipoControloAutForma " +
										"WHERE LTRIM(RTRIM(d.CatCode)) = '{0}' AND d.Termo = '{1}' AND cad.IDControloAut != {2} " +
                                        "AND cad.isDeleted = 0 AND tna.ID = {3} AND tcaf.ID = 1", catCode, termo_esc, CurrentIdCA, tnaID);
			}
			long usageCount = 0;
			try 
			{
                if (comText != string.Empty)
                {
                    SqlCommand command = new SqlCommand(comText, (SqlConnection)tran.Connection, (SqlTransaction)tran);
                    usageCount = System.Convert.ToInt64(command.ExecuteScalar());
                }
			} 
			catch (Exception e)
			{
				Trace.WriteLine(e.ToString());
				throw e;
			}
			return usageCount > 0;
		}

		public override ICollection LoadNivelControloAut(DataSet currentDataSet, ArrayList IDs, IDbConnection conn)
		{	
			foreach (long id in IDs) {
				DBAbstractDataLayer.DataAccessRules.NivelRule.Current.LoadControloAutChildren(id, currentDataSet, conn);
				DBAbstractDataLayer.DataAccessRules.NivelRule.Current.LoadNivelControloAutChildrenByCA(id, currentDataSet, conn);
			}

			ArrayList NivelIDs = new ArrayList();
			string query = string.Format(" INNER JOIN NivelControloAut nca ON nca.ID = Nivel.ID WHERE nca.isDeleted=0 AND nca.IDControloAut IN ({0})", longCollectionToCommaDelimitedString(IDs));
			string cmd = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Nivel"], query);
			SqlCommand command = new SqlCommand(cmd, (SqlConnection) conn);
			SqlDataReader reader = command.ExecuteReader();
			while (reader.Read()) 
			{
				NivelIDs.Add(reader.GetValue(0));
			}
			reader.Close();
			return NivelIDs;
		}

		public override IDataReader GetNoticiasAutoridadeRelaccionadas(DataSet currentDataSet, ArrayList IDs, IDbConnection conn)
		{
			string QueryIndex = string.Format("WHERE ID IN " + 
				"(" + 
				"(SELECT IDDicionario FROM ControloAutDicionario cad " + 
				"INNER JOIN ControloAut ca ON cad.IDTipoControloAutForma=1 AND cad.IDControloAut=ca.ID " + 
				"INNER JOIN ControloAutRel car ON car.IDControloAut=ca.ID AND car.IDControloAutAlias IN ({0}) " + 
				"WHERE cad.isDeleted=0 AND ca.isDeleted=0 AND car.isDeleted=0 " + 
				"UNION " + 
				"SELECT IDDicionario FROM ControloAutDicionario cad " + 
				"INNER JOIN ControloAut ca ON cad.IDTipoControloAutForma = 1 And cad.IDControloAut = ca.ID " + 
				"INNER JOIN ControloAutRel car ON car.IDControloAut IN ({0}) AND car.IDControloAutAlias=ca.ID " + 
				"WHERE cad.isDeleted=0 AND ca.isDeleted=0 AND car.isDeleted=0) " + 
				" UNION " + 
				"(SELECT IDDicionario FROM ControloAutDicionario cad " + 
				"INNER JOIN ControloAut ca ON cad.IDTipoControloAutForma=1 AND cad.IDControloAut=ca.ID " + 
				"INNER JOIN NivelControloAut nca ON nca.IDControloAut = ca.ID " + 
				"INNER JOIN RelacaoHierarquica rhUpper ON rhUpper.ID = nca.ID " + 
				"INNER JOIN NivelControloAut ncaUpper ON ncaUpper.ID = rhUpper.IDUpper AND ncaUpper.IDControloAut IN ({0}) " + 
				"WHERE cad.isDeleted=0 AND ca.isDeleted=0 AND nca.isDeleted=0 AND rhUpper.isDeleted=0 AND ncaUpper.isDeleted=0) " + 
				"UNION " + 
				"(SELECT IDDicionario FROM ControloAutDicionario cad " + 
				"INNER JOIN ControloAut ca ON cad.IDTipoControloAutForma=1 AND cad.IDControloAut=ca.ID " + 
				"INNER JOIN NivelControloAut nca ON nca.IDControloAut = ca.ID " + 
				"INNER JOIN RelacaoHierarquica rhChild ON rhChild.IDUpper = nca.ID " + 
				"INNER JOIN NivelControloAut ncaChild ON ncaChild.ID = rhChild.ID AND ncaChild.IDControloAut IN ({0}) " + 
				"WHERE cad.isDeleted=0 AND ca.isDeleted=0 AND nca.isDeleted=0 AND rhChild.isDeleted=0 AND ncaChild.isDeleted=0) " + 
				")", longCollectionToCommaDelimitedString(IDs));

			string cmd = SqlSyntax.CreateSelectCommandText(currentDataSet.Tables["Dicionario"], QueryIndex);
			SqlCommand command = new SqlCommand(cmd, (SqlConnection) conn);
			SqlDataReader reader = command.ExecuteReader();
			return reader;
		}

        public override List<CAAssociado> GetCANiveisAssociados(List<long> IDs, IDbConnection conn) 
		{
			string query = "CREATE TABLE #NiveisTemp (IDNivel BIGINT)";
			SqlCommand command = new SqlCommand(query, (SqlConnection) conn);
			command.CommandType = CommandType.Text;
			command.ExecuteNonQuery();			

			foreach (long ID in IDs) 
			{
				query = string.Format(" INSERT INTO #NiveisTemp VALUES ({0})", ID.ToString());                                
				command.CommandText = query;
				command.ExecuteNonQuery();
			}
			
			command = new SqlCommand("sp_getCANiveisAssociados", (SqlConnection) conn);
			command.CommandType = CommandType.StoredProcedure;			
			SqlDataReader reader = command.ExecuteReader();

            List<CAAssociado> result = new List<CAAssociado>();
			CAAssociado caAssociado;
			while (reader.Read())
			{
                caAssociado = new CAAssociado();
                caAssociado.IDNivel = System.Convert.ToInt64(reader.GetValue(0));
                caAssociado.TipoNivelDesignado = reader.GetValue(1).ToString();
                caAssociado.NivelDesignacao = reader.GetValue(2).ToString();
                caAssociado.AllowDelete = System.Convert.ToBoolean(reader.GetValue(3));
				result.Add(caAssociado);
			}
			reader.Close();

			return result;
		}

        public override List<long> GetIDLowers(long IDNivel, IDbConnection conn) {
			string query = string.Format("SELECT ID FROM RelacaoHierarquica rh WHERE rh.IDUpper = {0}", IDNivel);
			SqlCommand command = new SqlCommand(query, (SqlConnection) conn);
			command.CommandType = CommandType.Text;
			var reader = command.ExecuteReader();

            List<long> result = new List<long>();

            while (reader.Read()) {
                long id = reader.GetInt64(0);
                result.Add(id);
            }

            reader.Close();

            return result;
        }
		#endregion
	}
}
