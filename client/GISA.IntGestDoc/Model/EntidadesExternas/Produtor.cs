using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.IntGestDoc.Model.EntidadesExternas
{
    public class Produtor : RegistoAutoridadeExterno
    {
        public string Codigo { get; set; }
        
        public override TipoEntidadeExterna Tipo { get { return TipoEntidadeExterna.Produtor; } }
        
        public override string IDExterno
        {
            get { return this.Codigo; }
        }

        public Produtor(Sistema sistema, DateTime timestamp) : base(sistema, timestamp) { }
    }
}
