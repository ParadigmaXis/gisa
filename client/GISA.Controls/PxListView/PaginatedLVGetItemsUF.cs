using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA.Controls
{
	public class PaginatedLVGetItemsUF : PaginatedLVGetItems
	{

		public PaginatedLVGetItemsUF(ArrayList rInfo)
		{
			rowsInfo = rInfo;
		}
	}

} //end of root namespace