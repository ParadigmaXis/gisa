using System;
using System.Net;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace GISA.Search
{
    public class SearchWebException : Exception
    {
        public Exception mWebException;
        public SearchWebException(WebException webException)
        {
            mWebException = webException;
        }
    }
}
