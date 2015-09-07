using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.IntGestDoc.Model.EntidadesExternas
{
    public class Onomastico : RegistoAutoridadeExterno
    {
        public string NIF { get; set; }

        public override TipoEntidadeExterna Tipo { get { return TipoEntidadeExterna.Onomastico; } }

        public override string IDExterno
        {
            get { return (this.NIF != null && this.NIF.Length != 0 ? this.NIF : this.Titulo); }
        }

        public Onomastico(Sistema sistema, DateTime timestamp) : base(sistema, timestamp) { }
    }
}
