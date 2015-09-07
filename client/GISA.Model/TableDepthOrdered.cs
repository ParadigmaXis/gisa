using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

using GISA.Model;
namespace GISA.Model
{
	public class TableDepthOrdered
	{
		public enum TableCloudType: int
		{   
            All = 0,
			FRD = 1,
			CA = 2,
			NVL = 3
		}

		public struct tableDepth
		{
			public DataTable tab;
			public int dep;
			public TableCloudType nuvem;
			public tableDepth(DataTable tab, int dep, TableCloudType nuvem)
			{
				this.tab = tab;
				this.dep = dep;
				this.nuvem = nuvem; //necessario para as datas de produção
			}
		}

		private ArrayList orderedTables = new ArrayList();
		private ArrayList reverseOrderedTables = new ArrayList();
		public ArrayList nuvemCA = new ArrayList();
		public ArrayList nuvemFRD = new ArrayList();
		public ArrayList nuvemNVL = new ArrayList();		

		//returna um arraylist com as tabelas ordenadas para inserts e updates
		public ArrayList getTabelasOrdenadas()
		{
			return getTabelasOrdenadas(false);
		}

        public ArrayList getTabelasOrdenadas(bool ordemInversa)
		{
			if (orderedTables.Count == 0 || reverseOrderedTables.Count == 0)
			{
				orderedTables.Clear();
				reverseOrderedTables.Clear();

				tableDepth depth = new tableDepth();
				calculaNuvens();

				//determinar as tabelas que n tem parent relations logo aquelas que tem profundidade 1
				foreach (DataTable dt in GisaDataSetHelper.GetInstance().Tables)
				{
					if (dt.ParentRelations.Count < 1)
						orderedTables.Add(new tableDepth(dt, 1, tabPertenceNuvem(dt.TableName)));
				}

				//adiconar as childtables a lista
				int tamInicial = orderedTables.Count;
				int tempFor1 = tamInicial;
				for (int i = 0; i < tempFor1; i++)
				{
					depth = (tableDepth)(orderedTables[i]);
					if (depth.tab.ChildRelations.Count > 0)
					{
						chamaMetodo(depth.tab, 2);
					}
				}

				orderedTables.Sort(new TableDepthSorter());
				reverseOrderedTables.AddRange(orderedTables);
				reverseOrderedTables.Reverse();
				//imprimeListaTabelas();

			}
			if (! ordemInversa)
			{
				return orderedTables;
			}
			else
			{
				return reverseOrderedTables;
			}
		}

		//metodo recursivo que calcula a profundidade de cada tabela e insere-as no array ar
		private void chamaMetodo(DataTable dt, int pr)
		{
			DataTable childTable = null;
			int tempFor1 = dt.ChildRelations.Count;
			for (int i = 0; i < tempFor1; i++)
			{
				childTable = dt.ChildRelations[i].ChildTable;
				if (childTable.ChildRelations.Count > 0 && ! (dt == childTable))
				{
					chamaMetodo(childTable, pr + 1);
				}
				if (! (dt == childTable))
				{
					listaTabela(childTable, pr);
				}
			}
		}

		//adiciona uma tabela a lista com a respectiva profundidade
		private void listaTabela(DataTable dt, int pr)
		{
			tableDepth r = new tableDepth();
			foreach (object i in orderedTables)
			{
				r = (tableDepth)i;
				if (r.tab == dt)
				{
					if (r.dep < pr)
					{
						orderedTables.Remove(i);
						orderedTables.Add(new tableDepth(dt, pr, tabPertenceNuvem(dt.TableName)));
						return;
					}
					return;
				}
			}
			orderedTables.Add(new tableDepth(dt, pr, tabPertenceNuvem(dt.TableName)));
		}

		//retorna true se uma tabela está relacionada com ela propria
		private bool isSelfChild(DataTable dt)
		{
			DataTable tab = null;
			if (dt.ChildRelations.Count > 0)
			{
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of dt.ChildRelations.Count for every iteration:
				int tempFor1 = dt.ChildRelations.Count;
				for (int i = 0; i < tempFor1; i++)
				{
					tab = dt.ChildRelations[i].ChildTable;
					if (dt == tab)
					{
						return true;
					}
				}
			}
			return false;
		}

