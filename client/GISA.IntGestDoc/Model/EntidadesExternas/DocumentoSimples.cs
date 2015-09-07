using System;
using System.Collections.Generic;
using System.Linq;

namespace GISA.IntGestDoc.Model.EntidadesExternas
{

    public class DocumentoSimples : DocumentoExterno
    {
        public string NUD { get; set; }
        public string NUDCapa { get; set; }
        public DocumentoComposto Processo { get; set; }
        public string NumeroEspecifico { get; set; }
        public string DataCriacao { get; set; }
        public string DataArquivoGeral { get; set; }
        public Ideografico Ideografico { get; set; }
        public string Assunto { get; set; }
        public Onomastico Onomastico { get; set; }
        public Geografico Toponimia { get; set; }
        public Tipologia Tipologia { get; set; }
        public Onomastico TecnicoDeObra { get; set; }
        public string Notas { get; set; }
        public string RefPredial { get; set; }
        public string Local { get; set; }
        public string NumPolicia { get; set; }
        public string CodPostal { get; set; }
        public string Localidade { get; set; }

        public override TipoEntidadeExterna Tipo { get { return TipoEntidadeExterna.Documento; } }

        public override string IDExterno
        {
            get { return this.NUD; }
        }

        public List<RegistoAutoridadeExterno> RegistosAutoridade
        {
            get
            {
                return new List<RegistoAutoridadeExterno>() { Ideografico, Onomastico, Toponimia, Tipologia, TecnicoDeObra }.Where(rae => rae != null).ToList();
            }
        }

        public DocumentoSimples(Sistema sistema, DateTime timestamp, int orderNr) : base(sistema, timestamp, orderNr) { }

        public override int GetHashCode()
        {
            return NUD.GetHashCode() ^ Sistema.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj is DocumentoSimples)
            {
                DocumentoSimples other = (DocumentoSimples)obj;
                isEqual = this.NUD == other.NUD && this.Sistema == other.Sistema;
            }
            return isEqual;
        }
    }
}
