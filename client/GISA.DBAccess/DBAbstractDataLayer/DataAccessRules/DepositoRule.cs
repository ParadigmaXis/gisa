using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DBAbstractDataLayer.DataAccessRules
{
    public abstract class DepositoRule : DALRule
    {
        private static DepositoRule current = null;
        public static void ClearCurrent()
        {
            current = null;
        }
        public static DepositoRule Current
        {
            get
            {
                if (Object.ReferenceEquals(null, current))
                {
                    current = (DepositoRule)Create(typeof(DepositoRule));
                }
                return current;
            }
        }

        #region " DepositoList "
        public abstract void CalculateOrderedItems(string FiltroDesignacaoLike, IDbConnection conn);
        public abstract int GetPageForID(long depositoID, int pageLimit, IDbConnection conn);
        public abstract int CountPages(int itemsPerPage, ref int numberOfItems, IDbConnection conn);
        public abstract ArrayList GetItems(DataSet currentDataSet, int pageNr, int itemsPerPage, IDbConnection conn);
        public abstract void DeleteTemporaryResults(IDbConnection conn);
        #endregion

        #region " MasterPanelDepositos "
        public abstract double GetMetrosLinearesTotais(long depositoID, IDbConnection conn);

        protected string Build_SQL_Largura_todas_ufs_nao_eliminadas(long depositoID)
        {
            return string.Format(
                "SELECT SUM(uf.MedidaLargura) largura_ufs_nao_eliminadas " +
                "FROM SFRDUFDescricaoFisica uf " +
                "INNER JOIN FRDBase frd ON frd.ID = uf.IDFRDBase AND uf.isDeleted = 0 " +
                "INNER JOIN Nivel n ON n.ID = frd.IDNivel AND n.isDeleted = 0 " +
                "INNER JOIN NivelUnidadeFisica nuf  ON nuf.ID = n.ID AND nuf.isDeleted = 0 " +
                "INNER JOIN NivelUnidadeFisicaDeposito nufd ON nufd.IDNivelUnidadeFisica = nuf.ID AND nufd.isDeleted = 0 {0} " +
                "WHERE  (nuf.Eliminado is null OR nuf.Eliminado = 0) ", depositoID == long.MinValue ? string.Empty : " AND nufd.IDDeposito = " + depositoID.ToString());
        }
        public abstract double GetMetrosLinearesOcupados(long depositoID, IDbConnection conn);

        public class Info_UFs_Larguras
        {
            public long TotalUFs = 0;
            public long TotalUFs_semLargura = 0;
            public double Media_largura = 0.0;
        }

        protected string Build_SQL_GetTotalUFs(long depositoID)
        {
            return string.Format(
                "SELECT COUNT(n.ID) TotalUFs, COUNT(base.ID) TotalUFs_sem_largura, COALESCE(AVG(sfrduf.MedidaLargura), 0)  media_larguras " +
                "FROM Nivel n " +
                "LEFT JOIN ( " +
                    "SELECT frd.ID ID, frd.IDNivel IDNIVEL, sfrduf.MedidaLargura FROM FRDBase frd  " +
                    "INNER JOIN SFRDUFDescricaoFisica sfrduf ON sfrduf.IDFRDBase = frd.ID AND sfrduf.MedidaLargura IS NULL " +
                " ) base ON base.IDNIVEL = n.ID " +
                " LEFT JOIN FRDBase frd ON frd.IDNivel = n.ID " +
                " LEFT JOIN SFRDUFDescricaoFisica sfrduf ON sfrduf.IDFRDBase = frd.ID " +
                " INNER JOIN NivelUnidadeFisica nuf ON nuf.ID = n.ID AND nuf.isDeleted = 0 " +
                " INNER JOIN NivelUnidadeFisicaDeposito nufd ON nufd.IDNivelUnidadeFisica = nuf.ID AND nufd.isDeleted = 0 {0} " +
                " WHERE n.IDTipoNivel = 4 AND n.isDeleted = 0 " +
                " AND (nuf.Eliminado is null OR nuf.Eliminado = 0)", depositoID == long.MinValue ? string.Empty : " AND nufd.IDDeposito = " + depositoID.ToString());
        }
        public abstract Info_UFs_Larguras Get_Info_UFs_Larguras(long depositoID, IDbConnection conn);
        #endregion

        #region " MasterPanelDepositos "
        public abstract void LoadDepositoData(DataSet currentDataSet, long depositoID, IDbConnection conn);
        public abstract void LoadDepositodDataForUpdate(DataSet currentDataSet, long depositoID, IDbTransaction tran);
        public abstract bool CanDeleteDeposito(long depositoID, IDbTransaction tran);
        #endregion

        #region " PanelDepIdentificao "
        public abstract HashSet<UFRule.UnidadeFisicaInfo> LoadDepIdentificacaoData(DataSet currentDataSet, long depositoID, IDbConnection conn);
        public abstract HashSet<UFRule.UnidadeFisicaInfo> LoadUFData(long nivelID, IDbConnection conn);
        #endregion

        #region " PanelDepUFEliminadas "
        public abstract void LoadAutosEliminacao(DataSet currentDataSet, IDbConnection conn);
        #endregion

        #region " SlavePanelPermissoesDesposito "
        public abstract void LoadDepositosPermissionsData(DataSet currentDataSet, long trusteeID, IDbConnection conn);
        #endregion
    }
}