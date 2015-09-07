using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GISA.IntGestDoc.Model.EntidadesExternas;

namespace GISA.IntGestDoc.Controllers
{
    /// <summary>
    /// Services that the external system
    /// provides.
    /// </summary>
    public interface IIntGestDocService
    {
        List<DocumentoExterno> GetDocumentos(DateTime timeStamp, int maxDocs);        
    }
}
