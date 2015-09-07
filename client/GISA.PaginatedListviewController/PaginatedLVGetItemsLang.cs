using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA
{
	public class PaginatedLVGetItemsLang : PaginatedLVGetItems
	{

		public PaginatedLVGetItemsLang(ArrayList rInfo)
		{
			rowsInfo = rInfo;
		}

	}

} //end of root namespace