using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using NUnit.Framework;
using GISAServer.Hibernate;
using GISAServer.Hibernate.Exceptions;
using GISAServer.Hibernate.Utils;

namespace GISAServer.HibernateTester
{
    [TestFixture]    
    public class UnidadeFisicaTester
    {                       

        [Test]
        [ExpectedException(typeof(InvalidIdException))]
        public void GetANegativeIdUnidadeFisica()
        {
            UnidadeFisica uf = new UnidadeFisica(-1);
        }        

        [Test]        
        public void GetARandomUnidadeFisica()
        {            
            IList<long> ids = GISAUtils.getAllUnidadesFisicasIds();

            if (ids.Count > 0)
            {
                Random random = new Random();
                int pos = random.Next(0, ids.Count - 1);
                
                UnidadeFisica uf = new UnidadeFisica(ids[pos]);                                
                Assert.IsNotNull(uf);
            }
        }

        [Test]
        public void GetAllUnidadesFisicas()
        {
            IList<long> ids = GISAUtils.getAllUnidadesFisicasIds();

            foreach (long id in ids)
            {
                UnidadeFisica uf = new UnidadeFisica(id);
                Assert.IsNotNull(uf);
            }
        }

        [Test]
        public void DateWellFormedString()
        {
            IList<long> ids = GISAUtils.getAllUnidadesFisicasIds();

            foreach (long id in ids)
            {
                UnidadeFisica uf = new UnidadeFisica(id);
                Console.WriteLine(uf.DataInicioProd);
                Console.WriteLine(uf.DataFimProd);
                Assert.AreEqual(8, uf.DataInicioProd.Length);
                Assert.AreEqual(8, uf.DataFimProd.Length);                
            }
        }

        [Test]
        public void GetOperator()
        {
            string searchText = "operador: xpto";
            Match m = Regex.Match(searchText, @"operador([ ]*)?:([ ]*)?([a-zA-Z0-9]* + \([a-zA-Z0-9 ]*\))");
            if (m.Success)
            {
                string operador = m.ToString().Replace("operador:","").Trim();
                Console.WriteLine("operador: {0}", operador);
            }
        }
    }
}
