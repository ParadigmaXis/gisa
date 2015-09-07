using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.IntGestDoc.Model.EntidadesInternas
{
    public class Ideografico : RegistoAutoridadeInterno
    {
        public override GISA.Model.TipoNoticiaAut TipoNoticiaAut 
        {
            get { return GISA.Model.TipoNoticiaAut.Ideografico; }
        }
    }
}
