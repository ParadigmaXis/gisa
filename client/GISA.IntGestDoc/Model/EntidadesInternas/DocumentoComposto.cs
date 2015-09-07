using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.IntGestDoc.Model.EntidadesInternas
{
    public class DocumentoComposto : DocumentoInterno
    {
        public List<string> Produtores { get; set; }
    }
}
