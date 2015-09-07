using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.IntGestDoc.Model.EntidadesExternas
{
    public abstract class RegistoAutoridadeExterno : EntidadeExterna
    {
        public string Titulo { get; set; }

        public RegistoAutoridadeExterno(Sistema sistema, DateTime timestamp) : base(sistema, timestamp) { }

        public override int GetHashCode()
        {
            return this.IDExterno.GetHashCode() ^ this.Sistema.GetHashCode() ^ this.Tipo.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj is RegistoAutoridadeExterno)
            {
                RegistoAutoridadeExterno other = (RegistoAutoridadeExterno)obj;
                isEqual = this.IDExterno == other.IDExterno && this.Sistema == other.Sistema && this.Tipo == other.Tipo;
            }
            return isEqual;
        }

        
    }
}
