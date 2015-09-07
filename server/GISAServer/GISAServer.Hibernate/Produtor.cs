using System;
using System.Collections.Generic;
using System.Text;

using NHibernate;

using GISAServer.Hibernate.Objects;
using GISAServer.Hibernate.Utils;

namespace GISAServer.Hibernate
{
    public class Produtor
    {
        private long id;
        private string formaAutorizada;
        private string tipoNivel;   // Designacao do tipoNivel
        private string designacao;  // Termos do dicionario
        private string validado;  

        public Produtor(long idControloAut) {            

            ISession session = null;
            try
            {
                session = GISAUtils.SessionFactory.OpenSession();

                this.FormaAutorizada = "";

                // Designacao:
                StringBuilder designacoes = new StringBuilder();
                ControloAutEntity ca = session.Get<ControloAutEntity>(idControloAut);

                this.id = ca.Id;

                IQuery queryDict = session.CreateQuery("from ControloAutDicionarioEntity dict where dict.ControloAut.Id = ?  AND dict.IsDeleted = 0 ").SetInt64(0, ca.Id);
                queryDict.SetTimeout(1000);
                foreach (ControloAutDicionarioEntity controloAutDict in queryDict.Enumerable<ControloAutDicionarioEntity>())
                {
                    if (controloAutDict.TipoControloAutForma.Id == 1)
                        this.FormaAutorizada = controloAutDict.Dicionario.Termo;

                    if (controloAutDict.Dicionario != null)
                    {
                        designacoes.Append(controloAutDict.Dicionario.Termo).Append(" ");
                    }
                }
                this.designacao = designacoes.ToString();
                this.Validado = ca.Autorizado ? "sim" : "nao";

                IQuery query_nivelControloAut = session.CreateQuery("from NivelControloAutEntity ncae where ncae.ControloAut.Id = ? AND ncae.IsDeleted = 0 ").SetInt64(0, idControloAut);
                query_nivelControloAut.SetTimeout(1000);
                NivelControloAutEntity ncae = query_nivelControloAut.UniqueResult<NivelControloAutEntity>();

                if (ncae != null)
                {
                    long idNivel = ncae.Id;

                    // TipoNivel:
                    StringBuilder tipoNiveis = new StringBuilder();
                    IQuery query = session.CreateQuery("from RelacaoHierarquicaEntity entH where entH.ID.Id = ?  AND entH.IsDeleted = 0 ").SetInt64(0, idNivel);
                    query.SetTimeout(1000);

                    foreach (RelacaoHierarquicaEntity relHEnt in query.Enumerable<RelacaoHierarquicaEntity>())
                    {
                        tipoNiveis.Append(relHEnt.TipoNivelRelacionado.Designacao).Append(" ");
                    }

                    this.tipoNivel = tipoNiveis.ToString();
                }
                else
                {
                    this.tipoNivel = "";
                }
            }
            catch (Exception) { throw; }
            finally
            {
                if (session != null)
                    session.Close();
            }
        }

        public long Id
        {
            get { return this.id; }
        }

        public string FormaAutorizada
        {
            get { return formaAutorizada; }
            set { this.formaAutorizada = value; }
        }

        public string TipoNivel
        {
            get { return tipoNivel; }
            set { tipoNivel = value; }
        }

        public string Designacao
        {
            get { return designacao; }
            set { designacao = value; }
        }

        public string Validado
        {
            get { return validado; }
            set { validado = value; }
        }
    }
}
