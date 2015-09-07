using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.Model
{
    public class PreTransactionAction
    {
        public PersistencyHelper.PreTransactionDelegate preTransactionDelegate;
        public PersistencyHelper.PreTransactionArguments args;
        public void ExecuteAction()
        {
            if (this.preTransactionDelegate != null)
                this.preTransactionDelegate(this.args);
        }
    }
}
