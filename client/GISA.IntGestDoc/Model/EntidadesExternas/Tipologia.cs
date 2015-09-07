using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.IntGestDoc.Model.EntidadesExternas
{
    public class Tipologia : RegistoAutoridadeExterno
    {
        public string ID { get; set; }

        public override TipoEntidadeExterna Tipo { get { return TipoEntidadeExterna.TipologiaInformacional; } }

        public override string IDExterno
        {
            get { return (this.ID == null ? this.Titulo : this.ID); }
        }

        public Tipologia(Sistema sistema, DateTime timestamp) : base(sistema, timestamp) { }
    }
}
