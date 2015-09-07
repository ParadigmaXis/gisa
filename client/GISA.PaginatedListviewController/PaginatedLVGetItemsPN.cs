using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA
{
	public class PaginatedLVGetItemsPN : PaginatedLVGetItems
	{
		public Dictionary<long, string> designacoes;
        public long trusteeID;
        public PaginatedLVGetItemsPN(ArrayList rInfo, Dictionary<long, string> desig, long tID)
		{
			rowsInfo = rInfo;
			designacoes = desig;
            trusteeID = tID;
		}
	}
}