using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

using GISAServer.Search;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GISAServer.SearchTester.Locks
{
    [TestClass]
    public class LocksTester
    {
        public static int doc_nr = 10000;
        public static string query = "document";
        public static string pattern = "test document {0}";
        public static string default_field = "designacao";

        [TestMethod]
        public void CreateIndexAndSearchConcurrently()
        {
            /*
             * Este método prova o conceito da utilização concorrente de um índice entre indexwriter e indexreader. Um índice é pesquisável quando bloqueado 
             * por um writer. O universo de documentos é aquele até o lock ser aplicado writer. Quando o writer termina a escrita, o índice é actualizado e 
             * a nova versão passa a estar acessível para consulta
             */

            // TODO: correcção de alguns bugs:
            //  * updater.ClearIndex nem sempre funciona e não consegui perceber quando não funciona (para evitar outros erros é preciso garantir que a pasta 
            //      index é apagada
            //  * o último assert falha sempre; desconfio que é um lock qualquer que está impedir que o index searcher pesquise sobre a versão do índice com dados

            var updater = new U(new MockUnidadeFisicaUpdater(pattern, doc_nr));
            var searcher = new S(new MockUnidadeFisicaSearcher("", default_field), query);
            
            updater.ClearIndex();

            Thread u_t = new Thread(new ParameterizedThreadStart(updater.CreateIndex));
            TestData td1 = new TestData() { e = new ManualResetEvent(false), x = new ManualResetEvent(false) };
            Thread s_t = new Thread(new ParameterizedThreadStart(searcher.Search));
            TestData td2 = new TestData() { e = td1.e, x = new ManualResetEvent(false) };
            Thread s_t2 = new Thread(new ParameterizedThreadStart(searcher.Search));
            TestData td3 = new TestData();

            u_t.Start(td1);
            s_t.Start(td2);

            td1.x.WaitOne();
            td2.x.WaitOne();
            td1.e.Set();

            u_t.Join();
            s_t.Join();

            Thread.Sleep(10000);

            s_t2.Start(td3);
            s_t2.Join();

            Assert.AreEqual(doc_nr, td1.docs_created);
            Assert.AreEqual(0, td2.hits);
            Assert.AreEqual(doc_nr, td3.hits);
        }

        class TestData
        {
            public int docs_created = 0;
            public int hits = 0;
            public ManualResetEvent e;
            public ManualResetEvent x;
        }

        class S
        {
            MockUnidadeFisicaSearcher C { get; set; }
            string Q { get; set; }
            public S(MockUnidadeFisicaSearcher c, string q) { C = c; Q = q; }
            public void Search(object obj) { TestData param = (TestData)obj; if (param.x != null) param.x.Set(); if (param.e != null)param.e.WaitOne(); C.IsIndexLocked(); param.hits = C.Search(Q).Count; }
        }

        class U
        {
            MockUnidadeFisicaUpdater C { get; set; }
            List<long> IDs { get; set; }
            public U(MockUnidadeFisicaUpdater c) { C = c; }
            public U(MockUnidadeFisicaUpdater c, List<long> ids) { C = c; IDs = ids; }
            public void CreateIndex(object obj) { TestData param = (TestData)obj; if (param.x != null) param.x.Set(); param.e.WaitOne(); C.CreateIndex(); param.docs_created = C.CountAllIndexRecords(); }
            
            public void UpdateIndex()
            {
                if (IDs == null) new Exception("IDs null");
                else if (IDs.Count == 0) new Exception("IDs empty");
                else C.Update(IDs);
            }
            public void ClearIndex() { C.ClearIndex(); }
        }
    }
}