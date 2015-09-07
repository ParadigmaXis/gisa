using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.IntGestDoc.Model
{
    public class CorrespondenciaDocsCompostos: Correspondencia
    {
        internal CorrespondenciaDocsCompostos(Model.EntidadesExternas.DocumentoComposto dce, Model.EntidadesInternas.DocumentoGisa dci, TipoSugestao tipoSugestao)
            :base(dce, dci, tipoSugestao) { }
    }
}
