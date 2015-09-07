using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.IntGestDoc.Model.EntidadesExternas
{
    public class DocumentoAnexo: DocumentoExterno
    {
        public string NUD { get; set; }
        public string TipoDescricao { get; set; }
        public string Descricao { get; set; }
        public string Assunto { get; set; }
        public string DocumentoSimples { get; set; } // esta propriedade é necessária por questões de interface; serve para indicar ao utilizador o documento simples que contém o anexo
        public DocumentoComposto Processo { get; set; }

        public override string IDExterno
        {
            get { return NUD; }
        }

        public override TipoEntidadeExterna Tipo
        {
            get { return TipoEntidadeExterna.DocumentoAnexo; }
        }

        public DocumentoAnexo(Sistema sistema, DateTime timestamp, int orderNr) : base(sistema, timestamp, orderNr) { }

        public override int GetHashCode()
        {
            return NUD.GetHashCode() ^ Sistema.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj is DocumentoAnexo)
            {
                DocumentoAnexo other = (DocumentoAnexo)obj;
                isEqual = this.NUD == other.NUD && this.Sistema == other.Sistema;
            }
            return isEqual;
        }
    }
}
