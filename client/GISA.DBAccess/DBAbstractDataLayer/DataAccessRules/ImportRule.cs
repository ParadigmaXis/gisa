using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBAbstractDataLayer.DataAccessRules
{
    public abstract class ImportRule : DALRule
    {
        private static ImportRule current = null;
        public static void ClearCurrent()
        {
            current = null;
        }

        public static ImportRule Current
        {
            get
            {
                if (Object.ReferenceEquals(null, current))
                {
                    current = (ImportRule)Create(typeof(ImportRule));
                }
                return current;
            }
        }

        public abstract void LoadDocumentos(DataSet currentDataSet, string[] designacoes, IDbConnection conn);
        public abstract void LoadUnidadesFisicas(DataSet currentDataSet, string[] codigos, IDbConnection conn);
        public abstract void LoadControloAuts(DataSet currentDataSet, string[] designacoes, IDbConnection conn);
    }
}
