using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GISA.Model;

namespace GISA.Import
{
    public class Registo
    {
        public GISADataset.FRDBaseRow CurrentFRDBase;
        public GISADataset.TrusteeUserRow tuOperator;
        public GISADataset.TrusteeUserRow tuAuthor;
        public DateTime data;

        public Registo() { }
    }
}
