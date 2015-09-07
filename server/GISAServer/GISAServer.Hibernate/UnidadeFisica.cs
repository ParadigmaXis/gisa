using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using log4net;
using GISAServer.Hibernate.Objects;
using GISAServer.Hibernate.Utils;

namespace GISAServer.Hibernate
{
    public class UnidadeFisica
    {
        // Logging initializations
        private static readonly ILog log = LogManager.GetLogger(typeof(UnidadeFisica));
        
        // Nivel.ID:
        private string id = "";
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string numero = "";
        public string Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        private string designacao = "";
        public string Designacao
        {
            get { return designacao; }
            set { designacao = value; }
        }

        private string cota = "";
        public string Cota
        {
            get { return cota; }
            set { cota = value; }
        }

        private string dataInicioProd = "00000000";
        public string DataInicioProd
        {
            get { return dataInicioProd; }
            set { dataInicioProd = value; }
        }

        private string dataFimProd = "00000000";
        public string DataFimProd
        {
            get { return dataFimProd; }
            set { dataFimProd = value; }
        }

        private string conteudoInformacional = "";
        public string ConteudoInformacional
        {
            get { return conteudoInformacional; }
            set { conteudoInformacional = value; }
        }

        private string tipoUnidadeFisica = "";
        public string TipoUnidadeFisica
        {
            get { return tipoUnidadeFisica; }
            set { tipoUnidadeFisica = value; }
        }                

        private string guiaIncorporacao = "";
        public string GuiaIncorporacao
        {
            get { return guiaIncorporacao; }
            set { guiaIncorporacao = value; }
        }

        private string codigoBarras = "";
        public string CodigoBarras
        {
            get { return codigoBarras; }
            set { codigoBarras = value; }
        }

        private string eliminado = "";
        public string Eliminado {
            get { return eliminado; }
            set { eliminado = value; }
        }

        public UnidadeFisica() { }
        public UnidadeFisica(long idNivel)
        {
            ISession session = null;
            try
            {
                session = GISAUtils.SessionFactory.OpenSession();
                var ufs = session.CreateSQLQuery(GISAUtils.getAllUnidadesFisicasQuery + " and n.ID = " + idNivel.ToString());
                ufs.SetTimeout(1000);

                foreach (var uf in ufs.List<object[]>())
                {
                    this.id = uf[0].ToString();
                    this.numero = uf[1].ToString();
                    this.designacao = uf[2].ToString();
                    this.codigoBarras = uf[3].ToString();
                    this.guiaIncorporacao = uf[4].ToString();
                    this.eliminado = uf[5].ToString().Equals("1") ? "sim" : "nao";
                    this.cota = uf[6].ToString();
                    this.dataInicioProd = GISAUtils.DataInicioProdFormatada(uf[7].ToString(), uf[8].ToString(), uf[9].ToString());
                    this.dataFimProd = GISAUtils.DataInicioProdFormatada(uf[10].ToString(), uf[11].ToString(), uf[12].ToString());
                    this.conteudoInformacional = uf[13].ToString();
                    this.tipoUnidadeFisica = uf[14].ToString();
                }
            }
            catch (Exception) { throw; }
            finally
            {
                if (session != null) session.Close();
            }

            //ISession session = null;
            //try
            //{
            //    session = GISAUtils.SessionFactory.OpenSession();

            //    // Codigo e designacao
            //    NivelEntity nivel = session.Get<NivelEntity>(idNivel);
            //    if (nivel != null && nivel.Codigo != null && !nivel.IsDeleted)
            //    {
            //        this.id = nivel.Id.ToString();
            //        this.numero = nivel.Codigo;

            //        NivelDesignadoEntity nd = session.Get<NivelDesignadoEntity>(nivel.Id);
            //        if (nd != null && nd.Designacao != null && !nd.IsDeleted)
            //        {
            //            this.designacao = nd.Designacao;

            //            NivelUnidadeFisicaEntity nuf = session.Get<NivelUnidadeFisicaEntity>(nd.Id);
            //            if (nuf != null && nuf.CodigoBarras != null && !nuf.IsDeleted)
            //                this.CodigoBarras = nuf.CodigoBarras.ToString();
            //        }

            //        // Guia de incorporacao, Eliminado:
            //        NivelUnidadeFisicaEntity nufe = session.Get<NivelUnidadeFisicaEntity>(nivel.Id);
            //        if (nufe != null && !nufe.IsDeleted)
            //        {
            //            if (nufe.GuiaIncorporacao != null)
            //                this.guiaIncorporacao = nufe.GuiaIncorporacao;
            //            this.eliminado = nufe.Eliminado ? "sim" : "nao";
            //        }
            //        else
            //            this.eliminado = "nao";
            //    }

            //    long idFRDBase = -1;
            //    IQuery qFrdbase = session.CreateQuery("from FRDBaseEntity frdb WHERE frdb.Nivel.Id = " + idNivel);
            //    qFrdbase.SetTimeout(1000);
            //    FRDBaseEntity frdbase = qFrdbase.UniqueResult<FRDBaseEntity>();

            //    if (frdbase == null)
            //    {
            //        return;
            //    }

            //    idFRDBase = frdbase.Id;

            //    // Cota                       
            //    SFRDUFCotaEntity sfrdufc = session.Get<SFRDUFCotaEntity>(idFRDBase);
            //    if (sfrdufc != null && !sfrdufc.IsDeleted)
            //    {
            //        if (sfrdufc.Cota != null)
            //            this.cota = sfrdufc.Cota;
            //    }

            //    // Datas de producao
            //    SFRDDatasProducaoEntity sfrddp = session.Get<SFRDDatasProducaoEntity>(idFRDBase);

            //    this.dataInicioProd = GISAUtils.DataInicioProdFormatada(sfrddp);
            //    this.dataFimProd = GISAUtils.DataFimProdFormatada(sfrddp);

            //    // Conteudo informacional e guia incorporacao
            //    SFRDConteudoEEstruturaEntity sfrdcee = session.Get<SFRDConteudoEEstruturaEntity>(idFRDBase);
            //    if (sfrdcee != null && sfrdcee.ConteudoInformacional != null && !sfrdcee.IsDeleted)
            //    {
            //        this.conteudoInformacional = sfrdcee.ConteudoInformacional;
            //    }

            //    // Tipo de unidade fisica
            //    SFRDUFDescricaoFisicaEntity sfrdufdf = session.Get<SFRDUFDescricaoFisicaEntity>(idFRDBase);
            //    if (sfrdufdf != null && !sfrdufdf.IsDeleted)
            //    {
            //        TipoAcondicionamentoEntity ta = sfrdufdf.TipoAcondicionamento;
            //        if (ta != null && ta.Designacao != null && !ta.IsDeleted)
            //        {
            //            this.tipoUnidadeFisica = ta.Designacao;
            //        }
            //    }

            //    // Operador/Grupo - on the fly

            //    // Unidades arquivisticas - on the fly
            //}
            //catch (Exception) { throw; }
            //finally
            //{
            //    if (session != null) session.Close();
            //}
        }
    }
}
