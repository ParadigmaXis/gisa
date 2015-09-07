using System.Collections;

namespace DBAbstractDataLayer
{
	public interface IMetaModelProvider
	{
		ArrayList getColumnTypes(string tableName, string sgbd);
	}
}