		private void imprimeListaTabelas()
		{
			tableDepth r = new tableDepth();
			foreach (object i in orderedTables)
			{
				r = (tableDepth)i;
				//Console.WriteLine(String.Format("{0}  ----   DELETE FROM {1} WHERE isDeleted = 1", r.dep, r.tab));
				//Console.WriteLine(String.Format("DELETE FROM {0}", r.tab));
				//Console.WriteLine(string.Format("INSERT INTO #OrderedTables VALUES({0}, {1})", r.tab, r.dep));				
				//Console.WriteLine(string.Format("\"{0}\",", r.tab));
/*                
                StringBuilder pk_cols = new StringBuilder();
                StringBuilder inner_joins = new StringBuilder();
                foreach (DataColumn pk in r.tab.PrimaryKey)
                {
                    //pkList.Add(pk.ToString());
                    if (pk.Ordinal > 0)
                    {
                        pk_cols.Append(", ");
                        inner_joins.Append("AND ");
                    }
                    pk_cols.Append(pk.ToString());
                    inner_joins.AppendFormat("i.{0} = d.pk{1} ", pk.ToString(), pk.Ordinal);
                }
                var a = string.Format(@"
CREATE TRIGGER {0}_on_delete ON {0} AFTER UPDATE AS 
    IF UPDATE (isDeleted)
        INSERT INTO deleted_history_{1}
        SELECT '{0}', {2} FROM inserted where isDeleted = 0
        DELETE FROM deleted_history_{1} FROM deleted_history_{1} d INNER JOIN inserted i ON {3} AND i.isDeleted = 0 AND d.table_name = '{0}';
GO", r.tab.TableName, r.tab.PrimaryKey.Length, pk_cols.ToString(), inner_joins.ToString());
                Console.WriteLine(a);
*/
                StringBuilder pk_cols = new StringBuilder();
                foreach (DataColumn pk in r.tab.PrimaryKey)
                {
                    //pkList.Add(pk.ToString());
                    if (pk.Ordinal > 0)
                    {
                        pk_cols.Append(", ");
                    }
                    pk_cols.Append(pk.ToString());
                }

                var a = string.Format("CREATE INDEX IX_isDeleted ON {0} (Versao) INCLUDE ({1}) WHERE isDeleted = 1", r.tab.TableName, pk_cols.ToString());
                Console.WriteLine(a);
			}
		}

		//metodo que tem como objectivo criar dois arraylists onde mantem a lista de tabelas relaccionadas com 
		//as tabelas ControloAut e FRDBase
		private void calculaNuvens()
		{
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of GisaDataSetHelper.GetInstance().Tables("ControloAut").ChildRelations.Count for every iteration:
			int tempFor1 = GisaDataSetHelper.GetInstance().Tables["ControloAut"].ChildRelations.Count;
			for (int i = 0; i < tempFor1; i++)
			{
				if (GisaDataSetHelper.GetInstance().Tables["ControloAut"].ChildRelations[i].ChildTable.ChildRelations.Count > 0)
				{
					calculaNuvens(GisaDataSetHelper.GetInstance().Tables["ControloAut"].ChildRelations[i].ChildTable, TableCloudType.CA);
				}
				if (! (nuvemCA.Contains(GisaDataSetHelper.GetInstance().Tables["ControloAut"].ChildRelations[i].ChildTable.TableName)))
				{
					nuvemCA.Add(GisaDataSetHelper.GetInstance().Tables["ControloAut"].ChildRelations[i].ChildTable.TableName);
				}
			}
			nuvemCA.Add("ControloAut");

//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of GisaDataSetHelper.GetInstance().Tables("FRDBase").ChildRelations.Count for every iteration:
			int tempFor2 = GisaDataSetHelper.GetInstance().Tables["FRDBase"].ChildRelations.Count;
			for (int j = 0; j < tempFor2; j++)
			{
				if (GisaDataSetHelper.GetInstance().Tables["FRDBase"].ChildRelations[j].ChildTable.ChildRelations.Count > 0)
				{
					calculaNuvens(GisaDataSetHelper.GetInstance().Tables["FRDBase"].ChildRelations[j].ChildTable, TableCloudType.FRD);
				}
				if (! (nuvemFRD.Contains(GisaDataSetHelper.GetInstance().Tables["FRDBase"].ChildRelations[j].ChildTable.TableName)))
				{
					nuvemFRD.Add(GisaDataSetHelper.GetInstance().Tables["FRDBase"].ChildRelations[j].ChildTable.TableName);
				}
			}
			nuvemFRD.Add("FRDBase");

//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of GisaDataSetHelper.GetInstance().Tables("FRDBase").ChildRelations.Count for every iteration:
			int tempFor3 = GisaDataSetHelper.GetInstance().Tables["Nivel"].ChildRelations.Count;
			for (int j = 0; j < tempFor3; j++)
			{
				if (GisaDataSetHelper.GetInstance().Tables["Nivel"].ChildRelations[j].ChildTable.ChildRelations.Count > 0)
				{
					calculaNuvens(GisaDataSetHelper.GetInstance().Tables["Nivel"].ChildRelations[j].ChildTable, TableCloudType.NVL);
				}
				if (! (nuvemNVL.Contains(GisaDataSetHelper.GetInstance().Tables["Nivel"].ChildRelations[j].ChildTable.TableName)))
				{
					nuvemNVL.Add(GisaDataSetHelper.GetInstance().Tables["Nivel"].ChildRelations[j].ChildTable.TableName);
				}
			}
			nuvemNVL.Add("Nivel");
		}

