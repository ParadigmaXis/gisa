using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA
{
	public class PaginatedLVGetItemsPesq : PaginatedLVGetItems
	{

		public PaginatedLVGetItemsPesq(ArrayList rInfo)
		{
			rowsInfo = rInfo;
		}
	}

} //end of root namespace