using System.Collections.Generic;
using System.Text;
using GISAServer.Hibernate.Exceptions;
using GISAServer.Hibernate.Objects;
using GISAServer.Hibernate.Utils;
using log4net;
using NHibernate;
using NHibernate.Criterion;
using System;

namespace GISAServer.Hibernate
{
    public class NivelDocumentalInternet
    {
        // Logging initializations
        private static readonly ILog log = LogManager.GetLogger(typeof(NivelDocumentalInternet));        

        # region Properties and initializations
 
        // Nivel
        private string id = "";
        public string Id { get { return id; } set { id = value; } }

        private string codigo = "";
        public string Codigo { get { return codigo; } set { codigo = value; } }        
        
        // TipoNivelRelacionado
        private string designacaoTipoNivelRelacionado = "";
        public string DesignacaoTipoNivelRelacionado { get { return designacaoTipoNivelRelacionado; } set { designacaoTipoNivelRelacionado = value; } }

        // SFRDDatasProducao
        private string dataInicioProd = "00000000";
        public string DataInicioProd { get { return dataInicioProd; } set { dataInicioProd = value; } }

        private string dataFimProd = "99999999";
        public string DataFimProd { get { return dataFimProd; } set { dataFimProd = value; } }

        private string inicioAno = "";
        public string InicioAno { get { return inicioAno; } set { inicioAno = value; } }

        private string inicioMes = "";
        public string InicioMes { get { return inicioMes; } set { inicioMes = value; } }

        private string inicioDia = "";
        public string InicioDia { get { return inicioDia; } set { inicioDia = value; } }

        private string fimAno = "";
        public string FimAno { get { return fimAno; } set { fimAno = value; } }

        private string fimMes = "";
        public string FimMes { get { return fimMes; } set { fimMes = value; } }

        private string fimDia = "";
        public string FimDia { get { return fimDia; } set { fimDia = value; } }

        // SFRDConteudoEEstrutura
        private string conteudoInformacional = "";
        public string ConteudoInformacional { get { return conteudoInformacional; } set { conteudoInformacional = value; } }
        
        // Dicionario
        private string termos = ""; // lista
        public string Termos { get { return termos; } set { termos = value; } }

        private string termosDeIndexacao = ""; // lista
        public string TermosDeIndexacao { get { return termosDeIndexacao; } set { termosDeIndexacao = value; } }

        private string tipologiaInformacional = ""; // lista
        public string TipologiaInformacional { get { return tipologiaInformacional; } set { tipologiaInformacional = value; } }

        private string entidadeProdutora = "";
        public string EntidadeProdutora { get { return entidadeProdutora; } set { entidadeProdutora = value; } }

        private string idsControlosAutoridade = ""; // lista
        public string IdsControlosAutoridade { get { return idsControlosAutoridade; } set { idsControlosAutoridade = value; } }

        // Autor
        private string autor = "";
        public string Autor { get { return autor; } set { autor = value; } }

        // SFRDAvaliacao
        private string publicar = "";
        public string Publicar { get { return publicar; } set { publicar = value; } }

        // SFRDNotaGeral
        private string notaGeral = "";
        public string NotaGeral { get { return notaGeral; } set { notaGeral = value; } }

        // NivelDesignado
        private string designacaoNivelDesignado = "";
        public string DesignacaoNivelDesignado { get { return designacaoNivelDesignado; } set { designacaoNivelDesignado = value; } }

        // SFRDImagem
        private string numImagens = "0";
        public string NumImagens { get { return numImagens; } set { numImagens = value; } }

        //IdsUpper
        private string idUpper = ""; // lista (só é preenchido para subséries e (sud)documentos)
        public string IdUpper { get { return idUpper; } set { idUpper = value; } }
        #endregion

        #region Licencas de Obras: Properties and initializations

        // LicencaObra:
        private string tipoObra = string.Empty;
        public string TipoObra { get { return tipoObra; } set { tipoObra = value; } }
        
        private string pHTexto = string.Empty;
        public string PHTexto { get { return pHTexto;  } set { pHTexto = value; } }

