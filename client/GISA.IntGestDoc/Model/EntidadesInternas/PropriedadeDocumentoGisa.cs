using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.IntGestDoc.Model.EntidadesInternas
{
    public abstract class PropriedadeDocumentoGisa
    {
        public TipoOpcao TipoOpcao { get; set; }

        public abstract void Revert();
    }
}
