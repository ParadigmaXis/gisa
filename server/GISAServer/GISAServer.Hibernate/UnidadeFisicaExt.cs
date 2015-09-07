using System;
using System.Collections.Generic;
using System.Text;

using NHibernate;

using GISAServer.Hibernate.Objects;
using GISAServer.Hibernate.Utils;


namespace GISAServer.Hibernate
{
    public class UnidadeFisicaExt
    {
        public static IList<long> GetTrusteeUsersIds(string operador)
        {
            ISession session = GISAUtils.SessionFactory.OpenSession();
            IQuery q = session.CreateQuery("FROM TrusteeEntity t WHERE " + operador);
            
            IList<long> ret = new List<long>();

            foreach (TrusteeEntity t in q.Enumerable<TrusteeEntity>())
            {
                if (t.CatCode.Equals("GRP"))
                {
                    IQuery qGRP = session.CreateQuery("FROM UserGroupsEntity ug WHERE ug.Group.Id = " + t.Id);
                    foreach (UserGroupsEntity ug in qGRP.Enumerable<UserGroupsEntity>())
                    {
                        ret.Add(ug.User.Id);
                    }
                }
                else
                {
                    ret.Add(t.Id);                    
                }
                
            }
            session.Close();

            return ret;
        }

        public static IList<long> GetFRDBaseIds(string operador,DateTime? inicio, DateTime? fim)
        {
            
            string qOperador = "";
            if (operador != null && !operador.Equals(""))
            {
             
                IList<long> trusteeUsers = GetTrusteeUsersIds(operador);

                if (trusteeUsers.Count > 0)
                {
                    string[] ids = new string[trusteeUsers.Count];

                    for (int i = 0; i < ids.Length; i++)
                    {
                        ids[i] = trusteeUsers[i].ToString();
                    }
                    qOperador = "fddd.TrusteeOperator.ID in (" + string.Join(",", ids) + ") AND";
                                      
                }
                else
                {
                    // Se não existir operador 
                    // não devolve resultados  
                    return new List<long>(0);
                }
                

            }

            string query = string.Format("select distinct fddd.FRDBase.Id from FRDBaseDataDeDescricaoEntity fddd where {0} fddd.IsDeleted = 0 AND fddd.FRDBase.TipoFRDBase.Id = 2", qOperador);

            ISession session = GISAUtils.SessionFactory.OpenSession();
            
            if (inicio != null)
            {
                query += " AND fddd.DataEdicao >= :dataInicial";
            }

            if (fim != null)
            {
                query += " AND fddd.DataEdicao <= :dataFinal";
            }

            IQuery q = session.CreateQuery(query);

            if (inicio != null)
            {
                q.SetDateTime("dataInicial", (DateTime) inicio);
            }

            if (fim != null)
            {
                q.SetDateTime("dataFinal", (DateTime)fim);
            }

            IList<long> ret = q.List<long>();
            session.Close();
            return ret;

        }
    }
}