        // LicencaObraAtestadoHabitabilidade:
        private string codigosAtestadoHabitabilidade = string.Empty;
        public string CodigosAtestadoHabitabilidade { get { return codigosAtestadoHabitabilidade; } set { codigosAtestadoHabitabilidade = value; } }

        // LicencaObraLocalizacaoObraActual:
        private string numPolicia_LicencaObraLocalizacaoObraActual = string.Empty;
        public string NumPolicia_LicencaObraLocalizacaoObraActual { get { return numPolicia_LicencaObraLocalizacaoObraActual; } set { numPolicia_LicencaObraLocalizacaoObraActual = value; } }
        private string termo_LicencaObraLocalizacaoObraActual = string.Empty;
        public string Termo_LicencaObraLocalizacaoObraActual { get { return termo_LicencaObraLocalizacaoObraActual; } set { termo_LicencaObraLocalizacaoObraActual = value; } }

        // LicencaObraLocalizacaoObraAntiga:
        private string numPolicia_LicencaObraLocalizacaoObraAntiga = string.Empty;
        public string NumPolicia_LicencaObraLocalizacaoObraAntiga { get { return numPolicia_LicencaObraLocalizacaoObraAntiga; } set { numPolicia_LicencaObraLocalizacaoObraAntiga = value; } }
        private string nomeLocal_LicencaObraLocalizacaoObraAntiga = string.Empty;
        public string NomeLocal_LicencaObraLocalizacaoObraAntiga { get { return nomeLocal_LicencaObraLocalizacaoObraAntiga; } set { nomeLocal_LicencaObraLocalizacaoObraAntiga = value; } }

        // LicencaObraRequerentes:
        private string nome_LicencaObraRequerentes = string.Empty;
        public string Nome_LicencaObraRequerentes { get { return nome_LicencaObraRequerentes; } set { nome_LicencaObraRequerentes = value; } }

        // LicencaObraTecnicoObra:
        private string termo_LicencaObraTecnicoObra = string.Empty;
        public string Termo_LicencaObraTecnicoObra { get { return termo_LicencaObraTecnicoObra; } set { termo_LicencaObraTecnicoObra = value; } }
        #endregion

        #region Constructors

        public NivelDocumentalInternet() { }

        public NivelDocumentalInternet(long id)
        {
            this.id = GISAUtils.DocumentosInternet[id].Id;
            this.designacaoTipoNivelRelacionado = GISAUtils.DocumentosInternet[id].DesignacaoTipoNivelRelacionado;
            this.codigo = GISAUtils.DocumentosInternet[id].Codigo;
            this.designacaoNivelDesignado = GISAUtils.DocumentosInternet[id].designacaoNivelDesignado;
            this.inicioAno = GISAUtils.DocumentosInternet[id].InicioAno;
            this.fimAno = GISAUtils.DocumentosInternet[id].FimAno;
            this.inicioMes = GISAUtils.DocumentosInternet[id].InicioMes;
            this.fimMes = GISAUtils.DocumentosInternet[id].FimMes;
            this.inicioDia = GISAUtils.DocumentosInternet[id].InicioDia;
            this.fimDia = GISAUtils.DocumentosInternet[id].FimDia;
            this.dataInicioProd = GISAUtils.DocumentosInternet[id].DataInicioProd;
            this.dataFimProd = GISAUtils.DocumentosInternet[id].DataFimProd;
            this.publicar = GISAUtils.DocumentosInternet[id].Publicar;
            this.conteudoInformacional = GISAUtils.DocumentosInternet[id].ConteudoInformacional;
            this.tipoObra = GISAUtils.DocumentosInternet[id].TipoObra;
            this.pHTexto = GISAUtils.DocumentosInternet[id].PHTexto;
            this.termosDeIndexacao = GISAUtils.DocumentosInternet[id].TermosDeIndexacao;
            this.tipologiaInformacional = GISAUtils.DocumentosInternet[id].TipologiaInformacional;
            this.autor = GISAUtils.DocumentosInternet[id].Autor;
            this.notaGeral = GISAUtils.DocumentosInternet[id].NotaGeral;
            this.numImagens = GISAUtils.DocumentosInternet[id].NumImagens;
            this.codigosAtestadoHabitabilidade = GISAUtils.DocumentosInternet[id].CodigosAtestadoHabitabilidade;
            this.numPolicia_LicencaObraLocalizacaoObraActual = GISAUtils.DocumentosInternet[id].NumPolicia_LicencaObraLocalizacaoObraActual;
            this.termo_LicencaObraLocalizacaoObraActual = GISAUtils.DocumentosInternet[id].Termo_LicencaObraLocalizacaoObraActual;
            this.nome_LicencaObraRequerentes = GISAUtils.DocumentosInternet[id].Nome_LicencaObraRequerentes;
            this.numPolicia_LicencaObraLocalizacaoObraAntiga = GISAUtils.DocumentosInternet[id].NumPolicia_LicencaObraLocalizacaoObraAntiga;
            this.nomeLocal_LicencaObraLocalizacaoObraAntiga = GISAUtils.DocumentosInternet[id].NomeLocal_LicencaObraLocalizacaoObraAntiga;
            this.termo_LicencaObraTecnicoObra = GISAUtils.DocumentosInternet[id].Termo_LicencaObraTecnicoObra;
            this.entidadeProdutora = GISAUtils.DocumentosInternet[id].EntidadeProdutora;
            this.idsControlosAutoridade = GISAUtils.DocumentosInternet[id].IdsControlosAutoridade;
            this.idUpper = GISAUtils.DocumentosInternet[id].IdUpper;
        }

