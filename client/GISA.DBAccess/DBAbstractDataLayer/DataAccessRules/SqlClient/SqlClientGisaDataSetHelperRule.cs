using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
	/// <summary>
	/// Summary description for SqlClientGisaDataSetHelperRule.
	/// </summary>
	public class SqlClientGisaDataSetHelperRule: GisaDataSetHelperRule
	{
		public override System.Data.DataRow[] selectIndexFRDCA(System.Data.DataSet ds, long FRDBaseID)
		{
			return ds.Tables["IndexFRDCA"].Select("IDFRDBase=" + FRDBaseID.ToString());			
		}

		public override System.Data.DataRow[] selectControloAutDicionario(System.Data.DataSet ds, long ControloAutID)
		{
			return ds.Tables["ControloAutDicionario"].Select("IDControloAut=" + ControloAutID.ToString());			
		}

		public override void LoadStaticDataTables(DataSet CurrentDataSet, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
				try 
				{
					// Normas de países, línguas, caligrafia...
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["Iso15924"]);
					da.Fill(CurrentDataSet, "Iso15924");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["Iso3166"]);
					da.Fill(CurrentDataSet, "Iso3166");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["Iso639"]);
					da.Fill(CurrentDataSet, "Iso639");

					//Configuracoes
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["GlobalConfig"]);
					da.Fill(CurrentDataSet, "GlobalConfig");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["ConfigAlfabeto"]);
                    da.Fill(CurrentDataSet, "ConfigAlfabeto");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["ConfigLingua"]);
                    da.Fill(CurrentDataSet, "ConfigLingua");

					//Conjunto de Privilégios da Aplicação
					//Identificam acesso a Módulos da aplicação ou funcionalidades dentro destes, não a dados.
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoFunctionGroup"]);
					da.Fill(CurrentDataSet, "TipoFunctionGroup");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoFunction"]);
					da.Fill(CurrentDataSet, "TipoFunction");                    
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoOperation"]);
					da.Fill(CurrentDataSet, "TipoOperation");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["NivelTipoOperation"]);
					da.Fill(CurrentDataSet, "NivelTipoOperation");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["DepositoTipoOperation"]);
                    da.Fill(CurrentDataSet, "DepositoTipoOperation");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["ObjetoDigitalTipoOperation"]);
                    da.Fill(CurrentDataSet, "ObjetoDigitalTipoOperation");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["FunctionOperation"]);
					da.Fill(CurrentDataSet, "FunctionOperation");

					//Tipos de producto existentes e funcionalidades proprias de cada um
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoServer"]);
					da.Fill(CurrentDataSet, "TipoServer");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["Modules"]);
					da.Fill(CurrentDataSet, "Modules");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["ProductFunction"]);
					da.Fill(CurrentDataSet, "ProductFunction");

					//Enumerados utilizados em foreign keys
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoControloAutForma"]);
					da.Fill(CurrentDataSet, "TipoControloAutForma");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoControloAutRel"]);
					da.Fill(CurrentDataSet, "TipoControloAutRel");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoDensidade"]);
					da.Fill(CurrentDataSet, "TipoDensidade");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoEntidadeProdutora"]);
					da.Fill(CurrentDataSet, "TipoEntidadeProdutora");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoEstadoDeConservacao"]);
					da.Fill(CurrentDataSet, "TipoEstadoDeConservacao");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoFormaSuporteAcond"]);
					da.Fill(CurrentDataSet, "TipoFormaSuporteAcond");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoFRDBase"]);
					da.Fill(CurrentDataSet, "TipoFRDBase");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoMaterialDeSuporte"]);
					da.Fill(CurrentDataSet, "TipoMaterialDeSuporte");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoMedida"]);
					da.Fill(CurrentDataSet, "TipoMedida");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoNivel"]);
					da.Fill(CurrentDataSet, "TipoNivel");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoNivelRelacionado"]);
					da.Fill(CurrentDataSet, "TipoNivelRelacionado");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoNivelRelacionadoCodigo"]);
					da.Fill(CurrentDataSet, "TipoNivelRelacionadoCodigo");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["RelacaoTipoNivelRelacionado"]);
					da.Fill(CurrentDataSet, "RelacaoTipoNivelRelacionado");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoNoticiaAut"]);
					da.Fill(CurrentDataSet, "TipoNoticiaAut");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoNoticiaATipoControloAForma"]);
					da.Fill(CurrentDataSet, "TipoNoticiaATipoControloAForma");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoOrdenacao"]);
					da.Fill(CurrentDataSet, "TipoOrdenacao");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoPertinencia"]);
					da.Fill(CurrentDataSet, "TipoPertinencia");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoSuporte"]);
					da.Fill(CurrentDataSet, "TipoSuporte");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoAcondicionamento"]);
					da.Fill(CurrentDataSet, "TipoAcondicionamento");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoTecnicaRegisto"]);
					da.Fill(CurrentDataSet, "TipoTecnicaRegisto");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoEstadoConservacao"]);
					da.Fill(CurrentDataSet, "TipoEstadoConservacao");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoSubDensidade"]);
					da.Fill(CurrentDataSet, "TipoSubDensidade");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoTecnicasDeRegisto"]);
					da.Fill(CurrentDataSet, "TipoTecnicasDeRegisto");
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoTradicaoDocumental"]);
					da.Fill(CurrentDataSet, "TipoTradicaoDocumental");

					//Autos de eliminação
					da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["AutoEliminacao"]);
					da.Fill(CurrentDataSet, "AutoEliminacao");

                    //Tipos de entrega
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoEntrega"]);
                    da.Fill(CurrentDataSet, "TipoEntrega");

                    //Integracao
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["Integ_Sistema"]);
                    da.Fill(CurrentDataSet, "Integ_Sistema");
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["Integ_TipoEntidade"]);
                    da.Fill(CurrentDataSet, "Integ_TipoEntidade");

                    // TipoTipologias:
                    da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TipoTipologias"]);
                    da.Fill(CurrentDataSet, "TipoTipologias");
				}
				catch (Exception) 
				{
					throw;
				}
			}
		}

		public override int GetRowCount(string TableName, IDbConnection Conn) 
		{
			return GetRowCount(TableName, "", Conn);
		}

		public override int GetRowCount(string TableName, string Suffix, IDbConnection Conn)
		{
			SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM [" + TableName + "] " + Suffix, (SqlConnection) Conn);
			return (int) command.ExecuteScalar();
		}

		#region "Foreign key resolution"
		public override string GetJoinExpression(DataRelation DataRelation, DataRow ChildDataRow)
		{
			StringBuilder Result = new StringBuilder();
			for (int I = 0; I <= DataRelation.ParentColumns.Length - 1; I++) 
			{
				if (Result.Length > 0) 
					Result.Append(" AND ");
				
				Result.Append(string.Format("{0}.{1}={2}", DataRelation.ParentTable.TableName, DataRelation.ParentColumns[I].ColumnName, ChildDataRow[DataRelation.ChildColumns[I].ColumnName].ToString()));				
			}
			return Result.ToString();
		}

		public override void FixRow(DataSet ds, DataRow dr, IDbConnection conn)
		{
			foreach (DataRelation rel in dr.Table.ParentRelations) 
			{
				if (!(dr.IsNull(rel.ChildColumns[0]))) 
				{
					string str = "WHERE " + GetJoinExpression(rel, dr);
                    using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
					{
						da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(ds.Tables[rel.ParentTable.TableName],
							str);
						da.Fill(ds, rel.ParentTable.TableName);
					}					
				}
			}
			dr.ClearErrors();
		}
		#endregion
	}
}
