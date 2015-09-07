using System;
using System.Data;
using NUnit.Framework;

namespace DBAbstractDataLayer
{
#if DEBUG
	using NUnit.Framework;
	[TestFixture] public class TestFixture
	{

		[Test] public void CreateBuilder() 
		{
			DBAbstractDataLayer.DataAccessRules.DALRule.ConnectionBuilder = new DBAbstractDataLayer.ConnectionBuilders.SqlClientBuilder();
			IDbConnection c = DBAbstractDataLayer.DataAccessRules.DALRule.ConnectionBuilder.Connection;

			Assert.IsNotNull(c);
			Assert.AreSame(typeof(System.Data.SqlClient.SqlConnection), c.GetType());

		}
		[Test] public void CreateNivelRule()
		{
			DBAbstractDataLayer.DataAccessRules.DALRule.ConnectionBuilder = new DBAbstractDataLayer.ConnectionBuilders.SqlClientBuilder();
			DBAbstractDataLayer.DataAccessRules.NivelRule rule = DBAbstractDataLayer.DataAccessRules.NivelRule.Current;
			DBAbstractDataLayer.DataAccessRules.NivelRule.ClearCurrent();
			Assert.IsNotNull(rule);
			Assert.AreSame(typeof(DBAbstractDataLayer.DataAccessRules.SqlClient.SqlClientNivelRule), rule.GetType());

		}
		[Test] public void ExampleLoadTipoNivel() {
			// Configuration phase
			DBAbstractDataLayer.DataAccessRules.DALRule.ConnectionBuilder = new DBAbstractDataLayer.ConnectionBuilders.SqlClientBuilder();
			
			// Example
			IDbConnection c = DBAbstractDataLayer.DataAccessRules.DALRule.ConnectionBuilder.Connection;
			Console.WriteLine(c.ConnectionString);
				
			c.Open();
			System.Data.DataSet d = new DataSet();
			System.Data.DataTable dt = new DataTable("Dicionario");
			System.Data.DataColumn dc1 = new DataColumn("ID", typeof(System.Int64), null, System.Data.MappingType.Element);
			System.Data.DataColumn dc2 = new DataColumn("Termo", typeof(System.String), null, System.Data.MappingType.Element);
			System.Data.DataColumn dc3 = new DataColumn("CatCode", typeof(System.String), null, System.Data.MappingType.Element);
			System.Data.DataColumn dc4 = new DataColumn("Versao", typeof(System.Byte[]), null, System.Data.MappingType.Element);
			System.Data.DataColumn dc5 = new DataColumn("isDeleted", typeof(System.Boolean), null, System.Data.MappingType.Element);
			dt.Columns.Add(dc1);
			dt.Columns.Add(dc2);
			dt.Columns.Add(dc3);
			dt.Columns.Add(dc4);
			dt.Columns.Add(dc5);
			d.Tables.Add(dt);

			//DBAbstractDataLayer.DataAccessRules.NivelRule.Current.t(d,c);
			// Load other stuff if needed
			c.Close();
	
		}
	}
#endif
}
