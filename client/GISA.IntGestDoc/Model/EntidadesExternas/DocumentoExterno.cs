using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.IntGestDoc.Model.EntidadesExternas
{
    public abstract class DocumentoExterno: EntidadeExterna
    {
        // property para preservar ordenação dos registos obtidos do webservice
        private int _orderNr = 0;
        public int OrderNr { get { return _orderNr; } }

        public struct Conteudo
        {
            public string Tipo;
            public string Titulo;
            public string Ficheiro;
            public string TipoDescricao;
        }

        public List<Conteudo> Conteudos { get; set; }

        public DocumentoExterno(Sistema sistema, DateTime timestamp, int orderNr) : base(sistema, timestamp) { _orderNr = orderNr; }
    }
}
