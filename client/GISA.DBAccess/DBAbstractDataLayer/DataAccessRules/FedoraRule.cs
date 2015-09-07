using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBAbstractDataLayer.DataAccessRules
{
    public abstract class FedoraRule : DALRule
    {
        private static FedoraRule current = null;
        public static void ClearCurrent()
        {
            current = null;
        }
        public static FedoraRule Current
        {
            get
            {
                if (Object.ReferenceEquals(null, current))
                {
                    current = (FedoraRule)Create(typeof(FedoraRule));
                }
                return current;
            }
        }

        public abstract void LoadObjDigitalData(DataSet currentDataSet, long docID, long IDTipoNivelRelacionado, IDbConnection conn);
        public abstract void LoadObjDigitalSimples(DataSet currentDataSet, long docID, long IDTipoNivelRelacionado, IDbConnection conn);
        public abstract void LoadObjDigitalPermissoes(DataSet currentDataSet, long nRowID, IDbConnection conn);
        public abstract void LoadObjDigitalPermissoesSimples(DataSet currentDataSet, long nRowIDUpper, IDbConnection conn);
        public abstract bool GetObjDigitalSimplesPub(long nRowIDUpper, IDbConnection conn);
        public abstract void LoadSFRDImagemFedora(DataSet currentDataSet, long docID, long IDTipoNivelRelacionado, IDbConnection conn);
        public abstract void GetPidsPorNvl(List<string> IDNiveis, IDbConnection conn, out List<string> pids);
        public abstract bool CanUserDeleteAnyAssocOD2UI(long nivelID, long userID, IDbConnection conn);
        public abstract void LoadTitulos(DataSet currentDataSet, IDbConnection conn);
        public abstract List<string> GetAssociatedODs(long nivelID, IDbConnection conn);
    }
}
