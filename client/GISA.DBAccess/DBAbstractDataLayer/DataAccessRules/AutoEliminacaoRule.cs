using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DBAbstractDataLayer.DataAccessRules
{
    public abstract class AutoEliminacaoRule : DALRule
    {
        private static AutoEliminacaoRule current = null;
        public static void ClearCurrent()
        {
            current = null;
        }
        public static AutoEliminacaoRule Current
        {
            get
            {
                if (Object.ReferenceEquals(null, current))
                {
                    current = (AutoEliminacaoRule)Create(typeof(AutoEliminacaoRule));
                }
                return current;
            }
        }

        public class AutoEliminacao_UFsEliminadas {
            public long IDNivel;
            public bool paraEliminar;
            public string codigo;
            public string designacao;
            public decimal? largura = null;
            public decimal? altura = null;
            public decimal? profundidade = null;
            public string tipoMedida;
            public override string ToString() { return this.designacao; }
        }

        // métodos abstrat

        #region " AutoEliminacaoList "
        public abstract void CalculateOrderedItems(string FiltroDesignacao, IDbConnection conn);
        public abstract ArrayList GetItems(DataSet currentDataSet, int pageNr, int itemsPerPage, IDbConnection conn);
        public abstract void DeleteTemporaryResults(IDbConnection conn);
        #endregion

        public abstract void LoadAutoEliminacao(DataSet currentDataSet, long aeID, IDbConnection conn);

        public abstract void LoadAutoEliminacaoUFsID(DataSet currentDataSet, long aeID, IDbConnection conn);

        public abstract List<AutoEliminacao_UFsEliminadas> LoadUnidadesFisicasAvaliadas(DataSet currentDataSet, long aeID, IDbConnection conn);

        #region " PARA APAGAR QUANDO FOR RETIRADO O MÓDULO DE DEPÓSITOS ANTIGO "
        protected string Build_SQL_Largura_todas_ufs_nao_eliminadas()
        {
            return
                "SELECT SUM(uf.MedidaLargura) largura_ufs_nao_eliminadas " +
                "FROM SFRDUFDescricaoFisica uf " +
                "INNER JOIN FRDBase frd ON frd.ID = uf.IDFRDBase AND uf.isDeleted = 0 " +
                "INNER JOIN Nivel n ON n.ID = frd.IDNivel AND n.isDeleted = 0 " +
                "LEFT JOIN NivelUnidadeFisica nuf  ON nuf.ID = n.ID AND nuf.isDeleted = 0 " +
                "WHERE  (nuf.Eliminado is null OR nuf.Eliminado = 0) ";
        }
        public abstract double GetMetrosLinearesOcupados(IDbConnection conn);


        public class Info_UFs_Larguras
        {
            public long TotalUFs = 0;
            public long TotalUFs_semLargura = 0;
            public double Media_largura = 0.0;
        }

        protected string Build_SQL_GetTotalUFs()
        {
            return
                "SELECT COUNT(n.ID) TotalUFs, COUNT(base.ID) TotalUFs_sem_largura, COALESCE(AVG(sfrduf.MedidaLargura), 0)  media_larguras " +
                "FROM Nivel n " +
                "LEFT JOIN ( " +
                    "SELECT frd.ID ID, frd.IDNivel IDNIVEL, sfrduf.MedidaLargura FROM FRDBase frd  " +
                    "INNER JOIN SFRDUFDescricaoFisica sfrduf ON sfrduf.IDFRDBase = frd.ID AND sfrduf.MedidaLargura IS NULL " +
                " ) base ON base.IDNIVEL = n.ID " +
                " LEFT JOIN FRDBase frd ON frd.IDNivel = n.ID " +
                " LEFT JOIN SFRDUFDescricaoFisica sfrduf ON sfrduf.IDFRDBase = frd.ID " +
                " LEFT JOIN NivelUnidadeFisica nuf  ON nuf.ID = n.ID AND nuf.isDeleted = 0 " +
                " WHERE n.IDTipoNivel = 4 AND n.isDeleted = 0 " +
                " AND (nuf.Eliminado is null OR nuf.Eliminado = 0) ";
        }
        public abstract Info_UFs_Larguras Get_Info_UFs_Larguras(IDbConnection conn);
        #endregion
    }
}
