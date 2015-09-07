//INSTANT C# NOTE: Formerly VB.NET project-level imports:
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;

#if DEBUG
using NUnit.Framework;
#endif
using System.Xml;
using System.Xml.XPath;

namespace GISA.Model
{
	public class MetaModelHelper : DBAbstractDataLayer.IMetaModelProvider
	{

		private static XmlDocument mMetaModel = null;
		public static XmlDocument MetaModel
		{
			get
			{
				return mMetaModel;
			}
			set
			{
				mMetaModel = value;
			}
		}

		private static XmlDocument mDataTypesDictionary = null;
		public static XmlDocument DataTypesDictionary
		{
			get
			{
				return mDataTypesDictionary;
			}
			set
			{
				mDataTypesDictionary = value;
			}
		}

		private static XmlDocument mFriendlyNames = null;
		public static XmlDocument FriendlyNames
		{
			get
			{
				return mFriendlyNames;
			}
			set
			{
				mFriendlyNames = value;
			}
		}

		//compilar a expressao passada como argumento para xpath
		private static XPathNavigator metaModelXPathNavigator = null;
		private static Hashtable metaModelXPathExpressions = new Hashtable(); //manter guardado o comando sql de forma a ser gerado e compilado 1 única vez
		private static XPathExpression getMetaModelXPathExpression(string xpath)
		{
			XPathExpression metaModelXPathExpression = (XPathExpression)(metaModelXPathExpressions[xpath]);
			if (metaModelXPathExpression == null)
			{
				if (metaModelXPathNavigator == null)
				{
					metaModelXPathNavigator = MetaModel.CreateNavigator();
				}
				metaModelXPathExpression = metaModelXPathNavigator.Compile(xpath);
				metaModelXPathExpressions.Add(xpath, metaModelXPathExpression);
			}
			return metaModelXPathExpression;
		}

		protected struct MetaType
		{
			public string name;
			public string encoding;
			public string size;
			public bool variable;
		}

		//funcao que retorna o tipo em oleDB correspondente ao metatipo passado como argumento
		protected static string TranslateType(MetaType metaType, string SqlDialect)
		{
			XPathNavigator dictionaryXPathNavigator = mDataTypesDictionary.CreateNavigator();

			string dbTypeStr = null;
			XPathNodeIterator dbTypeIterator = null;

			XPathNodeIterator iterator = null;
			//a distinção entre as strings, inteiros e outros deve-se à informação necessária para a determinação dos tipos correspondentes em oleDB
			switch (metaType.name)
			{
				case "string":
					string minStr = null;
					//testa logo à cabeça se o metatipo tem definido como ilimitado o tamanho máximo pretendido (só acontece para bases de dados em postGrés)
					if (metaType.size.Equals("ILIMITADO"))
					{
						minStr = "ILIMITADO";
					}
					else
					{

						//calculo do tipo que suporta o tamanho definido no meta tipo e o que melhor se adequa (por exemplo, se o pretendido for 300 caracteres não é necessário uma variavel do tipo ntext)
						iterator = dictionaryXPathNavigator.Select(string.Format("//tipo[@tipoBase='{0}' and @encoding='{1}' and @comp_variavel='{2}']", metaType.name, metaType.encoding, metaType.variable));

						int min = int.MaxValue;

						int tmpMin = 0;
						while (iterator.MoveNext())
						{
							iterator.Current.MoveToFirstAttribute();
							do
							{
								switch (iterator.Current.Name)
								{
									case "tamanho":
										//valor que não entra para o cálculo do mínimo (verificado logo à cabeça)
										if (iterator.Current.Value.Equals("ILIMITADO"))
										{
											goto ExitLabel1;
										}
										//calculo do valor minimo que cobre o tamanho pretendido de forma a selecionar o tamanho correspondente
										tmpMin = int.Parse(iterator.Current.Value);
										if (tmpMin < min && min > int.Parse(metaType.size))
										{
											min = tmpMin;
										}
										//dado que só o "tamanho" interessa não é necessário percorrer os restantes atributos
										goto ExitLabel1;
								}
								if (! (iterator.Current.MoveToNextAttribute()))
								{
									break;
								}
							} while (true);
							ExitLabel1: ;
						}
						minStr = min.ToString();
					}
					dbTypeIterator = dictionaryXPathNavigator.Select(string.Format("//tipo[@tipoBase='{0}' and @encoding='{1}' and @comp_variavel='{2}' and @tamanho='{3}']/@{4}", metaType.name, metaType.encoding, metaType.variable, minStr, SqlDialect));
					break;
				case "integer":
					dbTypeIterator = dictionaryXPathNavigator.Select(string.Format("//tipo[@tipoBase='{0}' and @tamanho='{1}']/@{2}", metaType.name, metaType.size, SqlDialect));
					break;
				default:
					dbTypeIterator = dictionaryXPathNavigator.Select(string.Format("//tipo[@tipoBase='{0}']/@{1}", metaType.name, SqlDialect));
					break;
			}
			dbTypeIterator.MoveNext();
			dbTypeStr = dbTypeIterator.Current.Value.ToString();
			return dbTypeStr;
		}

