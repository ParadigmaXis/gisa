using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Model
{
	public class Concorrencia
	{
		//arraylist que mantem uma lista com as tabelas que não contem nenhuma relação com outras do tipo 1 para 1
		private static ArrayList mUndeletableTables = null;
		private static ArrayList UndeletableTables
		{
			get
			{
				if (mUndeletableTables == null)
				{
					mUndeletableTables = new ArrayList();
					foreach (DataTable dt in GisaDataSetHelper.GetInstance().Tables)
					{
						if (! (has1To1Relations(dt)))
							mUndeletableTables.Add(dt);
					}
				}
				return mUndeletableTables;
			}
		}

		private static DataSet mOriginalRowsDB = null;
		public static DataSet OriginalRowsDB
		{
            get { return mOriginalRowsDB; }
            set { mOriginalRowsDB = value; }
		}

		private static System.Text.StringBuilder mStrConcorrenciaUser = new System.Text.StringBuilder();
		public static System.Text.StringBuilder StrConcorrenciaUser
		{
            get { return mStrConcorrenciaUser; }
            set { mStrConcorrenciaUser = value; }
		}

		private static System.Text.StringBuilder mStrConcorrenciaBD = new System.Text.StringBuilder();
		public static System.Text.StringBuilder StrConcorrenciaBD
		{
            get { return mStrConcorrenciaBD; }
            set { mStrConcorrenciaBD = value; }
		}

		private static System.Text.StringBuilder mStrConcorrenciaLinhasNaoGravadas = new System.Text.StringBuilder();
		public static System.Text.StringBuilder StrConcorrenciaLinhasNaoGravadas
		{
            get { return mStrConcorrenciaLinhasNaoGravadas; }
            set { mStrConcorrenciaLinhasNaoGravadas = value; }
		}

		public DataSet mGisaBackup = null;
		public DataSet gisabackup
		{
            get { return mGisaBackup; }
		}

		//estrutura que permite guardar as linhas alteradas de uma tabela
		//recorri a utilização de 2 arrayslists de forma a poder ter uma separação entre as linhas marcadas como added e modified e as aquelas marcadas como deleted.
		//isto de forma a facilitar posteriormente aquando da gravação já que também é executada 2 duas fases

		//apesar de ser a solução mais desejada, o uso dos arrays causava vários problemas. é necessário na altura da declaração da variavel 
		//indicar o seu tamanho máximo. isto levava a que todas as posições do array eram inicializadas. isto levantava um problema dado
		//que havia a possibilidade de essas posições não serem ocupadas com rows o que na altura do fill causava problemas
		public struct changedRows
		{
			public string tab;
			public ArrayList rowsAdd;
			public ArrayList rowsMod;
			public ArrayList rowsDel;
			public changedRows(string tab, ArrayList rowsAdd, ArrayList rowsMod, ArrayList rowsDel)
			{
				this.tab = tab;
				this.rowsAdd = rowsAdd;
				this.rowsMod = rowsMod;
				this.rowsDel = rowsDel;
			}
		}

		//varios selects com blocos de IDs
		public DataSet getOriginalRowsDB(ArrayList changedrows, IDbTransaction tran)
		{

			//Este metodo tem como objectivo obter as linhas da base de dados correspondentes aquelas
			//alteradas na interface (modified e deleted. essas linhas sao colocadas no dataset 
			//originalRowsDS.Estas() sao obtidas em blocos, isto é, com um select é obtido um conjunto
			//de linhas. Isto porque, com um único select ocorre um stack overflow qd o nro de linhas 
			//pretendido é mto elevado (30000). Linha a linha, o tempo de processamento é muito elevado, 
			//aproximadamente 27s.

			//bloco de 50 linhas: entre 6 e 7 seg
			//bloco de 500 linhas (sempre para a mesma tabela e mm nro de linhas): 
			//                   1ª passagem - 30S
			//                   passagens seguintes - 1s        

			//nRows: variavel onde fica guardado o nro de linhas da tabela que esta a ser processada 
			//       proveniente do dataset originalRowsDS
			//contador: variavel de apoio para o processamento de blocos de linhas a serem obtidas da base de
			//   dados
			//j: variavel iteradora sobre as colunas que compoem a chave primaria de uma coluna
			//s: string que vai conter o filtro dos select
			//pk: string que vai conter o nome da(s) coluna(s) que compoe(m) a chave primaria das
			//       tabelas

			DataTable dt = null;
			//Dim pk As System.Text.StringBuilder
			ArrayList rows = new ArrayList(); //array que vai conter as linhas marcadas como added, modified e deleted
			ArrayList childRows = new ArrayList(); // array que irá contar as linhas filho que tb devem ser carregadas (as filhas das deleted, uma vez que também terão de ser apagadas)

			DataSet origRowsDB = new DataSet();
            origRowsDB.EnforceConstraints = false;
			origRowsDB.CaseSensitive = true;

			bool haColunasConcorrentes = false;

			long startTicks = 0;
			startTicks = DateTime.Now.Ticks;

			//por cada tabela
			foreach (changedRows tab in changedrows)
			{
				rows.Clear();
				dt = GisaDataSetHelper.GetInstance().Tables[tab.tab];

				if (tab.rowsAdd.Count + tab.rowsMod.Count + tab.rowsDel.Count > 0)
				{
					rows.AddRange(tab.rowsMod);
					rows.AddRange(tab.rowsAdd);
					rows.AddRange(tab.rowsDel);
					ConcorrenciaRule.Current.fillRowsToOtherDataset(dt, rows, origRowsDB, tran); // preencher o dataset com as linhas da BD a partir das linhas adicionadas/alteradas/eliminadas em memória
				}
			}

			Debug.WriteLine("Get changed rows from DB: " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());

			mOriginalRowsDB = origRowsDB;
			startTicks = DateTime.Now.Ticks;
			haColunasConcorrentes = getLinhasConcorrentes(changedrows, tran);
			Debug.WriteLine("Get linhas concorrentes: " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());

			//so no caso de existirem linhas concorrentes é que o dataset com as linhas provenientes da BD é retornado
			//estrategia necessaria para facilitar, na altura de gravação, o tratamento dos conflitos 
			if (haColunasConcorrentes)
				return mOriginalRowsDB;
			else
				return null;
		}

		//funcao que retorna um arraylist com todas as linhas marcadas como modified, added e deleted provenientes do dataset principal
		public ArrayList getAlteracoes(DataSet ds, ArrayList sortedTables)
		{
			long start = 0;
			start = DateTime.Now.Ticks;

			DataTable dt = null;
			ArrayList changedRowsArrayList = new ArrayList();

			DataSet gisaBackup = new DataSet();
            gisaBackup.EnforceConstraints = false;
			gisaBackup.CaseSensitive = true;

			//processamento efectuado tabela a tabela (o array resultante desta operação já vai ordenado para o save de linhas marcadas como added e deleted)
			foreach (object o in sortedTables)
			{
				dt = GisaDataSetHelper.GetInstance().Tables[((TableDepthOrdered.tableDepth)o).tab.TableName];
				//so prossegue a operação se existir alguma linha alterada
				if (dt.Select("", "", DataViewRowState.ModifiedOriginal | DataViewRowState.Added | DataViewRowState.Deleted).Length > 0)
				{
					//array de suporte onde vao ser guardadas as linhas marcadas como modified e added
					ArrayList modif = new ArrayList();

					if (! (gisaBackup.Tables.Contains(dt.TableName)))
						gisaBackup.Tables.Add(dt.Clone());

					//por cada linha modificada verifica se foi realmente alterada; se sim, adiciona-se ao array
					DataRow[] rows = dt.Select("", "", DataViewRowState.ModifiedOriginal);
					foreach (DataRow dr in rows)
					{
						if (! (isModifiedRow(dr)))
							dr.AcceptChanges();
						else
						{
							modif.Add(dr);
							gisaBackup.Tables[dt.TableName].ImportRow(dr);
						}
					}

					ArrayList add = new ArrayList();
					//adicionar as linhas marcadas como added ao array
					foreach (DataRow dr in dt.Select("", "", DataViewRowState.Added))
					{
						add.Add(dr);
						gisaBackup.Tables[dt.TableName].ImportRow(dr);
					}

					//adicionar as linhas marcadas como deleted ao array
					ArrayList del = new ArrayList();
					foreach (DataRow dr in dt.Select("", "", DataViewRowState.Deleted))
					{
						del.Add(dr);
						gisaBackup.Tables[dt.TableName].ImportRow(dr);
					}

					//adiciona uma tabela com as respectivas linhas alteradas ao arraylist que mantem a lista de todas as linhas alteradas
					add.TrimToSize();
					modif.TrimToSize();
					del.TrimToSize();

					if (add.Count > 0 || modif.Count > 0 || del.Count > 0)
					{
						changedRowsArrayList.Add(new changedRows(dt.TableName, add, modif, del));
						mGisaBackup = gisaBackup;
					}
				}
			}
			Debug.WriteLine("<<getAlteracoes>>: " + new TimeSpan(DateTime.Now.Ticks - start).ToString());
			return changedRowsArrayList;
		}

        // ToDo: colocar método numa classe responsável por registar alterações feitas sobre CAs, UFs e Niveis
        public static bool WasRecordModified(DataRow record)
        {
            DataRow row;
            Queue<DataRow> rows = new Queue<DataRow>();
            rows.Enqueue(record);

            while (rows.Count > 0)
            {
                row = rows.Dequeue();

                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Deleted ||
                    (row.RowState == DataRowState.Modified && Concorrencia.isModifiedRow(row)))

                    return true;

                foreach (DataRelation drel in row.Table.ChildRelations)
                {
                    foreach (DataRow drow in row.GetChildRows(drel, DataRowVersion.Current))
                        rows.Enqueue(drow);

                    foreach (DataRow drow in row.GetChildRows(drel, DataRowVersion.Original))
                        rows.Enqueue(drow);
                }
            }

            return false;
        }

		//funcao que verifica se uma determinada row foi realmente alterada; se as versoes original e current sao diferentes
		//a versao retorna true
		public static bool isModifiedRow(DataRow row)
		{
			if (row.RowState != DataRowState.Modified && row.RowState != DataRowState.Unchanged)
				return true;

			foreach (DataColumn column in row.Table.Columns){

                if (column.DataType == typeof(byte[]))
                {
                    var orig = row[column, DataRowVersion.Original] as byte[];
                    var curr = row[column, DataRowVersion.Current] as byte[];

                    if (orig != null && !orig.SequenceEqual(curr))
                        return true;
                }
				else if (!(row[column, DataRowVersion.Original].Equals(row[column, DataRowVersion.Current])))
                {
					if (!((object.ReferenceEquals(row[column, DataRowVersion.Original], DBNull.Value) && row[column, DataRowVersion.Current] is string && row[column, DataRowVersion.Current].Equals("")) || (object.ReferenceEquals(row[column, DataRowVersion.Current], DBNull.Value) && row[column, DataRowVersion.Original] is string && row[column, DataRowVersion.Original].Equals(""))))
						return true;
				}
				else if (RowsChangedToModified != null && RowsChangedToModified.Contains(row))
					return true;
			}			
			return false;
		}

		public void ClearRowsChangedToModified()
		{
			if (RowsChangedToModified != null)
				RowsChangedToModified.Clear();
		}

		// Variável que vai manter todas as rows cujo rowstate, inicialmente added, passou a ser modified. Num conflito
		// de concorrência sobre estas linhas added, caso o utilizador optar pela opção cancelar, estas linhas não eram
		// consideradas como alteradas no save seguinte uma vez que os valores das suas colunas actual e original eram 
		// iguais (resultado resultado da mudança do valor do rowstate)
        private static Hashtable RowsChangedToModified = new Hashtable();
		public bool getLinhasConcorrentes(ArrayList changedRows, IDbTransaction tran)
		{			
			ArrayList rows = new ArrayList();
			DataTable dt = null;
			string s = null;
			DataRow row = null;
			DataRow row2 = null;
			//Dim resurrectedRows As New ArrayList ' contém rows do dataset originalRowsDB que serão ressuscitadas
			Hashtable resurrectedRows = new Hashtable();

			System.Text.StringBuilder msgConcorrencia = new System.Text.StringBuilder();
			System.Text.StringBuilder str = new System.Text.StringBuilder();
			int c = 0;

			//mensagem que indica quais os campos que não puderam ser gravados em memoria
			System.Text.StringBuilder infoNaoGravada = new System.Text.StringBuilder();

			//iteracao sobre as tabelas linhas alteradas
			foreach (changedRows tab in changedRows)
			{
				rows.Clear();
				rows.AddRange(tab.rowsAdd);
				//dt usado para obter o nome das colunas da tabela actual
				dt = GisaDataSetHelper.GetInstance().Tables[tab.tab];

				//iteração sobre as linhas com estado "added"
				foreach (DataRow rowWithinLoop in rows)
				{
                    //row = rowWithinLoop;

                    //s = ConcorrenciaRule.Current.buildFilter(dt, rowWithinLoop);

                    row = rowWithinLoop;
                    s = ConcorrenciaRule.Current.buildFilter(dt, rowWithinLoop);
                    DataRow originalRow = GetOriginalRow(dt, rowWithinLoop);

					//se existe uma linha na BD com a mesma PK ou unique
					//if (OriginalRowsDB.Tables[tab.tab].Select(s.ToString()).Length > 0)
                    if (originalRow != null)
					{
						row2 = originalRow;
						if ((byte)row2["isDeleted"] == 0)
						{
							//a linha existe e não está marcada como deleted -> a linha a inserir é tratada como se já tivesse side criada anteriormente e o utilizador quisesse editá-la

							changeRowStateToModified(rowWithinLoop, tab, dt);
							//actualiza a chave primária negativa (se for esse o caso) por aquela que já existe na BD
							updateRow(rowWithinLoop, row2);

							//actualizar todas as childrows: 
							// - como a linha actual a ser adicionada já tem uma equivalente na BD (caso daquelas que são túpulos de tabelas com colunas unique) é necessario alterar o seu estado para modified
							// - as suas childrows também vem o seu estado mudado para modified caso também tenham uma linha correspondente na BD (como o ID das childrows inicialmente era negativo não foi possivel verificar se existiam linhas correspondentes na BD, pelo que é necessário voltar a obtê-las utilizando os IDs positivos)
							// - é necessário mudar igualmente o estado das childrows para "undeleted" (Exemplo: utilizador muda a forma autorizada para um termo novo e posteriormente volta a mudar para o termo antigo -> as linhas marcadas como deleted podem ainda estar na BD e portanto deve-se impedir que sejam adicionadas outras cmo o mesmo ID de forma a evitar excepções nas chaves)
							foreach (DataRelation relation in dt.ChildRelations)
							{
								// obter todas as childrows da row em causa
								DataRow[] childRows = null;
								childRows = rowWithinLoop.GetChildRows(relation.RelationName);

								// gerar um filtro que selecciona apenas as childrows "added"
								string childFilter = string.Empty;
								childFilter = ConcorrenciaRule.Current.getQueryForRows(childRows, DataRowState.Added);

								if (childRows.Length > 0)
								{
									// preencher o dataset originalRowsDB com as childrows                                
									ConcorrenciaRule.Current.FillTableInGetLinhasConcorrentes(OriginalRowsDB, GisaDataSetHelper.GetInstance().Tables[relation.ChildTable.TableName], string.Format("WHERE {0} ", childFilter), DBAbstractDataLayer.DataAccessRules.SqlClient.SqlSyntax.DataDeletionStatus.All, tran);

									// gerar filtro que seleccionará as childrows "added" no dataset 
									// de trabalho que estejam em conflito com rows na BD
									string conflictingRows = null;
									conflictingRows = ConcorrenciaRule.Current.getQueryForRows(GisaDataSetHelper.GetInstance().Tables[relation.ChildTable.TableName].Select(childFilter), DataRowState.Unchanged);

									if (conflictingRows.Length > 0)
									{
										// sao consideradas apenas as rows como added apesar das linhas 
										// obtidas pelo filtro são já necessáriamente apenas as novas
										foreach (DataRow drow in GisaDataSetHelper.GetInstance().Tables[relation.ChildTable.TableName].Select(conflictingRows, "", DataViewRowState.Added))
										{
											// alterar o estado de added para modified
											changedRows r = getChangedRowsElement(changedRows, relation.ChildTable.TableName);
											changeRowStateToModified(drow, r, relation.ChildTable);
											drow["isDeleted"] = 0;
										}
									}
								}
							}
						}
						else if ((byte)row2["isDeleted"] == 1 && (! (UndeletableTables.Contains(rowWithinLoop.Table)) || (UndeletableTables.Contains(rowWithinLoop.Table) && ! (parentsExists(rowWithinLoop)))))
						{
							//a linha existe, está marcada como deleted e não é possível ressuscitá-la porque ou a sua tabela tem pelo menos um relação de 1 para 1 com outra tabela, ou não tem nenhuma relação desse tipo mas a parent row não existe
							//o tratamento desta situação é idêntico às linhas marcadas como modified mas não existem as correspondentes na BD

							//MessageBox.Show(String.Format("A informação relativa a {0} não pode ser guardada devido ao facto de o contexto ter sido apagado por outro utilizador.", MetaModelHelper.getFriendlyName(row.Table.TableName)))

							if (infoNaoGravada.Length > 0)
								infoNaoGravada.Append("; ");

							infoNaoGravada.Append(MetaModelHelper.getFriendlyName(rowWithinLoop.Table.TableName));
							
							removeChildRows(rowWithinLoop, changedRows);

							tab.rowsAdd.Remove(rowWithinLoop);
							rowWithinLoop.Table.Rows.Remove(rowWithinLoop);


						}
						else if ((byte)row2["isDeleted"] == 1 && UndeletableTables.Contains(rowWithinLoop.Table) && parentsExists(rowWithinLoop))
						{
							//a linha existe, está marcada como deleted e é possível ressuscitá-la porque porque a sua tabela não tem nenhuma relação do tipo 1 para 1 com outro e a sua parent row existe
							//nesta situação a linha é ressuscitada

							// no caso de a linha existir na BD e estar marcada como deleted é necessario voltar a mudar o seu estado para undeleted (isDeleted=false)
							rowWithinLoop["isDeleted"] = 0; //ToDo: verificar a utilidade desta instrução

							changeRowStateToModified(rowWithinLoop, tab, dt);
							//actualiza a chave primária negativa (se for esse o caso) por aquela que já existe na BD
							updateRow(rowWithinLoop, row2);

							resurrectedRows.Add(row2, row2);

							//actualizar todas as childrows: 
							// - como a linha actual a ser adicionada já tem uma equivalente na BD (caso daquelas que são túpulos de tabelas com colunas unique) é necessario alterar o seu estado para modified
							// - as suas childrows também vem o seu estado mudado para modified caso também tenham uma linha correspondente na BD (como o ID das childrows inicialmente era negativo não foi possivel verificar se existiam linhas correspondentes na BD, pelo que é necessário voltar a obtê-las utilizando os IDs positivos)
							// - é necessário mudar igualmente o estado das childrows para "undeleted" (Exemplo: utilizador muda a forma autorizada para um termo novo e posteriormente volta a mudar para o termo antigo -> as linhas marcadas como deleted podem ainda estar na BD e portanto deve-se impedir que sejam adicionadas outras cmo o mesmo ID de forma a evitar excepções nas chaves)
							foreach (DataRelation relation in dt.ChildRelations)
							{
								// obter todas as childrows da row em causa
								DataRow[] childRows = null;
								childRows = rowWithinLoop.GetChildRows(relation.RelationName);

								// gerar um filtro que selecciona apenas as childrows "added"
								string childFilter = null;
								childFilter = ConcorrenciaRule.Current.getQueryForRows(childRows, DataRowState.Added);

								if (childRows.Length > 0)
								{
									// preencher o dataset originalRowsDB com as childrows
									ConcorrenciaRule.Current.FillTableInGetLinhasConcorrentes(OriginalRowsDB, GisaDataSetHelper.GetInstance().Tables[relation.ChildTable.TableName], string.Format("where {0}", childFilter), DBAbstractDataLayer.DataAccessRules.SqlClient.SqlSyntax.DataDeletionStatus.All, tran);

									// gerar filtro que seleccionará as childrows "added" no dataset 
									// de trabalho que estejam em conflito com rows na BD
									string conflictingRows = null;
									conflictingRows = ConcorrenciaRule.Current.getQueryForRows(GisaDataSetHelper.GetInstance().Tables[relation.ChildTable.TableName].Select(childFilter), DataRowState.Unchanged);

									if (conflictingRows.Length > 0)
									{
										// sao consideradas apenas as rows como added apesar das linhas 
										// obtidas pelo filtro são já necessáriamente apenas as novas
										foreach (DataRow drow in GisaDataSetHelper.GetInstance().Tables[relation.ChildTable.TableName].Select(conflictingRows, "", DataViewRowState.Added))
										{
											// alterar o estado de added para modified
											changedRows r = getChangedRowsElement(changedRows, relation.ChildTable.TableName);

											DataRow origRow = OriginalRowsDB.Tables[relation.ChildTable.TableName].Select(ConcorrenciaRule.Current.buildFilter(relation.ChildTable, drow).ToString())[0];
											changeRowStateToModified(drow, r, relation.ChildTable);
											if ((byte)origRow["isDeleted"] == 1)
											{
												drow["isDeleted"] = 0;
												resurrectedRows.Add(origRow, origRow);
											}
										}
									}
								}
							}
						}
					}
					else
					{
						//a linha a ser adicionada não tem nenhuma correspondente na BD mas no entanto é necessario verificar se as linhas pai correspondentes existem (se for o caso)

						// No caso de não existirem linhas pai (mas existirem relações pai) as linhas a serem adicionadas e todas as que de si dependem (filhas, netas, etc) são removidas do dataset de trabalho
						DataRow[] parentRows = null;
						bool parentMissing = false;
						foreach (DataRelation rel in rowWithinLoop.Table.ParentRelations)
						{
							parentRows = rowWithinLoop.GetParentRows(rel);
							if (parentRows.Length != 0){
								string queryRows = ConcorrenciaRule.Current.getQueryForRows(parentRows, DataRowState.Unchanged, DataRowState.Modified);
								DataTable parentTable = GisaDataSetHelper.GetInstance().Tables[rel.ParentTable.TableName];
								if (!OriginalRowsDB.Tables.Contains(rel.ParentTable.TableName) ||
									parentTable.Select(queryRows).Length != OriginalRowsDB.Tables[rel.ParentTable.TableName].Select(queryRows).Length) 
                                {
									ConcorrenciaRule.Current.FillTableInGetLinhasConcorrentes(OriginalRowsDB, parentTable , string.Format("WHERE {0}", queryRows), DBAbstractDataLayer.DataAccessRules.SqlClient.SqlSyntax.DataDeletionStatus.All, tran);
								}
  								foreach (DataRow parentRow in parentRows)
								{
									DataRow[] parentRows2 = null;
									parentRows2 = OriginalRowsDB.Tables[parentRow.Table.TableName].Select(ConcorrenciaRule.Current.buildFilter(parentRow.Table, parentRow).ToString());

									// é detectado um pai em falta se:
									//  -> não é encontrado um na Bd 
									//  -> não existe um "novo" em memória  (uma linha added ou uma linha modified que tenha sido originalmente added)
									// Nota: a condição "Not parentRow.RowState = DataRowState.Added" deve já estar abrangida pela "Not parentRow("Versao") Is DBNull.Value" e não ser por isso necessária
									if ((parentRows2.Length == 0 || (byte)(parentRows2[0]["isDeleted"]) == 1) && (! (parentRow.RowState == DataRowState.Added) && ! (parentRow["Versao"] == DBNull.Value) && ! (((byte[])(parentRow["Versao"])).Length == 0)))
									{
                                        parentMissing = true;
										break;
									}
								}
							}
						}

						if (parentMissing)
						{
							//ToDo: criar mensagem a indicar que a informação não pode ser gravada na base de dados
							//ConcorrenciaMessagesHelper.LinhasNaoGravadas.Add(New ConcorrenciaMessagesHelper.infoNotSaved(row.Table, row))

							if (infoNaoGravada.Length > 0)
								infoNaoGravada.Append("; ");

							infoNaoGravada.Append(MetaModelHelper.getFriendlyName(rowWithinLoop.Table.TableName));

							removeChildRows(rowWithinLoop, changedRows);
							changedRows r = getChangedRowsElement(changedRows, row.Table.TableName);
							r.rowsAdd.Remove(rowWithinLoop);
							rowWithinLoop.Table.Rows.Remove(rowWithinLoop);
						}
					}
				}
			}

			//Iteração sobre as linhas com estado "modified" e "deleted"
			foreach (changedRows tab in changedRows)
			{
				rows.Clear();
				rows.AddRange(tab.rowsMod);
				rows.AddRange(tab.rowsDel);
				dt = GisaDataSetHelper.GetInstance().Tables[tab.tab];

                //TimeSpan start = new TimeSpan();
				foreach (DataRow rowWithinLoop in rows)
				{
                    row = rowWithinLoop;
                    s = ConcorrenciaRule.Current.buildFilter(dt, rowWithinLoop);
                    DataRow originalRow = GetOriginalRow(dt, rowWithinLoop);

					//se a row actual tem o estado "deleted" e a sua correspondente na BD não existe 
					//ou tem o booleano isDeleted igual a true...
					if (rowWithinLoop.RowState == DataRowState.Deleted && (originalRow == null || (originalRow != null && (byte)originalRow["isDeleted"] == 1)))
					{
						//remover a row da estrutura que mantém as linhas alteradas
						tab.rowsDel.Remove(rowWithinLoop);
						rowWithinLoop.AcceptChanges();

						//ToDo: verificar se as linhas descendentes também têm o estado "deleted"
					}
					else if (rowWithinLoop.RowState == DataRowState.Modified && ! (rowWithinLoop["Versao"] == DBNull.Value) && ((byte[])(rowWithinLoop["Versao"])).Length > 0 && (originalRow == null || (originalRow != null && (byte)originalRow["isDeleted"] == 1)))
					{
						//a linha que se pretende editar não existe na BD ou a sua correspondente tem o boolano isDeleted igual a true;
						//nesta situação a linha é apagada bem como todas as suas filhas
						//NOTA: o teste feito sobre o valor "Versao" tem como objectivo distinguir as linhas cujo estado original é 
						//"added" daquelas com o estado "modified" (se o valor não for nulo estamos perante uma linha com estado 
						//original modified)



						//marcar como deleted as linhas que lhe estao associadas
						removeChildRows(rowWithinLoop, changedRows);

						//retirar a row da estrutura que mantem as linhas apagadas
						tab.rowsMod.Remove(rowWithinLoop);

						//remover a linha
						rowWithinLoop.Table.Rows.Remove(rowWithinLoop);

						//MessageBox.Show(String.Format("A informação relativa a {0} não pode ser guardada devido ao facto de o contexto ter sido apagado por outro utilizador.", MetaModelHelper.getFriendlyName(row.Table.TableName)))

						if (infoNaoGravada.Length > 0)
							infoNaoGravada.Append("; ");

						infoNaoGravada.Append(MetaModelHelper.getFriendlyName(rowWithinLoop.Table.TableName));

						//ConcorrenciaMessagesHelper.LinhasNaoGravadas.Add(New ConcorrenciaMessagesHelper.infoNotSaved(row.Table, row))

						//ToDo: verificar se ainda é necessário testar o valor da coluna Versao

					}
					else if (rowWithinLoop.RowState == DataRowState.Modified && originalRow != null && resurrectedRows.Contains(originalRow))
					{

						//evitar o processo de detecção de conflitos de concorrência no caso da linha existir na BD e estar 
						//marcada como deleted (situação onde é criada um linha com a mesma PK de uma existente na BD mas com o 
						//booleano igual a true e só é pretendido manter o valor da PK)
						//so é feito um AcceptChanges() (esta operação não é executada antes devido à necessidade de detectar 
						//esta situação descrita na linha anterior)

						//ToDo: verificar a utilidade destas instruções (o estado da row2 não é "unchanged"?)
						originalRow.AcceptChanges();
					}
					else
					{
						//processo de detecção de conflitos de concorrência


						// linha da BD correspondente àquela que está a ser tratada
						//row2 = OriginalRowsDB.Ta0.bles.Item(tab.tab).Select(s.ToString())(0)

						// se a linha não tiver valor timestamp (nem original nem current) é indicativo que inicialmente estava maracada como
						// "added" e no ciclo anterior o seu estado foi alterado para "modified" (daí o facto de não ter nenhum timestamp)
						if ((rowWithinLoop["Versao", DataRowVersion.Original] == DBNull.Value || ((byte[])(rowWithinLoop["Versao", DataRowVersion.Original])).Length == 0) && (rowWithinLoop["Versao", DataRowVersion.Current] == DBNull.Value || ((byte[])(rowWithinLoop["Versao", DataRowVersion.Current])).Length == 0))
						{
							bool haColunasConcorrentes = getColunasConcorrentes(tab.tab, row, originalRow);
							if (haColunasConcorrentes)
							{
								c += 1;
								//msgConcorrencia.Append(MetaModelHelper.getFriendlyName(tab.tab) + ", " + strColConc + ", ")
								//str.Append(strColConc + ", ")
							}
							else
							{
								rowWithinLoop.AcceptChanges();
								tab.rowsMod.Remove(rowWithinLoop);
							}
							//se se tratar duma linha inicialmente marcada como "modified"
						}
						else
						{
							//pretende-se a versao original da linha pois para o caso da linha estiver marcada como deleted somente os valores originais estão acessiveis
							byte[] v1 = (byte[])(row["Versao", DataRowVersion.Original]);
							byte[] v2 = null;
							try
							{
								v2 = (byte[])(originalRow["Versao"]);
							}
							catch (Exception ex)
							{
								Trace.WriteLine(ex.ToString());
								throw ex;
							}

							if (haConcorrencia(v1, v2))
							{
								//strColConc = getColunasConcorrentes(tab.tab, row, row2).ToString()
								bool haColunasConcorrentes = getColunasConcorrentes(tab.tab, row, originalRow);
								if (haColunasConcorrentes)
								{
									c += 1;
									//msgConcorrencia.Append(MetaModelHelper.getFriendlyName(tab.tab) + ", " + strColConc + ", ")
									//str.Append(strColConc + ", ")
								}
								else
								{
									//a row alterada é igual à correspondente em memória
									rowWithinLoop.AcceptChanges();
									tab.rowsMod.Remove(rowWithinLoop);
									tab.rowsDel.Remove(rowWithinLoop);
									//garantir que no dataset com as linhas provenientes da BD so existem aquelas que realmente estão em concorrencia
									originalRow.Table.Rows.Remove(originalRow);
								}
							}
							else
							{
								//foi detectado que não existe conflito de concorrência, no entanto, pode ter existido uma alteração sobre o valor 
								//de uma FK que entretanto a linha referenciada por este valor foi apagada por outro utilizador provocando um problema
								//de inconsistência de dados (exemplo: é selecionado um auto de eliminação, que não está associado a qualquer 
								//nível entretanto apagado por outro utilizador; na mudança de contexto só existe, no mínimo, uma linha com estado 
								//modified cuja alteração é o valor da coluna IDAutoEliminacao; como a eliminação do auto de eliminação não provocou
								//qualquer mudança na avaliação em questão não é detectado conflito de concorrência; no entanto, quando a avaliação
								//vai ser gravada na base de dados vai ocorrer um erro pela ausência do auto de eliminação)
								//Por isso, se não for detectado qualquer conflito de concorrência deve-se, ainda antes de gravar, verificar se as 
								//linhas referenciadas pelos valores das FK ainda existem na base de dados para prever este tipo de erros.

								if (rowWithinLoop.RowState == DataRowState.Modified)
								{
									DataRow[] parentRows = null;
									foreach (DataRelation rel in rowWithinLoop.Table.ParentRelations)
									{
                                        if (rel.ChildKeyConstraint.Columns[0].AllowDBNull)
                                        {
                                            parentRows = rowWithinLoop.GetParentRows(rel);
                                            if (parentRows.Length > 0)
                                            {
                                                ConcorrenciaRule.Current.FillTableInGetLinhasConcorrentes(OriginalRowsDB, rel.ParentTable, string.Format("WHERE {0}", ConcorrenciaRule.Current.getQueryForRows(parentRows, DataRowState.Unchanged, DataRowState.Modified)), DBAbstractDataLayer.DataAccessRules.SqlClient.SqlSyntax.DataDeletionStatus.All, tran);
                                                foreach (DataRow parentRow in parentRows)
                                                {
                                                    DataRow[] parentRows2 = null;
                                                    parentRows2 = OriginalRowsDB.Tables[parentRow.Table.TableName].Select(ConcorrenciaRule.Current.buildFilter(parentRow.Table, parentRow).ToString());

                                                    // é detectado um pai em falta se:
                                                    //  -> não é encontrado um na Bd 
                                                    //  -> não existe um "novo" em memória  (uma linha added ou uma linha modified que tenha sido originalmente added)
                                                    // Nota: a condição "Not parentRow.RowState = DataRowState.Added" deve já estar abrangida pela "Not parentRow("Versao") Is DBNull.Value" e não ser por isso necessária
                                                    if ((parentRows2.Length == 0 || (byte)parentRows2[0]["isDeleted"] == 1) && !(parentRow.RowState == DataRowState.Added) && !(parentRow["Versao"] == DBNull.Value) && !(((byte[])(parentRow["Versao"])).Length == 0))
                                                    {
                                                        //a coluna que é uma FK permite o valor NULL
                                                        if (rel.ChildKeyConstraint.Columns[0].AllowDBNull)
                                                        {
                                                            Trace.WriteLine("A colocar o valor null a uma coluna FK.");
                                                            rowWithinLoop[rel.ChildKeyConstraint.Columns[0].ColumnName] = DBNull.Value;
                                                            //FIXME: indicar ao utilizador que alguma coisa correu mal e não pôde ser gravada (no caso das 
                                                            //avaliações não foi possível associar um auto de eliminação a um nível por este ter sido eliminado)
                                                        }
                                                        else
                                                        {
                                                            Trace.WriteLine("Não é permitido colocar o valor NULL à coluna por isso vai ocorrer uma erro.");
                                                            Debug.Assert(false, "FK unexpected error occurred.");
                                                        }
                                                    }
                                                }
                                            }
                                        }
									}
								}
							}
						}
					}
				}

                //Debug.WriteLine("<<modified/deleted row>>: " + start.ToString());
			}

			mStrConcorrenciaLinhasNaoGravadas = infoNaoGravada;

            return c > 0;
        }        

        public List<object> buildFilterPK(DataTable dt, DataRow row)
        {
            List<object> res = new List<object>();
            DataRowVersion version = DataRowVersion.Original;
            if (row.RowState == DataRowState.Added)
                version = DataRowVersion.Current;

            foreach (DataColumn col in dt.PrimaryKey)
                res.Add(row[col.ColumnName, version]);

            return res;
        }

        public DataRow GetOriginalRow(DataTable dt, DataRow row)
        {
            //obter row original tendo em consideração só a PK da tabela

            //long tempo = DateTime.Now.Ticks;
            DataView dv;
            DataRowView[] dr;
            DataRow originalRow = null;

            List<object> res = buildFilterPK(dt, row);
            dv = OriginalRowsDB.Tables[dt.TableName].DefaultView;
            dv.ApplyDefaultSort = true;
            dr = dv.FindRows(res.ToArray());
            if (dr.Length > 0)
                originalRow = dr[0].Row;
            //start += new TimeSpan(DateTime.Now.Ticks - tempo);


            // caso não se encontre a row original pela PK, procura-se novamente mas considerando
            // outras unique constraints (Ex: quando se cria uma row da FRDBase, o ID é negativo e 
            // por esse motivo não se encontra a row original; procura-se então pelo IDNivel que 
            // faz parte de outra unique constraint)
            if (originalRow == null)
            {
                string s = ConcorrenciaRule.Current.buildFilter(dt, row);
                DataRow[] rows = OriginalRowsDB.Tables[dt.TableName].Select(s);
                if (rows.Length > 0)
                    originalRow = rows[0];
            }

            return originalRow;
        }

		private void changeRowStateToModified(DataRow dataRow, changedRows changedRow, DataTable dt)
		{
			//verificar se é possivel forçar a mudança de estado (tabelas intermedias que so sejam compostas por chaves primarias não são permitidas edições)
			if (allowRowStateChanges(dt))
			{
				//forçar a mudança de estado para modified da linha marcada como added
				dataRow.AcceptChanges();
				bool isReadOnly = false;
				isReadOnly = dataRow.Table.Columns[0].ReadOnly;
				dataRow.Table.Columns[0].ReadOnly = false;
				dataRow[0] = dataRow[0];
				dataRow.Table.Columns[0].ReadOnly = isReadOnly;
				changedRow.rowsMod.Add(dataRow);
				RowsChangedToModified.Add(dataRow, dataRow);
			}
			else
			{
				dataRow.AcceptChanges();
			}

			//como a linha já existe na BD esta deve ser removida da lista das added
			changedRow.rowsAdd.Remove(dataRow);

		}

		//funcao que retorna a estrutura que contem as linhas modificadas da tabela passada como argumento
		internal changedRows getChangedRowsElement(ArrayList changedRows, string dt)
		{
			foreach (changedRows el in changedRows)
			{
				if (el.tab.Equals(dt))
					return el;
			}
			return (changedRows)(changedRows[changedRows.Add(new changedRows(dt, new ArrayList(), new ArrayList(), new ArrayList()))]);
		}

		// actualiza os valores de datarow1 com os existentes em datarow2 (somente as colunas da chave primaria)
		private void updateRow(DataRow dataRow1, DataRow dataRow2)
		{
			updateRow(dataRow1, dataRow2, true);
		}

		private void updateRow(DataRow dataRow1, DataRow dataRow2, bool justPK)
		{
			DataTable table = dataRow1.Table;
			bool isReadOnly = false;
			int tempFor1 = dataRow1.Table.Columns.Count;
			for (int i = 0; i < tempFor1; i++)
			{
				if (Array.IndexOf(table.PrimaryKey, table.Columns[i]) != -1)
				{
					isReadOnly = table.Columns[i].ReadOnly;
					table.Columns[i].ReadOnly = false;
					dataRow1[i] = dataRow2[i];
					table.Columns[i].ReadOnly = isReadOnly;
				}
			}
		}

		//metodo que verifica se existe concorrencia entre duas linhas comparando os seus timestamps (retorna true se houver concorrencia)
		private bool haConcorrencia(byte[] row1, byte[] row2)
		{
			int tempFor1 = row1.Length;
            for (int i = row1.Length - 1; i >= 0; i--)
			{
                if (row1[i] != row2[i])
					return true;
			}
			return false;
		}

		private bool getColunasConcorrentes(string tabName, DataRow rowDS, DataRow rowDB)
		{
			bool temColunasConcorrentes = false;

			System.Text.StringBuilder strUser = new System.Text.StringBuilder();
			System.Text.StringBuilder strBD = new System.Text.StringBuilder();

			string tableFriendlyName = null;
			string columnFriendlyName = null;

			System.Data.DataRowVersion versao = 0;
			if (rowDS.RowState == DataRowState.Deleted)
				versao = DataRowVersion.Original;
			else
				versao = DataRowVersion.Current;


			ArrayList fkColumns = getForeignKeyColumns(rowDS.Table);
			int tempFor1 = rowDS.Table.Columns.Count;
			for (int i = 0; i < tempFor1; i++)
			{

				// Verificar se os valores são diferentes (para cada coluna desta row)
				// Primeiro testa-se se os valores das colunas são dbnull
				// Para as strings é feito um trim antes da comparação
				if (((! (rowDS[i, versao] == DBNull.Value) && ! (rowDB[i] == DBNull.Value)) || (! (rowDS[i, versao] == DBNull.Value) && rowDB[i] == DBNull.Value) || (rowDS[i, versao] == DBNull.Value && ! (rowDB[i] == DBNull.Value))) && (rowDS.Table.Columns[i].DataType == typeof(string) && ! (rowDS[i, versao].ToString().Trim().Equals(rowDB[i].ToString().Trim())) || ! (rowDS.Table.Columns[i].DataType == typeof(string)) && ! (rowDS[i, versao].Equals(rowDB[i]))) && ! (rowDS.Table.Columns[i].ReadOnly) && ! (rowDS.Table.Columns[i].ColumnName.Equals("isDeleted")))
				{
					// verificar se a coluna actual pertence às foreign keys da sua tabela
					// Nota: não se espera que neste teste entrem valores "nothing" por se tratarem de FK
					if (fkColumns.Contains(rowDS.Table.Columns[i]))
					{
						DataTable foreignTable = getForeignTable(rowDS.Table.Columns[i]);
						Debug.WriteLine(foreignTable.TableName);
						tableFriendlyName = MetaModelHelper.getFriendlyName(tabName);

						strUser.Append(tableFriendlyName + ":");
						strBD.Append(tableFriendlyName + ":");

						strUser.Append(System.Environment.NewLine);
						strBD.Append(System.Environment.NewLine);

						if (foreignTable.TableName.Equals("Iso639"))
						{
							strUser.Append(foreignTable.Select(string.Format("ID={0}", rowDS[i, versao]))[0]["LanguageNameEnglish"]);
							strBD.Append(foreignTable.Select(string.Format("ID={0}", rowDB[i]))[0]["LanguageNameEnglish"]);
						}
						else if (foreignTable.TableName.Equals("Iso15924"))
						{
							strUser.Append(foreignTable.Select(string.Format("ID={0}", rowDS[i, versao]))[0]["ScriptNameEnglish"]);
							strBD.Append(foreignTable.Select(string.Format("ID={0}", rowDB[i]))[0]["ScriptNameEnglish"]);
						}
						else
						{
							strUser.Append(getReadableRowValue(rowDS, rowDS.Table.Columns[i], versao, foreignTable));
							strBD.Append(getReadableRowValue(rowDB, rowDB.Table.Columns[i], DataRowVersion.Current, foreignTable));
						}

						strUser.Append(System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine);
						strBD.Append(System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine);

						temColunasConcorrentes = true;
					}
					else
					{
						tableFriendlyName = MetaModelHelper.getFriendlyName(tabName);
						strUser.Append(tableFriendlyName);
						strBD.Append(tableFriendlyName);
						columnFriendlyName = MetaModelHelper.getFriendlyName(tabName, rowDS.Table.Columns[i].ColumnName);
						if (tableFriendlyName != null && tableFriendlyName.Length > 0 && columnFriendlyName != null && columnFriendlyName.Length > 0)
						{
							strUser.Append(", ");
							strBD.Append(", ");
						}
						strUser.Append(columnFriendlyName + ":" + System.Environment.NewLine);
						strBD.Append(columnFriendlyName + ":" + System.Environment.NewLine);
						strUser.Append(getReadableRowValue(rowDS, rowDS.Table.Columns[i], versao));
						strBD.Append(getReadableRowValue(rowDB, rowDB.Table.Columns[i]));
						temColunasConcorrentes = true;
						strUser.Append(System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine);
						strBD.Append(System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine);
					}
				}
			}

			if (temColunasConcorrentes)
			{
				mStrConcorrenciaUser.Append(strUser.ToString());
				mStrConcorrenciaBD.Append(strBD.ToString());
			}

			return temColunasConcorrentes;
		}

		private string getReadableRowValue(DataRow row, DataColumn column, DataRowVersion version)
		{
			return getReadableRowValue(row, column, version, null);
		}

		private string getReadableRowValue(DataRow row, DataColumn column)
		{
			return getReadableRowValue(row, column, DataRowVersion.Current, null);
		}

		private string getReadableRowValue(DataRow row, DataColumn column, DataRowVersion version, DataTable lookupTable)
		{
			if (row[column, version] == DBNull.Value)
				return "Valor não atribuído.";
			else
			{
				if (lookupTable == null)
				{
					if (column.DataType == typeof(bool))
						return translateBoolean((bool)(row[column, version]));
					else
						return row[column, version].ToString();
				}
				else
				{
					if (lookupTable.Select(string.Format("ID={0}", row[column, version])).Length > 0)
					{
						if (lookupTable.TableName == "TrusteeUser")
							return lookupTable.Select(string.Format("ID={0}", row[column, version]))[0]["FullName"].ToString();
						else
							return lookupTable.Select(string.Format("ID={0}", row[column, version]))[0]["Designacao"].ToString();
					}
					else
						return "Valor não atribuído.";
				}
			}
		}

		// devolve um arraylist de DataColumns foreign key de uma tabela
		private ArrayList getForeignKeyColumns(DataTable table)
		{
			ArrayList result = new ArrayList();
			foreach (DataRelation relation in table.ParentRelations)
				result.AddRange(relation.ChildColumns);

			return result;
		}

		//retorna a parenttable da foreignkey column passada como argumento
		private DataTable getForeignTable(DataColumn column)
		{
			foreach (DataRelation relation in column.Table.ParentRelations)
			{
				if (Array.IndexOf(relation.ChildColumns, column) != -1)
					return relation.ParentColumns[0].Table;
			}
			return null;
		}

		//retorna verdadeiro se for permitido forçar a mudança de estado das linhas da tabela
		private bool allowRowStateChanges(DataTable dt)
		{
			foreach (DataColumn column in dt.Columns)
			{
				if (! column.ReadOnly && System.Array.IndexOf(dt.PrimaryKey, column) < 0)
					return true;
			}
			return false;
		}

	#region  Rollback Dataset 
		//metodo que actualiza os dados em memoria com aqueles recolhidos da base de dados para efeitos de tratamento de concorrencia
		public void mergeDataFromDataBase(DataSet ds)
		{
			GisaDataSetHelper.GetInstance().Merge(ds, false);
		}


		public void MergeDatasets(DataSet srcDs, DataSet dstDs, ArrayList dataSetTablesOrderedA)
		{
			MergeDatasets(srcDs, dstDs, dataSetTablesOrderedA, null);
		}

//INSTANT C# NOTE: C# does not support optional parameters. Overloaded method(s) are created above.
//ORIGINAL LINE: Public Sub MergeDatasets(ByVal srcDs As DataSet, ByVal dstDs As DataSet, ByVal dataSetTablesOrderedA As ArrayList, Optional ByVal trackNewIDs As Hashtable = null)
		public void MergeDatasets(DataSet srcDs, DataSet dstDs, ArrayList dataSetTablesOrderedA, Hashtable trackNewIDs)
		{
			//dstDs.EnforceConstraints = false

			// efectuar a primeira fase do merge entre os dois datasets: no final desta instrução todas as linhas merged no dataset 
			// de destino ficam no estado modified (o estado actual dessas linhas no dataset de destino é unchanged) caso as linhas 
			// correspondentes no dataset de origem tenham o estado added
			dstDs.Merge(srcDs);

			// DataSet de apoio ao merge: de forma a se consegui mudar o estado das linhas, agora modified, para o estado original
			// (antes do merge) added é necessário removê-las do DataSet e fazer um import da sua correspondente e com o estado original
			// guardada no DataSet de origem; ao remover essa linha do DataSet todas e quaiquer linhas que se sejam dependentes de si
			// são igualmente eliminadas pelo que este DataSet vai servir para as guardar e no final deste passo do merge voltar a 
			// colocá-las no DataSet de origem.
			DataSet ds = new DataSet();
			foreach (TableDepthOrdered.tableDepth t in dataSetTablesOrderedA)
			{
				if (srcDs.Tables.Contains(t.tab.TableName))
				{
					DataTable srcTable = srcDs.Tables[t.tab.TableName];
					DataTable dstTable = dstDs.Tables[srcTable.TableName];
					foreach (DataRow srcInsRow in srcTable.Rows)
					{
						string filter = ConcorrenciaRule.Current.buildFilter(dstTable, srcInsRow, false);
						if (srcInsRow.RowState == DataRowState.Added)
						{
							// verificar se a linha actual ainda existe no DataSet de trabalho prevendo o caso de 
							// esta ter sido eliminada (marcada como deleted) por motivos de actualização do estado 
							// para added de uma linha "pai"; 
							// - nessa situação a linha é reposta no DataSet de  trabalho (destino) e o ciclo
							//   deve prosseguir com a próxima linha;
							// - caso contrário, antes de o estado (rowstate) da linha actual ser actualizado, 
							//   todas as suas linhas filhas, netas, ..., são copiadas para um terceiro DataSet
							//   com o fim de as voltar a copiar para o DataSet de destino um vez que aquando a 
							//   actualização do estado da linha actual, esse conjunto de linhas é apagado.
							if (dstTable.Select(filter).Length == 0)
							{
								if (dstTable.Select(filter, "", DataViewRowState.Deleted).Length > 0 && ds.Tables[dstTable.TableName].Select(filter).Length > 0)
								{

									dstTable.Select(filter, "", DataViewRowState.Deleted)[0].AcceptChanges();
									dstTable.ImportRow(srcInsRow);
								}
								else if (trackNewIDs != null && trackNewIDs.Contains(t.tab.TableName))
								{
									if (((Hashtable)(trackNewIDs[t.tab.TableName])).Contains(filter))
									{
										Hashtable ht = (Hashtable)(trackNewIDs[t.tab.TableName]);
										DataRow[] row = (DataRow[])(GisaDataSetHelper.GetInstance().Tables[t.tab.TableName].Select(((ArrayList)(ht[filter]))[1].ToString()));
										if (row.Length > 0)
										{
											dstTable.Rows.Remove(row[0]);
											dstTable.ImportRow(srcInsRow);
										}
									}
								}
								else
								{
									Debug.WriteLine("Situação desconhecida: " + dstTable.TableName);
								}
							}
							else
							{
								DataRow dstRow = dstTable.Select(filter)[0];
								// se tanto a linha de origem como a linha de destino tiverem o rowstate
								// added, o merge entre as duas já foi feito com o método DataSet.Merge(DataSet)
								if (! (dstRow.RowState == DataRowState.Added && srcInsRow.RowState == DataRowState.Added))
								{
									if (dstRow.RowState == DataRowState.Modified && srcInsRow.RowState == DataRowState.Added)
									{
										foreach (DataRelation rel in dstTable.ChildRelations)
										{
											if (dstRow.GetChildRows(rel).Length > 0)
											{
												ds.Tables.Add(rel.ChildTable.Clone());
												foreach (DataRow childRow in dstRow.GetChildRows(rel))
												{
													string filter2 = ConcorrenciaRule.Current.buildFilter(ds.Tables[rel.ChildTable.TableName], childRow, false);
													if (ds.Tables[rel.ChildTable.TableName].Select(filter2).Length == 0)
													{
														ds.Tables[rel.ChildTable.TableName].ImportRow(childRow);
													}
													getChildRows(childRow, dstDs, ds);
												}
											}
										}
									}
									dstTable.Rows.Remove(dstRow);
									dstTable.ImportRow(srcInsRow);
								}
							}
						}
						else if (srcInsRow.RowState == DataRowState.Unchanged)
						{
							// prever todos os casos onde as linhas são alteradas após a execução do backup; uma 
							// vez que é feito um primeiro Merge no início do método, toda informação que possa ter
							// sido alterada, neste ponto de execução, esta já foi reposta para o valor original
							// faltando somente voltar a definir o RowState para o valor Unchanged (tipicamente 
							// o RowState destas linhas tem o valor Modified)
							if (dstTable.Select(filter).Length > 0 && dstTable.Select(filter)[0].RowState == DataRowState.Modified)
							{
								dstTable.Select(filter)[0].AcceptChanges();
							}
						}
					}
				}
			}

			// repor no dataset de destino todas as linhas filhas daquelas que tinham o estado original added
			foreach (DataTable srcTable in ds.Tables)
			{
				DataTable dstTable = dstDs.Tables[srcTable.TableName];
				foreach (DataRow srcRow in srcTable.Rows)
				{
					string filter = ConcorrenciaRule.Current.buildFilter(dstTable, srcRow, false);
					if (dstTable.Select(filter, "", DataViewRowState.Deleted).Length > 0)
					{
						DataRow dstRow = dstTable.Select(filter, "", DataViewRowState.Deleted)[0];
						dstRow.AcceptChanges();
						dstTable.ImportRow(srcRow);
					}
				}
			}
			//dstDs.EnforceConstraints = true;
		}

		// Método de apoio ao MergeDatasets que tem como função identificar e copiar para um DataSet as 
		// linhas "filhas" daquela passada como argumento
		private void getChildRows(DataRow row, DataSet dstDs, DataSet ds)
		{
			foreach (DataRelation rel in row.Table.ChildRelations)
			{
				if (row.GetChildRows(rel).Length > 0)
				{
					ds.Tables.Add(rel.ChildTable.Clone());
					foreach (DataRow childRow in row.GetChildRows(rel))
					{
						string filter2 = ConcorrenciaRule.Current.buildFilter(ds.Tables[rel.ChildTable.TableName], childRow, false);
						if (ds.Tables[rel.ChildTable.TableName].Select(filter2).Length == 0)
						{
							ds.Tables[rel.ChildTable.TableName].ImportRow(childRow);
						}
						getChildRows(childRow, dstDs, ds);
					}
				}
			}
		}

		// Método que tem como objectivo manter a informação referente à actualização dos Ids das linhas 
		// quando estas são adicionadas na base de dados, isto é, saber qual o valor (negativo) do ID 
		// antes da linha ser gravada e o valor (positivo) atribuído pela base de dados depois do save
		public void startTrackingIdsAddedRows(DataSet workDataSet, ArrayList workDataSetChangedRows, ref Hashtable trackStruture)
		{
			foreach (changedRows changedRow in workDataSetChangedRows)
			{
				if (changedRow.rowsAdd.Count > 0)
				{
					ArrayList dataRowEFiltroNovo = new ArrayList();
					Hashtable filtrosEDataRow = new Hashtable();
					foreach (DataRow addRow in changedRow.rowsAdd)
					{
						if (addRow.RowState != DataRowState.Detached) {
							string filter = ConcorrenciaRule.Current.buildFilter(addRow.Table, addRow);
							dataRowEFiltroNovo.Add(addRow);
                            filtrosEDataRow.Add(filter, dataRowEFiltroNovo);
						}					
					}
					trackStruture.Add(changedRow.tab, filtrosEDataRow);
				}
			}
		}

		// determinar o filtro para a query que irá obter as rows added com os Ids actualizados, ou seja, 
		// com valores positivos
		public void prepareRollBackDataSet(ref Hashtable trackStruture)
		{
			foreach (Hashtable filtroAntigo in trackStruture.Values)
			{
				foreach (ArrayList rowAlterada in filtroAntigo.Values)
				{
					rowAlterada.Add(ConcorrenciaRule.Current.buildFilter(((DataRow)(rowAlterada[0])).Table, (DataRow)(rowAlterada[0])));
				}
			}
		}

		// apagar do dataset de trabalho todas as linhas (inicialmente com o estado added) que foram 
		// gravadas (acceptChanges) em memória, mas que por motivos de concorrência a transacção onde 
		// estavam incluídas foi reiniciada e por esse motivo deixaram de ser úteis pois vão ser atribuídos
		// novos Ids caso a transacção seja bem sucedida
		public void deleteUnusedRows(DataSet workDataSet, ref Hashtable trackStruture)
		{
			foreach (Hashtable filtroAntigo in trackStruture.Values)
			{
				foreach (ArrayList rowAlterada in filtroAntigo.Values)
				{
					DataRow row = (DataRow)rowAlterada[0];
					if (!(row.RowState == DataRowState.Detached) && workDataSet.Tables[row.Table.TableName].Select(ConcorrenciaRule.Current.buildFilter(row.Table, row)).Length > 0) {
						workDataSet.Tables[row.Table.TableName].Rows.Remove(row);
					}
				}
			}
		}
	#endregion

		//metodo de suporte para verificar se existe concorrencia quando o utilizador decide manter as suas alterações
		public bool wasModified(DataSet ds1, DataSet ds2, ArrayList cr)
		{
			ArrayList rows = new ArrayList();
			DataRow row1 = null;
			DataRow row2 = null;
			DataTable dt = null;
			string filter = null;

			foreach (changedRows tab in cr)
			{
				rows.Clear();
				rows.AddRange(tab.rowsMod);
				rows.AddRange(tab.rowsDel);
				dt = GisaDataSetHelper.GetInstance().Tables[tab.tab];

				foreach (DataRow r in rows)
				{
					filter = ConcorrenciaRule.Current.buildFilter(dt, r).ToString();

					if (ds1.Tables[tab.tab].Select(filter).Length > 0 && ds2.Tables[tab.tab].Select(filter).Length > 0)
					{
						row1 = ds1.Tables[tab.tab].Select(filter)[0];
						row2 = ds2.Tables[tab.tab].Select(filter)[0];

						//testar se ha concorrencia
						if (haConcorrencia((byte[])(row1["Versao"]), (byte[])(row2["Versao"])))
							return true;

					}
					else if (! (ds1.Tables[tab.tab].Select(filter).Length > 0) && ds2.Tables[tab.tab].Select(filter).Length > 0)
					{
						//linha passou a existir na BD
					}
					else if (ds1.Tables[tab.tab].Select(filter).Length > 0 && ! (ds2.Tables[tab.tab].Select(filter).Length > 0))
					{
						//linha deixou de existir na BD
					}
				}
			}

			return false;
		}

		//metodo que verifica se 1 dataset tem linhas guardadas
		public bool temLinhas(DataSet ds)
		{
			foreach (DataTable dt in ds.Tables)
			{
				if (dt.Rows.Count > 0)
					return true;
			}
			return false;
		}

		//método de suporte para a construção da mansagem a apresentar ao utilizador numa situação de conflito
		//traduz os valores da variavel "val" para sim, não ou não definido consoante o valor passado
        public static string translateBoolean(bool val)
		{
			if (val == true)
				return "Sim";
			else
				return "Não";
		}

		//apaga / marca como apagada, as linhas descendentes daquela passada como argumento e remove-as da estrutura que mantem as linhas alteradas em memoria
		private void removeChildRows(DataRow dr, ArrayList changedrows)
		{
			changedRows el = new changedRows();
			ArrayList rows = new ArrayList();
			foreach (DataRelation rel in dr.Table.ChildRelations)
			{
				foreach (DataRow row in dr.GetChildRows(rel))
				{
					if (row.Table.ChildRelations.Count > 0)
						removeChildRows(row, changedrows);

					rows.Clear();
					//verificar se a row está no arraylist com as linhas alteradas
					if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Deleted || row.RowState == DataRowState.Modified)
					{
						el = getChangedRowsElement(changedrows, row.Table.TableName);
						el.rowsAdd.Remove(row);
						el.rowsMod.Remove(row);
						el.rowsDel.Remove(row);
					}
					row.Table.Rows.Remove(row);
				}
			}
		}

		private static bool has1To1Relations(DataTable dt)
		{
			foreach (DataRelation rel in dt.ParentRelations)
			{
				DataColumn[] relationParentColumns = rel.ParentColumns;
				DataColumn[] parentTablePrimaryKey = rel.ParentTable.PrimaryKey;
				if (dt.PrimaryKey.Length == rel.ParentTable.PrimaryKey.Length && areTheSameColumns(relationParentColumns, parentTablePrimaryKey))
					return true;
			}

			foreach (DataRelation rel in dt.ChildRelations)
			{
				DataColumn[] relationChildColumns = rel.ChildColumns;
				DataColumn[] childTablePrimaryKey = rel.ChildTable.PrimaryKey;
				if (dt.PrimaryKey.Length == rel.ChildTable.PrimaryKey.Length && areTheSameColumns(relationChildColumns, childTablePrimaryKey))
					return true;
			}

			return false;
		}

		private static bool areTheSameColumns(DataColumn[] list1, DataColumn[] list2)
		{
			if (list1.Length != list2.Length)
				return false;

			int tempFor1 = list1.Length;
			for (int i = 0; i < tempFor1; i++)
			{
				if (! (list1[i] == list2[i]))
					return false;
			}
			return true;
		}

		private static bool parentsExists(DataRow row)
		{
			if (row.Table.ParentRelations.Count == 0)
				return true;

			foreach (DataRelation rel in row.Table.ParentRelations)
				return row.GetParentRows(rel).Length > 0;

			return false;
		}
	}

//#If DEBUG Then
//<TestFixture()> Public Class TestConcorrencia

//    <SetUp()> Public Sub SetUp()
//    End Sub

//    <TearDown()> Public Sub TearDown()
//    End Sub

//    <Test()> Public Sub TestHas1To1Relations()
//        Dim ds As New GISADataset
//        Dim mc As New MockConcorrencia
//        Assertion.Assert("Table RelacaoHierarquica reported unexpected relations.", Not mc.InvokeHas1To1Relations(ds.RelacaoHierarquica))
//    End Sub

//    Private Class MockConcorrencia
//        Inherits Concorrencia

//        Public Function InvokeHas1To1Relations(ByVal dt As DataTable) As Boolean
//            Return has1To1Relations(dt)
//        End Function
//    End Class
//End Class
//#End If
}