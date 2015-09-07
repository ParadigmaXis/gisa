using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
    public class SqlClientPaginatedListRule: PaginatedListRule
    {
        public override int CountPages(int itemsPerPage, out int numberOfItems, IDbConnection conn)
        {
            SqlDataReader reader;
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM #OrderedItems", (SqlConnection)conn);

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
                throw e;
            }

            numberOfItems = count;
            if (count % itemsPerPage != 0)
                return System.Convert.ToInt32(count / itemsPerPage) + 1;
            else
                return System.Convert.ToInt32(count / itemsPerPage);
        }

        public override int GetPageForID(long ID, int pageLimit, IDbConnection conn)
        {
            int id_seq = 0;
            using (SqlCommand command = new SqlCommand("", (SqlConnection)conn))
            {
                command.CommandText =
                    string.Format("SELECT seq_id FROM #OrderedItems WHERE ID={0}", ID);

                id_seq = System.Convert.ToInt32(command.ExecuteScalar());
            }
            return id_seq % pageLimit != 0 ? id_seq / pageLimit + 1 : id_seq / pageLimit;
        }
    }
}
