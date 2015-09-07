using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.IntGestDoc.Model
{
    public enum Sistema
    {
        DocInPorto = 1
    }

    public abstract class EntidadeExterna : Entidade
    {
        public abstract TipoEntidadeExterna Tipo { get; }
        public abstract string IDExterno { get; }
        public Sistema Sistema { get; set; }
        public DateTime Timestamp { get; set; }

        public EntidadeExterna(Sistema sistema, DateTime timestamp)
        {
            this.Sistema = sistema;
            this.Timestamp = timestamp;
        }

        public override int GetHashCode()
        {
            return this.IDExterno.GetHashCode() ^ this.Sistema.GetHashCode() ^ this.Tipo.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj is EntidadeExterna)
            {
                EntidadeExterna other = (EntidadeExterna)obj;
                isEqual = this.IDExterno == other.IDExterno && this.Sistema == other.Sistema && this.Tipo == other.Tipo;
            }
            return isEqual;
        }

    }
}
