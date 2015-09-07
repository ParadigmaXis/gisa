using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GISA.Model
{
    public class PostSaveAction
    {
        public PersistencyHelper.PostSaveDelegate postSaveDelegate;
        public PersistencyHelper.PostSaveArguments args;
        public void ExecuteAction()
        {
            if (!args.cancelAction && this.postSaveDelegate != null)
                this.postSaveDelegate(this.args);
        }
    }
}
