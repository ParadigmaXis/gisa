using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.IntGestDoc.Model
{
    public abstract class EntidadeInterna : Entidade
    {
        public long Id { get; set; }
        public virtual string Titulo { get; set; }
        public TipoEstado Estado { get; set; }
    }
}
