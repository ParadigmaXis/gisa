using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.IntGestDoc.Model.EntidadesInternas
{
    public abstract class DocumentoInterno : EntidadeInterna
    {
        public string Codigo { get; set; }
        public TipoEntidadeInterna Tipo { get; set; }

        public override int GetHashCode()
        {
            return this.Codigo.GetHashCode() ^ this.Titulo.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj is DocumentoInterno)
            {
                DocumentoInterno other = (DocumentoInterno)obj;
                isEqual = this.Codigo.Equals(other.Codigo) && this.Titulo.Equals(other.Titulo) && this.Id == other.Id;
            }
            return isEqual;
        }

        public string CodigoComTitulo
        {
            get 
            {
                if (this.Codigo.Equals(this.Titulo))
                    return this.Codigo;
                else
                    return this.Codigo + " - " + this.Titulo;
            }
        }
    }
}
