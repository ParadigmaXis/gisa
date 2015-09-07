using System;
using System.Collections.Generic;
using System.Text;

namespace GISA.Search
{
    public class UnidadeFisicaSearch
    {
        private string numero;
        public string Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        private string designacao;
        public string Designacao
        {
            get { return designacao; }
            set { designacao = value; }
        }

        private string cota;
        public string Cota
        {
            get { return cota; }
            set { cota = value; }
        }

        private string codigoBarras;
        public string CodigoBarras
        {
            get { return codigoBarras; }
            set { codigoBarras = value; }
        }

        private string dataProducaoInicioDoInicio;
        public string DataProducaoInicioDoInicio
        {
            get { return dataProducaoInicioDoInicio; }
            set { dataProducaoInicioDoInicio = value; }
        }

        private string dataProducaoFimDoInicio;
        public string DataProducaoFimDoInicio
        {
            get { return dataProducaoFimDoInicio; }
            set { dataProducaoFimDoInicio = value; }
        }

        private string dataProducaoInicioDoFim;
        public string DataProducaoInicioDoFim
        {
            get { return dataProducaoInicioDoFim; }
            set { dataProducaoInicioDoFim = value; }
        }

        private string dataProducaoFimDoFim;
        public string DataProducaoFimDoFim
        {
            get { return dataProducaoFimDoFim; }
            set { dataProducaoFimDoFim = value; }
        }

        private string conteudoInformacional;
        public string ConteudoInformacional
        {
            get { return conteudoInformacional; }
            set { conteudoInformacional = value; }
        }

        private string tipoUnidadeFisica;
        public string TipoUnidadeFisica 
        {
            get { return tipoUnidadeFisica; }
            set { tipoUnidadeFisica = value; }
        }

        private string operadorGrupo;
        public string OperadorGrupo
        {
            get { return operadorGrupo; }
            set { operadorGrupo = value; }
        }

        private string dataEdicaoInicio;
        public string DataEdicaoInicio
        {
            get { return dataEdicaoInicio; }
            set { dataEdicaoInicio = value; }
        }

        private string dataEdicaoFim;
        public string DataEdicaoFim
        {
            get { return dataEdicaoFim; }
            set { dataEdicaoFim = value; }
        }        

        private string guiaIncorporacao;
        public string GuiaIncorporacao
        {
            get { return guiaIncorporacao; }
            set { guiaIncorporacao = value; }
        }

        public string eliminado;
        public string Eliminado {
            get { return eliminado; }
            set { eliminado = value; }
        }

        public UnidadeFisicaSearch() {
            this.numero = "";
            this.designacao = "";
            this.cota = "";
            this.codigoBarras = "";
            this.dataProducaoInicioDoInicio = "";
            this.dataProducaoFimDoInicio = "";
            this.dataProducaoInicioDoFim = "";
            this.dataProducaoFimDoFim = "";
            this.conteudoInformacional = "";
            this.operadorGrupo = "";
            this.dataEdicaoInicio = "";
            this.dataEdicaoFim = "";
            this.guiaIncorporacao = "";
            this.eliminado = "";
        }

