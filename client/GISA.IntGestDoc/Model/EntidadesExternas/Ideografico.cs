using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.IntGestDoc.Model.EntidadesExternas
{
    public class Ideografico : RegistoAutoridadeExterno
    {
        public override TipoEntidadeExterna Tipo { get { return TipoEntidadeExterna.Ideografico; } }

        public override string IDExterno
        {
            get { return this.Titulo; }
        }

        public Ideografico(Sistema sistema, DateTime timestamp) : base(sistema, timestamp) { }
    }
}
