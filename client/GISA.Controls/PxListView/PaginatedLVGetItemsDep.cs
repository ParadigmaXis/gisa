using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.Controls
{
    public class PaginatedLVGetItemsDep : PaginatedLVGetItems
	{
        public PaginatedLVGetItemsDep(ArrayList rInfo)
		{
			rowsInfo = rInfo;
		}
	}
}
