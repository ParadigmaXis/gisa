using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace DBAbstractDataLayer.DataAccessRules
{
	public abstract class PermissoesRule: DALRule
	{
		private static PermissoesRule current = null;
		public static void ClearCurrent() 
		{
			current = null;
		}
		public static PermissoesRule Current
		{
			get 
			{
				if (Object.ReferenceEquals(null, current)) 
				{
					current = (PermissoesRule) Create(typeof(PermissoesRule));
				}
				return current;
			}
		}

		#region Permissões por Módulo
		public abstract void LoadDataModuloPermissoes (DataSet CurrentDataSet, Int16 IDTipoFunctionGroup, Int16 IdxTipoFunction, IDbConnection conn);
		#endregion

		#region Permissões por Classificação de Informação
        public abstract void LoadDataCIPermissoes(DataSet CurrentDataSet, List<long> lstIDNivel, IDbConnection conn);
		public abstract void LoadDataCIPermissoes(DataSet CurrentDataSet, long IDNivel, IDbConnection conn);
        public abstract void LoadDataCIPermissoes(DataSet CurrentDataSet, long IDNivel, IDbTransaction tran);
        public abstract void GetEffectivePermissions(string query, long IDTrustee, IDbConnection conn);
        public abstract void GetEffectiveReadPermissions(string query, long IDTrustee, IDbConnection conn);
        public abstract Dictionary<long, Dictionary<string, bool>> GetEffectiveReadWritePermissions(long IDNivel, IDbConnection conn);
        public abstract void DropEffectivePermissionsTempTable(IDbConnection conn);

        internal static Dictionary<long, Dictionary<string, byte>> GetEffectivePermissions(IDataReader reader)
        {
            Dictionary<long, Dictionary<string, byte>> nivelPermissoes = new Dictionary<long, Dictionary<string, byte>>();
            Dictionary<string, byte> permisssaoPorTipoOperacao;
            long nivelID;
            while (reader.Read())
            {
                nivelID = reader.GetInt64(0);
                if (!nivelPermissoes.ContainsKey(nivelID))
                {
                    permisssaoPorTipoOperacao = new Dictionary<string, byte>();
                    if (!reader.IsDBNull(2)) permisssaoPorTipoOperacao.Add(reader.GetName(2), reader.GetByte(2));
                    if (!reader.IsDBNull(3)) permisssaoPorTipoOperacao.Add(reader.GetName(3), reader.GetByte(3));
                    if (!reader.IsDBNull(4)) permisssaoPorTipoOperacao.Add(reader.GetName(4), reader.GetByte(4));
                    if (!reader.IsDBNull(5)) permisssaoPorTipoOperacao.Add(reader.GetName(5), reader.GetByte(5));
                    if (!reader.IsDBNull(6)) permisssaoPorTipoOperacao.Add(reader.GetName(6), reader.GetByte(6));
                    

                    nivelPermissoes.Add(nivelID, permisssaoPorTipoOperacao);
                }
            }
            reader.Close();
            return nivelPermissoes;
        }

        internal static Dictionary<long, Dictionary<string, bool>> GetEffectiveReadWritePermissions(IDataReader reader)
        {
            Dictionary<long, Dictionary<string, bool>> nivelPermissoes = new Dictionary<long, Dictionary<string, bool>>();
            Dictionary<string, bool> permisssaoPorTipoOperacao;
            long trusteeID;
            while (reader.Read())
            {
                trusteeID = reader.GetInt64(0);
                if (!nivelPermissoes.ContainsKey(trusteeID))
                {
                    permisssaoPorTipoOperacao = new Dictionary<string, bool>();
                    if (!reader.IsDBNull(1)) permisssaoPorTipoOperacao.Add(reader.GetName(1), System.Convert.ToBoolean(reader.GetValue(1)));
                    if (!reader.IsDBNull(2)) permisssaoPorTipoOperacao.Add(reader.GetName(2), System.Convert.ToBoolean(reader.GetValue(2)));                    

                    nivelPermissoes.Add(trusteeID, permisssaoPorTipoOperacao);
                }
            }
            reader.Close();
            return nivelPermissoes;
        }
		#endregion

        #region FormAdUtilizadores
        public abstract void LoadUtilizadores (DataSet CurrentDataSet, IDbConnection conn);
		#endregion

		#region ControloNivelList
        public abstract Dictionary<long, Dictionary<string, byte>> CalculateEffectivePermissions(List<long> IDNiveis, long IDTrustee, IDbConnection conn);
        public abstract Dictionary<long, Dictionary<string, byte>> CalculateImplicitPermissions(long IDNivel, long IDTrustee, IDbConnection conn);
		#endregion

        #region SlavePanelPermissoesOBjDigital
        public struct ObjDig
        {
            public long ID;
            public string pid;
            public string titulo;
            public Dictionary<long, byte> Permissoes;
            public TrusteeRule.Nivel NivelPerm;
        }
        public abstract List<PermissoesRule.ObjDig> LoadDataObjDigital(DataSet CurrentDataSet, long IDNivel, long IDTrustee, long IDLoginTrustee, out Dictionary<long, Dictionary<long, byte>> permsImpl, IDbConnection conn);
        public abstract void GetODEffectivePermissions(string query, long IDTrustee, IDbConnection conn);
        public abstract int CalculateODGroupPermissions(long IDTrustee, long IDObjetoDigital, long IDOperation, IDbConnection conn);
        #endregion
    }
}
