using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Diagnostics;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
	public class SqlClientConcorrenciaRule: ConcorrenciaRule
	{
        public override void fillRowsToOtherDataset(System.Data.DataTable table, System.Collections.ArrayList rows, System.Data.DataSet data, System.Data.IDbTransaction tran)
        {
            int contador = 0;
            StringBuilder s = new StringBuilder();
            data.Tables.Add(table.Clone());
            string baseSelectCommand = SqlSyntax.CreateSelectCommandText(table, "WHERE {0}", DBAbstractDataLayer.DataAccessRules.Syntax.DataDeletionStatus.All);
            using (SqlDataAdapter da = new SqlDataAdapter(new SqlCommand("", (SqlConnection)tran.Connection, (SqlTransaction)tran)))
            {
                // por cada linha da tabela modified ou deleted (as marcadas como added nao estao sujeitas a conflitos de concorrencia
                foreach (DataRow dr in rows)
                {
                    contador++;
                    s.Append("(");
                    s.Append(buildFilter(table, dr).ToString());
                    s.Append(")");
                    // impedir que na query entre 1 "or" a mais
                    if (contador % 200 != 0 && contador < rows.Count)
                        s.Append(" OR ");

                    // se ja tiverem sido obtidos 200 IDs, completar a query e executa-la e na base de dados
                    if (contador % 200 == 0 || contador == rows.Count)
                    {
                        // executar comando sql obtido
                        try
                        {
                            //System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(new System.Data.SqlClient.SqlCommand("", (System.Data.SqlClient.SqlConnection) tran.Connection, (System.Data.SqlClient.SqlTransaction) tran));
                            //da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommand(table, string.Format("where {0}", s), DBAbstractDataLayer.DataAccessRules.SqlClient.SqlSyntax.DataDeletionStatus.All);
                            da.SelectCommand.CommandText = string.Format(baseSelectCommand, s);
                            da.Fill(data, table.TableName);
                            // limpar filtro para o voltar a encher com os proximos 50
                            s = new System.Text.StringBuilder();
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine(string.Format("Erro ({0}): {1}", table.TableName, ex));
                            throw;
                        }
                    }
                }
            }
        }

        public override string buildFilter(DataTable dt, DataRow row)
        {
            return buildFilter(dt, row, false);
        }

        public override string buildFilterPK(DataTable dt, DataRow row)
        {
            return buildFilterPK(dt, row, false);
        }

        //função de filtragem por PK
        public override string buildFilterPK(DataTable dt, DataRow row, bool fillDataSetPorpose)
        {
            StringBuilder filter = new StringBuilder();

            foreach (DataColumn dc in dt.PrimaryKey)
            {
                if (dc != dt.PrimaryKey[0])
                    filter.Append(" AND ");

                if (dc.DataType == typeof(DateTime))
                    filter.AppendFormat("{0} = '{1:s}'", dc.ColumnName, row[dc.ColumnName]);
                else
                    filter.AppendFormat("{0} = '{1}'", dc.ColumnName, row[dc.ColumnName]);
            }

            if (filter != null)
                return filter.ToString();
            else
                return string.Empty;
        }

        // função que calcula o filtro a ser aplicado às pesquisas realizadas nos métodos getOriginalRowsDB, getLinhasConcorrentes, fillRowsToOtherDataset
        public override string buildFilter(DataTable dt, DataRow row, bool fillDataSetPorpose)
        {
            StringBuilder filter = null;

            // As linhas marcadas como added não tem valores originais motivo pelo qual são utilizados os valores actuais
            // no caso das linhas marcadas como deleted, os valores das suas colunas não estão acessiveis pelo que são necessários
            // os valores originais
            DataRowVersion version = DataRowVersion.Original;
            if (row.RowState == DataRowState.Added)
                version = DataRowVersion.Current;

            // só se pretende pesquisar pelos valores da chave primaria ou colunas únicas (correspondem às constraints do tipo unique)
            foreach (Constraint cs in dt.Constraints)
            {
                if (cs is UniqueConstraint)
                {
                    if (filter == null)
                        filter = new System.Text.StringBuilder();
                    else
                        filter.Append(" OR ");

                    UniqueConstraint uCS;
                    uCS = ((UniqueConstraint)(cs));
                    filter.Append("(");
                    foreach (DataColumn col in uCS.Columns)
                    {
                        if (col != uCS.Columns[0])
                            filter.Append(" AND ");

                        if (col.DataType == typeof(DateTime))
                            filter.AppendFormat("{0} = '{1:s}'", col.ColumnName, row[col.ColumnName, version]);
                        else if (col.DataType == typeof(string))
                            filter.AppendFormat("{0} = '{1}'", col.ColumnName, PesquisaRule.Current.sanitizeSearchTerm_WithoutWidcards(row[col.ColumnName, version].ToString()));
                        else if (col.DataType == typeof(long))
                            filter.AppendFormat("{0} = {1}", col.ColumnName, row[col.ColumnName, version]);
                        else if (col.DataType == typeof(byte))
                            filter.AppendFormat("{0} = {1}", col.ColumnName, row[col.ColumnName, version]);
                        else
                            filter.AppendFormat("{0} = '{1}'", col.ColumnName, row[col.ColumnName, version]);
                    }
                    filter.Append(")");
                }
            }

            if (filter != null)
                return filter.ToString();
            else
                return string.Empty;
        }

		// o argumento isParentOptional indica se deverão ser devolvidas apenas as rows 
		// cuja FK é nullable ou se deverão ser devolvidas apenas aquelas em que a FK é 
		// não nullable
		public override ConcorrenciaRule.ChildRelationRows getChildRowsFromDB(DataSet data, DataTable table, ArrayList parentRows, IDbTransaction tran)
		{
			ChildRelationRows result = new ChildRelationRows(new ArrayList(), new ArrayList());
			DataTable childTable;
			StringBuilder genericChildsFilter = new StringBuilder();
			string childsFilter = string.Empty;
			foreach (DataRelation childRelation in table.ChildRelations) 
			{
				DataColumn[] parentColumns;
				DataColumn[] childColumns;
				parentColumns = childRelation.ChildKeyConstraint.RelatedColumns;
				childColumns = childRelation.ChildKeyConstraint.Columns;
				for (int i = 0; i <= childColumns.Length - 1; i++) 
				{
					if (genericChildsFilter.Length > 0) 
						genericChildsFilter.Append(" AND ");
					genericChildsFilter.AppendFormat("{0}={1}", childColumns[i].ColumnName, "{" + i.ToString() + "}");
				}
				foreach (DataRow parentRow in parentRows) 
				{
					ArrayList relationParentValues = new ArrayList();
					for (int i = 0; i <= parentColumns.Length - 1; i++) 
					{
						DataColumn parentColumn = parentColumns[i];
						// as parentRow podem pertencer ao dataset de trabalho ou aos dados que estamos agora a recolher da BD. Apenas no 1º caso o rowstate será deleted.
						if (parentRow.RowState == DataRowState.Deleted)
							relationParentValues.Add(parentRow[parentColumn.ColumnName, DataRowVersion.Original]);
						else
							relationParentValues.Add(parentRow[parentColumn.ColumnName]);
					}
					childsFilter = string.Format("WHERE " + genericChildsFilter.ToString(), relationParentValues.ToArray());
					childTable = new DataTable(childRelation.ChildTable.TableName);
                    using (var command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlTransaction)tran))
                    using (var da = new SqlDataAdapter(command))
                    {
                        da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(childRelation.ChildTable, childsFilter);
                        da.Fill(childTable);
                    }
					result.tables.Add(childTable);
					result.relationColumns.Add(childColumns);
				}
				genericChildsFilter = new StringBuilder();
				childsFilter = string.Empty;
			}
			return result;
		}

		public override void FillTableInGetLinhasConcorrentes(DataSet ds, DataTable table, string query, DBAbstractDataLayer.DataAccessRules.Syntax.DataDeletionStatus status, IDbTransaction tran)
		{
			try 
			{
				if (ds.Tables[table.TableName] == null)
					ds.Tables.Add(table.Clone());

                using (var command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlTransaction)tran))
                using (var da = new SqlDataAdapter(command))
                {
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(table, " WITH (UPDLOCK) " + query, status);
                    da.Fill(ds, table.TableName);
                }
			}
			catch (Exception e)
			{
				Trace.WriteLine(table + ": " + e.Message);
				throw e;
			}
		}

		public override string getQueryForRows(DataRow[] rows, params DataRowState[] rowStates)
		{
			System.Text.StringBuilder childFilter = new System.Text.StringBuilder();
			try 
			{
				int counter = 0;
				foreach (DataRow row in rows) 
				{
					counter++;
					if (rowStates.Length == 0 || Array.IndexOf(rowStates, row.RowState) != -1) 
					{
						if (childFilter.Length > 0) 
						{
							if (counter % 100 == 0)
							{
								childFilter.Append(") OR (");
							}
							else
							{
								childFilter.Append(" OR ");							
							}
						} else {
							childFilter.Append("(");
						}
						foreach (DataColumn childRowColumn in row.Table.PrimaryKey) 
						{
							if (childRowColumn.Ordinal != 0) 
							{
								childFilter.Append(" AND ");
							}
							childFilter.Append(childRowColumn.ColumnName);
							childFilter.Append(" = '");
							if (childRowColumn.DataType == typeof(DateTime)) 
							{
								childFilter.AppendFormat("{0:yyyy-MM-dd HH:mm:ss}", row[childRowColumn.Ordinal]);
							} 
							else 
							{
								childFilter.Append(row[childRowColumn.Ordinal]);
							}
							childFilter.Append("'");
						}
					}
				}
				if (childFilter.Length > 0)
				{
					childFilter.Append(")");
				}
			}
			catch (Exception e)
			{
				Trace.WriteLine(e);
				throw;
			}

			if (childFilter.Length == 0) 
			{
				return "0=1";
			} else {
				return childFilter.ToString();
			}
		}
	}
}
