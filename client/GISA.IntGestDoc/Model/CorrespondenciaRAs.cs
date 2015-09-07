using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GISA.IntGestDoc.Model.EntidadesExternas;
using GISA.IntGestDoc.Model.EntidadesInternas;

namespace GISA.IntGestDoc.Model
{
    public class CorrespondenciaRAs : Correspondencia
    {
        internal CorrespondenciaRAs(RegistoAutoridadeExterno rae, RegistoAutoridadeInterno rai, TipoSugestao tipoSugestao)
            :base(rae, rai, tipoSugestao) { }

        public string TituloRAI
        {
            get { return this.EntidadeInterna.Titulo; }
        }
    }
}
