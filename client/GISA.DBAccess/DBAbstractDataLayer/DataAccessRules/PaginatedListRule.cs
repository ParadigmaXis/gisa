using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBAbstractDataLayer.DataAccessRules
{
    public abstract class PaginatedListRule : DALRule
    {
        private static PaginatedListRule current = null;
        public static void ClearCurrent()
        {
            current = null;
        }
        public static PaginatedListRule Current
        {
            get
            {
                if (Object.ReferenceEquals(null, current))
                {
                    current = (PaginatedListRule)Create(typeof(PaginatedListRule));
                }
                return current;
            }
        }

        public abstract int GetPageForID(long ID, int pageLimit, IDbConnection conn);
        public abstract int CountPages(int itemsPerPage, out int numberOfItems, IDbConnection conn);
    }
}
