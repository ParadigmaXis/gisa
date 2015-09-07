using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Controls
{
	public class PaginatedLVGetItemsPN : PaginatedLVGetItems
	{
        public long trusteeID;
        public PaginatedLVGetItemsPN(ArrayList rInfo, long tID)
		{
			this.rowsInfo = rInfo;
            this.trusteeID = tID;
		}
	}
}