        #endregion

        public override string ToString(){
            StringBuilder ret = new StringBuilder();
            
            // Nivel
            ret.Append(this.id);ret.Append(" ");      
            ret.Append(this.codigo);ret.Append(" ");  

            // TipoNivelRelacionado
            ret.Append(this.designacaoTipoNivelRelacionado);ret.Append(" "); 
            
            // SFRDDatasProducao
            ret.Append(this.inicioAno);ret.Append(" ");
            ret.Append(this.fimAno);ret.Append(" ");
            
            // SFRDConteudoEEstrutura
            ret.Append(this.conteudoInformacional);ret.Append(" ");
            
            // Dicionario
            ret.Append(this.termos);ret.Append(" "); // lista
            ret.Append(this.termosDeIndexacao);ret.Append(" "); // lista
            ret.Append(this.tipologiaInformacional);ret.Append(" "); // lista
            ret.Append(this.entidadeProdutora); ret.Append(" "); // lista
            ret.Append(this.idsControlosAutoridade); ret.Append(" "); // lista

            // Autor
            ret.Append(this.autor); ret.Append(" "); // lista

            // SFRDAvaliacao    
            ret.Append(this.publicar);ret.Append(" ");
            
            // NivelDesignado
            ret.Append(this.designacaoNivelDesignado);ret.Append(" ");

            //IdsUpper
            ret.Append(this.idUpper); ret.Append(" ");

            #region Licencas de obra (toString())
            // LicencaObra
            ret.Append(this.TipoObra); ret.Append(" ");
            ret.Append(this.PHTexto); ret.Append(" ");

            // LicencaObraAtestadoHabitabilidade
            ret.Append(this.CodigosAtestadoHabitabilidade); ret.Append(" ");

            // LicencaObraLocalizacaoObraActual
            ret.Append(this.NumPolicia_LicencaObraLocalizacaoObraActual).Append(" ");
            ret.Append(this.Termo_LicencaObraLocalizacaoObraActual).Append(" ");

            // LicencaObraLocalizacaoObraAntiga
            ret.Append(this.NumPolicia_LicencaObraLocalizacaoObraAntiga).Append(" ");
            ret.Append(this.NomeLocal_LicencaObraLocalizacaoObraAntiga).Append(" ");

            // LicencaObraRequerentes
            ret.Append(this.Nome_LicencaObraRequerentes).Append(" ");

            // LicencaObraTecnicoObra
            ret.Append(this.Termo_LicencaObraTecnicoObra).Append(" ");

            #endregion

            return ret.ToString();
        }
    }
}