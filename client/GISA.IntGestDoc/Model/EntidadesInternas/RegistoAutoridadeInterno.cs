using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GISA.IntGestDoc.Model;

namespace GISA.IntGestDoc.Model.EntidadesInternas
{
    public abstract class RegistoAutoridadeInterno : EntidadeInterna
    {
        public abstract GISA.Model.TipoNoticiaAut TipoNoticiaAut { get; }

        public override int GetHashCode()
        {
            return this.Titulo.GetHashCode() ^ this.TipoNoticiaAut.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj is RegistoAutoridadeInterno)
            {
                RegistoAutoridadeInterno other = (RegistoAutoridadeInterno)obj;
                isEqual = this.Titulo == other.Titulo && this.TipoNoticiaAut == other.TipoNoticiaAut;
            }
            return isEqual;
        }
    }
}