		private void calculaNuvens(DataTable dt, TableCloudType nv)
		{
			if (nv == TableCloudType.CA)
			{
				int tempFor1 = dt.ChildRelations.Count;
				for (int i = 0; i < tempFor1; i++)
				{
					if (dt.ChildRelations[i].ChildTable.ChildRelations.Count > 0)
					{
						calculaNuvens(dt.ChildRelations[i].ChildTable, TableCloudType.CA);
					}
					if (! (nuvemCA.Contains(dt.ChildRelations[i].ChildTable.TableName)))
					{
						nuvemCA.Add(dt.ChildRelations[i].ChildTable.TableName);
					}
				}
			}
			else if (nv == TableCloudType.FRD)
			{
				int tempFor2 = dt.ChildRelations.Count;
				for (int i = 0; i < tempFor2; i++)
				{
					if (dt.ChildRelations[i].ChildTable.ChildRelations.Count > 0)
					{
						calculaNuvens(dt.ChildRelations[i].ChildTable, TableCloudType.FRD);
					}
					if (! (nuvemFRD.Contains(dt.ChildRelations[i].ChildTable.TableName)))
					{
						nuvemFRD.Add(dt.ChildRelations[i].ChildTable.TableName);
					}
				}
			}
			else
			{
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of dt.ChildRelations.Count for every iteration:
				int tempFor3 = dt.ChildRelations.Count;
				for (int i = 0; i < tempFor3; i++)
				{
					if (dt.ChildRelations[i].ChildTable.ChildRelations.Count > 0)
					{
						calculaNuvens(dt.ChildRelations[i].ChildTable, TableCloudType.NVL);
					}
					if (! (nuvemNVL.Contains(dt.ChildRelations[i].ChildTable.TableName)))
					{
						nuvemNVL.Add(dt.ChildRelations[i].ChildTable.TableName);
					}
				}
			}
		}

		//metodo que identifica a qual nuvem a tabela dt pertence
		private TableCloudType tabPertenceNuvem(string tab)
		{
			if (nuvemFRD.Contains(tab))
			{
				return TableCloudType.FRD;
			}
			if (nuvemCA.Contains(tab))
			{
				return TableCloudType.CA;
			}
			if (nuvemNVL.Contains(tab))
			{
				return TableCloudType.NVL;
			}
			//INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
			return 0;
		}

	}

	public class TableDepthSorter : IComparer
	{

		public int Compare(object obj1, object obj2)
		{

			if (((TableDepthOrdered.tableDepth)obj1).dep > ((TableDepthOrdered.tableDepth)obj2).dep)
			{
				return 1;
			}
			else if (((TableDepthOrdered.tableDepth)obj1).dep < ((TableDepthOrdered.tableDepth)obj2).dep)
			{
				return -1;
			}

			return 0;
		}
	}

} //end of root namespace