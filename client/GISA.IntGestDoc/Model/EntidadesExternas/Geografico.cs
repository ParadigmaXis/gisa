using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.IntGestDoc.Model.EntidadesExternas
{
    public class Geografico : RegistoAutoridadeExterno
    {
        public string Codigo { get; set; }
        public string NroPolicia { get; set; }

        public override TipoEntidadeExterna Tipo { get { return TipoEntidadeExterna.Geografico; } }

        public override string IDExterno
        {
            get { return this.Codigo; }
        }

        public Geografico(Sistema sistema, DateTime timestamp) : base(sistema, timestamp) {}

        public override int GetHashCode()
        {
            if (this.NroPolicia != null)
                return base.GetHashCode() ^ this.NroPolicia.GetHashCode();
            else
                return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj is Geografico)
            {
                Geografico other = (Geografico)obj;
                isEqual = this.IDExterno == other.IDExterno && this.Sistema == other.Sistema && this.Tipo == other.Tipo && this.NroPolicia == other.NroPolicia;
            }
            return isEqual;
        }
    }
}
