using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
    class SqlClientObjectoDigitalStatusRule : ObjectoDigitalStatusRule
    {
        public override List<ObjectoDigitalStatusInfo> GetObjectoDigitalStatusInfo(System.Data.IDbConnection conn, System.Collections.ArrayList orderInfo)
        {
            SqlCommand command = new SqlCommand(this.BuildSQLQuery_GetObjectoDigitalStatusInfo(orderInfo), (SqlConnection)conn);
            SqlDataReader reader = command.ExecuteReader();
            List<ObjectoDigitalStatusInfo> results = BuildObjectoDigitalStatusInfo(reader);
            reader.Close();
            return results;
        }

        public override void removeOldODsFromQueue(System.Data.IDbConnection conn)
        {
            SqlCommand command = new SqlCommand("sp_removeOldODsFromQueue", (SqlConnection)conn);
            command.Parameters.Add("@pid", SqlDbType.NVarChar);
            command.Parameters[0].Value = "";
            command.Parameters.Add("@quality", SqlDbType.NVarChar);
            command.Parameters[1].Value = "";
            command.Parameters.Add("@state", SqlDbType.NVarChar);
            command.Parameters[2].Value = "";

            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
        }

    }
}
