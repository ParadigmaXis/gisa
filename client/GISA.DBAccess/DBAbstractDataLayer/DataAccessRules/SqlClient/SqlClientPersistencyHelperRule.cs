using System;
using System.Linq;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
	public class SqlClientPersistencyHelperRule: PersistencyHelperRule
	{
		public override void saveRows(DataTable dt, DataRow[] dr, IDbTransaction tran)
		{
            try
            {

                //using (SqlBulkCopy bulkCopy = new SqlBulkCopy((SqlConnection)tran.Connection, SqlBulkCopyOptions.CheckConstraints, (SqlTransaction)tran))
                //{
                //    var rows = dr.Where(r => r.RowState == DataRowState.Added).ToArray();
                //    bulkCopy.DestinationTableName = dt.TableName;

                //    if (dt.PrimaryKey.Cast<DataColumn>().Count(dc => dc.AutoIncrement) == 0)
                //    {
                //        foreach (DataColumn col in dt.Columns)
                //            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(col.ColumnName, col.ColumnName));
                //        bulkCopy.WriteToServer(rows);
                //        rows.ToList().ForEach(r => r.AcceptChanges());
                //    }
                //}
                                
                using (SqlDataAdapter da = new SqlDataAdapter(new SqlCommand("", (SqlConnection)tran.Connection, (SqlTransaction)tran)))
                {
                    da.InsertCommand = SqlSyntax.CreateInsertCommand(dt);
                    da.InsertCommand.Connection = (SqlConnection)tran.Connection;
                    da.InsertCommand.Transaction = (SqlTransaction)tran;
                    da.UpdateCommand = SqlSyntax.CreateUpdateCommand(dt);
                    da.UpdateCommand.Connection = (SqlConnection)tran.Connection;
                    da.UpdateCommand.Transaction = (SqlTransaction)tran;
                    da.DeleteCommand = SqlSyntax.CreateDeleteCommand(dt);
                    da.DeleteCommand.Connection = (SqlConnection)tran.Connection;
                    da.DeleteCommand.Transaction = (SqlTransaction)tran;
                    da.Update(dr);
                }
            }

            catch (Exception e)
            {
                Trace.WriteLine("Save error.");
                Trace.WriteLine(e);
                Trace.WriteLine(dt.TableName);
                dr.ToList().ForEach(r =>
                {
                    if (r.RowState == DataRowState.Added || r.RowState == DataRowState.Modified)
                    {
                        dt.Columns.Cast<DataColumn>().ToList().ForEach(c =>
                        {
                            Trace.WriteLine(c.ColumnName);
                            Trace.WriteLine(r[c].ToString());
                        });
                    }
                    else if (r.RowState == DataRowState.Deleted)
                        Trace.WriteLine("Deleting row...");
                });

                throw;
            }	
		}

        public override byte[] CleanDatasetDeletedData(DataSet ds, DataTable dt, byte[] ts, IDbConnection conn)
		{
            if (!dt.Columns.Contains("isDeleted")) return null;

            long startTicks = DateTime.Now.Ticks;
            var new_max_ts = new byte[] { };

            StringBuilder pk_col = new StringBuilder();
            foreach (DataColumn pk in dt.PrimaryKey)
            {
                if (pk.Ordinal > 0)
                    pk_col.Append(", ");
                pk_col.Append(pk.ToString());
            }            

            var cmd = string.Format("SELECT {0} FROM {1} WHERE isDeleted = @isDeleted", pk_col.ToString(), dt.TableName);
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.Parameters.AddWithValue("@isDeleted", 1);
            if (ts != null)
            {
                SqlParameter par = new SqlParameter();
                par.SqlDbType = SqlDbType.Timestamp;
                par.ParameterName = "@ts";
                par.Value = ts;
                command.Parameters.Add(par);
                cmd += " AND Versao >= @ts";
            }

            cmd += " ORDER BY Versao";
            command.CommandText = cmd;
            SqlDataReader reader = command.ExecuteReader();

            object[] res = new object[reader.FieldCount];
            DataView dv;
            DataRowView[] dr;

            dv = ds.Tables[dt.TableName].DefaultView;
            dv.ApplyDefaultSort = true;

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                    res[i] = reader.GetValue(i);

                dr = dv.FindRows(res);
                if (dr.Length > 0)
                    dt.Rows.Remove(dr[0].Row);
            }
            reader.Close();
            
            TimeSpan tspan = new TimeSpan(DateTime.Now.Ticks - startTicks);
            if (tspan.Seconds > 0)
                Debug.WriteLine("Find & remove on " + dt.TableName + " in " + tspan.ToString());

            return new_max_ts;
		}
	}
}