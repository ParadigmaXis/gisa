using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using NUnit.Framework;

using DBAbstractDataLayer.DataAccessRules.SqlClient;

namespace GISA.UnitTests.DBAsbstractDataLayer.DataAccessRules.SQLClient
{
    [TestFixture]
    class SqlClientMovimentoRuleTest
    {

        private SqlConnection connection;
        private SqlClientMovimentoRule movimentoRule;

        public SqlClientMovimentoRuleTest()
        {
            this.connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            this.movimentoRule = new SqlClientMovimentoRule();
        }

        [TestFixtureSetUp]
        public void FillDatabase()
        {
            StringBuilder sql = new StringBuilder();

            // Add some documents
            sql.Append(@"
                INSERT INTO Nivel (IDMovimento, IDNivel, isDeleted)
                VALUES (@idMovimento, @idNivel, 0);
            ");

            // IsDeleted Data
            sql.Append(@"
                INSERT INTO DocumentosMovimentados (IDMovimento, IDNivel, isDeleted)
                VALUES (@idMovimento, @idNivel, 0);
            ");

            SqlCommand command = new SqlCommand();
        }

        [TestFixtureSetUp]
        public void RemoveTestDataFromDatabase()
        {

        }

        [Test]
        public void IsDeletable()
        { 
            
        }
    }
}
