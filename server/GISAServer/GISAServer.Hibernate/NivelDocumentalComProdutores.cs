using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GISAServer.Hibernate.Exceptions;
using GISAServer.Hibernate.Objects;
using GISAServer.Hibernate.Utils;
using log4net;
using NHibernate;
using NHibernate.Criterion;

namespace GISAServer.Hibernate
{
    public class NivelDocumentalComProdutores
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(NivelDocumentalComProdutores));

        private long idDocumento = -1;
        public long IdDocumento
        {
            get { return idDocumento; }
            set { idDocumento = value; }
        }

        private string produtor = "";
        public string Produtor
        {
            get { return produtor; }
            set { produtor = value; }
        }

        private string produtorAutorizado = "";
        public string ProdutorAutorizado
        {
            get { return produtorAutorizado; }
            set { produtorAutorizado = value; }
        }

        public NivelDocumentalComProdutores() { }

        public NivelDocumentalComProdutores(long id)
        {
            this.idDocumento = GISAUtils.DocumentosComProdutores[id].IDDocumento;
            this.produtor = GISAUtils.DocumentosComProdutores[id].TituloProdutor;
            this.produtorAutorizado = GISAUtils.DocumentosComProdutores[id].TituloProdutorAutorizado;
        }

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            ret.Append(this.idDocumento); ret.Append(" ");
            ret.Append(this.produtor); ret.Append(" ");
            ret.Append(this.produtorAutorizado); ret.Append(" ");
            return ret.ToString();
        }


    }
}