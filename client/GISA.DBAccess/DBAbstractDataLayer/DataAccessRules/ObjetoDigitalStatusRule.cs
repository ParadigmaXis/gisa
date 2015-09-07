using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace DBAbstractDataLayer.DataAccessRules
{
    public abstract class ObjectoDigitalStatusRule : DALRule
    {
        private static ObjectoDigitalStatusRule current = null;
        public static void ClearCurrent()
        {
            current = null;
        }
        public static ObjectoDigitalStatusRule Current
        {
            get
            {
                if (Object.ReferenceEquals(null, current))
                {
                    current = (ObjectoDigitalStatusRule)Create(typeof(ObjectoDigitalStatusRule));
                }
                return current;
            }
        }

        public class ObjectoDigitalStatusInfo
        {
            public string pid;
            public string titulo;
            public string quality;
            public string state;
            public DateTime date;
            public long idNivel;
            public string designacaoNivel;
        }

        public abstract List<ObjectoDigitalStatusInfo> GetObjectoDigitalStatusInfo(IDbConnection conn, ArrayList orderInfo);
        public abstract void removeOldODsFromQueue(System.Data.IDbConnection conn);

        protected string BuildSQLQuery_GetObjectoDigitalStatusInfo(ArrayList orderInfo)
        {   // Query de testes (quando nao existirem dados na tabela ObjetoDigital):
            //string _s = " SELECT od.[pid] as od_pid, [Titulo], [quality],[state],[date] FROM [GISA].[dbo].[ObjetoDigital] od  LEFT OUTER JOIN [GISA].[dbo].[ObjetoDigitalStatus] st on od.pid = st.pid " +
            //    this.build_order_by_clause(orderInfo);

            string _s = @"
SELECT od.pid, od.Titulo, st.quality, st.state, st.date, n.ID, nd.Designacao
FROM ObjetoDigitalStatus st 
	INNER JOIN ObjetoDigital od on od.pid = st.pid and od.isDeleted = 0
	INNER JOIN SFRDImagemObjetoDigital imgOD on imgOD.IDObjetoDigital = od.ID and imgOD.isDeleted = 0
	INNER JOIN SFRDImagem img on img.IDFRDBase = imgOD.IDFRDBase and img.idx = imgOD.idx and img.isDeleted = 0
	INNER JOIN FRDBase frd on frd.ID = img.IDFRDBase and frd.isDeleted = 0
	INNER JOIN Nivel n on n.ID = frd.IDNivel and n.isDeleted = 0
	INNER JOIN NivelDesignado nd on nd.ID = n.ID and nd.isDeleted = 0
UNION	
SELECT od.pid, od.Titulo, st.quality, st.state, st.date, n.ID, nd.Designacao
FROM ObjetoDigitalStatus st 
	INNER JOIN ObjetoDigital od on od.pid = st.pid and od.isDeleted = 0
	INNER JOIN ObjetoDigitalRelacaoHierarquica odrh on odrh.ID = od.ID and odrh.isDeleted = 0
	INNER JOIN ObjetoDigital odComp on odComp.ID = odrh.IDUpper and odComp.isDeleted = 0
	INNER JOIN SFRDImagemObjetoDigital imgOD on imgOD.IDObjetoDigital = odComp.ID and imgOD.isDeleted = 0
	INNER JOIN SFRDImagem img on img.IDFRDBase = imgOD.IDFRDBase and img.idx = imgOD.idx and img.isDeleted = 0
	INNER JOIN FRDBase frd on frd.ID = img.IDFRDBase and frd.isDeleted = 0
	INNER JOIN Nivel n on n.ID = frd.IDNivel and n.isDeleted = 0
	INNER JOIN NivelDesignado nd on nd.ID = n.ID and nd.isDeleted = 0" + this.build_order_by_clause(orderInfo);
            return _s;
        }

        // oederInfo: contem pares [ 1, true, 4, false, ...]
        private string build_order_by_clause(ArrayList orderInfo)
        {
            string _o = "";

            for (int i = 0; i < orderInfo.Count; )
            {
                int _iCol = (int)orderInfo[i] + 1;
                string _asc = (bool)orderInfo[i + 1] ? " ASC " : " DESC ";
                _o += " " + _iCol + _asc;
                if (i + 2 < orderInfo.Count)
                    _o += ", ";

                i += 2;
            }
            if (!_o.Equals(""))
                _o = " ORDER BY " + _o;
            return _o;
        }

        protected List<ObjectoDigitalStatusInfo> BuildObjectoDigitalStatusInfo(IDataReader reader)
        {
            List<ObjectoDigitalStatusInfo> row = new List<ObjectoDigitalStatusInfo>();
            while (reader.Read())
            {
                ObjectoDigitalStatusInfo obj = new ObjectoDigitalStatusInfo();
                obj.pid = reader.GetValue(0).ToString();
                obj.titulo = reader.GetValue(1).ToString();
                obj.quality = reader.GetValue(2).ToString();
                obj.state = reader.GetValue(3).ToString();
                if (!reader.IsDBNull(4))
                    obj.date = reader.GetDateTime(4);
                obj.idNivel = reader.GetInt64(5);
                obj.designacaoNivel = reader.GetString(6);
                row.Add(obj);
            }
            return row;
        }
    }
}
