using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISAServer.Hibernate.Objects
{
    /// <summary>
    /// An object representation of the results of the sp_getDocumentosComProdutores stored procedure.
    /// </summary>
    [Serializable]
    public partial class SPGetDocumentosComProdutores
    {
        private System.Int64 _IDDocumento;
        private System.String _TituloProdutor;
        private System.String _TituloProdutorAutorizado;

        public virtual System.Int64 IDDocumento
        {
            get { return _IDDocumento; }
            set { _IDDocumento = value; }
        }

        public virtual System.String TituloProdutor
        {
            get { return _TituloProdutor; }
            set
            {
                if (value == null) {
                    throw new NullReferenceException("TituloProdutor must not be null.");
                }
                _TituloProdutor = value;
            }
        }

        public virtual System.String TituloProdutorAutorizado
        {
            get { return _TituloProdutorAutorizado; }
            set { _TituloProdutorAutorizado = value == null? "" : value; }
        }
    }
}
