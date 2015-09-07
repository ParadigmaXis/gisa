using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.IntGestDoc.Model.EntidadesExternas
{
    public class DocumentoComposto: DocumentoExterno 
    {
        public struct LocalizacaoObraActual
        {
            public Geografico LocalizacaoObraDesignacaoActual;
            public string NroPolicia;
        }
        public string NUP { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public string Confidencialidade { get; set; }
        public Tipologia Tipologia { get; set; }
        public Produtor Produtor { get; set; }

        public HashSet<string> RequerentesOuProprietariosIniciais = new HashSet<string>();
        public HashSet<string> AverbamentosDeRequerenteOuProprietario = new HashSet<string>();
        public HashSet<LocalizacaoObraActual> LocalizacoesObraDesignacaoActual = new HashSet<LocalizacaoObraActual>();
        public HashSet<Onomastico> TecnicosDeObra = new HashSet<Onomastico>();

        public override TipoEntidadeExterna Tipo { get { return TipoEntidadeExterna.DocumentoComposto; } }

        public override string IDExterno
        {
            get { return this.NUP; }
        }

        public List<RegistoAutoridadeExterno> RegistosAutoridade
        {
            get
            {
                var ras = new List<RegistoAutoridadeExterno>();
                ras.AddRange(LocalizacoesObraDesignacaoActual.Select(c => c.LocalizacaoObraDesignacaoActual).Cast<RegistoAutoridadeExterno>());
                ras.AddRange(TecnicosDeObra.Cast<RegistoAutoridadeExterno>());
                if (Tipologia != null) ras.Add(Tipologia);
                if (Produtor != null) ras.Add(Produtor);
                return ras;
            }
        }

        public DocumentoComposto(Sistema sistema, DateTime timestamp, int orderNr) : base(sistema, timestamp, orderNr) { }

        public override int GetHashCode()
        {
            return NUP.GetHashCode() ^ Sistema.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj is DocumentoComposto)
            {
                DocumentoComposto other = (DocumentoComposto)obj;
                isEqual = this.NUP == other.NUP && this.Sistema == other.Sistema;
            }
            return isEqual;
        }
    }
}
