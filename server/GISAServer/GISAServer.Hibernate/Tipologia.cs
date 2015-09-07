using System;
using System.Collections.Generic;
using System.Text;

using NHibernate;

using GISAServer.Hibernate.Objects;
using GISAServer.Hibernate.Utils;

namespace GISAServer.Hibernate
{
    public class Tipologia
    {
        private string id;
        private string formaAutorizada;
        private string designacao;
        private string validado;

        public string Id
        {
            get { return id; }
            set { this.id = value; }
        }

        public string FormaAutorizada
        {
            get { return formaAutorizada; }
            set { this.formaAutorizada = value; }
        }

        public string Designacao
        {
            get { return this.designacao; }
            set { this.designacao = value; }
        }

        public string Validado
        {
            get { return this.validado; }
            set { this.validado = value; }
        }

        public Tipologia(long idControloAut)
        {            
            // Id:
            this.id = idControloAut.ToString();

            this.FormaAutorizada = "";

            ISession session = null;
            try
            {
                session = GISAUtils.SessionFactory.OpenSession();

                StringBuilder designacoes = new StringBuilder();

                IQuery queryDict = session.CreateQuery("from ControloAutDicionarioEntity dict where dict.ControloAut.Id = ?  AND dict.IsDeleted = 0 ").SetInt64(0, idControloAut);
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

                ControloAutEntity ca = session.Get<ControloAutEntity>(idControloAut);
                this.Validado = ca.Autorizado ? "sim" : "nao";
            }
            catch (Exception) { throw; }
            finally
            {
                if (session != null)
                    session.Close();
            }
        }
    }
}