        public override string ToString() {
            StringBuilder ret = new StringBuilder();

            ret.Append("(existe: sim)");

            if (this.numero.Trim().Length > 0)
            {
                ret.Append(" AND ");
                ret.Append(string.Format("(numero:{0})", this.numero));                
            }

            if (this.designacao.Trim().Length > 0)
            {
                ret.Append(" AND ");
                ret.Append(string.Format("(designacao:({0}))", this.designacao));                
            }

            if (this.cota.Trim().Length > 0)
            {
                ret.Append(" AND ");
                ret.Append(string.Format("(cota:({0}))", this.cota));
                //ret.Append(string.Format("(cota:({0}))", this.cota));
            }

            if (this.codigoBarras.Trim().Length > 0)
            {
                ret.Append(" AND ");
                ret.Append(string.Format("(codigoBarras:{0})", this.codigoBarras));
            }

            if (this.conteudoInformacional.Trim().Length > 0)
            {
                ret.Append(" AND ");
                ret.Append(string.Format("(conteudoInformacional:({0}))", this.conteudoInformacional));                
            }

            if (this.tipoUnidadeFisica.Trim().Length > 0)
            {
                ret.Append(" AND ");
                ret.Append(string.Format("(tipoUnidadeFisica:{0})", this.tipoUnidadeFisica));                
            }            

            // Intervalo de datas do inicio da producao
            if (this.dataProducaoInicioDoInicio.Trim().Length > 0 && this.dataProducaoFimDoInicio.Trim().Length > 0)
            {
                ret.Append(" AND ");
                ret.Append(string.Format("(dataProducaoInicio:[{0} TO {1}])", this.dataProducaoInicioDoInicio, this.dataProducaoFimDoInicio));                            
            }
            else if (this.dataProducaoInicioDoInicio.Trim().Length > 0 && !(this.dataProducaoFimDoInicio.Trim().Length > 0))
            {
                ret.Append(" AND ");
                ret.Append(string.Format("(dataProducaoInicio:[{0} TO 99999999])", this.dataProducaoInicioDoInicio));                            
            }
            else if (!(this.dataProducaoInicioDoInicio.Trim().Length > 0) && this.dataProducaoFimDoInicio.Trim().Length > 0)
            {
                ret.Append(" AND ");
                ret.Append(string.Format("(dataProducaoInicio:[00000000 TO {0}])", this.dataProducaoFimDoInicio));
            }

            // Intervalo de datas do fim da producao
            if (this.dataProducaoInicioDoFim.Trim().Length > 0 && this.dataProducaoFimDoFim.Trim().Length > 0)
            {
                ret.Append(" AND ");
                ret.Append(string.Format("(dataProducaoFim:[{0} TO {1}])", this.dataProducaoInicioDoFim, this.dataProducaoFimDoFim));
            }
            else if (this.dataProducaoInicioDoFim.Trim().Length > 0 && !(this.dataProducaoFimDoFim.Trim().Length > 0))
            {
                ret.Append(" AND ");
                ret.Append(string.Format("(dataProducaoFim:[{0} TO 99999999])", this.dataProducaoInicioDoFim));
            }
            else if (!(this.dataProducaoInicioDoFim.Trim().Length > 0) && this.dataProducaoFimDoFim.Trim().Length > 0)
            {
                ret.Append(" AND ");
                ret.Append(string.Format("(dataProducaoFim:[00000000 TO {0}])", this.dataProducaoFimDoFim));
            }

            // Intervalo de datas de edicao
            if (this.dataEdicaoInicio.Trim().Length > 0 && this.dataEdicaoFim.Trim().Length > 0)
            {
                ret.Append(" AND ");
                ret.Append(string.Format("(dataEdicao:[{0} TO {1}])", this.dataEdicaoInicio, this.dataEdicaoFim));
            }
            else if (this.dataEdicaoInicio.Trim().Length > 0 && !(this.dataEdicaoFim.Trim().Length > 0))
            {
                ret.Append(" AND ");
                ret.Append(string.Format("(dataEdicao:[{0} TO 99999999])", this.dataEdicaoInicio));
            }
            else if (!(this.dataEdicaoInicio.Trim().Length > 0) && this.dataEdicaoFim.Trim().Length > 0)
            {
                ret.Append(" AND ");
                ret.Append(string.Format("(dataEdicao:[00000000 TO {0}])", this.dataEdicaoFim));
            }

            if (this.operadorGrupo.Trim().Length > 0)
            {
                ret.Append(" AND ");
                ret.Append(string.Format("(operadorGrupo:({0}))", this.operadorGrupo));
            }

            if (this.guiaIncorporacao.Trim().Length > 0)
            {
                ret.Append(" AND ");
                ret.Append(string.Format("(guiaIncorporacao:({0}))", this.guiaIncorporacao));
            }

            if (this.Eliminado.Trim().Length > 0)
            {
                ret.Append(" AND ");
                ret.Append(string.Format("(eliminado:{0})", this.Eliminado));
            }

            return ret.ToString();
        }
    }
}
