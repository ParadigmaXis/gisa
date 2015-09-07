using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.IntGestDoc.Model.EntidadesInternas
{
    public class Onomastico : RegistoAutoridadeInterno
    {
        public override GISA.Model.TipoNoticiaAut TipoNoticiaAut
        {
            get { return GISA.Model.TipoNoticiaAut.Onomastico; }
        }

        private string codigo = String.Empty;
        public string Codigo { get { return this.codigo; } set { this.codigo = value; } }
    }
}