		protected static string TranslateType(string metaType, string SqlDialect)
		{
			XPathNavigator dictionaryXPathNavigator = mDataTypesDictionary.CreateNavigator();

			string dbTypeStr = null;
			XPathNodeIterator dbTypeIterator = null;

			dbTypeIterator = dictionaryXPathNavigator.Select(string.Format("//tipo[@nome='{0}']/@{1}", metaType, SqlDialect));
			dbTypeIterator.MoveNext();
			dbTypeStr = dbTypeIterator.Current.Value.ToString();
			return dbTypeStr;

		}

		//método que devolve um arraylist com os tipos em oleDB da tabela 'tablename' para serem passados ao customCommandBuilder
		private static XPathExpression columnTypesExpression = null;
		public System.Collections.ArrayList getColumnTypes(string tableName, string sgbd)
		{
			if (mMetaModel == null)
			{
				return null;
			}

			//compilar o xpath
			columnTypesExpression = getMetaModelXPathExpression(string.Format("//tab[@nome='{0}']/colunas/coluna", tableName));

			XPathNodeIterator iterator = metaModelXPathNavigator.Select(columnTypesExpression);

			ArrayList result = new ArrayList();

			//por cada coluna da tabela...
			while (iterator.MoveNext())
			{
				MetaType metaType = new MetaType();

				//por cada atributo da coluna da tabela...
				iterator.Current.MoveToFirstAttribute();
				do
				{
					switch (iterator.Current.Name)
					{
						case "tipo":
							metaType.name = iterator.Current.Value;
							break;
						case "encoding":
							metaType.encoding = iterator.Current.Value;
							break;
						case "tamanho":
							metaType.size = iterator.Current.Value;
							break;
						case "comp_variavel":
							if (iterator.Current.Value.ToLower().Equals("true"))
							{
								metaType.variable = true;
							}
							else if (iterator.Current.Value.ToLower().Equals("false"))
							{
								metaType.variable = false;
							}

							break;
					}

					if (! (iterator.Current.MoveToNextAttribute()))
					{
						break;
					}
				} while (true);
				
				result.Add((System.Data.SqlDbType)(Enum.Parse(typeof(System.Data.SqlDbType), TranslateType(metaType, sgbd))));
			}
			return result;
		}

		//obter os nomes utilizados na interface para facilitar a indicação dos campos em conflito ao utilizador 
		private static XPathExpression friendlyNameExpression = null;
		private static Hashtable metaModelFriendlyNames = new Hashtable();

		public static string getFriendlyName(string tableName)
		{
			return getFriendlyName(tableName, null);
		}

		public static string getFriendlyName(string tableName, string columnName)
		{
			string key = tableName;
			if (columnName != null)
			{
				key = key + " " + columnName;
			}

			string friendlyName = (string)(metaModelFriendlyNames[key]);

			//se não existir nenhuma entrada na hashtable cria uma com a chave definida pelo nome da tabela e da coluna associado a descricao respectiva
			if (friendlyName == null && ! (metaModelFriendlyNames.ContainsKey(key)))
			{
				if (columnName != null)
				{
					friendlyNameExpression = getMetaModelXPathExpression(string.Format("//tab[@nome='{0}']/colunas/coluna[@nome='{1}']/@descricao", tableName, columnName));
				}
				else
				{
					friendlyNameExpression = getMetaModelXPathExpression(string.Format("//tab[@nome='{0}']/@descricao", tableName));
				}

				XPathNodeIterator iterator = metaModelXPathNavigator.Select(friendlyNameExpression);

				//necessario para "saltar" para o nó pretendido
				if (iterator.MoveNext())
				{
					friendlyName = iterator.Current.Value;
				}


				if (friendlyName != null && friendlyName.Length > 0)
				{
					//adicionar o nome à hash table
					metaModelFriendlyNames.Add(key, friendlyName);
				}
				else
				{
					if (columnName != null)
					{
						Debug.WriteLine(string.Format("Friendly Name not defined (table, column): {0} {1}", tableName, columnName));
					}
					else
					{
						Debug.WriteLine(string.Format("Friendly Name not defined (table): {0}", tableName));
					}

				}

			}

			return friendlyName;
		}
	}

	#if DEBUG
	[TestFixture()]
	public class TestMetaModelHelper
	{

		public class MetaModelHelperPublisher : MetaModelHelper
		{

			public static new string TranslateType(string metaType, string SqlDialect)
			{
				return MetaModelHelper.TranslateType(metaType, SqlDialect);
			}
		}

		[Test()]
		public void TranslateType()
		{
			string metaType = "integerXL";
			string SqlDialect = "Transact-SQL";

			Assert.AreEqual(MetaModelHelperPublisher.TranslateType(metaType, SqlDialect), "BigInt");
		}
	}
	#endif

} //end of root namespace